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
    public class BombJournal : HerbalJournal
    {

        public BombJournal(string QuestId, int Record) : base(QuestId, Record)
        {

        }

        public override void populateInterface()
        {

            type = journalTypes.bombs;

            title = StringData.Strings(StringData.stringkeys.powderbox);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.herbalism),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.refresh),
                [203] = addButton(journalButtons.clearBuffs),
                [204] = addButton(journalButtons.omens),
                [205] = addButton(journalButtons.goods),

                [301] = addButton(journalButtons.exit),
                [302] = addButton(journalButtons.forward),
                [303] = addButton(journalButtons.end),

            };

            if (Mod.instance.magic)
            {

                interfaceComponents.Remove(204);

                interfaceComponents.Remove(303);

            }

        }
        
        public override void populateContent()
        {

            contentColumns = 12;

            pagination = 0;

            contentComponents = Mod.instance.herbalData.JournalBombs();

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key/3, xPositionOnScreen, yPositionOnScreen, width, height, 0, 56);

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

                case journalButtons.clearBuffs:

                    Mod.instance.herbalData.RemoveBuffs();

                    return;

                case journalButtons.refresh:
                        
                    Mod.instance.herbalData.MassGrind();

                    populateContent();

                    activateInterface();

                    return;

                case journalButtons.back:

                    DruidJournal.openJournal(journalTypes.herbalism);

                    return;

                case journalButtons.forward:

                    DruidJournal.openJournal(journalTypes.omens);

                    return;

                case journalButtons.end:

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

            if (contentComponents[focus].type == ContentComponent.contentTypes.toggleleft)
            {

                Mod.instance.herbalData.PotionBehaviour(contentComponents[focus].id);

                populateContent();

                return;

            }

            if (contentComponents[focus].type == ContentComponent.contentTypes.toggleright)
            {

                Mod.instance.herbalData.ConvertToGoods(contentComponents[focus].id, amount);

                populateContent();

                return;

            }

            string herbalId = contentComponents[focus].id;

            Mod.instance.herbalData.BrewHerbal(herbalId, amount, false, true);

            populateContent();

            return;

        }

        public override void pressCancel()
        {

            string herbalId = contentComponents[focus].id;

            if (!Mod.instance.herbalData.herbalism.ContainsKey(herbalId))
            {

                return;

            }

            Herbal herbal = Mod.instance.herbalData.herbalism[herbalId];

            if (Mod.instance.herbalData.buff.applied.ContainsKey(herbal.buff))
            {

                Game1.playSound("ghost");

                return;

            }

            if (HerbalHandle.GetHerbalism(herbal.herbal) > 0)
            {

                Game1.playSound("smallSelect");

                Mod.instance.herbalData.ConsumeHerbal(herbalId, true);

                populateContent();

            }

            return;

        }

    }

}
