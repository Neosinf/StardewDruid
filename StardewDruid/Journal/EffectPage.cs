using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class EffectPage : DruidJournal
    {

        public string effectQuest;

        public EffectPage(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {

            parent = journalTypes.effects;

        }

        public override void populateContent()
        {

            effectQuest = parameters[0];

            EffectsData.EffectPage effectPage = Enum.Parse<EffectsData.EffectPage>(effectQuest);

            StardewDruid.Data.Effect journalEffect = EffectsData.RetrieveEffect(effectPage);

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

        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openMasteries),

                [201] = addButton(journalButtons.previous),

                [202] = addButton(journalButtons.viewQuest),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

            string viewIndice = Mod.instance.questHandle.effectQuests(effectQuest);

            if (viewIndice == null)
            {

                interfaceComponents.Remove(202);

            }

        }
        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.viewQuest:

                    string findQuest = Mod.instance.questHandle.effectQuests(effectQuest);

                    openJournal(journalTypes.questPage,findQuest);

                    break;

                default:

                    base.pressButton(button);

                    break;

            }

        }

    }

}
