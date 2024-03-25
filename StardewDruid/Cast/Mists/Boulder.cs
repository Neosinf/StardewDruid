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

            resourceClump = ResourceClump;

        }

        public override void CastEffect()
        {

            int debrisMax = 1;

            if (targetPlayer.professions.Contains(22))
            {
                debrisMax = 2;

            }

            castCost = Math.Max(2, 36 - targetPlayer.MiningLevel * Mod.instance.virtualPick.UpgradeLevel);

            ModUtility.DestroyBoulder(targetLocation, targetPlayer, resourceClump, targetVector, debrisMax);

            resourceClump = null;

            castFire = true;

            castCost = 24;

            ModUtility.AnimateBolt(targetLocation, targetVector);

        }

    }
}
