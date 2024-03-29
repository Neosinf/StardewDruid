﻿using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast.Weald
{
    internal class FruitSapling : CastHandle
    {

        public FruitSapling(Vector2 target)
            : base(target)
        {

            castCost = Math.Max(2, 16 - Game1.player.FarmingLevel);

        }

        public override void CastEffect()
        {

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.FruitTree)
            {

                return;

            }

            StardewValley.TerrainFeatures.FruitTree treeFeature = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.FruitTree;

            treeFeature.dayUpdate();

            ModUtility.AnimateSparkles(targetLocation, targetVector, Color.White);

            Game1.playSound("yoba");

            castFire = true;

        }

    }

}
