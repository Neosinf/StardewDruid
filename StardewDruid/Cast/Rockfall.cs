using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using xTile.Dimensions;
using xTile.Tiles;

namespace StardewDruid.Cast
{
    internal class Rockfall: Cast
    {

        private MineShaft shaftLocation;

        public int objectIndex { get ; set; }

        public int objectStrength { get; set; }

        public Rockfall(Mod mod, Vector2 target, Farmer player)
            : base(mod, target, player)
        {

            castCost = 4;

            shaftLocation = targetPlayer.currentLocation as MineShaft;
            
            if (shaftLocation.mineLevel <= 40)
            {

                objectStrength = 1;
           
            }
            else if (shaftLocation.mineLevel <= 80)
            {

                objectStrength = 2;
            
            }
            else //(targetLocation.mineLevel <= 120)
            {
                
                objectStrength = 3;
            
            }

        }

        public override void CastEarth()
        {

            int tileX = (int)targetVector.X;

            int tileY = (int)targetVector.Y;

            Tile buildingTile = targetLocation.Map.GetLayer("Buildings").PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

            if (buildingTile != null)
            {

                return;

            }

            bool generateRock = false;

            bool generateHoed = false;

            if (shaftLocation.mineLevel % 10 != 0 || shaftLocation.mineLevel >= 121)
            {

                Tile backTile = targetLocation.Map.GetLayer("Back").PickTile(new Location((int)targetVector.X * 64, (int)targetVector.Y * 64), Game1.viewport.Size);

                if (backTile != null)
                {

                    if (backTile.TileIndexProperties.TryGetValue("Type", out var typeValue))
                    {

                        if (typeValue == "Stone")
                        {

                            generateRock = true;

                        }

                        if (typeValue == "Dirt")
                        {

                            generateHoed = true;

                        }

                    }

                }

            }

            int randomInt = (generateRock) ? 18 : 9;

            int probability = randomIndex.Next(randomInt);

            Dictionary<int, int> objectIndexes;

            Dictionary<int, int> scatterIndexes;

            if (shaftLocation.mineLevel <= 40)
            {

                objectIndexes = new()
                {
                    [0] = 32, // grade 1 stone
                    [1] = 32, // grade 1 stone
                    [2] = 32, // grade 1 stone
                    [3] = 40, // grade 1 stone
                    [4] = 40, // grade 1 stone
                    [5] = 40, // grade 1 stone
                    [6] = 42, // grade 1 stone
                    [7] = 42, // grade 1 stone
                    [8] = 42, // grade 1 stone
                    [9] = 668, // coal stone
                    [10] = 670, // coal stone
                    [11] = 670, // coal stone
                    [12] = 751, // copper stone
                    [13] = 751, // copper stone
                    [14] = 751, // copper stone
                    [15] = 751, // copper stone
                    [16] = 8, // amethyst
                    [17] = 10, // topaz
                };

                scatterIndexes = new()
                {
                    [32] = 33,
                    [40] = 41,
                    [42] = 43,
                    [668] = 669,
                    [670] = 671,
                    [751] = 33,
                    [8] = 9,
                    [10] = 11,
                };

            }
            else if (shaftLocation.mineLevel <= 80)
            {

                objectIndexes = new()
                {
                    [0] = 48, // grade 2a stone
                    [1] = 48, // grade 2a stone
                    [2] = 48, // grade 2a stone
                    [3] = 50, // grade 2b stone
                    [4] = 50, // grade 2b stone
                    [5] = 50, // grade 2b stone
                    [6] = 52, // grade 2c stone
                    [7] = 52, // grade 2c stone
                    [8] = 54, // grade 2d stone
                    [9] = 54, // grade 2d stone
                    [10] = 44, // special stone
                    [11] = 44, // special stone
                    [12] = 290, // iron ore
                    [13] = 290, // iron ore
                    [14] = 290, // iron ore
                    [15] = 290, // iron ore
                    [16] = 12, // emerald
                    [17] = 14, // aquamarine
                };

                scatterIndexes = new()
                {
                    [48] = 49,
                    [50] = 51,
                    [52] = 53,
                    [54] = 55,
                    [44] = 45,
                    [290] = 35,
                    [12] = 13,
                    [14] = 15,
                };

            }
            else //(targetLocation.mineLevel <= 120)
            {

                objectIndexes = new()
                {
                    [0] = 760, // grade 3 stone
                    [1] = 760, // grade 3 stone
                    [2] = 760, // grade 3 stone
                    [3] = 762, // grade 3 stone
                    [4] = 762, // grade 3 stone
                    [5] = 762, // grade 3 stone
                    [6] = 56, // grade 3 stone
                    [7] = 56, // grade 3 stone
                    [8] = 58, // grade 3 stone
                    [9] = 58, // grade 3 stone
                    [10] = 44, // special stone
                    [11] = 44, // special stone
                    [12] = 764, // gold ore
                    [13] = 764, // gold ore
                    [14] = 764, // gold ore
                    [15] = 764, // gold ore
                    [16] = 2, // ruby
                    [17] = 4, // diamond
                };

                scatterIndexes = new()
                {
                    [760] = 761,
                    [762] = 763,
                    [56] = 57,
                    [58] = 59,
                    [44] = 45,
                    [764] = 761,
                    [2] = 3,
                    [4] = 5,
                };

            }

            objectIndex = objectIndexes[probability];

            int scatterIndex = scatterIndexes[objectIndex];

            Microsoft.Xna.Framework.Rectangle objectRectangle = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, objectIndex, 16, 16);

