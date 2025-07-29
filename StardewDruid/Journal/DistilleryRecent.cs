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
    public class DistilleryRecent : DistilleryEstimated
    {

        public DistilleryRecent(string QuestId, int Record) : base(QuestId, Record) 
        {

        }

        public override void populateInterface()
        {

            parentJournal = journalTypes.distillery;

            type = journalTypes.distilleryRecent;

            title = JournalData.JournalTitle(type);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openProductionEstimated),

                [201] = addButton(journalButtons.back),

                [202] = addButton(journalButtons.refresh),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public override List<string> ProductionContent()
        {

            return Mod.instance.exportHandle.recents;

        }

    }

}
