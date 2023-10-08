using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class Weed : Cast
    {

        public Weed(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            castCost = 1;

        }

        public override void CastEarth()
        {

            if(!targetLocation.objects.ContainsKey(targetVector))
            {

                return;

            }

            int powerLevel = riteData.castAxe.UpgradeLevel;

            StardewValley.Object targetObject = targetLocation.objects[targetVector];

            targetObject.Fragility = 0;

            int explodeRadius = (powerLevel < 2) ? 2 : powerLevel;

            /*if (targetObject.name.Contains("Weeds"))
            {

                StardewValley.Tools.MeleeWeapon targetScythe = new(47);

                targetScythe.DoFunction(targetLocation, 0, 0, 1, targetPlayer);

                targetObject.performToolAction(targetScythe, targetLocation);

            }

            if (targetObject.name.Contains("Twig"))
            {

                Game1.createRadialDebris(targetLocation, 12, (int)targetVector.X, (int)targetVector.Y, Game1.random.Next(4, 10), resource: false);
                
                Game1.createObjectDebris(388, (int)targetVector.X + 1, (int)targetVector.Y + 1);

            }*/

            if (targetLocation is StardewValley.Locations.MineShaft && targetObject.name.Contains("Stone"))
            {

                explodeRadius = Math.Min(6,2 + powerLevel);

                Game1.playSound("fireball");


            }

            targetLocation.explode(targetVector, explodeRadius, targetPlayer, false);

            castFire = true;

        }

    }

}
