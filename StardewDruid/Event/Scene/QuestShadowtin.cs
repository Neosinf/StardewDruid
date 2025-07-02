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
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Timers;


namespace StardewDruid.Event.Scene
{
    public class QuestShadowtin : EventHandle
    {

        public Dictionary<int, Vector2> eventVectors = new()
        {

            // event trigger
            [0] = new Vector2(89, 24),
            // shadowtin enter forest
            [1] = new Vector2(91, 22),
            // shadowtin wait in forest
            [2] = new Vector2(94, 27),
            // dwarf enter forest
            [3] = new Vector2(104, 26),
            // dwarf walk to meeting
            [4] = new Vector2(95, 26),
            // dwarf leave forest
            [5] = new Vector2(114, 26),

            // warp farmer to clearing
            //[10] = new Vector2(58, 71),
            [10] = new Vector2(32,15),
            // warp companion to clearing
            //[11] = new Vector2(56, 70),
            [11] = new Vector2(35,16),
            // look at access to shrine
            //[12] = new Vector2(49, 70),
            [12] = new Vector2(44, 15),
            // trigger access to shrine
            //[13] = new Vector2(50, 73),
            [13] = new Vector2(47, 13),
            // shrine warp farmer
            [14] = new Vector2(27, 26),
            // shrine warp companion
            [15] = new Vector2(28, 25),
            // shrine look around one 
            [16] = new Vector2(28, 19),
            // move closer to shrine
            [17] = new Vector2(28, 16),
            // voices
            [18] = new Vector2(23, 10),

            // manuscript
            [19] = new Vector2(35, 16),
            // pick up manuscript
            [20] = new Vector2(34, 16),
            // panic 1
            [21] = new Vector2(27, 16),
            // panic 2
            [22] = new Vector2(22, 18),
            // panic 3
            [23] = new Vector2(27, 20),
            // panic 4
            [24] = new Vector2(32, 18),
            // wizard enter shrine
            [25] = new Vector2(29, 25),
            // wizard move into shrine
            [26] = new Vector2(28, 14),
            // wizard battle position
            [27] = new Vector2(30, 18),
            // wizard wound position
            [28] = new Vector2(28, 23),
            // shadowtin leave shrine
            [29] = new Vector2(28, 28),

            // forest cache enter farmer
            [30] = new Vector2(30, 17),
            // forest cache enter shadowtin
            [31] = new Vector2(29, 19),
            // forest cache location
            [32] = new Vector2(27, 22),
            // forest enter rogue
            [33] = new Vector2(26, 17),
            // forest enter goblin
            [34] = new Vector2(28, 16),
            // forest cache enter dwarf
            [35] = new Vector2(27, 16),
            // rogue exit
            [36] = new Vector2(26, 7),
            // dwarf exit
            [37] = new Vector2(29, 10),
            // shadowtin contemplation
            [38] = new Vector2(27, 29),

        };

        public QuestShadowtin()
        {

            mainEvent = true;

            activeLimit = 600;

        }

        public override void EventSetup(string id)
        {

            base.EventSetup(id);

            origin = eventVectors[0] * 64;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            locales = new()
            {
                "Forest",
                LocationHandle.druid_clearing_name,
                LocationHandle.druid_engineum_name,

            };

            origin = eventVectors[0] * 64;

            location.playSound("discoverMineral");

            for(int t = Mod.instance.trackers.Count - 1; t >= 0; t--)
            {

                TrackHandle tracker = Mod.instance.trackers.ElementAt(t).Value;

                Mod.instance.characters[tracker.trackFor].SwitchToMode(Character.Character.mode.home, Game1.player);

            }

        }
        
        public override bool AttemptReset()
        {

            Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.abortTomorrow), 3, true);

