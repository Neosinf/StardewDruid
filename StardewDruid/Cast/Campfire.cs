using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;

namespace StardewDruid.Cast
{
    internal class Campfire : Cast
    {

        public Campfire (Mod mod, Vector2 target, Farmer player)
            : base(mod, target, player)
        {

        }

        public override void CastWater()
        {

            Vector2 newVector = new(targetVector.X, targetVector.Y);

            targetLocation.objects.Remove(targetVector);

            targetLocation.objects.Add(newVector, new Torch(newVector, 278, bigCraftable: true)
            {
                Fragility = 1,
                destroyOvernight = true
            });

            Vector2 tilePosition = new Vector2(newVector.X * 64, newVector.Y * 64);

            Game1.playSound("fireball");

            ModUtility.AnimateBolt(targetLocation, targetVector);

            castFire = true;

            castCost = 24;

            castLimit = true;

            return;

        }

    }
}
