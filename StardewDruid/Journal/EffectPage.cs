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

namespace StardewDruid.Journal
{
    public class EffectPage : QuestPage
    {

        public string effectQuest;

        public int effectIndex;

        public EffectPage(string QuestId, int Record) : base(QuestId, Record) 
        {

        }

        public override void populateInterface()
        {

            parentJournal = journalTypes.effects;

            type = journalTypes.effectPage;

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.viewQuest),

                [201] = addButton(journalButtons.back),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public override void populateContent()
        {

            string[] effectParts = journalId.Split(".");

            effectQuest = effectParts[0];

            effectIndex = Convert.ToInt32(effectParts[1]);

            Data.Effect journalEffect = Mod.instance.questHandle.effects[effectQuest][effectIndex];

            // ----------------------------- title

            int textHeight = 48;

            title = journalEffect.title;

            int start = 0;

            // ----------------------------- description

            contentComponents[start] = new(ContentComponent.contentTypes.text, "description");

            contentComponents[start].text[0] = journalEffect.description;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width-128, 0);

            textHeight += contentComponents[start++].bounds.Height;


            // ------------------------------ instructions

            contentComponents[start] = new(ContentComponent.contentTypes.text, "instruction");

            contentComponents[start].text[0] = journalEffect.instruction;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;


            // ------------------------------ conditional instructions

            for(int i = 0; i < journalEffect.details.Count; i++)
            {

                string detail = journalEffect.details[i];

                contentComponents[start] = new(ContentComponent.contentTypes.text, i.ToString());

                contentComponents[start].text[0] = detail;

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkBlue;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

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

            if (Mod.instance.magic)
            {

                interfaceComponents[101].active = false;

            }
            else
            if (Mod.instance.questHandle.quests[effectQuest].type != Quest.questTypes.lesson)
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

                case journalButtons.viewQuest:

                    KeyValuePair<string, int> findQuest = Mod.instance.questHandle.effectQuests(journalId);

                    openJournal(journalTypes.questPage, findQuest.Key, findQuest.Value);

                    break;

                default:

                    base.pressButton(button);

                    break;

            }

        }

    }

}
