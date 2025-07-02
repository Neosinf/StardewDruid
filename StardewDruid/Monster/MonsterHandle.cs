using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewValley;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.IO;

namespace StardewDruid.Monster
{

    public class SpawnHandle
    {

        public MonsterHandle.bosses boss;

        public Monster.Boss.temperment temperment;

        public Monster.Boss.difficulty difficulty;

        public SpawnHandle(MonsterHandle.bosses Boss, Monster.Boss.temperment Temperment, Monster.Boss.difficulty Difficulty)
        {
            boss = Boss;
            temperment = Temperment;
            difficulty = Difficulty;

        }

        public SpawnHandle(MonsterHandle.bosses Boss, int Temperment = 4, int Difficulty = 0)
        {
            boss = Boss;
            temperment = (Monster.Boss.temperment)Temperment;
            difficulty = (Monster.Boss.difficulty)Difficulty;

        }

    }

    public class MonsterHandle
    {

        public List<StardewDruid.Monster.Boss> monsterSpawns = new();

        public int monstersLeft;

        public enum bosses
        {

            batwing,
            dustfiend,
            firefiend,
            jellyfiend,
            darkbrute,
            darkshooter,
            spectre,
            phantom,
            demonki,
            dino,
            dragon,
            gargoyle,
            goblin,
            reaper,
            rogue,
            scavenger,
            shadowfox,
            shadowbear,
            shadowwolf,

        }

        public Dictionary<int,List<SpawnHandle>> spawnSchedule = new();

        public int spawnCounter;

        public Vector2 spawnWithin;

        public Vector2 spawnRange;

        public int spawnLimit;

        public int spawnTotal;

        public GameLocation spawnLocation;

        public int spawnCombat;

        public Random randomIndex;

        public bool spawnWater;

        public bool spawnVoid;

        public bool spawnGroup;

        public MonsterHandle(Vector2 target, GameLocation location)
        {

            BaseTarget(target);

            spawnLocation = location;

            spawnCombat = Mod.instance.CombatDifficulty();

            spawnWater = false;

            spawnLimit = 5 + (Mod.instance.ModDifficulty() / 2);

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

            for (int i = monsterSpawns.Count - 1; i >= 0; i--)
            {

                Mod.instance.iconData.AnimateQuickWarp(spawnLocation, monsterSpawns[i].Position, true, monsterSpawns[i].warp);

                monsterSpawns[i].Health = 0;

                spawnLocation.characters.Remove(monsterSpawns[i]);

                monsterSpawns.RemoveAt(i);

            }

            return;

        }

        public void SpawnCheck()
        {

            for (int i = monsterSpawns.Count - 1; i >= 0; i--)
            {

                if (!ModUtility.MonsterVitals(monsterSpawns[i],spawnLocation))
                {
                    
                    monsterSpawns.RemoveAt(i);

                }

            }

            monstersLeft = monsterSpawns.Count;

        }

        public int SpawnInterval()
        {
            
            spawnCounter++;

            int spawnAmount = 0;

            Vector2 spawnVector;

            if (!spawnSchedule.ContainsKey(spawnCounter))
            {

                return 0;

            }

            if(monsterSpawns.Count >= spawnLimit)
            {

                return 0;

            }

            for (int i = 0; i < spawnSchedule[spawnCounter].Count; i++)
            {

                for(int j = 0; j < 4; j++)
                {

                    spawnVector = SpawnVector();

                    if (spawnVector.X > 0)
                    {

                        SpawnGround(spawnVector, spawnSchedule[spawnCounter][i]);

                        spawnAmount++;

                        break;

                    }

                }

            }

            return spawnAmount;

        }

        public Vector2 SpawnVector(int spawnTries = 4, int fromX = -1, int fromY = -1, int spawnX = -1, int spawnY = -1)
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

            while (spawnAttempt++ < spawnTries)
            {

                int offsetX = Mod.instance.randomIndex.Next(spawnX);

                int offsetY = Mod.instance.randomIndex.Next(spawnY);

                Vector2 offsetVector = new(offsetX, offsetY);

                spawnVector = fromVector + offsetVector;

                if (Math.Abs(spawnVector.X - playerVector.X) <= 1 && Math.Abs(spawnVector.Y - playerVector.Y) <= 1)
                {
                    continue;
                }

                string groundCheck = ModUtility.GroundCheck(spawnLocation, spawnVector, true);

                if (groundCheck == "void" && !spawnVoid)
                {

                    continue;

                }
                else if (groundCheck == "water" && !spawnWater)
                {

                    continue;

                }
                else if (groundCheck != "ground")
                {
                    
                    continue;
               
                }
                else if (ModUtility.NeighbourCheck(spawnLocation, spawnVector, 0, 0).Count > 0)
                {
                    
                    continue;
                
                }

                return spawnVector;

            }

            return spawnVector;

        }

