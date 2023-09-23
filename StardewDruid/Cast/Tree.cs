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

            StardewValley.TerrainFeatures.Tree treeFeature = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.Tree;

            int probability = randomIndex.Next(10);

            if (treeFeature.growthStage.Value >= 5)
            {

                Game1.createObjectDebris(388, (int)targetVector.X, (int)targetVector.Y);

                Game1.createObjectDebris(388, (int)targetVector.X, (int)targetVector.Y);

                if (probability >= 3)
                {

                    Game1.createObjectDebris(388, (int)targetVector.X + 1, (int)targetVector.Y);

                    //Game1.createObjectDebris(388, (int)targetVector.X, (int)targetVector.Y);
                };

                if (probability >= 6)
                {

                    Game1.createObjectDebris(388, (int)targetVector.X, (int)targetVector.Y + 1);

                    //Game1.createObjectDebris(388, (int)targetVector.X, (int)targetVector.Y);

                };

                //if (probability == 9)
                //{

                    //Game1.createObjectDebris(388, (int)targetVector.X + 1, (int)targetVector.Y + 1);

                //};

                if (!treeFeature.stump.Value)
                {

                    treeFeature.performToolAction(null, 1, targetVector, null);

                }

                castFire = true;

                targetPlayer.gainExperience(2,4); // gain foraging experience

            }
            else
            {

                if (probability >= 7 && treeFeature.fertilized.Value == false)
                {

                    treeFeature.fertilize(Game1.currentLocation);

                    castFire = true;

                }

            };

        }

    }

}
