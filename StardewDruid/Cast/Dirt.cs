using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast
{
    internal class Dirt : Cast
    {

        private readonly Dictionary<string, bool> spawnIndex;

        public Dirt(Mod mod, Vector2 target, Farmer player, Dictionary<string, bool> SpawnIndex)
            : base(mod, target, player)
        {

            spawnIndex = SpawnIndex;

        }

        public override void CastEarth()
        {
            
            int probability = randomIndex.Next(10);

            if (probability <= 2 && spawnIndex["grass"]) // 3/10 grass
            {

                targetLocation.terrainFeatures.Add(targetVector, new StardewValley.TerrainFeatures.Grass(1, 4));

                castCost = 0;

                castFire = true;

            }
            else if (probability >= 3 && probability <= 4 && spawnIndex["trees"]) // 1/10 tree
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

                    StardewValley.TerrainFeatures.Tree newTree;

                    if (targetLocation.Name == "Desert")
                    {
                        
                        newTree = new(9, 1);

                    }
                    else
                    {

                        List<int> treeIndex = new()
                        {
                            1,2,3,1,2,3,1,2,3,7,8,
                        };

                       newTree = new(treeIndex[randomIndex.Next(11)], 1);

                    };

                    newTree.fertilized.Value = true;

                    targetLocation.terrainFeatures.Add(targetVector, newTree);

                    castFire = true;

                    ModUtility.AnimateGrowth(targetLocation,targetVector);

                }

            }

            return;

        }

    }
}
