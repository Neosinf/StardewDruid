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
using static StardewDruid.Character.Character;

namespace StardewDruid.Render
{
    public class BatRender
    {

        public CharacterHandle.characters batType;

        public Texture2D batTexture;

        public enum batFrames
        {

            downOne,
            downTwo,
            downThree,
            downFour,
            standby,
            standbyTwo,

            rightOne,
            rightTwo,
            rightThree,
            rightFour,
            rightBlank,
            rightBlankTwo,

            upOne,
            upTwo,
            upThree,
            upFour,
            upBlank,
            upBlankTwo,

        }

        public enum batDashFrames
        {

            down,
            right,
            up,

        }

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> walkFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> restFrames = new();

        public Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> specialFrames = new();

        public Dictionary<Character.Character.dashes, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> dashFrames = new();

        public float batFloat;
        public int batMoment;
        public int batLapse;
        public int batLapseTwo;
        public int batLapseThree;
        public int batOffset;
        public float batAdjust;
        public int batRaise;

        public BatRender()
        {

        }

        public BatRender(CharacterHandle.characters characterType)
        {

            batType = characterType;

            batTexture = CharacterHandle.CharacterTexture(characterType);

            switch (characterType)
            {

                default: // Bat

                    WalkFrames();

                    RestFrames();

                    SpecialFrames();

                    DashFrames();

                    batFloat = 0.35f;

                    batLapse = 12;

                    break;


            }

            batLapseTwo = 0 - (batLapse * walkFrames[0].Count);

            batLapseThree = batLapse * walkFrames[0].Count * 2;

            batOffset = Mod.instance.randomIndex.Next(12);

        }

        public Microsoft.Xna.Framework.Rectangle HoverRectangle(batFrames frame, bool special = false)
        {

            switch (batType)
            {

                case CharacterHandle.characters.Batking:
                case CharacterHandle.characters.Batcleric:

                    Rectangle largerect = new((int)frame % 6 * 64, (int)frame / 6 * 64, 64, 64);

                    if (special)
                    {

                        largerect.Y += 192;

                    }

                    return largerect;

                default:

                    Rectangle rect = new((int)frame % 6 * 32, (int)frame / 6 * 32, 32, 32);

                    if (special)
                    {

                        rect.Y += 96;

                    }

                    return rect;

            }

        }

        public Microsoft.Xna.Framework.Rectangle DashRectangle(batDashFrames frame)
        {
            switch (batType)
            {

                case CharacterHandle.characters.Batking:
                case CharacterHandle.characters.Batcleric:

                    return new(256, 192 + (int)frame % 3 * 64, 64, 64);

                default:

                    return new((int)frame % 3 * 64, 192, 64, 64);

            }

        }

