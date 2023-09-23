using Microsoft.Xna.Framework;
using StardewValley;

namespace StardewDruid.Cast
{
    internal class Weed : Cast
    {

        public Weed(Mod mod, Vector2 target, Farmer player)
            : base(mod, target, player)
        {

            castCost = 0;

        }

        public override void CastEarth()
        {

            if(!targetLocation.objects.ContainsKey(targetVector))
            {

                return;

            }

            StardewValley.Object targetObject = targetLocation.objects[targetVector];

            targetObject.Fragility = 0;

            if (targetObject.name.Contains("Weeds"))
            {
                
                StardewValley.Tools.MeleeWeapon targetScythe = new(47);

                targetScythe.DoFunction(targetLocation, 0, 0, 1, targetPlayer);

                targetObject.performToolAction(targetScythe, targetLocation);

            }

            if (targetObject.name.Contains("Twig"))
            {

                Game1.createRadialDebris(targetLocation, 12, (int)targetVector.X, (int)targetVector.Y, Game1.random.Next(4, 10), resource: false);
                
                Game1.createObjectDebris(388, (int)targetVector.X + 1, (int)targetVector.Y + 1);

            }

            targetLocation.explode(targetVector, 2, targetPlayer, false);

            castFire = true;

        }

    }
}
