using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class Smite: Cast
    {

        private StardewValley.Monsters.Monster targetMonster;

        public Smite(Mod mod, Vector2 target, Rite rite, StardewValley.Monsters.Monster TargetMonster)
            : base(mod, target, rite)
        {

            int castCombat = rite.caster.CombatLevel / 2;

            castCost = Math.Max(4, 10-castCombat);

            targetMonster = TargetMonster;

        }

        public override void CastWater()
        {

            Rectangle areaOfEffect = new(
                ((int)targetVector.X * 64) - 32,
                ((int)targetVector.Y * 64) - 32,
                128,
                128
            );

            targetLocation.damageMonster(areaOfEffect, riteData.castDamage * 2, riteData.castDamage * 3, true, targetPlayer);

            ModUtility.AnimateBolt(targetLocation, new Vector2(targetVector.X, targetVector.Y - 1));

            if (randomIndex.Next(2) == 0)
            {

                castLimit = true;

            }

            castFire = true;

            return;

        }

    }

}
