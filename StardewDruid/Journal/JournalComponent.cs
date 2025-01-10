using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley.Menus;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley.Network;
using StardewDruid.Character;

namespace StardewDruid.Journal
{
    public class JournalComponent
    {

        public DruidJournal.journalButtons button;

        public Microsoft.Xna.Framework.Vector2 position;

        public IconData.displays display;

        public Microsoft.Xna.Framework.Rectangle bounds;

        public Microsoft.Xna.Framework.Rectangle source;

        public int hover;

        public string text;

        public bool active;

        public float fade = 1f;

        public JournalAdditional spec = new();

        public JournalComponent(DruidJournal.journalButtons Button, Vector2 Position, IconData.displays Display, JournalAdditional additional)
        {

            position = Position;

            button = Button;

            display = Display;

            spec = additional;

            setBounds();

        }

        public void setBounds()
        {

            source = IconData.DisplayRectangle(display);

            bounds = new((int)position.X - (int)(8f * spec.scale), (int)position.Y - (int)(8f * spec.scale), (int)(16f * spec.scale), (int)(16f * spec.scale));

            text = StringData.ButtonStrings(button);

        }

        public void draw(SpriteBatch b)
        {

            b.Draw(
                Mod.instance.iconData.displayTexture,
                position,
                source,
                Color.White * fade,
                0f,
                new Vector2(8),
                spec.scale + (0.05f * hover),
                spec.flip ? SpriteEffects.FlipHorizontally : 0,
                999f
            );

        }

    }

    public class JournalAdditional
    {

        public float scale = 3.5f;

        public bool flip;

        public float hoverLimit = 5;

    }

    public class ContentComponent
    {

        public enum contentTypes
        {
            list,
            header,
            potion,
            toggle,
            relic,
            title,
            text,
            custom,
            potionlist,
            herolist,
            //herobutton,
        }

        public contentTypes type;

        public bool active;

        public string id;

        public int serial;

        public Microsoft.Xna.Framework.Rectangle bounds;

        // display content

        public Dictionary<int, string> text = new();

        public Dictionary<int, string> textParse = new();

        public Dictionary<int, Vector2> textMeasures = new();

        public Dictionary<int, Microsoft.Xna.Framework.Color> textColours = new();

        public Dictionary<int, IconData.displays> icons = new();

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> iconSources = new();

        public Dictionary<int, Microsoft.Xna.Framework.Color> iconColours = new();

        public Dictionary<int, IconData.relics> relics = new();

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> relicSources = new();

        public Dictionary<int, Microsoft.Xna.Framework.Color> relicColours = new();

        public Dictionary<int, IconData.potions> potions = new();

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> potionSources = new();

        public Microsoft.Xna.Framework.Color defaultColour = new Color(86, 22, 12);

        public Dictionary<int, Texture2D> textures = new();

        public ContentComponent(contentTypes Type, string ID, bool Active = true)
        {

            active = Active;

            type = Type;

            id = ID;

        }

