using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Handle;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StardewDruid.Render
{
    public class SlimeRender
    {

        public Texture2D slimeTexture;

        public enum slimeFrames
        {

        }

        public Dictionary<Character.Character.idles, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> idleFrames = new();

        public Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> specialFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> dashFrames = new();

        public List<float> specialAdjust = new();

        public List<float> sweepAdjust = new();

        public int frameWidth = 32;

        public int frameHeight = 32;

        public int shadowHeight = 96;

        public Vector2 frameOrigin;

        public float hoverFloat;

        public int hoverMoment;

        public int hoverLapse;

        public int hoverLapseTwo;

        public int hoverLapseThree;

        public int hoverOffset;

        public float hoverAdjust;

        public bool standbyTransition;

        public int transitionLapse;

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> transitionFrames = new();

        public SlimeRender(CharacterHandle.characters entity)
        {

            slimeTexture = CharacterHandle.CharacterTexture(entity);

            switch (entity)
            {

                default:

                    IdleFrames();

                    SpecialFrames();

                    DashFrames();

                    break;

                case CharacterHandle.characters.Jellyking:

                    LargeIdleFrames();

                    LargeSpecialFrames();

                    LargeDashFrames();

                    break;

            }

            frameOrigin = new(frameWidth / 2, frameHeight / 2);

            hoverFloat = 0.35f;

            hoverLapse = 10;

            hoverLapseTwo = 0 - (hoverLapse * idleFrames[Character.Character.idles.none][0].Count);

            hoverLapseThree = hoverLapse * idleFrames[Character.Character.idles.none][0].Count * 2;

            hoverOffset = Mod.instance.randomIndex.Next(12);

        }

        public void IdleFrames()
        {

            idleFrames = new();

            idleFrames[Character.Character.idles.none] = new()
            {
                [0] = new()
                {
                    new(96,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                    new(0,0,32,32),
                },
                [1] = new()
                {
                    new(96,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                    new(0,0,32,32),
                },
                [2] = new()
                {
                    new(96,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                    new(0,0,32,32),
                },
                [3] = new()
                {
                    new(96,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                    new(0,0,32,32),
                },
            };

            idleFrames[Character.Character.idles.idle] = new()
            {
                [0] = new()
                {

                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                    new(0,0,32,32),
                },
                [1] = new()
                {

                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                    new(0,0,32,32),
                },
                [2] = new()
                {

                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                    new(0,0,32,32),
                },
                [3] = new()
                {

                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                    new(0,0,32,32),
                },
            };

            idleFrames[Character.Character.idles.standby] = new()
            {

                [0] = new()
                {
                    new(64,128,32,32),
                    new(96,128,32,32),
                    new(128,128,32,32),
                    new(160,128,32,32),
                },
                [1] = new()
                {
                    new(64,128,32,32),
                    new(96,128,32,32),
                    new(128,128,32,32),
                    new(160,128,32,32),
                },
                [2] = new()
                {
                    new(64,128,32,32),
                    new(96,128,32,32),
                    new(128,128,32,32),
                    new(160,128,32,32),
                },
                [3] = new()
                {
                    new(64,128,32,32),
                    new(96,128,32,32),
                    new(128,128,32,32),
                    new(160,128,32,32),
                },
            
            };

            transitionFrames = new()
            {
                [0] = new(){
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(0,128,32,32),
                    new(32,128,32,32),
                    new(0,0,32,32),
                },
                [1] = new(){
                    new(160,0,32,32),
                    new(128,0,32,32),
                    new(96,0,32,32),
                    new(64,0,32,32),
                    new(160,0,32,32),
                },
            };

        }

        public void LargeIdleFrames()
        {

            frameWidth = 48;

            frameHeight = 64;

            shadowHeight = 320;

            idleFrames = new();

            idleFrames[Character.Character.idles.none] = new()
            {
                [0] = new()
                {
                    new(144,128,48,64),
                    new(96,128,48,64), 
                    new(48,128,48,64),
                    new(0,128,48,64),
                },
                [1] = new()
                {
                    new(144,64,48,64),
                    new(96,64,48,64),
                    new(48,64,48,64),
                    new(0,64,48,64),
                },
                [2] = new()
                {
                    new(144,0,48,64),
                    new(96,0,48,64),
                    new(48,0,48,64),
                    new(0,0,48,64),
                },
                [3] = new()
                {
                    new(144,64,48,64),
                    new(96,64,48,64),
                    new(48,64,48,64),
                    new(0,64,48,64),
                },
            };

            idleFrames[Character.Character.idles.idle] = new()
            {
                [0] = new()
                {
                    new(0,128,48,64),
                    new(48,128,48,64),
                    new(96,128,48,64),
                    new(48,128,48,64),
                    new(0,128,48,64),
                },
                [1] = new()
                {
                    new(0,64,48,64),
                    new(48,64,48,64),
                    new(96,64,48,64),
                    new(48,64,48,64),
                    new(0,64,48,64),
                },
                [2] = new()
                {
                    new(0,0,48,64),
                    new(48,0,48,64),
                    new(96,0,48,64),
                    new(48,0,48,64),
                    new(0,0,48,64),
                },
                [3] = new()
                {
                    new(0,64,48,64),
                    new(48,64,48,64),
                    new(96,64,48,64),
                    new(48,64,48,64),
                    new(0,64,48,64),
                },
            };

            idleFrames[Character.Character.idles.standby] = new()
            {

                [0] = new()
                {
                    new(96,384,48,64),
                    new(96,384,48,64),
                    new(96,384,48,64),
                    new(96,384,48,64),
                },
                [1] = new()
                {
                    new(96,384,48,64),
                    new(96,384,48,64),
                    new(96,384,48,64),
                    new(96,384,48,64),
                },
                [2] = new()
                {
                    new(96,384,48,64),
                    new(96,384,48,64),
                    new(96,384,48,64),
                    new(96,384,48,64),
                },
                [3] = new()
                {
                    new(96,384,48,64),
                    new(96,384,48,64),
                    new(96,384,48,64),
                    new(96,384,48,64),
                },

            };

            transitionFrames = new()
            {
                [0] = new(){
                    new(240,196,48,64),
                    new(0,384,48,64),
                    new(0,384,48,64),
                    new(48,384,48,64),
                    new(48,384,48,64),
                },
                [1] = new(){
                    new(240,196,48,64),
                    new(0,384,48,64),
                    new(0,384,48,64),
                    new(48,384,48,64),
                    new(48,384,48,64),
                },
            };

        }


        public void SpecialFrames()
        {

            specialFrames[Character.Character.specials.special] = new()
            {
                [0] = new()
                {
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,64,32,32),
                    new(128,64,32,32),
                    new(160,64,32,32),
                },
                [1] = new()
                {
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,64,32,32),
                    new(128,64,32,32),
                    new(160,64,32,32),
                },
                [2] = new()
                {
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,64,32,32),
                    new(128,64,32,32),
                    new(160,64,32,32),
                },
                [3] = new()
                {
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,64,32,32),
                    new(128,64,32,32),
                    new(160,64,32,32),
                },
            };

            specialAdjust = new()
            {
                5f,
                10f,
                15f,
                10f,
                5f,

            };

            specialFrames[Character.Character.specials.sweep] = new()
            {
                [0] = new()
                {
                    new(0,32,32,32),
                    new(32,32,32,32),
                    new(32,32,32,32),
                    new(64,32,32,32),

                },
                [1] = new()
                {
                    new(0,64,32,32),
                    new(32,64,32,32),
                    new(32,64,32,32),
                    new(64,64,32,32),
                },
                [2] = new()
                {
                    new(96,32,32,32),
                    new(128,32,32,32),
                    new(128,32,32,32),
                    new(160,32,32,32),
                },
                [3] = new()
                {
                    new(0,64,32,32),
                    new(32,64,32,32),
                    new(32,64,32,32),
                    new(64,64,32,32),
                },
            };

            sweepAdjust = new()
            {
                5f,
                10f,
                15f,
                5f,
            };

            specialFrames[Character.Character.specials.tackle] = new()
            {
                [0] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                    new(160,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                },
                [1] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                    new(160,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                },
                [2] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                    new(160,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                },
                [3] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                    new(160,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                },
            };

        }

        public void LargeSpecialFrames()
        {

            specialFrames[Character.Character.specials.special] = new()
            {
                [0] = new()
                {
                    new(48,128,48,64),
                    new(96,128,48,64),
                    new(144,256,48,64),
                    new(192,256,48,64),
                    new(240,256,48,64),
                },
                [1] = new()
                {
                    new(48,64,48,64),
                    new(96,64,48,64),
                    new(144,256,48,64),
                    new(192,256,48,64),
                    new(240,256,48,64),
                },
                [2] = new()
                {
                    new(48,0,48,64),
                    new(96,0,48,64),
                    new(144,256,48,64),
                    new(192,256,48,64),
                    new(240,256,48,64),
                },
                [3] = new()
                {
                    new(48,64,48,64),
                    new(96,64,48,64),
                    new(144,256,48,64),
                    new(192,256,48,64),
                    new(240,256,48,64),
                },
            };


            specialAdjust = new()
            {
                5f,
                10f,
                15f,
                10f,
                5f,

            };

            specialFrames[Character.Character.specials.sweep] = new()
            {
                [0] = new()
                {
                    new(0,192,48,64),
                    new(48,192,48,64),
                    new(48,192,48,64),
                    new(96,192,48,64),

                },
                [1] = new()
                {
                    new(0,256,48,64),
                    new(48,256,48,64),
                    new(48,256,48,64),
                    new(96,256,48,64),
                },
                [2] = new()
                {
                    new(144,192,48,64),
                    new(192,192,48,64),
                    new(192,192,48,64),
                    new(240,192,48,64),
                },
                [3] = new()
                {
                    new(0,256,48,64),
                    new(48,256,48,64),
                    new(48,256,48,64),
                    new(96,256,48,64),
                },
            };

            sweepAdjust = new()
            {
                5f,
                10f,
                15f,
                5f,
            };

            specialFrames[Character.Character.specials.tackle] = new()
            {
                [0] = new()
                {
                    new(0,128,48,64),
                    new(48,128,48,64),
                    new(96,128,48,64),
                    new(144,128,48,64),
                    new(192,128,48,64),
                    new(240,128,48,64),
                    new(96,128,48,64),
                    new(48,128,48,64),
                },
                [1] = new()
                {
                    new(0,64,48,64),
                    new(48,64,48,64),
                    new(96,64,48,64),
                    new(144,64,48,64),
                    new(192,64,48,64),
                    new(240,64,48,64),
                    new(96,64,48,64),
                    new(48,64,48,64),
                },
                [2] = new()
                {
                    new(0,0,48,64),
                    new(48,0,48,64),
                    new(96,0,48,64),
                    new(144,0,48,64),
                    new(192,0,48,64),
                    new(240,0,48,64),
                    new(96,0,48,64),
                    new(48,0,48,64),
                },
                [3] = new()
                {
                    new(0,64,48,64),
                    new(48,64,48,64),
                    new(96,64,48,64),
                    new(144,64,48,64),
                    new(192,64,48,64),
                    new(240,64,48,64),
                    new(96,64,48,64),
                    new(48,64,48,64),
                },

            };

        }


        public void DashFrames()
        {

            dashFrames = new()
            {
                [0] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                },
                [1] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                },
                [2] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                },
                [3] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                },
                [4] = new()
                {
                    new(160,0,32,32),
                },
                [5] = new()
                {
                    new(160,0,32,32),
                },
                [6] = new()
                {
                    new(160,0,32,32),
                },
                [7] = new()
                {
                    new(160,0,32,32),
                },
                [8] = new()
                {
                    new(64,0,32,32),
                    new(32,0,32,32),
                },
                [9] = new()
                {
                    new(64,0,32,32),
                    new(32,0,32,32),
                },
                [10] = new()
                {
                    new(64,0,32,32),
                    new(32,0,32,32),

                },
                [11] = new()
                {
                    new(64,0,32,32),
                    new(32,0,32,32),

                },
            };

        }

        public void LargeDashFrames()
        {

            dashFrames = new()
            {
                [0] = new()
                {
                    new(0,128,48,64),
                    new(48,128,48,64),
                    new(96,128,48,64),
                    new(144,128,48,64),
                    new(192,128,48,64),
                },
                [1] = new()
                {
                    new(0,64,48,64),
                    new(48,64,48,64),
                    new(96,64,48,64),
                    new(144,64,48,64),
                    new(192,64,48,64),
                },
                [2] = new()
                {
                    new(0,0,48,64),
                    new(48,0,48,64),
                    new(96,0,48,64),
                    new(144,0,48,64),
                    new(192,0,48,64),
                },
                [3] = new()
                {
                    new(0,64,48,64),
                    new(48,64,48,64),
                    new(96,64,48,64),
                    new(144,64,48,64),
                    new(192,64,48,64),
                },
                [4] = new()
                {
                    new(240,128,48,64),
                },
                [5] = new()
                {
                    new(240,64,48,64),
                },
                [6] = new()
                {
                    new(240,0,48,64),
                },
                [7] = new()
                {
                    new(240,64,48,64),
                },
                [8] = new()
                {
                    new(96,128,48,64),
                    new(48,128,48,64),
                },
                [9] = new()
                {
                    new(96,64,48,64),
                    new(48,64,48,64),
                },
                [10] = new()
                {
                    new(96,0,48,64),
                    new(48,0,48,64),

                },
                [11] = new()
                {
                    new(96,64,48,64),
                    new(48,64,48,64),

                },
            };

        }

        public void DrawNormal(SpriteBatch b, SlimeRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            int shadowFrame = 0;

            switch (use.series)
            {

                case SlimeRenderAdditional.slimeseries.sweep:

                    use.position = use.position - new Vector2(0, sweepAdjust[use.frame] * use.scale);

                    source = specialFrames[Character.Character.specials.sweep][use.direction][use.frame];

                    break;

                case SlimeRenderAdditional.slimeseries.special:

                    use.position = use.position - new Vector2(0, specialAdjust[use.frame] * use.scale);

                    source = specialFrames[Character.Character.specials.special][use.direction][use.frame];

                    break;

                case SlimeRenderAdditional.slimeseries.tackle:

                    use.position = use.position - new Vector2(0, specialAdjust[use.frame] * use.scale);

                    source = specialFrames[Character.Character.specials.tackle][use.direction][use.frame];

                    break;

                case SlimeRenderAdditional.slimeseries.dash:

                    use.frame = Math.Min(use.frame, (dashFrames[use.direction].Count - 1));

                    source = dashFrames[use.direction][use.frame];

                    shadowFrame = source.X;

                    break;

                case SlimeRenderAdditional.slimeseries.idle:

                    if (standbyTransition)
                    {

                        if (transitionLapse == -1)
                        {

                            transitionLapse = (3 + use.frame) % 4;

                        }

                        if (use.frame != transitionLapse)
                        {

                            source = transitionFrames[1][use.frame];

                        }
                        else
                        {

                            transitionLapse = -1;

                            standbyTransition = false;

                        }


                    }

                    source = idleFrames[Character.Character.idles.idle][use.direction][use.frame];

                    shadowFrame = source.X;

                    break;

                case SlimeRenderAdditional.slimeseries.standby:

                    source = idleFrames[Character.Character.idles.standby][use.direction][use.frame];

                    if (!standbyTransition)
                    {

                        if(transitionLapse == -1)
                        {

                            transitionLapse = (3 + use.frame) % 4;

                        }

                        if(use.frame != transitionLapse)
                        {

                            source = transitionFrames[0][use.frame];

                        }
                        else
                        {

                            transitionLapse = -1;

                            standbyTransition = true;

                        }


                    }

                    shadowFrame = source.X;

                    break;

                default:

                    if (standbyTransition)
                    {

                        if (transitionLapse == -1)
                        {

                            transitionLapse = (3 + use.frame) % 4;

                        }

                        if (use.frame != transitionLapse)
                        {

                            source = transitionFrames[1][use.frame];

                        }
                        else
                        {

                            transitionLapse = -1;

                            standbyTransition = false;

                        }

                    }

                    use.position = use.position + new Vector2(0, hoverAdjust * use.scale);

                    use.frame = HoverFrame();

                    source = idleFrames[Character.Character.idles.none][use.direction][use.frame];

                    shadowFrame = source.X;

                    break;

            }

            Rectangle shadowRect = new(shadowFrame, shadowHeight, frameWidth, frameHeight);

            b.Draw(slimeTexture, use.shadow, shadowRect, Color.White * 0.2f, 0.0f, frameOrigin, use.scale, use.flip ? SpriteEffects.FlipHorizontally : 0, use.layer - 0.001f);

            b.Draw(slimeTexture, use.position, source, Color.White * 0.8f, 0.0f, frameOrigin, use.scale, use.flip ? SpriteEffects.FlipHorizontally : 0, use.layer);

        }

        public int HoverFrame()
        {

            return Math.Min((int)(hoverMoment / hoverLapse), idleFrames[0].Count-1);

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
            else if (hoverAdjust < 0)
            {

                hoverAdjust = (hoverLapseTwo + hoverMoment) * hoverFloat;

            }
            else
            {

                hoverOffset = 0;

            }

        }

    }

    public class SlimeRenderAdditional
    {

        public int direction = 2;

        public int frame = 0;

        public Vector2 position;

        public Vector2 shadow;

        public float scale = 4f;

        public bool flip = false;

        public float layer = 0.0001f;

        public enum slimemode
        {
            none,
            growl,
        }

        public slimemode mode;

        public enum slimeseries
        {
            none,
            sweep,
            dash,
            special,
            hover,
            tackle,
            idle,
            walk,
            standby,
        }

        public slimeseries series;

        public float fade = 1f;

    }

}
