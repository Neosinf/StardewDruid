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
    public class CritterRender
    {

        public enum critterFrames
        {

            downIdle,
            downWalk1,
            downWalk2,
            downWalk3,
            downWalk4,
            downWalk5,
            downWalk6,

            rightIdle,
            rightWalk1,
            rightWalk2,
            rightWalk3,
            rightWalk4,
            rightWalk5,
            rightWalk6,

            upIdle,
            upWalk1,
            upWalk2,
            upWalk3,
            upWalk4,
            upWalk5,
            upWalk6,

            leftIdle,
            leftWalk1,
            leftWalk2,
            leftWalk3,
            leftWalk4,
            leftWalk5,
            leftWalk6,

            downRun1,
            downRun2,
            downRun3,
            downRun4,
            downPose1,
            downBlank1,
            downBlank2,

            rightRun1,
            rightRun2,
            rightRun3,
            rightRun4,
            rightPose1,
            rightPose2,
            rightPose3,

            upRun1,
            upRun2,
            upRun3,
            upRun4,
            upPose1,
            upBlank1,
            upBlank2,

            leftRun1,
            leftRun2,
            leftRun3,
            leftRun4,
            leftPose1,
            leftPose2,
            leftPose3,

            sit1,
            sit2,
            sit3,
            sit4,
            sitBlank1,
            sitBlank2,
            sitBlank3,

            greet1,
            greet2,
            greet3,
            sleep1,
            sleep2,
            sleep3,

        }

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> idleFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> walkFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> runningFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> specialFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> dashFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sitFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> greetFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sleepFrames = new();


        public CritterRender(string name)
        {

            idleFrames = new()
            {
                [0] = new()
                {
                    CritterRectangle(critterFrames.upIdle),
                },
                [1] = new()
                {
                    CritterRectangle(critterFrames.rightIdle),
                },
                [2] = new()
                {
                    CritterRectangle(critterFrames.downIdle),
                },
                [3] = new()
                {
                    CritterRectangle(critterFrames.leftIdle),
                },
            };

            walkFrames = new()
            {
                [0] = new()
                {
                    CritterRectangle(critterFrames.upIdle),
                    CritterRectangle(critterFrames.upWalk1),
                    CritterRectangle(critterFrames.upWalk2),
                    CritterRectangle(critterFrames.upWalk3),
                    CritterRectangle(critterFrames.upWalk4),
                    CritterRectangle(critterFrames.upWalk5),
                    CritterRectangle(critterFrames.upWalk6),
                },
                [1] = new()
                {
                    CritterRectangle(critterFrames.rightIdle),
                    CritterRectangle(critterFrames.rightWalk1),
                    CritterRectangle(critterFrames.rightWalk2),
                    CritterRectangle(critterFrames.rightWalk3),
                    CritterRectangle(critterFrames.rightWalk4),
                    CritterRectangle(critterFrames.rightWalk5),
                    CritterRectangle(critterFrames.rightWalk6),
                },
                [2] = new()
                {
                    CritterRectangle(critterFrames.downIdle),
                    CritterRectangle(critterFrames.downWalk1),
                    CritterRectangle(critterFrames.downWalk2),
                    CritterRectangle(critterFrames.downWalk3),
                    CritterRectangle(critterFrames.downWalk4),
                    CritterRectangle(critterFrames.downWalk5),
                    CritterRectangle(critterFrames.downWalk6),
                },
                [3] = new()
                {
                    CritterRectangle(critterFrames.leftIdle),
                    CritterRectangle(critterFrames.leftWalk1),
                    CritterRectangle(critterFrames.leftWalk2),
                    CritterRectangle(critterFrames.leftWalk3),
                    CritterRectangle(critterFrames.leftWalk4),
                    CritterRectangle(critterFrames.leftWalk5),
                    CritterRectangle(critterFrames.leftWalk6),
                },
            };

            runningFrames = new()
            {
                [0] = new()
                {
                    CritterRectangle(critterFrames.upIdle),
                    CritterRectangle(critterFrames.upRun1),
                    CritterRectangle(critterFrames.upRun2),
                    CritterRectangle(critterFrames.upRun3),
                    CritterRectangle(critterFrames.upRun4),
                },
                [1] = new()
                {
                    CritterRectangle(critterFrames.rightIdle),
                    CritterRectangle(critterFrames.rightRun1),
                    CritterRectangle(critterFrames.rightRun2),
                    CritterRectangle(critterFrames.rightRun3),
                    CritterRectangle(critterFrames.rightRun4),
                },
                [2] = new()
                {
                    CritterRectangle(critterFrames.downIdle),
                    CritterRectangle(critterFrames.downRun1),
                    CritterRectangle(critterFrames.downRun2),
                    CritterRectangle(critterFrames.downRun3),
                    CritterRectangle(critterFrames.downRun4),
                },
                [3] = new()
                {
                    CritterRectangle(critterFrames.leftIdle),
                    CritterRectangle(critterFrames.leftRun1),
                    CritterRectangle(critterFrames.leftRun2),
                    CritterRectangle(critterFrames.leftRun3),
                    CritterRectangle(critterFrames.leftRun4),
                },
            };

            specialFrames = new()
            {

                [0] = new()
                {
                    CritterRectangle(critterFrames.upPose1),

                },
                [1] = new()
                {
                    CritterRectangle(critterFrames.rightPose1),

                },
                [2] = new()
                {
                    CritterRectangle(critterFrames.downPose1),

                },
                [3] = new()
                {
                    CritterRectangle(critterFrames.leftPose1),

                },

            };

            sweepFrames = new()
            {

                [0] = new()
                {
                    CritterRectangle(critterFrames.upRun1),
                    CritterRectangle(critterFrames.upRun2),
                    CritterRectangle(critterFrames.upRun2),
                    CritterRectangle(critterFrames.upRun3),
                },
                [1] = new()
                {
                    CritterRectangle(critterFrames.rightRun1),
                    CritterRectangle(critterFrames.rightRun2),
                    CritterRectangle(critterFrames.rightRun2),
                    CritterRectangle(critterFrames.rightRun3),
                },
                [2] = new()
                {
                    CritterRectangle(critterFrames.downRun1),
                    CritterRectangle(critterFrames.downRun2),
                    CritterRectangle(critterFrames.downRun2),
                    CritterRectangle(critterFrames.downRun3),
                },
                [3] = new()
                {
                    CritterRectangle(critterFrames.leftRun1),
                    CritterRectangle(critterFrames.leftRun2),
                    CritterRectangle(critterFrames.leftRun2),
                    CritterRectangle(critterFrames.leftRun3),
                },

            };

            dashFrames = new()
            {

                [0] = new()
                {
                    CritterRectangle(critterFrames.upRun1),
                    CritterRectangle(critterFrames.upRun2),
                },
                [1] = new()
                {
                    CritterRectangle(critterFrames.rightRun1),
                    CritterRectangle(critterFrames.rightRun2),
                },
                [2] = new()
                {
                    CritterRectangle(critterFrames.downRun1),
                    CritterRectangle(critterFrames.downRun2),
                },
                [3] = new()
                {
                    CritterRectangle(critterFrames.leftRun1),
                    CritterRectangle(critterFrames.leftRun2),
                },

                [4] = new()
                {
                    CritterRectangle(critterFrames.upRun2),
                },
                [5] = new()
                {
                    CritterRectangle(critterFrames.rightRun2),
                },
                [6] = new()
                {
                    CritterRectangle(critterFrames.downRun2),
                },
                [7] = new()
                {
                    CritterRectangle(critterFrames.leftRun2),
                },

                [8] = new()
                {
                    CritterRectangle(critterFrames.upRun3),
                    CritterRectangle(critterFrames.upRun4),
                    CritterRectangle(critterFrames.upWalk5),
                },
                [9] = new()
                {
                    CritterRectangle(critterFrames.rightRun3),
                    CritterRectangle(critterFrames.rightRun4),
                    CritterRectangle(critterFrames.rightWalk5),
                },
                [10] = new()
                {
                    CritterRectangle(critterFrames.downRun3),
                    CritterRectangle(critterFrames.downRun4),
                    CritterRectangle(critterFrames.downWalk5),
                },
                [11] = new()
                {
                    CritterRectangle(critterFrames.leftRun3),
                    CritterRectangle(critterFrames.leftRun4),
                    CritterRectangle(critterFrames.leftWalk5),
                },

            };

            sitFrames = new()
            {

                [0] = new()
                {
                    CritterRectangle(critterFrames.sit1),
                    CritterRectangle(critterFrames.sit2),
                    CritterRectangle(critterFrames.sit3),
                    CritterRectangle(critterFrames.sit4),
                },
                [1] = new()
                {
                    CritterRectangle(critterFrames.sit1),
                    CritterRectangle(critterFrames.sit2),
                    CritterRectangle(critterFrames.sit3),
                    CritterRectangle(critterFrames.sit4),
                },
                [2] = new()
                {
                    CritterRectangle(critterFrames.sit1),
                    CritterRectangle(critterFrames.sit2),
                    CritterRectangle(critterFrames.sit3),
                    CritterRectangle(critterFrames.sit4),
                },
                [3] = new()
                {
                    CritterRectangle(critterFrames.sit1),
                    CritterRectangle(critterFrames.sit2),
                    CritterRectangle(critterFrames.sit3),
                    CritterRectangle(critterFrames.sit4),
                },

            };

            greetFrames = new()
            {

                [0] = new()
                {
                    CritterRectangle(critterFrames.greet1),
                    CritterRectangle(critterFrames.greet2),
                    CritterRectangle(critterFrames.greet3),
                    CritterRectangle(critterFrames.greet2),
                    CritterRectangle(critterFrames.greet3),
                },
                [1] = new()
                {
                    CritterRectangle(critterFrames.greet1),
                    CritterRectangle(critterFrames.greet2),
                    CritterRectangle(critterFrames.greet3),
                    CritterRectangle(critterFrames.greet2),
                    CritterRectangle(critterFrames.greet3),
                },
                [2] = new()
                {
                    CritterRectangle(critterFrames.greet1),
                    CritterRectangle(critterFrames.greet2),
                    CritterRectangle(critterFrames.greet3),
                    CritterRectangle(critterFrames.greet2),
                    CritterRectangle(critterFrames.greet3),
                },
                [3] = new()
                {
                    CritterRectangle(critterFrames.greet1),
                    CritterRectangle(critterFrames.greet2),
                    CritterRectangle(critterFrames.greet3),
                    CritterRectangle(critterFrames.greet2),
                    CritterRectangle(critterFrames.greet3),
                },

            };

            sleepFrames = new()
            {

                [0] = new()
                {
                    CritterRectangle(critterFrames.sleep1),
                },
                [1] = new()
                {
                    CritterRectangle(critterFrames.sleep1),
                },
                [2] = new()
                {
                    CritterRectangle(critterFrames.sleep1),
                },
                [3] = new()
                {
                    CritterRectangle(critterFrames.sleep1),
                },

            };

        }

        public static Microsoft.Xna.Framework.Rectangle CritterRectangle(critterFrames frame)
        {

            return new((int)frame % 7 * 48, (int)frame / 7 * 48, 48, 48);

        }


    }

}
