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

namespace StardewDruid
{
    static class ModUtility
    {

        public static void AnimateHands(Farmer player, int direction, int timeFrame)
        {

            player.Halt();

            AnimationFrame carryAnimation;

            switch (direction)
            {

                case 0: // Up

                    carryAnimation = new(12, timeFrame, true, false); // changes secondaryArm to active

                    break;

                case 1: // Right

                    carryAnimation = new(6, timeFrame, true, false);

                    break;

                case 2: // Down

                    carryAnimation = new(0, timeFrame, true, false);

                    break;

                default: // Left

                    carryAnimation = new(6, timeFrame, true, true); // same as right but flipped

                    break;

            }

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

        public static void AnimateEarth(GameLocation targetLocation, Vector2 targetVector) // DruidGrowthAnimation
        {

            int animationRow = 11;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            int animationLoops = 1;

            Microsoft.Xna.Framework.Color animationColor = Microsoft.Xna.Framework.Color.White; //new(0.8f, 1, 0.8f, 1); // light green

            Vector2 animationPosition = new(targetVector.X * 64 + 8, targetVector.Y * 64 + 8);

            float animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString());

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, animationColor, 0.75f, 0f, 0f, 0f);

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

        public static void AnimateBolt(GameLocation targetLocation, Vector2 targetVector) //DruidLightningBolt
        {
            AnimateBolt(targetLocation, targetVector, "thunder");

        }

        public static void AnimateBolt(GameLocation targetLocation, Vector2 targetVector, string playSound)
        {

            //Game1.flashAlpha = (float)(0.5 + Game1.random.NextDouble());

            Game1.playSound(playSound);

            Vector2 targetPosition = new(targetVector.X * 64, targetVector.Y * 64);

            targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite(362, 75f, 6, 1, targetPosition, flicker: false, flipped: false));

            Microsoft.Xna.Framework.Rectangle sourceRect = new(644, 1078, 37, 57);

            Vector2 position = targetPosition + new Vector2(-5, -sourceRect.Height);

            TemporaryAnimatedSprite boltAnimation;

            while (position.Y > (float)(-sourceRect.Height * 2))
            {

                boltAnimation = new("LooseSprites\\Cursors", sourceRect, 9999f, 1, 999, position, flicker: false, Game1.random.NextDouble() < 0.5, (targetPosition.Y + 32f) / 10000f + 0.001f, 0.025f, Microsoft.Xna.Framework.Color.White, 2f, 0f, 0f, 0f)
                {
                    light = true,
                    lightRadius = 2f,
                    delayBeforeAnimationStart = 200,
                    lightcolor = Microsoft.Xna.Framework.Color.Black
                };

                targetLocation.temporarySprites.Add(boltAnimation);
                position.Y -= sourceRect.Height * 2;
            }

            return;

        }

        public static void AnimateRipple(GameLocation targetLocation, Vector2 targetVector) //DruidCastSplosh
        {

            Game1.playSound("yoba");

            int animationRow = 0;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            int animationLoops = 1;

            Vector2 animationPosition = new((targetVector.X * 64) - 16, (targetVector.Y * 64) - 16);

            float animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString());

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, Microsoft.Xna.Framework.Color.White, 1.5f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(newAnimation);

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

                    meteorMotion = new(0.32f, 0.64f);

                    meteorRoll = false;

                    break;

                default:

                    meteorPosition = new((targetVector.X + 3) * 64, (targetVector.Y - 6) * 64);

                    meteorMotion = new(-0.32f, 0.64f);

                    meteorRoll = true;

