using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Weald;
using StardewDruid.Character;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Buildings;
using StardewValley.Characters;
using StardewValley.GameData.FarmAnimals;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using xTile;
using xTile.Layers;
using xTile.ObjectModel;
using xTile.Tiles;
using static StardewValley.Minigames.TargetGame;


namespace StardewDruid
{
    static class ModUtility
    {

        //======================== Animations

        public static List<TemporaryAnimatedSprite> AnimateDecoration(GameLocation location, Vector2 origin, string name = "weald", float size = 1f, float interval = 1000f, float depth = 0.0001f)
        {

            List<TemporaryAnimatedSprite> animations = new();

            Vector2 originOffset = origin + new Vector2(32, 32) - (new Vector2(32, 32) * (3f * size));

            Microsoft.Xna.Framework.Rectangle rect = new(0, 0, 64, 64);

            Texture2D decorationTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Decorations.png"));

            switch (name)
            {

                case "mists":

                    rect.X += 64;

                    break;

                case "stars":

                    rect.X += 128;

                    break;

                case "fates":

                    rect.X += 192;

                    break;

                case "ether":

                    rect.Y += 64;

                    break;

            }

            TemporaryAnimatedSprite radiusAnimation = new(0, interval, 1, 1, originOffset, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = decorationTexture,

                scale = 3f * size,

                timeBasedMotion = true,

                layerDepth = depth,

                rotationChange = (float)(Math.PI / 120),

                Parent = location,

                alpha = 0.65f,

            };

            location.temporarySprites.Add(radiusAnimation);

            animations.Add(radiusAnimation);

            /*if(name != "Ether")
            {

                TemporaryAnimatedSprite glyphAnimation = new(0, interval, 1, 1, originOffset, false, false)
                {

                    sourceRect = new Microsoft.Xna.Framework.Rectangle(0, 64, 64, 64),

                    sourceRectStartingPos = new Vector2(0, 64),

                    texture = decorationTexture,

                    scale = 3f * size,

                    timeBasedMotion = true,

                    layerDepth = depth + 0.0001f,

                    rotationChange = 0 - (float)(Math.PI / 60),

                    Parent = location,

                    alpha = 0.45f,

                };

                location.temporarySprites.Add(glyphAnimation);

                animations.Add(glyphAnimation);

            }*/

            return animations;

        }

        public static void AnimateQuickWarp(GameLocation location, Vector2 origin, bool reverse = false)
        {

            Vector2 originOffset = origin - new Vector2(32, 32);

            Microsoft.Xna.Framework.Rectangle rect = reverse ? new(0, 32, 32, 32) : new(0, 0, 32, 32);

            TemporaryAnimatedSprite cursorAnimation = new(0, 75, 8, 1, originOffset, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(0, rect.Y),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Warp.png")),

                scale = 4f,

                layerDepth = 0.001f,

                alpha = 0.65f,

            };

            location.temporarySprites.Add(cursorAnimation);


        }

        public static TemporaryAnimatedSprite AnimateCursor(GameLocation location, Vector2 origin, string cursor = "weald", float interval = 1200f, float scale = 3f)
        {

            Vector2 originOffset = origin + new Vector2(32,32) - new Vector2(16*scale, 16*scale);

            Microsoft.Xna.Framework.Rectangle rect = new(0, 0, 32, 32);

            switch (cursor)
            {

                case "mists":

                    rect.X += 32;

                    break;

                case "stars":

                    rect.X += 64;

                    break;

                case "fates":

                    rect.X += 96;

                    break;

                case "comet":

                    rect.Y += 32;

                    break;

                case "target":

                    rect.X += 64;

                    rect.Y += 32;

                    break;

                case "death":

                    rect.X += 96;

                    rect.Y += 32;

                    break;

            }

            TemporaryAnimatedSprite cursorAnimation = new(0, interval, 1, 1, originOffset, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Cursors.png")),

                scale = scale,

                layerDepth = 0.001f,

                timeBasedMotion = true,

                rotationChange = (float)(Math.PI / 60),

                alpha = 0.65f,

            };

            location.temporarySprites.Add(cursorAnimation);

            return cursorAnimation;

        }

        public static TemporaryAnimatedSprite AnimateCharge(GameLocation location, Vector2 origin, string cursor = "weald")
        {
            
            Vector2 originOffset = origin - new Vector2(24,24);

            Microsoft.Xna.Framework.Rectangle rect = new(0, 64, 32, 32);

            switch (cursor)
            {

                case "chaos":

                    rect.Y += 32;

                    break;

                case "mists":

                    rect.X += 32;

                    break;

                case "stars":

                    rect.X += 64;

                    break;

                case "fates":

                    rect.X += 96;

                    break;

            }

            TemporaryAnimatedSprite cursorAnimation = new(0, 2000, 1, 30, originOffset, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Cursors.png")),

                scale = 3.5f,

                layerDepth = 0.001f,

                timeBasedMotion = true,

                rotationChange = (float)(Math.PI / 120),

                Parent = location,

                alpha = 0.65f,

            };

            location.temporarySprites.Add(cursorAnimation);

            return cursorAnimation;

        }

        public static void AnimateGlare(GameLocation location, Vector2 origin, Color color)
        {

            Vector2 originOffset = origin + new Vector2(16, 16);

            TemporaryAnimatedSprite glareAnimation = new(0, 1000f, 1, 1, originOffset, false, false)
            {

                sourceRect = new(0, 0, 32, 32),

                sourceRectStartingPos = new Vector2(0, 0),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Glare.png")),

                scale = 1f,

                scaleChange = 0.002f,

                motion = new Vector2(-0.032f, -0.032f),

                layerDepth = 0.002f,

                timeBasedMotion = true,

                alphaFade = 0.0005f,

                color = color,

            };

            location.temporarySprites.Add(glareAnimation);

        }

        public static void AnimateHands(Farmer player, int direction, int timeFrame)
        {

            if (Mod.instance.Config.disableHands)
            {

                return;

            }

            if (Mod.instance.Helper.ModRegistry.IsLoaded("PeacefulEnd.FashionSense"))
            {

                return;

            }

            player.Halt();

            FarmerSprite.AnimationFrame carryAnimation;

            int frameIndex;

            bool frameFlip = false;

            switch (direction)
            {

                case 0: // Up

                    frameIndex = 12;

                    break;

                case 1: // Right

                    frameIndex = 6;

                    break;

                case 2: // Down

                    frameIndex = 0;

                    break;

                default: // Left

                    frameIndex = 6;

                    frameFlip = true; // same as right but flipped

                    break;

            }

            carryAnimation = new(frameIndex, timeFrame, true, frameFlip);

            player.FarmerSprite.animateOnce(new FarmerSprite.AnimationFrame[1] { carryAnimation });

        }

        public static void AnimateSparkles(GameLocation targetLocation, Vector2 targetVector, Microsoft.Xna.Framework.Color animationColor) // DruidCastGrowth (0.8f, 1, 0.8f, 1)
        {

            Microsoft.Xna.Framework.Rectangle bounds = new((int)(targetVector.X * 64 - 64), (int)(targetVector.Y * 64 - 64), 192, 192);

            for (int i = 0; i < 5; i++)
            {
                int sparkleIndex = i % 2 == 0 ? 10 : 11;

                TemporaryAnimatedSprite sparkle = new(sparkleIndex, new Vector2(Game1.random.Next(bounds.X, bounds.Right), Game1.random.Next(bounds.Y, bounds.Bottom)), animationColor)
                {
                    delayBeforeAnimationStart = i*50,
                    texture = Game1.animations,
                };

                targetLocation.temporarySprites.Add(sparkle);

            }

            return;

        }

        public static void AnimateImpact(GameLocation targetLocation, Vector2 origin, float size, int frame = 0, string image = "Impact", float interval = 100f)
        {

            size = Math.Min(4, size);

            TemporaryAnimatedSprite bomb = new(0, interval, 8-frame, 1, new(origin.X - 32 - (32 * size), origin.Y - 48 - (32 * size)), false, false)
            {
                sourceRect = new(64*frame, 0, 64, 64),
                sourceRectStartingPos = new Vector2(64 * frame, 0.0f),
                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", image+".png")),
                scale = 2f + size,
                timeBasedMotion = true,
                layerDepth = 999f,
                rotationChange = 0.00628f,
                alpha = 0.35f,
                //alphaFade = 1f / 2000f
            };

            targetLocation.temporarySprites.Add(bomb);

            TemporaryAnimatedSprite light = new(23, 500f, 6, 1, origin, false, Game1.random.NextDouble() < 0.5)
            {
                texture = Game1.mouseCursors,
                light = true,
                lightRadius = 2 + size,
                lightcolor = Color.Black,
                alphaFade = 0.03f,
                Parent = targetLocation
            };

            targetLocation.temporarySprites.Add(light);

        }

        public static void AnimateSplash(GameLocation targetLocation, Vector2 targetVector, bool animationFlipped) // DruidCastSplash
        {

            int animationRow = 19;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 128, 128);

            float animationInterval = 150f;

            int animationLength = 4;

            int animationLoops = 1;

            Microsoft.Xna.Framework.Color animationColor = Microsoft.Xna.Framework.Color.White;

            Vector2 animationPosition = new(targetVector.X * 64, targetVector.Y * 64 - 64);

            float animationSort = (targetVector.Y / 10000);

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlipped, animationSort, 0.001f, animationColor, 0.75f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(newAnimation);

            return;

        }

        public static void AnimateBolt(GameLocation location, Vector2 origin, int playSound = 800)
        {

            Vector2 originOffset = new(origin.X - 64, origin.Y - 320);

            TemporaryAnimatedSprite boltAnimation = new(0, 75, 6, 1, originOffset, false, (Game1.random.NextDouble() < 0.5) ? true : false)
            {

                sourceRect = new(0, 0, 64, 128),

                sourceRectStartingPos = new Vector2(0, 0),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Bolt.png")),

                layerDepth = 999f,

                alpha = 0.65f,

                scale = 3f,

            };

            location.temporarySprites.Add(boltAnimation);

            Vector2 lightOffset = new(origin.X, origin.Y - 192);

            TemporaryAnimatedSprite lightAnimation = new(23, 500f, 1, 1, lightOffset, flicker: false, (Game1.random.NextDouble() < 0.5) ? true : false)
            {
                light = true,
                lightRadius = 6,
                lightcolor = Color.Black,
                alpha = 0.5f,
                alphaFade = 0.001f,
                Parent = location,

            };

            location.temporarySprites.Add(lightAnimation);

            if (playSound >= 500)
            {

                Game1.currentLocation.playSound("flameSpellHit", origin, 800);

            }

            return;

        }

