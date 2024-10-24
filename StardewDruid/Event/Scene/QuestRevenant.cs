using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Ether;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Companions;
using StardewValley.GameData;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.SpecialOrders;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Event.Scene
{
    public class QuestRevenant : EventHandle
    {

        public Dictionary<int, Vector2> eventVectors = new()
        {

            // LAIR VECTORS
            // Shadowtin enter
            [1] = new Vector2(24, 28),
            // Shadowtin stop
            [2] = new Vector2(24, 16),
            // Jester enter
            [3] = new Vector2(32, 28),
            // Jester stop
            [4] = new Vector2(30, 17),
            // Shadowtin pace
            [5] = new Vector2(25, 13),
            // Mercenary enter
            [6] = new Vector2(29, 4),
            // Mercenary stop
            [7] = new Vector2(28, 12),
            // Jester close
            [8] = new Vector2(28, 14),


            // Shadowtin leave
            [101] = new Vector2(26, 4),
            // Sergeant leave
            [102] = new Vector2(29, 4),
            // Jester leave
            [103] = new Vector2(27, 28),

            // Farmer Chapel Entry
            [109] = new Vector2(26, 30),

            // Revenant position
            [110] = new Vector2(31, 18),
            // Doja position
            [111] = new Vector2(22, 21),
            // Rogue position
            [112] = new Vector2(28, 20),
            // Rogue 2 position
            [113] = new Vector2(31, 21),

            // Revenant settle
            [150] = new Vector2(25, 23),            
            // Doja leave
            [151] = new Vector2(26, 17),
            // Rogue 1 settle
            [152] = new Vector2(37, 23),
            // Rogue 2 settle
            [153] = new Vector2(38, 24),

            // Doja in tomb
            [157] = new Vector2(26.5f, 15),


            // Shadowtin enters
            [201] = new Vector2(27, 32),
            // Shadowtin walks around
            [202] = new Vector2(29, 24),
            // Shadowtin walks to friends
            [203] = new Vector2(34, 24),
            // Marlon enters
            [204] = new Vector2(27, 32),
            // Marlon walks to friends
            [205] = new Vector2(33, 22),
            // Sergeant enters
            [206] = new Vector2(29, 32),
            // Sergeant walks to friends
            [207] = new Vector2(36, 26),


            // Macarbi enters
            [301] = new Vector2(10, 4),
            // Macarbi settles
            [302] = new Vector2(27, 17),
            // Revenant moves back
            [303] = new Vector2(22, 26),
            // Macarbi paces around
            [304] = new Vector2(33,20),
            [305] = new Vector2(27,26),
            [306] = new Vector2(21,20),
            // Jester enters
            [307] = new Vector2(27, 32),
            // Jester settles
            [308] = new Vector2(26, 23),
            // Bat enters
            [309] = new Vector2(24, 34),
            // Bat settles
            [310] = new Vector2(26, 27),
            // Macarbi leaves
            [311] = new Vector2(56, 8),
            // Bat leaves
            [312] = new Vector2(27, 1),
            // Revenant returns
            [366] = new Vector2(24, 24),


            // Jester Leave
            [401] = new Vector2(27, 33),
            // Shadowtin Leave
            [402] = new Vector2(26, 33),
            // Revenant Leave
            [403] = new Vector2(25, 33),
            // Farmer warp to tomb
            [404] = new Vector2(27, 27),
            // Jester Warp
            [405] = new Vector2(24, 26),
            // Shadowtin Warp
            [406] = new Vector2(26, 26),
            // Revenant Warp
            [407] = new Vector2(28, 26),
            // Shadowtin forward
            [408] = new Vector2(26, 21),
            // Jester forward
            [409] = new Vector2(22, 17),
            // Revenant forward
            [410] = new Vector2(32, 17),
            // Thanatoshi settle
            [450] = new Vector2(26, 15),
            // Shadowtin settle
            [451] = new Vector2(23, 17),
            // Jester settle
            [452] = new Vector2(25, 19),
            // Revenant settle
            [453] = new Vector2(28, 16),

        };

        public Dictionary<int,Vector2> ghostStarts = new()
        {

            [0] = new Vector2(17, 0),

            [1] = new Vector2(18, 0),

            [2] = new Vector2(19, 0),

            [3] = new Vector2(21, 0),

            [4] = new Vector2(22, 0),

            [5] = new Vector2(24, 0),

            [6] = new Vector2(25, 0),

            [7] = new Vector2(27, 0),

            [8] = new Vector2(29, 0),

            [9] = new Vector2(31, 0),

            [10] = new Vector2(33, 0),

        };

        public Dictionary<int, Vector2> ghostSettles = new()
        {

            [0] = new Vector2(18, 15),

            [1] = new Vector2(19, 13),

            [2] = new Vector2(21, 14),

            [3] = new Vector2(23, 12),

            [4] = new Vector2(24, 14),

            [5] = new Vector2(26, 13),

            [6] = new Vector2(28, 13),

            [7] = new Vector2(29, 12),

            [8] = new Vector2(31, 12),

            [9] = new Vector2(32, 13),

            [10] = new Vector2(34, 14),

        };


        public Dictionary<int, Vector2> ghostForwards = new()
        {

            [0] = new Vector2(19, 19),

            [1] = new Vector2(20, 18),

            [2] = new Vector2(22, 17),

            [3] = new Vector2(25, 16),

            [4] = new Vector2(26, 18),

            [5] = new Vector2(27, 17),

            [6] = new Vector2(28, 17),

            [7] = new Vector2(29, 16),

            [8] = new Vector2(31, 16),

            [9] = new Vector2(32, 17),

            [10] = new Vector2(33, 18),

        };

        public Dictionary<int, StardewDruid.Character.Character> ghostSummons = new();

        public Dictionary<int, Vector2> batStarts = new()
        {

            [0] = new Vector2(15, 38),

            [1] = new Vector2(17, 37),

            [2] = new Vector2(19, 36),

            [3] = new Vector2(24, 37),

            [4] = new Vector2(29, 36),

            [5] = new Vector2(32, 38),

            [6] = new Vector2(34, 36),

            [7] = new Vector2(36, 37),

            [8] = new Vector2(22, 36),

            [9] = new Vector2(14, 35),

            [10] = new Vector2(35, 38),

        };

        public Dictionary<int, StardewDruid.Character.Character> batSummons = new();

        public Vector2 dragonPosition;

        public QuestRevenant()
        {

            mainEvent = true;

            activeLimit = -1;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            locales = new()
            {
                LocationData.druid_graveyard_name,
                LocationData.druid_chapel_name,
                LocationData.druid_tomb_name,

            };

            origin = eventVectors[1] * 64;

            location.playSound("discoverMineral");

            //Mod.instance.iconData.DecorativeIndicator(location, Game1.player.Position, IconData.decorations.bones, 3f, new());

            SendFriendsHome();

        }

        public override bool AttemptReset()
        {

            Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.abortTomorrow), 3, true);

            return false;

        }

        public override void EventInterval()
        {

            activeCounter++;

            // Introduction (graves) / Mercenary visit / Talk Shadowtin to coordinate plans
            if (activeCounter < 100)
            {

                ScenePartOne();

                return;

            }

            // Warp to chapel / Fight Doja / Talk to Revenant about Doja
            if (activeCounter < 200)
            {

                ScenePartTwo();

                return;

            }

            // Shadowtin arrives with Marlon / Talk to Shadowtin about what happened at spring
            if (activeCounter < 300)
            {

                ScenePartThree();

                return;

            }

            // Cabby arrives to challenge, Jester,Buffin arrives to save / Talk to Revenant about his sin
            if (activeCounter < 400)
            {

                ScenePartFour();

                return;

            }

            // Fight Doja as dragon / Thanatoshi appears / Talk to Revenant about his redemption
            if (activeCounter < 500)
            {

                ScenePartFive();

                return;

            }

            // finish
            if (activeCounter < 600)
            {

                ScenePartSix();

                return;

            }

            // 
            /*if (activeCounter < 700)
            {

                ScenePartSeven();

                return;

            }*/


            eventComplete = true;

        }

        public void ScenePartOne()
        {

            // ========================== Introduction

            switch (activeCounter)
            {

                case 1:

                    Game1.stopMusicTrack(MusicContext.Default);

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Shadowtin] as StardewDruid.Character.Shadowtin;

                    voices[0] = companions[0];

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[0], eventVectors[1] * 64);

                    companions[0].eventName = eventId;

                    companions[0].TargetEvent(0,eventVectors[2] * 64);

                    DialogueCue(1);

                    break;

                case 4:

                    companions[1] = Mod.instance.characters[CharacterHandle.characters.Jester] as StardewDruid.Character.Jester;

                    voices[1] = companions[1];

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[1], eventVectors[3] * 64);

                    companions[1].eventName = eventId;

                    companions[1].TargetEvent(0, eventVectors[4] * 64);

                    DialogueCue(4);

                    break;

                case 7:

                    companions[0].LookAtTarget(companions[1].Position, true);

                    companions[1].ResetActives();

                    companions[1].LookAtTarget(companions[0].Position, true);

                    DialogueCue(7);

                    break;

                case 9:

                    companions[0].TargetEvent(0, eventVectors[5] * 64);

                    break;

                case 10:

                    companions[1].ResetActives();

                    companions[1].netIdle.Set((int)Character.Character.idles.standby);

                    DialogueCue(10);

                    break;

                case 11:

                    companions[0].ResetActives();

                    companions[0].netDirection.Set(1);

                    break;

                case 12:

                    companions[0].ResetActives();

                    companions[0].netDirection.Set(3);

                    break;

                case 13:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueCue(13);

                    break;

                case 15:

                    // sergeant enter

                    companions[2] = new Shadowtin(CharacterHandle.characters.DarkShooter);

                    voices[2] = companions[2];

                    companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[2], eventVectors[6] * 64);

                    companions[2].eventName = eventId;

                    companions[2].TargetEvent(0, eventVectors[7] * 64);

                    break;

                case 16:

                    companions[0].LookAtTarget(companions[2].Position,true);

                    companions[0].doEmote(16);

                    companions[1].ResetActives(true);

                    companions[1].LookAtTarget(companions[2].Position, true);

                    companions[1].doEmote(16);

                    DialogueCue(16);

                    break;

                case 19:

                    companions[0].LookAtTarget(companions[2].Position, true);

                    companions[1].ResetActives(true);

                    companions[1].LookAtTarget(companions[2].Position, true);

                    DialogueCueWithFeeling(19);

                    break;

                case 22:

                    companions[0].LookAtTarget(companions[2].Position, true);

                    companions[1].ResetActives(true);

                    companions[1].LookAtTarget(companions[2].Position, true);

                    DialogueCueWithFeeling(22, 0, Character.Character.specials.gesture);

                    break;

                case 25:

                    companions[2].ResetActives(true);

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    DialogueCueWithFeeling(25);

                    break;

                case 27:

                    companions[2].ResetActives(true);

                    companions[2].LookAtTarget(companions[1].Position, true);

                    break;

                case 28:

                    companions[2].ResetActives(true);

                    companions[2].LookAtTarget(companions[0].Position, true);

                    companions[1].netIdle.Set((int)Character.Character.idles.standby);

                    DialogueCueWithFeeling(28, 0, Character.Character.specials.gesture);

                    break;

                case 31:

                    companions[0].ResetActives(true);

                    companions[0].LookAtTarget(companions[2].Position, true);

                    DialogueCueWithFeeling(31,0, Character.Character.specials.gesture);

                    break;

                case 34:

                    companions[2].ResetActives(true);

                    companions[2].LookAtTarget(companions[0].Position, true);

                    DialogueCueWithFeeling(34, 0, Character.Character.specials.gesture);

                    companions[0].LookAtTarget(companions[2].Position, true);

                    companions[0].doEmote(16);

                    companions[1].ResetActives(true);

                    companions[1].LookAtTarget(companions[2].Position, true);

                    companions[1].doEmote(16);

                    break;

                case 38:

                    DialogueCue(38);

                    break;

                case 41:

                    DialogueCueWithFeeling(41, 0, Character.Character.specials.gesture);

                    break;

                case 43:

                    companions[2].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                case 44:

                    companions[0].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[1].TargetEvent(0, eventVectors[8] * 64, true);

                    DialogueCue(44);

                    break;

                case 48:

                    companions[0].ResetActives();

                    DialogueCueWithFeeling(48);

                    break;

                case 51:

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    companions[1].LookAtTarget(Game1.player.Position, true);

                    companions[2].ResetActives();

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    DialogueLoad(companions[0], 1);

                    break;

                case 60:


                    DialogueClear(companions[0]);

                    activeCounter = 100;

                    break;

            }

        }


        public void ScenePartTwo()
        {

            // =======================  Doja Fight

            if (bosses.ContainsKey(0))
            {

                if (bosses[0].netWoundedActive.Value)
                {

                    if (activeCounter < 150)
                    {

                        activeCounter = 150;

                        return;

                    }

                }
                else if (!ModUtility.MonsterVitals(bosses[0], location))
                {

                    bosses[0].netWoundedActive.Set(true);

                    bosses[0].Health = bosses[0].MaxHealth;

                    activeCounter = 150;

                    return;

                }

            }

            // ==========================

            DialogueCue(activeCounter);


            switch (activeCounter)
            {

                case 101:

                    location.warps.Clear();

                    companions[0].LookAtTarget(companions[2].Position, true);

                    break;

                case 102:

                    companions[0].TargetEvent(0, eventVectors[101] * 64, true);

                    companions[2].TargetEvent(0, eventVectors[102] * 64, true);

                    break;

                case 104:

                    companions[1].TargetEvent(0, eventVectors[103] * 64, true);

                    break;

                case 106:

                    location.updateWarps();

                    location = Mod.instance.locations[LocationData.druid_chapel_name];

                    (location as Chapel).activeCircle = true;

                    Game1.warpFarmer(location.Name, (int)eventVectors[109].X, (int)eventVectors[109].Y, 0);

                    // Revenant

                    companions[3] = Mod.instance.characters[CharacterHandle.characters.Revenant] as StardewDruid.Character.Revenant;

                    voices[3] = companions[3];

                    companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[3], eventVectors[110] * 64);

                    companions[3].eventName = eventId;

                    companions[3].netIdle.Set((int)Character.Character.idles.standby);

                    companions[3].LookAtTarget(eventVectors[109] * 64, true);

                    // Doja

                    LoadBoss(new DarkMage(eventVectors[111], Mod.instance.CombatDifficulty()) { basePulp = 40, }, 0, 3, 4);

                    bosses[0].setWounded = true;

                    // Rogue

                    LoadBoss(new DarkRogue(eventVectors[112], Mod.instance.CombatDifficulty()), 1, 2, 5);

                    bosses[1].setWounded = true;

                    // Rogue 2

                    LoadBoss(new DarkGoblin(eventVectors[113], Mod.instance.CombatDifficulty()), 2, 2, 6);

                    bosses[2].setWounded = true;

                    // stances

                    bosses[0].SetDirection(companions[3].Position);

                    companions[3].LookAtTarget(bosses[0].Position, true);

                    bosses[1].SetDirection(companions[3].Position);

                    bosses[1].netAlert.Set(true);

                    bosses[1].idleTimer = 300;

                    bosses[1].groupMode = true;

                    bosses[2].SetDirection(companions[3].Position);

                    bosses[2].netAlert.Set(true);

                    bosses[2].idleTimer = 300;

                    bosses[2].groupMode = true;

                    SetTrack("tribal");

                    break;

                case 109:

                    bosses[0].SetDirection(Game1.player.Position);

                    break;

                case 113:

                    (location as Chapel).activeCircle = true;

                    bosses[0].netPosturing.Set(false);

                    bosses[1].netPosturing.Set(false);

                    bosses[2].netPosturing.Set(false);

                    companions[3].ResetActives();

                    companions[3].SwitchToMode(Character.Character.mode.track, Game1.player);

                    break;

                case 151:

                    bosses[0].ResetActives();

                    bosses[1].netPosturing.Set(true);

                    bosses[0].netWoundedActive.Set(true);

                    bosses[1].ResetActives();

                    bosses[1].netPosturing.Set(true);

                    bosses[1].netWoundedActive.Set(true);

                    bosses[2].ResetActives();

                    bosses[2].netPosturing.Set(true);

                    bosses[2].netWoundedActive.Set(true);

                    bosses[1].doEmote(28);

                    bosses[2].doEmote(28);

                    // Revenant
                    companions[3].ResetActives();

                    companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[3].eventName = eventId;

                    companions[3].TargetEvent(150, eventVectors[150] * 64, true);

                    break;

                case 153:

                    // Doja character switch

                    companions[4] = new Wizard(CharacterHandle.characters.Doja);

                    voices[4] = companions[4];

                    companions[4].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[4], bosses[0].Position, false);

                    companions[4].eventName = eventId;

                    companions[4].TargetEvent(151, eventVectors[151] * 64);

                    companions[4].netMovement.Set((int)Character.Character.movements.run);

                    // Rogue character switch

                    companions[5] = new Shadowtin(CharacterHandle.characters.DarkRogue);

                    voices[5] = companions[5];

                    companions[5].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[5], bosses[1].Position, false);

                    companions[5].eventName = eventId;

                    companions[5].TargetEvent(152, eventVectors[152] * 64);

                    // Rogue 2 character switch

                    companions[6] = new Shadowtin(CharacterHandle.characters.DarkGoblin);

                    voices[6] = companions[6];

                    companions[6].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[6], bosses[2].Position, false);

                    companions[6].eventName = eventId;

                    companions[6].TargetEvent(153, eventVectors[153] * 64);

                    foreach(KeyValuePair<int,Boss> boss in bosses)
                    {
                        
                        location.characters.Remove(boss.Value);

                    }

                    bosses.Clear();

                    RemoveSummons();

                    break;

                case 157:

                    DialogueLoad(companions[3], 2);

                    StopTrack();

                    break;

                case 165:

                    DialogueClear(companions[3]);

                    activeCounter = 200;

                    break;

            }

        }

        public void ScenePartThree()
        {

            // ==========================

            switch (activeCounter)
            {

                case 201:

                    StopTrack();

                    DialogueCue(201);

                    CharacterMover.Warp(location, companions[0], eventVectors[201]*64, false);

                    companions[0].TargetEvent(0, eventVectors[202] * 64, true);

                    companions[3].LookAtTarget(companions[0].Position, true);

                    break;

                case 205:

                    companions[0].LookAtTarget(companions[5].Position, true);

                    break;

                case 206:

                    companions[0].LookAtTarget(companions[3].Position, true);

                    break;

                case 207:

                    DialogueCueWithFeeling(207, 0, Character.Character.specials.gesture);

                    break;

                case 210:

                    DialogueCueWithFeeling(210, 0, Character.Character.specials.gesture);

                    break;

                case 213:

                    companions[3].LookAtTarget(companions[5].Position, true);

                    DialogueCueWithFeeling(213);

                    companions[5].doEmote(28);

                    companions[6].doEmote(28);

                    break;

                case 215:

                    companions[0].TargetEvent(0, eventVectors[203] * 64, true);

                    break;

                case 216:

                    // Shadowtin talk

                    companions[0].ResetActives(true);

                    DialogueCueWithFeeling(216, 0, Character.Character.specials.gesture);

                    companions[5].doEmote(32);

                    companions[6].doEmote(32);

                    // Marlon

                    companions[7] = new Marlon(CharacterHandle.characters.Marlon);

                    companions[7].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[7].eventName = eventId;

                    voices[7] = companions[7];

                    CharacterMover.Warp(location, companions[7], eventVectors[204] * 64, false);

                    companions[7].TargetEvent(201, eventVectors[205] * 64, true);


                    break;

                case 218:

                    // Shadowtin move, kneel

                    companions[0].TargetEvent(203, eventVectors[203] * 64, true);

                    // Sergeant

                    CharacterMover.Warp(location, companions[2], eventVectors[206] * 64, false);

                    companions[2].TargetEvent(202, eventVectors[207] * 64, true);

                    break;

                case 219:

                    DialogueCue(219);

                    break;

                case 222:

                    DialogueCue(222);

                    break;

                case 225:

                    DialogueCue(225);

                    break;

                case 228:

                    DialogueCue(228);

                    break;

                case 231:

                    companions[0].ResetActives(true);

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    companions[2].ResetActives(true);

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    companions[3].ResetActives(true);

                    companions[3].LookAtTarget(companions[0].Position, true);

                    companions[5].ResetActives(true);

                    companions[5].LookAtTarget(Game1.player.Position, true);

                    companions[6].ResetActives(true);

                    companions[6].LookAtTarget(Game1.player.Position, true);

                    companions[7].ResetActives(true);

                    companions[7].LookAtTarget(Game1.player.Position, true);

                    DialogueLoad(companions[0],3);

                    break;

                case 240:

                    DialogueClear(companions[0]);

                    activeCounter = 300;

                    break;

            }

        }

        public void ScenePartFour()
        {

            // ========================== 

            switch (activeCounter)
            {

                case 301:

                    // Macarbi

                    companions[8] = new Flyer(CharacterHandle.characters.Macarbi);

                    voices[8] = companions[8];

                    companions[8].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[8], eventVectors[301] * 64);

                    companions[8].TargetEvent(301, eventVectors[302] * 64, true);

                    break;

                case 304:
                    DialogueCue(304); 
                    break; // = new() { [3] = "What's this angry bird then", },
                case 307:
                    DialogueCue(307);

                    companions[0].doEmote(8);

                    companions[2].doEmote(8);

                    companions[3].doEmote(8);

                    companions[5].doEmote(8);

                    companions[6].doEmote(8);

                    companions[7].doEmote(8);

                    break; // ] = new() { [8] = "Criminals!", },

                case 308:

                    companions[8].TargetEvent(0, eventVectors[304] * 64, true);
                    companions[8].TargetEvent(1, eventVectors[305] * 64, false);
                    companions[8].TargetEvent(2, eventVectors[306] * 64, false);
                    companions[8].TargetEvent(301, eventVectors[302] * 64, false);
                    break;

                case 310:
                    DialogueCue(310); 
                    break; // ] = new() { [8] = "The testimonies against your Circle overwhelm the Court", },
                case 313:
                    DialogueCue(313); 
                    break; // ] = new() { [3] = "What?", },
                case 316:

                    DialogueCue(316);
                    
                    companions[3].TargetEvent(0, eventVectors[303] * 64, true);
                    
                    companions[3].LookAtTarget(companions[8].Position, true);

                    break; // ] = new() { [8] = "Envoys of the Fae, murdered. Justice, subverted.", },

                case 317:

                    location.playSound(SpellHandle.sounds.ghost.ToString());

                    break;

                case 318:

                    foreach (KeyValuePair<int, Vector2> ghostStart in ghostStarts)
                    {

                        ghostSummons[ghostStart.Key] = new Hoverer(CharacterHandle.characters.Spectre);

                        ghostSummons[ghostStart.Key].setScale -= Mod.instance.randomIndex.Next(4) * 0.2f;

                        ghostSummons[ghostStart.Key].SwitchToMode(Character.Character.mode.scene, Game1.player);

                        ghostSummons[ghostStart.Key].fadeOut = 0.7f;

                        CharacterMover.Warp(location, ghostSummons[ghostStart.Key], ghostStart.Value * 64);

                    }

                    //location.playSound(SpellHandle.sounds.ghost.ToString());

                    break;

                case 319:

                    foreach (KeyValuePair<int, StardewDruid.Character.Character> ghost in ghostSummons)
                    {

                        ghost.Value.TargetEvent(0, ghostSettles[ghost.Key] * 64);

                    }

                    location.playSound(SpellHandle.sounds.ghost.ToString());

                    DialogueCue(319); 
                    break; // ] = new() { [3] = "Meaning?", },
                case 322:
                    DialogueCue(322);

                    companions[0].doEmote(16);

                    companions[2].doEmote(16);

                    companions[3].doEmote(16);

                    companions[5].doEmote(16);

                    companions[6].doEmote(16);

                    companions[7].doEmote(16);

                    // Jester enters

                    CharacterMover.Warp(location, companions[1], eventVectors[307] * 64);

                    companions[1].TargetEvent(0, eventVectors[308] * 64, true);

                    break; // ] = new() { [8] = "Your souls are to be reaped... and weighed", },

                case 325:
                    DialogueCue(325); 
                    break; // ] = new() { [1] = "No", },
                case 328:
                    DialogueCue(328); 
                    break; // ] = new() { [8] = "Excuse me?", },
                case 331:
                    DialogueCue(331);
                    companions[1].LookAtTarget(companions[0].Position, true);
                    break; // ] = new() { [1] = "These faithful are sanctified by the Priesthood", },
                case 332:
                    companions[1].LookAtTarget(companions[3].Position, true);
                    break;
                case 333:
                    companions[1].LookAtTarget(Game1.player.Position, true);
                    break;
                case 334:
                    companions[1].LookAtTarget(companions[8].Position, true);
                    DialogueCue(334); 
                    break; // ] = new() { [8] = "I doubt you bear such authority", },
                case 336:
                    companions[8].TargetEvent(0, eventVectors[306] * 64, true);
                    companions[8].TargetEvent(1, eventVectors[305] * 64, false);
                    companions[8].TargetEvent(2, eventVectors[304] * 64, false);
                    companions[8].TargetEvent(301, eventVectors[302] * 64, false);
                    break;
                case 337:
                    DialogueCue(337);
                    companions[0].doEmote(16);
                    companions[0].LookAtTarget(companions[0].Position, true);
                    companions[7].doEmote(16);
                    companions[7].LookAtTarget(companions[0].Position, true);
                    break; // ] = new() { [8] = "This mortal, this shadow, their deaths were foretold", },
                case 340:
                    DialogueCue(340); 
                    break; // ] = new() { [8] = "You have denied them glory!", },
                case 343:
                    DialogueCue(343); 
                    break; // ] = new() { [1] = "I, THE JESTER OF FATES, HAVE WILLED IT SO", },
                case 346:
                    DialogueCue(346); 
                    break; // ] = new() { [8] = "BLASPHEMY. YOU SHAME FORTUMEI", },
                case 349:

                    foreach (KeyValuePair<int, StardewDruid.Character.Character> ghost in ghostSummons)
                    {

                        ghost.Value.TargetEvent(0, ghostForwards[ghost.Key] * 64);

                    }

                    DialogueCue(349); 
                    break; // ] = new() { [8] = "Sieze them all", },
                case 352:

                    foreach (KeyValuePair<int, StardewDruid.Character.Character> ghost in ghostSummons)
                    {

                        ghost.Value.doEmote(8);

                    }

                    DialogueCue(352);

                    location.playSound(SpellHandle.sounds.batScreech.ToString());
                    break; // ] = new() { [1] = "cheep?", },
                case 353:
                    location.playSound(SpellHandle.sounds.batFlap.ToString());
                    break;
                case 354:

                    //location.playSound(SpellHandle.sounds.batScreech.ToString());
                    break;
                case 355:
                    companions[9] = new Hoverer(CharacterHandle.characters.ShadowBat);

                    voices[9] = companions[9];

                    companions[9].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[9], eventVectors[309] * 64);

                    companions[9].TargetEvent(0, eventVectors[310] * 64, true);

                    location.playSound(SpellHandle.sounds.batScreech.ToString());


                    break;
                case 356:

                    location.playSound(SpellHandle.sounds.batFlap.ToString());
                    break;
                case 357:
                    //location.playSound(SpellHandle.sounds.batScreech.ToString());
                    break;
                case 358:

                    foreach (KeyValuePair<int, StardewDruid.Character.Character> ghost in ghostSummons)
                    {

                        ghost.Value.TargetEvent(0, ghostSettles[ghost.Key]*64);

                        ghost.Value.LookAtTarget(companions[9].Position);

                    }
                    // Look at cleric bat

                    companions[0].LookAtTarget(companions[9].Position, true);

                    companions[1].LookAtTarget(companions[9].Position, true);

                    companions[2].LookAtTarget(companions[9].Position, true);

                    companions[3].LookAtTarget(companions[9].Position, true);

                    companions[5].LookAtTarget(companions[9].Position, true);

                    companions[6].LookAtTarget(companions[9].Position, true);

                    companions[7].LookAtTarget(companions[9].Position, true);

                    companions[8].LookAtTarget(companions[9].Position, true);

                    DialogueCue(358);
                    location.playSound(SpellHandle.sounds.batFlap.ToString());
                    break; // ] = new() { [9] = "CHEEP!", },
                case 359:
                    //location.playSound(SpellHandle.sounds.batFlap.ToString());
                    break;
                case 360:
                    //location.playSound(SpellHandle.sounds.batScreech.ToString());
                    foreach (KeyValuePair<int, StardewDruid.Character.Character> ghost in ghostSummons)
                    {
                        ghost.Value.LookAtTarget(companions[9].Position, true);

                    }

                    foreach (KeyValuePair<int, Vector2> batStart in batStarts)
                    {

                        batSummons[batStart.Key] = new Hoverer(CharacterHandle.characters.ShadowBat);

                        batSummons[batStart.Key].gait *= 1.5f;

                        batSummons[batStart.Key].setScale -= Mod.instance.randomIndex.Next(4) * 0.2f;

                        batSummons[batStart.Key].SwitchToMode(Character.Character.mode.scene, Game1.player);

                        CharacterMover.Warp(location, batSummons[batStart.Key], batStart.Value * 64);

                        batSummons[batStart.Key].LookAtTarget(ghostSettles[batStart.Key] * 64, true);

                    }

                    break;
                case 361:

                    foreach (KeyValuePair<int, StardewDruid.Character.Character> bat in batSummons)
                    {

                        bat.Value.TargetEvent(800+bat.Key, ghostSettles[bat.Key] * 64);

                        bat.Value.netMovement.Set((int)Character.Character.movements.run);

                    }

                    DialogueCue(361);

                    location.playSound(SpellHandle.sounds.batFlap.ToString());

                    break; // ] = new() { [9] = "Protect skull friends and lady friends!", },
                case 362:

                    //location.playSound(SpellHandle.sounds.batScreech.ToString());
                   
                    DialogueCue(362);

                    companions[8].TargetEvent(303, eventVectors[311] * 64, true);

                    companions[9].TargetEvent(302, eventVectors[312] * 64, true);

                    // look at Macarbi

                    companions[0].LookAtTarget(companions[8].Position, true);

                    companions[1].LookAtTarget(companions[8].Position, true);

                    companions[2].LookAtTarget(companions[8].Position, true);

                    companions[3].LookAtTarget(companions[8].Position, true);

                    companions[5].LookAtTarget(companions[8].Position, true);

                    companions[6].LookAtTarget(companions[8].Position, true);

                    companions[7].LookAtTarget(companions[8].Position, true);

                    break;

                case 363:

                    location.playSound(SpellHandle.sounds.batScreech.ToString());

                    break;

                case 366:

                    companions[1].netIdle.Set((int)Character.Character.idles.standby);

                    companions[3].TargetEvent(0, eventVectors[366] * 64, true);

                    DialogueCue(366);

                    companions[0].doEmote(20);

                    companions[2].doEmote(20);

                    companions[3].doEmote(20);

                    companions[5].doEmote(20);

                    companions[6].doEmote(20);

                    companions[7].doEmote(20);

                    companions[0].LookAtTarget(companions[1].Position, true);

                    companions[2].LookAtTarget(companions[1].Position, true);

                    companions[3].LookAtTarget(companions[1].Position, true);

                    companions[5].LookAtTarget(companions[1].Position, true);

                    companions[6].LookAtTarget(companions[1].Position, true);

                    companions[7].LookAtTarget(companions[1].Position, true);


                    break; // ] = new() { [1] = "This too, I foresaw", },

                case 369:

                    DialogueLoad(companions[3], 4);

                    break;

                case 379:

                    DialogueClear(companions[3]);

                    activeCounter = 400;

                    break;

            }

        }

        public void ScenePartFive()
        {

            // =======================  Dragon Fight

            if (bosses.ContainsKey(0))
            {

                dragonPosition = bosses[0].Position;

                DialogueCue(activeCounter);

                if (bosses[0].netWoundedActive.Value)
                {

                    if (activeCounter < 475)
                    {

                        activeCounter = 475;

                        return;

                    }

                }
                else if (!ModUtility.MonsterVitals(bosses[0], location))
                {

                    location.characters.Remove(bosses[0]);

                    location.characters.Add(bosses[0]);

                    bosses[0].currentLocation = location;

                    bosses[0].netWoundedActive.Set(true);

                    bosses[0].Health = bosses[0].MaxHealth;

                    activeCounter = 475;

                    return;

                }

            }

            switch (activeCounter)
            {

                case 401:

                    location.warps.Clear();

                    DialogueCue(401);

                    companions[1].LookAtTarget(companions[3].Position, true);

                    break;

                case 404:

                    DialogueCue(404);

                    break;

                case 407:

                    DialogueCue(407);

                    companions[1].TargetEvent(401,eventVectors[401] * 64, true);

                    break;

                case 410:

                    DialogueCue(410);

                    companions[0].TargetEvent(402, eventVectors[402] * 64, true);

                    break;

                case 412:

                    companions[3].TargetEvent(403, eventVectors[403] * 64, true);

                    break;

                case 413:

                    DialogueCue(413);

                    break;

                case 416:

                    location.updateWarps();

                    location = Mod.instance.locations[LocationData.druid_tomb_name];

                    (location as Tomb).activeCircle = true;

                    Game1.warpFarmer(location.Name, (int)eventVectors[404].X, (int)eventVectors[404].Y, 0);

                    break;

                case 417:

                    companions[0].TargetEvent(0, eventVectors[408] * 64, false);

                    companions[1].TargetEvent(0, eventVectors[409] * 64, false);

                    companions[3].TargetEvent(0, eventVectors[410] * 64, false);

                    break;

                case 419:

                    DialogueCueWithFeeling(419);

                    companions[4].LookAtTarget(companions[0].Position, true);

                    break;//[419] = new() { [0] = "Doja, depart from this place", },

                case 422:

                    companions[0].LookAtTarget(companions[4].Position, true);

                    companions[1].LookAtTarget(companions[4].Position, true);

                    companions[3].LookAtTarget(companions[4].Position, true);

                    DialogueCueWithFeeling(422);

                    break;//] = new() { [0] = "You do not possess the temperance to wield power", },
                case 425:

                    DialogueCueWithFeeling(425);

                    location.playSound("DragonRoar");

                    break;//] = new() { [4] = "DoJaH?", },
                case 428:

                    DialogueCueWithFeeling(428);

                    companions[0].netIdle.Set((int)Character.Character.idles.alert);

                    companions[1].netIdle.Set((int)Character.Character.idles.alert);

                    companions[3].netIdle.Set((int)Character.Character.idles.alert);

                    location.playSound("DragonRoar");

                    break; //] = new() { [4] = "A UsEleSs nAmE fOr a pEtTy VeSsel", },

                case 430:

                    location.characters.Remove(companions[4]);

                    StardewDruid.Monster.Dragon dragon = new StardewDruid.Monster.Dragon(ModUtility.PositionToTile(companions[4].Position), Mod.instance.CombatDifficulty());

                    dragon.dragonRender.LoadColourScheme(DragonRender.dragonSchemes.dragon_black);

                    dragon.dragonRender.LoadBreathScheme(DragonRender.breathSchemes.breath_ether);

                    dragon.basePulp = 50;

                    dragon.groupMode = true;

                    LoadBoss(dragon, 0, 4, 10);

                    dragonPosition = bosses[0].Position;

                    SetTrack("cowboy_boss");

                    SpellHandle dragonPuff = new(companions[4].Position, 256, IconData.impacts.deathbomb, new());

                    dragonPuff.sound = SpellHandle.sounds.shadowDie;

                    Mod.instance.spellRegister.Add(dragonPuff);

                    break;

                case 431:

                    bosses[0].netPosturing.Set(false);

                    // Start Combat

                    companions[0].SwitchToMode(Character.Character.mode.track, Game1.player);

                    companions[0].specialDisable = true;

                    companions[1].SwitchToMode(Character.Character.mode.track, Game1.player);

                    companions[1].specialDisable = true;

                    companions[3].SwitchToMode(Character.Character.mode.track, Game1.player);

                    companions[3].specialDisable = true;

                    break;

                    /*[431] = new() { [10] = "SUBJECTS", },
                    [434] = new() { [10] = "You witness the return of your rightful king", },
                    [437] = new() { [10] = "I am grateful for your sacrifice", },
                    [440] = new() { [3] = "Great Jin, you are mistaken", },
                    [443] = new() { [3] = "Your life is past, and you cannot reclaim it", },
                    [446] = new() { [3] = "Let go of this world", },
                    [449] = new() { [10] = "SILENCE", },
                    [452] = new() { [10] = "I will consume the essence of your souls", },
                    [455] = new() { [10] = "You will continue to serve in my undead legion", },
                    [458] = new() { [1] = "This lizard refuses to quit", },
                    [461] = new() { [0] = "I'm... breathing is difficult... ", },
                    [464] = new() { [3] = "Aye. Starting to heat up in here.", },
                    [467] = new() { [10] = "I am diminished, but resolute", },
                    [470] = new() { [10] = "Mysterus will answer for his treachery", },
                    [473] = new() { [1] = "Keep going, almost there", },*/

                case 476:

                    bosses[0].ResetActives();

                    bosses[0].netPosturing.Set(true);

                    bosses[0].netWoundedActive.Set(true);

                    DialogueCue(476);

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[0].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[1].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[3].netIdle.Set((int)Character.Character.idles.kneel);

                    break;//] = new() { [0] = "Exhausted... retreat is our only option", },

                case 478:
                case 479:
                case 480:

                    if(bosses.Count > 0)
                    {

                        bosses[0].netDirection.Set(Mod.instance.randomIndex.Next(4));

                    }

                    List<int> directions = new() { 0, 1, 2, 3, 4, 5, 6, 7, };

                    int delay = 0;

                    for (int i = 0; i < 4; i++)
                    {

                        SpellHandle sweep = new(Game1.player, new() { bosses[0] }, Mod.instance.CombatDamage());

                        sweep.type = SpellHandle.spells.warpstrike;

                        int d = directions[Mod.instance.randomIndex.Next(directions.Count)];

                        directions.Remove(d);

                        sweep.counter = 0 - delay;

                        delay += 15;

                        sweep.scheme = IconData.schemes.death;

                        sweep.projectile = d;

                        Mod.instance.spellRegister.Add(sweep);

                    }

                    break;

                case 481:

                    companions[0].doEmote(16);

                    companions[1].doEmote(16);

                    companions[3].doEmote(16);

                    SpellHandle dragonDeath = new(companions[4].Position, 256, IconData.impacts.deathbomb, new());

                    dragonDeath.sound = SpellHandle.sounds.shadowDie;

                    Mod.instance.spellRegister.Add(dragonDeath);

                    CharacterMover.Warp(location, companions[4], dragonPosition);

                    companions[4].ResetActives();

                    companions[4].netSpecial.Set((int)Character.Character.specials.pickup);

                    companions[4].specialTimer = 180;

                    location.characters.Remove(bosses[0]);

                    bosses.Remove(0);

                    voices.Remove(10);

                    DialogueCue(481);

                    break;//] = new() { [4] = "...(HACK)... how...", },

                case 483:

                    StopTrack();

                    RemoveSummons();
                    
                    companions[11] = new Thanatoshi(CharacterHandle.characters.Thanatoshi);

                    voices[11] = companions[11];

                    companions[11].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[11], companions[4].Position + new Vector2(64,0), false);

                    companions[11].eventName = eventId;

                    companions[11].TargetEvent(450, eventVectors[450] * 64);

                    break;

                case 484:

                    SpellHandle dojaDeath = new(companions[4].Position, 256, IconData.impacts.deathbomb, new());

                    dojaDeath.sound = SpellHandle.sounds.shadowDie;

                    Mod.instance.spellRegister.Add(dojaDeath);

                    location.characters.Remove(companions[4]);

                    companions.Remove(4);

                    voices.Remove(4);

                    break;

                case 485:

                    DialogueCue(485);

                    break;//] = new() { [11] = "I have remembered who I am", },

                case 488:

                    DialogueCue(488);

                    companions[0].ResetActives();

                    companions[0].TargetEvent(451, eventVectors[451] * 64);

                    companions[1].ResetActives();

                    companions[1].TargetEvent(452, eventVectors[452] * 64);

                    companions[3].ResetActives();

                    companions[3].TargetEvent(453, eventVectors[453] * 64);

                    break;//] = new() { [11] = "No one escapes the Reaper", },

                case 491:

                    DialogueLoad(companions[11], 5);

                    break;

                case 499:

                    DialogueClear(companions[11]);

                    activeCounter = 500;

                    break;


            }

        }

        public void ScenePartSix()
        {

            switch (activeCounter)
            {

                case 501:

                    DialogueCue(501);

                    break;//] = new() { [11] = "I can't go to the fields alone", },
                case 504:

                    DialogueCueWithFeeling(504);

                    break;//] = new() { [11] = "Who will write my name in the book of morticians?", },
                case 507:

                    DialogueCue(507);

                    break;//] = new() { [3] = "I'll take you", },
                case 510:

                    DialogueCue(510);

                    break;//] = new() { [11] = "Unexpected. Do you not despise me?", },
                case 513:

                    DialogueCueWithFeeling(513, 0, Character.Character.specials.gesture);

                    break;//] = new() { [3] = "Heh. Nay, friend.", },
                case 516:

                    companions[0].LookAtTarget(companions[1].Position, true);

                    companions[3].LookAtTarget(companions[1].Position, true);

                    companions[11].LookAtTarget(companions[1].Position, true);

                    DialogueCueWithFeeling(516);

                    break;//] = new() { [3] = "This is how it's meant to be, right cat?", },

                case 517:

                    companions[1].doEmote(32);

                    companions[1].netIdle.Set((int)Character.Character.idles.standby);

                    break;

                case 519:

                    DialogueCue(519);

                    break;//] = new() { [11] = "Lead the way", },

                case 522:

                    companions[11].ResetActives();

                    companions[11].netDirection.Set(0);

                    DialogueCueWithFeeling(522);

                    break;//] = new() { [11] = "Lead the way", },

                case 523:

                    companions[0].LookAtTarget(companions[3].Position, true);

                    companions[1].LookAtTarget(companions[3].Position, true);

                    companions[3].TargetEvent(501, companions[3].Position - new Vector2(0, 64));

                    companions[3].TargetEvent(502, companions[3].Position - new Vector2(0, 128), false);

                    companions[3].TargetEvent(503, companions[3].Position - new Vector2(0, 192), false);

                    companions[3].TargetEvent(504, companions[3].Position - new Vector2(0, 256), false);

                    companions[3].TargetEvent(505, companions[3].Position - new Vector2(0, 320), false);

                    companions[3].TargetEvent(506, companions[3].Position - new Vector2(0, 384), false);

                    companions[3].TargetEvent(507, companions[3].Position - new Vector2(0, 448), false);

                    companions[3].TargetEvent(508, companions[3].Position - new Vector2(0, 512), false);

                    companions[3].TargetEvent(509, companions[3].Position - new Vector2(0, 576), false);

                    companions[11].TargetEvent(510, companions[11].Position - new Vector2(0, 64));

                    companions[11].TargetEvent(511, companions[11].Position - new Vector2(0, 128), false);

                    companions[11].TargetEvent(512, companions[11].Position - new Vector2(0, 192), false);

                    companions[11].TargetEvent(513, companions[11].Position - new Vector2(0, 256), false);

                    companions[11].TargetEvent(514, companions[11].Position - new Vector2(0, 320), false);

                    companions[11].TargetEvent(515, companions[11].Position - new Vector2(0, 384), false);

                    companions[11].TargetEvent(516, companions[11].Position - new Vector2(0, 448), false);

                    companions[11].TargetEvent(517, companions[11].Position - new Vector2(0, 512), false);

                    companions[11].TargetEvent(518, companions[11].Position - new Vector2(0, 576), false);

                    break;

                case 530:

                    eventComplete = true;

                    break;

            }

        }


        public override void EventScene(int index)
        {

            switch (index)
            {

                case 150:

                    companions[3].LookAtTarget(Game1.player.Position,true);

                    break;

                case 151:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[4].Position, true);

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_tomb_name], companions[4], eventVectors[157] * 64, false);

                    break;

                case 152:

                    companions[5].ResetActives();
                    
                    companions[5].LookAtTarget(Game1.player.Position, true);

                    companions[5].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[5].doEmote(28);

                    break;

                case 153:

                    companions[6].ResetActives();

                    companions[6].LookAtTarget(Game1.player.Position,true);

                    companions[6].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[6].doEmote(28);

                    break;

                case 201:

                    companions[7].ResetActives();

                    companions[7].LookAtTarget(companions[0].Position, true);

                    break;

                case 202:

                    companions[2].ResetActives();

                    companions[2].LookAtTarget(companions[5].Position, true);

                    companions[6].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[6].doEmote(32);

                    break;

                case 203:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(companions[5].Position, true);

                    companions[0].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                case 301:

                    companions[8].ResetActives();

                    companions[8].LookAtTarget(Game1.player.Position, true);

                    companions[8].netIdle.Set((int)Character.Character.idles.standby);

                    companions[0].LookAtTarget(companions[8].Position, true);

                    companions[2].LookAtTarget(companions[8].Position, true);

                    companions[3].LookAtTarget(companions[8].Position, true);

                    companions[5].LookAtTarget(companions[8].Position, true);

                    companions[6].LookAtTarget(companions[8].Position, true);

                    companions[7].LookAtTarget(companions[8].Position, true);

                    break;

                case 302:

                    location.characters.Remove(companions[8]);

                    companions.Remove(8);

                    voices.Remove(8);

                    break;

                case 303:

                    location.characters.Remove(companions[9]);

                    companions.Remove(9);

                    voices.Remove(9);

                    break;

                case 800:
                case 801:
                case 802:
                case 803:
                case 804:
                case 805:
                case 806:
                case 807:
                case 808:
                case 809:
                case 810:

                    int ghostIndex = index - 800;

                    SpellHandle ghostDeath = new(ghostSummons[ghostIndex].Position, 256, IconData.impacts.deathbomb, new());

                    ghostDeath.sound = SpellHandle.sounds.shadowDie;

                    Mod.instance.spellRegister.Add(ghostDeath);

                    location.characters.Remove(ghostSummons[ghostIndex]);

                    ghostSummons.Remove(ghostIndex);

                    batSummons[ghostIndex].TargetEvent(900 + ghostIndex, ghostStarts[ghostIndex]*64);

                    batSummons[ghostIndex].netMovement.Set((int)Character.Character.movements.run);

                    break;

                case 900:
                case 901:
                case 902:
                case 903:
                case 904:
                case 905:
                case 906:
                case 907:
                case 908:
                case 909:
                case 910:

                    int batIndex = index - 900;

                    location.characters.Remove(batSummons[batIndex]);

                    batSummons.Remove(batIndex);

                    break;

                case 401:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position, true);

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_tomb_name], companions[1], eventVectors[405] * 64, false);

                    companions[1].LookAtTarget(companions[4].Position, true);

                    break;

                case 402:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[0].Position, true);

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_tomb_name], companions[0], eventVectors[406] * 64, false);

                    companions[0].LookAtTarget(companions[4].Position, true);

                    break;

                case 403:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[3].Position, true);

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_tomb_name], companions[3], eventVectors[407] * 64, false);

                    companions[3].LookAtTarget(companions[4].Position, true);

                    break;

                case 450:

                    companions[11].ResetActives();

                    companions[11].LookAtTarget(Game1.player.Position, true);

                    companions[0].LookAtTarget(companions[11].Position, true);

                    companions[1].LookAtTarget(companions[11].Position, true);

                    companions[3].LookAtTarget(companions[11].Position, true);

                    break;

                case 451:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(companions[11].Position, true);

                    break;

                case 452:

                    companions[1].ResetActives();

                    companions[1].LookAtTarget(companions[11].Position, true);

                    break;

                case 453:

                    companions[3].ResetActives();

                    companions[3].LookAtTarget(companions[11].Position, true);

                    break;

                case 501:
                case 502:
                case 503:
                case 504:
                case 505:
                case 506:
                case 507:
                case 508:

                    if (companions[3].fadeOut > 0)
                    {

                        companions[3].fadeOut -= 0.124f;

                        if (companions[3].fadeOut <= 0.1f)
                        {

                            companions[3].fadeOut = 0.01f;

                        }

                    }

                    break;

                case 510:
                case 511:
                case 512:
                case 513:
                case 514:
                case 515:
                case 516:
                case 517:

                    if (companions[11].fadeOut > 0)
                    {

                        companions[11].fadeOut -= 0.124f;

                        if (companions[11].fadeOut <= 0.1f)
                        {

                            companions[11].fadeOut = 0.01f;

                        }

                    }

                    break;

                case 509:

                    if (companions[3].fadeOut > 0)
                    {

                        companions[3].fadeOut -= 0.124f;

                        if (companions[3].fadeOut <= 0.1f)
                        {

                            companions[3].fadeOut = 0.01f;

                        }

                    }

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_chapel_name], companions[3], eventVectors[110] * 64, false);

                    companions.Remove(3);

                    voices.Remove(3);

                    break;

                case 518:
                    if (companions[11].fadeOut > 0)
                    {

                        companions[11].fadeOut -= 0.125f;


                    }
                    location.characters.Remove(companions[11]);

                    companions.Remove(11);

                    voices.Remove(11);

                    break;

            }

        }

    }

}