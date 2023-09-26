using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class Tree : Cast
    {

        public Tree(Mod mod, Vector2 target, Farmer player)
            : base(mod, target, player)
        {

        }

        public override void CastEarth()
        {

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.Tree)
            {

                return;

            }

            StardewValley.TerrainFeatures.Tree treeFeature = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.Tree;

            int probability = randomIndex.Next(10);

            if (treeFeature.growthStage.Value >= 5)
            {

                int debrisType = 388;

                int debrisAmount = 1;

                if (probability <= 2)
                {

                    debrisAmount = 2;

                }
                else if (probability <= 5)
                {

                    debrisAmount = 3;

                }

                if (treeFeature.treeType.Value == 8) //mahogany
                {

                    debrisType = 709; debrisAmount = 1;

                }

                if (treeFeature.treeType.Value == 7) // mushroom
                {

                    debrisType = 420; debrisAmount = 1;

                }

                for (int i = 0; i < debrisAmount; i++)
                {

                    Game1.createObjectDebris(debrisType, (int)targetVector.X, (int)targetVector.Y + 1);

                }

                if (!treeFeature.stump.Value)
                {

                    treeFeature.performToolAction(null, 1, targetVector, null);

                    treeFeature.health.Value += 1;

                }

                castFire = true;

                targetPlayer.gainExperience(2,2); // gain foraging experience

            }
            else if (treeFeature.fertilized.Value == false)
            {

                treeFeature.fertilize(Game1.currentLocation);

                castFire = true;

            }

        }

    }

}
