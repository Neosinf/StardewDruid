using Force.DeepCloner;
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewValley;
using System;
using System.Collections.Generic;
using static StardewDruid.Event.SpellHandle;
using static StardewValley.Minigames.TargetGame;

namespace StardewDruid.Event.Challenge
{
    public class Summon : EventHandle
    {

        public int summonStrength;

        public int summonRounds;

        public int roundCountdown;

        public int roundTimer;

        public int roundIndex;

        public int summonLimit;

        public Summon(Vector2 target)
            : base(target)
        {
            targetVector = target;
            
        }

        public override void EventTrigger()
        {

            AddActor((targetVector - new Vector2(0, 1)) * 64 - new Vector2(16,0));

            targetLocation.objects.Remove(targetVector);

            SummonConfig();

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 180;

            LightHandle brazier = new(targetLocation, targetVector);

            braziers.Add(brazier);

            Vector2 boltVector = new(targetVector.X, targetVector.Y - 1);

            ModUtility.AnimateBolt(targetLocation, boltVector * 64 + new Vector2(32));

            Mod.instance.RegisterEvent(this, "active");

            Mod.instance.CastMessage("Summoning initiated. Rounds " + summonRounds.ToString() + " Difficulty " + summonStrength);

        }

        public void SummonConfig()
        {

            int strength = 1;

            int power = Mod.instance.PowerLevel;

            List<Vector2> removeList = new();

            for (int i = 1; i < 4; i++)
            {

                List<Vector2> tileVectors = ModUtility.GetTilesWithinRadius(targetLocation, targetVector, i);

                foreach (Vector2 tileVector in tileVectors)
                {

                    if (targetLocation.objects.ContainsKey(tileVector))
                    {

                        StardewValley.Object targetObject = targetLocation.objects[tileVector];

                        if (targetObject.itemId.Contains("93")) // crafted candle torch
                        {

                            removeList.Add(tileVector);

                        }

                    }

                }

            }

            foreach (Vector2 tileVector in removeList)
            {

                targetLocation.objects.Remove(tileVector);

                strength++;

            }

            strength = Math.Min(strength, power);

            summonStrength = strength;

            summonRounds = strength + power;

            monsterHandle = new(targetVector, Mod.instance.rite.castLocation);

            monsterHandle.spawnCombat = (int)(Mod.instance.CombatDifficulty() * (1 + (0.2 * strength)));

            roundIndex = 0;

            SummonSetup();
            
        }

