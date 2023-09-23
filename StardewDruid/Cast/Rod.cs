using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using System;


namespace StardewDruid.Cast
{
    internal class Rod : Cast
    {

        public Rod(Mod mod, Vector2 target, Farmer player)
            : base(mod, target, player)
        {

        }

        public override void CastWater()
        {

            StardewValley.Object targetObject = targetLocation.objects[targetVector];

            targetObject.heldObject.Value = new StardewValley.Object(787, 1);

            targetObject.MinutesUntilReady = StardewValley.Utility.CalculateMinutesUntilMorning(Game1.timeOfDay);

            targetObject.shakeTimer = 1000;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

    }

}
