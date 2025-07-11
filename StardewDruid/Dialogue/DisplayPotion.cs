﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewValley.Menus;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StardewValley.Extensions;
using System.Reflection;
using static StardewDruid.Data.IconData;
using StardewDruid.Data;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Handle;

namespace StardewDruid.Dialogue
{
    public class DisplayPotion : StardewValley.HUDMessage
    {

        public Herbal herbal;

        public DisplayPotion(string message, Herbal Herbal)
          : base(message)
        {

            herbal = Herbal;
            
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

            b.Draw(Mod.instance.iconData.potionsTexture, vector + new Vector2( 2f, 4f), IconData.PotionRectangles(herbal.display), Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, Vector2.Zero, 3f, 0, 0.900f);
            
            b.Draw(Mod.instance.iconData.potionsTexture, vector, IconData.PotionRectangles(herbal.display), Color.White, 0f, Vector2.Zero, 3f, 0, 0.901f);

            float timeThink = timeLeft - 3000f;

            if (!message.Contains("-"))
            {

                b.Draw(Game1.mouseCursors, vector + new Vector2(24, 24) + new Vector2(8f, 8f) * 4f, new Rectangle(0, 411, 16, 16), Color.White * transparency, 0f, new Vector2(8f, 8f), 3f + (timeThink < 0f ? 0f : timeThink / 900f), SpriteEffects.None, 1f);

            }

            vector.X += 51f;

            vector.Y += 51f;

            vector.X += 32f;

            vector.Y -= 33f;

            Utility.drawTextWithShadow(b, message, Game1.smallFont, vector, Game1.textColor * transparency, 1f, 1f, -1, -1, transparency);

        }

    }

}
