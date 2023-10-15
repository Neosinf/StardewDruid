using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using static StardewValley.FarmerSprite;
using StardewDruid.Cast;
using StardewValley.TerrainFeatures;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;
using StardewValley.Locations;
using System.Threading;
using StardewModdingAPI;
using StardewValley.Monsters;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Map;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations;
using StardewValley.Characters;

namespace StardewDruid
{
    static class ModUtility
    {

        public static void AnimateEarthCast(Vector2 activeVector, int chargeLevel, int cycleLevel) {

            //-------------------------- cast variables

            int animationRow;

            Microsoft.Xna.Framework.Rectangle animationRectangle;

            float animationInterval;

            int animationLength;

            int animationLoops;

            bool animationFlip;

            float animationScale;

            Microsoft.Xna.Framework.Color animationColor;

            Vector2 animationPosition;

            TemporaryAnimatedSprite newAnimation;

            animationFlip = false;

            float colorIncrement = 1.2f - (0.2f * chargeLevel);

            animationColor = new(colorIncrement, 1, colorIncrement, 1);

            animationScale = 0.6f + (0.2f * chargeLevel);

            float vectorCastX = 12 - (6 * chargeLevel);

            float vectorCastY = 0 - 122 - (6 * chargeLevel);

            animationPosition = (activeVector * 64) + new Vector2(vectorCastX, vectorCastY);

            //-------------------------- cast animation

            animationRow = 10;

            animationRectangle = new(0, animationRow * 64, 64, 64);

            animationInterval = 75f;

            animationLength = 8;

            animationLoops = 1;

            newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlip, -1, 0f, animationColor, animationScale, 0f, 0f, 0f);

            Game1.player.currentLocation.temporarySprites.Add(newAnimation);

            //-------------------------- sound and pitch

            int pitchLevel = cycleLevel % 4;

            if (chargeLevel == 1)
            {

                Game1.player.currentLocation.playSoundPitched("discoverMineral", 600 + (pitchLevel * 200));

                Rumble.rumbleAndFade(1f, 333);

            }

            if (chargeLevel == 3)
            {

                Game1.player.currentLocation.playSoundPitched("discoverMineral", 700 + (pitchLevel * 200));

                Rumble.rumbleAndFade(1f, 333);

            }

        }

        public static void AnimateWaterCast(Vector2 activeVector, int chargeLevel, int cycleLevel)
        {

            //-------------------------- cast variables

            int animationRow = 5;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(128, animationRow * 64, 64, 64);

            float animationInterval = 168f;

            int animationLength = 2;

            bool animationFlip = false;

            float animationScale;

            float animationDepth = activeVector.X * 1000 + activeVector.Y;

            Microsoft.Xna.Framework.Color animationColor;

            Vector2 animationPosition;

            //-------------------------- cast shadow

            animationColor = new(0, 0, 0, 0.5f);

            animationScale = 0.2f * chargeLevel;

            animationPosition = (activeVector * 64) + (new Vector2(6, 6) * (5 - chargeLevel));

            TemporaryAnimatedSprite shadowAnimationOne = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, 1, animationPosition, false, animationFlip, animationDepth + 1, 0f, animationColor, animationScale, 0f, 0f, 0f);

            Game1.currentLocation.temporarySprites.Add(shadowAnimationOne);

            //-------------------------- cast shadow with delay

            animationScale = 0.1f + (0.2f * chargeLevel);

            animationPosition = (activeVector * 64) + (new Vector2(6, 6) * (5 - chargeLevel)) - new Vector2(3, 3);

