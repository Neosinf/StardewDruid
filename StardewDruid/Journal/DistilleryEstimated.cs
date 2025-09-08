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
    public class DistilleryEstimated : DruidJournal
    {

        public DistilleryEstimated(journalTypes Type, List<string> Parameters) : base(Type, Parameters) 
        {

        }

        public override void populateInterface()
        {

            parent = journalTypes.distillery;

            type = journalTypes.distilleryEstimated;

            interfaceComponents = new()
            {
                
                [101] = addButton(journalButtons.openOrders),


                [201] = addButton(journalButtons.previous),
                [202] = addButton(journalButtons.openGuilds),
                [203] = addButton(journalButtons.openGoodsDistillery),
                [204] = addButton(journalButtons.openDistillery),
                [205] = addButton(journalButtons.openDistilleryInventory),
                [206] = addButton(journalButtons.openProductionRecent),

                [207] = addButton(journalButtons.refresh),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.refresh:

                    Mod.instance.exportHandle.Estimate();

                    populateContent();

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

        public override void prepareContent()
        {

            Mod.instance.exportHandle.Estimate();

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

        public virtual List<string> ProductionContent()
        {

            return Mod.instance.exportHandle.estimatedProduction;

        }

    }

}
