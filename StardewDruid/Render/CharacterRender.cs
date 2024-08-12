using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static StardewDruid.Character.Character;

namespace StardewDruid.Render
{
    public class CharacterRender
    {

        public enum humanoidFrames
        {
            
            idleDown,
            jumpDown,
            stretchDown,
            pointDown,
            crouchDown,
            crouchDownTwo,
            custom1,
            custom2,

            idleRight,
            jumpRight,
            stretchRight,
            pointRight,
            crouchRight,
            crouchRightTwo,
            custom3,
            custom4,

            idleUp,
            jumpUp,
            stretchUp,
            pointUp,
            crouchUp,
            crouchUpTwo,
            custom5,
            custom6,

            idleLeft,
            jumpLeft,
            stretchLeft,
            pointLeft,
            gesture,
            kneel,
            custom7,
            custom8,

            walkDownL1,
            walkDownL2, 
            walkDownL3,
            walkDownR1,
            walkDownR2,
            walkDownR3,
            boxDown1,
            boxDown2,

            walkRightL1,
            walkRightL2,
            walkRightL3,
            walkRightR1,
            walkRightR2,
            walkRightR3,
            boxRight1,
            boxRight2,

            walkUpL1,
            walkUpL2,
            walkUpL3,
            walkUpR1,
            walkUpR2,
            walkUpR3,
            boxUp1,
            boxUp2,

            walkLeftL1,
            walkLeftL2,
            walkLeftL3,
            walkLeftR1,
            walkLeftR2,
            walkLeftR3,
            boxLeft1,
            boxLeft2,

            smashDown1,
            smashDown2,
            smashDown3,
            smashDown4,

            sweepRight1,
            sweepRight2,
            sweepRight3,
            sweepRight4,

            smashRight1,
            smashRight2,
            smashRight3,
            smashRight4,

            twirlDown,
            twirlRight,
            twirlUp,
            twirlLeft,

            smashUp1,
            smashUp2,
            smashUp3,
            smashUp4,

            alertDown,
            alertRight,
            alertUp,
            alertLeft,

        }

        public static List<Microsoft.Xna.Framework.Rectangle> RectangleHumanoidList(List<humanoidFrames> frames)
        {

            List<Microsoft.Xna.Framework.Rectangle> rectangles = new();

            foreach (humanoidFrames frame in frames)
            {

                rectangles.Add(RectangleHumanoid(frame));
            
            }

            return rectangles;

        }

        public static Microsoft.Xna.Framework.Rectangle RectangleHumanoid(humanoidFrames frame)
        {
            int X = (int)frame % 8 * 32;

            int Y = (int)frame / 8 * 32;

            return new Microsoft.Xna.Framework.Rectangle(X, Y, 32, 32);

        }
        
        public static Dictionary<Character.Character.idles, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> HumanoidIdle()
        {

            Dictionary<Character.Character.idles, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> custom = new();

            custom[Character.Character.idles.kneel] = new()
            {
                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.kneel,

                }),
            };

