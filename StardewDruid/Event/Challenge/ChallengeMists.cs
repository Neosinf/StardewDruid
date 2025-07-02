using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Companions;
using StardewValley.TerrainFeatures;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeMists : EventHandle
    {

        int progressCounter = 0;

        public Dictionary<int, Vector2> eventVectors = new()
        {
            
            // Shooter place
            [0] = new Vector2(26, 14),
            
            // Doja place
            [1] = new Vector2(28, 14),
            
            // spawn around
            [2] = new Vector2(21, 11),
            
            // Doja leave
            [3] = new Vector2(27, 4),

            // Shadowtin enter
            [4] = new Vector2(27, 4),

            // Shadowtin stop
            [5] = new Vector2(28, 13),

            // Shooter stop
            [6] = new Vector2(26, 13),

            // Shooter leave 1
            [7] = new Vector2(26, 10),

            // Shadowtin leave 1
            [8] = new Vector2(28, 10),

            // Shooter leave 2
            [9] = new Vector2(26, 4),

            // Shadowtin leave 2
            [10] = new Vector2(28, 4),

            // Shooter position
            [11] = new Vector2(27,16),

        };

        public ChallengeMists()
        {

            activeLimit = -1;

            mainEvent = true;

        }

        public override bool TriggerActive()
        {

            if(Game1.timeOfDay < 1900)
            {

                return false;

            }

            if (TriggerLocation())
            {

                EventActivate();

            }

            return false;

        }

        public override bool AttemptReset()
        {

            if (!eventActive)
            {

                return true;

            }

            if(activeCounter >= 100)
            {

                return false;

            }

            eventActive = false;

            EventRemove();

            triggerEvent = true;

            return true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            location.warps.Clear();

            // Monster handle

            monsterHandle = new(origin, location)
            {
                spawnSchedule = new()
            };

            for (int i = 1; i <= 20; i++)
            {

                monsterHandle.spawnSchedule.Add(i, new() { new(MonsterHandle.bosses.darkbrute, Boss.temperment.random, Boss.difficulty.medium) });

            }

            monsterHandle.spawnWithin = eventVectors[2];

            monsterHandle.spawnRange = new(9, 9);

            // Effigy

            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Effigy))
            {

                voices[3] = Mod.instance.characters[CharacterHandle.characters.Effigy];

            }

            HoldCompanions(180);

        }

        public override float SpecialProgress(int displayId)
        {

            switch (displayId)
            {

                case 1:

                    return ((float)progressCounter / 80f);

                default:

                    return ((float)eventRating / 7f);


            }

        }


        public override void EventInterval()
        {

            activeCounter++;

            if(activeCounter < 100)
            {

                SegmentOne();

                return;

            }
            else if (activeCounter < 200)
            {

                SegmentTwo();

                return;

            }
            else if (activeCounter < 300)
            {

                SegmentThree();

                return;

            }
            else if (activeCounter < 400)
            {

                SegmentFour();

                return;

            }


        }


        public void SegmentOne()
        {

            switch (activeCounter)
            {
                case 1:

                    // sergeant enter

                    companions[0] = new Shadowfolk(CharacterHandle.characters.DarkShooter);

                    voices[0] = companions[0];

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[0], eventVectors[0] * 64);

                    companions[0].eventName = eventId;

                    // Doja

                    companions[1] = new Doja(CharacterHandle.characters.Doja);

                    voices[4] = companions[1];

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[1], eventVectors[1] * 64);

                    companions[1].eventName = eventId;

                    // look

                    companions[0].LookAtTarget(companions[1].Position);

                    companions[1].LookAtTarget(companions[0].Position);

                    break;

                case 2:

                    DialogueCue(910);

                    break;

                case 3:

                    DialogueCueWithFeeling(3, 0, Character.Character.specials.gesture);

                    companions[1].LookAtTarget(companions[1].Position + new Vector2(128, 0),true);

                    break;

                case 4:

                    companions[1].LookAtTarget(companions[1].Position + new Vector2(0, 128), true);

                    break;

                case 5:

                    companions[1].LookAtTarget(companions[0].Position, true);

                    break;

                case 6:

                    DialogueCueWithFeeling(6, 0, Character.Character.specials.gesture); //"DialogueData.311.2": "He gets distracted",

                    break;

                case 9:

                    DialogueCueWithFeeling(9); //"DialogueData.311.3": "Well pray it's a disease of competence",

                    break;

                case 12:

                    DialogueCue(12); //"DialogueData.311.4": "Have you swept this entire site?",

                    companions[1].LookAtTarget(companions[1].Position + new Vector2(0, 128), true);

                    break;

                case 15:

                    DialogueCue(15); //"Surface scanners found no traces of ether",

                    break;

                case 18:

                    DialogueCueWithFeeling(18, 0, Character.Character.specials.gesture); //"Orders are not to disturb the interred",

                    break;

                case 21:

                    companions[1].LookAtTarget(companions[0].Position, true);

                    DialogueCueWithFeeling(21, 0, Character.Character.specials.invoke); //"Some desecration is merited",

                    break;

                case 24:

                    companions[1].LookAtTarget(companions[1].Position + new Vector2(0, 128), true);

                    DialogueCue(24); //"If it brings me closer to my desire",

                    break;

                case 26:

                    activeCounter = 1;

                    break;

            }

            if(Vector2.Distance(Game1.player.Position,origin) <= 448)
            {

                activeCounter = 100;

            }

        }

        public void SegmentTwo()
        {

            switch (activeCounter)
            {

                case 101:

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    companions[1].LookAtTarget(Game1.player.Position,true);

                    DialogueCueWithFeeling(101); //[101] = "We've been discovered", new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.950"), },

                    companions[0].doEmote(8);

                    break;

                case 104:

                    DialogueCue(104); //[104] = "One of them twinkle fingers",new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.951"), },

                    break;

                case 107:

                    companions[1].LookAtTarget(companions[0].Position, true);

                    DialogueCue(107); //[107] = "(Annoyed disgust) Deal with the farmer and report to me"new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.9"), }

                    break;

                case 109:

                    companions[1].TargetEvent(100, eventVectors[3] * 64, true);

                    break;

                case 110:

                    DialogueCueWithFeeling(110);//[110] = "Alright lads, to business!" new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.10"), },

                    break;

                case 113:

                    activeCounter = 200;

                    break;

            }

        }

        public void SegmentThree()
        {

            progressCounter++;

            if(activeCounter == 201)
            {

                // remove sergeant

                companions[0].currentLocation.characters.Remove(companions[0]);

                // spawn boss

                bosses[0] = new DarkShooter(ModUtility.PositionToTile(companions[0].Position), Mod.instance.CombatDifficulty());

                bosses[0].SetMode(2);

                bosses[0].netPosturing.Set(true);

                location.characters.Add(bosses[0]);

                bosses[0].currentLocation = location;

                bosses[0].update(Game1.currentGameTime, location);

                voices[0] = bosses[0];

                bosses[0].LookAtFarmer();

                EventBar(Mod.instance.questHandle.quests[eventId].title, 1);

                EventDisplay bomberbar = EventBar(StringData.Strings(StringData.stringkeys.bomberInterruptions), 2);

                bomberbar.colour = Microsoft.Xna.Framework.Color.LightGreen;

                SetTrack("tribal");

            }

            monsterHandle.SpawnCheck();

            if(monsterHandle.monsterSpawns.Count > 0)
            {

                voices[1] = monsterHandle.monsterSpawns.First();

            }

            int shooterCycle = activeCounter % 8;

            switch (shooterCycle)
            {

                case 1:
                    
                    PrepareShooter(); 
                    
                    break;
                
                case 2:
                case 3:

                    StandbyShooter();

                    break;

                case 4:

                    EngageShooter();
                    
                    break;
                
                case 6:
                    
                    RepositionShooter();
                    
                    break;

                case 7:

                    DialogueCue(900);

                    break;
            
            }

            if (activeCounter % 5 == 0)
            {

                monsterHandle.SpawnInterval();

            }

            DialogueCue(activeCounter);

            if (activeCounter == 256)
            {

                activeCounter = 300;

            }

        }

        public void SegmentFour()
        {
            progressCounter++;


            switch (activeCounter)
            {

                //[301] = "Attempting a capture are we"new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.968"), },

                case 301:


                    // shadowtin enter
                    if (Mod.instance.questHandle.IsComplete(eventId))
                    {

                        companions[2] = new Shadowfolk(CharacterHandle.characters.DarkRogue);

                    }
                    else
                    {

                        companions[2] = new Shadowtin(CharacterHandle.characters.Shadowtin);

                    }

                    voices[2] = companions[2];

                    companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[2], eventVectors[4] * 64);

                    companions[2].eventName = eventId;

                    companions[2].TargetEvent(0, eventVectors[5] * 64, true);

                    DialogueCue(activeCounter);

                    bosses[0].LookAtTarget(eventVectors[6] * 64);

                    bosses[0].PerformFlight(eventVectors[6] * 64);

                    break;
                //[302] = "That is the mercenary that hunted me"new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.969"), },

                case 302:

                    DialogueCue(302);

                    break;
                //[304] = "Another twinkler, boss"new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.970"), },

                case 304:

                    DialogueCue(304);

                    break;
                //[307] = "Aim higher"new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.971"), },
                case 307:

                    companions[2].LookAtTarget(bosses[0].Position, true);

                    DialogueCueWithFeeling(307,0,Character.Character.specials.invoke);

                    PrepareShooter();

                    break;

                case 308:
                case 309:

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    StandbyShooter();

                    break;

                //[310] = "Uh, sorry boss", new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.972"), },

                case 310:

                    DialogueCue(310);

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    StandbyShooter();

                    break;

                case 311:

                    EngageShooter();

                    break;

                //[313] = "We're too exposed here, call them all back",new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.973"), },
                case 313:

                    DialogueCueWithFeeling(313);

                    companions[0].Position = bosses[0].Position;

                    voices[0] = companions[0];

                    location.characters.Add(companions[0]);

                    location.characters.Remove(bosses[0]);

                    bosses.Clear();

                    companions[2].LookAtTarget(companions[0].Position, true);

                    companions[0].LookAtTarget(companions[2].Position, true);

                    break;

                case 315:

                    companions[0].TargetEvent(0, eventVectors[7] * 64);

                    break;

                //[316] = "You heard the boss!",new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.974"), },

                case 316:

                    companions[2].TargetEvent(0, eventVectors[8] * 64);

                    DialogueCueWithFeeling(316);

                    break;

                case 317:

                    RemoveMonsters();

                    break;

                //[319] = "Watch yourself, farmer",new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.975"), },

                case 319:

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    companions[0].TargetEvent(301, eventVectors[9] * 64);

                    companions[0].netMovement.Set((int)Character.Character.movements.run);

                    DialogueCue(319);

                    break;

                case 321:

                    companions[2].TargetEvent(302, eventVectors[10] * 64);

                    companions[2].netMovement.Set((int)Character.Character.movements.run);

                    break;

                case 324:

                    eventComplete = true;

                    break;

            }

        }

        public void RepositionShooter(int point = -1)
        {

            List<Vector2> points = ModUtility.GetTilesWithinRadius(location, ModUtility.PositionToTile(origin), 4+Mod.instance.randomIndex.Next(2));

            int tryPoint = 0;

            while(tryPoint++ < 5)
            {

                Vector2 reposition = points[Mod.instance.randomIndex.Next(points.Count)];

                if (ModUtility.GroundCheck(location, reposition) == "ground")
                {

                    bosses[0].LookAtTarget(reposition * 64);

                    bosses[0].PerformFlight(reposition * 64, 0);

                }

                break;

            }

        }

        public void PrepareShooter()
        {

            bosses[0].LookAtTarget(Game1.player.Position);

            bosses[0].netChannelActive.Set(true);

            bosses[0].specialTimer = 90;

            bosses[0].specialFrame = 0;

            DialogueCue(901 + Mod.instance.randomIndex.Next(3));

            Mod.instance.iconData.CursorIndicator(location, Game1.player.Position, IconData.cursors.scope, new() { scale = 4f, scheme = IconData.schemes.stars, });

        }

        public void StandbyShooter()
        {

            if (bosses[0].netChannelActive.Value)
            {

                bosses[0].LookAtTarget(Game1.player.Position);

                bosses[0].specialTimer = 90;

                bosses[0].specialFrame = 0;

                Mod.instance.iconData.CursorIndicator(location, Game1.player.Position, IconData.cursors.scope, new() { scale = 4f, scheme = IconData.schemes.stars, });

            }

        }

        public void EngageShooter()
        {

            if (bosses[0].netChannelActive.Value && Vector2.Distance(bosses[0].Position,Game1.player.Position) > 128 )
            {

                bosses[0].PerformChannel(Game1.player.Position);

                if(Mod.instance.randomIndex.Next(2) == 0)
                {

                    DialogueCue(907 + Mod.instance.randomIndex.Next(3));

                }

            }
            else
            {

                if (Mod.instance.randomIndex.Next(2) == 0)
                {

                    DialogueCue(904 + Mod.instance.randomIndex.Next(3));

                }

                eventRating++;

            }

        }

        public override void EventScene(int index)
        {
            
            switch (index)
            {
                case 100:

                    Mod.instance.iconData.AnimateQuickWarp(companions[1].currentLocation, companions[1].Position, true);

                    companions[1].currentLocation.characters.Remove(companions[1]);

                    activeCounter = 200;

                    break;

                case 301:

                    if (companions.ContainsKey(0))
                    {

                        Mod.instance.iconData.AnimateQuickWarp(companions[0].currentLocation, companions[1].Position, true);

                        companions[0].currentLocation.characters.Remove(companions[0]);

                    }

                    break;

                case 302:

                    if (companions.ContainsKey(2))
                    {

                        Mod.instance.iconData.AnimateQuickWarp(companions[2].currentLocation, companions[1].Position, true);

                        companions[2].currentLocation.characters.Remove(companions[2]);

                    }

                    eventComplete = true;

                    break;
            }
        
        }

    }

}
