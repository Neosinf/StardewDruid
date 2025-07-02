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
using System.IO;
using StardewValley.Extensions;
using StardewDruid.Monster;
using StardewDruid.Location.Terrain;
using StardewDruid.Handle;

namespace StardewDruid.Location.Druid
{

    public class Grove : DruidLocation
    {

        public Vector2 anvilPosition = Vector2.Zero;

        public Vector2 circlePosition = Vector2.Zero;

        public Grove() { }

        public Grove(string name)
            : base(name)
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

            IsOutdoors = true;

            seasonalGround = true;

            ignoreOutdoorLighting.Set(false);

            largeTerrainFeatures.Set(new List<LargeTerrainFeature>());

            mapReset();

            Dictionary<int, List<List<int>>> codes = new();

            // -----------------------------------------------------
            // Tiles

            codes = new()
            {
                [0] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [1] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [2] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [3] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [4] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [5] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [6] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [7] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [8] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [16] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [17] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [18] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [19] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },

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

                            switch (Mod.instance.randomIndex.Next(4))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 160 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 180 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 2:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 212 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 3:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 232 + Mod.instance.randomIndex.Next(8));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;

                        case 3:

                            switch (Mod.instance.randomIndex.Next(4))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 168 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 188 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 2:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 204 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 3:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 224 + Mod.instance.randomIndex.Next(8));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;

                        case 4:

                            switch (Mod.instance.randomIndex.Next(4))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 176 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 196 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 2:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 200 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 3:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, 220 + Mod.instance.randomIndex.Next(4));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;

                        default:

                            back.Tiles[array[0], groundCode.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, array[1]);

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;
                    }
                }

            }

            // BORDER LAYER

            codes = new()
            {
                [0] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [1] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [2] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [3] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [4] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [5] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [6] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [7] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [8] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [16] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [17] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [18] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [19] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> groundCode in codes)
            {

                foreach (List<int> array in groundCode.Value)
                {

                    if (back.Tiles[array[0], groundCode.Key] != null)
                    {

                        buildings.Tiles[array[0], groundCode.Key] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, DruidLocation.barrierIndex);

                    }

                }

            }

            // FEATURES

            codes = new()
            {
                [-2] = new() { new() { 6, 4 }, new() { 17, 4 }, new() { 31, 4 }, },
                [-1] = new() { new() { 11, 2 }, new() { 23, 4 }, new() { 39, 4 }, },
                [0] = new() { new() { 2, 2 }, new() { 50, 4 }, },
                [1] = new() { },
                [2] = new() { new() { 44, 2 }, },
                [3] = new() { new() { -1, 4 }, new() { 32, 1 }, },
                [4] = new() { new() { 18, 13 }, },
                [5] = new() { new() { 13, 12 }, new() { 23, 14 }, new() { 51, 4 }, },
                [6] = new() { },
                [7] = new() { new() { 1, 4 }, },
                [8] = new() { },
                [9] = new() { },
                [10] = new() { new() { 10, 11 }, new() { 18, 99 }, new() { 26, 15 }, new() { 48, 2 }, },
                [11] = new() { new() { -1, 4 }, new() { 14, 99 }, new() { 25, 99 }, },
                [12] = new() { },
                [13] = new() { },
                [14] = new() { new() { 1, 2 }, new() { 36, 19 }, },
                [15] = new() { new() { 24, 16 }, },
                [16] = new() { new() { 11, 99 }, new() { 27, 99 }, new() { 38, 99 }, new() { 50, 4 }, },
                [17] = new() { new() { 11, 18 }, },
                [18] = new() { },
                [19] = new() { new() { -1, 4 }, },
                [20] = new() { new() { 12, 99 }, new() { 16, 17 }, new() { 18, 20 }, },
                [21] = new() { new() { 26, 99 }, },
                [22] = new() { new() { 49, 2 }, },
                [23] = new() { new() { 19, 99 }, },
                [24] = new() { new() { 0, 4 }, },
                [25] = new() { new() { 33, 2 }, },
                [26] = new() { new() { 6, 2 }, new() { 42, 2 }, },
                [27] = new() { new() { 17, 2 }, },
                [28] = new() { new() { 2, 4 }, new() { 24, 4 }, },
                [29] = new() { new() { 51, 4 }, },
                [30] = new() { new() { 12, 4 }, new() { 36, 4 }, },
                [31] = new() { new() { 44, 4 }, },

            };

            List<Vector2> reserved = new();

            lightFields = new();

            if (Mod.instance.iconData.sheetTextures.ContainsKey(IconData.tilesheets.grove))
            {

                foreach (KeyValuePair<int, List<List<int>>> code in codes)
                {

                    foreach (List<int> array in code.Value)
                    {

                        List<Vector2> baseTiles = new();

                        switch (array[1])
                        {
                            default:

                                Menhir menhir = new(new Vector2(array[0], code.Key) * 64, array[1]);

                                baseTiles = menhir.baseTiles;

                                terrainFields.Add(menhir);

                                LightField light = new(menhir.bounds.Center.ToVector2())
                                {
                                    colour = Microsoft.Xna.Framework.Color.LightBlue,

                                    luminosity = 4,

                                    lightTimer = 1900
                                };

                                lightFields.Add(light);

                                break;

                            case 1:

                                Magnolia magnolia = new(new Vector2(array[0], code.Key) * 64);

                                magnolia.AddLitter();

                                baseTiles = magnolia.baseTiles;

                                reserved.AddRange(baseTiles);

                                terrainFields.Add(magnolia);

                                break;

                            case 2:

                                Magnolia smallmagnolia = new(new Vector2(array[0], code.Key) * 64, 2);

                                smallmagnolia.AddLitter();

                                baseTiles = smallmagnolia.baseTiles;

                                reserved.AddRange(baseTiles);

                                terrainFields.Add(smallmagnolia);

                                break;

                            case 3:

                                DarkOak darkoak = new(new Vector2(array[0], code.Key) * 64);

                                darkoak.AddLitter();

                                baseTiles = darkoak.baseTiles;

                                reserved.AddRange(baseTiles);

                                terrainFields.Add(darkoak);

                                break;

                            case 4:

                                DarkOak smalldarkoak = new(new Vector2(array[0], code.Key) * 64, 2);

                                smalldarkoak.AddLitter();

                                baseTiles = smalldarkoak.baseTiles;

                                reserved.AddRange(baseTiles);

                                terrainFields.Add(smalldarkoak);

                                break;

                            case 20:

                                GroveBowl bowl = new(new Vector2(array[0], code.Key) * 64, array[1])
                                {
                                    disabled = true
                                };

                                terrainFields.Add(bowl);

                                break;

                            case 99:

                                switch (Mod.instance.randomIndex.Next(2))
                                {
                                    case 0:

                                        Flower weed = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.weed);

                                        grassFields.Add(weed);

                                        break;

                                    case 1:

                                        Flower weedtwo = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.weedtwo);

                                        grassFields.Add(weedtwo);

                                        break;

                                }

                                break;

                        }

                        foreach (Vector2 bottom in baseTiles)
                        {

                            if(back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                            {

                                buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                            }

                        }

                    }

                }

            }


            // FLOWER CODES

            codes = new()
            {
                [0] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [1] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [2] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [3] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [4] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [5] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [6] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [7] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [8] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [16] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [17] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [18] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [19] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [32] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    if (reserved.Contains(new Vector2(array[0], code.Key)))
                    {
                        continue;
                    }

                    switch (Mod.instance.randomIndex.Next(9))
                    {
                        case 0:

                            Flower flower = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.flower);

                            grassFields.Add(flower);

                            break;
                        case 1:

                            Flower flowertwo = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.flowertwo);

                            grassFields.Add(flowertwo);

                            break;
                        case 2: case 3: case 4:

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

            // CIRCLE

            if (Mod.instance.Config.decorateGrove)
            {

                RitualCircle circle = new(new Vector2(16, 12)*64);

                switch (Game1.season)
                {

                    case Season.Spring:

                        circle.fadeout = 0.6f;

                        break;

                    case Season.Summer:

                        circle.fadeout = 0.75f;

                        break;

                    case Season.Fall:

                        circle.fadeout = 0.5f;

                        break;

                    case Season.Winter:

                        circle.color = Microsoft.Xna.Framework.Color.LightBlue;

                        circle.fadeout = 0.75f;

                        break;
                
                }

                circle.ritualStatus = 2;

                circle.LoadCandles();

                circlePosition = circle.bounds.Center.ToVector2();

                terrainFields.Add(circle);

            }

            addDialogue();

            this.map = newMap;

        }

        public override void UpdateMapSeats()
        {

            base.UpdateMapSeats();

            MapSeat seat = new();

            seat.tilePosition.Set(new Vector2(36, 15));

            seat.size.Set(new Vector2(4, 1));

            seat.direction.Set(2);

            seat.drawTilePosition.Set(new Vector2(-1));

            seat.seatType.Set("GroveBench");

            mapSeats.Add(seat);

        }

        public override bool readyDialogue()
        {

            return Mod.instance.questHandle.IsComplete(QuestHandle.swordWeald);

        }

        public override void addDialogue()
        {

            if (dialogueTiles.Count > 0) { return; }

            dialogueTiles.Add(new(18, 9), CharacterHandle.characters.energies);

            dialogueTiles.Add(new(19, 9), CharacterHandle.characters.energies);

            dialogueTiles.Add(new(20, 9), CharacterHandle.characters.energies);

            dialogueTiles.Add(new(17, 20), CharacterHandle.characters.herbalism);

            dialogueTiles.Add(new(18, 20), CharacterHandle.characters.herbalism);

            dialogueTiles.Add(new(19, 20), CharacterHandle.characters.herbalism);

            dialogueTiles.Add(new(20, 20), CharacterHandle.characters.herbalism);

            dialogueTiles.Add(new(17, 21), CharacterHandle.characters.herbalism);

            dialogueTiles.Add(new(18, 21), CharacterHandle.characters.herbalism);

            dialogueTiles.Add(new(19, 21), CharacterHandle.characters.herbalism);

            dialogueTiles.Add(new(20, 21), CharacterHandle.characters.herbalism);

            dialogueTiles.Add(new(12, 17), CharacterHandle.characters.anvil);

            dialogueTiles.Add(new(13, 17), CharacterHandle.characters.anvil);

            dialogueTiles.Add(new(12, 18), CharacterHandle.characters.anvil);

            dialogueTiles.Add(new(13, 18), CharacterHandle.characters.anvil);

            anvilPosition = new Vector2(12f, 17f) * 64;

        }

        public void ToggleBowl(bool disabled = false)
        {

            foreach (TerrainField terrain in terrainFields)
            { 
            
                if(terrain is GroveBowl bowl)
                {

                    bowl.disabled = disabled;

                }

            }

        }

        public override void updateWarps()
        {

            warps.Clear();

            warps.Add(new Warp(29, 1, LocationHandle.druid_sanctuary_name, 26, 26, flipFarmer: false));

            warps.Add(new Warp(30, 1, LocationHandle.druid_sanctuary_name, 26, 26, flipFarmer: false));

            warps.Add(new Warp(29, 29, LocationHandle.druid_cavern_name, 8, 8, flipFarmer: false));

            warps.Add(new Warp(30, 29, LocationHandle.druid_cavern_name, 8, 8, flipFarmer: false));

        }

    }

}
