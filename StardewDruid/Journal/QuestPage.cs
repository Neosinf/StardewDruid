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

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.BurlyWood;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }
            
            if (isActive && Mod.instance.save.progress[questId].status == 4)
            {
                
                contentComponents[start] = new(ContentComponent.contentTypes.text, "questReplay");

                contentComponents[start].text[0] = DialogueData.Strings(StardewDruid.Data.DialogueData.stringkeys.questReplay);

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.BurlyWood;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            // ------------------------------ instructions

            contentComponents[start] = new(ContentComponent.contentTypes.text, "instruction");

            contentComponents[start].text[0] = questRecord.instruction;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;


            // ------------------------------ conditional instructions

            if (questRecord.type == Quest.questTypes.lesson)
            {

                string lessonProgress = 
                    isActive ? "Mastered " : "" +
                    Mod.instance.save.progress[questId].progress.ToString() + " " + 
                    DialogueData.Strings(StardewDruid.Data.DialogueData.stringkeys.outOf) + " " + 
                    questRecord.requirement.ToString() + " " + 
                    questRecord.progression;

                contentComponents[start] = new(ContentComponent.contentTypes.text, "progress");

                contentComponents[start].text[0] = lessonProgress;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            if (questReward > 0)
            {

                if(Mod.instance.save.progress[questId].status == 4) { questReward *= 3; questReward /= 2; }

                float adjustReward = 1.2f - ((float)Mod.instance.ModDifficulty() * 0.1f);

                questReward = (int)(questReward * adjustReward);

                contentComponents[start] = new(ContentComponent.contentTypes.text, "lesson");

                contentComponents[start].text[0] = DialogueData.Strings(StardewDruid.Data.DialogueData.stringkeys.reward) + ": " + questReward.ToString() + "g";

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            if (isActive && Mod.instance.save.progress[questId].status == 4 && questRecord.replay != null)
            {

                contentComponents[start] = new(ContentComponent.contentTypes.text, "replay");

                contentComponents[start].text[0] = DialogueData.Strings(StardewDruid.Data.DialogueData.stringkeys.replayReward) + ": " + questRecord.replay;

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            for(int i = 0; i < questRecord.details.Count; i++)
            {

                string detail = questRecord.details[i];

                contentComponents[start] = new(ContentComponent.contentTypes.text, "detail"+i.ToString());

                contentComponents[start].text[0] = detail;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;


            }

            if (questRecord.type == Quest.questTypes.challenge)
            {

                Dictionary<int, Dictionary<int, string>> dialogueScene = DialogueData.DialogueScene(questId);

                Dictionary<int, string> narrators = DialogueData.DialogueNarrators(questId);

                if (dialogueScene.Count > 0)
                {

                    foreach (KeyValuePair<int, Dictionary<int, string>> sceneEntry in dialogueScene)
                    {

                        foreach (KeyValuePair<int, string> sceneMoment in sceneEntry.Value)
                        {

                            contentComponents[start] = new(ContentComponent.contentTypes.text, "narration" + start.ToString());

                            contentComponents[start].text[0] = narrators[sceneMoment.Key] + ": " + sceneMoment.Value;

                            contentComponents[start].textColours[0] = Mod.instance.iconData.schemeColours.ElementAt(sceneMoment.Key + 1).Value;

                            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                            textHeight += contentComponents[start++].bounds.Height;

                        }

                    }

                }

            }

            if (Mod.instance.questHandle.loresets.ContainsKey(questId))
            {

                foreach (LoreData.stories story in Mod.instance.questHandle.loresets[questId])
                {

                    if (Mod.instance.questHandle.lores.ContainsKey(story))
                    {

                        contentComponents[start] = new(ContentComponent.contentTypes.text, "lore" + start.ToString());

                        contentComponents[start].text[0] = Mod.instance.questHandle.lores[story].answer;

                        contentComponents[start].textColours[0] = Mod.instance.iconData.schemeColours.ElementAt((int)Mod.instance.questHandle.lores[story].character + 1).Value;

                        contentComponents[start].setBounds(0, xPositionOnScreen+ 64, yPositionOnScreen + textHeight, width - 128, 0);

                        textHeight += contentComponents[start++].bounds.Height;

                        contentComponents[start] = new(ContentComponent.contentTypes.text, "lore" + start.ToString());

                        contentComponents[start].text[0] = "(" + CharacterHandle.CharacterTitle(Mod.instance.questHandle.lores[story].character) + ")";

                        contentComponents[start].textColours[0] = Mod.instance.iconData.schemeColours.ElementAt((int)Mod.instance.questHandle.lores[story].character + 1).Value;

                        contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                        textHeight += contentComponents[start++].bounds.Height;

                    }

                }

            }

            textHeight += 48;

            contentBox = new(xPositionOnScreen, yPositionOnScreen + 96, width, textHeight);

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
