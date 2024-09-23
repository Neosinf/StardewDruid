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

        public List<Vector2> cannonPositions = new();

        public ChallengeAtoll()
        {

            activeLimit = 80;

            mainEvent = true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            monsterHandle = new(origin, location);

            monsterHandle.spawnSchedule = new();

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

            monsterHandle.spawnGroup = true;

            EventBar(Mod.instance.questHandle.quests[eventId].title,0);

            ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

            //Mod.instance.iconData.AnimateBolt(location, origin);

            Mod.instance.spellRegister.Add(new(origin, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Effigy))
            {

                voices[1] = Mod.instance.characters[CharacterHandle.characters.Effigy];

            }

            HoldCompanions();

            location.playSound("thunder_small");

            cannonPositions = new()
            {
                monsterHandle.spawnWithin + new Vector2(1,9),

                monsterHandle.spawnWithin + new Vector2(7,9),

                monsterHandle.spawnWithin + new Vector2(13,9),

                monsterHandle.spawnWithin + new Vector2(-2,4),

                monsterHandle.spawnWithin + new Vector2(4,4),

                monsterHandle.spawnWithin + new Vector2(10,4),

                monsterHandle.spawnWithin + new Vector2(16,4),

            };

            Wisps wispNew = new();

            wispNew.EventSetup(Game1.player.Position, "wisps");

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

                    bosses[0] = new Phantom(ModUtility.PositionToTile(origin) - new Vector2(5), Mod.instance.CombatDifficulty());

                    bosses[0].SetMode(3);

                    bosses[0].netPosturing.Set(true);

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    bosses[0].LookAtFarmer();

                    (bosses[0] as Phantom).fadeFactor = 0.75f;

                    voices[0] = bosses[0];

                    SpawnData.MonsterDrops(bosses[0], SpawnData.drops.seafarer);

                    location.playSound(SpellHandle.sounds.thunder_small.ToString());

                    break;
                case 2:
                    location.playSound(SpellHandle.sounds.thunder_small.ToString());
                    break;
                case 3:
                    location.playSound(SpellHandle.sounds.thunder_small.ToString());
                    break;
                case 4:
                    location.playSound(SpellHandle.sounds.thunder_small.ToString());
                    break;

                case 5:

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

            for (int k = 0; k < cannonPositions.Count; k++)
            {

                Vector2 impact = cannonPositions[k] * 64;

                SpellHandle missile = new(location, impact, new(impact.X > 27 * 64 ? 55 * 64 : 0, impact.Y), 256, bosses[0].GetThreat(), Mod.instance.CombatDamage());

                missile.type = SpellHandle.spells.ballistic;

                missile.projectile = 3;

                missile.missile = missiles.cannonball;

                missile.display = IconData.impacts.bomb;

                missile.scheme = IconData.schemes.death;

                missile.indicator = IconData.cursors.death;

                if (k % 2 == 0)
                {

                    missile.sound = sounds.explosion;

                }

                Mod.instance.spellRegister.Add(missile);

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

            bosses[0].netSpecialActive.Set(true);

            bosses[0].specialTimer = 300;

            bosses[0].specialInterval = 180;

            for (int k = 0; k < cannonPositions.Count; k++)
            {

                Vector2 impact = cannonPositions[k] * 64;

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
