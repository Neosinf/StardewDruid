using Microsoft.Xna.Framework;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast
{
    internal class Sword : Cast
    {

        private int swordIndex;

        private readonly Quest questData;

        public Sword(Mod mod, Vector2 target, Rite rite, Quest quest)
            : base(mod, target, rite)
        {

            questData = quest;

        }

        public override void CastQuest()
        {

            switch(questData.triggerCast)
            {

                case "CastStars":

                    CastStars();

                    break;

                case "CastWater":

                    CastWater();

                    break;

                default: // CastEarth

                    CastEarth();

                    break;

            }

        }

        public override void CastEarth()
        {

            string effigyQuestion = "Voices in the Rustle of the Leaves: " +
                "^You refer to the one destined to bring balance to the for- rest... between the light...of Spring... the dark... of Winter ...you believe it's this... farmer?";

            List<Response> effigyChoices = new()
            {
                new Response("trigger", "I've come to pay homage to the Kings of Oak and Holly."),

                new Response("none", "(say nothing)")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerEarth);

            targetPlayer.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

        }

        public void AnswerEarth(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "trigger":

                    Game1.activeClickableMenu = new DialogueBox("Two voices speak in unison: \"...Arise...\"");

                    DelayedAction.functionAfterDelay(ThrowEarth, 2000);

                    mod.RemoveTrigger("swordEarth");

                    mod.UpdateQuest("swordEarth", true);

                    break;

                default:

                    Game1.activeClickableMenu = new DialogueBox("The voices disperse into a rustle of laughter and whispered chants.");

                    break;

            }

            return;

        }

        public void ThrowEarth()
        {

            DialogueBox dialogueBox = Game1.activeClickableMenu as DialogueBox;
            
            if(dialogueBox != null)
            {

                dialogueBox.closeDialogue();

            }

            //---------------------- throw Forest Sword

            swordIndex = 15;

            int delayThrow = 600;

            ThrowSword(questData.triggerVector + new Vector2(0, -4), delayThrow);

            //----------------------- cast animation

            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(-3, -2));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(-3, -3));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(-2, -4));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(-1, -5));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(0, -6));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(1, -6));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(2, -5));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(3, -4));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(4, -3));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(4, -2));

        }

        public override void CastWater()
        {
            
            string effigyQuestion = "Voices in the Breaking of the Waves: " +
                "^The river sings again... smish... the spring is clean... smashh... the farmer is friend to the water...";

            List<Response> effigyChoices = new()
            {
                new Response("trigger", "I harken to the Voice Beyond the Shore."),

                new Response("none", "(say nothing)")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerWater);

            targetPlayer.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

        }

        public void AnswerWater(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "trigger":

                    Game1.activeClickableMenu = new DialogueBox("Voice Beyond the Shore: \"And I Hear Your Voice.\"");

                    targetLocation.playSound("thunder_small");

                    DelayedAction.functionAfterDelay(ThrowWater, 2000);

                    mod.RemoveTrigger("swordWater");

                    mod.UpdateQuest("swordWater", true);

                    break;

                default:

                    Game1.activeClickableMenu = new DialogueBox("The voices disperse into the gentle rolling of the waves.");

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

            int delayThrow = 600;

            Vector2 originVector = targetVector + new Vector2(3, 4);

            ThrowSword(originVector, delayThrow);

            //----------------------- strike animations

            ModUtility.AnimateBolt(targetLocation, originVector);

        }


        public override void CastStars()
        {

            string effigyQuestion = "Voices in the Roiling Flames: " +
                "^???? ...it's the farmer... the shadow slayer... unburnt... watch we do...";

            List<Response> effigyChoices = new()
            {
                new Response("trigger", "May the Lights Beyond the Expanse shine upon me."),

                new Response("none", "(say nothing)")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerStars);

            targetPlayer.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

        }

        public void AnswerStars(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "trigger":

                    Game1.activeClickableMenu = new DialogueBox("Voices in the Roiling Flames: \"...Star Caller...\"");

                    DelayedAction.functionAfterDelay(ThrowStars, 2000);

                    mod.RemoveTrigger("swordStars");

                    mod.UpdateQuest("swordStars", true);

                    break;

                default:

                    Game1.activeClickableMenu = new DialogueBox("The voices disperse into the churning of the lava.");

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

            ThrowSword(originVector, delayThrow);

            //---------------------- meteor animation

            ModUtility.AnimateMeteor(targetLocation, originVector, true);

            mod.RemoveTrigger("swordStars");

            mod.UpdateQuest("swordStars", true);

        }


        public void ThrowSword(Vector2 originVector, int delayThrow = 200)
        {
            /*
             * compensate       compensate for downward arc // 555 seems a nice substitute for 0.001 compounded 1000 times
             * 
             * motion           the movement of the animation every millisecond
             * 
             * acceleration     positive Y movement every millisecond creates a downward arc
             * 
             */

            int swordOffset = swordIndex % 8;

            int swordRow = (swordIndex-swordOffset) / 8;

            Rectangle swordRectangle = new(swordOffset*16, swordRow*16, 16, 16);

            Vector2 targetPosition = new(originVector.X * 64, (originVector.Y * 64) - 96);

            Vector2 playerPosition = targetPlayer.Position;

            float animationInterval = 1000f;

            float motionX = (playerPosition.X - targetPosition.X) / 1000;

            float compensate = 0.555f;

            float motionY = ((playerPosition.Y - targetPosition.Y) / 1000) - compensate;

            float animationSort = (originVector.X * 1000) + originVector.Y + 20;

            TemporaryAnimatedSprite throwAnimation = new("TileSheets\\weapons", swordRectangle, animationInterval, 1, 0, targetPosition, flicker: false, flipped: false, animationSort, 0f, Color.White, 4f, 0f, 0f, 0.2f)
            {

                motion = new Vector2(motionX, motionY),

                acceleration = new Vector2(0f, 0.001f),

                timeBasedMotion = true,

                endFunction = CatchSword,

                delayBeforeAnimationStart = delayThrow,

            };

            targetLocation.temporarySprites.Add(throwAnimation);

        }

        public void CatchSword(int EndBehaviour)
        {
            
            Item targetSword = new MeleeWeapon(swordIndex);

            targetPlayer.addItemByMenuIfNecessaryElseHoldUp(targetSword);

        }

    }
}
