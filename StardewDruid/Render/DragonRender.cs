﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netcode;
using System.ComponentModel;
using StardewDruid.Data;

namespace StardewDruid.Render
{
    public class DragonRender
    {
        // ============================= Dragon Specific


        public Texture2D characterTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Dragon.png"));

        public Texture2D dashTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DragonDash.png"));

        public Texture2D breathTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DragonBreath.png"));

        public Texture2D breathTextureTwo = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DragonBreathTwo.png"));

        public Texture2D dinosaurTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Dinosaur.png"));

        // ============================================================

        public Color primary = new(190, 30, 45);

        public Color secondary = new(191, 142, 93);

        public Color tertiary = new(39, 170, 225);

        public breathSchemes fire = breathSchemes.breath_stars;

        public enum dragonIndexes
        {
            // line 1
            head_2,
            head_2S,
            body_2,
            back_2,
            submerge_1,
            blank2,
            blank3,
            // line 2
            head_1,
            head_1S,
            body_1,
            back_1,
            emerge_1,
            blank5,
            blank6,
            // line 3
            head_0,
            head_0S,
            body_0,
            back_0,
            blank7,
            blank8,
            blank9,
            // line 4
            idle_2,
            walk_21,
            walk_22,
            walk_23,
            walk_24,
            walk_25,
            walk_26,
            // line 5
            idle_1,
            walk_11,
            walk_12,
            walk_13,
            walk_14,
            walk_15,
            walk_16,
            // line 6
            idle_0,
            walk_01,
            walk_02,
            walk_03,
            walk_04,
            walk_05,
            walk_06,

        }

        public enum dragonShadows
        {
            walk_0,
            walk_1,
            walk_2,
            blank1,
            swim_21,
            swim_22,
            swim_11,
            swim_12,
            swim_01,
            swim_02,
            swim_submerge_1,
            swim_submerge_2,
            swim_emerge_1,
            swim_emerge_2,
        }

        public enum dashIndexes
        {
            // line 1
            head_11,
            head_12,
            head_11S,
            head_12S,
            head_11T,
            head_12T,
            blank1,
            blank2,
            // line 2
            head_01,
            head_02,
            head_01S,
            head_02S,
            head_01T,
            head_02T,
            blank3,
            blank4,
            // line 3
            head_21,
            head_22,
            head_21S,
            head_22S,
            head_21T,
            head_22T,
            blank5,
            blank6,
            // line 4
            head_41,
            blank7,
            head_41S,
            blank8,
            head_41T,
            blank9,
            blank10,
            blank11,
            // line 5
            wing_11,
            blank12,
            wing_12,
            blank13,
            wing_13,
            blank14,
            wing_14,
            blank15,
            // line 6
            wing_01,
            blank16,
            wing_02,
            blank17,
            wing_03,
            blank18,
            wing_04,
            blank19,
            // line 7
            wing_21,
            blank20,
            wing_22,
            blank21,
            wing_23,
            blank22,
            wing_24,
            blank23,
            // line 8
            body_11,
            blank24,
            body_12,
            blank25,
            body_13,
            blank26,
            body_14,
            blank27,
            // line 9
            body_01,
            blank28,
            body_02,
            blank29,
            body_03,
            blank30,
            body_04,
            blank31,
            // line 10
            body_21,
            blank32,
            body_22,
            blank33,
            body_23,
            blank34,
            body_24,
            blank35,
            // line 8
            leg_11,
            blank44,
            leg_12,
            blank45,
            leg_13,
            blank46,
            leg_14,
            blank47,
            // line 9
            leg_01,
            blank48,
            leg_02,
            blank49,
            leg_03,
            blank50,
            leg_04,
            blank51,
            // line 10
            leg_21,
            blank52,
            leg_22,
            blank53,
            leg_23,
            blank54,
            leg_24,
            blank55,
            // line 11
            dig_01,
            blank36,
            dig_02,
            blank37,
            dig_01S,
            blank38,
            dig_02S,
            blank39,
            // line 12
            shade_2,
            blank40,
            shade_1,
            blank41,
            shade_0,


        }

        public enum dinosaurIndexes
        {
            // line 1
            body_2,
            body_2S,
            head_2,
            head_2S,
            special_2,
            special_2S,
            // line 2
            body_1,
            body_1S,
            head_1,
            head_1S,
            special_1,
            special_1S,
            // line 3
            body_0,
            body_0S,
            head_0,
            head_0S,
            special_0,
            special_0S,
            // line 4
            shadow_2,
            shadow_1,
            shadow_0,

        }

        public enum dinosaurLegIndexes
        {
            // line 4
            walk_20,
            walk_21,
            walk_22,
            walk_23,
            walk_24,
            walk_25,
            walk_26,
            // line 5
            walk_10,
            walk_11,
            walk_12,
            walk_13,
            walk_14,
            walk_15,
            walk_16,
            // line 6
            walk_00,
            walk_01,
            walk_02,
            walk_03,
            walk_04,
            walk_05,
            walk_06,

        }

