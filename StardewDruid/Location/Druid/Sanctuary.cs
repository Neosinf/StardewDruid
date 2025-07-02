using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Layers;
using xTile.Tiles;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley.Buildings;
using System.Drawing;
using StardewValley.Objects;
using System.Runtime.Intrinsics.X86;
using StardewValley.GameData.Locations;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.TerrainFeatures;
using System.Threading;
using System.Xml.Serialization;
using StardewDruid.Data;
using xTile.Dimensions;
using StardewDruid.Event;
using StardewDruid.Location.Terrain;
using StardewDruid.Handle;

namespace StardewDruid.Location.Druid
{

    public class Sanctuary : DruidLocation
    {

        public bool gateOpen;

        public Sanctuary() { }

        public Sanctuary(string Name)
            : base(Name)
        {

        }

        public override void OnMapLoad(xTile.Map map)
        {


            xTile.Dimensions.Size tileSize = map.GetLayer("Back").TileSize;

            xTile.Map newMap = new(map.Id);

            Layer back = new("Back", newMap, new(54, 32), tileSize);

            newMap.AddLayer(back);

            Layer buildings = new("Buildings", newMap, new(54, 32), tileSize);

            newMap.AddLayer(buildings);

            Layer front = new("Front", newMap, new(54,32), tileSize);

            newMap.AddLayer(front);

            Layer alwaysfront = new("AlwaysFront", newMap, new(54, 32), tileSize);

            newMap.AddLayer(alwaysfront);

            TileSheet groundsheet = new(
                newMap,
                "StardewDruid.Tilesheets.groundspring",
                new(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.groundspring].Width / 16,
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.groundspring].Height / 16
                ),
                new(16, 16)
            );

            newMap.AddTileSheet(groundsheet);

            seasonalGround = true;

            IsOutdoors = true;

            ignoreOutdoorLighting.Set(false);

            mapReset();

            // -----------------------------------------------------
            // Tiles

            Dictionary<int, List<List<int>>> codes = new()
            {
                [0] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [1] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [2] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [3] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [4] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [5] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [6] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [7] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [8] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 3 }, new() { 8, 3 }, new() { 9, 3 }, new() { 10, 3 }, new() { 11, 3 }, new() { 12, 3 }, new() { 13, 3 }, new() { 14, 3 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, new() { 41, 3 }, new() { 42, 3 }, new() { 43, 3 }, new() { 44, 3 }, new() { 45, 3 }, new() { 46, 3 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 3 }, new() { 8, 3 }, new() { 9, 3 }, new() { 10, 3 }, new() { 11, 3 }, new() { 12, 3 }, new() { 13, 3 }, new() { 14, 3 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, new() { 41, 3 }, new() { 42, 3 }, new() { 43, 3 }, new() { 44, 3 }, new() { 45, 3 }, new() { 46, 3 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [16] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [17] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [18] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [19] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 3 }, new() { 8, 3 }, new() { 9, 3 }, new() { 10, 3 }, new() { 11, 3 }, new() { 12, 3 }, new() { 13, 3 }, new() { 14, 3 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, new() { 41, 3 }, new() { 42, 3 }, new() { 43, 3 }, new() { 44, 3 }, new() { 45, 3 }, new() { 46, 3 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 3 }, new() { 7, 3 }, new() { 8, 3 }, new() { 9, 3 }, new() { 10, 3 }, new() { 11, 3 }, new() { 12, 3 }, new() { 13, 3 }, new() { 14, 3 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 3 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, new() { 41, 3 }, new() { 42, 3 }, new() { 43, 3 }, new() { 44, 3 }, new() { 45, 3 }, new() { 46, 3 }, new() { 47, 3 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },


            };

            foreach (KeyValuePair<int, List<List<int>>> groundCode in codes)
            {

                foreach (List<int> array in groundCode.Value)
                {

                    switch (array[1])
                    {
                        case 1:

                            back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, Mod.instance.randomIndex.Next(80));

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Grass");

                            break;

                        case 2:

                            switch (Mod.instance.randomIndex.Next(2))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 252 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 272 + Mod.instance.randomIndex.Next(4));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;

                        case 3:

                            switch (Mod.instance.randomIndex.Next(2))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 176 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 196 + Mod.instance.randomIndex.Next(4));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;

                    }
                }

            }

