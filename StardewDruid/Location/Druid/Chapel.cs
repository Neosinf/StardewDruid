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
using StardewValley.Objects;
using System.Runtime.Intrinsics.X86;
using StardewValley.GameData.Locations;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley.TerrainFeatures;
using System.Threading;
using xTile;
using StardewDruid.Character;

namespace StardewDruid.Location.Druid
{
    public class Chapel : DruidLocation
    {

        public List<Location.TerrainField> altarTiles = new();

        public List<Location.TerrainField> circleTiles = new();

        public bool activeCircle;


        public Chapel() { }

        public Chapel(string Name)
            : base(Name)
        {

        }

        public override void draw(SpriteBatch b)
        {

            base.draw(b);

            if (activeCircle)
            {

                foreach (TerrainField tile in circleTiles)
                {

                    tile.draw(b, this);

                }

            }
            else
            {

                foreach (TerrainField tile in altarTiles)
                {

                    tile.draw(b, this);

                }

            }


        }

        public override void drawBackground(SpriteBatch b)
        {

            base.drawBackground(b);

            DrawParallaxHorizon(b);

        }

        public virtual void DrawParallaxHorizon(SpriteBatch b)
        {

            float offset = Game1.viewport.Y == 0 ? 0f : Game1.viewport.Y * 192f / map.DisplayHeight;

            Vector2 localPosition = Game1.GlobalToLocal(Game1.viewport, new Vector2(27, 7) * 64 + new Vector2(0, offset));

            Color rain = Game1.isRaining ? Color.LightSkyBlue : Color.White;

            int currentTime = (int)(Game1.gameTimeInterval / (float)Game1.realMilliSecondsPerGameMinute % 10f);

            float pastDarkTime = Utility.ConvertTimeToMinutes(Game1.timeOfDay + currentTime - Game1.getStartingToGetDarkTime(this));

            float gettingDarkerTime = Utility.ConvertTimeToMinutes(Game1.getTrulyDarkTime(this) - Game1.getStartingToGetDarkTime(this));

            float value = pastDarkTime / gettingDarkerTime;

            b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.skyblue], localPosition, new Rectangle(0, 0, 864, 432), rain, 0f, new Vector2(432, 216), 2f, SpriteEffects.None, 0f);

