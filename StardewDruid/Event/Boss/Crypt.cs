﻿using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Event.Challenge;
using StardewDruid.Location;
using StardewDruid.Map;
using StardewDruid.Monster.Boss;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.IO;
using xTile;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Event.Boss
{
    public class Crypt : BossHandle
    {

        public List<StardewDruid.Monster.Boss.Boss> bossMonsters;

        public Crypt(Vector2 target,  Quest quest)
          : base(target, quest)
        {

            targetVector = target;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 90.0;

            bossMonsters = new();

            cues = DialogueData.DialogueScene(questData.name);

            AddActor(new (target.X * 64f, target.Y * 64f - 32f));

        }

        public override void EventRemove()
        {

            base.EventRemove();

        }

        public override void RemoveMonsters()
        {
            if(bossMonsters.Count > 0)
            {

                for (int i = bossMonsters.Count - 1; i >= 0; i--)
                {

                    StardewDruid.Monster.Boss.Boss boss = bossMonsters[i];

                    boss.currentLocation.characters.Remove(boss);

                }

                bossMonsters.Clear();

            }

            base.RemoveMonsters();

        }

        public override void EventAbort()
        {

            base.EventAbort();

            if(Game1.player.currentLocation.Name == "18465_Crypt")
            {

                WarpBackToTown();

            }

        }


        public override bool EventExpire()
        {

            if (eventLinger == -1)
            {

                RemoveMonsters();

                eventLinger = 3;

                return true;

            }

            if (eventLinger == 2)
            {

                if (expireEarly)
                {

                    EventComplete();

                    QuestData.NextProgress();

                }
                else
                {

                    WarpBackToTown();

                    Mod.instance.CastMessage("Try again tomorrow");

                }

            }

            return base.EventExpire();

        }

        public void WarpBackToTown()
        {

            int warpX = (int)questData.triggerVector.X;

            int warpY = (int)questData.triggerVector.Y;

            Game1.warpFarmer("town", warpX, warpY, 2);

            Game1.xLocationAfterWarp = warpX;

            Game1.yLocationAfterWarp = warpY;

            EventQuery("LocationReturn");

        }

        public override void EventInterval()
        {
            
            activeCounter++;

            if (activeCounter == 1)
            {

                Location.LocationData.CryptEdit();

                targetLocation = Game1.getLocationFromName("18465_Crypt");

                targetVector = new Vector2(20, 15);

                Game1.warpFarmer("18465_Crypt", 20, 10, 2);

                Game1.xLocationAfterWarp = 20;

                Game1.yLocationAfterWarp = 10;

                RemoveActors();

                actors.Clear();

                AddActor(new(targetVector.X * 64, targetVector.Y * 64 - 32f));

                SetTrack("tribal");

                return;

            }

            if (activeCounter == 2)
            {

                EventQuery("LocationEdit");

                EventQuery("LocationPortal");

                int difficulty = 1;

                if (questData.name.Contains("Two"))
                {

                    difficulty = 2;
                
                }
                else
                {
                    Monster.Boss.Shadowtin bossShadowtin = MonsterData.CreateMonster(18, targetVector + new Vector2(0, 5f)) as Monster.Boss.Shadowtin;

                    targetLocation.characters.Add(bossShadowtin);

                    bossShadowtin.currentLocation = Mod.instance.rite.castLocation;

                    bossShadowtin.update(Game1.currentGameTime, Mod.instance.rite.castLocation);

                    bossMonsters.Add(bossShadowtin);

                }

                int monsterIndex;

                for(int j = 0; j < difficulty; j++)
                {

                    monsterIndex = 19;

                    if (j == 1) { monsterIndex = 23; }

                    Scavenger bossScavenger = MonsterData.CreateMonster(monsterIndex, targetVector + new Vector2(4 - (2* difficulty), 6f - (3 * difficulty))) as Scavenger;

                    if (questData.name.Contains("Two"))
                    {
                        bossScavenger.HardMode();
                    }

                    targetLocation.characters.Add(bossScavenger);

                    bossScavenger.currentLocation = Mod.instance.rite.castLocation;

                    bossScavenger.update(Game1.currentGameTime, Mod.instance.rite.castLocation);

                    bossMonsters.Add(bossScavenger);

                    monsterIndex = 20;

                    if (j == 1) { monsterIndex = 24; }

                    StardewDruid.Monster.Boss.Shadowtin bossRogue = MonsterData.CreateMonster(monsterIndex, targetVector + new Vector2(-4 + (2 * difficulty), 6f - (3 * difficulty))) as StardewDruid.Monster.Boss.Shadowtin;

                    if (questData.name.Contains("Two"))
                    {
                        bossRogue.HardMode();
                    }

                    targetLocation.characters.Add(bossRogue);

                    bossRogue.currentLocation = Mod.instance.rite.castLocation;

                    bossRogue.update(Game1.currentGameTime, Mod.instance.rite.castLocation);

                    bossMonsters.Add(bossRogue);

                }

                braziers.Add(new(targetLocation, new(13, 13)));

                braziers.Add(new(targetLocation, new(13, 26)));

                braziers.Add(new(targetLocation, new(26, 13)));

                braziers.Add(new(targetLocation, new(26, 26)));

                return;

            }

            if (activeCounter < 5) {

                return;

            }

            for(int i = bossMonsters.Count - 1; i >= 0; i--)
            {

                StardewDruid.Monster.Boss.Boss boss = bossMonsters[i];

                if (!ModUtility.MonsterVitals(boss, targetLocation))
                {
                    bossMonsters.RemoveAt(i);
                }

            }

            if(bossMonsters.Count == 0)
            {
                expireEarly = true;

                return;

            }

            DialogueCue(DialogueData.DialogueNarrator(questData.name), new() { [0] = bossMonsters[0], }, activeCounter);

            if (activeCounter % 60 == 0)
            {

                ResetBraziers();

            }

        }

    }

}