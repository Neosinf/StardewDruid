﻿using Microsoft.Xna.Framework;
using StardewValley;


namespace StardewDruid.Cast.Mists
{
    internal class Rod : CastHandle
    {

        public Rod(Vector2 target, Rite rite)
            : base(target, rite)
        {
            castCost = 0;
        }

        public override void CastEffect()
        {

            StardewValley.Object targetObject = targetLocation.objects[targetVector];

            targetObject.heldObject.Value = new StardewValley.Object(787, 1);

            targetObject.MinutesUntilReady = Utility.CalculateMinutesUntilMorning(Game1.timeOfDay);

            targetObject.shakeTimer = 1000;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

    }

}