        public StardewDruid.Monster.Boss SpawnGround(Vector2 spawnVector, SpawnHandle spawnProfile)
        {

            StardewDruid.Monster.Boss theMonster = CreateMonster(spawnProfile.boss, spawnVector, spawnCombat, spawnProfile.difficulty, spawnProfile.temperment);

            monsterSpawns.Add(theMonster);

            theMonster.groupMode = spawnGroup;

            spawnLocation.characters.Add(theMonster);

            theMonster.currentLocation = spawnLocation;

            theMonster.update(Game1.currentGameTime, spawnLocation);

            spawnTotal++;

            Mod.instance.iconData.AnimateQuickWarp(spawnLocation, spawnVector * 64 - new Vector2(0, 32), true, theMonster.warp);

            return theMonster;

        }

        public void SpawnImport(StardewDruid.Monster.Boss theMonster, bool warpIn = true)
        {

            monsterSpawns.Add(theMonster);

            spawnLocation.characters.Add(theMonster);

            theMonster.currentLocation = spawnLocation;

            theMonster.update(Game1.currentGameTime, spawnLocation);

            spawnTotal++;

            if (warpIn)
            {

                Mod.instance.iconData.AnimateQuickWarp(spawnLocation, theMonster.Position - new Vector2(0, 32), true, theMonster.warp);

            }

        }

        public static StardewDruid.Monster.Boss CreateMonster(
            bosses spawnMob, 
            Vector2 spawnVector, 
            int combatModifier = -1, 
            Boss.difficulty difficulty = Boss.difficulty.basic, 
            Boss.temperment temperment = Boss.temperment.cautious
        )
        {

            if (combatModifier == -1)
            {

                combatModifier = Mod.instance.CombatDifficulty();

            }

            StardewDruid.Monster.Boss theMonster;

            switch (spawnMob)
            {

                default:
                case bosses.batwing:

                    switch (Mod.instance.randomIndex.Next(4))
                    {
                        default:

                            theMonster = new ShadowBat(spawnVector, combatModifier);

                            break;

                        case 1:

                            theMonster = new ShadowBat(spawnVector, combatModifier, CharacterHandle.characters.BrownBat.ToString());

                            break;

                        case 2:

                            theMonster = new ShadowBat(spawnVector, combatModifier, CharacterHandle.characters.RedBat.ToString());

                            break;
                    }

                    break;

                case bosses.dustfiend:

                    theMonster = new Dustfiend(spawnVector, combatModifier);

                    break;

                case bosses.firefiend:

                    theMonster = new Dustfiend(spawnVector, combatModifier, CharacterHandle.characters.Firefiend.ToString());

                    break;

                case bosses.jellyfiend:

                    switch (Mod.instance.randomIndex.Next(4))
                    {
                        default:

                            theMonster = new Jellyfiend(spawnVector, combatModifier);

                            break;

                        case 1:

                            theMonster = new Jellyfiend(spawnVector, combatModifier, CharacterHandle.characters.BlueJellyfiend.ToString());

                            break;


                        case 2:

                            theMonster = new Jellyfiend(spawnVector, combatModifier, CharacterHandle.characters.PinkJellyfiend.ToString());

                            break;

                    }

                    break;

                case bosses.darkbrute:

                    switch (Mod.instance.randomIndex.Next(2))
                    {
                        default:

                            theMonster = new DarkBrute(spawnVector, combatModifier);

                            break;

                        case 1:

                            theMonster = new DarkBrute(spawnVector, combatModifier,"DarkBruteTwo");

                            break;

                    }


                    break;

                case bosses.darkshooter:

                    theMonster = new DarkShooter(spawnVector, combatModifier);

                    break;

                case bosses.spectre:

                    switch (Mod.instance.randomIndex.Next(2))
                    {
                        default:

                            theMonster = new Spectre(spawnVector, combatModifier);

                            break;

                        case 1:

                            theMonster = new Spectre(spawnVector, combatModifier, CharacterHandle.characters.WhiteSpectre.ToString());

                            break;

                    }

                    break;

                case bosses.phantom:

                    theMonster = new DarkPhantom(spawnVector, combatModifier);

                    break;

                case bosses.shadowbear:

                    switch (Mod.instance.randomIndex.Next(2))
                    {
                        default:

                            theMonster = new ShadowBear(spawnVector, combatModifier);

                            break;

                        case 1:

                            theMonster = new ShadowBear(spawnVector, combatModifier, CharacterHandle.characters.BlackBear.ToString());

                            break;

                    }

                    break;

                case bosses.shadowwolf:

                    switch (Mod.instance.randomIndex.Next(2))
                    {
                        default:

                            theMonster = new ShadowWolf(spawnVector, combatModifier);

                            break;

                        case 1:

                            theMonster = new ShadowWolf(spawnVector, combatModifier, CharacterHandle.characters.BlackWolf.ToString());

                            break;

                    }

                    break;

            }

            theMonster.SetMode((int)difficulty);

            switch (temperment)
            {

                case Monster.Boss.temperment.random:

                    theMonster.RandomTemperment();

                    break;

                default:

                    theMonster.tempermentActive = temperment;

                    break;

            }

            return theMonster;

        }

        public static Texture2D MonsterTexture(string characterName)
        {

            Texture2D characterTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", characterName + ".png"));

            return characterTexture;

        }

    }

}
