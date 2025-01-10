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

            Vector2 textOffset;

            float containerOffset;

            float containerHeight;

            int close = time - count;

            float displayScale = 1f;

            bool textRender = true;

            switch (type)
            {

                case displayTypes.title:
                case displayTypes.plain:

                    textOffset = Game1.smallFont.MeasureString(text) * 1.35f;

                    containerOffset = Math.Max(textOffset.X + 48, 800);

                    containerHeight = (int)(textOffset.Y + 24);

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
                        3f * displayScale,
                        false,
                        990f
                    );

                    Mod.instance.displayOffset += (int)containerHeight + 8;

                    if (!textRender)
                    {

                        break;
                    
                    }

                    b.DrawString(Game1.smallFont, text, new Vector2(container.Center.X - (textOffset.X / 2) - 1, container.Center.Y - (textOffset.Y / 2) + 5), Game1.textShadowColor, 0f, Vector2.Zero, 1.35f, SpriteEffects.None, 998f);

                    b.DrawString(Game1.smallFont, text, new Vector2(container.Center.X - (textOffset.X / 2), container.Center.Y - (textOffset.Y / 2) + 3), Game1.textColor * 0.75f, 0f, Vector2.Zero, 1.35f, SpriteEffects.None, 999f);

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
                        990f
                    );

                    b.Draw(Game1.staminaRect, new Vector2(container.X + 5, container.Y + 5), new Rectangle(container.X + 5, container.Y + 5, container.Width - 10, container.Height - 10), new(254, 240, 192), 0f, Vector2.Zero, 1f, 0, 990f);

                    b.Draw(Game1.staminaRect, new Vector2(container.X + 5, container.Y + 5), new Rectangle(container.X + 5, container.Y + 5, (int)((container.Width - 10) * progress), container.Height-10), colour * 0.25f, 0f, Vector2.Zero, 1f, 0, 997f);

                    b.DrawString(Game1.smallFont, text, new Vector2(container.X + 7, container.Y + 9), Game1.textShadowColor, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 998f);

                    b.DrawString(Game1.smallFont, text, new Vector2(container.X + 8, container.Y + 8), Game1.textColor, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 999f);

                    break;


            }

        }

    }

}
