using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Menus;
using System;


namespace StardewDruid.Event
{
    public class EventDisplay
    {

        public enum displayTypes
        {

            title,
            plain,
            quick,
            strong,
        }

        public displayTypes type;

        public Color colour;

        public string title;

        public string text;

        public int time;

        public float progress;

        public string eventId;

        public int uniqueId;

        public int level;

        public Rectangle container = Rectangle.Empty;

        public float textHeight;

        public float textWidth;

        public Texture2D imageTexture;

        public Rectangle imageSource;

        public bool imageFrame;

        public bool hover;

        public EventDisplay(string Text, string Title,  int Time, displayTypes Type = displayTypes.title, int UniqueId = 0)
        {

            text = Text;

            title = Title;

            colour = Game1.textColor;

            time = Time;

            type = Type;

            uniqueId = UniqueId;


        }

        public virtual bool update()
        {

            progress++;

            if (progress >= time)
            {

                return false;

            }

            return true;

        }

        public virtual void draw(SpriteBatch b, int mouseX, int mouseY)
        {

            Rectangle titleSafeArea = Game1.graphics.GraphicsDevice.Viewport.GetTitleSafeArea();

            float containerOffset;

            float containerHeight;

            float displayScale = 1f;

            displayScale *= Game1.options.uiScale;

            float textScale = 1.35f;

            textScale *= Game1.options.uiScale;

            bool textRender = true;

            if (type == displayTypes.strong)
            {

                displayScale *= 1.1f;

                textScale *= 1.1f;

            }

            Vector2 plainTextMeasure = Game1.smallFont.MeasureString(text) * textScale;

            textWidth = plainTextMeasure.X;

            textHeight = plainTextMeasure.Y;

            switch (type)
            {

                case displayTypes.title:
                case displayTypes.plain:
                case displayTypes.quick:
                case displayTypes.strong:

                    int close = time - (int)progress;

                    containerHeight = Math.Max((int)(textHeight + 24),64);

                    int containerWidth = (int)(640 * displayScale);

                    if(type == displayTypes.quick)
                    {

                        containerWidth /= 2;

                    }

                    containerOffset = Math.Max(textWidth + 48, containerWidth);
                    
                    if (imageTexture != null)
                    {

                        containerOffset += containerHeight;

                    }

                    if ((int)progress < 4)
                    {
                        displayScale *= (0.2f * (int)progress);

                        containerHeight *= displayScale;

                        containerOffset *= displayScale;

                        textRender = false;

                    } 
                    else if (close < 4)
                    {

                        displayScale *= (0.2f * close);

                        containerHeight *= displayScale;

                        containerOffset *= displayScale;
                        
                        textRender = false;
                    
                    }

                    container = new(titleSafeArea.Center.X - (int)(containerOffset / 2), titleSafeArea.Bottom - Mod.instance.displayOffset - (int)containerHeight, (int)containerOffset, (int)containerHeight);

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        container.X,
                        container.Y,
                        container.Width, 
                        container.Height,
                        Color.White,
                        3f,
                        false,
                        0.0001f
                    );

                    Mod.instance.displayOffset += (int)containerHeight;
                    
                    Mod.instance.barOffset += 8;

                    if (!textRender)
                    {

                        break;
                    
                    }

                    if(imageTexture != null)
                    {

                        if (imageFrame)
                        {

                            IClickableMenu.drawTextureBox(
                                b,
                                Game1.mouseCursors,
                                new Rectangle(384, 396, 15, 15),
                                container.Left - 2,
                                container.Top - 2,
                                (int)(containerHeight) + 4,
                                (int)(containerHeight) + 4,
                                Color.White,
                                3f,
                                false,
                                0.0008f
                            );

                        }
                            
                        b.Draw(
                            imageTexture,
                            new Vector2(container.Left + containerHeight/2, container.Center.Y),
                            imageSource,
                            Microsoft.Xna.Framework.Color.White,
                            0f,
                            new Vector2(8),
                            4f,
                            0,
                            0.0009f
                        );


                        b.DrawString(Game1.smallFont, text, new Vector2(container.Center.X - (textWidth / 2) + (containerHeight / 2) - 1, container.Center.Y - (textHeight / 2) + 5), Game1.textShadowColor, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0006f);

                        b.DrawString(Game1.smallFont, text, new Vector2(container.Center.X - (textWidth / 2) + (containerHeight / 2), container.Center.Y - (textHeight / 2) + 3), Game1.textColor * 0.75f, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0007f);

                        break;

                    }


                    b.DrawString(Game1.smallFont, text, new Vector2(container.Center.X - (textWidth / 2) - 1, container.Center.Y - (textHeight / 2) + 5), Game1.textShadowColor, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0006f);

                    b.DrawString(Game1.smallFont, text, new Vector2(container.Center.X - (textWidth / 2), container.Center.Y - (textHeight / 2) + 3), Game1.textColor * 0.75f, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0007f);


                    break;

            }

            hover = false;

            if (container.Contains(mouseX, mouseY))
            {

                hover = true;

            }

        }

        public virtual void click(int mouseX, int mouseY)
        {

            if(!hover)
            {
                return;
            }

            int exitProgress = time - 4;

            if (progress < exitProgress)
            {

                progress = exitProgress;

            }

        }

    }

}