                    break;

            }

            float meteorInterval = 150f;

            float animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString());

            TemporaryAnimatedSprite meteorAnimation = new("TileSheets\\Fireball", meteorRectangle, meteorInterval, 4, 0, meteorPosition, flicker: false, meteorRoll, animationSort, 0f, Color.White, 2f, 0f, 0f, 0f)
            {

                motion = meteorMotion,

                timeBasedMotion = true,

            };

            targetLocation.temporarySprites.Add(meteorAnimation);

        }

        public static Dictionary<string, List<Vector2>> NeighbourCheck(GameLocation targetLocation, Vector2 targetVector)
        {

            Dictionary<string, List<Vector2>> neighbourList = new();

            List<Vector2> neighbourVectors = GetTilesWithinRadius(targetLocation, targetVector, 1);

            Layer buildingLayer = targetLocation.Map.GetLayer("Buildings");

            Layer pathsLayer = targetLocation.Map.GetLayer("Paths");

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

        public static void UpgradeCrop(StardewValley.TerrainFeatures.HoeDirt hoeDirt, int targetX, int targetY, Farmer targetPlayer, GameLocation targetLocation)
        {

            int generateItem;

            int targetSeed = Game1.random.Next(8);

            if (targetSeed >= 5) // 2/3 low grade random seed
            {

                generateItem = 770;

            }
            else
            {

                Dictionary<int, int> objectIndexes;

                switch (Game1.currentSeason)
                {

                    case "spring":

                        objectIndexes = new()
                        {
                            [0] = 478, // rhubarb
                            [1] = 476, // garlic
                            [2] = 433, // coffee
                            [3] = 745, // strawberry
                            [4] = 473, // bean
                        };

                        break;

                    case "summer":

                        objectIndexes = new()
                        {
                            [0] = 479, // melon
                            [1] = 485, // red cabbage
                            [2] = 433, // coffee
                            [3] = 481, // blueberry
                            [4] = 301 // hops
                        };


                        break;

                    default: // "fall":

                        objectIndexes = new()
                        {
                            [0] = 490, // pumpkin
                            [1] = 492, // yam
                            [2] = 299, // amaranth
                            [3] = 493, // cranberry
                            [4] = 302 // grape
                        };

                        break;

                }

                generateItem = objectIndexes[targetSeed];

            }

            hoeDirt.destroyCrop(new Vector2(targetX, targetY), false, targetLocation);

            hoeDirt.plant(generateItem, targetX, targetY, targetPlayer,false, targetLocation);

            //hoeDirt.crop.updateDrawMath(new Vector2(targetX, targetY));

        }

        /*public static void PlantSeed(GameLocation targetLocation, Farmer targetPlayer, Vector2 targetVector, int targetSeed)
        {

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if(targetLocation.terrainFeatures[targetVector] is not HoeDirt)
            { 
                
                return; 
            
            }

            StardewValley.TerrainFeatures.HoeDirt hoeDirt = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.HoeDirt;

            int generateItem;

            if (targetSeed == 5) // 2/3 low grade random seed
            {

                generateItem = 770;

            }
            else
            {

                Dictionary<int, int> objectIndexes;

                switch (Game1.currentSeason)
                {

                    case "spring":

                        objectIndexes = new()
                        {
                            [0] = 478, // rhubarb
                            [1] = 476, // garlic
                            [2] = 433, // coffee
                            [3] = 745, // strawberry
                            [4] = 473, // bean
                        };

                        break;

                    case "summer":

                        objectIndexes = new()
                        {
                            [0] = 479, // melon
                            [1] = 485, // red cabbage
                            [2] = 433, // coffee
                            [3] = 481, // blueberry
                            [4] = 301 // hops
                        };


                        break;

                    default: // "fall":

                        objectIndexes = new()
                        {
                            [0] = 490, // pumpkin
                            [1] = 492, // yam
                            [2] = 299, // amaranth
                            [3] = 493, // cranberry
                            [4] = 302 // grape
                        };

                        break;

                }

                generateItem = objectIndexes[targetSeed];

            }

            hoeDirt.state.Value = 1;

            hoeDirt.plant(generateItem, (int)targetVector.X, (int)targetVector.Y, targetPlayer, false, targetLocation); // high grade seed

            int currentPhase;

            switch (hoeDirt.crop.phaseDays.Count)
            {

                //case 5: currentPhase = 3; break;

                case 6: currentPhase = 3; break;

                default: currentPhase = 2; break;

            }

            for(int i = 0; i < currentPhase; i++)
            {

                hoeDirt.crop.currentPhase.Value++;

            }

            hoeDirt.crop.dayOfCurrentPhase.Value = 0;

            hoeDirt.crop.updateDrawMath(targetVector);

            hoeDirt.plant(920, (int)targetVector.X, (int)targetVector.Y, targetPlayer, true, targetLocation); // always watered        


        }*/

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
