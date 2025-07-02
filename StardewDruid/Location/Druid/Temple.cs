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

    public class Temple : DruidLocation
    {

        public Vector2 brazierTile;

        public Temple() { }

        public Temple(string Name)
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

            Layer front = new("Front", newMap, new(54, 32), tileSize);

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
                [8] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [16] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [17] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [18] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [19] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
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

                            back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, Mod.instance.randomIndex.Next(80));

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
                [2] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [3] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [4] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [5] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [6] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [7] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [8] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [16] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [17] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [18] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [19] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
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

                [8] = new() { new() { 7, 1 }, new() { 11, 2 }, new() { 15, 2 }, new() { 19, 2 }, new() { 23, 2 }, new() { 27, 2 }, new() { 31, 2 }, new() { 35, 2 }, new() { 39, 2 }, new() { 43, 21 }, },
                [9] = new() { },
                [10] = new() { },
                [11] = new() { },
                [12] = new() { new() { 7, 3 }, new() { 11, 4 }, new() { 15, 4 }, new() { 19, 4 }, new() { 23, 4 }, new() { 27, 4 }, new() { 31, 4 }, new() { 35, 4 }, new() { 39, 4 }, new() { 43, 23 }, },
                [13] = new() { },
                [14] = new() { },
                [15] = new() { },
                [16] = new() { new() { 7, 3 }, new() { 11, 4 }, new() { 15, 4 }, new() { 19, 4 }, new() { 23, 4 }, new() { 27, 4 }, new() { 31, 4 }, new() { 35, 4 }, new() { 39, 4 }, new() { 43, 23 }, },
                [17] = new() { },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { new() { 7, 3 }, new() { 11, 4 }, new() { 15, 4 }, new() { 19, 4 }, new() { 23, 4 }, new() { 27, 4 }, new() { 31, 4 }, new() { 35, 4 }, new() { 39, 4 }, new() { 43, 23 }, },
                [21] = new() { },
                [22] = new() { },
                [23] = new() { },
                [24] = new() { new() { 7, 5 }, new() { 11, 6 }, new() { 15, 6 }, new() { 19, 6 }, new() { 23, 6 }, new() { 27, 6 }, new() { 31, 6 }, new() { 35, 6 }, new() { 39, 6 }, new() { 43, 25 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {
                    int useIndex = array[1];

                    if (array[1] > 20)
                    {

                        useIndex = array[1] - 20;

                    }

                    TemplePave pavement = new(new Vector2(array[0], code.Key) * 64, useIndex);

                    floorFields.Add(pavement);

                    if (array[1] > 20)
                    {

                        pavement.flip = true;

                    }

                }

            }

            // ------------------------------------------------------
            // columns

            codes = new()
            {
                [4] = new() { new() { 10, 1 }, new() { 14, 1 }, new() { 18, 1 }, new() { 22, 1 }, new() { 26, 1 }, new() { 30, 1 }, new() { 34, 1 }, new() { 38, 1 }, new() { 42, 1 }, },
                [5] = new() { },
                [6] = new() { },
                [7] = new() { new() { 10, 1 }, new() { 38, 1 }, new() { 42, 1 }, },
                [8] = new() { },
                [9] = new() { },
                [10] = new() { new() { 10, 1 }, new() { 38, 3 }, new() { 42, 1 }, },
                [11] = new() { },
                [12] = new() { },
                [13] = new() { new() { 10, 1 }, new() { 38, 4 }, new() { 42, 1 }, },
                [14] = new() { },
                [15] = new() { },
                [16] = new() { new() { 10, 2 }, new() { 14, 3 }, new() { 18, 4 }, new() { 22, 2 }, new() { 26, 3 }, new() { 30, 1 }, new() { 34, 1 }, new() { 38, 1 }, new() { 42, 1 }, },
                [17] = new() { },
                [18] = new() { },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    int useIndex = array[1];

                    if (array[1] > 20)
                    {

                        useIndex = array[1] - 20;

                    }

                    TempleColumn column = new(new Vector2(array[0], code.Key) * 64, useIndex);

                    terrainFields.Add(column);

                    if (array[1] > 20)
                    {

                        column.flip = true;

                    }

                    foreach (Vector2 bottom in column.baseTiles)
                    {

                        buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, DruidLocation.barrierIndex);

                    }

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

                        case 11:

                            TempleBrazier brazier = new(new Vector2(array[0], code.Key) * 64);

                            baseTiles = brazier.baseTiles;

                            terrainFields.Add(brazier);

                            brazierTile = new Vector2(array[0], code.Key);

                            break;

                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:

                            int useIndex = array[1] - 20;

                            if (array[1] > 30)
                            {

                                useIndex = array[1] - 30;

                            }

                            TempleBeam beam = new(new Vector2(array[0], code.Key) * 64, useIndex);

                            terrainFields.Add(beam);

                            if (array[1] > 30)
                            {

                                beam.flip = true;

                            }

                    
                        break;

                    }

                    foreach (Vector2 bottom in baseTiles)
                    {

                        if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                        {

                            buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, DruidLocation.barrierIndex);

                        }

                    }

                }

            }

            // FLOWER CODES

            codes = new()
            {
                [0] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [1] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [2] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [3] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [4] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [5] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [6] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [7] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [8] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [16] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, },
                [17] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, },
                [18] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, },
                [19] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },

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

            dialogueTiles.Add(new(brazierTile.X, brazierTile.Y + 4), CharacterHandle.characters.crow_brazier);

            dialogueTiles.Add(new(brazierTile.X + 1, brazierTile.Y + 4), CharacterHandle.characters.crow_brazier);

            dialogueTiles.Add(new(brazierTile.X + 2, brazierTile.Y + 4), CharacterHandle.characters.crow_brazier);

            dialogueTiles.Add(new(brazierTile.X + 3, brazierTile.Y + 4), CharacterHandle.characters.crow_brazier);

            dialogueTiles.Add(new(brazierTile.X, brazierTile.Y + 5), CharacterHandle.characters.crow_brazier);

            dialogueTiles.Add(new(brazierTile.X + 1, brazierTile.Y + 5), CharacterHandle.characters.crow_brazier);

            dialogueTiles.Add(new(brazierTile.X + 2, brazierTile.Y + 5), CharacterHandle.characters.crow_brazier);

            dialogueTiles.Add(new(brazierTile.X + 3, brazierTile.Y + 5), CharacterHandle.characters.crow_brazier);


        }

        public override void updateWarps()
        {

            warps.Clear();

            warps.Add(new Warp(49, 16, LocationHandle.druid_sanctuary_name, 6, 15, flipFarmer: false));

            warps.Add(new Warp(49, 17, LocationHandle.druid_sanctuary_name, 6, 15, flipFarmer: false));

            warps.Add(new Warp(49, 18, LocationHandle.druid_sanctuary_name, 6, 15, flipFarmer: false));

        }

        public void BrazierOverride(int status)
        {

            foreach (TerrainField field in terrainFields)
            {

                if (field is TempleBrazier bonebrazier)
                {

                    bonebrazier.brazierStatus = status;

                }

            }

        }


    }

}
