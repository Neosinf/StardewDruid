﻿using GenericModConfigMenu;
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;

namespace StardewDruid.Event.Other
{
    internal class Sword
    {

        private int swordIndex;

        private readonly Quest questData;

        private readonly Vector2 targetVector;

        public Sword(Vector2 target,  Quest quest)
        {

            targetVector = target;

            questData = quest;

        }

        public void EventTrigger()
        {

            switch (questData.name)
            {

                case "swordStars":

                    SwordStars();

                    break;

                case "swordWater":

                    SwordWater();

                    break;

                default: // CastEarth

                    SwordEarth();

                    break;

            }

        }

        public void SwordEarth()
        {

            string effigyQuestion = "Voices in the Rustle of the Leaves: " +
                "^...Farmer...";

            List<Response> effigyChoices = new()
            {
                new Response("trigger", "I've come to pay homage to the Kings of Oak and Holly."),

                new Response("none", "(say nothing)")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerEarth);

            Mod.instance.rite.castLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

        }

        public void AnswerEarth(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "trigger":

                    Game1.activeClickableMenu = new DialogueBox("Two voices speak in unison: \"...Arise...\"");

                    DelayedAction.functionAfterDelay(ThrowEarth, 2000);

                    Mod.instance.CompleteQuest("swordEarth");

                    Mod.instance.dialogue["Effigy"].AddSpecial("Effigy", "swordEarth");

                    break;

                default:

                    Game1.activeClickableMenu = new DialogueBox("The voices disperse into a rustle of laughter and whispered chants.");

                    Mod.instance.ReassignQuest("swordEarth");

                    break;

            }

            return;

        }

        public void ThrowEarth()
        {

            DialogueBox dialogueBox = Game1.activeClickableMenu as DialogueBox;

            if (dialogueBox != null)
            {

                dialogueBox.closeDialogue();

            }

            //---------------------- throw Forest Sword

            swordIndex = 15;

            if (Mod.instance.Helper.ModRegistry.IsLoaded("DaLion.Overhaul"))
            {
                swordIndex = 44;
            }

            int delayThrow = 600;

            Vector2 triggerVector = targetVector - new Vector2(2, 1);

            new Throw().ThrowSword(Game1.player, swordIndex, triggerVector, delayThrow);

            //----------------------- cast animation

            Color animateColor = new(0.8f, 1, 0.8f, 1);

            ModUtility.AnimateSparkles(Mod.instance.rite.castLocation, triggerVector + new Vector2(-3, -3), animateColor);
            ModUtility.AnimateSparkles(Mod.instance.rite.castLocation, triggerVector + new Vector2(-3, -4), animateColor);
            ModUtility.AnimateSparkles(Mod.instance.rite.castLocation, triggerVector + new Vector2(-2, -5), animateColor);
            ModUtility.AnimateSparkles(Mod.instance.rite.castLocation, triggerVector + new Vector2(-1, -6), animateColor);
            ModUtility.AnimateSparkles(Mod.instance.rite.castLocation, triggerVector + new Vector2(0, -7), animateColor);
            ModUtility.AnimateSparkles(Mod.instance.rite.castLocation, triggerVector + new Vector2(1, -7), animateColor);
            ModUtility.AnimateSparkles(Mod.instance.rite.castLocation, triggerVector + new Vector2(2, -7), animateColor);
            ModUtility.AnimateSparkles(Mod.instance.rite.castLocation, triggerVector + new Vector2(4, -5), animateColor);
            ModUtility.AnimateSparkles(Mod.instance.rite.castLocation, triggerVector + new Vector2(5, -4), animateColor);
            ModUtility.AnimateSparkles(Mod.instance.rite.castLocation, triggerVector + new Vector2(5, -3), animateColor);

        }

        public void SwordWater()
        {

            string effigyQuestion = "Voices in the Breaking of the Waves: " +
                "^The farmer has cleansed the spring. The river sings again. The farmer is a friend of the water.";

            List<Response> effigyChoices = new()
            {
                new Response("trigger", "I harken to the Voice Beyond the Shore."),

                new Response("none", "(say nothing)")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerWater);

            Mod.instance.rite.castLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

        }

        public void AnswerWater(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "trigger":

                    Game1.activeClickableMenu = new DialogueBox("Voice Beyond the Shore: \"And I Hear Your Call.\"");

                    Mod.instance.rite.castLocation.playSound("thunder_small");

                    DelayedAction.functionAfterDelay(ThrowWater, 2000);

                    Mod.instance.CompleteQuest("swordWater");

                    Mod.instance.dialogue["Effigy"].AddSpecial("Effigy", "swordWater");

                    break;

                default:

                    Game1.activeClickableMenu = new DialogueBox("The voices disperse into the gentle rolling of the waves.");

                    Mod.instance.ReassignQuest("swordWater");

                    break;

            }

            return;

        }

        public void ThrowWater()
        {

            DialogueBox dialogueBox = Game1.activeClickableMenu as DialogueBox;

            if (dialogueBox != null)
            {

                dialogueBox.closeDialogue();

            }

            //---------------------- throw Neptune Glaive

            swordIndex = 14;

            if (Mod.instance.Helper.ModRegistry.IsLoaded("DaLion.Overhaul"))
            {
                swordIndex = 7;
            }

            int delayThrow = 600;

            Vector2 originVector = targetVector + new Vector2(3, 4);

            new Throw().ThrowSword(Game1.player, swordIndex, originVector, delayThrow);

            //----------------------- strike animations

            ModUtility.AnimateBolt(Mod.instance.rite.castLocation, originVector);

        }


        public void SwordStars()
        {

            string effigyQuestion = "Voices in the Roiling Flames: " +
                "Farmer. Shadow slayer. Unburnt one." +
                "^Will you walk under the light?" +
                "^Will you walk over the fire?";

            List<Response> effigyChoices = new()
            {
                new Response("trigger", "May the Lights Beyond the Expanse shine upon me."),

                new Response("none", "(say nothing)")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerStars);

            Mod.instance.rite.castLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

        }

        public void AnswerStars(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "trigger":

                    Game1.activeClickableMenu = new DialogueBox("Voices in the Roiling Flames: \"...Star Caller...\"");

                    DelayedAction.functionAfterDelay(ThrowStars, 2000);

                    Mod.instance.CompleteQuest("swordStars");

                    Mod.instance.dialogue["Effigy"].AddSpecial("Effigy", "swordStars");

                    break;

                default:

                    Game1.activeClickableMenu = new DialogueBox("The voices disperse into the churning of the lava.");

                    Mod.instance.ReassignQuest("swordStars");

                    break;

            }

            return;

        }

        public void ThrowStars()
        {

            DialogueBox dialogueBox = Game1.activeClickableMenu as DialogueBox;

            if (dialogueBox != null)
            {

                dialogueBox.closeDialogue();

            }

            //---------------------- throw Lava Katana

            swordIndex = 9;

            int delayThrow = 600;

            Vector2 originVector = targetVector + new Vector2(5, 0);

            new Throw().ThrowSword(Game1.player, swordIndex, originVector, delayThrow);

            //---------------------- meteor animation

            ModUtility.AnimateMeteor(Mod.instance.rite.castLocation, originVector, true);

        }


    }
}
