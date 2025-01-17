﻿using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewValley;
using StardewValley.Minigames;
using StardewValley.Tools;

namespace StardewDruid.Cast.Mists
{
    public class Fishspot : EventHandle
    {

        public int fishCounter;

        public int fishTotal;

        public bool fishingRelic;

        public Fishspot()
        {

            activeLimit = 300;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            //Mod.instance.iconData.AnimateBolt(location, origin + new Vector2(32));
            Mod.instance.spellRegister.Add(new(origin + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

            IconData.relics fishRelic = Mod.instance.relicsData.RelicMistsLocations();

            if (fishRelic != IconData.relics.none)
            {

                if (!RelicData.HasRelic(fishRelic))
                {
                    fishingRelic = true;
                }

            }

        }

        public override void EventDecimal()
        {

            if (!EventActive())
            {

                return;

            }

            fishCounter--;

            if (fishCounter <= 0)
            {

                int fishIndex = SpawnData.RandomJumpFish(location);

                ModUtility.AnimateFishJump(location, origin, fishIndex, true);

                ModUtility.AnimateFishJump(location, origin, fishIndex, false);

                fishCounter = 20;

            }

            if (Game1.player.CurrentTool == null)
            {

                return;

            };

            if (Game1.player.CurrentTool is not FishingRod fishingRod)
            {
                
                return;

            }

            if (!fishingRod.isFishing)
            {

                return;

            }

            if(Game1.currentMinigame is FishingGame)
            {

                return;

            }

            int checkTime = 24; // check bait

            if (fishingRod.attachments[0] != null)
            {
                
                checkTime = 12;

            };

            if (fishingRod.fishingBiteAccumulator <= (checkTime * 100))
            {

                fishTotal++;

                if(fishTotal >= (checkTime * 3) && fishingRelic)
                {

                    IconData.relics fishRelic = Mod.instance.relicsData.RelicMistsLocations();

                    if (fishRelic != IconData.relics.none)
                    {

                        if (!RelicData.HasRelic(fishRelic))
                        {

                            ThrowHandle throwRelic = new(Game1.player, origin, fishRelic);

                            throwRelic.register();

                            fishingRod.doneFishing(Game1.player,false);

                        }

                    }

                    fishingRelic = false;

                }

                return;

            }

            Vector2 bobberPosition = fishingRod.bobber.Value;

            Rectangle splashRectangle = new((int)origin.X - 56, (int)origin.Y - 56, 176, 176);

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

                if (!Mod.instance.questHandle.IsComplete(QuestHandle.mistsThree))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.mistsThree, 1);

                }
                else
                {

                    enableRare = true;

                }

                string objectIndex = SpawnData.RandomHighFish(Game1.player.currentLocation, enableRare, ModUtility.PositionToTile(bobberPosition));

                int animationRow = 10;

                Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

                float animationInterval = 100f;

                int animationLength = 8;

                int animationLoops = 1;

                Color animationColor = new(0.6f, 1, 0.6f, 1); // light green

                float animationSort = origin.Y / 10000;

                TemporaryAnimatedSprite newAnimation = new(
                    "TileSheets\\animations", 
                    animationRectangle, 
                    animationInterval, 
                    animationLength, 
                    animationLoops, 
                    origin, 
                    false, 
                    false, 
                    animationSort, 
                    0f, 
                    animationColor, 
                    1f, 
                    0f, 
                    0f, 
                    0f
                );

                fishingRod.startMinigameEndFunction(new StardewValley.Object(objectIndex,1));

                Game1.player.currentLocation.temporarySprites.Add(newAnimation);

                Game1.player.currentLocation.playSound("squid_bubble");

                DelayedAction.playSoundAfterDelay("FishHit", 800, Game1.player.currentLocation);

            }

            return;

        }

    }

}