        public virtual void SummonSetup()
        {

            switch (roundIndex)
            {

                default: // start, bats, 0

                    monsterHandle.spawnCounter = 0;

                    monsterHandle.spawnIndex = new(){ 0, };

                    monsterHandle.spawnFrequency = 2;

                    monsterHandle.spawnAmplitude = 1;

                    monsterHandle.championInterval = 10 - summonStrength;

                    monsterHandle.championAmount = 0;

                    monsterHandle.championCounter = 0;

                    monsterHandle.championLimit = 2;

                    summonLimit = 3;

                    SetTrack("tribal");

                    break;

                case 1: // shadows

                    monsterHandle.spawnCounter = 0;

                    monsterHandle.spawnIndex = new() { 1, };

                    monsterHandle.spawnFrequency = 3;

                    monsterHandle.spawnAmplitude = 2;

                    monsterHandle.championInterval = 10 - summonStrength;

                    monsterHandle.championAmount = 0;

                    monsterHandle.championCounter = 0;

                    monsterHandle.championLimit = 2;

                    summonLimit = 3;

                    break;

                case 2: // slimes

                    monsterHandle.spawnCounter = 0;

                    monsterHandle.spawnIndex = new() { 2, };

                    monsterHandle.spawnFrequency = 3;

                    monsterHandle.spawnAmplitude = 2;

                    monsterHandle.championInterval = 10 - summonStrength;

                    monsterHandle.championAmount = 0;

                    monsterHandle.championCounter = 0;

                    monsterHandle.championLimit = 2;

                    summonLimit = 3;

                    SetTrack("tribal");

                    break;

                case 3: // pirates

                    monsterHandle.spawnCounter = 0;

                    monsterHandle.spawnIndex = new() { 3, 4, };

                    monsterHandle.spawnFrequency = 2;

                    monsterHandle.spawnAmplitude = 2;

                    monsterHandle.championInterval = 0;

                    monsterHandle.championAmount = 0;

                    monsterHandle.championCounter = 0;

                    monsterHandle.championLimit = 0;

                    summonLimit = 3;

                    break;

                case 4: // dusts

                    monsterHandle.spawnCounter = 0;

                    monsterHandle.spawnIndex = new() { 5, };

                    monsterHandle.spawnFrequency = 1;

                    monsterHandle.spawnAmplitude = 2;

                    monsterHandle.championInterval = 0;

                    monsterHandle.championAmount = 0;

                    monsterHandle.championCounter = 0;

                    monsterHandle.championLimit = 0;

                    summonLimit = 4;

                    SetTrack("tribal");

                    break;

                case 5: // monkeys, gargoyles

                    monsterHandle.spawnCounter = 0;

                    monsterHandle.spawnIndex = new() { 6, 7, };

                    monsterHandle.spawnFrequency = 3;

                    monsterHandle.spawnAmplitude = 1;

                    monsterHandle.championInterval = 10 - summonStrength;

                    monsterHandle.championAmount = 0;

                    monsterHandle.championCounter = 0;

                    monsterHandle.championLimit = 3;

                    summonLimit = 3;

                    break;

                case 6: // dinos

                    monsterHandle.spawnCounter = 0;

                    monsterHandle.spawnIndex = new() { 8, };

                    monsterHandle.spawnFrequency = 5;

                    monsterHandle.spawnAmplitude = 2;

                    monsterHandle.championInterval = 10;

                    monsterHandle.championAmount = 0;

                    monsterHandle.championCounter = 0;

                    monsterHandle.championLimit = 1;

                    summonLimit = 2;

                    SetTrack("tribal");

                    break;

                case 7: // foxes cats

                    monsterHandle.spawnCounter = 0;

                    monsterHandle.spawnIndex = new() { 9, };

                    monsterHandle.spawnFrequency = 10;

                    monsterHandle.spawnAmplitude = 3;

                    monsterHandle.championInterval = 0;

                    monsterHandle.championCounter = 0;

                    monsterHandle.championLimit = 0;

                    summonLimit = 2;

                    break;

                case 8: // rogues, goblins

                    monsterHandle.spawnCounter = 0;

                    monsterHandle.spawnIndex = new() { 10, };

                    monsterHandle.spawnFrequency = 10;

                    monsterHandle.spawnAmplitude = 3;

                    monsterHandle.championInterval = 0;

                    monsterHandle.championCounter = 0;

                    monsterHandle.championLimit = 0;

                    summonLimit = 2;

                    SetTrack("tribal");

                    break;

                case 9: // dragons

                    monsterHandle.spawnCounter = 0;

                    monsterHandle.spawnIndex = new() { 11, };

                    monsterHandle.spawnFrequency = 30;

                    monsterHandle.spawnAmplitude = 4;

                    monsterHandle.championInterval = 0;

                    monsterHandle.championCounter = 0;

                    monsterHandle.championLimit = 0;

                    summonLimit = 1;

                    break;

            }

            roundCountdown = 5;

            roundTimer = 15 + summonStrength * 2;

            expireTime += 25;

        }

        public override bool EventExpire()
        {

            if (eventLinger == -1)
            {

                DealRewards();

                RemoveMonsters();

                eventLinger = 2;

                return true;

            }

            return base.EventExpire();

        }

