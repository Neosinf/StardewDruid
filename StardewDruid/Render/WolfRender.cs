using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StardewDruid.Render
{
    public class WolfRender
    {

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
            downRunGlide2,
            downRunDown2,
            downRunDown3,
            blank1,

            rightRunUp1,
            rightRunUp2,
            rightRunGlide1,
            rightRunGlide2,
            rightRunDown2,
            rightRunDown3,
            blank2,

            upRunUp1,
            upRunUp2,
            upRunGlide1,
            upRunGlide2,
            upRunDown2,
            upRunDown3,
            blank3,

            downIdleSpecial,
            downWalkR1Special,
            downWalkR2Special,
            downWalkR3Special,
            downWalkL1Special,
            downWalkL2Special,
            downWalkL3Special,

            rightIdleSpecial,
            rightWalkR1Special,
            rightWalkR2Special,
            rightWalkR3Special,
            rightWalkL1Special,
            rightWalkL2Special,
            rightWalkL3Special,

            upIdleSpecial,
            upWalkR1Special,
            upWalkR2Special,
            upWalkR3Special,
            upWalkL1Special,
            upWalkL2Special,
            upWalkL3Special,

            downRunUp1Special,
            downRunUp2Special,
            downRunGlide1Special,
            downRunGlide2Special,
            downRunDown2Special,
            downRunDown3Special,
            blank1Special,

            rightRunUp1Special,
            rightRunUp2Special,
            rightRunGlide1Special,
            rightRunGlide2Special,
            rightRunDown2Special,
            rightRunDown3Special,
            blank2Special,

            upRunUp1Special,
            upRunUp2Special,
            upRunGlide1Special,
            upRunGlide2Special,
            upRunDown2Special,
            upRunDown3Special,
            blank3Special,

        }

        public static Microsoft.Xna.Framework.Rectangle WolfRectangle(wolfFrames frame)
        {
            switch (frame)
            {

                case wolfFrames.upRunGlide1:
                case wolfFrames.rightRunGlide1:
                case wolfFrames.downRunGlide1:
                case wolfFrames.upRunGlide1Special:
                case wolfFrames.rightRunGlide1Special:
                case wolfFrames.downRunGlide1Special:

                    return new((int)frame % 7 * 64, (int)frame / 7 * 64, 128, 64);

            }

            return new((int)frame % 7 * 64, (int)frame / 7 * 64, 64, 64);

        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> WalkFrames()
        {

            Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new()
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

            return sweepFrames;

        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> SweepFrames()
        {

            Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new()
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

            return sweepFrames;

        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> SpecialFrames()
        {


            Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> specFrames = new()
            {
                [0] = new()
                    {
                        WolfRectangle(wolfFrames.upIdleSpecial),

                    },
                [1] = new()
                    {
                        WolfRectangle(wolfFrames.rightIdleSpecial),

                    },
                [2] = new()
                    {
                        WolfRectangle(wolfFrames.downIdleSpecial),

                    },
                [3] = new()
                    {
                        WolfRectangle(wolfFrames.rightIdleSpecial),

                    },

            };

            return specFrames;

        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> DashFrames()
        {

            Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new()
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

            return sweepFrames;

        }

    }

}
