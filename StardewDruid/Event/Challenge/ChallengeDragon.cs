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

namespace StardewDruid.Event.Challenge
{
    public class ChallengeDragon : EventHandle
    {

        public Warp warpExit;

        public ChallengeDragon()
        {

        }

        public override bool TriggerActive()
        {

            if (TriggerLocation())
            {

                EventActivate();

            }

            return false;

        }

        public override void EventActivate()
        {

            mainEvent = true;

            base.EventActivate();

            EventBar(Mod.instance.questHandle.quests[eventId].title, 0);

            activeLimit = 120;

            SpawnBoss();

        }

        public virtual void SpawnBoss()
        {

            location.warps.Clear();

            warpExit = new Warp(26, 32, "Mine", 17, 6, flipFarmer: false);

            Mod.instance.iconData.ImpactIndicator(location, origin, impacts.smoke, 4f, new());

            bosses[0] = new Dragon(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());

            bosses[0].netScheme.Set(2);

            bosses[0].SetMode(3);

            bosses[0].netHaltActive.Set(true);

            bosses[0].idleTimer = 300;

            location.characters.Add(bosses[0]);

            bosses[0].currentLocation = location;

            bosses[0].update(Game1.currentGameTime, location);

            voices[0] = bosses[0];

            BossBar(0, 0);

            location.playSound("DragonRoar");

            HoldCompanions();

        }

        public override bool AttemptReset()
        {

            if (!eventActive)
            {

                return true;

            }

            if (location.Name == LocationData.druid_lair_name)
            {

                Game1.player.warpFarmer(warpExit, 2);

                DialogueCue(900);

            }

            EventRemove();

            eventActive = false;

            triggerEvent = true;

            return true;

        }

        public override bool EventExpire()
        {
            
            return AttemptReset();

        }


        public override void EventInterval()
        {

            activeCounter++;

            if (bosses.Count > 0)
            {

                if (!ModUtility.MonsterVitals(bosses[0], location))
                {

                    bosses[0].currentLocation.characters.Remove(bosses[0]);

                    bosses.Clear();

                    cues.Clear();

                    eventComplete = true;

                    return;

                }

            }

            if (activeCounter == 2)
            {

                location.playSound("DragonRoar");

            }

            if (activeCounter == 4)
            {

                location.playSound("DragonRoar");

            }

            if(activeCounter == 6)
            {

                SetTrack("cowboy_boss");

            }

            DialogueCue(activeCounter);

        }

    }

}
