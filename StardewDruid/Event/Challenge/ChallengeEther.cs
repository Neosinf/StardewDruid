using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeEther : EventHandle
    {

        public Dictionary<int,Vector2> eventVectors = new()
        {

            // Focus vector
            [1] = new Vector2(27, 21),
            // Boss vector
            [2] = new Vector2(28, 22),

        };

        public ChallengeEther()
        {

            activeLimit = 180;

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
                    //,
                    //new(MonsterHandle.bosses.firefiend, Boss.temperment.random, (Boss.difficulty)Mod.instance.randomIndex.Next(1,3))
                };

                monsterHandle.spawnSchedule.Add(i, dustSpawns);

            }

            monsterHandle.spawnWithin = new(15, 10);

            monsterHandle.spawnRange = new(24, 21);

            monsterHandle.spawnGroup = true;

            EventBar(StringData.Strings(StringData.stringkeys.theDusting),0);

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 288, IconData.impacts.supree, new()) { sound = SpellHandle.sounds.getNewSpecialItem, });

            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Blackfeather))
            {
                
                if(Mod.instance.characters[CharacterHandle.characters.Blackfeather].currentLocation is Gate)
                {
                    
                    Mod.instance.characters[CharacterHandle.characters.Blackfeather].SwitchToMode(Character.Character.mode.track, Game1.player);

                }

            }

            (Mod.instance.locations[LocationHandle.druid_gate_name] as Gate).alightBrazier = true;

            EventRender cannoliBone = new("cannoliBone", location.Name, eventVectors[1] * 64 + new Vector2(32,18), IconData.relics.skull_cannoli) { layer = 1f };

            eventRenders.Add(cannoliBone);

            location.playSound("furnace");

            location.playSound("furnace");

            location.playSound("furnace");

            HoldCompanions();

        }

        public override void EventRemove()
        {

            base.EventRemove();

            if (eventActive)
            {
                
                (Mod.instance.locations[LocationHandle.druid_gate_name] as Gate).alightBrazier = false;

                eventRenders.Clear();

                if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Blackfeather))
                {

                    if (!Mod.instance.questHandle.IsComplete(eventId))
                    {

                        CharacterMover mover = new(CharacterHandle.characters.Blackfeather, CharacterMover.moveType.purge);

                        Mod.instance.movers[CharacterHandle.characters.Blackfeather] = mover;

                    }

                    if (Mod.instance.characters[CharacterHandle.characters.Blackfeather].modeActive == Character.Character.mode.track && !Mod.instance.questHandle.IsComplete(QuestHandle.questBlackfeather))
                    {

                        Mod.instance.characters[CharacterHandle.characters.Blackfeather].SwitchToMode(Character.Character.mode.home, Game1.player);

                    }

                }

            }

        }

        public override void EventInterval()
        {

            activeCounter++;

            DialogueCue(activeCounter);

            if (activeCounter <= 90)
            {

                EventPartOne();

                return;

            }

            EventPartTwo();

        }

        public void EventPartOne()
        {

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

                        voices.Clear();

                        activeCounter = 90;

                    }

                }

            }

            switch (activeCounter)
            {

                case 3:

                    SetTrack("tribal");

                    bosses[0] = new Dustfiend(eventVectors[2], Mod.instance.CombatDifficulty());

                    bosses[0].SetMode(4);

                    bosses[0].netScheme.Set(2);

                    bosses[0].netPosturing.Set(true);

                    bosses[0].netLayer.Set(12000);

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    voices[0] = bosses[0];

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0,128), IconData.impacts.smoke, 5f, new() { interval = 150, color = Microsoft.Xna.Framework.Color.Gray,});

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0, 128), IconData.impacts.steam, 5f, new() { interval = 150, });

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0, 128), IconData.impacts.steam, 5f, new() { interval = 150, flip = true, });

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0, 128), IconData.impacts.puff, 5f, new() { interval = 150, });

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0, 128), IconData.impacts.plume, 5f, new() { interval = 150, color = Microsoft.Xna.Framework.Color.DarkGray, });

                    location.playSound(SpellHandle.sounds.explosion.ToString());

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

                        Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position, IconData.impacts.bomb, 6f, new());

                        bosses[0].currentLocation.characters.Remove(bosses[0]);

                        bosses.Clear();

                        voices.Clear();

                    }

                    break;

                case 90:

                    eventRating = monsterHandle.spawnTotal - monsterHandle.monsterSpawns.Count;

                    RemoveMonsters();

                    break;

            }

        }

        public void EventPartTwo()
        {

            if (Mod.instance.questHandle.IsComplete(eventId))
            {

                eventComplete = true;

                return;

            }

            List<Vector2> ruins = new()
            {
                new Vector2(32,7),
                new Vector2(42,11),
                new Vector2(38,13),
                new Vector2(42,17),
                new Vector2(32,24),
                new Vector2(38,24),

            };

            Flyer corvid;

            switch (activeCounter)
            {

                case 91:


                    SetTrack("fall3");

                    corvid = new Flyer(CharacterHandle.characters.CorvidCrow);

                    corvid.SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, corvid, new Vector2(1,1));

                    corvid.TargetEvent(0, (ruins[0] * 64) + new Vector2(32, 0), true);

                    corvid.netMovement.Set((int)Character.Character.movements.run);

                    companions[1] = corvid;

                    break;

                case 92:

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 93:

                    corvid = new Flyer(CharacterHandle.characters.CorvidCrow);

                    corvid.SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, corvid, new Vector2(1, 33));

                    corvid.TargetEvent(0, (ruins[1] * 64) + new Vector2(32, 0), true);

                    //corvid.netMovement.Set((int)Character.Character.movements.run);

                    companions[2] = corvid;

                    break;

                case 94:

                    corvid = new Flyer(CharacterHandle.characters.CorvidCrow);

                    corvid.SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, corvid, new Vector2(53, 1));

                    corvid.TargetEvent(0, (ruins[2] * 64) + new Vector2(32, 0), true);

                    //corvid.netMovement.Set((int)Character.Character.movements.run);

                    companions[3] = corvid;

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 95:

                    corvid = new Flyer(CharacterHandle.characters.CorvidCrow);

                    corvid.SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, corvid, new Vector2(53, 33));

                    corvid.TargetEvent(0, (ruins[3] * 64) + new Vector2(32, 0), true);

                    //corvid.netMovement.Set((int)Character.Character.movements.run);

                    companions[4] = corvid;

                    break;

                case 96:

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 97:

                    corvid = new Flyer(CharacterHandle.characters.CorvidCrow);

                    corvid.SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, corvid, new Vector2(1, 1));

                    corvid.TargetEvent(0, (ruins[4] * 64) + new Vector2(32, 0), true);

                    //corvid.netMovement.Set((int)Character.Character.movements.run);

                    companions[5] = corvid;

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 98:

                    corvid = new Flyer(CharacterHandle.characters.CorvidCrow);

                    corvid.SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, corvid, new Vector2(53, 1));

                    corvid.TargetEvent(0, (ruins[5] * 64) + new Vector2(32, 0), true);

                    //corvid.netMovement.Set((int)Character.Character.movements.run);

                    companions[6] = corvid;

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 99:

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 100:

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 101:

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Blackfeather, Character.Character.mode.scene);

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Blackfeather];

                    voices[1] = Mod.instance.characters[CharacterHandle.characters.Blackfeather];

                    Vector2 blackfeather = (ModUtility.PositionToTile(origin) - new Vector2(1, 5)) * 64;

                    CharacterMover.Warp(location, companions[0], blackfeather, false);

                    Mod.instance.iconData.CreateImpact(
                        location, 
                        Mod.instance.characters[CharacterHandle.characters.Blackfeather].Position, 
                        IconData.impacts.plume,
                        4f, 
                        new() { 
                            color = Mod.instance.iconData.schemeColours[IconData.schemes.bones], 
                            alpha = 1f, 
                        }
                        );

                    break;

                case 102:

                    companions[0].LookAtTarget(new Vector2(4800, 2040), true);

                    break;

                case 103:

                    companions[0].LookAtTarget(new Vector2(0, 2040), true);

                    break;

                case 104:

                    foreach(KeyValuePair<int,Character.Character> companion in companions)
                    {
                        
                        companion.Value.LookAtTarget(Game1.player.Position, true);

                    }

                    DialogueLoad(companions[0], 1);

                    break;

                case 109:

                    DialogueNext(companions[0]);

                    break;

                case 112:

                    activeCounter = 199;

                    break;

                case 200:

                    foreach (KeyValuePair<int, Character.Character> companion in companions)
                    {

                        companion.Value.LookAtTarget(Game1.player.Position, true);

                    }
                    DialogueSetups(companions[0], 2);

                    break;

                case 205:

                    activeCounter = 299;

                    break;

                case 300:

                    foreach (KeyValuePair<int, Character.Character> companion in companions)
                    {

                        companion.Value.LookAtTarget(Game1.player.Position, true);

                    }

                    DialogueSetups(companions[0], 3);

                    break;

                case 305:

                    activeCounter = 399;

                    break;

                case 400:

                    eventComplete = true;

                    break;

            }

        }

        public override float DisplayProgress(int displayId = 0)
        {

            if (activeCounter <= 24)
            {

                return 0f;

            }

            if (activeCounter >= 90)
            {

                return -1f;

            }

            float progress = ((float)activeCounter - 24f) / (float)66;

            return progress;

        }

    }

}
