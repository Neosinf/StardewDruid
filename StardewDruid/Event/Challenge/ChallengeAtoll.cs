using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Monsters;
using StardewValley.Projectiles;
using StardewValley.TerrainFeatures;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using xTile;
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Data.IconData;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeAtoll : EventHandle
    {

        public Dictionary<int,Vector2> eventVectors = new()  
        {

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

            [101] = new Vector2(47, 3),
            [102] = new Vector2(48, 5),
            [103] = new Vector2(54, 9),

            [201] = new Vector2(47, 19),
            [202] = new Vector2(43, 12),
            [203] = new Vector2(36, 10),
        };

        public ChallengeAtoll()
        {

            activeLimit = 80;

            mainEvent = true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            monsterHandle = new(origin, location);

            /*monsterHandle.spawnSchedule = new();

            for (int i = 1; i <= 12; i++)
            {

                monsterHandle.spawnSchedule.Add(
                    i,
                    new() {
                    new(MonsterHandle.bosses.phantom, Boss.temperment.random, Boss.difficulty.medium),
                    new(MonsterHandle.bosses.phantom, Boss.temperment.random, Boss.difficulty.medium),
                    }
                );

            }

            monsterHandle.spawnWithin = ModUtility.PositionToTile(origin) - new Vector2(8, 4);

            monsterHandle.spawnRange = new(16, 7);

            monsterHandle.spawnGroup = true;*/

            EventBar(Mod.instance.questHandle.quests[eventId].title,0);

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 384, IconData.impacts.nature, new()) { scheme = IconData.schemes.mists, sound = SpellHandle.sounds.getNewSpecialItem, });

            Mod.instance.spellRegister.Add(new(origin, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Effigy))
            {

                voices[1] = Mod.instance.characters[CharacterHandle.characters.Effigy];

            }

            HoldCompanions();

            location.playSound("thunder_small");

            Wisps wispNew = new();

            wispNew.EventSetup(Game1.player.Position, Rite.eventWisps);

            wispNew.EventActivate();

            wispNew.WispArray();

            wispNew.eventLocked = true;

            location.warps.Clear();

            (location as Atoll).ambientDarkness = true;

           // EventRender ritePortal = new(eventId, location.Name, origin, IconData.circles.summoning, Microsoft.Xna.Framework.Color.White);

            //eventRenders.Add(ritePortal);
        
        }

        public override void EventInterval()
        {

            activeCounter++;

            monsterHandle.SpawnCheck();

            if(activeCounter > 5)
            {

                int spawn1 = -1;

                int spawn2 = 2;

                switch (activeCounter % 25)
                {

                    case 1:

                        spawn1 = 1;
                        spawn2 = 2;
                        break;

                    case 6:

                        spawn1 = 3;
                        spawn2 = 4;
                        break;

                    case 11:

                        spawn1 = 5;
                        spawn2 = 6;
                        break;

                    case 16:

                        spawn1 = 7;
                        spawn2 = 8;
                        break;

                    case 21:

                        spawn1 = 9;
                        spawn2 = 10;

                        break;
                }

                if(spawn1 != -1)
                {

                    Phantom phantom1 = new(eventVectors[spawn1], Mod.instance.CombatDifficulty());

                    phantom1.RandomTemperment();

                    monsterHandle.SpawnImport(phantom1, false);

                    phantom1.SetMode(3);

                    phantom1.ResetActives();

                    phantom1.PerformFollow(eventVectors[spawn2] * 64);

                    phantom1.followTimer = 72;

                    phantom1.fadeFactor = 0.005f;


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

                    bosses[0] = new Phantom(eventVectors[1], Mod.instance.CombatDifficulty());

                    bosses[0].SetMode(3);

                    bosses[0].netPosturing.Set(true);

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    bosses[0].ResetActives();

                    bosses[0].PerformFollow(eventVectors[2] * 64);

                    bosses[0].followTimer = 132;

                    (bosses[0] as Phantom).fadeFactor = 0.005f;

                    voices[0] = bosses[0];

                    SpawnData.MonsterDrops(bosses[0], SpawnData.drops.seafarer);

                    break;

                case 2:

                    location.playSound(SpellHandle.sounds.thunder_small.ToString());
                    break;

                case 3:

                    bosses[0].ResetActives();

                    break;

                case 5:
                    
                    location.playSound(SpellHandle.sounds.thunder_small.ToString());

                    bosses[0].ResetActives();

                    bosses[0].netSpecialActive.Set(true);

                    bosses[0].specialTimer = 300;

                    bosses[0].specialInterval = 180;

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

            if (activeCounter % 5 == 0 && activeCounter <= 75)
            {

                monsterHandle.SpawnInterval();

            }

            switch (activeCounter)
            {

                case 10: CannonsAtTheReady(); break;
                case 13: CannonsToFire(); break;

                case 28: CannonsAtTheReady(); break;
                case 31: CannonsToFire(); break;

                case 43: CannonsAtTheReady(); break;
                case 46: CannonsToFire(); break;

                case 58: CannonsAtTheReady(); break;
                case 61: CannonsToFire(); break;

                default: break;

            }

            DialogueCue(activeCounter);

        }

        public void CannonsToFire()
        {

            DialogueCue(995);

            for (int k = 1; k < 4; k++)
            {

                Vector2 impact = eventVectors[200+k] * 64;

                SpellHandle missile = new(location, impact, eventVectors[100 + k] * 64, 256, bosses[0].GetThreat(), Mod.instance.CombatDamage());

                missile.type = SpellHandle.spells.ballistic;

                missile.impactLayer = 999f;

                missile.projectile = 3;

                missile.missile = missiles.cannonball;

                missile.display = IconData.impacts.bomb;

                missile.scheme = IconData.schemes.stars;

                missile.indicator = IconData.cursors.death;

                if (k % 2 == 0)
                {

                    missile.sound = sounds.explosion;

                }

                Mod.instance.spellRegister.Add(missile);

                SpellHandle effect = new(eventVectors[100 + k]*64, 256, impacts.flashbang, new()) { impactLayer = 999f };

                Mod.instance.spellRegister.Add(effect);

                SpellHandle effect2 = new(eventVectors[100 + k] * 64, 256, impacts.plume, new()) { counter = -15, impactLayer = 999f };

                Mod.instance.spellRegister.Add(effect2);
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

                if (monsterSpawn is Phantom phantom)
                {

                    phantom.showTextAboveHead(textList[Mod.instance.randomIndex.Next(textList.Count)], duration: 2000);
                }
            
            }

        }

        public void CannonsAtTheReady()
        {

            DialogueCue(994);

            bosses[0].ResetActives();

            bosses[0].netSpecialActive.Set(true);

            bosses[0].specialTimer = 300;

            bosses[0].specialInterval = 180;

            for (int k = 1; k < 4; k++)
            {

                Vector2 impact = eventVectors[200 + k] * 64;

                CursorAdditional addEffects = new() { interval = 3000, scale = 3, scheme = IconData.schemes.death, alpha = 0.4f, };

                animations.Add(Mod.instance.iconData.CursorIndicator(location, impact, IconData.cursors.death, addEffects));

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

                if (monsterSpawn is Phantom phantom)
                {

                    phantom.showTextAboveHead(textList[Mod.instance.randomIndex.Next(textList.Count)], duration: 2000);
                
                }

            }

        }

    }

}