            // BORDER LAYER

            codes = new()
            {
                [0] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [1] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [2] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [3] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [4] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [5] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [6] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [7] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [8] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [16] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [17] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [18] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [19] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> groundCode in codes)
            {

                foreach (List<int> array in groundCode.Value)
                {

                    buildings.Tiles[array[0], groundCode.Key] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, DruidLocation.barrierIndex);

                }

            }

            // ------------------------------------------------------
            // paving

            codes = new()
            {
                [3] = new() { new() { 25, 4 }, },
                [4] = new() { },
                [5] = new() { },
                [6] = new() { },
                [7] = new() { },
                [8] = new() { },
                [9] = new() { new() { 6, 1 }, new() { 36, 1 }, },
                [10] = new() { },
                [11] = new() { },
                [12] = new() { },
                [13] = new() { new() { 20, 2 }, },
                [14] = new() { },
                [15] = new() { },
                [16] = new() { new() { 9, 3 }, new() { 39, 3 }, },
                [17] = new() { },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { },
                [21] = new() { new() { 6, 1 }, new() { 36, 1 }, },
                [22] = new() { },


            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    SanctuaryPave pavement = new(new Vector2(array[0], code.Key) * 64, array[1]);

                    floorFields.Add(pavement);

                }

            }

            // ------------------------------------------------------
            // walls

            codes = new()
            {
                [5] = new() { new() { 3, 1 }, new() { 4, 11 }, new() { 8, 2 }, new() { 9, 11 }, new() { 13, 2 }, new() { 14, 11 }, new() { 18, 2 }, new() { 19, 12 }, new() { 21, 3 }, new() { 32, 1 }, new() { 33, 12 }, new() { 35, 2 }, new() { 36, 11 }, new() { 40, 2 }, new() { 41, 11 }, new() { 45, 2 }, new() { 46, 11 }, new() { 50, 3 }, },
                [6] = new() { new() { 3, 13 }, new() { 50, 13 }, },
                [7] = new() { },
                [8] = new() { },
                [9] = new() { new() { 3, 5 }, new() { 50, 5 }, },
                [10] = new() { new() { 3, 13 }, new() { 50, 13 }, },
                [11] = new() { },
                [12] = new() { },
                [13] = new() { new() { 3, 4 }, new() { 50, 5 }, },
                [14] = new() { new() { 50, 13 }, },
                [15] = new() { },
                [16] = new() { },
                [17] = new() { new() { 50, 13 }, },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { new() { 3, 4 }, new() { 50, 5 }, },
                [21] = new() { new() { 3, 13 }, new() { 50, 13 }, },
                [22] = new() { },
                [23] = new() { },
                [24] = new() { new() { 3, 5 }, new() { 50, 5 }, },
                [25] = new() { new() { 3, 13 }, new() { 50, 13 }, },
                [26] = new() { },
                [27] = new() { },
                [28] = new() { new() { 3, 1 }, new() { 4, 11 }, new() { 8, 2 }, new() { 9, 11 }, new() { 13, 2 }, new() { 14, 11 }, new() { 18, 2 }, new() { 19, 11 }, new() { 23, 3 }, new() { 30, 1 }, new() { 31, 11 }, new() { 35, 2 }, new() { 36, 11 }, new() { 40, 2 }, new() { 41, 11 }, new() { 45, 2 }, new() { 46, 11 }, new() { 50, 3 }, },
                [29] = new() { },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    SanctuaryWall wall = new(new Vector2(array[0], code.Key) * 64, array[1]);

                    terrainFields.Add(wall);

                }

            }


            // ------------------------------------------------------
            // trees and miscellaneous
            codes = new()
            {
                [-3] = new() { new() { 1, 2 }, new() { 29, 3 }, },
                [-2] = new() { },
                [-1] = new() { },
                [0] = new() { },
                [1] = new() { },
                [2] = new() { new() { 10, 21 }, new() { 15, 22 }, new() { 19, 22 }, new() { 23, 22 }, new() { 27, 23 }, new() { 31, 24 }, new() { 35, 22 }, new() { 39, 31 }, new() { 47, 2 }, },
                [3] = new() { new() { 10, 29 }, new() { 42, 29 }, },
                [4] = new() { },
                [5] = new() { },
                [6] = new() { new() { 10, 30 }, new() { 42, 29 }, },
                [7] = new() { },
                [8] = new() { },
                [9] = new() { new() { 42, 29 }, },
                [10] = new() { },
                [11] = new() { },
                [12] = new() { new() { 42, 29 }, },
                [13] = new() { },
                [14] = new() { new() { 35, 37 }, new() { 39, 35 }, },
                [15] = new() { },
                [16] = new() { new() { -3, 2 }, },
                [17] = new() { },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { },
                [21] = new() { },
                [22] = new() { },
                [23] = new() { },
                [24] = new() { },
                [25] = new() { new() { 41, 2 }, },
                [26] = new() { },
                [27] = new() { new() { 13, 3 }, },
                [28] = new() { },
                [29] = new() { },
                [30] = new() { },
                [31] = new() { },

            };

            List<Vector2> reserved = new();

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    List<Vector2> baseTiles = new();

                    switch (array[1])
                    {

                        case 1:
                        case 5:

                            Magnolia magnolia = new(new Vector2(array[0], code.Key) * 64, array[1] > 4 ? 2 : 1);

                            //magnolia.ruined = true;

                            magnolia.AddLitter();

                            baseTiles = magnolia.baseTiles;

                            terrainFields.Add(magnolia);

                            foreach (Vector2 bottom in baseTiles)
                            {

                                reserved.Add(bottom);

                            }

                            break;

                        case 2:
                        case 6:

                            DarkOak darkoak = new(new Vector2(array[0], code.Key) * 64, array[1] > 4 ? 2 : 1)
                            {
                                flip = (Mod.instance.randomIndex.Next(2) == 0)
                            };

                            darkoak.AddLitter();

                            baseTiles = darkoak.baseTiles;

                            terrainFields.Add(darkoak);

                            foreach (Vector2 bottom in baseTiles)
                            {

                                reserved.Add(bottom);

                            }

                            break;

                        case 3:
                        case 7:

                            Hawthorn hawthorn = new(new Vector2(array[0], code.Key) * 64, array[1] > 4 ? 2 : 1);

                            //hawthorn.ruined = true;

                            hawthorn.AddLitter();

                            baseTiles = hawthorn.baseTiles;

                            terrainFields.Add(hawthorn);

                            foreach (Vector2 bottom in baseTiles)
                            {

                                reserved.Add(bottom);

                            }

                            break;

                        case 4:
                        case 8:

                            Holly holly = new(new Vector2(array[0], code.Key) * 64, array[1] > 4 ? 2 : 1);

                            //holly.ruined = true;

                            holly.AddLitter();

                            baseTiles = holly.baseTiles;

                            terrainFields.Add(holly);

                            foreach (Vector2 bottom in baseTiles)
                            {

                                reserved.Add(bottom);

                            }

                            break;

                        case 10:

                            SanctuaryGate gate = new(new Vector2(array[0], code.Key) * 64);

                            baseTiles = gate.baseTiles;

                            terrainFields.Add(gate);

                            break;

                        case 11:

                            SanctuaryPergola pergola = new(new Vector2(array[0], code.Key) * 64);

                            baseTiles = pergola.baseTiles;

                            terrainFields.Add(pergola);

                            break;

                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:

                            SanctuaryStatue statue = new(new Vector2(array[0], code.Key) * 64, array[1] -20);

                            baseTiles = statue.baseTiles;

                            terrainFields.Add(statue);

                            break;

                    }

                    foreach (Vector2 bottom in baseTiles)
                    {

                        if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                        {

                            buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, 360);

                        }

                    }

                }

            }

            // FLOWER CODES

            codes = new()
            {
                [0] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [1] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [2] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [3] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [4] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [5] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [6] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [7] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [8] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [16] = new() { new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [17] = new() { new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [18] = new() { new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [19] = new() { new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [32] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    if (reserved.Contains(new Vector2(array[0], code.Key)))
                    {
                        continue;
                    }

                    switch (Mod.instance.randomIndex.Next(12))
                    {
                        case 0:

                            Flower flower = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.flower);

                            grassFields.Add(flower);

                            break;

                        case 1:

                            Flower flowertwo = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.flowertwo);

                            grassFields.Add(flowertwo);

                            break;

                        case 2:
                        case 3:
                        case 4:

                            Flower flowergrass = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.grasstwo);

                            grassFields.Add(flowergrass);

                            Flower offsetgrass = new(new Vector2(array[0] - 0.5f, code.Key - 0.5f) * 64, Flower.grasstypes.grass);

                            grassFields.Add(offsetgrass);

                            break;

                        default:

                            Flower grass = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.grass);

                            grassFields.Add(grass);

                            break;

                    }

                }

            }

            addDialogue();

            this.map = newMap;

        }

        public override void addDialogue()
        {

            if (dialogueTiles.Count > 0) { return; }

            dialogueTiles.Add(new(26, 6), CharacterHandle.characters.crow_gate);

            dialogueTiles.Add(new(27, 6), CharacterHandle.characters.crow_gate);

        }

        public override void updateWarps()
        {

            warps.Clear();

            // --------------------------------

            warps.Add(new Warp(25, 28, LocationHandle.druid_grove_name, 29, 5, flipFarmer: false));

            warps.Add(new Warp(26, 28, LocationHandle.druid_grove_name, 29, 5, flipFarmer: false));

            warps.Add(new Warp(27, 28, LocationHandle.druid_grove_name, 29, 5, flipFarmer: false));

            warps.Add(new Warp(28, 28, LocationHandle.druid_grove_name, 29, 5, flipFarmer: false));

            // --------------------------------

            warps.Add(new Warp(1, 15, LocationHandle.druid_temple_name, 47, 16, flipFarmer: false));

            warps.Add(new Warp(1, 16, LocationHandle.druid_temple_name, 47, 16, flipFarmer: false));

            warps.Add(new Warp(1, 17, LocationHandle.druid_temple_name, 47, 16, flipFarmer: false));

            warps.Add(new Warp(1, 18, LocationHandle.druid_temple_name, 47, 16, flipFarmer: false));

            // --------------------------------

            if (gateOpen)
            {

                OpenGate();

            }

        }

        public override void UpdateMapSeats()
        {

            base.UpdateMapSeats();

            int i = 0;

            foreach(TerrainField field in terrainFields)
            {

                if(field is SanctuaryStatue statue)
                {

                    if(statue.index == 6)
                    {

                        MapSeat seat = new();

                        i++;

                        Vector2 statueTile = ModUtility.PositionToTile(statue.position + new Vector2(0,64));

                        seat.tilePosition.Set(statueTile);

                        seat.size.Set(new Vector2(4, 1));

                        seat.direction.Set(2);

                        seat.drawTilePosition.Set(new Vector2(-1));

                        seat.seatType.Set("SanctuaryBench"+i);

                        mapSeats.Add(seat);

                    }

                }

            }

        }

        public void GateOverride(int status)
        {

            foreach (TerrainField field in terrainFields)
            {

                if (field is SanctuaryGate crowgate)
                {

                    crowgate.doorStatus = status;

                }

            }

        }

        public void OpenGate()
        {

            gateOpen = true;

            Layer buildings = map.GetLayer("Buildings");

            buildings.Tiles[26, 5] = null;

            buildings.Tiles[27, 5] = null;

            buildings.Tiles[26, 6] = null;

            buildings.Tiles[27, 6] = null;

            warps.Add(new Warp(26, 5, LocationHandle.druid_moors_name, 26, 69, flipFarmer: false));

            warps.Add(new Warp(27, 5, LocationHandle.druid_moors_name, 27, 69, flipFarmer: false));

            foreach(TerrainField field in terrainFields)
            {

                if(field is SanctuaryGate crowgate)
                {

                    crowgate.opened = true;

                }

            }

            return;

        }

    }

}
