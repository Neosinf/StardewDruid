using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Quests;
using System;
using System.Collections.Generic;

namespace StardewDruid.Journal
{
    public class QuestionPage : DruidJournal
    {


        public QuestionPage(string QuestId, int Record) : base(QuestId, Record) 
        {

        }

        public override void populateInterface()
        {

            parentJournal = journalTypes.quests;

            type = journalTypes.questPage;

            interfaceComponents = new()
            {
                
                [201] = addButton(journalButtons.back),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public override void populateContent()
        {

            title = StringData.Strings(StardewDruid.Data.StringData.stringkeys.questionCurrent);

            int textHeight = 48;

            int start = 0;

            string current = string.Empty;

            string instruction = string.Empty;

            string recap = string.Empty;

            if (Enum.IsDefined(Mod.instance.save.milestone + 1))
            {

                QuestHandle.milestones nextMilestone = Mod.instance.save.milestone + 1;

                foreach (string questId in QuestHandle.milestoneQuests[nextMilestone])
                {

                    if (Mod.instance.questHandle.IsComplete(questId))
                    {

                        if(Mod.instance.questHandle.quests[questId].type == Data.Quest.questTypes.lesson)
                        {

                            recap = Mod.instance.questHandle.quests[questId].title;

                        }
                        else
                        {
                            
                            recap = Mod.instance.questHandle.quests[questId].explanation;

                        }

                        continue;

                    }

                    if (Mod.instance.questHandle.IsGiven(questId))
                    {

                        current = Mod.instance.questHandle.quests[questId].title;

                        instruction = Mod.instance.questHandle.quests[questId].instruction;

                        if(!Context.IsMainPlayer)
                        {

                            instruction = StringData.Strings(StardewDruid.Data.StringData.stringkeys.hostOnly);

                        }

                        break;

                    }

                    current = Mod.instance.questHandle.quests[questId].title;

                    foreach (KeyValuePair<CharacterHandle.characters, DialogueSpecial> special in Mod.instance.questHandle.quests[questId].before)
                    {

                        if (special.Value.questContext != 0)
                        {

                            continue;

                        }

                        instruction = StringData.Strings(StardewDruid.Data.StringData.stringkeys.questionTalkto) + CharacterHandle.CharacterTitle(special.Key);

                        if(Mod.instance.save.progress.ContainsKey(questId)) //|| Mod.instance.save.progress[questId].delay > 0)
                        {

                            if(Mod.instance.save.progress[questId].delay > 0)
                            {

                                instruction += StringData.Strings(StardewDruid.Data.StringData.stringkeys.questionTomorrow);

                            }

                        }

                        if (!Context.IsMainPlayer)
                        {

                            instruction = StringData.Strings(StardewDruid.Data.StringData.stringkeys.hostOnly);

                        }

                        break;

                    }

                    break;

                }

            }
            else
            {

                current = StringData.Strings(StardewDruid.Data.StringData.stringkeys.questionComplete);

                instruction = StringData.Strings(StardewDruid.Data.StringData.stringkeys.questionCongratulations);

            }

            if (recap == string.Empty && (int)Mod.instance.save.milestone != 0)
            {

                foreach (string questId in QuestHandle.milestoneQuests[Mod.instance.save.milestone])
                {

                    if (Mod.instance.questHandle.IsComplete(questId))
                    {

                        recap = Mod.instance.questHandle.quests[questId].explanation;

                        continue;

                    }

                }

            }

            // ----------------------- Current objective

            /*contentComponents[start] = new(ContentComponent.contentTypes.text, "currentIntro");

            contentComponents[start].text[0] = "Current Objective";

            contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;*/

            // ----------------------- title

            contentComponents[start] = new(ContentComponent.contentTypes.text, "current");

            contentComponents[start].text[0] = current;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            // ----------------------- Current instructions

            contentComponents[start] = new(ContentComponent.contentTypes.text, "instructionIntro");

            contentComponents[start].text[0] = StringData.Strings(StardewDruid.Data.StringData.stringkeys.questionHint);

            contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            // ----------------------- instruction

            contentComponents[start] = new(ContentComponent.contentTypes.text, "instruction");

            contentComponents[start].text[0] = instruction;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            if(recap != string.Empty)
            {
                
                // ----------------------- Previous quest

                contentComponents[start] = new(ContentComponent.contentTypes.text, "recapIntro");

                contentComponents[start].text[0] = StringData.Strings(StardewDruid.Data.StringData.stringkeys.questionPreviously);

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

                // ----------------------- recap

                contentComponents[start] = new(ContentComponent.contentTypes.text, "recap");

                contentComponents[start].text[0] = recap;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;


            }

            textHeight += 16;

            contentBox = new(xPositionOnScreen, yPositionOnScreen + 64, width, textHeight);

        }

        public override void activateInterface()
        {

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

        }

        public override void pressButton(journalButtons button)
        {
            
            switch (button)
            {

                case journalButtons.back:

                    DruidJournal.openJournal(parentJournal, null, record);

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
