using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Monster;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StardewDruid.Render
{
    public class WolfRender
    {

        Texture2D wolfTexture;

        public enum wolfFrames
        {

            downIdle,
            downWalkR1,
            downWalkR2,
            downWalkR3,
            downWalkL1,
            downWalkL2,
            downWalkL3,

            rightIdle,
            rightWalkR1,
            rightWalkR2,
            rightWalkR3,
            rightWalkL1,
            rightWalkL2,
            rightWalkL3,

            upIdle,
            upWalkR1,
            upWalkR2,
            upWalkR3,
            upWalkL1,
            upWalkL2,
            upWalkL3,

            downRunUp1,
            downRunUp2,
            downRunGlide1,
            downRunDown2,
            downRunDown3,
            downHowl,
            blank1,

            rightRunUp1,
            rightRunUp2,
            rightRunGlide1,
            rightRunDown2,
            rightRunDown3,
            rightHowl,
            blank2,

            upRunUp1,
            upRunUp2,
            upRunGlide1,
            upRunDown2,
            upRunDown3,
            upHowl,
            blank3,

            sit1,
            sit2,
            sleep1,
            sleep2,
            downShadow,
            rightShadow,
            upShadow,

        }

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> idleFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> walkFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> specialFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> dashFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> standbyFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> restFrames = new();

        public Dictionary<int, Microsoft.Xna.Framework.Rectangle> shadowFrames = new();

        public WolfRender(string name)
        {

            wolfTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", name + ".png"));

            IdleFrames();

            WalkFrames();

            SpecialFrames();

            SweepFrames();

            DashFrames();

            StandbyFrames();

            RestFrames();

            ShadowFrames();

        }

        public static Microsoft.Xna.Framework.Rectangle WolfRectangle(wolfFrames frame)
        {

            return new((int)frame % 7 * 96, (int)frame / 7 * 96, 96, 96);

        }

        public void IdleFrames()
        {

            idleFrames = new()
            {
                [0] = new()
                    {
                        WolfRectangle(wolfFrames.upIdle),
                    },
                [1] = new()
                    {
                        WolfRectangle(wolfFrames.rightIdle),
                    },
                [2] = new()
                    {
                        WolfRectangle(wolfFrames.downIdle),
                    },
                [3] = new()
                    {
                        WolfRectangle(wolfFrames.rightIdle),
                    },

            };

        }

        public void WalkFrames()
        {

            walkFrames = new()
            {
                [0] = new()
                    {
                        WolfRectangle(wolfFrames.upIdle),
                        WolfRectangle(wolfFrames.upWalkR1),
                        WolfRectangle(wolfFrames.upWalkR2),
                        WolfRectangle(wolfFrames.upWalkR3),
                        WolfRectangle(wolfFrames.upWalkL1),
                        WolfRectangle(wolfFrames.upWalkL2),
                        WolfRectangle(wolfFrames.upWalkL3),

                    },
                [1] = new()
                    {
                        WolfRectangle(wolfFrames.rightIdle),
                        WolfRectangle(wolfFrames.rightWalkR1),
                        WolfRectangle(wolfFrames.rightWalkR2),
                        WolfRectangle(wolfFrames.rightWalkR3),
                        WolfRectangle(wolfFrames.rightWalkL1),
                        WolfRectangle(wolfFrames.rightWalkL2),
                        WolfRectangle(wolfFrames.rightWalkL3),
                    },
                [2] = new()
                    {
                        WolfRectangle(wolfFrames.downIdle),
                        WolfRectangle(wolfFrames.downWalkR1),
                        WolfRectangle(wolfFrames.downWalkR2),
                        WolfRectangle(wolfFrames.downWalkR3),
                        WolfRectangle(wolfFrames.downWalkL1),
                        WolfRectangle(wolfFrames.downWalkL2),
                        WolfRectangle(wolfFrames.downWalkL3),
                    },
                [3] = new()
                    {
                        WolfRectangle(wolfFrames.rightIdle),
                        WolfRectangle(wolfFrames.rightWalkR1),
                        WolfRectangle(wolfFrames.rightWalkR2),
                        WolfRectangle(wolfFrames.rightWalkR3),
                        WolfRectangle(wolfFrames.rightWalkL1),
                        WolfRectangle(wolfFrames.rightWalkL2),
                        WolfRectangle(wolfFrames.rightWalkL3),
                    },

            };

        }

        public void SpecialFrames()
        {

            specialFrames = new()
            {
                [0] = new()
                    {
                        WolfRectangle(wolfFrames.upHowl),
                    },
                [1] = new()
                    {
                        WolfRectangle(wolfFrames.rightHowl),
                    },
                [2] = new()
                    {
                        WolfRectangle(wolfFrames.downHowl),
                    },
                [3] = new()
                    {
                        WolfRectangle(wolfFrames.rightHowl),
                    },

            };

        }

        public void StandbyFrames()
        {

            standbyFrames = new()
            {
                [0] = new()
                    {
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit2),
                    },
                [1] = new()
                    {
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit2),
                    },
                [2] = new()
                    {
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit2),
                    },
                [3] = new()
                    {
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit1),
                        WolfRectangle(wolfFrames.sit2),
                    },

            };

        }

        public void RestFrames()
        {

            restFrames = new()
            {
                [0] = new()
                    {
                        WolfRectangle(wolfFrames.sleep1),
                        WolfRectangle(wolfFrames.sleep2),
                        WolfRectangle(wolfFrames.sleep2),
                        WolfRectangle(wolfFrames.sleep2),
                    },
                [1] = new()
                    {
                        WolfRectangle(wolfFrames.sleep1),
                        WolfRectangle(wolfFrames.sleep2),
                        WolfRectangle(wolfFrames.sleep2),
                        WolfRectangle(wolfFrames.sleep2),
                    },
                [2] = new()
                    {
                        WolfRectangle(wolfFrames.sleep1),
                        WolfRectangle(wolfFrames.sleep2),
                        WolfRectangle(wolfFrames.sleep2),
                        WolfRectangle(wolfFrames.sleep2),
                    },
                [3] = new()
                    {
                        WolfRectangle(wolfFrames.sleep1),
                        WolfRectangle(wolfFrames.sleep2),
                        WolfRectangle(wolfFrames.sleep2),
                        WolfRectangle(wolfFrames.sleep2),
                    },

            };

        }

        public void SweepFrames()
        {

            sweepFrames = new()
            {
                [0] = new()
                    {
                        WolfRectangle(wolfFrames.upRunUp1),
                        WolfRectangle(wolfFrames.upRunUp2),
                        WolfRectangle(wolfFrames.upRunDown2),
                        WolfRectangle(wolfFrames.upRunDown3),

                    },
                [1] = new()
                    {
                        WolfRectangle(wolfFrames.rightRunUp1),
                        WolfRectangle(wolfFrames.rightRunUp2),
                        WolfRectangle(wolfFrames.rightRunDown2),
                        WolfRectangle(wolfFrames.rightRunDown3),

                    },
                [2] = new()
                    {
                        WolfRectangle(wolfFrames.downRunUp1),
                        WolfRectangle(wolfFrames.downRunUp2),
                        WolfRectangle(wolfFrames.downRunDown2),
                        WolfRectangle(wolfFrames.downRunDown3),

                    },
                [3] = new()
                    {
                        WolfRectangle(wolfFrames.rightRunUp1),
                        WolfRectangle(wolfFrames.rightRunUp2),
                        WolfRectangle(wolfFrames.rightRunDown2),
                        WolfRectangle(wolfFrames.rightRunDown3),

                    },

            };

        }

        public void DashFrames()
        {

            dashFrames = new()
            {
                [0] = new()
                    {
                        WolfRectangle(wolfFrames.upRunUp1),
                        WolfRectangle(wolfFrames.upRunUp2),


                    },
                [1] = new()
                    {
                        WolfRectangle(wolfFrames.rightRunUp1),
                        WolfRectangle(wolfFrames.rightRunUp2),


                    },
                [2] = new()
                    {
                        WolfRectangle(wolfFrames.downRunUp1),
                        WolfRectangle(wolfFrames.downRunUp2),


                    },
                [3] = new()
                    {
                        WolfRectangle(wolfFrames.rightRunUp1),
                        WolfRectangle(wolfFrames.rightRunUp2),


                    },
                [4] = new()
                    {

                        WolfRectangle(wolfFrames.upRunGlide1),


                    },
                [5] = new()
                    {

                        WolfRectangle(wolfFrames.rightRunGlide1),


                    },
                [6] = new()
                    {

                        WolfRectangle(wolfFrames.downRunGlide1),


                    },
                [7] = new()
                    {

                        WolfRectangle(wolfFrames.rightRunGlide1),



                    },
                [8] = new()
                    {

                        WolfRectangle(wolfFrames.upRunDown2),
                        WolfRectangle(wolfFrames.upRunDown3),

                    },
                [9] = new()
                    {

                        WolfRectangle(wolfFrames.rightRunDown2),
                        WolfRectangle(wolfFrames.rightRunDown3),

                    },
                [10] = new()
                    {

                        WolfRectangle(wolfFrames.downRunDown2),
                        WolfRectangle(wolfFrames.downRunDown3),

                    },
                [11] = new()
                    {

                        WolfRectangle(wolfFrames.rightRunDown2),
                        WolfRectangle(wolfFrames.rightRunDown3),

                    },

            };

        }

        public void ShadowFrames()
        {

            shadowFrames = new()
            {

                [0] = WolfRectangle(wolfFrames.upShadow),
                [1] = WolfRectangle(wolfFrames.rightShadow),
                [2] = WolfRectangle(wolfFrames.downShadow),
                [3] = WolfRectangle(wolfFrames.rightShadow),
            
            };

        }

        public void DrawNormal(SpriteBatch b, WolfRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            bool under = false;

            switch (use.series)
            {

                case WolfRenderAdditional.wolfseries.sweep:

                    source = sweepFrames[use.direction][use.frame];

                    break;

                case WolfRenderAdditional.wolfseries.special:

                    source = specialFrames[use.direction][use.frame];

                    break;

                case WolfRenderAdditional.wolfseries.rest:

                    source = restFrames[use.direction][use.frame];

                    under = true;

                    break;

                case WolfRenderAdditional.wolfseries.standby:

                    source = standbyFrames[use.direction][use.frame];

                    under = true;

                    break;

                case WolfRenderAdditional.wolfseries.dash:

                    source = dashFrames[use.direction][use.frame];

                    break;

                default:

                    source = walkFrames[use.direction][use.frame];

                    break;

            }

            b.Draw(
                wolfTexture,
                use.position,
                source,
                Microsoft.Xna.Framework.Color.White * use.fade,
                0f,
                new Vector2(source.Width / 2, source.Height / 2),
                use.scale,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer + 0.0001f
            );

            if (under)
            {

                b.Draw(
                    wolfTexture,
                    use.position + new Vector2(2,4),
                    source,
                    Microsoft.Xna.Framework.Color.Black * 0.15f * use.fade,
                    0f,
                    new Vector2(source.Width / 2, source.Height / 2),
                    use.scale,
                    use.flip ? (SpriteEffects)1 : 0,
                    use.layer + 0.0001f
                );

                return;

            }

            int useDirection = use.direction % 4;

            Microsoft.Xna.Framework.Rectangle shadow = shadowFrames[useDirection];

            Vector2 shadowPosition = use.position;

            if(useDirection % 2 == 1)
            {

                shadowPosition.Y += 8;

            }

            b.Draw(
                wolfTexture,
                shadowPosition,
                shadow,
                Color.White * 0.15f,
                0.0f,
                new Vector2(shadow.Width / 2, shadow.Height / 2),
                use.scale + (Math.Abs(0 - (walkFrames[0].Count() / 2) + use.scale) * 0.05f),
                use.flip ? (SpriteEffects)1 : 0,
                use.layer - 0.0001f
            );

        }

    }

    public class WolfRenderAdditional
    {

        public int direction = 2;

        public int frame = 0;

        public Vector2 position;

        public float scale = 4f;

        public bool flip = false;

        public float layer = 0.0001f;

        public enum wolfmode
        {
            none,
            growl,
        }

        public wolfmode mode;

        public enum wolfseries
        {
            none,
            sweep,
            dash,
            special,
            rest,
            standby,
        }

        public wolfseries series;

        public float fade = 1f;

    }

}
