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

namespace StardewDruid.Journal
{
    public class LorePage : DruidJournal
    {

        public string effectQuest;

        public int effectIndex;

        public LorePage(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {

        }

        public override void populateInterface()
        {

            parent = journalTypes.lore;

            type = journalTypes.lorePage;

            interfaceComponents = new()
            {

                [201] = addButton(journalButtons.previous),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public override void populateContent()
        {

            LoreSet.loresets set = Enum.Parse<LoreSet.loresets>(parameters[0]);

            // ----------------------------- title

            int textHeight = 48;

            title = Mod.instance.questHandle.loresets[set].title;

            int start = 0;

            if (Mod.instance.questHandle.loresets[set].settype == LoreSet.settypes.transcript)
            {
                
                contentComponents[start] = new(ContentComponent.contentTypes.text, "description");

                contentComponents[start].text[0] = StringData.Get(StringData.str.questTranscript);

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

                Dictionary<int, Dictionary<int, string>> dialogueScene = DialogueData.DialogueScene(Mod.instance.questHandle.loresets[set].quest);

                Dictionary<int, string> narrators = NarratorData.DialogueNarrators(Mod.instance.questHandle.loresets[set].quest);

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

                            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                            textHeight += contentComponents[start++].bounds.Height;

                        }

                    }
                }

            }
            else
            {
                
                foreach (KeyValuePair<LoreStory.stories, LoreStory> story in Mod.instance.questHandle.lores)
                {

                    if (story.Value.loreset != set)
                    {

                        continue;

                    }

                    if (!Mod.instance.questHandle.IsComplete(story.Value.quest))
                    {

                        continue;

                    }

                    contentComponents[start] = new(ContentComponent.contentTypes.text, "description");

                    contentComponents[start].text[0] = story.Value.description;

                    contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                    textHeight += contentComponents[start++].bounds.Height;

                    switch (story.Value.loretype)
                    {

                        case LoreStory.loretypes.information:

                            foreach (string detail in story.Value.details)
                            {

                                contentComponents[start] = new(ContentComponent.contentTypes.text, "detail" + start.ToString());

                                contentComponents[start].text[0] = detail;

                                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkBlue;

                                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                                textHeight += contentComponents[start++].bounds.Height;

                            }

                            break;

                        case LoreStory.loretypes.story:

                            contentComponents[start] = new(ContentComponent.contentTypes.text, "lore" + start.ToString());

                            contentComponents[start].text[0] = story.Value.question;

                            contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

                            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                            textHeight += contentComponents[start++].bounds.Height;

                            contentComponents[start] = new(ContentComponent.contentTypes.text, "lore" + start.ToString());

                            contentComponents[start].text[0] = story.Value.answer;

                            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                            textHeight += contentComponents[start++].bounds.Height;

                            break;

                    }

                }

            }

            textHeight += 48;

            contentBox = new(xPositionOnScreen, yPositionOnScreen + 96, width, textHeight);

        }


    }

}
