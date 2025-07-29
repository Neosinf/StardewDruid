using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
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

        public int woundedCounter;

        public SwordEther()
        {

        }

        public override void EventActivate()
        {

            mainEvent = true;

            base.EventActivate();

            ProgressBar(Mod.instance.questHandle.quests[eventId].title, 0);

            activeLimit = 120;

            if (!Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Jester))
            {

                Mod.instance.characters[CharacterHandle.characters.Jester].SwitchToMode(Character.Character.mode.track, Game1.player);

            }

            voices[0] = Mod.instance.characters[CharacterHandle.characters.Jester];

            location.warps.Clear();

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

        public override void OnHealthReset()
        {

            DialogueCue(991);

            EventReset();

        }

        public override void LeaveLocations()
        {

            if (Game1.player.currentLocation.Name == LocationHandle.druid_tomb_name)
            {

                Mod.instance.WarpAllFarmers("SkullCave", 5, 5, 1);

            }
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

                        SpellHandle reaperDeath = new(reaper.Position, 320, IconData.impacts.deathbomb, new()) { displayRadius = 4, sound = SpellHandle.Sounds.shadowDie };

                        reaperDeath.sound = SpellHandle.Sounds.shadowDie;

                        Mod.instance.spellRegister.Add(reaperDeath);

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
