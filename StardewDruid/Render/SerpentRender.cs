using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Handle;
using StardewValley;
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

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> transitionFrames = new();

        public float hoverFloat;
        public int hoverMoment;
        public int hoverLapse;
        public int hoverLapseTwo;
        public int hoverLapseThree;
        public int hoverOffset;
        public float hoverAdjust;

        public bool inflight;

        public SerpentRender(CharacterHandle.characters entity)
        {

            serpentTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Serpentking);//CharacterHandle.CharacterTexture(entity);

            WalkFrames();

            SpecialFrames();

            DashFrames();

            ShadowFrames();

            TransitionFrames();

            hoverFloat = 0.1f;

            hoverLapse = 32;

            hoverLapseTwo = 0 - (hoverLapse * walkFrames[0].Count);

            hoverLapseThree = hoverLapse * walkFrames[0].Count * 2;

            hoverOffset = Mod.instance.randomIndex.Next(12);

        }

        public void WalkFrames()
        {

            walkFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 96, 96),
                    new Rectangle(96, 160, 96, 96),
                    new Rectangle(96, 160, 96, 96),
                    new Rectangle(0, 160, 96, 96),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 96, 96, 64),
                    new Rectangle(96, 96, 96, 64),
                    new Rectangle(96, 96, 96, 64),
                    new Rectangle(0, 96, 96, 64),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 0, 96, 96),
                    new Rectangle(96, 0, 96, 96),
                    new Rectangle(96, 0, 96, 96),
                    new Rectangle(0, 0, 96, 96),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 96, 96, 64),
                    new Rectangle(96, 96, 96, 64),
                    new Rectangle(96, 96, 96, 64),
                    new Rectangle(0, 96, 96, 64),
                }
            };

        }

        public void SpecialFrames()
        {

            specialFrames[Character.Character.specials.special] = new()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(192, 160, 96, 96),
                    new Rectangle(192, 160, 96, 96),
                    new Rectangle(192, 160, 96, 96),
                    new Rectangle(192, 160, 96, 96),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(192, 96, 128, 64),
                    new Rectangle(192, 96, 128, 64),
                    new Rectangle(192, 96, 128, 64),
                    new Rectangle(192, 96, 128, 64),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(192, 0, 96, 96),
                    new Rectangle(192, 0, 96, 96),
                    new Rectangle(192, 0, 96, 96),
                    new Rectangle(192, 0, 96, 96),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(192, 96, 128, 64),
                    new Rectangle(192, 96, 128, 64),
                    new Rectangle(192, 96, 128, 64),
                    new Rectangle(192, 96, 128, 64),
                }
            };

            specialFrames[Character.Character.specials.sweep] = new()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 416, 96, 96),
                    new Rectangle(0, 416, 96, 96),
                    new Rectangle(96, 416, 96, 96),
                    new Rectangle(96, 416, 96, 96),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 256, 96, 96),
                    new Rectangle(0, 256, 96, 96),
                    new Rectangle(96, 256, 96, 96),
                    new Rectangle(96, 256, 96, 96),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                }

            };

            specialFrames[Character.Character.specials.tackle] = new()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 416, 96, 96),
                    new Rectangle(96, 416, 96, 96),
                    new Rectangle(192, 416, 96, 96),
                    new Rectangle(288, 416, 96, 96),
                    new Rectangle(0, 416, 96, 96),
                    new Rectangle(96, 416, 96, 96),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                    new Rectangle(256, 352, 128, 64),
                    new Rectangle(384, 352, 128, 64),
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 256, 96, 96),
                    new Rectangle(96, 256, 96, 96),
                    new Rectangle(192, 256, 96, 96),
                    new Rectangle(288, 256, 96, 96),
                    new Rectangle(0, 256, 96, 96),
                    new Rectangle(96, 256, 96, 96),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                    new Rectangle(256, 352, 128, 64),
                    new Rectangle(384, 352, 128, 64),
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                }

            };
        }

        public void DashFrames()
        {

            dashFrames[Character.Character.dashes.dash] = new()
            {

                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 416, 96, 96),
                    new Rectangle(96, 416, 96, 96),
                    new Rectangle(192, 416, 96, 96),
                },
                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                    new Rectangle(256, 352, 128, 64),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 256, 96, 96),
                    new Rectangle(96, 256, 96, 96),
                    new Rectangle(192, 256, 96, 96),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                    new Rectangle(256, 352, 128, 64),
                },

                [4] = new List<Rectangle>()
                {
                    new Rectangle(288, 416,128, 96),
                },
                [5] = new List<Rectangle>()
                {
                    new Rectangle(384, 352, 128, 64),
                },
                [6] = new List<Rectangle>()
                {
                    new Rectangle(288, 256, 128, 96),
                },
                [7] = new List<Rectangle>()
                {
                    new Rectangle(384, 352, 128, 64),
                },

                [8] = new List<Rectangle>()
                {
                    new Rectangle(0, 416, 96, 96),
                    new Rectangle(96, 416, 96, 96),
                },
                [9] = new List<Rectangle>()
                {
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                },
                [10] = new List<Rectangle>()
                {
                    new Rectangle(0, 256, 96, 96),
                    new Rectangle(96, 256, 96, 96),
                },
                [11] = new List<Rectangle>()
                {
                    new Rectangle(0, 352, 128, 64),
                    new Rectangle(128, 352, 128, 64),
                },

            };

        }

        public void ShadowFrames()
        {

            shadowFrames = new()
            {

                [0] = new Rectangle(416, 0, 64, 64),

            };

        }

        public void TransitionFrames()
        {

            transitionFrames = new()
            {

                [0] = new Rectangle(192, 160, 96, 96),

                [1] = new Rectangle(192, 96, 128, 64),

                [2] = new Rectangle(192, 0, 96, 96),

                [3] = new Rectangle(192, 96, 128, 64),

            };

        }

        public int HoverFrame()
        {

            return Math.Min((int)(hoverMoment / hoverLapse), 3);

        }

        public void Update(bool adjust = false)
        {

            hoverOffset++;

            hoverMoment = Math.Abs(hoverLapseTwo + hoverOffset);

            if (hoverOffset >= hoverLapseThree)
            {

                hoverOffset = 0;

            }

            if (adjust)
            {

                hoverAdjust = (hoverLapseTwo + hoverMoment) * hoverFloat;

            }
            else if (hoverAdjust > 0)
            {

                hoverAdjust -= hoverFloat;

            }

        }

        public void DrawNormal(SpriteBatch b, SerpentRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            Microsoft.Xna.Framework.Rectangle shadowSource;

            bool flight = false;

            Vector2 usePosition = use.position + new Vector2(0, hoverAdjust*use.scale);

            switch (use.series)
            {

                default:
                case SerpentRenderAdditional.serpentseries.none:
                case SerpentRenderAdditional.serpentseries.hover:

                    use.frame = HoverFrame();

                    source = walkFrames[use.direction][use.frame];

                    shadowSource = shadowFrames[0];

                    break;

                case SerpentRenderAdditional.serpentseries.special:

                    source = specialFrames[Character.Character.specials.special][use.direction][use.frame];

                    shadowSource = shadowFrames[0];

                    break;

                case SerpentRenderAdditional.serpentseries.sweep:

                    source = specialFrames[Character.Character.specials.sweep][use.direction][use.frame];

                    shadowSource = source;

                    flight = true;

                    break;

                case SerpentRenderAdditional.serpentseries.tackle:

                    source = specialFrames[Character.Character.specials.tackle][use.direction][use.frame];

                    shadowSource = source;

                    flight = true;

                    break;

                case SerpentRenderAdditional.serpentseries.dash:

                    source = dashFrames[Character.Character.dashes.dash][use.direction][use.frame];

                    shadowSource = source;

                    flight = true;

                    break;

            }


            if (use.frame == 0)
            {

                if (flight)
                {
                    
                    if (!inflight)
                    { 
                    
                        source = transitionFrames[use.direction];
                        
                        shadowSource = source;

                    }


                }
                else
                {

                    if (inflight)
                    {

                        source = transitionFrames[use.direction];

                        shadowSource = source;

                    }


                }


            } 
            else
            {

                inflight = flight;

            }

            Vector2 shadowPosition = use.position + new Vector2(0, use.scale * source.Height / 2);

            b.Draw(
                serpentTexture,
                shadowPosition,
                shadowSource,
                Color.Black * 0.2f,
                0f,
                new Vector2(shadowSource.Width / 2, shadowSource.Height / 2),
                use.scale * 0.75f,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer - 0.0001f
            );

            b.Draw(
                serpentTexture,
                usePosition,
                source,
                Microsoft.Xna.Framework.Color.White * use.fade,
                0f,
                new Vector2(source.Width / 2, source.Height / 2),
                use.scale,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer + 0.0002f
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
            sweep,
            tackle,
        }

        public serpentseries series;

        public float fade = 1f;

    }

}
