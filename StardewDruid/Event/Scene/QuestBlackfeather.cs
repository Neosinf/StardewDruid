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
using System.IO;
using System.Linq;


namespace StardewDruid.Event.Scene
{
    public class QuestBlackfeather : EventHandle
    {

        public Dictionary<int, Vector2> eventVectors = new()
        {

            // blackfeather enter
            [1] = new Vector2(31, 15),
            // blackfeather walk around
            [2] = new Vector2(31, 19),

            // mist vector
            [101] = new Vector2(28, 14),
            // hut 1
            [102] = new Vector2(24, 15),
            // hut 2
            [103] = new Vector2(32, 9),
            // hut 3
            [104] = new Vector2(34, 12),
            // hut 4
            [105] = new Vector2(25, 11),

            // crowmother entry
            [106] = new Vector2(27, 14),
            // lady entry
            [107] = new Vector2(30, 13),
            // guardian entry
            [108] = new Vector2(25, 5),
            // crowmother wander
            [109] = new Vector2(29, 15),

            // walk towards shrine engine door
            [201] = new Vector2(40,13),
            // farmer entry
            [202] = new Vector2(26, 23),
            // Blackfeather entry
            [203] = new Vector2(28, 23),
            // Blackfeather walk into
            [204] = new Vector2(30, 21),
            // Mist vector
            [205] = new Vector2(27, 17),
            // Crowmother
            [206] = new Vector2(22, 17),
            // Lady Beyond
            [207] = new Vector2(33, 17),
            // Crowmother walk towards
            [208] = new Vector2(26, 17),
            // Crowmother walk back
            [209] = new Vector2(22, 17),

            // walk towards shrine engine door
            [301] = new Vector2(28, 26),
            // farmer atoll entry
            [302] = new Vector2(15, 11),
            // Blackfeather entry
            [303] = new Vector2(17, 12),
            // Blackfeather walk into center
            [304] = new Vector2(24, 17),
            // Blackfeather walk down
            [305] = new Vector2(26, 22),
            // Blackfeather turn to center
            [306] = new Vector2(24, 14),
            // Wisps
            [307] = new Vector2(26, 12),
            // First Farmer
            [308] = new Vector2(29, 15),
            // Justiciar
            [309] = new Vector2(26, 11),
            // Reaper
            [310] = new Vector2(21, 13),
            // Seafarer
            [311] = new Vector2(18, 15),
            // Seafarer2
            [312] = new Vector2(16, 14),
            // Seafarer3
            [313] = new Vector2(17, 16),

            // Lady move 1
            [401] = new Vector2(25, 17),
            // Lady move 2
            [402] = new Vector2(18, 14),

            // Blackfeather return
            [501] = new Vector2(25, 17),

        };

        public QuestBlackfeather()
        {

            mainEvent = true;

            activeLimit = -1;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            locales = new()
            {
                LocationData.druid_clearing_name,
                LocationData.druid_engineum_name,
                LocationData.druid_atoll_name,

            };

            origin = eventVectors[1] * 64;

            location.playSound("discoverMineral");

            SendFriendsHome();

        }
        
        public override bool AttemptReset()
        {

            Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.abortTomorrow), 3, true);

