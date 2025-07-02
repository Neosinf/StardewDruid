using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Menus;
using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Channels;


namespace StardewDruid.Event
{
    public class EventDisplay
    {

        public enum displayTypes
        {

            title,
            plain,
            bar,
            quick,

        }

        public displayTypes type;

        public Color colour;

        public string title;

        public string text;

        public int time;

        public int count;

        public float progress;

        public string eventId;

        public int uniqueId;

        public int boss;

        public int level;

        public float textHeight;

        public float textWidth;

        public Texture2D portrait;

        public Rectangle portraitSource;

        public EventDisplay(string Title, string Text, int Time, displayTypes Type = displayTypes.title, int UniqueId = 0)
        {

            title = Title;

            colour = Game1.textColor;

            text = Text;

            time = Time * 10;

            count = 0;

            type = Type;

            boss = -1;

            uniqueId = UniqueId;

            switch (type)
            {

                case displayTypes.bar:

                    break;

                case displayTypes.title:
                case displayTypes.plain:

                    Vector2 plainTextMeasure = Game1.smallFont.MeasureString(text) * 1.35f;

                    textWidth = plainTextMeasure.X;

                    textHeight = plainTextMeasure.Y;
                    
                    break;

            }

        }

        public virtual bool update()
        {

            switch (type)
            {

                case displayTypes.bar:
            

                    if (eventId == null)
                    {

                        return false;

                    }

                    if (!Mod.instance.eventRegister.ContainsKey(eventId))
                    {

                        return false;

                    }

                    if (boss != -1)
                    {

                        if (!Mod.instance.eventRegister[eventId].bosses.ContainsKey(boss))
                        {

                            return false;

                        }

                        StardewDruid.Monster.Boss monster = Mod.instance.eventRegister[eventId].bosses[boss];

                        if (monster.netWoundedActive.Value)
                        {

                            progress = 0;

                            return true;

                        }

                        if (!ModUtility.MonsterVitals(monster, Game1.player.currentLocation))
                        {

                            return false;

                        }

                        progress = (float)monster.Health / (float)monster.MaxHealth;

                    }
                    else
                    {

                        progress = Mod.instance.eventRegister[eventId].DisplayProgress(uniqueId);

                        if (progress == -1f)
                        {

                            return false;

                        }

                    }

                    break;

                case displayTypes.title:
                case displayTypes.plain:
                case displayTypes.quick:

                    count++;

                    if (count >= time)
                    {

                        return false;

                    }

                    break;

            }

            return true;

        }

        public virtual void draw(SpriteBatch b)
        {

            if(Game1.activeClickableMenu != null || !Game1.displayHUD)
            {

                return;

            }

            Rectangle titleSafeArea = Game1.graphics.GraphicsDevice.Viewport.GetTitleSafeArea();

            Rectangle container;

            float containerOffset;

            float containerHeight;

            int close = time - count;

            float displayScale = 1f;

            bool textRender = true;

            switch (type)
            {

                case displayTypes.title:
                case displayTypes.plain:
                case displayTypes.quick:


                    int portraitOffset = 0;

                    containerHeight = Math.Max((int)(textHeight + 24),64);

                    if (portrait != null)
                    {

                        portraitOffset = (int)((textHeight + 24) / 2);

                    }

                    int containerWidth = 800;

                    if(type == displayTypes.quick)
                    {

                        containerWidth = 400;

                    }

                    containerOffset = Math.Max(textWidth + 48, containerWidth - (portraitOffset * 2));

                    if (count < 4)
                    {
                        displayScale = (0.2f * count);

                        containerHeight *= displayScale;

                        containerOffset *= displayScale;

                        textRender = false;

                    } 
                    else if (close < 4)
                    {

                        displayScale = (0.2f * close);

                        containerHeight *= displayScale;

                        containerOffset *= displayScale;
                        
                        textRender = false;
                    
                    }

                    container = new(titleSafeArea.Center.X - (int)(containerOffset / 2) - portraitOffset, titleSafeArea.Bottom - Mod.instance.displayOffset - (int)containerHeight, (int)containerOffset + (portraitOffset * 2), (int)containerHeight);

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        container.X,
                        container.Y,
                        container.Width, 
                        container.Height,
                        Color.White,
                        3f * displayScale,
                        false,
                        0.0001f
                    );

                    Mod.instance.displayOffset += (int)containerHeight + 8;

                    if (!textRender)
                    {

                        break;
                    
                    }

                    b.DrawString(Game1.smallFont, text, new Vector2(container.Center.X - (textWidth / 2) + portraitOffset - 1, container.Center.Y - (textHeight / 2) + 5), Game1.textShadowColor, 0f, Vector2.Zero, 1.35f, SpriteEffects.None, 0.0006f);

                    b.DrawString(Game1.smallFont, text, new Vector2(container.Center.X - (textWidth / 2) + portraitOffset, container.Center.Y - (textHeight / 2) + 3), Game1.textColor * 0.75f, 0f, Vector2.Zero, 1.35f, SpriteEffects.None, 0.0007f);

                    if(portrait != null)
                    {

                        IClickableMenu.drawTextureBox(
                            b,
                            Game1.mouseCursors,
                            new Rectangle(384, 396, 15, 15),
                            container.Left - 2, 
                            container.Center.Y- portraitOffset - 2,
                            (int)(containerHeight)+4,
                            (int)(containerHeight)+4,
                            Color.White,
                            3f,
                            false,
                            0.0008f
                        );
                            
                        b.Draw(
                            portrait,
                            new Vector2(container.Left + portraitOffset, container.Center.Y),
                            portraitSource,
                            Microsoft.Xna.Framework.Color.White,
                            0f,
                            new Vector2(8),
                            (containerHeight-8) / 16,
                            0,
                            0.0009f
                        );

                    }

                    break;

                case displayTypes.bar:

                    container = new(titleSafeArea.Right - 360, titleSafeArea.Top + Mod.instance.barOffset, 360, 52);

                    Mod.instance.barOffset += 60;

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        container.X,
                        container.Y,
                        container.Width,
                        container.Height,
                        Color.White,
                        2f,
                        false,
                        0.0001f
                    );

                    b.Draw(Game1.staminaRect, new Vector2(container.X + 5, container.Y + 5), new Rectangle(container.X + 5, container.Y + 5, container.Width - 10, container.Height - 10), new(254, 240, 192), 0f, Vector2.Zero, 1f, 0, 0.0001f);

                    b.Draw(Game1.staminaRect, new Vector2(container.X + 5, container.Y + 5), new Rectangle(container.X + 5, container.Y + 5, (int)((container.Width - 10) * progress), container.Height-10), colour * 0.25f, 0f, Vector2.Zero, 1f, 0, 0.0007f);

                    b.DrawString(Game1.smallFont, text, new Vector2(container.X + 7, container.Y + 9), Game1.textShadowColor, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0.0008f);

                    b.DrawString(Game1.smallFont, text, new Vector2(container.X + 8, container.Y + 8), Game1.textColor, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0.0009f);

                    break;


            }

        }

    }

}
