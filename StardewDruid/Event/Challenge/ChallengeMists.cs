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

        int progressCounter = 0;

        public ChallengeMists()
        {

            activeLimit = -1;

            mainEvent = true;

        }

        public override bool TriggerActive()
        {

            if(Game1.timeOfDay < 1900)
            {

                return false;

            }

            if (TriggerLocation())
            {

                EventActivate();

            }

            return false;

        }

        public override bool AttemptReset()
        {

            if (!eventActive)
            {

                return true;

            }

            if(activeCounter >= 100)
            {

                return false;

            }

            eventActive = false;

            EventRemove();

            triggerEvent = true;

            return true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            location.warps.Clear();

            // Monster handle

            monsterHandle = new(origin, location);

            monsterHandle.spawnSchedule = new();

            for (int i = 1; i <= 20; i++)
            {

                monsterHandle.spawnSchedule.Add(i, new() { new(MonsterHandle.bosses.darkbrute, Boss.temperment.random, Boss.difficulty.medium) });

            }

            monsterHandle.spawnWithin = ModUtility.PositionToTile(origin) - new Vector2(4, 3);

            monsterHandle.spawnRange = new(9, 9);

            // Effigy

            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Effigy))
            {

                voices[3] = Mod.instance.characters[CharacterHandle.characters.Effigy];

            }

            HoldCompanions(180);

            // Voice 0

            bosses[0] = new DarkShooter(ModUtility.PositionToTile(origin) - new Vector2(1, 0), Mod.instance.CombatDifficulty());

            bosses[0].SetMode(2);

            bosses[0].netPosturing.Set(true);

            location.characters.Add(bosses[0]);

            bosses[0].currentLocation = location;

            bosses[0].update(Game1.currentGameTime, location);

            // Voice 4

            bosses[4] = new DarkMage(ModUtility.PositionToTile(origin) + new Vector2(1, 0),Mod.instance.CombatDifficulty());

            bosses[4].SetMode(2);

            bosses[4].netPosturing.Set(true);

            location.characters.Add(bosses[4]);

            bosses[4].currentLocation = location;

            bosses[4].update(Game1.currentGameTime, location);

            // talking

            voices[0] = bosses[0];

            voices[4] = bosses[4];

            bosses[4].SetDirection(bosses[0].Position);

            bosses[0].SetDirection(bosses[4].Position);

            Mod.instance.iconData.AnimateQuickWarp(location, bosses[0].Position);

            Mod.instance.iconData.AnimateQuickWarp(location, bosses[4].Position);

        }

        public override float SpecialProgress(int displayId)
        {

            switch (displayId)
            {

                case 1:

                    return ((float)progressCounter / 80f);

                default:

                    return ((float)eventRating / 7f);


            }

        }


        public override void EventInterval()
        {

            activeCounter++;

            if(activeCounter < 100)
            {

                SegmentOne();

                return;

            }
            else if (activeCounter < 200)
            {

                SegmentTwo();

                return;

            }
            else if (activeCounter < 300)
            {

                SegmentThree();

                return;

            }
            else if (activeCounter < 400)
            {

                SegmentFour();

                return;

            }


        }


        public void SegmentOne()
        {

            if (activeCounter == 1)
            {

                DialogueCue(910);

            }

            if (cues.ContainsKey(activeCounter) && cues[activeCounter].First().Key == 4)
            {

                DialogueCueWithFeeling(activeCounter);

            }
            else
            {

                DialogueCue(activeCounter);

            }

            if (activeCounter == 25)
            {

                activeCounter = 0;

            }

            if(Vector2.Distance(Game1.player.Position,origin) <= 384)
            {

                activeCounter = 100;

            }

        }

        public void SegmentTwo()
        {

            //DialogueCueWithFeeling(activeCounter);

            if (cues.ContainsKey(activeCounter) && cues[activeCounter].First().Key == 4)
            {
                
                DialogueCueWithFeeling(activeCounter);
            
            }
            else
            {
                
                DialogueCue(activeCounter);
            
            }

            BossesAddressPlayer();

            if ((activeCounter >= 109 && activeCounter <= 112))
            {
                
                bosses[4].PerformFlight(origin + new Vector2(320, -2400),5);

            }

            if (activeCounter == 112)
            {

                Mod.instance.iconData.AnimateQuickWarp(location, bosses[4].Position);

                bosses[4].currentLocation.characters.Remove(bosses[4]);

                voices.Remove(4);

                bosses.Remove(4);

                activeCounter = 200;

            }

        }

        public void SegmentThree()
        {
            progressCounter++;

            if(activeCounter == 201)
            {
                
                EventBar(Mod.instance.questHandle.quests[eventId].title, 1);

                EventDisplay bomberbar = EventBar(DialogueData.Strings(DialogueData.stringkeys.bomberInterruptions), 2);

                bomberbar.colour = Microsoft.Xna.Framework.Color.LightGreen;

                SetTrack("tribal");

            }

            monsterHandle.SpawnCheck();

            if(monsterHandle.monsterSpawns.Count > 0)
            {

                voices[1] = monsterHandle.monsterSpawns.First();

            }

            int shooterCycle = activeCounter % 8;

            switch (shooterCycle)
            {

                case 1:
                    
                    PrepareShooter(); 
                    
                    break;
                
                case 2:
                case 3:

                    StandbyShooter();

                    break;

                case 4:

                    EngageShooter();
                    
                    break;
                
                case 6:
                    
                    RepositionShooter();
                    
                    break;

                case 7:

                    DialogueCue(900);

                    break;
            
            }

            if (activeCounter % 5 == 0)
            {

                monsterHandle.SpawnInterval();

            }

            DialogueCue(activeCounter);

            if (activeCounter == 256)
            {

                activeCounter = 300;

            }

        }

        public void SegmentFour()
        {
            progressCounter++;

            DialogueCue(activeCounter);

            switch (activeCounter)
            {

                case 301:

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

                    bosses[0].PerformFlight(origin + new Vector2(-128, -64));

                    break;

                case 307:

                    bosses[2].SetDirection(Game1.player.Position);

                    bosses[2].netSpecialActive.Set(true);

                    bosses[2].specialTimer = 30;

                    bosses[2].specialFrame = 1;

                    PrepareShooter();

                    break;

                case 308:
                case 309:
                case 310:

                    bosses[2].SetDirection(Game1.player.Position);

                    StandbyShooter();

                    break;

                case 311:

                    EngageShooter();

                    break;

                case 316:

                    bosses[0].ResetActives();

                    bosses[0].PerformFlight(origin + new Vector2(120, -240), 5);

                    break;

                case 317:

                    bosses[2].ResetActives();

                    bosses[2].PerformFlight(origin + new Vector2(180, -320), 5);

                    break;

                case 319:

                    bosses[0].ResetActives();

                    bosses[0].PerformFlight(origin + new Vector2(180, -2400),5);

                    break;

                case 321:

                    bosses[2].ResetActives();

                    bosses[2].PerformFlight(origin + new Vector2(180, -2400), 5);

                    break;

                case 322:

                    eventComplete = true;

                    break;

            }

        }

        public void RepositionShooter(int point = -1)
        {

            List<Vector2> points = ModUtility.GetTilesWithinRadius(location, ModUtility.PositionToTile(origin), 7);

            Vector2 reposition = points[Mod.instance.randomIndex.Next(points.Count)] * 64;

            bosses[0].SetDirection(reposition);

            bosses[0].PerformFlight(reposition, 0);

        }

        public void PrepareShooter()
        {

            bosses[0].SetDirection(Game1.player.Position);

            bosses[0].netChannelActive.Set(true);

            bosses[0].specialTimer = 90;

            bosses[0].specialFrame = 0;

            DialogueCue(902 + Mod.instance.randomIndex.Next(3));

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

        public void EngageShooter()
        {

            if (bosses[0].netChannelActive.Value && Vector2.Distance(bosses[0].Position,Game1.player.Position) > 128 )
            {

                bosses[0].PerformChannel(Game1.player.Position);

                DialogueCue(907 + Mod.instance.randomIndex.Next(3));

            }
            else
            {

                DialogueCue(904 + Mod.instance.randomIndex.Next(3));

                eventRating++;

            }

        }

    }

}