        public Dictionary<int, List<dragonIndexes>> bodyFrames = new()
        {

            [0] = new()
            {
                dragonIndexes.body_0,
            },
            [1] = new()
            {
                dragonIndexes.body_1,
            },
            [2] = new()
            {
                dragonIndexes.body_2,
            },
            [3] = new()
            {
                dragonIndexes.body_1,
            },

        };

        public Dictionary<int, List<dragonIndexes>> backFrames = new()
        {

            [0] = new()
            {
                dragonIndexes.back_0,
            },
            [1] = new()
            {
                dragonIndexes.back_1,
            },
            [2] = new()
            {
                dragonIndexes.back_2,
            },
            [3] = new()
            {
                dragonIndexes.back_1,
            },

        };

        public Dictionary<int, List<dragonIndexes>> headFrames = new()
        {

            [0] = new()
            {
                dragonIndexes.head_0,
                dragonIndexes.head_0S,
            },
            [1] = new()
            {
                dragonIndexes.head_1,
                dragonIndexes.head_1S,
            },
            [2] = new()
            {
                dragonIndexes.head_2,
                dragonIndexes.head_2S,
            },
            [3] = new()
            {
                dragonIndexes.head_1,
                dragonIndexes.head_1S,
            },

        };

        public Dictionary<int, List<dragonIndexes>> legFrames = new()
        {

            [0] = new()
            {
                dragonIndexes.idle_0,
                dragonIndexes.walk_01,
                dragonIndexes.walk_02,
                dragonIndexes.walk_03,
                dragonIndexes.walk_04,
                dragonIndexes.walk_05,
                dragonIndexes.walk_06,
            },
            [1] = new()
            {
                dragonIndexes.idle_1,
                dragonIndexes.walk_11,
                dragonIndexes.walk_12,
                dragonIndexes.walk_13,
                dragonIndexes.walk_14,
                dragonIndexes.walk_15,
                dragonIndexes.walk_16,
            },
            [2] = new()
            {
                dragonIndexes.idle_2,
                dragonIndexes.walk_21,
                dragonIndexes.walk_22,
                dragonIndexes.walk_23,
                dragonIndexes.walk_24,
                dragonIndexes.walk_25,
                dragonIndexes.walk_26,
            },
            [3] = new()
            {
                dragonIndexes.idle_1,
                dragonIndexes.walk_11,
                dragonIndexes.walk_12,
                dragonIndexes.walk_13,
                dragonIndexes.walk_14,
                dragonIndexes.walk_15,
                dragonIndexes.walk_16,
            },

        };

        public Dictionary<int, List<dragonShadows>> swimFrames = new()
        {

            [0] = new()
            {
                dragonShadows.swim_01,
                dragonShadows.swim_02,
            },
            [1] = new()
            {
                dragonShadows.swim_11,
                dragonShadows.swim_12,
            },
            [2] = new()
            {
                dragonShadows.swim_21,
                dragonShadows.swim_22,
            },
            [3] = new()
            {
                dragonShadows.swim_11,
                dragonShadows.swim_12,
            },

        };

        public Dictionary<int, List<dragonShadows>> shadowFrames = new()
        {

            [0] = new()
            {
                dragonShadows.walk_0,

            },
            [1] = new()
            {
                dragonShadows.walk_1,
            },
            [2] = new()
            {
                dragonShadows.walk_2,
            },
            [3] = new()
            {
                dragonShadows.walk_1,
            },

        };

        public List<dragonIndexes> diveFrames = new()
        {
            dragonIndexes.submerge_1,
            dragonIndexes.submerge_1,
            dragonIndexes.emerge_1,
            dragonIndexes.emerge_1,
            dragonIndexes.emerge_1,
            dragonIndexes.emerge_1,
        };

        public List<dragonShadows> diveShadowFrames = new()
        {
            dragonShadows.swim_submerge_1,
            dragonShadows.swim_submerge_2,
            dragonShadows.swim_emerge_1,
            dragonShadows.swim_emerge_2,
            dragonShadows.swim_emerge_1,
            dragonShadows.swim_emerge_2,
        };

        public Dictionary<int, List<dashIndexes>> dashHeadFrames = new()
        {

            [0] = new()
            {
                dashIndexes.head_01,
                dashIndexes.head_02,
            },
            [1] = new()
            {
                dashIndexes.head_11,
                dashIndexes.head_12,
            },
            [2] = new()
            {
                dashIndexes.head_21,
                dashIndexes.head_22,
            },
            [3] = new()
            {
                dashIndexes.head_11,
                dashIndexes.head_12,
            },
            [4] = new()
            {
                dashIndexes.head_41,
            },

        };

