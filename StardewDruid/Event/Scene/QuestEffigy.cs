using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Timers;


namespace StardewDruid.Event.Scene
{
    public class QuestEffigy : EventHandle
    {

        public Vector2 beachWarp;

        public Vector2 campFire;

        public Vector2 bridgeVector;

        public Vector2 blobVector;

        public Vector2 atollVector;

        public Vector2 mistVector;

        public QuestEffigy()
        {

            mainEvent = true;

            activeLimit = 600;

        }

        public override void EventActivate()
        {

            if (!Mod.instance.characters.ContainsKey(CharacterHandle.characters.Effigy))
            {

                return;

            }

            base.EventActivate();

            locales = new()
            {
                "Beach",
                LocationData.druid_atoll_name,

            };

            beachWarp = new Vector2(10f, 15f);

            campFire = new Vector2(48, 20);

            bridgeVector = new Vector2(55, 13);

            //blobVector = new Vector2(75, 8);

            blobVector = new Vector2(25, 10);

            atollVector = new Vector2(92, 7);

            mistVector = new Vector2(25, 15);

            ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

            location.playSound("discoverMineral");

            //(Mod.instance.locations[LocationData.druid_atoll_name] as Atoll).ambientDarkness = true;

        }

        public override bool AttemptReset()
        {

            Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.abortTomorrow), 3, true);

