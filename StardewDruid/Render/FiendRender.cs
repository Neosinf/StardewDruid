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
    public class FiendRender
    {

        public Texture2D fiendTexture;

        public enum slimeFrames
        {

        }

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> idleFrames = new();

        public Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> specialFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> dashFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> fluffFrames = new();

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


        public FiendRender(CharacterHandle.characters entity)
        {

            fiendTexture = CharacterHandle.CharacterTexture(entity);

            switch (entity)
            {

                default:

                    IdleFrames();

                    SpecialFrames();

                    DashFrames();

                    FluffFrames();

                    break;

            }

            frameOrigin = new(frameWidth / 2, frameHeight / 2);

            hoverFloat = 0.6f;

            hoverLapse = 8;

            hoverLapseTwo = 0 - (hoverLapse * idleFrames[0].Count);

            hoverLapseThree = hoverLapse * idleFrames[0].Count * 2;

            hoverOffset = Mod.instance.randomIndex.Next(12);

        }

        public void IdleFrames()
        {

            idleFrames = new()
            {
                [0] = new()
                {
                    new(96,64,32,32),
                    new(64,64,32,32),
                    new(32,64,32,32),
                    new(0,64,32,32),

                },
                [1] = new()
                {
                    new(96,32,32,32),
                    new(64,32,32,32),
                    new(32,32,32,32),
                    new(0,32,32,32),

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
                    new(96,32,32,32),
                    new(64,32,32,32),
                    new(32,32,32,32),
                    new(0,32,32,32),

                },
            };

        }

        public void SpecialFrames()
        {

            specialFrames[Character.Character.specials.special] = new()
            {
                [0] = new()
                {
                    new(0,64,32,32),
                    new(32,64,32,32),
                    new(64,64,32,32),
                    new(96,64,32,32),
                    new(128,64,32,32),
                    new(160,64,32,32),
                    new(192,64,32,32),
                    new(224,64,32,32),
                    new(32,64,32,32),
                },
                [1] = new()
                {
                    new(0,32,32,32),
                    new(32,32,32,32),
                    new(64,32,32,32),
                    new(96,32,32,32),
                    new(128,32,32,32),
                    new(160,32,32,32),
                    new(192,32,32,32),
                    new(224,32,32,32),
                    new(32,32,32,32),
                },
                [2] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                    new(160,0,32,32),
                    new(192,0,32,32),
                    new(224,0,32,32),
                    new(32,0,32,32),
                },
                [3] = new()
                {
                    new(0,32,32,32),
                    new(32,32,32,32),
                    new(64,32,32,32),
                    new(96,32,32,32),
                    new(128,32,32,32),
                    new(160,32,32,32),
                    new(192,32,32,32),
                    new(224,32,32,32),
                    new(32,32,32,32),
                },
            };

            specialFrames[Character.Character.specials.sweep] = new()
            {
                [0] = new()
                {
                    new(0,64,32,32),
                    new(32,64,32,32),
                    new(64,64,32,32),
                    new(96,64,32,32),
                    new(128,64,32,32),
                    new(96,64,32,32),
                    new(32,64,32,32),
                },
                [1] = new()
                {
                    new(0,32,32,32),
                    new(32,32,32,32),
                    new(64,32,32,32),
                    new(96,32,32,32),
                    new(128,32,32,32),
                    new(96,32,32,32),
                    new(32,32,32,32),
                },
                [2] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                    new(96,0,32,32),
                    new(32,0,32,32),
                },
                [3] = new()
                {
                    new(0,32,32,32),
                    new(32,32,32,32),
                    new(64,32,32,32),
                    new(96,32,32,32),
                    new(128,32,32,32),
                    new(96,32,32,32),
                    new(32,32,32,32),
                },
            };

            specialAdjust = new()
            {
                5f,
                10f,
                15f,
                20f,
                20f,
                20f,
                15f,
                10f,
                5f,

            };

            sweepAdjust = new()
            {
                5f,
                10f,
                15f,
                20f,
                20f,
                12.5f,
                5f,

            };

            specialFrames[Character.Character.specials.tackle] = new()
            {
                [0] = new()
                {
                    new(0,64,32,32),
                    new(32,64,32,32),
                    new(64,64,32,32),
                    new(96,64,32,32),
                    new(128,64,32,32),
                    new(96,64,32,32),
                    new(64,64,32,32),
                    new(32,64,32,32),
                },
                [1] = new()
                {
                    new(0,32,32,32),
                    new(32,32,32,32),
                    new(64,32,32,32),
                    new(96,32,32,32),
                    new(128,32,32,32),
                    new(96,32,32,32),
                    new(64,32,32,32),
                    new(32,32,32,32),
                },
                [2] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                    new(128,0,32,32),
                    new(96,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                },
                [3] = new()
                {
                    new(0,32,32,32),
                    new(32,32,32,32),
                    new(64,32,32,32),
                    new(96,32,32,32),
                    new(128,32,32,32),
                    new(96,32,32,32),
                    new(64,32,32,32),
                    new(32,32,32,32),
                },
            };

        }

        public void DashFrames()
        {

            dashFrames = new()
            {
                [0] = new()
                {
                    new(0,64,32,32),
                    new(32,64,32,32),
                    new(64,64,32,32),
                    new(96,64,32,32),
                },
                [1] = new()
                {
                    new(0,32,32,32),
                    new(32,32,32,32),
                    new(64,32,32,32),
                    new(96,32,32,32),
                },
                [2] = new()
                {
                    new(0,0,32,32),
                    new(32,0,32,32),
                    new(64,0,32,32),
                    new(96,0,32,32),
                },
                [3] = new()
                {
                    new(0,32,32,32),
                    new(32,32,32,32),
                    new(64,32,32,32),
                    new(96,32,32,32),
                },
                [4] = new()
                {
                    new(128,64,32,32),
                },
                [5] = new()
                {
                    new(128,32,32,32),
                },
                [6] = new()
                {
                    new(128,0,32,32),
                },
                [7] = new()
                {
                    new(128,32,32,32),
                },
                [8] = new()
                {
                    new(96,64,32,32),
                    new(64,64,32,32),
                    new(32,64,32,32),
                },
                [9] = new()
                {
                    new(96,32,32,32),
                    new(64,32,32,32),
                    new(32,32,32,32),
                },
                [10] = new()
                {
                    new(96,0,32,32),
                    new(64,0,32,32),
                    new(32,0,32,32),
                },
                [11] = new()
                {
                    new(96,32,32,32),
                    new(64,32,32,32),
                    new(32,32,32,32),
                },
            };

        }

        public void FluffFrames()
        {

            fluffFrames = new()
            {
                [0] = new()
                {
                    new(160,64,32,32),
                    new(192,64,32,32),
                    new(224,64,32,32),
                },
                [1] = new()
                {
                    new(160,32,32,32),
                    new(192,32,32,32),
                    new(224,32,32,32),
                },
                [2] = new()
                {
                    new(160,0,32,32),
                    new(192,0,32,32),
                    new(224,0,32,32),
                },
                [3] = new()
                {
                    new(160,32,32,32),
                    new(192,32,32,32),
                    new(224,32,32,32),
                },
            };

        }

        public void DrawNormal(SpriteBatch b, FiendRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            int shadowFrame = 0;

            hoverFloat = 0.6f;

            hoverLapse = 8;

            switch (use.series)
            {

                case FiendRenderAdditional.fiendseries.sweep:

                    use.position = use.position - new Vector2(0, sweepAdjust[use.frame] * use.scale);

                    source = specialFrames[Character.Character.specials.sweep][use.direction][use.frame];

                    break;

                case FiendRenderAdditional.fiendseries.special:

                    use.position = use.position - new Vector2(0, specialAdjust[use.frame] * use.scale);

                    source = specialFrames[Character.Character.specials.special][use.direction][use.frame];

                    break;

                case FiendRenderAdditional.fiendseries.tackle:

                    use.position = use.position - new Vector2(0, specialAdjust[use.frame] * use.scale);

                    source = specialFrames[Character.Character.specials.tackle][use.direction][use.frame];

                    break;

                case FiendRenderAdditional.fiendseries.dash:

                    use.frame = Math.Min(use.frame, (dashFrames[use.direction].Count - 1));

                    source = dashFrames[use.direction][use.frame];

                    shadowFrame = source.X;

                    break;

                case FiendRenderAdditional.fiendseries.fluff:

                    // use.position = use.position + new Vector2(0, (hoverAdjust * use.scale)/3);

                    use.scale -= 1f;

                    source = fluffFrames[use.direction][use.frame];

                    shadowFrame = source.X;

                    break;

                default:

                    use.position = use.position + new Vector2(0, hoverAdjust * use.scale);

                    use.frame = HoverFrame();

                    source = idleFrames[use.direction][use.frame];

                    shadowFrame = source.X;

                    break;

            }

            Rectangle shadowRect = new(shadowFrame, shadowHeight, frameWidth, frameHeight);

            b.Draw(fiendTexture, use.shadow, shadowRect, Color.White * 0.2f, 0.0f, frameOrigin, use.scale, 0, use.layer - 0.001f);

            b.Draw(fiendTexture, use.position, source, Color.White * 0.8f, 0.0f, frameOrigin, use.scale, use.flip ? SpriteEffects.FlipHorizontally : 0, use.layer);

        }

        public int HoverFrame()
        {

            return Math.Min((int)(hoverMoment / hoverLapse), idleFrames[0].Count - 1);

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

    public class FiendRenderAdditional
    {

        public int direction = 2;

        public int frame = 0;

        public Vector2 position;

        public Vector2 shadow;

        public float scale = 4f;

        public bool flip = false;

        public float layer = 0.0001f;

        public enum fiendseries
        {
            none,
            sweep,
            dash,
            special,
            hover,
            fluff,
            tackle,
            idle,
        }

        public fiendseries series;

        public float fade = 1f;

    }

}
