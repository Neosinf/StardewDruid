﻿using StardewValley;

namespace StardewDruid.Monster
{
    public class MonsterSpawn
    {

        public StardewValley.Monsters.Monster targetMonster;

        public GameLocation targetLocation;

        public bool spawnComplete;

        public int spawnHealth;

        public int spawnDamage;

        public MonsterSpawn(GameLocation Location, StardewValley.Monsters.Monster Monster)
        {

            targetLocation = Location;

            targetMonster = Monster;

            spawnHealth = targetMonster.Health;

            spawnDamage = targetMonster.DamageToFarmer;

        }

        public void InitiateMonster(int delayTimer)
        {

            DelayedAction.functionAfterDelay(ManifestMonster, delayTimer);

        }

        public void ManifestMonster()
        {

            targetLocation.characters.Add(targetMonster);

            targetMonster.currentLocation = targetLocation;

            targetMonster.update(Game1.currentGameTime, targetLocation);

            spawnComplete = true;

        }

        public void ActivateMonster()
        {

            targetMonster.MaxHealth = spawnHealth;

            targetMonster.Health = spawnHealth;

            targetMonster.DamageToFarmer = spawnDamage;

        }


    }
}
