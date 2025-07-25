﻿using System;
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
            rest,
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
            
            custom[Character.Character.idles.rest] = new()
            {
                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.rest,

                }),
            };

            custom[Character.Character.idles.jump] = new()
            {
                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.jumpUp,

                }),
                [1] = RectangleHumanoidList(new(){

                    humanoidFrames.jumpRight,

                }),
                [2] = RectangleHumanoidList(new(){

                    humanoidFrames.jumpDown,

                }),
                [3] = RectangleHumanoidList(new(){

                    humanoidFrames.jumpLeft,

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

            custom[Character.Character.specials.launch] = new()
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

            custom[Character.Character.specials.point] = new()
            {
                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.pointUp,
                }),
                [1] = RectangleHumanoidList(new(){

                    humanoidFrames.pointRight,
                }),
                [2] = RectangleHumanoidList(new(){

                    humanoidFrames.pointDown,
                }),
                [3] = RectangleHumanoidList(new(){

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

            custom[Character.Character.specials.gesture] = new()
            {

                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.gesture,

                }),

                [1] = RectangleHumanoidList(new(){

                    humanoidFrames.gesture,

                }),

                [2] = RectangleHumanoidList(new(){

                    humanoidFrames.gesture,

                }),

                [3] = RectangleHumanoidList(new(){

                    humanoidFrames.gesture,

                }),

            };

            custom[specials.hadouken] = new()
            {
                [0] = RectangleHumanoidList(new() { humanoidFrames.sweepRight1, humanoidFrames.sweepRight2, humanoidFrames.sweepRight3, humanoidFrames.sweepRight4, }),
                [1] = RectangleHumanoidList(new() { humanoidFrames.sweepRight1, humanoidFrames.sweepRight2, humanoidFrames.sweepRight3, humanoidFrames.sweepRight4, }),
                [2] = RectangleHumanoidList(new() { humanoidFrames.sweepRight1, humanoidFrames.sweepRight2, humanoidFrames.sweepRight3, humanoidFrames.sweepRight4, }),
                [3] = RectangleHumanoidList(new() { humanoidFrames.sweepRight1, humanoidFrames.sweepRight2, humanoidFrames.sweepRight3, humanoidFrames.sweepRight4, }),
            };

            return custom;

        }

        public static Dictionary<StardewDruid.Character.Character.specials, int> HumanoidIntervals()
        {

            return new()
            {
                [StardewDruid.Character.Character.specials.invoke] = 30,
                [StardewDruid.Character.Character.specials.special] = 30,
                [StardewDruid.Character.Character.specials.launch] = 30,
                [StardewDruid.Character.Character.specials.point] = 60,
                [StardewDruid.Character.Character.specials.sweep] = 15,
                [StardewDruid.Character.Character.specials.pickup] = 30,
                [StardewDruid.Character.Character.specials.gesture] = 60,
                [StardewDruid.Character.Character.specials.hadouken] = 15,
            };

        }
        public static Dictionary<StardewDruid.Character.Character.specials, int> HumanoidCeilings()
        {

            return new()
            {
                [StardewDruid.Character.Character.specials.invoke] = 1,
                [StardewDruid.Character.Character.specials.special] = 1,
                [StardewDruid.Character.Character.specials.launch] = 2,
                [StardewDruid.Character.Character.specials.point] = 0,
                [StardewDruid.Character.Character.specials.sweep] = 3,
                [StardewDruid.Character.Character.specials.pickup] = 1,
                [StardewDruid.Character.Character.specials.gesture] = 0,
                [StardewDruid.Character.Character.specials.hadouken] = 3,
            };
        }

        public static Dictionary<StardewDruid.Character.Character.specials, int> HumanoidFloors()
        {

            return new()
            {
                [StardewDruid.Character.Character.specials.invoke] = 1,
                [StardewDruid.Character.Character.specials.special] = 1,
                [StardewDruid.Character.Character.specials.launch] = 2,
                [StardewDruid.Character.Character.specials.point] = 0,
                [StardewDruid.Character.Character.specials.sweep] = 0,
                [StardewDruid.Character.Character.specials.pickup] = 1,
                [StardewDruid.Character.Character.specials.gesture] = 0,
                [StardewDruid.Character.Character.specials.hadouken] = 3,
            };
        }
        
        public static Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> WitchSpecial()
        {

            Dictionary<Character.Character.specials, Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>> specialFrames = HumanoidSpecial();

            specialFrames[specials.launch] = new()
            {

                [0] = RectangleHumanoidList(
                                new()
                                {
                        CharacterRender.humanoidFrames.jumpUp,
                        CharacterRender.humanoidFrames.boxUp1,
                        CharacterRender.humanoidFrames.boxUp2,
                        CharacterRender.humanoidFrames.boxUp2,
                        CharacterRender.humanoidFrames.jumpUp,
                                }),
                [1] = RectangleHumanoidList(
                                new()
                                {
                        CharacterRender.humanoidFrames.jumpRight,
                        CharacterRender.humanoidFrames.boxRight1,
                        CharacterRender.humanoidFrames.boxRight2,
                        CharacterRender.humanoidFrames.boxRight2,
                        CharacterRender.humanoidFrames.jumpRight,
                                }),
                [2] = RectangleHumanoidList(
                                new()
                                {
                        CharacterRender.humanoidFrames.jumpDown,
                        CharacterRender.humanoidFrames.boxDown1,
                        CharacterRender.humanoidFrames.boxDown2,
                        CharacterRender.humanoidFrames.boxDown2,
                        CharacterRender.humanoidFrames.jumpDown,
                                }),
                [3] = RectangleHumanoidList(
                                new()
                                {
                        CharacterRender.humanoidFrames.jumpLeft,
                        CharacterRender.humanoidFrames.boxLeft1,
                        CharacterRender.humanoidFrames.boxLeft2,
                        CharacterRender.humanoidFrames.boxLeft2,
                        CharacterRender.humanoidFrames.jumpLeft,
                                }),

            };


            specialFrames[specials.liftup] = new()
            {

                [0] = RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpUp,
                        CharacterRender.humanoidFrames.boxUp1,
                    }),
                [1] = RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpRight,
                        CharacterRender.humanoidFrames.boxRight1,
                    }),
                [2] = RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpDown,
                        CharacterRender.humanoidFrames.boxDown1,
                    }),
                [3] = RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpLeft,
                        CharacterRender.humanoidFrames.boxLeft1,
                    }),

            };



            specialFrames[specials.liftdown] = new()
            {

                [0] = RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.boxUp2,
                        CharacterRender.humanoidFrames.jumpUp,
                    }),
                [1] = RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.boxRight2,
                        CharacterRender.humanoidFrames.jumpRight,
                    }),
                [2] = RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.boxDown2,
                        CharacterRender.humanoidFrames.jumpDown,
                    }),
                [3] = RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.boxLeft2,
                        CharacterRender.humanoidFrames.jumpLeft,
                    }),

            };

            return specialFrames;

        }

        public static Dictionary<StardewDruid.Character.Character.specials, int> WitchIntervals()
        {

            return new()
            {
                [StardewDruid.Character.Character.specials.invoke] = 30,
                [StardewDruid.Character.Character.specials.special] = 30,
                [StardewDruid.Character.Character.specials.launch] = 12,
                [StardewDruid.Character.Character.specials.point] = 60,
                [StardewDruid.Character.Character.specials.sweep] = 150,
                [StardewDruid.Character.Character.specials.pickup] = 30,
                [StardewDruid.Character.Character.specials.gesture] = 60,
                [StardewDruid.Character.Character.specials.liftup] = 15,
                [StardewDruid.Character.Character.specials.liftdown] = 15,
            };

        }

        public static Dictionary<StardewDruid.Character.Character.specials, int> WitchCeilings()
        {
            return new()
            {
                [StardewDruid.Character.Character.specials.invoke] = 1,
                [StardewDruid.Character.Character.specials.special] = 1,
                [StardewDruid.Character.Character.specials.launch] = 4,
                [StardewDruid.Character.Character.specials.point] = 0,
                [StardewDruid.Character.Character.specials.sweep] = 1,
                [StardewDruid.Character.Character.specials.pickup] = 1,
                [StardewDruid.Character.Character.specials.gesture] = 0,
                [StardewDruid.Character.Character.specials.liftup] = 1,
                [StardewDruid.Character.Character.specials.liftdown] = 1,
            };
        }

        public static Dictionary<StardewDruid.Character.Character.specials, int> WitchFloors()
        {

            return new()
            {
                [StardewDruid.Character.Character.specials.invoke] = 1,
                [StardewDruid.Character.Character.specials.special] = 1,
                [StardewDruid.Character.Character.specials.launch] = 0,
                [StardewDruid.Character.Character.specials.point] = 0,
                [StardewDruid.Character.Character.specials.sweep] = 0,
                [StardewDruid.Character.Character.specials.pickup] = 1,
                [StardewDruid.Character.Character.specials.gesture] = 0,
                [StardewDruid.Character.Character.specials.liftup] = 1,
                [StardewDruid.Character.Character.specials.liftdown] = 1,
            };
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

        public static Dictionary<Character.Character.hats, Dictionary<int,Microsoft.Xna.Framework.Vector2>> HumanoidHats()
        {

            Dictionary<Character.Character.hats, Dictionary<int, Microsoft.Xna.Framework.Vector2>> hats = new()
            {

                [Character.Character.hats.stand] = new()
                {
                    [0] = new(0, 12),
                    [1] = new(0, 12),
                    [2] = new(0, 12),
                    [3] = new(0, 12),
                    [4] = new(0, 12),
                    [6] = new(0, 12),
                },
                [Character.Character.hats.jump] = new()
                {
                    [0] = new(0, 12),
                    [1] = new(0, 12),
                    [2] = new(0, 12),
                    [3] = new(0, 12),
                    [4] = new(0, 12),
                    [6] = new(0, 12),
                },
                [Character.Character.hats.kneel] = new()
                {
                    [0] = new(0, 6),
                    [1] = new(0, 6),
                    [2] = new(0, 6),
                    [3] = new(0, 6),
                    [4] = new(0, 6),
                    [6] = new(0, 6),
                },
                [Character.Character.hats.launch] = new()
                {
                    [0] = new(0, 6),
                    [1] = new(0, 6),
                    [2] = new(0, 9),
                    [3] = new(0, 6),
                    [4] = new(0, 6),
                    [6] = new(0, 9),
                },

            };

            return hats;

        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> WitchLongKick()
        {


            return new()
            {
                [0] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkUpL1),
                    RectangleHumanoid(humanoidFrames.jumpUp)
                },
                [1] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkRightL1),
                    RectangleHumanoid(humanoidFrames.jumpRight)
                },
                [2] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkDownL1),
                    RectangleHumanoid(humanoidFrames.jumpDown)
                },
                [3] = new()
                {
                    RectangleHumanoid(humanoidFrames.walkRightL1),
                    RectangleHumanoid(humanoidFrames.jumpRight)
                },
                [4] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashUp1),
                },
                [5] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashRight1),
                },
                [6] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashDown1),
                },
                [7] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashRight1),
                },
                [8] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashUp1),
                    RectangleHumanoid(humanoidFrames.smashUp1),
                    RectangleHumanoid(humanoidFrames.smashUp1),
                    RectangleHumanoid(humanoidFrames.jumpUp)
                },
                [9] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashRight1),
                    RectangleHumanoid(humanoidFrames.smashRight1),
                    RectangleHumanoid(humanoidFrames.smashRight1),
                    RectangleHumanoid(humanoidFrames.jumpRight)
                },
                [10] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashDown1),
                    RectangleHumanoid(humanoidFrames.smashDown1),
                    RectangleHumanoid(humanoidFrames.smashDown1),
                    RectangleHumanoid(humanoidFrames.jumpDown)
                },
                [11] = new()
                {
                    RectangleHumanoid(humanoidFrames.smashRight1),
                    RectangleHumanoid(humanoidFrames.smashRight1),
                    RectangleHumanoid(humanoidFrames.smashRight1),
                    RectangleHumanoid(humanoidFrames.jumpRight)
                },
            };
        }

        public static Dictionary<int,List<Microsoft.Xna.Framework.Rectangle>> WitchKick()
        {

            return new()
            {

                [0] = RectangleHumanoidList(new(){

                    humanoidFrames.jumpUp,
                    humanoidFrames.smashUp1,
                    humanoidFrames.smashUp1,
                    humanoidFrames.jumpUp,
                }),

                [1] = RectangleHumanoidList(new(){

                    humanoidFrames.jumpRight,
                    humanoidFrames.smashRight1,
                    humanoidFrames.smashRight1,
                    humanoidFrames.jumpRight,
                }),

                [2] = RectangleHumanoidList(new(){

                    humanoidFrames.jumpDown,
                    humanoidFrames.smashDown1,
                    humanoidFrames.smashDown1,
                    humanoidFrames.jumpDown,
                }),

                [3] = RectangleHumanoidList(new(){


                    humanoidFrames.jumpRight,
                    humanoidFrames.smashRight1,
                    humanoidFrames.smashRight1,
                    humanoidFrames.jumpRight,
                }),

            };

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
        
        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> WeaponSweep(WeaponRender.weapons weapon = WeaponRender.weapons.sword)
        {

            switch (weapon)
            {
                default:


                    return new()
                    {
                        [0] = RectangleHumanoidList(new(){

                            humanoidFrames.twirlUp,
                            humanoidFrames.twirlLeft,
                            humanoidFrames.twirlDown,
                            humanoidFrames.twirlRight,
                            humanoidFrames.twirlUp,
                        }),

                        [1] = RectangleHumanoidList(new(){

                            humanoidFrames.alertRight,
                            humanoidFrames.sweepRight1,
                            humanoidFrames.sweepRight2,
                            humanoidFrames.sweepRight3,
                            humanoidFrames.sweepRight4,
                        }),

                        [2] = RectangleHumanoidList(new(){

                            humanoidFrames.twirlDown,
                            humanoidFrames.twirlRight,
                            humanoidFrames.twirlUp,
                            humanoidFrames.twirlLeft,
                            humanoidFrames.twirlDown,
                        }),

                        [3] = RectangleHumanoidList(new(){

                            humanoidFrames.alertRight,
                            humanoidFrames.sweepRight1,
                            humanoidFrames.sweepRight2,
                            humanoidFrames.sweepRight3,
                            humanoidFrames.sweepRight4,
                        }),

                    };

                case WeaponRender.weapons.axe:
                case WeaponRender.weapons.carnyx:
                case WeaponRender.weapons.hammer:
                case WeaponRender.weapons.scythe:
                case WeaponRender.weapons.scythetwo:


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

        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> WeaponLaunch(WeaponRender.weapons weapon = WeaponRender.weapons.sword)
        {

            switch (weapon)
            {
                default:

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

                case WeaponRender.weapons.cannon:


                    return new()
                    {

                        [0] = RectangleHumanoidList(new(){

                            humanoidFrames.crouchUpTwo,
                            humanoidFrames.crouchUpTwo,
                            humanoidFrames.crouchUpTwo,
                            humanoidFrames.crouchUpTwo,
                            humanoidFrames.crouchUpTwo,
                            humanoidFrames.crouchUpTwo,
                            humanoidFrames.crouchUp,
                            humanoidFrames.crouchUp,
                            humanoidFrames.crouchUp,
                            humanoidFrames.crouchUp,
                        }),

                        [1] = RectangleHumanoidList(new(){

                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRight,
                            humanoidFrames.crouchRight,
                            humanoidFrames.crouchRight,
                            humanoidFrames.crouchRight,

                        }),

                        [2] = RectangleHumanoidList(new(){

                            humanoidFrames.crouchDownTwo,
                            humanoidFrames.crouchDownTwo,
                            humanoidFrames.crouchDownTwo,
                            humanoidFrames.crouchDownTwo,
                            humanoidFrames.crouchDownTwo,
                            humanoidFrames.crouchDownTwo,
                            humanoidFrames.crouchDown,
                            humanoidFrames.crouchDown,
                            humanoidFrames.crouchDown,
                            humanoidFrames.crouchDown,

                        }),

                        [3] = RectangleHumanoidList(new(){

                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRightTwo,
                            humanoidFrames.crouchRight,
                            humanoidFrames.crouchRight,
                            humanoidFrames.crouchRight,
                            humanoidFrames.crouchRight,

                        }),

                    };


            }


        }

        public static Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> WeaponAlert(WeaponRender.weapons weapon = WeaponRender.weapons.sword)
        {

            switch (weapon)
            {
                default:

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

                case WeaponRender.weapons.carnyx:

                    return new()
                    {

                        [0] = new()
                        {
                            RectangleHumanoid(humanoidFrames.twirlDown)
                        },
                        [1] = new()
                        {
                            RectangleHumanoid(humanoidFrames.twirlRight)
                        },
                        [2] = new()
                        {
                            RectangleHumanoid(humanoidFrames.twirlUp)
                        },
                        [3] = new()
                        {
                            RectangleHumanoid(humanoidFrames.twirlLeft)
                        },

                    };


            }

        }

    }

}