        public void setBounds(int index, int xP, int yP, int width, int height)
        {

            iconSources = new();

            relicSources = new();

            textMeasures = new();

            foreach (KeyValuePair<int, IconData.displays> icon in icons)
            {

                iconSources.Add(icon.Key, IconData.DisplayRectangle(icon.Value));

            }

            foreach (KeyValuePair<int, IconData.relics> relic in relics)
            {

                relicSources.Add(relic.Key, IconData.RelicRectangles(relic.Value));

            }

            foreach (KeyValuePair<int, IconData.potions> potion in potions)
            {

                potionSources.Add(potion.Key, IconData.PotionRectangles(potion.Value));

            }

            if (text.Count == 0)
            {

                text[0] = id;

            }

            foreach (KeyValuePair<int, string> texties in text)
            {

                textParse[texties.Key] = Game1.parseText(texties.Value, Game1.dialogueFont, width);

                textMeasures[texties.Key] = Game1.dialogueFont.MeasureString(textParse[texties.Key]);

                if (!textColours.ContainsKey(texties.Key))
                {

                    textColours[texties.Key] = defaultColour;

                }

            }

            switch (type)
            {

                case contentTypes.list:
                case contentTypes.potionlist:

                    bounds = new Rectangle(xP + 16, yP + 16 + index * ((height - 32) / 6), width - 32, (height - 32) / 6 + 4);

                    return;

                case contentTypes.title:

                    bounds = new Rectangle(xP, yP, width, 80);

                    return;

                case contentTypes.text:

                    // width should be journal.width - 128

                    bounds = new Rectangle(xP, yP, (int)textMeasures[0].X, (int)textMeasures[0].Y + 16);

                    return;

                case contentTypes.header:

                    foreach (KeyValuePair<int, string> texties in text)
                    {

                        textParse[texties.Key] = Game1.parseText(texties.Value, Game1.smallFont, width);

                        textMeasures[texties.Key] = Game1.smallFont.MeasureString(textParse[texties.Key]);

                    }

                    bounds = new Rectangle(xP + 16, yP + 16 + (index * (146 + 56)), width, 56);

                    return;

                case contentTypes.potion:

                    bounds = new Rectangle(xP + 16 + 4 + (index % 6) * ((width - 32) / 6), yP + 16 + (int)(index / 6) * (146 + 56), (width - 32) / 6 - 8, 146);

                    return;

                case contentTypes.toggle:

                    bounds = new Rectangle(xP + 16 + 4 + (index % 6) * ((width - 32) / 6), yP + 16 + 146 + (int)(index / 6) * (146 + 56), (width - 32) / 6 - 8, 56);

                    return;

                case contentTypes.relic:

                    bounds = new Rectangle(xP + 16 + 4 + (index % 6) * ((width - 32) / 6), yP + 16 + 56 + (int)(index / 6) * (146 + 56), (width - 32) / 6 - 8, 146);

                    return;

                case contentTypes.herolist:

                    bounds = new Rectangle(xP + 16, yP + 16 + index * ((height - 32) / 4), width - 32, (height - 32) / 4 + 4);

                    return;

                /*case contentTypes.herobutton:

                    bounds = new Rectangle(xP + width - 16, yP + 16 + index * ((height - 32) / 4), 32, (height - 32) / 4 + 4);

                    return;*/

            }

        }

