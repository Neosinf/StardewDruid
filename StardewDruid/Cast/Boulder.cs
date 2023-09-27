using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;

namespace StardewDruid.Cast
{
    internal class Boulder : Cast
    {

        private readonly ResourceClump resourceClump;

        public Boulder(Mod mod, Vector2 target, Rite rite, ResourceClump ResourceClump)
            : base(mod, target, rite)
        {

            resourceClump = ResourceClump;

        }

        public override void CastEarth()
        {

            int debrisType = 390;

            int debrisAmount = randomIndex.Next(1, 6);

            for (int i = 0; i < debrisAmount; i++)
            {

                Game1.createObjectDebris(debrisType, (int)targetVector.X, (int)targetVector.Y + 1);

            }

            if (debrisAmount == 1)
            {
                Game1.createObjectDebris(382, (int)targetVector.X + 1, (int)targetVector.Y);

                Game1.createObjectDebris(382, (int)targetVector.X, (int)targetVector.Y + 1);

            }

            castFire = true;

            targetPlayer.gainExperience(2, 4); // gain foraging experience

            ModUtility.AnimateGrowth(targetLocation, targetVector);

        }

        public override void CastWater()
        {

            StardewValley.Tools.Pickaxe targetAxe = new();

            targetAxe.UpgradeLevel = 3;

            targetAxe.DoFunction(targetLocation, 0, 0, 1, targetPlayer);

            resourceClump.health.Set(1f);

            resourceClump.performToolAction(targetAxe, 1, targetVector, targetLocation);

            resourceClump.NeedsUpdate = false;

            targetLocation._activeTerrainFeatures.Remove(resourceClump);

            targetLocation.resourceClumps.Remove(resourceClump);

            resourceClump.currentLocation = null;

            Game1.createObjectDebris(382, (int)targetVector.X + 1, (int)targetVector.Y);

            Game1.createObjectDebris(382, (int)targetVector.X, (int)targetVector.Y + 1);

            Game1.createObjectDebris(382, (int)targetVector.X + 1, (int)targetVector.Y + 1);

            switch ((int)resourceClump.parentSheetIndex.Value)
            {

                //case 752:
                case 754:

                    Game1.createObjectDebris(537, (int)targetVector.X, (int)targetVector.Y);

                    //Game1.createObjectDebris(537, (int)targetVector.X, (int)targetVector.Y + 1);

                    break;

                case 756:
                case 758:

                    Game1.createObjectDebris(536, (int)targetVector.X, (int)targetVector.Y);

                    //Game1.createObjectDebris(536, (int)targetVector.X, (int)targetVector.Y + 1);

                    break;

                default:

                    Game1.createObjectDebris(535, (int)targetVector.X, (int)targetVector.Y);

                    //Game1.createObjectDebris(535, (int)targetVector.X, (int)targetVector.Y + 1);

                    break;

            }

            castFire = true;

            castCost = 24;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

    }
}
