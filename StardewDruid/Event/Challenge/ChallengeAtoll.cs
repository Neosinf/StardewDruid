using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Mists;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Location.Druid;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Monsters;
using StardewValley.Projectiles;
using StardewValley.TerrainFeatures;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using xTile;
using static StardewDruid.Monster.Boss;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeAtoll : EventHandle
    {

        public Dictionary<int,Vector2> eventVectors = new()  
        {

            [0] = new Vector2(48, 11),

            [1] = new Vector2(51, 8),
            [2] = new Vector2(46, 13),

            [3] = new Vector2(44, 6),
            [4] = new Vector2(44, 12),

            [5] = new Vector2(41, 5),
            [6] = new Vector2(41, 12),

            [7] = new Vector2(52, 10),
            [8] = new Vector2(48, 12),

            [9] = new Vector2(54, 14),
            [10] = new Vector2(49, 16),


            [11] = new Vector2(39, 5),
            [21] = new Vector2(44, 19),

            [12] = new Vector2(46, 8),
            [22] = new Vector2(37, 17),

            [13] = new Vector2(44, 25),
            [23] = new Vector2(43, 11),

            [14] = new Vector2(51, 18),
            [24] = new Vector2(36, 14),



        };

        public Dictionary<int,StardewDruid.Monster.DarkPhantom> cannoneers = new();


        public ChallengeAtoll()
        {

            activeLimit = 80;

            mainEvent = true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            monsterHandle = new(origin, location);

            ProgressBar(Mod.instance.questHandle.quests[eventId].title,0);

            //Mod.instance.spellRegister.Add(new(Game1.player.Position, 384, IconData.impacts.supree, new()) { displayRadius = 4, scheme = IconData.schemes.mists, sound = SpellHandle.sounds.getNewSpecialItem, });

            //Mod.instance.spellRegister.Add(new(origin, 128, IconData.impacts.puff, new()) { displayRadius = 2, type = SpellHandle.spells.bolt });

            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Effigy))
            {

                voices[1] = Mod.instance.characters[CharacterHandle.characters.Effigy];

            }

            HoldCompanions();

            location.playSound(SpellHandle.Sounds.thunder.ToString());

            Wisps wispNew = new();

            wispNew.EventSetup(Game1.player, Game1.player.Position, Rite.eventWisps);

            wispNew.EventActivate();

            wispNew.WispArray();

            location.warps.Clear();

            (location as Atoll).ambientDarkness = true;

        }

        public override void EventInterval()
        {

            activeCounter++;

            monsterHandle.SpawnCheck();

            if(activeCounter > 5)
            {

                if(activeCounter % 5 == 1 && monsterHandle.monsterSpawns.Count < 8)
                {

                    int path = Mod.instance.randomIndex.Next(1, 6) * 2;

                    DarkPhantom phantom1 = new(eventVectors[path-1], Mod.instance.CombatDifficulty());

                    monsterHandle.SpawnImport(phantom1, false);

                    phantom1.SetMode(1);

                    phantom1.ResetActives();

                    phantom1.PerformFollow(eventVectors[path] * 64);

                    phantom1.followTimer = 72;

                    phantom1.fadeOut = 0.005f;


                }


            }

            if (bosses.Count > 0)
            {

                if (!ModUtility.MonsterVitals(bosses[0], location))
                {

                    bosses[0].currentLocation.characters.Remove(bosses[0]);

                    bosses.Clear();

                    cues.Clear();

                    eventComplete = true;

                }

            }

            switch (activeCounter)
            {

                case 1:

                    bosses[0] = new DarkPhantom(eventVectors[1], Mod.instance.CombatDifficulty(),"PhantomCaptain");

                    bosses[0].SetMode(3);

                    bosses[0].netPosturing.Set(true);

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    bosses[0].ResetActives();

                    bosses[0].PerformFollow(eventVectors[0] * 64);

                    bosses[0].followTimer = 132;

                    (bosses[0] as DarkPhantom).fadeOut = 0.005f;

                    voices[0] = bosses[0];

                    SpawnData.MonsterDrops(bosses[0], SpawnData.Drops.seafarer);

                    break;

                case 2:

                    location.playSound(SpellHandle.Sounds.thunder_small.ToString());
                    break;

                case 3:

                    bosses[0].ResetActives();

                    break;

                case 5:
                    
                    location.playSound(SpellHandle.Sounds.thunder_small.ToString());

                    SetTrack("PIRATE_THEME");

                    break;

                case 49:

                    bosses[0].netPosturing.Set(false);

                    bosses[0].specialInterval = 30;

                    BossBar(0, 0);

                    break;

                case 79:

                    eventComplete = true;

                    break;


            }

            switch (activeCounter)
            {

                case 10: CannonsAtTheReady(11); break;
                case 13: CannonsToFire(11); break;

                case 28: CannonsAtTheReady(13); break;
                case 31: CannonsToFire(13); break;

                case 43: CannonsAtTheReady(11); break;
                case 46: CannonsToFire(11); break;

                case 58: CannonsAtTheReady(13); break;
                case 61: CannonsToFire(13); break;

                default: break;

            }

            if(activeCounter > 3 && activeCounter < 49)
            {

                DialogueCueWithFeeling(activeCounter);

            }
            else
            {

                DialogueCue(activeCounter);

            }

        }

        public void CannonsAtTheReady(int barrage)
        {

            DialogueCueWithFeeling(994);

            for (int j = 0; j < 2; j++)
            {

                int k = barrage + j;

                if (cannoneers.ContainsKey(k))
                {

                    if (ModUtility.MonsterVitals(cannoneers[k], location))
                    {

                        continue;

                    }

                }

                DarkPhantom phantom1 = new(eventVectors[k], Mod.instance.CombatDifficulty(), "PhantomMate");

                monsterHandle.SpawnImport(phantom1, false);

                phantom1.SetMode(2);

                phantom1.fadeOut = 0.25f;

                phantom1.tether = eventVectors[k] * 64;

                phantom1.tetherLimit = 320;

                phantom1.tempermentActive = temperment.ranged;

                cannoneers[k] = phantom1;

            }

            for (int j = 0; j < 2; j++)
            {

                int k = barrage + j;

                if (!cannoneers.ContainsKey(k))
                {

                    continue;

                }

                cannoneers[k].ResetActives();

                cannoneers[k].LookAtTarget(eventVectors[k + 10] * 64);

                cannoneers[k].netChannelActive.Set(true);

                cannoneers[k].specialTimer = 300;

                Mod.instance.iconData.CursorIndicator(location, eventVectors[k + 10] * 64, IconData.cursors.death, new() { interval = 4000, scale = 4f, scheme = IconData.schemes.bomb_two, alpha = 0.5f, });

            }


            List<string> textList = new()
            {
                 cues[990][0],
                 cues[991][0],
                 cues[992][0],
                 cues[993][0],

            };

            foreach (StardewValley.Monsters.Monster monsterSpawn in monsterHandle.monsterSpawns)
            {

                if (monsterSpawn is DarkPhantom phantom)
                {

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        phantom.showTextAboveHead(textList[Mod.instance.randomIndex.Next(textList.Count)], duration: 2000);

                    }

                }

            }

        }

        public void CannonsToFire(int barrage)
        {

            DialogueCue(995);

            for (int j = 0; j < 2; j++)
            {

                int k = barrage + j;

                if (cannoneers.ContainsKey(k))
                {

                    if (!ModUtility.MonsterVitals(cannoneers[k], location))
                    {

                        continue;

                    }

                    SpellHandle missile = new(location, eventVectors[k + 10] * 64, cannoneers[k].GetBoundingBox().Center.ToVector2(), 320, bosses[0].GetThreat() * 2, Mod.instance.CombatDamage() * 2)
                    {
                        type = SpellHandle.Spells.missile,

                        displayFactor = 3,

                        instant = true,

                        missile = MissileHandle.missiles.cannonball,

                        display = IconData.impacts.bigimpact,

                        displayRadius = 5,

                        scheme = IconData.schemes.stars
                    };

                    Mod.instance.spellRegister.Add(missile);

                }

            }

            //cannoneers.Clear();

            List<string> textList = new()
            {
                 cues[990][0],
                 cues[991][0],
                 cues[992][0],
                 cues[993][0],

            };

            foreach (StardewValley.Monsters.Monster monsterSpawn in monsterHandle.monsterSpawns)
            {

                if (monsterSpawn is DarkPhantom phantom)
                {

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        phantom.showTextAboveHead(textList[Mod.instance.randomIndex.Next(textList.Count)], duration: 2000);

                    }
                    
                }
            
            }

        }

    }

}
