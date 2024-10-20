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
using StardewValley.BellsAndWhistles;
using StardewValley.Companions;
using StardewValley.GameData;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.SpecialOrders;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace StardewDruid.Event.Scene
{
    public class ChallengeBones : EventHandle
    {

        public Dictionary<int, Vector2> eventVectors = new()
        {

            // ENTER
            // Blackfeather enter
            [1] = new Vector2(26, 38),
            // Wizard enter
            [2] = new Vector2(27, 38),
            // Linus enter
            [3] = new Vector2(28, 38),
            // SETTLE
            // Blackfeather settle
            [4] = new Vector2(25, 27),
            // Wizard settle
            [5] = new Vector2(29, 28),
            // Linus settle
            [6] = new Vector2(30, 27),
            // PACE
            // Wizard 1
            [7] = new Vector2(26, 29),
            // Wizard 2
            [8] = new Vector2(29, 29),

            // Effigy enter
            [101] = new Vector2(28, 38),
            // Effigy settle
            [102] = new Vector2(27, 29),
            // Effigy leave
            [103] = new Vector2(28, 38),

            // Warp farmer grove
            [104] = new Vector2(21, 5),
            // Effigy
            [105] = new Vector2(20, 6),
            // Effigy walk
            [106] = new Vector2(22, 7),
            // Jester
            [107] = new Vector2(29, 12),
            // Jester walk
            [108] = new Vector2(25, 7),
            // Buffin
            [109] = new Vector2(30, 11),
            // Buffin walk
            [110] = new Vector2(25, 6),
            // Shadowtin
            [111] = new Vector2(12, 17),
            // Shadowtin walk
            [112] = new Vector2(11, 13),
            // Shadowtin closer
            [113] = new Vector2(18, 8),
            // Jester leave
            [114] = new Vector2(21, 1),
            // Buffin leave
            [115] = new Vector2(21, 1),
            // Shadowtin leave
            [116] = new Vector2(21, 1),
            // Effigy move
            [117] = new Vector2(26, 12),
            // Effigy move again
            [118] = new Vector2(21, 15),
            // Sighs location
            [119] = new Vector2(20, 7),
            // Rustling location
            [120] = new Vector2(17, 9),
            // Whistling location
            [121] = new Vector2(23, 9),
            // Effigy to tree
            [122] = new Vector2(34, 18),


            // Effigy leave
            [201] = new Vector2(29,18),
            [202] = new Vector2(27,9),

            // Warp farmer gate
            [203] = new Vector2(25, 20),
            // Focus vector
            [204] = new Vector2(27, 21),
            // Boss vector
            [205] = new Vector2(27, 22),

            // Effigy Place
            [210] = new Vector2(28, 27),
            // Blackfeather Place
            [211] = new Vector2(27, 19),
            // Wizard Place
            [212] = new Vector2(23, 23),
            // Linus Place
            [213] = new Vector2(32, 23),
            // Jester
            [214] = new Vector2(32, 19),
            // Buffin Place
            [215] = new Vector2(34, 20),
            // Shadowtin Place
            [216] = new Vector2(21, 20),

            // Leave fight
            [301] = new Vector2(27, 38),
            // Blackfeather turn back
            [302] = new Vector2(28, 32),
            // Blackfeather closer
            [303] = new Vector2(27, 27),

            // Route left
            [304] = new Vector2(24, 24),
            // Route right
            [305] = new Vector2(29, 24),

            // Wizard Grove
            [352] = new Vector2(22, 14),
            // Linus Grove
            [353] = new Vector2(23, 14),
            // Jester Grove
            [354] = new Vector2(20, 15),
            // Buffin Grove
            [355] = new Vector2(21, 14),
            // Shadowtin Grove
            [356] = new Vector2(23, 16),

            // Raven
            [401] = new Vector2(27, 20),
            // Crow
            [402] = new Vector2(28, 21),
            // Magpie
            [403] = new Vector2(26, 21),
            // Rook
            [404] = new Vector2(27, 22),
            // Aldebaran jump off
            [405] = new Vector2(27, 18),


            // Wizard enter
            [410] = new Vector2(27, 38),
            // Wizard reenter
            [412] = new Vector2(23, 26),
            // Linus reenter
            [413] = new Vector2(25, 27),
            // Jester reenter
            [414] = new Vector2(28, 28),
            // Buffin reenter
            [415] = new Vector2(29, 28),
            // Shadowtin reenter
            [416] = new Vector2(31, 26),
 
        };

        public ChallengeBones()
        {

            mainEvent = true;

            activeLimit = -1;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            locales = new()
            {
                LocationData.druid_gate_name,
                LocationData.druid_grove_name,

            };

            origin = eventVectors[1] * 64;

            location.playSound("discoverMineral");

            foreach (KeyValuePair<CharacterHandle.characters, StardewDruid.Character.Character> companion in Mod.instance.characters)
            {

                companion.Value.SwitchToMode(Character.Character.mode.limbo, Game1.player);

            }

        }

        public override void EventRemove()
        {

            foreach (TerrainTile brazierTile in (Mod.instance.locations[LocationData.druid_gate_name] as Gate).brazierTiles)
            {

                brazierTile.index = 11;

                brazierTile.reset();

            }

            foreach (TerrainTile maypoleTile in (Mod.instance.locations[LocationData.druid_gate_name] as Gate).maypoleTiles)
            {

                maypoleTile.index = 15;

                maypoleTile.reset();

            }

            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.BrownBear))
            {

                Mod.instance.characters.Remove(CharacterHandle.characters.BrownBear);

            }

            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Wizard))
            {

                Mod.instance.characters.Remove(CharacterHandle.characters.Wizard);

            }

            base.EventRemove();

            if (eventActive)
            {

                if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Aldebaran))
                {

                    if (!Mod.instance.questHandle.IsComplete(eventId))
                    {

                        CharacterMover mover = new(CharacterHandle.characters.Aldebaran, CharacterMover.moveType.purge);

                        Mod.instance.movers[CharacterHandle.characters.Aldebaran] = mover;

                    }

                }

                foreach (KeyValuePair<CharacterHandle.characters, StardewDruid.Character.Character> companion in Mod.instance.characters)
                {

                    if (companion.Value.modeActive == Character.Character.mode.limbo)
                    {

                        companion.Value.SwitchToMode(Character.Character.mode.home, Game1.player);

                    }

                }

            }

        }

        public override bool AttemptReset()
        {

            Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.abortTomorrow), 3, true);

            return false;

        }

        public override void EventInterval()
        {

            activeCounter++;

            // Introduction (gate) Blackfeather talking to Wizard, Linus, Effigy approaches about missing piece (c)
            if (activeCounter < 100)
            {

                ScenePartOne();

                return;

            }

            // Warp to grove / Jester says goodbye / Buffin, Shadowtin and others walk to gate / Effigy walks around talking / Effigy says goodbye to Weald (c)
            if (activeCounter < 200)
            {

                ScenePartTwo();

                return;

            }

            // Ritual starts, Effigy moves into the brazier, Blackfeather confused (c)
            if (activeCounter < 300)
            {

                ScenePartThree();

                return;

            }

            // Ritual begins / Fire fiend attack / core left in brazier / Blackfeather unsure what to do (c)
            if (activeCounter < 400)
            {

                ScenePartFour();

                return;

            }

            // Ritual completes / Aldebaran emerges (c)
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

            eventComplete = true;

        }

        public void ScenePartOne()
        {

            // ========================== Introduction

            switch (activeCounter)
            {

                case 1:

                    Game1.stopMusicTrack(MusicContext.Default);

                    // Blackfeather

                    companions[1] = Mod.instance.characters[CharacterHandle.characters.Blackfeather] as StardewDruid.Character.Blackfeather;

                    voices[1] = companions[1];

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[1], eventVectors[1] * 64);

                    companions[1].eventName = eventId;

                    companions[1].TargetEvent(0, eventVectors[4] * 64);

                    // Wizard

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Nom0ri.RomRas"))
                    {

                        companions[2] = new Witch(CharacterHandle.characters.Witch);

                    }
                    else
                    {

                        companions[2] = new Wizard(CharacterHandle.characters.Wizard);

                    }

                    voices[2] = companions[2];

                    companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[2], eventVectors[2] * 64);

                    companions[2].eventName = eventId;

                    companions[2].TargetEvent(0, eventVectors[5] * 64);

                    // Linus

                    companions[3] = new Linus(CharacterHandle.characters.Linus);

                    voices[3] = companions[3];

                    companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[3], eventVectors[3] * 64);

                    companions[3].eventName = eventId;

                    companions[3].TargetEvent(0, eventVectors[6] * 64);

                    DialogueCue(1);

                    break;

                    //[1] = new() { [1] = "Welcome to the temple of burning passion", },

                case 4:

                    DialogueCue(4); 

                    break;//[4] = new() { [1] = "It's still remarkable, despite centuries of neglect", },

                case 5:

                    companions[1].LookAtTarget(companions[2].Position, true);

                    break;

                case 7:

                    companions[2].LookAtTarget(companions[1].Position, true);

                    companions[3].LookAtTarget(companions[1].Position, true);

                    DialogueCueWithFeeling(7,0,Character.Character.specials.gesture); 

                    break;//[7] = new() { [1] = "I hope it's enough to host the Great Stream", },

                case 10:

                    DialogueCueWithFeeling(10, 0, Character.Character.specials.gesture);

                    break;//[10] = new() { [3] = "Heh. Chaos might prefer the ruins", },

                case 13:

                    companions[2].TargetEvent(0, eventVectors[7] * 64);

                    DialogueCue(13); 
                    
                    break;//[13] = new() { [2] = "I comprehend the intricacies of the rite", },

                case 15:

                    companions[2].TargetEvent(0, eventVectors[8] * 64);

                    break;

                case 16:

                    DialogueCue(16); break;//[16] = new() { [2] = "I'm not convinced about WHY we will perform it", },
                
                case 17:

                    companions[2].LookAtTarget(companions[1].Position, true);

                    break;

                case 19:

                    DialogueCueWithFeeling(19, 0, Character.Character.specials.gesture);
                    break;

                case 22:

                    DialogueLoad(companions[1],1);

                    break;
                
                case 30:

                    DialogueClear(companions[1]);

                    activeCounter = 100;

                    break;

            }

        }


        public void ScenePartTwo()
        {

            // ========================== 

            switch (activeCounter)
            {

                case 101:

                    DialogueCue(101);

                    break;//] = new() { [1] = "There is one complication", },

                case 104:

                    DialogueCueWithFeeling(104, 0, Character.Character.specials.gesture);

                    break;//] = new() { [1] = "When I appraised the golden bones", },

                case 107:

                    DialogueCue(107); 
                    
                    break;//] = new() { [1] = "It appeared to me that the set is incomplete", },

                case 109:

                    // Effigy

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Effigy] as StardewDruid.Character.Effigy;

                    voices[0] = companions[0];

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[0], eventVectors[101] * 64);

                    companions[0].eventName = eventId;

                    companions[0].TargetEvent(0, eventVectors[102] * 64);

                    break;

                case 110:

                    DialogueCue(110); 
                    
                    break;//] = new() { [0] = "Fear not", },

                case 113:

                    companions[1].LookAtTarget(companions[0].Position, true);
                    companions[2].LookAtTarget(companions[0].Position, true);
                    companions[3].LookAtTarget(companions[0].Position, true);

                    DialogueCue(113);

                    break;//] = new() { [0] = "The last piece is in my possession", },

                case 116:

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueCueWithFeeling(116); 
                    
                    break;//] = new() { [0] = "Archdruid, would you please assist me?", },

                case 118:

                    companions[0].TargetEvent(0, eventVectors[103] * 64);

                    break;

                case 119:

                    companions[1].LookAtTarget(companions[0].Position, true);
                    companions[2].LookAtTarget(companions[0].Position, true);
                    companions[3].LookAtTarget(companions[0].Position, true);

                    break;

                case 120:

                    // warp Farmer

                    location = Mod.instance.locations[LocationData.druid_grove_name];

                    Game1.warpFarmer(location.Name, (int)eventVectors[104].X, (int)eventVectors[104].Y, 2);

                    // Warp Effigy

                    CharacterMover.Warp(location, companions[0], eventVectors[105] * 64, false);

                    companions[0].TargetEvent(0, eventVectors[106] * 64, true);

                    // Jester

                    companions[4] = Mod.instance.characters[CharacterHandle.characters.Jester] as StardewDruid.Character.Jester;

                    voices[4] = companions[4];

                    companions[4].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[4], eventVectors[107] * 64);

                    companions[4].eventName = eventId;

                    companions[4].TargetEvent(0, eventVectors[108] * 64, true);

                    // Buffin

                    companions[5] = Mod.instance.characters[CharacterHandle.characters.Buffin] as StardewDruid.Character.Buffin;

                    voices[5] = companions[5];

                    companions[5].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[5], eventVectors[109] * 64);

                    companions[5].eventName = eventId;

                    companions[5].TargetEvent(0, eventVectors[110] * 64, true);

                    // Shadowtin

                    companions[6] = Mod.instance.characters[CharacterHandle.characters.Shadowtin] as StardewDruid.Character.Shadowtin;

                    voices[6] = companions[6];

                    companions[6].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[6], eventVectors[111] * 64);

                    companions[6].eventName = eventId;

                    companions[6].TargetEvent(0, eventVectors[112] * 64, true);

                    break;

                case 122:

                    companions[0].LookAtTarget(companions[4].Position, true);

                    companions[4].LookAtTarget(companions[0].Position, true);

                    companions[5].LookAtTarget(companions[0].Position, true);

                    DialogueCue(122);

                    break;//] = new() { [4] = "No pyre?", },

                case 123:

                    companions[6].LookAtTarget(companions[4].Position, true);

                    break;

                case 125:

                    DialogueCueWithFeeling(125,0,Character.Character.specials.gesture); 
                    
                    break;//] = new() { [0] = "No. Maybe a maypole", },

                case 128:

                    DialogueCue(128);

                    companions[6].TargetEvent(0, eventVectors[113] * 64, true);

                    break;//] = new() { [4] = "I guess some visions never come to pass", },

                case 131:

                    companions[4].TargetEvent(114, eventVectors[114] * 64, true);

                    companions[5].TargetEvent(115, eventVectors[115] * 64, true);

                    companions[0].LookAtTarget(companions[6].Position, true);

                    companions[6].LookAtTarget(companions[0].Position, true);

                    DialogueCueWithFeeling(131, 0, Character.Character.specials.gesture);

                    break;//] = new() { [6] = "Farmer. Effigy. I look forward to the rite", },

                case 134:

                    companions[6].TargetEvent(116, eventVectors[116] * 64, true);

                    DialogueCueWithFeeling(134, 0, Character.Character.specials.gesture);

                    break;//] = new() { [0] = "So do I. Until then, friends", },

                case 136:

                    companions[0].TargetEvent(0, eventVectors[117] * 64, true);

                    break;

                case 137:

                    DialogueCue(137);

                    break;//] = new() { [0] = "It's quiet here", },

                case 139:

                    companions[0].TargetEvent(0, eventVectors[118] * 64, true);

                    break;

                case 140:

                    AddActor(7, eventVectors[119] * 64); voices[7] = actors[7];

                    AddActor(8, eventVectors[120] * 64); voices[8] = actors[8];

                    AddActor(9, eventVectors[121] * 64); voices[9] = actors[9];

                    DialogueCue(140); 
                    
                    break;//] = new() { [7] = "Who disturbs the resting stones", },

                case 143:

                    companions[0].LookAtTarget(actors[7].Position, true);

                    DialogueCue(143); 
                    
                    break;//] = new() { [0] = "Your protectors", },

                case 146:

                    DialogueCue(146); 
                    
                    break;//] = new() { [7] = "...", },

                case 149:

                    DialogueCueWithFeeling(149); 
                    
                    break;//] = new() { [0] = "Be less grumpy for me", },

                case 152:

                    DialogueCue(152); 
                    
                    break;//] = new() { [7] = "Yes caretaker", [8] = "hehe", [9] = "(giggle)", },

                case 153:

                    companions[0].TargetEvent(0, eventVectors[122] * 64, true);

                    break;

                case 155:

                    location.characters.Remove(actors[7]); actors.Remove(7); voices.Remove(7);
                    location.characters.Remove(actors[8]); actors.Remove(8); voices.Remove(8);
                    location.characters.Remove(actors[9]); actors.Remove(9); voices.Remove(9);

                    DialogueCue(155); 
                    
                    break;//] = new() { [0] = "We've all benefited from your efforts", },

                case 158:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueCueWithFeeling(158, 0, Character.Character.specials.gesture);

                    break;//] = new() { [0] = "The Circle is in good hands", },

                case 161:

                    DialogueLoad(companions[0], 2);

                    break;

                case 170:

                    DialogueClear(companions[0]);

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

                    companions[0].TargetEvent(0, eventVectors[201] * 64, true);

                    DialogueCue(201);

                    break;//] = new() { [0] = "I have Blackfeather's missing piece with me", },

                case 204:

                    companions[0].TargetEvent(0, eventVectors[202] * 64, true);

                    DialogueCue(204);

                    break;//] = new() { [0] = "Come, let's walk the long trail back", },

                case 207:

                    // warp Farmer

                    location = Mod.instance.locations[LocationData.druid_gate_name];

                    Game1.warpFarmer(location.Name, (int)eventVectors[203].X, (int)eventVectors[203].Y, 2);

                    // warp Effigy

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_gate_name], companions[0], eventVectors[210] * 64, false);

                    // warp Blackfeather

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_gate_name], companions[1], eventVectors[211] * 64, false);

                    // warp Wizard

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_gate_name], companions[2], eventVectors[212] * 64, false);

                    // warp Linus

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_gate_name], companions[3], eventVectors[213] * 64, false);

                    // look at brazier

                    for(int i = 0; i < 7; i++)
                    {

                        companions[i].LookAtTarget(eventVectors[204] * 64 + new Vector2(32), true);

                    }

                    foreach (TerrainTile brazierTile in (Mod.instance.locations[LocationData.druid_gate_name] as Gate).brazierTiles)
                    {

                        brazierTile.index = 13;

                        brazierTile.reset();

                    }

                    foreach (TerrainTile maypoleTile in (Mod.instance.locations[LocationData.druid_gate_name] as Gate).maypoleTiles)
                    {

                        maypoleTile.index = 16;

                        maypoleTile.reset();

                    }

                    // relic brazier

                    //EventRender goldenPot = new("goldenPot", location.Name, eventVectors[204] * 64 + new Vector2(32), IconData.relics.golden_pot) { layer = 0.6f };

                    //eventRenders.Add(goldenPot);

                    break;

                case 208:

                    SetTrack("spirits_eve");

                    break;

                case 210:

                    DialogueCue(210);

                    companions[1].netSpecial.Set((int)Character.Character.specials.liftup);

                    companions[1].specialTimer = 600;

                    break;//] = new() { [0] = "Great Stream of Chaos", },

                case 213:

                    DialogueCue(213);

                    break;//] = new() { [0] = "The crows have come home", },

                case 216:

                    companions[1].netSpecial.Set((int)Character.Character.specials.liftdown);

                    companions[1].specialTimer = 180;

                    DialogueCueWithFeeling(216,0,Character.Character.specials.special);

                    companions[2].specialTimer = 120;

                    break;

                case 219:

                    DialogueCueWithFeeling(219, 0, Character.Character.specials.special);

                    companions[2].specialTimer = 120;

                    break;

                case 222:

                    companions[1].netSpecial.Set((int)Character.Character.specials.liftup);

                    companions[1].specialTimer = 600;

                    DialogueCue(222);

                    break;//] = new() { [0] = "We offer the golden bones to you", },

                case 225:

                    companions[0].TargetEvent(205, eventVectors[204] * 64 + new Vector2(32), true);

                    companions[0].SetDash(eventVectors[204] * 64 + new Vector2(32));

                    DialogueCue(225);

                    companions[0].netLayer.Set(7500);

                    break;

                case 228:

                    companions[1].netSpecial.Set((int)Character.Character.specials.liftdown);

                    companions[1].specialTimer = 180;

                    companions[1].doEmote(16);
                    companions[3].doEmote(16);
                    companions[4].doEmote(32);
                    companions[5].doEmote(32);

                    DialogueCue(228);

                    break;//] = new() { [0] = "Oh?", [2] = "Curious", [3] = "Ahhh", [6] = "Always the weirdest action possible", },

                case 231:

                    DialogueLoad(companions[1], 3);

                    DialogueNext(companions[1]);

                    break;

                case 240:

                    DialogueClear(companions[1]);

                    activeCounter = 300;

                    break;

            }

        }

        public void ScenePartFour()
        {

            if(activeCounter > 320 && activeCounter <= 352)
            {
                
                Firefiend fiend = new(eventVectors[205], Mod.instance.CombatDifficulty());

                fiend.Position = fiend.Position + ModUtility.DirectionAsVector(Mod.instance.randomIndex.Next(8) * 192);

                fiend.SetMode(Mod.instance.randomIndex.Next(0, 3));

                fiend.RandomScheme();

                fiend.RandomTemperment();

                fiend.groupMode = true;

                monsterHandle.SpawnImport(fiend);

                for(int i = 0; i < 2; i++)
                {

                    List<Vector2> meteorTargets = ModUtility.GetTilesWithinRadius(location, eventVectors[204], Mod.instance.randomIndex.Next(4, 9));

                    SpellHandle meteor = new(location, meteorTargets[Mod.instance.randomIndex.Next(meteorTargets.Count)] * 64, Game1.player.Position, 256, Mod.instance.CombatDifficulty(), -1);

                    meteor.type = SpellHandle.spells.orbital;

                    meteor.missile = IconData.missiles.fireball;

                    meteor.indicator = IconData.cursors.shadow;

                    meteor.display = IconData.impacts.bomb;

                    meteor.scheme = IconData.schemes.stars;

                    meteor.projectile = 4;

                    Mod.instance.spellRegister.Add(meteor);

                }


            }

            // ========================== 

            switch (activeCounter)
            {

                case 301:

                    Game1.stopMusicTrack(MusicContext.Default);

                    activeCounter = 304;

                    DialogueCue(304);

                    // MonsterHandle

                    monsterHandle = new(eventVectors[204] * 64 + new Vector2(32), location);

                    monsterHandle.warpout = IconData.warps.smoke;

                    break;//] = new() { [1] = "Accept our offering", [2] = "Accept our offering", [3] = "Accept our offering", },

                case 307:

                    companions[1].LookAtTarget(eventVectors[204] * 64, true);

                    companions[1].netSpecial.Set((int)Character.Character.specials.liftdown);

                    companions[1].specialTimer = 180;

                    DialogueCue(307);

                    break;

                case 310:

                    DialogueCue(310);

                    (Mod.instance.locations[LocationData.druid_gate_name] as Gate).alightBrazier = true;

                    foreach (TerrainTile brazierTile in (Mod.instance.locations[LocationData.druid_gate_name] as Gate).brazierTiles)
                    {

                        brazierTile.index = 14;

                        brazierTile.reset();

                    }

                    location.playSound("furnace");

                    break;//] = new() { [1] = "Fulfil your oath to Elders and Fates", },

                case 313:

                    companions[1].netSpecial.Set((int)Character.Character.specials.liftdown);

                    companions[1].specialTimer = 180;

                    break;

                case 315:

                    Mod.instance.spellRegister.Add(new(eventVectors[204] * 64 + new Vector2(32), 256, IconData.impacts.bomb, new()) { sound = SpellHandle.sounds.explosion, impactLayer = 2f, });

                    //Mod.instance.iconData.ImpactIndicator(location, eventVectors[204] * 64 + new Vector2(32), IconData.impacts.bomb, 256, new() { layer = 2f, });

                    break;

                case 316:

                    // Boss

                    Game1.flashAlpha = 1f;

                    (Mod.instance.locations[LocationData.druid_gate_name] as Gate).alightBrazier = false;

                    LoadBoss(new Firefiend(ModUtility.PositionToTile(eventVectors[205] * 64), Mod.instance.CombatDifficulty()), 0, 4, 10);

                    bosses[0].Position = eventVectors[205] * 64 + new Vector2(32,0);

                    voices[10] = bosses[0];

                    bosses[0].netPosturing.Set(true);

                    bosses[0].setWounded = true;

                    bosses[0].netLayer.Set(7500);

                    // Remove Effigy

                    companions[0].SwitchToMode(Character.Character.mode.limbo, Game1.player);

                    companions.Remove(0); voices.Remove(0);

                    companions[1].doEmote(16);
                    companions[2].doEmote(16);
                    companions[3].doEmote(16);
                    companions[6].doEmote(16);

                    DialogueCue(316);

                    break;//] = new() { [5] = "Behold, the rage of the heavens", },

                case 319:

                    bosses[0].LookAtFarmer();

                    DialogueCue(319);

                    //SetTrack("cowboy_boss");

                    break;//] = new() { [5] = "Made manifest in roaring flame", },

                case 322:

                    Growler bear = new(CharacterHandle.characters.BrownBear);

                    CharacterMover.Warp(location, bear, companions[3].Position, false);

                    Mod.instance.spellRegister.Add(new(companions[3].Position, 256, IconData.impacts.plume, new()));

                    location.characters.Remove(companions[3]);

                    companions[3] = bear;

                    voices[3] = bear;

                    Mod.instance.characters[CharacterHandle.characters.BrownBear] = companions[3];

                    Mod.instance.characters[CharacterHandle.characters.Wizard] = companions[2];

                    for (int i = 1; i < 7; i++)
                    {

                        companions[i].SwitchToMode(Character.Character.mode.track, Game1.player);

                        companions[i].specialDisable = true;

                    }

                    DialogueCue(322);

                    break;//] = new() { [4] = "Yikes that's hot", },

                case 325:

                    DialogueCue(325);

                    break;//] = new() { [6] = "Nothing to be done about it, fight!", },

                case 328:

                    DialogueCue(328);

                    break;//] = new() { [1] = "Who knew there was so much anger in him", },

                case 331:

                    DialogueCue(331);

                    break;//] = new() { [1] = "He contained it all this time", },

                case 334:

                    DialogueCue(334);

                    break;//] = new() { [3] = "Ah a long time back I saw", },

                case 337:

                    DialogueCue(337);

                    break;//] = new() { [3] = "A moment of horrifying rage", },

                case 340:

                    DialogueCue(340);

                    break;//] = new() { [3] = "That poor pumpkin", },

                case 343:

                    DialogueCue(343);

                    break;//] = new() { [2] = "Fall back to the grove!", },

                case 345:

                    for (int i = 1; i < 7; i++)
                    {

                        companions[i].SwitchToMode(Character.Character.mode.scene, Game1.player);

                        Vector2 companionVector = ModUtility.PositionToTile(companions[i].Position);

                        if(companionVector.Y > eventVectors[204].Y)
                        {


                            companions[i].TargetEvent(350 + i, eventVectors[301] * 64, true);

                        }
                        else if(companionVector.X < eventVectors[204].X)
                        {

                            companions[i].TargetEvent(0, eventVectors[304] * 64, true);
                            companions[i].TargetEvent(350 + i, eventVectors[301] * 64, false);

                        }
                        else
                        {

                            companions[i].TargetEvent(0, eventVectors[305] * 64, true);
                            companions[i].TargetEvent(350 + i, eventVectors[301] * 64, false);

                        }

                        companions[i].netMovement.Set((int)Character.Character.movements.run);

                    }

                    break;

                case 346:

                    DialogueCue(346);

                    break;//] = new() { [9] = "Successor...", },

                case 349:

                    StopTrack();

                    DialogueCue(349);

                    break;//] = new() { [9] = "SUCCESSOR", },

                case 352:

                    companions[1].TargetEvent(0, eventVectors[302] * 64, true);

                    DialogueCue(352);

                    break;//] = new() { [1] = "It's in pain. Only you can help it", },

                case 354:

                    SpellHandle bolt = new(Game1.player, new() { bosses[0], }, Mod.instance.CombatDamage() * 2);

                    bolt.scheme = IconData.schemes.golden;

                    bolt.type = SpellHandle.spells.bolt;

                    bolt.projectile = 4;

                    bolt.sound = SpellHandle.sounds.thunder;

                    Mod.instance.spellRegister.Add(bolt);

                    monsterHandle.ShutDown();

                    break;

                case 355:

                    companions[1].TargetEvent(0, eventVectors[303] * 64, true);

                    DialogueCue(355);

                    break;//] = new() { [1] = "I'm with you", },

                case 357:

                    SpellHandle bolt3 = new(Game1.player, new() { bosses[0], }, Mod.instance.CombatDamage() * 2);

                    bolt3.scheme = IconData.schemes.golden;

                    bolt3.type = SpellHandle.spells.bolt;

                    bolt3.projectile = 4;

                    bolt3.sound = SpellHandle.sounds.thunder;

                    Mod.instance.spellRegister.Add(bolt3);

                    break;

                case 358:

                    DialogueCue(358);

                    break;//] = new() { [9] = "...peace...", },

                case 360:

                    SpellHandle bolt2 = new(Game1.player, new() { bosses[0], }, Mod.instance.CombatDamage() * 2);

                    bolt2.scheme = IconData.schemes.golden;

                    bolt2.type = SpellHandle.spells.bolt;

                    bolt2.projectile = 4;

                    bolt2.sound = SpellHandle.sounds.thunder;

                    Mod.instance.spellRegister.Add(bolt2);

                    Mod.instance.iconData.ImpactIndicator(location, eventVectors[204] * 64 + new Vector2(32), IconData.impacts.bomb, 256, new() { layer = 2f, });

                    location.characters.Remove(bosses[0]);

                    bosses.Clear();

                    voices.Remove(10);

                    RemoveMonsters();

                    Mod.instance.spellRegister.Add(new(eventVectors[204] * 64, 256, IconData.impacts.smoke, new()) { sound = SpellHandle.sounds.flameSpellHit});

                    EventRender goldenCore = new("goldenCore", location.Name, eventVectors[204] * 64 + new Vector2(32), IconData.relics.golden_core) { layer = 1f};

                    eventRenders.Add(goldenCore);

                    DialogueLoad(companions[1], 4);

                    break;

                case 370:

                    DialogueClear(companions[1]);

                    activeCounter = 400;

                    break;

            }

        }

        public void ScenePartFive()
        {

            // ========================== 

            switch (activeCounter)
            {

                case 402:

                    Cast.Bones.Corvids.SummonCorvids();

                    Mod.instance.trackers[CharacterHandle.characters.Raven].eventLock = true;

                    Mod.instance.spellRegister.Add(new(Game1.player.Position, 384, IconData.impacts.nature, new()) { sound = SpellHandle.sounds.getNewSpecialItem, });

                    companions[1].LookAtTarget(Mod.instance.characters[CharacterHandle.characters.Raven].Position,true);

                    break;

                case 403:

                    companions[1].doEmote(32);

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    companions[1].LookAtTarget(Mod.instance.characters[CharacterHandle.characters.Raven].Position, true);

                    break;

                case 406:

                    ThrowHandle throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Raven].Position, IconData.relics.crow_hammer);

                    throwRelic.pocket = false;

                    throwRelic.register();

                    break;

                case 407:

                    ThrowHandle.AnimateHoldup();

                    Microsoft.Xna.Framework.Rectangle relicRectCrow = IconData.RelicRectangles(IconData.relics.crow_hammer);

                    TemporaryAnimatedSprite animationCrow = new(0, 2500, 1, 1, Game1.player.Position + new Vector2(2, -124f), false, false)
                    {
                        sourceRect = relicRectCrow,
                        sourceRectStartingPos = new(relicRectCrow.X, relicRectCrow.Y),
                        texture = Mod.instance.iconData.relicsTexture,
                        layerDepth = 900f,
                        delayBeforeAnimationStart = 175,
                        scale = 3f,

                    };

                    Game1.player.currentLocation.TemporarySprites.Add(animationCrow);

                    Game1.drawObjectDialogue(cues[407][0]);

                    companions[1].doEmote(20);

                    break;

                case 409:

                    RemoveSummons();

                    CharacterMover.Warp(location, companions[2], eventVectors[410] * 64);

                    companions[2].TargetEvent(412, eventVectors[412] * 64);

                    CharacterMover.Warp(location, companions[3], eventVectors[410] * 64);

                    companions[3].TargetEvent(413, eventVectors[413] * 64);

                    CharacterMover.Warp(location, companions[4], eventVectors[410] * 64);

                    companions[4].TargetEvent(414, eventVectors[414] * 64);

                    CharacterMover.Warp(location, companions[5], eventVectors[410] * 64);

                    companions[5].TargetEvent(415, eventVectors[415] * 64);

                    CharacterMover.Warp(location, companions[6], eventVectors[410] * 64);

                    companions[6].TargetEvent(416, eventVectors[416] * 64);

                    break;

                case 410:

                    Mod.instance.spellRegister.Add(new(Game1.player.Position, 288, IconData.impacts.nature, new()) { scheme = IconData.schemes.golden, sound = SpellHandle.sounds.getNewSpecialItem, });

                    Vector2 where = eventVectors[204] * 64 + new Vector2(32, 32);

                    Mod.instance.spellRegister.Add(new(where, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -60, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.secret1 });
                    Mod.instance.spellRegister.Add(new(where, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -45, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.silent, });
                    Mod.instance.spellRegister.Add(new(where, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -30, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.silent, });
                    Mod.instance.spellRegister.Add(new(where, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -15, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.silent, });
                    Mod.instance.spellRegister.Add(new(where, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.thunder, });

                    Microsoft.Xna.Framework.Rectangle relicRect = IconData.RelicRectangles(IconData.relics.crow_hammer);

                    TemporaryAnimatedSprite animation = new(0, 1500, 1, 1, where - new Vector2(16, 60), false, false)
                    {
                        sourceRect = relicRect,
                        sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                        texture = Mod.instance.iconData.relicsTexture,
                        layerDepth = 900f,
                        rotation = -0.76f,
                        scale = 4f,
                    };

                    Game1.player.currentLocation.TemporarySprites.Add(animation);

                    Mod.instance.spellRegister.Add(new(where - new Vector2(24, 32), 320, IconData.impacts.nature, new()) { type = SpellHandle.spells.effect, counter = -45, instant = true, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.yoba });

                    Mod.instance.spellRegister.Add(new(where - new Vector2(24, 32), 320, IconData.impacts.nature, new()) { type = SpellHandle.spells.effect, instant = true, scheme = IconData.schemes.golden, });

                    break;

                case 411:

                    //Mod.instance.spellRegister.Add(new(eventVectors[204] * 64 + new Vector2(32), 256, IconData.impacts.smoke, new()) { sound = SpellHandle.sounds.flameSpellHit });

                    eventRenders.Clear();

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Aldebaran, Character.Character.mode.scene);

                    CharacterMover.Warp(location,Mod.instance.characters[CharacterHandle.characters.Aldebaran], eventVectors[204] * 64 + new Vector2(32), false);

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Aldebaran];

                    voices[0] = companions[0];

                    voices[11] = companions[0];

                    companions[0].eventName = eventId;

                    companions[0].netLayer.Set(10000);

                    companions[1].LookAtTarget(companions[0].Position, true);

                    break;

                case 412:

                    for(int i = 1; i < 7; i++)
                    {

                        companions[i].doEmote(8);

                    }

                    companions[0].TargetEvent(401, eventVectors[405] * 64, true);

                    companions[0].SetDash(eventVectors[405] * 64);

                    break;

                case 415:


                    for (int i = 1; i < 7; i++)
                    {

                        companions[i].LookAtTarget(companions[0].Position,true);

                    }

                    companions[0].netLayer.Set(0);

                    DialogueLoad(companions[0], 5);

                    break;

                case 423:

                    DialogueNext(companions[0]);

                    break;

                case 425:

                    DialogueClear(companions[0]);

                    activeCounter = 500;

                    break;

            }

        }

        public void ScenePartSix()
        {

            // ========================== 

            switch (activeCounter)
            {

                case 501:

                    companions[0].LookAtTarget(companions[1].Position, true);

                    for (int i = 1; i < 7; i++)
                    {

                        companions[i].LookAtTarget(companions[0].Position, true);

                    }

                    break;

                case 502:

                    DialogueCue(502);

                    break;

                case 505:

                    DialogueCue(505);

                    break;

                case 506:

                    for (int i = 1; i < 7; i++)
                    {

                        companions[i].doEmote(20);

                    };

                    break;

                case 510:

                    eventComplete = true;

                    break;


            }

        }

        public override void EventScene(int index)
        {

            switch (index)
            {

                // Jester exit grove
                case 114:

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_gate_name], companions[4], eventVectors[214] * 64, false);

                    break;

                // Buffin exit grove
                case 115:

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_gate_name], companions[5], eventVectors[215] * 64, false);

                    break;

                // Shadowtin exit grove
                case 116:

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_gate_name], companions[6], eventVectors[216] * 64, false);

                    break;

                // Effigy kneeling in brazier
                case 205:

                    companions[0].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                // Blackfeather leave
                case 351:

                    companions[1].LookAtTarget(Game1.player.Position);

                    break;

                // Wizard leave
                case 352:

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_grove_name], companions[2], eventVectors[352] * 64, false);

                    break;

                // Linus leave
                case 353:

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.BrownBear))
                    {

                        Mod.instance.characters.Remove(CharacterHandle.characters.BrownBear);

                    }

                    location.characters.Remove(companions[3]);

                    companions[3] = new Linus(CharacterHandle.characters.Linus);

                    voices[3] = companions[3];

                    companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[3].eventName = eventId;

                    CharacterMover.Warp(location, companions[3], eventVectors[3] * 64);

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_grove_name], companions[3], eventVectors[353] * 64, false);

                    break;

                // Jester leave
                case 354:

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_grove_name], companions[4], eventVectors[354] * 64, false);

                    break;

                // Buffin leave
                case 355:

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_grove_name], companions[5], eventVectors[355] * 64, false);

                    break;

                // Shadowtin leave
                case 356:

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_grove_name], companions[6], eventVectors[356] * 64, false);

                    break;

                case 401:

                    companions[0].LookAtTarget(Game1.player.Position, true);
                    
                    break;

                case 412:

                    companions[2].LookAtTarget(eventVectors[204] * 64, true);

                    break;

                case 413:

                    companions[3].LookAtTarget(eventVectors[204] * 64, true);

                    break;

                case 414:

                    companions[4].LookAtTarget(eventVectors[204] * 64, true);

                    break;

                case 415:

                    companions[5].LookAtTarget(eventVectors[204] * 64, true);

                    break;

                case 416:

                    companions[6].LookAtTarget(eventVectors[204] * 64, true);

                    break;

            }

        }

    }

}