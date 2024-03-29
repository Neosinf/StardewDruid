﻿using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Tools;


namespace StardewDruid.Cast.Weald
{
    internal class Bowl : CastHandle
    {

        public Bowl(Vector2 target)
            : base(target)
        {
            castCost = 0;
        }

        public override void CastEffect()
        {

            Mod.instance.virtualCan.WaterLeft = 100;

            (targetLocation as Farm).performToolAction(Mod.instance.virtualCan, (int)targetVector.X, (int)targetVector.Y);

            ModUtility.AnimateSparkles(targetLocation, targetVector, Color.White);

            return;

        }

    }

}
