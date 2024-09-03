using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Data.IconData;
using static System.Net.Mime.MediaTypeNames;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeEther : EventHandle
    {

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
                };

                monsterHandle.spawnSchedule.Add(i, dustSpawns);

            }

            monsterHandle.spawnWithin = new(15, 10);

            monsterHandle.spawnRange = new(24, 21);

            monsterHandle.spawnGroup = true;

            EventBar(DialogueData.Strings(DialogueData.stringkeys.theDusting),0);

            ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

            location.playSound("discoverMineral");

            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Blackfeather))
            {
                
                if(Mod.instance.characters[CharacterHandle.characters.Blackfeather].currentLocation is Gate)
                {
                    
                    Mod.instance.characters[CharacterHandle.characters.Blackfeather].SwitchToMode(Character.Character.mode.track, Game1.player);

                }

            }

            HoldCompanions();

        }

        public override void EventRemove()
        {

            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Blackfeather))
            {

                if (Mod.instance.characters[CharacterHandle.characters.Blackfeather].modeActive == Character.Character.mode.track && !Mod.instance.questHandle.IsComplete(QuestHandle.questBlackfeather))
                {

                    Mod.instance.characters[CharacterHandle.characters.Blackfeather].SwitchToMode(Character.Character.mode.home, Game1.player);

                }

            }

            base.EventRemove();

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

                case 1:


                    SetTrack("tribal");

                    bosses[0] = new Dustfiend(ModUtility.PositionToTile(origin) - new Vector2(1,3), Mod.instance.CombatDifficulty());

                    bosses[0].SetMode(4);

                    bosses[0].netScheme.Set(2);

                    bosses[0].netPosturing.Set(true);

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    voices[0] = bosses[0];

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0,128), IconData.impacts.smoke, 4f, new() { interval = 150, color = Microsoft.Xna.Framework.Color.Gray,});

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0, 128), IconData.impacts.steam, 4f, new() { interval = 150, });

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0, 128), IconData.impacts.steam, 4f, new() { interval = 150, flip = true, });

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0, 128), IconData.impacts.puff, 4f, new() { interval = 150, });

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position - new Vector2(0, 128), IconData.impacts.plume, 4f, new() { interval = 150, color = Microsoft.Xna.Framework.Color.DarkGray, });

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
                new Vector2(42,16),
                new Vector2(32,23),
                new Vector2(38,23),

            };

            Flyer corvid;

            switch (activeCounter)
            {

                case 91:

                    SetTrack("fall3");

                    corvid = new Flyer(CharacterHandle.characters.ShadowCrow);

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

                    corvid = new Flyer(CharacterHandle.characters.ShadowCrow);

                    corvid.SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, corvid, new Vector2(1, 33));

                    corvid.TargetEvent(0, (ruins[1] * 64) + new Vector2(32, 0), true);

                    //corvid.netMovement.Set((int)Character.Character.movements.run);

                    companions[2] = corvid;

                    break;

                case 94:

                    corvid = new Flyer(CharacterHandle.characters.ShadowCrow);

                    corvid.SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, corvid, new Vector2(53, 1));

                    corvid.TargetEvent(0, (ruins[2] * 64) + new Vector2(32, 0), true);

                    //corvid.netMovement.Set((int)Character.Character.movements.run);

                    companions[3] = corvid;

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 95:

                    corvid = new Flyer(CharacterHandle.characters.ShadowCrow);

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

                    corvid = new Flyer(CharacterHandle.characters.ShadowCrow);

                    corvid.SwitchToMode(Character.Character.mode.scene, Game1.player);

                    CharacterMover.Warp(location, corvid, new Vector2(1, 1));

                    corvid.TargetEvent(0, (ruins[4] * 64) + new Vector2(32, 0), true);

                    //corvid.netMovement.Set((int)Character.Character.movements.run);

                    companions[5] = corvid;

                    location.playSound(SpellHandle.sounds.crow.ToString());

                    break;

                case 98:

                    corvid = new Flyer(CharacterHandle.characters.ShadowCrow);

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

                    Vector2 blackfeather = (ModUtility.PositionToTile(origin) - new Vector2(1, 3)) * 64;

                    CharacterMover.Warp(location, companions[0], blackfeather, false);

                    Mod.instance.iconData.CreateImpact(location, Mod.instance.characters[CharacterHandle.characters.Blackfeather].Position, impacts.plume, 4f, new() { color = Mod.instance.iconData.schemeColours[schemes.npc_blackfeather], alpha = 1f, });

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
