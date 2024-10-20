using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StardewDruid.Render
{
    public class BearRender
    {

        public enum bearFrames
        {

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

            downIdleSpecial,
            downWalkR1Special,
            downWalkR2Special,
            downWalkR3Special,
            downWalkL1Special,
            downWalkL2Special,
            downWalkL3Special,
            downSwipeRSpecial,
            downSwipeLSpecial,

            rightIdleSpecial,
            rightWalkR1Special,
            rightWalkR2Special,
            rightWalkR3Special,
            rightWalkL1Special,
            rightWalkL2Special,
            rightWalkL3Special,
            rightSwipeRSpecial,
            rightSwipeLSpecial,

            upIdleSpecial,
            upWalkR1Special,
            upWalkR2Special,
            upWalkR3Special,
            upWalkL1Special,
            upWalkL2Special,
            upWalkL3Special,
            upSwipeRSpecial,
            upSwipeLSpecial,

        }

        public static Microsoft.Xna.Framework.Rectangle BearRectangle(bearFrames frame)
        {
            return new((int)frame % 9 * 64, (int)frame / 9 * 64, 64, 64);
        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> WalkFrames()
        {

            Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new()
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

            return sweepFrames;

        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> SweepFrames()
        {

            Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new()
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

            return sweepFrames;

        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> DashFrames()
        {

            Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new()
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

            return sweepFrames;

        }

    }

}
