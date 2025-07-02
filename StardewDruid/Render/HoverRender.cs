using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
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

        public Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> specialFrames = new();

        public Dictionary<Character.Character.dashes, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> dashFrames = new();

        public float hoverFloat;
        public int hoverMoment;
        public int hoverLapse;
        public int hoverLapseTwo;
        public int hoverLapseThree;
        public int hoverOffset;
        public float hoverAdjust;

        public HoverRender()
        {

        }

        public HoverRender(CharacterHandle.characters characterType)
        {

            hoverTexture = CharacterHandle.CharacterTexture(characterType);

            switch (characterType)
            {
                default:

                    WalkFrames();

                    SpecialFrames();

                    DashFrames();

                    hoverFloat = 0.15f;

                    hoverLapse = 24;

                    break;

                case CharacterHandle.characters.BoneWitch:
                case CharacterHandle.characters.PeatWitch:
                case CharacterHandle.characters.MoorWitch:

                    WitchWalkFrames();

                    WitchSpecialFrames();

                    WitchDashFrames();

                    hoverFloat = 0.15f;

                    hoverLapse = 24;

                    break;

            }

            hoverLapseTwo = 0 - (hoverLapse * walkFrames[0].Count);

            hoverLapseThree = hoverLapse * walkFrames[0].Count * 2;

            hoverOffset = Mod.instance.randomIndex.Next(12);

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
                    HoverRectangle(hoverFrames.upThree),
                    HoverRectangle(hoverFrames.upTwo),
                    HoverRectangle(hoverFrames.upTwo),
                    HoverRectangle(hoverFrames.upOne),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightThree),
                    HoverRectangle(hoverFrames.rightTwo),
                    HoverRectangle(hoverFrames.rightTwo),
                    HoverRectangle(hoverFrames.rightOne),

                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.downThree),
                    HoverRectangle(hoverFrames.downTwo),
                    HoverRectangle(hoverFrames.downTwo),
                    HoverRectangle(hoverFrames.downOne),

                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightThree),
                    HoverRectangle(hoverFrames.rightTwo),
                    HoverRectangle(hoverFrames.rightTwo),
                    HoverRectangle(hoverFrames.rightOne),

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
                    HoverRectangle(hoverFrames.upTwoSpecial),
                    HoverRectangle(hoverFrames.upThreeSpecial),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightOneSpecial),
                    HoverRectangle(hoverFrames.rightTwoSpecial),
                    HoverRectangle(hoverFrames.rightTwoSpecial),
                    HoverRectangle(hoverFrames.rightThreeSpecial),

                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.downOneSpecial),
                    HoverRectangle(hoverFrames.downTwoSpecial),
                    HoverRectangle(hoverFrames.downTwoSpecial),
                    HoverRectangle(hoverFrames.downThreeSpecial),

                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightOneSpecial),
                    HoverRectangle(hoverFrames.rightTwoSpecial),
                    HoverRectangle(hoverFrames.rightTwoSpecial),
                    HoverRectangle(hoverFrames.rightThreeSpecial),

                },
            };

            specialFrames[Character.Character.specials.sweep] = new()
            {

                [0] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.upDashOne),
                    HoverRectangle(hoverFrames.upDashTwo),
                    HoverRectangle(hoverFrames.upDashThree),
                    HoverRectangle(hoverFrames.upDashTwo),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightDashOne),
                    HoverRectangle(hoverFrames.rightDashTwo),
                    HoverRectangle(hoverFrames.rightDashThree),
                    HoverRectangle(hoverFrames.rightDashTwo),

                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.downDashOne),
                    HoverRectangle(hoverFrames.downDashTwo),
                    HoverRectangle(hoverFrames.downDashThree),
                    HoverRectangle(hoverFrames.downDashTwo),

                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightDashOne),
                    HoverRectangle(hoverFrames.rightDashTwo),
                    HoverRectangle(hoverFrames.rightDashThree),
                    HoverRectangle(hoverFrames.rightDashTwo),

                },
            };

            specialFrames[Character.Character.specials.tackle] = new()
            {

                [0] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.upDashOne),
                    HoverRectangle(hoverFrames.upDashTwo),
                    HoverRectangle(hoverFrames.upDashThree),
                    HoverRectangle(hoverFrames.upDashTwo),
                    HoverRectangle(hoverFrames.upDashOne),
                    HoverRectangle(hoverFrames.upDashTwo),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightDashOne),
                    HoverRectangle(hoverFrames.rightDashTwo),
                    HoverRectangle(hoverFrames.rightDashThree),
                    HoverRectangle(hoverFrames.rightDashTwo),
                    HoverRectangle(hoverFrames.rightDashOne),
                    HoverRectangle(hoverFrames.rightDashTwo),

                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.downDashOne),
                    HoverRectangle(hoverFrames.downDashTwo),
                    HoverRectangle(hoverFrames.downDashThree),
                    HoverRectangle(hoverFrames.downDashTwo),
                    HoverRectangle(hoverFrames.downDashOne),
                    HoverRectangle(hoverFrames.downDashTwo),

                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(hoverFrames.rightDashOne),
                    HoverRectangle(hoverFrames.rightDashTwo),
                    HoverRectangle(hoverFrames.rightDashThree),
                    HoverRectangle(hoverFrames.rightDashTwo),
                    HoverRectangle(hoverFrames.rightDashOne),
                    HoverRectangle(hoverFrames.rightDashTwo),

                },
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

        public enum witchFrames
        {

            downOne,
            downTwo,
            downThree,

            rightOne,
            rightTwo,
            rightThree,

            upOne,
            upTwo,
            upThree,

            downDashOne,
            downDashTwo,
            downDashThree,

            rightDashOne,
            rightDashTwo,
            rightDashThree,

            upDashOne,
            upDashTwo,
            upDashThree,

        }

        public static Microsoft.Xna.Framework.Rectangle WitchRectangle(witchFrames frame)
        {

            return new((int)frame % 3 * 64, (int)frame / 3 * 64, 64, 64);

        }

        public void WitchWalkFrames()
        {

            walkFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.upOne),
                    WitchRectangle(witchFrames.upTwo),
       
                    WitchRectangle(witchFrames.upTwo),
                    WitchRectangle(witchFrames.upThree),
                },

                [1] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.rightOne),
                    WitchRectangle(witchFrames.rightTwo),

                    WitchRectangle(witchFrames.rightTwo),
                    WitchRectangle(witchFrames.rightThree),
                },
                [2] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.downOne),
                    WitchRectangle(witchFrames.downTwo),

                    WitchRectangle(witchFrames.downTwo), 
                    WitchRectangle(witchFrames.downThree),
                },
                [3] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.rightOne),
                    WitchRectangle(witchFrames.rightTwo),

                    WitchRectangle(witchFrames.rightTwo),
                    WitchRectangle(witchFrames.rightThree),
                }
            };

        }

        public void WitchSpecialFrames()
        {

            specialFrames[Character.Character.specials.special] = new()
            {
                [0] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.upOne),
                    WitchRectangle(witchFrames.upTwo),

                    WitchRectangle(witchFrames.upTwo),
                    WitchRectangle(witchFrames.upThree),
                },

                [1] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.rightOne),
                    WitchRectangle(witchFrames.rightTwo),

                    WitchRectangle(witchFrames.rightTwo),
                    WitchRectangle(witchFrames.rightThree),
                },
                [2] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.downOne),
                    WitchRectangle(witchFrames.downTwo),

                    WitchRectangle(witchFrames.downTwo),
                    WitchRectangle(witchFrames.downThree),
                },
                [3] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.rightOne),
                    WitchRectangle(witchFrames.rightTwo),

                    WitchRectangle(witchFrames.rightTwo),
                    WitchRectangle(witchFrames.rightThree),
                }

            };

        }

        public void WitchDashFrames()
        {

            dashFrames[Character.Character.dashes.dash] = new()
            {
                [0] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.upDashOne),
                    WitchRectangle(witchFrames.upDashTwo),

                },
                [1] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.rightDashOne),
                    WitchRectangle(witchFrames.rightDashTwo),


                },
                [2] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.downDashOne),
                    WitchRectangle(witchFrames.downDashTwo),


                },
                [3] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.rightDashOne),
                    WitchRectangle(witchFrames.rightDashTwo),


                },
                [4] = new List<Rectangle>()
                {

                    WitchRectangle(witchFrames.upDashThree),
                    WitchRectangle(witchFrames.upDashTwo),
                },
                [5] = new List<Rectangle>()
                {

                    WitchRectangle(witchFrames.rightDashThree),
                    WitchRectangle(witchFrames.rightDashTwo),

                },
                [6] = new List<Rectangle>()
                {

                    WitchRectangle(witchFrames.downDashThree),
                    WitchRectangle(witchFrames.downDashTwo),

                },
                [7] = new List<Rectangle>()
                {

                    WitchRectangle(witchFrames.rightDashThree),
                    WitchRectangle(witchFrames.rightDashTwo),

                },
                [8] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.upDashOne),
                    WitchRectangle(witchFrames.upDashTwo),

                },
                [9] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.rightDashOne),
                    WitchRectangle(witchFrames.rightDashTwo),


                },
                [10] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.downDashOne),
                    WitchRectangle(witchFrames.downDashTwo),


                },
                [11] = new List<Rectangle>()
                {
                    WitchRectangle(witchFrames.rightDashOne),
                    WitchRectangle(witchFrames.rightDashTwo),


                }

            };

        }

        public int HoverFrame()
        {

            return Math.Min((int)(hoverMoment / hoverLapse), 3);

        }

        public void Update(bool adjust = false)
        {

            hoverOffset++;

            //hoverOffset++;

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

        public void DrawNormal(SpriteBatch b, HoverRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            Vector2 shadowPosition = use.position + new Vector2(0, use.scale * 18);

            use.position = use.position + new Vector2(0, hoverAdjust*use.scale);

            switch (use.series)
            {

                default:
                case HoverRenderAdditional.hoverseries.none:

                    use.frame = HoverFrame();

                    source = walkFrames[use.direction][use.frame];

                    break;

                case HoverRenderAdditional.hoverseries.special:


                    source = specialFrames[Character.Character.specials.special][use.direction][use.frame];

                    break;

                case HoverRenderAdditional.hoverseries.sweep:

                    source = specialFrames[Character.Character.specials.sweep][use.direction][use.frame];

                    break;

                case HoverRenderAdditional.hoverseries.tackle:

                    source = specialFrames[Character.Character.specials.tackle][use.direction][use.frame];

                    break;

                case HoverRenderAdditional.hoverseries.dash:

                    source = dashFrames[Character.Character.dashes.dash][use.direction][use.frame];

                    break;

            }

            if (use.direction % 2 == 1)
            {
                shadowPosition.Y += 4;
            }

            float shadowScale = 0.5f + Math.Abs(0 - 0.1f + (((float)hoverOffset / (float)hoverLapseThree) * 0.2f));

            b.Draw(
                Mod.instance.iconData.cursorTexture,
                shadowPosition,
                Mod.instance.iconData.shadowRectangle,
                Color.White * 0.25f * use.fade,
                0.0f,
                new Vector2(24),
                use.scale * shadowScale,
                0,
                use.layer - 0.0001f
            );

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
            dash,
            special,
            sweep,
            tackle,
        }

        public hoverseries series;

        public float fade = 1f;

    }

}