        public Dictionary<int, List<dashIndexes>> dashWingFrames = new()
        {

            [0] = new()
            {
                dashIndexes.wing_01,
                dashIndexes.wing_02,
                dashIndexes.wing_03,
                dashIndexes.wing_04,
                dashIndexes.wing_03,
                dashIndexes.wing_01,
            },
            [1] = new()
            {
                dashIndexes.wing_11,
                dashIndexes.wing_12,
                dashIndexes.wing_13,
                dashIndexes.wing_14,
                dashIndexes.wing_13,
                dashIndexes.wing_11,

            },
            [2] = new()
            {
                dashIndexes.wing_21,
                dashIndexes.wing_22,
                dashIndexes.wing_23,
                dashIndexes.wing_24,
                dashIndexes.wing_23,
                dashIndexes.wing_21,
            },
            [3] = new()
            {
                dashIndexes.wing_11,
                dashIndexes.wing_12,
                dashIndexes.wing_13,
                dashIndexes.wing_14,
                dashIndexes.wing_13,
                dashIndexes.wing_11,
            },

        };

        public Dictionary<int, List<dashIndexes>> dashBodyFrames = new()
        {

            [0] = new()
            {
                dashIndexes.body_01,
                dashIndexes.body_02,
                dashIndexes.body_02,
                dashIndexes.body_02,
                dashIndexes.body_02,
                dashIndexes.body_01,
            },
            [1] = new()
            {
                dashIndexes.body_11,
                dashIndexes.body_12,
                dashIndexes.body_12,
                dashIndexes.body_12,
                dashIndexes.body_12,
                dashIndexes.body_11,
            },
            [2] = new()
            {
                dashIndexes.body_21,
                dashIndexes.body_22,
                dashIndexes.body_22,
                dashIndexes.body_22,
                dashIndexes.body_22,
                dashIndexes.body_21,
            },
            [3] = new()
            {
                dashIndexes.body_11,
                dashIndexes.body_12,
                dashIndexes.body_12,
                dashIndexes.body_12,
                dashIndexes.body_12,
                dashIndexes.body_11,
            },
            [4] = new()
            {
                dashIndexes.dig_01,
                dashIndexes.dig_02,
            }

        };

        public Dictionary<int, List<dashIndexes>> dashLegFrames = new()
        {

            [0] = new()
            {
                dashIndexes.leg_01,
                dashIndexes.leg_02,
                dashIndexes.leg_02,
                dashIndexes.leg_02,
                dashIndexes.leg_02,
                dashIndexes.leg_01,
            },
            [1] = new()
            {
                dashIndexes.leg_11,
                dashIndexes.leg_12,
                dashIndexes.leg_12,
                dashIndexes.leg_12,
                dashIndexes.leg_12,
                dashIndexes.leg_11,
            },
            [2] = new()
            {
                dashIndexes.leg_21,
                dashIndexes.leg_22,
                dashIndexes.leg_22,
                dashIndexes.leg_22,
                dashIndexes.leg_22,
                dashIndexes.leg_21,
            },
            [3] = new()
            {
                dashIndexes.leg_11,
                dashIndexes.leg_12,
                dashIndexes.leg_12,
                dashIndexes.leg_12,
                dashIndexes.leg_12,
                dashIndexes.leg_11,
            },

        };

        public Dictionary<int, List<dashIndexes>> dashShadowFrames = new()
        {

            [0] = new()
            {
                dashIndexes.shade_0,
            },
            [1] = new()
            {
                dashIndexes.shade_1,
            },
            [2] = new()
            {
                dashIndexes.shade_2,
            },
            [3] = new()
            {
                dashIndexes.shade_1,
            },

        };

        public Dictionary<int, Vector2> walkBreathVectors = new()
        {

            [0] = new(45, 7),
            [1] = new(64, 24),
            [2] = new(48, 23),
            [3] = new(64, 24),
        };

        public Dictionary<int, Vector2> dashBreathVectors = new()
        {
            [0] = new(20, 10),
            [1] = new(55, 25),
            [2] = new(31, 49),
            [3] = new(55, 25),
        };

        public Dictionary<int, List<dinosaurIndexes>> headFramesDinosaur = new()
        {

            [0] = new()
            {
                dinosaurIndexes.head_0,
                dinosaurIndexes.special_0,
            },
            [1] = new()
            {
                dinosaurIndexes.head_1,
                dinosaurIndexes.special_1,
            },
            [2] = new()
            {
                dinosaurIndexes.head_2,
                dinosaurIndexes.special_2,
            },
            [3] = new()
            {
                dinosaurIndexes.head_1,
                dinosaurIndexes.special_1,
            },

        };


        public Dictionary<int, List<dinosaurIndexes>> bodyFramesDinosaur = new()
        {

            [0] = new()
            {
                dinosaurIndexes.body_0,

            },
            [1] = new()
            {
                dinosaurIndexes.body_1,

            },
            [2] = new()
            {
                dinosaurIndexes.body_2,

            },
            [3] = new()
            {
                dinosaurIndexes.body_1,

            },

        };

