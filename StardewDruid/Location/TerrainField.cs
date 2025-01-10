using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location
{

    public class TerrainField
    {

        public Vector2 position = Vector2.Zero;

        public List<Vector2> baseTiles = new();

        public Vector2 center;

        public float girth;

        public int clearance;

        public Microsoft.Xna.Framework.Rectangle source = Microsoft.Xna.Framework.Rectangle.Empty;

        public Microsoft.Xna.Framework.Rectangle bounds = Microsoft.Xna.Framework.Rectangle.Empty;

        public int index;

        public float layer;

        public bool flip;

        public bool special;

        public IconData.tilesheets tilesheet;

        public int shake;

        public float fadeout;

        public float shade;

        public Vector2 shadeOffset;

        public Color color;

        public bool disabled;

        public bool ruined;

        public enum shadows
        {
            none,
            offset,
            deepset,
            smallleafy,
            leafy,
            moreleafy,
            bigleafy,
            treeleafy,
            bush,
            reflection,
        }

        public shadows shadow;

        public TerrainField()
        {

        }

        public TerrainField(IconData.tilesheets Tilesheet, int Index, Vector2 Position, shadows Shadow = shadows.offset, bool Flip = false)
        {

            tilesheet = Tilesheet;

            index = Index;

            position = Position;

            color = Color.White;

            shadow = Shadow;

            flip = Flip;

            shade = 0.3f;

            reset();

        }

        public virtual void reset()
        {

            source = LocationHandle.TerrainRectangles(tilesheet, index);

            baseTiles = LocationHandle.TerrainBases(tilesheet, index, position, source);

            if (baseTiles.Count > 0)
            {

                Vector2 corner00 = baseTiles.First() * 64;

                Vector2 corner11 = baseTiles.Last() * 64;

                center = corner00 + (corner11 - corner00) / 2;

                girth = Vector2.Distance(corner00, center);

                clearance = (int)Math.Ceiling(girth / 64);

            }

            layer = LocationHandle.TerrainLayers(tilesheet, index, position, source);

            special = LocationHandle.TerrainRules(tilesheet, index);

            shadow = LocationHandle.TerrainShadows(tilesheet, index, shadow);

            fadeout = LocationHandle.TerrainFadeout(tilesheet, index);

        }

        public virtual void drawFront(SpriteBatch b, GameLocation location)
        {

            draw(b, location);

        }

        public virtual float Fadeout(GameLocation location, Microsoft.Xna.Framework.Rectangle useSource)
        {

            float opacity = 1f;

            int backing = 0;

            if (baseTiles.Count > 0)
            {

                backing = 72;

            }

            Microsoft.Xna.Framework.Rectangle bounds = new((int)position.X + 8, (int)position.Y, useSource.Width * 4 - 16, useSource.Height * 4 - backing);

            foreach (Farmer character in location.farmers)
            {

                if (bounds.Contains(character.Position.X, character.Position.Y))
                {

                    opacity = fadeout;

                }

            }

            if (opacity == 1f)
            {

                foreach (NPC character in location.characters)
                {

                    if (bounds.Contains(character.Position.X, character.Position.Y))
                    {

                        opacity = fadeout;
                    }

                }

            }
            return opacity;

        }

        public virtual bool DrawCheck()
        {

            if (!Utility.isOnScreen(position + (source.Center.ToVector2()*2), source.Height * 8 + 128))
            {

                return false;

            }

            if (!Mod.instance.iconData.sheetTextures.ContainsKey(tilesheet))
            {

                return false;

            }

            if (Mod.instance.iconData.sheetTextures[tilesheet].IsDisposed)
            {

                return false;

            }

            if (disabled)
            {

                return false;

            }

            return true;


        }

        public virtual void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            if (special)
            {

                useSource = LocationHandle.TerrainSpecials(tilesheet, index, useSource);

                if(useSource == Rectangle.Empty)
                {

                    return;

                }

            }

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            float opacity = Fadeout(location, useSource);

            DrawShadow(b, origin, useSource, opacity);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color * opacity, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

        public virtual void DrawShadow(SpriteBatch b, Vector2 origin, Rectangle useSource, float opacity)
        {

            switch (shadow)
            {

                case shadows.offset:

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(1, 6), useSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    break;

                case shadows.deepset:

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(2, 10), useSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    break;

                case shadows.smallleafy:

                    b.Draw(Game1.mouseCursors, origin + new Vector2(useSource.Width * 2, useSource.Height * 4 - 18), new Rectangle(663, 1011, 41, 30), Color.White, 0f, new Vector2(20, 15), 3f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                    break;

                case shadows.leafy:

                    b.Draw(Game1.mouseCursors, origin + new Vector2(useSource.Width * 2, useSource.Height * 4 - 24), new Rectangle(663, 1011, 41, 30), Color.White, 0f, new Vector2(20, 15), 4f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                    break;

                case shadows.moreleafy:

                    b.Draw(Game1.mouseCursors, origin + new Vector2(useSource.Width * 2, useSource.Height * 4 - 36), new Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 7f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                    break;

                case shadows.bigleafy:

                    if (flip)
                    {

                        b.Draw(Game1.mouseCursors, origin + new Vector2(useSource.Width * 2 - 32, useSource.Height * 4 - 64), new Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 9f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                    }
                    else
                    {

                        b.Draw(Game1.mouseCursors, origin + new Vector2(useSource.Width * 2 + 32, useSource.Height * 4 - 64), new Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 9f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                    }

                    break;


                case shadows.treeleafy:

                    b.Draw(Game1.mouseCursors, origin + new Vector2(useSource.Width * 2, useSource.Height * 4 - 36) + shadeOffset, new Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 6f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                    b.Draw(Game1.mouseCursors, origin + new Vector2(useSource.Width * 2, useSource.Height * 4 - 36) + shadeOffset, new Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 10f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                    break;

                case shadows.bush:

                    b.Draw(Game1.mouseCursors_1_6, origin + new Vector2(useSource.Width * 2, useSource.Height * 4 - 18), new Rectangle(469, 298, 42, 31), Color.White, 0f, new Vector2(20, 15), 3f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                    break;

                case shadows.reflection:

                    Rectangle shadowSource = new(useSource.X, useSource.Y + (useSource.Height - 24), useSource.Width, 24);

                    //shadow
                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, 8 + (useSource.Height * 4 - 96)), shadowSource, Color.Black * 0.3f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    Rectangle reflectuseSource = new(useSource.X, useSource.Y + useSource.Height, useSource.Width, 2);

                    Vector2 reflectOrigin = new(origin.X + useSource.Width * 2, origin.Y + useSource.Height * 4 - 28);

                    Vector2 reflectOut = new(useSource.Width / 2, 1);

                    float reflectFade = 0f;

                    for (int i = 0; i < 32; i++)
                    {
                        reflectuseSource.Y -= 2;

                        reflectOrigin.Y += 8;

                        if (i >= 16)
                        {


                            reflectFade -= 0.03f;


                        }
                        else
                        {

                            reflectFade += 0.03f;


                        }

                        if (flip)
                        {

                            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], reflectOrigin, reflectuseSource, Color.White * reflectFade, (float)Math.PI, reflectOut, 4, 0, 1E-06f);

                        }
                        else
                        {

                            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], reflectOrigin, reflectuseSource, Color.White * reflectFade, 0, reflectOut, 4, (SpriteEffects)2, 1E-06f);

                        }

                    }

                    break;

            }
        }


    }

}
