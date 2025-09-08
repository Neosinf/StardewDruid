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

        public DistilleryRecent(journalTypes Type, List<string> Parameters) : base(Type, Parameters) 
        {

        }

        public override void populateInterface()
        {

            parent = journalTypes.distillery;

            type = journalTypes.distilleryRecent;

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openOrders),


                [201] = addButton(journalButtons.previous),

                [203] = addButton(journalButtons.openGuilds),
                [204] = addButton(journalButtons.openGoodsDistillery),
                [205] = addButton(journalButtons.openDistillery),
                [206] = addButton(journalButtons.openDistilleryInventory),
                [207] = addButton(journalButtons.openProductionEstimated),

                [301] = addButton(journalButtons.exit),
                [302] = addButton(journalButtons.scrollUp),
                [303] = addButton(journalButtons.scrollBar),
                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public override List<string> ProductionContent()
        {

            return Mod.instance.exportHandle.recentProduction;

        }

    }

}