            return false;

        }

        public override void RemoveActors()
        {

            base.RemoveActors();

            Cast.Bones.Corvids.RemoveCorvids();

        }

        public override void EventInterval()
        {

            activeCounter++;

            // Introduction (orchard)
            if (activeCounter < 100)
            {

                ScenePartOne();

                return;

            }

            // First mist scene (orchard)
            if (activeCounter < 200)
            {

                ScenePartTwo();

                return;

            }

            // Second mist scene (engine room)
            if (activeCounter < 300)
            {

                ScenePartThree();

                return;

            }

            // Third mist scene (atoll)
            if (activeCounter < 400)
            {

                ScenePartFour();

                return;

            }

            // Confusion (atoll)
            if (activeCounter < 500)
            {

                ScenePartFive();

                return;

            }

            // Fight sequence (atoll)
            if (activeCounter < 600)
            {

                ScenePartSix();

                return;

            }

            // Reflections (atoll)
            if (activeCounter < 700)
            {

                ScenePartSeven();

                return;

            }

            eventComplete = true;

        }

        public void ScenePartOne()
        {

            switch (activeCounter)
            {

                case 1:

                    Game1.stopMusicTrack(MusicContext.Default);

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Blackfeather] as StardewDruid.Character.Blackfeather;

                    voices[0] = companions[0];

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[0], eventVectors[1] * 64);

                    companions[0].netDirection.Set(3);

                    companions[0].eventName = eventId;

                    break;

                case 2:

                    companions[0].TargetEvent(0, eventVectors[2] * 64, true);

                    DialogueCue(1);

                    break;

                case 5:

                    companions[0].ResetActives();

                    companions[0].netDirection.Set(3);

                    companions[0].netAlternative.Set(3);

                    DialogueCue(2);

                    break;

                case 6:
                    
                    companions[0].ResetActives();

                    companions[0].netDirection.Set(1);

                    companions[0].netAlternative.Set(1);

                    break;

                case 7:

                    companions[0].ResetActives();

                    companions[0].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                case 8:

                    DialogueCue(3);

                    break;

                case 10:

                    Wisps wispNew = new();

                    wispNew.EventSetup(eventVectors[101] * 64, Rite.eventWisps);

                    wispNew.eventLocked = true;

                    wispNew.EventActivate();

                    wispNew.WispArray();

                    (location as Clearing).ambientDarkness = true;

                    break;

                case 11:

                    companions[0].doEmote(8);

                    break;

                case 12:

                    companions[0].ResetActives(true);

                    break;

                case 14:

                    companions[0].LookAtTarget(Game1.player.Position, true);

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

                    /*Vector2 mistCorner = eventVectors[101] * 64 - new Vector2(320,320);

                    List<int> cornersX = new() { 0, 6, };

                    List<int> cornersY = new() { 0, 4, };

                    for (int i = 0; i < 9; i++)
                    {

                        for (int j = 0; j < 6; j++)
                        {

                            if (cornersX.Contains(i) && cornersY.Contains(j))
                            {
                                continue;
                            }

                            Vector2 mistVector = mistCorner + new Vector2(i * 72, j * 72);

                            TemporaryAnimatedSprite mistSprite = new TemporaryAnimatedSprite(0, 40000f,1,1, mistVector, false, false)
                            {
                                sourceRect = new Microsoft.Xna.Framework.Rectangle(88, 1779, 30, 30),
                                sourceRectStartingPos = new Vector2(88, 1779),
                                texture = Game1.mouseCursors,
                                motion = new Vector2(-0.0004f + Mod.instance.randomIndex.Next(5) * 0.0002f, -0.0004f + Mod.instance.randomIndex.Next(5) * 0.0002f),
                                scale = 5f,
                                layerDepth = 991f,
                                timeBasedMotion = true,
                                alpha = 0.5f,
                                color = new Microsoft.Xna.Framework.Color(0.75f, 0.75f, 1f, 1f),
                            };

                            location.temporarySprites.Add(mistSprite);

                            animations.Add(mistSprite);

                        }

                    }

                    TemporaryAnimatedSprite cloudAnimation = Mod.instance.iconData.CreateImpact(

                        location,
                        eventVectors[101] * 64,
                        IconData.impacts.spiral,
                        11f,
                        new() { interval = 125f, loops = 40, flip = true, alpha = 0.1f, layer = mistCorner.Y / 10000 }
                     );

                    animations.Add(cloudAnimation);*/

                    break;

                case 102:

                    Texture2D JunimoHuts = Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Buildings", "Junimo Hut"));

                    int HutOffset = (int)Game1.season;

                    List<Vector2> hutLocations = new()
                    {

                        eventVectors[102],
                        eventVectors[103],
                        eventVectors[104],
                        eventVectors[105],
                    };

                    foreach(Vector2 hut in hutLocations)
                    {

                        TemporaryAnimatedSprite hutSprite = new TemporaryAnimatedSprite(0, 65000f, 1, 1, hut*64, false, false)
                        {
                            sourceRect = new Microsoft.Xna.Framework.Rectangle(0 + HutOffset * 48, 0, 48, 64),
                            sourceRectStartingPos = new Vector2(0 + HutOffset*48,0),
                            texture =JunimoHuts,
                            scale = 3f,
                            layerDepth = (hut.Y+2) * 64 / 10000,
                            alpha = 0.5f,
                        };

                        location.temporarySprites.Add(hutSprite);

                        animations.Add(hutSprite);

                        Vector2 burnVector = hut * 64 + new Vector2(32,96);

                        List<TemporaryAnimatedSprite> embers = Mod.instance.iconData.EmberConstruct(location, IconData.schemes.stars, burnVector, 4f, 4, 999f);

                        animations.AddRange(embers);

                        TemporaryAnimatedSprite lightCircle = new(23, 200f, 5, 9, burnVector, false, Game1.random.NextDouble() < 0.5)
                        {
                            texture = Game1.mouseCursors,
                            light = true,
                            lightRadius = 3f,
                            lightcolor = Color.Black,
                            Parent = location,
                        };

                        location.temporarySprites.Add(lightCircle);

                        animations.Add(lightCircle);


                    }

                    companions[0].LookAtTarget(eventVectors[1] * 64,true);

                    break;

                case 103:

                    DialogueCue(101);

                    break;

                case 105:

                    companions[1] = new Crowmother(CharacterHandle.characters.Crowmother);

                    companions[1].fadeOut = 0.8f;

                    voices[1] = companions[1];

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[1].currentLocation.Name != location.Name)
                    {
                        companions[1].currentLocation.characters.Remove(companions[1]);

                        companions[1].currentLocation = location;

                        companions[1].currentLocation.characters.Add(companions[1]);

                    }

                    companions[1].Position = eventVectors[106] * 64;

                    companions[0].LookAtTarget(companions[1].Position, true);

                    Mod.instance.iconData.ImpactIndicator(location, companions[1].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { color = Microsoft.Xna.Framework.Color.Magenta,});

                    // Raven Familiar
                    companions[4] = new Flyer(CharacterHandle.characters.Raven);

                    companions[4].setScale = 3f;

                    companions[4].fadeOut = 0.7f;

                    companions[4].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[4].currentLocation.Name != location.Name)
                    {
                        companions[4].currentLocation.characters.Remove(companions[4]);

                        companions[4].currentLocation = location;

                        companions[4].currentLocation.characters.Add(companions[4]);

                    }

                    companions[4].Position = (eventVectors[106] - new Vector2(1,3)) * 64;

                    companions[4].LookAtTarget(companions[1].Position, true);

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    Mod.instance.iconData.ImpactIndicator(location, companions[4].Position - new Vector2(0, 64), IconData.impacts.plume, 2f, new() { color = Microsoft.Xna.Framework.Color.Black, });

                    break;

                case 106:

                    DialogueCue(102);

                    companions[1].ResetActives();

                    companions[1].netDirection.Set(3);

                    companions[1].netAlternative.Set(3);
                
                    break;

                case 108:

                    companions[1].ResetActives();

                    companions[1].netDirection.Set(1);

                    companions[1].netAlternative.Set(1);

                    DialogueCue(104);
                    
                    break;

                case 110:

                    companions[2] = new LadyBeyond(CharacterHandle.characters.LadyBeyond);

                    companions[2].fadeOut = 0.8f;

                    voices[2] = companions[2];

                    companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[2].currentLocation.Name != location.Name)
                    {

                        companions[2].currentLocation.characters.Remove(companions[2]);

                        companions[2].currentLocation = location;

                        companions[2].currentLocation.characters.Add(companions[2]);

                    }

                    companions[2].Position = eventVectors[107] * 64;

                    Mod.instance.iconData.ImpactIndicator(location, companions[2].Position- new Vector2(0,64), IconData.impacts.plume, 4f, new() { color = Microsoft.Xna.Framework.Color.Cyan, });

                    companions[1].LookAtTarget(companions[2].Position, true);

                    companions[0].LookAtTarget(companions[2].Position, true);

                    companions[4].LookAtTarget(companions[2].Position, true);

                    break;

                case 111:

                    DialogueCue(105);

                    break;

                case 112:

                    companions[2].netSpecial.Set((int)Character.Character.specials.liftup);

                    companions[2].specialTimer = 120;

                    break;

                case 114:

                    List<Vector2> boltLocations = new()
                    {

                        eventVectors[102] + new Vector2(1,3),
                        eventVectors[103] + new Vector2(1,3),
                        eventVectors[104] + new Vector2(1,3),
                        eventVectors[105] + new Vector2(1,3),
                    };

                    foreach (Vector2 hut in boltLocations)
                    {

                        Mod.instance.iconData.ImpactIndicator(location, hut * 64, IconData.impacts.puff, 4f, new() { color = Microsoft.Xna.Framework.Color.DarkCyan, });
                        Mod.instance.iconData.ImpactIndicator(location, hut * 64, IconData.impacts.plume, 4f, new() { color = Microsoft.Xna.Framework.Color.Cyan, });
                    }

                    companions[1].ResetActives();

                    companions[1].LookAtTarget(eventVectors[102] * 64, true);

                    companions[2].netSpecial.Set((int)Character.Character.specials.liftdown);

                    companions[2].specialTimer = 120;

                    break;

                case 115:

                    companions[1].LookAtTarget(companions[2].Position);

                    companions[2].LookAtTarget(companions[1].Position);

                    DialogueCue(106);

                    break;

                case 118:

                    DialogueCue(107);

                    companions[2].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                case 121:

                    companions[1].TargetEvent(0,eventVectors[109] * 64, true);

                    DialogueCue(108);

                    companions[3] = new Paladin(CharacterHandle.characters.Paladin);

                    companions[3].fadeOut = 0.8f;

                    voices[3] = companions[3];

                    companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[3].currentLocation.Name != location.Name)
                    {

                        companions[3].currentLocation.characters.Remove(companions[3]);

                        companions[3].currentLocation = location;

                        companions[3].currentLocation.characters.Add(companions[3]);

                    }

                    companions[3].Position = eventVectors[108] * 64;

                    companions[3].TargetEvent(0, eventVectors[101] * 64, true);

                    companions[4].LookAtTarget(companions[3].Position, true);

                    break;

                case 124:

                    companions[1].ResetActives();

                    companions[1].LookAtTarget(companions[3].Position, true);

                    companions[2].ResetActives();

                    companions[2].LookAtTarget(companions[3].Position, true);

                    companions[4].LookAtTarget(companions[3].Position, true);

                    DialogueCue(109);

                    break;

                case 127:

                    DialogueCue(110);

                    break;

                case 130:

                    DialogueCue(111);

                    companions[3].ResetActives(true);

                    companions[3].LookAtTarget(companions[1].Position, true);

                    companions[3].netSpecial.Set((int)Character.Character.specials.gesture);

                    companions[3].specialTimer = 60;
                    
                    break;

                case 133:
                    companions[4].Position = (eventVectors[106] - new Vector2(1, 3)) * 64;

                    DialogueCue(112);

                    companions[3].ResetActives(true);

                    companions[3].LookAtTarget(companions[2].Position, true);

                    companions[3].netSpecial.Set((int)Character.Character.specials.gesture);

                    companions[3].specialTimer = 60;

                    break;

                case 136:

                    DialogueCue(113);

                    companions[3].ResetActives(true);

                    companions[3].LookAtTarget(Game1.player.Position, true);

                    break;

                case 141:

                    for(int i = 1; i < 5; i++)
                    {

                        Mod.instance.iconData.ImpactIndicator(location, companions[i].Position - new Vector2(0, 64), IconData.impacts.smoke, 4f, new());

                        location.characters.Remove(companions[i]);

                        companions.Remove(i);

                        voices.Remove(i);

                    }

                    StopTrack();

                    Cast.Bones.Corvids.RemoveCorvids();

                    break;

                case 142:

                    DialogueCue(114);

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    break;

                case 145:

                    DialogueLoad(companions[0], 2);

                    break;

            }

        }

        public void ScenePartThree()
        {

            switch (activeCounter)
            {
                case 201:

                    companions[0].TargetEvent(0, eventVectors[201] * 64, true);

                    Mod.instance.eventRegister[Rite.eventWisps].eventAbort = true;

                    break;

                case 202:

                    location = Mod.instance.locations[LocationData.druid_engineum_name];

                    Game1.warpFarmer(location.Name, (int)eventVectors[202].X, (int)eventVectors[202].Y, 0);

                    CharacterMover.Warp(location, companions[0], eventVectors[203] * 64, false);

                    Game1.stopMusicTrack(MusicContext.Default);

                    companions[0].TargetEvent(0, eventVectors[204] * 64, true);

                    break;

                case 204:

                    DialogueCue(201);

                    break;

                case 207:

                    DialogueCue(202);

                    companions[0].LookAtTarget(Game1.player.Position);

                    companions[0].netSpecial.Set((int)Character.Character.specials.gesture);

                    companions[0].specialTimer = 60;

                    break;

                case 210:

                    DialogueCue(203);

                    break;

                case 211:

                    /*Vector2 mistCorner = eventVectors[205] * 64 - new Vector2(320, 320);

                    List<int> cornersX = new() { 0, 6, };

                    List<int> cornersY = new() { 0, 4, };

                    for (int i = 0; i < 9; i++)
                    {

                        for (int j = 0; j < 6; j++)
                        {

                            if (cornersX.Contains(i) && cornersY.Contains(j))
                            {
                                continue;
                            }

                            Vector2 mistVector = mistCorner + new Vector2(i * 72, j * 72);

                            TemporaryAnimatedSprite mistSprite = new TemporaryAnimatedSprite(0, 50000f, 1, 1, mistVector, false, false)
                            {
                                sourceRect = new Microsoft.Xna.Framework.Rectangle(88, 1779, 30, 30),
                                sourceRectStartingPos = new Vector2(88, 1779),
                                texture = Game1.mouseCursors,
                                motion = new Vector2(-0.0004f + Mod.instance.randomIndex.Next(5) * 0.0002f, -0.0004f + Mod.instance.randomIndex.Next(5) * 0.0002f),
                                scale = 5f,
                                layerDepth = 991f,
                                timeBasedMotion = true,
                                alpha = 0.5f,
                                color = new Microsoft.Xna.Framework.Color(0.75f, 0.75f, 1f, 1f),
                            };

                            location.temporarySprites.Add(mistSprite);

                            animations.Add(mistSprite);

                        }

                    }

                    TemporaryAnimatedSprite cloudAnimation = Mod.instance.iconData.CreateImpact(

                        location,
                        eventVectors[205] * 64,
                        IconData.impacts.spiral,
                        11f,
                        new() { interval = 125f, loops = 50, flip = true, alpha = 0.1f, layer = mistCorner.Y / 10000 }
                     );

                    animations.Add(cloudAnimation);*/

                    break;

                case 212:

                    SetTrack("spirits_eve");

                    companions[1] = new Crowmother(CharacterHandle.characters.Crowmother);

                    companions[1].fadeOut = 0.8f;

                    voices[1] = companions[1];

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[1].currentLocation.Name != location.Name)
                    {
                        companions[1].currentLocation.characters.Remove(companions[1]);

                        companions[1].currentLocation = location;

                        companions[1].currentLocation.characters.Add(companions[1]);

                    }

                    companions[1].Position = eventVectors[206] * 64;

                    Mod.instance.iconData.ImpactIndicator(location, companions[1].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { color = Microsoft.Xna.Framework.Color.Magenta, });

                    companions[1].LookAtTarget(eventVectors[207] * 64, true);

                    companions[0].LookAtTarget(companions[1].Position, true);

                    // Raven Familiar
                    companions[3] = new Flyer(CharacterHandle.characters.Raven);

                    companions[3].setScale = 3f;

                    companions[3].fadeOut = 0.8f;

                    companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[3].currentLocation.Name != location.Name)
                    {
                        companions[3].currentLocation.characters.Remove(companions[3]);

                        companions[3].currentLocation = location;

                        companions[3].currentLocation.characters.Add(companions[3]);

                    }

                    companions[3].Position = (eventVectors[206] - new Vector2(2, 1)) * 64;

                    companions[3].LookAtTarget(companions[1].Position, true);

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    Mod.instance.iconData.ImpactIndicator(location, companions[3].Position - new Vector2(0, 64), IconData.impacts.plume, 2f, new() { color = Microsoft.Xna.Framework.Color.Black, });

                    break;

                case 213:

                    DialogueCue(204);

                    companions[1].netSpecial.Set((int)Character.Character.specials.gesture); companions[1].specialTimer = 60;

                    break;

                case 216:

                    DialogueCue(205);

                    companions[1].netSpecial.Set((int)Character.Character.specials.gesture); companions[1].specialTimer = 60;

                    break;

                case 218:

                    companions[2] = new LadyBeyond(CharacterHandle.characters.LadyBeyond);

                    companions[2].fadeOut = 0.8f;

                    voices[2] = companions[2];

                    companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[2].currentLocation.Name != location.Name)
                    {

                        companions[2].currentLocation.characters.Remove(companions[2]);

                        companions[2].currentLocation = location;

                        companions[2].currentLocation.characters.Add(companions[2]);

                    }

                    companions[2].Position = eventVectors[207] * 64;

                    Mod.instance.iconData.ImpactIndicator(location, companions[2].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { color = Microsoft.Xna.Framework.Color.Cyan, });

                    companions[2].LookAtTarget(companions[1].Position, true);

                    companions[0].LookAtTarget(companions[2].Position, true);

                    break;

                case 219:

                    DialogueCue(206);

                    companions[2].netSpecial.Set((int)Character.Character.specials.gesture); companions[2].specialTimer = 60;

                    break;

                case 222:

                    DialogueCue(207);

                    break;

                case 225:

                    DialogueCue(208);

                    companions[1].netSpecial.Set((int)Character.Character.specials.gesture); companions[1].specialTimer = 60;

                    break;

                case 228:

                    DialogueCue(209);

                    break;

                case 231:

                    DialogueCue(210);

                    companions[1].ResetActives(true);

                    companions[1].TargetEvent(0, eventVectors[208] * 64, true);

                    break;

                case 234:

                    DialogueCue(211);

                    companions[2].ResetActives(true);

                    companions[2].netSpecial.Set((int)Character.Character.specials.gesture); companions[2].specialTimer = 60;

                    break;

                case 237:

                    DialogueCue(212);

                    companions[1].ResetActives(true);

                    companions[1].netSpecial.Set((int)Character.Character.specials.gesture); companions[1].specialTimer = 60;

                    break;

                case 240:

                    DialogueCue(213);

                    companions[2].ResetActives();

                    companions[2].netDirection.Set(1);

                    companions[2].netAlternative.Set(1);

                    break;

                case 243:

                    DialogueCue(214);

                    companions[1].ResetActives(true);

                    companions[1].netSpecial.Set((int)Character.Character.specials.gesture); companions[1].specialTimer = 60;

                    break;

                case 246:

                    DialogueCue(215);

                    companions[2].ResetActives();

                    companions[2].LookAtTarget(companions[1].Position, true);

                    break;

                case 249:

                    DialogueCue(216);

                    break;

                case 252:

                    DialogueCue(217);

                    break;

                case 255:

                    DialogueCue(218);

                    break;

                case 258:

                    DialogueCue(219);

                    companions[1].ResetActives(true);

                    companions[1].TargetEvent(0, eventVectors[209] * 64, true);

                    break;

                case 261:

                    for (int i = 1; i < 4; i++)
                    {
                        
                        Mod.instance.iconData.ImpactIndicator(location, companions[i].Position - new Vector2(0, 64), IconData.impacts.smoke, 4f, new());

                        location.characters.Remove(companions[i]);

                        companions.Remove(i);

                        voices.Remove(i);

                    }

                    StopTrack();

                    DialogueCue(220);

                    break;

                case 264:

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueLoad(companions[0], 3);

                    break;

            }

        }

        public void ScenePartFour()
        {


            switch (activeCounter)
            {

                case 301:

                    companions[0].TargetEvent(0, eventVectors[301] * 64, true);

                    break;

                case 303:

                    Game1.stopMusicTrack(MusicContext.Default);

                    location = Mod.instance.locations[LocationData.druid_atoll_name];

                    Game1.warpFarmer(location.Name, (int)eventVectors[302].X, (int)eventVectors[302].Y, 1);

                    CharacterMover.Warp(location, companions[0], eventVectors[303] * 64, false);

                    companions[0].TargetEvent(0, eventVectors[304] * 64, true);

                    break;

                case 304:

                    DialogueCue(301);

                    break;

                case 307:

                    companions[0].ResetActives(true);

                    companions[0].LookAtTarget(Game1.player.Position);

                    DialogueCue(302);

                    break;

                case 310:

                    companions[0].ResetActives();

                    companions[0].TargetEvent(0, eventVectors[305] * 64, true);

                    DialogueCue(303);

                    break;

                case 312:

                    companions[0].netSpecial.Set((int)Character.Character.specials.liftup);

                    companions[0].specialTimer = 180;

                    break;

                case 313:

                    DialogueCue(304);

                    break;

                case 314:

                    companions[0].netSpecial.Set((int)Character.Character.specials.liftdown);

                    companions[0].specialTimer = 180;

                    break;

                case 316:

                    DialogueCue(305);

                    break;

                case 318:

                    Wisps wispNew = new();

                    wispNew.EventSetup(eventVectors[307] * 64, Rite.eventWisps);

                    wispNew.eventLocked = true;

                    wispNew.EventActivate();

                    wispNew.WispArray();

                    (location as Atoll).ambientDarkness = true;

                    break;

                case 319:

                    companions[0].LookAtTarget(eventVectors[306] * 64, true);

                    SetTrack("night_market");

                    // First Farmer
                    companions[4] = new FirstFarmer(CharacterHandle.characters.FirstFarmer);

                    companions[4].fadeOut = 0.8f;

                    voices[4] = companions[4];

                    companions[4].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[4].currentLocation.Name != location.Name)
                    {
                        companions[4].currentLocation.characters.Remove(companions[4]);

                        companions[4].currentLocation = location;

                        companions[4].currentLocation.characters.Add(companions[4]);

                    }

                    companions[4].Position = eventVectors[308] * 64;

                    Mod.instance.iconData.ImpactIndicator(location, companions[4].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { color = Microsoft.Xna.Framework.Color.Green, });


                    // Justiciar
                    companions[5] = new Justiciar(CharacterHandle.characters.Justiciar);

                    companions[5].fadeOut = 0.8f;

                    voices[5] = companions[5];

                    companions[5].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[5].currentLocation.Name != location.Name)
                    {
                        companions[5].currentLocation.characters.Remove(companions[5]);

                        companions[5].currentLocation = location;

                        companions[5].currentLocation.characters.Add(companions[5]);

                    }

                    companions[5].Position = eventVectors[309] * 64;

                    Mod.instance.iconData.ImpactIndicator(location, companions[5].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { color = Microsoft.Xna.Framework.Color.Orange,  });


                    // Reaper
                    companions[6] = new Thanatoshi(CharacterHandle.characters.Reaper);

                    companions[6].fadeOut = 0.8f;

                    voices[6] = companions[6];

                    companions[6].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[6].currentLocation.Name != location.Name)
                    {
                        companions[6].currentLocation.characters.Remove(companions[6]);

                        companions[6].currentLocation = location;

                        companions[6].currentLocation.characters.Add(companions[6]);

                    }

                    companions[6].Position = eventVectors[310] * 64;

                    Mod.instance.iconData.ImpactIndicator(location, companions[6].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { color = Microsoft.Xna.Framework.Color.Gray, });


                    // Seafarers
                    companions[7] = new Seafarer(CharacterHandle.characters.Seafarer);

                    companions[7].fadeOut = 0.8f;

                    voices[7] = companions[7];

                    companions[7].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[7].currentLocation.Name != location.Name)
                    {
                        companions[7].currentLocation.characters.Remove(companions[7]);

                        companions[7].currentLocation = location;

                        companions[7].currentLocation.characters.Add(companions[7]);

                    }

                    companions[7].Position = eventVectors[311] * 64;

                    Mod.instance.iconData.ImpactIndicator(location, companions[7].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new());

                    // Seafarers
                    companions[8] = new Seafarer(CharacterHandle.characters.Seafarer);

                    companions[8].fadeOut = 0.8f;

                    voices[8] = companions[8];

                    companions[8].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[8].currentLocation.Name != location.Name)
                    {
                        companions[8].currentLocation.characters.Remove(companions[8]);

                        companions[8].currentLocation = location;

                        companions[8].currentLocation.characters.Add(companions[8]);

                    }

                    companions[8].Position = eventVectors[312] * 64;

                    Mod.instance.iconData.ImpactIndicator(location, companions[8].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new());

                    // Seafarers
                    companions[9] = new Seafarer(CharacterHandle.characters.Seafarer);

                    companions[9].fadeOut = 0.8f;

                    voices[9] = companions[9];

                    companions[9].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    if (companions[9].currentLocation.Name != location.Name)
                    {
                        companions[9].currentLocation.characters.Remove(companions[9]);

                        companions[9].currentLocation = location;

                        companions[9].currentLocation.characters.Add(companions[9]);

                    }

                    companions[9].Position = eventVectors[313] * 64;

                    Mod.instance.iconData.ImpactIndicator(location, companions[9].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new());


                    // Stage set
                    companions[0].LookAtTarget(companions[4].Position, true);

                    companions[4].LookAtTarget(companions[6].Position, true);

                    companions[5].LookAtTarget(companions[4].Position, true);

                    companions[6].LookAtTarget(companions[4].Position, true);

                    companions[7].LookAtTarget(companions[4].Position, true);

                    companions[8].LookAtTarget(companions[4].Position, true);

                    companions[9].LookAtTarget(companions[4].Position, true);

                    break;

                case 320:

                    DialogueCue(306);

                    companions[5].netSpecial.Set((int)Character.Character.specials.gesture);

                    companions[5].specialTimer = 90;
                    break;

                case 323:

                    DialogueCue(307);

                    companions[4].netSpecial.Set((int)Character.Character.specials.gesture);

                    companions[4].specialTimer = 90;
                    break;

                case 326:

                    DialogueCue(308);

                    companions[4].netSpecial.Set((int)Character.Character.specials.point);

                    companions[4].specialTimer = 90;

                    break;

                case 329:

                    DialogueCue(309);

                    companions[6].LookAtTarget(companions[4].Position, true);

                    companions[6].netSpecial.Set((int)Character.Character.specials.point);

                    companions[6].specialTimer = 90;

                    break;

                case 332:

                    DialogueCue(310);

                    companions[5].netSpecial.Set((int)Character.Character.specials.gesture);

                    companions[5].specialTimer = 90;

                    break;

                case 335:

                    DialogueCue(311);

                    break;

                case 338:

                    DialogueCue(312);

                    break;

                case 341:

                    DialogueCue(313);
                    companions[4].netIdle.Set((int)Character.Character.idles.alert);
                    companions[4].LookAtTarget(companions[6].Position, true);
                    break;

                case 344:

                    DialogueCue(314);
                    companions[6].netIdle.Set((int)Character.Character.idles.alert);
                    companions[6].LookAtTarget(companions[4].Position, true);
                    companions[7].netIdle.Set((int)Character.Character.idles.alert);
                    companions[7].LookAtTarget(companions[4].Position, true);
                    companions[8].netIdle.Set((int)Character.Character.idles.alert);
                    companions[8].LookAtTarget(companions[4].Position, true);
                    companions[9].netIdle.Set((int)Character.Character.idles.alert);
                    companions[9].LookAtTarget(companions[4].Position, true);
                    break;

                case 347:

                    DialogueCue(315);

                    companions[5].LookAtTarget(companions[6].Position, true);

                    companions[5].netSpecial.Set((int)Character.Character.specials.gesture);

                    companions[5].specialTimer = 60;

                    break;

                case 350:

                    DialogueCue(316);

                    companions[5].LookAtTarget(companions[4].Position, true);

                    companions[5].netSpecial.Set((int)Character.Character.specials.point);

                    companions[5].specialTimer = 90;

                    break;

                case 351:

                    companions[4].ResetActives();

                    companions[4].LookAtTarget(companions[5].Position, true);

                    companions[4].TargetEvent(401, companions[5].Position + new Vector2(64, 64), true);

                    companions[4].pathActive = StardewDruid.Character.Character.pathing.monster;

                    companions[4].SetDash(companions[5].Position, true);

                    break;

                case 352:

                    companions[6].doEmote(16);
                    companions[6].LookAtTarget(companions[4].Position, true);
                    companions[7].doEmote(16);
                    companions[7].LookAtTarget(companions[4].Position, true);
                    companions[8].doEmote(16);
                    companions[8].LookAtTarget(companions[4].Position, true);
                    companions[9].doEmote(16);
                    companions[9].LookAtTarget(companions[4].Position, true);

                    break;

                case 353:

                    companions[5].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[5].doEmote(36);

                    companions[4].LookAtTarget(companions[6].Position, true);

                    break;

                case 356:

                    DialogueCue(317);

                    break;

                case 359:

                    DialogueCue(318);

                    companions[6].ResetActives();

                    companions[6].TargetEvent(402, companions[4].Position + new Vector2(64,0), true);

                    companions[6].pathActive = StardewDruid.Character.Character.pathing.monster;

                    companions[6].SetDash(companions[4].Position, true);

                    location.characters.Remove(companions[5]);

                    Mod.instance.iconData.ImpactIndicator(location, companions[5].Position - new Vector2(0, 64), IconData.impacts.smoke, 4f, new());

                    companions.Remove(5);

                    voices.Remove(5);

                    break;

                case 362:

                    companions[4].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[4].doEmote(36);

                    break;

                case 365:

                    DialogueCue(319);

                    break;

                case 367:

                    Mod.instance.iconData.ImpactIndicator(location, companions[4].Position - new Vector2(0, 64), IconData.impacts.smoke, 4f, new());

                    location.characters.Remove(companions[4]);

                    companions.Remove(4);

                    voices.Remove(4);

                    break;

                case 371:

                    DialogueCue(320);
                    companions[6].ResetActives();
                    companions[7].ResetActives();
                    companions[7].LookAtTarget(companions[6].Position, true);
                    companions[8].ResetActives();
                    companions[8].LookAtTarget(companions[6].Position, true);
                    companions[9].ResetActives();
                    companions[9].LookAtTarget(companions[6].Position, true);
                    break;

                case 374:

                    DialogueCue(321);

                    companions[7].ResetActives();
                    companions[7].TargetEvent(0, companions[7].Position - new Vector2(192, 0), true);
                    companions[8].ResetActives();
                    companions[8].TargetEvent(0, companions[8].Position - new Vector2(192, 0), true);
                    companions[9].ResetActives();
                    companions[9].TargetEvent(0, companions[9].Position - new Vector2(192, 0), true);

                    break;

                case 376:

                    DialogueCue(322);
                    companions[6].ResetActives(true);
                    companions[6].LookAtTarget(companions[7].Position, true);
                    companions[6].netSpecial.Set((int)Character.Character.specials.point);
                    companions[6].specialTimer = 90;
                    companions[7].ResetActives();
                    companions[7].doEmote(16);
                    companions[8].ResetActives();
                    companions[8].doEmote(16);
                    companions[9].ResetActives();
                    companions[9].doEmote(16);

                    break;

                case 378:

                    companions[7].ResetActives();
                    companions[7].LookAtTarget(companions[6].Position, true);
                    companions[8].ResetActives();
                    companions[8].LookAtTarget(companions[6].Position, true);
                    companions[9].ResetActives();
                    companions[9].LookAtTarget(companions[6].Position, true);

                    break;

                case 380:

                    DialogueCue(323);

                    break;

                case 383:

                    DialogueCue(324);

                    break;

                case 386:

                    DialogueCue(325);

                    companions[7].ResetActives();
                    companions[7].doEmote(16);
                    companions[8].ResetActives();
                    companions[8].doEmote(16);
                    companions[9].ResetActives();
                    companions[9].doEmote(16);

                    break;

                case 389:

                    for (int i = 6; i < 10; i++)
                    {

                        Mod.instance.iconData.ImpactIndicator(location, companions[i].Position - new Vector2(0, 64), IconData.impacts.smoke, 4f, new());

                        location.characters.Remove(companions[i]);

                        companions.Remove(i);

                        voices.Remove(i);

                    }

                    break;

                case 390:

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueLoad(companions[0], 4);

                    break;

            }

        }

        public void ScenePartFive()
        {

            switch (activeCounter)
            {

                case 401:

                    companions[0].TargetEvent(0, eventVectors[401] * 64, true);

                    DialogueCue(401);

                    break;

                case 403:

                    companions[0].TargetEvent(0, eventVectors[402] * 64, true);

                    break;

                case 404:

                    companions[0].ResetActives(true);

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueCue(402);

                    break;

                case 407:

                    companions[0].doEmote(8);

                    break;

                case 409:

                    DialogueSetups(companions[0], 5);

                    break;


            }


        }

        public void ScenePartSix()
        {

            if (bosses.ContainsKey(0))
            {

                if (!ModUtility.MonsterVitals(bosses[0], location))
                {

                    bosses[0].netWoundedActive.Set(true);

                }

                if (!location.characters.Contains(bosses[0]))
                {

                    location.characters.Add(bosses[0]);

                    bosses[0].currentLocation = location;

                }

            }

            switch (activeCounter)
            {

                case 501:

                    StopTrack();

                    DialogueClear(0);

                    bosses[0] = new DarkWitch(ModUtility.PositionToTile(companions[0].Position), Mod.instance.CombatDifficulty(), "LadyBeyond");

                    location.characters.Remove(companions[0]);

                    bosses[0].netScheme.Set(2);

                    bosses[0].SetMode(3);

                    bosses[0].netHaltActive.Set(true);

                    bosses[0].idleTimer = 60;

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    bosses[0].setWounded = true;

                    voices[2] = bosses[0];

                    EventDisplay bossBar = BossBar(0, 2);

                    bossBar.colour = Microsoft.Xna.Framework.Color.RoyalBlue;

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { color = Microsoft.Xna.Framework.Color.RoyalBlue, });

                    SetTrack("cowboy_boss");

                    (Mod.instance.eventRegister[Rite.eventWisps] as Wisps).WispArray();
                    
                    break;

                case 502:

                    DialogueCue(501);

                    break;

                case 505:

                    DialogueCue(502);

                    break;

                case 508:

                    DialogueCue(503);

                    Cast.Bones.Corvids.SummonRaven();

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 511:

                    DialogueCue(504);

                    Mod.instance.characters[CharacterHandle.characters.Raven].ResetActives();

                    Mod.instance.characters[CharacterHandle.characters.Raven].SmashAttack(bosses[0]);

                    break;

                case 514:

                    DialogueCue(505);

                    Cast.Bones.Corvids.SummonCrow();

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 517:

                    DialogueCue(506);

                    Mod.instance.characters[CharacterHandle.characters.Crow].ResetActives();

                    Mod.instance.characters[CharacterHandle.characters.Crow].SmashAttack(bosses[0]);

                    break;

                case 520:

                    DialogueCue(507);

                    Cast.Bones.Corvids.SummonRook();

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 523:

                    DialogueCue(508);

                    Mod.instance.characters[CharacterHandle.characters.Rook].ResetActives();

                    Mod.instance.characters[CharacterHandle.characters.Rook].SmashAttack(bosses[0]);

                    break;

                case 526:

                    DialogueCue(509);

                    Cast.Bones.Corvids.SummonMagpie();

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 529:

                    DialogueCue(510);

                    Mod.instance.characters[CharacterHandle.characters.Magpie].ResetActives();

                    Mod.instance.characters[CharacterHandle.characters.Magpie].SmashAttack(bosses[0]);

                    break;

                case 530:

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 531:

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 532:

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    DialogueCue(511);

                    break;

                case 533:

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 534:

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 535:

                    companions[0].Position = bosses[0].Position;

                    voices[2] = companions[0];

                    location.characters.Add(companions[0]);

                    location.characters.Remove(bosses[0]);

                    bosses.Clear();

                    companions[0].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[0].doEmote(28);

                    Mod.instance.iconData.ImpactIndicator(location, companions[0].Position - new Vector2(0, 64), IconData.impacts.plume, 4f, new() { color = Microsoft.Xna.Framework.Color.RoyalBlue, });

                    break;

                case 537:

                    StopTrack();

                    companions[0].doEmote(28);

                    break;

                case 539:

                    DialogueLoad(companions[0], 6);
                    
                    break;

                case 550:

                    activeCounter = 600;

                    break;

            }

        }

        public void ScenePartSeven()
        {

            switch (activeCounter)
            {

                case 601:

                    companions[0].netIdle.Set((int)Character.Character.idles.kneel);

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

                            Mod.instance.trackers[corvid].linger = 640;

                            Mod.instance.trackers[corvid].lingerSpot = companions[0].Position;

                        }

                    }

                    DialogueCue(601);

                    break;

                case 602:

                    companions[0].LookAtTarget(Mod.instance.characters[CharacterHandle.characters.Raven].Position, true);

                    Mod.instance.characters[CharacterHandle.characters.Raven].doEmote(20);

                    break;

                case 604:

                    companions[0].LookAtTarget(Mod.instance.characters[CharacterHandle.characters.Crow].Position, true);

                    Mod.instance.characters[CharacterHandle.characters.Crow].doEmote(20);

                    DialogueCue(602);

                    break;

                case 606:

                    companions[0].LookAtTarget(Mod.instance.characters[CharacterHandle.characters.Rook].Position, true);

                    Mod.instance.characters[CharacterHandle.characters.Rook].doEmote(20);

                    break;

                case 607:

                    DialogueCue(603);

                    break;

                case 608:

                    companions[0].LookAtTarget(Mod.instance.characters[CharacterHandle.characters.Magpie].Position, true);

                    Mod.instance.characters[CharacterHandle.characters.Magpie].doEmote(20);

                    break;

                case 610:

                    DialogueCue(604);

                    break;

                case 611:

                    companions[0].ResetActives(true);

                    break;

                case 612:

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    companions[0].netSpecial.Set((int)Character.Character.specials.gesture);

                    companions[0].specialTimer = 60;

                    break;

                case 613:

                    DialogueCue(605);

                    eventComplete = true;

                    break;

            }

        }

        public override void EventScene(int index)
        {

            switch (index)
            {
                case 401:

                    companions[4].netIdle.Set((int)Character.Character.idles.alert);

                    break;

                case 402:

                    companions[6].netIdle.Set((int)Character.Character.idles.alert);

                    break;


            }

        }


    }

}