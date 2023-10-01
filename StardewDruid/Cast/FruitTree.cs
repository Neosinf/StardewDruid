using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;
using StardewValley.Monsters;

namespace StardewDruid.Cast
{
    internal class FruitTree : Cast
    {

        public FruitTree(Mod mod, Vector2 target, Rite rite)
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

            //treeFeature.performToolAction(null, 1, targetVector, null);

            treeFeature.performUseAction(targetVector, targetLocation);

            castCost = 0;

            castFire = true;

        }

    }

}
