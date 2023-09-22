using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class Totem : Cast
    {

        public int targetIndex { get; set; }


        public Totem(Mod mod, Vector2 target, Farmer player, int TargetIndex)
            : base(mod, target, player)
        {

            targetIndex = TargetIndex;

        }

        public override void CastWater()
        {

            int probability = randomIndex.Next(mod.SpecialLimit());

            if (probability == 0)
            {
                
                Game1.createObjectDebris(targetIndex, (int)targetVector.X, (int)targetVector.Y-1);

                castFire = true;

                castCost = 48;

                Vector2 boltVector = new(targetVector.X, targetVector.Y - 2);

                ModUtility.AnimateBolt(targetLocation, boltVector);

                mod.SpecialIncrement();

            }

            return;

        }

    }

}
