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
using StardewValley.TerrainFeatures;
using System.Threading;
using System.Xml.Serialization;
using StardewDruid.Data;
using StardewValley.Extensions;
using StardewDruid.Location.Terrain;
using StardewDruid.Handle;


namespace StardewDruid.Location.Druid
{

    public class Moors : DruidLocation
    {

        public Moors() { }

        public Moors(string Name)
            : base(Name)
        {

        }

        public override void drawBackground(SpriteBatch b)
        {

            base.drawBackground(b);

            DrawParallaxHorizon(b);

        }

        public virtual void DrawParallaxHorizon(SpriteBatch b)
        {

            float offset = Game1.viewport.Y == 0 ? 0f : Game1.viewport.Y * 192f / map.DisplayHeight;

            Vector2 localPosition = Game1.GlobalToLocal(Game1.viewport, new Vector2(27, 11) * 64 + new Vector2(0, offset));

            Color rain = Game1.isRaining ? Color.LightSkyBlue : Color.White;

            int currentTime = (int)(Game1.gameTimeInterval / (float)Game1.realMilliSecondsPerGameMinute % 10f);

            float pastDarkTime = Utility.ConvertTimeToMinutes(Game1.timeOfDay + currentTime - Game1.getStartingToGetDarkTime(this));

            float gettingDarkerTime = Utility.ConvertTimeToMinutes(Game1.getTrulyDarkTime(this) - Game1.getStartingToGetDarkTime(this));

            float value = pastDarkTime / gettingDarkerTime;

            b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.skyblue], localPosition, new Rectangle(0, 0, 864, 432), rain, 0f, new Vector2(432, 216), 4f, SpriteEffects.None, 0f);

