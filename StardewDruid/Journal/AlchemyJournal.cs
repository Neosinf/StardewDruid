using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Menus;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static StardewDruid.Handle.ApothecaryHandle;

namespace StardewDruid.Journal
{

    public class AlchemyJournal : DruidJournal
    {

        // --------------------------------------

        public int useSlot = -1;

        public ApothecaryHandle.items useItem = ApothecaryHandle.items.none;

        public Dictionary<AlchemyProcess.processes, AlchemyRecipe.recipes> useRecipes = new();

        public Dictionary<AlchemyProcess.processes, AlchemyProduct> useProducts = new();

        // --------------------------------------

        public AlchemyProduct hoverProduct = null;

        public int hoverFocus = -1;

        public int hoverShift = 0;

        public string hoverTitle;

        public string hoverDescription;

        public List<string> hoverDetails = new();

        public int hoverSlot;

        public ApothecaryHandle.items hoverItem = ApothecaryHandle.items.none;

        public int hoverItemAmount = 0;

        public int hoverHeight;

        public int hoverSerial = 0;

        public AlchemyJournal(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {


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

                [201] = addButton(journalButtons.openOmens),
                [202] = addButton(journalButtons.openTrophies),

                [301] = addButton(journalButtons.exit),
                [302] = addButton(journalButtons.forward),
                [303] = addButton(journalButtons.end),

            };

        }

        public override void prepareContent()
        {

            useSlot = Game1.player.CurrentToolIndex;

            //useSlot = -1;

            useItem = ApothecaryHandle.items.none;

            useRecipes = new();

            useProducts = new();

            if (parameters.Count > 0)
            {

                useSlot = Convert.ToInt32(parameters[0]);

            }

            if (parameters.Count > 1)
            {

                useItem = Enum.Parse<ApothecaryHandle.items>(parameters[1]);

            }

            useRecipes = ApothecaryHandle.FindRecipe(useSlot, useItem);

            foreach(KeyValuePair<AlchemyProcess.processes, AlchemyRecipe.recipes> recipeEntry in useRecipes)
            {

                useProducts[recipeEntry.Key] = Mod.instance.apothecaryHandle.GetProduct(recipeEntry.Value, useSlot);

            }

        }

