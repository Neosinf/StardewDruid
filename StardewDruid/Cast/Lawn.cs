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

        public Lawn(Mod mod, Vector2 target, Farmer player, Dictionary<string, bool> SpawnIndex)
            : base(mod, target, player)
        {

            spawnIndex = SpawnIndex;

        }

        public override void CastEarth()
        {

            int probability = randomIndex.Next(80);

            if (probability <= 1 && spawnIndex["flower"]) // 2/80 flower
            {

                Dictionary<int, int> objectIndexes;

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

                 castCost = 8;

            }
            else if (probability >= 2 && probability <= 3 && spawnIndex["forage"]) // 2/80 forage
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
            else if (probability >= 4 && probability <= 19 && spawnIndex["grass"]) // 16/80 grass
            {

                StardewValley.TerrainFeatures.Grass grassFeature = new(1, 4);

                targetLocation.terrainFeatures.Add(targetVector, grassFeature);

                castFire = true;

                Microsoft.Xna.Framework.Rectangle tileRectangle = new((int)targetVector.X * 64 + 1, (int)targetVector.Y * 64 + 1, 62, 62);

                grassFeature.doCollisionAction(tileRectangle, 2, targetVector, null, targetLocation);

                castCost = 0;

            }
            else if (probability >= 20 && probability <= 31 && spawnIndex["trees"]) // 12/80 tree
            {
                
                bool plantSeed = true;

                List<Vector2> neighbourVectors = ModUtility.GetTilesWithinRadius(targetLocation, targetVector, 1);

                Layer buildingLayer = targetLocation.Map.GetLayer("Buildings");

                foreach (Vector2 neighbourVector in neighbourVectors)
                {

                    if (!plantSeed)
                    {
                        break;
                    }

                    Tile buildingTile = buildingLayer.PickTile(new Location((int)neighbourVector.X * 64, (int)neighbourVector.Y * 64), Game1.viewport.Size);

                    if (buildingTile != null)
                    {

                        if (buildingTile.TileIndexProperties.TryGetValue("Passable", out _) == false)
                        {
                            plantSeed = false;

                        }

                        continue;

                    }

                    if (targetLocation.terrainFeatures.ContainsKey(neighbourVector))
                    {
                        var terrainFeature = targetLocation.terrainFeatures[neighbourVector];

                        switch (terrainFeature.GetType().Name.ToString())
                        {

                            case "Tree":

                                plantSeed = false;

                                break;

                            case "HoeDirt":

                                HoeDirt hoeDirt = terrainFeature as HoeDirt;

                                if (hoeDirt.crop != null)
                                {

                                    plantSeed = false;

                                }

                                break;

                            default:

                                break;

                        }
                    }

                }

                if (plantSeed)
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

}
