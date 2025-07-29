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
    public class DistilleryEstimated : QuestPage
    {

        public DistilleryEstimated(string QuestId, int Record) : base(QuestId, Record) 
        {

        }

        public override void populateInterface()
        {

            parentJournal = journalTypes.distillery;

            type = journalTypes.distilleryEstimated;

            title = JournalData.JournalTitle(type);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openProductionRecent),

                [201] = addButton(journalButtons.back),

                [202] = addButton(journalButtons.refresh),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public virtual List<string> ProductionContent()
        {

            Mod.instance.exportHandle.CalculateOutput();

            return Mod.instance.exportHandle.calculations;

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.back:

                    DruidJournal.openJournal(journalTypes.distillery);

                    break;

                case journalButtons.refresh:

                    Mod.instance.exportHandle.CalculateOutput();

                    populateContent();

                    return;

                case journalButtons.openProductionEstimated:

                    DruidJournal.openJournal(journalTypes.distilleryEstimated);

                    return;

                case journalButtons.openProductionRecent:

                    DruidJournal.openJournal(journalTypes.distilleryRecent);

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

        public override void populateContent()
        {

            // ----------------------------- title

            int textHeight = 48;

            int start = 0;

            List<string> useContent = ProductionContent();

            // ------------------------------ conditional instructions

            for (int i = 0; i < useContent.Count; i++)
            {

                string detail = useContent[i];

                contentComponents[start] = new(ContentComponent.contentTypes.text, i.ToString());

                contentComponents[start].text[0] = detail;

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

        }

    }

}
