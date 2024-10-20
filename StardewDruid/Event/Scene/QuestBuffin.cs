using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Bones;
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
using System.Linq;
using System.Runtime;
using static StardewValley.Minigames.TargetGame;


namespace StardewDruid.Event.Scene
{
    public class QuestBuffin : EventHandle
    {

        public Dictionary<int, Vector2> eventVectors = new()
        {

            // LAIR VECTORS
            // Buffin Enter
            [1] = new Vector2(31, 20),
            // Buffin walk point 1
            [2] = new Vector2(34, 17),
            // Buffin walk point 2
            [3] = new Vector2(29, 15),

            // Raven Spot
            [103] = new Vector2(23, 9),
            // Crow Spot
            [106] = new Vector2(25, 8),
            // Magpie Spot
            [109] = new Vector2(33, 8),
            // Rook Spot
            [112] = new Vector2(36, 8),
            // Buffin Search
            [115] = new Vector2(40, 16),
            // Buffin Return
            [120] = new Vector2(34, 17),

            // COURT VECTORS
            // Argyle sit
            [201] = new Vector2(31, 18),
            // Jester sit
            [202] = new Vector2(33, 16),
            // Macarbbi sit
            [203] = new Vector2(35, 17),
            // Buffin sit
            [204] = new Vector2(37, 17),
            // Blackfeather start
            [205] = new Vector2(29, 20),
            // Effigy start
            [206] = new Vector2(33, 23),
            // Shadowtin start
            [207] = new Vector2(39, 21),
            // player start
            [208] = new Vector2(29, 22),

            // Candle 1
            [211] = new Vector2(33, 18),
            // Candle 2
            [212] = new Vector2(36, 18),
            // Candle 3
            [213] = new Vector2(32, 20),
            // Candle 4
            [214] = new Vector2(33, 22),
            // Candle 5
            [215] = new Vector2(36, 22),
            // Candle 6
            [216] = new Vector2(37, 20),

            // skull position 1
            [217] = new Vector2(34, 19),
            // skull position 2
            [218] = new Vector2(34, 21),
            // skull position 3
            [219] = new Vector2(35, 21),
            // skull left
            [220] = new Vector2(34, 20),
            // skull right
            [221] = new Vector2(35, 20),

            // Carnevellion summon
            [319] = new Vector2(33, 19),
            // hound to left
            [320] = new Vector2(29, 18),
            // hound down
            [323] = new Vector2(34, 23),
            // hound to right
            [333] = new Vector2(40, 19),
            // hound to left
            [341] = new Vector2(29, 20),
            // hound to middle
            [348] = new Vector2(34, 20),
            // hound leaves
            [358] = new Vector2(34, 28),

            // Bear enters
            [409] = new Vector2(30, 28),

            // Linus joins the circle
            [502] = new Vector2(29, 19),
            // Shadowtin ponders
            [524] = new Vector2(39, 18),
            // Companions exit cavern
            [555] = new Vector2(28, 30),
            // Cabby leaves
            [568] = new Vector2(0, 12),
            // Buffin stands by monument
            [570] = new Vector2(40, 15),

            // Buffin looks at monument
            [601] = new Vector2(45, 11),

        };

        public QuestBuffin()
        {

            mainEvent = true;

            activeLimit = -1;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            locales = new()
            {
                LocationData.druid_lair_name,
                LocationData.druid_court_name,

            };

            origin = eventVectors[1] * 64;

            location.playSound("discoverMineral");

            Mod.instance.iconData.DecorativeIndicator(location, Game1.player.Position, IconData.decorations.bones, 3f, new());

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

            // Introduction (lair) // Talk to Buffin
            if (activeCounter < 100)
            {

                ScenePartOne();

                return;

            }

            // Unearth remains of Carnevellion (lair) // Talk to Buffin
            if (activeCounter < 200)
            {

                ScenePartTwo();

                return;

            }

            // Prepare Rite of Fates // Talk to Buffin
            if (activeCounter < 300)
            {

                ScenePartThree();

                return;

            }

            // Summon Carnevellion // Talk to Carnevellion
            if (activeCounter < 400)
            {

                ScenePartFour();

                return;

            }

            // Fight Ghost Bear / Wolf // Talk to Linus
            if (activeCounter < 500)
            {

                ScenePartFive();

                return;

            }

            // Doja revealled as second heir to Rite of Bones // Talk to Buffin
            if (activeCounter < 600)
            {

                ScenePartSix();

                return;

            }

            // Concluding statement
            if (activeCounter < 700)
            {

                ScenePartSeven();

                return;

            }


            eventComplete = true;

        }

        public void ScenePartOne()
        {

            // ========================== Introduction

            switch (activeCounter)
            {

                case 1:

                    Game1.stopMusicTrack(MusicContext.Default);

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Buffin] as StardewDruid.Character.Buffin;

                    voices[0] = companions[0];

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[0], eventVectors[1] * 64);

                    companions[0].netDirection.Set(0);

                    companions[0].eventName = eventId;

                    DialogueCue(1);

                    break;

                case 4:

                    companions[0].ResetActives();

                    companions[0].TargetEvent(0, eventVectors[2] * 64, true);

                    DialogueCue(4);

                    break;

                case 7:

                    companions[0].ResetActives();

                    companions[0].TargetEvent(0, eventVectors[3] * 64, true);

                    DialogueCue(7);

                    break;

                case 11:

