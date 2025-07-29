using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
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

        public StardewValley.Object currentFish;

        public Fishspot()
        {

        }

        public override void EventActivate()
        {

            base.EventActivate();

            Mod.instance.spellRegister.Add(new(origin + new Vector2(32), 128, IconData.impacts.splash, new()) { type = SpellHandle.Spells.bolt });

            IconData.relics fishRelic = Mod.instance.relicHandle.RelicMistsLocations();

            if (fishRelic != IconData.relics.none)
            {

                if (!RelicHandle.HasRelic(fishRelic))
                {
                    fishingRelic = true;
                }

            }

            fishCounter = 10;

            currentFish = new StardewValley.Object(SpawnData.RandomHighFish(Game1.player.currentLocation, ModUtility.PositionToTile(origin), Mod.instance.questHandle.IsComplete(QuestHandle.mistsThree)), 1);

        }

        public override void EventDecimal()
        {

            fishCounter--;

            if (fishCounter <= 0)
            {

                currentFish = new StardewValley.Object(SpawnData.RandomHighFish(Game1.player.currentLocation, ModUtility.PositionToTile(origin), Mod.instance.questHandle.IsComplete(QuestHandle.mistsThree)),1);

                ModUtility.AnimateFishJump(location, origin, currentFish, true);

                ModUtility.AnimateFishJump(location, origin, currentFish, false);

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

                    IconData.relics fishRelic = Mod.instance.relicHandle.RelicMistsLocations();

                    if (fishRelic != IconData.relics.none)
                    {

                        if (!RelicHandle.HasRelic(fishRelic))
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

                if (!Mod.instance.questHandle.IsComplete(QuestHandle.mistsThree))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.mistsThree, 1);

                }

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

                fishingRod.startMinigameEndFunction(currentFish);

                Game1.player.currentLocation.temporarySprites.Add(newAnimation);

                Game1.player.currentLocation.playSound("squid_bubble");

                DelayedAction.playSoundAfterDelay("FishHit", 800, Game1.player.currentLocation);

            }

            return;

        }

    }

}
