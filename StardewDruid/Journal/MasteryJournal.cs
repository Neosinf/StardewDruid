using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class MasteryJournal : DruidJournal
    {

        public MasteryJournal(string QuestId, int Record) : base(QuestId, Record)
        {


        }

        public override void prepareContent()
        {

        }

        public override void populateContent()
        {

            type = journalTypes.masteries;

            title = JournalData.JournalTitle(type);

            pagination = 0;

            contentComponents = Mod.instance.masteryHandle.SectionComponents();

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key, xPositionOnScreen, yPositionOnScreen, width, height);

            }

        }

        public override void populateInterface()
        {


            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openQuests),
                [102] = addButton(journalButtons.openMasteries),
                [103] = addButton(journalButtons.openRelics),
                [104] = addButton(journalButtons.openAlchemy),
                [105] = addButton(journalButtons.openPotions),
                [106] = addButton(journalButtons.openCompanions),
                [107] = addButton(journalButtons.openDragonomicon),

                [201] = addButton(journalButtons.openEffects),
                [202] = addButton(journalButtons.openLore),

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void pressContent()
        {

            string goodId = contentComponents[focus].id;

            ExportHandle.exports export = Enum.Parse<ExportHandle.exports>(goodId);


        }

        public override void drawHover(SpriteBatch b)
        {

            base.drawHover(b);

            if (!browsing)
            {

                return;

            }

            string goodId = contentComponents[focus].id;

            ExportHandle.exports export = Enum.Parse<ExportHandle.exports>(goodId);

            ExportGood good = Mod.instance.exportHandle.goods[export];

            float contentHeight = 16;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(good.name, Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            contentHeight += 20 + titleSize.Y;

            // -------------------------------------------------------
            // description

            string descriptionText = Game1.parseText(good.description, Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            contentHeight += 12 + descriptionSize.Y;

            // -------------------------------------------------------

            string technicalText = Game1.parseText(good.technical, Game1.smallFont, 476);

            Vector2 technicalSize = Game1.smallFont.MeasureString(technicalText);

            contentHeight += 12 + technicalSize.Y;

            // -------------------------------------------------------
            // details

            if (good.details.Count > 0)
            {

                contentHeight += 12; // bar

                foreach (string detail in good.details)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    contentHeight += detailSize.Y;

                }

                contentHeight += 12;

            }
            // -------------------------------------------------------
            // sell now

            string sellText = String.Empty;

            Vector2 sellSize = Vector2.Zero;

            int quickSell = Mod.instance.exportHandle.QuickSell(export);

            if (quickSell > 0)
            {

                contentHeight += 12; // bar

                sellText = Game1.parseText(StringData.Strings(StringData.stringkeys.sellnow) + quickSell + StringData.currency, Game1.smallFont, 476);

                sellSize = Game1.smallFont.MeasureString(sellText);

                contentHeight += sellSize.Y;

                contentHeight += 12;


            }

            contentHeight += 4; // bottom

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
            // technical

            b.DrawString(Game1.smallFont, technicalText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, technicalText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + technicalSize.Y;

            // -------------------------------------------------------
            // details

            if (good.details.Count > 0)
            {

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

                textPosition += 12;

                foreach (string detail in good.details)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin, textPosition), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += detailSize.Y;

                }

                textPosition += 12;

            }

            // -------------------------------------------------------
            // sell now

            if (good.sell > 0)
            {

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

                textPosition += 12;

                b.DrawString(Game1.smallFont, sellText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, sellText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

                textPosition += 12 + sellSize.Y;

            }

        }

    }

}
