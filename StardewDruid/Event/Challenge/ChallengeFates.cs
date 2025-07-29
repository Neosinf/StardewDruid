using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Minigames;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeFates : EventHandle
    {

        public int activeSection;

        public List<Vector2> spawnPoints = new();

        public List<Vector2> retreatPoints = new();

        public Dictionary<int, int> previousModes = new();

        public ChallengeFates()
        {

            activeLimit = 150;

            mainEvent = true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            location.warps.Clear();

            spawnPoints = new()
            {

                new Vector2(30, 16),
                new Vector2(29, 14),
                new Vector2(31, 14),
                new Vector2(33, 16),

            };

            retreatPoints = new()
            {

                new Vector2(33, 13),
                new Vector2(32, 12),
                new Vector2(34, 12),
                new Vector2(36, 13),

            };

            HoldCompanions();

        }

        public override void EventRemove()
        {

            base.EventRemove();

            if (eventActive)
            {

                if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Shadowtin))
                {

                    if (!Mod.instance.questHandle.IsComplete(eventId))
                    {

                        CharacterMover mover = new(CharacterHandle.characters.Shadowtin, CharacterMover.moveType.purge);

                        Mod.instance.movers[CharacterHandle.characters.Shadowtin] = mover;

                    }

                }

            }

        }

        public override void EventInterval()
        {

            activeCounter++;

            //SetTrack("LavaMine");
            SetTrack("tribal");

            switch (activeSection)
            {
                default:
                case 0:

                    EventPartOne();

                    break;

                case 1:

                    EventPartTwo();

                    break;

                case 2:

                    EventPartThree();

                    break;


            }

        }

        public void SpawnBosses()
        {

            // ========================== Leader


            if (Mod.instance.questHandle.IsComplete(eventId))
            {

                DarkBrute darkBrute = new DarkBrute(spawnPoints[0], Mod.instance.CombatDifficulty());

                bosses[0] = darkBrute;

                darkBrute.SetMode(4);

                darkBrute.groupMode = true;

                darkBrute.netPosturing.Set(true);

                darkBrute.setWounded = true;

                location.characters.Add(darkBrute);

                darkBrute.update(Game1.currentGameTime, location);

                voices[3] = darkBrute;


            }
            else
            {

                DarkLeader darkLeader = new DarkLeader(spawnPoints[0], Mod.instance.CombatDifficulty());

                bosses[0] = darkLeader;

                darkLeader.SetMode(3);

                darkLeader.groupMode = true;

                darkLeader.netPosturing.Set(true);

                darkLeader.setWounded = true;

                location.characters.Add(darkLeader);

                darkLeader.update(Game1.currentGameTime, location);

                voices[3] = darkLeader;

            }

            Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position, IconData.impacts.puff, 2f, new() { scheme = IconData.schemes.herbal_celeri, });

            // ========================== Shooter

            DarkShooter darkShooter = new(spawnPoints[1], Mod.instance.CombatDifficulty());

            bosses[1] = darkShooter;

            darkShooter.SetMode(3);

            darkShooter.groupMode = true;

            darkShooter.netPosturing.Set(true);

            darkShooter.setWounded = true;

            location.characters.Add(darkShooter);

            darkShooter.update(Game1.currentGameTime, location);

            voices[4] = darkShooter;

            Mod.instance.iconData.ImpactIndicator(location, bosses[1].Position, IconData.impacts.puff, 2f, new() { scheme = IconData.schemes.herbal_celeri, });

            // ========================== Goblin

            DarkGoblin darkGoblin = new(spawnPoints[2], Mod.instance.CombatDifficulty());

            bosses[2] = darkGoblin;

            darkGoblin.SetMode(3);

            darkGoblin.groupMode = true;

            darkGoblin.netPosturing.Set(true);

            darkGoblin.setWounded = true;

            location.characters.Add(darkGoblin);

            darkGoblin.update(Game1.currentGameTime, location);

            voices[5] = darkGoblin;

            Mod.instance.iconData.ImpactIndicator(location, bosses[2].Position, IconData.impacts.puff, 2f, new() { scheme = IconData.schemes.herbal_celeri, });

            // ========================== Rogue

            Dark darkRogue = new(spawnPoints[3], Mod.instance.CombatDifficulty());

            bosses[3] = darkRogue;

            darkRogue.SetMode(3);

            darkRogue.groupMode = true;

            darkRogue.netPosturing.Set(true);

            darkRogue.setWounded = true;

            location.characters.Add(darkRogue);

            darkRogue.update(Game1.currentGameTime, location);

            voices[6] = darkRogue;

            Mod.instance.iconData.ImpactIndicator(location, bosses[3].Position, IconData.impacts.puff, 2f, new() { scheme = IconData.schemes.herbal_celeri, });

            // =========================== MonsterHandle

            monsterHandle = new(origin, location)
            {
                spawnSchedule = new(),

                spawnGroup = true
            };

            for (int i = 1; i <= 12; i++)
            {

                monsterHandle.spawnSchedule.Add(
                    i,
                    new() {
                        new(MonsterHandle.bosses.darkbrute, Boss.temperment.random, Boss.difficulty.medium),
                    }
                );

            }

            monsterHandle.spawnWithin = ModUtility.PositionToTile(origin) - new Vector2(8, 4);

            monsterHandle.spawnRange = new(16, 7);

            return;

        }

        public void EngageBosses()
        {

            // ========================== Leader

            BossBar(0, 3);

            bosses[0].netPosturing.Set(false);

            // ========================== Shooter

            BossBar(1, 4);

            bosses[1].netPosturing.Set(false);

            // ========================== Goblin

            BossBar(2, 5);

            bosses[2].netPosturing.Set(false);


            // ========================== Rogue

            BossBar(3, 6);

            bosses[3].netPosturing.Set(false);

        }

        public void StayCompanions()
        {

            // The Effigy

            companions[0] = Mod.instance.characters[CharacterHandle.characters.Effigy];

            companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

            CharacterMover.Warp(location, companions[0], Game1.player.Position - new Vector2(128,0));

            companions[0].LookAtTarget(bosses[0].Position);

            companions[0].eventName = eventId;

            voices[0] = companions[0];

            // Jester

            companions[1] = Mod.instance.characters[CharacterHandle.characters.Jester];

            companions[1].SwitchToMode(Character.Character.mode.scene, Game1.player);

            CharacterMover.Warp(location, companions[1], Game1.player.Position + new Vector2(128, 0));

            companions[1].LookAtTarget(bosses[0].Position);

            companions[1].eventName = eventId;

            voices[1] = companions[1];

            // Buffin

            companions[2] = Mod.instance.characters[CharacterHandle.characters.Buffin];

            companions[2].SwitchToMode(Character.Character.mode.scene, Game1.player);

            CharacterMover.Warp(location, companions[2], Game1.player.Position + new Vector2(64, 128));

            companions[2].LookAtTarget(bosses[0].Position);

            companions[2].eventName = eventId;

            voices[2] = companions[2];

            if (Mod.instance.questHandle.IsComplete(eventId))
            {

                companions[3] = Mod.instance.characters[CharacterHandle.characters.Shadowtin];

                companions[3].SwitchToMode(Character.Character.mode.scene, Game1.player);

                CharacterMover.Warp(location, companions[3], Game1.player.Position + new Vector2(-64, 128));

                companions[3].LookAtTarget(bosses[0].Position);

                companions[3].eventName = eventId;

                voices[7] = companions[3];

            }

        }


        public void FreeCompanions()
        {
            
            for (int c = companions.Count - 1; c >= 0; c--)
            {

                companions[c].SwitchToMode(Character.Character.mode.track, Game1.player);

            }

        }

        public void EventPartOne()
        {

            DialogueCueWithFeeling(activeCounter);

            if (activeCounter == 1)
            {

                SpawnBosses();

                StayCompanions();

            }

            if(activeCounter == 24)
            {

                activeSection = 1;

            }

        }

        public void EventPartTwo()
        {

            DialogueCue(activeCounter);

            if (activeCounter == 25)
            {

                ProgressBar(Mod.instance.questHandle.quests[eventId].title, 0);

                EngageBosses();

                FreeCompanions();

            }

            monsterHandle.SpawnCheck();

            if(activeCounter % 8 == 0)
            {

                monsterHandle.SpawnInterval();

            }

            int defeated = 0;

            foreach(KeyValuePair<int, Boss> bossPair in bosses)
            {

                Boss boss = bossPair.Value;

                if (boss.netPosturing.Value)
                {

                    defeated++;

                    continue;

                }

                if (boss.netWoundedActive.Value || !ModUtility.MonsterVitals(boss,location) || activeCounter == 89)
                {

                    boss.ResetActives();

                    boss.Position = retreatPoints[bossPair.Key] * 64;

                    boss.Health = boss.MaxHealth;

                    boss.netPosturing.Set(true);

                    boss.netWoundedActive.Set(true);

                    boss.LookAtTarget(retreatPoints[0]*64);

                    Mod.instance.iconData.AnimateQuickWarp(location, boss.Position);

                    defeated++;

                }

            }

            if(defeated == bosses.Count || activeCounter == 89)
            {

                monsterHandle.ShutDown();

                if (Mod.instance.questHandle.IsComplete(eventId))
                {
                    
                    activeCounter = 150;

                    eventComplete = true; 
                    
                    return;

                }

                activeCounter = 89;

                activeSection = 2;

                StayCompanions();

            }

        }

        public void EventPartThree()
        {

            DialogueCue(activeCounter);

            switch (activeCounter)
            {

                case 122:

                    Mod.instance.iconData.ImpactIndicator(location, bosses[2].Position, IconData.impacts.puff, 2f, new() { scheme = IconData.schemes.herbal_celeri, });

                    bosses[1].currentLocation.characters.Remove(bosses[1]);

                    bosses.Remove(1);

                    voices.Remove(4);
                    break;

                case 123:

                    Mod.instance.iconData.ImpactIndicator(location, bosses[2].Position, IconData.impacts.puff, 2f, new() { scheme = IconData.schemes.herbal_celeri, });

                    bosses[3].currentLocation.characters.Remove(bosses[3]);

                    bosses.Remove(3);

                    voices.Remove(6);

                    break;

                case 124:

                    Mod.instance.iconData.ImpactIndicator(location, bosses[2].Position, IconData.impacts.puff, 2f, new() { scheme = IconData.schemes.herbal_celeri, });

                    bosses[2].currentLocation.characters.Remove(bosses[2]);

                    bosses.Remove(2);

                    voices.Remove(5);

                    bosses[0].currentLocation.characters.Remove(bosses[0]);

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Shadowtin, Character.Character.mode.scene);

                    companions[3] = Mod.instance.characters[CharacterHandle.characters.Shadowtin];

                    CharacterMover.Warp(location, companions[3], bosses[0].Position, false);

                    bosses.Remove(0);

                    voices[3] = companions[3];

                    companions[3].LookAtTarget(Game1.player.Position);

                    break;

                case 126:

                    companions[3].doEmote(28);

                    break;

                case 132:

                    companions[3].doEmote(16);

                    break;

                case 138:

                    DialogueSetups(companions[3], 1);

                    companions[3].LookAtTarget(Game1.player.Position); 
                    
                    break;

                case 145: DialogueSetups(companions[1], 2); companions[3].LookAtTarget(Game1.player.Position); break;

                case 149:

                    ThrowHandle throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Shadowtin].Position, IconData.relics.book_wyrven);

                    throwRelic.register();

                    break;

                case 150: 
                   
                    eventComplete = true; break;

            }

        }

        public override float DisplayProgress(int displayId = 0)
        {

            if (activeCounter <= 24)
            {

                return 0f;

            }

            if(activeCounter >= 90)
            {

                return -1f;

            }

            float progress = ((float)activeCounter - 24f) / (float)66;

            return progress;

        }

    }

}
