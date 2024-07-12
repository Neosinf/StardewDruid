using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StardewDruid.Data.DialogueData;

namespace StardewDruid.Journal
{
    public class QuestPage : DruidJournal
    {

        public QuestPage(string QuestId, int Record) : base(QuestId, Record) 
        {

        }

        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [301] = addButton(journalButtons.exit),

                [201] = addButton(journalButtons.back),

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


            // ----------------------------- setup

            int textHeight = 32;

            contentComponents[101] = new(ContentComponent.contentTypes.title, questRecord.title);

            contentComponents[101].setBounds(0, xPositionOnScreen, yPositionOnScreen + textHeight, width, 64);

            textHeight += contentComponents[101].bounds.Height;

            contentComponents[102] = new(ContentComponent.contentTypes.text, questRecord.description);

            contentComponents[102].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width-128, 0);

            textHeight += contentComponents[102].bounds.Height;

            // ------------------------------ conditional description

            if (isActive && !Context.IsMainPlayer && questRecord.type != Quest.questTypes.lesson)
            {

                contentComponents[103] = new(ContentComponent.contentTypes.text, DialogueData.Strings(stringkeys.hostOnly));

                contentComponents[103].color = Microsoft.Xna.Framework.Color.BurlyWood;

                contentComponents[103].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[103].bounds.Height;

            }
            
            if (isActive && Mod.instance.save.progress[questId].status == 4)
            {
                
                contentComponents[104] = new(ContentComponent.contentTypes.text, DialogueData.Strings(stringkeys.questReplay));

                contentComponents[104].color = Microsoft.Xna.Framework.Color.BurlyWood;

                contentComponents[104].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[104].bounds.Height;

            }

            // ------------------------------ instructions

            contentComponents[105] = new(ContentComponent.contentTypes.text, questRecord.instruction);

            contentComponents[105].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[105].bounds.Height;


            // ------------------------------ conditional instructions

            if (questRecord.type == Quest.questTypes.lesson)
            {

                string lessonProgress = 
                    isActive ? "Mastered " : "" +
                    Mod.instance.save.progress[questId].progress.ToString() + " " + 
                    DialogueData.Strings(stringkeys.outOf) + " " + 
                    questRecord.requirement.ToString() + " " + 
                    questRecord.progression;

                contentComponents[106] = new(ContentComponent.contentTypes.text, lessonProgress);

                contentComponents[106].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[106].bounds.Height;

            }

            if (questReward > 0)
            {

                if(Mod.instance.save.progress[questId].status == 4) { questReward *= 3; questReward /= 2; }

                float adjustReward = 1.2f - ((float)Mod.instance.ModDifficulty() * 0.1f);

                questReward = (int)(questReward * adjustReward);

                contentComponents[107] = new(ContentComponent.contentTypes.text, DialogueData.Strings(stringkeys.reward) + ": " + questReward.ToString() + "g");

                contentComponents[107].color = Microsoft.Xna.Framework.Color.DarkGreen;

                contentComponents[107].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[107].bounds.Height;

            }

            if (isActive && Mod.instance.save.progress[questId].status == 4 && questRecord.replay != null)
            {

                contentComponents[108] = new(ContentComponent.contentTypes.text, DialogueData.Strings(DialogueData.stringkeys.replayReward) + ": " + questRecord.replay);

                contentComponents[108].color = Microsoft.Xna.Framework.Color.DarkGreen;

                contentComponents[108].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[108].bounds.Height;

            }

            for(int i = 0; i < questRecord.details.Count; i++)
            {

                string detail = questRecord.details[i];

                contentComponents[200 + i] = new(ContentComponent.contentTypes.text, detail);

                contentComponents[200 + i].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[200 + i].bounds.Height;


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

                            contentComponents[1000 + sceneEntry.Key] = new(ContentComponent.contentTypes.text, narrators[sceneMoment.Key] + ": " + sceneMoment.Value);

                            contentComponents[1000 + sceneEntry.Key].color = Mod.instance.iconData.schemeColours.ElementAt(sceneMoment.Key + 1).Value;

                            contentComponents[1000 + sceneEntry.Key].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                            textHeight += contentComponents[1000 + sceneEntry.Key].bounds.Height;

                        }

                    }

                }

            }

            if (Mod.instance.questHandle.loresets.ContainsKey(questId))
            {

                int loreCounter = 300;

                foreach (LoreData.stories story in Mod.instance.questHandle.loresets[questId])
                {

                    if (Mod.instance.questHandle.lores.ContainsKey(story))
                    {

                        contentComponents[loreCounter] = new(ContentComponent.contentTypes.text, Mod.instance.questHandle.lores[story].answer);

                        contentComponents[loreCounter].color = Mod.instance.iconData.schemeColours.ElementAt((int)Mod.instance.questHandle.lores[story].character + 1).Value;

                        contentComponents[loreCounter].setBounds(0, xPositionOnScreen+ 64, yPositionOnScreen + textHeight, width - 128, 0);

                        textHeight += contentComponents[loreCounter++].bounds.Height;

                        contentComponents[loreCounter] = new(ContentComponent.contentTypes.text, "(" + CharacterHandle.CharacterTitle(Mod.instance.questHandle.lores[story].character) + ")");

                        contentComponents[loreCounter].color = Mod.instance.iconData.schemeColours.ElementAt((int)Mod.instance.questHandle.lores[story].character + 1).Value;

                        contentComponents[loreCounter].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                        textHeight += contentComponents[loreCounter++].bounds.Height;

                    }

                }

            }

            contentBox = new(xPositionOnScreen, yPositionOnScreen + 96, width, textHeight - 96);

        }

        public override void activateInterface()
        {

            resetInterface();

            scrolled = 0;

            if(contentBox.Height < 512)
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

                    DruidJournal.openJournal(journalTypes.quests, null, record);

                    break;

                default:

                    base.pressButton(interfaceComponents[focus].button);

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

            Rectangle inframe = new(xPositionOnScreen + 32, yPositionOnScreen + 32, width - 64, height - 64);

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