            return false;

        }

        public override void EventInterval()
        {

            /*if (Game1.activeClickableMenu != null)
            {

                activeLimit += 1;

                return;

            }*/

            activeCounter++;

            if (activeCounter > 500)
            {

                DialogueCue(activeCounter);

            }

            switch (activeCounter)
            {

                // ------------------------------------------
                // 1 Beginning
                // ------------------------------------------

                case 1:

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Effigy] as StardewDruid.Character.Effigy;

                    voices[0] = companions[0];

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[0], beachWarp * 64);

                    companions[0].netDirection.Set(3);

                    companions[0].eventName = eventId;

                    DialogueCue(1);

                    //activeCounter = 300;

                    break;

                case 3:

                    companions[0].ResetActives();

                    DialogueCue(2);

                    break;

                case 6:

                    DialogueLoad(0, 1);

                    break;

                case 16: activeCounter = 100; break;


                // ------------------------------------------
                // 2 Fish surprise
                // ------------------------------------------

                case 101: // clear

                    DialogueClear(0);

                    companions[0].ResetActives();

                    break;

                case 102:

                    DialogueCue(3);

                    companions[0].TargetEvent(110, companions[0].Position + new Vector2(-128, 0), true);

                    break;

                case 112:

                    DialogueCue(4);

                    companions[0].netSpecial.Set((int)Character.Character.specials.invoke);

                    companions[0].specialTimer = 60;

                    Mod.instance.iconData.DecorativeIndicator(location, companions[0].Position, IconData.decorations.weald, 3f, new() { interval = 1200, });

                    break;

                case 113:


                    location.playSound("discoverMineral", null, 800);

                    Mod.instance.iconData.ImpactIndicator(location, (beachWarp - new Vector2(6, 0)) * 64, IconData.impacts.nature, 4f, new());

                    break;

                case 115: DialogueCue(777); break;

                case 118:

                    DialogueCue(5);

                    companions[0].netSpecial.Set((int)Character.Character.specials.invoke);

                    companions[0].specialTimer = 60;

                    break;

                case 119:

                    location.playSound("discoverMineral");

                    List<Vector2> vectors25 = new()
                    {

                        new(6,0),
                        new(8,2),
                        new(7,4),
                        new(7,-4),
                        new(9,-2),
                    };

                    Mod.instance.iconData.DecorativeIndicator(location, companions[0].Position, IconData.decorations.weald, 3f, new() { interval = 1200, });

                    for (int i = 0; i < vectors25.Count; i++)
                    {

                        Mod.instance.iconData.ImpactIndicator(location, (beachWarp - vectors25[i]) * 64, IconData.impacts.nature, 4f, new());

                    }

                    break;

                case 121:

                    DialogueCue(6);

                    break;

                case 122:

                    companions[0].TargetEvent(140, (campFire * 64) - new Vector2(0, 128), true);

                    companions[0].netDirection.Set(3);

                    location.playSound("pullItemFromWater");

                    Mod.instance.iconData.ImpactIndicator(location, companions[0].Position - new Vector2(256, 0), IconData.impacts.fish, 3f, new());

                    new ThrowHandle(
                        companions[0].Position - new Vector2(256, 0),
                        companions[0].Position,
                        new StardewValley.Object("147", 1))
                    { pocket = true }.register();

                    break;

                case 123:
                case 124:
                case 125:

                    if (activeCounter == 124)
                    {

                        DialogueCue(7);

                        companions[0].netDirection.Set(1);

                    }

                    location.playSound("pullItemFromWater");

                    List<Vector2> vectors28 = new()
                    {

                        new(6,0),
                        new(8,2),
                        new(7,4),
                        new(7,-4),
                        new(9,-2),
                    };

                    Vector2 position28 = origin;

                    for (int i = 0; i < vectors28.Count; i++)
                    {

                        Mod.instance.iconData.ImpactIndicator(location, (beachWarp - vectors28[i]) * 64, IconData.impacts.fish, 3f, new());

                        new ThrowHandle(
                            origin - (vectors28[i] * 64),
                            companions[0].Position - new Vector2(256, 128) + new Vector2(Mod.instance.randomIndex.Next(16) * 32, Mod.instance.randomIndex.Next(8) * 32),
                            new StardewValley.Object("147", 1)
                        )
                        { pocket = Mod.instance.randomIndex.Next(2) == 0 }.register();

                        new ThrowHandle(
                            origin - (vectors28[i] * 64),
                            companions[0].Position - new Vector2(256, 128) + new Vector2(Mod.instance.randomIndex.Next(16) * 32, Mod.instance.randomIndex.Next(8) * 32),
                            new StardewValley.Object("147", 1)
                        )
                        { pocket = Mod.instance.randomIndex.Next(2) == 0 }.register();

                    }

                    break;

                case 141:

                    DialogueLoad(0, 2);

                    break;

                case 156: activeCounter = 200; break;


                // ------------------------------------------
                // 3 Fish stew
                // ------------------------------------------

                case 201:

                    DialogueClear(0);

                    DialogueCue(8);

                    Vector2 cursor56 = campFire * 64;

                    companions[0].LookAtTarget(cursor56, true);

                    break;

                case 203:

                    companions[0].netSpecial.Set((int)Character.Character.specials.invoke);

                    companions[0].specialTimer = 60;

                    Mod.instance.iconData.DecorativeIndicator(location, companions[0].Position, IconData.decorations.mists, 3f, new() { interval = 1200, });

                    break;

                case 204:

                    if (!location.objects.ContainsKey(campFire))
                    {
                        Torch camp = new("278", true)
                        {
                            Fragility = 1,
                            destroyOvernight = true
                        };

                        location.objects.Add(campFire, camp);

                    }

                    Vector2 cursor57 = campFire * 64;

                    Mod.instance.iconData.CursorIndicator(location, cursor57, IconData.cursors.mists, new());

                    //Mod.instance.iconData.AnimateBolt(location, cursor57);
                    Mod.instance.spellRegister.Add(new(cursor57, 128, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt });

                    break;

                case 205:

                    DialogueCue(9);

                    Game1.playSound("fireball");

                    Mod.instance.iconData.ImpactIndicator(location, campFire * 64, IconData.impacts.impact, 3f, new());

                    break;

                case 206:

                    companions[0].netSpecial.Set((int)Character.Character.specials.invoke);

                    companions[0].specialTimer = 60;

                    Mod.instance.iconData.DecorativeIndicator(location, companions[0].Position, IconData.decorations.mists, 3f, new() { interval = 1200, });

                    break;

                case 207:

                    Vector2 cursor60 = campFire * 64;

                    Mod.instance.iconData.CursorIndicator(location, cursor60, IconData.cursors.mists, new());

                    //Mod.instance.iconData.AnimateBolt(location, campFire * 64);
                    Mod.instance.spellRegister.Add(new(campFire * 64, 128, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt });

                    break;

                case 208:

                    DialogueCue(10);

                    break;

                case 209:

                    Game1.playSound("fireball");

                    Mod.instance.iconData.ImpactIndicator(location, campFire * 64, IconData.impacts.impact, 3f, new());

                    break;

                case 210:

                    companions[0].netSpecial.Set((int)Character.Character.specials.invoke);

                    companions[0].specialTimer = 60;

                    Mod.instance.iconData.DecorativeIndicator(location, companions[0].Position, IconData.decorations.mists, 3f, new() { interval = 1200, });

                    break;

                case 211:

                    Vector2 cursor64 = campFire * 64;

                    Mod.instance.iconData.CursorIndicator(location, cursor64, IconData.cursors.mists, new());

                    //Mod.instance.iconData.AnimateBolt(location, cursor64);
                    Mod.instance.spellRegister.Add(new(cursor64, 128, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt });

                    break;

                case 213:

                    DialogueCue(11);

                    new ThrowHandle(campFire * 64, campFire * 64 - new Vector2(128, 0), new StardewValley.Object("728", 1)).register();

                    Game1.playSound("fireball");

                    Mod.instance.iconData.ImpactIndicator(location, campFire * 64, IconData.impacts.impact, 5f, new());

                    break;

                case 214:

                    new ThrowHandle(campFire * 64 - new Vector2(128, 0), campFire * 64 - new Vector2(256, 0), new StardewValley.Object("728", 1)).register();

                    Game1.playSound("fireball");

                    Mod.instance.iconData.ImpactIndicator(location, (campFire - new Vector2(2, 0)) * 64, IconData.impacts.impact, 3f, new());

                    break;

                case 215:

                    new ThrowHandle(campFire * 64 - new Vector2(256, 0), campFire * 64 - new Vector2(384, 0), new StardewValley.Object("728", 1)) { pocket = true }.register();

                    Game1.playSound("fireball");

                    Mod.instance.iconData.ImpactIndicator(location, (campFire - new Vector2(4, 0)) * 64, IconData.impacts.impact, 3f, new());

                    break;

                case 216:

                    DialogueLoad(0, 3);

                    Game1.playSound("fireball");

                    Mod.instance.iconData.ImpactIndicator(location, (campFire - new Vector2(6, 0)) * 64, IconData.impacts.impact, 4f, new());

                    break;

                case 228: activeCounter = 250; break;

                // ------------------------------------------
                // 3.5 Position before Jellyking
                // ------------------------------------------

                case 251:

                    DialogueClear(0);

                    companions[0].TargetEvent(255, bridgeVector*64, true);

                    companions[0].TargetEvent(260, (bridgeVector + new Vector2(12,0))*64, false);

                    //companions[0].TargetEvent(300, (bridgeVector + new Vector2(14, -2))*64, false);
                    companions[0].TargetEvent(265, (bridgeVector + new Vector2(14, -2)) * 64, false);

                    companions[0].TargetEvent(270, (bridgeVector + new Vector2(24, -2)) * 64, false);

                    companions[0].TargetEvent(275, atollVector * 64, false);

                    DialogueCue(12);

                    break;

                case 254:

                    DialogueCue(31);

                    break;

                case 257: 
                    
                    DialogueCue(32);
                    
                    break;

                case 276:

                    DialogueClear(0);

                    Game1.warpFarmer(LocationData.druid_atoll_name, 15, 11, 1);

                    Game1.xLocationAfterWarp = 15;

                    Game1.yLocationAfterWarp = 11;

                    location = Mod.instance.locations[LocationData.druid_atoll_name];

                    location.warps.Clear();

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_atoll_name], companions[0], new Vector2(17, 12) * 64, false);

                    break;

                case 277:
                case 278:
                case 279:
                case 280:

                    if (Game1.player.currentLocation.Name == LocationData.druid_atoll_name)
                    {

                        activeCounter = 300;

                    }

                    break;


                // ------------------------------------------
                // 4 Jellyking
                // ------------------------------------------

                case 301:

                    DialogueCue(13);

                    break;

                case 302:

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    break;

                case 305: 
                    
                    DialogueCue(14 ); 
                    
                    break;

                case 308: 
                    
                    DialogueCue(15); 
 
                    Blobfiend blobking = new Blobfiend(blobVector, Mod.instance.CombatDifficulty());

                    blobking.netScheme.Set(2);

                    blobking.SetMode(3);

                    blobking.netPosturing.Set(true);

                    blobking.LookAtFarmer();

                    blobking.groupMode = true;

                    location.characters.Add(blobking);

                    blobking.update(Game1.currentGameTime, location);

                    voices[1] = blobking;

                    bosses[0] = blobking;

                    break;

                case 311: DialogueCue(16); break;

                case 313:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(bosses[0].Position);

                    break;

                case 314: 
                    
                    DialogueCue(17); 
                    
                    companions[0].LookAtTarget(bosses[0].Position); 
                    
                    break;

                case 317: 
                    
                    DialogueCue(18 ); 
                    
                    companions[0].LookAtTarget(bosses[0].Position); 
                    
                    break;

                case 320: 
                    
                    DialogueCue(19 ); 
                    
                    companions[0].LookAtTarget(bosses[0].Position); 
                    
                    break;

                case 323: 
                    
                    DialogueCue(20); 
                    
                    companions[0].LookAtTarget(bosses[0].Position); 
                    
                    break;

                case 326: 
                    
                    DialogueCue(21 ); 
                    
                    companions[0].LookAtTarget(bosses[0].Position); 
                    
                    break;

                case 329: 
                    
                    DialogueCue(22); 
                    
                    companions[0].LookAtTarget(bosses[0].Position); 
                    
                    break;

                case 332:

                    DialogueCue(23);

                    companions[0].SwitchToMode(Character.Character.mode.track, Game1.player);

                    bosses[0].netPosturing.Set(false);

                    bosses[0].Health = 9999;

                    bosses[0].MaxHealth = 9999;

                    bosses[0].baseJuice = 1;

                    companions[0].ResetActives();

                    companions[0].SmashAttack(bosses[0]);

                    //companions[0].netSmashActive.Set(true);

                    //companions[0].TargetEvent(340, bosses[0].Position - new Vector2(64, 0), true);

                    SetTrack("Cowboy_undead");

                    break;

                //340 blobking fight triggered

                case 341:

                   // DialogueCue(24);

                    //bosses[0].netPosturing.Set(false);

                    //bosses[0].Health = 9999;

                    //bosses[0].MaxHealth = 9999;

                    break;

                case 345:

                    DialogueCue(25 );

                    break;

                case 348:

                    DialogueCue(26);

                    break;

                case 351:

                    DialogueCue(27 );

                    break;

                case 354:

                    DialogueCue(28 );

                    break;

                case 357:

                    DialogueCue(29 );

                    break;

                case 359:

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[0].netSpecial.Set((int)Character.Character.specials.invoke);

                    companions[0].specialTimer = 90;

                    companions[0].LookAtTarget(bosses[0].Position, true);

                    Mod.instance.iconData.DecorativeIndicator(location, companions[0].Position, IconData.decorations.stars, 3f, new() { interval = 1200, });

                    SpellHandle meteor = new(location, bosses[0].Position, Game1.player.Position);

                    meteor.type = SpellHandle.spells.orbital;

                    meteor.missile = IconData.missiles.meteor;

                    meteor.projectile = 4;

                    Mod.instance.spellRegister.Add(meteor);

                    DialogueCue(30);

                    bosses[0].ResetActives();

                    bosses[0].netAlternative.Set(1);

                    bosses[0].PerformFlight(new Vector2(-2,-2)*64, 5);

                    break;

                case 362:

                    DialogueLoad(0, 4);

                    companions[0].netDirection.Set(2);

                    location.characters.Remove(bosses[0]);

                    bosses.Clear();

                    voices.Remove(1);

                    Game1.stopMusicTrack(MusicContext.Default);

                    break;

                case 373:
                case 410:

                    //activeCounter = 400; 
                    activeCounter = 450;

                    break;

                // ------------------------------------------
                // 4.5 Transfer to Atoll
                // ------------------------------------------

                /*case 401:

                    DialogueClear(0);

                    companions[0].TargetEvent(420, atollVector * 64, true);

                    break;

                case 402:

                    DialogueCue(31);

                    inabsentia = true;

                    break;

                case 421:

                    Game1.warpFarmer(LocationData.druid_atoll_name, 14, 10, 1);

                    Game1.xLocationAfterWarp = 14;

                    Game1.yLocationAfterWarp = 10;

                    location = Mod.instance.locations[LocationData.druid_atoll_name];

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_atoll_name], companions[0], new Vector2(16, 11) * 64, false);

                    break;

                case 422:
                case 423:
                case 424:
                case 425:

                    if(Game1.player.currentLocation.Name == LocationData.druid_atoll_name)
                    {

                        inabsentia = false;

                        activeCounter = 450;

                    }

                    break;

                case 426:

                    inabsentia = false;

                    activeCounter = 450;

                    break;*/

                // ------------------------------------------
                // 5 Wisps
                // ------------------------------------------

                case 451:

                    //Game1.ambientLight = new(64, 0, 0);

                    //(location as Atoll).ambientDarkness = true;

                    companions[0].TargetEvent(455, mistVector * 64, true);

                    break;

                case 452:

                    DialogueCue(33);

                    Wisps wispNew = new();

                    wispNew.EventSetup(mistVector * 64, "wisps");

                    wispNew.eventLocked = true;

                    wispNew.EventActivate();

                    wispNew.WispArray();

                    break;

                case 454: DialogueCue(34); break;

                case 457:

                    DialogueLoad(0, 5);

                    break;

                case 473:

                    activeCounter = 500; 
                    
                    break;

                // ------------------------------------------
                // 6 First Farmer / Lady Beyond
                // ------------------------------------------

                case 501:

                    DialogueClear(0);

                    break;

                case 504:

                    companions[0].netDirection.Set(0);

                    break;

                case 505:

                    companions[0].netSpecial.Set((int)Character.Character.specials.invoke);

                    companions[0].specialTimer = 60;

                    break;

                case 506:

                    Vector2 mistCorner = mistVector * 64 - new Vector2(72*3,72*5);

                    List<int> cornersX = new() { 0, 6,};

                    List<int> cornersY = new() { 0, 4, };

                    for (int i = 0; i < 7; i++)
                    {

                        for (int j = 0; j < 5; j++)
                        {

                            if (cornersX.Contains(i) && cornersY.Contains(j))
                            {
                                continue;
                            }

                            Vector2 glowVector = mistCorner + new Vector2(i * 72, j * 72);

                            TemporaryAnimatedSprite glowSprite = new TemporaryAnimatedSprite(0, 5000f, 1, 13, glowVector, false, false)
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

                            location.temporarySprites.Add(glowSprite);

                        }

                    }

                    TemporaryAnimatedSprite cloudAnimation = Mod.instance.iconData.CreateImpact(
                        
                        location, 
                        (mistVector + new Vector2(1,-3)) * 64, 
                        IconData.impacts.spiral, 
                        9f, 
                        new() { interval = 125f, loops = 13*5, flip = true, alpha = 0.1f, layer = mistCorner.Y / 10000 }
                     );

                    companions[2] = new FirstFarmer(CharacterHandle.characters.FirstFarmer);

                    voices[2] = companions[2];

                    companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[2].netDirection.Set(1);

                    if (companions[2].currentLocation.Name != location.Name)
                    {
                        companions[2].currentLocation.characters.Remove(companions[2]);

                        companions[2].currentLocation = location;

                        companions[2].currentLocation.characters.Add(companions[2]);

                    }

                    companions[2].Position = (mistVector + new Vector2(-2, -3)) * 64;

                    companions[3] = new LadyBeyond(CharacterHandle.characters.LadyBeyond);

                    voices[3] = companions[3];

                    companions[3].SwitchToMode(Character.Character.mode.scene,Game1.player);

                    companions[3].netDirection.Set(1);

                    companions[3].flip = true;

                    if (companions[3].currentLocation.Name != location.Name)
                    {
                        companions[3].currentLocation.characters.Remove(companions[3]);

                        companions[3].currentLocation = location;

                        companions[3].currentLocation.characters.Add(companions[3]);

                    }

                    companions[3].Position = (mistVector + new Vector2(2, -2)) * 64;

                    break;

                case 508: 
                    companions[3].netSpecial.Set((int)Character.Character.specials.gesture); companions[3].specialTimer = 90;
                    break;
                case 511: 
                    companions[2].netSpecial.Set((int)Character.Character.specials.gesture); companions[2].specialTimer = 90;
                    break;
                case 514: 
                    companions[3].netSpecial.Set((int)Character.Character.specials.gesture); companions[3].specialTimer = 90;
                    break;
                case 523: 
                    companions[2].netSpecial.Set((int)Character.Character.specials.gesture); companions[2].specialTimer = 90;
                    break;
                case 526: 
                    companions[3].netSpecial.Set((int)Character.Character.specials.gesture); companions[3].specialTimer = 90; 
                    break;
                case 529: 
                    companions[2].netSpecial.Set((int)Character.Character.specials.gesture); companions[2].specialTimer = 90;
                    break;
                case 532: 
                    companions[3].netSpecial.Set((int)Character.Character.specials.gesture); companions[3].specialTimer = 90;
                    break;
                case 535: 
                    companions[2].netSpecial.Set((int)Character.Character.specials.gesture); companions[2].specialTimer = 90; 
                    break;
                case 538: 
                    companions[3].netSpecial.Set((int)Character.Character.specials.gesture); companions[3].specialTimer = 90;
                    break;
                case 547: 
                    companions[2].netSpecial.Set((int)Character.Character.specials.gesture); companions[2].specialTimer = 90;
                    break;
                case 550: 
                    break;
                case 553: 
                    companions[2].netSpecial.Set((int)Character.Character.specials.gesture); companions[2].specialTimer = 90;
                    break;
                case 556: 
                    companions[3].netSpecial.Set((int)Character.Character.specials.gesture); companions[3].specialTimer = 90;
                    break;
                case 559: 
                    companions[3].netDirection.Set(2);
                    break;
                case 561: 
                    companions[2].netDirection.Set(2);
                    companions[3].currentLocation.characters.Remove(companions[3]);
                    companions.Remove(3);
                    break;
                case 564: 
                    companions[2].currentLocation.characters.Remove(companions[2]);
                    companions.Remove(2);
                    break;
                case 570:

                    DialogueLoad(0, 6);

                    break;

                case 582: activeCounter = 600; break;

                // ------------------------------------------
                // 7 Ending
                // ------------------------------------------

                case 601:

                    DialogueClear(0);

                    DialogueCue(35);

                    ThrowHandle throwRelic = new(Game1.player, companions[0].Position, IconData.relics.effigy_crest);

                    throwRelic.register();

                    break;

                case 604:

                    DialogueCue(36);

                    break;

                case 607:

                    DialogueCue(37);

                    break;

                case 609:

                    companions[0].netDirection.Set(1);

                    //Mod.instance.questHandle.CompleteQuest(eventId);

                    companions[0].SwitchToMode(Character.Character.mode.random, Game1.player);

                    eventComplete = true;

                    break;

            }

        }

        public override void EventScene(int index)
        {

            switch (index)
            {

                case 110: // cast position

                    activeCounter = 110;

                    break;

                case 140: // camp position

                    activeCounter = 140;

                    break;

                /*case 300: // Skygazing position

                    activeCounter = 300;

                    break;*/

                case 275: // Skygazing position on Atoll

                    activeCounter = 275;

                    break;

                // 340: // Jellyking position

                //    activeCounter = 340;

                //   break;

                //case 420: // mist position

                //    activeCounter = 420;

                //   break;


            }

        }

    }

}