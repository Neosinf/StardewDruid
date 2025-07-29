using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Menus;
using System;
using System.Diagnostics.Metrics;


namespace StardewDruid.Event
{
    public class EventBar
    {

        public enum barTypes
        {

            scene,
            progress,
            boss,

        }

        public barTypes type;

        public Color colour;

        public string text;

        public float progress;

        public string eventId;

        public int uniqueId;

        public int level;

        public Rectangle container = Rectangle.Empty;

        public float textHeight;

        public float textWidth;

        public Rectangle iconSource;

        public bool hover;

        public EventBar(string Text, barTypes Type = barTypes.scene, int UniqueId = 0)
        {

            text = Text;

            colour = Game1.textColor;

            type = Type;

            uniqueId = UniqueId;

            switch (type)
            {

                case barTypes.scene:

                    iconSource = IconData.DisplayRectangle(IconData.displays.skip);

                    break;


            }

        }

        public virtual bool update()
        {

            switch (type)
            {

                case barTypes.scene:

                    if (!Mod.instance.eventRegister.ContainsKey(eventId))
                    {

                        return false;

                    }

                    progress = Mod.instance.eventRegister[eventId].DisplayScene(uniqueId);

                    if (progress == -1f)
                    {

                        return false;

                    }

                    break;

                case barTypes.progress:

                    if (!Mod.instance.eventRegister.ContainsKey(eventId))
                    {

                        return false;

                    }

                    progress = Mod.instance.eventRegister[eventId].DisplayProgress(uniqueId);

                    if (progress == -1f)
                    {

                        return false;

                    }

                    break;

                case barTypes.boss:

                    if (!Mod.instance.eventRegister.ContainsKey(eventId))
                    {

                        return false;

                    }

                    if (!Mod.instance.eventRegister[eventId].bosses.ContainsKey(uniqueId))
                    {

                        return false;

                    }

                    StardewDruid.Monster.Boss monster = Mod.instance.eventRegister[eventId].bosses[uniqueId];

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

                    break;

            }

            return true;

        }

        public virtual void draw(SpriteBatch b, int mouseX, int mouseY, bool fade = false)
        {

            Rectangle titleSafeArea = Game1.graphics.GraphicsDevice.Viewport.GetTitleSafeArea();

            float displayScale = Game1.options.uiScale;

            float textScale = 1.35f * displayScale;

            Vector2 plainTextMeasure = Game1.smallFont.MeasureString(text) * textScale;

            textWidth = plainTextMeasure.X;

            textHeight = plainTextMeasure.Y;

            int barRight = (int)(360 * displayScale);

            int barHeight = (int)(52 * displayScale);

            container = new(titleSafeArea.Right - barRight, titleSafeArea.Top + Mod.instance.barOffset, 360, barHeight);

            Mod.instance.barOffset += barHeight;

            Mod.instance.barOffset += 8;

            float fadeOut = 1f;

            if (fade)
            {

                fadeOut = 0.5f;

            }

            IClickableMenu.drawTextureBox(
                b,
                Game1.mouseCursors,
                new Rectangle(384, 396, 15, 15),
                container.X,
                container.Y,
                container.Width,
                container.Height,
                Color.White * fadeOut,
                2f,
                false,
                0.0001f
            );

            b.Draw(Game1.staminaRect, new Vector2(container.X + 5, container.Y + 5), new Rectangle(container.X + 5, container.Y + 5, container.Width - 10, container.Height - 10), new Color(254, 240, 192) * fadeOut, 0f, Vector2.Zero, 1f, 0, 0.0001f);

            b.Draw(Game1.staminaRect, new Vector2(container.X + 5, container.Y + 5), new Rectangle(container.X + 5, container.Y + 5, (int)((container.Width - 10) * progress), container.Height - 10), colour * 0.25f * fadeOut, 0f, Vector2.Zero, 1f, 0, 0.0007f);

            textScale *= 0.85f;

            b.DrawString(Game1.smallFont, text, new Vector2(container.X + 7, container.Y + 9), Game1.textShadowColor * fadeOut, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0008f);

            b.DrawString(Game1.smallFont, text, new Vector2(container.X + 8, container.Y + 8), Game1.textColor * fadeOut, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0009f);

            hover = false;

            if (container.Contains(mouseX, mouseY) && !fade)
            {

                hover = true;

                drawHover(b, mouseX, mouseY, displayScale);

            }

        }

        public virtual void drawHover(SpriteBatch b, int mouseX, int mouseY, float displayScale)
        {

            switch (type)
            {
                case barTypes.scene:

                    float skipScale = 4f * displayScale;

                    int skipWidth = (int)(20 * skipScale);

                    string t = "Click to skip scene";

                    Vector2 plainTextMeasure = Game1.smallFont.MeasureString(t) * displayScale;

                    int plainWidth = (int)(plainTextMeasure.X / 2);

                    int plainHeight = (int)(plainTextMeasure.Y / 2);

                    Vector2 corner = new Vector2(mouseX, mouseY) + new Vector2(0 - (plainWidth + 16), 32);

                    Microsoft.Xna.Framework.Rectangle bounds = new((int)corner.X, (int)corner.Y, (int)plainTextMeasure.X + 32, (int)plainTextMeasure.Y + 16);

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        bounds.X,
                        bounds.Y,
                        bounds.Width,
                        bounds.Height,
                        Color.White,
                        3f,
                        true,
                        -1f
                    );

                    b.DrawString(
                        Game1.smallFont,
                        t,
                        new Vector2(bounds.Center.X - plainWidth,
                        bounds.Center.Y - plainHeight + 2f),
                        Game1.textColor,
                        0f, Vector2.Zero, displayScale, SpriteEffects.None, -1f);

                    b.DrawString(
                        Game1.smallFont,
                        t,
                        new Vector2(bounds.Center.X - plainWidth - 1f,
                        bounds.Center.Y - plainHeight + 2f + 1.5f),
                        Microsoft.Xna.Framework.Color.Brown * 0.35f,
                        0f, Vector2.Zero, displayScale, SpriteEffects.None, -1f);


                    break;

            }

        }

        public virtual void click(int mouseX, int mouseY)
        {

            if (!hover)
            {

                return;

            }

            if (!Mod.instance.eventRegister.ContainsKey(eventId))
            {

                return;

            }

            switch (type)
            {

                case barTypes.scene:

                    Mod.instance.eventRegister[eventId].SkipEvent(uniqueId);

                    break;

            }

        }

    }

}
