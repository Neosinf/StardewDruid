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
using System.Reflection;
using static StardewDruid.Data.DialogueData;
using static StardewDruid.Journal.HerbalData;


namespace StardewDruid.Journal
{
    public class HerbalJournal : DruidJournal
    {

        public HerbalJournal(string QuestId, int Record) : base(QuestId, Record)
        {

        }

        public override void populateInterface()
        {

            type = journalTypes.herbalism;

            title = DialogueData.Strings(DialogueData.stringkeys.apothecary);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.quests),
                [102] = addButton(journalButtons.effects),
                [103] = addButton(journalButtons.relics),
                [104] = addButton(journalButtons.herbalism),

                [106] = addButton(journalButtons.refresh),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),

                [203] = addButton(journalButtons.headerOne),
                [204] = addButton(journalButtons.headerTwo),
                [205] = addButton(journalButtons.headerThree),

                [301] = addButton(journalButtons.exit),

                [305] = addButton(journalButtons.end),
                [306] = addButton(journalButtons.forward),

            };

        }
        
        public override void populateContent()
        {

            pagination = 18;

            contentComponents = Mod.instance.herbalData.JournalHerbals(); //Mod.instance.questHandle.JournalEffects();

            otherComponents = Mod.instance.herbalData.JournalHeaders();

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key % pagination, xPositionOnScreen, yPositionOnScreen, width, height);

            }

            foreach (KeyValuePair<int, ContentComponent> component in otherComponents)
            {

                component.Value.setBounds(component.Key % 3, xPositionOnScreen + 56, yPositionOnScreen, width - 56, height);

            }

        }

        public override void activateInterface()
        {

            resetInterface();

            fadeMenu();

            Dictionary<int, HerbalData.herbals> herbalButtons = new()
            {
                [203] = HerbalData.herbals.ligna,
                [204] = HerbalData.herbals.impes,
                [205] = HerbalData.herbals.celeri,
            };

            foreach(KeyValuePair<int, HerbalData.herbals> herbalButton in herbalButtons)
            {

                IconData.displays flag = IconData.displays.complete;

                DialogueData.stringkeys hovertext = DialogueData.stringkeys.acEnabled;

                if (Mod.instance.save.potions.ContainsKey(herbalButton.Value))
                {

                    switch (Mod.instance.save.potions[herbalButton.Value])
                    {

                        case 0:

                            flag = IconData.displays.exit;
                            hovertext = DialogueData.stringkeys.acDisabled;
                            break;

                        case 1:

                            flag = IconData.displays.complete;

                            break;

                        case 2:

                            flag = IconData.displays.flag;
                            hovertext = DialogueData.stringkeys.acPriority;
                            break;
                    }
                }

                interfaceComponents[herbalButton.Key].display = flag;

                interfaceComponents[herbalButton.Key].source = IconData.DisplayRectangle(flag);

                interfaceComponents[herbalButton.Key].text = DialogueData.Strings(hovertext);

            }

            int firstOnThisPage = record - (record % pagination);

            int thispage = firstOnThisPage == 0 ? 0 : firstOnThisPage / pagination;

            int last = contentComponents.Count - 1;

            int firstOnLastPage = last - (last % pagination);

            int lastpage = firstOnLastPage == 0 ? 0 : firstOnLastPage / pagination;

            if (thispage == 0)
            {

                // back
                interfaceComponents[201].active = false;

                // start
                interfaceComponents[202].active = false;

            }

            if (lastpage == thispage)
            {

                // forward
                interfaceComponents[305].active = false;

                // end
                interfaceComponents[306].active = false;

            }

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.refresh:
                        
                    Mod.instance.herbalData.MassBrew();

                    populateContent();

                    return;

                case journalButtons.headerOne:

                    Mod.instance.herbalData.PotionBehaviour(HerbalData.herbals.ligna);

                    populateContent();

                    return;

                case journalButtons.headerTwo:

                    Mod.instance.herbalData.PotionBehaviour(HerbalData.herbals.impes);

                    populateContent();

                    return;

                case journalButtons.headerThree:

                    Mod.instance.herbalData.PotionBehaviour(HerbalData.herbals.celeri);

                    populateContent();

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

        public override void pressContent()
        {

            string herbalId = contentComponents[focus].id;

            if (Mod.instance.herbalData.herbalism[herbalId].status == 1)
            {

                int amount = 1;

                if (Mod.instance.Helper.Input.GetState(SButton.LeftShift) == SButtonState.Held)
                {
                    amount = 10;
                }
                else if (Mod.instance.Helper.Input.GetState(SButton.RightShift) == SButtonState.Held)
                {
                    amount = 10;
                }
                else if (Mod.instance.Helper.Input.GetState(SButton.LeftControl) == SButtonState.Held)
                {
                    amount = 50;
                }
                else if (Mod.instance.Helper.Input.GetState(SButton.RightControl) == SButtonState.Held)
                {
                    amount = 50;
                }

                Mod.instance.herbalData.BrewHerbal(herbalId, amount);

                populateContent();

                return;

            }

        }

        public override void pressCancel()
        {

            if (Game1.player.health == Game1.player.maxHealth & Game1.player.Stamina == Game1.player.MaxStamina)
            {

                return;

            }

            string herbalId = contentComponents[focus].id;

            if (!Mod.instance.herbalData.herbalism.ContainsKey(herbalId))
            {

                return;

            }

            Herbal herbal = Mod.instance.herbalData.herbalism[herbalId];

            if (herbal.resource)
            {

                return;

            }

            if (Mod.instance.save.herbalism.ContainsKey(herbal.herbal))
            {

                if (Mod.instance.save.herbalism[herbal.herbal] > 0)
                {

                    Game1.playSound("smallSelect");

                    Mod.instance.herbalData.ConsumeHerbal(herbalId);

                    populateContent();

                }

            }

            return;

        }

        public override void drawContent(SpriteBatch b)
        {

            b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.HP) + " " + Game1.player.health + "/" + Game1.player.maxHealth, new Vector2(xPositionOnScreen + width - 304, yPositionOnScreen - 64), Color.Wheat, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.88f);

            b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.STM) + " " + (int)Game1.player.Stamina + " /" + Game1.player.MaxStamina, new Vector2(xPositionOnScreen + width - 304, yPositionOnScreen - 32), Color.Wheat, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.88f);

            int top = record - (record % pagination);

            for (int i = 0; i < pagination; i++)
            {

                int index = top + i;

                if (contentComponents.Count <= index)
                {
                    
                    break;
                
                }

                if (!contentComponents[index].active)
                {

                    continue;

                }

                ContentComponent component = contentComponents[index];

                component.draw(b, Microsoft.Xna.Framework.Vector2.Zero, (browsing && index == focus));

            }

            int header = (top / (pagination / 3));

            for (int i = 0; i < 3; i++)
            {

                int index = header + i;

                if (otherComponents.Count <= index)
                {
                    break;
                }

                ContentComponent component = otherComponents[index];

                component.draw(b, Microsoft.Xna.Framework.Vector2.Zero);

            }

        }

        public override void drawHover(SpriteBatch b)
        {

            base.drawHover(b);

            if (!browsing)
            {

                return;

            }

            string herbalId = contentComponents[focus].id;

            List<string> details = new();

            Dictionary<string, int> items = new();

            Dictionary<HerbalData.herbals, int> potions = new();

            Herbal herbal = Mod.instance.herbalData.herbalism[herbalId];

            string herbalTitle = herbal.title;

            int amount = 0;

            if (Mod.instance.save.herbalism.ContainsKey(herbal.herbal))
            {

                amount = Mod.instance.save.herbalism[herbal.herbal];

                herbalTitle += " x" + amount.ToString();

            }

            int baseAmount = 0;

            if (herbal.bases.Count > 0)
            {

                foreach (HerbalData.herbals basePotion in herbal.bases)
                {

                    int potionAmount = 0;

                    if (Mod.instance.save.herbalism.ContainsKey(basePotion))
                    {

                        potionAmount = Mod.instance.save.herbalism[basePotion];

                        if (baseAmount == 0)
                        {

                            baseAmount = potionAmount;

                        }
                        else
                        {

                            baseAmount = Math.Min(potionAmount, baseAmount);

                        }

                    }

                    potions.Add(basePotion, potionAmount);

                }

            }

            if (herbal.status == 3)
            {

                herbalTitle += " " + DialogueData.Strings(DialogueData.stringkeys.MAX);

            }
            else if (herbal.status == 1)
            {

                int craftable = 0;

                foreach (KeyValuePair<string, int> ingredient in herbal.amounts)
                {

                    craftable += ingredient.Value;

                }

                craftable = Math.Min(999 - amount, craftable);

                if (herbal.bases.Count > 0)
                {

                    craftable = Math.Min(craftable, baseAmount);

                }

                herbalTitle += " (" + craftable.ToString() + ")";

            }

            string herbalDescription = herbal.description;

            details = new(herbal.details);

            foreach (string ingredient in herbal.ingredients)
            {

                int herbalAmount = 0;

                if (herbal.amounts.ContainsKey(ingredient))
                {

                    herbalAmount = herbal.amounts[ingredient];

                }

                items.Add(ingredient, herbalAmount);

            }

            float contentHeight = 16;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(herbalTitle, Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            contentHeight += 24 + titleSize.Y;

            // -------------------------------------------------------
            // description

            string descriptionText = Game1.parseText(herbalDescription, Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            contentHeight += 24 + descriptionSize.Y;

            if (details.Count > 0)
            {

                foreach (string detail in details)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    contentHeight += detailSize.Y;

                }

                contentHeight += 24;

            }

            if (potions.Count > 0 || items.Count > 0)
            {

                contentHeight += (32 * potions.Count);

                contentHeight += (32 * items.Count);

                contentHeight += 24;

            }

            // -------------------------------------------------------
            // texturebox

            int cornerX = Game1.getMouseX() + 32;

            int cornerY = Game1.getMouseY() + 32;

            if (cornerX > Game1.graphics.GraphicsDevice.Viewport.Width - 512)
            {

                int tryCorner = cornerX - 576;

                cornerX = tryCorner < 0 ? 0 : tryCorner;

            }

            if (cornerY > Game1.graphics.GraphicsDevice.Viewport.Height - contentHeight - 48)
            {

                int tryCorner = cornerY - (int)(contentHeight + 64f);

                cornerY = tryCorner < 0 ? 0 : tryCorner;

            }

            Vector2 corner = new(cornerX, cornerY);

            IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), (int)corner.X, (int)corner.Y, 512, (int)(contentHeight), Color.White, 1f, true, -1f);

            float textPosition = corner.Y + 16;

            float textMargin = corner.X + 16;

            // -------------------------------------------------------
            // title

            b.DrawString(Game1.dialogueFont, titleText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.dialogueFont, titleText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 8 + titleSize.Y;

            Color outerTop = new(167, 81, 37);

            Color outerBot = new(139, 58, 29);

            Color inner = new(246, 146, 30);

            // --------------------------------
            // top

            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);
            
            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

            textPosition += 12;

            // -------------------------------------------------------
            // description

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + descriptionSize.Y;

            // -------------------------------------------------------
            // details

            if (details.Count > 0)
            {
                
                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);
                
                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

                textPosition += 12;

                foreach (string detail in details)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin, textPosition), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += detailSize.Y;

                }

                textPosition += 12;

            }

            // ---------------------------------------------------------
            // items

            if (potions.Count > 0 || items.Count > 0)
            {
                
                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);
                
                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

                textPosition += 12;

                foreach (KeyValuePair<HerbalData.herbals, int> potion in potions)
                {

                    Herbal basePotion = Mod.instance.herbalData.herbalism[potion.Key.ToString()];

                    Microsoft.Xna.Framework.Color colour = Mod.instance.iconData.SchemeColour(Mod.instance.herbalData.schemes[basePotion.line]);

                    b.Draw(Mod.instance.iconData.relicsTexture, new Vector2(textMargin, textPosition), IconData.RelicRectangles(basePotion.container), Color.White, 0f, Vector2.Zero, 1.5f, 0, 0.901f);

                    b.Draw(Mod.instance.iconData.relicsTexture, new Vector2(textMargin, textPosition), IconData.RelicRectangles(basePotion.content), colour, 0f, Vector2.Zero, 1.5f, 0, 0.902f);

                    b.DrawString(Game1.smallFont, basePotion.title, new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, basePotion.title, new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    string displayAmount = "(" + potion.Value + ")";

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - (displayAmount.Length * 16), textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - (displayAmount.Length * 16) - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += 32;

                }

                foreach (KeyValuePair<string, int> item in items)
                {

                    ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(item.Key);

                    b.Draw(dataOrErrorItem.GetTexture(), new Vector2(textMargin, textPosition), dataOrErrorItem.GetSourceRect(), Color.White, 0f, Vector2.Zero, 2f, 0, 1f);

                    b.DrawString(Game1.smallFont, dataOrErrorItem.DisplayName, new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, dataOrErrorItem.DisplayName, new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    string displayAmount = "(" + item.Value + ")";

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - (displayAmount.Length * 16), textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - (displayAmount.Length * 16) - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += 32;

                }

            }

        }

    }

}
