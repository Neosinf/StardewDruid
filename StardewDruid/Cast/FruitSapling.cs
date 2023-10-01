using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class FruitSapling : Cast
    {

        public FruitSapling(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

        }

        public override void CastEarth()
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

            treeFeature.dayUpdate(targetLocation,targetVector);

            castCost = 8;

            castFire = true;

        }

    }

}
