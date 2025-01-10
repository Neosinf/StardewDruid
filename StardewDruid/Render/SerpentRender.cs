using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StardewDruid.Render
{
    public class SerpentRender
    {

        public Texture2D serpentTexture;

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> walkFrames = new();

        public Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> specialFrames = new();

        public Dictionary<Character.Character.dashes, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> dashFrames = new();

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> shadowFrames = new();

        public SerpentRender(string name)
        {

            serpentTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", name+".png"));

            WalkFrames();

            SpecialFrames();

            DashFrames();

            ShadowFrames();

        }

        public void WalkFrames()
        {

            walkFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 64, 64, 32),
                    new Rectangle(64, 64, 64, 32),
                    new Rectangle(128, 64, 64, 32),
                    new Rectangle(64, 64, 64, 32),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 32, 64, 32),
                    new Rectangle(64, 32, 64, 32),
                    new Rectangle(128, 32, 64, 32),
                    new Rectangle(64, 32, 64, 32),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 0, 64, 32),
                    new Rectangle(64, 0, 64, 32),
                    new Rectangle(128, 0, 64, 32),
                    new Rectangle(64, 0, 64, 32),

                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 32, 64, 32),
                    new Rectangle(64, 32, 64, 32),
                    new Rectangle(128, 32, 64, 32),
                    new Rectangle(64, 32, 64, 32),
                }
            };

        }

        public void SpecialFrames()
        {

            specialFrames[Character.Character.specials.special] = new()
            {

                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 64, 64, 32),
                    new Rectangle(64, 64, 64, 32),
                    new Rectangle(128, 64, 64, 32),
                    new Rectangle(64, 64, 64, 32),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 32, 64, 32),
                    new Rectangle(64, 32, 64, 32),
                    new Rectangle(128, 32, 64, 32),
                    new Rectangle(64, 32, 64, 32),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 0, 64, 32),
                    new Rectangle(64, 0, 64, 32),
                    new Rectangle(128, 0, 64, 32),
                    new Rectangle(64, 0, 64, 32),

                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 32, 64, 32),
                    new Rectangle(64, 32, 64, 32),
                    new Rectangle(128, 32, 64, 32),
                    new Rectangle(64, 32, 64, 32),
                }

            };

        }

        public void DashFrames()
        {

            dashFrames[Character.Character.dashes.dash] = new()
            {

                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 96, 64, 64),
                    new Rectangle(64, 96, 64, 64),
                    new Rectangle(128, 96, 64, 64),
                },
                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 96, 64),
                    new Rectangle(96, 160, 96, 64),
                    new Rectangle(0, 224, 96, 64),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 96, 64, 64),
                    new Rectangle(64, 96, 64, 64),
                    new Rectangle(128, 96, 64, 64),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 96, 64),
                    new Rectangle(96, 160, 96, 64),
                    new Rectangle(0, 224, 96, 64),
                },

                [4] = new List<Rectangle>()
                {
                    new Rectangle(64, 96, 64, 64),
                },
                [5] = new List<Rectangle>()
                {
                    new Rectangle(96, 160, 96, 64),
                },
                [6] = new List<Rectangle>()
                {
                    new Rectangle(64, 96, 64, 64),
                },
                [7] = new List<Rectangle>()
                {
                    new Rectangle(96, 160, 96, 64),
                },

                [8] = new List<Rectangle>()
                {
                    new Rectangle(128, 96, 64, 64),
                    new Rectangle(64, 96, 64, 64),
                },
                [9] = new List<Rectangle>()
                {
                    new Rectangle(0, 224, 96, 64),
                    new Rectangle(96, 160, 96, 64),
                },
                [10] = new List<Rectangle>()
                {
                    new Rectangle(128, 96, 64, 64),
                    new Rectangle(64, 96, 64, 64),
                },
                [11] = new List<Rectangle>()
                {
                    new Rectangle(0, 224, 96, 64),
                    new Rectangle(96, 160, 96, 64),
                }

            };

        }

        public void ShadowFrames()
        {

            shadowFrames = new()
            {

                [0] = new Rectangle(96, 224, 64, 64),

                [1] = new Rectangle(0, 288, 96, 32),

                [2] = new Rectangle(96, 288, 64, 32),

            };

        }

        public void DrawNormal(SpriteBatch b, SerpentRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            float rotate = 0f;

            Microsoft.Xna.Framework.Rectangle shadowSource;

            switch (use.series)
            {

                default:
                case SerpentRenderAdditional.serpentseries.hover:

                    source = walkFrames[use.direction][use.frame];

                    shadowSource = shadowFrames[2];

                    break;

                case SerpentRenderAdditional.serpentseries.special:

                    source = specialFrames[Character.Character.specials.special][use.direction][use.frame];

                    shadowSource = shadowFrames[2];

                    break;

                case SerpentRenderAdditional.serpentseries.dash:

                    source = dashFrames[Character.Character.dashes.dash][use.direction][use.frame];

                    shadowSource = shadowFrames[1];

                    if (use.direction % 4 == 0)
                    {

                        if (use.flip)
                        {
                            rotate = (float)(Math.PI / 2);
                        }
                        else
                        {
                            rotate = (float)(Math.PI * 3 / 2);
                        }

                        shadowSource = shadowFrames[0];

                    }
                    else if(use.direction % 2 == 0)
                    {

                        shadowSource = shadowFrames[0];

                    }

                    break;

            }

            b.Draw(
                serpentTexture,
                use.position,
                source,
                Microsoft.Xna.Framework.Color.White * use.fade,
                rotate,
                new Vector2(source.Width / 2, source.Height / 2),
                use.scale,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer + 0.0002f
            );

            Vector2 shadowPosition = use.position + new Vector2(0, use.scale * 18);

            b.Draw(
                serpentTexture,
                shadowPosition,
                shadowSource,
                Color.White * 0.2f,
                rotate,
                new Vector2(shadowSource.Width / 2, shadowSource.Height / 2),
                use.scale * 0.75f,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer - 0.0001f
            );

        }

    }

    public class SerpentRenderAdditional
    {

        public int direction = 2;

        public int frame = 0;

        public Vector2 position;

        public float scale = 4f;

        public bool flip = false;

        public float layer = 0.0001f;

        public enum serpentseries
        {
            none,
            hover,
            dash,
            special,
        }

        public serpentseries series;

        public float fade = 1f;

    }

}
