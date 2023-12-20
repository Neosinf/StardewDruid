using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast.Stars
{
    internal class Meteor : CastHandle
    {

        int targetDirection;

        int meteorRange;

        public Meteor(Vector2 target, Rite rite, int range = 2)
            : base(target, rite)
        {

            castCost = Math.Max(6, 14 - Game1.player.CombatLevel);

            targetDirection = rite.direction;

            meteorRange = range;

        }

        public override void CastEffect()
        {

            //ModUtility.AnimateMeteorZone(targetLocation, targetVector, new Color(1f, 0.4f, 0.4f, 1));

            ModUtility.AnimateMeteor(targetLocation, targetVector, targetDirection < 2);

            DelayedAction.functionAfterDelay(MeteorImpact, 600);

            castFire = true;

        }

        public void MeteorImpact()
        {

            if (targetLocation != Game1.currentLocation)
            {

                return;

            }

            ModUtility.Explode(targetLocation, targetVector, targetPlayer, meteorRange, riteData.castDamage, 2, Mod.instance.virtualPick, Mod.instance.virtualAxe);

            if (randomIndex.Next(6) == 0) { Game1.currentLocation.playSound("fireball"); }

            castFire = true;

        }

    }
}
