using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StardewDruid.Render
{
    public class BearRender
    {

        public Texture2D bearTexture;

        public enum bearFrames
        {

            downTorso,
            rightTorso,
            upTorso,
            downHead,
            rightHead,
            upHead,
            downHeadSpecial,
            rightHeadSpecial,
            upHeadSpecial,

            downIdle,
            downWalkR1,
            downWalkR2,
            downWalkR3,
            downWalkL1,
            downWalkL2,
            downWalkL3,
            downSwipeR,
            downSwipeL,

            rightIdle,
            rightWalkR1,
            rightWalkR2,
            rightWalkR3,
            rightWalkL1,
            rightWalkL2,
            rightWalkL3,
            rightSwipeR,
            rightSwipeL,

            upIdle,
            upWalkR1,
            upWalkR2,
            upWalkR3,
            upWalkL1,
            upWalkL2,
            upWalkL3,
            upSwipeR,
            upSwipeL,

            downShadow,
            rightShadow,
            upShadow,

        }

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> torsoFrames = new();

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> headFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> idleFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> walkFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> specialFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> dashFrames = new();

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> shadowFrames = new();

        public BearRender(string name)
        {

            bearTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", name+".png"));

            TorsoFrames();

            HeadFrames();

            IdleFrames();

            WalkFrames();

            SweepFrames();

            DashFrames();

            ShadowFrames();

        }

        public static Microsoft.Xna.Framework.Rectangle BearRectangle(bearFrames frame)
        {
            return new((int)frame % 9 * 64, (int)frame / 9 * 64, 64, 64);
        }

        public void TorsoFrames()
        {

            torsoFrames = new()
            {

                [0] = BearRectangle(bearFrames.upTorso),
                [1] = BearRectangle(bearFrames.rightTorso),
                [2] = BearRectangle(bearFrames.downTorso),
                [3] = BearRectangle(bearFrames.rightTorso),

            };

        }

        public void HeadFrames()
        {

            headFrames = new()
            {

                [0] = BearRectangle(bearFrames.upHead),
                [1] = BearRectangle(bearFrames.rightHead),
                [2] = BearRectangle(bearFrames.downHead),
                [3] = BearRectangle(bearFrames.rightHead),

            };

        }


        public void ShadowFrames()
        {

            shadowFrames = new()
            {

                [0] = BearRectangle(bearFrames.upShadow),
                [1] = BearRectangle(bearFrames.rightShadow),
                [2] = BearRectangle(bearFrames.downShadow),
                [3] = BearRectangle(bearFrames.rightShadow),

            };

        }

        public void IdleFrames()
        {

            idleFrames = new()
            {
                [0] = new()
                    {
                        BearRectangle(bearFrames.upIdle),
                    },
                [1] = new()
                    {
                        BearRectangle(bearFrames.rightIdle),
                    },
                [2] = new()
                    {
                        BearRectangle(bearFrames.downIdle),
                    },
                [3] = new()
                    {
                        BearRectangle(bearFrames.rightIdle),
                    },

            };

        }

        public void WalkFrames()
        {

            walkFrames = new()
            {
                [0] = new()
                    {
                        BearRectangle(bearFrames.upIdle),
                        BearRectangle(bearFrames.upWalkR1),
                        BearRectangle(bearFrames.upWalkR2),
                        BearRectangle(bearFrames.upWalkR3),
                        BearRectangle(bearFrames.upWalkL1),
                        BearRectangle(bearFrames.upWalkL2),
                        BearRectangle(bearFrames.upWalkL3),

                    },
                [1] = new()
                    {
                        BearRectangle(bearFrames.rightIdle),
                        BearRectangle(bearFrames.rightWalkR1),
                        BearRectangle(bearFrames.rightWalkR2),
                        BearRectangle(bearFrames.rightWalkR3),
                        BearRectangle(bearFrames.rightWalkL1),
                        BearRectangle(bearFrames.rightWalkL2),
                        BearRectangle(bearFrames.rightWalkL3),
                    },
                [2] = new()
                    {
                        BearRectangle(bearFrames.downIdle),
                        BearRectangle(bearFrames.downWalkR1),
                        BearRectangle(bearFrames.downWalkR2),
                        BearRectangle(bearFrames.downWalkR3),
                        BearRectangle(bearFrames.downWalkL1),
                        BearRectangle(bearFrames.downWalkL2),
                        BearRectangle(bearFrames.downWalkL3),
                    },
                [3] = new()
                    {
                        BearRectangle(bearFrames.rightIdle),
                        BearRectangle(bearFrames.rightWalkR1),
                        BearRectangle(bearFrames.rightWalkR2),
                        BearRectangle(bearFrames.rightWalkR3),
                        BearRectangle(bearFrames.rightWalkL1),
                        BearRectangle(bearFrames.rightWalkL2),
                        BearRectangle(bearFrames.rightWalkL3),
                    },

            };

        }

        public void SweepFrames()
        {

            sweepFrames = new()
            {
                [0] = new()
                    {
                        BearRectangle(bearFrames.upWalkR1),
                        BearRectangle(bearFrames.upSwipeR),
                        BearRectangle(bearFrames.upSwipeR),
                        BearRectangle(bearFrames.upWalkL1),
                        BearRectangle(bearFrames.upSwipeL),
                        BearRectangle(bearFrames.upSwipeL),

                    },
                [1] = new()
                    {
                        BearRectangle(bearFrames.rightWalkR1),
                        BearRectangle(bearFrames.rightSwipeR),
                        BearRectangle(bearFrames.rightSwipeR),
                        BearRectangle(bearFrames.rightWalkL1),
                        BearRectangle(bearFrames.rightSwipeL),
                        BearRectangle(bearFrames.rightSwipeL),

                    },
                [2] = new()
                    {
                        BearRectangle(bearFrames.downWalkR1),
                        BearRectangle(bearFrames.downSwipeR),
                        BearRectangle(bearFrames.downSwipeR),
                        BearRectangle(bearFrames.downWalkL1),
                        BearRectangle(bearFrames.downSwipeL),
                        BearRectangle(bearFrames.downSwipeL),

                    },
                [3] = new()
                    {
                        BearRectangle(bearFrames.rightWalkR1),
                        BearRectangle(bearFrames.rightSwipeR),
                        BearRectangle(bearFrames.rightSwipeR),
                        BearRectangle(bearFrames.rightWalkL1),
                        BearRectangle(bearFrames.rightSwipeL),
                        BearRectangle(bearFrames.rightSwipeL),

                    },

            };

        }

        public void DashFrames()
        {

            dashFrames = new()
            {
                [0] = new()
                    {
                        BearRectangle(bearFrames.upWalkL1),
                        BearRectangle(bearFrames.upWalkL2),
                        BearRectangle(bearFrames.upWalkL3),
                        BearRectangle(bearFrames.upWalkR1),
                    },
                [1] = new()
                    {
                        BearRectangle(bearFrames.rightWalkL1),
                        BearRectangle(bearFrames.rightWalkL2),
                        BearRectangle(bearFrames.rightWalkL3),
                        BearRectangle(bearFrames.rightWalkR1),
                    },
                [2] = new()
                    {
                        BearRectangle(bearFrames.downWalkL1),
                        BearRectangle(bearFrames.downWalkL2),
                        BearRectangle(bearFrames.downWalkL3),
                        BearRectangle(bearFrames.downWalkR1),
                    },
                [3] = new()
                    {
                        BearRectangle(bearFrames.rightWalkL1),
                        BearRectangle(bearFrames.rightWalkL2),
                        BearRectangle(bearFrames.rightWalkL3),
                        BearRectangle(bearFrames.rightWalkR1),
                    },

                [4] = new()
                    {
                        BearRectangle(bearFrames.upSwipeR),
                    },
                [5] = new()
                    {
                        BearRectangle(bearFrames.rightSwipeR),
                    },
                [6] = new()
                    {
                        BearRectangle(bearFrames.downSwipeR),
                    },
                [7] = new()
                    {
                        BearRectangle(bearFrames.rightSwipeR),
                    },

                [8] = new()
                    {
                        BearRectangle(bearFrames.upWalkR2),
                        BearRectangle(bearFrames.upWalkR3),
                    },
                [9] = new()
                    {
                        BearRectangle(bearFrames.rightWalkR2),
                        BearRectangle(bearFrames.rightWalkR3),
                    },
                [10] = new()
                    {
                        BearRectangle(bearFrames.downWalkR2),
                        BearRectangle(bearFrames.downWalkR3),
                    },
                [11] = new()
                    {
                        BearRectangle(bearFrames.rightWalkR2),
                        BearRectangle(bearFrames.rightWalkR3),
    
                    },

            };

        }
        
        public void DrawNormal(SpriteBatch b, BearRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            switch (use.series)
            {

                case BearRenderAdditional.bearseries.sweep:

                    source = sweepFrames[use.direction][use.frame];

                    break;

                case BearRenderAdditional.bearseries.special:

                    source = specialFrames[use.direction][use.frame];

                    break;

                case BearRenderAdditional.bearseries.dash:

                    source = dashFrames[use.direction][use.frame];

                    break;

                default:

                    source = walkFrames[use.direction][use.frame];

                    break;

            }

            b.Draw(
                bearTexture,
                use.position,
                source,
                Microsoft.Xna.Framework.Color.White * use.fade,
                0f,
                new Vector2(source.Width / 2, source.Height / 2),
                use.scale,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer + 0.0002f
            );

            int useDirection = use.direction % 4;

            Microsoft.Xna.Framework.Rectangle torso = torsoFrames[useDirection];

            b.Draw(
                bearTexture,
                use.position,
                torso,
                Microsoft.Xna.Framework.Color.White * use.fade,
                0f,
                new Vector2(torso.Width / 2, torso.Height / 2),
                use.scale,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer
            );

            Microsoft.Xna.Framework.Rectangle head = headFrames[useDirection];

            float headOffset = 0.0001f;

            switch (useDirection)
            {
                case 0:

                    headOffset = -0.0001f;

                    break;

                case 2:

                    headOffset = 0.0003f;

                    break;
            }

            if (use.mode == BearRenderAdditional.bearmode.growl)
            {
                head.X += 192;
            }

            b.Draw(
                bearTexture,
                use.position,
                head,
                Microsoft.Xna.Framework.Color.White * use.fade,
                0f,
                new Vector2(head.Width / 2, head.Height / 2),
                use.scale,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer + headOffset
            );

            Microsoft.Xna.Framework.Rectangle shadow = shadowFrames[useDirection];

            Vector2 shadowPosition = use.position;

            if (useDirection % 2 == 1)
            {

                shadowPosition.Y += 8;

            }

            b.Draw(
                bearTexture,
                shadowPosition,
                shadow,
                Color.White * 0.15f,
                0.0f,
                new Vector2(shadow.Width / 2, shadow.Height / 2),
                use.scale + (Math.Abs(0 - (walkFrames[0].Count() / 2) + use.scale) * 0.05f),
                use.flip ? (SpriteEffects)1 : 0,
                use.layer - 0.0002f
            );

        }

    }

    public class BearRenderAdditional
    {

        public int direction = 2;

        public int frame = 0;

        public Vector2 position;

        public float scale = 4f;

        public bool flip = false;

        public float layer = 0.0001f;

        public enum bearmode
        {
            none,
            growl,
        }

        public bearmode mode;

        public enum bearseries
        {
            none,
            sweep,
            dash,
            special,
        }

        public bearseries series;

        public float fade = 1f;

    }

}
