using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

namespace StardewDruid.Journal
{
    public class RelicJournal : DruidJournal
    {

        public RelicJournal(string QuestId, int Record) : base(QuestId, Record)
        {

        }

        public override void populateInterface()
        {

            type = journalTypes.relics;

            title = DialogueData.Strings(DialogueData.stringkeys.reliquary);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.quests),
                [102] = addButton(journalButtons.effects),
                [103] = addButton(journalButtons.relics),
                [104] = addButton(journalButtons.herbalism),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),

                [301] = addButton(journalButtons.exit),

                [305] = addButton(journalButtons.end),
                [306] = addButton(journalButtons.forward),

            };

        }

        public override void populateContent()
        {

            pagination = 18;

            contentComponents = Mod.instance.relicsData.JournalRelics(); //Mod.instance.questHandle.JournalEffects();

            otherComponents = Mod.instance.relicsData.JournalHeaders();

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

                component.Value.setBounds(component.Key % 3, xPositionOnScreen, yPositionOnScreen, width, height);

            }

        }

        public override void activateInterface()
        {

            resetInterface();

            fadeMenu();

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

        public override void pressContent()
        {

            string relicId = contentComponents[focus].id;

            if (!Mod.instance.save.reliquary.ContainsKey(relicId))
            {

                return;

            }

            if (!Mod.instance.relicsData.reliquary[relicId].function)
            {

                return;

            }

            int function = Mod.instance.relicsData.RelicFunction(relicId);

            switch (function)
            {

                case 1:

                    exitThisMenu();

                    return;

                case 2:

                    openJournal(journalTypes.relicPage, relicId, focus);

                    return;

                case 3:

                    openJournal(journalTypes.dragonPage, relicId, focus);

                    return;

                default:

                    Game1.playSound("ghost");

                    return;

            }

        }        
        
        public override void pressCancel()
        {

            string relicId = contentComponents[focus].id;

            if (!Mod.instance.save.reliquary.ContainsKey(relicId))
            {

                return;

            }

            if (!Mod.instance.relicsData.reliquary[relicId].cancel)
            {

                return;

            }

            int function = Mod.instance.relicsData.RelicCancel(relicId);

            switch (function)
            {

                case 1:

                    exitThisMenu();

                    return;

                default:

                    Game1.playSound("ghost");

                    return;

            }

        }

        public override void drawContent(SpriteBatch b)
        {

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

            string relicId = contentComponents[focus].id;

            Relic relic = Mod.instance.relicsData.reliquary[relicId];

            string relicTitle;

            string relicDescription;

            List<string> relicDetails = new();

            if (Mod.instance.save.reliquary.ContainsKey(relicId))
            {

                relicTitle = relic.title;

                relicDescription = relic.description;

                relicDetails = new(relic.details);

            }
            else
            {

                relicTitle = DialogueData.Strings(stringkeys.relicUnknown);

                relicDescription = DialogueData.Strings(stringkeys.relicNotFound);

                relicDetails.Add(relic.hint);

                //details.AddRange(relic.details);

            }

            float contentHeight = 16;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(relicTitle, Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            contentHeight += 24 + titleSize.Y;

            // -------------------------------------------------------
            // description

            string descriptionText = Game1.parseText(relicDescription, Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            contentHeight += 24 + descriptionSize.Y;

            if (relicDetails.Count > 0)
            {

                foreach (string detail in relicDetails)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    contentHeight += detailSize.Y;

                }

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

            if (relicDetails.Count > 0)
            {

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

                textPosition += 12;

                foreach (string detail in relicDetails)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin, textPosition), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += detailSize.Y;

                }

                textPosition += 12;

            }

        }

    }

}
