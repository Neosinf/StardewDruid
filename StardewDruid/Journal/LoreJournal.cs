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

        public LoreJournal(string QuestId, int Record) : base(QuestId, Record)
        {


        }

        public override void populateContent()
        {

            type = journalTypes.lore;

            title = StringData.Strings(StringData.stringkeys.chronicle);

            pagination = 6;

            contentComponents = Mod.instance.questHandle.JournalLore();

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

            openJournal(journalTypes.lorePage, contentComponents[focus].id, focus);

        }

    }

}
