using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Data.IconData;
using static System.Net.Mime.MediaTypeNames;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeEther : EventHandle
    {

        public ChallengeEther()
        {

            activeLimit = 90;

            mainEvent = true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            monsterHandle = new(origin, location);

            monsterHandle.spawnSchedule = new();

            for (int i = 20; i <= 80; i++)
            {

                List<SpawnHandle> dustSpawns = new()
                {
                    new(MonsterHandle.bosses.dustfiend, Boss.temperment.random, (Boss.difficulty)Mod.instance.randomIndex.Next(1,3))
                };

                monsterHandle.spawnSchedule.Add(i, dustSpawns);

            }

            monsterHandle.spawnWithin = new(15, 7);

            monsterHandle.spawnRange = new(24, 21);

            monsterHandle.spawnGroup = true;

            EventBar(DialogueData.Strings(DialogueData.stringkeys.theDusting),0);

            eventProximity = 1280;

            ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

            location.playSound("discoverMineral");

            HoldCompanions();

        }

        public override void RemoveMonsters()
        {

            foreach(KeyValuePair<int,StardewDruid.Monster.Boss> boss in bosses)
            {

                boss.Value.currentLocation.characters.Remove(boss.Value);

            }

            bosses.Clear();

            base.RemoveMonsters();

        }

        public override void EventInterval()
        {

            activeCounter++;

            monsterHandle.SpawnCheck();

            monsterHandle.SpawnInterval();

            if (bosses.Count > 0)
            {

                for (int b = bosses.Count - 1; b >= 0; b--)
                {

                    KeyValuePair<int, StardewDruid.Monster.Boss> boss = bosses.ElementAt(b);

                    if (!ModUtility.MonsterVitals(boss.Value, location))
                    {

                        boss.Value.currentLocation.characters.Remove(boss.Value);

                        bosses.Remove(boss.Key);

                        eventComplete = true;

                    }

                }

            }

            switch (activeCounter)
            {

                case 1:

                    StopTrack();

                    bosses[0] = new Dustfiend(ModUtility.PositionToTile(origin) - new Vector2(1,3), Mod.instance.CombatDifficulty());

                    bosses[0].SetMode(4);

                    bosses[0].netScheme.Set(2);

                    bosses[0].netPosturing.Set(true);

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    voices[0] = bosses[0];

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position, IconData.impacts.puff, 3f, new());

                    break;

                case 16: location.playSound("dustMeep"); break;
                case 18: location.playSound("dustMeep"); break;
                case 20: location.playSound("dustMeep"); break;
                case 22: location.playSound("dustMeep"); SetTrack("cowboy_outlawsong"); break;

                case 45:

                    bosses[0].netPosturing.Set(false);

                    bosses[0].MaxHealth *= 2;

                    bosses[0].Health = bosses[0].MaxHealth;

                    BossBar(0, 0);

                    break;

                case 89:

                    if(bosses.ContainsKey(0))
                    {

                        //Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position, IconData.impacts.deathbomb, 6f, new());

                        Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position, IconData.impacts.bomb, 6f, new());

                        bosses[0].currentLocation.characters.Remove(bosses[0]);

                        bosses.Clear();

                    }

                    break;

                case 90:

                    eventRating = monsterHandle.spawnTotal - monsterHandle.monsterSpawns.Count;

                    eventComplete = true;

                    break;

            }

            DialogueCue(activeCounter);

        }

    }

}
