using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StardewDruid.Render
{
    public class DustkingRender
    {

        public Texture2D dustTexture;

        public enum dustFrames
        {

            downIdle,
            downJump1,
            downJump2,
            downJump3,
            downWalk1,
            downWalk2,

            rightIdle,
            rightJump1,
            rightJump2,
            rightJump3,
            rightWalk1,
            rightWalk2,

            upIdle,
            upJump1,
            upJump2,
            upJump3,
            upWalk1,
            upWalk2,

            shadow,
            laugh1,
            laugh2,
            sit1,
            sit2,

        }

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> idleFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> walkFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> specialFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> dashFrames = new();

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> shadowFrames = new();

        public int bounce = 0;

        public DustkingRender(string name)
        {

            dustTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", name+".png"));

            IdleFrames();

            WalkFrames();

            SpecialFrames();

            DashFrames();

            ShadowFrames();

        }

        public static Microsoft.Xna.Framework.Rectangle DustRectangle(dustFrames frame)
        {
            
            return new((int)frame % 6 * 48, (int)frame / 6 * 48, 48, 48);
       
        }

        public void ShadowFrames()
        {

            shadowFrames = new()
            {

                [0] = DustRectangle(dustFrames.shadow),
                [1] = DustRectangle(dustFrames.shadow),
                [2] = DustRectangle(dustFrames.shadow),
                [3] = DustRectangle(dustFrames.shadow),
            };

        }

        public void IdleFrames()
        {

            idleFrames = new()
            {
                [0] = new()
                    {
                        DustRectangle(dustFrames.upIdle),
                    },
                [1] = new()
                    {
                        DustRectangle(dustFrames.rightIdle),
                    },
                [2] = new()
                    {
                        DustRectangle(dustFrames.downIdle),
                    },
                [3] = new()
                    {
                        DustRectangle(dustFrames.rightIdle),
                    },
            };

        }

        public void WalkFrames()
        {

            walkFrames = new()
            {
                [0] = new()
                    {
                    DustRectangle(dustFrames.upIdle),
                        DustRectangle(dustFrames.upWalk1),
                        DustRectangle(dustFrames.upWalk1),
                        DustRectangle(dustFrames.upWalk2),
                        DustRectangle(dustFrames.upWalk2),
                    },
                [1] = new()
                    {
                    DustRectangle(dustFrames.rightIdle),
                        DustRectangle(dustFrames.rightWalk1),
                        DustRectangle(dustFrames.rightWalk1),
                        DustRectangle(dustFrames.rightWalk2),
                        DustRectangle(dustFrames.rightWalk2),
                    },
                [2] = new()
                    {
                    DustRectangle(dustFrames.downIdle),
                        DustRectangle(dustFrames.downWalk1),
                        DustRectangle(dustFrames.downWalk1),
                        DustRectangle(dustFrames.downWalk2),
                        DustRectangle(dustFrames.downWalk2),
                    },
                [3] = new()
                    {
                    DustRectangle(dustFrames.rightIdle),
                        DustRectangle(dustFrames.rightWalk1),
                        DustRectangle(dustFrames.rightWalk1),
                        DustRectangle(dustFrames.rightWalk2),
                        DustRectangle(dustFrames.rightWalk2),
                    },
            };

        }

        public void SpecialFrames()
        {

            specialFrames = new()
            {

                [0] = new()
                    {
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                    },
                [1] = new()
                    {
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                    },
                [2] = new()
                    {
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                    },
                [3] = new()
                    {
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                        DustRectangle(dustFrames.laugh1),
                        DustRectangle(dustFrames.laugh2),
                    },

            };

            sweepFrames = new()
            {
                [0] = new()
                    {
                        DustRectangle(dustFrames.upJump1),
                        DustRectangle(dustFrames.upJump2),
                        DustRectangle(dustFrames.upJump3),
                        DustRectangle(dustFrames.upJump1),
                    },
                [1] = new()
                    {
                        DustRectangle(dustFrames.rightJump1),
                        DustRectangle(dustFrames.rightJump2),
                        DustRectangle(dustFrames.rightJump3),
                        DustRectangle(dustFrames.rightJump1),

                    },
                [2] = new()
                    {
                        DustRectangle(dustFrames.downJump1),
                        DustRectangle(dustFrames.downJump2),
                        DustRectangle(dustFrames.downJump3),
                        DustRectangle(dustFrames.downJump1),

                    },
                [3] = new()
                    {
                        DustRectangle(dustFrames.rightJump1),
                        DustRectangle(dustFrames.rightJump2),
                        DustRectangle(dustFrames.rightJump3),
                        DustRectangle(dustFrames.rightJump1),
                    },

            };

        }

        public void DashFrames()
        {

            dashFrames = new()
            {
                [0] = new()
                    {
                        DustRectangle(dustFrames.upJump1),
                        DustRectangle(dustFrames.upJump2),
                        DustRectangle(dustFrames.upJump2),
                    },
                [1] = new()
                    {
                        DustRectangle(dustFrames.rightJump1),
                        DustRectangle(dustFrames.rightJump2),
                        DustRectangle(dustFrames.rightJump2),
                    },
                [2] = new()
                    {
                        DustRectangle(dustFrames.downJump1),
                        DustRectangle(dustFrames.downJump2),
                        DustRectangle(dustFrames.downJump2),
                    },
                [3] = new()
                    {
                        DustRectangle(dustFrames.rightJump1),
                        DustRectangle(dustFrames.rightJump2),
                        DustRectangle(dustFrames.rightJump2),
                    },

                [4] = new()
                    {
                        DustRectangle(dustFrames.upJump3),
                    },
                [5] = new()
                    {
                        DustRectangle(dustFrames.rightJump3),
                    },
                [6] = new()
                    {
                        DustRectangle(dustFrames.downJump3),
                    },
                [7] = new()
                    {
                        DustRectangle(dustFrames.rightJump3),
                    },

                [8] = new()
                    {
                        DustRectangle(dustFrames.upJump1),
                    },
                [9] = new()
                    {
                        DustRectangle(dustFrames.rightJump1),
                    },
                [10] = new()
                    {
                         DustRectangle(dustFrames.downJump1),
                    },
                [11] = new()
                    {
                        DustRectangle(dustFrames.rightJump1),
                    },

            };

        }
        
        public void DrawNormal(SpriteBatch b, DustkingRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            Vector2 usePosition = use.position;

            switch (use.series)
            {

                case DustkingRenderAdditional.dustseries.sweep:

                    source = sweepFrames[use.direction][use.frame];

                    break;

                case DustkingRenderAdditional.dustseries.special:

                    source = specialFrames[use.direction][use.frame];

                    break;

                case DustkingRenderAdditional.dustseries.dash:

                    source = dashFrames[use.direction][use.frame];

                    break;

                default:

                    switch (use.frame)
                    {

                        case 1:
                        case 3:

                            if(bounce < (int)(use.scale * 16))
                            {

                                bounce++;
                                bounce++;

                            }

                            break;

                        default:

                            if(bounce > 0)
                            {

                                bounce--;
                                bounce--;

                            }

                            break;

                    }

                    usePosition.Y -= bounce;

                    source = walkFrames[use.direction][use.frame];

                    break;

            }

            b.Draw(
                dustTexture,
                usePosition,
                source,
                Microsoft.Xna.Framework.Color.White * use.fade,
                0f,
                new Vector2(source.Width / 2, source.Height / 2),
                use.scale,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer + 0.0002f
            );

            int useDirection = use.direction % 4;

            Microsoft.Xna.Framework.Rectangle shadow = shadowFrames[useDirection];

            Vector2 shadowPosition = new Vector2(use.position.X, use.position.Y + (use.scale*8));

            b.Draw(
                dustTexture,
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

    public class DustkingRenderAdditional
    {

        public int direction = 2;

        public int frame = 0;

        public Vector2 position;

        public float scale = 4f;

        public bool flip = false;

        public float layer = 0.0001f;

        public enum dustseries
        {
            none,
            sweep,
            dash,
            special,
        }

        public dustseries series;

        public float fade = 1f;

    }

}
