﻿using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Objects;
using System.Collections.Generic;

namespace StardewDruid.Event.Challenge
{
    public class Infestation : ChallengeHandle
    {

        public BossSlime bossMonster;

        public Infestation(Vector2 target, Rite rite, Quest quest)
            : base(target, rite, quest)
        {

        }

        public override void EventTrigger()
        {

            challengeSpawn = new() { 0, };
            challengeFrequency = 1;
            challengeAmplitude = 1;
            challengeSeconds = 60;
            challengeWithin = new(72, 71);
            challengeRange = new(14, 13);
            challengeTorches = new()
            {
                new(75, 74),
                new(82, 74),
                new(75, 81),
                new(82, 81),
            };
            
            SetupSpawn();

            if (questData.name.Contains("Two"))
            {
                
                monsterHandle.spawnCombat *= 3;

                monsterHandle.spawnCombat /= 2;

            }

            Game1.addHUDMessage(new HUDMessage($"Defeat the slimes!", "2"));

            SetTrack("tribal");

            Mod.instance.RegisterEvent(this, "active");

        }

        public override void RemoveMonsters()
        {

            if (bossMonster != null)
            {

                riteData.castLocation.characters.Remove(bossMonster);

                bossMonster = null;

            }

            base.RemoveMonsters();

        }

        public override bool EventExpire()
        {

            Mod.instance.CompleteQuest(questData.name);

            if (!questData.name.Contains("Two"))
            {

                List<string> NPCIndex = VillagerData.VillagerIndex("forest");

                Game1.addHUDMessage(new HUDMessage($"You have gained favour with those who love the forest", ""));

                UpdateFriendship(NPCIndex);

                Mod.instance.dialogue["Effigy"].specialDialogue["journey"] = new() { "I sense a change", "I defeated the Pumpkin Slime. Now I'm covered in his gunk." };

                Throw throwHat = new(Game1.player,targetVector*64,99);

                throwHat.objectInstance = new Hat(48);

                throwHat.ThrowObject();

            }
            else
            {

                List<int> eggList = new()
                {
                    413,
                    437,
                    439,
                    680,
                    857,
                };

                Throw throwEgg = new(Game1.player, targetVector * 64, eggList[randomIndex.Next(eggList.Count)]);

                throwEgg.ThrowObject();

            }

            return false;

        }

        public override void EventInterval()
        {
            
            activeCounter++;

            if (eventLinger != -1)
            {

                return;

            }

            monsterHandle.SpawnInterval();

            if (activeCounter == 14)
            {

                Vector2 bossVector = monsterHandle.SpawnVector(12,76,72,5,4);

                if(bossVector == new Vector2(-1))
                {
                    bossVector = new(78, 74);

                }

                StardewValley.Monsters.Monster theMonster = MonsterData.CreateMonster(13, bossVector, riteData.combatModifier);

                bossMonster = theMonster as BossSlime;

                bossMonster.posturing = true;

                riteData.castLocation.characters.Add(bossMonster);

                bossMonster.update(Game1.currentGameTime, riteData.castLocation);

            }

            if (activeCounter <= 14)
            {
                return;
            }

            if (bossMonster.Health >= 1)
            {
                switch (activeCounter)
                {

                    case 15: bossMonster.showTextAboveHead("HOW BORING", 3000); break;

                    case 18: bossMonster.showTextAboveHead("the monarchs must be asleep.", 3000); break;

                    case 21: bossMonster.showTextAboveHead("if they send only a farmer", 3000); break;

                    case 24: bossMonster.showTextAboveHead("to face the onslaught...", 3000); break;

                    case 27: bossMonster.showTextAboveHead("OF THE MIGHTY SLIME", 3000); break;

                    case 30: bossMonster.showTextAboveHead("you will be consumed", 3000); break;

                    case 33: bossMonster.showTextAboveHead("along with the whole valley", 3000); break;

                    case 36: bossMonster.showTextAboveHead("ALL WILL BE JELLY", 3000); break;

                    case 37:

                        bossMonster.posturing = false;

                        bossMonster.focusedOnFarmers = true;

                        break;

                    case 55:

                        bossMonster.showTextAboveHead("bloop?");

                        bossMonster.Halt();

                        bossMonster.stunTime = 5000;

                        break;

                    case 56:

                        bossMonster.showTextAboveHead("that's a lot of star power");

                        bossMonster.Halt();

                        break;

                    case 58:

                        bossMonster.showTextAboveHead("!!!!");

                        Vector2 meteorVector = bossMonster.getTileLocation();

                        ModUtility.AnimateRadiusDecoration(targetLocation, meteorVector + new Vector2(-2, 1), "Stars", 1f, 1f);

                        ModUtility.AnimateMeteor(riteData.castLocation, meteorVector + new Vector2(-2, 1), true);

                        ModUtility.AnimateRadiusDecoration(targetLocation, meteorVector + new Vector2(1, -2), "Stars", 1f, 1f);

                        ModUtility.AnimateMeteor(riteData.castLocation, meteorVector + new Vector2(1, -2), true);

                        ModUtility.AnimateRadiusDecoration(targetLocation, meteorVector + new Vector2(2, 1), "Stars", 1f, 1f);

                        ModUtility.AnimateMeteor(riteData.castLocation, meteorVector + new Vector2(2, 1), false);

                        ModUtility.AnimateRadiusDecoration(targetLocation, meteorVector + new Vector2(1, 2), "Stars", 1f, 1f);

                        ModUtility.AnimateMeteor(riteData.castLocation, meteorVector + new Vector2(1, 2), false);

                        DelayedAction.functionAfterDelay(MeteorImpact, 600);

                        break;

                    default: break;

                }

            }

        }

        public void MeteorImpact()
        {

            List<Vector2> impactVectors;

            Vector2 impactCenter = bossMonster.getTileLocation();

            for (int i = 0; i < 5; i++)
            {

                impactVectors = ModUtility.GetTilesWithinRadius(riteData.castLocation, impactCenter, i);

                foreach (Vector2 impactVector in impactVectors)
                {

                    ModUtility.ImpactVector(targetLocation, impactVector);

                }

            }

            bossMonster.takeDamage(bossMonster.MaxHealth + 5, 0, 0, false, 999, targetPlayer);

        }

    }
}
