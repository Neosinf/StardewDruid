using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Characters;
using StardewValley.Locations;
using StardewValley.Minigames;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;


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

            CharacterMover.Warp(location, Mod.instance.characters[CharacterHandle.characters.Jester], origin);

            SetupCompanion();

            companions[0] = Mod.instance.characters[CharacterHandle.characters.Jester];

            companions[0].netStandbyActive.Set(true);

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

                // ------------------------------------------
                // 1 Beginning
                // ------------------------------------------

                case 101:

                    Mod.instance.characters[CharacterHandle.characters.Jester].netStandbyActive.Set(false);

                    Mod.instance.characters[CharacterHandle.characters.Jester].LookAtTarget(Game1.player.Position, true);

                    DialogueLoad(0, 2);

                    break;

                case 105:

                    DialogueNext(companions[0]);

                    break;

                case 110:

                    activeCounter = 200;

                    break;

                case 201:

                    DialogueLoad(0, 3);

                    break;

                case 205:

                    DialogueNext(companions[0]);

                    break;

                case 210:

                    activeCounter = 300;

                    break;

                case 301:

                    eventComplete = true;

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