        public void DealRewards()
        {

            if (!Mod.instance.rite.castTask.ContainsKey("masterPortal"))
            {

                Mod.instance.UpdateTask("lessonPortal", roundIndex);

            }
            else if (!Mod.instance.rite.castTask.ContainsKey("lessonPortal"))
            {

                Mod.instance.TaskSet("lessonPortal", roundIndex);

            }
            else if (Mod.instance.rite.castTask["lessonPortal"] < roundIndex)
            {

                Mod.instance.TaskSet("lessonPortal", roundIndex);

            }


            int tileX = (int)targetVector.X;

            int tileY = (int)targetVector.Y;

            switch (roundIndex)
            {

                default:

                    for (int i = 0; i < 2; i++)
                    {
                        Game1.createObjectDebris("334", tileX, tileY);
                    }

                    CastVoice("sufficient", 2000);

                    break;

                case 2:
                case 3:

                    for (int i = 0; i < 2; i++)
                    {
                        Game1.createObjectDebris("335", tileX, tileY);
                    }

                    CastVoice("good", 2000);

                    break;

                case 4:
                case 5:

                    for (int i = 0; i < 2; i++)
                    {
                        Game1.createObjectDebris("336", tileX, tileY);
                    }

                    CastVoice("great", 2000);

                    break;

                case 6:

                    for (int i = 0; i < 2; i++)
                    {
                        Game1.createObjectDebris("337", tileX, tileY);
                    }

                    CastVoice("superb", 2000);

                    break;

                case 7:
                case 8:

                    Game1.createObjectDebris("446", tileX, tileY, itemQuality: 4);

                    CastVoice("brilliant", 2000);

                    break;

                case 9:
                case 10:

                    Game1.createObjectDebris("74", tileX, tileY);

                    CastVoice("legendary", 2000);

                    break;


            }

        }

        public override void EventAbort()
        {
            Mod.instance.CastMessage("The portal through the veil has collapsed", 3, true);

            if (!Mod.instance.CasterBusy())
            {

                DealRewards();

            }

        }

        public override void EventInterval()
        {

            // --------------------------------------

            activeCounter++;

            monsterHandle.SpawnCheck();

            if (eventLinger != -1)
            {

                return;

            }

            if (activeCounter % 30 == 0)
            {

                ResetBraziers();

            }

            // -------------------------------------

            if(roundCountdown >= 1)
            {
                if(roundCountdown == 5)
                {

                    CastVoice("round " + (roundIndex + 1).ToString(), 2000);

                }

                if (roundCountdown <= 3)
                {

                    CastVoice(roundCountdown.ToString(), 1000);

                }

                roundCountdown--;

                return;


            }

            // -------------------------------------

            if(roundTimer >= 0)
            {

                roundTimer--;

            }

            if(roundTimer == 10)
            {

                CastVoice("halfway", 2000);

            }

            if (roundTimer == 5)
            {

                if(monsterHandle.monsterSpawns.Count > summonLimit)
                {

                    Mod.instance.CastMessage("At least " + (monsterHandle.monsterSpawns.Count - summonLimit).ToString() + " more summons must be defeated!", 0, true);

                }

            }

            if(roundTimer == 0)
            {
                
                monsterHandle.ShutDown();

                if (monsterHandle.monstersLeft > summonLimit)
                {
                    CastVoice("enough", 2000);

                    Mod.instance.CastMessage(monsterHandle.monstersLeft.ToString() + " monsters remained, " + (roundIndex+1).ToString() + " rounds cleared", 0, true);

                    expireEarly = true;

                    return;

                }

                roundIndex++;

                if (roundIndex == summonRounds)
                {

                    Mod.instance.CastMessage("The summoning concludes, with " + (roundIndex).ToString() + " rounds cleared",0,true);

                    expireEarly = true;

                    return;

                }

                SummonSetup();

                return;

            }

            if(roundTimer > 6)
            {

                monsterHandle.SpawnInterval();

            }
            else if(monsterHandle.monsterSpawns.Count == 0)
            {

                roundTimer = 1;


            }

        }

    }

}
