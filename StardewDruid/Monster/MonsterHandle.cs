using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;

namespace StardewDruid.Monster
{
    public class MonsterHandle
    {

        //private Rite riteData;

        public List<StardewValley.Monsters.Monster> monsterSpawns;

        public int monstersLeft;

        public List<int> spawnIndex;

        public int spawnFrequency;

        public int spawnAmplitude;

        public int spawnCounter;

        public bool spawnChampion;

        public int championCounter;

        public int championInterval;

        public int championAmount;

        public int championLimit;

        public Vector2 spawnWithin;

        public Vector2 spawnRange;

        //public List<MonsterSpawn> spawnHandles;

        public int spawnTotal;

        public GameLocation spawnLocation;

        public int spawnCombat;

        public Random randomIndex;

        public MonsterHandle(Vector2 target, GameLocation location)
        {

            BaseTarget(target);

            spawnFrequency = 1;

            spawnAmplitude = 1;

            monsterSpawns = new();

            //spawnHandles = new();

            spawnIndex = new() { 99, };

            spawnLocation = location;

            randomIndex = new();

            spawnCombat = Mod.instance.CombatDifficulty();

            //riteData = rite;

        }

        public void BaseTarget(Vector2 target)
        {

            spawnWithin = target - new Vector2(4, 4);

            spawnRange = new Vector2(9, 9);

        }

        public void TargetToPlayer(Vector2 target,bool precision = false)
        {

            Vector2 playerVector = Game1.player.Tile;

            if (precision)
            {

                spawnWithin = playerVector - new Vector2(1,1);

                spawnRange = new Vector2(2, 2);

                return;

            }

            spawnWithin = playerVector + ((target - playerVector) / 2) - new Vector2(2, 2);

            spawnRange = new Vector2(5, 5);

        }

        public void ShutDown()
        {

            SpawnCheck();

            ModUtility.LogMonsters(monsterSpawns);

            for (int i = monsterSpawns.Count - 1; i >= 0; i--)
            {

                ModUtility.AnimateQuickWarp(spawnLocation, monsterSpawns[i].Position, true);

                monsterSpawns[i].Health = 0;

                spawnLocation.characters.Remove(monsterSpawns[i]);

                monsterSpawns.RemoveAt(i);

            }

            //spawnHandles = new();

            return;

        }

        public void SpawnCheck()
        {

            //monstersLeft = monsterSpawns.Count;

            /*for (int i = spawnHandles.Count - 1; i >= 0; --i)
            {

                if (spawnHandles[i].spawnComplete)
                {

                    monsterSpawns.Add(spawnHandles[i].targetMonster);

                    spawnHandles.RemoveAt(i);

                }

            }*/

            for (int i = monsterSpawns.Count - 1; i >= 0; i--)
            {

                if (!ModUtility.MonsterVitals(monsterSpawns[i],spawnLocation))
                {
                    
                    monsterSpawns.RemoveAt(i);

                    //monstersLeft--;

                }

            }

            monstersLeft = monsterSpawns.Count;

        }

        public int SpawnInterval()
        {
            
            spawnCounter--;

            if (spawnCounter > 0)
            {

                return 0;

            }

            spawnCounter = spawnFrequency;

            if (championInterval != 0)
            {

                if (championAmount == championLimit)
                {

                    championInterval = 0;

                }

                championCounter++;

                if(championCounter == championInterval)
                {

                    spawnChampion = true;

                    championAmount++;

                    championCounter = 0;

                }

            }

            int spawnAmount = 0;

            Vector2 spawnVector;

            for (int i = 0; i < spawnAmplitude; i++)
            {

                spawnVector = SpawnVector();

                if (spawnVector.X >= 0)
                {

                    SpawnGround(spawnVector);

                    spawnAmount++;

                    spawnTotal++;

                }

            }

            return spawnAmount;

        }

        public Vector2 SpawnVector(int spawnLimit = 4, int fromX = -1, int fromY = -1, int spawnX = -1, int spawnY = -1)
        {

            Vector2 spawnVector = new(-1);

            Vector2 playerVector = Game1.player.Tile;

            int spawnAttempt = 0;
            
            Vector2 fromVector = new(fromX, fromY); 

            if (fromX == -1)
            {

                fromVector = spawnWithin;

            }

            if (spawnX == -1)
            {

                spawnX = (int)spawnRange.X;

                spawnY = (int)spawnRange.Y;

            }

            while (spawnAttempt++ < spawnLimit)
            {

                int offsetX = randomIndex.Next(spawnX);

                int offsetY = randomIndex.Next(spawnY);

                Vector2 offsetVector = new(offsetX, offsetY);

                spawnVector = fromVector + offsetVector;

                if (Math.Abs(spawnVector.X - playerVector.X) <= 1 && Math.Abs(spawnVector.Y - playerVector.Y) <= 1)
                {
                    continue;
                }

                if (ModUtility.GroundCheck(spawnLocation, spawnVector, true) != "ground")
                {
                    continue;
                }

                if (ModUtility.NeighbourCheck(spawnLocation, spawnVector, 0).Count > 0)
                {
                    continue;
                }

                return spawnVector;

            }

            return spawnVector;

        }