            b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.overlook], localPosition + new Vector2(208, 360), new Rectangle(0, 0, 144, 96), Color.White, 0f, new Vector2(80, 96), 4f, SpriteEffects.None, 0f);

            if (value > 0f)
            {

                b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.skynight], localPosition, new Rectangle(0, 0, 864, 432), Color.White * value, 0f, new Vector2(432, 216), 4f, SpriteEffects.None, 0f);

                b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.overlook], localPosition + new Vector2(208, 360), new Rectangle(0, 96, 144, 96), Color.White * value, 0f, new Vector2(80, 96), 4f, SpriteEffects.None, 0.1f);

            }

        }

        public override void OnMapLoad(xTile.Map map)
        {


            xTile.Dimensions.Size tileSize = map.GetLayer("Back").TileSize;

            xTile.Map newMap = new(map.Id);

            Layer back = new("Back", newMap, new(54, 72), tileSize);

            newMap.AddLayer(back);

            Layer buildings = new("Buildings", newMap, new(54, 72), tileSize);

            newMap.AddLayer(buildings);

            Layer front = new("Front", newMap, new(54, 72), tileSize);

            newMap.AddLayer(front);

            Layer alwaysfront = new("AlwaysFront", newMap, new(54, 72), tileSize);

            newMap.AddLayer(alwaysfront);

            TileSheet outdoor = new(
                newMap,
                "StardewDruid.Tilesheets.groundspring",
                new(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.groundspring].Width / 16,
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.groundspring].Height / 16
                ),
                new(16, 16)
            );

            newMap.AddTileSheet(outdoor);

            newMap.LoadTileSheets(Game1.mapDisplayDevice);

            IsOutdoors = true;

            ignoreOutdoorLighting.Set(false);

            mapReset();

            // -----------------------------------------------------
            // Tiles

            Dictionary<int, List<List<int>>> codes = new()
            {
                [18] = new() { new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, },
                [19] = new() { new() { 20, 2 }, new() { 21, 2 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, },
                [20] = new() { new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 2 }, new() { 18, 3 }, new() { 19, 3 }, new() { 20, 3 }, new() { 21, 3 }, new() { 22, 2 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, },
                [21] = new() { new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 4 }, new() { 19, 4 }, new() { 20, 4 }, new() { 21, 4 }, new() { 22, 3 }, new() { 23, 2 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, },
                [22] = new() { new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 4 }, new() { 19, 4 }, new() { 20, 4 }, new() { 21, 4 }, new() { 22, 3 }, new() { 23, 2 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, },
                [23] = new() { new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 4 }, new() { 19, 4 }, new() { 20, 4 }, new() { 21, 4 }, new() { 22, 3 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [24] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 2 }, new() { 17, 3 }, new() { 18, 4 }, new() { 19, 4 }, new() { 20, 4 }, new() { 21, 4 }, new() { 22, 3 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 3 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 3 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 2 }, new() { 18, 3 }, new() { 19, 3 }, new() { 20, 3 }, new() { 21, 3 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 3 }, new() { 25, 4 }, new() { 26, 320 }, new() { 27, 340 }, new() { 28, 321 }, new() { 29, 4 }, new() { 30, 3 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 3 }, new() { 24, 4 }, new() { 25, 320 }, new() { 26, 324 }, new() { 27, 349 }, new() { 28, 345 }, new() { 29, 321 }, new() { 30, 4 }, new() { 31, 3 }, new() { 32, 2 }, new() { 33, 1 }, new() { 34, 2 }, new() { 35, 3 }, new() { 36, 3 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, new() { 41, 2 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 4 }, new() { 24, 320 }, new() { 25, 344 }, new() { 26, 348 }, new() { 27, 331 }, new() { 28, 351 }, new() { 29, 325 }, new() { 30, 321 }, new() { 31, 4 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 3 }, new() { 35, 4 }, new() { 36, 4 }, new() { 37, 4 }, new() { 38, 4 }, new() { 39, 4 }, new() { 40, 4 }, new() { 41, 3 }, new() { 42, 2 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 4 }, new() { 24, 343 }, new() { 25, 348 }, new() { 26, 330 }, new() { 27, 328 }, new() { 28, 330 }, new() { 29, 350 }, new() { 30, 341 }, new() { 31, 4 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 3 }, new() { 35, 4 }, new() { 36, 4 }, new() { 37, 4 }, new() { 38, 4 }, new() { 39, 4 }, new() { 40, 4 }, new() { 41, 3 }, new() { 42, 2 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 4 }, new() { 24, 323 }, new() { 25, 327 }, new() { 26, 350 }, new() { 27, 329 }, new() { 28, 349 }, new() { 29, 346 }, new() { 30, 322 }, new() { 31, 4 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 3 }, new() { 35, 4 }, new() { 36, 4 }, new() { 37, 4 }, new() { 38, 4 }, new() { 39, 4 }, new() { 40, 4 }, new() { 41, 3 }, new() { 42, 2 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 2 }, new() { 23, 3 }, new() { 24, 4 }, new() { 25, 343 }, new() { 26, 347 }, new() { 27, 351 }, new() { 28, 326 }, new() { 29, 322 }, new() { 30, 4 }, new() { 31, 3 }, new() { 32, 2 }, new() { 33, 1 }, new() { 34, 2 }, new() { 35, 3 }, new() { 36, 3 }, new() { 37, 3 }, new() { 38, 3 }, new() { 39, 3 }, new() { 40, 3 }, new() { 41, 2 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 3 }, new() { 25, 4 }, new() { 26, 323 }, new() { 27, 342 }, new() { 28, 322 }, new() { 29, 4 }, new() { 30, 3 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [32] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 3 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 3 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [33] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [34] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [35] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [36] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [37] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [38] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [39] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [40] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [41] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [42] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [43] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [44] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [45] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [46] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [47] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [48] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [49] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [50] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [51] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [52] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [53] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [54] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [55] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [56] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [57] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [58] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [59] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [60] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [61] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [62] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [63] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [64] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [65] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [66] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [67] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [68] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [69] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 2 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 2 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [70] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 2 }, new() { 25, 3 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [71] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 2 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> groundCode in codes)
            {

                foreach (List<int> array in groundCode.Value)
                {

                    switch (array[1])
                    {
                        case 1:

                            back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, Mod.instance.randomIndex.Next(80));

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Grass");

                            break;

                        case 2:

                            switch (Mod.instance.randomIndex.Next(4))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 160 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 180 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 2:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 212 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 3:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 232 + Mod.instance.randomIndex.Next(8));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;

                        case 3:

                            switch (Mod.instance.randomIndex.Next(4))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 168 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 188 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 2:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 204 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 3:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 224 + Mod.instance.randomIndex.Next(8));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;

                        case 4:

                            switch (Mod.instance.randomIndex.Next(4))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 176 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 196 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 2:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 200 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 3:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 220 + Mod.instance.randomIndex.Next(4));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;

                        default:

                            back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, array[1]);

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;
                    }
                }

            }

            // -----------------------------------------------------
            // Building

            codes = new()
            {
                [17] = new() { new() { 22, 85 }, new() { 23, 90 }, new() { 24, 109 }, new() { 25, 110 }, new() { 26, 111 }, new() { 27, 80 }, new() { 28, 81 }, new() { 29, 100 }, new() { 30, 101 }, new() { 31, 102 }, new() { 32, 103 }, new() { 33, 86 }, },
                [18] = new() { new() { 20, 85 }, new() { 21, 84 }, new() { 34, 87 }, new() { 35, 86 }, },
                [19] = new() { new() { 14, 85 }, new() { 15, 100 }, new() { 16, 101 }, new() { 17, 102 }, new() { 18, 103 }, new() { 19, 105 }, new() { 36, 106 }, new() { 37, 108 }, new() { 38, 109 }, new() { 39, 94 }, },
                [20] = new() { new() { 12, 85 }, new() { 13, 104 }, new() { 39, 114 }, new() { 40, 95 }, },
                [21] = new() { new() { 8, 92 }, new() { 9, 82 }, new() { 10, 83 }, new() { 11, 84 }, new() { 40, 115 }, new() { 41, 94 }, },
                [22] = new() { new() { 7, 93 }, new() { 8, 112 }, new() { 41, 114 }, new() { 42, 80 }, new() { 43, 81 }, new() { 44, 82 }, new() { 45, 83 }, new() { 46, 100 }, new() { 47, 101 }, new() { 48, 95 }, },
                [23] = new() { new() { 2, 85 }, new() { 3, 108 }, new() { 4, 109 }, new() { 5, 110 }, new() { 6, 111 }, new() { 7, 113 }, new() { 48, 115 }, new() { 49, 94 }, },
                [24] = new() { new() { 0, 103 }, new() { 1, 104 }, new() { 49, 114 }, new() { 50, 108 }, new() { 51, 109 }, new() { 52, 110 }, new() { 53, 111 }, },
                [25] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [32] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [33] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [34] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [35] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [36] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [37] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [38] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [39] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [40] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [41] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [42] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [43] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [44] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [45] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [46] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [47] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [48] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [49] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [50] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [51] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [52] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [53] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [54] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [55] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [56] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [57] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [58] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [59] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [60] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [61] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [62] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [63] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [64] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [65] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [66] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [67] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [68] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [69] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [70] = new() { new() { 0, 1 }, new() { 53, 1 }, },
                [71] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    if(back.Tiles[array[0], code.Key] != null)
                    {

                        buildings.Tiles[array[0], code.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, back.Tiles[array[0], code.Key].TileIndex);

                    }
                    else
                    if (array[1] == 1)
                    {

                        buildings.Tiles[array[0], code.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, Mod.instance.randomIndex.Next(80));

                    }
                    else
                    {

                        buildings.Tiles[array[0], code.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, array[1]);


                    }

                }

            }

            List<Vector2> reserved = new();

            codes = new()
            {

                [24] = new() { new() { 1, 6 }, },
                [25] = new() { },
                [26] = new() { },
                [27] = new() { },
                [28] = new() { },
                [29] = new() { },
                [30] = new() { new() { 46, 9 }, },
                [31] = new() { },
                [32] = new() { },
                [33] = new() { },
                [34] = new() { },
                [35] = new() { new() { 8, 9 }, },
                [36] = new() { new() { 33, 10 }, },
                [37] = new() { new() { 49, 12 }, },
                [38] = new() { },
                [39] = new() { new() { 1, 4 }, },
                [40] = new() { },
                [41] = new() { new() { 37, 9 }, },
                [42] = new() { new() { 16, 3 }, },
                [43] = new() { new() { 44, 5 }, },
                [44] = new() { },
                [45] = new() { },
                [46] = new() { },
                [47] = new() { },
                [48] = new() { },
                [49] = new() { },
                [50] = new() { },
                [51] = new() { new() { 23, 9 }, },
                [52] = new() { },
                [53] = new() { },
                [54] = new() { },
                [55] = new() { },
                [56] = new() { },
                [57] = new() { new() { 4, 5 }, },
                [58] = new() { },
                [59] = new() { },
                [60] = new() { new() { 17, 11 }, new() { 34, 3 }, },
                [61] = new() { },
                [62] = new() { },
                [63] = new() { new() { 48, 11 }, },
                [64] = new() { },
                [65] = new() { },
                [66] = new() { },
                [67] = new() { },



            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    switch (array[1])
                    {

                        default:

                            /*DarkOak darkoak = new(new Vector2(array[0], code.Key) * 64, array[1] > 6 ? 2 : 1);

                            darkoak.season = 0;

                            foreach (Vector2 bottom in darkoak.baseTiles)
                            {
                                reserved.Add(bottom);
                                buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, outdoor, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                            }

                            terrainFields.Add(darkoak);

                            frontFields.Add(darkoak);

                            if (array[1] % 2 == 0)
                            {

                                darkoak.ruined = true;

                            }
                            else
                            {

                                darkoak.AddLitter();

                            }

                            break;*/

                        case 3:
                        case 4:
                        case 9:
                        case 10:

                            Hawthorn hawthorn = new(new Vector2(array[0], code.Key) * 64, array[1] > 6 ? 2 : 1)
                            {
                                season = 0
                            };

                            foreach (Vector2 bottom in hawthorn.baseTiles)
                            {
                                reserved.Add(bottom);
                                buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, outdoor, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                            }

                            terrainFields.Add(hawthorn);

                            if (array[1] % 2 == 0)
                            {

                                hawthorn.ruined = true;

                            }
                            else
                            {

                                hawthorn.AddLitter();

                            }

                            break;

                        case 5:
                        case 6:
                        case 11:
                        case 12:

                            Holly holly = new(new Vector2(array[0], code.Key) * 64, array[1] > 6 ? 2 : 1)
                            {
                                season = 0
                            };

                            foreach (Vector2 bottom in holly.baseTiles)
                            {
                                reserved.Add(bottom);
                                buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, outdoor, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                            }

                            terrainFields.Add(holly);

                            if (array[1] % 2 == 0)
                            {

                                holly.ruined = true;

                            }
                            else
                            {

                                holly.AddLitter();

                            }

                            break;
                    }


                }

            }

            codes = new()
            {
                [10] = new() { new() { 17, 2 }, },
                [11] = new() { },
                [12] = new() { },
                [13] = new() { },
                [14] = new() { },
                [15] = new() { },
                [16] = new() { },
                [17] = new() { },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { },
                [21] = new() { },
                [22] = new() { },
                [23] = new() { new() { 34, 3 }, new() { 50, 9 }, },
                [24] = new() { new() { 6, 6 }, },
                [25] = new() { new() { 46, 8 }, },
                [26] = new() { },
                [27] = new() { },
                [28] = new() { },
                [29] = new() { },
                [30] = new() { new() { 8, 4 }, },
                [31] = new() { },
                [32] = new() { },
                [33] = new() { },
                [34] = new() { new() { 40, 5 }, },
                [35] = new() { },
                [36] = new() { new() { 1, 7 }, new() { 47, 7 }, },
                [37] = new() { },
                [38] = new() { },
                [39] = new() { },
                [40] = new() { new() { 11, 8 }, },
                [41] = new() { },
                [42] = new() { },
                [43] = new() { },
                [44] = new() { },
                [45] = new() { },
                [46] = new() { },
                [47] = new() { new() { 2, 9 }, },
                [48] = new() { },
                [49] = new() { },
                [50] = new() { },
                [51] = new() { },
                [52] = new() { new() { 8, 1 }, },
                [53] = new() { },
                [54] = new() { },
                [55] = new() { },
                [56] = new() { },


            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    MoorsMonument tField = new(
                        new Vector2(array[0], code.Key) * 64,
                        array[1]
                        );

                    foreach (Vector2 bottom in tField.baseTiles)
                    {

                        reserved.Add(bottom);

                        if (buildings.Tiles[(int)bottom.X, (int)bottom.Y] == null && back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                        {

                            buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, outdoor, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                        }

                        switch (array[1])
                        {
                            case 1:

                                dialogueTiles.Add(new((int)bottom.X, (int)bottom.Y), CharacterHandle.characters.cairn_witches);

                                break;

                            case 2:

                                dialogueTiles.Add(new((int)bottom.X, (int)bottom.Y), CharacterHandle.characters.cairn_dragon);

                                break;

                            case 3:

                                dialogueTiles.Add(new((int)bottom.X, (int)bottom.Y), CharacterHandle.characters.cairn_warrior);

                                break;

                        }

                    }

                    terrainFields.Add(tField);

                }

            }

            // FLOWER CODES

            codes = new()
            {

                [19] = new() { new() { 20, 2 }, new() { 21, 2 }, },
                [20] = new() { new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, },
                [21] = new() { new() { 12, 1 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 38, 2 }, new() { 39, 2 }, },
                [22] = new() { new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 39, 2 }, new() { 40, 2 }, },
                [23] = new() { new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [24] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 2 }, new() { 11, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 2 }, new() { 11, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 2 }, new() { 11, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [32] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [33] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [34] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 2 }, new() { 14, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [35] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [36] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [37] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [38] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [39] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [40] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [41] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 2 }, new() { 18, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [42] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [43] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 2 }, new() { 16, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [44] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 2 }, new() { 15, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 49, 2 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [45] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 2 }, new() { 14, 2 }, new() { 49, 2 }, new() { 50, 2 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, },
                [46] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 2 }, new() { 14, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 1 }, new() { 53, 1 }, },
                [47] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 50, 2 }, new() { 51, 2 }, new() { 52, 1 }, new() { 53, 1 }, },
                [48] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 51, 2 }, new() { 52, 1 }, new() { 53, 1 }, },
                [49] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 51, 2 }, new() { 52, 2 }, new() { 53, 1 }, },
                [50] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 52, 2 }, new() { 53, 1 }, },
                [51] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 52, 2 }, new() { 53, 1 }, },
                [52] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 52, 2 }, new() { 53, 2 }, },
                [53] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 2 }, new() { 14, 2 }, new() { 53, 2 }, },
                [54] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 1 }, },
                [55] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 14, 2 }, new() { 15, 1 }, },
                [56] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 14, 2 }, new() { 15, 1 }, },
                [57] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 1 }, },
                [58] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, },
                [59] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, },
                [60] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 2 }, new() { 3, 2 }, new() { 4, 2 }, new() { 5, 2 }, new() { 6, 2 }, new() { 7, 2 }, },
                [61] = new() { new() { 0, 2 }, new() { 1, 2 }, new() { 2, 2 }, },



            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    if (reserved.Contains(new Vector2(array[0], code.Key)))
                    {
                        continue;
                    }

                    Flower flowergrass = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.heath);

                    grassFields.Add(flowergrass);

                    if (array[1] == 1)
                    {

                        Flower offsetgrass = new(new Vector2(array[0] - 0.5f, code.Key - 0.5f) * 64, Flower.grasstypes.heath);

                        grassFields.Add(offsetgrass);

                    }

                }

            }

            // CIRCLE

            Terrain.RitualCircle circleTile = new(new Vector2(24,25.5f)*64)
            {
                color = Mod.instance.iconData.schemeColours[IconData.schemes.death],

                fadeout = 0.5f,

                disabled = true
            };

            circleTile.LoadCandles();

            terrainFields.Add(circleTile);

            this.map = newMap;

        }

        public override void updateWarps()
        {

            warps.Clear();

            warps.Add(new Warp(25, 71, LocationHandle.druid_sanctuary_name, 26, 7, flipFarmer: false));

            warps.Add(new Warp(26, 71, LocationHandle.druid_sanctuary_name, 26, 7, flipFarmer: false));

            warps.Add(new Warp(27, 71, LocationHandle.druid_sanctuary_name, 27, 7, flipFarmer: false));

            warps.Add(new Warp(28, 71, LocationHandle.druid_sanctuary_name, 27, 7, flipFarmer: false));

        }

    }

}
