using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.TerrainFeatures;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeMists : EventHandle
    {

        public ChallengeMists()
        {

            activeLimit = 75;

            mainEvent = true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            monsterHandle = new(origin, location);

            monsterHandle.spawnSchedule = new();

            for (int i = 1; i <= 12; i++)
            {

                monsterHandle.spawnSchedule.Add(i, new() { new(MonsterHandle.bosses.darkbrute, Boss.temperment.random, Boss.difficulty.medium) });

            }

            monsterHandle.spawnWithin = ModUtility.PositionToTile(origin) - new Vector2(4, 3);

            monsterHandle.spawnRange = new(9, 9);

            EventBar(Mod.instance.questHandle.quests[eventId].title,0);

            EventDisplay trashbar = EventBar(DialogueData.Strings(DialogueData.stringkeys.bomberInterruptions), 0);

            trashbar.colour = Microsoft.Xna.Framework.Color.LightGreen;

            SetTrack("tribal");

            eventProximity = 1280;

            ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

            Mod.instance.spellRegister.Add(new(origin, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Effigy))
            {

                voices[3] = Mod.instance.characters[CharacterHandle.characters.Effigy];

            }

            location.playSound("thunder_small");

            HoldCompanions(180);

        }

        public override float SpecialProgress(int displayId)
        {

            return (float)eventRating / 5;

        }

        public override void EventInterval()
        {

            activeCounter++;

            monsterHandle.SpawnCheck();

            switch (activeCounter)
            {

                case 2:

                    bosses[0] = new DarkShooter(ModUtility.PositionToTile(origin) - new Vector2(2, 2), Mod.instance.CombatDifficulty());

                    bosses[0].SetMode(2);

                    bosses[0].netPosturing.Set(true);

                    location.characters.Add(bosses[0]);

                    bosses[0].currentLocation = location;

                    bosses[0].LookAtFarmer();

                    bosses[0].update(Game1.currentGameTime, location);

                    bosses[0].smashSet = false;

                    voices[0] = bosses[0];

                    Mod.instance.iconData.ImpactIndicator(location, bosses[0].Position, IconData.impacts.impact, 2f, new() { frame = 4,});

                    break;

                case 4:

                    bosses[1] = new DarkBrute(ModUtility.PositionToTile(origin) + new Vector2(2, -1), Mod.instance.CombatDifficulty());

                    bosses[1].SetMode(1);

                    bosses[1].netPosturing.Set(true);

                    location.characters.Add(bosses[1]);

                    bosses[1].currentLocation = location;

                    bosses[1].LookAtFarmer();

                    bosses[1].update(Game1.currentGameTime, location);

                    voices[1] = bosses[1];

                    BossBar(1,1);

                    Mod.instance.iconData.ImpactIndicator(location, bosses[1].Position, IconData.impacts.impact, 2f, new() { frame = 4, });

                    break;

                case 6:

                    bosses[1].netPosturing.Set(false);

                    voices.Remove(1);

                    DialogueCue(900);

                    RepositionShooter();

                    break;

                case 9:

                    PrepareShooter();

                    break;

                case 10:
                case 11:
                case 12:

                    StandbyShooter();

                    break;

                case 13:

                    EngageShooter(13);

                    break;

                case 15:

                    RepositionShooter();

                    DialogueCue(900);

                    break;

                case 18:

                    PrepareShooter();

                    break;

                case 19:
                case 20:
                case 21:

                    StandbyShooter();

                    break;

                case 22:

                    EngageShooter(22);

                    break;

                case 24:

                    RepositionShooter();

                    break;

                case 25:
                case 28:

                    if (monsterHandle.monsterSpawns.Count > 0)
                    {

                        voices[1] = monsterHandle.monsterSpawns.First();

                    }
                    else
                    {

                        cues.Remove(25);
                        cues.Remove(28);
                        cues.Remove(31);

                    }

                    break;

                case 31:

                    PrepareShooter();

                    break;

                case 32:
                case 33:
                case 34:

                    StandbyShooter();

                    break;

                case 35:

                    EngageShooter(35);

                    break;

                case 37:

                    RepositionShooter();

                    break;

                case 38:
                case 40:
                case 42:


                    if (monsterHandle.monsterSpawns.Count > 0)
                    {

                        voices[1] = monsterHandle.monsterSpawns.First();

                    }
                    else
                    {

                        cues.Remove(38);
                        cues.Remove(40);
                        cues.Remove(42);

                    }

                    break;

                case 41:

                    RepositionShooter();

                    break;

                case 45:

                    PrepareShooter();

                    break;

                case 46:
                case 47:

                    StandbyShooter();

                    break;

                case 48:

                    EngageShooter(48);

                    break;

                case 50:

                    if (Mod.instance.questHandle.IsComplete(eventId))
                    {

                        bosses[2] = new DarkRogue(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());

                    }
                    else
                    {
                        bosses[2] = new DarkLeader(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());

                    }

                    bosses[2].SetMode(2);

                    bosses[2].netPosturing.Set(true);

                    location.characters.Add(bosses[2]);

                    bosses[2].smashSet = false;

                    bosses[2].currentLocation = location;

                    bosses[2].LookAtFarmer();

                    bosses[2].update(Game1.currentGameTime, location);

                    voices[2] = bosses[2];

                    bosses[0].SetDirection(origin + new Vector2(-128, -64));

                    bosses[0].PerformFlight(origin + new Vector2(-128,-64));

                    break;

                case 55:

                    bosses[2].SetDirection(Game1.player.Position);

                    bosses[2].netSpecialActive.Set(true);

                    bosses[2].specialTimer = 90;

                    bosses[2].specialFrame = 1;

                    PrepareShooter();

                    break;

                case 56:
                case 57:
                case 58:

                    bosses[2].SetDirection(Game1.player.Position);

                    bosses[2].netSpecialActive.Set(true);

                    bosses[2].specialTimer = 90;

                    bosses[2].specialFrame = 1;
                    
                    StandbyShooter();

                    break;

                case 59:

                    EngageShooter(59);

                    break;

                case 61:

                    bosses[2].SetDirection(origin + new Vector2(384, -384));

                    bosses[2].PerformFlight(origin + new Vector2(384, -384));

                    break;

                case 63:

                    bosses[0].SetDirection(origin + new Vector2(320, -384));

                    bosses[0].PerformFlight(origin + new Vector2(320, -384));

                    break;

                case 65:

                    bosses[0].SetDirection(origin + new Vector2(640, -1280));

                    bosses[0].PerformFlight(origin + new Vector2(640, -1280));

                    bosses[2].LookAtFarmer();

                    bosses[2].netSpecialActive.Set(true);

                    bosses[2].specialTimer = 60;

                    bosses[2].specialFrame = 0;

                    break;

                case 67:

                    bosses[2].SetDirection(origin + new Vector2(640, -1280));

                    bosses[2].PerformFlight(origin + new Vector2(640, -1280));

                    break;

                case 68:

                    eventComplete = true;

                    break;


            }

            if (activeCounter % 5 == 0)
            {

                monsterHandle.SpawnInterval();

            }

            DialogueCue(activeCounter);

        }

        public void RepositionShooter(int point = -1)
        {

            List<Vector2> points = new()
            {
                origin,
                origin + new Vector2(10*64,1*64),
                origin + new Vector2(8*64,10*64),
                origin + new Vector2(-6*64,10*64),

            };

            if(point == -1)
            {

                point = Mod.instance.randomIndex.Next(points.Count);

            }

            bosses[0].SetDirection(points[point]);

            bosses[0].PerformFlight(points[point], 0);

        }

        public void PrepareShooter()
        {

            bosses[0].SetDirection(Game1.player.Position);

            bosses[0].netChannelActive.Set(true);

            bosses[0].specialTimer = 90;

            bosses[0].specialFrame = 0;

            Mod.instance.iconData.CursorIndicator(location, Game1.player.Position, IconData.cursors.scope, new() { scale = 4f, scheme = IconData.schemes.stars, });

        }

        public void StandbyShooter()
        {

            if (bosses[0].netChannelActive.Value)
            {

                bosses[0].SetDirection(Game1.player.Position);

                bosses[0].specialTimer = 90;

                bosses[0].specialFrame = 0;

                Mod.instance.iconData.CursorIndicator(location, Game1.player.Position, IconData.cursors.scope, new() { scale = 4f, scheme = IconData.schemes.stars, });

            }

        }

        public void EngageShooter(int dialogueIndex)
        {

            if (bosses[0].netChannelActive.Value && Vector2.Distance(bosses[0].Position,Game1.player.Position) > 128 )
            {

                bosses[0].PerformChannel(Game1.player.Position);

                if(Mod.instance.randomIndex.Next(2) == 0)
                {
                    
                    DialogueCue(901);

                } else
                {
                    
                    DialogueCue(902);

                }

                for(int i = 0; i < 6; i++)
                {

                    cues.Remove(activeCounter + i);

                }

            }
            else
            {

                eventRating++;

            }

        }

    }

}
