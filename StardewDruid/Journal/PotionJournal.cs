using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.FarmAnimals;
using StardewValley.GameData.Tools;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Menus;
using StardewValley.Monsters;
using StardewValley.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace StardewDruid.Journal
{
    public class PotionJournal : DruidJournal
    {

        public List<ApothecaryHandle.items> apothecaryLayout = new();

        public AlchemyProduct hoverProduct = null;

        public int hoverFocus = -1;

        public int hoverShift = 0;

        public string hoverTitle;

        public string hoverDescription;

        public List<string> hoverDetails = new();

        public int hoverHeight;

        public int hoverSerial = 0;

        public List<Microsoft.Xna.Framework.Rectangle> hoverSource = new();

        public PotionJournal(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {

        }

        public override void prepareContent()
        {

            type = journalTypes.potions;

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
                [107] = addButton(journalButtons.openOrders),
                [108] = addButton(journalButtons.openDragonomicon),

                [201] = addButton(journalButtons.openPowders),
                [202] = addButton(journalButtons.openGoods),
                [203] = addButton(journalButtons.clearBuffs),
                [204] = addButton(journalButtons.refresh),

                [301] = addButton(journalButtons.exit),
                [302] = addButton(journalButtons.HP),
                [303] = addButton(journalButtons.STM),
                [306] = addButton(journalButtons.forward),
                [307] = addButton(journalButtons.end),

            };

        }

        public override void populateContent()
        {

            contentColumns = 20;

            pagination = 0;

            contentComponents = Mod.instance.apothecaryHandle.JournalByType(type);

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            List<Vector2> grid = new();

            int xBase = xPositionOnScreen + 20;

            int xWidth = (width - 32) / 5;

            int xIcon = (xWidth - 8) / 3;

            int yBase = yPositionOnScreen + 16;

            int yHeight= 202;

            for (int j = 0; j < 3; j++)
            {

                for(int k = 0; k < 5; k++)
                {

                    grid.Add(new(xBase + (k * xWidth), yBase + (j * yHeight)));

                }

            }

            for (int i = 0; i < contentComponents.Count; i++)
            {

                ContentComponent comp = contentComponents[i];

                if (!comp.active)
                {

                    continue;

                }

                int gridIndex = comp.grid; // grid spot

                switch (comp.serial)
                {

                    case 0:

                        comp.SetText(xWidth - 8);

                        comp.bounds = new Rectangle((int)grid[gridIndex].X, (int)grid[gridIndex].Y, xWidth - 8, 146);

                        break;

                    case 1:
                    case 2:
                    case 3:

                        comp.bounds = new Rectangle((int)grid[gridIndex].X + (xIcon * (comp.serial - 1)), (int)grid[gridIndex].Y + 146, xIcon, 56);

                        break;

                }

            }

        }

        public override void pressButton(journalButtons button)
        {

            int shiftAmount = ShiftPressed();

            switch (button)
            {
                case journalButtons.clearBuffs:

                    Mod.instance.apothecaryHandle.buff.RemoveBuffs();

                    return;

                case journalButtons.refresh:
                        
                    Mod.instance.apothecaryHandle.MassCraft(apothecaryLayout, shiftAmount);

                    populateContent();

                    activateInterface();

                    return;

                case journalButtons.forward:

                    DruidJournal.openJournal(journalTypes.powders);

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

            int shiftAmount = ShiftPressed();

            bool success = false;

            string sound = "smallSelect";

            ApothecaryHandle.items itemId = Enum.Parse<ApothecaryHandle.items>(contentComponents[focus].id);

            switch (contentComponents[focus].serial)
            {

                case 0: // display

                    success = Mod.instance.apothecaryHandle.ConsumeItem(itemId, shiftAmount);

                    break;

                case 1: // recipes

                    AlchemyRecipe.recipes recipeId = Mod.instance.apothecaryHandle.apothecary[itemId].recipe;

                    AlchemyProduct product = Mod.instance.apothecaryHandle.UseRecipe(recipeId, Game1.player.CurrentToolIndex, shiftAmount);

                    if(product.processed > 0)
                    {

                        success = true;

                    }

                    break;

                case 2: // goods

                    success = Mod.instance.apothecaryHandle.ConvertToGoods(itemId, shiftAmount);

                    sound = SpellHandle.Sounds.Ship.ToString();

                    break;

                case 3: // settings

                    Mod.instance.apothecaryHandle.ShiftBehaviour(itemId);

                    success = true;

                    return;

            }

            if (success)
            {

                Game1.playSound(sound);

                populateContent();

            }
            else
            {

                Game1.playSound(SpellHandle.Sounds.ghost.ToString());

            }

            return;

        }

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

                    case 0:

                        DrawItem(b,comp, (browsing && i == focus));

                        break;

                    case 1:
                    case 2:
                    case 3:

                        comp.draw(b,Vector2.Zero,(browsing && i == focus));

                        break;

                }

            }

        }

        public virtual void DrawItem(SpriteBatch b, ContentComponent comp, bool focus = false)
        {

            IClickableMenu.drawTextureBox(
                b,
                Game1.mouseCursors,
                new Rectangle(384, 396, 15, 15),
                comp.bounds.X,
                comp.bounds.Y,
                comp.bounds.Width,
                comp.bounds.Height,
                focus ? Color.Wheat : Color.White,
                4f,
                false,
                -1f
            );

            b.DrawString(Game1.dialogueFont, comp.textParse[0], new Vector2(comp.bounds.Center.X + 6 - (comp.textMeasures[0].X * 0.9f / 2) - 1f, comp.bounds.Center.Y + 24 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.900f);

            b.DrawString(Game1.dialogueFont, comp.textParse[0], new Vector2(comp.bounds.Center.X + 6 - (comp.textMeasures[0].X * 0.9f / 2), comp.bounds.Center.Y + 24), ContentComponent.defaultColour, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.901f);

            b.Draw(Mod.instance.iconData.itemsTexture, new Vector2(comp.bounds.Center.X + 2f, comp.bounds.Center.Y - 20f + 4f), comp.textureSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, new Vector2(10), 4f, 0, 0.900f);

            b.Draw(Mod.instance.iconData.itemsTexture, new Vector2(comp.bounds.Center.X, comp.bounds.Center.Y - 20f), comp.textureSources[0], Color.White, 0f, new Vector2(10), 4f, 0, 0.901f);

            if (comp.textureSources.Count > 1)
            {

                int glitterTime = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 2000) / 100;

                if (glitterTime <= 7)
                {

                    b.Draw(Mod.instance.iconData.impactsTexture, new Vector2(comp.bounds.Center.X, comp.bounds.Center.Y), new Rectangle(0 + (glitterTime * 64), 128, 64, 64), Color.White * 0.5f, (float)(Math.PI / 2), new Vector2(32), 4f, 0, 0.901f);

                }

            }

        }

        public override void drawHover(SpriteBatch b)
        {

            base.drawHover(b);

            if (!browsing)
            {

                hoverFocus = -1;

                return;

            }

            switch (contentComponents[focus].serial)
            {

                case 0:

                    DrawApothecaryItem(b);

                    break;

                case 1:

                    DrawAlchemyRecipe(b);

                    break;

                case 2:

                    DrawDispatchResource(b);

                    break;

                default:

                    drawHoverText(b, contentComponents[focus].text[0]);

                    break;

            }

        }

        public virtual void ClearHover()
        {

            hoverFocus = focus;

            hoverProduct = null;

            hoverFocus = -1;

            hoverShift = 0;

            hoverTitle = null;

            hoverDescription = null;

            hoverDetails = new();

            hoverHeight = 0;

        }

        public virtual void PrepareApothecaryItem()
        {

            ClearHover();

            ApothecaryHandle.items itemId = Enum.Parse<ApothecaryHandle.items>(contentComponents[focus].id);

            ApothecaryItem itemData = Mod.instance.apothecaryHandle.apothecary[itemId];

            string useTitle = itemData.title;

            int amount = ApothecaryHandle.GetAmount(itemId);

            if (amount > 0)
            {

                useTitle += StringData.ecks + amount.ToString();

            }

            string useDescription = itemData.description;

            List<string> details = new(itemData.details);

            if (itemData.stamina > 0)
            {

                float difficulty = Mod.instance.masteryHandle.ReplenishmentFactor();

                int staminaGain = (int)(itemData.stamina * difficulty);

                int healthGain = (int)(itemData.health * difficulty);

                string staminaReadout = Mod.instance.Helper.Translation.Get("HerbalData.327.1").Tokens(new { stamina = staminaGain.ToString(), health = healthGain.ToString() });

                details.Add(staminaReadout);

            }

            if(itemData.buff != BuffHandle.buffTypes.none)
            {

                BuffDetail buffData = Mod.instance.apothecaryHandle.buff.details[itemData.buff];

                string buffString = buffData.name; 

                if(itemData.level > 1)
                {

                    buffString += " " + StringData.Get(StringData.str.level, new { level = itemData.level });
                }

                details.Add(buffString);

                details.Add(buffData.description);

                string applyString = StringData.Get(StringData.str.applyBuff);

                if (Mod.instance.apothecaryHandle.buff.applied.ContainsKey(itemData.buff))
                {

                    if(Mod.instance.apothecaryHandle.buff.applied[itemData.buff].level < itemData.level)
                    {

                        applyString = StringData.Get(StringData.str.activeBuff);

                    }

                }

                details.Add(applyString);


            }

            hoverHeight = 0;

            // -------------------------------------------------------
            // title

            hoverHeight += 4;

            hoverTitle = Game1.parseText(useTitle, Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(hoverTitle);

            hoverHeight += (int)(titleSize.Y * 0.8f);

            hoverHeight += 8;

            // -------------------------------------------------------
            // description

            hoverHeight += 12;

            hoverDescription = Game1.parseText(useDescription, Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(hoverDescription);

            hoverHeight += (int)descriptionSize.Y;

            hoverHeight += 12;

            // -------------------------------------------------------
            // details

            if (details.Count > 0)
            {

                hoverHeight += 12;

                foreach (string detail in details)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    hoverDetails.Add(detailText);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    hoverHeight += (int)detailSize.Y;

                }

                hoverHeight += 12;

            }

            hoverHeight += 24;

        }

        public virtual void DrawApothecaryItem(SpriteBatch b)
        {

            if(hoverFocus != focus)
            {

                PrepareApothecaryItem();

            }

            // -------------------------------------------------------
            // texturebox

            Rectangle boxBounds = drawHoverBox(b, 512, hoverHeight);

            float textPosition = boxBounds.Y + 16;

            float textMargin = boxBounds.X + 16;

            // -------------------------------------------------------
            // title

            textPosition += 4;

            b.DrawString(Game1.dialogueFont, hoverTitle, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, -1f);

            b.DrawString(Game1.dialogueFont, hoverTitle, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, -1.1f);

            textPosition += 8 + Game1.dialogueFont.MeasureString(hoverTitle).Y * 0.8f;

            // -------------------------------------------------------
            // description

            DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

            textPosition += 12;

            b.DrawString(Game1.smallFont, hoverDescription, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverDescription, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += Game1.smallFont.MeasureString(hoverDescription).Y;

            textPosition += 12;

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

        public virtual void PrepareAlchemyRecipe()
        {

            ClearHover();

            hoverShift = ShiftPressed();

            ApothecaryHandle.items itemId = Enum.Parse<ApothecaryHandle.items>(contentComponents[focus].id);

            ApothecaryItem itemData = Mod.instance.apothecaryHandle.apothecary[itemId];

            AlchemyRecipe recipeData = Mod.instance.apothecaryHandle.recipes[itemData.recipe];

            AlchemyProduct alchemyProduct = Mod.instance.apothecaryHandle.GetProduct(recipeData.type, Game1.player.CurrentToolIndex, hoverShift);

            hoverHeight = 0;

            // -------------------------------------------------------
            // description

            hoverHeight += 8;

            string useTitle = recipeData.name;

            hoverTitle = Game1.parseText(useTitle, Game1.smallFont, 476);

            Vector2 titleSize = Game1.smallFont.MeasureString(hoverTitle);

            hoverHeight += (int)titleSize.Y;

            hoverHeight += 8;

            // -------------------------------------------------------
            // product

            hoverHeight += 12; // bar

            hoverProduct = alchemyProduct;

            hoverHeight += 40;

            // -------------------------------------------------------
            // required

            hoverHeight += 12; // bar

            string useDescription = recipeData.instruction;

            List<string> details = new();

            hoverDescription = Game1.parseText(useDescription, Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(hoverDescription);

            hoverHeight += (int)descriptionSize.Y;

            // -------------------------------------------------------
            // potions

            hoverHeight += 12; // bar

            if (alchemyProduct.totalApothecary.Count > 0)
            {

                hoverHeight += (32 * alchemyProduct.totalApothecary.Count);

                hoverHeight += 24;

            }

            // -------------------------------------------------------
            // items

            if (alchemyProduct.totalInventory.Count > 0)
            {

                hoverHeight += (32 * alchemyProduct.totalInventory.Count);

                hoverHeight += 24;

            }
            // -------------------------------------------------------
            // items (slot)

            if (alchemyProduct.totalSlot.Count > 0)
            {

                hoverHeight += (32 * alchemyProduct.totalSlot.Count);

                hoverHeight += 24;

            }

            hoverHeight += 24;

        }

        public void DrawAlchemyRecipe(SpriteBatch b)
        {

            if (hoverFocus != focus || hoverShift != ShiftPressed())
            {

                PrepareAlchemyRecipe();

            }

            // -------------------------------------------------------
            // texturebox

            Rectangle boxBounds = drawHoverBox(b, 512, hoverHeight);

            float textPosition = boxBounds.Y + 16;

            float textMargin = boxBounds.X + 16;

            // -------------------------------------------------------
            // title

            textPosition += 8;

            b.DrawString(Game1.smallFont, hoverTitle, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverTitle, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += Game1.smallFont.MeasureString(hoverTitle).Y;

            textPosition += 8;

            // -------------------------------------------------------
            // product

            DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

            textPosition += 12;

            string productAmount = "(" + hoverProduct.processed + ")";

            if (hoverProduct.apothecaryItem != ApothecaryHandle.items.none)
            {

                ApothecaryItem apothecaryProduct = Mod.instance.apothecaryHandle.apothecary[hoverProduct.apothecaryItem];

                b.Draw(Mod.instance.iconData.itemsTexture, new Vector2(textMargin, textPosition), ApothecaryHandle.ItemRectangles(apothecaryProduct.item), Color.White, 0f, Vector2.Zero, 1.5f, 0, 0.901f);

                b.DrawString(Game1.smallFont, apothecaryProduct.title, new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, apothecaryProduct.title, new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                Vector2 displaySize = Game1.smallFont.MeasureString(productAmount);

                b.DrawString(Game1.smallFont, productAmount, new Vector2(textMargin + 476 - displaySize.X, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, productAmount, new Vector2(textMargin + 476 - displaySize.X - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                textPosition += 40;

            }

            if (hoverProduct.inventoryItem != null)
            {

                ParsedItemData inventoryProduct = ItemRegistry.GetDataOrErrorItem(hoverProduct.inventoryItem.QualifiedItemId);

                Rectangle itemSource = inventoryProduct.GetSourceRect();

                if (itemSource.Height > 16)
                {

                    itemSource.Height = 16; itemSource.Y += 16;

                }

                b.Draw(inventoryProduct.GetTexture(), new Vector2(textMargin, textPosition), itemSource, Color.White, 0f, Vector2.Zero, 2f, 0, 1f);

                if (hoverProduct.inventoryItem.Stack > 1)
                {
                    hoverProduct.inventoryItem.DrawMenuIcons(b, new Vector2(textMargin, textPosition), 0.5f, 1f, 1f + 1.2E-05f, StackDrawType.Draw, Color.White);
                }
                else if (hoverProduct.inventoryItem.Quality > 0)
                {
                    hoverProduct.inventoryItem.DrawMenuIcons(b, new Vector2(textMargin, textPosition), 0.5f, 1f, 1f + 1.2E-05f, StackDrawType.HideButShowQuality, Color.White);
                }

                b.DrawString(Game1.smallFont, inventoryProduct.DisplayName, new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, inventoryProduct.DisplayName, new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                Vector2 displaySize = Game1.smallFont.MeasureString(productAmount);

                b.DrawString(Game1.smallFont, productAmount, new Vector2(textMargin + 476 - displaySize.X, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, productAmount, new Vector2(textMargin + 476 - displaySize.X - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                textPosition += 40;

            }

            // -------------------------------------------------------
            // required

            DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

            textPosition += 12;

            b.DrawString(Game1.smallFont, hoverDescription, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverDescription, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += Game1.smallFont.MeasureString(hoverDescription).Y;

            textPosition += 12;

            // ---------------------------------------------------------
            // potions

            if (hoverProduct.totalApothecary.Count > 0)
            {

                DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

                textPosition += 12;

                foreach (KeyValuePair<ApothecaryHandle.items, int> item in hoverProduct.totalApothecary)
                {

                    ApothecaryItem basePotion = Mod.instance.apothecaryHandle.apothecary[item.Key];

                    b.Draw(Mod.instance.iconData.itemsTexture, new Vector2(textMargin, textPosition), ApothecaryHandle.ItemRectangles(basePotion.item), Color.White, 0f, Vector2.Zero, 1.5f, 0, 0.901f);

                    b.DrawString(Game1.smallFont, basePotion.title, new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, basePotion.title, new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);


                    string displayAmount = "";

                    if (hoverProduct.fromApothecary.ContainsKey(item.Key))
                    {

                        displayAmount += hoverProduct.fromApothecary[item.Key] + " ";

                    }

                    displayAmount += "(" + item.Value + ")";

                    Vector2 displaySize = Game1.smallFont.MeasureString(displayAmount);

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - displaySize.X, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - displaySize.X - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += 32;

                }

                textPosition += 12;

            }

            // ---------------------------------------------------------
            // items

            if (hoverProduct.totalInventory.Count > 0)
            {

                DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

                textPosition += 12;

                foreach (KeyValuePair<string, int> item in hoverProduct.totalInventory)
                {

                    ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(item.Key);

                    Rectangle itemSource = dataOrErrorItem.GetSourceRect();

                    if (itemSource.Height > 16)
                    {

                        itemSource.Height = 16; itemSource.Y += 16;

                    }

                    b.Draw(dataOrErrorItem.GetTexture(), new Vector2(textMargin, textPosition), itemSource, Color.White, 0f, Vector2.Zero, 2f, 0, 1f);

                    b.DrawString(Game1.smallFont, dataOrErrorItem.DisplayName, new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, dataOrErrorItem.DisplayName, new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    string displayAmount = "";

                    if (hoverProduct.fromInventory.ContainsKey(item.Key))
                    {

                        displayAmount += hoverProduct.fromInventory[item.Key] + " ";

                    }

                    displayAmount += "(" + item.Value + ")";

                    Vector2 displaySize = Game1.smallFont.MeasureString(displayAmount);

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - displaySize.X, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - displaySize.X - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += 32;

                }

            }

            // ---------------------------------------------------------
            // items (slot)

            if (hoverProduct.totalSlot.Count > 0)
            {

                DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

                textPosition += 12;

                foreach (KeyValuePair<int, int> item in hoverProduct.totalSlot)
                {

                    ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(Game1.player.Items[item.Key].QualifiedItemId);

                    Rectangle itemSource = dataOrErrorItem.GetSourceRect();

                    if (itemSource.Height > 16)
                    {

                        itemSource.Height = 16; itemSource.Y += 16;

                    }

                    b.Draw(dataOrErrorItem.GetTexture(), new Vector2(textMargin, textPosition), itemSource, Color.White, 0f, Vector2.Zero, 2f, 0, 1f);

                    if (Game1.player.Items[item.Key].Stack > 1)
                    {
                        Game1.player.Items[item.Key].DrawMenuIcons(b, new Vector2(textMargin, textPosition), 0.5f, 1f, 1f + 1.2E-05f, StackDrawType.Draw, Color.White);
                    }
                    else if (Game1.player.Items[item.Key].Quality > 0)
                    {
                        Game1.player.Items[item.Key].DrawMenuIcons(b, new Vector2(textMargin, textPosition), 0.5f, 1f, 1f + 1.2E-05f, StackDrawType.HideButShowQuality, Color.White);
                    }

                    b.DrawString(Game1.smallFont, dataOrErrorItem.DisplayName, new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, dataOrErrorItem.DisplayName, new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    string displayAmount = "";

                    if (hoverProduct.fromSlot.ContainsKey(item.Key))
                    {

                        displayAmount += hoverProduct.fromSlot[item.Key] + " ";

                    }

                    displayAmount += "(" + item.Value + ")";

                    Vector2 displaySize = Game1.smallFont.MeasureString(displayAmount);

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - displaySize.X, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - displaySize.X - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += 32;

                }

            }

        }

        public virtual void PrepareDispatchResource()
        {

            ClearHover();

            hoverShift = ShiftPressed();

            ApothecaryHandle.items itemId = Enum.Parse<ApothecaryHandle.items>(contentComponents[focus].id);

            ApothecaryItem itemData = Mod.instance.apothecaryHandle.apothecary[itemId];

            hoverHeight = 0;

            // -------------------------------------------------------
            // description

            hoverHeight += 8;

            string useTitle = StringData.Get(StringData.str.sendToGoods);

            hoverTitle = Game1.parseText(useTitle, Game1.smallFont, 476);

            Vector2 titleSize = Game1.smallFont.MeasureString(hoverTitle);

            hoverHeight += (int)titleSize.Y;

            hoverHeight += 8;

            // -------------------------------------------------------
            // separator

            hoverHeight += 12;

            // -------------------------------------------------------
            // product

            hoverSource = new();

            hoverSerial = itemData.convert;

            switch (itemData.convert)
            {

                case 1:

                    ExportResource resourceData = Mod.instance.exportHandle.resources[itemData.resource];

                    Microsoft.Xna.Framework.Rectangle resourceSource = IconData.DisplayRectangle(ExportResource.ResourceDisplay(itemData.resource));

                    hoverSource.Add(resourceSource);

                    hoverDetails.Add(resourceData.name);

                    break;

                case 2:

                    ExportGood goodData = Mod.instance.exportHandle.goods[itemData.export];

                    Microsoft.Xna.Framework.Rectangle goodSource = ExportGood.GoodRectangles(itemData.export);

                    hoverSource.Add(goodSource);

                    hoverDetails.Add(goodData.name);

                    break;

            }


            int amount = itemData.units * ApothecaryHandle.GetAmount(itemId);

            int toSend = Math.Min(amount, itemData.units * hoverShift);

            hoverDetails.Add(toSend + " (" + amount + ")");

            hoverHeight += 72;

        }

        public void DrawDispatchResource(SpriteBatch b)
        {

            if (hoverFocus != focus || hoverShift != ShiftPressed())
            {

                PrepareDispatchResource();

            }

            // -------------------------------------------------------
            // texturebox

            Rectangle boxBounds = drawHoverBox(b, 512, hoverHeight);

            float textPosition = boxBounds.Y + 16;

            float textMargin = boxBounds.X + 16;

            // -------------------------------------------------------
            // product

            textPosition += 8;

            b.DrawString(Game1.smallFont, hoverTitle, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverTitle, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += Game1.smallFont.MeasureString(hoverTitle).Y;

            textPosition += 8;

            // -------------------------------------------------------
            // description

            DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

            textPosition += 12;

            switch (hoverSerial)
            {

                case 1:

                    b.Draw(Mod.instance.iconData.displayTexture, new Vector2(textMargin, textPosition), hoverSource[0], Color.White, 0f, Vector2.Zero, 3f, 0, 0.901f);

                    break;

                case 2:

                    b.Draw(Mod.instance.iconData.exportTexture, new Vector2(textMargin, textPosition - 8), hoverSource[0], Color.White, 0f, Vector2.Zero, 1.5f, 0, 0.901f);

                    break;

            }

            b.DrawString(Game1.smallFont, hoverDetails[0], new Vector2(textMargin + 60, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverDetails[0], new Vector2(textMargin + 60 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

            Vector2 displaySize = Game1.smallFont.MeasureString(hoverDetails[1]);

            b.DrawString(Game1.smallFont, hoverDetails[1], new Vector2(textMargin + 476 - displaySize.X, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverDetails[1], new Vector2(textMargin + 476 - displaySize.X - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);


        }

    }

}
