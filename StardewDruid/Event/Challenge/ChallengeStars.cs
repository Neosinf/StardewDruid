﻿using Microsoft.Xna.Framework;
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
    public class ChallengeStars : EventHandle
    {

        public ChallengeStars()
        {

            activeLimit = 60;

            mainEvent = true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            monsterHandle = new(origin, location);

            monsterHandle.spawnSchedule = new();

            if (Mod.instance.questHandle.IsComplete(eventId))
            {

                for (int i = 1; i <= 15; i++)
                {

                    List<SpawnHandle> blobSpawns = new()
                    {

                        new(MonsterHandle.bosses.blobfiend, Boss.temperment.random, Boss.difficulty.hard)

                    };

                    monsterHandle.spawnSchedule.Add(i, blobSpawns);

                }

            }
            else
            {

                for (int i = 1; i <= 15; i++)
                {

                    List<SpawnHandle> blobSpawns = new();

                    blobSpawns.Add(new(MonsterHandle.bosses.blobfiend, Boss.temperment.random, Boss.difficulty.medium));

                    if(Mod.instance.randomIndex.Next(2) == 0)
                    {

                        blobSpawns.Add(new(MonsterHandle.bosses.blobfiend, Boss.temperment.coward, Boss.difficulty.basic));

                    }

                    monsterHandle.spawnSchedule.Add(i, blobSpawns);

                }

            }

            monsterHandle.spawnWithin = ModUtility.PositionToTile(origin) - new Vector2(7, 6);

            monsterHandle.spawnRange = new(14, 13);

            monsterHandle.spawnGroup = true;

            EventBar(Mod.instance.questHandle.quests[eventId].title, 0);

            SetTrack("cowboy_outlawsong");

            eventProximity = 1280;

            ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

            Mod.instance.rite.CastMeteors();

            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Effigy))
            {

                voices[1] = Mod.instance.characters[CharacterHandle.characters.Effigy];

            }

            HoldCompanions();

        }

        public override void EventInterval()
        {

            activeCounter++;

            monsterHandle.SpawnCheck();

            if (activeCounter % 5 == 1)
            {

                monsterHandle.SpawnInterval();

            }

            if (bosses.Count > 0)
            {

                if (!ModUtility.MonsterVitals(bosses[0], location))
                {

                    bosses[0].currentLocation.characters.Remove(bosses[0]);

                    bosses.Clear();

                    cues.Clear();

                }

            }

            switch (activeCounter)
            {

                case 14:

                    bosses[0] = new StardewDruid.Monster.Blobfiend(ModUtility.PositionToTile(origin) - new Vector2(3,3), Mod.instance.CombatDifficulty());

                    bosses[0].netScheme.Set(2);

                    if (Mod.instance.questHandle.IsComplete(eventId))
                    {

                        bosses[0].SetMode(4);

                    }
                    else
                    {

                        bosses[0].SetMode(3);

                    }

                    bosses[0].netPosturing.Set(true);

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    voices[0] = bosses[0];

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position, IconData.impacts.splatter, 5f, new());

                    ModUtility.Explode(location, ModUtility.PositionToTile(bosses[0].Position), Game1.player, 5, 3);

                    break;

                case 37:

                    bosses[0].netPosturing.Set(false);

                    bosses[0].MaxHealth *= 2;

                    bosses[0].Health = bosses[0].MaxHealth;

                    BossBar(0, 0);

                    break;

                case 59:

                    if(bosses.Count > 0)
                    {

                        SpellHandle meteor = new(Game1.player, bosses[0].Position, 8 * 64, Mod.instance.CombatDamage()*4);

                        meteor.type = spells.orbital;

                        meteor.missile = missiles.meteor;

                        meteor.projectile = 5;

                        meteor.sound = sounds.explosion;

                        meteor.explosion = 8;

                        meteor.power = 3;

                        Mod.instance.spellRegister.Add(meteor);

                        bosses[0].Halt();

                        bosses[0].idleTimer = 2000;

                        bosses[0].Health = 1;

                    }

                    break;

                case 60:

                    eventRating = monsterHandle.spawnTotal - monsterHandle.monsterSpawns.Count;

                    eventComplete = true;

                    break;

            }

            DialogueCue(activeCounter);

        }

    }

}