        public Dictionary<int, List<dinosaurLegIndexes>> legFramesDinosaur = new()
        {

            [0] = new()
            {
                dinosaurLegIndexes.walk_00,
                dinosaurLegIndexes.walk_01,
                dinosaurLegIndexes.walk_02,
                dinosaurLegIndexes.walk_03,
                dinosaurLegIndexes.walk_04,
                dinosaurLegIndexes.walk_05,
                dinosaurLegIndexes.walk_06,
            },
            [1] = new()
            {
                dinosaurLegIndexes.walk_10,
                dinosaurLegIndexes.walk_11,
                dinosaurLegIndexes.walk_12,
                dinosaurLegIndexes.walk_13,
                dinosaurLegIndexes.walk_14,
                dinosaurLegIndexes.walk_15,
                dinosaurLegIndexes.walk_16,
            },
            [2] = new()
            {
                dinosaurLegIndexes.walk_20,
                dinosaurLegIndexes.walk_21,
                dinosaurLegIndexes.walk_22,
                dinosaurLegIndexes.walk_23,
                dinosaurLegIndexes.walk_24,
                dinosaurLegIndexes.walk_25,
                dinosaurLegIndexes.walk_26,
            },
            [3] = new()
            {
                dinosaurLegIndexes.walk_10,
                dinosaurLegIndexes.walk_11,
                dinosaurLegIndexes.walk_12,
                dinosaurLegIndexes.walk_13,
                dinosaurLegIndexes.walk_14,
                dinosaurLegIndexes.walk_15,
                dinosaurLegIndexes.walk_16,
            },

        };


        public Dictionary<int, List<dinosaurIndexes>> shadowFramesDinosaur = new()
        {

            [0] = new()
            {
                dinosaurIndexes.shadow_0,
            },
            [1] = new()
            {
                dinosaurIndexes.shadow_1,
            },
            [2] = new()
            {
                dinosaurIndexes.shadow_2,
            },
            [3] = new()
            {
                dinosaurIndexes.shadow_1,
            },

        };

        public enum dragonSchemes
        {

            dragon_custom,
            dragon_red,
            dragon_blue,
            dragon_black,
            dragon_green,
            dragon_purple,

        }

        public dragonSchemes configScheme = dragonSchemes.dragon_custom;

        public Dictionary<dragonSchemes, List<Microsoft.Xna.Framework.Color>> gradientColours = new()
        {

            [dragonSchemes.dragon_red] = new() { new(190, 30, 45), new(191, 142, 93), new(39, 170, 225) },
            [dragonSchemes.dragon_green] = new() { new(121, 172, 66), new(207, 165, 73), new(248, 149, 32) },
            [dragonSchemes.dragon_blue] = new() { new(53, 70, 179), new(147, 149, 151), new(247, 148, 29) },
            [dragonSchemes.dragon_black] = new() { new(57, 52, 53), new(69, 186, 235), new(146, 86, 163) },
            [dragonSchemes.dragon_purple] = new() { new(159, 80, 191), new(69, 186, 235), new(0, 167, 157) },

        };

        public enum breathSchemes
        {

            breath_stars,
            breath_fates,
            breath_ether,

        }

        public breathSchemes configBreath = breathSchemes.breath_stars;

        public Dictionary<breathSchemes, List<Microsoft.Xna.Framework.Color>> breathGradientColours = new()
        {

            [breathSchemes.breath_stars] = new() { new(255, 230, 166), new(255, 173, 84), new(231, 102, 84), },
            [breathSchemes.breath_fates] = new() { new(119, 75, 131), new(59, 55, 100), new(37, 34, 74), new(236, 118, 124), },
            [breathSchemes.breath_ether] = new() { new(111, 203, 220), new(84, 163, 218), new(13, 114, 185), },

        };


        public static string SchemeDescriptions(int scheme)
        {

            switch (scheme)
            {
                default:
                case 0:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.1");
                case 1:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.2");
                case 2:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.3");
                case 3:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.4");
                case 4:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.5");
                case 5:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.6");

            }

        }

        public static string BreathDescriptions(int breathScheme)
        {

            switch (breathScheme)
            {
                default:
                case 0:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.7");
                case 1:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.8");
                case 2:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.9");

            }

        }

        public static string SizeDescriptions(int size)
        {

            switch (size)
            {
                default:

                case 1:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.10");
                case 2:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.11");
                case 3:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.12");
                case 4:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.13");
                case 5:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351.14");
            }

        }

        // =====================================================================================

        public DragonRender()
        {
        }

        public Rectangle RectangleDragonIndex(dragonIndexes index, int width = 1, int offset = 0)
        {
            int X = (int)index % 7 * 64 + offset;

            int Y = (int)index / 7 * 64;

            int W = 64 * width;

            int H = 64;

            return new Rectangle(X, Y, W, H);

        }

        public Rectangle RectangleShadowIndex(dragonShadows index, int width = 1, int offset = 0)
        {
            int X = (int)index * 64 + offset;

            int Y = 6 * 64;

            int W = 64 * width;

            int H = 64;

            return new Rectangle(X, Y, W, H);

        }

        public Rectangle RectangleDashIndex(dashIndexes index, int width = 2, int offset = 0)
        {
            int X = (int)index % 8 * 64 + offset;

            int Y = (int)index / 8 * 64;

            int W = 64 * width;

            int H = 64;

            return new Rectangle(X, Y, W, H);

        }

