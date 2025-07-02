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
using StardewDruid.Cast.Mists;

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
            toggleleft,
            toggleright,
            relic,
            portrait,
            title,
            text,
            textsmall,
            custom,
            herolist,
            
            goods,
            machine,
            estimate,
            order,

            battle,
            battlereadout,
            battlereturn,
            battleportrait,

            lovebar,
        
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

        public Microsoft.Xna.Framework.Color defaultColour = new Color(86, 22, 12);

        public Dictionary<int, Texture2D> textures = new();

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> textureSources = new();

        public Dictionary<int, Microsoft.Xna.Framework.Color> textureColours = new();

        public int readout;

        public int readoutTick;

        public int readoutPace;

        public int readoutLength;

        public ContentComponent(contentTypes Type, string ID, bool Active = true)
        {

            active = Active;

            type = Type;

            id = ID;

        }

        public void SetText(int width)
        {

            if (text.Count == 0)
            {

                text[0] = id;

            }

            textMeasures = new();

            readoutLength = 0;

            foreach (KeyValuePair<int, string> textIndex in text)
            {

                textParse[textIndex.Key] = Game1.parseText(textIndex.Value, Game1.dialogueFont, width);

                if(type == contentTypes.textsmall)
                {

                    textMeasures[textIndex.Key] = Game1.dialogueFont.MeasureString(textParse[textIndex.Key]) * 0.8f;

                }
                else
                {

                    textMeasures[textIndex.Key] = Game1.dialogueFont.MeasureString(textParse[textIndex.Key]);

                }

                if (!textColours.ContainsKey(textIndex.Key))
                {

                    textColours[textIndex.Key] = defaultColour;

                }

                if(readoutPace > 0)
                {

                    readoutLength += textParse[textIndex.Key].Length;

                }

            }

        }


        public void SetIcon()
        {

            iconSources = new();

            foreach (KeyValuePair<int, IconData.displays> icon in icons)
            {

                iconSources.Add(icon.Key, IconData.DisplayRectangle(icon.Value));

            }

        }

        public void setBounds(int index, int xP, int yP, int width, int height, int column = 0, int row = 0)
        {

            SetText(width);

            SetIcon();

            switch (type)
            {

                case contentTypes.list:

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

                    bounds = new Rectangle(xP + 16, yP + 16 + (index * (56 + row)), width, 56);

                    return;

                case contentTypes.potion:

                    bounds = new Rectangle(xP + 16 + 4 + (index % 6) * ((width - 32) / 6), yP + 16 + (int)(index / 6) * (146 + row), (width - 32) / 6 - 8, 146);

                    if (!textureColours.ContainsKey(0))
                    {

                        textureColours[0] = Color.Wheat;

                    }

                    if (!textureColours.ContainsKey(1))
                    {

                        textureColours[1] = Color.White;

                    }

                    return;

                case contentTypes.toggle:

                    bounds = new Rectangle(xP + 16 + 4 + (index % 6) * ((width - 32) / 6), yP + 16 + 146 + (int)(index / 6) * (146 + 56), (width - 32) / 6 - 8, 56);

                    return;

                case contentTypes.toggleleft:

                    bounds = new Rectangle(xP + 16 + 4 + (index % 6) * ((width - 32) / 6), yP + 16 + 146 + (int)(index / 6) * (146 + 56), (width - 32) / 12 - 8, 56);

                    return;

                case contentTypes.toggleright:

                    bounds = new Rectangle(xP + 16 + 4 + (int)(((index % 6)+ 0.5f) * ((width - 32) / 6)), yP + 16 + 146 + (int)(index / 6) * (146 + 56), (width - 32) / 12 - 8, 56);

                    return;

                case contentTypes.relic:

                    bounds = new Rectangle(xP + 16 + 4 + (index % 6) * ((width - 32) / 6), yP + 16 + (int)(index / 6) * (146 + row), (width - 32) / 6 - 8, 146);

                    return;

                case contentTypes.herolist:

                    bounds = new Rectangle(xP + 16, yP + 16 + index * ((height - 32) / 4), width - 32, (height - 32) / 4 + 4);

                    return;

                case contentTypes.goods:

                    bounds = new Rectangle(xP + 16 + 4 + (index % 4) * ((width - 32) / 4), yP + 24 + (int)(index / 4) * (224 + 56), ((width - 32) / 4) - 8, 224);

                    return;

                case contentTypes.machine:

                    bounds = new Rectangle(xP + 16 + 4 + (index % 4) * ((width - 32) / 4), yP + 24 + (int)(index / 4) * (224 + 4), ((width - 32) / 4) - 8, 224);

                    return;

                case contentTypes.estimate:

                    bounds = new Rectangle(xP + 16 + 4 + (index % 4) * ((width - 32) / 4), yP + 24 + (int)(index / 4) * (224 + 4), ((width - 32) / 4) - 8, 112);

                    return;

                case contentTypes.order:

                    bounds = new Rectangle(xP + 16, yP + 16 + index * ((height - 32) / 4), width - 32, (height - 32) / 4 + 4);

                    textParse[1] = Game1.parseText(text[1], Game1.smallFont, width-384);

                    textMeasures[1] = Game1.dialogueFont.MeasureString(textParse[1]);

                    return;

                case contentTypes.battle:

                    bounds = new Rectangle(xP + 20 + (index % 3) * ((width - 36) / 3), yP + 16 + ((int)(index / 3) * 88), ((width - 36) / 3) - 8, 80);

                    return;

                case contentTypes.battlereadout:

                    bounds = new Rectangle(xP + 20, yP, width - 40, 172);

                    SetText(width - 64);

                    return;

                case contentTypes.battlereturn:

                    bounds = new Rectangle(xP + width - 20 - ((width - 36) / 3), yP - 144, ((width - 36) / 3) - 8, 80);

                    return;

            }

        }

        public void draw(SpriteBatch b, Vector2 offset, bool focus = false)
        {

            switch (type)
            {

                case contentTypes.list:

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

                    return;

                case contentTypes.portrait:

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        bounds.X,
                        bounds.Y,
                        bounds.Width,
                        bounds.Height,
                        Color.Wheat,
                        4f,
                        false,
                        -1.2f
                    );
                    float portraitScale;

                    switch (textureSources[0].Width)
                    {
                        
                        case 64:
                            portraitScale = 4f;
                            break;

                        default:
                            portraitScale = 256f / textureSources[0].Width;
                            break;

                    };

                    b.Draw(
                        textures[0], 
                        new Vector2(bounds.Center.X, bounds.Center.Y), 
                        textureSources[0], 
                        Color.White, 
                        0f, 
                        new Vector2(textureSources[0].Width/2, textureSources[0].Height/2), 
                        portraitScale, 
                        0, 
                        0.901f
                    );

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

                case contentTypes.textsmall:

                    b.DrawString(
                        Game1.dialogueFont,
                        textParse[0],
                        new Vector2(bounds.Left - 1f, offset.Y + bounds.Top + 1f),
                        Microsoft.Xna.Framework.Color.Brown * 0.35f,
                        0f,
                        Vector2.Zero,
                        0.8f,
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
                        0.8f,
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

                    b.Draw(Mod.instance.iconData.potionsTexture, new Vector2(bounds.Center.X + 2f, bounds.Center.Y - 20f + 4f), textureSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, new Vector2(10), 4f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.potionsTexture, new Vector2(bounds.Center.X, bounds.Center.Y - 20f), textureSources[0], Color.White, 0f, new Vector2(10), 4f, 0, 0.901f);

                    if(textureSources.Count > 1)
                    {

                        int glitterTime = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 2000) / 100;

                        if(glitterTime <= 7)
                        {

                            b.Draw(Mod.instance.iconData.impactsTexture, new Vector2(bounds.Center.X, bounds.Center.Y), new Rectangle(0 + (glitterTime * 64), 128, 64, 64), Color.White * 0.5f, (float)(Math.PI/2), new Vector2(32), 4f, 0, 0.901f);

                        }

                    }

                    return;

                case contentTypes.toggle:
                case contentTypes.toggleleft:
                case contentTypes.toggleright:

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

                    b.Draw(Mod.instance.iconData.relicsTexture, new Vector2(bounds.Center.X - 40f + 2f, bounds.Center.Y - 40f + 4f), textureSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, Vector2.Zero, 4f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.relicsTexture, new Vector2(bounds.Center.X - 40f, bounds.Center.Y - 40f), textureSources[0], textureColours[0], 0f, Vector2.Zero, 4f, 0, 0.901f);

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
                            new Vector2(bounds.Right - 200 + 1f, bounds.Center.Y - 1f),
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
                            new Vector2(bounds.Right - 200, bounds.Center.Y - 4f),
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

                    if (textParse.Count > 1)
                    {

                        b.DrawString(
                            Game1.dialogueFont,
                            textParse[0],
                            new Vector2(bounds.Left + 192f + 1f, bounds.Center.Y - textMeasures[0].Y - 4f + 1.5f),
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
                            new Vector2(bounds.Left + 192f, bounds.Center.Y - textMeasures[0].Y - 4f),
                            defaultColour,
                            0f,
                            Vector2.Zero,
                            1f,
                            SpriteEffects.None,
                            0.901f
                        );

                        b.DrawString(
                            Game1.dialogueFont,
                            textParse[1],
                            new Vector2(bounds.Left + 192f + 1f, bounds.Center.Y + 4f + 1.5f),
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
                            new Vector2(bounds.Left + 192f, bounds.Center.Y + 4f),
                            defaultColour,
                            0f,
                            Vector2.Zero,
                            1f,
                            SpriteEffects.None,
                            0.901f
                        );

                        b.DrawString(
                            Game1.dialogueFont,
                            textParse[2],
                            new Vector2(bounds.Right - 160 + 1f, bounds.Center.Y - (textMeasures[1].Y / 2) + 1.5f),
                            Microsoft.Xna.Framework.Color.Brown * 0.35f,
                            0f,
                            Vector2.Zero,
                            1f,
                            SpriteEffects.None,
                            0.900f
                        );

                        b.DrawString(
                            Game1.dialogueFont,
                            textParse[2],
                            new Vector2(bounds.Right - 160, bounds.Center.Y - (textMeasures[1].Y / 2)),
                            defaultColour,
                            0f,
                            Vector2.Zero,
                            1f,
                            SpriteEffects.None,
                            0.901f
                        );

                    }
                    else
                    {


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


                    }


                    // image

                    if (textures.Count > 0)
                    {

                        b.Draw(
                            textures[0],
                            new Vector2(bounds.Left + 96f, bounds.Center.Y - 4f),
                            textureSources[0],
                            Microsoft.Xna.Framework.Color.White,
                            0f,
                            new Vector2(textureSources[0].Width / 2, textureSources[0].Height / 2),
                            5f,
                            0,
                            0.900f
                        );

                    }
                    else
                    {

                        b.Draw(
                            Mod.instance.iconData.displayTexture,
                            new Vector2(bounds.Left + 96f, bounds.Center.Y - 4f),
                            IconData.DisplayRectangle(IconData.displays.question),
                            Microsoft.Xna.Framework.Color.White,
                            0f,
                            new Vector2(8),
                            5f,
                            0,
                            0.900f
                        );

                    }

                    return;

                case contentTypes.goods:

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

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Center.X + 6 - (textMeasures[0].X * 0.9f / 2) - 1f, bounds.Center.Y + 60 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Center.X + 6 - (textMeasures[0].X * 0.9f / 2), bounds.Center.Y + 60), textColours[0], 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.901f);

                    b.Draw(Mod.instance.iconData.workshopTexture, new Vector2(bounds.Center.X + 2f, bounds.Center.Y - 16f + 4f), textureSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, new Vector2(16), 4f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.workshopTexture, new Vector2(bounds.Center.X, bounds.Center.Y - 16f), textureSources[0], Color.White, 0f, new Vector2(16), 4f, 0, 0.901f);

                    if(text.Count > 1)
                    {

                        b.DrawString(Game1.dialogueFont, textParse[1], new Vector2(bounds.Left + 12f, bounds.Bottom + 8f + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.900f);

                        b.DrawString(Game1.dialogueFont, textParse[1], new Vector2(bounds.Left + 12f, bounds.Bottom + 8f), textColours[1], 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.901f);

                        b.DrawString(Game1.dialogueFont, textParse[2], new Vector2(bounds.Right + 8f - textMeasures[2].X - 1f, bounds.Bottom + 9f + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.900f);

                        b.DrawString(Game1.dialogueFont, textParse[2], new Vector2(bounds.Right + 8f - textMeasures[2].X, bounds.Bottom + 9f), textColours[2], 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.901f);

                    }

                    return;

                case contentTypes.machine:

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

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Center.X + 6 - (textMeasures[0].X * 0.9f / 2) - 1f, bounds.Center.Y + 60 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Center.X + 6 - (textMeasures[0].X * 0.9f / 2), bounds.Center.Y + 60), textColours[0], 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.901f);

                    b.Draw(Mod.instance.iconData.workshopTexture, new Vector2(bounds.Center.X + 2f, bounds.Center.Y - 16f + 4f), textureSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, new Vector2(16), 4f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.workshopTexture, new Vector2(bounds.Center.X, bounds.Center.Y - 16f), textureSources[0], Color.White, 0f, new Vector2(16), 4f, 0, 0.901f);

                    return;

                case contentTypes.estimate:

                    b.Draw(Mod.instance.iconData.workshopTexture, new Vector2(bounds.Left + 64f + 2f, bounds.Center.Y + 4f), textureSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, new Vector2(16), 3f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.workshopTexture, new Vector2(bounds.Left + 64f, bounds.Center.Y), textureSources[0], Color.White, 0f, new Vector2(16), 3f, 0, 0.901f);

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Right - 16f - (textMeasures[0].X * 0.9f) - 1f, bounds.Center.Y + 4 - textMeasures[0].Y + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Right - 16f - (textMeasures[0].X * 0.9f), bounds.Center.Y + 4 - textMeasures[0].Y), textColours[0], 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.901f);

                    b.DrawString(Game1.dialogueFont, textParse[1], new Vector2(bounds.Right - 16f - (textMeasures[1].X * 0.9f) - 1f, bounds.Center.Y + 8 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.dialogueFont, textParse[1], new Vector2(bounds.Right - 16f - (textMeasures[1].X * 0.9f), bounds.Center.Y + 8), textColours[1], 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.901f);

                    return;

                case contentTypes.order:

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

                    b.Draw(Mod.instance.iconData.workshopTexture, new Vector2(bounds.Left + 64f + 1f, bounds.Center.Y + 4f), textureSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, new Vector2(16), 2.8f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.workshopTexture, new Vector2(bounds.Left + 64f, bounds.Center.Y), textureSources[0], Color.White, 0f, new Vector2(16), 2.8f, 0, 0.901f);

                    b.Draw(Mod.instance.iconData.relicsTexture, new Vector2(bounds.Left + 148f + 2f, bounds.Top + 40 + 1f), textureSources[1], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, new Vector2(10), 1.8f, 0, 0.900f);

                    b.Draw(Mod.instance.iconData.relicsTexture, new Vector2(bounds.Left + 148f, bounds.Top + 40), textureSources[1], Color.White, 0f, new Vector2(10), 1.8f, 0, 0.901f);

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Left + 176f - 1f, bounds.Top + 24 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Left + 176f, bounds.Top + 24), textColours[0], 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.901f);

                    b.DrawString(Game1.smallFont, textParse[1], new Vector2(bounds.Left + 132f - 1f, bounds.Top + 68 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.smallFont, textParse[1], new Vector2(bounds.Left + 132f, bounds.Top + 68), textColours[1], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

                    b.DrawString(Game1.smallFont, textParse[4], new Vector2(bounds.Right - 208f - 1f, bounds.Top + 20 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.smallFont, textParse[4], new Vector2(bounds.Right - 208f, bounds.Top + 20), textColours[4], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

                    b.DrawString(Game1.dialogueFont, textParse[2], new Vector2(bounds.Right - 208f - 1f, bounds.Top + 60 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.dialogueFont, textParse[2], new Vector2(bounds.Right - 208f, bounds.Top + 60), textColours[2], 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.901f);

                    b.DrawString(Game1.smallFont, textParse[3], new Vector2(bounds.Right - 208f - 1f, bounds.Top + 108 + 1f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.smallFont, textParse[3], new Vector2(bounds.Right - 208f, bounds.Top + 108), textColours[3], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

                    return;

                case contentTypes.battlereturn:
                case contentTypes.battle:

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        bounds.X,
                        bounds.Y,
                        bounds.Width,
                        bounds.Height,
                        focus ? Color.Wheat : Color.White,
                        3f,
                        false,
                        -1f
                    );

                    if (icons.Count > 0)
                    {

                        b.Draw(
                            Mod.instance.iconData.displayTexture,
                            new Vector2(bounds.Left + 49, bounds.Center.Y + 2f),
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
                            new Vector2(bounds.Left +48, bounds.Center.Y),
                            iconSources[0],
                            Microsoft.Xna.Framework.Color.White,
                            0f,
                            new Vector2(8),
                            3f,
                            0,
                            0.901f
                        );

                    }

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Left + 95, bounds.Center.Y - (textMeasures[0].Y / 2) + 2f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.dialogueFont, textParse[0], new Vector2(bounds.Left + 96, bounds.Center.Y - (textMeasures[0].Y / 2)), textColours[0], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

                    return;

                case contentTypes.battlereadout:

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        bounds.X,
                        bounds.Y,
                        bounds.Width,
                        bounds.Height,
                        Color.White,
                        4f,
                        false,
                        -1f
                    );

                    if(text.Count == 0)
                    {

                        return;

                    }

                    string readoutTruncate = textParse[0];

                    if (readoutPace > 0 && readout < readoutLength)
                    {

                        //readoutTick++;

                        //if(readoutTick >= readoutPace)
                        //{

                            readout++;

                        //    readoutTick = 0;

                        //}

                        readoutTruncate = textParse[0].Substring(0, readout);

                    }

                    Vector2 readoutVector1 = new(textMeasures[0].X * 0.5f, textMeasures[0].Y * 0.5f);

                    b.DrawString(Game1.dialogueFont, readoutTruncate, bounds.Center.ToVector2() - readoutVector1 + new Vector2(-1,2), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

                    b.DrawString(Game1.dialogueFont, readoutTruncate, bounds.Center.ToVector2() - readoutVector1, textColours[0], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

                    return;


                case contentTypes.battleportrait:

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        bounds.X,
                        bounds.Y,
                        bounds.Width,
                        bounds.Height,
                        Color.Wheat,
                        4f,
                        false,
                        -1.2f
                    );

                    b.Draw(
                        textures[0],
                        new Vector2(bounds.Center.X, bounds.Center.Y),
                        textureSources[0],
                        Color.White,
                        0f,
                        new Vector2(textureSources[0].Width / 2, textureSources[0].Height / 2),
                        4f,
                        0,
                        0.901f
                    );

                    return;


                case contentTypes.lovebar:

                    int loveAmount = Convert.ToInt32(text[0]);

                    Rectangle loveFull = IconData.DisplayRectangle(IconData.displays.fullheart);

                    int loveSet = 0;

                    for(int i = 0; i < (loveAmount / 2); i++)
                    {

                        b.Draw(
                             Mod.instance.iconData.displayTexture,
                             new Vector2(bounds.Center.X, bounds.Center.Y + loveSet),
                             loveFull,
                             Color.White,
                             0f,
                             new Vector2(10,10),
                             3f,
                             0,
                             0.901f
                         );

                        loveSet += 70;

                    }

                    if (loveAmount % 2 == 1)
                    {

                        Rectangle loveHalf = IconData.DisplayRectangle(IconData.displays.halfheart);

                        b.Draw(
                             Mod.instance.iconData.displayTexture,
                             new Vector2(bounds.Center.X, bounds.Center.Y + loveSet),
                             loveHalf,
                             Color.White,
                             0f,
                             new Vector2(10, 10),
                             3f,
                             0,
                             0.901f
                         );

                    }

                    return;

            }

        }

    }

}
