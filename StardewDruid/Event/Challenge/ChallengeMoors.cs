using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewDruid.Location.Terrain;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.GameData;
using StardewValley.Locations;
using System;
using System.Collections.Generic;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeMoors : EventHandle
    {

        public Dictionary<int,Vector2> eventVectors = new()
        {

            // Aldebaran enter
            [1] = new Vector2(28, 24),
            // Aldebaran move to edge
            [2] = new Vector2(28, 18),
            // Move to stack
            [3] = new Vector2(24, 21),

            // Move to cairn
            [101] = new Vector2(33, 26),
            // Back away
            [102] = new Vector2(31, 26),
            // Look back
            [103] = new Vector2(35, 26),

            // Spawn grid
            [201] = new Vector2(27, 32),
            // Bonewitch spawn
            [202] = new Vector2(27, 20),
            // Aldebaran back to center
            [203] = new Vector2(27, 24),
            // Bonewitch spawn 2
            [204] = new Vector2(21, 32),
            // Bonewitch spawn 3
            [205] = new Vector2(33, 34),
            // Ritual center
            [206] = new Vector2(27, 28),

            // Back to cairn
            [301] = new Vector2(31, 26),
            // Look out over the edge again
            [302] = new Vector2(27, 18),

        };

        public ChallengeMoors()
        {

            activeLimit = -1;

            mainEvent = true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 288, IconData.impacts.supree, new()) { displayRadius = 4, sound = SpellHandle.Sounds.getNewSpecialItem, });

            HoldCompanions();

            if (Mod.instance.eventRegister.ContainsKey(QuestHandle.threatMoors))
            {

                Mod.instance.eventRegister[QuestHandle.threatMoors].eventComplete = true;

            }

        }

        public override void EventRemove()
        {

            List<CharacterHandle.characters> honourList = new()
            {
                CharacterHandle.characters.HonourCaptain,
                CharacterHandle.characters.HonourKnight,
                CharacterHandle.characters.HonourGuard,

            };

            for (int i = 0; i < 3; i++)
            {

                if (!Mod.instance.characters.ContainsKey(honourList[i]))
                {

                    continue;

                }

                Mod.instance.characters[honourList[i]].SwitchToMode(Character.Character.mode.random, Game1.player);

                Mod.instance.characters[honourList[i]].currentLocation.characters.Remove(Mod.instance.characters[honourList[i]]);

                Mod.instance.characters.Remove(honourList[i]);

            }

            if(location != null)
            {

                (location as Moors).ambientDarkness = false;

            }

            base.EventRemove();

        }

        public override void EventInterval()
        {

            activeCounter++;

            // Aldebaran refers to Undervalley and castle
            if (activeCounter < 100)
            {

                EventPartOne();

                return;

            }

            // Aldebaran inters the other golden bones, his former guard
            if (activeCounter < 200)
            {

                EventPartTwo();

                return;

            }

            // Fight against cultists
            if (activeCounter < 300)
            {

                EventPartThree();

                return;

            }

            // Aldebaran releases his former guard
            if (activeCounter < 400)
            {

                EventPartFour();

                return;

            }

            eventComplete = true;


        }

        public void EventPartOne()
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

                    break;

                case 2:

                    companions[0].TargetEvent(0, eventVectors[2] * 64, true);

                    DialogueCue(1); // [1] = new() { [0] = "So it still stands", },

                    break;

                case 5:

                    DialogueCueWithFeeling(2); // [2] = new() { [0] = "The Castle of Two Kings", },

                    break;

                case 8:

                    DialogueCue(3); // [3] = new() { [0] = "Seated above their secret domain", },

                    break;

                case 11:

                    companions[0].ResetActives(true);

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    companions[0].doEmote(8);

                    DialogueCue(4); //[4] = new() { [0] = "The Undervalley", },
  
                    break;

                case 14:

                    companions[0].TargetEvent(0, eventVectors[3] * 64, true);

                    break;

                case 16:

                    DialogueLoad(0, 1);

                    break;

                case 24:

                    activeCounter = 100;

                    break;

            }

        }

        public void EventPartTwo()
        {

            switch (activeCounter)
            {

                case 101:

                    DialogueClear(companions[0]);

                    companions[0].TargetEvent(0, eventVectors[101] * 64, true);

                    break;

                case 105:

                    companions[0].ResetActives();

                    companions[0].netIdle.Set((int)Character.Character.idles.kneel);

                    DialogueCue(101); // [101] = new() { [0] = "To the tranquil halls of the dead", },

                    EventRender goldenCore = new("golden_pot", location.Name, companions[0].Position + new Vector2(32), IconData.relics.golden_pot);

                    goldenCore.layer += 0.64f;

                    goldenCore.scale = 3f;

                    eventRenders.Add(goldenCore);

                    break;

                case 107:

                    companions[0].ResetActives();

                    companions[0].netSpecial.Set((int)Character.Character.specials.pickup);

                    companions[0].specialTimer = 180;

                    break;

                case 108:

                    DialogueCue(102); // [102] = new() { [0] = "I commend the souls of the brave", },

                    eventRenders.Clear();

                    break;

                case 111:

                    companions[0].ResetActives();

                    DialogueCue(103); // [103] = new() { [0] = "...", },

                    break;

                case 114:

                    companions[0].TargetEvent(0, eventVectors[102] * 64, true);

                    companions[0].LookAtTarget(eventVectors[103] * 64, true);

                    DialogueCue(104); //[104] = new() { [0] = "Well that's done", },

                    break;

                case 117:

                    DialogueCue(105); //[105] = new() { [0] = "(sigh)", },

                    break;

                case 120:

                    DialogueLoad(0, 2);

                    break;

                case 128:

                    activeCounter = 200;

                    break;

            }

        }

        public void EventPartThree()
        {

            if (activeCounter > 205 && activeCounter < 285)
            {

                bool challengeOver = true;

                for (int i = 0; i < 3; i++)
                {

                    if (ModUtility.MonsterVitals(bosses[i],location))
                    {

                        challengeOver = false;

                        break;

                    }

                }

                if (challengeOver)
                {

                    activeCounter = 285;

                }

            }


            switch (activeCounter)
            {

                case 201:

                    DialogueClear(companions[0]);

                    LoadBoss(new BoneWitch(eventVectors[202], Mod.instance.CombatDifficulty(), "BoneWitch"), 0, 3, 1);

                    LoadBoss(new BoneWitch(eventVectors[204], Mod.instance.CombatDifficulty(), "PeatWitch"), 1, 3, 3);

                    LoadBoss(new BoneWitch(eventVectors[205], Mod.instance.CombatDifficulty(), "MoorWitch"), 2, 3, 4);

                    bosses[0].LookAtTarget(companions[0].Position);

                    bosses[1].LookAtTarget(companions[0].Position);

                    bosses[2].LookAtTarget(companions[0].Position);

                    Mod.instance.iconData.AnimateQuickWarp(location, bosses[0].Position, false, IconData.warps.death);
                    
                    Mod.instance.iconData.AnimateQuickWarp(location, bosses[1].Position, false, IconData.warps.death);
                    
                    Mod.instance.iconData.AnimateQuickWarp(location, bosses[2].Position, false, IconData.warps.death);

                    companions[0].TargetEvent(0, eventVectors[206] * 64, true);

                    break;

                case 202:

                    DialogueCue(201); //[201] = new() { [1] = "heh", },[201] = new() { [0] = "...again...", },

                    SetTrack("spirits_eve");

                    break;

                case 205:

                    companions[0].LookAtTarget(bosses[0].Position, true);

                    DialogueCueWithFeeling(202); //[202] = new() { [1] = "You are not welcome, fallen one", },

                    break;

                case 206:

                    if (location is Moors moor)
                    {

                        moor.ambientDarkness = true;

                        foreach (TerrainField terrainField in moor.terrainFields)
                        {

                            if (terrainField is RitualCircle circle)
                            {

                                circle.ritualStatus = 1;

                                circle.SetCandles(1);

                            }

                        }

                    }

                    location.playSound(SpellHandle.Sounds.thunder.ToString());

                    Game1.flashAlpha = 1f;
                    
                    break;

                case 208:

                    DialogueCue(203); //[203] = new() { [0] = "(disgust) Cult Witch", },
                    
                    companions[0].netIdle.Set((int)StardewDruid.Character.Character.idles.alert);

                    break;

                case 211:

                    DialogueCueWithFeeling(204); //[204] = new() { [0] = "Save your wicked words for the Fates", },

                    break;

                case 214:

                    DialogueCueWithFeeling(205); //[205] = new() { [0] = "Your destruction nears", },

                    break;

                case 217:

                    DialogueCueWithFeeling(206); //[206] = new() { [1] = "We cast you to the abyss once", },

                    companions[0].SwitchToMode(Character.Character.mode.track, Game1.player);

                    // -------------------------------------------- Chains

                    Snare snareEffect;

                    if (!Mod.instance.eventRegister.ContainsKey(Rite.eventSnare))
                    {

                        snareEffect = new()
                        {
                            eventId = Rite.eventSnare
                        };

                        snareEffect.EventActivate();

                    }
                    else
                    {

                        snareEffect = Mod.instance.eventRegister[Rite.eventSnare] as Snare;

                    }

                    snareEffect.AddVictim(companions[0], 300, SpellHandle.Effects.chain);

                    location.playSound(SpellHandle.Sounds.thunder.ToString());

                    Game1.flashAlpha = 1f;

                    break;

                case 220:

                    DialogueCueWithFeeling(207); //[207] = new() { [1] = "and now again", },

                    bosses[0].netPosturing.Set(false);

                    bosses[0].groupMode = true;

                    bosses[0].tempermentActive = Boss.temperment.cautious;

                    bosses[1].netPosturing.Set(false);

                    bosses[1].groupMode = true;

                    bosses[1].tempermentActive = Boss.temperment.ranged;

                    bosses[2].netPosturing.Set(false);

                    bosses[2].groupMode = true;

                    bosses[2].tempermentActive = Boss.temperment.aggressive;

                    EventBar(Mod.instance.questHandle.quests[eventId].title, 1);

                    break;

                case 223:

                    DialogueCue(208); //[208] = new() { [1] = "...your bones sing to us...", },

                    break;

                case 226:

                    DialogueCue(209); //[209] = new() { [1] = "We claimed them once", },

                    break;
                
                case 229:

                    DialogueCue(210); //[210] = new() { [1] = "and we will do so again", },

                    break;
                
                case 232:

                    DialogueCue(211); //[211] = new() { [1] = "Invader", },

                    break;

                case 235:

                    DialogueCue(212); //[212] = new() { [1] = "DESTROYER", },

                    break;

                case 238:

                    DialogueCue(213); //[209] = new() { [1] = "Manslayer", },

                    break;

                //case 241:
                case 245:

                    Mod.instance.characters[CharacterHandle.characters.HonourCaptain] = new Character.HonourGuard(CharacterHandle.characters.HonourCaptain);

                    Mod.instance.characters[CharacterHandle.characters.HonourCaptain].SwitchToMode(Character.Character.mode.track,Game1.player);

                    voices[2] = Mod.instance.characters[CharacterHandle.characters.HonourCaptain];

                    DialogueCue(214); //[214] = new() { [1] = "...sieze their bones...", },

                    break;

                //case 244:
                case 250:
                    Mod.instance.characters[CharacterHandle.characters.HonourKnight] = new Character.HonourGuard(CharacterHandle.characters.HonourKnight);

                    Mod.instance.characters[CharacterHandle.characters.HonourKnight].SwitchToMode(Character.Character.mode.track, Game1.player);

                    DialogueCue(215); //[211] = new() { [2] = "Knights!", },

                    break;

                //case 247:
                case 255:

                    Mod.instance.characters[CharacterHandle.characters.HonourGuard] = new Character.HonourGuard(CharacterHandle.characters.HonourGuard);

                    Mod.instance.characters[CharacterHandle.characters.HonourGuard].SwitchToMode(Character.Character.mode.track, Game1.player);

                    DialogueCue(216); //[212] = new() { [2] = "Rally to our General", },

                    break;

                //case 250:
                case 260:
                    DialogueCue(217); //[213] = new() { [0] = "The honor guard", },

                    break;

                //case 253:
                case 265:
                    DialogueCue(218); //[214] = new() { [2] = "Hold the rear!", },

                    break;

                //case 256:
                case 270:

                    DialogueCue(219); //[215] = new() { [2] = "...terror!...", },

                    break;

                //case 265:
                case 285:

                    RemoveMonsters();

                    voices.Remove(1);

                    DialogueCue(220); //[216] = new() { [2] = "Victory", },

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[0].eventName = eventId;

                    companions[0].TargetEvent(0, eventVectors[203] * 64, true);

                    break;

                //case 268:
                case 288:

                    List<CharacterHandle.characters> honourList = new()
                    {
                        CharacterHandle.characters.HonourCaptain,
                        CharacterHandle.characters.HonourKnight,
                        CharacterHandle.characters.HonourGuard,

                    };

                    for(int i = 0; i < 3; i++)
                    {

                        if (!Mod.instance.characters.ContainsKey(honourList[i]))
                        {

                            continue;

                        }

                        Mod.instance.iconData.AnimateQuickWarp(location, Mod.instance.characters[honourList[i]].Position, false, Mod.instance.characters[honourList[i]].warpDisplay);

                        Mod.instance.characters[honourList[i]].SwitchToMode(Character.Character.mode.random, Game1.player);

                        Mod.instance.characters[honourList[i]].currentLocation.characters.Remove(Mod.instance.characters[honourList[i]]);

                        Mod.instance.characters.Remove(honourList[i]);

                    }

                    voices.Remove(2);

                    break;

                //case 271:
                case 291:

                    StopTrack();

                    (location as Moors).ambientDarkness = false;

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueLoad(0, 3);

                    break;

                //case 280:

                //    activeCounter = 300;

                //   break;

            }

        }

        public void EventPartFour()
        {

            switch (activeCounter)
            {

                case 301:

                    DialogueClear(companions[0]);

                    companions[0].TargetEvent(0, eventVectors[301] * 64, true);

                    break;

                case 303:

                    DialogueCue(301); // [301] = new() { [0] = "Thank you", },

                    break;

                case 304:

                    companions[0].netIdle.Set((int)Character.Character.idles.kneel);

                    break;

                case 306:

                    DialogueCue(302); // [301] = new() { [0] = "Thank you", },

                    break;

                case 307:

                    companions[0].ResetActives();

                    break;

                case 308:

                    companions[0].TargetEvent(0, eventVectors[302] * 64, true);

                    break;

                case 309:

                    DialogueCue(303); // [301] = new() { [0] = "Thank you", },

                    break;

                case 313:

                    eventComplete = true;

                    break;

            }

        }

        public override float SpecialProgress(int displayId)
        {

            switch (displayId)
            {
                case 1:

                    if(activeCounter > 285)
                    {

                        return -1f;

                    }

                    return (float)(activeCounter - 220) / 65f;

            }

             return -1f;

        }

    }

}
