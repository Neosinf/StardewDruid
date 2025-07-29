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
    public class PowderJournal : HerbalJournal
    {

        public PowderJournal(string QuestId, int Record) : base(QuestId, Record)
        {

        }

        public override void populateInterface()
        {

            type = journalTypes.powders;

            title = JournalData.JournalTitle(type);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openQuests),
                [102] = addButton(journalButtons.openMasteries),
                [103] = addButton(journalButtons.openRelics),
                [104] = addButton(journalButtons.openAlchemy),
                [105] = addButton(journalButtons.openPotions),
                [106] = addButton(journalButtons.openCompanions),
                [107] = addButton(journalButtons.openDragonomicon),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.openGoods),

                [301] = addButton(journalButtons.exit),
                [302] = addButton(journalButtons.HP),
                [303] = addButton(journalButtons.STM),
                [304] = addButton(journalButtons.refresh),
                [305] = addButton(journalButtons.clearBuffs),
                [306] = addButton(journalButtons.forward),
                [307] = addButton(journalButtons.end),
            };

        }
        
        public override void populateContent()
        {

            contentColumns = 12;

            pagination = 0;

            contentComponents = Mod.instance.herbalHandle.JournalBombs();

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key/3, xPositionOnScreen, yPositionOnScreen, width, height, 0, 56);

            }

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.clearBuffs:

                    Mod.instance.herbalHandle.RemoveBuffs();

                    return;

                case journalButtons.refresh:
                        
                    Mod.instance.herbalHandle.MassGrind();

                    populateContent();

                    activateInterface();

                    return;

                case journalButtons.back:

                    DruidJournal.openJournal(journalTypes.herbalism);

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

            if (contentComponents[focus].type == ContentComponent.contentTypes.toggleleft)
            {

                Mod.instance.herbalHandle.PotionBehaviour(contentComponents[focus].id);

                populateContent();

                return;

            }

            if (contentComponents[focus].type == ContentComponent.contentTypes.toggleright)
            {

                Mod.instance.herbalHandle.ConvertToGoods(contentComponents[focus].id, amount);

                populateContent();

                return;

            }

            string herbalId = contentComponents[focus].id;

            Mod.instance.herbalHandle.BrewHerbal(herbalId, amount, false, true);

            populateContent();

            return;

        }

        public override void pressCancel()
        {

            string herbalId = contentComponents[focus].id;

            if (!Mod.instance.herbalHandle.herbalism.ContainsKey(herbalId))
            {

                return;

            }

            Herbal herbal = Mod.instance.herbalHandle.herbalism[herbalId];

            if (Mod.instance.herbalHandle.buff.applied.ContainsKey(herbal.buff))
            {

                Game1.playSound("ghost");

                return;

            }

            if (HerbalHandle.GetHerbalism(herbal.herbal) > 0)
            {

                Game1.playSound("smallSelect");

                Mod.instance.herbalHandle.ConsumeHerbal(herbalId, true);

                populateContent();

            }

            return;

        }

    }

}
