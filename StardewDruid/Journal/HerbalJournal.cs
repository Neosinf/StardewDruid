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
    public class HerbalJournal : DruidJournal
    {

        public HerbalJournal(string QuestId, int Record) : base(QuestId, Record)
        {

        }

        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.quests),
                [102] = addButton(journalButtons.effects),
                [103] = addButton(journalButtons.relics),
                [104] = addButton(journalButtons.herbalism),
                [105] = addButton(journalButtons.lore),
                [106] = addButton(journalButtons.transform),
                [107] = addButton(journalButtons.recruits),

                [201] = addButton(journalButtons.bombs),
                [202] = addButton(journalButtons.refresh),

                [301] = addButton(journalButtons.exit),

            };

        }
        
        public override void populateContent()
        {

            type = journalTypes.herbalism;

            title = StringData.Strings(StringData.stringkeys.apothecary);

            pagination = 36;

            contentComponents = Mod.instance.herbalData.JournalHerbals();
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

            fadeMenu();

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.refresh:
                        
                    Mod.instance.herbalData.MassBrew();

                    populateContent();

                    activateInterface();

                    return;

                case journalButtons.back:
                case journalButtons.forward:

                    DruidJournal.openJournal(journalTypes.bombs);

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

            if (herbal.resource)
            {

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

        public override void drawContent(SpriteBatch b)
        {

            b.DrawString(Game1.smallFont, StringData.Strings(StardewDruid.Data.StringData.stringkeys.HP), new Vector2(xPositionOnScreen + width + 16, yPositionOnScreen + 96), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

            b.DrawString(Game1.smallFont, Game1.player.health + "/" + Game1.player.maxHealth, new Vector2(xPositionOnScreen + width + 16, yPositionOnScreen + 128), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

            b.DrawString(Game1.smallFont, StringData.Strings(StardewDruid.Data.StringData.stringkeys.STM), new Vector2(xPositionOnScreen + width + 16, yPositionOnScreen + 176), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

            b.DrawString(Game1.smallFont, (int)Game1.player.Stamina + " /" + Game1.player.MaxStamina, new Vector2(xPositionOnScreen + width + 16, yPositionOnScreen + 208), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

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

        }

        public override void drawHover(SpriteBatch b)
        {

            base.drawHover(b);

            if (!browsing)
            {

                return;

            }

            if (contentComponents[focus].type == ContentComponent.contentTypes.toggle)
            {

                return;

            }

            string herbalId = contentComponents[focus].id;

            List<string> details = new();

            Dictionary<string, int> items = new();

            Dictionary<HerbalData.herbals, int> potions = new();

            Herbal herbal = Mod.instance.herbalData.herbalism[herbalId];

            string herbalTitle = herbal.title;

            int amount = HerbalData.UpdateHerbalism(herbal.herbal);

            if (amount > 0)
            {

                herbalTitle += " x" + amount.ToString();

            }

            int baseAmount = 0;

            if (herbal.bases.Count > 0)
            {

                foreach (HerbalData.herbals basePotion in herbal.bases)
                {

                    int potionAmount = HerbalData.UpdateHerbalism(basePotion);

                    potions.Add(basePotion, potionAmount);

                }

                if (potions.Count > 0)
                {

                    baseAmount = potions.Values.Min();
                
                }

            }

            if (herbal.status == 3)
            {

                herbalTitle += " " + StringData.Strings(StringData.stringkeys.MAX);

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

            if (herbal.stamina > 0)
            {

                float difficulty = 1.6f - (Mod.instance.ModDifficulty() * 0.1f);

                int staminaGain = (int)(herbal.stamina * difficulty);

                int healthGain = (int)(herbal.health * difficulty);

                details.Add(Mod.instance.Helper.Translation.Get("HerbalData.327.1").Tokens(new { stamina = staminaGain.ToString(), health = healthGain.ToString() }));

            }

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

                    b.Draw(Mod.instance.iconData.potionsTexture, new Vector2(textMargin, textPosition), IconData.PotionRectangles(basePotion.display), Color.White, 0f, Vector2.Zero, 1.5f, 0, 0.901f);

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