        public void WalkFrames()
        {

            walkFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.upThree),
                    HoverRectangle(batFrames.upTwo),
                    HoverRectangle(batFrames.upTwo),
                    HoverRectangle(batFrames.upOne),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightThree),
                    HoverRectangle(batFrames.rightTwo),
                    HoverRectangle(batFrames.rightTwo),
                    HoverRectangle(batFrames.rightOne),

                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.downThree),
                    HoverRectangle(batFrames.downTwo),
                    HoverRectangle(batFrames.downTwo),
                    HoverRectangle(batFrames.downOne),

                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightThree),
                    HoverRectangle(batFrames.rightTwo),
                    HoverRectangle(batFrames.rightTwo),
                    HoverRectangle(batFrames.rightOne),

                },
                [4] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.upThree),
                    HoverRectangle(batFrames.upFour),
                    HoverRectangle(batFrames.upFour),
                    HoverRectangle(batFrames.upOne),
                },
                [5] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightThree),
                    HoverRectangle(batFrames.rightFour),
                    HoverRectangle(batFrames.rightFour),
                    HoverRectangle(batFrames.rightOne),

                },
                [6] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.downThree),
                    HoverRectangle(batFrames.downFour),
                    HoverRectangle(batFrames.downFour),
                    HoverRectangle(batFrames.downOne),
                },
                [7] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightThree),
                    HoverRectangle(batFrames.rightFour),
                    HoverRectangle(batFrames.rightFour),
                    HoverRectangle(batFrames.rightOne),

                }
            };

        }
        public void RestFrames()
        {

            restFrames = new()
            {

                [0] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.standby),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.standby),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.standby),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.standby),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                    HoverRectangle(batFrames.standbyTwo),
                }
            };

        }

        public void SpecialFrames()
        {

            specialFrames[Character.Character.specials.special] = new()
            {
                [0] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.upThree, true),
                    HoverRectangle(batFrames.upTwo, true),
                    HoverRectangle(batFrames.upTwo, true),
                    HoverRectangle(batFrames.upOne, true),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightThree, true),
                    HoverRectangle(batFrames.rightTwo, true),
                    HoverRectangle(batFrames.rightTwo, true),
                    HoverRectangle(batFrames.rightOne, true),

                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.downThree, true),
                    HoverRectangle(batFrames.downTwo, true),
                    HoverRectangle(batFrames.downTwo, true),
                    HoverRectangle(batFrames.downOne, true),

                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightThree, true),
                    HoverRectangle(batFrames.rightTwo, true),
                    HoverRectangle(batFrames.rightTwo, true),
                    HoverRectangle(batFrames.rightOne, true),

                },
                [4] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.upThree, true),
                    HoverRectangle(batFrames.upFour, true),
                    HoverRectangle(batFrames.upFour, true),
                    HoverRectangle(batFrames.upOne, true),
                },
                [5] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightThree, true),
                    HoverRectangle(batFrames.rightFour, true),
                    HoverRectangle(batFrames.rightFour, true),
                    HoverRectangle(batFrames.rightOne, true),

                },
                [6] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.downThree, true),
                    HoverRectangle(batFrames.downFour, true),
                    HoverRectangle(batFrames.downFour, true),
                    HoverRectangle(batFrames.downOne, true),

                },
                [7] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightThree, true),
                    HoverRectangle(batFrames.rightFour, true),
                    HoverRectangle(batFrames.rightFour, true),
                    HoverRectangle(batFrames.rightOne, true),

                }
            };

            specialFrames[Character.Character.specials.sweep] = new()
            {

                [0] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.upOne, true),
                    DashRectangle(batDashFrames.up),
                    DashRectangle(batDashFrames.up),
                    HoverRectangle(batFrames.upOne, true),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightOne, true),
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                    HoverRectangle(batFrames.rightOne, true),
                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.downOne, true),
                    DashRectangle(batDashFrames.down),
                    DashRectangle(batDashFrames.down),
                    HoverRectangle(batFrames.downOne, true),
                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightOne, true),
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                    HoverRectangle(batFrames.rightOne, true),
                }
            };

            specialFrames[Character.Character.specials.tackle] = new()
            {
                [0] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.upOne, true),
                    DashRectangle(batDashFrames.up),
                    DashRectangle(batDashFrames.up),
                    DashRectangle(batDashFrames.up),
                    DashRectangle(batDashFrames.up),
                    DashRectangle(batDashFrames.up),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightOne, true),
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.downOne, true),
                    DashRectangle(batDashFrames.down),
                    DashRectangle(batDashFrames.down),
                    DashRectangle(batDashFrames.down),
                    DashRectangle(batDashFrames.down),
                    DashRectangle(batDashFrames.down),
                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightOne, true),
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                }
            };

        }

        public void DashFrames()
        {

            dashFrames[Character.Character.dashes.dash] = new()
            {
                [0] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.upOne, true),
                    DashRectangle(batDashFrames.up),
                },
                [1] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightOne, true),
                    DashRectangle(batDashFrames.right),
                },
                [2] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.downOne, true),
                    DashRectangle(batDashFrames.down),
                },
                [3] = new List<Rectangle>()
                {
                    HoverRectangle(batFrames.rightOne, true),
                    DashRectangle(batDashFrames.right),
                },
                [4] = new List<Rectangle>()
                {
                    DashRectangle(batDashFrames.up),
                    DashRectangle(batDashFrames.up),
                },
                [5] = new List<Rectangle>()
                {
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                },
                [6] = new List<Rectangle>()
                {
                    DashRectangle(batDashFrames.down),
                    DashRectangle(batDashFrames.down),
                },
                [7] = new List<Rectangle>()
                {
                    DashRectangle(batDashFrames.right),
                    DashRectangle(batDashFrames.right),
                },
                [8] = new List<Rectangle>()
                {
                    DashRectangle(batDashFrames.up),
                    HoverRectangle(batFrames.upOne, true),
                },
                [9] = new List<Rectangle>()
                {
                    DashRectangle(batDashFrames.right),
                    HoverRectangle(batFrames.rightOne, true),
                },
                [10] = new List<Rectangle>()
                {
                    DashRectangle(batDashFrames.down),
                    HoverRectangle(batFrames.downOne, true),
                },
                [11] = new List<Rectangle>()
                {
                    DashRectangle(batDashFrames.right),
                    HoverRectangle(batFrames.rightOne, true),
                }

            };

        }

        public int HoverFrame()
        {

            return Math.Min((int)(batMoment / batLapse), 3);

        }
        
        public int HoverDirection(int direction)
        {

            if (batRaise < 0)
            {

                return direction + 4;

            }

            return direction;

        }

        public void Update(bool adjust = false)
        {

            batOffset++;

            batRaise = batLapseTwo + batOffset;

            batMoment = Math.Abs(batRaise);

            if (batOffset >= batLapseThree)
            {

                batOffset = 0;

            }

            if (adjust)
            {

                batAdjust = (batLapseTwo + batMoment) * batFloat;

            }
            else if (batAdjust > 0)
            {

                batAdjust -= batFloat;

            }

        }

        public void DrawNormal(SpriteBatch b, BatRenderAdditional use)
        {

            Microsoft.Xna.Framework.Rectangle source;

            Vector2 shadowPosition = use.position + new Vector2(0, use.scale * 18);

            Vector2 usePosition = use.position + new Vector2(0, batAdjust*use.scale);

            int useDirection = use.direction;

            switch (use.series)
            {

                default:
                case BatRenderAdditional.batseries.none:

                    use.frame = HoverFrame();

                    useDirection = HoverDirection(use.direction);

                    source = walkFrames[use.direction][use.frame];

                    break;

                case BatRenderAdditional.batseries.special:

                    use.frame = HoverFrame();

                    useDirection = HoverDirection(use.direction);

                    source = specialFrames[Character.Character.specials.special][useDirection][use.frame];

                    break;

                case BatRenderAdditional.batseries.sweep:

                    source = specialFrames[Character.Character.specials.sweep][use.direction][use.frame];

                    break;

                case BatRenderAdditional.batseries.tackle:

                    source = specialFrames[Character.Character.specials.tackle][use.direction][use.frame];

                    break;

                case BatRenderAdditional.batseries.dash:

                    source = dashFrames[Character.Character.dashes.dash][use.direction][use.frame];

                    break;

                case BatRenderAdditional.batseries.rest:

                    source = restFrames[use.direction][use.frame];

                    b.Draw(
                        Mod.instance.iconData.shadowTexture,
                        shadowPosition,
                        Mod.instance.iconData.shadowRectangle,
                        Color.White * 0.25f * use.fade,
                        0.0f,
                        new Vector2(24),
                        use.scale * 0.5f,
                        0,
                        use.layer - 0.0001f
                    );

                    b.Draw(
                        batTexture,
                        use.position,
                        source,
                        Microsoft.Xna.Framework.Color.White * use.fade,
                        0f,
                        new Vector2(source.Width / 2, source.Height / 2),
                        use.scale,
                        use.flip ? (SpriteEffects)1 : 0,
                        use.layer + 0.0002f
                    );

                    return;
            }

            if (use.direction % 2 == 1)
            {
                shadowPosition.Y += 4;
            }

            float shadowScale = 0.5f + Math.Abs(0 - 0.1f + (((float)batOffset / (float)batLapseThree) * 0.2f));

            b.Draw(
                Mod.instance.iconData.shadowTexture,
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
                batTexture,
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

    public class BatRenderAdditional
    {

        public int direction = 2;

        public int frame = 0;

        public Vector2 position;

        public float scale = 4f;

        public bool flip = false;

        public float layer = 0.0001f;

        public enum batseries
        {
            none,
            dash,
            special,
            sweep,
            tackle,
            rest,
        }

        public batseries series;

        public float fade = 1f;

    }

}