        public Rectangle RectangleDinosaurIndex(dinosaurIndexes index, int width = 1, int offset = 0)
        {
            int X = (int)index % 6 * 96 + offset;

            int Y = (int)index / 6 * 96;

            int W = 96 * width;

            int H = 96;

            return new Rectangle(X, Y, W, H);

        }

        public Rectangle RectangleDinosaurLegsIndex(dinosaurLegIndexes index, int width = 1, int offset = 0)
        {
            int X = (int)index % 7 * 64 + offset;

            int Y = (int)index / 7 * 64 + 384;

            int W = 64 * width;

            int H = 64;

            return new Rectangle(X, Y, W, H);

        }

        public void LoadCustomColour()
        {

            gradientColours[dragonSchemes.dragon_custom] = new()
            {
                new(Mod.instance.Config.dragonPrimaryR,Mod.instance.Config.dragonPrimaryG,Mod.instance.Config.dragonPrimaryB),
                new(Mod.instance.Config.dragonSecondaryR,Mod.instance.Config.dragonSecondaryG,Mod.instance.Config.dragonSecondaryB),
                new(Mod.instance.Config.dragonTertiaryR,Mod.instance.Config.dragonTertiaryG,Mod.instance.Config.dragonTertiaryB),
            };

        }

        public void LoadConfigScheme()
        {

            LoadCustomColour();

            switch (Mod.instance.Config.dragonScheme)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:

                    break;

                default:
                case 28:
                    Mod.instance.Config.dragonScheme = 0;
                    break;
                case 29:
                    Mod.instance.Config.dragonScheme = 1;
                    break;
                case 30:
                    Mod.instance.Config.dragonScheme = 2;
                    break;
                case 31:
                    Mod.instance.Config.dragonScheme = 3;
                    break;
                case 32:
                    Mod.instance.Config.dragonScheme = 4;
                    break;
                case 33:
                    Mod.instance.Config.dragonScheme = 5;
                    break;

            }

            switch (Mod.instance.Config.dragonBreath)
            {
                case 0:
                case 1:
                case 2:
                    break;

                default:
                case 3:
                    Mod.instance.Config.dragonBreath = 0;
                    break;
                case 4:
                    Mod.instance.Config.dragonBreath = 1;
                    break;
                case 5:
                    Mod.instance.Config.dragonBreath = 2;
                    break;

            }

            LoadColourScheme((dragonSchemes)Mod.instance.Config.dragonScheme);

            LoadBreathScheme((breathSchemes)Mod.instance.Config.dragonBreath);

        }

        public void LoadColourScheme(dragonSchemes scheme)
        {
            
            if (!gradientColours.ContainsKey(scheme))
            {

                scheme = dragonSchemes.dragon_red;

            }

            List<Color> colors = gradientColours[scheme];

            primary = colors[0];

            secondary = colors[1];

            tertiary = colors[2];

        }

        public void LoadBreathScheme(breathSchemes breath)
        {

            if (!breathGradientColours.ContainsKey(breath))
            {

                breath = breathSchemes.breath_stars;

            }

            fire = breath;

        }

        public void GhostScheme()
        {

            primary *= 0.75f;

            secondary *= 0.75f;

            tertiary *= 0.75f;

        }

