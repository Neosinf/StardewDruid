using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Characters;
using StardewValley.Locations;
using StardewValley.Minigames;
using System;
using System.Collections.Generic;

namespace StardewDruid.Event.Scene
{

    public class ApproachJester : EventHandle
    {

        public ApproachJester()
        {

        }

        public override void SetupCompanion()
        {

            CharacterHandle.CharacterLoad(CharacterHandle.characters.Jester, StardewDruid.Character.Character.mode.scene);

            location = Game1.getLocationFromName(Mod.instance.questHandle.quests[eventId].triggerLocation);

            CharacterMover.Warp(location, Mod.instance.characters[CharacterHandle.characters.Jester], origin);

            companions[0] = Mod.instance.characters[CharacterHandle.characters.Jester];

            companions[0].ResetActives();

            companions[0].eventName = eventId;

            DialogueLoad(companions[0], 1);

        }

        public override bool AttemptReset()
        {

            if (!eventActive)
            {

                return true;

            }

            eventActive = false;

            EventRemove();

            SetupCompanion();

            return true;

        }

        public override void EventInterval()
        {

            activeCounter++;

            switch (activeCounter)
            {

                case 101:

                    Mod.instance.characters[CharacterHandle.characters.Jester].netIdle.Set((int)Character.Character.idles.standby);

                    Mod.instance.characters[CharacterHandle.characters.Jester].LookAtTarget(Game1.player.Position, true);

                    DialogueSetups(companions[0], 2);

                    break;

                case 105:

                    activeCounter = 200;

                    break;

                case 201:

                    DialogueSetups(companions[0], 3);

                    break;

                case 205:

                    activeCounter = 300;

                    break;

                case 301:

                    eventComplete = true;

                    companionModes[0] = Character.Character.mode.track;

                    Mod.instance.characters[CharacterHandle.characters.Jester].SwitchToMode(Character.Character.mode.track, Game1.player);

                    break;

            }

        }


        public override void DialogueResponses(Farmer visitor, string dialogueId)
        {
            
            base.DialogueResponses(visitor, dialogueId);

            List<string> dialogueIndexes = new(dialogueId.Split("."));

            int dialogueIndex = Convert.ToInt32(dialogueIndexes[1]);

            int answerIndex = Convert.ToInt32(dialogueIndexes[2]);

            if(dialogueIndex == 1)
            {

                EventActivate();

                activeCounter = 100;

                return;

            }

        }

    }

}