            Microsoft.Xna.Framework.Rectangle scatterRectangle = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, scatterIndex, 16, 16);

            float animationInterval = 500f;

            Vector2 animationPosition;

            float animationAcceleration;

            float animationSort;

            float animationRotation;

            float animationRotate;

            TemporaryAnimatedSprite animationScatter;

            TemporaryAnimatedSprite animationRock;

            // ----------------------------- large debris

            animationPosition = new(targetVector.X * 64 - 8, (targetVector.Y - 3) * 64 - 8);

            animationAcceleration = 0.001f;

            animationSort = targetVector.X * 1000 + targetVector.Y + 10;

            animationScatter = new("Maps\\springobjects", scatterRectangle, animationInterval, 1, 0, animationPosition, flicker: false, flipped: false, animationSort, 0f, Color.White, 5f, 0f, 0f, 0f)
            {
                acceleration = new Vector2(0, animationAcceleration),
                delayBeforeAnimationStart = 200,
                timeBasedMotion = true,
            };

            targetPlayer.currentLocation.temporarySprites.Add(animationScatter);

            DelayedAction.functionAfterDelay(DebrisImpact, 575);


            // ----------------------------- large debris shadow

            animationPosition = new(targetVector.X * 64, (targetVector.Y - 3) * 64);

            animationSort = targetVector.X * 1000 + targetVector.Y + 11;

            animationScatter = new("Maps\\springobjects", scatterRectangle, animationInterval, 1, 0, animationPosition, flicker: false, flipped: false, animationSort, 0f, Color.Black * 0.5f, 4f, 0f, 0f, 0f)
            {
                delayBeforeAnimationStart = 200,
                timeBasedMotion = true,
            };

            targetPlayer.currentLocation.temporarySprites.Add(animationScatter);

            DelayedAction.functionAfterDelay(DebrisImpact, 575);

            // ------------------------------ rock generation

            if (generateRock)
            {

                animationPosition = new(targetVector.X * 64 + 8, (targetVector.Y - 3) * 64 + 8);

                animationSort = targetVector.X * 1000 + targetVector.Y + 12;

                animationRotation = 0f;

                animationRotate = 0f;

                animationRock = new("Maps\\springobjects", objectRectangle, animationInterval, 1, 0, animationPosition, flicker: false, flipped: false, animationSort, 0f, Color.White, 3f, 0f, animationRotation, animationRotate)
                {
                    acceleration = new Vector2(0, animationAcceleration),
                    delayBeforeAnimationStart = 100,
                    timeBasedMotion = true,
                };

                targetPlayer.currentLocation.temporarySprites.Add(animationRock);

                //castCost = 4;

                DelayedAction.functionAfterDelay(RockImpact, 600);

            }

            // ------------------------------ hoed generation

            if (generateHoed)
            {

                DelayedAction.functionAfterDelay(DirtImpact, 600);


            }

            castFire = true;

        }

        public void DebrisImpact()
        {

            Microsoft.Xna.Framework.Rectangle areaOfEffect = new(
                ((int)targetVector.X - 1) * 64,
                ((int)targetVector.Y - 1) * 64,
                192,
                192
            );

            targetLocation.damageMonster(areaOfEffect, 10 * objectStrength, 20 * objectStrength, true, targetPlayer);

            targetLocation.lightGlows.Add(targetVector * 64);

        }

        public void RockImpact()
        {

            StardewValley.Object objectInstance = new(targetVector, objectIndex, "Stone", true, false, false, false)
            {
                MinutesUntilReady = objectStrength
            };
            
            if (objectInstance != null)
            {

                targetLocation.Objects.Add(targetVector, objectInstance);

            }

            Game1.createObjectDebris(390, (int)targetVector.X, (int)targetVector.Y);
            Game1.createObjectDebris(390, (int)targetVector.X, (int)targetVector.Y);

            if (randomIndex.Next(2) == 0)
            {

                Game1.createObjectDebris(390, (int)targetVector.X, (int)targetVector.Y);
                Game1.createObjectDebris(390, (int)targetVector.X, (int)targetVector.Y);

            }

            if (randomIndex.Next(2) == 0)
            {

                Game1.createObjectDebris(382, (int)targetVector.X, (int)targetVector.Y);

            }

        }

        public void DirtImpact()
        {

            List<Vector2> tileVectors = new()
            {
                targetVector,
                targetVector + new Vector2(-1,0),
                targetVector + new Vector2(0,-1),
                targetVector + new Vector2(1,0),
                targetVector + new Vector2(0,1),

            };

            foreach (Vector2 tileVector in tileVectors)
            {
                if (!targetLocation.objects.ContainsKey(tileVector) && targetLocation.doesTileHaveProperty((int)tileVector.X, (int)tileVector.Y, "Diggable", "Back") != null && !targetLocation.isTileHoeDirt(tileVector))
                {
                    
                    targetLocation.checkForBuriedItem((int)tileVector.X, (int)tileVector.Y, explosion: true, detectOnly: false, targetPlayer);
                    
                    targetLocation.makeHoeDirt(tileVector);

                }

            }

        }

    }

}
