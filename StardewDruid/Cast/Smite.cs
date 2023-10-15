using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Tools;
using System;

namespace StardewDruid.Cast
{
    internal class Smite: CastHandle
    {

        private StardewValley.Monsters.Monster targetMonster;

        public Smite(Mod mod, Vector2 target, Rite rite, StardewValley.Monsters.Monster TargetMonster)
            : base(mod, target, rite)
        {

            int castCombat = rite.caster.CombatLevel / 2;

            castCost = Math.Max(6, 12-castCombat);

            targetMonster = TargetMonster;

        }

        public override void CastWater()
        {

            /*Rectangle areaOfEffect = new(
                ((int)targetVector.X * 64) - 32,
                ((int)targetVector.Y * 64) - 32,
                128,
                128
            );*/

            //targetLocation.damageMonster(areaOfEffect, riteData.castDamage * 2, riteData.castDamage * 3, true, targetPlayer);

            float critChance = 0.05f;

            if (!riteData.castTask.ContainsKey("masterSmite"))
            {

                mod.UpdateTask("lessonSmite", 1);

            }
            else
            {

                critChance = 0.35f;

            }

            Rectangle areaOfEffect = new(
                ((int)targetVector.X * 64)+2,
                ((int)targetVector.Y * 64)+2,
                60,
                60
            );
            
            targetLocation.damageMonster(areaOfEffect, riteData.castDamage, riteData.castDamage * 2, false, 0f, targetPlayer.CombatLevel, critChance, 2, false, targetPlayer);

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