            return custom;

        }

        public static Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> HumanoidSpecial()
        {

            Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> custom = new();

            custom[Character.Character.specials.invoke] = new()
            {
                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.stretchUp,
                    humanoidFrames.pointUp,

                }),
                [1] = RectangleHumanoidList(new(){

                    humanoidFrames.stretchRight,
                    humanoidFrames.pointRight,

                }),
                [2] = RectangleHumanoidList(new(){

                    humanoidFrames.stretchDown,
                    humanoidFrames.pointDown,

                }),
                [3] = RectangleHumanoidList(new(){

                    humanoidFrames.stretchLeft,
                    humanoidFrames.pointLeft,

                }),

            };

            custom[Character.Character.specials.special] = new(custom[Character.Character.specials.invoke]);

            custom[Character.Character.specials.launch] = new(custom[Character.Character.specials.invoke]);

            custom[Character.Character.specials.point] = new()
            {
                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.pointUp,
                    humanoidFrames.pointUp,
                }),
                [1] = RectangleHumanoidList(new(){

                    humanoidFrames.pointRight,

                    humanoidFrames.pointRight,
                }),
                [2] = RectangleHumanoidList(new(){

                    humanoidFrames.pointDown,

                    humanoidFrames.pointDown,
                }),
                [3] = RectangleHumanoidList(new(){

                    humanoidFrames.pointLeft,

                    humanoidFrames.pointLeft,
                }),

            };

            custom[Character.Character.specials.sweep] = new()
            {
                
                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.boxUp1,
                    humanoidFrames.boxUp2,

                }),

                [1] = RectangleHumanoidList(new(){

                    humanoidFrames.boxRight1,
                    humanoidFrames.boxRight2,

                }),

                [2] = RectangleHumanoidList(new(){

                    humanoidFrames.boxDown1,
                    humanoidFrames.boxDown2,

                }),

                [3] = RectangleHumanoidList(new(){

                    humanoidFrames.boxLeft1,
                    humanoidFrames.boxLeft2,

                }),

            };

            custom[Character.Character.specials.pickup] = new()
            {

                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.kneel,
                    humanoidFrames.crouchRight,

                }),

                [1] = RectangleHumanoidList(new(){

                    humanoidFrames.kneel,
                    humanoidFrames.crouchRight,

                }),

                [2] = RectangleHumanoidList(new(){

                    humanoidFrames.kneel,
                    humanoidFrames.crouchRight,

                }),

                [3] = RectangleHumanoidList(new(){

                    humanoidFrames.kneel,
                    humanoidFrames.crouchRight,

                }),

            };

            return custom;

        }
        
        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> HumanoidWalk()
        {

            return new()
            {
                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.idleUp,
                    humanoidFrames.walkUpL1,
                    humanoidFrames.walkUpL2,
                    humanoidFrames.walkUpL3,
                    humanoidFrames.walkUpR1,
                    humanoidFrames.walkUpR2,
                    humanoidFrames.walkUpR3,
                }),

                [1] = RectangleHumanoidList(new(){

                    humanoidFrames.idleRight,
                    humanoidFrames.walkRightL1,
                    humanoidFrames.walkRightL2,
                    humanoidFrames.walkRightL3,
                    humanoidFrames.walkRightR1,
                    humanoidFrames.walkRightR2,
                    humanoidFrames.walkRightR3,
                }),

                [2] = RectangleHumanoidList(new(){
                    humanoidFrames.idleDown,
                    humanoidFrames.walkDownL1,
                    humanoidFrames.walkDownL2,
                    humanoidFrames.walkDownL3,
                    humanoidFrames.walkDownR1,
                    humanoidFrames.walkDownR2,
                    humanoidFrames.walkDownR3,

                }),

                [3] = RectangleHumanoidList(new(){

                    humanoidFrames.idleLeft,
                    humanoidFrames.walkLeftL1,
                    humanoidFrames.walkLeftL2,
                    humanoidFrames.walkLeftL3,
                    humanoidFrames.walkLeftR1,
                    humanoidFrames.walkLeftR2,
                    humanoidFrames.walkLeftR3,
                }),

            };

        }        
        
        public static Dictionary<Character.Character.dashes, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> HumanoidDash()
        {


            Dictionary<Character.Character.dashes, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> dashes = new();

            dashes[Character.Character.dashes.dash] = new(){

                [0] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkUpL1)
                },
                [1] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkRightL1)
                },
                [2] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkDownL1)
                },
                [3] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkLeftL1)
                },
                [4] = new()
                {
                    RectangleHumanoid(humanoidFrames.jumpUp)
                },
                [5] = new()
                {
                    RectangleHumanoid(humanoidFrames.jumpRight)
                },
                [6] = new()
                {
                    RectangleHumanoid(humanoidFrames.jumpDown)
                },
                [7] = new()
                {
                    RectangleHumanoid(humanoidFrames.jumpLeft)
                },
                [8] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkUpR1),
                    RectangleHumanoid(humanoidFrames.walkUpR2),
                    RectangleHumanoid(humanoidFrames.walkUpR3),
                },
                [9] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkRightR1),
                    RectangleHumanoid(humanoidFrames.walkRightR2),
                    RectangleHumanoid(humanoidFrames.walkRightR3),
                },
                [10] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkDownR1),
                    RectangleHumanoid(humanoidFrames.walkDownR2),
                    RectangleHumanoid(humanoidFrames.walkDownR3),
                },
                [11] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkLeftR1),
                    RectangleHumanoid(humanoidFrames.walkLeftR2),
                    RectangleHumanoid(humanoidFrames.walkLeftR3),
                },

            };

            dashes[Character.Character.dashes.smash] = new(dashes[Character.Character.dashes.dash]);

            return dashes;

        }
        
        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> WeaponSmash()
        {


            return new()
            {
                [0] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashUp1),
                    RectangleHumanoid(humanoidFrames.smashUp2)
                },
                [1] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashRight1),
                    RectangleHumanoid(humanoidFrames.smashRight2)
                },
                [2] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashDown1),
                    RectangleHumanoid(humanoidFrames.smashDown2)
                },
                [3] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashRight1),
                    RectangleHumanoid(humanoidFrames.smashRight2)
                },
                [4] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashUp3),
                },
                [5] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashRight3),
                },
                [6] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashDown3),
                },
                [7] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashRight3),
                },
                [8] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashUp4),
                },
                [9] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashRight4),
                },
                [10] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashDown4),
                },
                [11] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashRight4),
                },
            };
        }
        
        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> WeaponSweep()
        {


            return new()
            {
                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.twirlUp,
                    humanoidFrames.twirlLeft,
                    humanoidFrames.twirlDown,
                    humanoidFrames.twirlRight,

                }),

                [1] = RectangleHumanoidList(new(){

                    humanoidFrames.twirlLeft,
                    humanoidFrames.twirlDown,
                    humanoidFrames.twirlRight,
                    humanoidFrames.twirlUp,

                }),

                [2] = RectangleHumanoidList(new(){

                    humanoidFrames.twirlDown,
                    humanoidFrames.twirlRight,
                    humanoidFrames.twirlUp,
                    humanoidFrames.twirlLeft,

                }),

                [3] = RectangleHumanoidList(new(){

                    humanoidFrames.twirlRight,
                    humanoidFrames.twirlUp,
                    humanoidFrames.twirlLeft,
                    humanoidFrames.twirlDown,

                }),

            };

        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> WeaponLaunch()
        {

            return new()
            {
                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.crouchUp,
                    humanoidFrames.crouchUp,
                    humanoidFrames.crouchUpTwo,

                }),

                [1] = RectangleHumanoidList(new(){


                    humanoidFrames.crouchRight,
                    humanoidFrames.crouchRight,
                    humanoidFrames.crouchRightTwo,

                }),

                [2] = RectangleHumanoidList(new(){


                    humanoidFrames.crouchDown,
                    humanoidFrames.crouchDown,
                    humanoidFrames.crouchDownTwo,

                }),

                [3] = RectangleHumanoidList(new(){


                    humanoidFrames.crouchRight,
                    humanoidFrames.crouchRight,
                    humanoidFrames.crouchRightTwo,

                }),

            };

        }
        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> WeaponAlert()
        {


            return new()
            {
                [0] = new()
                {
                    RectangleHumanoid(humanoidFrames.alertUp)
                },
                [1] = new()
                {
                    RectangleHumanoid(humanoidFrames.alertRight)
                },
                [2] = new()
                {
                    RectangleHumanoid(humanoidFrames.alertDown)
                },
                [3] = new()
                {
                    RectangleHumanoid(humanoidFrames.alertLeft)
                },

            };
        }



        public enum flyerFrames
        {

            downIdle,
            downPeck,
            downLift,
            downFullOut,
            downHalfOut,
            downGlide,
            downHalfIn,
            downFullIn,

            rightIdle,
            rightPeck,
            rightLift,
            rightFullOut,
            rightHalfOut,
            rightGlide,
            rightHalfIn,
            rightFullIn,

            upIdle,
            upPeck,
            upLift,
            upFullOut,
            upHalfOut,
            upGlide,
            upHalfIn,
            upFullIn,

        }

        public static Microsoft.Xna.Framework.Rectangle FlyerRectangle(flyerFrames frame)
        {
            return new((int)frame % 8 * 48, (int)frame / 8 * 48, 48, 48);
        }
 
        public static Dictionary<Character.Character.idles, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> FlyerIdle()
        {
            
            Dictionary<Character.Character.idles, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> idleFrames = new();
            
            idleFrames[Character.Character.idles.standby] = new()
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

            return idleFrames;

        }

        public static Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> FlyerSpecial()
        {

            return new();

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