            TemporaryAnimatedSprite shadowAnimationTwo = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, 1, animationPosition, false, animationFlip, animationDepth + 2, 0f, animationColor, animationScale, 0f, 0f, 0f)
            {

                delayBeforeAnimationStart = 334,

            };

            Game1.currentLocation.temporarySprites.Add(shadowAnimationTwo);

            //-------------------------- cast animation

            float colorIncrement = 1.2f - (0.2f * chargeLevel);

            animationColor = new(colorIncrement, colorIncrement, 1, 1); // deepens from white to blue

            animationScale = 0.2f + (0.4f * chargeLevel);

            float vectorCastX = 30 - (12 * chargeLevel);

            float vectorCastY = 0 - 160 - (32f * chargeLevel);

            animationPosition = (activeVector * 64) + new Vector2(vectorCastX, vectorCastY);

            TemporaryAnimatedSprite animationOne = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, 1, animationPosition, false, animationFlip, animationDepth + 3, 0f, animationColor, animationScale, 0f, 0f, 0f);

            Game1.currentLocation.temporarySprites.Add(animationOne);

            //-------------------------- cast animation with delay

            colorIncrement = 1.1f - (0.2f * chargeLevel);

            animationColor = new(colorIncrement, colorIncrement, 1, 1); // deepens from white to blue

            animationScale = 0.4f + (0.4f * chargeLevel);

            vectorCastX = 24 - (12 * chargeLevel);

            vectorCastY = 0 - 176 - (32f * chargeLevel);

            animationPosition = (activeVector * 64) + new Vector2(vectorCastX, vectorCastY);

            TemporaryAnimatedSprite animationTwo = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, 1, animationPosition, false, animationFlip, animationDepth + 4, 0f, animationColor, animationScale, 0f, 0f, 0f)
            {

                delayBeforeAnimationStart = 334,

            };

            Game1.currentLocation.temporarySprites.Add(animationTwo);

            //-------------------------- sound and pitch

            int pitchLevel = cycleLevel % 4;

            if (chargeLevel == 1)
            {

                Game1.currentLocation.playSoundPitched("thunder_small", 600 + (pitchLevel * 200));

            }

            if (chargeLevel == 3)
            {

                Game1.currentLocation.playSoundPitched("thunder_small", 700 + (pitchLevel * 200));

            }


        }

        public static void AnimateStarsCast(Vector2 activeVector, int chargeLevel, int cycleLevel)
        {

            //if (chargeLevel == 2)
            //{

            //    Game1.currentLocation.playSound("Meteorite");

            //}

        }

        public static void AnimateHands(Farmer player, int direction, int timeFrame)
        {

            player.Halt();

            AnimationFrame carryAnimation;

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

            player.FarmerSprite.animateOnce(new AnimationFrame[1] { carryAnimation });


        }

        public static void AnimateGrowth(GameLocation targetLocation, Vector2 targetVector) // DruidCastGrowth
        {

            int animationRow = 10;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            int animationLoops = 1;

            Microsoft.Xna.Framework.Color animationColor = new(0.8f, 1, 0.8f, 1); // light green

            Vector2 animationPosition = new(targetVector.X * 64 + 8, targetVector.Y * 64 + 8);

            float animationSort = float.Parse(targetVector.X.ToString() + targetVector.Y.ToString() + "0000");

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, animationColor, 1f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(newAnimation);

            return;

        }

        public static void AnimateCastRadius(GameLocation targetLocation, Vector2 targetVector, Color animationColor, int delayInterval = 0, int animationLoops = 1, float animationStrength = 0.75f)
        {

            int animationRow = 11;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            Vector2 animationPosition;

            if (animationStrength == 0.75f)
            {
                animationPosition = new(targetVector.X * 64 + 8, targetVector.Y * 64 + 8);
            }
            else
            {
                animationPosition = new(targetVector.X * 64 + (32 - (animationStrength * 32f)), targetVector.Y * 64 + (32 - (animationStrength * 32f)));
            }

            float animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString());

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, animationColor, animationStrength, 0f, 0f, 0f)
            {
                delayBeforeAnimationStart = 333 * delayInterval,
            };

            targetLocation.temporarySprites.Add(newAnimation);

            return;

        }

        public static void AnimateSplash(GameLocation targetLocation, Vector2 targetVector, bool animationFlipped) // DruidCastSplash
        {

            int animationRow = 19;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 128, 128);

            float animationInterval = 150f;

            int animationLength = 4;

            int animationLoops = 1;

            Microsoft.Xna.Framework.Color animationColor = Microsoft.Xna.Framework.Color.White;

            Vector2 animationPosition = new(targetVector.X * 64, targetVector.Y * 64);

            float animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString());

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlipped, animationSort, 0f, animationColor, 1f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(newAnimation);

            return;

        }

        public static void AnimateBolt(GameLocation targetLocation, Vector2 targetVector, bool playSound = true)
        {

            if(playSound)
            {

                Game1.currentLocation.playSoundPitched("flameSpellHit", 800);

            }

            Vector2 targetPosition = new(targetVector.X * 64, (targetVector.Y * 64) - 256);

            // ------------------------- cloud

            TemporaryAnimatedSprite boltCloud = new("TileSheets\\animations", new(128, 5 * 64, 64, 64), 250f, 4, 1, new Vector2(targetPosition.X+8,targetPosition.Y), flicker: false, false, (targetPosition.Y + 32f) / 10000f + 0.001f, 0.01f, new Color(0.6f,0.6f,1f,1), 0.875f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(boltCloud);

            // ------------------------- glow

            targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite(362, 75f, 6, 1, targetPosition, flicker: false, flipped: false));

            Microsoft.Xna.Framework.Rectangle sourceRect = new(644, 1078, 37, 57);

            TemporaryAnimatedSprite boltAnimation;

            // ------------------------- first bolt

            boltAnimation = new("LooseSprites\\Cursors", sourceRect, 1000f, 1, 1, targetPosition + new Vector2(0, 28.5f), flicker: false, Game1.random.NextDouble() < 0.5, (targetPosition.Y + 32f) / 10000f + 0.003f, 0.015f, new Color(0.6f, 0.6f, 1f, 1), 2f, 0f, 0f, 0f)
            {
                light = true,
                lightRadius = 2f,
                lightcolor = Color.Black
            };

            targetLocation.temporarySprites.Add(boltAnimation);

            // ------------------------- second bolt

            boltAnimation = new("LooseSprites\\Cursors", sourceRect, 1000f, 1, 1, targetPosition + new Vector2(0, 57f), flicker: false, Game1.random.NextDouble() < 0.5, (targetPosition.Y + 32f) / 10000f + 0.003f, 0.01f, new Color(0.8f, 0.8f, 1f, 1), 2f, 0f, 0f, 0f)
            {
                light = true,
                lightRadius = 2f,
                lightcolor = Color.Black
            };

            targetLocation.temporarySprites.Add(boltAnimation);

            // ------------------------- third bolt

            boltAnimation = new("LooseSprites\\Cursors", sourceRect, 1000f, 1, 1, targetPosition + new Vector2(0, 85.5f), flicker: false, Game1.random.NextDouble() < 0.5, (targetPosition.Y + 32f) / 10000f + 0.002f, 0.005f, Color.White, 2f, 0f, 0f, 0f)
            {
                light = true,
                lightRadius = 2f,
                lightcolor = Color.Black
            };

            targetLocation.temporarySprites.Add(boltAnimation);
            
            return;

        }

        /*public static TemporaryAnimatedSprite AnimateFishSpot(GameLocation targetLocation, Vector2 targetVector)
        {

            Microsoft.Xna.Framework.Color animationColor = new(0.6f, 1, 0.6f, 1); // light green

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, 51 * 64, 64, 64);

            Vector2 animationPosition = new((targetVector.X * 64), (targetVector.Y * 64));

            float animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString() + "22");

            TemporaryAnimatedSprite portalAnimation = new("TileSheets\\animations", animationRectangle, 80f, 10, 999999, animationPosition, false, false, animationSort, 0f, animationColor, 1f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(portalAnimation);

            return portalAnimation;

        }*/

        public static void AnimateFishJump(GameLocation targetLocation, Vector2 targetVector)
        {
            
            int fishIndex = SpawnData.RandomJumpFish(targetLocation);

            //if(!(Game1.viewport.X <= (targetVector.X*64)) || !(Game1.viewport.Y <= (targetVector.Y*64)))
            if (!Utility.isOnScreen(targetVector* 64, 128))
            {

                return;    
            
            }

            targetLocation.playSound("pullItemFromWater"); 
            
            DelayedAction.functionAfterDelay(QuickSlosh, 900);

            Vector2 fishPosition;

            Vector2 sloshPosition;

            Vector2 splashPosition;

            Vector2 sloshMotion;

            Vector2 sloshAcceleration;

            bool fishFlip;

            float fishRotate;

            bool sloshFlip;

            switch (Game1.random.Next(2) == 0)
            {

                case true:
                    
                    fishPosition = new((targetVector.X * 64) - 64, (targetVector.Y * 64) - 8);
                    
                    sloshPosition = new((targetVector.X * 64) + 100, targetVector.Y * 64);

                    splashPosition = new((targetVector.X * 64) - 128, (targetVector.Y * 64) - 40);

                    sloshMotion = new(0.160f, -0.5f);

                    sloshAcceleration = new(0f, 0.001f);

                    fishFlip = false;

                    fishRotate = 0.03f;

                    sloshFlip = true;

                    break;

                default:

                    fishPosition = new((targetVector.X * 64) + 64, (targetVector.Y * 64) - 8);

                    sloshPosition = new((targetVector.X * 64) - 128, targetVector.Y * 64);

                    splashPosition = new((targetVector.X * 64) + 100, (targetVector.Y * 64) - 40);

                    sloshMotion = new(-0.160f, -0.5f);

                    sloshAcceleration = new(0f, 0.001f);

                    fishFlip = true;

                    fishRotate = -0.03f;

                    sloshFlip = false;

                    break;


            }


            Microsoft.Xna.Framework.Rectangle targetRectangle = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, fishIndex, 16, 16);

            float animationInterval = 1050f;

            float animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString() + "33");

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

            animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString() + "44");

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, splashPosition, false, sloshFlip, animationSort, 0f, animationColor, 0.75f, 0f, 0.1f, 0f);

            targetLocation.temporarySprites.Add(newAnimation);

            // ------------------------------------

            //animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString() + "55");

            TemporaryAnimatedSprite sloshAnimation = new(28, 200f, 2, 1, sloshPosition, flicker: false, flipped: false)
            {
                
                delayBeforeAnimationStart = 900,

            };

            targetLocation.temporarySprites.Add(sloshAnimation);

        }

        public static void QuickSlosh()
        {
            Game1.currentLocation.localSound("quickSlosh");

        }

        /*public static void AnimateRipple(GameLocation targetLocation, Vector2 targetVector) //DruidCastSplosh
        {
            
            Game1.playSound("yoba");

            int animationRow = 0;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            int animationLoops = 1;

            Vector2 animationPosition = new((targetVector.X * 64), (targetVector.Y * 64));

            float animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString() + "11");

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, Microsoft.Xna.Framework.Color.White, 1f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(newAnimation);

        }*/

        public static void AnimateMeteorZone(GameLocation targetLocation, Vector2 targetVector, Color animationcolor, int meteorRange = 4, int animationLoops = 1, float animationStrength = 0.75f)
        {

            // --------------------------- splash animation

            int animationRow = 0;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            Vector2 animationPosition = new((targetVector.X * 64), (targetVector.Y * 64));

            float animationSort = (targetVector.X * 1000) + targetVector.Y;

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, Color.Red, 1f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(newAnimation);

            // --------------------------- splash radius

            List<Vector2> meteorZones = GetTilesWithinRadius(targetLocation, targetVector, meteorRange);

            foreach(Vector2 meteorZone in meteorZones)
            {

                AnimateCastRadius(targetLocation, meteorZone, animationcolor, 0, animationLoops, animationStrength);

            }

        }

        public static void AnimateMeteor(GameLocation targetLocation, Vector2 targetVector, bool targetDirection)
        {

            Microsoft.Xna.Framework.Rectangle meteorRectangle = new(0, 0, 32, 32);

            Vector2 meteorPosition;

            Vector2 meteorMotion;

            bool meteorRoll;

            switch (targetDirection)
            {

                case true:

                    meteorPosition = new((targetVector.X - 3) * 64, (targetVector.Y - 6) * 64);

                    meteorMotion = new Vector2(0.32f, 0.64f);

                    meteorRoll = false;

                    break;

                default:

                    meteorPosition = new((targetVector.X + 3) * 64, (targetVector.Y - 6) * 64);

                    meteorMotion = new Vector2(-0.32f, 0.64f);

                    meteorRoll = true;

                    break;

            }

            float meteorInterval = 150;

            float animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString());

            TemporaryAnimatedSprite meteorAnimation = new("TileSheets\\Fireball", meteorRectangle, meteorInterval, 4, 1, meteorPosition, flicker: false, meteorRoll, animationSort, 0f, Color.White, 2f, 0f, 0f, 0f)
            {

                motion = meteorMotion,

                timeBasedMotion = true,

            };

            targetLocation.temporarySprites.Add(meteorAnimation);

            Game1.currentLocation.playSound("fireball");

        }

        public static bool WaterCheck(GameLocation targetLocation, Vector2 targetVector)
        {
            bool check = true;

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            Tile backTile;

            for (int i = 0; i < 4; i++)
            {

                List<Vector2> neighbours = GetTilesWithinRadius(targetLocation, targetVector, i);

                foreach (Vector2 neighbour in neighbours)
                {

                    backTile = backLayer.PickTile(new Location((int)neighbour.X * 64, (int)neighbour.Y * 64), Game1.viewport.Size);

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

        public static Dictionary<string, List<Vector2>> NeighbourCheck(GameLocation targetLocation, Vector2 targetVector)
        {

            Dictionary<string, List<Vector2>> neighbourList = new();

            List<Vector2> neighbourVectors = GetTilesWithinRadius(targetLocation, targetVector, 1);

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            Layer buildingLayer = targetLocation.Map.GetLayer("Buildings");

            Layer pathsLayer = targetLocation.Map.GetLayer("Paths");

            if (targetLocation is BuildableGameLocation)
            {

                foreach (Vector2 neighbourVector in neighbourVectors)
                {

                    BuildableGameLocation farmLocation = targetLocation as BuildableGameLocation;

                    if (farmLocation.isTileOccupiedForPlacement(neighbourVector))
                    {

                        if (!neighbourList.ContainsKey("Building"))
                        {

                            neighbourList["Building"] = new();

                        }

                        neighbourList["Building"].Add(neighbourVector);

                    }

                    continue;


                }

            }

            foreach (Vector2 neighbourVector in neighbourVectors)
            {

                Tile buildingTile = buildingLayer.PickTile(new Location((int)neighbourVector.X * 64, (int)neighbourVector.Y * 64), Game1.viewport.Size);

                if (buildingTile != null)
                {

                    if (buildingTile.TileIndexProperties.TryGetValue("Passable", out _) == false)
                    {

                        if (!neighbourList.ContainsKey("Building"))
                        {

                            neighbourList["Building"] = new();

                        }

                        neighbourList["Building"].Add(neighbourVector);

                    }

                    continue;

                }
                
                if (pathsLayer != null)
                {

                    Tile pathsTile = buildingLayer.PickTile(new Location((int)neighbourVector.X * 64, (int)neighbourVector.Y * 64), Game1.viewport.Size);

                    if (pathsTile != null)
                    {

                        if (!neighbourList.ContainsKey("Building"))
                        {

                            neighbourList["Building"] = new();

                        }

                        neighbourList["Building"].Add(neighbourVector);

                        continue;

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

                            if(hoedCheck.crop != null)
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

                            break;

                    }

                    continue;

                }

            }

            return neighbourList;

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

            hoeDirt.destroyCrop(new Vector2(targetX, targetY), false, targetLocation);

            hoeDirt.plant(generateItem, targetX, targetY, targetPlayer,false, targetLocation);

            //hoeDirt.crop.updateDrawMath(new Vector2(targetX, targetY));

        }

        public static void GreetVillager(GameLocation location, Farmer player, NPC villager, bool friendShip = false)
        {

            villager.faceTowardFarmerForPeriod(3000, 4, false, player);

            if (!player.hasPlayerTalkedToNPC(villager.Name))
            {

                player.friendshipData[villager.Name].TalkedToToday = true;

                if (friendShip)
                {

                    player.changeFriendship(25, villager);

                }

            }

            int emoteIndex = 8;

            if (player.friendshipData[villager.Name].Points >= 500)
            {

                emoteIndex = 32;

            }

            if (player.friendshipData[villager.Name].Points >= 1000)
            {

                emoteIndex = 20;

            }

            villager.doEmote(emoteIndex);

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

                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, (int)targetAnimal.friendshipTowardFarmer.Value + num);

            }
            else
            {

                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, (int)targetAnimal.friendshipTowardFarmer.Value + 15);

            }

            if ((targetPlayer.professions.Contains(3) && !targetAnimal.isCoopDweller()) || (targetPlayer.professions.Contains(2) && targetAnimal.isCoopDweller()))
            {

                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, (int)targetAnimal.friendshipTowardFarmer.Value + 15);

                targetAnimal.happiness.Value = (byte)Math.Min(255, (byte)targetAnimal.happiness.Value + Math.Max(5, 40 - (byte)targetAnimal.happinessDrain.Value));

            }

            int num2 = 20;

            if (targetAnimal.wasAutoPet.Value)
            {

                num2 = 32;

            }

            targetAnimal.doEmote(((int)targetAnimal.moodMessage.Value == 4) ? 12 : num2);

            targetAnimal.happiness.Value = (byte)Math.Min(255, (byte)targetAnimal.happiness.Value + Math.Max(5, 40 - (byte)targetAnimal.happinessDrain.Value));

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

                center + new Vector2(2,-7),
                center + new Vector2(3,-6),

                center + new Vector2(4,-6),
                center + new Vector2(5,-5), // NE
                center + new Vector2(6,-4),

                center + new Vector2(6,-3),
                center + new Vector2(7,-2),

                center + new Vector2(7,2),
                center + new Vector2(6,3),

                center + new Vector2(6,4),
                center + new Vector2(5,5), // SE
                center + new Vector2(4,6),

                center + new Vector2(3,6),
                center + new Vector2(2,7),

                center + new Vector2(-2,7),
                center + new Vector2(-3,6),

                center + new Vector2(-4,6),
                center + new Vector2(-5,5), // SW
                center + new Vector2(-6,4),

                center + new Vector2(-6,3),
                center + new Vector2(-7,2),

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

        public static List<Vector2> GetTilesWithinRadius(GameLocation playerLocation, Vector2 center, int level)
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

            float mapWidth = (playerLocation.Map.DisplayWidth / 16);

            float mapHeight = (playerLocation.Map.DisplayHeight / 16);

            List<Vector2> vectorList = new();

            foreach (Vector2 testVector in templateList)
            {

                if (testVector.X < 0 || testVector.X > mapWidth || testVector.Y < 0 || testVector.Y > mapHeight)
                {

                }
                else
                {

                    vectorList.Add(testVector);

                }

            }

            return vectorList;

        }

    }

}