        public void draw(SpriteBatch b, Vector2 offset, bool focus = false)
        {

            switch (type)
            {

                case contentTypes.list:
                case contentTypes.potionlist:

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        bounds.X,
                        bounds.Y,
                        bounds.Width,
                        bounds.Height,
                        focus ? Color.Wheat : Color.White,
                        4f,
                        false,
                        -1f
                    );

                    // icons
                    if (icons.Count > 0)
                    {

                        b.Draw(
                            Mod.instance.iconData.displayTexture,
                            new Vector2(bounds.Left + 20f + 32f - 1.5f, bounds.Center.Y + 3f),
                            iconSources[0],
                            Microsoft.Xna.Framework.Color.Black * 0.35f,
                            0f,
                            new Vector2(8),
                            3f,
                            0,
                            0.900f
                        );

                        b.Draw(
                            Mod.instance.iconData.displayTexture,
                            new Vector2(bounds.Left + 20f + 32f, bounds.Center.Y),
                            iconSources[0],
                            Microsoft.Xna.Framework.Color.White,
                            0f,
                            new Vector2(8),
                            3f,
                            0,
                            0.901f
                        );

                    }

                    if (icons.Count > 1)
                    {


                        b.Draw(
                            Mod.instance.iconData.displayTexture,
                            new Vector2(bounds.Right - 20f - 32f + 1f, bounds.Center.Y + 3f),
                            iconSources[1],
                            Microsoft.Xna.Framework.Color.Black * 0.35f,
                            0f,
                            new Vector2(8),
                            3f,
                            0,
                            0.900f
                        );

                        b.Draw(
                            Mod.instance.iconData.displayTexture,
                            new Vector2(bounds.Right - 20f - 32f, bounds.Center.Y),
                            iconSources[1],
                            Microsoft.Xna.Framework.Color.White,
                            0f,
                            new Vector2(8),
                            3f,
                            0,
                            0.901f
                        );

                    }

                    // title

                    b.DrawString(
                        Game1.dialogueFont,
                        textParse[0],
                        new Vector2(bounds.Left + 20f + 64 + 16f - 1f, bounds.Center.Y - (textMeasures[0].Y / 2) + 1.5f),
                        Microsoft.Xna.Framework.Color.Brown * 0.35f,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.900f
                    );

                    b.DrawString(
                        Game1.dialogueFont,
                        textParse[0],
                        new Vector2(bounds.Left + 20f + 64 + 16f, bounds.Center.Y - (textMeasures[0].Y / 2)),
                        defaultColour,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.901f
                    );

                    if(type == contentTypes.potionlist)
                    {

                        b.Draw(Mod.instance.iconData.potionsTexture, new Vector2(bounds.Left + 20f + 32f - 1.5f, bounds.Center.Y + 3f), potionSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, new Vector2(10), 2.5f, 0, 0.900f);

                        b.Draw(Mod.instance.iconData.potionsTexture, new Vector2(bounds.Left + 20f + 32f, bounds.Center.Y), potionSources[0], Color.White, 0f, new Vector2(10), 2.5f, 0, 0.901f);

                        b.DrawString(
                            Game1.dialogueFont,
                            textParse[1],
                            new Vector2(bounds.Right - 144f - 1.5f, bounds.Center.Y - (textMeasures[0].Y / 2) + 1.5f),
                            Microsoft.Xna.Framework.Color.Brown * 0.35f,
                            0f,
                            Vector2.Zero,
                            1f,
                            SpriteEffects.None,
                            0.900f
                        );

                        b.DrawString(
                            Game1.dialogueFont,
                            textParse[1],
                            new Vector2(bounds.Right - 144f, bounds.Center.Y - (textMeasures[0].Y / 2)),
                            defaultColour,
                            0f,
                            Vector2.Zero,
                            1f,
                            SpriteEffects.None,
                            0.901f
                        );

                    }

                    return;

                case contentTypes.title:

                    b.DrawString(
                        Game1.dialogueFont,
                        text[0],
                        new Vector2(bounds.Center.X - (textMeasures[0].X / 2f * 1.1f) - 1.5f, offset.Y + bounds.Center.Y - (textMeasures[0].Y / 2 * 1.1f) + 1.5f),
                        Microsoft.Xna.Framework.Color.Brown * 0.35f,
                        0f,
                        Vector2.Zero,
                        1.1f,
                        SpriteEffects.None,
                        0.900f
                    );

                    b.DrawString(
                        Game1.dialogueFont,
                        text[0],
                        new Vector2(bounds.Center.X - (textMeasures[0].X / 2f * 1.1f), offset.Y + bounds.Center.Y - (textMeasures[0].Y / 2 * 1.1f)),
                        textColours[0],
                        0f,
                        Vector2.Zero,
                        1.1f,
                        SpriteEffects.None,
                        0.901f
                    );

                    return;

                case contentTypes.text:

                    b.DrawString(
                        Game1.dialogueFont,
                        textParse[0],
                        new Vector2(bounds.Left - 1.5f, offset.Y + bounds.Top + 1.5f),
                        Microsoft.Xna.Framework.Color.Brown * 0.35f,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.900f
                    );

                    b.DrawString(
                        Game1.dialogueFont,
                        textParse[0],
                        new Vector2(bounds.Left, offset.Y + bounds.Top),
                        textColours[0],
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.901f
                    );
                    return;

                case contentTypes.header:

                    b.DrawString(Game1.smallFont, textParse[0], new Vector2(bounds.Left + 10 - 1f, bounds.Center.Y - textMeasures[0].Y / 2 + 2f + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.smallFont, textParse[0], new Vector2(bounds.Left + 10, bounds.Center.Y - textMeasures[0].Y / 2 + 2f), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0.901f);

                    //b.DrawString(Game1.dialogueFont, textParse[1], new Vector2(bounds.Left + 156 - 0.5f, bounds.Center.Y - (textMeasures[1].Y * 0.8f) / 2 + 4f + 0.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.900f);
                    
                    int headerLeft = Math.Max(272, (int)textMeasures[0].X + 48);

                    b.DrawString(Game1.smallFont, textParse[1], new Vector2(bounds.Left + headerLeft, bounds.Center.Y - (textMeasures[1].Y * 0.9f) / 2 + 4f), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -0.901f);

                    return;

                case contentTypes.potion:

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        bounds.X,
                        bounds.Y,
                        bounds.Width,
                        bounds.Height,
                        focus ? Color.Wheat : Color.White,
                        4f,
                        false,
                        -1f
                    );

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Center.X + 6 - (textMeasures[0].X * 0.9f / 2) - 1f, bounds.Center.Y + 24 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Center.X + 6 - (textMeasures[0].X * 0.9f / 2), bounds.Center.Y + 24), textColours[0], 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.901f);