        public static void AnimateFishJump(GameLocation targetLocation, Vector2 targetPosition, int fishIndex, bool fishDirection)
        {

            //if(!(Game1.viewport.X <= (targetVector.X*64)) || !(Game1.viewport.Y <= (targetVector.Y*64)))
            if (!Utility.isOnScreen(targetPosition, 128))
            {

                return;

            }

            Vector2 targetVector = targetPosition;

            targetLocation.playSound("pullItemFromWater");

            DelayedAction.functionAfterDelay(SoundSlosh, 900);

            Vector2 fishPosition;

            Vector2 sloshPosition;

            Vector2 splashPosition;

            Vector2 sloshMotion;

            Vector2 sloshAcceleration;

            bool fishFlip;

            float fishRotate;

            bool sloshFlip;

            switch (fishDirection)
            {

                case true:

                    fishPosition = new(targetVector.X - 64, targetVector.Y - 8);

                    sloshPosition = new(targetVector.X + 100, targetVector.Y);

                    splashPosition = new(targetVector.X - 128, targetVector.Y - 40);

                    sloshMotion = new(0.160f, -0.5f);

                    sloshAcceleration = new(0f, 0.001f);

                    fishFlip = false;

                    fishRotate = 0.03f;

                    sloshFlip = true;

                    break;

                default:

                    fishPosition = new(targetVector.X + 64, targetVector.Y - 8);

                    sloshPosition = new(targetVector.X - 128, targetVector.Y);

                    splashPosition = new(targetVector.X + 100, targetVector.Y - 40);

                    sloshMotion = new(-0.160f, -0.5f);

                    sloshAcceleration = new(0f, 0.001f);

                    fishFlip = true;

                    fishRotate = -0.03f;

                    sloshFlip = false;

                    break;


            }


            Microsoft.Xna.Framework.Rectangle targetRectangle = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, fishIndex, 16, 16);

            float animationInterval = 1050f;

            float animationSort = (targetVector.Y / 10000) + 0.00003f;

            TemporaryAnimatedSprite fishAnimation = new("Maps\\springobjects", targetRectangle, animationInterval, 1, 0, fishPosition, flicker: false, flipped: fishFlip, animationSort, 0f, Color.White, 3f, 0f, 0f, fishRotate)
            {

                motion = sloshMotion,

                acceleration = sloshAcceleration,

                timeBasedMotion = true,

            };

            targetLocation.temporarySprites.Add(fishAnimation);

            // ------------------------------------

