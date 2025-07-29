using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Ether;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
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
using System.IO;
using System.Linq;

namespace StardewDruid.Event.Scene
{

    public class SwordHeirs : EventHandle
    {

        public Dictionary<int, Vector2> eventVectors = new()
        {

            // Aldebaran Enter
            [1] = new Vector2(28, 15),
            // Aldebaran look at Gate
            [2] = new Vector2(28, 10),
            // Shift Blackfeather
            [3] = new Vector2(26, 31),

            // mist vector
            [101] = new Vector2(26, 10),
            // lady enter
            [102] = new Vector2(25, 11),
            // lady position
            [103] = new Vector2(25, 7),
            // crowmother enter
            [104] = new Vector2(28, 1),
            // crowmother position
            [105] = new Vector2(27, 7),

            // Aldebaran present himself
            [201] = new Vector2(27, 8),
            // Gate lightning
            [202] = new Vector2(26, 4),


        };

        public SwordHeirs()
        {

            mainEvent = true;

            

        }

        public override void EventActivate()
        {

            base.EventActivate();

            locales = new()
            {
                LocationHandle.druid_moors_name,

            };

            origin = eventVectors[1] * 64;

            location.playSound("discoverMineral");

            SendFriendsHome();

        }

        public override void OnLocationAbort()
        {

            Mod.instance.RegisterMessage(StringData.Strings(StringData.stringkeys.abortTomorrow), 3, true);

        }

        public override void EventInterval()
        {

            activeCounter++;

            // Introduction (gates)
            if (activeCounter < 100)
            {

                ScenePartOne();

                return;

            }

            // First mist scene (gates)
            if (activeCounter < 200)
            {

                ScenePartTwo();

                return;

            }

            // Sword retrieval
            if (activeCounter < 300)
            {

                ScenePartThree();

                return;

            }


            DialogueClear(companions[0]);

            eventComplete = true;

        }

        public void ScenePartOne()
        {

            switch (activeCounter)
            {

                case 1:

                    Game1.stopMusicTrack(MusicContext.Default);

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Aldebaran] as StardewDruid.Character.Aldebaran;

                    voices[0] = companions[0];

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[0].currentLocation.Name != location.Name || Vector2.Distance(eventVectors[2] * 64, companions[0].Position) > 480)
                    {

                        CharacterMover.Warp(location, companions[0], eventVectors[1] * 64);

                        companions[0].LookAtTarget(eventVectors[2] * 64);

                    }

                    companions[0].eventName = eventId;

                    if(Mod.instance.characters[CharacterHandle.characters.Blackfeather].modeActive != Character.Character.mode.track)
                    {

                        if (Mod.instance.characters[CharacterHandle.characters.Blackfeather].currentLocation.Name == location.Name)
                        {

                            CharacterMover.Warp(
                                Mod.instance.locations[LocationHandle.druid_grove_name],
                                Mod.instance.characters[CharacterHandle.characters.Blackfeather],
                                CharacterHandle.CharacterStart(CharacterHandle.locations.grove, CharacterHandle.characters.Blackfeather)
                                );

                        }

                    }

                    break;

                case 2:

                    companions[0].TargetEvent(2, eventVectors[2] * 64, true);

                    DialogueCue(1);

                    break;

                case 5:

                    DialogueCue(2);

                    break;

                case 8:

                    DialogueCue(3);

                    break;

                case 11:

                    companions[0].ResetActives(true);

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    companions[0].doEmote(8);

                    DialogueCue(4);

                    break;

                case 14:

                    DialogueLoad(0, 1);

                    break;

                case 24:

                    activeCounter = 100;