                    b.Draw(Mod.instance.iconData.potionsTexture, new Vector2(bounds.Center.X - 40f + 2f, bounds.Center.Y - 60f + 4f), potionSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, Vector2.Zero, 4f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.potionsTexture, new Vector2(bounds.Center.X - 40f, bounds.Center.Y - 60f), potionSources[0], Color.White, 0f, Vector2.Zero, 4f, 0, 0.901f);

                    return;

                case contentTypes.toggle:

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        bounds.Center.ToVector2() + new Vector2(-1f,2f),
                        iconSources[0],
                        Microsoft.Xna.Framework.Color.Black * 0.35f,
                        0f,
                        new Vector2(8),
                        2.5f,
                        0,
                        0.900f
                    );

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        bounds.Center.ToVector2(),
                        iconSources[0],
                        Microsoft.Xna.Framework.Color.White,
                        0f,
                        new Vector2(8),
                        2.5f,
                        0,
                        0.901f
                    );

                    return;

                case contentTypes.relic:

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        bounds.X,
                        bounds.Y,
                        bounds.Width,
                        bounds.Height,
                        focus ? Color.Wheat : Color.White,
                        4f,
                        false,
                        -1f
                    );

                    b.Draw(Mod.instance.iconData.relicsTexture, new Vector2(bounds.Center.X - 40f + 2f, bounds.Center.Y - 40f + 4f), relicSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, Vector2.Zero, 4f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.relicsTexture, new Vector2(bounds.Center.X - 40f, bounds.Center.Y - 40f), relicSources[0], relicColours[0], 0f, Vector2.Zero, 4f, 0, 0.901f);

                    return;

                case contentTypes.herolist:

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        bounds.X,
                        bounds.Y,
                        bounds.Width,
                        bounds.Height,
                        focus ? Color.Wheat : Color.White,
                        4f,
                        false,
                        -1f
                    );

                    if (icons.Count > 0)
                    {

                        b.Draw(
                            Mod.instance.iconData.displayTexture,
                            new Vector2(bounds.Right - 96 + 1f, bounds.Center.Y + 3f),
                            iconSources[0],
                            Microsoft.Xna.Framework.Color.Black * 0.35f,
                            0f,
                            new Vector2(8),
                            3f,
                            0,
                            0.900f
                        );

                        b.Draw(
                            Mod.instance.iconData.displayTexture,
                            new Vector2(bounds.Right - 96, bounds.Center.Y),
                            iconSources[0],
                            Microsoft.Xna.Framework.Color.White,
                            0f,
                            new Vector2(8),
                            3f,
                            0,
                            0.901f
                        );

                    }

                    // title

                    b.DrawString(
                        Game1.dialogueFont,
                        textParse[0],
                        new Vector2(bounds.Left + 192f + 1f, bounds.Center.Y - (textMeasures[0].Y / 2) + 1.5f),
                        Microsoft.Xna.Framework.Color.Brown * 0.35f,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.900f
                    );

                    b.DrawString(
                        Game1.dialogueFont,
                        textParse[0],
                        new Vector2(bounds.Left + 192f, bounds.Center.Y - (textMeasures[0].Y / 2)),
                        defaultColour,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.901f
                    );

                    // image

                    b.Draw(
                        textures[0],
                        new Vector2(bounds.Left + 96f, bounds.Center.Y - 4f),
                        new Rectangle(0, 0, 16, 24),
                        Microsoft.Xna.Framework.Color.White,
                        0f,
                        new Vector2(8,12),
                        5f,
                        0,
                        0.900f
                    );

                    return;

                /*case contentTypes.herobutton:

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        bounds.Center.ToVector2() + new Vector2(-1f, 2f),
                        iconSources[0],
                        Microsoft.Xna.Framework.Color.Black * 0.35f,
                        0f,
                        new Vector2(8),
                        3f,
                        0,
                        0.900f
                    );

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        bounds.Center.ToVector2(),
                        iconSources[0],
                        Microsoft.Xna.Framework.Color.White,
                        0f,
                        new Vector2(8),
                        3f,
                        0,
                        0.901f
                    );

                    return;*/

            }

        }

    }

}
