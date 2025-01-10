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

        public enum types
        {
            stair,
            door,
        }

        public types type;

        public AccessHandle() { }

        public void AccessSetup(string Entrance, string Access, Vector2 Start, Vector2 Exit, types AccessType = types.stair)
        {

            start = Start;

            entrance = Entrance;

            access = Access;

            exit = Exit;

            type = AccessType;

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
                type.ToString(),
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

            type = Enum.Parse<types>(accessData[6]);

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

                if ((warp.X == start.X || warp.X == start.X + 1 || warp.X == start.X + 2) && (warp.Y == start.Y || warp.Y == start.Y + 1))
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

            switch (type)
            {

                case types.stair:

                    buildings.Tiles[tilex, tiley] = null;

                    buildings.Tiles[tilex + 1, tiley] = null;

                    buildings.Tiles[tilex + 2, tiley] = null;

                    buildings.Tiles[tilex, tiley + 1] = null;

                    buildings.Tiles[tilex + 1, tiley + 1] = null;

                    buildings.Tiles[tilex + 2, tiley + 1] = null;

                    break;

                case types.door:

                    Layer front = location.map.GetLayer("Front");

                    buildings.Tiles[tilex, tiley] = null;

                    buildings.Tiles[tilex + 1, tiley] = null;

                    buildings.Tiles[tilex + 2, tiley] = null;

                    front.Tiles[tilex, tiley - 1] = null;

                    front.Tiles[tilex + 1, tiley - 1] = null;

                    front.Tiles[tilex + 2, tiley - 1] = null;

                    front.Tiles[tilex, tiley - 2] = null;

                    front.Tiles[tilex + 1, tiley - 2] = null;

                    front.Tiles[tilex + 2, tiley - 2] = null;

                    front.Tiles[tilex, tiley - 3] = null;

                    front.Tiles[tilex + 1, tiley - 3] = null;

                    front.Tiles[tilex + 2, tiley - 3] = null;

                    break;

            }

            for(int w = location.warps.Count - 1; w >= 0; w--)
            {

                Warp warp = location.warps.ElementAt(w);

                if ((warp.X == start.X || warp.X == start.X + 1 || warp.X == start.X + 2) && (warp.Y == start.Y || warp.Y == start.Y + 1))
                {

                    location.warps.RemoveAt(w);

                    continue;

                }

            }

            if (Utility.isOnScreen(start * 64, 64))
            {

                Mod.instance.spellRegister.Add(new(start * 64 + new Vector2(64, 0), 384, IconData.impacts.impact, new()) { sound = SpellHandle.sounds.boulderBreak, });

            }

        }

        public virtual void AccessStart(bool animate = true)
        {

            string accessSheet = "StardewDruid.Tilesheets.access";

            TileSheet tileSheet = new(
                location.map,
                accessSheet,
                new(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.access].Width / 16,
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.access].Height / 16
                ),
                new(16, 16)
            );

            location.map.AddTileSheet(tileSheet);

            location.map.LoadTileSheets(Game1.mapDisplayDevice);

            int tilex = (int)start.X;

            int tiley = (int)start.Y;

            Layer buildings = location.map.GetLayer("Buildings");

            switch (type)
            {

                case types.stair:

                    buildings.Tiles[tilex, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 0);

                    buildings.Tiles[tilex, tiley].TileIndexProperties.Add("Passable", new(true));

                    buildings.Tiles[tilex + 1, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 1);

                    buildings.Tiles[tilex + 2, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 2);

                    buildings.Tiles[tilex, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 12);

                    buildings.Tiles[tilex, tiley + 1].TileIndexProperties.Add("Passable", new(true));

                    buildings.Tiles[tilex + 1, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 13);

                    buildings.Tiles[tilex + 2, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 14);

                    break;

                case types.door:

                    Layer front = location.map.GetLayer("Front");

                    Layer alwaysfront = location.map.GetLayer("AlwaysFront");

                    buildings.Tiles[tilex, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 39);

                    //buildings.Tiles[tilex, tiley].TileIndexProperties.Add("Passable", new(true));

                    buildings.Tiles[tilex + 1, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 40);

                    buildings.Tiles[tilex, tiley].TileIndexProperties.Add("Passable", new(true));

                    buildings.Tiles[tilex + 2, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha,41);

                    //buildings.Tiles[tilex, tiley].TileIndexProperties.Add("Passable", new(true));

                    front.Tiles[tilex, tiley - 1] = new StaticTile(front, tileSheet, BlendMode.Alpha, 27);

                    front.Tiles[tilex + 1, tiley - 1] = new StaticTile(front, tileSheet, BlendMode.Alpha, 28);

                    front.Tiles[tilex + 2, tiley - 1] = new StaticTile(front, tileSheet, BlendMode.Alpha, 29);

                    front.Tiles[tilex, tiley - 2] = new StaticTile(front, tileSheet, BlendMode.Alpha, 15);

                    front.Tiles[tilex + 1, tiley - 2] = new StaticTile(front, tileSheet, BlendMode.Alpha, 16);

                    front.Tiles[tilex + 2, tiley - 2] = new StaticTile(front, tileSheet, BlendMode.Alpha, 17);

                    front.Tiles[tilex, tiley - 3] = new StaticTile(front, tileSheet, BlendMode.Alpha, 3);

                    front.Tiles[tilex + 1, tiley - 3] = new StaticTile(front, tileSheet, BlendMode.Alpha, 4);

                    front.Tiles[tilex + 2, tiley - 3] = new StaticTile(front, tileSheet, BlendMode.Alpha, 5);

                    break;

            }

            if (Context.IsMultiplayer && Context.IsMainPlayer)
            {

                AccessQuery();

            }

            if (Utility.isOnScreen(start * 64, 64) && animate)
            {

                Mod.instance.spellRegister.Add(new(start * 64 + new Vector2(64, 0), 384, IconData.impacts.impact, new()) { sound = SpellHandle.sounds.boulderBreak, });

                location.playSound(SpellHandle.sounds.secret1.ToString());

            }

        }

        public virtual void AccessWarps()
        {

            int tilex = (int)start.X;

            int tiley = (int)start.Y;

            switch (type)
            {

                case types.stair:

                    location.warps.Add(new Warp(tilex, tiley, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

                    location.warps.Add(new Warp(tilex, tiley + 1, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

                    break;

                case types.door:

                    //location.warps.Add(new Warp(tilex, tiley, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

                    location.warps.Add(new Warp(tilex + 1, tiley, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

                    //location.warps.Add(new Warp(tilex + 2, tiley, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

                    break;

            }


        }

    }

}
