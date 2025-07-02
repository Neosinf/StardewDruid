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
    public class GhostkingRender
    {

        public Texture2D ghostTexture;

        public enum ghostFrames
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
            restOne,
            restTwo,
            restThree,

            upOne,
            upTwo,
            upThree,
            shadow,
            blankTwo,
            blankThree,

            downDashOne,
            downDashTwo,
            downDashThree,
            downDashFour,
            downSwipe,
            downBlank,

            rightDashOne,
            rightDashTwo,
            rightDashThree,
            rightDashFour,
            rightSwipe,
            rightBlank,

            upDashOne,
            upDashTwo,
            upDashThree,
            upDashFour,
            upSwipe,
            upBlank,
        }

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> walkFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> restFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> shadowFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> swipeFrames = new();

        public Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> specialFrames = new();

        public Dictionary<Character.Character.dashes, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> dashFrames = new();

        public float hoverFloat;
        public int hoverMoment;
        public int hoverLapse;
        public int hoverLapseTwo;
        public int hoverLapseThree;
        public int hoverOffset;
        public float hoverAdjust;

        public int swipeEffect;

        public GhostkingRender()
        {

        }

        public GhostkingRender(CharacterHandle.characters characterType)
        {

            ghostTexture = CharacterHandle.CharacterTexture(characterType);

            WalkFrames();

            SpecialFrames();

            DashFrames();

            hoverFloat = 0.25f;

            hoverLapse = 24;

            hoverLapseTwo = 0 - (hoverLapse * walkFrames[0].Count);

            hoverLapseThree = hoverLapse * walkFrames[0].Count * 2;

            hoverOffset = Mod.instance.randomIndex.Next(12);

        }

        public static Microsoft.Xna.Framework.Rectangle GhostRectangle(ghostFrames frame)
        {

            return new((int)frame % 6 * 64, (int)frame / 6 * 64, 64, 64);

        }


        public void WalkFrames()
        {

            walkFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.upOne),
                    GhostRectangle(ghostFrames.upOne),
                    GhostRectangle(ghostFrames.upOne),
                    GhostRectangle(ghostFrames.upOne),
                },
                [1] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightOne),
                    GhostRectangle(ghostFrames.rightOne),
                    GhostRectangle(ghostFrames.rightOne),
                    GhostRectangle(ghostFrames.rightOne),
                },
                [2] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downOne),
                    GhostRectangle(ghostFrames.downOne),
                    GhostRectangle(ghostFrames.downOne),
                    GhostRectangle(ghostFrames.downOne),
                },
                [3] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightOne),
                    GhostRectangle(ghostFrames.rightOne),
                    GhostRectangle(ghostFrames.rightOne),
                    GhostRectangle(ghostFrames.rightOne),

                }
            };

            restFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.restOne),
                    GhostRectangle(ghostFrames.restTwo),
                    GhostRectangle(ghostFrames.restThree),
                },
                [1] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.restOne),
                    GhostRectangle(ghostFrames.restTwo),
                    GhostRectangle(ghostFrames.restThree),

                },
                [2] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.restOne),
                    GhostRectangle(ghostFrames.restTwo),
                    GhostRectangle(ghostFrames.restThree),

                },
                [3] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.restOne),
                    GhostRectangle(ghostFrames.restTwo),
                    GhostRectangle(ghostFrames.restThree),

                }
            };

            shadowFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.shadow),
                },
                [1] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.shadow),

                },
                [2] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.shadow),
                },
                [3] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.shadow),
                }
            };

            swipeFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.upSwipe),
                },
                [1] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightSwipe),

                },
                [2] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downSwipe),
                },
                [3] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightSwipe),
                }
            };

        }

        public void SpecialFrames()
        {

            specialFrames[Character.Character.specials.special] = new()
            {

                [0] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downOneSpecial),
                    GhostRectangle(ghostFrames.downTwoSpecial),
                    GhostRectangle(ghostFrames.downThreeSpecial),
                    GhostRectangle(ghostFrames.downOneSpecial),
                    GhostRectangle(ghostFrames.downTwoSpecial),
                    GhostRectangle(ghostFrames.downThreeSpecial),
                },
                [1] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downOneSpecial),
                    GhostRectangle(ghostFrames.downTwoSpecial),
                    GhostRectangle(ghostFrames.downThreeSpecial),
                    GhostRectangle(ghostFrames.downOneSpecial),
                    GhostRectangle(ghostFrames.downTwoSpecial),
                    GhostRectangle(ghostFrames.downThreeSpecial),

                },
                [2] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downOneSpecial),
                    GhostRectangle(ghostFrames.downTwoSpecial),
                    GhostRectangle(ghostFrames.downThreeSpecial),
                    GhostRectangle(ghostFrames.downOneSpecial),
                    GhostRectangle(ghostFrames.downTwoSpecial),
                    GhostRectangle(ghostFrames.downThreeSpecial),
                },
                [3] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downOneSpecial),
                    GhostRectangle(ghostFrames.downTwoSpecial),
                    GhostRectangle(ghostFrames.downThreeSpecial),
                    GhostRectangle(ghostFrames.downOneSpecial),
                    GhostRectangle(ghostFrames.downTwoSpecial),
                    GhostRectangle(ghostFrames.downThreeSpecial),
                },
            };

            specialFrames[Character.Character.specials.sweep] = new()
            {

                [0] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.upDashOne),
                    GhostRectangle(ghostFrames.upDashOne),
                    GhostRectangle(ghostFrames.upDashOne),
                    GhostRectangle(ghostFrames.upDashFour),
                    GhostRectangle(ghostFrames.upDashFour),
                },
                [1] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashFour),
                    GhostRectangle(ghostFrames.rightDashFour),
                },
                [2] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downDashOne),
                    GhostRectangle(ghostFrames.downDashOne),
                    GhostRectangle(ghostFrames.downDashOne),
                    GhostRectangle(ghostFrames.downDashFour),
                    GhostRectangle(ghostFrames.downDashFour),
                },
                [3] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashFour),
                    GhostRectangle(ghostFrames.rightDashFour),
                },
            };

            specialFrames[Character.Character.specials.tackle] = new()
            {

                [0] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.upDashOne),
                    GhostRectangle(ghostFrames.upDashOne),
                    GhostRectangle(ghostFrames.upDashOne),
                    GhostRectangle(ghostFrames.upDashFour),
                    GhostRectangle(ghostFrames.upDashFour),
                },
                [1] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashFour),
                    GhostRectangle(ghostFrames.rightDashFour),
                },
                [2] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downDashOne),
                    GhostRectangle(ghostFrames.downDashOne),
                    GhostRectangle(ghostFrames.downDashOne),
                    GhostRectangle(ghostFrames.downDashFour),
                    GhostRectangle(ghostFrames.downDashFour),
                },
                [3] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashFour),
                    GhostRectangle(ghostFrames.rightDashFour),
                },
            };

        }

        public void DashFrames()
        {

            dashFrames[Character.Character.dashes.dash] = new()
            {
                [0] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.upDashOne),
                    GhostRectangle(ghostFrames.upDashOne),
                    GhostRectangle(ghostFrames.upDashOne),
                },
                [1] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                },
                [2] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downDashOne),
                    GhostRectangle(ghostFrames.downDashOne),
                    GhostRectangle(ghostFrames.downDashOne),
                },
                [3] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                },
                [4] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.upDashOne),
                    GhostRectangle(ghostFrames.upDashOne),
                    GhostRectangle(ghostFrames.upDashOne),
                },
                [5] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                },
                [6] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downDashOne),
                    GhostRectangle(ghostFrames.downDashOne),
                    GhostRectangle(ghostFrames.downDashOne),
                },
                [7] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                    GhostRectangle(ghostFrames.rightDashOne),
                },
                [8] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.upDashFour),

                },
                [9] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightDashFour),

                },
                [10] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.downDashFour),

                },
                [11] = new List<Rectangle>()
                {
                    GhostRectangle(ghostFrames.rightDashFour),

                }

            };

        }

        public int GhostFrame()
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

        public void DrawNormal(SpriteBatch b, GhostkingRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            Vector2 shadowPosition = use.position + new Vector2(0, use.scale * 24);

            //use.position = use.position + new Vector2(0, hoverAdjust*use.scale);

            bool swipeAnimation = false;

            switch (use.series)
            {

                default:
                case GhostkingRenderAdditional.ghostseries.none:

                    use.frame = GhostFrame();

                    source = walkFrames[use.direction][use.frame];

                    break;

                case GhostkingRenderAdditional.ghostseries.special:

                    use.frame = GhostFrame();

                    source = specialFrames[Character.Character.specials.special][use.direction][use.frame];

                    break;

                case GhostkingRenderAdditional.ghostseries.sweep:

                    source = specialFrames[Character.Character.specials.sweep][use.direction][use.frame];

                    if (use.frame > 2)
                    {

                        swipeAnimation = true;

                    }

                    break;

                case GhostkingRenderAdditional.ghostseries.tackle:

                    source = specialFrames[Character.Character.specials.tackle][use.direction][use.frame];

                    if (use.frame > 2)
                    {

                        swipeAnimation = true;

                    }

                    break;

                case GhostkingRenderAdditional.ghostseries.dash:

                    source = dashFrames[Character.Character.dashes.dash][use.direction][use.frame];

                    if(use.direction > 7)
                    {

                        swipeAnimation = true;

                    }

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
                shadowFrames[0][0],
                Color.White * 0.25f * use.fade,
                0.0f,
                new Vector2(24),
                use.scale * shadowScale,
                0,
                use.layer - 0.0001f
            );

            b.Draw(
                ghostTexture,
                use.position,
                source,
                Microsoft.Xna.Framework.Color.White * use.fade,
                0f,
                new Vector2(source.Width / 2, source.Height / 2),
                use.scale,
                use.flip ? (SpriteEffects)1 : 0,
                use.layer + 0.0002f
            );

            if (swipeAnimation)
            {

                if(swipeEffect == -1)
                {

                    swipeEffect = 100;

                }

                swipeEffect -= 5;

                b.Draw(
                    ghostTexture,
                    use.position,
                    swipeFrames[(use.direction % 4)][0],
                    Microsoft.Xna.Framework.Color.White * ((float)swipeEffect / 100) * use.fade,
                    0f,
                    new Vector2(source.Width / 2, source.Height / 2),
                    use.scale,
                    use.flip ? (SpriteEffects)1 : 0,
                    use.layer + 0.0003f
                );

            }
            else
            {

                swipeEffect = -1;

            }

        }

    }

    public class GhostkingRenderAdditional
    {

        public int direction = 2;

        public int frame = 0;

        public Vector2 position;

        public float scale = 4f;

        public bool flip = false;

        public float layer = 0.0001f;

        public enum ghostseries
        {
            none,
            dash,
            special,
            sweep,
            tackle,
        }

        public ghostseries series;

        public float fade = 1f;

    }

}
