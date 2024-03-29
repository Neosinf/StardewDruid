﻿using Microsoft.Xna.Framework;
using StardewValley;

namespace StardewDruid.Cast.Weald
{
    internal class Sapling : CastHandle
    {

        public Sapling(Vector2 target)
            : base(target)
        {

            if (Game1.player.ForagingLevel >= 8)
            {

                castCost = 1;

            }

        }

        public override void CastEffect()
        {

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.Tree treeFeature)
            {

                return;

            }

            if (treeFeature.growthStage.Value < 3)
            {

                treeFeature.growthStage.Value++;

            }
            else
            {

                treeFeature.dayUpdate();

            }

            //Utility.addSprinklesToLocation(targetLocation, (int)targetVector.X, (int)targetVector.Y, 1, 1, 1000, 500, new Color(0.8f, 1, 0.8f, 1));
            ModUtility.AnimateSparkles(targetLocation, targetVector, Color.White);

            castFire = true;

        }

    }

}
