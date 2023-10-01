using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Locations;
using System.Collections.Generic;
using System.ComponentModel.Design;
using xTile.Dimensions;
using xTile.Tiles;

namespace StardewDruid.Cast
{
    internal class Rockfall: Cast
    {

        private readonly MineShaft shaftLocation;

        public int objectIndex;

        public int objectStrength;

        public int debrisIndex;

        public Rockfall(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            castCost = 1;

            shaftLocation = targetPlayer.currentLocation as MineShaft;


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

            //int randomInt = 11;

            int probability = randomIndex.Next(11);

            Dictionary<int, int> objectIndexes;

            Dictionary<int, int> specialIndexes;

            if (shaftLocation.mineLevel <= 40)
            {

                objectStrength = 1;

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
                    [9] = 42, // grade 1 stone

                };

                specialIndexes = new()
                {

                    [0] = 668, // coal stone
                    [1] = 751, // copper stone
                    [2] = 751, // copper stone
                    [3] = 8, // amethyst
                    [4] = 10, // topaz

                };

            }
            else if (shaftLocation.mineLevel <= 80)
            {


                objectStrength = 2;

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
                };

                specialIndexes = new()
                {

                    [0] = 668, // coal stone
                    [1] = 290, // iron ore
                    [2] = 290, // iron ore
                    [3] = 12, // emerald
                    [4] = 14, // aquamarine*

                };


            }
            else //(targetLocation.mineLevel <= 120)
            {

                objectStrength = 4;

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

                };

                specialIndexes = new()
                {

                    [0] = 668, // coal stone
                    [1] = 764, // gold ore
                    [2] = 764, // gold ore
                    [3] = 2, // ruby
                    [4] = 4, // diamond*

                };

            }

            Dictionary<int, int> scatterIndexes = new()
            {

                [2] = 3,
                [4] = 5,
                [8] = 9,
                [10] = 11,
                [12] = 13,
                [14] = 15,
                [32] = 33,
                [40] = 41,
                [42] = 43,
                [44] = 45,
                [48] = 49,
                [50] = 51,
                [52] = 53,
                [54] = 55,
                [56] = 57,
                [58] = 59,
                [290] = 35,
                [668] = 669,
                [670] = 671,
                [751] = 33,
                [760] = 761,
                [762] = 763,
                [764] = 761,

            };

            Dictionary<int, int> debrisIndexes = new()
            {

                [2] = 72,
                [4] = 64,
                [8] = 66,
                [10] = 68,
                [12] = 60,
                [14] = 62,
                [290] = 380,
                [751] = 378,
                [764] = 384,

            };

            if (probability == 10)
            {
                objectIndex = specialIndexes[randomIndex.Next(5)];

                objectStrength += 1;

            }
            else
            {
                objectIndex = objectIndexes[probability];

            }

            debrisIndex = 390;

            if (debrisIndexes.ContainsKey(objectIndex))
            {
                debrisIndex = debrisIndexes[objectIndex];

            }

            int scatterIndex = scatterIndexes[objectIndex];

            Microsoft.Xna.Framework.Rectangle objectRectangle = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, objectIndex, 16, 16);

            Microsoft.Xna.Framework.Rectangle scatterRectangle = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, scatterIndex, 16, 16);

            float animationInterval = 500f;

            Vector2 animationPosition;

            float animationAcceleration;

            float animationSort;

            TemporaryAnimatedSprite animationScatter;

            TemporaryAnimatedSprite animationRock;

            // ----------------------------- large debris

            animationPosition = new(targetVector.X * 64 + 8, (targetVector.Y - 3) * 64 - 24);

            animationAcceleration = 0.001f;

            animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString() + "1");

            animationScatter = new("Maps\\springobjects", scatterRectangle, animationInterval, 1, 0, animationPosition, flicker: false, flipped: false, animationSort, 0f, Color.White, 3f, 0f, 0f, 0f)
            {
                acceleration = new Vector2(0, animationAcceleration),
                timeBasedMotion = true,
            };

            targetPlayer.currentLocation.temporarySprites.Add(animationScatter);

            // ------------------------------ rock generation

            animationPosition = new(targetVector.X * 64 + 8, (targetVector.Y - 3) * 64 + 8);

            animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString() + "2");

            animationRock = new("Maps\\springobjects", objectRectangle, animationInterval, 1, 0, animationPosition, flicker: false, flipped: false, animationSort, 0f, Color.White, 3f, 0f, 0f, 0f)
            {
                acceleration = new Vector2(0, animationAcceleration),
                timeBasedMotion = true,
            };

            targetPlayer.currentLocation.temporarySprites.Add(animationRock);

            // ----------------------------- shadow

            animationPosition = new(targetVector.X * 64 + 16, targetVector.Y * 64 + 16);

            animationSort = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString() + "3");

            animationRock = new("Maps\\springobjects", objectRectangle, animationInterval, 1, 0, animationPosition, flicker: false, flipped: false, animationSort, 0f, Color.Black * 0.5f, 2f, 0f, 0f, 0f);

            targetPlayer.currentLocation.temporarySprites.Add(animationRock);

            // ------------------------------ impacts

            DelayedAction.functionAfterDelay(DebrisImpact, 575);

            if (generateRock)
            {
                
                DelayedAction.functionAfterDelay(RockImpact, 600);

            }

            if (generateHoed)
            {

                DelayedAction.functionAfterDelay(DirtImpact, 600);

            }

            castFire = true;

        }

        public void DebrisImpact()
        {

            Microsoft.Xna.Framework.Rectangle areaOfEffect = new(
                ((int)targetVector.X - 2) * 64,
                ((int)targetVector.Y - 2) * 64,
                300,
                300
            );

            targetLocation.damageMonster(areaOfEffect, 30 * objectStrength, 40 * objectStrength, true, targetPlayer);

            //targetLocation.lightGlows.Add(targetVector * 64);

        }

        public void RockImpact()
        {

            /*StardewValley.Object objectInstance = new(targetVector, objectIndex, "Stone", true, false, false, false)
            {
                MinutesUntilReady = objectStrength
            };
            
            if (objectInstance != null)
            {
                targetLocation.Objects.Add(targetVector, objectInstance);
            }

            mod.UpdateEarthCasts(targetLocation, targetVector, false);*/

            for (int i = 0; i < randomIndex.Next(1, 3); i++)
            {

                if (i == 0)
                {

                    Game1.createObjectDebris(debrisIndex, (int)targetVector.X, (int)targetVector.Y);

                }

                Game1.createObjectDebris(390, (int)targetVector.X, (int)targetVector.Y);

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
