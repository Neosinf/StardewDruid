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
    public class HoverRender
    {

        public Texture2D hoverTexture;

        public enum hoverFrames
        {

            downOne,
            downTwo,
            downThree,
            downOneSpecial,
            downTwoSpecial,
            downThreeSpecial,

            rightOne,
            rightTwo,
            rightThree,
            rightOneSpecial,
            rightTwoSpecial,
            rightThreeSpecial,

            upOne,
            upTwo,
            upThree,
            upOneSpecial,
            upTwoSpecial,
            upThreeSpecial,

            downDashOne,
            d1,
            downDashTwo,
            d2,
            downDashThree,
            d3,

            b1, b2, b3, b4, b5, b6,

            rightDashOne,
            d4,
            rightDashTwo,
            d5,
            rightDashThree,
            d6,

            b7, b8, b9, b10, b11, b12,

            upDashOne,
            d7,
            upDashTwo,
            d8,
            upDashThree,
            d9,

            b13, b14, b15, b16, b17, b18,

            downHat,
            rightHat,
            upHat,

        }

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> walkFrames = new();
        
        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> hatFrames = new();

        public Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> specialFrames = new();

        public Dictionary<Character.Character.dashes, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> dashFrames = new();

        //public Dictionary<int, Microsoft.Xna.Framework.Rectangle> shadowFrames = new();

        public HoverRender(string name)
        {

            hoverTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", name+".png"));

            WalkFrames();

            SpecialFrames();

            DashFrames();

            HatFrames();

        }

        public static Microsoft.Xna.Framework.Rectangle HoverRectangle(hoverFrames frame)
        {

            if((int)frame > 17 && (int)frame < 54)
            {

                return new((int)frame % 6 * 32, (int)frame / 6 * 32, 64, 64);

            }

            return new((int)frame % 6 * 32, (int)frame / 6 * 32, 32, 32);
        }


        public void WalkFrames()
        {

            walkFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),

                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),

                }
            };

        }

        public void SpecialFrames()
        {

            specialFrames[Character.Character.specials.special] = new()
            {

                [0] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.upOneSpecial),
                    HoverRectangle(hoverFrames.upTwoSpecial),
                    HoverRectangle(hoverFrames.upThreeSpecial),
                    HoverRectangle(hoverFrames.upTwoSpecial),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightOneSpecial),
                    HoverRectangle(hoverFrames.rightTwoSpecial),
                    HoverRectangle(hoverFrames.rightThreeSpecial),
                    HoverRectangle(hoverFrames.rightTwoSpecial),

                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.downOneSpecial),
                    HoverRectangle(hoverFrames.downTwoSpecial),
                    HoverRectangle(hoverFrames.downThreeSpecial),
                    HoverRectangle(hoverFrames.downTwoSpecial),

                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightOneSpecial),
                    HoverRectangle(hoverFrames.rightTwoSpecial),
                    HoverRectangle(hoverFrames.rightThreeSpecial),
                    HoverRectangle(hoverFrames.rightTwoSpecial),

                }

            };

        }

        public void DashFrames()
        {

            dashFrames[Character.Character.dashes.dash] = new()
            {
                [0] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.upDashOne),
                    HoverRectangle(hoverFrames.upDashTwo),

                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightDashOne),
                    HoverRectangle(hoverFrames.rightDashTwo),


                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.downDashOne),
                    HoverRectangle(hoverFrames.downDashTwo),


                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightDashOne),
                    HoverRectangle(hoverFrames.rightDashTwo),


                },
                [4] = new List<Rectangle>()
                {

                    HoverRectangle(hoverFrames.upDashThree),
                    HoverRectangle(hoverFrames.upDashTwo),
                },
                [5] = new List<Rectangle>()
                {

                    HoverRectangle(hoverFrames.rightDashThree),
                    HoverRectangle(hoverFrames.rightDashTwo),

                },
                [6] = new List<Rectangle>()
                {

                    HoverRectangle(hoverFrames.downDashThree),
                    HoverRectangle(hoverFrames.downDashTwo),

                },
                [7] = new List<Rectangle>()
                {

                    HoverRectangle(hoverFrames.rightDashThree),
                    HoverRectangle(hoverFrames.rightDashTwo),

                },
                [8] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.upDashOne),
                    HoverRectangle(hoverFrames.upDashTwo),

                },
                [9] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightDashOne),
                    HoverRectangle(hoverFrames.rightDashTwo),


                },
                [10] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.downDashOne),
                    HoverRectangle(hoverFrames.downDashTwo),


                },
                [11] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightDashOne),
                    HoverRectangle(hoverFrames.rightDashTwo),


                }

            };

        }
        
        public void HatFrames()
        {

            hatFrames = new()
            {
                [0] = new()
                {
                    HoverRectangle(hoverFrames.upHat),
                },
                [1] = new()
                {
                    HoverRectangle(hoverFrames.rightHat),
                },
                [2] = new()
                {
                    HoverRectangle(hoverFrames.downHat),
                },
                [3] = new()
                {
                    HoverRectangle(hoverFrames.rightHat),
                },
            };

        }

        public void DrawNormal(SpriteBatch b, HoverRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            switch (use.series)
            {

                default:
                case HoverRenderAdditional.hoverseries.hover:

                    source = walkFrames[use.direction][use.frame];

                    break;

                case HoverRenderAdditional.hoverseries.special:

                    source = specialFrames[Character.Character.specials.special][use.direction][use.frame];

                    break;

                case HoverRenderAdditional.hoverseries.dash:

                    source = dashFrames[Character.Character.dashes.dash][use.direction][use.frame];
                    break;

            }

            b.Draw(
                hoverTexture,
                use.position,
                source,
                Microsoft.Xna.Framework.Color.White * use.fade,
                0f,
                new Vector2(source.Width / 2, source.Height / 2),
                use.scale,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer + 0.0002f
            );

            Vector2 shadowPosition = use.position + new Vector2(0, use.scale * 18);

            float offset = 2f + (Math.Abs(0 - (walkFrames[0].Count() / 2) + use.frame) * 0.1f);

            if (use.direction % 2 == 1)
            {
                shadowPosition.Y += 4;
            }

            b.Draw(
                Mod.instance.iconData.cursorTexture,
                shadowPosition,
                Mod.instance.iconData.shadowRectangle,
                Color.White * 0.35f,
                0.0f,
                new Vector2(24),
                use.scale / offset,
                0,
                use.layer - 0.0001f
            );

            if (use.hat)
            {

                int hatFrame = use.direction % 4;

                b.Draw(
                    hoverTexture,
                    use.position,
                    hatFrames[hatFrame][0],
                    Microsoft.Xna.Framework.Color.White * use.fade,
                    0f,
                    new Vector2(source.Width / 2, source.Height / 2),
                    use.scale,
                    use.flip ? (SpriteEffects)1 : 0,
                    use.layer + 0.0002f
                );

            }

        }

    }

    public class HoverRenderAdditional
    {

        public int direction = 2;

        public int frame = 0;

        public Vector2 position;

        public float scale = 4f;

        public bool flip = false;

        public float layer = 0.0001f;

        public enum hoverseries
        {
            none,
            hover,
            dash,
            special,
        }

        public hoverseries series;

        public bool hat = false;

        public float fade = 1f;

    }

}
