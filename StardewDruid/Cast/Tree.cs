using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class Tree : Cast
    {

        private readonly StardewValley.TerrainFeatures.Tree treeFeature;

        public Tree(Mod mod, Vector2 target, Farmer player, StardewValley.TerrainFeatures.Tree TreeFeature)
            : base(mod, target, player)
        {

            treeFeature = TreeFeature;

        }

        public override void CastEarth()
        {

            int probability = randomIndex.Next(10);

            if (treeFeature.growthStage.Value >= 5)
            {

                Game1.createObjectDebris(388, (int)targetVector.X, (int)targetVector.Y);

                Game1.createObjectDebris(388, (int)targetVector.X, (int)targetVector.Y);

                if (probability >= 3)
                {

                    Game1.createObjectDebris(388, (int)targetVector.X + 1, (int)targetVector.Y);

                    Game1.createObjectDebris(388, (int)targetVector.X, (int)targetVector.Y);
                };

                if (probability >= 6)
                {

                    Game1.createObjectDebris(388, (int)targetVector.X, (int)targetVector.Y + 1);

                    Game1.createObjectDebris(388, (int)targetVector.X, (int)targetVector.Y);

                };

                if (probability == 9)
                {

                    Game1.createObjectDebris(388, (int)targetVector.X + 1, (int)targetVector.Y + 1);

                };

                if (!treeFeature.stump.Value)
                {

                    treeFeature.performToolAction(null, 1, targetVector, null);

                }

                castFire = true;

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