            int animationRow = 19;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 128, 128);

            animationInterval = 150f;

            int animationLength = 4;

            int animationLoops = 1;

            Microsoft.Xna.Framework.Color animationColor = Microsoft.Xna.Framework.Color.White;

            animationSort = (targetVector.Y / 10000) + 0.00004f;

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, splashPosition, false, sloshFlip, animationSort, 0f, animationColor, 0.75f, 0f, 0.1f, 0f);

            targetLocation.temporarySprites.Add(newAnimation);

            // ------------------------------------

            TemporaryAnimatedSprite sloshAnimation = new(28, 200f, 2, 1, sloshPosition, flicker: false, flipped: false)
            {

                delayBeforeAnimationStart = 900,

            };

            targetLocation.temporarySprites.Add(sloshAnimation);

        }

        public static void AnimateRockfalls(GameLocation location, Vector2 vector)
        {
            Random random = new Random();
            for (int index1 = 0; index1 < 10; ++index1)
            {
                int index2 = index1 % 5;
                List<Vector2> tilesWithinRadius = GetTilesWithinRadius(location, vector, index2 + 2);
                if (random.Next(2) == 0)
                    tilesWithinRadius.Reverse();
                int count = tilesWithinRadius.Count;
                if (count != 0)
                {
                    int num1 = new List<int>() { 6, 8, 8, 7, 8 }[index2];
                    int num2 = new List<int>() { 2, 2, 3, 4, 4 }[index2];
                    for (int index3 = 0; index3 < num2; ++index3)
                    {
                        int minValue = num1 * index3;
                        if (minValue + 1 < count)
                        {
                            int castDelay = random.Next(3, 20) * 100;
                            int maxValue = Math.Min(minValue + num1, tilesWithinRadius.Count);
                            int index4 = random.Next(minValue, maxValue);
                            Vector2 vector2 = tilesWithinRadius[index4];
                            AnimateRockfall(location, vector2, castDelay);
                        }
                    }
                }
            }
        }

        public static void AnimateRockfall(GameLocation targetLocation, Vector2 targetVector, int castDelay, int objectIndex = -1, int scatterIndex = -1)
        {
            if (objectIndex == -1)
            {
                List<int> intList = SpawnData.RockFall(targetLocation, Game1.player);
                objectIndex = intList[0];
                scatterIndex = intList[1];
            }
            Microsoft.Xna.Framework.Rectangle standardTileSheet1 = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, objectIndex, 16, 16);
            Microsoft.Xna.Framework.Rectangle standardTileSheet2 = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, scatterIndex, 16, 16);
            float num1 = 575f;
            Vector2 vector2 = new((float)(targetVector.X * 64.0 + 8.0), (float)((targetVector.Y - 3.0) * 64.0 - 24.0));
            float num2 = 0.0015f;
            float num3 = (targetVector.Y / 10000) + 0.00001f;
            TemporaryAnimatedSprite temporaryAnimatedSprite1 = new TemporaryAnimatedSprite("Maps\\springobjects", standardTileSheet2, num1, 1, 0, vector2, false, false, num3, 1f / 1000f, Color.White, 3f, 0.0f, 0.0f, 0.0f, false)
            {
                acceleration = new Vector2(0.0f, num2),
                timeBasedMotion = true,
                delayBeforeAnimationStart = castDelay
            };
            targetLocation.temporarySprites.Add(temporaryAnimatedSprite1);

            vector2 = new((float)(targetVector.X * 64.0 + 8.0), (float)((targetVector.Y - 3.0) * 64.0 + 8.0));
            float num4 = (targetVector.Y / 10000) + 0.00002f;
            TemporaryAnimatedSprite temporaryAnimatedSprite2 = new TemporaryAnimatedSprite("Maps\\springobjects", standardTileSheet1, num1, 1, 0, vector2, false, false, num4, 1f / 1000f, Color.White, 3f, 0.0f, 0.0f, 0.0f, false)
            {
                acceleration = new Vector2(0.0f, num2),
                timeBasedMotion = true,
                delayBeforeAnimationStart = castDelay
            };
            targetLocation.temporarySprites.Add(temporaryAnimatedSprite2);

            vector2 = new((float)(targetVector.X * 64.0 + 16.0), (float)(targetVector.Y * 64.0 + 16.0));
            float num5 = (targetVector.Y / 10000) + 0.00003f;
            TemporaryAnimatedSprite temporaryAnimatedSprite3 = new TemporaryAnimatedSprite("Maps\\springobjects", standardTileSheet1, num1, 1, 0, vector2, false, false, num5, 1f / 1000f, Color.Black * 0.5f, 2f, 0.0f, 0.0f, 0.0f, false)
            {
                delayBeforeAnimationStart = castDelay
            };
            targetLocation.temporarySprites.Add(temporaryAnimatedSprite3);
        }

        public static void SoundSlosh()
        {
            Game1.currentLocation.localSound("quickSlosh");

        }

        public static void AnimateRandomCritter(GameLocation location, Vector2 vector)
        {

            Dictionary<int, List<int>> critterList = new()
            {
                // base frame, move frame, frame length, loops, rect x, rect y, columns, offset x 64, offset y 64, left/right, upper/lower
                [0] = new() { 286, 280, 5, 3, 16, 16, 20, -88, 16, 1, 1 },
                [1] = new() { 306, 300, 5, 3, 16, 16, 20, -88, 16, 1, 1 },
                [2] = new() { 68, 54, 6, 2, 32, 32, 10, -160, 16, 1, 1 },
                [3] = new() { 69, 74, 6, 2, 32, 32, 10, -160, 16, 1, 1 },
                [4] = new() { 61, 62, 6, 2, 32, 32, 10, -160, 16, 1, 1 },

            };
            Dictionary<int, List<int>> birdList = new()
            {
                [0] = new() { 31, 31, 3, 4, 32, 32, 10, 160, -144, -1, -1 },
                [1] = new() { 51, 31, 3, 4, 32, 32, 10, 160, -144, -1, -1 },
            };

            for (int i = 0; i < 5; i++)
            {
                List<int> critterConfig;

                if (i < 3)
                {

                    critterConfig = critterList[Game1.random.Next(critterList.Count)];

                }
                else
                {

                    critterConfig = birdList[Game1.random.Next(birdList.Count)];

                }

                int offset = i % 2;

                int baseColumn = critterConfig[1] % critterConfig[6];

                int baseRow = (critterConfig[1] - baseColumn) / critterConfig[6];

                Microsoft.Xna.Framework.Rectangle animationRectangle = new(baseColumn * critterConfig[4], baseRow * critterConfig[5], critterConfig[4], critterConfig[5]);

                float animationInterval = 80f;

                int animationLength = critterConfig[2];

                int animationLoops = critterConfig[3];

                float critterOffsetX = 32f * offset * critterConfig[9];

                float critterOffsetY = 32f * offset * critterConfig[10];

                Microsoft.Xna.Framework.Color animationColor = Microsoft.Xna.Framework.Color.White;

                Vector2 animationPosition = new(vector.X * 64 + critterConfig[7] + critterOffsetX, vector.Y * 64 + critterConfig[8] + critterOffsetY);

                float animationSort = (vector.Y / 10000) + 0.00001f;

                TemporaryAnimatedSprite critterAnimation = new("TileSheets\\critters", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, flicker: false, flipped: false, animationSort, 0.001f, animationColor, 3f, 0f, 0f, 0f)
                {
                    motion = new Vector2(critterConfig[4] / 80f * critterConfig[9], 0), // (float)critterConfig[4] / 80f * critterConfig[9]
                    timeBasedMotion = true,
                };

                location.temporarySprites.Add(critterAnimation);

                // ---------------------------- puff

                animationSort = (vector.Y / 10000) + 0.00002f;

                TemporaryAnimatedSprite boltCloud = new("TileSheets\\animations", new(128, 5 * 64, 64, 64), 333f, 3, 1, animationPosition, flicker: false, false, animationSort, 0.02f, Color.White, 0.5f, 0f, 0f, 0f);

                location.temporarySprites.Add(boltCloud);

            }

        }

        public static void AnimateButterflySpray(GameLocation location, Vector2 vector)
        {

            location.critters.Add(new Butterfly(location,vector, false));

            location.critters.Add(new Butterfly(location, vector - new Vector2(1, 0), false));

            location.critters.Add(new Butterfly(location, vector + new Vector2(1, 0), false));

            location.critters.Add(new Butterfly(location, vector - new Vector2(2, 0), false));

            location.critters.Add(new Butterfly(location, vector + new Vector2(2, 0), false));

        }

        public static void AnimateRandomFish(GameLocation location, Vector2 vector)
        {

            Vector2 position = vector * 64;

            List<int> fishIndexes = new() { 138, 146, 717, };

            int fishIndex = fishIndexes[Game1.random.Next(fishIndexes.Count)];

            AnimateFishJump(location, position - new Vector2(32, 64), fishIndex, true);

            AnimateFishJump(location, position + new Vector2(32, 64), fishIndex, false);

            AnimateFishJump(location, position - new Vector2(32, 0), fishIndex, true);

            AnimateFishJump(location, position + new Vector2(32, 0), fishIndex, false);

        }

        public static void AnimateDeathSpray(GameLocation location, Vector2 position, Color color)
        {

            Utility.makeTemporarySpriteJuicier(new TemporaryAnimatedSprite(45, position, color, 10), location);

            for (int i = 1; i < 3; i++)
            {
                location.temporarySprites.Add(new TemporaryAnimatedSprite(6, position + new Vector2(0f, 1f) * 64f * i, color * 0.75f, 10)
                {
                    delayBeforeAnimationStart = i * 159
                });
                location.temporarySprites.Add(new TemporaryAnimatedSprite(6, position + new Vector2(0f, -1f) * 64f * i, color * 0.75f, 10)
                {
                    delayBeforeAnimationStart = i * 159
                });
                location.temporarySprites.Add(new TemporaryAnimatedSprite(6, position + new Vector2(1f, 0f) * 64f * i, color * 0.75f, 10)
                {
                    delayBeforeAnimationStart = i * 159
                });
                location.temporarySprites.Add(new TemporaryAnimatedSprite(6, position + new Vector2(-1f, 0f) * 64f * i, color * 0.75f, 10)
                {
                    delayBeforeAnimationStart = i * 159
                });
            }

            location.localSound("shadowDie");

        }

        public static void AnimateFate(GameLocation targetLocation, Vector2 playerVector, Vector2 targetVector, int fuelSource = 768, bool fadeAway = false, bool usePosition = false)
        {

            Vector2 targetPosition;

            Vector2 playerPosition;

            if (usePosition)
            {

                targetPosition = targetVector;

                playerPosition = playerVector - new Vector2(0, 64);

            }
            else
            {

                targetPosition = new(targetVector.X * 64, targetVector.Y * 64);

                playerPosition = (playerVector * 64) - new Vector2(0, 64);

            }

            float xOffset = (targetPosition.X - playerPosition.X);

            float yOffset = (targetPosition.Y - playerPosition.Y);

            float motionX = xOffset / 1000;

            float compensate = 0.555f;

            float motionY = (yOffset / 1000) - compensate;

            float animationSort = (targetVector.Y / 10000) + 0.00001f;

            float animationFade = fadeAway ? 0.001f : 0f;

            Color tingleColor = (fuelSource == 768) ? Color.Yellow : Color.DarkBlue;

            Microsoft.Xna.Framework.Rectangle targetRectangle = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, fuelSource, 16, 16);

            TemporaryAnimatedSprite starAnimation = new("Maps\\springobjects", targetRectangle, 1000f, 1, 1, playerPosition, flicker: false, flipped: false, animationSort, animationFade, Color.White, 3f, 0f, 0f, 0.2f)
            {

                motion = new Vector2(motionX, motionY),

                acceleration = new Vector2(0f, 0.001f),

                timeBasedMotion = true,

            };

            targetLocation.temporarySprites.Add(starAnimation);

            animationSort = (targetVector.Y / 10000) + 0.00002f;

            TemporaryAnimatedSprite tingleAnimation = new("TileSheets\\animations", new(0, 11 * 64, 64, 64), 62.5f, 8, 2, playerPosition, false, false, animationSort, animationFade, tingleColor, 1f, 0f, 0f, 0.2f)
            {

                motion = new Vector2(motionX, motionY),

                acceleration = new Vector2(0f, 0.001f),

                timeBasedMotion = true,

                delayBeforeAnimationStart = 40,

            };

            targetLocation.temporarySprites.Add(tingleAnimation);

            TemporaryAnimatedSprite tingleAnimationTwo = new("TileSheets\\animations", new(0, 11 * 64, 64, 64), 62.5f, 8, 2, playerPosition, false, false, animationSort, animationFade, tingleColor, 0.75f, 0f, 0f, 0.2f)
            {

                motion = new Vector2(motionX, motionY),

                acceleration = new Vector2(0f, 0.001f),

                timeBasedMotion = true,

                delayBeforeAnimationStart = 80,

            };

            targetLocation.temporarySprites.Add(tingleAnimationTwo);

            TemporaryAnimatedSprite tingleAnimationThree = new("TileSheets\\animations", new(0, 11 * 64, 64, 64), 62.5f, 8, 2, playerPosition, false, false, animationSort, animationFade, tingleColor, 0.75f, 0f, 0f, 0.2f)
            {

                motion = new Vector2(motionX, motionY),

                acceleration = new Vector2(0f, 0.001f),

                timeBasedMotion = true,

                delayBeforeAnimationStart = 120,

            };

            targetLocation.temporarySprites.Add(tingleAnimationThree);

            //targetLocation.playSoundPitched("yoba",700);

        }

        //======================== Gameworld Interactions

        public static bool RandomTree(GameLocation targetLocation, Vector2 targetVector)
        {

            int treeIndex = Map.SpawnData.RandomTree(targetLocation);

            StardewValley.TerrainFeatures.Tree newTree = new(treeIndex.ToString(), 1);

            targetLocation.terrainFeatures.Add(targetVector, newTree);

            return true;

        }

        public static void UpgradeCrop(StardewValley.TerrainFeatures.HoeDirt hoeDirt, int targetX, int targetY, Farmer targetPlayer, GameLocation targetLocation, bool enableQuality)
        {

            int generateItem = 770;

            if (enableQuality)
            {

                Dictionary<int, int> objectIndexes = SpawnData.CropList(targetLocation);

                int chance = Game1.random.Next(objectIndexes.Count * 3);

                if (objectIndexes.ContainsKey(chance))
                {
                    generateItem = objectIndexes[chance];

                }

            }

            hoeDirt.destroyCrop(true);

            if (generateItem == 829)
            {

                StardewValley.Crop newGinger = new(true, "2", targetX, targetY, targetLocation);

                hoeDirt.crop = newGinger;

                targetLocation.playSound("dirtyHit");

                Game1.stats.SeedsSown++;

                return;

            }

            hoeDirt.plant(generateItem.ToString(),targetPlayer, false);

            //hoeDirt.crop.updateDrawMath(new Vector2(targetX, targetY));

        }

        public static bool GreetVillager(Farmer player, NPC villager, int friendShip = 0)
        {

            bool friendCheck = player.hasPlayerTalkedToNPC(villager.Name);

            Game1.player.checkForQuestComplete(villager, -1, -1, null, null, 5);

            if (!friendCheck && player.friendshipData.ContainsKey(villager.Name))
            {

                villager.faceTowardFarmerForPeriod(3000, 4, false, player);

                player.friendshipData[villager.Name].TalkedToToday = true;

                if (friendShip > 0)
                {

                    player.changeFriendship(friendShip, villager);

                }

                return true;

            }

            return false;

        }

        public static void UpdateFriendship(Farmer player, List<string> NPCIndex)
        {

            foreach (string NPCName in NPCIndex)
            {

                NPC characterFromName = Game1.getCharacterFromName(NPCName);

                characterFromName ??= Game1.getCharacterFromName<Child>(NPCName, mustBeVillager: false);

                if (characterFromName != null && player.friendshipData.ContainsKey(NPCName))
                {

                    player.changeFriendship(375, characterFromName);

                }

            }

        }

        public static void PetAnimal(Farmer targetPlayer, FarmAnimal targetAnimal)
        {

            if (targetAnimal.wasPet.Value)
            {

                return;

            }

            targetAnimal.wasPet.Value = true;

            int num = 7;

            if (targetAnimal.wasAutoPet.Value)
            {

                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, targetAnimal.friendshipTowardFarmer.Value + num);

            }
            else
            {

                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, targetAnimal.friendshipTowardFarmer.Value + 15);

            }

            FarmAnimalData animalData = targetAnimal.GetAnimalData();

            int num2 = animalData?.HappinessDrain ?? 0;

            if (animalData != null && animalData.ProfessionForHappinessBoost >= 0 && targetPlayer.professions.Contains(animalData.ProfessionForHappinessBoost))
            {

                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, targetAnimal.friendshipTowardFarmer.Value + 15);

                targetAnimal.happiness.Value = (byte)Math.Min(255, (int)targetAnimal.happiness.Value + Math.Max(5, 30 + num2));

            }

            targetAnimal.doEmote(20);

            targetAnimal.happiness.Value = (byte)Math.Min(255, (int)targetAnimal.happiness.Value + Math.Max(5, 30 + num2));

            targetAnimal.makeSound();

            targetPlayer.gainExperience(0, 5);

        }

        public static void LearnRecipe(Farmer targetPlayer)
        {

            List<string> recipeList = SpawnData.RecipeList();

            int learnedRecipes = 0;

            foreach (string recipe in recipeList)
            {

                if (!targetPlayer.cookingRecipes.ContainsKey(recipe))
                {

                    targetPlayer.cookingRecipes.Add(recipe, 0);

                    learnedRecipes++;

                }

            }

            if (learnedRecipes >= 1)
            {

                Game1.addHUDMessage(new HUDMessage($"Learned {learnedRecipes} recipes", 2));

            }

        }

        //======================== Tile Interactions

        public static bool WaterCheck(GameLocation targetLocation, Vector2 targetVector, int radius = 4)
        {
            bool check = true;

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            Tile backTile;

            for (int i = 0; i < radius; i++)
            {

                List<Vector2> neighbours = GetTilesWithinRadius(targetLocation, targetVector, i);

                foreach (Vector2 neighbour in neighbours)
                {

                    backTile = backLayer.PickTile(new xTile.Dimensions.Location((int)neighbour.X * 64, (int)neighbour.Y * 64), Game1.viewport.Size);

                    if (backTile != null)
                    {

                        if (!backTile.TileIndexProperties.TryGetValue("Water", out _))
                        {

                            check = false;
                        }

                    }

                }

            }

            return check;

        }

        public static string GroundCheck(GameLocation targetLocation, Vector2 neighbour, bool barrierCheck = false)
        {

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            Tile backTile;

            int mapWidth = targetLocation.Map.DisplayWidth / 64;

            int mapHeight = targetLocation.Map.DisplayHeight / 64;

            int targetX = (int)neighbour.X;

            int targetY = (int)neighbour.Y;

            string assumption = "unknown";

            if (targetX < 0 || targetX >= mapWidth || targetY < 0 || targetY >= mapHeight)
            {

                return "void";

            }

            backTile = backLayer.Tiles[targetX, targetY];

            if (backTile == null)
            {

                return "void";

            }

            if (backTile.TileIndex == 2154)
            {

                return "void";

            }

            if(!targetLocation.IsOutdoors)
            {

                Layer frontLayer = Game1.player.currentLocation.Map.GetLayer("Front");
                
                Tile frontTile = frontLayer.Tiles[targetX, targetY];

                if (frontTile != null)
                {
                    return "void";

                }

            }

            if (backTile.TileIndexProperties.TryGetValue("Water", out _))
            {

                return "water";

            }

            if (barrierCheck)
            {

                if (BarrierCheck(targetLocation, neighbour))
                {

                    return "barrier";

                }

                assumption = "ground";

            }

            PropertyValue backing = null;

            backTile.TileIndexProperties.TryGetValue("Type", out backing);

            if (backing != null)
            {

                return "ground";

            }

            if (targetLocation.IsOutdoors)
            {

                List<int> grounds = new() {304, 351, 404, 356, 300, 305, };

                if (grounds.Contains(backTile.TileIndex))
                {
                    return "ground";

                }

            }

            if(targetLocation is Caldera)
            {

                if(backTile.TileIndex == 28)
                {
                    return "ground";
                }

            }

            if (targetLocation is Sewer)
            {

                if (backTile.TileIndex == 34 || backTile.TileIndex == 41 || backTile.TileIndex == 42)
                {
                    return "ground";
                }

            }

            return assumption;

        }

        public static bool BarrierCheck(GameLocation targetLocation, Vector2 tileVector)
        {
            
            Layer backLayer = targetLocation.Map.GetLayer("Back");

            Layer buildingLayer = targetLocation.Map.GetLayer("Buildings");

            List<Vector2> checks = new()
            {

                tileVector,
                tileVector+ new Vector2(0,-1),
                tileVector+ new Vector2(1,0),
                tileVector+ new Vector2(0,1),
                tileVector+ new Vector2(-1,0),

            };

            Dictionary<int, bool> found = new();

            for(int i = 0; i <= 5; i++)
            {

                found[i] = false;

                int targetX = (int)checks[0].X;

                int targetY = (int)checks[0].Y;

                Tile backTile = backLayer.Tiles[targetX, targetY];

                Tile buildingTile = buildingLayer.Tiles[targetX, targetY];

                /*if (targetLocation is FarmCave)
                {
                    
                    if(targetX <= 3 || targetX >= targetLocation.map.Layers[0].LayerWidth - 4)
                    {

                        return true;

                    }

                    if (targetY <= 3 || targetY >= targetLocation.map.Layers[0].LayerHeight - 3)
                    {

                        return true;

                    }

                    if (buildingTile != null)
                    {

                         return true;

                    }

                }*/

                PropertyValue barrier = null;

                backTile.Properties.TryGetValue("NPCBarrier", out barrier);

                if (barrier != null) { if (i == 0) { return true; } found[i] = true; break; }

                backTile.Properties.TryGetValue("TemporaryBarrier", out barrier);

                if (barrier != null) { if (i == 0) { return true; } found[i] = true; break; }

                backTile.TileIndexProperties.TryGetValue("Passable", out barrier);

                if (barrier != null) { if (i == 0) { return true; } found[i] = true; break; }

                if (buildingLayer != null)
                {

                    if (buildingTile != null)
                    {

                        buildingTile.TileIndexProperties.TryGetValue("Shadow", out barrier);

                        if (barrier != null) { if (i == 0) { return true; } found[i] = true; break; }

                        buildingTile.TileIndexProperties.TryGetValue("Passable", out barrier);

                        if (barrier != null) { break; }

                        buildingTile.TileIndexProperties.TryGetValue("NPCPassable", out barrier);

                        if (barrier != null) { break; }

                        buildingTile.Properties.TryGetValue("Passable", out barrier);

                        if (barrier != null) { break; }

                        buildingTile.Properties.TryGetValue("NPCPassable", out barrier);

                        if (barrier != null) { break; }

                        if (i == 0) { return true; }

                        found[i] = true; break;

                    }

                }

            }

            if (found[1] && found[2])
            {
                //Mod.instance.Monitor.Log("corner " + checks[0].ToString() + " " + checks[1].ToString() + " " + checks[2].ToString(), LogLevel.Debug);
                return true;

            }
            
            if (found[2] && found[3]) {
                //Mod.instance.Monitor.Log("corner " + checks[0].ToString() + " " + checks[2].ToString() + " " + checks[3].ToString(), LogLevel.Debug);
                return true; 
            
            }

            if (found[3] && found[4])
            {
                //Mod.instance.Monitor.Log("corner " + checks[0].ToString() + " " + checks[3].ToString() + " " + checks[4].ToString(), LogLevel.Debug);
                return true;

            }

            if (found[4] && found[1])
            {
                //Mod.instance.Monitor.Log("corner " + checks[0].ToString() + " " + checks[4].ToString() + " " + checks[1].ToString(), LogLevel.Debug);
                return true;

            }

            return false;

        }

        public static Dictionary<Vector2, string> LocationTargets(GameLocation targetLocation)
        {

            Dictionary<Vector2, string> targetCasts = new();

            if (targetLocation.largeTerrainFeatures.Count > 0)
            {

                foreach (LargeTerrainFeature largeTerrainFeature in targetLocation.largeTerrainFeatures)
                {

                    if (largeTerrainFeature is not StardewValley.TerrainFeatures.Bush bushFeature)
                    {

                        continue;

                    }

                    Vector2 originVector = bushFeature.Tile;

                    targetCasts[originVector] = "Bush";

                    switch (bushFeature.size.Value)
                    {
                        case 0:
                        case 3:
                            targetCasts[new Vector2(originVector.X, originVector.Y + 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X, originVector.Y - 1)] = "Bush";
                            break;
                        case 1:
                        case 4:
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y)] = "Bush";
                            targetCasts[new Vector2(originVector.X, originVector.Y + 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y +1)] = "Bush";
                            targetCasts[new Vector2(originVector.X, originVector.Y -1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y -1)] = "Bush";
                            break;
                        case 2:
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 2, originVector.Y)] = "Bush";
                            targetCasts[new Vector2(originVector.X, originVector.Y + 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y +1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 2, originVector.Y +1)] = "Bush";
                            targetCasts[new Vector2(originVector.X, originVector.Y - 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y - 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 2, originVector.Y - 1)] = "Bush";
                            break;
                    }

                }

            }

            if (targetLocation.resourceClumps.Count > 0)
            {

                foreach (ResourceClump resourceClump in targetLocation.resourceClumps)
                {

                    Vector2 originVector = resourceClump.Tile;

                    targetCasts[originVector] = "Clump";

                    targetCasts[new Vector2(originVector.X + 1, originVector.Y)] = "Clump";

                    targetCasts[new Vector2(originVector.X, originVector.Y + 1)] = "Clump";

                    targetCasts[new Vector2(originVector.X + 1, originVector.Y + 1)] = "Clump";

                }

            }
            /*
            if (targetLocation is Woods woodsLocation)
            {
                foreach (ResourceClump resourceClump in woodsLocation.stumps)
                {

                    Vector2 originVector = resourceClump.Tile;

                    targetCasts[originVector] = "Clump";

                    targetCasts[new Vector2(originVector.X + 1, originVector.Y)] = "Clump";

                    targetCasts[new Vector2(originVector.X, originVector.Y + 1)] = "Clump";

                    targetCasts[new Vector2(originVector.X + 1, originVector.Y + 1)] = "Clump";

                }

            }*/

            if (targetLocation.furniture.Count > 0)
            {
                foreach (Furniture item in targetLocation.furniture)
                {

                    Vector2 originVector = item.TileLocation;

                    for (int i = 0; i < (item.boundingBox.Width / 64); i++)
                    {

                        for (int j = 0; j < (item.boundingBox.Height / 64); j++)
                        {

                            targetCasts[new Vector2(originVector.X + i, originVector.Y + j)] = "Furniture";

                        }

                    }

                }

            }
            foreach (Building building in targetLocation.buildings)
            {

                int radius = building.GetAdditionalTilePropertyRadius();

                int cornerX = building.tileX.Value - radius;

                int cornerY = building.tileY.Value - radius;

                int width = building.tilesWide.Value + (radius * 2);

                int height = building.tilesHigh.Value + (radius * 2);

                for (int i = 0; i < width; i++)
                {

                    for (int j = 0; j < height; j++)
                    {

                        targetCasts[new Vector2(cornerX + i, cornerY + j)] = "Building";

                    }

                }

            }

            return targetCasts;

        }

        public static Dictionary<string, List<Vector2>> NeighbourCheck(GameLocation targetLocation, Vector2 targetVector, int targetRadius = 1)
        {

            Dictionary<string, List<Vector2>> neighbourList = new();

            List<Vector2> neighbourVectors = GetTilesWithinRadius(targetLocation, targetVector, targetRadius);

            Layer buildingLayer = targetLocation.Map.GetLayer("Buildings");

            Layer pathsLayer = targetLocation.Map.GetLayer("Paths");

            if (!Mod.instance.targetCasts.ContainsKey(targetLocation.Name))
            {

                Mod.instance.targetCasts[targetLocation.Name] = LocationTargets(targetLocation);

            }

            foreach (Vector2 neighbourVector in neighbourVectors)
            {

                if (Mod.instance.targetCasts[targetLocation.Name].ContainsKey(neighbourVector))
                {

                    string targetType = Mod.instance.targetCasts[targetLocation.Name][neighbourVector];

                    if (!neighbourList.ContainsKey(targetType))
                    {

                        neighbourList[targetType] = new();

                    }

                    neighbourList[targetType].Add(neighbourVector);

                    continue;

                }

                Tile buildingTile = buildingLayer.PickTile(new xTile.Dimensions.Location((int)neighbourVector.X * 64, (int)neighbourVector.Y * 64), Game1.viewport.Size);

                if (buildingTile != null)
                {

                    if (buildingTile.TileIndexProperties.TryGetValue("Passable", out _) == false)
                    {

                        if (!neighbourList.ContainsKey("Wall"))
                        {

                            neighbourList["Wall"] = new();

                        }

                        neighbourList["Wall"].Add(neighbourVector);

                        continue;

                    }

                    if (targetLocation is Beach)
                    {
                        
                        List<int> tidalList = new() { 60, 61, 62, 63, 77, 78, 79, 80, 94, 95, 96, 97, 104, 287, 288, 304, 305, 321, 362, 363 };

                        if (tidalList.Contains(buildingTile.TileIndex))
                        {

                            neighbourList["Pool"].Add(neighbourVector);

                            continue;

                        }

                    }

                }

                if (pathsLayer != null)
                {

                    Tile pathsTile = buildingLayer.PickTile(new xTile.Dimensions.Location((int)neighbourVector.X * 64, (int)neighbourVector.Y * 64), Game1.viewport.Size);

                    if (pathsTile != null)
                    {

                        if (!neighbourList.ContainsKey("Path"))
                        {

                            neighbourList["Path"] = new();

                        }

                        neighbourList["Path"].Add(neighbourVector);

                    }

                }

                if (targetLocation.terrainFeatures.ContainsKey(neighbourVector))
                {
                    var terrainFeature = targetLocation.terrainFeatures[neighbourVector];

                    switch (terrainFeature.GetType().Name.ToString())
                    {

                        case "FruitTree":

                            if (!neighbourList.ContainsKey("Sapling"))
                            {

                                neighbourList["Sapling"] = new();

                            }

                            neighbourList["Sapling"].Add(neighbourVector);

                            break;

                        case "Tree":

                            StardewValley.TerrainFeatures.Tree treeCheck = terrainFeature as StardewValley.TerrainFeatures.Tree;

                            if (treeCheck.growthStage.Value >= 5)
                            {

                                if (!neighbourList.ContainsKey("Tree"))
                                {

                                    neighbourList["Tree"] = new();

                                }

                                neighbourList["Tree"].Add(neighbourVector);

                            }
                            else
                            {

                                if (!neighbourList.ContainsKey("Sapling"))
                                {

                                    neighbourList["Sapling"] = new();

                                }

                                neighbourList["Sapling"].Add(neighbourVector);

                            }

                            break;

                        case "HoeDirt":

                            StardewValley.TerrainFeatures.HoeDirt hoedCheck = terrainFeature as StardewValley.TerrainFeatures.HoeDirt;

                            if (hoedCheck.crop != null)
                            {

                                if (!neighbourList.ContainsKey("Crop"))
                                {

                                    neighbourList["Crop"] = new();

                                }

                                neighbourList["Crop"].Add(neighbourVector);

                            }

                            if (!neighbourList.ContainsKey("HoeDirt"))
                            {

                                neighbourList["HoeDirt"] = new();

                            }

                            neighbourList["HoeDirt"].Add(neighbourVector);

                            break;

                        default:

                            if (!neighbourList.ContainsKey("Feature"))
                            {

                                neighbourList["Feature"] = new();

                            }

                            neighbourList["Feature"].Add(neighbourVector);

                            break;

                    }

                    continue;

                }

                if (targetLocation.objects.ContainsKey(neighbourVector))
                {

                    if (!neighbourList.ContainsKey("Object"))
                    {

                        neighbourList["Object"] = new();

                    }

                    neighbourList["Object"].Add(neighbourVector);

                    if (targetLocation.objects[neighbourVector] is StardewValley.Fence || targetLocation.objects[neighbourVector] is StardewValley.Objects.BreakableContainer || targetLocation.objects[neighbourVector].bigCraftable.Value)
                    {

                        if (!neighbourList.ContainsKey("BigObject"))
                        {

                            neighbourList["BigObject"] = new();

                        }

                        neighbourList["BigObject"].Add(neighbourVector);

                    }

                }

            }

            return neighbourList;

        }

        static List<Vector2> TilesWithinOne(Vector2 center)
        {
            
            List<Vector2> result = new() {

                center + new Vector2(0, -1),    // N
                center + new Vector2(1, -1),    // NE
                center + new Vector2(1, 0),     // E
                center + new Vector2(1, 1),     // SE
                center + new Vector2(0, 1),     // S
                center + new Vector2(-1, 1),    // SW
                center + new Vector2(-1, 0),    // W
                center + new Vector2(-1, -1),   // NW

            };

            return result;

        }

        static List<Vector2> TilesWithinTwo(Vector2 center)
        {
            List<Vector2> result = new()
            {

                center + new Vector2(0,-2), // N
                center + new Vector2(1,-2), // NE

                center + new Vector2(2,-1), // NE
                center + new Vector2(2,0), // E
                center + new Vector2(2,1), // SE

                center + new Vector2(1,2), // SE
                center + new Vector2(0,2), // S
                center + new Vector2(-1,2), // SW

                center + new Vector2(-2,1), // SW
                center + new Vector2(-2,0), // W
                center + new Vector2(-2,-1), // NW

                 center + new Vector2(-1,-2), // NW

            };

            return result;

        }

        static List<Vector2> TilesWithinThree(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-3), // N
                center + new Vector2(1,-3),

                center + new Vector2(2,-2), // NE

                center + new Vector2(3,-1), // E
                center + new Vector2(3,0),
                center + new Vector2(3,1),

                center + new Vector2(2,2), // SE

                center + new Vector2(1,3), // S
                center + new Vector2(0,3),
                center + new Vector2(-1,3),

                center + new Vector2(-2,2), // SW

                center + new Vector2(-3,1), // W
                center + new Vector2(-3,0),
                center + new Vector2(-3,-1),

                center + new Vector2(-2,-2), // NW

                center + new Vector2(-1,-3), // NNW
 
            };

            return result;

        }

        static List<Vector2> TilesWithinFour(Vector2 center)
        {
            List<Vector2> result = new() {


                center + new Vector2(0,-4), // N
                center + new Vector2(1,-4),

                center + new Vector2(2,-3),
                center + new Vector2(3,-3), // NE
                center + new Vector2(3,-2),

                center + new Vector2(4,-1), // E
                center + new Vector2(4,0),
                center + new Vector2(4,1),

                center + new Vector2(3,2),
                center + new Vector2(3,3), // SE
                center + new Vector2(2,3),

                center + new Vector2(1,4), // S
                center + new Vector2(0,4),
                center + new Vector2(-1,4),

                center + new Vector2(-2,3),
                center + new Vector2(-3,3), // SW
                center + new Vector2(-3,2),

                center + new Vector2(-4,1), // W
                center + new Vector2(-4,0),
                center + new Vector2(-4,-1),

                center + new Vector2(-3,-2),
                center + new Vector2(-3,-3), // NW
                center + new Vector2(-2,-3),

                center + new Vector2(-1,-4), // NNW

            };

            return result;

        }

        static List<Vector2> TilesWithinFive(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-5), // N
                center + new Vector2(1,-5),

                center + new Vector2(2,-4), // NE
                center + new Vector2(3,-4),
                center + new Vector2(4,-3),
                center + new Vector2(4,-2),

                center + new Vector2(5,-1), // E
                center + new Vector2(5,0),
                center + new Vector2(5,1),

                center + new Vector2(4,2), // SE
                center + new Vector2(4,3),
                center + new Vector2(3,4),
                center + new Vector2(2,4),

                center + new Vector2(1,5), // S
                center + new Vector2(0,5),
                center + new Vector2(-1,5),

                center + new Vector2(-2,4), // SW
                center + new Vector2(-3,4),
                center + new Vector2(-4,3),
                center + new Vector2(-4,2),

                center + new Vector2(-5,1), // W
                center + new Vector2(-5,0),
                center + new Vector2(-5,-1),

                center + new Vector2(-4,-2), // NW
                center + new Vector2(-4,-3),
                center + new Vector2(-3,-4),
                center + new Vector2(-2,-4),

                center + new Vector2(-1,-5), // NNW

            };

            return result;

        }

        static List<Vector2> TilesWithinSix(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-6), // N
                center + new Vector2(1,-6),

                center + new Vector2(2,-5),
                center + new Vector2(3,-5),
                center + new Vector2(4,-4), // NE
                center + new Vector2(5,-3),
                center + new Vector2(5,-2),

                center + new Vector2(6,-1),
                center + new Vector2(6,0), // E
                center + new Vector2(6,1),

                center + new Vector2(5,2),
                center + new Vector2(5,3),
                center + new Vector2(4,4), // SE
                center + new Vector2(3,5),
                center + new Vector2(2,5),

                center + new Vector2(1,6),
                center + new Vector2(0,6), // S
                center + new Vector2(-1,6),

                center + new Vector2(-2,5),
                center + new Vector2(-3,5),
                center + new Vector2(-4,4), // SW
                center + new Vector2(-5,3),
                center + new Vector2(-5,2),

                center + new Vector2(-6,-1),
                center + new Vector2(-6,0), // W
                center + new Vector2(-6,1),

                center + new Vector2(-5,-2),
                center + new Vector2(-5,-3),
                center + new Vector2(-4,-4), // NW
                center + new Vector2(-3,-5),
                center + new Vector2(-2,-5),

                center + new Vector2(-1,-6), // NNW

            };

            return result;

        }

        static List<Vector2> TilesWithinSeven(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-7), // N
                center + new Vector2(1,-7),

                center + new Vector2(2,-6),
                //center + new Vector2(3,-6),

                center + new Vector2(4,-5), // NE
                center + new Vector2(5,-4), // NE

                //center + new Vector2(6,-3),
                center + new Vector2(6,-2),

                center + new Vector2(7,-1),
                center + new Vector2(7,0), // E
                center + new Vector2(7,1),

                center + new Vector2(6,2),
                //center + new Vector2(6,3),

                center + new Vector2(5,4), // SE
                center + new Vector2(4,5), // SE

                //center + new Vector2(3,6),
                center + new Vector2(2,6),

                center + new Vector2(1,7),
                center + new Vector2(0,7), // S
                center + new Vector2(-1,7),

                center + new Vector2(-2,6),
                //center + new Vector2(-3,6),

                center + new Vector2(-4,5), // SW
                center + new Vector2(-5,4), // SW

                //center + new Vector2(-6,3),
                center + new Vector2(-6,2),

                center + new Vector2(-7,-1),
                center + new Vector2(-7,0), // W
                center + new Vector2(-7,1),

                center + new Vector2(-6,-2),
                //center + new Vector2(-6,-3),

                center + new Vector2(-5,-4), // NW
                center + new Vector2(-4,-5), // NW

                //center + new Vector2(-3,-6),
                center + new Vector2(-2,-6),

                center + new Vector2(-1,-7), // NNW

            };

            return result;

        }

        static List<Vector2> TilesWithinEight(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-8),

                center + new Vector2(2,-7),
                center + new Vector2(3,-6),

                center + new Vector2(4,-6),
                center + new Vector2(5,-5), // NE
                center + new Vector2(6,-4),

                center + new Vector2(6,-3),
                center + new Vector2(7,-2),

                center + new Vector2(8,0),

                center + new Vector2(7,2),
                center + new Vector2(6,3),

                center + new Vector2(6,4),
                center + new Vector2(5,5), // SE
                center + new Vector2(4,6),

                center + new Vector2(3,6),
                center + new Vector2(2,7),

                center + new Vector2(0,-8),

                center + new Vector2(-2,7),
                center + new Vector2(-3,6),

                center + new Vector2(-4,6),
                center + new Vector2(-5,5), // SW
                center + new Vector2(-6,4),

                center + new Vector2(-6,3),
                center + new Vector2(-7,2),

                center + new Vector2(-8,0),

                center + new Vector2(-7,-2),
                center + new Vector2(-6,-3),

                center + new Vector2(-6,-4),
                center + new Vector2(-5,-5), // NW
                center + new Vector2(-4,-6),

                center + new Vector2(-3,-6),
                center + new Vector2(-2,-7),

            };

            return result;

        }

        public static List<Vector2> GetTilesWithinRadius(GameLocation location, Vector2 center, int level, bool onmap = true, int segment = -1)
        {

            List<Vector2> templateList;

            switch (level)
            {
                case 1:
                    templateList = TilesWithinOne(center);
                    break;
                case 2:
                    templateList = TilesWithinTwo(center);
                    break;
                case 3:
                    templateList = TilesWithinThree(center);
                    break;
                case 4:
                    templateList = TilesWithinFour(center);
                    break;
                case 5:
                    templateList = TilesWithinFive(center);
                    break;
                case 6:
                    templateList = TilesWithinSix(center);
                    break;
                case 7:
                    templateList = TilesWithinSeven(center);
                    break;
                case 8:
                    templateList = TilesWithinEight(center);
                    break;
                default: // 0
                    templateList = new() { center, };
                    break;

            }

            if (segment != -1)
            {

                List<Vector2> segmentList = new();

                int total = templateList.Count;

                float segmentLength = total / 8;

                int segmentGrab = (int)Math.Ceiling((double)(total / 3)) -1;

                int segmentStart = (int)(segmentLength * segment);

                segmentList.Add(templateList[segmentStart]);

                int segmentIndex;

                int j = 1;

                for (int i = 0; i < segmentGrab; i++)
                {

                    if(i % 2 == 0)
                    {

                        segmentIndex = (segmentStart + j + total) % total;

                    }
                    else
                    {

                        segmentIndex = (segmentStart + j + total) % total;

                        j++;

                    }

                    segmentList.Add(templateList[segmentIndex]);

                }

                templateList = segmentList;

            }

            if (!onmap)
            {

                return templateList;


            }

            List<Vector2> vectorList = new();

            foreach (Vector2 testVector in templateList)
            {

                if (location.isTileOnMap(testVector))
                {
                    
                    vectorList.Add(testVector);

                }

            }

            return vectorList;

        }

        public static List<Vector2> GetTilesBetweenPositions(GameLocation location, Vector2 distant, Vector2 near, float limit = -1)
        {
            
            List<Vector2> vectorList = new();

            float increment = limit * 2;

            if(limit == -1)
            {

                increment = Vector2.Distance(distant,near) / 32;

            }
            
            Vector2 factor = PathFactor(near,distant) * 32;

            Vector2 check = near + factor;

            for(int i = 1; i <= increment; i++)
            {

                check += factor;

                Vector2 tile = new((int)(check.X / 64), (int)check.Y / 64);

                if (!vectorList.Contains(tile) && tile != near && tile != distant)
                {
                    vectorList.Add(tile);
                }

            }

            return vectorList;

        }

        public static List<Vector2> GetOccupiableTilesNearby(GameLocation location, Vector2 target, int direction = 0, int proximity = 0, int margin = 0)
        {

            List<Vector2> occupiable = new();

            for(int i = 0; i < margin + 1; i++)
            {

                List<Vector2> leads = GetTilesWithinRadius(location, target, i + proximity, true, direction);

                foreach(Vector2 lead in leads)
                {

                    if(TileAccessibility(location,lead) == 0)
                    {

                        occupiable.Add(lead);

                    }

                }


            }

            return occupiable;
        
        }

        public static List<Vector2> GetOccupiedTilesWithinRadius(GameLocation location, Vector2 center, int level)
        {

            List<Vector2> occupied = new();

            for (int i = 0; i < level; i++)
            {

                List<Vector2> leads = GetTilesWithinRadius(location, center, i + 1, true);

                foreach (Vector2 lead in leads)
                {

                    if (TileAccessibility(location, lead) != 0)
                    {

                        occupied.Add(lead);

                    }

                }

            }

            return occupied;

        }

        public static List<int> DirectionToCenter(GameLocation location, Vector2 position)
        {

            int layerWidth = location.map.Layers[0].LayerWidth;

            int layerHeight = location.map.Layers[0].LayerHeight;

            int midWidth = (layerWidth / 2) * 64;

            int midHeight = (layerHeight / 2) * 64;

            Vector2 centerPosition = new(midWidth, midHeight);

            List<int> directions = DirectionToTarget(position, centerPosition);

            return directions;

        }

        public static List<int> DirectionToTarget(Vector2 origin, Vector2 target)
        {

            List<int> directions = new();

            int moveDirection;

            int altDirection;

            int diagonal;

            Vector2 difference = new(target.X - origin.X, target.Y - origin.Y);

            float absoluteX = Math.Abs(difference.X);

            float absoluteY = Math.Abs(difference.Y);

            int signX = difference.X < 0.001f ? -1 : 1;

            int signY = difference.Y < 0.001f ? -1 : 1;

            if (absoluteX > absoluteY)
            {

                moveDirection = 2 - signX;

                altDirection = 1 + signY;

            }
            else
            {
                moveDirection = 1 + signY;

                altDirection = 2 - signX;

            }

            double radian = Math.Abs(Math.Atan2(difference.Y, difference.X));


            double pie = Math.PI / 8;

            if (signY == 1)
            {

                diagonal = 6;

                if (radian < pie)
                {

                    diagonal = 2;

                }
                else if (radian < pie * 3)
                {

                    diagonal = 3;

                }
                else if (radian < pie * 5)
                {

                    diagonal = 4;

                }
                else if (radian < pie * 7)
                {

                    diagonal = 5;

                }

            }
            else
            {

                diagonal = 6;

                if (radian < pie)
                {

                    diagonal = 2;

                }
                else if (radian < pie * 3)
                {

                    diagonal = 1;

                }
                else if (radian < pie * 5)
                {

                    diagonal = 0;

                }
                else if (radian < pie * 7)
                {

                    diagonal = 7;

                }

            }

            //Mod.instance.Monitor.Log(origin.ToString() + " " + target.ToString() + " " + moveDirection.ToString() + " " + altDirection.ToString() + " " + radian.ToString() + " " + diagonal.ToString(), LogLevel.Debug);
            directions.Add(moveDirection);

            directions.Add(altDirection);

            directions.Add(diagonal);

            return directions;

        }

        public static Vector2 DirectionAsVector(int direction)
        {

            Vector2 move = new(0, -1);

            switch (direction)
            {

                case 1: move = new(1, -1);  break;

                case 2: move = new(1, 0); break;

                case 3: move = new(1, 1); break;
                
                case 4: move = new(0, 1); break;
                
                case 5: move = new(-1, 1); break;
                
                case 6: move = new(-1, 0); break;
                
                case 7: move = new(-1, -1); break;

            }

            return move;

        }

        public static Vector2 PathMovement(Vector2 origin, Vector2 destination, float speed)
        {

            if (Vector2.Distance(destination, origin) < (speed * 1.5))
            {

                return destination;

            }

            Vector2 factor = PathFactor(origin, destination);

            return origin + (factor * speed);

        }

        public static Vector2 PathFactor(Vector2 origin, Vector2 destination)
        {

            Vector2 difference = destination - origin;

            float absX = Math.Abs(difference.X); // x position

            float absY = Math.Abs(difference.Y); // y position

            float moveX = difference.X > 0f ? 1 : -1; // x sign

            float moveY = difference.Y > 0f ? 1 : -1; // y sign

            if (destination.X == origin.X)
            {

                moveX = 0;

            }
            else if (destination.Y == origin.Y)
            {

                moveY = 0;

            }
            else if (absY > absX)
            {

                moveX *= (absX / absY);

            }
            else
            {
                moveY *= (absY / absX);

            }

            Vector2 factor = new(moveX, moveY);

            return factor;

        }

        /*public static List<Vector2> GetOpenSet(GameLocation location, List<Vector2> closed, Vector2 path, Vector2 target, int bias = -1)
        {

            List<Vector2> set = new();

            List<Vector2> open = TilesWithinOne(path);

            List<int> directions = DirectionToTarget(path * 64, target * 64);

            int direction = directions[2];

            int even = 1;

            int odd = -1;

            if((bias == -1 && new Random().Next(2) == 0) || bias >= 2)
            {
                even = -1;

                odd = 1;

            }

            if (!closed.Contains(open[direction]))
            {

                set.Add(open[direction]);

            }

            List<int> series = new()
            {
                (direction + 8 + even) % 8,
                (direction + 8 + odd ) % 8,
                (direction + 8 + even * 2) % 8,
                (direction + 8 + odd * 2) % 8,
            };

            foreach (int next in series)
            {

                if (!closed.Contains(open[next]))
                {

                    set.Add(open[next]);

                }

            }

            return set;

        }

        public static Vector2 PathMovement(Vector2 origin, Vector2 destination, float speed)
        {

            Vector2 difference = destination - origin;

            if(Vector2.Distance(destination,origin) < (speed * 1.5))
            {

                return destination;

            }

            float absX = Math.Abs(difference.X); // x position

            float absY = Math.Abs(difference.Y); // y position

            float moveX = difference.X > 0f ? 1 : -1; // x sign

            float moveY = difference.Y > 0f ? 1 : -1; // y sign

            if (destination.X == origin.X)
            {

                moveX = 0;

            } 
            else if (destination.Y == origin.Y)
            {

                moveY = 0;

            }
            else if (absY > absX)
            {

                moveX *= (absX / absY);

            }
            else
            {
                moveY *= (absY / absX);

            }

            Vector2 factor = new(moveX, moveY);

            return origin + factor * speed;


            /*List<int> directions = DirectionToTarget(origin, destination);

            int direction = directions[2];

            Vector2 adjust = new((int)(origin.X / 64), (int)(origin.X / 64));

            adjust = adjust * 64;

            switch (direction)
            {
                case 0:

                    adjust.X += 32;

                    break;

                case 1:

                    adjust.X += 64;

                    break;

                case 2:

                    adjust.X += 64;

                    adjust.Y += 32;

                    break;

                case 3:

                    adjust.X += 64;

                    adjust.Y += 64;

                    break;

                case 4:

                    adjust.X += 32;

                    adjust.Y += 64;

                    break;

                case 5:

                    adjust.Y += 64;

                    break;

                case 6:

                    adjust.Y += 32;

                break;

            }

            Vector2 diff = adjust - origin;

            float adjustment = Vector2.Distance(adjust, origin);

            movement = diff / ( adjustment / speed );

            return movement;

        }*/

        public static void HighlightTile(GameLocation location, Vector2 tile, Color colour)
        {
            
            if (!Mod.instance.highlights.ContainsKey(location.Name))
            {

                Mod.instance.highlights[location.Name] = new();

            }
            
            if (Mod.instance.highlights[location.Name].ContainsKey(tile)) { 
                
                return; 
            
            }

            TemporaryAnimatedSprite radiusAnimation = new(0, 99999, 1, 1, tile*64, false, false)
            {

                sourceRect = new(0,0,32,32),

                sourceRectStartingPos = new Vector2(0, 0),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Target.png")),

                scale = 2f,

                layerDepth = 999f,

                color = colour,

            };

            location.temporarySprites.Add(radiusAnimation);

            Mod.instance.highlights[location.Name][tile] = Game1.currentGameTime.TotalGameTime.TotalMinutes;

        }

        public static int TileAccessibility(GameLocation location, Vector2 check)
        {

            Dictionary<string, List<Vector2>> ObjectCheck = NeighbourCheck(location, check, 0);

            if (ObjectCheck.ContainsKey("Building") || ObjectCheck.ContainsKey("Wall"))
            {
                //HighlightTile(location, check, Color.Red);
                return 2;

            }

            if (ObjectCheck.ContainsKey("BigObject") || ObjectCheck.ContainsKey("Fence"))
            {
                //HighlightTile(location, check, Color.Blue);
                return 1;
      
            }

            string groundCheck = GroundCheck(location, check, true);

            if (groundCheck == "ground")
            {
                //HighlightTile(location, check, Color.Green);
                return 0;

            }

            if (groundCheck == "water")
            {
                //HighlightTile(location, check, Color.Blue);
                return 1; 

            }

            //HighlightTile(location, check, Color.Red);
            return 2;

        }

        /*public static List<Vector2> PathWalkToward(GameLocation location, List<Vector2> closed, Vector2 Position, Vector2 targetPosition)
        {

            Vector2 current = new((int)(Position.X / 64), (int)(Position.Y / 64));

            Vector2 target = new((int)(targetPosition.X / 64), (int)(targetPosition.Y / 64));

            Vector2 check;

            List<Vector2> nodes = GetOpenSet(location, closed, current, target);

            List<Vector2> leads = new();

            Dictionary<Vector2, List<Vector2>> paths = new() {};

            Vector2 branch = current;

            paths.Add(branch, new());

            for (int i = 0; i < 50; i++)
            {

                if (nodes.Count == 0)
                {

                    if (leads.Count > 0)
                    {

                        branch = leads.First();

                        nodes = GetOpenSet(location, closed, branch, target);

                        leads.RemoveAt(0);

                    }

                }

                if (nodes.Count > 0)
                {

                    check = nodes.Last();

                    nodes.RemoveAt(nodes.Count-1);

                    if(check == target)
                    {

                        float factor = 1;

                        foreach(Vector2 v in paths[branch])
                        {

                            HighlightTile(location, check, Color.Blue*factor);

                            factor -= 0.05f;

                        }

                        foreach (Vector2 v in closed)
                        {

                            HighlightTile(location, check, Color.Red);

                        }

                        return paths[branch];

                    }

                }
                else
                {

                    return new();
                    
                }

                if (closed.Contains(check))
                {

                    continue;

                }

                closed.Add(check);

                Dictionary<string, List<Vector2>> ObjectCheck = NeighbourCheck(location, check, 0);

                if (ObjectCheck.ContainsKey("Building") || ObjectCheck.ContainsKey("Wall") || ObjectCheck.ContainsKey("BigObject") || ObjectCheck.ContainsKey("Fence"))
                {

                    continue;

                }

                string groundCheck = GroundCheck(location, check, true);

                if (groundCheck == "ground")
                {

                    leads.Prepend(check);

                    List<Vector2> path = paths[branch];

                    path.Add(check);

                    paths.Add(check, path);

                }

                continue;

            }

            return new();

        }

        public static List<Vector2> PathJumpToward(GameLocation location, Vector2 Position, Vector2 targetPosition, float threshold)
        {

            List<Vector2> straights = GetTilesBetweenVectors(location, targetPosition, Position, 6);

            List<Vector2> jumps = new();

            for(int i = 0; i < straights.Count; i++)
            {

                Vector2 check = straights[i];

                Dictionary<string, List<Vector2>> ObjectCheck = NeighbourCheck(location, check, 0);

                if (ObjectCheck.ContainsKey("Building") || ObjectCheck.ContainsKey("Wall"))
                {

                    return jumps;

                }

                string groundCheck = GroundCheck(location, check, true);

                if(groundCheck == "ground")
                {

                    jumps.Add(check);

                }

                if(groundCheck != "water")
                {

                    return jumps;

                }

            }

            return jumps;

        }*/

        // ===================================================
        // GAME WORLD INTERACTIONS
        // ===================================================

        public static float CalculateCritical(float damage, float critChance = 0.1f, float critModifier = 2f)
        {

            Random random = new();

            if (Game1.player.professions.Contains(25))
            {

                critChance += 0.15f;

            }

            if((float)random.NextDouble() > critChance)
            {

                return -1;

            }

            if (Game1.player.professions.Contains(29))
            {
                critModifier += 1;

            }

            damage *= critModifier;

            return damage;

        }

        public static List<int> CalculatePush(GameLocation location, StardewValley.Monsters.Monster monster, Vector2 from, int range = 128)
        {

            List<int> pushList = new() {  0, 0 };

            if (!monster.isGlider.Value && !MonsterData.BossMonster(monster) && monster.Slipperiness != -1)
            {
                float num1 = monster.Position.X - from.X;

                float num2 = monster.Position.Y - from.Y;

                int diffX;

                int diffY;

                int num3 = 1;
                
                int num4 = 1;
                
                if ((double)num2 < 0.0)
                {
                    num3 = -1;
                }

                if ((double)num1 < 0.0)
                {
                    num4 = -1;
                }

                if ((double)Math.Abs(num1) < (double)Math.Abs(num2))
                {
                    float num5 = Math.Abs(num1) / Math.Abs(num2);

                    diffX = (int)(range * num4 * (double)num5);

                    diffY = range * num3;

                }
                else
                {
                    float num6 = Math.Abs(num2) / Math.Abs(num1);

                    diffX = range * num4;

                    diffY = (int)(range * num3 * (double)num6);

                }

                pushList[0] = diffX;

                pushList[1] = diffY;

                monster.stunTime.Set(Math.Max(monster.stunTime.Value,range));

            }

            return pushList;

        }

        public static List<Farmer> FarmerProximity(GameLocation targetLocation, List<Vector2> targetPosition, float radius, bool checkInvincible = false)
        {

            Dictionary<Farmer, float> farmerList = new();

            float impact = radius * 64;

            impact += 32;

            impact = Math.Max(64, impact);

            foreach (Farmer farmer in Game1.getAllFarmers())
            {

                if(checkInvincible && farmer.temporarilyInvincible)
                {
                    
                    continue;

                }

                if (farmer.currentLocation.Name == targetLocation.Name)
                {

                    float distance = Proximation(farmer.Position, targetPosition, impact);

                    if (distance != -1)
                    {

                        farmerList.Add(farmer,distance);

                    }

                }

            }

            List<Farmer> ordered = new();

            foreach(KeyValuePair<Farmer,float> kvp in farmerList.OrderBy(key => key.Value))
            {

                ordered.Add(kvp.Key);

            }

            return ordered;

        }

        public static void DamageFarmers(GameLocation targetLocation, List<Farmer> farmers, int damage, StardewValley.Monsters.Monster monster, bool parry = false)
        {

            if(farmers.Count == 0)
            {
                return;
            }

            foreach (Farmer farmer in farmers)
            {

                if ((farmer.health + farmer.buffs.Defense) - damage < 10)
                {

                    Mod.instance.CriticalCondition();

                    break;

                }

                farmer.takeDamage(damage, parry, monster);

            }

        }

        public static List<StardewValley.Monsters.Monster> MonsterProximity(GameLocation targetLocation, List<Vector2> targetPosition, float radius, bool checkInvincible = false)
        {

            Dictionary<StardewValley.Monsters.Monster,float> monsterList = new();

            if(targetLocation is SlimeHutch)
            {

                return new();

            }

            float threshold = 32 + (64 * radius);

            foreach (NPC nonPlayableCharacter in targetLocation.characters)
            {

                if (nonPlayableCharacter is StardewValley.Monsters.Monster monster)
                {

                    if (monster.IsInvisible|| monster.Health <= 0)
                    {

                        continue;

                    }

                    if(checkInvincible && monster.isInvincible())
                    {
                        continue;
                    }

                    float monsterThreshold = threshold;

                    if (monster.Sprite.SpriteWidth > 16)
                    {
                        monsterThreshold += 32f;
                    }

                    if (monster.Sprite.SpriteWidth > 32)
                    {
                        monsterThreshold += 32f;
                    }

                    float difference = Proximation(monster.Position, targetPosition, monsterThreshold);
                    
                    if(difference != -1f)
                    {

                        monsterList.Add(monster,difference);

                    }

                }

            }

            List<StardewValley.Monsters.Monster> ordered = new();

            foreach (KeyValuePair<StardewValley.Monsters.Monster, float> kvp in monsterList.OrderBy(key => key.Value))
            {

                ordered.Add(kvp.Key);

            }

            return ordered;

        }

        public static float Proximation(Vector2 position, List<Vector2> positions, float threshold)
        {

            foreach (Vector2 attempt in positions)
            {

                float difference = Vector2.Distance(position, attempt);

                if (difference < threshold)
                {

                    return difference;

                }

            }

            return -1f;

        }

        public static List<StardewValley.Monsters.Monster> MonsterIntersect(GameLocation targetLocation, Microsoft.Xna.Framework.Rectangle hitBox, bool checkInvincible = false)
        {

            List<StardewValley.Monsters.Monster> monsterList = new();

            foreach (NPC nonPlayableCharacter in targetLocation.characters)
            {

                if (nonPlayableCharacter is StardewValley.Monsters.Monster monster)
                {

                    if (monster.IsInvisible || monster.Health <= 0)
                    {

                        continue;

                    }

                    if (checkInvincible && monster.isInvincible())
                    {
                        continue;
                    }

                    Microsoft.Xna.Framework.Rectangle boundingBox = monster.GetBoundingBox();

                    if (boundingBox.Intersects(hitBox))
                    {

                        monsterList.Add(monster);

                    }

                }

            }

            return monsterList;

        }

        public static void DamageMonsters(GameLocation targetLocation, List<StardewValley.Monsters.Monster> monsterList, Farmer targetPlayer, int damage, bool push = false)
        {

            if(monsterList.Count == 0)
            {
                return;
            }

            foreach (StardewValley.Monsters.Monster monster in monsterList)
            {

                bool critApplied = false;

                float critDamage = CalculateCritical(damage);

                if (critDamage > 0)
                {

                    damage = (int)critDamage;

                    critApplied = true;

                }

                List<int> diff = new() { 0, 0 };

                if (push)
                {

                    CalculatePush(targetLocation, monster, targetPlayer.Position, 64);

                }

                Vector2 monsterPosition = monster.Position;

                HitMonster(targetLocation, targetPlayer, monster, damage, critApplied, diffX: diff[0], diffY: diff[1]);

            }

        }

        public static void HitMonster(GameLocation targetLocation, Farmer targetPlayer, StardewValley.Monsters.Monster targetMonster, int damage, bool critApplied, int diffX = 0, int diffY = 0)
        {

            bool specialHit = false;

            int damageDealt = 0;

            if (targetMonster is StardewValley.Monsters.Mummy mummy)
            {

                if (mummy.reviveTimer.Value > 0)
                {

                    damageDealt = mummy.takeDamage(1, 0, 0, true, 999f, targetPlayer);

                    specialHit = true;

                }

            }

            if (targetMonster is StardewValley.Monsters.Bug buggy)
            {

                damageDealt = 99;

                buggy.Health = 0;

                buggy.currentLocation.playSound("hitEnemy");

                buggy.deathAnimation();

                specialHit = true;

            }

            if(targetMonster is StardewValley.Monsters.RockCrab crabby)
            {
                Type reflectType = typeof(StardewValley.Monsters.RockCrab);

                FieldInfo reflectField = reflectType.GetField("shellGone", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                var shellGone = reflectField.GetValue(crabby);
;
                if (shellGone != null)
                {

                    if(!(shellGone as NetBool).Value)
                    {
                        FieldInfo shellField = reflectType.GetField("shellHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                        var shellHealth = shellField.GetValue(crabby);
                        
                        for(int i = 0; i < (shellHealth as NetInt).Value; i++)
                        {

                            crabby.hitWithTool(Mod.instance.virtualPick);

                        }

                    }

                }

            }

            if (!specialHit)
            {

                damageDealt = targetMonster.takeDamage(damage, diffX, diffY, false, 999f, targetPlayer);

            }

            foreach (StardewValley.Enchantments.BaseEnchantment enchantment in targetPlayer.enchantments)
            {
                enchantment.OnCalculateDamage(targetMonster, targetLocation, targetPlayer, ref damageDealt);
            }

            targetLocation.removeDamageDebris(targetMonster);

            Microsoft.Xna.Framework.Rectangle boundingBox = targetMonster.GetBoundingBox();

            Color color = new(255, 130, 0);

            if (critApplied)
            {

                color = Color.Yellow;

                targetLocation.playSound("crit");

            }

            targetLocation.debris.Add(new Debris(damageDealt, new Vector2(boundingBox.Center.X + 16, boundingBox.Center.Y), color, critApplied ? 1f + damageDealt / 300f : 1f, targetMonster));

            foreach (StardewValley.Enchantments.BaseEnchantment enchantment2 in targetPlayer.enchantments)
            {
                enchantment2.OnDealDamage(targetMonster, targetLocation, targetPlayer, ref damageDealt);
            }

            if (targetMonster.Health <= 0)
            {

                targetPlayer.checkForQuestComplete(null, 1, 1, null, targetMonster.Name, 4);

                if (Game1.player.team.specialOrders is not null)
                {
                    foreach (StardewValley.SpecialOrders.SpecialOrder specialOrder in Game1.player.team.specialOrders)
                    {
                        if (specialOrder.onMonsterSlain != null)
                        {
                            specialOrder.onMonsterSlain(Game1.player, targetMonster);
                        }
                    }
                }

                foreach (StardewValley.Enchantments.BaseEnchantment enchantment3 in targetPlayer.enchantments)
                {
                    enchantment3.OnMonsterSlay(targetMonster, targetLocation, targetPlayer);
                }

                if (targetPlayer.leftRing.Value != null)
                {
                    targetPlayer.leftRing.Value.onMonsterSlay(targetMonster, targetLocation, targetPlayer);
                }

                if (targetPlayer.rightRing.Value != null)
                {
                    targetPlayer.rightRing.Value.onMonsterSlay(targetMonster, targetLocation, targetPlayer);
                }

                if (!(targetMonster is GreenSlime) || (targetMonster as GreenSlime).firstGeneration.Value)
                {
                    if (targetPlayer.IsLocalPlayer)
                    {
                        Game1.stats.monsterKilled(targetMonster.Name);
                    }
                    else if (Game1.IsMasterGame)
                    {
                        targetPlayer.queueMessage(25, Game1.player, targetMonster.Name);
                    }
                }

                targetLocation.monsterDrop(targetMonster, boundingBox.Center.X, boundingBox.Center.Y, targetPlayer);

                targetPlayer.gainExperience(4, targetMonster.ExperienceGained);

                if (targetMonster.isHardModeMonster.Value)
                {
                    Game1.stats.Increment("hardModeMonstersKilled", 1);
                }

                //targetLocation.characters.Remove(targetMonster);

                Game1.stats.MonstersKilled++;

            }

        }

        public static List<Vector2> Explode(GameLocation targetLocation, Vector2 targetVector, Farmer targetPlayer, int radius, int powerLevel = 1)
        {

            Tool Pick = Mod.instance.virtualPick;

            Tool Axe = Mod.instance.virtualAxe;

            List<Vector2> returnVectors = new();

            // ----------------- clump destruction

            if (targetLocation.resourceClumps.Count > 0 && powerLevel >= 4)
            {
                
                for (int index = targetLocation.resourceClumps.Count - 1; index >= 0; --index)
                {
                    
                    ResourceClump resourceClump = targetLocation.resourceClumps[index];
                    
                    Vector2 targetVector1 = resourceClump.Tile;
                    
                    if ((double)Vector2.Distance(targetVector1, targetVector) <= radius + 1)
                    {
                        
                        switch (resourceClump.parentSheetIndex.Value)
                        {
                            case ResourceClump.stumpIndex:
                            case ResourceClump.hollowLogIndex:

                                DestroyStump(targetLocation, targetPlayer, resourceClump, targetVector1);
                                break;

                            default:

                                DestroyBoulder(targetLocation, targetPlayer, resourceClump, targetVector1);
                                break;

                        }
                    
                    }
                
                }
            
            }

            // ----------------- object destruction

            List<Vector2> tileVectors;

            int impactRadius = radius + 1;

            for (int i = 0; i < impactRadius; i++)
            {

                if (i == 0)
                {

                    tileVectors = new List<Vector2>
                    {

                        targetVector

                    };

                }
                else
                {

                    tileVectors = GetTilesWithinRadius(targetLocation, targetVector, i);

                }


                bool destroyVector;

                foreach (Vector2 tileVector in tileVectors)
                {

                    destroyVector = false;

                    if (targetLocation.objects.ContainsKey(tileVector))
                    {

                        StardewValley.Object targetObject = targetLocation.objects[tileVector];

                        if (targetObject.Name == "Stone")
                        {
                            
                            if (powerLevel >= 2)
                            {

                                targetLocation.OnStoneDestroyed(@targetObject.ParentSheetIndex.ToString(), (int)tileVector.X, (int)tileVector.Y, targetPlayer);

                                targetLocation.objects.Remove(tileVector);

                                destroyVector = true;

                            }

                        }
                        else if (targetObject.Name.Contains("Twig"))
                        {

                            Throw throwObject = new(targetPlayer, tileVector * 64, 388);

                            throwObject.ThrowObject();

                            targetObject.onExplosion(targetPlayer);

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;

                        }
                        else if (targetObject.Name.Contains("Weed"))
                        {

                            Throw throwObject = new(targetPlayer, tileVector * 64, 771);

                            throwObject.ThrowObject();

                            targetObject.onExplosion(targetPlayer);

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;

                        }
                        else if (targetObject.Name.Contains("SupplyCrate"))
                        {
                            targetObject.MinutesUntilReady = 1;

                            targetObject.performToolAction(Pick);

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;
                        }
                        else if (targetObject is BreakableContainer breakableContainer)
                        {

                            breakableContainer.releaseContents(targetPlayer);

                            targetLocation.objects.Remove(tileVector);

                            targetLocation.playSound("barrelBreak", tileVector*64);

                            destroyVector = true;

                        }
                        else if (targetObject is Fence || targetObject is StardewValley.Objects.Workbench || targetObject is StardewValley.Objects.Furniture || targetObject is StardewValley.Objects.Chest)
                        {

                            // do nothing

                        }
                        else if (powerLevel >= 3)
                        {

                            // ----------------- dislodge craftable

                            for (int j = 0; j < 2; j++)
                            {

                                Tool toolUse = (j == 0) ? Pick : Axe;

                                if (targetLocation.objects.ContainsKey(tileVector) && targetObject.performToolAction(toolUse))
                                {
                                    targetObject.performRemoveAction();

                                    targetObject.dropItem(targetLocation, tileVector * 64, tileVector * 64 + new Vector2(0, 32));

                                    targetLocation.objects.Remove(tileVector);

                                }

                            }

                        }

                    }

                    if (targetLocation.terrainFeatures.ContainsKey(tileVector))
                    {

                        if (powerLevel >= 3)
                        {

                            if (targetLocation.terrainFeatures[tileVector] is StardewValley.TerrainFeatures.Tree)
                            {

                                StardewValley.TerrainFeatures.Tree targetTree = targetLocation.terrainFeatures[tileVector] as StardewValley.TerrainFeatures.Tree;

                                if (targetTree.growthStage.Value >= 5)
                                {

                                    targetTree.performToolAction(null, (int)targetTree.health.Value, tileVector);

                                }
                                else
                                {

                                    targetTree.performToolAction(Axe, 0, tileVector);

                                    targetLocation.terrainFeatures.Remove(tileVector);

                                }

                                targetTree = null;

                                destroyVector = true;

                            }

                        }

                        if (targetLocation.terrainFeatures.ContainsKey(tileVector))
                        {

                            if (targetLocation.terrainFeatures[tileVector] is StardewValley.TerrainFeatures.Grass)
                            {

                                targetLocation.terrainFeatures.Remove(tileVector);


                                if (Game1.random.NextDouble() < 0.5)
                                {

                                    Throw throwObject = new(targetPlayer, tileVector * 64, 771);

                                    throwObject.ThrowObject();

                                }

                                destroyVector = true;

                            }

                        }

                    }

                    if (destroyVector)
                    {

                        returnVectors.Add(tileVector);

                    }

                }

            }

            return returnVectors;

        }

        public static void Reave(GameLocation targetLocation, Vector2 targetVector, Farmer targetPlayer, int radius)
        {
            
            List<Vector2> tileVectors;

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            int wet = (Game1.IsRainingHere(targetLocation) && (bool)targetLocation.IsOutdoors && !targetLocation.Name.Equals("Desert")) ? 1 : 0;

            for (int i = 0; i < radius + 1; i++)
            {

                if (i == 0)
                {

                    tileVectors = new List<Vector2>
                    {

                        targetVector

                    };

                }
                else
                {

                    tileVectors = GetTilesWithinRadius(targetLocation, targetVector, i);

                }

                int dirtCount = 0;

                foreach (Vector2 tileVector in tileVectors)
                {

                    dirtCount++;

                    if (i == radius && dirtCount % 2 == 1)
                    {

                        continue;

                    }

                    if (GroundCheck(targetLocation, tileVector) == "ground" && NeighbourCheck(targetLocation, tileVector, 0).Count == 0)
                    {

                        int tilex = (int)tileVector.X;
                        int tiley = (int)tileVector.Y;

                        Tile backTile = backLayer.Tiles[tilex, tiley];

                        if (backTile.TileIndexProperties.TryGetValue("Diggable", out _))
                        {

                            targetLocation.checkForBuriedItem(tilex, tiley, explosion: false, detectOnly: false, Game1.player);

                            targetLocation.terrainFeatures.Add(tileVector, new HoeDirt(wet, targetLocation));

                        }
                        else if (backTile.TileIndexProperties.TryGetValue("Diggable", out _))
                        {

                            targetLocation.checkForBuriedItem(tilex, tiley, explosion: false, detectOnly: false, Game1.player);

                            targetLocation.terrainFeatures.Add(tileVector, new HoeDirt(wet, targetLocation));

                        }

                    }

                }

            }

        }

        public static bool MonsterVitals(StardewValley.Monsters.Monster Monster, GameLocation location)
        {

            if (Monster == null)
            {

                return false;

            }

            if (Monster.Health <= 0)
            {

                return false;

            }

            if (Monster.currentLocation == null)
            {

                return false;

            }

            if (!Monster.currentLocation.characters.Contains(Monster))
            {

                return false;

            }

            if (Monster.currentLocation.Name != location.Name)
            {

                return false;

            }

            return true;

        }

        public static void DestroyBoulder(GameLocation targetLocation,Farmer targetPlayer,ResourceClump resourceClump,Vector2 targetVector)
        {
            Random random = new Random();

            resourceClump.health.Set(1f);

            resourceClump.performToolAction(Mod.instance.virtualPick, 1, targetVector);

            resourceClump.NeedsUpdate = false;

            targetLocation._activeTerrainFeatures.Remove(resourceClump);

            targetLocation.resourceClumps.Remove(resourceClump);

            int debris = 2;

            if (targetPlayer.professions.Contains(22))
            {
                debris = 4;
            }

            Throw throwObject;

            for (int index = 0; index < random.Next(1, debris); ++index)
            {
                switch (resourceClump.parentSheetIndex.Value)
                {
                    case 756:
                    case 758:

                        throwObject = new(targetPlayer, targetVector * 64, 536);

                        throwObject.ThrowObject();

                        break;

                    default:

                        if (targetLocation is MineShaft)
                        {
                            MineShaft mineShaft = (MineShaft)targetLocation;

                            if (mineShaft.mineLevel >= 80)
                            {

                                throwObject = new(targetPlayer, targetVector * 64, 537);

                                throwObject.ThrowObject();

                                break;
                            }
                            if (mineShaft.mineLevel >= 121)
                            {

                                throwObject = new(targetPlayer, targetVector * 64, 749);

                                throwObject.ThrowObject();

                                break;
                            }
                        }

                        throwObject = new(targetPlayer, targetVector * 64, 535);

                        throwObject.ThrowObject();

                        break;
                }
            }
        }

        public static void DestroyStump(GameLocation targetLocation,Farmer targetPlayer,ResourceClump resourceClump,Vector2 targetVector)
        {
            resourceClump.health.Set(1f);

            resourceClump.performToolAction(Mod.instance.virtualAxe, 1, targetVector);

            resourceClump.NeedsUpdate = false;

            Throw throwObject = new(targetPlayer, targetVector * 64, 709);

            throwObject.ThrowObject();

            throwObject.ThrowObject();

            if (targetLocation._activeTerrainFeatures.Contains(resourceClump))
            {
                
                targetLocation._activeTerrainFeatures.Remove(resourceClump);

            }

            if (targetLocation.resourceClumps.Contains(resourceClump))
            {

                targetLocation.resourceClumps.Remove(resourceClump);
            
            }

        }

        public static void LogStrings(List<string> strings)
        {
            
            string log = "";

            foreach (string s in strings)
            {

                log += s + ", ";

            }

            Mod.instance.Monitor.Log(log, LogLevel.Debug);

        }

        public static void LogIntegers(List<int> integers)
        {
            string log = "";

            foreach (int i in integers)
            {

                log += i.ToString() + ", ";

            }

            Mod.instance.Monitor.Log(log, LogLevel.Debug);
        }

        public static void LogVectors(List<Vector2> vectors)
        {
            string log = "";

            foreach (Vector2 v in vectors)
            {

                log += v.ToString() + ", ";

            }

            Mod.instance.Monitor.Log(log, LogLevel.Debug);
        }

        public static void LogMonsters(List<StardewValley.Monsters.Monster> monsters)
        {
            string log;

            foreach (StardewValley.Monsters.Monster m in monsters)
            {

                log = m.Name.ToString() + ", " + m.Position.ToString() + ", " + m.currentLocation.Name.ToString() + ", " + m.Health.ToString();

                Mod.instance.Monitor.Log(log, LogLevel.Debug);
            
            }

        }

    }

}
