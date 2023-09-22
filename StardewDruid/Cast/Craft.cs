using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class Craft: Cast
    {

        private StardewValley.Object targetObject;

        public Craft(Mod mod, Vector2 target, Farmer player, StardewValley.Object TargetObject)
            : base(mod, target, player)
        {

            targetObject = TargetObject;

        }

        public override void CastWater()
        {

            int probability = randomIndex.Next(mod.SpecialLimit());

            if(probability == 0)
            {

                Utility.addSprinklesToLocation(targetPlayer.currentLocation, (int)targetVector.X, (int)targetVector.Y, 1, 2, 400, 40, Color.White);

                Game1.playSound("yoba");

                targetObject.MinutesUntilReady = 10;

                DelayedAction.functionAfterDelay(delegate
                {
                    targetObject.minutesElapsed(10, targetPlayer.currentLocation);
                }, 50);

                castFire = true;

                castCost = 48;

                mod.SpecialIncrement();

            }

            return;

        }


    }
}