            return false;

        }


        public override void EventInterval()
        {

            activeCounter++;

            if (activeCounter < 100)
            {

                ScenePartOne();

                return;

            }

            if (activeCounter < 200)
            {

                ScenePartTwo();

                return;

            }

            if (activeCounter < 300)
            {

                ScenePartThree();

                return;

            }

            if (activeCounter < 400)
            {

                ScenePartFour();

                return;

            }

            if (activeCounter < 500)
            {

                ScenePartFive();

                return;

            }

            if (activeCounter < 600)
            {

                ScenePartSix();

                return;

            }

            if (activeCounter < 700)
            {

                ScenePartSeven();

                return;

            }

            eventComplete = true;

        }

        public void ScenePartOne() {


            switch (activeCounter)
            {

                // Part 1: Thievery

                case 1:

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Shadowtin] as Shadowtin;

                    voices[0] = companions[0];

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[0], eventVectors[1] * 64);

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    companions[0].eventName = eventId;

                    companions[0].TargetEvent(5, eventVectors[2] * 64);

                    DialogueCue(1);

                    break;

                case 4:

                    companions[0].ResetActives();

                    companions[0].netDirection.Set(1);

                    DialogueCue(2);

                    companions[1] = new Dwarf(CharacterHandle.characters.Dwarf);

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[1], eventVectors[3] * 64);

                    voices[1] = companions[1];

                    //companions[1].update(Game1.currentGameTime, location);

                    companions[1].ResetActives();

                    companions[1].eventName = eventId;

                    companions[1].TargetEvent(0, eventVectors[4] * 64, true);

                    break;

                case 7:

                    companions[1].ResetActives(true);

                    companions[1].eventVectors.Clear();

                    companions[1].LookAtTarget(companions[0].Position, true);

                    DialogueCue(3);

                    break;

                case 10:

                    ThrowHandle throwMilk = new(companions[0].Position, companions[1].Position, 186, 0);

                    throwMilk.register();

                    DialogueCue(4);

                    break;

                case 13:

                    ThrowHandle throwDwarfRelic = new(companions[1].Position, companions[0].Position, IconData.relics.wayfinder_dwarf);

                    throwDwarfRelic.register();

                    DialogueCue(5);

                    break;

                case 15:

                    DialogueCue(6);

                    break;

                case 18:

                    companions[1].ResetActives();

                    companions[1].TargetEvent(0, eventVectors[5] * 64, true);

                    DialogueCue(7);

                    break;

                case 21:

                    DialogueCue(8);

                    break;

                case 24:

                    companions[1].currentLocation.characters.Remove(companions[1]);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position, true);

                    DialogueLoad(0, 1);

                    break;

                case 35:

                    activeCounter = 99;

                    break;

            }

        }

        public void ScenePartTwo()
        {

            switch (activeCounter)
            { 

                // =======================  Part 2: Followed?

                case 100:

                    DialogueClear(0);

                    DialogueCue(100);

                    companions[0].TargetEvent(0, companions[0].Position + new Vector2(-512,512), true);

                    break;

                case 103:

                    //Game1.warpFarmer(location.Name, (int)eventVectors[10].X, (int)eventVectors[10].Y, 3);

                    Mod.instance.WarpAllFarmers(LocationHandle.druid_clearing_name, (int)eventVectors[10].X, (int)eventVectors[10].Y, 1);

                    location = Mod.instance.locations[LocationHandle.druid_clearing_name];

                    CharacterMover.Warp(location, companions[0], eventVectors[11] * 64, false);

                    companions[0].ResetActives();

                    companions[0].TargetEvent(0, eventVectors[12] * 64, true);

                    break;

                case 106:

                    DialogueCue(101);

                    break;

                case 107:

                    companions[0].LookAtTarget(eventVectors[13]*64,true);

                    companions[0].netSpecial.Set((int)Character.Character.specials.invoke);

                    companions[0].specialTimer = 60;

                    (location as Clearing).OpenAccess();

                    break;

                case 109:

                    DialogueCue(102);

                    companions[0].TargetEvent(0, eventVectors[13] * 64, true);

                    break;

                case 111:

                    location = Mod.instance.locations[LocationHandle.druid_engineum_name];

                    Mod.instance.WarpAllFarmers(location.Name, (int)eventVectors[14].X, (int)eventVectors[14].Y, 0);

                    CharacterMover.Warp(location, companions[0], eventVectors[15] * 64, false);

                    Game1.stopMusicTrack(MusicContext.Default);

                    companions[0].TargetEvent(0, eventVectors[16] * 64, true);

                    break;

                case 113:

                    DialogueCue(103);

                    break;

                case 116:

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueLoad(0, 2);

                    break;

                case 125:

                    activeCounter = 199;

                    break;

            }

        }

        public void ScenePartThree()
        {

            switch (activeCounter)
            {

                // =======================  Part 3: Shrine Engine

                case 200:

                    DialogueClear(0);

                    companions[0].TargetEvent(0, eventVectors[17] * 64, true);

                    break;

                case 202:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueCue(200);

                    break;

                case 204:

                    AddActor(2, eventVectors[18] * 64);

                    voices[3] = actors[2];

                    break;

                case 205:

                    DialogueCue(201);

                    break;

                case 208:

                    DialogueCue(202);

                    break;

                case 211:

                    DialogueCue(203);

                    break;

                case 214:

                    DialogueCue(204);

                    break;

                case 217:

                    DialogueCue(205);

                    break;

                case 220:

                    DialogueCue(206);

                    break;

                case 223:

                    DialogueCue(207);

                    break;

                case 225:

                    Microsoft.Xna.Framework.Rectangle relicRect = IconData.RelicRectangles(IconData.relics.book_manual);

                    TemporaryAnimatedSprite animation = new(0, 6000, 1, 1, eventVectors[19] * 64, false, false)
                    {
                        sourceRect = relicRect,
                        sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                        texture = Mod.instance.iconData.relicsTexture,
                        layerDepth = (eventVectors[19] * 64).Y / 10000 + 0.032f,
                        scale = 4f,
                    };

                    location.TemporarySprites.Add(animation);

                    DialogueCue(208);

                    break;

                case 228:

                    companions[0].LookAtTarget(eventVectors[19] * 64, true);

                    DialogueCueWithFeeling(209);

                    break;

                case 231:

                    CharacterMover.Warp(location, companions[0], eventVectors[20] * 64, true);

                    companions[0].netSpecial.Set((int)Character.Character.specials.pickup);

                    companions[0].specialTimer = 60;

                    break;

                case 232:

                    Mod.instance.iconData.ImpactIndicator(location, companions[0].Position, IconData.impacts.puff, 4f, new() { scheme = IconData.schemes.snazzle, });

                    location.characters.Remove(companions[0]);

                    companions[2] = new Critter(CharacterHandle.characters.BlackCat);

                    companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[2], companions[0].Position, false);

                    voices[4] = companions[2];

                    companions[2].eventName = eventId;

                    DialogueCue(210);

                    break;

                case 234:

                    companions[2].TargetEvent(0, eventVectors[21] * 64, true);

                    companions[2].TargetEvent(1, eventVectors[22] * 64, false);

                    companions[2].TargetEvent(2, eventVectors[23] * 64, false);

                    companions[2].TargetEvent(3, eventVectors[24] * 64, false);

                    companions[2].TargetEvent(4, eventVectors[21] * 64, false);

                    companions[2].TargetEvent(5, eventVectors[22] * 64, false);

                    companions[2].TargetEvent(6, eventVectors[23] * 64, false);

                    companions[2].TargetEvent(7, eventVectors[24] * 64, false);

                    companions[2].TargetEvent(8, eventVectors[21] * 64, false);

                    companions[2].TargetEvent(9, eventVectors[22] * 64, false);

                    companions[2].TargetEvent(10, eventVectors[23] * 64, false);

                    companions[2].TargetEvent(11, eventVectors[24] * 64, false);

                    companions[2].netMovement.Set((int)Character.Character.movements.run);

                    break;

                case 235:

                    DialogueCue(211);

                    break;

                case 237:

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Nom0ri.RomRas"))
                    {

                        companions[3] = new Witch(CharacterHandle.characters.Witch);

                    }
                    else
                    {

                        companions[3] = new Wizard(CharacterHandle.characters.Wizard);

                    }

                    companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[3], eventVectors[25] * 64, true);

                    voices[5] = companions[3];

                    companions[3].eventName = eventId;

                    companions[3].LookAtTarget(Game1.player.Position);

                    SetTrack("WizardSong");

                    break;

                case 238:

                    companions[3].TargetEvent(0, eventVectors[26] * 64, true);

                    DialogueCue(212);

                    break;

                case 241:

                    DialogueCue(213);

                    break;

                case 244:

                    companions[3].ResetActives();

                    companions[3].LookAtTarget(Game1.player.Position, true);

                    companions[2].ResetActives();

                    DialogueCue(214);

                    break;

                case 245:

                    DialogueCue(215);

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    break;

                case 247:

                    DialogueLoad(3, 3);

                    break;

                case 257:

                    activeCounter = 299;

                    break;

            }

        }

        public void ScenePartFour()
        {

            // =======================  Part 4: The duel

            if (bosses.ContainsKey(0))
            {

                if (bosses[0].netWoundedActive.Value)
                {
                    
                    if(activeCounter < 357)
                    {
                        
                        activeCounter = 357;

                        DialogueCue(310);

                        return;

                    }

                }
                else if (!ModUtility.MonsterVitals(bosses[0], location))
                {

                    bosses[0].netWoundedActive.Set(true);

                    bosses[0].Health = bosses[0].MaxHealth;

                    //bosses[0].currentLocation = location;

                    //if (!location.characters.Contains(bosses[0]))
                    //{

                    //    location.characters.Add(bosses[0]);

                    //}

                    activeCounter = 357;

                    DialogueCue(310);

                    return;

                }

            }
            
            switch (activeCounter)
            {

                case 300:

                    DialogueClear(0);

                    companions[3].TargetEvent(300, eventVectors[27] * 64, true);

                    companions[2].TargetEvent(0, eventVectors[20] * 64, true);

                    break;

                case 303:

                    location.characters.Remove(companions[3]);

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Nom0ri.RomRas"))
                    {

                        bosses[0] = new DarkWitch(ModUtility.PositionToTile(companions[3].Position), Mod.instance.CombatDifficulty(),"Witch");


                    }
                    else
                    {

                        bosses[0] = new DarkWizard(ModUtility.PositionToTile(companions[3].Position), Mod.instance.CombatDifficulty());

                    }

                    bosses[0].netScheme.Set(2);

                    bosses[0].SetMode(3);

                    bosses[0].netHaltActive.Set(true);

                    bosses[0].idleTimer = 60;

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    bosses[0].setWounded = true;

                    bosses[0].tether = eventVectors[27] *64;

                    bosses[0].tetherLimit = 768;

                    voices[6] = bosses[0];

                    EventDisplay bossBar = BossBar(0, 6);

                    bossBar.colour = Microsoft.Xna.Framework.Color.Purple;

                    SetTrack("cowboy_boss");

                    companions[2].ResetActives();

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    DialogueCue(211);

                    break;

                case 306:

                    DialogueCue(300);

                    break;

                case 309:

                    DialogueCue(301);

                    break;

                case 312:

                    DialogueCue(302);

                    break;

                case 318:

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    DialogueCue(211);

                    break;

                case 328:

                    DialogueCue(303);

                    break;

                case 331:

                    DialogueCue(304);

                    break;

                case 334:

                    DialogueCue(305);

                    break;

                case 339:

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    DialogueCue(211);

                    break;

                case 345:

                    DialogueCue(306);

                    break;

                case 348:

                    DialogueCue(307);

                    break;

                case 351:

                    DialogueCue(308);

                    break;

                case 354:

                    companions[2].LookAtTarget(Game1.player.Position, true);

                    DialogueCue(309);

                    break;

                case 357:

                    bosses[0].PerformWarp(bosses[0].tether);

                    bosses[0].netWoundedActive.Set(true);

                    bosses[0].Health = bosses[0].MaxHealth;

                    DialogueCue(310);

                    break;

                case 360:

                    StopTrack();

                    CharacterMover.Warp(location, companions[3], bosses[0].Position, false);

                    location.characters.Remove(bosses[0]);

                    bosses.Remove(0);

                    companions[3].ResetActives();

                    companions[3].LookAtTarget(Game1.player.Position, true);

                    DialogueLoad(3, 4);

                    break;

                case 370:

                    activeCounter = 399;

                    break;

            }

        }

        public void ScenePartFive()
        {

            // =======================  Part 5: The revelation

            switch (activeCounter)
            {

                case 400:

                    Game1.stopMusicTrack(MusicContext.Default);

                    DialogueClear(0);

                    DialogueCue(400);

                    companions[3].LookAtTarget(companions[0].Position, true);

                    break;

                case 403:

                    DialogueCue(401);

                    companions[3].netSpecial.Set((int)Character.Character.specials.invoke);

                    companions[3].specialTimer = 60;

                    Mod.instance.iconData.ImpactIndicator(location, companions[0].Position, IconData.impacts.puff, 4f, new() { scheme = IconData.schemes.snazzle, });

                    location.characters.Remove(companions[2]);

                    location.characters.Add(companions[0]);

                    companions[0].ResetActives(true);

                    companions[0].Position = companions[2].Position;

                    companions[0].LookAtTarget(companions[3].Position,true);

                    companions.Remove(2);

                    voices.Remove(4);

                    break;

                case 406:

                    ThrowHandle throwManuscript = new(companions[0].Position, companions[3].Position, IconData.relics.book_manual);

                    throwManuscript.register();

                    DialogueCueWithFeeling(402);

                    break;

                case 409:

                    DialogueCue(403);

                    break;

                case 412:

                    DialogueCueWithFeeling(404);

                    break;

                case 415:

                    DialogueCueWithFeeling(405);

                    break;

                case 418:

                    DialogueCue(406);

                    break;

                case 421:

                    DialogueCueWithFeeling(407);

                    companions[3].LookAtTarget(Game1.player.Position, true);

                    break;

                case 423:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[3].Position, true);

                    location.characters.Remove(companions[3]);

                    companions.Remove(3);

                    voices.Remove(5);

                    break;

                case 424:

                    DialogueCue(408);

                    companions[0].LookAtTarget(actors[2].Position, true);

                    break;

                case 427:

                    DialogueCue(409);

                    break;

                case 430:

                    DialogueCue(410);

                    companions[0].TargetEvent(0, eventVectors[28] * 64, true);

                    break;

                case 433:

                    DialogueCue(411);

                    DialogueLoad(0, 5);

                    StopTrack();

                    break;

                case 445:

                    activeCounter = 499;

                    break;

            }

        }

        public void ScenePartSix()
        {
            switch (activeCounter)
            {
                // =======================  Part 6: The cache

                case 500:

                    ThrowHandle throwRelic = new(Game1.player, companions[0].Position, IconData.relics.wayfinder_dwarf);

                    throwRelic.register();

                    DialogueClear(0);

                    companions[0].TargetEvent(0, eventVectors[29] * 64, true);

                    break;

                case 503:

                    //location = Game1.getLocationFromName("Forest");

                    location = Mod.instance.locations[LocationHandle.druid_clearing_name];

                    Mod.instance.WarpAllFarmers(location.Name, (int)eventVectors[30].X, (int)eventVectors[30].Y, 1);

                    CharacterMover.Warp(location, companions[0], eventVectors[31] * 64, false);

                    companions[0].ResetActives();

                    companions[0].TargetEvent(0, eventVectors[32] * 64 - new Vector2(0,64), true);

                //    break;

                //case 504:

                    DialogueCue(500);

                    break;

                //case 507:
                case 506:

                    Mod.instance.iconData.DecorativeIndicator(location, eventVectors[32] * 64, IconData.decorations.ether, 3f, new() { interval = 10000, });

                    DialogueCueWithFeeling(501);

                    break;

                //case 510:
                case 509:

                    DialogueCue(502);

                    break;

                case 512:

                    DialogueCue(503);

                    companions[0].LookAtTarget(Game1.player.Position);

                    break;

                case 513:

                    Rite.CastTransform();

                    StardewDruid.Cast.Ether.Dragon dragon = (Mod.instance.eventRegister[Rite.eventTransform] as Transform).avatar;

                    dragon.netDigActive.Set(true);

                    dragon.digActive = true;

                    dragon.digTimer = 108;

                    dragon.digMoment = 0;

                    dragon.digPosition = eventVectors[32] * 64;

                    break;

                case 515:

                    (Mod.instance.eventRegister[Rite.eventTransform] as Transform).EventRemove();

                    Mod.instance.eventRegister.Remove(Rite.eventTransform);

                    SpellHandle burn = new(Game1.player, eventVectors[32] * 64, 320, Mod.instance.CombatDamage())
                    {
                        type = SpellHandle.Spells.explode,

                        scheme = IconData.schemes.stars,

                        instant = true,

                        power = 4,

                        terrain = 8,

                        explosion = 8,

                        display = IconData.impacts.dustimpact,

                        sound = SpellHandle.Sounds.flameSpellHit
                    };

                    Mod.instance.spellRegister.Add(burn);

                    SpellHandle burnTwo = new(Game1.player, eventVectors[32] * 64, 384, Mod.instance.CombatDamage())
                    {
                        type = SpellHandle.Spells.explode,

                        scheme = IconData.schemes.ether,

                        power = 4,

                        terrain = 8,

                        explosion = 8,

                        display = IconData.impacts.shockwave,

                        sound = SpellHandle.Sounds.explosion,

                        added = new() { SpellHandle.Effects.embers, }
                    };

                    Mod.instance.spellRegister.Add(burnTwo);

                    break;

                case 516:

                    DialogueCueWithFeeling(504);

                    companions[0].LookAtTarget(eventVectors[32] * 64 + new Vector2(32), true);

                    EventRender gelatinBone = new("gelatinBone", location.Name, eventVectors[32] * 64 + new Vector2(32), IconData.relics.skull_gelatin) { layer = 1f };

                    eventRenders.Add(gelatinBone);

                    EventRender cannoliBone = new("cannoliBone", location.Name, eventVectors[32] * 64 + new Vector2(96,32), IconData.relics.skull_cannoli) { layer = 1f };

                    eventRenders.Add(cannoliBone);
                    break;

                case 517:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(eventVectors[32] * 64 + new Vector2(32), true);

                    companions[0].netIdle.Set((int)Character.Character.idles.kneel);

                    companions[0].idleTimer = 180;

                    break;

                case 519:

                    DialogueCue(505);

                    break;

                case 522:

                    //ThrowHandle newThrowRelic = new(Game1.player, eventVectors[32] * 64, IconData.relics.skull_gelatin);

                    //newThrowRelic.impact = IconData.impacts.puff;

                    //newThrowRelic.register();

                    eventRenders.Clear();

                    DialogueLoad(0, 6);

                    break;

                case 532:

                    activeCounter = 599;

                    break;


            }


        }

        public void ScenePartSeven()
        {
            
            switch (activeCounter)
            {

                case 600:

                    DialogueClear(0);

                    // Rogue character switch

                    companions[7] = new Shadowfolk(CharacterHandle.characters.DarkRogue);

                    voices[7] = companions[7];

                    companions[7].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[7], (eventVectors[33]*64) - new Vector2(0,640), false);

                    companions[7].eventName = eventId;

                    companions[7].LookAtTarget(companions[0].Position, true);

                    companions[7].TargetEvent(0, (eventVectors[33] * 64));

                    // Rogue 2 character switch

                    companions[8] = new Shadowfolk(CharacterHandle.characters.DarkGoblin);

                    voices[8] = companions[8];

                    companions[8].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[8], (eventVectors[34]) * 64 - new Vector2(0, 640), false);

                    companions[8].eventName = eventId;

                    companions[8].LookAtTarget(companions[0].Position, true);

                    companions[8].TargetEvent(0, (eventVectors[34] * 64));

                    // Dwarf

                    CharacterMover.Warp(location, companions[1], (eventVectors[35] * 64) - new Vector2(0, 640));

                    companions[1].TargetEvent(0, (eventVectors[35] * 64));

                    companions[1].LookAtTarget(companions[0].Position, true);

                    break;

                case 601:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(companions[7].Position, true);

                    DialogueCue(600);

                    break;

                case 604:

                    DialogueCueWithFeeling(601);

                    break;

                case 607:

                    companions[0].LookAtTarget(companions[7].Position, true);

                    DialogueCueWithFeeling(602);

                    break;

                case 610:

                    DialogueCueWithFeeling(603);

                    break;

                case 613:

                    companions[0].LookAtTarget(companions[7].Position, true);

                    DialogueCueWithFeeling(604);

                    break;

                case 616:

                    DialogueCueWithFeeling(605);

                    break;

                case 619:

                    DialogueCueWithFeeling(606);

                    break;

                case 622:

                    DialogueCueWithFeeling(607);

                    break;

                case 625:

                    DialogueCueWithFeeling(608);

                    break;

                case 628:

                    DialogueCueWithFeeling(609);

                    break;

                case 631:

                    DialogueCueWithThreat(610);

                    break;

                case 634:

                    DialogueCueWithFeeling(611);

                    break;

                case 637:

                    DialogueCueWithFeeling(612);

                    break;

                case 640:

                    DialogueCueWithFeeling(613);

                    ThrowHandle throwMap = new(companions[0].Position, companions[7].Position, IconData.relics.book_chart);

                    throwMap.register();

                    break;

                case 643:

                    DialogueCueWithFeeling(614);

                    break;

                case 646:

                    DialogueCue(615);

                    break;

                case 648:

                    companions[8].ResetActives();

                    companions[7].TargetEvent(650, (eventVectors[33]) * 64 - new Vector2(0, 640));

                    break;

                case 649:

                    DialogueCue(616);

                    break;

                case 650:

                    companions[1].netDirection.Set(0);

                    break;

                case 651:

                    companions[1].netDirection.Set(1);

                    companions[8].TargetEvent(651, (eventVectors[34]) * 64 - new Vector2(0, 640));

                    break;

                case 652:

                    companions[1].LookAtTarget(companions[0].Position, true);

                    DialogueCue(617);

                    break;

                case 655:

                    ThrowHandle throwBazooka = new(companions[1].Position, companions[0].Position, IconData.relics.dwarven_bazooka);

                    throwBazooka.register();

                    DialogueCue(618);

                    break;

                case 658:

                    DialogueCue(619);

                    break;

                case 659:

                    companions[1].ResetActives();

                    companions[1].TargetEvent(652,eventVectors[37]*64,true);

                    break;

                case 661:

                    companions[0].ResetActives();

                    companions[0].TargetEvent(0, eventVectors[38] * 64);

                    break;

                case 666:

                    companions[0].ResetActives();

                    DialogueLoad(0, 7);

                    break;

                case 678:

                    activeCounter = 699;

                    break;

            }

        }

        public override void EventScene(int index)
        {

            switch (index)
            {
                case 300:

                    companions[3].LookAtTarget(Game1.player.Position);

                    break;

                case 650:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[7].Position, true);

                    companions[7].currentLocation.characters.Remove(companions[7]);

                    companions[7].ClearLight();

                    companions.Remove(7);

                    voices.Remove(7);

                    break;

                case 651:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[8].Position, true);

                    companions[8].currentLocation.characters.Remove(companions[8]);

                    companions[8].ClearLight();

                    companions.Remove(8);

                    voices.Remove(8);

                    break;

                case 652:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position, true);

                    companions[1].currentLocation.characters.Remove(companions[1]);

                    companions[1].ClearLight();

                    companions.Remove(1);

                    voices.Remove(1);

                    break;

            }

        }

    }

}