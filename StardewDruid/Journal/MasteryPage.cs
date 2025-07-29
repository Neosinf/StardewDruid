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
    public class MasteryPage : DruidJournal
    {

        public MasteryPage(string QuestId, int Record) : base(QuestId, Record)
        {

        }

        public override void populateInterface()
        {

            type = journalTypes.masteryPage;

            //title = JournalData.JournalTitle(type);

        }
        
        public override void populateContent()
        {

            contentColumns = 12;

            pagination = 0;

            contentComponents = Mod.instance.herbalHandle.JournalOmens();

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key/2, xPositionOnScreen, yPositionOnScreen, width, height, 0, 56);

            }

        }

        public override void activateInterface()
        {

            resetInterface();

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.start:

                    DruidJournal.openJournal(journalTypes.herbalism);

                    return;

                case journalButtons.back:

                    DruidJournal.openJournal(journalTypes.powders);

                    return;

                case journalButtons.forward:

                    DruidJournal.openJournal(journalTypes.goods);

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

        public override void pressContent()
        {

            int amount = 1;

            if (Mod.instance.Helper.Input.GetState(SButton.LeftShift) == SButtonState.Held)
            {
                amount = 5;
            }
            else if (Mod.instance.Helper.Input.GetState(SButton.RightShift) == SButtonState.Held)
            {
                amount = 5;
            }
            else if (Mod.instance.Helper.Input.GetState(SButton.LeftControl) == SButtonState.Held)
            {
                amount = 10;
            }
            else if (Mod.instance.Helper.Input.GetState(SButton.RightControl) == SButtonState.Held)
            {
                amount = 10;
            }

            if (contentComponents[focus].type == ContentComponent.contentTypes.toggle)
            {

                Mod.instance.herbalHandle.ConvertToGoods(contentComponents[focus].id, amount);

                populateContent();

                return;

            }

            string herbalId = contentComponents[focus].id;

            Mod.instance.herbalHandle.ConsumeOmen(herbalId, amount);

            populateContent();

            return;

        }

        public override void pressCancel()
        {

            return;

        }

    }

}