        public override void populateContent()
        {

            type = journalTypes.alchemy;

            contentColumns = 8;

            int start = 0;

            Rectangle contentWindow = new(xPositionOnScreen + 16, yPositionOnScreen + 20, width - 32, height - 40);

            int contentHeight = (int)contentWindow.Y;

            // -----------------------------------------------

            List<int> rows = new()
            {
                2,
                3,
                3,
                2,
            };

            Dictionary<int, List<AlchemyProcess.processes>> entries = new()
            {

                [0] = new()
                    {
                        AlchemyProcess.processes.winds,
                        AlchemyProcess.processes.weald,
                        AlchemyProcess.processes.mists,
                        AlchemyProcess.processes.stars,
                    },
                [1] = new()
                    {
                        AlchemyProcess.processes.material,
                        AlchemyProcess.processes.transmute,
                        AlchemyProcess.processes.reagent,
                    },
                [2] = new()
                    {
                        AlchemyProcess.processes.sol,
                        AlchemyProcess.processes.separate,
                        AlchemyProcess.processes.enchant,
                        AlchemyProcess.processes.lune,
                    },
                [3] = new()
                    {
                        AlchemyProcess.processes.voide,
                        AlchemyProcess.processes.fates,
                        AlchemyProcess.processes.witch,
                        AlchemyProcess.processes.ether,
                    },
            };

            foreach(KeyValuePair < int, List<AlchemyProcess.processes>> processList in entries)
            {

                int entryWidth = (int)(contentWindow.Width / processList.Value.Count);

                int entryHeight = (int)(contentWindow.Height * rows[processList.Key] / 10);

                Vector2 entryStart = new Vector2(contentWindow.X, contentHeight);

                for (int i = 0; i < processList.Value.Count; i++)
                {

                    ContentComponent top = new(ContentComponent.contentTypes.text, processList.Value[i].ToString());

                    top.serial = 1;

                    top.text[0] = Mod.instance.apothecaryHandle.processes[processList.Value[i]].name;

                    top.textFonts[0] = 2;

                    top.text[1] = Mod.instance.apothecaryHandle.processes[processList.Value[i]].description;

                    top.textFonts[1] = 2;

                    top.SetText(entryWidth);

                    top.bounds = new((int)entryStart.X, (int)entryStart.Y, entryWidth, entryHeight);

                    contentComponents[start++] = top;

                    entryStart.X += entryWidth;

                }

                for (int i = 0; i < (4 - processList.Value.Count); i++)
                {

                    ContentComponent format = new(ContentComponent.contentTypes.text, i.ToString(), false);

                    contentComponents[start++] = format;

                }

                contentHeight += entryHeight;

            }

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.forward:

                    DruidJournal.openJournal(journalTypes.omens);

                    return;

                case journalButtons.end:

                    DruidJournal.openJournal(journalTypes.trophies);

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

        public override void pressContent()
        {

        }

        public override void drawContent(SpriteBatch b)
        {

            Vector2 displayLayout = new Vector2(8);

            for (int i = 0; i < contentComponents.Count; i++)
            {

                if (!contentComponents[i].active)
                {

                    continue;

                }

                DrawProcess(b, i);

            }

        }

        public virtual void DrawProcess(SpriteBatch b, int i)
        {

            ContentComponent comp = contentComponents[i];

            IClickableMenu.drawTextureBox(
                b,
                Game1.mouseCursors,
                new Rectangle(384, 396, 15, 15),
                (int)comp.bounds.X,
                (int)comp.bounds.Y,
                comp.bounds.Width,
                comp.bounds.Height,
                (browsing && focus == i) ? Color.Wheat : Color.White,
                4f,
                false,
                -1f
            );

            int offset2 = (int)(comp.textMeasures[0].X / 2);

            Vector2 position2 = new Vector2(comp.bounds.X + (comp.bounds.Width / 2) - offset2, comp.bounds.Bottom - 64);

            b.DrawString(Game1.smallFont, comp.textParse[0], position2 + new Vector2(-1, 1), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

            b.DrawString(Game1.smallFont, comp.textParse[0], position2, ContentComponent.defaultColour, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

            AlchemyProcess.processes process = Enum.Parse<AlchemyProcess.processes>(comp.id);

            StardewValley.Item displayMaterial = null;

            ApothecaryHandle.items displayReagent = ApothecaryHandle.items.none;

            switch (process)
            {

                case AlchemyProcess.processes.material:

                    if(useSlot == -1)
                    {

                        break;

                    }

                    if (Game1.player.Items[useSlot] == null)
                    {

                        break;

                    }

                    displayMaterial = Game1.player.Items[useSlot];

                    break;

                case AlchemyProcess.processes.reagent:

                    if (ApothecaryHandle.GetAmount(useItem) <= 0)
                    {

                        break;

                    }

                    displayReagent = useItem;

                    break;

                case AlchemyProcess.processes.transmute:
                case AlchemyProcess.processes.separate:
                case AlchemyProcess.processes.enchant:

                    if (!useProducts.ContainsKey(process))
                    {

                        break;

                    }

                    AlchemyProduct displayProduct = useProducts[process];

                    displayReagent = displayProduct.apothecaryItem;

                    displayMaterial = displayProduct.inventoryItem;

                    break;

            }


            if(displayMaterial != null)
            {

                ParsedItemData materialData = ItemRegistry.GetDataOrErrorItem(displayMaterial.QualifiedItemId);

                Rectangle materialSource = materialData.GetSourceRect();

                Vector2 materialPosition = comp.bounds.Center.ToVector2() - new Vector2(materialSource.Width * 2, materialSource.Height * 2);

                b.Draw(materialData.GetTexture(), materialPosition, materialSource, Color.White, 0, Vector2.Zero, 4f, 0, 1f);

                if (displayMaterial.Stack > 1)
                {
                    displayMaterial.DrawMenuIcons(b, materialPosition, 1f, 1f, 1f + 1.2E-05f, StackDrawType.Draw, Color.White);
                }
                else if (displayMaterial.Quality > 0)
                {
                    displayMaterial.DrawMenuIcons(b, materialPosition, 1f, 1f, 1f + 1.2E-05f, StackDrawType.HideButShowQuality, Color.White);
                }

            }

            if(displayReagent != ApothecaryHandle.items.none)
            {

                Vector2 reagentPosition = comp.bounds.Center.ToVector2() - new Vector2(40);

                b.Draw(Mod.instance.iconData.itemsTexture, reagentPosition, ApothecaryHandle.ItemRectangles(displayReagent), Color.White, 0f, Vector2.Zero, 4f, 0, 0.901f);

            }

        }

        public override void drawHover(SpriteBatch b)
        {

            base.drawHover(b);

            if (browsing)
            {

                AlchemyProcess.processes process = Enum.Parse<AlchemyProcess.processes>(contentComponents[focus].id);

                switch (process)
                {

                    case AlchemyProcess.processes.transmute:
                    case AlchemyProcess.processes.separate:
                    case AlchemyProcess.processes.enchant:

                        if (useRecipes.ContainsKey(process))
                        {

                            DrawAlchemyRecipe(b, process);

                            return;

                        }

                        break;

                }

                DrawProcessHover(b, process);

                return;

            }

        }

        public virtual void ClearHover()
        {

            hoverFocus = focus;

            hoverFocus = -1;

            hoverShift = 0;

            hoverTitle = null;

            hoverDescription = null;

            hoverDetails = new();

            hoverSlot = -1;

            hoverItem = ApothecaryHandle.items.none;

            hoverItemAmount = -1;

            hoverHeight = 0;

        }


        public virtual void PrepareProcessHover(AlchemyProcess.processes process)
        {

            ClearHover();

            ContentComponent comp = contentComponents[focus];

            List<string> details = new();

            hoverHeight = 0;

            switch (process)
            {

                case AlchemyProcess.processes.material:

                    if(useSlot == -1)
                    {

                        break;

                    }

                    if (Game1.player.Items[useSlot] == null)
                    {

                        break;

                    }

                    hoverSlot = useSlot;

                    hoverHeight += 60;

                    break;

                case AlchemyProcess.processes.reagent:

                    int reagentAmount = ApothecaryHandle.GetAmount(useItem);

                    if (reagentAmount <= 0)
                    {

                        break;

                    }

                    hoverItemAmount = reagentAmount;

                    hoverItem = useItem;
                    
                    hoverHeight += 60;
                    
                    break;

                case AlchemyProcess.processes.separate:


                    if (!MasteryHandle.HasMastery(Data.MasteryNode.nodes.alchemy_separate))
                    {

                        details.Add(StringData.Get(StringData.str.moreAlchemy));

                        break;

                    }

                    break;

                case AlchemyProcess.processes.enchant:

                    if (!MasteryHandle.HasMastery(Data.MasteryNode.nodes.alchemy_enchant))
                    {

                        details.Add(StringData.Get(StringData.str.moreAlchemy));

                        break;

                    }

                    break;

            }

            string useTitle = comp.text[0];

            string useDescription = comp.text[1];

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

        public virtual void DrawProcessHover(SpriteBatch b, AlchemyProcess.processes process)
        {

            if (hoverFocus != focus)
            {

                PrepareProcessHover(process);

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

            if (hoverSlot != -1)
            {

                DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

                textPosition += 12;

                ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(Game1.player.Items[hoverSlot].QualifiedItemId);

                Rectangle itemSource = dataOrErrorItem.GetSourceRect();

                if (itemSource.Height > 16)
                {

                    itemSource.Height = 16; itemSource.Y += 16;

                }

                b.Draw(dataOrErrorItem.GetTexture(), new Vector2(textMargin, textPosition), itemSource, Color.White, 0f, Vector2.Zero, 2f, 0, 1f);

                if (Game1.player.Items[hoverSlot].Quality > 0)
                {
                    Game1.player.Items[hoverSlot].DrawMenuIcons(b, new Vector2(textMargin, textPosition), 0.5f, 1f, 1f + 1.2E-05f, StackDrawType.HideButShowQuality, Color.White);
                }

                b.DrawString(Game1.smallFont, dataOrErrorItem.DisplayName, new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, dataOrErrorItem.DisplayName, new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                string displayAmount = "(" + Game1.player.Items[hoverSlot].Stack + ")";

                Vector2 displaySize = Game1.smallFont.MeasureString(displayAmount);

                b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - displaySize.X, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - displaySize.X - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

            }
            else
            if (hoverItem != ApothecaryHandle.items.none)
            {

                DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

                textPosition += 12;

                ApothecaryItem basePotion = Mod.instance.apothecaryHandle.apothecary[hoverItem];

                b.Draw(Mod.instance.iconData.itemsTexture, new Vector2(textMargin, textPosition), ApothecaryHandle.ItemRectangles(basePotion.item), Color.White, 0f, Vector2.Zero, 1.5f, 0, 0.901f);

                b.DrawString(Game1.smallFont, basePotion.title, new Vector2(textMargin + 40, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, basePotion.title, new Vector2(textMargin + 40 - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                string displayAmount = "(" + hoverItemAmount + ")";

                Vector2 displaySize = Game1.smallFont.MeasureString(displayAmount);

                b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - displaySize.X, textPosition + 2), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, displayAmount, new Vector2(textMargin + 476 - displaySize.X - 1.5f, textPosition + 2 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

            }


        }

        public virtual void PrepareAlchemyRecipe(AlchemyProcess.processes process)
        {

            ClearHover();

            hoverShift = ShiftPressed();

            AlchemyRecipe recipeData = Mod.instance.apothecaryHandle.recipes[useRecipes[process]];

            AlchemyProduct alchemyProduct = Mod.instance.apothecaryHandle.GetProduct(recipeData.type, useSlot, hoverShift);

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

            hoverHeight += 40; // product

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

        public void DrawAlchemyRecipe(SpriteBatch b, AlchemyProcess.processes process)
        {

            if (hoverFocus != focus || hoverShift != ShiftPressed())
            {

                PrepareAlchemyRecipe(process);

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
            // items (inventory)

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

                    if (Game1.player.Items[item.Key].Quality > 0)
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

    }

}
