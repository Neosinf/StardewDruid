using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Network;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class LorePage : QuestPage
    {

        public string effectQuest;

        public int effectIndex;

        public LorePage(string QuestId, int Record) : base(QuestId, Record) 
        {

        }

        public override void populateInterface()
        {

            parentJournal = journalTypes.lore;

            type = journalTypes.lorePage;

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

            string[] loreParts = journalId.Split(".");

            string questId = loreParts[0];

            bool isTranscript = loreParts[1] == "transcript";

            // ---------------------------- quest

            Quest questRecord = Mod.instance.questHandle.quests[questId];

            int questReward = questRecord.reward;

            // ----------------------------- title

            int textHeight = 48;

            title = questRecord.title;

            int start = 0;

            // ----------------------------- description

            contentComponents[start] = new(ContentComponent.contentTypes.text, "description");

            if (isTranscript)
            {
                
                contentComponents[start].text[0] = DialogueData.Strings(DialogueData.stringkeys.questTranscript);

            }
            else
            {
                
                contentComponents[start].text[0] = DialogueData.Strings(DialogueData.stringkeys.questLore);

            }

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            // ----------------------------- lore

            if (isTranscript && questRecord.type == Quest.questTypes.challenge)
            {

                Dictionary<int, Dictionary<int, string>> dialogueScene = DialogueData.DialogueScene(questId);

                Dictionary<int, string> narrators = DialogueData.DialogueNarrators(questId);

                if (dialogueScene.Count > 0)
                {

                    foreach (KeyValuePair<int, Dictionary<int, string>> sceneEntry in dialogueScene)
                    {

                        foreach (KeyValuePair<int, string> sceneMoment in sceneEntry.Value)
                        {

                            if (sceneMoment.Key == 999)
                            {

                                continue;

                            }

                            contentComponents[start] = new(ContentComponent.contentTypes.text, "narration" + start.ToString());

                            contentComponents[start].text[0] = narrators[sceneMoment.Key] + ": " + sceneMoment.Value;

                            //contentComponents[start].textColours[0] = Mod.instance.iconData.schemeColours.ElementAt(sceneMoment.Key + 1).Value;

                            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                            textHeight += contentComponents[start++].bounds.Height;

                        }

                    }
                }
            }

            if (!isTranscript && Mod.instance.questHandle.loresets.ContainsKey(questId))
            {

                foreach (LoreData.stories story in Mod.instance.questHandle.loresets[questId])
                {

                    if (Mod.instance.questHandle.lores.ContainsKey(story))
                    {
                        
                        contentComponents[start] = new(ContentComponent.contentTypes.text, "lore" + start.ToString());

                        contentComponents[start].text[0] = Mod.instance.questHandle.lores[story].question;

                        contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

                        contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                        textHeight += contentComponents[start++].bounds.Height;

                        contentComponents[start] = new(ContentComponent.contentTypes.text, "lore" + start.ToString());

                        contentComponents[start].text[0] = Mod.instance.questHandle.lores[story].answer;

                        //contentComponents[start].textColours[0] = Mod.instance.iconData.schemeColours.ElementAt((int)Mod.instance.questHandle.lores[story].character + 1).Value;

                        contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                        textHeight += contentComponents[start++].bounds.Height;

                        contentComponents[start] = new(ContentComponent.contentTypes.text, "lore" + start.ToString());

                        contentComponents[start].text[0] = "(" + CharacterHandle.CharacterTitle(Mod.instance.questHandle.lores[story].character) + ")";

                        //contentComponents[start].textColours[0] = Mod.instance.iconData.schemeColours.ElementAt((int)Mod.instance.questHandle.lores[story].character + 1).Value;

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

    }

}
