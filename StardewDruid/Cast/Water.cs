using Microsoft.Xna.Framework;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;
using static StardewValley.Minigames.MineCart.Whale;

namespace StardewDruid.Cast
{
    internal class Water : CastHandle
    {

        //public TemporaryAnimatedSprite portalAnimation;

        public int fishCounter;

        public Water(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

        }

        public override void CastEarth()
        {
            
            if (!riteData.castToggle.ContainsKey("forgetFish"))
            {

                return;

            }

            int catchChance = 120 - (targetPlayer.FishingLevel * 5);

            int probability = randomIndex.Next(catchChance);

            if (probability >= 12) // nothing
            {
                return;
            }

            if(probability >= 8)
            {

                if (riteData.spawnIndex["critter"] && !riteData.castToggle.ContainsKey("forgetCritters"))
                {

                    Portal critterPortal = new(mod, targetPlayer.getTileLocation(), riteData);

                    critterPortal.spawnFrequency = 1;

                    critterPortal.spawnIndex = new()
                    {
                        0,3,99,

                    };

                    critterPortal.baseType = "terrain";

                    critterPortal.baseVector = targetVector;

                    critterPortal.baseTarget = true;

                    critterPortal.CastTrigger();

                    if (critterPortal.spawnQueue.Count > 0)
                    {

                        if (!riteData.castTask.ContainsKey("masterCreature"))
                        {

                            mod.UpdateTask("lessonCreature", 1);

                        }

                    }
                }

                return;

            }

            int randomFish = SpawnData.RandomLowFish(targetLocation);

            int objectQuality = 0;

            int experienceGain;

            //if (probability <= 3)
            //{
                    
            //   experienceGain = 4;

            //}
            //else
            //{

                experienceGain = 8;

                if (randomIndex.Next(11 - targetPlayer.fishingLevel.Value) == 0)
                {

                    objectQuality = 2;

                    experienceGain = 16;

                }

                /*if (targetPlayer.professions.Contains(6))
                {

                    objectQuality = 3;

                    experienceGain = 36;

                }*/

            //}

            StardewDruid.Cast.Throw throwObject = new(randomFish, objectQuality);

            throwObject.ThrowObject(targetPlayer, targetVector);

            targetPlayer.currentLocation.playSound("pullItemFromWater");

            targetPlayer.gainExperience(1, experienceGain); // gain fishing experience

            castFire = true;

            castLimit = true;

            bool targetDirection = (targetPlayer.getTileLocation().X <= targetVector.X);

            ModUtility.AnimateSplash(targetLocation,targetVector,targetDirection);

        }

        public override void CastWater() {

            castCost = Math.Max(8, 48 - (targetPlayer.FishingLevel * 3));

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 300;

            //portalAnimation = ModUtility.AnimateFishSpot(targetLocation, targetVector);

            castLimit = true;

            castFire = true;

            castActive = true;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            //ModUtility.AnimateRipple(targetLocation, targetVector);
            Utility.addSprinklesToLocation(targetLocation, (int)targetVector.X, (int)targetVector.Y, 3, 3, 999, 333, Color.White);

            return;

        }

        public override bool CastActive(int castIndex, int castLimit)
        {

            if(castIndex != castLimit) // replaced by new instance
            {

                return false;

            }

            if (expireTime >= Game1.currentGameTime.TotalGameTime.TotalSeconds && targetPlayer.currentLocation == targetLocation)
            {

                return true;

            }

            return false;

        }
       
        public override void CastRemove()
        {

            //targetLocation.temporarySprites.Remove(portalAnimation);

        }
        
        public override void CastTrigger()
        {

            if (Game1.player.CurrentTool == null)
            {

                return;

            };

            fishCounter--;

            if (fishCounter <= 0) {

                ModUtility.AnimateFishJump(targetLocation, targetVector);

                fishCounter = 5;

            }

            if (Game1.player.CurrentTool.GetType().Name != "FishingRod")
            {
                return;

            }

            FishingRod fishingRod = Game1.player.CurrentTool as FishingRod;

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

            Vector2 bobberPosition = fishingRod.bobber;

            Microsoft.Xna.Framework.Rectangle splashRectangle = new((int)portalPosition.X - 56, (int)portalPosition.Y - 56, 176, 176);

            Microsoft.Xna.Framework.Rectangle bobberRectangle = new((int)bobberPosition.X, (int)bobberPosition.Y, 64, 64);

            if (bobberRectangle.Intersects(splashRectangle))
            {

                fishingRod.fishingBiteAccumulator = 0f;
                fishingRod.timeUntilFishingBite = -1f;
                fishingRod.isNibbling = true;
                fishingRod.timePerBobberBob = 1f;
                fishingRod.timeUntilFishingNibbleDone = FishingRod.maxTimeToNibble;
                fishingRod.hit = true;

                bool enableRare = false;

                if(!riteData.castTask.ContainsKey("masterFishspot"))
                {

                    mod.UpdateTask("lessonFishspot", 1);

                }
                else
                {

                    enableRare = true;

                }

                int objectIndex = SpawnData.RandomHighFish(targetLocation, enableRare);

                int animationRow = 10;

                Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

                float animationInterval = 100f;

                int animationLength = 8;

                int animationLoops = 1;

                Color animationColor = new(0.6f, 1, 0.6f, 1); // light green

                float animationSort = (targetVector.X * 1000) + targetVector.Y + 1;

                TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, portalPosition, false, false, animationSort, 0f, animationColor, 1f, 0f, 0f, 0f)
                {
                    endFunction = fishingRod.startMinigameEndFunction,
                    extraInfoForEndBehavior = objectIndex,
                    id = animationSort
                };

                Game1.player.currentLocation.temporarySprites.Add(newAnimation);

                Game1.player.currentLocation.playSound("squid_bubble");

                DelayedAction.playSoundAfterDelay("FishHit", 800, Game1.player.currentLocation);

            }
           
            return;

        }

    }

}
