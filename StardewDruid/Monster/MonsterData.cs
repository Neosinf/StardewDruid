using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewValley.Locations;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Monster
{
    static class MonsterData
    {
        public static StardewValley.Monsters.Monster CreateMonster(int spawnMob, Vector2 spawnVector, int combatModifier)
        {

            StardewValley.Monsters.Monster theMonster;

            /*
              
             Medium
                Start + combat 2
                (2+1)^2 * 7 = 63
                Water + combat 5
                (2+2)^2 * 10 = 160
                Stars + combat 8
                (2+3)^2 * 13 = 325
             
             Hard
                Start + combat 2
                (3+1)^2 * 7 = 112
                Water + combat 5
                (3+2)^2 * 10 = 250
                Stars + combat 8
                (3+3)^2 * 13 = 468          

             */

            switch (spawnMob)
            {

                default: // Bat

                    theMonster = new StardewDruid.Monster.Bat(spawnVector, combatModifier);

                    break;

                case 0: // Green Slime

                    theMonster = new StardewDruid.Monster.Slime(spawnVector, combatModifier);

                    break;

                case 1: // Shadow Brute

                    theMonster = new StardewDruid.Monster.Shadow(spawnVector, combatModifier);

                    break;

                case 2: // Skeleton

                    theMonster = new StardewDruid.Monster.Skeleton(spawnVector, combatModifier);

                    break;

                case 3: // Golem

                    theMonster = new StardewDruid.Monster.Golem(spawnVector, combatModifier);

                    break;

                case 4: // DustSpirit

                    theMonster = new StardewDruid.Monster.Spirit(spawnVector, combatModifier);

                    break;

                // ------------------ Bosses

                case 11: // Bat

                    theMonster = new StardewDruid.Monster.BossBat(spawnVector, combatModifier);

                    break;

                case 12: // Shooter

                    theMonster = new StardewDruid.Monster.BossShooter(spawnVector, combatModifier);

                    break;

                case 13: // Slime

                    theMonster = new StardewDruid.Monster.BossSlime(spawnVector, combatModifier);

                    break;

                case 14: // Dino Monster

                    theMonster = new StardewDruid.Monster.BossDragon(spawnVector, combatModifier);

                    break;


            }

            return theMonster;

        }

    }

}
