using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StardewDruid.Render
{
    public class FlyerRender
    {

        public enum flyerFrames
        {

            downIdle,
            downLift,
            downFullOut,
            downHalfOut,
            downGlide,
            downHalfIn,
            downFullIn,

            rightIdle,
            rightLift,
            rightFullOut,
            rightHalfOut,
            rightGlide,
            rightHalfIn,
            rightFullIn,

            upIdle,
            upLift,
            upFullOut,
            upHalfOut,
            upGlide,
            upHalfIn,
            upFullIn,

            downPeck,
            downHop,
            downCaw,
            downLookBack,
            downBlank,
            downBlankTwo,
            downBlankThree,

            rightPeck,
            rightHop,
            rightCaw,
            rightLookBack,
            rightBlank,
            rightBlankTwo,
            rightBlankThree,

            upPeck,
            upHop,
            upCaw,
            upLookBack,
            upBlank,
            upBlankTwo,
            upBlankThree,

        }

        public static Microsoft.Xna.Framework.Rectangle FlyerRectangle(flyerFrames frame)
        {
            return new((int)frame % 7 * 48, (int)frame / 7 * 48, 48, 48);
        }


        public static Dictionary<Character.Character.idles, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> FlyerIdle()
        {
            
            Dictionary<Character.Character.idles, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> idleFrames = new();
            
            idleFrames[Character.Character.idles.idle] = new()
            {

                [0] = new() {
                    FlyerRectangle(flyerFrames.upIdle),
                    FlyerRectangle(flyerFrames.upPeck),
                    FlyerRectangle(flyerFrames.upIdle),
                    FlyerRectangle(flyerFrames.upLookBack),
                    FlyerRectangle(flyerFrames.upIdle),
                },

                [1] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightPeck),
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightLookBack),
                    FlyerRectangle(flyerFrames.rightIdle),
                },

                [2] = new() {
                    FlyerRectangle(flyerFrames.downIdle),
                    FlyerRectangle(flyerFrames.downPeck),
                    FlyerRectangle(flyerFrames.downIdle),
                    FlyerRectangle(flyerFrames.downLookBack),
                    FlyerRectangle(flyerFrames.downIdle),
                },

                [3] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightPeck),
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightLookBack),
                    FlyerRectangle(flyerFrames.rightIdle),
                },

            };

            return idleFrames;

        }

        public static Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> FlyerSpecial()
        {

            Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> specialsFrames = new();

            specialsFrames[Character.Character.specials.special] = new()
            {

                [0] = new() {
                    FlyerRectangle(flyerFrames.upIdle),
                    FlyerRectangle(flyerFrames.upPeck),
                    FlyerRectangle(flyerFrames.upIdle),
                    FlyerRectangle(flyerFrames.upPeck),
                    FlyerRectangle(flyerFrames.upIdle),
                    FlyerRectangle(flyerFrames.upIdle),
                },

                [1] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightPeck),
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightPeck),
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightIdle),
                },

                [2] = new() {
                    FlyerRectangle(flyerFrames.downIdle),
                    FlyerRectangle(flyerFrames.downPeck),
                    FlyerRectangle(flyerFrames.downIdle),
                    FlyerRectangle(flyerFrames.downPeck),
                    FlyerRectangle(flyerFrames.downIdle),
                    FlyerRectangle(flyerFrames.downIdle),
                },

                [3] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightPeck),
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightPeck),
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightIdle),
                },

            };

            specialsFrames[Character.Character.specials.sweep] = new()
            {

                [0] = new() {
                    FlyerRectangle(flyerFrames.upHop),
                    FlyerRectangle(flyerFrames.upHop),
                    FlyerRectangle(flyerFrames.upCaw),
                    FlyerRectangle(flyerFrames.upIdle),
                },

                [1] = new() {
                    FlyerRectangle(flyerFrames.rightHop),
                    FlyerRectangle(flyerFrames.rightHop),
                    FlyerRectangle(flyerFrames.rightCaw),
                    FlyerRectangle(flyerFrames.rightIdle),
                },

                [2] = new() {
                    FlyerRectangle(flyerFrames.downHop),
                    FlyerRectangle(flyerFrames.downHop),
                    FlyerRectangle(flyerFrames.downCaw),
                    FlyerRectangle(flyerFrames.downIdle),
                },

                [3] = new() {
                    FlyerRectangle(flyerFrames.rightHop),
                    FlyerRectangle(flyerFrames.rightHop),
                    FlyerRectangle(flyerFrames.rightCaw),
                    FlyerRectangle(flyerFrames.rightIdle),
                },

            };

            return specialsFrames;

        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> FlyerWalk()
        {

            return new()
            {

                [0] = new() {
                    FlyerRectangle(flyerFrames.upIdle),
                    FlyerRectangle(flyerFrames.upLift),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upFullOut),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upHalfIn),
                    FlyerRectangle(flyerFrames.upFullIn),
                    FlyerRectangle(flyerFrames.upHalfIn),
                },

                [1] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightLift),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightFullIn),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                },

                [2] = new() {
                    FlyerRectangle(flyerFrames.downIdle),
                    FlyerRectangle(flyerFrames.downLift),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downFullOut),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downHalfIn),
                    FlyerRectangle(flyerFrames.downFullIn),
                    FlyerRectangle(flyerFrames.downHalfIn),
                },

                [3] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightLift),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightFullIn),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                },

            };

        }

        public static Dictionary<Character.Character.dashes, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> FlyerDash()
        {


            Dictionary<Character.Character.dashes, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> dashes = new();

            dashes[Character.Character.dashes.dash] = new()
            {

                [0] = new() {
                    FlyerRectangle(flyerFrames.upLift),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upFullOut),
                    FlyerRectangle(flyerFrames.upHalfOut),
                },
                [1] = new() {
                    FlyerRectangle(flyerFrames.rightLift),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                },
                [2] = new() {
                    FlyerRectangle(flyerFrames.downLift),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downFullOut),
                    FlyerRectangle(flyerFrames.downHalfOut),
                },
                [3] = new() {
                    FlyerRectangle(flyerFrames.rightLift),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                },
                [4] = new() {
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upFullOut),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upHalfIn),
                    FlyerRectangle(flyerFrames.upFullIn),
                    FlyerRectangle(flyerFrames.upHalfIn),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upFullOut),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upGlide),
                },
                [5] = new() {
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightFullIn),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                },
                [6] = new() {
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downFullOut),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downHalfIn),
                    FlyerRectangle(flyerFrames.downFullIn),
                    FlyerRectangle(flyerFrames.downHalfIn),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downFullOut),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downGlide),
                },
                [7] = new() {
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightFullIn),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                },
                [8] = new() {
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upFullOut),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upLift),
                },
                [9] = new() {
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightLift),
                },
                [10] = new() {
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downFullOut),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downLift),
                },
                [11] = new() {
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightLift),
                },
            };

            dashes[Character.Character.dashes.smash] = new(dashes[Character.Character.dashes.dash]);

            return dashes;

        }


    }

}
