using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Layers;
using xTile.Tiles;


namespace StardewDruid.Event.Access
{
    public class AccessHandle
    {

        public GameLocation location;

        public Vector2 start;

        public Vector2 exit;

        public string entrance;

        public string access;

        public bool manualSet;

        public QueryData.queries queryHandle = QueryData.queries.AccessHandle;

        public AccessHandle() { }

        public void AccessSetup(string Entrance, string Access, Vector2 Start, Vector2 Exit)
        {

            start = Start;

            entrance = Entrance;

            access = Access;

            exit = Exit;

        }

        public virtual void AccessQuery()
        {

            List<string> array = new()
            {
                start.X.ToString(),
                start.Y.ToString(),
                entrance,
                access,
                exit.X.ToString(),
                exit.Y.ToString(),
            };

            QueryData query = new()
            {
                name = location.Name,

                value = System.Text.Json.JsonSerializer.Serialize(array),

                location = location.Name,

            };

            Mod.instance.EventQuery(query, queryHandle);

        }

        public void AccessSetup(List<string> accessData)
        {

            start = new(Convert.ToInt32(accessData[0]), Convert.ToInt32(accessData[1]));

            entrance = accessData[2];

            access = accessData[3];

            exit = new(Convert.ToInt32(accessData[4]), Convert.ToInt32(accessData[5]));

        }

        public virtual void AccessCheck(GameLocation Location, bool clear = false)
        {

            location = Location;

            if (CheckStart())
            {
                
                if (clear)
                {

                    AccessClear();

                }

                return;

            }

            AccessStart();

            AccessWarps();

        }

        public virtual bool CheckStart()
        {

            foreach (Warp warp in location.warps)
            {

                if (warp.X == start.X && warp.Y == start.Y)
                {

                    return true;

                }

            }

            return false;

        }

        public virtual void AccessClear()
        {

            int tilex = (int)start.X;

            int tiley = (int)start.Y;

            Layer buildings = location.map.GetLayer("Buildings");

            buildings.Tiles[tilex, tiley] = null;

            buildings.Tiles[tilex + 1, tiley] = null;

            buildings.Tiles[tilex + 2, tiley] = null;

            buildings.Tiles[tilex, tiley + 1] = null;

            buildings.Tiles[tilex + 1, tiley + 1] = null;

            buildings.Tiles[tilex + 2, tiley + 1] = null;

            for(int w = location.warps.Count - 1; w >= 0; w--)
            {

                Warp warp = location.warps.ElementAt(w);

                if (warp.X == start.X && (warp.Y == start.Y || warp.Y == start.Y + 1))
                {

                    location.warps.RemoveAt(w);

                    continue;

                }

            }

            if (Utility.isOnScreen(start * 64, 64))
            {

                Mod.instance.iconData.ImpactIndicator(location, start * 64 + new Vector2(64, 0), IconData.impacts.impact, 6, new());

                location.playSound("boulderCrack");

            }

        }

        public virtual void AccessStart(bool animate = true)
        {

            TileSheet tileSheet = new(
                location.map,
                "StardewDruid.Tilesheets.chapel",
                new(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.chapel].Width / 16,
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.chapel].Height / 16
                ),
                new(16, 16)
            );

            location.map.AddTileSheet(tileSheet);

            location.map.LoadTileSheets(Game1.mapDisplayDevice);

            int tilex = (int)start.X;

            int tiley = (int)start.Y;

            Layer buildings = location.map.GetLayer("Buildings");

            buildings.Tiles[tilex, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 70);

            buildings.Tiles[tilex, tiley].TileIndexProperties.Add("Passable", new(true));

            buildings.Tiles[tilex + 1, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 71);

            buildings.Tiles[tilex + 2, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 72);

            buildings.Tiles[tilex, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 84);

            buildings.Tiles[tilex, tiley + 1].TileIndexProperties.Add("Passable", new(true));

            buildings.Tiles[tilex + 1, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 85);

            buildings.Tiles[tilex + 2, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 86);

            if (Context.IsMultiplayer && Context.IsMainPlayer)
            {

                AccessQuery();

            }

            if (Utility.isOnScreen(start * 64, 64) && animate)
            {

                ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

                Mod.instance.iconData.ImpactIndicator(location, start * 64 + new Vector2(64, 0), IconData.impacts.impact, 6, new());

                location.playSound(SpellHandle.sounds.boulderBreak.ToString());

                location.playSound(SpellHandle.sounds.secret1.ToString());

            }

        }

        public virtual void AccessWarps()
        {

            int tilex = (int)start.X;

            int tiley = (int)start.Y;

            location.warps.Add(new Warp(tilex, tiley, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

            location.warps.Add(new Warp(tilex, tiley + 1, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

        }

    }

}
