using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace StardewDruid.Event.Sword
{
    public class SwordEther : EventHandle
    {

        public int activeSection;

        public Warp warpExit;

        public int woundedCounter;

        public SwordEther()
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

            EventBar("The Tomb of Tyrannus",0);

            activeLimit = 120;

            if (!Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Jester))
            {

                Mod.instance.characters[CharacterHandle.characters.Jester].SwitchToMode(Character.Character.mode.track, Game1.player);

            }

            voices[0] = Mod.instance.characters[CharacterHandle.characters.Jester];

            location.warps.Clear();

            warpExit = new Warp(26, 32, "SkullCave", 5, 5, flipFarmer: false);

            SpawnBoss();

        }

        public virtual void SpawnBoss()
        {

            bosses[0] = new StardewDruid.Monster.Reaper(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());

            bosses[0].netScheme.Set(2);

            bosses[0].SetMode(3);

            bosses[0].netHaltActive.Set(true);

            bosses[0].idleTimer = 300;

            location.characters.Add(bosses[0]);

            bosses[0].update(Game1.currentGameTime, location);

            bosses[0].setWounded = true;

            voices[1] = bosses[0];

            BossBar(0, 1);

            HoldCompanions();

            SetTrack("cowboy_boss");

        }

        public override bool AttemptReset()
        {

            if (!eventActive)
            {

                return true;

            }

            if(Game1.player.currentLocation.Name == LocationData.druid_tomb_name)
            {
                
                DialogueCue(991);

                Game1.player.warpFarmer(warpExit, 2);

                Mod.instance.CastMessage("You managed to escape! Enter the tomb to try again.", 3);

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

                Boss reaper = bosses[0];

                if (reaper.netWoundedActive.Value)
                {

                    if (woundedCounter == 0)
                    {

                        DialogueCue(992);

                        woundedCounter = 4;

                        activeLimit += 4;

                    }

                    woundedCounter--;

                    if (woundedCounter <= 1)
                    {

                        //Mod.instance.iconData.ImpactIndicator(location, reaper.Position, IconData.impacts.deathbomb, 5f, new());

                        Mod.instance.iconData.ImpactIndicator(location, reaper.Position, IconData.impacts.deathbomb, 5f, new() { });

                        reaper.currentLocation.characters.Remove(reaper);

                        bosses.Clear();

                        eventComplete = true;

                    }

                    return;

                }
                else if (!ModUtility.MonsterVitals(reaper, location))
                {

                    reaper.netWoundedActive.Set(true);

                    reaper.Health = reaper.MaxHealth;

                    return;

                }

            }

            DialogueCue(activeCounter);

        }


    }

}
