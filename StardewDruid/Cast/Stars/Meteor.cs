using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;


namespace StardewDruid.Cast.Stars
{
    internal class Meteor : CastHandle
    {

        int targetDirection;

        float damage;

        public Meteor(Vector2 target,  float Damage)
            : base(target)
        {

            castCost = Math.Max(6, 14 - Game1.player.CombatLevel);

            targetDirection = Game1.player.FacingDirection;

            damage = Damage;

        }

        public override void CastEffect()
        {

            ModUtility.AnimateMeteor(targetLocation, targetVector, targetDirection < 2);

            ModUtility.AnimateCursor(targetLocation, targetVector * 64, targetVector * 64, "Stars", 1000);

            DelayedAction.functionAfterDelay(MeteorImpact, 1000);

            Game1.currentLocation.playSound("fireball");

            castFire = true;

        }

        public void MeteorImpact()
        {

            if (targetLocation != Game1.currentLocation)
            {

                return;

            }

            List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(targetLocation, new() { targetVector * 64, }, 3, true);

            if (!Mod.instance.TaskList().ContainsKey("masterMeteor"))
            {

                for(int i = monsters.Count - 1; i >= 0; i--)               
                {

                    Mod.instance.UpdateTask("lessonMeteor", 1);

                }

            }

            ModUtility.DamageMonsters(targetLocation, monsters, targetPlayer,(int)damage, true);

            ModUtility.Explode(targetLocation, targetVector, targetPlayer, 3, powerLevel:3);

            ModUtility.AnimateImpact(targetLocation, targetVector, 1, 2);

            Game1.currentLocation.playSound("flameSpellHit");

            castFire = true;

        }

    }

}
