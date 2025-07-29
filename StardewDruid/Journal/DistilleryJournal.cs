using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class DistilleryJournal : DruidJournal
    {

        public DistilleryJournal(string QuestId, int Record) : base(QuestId, Record)
        {


        }

        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openProductionEstimated),

                [102] = addButton(journalButtons.openProductionRecent),

                [201] = addButton(journalButtons.refresh),

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void activateInterface()
        {

            resetInterface();

        }

        public override void populateContent()
        {

            type = journalTypes.distillery;

            title = JournalData.JournalTitle(type);

            pagination = 12;

            Mod.instance.exportHandle.CalculateOutput();

            contentComponents = Mod.instance.exportHandle.JournalDistillery();

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key % pagination, xPositionOnScreen, yPositionOnScreen, width, height);

            }

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.refresh:

                    populateContent();

                    return;

                case journalButtons.openProductionEstimated:

                    DruidJournal.openJournal(journalTypes.distilleryEstimated);

                    return;

                case journalButtons.openProductionRecent:

                    DruidJournal.openJournal(journalTypes.distilleryRecent);

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

        public override void pressContent()
        {

            if(contentComponents[focus].type == ContentComponent.contentTypes.estimate)
            {

                return;

            }

            ExportHandle.exports export = Enum.Parse<ExportHandle.exports>(contentComponents[focus].id);

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

            if (Mod.instance.exportHandle.CraftMachine(export, amount))
            {

                populateContent();

                return;

            }
            else
            {

                Game1.playSound(SpellHandle.Sounds.ghost.ToString());

            }

        }

        public override void drawHover(SpriteBatch b)
        {

            base.drawHover(b);

            if (!browsing)
            {

                return;

            }

            if (contentComponents[focus].type == ContentComponent.contentTypes.estimate)
            {

                DrawEstimate(b);

                return;

            }

            float contentHeight = 16;

            ExportHandle.exports export = Enum.Parse<ExportHandle.exports>(contentComponents[focus].id);

            ExportMachine machine = Mod.instance.exportHandle.machines[export];

            // -------------------------------------------------------

            string titleText = Game1.parseText(machine.name, Game1.dialogueFont, 412);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            contentHeight += 24 + titleSize.Y;

            // -------------------------------------------------------

            string descriptionText = Game1.parseText(machine.description, Game1.smallFont, 412);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            contentHeight += 24 + descriptionSize.Y;

            // -------------------------------------------------------

            string technicalText = Game1.parseText(machine.technical, Game1.smallFont, 412);

            Vector2 technicalSize = Game1.smallFont.MeasureString(technicalText);

            contentHeight += 24 + technicalSize.Y;

            // -------------------------------------------------------

            contentHeight += 32;

            contentHeight += (32 * machine.resources.Count);

            contentHeight += 24;

            // =================================================================================================

            int cornerX = Game1.getMouseX() + 32;

            int cornerY = Game1.getMouseY() + 32;

            if (cornerX > Game1.graphics.GraphicsDevice.Viewport.Width - 448)
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

            IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), (int)corner.X, (int)corner.Y, 448, (int)(contentHeight), Color.White, 1f, true, -1f);

            float textPosition = corner.Y + 16;

            float textMargin = corner.X + 16;

            // -------------------------------------------------------

            b.DrawString(Game1.dialogueFont, titleText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.dialogueFont, titleText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 8 + titleSize.Y;

            Color outerTop = new(167, 81, 37);

            Color outerBot = new(139, 58, 29);

            Color inner = new(246, 146, 30);

            // -------------------------------------------------------

            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 424, 2), outerTop);

            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 424, 3), inner);

            textPosition += 12;

            // -------------------------------------------------------

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + descriptionSize.Y;

            b.DrawString(Game1.smallFont, technicalText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, technicalText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + technicalSize.Y;

            // --------------------------------------------------------

            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 424, 2), outerTop);

            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 424, 3), inner);

            textPosition += 12;

            b.Draw(Mod.instance.iconData.relicsTexture, new Vector2(textMargin, textPosition), IconData.RelicRectangles(PalHandle.PalRelic(machine.pal)), Color.White, 0f, Vector2.Zero, 1.5f, 0, 0.901f);

            b.DrawString(Game1.smallFont, machine.labour.ToString(), new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, machine.labour.ToString(), new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

            string palTitle = PalHandle.PalTitle(machine.pal);

            b.DrawString(Game1.smallFont, palTitle, new Vector2(textMargin + 96, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, palTitle, new Vector2(textMargin + 96 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

            int availablePal = 0;

            if (Mod.instance.save.pals.ContainsKey(machine.pal))
            {

                availablePal = Mod.instance.save.pals[machine.pal].caught - Mod.instance.save.pals[machine.pal].hired;

            }

            string amountPal = "(" + availablePal + ")";

            Vector2 amountPalParse = Game1.smallFont.MeasureString(amountPal);

            b.DrawString(Game1.smallFont, amountPal, new Vector2(textMargin + 412 - amountPalParse.X, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, amountPal, new Vector2(textMargin + 412 - amountPalParse.X - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

            textPosition += 32;

            foreach (KeyValuePair<string, int> item in machine.resources)
            {

                ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(item.Key);

                b.Draw(dataOrErrorItem.GetTexture(), new Vector2(textMargin, textPosition), dataOrErrorItem.GetSourceRect(), Color.White, 0f, Vector2.Zero, 2f, 0, 1f);

                b.DrawString(Game1.smallFont, item.Value.ToString(), new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, item.Value.ToString(), new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                string displayItem = dataOrErrorItem.DisplayName;

                b.DrawString(Game1.smallFont, displayItem, new Vector2(textMargin + 96, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, displayItem, new Vector2(textMargin + 96 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                int availableItem = Game1.player.Items.CountId(item.Key);

                string amountItem = "(" + availableItem + ")";

                Vector2 amountParse = Game1.smallFont.MeasureString(amountItem);

                b.DrawString(Game1.smallFont, amountItem, new Vector2(textMargin + 412 - amountParse.X, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, amountItem, new Vector2(textMargin + 412 - amountParse.X - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                textPosition += 32;

            }

        }

        public void DrawEstimate(SpriteBatch b)
        {

            float contentHeight = 16;

            ExportHandle.exports export = Enum.Parse<ExportHandle.exports>(contentComponents[focus].id);

            ExportGood good = Mod.instance.exportHandle.goods[export];

            // -------------------------------------------------------

            string titleText = Game1.parseText(good.name, Game1.dialogueFont, 412);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            contentHeight += 24 + titleSize.Y;

            // -------------------------------------------------------

            string descriptionText = Game1.parseText(good.technical, Game1.smallFont, 412);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            contentHeight += 12 + descriptionSize.Y;

            // -------------------------------------------------------

            string technicalText = Game1.parseText(good.technical, Game1.smallFont, 412);

            Vector2 technicalSize = Game1.smallFont.MeasureString(technicalText);

            contentHeight += 24 + technicalSize.Y;

            // -------------------------------------------------------

            string priceText = String.Empty;

            if (Mod.instance.exportHandle.stock.ContainsKey(export))
            {
                contentHeight += 12;

                string price = StringData.Strings(StringData.stringkeys.currentPrice) + Mod.instance.exportHandle.stock[export][1].ToString() + StringData.currency;

                priceText = Game1.parseText(price, Game1.smallFont, 412);

                Vector2 priceSize = Game1.smallFont.MeasureString(priceText);

                contentHeight += 12 + priceSize.Y;

            }

            // =================================================================================================

            int cornerX = Game1.getMouseX() + 32;

            int cornerY = Game1.getMouseY() + 32;

            if (cornerX > Game1.graphics.GraphicsDevice.Viewport.Width - 448)
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

            IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), (int)corner.X, (int)corner.Y, 448, (int)(contentHeight), Color.White, 1f, true, -1f);

            float textPosition = corner.Y + 16;

            float textMargin = corner.X + 16;

            // -------------------------------------------------------

            b.DrawString(Game1.dialogueFont, titleText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.dialogueFont, titleText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 8 + titleSize.Y;

            Color outerTop = new(167, 81, 37);

            Color outerBot = new(139, 58, 29);

            Color inner = new(246, 146, 30);

            // -------------------------------------------------------

            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 424, 2), outerTop);

            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 424, 3), inner);

            textPosition += 12;

            // -------------------------------------------------------

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + descriptionSize.Y;
            
            // -------------------------------------------------------
            // technical

            b.DrawString(Game1.smallFont, technicalText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, technicalText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + technicalSize.Y;

            if (priceText != String.Empty)
            {

                b.DrawString(Game1.smallFont, priceText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, priceText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f); ;

                textPosition += 12 + descriptionSize.Y;

            }
        }

    }

}
