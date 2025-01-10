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
    public class HerbalTrade : DruidJournal
    {

        public HerbalTrade(string QuestId, int Record) : base(QuestId, Record)
        {


        }
        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void activateInterface()
        {

            resetInterface();

        }

        public override void populateContent()
        {

            type = journalTypes.herbalTrade;

            title = StringData.Strings(StringData.stringkeys.herbalTrade);

            pagination = 6;

            contentComponents = Mod.instance.herbalData.TradeHerbals();

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

            string key = contentComponents[focus].id;

            HerbalData.herbals herbal = Enum.Parse<HerbalData.herbals>(key);

            int order = Mod.instance.herbalData.orders[herbal];

            int price = Mod.instance.herbalData.herbalism[key].price;

            int total = order * price;

            if (HerbalData.UpdateHerbalism(herbal) < order)
            {
                
                Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                return;

            }

            HerbalData.UpdateHerbalism(herbal, 0 - order);

            Mod.instance.herbalData.orders.Remove(herbal);

            Game1.player.Money += total;

            browsing = false;

            focus = 0;

            populateContent();

        }

    }

}
