using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location.Terrain
{
    public class CaveRay : TerrainField
    {

        public CaveRay(Vector2 Position, int Index, bool Flip = false)
            : base()
        {

            tilesheet = IconData.tilesheets.ray;

            index = Index;

            position = Position;

            color = Color.White;

            flip = Flip;

            reset();

        }

        public override void reset()
        {

            source = new(0, 0, 128, 64);

            layer = (position.Y + (source.Height * 4)) / 10000;

            bounds = new((int)position.X + 8, (int)position.Y, source.Width * 4 - 16 + (index * 128), source.Height * 4 * index - backing);

        }

        public override void update(GameLocation location)
        {

            if (disabled)
            {

                return;

            }

            string id = "18465_caveray_" + (position.X * 10000 + position.Y).ToString();

            if (!Game1.currentLightSources.ContainsKey(id))
            {

                Vector2 lightplacement = position + new Vector2(224, -32);

                for (int i = 0; i < index; i++)
                {

                    if (flip)
                    {

                        lightplacement += new Vector2(-128, 256);

                    }
                    else
                    {

                        lightplacement += new Vector2(128, 256);

                    }
                    
                }

                LightSource light = new LightSource(id, 4, lightplacement, 1.25f, Color.Black * 0.75f);

                Game1.currentLightSources.Add(id, light);

            }

        }

        public override void drawFront(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X  - Game1.viewport.X, position.Y - Game1.viewport.Y);

            float ray = 0.08f;

            Microsoft.Xna.Framework.Color useColour = color * ray * fade;

            Microsoft.Xna.Framework.Rectangle useRectangle = new(0, 0, 160, 64);

            Microsoft.Xna.Framework.Rectangle useRectangle2 = new(0, 64, 160, 64);

            Microsoft.Xna.Framework.Rectangle useRectangle3 = new(0, 128, 160, 64);

            Microsoft.Xna.Framework.Rectangle useRectangle4 = new(0, 192, 160, 64);

            for (int i = 1; i <= index; i++)
            {

                if (i == index)
                {

                    useRectangle = new(0, 0, 160, 4);

                    useRectangle2 = new(0, 64, 160, 4);

                    useRectangle3 = new(0, 128, 160, 4);

                    useRectangle4 = new(0, 192, 160, 4);

                    for (int j = 0; j <= 16; j++)
                    {

                        ray -= 0.005f;

                        useColour = color * ray * fade;

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useRectangle, useColour, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useRectangle2, useColour, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useRectangle3, useColour, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useRectangle4, useColour, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                        if (flip)
                        {

                            origin += new Vector2(0, 16);

                        }
                        else
                        {

                            origin += new Vector2(0, 16);

                        }

                        useRectangle.Y += 4;

                        useRectangle2.Y += 4;

                        useRectangle3.Y += 4;

                        useRectangle4.Y += 4;

                    }

                }
                else
                {

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useRectangle, useColour, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useRectangle2, useColour, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useRectangle3, useColour, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useRectangle4, useColour, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                }

                if (flip)
                {

                    origin += new Vector2(-128, 256);

                }
                else
                {

                    origin += new Vector2(128, 256);

                }

            }

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

        }


    }


}
