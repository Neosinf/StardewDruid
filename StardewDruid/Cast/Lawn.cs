using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast
{
    internal class Lawn : Cast
    {

        private readonly Dictionary<string, bool> spawnIndex;

        public Lawn(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            spawnIndex = rite.spawnIndex;

        }

        public override void CastEarth()
        {
            
            if (targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            Dictionary<string, List<Vector2>> neighbourList = ModUtility.NeighbourCheck(targetLocation, targetVector);

            int probability = randomIndex.Next(120);

            if (probability <= 1 && spawnIndex["flower"] && neighbourList.Count == 0) // 2/120 flower
            {

                /*Dictionary<int, int> objectIndexes;

                switch (Game1.currentSeason)
                {

                    case "spring":

                        objectIndexes = new()
                        {
                            [0] = 427, //591, // tulip
                            [1] = 429, //597, // jazz
                        };

                        break;

                    case "summer":

                        objectIndexes = new()
                        {
                            [0] = 455, //593, // spangle
                            [1] = 453, //376, // poppy
                        };

                        break;

                    default: //"fall":

                        objectIndexes = new()
                        {
                            [0] = 425, //595, // fairy
                            [1] = 431, //421, // sunflower
                        };

                        break;

                }

                Crop cropFlower = new(objectIndexes[probability], (int)targetVector.X, (int)targetVector.Y);
                    
                HoeDirt hoeDirt = new(2, cropFlower);

                targetLocation.terrainFeatures.Add(targetVector, hoeDirt);

                cropFlower.growCompletely();

                //cropFlower.forageCrop.Value = true;

                castFire = true;

                castCost = 6;*/


                Dictionary<int, int> objectIndexes;

                switch (Game1.currentSeason)
                {

                    case "spring":

                        objectIndexes = new()
                        {
                            [0] = 591, // tulip
                            [1] = 597, // jazz
                        };

                        break;

                    case "summer":

                        objectIndexes = new()
                        {
                            [0] = 593, // spangle
                            [1] = 376, // poppy
                        };

                        break;

                    default: //"fall":

                        objectIndexes = new()
                        {
                            [0] = 595, // fairy
                            [1] = 421, // sunflower
                        };

                        break;

                }

                int randomCrop = objectIndexes[probability];

                targetLocation.dropObject(
                    new StardewValley.Object(
                        targetVector,
                        randomCrop,
                        null,
                        canBeSetDown: false,
                        canBeGrabbed: true,
                        isHoedirt: false,
                        isSpawnedObject: true
                    ),
                    new Vector2(targetVector.X * 64, targetVector.Y * 64),
                    Game1.viewport,
                    initialPlacement: true
                );

                castFire = true;

                castCost = 4;


            }
            else if (probability >= 2 && probability <= 3 && spawnIndex["forage"] && neighbourList.Count == 0) // 2/120 forage
            {

                Dictionary<int, int> randomCrops;

                int randomCrop;

                switch (Game1.currentSeason)
                {

                    case "spring":

                        randomCrop = 16 + randomIndex.Next(4) * 2;

                        break;

                    case "summer":

                        randomCrops = new()
                        {
                            [0] = 396,
                            [1] = 396,
                            [2] = 402,
                            [3] = 398,
                        };

                        randomCrop = randomCrops[randomIndex.Next(4)];

                        break;

                    default: //"fall":

                        randomCrop = 404 + randomIndex.Next(4) * 2;

                        break;

                }

                targetLocation.dropObject(
                    new StardewValley.Object(
                        targetVector,
                        randomCrop,
                        null,
                        canBeSetDown: false,
                        canBeGrabbed: true,
                        isHoedirt: false,
                        isSpawnedObject: true
                    ),
                    new Vector2(targetVector.X * 64, targetVector.Y * 64),
                    Game1.viewport,
                    initialPlacement: true
                );

                castFire = true;

                castCost = 4;

            }
            else if (probability >= 4 && probability <= 15 && spawnIndex["grass"] && neighbourList.ContainsKey("Tree")) // 12/120 grass
            {

                StardewValley.TerrainFeatures.Grass grassFeature = new(1, 4);

                targetLocation.terrainFeatures.Add(targetVector, grassFeature);

                Microsoft.Xna.Framework.Rectangle tileRectangle = new((int)targetVector.X * 64 + 1, (int)targetVector.Y * 64 + 1, 62, 62);

                grassFeature.doCollisionAction(tileRectangle, 2, targetVector, null, targetLocation);

                castFire = true;

                castCost = 0;

            }
            //else if (probability >= 16 && probability <= 25 && spawnIndex["trees"]) // 10/120 tree
            else if (probability >= 4 && probability <= 13 && spawnIndex["trees"] && neighbourList.Count == 0) // 10/120 tree
            {

                List<int> treeIndex = new()
                {
                    1,2,3,1,2,3,1,2,3,7,8,
                };

                StardewValley.TerrainFeatures.Tree newTree = new(treeIndex[randomIndex.Next(11)], 1);

                //newTree.fertilized.Value = true;

                targetLocation.terrainFeatures.Add(targetVector, newTree);

                castCost = 2;

                castFire = true;

                ModUtility.AnimateGrowth(targetLocation, targetVector);

            }
            
        }

    }

}
