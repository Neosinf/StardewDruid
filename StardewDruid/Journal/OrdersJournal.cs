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

        public OrdersJournal(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
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

            pagination = 4;

            contentComponents = Mod.instance.exportHandle.JournalOrders();

            ParameterRecord();

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key % pagination, xPositionOnScreen, yPositionOnScreen, width, height);

            }

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

                [201] = addButton(journalButtons.start),
                [202] = addButton(journalButtons.back),

                [203] = addButton(journalButtons.openGuilds),
                [204] = addButton(journalButtons.openGoodsDistillery),
                [205] = addButton(journalButtons.openDistillery),
                [206] = addButton(journalButtons.openDistilleryInventory),
                [207] = addButton(journalButtons.openProductionRecent),
                [208] = addButton(journalButtons.openProductionEstimated),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.end),
                [303] = addButton(journalButtons.forward),

            };
             
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
