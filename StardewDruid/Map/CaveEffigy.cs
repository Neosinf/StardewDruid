using Microsoft.Xna.Framework;
using StardewValley.Menus;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Layers;
using xTile.Tiles;
using System.Runtime.CompilerServices;
using StardewValley.Locations;
using System.ComponentModel.Design;
using StardewModdingAPI;

namespace StardewDruid.Map
{
    public class CaveEffigy
    {

        private readonly Mod mod;

        private string activeDecoration;

        private int X;

        private int Y;

        private bool hideStatue;

        private bool makeSpace;

        public CaveEffigy(Mod Mod,int statueX, int statueY, bool HideStatue, bool MakeSpace)
        {

            mod = Mod;

            X = statueX; // 6

            Y = statueY; // 3

            hideStatue = HideStatue;

            makeSpace = MakeSpace;

        }

        public void ModifyCave()
        {

            if (hideStatue) { return; }

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            Layer backLayer = farmCave.map.GetLayer("Back");

            Layer buildingsLayer = farmCave.map.GetLayer("Buildings");

            //------------------------ rearrange existing tiles

            if (makeSpace) {


                backLayer.Tiles[6, 1] = buildingsLayer.Tiles[6, 1];

                backLayer.Tiles[5, 2] = buildingsLayer.Tiles[5, 2];

                backLayer.Tiles[6, 2] = buildingsLayer.Tiles[6, 3];

                backLayer.Tiles[7, 2] = buildingsLayer.Tiles[7, 2];

                buildingsLayer.Tiles[5, 3] = buildingsLayer.Tiles[3, 4];

                buildingsLayer.Tiles[7, 3] = buildingsLayer.Tiles[9, 4];


            }

            //------------------------ add effigy

            TileSheet witchSheet = farmCave.map.GetTileSheet("witchHutTiles");

            buildingsLayer.Tiles[X, Y - 2] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 108);

            buildingsLayer.Tiles[X - 1, Y - 1] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 115);

            StaticTile effigyBody = new(buildingsLayer, witchSheet, BlendMode.Alpha, 116);

            effigyBody.TileIndexProperties.Add("Action", "FarmCaveDruidEffigy");

            buildingsLayer.Tiles[X, Y - 1] = effigyBody;

            buildingsLayer.Tiles[X + 1, Y - 1] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 117);

            StaticTile effigyFeet = new(buildingsLayer, witchSheet, BlendMode.Alpha, 124);

            effigyFeet.TileIndexProperties.Add("Action", "FarmCaveDruidEffigy");

            buildingsLayer.Tiles[X, Y] = effigyFeet;

        }

        public void DecorateCave()
        {

            string blessing = mod.ActiveBlessing();

            if (blessing == activeDecoration)
            {

                return;

            }

            switch (blessing)
            {
                case "earth":

                    EarthDecoration();

                    break;

                case "water":

                    WaterDecoration();

                    break;

                case "stars":

                    StarsDecoration();

                    break;

                default: // "none"

                    NoDecoration();

                    break;

            }

            activeDecoration = blessing;

            return;

        }

        public void NoDecoration()
        {

            if (hideStatue) { return; }

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            Layer frontLayer = farmCave.map.GetLayer("Front");

            frontLayer.Tiles[X-1, Y-3] = null;

            frontLayer.Tiles[X-1, Y-2] = null;

            frontLayer.Tiles[X+1, Y-3] = null;

            frontLayer.Tiles[X+1, Y-2] = null;

            return;

        }

        public void EarthDecoration()
        {

            if (hideStatue) { return; }

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");

                frontLayer.Tiles[X - 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 372);

                frontLayer.Tiles[X - 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 380);

                frontLayer.Tiles[X + 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 372);

                frontLayer.Tiles[X + 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 380);
            }

            return;

        }

        public void WaterDecoration()
        {

            if (hideStatue) { return; }

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");

                frontLayer.Tiles[X - 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 373);

                frontLayer.Tiles[X - 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 381);

                frontLayer.Tiles[X + 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 373);

                frontLayer.Tiles[X + 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 381);
            }

            return;

        }

        public void StarsDecoration()
        {

            if (hideStatue) { return; }

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");

                frontLayer.Tiles[X - 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 374);

                frontLayer.Tiles[X - 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 382);

                frontLayer.Tiles[X + 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 374);

                frontLayer.Tiles[X + 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 382);

            }

            return;

        }


    }

}
