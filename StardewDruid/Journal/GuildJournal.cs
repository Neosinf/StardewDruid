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
    public class GuildJournal : DruidJournal
    {

        public GuildJournal(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
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

                [204] = addButton(journalButtons.openGoodsDistillery),
                [205] = addButton(journalButtons.openDistillery),
                [206] = addButton(journalButtons.openDistilleryInventory),
                [207] = addButton(journalButtons.openProductionRecent),
                [208] = addButton(journalButtons.openProductionEstimated),

                [301] = addButton(journalButtons.exit),
                [302] = addButton(journalButtons.reverse),
                [303] = addButton(journalButtons.end),
                [304] = addButton(journalButtons.forward),

            };

        }

        public override void populateContent()
        {

            type = journalTypes.guilds;

            pagination = 4;

            contentComponents = Mod.instance.exportHandle.JournalGuilds();

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            int xP = xPositionOnScreen + 16;

            int yP = yPositionOnScreen + 16;

            int contentWidth = width - 32;

            int contentHeight = height - 32;

            int yHeight = contentHeight / 4;

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                int compIndex = component.Key % pagination;

                component.Value.SetText(width);

                component.Value.bounds = new Rectangle(xP, yP + (compIndex * yHeight), contentWidth, yHeight);

            }

        }

        public override void pressContent()
        {

            openJournal(journalTypes.guildPage, contentComponents[focus].id);

        }

    }

}