        public virtual void drawWalk(SpriteBatch b, Vector2 spritePosition, DragonAdditional additional)
        {

            Vector2 spriteShadow = spritePosition + new Vector2(0,8 * additional.scale);

            if (additional.layer == -1f)
            {

                additional.layer = spritePosition.Y / 10000f;

            }

            if (additional.shadow)
            {
                
                b.Draw(characterTexture, spriteShadow, RectangleShadowIndex(shadowFrames[additional.direction][0]), Color.White * 0.15f, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer - 0.0002f);

            }

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(backFrames[additional.direction][0]), primary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer - 0.0001f);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(backFrames[additional.direction][0], 1, 320), secondary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer - 0.0002f);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(legFrames[additional.direction][additional.frame]), primary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(legFrames[additional.direction][additional.frame], 1, 448), secondary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(headFrames[additional.direction][additional.version]), primary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(headFrames[additional.direction][additional.version], 1, 320), secondary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(headFrames[additional.direction][additional.version], 1, 704), tertiary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(bodyFrames[additional.direction][0]), primary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(bodyFrames[additional.direction][0], 1, 320), secondary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

            if (additional.breath)
            {

                Vector2 breathVector = additional.scale * walkBreathVectors[additional.direction];

                Vector2 breathPosition = additional.flip ? spritePosition + new Vector2(additional.scale * 64, 0) + new Vector2(0 - breathVector.X, breathVector.Y) : spritePosition + breathVector;

                drawBreath(b, breathPosition, additional);

            }

        }

        public virtual void drawWalkDinosaur(SpriteBatch b, Vector2 spritePosition, DragonAdditional additional)
        {

            Vector2 dinoPosition = spritePosition - new Vector2(16) * additional.scale;

            Vector2 shadowPosition = dinoPosition + new Vector2(0, 16) * additional.scale;

            if (additional.layer == -1f)
            {

                additional.layer = spritePosition.Y / 10000f;

            }

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(legFrames[additional.direction][additional.frame]), primary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(legFrames[additional.direction][additional.frame], 1, 448), secondary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(dinosaurTexture, dinoPosition, RectangleDinosaurIndex(headFramesDinosaur[additional.direction][additional.version]), primary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(dinosaurTexture, dinoPosition, RectangleDinosaurIndex(headFramesDinosaur[additional.direction][additional.version], 1, 96), tertiary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(dinosaurTexture, dinoPosition, RectangleDinosaurIndex(bodyFramesDinosaur[additional.direction][0]), primary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

            b.Draw(dinosaurTexture, dinoPosition, RectangleDinosaurIndex(bodyFramesDinosaur[additional.direction][0], 1, 96), additional.version > 0 ? tertiary : secondary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

            b.Draw(dinosaurTexture, spritePosition, RectangleDinosaurLegsIndex(legFramesDinosaur[additional.direction][additional.frame]), primary, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0003f);

            b.Draw(dinosaurTexture, shadowPosition, RectangleDinosaurIndex(shadowFramesDinosaur[additional.direction][0]), Color.White * 0.15f, 0.0f, new Vector2(0.0f, 0.0f), additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer - 0.0002f);

        }

        public virtual void drawSwim(SpriteBatch b, Vector2 spritePosition, DragonAdditional additional)
        {

            if (additional.layer == -1f)
            {

                additional.layer = spritePosition.Y / 10000f;

            }

            int swimFrame = additional.frame % 2;

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(backFrames[additional.direction][0]), primary, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer - 0.0001f);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(backFrames[additional.direction][0], 1, 320), secondary, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer - 0.0001f);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(headFrames[additional.direction][additional.version]), primary, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(headFrames[additional.direction][additional.version], 1, 320), secondary, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(headFrames[additional.direction][additional.version], 1, 704), tertiary, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(bodyFrames[additional.direction][0]), primary, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(characterTexture, spritePosition, RectangleDragonIndex(bodyFrames[additional.direction][0], 1, 320), secondary, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(characterTexture, spritePosition, RectangleShadowIndex(swimFrames[additional.direction][swimFrame]), Color.White * 0.35f, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

        }

        public virtual void drawDive(SpriteBatch b, Vector2 spritePosition, DragonAdditional additional)
        {

            Vector2 divePosition = spritePosition + new Vector2(0,-2f * (additional.frame % 3));

            if (additional.layer == -1f)
            {

                additional.layer = divePosition.Y / 10000f;

            }

            b.Draw(characterTexture, divePosition, RectangleDragonIndex(diveFrames[additional.frame]), primary, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(characterTexture, divePosition, RectangleDragonIndex(diveFrames[additional.frame], 1, 320), secondary, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(characterTexture, divePosition, RectangleDragonIndex(diveFrames[additional.frame], 1, 576), tertiary, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(characterTexture, divePosition, RectangleShadowIndex(diveShadowFrames[additional.frame]), Color.White * 0.35f, 0.0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

        }

        public virtual void drawFlight(SpriteBatch b, Vector2 localPosition, DragonAdditional additional)
        {

            Vector2 spritePosition = localPosition - new Vector2(32*additional.scale,additional.flight);

            b.Draw(dashTexture, spritePosition + new Vector2(0, additional.flight), RectangleDashIndex(dashShadowFrames[additional.direction][0]), Color.White * 0.2f, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer - 0.0001f);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashLegFrames[additional.direction][additional.frame]), primary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashLegFrames[additional.direction][additional.frame], 2, 256), secondary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashBodyFrames[additional.direction][additional.frame]), primary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashBodyFrames[additional.direction][additional.frame], 2, 256), secondary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            Vector2 flightPosition = new(spritePosition.X, spritePosition.Y);

            if(additional.direction % 2 == 1 && additional.frame != 0)
            {
                flightPosition.X += additional.direction == 3? additional.scale * 3 : additional.scale * -3;
                flightPosition.Y -= additional.scale * 8;

            }

            Rectangle wingFrame = RectangleDashIndex(dashWingFrames[additional.direction][additional.frame]);

            b.Draw(dashTexture, flightPosition, wingFrame, primary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            wingFrame.Y += 704;

            b.Draw(dashTexture, flightPosition, wingFrame, secondary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            if (additional.breath)
            {

                Vector2 breathVector = additional.scale * dashBreathVectors[additional.direction];

                Vector2 breathPosition = additional.flip ? spritePosition + new Vector2(64 * additional.scale, 0) + new Vector2(0 - breathVector.X, breathVector.Y) : spritePosition + new Vector2(64 * additional.scale, 0) + breathVector;

                drawBreath(b, breathPosition, additional);

            }

            if (!additional.flip) { spritePosition.X += 64 * additional.scale; }

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashHeadFrames[additional.direction][additional.version], 1), primary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashHeadFrames[additional.direction][additional.version], 1, 128), secondary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashHeadFrames[additional.direction][additional.version], 1, 256), tertiary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

        }

        public virtual void drawDig(SpriteBatch b, Vector2 localPosition, DragonAdditional additional)
        {

            Vector2 spritePosition = localPosition - new Vector2(64 * additional.scale, 0);

            if (additional.flip)
            {

                spritePosition = localPosition - new Vector2(16 * additional.scale, 0);

            }

            Vector2 spriteShadow = spritePosition + new Vector2(0, 8 * additional.scale);

            Rectangle wingFrame = RectangleDashIndex(dashWingFrames[1][0]);

            b.Draw(dashTexture, spritePosition, wingFrame, primary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0003f);

            wingFrame.Y += 704;

            b.Draw(dashTexture, spritePosition, wingFrame, secondary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0003f);

            //b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashLegFrames[4][additional.frame]), primary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            //b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashLegFrames[4][additional.frame], 2, 256), secondary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashBodyFrames[4][additional.frame]), primary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashBodyFrames[4][additional.frame], 2, 256), secondary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

            Vector2 headPosition = new(spritePosition.X, spritePosition.Y);

            if (!additional.flip) { headPosition.X += 64 * additional.scale; }

            b.Draw(dashTexture, headPosition, RectangleDashIndex(dashHeadFrames[4][0], 1), primary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(dashTexture, headPosition, RectangleDashIndex(dashHeadFrames[4][0], 1, 128), secondary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(dashTexture, headPosition, RectangleDashIndex(dashHeadFrames[4][0], 1, 256), tertiary, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(dashTexture, spriteShadow, RectangleDashIndex(dashShadowFrames[1][0]), Color.White * 0.15f, 0f, Vector2.Zero, additional.scale, additional.flip ? (SpriteEffects)1 : 0, additional.layer - 0.0001f);

        }

        public virtual void drawSweep(SpriteBatch b, Vector2 localPosition, DragonAdditional additional)
        {

            Vector2 spritePosition = localPosition - new Vector2(32 * additional.scale, 0);

            Vector2 spriteShadow = spritePosition + new Vector2(0, 8 * additional.scale);

            int sequence = additional.direction;

            if (additional.flip)
            {
                switch (additional.direction)
                {
                    case 0: sequence = 5; break;
                    case 1: sequence = 4; break;
                    case 2: sequence = 3; break;
                }

            }

            sequence = (sequence + additional.frame + 1) % 6;

            int useDirection = sequence;

            bool useFlip = false;

            if (sequence >= 3)
            {

                switch (sequence)
                {
                    case 3: useDirection = 2; break;
                    case 4: useDirection = 1; break;
                    case 5: useDirection = 0; break;
                }

                useFlip = true;

            }

            b.Draw(dashTexture, spriteShadow, RectangleDashIndex(dashShadowFrames[useDirection][0]), Color.White * 0.15f, 0f, Vector2.Zero, additional.scale, useFlip ? (SpriteEffects)1 : 0, additional.layer - 0.0001f);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashLegFrames[useDirection][0]), primary, 0f, Vector2.Zero, additional.scale, useFlip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashLegFrames[useDirection][0], 2, 256), secondary, 0f, Vector2.Zero, additional.scale, useFlip ? (SpriteEffects)1 : 0, additional.layer + 0.0001f);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashBodyFrames[useDirection][0]), primary, 0f, Vector2.Zero, additional.scale, useFlip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashBodyFrames[useDirection][0], 2, 256), secondary, 0f, Vector2.Zero, additional.scale, useFlip ? (SpriteEffects)1 : 0, additional.layer + 0.0002f);

            Rectangle wingFrame = RectangleDashIndex(dashWingFrames[useDirection][0]);

            b.Draw(dashTexture, spritePosition, wingFrame, primary, 0f, Vector2.Zero, additional.scale, useFlip ? (SpriteEffects)1 : 0, additional.layer + 0.0003f);

            wingFrame.Y += 704;

            b.Draw(dashTexture, spritePosition, wingFrame, secondary, 0f, Vector2.Zero, additional.scale, useFlip ? (SpriteEffects)1 : 0, additional.layer + 0.0003f);


            if (additional.breath)
            {

                Vector2 breathVector = additional.scale * dashBreathVectors[additional.direction];

                Vector2 breathPosition = additional.flip ? spritePosition + new Vector2(64 * additional.scale, 0) + new Vector2(0 - breathVector.X, breathVector.Y) : spritePosition + new Vector2(64 * additional.scale, 0) + breathVector;

                drawBreath(b, breathPosition, additional);

            }

            if (!useFlip) { spritePosition.X += 64 * additional.scale; }

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashHeadFrames[useDirection][additional.version], 1), primary, 0f, Vector2.Zero, additional.scale, useFlip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashHeadFrames[useDirection][additional.version], 1, 128), secondary, 0f, Vector2.Zero, additional.scale, useFlip ? (SpriteEffects)1 : 0, additional.layer);

            b.Draw(dashTexture, spritePosition, RectangleDashIndex(dashHeadFrames[useDirection][additional.version], 1, 256), tertiary, 0f, Vector2.Zero, additional.scale, useFlip ? (SpriteEffects)1 : 0, additional.layer);

        }

        public virtual void drawSweepDinosaur(SpriteBatch b, Vector2 localPosition, DragonAdditional additional)
        {

            Vector2 offset = ModUtility.DirectionAsVector(additional.direction * 2);

            switch (additional.frame)
            {
                case 0:

                    localPosition += offset * additional.scale * 12;

                    additional.version = 1;

                    break;

                case 1:

                    localPosition += offset * additional.scale * 24;

                    additional.version = 1;

                    break;

                case 2:

                    localPosition += offset * additional.scale * 24;

                    additional.version = 0;

                    break;

                case 3:

                    localPosition += offset * additional.scale * 16;

                    additional.version = 0;

                    break;

                case 4:

                    localPosition += offset * additional.scale * 8;

                    additional.version = 0;

                    break;
            }

            drawWalkDinosaur(b, localPosition, additional);

        }

        public virtual void drawBreath(SpriteBatch b, Vector2 spritePosition, DragonAdditional additional)
        {

            float scale = additional.scale - 0.5f;

            int breathFrame = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 1000 / 250);

            float rotation = 0f;

            float layer = additional.layer + 0.0005f;

            switch (additional.direction)
            {
                case 0:


                    if (additional.flip)
                    {

                        rotation = 0f + (float)Math.PI * 0.55f;

                    }
                    else
                    {

                        rotation = 0f - (float)Math.PI * 0.55f;

                    }

                    layer -= 0.002f;

                    break;

                case 1:

                    //layer += 0.032f;

                    rotation = 0f - (float)Math.PI * 0.05f;

                    break;

                case 3:

                    //layer += 0.032f;

                    rotation = 0f + (float)Math.PI * 0.05f;

                    break;

                case 2:

                    //layer += 0.032f;

                    if (additional.flip)
                    {

                        rotation = 0f - (float)Math.PI * 0.05f;

                    }
                    else
                    {

                        rotation = 0f + (float)Math.PI * 0.05f;

                    }

                    break;


            }
            
            List<Color> colours = breathGradientColours[breathSchemes.breath_stars];

            if (breathGradientColours.ContainsKey(fire))
            {

                colours = breathGradientColours[fire];

            }

            switch (additional.direction)
            {
                case 0:
                case 2:

                    b.Draw(breathTexture, spritePosition, new Rectangle(breathFrame * 192, 0, 192, 192), Color.White*0.9f, rotation, new Vector2(96), scale - 0.5f, additional.flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(breathTexture, spritePosition, new Rectangle(breathFrame * 192, 192, 192, 192), colours[0] * 0.9f, rotation, new Vector2(96), scale - 0.5f, additional.flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(breathTexture, spritePosition, new Rectangle(breathFrame * 192, 384, 192, 192), colours[1] * 0.9f, rotation, new Vector2(96), scale - 0.5f, additional.flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(breathTexture, spritePosition, new Rectangle(breathFrame * 192, 576, 192, 192), colours[2] * 0.9f, rotation, new Vector2(96), scale - 0.5f, additional.flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(breathTexture, spritePosition, new Rectangle(breathFrame * 192, 768, 192, 192), colours[1] * 0.9f, rotation, new Vector2(96), scale - 0.5f, additional.flip ? (SpriteEffects)1 : 0, layer);

                    break;

                case 1:
                case 3:

                    b.Draw(breathTextureTwo, spritePosition, new Rectangle(breathFrame * 192, 0, 192, 192), Color.White * 0.9f, rotation, new Vector2(96), scale - 0.5f, additional.flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(breathTextureTwo, spritePosition, new Rectangle(breathFrame * 192, 192, 192, 192), colours[0] * 0.9f, rotation, new Vector2(96), scale - 0.5f, additional.flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(breathTextureTwo, spritePosition, new Rectangle(breathFrame * 192, 384, 192, 192), colours[1] * 0.9f, rotation, new Vector2(96), scale - 0.5f, additional.flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(breathTextureTwo, spritePosition, new Rectangle(breathFrame * 192, 576, 192, 192), colours[2] * 0.9f, rotation, new Vector2(96), scale - 0.5f, additional.flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(breathTextureTwo, spritePosition, new Rectangle(breathFrame * 192, 768, 192, 192), colours[1] * 0.9f, rotation, new Vector2(96), scale - 0.5f, additional.flip ? (SpriteEffects)1 : 0, layer);

                    break;


            }


        }

    }

    public class DragonAdditional
    {

        public int direction = 2;

        public int frame = 0;

        public float scale = 4f;

        public bool flip = false;

        public float layer = -1f;

        public int version = 0;

        public int flight = 0;

        public bool breath = false;

        public bool shadow = true;

    }

}
