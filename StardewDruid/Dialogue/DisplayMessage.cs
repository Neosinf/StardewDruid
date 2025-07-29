using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Dialogue
{
    public class DisplayMessage : StardewValley.HUDMessage
    {

        public enum iconTypes
        {
            potion,
            relic,
            display,
        }

        public iconTypes iconType;

        public IconData.potions potion;

        public IconData.relics relic;

        public IconData.displays display;

        public Microsoft.Xna.Framework.Rectangle iconSource;

        public DisplayMessage(string message, Herbal Herbal)
          : base(message)
        {

            potion = Herbal.display;

            iconSource = IconData.PotionRectangles(potion);

            iconType = iconTypes.potion;


        }

        public DisplayMessage(string message, IconData.relics RelicId)
          : base(message)
        {

            relic = RelicId;

            iconSource = IconData.RelicRectangles(relic);

            iconType = iconTypes.relic;


        }

        public DisplayMessage(string message, IconData.displays DisplayId)
          : base(message)
        {

            display = DisplayId;

            iconSource = IconData.DisplayRectangle(display);

            iconType = iconTypes.display;

        }

        public override void draw(SpriteBatch b, int i, ref int heightUsed)
        {

            Rectangle titleSafeArea = Game1.graphics.GraphicsDevice.Viewport.GetTitleSafeArea();

            int num2 = 112;

            Vector2 vector = new Vector2(titleSafeArea.Left + 16, titleSafeArea.Bottom - num2 - heightUsed - 64);

            heightUsed += num2;

            if (Game1.isOutdoorMapSmallerThanViewport())
            {
                vector.X = Math.Max(titleSafeArea.Left + 16, -Game1.uiViewport.X + 16);
            }

            if (Game1.uiViewport.Width < 1400)
            {
                vector.Y -= 48f;
            }

            b.Draw(Game1.mouseCursors, vector, (messageSubject is StardewValley.Object @object && @object.sellToStorePrice(-1L) > 500) ? new Rectangle(163, 399, 26, 24) : new Rectangle(293, 360, 26, 24), Color.White * transparency, 0f, Vector2.Zero, 4f, SpriteEffects.None, 1f);

            float x = Game1.smallFont.MeasureString(message).X;

            b.Draw(Game1.mouseCursors, new Vector2(vector.X + 104f, vector.Y), new Rectangle(319, 360, 1, 24), Color.White * transparency, 0f, Vector2.Zero, new Vector2(x, 4f), SpriteEffects.None, 1f);

            b.Draw(Game1.mouseCursors, new Vector2(vector.X + 104f + x, vector.Y), new Rectangle(323, 360, 6, 24), Color.White * transparency, 0f, Vector2.Zero, 4f, SpriteEffects.None, 1f);

            vector.X += 16f;
            vector.Y += 16f;

            float timeThink = timeLeft - 3000f;

            switch (iconType)
            {

                case iconTypes.potion:

                    b.Draw(Mod.instance.iconData.potionsTexture, vector + new Vector2(2f, 4f), iconSource, Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, Vector2.Zero, 3f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.potionsTexture, vector, iconSource, Color.White, 0f, Vector2.Zero, 3f, 0, 0.901f);

                    if (!message.Contains("-"))
                    {

                        b.Draw(Game1.mouseCursors, vector + new Vector2(24, 24) + new Vector2(8f, 8f) * 4f, new Rectangle(0, 411, 16, 16), Color.White * transparency, 0f, new Vector2(8f, 8f), 3f + (timeThink < 0f ? 0f : timeThink / 900f), SpriteEffects.None, 1f);

                    }

                    break;

                case iconTypes.relic:

                    b.Draw(Mod.instance.iconData.relicsTexture, vector + new Vector2(2f, 4f), iconSource, Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, Vector2.Zero, 3f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.relicsTexture, vector, iconSource, Color.White, 0f, Vector2.Zero, 3f, 0, 0.901f);

                    if (!message.Contains("-"))
                    {

                        b.Draw(Game1.mouseCursors, vector + new Vector2(24, 24) + new Vector2(8f, 8f) * 4f, new Rectangle(0, 411, 16, 16), Color.White * transparency, 0f, new Vector2(8f, 8f), 3f + (timeThink < 0f ? 0f : timeThink / 900f), SpriteEffects.None, 1f);

                    }

                    break;

                case iconTypes.display:

                    //b.Draw(Mod.instance.iconData.displayTexture, vector + new Vector2(2f, 4f), iconSource, Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, Vector2.Zero, 4f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.displayTexture, vector, iconSource, Color.White, 0f, Vector2.Zero, 4f, 0, 0.901f);

                    break;

            }

            vector.X += 51f;

            vector.Y += 51f;

            vector.X += 32f;

            vector.Y -= 33f;

            Utility.drawTextWithShadow(b, message, Game1.smallFont, vector, Game1.textColor * transparency, 1f, 1f, -1, -1, transparency);

        }

    }

}