                    companions[0].ResetActives();

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    DialogueCue(11);

                    break;

                case 15:

                    companions[0].LookAtTarget(Game1.player.Position);

                    DialogueLoad(companions[0], 1);

                    break;

                case 21:

                    DialogueClear(companions[0]);

                    activeCounter = 100;

                    break;

            }


        }

        public void ScenePartTwo()
        {

            // ========================== Scavenger Hunt

            switch (activeCounter)
            {

                case 101:

                    Cast.Bones.Corvids.SummonRaven();

                    Mod.instance.trackers[CharacterHandle.characters.Raven].eventLock = true;

                    //companions[1] = Mod.instance.characters[CharacterHandle.characters.Raven];

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 103:

                    companions[0].ResetActives(true);

                    companions[0].LookAtTarget(eventVectors[103] * 64, true);

                    DialogueCue(103);

                    //companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    //companions[1].ResetActives(true);

                    Mod.instance.characters[CharacterHandle.characters.Raven].doEmote(20);

                    Mod.instance.trackers[CharacterHandle.characters.Raven].linger = 600;

                    Mod.instance.trackers[CharacterHandle.characters.Raven].lingerSpot = eventVectors[103] * 64;

                    //companions[1].TargetEvent(103, eventVectors[103] * 64, true);

                    break;

                case 104:

                    Cast.Bones.Corvids.SummonCrow();

                    //companions[2] = Mod.instance.characters[CharacterHandle.characters.Crow];

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 107:

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueCue(107);

                    Mod.instance.characters[CharacterHandle.characters.Crow].doEmote(20);

                    Mod.instance.trackers[CharacterHandle.characters.Crow].linger = 600;

                    Mod.instance.trackers[CharacterHandle.characters.Crow].lingerSpot = eventVectors[106] * 64;

                    //companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    //companions[2].ResetActives(true);

                    //companions[2].doEmote(20);

                    //companions[2].TargetEvent(106, eventVectors[106] * 64, true);

                    break;

                case 108:

                    Cast.Bones.Corvids.SummonMagpie();

                    //companions[3] = Mod.instance.characters[CharacterHandle.characters.Magpie];

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 111:

                    companions[0].LookAtTarget(eventVectors[109] * 64, true);

                    DialogueCue(111);

                    Mod.instance.characters[CharacterHandle.characters.Magpie].doEmote(20);

                    Mod.instance.trackers[CharacterHandle.characters.Magpie].linger = 600;

                    Mod.instance.trackers[CharacterHandle.characters.Magpie].lingerSpot = eventVectors[109] * 64;

                    //companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    //companions[3].ResetActives(true);

                    //companions[3].doEmote(20);

                    //companions[3].TargetEvent(109, eventVectors[109] * 64, true);

                    // Raven

                    //companions[1].ResetActives();

                    // companions[1].netIdle.Set((int)Character.Character.idles.standby);

                    break;

                case 112:

                    Cast.Bones.Corvids.SummonRook();

                    //companions[4] = Mod.instance.characters[CharacterHandle.characters.Rook];

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 115:

                    companions[0].LookAtTarget(eventVectors[112] * 64, true);

                    DialogueCue(115);

                    Mod.instance.characters[CharacterHandle.characters.Rook].doEmote(20);

                    Mod.instance.trackers[CharacterHandle.characters.Rook].linger = 600;

                    Mod.instance.trackers[CharacterHandle.characters.Rook].lingerSpot = eventVectors[112] * 64;

                    //companions[4].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    //companions[4].ResetActives(true);

                    //companions[4].doEmote(20);

                    //companions[4].TargetEvent(112, eventVectors[112] * 64, true);

                    // Crow

                    //companions[2].ResetActives();

                    //companions[2].netIdle.Set((int)Character.Character.idles.standby);

                    break;

                case 118:

                    companions[0].TargetEvent(0, eventVectors[115] * 64, true);

                    break;

                case 119:

                    DialogueCue(119);

                    break;

                case 122:

                    DialogueCue(122);

                    break;

                case 123:

                    /*companions[4].netIdle.Set((int)Character.Character.idles.standby);

                    companions[3].ResetActives();

                    companions[1].doEmote(16);
                    companions[2].doEmote(16);
                    companions[3].doEmote(32);
                    companions[4].doEmote(16);

                    companions[1].SwitchToMode(Character.Character.mode.track, Game1.player);
                    companions[2].SwitchToMode(Character.Character.mode.track, Game1.player);
                    companions[3].SwitchToMode(Character.Character.mode.track, Game1.player);
                    companions[4].SwitchToMode(Character.Character.mode.track, Game1.player);

                    ThrowHandle throwSkull = new(Game1.player, companions[3].Position, IconData.relics.skull_fox);

                    throwSkull.register();*/

                    List<CharacterHandle.characters> corvids = new()
                    {
                        CharacterHandle.characters.Raven,
                        CharacterHandle.characters.Crow,
                        CharacterHandle.characters.Rook,
                        CharacterHandle.characters.Magpie,
                    };

                    foreach (CharacterHandle.characters corvid in corvids)
                    {

                        if (Mod.instance.characters.ContainsKey(corvid))
                        {

                            Mod.instance.characters[corvid].ResetActives();

                            Mod.instance.trackers[corvid].linger = 0;

                            Mod.instance.trackers[corvid].lingerSpot = Vector2.Zero;

                            Mod.instance.characters[corvid].doEmote(16);

                        }

                    }

                    Mod.instance.characters[CharacterHandle.characters.Magpie].doEmote(32);

                    ThrowHandle throwSkull = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Magpie].Position, IconData.relics.skull_fox);

                    throwSkull.register();

                    break;

                case 124:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(Game1.player.Position);

                    companions[0].TargetEvent(0, eventVectors[120] * 64);

                    break;

                case 125:

                    DialogueCue(125);

                    break;

                case 127:

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    break;

                case 128:

                    DialogueLoad(companions[0], 2);

                    break;

                case 136:

                    DialogueClear(companions[0]);

                    activeCounter = 200;

                    break;

            }

        }

        public void SummonFriends()
        {

            // add Argyle

            companions[1] = new Character.Flyer(CharacterHandle.characters.Raven);

            companions[1].setScale = 3.5f;

            companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

            companions[1].ResetActives();

            companions[1].eventName = eventId;

            CharacterMover.Warp(location, companions[1], eventVectors[201] * 64, false);

            companions[1].netIdle.Set((int)Character.Character.idles.standby);

            // add Jester

            companions[2] = Mod.instance.characters[CharacterHandle.characters.Jester] as StardewDruid.Character.Jester;

            voices[2] = companions[2];

            companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

            CharacterMover.Warp(location, companions[2], eventVectors[202] * 64);

            companions[2].ResetActives();

            companions[2].netDirection.Set(2);

            companions[2].eventName = eventId;

            companions[2].netIdle.Set((int)Character.Character.idles.standby);

            // add Macarbbi

            companions[3] = new Flyer(CharacterHandle.characters.Macarbi);

            voices[3] = companions[3];

            companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

            CharacterMover.Warp(location, companions[3], eventVectors[203] * 64);

            companions[3].netDirection.Set(2);

            companions[3].netAlternative.Set(3);

            companions[3].eventName = eventId;

            companions[3].netIdle.Set((int)Character.Character.idles.standby);

            // add Blackfeather

            companions[4] = Mod.instance.characters[CharacterHandle.characters.Blackfeather] as StardewDruid.Character.Blackfeather;

            voices[4] = companions[4];

            companions[4].SwitchToMode(Character.Character.mode.scene, Game1.player);

            CharacterMover.Warp(location, companions[4], eventVectors[205] * 64);

            companions[4].ResetActives();

            companions[4].eventName = eventId;

            companions[4].TargetEvent(0, eventVectors[211] * 64, true);

            // add Effigy

            companions[5] = Mod.instance.characters[CharacterHandle.characters.Effigy] as StardewDruid.Character.Effigy;

            voices[5] = companions[5];

            companions[5].SwitchToMode(Character.Character.mode.scene, Game1.player);

            CharacterMover.Warp(location, companions[5], eventVectors[206] * 64);

            companions[5].ResetActives();

            companions[5].eventName = eventId;

            companions[5].TargetEvent(0, eventVectors[213] * 64, true);

            // add Shadowtin

            companions[6] = Mod.instance.characters[CharacterHandle.characters.Shadowtin] as StardewDruid.Character.Shadowtin;

            voices[6] = companions[6];

            companions[6].SwitchToMode(Character.Character.mode.scene, Game1.player);

            CharacterMover.Warp(location, companions[6], eventVectors[207] * 64);

            companions[6].ResetActives();

            companions[6].eventName = eventId;

            companions[6].TargetEvent(0, eventVectors[215] * 64, true);

        }

        public void ScenePartThree()
        {

            // ========================== Prepare Ritual

            switch (activeCounter)
            {

                case 201:

                    companions[0].TargetEvent(0, eventVectors[1] * 64, true);

                    break;

                case 202:

                    Mod.instance.trackers[CharacterHandle.characters.Raven].eventLock = false;

                    Cast.Bones.Corvids.RemoveCorvids();

                    break;

                case 203:

                    Game1.stopMusicTrack(MusicContext.Default);

                    // move Farmer

                    location = Mod.instance.locations[LocationData.druid_court_name];

                    (location as Court).circleActive = true;

                    Game1.warpFarmer(location.Name, (int)eventVectors[208].X, (int)eventVectors[208].Y, 1);

                    // move Buffin

                    CharacterMover.Warp(location, companions[0], eventVectors[204] * 64, false);

                    companions[0].ResetActives();

                    companions[0].netDirection.Set(2);

                    companions[0].netAlternative.Set(3);

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    SummonFriends();

                    break;

                case 204:

                    DialogueCue(204);

                    break;

                case 205:

                    // kneel for candles
                    companions[4].ResetActives();

                    companions[4].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[5].ResetActives();

                    companions[5].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[6].ResetActives();

                    companions[6].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                case 206:

                    DialogueCue(206);

                    break;

                case 207:

                    // kneel candles
                    StardewValley.Object candleOne = new Torch();

                    location.objects.TryAdd(eventVectors[211], candleOne);

                    candleOne.performDropDownAction(Game1.player);

                    StardewValley.Object candleTwo = new Torch();

                    location.objects.TryAdd(eventVectors[213], candleTwo);

                    candleTwo.performDropDownAction(Game1.player);

                    StardewValley.Object candleThree = new Torch();

                    location.objects.TryAdd(eventVectors[215], candleThree);

                    candleThree.performDropDownAction(Game1.player);

                    break;

                case 208:

                    DialogueCue(208);

                    companions[4].ResetActives();

                    companions[5].ResetActives();

                    companions[6].ResetActives();

                    break;

                case 209:

                    companions[4].TargetEvent(0, eventVectors[212] * 64, true);

                    companions[5].TargetEvent(0, eventVectors[214] * 64, true);

                    companions[6].TargetEvent(0, eventVectors[216] * 64, true);

                    break;

                case 210:

                    DialogueCue(210);

                    break;

                case 211:

                    // kneel for candles
                    companions[4].ResetActives();

                    companions[4].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[5].ResetActives();

                    companions[5].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[6].ResetActives();

                    companions[6].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                case 212:

                    DialogueCue(212);

                    break;

                case 213:

                    // kneel candles
                    StardewValley.Object candleFour = new Torch();

                    location.objects.TryAdd(eventVectors[212],candleFour);

                    candleFour.performDropDownAction(Game1.player);

                    StardewValley.Object candleFive = new Torch();

                    location.objects.TryAdd(eventVectors[214], candleFive);

                    candleFive.performDropDownAction(Game1.player);

                    StardewValley.Object candleSix = new Torch();

                    location.objects.TryAdd(eventVectors[216], candleSix);

                    candleSix.performDropDownAction(Game1.player);

                    break;

                case 214:

                    DialogueCue(214);

                    companions[4].ResetActives();

                    companions[5].ResetActives();

                    companions[6].ResetActives();

                    break;

                case 215:

                    companions[4].TargetEvent(0, eventVectors[217] * 64, true);

                    companions[5].TargetEvent(0, eventVectors[218] * 64, true);

                    companions[6].TargetEvent(0, eventVectors[219] * 64, true);

                    break;

                case 216:

                    DialogueCue(216);

                    break;

                case 217:

                    // kneel for skulls
                    companions[4].ResetActives();

                    companions[4].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[5].ResetActives();

                    companions[5].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[6].ResetActives();

                    companions[6].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                case 218:

                    eventRenders.Add(new("skull_saurus", location.Name, eventVectors[220] * 64 - new Vector2(0,32), IconData.relics.skull_saurus));

                    eventRenders.Add(new("skull_gelatin", location.Name, eventVectors[220] * 64 + new Vector2(0, 32), IconData.relics.skull_gelatin));

                    eventRenders.Add(new("skull_cannoli", location.Name, eventVectors[221] * 64 - new Vector2(0, 32), IconData.relics.skull_cannoli));

                    eventRenders.Add(new("skull_fox", location.Name, eventVectors[221] * 64 + new Vector2(0, 32), IconData.relics.skull_fox));

                    DialogueCue(218);

                    break;

                case 219:

                    companions[4].ResetActives();

                    companions[5].ResetActives();

                    companions[6].ResetActives();

                    break;

                case 220:

                    DialogueCue(220);

                    companions[4].TargetEvent(0, eventVectors[205] * 64, true);

                    companions[5].TargetEvent(0, eventVectors[206] * 64, true);

                    companions[6].TargetEvent(0, eventVectors[207] * 64, true);

                    break;

                case 222:

                    companions[4].ResetActives();

                    companions[4].LookAtTarget(eventVectors[220] * 64, true);

                    companions[5].ResetActives();

                    companions[5].LookAtTarget(eventVectors[220] * 64, true);

                    companions[6].ResetActives();

                    companions[6].LookAtTarget(eventVectors[220] * 64, true);

                    DialogueCue(222);

                    break;

                case 224:

                    DialogueLoad(companions[0], 3);

                    DialogueCue(224);

                    break;

                case 226:

                    DialogueCue(226);

                    break;

                case 232:

                    DialogueClear(companions[0]);

                    activeCounter = 300;

                    break;

            }

        }

        public void ScenePartFour()
        {

            // ========================== Summoning Carnivellion

            switch (activeCounter)
            {

                case 302:

                    companions[0].LookAtTarget(eventVectors[220] * 64);

                    companions[0].netAlternative.Set(3);

                    DialogueCue(302);

                    SpellHandle circleHandle = new(eventVectors[220] * 64 + new Vector2(32, 0), 256, IconData.impacts.summoning, new());

                    circleHandle.scheme = IconData.schemes.fates;

                    circleHandle.sound = SpellHandle.sounds.shadowDie;

                    Mod.instance.spellRegister.Add(circleHandle);

                    break;

                case 305:

                    DialogueCue(305);

                    SpellHandle circleHandleTwo = new(eventVectors[220] * 64 + new Vector2(32,0), 256, IconData.impacts.summoning, new());

                    circleHandleTwo.scheme = IconData.schemes.fates;

                    circleHandleTwo.sound = SpellHandle.sounds.shadowDie;

                    Mod.instance.spellRegister.Add(circleHandleTwo);

                    break;

                case 308:

                    DialogueCue(308);

                    location.playSound(SpellHandle.sounds.ghost.ToString());

                    break;

                case 310:

                    DialogueCue(311);

                    SpellHandle circleHandleThree = new(eventVectors[220] * 64 + new Vector2(32, 0), 256, IconData.impacts.summoning, new());

                    circleHandleThree.scheme = IconData.schemes.fates;

                    circleHandleThree.sound = SpellHandle.sounds.shadowDie;

                    Mod.instance.spellRegister.Add(circleHandleThree);

                    break;

                case 313:

                    Game1.flashAlpha = 1;

                    (location as Court).ambientDarkness = true;

                    for(int i = 1; i < 7; i++)
                    {
                        
                        if (Mod.instance.characters.ContainsKey(companions[i].characterType))
                        {

                            companions[i].SwitchToMode(Character.Character.mode.home, Game1.player);

                            companions[i].eventName = null;

                        }
                        else
                        {

                            location.characters.Remove(companions[i]);

                        }

                    }

                    SetTrack("spirits_eve");

                    break;

                case 314:

                    //Game1.flashAlpha = 1;

                    StardewDruid.Monster.Dinosaur dinosaur = new(eventVectors[211], Mod.instance.CombatDifficulty());

                    dinosaur.netScheme.Set(2);

                    dinosaur.SetMode(3);

                    dinosaur.netPosturing.Set(true);

                    location.characters.Add(dinosaur);

                    dinosaur.dragonRender.GhostScheme();

                    dinosaur.LookAtFarmer();

                    dinosaur.update(Game1.currentGameTime, location);

                    bosses[0] = dinosaur;

                    eventRenders.Remove(eventRenders.First());

                    SpellHandle smokeOne = new(eventVectors[220] * 64 + new Vector2(32, 0), 256, IconData.impacts.smoke, new());

                    smokeOne.sound = SpellHandle.sounds.flameSpellHit;

                    Mod.instance.spellRegister.Add(smokeOne);

                    break;

                case 315:

                    //Game1.flashAlpha = 1;

                    Blobfiend blobking = new Blobfiend(eventVectors[212], Mod.instance.CombatDifficulty());

                    blobking.netScheme.Set(2);

                    blobking.SetMode(3);

                    blobking.netPosturing.Set(true);

                    blobking.LookAtFarmer();

                    location.characters.Add(blobking);

                    blobking.update(Game1.currentGameTime, location);

                    bosses[1] = blobking;

                    companions[0].doEmote(16);

                    eventRenders.Remove(eventRenders.First());

                    SpellHandle smokeTwo = new(eventVectors[221] * 64 + new Vector2(32, 0), 256, IconData.impacts.smoke, new());

                    smokeTwo.sound = SpellHandle.sounds.flameSpellHit;

                    Mod.instance.spellRegister.Add(smokeTwo);

                    break;

                case 316:

                    //Game1.flashAlpha = 1;

                    Dustfiend dustking = new Dustfiend(eventVectors[215], Mod.instance.CombatDifficulty());

                    dustking.netScheme.Set(2);

                    dustking.SetMode(3);

                    dustking.netPosturing.Set(true);

                    dustking.LookAtFarmer();

                    location.characters.Add(dustking);

                    dustking.update(Game1.currentGameTime, location);

                    bosses[2] = dustking;

                    eventRenders.Remove(eventRenders.First());

                    SpellHandle smokeThree = new(eventVectors[221] * 64 + new Vector2(32, 0), 256, IconData.impacts.smoke, new());

                    smokeThree.sound = SpellHandle.sounds.flameSpellHit;

                    Mod.instance.spellRegister.Add(smokeThree);
                    break;

                case 317:

                    //Game1.flashAlpha = 1;

                    StardewDruid.Monster.Dragon dragon = new(eventVectors[214], Mod.instance.CombatDifficulty());

                    dragon.netScheme.Set(2);

                    dragon.SetMode(3);

                    dragon.netPosturing.Set(true);

                    location.characters.Add(dragon);

                    dragon.dragonRender.GhostScheme();

                    dragon.LookAtFarmer();

                    dragon.update(Game1.currentGameTime, location);

                    bosses[3] = dragon;

                    eventRenders.Remove(eventRenders.First());

                    SpellHandle smokeFour = new(eventVectors[220] * 64 + new Vector2(32, 0), 256, IconData.impacts.smoke, new());

                    smokeFour.sound = SpellHandle.sounds.flameSpellHit;

                    Mod.instance.spellRegister.Add(smokeFour);

                    DialogueCue(317);

                    break;

                case 318:

                    RemoveMonsters();

                    eventRenders.Clear();

                    for(int i = 211; i < 222; i++)
                    {

                        SpellHandle deathPlume = new(eventVectors[i] * 64, 192, IconData.impacts.plume, new());

                        deathPlume.scheme = IconData.schemes.fates;

                        Mod.instance.spellRegister.Add(deathPlume);

                    }

                    location.playSound(SpellHandle.sounds.shadowDie.ToString());

                    break;

                case 319:

                    companions[7] = new Barker(CharacterHandle.characters.Carnivellion);

                    companions[7].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[7].setScale = 4.5f;

                    companions[7].fadeOut = 0.7f;

                    companions[7].Position = eventVectors[319] * 64;

                    companions[7].currentLocation = location;

                    location.characters.Add(companions[7]);

                    voices[7] = companions[7];

                    companions[7].TargetEvent(0, eventVectors[320] * 64);
                    
                    break;

                case 320:

                    companions[0].ResetActives();

                    DialogueCue(320);

                    break;

                case 323:

                    companions[7].TargetEvent(0, eventVectors[323] * 64);

                    DialogueCue(323);

                    break;//] = new() { [0] = "You smell awful, ancestor", },

                case 326:

                    companions[7].LookAtTarget(companions[0].Position, true);

                    DialogueCue(326);

                    break;//] = new() { [7] = "Ah yes... the sassiness that distinguishes my lineage.", },

                case 330:

                    companions[0].LookAtTarget(companions[7].Position, true);

                    DialogueCue(330);

                    break;//] = new() { [0] = "So what happened?", },

                case 333:

                    companions[0].LookAtTarget(companions[7].Position, true);

                    companions[7].ResetActives(true);

                    companions[7].TargetEvent(0, eventVectors[333] * 64);

                    DialogueCue(333);

                    break;//] = new() { [7] = "Knight Wyrven, in his insanity, torched the valley in dragonform." },

                case 335:

                    companions[7].LookAtTarget(companions[0].Position, true);

                    break;

                case 337:

                    companions[0].LookAtTarget(companions[7].Position, true);

                    DialogueCue(337);

                    break;//] = new() { [7] = "I confronted him, and after a vicious battle, we both perished.", },

                case 341:

                    companions[0].LookAtTarget(companions[7].Position, true);

                    companions[7].TargetEvent(0, eventVectors[341] * 64);

                    DialogueCue(341);

                    break;//] = new() { [0] = "What... you lost?", },

                case 344:

                    companions[7].LookAtTarget(companions[0].Position, true);

                    companions[0].LookAtTarget(companions[7].Position, true);

                    DialogueCue(344);

                    break;//] = new() { [7] = "I never realised those cretinous chefs had laid a trap.", },

                case 348:

                    companions[0].LookAtTarget(companions[7].Position, true);

                    companions[7].TargetEvent(0, eventVectors[348] * 64);

                    DialogueCue(348);

                    break;//] = new() { [7] = "Both my soul and the dragonshifter's were ensnared in this stone.", },

                case 352:

                    companions[7].LookAtTarget(companions[0].Position, true);

                    DialogueCue(352);

                    break;//] = new() { [7] = "Thank you child, for my liberation.", },

                case 355:

                    DialogueCue(355);

                    ThrowHandle throwMap = new(companions[7].Position, companions[0].Position, IconData.relics.book_chart);

                    throwMap.register();

                    break;//] = new() { [7] = "Take this final prophecy.", },

                case 358:

                    companions[7].TargetEvent(0, eventVectors[358] * 64);

                    DialogueCue(358);

                    break;//] = new() { [7] = "The Successor of the Wild draws near.", },

                case 360:

                    location.characters.Remove(companions[7]);

                    companions.Remove(7);
                    
                    break;

                case 361:

                    DialogueCue(361);

                    break;//] = new() { [0] = "Farewell ancestor...", },

                case 364:

                    DialogueCue(364);

                    break;

                case 365:

                    DialogueLoad(companions[0], 4);

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    break;

                case 371:

                    DialogueClear(companions[0]);

                    activeCounter = 400;

                    break;

            }

        }

        public void ScenePartFive()
        {

            // =======================  Bear Duel

            if (bosses.ContainsKey(0))
            {

                if (bosses[0].netWoundedActive.Value)
                {

                    if (activeCounter < 449)
                    {

                        activeCounter = 449;

                        return;

                    }

                }
                else if (!ModUtility.MonsterVitals(bosses[0], location))
                {

                    bosses[0].netWoundedActive.Set(true);

                    bosses[0].Health = bosses[0].MaxHealth;

                    activeCounter = 449;

                    return;

                }

            }

            switch (activeCounter)
            {

                case 401:

                    StopTrack(); break;

                case 402:

                    DialogueCue(402);

                    break;

                case 403:

                    location.playSound("BearGrowl");

                    break;

                case 405:

                    DialogueCue(405);

                    break;

                case 406:

                    location.playSound("BearGrowlTwo");

                    break;

                case 408:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[0].Position, true);

                    companions[0].SwitchToMode(Character.Character.mode.random,Game1.player);

                    location.characters.Remove(companions[0]);

                    companions[0].Position = CharacterHandle.CharacterStart(CharacterHandle.locations.grove, CharacterHandle.characters.Effigy);

                    companions[0].currentLocation = Mod.instance.locations[LocationData.druid_clearing_name];

                    Mod.instance.locations[LocationData.druid_clearing_name].characters.Add(companions[0]);

                    break;

                case 409:

                    location.playSound("BearGrowlThree");

                    bosses[0] = new ShadowBear(ModUtility.PositionToTile(eventVectors[409] * 64), Mod.instance.CombatDifficulty());

                    bosses[0].netScheme.Set(2);

                    bosses[0].SetMode(4);

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    bosses[0].ResetActives();

                    bosses[0].PerformFollow(Game1.player.Position);

                    bosses[0].setWounded = true;

                    voices[8] = bosses[0];

                    EventDisplay bossBar = BossBar(0, 8);

                    bossBar.colour = Microsoft.Xna.Framework.Color.Brown;

                    SetTrack("cowboy_boss");

                    break;

                case 417:

                    location.playSound("WolfGrowl");

                    bosses[1] = new ShadowWolf(ModUtility.PositionToTile(eventVectors[409] * 64), Mod.instance.CombatDifficulty());

                    bosses[1].netScheme.Set(2);

                    bosses[1].SetMode(2);

                    location.characters.Add(bosses[1]);

                    bosses[1].update(Game1.currentGameTime, location);

                    bosses[1].ResetActives();

                    bosses[1].PerformFollow(Game1.player.Position);

                    break;

                case 425:

                    location.playSound("WolfGrowl");

                    bosses[2] = new ShadowWolf(ModUtility.PositionToTile(eventVectors[409] * 64), Mod.instance.CombatDifficulty(),"BlackWolf");

                    bosses[2].netScheme.Set(2);

                    bosses[2].SetMode(2);

                    location.characters.Add(bosses[2]);

                    bosses[2].update(Game1.currentGameTime, location);

                    bosses[2].ResetActives();

                    bosses[2].PerformFollow(Game1.player.Position);

                    break;

                /*case 433:

                    location.playSound("WolfGrowl");

                    bosses[3] = new ShadowWolf(ModUtility.PositionToTile(eventVectors[409] * 64), Mod.instance.CombatDifficulty());

                    bosses[3].netScheme.Set(2);

                    bosses[3].SetMode(2);

                    location.characters.Add(bosses[3]);

                    bosses[3].update(Game1.currentGameTime, location);

                    bosses[3].ResetActives();

                    bosses[3].PerformFollow(Game1.player.Position);

                    break;*/

                case 450:

                    foreach(KeyValuePair<int,Boss> boss in bosses)
                    {
                        
                        boss.Value.ResetActives();

                        boss.Value.LookAtFarmer();

                        boss.Value.netPosturing.Set(true);

                    }

                    DialogueCue(450);

                    break;

                case 453:

                    StopTrack();

                    Vector2 bearPosition = bosses[0].Position;

                    RemoveMonsters();

                    RemoveSummons();

                    // reset locations

                    Game1.flashAlpha = 1;

                    Game1.stopMusicTrack(MusicContext.Default);

                    (location as Court).ambientDarkness = false;

                    // return friends

                    SummonFriends();

                    // return Buffin

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Buffin] as StardewDruid.Character.Buffin;

                    voices[0] = companions[0];

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[0], eventVectors[204] * 64);

                    companions[0].Position = eventVectors[204] * 64;

                    companions[0].eventName = eventId;

                    // return to idle states
                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    // Linus

                    companions[9] = new Linus(CharacterHandle.characters.Linus);

                    voices[9] = companions[9];

                    companions[9].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[9], bearPosition);

                    companions[9].LookAtTarget(Game1.player.Position, true);

                    companions[9].eventName = eventId;

                    for (int i = 0; i < 7; i++)
                    {

                        companions[i].LookAtTarget(bearPosition, true);

                    }

                    break;

                case 455:

                    DialogueCue(455);

                    break;

                case 456:

                    DialogueLoad(companions[9], 5);

                    break;

                case 458:

                    DialogueCue(458);

                    break;

                case 461:

                    DialogueCue(461);

                    break;

                case 464:

                    DialogueCue(464);

                    break;

                case 470:

                    DialogueClear(companions[9]);

                    activeCounter = 500;

                    break;
            }

        }

        public void ScenePartSix()
        {


            switch (activeCounter)
            {


                case 502:

                    DialogueCue(502);

                    companions[9].ResetActives();

                    companions[9].TargetEvent(0, eventVectors[502] * 64, true);

                    break;// = new() { [9] = "Has the Successor of Elements revealed themselves?", },

                case 503:
                case 504:
                case 505:

                    for (int i = 0; i < 7; i++)
                    {

                        companions[i].LookAtTarget(companions[9].Position, true);

                    }

                    break;

                case 506:

                    companions[2].ResetActives(true);

                    DialogueCue(506);

                    break;// = new() { [2] = "Well... what kind of animal do they shapeshift into?", },

                case 508:

                    companions[9].LookAtTarget(companions[2].Position, true);
                    break;

                case 510:

                    DialogueCueWithFeeling(510,0,Character.Character.specials.gesture);

                    break;// = new() { [9] = "They will be a practitioner of the arcane arts", },

                case 514:

                    companions[0].ResetActives(true);

                    companions[2].netIdle.Set((int)Character.Character.idles.standby);

                    DialogueCue(514);

                    break;// = new() { [0] = "I think one of those arcanists lives in the forest", },

                case 516:
                    companions[9].LookAtTarget(companions[0].Position, true);
                    break;

                case 518:

                    DialogueCueWithFeeling(518, 0, Character.Character.specials.gesture);

                    break;// = new() { [9] = "Yes, they are a good friend of mine", },
                case 522:

                    DialogueCueWithFeeling(522, 0, Character.Character.specials.gesture);

                    break;// = new() { [9] = "But they are agnostic about the old myths", },

                case 524:

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    companions[6].TargetEvent(0, eventVectors[524] * 64, true);

                    break;

                case 526:

                    for (int i = 0; i < 6; i++)
                    {

                        companions[i].LookAtTarget(companions[6].Position, true);

                    }

                    companions[9].LookAtTarget(companions[6].Position, true);

                    companions[6].ResetActives();

                    companions[6].LookAtTarget(companions[9].Position, true);

                    DialogueCueWithFeeling(526, 0, Character.Character.specials.gesture);

                    break;// = new() { [6] = "I have a theory. My former client is a mage", },

                case 529:

                    DialogueCueWithFeeling(529, 0, Character.Character.specials.gesture);

                    break;// = new() { [6] = "He would often boast about a peculiar amount of esoteric knowledge", },

                case 532:

                    DialogueCueWithFeeling(532, 0, Character.Character.specials.gesture);

                    break;// = new() { [6] = "Especially in regards to rituals with bones or dragons", },

                case 536:

                    for (int i = 0; i < 6; i++)
                    {

                        companions[i].doEmote(16);

                    }

                    DialogueCueWithFeeling(536, 0, Character.Character.specials.gesture);

                    break;// = new() { [6] = "Doja. He's the second Successor.", },
                case 539:

                    DialogueCueWithFeeling(539, 0, Character.Character.specials.point);

                    break;// = new() { [4] = "Can we trust in his cooperation?", },
                case 542:

                    DialogueCueWithFeeling(542, 0, Character.Character.specials.gesture);

                    break;// = new() { [6] = "Still, I will attempt contact through an intermediary", },

                case 546:

                    for (int i = 0; i < 6; i++)
                    {

                        if (i == 3) { continue; }

                        companions[i].LookAtTarget(companions[3].Position, true);

                    }

                    DialogueCue(546);

                    break;// = new() { [3] = "My nose itches. Can we adjourn?", },

                case 550:

                    DialogueCueWithFeeling(550, 0, Character.Character.specials.gesture);

                    break;// = new() { [4] = "I need some herbal tea to settle my nerves.", },

                case 552:

                    for (int i = 0; i < 6; i++)
                    {

                        if (i == 4) { continue; }

                        companions[i].doEmote(32);

                        companions[i].LookAtTarget(companions[4].Position, true);

                    }

                    companions[9].LookAtTarget(companions[4].Position, true);

                    companions[9].doEmote(32);

                    break;

                case 554:

                    DialogueCue(554);

                    break;// = new() { [9] = "That sounds like a fabulous idea", },

                case 555:

                    companions[1].TargetEvent(501, eventVectors[555] * 64, true);

                    companions[4].TargetEvent(502, eventVectors[555] * 64, true);

                    companions[5].TargetEvent(503, eventVectors[555] * 64, true);

                    companions[6].TargetEvent(504, eventVectors[555] * 64, true);

                    companions[9].TargetEvent(505, eventVectors[555] * 64, true);

                    break;

                case 557:

                    companions[3].LookAtTarget(companions[2].Position, true);

                    companions[2].LookAtTarget(companions[3].Position, true);

                    companions[2].ResetActives();

                    DialogueCue(557);

                    break;// = new() { [3] = "I enjoyed playing a part in this little drama.", },

                case 560:

                    companions[3].LookAtTarget(companions[0].Position, true);

                    companions[0].LookAtTarget(companions[3].Position, true);

                    companions[0].ResetActives();

                    DialogueCue(560);

                    break;// = new() { [3] = "I will advise the mortuary to receive your ancestor's spirit.", },

                case 564:

                    DialogueCue(564);

                    break;// = new() { [0] = "Hmmm... yes. I. We, we did it. We found him.", },

                case 567:

                    DialogueCue(567);

                    break;// = new() { [2] = "Bye Cabby, see you soon.", },

                case 568:

                    companions[3].TargetEvent(506, eventVectors[568] * 64, true);

                    break;

                case 569:

                    companions[2].LookAtTarget(companions[3].Position, true);

                    break;

                case 570:

                    companions[0].TargetEvent(0, eventVectors[570] * 64, true);

                    DialogueCue(570);

                    break;// = new() { [0] = "...Ancestor.", },

                case 573:

                    companions[2].LookAtTarget(companions[0].Position, true);

                    companions[2].netIdle.Set((int)Character.Character.idles.standby);

                    DialogueLoad(companions[0], 6);

                    break;

                case 580:

                    DialogueClear(companions[0]);

                    activeCounter = 600;

                    break;


            }

        }

        public void ScenePartSeven()
        {


            switch (activeCounter)
            {
                case 601:

                    companions[0].LookAtTarget(eventVectors[601]*64, true);

                    DialogueCue(601);

                    break;

                case 604:

                    eventComplete = true;
                    break;

            }

        }

        public override void EventScene(int index)
        {

            switch (index)
            {
                case 103:

                    companions[1].ResetActives();

                    companions[1].netSpecial.Set((int)Character.Character.specials.special);

                    companions[1].specialTimer = 600;

                    break;

                case 106:

                    companions[2].ResetActives();

                    companions[2].netSpecial.Set((int)Character.Character.specials.special);

                    companions[2].specialTimer = 600;

                    break;

                case 109:

                    companions[3].ResetActives();

                    companions[3].netSpecial.Set((int)Character.Character.specials.special);

                    companions[3].specialTimer = 600;

                    break;

                case 112:

                    companions[4].ResetActives();

                    companions[4].netSpecial.Set((int)Character.Character.specials.special);

                    companions[4].specialTimer = 600;

                    break;

                // leave scene

                case 501:

                    location.characters.Remove(companions[1]);

                    companions.Remove(1);

                    voices.Remove(1);

                    break;

                case 502:

                    companions[4].SwitchToMode(Character.Character.mode.home,Game1.player);

                    companions.Remove(4);

                    voices.Remove(4);

                    break;

                case 503:

                    companions[5].SwitchToMode(Character.Character.mode.home, Game1.player);

                    companions.Remove(5);

                    voices.Remove(5);

                    break;

                case 504:

                    companions[6].SwitchToMode(Character.Character.mode.home, Game1.player);

                    companions.Remove(6);

                    voices.Remove(6);

                    break;

                case 505:

                    location.characters.Remove(companions[9]);

                    companions.Remove(9);

                    voices.Remove(9);

                    break;

                case 506:

                    location.characters.Remove(companions[3]);

                    companions.Remove(3);

                    voices.Remove(3);

                    break;

            }

        }

    }

}