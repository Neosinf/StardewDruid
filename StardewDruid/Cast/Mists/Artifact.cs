using Microsoft.Xna.Framework;
using System;

namespace StardewDruid.Cast.Mists
{
    internal class Artifact : CastHandle
    {

        public Artifact(Vector2 target, Rite rite)
            : base(target, rite)
        {

        }

        public override void CastEffect()
        {
            if (!targetLocation.objects.ContainsKey(targetVector))
            {
                return;

            }

            StardewValley.Object artifactSpot = targetLocation.objects[targetVector];

            if (artifactSpot == null)
            {
                return;

            }

            Mod.instance.virtualHoe.DoFunction(targetLocation, 0, 0, 1, targetPlayer);

            //targetPlayer.Stamina += Math.Min(2, targetPlayer.MaxStamina - targetPlayer.Stamina);

            artifactSpot.performToolAction(Mod.instance.virtualHoe, targetLocation);

            castFire = true;

            castCost = 8;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

    }
}
