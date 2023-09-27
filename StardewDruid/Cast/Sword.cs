using Microsoft.Xna.Framework;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast
{
    internal class Sword : Cast
    {

        private int swordIndex;

        private readonly Quest questData;

        public Sword(Mod mod, Vector2 target, Rite rite, Quest quest)
            : base(mod, target, rite)
        {

            questData = quest;

        }

        public override void CastQuest()
        {

            switch(questData.triggerCast)
            {

                case "CastStars":

                    CastStars();

                    break;

                case "CastWater":

                    CastWater();

                    break;

                default: // CastEarth

                    CastEarth();

                    break;

            }

        }

        public override void CastEarth()
        {
            //---------------------- throw Forest Sword

            swordIndex = 15;

            int delayThrow = 600;

            ThrowSword(questData.triggerVector + new Vector2(0, -4), delayThrow);

            //----------------------- cast animation

            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(-2, -2));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(-2, -3));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(-1, -4));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(-0, -5));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(1, -5));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(1, -4));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(2, -3));
            ModUtility.AnimateGrowth(targetLocation, questData.triggerVector + new Vector2(2, -2));

            mod.UpdateQuest("swordEarth", true);

        }

        public override void CastWater()
        {
            //---------------------- throw Neptune Glaive

            swordIndex = 14;

            int delayThrow = 600;

            Vector2 originVector = targetVector + new Vector2(3, 4);

            ThrowSword(originVector, delayThrow);

            //----------------------- strike animations

            ModUtility.AnimateBolt(targetLocation, originVector);

            mod.UpdateQuest("swordWater", true);

        }

        public override void CastStars()
        {

            //---------------------- throw Lava Katana

            swordIndex = 9;

            int delayThrow = 600;

            Vector2 originVector = targetVector + new Vector2(5, 0);

            ThrowSword(originVector, delayThrow);

            //---------------------- meteor animation

            ModUtility.AnimateMeteor(targetLocation, originVector, true);

            mod.UpdateQuest("swordStars", true);

        }

        public void ThrowSword(Vector2 originVector, int delayThrow = 200)
        {
            /*
             * compensate       compensate for downward arc // 555 seems a nice substitute for 0.001 compounded 1000 times
             * 
             * motion           the movement of the animation every millisecond
             * 
             * acceleration     positive Y movement every millisecond creates a downward arc
             * 
             */

            int swordOffset = swordIndex % 8;

            int swordRow = (swordIndex-swordOffset) / 8;

            Rectangle swordRectangle = new(swordOffset*16, swordRow*16, 16, 16);

            Vector2 targetPosition = new(originVector.X * 64, (originVector.Y * 64) - 96);

            Vector2 playerPosition = targetPlayer.Position;

            float animationInterval = 1000f;

            float motionX = (playerPosition.X - targetPosition.X) / 1000;

            float compensate = 0.555f;

            float motionY = ((playerPosition.Y - targetPosition.Y) / 1000) - compensate;

            float animationSort = (originVector.X * 1000) + originVector.Y + 20;

            TemporaryAnimatedSprite throwAnimation = new("TileSheets\\weapons", swordRectangle, animationInterval, 1, 0, targetPosition, flicker: false, flipped: false, animationSort, 0f, Color.White, 4f, 0f, 0f, 0.2f)
            {

                motion = new Vector2(motionX, motionY),

                acceleration = new Vector2(0f, 0.001f),

                timeBasedMotion = true,

                endFunction = CatchSword,

                delayBeforeAnimationStart = delayThrow,

            };

            targetLocation.temporarySprites.Add(throwAnimation);

        }

        public void CatchSword(int EndBehaviour)
        {
            
            Item targetSword = new MeleeWeapon(swordIndex);

            targetPlayer.addItemByMenuIfNecessaryElseHoldUp(targetSword);

        }

    }
}
