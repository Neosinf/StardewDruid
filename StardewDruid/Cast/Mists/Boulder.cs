using Microsoft.Xna.Framework;
using StardewValley.TerrainFeatures;
using System;

namespace StardewDruid.Cast.Mists
{
    internal class Boulder : CastHandle
    {

        private ResourceClump resourceClump;

        public Boulder(Vector2 target,  ResourceClump ResourceClump)
            : base(target)
        {

            castCost = Math.Max(8, 32 - targetPlayer.MiningLevel * 3);

            resourceClump = ResourceClump;

        }

        public override void CastEffect()
        {

            ModUtility.DestroyBoulder(targetLocation, targetPlayer, resourceClump, targetVector);

            resourceClump = null;

            castFire = true;

            ModUtility.AnimateBolt(targetLocation, targetVector * 64 + new Vector2(32));

        }

    }
}
