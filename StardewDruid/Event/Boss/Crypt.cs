using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Event.Challenge;
using StardewDruid.Location;
using StardewDruid.Map;
using StardewDruid.Monster;
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

        public List<StardewDruid.Monster.Boss> bossMonsters;

        public Crypt(Vector2 target, Rite rite, Quest quest)
          : base(target, rite, quest)
        {

            targetVector = target;

            voicePosition = new(targetVector.X * 64f, targetVector.Y * 64f - 32f);

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 120.0;

            bossMonsters = new();

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

                    StardewDruid.Monster.Boss boss = bossMonsters[i];

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

                    Mod.instance.CompleteQuest(questData.name);

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


        }

        public override void EventInterval()
        {
            
            activeCounter++;

            if (activeCounter == 1)
            {

                AddCrypt();

                targetVector = new Vector2(20, 15);

                Game1.warpFarmer("18465_Crypt", 20, 10, 2);

                Game1.xLocationAfterWarp = 20;

                Game1.yLocationAfterWarp = 9;

                voicePosition = new(targetVector.X * 64, targetVector.Y * 64 - 32f);

                SetTrack("tribal");

                return;

            }

            if (activeCounter == 2)
            {
                int difficulty = 1;

                if (questData.name.Contains("Two"))
                {

                    difficulty = 2;
                
                }
                else
                {
                    StardewDruid.Monster.Shadowtin bossShadowtin = MonsterData.CreateMonster(18, targetVector + new Vector2(0, 5f), riteData.combatModifier) as StardewDruid.Monster.Shadowtin;

                    targetLocation.characters.Add(bossShadowtin);

                    bossShadowtin.currentLocation = riteData.castLocation;

                    bossShadowtin.update(Game1.currentGameTime, riteData.castLocation);

                    bossMonsters.Add(bossShadowtin);

                }

                for(int j = 0; j < difficulty; j++)
                {

                    Scavenger bossScavenger = MonsterData.CreateMonster(19, targetVector + new Vector2(4 - (2* difficulty), 6f - (3 * difficulty)), riteData.combatModifier) as Scavenger;

                    if (questData.name.Contains("Two"))
                    {
                        bossScavenger.HardMode();
                    }

                    targetLocation.characters.Add(bossScavenger);

                    bossScavenger.currentLocation = riteData.castLocation;

                    bossScavenger.update(Game1.currentGameTime, riteData.castLocation);

                    bossMonsters.Add(bossScavenger);


                    Rogue bossRogue = MonsterData.CreateMonster(20, targetVector + new Vector2(-4 + (2 * difficulty), 6f - (3 * difficulty)), riteData.combatModifier) as Rogue;

                    if (questData.name.Contains("Two"))
                    {
                        bossRogue.HardMode();
                    }

                    targetLocation.characters.Add(bossRogue);

                    bossRogue.currentLocation = riteData.castLocation;

                    bossRogue.update(Game1.currentGameTime, riteData.castLocation);

                    bossMonsters.Add(bossRogue);

                }


                StaticHandle staticHandle;

                if (Mod.instance.eventRegister.ContainsKey("static"))
                {

                    staticHandle = Mod.instance.eventRegister["static"] as StaticHandle;

                }
                else
                {

                    staticHandle = new();

                    staticHandle.EventTrigger();

                }

                staticHandle.AddBrazier(targetLocation, new(13, 13));

                staticHandle.AddBrazier(targetLocation, new(13, 26));

                staticHandle.AddBrazier(targetLocation, new(26, 13));

                staticHandle.AddBrazier(targetLocation, new(26, 26));

                return;

            }

            if (activeCounter > 4)
            {

                for(int i = bossMonsters.Count - 1; i >= 0; i--)
                {

                    StardewDruid.Monster.Boss boss = bossMonsters[i];

                    if (!ModUtility.MonsterVitals(boss, targetLocation))
                    {
                        bossMonsters.RemoveAt(i);
                    }

                }

                if(bossMonsters.Count == 0)
                {
                    expireEarly = true;

                }

            }

        }

        public void AddCrypt()
        {

            GameLocation crypt = Game1.getLocationFromName("18465_Crypt");

            if (crypt != null)
            {

                //Game1.locations.Remove(crypt);

                //Game1.removeLocationFromLocationLookup(crypt);

                targetLocation = crypt;

                return;

            }

            targetLocation = new Location.Crypt("18465_Crypt");

            Game1.locations.Add(targetLocation);

            Mod.instance.locationList.Add("18465_Crypt");

        }

    }

}