        public StardewValley.Monsters.Monster SpawnGround(Vector2 spawnVector)
        {

            int spawnMob = spawnIndex[randomIndex.Next(spawnIndex.Count)];

            StardewValley.Monsters.Monster theMonster = MonsterData.CreateMonster(spawnMob, spawnVector, spawnCombat, spawnChampion);

            if (spawnChampion) { spawnChampion = false; }

            monsterSpawns.Add(theMonster);

            spawnLocation.characters.Add(theMonster);

            theMonster.currentLocation = spawnLocation;

            theMonster.update(Game1.currentGameTime, spawnLocation);

            spawnTotal++;

            //MonsterSpawn monsterSpawn = new(spawnLocation, theMonster);

            //monsterSpawn.InitiateMonster(150);

            //spawnHandles.Add(monsterSpawn);

            ModUtility.AnimateQuickWarp(spawnLocation, spawnVector * 64 - new Vector2(0, 32));

            // ------------------------------ monster

            return theMonster;

        }

        public void SpawnImport(StardewValley.Monsters.Monster theMonster)
        {

            monsterSpawns.Add(theMonster);

            spawnLocation.characters.Add(theMonster);

            theMonster.currentLocation = spawnLocation;

            theMonster.update(Game1.currentGameTime, spawnLocation);

            spawnTotal++;

            //MonsterSpawn monsterSpawn = new(spawnLocation, theMonster);

            //monsterSpawn.InitiateMonster(150);

            //spawnHandles.Add(monsterSpawn);

            ModUtility.AnimateQuickWarp(spawnLocation, theMonster.Position - new Vector2(0, 32));

        }

        /*public StardewValley.Monsters.Monster SpawnTerrain(Vector2 spawnVector, Vector2 terrainVector, bool splash)
        {

            int spawnMob = spawnIndex[randomIndex.Next(spawnIndex.Count)];

            StardewValley.Monsters.Monster theMonster = MonsterData.CreateMonster(spawnMob, spawnVector, spawnCombat);

            Vector2 fromPosition = new(terrainVector.X * 64, terrainVector.Y * 64);

            Vector2 toPosition = new(spawnVector.X * 64, spawnVector.Y * 64);

            float animationInterval = 125f;

            float motionX = (toPosition.X - fromPosition.X) / 1000;

            float compensate = 0.555f;

            float motionY = (toPosition.Y - fromPosition.Y) / 1000 - compensate;

            float animationSort =(terrainVector.Y / 10000);

            Color monsterColor = Color.White;

            if (theMonster is GreenSlime)
            {
                GreenSlime slimeMonster = (GreenSlime)theMonster;

                monsterColor = slimeMonster.color.Value;
            }

            MonsterSpawn monsterSpawn = new(spawnLocation, theMonster);

            monsterSpawn.InitiateMonster(1000);

            spawnHandles.Add(monsterSpawn);

            // ----------------------------- animation

            if (MonsterData.BossMonster(theMonster))
            {
                monsterSpawn.InitiateMonster(150);

                return theMonster;

            }

            string textureName = theMonster.Sprite.textureName.Value;

            Rectangle targetRectangle = theMonster.Sprite.SourceRect;

            TemporaryAnimatedSprite monsterSprite = new(textureName, targetRectangle, animationInterval, 4, 2, fromPosition, flicker: false, flipped: false, animationSort, 0f, monsterColor, 4f, 0f, 0f, 0f)
            {

                motion = new Vector2(motionX, motionY),

                acceleration = new Vector2(0f, 0.001f),

                timeBasedMotion = true,

            };

            spawnLocation.temporarySprites.Add(monsterSprite);

            ModUtility.AnimateQuickWarp(spawnLocation, fromPosition);

            if (splash)
            {

                ModUtility.AnimateSplash(spawnLocation, terrainVector, true);

            }

            // ------------------------------ monster

            return theMonster;

        }*/

    }

}
