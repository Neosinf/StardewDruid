using Force.DeepCloner;
using Microsoft.Xna.Framework;
using Netcode;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Minigames;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast.Mists
{
    internal class Boulder : CastHandle
    {

        private ResourceClump resourceClump;

        public Boulder(Vector2 target, Rite rite, ResourceClump ResourceClump)
            : base(target, rite)
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

            ModUtility.DestroyBoulder(targetLocation, targetPlayer, resourceClump, targetVector, Mod.instance.virtualPick, debrisMax);
           
            resourceClump = null;

            castFire = true;

            castCost = 24;

            ModUtility.AnimateBolt(targetLocation, targetVector);

        }

    }
}
