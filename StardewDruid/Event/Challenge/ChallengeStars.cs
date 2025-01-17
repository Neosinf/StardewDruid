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

namespace StardewDruid.Event.Challenge
{
    public class ChallengeStars : EventHandle
    {

        public ChallengeStars()
        {

            activeLimit = 80;

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

                        new(MonsterHandle.bosses.blobfiend, Boss.temperment.random, Boss.difficulty.medium),

                        new(MonsterHandle.bosses.blobfiend, Boss.temperment.random, Boss.difficulty.basic),

                    };

                    monsterHandle.spawnSchedule.Add(i, blobSpawns);

                }

            }
            else
            {

                for (int i = 1; i <= 15; i++)
                {

                    List<SpawnHandle> blobSpawns = new()
                    {

                        new(MonsterHandle.bosses.blobfiend, Boss.temperment.random, Boss.difficulty.medium),

                        new(MonsterHandle.bosses.blobfiend, Boss.temperment.coward, Boss.difficulty.basic),

                    };

                    monsterHandle.spawnSchedule.Add(i, blobSpawns);

                }

            }

            monsterHandle.spawnWithin = ModUtility.PositionToTile(origin) - new Vector2(7, 6);

            monsterHandle.spawnRange = new(14, 13);

            monsterHandle.spawnGroup = true;

            EventBar(Mod.instance.questHandle.quests[eventId].title, 0);

            EventDisplay slimebar = EventBar(StringData.Strings(StringData.stringkeys.slimesDestroyed), 1);

            slimebar.colour = Microsoft.Xna.Framework.Color.LightGreen;

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 288, IconData.impacts.supree, new()) {sound = SpellHandle.sounds.getNewSpecialItem, });

            Mod.instance.rite.CastMeteors();

            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Effigy))
            {

                voices[1] = Mod.instance.characters[CharacterHandle.characters.Effigy];

            }

            HoldCompanions();

            //EventRender ritePortal = new(eventId, location.Name, origin, IconData.circles.summoning, Microsoft.Xna.Framework.Color.White);

           // eventRenders.Add(ritePortal);

        }

        public override float SpecialProgress(int displayId)
        {

            return (float)eventRating / 30f;

        }

        public override void EventInterval()
        {

            activeCounter++;

            monsterHandle.SpawnCheck();

            eventRating = monsterHandle.spawnTotal - monsterHandle.monsterSpawns.Count;

            if (activeCounter > 5 && activeCounter % 5 == 1)
            {

                monsterHandle.SpawnInterval();

            }

            if (bosses.Count > 0)
            {

                if (!ModUtility.MonsterVitals(bosses[0], location))
                {

                    bosses[0].currentLocation.characters.Remove(bosses[0]);

                    bosses.Clear();

                }

            }

            switch (activeCounter)
            {

                case 1:

                    bosses[0] = new StardewDruid.Monster.Blobfiend(ModUtility.PositionToTile(origin) + new Vector2(3,-3), Mod.instance.CombatDifficulty());

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

                    Mod.instance.spellRegister.Add(new(bosses[0].Position, 288, IconData.impacts.splatter, new()) { scheme = IconData.schemes.herbal_impes,});

                    location.playSound(SpellHandle.sounds.slime.ToString());

                    break;
                case 2:
                    location.playSound(SpellHandle.sounds.slime.ToString());
                    break;
                case 3:
                    location.playSound(SpellHandle.sounds.slime.ToString());
                    break;
                case 4:
                    location.playSound(SpellHandle.sounds.slime.ToString());
                    break;

                case 5:

                    SetTrack("cowboy_outlawsong");
                    break;

                case 17:

                    bosses[0].netPosturing.Set(false);

                    BossBar(0, 0);

                    break;

                case 77:

                    if(bosses.Count > 0)
                    {

                        SpellHandle meteor = new(Game1.player, bosses[0].Position, 8 * 64, 9999);

                        meteor.type = SpellHandle.spells.missile;

                        meteor.missile = MissileHandle.missiles.meteor;

                        meteor.factor =5;

                        meteor.sound = SpellHandle.sounds.explosion;

                        meteor.explosion = 8;

                        meteor.power = 3;

                        Mod.instance.spellRegister.Add(meteor);

                        bosses[0].Halt();

                        bosses[0].idleTimer = 2000;

                        bosses[0].Health = 10;

                    }

                    break;

                case 79:

                    if (bosses.Count > 0)
                    {

                        Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position, IconData.impacts.splatter, 5f, new() { scheme = IconData.schemes.herbal_impes, });

                        bosses[0].currentLocation.characters.Remove(bosses[0]);

                        bosses.Clear();

                    }

                    eventComplete = true;

                    break;

            }

            DialogueCue(activeCounter);

        }

    }

}
