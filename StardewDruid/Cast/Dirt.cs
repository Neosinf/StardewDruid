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

            if (probability <= 1 && spawnIndex["grass"]) // 2/10 grass
            {

                targetLocation.terrainFeatures.Add(targetVector, new StardewValley.TerrainFeatures.Grass(1, 4));

                castCost = 0;

                castFire = true;

            }
            else if (probability <= 2 && spawnIndex["trees"]) // 1/10 tree
            {

                if (ModUtility.CheckSeed(targetLocation, targetVector))
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

                    //newTree.fertilized.Value = true;

                    targetLocation.terrainFeatures.Add(targetVector, newTree);

                    castFire = true;

                    ModUtility.AnimateGrowth(targetLocation,targetVector);

                }

            }
            else if (probability <= 5) // 3/10 hoe dirt
            {

                targetLocation.makeHoeDirt(targetVector);

                mod.UpdateEarthCasts(targetLocation, targetVector, false);

            }

            return;

        }

    }
}
