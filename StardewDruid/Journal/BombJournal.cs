using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
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

                [301] = addButton(journalButtons.exit),

            };

        }
        
        public override void populateContent()
        {

            pagination = 36;

            contentComponents = Mod.instance.herbalData.JournalBombs();

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                if(component.Value.type == ContentComponent.contentTypes.potion)
                {

                    component.Value.setBounds((int)(component.Key / 2), xPositionOnScreen, yPositionOnScreen, width, height);

                }
                else
                {

                    component.Value.setBounds((int)(component.Key / 2), xPositionOnScreen, yPositionOnScreen, width, height);

                }

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

                    Mod.instance.herbalData.ClearBuffs();

                    return;

                case journalButtons.refresh:
                        
                    Mod.instance.herbalData.MassGrind();

                    populateContent();

                    activateInterface();

                    return;

                case journalButtons.back:

                    DruidJournal.openJournal(journalTypes.herbalism);

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

        public override void pressContent()
        {
            
            if (contentComponents[focus].type == ContentComponent.contentTypes.toggle)
            {

                Mod.instance.herbalData.PotionBehaviour(contentComponents[focus].id);

                populateContent();

                return;

            }

            string herbalId = contentComponents[focus].id;

            if (Mod.instance.herbalData.herbalism[herbalId].status == 1)
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

                Mod.instance.herbalData.BrewHerbal(herbalId, amount, false, true);

                populateContent();

                return;

            }

        }

        public override void pressCancel()
        {

            string herbalId = contentComponents[focus].id;

            if (!Mod.instance.herbalData.herbalism.ContainsKey(herbalId))
            {

                return;

            }

            Herbal herbal = Mod.instance.herbalData.herbalism[herbalId];

            if (Mod.instance.herbalData.applied.ContainsKey(herbal.buff))
            {

                Game1.playSound("ghost");

                return;

            }

            if (HerbalData.UpdateHerbalism(herbal.herbal) > 0)
            {

                Game1.playSound("smallSelect");

                Mod.instance.herbalData.ConsumeHerbal(herbalId);

                populateContent();

            }

            return;

        }

    }

}
