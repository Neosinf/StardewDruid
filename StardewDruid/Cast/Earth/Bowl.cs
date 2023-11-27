using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;
using System;


namespace StardewDruid.Cast.Earth
{
    internal class Bowl : CastHandle
    {

        public Bowl(Vector2 target, Rite rite)
            : base(target, rite)
        {
            castCost = 0;
        }

        public override void CastEffect()
        {

            WateringCan wateringCan = new();

            wateringCan.WaterLeft = 100;

            (targetLocation as Farm).performToolAction(wateringCan, (int)targetVector.X, (int)targetVector.Y);

            Utility.addSprinklesToLocation(targetLocation, (int)targetVector.X - 1, (int)targetVector.Y - 1, 3, 3, 999, 333, Color.White);

            return;

        }

    }

}
