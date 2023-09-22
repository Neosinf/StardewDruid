using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class Smite: Cast
    {

        private StardewValley.Monsters.Monster targetMonster;

        public Smite(Mod mod, Vector2 target, Farmer player, StardewValley.Monsters.Monster TargetMonster)
            : base(mod, target, player)
        {

            targetMonster = TargetMonster;

        }

        public override void CastWater()
        {

            Rectangle areaOfEffect = new(
                ((int)targetVector.X - 1) * 64,
                ((int)targetVector.Y - 1) * 64,
                192,
                192
            );

            targetLocation.damageMonster(areaOfEffect, 999, 999, true, targetPlayer);

            ModUtility.AnimateBolt(targetLocation, new Vector2(targetVector.X, targetVector.Y - 1), "flameSpellHit");

            if (randomIndex.Next(2) == 0)
            {

                castLimit = true;

            }

            castFire = true;

            castCost = 12;

            return;

        }


    }
}
