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
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Data.IconData;

namespace StardewDruid.Event.Access
{
    public class AccessHandle
    {

        public GameLocation location;

        public Vector2 stair;

        public Vector2 exit;

        public string entrance;

        public string access;

        public bool manualSet;

        public AccessHandle() { }

        public void AccessSetup(string Entrance, string Access, Vector2 Stair, Vector2 Exit)
        {

            stair = Stair;

            entrance = Entrance;

            access = Access;

            exit = Exit;

        }

        public void AccessQuery()
        {

            List<string> array = new()
            {
                stair.X.ToString(),
                stair.Y.ToString(),
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

            Mod.instance.EventQuery(query, QueryData.queries.AccessHandle);

        }

        public void AccessSetup(List<string> accessData)
        {

            stair = new(Convert.ToInt32(accessData[0]), Convert.ToInt32(accessData[1]));

            entrance = accessData[2];

            access = accessData[3];

            exit = new(Convert.ToInt32(accessData[4]), Convert.ToInt32(accessData[5]));

        }

        public virtual void AccessCheck(GameLocation Location)
        {

            location = Location;

            if (CheckStair())
            {

                AccessClear();

                return;

            }

            AccessStair();

            AccessWarps();

        }

        public virtual bool CheckStair()
        {

            foreach (Warp warp in location.warps)
            {

                if (warp.X == stair.X && warp.Y == stair.Y)
                {

                    return true;

                }

            }

            return false;

        }

        public void AccessClear()
        {

            int tilex = (int)stair.X;

            int tiley = (int)stair.Y;

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

                if (warp.X == stair.X && (warp.Y == stair.Y || warp.Y == stair.Y + 1))
                {

                    location.warps.RemoveAt(w);

                    continue;

                }

            }

            if (Utility.isOnScreen(stair * 64, 64))
            {

                Mod.instance.iconData.ImpactIndicator(location, stair * 64 + new Vector2(64, 0), IconData.impacts.impact, 6, new());

                location.playSound("boulderCrack");

            }

        }

        public void AccessStair()
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

            int tilex = (int)stair.X;

            int tiley = (int)stair.Y;

            Layer buildings = location.map.GetLayer("Buildings");

            buildings.Tiles[tilex, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 70);

            buildings.Tiles[tilex, tiley].TileIndexProperties.Add("Passable", new(true));

            buildings.Tiles[tilex + 1, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 71);

            buildings.Tiles[tilex + 2, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 72);

            buildings.Tiles[tilex, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 84);

            buildings.Tiles[tilex, tiley + 1].TileIndexProperties.Add("Passable", new(true));

            buildings.Tiles[tilex + 1, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 85);

            buildings.Tiles[tilex + 2, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 86);

            location.localSound("secret1");

            if (Context.IsMultiplayer && Context.IsMainPlayer)
            {

                AccessQuery();

            }

            if (Utility.isOnScreen(stair * 64, 64))
            {

                ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

                Mod.instance.iconData.ImpactIndicator(location, stair * 64 + new Vector2(64, 0), IconData.impacts.impact, 6, new());

                location.playSound("boulderBreak");

            }

        }

        public void AccessWarps()
        {

            int tilex = (int)stair.X;

            int tiley = (int)stair.Y;

            location.warps.Add(new Warp(tilex, tiley, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

            location.warps.Add(new Warp(tilex, tiley + 1, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

        }

    }

}
