﻿using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Event
{
    public class Aquifer : ChallengeHandle
    {

        public Queue<Vector2> trashQueue;

        public int trashCollected;

        public BossBat bossMonster;

        public Aquifer(Mod Mod, Vector2 target, Rite rite, Quest quest)
            : base(Mod, target, rite, quest)
        {

        }

        public override void EventTrigger()
        {

            challengeSpawn = new() { 99, };
            challengeFrequency = 1;
            challengeAmplitude = 1;
            challengeSeconds = 60;
            challengeWithin = new(17, 10);
            challengeRange = new(9, 9);
            challengeTorches = new() { new(20, 13), };

            if (questData.name.Contains("Two"))
            {
                challengeFrequency = 2;
                challengeAmplitude = 3;
            }

            SetupSpawn();

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            trashQueue = new();

            trashCollected = 0;

            for (int i = 0; i < 3; i++)
            {

                List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(targetLocation, targetVector, 3 + i);

                foreach (Vector2 castVector in castSelection)
                {

                    Tile backTile = backLayer.PickTile(new Location((int)castVector.X * 64, (int)castVector.Y * 64), Game1.viewport.Size);

                    if (backTile != null)
                    {

                        if (backTile.TileIndexProperties.TryGetValue("Water", out _))
                        {

                            if (randomIndex.Next(2) == 0) { continue; };

                            trashQueue.Enqueue(castVector);

                        }

                    }

                }

            }

            Game1.addHUDMessage(new HUDMessage($"Stand your ground!", "2"));

            Game1.changeMusicTrack("tribal", false, Game1.MusicContext.Default);

            mod.RegisterChallenge(this, "active");

        }

        public override bool EventActive()
        {

            if (targetPlayer.currentLocation == targetLocation)
            {

                double nowTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;

                if (expireTime >= nowTime)
                {
                    int diffTime = (int)Math.Round(expireTime - nowTime);

                    if (diffTime % 10 == 0 && diffTime != 0)
                    {

                        Game1.addHUDMessage(new HUDMessage($"{diffTime} minutes left until cleanup complete", "2"));

                    }
                    return true;

                }
                else
                {

                    EventReward();
                
                }
            
            }
            else
            {

                EventAbort();

            }

            return false;

        }

        public override void EventRemove()
        {

            if(bossMonster != null)
            {

                riteData.castLocation.characters.Remove(bossMonster);

                bossMonster = null;

            }

            base.EventRemove();

        }

        public override void EventReward()
        {

            if (trashCollected < 12)
            {

                Game1.addHUDMessage(new HUDMessage($"Try again to collect more trash", ""));

                mod.ReassignQuest(questData.name);

            }
            else
            {

                Game1.addHUDMessage(new HUDMessage($"Collected {trashCollected} pieces of trash!", ""));

                List<string> NPCIndex = Map.VillagerData.VillagerIndex("mountain");

                Game1.addHUDMessage(new HUDMessage($"You have gained favour with the mountain residents and their friends", ""));

                mod.CompleteQuest(questData.name);

                if (!questData.name.Contains("Two"))
                {
                    
                    UpdateFriendship(NPCIndex);

                    mod.UpdateEffigy(questData.name);

                    mod.LevelBlessing("earth");

                }

            }

        }

        public override void EventInterval()
        {

            activeCounter++;

            monsterHandle.SpawnInterval();

            if (randomIndex.Next(2) == 0)
            {

                ThrowTrash();

            }

            List<Vector2> rockFalls = new();

            for (int i = 0; i < randomIndex.Next(2, 4); i++)
            {

                Vector2 randomVector = challengeWithin + new Vector2(randomIndex.Next((int)challengeRange.X), randomIndex.Next((int)challengeRange.Y));

                if (rockFalls.Contains(randomVector)) { continue; }

                Rockfall rockFall = new(mod, randomVector, riteData);

                rockFall.challengeCast = true;

                rockFall.CastEarth();

                rockFalls.Add(randomVector);

            }

            if (activeCounter == 20)
            {
                StardewValley.Monsters.Monster theMonster = MonsterData.CreateMonster(11, new(30, 13), riteData.combatModifier);

                bossMonster = theMonster as BossBat;

                riteData.castLocation.characters.Add(bossMonster);

                bossMonster.update(Game1.currentGameTime, riteData.castLocation);

            }

            if(activeCounter <= 20)
            {
                return;
            }

            if (bossMonster.Health >= 1)
            {
                switch (activeCounter)
                {

                    case 22: bossMonster.showTextAboveHead("...farmer..."); break;

                    case 24: bossMonster.showTextAboveHead("...you tresspass..."); break;

                    case 26: bossMonster.showTextAboveHead("cheeep cheep"); break;

                    case 28: bossMonster.showTextAboveHead("...your kind..."); break;

                    case 30: bossMonster.showTextAboveHead("...defile waters..."); break;

                    case 32: bossMonster.showTextAboveHead("cheeep cheep"); break;

                    case 34: bossMonster.showTextAboveHead("She Who Screeches Over Seas"); break;

                    case 36: bossMonster.showTextAboveHead("...is angry..."); break;

                    case 38: bossMonster.showTextAboveHead("CHEEEP"); bossMonster.posturing = false; bossMonster.focusedOnFarmers = true; break;

                    case 56:

                        bossMonster.showTextAboveHead("...rocks hurt...");

                        break;

                    case 57:

                        Rockfall rockFall = new(mod, bossMonster.getTileLocation(), riteData);

                        rockFall.challengeCast = true;

                        rockFall.CastEarth();

                        break;

                    case 58:

                        bossMonster.showTextAboveHead("CHEEE--- aack");

                        break;

                    case 59:

                        bossMonster.takeDamage(999, 0, 0, false, 999, targetPlayer);

                        break;

                    default: break;

                }

            }


        }

        public void ThrowTrash()
        {

            if (trashQueue.Count == 0)
            {
                return;
            }

            Vector2 trashVector = trashQueue.Dequeue();

            Dictionary<int, int> artifactIndexes = new()
            {
                [0] = 105,
                [1] = 106,
                [2] = 110,
                [3] = 111,
                [4] = 112,
                [5] = 115,
                [6] = 117,
            };

            Dictionary<int, int> objectIndexes = new()
            {
                [0] = artifactIndexes[randomIndex.Next(7)],
                [1] = 167,
                [2] = 168,
                [3] = 169,
                [4] = 170,
                [5] = 171,
                [6] = 172,
            };

            int objectIndex = objectIndexes[randomIndex.Next(7)];

            Throw throwObject;

            if (trashCollected == 8)
            {

                objectIndex = 517;

                throwObject = new(objectIndex, 0);

                throwObject.objectInstance = new Ring(objectIndex);

            }
            else if (trashCollected == 16)
            {

                objectIndex = 519;

                throwObject = new(objectIndex, 0);

                throwObject.objectInstance = new Ring(objectIndex);

            }
            else
            {

                throwObject = new(objectIndex, 0);

            }

            throwObject.ThrowObject(targetPlayer, trashVector);

            targetPlayer.currentLocation.playSound("pullItemFromWater");

            bool targetDirection = targetPlayer.getTileLocation().X >= trashVector.X;

            Utility.addSprinklesToLocation(targetLocation, (int)trashVector.X, (int)trashVector.Y, 2, 2, 1000, 200, new Color(0.8f, 1f, 0.8f, 0.75f));

            ModUtility.AnimateSplash(targetLocation, trashVector, targetDirection);

            trashCollected++;

        }

    }

}
