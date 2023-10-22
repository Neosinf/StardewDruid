﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class Sapling : CastHandle
    {

        public Sapling(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            if (rite.caster.ForagingLevel >= 8)
            {

                castCost = 1;

            }

        }

        public override void CastEarth()
        {

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.Tree treeFeature)
            {

                return;

            }

            treeFeature.dayUpdate(targetLocation,targetVector);

            Utility.addSprinklesToLocation(targetLocation, (int)targetVector.X, (int)targetVector.Y, 1, 1, 1000, 500, new Color(0.8f, 1, 0.8f, 1));

            castFire = true;

        }

    }

}
