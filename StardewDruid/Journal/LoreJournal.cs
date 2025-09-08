using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class LoreJournal : DruidJournal
    {

        public LoreJournal(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
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
                [107] = addButton(journalButtons.openOrders),
                [108] = addButton(journalButtons.openDragonomicon),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),

                [204] = addButton(journalButtons.openEffects),

                [301] = addButton(journalButtons.exit),
                [302] = addButton(journalButtons.reverse),
                [303] = addButton(journalButtons.end),
                [304] = addButton(journalButtons.forward),

            };

        }

        public override void populateContent()
        {

            type = journalTypes.lore;

            pagination = 6;

            contentComponents = Mod.instance.questHandle.JournalLore();

            ParameterRecord();

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key % pagination, xPositionOnScreen, yPositionOnScreen, width, height);

            }

        }

        public override void pressContent()
        {

            openJournal(journalTypes.lorePage, contentComponents[focus].id);

        }

    }

}
