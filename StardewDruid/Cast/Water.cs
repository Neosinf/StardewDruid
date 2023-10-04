using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast
{
    internal class Water : Cast
    {

        public Vector2 portalPosition;

        public TemporaryAnimatedSprite portalAnimation;

        public Water(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            castCost = 8;

            if(rite.caster.FishingLevel > 5)
            {

                castCost = 4;

            }

            portalPosition = new(targetVector.X * 64, targetVector.Y * 64);

        }

        public override void CastEarth()
        {
            
            if (mod.ForgotEffect("forgetFish"))
            {

                return;

            }

            int probability = randomIndex.Next(60);

            if (probability >= 11) // nothing
            {
                return;
            }

            if(probability >= 8)
            {

                if (riteData.spawnIndex["critter"] && !mod.ForgotEffect("forgetCritters"))
                {

                    Portal critterPortal = new(mod, targetPlayer.getTileLocation(), riteData);

                    critterPortal.spawnFrequency = 1;

                    critterPortal.specialType = 5;

                    critterPortal.baseType = "terrain";

                    critterPortal.baseVector = targetVector;

                    critterPortal.baseTarget = true;

                    critterPortal.CastTrigger();

                }

                return;

            }

            Dictionary<int, int> objectIndexes;

            if (targetPlayer.currentLocation.Name.Contains("Beach"))
            {

                objectIndexes = new Dictionary<int, int>()
                {
                        
                    [0] = 152, // seaweed
                    [1] = 152, // seaweed
                    [2] = 152, // seaweed
                    [3] = 701, // tilapia
                    [4] = 131, // sardine
                    [5] = 147, // herring
                    [6] = 129, // anchovy
                    [7] = 150, // red snapper

                };

            }
            else
            {

                objectIndexes = new Dictionary<int, int>()
                {
                        
                    [0] = 153, // algae
                    [1] = 153, // algae
                    [2] = 153, // algae
                    [3] = 141, // perch
                    [4] = 145, // carp
                    [5] = 137, // smallmouth bass
                    [6] = 142,  // sunfish
                    [7] = 132  // bream
                    
                };

            }

            int objectQuality = 0;

            int experienceGain;

            if (probability <= 3)
            {
                    
                experienceGain = 6;

            }
            else
            {

                experienceGain = 12;

                if (randomIndex.Next(11 - targetPlayer.fishingLevel.Value) == 0)
                {

                    objectQuality = 2;

                    experienceGain = 24;

                }

                if (targetPlayer.professions.Contains(6))
                {

                    objectQuality = 3;

                    experienceGain = 36;

                }

            }

            Throw throwObject = new(objectIndexes[probability], objectQuality);

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

            Microsoft.Xna.Framework.Color animationColor = new(0.6f, 1, 0.6f, 1); // light green

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, 51 * 64, 64, 64);

            float animationSort = (targetVector.X * 1000) + targetVector.Y+1;

            portalAnimation = new("TileSheets\\animations", animationRectangle, 80f, 10, 999999, portalPosition, false, false, animationSort, 0f, animationColor, 1f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(portalAnimation);

            castLimit = true;

            castFire = true;

            castActive = true;

            ModUtility.AnimateRipple(targetLocation, targetVector);

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

            targetLocation.temporarySprites.Remove(portalAnimation);

        }
        
        public override void CastTrigger()
        {

            if (Game1.player.CurrentTool == null)
            {

                return;

            };

            if (Game1.player.CurrentTool.GetType().Name != "FishingRod")
            {

                return;

            }

            FishingRod fishingRod = Game1.player.CurrentTool as FishingRod;

            if (!fishingRod.isFishing)
            {

                return;

            }

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

            Microsoft.Xna.Framework.Rectangle splashRectangle = new((int)portalPosition.X - 32, (int)portalPosition.Y - 32, 128, 128);

            Microsoft.Xna.Framework.Rectangle bobberRectangle = new((int)bobberPosition.X, (int)bobberPosition.Y, 64, 64);

            if (bobberRectangle.Intersects(splashRectangle))
            {

                fishingRod.fishingBiteAccumulator = 0f;
                fishingRod.timeUntilFishingBite = -1f;
                fishingRod.isNibbling = true;
                fishingRod.timePerBobberBob = 1f;
                fishingRod.timeUntilFishingNibbleDone = FishingRod.maxTimeToNibble;
                fishingRod.hit = true;

                Dictionary<int, int> objectIndexes;

                //Random randomIndex = new();

                switch (Game1.currentLocation.Name)
                {

                    case "Beach":
                    case "IslandSouth":
                    case "IslandSouthEast":
                    case "IslandWest":

                        objectIndexes = new()
                        {
                            [0] = 148, // eel
                            [1] = 149, // squid
                            [2] = 151, // octopus
                            [3] = 155, // super cucumber
                            [4] = 128, // puff ball
                            [5] = 836  // stingray
                        };

                        break;

                    case "Woods":
                    case "Desert":

                        objectIndexes = new()
                        {
                            [0] = 161, // ice pip
                            [1] = 734, // wood skip
                            [2] = 164, // sand fish
                            [3] = 165, // scorpion carp
                            [4] = 162, // lava eel
                            [5] = 156, // ghost fish
                        };

                        break;

                    default: // default

                        objectIndexes = new()
                        {
                            [0] = 143, // cat fish
                            [1] = 698, // sturgeon
                            [2] = 140, // walleye
                            [3] = 699, // tiger trout
                            [4] = 158, // stone fish
                            [5] = 269, // midnight carp

                        };

                        break;

                }

                int objectIndex = objectIndexes[randomIndex.Next(objectIndexes.Count)];

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