            if (value > 0f)
            {

                b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.skynight], localPosition, new Rectangle(0, 0, 864, 432), Color.White * value, 0f, new Vector2(432, 216), 2f, SpriteEffects.None, 0f);

            }

        }

        public override void OnMapLoad(Map map)
        {

            xTile.Dimensions.Size tileSize = map.GetLayer("Back").TileSize;

            Map newMap = new(map.Id);

            Layer back = new("Back", newMap, new(54, 36), tileSize);

            newMap.AddLayer(back);

            Layer back2 = new("Back2", newMap, new(54, 36), tileSize);

            newMap.AddLayer(back2);

            Layer buildings = new("Buildings", newMap, new(54, 36), tileSize);

            newMap.AddLayer(buildings);

            Layer front = new("Front", newMap, new(54, 36), tileSize);

            newMap.AddLayer(front);

            Layer alwaysfront = new("AlwaysFront", newMap, new(54, 36), tileSize);

            newMap.AddLayer(alwaysfront);

            TileSheet chapelsheet = new(
                newMap,
                "StardewDruid.Tilesheets.chapel",
                new(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.chapel].Width / 16,
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.chapel].Height / 16
                ),
                new(16, 16)
            );

            newMap.AddTileSheet(chapelsheet);

            newMap.LoadTileSheets(Game1.mapDisplayDevice);

            IsOutdoors = false;

            ignoreOutdoorLighting.Set(false);

            terrainFields = new();

            Dictionary<int, List<List<int>>> codes = new()
            {
                [0] = new() { },
                [1] = new() { },
                [2] = new() { },
                [3] = new() { },
                [4] = new() { },
                [5] = new() { },
                [6] = new() { },
                [7] = new() { },
                [8] = new() { },
                [9] = new() { },
                [10] = new() { },
                [11] = new() { },
                [12] = new() { },
                [13] = new() { new() { 15, 3 }, new() { 16, 3 }, new() { 17, 3 }, new() { 18, 3 }, new() { 19, 17 }, new() { 20, 18 }, new() { 21, 17 }, new() { 22, 18 }, new() { 23, 17 }, new() { 24, 18 }, new() { 25, 17 }, new() { 26, 18 }, new() { 27, 17 }, new() { 28, 18 }, new() { 29, 17 }, new() { 30, 18 }, new() { 31, 17 }, new() { 32, 18 }, new() { 33, 17 }, new() { 34, 18 }, new() { 35, 3 }, new() { 36, 3 }, new() { 37, 3 }, new() { 38, 3 }, },
                [14] = new() { new() { 15, 3 }, new() { 16, 3 }, new() { 17, 3 }, new() { 18, 3 }, new() { 19, 31 }, new() { 20, 32 }, new() { 21, 31 }, new() { 22, 32 }, new() { 23, 31 }, new() { 24, 32 }, new() { 25, 31 }, new() { 26, 32 }, new() { 27, 31 }, new() { 28, 32 }, new() { 29, 31 }, new() { 30, 32 }, new() { 31, 31 }, new() { 32, 32 }, new() { 33, 31 }, new() { 34, 32 }, new() { 35, 3 }, new() { 36, 3 }, new() { 37, 3 }, new() { 38, 3 }, },
                [15] = new() { new() { 15, 3 }, new() { 16, 3 }, new() { 17, 17 }, new() { 18, 18 }, new() { 19, 17 }, new() { 20, 18 }, new() { 21, 45 }, new() { 22, 46 }, new() { 23, 45 }, new() { 24, 46 }, new() { 25, 73 }, new() { 26, 74 }, new() { 27, 73 }, new() { 28, 74 }, new() { 29, 45 }, new() { 30, 46 }, new() { 31, 45 }, new() { 32, 46 }, new() { 33, 17 }, new() { 34, 18 }, new() { 35, 17 }, new() { 36, 18 }, new() { 37, 3 }, new() { 38, 3 }, },
                [16] = new() { new() { 15, 3 }, new() { 16, 3 }, new() { 17, 31 }, new() { 18, 32 }, new() { 19, 31 }, new() { 20, 32 }, new() { 21, 59 }, new() { 22, 60 }, new() { 23, 59 }, new() { 24, 60 }, new() { 25, 87 }, new() { 26, 88 }, new() { 27, 87 }, new() { 28, 88 }, new() { 29, 59 }, new() { 30, 60 }, new() { 31, 59 }, new() { 32, 60 }, new() { 33, 31 }, new() { 34, 32 }, new() { 35, 31 }, new() { 36, 32 }, new() { 37, 3 }, new() { 38, 3 }, },
                [17] = new() { new() { 15, 3 }, new() { 16, 3 }, new() { 17, 17 }, new() { 18, 18 }, new() { 19, 45 }, new() { 20, 46 }, new() { 21, 45 }, new() { 22, 46 }, new() { 23, 45 }, new() { 24, 46 }, new() { 25, 45 }, new() { 26, 46 }, new() { 27, 45 }, new() { 28, 46 }, new() { 29, 45 }, new() { 30, 46 }, new() { 31, 45 }, new() { 32, 46 }, new() { 33, 45 }, new() { 34, 46 }, new() { 35, 17 }, new() { 36, 18 }, new() { 37, 3 }, new() { 38, 3 }, },
                [18] = new() { new() { 15, 3 }, new() { 16, 3 }, new() { 17, 31 }, new() { 18, 32 }, new() { 19, 59 }, new() { 20, 60 }, new() { 21, 59 }, new() { 22, 60 }, new() { 23, 59 }, new() { 24, 60 }, new() { 25, 59 }, new() { 26, 60 }, new() { 27, 59 }, new() { 28, 60 }, new() { 29, 59 }, new() { 30, 60 }, new() { 31, 59 }, new() { 32, 60 }, new() { 33, 59 }, new() { 34, 60 }, new() { 35, 31 }, new() { 36, 32 }, new() { 37, 3 }, new() { 38, 3 }, },
                [19] = new() { new() { 13, 3 }, new() { 14, 3 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 17 }, new() { 18, 18 }, new() { 19, 45 }, new() { 20, 46 }, new() { 21, 45 }, new() { 22, 46 }, new() { 23, 45 }, new() { 24, 46 }, new() { 25, 45 }, new() { 26, 46 }, new() { 27, 45 }, new() { 28, 46 }, new() { 29, 45 }, new() { 30, 46 }, new() { 31, 45 }, new() { 32, 46 }, new() { 33, 45 }, new() { 34, 46 }, new() { 35, 17 }, new() { 36, 18 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, },
                [20] = new() { new() { 13, 3 }, new() { 14, 3 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 31 }, new() { 18, 32 }, new() { 19, 59 }, new() { 20, 60 }, new() { 21, 59 }, new() { 22, 60 }, new() { 23, 59 }, new() { 24, 60 }, new() { 25, 59 }, new() { 26, 60 }, new() { 27, 59 }, new() { 28, 60 }, new() { 29, 59 }, new() { 30, 60 }, new() { 31, 59 }, new() { 32, 60 }, new() { 33, 59 }, new() { 34, 60 }, new() { 35, 31 }, new() { 36, 32 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, },
                [21] = new() { new() { 13, 3 }, new() { 14, 3 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 17 }, new() { 18, 18 }, new() { 19, 45 }, new() { 20, 46 }, new() { 21, 45 }, new() { 22, 46 }, new() { 23, 45 }, new() { 24, 46 }, new() { 25, 45 }, new() { 26, 46 }, new() { 27, 45 }, new() { 28, 46 }, new() { 29, 45 }, new() { 30, 46 }, new() { 31, 45 }, new() { 32, 46 }, new() { 33, 45 }, new() { 34, 46 }, new() { 35, 17 }, new() { 36, 18 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, },
                [22] = new() { new() { 13, 3 }, new() { 14, 3 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 31 }, new() { 18, 32 }, new() { 19, 59 }, new() { 20, 60 }, new() { 21, 59 }, new() { 22, 60 }, new() { 23, 59 }, new() { 24, 60 }, new() { 25, 59 }, new() { 26, 60 }, new() { 27, 59 }, new() { 28, 60 }, new() { 29, 59 }, new() { 30, 60 }, new() { 31, 59 }, new() { 32, 60 }, new() { 33, 59 }, new() { 34, 60 }, new() { 35, 31 }, new() { 36, 32 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, },
                [23] = new() { new() { 13, 17 }, new() { 14, 18 }, new() { 15, 17 }, new() { 16, 18 }, new() { 17, 17 }, new() { 18, 18 }, new() { 19, 45 }, new() { 20, 46 }, new() { 21, 45 }, new() { 22, 46 }, new() { 23, 45 }, new() { 24, 46 }, new() { 25, 45 }, new() { 26, 46 }, new() { 27, 45 }, new() { 28, 46 }, new() { 29, 45 }, new() { 30, 46 }, new() { 31, 45 }, new() { 32, 46 }, new() { 33, 45 }, new() { 34, 46 }, new() { 35, 17 }, new() { 36, 18 }, new() { 37, 17 }, new() { 38, 18 }, new() { 39, 17 }, new() { 40, 18 }, },
                [24] = new() { new() { 13, 31 }, new() { 14, 32 }, new() { 15, 31 }, new() { 16, 32 }, new() { 17, 31 }, new() { 18, 32 }, new() { 19, 59 }, new() { 20, 60 }, new() { 21, 59 }, new() { 22, 60 }, new() { 23, 59 }, new() { 24, 60 }, new() { 25, 59 }, new() { 26, 60 }, new() { 27, 59 }, new() { 28, 60 }, new() { 29, 59 }, new() { 30, 60 }, new() { 31, 59 }, new() { 32, 60 }, new() { 33, 59 }, new() { 34, 60 }, new() { 35, 31 }, new() { 36, 32 }, new() { 37, 31 }, new() { 38, 32 }, new() { 39, 31 }, new() { 40, 32 }, },
                [25] = new() { new() { 11, 3 }, new() { 12, 3 }, new() { 13, 17 }, new() { 14, 18 }, new() { 15, 45 }, new() { 16, 46 }, new() { 17, 45 }, new() { 18, 46 }, new() { 19, 45 }, new() { 20, 46 }, new() { 21, 45 }, new() { 22, 46 }, new() { 23, 45 }, new() { 24, 46 }, new() { 25, 45 }, new() { 26, 46 }, new() { 27, 45 }, new() { 28, 46 }, new() { 29, 45 }, new() { 30, 46 }, new() { 31, 45 }, new() { 32, 46 }, new() { 33, 45 }, new() { 34, 46 }, new() { 35, 45 }, new() { 36, 46 }, new() { 37, 45 }, new() { 38, 46 }, new() { 39, 17 }, new() { 40, 18 }, new() { 41, 3 }, new() { 42, 3 }, },
                [26] = new() { new() { 11, 3 }, new() { 12, 3 }, new() { 13, 31 }, new() { 14, 32 }, new() { 15, 59 }, new() { 16, 60 }, new() { 17, 59 }, new() { 18, 60 }, new() { 19, 59 }, new() { 20, 60 }, new() { 21, 59 }, new() { 22, 60 }, new() { 23, 59 }, new() { 24, 60 }, new() { 25, 59 }, new() { 26, 60 }, new() { 27, 59 }, new() { 28, 60 }, new() { 29, 59 }, new() { 30, 60 }, new() { 31, 59 }, new() { 32, 60 }, new() { 33, 59 }, new() { 34, 60 }, new() { 35, 59 }, new() { 36, 60 }, new() { 37, 59 }, new() { 38, 60 }, new() { 39, 31 }, new() { 40, 32 }, new() { 41, 3 }, new() { 42, 3 }, },
                [27] = new() { new() { 11, 3 }, new() { 12, 3 }, new() { 13, 17 }, new() { 14, 18 }, new() { 15, 45 }, new() { 16, 46 }, new() { 17, 45 }, new() { 18, 46 }, new() { 19, 45 }, new() { 20, 46 }, new() { 21, 45 }, new() { 22, 46 }, new() { 23, 45 }, new() { 24, 46 }, new() { 25, 45 }, new() { 26, 46 }, new() { 27, 45 }, new() { 28, 46 }, new() { 29, 45 }, new() { 30, 46 }, new() { 31, 45 }, new() { 32, 46 }, new() { 33, 45 }, new() { 34, 46 }, new() { 35, 45 }, new() { 36, 46 }, new() { 37, 45 }, new() { 38, 46 }, new() { 39, 17 }, new() { 40, 18 }, new() { 41, 3 }, new() { 42, 3 }, },
                [28] = new() { new() { 11, 3 }, new() { 12, 3 }, new() { 13, 31 }, new() { 14, 32 }, new() { 15, 59 }, new() { 16, 60 }, new() { 17, 59 }, new() { 18, 60 }, new() { 19, 59 }, new() { 20, 60 }, new() { 21, 59 }, new() { 22, 60 }, new() { 23, 59 }, new() { 24, 60 }, new() { 25, 59 }, new() { 26, 60 }, new() { 27, 59 }, new() { 28, 60 }, new() { 29, 59 }, new() { 30, 60 }, new() { 31, 59 }, new() { 32, 60 }, new() { 33, 59 }, new() { 34, 60 }, new() { 35, 59 }, new() { 36, 60 }, new() { 37, 59 }, new() { 38, 60 }, new() { 39, 31 }, new() { 40, 32 }, new() { 41, 3 }, new() { 42, 3 }, },
                [29] = new() { new() { 11, 3 }, new() { 12, 3 }, new() { 13, 17 }, new() { 14, 18 }, new() { 15, 45 }, new() { 16, 46 }, new() { 17, 45 }, new() { 18, 46 }, new() { 19, 17 }, new() { 20, 18 }, new() { 21, 17 }, new() { 22, 18 }, new() { 23, 17 }, new() { 24, 18 }, new() { 25, 17 }, new() { 26, 18 }, new() { 27, 17 }, new() { 28, 18 }, new() { 29, 17 }, new() { 30, 18 }, new() { 31, 17 }, new() { 32, 18 }, new() { 33, 17 }, new() { 34, 18 }, new() { 35, 46 }, new() { 36, 45 }, new() { 37, 46 }, new() { 38, 45 }, new() { 39, 17 }, new() { 40, 18 }, new() { 41, 3 }, new() { 42, 3 }, },
                [30] = new() { new() { 11, 3 }, new() { 12, 3 }, new() { 13, 31 }, new() { 14, 32 }, new() { 15, 59 }, new() { 16, 60 }, new() { 17, 59 }, new() { 18, 60 }, new() { 19, 31 }, new() { 20, 32 }, new() { 21, 31 }, new() { 22, 32 }, new() { 23, 31 }, new() { 24, 32 }, new() { 25, 31 }, new() { 26, 32 }, new() { 27, 31 }, new() { 28, 32 }, new() { 29, 31 }, new() { 30, 32 }, new() { 31, 31 }, new() { 32, 32 }, new() { 33, 31 }, new() { 34, 32 }, new() { 35, 60 }, new() { 36, 59 }, new() { 37, 60 }, new() { 38, 59 }, new() { 39, 31 }, new() { 40, 32 }, new() { 41, 3 }, new() { 42, 3 }, },
                [31] = new() { new() { 9, 3 }, new() { 10, 3 }, new() { 11, 17 }, new() { 12, 18 }, new() { 13, 17 }, new() { 14, 18 }, new() { 15, 45 }, new() { 16, 46 }, new() { 17, 45 }, new() { 18, 46 }, new() { 19, 17 }, new() { 20, 18 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 17 }, new() { 34, 18 }, new() { 35, 46 }, new() { 36, 45 }, new() { 37, 46 }, new() { 38, 45 }, new() { 39, 17 }, new() { 40, 18 }, new() { 41, 17 }, new() { 42, 18 }, new() { 43, 3 }, new() { 44, 3 }, },
                [32] = new() { new() { 9, 3 }, new() { 10, 3 }, new() { 11, 31 }, new() { 12, 32 }, new() { 13, 31 }, new() { 14, 32 }, new() { 15, 59 }, new() { 16, 60 }, new() { 17, 59 }, new() { 18, 60 }, new() { 19, 31 }, new() { 20, 32 }, new() { 22, 4 }, new() { 23, 4 }, new() { 24, 4 }, new() { 25, 4 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 4 }, new() { 30, 4 }, new() { 31, 4 }, new() { 33, 31 }, new() { 34, 32 }, new() { 35, 60 }, new() { 36, 59 }, new() { 37, 60 }, new() { 38, 59 }, new() { 39, 31 }, new() { 40, 32 }, new() { 41, 31 }, new() { 42, 32 }, new() { 43, 3 }, new() { 44, 3 }, },
                [33] = new() { new() { 9, 3 }, new() { 10, 3 }, new() { 11, 17 }, new() { 12, 18 }, new() { 13, 17 }, new() { 14, 18 }, new() { 15, 17 }, new() { 16, 18 }, new() { 17, 17 }, new() { 18, 18 }, new() { 19, 17 }, new() { 20, 18 }, new() { 23, 4 }, new() { 24, 4 }, new() { 25, 4 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 4 }, new() { 30, 4 }, new() { 33, 17 }, new() { 34, 18 }, new() { 35, 17 }, new() { 36, 18 }, new() { 37, 17 }, new() { 38, 18 }, new() { 39, 17 }, new() { 40, 18 }, new() { 41, 17 }, new() { 42, 18 }, new() { 43, 3 }, new() { 44, 3 }, },
                [34] = new() { new() { 9, 3 }, new() { 10, 3 }, new() { 11, 31 }, new() { 12, 32 }, new() { 13, 31 }, new() { 14, 32 }, new() { 15, 31 }, new() { 16, 32 }, new() { 17, 31 }, new() { 18, 32 }, new() { 19, 31 }, new() { 20, 32 }, new() { 24, 4 }, new() { 25, 4 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 4 }, new() { 33, 31 }, new() { 34, 32 }, new() { 35, 31 }, new() { 36, 32 }, new() { 37, 31 }, new() { 38, 32 }, new() { 39, 31 }, new() { 40, 32 }, new() { 41, 31 }, new() { 42, 32 }, new() { 43, 3 }, new() { 44, 3 }, },
                [35] = new() { new() { 25, 4 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    back.Tiles[array[0], code.Key] = new StaticTile(back, chapelsheet, BlendMode.Alpha, array[1]);

                    back.Tiles[array[0], code.Key].TileIndexProperties.Add("Type", "Stone");

                }

            }

            codes = new()
            {
                [17] = new() { },
                [18] = new() { new() { 19, 176 }, new() { 20, 177 }, new() { 21, 178 }, new() { 22, 178 }, new() { 23, 178 }, new() { 24, 178 }, new() { 25, 178 }, new() { 26, 178 }, new() { 27, 179 }, new() { 28, 179 }, new() { 29, 179 }, new() { 30, 179 }, new() { 31, 179 }, new() { 32, 179 }, new() { 33, 180 }, new() { 34, 181 }, },
                [19] = new() { new() { 19, 190 }, new() { 20, 191 }, new() { 21, 192 }, new() { 22, 192 }, new() { 23, 192 }, new() { 24, 192 }, new() { 25, 192 }, new() { 26, 192 }, new() { 27, 193 }, new() { 28, 193 }, new() { 29, 193 }, new() { 30, 193 }, new() { 31, 193 }, new() { 32, 193 }, new() { 33, 194 }, new() { 34, 195 }, },
                [20] = new() { new() { 19, 190 }, new() { 20, 191 }, new() { 21, 192 }, new() { 22, 192 }, new() { 23, 192 }, new() { 24, 192 }, new() { 25, 192 }, new() { 26, 192 }, new() { 27, 193 }, new() { 28, 193 }, new() { 29, 193 }, new() { 30, 193 }, new() { 31, 193 }, new() { 32, 193 }, new() { 33, 194 }, new() { 34, 195 }, },
                [21] = new() { new() { 19, 190 }, new() { 20, 191 }, new() { 21, 192 }, new() { 22, 192 }, new() { 23, 192 }, new() { 24, 192 }, new() { 25, 192 }, new() { 26, 192 }, new() { 27, 193 }, new() { 28, 193 }, new() { 29, 193 }, new() { 30, 193 }, new() { 31, 193 }, new() { 32, 193 }, new() { 33, 194 }, new() { 34, 195 }, },
                [22] = new() { new() { 19, 190 }, new() { 20, 191 }, new() { 21, 192 }, new() { 22, 192 }, new() { 23, 192 }, new() { 24, 192 }, new() { 25, 192 }, new() { 26, 192 }, new() { 27, 193 }, new() { 28, 193 }, new() { 29, 193 }, new() { 30, 193 }, new() { 31, 193 }, new() { 32, 193 }, new() { 33, 194 }, new() { 34, 195 }, },
                [23] = new() { new() { 19, 190 }, new() { 20, 191 }, new() { 21, 192 }, new() { 22, 192 }, new() { 23, 192 }, new() { 24, 192 }, new() { 25, 192 }, new() { 26, 192 }, new() { 27, 193 }, new() { 28, 193 }, new() { 29, 193 }, new() { 30, 193 }, new() { 31, 193 }, new() { 32, 193 }, new() { 33, 194 }, new() { 34, 195 }, },
                [24] = new() { new() { 19, 190 }, new() { 20, 191 }, new() { 21, 192 }, new() { 22, 192 }, new() { 23, 192 }, new() { 24, 192 }, new() { 25, 192 }, new() { 26, 192 }, new() { 27, 193 }, new() { 28, 193 }, new() { 29, 193 }, new() { 30, 193 }, new() { 31, 193 }, new() { 32, 193 }, new() { 33, 194 }, new() { 34, 195 }, },
                [25] = new() { new() { 19, 190 }, new() { 20, 191 }, new() { 21, 192 }, new() { 22, 192 }, new() { 23, 192 }, new() { 24, 192 }, new() { 25, 192 }, new() { 26, 192 }, new() { 27, 193 }, new() { 28, 193 }, new() { 29, 193 }, new() { 30, 193 }, new() { 31, 193 }, new() { 32, 193 }, new() { 33, 194 }, new() { 34, 195 }, },
                [26] = new() { new() { 19, 190 }, new() { 20, 191 }, new() { 21, 192 }, new() { 22, 192 }, new() { 23, 192 }, new() { 24, 192 }, new() { 25, 192 }, new() { 26, 192 }, new() { 27, 193 }, new() { 28, 193 }, new() { 29, 193 }, new() { 30, 193 }, new() { 31, 193 }, new() { 32, 193 }, new() { 33, 194 }, new() { 34, 195 }, },
                [27] = new() { new() { 19, 190 }, new() { 20, 191 }, new() { 21, 192 }, new() { 22, 192 }, new() { 23, 192 }, new() { 24, 192 }, new() { 25, 192 }, new() { 26, 192 }, new() { 27, 193 }, new() { 28, 193 }, new() { 29, 193 }, new() { 30, 193 }, new() { 31, 193 }, new() { 32, 193 }, new() { 33, 194 }, new() { 34, 195 }, },
                [28] = new() { new() { 19, 204 }, new() { 20, 205 }, new() { 21, 206 }, new() { 22, 206 }, new() { 23, 206 }, new() { 24, 206 }, new() { 25, 206 }, new() { 26, 206 }, new() { 27, 207 }, new() { 28, 207 }, new() { 29, 207 }, new() { 30, 207 }, new() { 31, 207 }, new() { 32, 207 }, new() { 33, 208 }, new() { 34, 209 }, },
                [29] = new() { },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    back2.Tiles[array[0], code.Key] = new StaticTile(back2, chapelsheet, BlendMode.Alpha, array[1]);

                }

            }

            codes = new()
            {
                [0] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [1] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [2] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [3] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [4] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [5] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [6] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [7] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [8] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [9] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [10] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [11] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [12] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 3 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 3 }, new() { 18, 3 }, new() { 19, 3 }, new() { 20, 3 }, new() { 21, 3 }, new() { 22, 3 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 3 }, new() { 34, 3 }, new() { 35, 3 }, new() { 36, 3 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [13] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 3 }, new() { 39, 3 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [14] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 3 }, new() { 39, 3 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [15] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 3 }, new() { 39, 3 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [16] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 3 }, new() { 39, 3 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [17] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 3 }, new() { 39, 3 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [18] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 3 }, new() { 13, 3 }, new() { 14, 3 }, new() { 39, 3 }, new() { 40, 3 }, new() { 41, 3 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [19] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 3 }, new() { 41, 3 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [20] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 3 }, new() { 41, 3 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [21] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 3 }, new() { 41, 3 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [22] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 3 }, new() { 41, 3 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [23] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 3 }, new() { 41, 3 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [24] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 3 }, new() { 11, 3 }, new() { 12, 3 }, new() { 41, 3 }, new() { 42, 3 }, new() { 43, 3 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [25] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 3 }, new() { 43, 3 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [26] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 3 }, new() { 43, 3 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [27] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 3 }, new() { 43, 3 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [28] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 3 }, new() { 43, 3 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [29] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 3 }, new() { 43, 3 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [30] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 3 }, new() { 9, 3 }, new() { 10, 3 }, new() { 43, 3 }, new() { 44, 3 }, new() { 45, 3 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [31] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 3 }, new() { 45, 3 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [32] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 3 }, new() { 21, 3 }, new() { 32, 3 }, new() { 45, 3 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [33] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 3 }, new() { 21, 3 }, new() { 22, 4 }, new() { 31, 4 }, new() { 32, 3 }, new() { 45, 3 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [34] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 3 }, new() { 21, 3 }, new() { 22, 4 }, new() { 23, 4 }, new() { 30, 4 }, new() { 31, 4 }, new() { 32, 3 }, new() { 45, 3 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [35] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, new() { 8, 3 }, new() { 9, 3 }, new() { 10, 3 }, new() { 11, 3 }, new() { 12, 3 }, new() { 13, 3 }, new() { 14, 3 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 3 }, new() { 18, 3 }, new() { 19, 3 }, new() { 20, 3 }, new() { 21, 3 }, new() { 22, 4 }, new() { 23, 4 }, new() { 24, 4 }, new() { 29, 4 }, new() { 30, 4 }, new() { 31, 4 }, new() { 32, 3 }, new() { 33, 3 }, new() { 34, 3 }, new() { 35, 3 }, new() { 36, 3 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, new() { 41, 3 }, new() { 42, 3 }, new() { 43, 3 }, new() { 44, 3 }, new() { 45, 3 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 2 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    buildings.Tiles[array[0], code.Key] = new StaticTile(back, chapelsheet, BlendMode.Alpha, array[1]);

                }

            }

            codes = new()
            {
                [0] = new() { new() { 14, 2 }, new() { 17, 1 }, new() { 34, 2 }, new() { 37, 1 }, },
                [1] = new() { },
                [2] = new() { },
                [3] = new() { },
                [4] = new() { },
                [5] = new() { new() { 15, 8 }, new() { 35, 8 }, },
                [6] = new() { },
                [7] = new() { },
                [8] = new() { new() { 11, 2 }, new() { 14, 1 }, new() { 37, 2 }, new() { 40, 1 }, },
                [9] = new() { },
                [10] = new() { },
                [11] = new() { },
                [12] = new() { new() { 16, 9 }, new() { 36, 9 }, },
                [13] = new() { new() { 12, 8 }, new() { 38, 8 }, },
                [14] = new() { },
                [15] = new() { },
                [16] = new() { new() { 8, 2 }, new() { 11, 1 }, new() { 40, 2 }, new() { 43, 1 }, },
                [17] = new() { },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { new() { 13, 9 }, new() { 39, 9 }, },
                [21] = new() { new() { 9, 8 }, new() { 41, 8 }, },
                [22] = new() { },
                [23] = new() { },
                [24] = new() { new() { 5, 2 }, new() { 8, 1 }, new() { 43, 2 }, new() { 46, 1 }, },
                [25] = new() { },
                [26] = new() { },
                [27] = new() { },
                [28] = new() { new() { 10, 9 }, new() { 42, 9 }, },
                [29] = new() { new() { 6, 8 }, new() { 44, 8 }, },
                [30] = new() { new() { 15, 10 }, new() { 35, 11 }, },
                [31] = new() { },
                [32] = new() { },
                [33] = new() { },
                [34] = new() { },
                [35] = new() { },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    TerrainField tField = new(IconData.tilesheets.chapel, array[1], new Vector2(array[0], code.Key) * 64);

                    if (array[1] == 2 || array[1] == 11)
                    {

                        tField.flip = true;

                    }
                    if (array[1] == 8)
                    {
                        tField.layer += 0.0384f;

                    }
                    foreach (Vector2 bottom in tField.baseTiles)
                    {

                        if (buildings.Tiles[(int)bottom.X, (int)bottom.Y] == null && back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                        {

                            buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, chapelsheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);
                        }

                    }

                    terrainFields.Add(tField);

                }

            }

            // ALTAR

            codes = new()
            {
                [14] = new() { new() { 24, 3 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    TerrainField tField = new(IconData.tilesheets.chapel, array[1], new Vector2(array[0], code.Key) * 64);

                    foreach (Vector2 bottom in tField.baseTiles)
                    {

                        if (buildings.Tiles[(int)bottom.X, (int)bottom.Y] == null && back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                        {

                            buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, chapelsheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);
                        }

                    }

                    altarTiles.Add(tField);

                }

            }

            // CIRCLE

            TerrainField circleTile = new(IconData.tilesheets.ritual, 1, new Vector2(1504, 832), TerrainField.shadows.none);

            circleTile.color = Color.White * 0.1f;

            circleTile.fadeout = 1f;

            circleTile.layer = 0.00064f;

            circleTiles.Add(circleTile);


            // OVERHEAD

            codes = new()
            {
                [0] = new() { new() { 13, 4 }, new() { 15, 5 }, new() { 19, 5 }, new() { 23, 5 }, new() { 27, 5 }, new() { 31, 5 }, new() { 35, 5 }, new() { 39, 6 }, },
                [1] = new() { new() { 19, 7 }, new() { 25, 7 }, new() { 31, 7 }, },


            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    TerrainField tField = new(IconData.tilesheets.chapel, array[1], new Vector2(array[0], code.Key) * 64);

                    frontFields.Add(tField);

                }

            }

            codes = new()
            {
                [0] = new() { },
                [1] = new() { },
                [2] = new() { },
                [3] = new() { },
                [4] = new() { },
                [5] = new() { },
                [6] = new() { },
                [7] = new() { },
                [8] = new() { },
                [9] = new() { },
                [10] = new() { },
                [11] = new() { },
                [12] = new() { new() { 16, 1 }, new() { 36, 1 }, },
                [13] = new() { },
                [14] = new() { new() { 25, 1 }, new() { 28, 1 }, },
                [15] = new() { },
                [16] = new() { },
                [17] = new() { },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { new() { 13, 1 }, new() { 39, 1 }, },
                [21] = new() { },
                [22] = new() { },
                [23] = new() { },
                [24] = new() { },
                [25] = new() { },
                [26] = new() { },
                [27] = new() { },
                [28] = new() { new() { 10, 1 }, new() { 42, 1 }, },
                [29] = new() { },
                [30] = new() { },
                [31] = new() { new() { 17, 1 }, new() { 35, 1 }, },
                [32] = new() { },
                [33] = new() { },
                [34] = new() { },
                [35] = new() { },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    LightField light = new(new Vector2(array[0], code.Key) * 64 + new Vector2(64, 32));

                    light.luminosity = 4;

                    light.lightAmbience = 0.7f;

                    lightFields.Add(light);

                }

            }

            addDialogue();

            this.map = newMap;

        }

        public override void addDialogue()
        {

            if (dialogueTiles.Count > 0) { return; }

            dialogueTiles.Add(new(25, 15), CharacterHandle.characters.star_altar);

            dialogueTiles.Add(new(26, 15), CharacterHandle.characters.star_altar);

            dialogueTiles.Add(new(27, 15), CharacterHandle.characters.star_altar);

            dialogueTiles.Add(new(28, 15), CharacterHandle.characters.star_altar);

            dialogueTiles.Add(new(25, 16), CharacterHandle.characters.star_altar);

            dialogueTiles.Add(new(26, 16), CharacterHandle.characters.star_altar);

            dialogueTiles.Add(new(27, 16), CharacterHandle.characters.star_altar);

            dialogueTiles.Add(new(28, 16), CharacterHandle.characters.star_altar);

            dialogueTiles.Add(new(16, 31), CharacterHandle.characters.star_desk);

            dialogueTiles.Add(new(17, 31), CharacterHandle.characters.star_desk);

            dialogueTiles.Add(new(16, 32), CharacterHandle.characters.star_desk);

            dialogueTiles.Add(new(17, 32), CharacterHandle.characters.star_desk);

            dialogueTiles.Add(new(36, 31), CharacterHandle.characters.star_bench);

            dialogueTiles.Add(new(37, 31), CharacterHandle.characters.star_bench);

            dialogueTiles.Add(new(36, 32), CharacterHandle.characters.star_bench);

            dialogueTiles.Add(new(37, 32), CharacterHandle.characters.star_bench);

        }

        public override void updateWarps()
        {

            warps.Clear();

            warps.Add(new Warp(25, 35, "Mine", 17, 5, flipFarmer: false));

            warps.Add(new Warp(26, 35, "Mine", 17, 5, flipFarmer: false));

            warps.Add(new Warp(27, 35, "Mine", 17, 5, flipFarmer: false));

            warps.Add(new Warp(28, 35, "Mine", 17, 5, flipFarmer: false));

            warps.Add(new Warp(29, 35, "Mine", 17, 5, flipFarmer: false));

            warps.Add(new Warp(30, 35, "Mine", 17, 5, flipFarmer: false));

        }

    }

}
