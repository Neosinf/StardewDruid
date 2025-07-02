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
    public class OmenJournal : HerbalJournal
    {

        public OmenJournal(string QuestId, int Record) : base(QuestId, Record)
        {

        }

        public override void populateInterface()
        {

            type = journalTypes.omens;

            title = StringData.Strings(StringData.stringkeys.satchel);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.herbalism),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),
                [203] = addButton(journalButtons.bombs),
                [204] = addButton(journalButtons.goods),

                [301] = addButton(journalButtons.exit),
                [302] = addButton(journalButtons.forward),
                [303] = addButton(journalButtons.end),

            };

            if (Mod.instance.magic)
            {

                interfaceComponents.Remove(204);

                interfaceComponents.Remove(302);

                interfaceComponents.Remove(303);

            }

        }
        
        public override void populateContent()
        {

            contentColumns = 12;

            pagination = 0;

            contentComponents = Mod.instance.herbalData.JournalOmens();

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

                    DruidJournal.openJournal(journalTypes.bombs);

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

                Mod.instance.herbalData.ConvertToGoods(contentComponents[focus].id, amount);

                populateContent();

                return;

            }

            string herbalId = contentComponents[focus].id;

            Mod.instance.herbalData.ConsumeOmen(herbalId, amount);

            populateContent();

            return;

        }

        public override void pressCancel()
        {

            return;

        }

    }

}
