using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using xTile.Dimensions;
using xTile.Tiles;
using static StardewDruid.Data.IconData;
using static StardewValley.Menus.CharacterCustomization;

namespace StardewDruid.Event.Scene
{
    public class QuestJester : EventHandle
    {

        public Vector2 companionVector;

        public Vector2 buffinVector;

        public Event.Access.AccessHandle MuseumAccess;

        public QuestJester()
        {
            
            mainEvent = true;

            activeLimit = 600;

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
                LocationData.druid_archaeum_name,

            };

            companions[0] = Mod.instance.characters[CharacterHandle.characters.Jester] as StardewDruid.Character.Jester;

            voices[0] = companions[0];

            companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

            companions[0].eventName = eventId;

            CharacterMover.Warp(location, companions[0], origin + new Vector2(128, 128));

            companions[0].netDirection.Set(1);

            ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

            location.playSound("discoverMineral");

        }

        public override bool AttemptReset()
        {

            Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.abortTomorrow), 3, true);

            return false;

        }

        public override void EventInterval()
        {

            activeCounter++;


            if (activeCounter > 900)
            {

                DialogueCue(activeCounter);

                if (activeCounter > 925 && activeCounter <= 964)
                {

                    if (!ModUtility.MonsterVitals(bosses[0], location))
                    {

                        activeCounter = 965;

                    }

                }

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

                    companions[0].netStandbyActive.Set(true);

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

                    companions[0].netStandbyActive.Set(false);

                    companions[0].netWorkActive.Set(true);

                    companions[0].netSpecialActive.Set(true);

                    companions[0].specialTimer = 120;

                    companions[2].Halt();

                    companions[2].netDirection.Set(3);

                    break;

                case 105:

                    companions[0].Position = companions[2].Position - new Vector2(96, 0);

                    companions[0].netStandbyActive.Set(true);

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

                    companions[0].netStandbyActive.Set(false);

                    DialogueCue(10);
                    break;

                case 124:

                    companions[2].netDirection.Set(3);

                    DialogueCue(11);
                    break;

                case 125:

                    DialogueCue(12);
                    break;

                case 127:

                    companions[2].netDirection.Set(2);

                    DialogueCue(13);
                    break;

                case 130:

                    companions[0].netStandbyActive.Set(true);

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

                    DialogueCue(19);

                    ThrowHandle throwRelic = new(companions[2].Position, companions[0].Position, IconData.relics.skull_saurus);

                    throwRelic.impact = IconData.impacts.puff;

                    throwRelic.register();

                    break;

                case 145:

                    DialogueCue(20);

                    Microsoft.Xna.Framework.Rectangle relicRect = Mod.instance.iconData.RelicRectangles(IconData.relics.skull_saurus);

                    TemporaryAnimatedSprite animation = new(0, 5000, 1, 1, companions[0].Position + new Vector2(32,32), false, false)
                    {
                        sourceRect = relicRect,
                        sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                        texture = Mod.instance.iconData.relicsTexture,
                        layerDepth = (companions[0].Position.Y + 65) / 10000,
                        scale = 4f,
                    };

                    location.TemporarySprites.Add(animation);

                    break;

                case 148:

                    DialogueCue(21);

                    break;

                case 151:

                    DialogueCue(22 );

                    break;

                case 154:

                    DialogueCue(23 );

                    companions[2].TargetEvent(302, origin + new Vector2 (448, 64));

                    break;

                case 156:

                    DialogueCue(24 );

                    companions[2].currentLocation.characters.Remove(companions[2]);

                    companions.Remove(2);

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
                    
                    companions[0].netStandbyActive.Set(false);

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

                    break;

                case 306:

                    DialogueCue(32);

                    break;

                case 309:

                    DialogueCue(33);

                    break;

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

                    companions[0].pathActive = Character.Character.pathing.running;

                    // Buffin route

                    Vector2 buffinRace = companions[1].Position + new Vector2(-384, 640);

                    companions[1].TargetEvent(0, buffinRace, true);

                    buffinRace += new Vector2(64, 1152);

                    companions[1].TargetEvent(1, buffinRace, false);

                    buffinRace += new Vector2(2788, 0);

                    companions[1].TargetEvent(2, buffinRace, false);

                    //buffinRace += new Vector2(256, -2624);

                    //companions[1].TargetEvent(3, buffinRace, false);

                    buffinVector = buffinRace;

                    companions[1].pathActive = Character.Character.pathing.running;

                    break;

                case 333:

                    DialogueCue(42);

                    break;

                case 335:

                    DialogueCue(43);

                    break;

                case 370:

                    companions[0].netStandbyActive.Set(true);

                    companions[0].netDirection.Set(1);

                    DialogueCue(44);

                    DialogueLoad(0, 4);

                    break;

                case 371:

                    companions[1].netStandbyActive.Set(true);

                    companions[1].netDirection.Set(3);

                    break;

                case 385:

                    activeCounter = 799;
                    
                    break;

                // --------------------------------------
                // Museum fight (inserted)

                case 800:

                    Vector2 archaeum = companions[0].Position + new Vector2(320, -64);

                    MuseumAccess = new();

                    MuseumAccess.AccessSetup("Town", LocationData.druid_archaeum_name, ModUtility.PositionToTile(archaeum), new Vector2(24, 15));

                    MuseumAccess.location = location;

                    MuseumAccess.AccessStair();

                    location.localSound("secret1");

                    DialogueClear(0);

                    companions[0].netStandbyActive.Set(false);

                    companions[0].TargetEvent(203, archaeum, true);

                    companions[1].netStandbyActive.Set(false);

                    companions[1].TargetEvent(0, archaeum, true);

                    DialogueCue(45);

                    break;

                case 810:

                    Game1.warpFarmer(LocationData.druid_archaeum_name, 27, 18, 1);

                    Game1.xLocationAfterWarp = 27;

                    Game1.yLocationAfterWarp = 18;

                    inabsentia = true;

                    location = Mod.instance.locations[LocationData.druid_archaeum_name];

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_archaeum_name], companions[0], new Vector2(24, 15) * 64, false);

                    companions[0].netStandbyActive.Set(true);

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_archaeum_name], companions[1], new Vector2(31, 15) * 64, false);

                    companions[1].netStandbyActive.Set(true);

                    companions[1].netDirection.Set(3);

                    Vector2 GuntherPosition = new Vector2(27, 14)* 64;

                    companions[3] = new Gunther(CharacterHandle.characters.Gunther);

                    companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, companions[3], GuntherPosition);

                    companions[3].eventName = eventId;

                    companions[3].netDirection.Set(2);

                    voices[3] = companions[3];

                    MuseumAccess.AccessWarps();

                    break;

                case 811:
                case 812:
                case 813:
                case 814:

                    if (Game1.player.currentLocation.Name == LocationData.druid_archaeum_name)
                    {

                        inabsentia = false;

                        activeCounter = 899;

                    }

                    break;

                case 815:

                    inabsentia = false;

                    activeCounter = 899;

                    break;

                // --------------------------------------
                // Museum fight

                case 900:

                    Microsoft.Xna.Framework.Rectangle relicRectTwo = Mod.instance.iconData.RelicRectangles(IconData.relics.skull_saurus);

                    TemporaryAnimatedSprite animationTwo = new(0, 20000, 1, 1, new Vector2(27,15)*64 + new Vector2(32), false, false)
                    {
                        sourceRect = relicRectTwo,
                        sourceRectStartingPos = new(relicRectTwo.X, relicRectTwo.Y),
                        texture = Mod.instance.iconData.relicsTexture,
                        layerDepth = 900f,
                        scale = 4f,
                    };

                    location.TemporarySprites.Add(animationTwo);

                    break;

                case 916:

                    companions[0].ResetActives(true);

                    companions[0].netDirection.Set(1);

                    companions[1].ResetActives(true);

                    companions[1].netDirection.Set(3);

                    Mod.instance.iconData.DecorativeIndicator(location, new Vector2(27,15) * 64 + new Vector2(32), IconData.decorations.fates, 3f, new() { interval = 6000 });

                    break;

                case 918:

                    Mod.instance.spellRegister.Add(new(new Vector2(27,15) * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt, scheme = IconData.schemes.fates, projectile = 1 });

                    break;

                case 920:

                    Mod.instance.spellRegister.Add(new(new Vector2(27,15) * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt, scheme = IconData.schemes.fates, projectile = 1 });

                    break;

                case 921:

                    StardewDruid.Monster.Dinosaur dinosaur = new(new Vector2(27,16), Mod.instance.CombatDifficulty());

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

                    companions[3].pathActive = Character.Character.pathing.running;

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

                    Mod.instance.spellRegister.Add(new(bosses[0].Position, 320, IconData.impacts.deathbomb, new()));

                    ThrowHandle newThrowRelic = new(bosses[0].Position, companions[1].Position, IconData.relics.skull_saurus);

                    newThrowRelic.impact = IconData.impacts.puff;

                    newThrowRelic.register();

                    location.characters.Remove(bosses[0]);

                    bosses.Clear();

                    voices.Remove(4);

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[0].netStandbyActive.Set(true);

                    Mod.instance.characters.Remove(CharacterHandle.characters.Buffin);

                    companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[1].netStandbyActive.Set(true);

                    break;

                case 969:

                    companions[0].ResetActives();

                    companions[0].TargetEvent(0, new Vector2(27, 32) * 64);

                    companions[1].ResetActives();

                    companions[1].TargetEvent(0, new Vector2(27, 32) * 64);

                    break;

                case 972:

                    Vector2 outsideTile = ModUtility.PositionToTile(companionVector);

                    Game1.warpFarmer("Town", (int)outsideTile.X+1, (int)outsideTile.Y + 2, 1);

                    Game1.xLocationAfterWarp = (int)outsideTile.X + 1;

                    Game1.yLocationAfterWarp = (int)outsideTile.Y + 2;

                    inabsentia = true;

                    location = Game1.getLocationFromName("Town");

                    CharacterMover.Warp(location, companions[0], companionVector, false);//new Vector2(100, 56) * 64, false);

                    buffinVector = companionVector + new Vector2(192, 0);

                    CharacterMover.Warp(location, companions[1], buffinVector, false);//new Vector2(102, 56) * 64, false);

                    companions[0].netDirection.Set(1);

                    companions[1].netDirection.Set(3);

                    break;

                case 973:
                case 974:
                case 975:
                case 976:

                    if (Game1.player.currentLocation.Name == "Town")
                    {

                        activeCounter = 399;

                    }

                    break;

                case 977:

                    activeCounter = 399;

                    break;

                // --------------------------------------
                // Cat fight

                case 400:

                    DialogueClear(0);

                    companions[0].netStandbyActive.Set(false);

                    companions[1].netStandbyActive.Set(false);

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

                    DialogueCue(53 );

                    companions[0].doEmote(16);

                    break;

                case 424:

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

                    companions[1].Position = companions[1].Position + new Vector2(196, 0);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position - new Vector2(0.0f, 32f));

                    break;

                case 436:

                    DialogueCue(58);

                    companions[0].Position = companions[0].Position + new Vector2(-196, 0);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[0].Position - new Vector2(0.0f, 32f));

                    break;

                case 438:

                    companions[0].ResetActives(true);

                    companions[0].TargetEvent(0, companions[1].Position + new Vector2(64, -32),true);

                    companions[0].SetDash(companions[1].Position + new Vector2(64, -32),false);
                    
                    companions[1].ResetActives(true);

                    companions[1].TargetEvent(0, companions[0].Position - new Vector2(64, 32),true);

                    companions[1].SetDash(companions[0].Position - new Vector2(64, 32),false);

                    Mod.instance.iconData.ImpactIndicator(location, companions[0].Position + new Vector2(352,32), Data.IconData.impacts.flashbang, 3, new());

                    break;

                case 440:

                    companions[0].LookAtTarget(companions[1].Position);

                    companions[1].LookAtTarget(companions[0].Position);

                    break;

                case 441:

                    DialogueCue(59);

                    companions[0].ResetActives(true);

                    companions[0].TargetEvent(0, companions[1].Position - new Vector2(64, 32), true);

                    companions[0].SetDash(companions[1].Position - new Vector2(64, 32), false);

                    companions[1].ResetActives(true);

                    companions[1].TargetEvent(0, companions[0].Position + new Vector2(64, -32), true);

                    companions[1].SetDash(companions[0].Position + new Vector2(64, -32), false);

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

                    companions[0].netSpecialActive.Set(true);

                    companions[0].specialTimer = 120;

                    SpellHandle beam = new(companions[0].currentLocation, companions[1].GetBoundingBox().Center.ToVector2(), companions[0].GetBoundingBox().Center.ToVector2());

                    beam.type = SpellHandle.spells.beam;

                    beam.scheme = schemes.ether;

                    Mod.instance.spellRegister.Add(beam);

                    // Buffin beam

                    companions[1].ResetActives(true);

                    companions[1].netSpecialActive.Set(true);

                    companions[1].specialTimer = 120;

                    SpellHandle beamTwo = new(companions[1].currentLocation, companions[0].GetBoundingBox().Center.ToVector2(), companions[1].GetBoundingBox().Center.ToVector2());

                    beamTwo.type = SpellHandle.spells.beam;

                    beamTwo.scheme = schemes.fates;

                    Mod.instance.spellRegister.Add(beamTwo);

                    Mod.instance.iconData.ImpactIndicator(location, companions[0].Position + new Vector2(416, 32), Data.IconData.impacts.flashbang, 3, new());

                    break;

                case 447:

                    Vector2 cornerVector = companionVector + new Vector2(0, -448);

                    for (int i = 1; i < 10; i++)
                    {

                        for (int j = 1; j < 6; j++)
                        {

                            Vector2 burnVector = cornerVector + new Vector2((i * 64), (j * 64)) - new Vector2(16 * Mod.instance.randomIndex.Next(0, 4), 16 * Mod.instance.randomIndex.Next(0, 4));

                            Mod.instance.iconData.EmberConstruct(location, IconData.schemes.stars, burnVector, Mod.instance.randomIndex.Next(2,5), Mod.instance.randomIndex.Next(3), 60, 999f);

                            if(i % 3 == 0 && j % 3 == 0)
                            {

                                TemporaryAnimatedSprite lightCircle = new(23, 200f, 6, 60, burnVector, false, Game1.random.NextDouble() < 0.5)
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

                    companions[0].netStandbyActive.Set(true);

                    companions[0].netDirection.Set(1);

                    break;

                case 523:

                    companions[1].netStandbyActive.Set(true);

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

                    companions[1].netStandbyActive.Set(false);

                    companions[1].TargetEvent(0, companions[0].Position + new Vector2(32, 0));

                    DialogueCue(81);

                    break;

                case 564:

                    companions[0].netStandbyActive.Set(false);

                    DialogueCue(82);

                    break;

                case 567:

                    companions[0].netStandbyActive.Set(true);

                    Mod.instance.iconData.AnimateQuickWarp(location, companions[1].Position - new Vector2(0.0f, 32f), true);

                    companions[1].currentLocation.characters.Remove(companions[1]);

                    companions[1] = null;

                    DialogueLoad(0, 6);

                    break;

                case 575:

                    activeCounter = 599;

                    break;

                case 600:

                    DialogueCue(83);

                    companions[0].SwitchToMode(Character.Character.mode.random,Game1.player);

                    companions[0].TargetIdle(10000);

                    eventComplete = true;

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
