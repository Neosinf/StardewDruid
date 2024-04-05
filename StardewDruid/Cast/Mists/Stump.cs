using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;

namespace StardewDruid.Cast.Mists
{
    internal class Stump : CastHandle
    {

        private ResourceClump resourceClump;

        public Stump(Vector2 target,  ResourceClump Resource)
            : base(target)
        {

            castCost = Math.Max(8, 32 - targetPlayer.ForagingLevel * 3);

            if(Resource.parentSheetIndex.Value == ResourceClump.hollowLogIndex)
            {

                castCost = (int)(castCost * 1.5);

            }

            resourceClump = Resource;

        }

        public override void CastEffect()
        {

            if (resourceClump == null)
            {

                return;

            }

            ModUtility.DestroyStump(targetLocation, targetPlayer, resourceClump, targetVector);

            resourceClump = null;

            castFire = true;

            ModUtility.AnimateBolt(targetLocation, targetVector * 64 + new Vector2(32));

            return;

        }

    }
}
