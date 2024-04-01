using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;

namespace StardewDruid.Cast.Mists
{
    internal class Stump : CastHandle
    {

        private ResourceClump resourceClump;

        private string resourceType;

        public Stump(Vector2 target,  ResourceClump ResourceClump, string ResourceType)
            : base(target)
        {

            castCost = Math.Max(8, 32 - targetPlayer.ForagingLevel * 3);

            resourceClump = ResourceClump;

            resourceType = ResourceType;

        }

        public override void CastEffect()
        {

            if (resourceClump == null)
            {

                return;

            }

            ModUtility.DestroyStump(targetLocation, targetPlayer, resourceClump, targetVector, resourceType);

            resourceClump = null;

            castFire = true;

            ModUtility.AnimateBolt(targetLocation, targetVector * 64 + new Vector2(32));

            return;

        }

    }
}
