using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Journal
{
    public class TrophyJournal : PotionJournal
    {

        public TrophyJournal(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {

        }

        public override void prepareContent()
        {

            type = journalTypes.trophies;

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
                [203] = addButton(journalButtons.openOmens),

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.back:

                    DruidJournal.openJournal(journalTypes.omens);

                    return;

                case journalButtons.start:

                    DruidJournal.openJournal(journalTypes.alchemy);

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

    }

}
