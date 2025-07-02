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
    public class HerbalJournal : DruidJournal
    {

        public HerbalJournal(string QuestId, int Record) : base(QuestId, Record)
        {

        }

        public override void populateInterface()
        {

            type = journalTypes.herbalism;

            title = StringData.Strings(StringData.stringkeys.apothecary);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.quests),
                [102] = addButton(journalButtons.effects),
                [103] = addButton(journalButtons.relics),
                [104] = addButton(journalButtons.herbalism),
                [105] = addButton(journalButtons.lore),
                [106] = addButton(journalButtons.transform),
                [107] = addButton(journalButtons.recruits),

                [201] = addButton(journalButtons.refresh),
                [202] = addButton(journalButtons.bombs),
                [203] = addButton(journalButtons.omens),
                [204] = addButton(journalButtons.goods),

                [301] = addButton(journalButtons.exit),
                [302] = addButton(journalButtons.forward),
                [303] = addButton(journalButtons.end),

            };

            /*if (!RelicData.HasRelic(StardewDruid.Data.IconData.relics.heiress_gift))
            {

                interfaceComponents[107] = addButton(journalButtons.pals);

            }*/

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

            contentComponents = Mod.instance.herbalData.JournalHerbals();

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

                case journalButtons.forward:

                    DruidJournal.openJournal(journalTypes.bombs);

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

            if (herbal.type == Herbal.herbalType.resource)
            {

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

        public override void drawContent(SpriteBatch b)
        {

            b.DrawString(Game1.smallFont, StringData.Strings(StardewDruid.Data.StringData.stringkeys.HP), new Vector2(xPositionOnScreen + width + 16, yPositionOnScreen + 96), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

            b.DrawString(Game1.smallFont, Game1.player.health + "/" + Game1.player.maxHealth, new Vector2(xPositionOnScreen + width + 16, yPositionOnScreen + 128), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

            b.DrawString(Game1.smallFont, StringData.Strings(StardewDruid.Data.StringData.stringkeys.STM), new Vector2(xPositionOnScreen + width + 16, yPositionOnScreen + 176), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

            b.DrawString(Game1.smallFont, (int)Game1.player.Stamina + " /" + Game1.player.MaxStamina, new Vector2(xPositionOnScreen + width + 16, yPositionOnScreen + 208), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

            int contentCount = contentComponents.Count;

            if(contentCount == 0)
            {

                return;

            }

            int top = record - (record % contentCount);

            for (int i = 0; i < contentCount; i++)
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

            if (contentComponents[focus].type != ContentComponent.contentTypes.potion)
            {

                drawHoverText(b, contentComponents[focus].text[0]);

                return;

            }

            string herbalId = contentComponents[focus].id;

            Herbal herbal = Mod.instance.herbalData.herbalism[herbalId];

            string herbalTitle = herbal.title;

            int amount = HerbalHandle.GetHerbalism(herbal.herbal);

            if (amount > 0)
            {

                herbalTitle += " x" + amount.ToString();

            }

            string herbalDescription = herbal.description;

            List<string> details = new(herbal.details);

            if(herbal.stamina > 0)
            {

                details.Add(herbal.staminaReadout);

            }

            switch (Mod.instance.herbalData.BuffStatus(herbal))
            {
                
                case 1:

                    details.Add(StringData.Strings(StringData.stringkeys.applyBuff));

                    break;

                case 2:

                    details.Add(StringData.Strings(StringData.stringkeys.activeBuff));

                    break;

            }

            List<string> potionreqs = new(herbal.potionrequirements);

            List<string> itemreqs = new(herbal.itemrequirements);

            Dictionary<HerbalHandle.herbals, int> potions = HerbalHandle.GetPotions(herbal);

            Dictionary<string, int> items = HerbalHandle.GetItems(herbal);

            int craftable = HerbalHandle.GetCraftable(herbal);

            if (craftable == 1000)
            {

                herbalTitle += " " + StringData.Strings(StringData.stringkeys.MAX);

            }
            else if (craftable > 0)
            {

                herbalTitle += " (" + craftable + ")";

            }

            float contentHeight = 16;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(herbalTitle, Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            contentHeight += titleSize.Y;

            contentHeight += 8;

            // -------------------------------------------------------
            // description

            contentHeight += 12;

            string descriptionText = Game1.parseText(herbalDescription, Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            contentHeight += descriptionSize.Y;

            contentHeight += 12;

            // -------------------------------------------------------
            // details


            if (details.Count > 0)
            {

                contentHeight += 12;

                foreach (string detail in details)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    contentHeight += detailSize.Y;

                }

                contentHeight += 12;

            }

            // -------------------------------------------------------
            // potions

            bool potionSection = false;

            if (potionreqs.Count > 0)
            {

                contentHeight += (32 * potionreqs.Count);

                potionSection = true;

            }

            if (potions.Count > 0)
            {

                contentHeight += (32 * potions.Count);

                potionSection = true;

            }

            if (potionSection)
            {

                contentHeight += 24;

            }

            // -------------------------------------------------------
            // items

            bool itemSection = false;

            if (itemreqs.Count > 0)
            {

                contentHeight += (32 * itemreqs.Count);

                itemSection = true;

            }

            if (items.Count > 0)
            {

                contentHeight += (32 * items.Count);

                itemSection = true;

            }

            if (itemSection)
            {

                contentHeight += 24;

            }


            contentHeight += 8; // bottom


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

            // -------------------------------------------------------
            // description

            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);
            
            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

            textPosition += 12;

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += descriptionSize.Y;

            textPosition += 12;

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
            // potions

            if (potionSection)
            {

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

                textPosition += 12;

            }

            if (potionreqs.Count > 0)
            {

                foreach (string requirement in potionreqs)
                {

                    b.DrawString(Game1.smallFont, requirement, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, requirement, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

                    textPosition += 32;

                }

            }

            if (potions.Count > 0)
            {

                foreach (KeyValuePair<HerbalHandle.herbals, int> potion in potions)
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

            }

            if (potionSection)
            {

                textPosition += 12;

            }

            // ---------------------------------------------------------
            // items

            if (itemSection)
            {

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

                textPosition += 12;

            }

            if (itemreqs.Count > 0)
            {

                foreach(string requirement in itemreqs)
                {

                    b.DrawString(Game1.smallFont, requirement, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, requirement, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

                    textPosition += 32;

                }

            }

            if (items.Count > 0)
            {

                foreach (KeyValuePair<string, int> item in items)
                {

                    ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(item.Key);

                    Rectangle itemSource = dataOrErrorItem.GetSourceRect();

                    if(itemSource.Height > 16)
                    {

                        itemSource.Height = 16; itemSource.Y += 16;

                    }

                    b.Draw(dataOrErrorItem.GetTexture(), new Vector2(textMargin, textPosition), itemSource, Color.White, 0f, Vector2.Zero, 2f, 0, 1f);

                    b.DrawString(Game1.smallFont, dataOrErrorItem.DisplayName, new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, dataOrErrorItem.DisplayName, new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    string displayAmount = "(" + item.Value + ")";

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - (displayAmount.Length * 16), textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - (displayAmount.Length * 16) - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += 32;

                }

            }

            if (itemSection)
            {

                textPosition += 12;

            }
        }

    }

}
