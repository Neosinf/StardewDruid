using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Journal
{
    public class QuestPage : DruidJournal
    {


        public QuestPage(string QuestId, int Record) : base(QuestId, Record) 
        {

        }

        public override void populateInterface()
        {

            parentJournal = journalTypes.quests;

            type = journalTypes.questPage;

            interfaceComponents = new()
            {
                
                [101] = addButton(journalButtons.viewEffect),

                [102] = addButton(journalButtons.replayQuest),

                [201] = addButton(journalButtons.back),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public override void populateContent()
        {

            string questId = journalId;

            // ---------------------------- quest

            bool isActive = Mod.instance.save.progress[questId].status == 1 || Mod.instance.save.progress[questId].status == 4;

            bool isComplete = Mod.instance.questHandle.IsComplete(questId);

            bool isReplayed = Mod.instance.save.progress[questId].status == 4;

            Quest questRecord = Mod.instance.questHandle.quests[questId];

            int questReward = questRecord.reward;


            // ----------------------------- title

            int textHeight = 48;

            //contentComponents[101] = new(ContentComponent.contentTypes.title, questRecord.title);

            //contentComponents[101].setBounds(0, xPositionOnScreen, yPositionOnScreen + textHeight, width, 64);

            //textHeight += contentComponents[101].bounds.Height;

            title = questRecord.title;

            int start = 0;

            // ----------------------------- description

            contentComponents[start] = new(ContentComponent.contentTypes.text, "description");

            contentComponents[start].text[0] = questRecord.description;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width-128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            // ------------------------------ conditional description

            if (isActive && !Context.IsMainPlayer && questRecord.type != Quest.questTypes.lesson)
            {

                contentComponents[start] = new(ContentComponent.contentTypes.text, "hostonly");

                contentComponents[start].text[0] = DialogueData.Strings(StardewDruid.Data.DialogueData.stringkeys.hostOnly);

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkRed;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }
            
            if (isReplayed)
            {
                
                contentComponents[start] = new(ContentComponent.contentTypes.text, "questReplay");

                contentComponents[start].text[0] = DialogueData.Strings(StardewDruid.Data.DialogueData.stringkeys.questReplay);

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkRed;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            // ------------------------------ instructions

            if(isComplete && !isReplayed && questRecord.explanation != null)
            {

                contentComponents[start] = new(ContentComponent.contentTypes.text, "instruction");

                contentComponents[start].text[0] = questRecord.explanation;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }
            else if (questRecord.instruction != null)
            {

                contentComponents[start] = new(ContentComponent.contentTypes.text, "instruction");

                contentComponents[start].text[0] = questRecord.instruction;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            // ------------------------------ conditional instructions

            if (questRecord.type == Quest.questTypes.lesson)
            {

                string completeString = "";

                string lessonProgress =
                            completeString +
                            Mod.instance.save.progress[questId].progress.ToString() + " " +
                            DialogueData.Strings(DialogueData.stringkeys.outOf) + " " +
                            questRecord.requirement.ToString() + " " +
                            questRecord.progression;

                if (isComplete)
                {
                    
                    completeString = DialogueData.Strings(DialogueData.stringkeys.mastered);

                    if(Mod.instance.save.progress[questId].progress < questRecord.requirement)
                    {

                        lessonProgress = DialogueData.Strings(DialogueData.stringkeys.lessonSkipped);

                    }

                }

                contentComponents[start] = new(ContentComponent.contentTypes.text, "progress");

                contentComponents[start].text[0] = lessonProgress;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            if (questReward > 0)
            {

                if(isReplayed) { questReward *= 3; questReward /= 2; }

                float adjustReward = 1.2f - ((float)Mod.instance.ModDifficulty() * 0.1f);

                questReward = (int)((float)questReward * adjustReward);

                contentComponents[start] = new(ContentComponent.contentTypes.text, "lesson");

                contentComponents[start].text[0] = DialogueData.Strings(StardewDruid.Data.DialogueData.stringkeys.reward) + ": " + questReward.ToString() + "g";

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            if (isActive && isReplayed && questRecord.replay != null)
            {

                contentComponents[start] = new(ContentComponent.contentTypes.text, "replay");

                contentComponents[start].text[0] = DialogueData.Strings(StardewDruid.Data.DialogueData.stringkeys.replayReward) + ": " + questRecord.replay;

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            textHeight += 16;

            contentBox = new(xPositionOnScreen, yPositionOnScreen + 64, width, textHeight);

        }

        public override void activateInterface()
        {


            if (Mod.instance.save.progress[journalId].status <= 1)
            {

                interfaceComponents[102] = addButton(journalButtons.skipQuest);

            }
            else if (Mod.instance.questHandle.IsReplayable(journalId))
            {

                switch (Mod.instance.save.progress[journalId].status)
                {

                    case 2:

                        interfaceComponents[102] = addButton(journalButtons.replayQuest);

                        break;
                    case 3:

                        interfaceComponents[102] = addButton(journalButtons.replayTomorrow);

                        break;
                    case 4:

                        interfaceComponents[102] = addButton(journalButtons.cancelReplay);

                        break;

                }

            }

            resetInterface();

            scrolled = 0;

            if (contentBox.Height < 512)
            {

                interfaceComponents[302].active = false;

                interfaceComponents[303].active = false;

                interfaceComponents[304].active = false;

            }
            else
            {

                scrollId = 303;

            }

            if (!Context.IsMainPlayer)
            {

                interfaceComponents[102].active = false;

            }
            else if (Mod.instance.save.progress[journalId].status > 1 && !Mod.instance.questHandle.IsReplayable(journalId))
            {

                interfaceComponents[102].active = false;

            }

            if (Mod.instance.questHandle.quests[journalId].type != Quest.questTypes.lesson)
            {

                interfaceComponents[101].active = false;

            }

        }

        public override void pressButton(journalButtons button)
        {
            
            switch (button)
            {

                case journalButtons.back:

                    DruidJournal.openJournal(parentJournal, null, record);

                    break;

                case journalButtons.viewEffect:

                    KeyValuePair<string, int> findEffect = Mod.instance.questHandle.questEffects(journalId);

                    openJournal(journalTypes.effectPage, findEffect.Key, findEffect.Value);

                    break;

                case journalButtons.skipQuest:

                    Game1.playSound("ghost");

                    Mod.instance.questHandle.CompleteQuest(journalId);

                    Mod.instance.questHandle.OnCancel(journalId);

                    DruidJournal.openJournal(journalTypes.questPage, journalId, record);

                    break;

                case journalButtons.replayQuest:

                    if (Mod.instance.questHandle.IsReplayable(journalId))
                    {

                        Game1.playSound("yoba");

                        Mod.instance.questHandle.RevisitQuest(journalId);

                        DruidJournal.openJournal(journalTypes.questPage, journalId, record);

                    }

                    break;

                case journalButtons.replayTomorrow:

                    break;

                case journalButtons.cancelReplay:

                    Game1.playSound("ghost");

                    Mod.instance.questHandle.OnCancel(journalId);

                    Mod.instance.SyncMultiplayer();

                    DruidJournal.openJournal(journalTypes.questPage, journalId, record);

                    break;

                default:

                    base.pressButton(button);

                    break;

            }

        }


        public override void drawContent(SpriteBatch b)
        {

            // preserve current batch rectangle
            
            Rectangle preserve = b.GraphicsDevice.ScissorRectangle;

            b.End();

            // create new batch for scroll rectangle

            SpriteBatch b2 = b;

            b2.Begin(0, BlendState.AlphaBlend, SamplerState.PointClamp, null, new RasterizerState() { ScissorTestEnable = true }, null, new Matrix?());

            // determine inframe

            Rectangle inframe = new(xPositionOnScreen + 32, yPositionOnScreen + 48, width - 64, height - 96);

            Rectangle screen = Utility.ConstrainScissorRectToScreen(inframe);

            Game1.graphics.GraphicsDevice.ScissorRectangle = screen;

            foreach (KeyValuePair<int,ContentComponent> component in contentComponents)
            {

                component.Value.draw(b, new Vector2(0, -scrolled));

            }

            // leave inframe

            b.End();

            b.GraphicsDevice.ScissorRectangle = preserve;

            b.Begin(0, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, new Matrix?());

        }

    }

}
