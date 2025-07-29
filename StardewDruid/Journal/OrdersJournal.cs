using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{

    public class OrdersJournal : DruidJournal
    {

        public OrdersJournal(string QuestId, int Record) : base(QuestId, Record)
        {


        }

        public override void prepareContent()
        {

            if (Mod.instance.save.orders.Count == 0)
            {

                Mod.instance.exportHandle.Orders();

            }

        }

        public override void populateContent()
        {

            type = journalTypes.orders;

            title = JournalData.JournalTitle(type);

            pagination = 4;

            contentComponents = Mod.instance.exportHandle.JournalOrders();

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key % pagination, xPositionOnScreen, yPositionOnScreen, width, height);

            }

        }

        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),

                [301] = addButton(journalButtons.exit),

                [305] = addButton(journalButtons.end),
                [306] = addButton(journalButtons.forward),

            };
             
        }

        public override void activateInterface()
        {

            resetInterface();

            int firstOnThisPage = record - (record % pagination);

            int thispage = firstOnThisPage == 0 ? 0 : firstOnThisPage / pagination;

            int last = contentComponents.Count - 1;

            int firstOnLastPage = last - (last % pagination);

            int lastpage = firstOnLastPage == 0 ? 0 : firstOnLastPage / pagination;

            if (thispage == 0)
            {

                // back
                interfaceComponents[201].active = false;

                // start
                interfaceComponents[202].active = false;

            }

            if (lastpage == thispage)
            {

                // forward
                interfaceComponents[305].active = false;

                // end
                interfaceComponents[306].active = false;

            }
        }


        public override void pressContent()
        {

            string key = contentComponents[focus].id;

            if (Mod.instance.exportHandle.CompleteOrder(Convert.ToInt32(key)))
            {

                populateContent();

            }
            else
            {

                Game1.playSound(SpellHandle.Sounds.ghost.ToString());

            }

        }

    }

}
