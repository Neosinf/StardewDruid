using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Collections.Generic;


namespace StardewDruid.Cast.Stars
{
    internal class Meteor : CastHandle
    {

        int targetDirection;

        public Meteor(Vector2 target, Rite rite)
            : base(target, rite)
        {

            castCost = Math.Max(6, 14 - Game1.player.CombatLevel);

            targetDirection = rite.direction;

        }

        public override void CastEffect()
        {

            ModUtility.AnimateMeteor(targetLocation, targetVector, targetDirection < 2);

            ModUtility.AnimateRadiusDecoration(targetLocation, targetVector, "Stars", 0.75f, 0.75f, 1000);

            DelayedAction.functionAfterDelay(MeteorImpact, 600);

            if (randomIndex.Next(2) == 0) { Game1.currentLocation.playSound("fireball"); }

            castFire = true;

        }

        public void MeteorImpact()
        {

            if (targetLocation != Game1.currentLocation)
            {

                return;

            }

            float castDamage = riteData.castDamage;

            List<Vector2> impactVectors = ModUtility.Explode(targetLocation, targetVector, targetPlayer, 2, (int)castDamage, powerLevel:2);

            foreach(Vector2 vector in impactVectors)
            {
                
                ModUtility.ImpactVector(targetLocation, vector);

            }

            if (randomIndex.Next(2) == 0) { Game1.currentLocation.playSound("flameSpellHit"); }

            castFire = true;

        }

    }

}
