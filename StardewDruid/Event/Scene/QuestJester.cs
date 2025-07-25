﻿using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Cast.Fates;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewDruid.Location.Terrain;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Constants;
using StardewValley.GameData.Characters;
using System;
using System.Collections.Generic;

namespace StardewDruid.Event.Scene
{
    public class QuestJester : EventHandle
    {

        public Dictionary<int,Vector2> eventVectors = new()
        {

            [900] = new Vector2(24f, 14.5f),

        };

        public Vector2 companionVector;

        public Vector2 buffinVector;

        public Event.Access.AccessHandle MuseumAccess;

        public QuestJester()
        {
            
            mainEvent = true;

            activeLimit = -1;

        }


        public override void EventActivate()
        {

            if (!Mod.instance.characters.ContainsKey(CharacterHandle.characters.Jester))
            {

                return;

            }

            base.EventActivate();

            locales = new()
            {
                "Town",
                LocationHandle.druid_archaeum_name,

            };

            companions[0] = Mod.instance.characters[CharacterHandle.characters.Jester] as StardewDruid.Character.Jester;

            voices[0] = companions[0];

            companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

            companions[0].eventName = eventId;

            CharacterMover.Warp(location, companions[0], origin + new Vector2(128, 128));

            companions[0].netDirection.Set(1);

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 384, IconData.impacts.supree, new()) { sound = SpellHandle.Sounds.getNewSpecialItem, });


        }

        public override bool AttemptReset()
        {

            Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.abortTomorrow), 3, true);

            return false;

        }

        public override void EventInterval()
        {

            activeCounter++;


            if (activeCounter >= 800)
            {

                SceneMuseum();

                return;

            }

            switch (activeCounter)
            {
                // Intro: Pelicans
                case 1:


                    DialogueCue(1);

                    break;

                case 4:

                    DialogueCue( 2 );

                    companionVector = origin + new Vector2(320, 64);

                    companions[0].TargetEvent(0, companionVector, true);

                    break;

                case 7:

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    DialogueLoad(0, 1);

                    break;

                case 18:
                    
                    activeCounter = 99; break;

                // Part One: Marlon
                case 100:

                    DialogueClear(0);

                    Vector2 marlonVector = companionVector + new Vector2(128,0);

                    companions[2] = new Marlon(CharacterHandle.characters.Marlon);

                    companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[2], marlonVector);

                    companions[2].eventName = eventId;

                    companions[2].TargetEvent(301, companions[2].Position + new Vector2(0,192));

                    voices[2] = companions[2];

                    location.playSound("doorOpen");

                    DialogueCue(3);

                    break;

                case 103:

                    DialogueCue(4);

                    companions[0].Position = companions[2].Position - new Vector2(48,0);

                    companions[0].netDirection.Set(1);

                    companions[0].ResetActives();

                    companions[0].netSpecial.Set((int)Character.Character.specials.greet);

                    companions[0].specialTimer = 120;

                    companions[2].Halt();

                    companions[2].netDirection.Set(3);

                    break;

                case 105:

                    companions[0].Position = companions[2].Position - new Vector2(96, 0);

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    break;

                case 106:

                    DialogueCue(5);

                    break;

                case 109:

                    DialogueCue(6);

                    break;

                case 112:

                    companions[2].netDirection.Set(2);

                    DialogueCue(7);

                    break;

                case 115:

                    DialogueCue(8);

                    break;

                case 118:

                    DialogueCue(9);
                    break;

                case 121:

                    companions[0].netDirection.Set(1);

                    companions[0].ResetActives();

                    DialogueCue(10);
                    break;

                case 124:

                    companions[2].netDirection.Set(3);

                    DialogueCueWithFeeling(11,1,Character.Character.specials.gesture);

                    break;

                case 125:

                    DialogueCue(12);
                    break;

                case 127:

                    companions[2].netDirection.Set(2);

                    DialogueCue(13);
                    break;

                case 130:

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    DialogueCue(14);

                    break;

                case 133:

                    DialogueCue(15);
                    break;

                case 134:

                    DialogueCue(16);
                    break;

                case 136:

                    DialogueCue(17);

                    break;

                case 139:

                    companions[2].netDirection.Set(3);

                    DialogueCue(18);

                    break;

                case 142:

                    DialogueCueWithFeeling(19, 1, Character.Character.specials.gesture);

                    ThrowHandle throwRelic = new(companions[2].Position, companions[0].Position, IconData.relics.skull_saurus)
                    {
                        impact = IconData.impacts.puff
                    };

                    throwRelic.register();

                    break;

                case 145:

                    DialogueCue(20);

                    EventRender saurusSkull = new("skull_saurus", location.Name, companions[0].Position + new Vector2(32, 32), IconData.relics.skull_saurus);

                    saurusSkull.layer += 0.0064f;

                    eventRenders.Add( saurusSkull);

                    break;

                case 148:

                    DialogueCue(21);

                    break;

                case 151:

                    ThrowHandle throwCrest = new(Game1.player, companions[2].Position, IconData.relics.crest_associate);

                    throwCrest.register();

                    DialogueCue(22 );

                    break;

                case 154:

                    DialogueCue(23 );

                    companions[2].TargetEvent(302, origin + new Vector2 (448, 64));

                    eventRenders.RemoveAt(eventRenders.Count - 1);

                    break;

                case 156:

                    DialogueCue(24 );

                    RemoveCompanion(2);

                    break;

                case 158:

                    DialogueLoad(0,2);

                    break;

                case 165:

                    activeCounter = 199; break;


                // ----------------------------------------
                // Part Two: Buffin
                
                case 200:

                    DialogueClear(0);

                    companionVector += new Vector2(640, 320);

                    companions[0].ResetActives();

                    companions[0].TargetEvent(0, companionVector, true);

                    DialogueCue(25);

                    break;

                case 203:

                    DialogueCue(26);

                    break;

                case 205:

                    Vector2 BuffinPosition = companionVector + new Vector2(128,0);

                    companions[1] = new Buffin(CharacterHandle.characters.Buffin);

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[1], BuffinPosition);

                    companions[1].eventName = eventId;

                    companions[1].moveDirection = 3;

                    companions[1].netDirection.Set(3);

                    voices[1] = companions[1];

                    break;

                case 206:

                    DialogueCue(27);

                    break;

                case 207:

                    buffinVector = companionVector + new Vector2(960, 192);

                    companions[1].TargetEvent(0, buffinVector, true);

                    break;

                case 209:

                    DialogueCue(28);

                    companionVector = companionVector + new Vector2(832, 192);

                    companions[0].TargetEvent(0, companionVector, true);

                    break;

                case 211:

                    companions[1].IsInvisible = true;

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position - new Vector2(0.0f, 32f));

                    break;

                case 212:

                    DialogueCue(29);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position + new Vector2(0.0f, -196f));

                    break;

                case 213:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position + new Vector2(-320f, 128f));

                    break;

                case 214:

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position + new Vector2(-384f, -196f));

                    break;

                case 215:


                    activeCounter = 249;

                    break;

                case 250:

                    companions[0].LookAtTarget(Game1.player.Position,true);

                    DialogueLoad(0,3);

                    break;

                case 261:

                    activeCounter = 299; break;

                // --------------------------------------
                // Part Three: Contest

                case 300:

                    DialogueClear(0);

                    companions[1].IsInvisible = false;

                    companions[1].Position = companions[0].Position + new Vector2(196, 64);

                    companions[1].Halt();

                    companions[1].netDirection.Set(3);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position - new Vector2(0.0f, 32f));

                    DialogueCue(30);

                    break;

                case 302:

                    companions[0].netDirection.Set(1);

                    break;

                case 303:

                    DialogueCue(31);

                    break;

                case 304:

                    companions[1].doEmote(20, true);
                    activeCounter = 310;
                    break;

                /*case 306:

                    DialogueCue(32);

                    break;

                case 309:

                    DialogueCue(33);

                    break;*/

                case 312:

                    DialogueCue(34 );

                    break;

                case 315:

                    DialogueCue(35 );

                    break;

                case 318:

                    DialogueCue(36);

                    break;

                case 321:

                    DialogueCue(37 );

                    break;

                case 324:

                    DialogueCue(38 );

                    break;

                case 327:

                    DialogueCue(39 );

                    break;

                case 330:

                    DialogueCue(40);

                    break;

                case 331:

                    DialogueCue(41);

                    companions[0].ResetActives();

                    // Jester route

                    Vector2 jesterRace = companions[0].Position + new Vector2(-240, 640);
                    
                    companions[0].TargetEvent(0, jesterRace, true);

                    jesterRace += new Vector2(64, 1152);

                    companions[0].TargetEvent(1, jesterRace, false);

                    jesterRace += new Vector2(2788, 0);

                    companions[0].TargetEvent(201, jesterRace, false);

                    //jesterRace += new Vector2(128, -2560);

                    //companions[0].TargetEvent(201, jesterRace, false);

                    companionVector = jesterRace;

                    companions[0].netMovement.Set((int)Character.Character.movements.run);


                    // Buffin route

                    companions[1].ResetActives();

                    Vector2 buffinRace = companions[1].Position + new Vector2(-384, 640);

                    companions[1].TargetEvent(0, buffinRace, true);

                    buffinRace += new Vector2(64, 1152);

                    companions[1].TargetEvent(1, buffinRace, false);

                    buffinRace += new Vector2(2788, 0);

                    companions[1].TargetEvent(2, buffinRace, false);

                    //buffinRace += new Vector2(256, -2624);

                    //companions[1].TargetEvent(3, buffinRace, false);

                    buffinVector = buffinRace;

                    companions[1].netMovement.Set((int)Character.Character.movements.run);

                    break;

                case 333:

                    DialogueCue(42);

                    break;

                case 335:

                    DialogueCue(43);

                    break;

                case 370:

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    companions[0].netDirection.Set(1);

                    DialogueCue(44);

                    DialogueLoad(0, 4);

                    break;

                case 371:

                    companions[1].netIdle.Set((int)Character.Character.idles.standby);

                    companions[1].netDirection.Set(3);

                    break;

                case 385:

                    activeCounter = 799;
                    
                    break;


                // --------------------------------------
                // Cat fight

                case 400:

                    RemoveCompanion(3);

                    DialogueClear(0);

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    companions[1].netIdle.Set((int)Character.Character.idles.standby);

                    companions[1].netDirection.Set(3);

                    DialogueCue(46);

                    break;

                case 403:

                    companions[0].netDirection.Set(1);

                    DialogueCue(47);

                    break;

                case 406:

                    DialogueCue(48);

                    break;

                case 409:

                    DialogueCue(49 );

                    break;

                case 412:

                    DialogueCue(50);

                    break;

                case 415:

                    DialogueCue(51 );

                    break;

                case 418:

                    DialogueCue(52);

                    break;

                case 421:

                    companions[0].ResetActives(true);

                    companions[0].LookAtTarget(companions[1].Position);

                    DialogueCue(53 );

                    companions[0].doEmote(16);

                    break;

                case 424:

                    companions[1].ResetActives(true);

                    companions[1].LookAtTarget(companions[0].Position);

                    DialogueCue(54);

                    break;

                case 427:

                    DialogueCue(55);

                    break;

                case 430:

                    DialogueCue(56);

                    break;

                case 433:

                    DialogueCue(57);

                    companions[1].Position = companions[1].Position + new Vector2(192f, -128f);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position - new Vector2(0.0f, 32f));

                    break;

                case 436:

                    DialogueCue(58);

                    companions[0].Position = companions[0].Position + new Vector2(64f, -128f);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[0].Position - new Vector2(0.0f, 32f));

                    break;

                case 438:

                    companions[0].ResetActives(true);

                    companions[0].TargetEvent(0, companions[1].Position,true);

                    companions[0].SetDash(companions[1].Position,false);
                    
                    companions[1].ResetActives(true);

                    companions[1].TargetEvent(0, companions[0].Position,true);

                    companions[1].SetDash(companions[0].Position,false);

                    Mod.instance.iconData.ImpactIndicator(location, companions[0].Position + new Vector2(352,32), Data.IconData.impacts.flashbang, 3, new());

                    break;

                case 440:

                    companions[0].LookAtTarget(companions[1].Position);

                    companions[1].LookAtTarget(companions[0].Position);

                    break;

                case 441:

                    DialogueCue(59);

                    companions[0].ResetActives(true);

                    companions[0].TargetEvent(0, companions[1].Position, true);

                    companions[0].SetDash(companions[1].Position, false);

                    companions[1].ResetActives(true);

                    companions[1].TargetEvent(0, companions[0].Position, true);

                    companions[1].SetDash(companions[0].Position, false);

                    Mod.instance.iconData.ImpactIndicator(location, companions[0].Position + new Vector2(-288,32), Data.IconData.impacts.flashbang, 3, new());

                    break;

                case 444:

                    DialogueCue(60);

                    companions[0].ResetActives(true);

                    companions[0].Position = companionVector + new Vector2(-32, -256);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[0].Position - new Vector2(0.0f, 32f));

                    companions[1].ResetActives(true);

                    companions[1].Position = buffinVector + new Vector2(576, -256);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position - new Vector2(0.0f, 32f));

                    companions[0].LookAtTarget(companions[1].Position);

                    companions[1].LookAtTarget(companions[0].Position);

                    break;

                case 446:

                    // Jester beam

                    companions[0].ResetActives(true);

                    companions[0].netSpecial.Set((int)Character.Character.specials.special);
                    
                    companions[0].specialTimer = 120;

                    SpellHandle beam = new(companions[0].currentLocation, companions[1].GetBoundingBox().Center.ToVector2(), companions[0].GetBoundingBox().Center.ToVector2())
                    {
                        type = SpellHandle.Spells.beam
                    };

                    Mod.instance.spellRegister.Add(beam);

                    // Buffin beam

                    companions[1].ResetActives(true);

                    companions[1].netSpecial.Set((int)Character.Character.specials.special);

                    companions[1].specialTimer = 120;

                    SpellHandle profanity = new(companions[1].currentLocation, companions[0].GetBoundingBox().Center.ToVector2(), companions[1].GetBoundingBox().Center.ToVector2())
                    {
                        type = SpellHandle.Spells.echo,

                        missile = MissileHandle.missiles.curseecho
                    };

                    Mod.instance.spellRegister.Add(profanity);

                    companions[1].doEmote(16);

                    break;

                case 447:

                    Vector2 cornerVector = companionVector + new Vector2(0, -448);

                    for (int i = 1; i < 10; i+=2)
                    {

                        for (int j = 1; j < 4; j+=2)
                        {

                            Vector2 burnVector = cornerVector + new Vector2((i * 64), (j * 64)) - new Vector2(16 * Mod.instance.randomIndex.Next(0, 4), 16 * Mod.instance.randomIndex.Next(0, 4));


                            Cast.Effect.EmberTarget ember = new(location, burnVector, 2, 0, 0, IconData.schemes.stars);

                            List<TemporaryAnimatedSprite> embers = ember.EmberConstruct(location, IconData.schemes.stars, burnVector, 2f + (0.5f * Mod.instance.randomIndex.Next(5)), 60, 999f);

                            animations.AddRange(embers);

                        }

                    }

                    break;

                case 448:

                    companions[0].netDirection.Set(0);

                    companions[0].netAlternative.Set(1);

                    companions[0].Position = companionVector + new Vector2(128,64);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[0].Position - new Vector2(0.0f, 32f));

                    companions[1].moveDirection = 0;

                    companions[1].netDirection.Set(0);

                    companions[1].netAlternative.Set(3);

                    companions[1].Position = buffinVector + new Vector2(128, 64);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position - new Vector2(0.0f, 32f));

                    break;

                case 449:

                    DialogueCue(61);

                    break;

                case 450:

                    DialogueLoad(0,5);

                    break;

                case 460:

                    activeCounter = 499;

                    break;

                // -----------------------------------
                // aftermath

                case 500:

                    DialogueClear(0);

                    companions[1].netDirection.Set(3);

                    companions[0].netDirection.Set(1);

                    break;

                case 501:

                    DialogueCue(62);

                    break;

                case 504:

                    DialogueCue(63);

                    break;

                case 507:

                    DialogueCue(64 );

                    companionVector = companionVector += new Vector2(-1280, 128);

                    companions[0].TargetEvent(202, companionVector, true);

                    buffinVector = companionVector + new Vector2(108, 0);

                    companions[1].TargetEvent(0, buffinVector, true);

                    break;

                case 510:

                    DialogueCue(65 );

                    break;

                case 520:

                    DialogueCue(66);

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    companions[0].netDirection.Set(1);

                    break;

                case 523:

                    companions[1].netIdle.Set((int)Character.Character.idles.standby);

                    companions[1].netDirection.Set(3);

                    DialogueCue(67 );

                    break;

                case 526:

                    DialogueCue(68 );

                    break;

                case 529:

                    DialogueCue(69 );

                    break;

                case 532:

                    DialogueCue(70);

                    break;

                case 535:

                    DialogueCue(71 );

                    break;

                case 538:

                    DialogueCue(72 );

                    break;

                case 541:

                    DialogueCue(73);

                    break;

                case 544:

                    DialogueCue(74 );

                    break;

                case 546:

                    DialogueCue(75 );

                    break;

                case 548:

                    DialogueCue(76);

                    break;

                case 550:

                    DialogueCue(77 );

                    break;

                case 553:

                    DialogueCue(78 );

                    break;

                case 556:

                    DialogueCue(79);

                    break;

                case 559:

                    DialogueCue(80);

                    break;

                case 562:

                    companions[1].ResetActives();

                    companions[1].TargetEvent(0, companions[0].Position + new Vector2(32, 0));

                    DialogueCue(81);

                    break;

                case 563:

                    companions[1].ResetActives();

                    companions[1].LookAtTarget(companions[0].Position, true);

                    companions[1].netSpecial.Set((int)Character.Character.specials.greet);

                    companions[1].specialTimer = 240;

                    break;

                case 564:

                    companions[0].ResetActives(true);

                    companions[0].LookAtTarget(companions[1].Position, true);

                    companions[0].netSpecial.Set((int)Character.Character.specials.greet);

                    companions[0].specialTimer = 180;

                    DialogueCue(82);

                    break;

                case 567:

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position - new Vector2(0.0f, 32f), true);

                    companions[1].currentLocation.characters.Remove(companions[1]);

                    DialogueLoad(0, 6);

                    break;

                case 575:

                    activeCounter = 599;

                    break;

                case 600:

                    DialogueCue(83);

                    companions[0].SwitchToMode(Character.Character.mode.random,Game1.player);

                    companions[0].TargetIdle(1440);

                    eventComplete = true;

                    break;

            }

        }

        public void SceneMuseum()
        {

            DialogueCue(activeCounter);

            if (bosses.Count > 0 && activeCounter < 965)
            {

                if (!ModUtility.MonsterVitals(bosses[0], location) || bosses[0].netWoundedActive.Value)
                {

                    activeCounter = 965;

                }

            }

            switch (activeCounter)
            {

                // --------------------------------------
                // Museum entry

                case 800:

                    Vector2 archaeum = companions[0].Position + new Vector2(448, -64);

                    MuseumAccess = new();

                    MuseumAccess.AccessSetup("Town", LocationHandle.druid_archaeum_name, ModUtility.PositionToTile(archaeum), new Vector2(24, 15));

                    MuseumAccess.location = location;

                    MuseumAccess.AccessStart();

                    location.localSound("secret1");

                    DialogueClear(0);

                    break;

                case 801:

                    Vector2 archaeumEntrance = companions[0].Position + new Vector2(320, -64);

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(archaeumEntrance, true);

                    companions[1].ResetActives();

                    companions[1].LookAtTarget(archaeumEntrance, true);

                    DialogueCue(45);

                    break;

                case 804:

                    DialogueCue(100);

                    break;

                case 805:

                    Vector2 archaeumTarget = companions[0].Position + new Vector2(320, -64);

                    companions[0].ResetActives();

                    companions[0].TargetEvent(203, archaeumTarget, true);

                    companions[1].ResetActives();

                    companions[1].TargetEvent(0, archaeumTarget, true);

                    break;

                case 810:

                    location = Mod.instance.locations[LocationHandle.druid_archaeum_name];

                    location.warps.Clear();

                    Mod.instance.WarpAllFarmers(LocationHandle.druid_archaeum_name, (int)eventVectors[900].X + 3, (int)(eventVectors[900].Y + 7), 1);

                    CharacterMover.Warp(Mod.instance.locations[LocationHandle.druid_archaeum_name], companions[0], new Vector2((int)eventVectors[900].X -1, (int)eventVectors[900].Y + 3) * 64, false);

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    CharacterMover.Warp(Mod.instance.locations[LocationHandle.druid_archaeum_name], companions[1], new Vector2((int)eventVectors[900].X + 8, (int)eventVectors[900].Y + 3) * 64, false);

                    companions[1].netIdle.Set((int)Character.Character.idles.standby);

                    companions[1].netDirection.Set(3);

                    Vector2 GuntherPosition = new Vector2((int)eventVectors[900].X + 3, (int)eventVectors[900].Y ) * 64;

                    companions[3] = new Gunther(CharacterHandle.characters.Gunther);

                    companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[3], GuntherPosition);

                    companions[3].eventName = eventId;

                    companions[3].netDirection.Set(2);

                    voices[3] = companions[3];

                    eventRenders.Add(new("skull_saurus", location.Name, new Vector2(eventVectors[900].X + 3f, eventVectors[900].Y + 3f) * 64, IconData.relics.skull_saurus));

                    MuseumAccess.AccessWarps();

                    break;

                case 811:
                case 812:
                case 813:
                case 814:

                    if (Game1.player.currentLocation.Name == LocationHandle.druid_archaeum_name)
                    {

                        activeCounter = 899;

                    }

                    break;

                case 815:

                    activeCounter = 899;

                    break;

                // --------------------------------------
                // Museum fight

                case 900:

                    SetTrack("libraryTheme");

                    break;

                case 916:

                    companions[0].ResetActives(true);

                    companions[0].netDirection.Set(1);

                    companions[1].ResetActives(true);

                    companions[1].netDirection.Set(3);

                    location.playSound(SpellHandle.Sounds.thunder.ToString());

                    if (location is Archaeum archaeum2)
                    {

                        archaeum2.ambientDarkness = true;

                        foreach (TerrainField terrainField in archaeum2.terrainFields)
                        {

                            if (terrainField is RitualCircle circle)
                            {

                                circle.disabled = false;

                                circle.ritualStatus = 1;

                                circle.LoadCandles(0);

                            }

                        }

                    }
                    break;

                case 918:

                    Winds windsNew = new();

                    windsNew.EventSetup(new Vector2(eventVectors[900].X+3, eventVectors[900].Y+3) * 64, Rite.eventWinds);

                    windsNew.EventActivate();

                    windsNew.eventLocked = true;

                    windsNew.WindArray(new(), WispHandle.wisptypes.winds, 50);

                    location.playSound(SpellHandle.Sounds.thunder.ToString());

                    if (location is Archaeum archaeum3)
                    {

                        foreach (TerrainField terrainField in archaeum3.terrainFields)
                        {

                            if (terrainField is RitualCircle circle)
                            {

                                circle.SetCandles(1);

                            }

                        }

                    }

                    break;

                case 920:

                    SpellHandle circleHandle = new(new Vector2(eventVectors[900].X + 3, eventVectors[900].Y + 3) * 64, 256, IconData.impacts.summoning, new())
                    {
                        scheme = IconData.schemes.fates,

                        sound = SpellHandle.Sounds.shadowDie
                    };

                    Mod.instance.spellRegister.Add(circleHandle);

                    eventRenders.RemoveAt(eventRenders.Count - 1);

                    break;

                case 921:

                    SetTrack("tribal");

                    StardewDruid.Monster.Dinosaur dinosaur = new(new Vector2((int)eventVectors[900].X+3, (int)eventVectors[900].Y+4), Mod.instance.CombatDifficulty());

                    dinosaur.netScheme.Set(2);

                    dinosaur.baseJuice = 1;

                    dinosaur.basePulp = 40;

                    dinosaur.SetMode(3);

                    dinosaur.setWounded = true;

                    dinosaur.netScheme.Set(1);

                    dinosaur.netPosturing.Set(true);

                    location.characters.Add(dinosaur);

                    dinosaur.dragonRender.GhostScheme();

                    dinosaur.update(Game1.currentGameTime, location);

                    bosses[0] = dinosaur;

                    voices[4] = dinosaur;

                    BossBar(0, 4);

                    break;

                case 923:

                    companions[3].TargetEvent(0, companions[3].Position - new Vector2(384, 256));

                    companions[3].netMovement.Set((int)Character.Character.movements.run);

                    bosses[0].netPosturing.Set(false);

                    break;

                case 925:

                    companions[3].LookAtTarget(bosses[0].Position);

                    companions[0].SwitchToMode(Character.Character.mode.track, Game1.player);

                    Mod.instance.characters[CharacterHandle.characters.Buffin] = companions[1];

                    companions[1].SwitchToMode(Character.Character.mode.track, Game1.player);

                    break;

                case 928:
                case 933:
                case 938:
                case 943:
                case 948:
                case 953:
                case 958:
                case 962:

                    companions[3].SpecialAttack(bosses[0]);

                    break;

                case 964:

                    bosses[0].Halt();

                    bosses[0].idleTimer = 2000;

                    Mod.instance.iconData.DecorativeIndicator(location, bosses[0].Position, IconData.decorations.fates, 3f, new() { interval = 2000 });

                    break;

                case 965:

                    Mod.instance.spellRegister.Add(new(bosses[0].Position, 320, IconData.impacts.deathbomb, new()) { displayRadius = 4, sound = SpellHandle.Sounds.shadowDie});

                    ThrowHandle newThrowRelic = new(Game1.player, companions[1].Position, IconData.relics.skull_saurus)
                    {
                        impact = IconData.impacts.puff
                    };

                    newThrowRelic.register();

                    location.characters.Remove(bosses[0]);

                    bosses.Clear();

                    voices.Remove(4);

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    Mod.instance.characters.Remove(CharacterHandle.characters.Buffin);

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[1].netIdle.Set((int)Character.Character.idles.standby);

                    if (location is Archaeum archaeum4)
                    {

                        foreach (TerrainField terrainField in archaeum4.terrainFields)
                        {

                            if (terrainField is RitualCircle circle)
                            {

                                circle.SetCandles(0);

                            }

                        }

                    }

                    if (Mod.instance.eventRegister.ContainsKey(Rite.eventWinds))
                    {

                        Mod.instance.eventRegister[Rite.eventWinds].eventComplete = true;

                    }

                    break;

                case 975:

                    companions[0].ResetActives();

                    companions[0].TargetEvent(0, new Vector2(27, 28) * 64);

                    companions[1].ResetActives();

                    companions[1].TargetEvent(0, new Vector2(27, 28) * 64);

                    break;

                case 978:

                    location.updateWarps();

                    Vector2 outsideTile = ModUtility.PositionToTile(companionVector);

                    Mod.instance.WarpAllFarmers("Town", (int)outsideTile.X + 1, (int)outsideTile.Y + 2, 1);

                    location = Game1.getLocationFromName("Town");

                    CharacterMover.Warp(location, companions[0], companionVector, false);//new Vector2(100, 56) * 64, false);

                    buffinVector = companionVector + new Vector2(192, 0);

                    CharacterMover.Warp(location, companions[1], buffinVector, false);//new Vector2(102, 56) * 64, false);

                    companions[0].netDirection.Set(1);

                    companions[1].netDirection.Set(3);

                    StopTrack();

                    break;

                case 979:
                case 980:
                case 981:
                case 982:

                    if (Game1.player.currentLocation.Name == "Town")
                    {

                        activeCounter = 399;

                    }

                    break;

                case 983:

                    activeCounter = 399;

                    break;

            }

        }


        public override void EventScene(int index)
        {

            switch (index)
            {

                case 201:

                    activeCounter = Math.Max(369, activeCounter);

                    break;

                case 202:

                    activeCounter = Math.Max(519, activeCounter);

                    break;

                case 203:

                    activeCounter = Math.Max(809, activeCounter);

                    break;

                case 301:

                    companions[2].Halt();

                    companions[2].netDirection.Set(3);

                    break;

                case 302:

                    companions[2].IsInvisible = true;

                    location.playSound("doorClose");

                    break;
            
            }

        }

    }

}
