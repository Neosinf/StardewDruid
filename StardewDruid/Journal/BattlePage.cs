using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley.Menus;
using System.Diagnostics.Metrics;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;
using xTile.ObjectModel;
using StardewDruid.Handle;
using StardewDruid.Cast;


namespace StardewDruid.Journal
{
    public class BattlePage : DruidJournal
    {

        public BattleHandle handle;

        public int controlPosition;

        public int controlMode;

        public BattlePage(string QuestId, int Record, BattleHandle Handle) : base()
        {

            handle = Handle;

            type = journalTypes.battle;

            title = StringData.Strings(StringData.stringkeys.battle);

            pagination = 0;

            record = 0;

            width = handle.interfaceWidth;

            height = handle.interfaceHeight;

            Vector2 centeringOnScreen = Utility.getTopLeftPositionForCenteringOnScreen(width, height, 0, 0);

            xPositionOnScreen = (int)centeringOnScreen.X;

            yPositionOnScreen = (int)centeringOnScreen.Y + 32;

            controlPosition = yPositionOnScreen + height - 204;

            populateContent();

            populateInterface();

            activateInterface();

        }

        public override void populateContent()
        {

            contentComponents = handle.InterfaceState();

            focus = handle.ControlFocus();

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key, xPositionOnScreen, controlPosition, width, height);

            }

        }

        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void activateInterface()
        {

            resetInterface();

        }

        public override void pressContent()
        {

            if (contentComponents[focus].type == ContentComponent.contentTypes.relic)
            {

                handle.SummonOption(contentComponents[focus].id, focus);

                return;

            }

            if (contentComponents[focus].type == ContentComponent.contentTypes.battle)
            {

                handle.SelectOption(contentComponents[focus].id, focus);

                return;

            }

            if (contentComponents[focus].type == ContentComponent.contentTypes.potion)
            {

                if(!handle.ItemOption(contentComponents[focus].id, focus))
                {

                    Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                }
                
                return;

            }

            if (contentComponents[focus].type == ContentComponent.contentTypes.battlereturn)
            {

                handle.ReturnOption(contentComponents[focus].id, focus);

                return;

            }

            if (contentComponents[focus].type == ContentComponent.contentTypes.battlereadout)
            {

                handle.ResetState();

                return;

            }

        }

        public override void drawContent(SpriteBatch b)
        {

            handle.DrawBattle(b, xPositionOnScreen, yPositionOnScreen, width, height, controlPosition);

            base.drawContent(b);

        }

        public override void drawHover(SpriteBatch b)
        {

            base.drawHover(b);

            if (!browsing)
            {

                return;

            }

            ContentComponent component = contentComponents[focus];

            if(component.type == ContentComponent.contentTypes.battlereadout)
            {

                return;

            }

            float contentHeight = 16;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(component.text[1], Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            contentHeight += 24 + titleSize.Y;

            // -------------------------------------------------------
            // description

            string descriptionText = Game1.parseText(component.text[2], Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            contentHeight += 24 + descriptionSize.Y;

            string technicalText = String.Empty;

            Vector2 technicalSize = Vector2.Zero;

            // -------------------------------------------------------
            if (component.text.ContainsKey(3))
            {

                technicalText = Game1.parseText(component.text[3], Game1.smallFont, 476);

                technicalSize = Game1.smallFont.MeasureString(technicalText);

                contentHeight += 16 + technicalSize.Y;

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
            // technical

            if(component.text.ContainsKey(3))
            {

                b.DrawString(Game1.smallFont, technicalText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, technicalText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

                textPosition += 12 + technicalSize.Y;

            }


        }

    }

}
