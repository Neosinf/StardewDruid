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

        public double expireTime;

        public Water(Mod mod, Vector2 target, Farmer player)
            : base(mod, target, player)
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 300;

        }

        public override void CastEarth()
        {

            int probability = randomIndex.Next(50);

            if (probability <= 4) // fish
            {

                Dictionary<int, int> objectIndexes;

                switch (targetPlayer.currentLocation.Name)
                {
                    case "Farm":
                    case "Hilltop":

                        objectIndexes = new Dictionary<int, int>()
                        {
                            [0] = 153, // algae 153
                            [1] = 153, // algae 153
                            [2] = 721, // snail 721
                            [3] = 716, // crayfish 716
                            [4] = 145  // carp 145
                        };

                        break;

                    case "Forest":

                        objectIndexes = new Dictionary<int, int>()
                        {
                            [0] = 153, // algae 153
                            [1] = 153, // algae 153
                            [2] = 722, // periwinkle 722
                            [3] = 717, // crab 717
                            [4] = 142  // sunfish 142
                            
                        };

                        break;

                    case "Beach":

                        objectIndexes = new Dictionary<int, int>()
                        {
                            [0] = 153, // seaweed 152
                            [1] = 152, // seaweed 152
                            [2] = 718, // cockle 718
                            [3] = 715, // lobster 715
                            [4] = 131  // sardine 131
                            
                        };

                        break;

                    default: // "Mountain":

                        objectIndexes = new Dictionary<int, int>()
                        {
                            [0] = 152, // seaweed 152
                            [1] = 152, // seaweed 152
                            [2] = 719, // mussel 719
                            [3] = 720, // shrimp 720
                            [4] = 145  // carp 145
                        };

                        break;

                }

                int randomQuality = randomIndex.Next(11 - targetPlayer.fishingLevel.Value);

                int objectQuality = 0;

                if(randomQuality == 0 && probability >= 48)
                {
                    
                    objectQuality = 2;

                }

                Throw throwObject = new(objectIndexes[probability], objectQuality);

                throwObject.ThrowObject(targetPlayer, targetVector);

                targetPlayer.currentLocation.playSound("pullItemFromWater");

                castCost = 8;

                castFire = true;

                bool targetDirection = (targetPlayer.getTileLocation().X <= targetVector.X);

                ModUtility.AnimateSplash(targetLocation,targetVector,targetDirection);

            }

        }

        public override void CastWater() {

            List<Vector2> neighbourVectors = ModUtility.GetTilesWithinRadius(targetPlayer.currentLocation, targetVector, 3);

            Layer backLayer = targetPlayer.currentLocation.Map.GetLayer("Back");

            bool validBubbleSpot = true;

            foreach(Vector2 neighbourVector in neighbourVectors)
            {

                Tile backTile = backLayer.PickTile(new Location((int)neighbourVector.X * 64, (int)neighbourVector.Y * 64), Game1.viewport.Size);

                if(backTile != null )
                {

                    if (!backTile.TileIndexProperties.TryGetValue("Water", out _))
                    {

                        validBubbleSpot = false;

                        break;

                    }

                }

            }

            if (validBubbleSpot)
            {

                expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 300;

                portalPosition = new(targetVector.X * 64, targetVector.Y * 64);

                Microsoft.Xna.Framework.Color animationColor = new(0.6f, 1, 0.6f, 1); // light green

                Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, 51 * 64, 64, 64);

                float animationSort = (targetVector.X * 1000) + targetVector.Y+1;

                portalAnimation = new("TileSheets\\animations", animationRectangle, 80f, 10, 999999, portalPosition, false, false, animationSort, 0f, animationColor, 1f, 0f, 0f, 0f);

                targetLocation.temporarySprites.Add(portalAnimation);

                castLimit = true;

                castFire = true;

                castCost = 48;

                castActive = true;

                ModUtility.AnimateRipple(targetLocation, targetVector);

            }

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

                Random randomIndex = new Random();

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