                    break;

            }

        }

        public void ScenePartTwo()
        {

            switch (activeCounter)
            {

                case 101:

                    SetTrack("woodsTheme");

                    DialogueClear(companions[0]);

                    break;

                case 102:

                    Wisps wispNew = new();

                    wispNew.EventSetup(Game1.player, eventVectors[101] * 64, Rite.eventWisps);

                    wispNew.EventActivate();

                    wispNew.WispArray();

                    Game1.flashAlpha = 1f;

                    location.playSound(SpellHandle.Sounds.thunder.ToString());

                    (Mod.instance.locations[LocationHandle.druid_sanctuary_name] as Sanctuary).ambientDarkness = true;

                    companions[0].LookAtTarget(eventVectors[1] * 64, true);

                    break;

                case 103:

                    DialogueCue(101);// [101] = new() { [0] = "Ahh... something stirs", },

                    break;

                case 105:

                    (Mod.instance.locations[LocationHandle.druid_sanctuary_name] as Sanctuary).GateOverride(1);

                    // Lady Beyond

                    companions[1] = new Lady(CharacterHandle.characters.LadyBeyond)
                    {
                        fadeOut = 0.4f, 
                        fadeSet = 0.8f,
                    };

                    voices[1] = companions[1];

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[1].currentLocation.Name != location.Name)
                    {

                        companions[1].currentLocation.characters.Remove(companions[1]);

                        companions[1].currentLocation = location;

                        companions[1].currentLocation.characters.Add(companions[1]);

                    }

                    companions[1].Position = eventVectors[102] * 64;

                    companions[1].TargetEvent(103, eventVectors[103] * 64, true);

                    Mod.instance.iconData.ImpactIndicator(location, companions[1].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { });

                    // Crowmother

                    companions[2] = new Crowmother(CharacterHandle.characters.Crowmother)
                    {
                        fadeOut = 0.4f,
                        fadeSet = 0.8f,
                    };

                    voices[2] = companions[2];

                    companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[2].currentLocation.Name != location.Name)
                    {
                        companions[2].currentLocation.characters.Remove(companions[2]);

                        companions[2].currentLocation = location;

                        companions[2].currentLocation.characters.Add(companions[2]);

                    }

                    companions[2].Position = eventVectors[104] * 64;

                    companions[2].TargetEvent(105,eventVectors[105] * 64, true);

                    companions[0].LookAtTarget(companions[2].Position, true);

                    Mod.instance.iconData.ImpactIndicator(location, companions[2].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { });

                    
                    break;

                case 106:

                    DialogueCue(102); //  [102] = new() { [2] = "Heiress", },

                    break;

                case 109:

                    DialogueCue(103); //  [103] = new() { [2] = "Thank you for coming", },

                    break;

                case 112:

                    DialogueCueWithFeeling(104,0,Character.Character.specials.gesture); // [104] = new() { [2] = "I am an Artisan, the Mother of Crows", },

                    break;

                case 115:

                    DialogueCue(105); // [105] = new() { [1] = "Salut, Crowmother", },

                    break;

                case 118:

                    DialogueCueWithFeeling(106, 0, Character.Character.specials.gesture); // [106] = new() { [1] = "Has Yoba heard my case?", },

                    break;

                case 121:

                    DialogueCue(107); // [107] = new() { [1] = "Will the Fae Court favour me", },

                    break;

                case 124:

                    companions[2].LookAtTarget(companions[1].Position + new Vector2(0, 320), true);

                    DialogueCue(108); // [108] = new() { [2] = "The High Priest condemns the celestials", },

                    break;

                case 127:

                    companions[2].LookAtTarget(companions[1].Position - new Vector2(0, 320), true);

                    DialogueCue(109); // [109] = new() { [2] = "But the Prince has bargained with chaos", },

                    break;

                case 130:

                    companions[2].LookAtTarget(companions[1].Position, true);

                    DialogueCue(110); // [110] = new() { [2] = "and I have brokered a peace", },

                    break;

                case 133:

                    EventRender goldenCore = new("golden_core", location.Name, companions[2].Position - new Vector2(32, 40), IconData.relics.golden_core);

                    goldenCore.layer += 0.64f;

                    goldenCore.scale = 3f;

                    eventRenders.Add("golden_core", goldenCore);

                    DialogueCueWithFeeling(111, 0, Character.Character.specials.gesture); // [111] = new() { [2] = "in return for the heart of a star", },

                    break;
                case 134:

                    companions[2].netSpecial.Set((int)Character.Character.specials.gesture);

                    companions[2].specialFrame = 0;

                    companions[2].specialTimer = 60;

                    break;

                case 135:

                    eventRenders.Clear();

                    break;

                case 136:

                    DialogueCueWithFeeling(112, 0, Character.Character.specials.invoke);
                    // [112] = new() { [1] = "I will make him pay for his treachery", },

                    break;

                case 139:

                    DialogueCue(113);// [113] = new() { [1] = "My retribution will be just and terrible", },

                    break;

                case 142:

                    DialogueCueWithFeeling(114, 0, Character.Character.specials.gesture);  // [114] = new() { [2] = "Delay, for his power eclipses yours", },

                    break;

                case 145:

                    DialogueCue(115); // [115] = new() { [2] = "and what about your duties?", },

                    break;

                case 148:

                    DialogueCueWithFeeling(116, 0, Character.Character.specials.gesture); // [116] = new() { [2] = "who will attend to the needs of your kin", },

                    break;

                case 151:

                    DialogueCueWithFeeling(117, 0, Character.Character.specials.gesture); // [117] = new() { [1] = "What about my needs, my vengeance?", },

                    break;

                case 154:

                    DialogueCue(118); // [118] = new() { [2] = "In time, others will take up your cause", },

                    break;

                case 157:

                    DialogueCueWithFeeling(119, 0, Character.Character.specials.gesture); // [119] = new() { [2] = "Amongst them, the general will redeem himself", },

                    break;

                case 160:

                    DialogueCue(120); // [120] = new() { [1] = "(sigh) I... I want to believe you", },

                    companions[1].ResetActives();

                    companions[1].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                case 163:

                    DialogueCue(121); // [121] = new() { [2] = "I will remain with you", },

                    companions[2].ResetActives();

                    companions[2].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                case 166:

                    DialogueCue(122); // [122] = new() { [2] = "But only you can seal this gate.", },

                    break;

                case 167:

                    companions[1].ResetActives();

                    companions[2].ResetActives();

                    break;

                case 169:

                    DialogueCue(123); // [123] = new() { [2] = "...very well ", },

                    companions[1].ResetActives();

                    companions[1].netSpecial.Set((int)Character.Character.specials.liftup);

                    companions[1].specialTimer = 240;

                    break;

                case 170:

                    EventRender heiressGift = new("heiress_gift", location.Name, companions[1].Position + new Vector2(96,-128), IconData.relics.druid_hieress);

                    heiressGift.layer += 0.64f;

                    heiressGift.scale = 3f;

                    eventRenders.Add("heiress_gift", heiressGift);
                    
                    break;

                case 171:

                    Vector2 strikeVector = companions[1].Position + new Vector2(80, -172);

                    Mod.instance.spellRegister.Add(new(strikeVector, 192, IconData.impacts.none, new()) { type = SpellHandle.Spells.greatbolt, displayFactor = 3, sound = SpellHandle.Sounds.thunder, });

                    break;

                case 172:

                    (Mod.instance.locations[LocationHandle.druid_sanctuary_name] as Sanctuary).GateOverride(2);

                    Game1.playSound(SpellHandle.Sounds.boulderBreak.ToString());

                    DialogueCue(124); // [124] = new() { [1] = "Promise me he will come back", },

                    break;

                case 173:

                    eventRenders.Clear();

                    companions[1].LookAtTarget(eventVectors[104] * 64, true);

                    companions[2].LookAtTarget(eventVectors[104] * 64, true);

                    break;

                case 175:

                    DialogueCue(125); // [125] = new() { [1] = "He will, heiress", },

                    break;

                case 178:

                    Mod.instance.iconData.ImpactIndicator(location, companions[1].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { });

                    Mod.instance.iconData.ImpactIndicator(location, companions[2].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { });

                    companions[1].currentLocation.characters.Remove(companions[1]);

                    companions.Remove(1);

                    voices.Remove(1);

                    companions[2].currentLocation.characters.Remove(companions[2]);

                    companions.Remove(2);

                    voices.Remove(2);

                    (Mod.instance.locations[LocationHandle.druid_sanctuary_name] as Sanctuary).ambientDarkness =false;

                    Mod.instance.eventRegister[Rite.eventWisps].eventComplete = true;

                    StopTrack();

                    break;

                case 179:

                    DialogueLoad(companions[0], 2);

                    break;

                case 187:

                    activeCounter = 200;

                    break;

            }

        }

        public void ScenePartThree()
        {

            switch (activeCounter)
            {

                case 201:

                    DialogueClear(companions[0]);

                    DialogueCue(201);

                    companions[0].TargetEvent(0, eventVectors[201] * 64, true);

                    break;

                case 204:

                    DialogueCueWithFeeling(202);

                    break;

                case 207:

                    DialogueCue(203);

                    break;

                case 209:

                    (Mod.instance.locations[LocationHandle.druid_sanctuary_name] as Sanctuary).GateOverride(0);

                    (Mod.instance.locations[LocationHandle.druid_sanctuary_name] as Sanctuary).OpenGate();

                    Game1.playSound(SpellHandle.Sounds.boulderBreak.ToString());

                    Game1.playSound(SpellHandle.Sounds.thunder.ToString());

                    eventComplete = true;

                    break;

            }

        }

        public override void EventScene(int index)
        {
            switch (index)
            {

                case 2:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(eventVectors[104]*64, true);

                    break;

                case 103:

                    companions[1].ResetActives();

                    companions[1].LookAtTarget(companions[2].Position, true);

                    break;

                case 105:

                    companions[2].ResetActives();

                    companions[2].LookAtTarget(companions[1].Position, true);

                    break;
            }
        }

    }

}