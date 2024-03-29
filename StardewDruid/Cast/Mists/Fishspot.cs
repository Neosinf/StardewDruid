﻿using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Tools;

namespace StardewDruid.Cast.Mists
{
    public class Fishspot : EventHandle
    {

        public int fishCounter;

        public Fishspot(Vector2 target)
            : base(target)
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 300;

        }

        public override void EventTrigger()
        {

            ModUtility.AnimateBolt(targetLocation, targetVector);

            Utility.addSprinklesToLocation(targetLocation, (int)targetVector.X, (int)targetVector.Y, 3, 3, 999, 333, Color.White);

            Mod.instance.RegisterEvent(this, "fishspot");

        }

        public override void EventInterval()
        {

            if (Game1.player.CurrentTool == null)
            {

                return;

            };

            fishCounter--;

            if (fishCounter <= 0)
            {

                int fishIndex = SpawnData.RandomJumpFish(targetLocation);

                ModUtility.AnimateFishJump(targetLocation, targetVector * 64, fishIndex, randomIndex.Next(2) == 0);

                Utility.addSprinklesToLocation(targetLocation, (int)targetVector.X, (int)targetVector.Y, 3, 3, 5000, 500, Color.White * 0.75f);

                fishCounter = 5;

            }

            if (Game1.player.CurrentTool is not FishingRod fishingRod)
            {
                return;

            }

            if (!fishingRod.isFishing)
            {

                return;

            }

            Vector2 portalPosition = new(targetVector.X * 64, targetVector.Y * 64);

            int checkTime = 4000; // check bait

            if (fishingRod.attachments[0] != null)
            {
                checkTime = 2000;

            };

            if (fishingRod.fishingBiteAccumulator <= checkTime)
            {

                return;

            }

            Vector2 bobberPosition = fishingRod.bobber.Value;

            Rectangle splashRectangle = new((int)portalPosition.X - 56, (int)portalPosition.Y - 56, 176, 176);

            Rectangle bobberRectangle = new((int)bobberPosition.X, (int)bobberPosition.Y, 64, 64);

            if (bobberRectangle.Intersects(splashRectangle))
            {

                fishingRod.fishingBiteAccumulator = 0f;
                fishingRod.timeUntilFishingBite = -1f;
                fishingRod.isNibbling = true;
                fishingRod.timePerBobberBob = 1f;
                fishingRod.timeUntilFishingNibbleDone = FishingRod.maxTimeToNibble;
                fishingRod.hit = true;

                bool enableRare = false;

                if (!Mod.instance.rite.castTask.ContainsKey("masterFishspot"))
                {

                    Mod.instance.UpdateTask("lessonFishspot", 1);

                }
                else
                {

                    enableRare = true;

                }

                int objectIndex = SpawnData.RandomHighFish(targetLocation, enableRare);

                int animationRow = 10;

                Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

                float animationInterval = 100f;

                int animationLength = 8;

                int animationLoops = 1;

                Color animationColor = new(0.6f, 1, 0.6f, 1); // light green

                float animationSort = targetVector.X * 1000 + targetVector.Y + 1;

                TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, portalPosition, false, false, animationSort, 0f, animationColor, 1f, 0f, 0f, 0f);

                fishingRod.startMinigameEndFunction(new StardewValley.Object(objectIndex.ToString(),1));

                Game1.player.currentLocation.temporarySprites.Add(newAnimation);

                Game1.player.currentLocation.playSound("squid_bubble");

                DelayedAction.playSoundAfterDelay("FishHit", 800, Game1.player.currentLocation);

            }

            return;

        }
    }
}
