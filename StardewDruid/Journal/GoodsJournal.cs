using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Minigames;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class GoodsJournal : DruidJournal
    {

        public int hoverFocus = -1;

        public int hoverHeight = 0;

        public string hoverTitle;

        public float hoverTitleX = 0f;

        public float hoverTitleY = 0f;

        public string hoverDescription;

        public float hoverDescriptionSize = 0f;

        public List<string> hoverDetails = new();

        public string hoverAction;

        public float hoverActionSize = 0f;

        public int hoverSell;

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> hoverSources = new();

        public GoodsJournal(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {


        }
        public override void prepareContent()
        {

            type = journalTypes.goods;

            if (record == 1)
            {

                parent = journalTypes.orders;

            }
            else
            {

                parent = journalTypes.potions;

            }

        }

        public override void populateContent()
        {

            pagination = 0;

            contentComponents = Mod.instance.exportHandle.JournalByType(type);

            int compWidth = ((width - 32) / 4);

            for (int i = 0; i < contentComponents.Count; i++) {

                ContentComponent comp = contentComponents[i];

                comp.SetText(compWidth);

                int compGridX = comp.grid % 4;

                int compGridY = comp.grid / 4;

                switch (comp.serial)
                {

                    case 1:

                        comp.bounds = new Rectangle(xPositionOnScreen + 16 + 4 + (compGridX * compWidth), yPositionOnScreen + 24 + (int)(compGridY * (192 + 56)), compWidth - 8, 192);

                        break;

                    case 2:

                        comp.bounds = new Rectangle(xPositionOnScreen + 16 + 4 + (compGridX * compWidth), yPositionOnScreen + 24 + 192 + (int)(compGridY * (192 + 56)), (compWidth - 8) / 3, 56);

                        break;

                    case 3:

                        comp.bounds = new Rectangle(xPositionOnScreen + 16 + 4 + (compGridX * compWidth), yPositionOnScreen + 24 + 8 + (int)(compGridY * (192 + 56)), compWidth - 8, 80);

                        break;

                }

            }

        }

        public override void populateInterface()
        {

            if(record == 1)
            {

                interfaceComponents = new()
                {

                    [101] = addButton(journalButtons.openOrders),

                    [201] = addButton(journalButtons.previous),

                    [202] = addButton(journalButtons.openGuilds),
                    [203] = addButton(journalButtons.openGoodsDistillery),
                    [204] = addButton(journalButtons.openDistillery),
                    [205] = addButton(journalButtons.openProductionRecent),
                    [206] = addButton(journalButtons.openProductionEstimated),

                    [207] = addButton(journalButtons.refresh),

                    [301] = addButton(journalButtons.exit),

                };

                return;

            }

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openQuests),
                [102] = addButton(journalButtons.openMasteries),
                [103] = addButton(journalButtons.openRelics),
                [104] = addButton(journalButtons.openAlchemy),
                [105] = addButton(journalButtons.openPotions),
                [106] = addButton(journalButtons.openCompanions),
                [107] = addButton(journalButtons.openOrders),
                [108] = addButton(journalButtons.openDragonomicon),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),
                [203] = addButton(journalButtons.openPowders),

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.start:

                    DruidJournal.openJournal(journalTypes.potions);

                    return;

                case journalButtons.back:

                    DruidJournal.openJournal(journalTypes.powders);

                    return;

                default:

                    base.pressButton(button);

                    break;

            }
        }

        public override void pressContent()
        {

            if(contentComponents[focus].serial != 2)
            {

                return;

            }

            string goodId = contentComponents[focus].id;

            ExportGood.goods export = Enum.Parse<ExportGood.goods>(goodId);

            if (Mod.instance.exportHandle.QuickSell(export) > 0)
            {

                Mod.instance.exportHandle.SellNow(export, ShiftPressed());

                Game1.playSound(SpellHandle.Sounds.Ship.ToString());

                prepareContent();

                populateContent();

            }
            else
            {

                Game1.playSound(SpellHandle.Sounds.ghost.ToString());

            }

        }

        // =============================================================================

        public override void drawContent(SpriteBatch b)
        {

            for (int i = 0; i < contentComponents.Count; i++)
            {

                ContentComponent comp = contentComponents[i];
 
                if (!comp.active)
                {

                    continue;

                }

                switch (comp.serial)
                {

                    case 1:

                        DrawExport(b, comp, (browsing && i == focus));

                        break;

                    case 2: // sell now

                        comp.draw(b, Vector2.Zero, (browsing && i == focus));
                            
                        break;

                    case 3:

                        DrawResource(b, comp, (browsing && i == focus));

                        break;

                }

                //b.Draw(Game1.staminaRect, new Vector2(comp.bounds.X, comp.bounds.Y), new Rectangle(0, 0, comp.bounds.Width, comp.bounds.Height), Color.Blue, 0f, Vector2.Zero, 1f, 0, 0.0001f);

            }

        }

        public virtual void DrawExport(SpriteBatch b, ContentComponent comp, bool infocus)
        {

            IClickableMenu.drawTextureBox(
                b,
                Game1.mouseCursors,
                new Rectangle(384, 396, 15, 15),
                comp.bounds.X,
                comp.bounds.Y,
                comp.bounds.Width,
                comp.bounds.Height,
                infocus ? Color.Wheat : Color.White,
                4f,
                false,
                -1f
            );

            float textWidth = (comp.textMeasures[0].X / 2 ) * 0.9f;

            b.DrawString(Game1.dialogueFont, comp.textParse[0], new Vector2(comp.bounds.Center.X + 6 - textWidth - 1f, comp.bounds.Bottom  - 52 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.900f);

            b.DrawString(Game1.dialogueFont, comp.textParse[0], new Vector2(comp.bounds.Center.X + 6 - textWidth, comp.bounds.Bottom  - 52), comp.textColours[0], 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.901f);

            b.Draw(Mod.instance.iconData.exportTexture, new Vector2(comp.bounds.Center.X + 2f, comp.bounds.Center.Y  - 20 + 4f), comp.textureSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, new Vector2(16), 4f, 0, 0.900f);

            b.Draw(Mod.instance.iconData.exportTexture, new Vector2(comp.bounds.Center.X, comp.bounds.Center.Y  - 20), comp.textureSources[0], Color.White, 0f, new Vector2(16), 4f, 0, 0.901f);

        }

        public virtual void DrawResource(SpriteBatch b, ContentComponent comp, bool infocus)
        {

            IClickableMenu.drawTextureBox(
                b,
                Game1.mouseCursors,
                new Rectangle(384, 396, 15, 15),
                comp.bounds.X,
                comp.bounds.Y,
                comp.bounds.Width,
                comp.bounds.Height,
                infocus ? Color.Wheat : Color.White,
                3f,
                false,
                -1f
            );

            float textMargin = comp.bounds.X + 16;
            
            float textPosition = comp.bounds.Center.Y - (comp.textMeasures[0].Y/2);

            float textMarginRight = comp.bounds.Right - 16;

            b.Draw(Mod.instance.iconData.displayTexture, new Vector2(textMargin, textPosition) + new Vector2(-1f, 1f), comp.textureSources[0], Color.Black * 0.35f, 0f, Vector2.Zero, 3f, 0, 0.900f);

            b.Draw(Mod.instance.iconData.displayTexture, new Vector2(textMargin, textPosition), comp.textureSources[0], Color.White, 0f, Vector2.Zero, 3f, 0, 0.901f);

            b.DrawString(Game1.dialogueFont, comp.textParse[0], new Vector2(textMarginRight - (comp.textMeasures[0].X), textPosition ), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

            b.DrawString(Game1.dialogueFont, comp.textParse[0], new Vector2(textMarginRight - (comp.textMeasures[0].X) - 1f, textPosition + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);


        }

        public override void drawHover(SpriteBatch b)
        {

            if (interfacing)
            {

                if (interfaceComponents[focus].text != null)
                {

                    drawHoverText(b, interfaceComponents[focus].text);

                }

                return;

            }

            if (!browsing)
            {

                hoverFocus = -1;

                return;

            }

            switch (contentComponents[focus].serial)
            {

                case 1:

                    DrawExportHover(b);

                    break;

                case 2:

                    DrawSellHover(b);

                    break;

                case 3:

                    DrawResourceHover(b);

                    break;

            }

        }

        public virtual void HoverReset()
        {

            hoverFocus = focus;

            hoverHeight = 0;

            hoverTitle = null;

            hoverTitleY = 0f;

            hoverDescription = null;

            hoverDescriptionSize = 0f;

            hoverDetails = new();

            hoverAction = null;

            hoverActionSize = 0f;

            hoverSell = 0;

        }

        public virtual void PrepareExportHover()
        {

            HoverReset();

            string goodId = contentComponents[focus].id;

            ExportGood.goods export = Enum.Parse<ExportGood.goods>(goodId);

            ExportGood good = Mod.instance.exportHandle.goods[export];

            float contentHeight = 20;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(good.name, Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            hoverTitle = titleText;

            hoverTitleY = titleSize.Y;

            contentHeight += titleSize.Y;

            contentHeight += 4;

            // -------------------------------------------------------
            // description

            string descriptionText = Game1.parseText(good.description, Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            hoverDescription = descriptionText;

            hoverDescriptionSize = descriptionSize.Y;

            contentHeight += 12 + descriptionSize.Y;

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

            }

            hoverDetails = new(good.details);

            // -------------------------------------------------------
            // sell details

            int currentPrice = Mod.instance.exportHandle.GoodsPrice(export);

            string sellText = StringData.Get(StringData.str.currentPrice) + StringData.colon + currentPrice + " (" + good.price + ")";

            string sellParse = Game1.parseText( sellText, Game1.smallFont, 476);

            Vector2 sellSize = Game1.smallFont.MeasureString(sellParse);

            contentHeight += sellSize.Y;

            hoverDetails.Add(sellParse);

            contentHeight += 12; // detail clearance

            // -------------------------------------------------------

            contentHeight += 20; // bottom

            hoverHeight = (int)contentHeight;

        }

        public virtual void DrawExportHover(SpriteBatch b)
        {

            if (hoverFocus != focus)
            {

                PrepareExportHover();

            }

            // -------------------------------------------------------
            // texturebox

            Rectangle boxBounds = drawHoverBox(b, 512, hoverHeight);

            float textPosition = boxBounds.Y + 20;

            float textMargin = boxBounds.X + 16;

            // -------------------------------------------------------
            // title

            b.DrawString(Game1.dialogueFont, hoverTitle, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.dialogueFont, hoverTitle, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 4 + hoverTitleY;

            // --------------------------------
            // separator

            DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

            textPosition += 12;

            // -------------------------------------------------------
            // description

            b.DrawString(Game1.smallFont, hoverDescription, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverDescription, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + hoverDescriptionSize;

            // -------------------------------------------------------
            // details

            if (hoverDetails.Count > 0)
            {

                DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

                textPosition += 12;

                foreach (string detail in hoverDetails)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin, textPosition), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += detailSize.Y;

                }

            }

        }

        public virtual void PrepareSellHover()
        {

            HoverReset();

            string goodId = contentComponents[focus].id;

            ExportGood.goods export = Enum.Parse<ExportGood.goods>(goodId);

            float contentHeight = 16;

            // -------------------------------------------------------
            // title

            int sellPrice = Mod.instance.exportHandle.QuickSell(export);

            string titleText = "";
            
            if(sellPrice > 0)
            {
                
                titleText = StringData.Get(StringData.str.sellnow) + sellPrice + StringData.currency;

            }
            else
            {

                titleText = StringData.Get(StringData.str.nosell);

            }

            string titleParse = Game1.parseText(titleText, Game1.smallFont, 476);

            Vector2 titleSize = Game1.smallFont.MeasureString(titleParse);

            hoverTitle = titleParse;

            hoverTitleX = titleSize.X;

            hoverTitleY = titleSize.Y;

            contentHeight += titleSize.Y;

            contentHeight += 16;

            hoverHeight = (int)contentHeight;

        }

        public virtual void DrawSellHover(SpriteBatch b)
        {

            // -------------------------------------------------------
            // sell button

            if (hoverFocus != focus)
            {

                PrepareSellHover();

            }

            // -------------------------------------------------------
            // texturebox

            Rectangle boxBounds = drawHoverBox(b, (int)hoverTitleX + 48, hoverHeight, 3f);

            float textPosition = boxBounds.Y + 16;

            float textMargin = boxBounds.X + 16;

            // -------------------------------------------------------
            // title

            b.DrawString(Game1.smallFont, hoverTitle, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverTitle, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 8 + hoverTitleY;

        }

        public virtual void PrepareResourceHover()
        {

            HoverReset();

            string resourceId = contentComponents[focus].id;

            ExportResource.resources export = Enum.Parse<ExportResource.resources>(resourceId);

            ExportResource resource = Mod.instance.exportHandle.resources[export];

            float contentHeight = 20;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(resource.name, Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            hoverTitle = titleText;

            hoverTitleY = titleSize.Y;

            contentHeight += 4 + titleSize.Y;

            // -------------------------------------------------------
            // description

            string descriptionText = Game1.parseText(resource.description, Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            hoverDescription = descriptionText;

            hoverDescriptionSize = descriptionSize.Y;

            contentHeight += 12 + descriptionSize.Y;

            // -------------------------------------------------------
            // details

            if (resource.details.Count > 0)
            {

                contentHeight += 12; // bar

                foreach (string detail in resource.details)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    contentHeight += detailSize.Y;

                }

            }

            hoverDetails = new(resource.details);

            contentHeight += 12;
            // -------------------------------------------------------

            contentHeight += 20; // bottom

            hoverHeight = (int)contentHeight;

        }

        public virtual void DrawResourceHover(SpriteBatch b)
        {

            // -------------------------------------------------------
            // resource

            if (hoverFocus != focus)
            {

                PrepareResourceHover();

            }

            DrawExportHover(b);

        }


    }

}
