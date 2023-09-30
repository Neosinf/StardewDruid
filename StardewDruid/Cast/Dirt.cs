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


        public Dirt(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            spawnIndex = rite.spawnIndex;

        }

        public override void CastEarth()
        {
            
            if(targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                mod.UpdateEarthCasts(targetLocation, targetVector, false);

                return;

            }

            Dictionary<string, List<Vector2>> neighbourList = ModUtility.NeighbourCheck(targetLocation, targetVector);

            int probability = randomIndex.Next(10);

            if (probability <= 2 && spawnIndex["grass"] && neighbourList.ContainsKey("Tree")) // 3/10 grass
            {

                StardewValley.TerrainFeatures.Grass grassFeature = new(1, 4);

                targetLocation.terrainFeatures.Add(targetVector, grassFeature);

                Microsoft.Xna.Framework.Rectangle tileRectangle = new((int)targetVector.X * 64 + 1, (int)targetVector.Y * 64 + 1, 62, 62);

                grassFeature.doCollisionAction(tileRectangle, 2, targetVector, null, targetLocation);

                castFire = true;

                castCost = 0;

            }
            else if (probability == 3 && spawnIndex["trees"] && neighbourList.Count == 0) // 1/10 tree
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
            else if (neighbourList.ContainsKey("Crop") && !neighbourList.ContainsKey("Tree") && !neighbourList.ContainsKey("Sapling")) // 3/10 hoe dirt
            {

                targetLocation.makeHoeDirt(targetVector);

            }

            if (!castFire)
            {

                mod.UpdateEarthCasts(targetLocation, targetVector, false);

            }

            return;

        }

    }
}
