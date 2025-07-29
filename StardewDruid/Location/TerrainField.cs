using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace StardewDruid.Location
{

    public class TerrainField
    {

        public Microsoft.Xna.Framework.Vector2 position = Vector2.Zero;

        public List<Microsoft.Xna.Framework.Vector2> baseTiles = new();

        public Microsoft.Xna.Framework.Vector2 center;

        public float girth;

        public int clearance;

        public int backing;

        public Microsoft.Xna.Framework.Rectangle source = Microsoft.Xna.Framework.Rectangle.Empty;

        public Microsoft.Xna.Framework.Rectangle bounds = Microsoft.Xna.Framework.Rectangle.Empty;

        public int index;

        public float layer;

        public bool flip;

        public bool special;

        public IconData.tilesheets tilesheet;

        public int where;

        public int shake;

        public bool shaked;

        public float wind;

        public float windout;

        public float fade = 1f;

        public float fadeout = 0.75f;

        public float shade = 0.3f;

        public Microsoft.Xna.Framework.Vector2 shadeOffset;

        public Microsoft.Xna.Framework.Color color;

        public bool disabled;

        public bool ruined;

        public enum shadows
        {
            none,
            offset,
            deepset,
            circle,
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

                backing = 72;

            }

            bounds = new((int)position.X + 8, (int)position.Y, source.Width * 4 - 16, source.Height * 4 - backing);

            layer = LocationHandle.TerrainLayers(tilesheet, index, position, source);

        }

        public virtual void drawFront(SpriteBatch b, GameLocation location)
        {

        }

        public virtual void update(GameLocation location)
        {

            if (disabled)
            {

                return;

            }

            Fadeout(location);

        }

        public virtual void Fadeout(GameLocation location)
        {

            fade = 1f;

            if (fadeout == 1f)
            {

                return;

            }

            foreach (Farmer character in location.farmers)
            {

                if (bounds.Contains(character.Position.X + 32, character.Position.Y + 32))
                {

                    fade = fadeout;

                }

            }

            if (fade == 1f)
            {

                foreach (NPC character in location.characters)
                {

                    if (bounds.Contains(character.Position.X + 32, character.Position.Y + 32))
                    {

                        fade = fadeout;
                    }

                }

            }

        }


        public virtual bool DrawCheck()
        {

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), source.Height * 8 + 128))
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

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            DrawShadow(b, origin, source, fade, shadow);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

        public virtual void DrawShadow(SpriteBatch b, Vector2 origin, Rectangle useSource, float opacity, shadows useShadow)
        {

            switch (useShadow)
            {

                case shadows.none:

                    return;

                default:
                case shadows.offset:

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(1, 6), useSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    break;

                case shadows.deepset:

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(2, 10), useSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    break;

                case shadows.circle:

                    b.Draw(Mod.instance.iconData.shadowTexture, origin + new Vector2(useSource.Width * 2, useSource.Height * 4 - 24), Mod.instance.iconData.shadowRectangle, Color.White * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    break;

            }

        }

        public virtual void DrawReflection(SpriteBatch b, Vector2 origin, Rectangle useSource, float opacity)
        {

            int heightDivide = 32;

            int heightMiddle = 16;

            float fadeIncrement = 0.5f;

            if(useSource.Height < 32)
            {

                heightDivide = 16;

                heightMiddle = 8;
                
                fadeIncrement = 1f;

            }

            int widthDivide = 32;

            int widthMiddle = 16;

            if (useSource.Width < 32)
            {

                widthDivide = 16;

                widthMiddle = 8;

                fadeIncrement = 1f;

            }

            int heightIncrement = useSource.Height / heightDivide;

            int widthIncrement = useSource.Width / widthDivide;

            Vector2 reflectOut = new(widthIncrement / 2, heightIncrement / 2);

            Vector2 reflectOrigin = new Vector2(origin.X, origin.Y);

            if (flip)
            {

                reflectOrigin.X += (useSource.Width * 4);

                reflectOrigin.X -= widthIncrement * 2;

            }
            else
            {

                reflectOrigin.X += widthIncrement * 2;

            }

            Rectangle reflectSource = new(useSource.X, useSource.Y + useSource.Height, widthIncrement, heightIncrement);

            Rectangle reflectUse = new(useSource.X, useSource.Y + useSource.Height, widthIncrement, heightIncrement);

            float widthFade = 0f;
            
            float heightFade = 0f;

            int widthUp = widthIncrement * 4;

            int heightUp = heightIncrement * 4;
            
            for (int w = 0; w < widthDivide; w++)
            {

                if (w < widthMiddle)
                {

                    widthFade += fadeIncrement;

                }
                else
                {

                    widthFade -= fadeIncrement;

                }

                reflectOrigin.Y = origin.Y;

                reflectUse.Y = reflectSource.Y;

                for (int h = 0; h < heightDivide; h++)
                {

                    reflectOrigin.Y += heightUp;

                    reflectUse.Y -= heightIncrement;

                    if (h < heightMiddle)
                    {

                        heightFade += fadeIncrement;

                    }
                    else
                    {

                        heightFade -= fadeIncrement;

                    }

                    float reflectFade = (widthFade * heightFade) / 100f;

                    if (flip)
                    {

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], reflectOrigin, reflectUse, Color.White * reflectFade, (float)Math.PI, reflectOut, 4, 0, layer + 0.002f);


                    }
                    else
                    {

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], reflectOrigin, reflectUse, Color.White * reflectFade, 0, reflectOut, 4, (SpriteEffects)2, layer + 0.002f);


                    }

                }

                reflectUse.X += widthIncrement;

                if (flip)
                {

                    reflectOrigin.X -= widthUp;

                }
                else
                {

                    reflectOrigin.X += widthUp;

                }

            }

        }

    }

}
