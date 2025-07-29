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
    public class QuestJournal : DruidJournal
    {

        public QuestJournal(string QuestId, int Record) : base(QuestId, Record)
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

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),
                [203] = addButton(journalButtons.getHint),
                [204] = addButton(journalButtons.active),
                [205] = addButton(journalButtons.reverse),

                [301] = addButton(journalButtons.exit),

                [305] = addButton(journalButtons.end),
                [306] = addButton(journalButtons.forward),

            };

        }

        public override void populateContent()
        {

            type = journalTypes.quests;

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

        public override void pressContent()
        {

            openJournal(journalTypes.questPage, contentComponents[focus].id, focus);

        }

    }

}
