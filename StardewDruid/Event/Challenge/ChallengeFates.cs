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

namespace StardewDruid.Event.Challenge
{
    public class ChallengeFates : EventHandle
    {

        public StardewDruid.Monster.Dragon terror;

        public Vector2 relicPosition = Vector2.Zero;

        public Warp warpExit;

        public ChallengeFates()
        {

            activeLimit = 120;

            mainEvent = true;

        }

        public override bool TriggerActive()
        {

            if (base.TriggerActive())
            {

                EventActivate();

            }

            return false;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            EventBar("The Terror Beneath",0);

            eventProximity = -1;

            cues = DialogueData.DialogueScene(eventId);

            narrators = DialogueData.DialogueNarrators(eventId);

            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Effigy))
            {

                voices[1] = Mod.instance.characters[CharacterHandle.characters.Effigy];

                Mod.instance.characters[CharacterHandle.characters.Effigy].Halt();

                Mod.instance.characters[CharacterHandle.characters.Effigy].idleTimer = 300;

            }

            location.warps.Clear();

            warpExit = new Warp(26, 32, "Mine", 17, 6, flipFarmer: false);

        }

        public override void EventRemove()
        {

            if (terror != null)
            {

                terror.currentLocation.characters.Remove(terror);

                terror.currentLocation = null;

                terror = null;

            }

            if (location != null)
            {

                location.updateWarps();

            }

            base.EventRemove();

        }

        public override void EventInterval()
        {

            activeCounter++;

            if(terror != null)
            {

                if (!ModUtility.MonsterVitals(terror, location))
                {

                    terror.currentLocation.characters.Remove(terror);

                    terror = null;

                    cues.Clear();

                    eventComplete = true;

                    return;

                }
                else
                {

                    relicPosition = terror.Position;

                }

            }

            if(activeCounter == 1)
            {

                terror = new(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());

                terror.netScheme.Set(2);

                terror.SetMode(3);

                terror.netHaltActive.Set(true);

                terror.idleTimer = 300;

                location.characters.Add(terror);

                terror.update(Game1.currentGameTime, location);

                voices[0] = terror;

                EventDisplay bossBar = Mod.instance.CastDisplay(narrators[0], narrators[0]);

                bossBar.boss = terror;

                bossBar.type = EventDisplay.displayTypes.bar;

                bossBar.colour = Microsoft.Xna.Framework.Color.Red;

                location.playSound("DragonRoar");

            }

            if(activeCounter == 4)
            {

                location.playSound("DragonRoar");

            }
            

            if(activeCounter == 120)
            {

                Game1.player.warpFarmer(warpExit, 2);

                Mod.instance.CastMessage("You managed to escape! Try again tomorrow.", 3);

            }

            DialogueCue(activeCounter);

        }

        public override void EventCompleted()
        {
            
            ThrowHandle throwRelic = new(Game1.player, relicPosition, IconData.relics.runestones_cat);

            throwRelic.register();

            Mod.instance.questHandle.CompleteQuest(eventId);

            (location as Vault).AddCrateTile(24, 10, 1);

            (location as Vault).AddCrateTile(28, 10, 2);

            (location as Vault).AddCrateTile(32, 10, 3);

        }

    }

}
