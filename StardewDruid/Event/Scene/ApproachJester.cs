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

        public override void EventInterval()
        {

            activeCounter++;

            switch (activeCounter)
            {

                case 1:

                    location = Game1.getLocationFromName(Mod.instance.questHandle.quests[eventId].triggerLocation);

                    LoadCompanion(CharacterHandle.characters.Jester, origin, 0, 0, false);

                    DialogueLoad(companions[0], 1);

                    break;

                case 5:

                    activeCounter = 2;

                    break;

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