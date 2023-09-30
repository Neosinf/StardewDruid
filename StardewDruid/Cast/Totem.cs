using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class Totem : Cast
    {

        public int targetIndex { get; set; }


        public Totem(Mod mod, Vector2 target, Rite rite, int TargetIndex)
            : base(mod, target, rite)
        {

            targetIndex = TargetIndex;

        }

        public override void CastWater()
        {

            //int probability = randomIndex.Next(mod.SpecialLimit());

            //if (probability == 0)
            //{

            for (int i = 0; i < randomIndex.Next(1, 3); i++)
            {
                Game1.createObjectDebris(targetIndex, (int)targetVector.X, (int)targetVector.Y - 1);

            }

            castFire = true;

            castCost = 48;

            Vector2 boltVector = new(targetVector.X, targetVector.Y - 2);

            ModUtility.AnimateBolt(targetLocation, boltVector);

            //mod.SpecialIncrement();

            //}

            return;

        }

    }

}
