﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;
using StardewValley.Monsters;

namespace StardewDruid.Cast.Earth
{
    internal class FruitTree : CastHandle
    {

        public FruitTree(Vector2 target, Rite rite)
            : base(target, rite)
        {

            castCost = 0;

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

            treeFeature.performUseAction(targetVector, targetLocation);

            castFire = true;

        }

    }

}