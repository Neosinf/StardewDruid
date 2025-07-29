using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley.Menus;
using System.Diagnostics.Metrics;


namespace StardewDruid.Journal
{
    public class AlchemyJournal : DruidJournal
    {

        public AlchemyJournal(string QuestId, int Record) : base(QuestId, Record)
        {


        }

        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openQuests),
                [102] = addButton(journalButtons.openMasteries),
                [103] = addButton(journalButtons.openRelics),
                [104] = addButton(journalButtons.openAlchemy),
                [105] = addButton(journalButtons.openPotions),
                [106] = addButton(journalButtons.openCompanions),
                [107] = addButton(journalButtons.openDragonomicon),

                [201] = addButton(journalButtons.openOmens),
                [202] = addButton(journalButtons.openTrophies),

                [301] = addButton(journalButtons.exit),
                [302] = addButton(journalButtons.forward),
                [303] = addButton(journalButtons.end),


            };

        }

        public override void populateContent()
        {

            type = journalTypes.alchemy;

            title = JournalData.JournalTitle(type);

            pagination = 6;

            contentComponents = Mod.instance.questHandle.JournalQuests();

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key % pagination, xPositionOnScreen, yPositionOnScreen, width, height);

            }

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.forward:

                    DruidJournal.openJournal(journalTypes.omens);

                    return;

                case journalButtons.end:

                    DruidJournal.openJournal(journalTypes.trophies);

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

        public override void pressContent()
        {

            openJournal(journalTypes.effectPage, contentComponents[focus].id, focus);

        }

    }

}
