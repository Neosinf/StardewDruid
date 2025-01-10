
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Event.Sword
{
    internal class SwordFates : EventHandle
    {

        public Vector2 statueVector;

        public SwordFates()
        {

        }
        public override void EventRemove()
        {

            base.EventRemove();

            if (eventAbort)
            {

                if (Game1.player.currentLocation.Name == LocationHandle.druid_tunnel_name)
                {

                    DialogueCue(901);

                    Vector2 warpBack = ModUtility.PositionToTile(Mod.instance.questHandle.quests[eventId].origin);

                    Mod.instance.WarpAllFarmers(
                        Mod.instance.questHandle.quests[eventId].triggerLocation,
                        (int)warpBack.X,
                        (int)warpBack.Y, 
                        2);


                }

            }


        }

        public override void EventActivate()
        {

            base.EventActivate();

            statueVector = new Vector2(30, 6) * 64;

            locales = new() { location.Name, LocationHandle.druid_tunnel_name, LocationHandle.druid_court_name, };

            monsterHandle = new(origin, location);

            monsterHandle.spawnSchedule = new();

            for (int i = 1; i <= 90; i += 2)
            {

                monsterHandle.spawnSchedule.Add(i, new() { new(MonsterHandle.bosses.spectre, 4, Mod.instance.randomIndex.Next(2)) });

            }

            monsterHandle.spawnWithin = ModUtility.PositionToTile(Game1.player.Position);

            monsterHandle.spawnRange = new(15, 15);

            monsterHandle.spawnWater = true;

            monsterHandle.spawnVoid = true;

            activeLimit = 155;

        }

        public override void EventInterval()
        {

            activeCounter++;

            DialogueCue(activeCounter);

            if (activeCounter <= 10)
            {

                EventPartOne();

            }
            else
            if(activeCounter <= 90)
            {

                EventPartTwo();

            }
            else
            if(activeCounter <= 120)
            {

                EventPartThree();

            }
            else
            if (activeCounter <= 155)
            {

                EventPartFour();

            }
            else
            {

                eventComplete = true;

            }

        }

        public void EventPartOne()
        {

            switch (activeCounter)
            {
                
                case 1:

                    Mod.instance.WarpAllFarmers(LocationHandle.druid_tunnel_name, 22, 99, 3);

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Jester, Character.Character.mode.track);

                    Game1.stopMusicTrack(StardewValley.GameData.MusicContext.Default);

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Jester];

                    voices[0] = companions[0];
                    
                    break;

                case 3:

                    location = Game1.player.currentLocation;

                    monsterHandle.spawnLocation = location;

                    location.playSound("ghost");

                    break;

                case 6:

                    location.playSound("ghost");

                    break;

                case 9:

                    location.playSound("ghost");

                    break;

                case 10:

                    DialogueCue(900);

                    EventDisplay run = EventBar(StringData.Strings(StringData.stringkeys.reachEnd), 1);

                    run.colour = Color.Orange;

                    SetTrack("spirits_eve");

                    break;

            }

        }

        public void EventPartTwo()
        {
            
            if (activeCounter % 10 == 0)
            {

                foreach(KeyValuePair< CharacterHandle.characters,TrackHandle> tracker in Mod.instance.trackers)
                {

                    tracker.Value.WarpToPlayer();

                }

                DialogueCue(900);

            }

            monsterHandle.spawnWithin = ModUtility.PositionToTile(Game1.player.Position);

            monsterHandle.SpawnCheck();

            monsterHandle.SpawnInterval();

            if(Vector2.Distance(Game1.player.Position,statueVector) <= 448)
            {

                activeCounter = 90;

            }

            if(activeCounter == 89)
            {

                eventAbort = true;

            }

        }

        public void EventPartThree()
        {

            if (Game1.player.currentLocation.Name == LocationHandle.druid_court_name)
            {

                activeCounter = 120;

                return;

            }

            switch (activeCounter)
            {

                case 91:

                    companions[0].Position = new Vector2(27, 6) * 64;

                    companions[0].Halt();

                    companions[0].idleTimer = 600;

                    companions[0].netDirection.Set(1);

                    break;

                case 92:

                    if (!Game1.player.mailReceived.Contains("gotGoldenScythe"))
                    {
                        
                        ThrowHandle swordThrow = new(Game1.player, companions[0].Position, SpawnData.swords.scythe);

                        swordThrow.register();

                        Game1.playSound("parry");

                        Game1.player.mailReceived.Add("gotGoldenScythe");

                        location.setMapTile(29, 4, 245, "Front", "mine");
                        location.setMapTile(30, 4, 246, "Front", "mine");
                        location.setMapTile(29, 5, 261, "Front", "mine");
                        location.setMapTile(30, 5, 262, "Front", "mine");
                        location.setMapTile(29, 6, 277, "Buildings", "mine");
                        location.setMapTile(30, 56, 278, "Buildings", "mine");
                    }

                    break;

                case 94:

                    Event.Access.AccessHandle CourtAccess = new();

                    CourtAccess.AccessSetup(LocationHandle.druid_tunnel_name, LocationHandle.druid_court_name, new(29, 8), new(45, 20));

                    CourtAccess.location = location;

                    CourtAccess.AccessStart(false);

                    break;

                case 97:

                    Mod.instance.WarpAllFarmers(LocationHandle.druid_court_name, 41, 20, 1);

                    location = Mod.instance.locations[LocationHandle.druid_court_name];

                    CharacterMover.Warp(Mod.instance.locations[LocationHandle.druid_court_name], companions[0], new Vector2(40, 18) * 64);

                    Event.Access.AccessHandle TunnelAccess = new();

                    TunnelAccess.AccessSetup( LocationHandle.druid_court_name, LocationHandle.druid_tunnel_name, new Vector2(46, 21), new Vector2(29, 8) );

                    TunnelAccess.location = location;

                    TunnelAccess.AccessStart(false);

                    break;

                case 100:

                    activeCounter = 120;

                    break;


            }

        }

        public void EventPartFour()
        {

            switch (activeCounter)
            {

                case 121:

                    StopTrack();

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[0].eventName = eventId;

                    break;

                case 124:

                    companions[0].LookAtTarget(Game1.player.Position);

                    DialogueLoad(0, 1);

                    break;

                case 128:

                    DialogueNext(companions[0]);

                    break;

                case 131:

                    companions[0].TargetEvent(0,new Vector2(34,13)*64,true);

                    break;

                case 134:

                    companions[0].LookAtTarget(new Vector2(1280,0));

                    DialogueLoad(0, 2);

                    break;

                case 135:

                    companions[0].LookAtTarget(new Vector2(6400, 1280));

                    break;

                case 136:

                    companions[0].LookAtTarget(Game1.player.Position);

                    break;

                case 137:

                    DialogueNext(companions[0]);

                    break;

                case 141:

                    companions[0].TargetEvent(0, new Vector2(29, 20) * 64, true);

                    break;

                case 144:

                    companions[0].LookAtTarget(Game1.player.Position);

                    DialogueLoad(0, 3);

                    break;

                case 147:
                    companions[0].LookAtTarget(Game1.player.Position);

                    DialogueNext(companions[0]);

                    break;

                case 151:

                    eventComplete = true;

                    break;


            }


        }

        public override float SpecialProgress(int displayId)
        {

            if (activeCounter > 80)
            {

                return -1;
            }

            return (float)(activeCounter - 10) / 80f;

        }

    }

}