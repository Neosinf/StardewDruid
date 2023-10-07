using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast
{
    internal class Stump : Cast
    {

        private ResourceClump resourceClump;

        private string resourceType;

        public Stump(Mod mod, Vector2 target, Rite rite, ResourceClump ResourceClump, string ResourceType)
            : base(mod, target, rite)
        {

            resourceClump = ResourceClump;

            resourceType = ResourceType;

        }

        public override void CastEarth()
        {

            int debrisType = 388;

            int debrisAmount = randomIndex.Next(1, 5);

            for (int i = 0; i < debrisAmount; i++)
            {

                Game1.createObjectDebris(debrisType, (int)targetVector.X, (int)targetVector.Y + 1);

            }

            if (debrisAmount == 1)
            {
                Game1.createObjectDebris(382, (int)targetVector.X + 1, (int)targetVector.Y);

            }

            castFire = true;

            targetPlayer.gainExperience(2, 2); // gain foraging experience

            ModUtility.AnimateGrowth(targetLocation,targetVector);

        }

        public override void CastWater()
        {

            StardewValley.Tools.Axe targetAxe = mod.RetrieveAxe();

            castCost = Math.Max(2, 36 - (targetPlayer.ForagingLevel * targetAxe.UpgradeLevel));

            if (resourceClump == null)
            {

                return;

            }

            resourceClump.health.Set(1f);

            resourceClump.performToolAction(targetAxe, 1, targetVector, targetLocation);

            resourceClump.NeedsUpdate = false;

            if (targetAxe.UpgradeLevel >= 3)
            {
                Game1.createObjectDebris(709, (int)this.targetVector.X, (int)this.targetVector.Y);

                Game1.createObjectDebris(709, (int)this.targetVector.X + 1, (int)this.targetVector.Y);

            }

            switch (resourceType)
            {

                case "Woods":

                    Woods woodsLocation = riteData.castLocation as Woods;

                    if (woodsLocation.stumps.Contains(resourceClump))
                    {

                        woodsLocation.stumps.Remove(resourceClump);

                    }

                    break;

                case "Log":

                    Forest forestLocation = riteData.castLocation as Forest;

                    forestLocation.log = null;

                    break;

                default: // Farm

                    if (targetLocation._activeTerrainFeatures.Contains(resourceClump))
                    {

                        targetLocation._activeTerrainFeatures.Remove(resourceClump);

                    }

                    if (targetLocation.resourceClumps.Contains(resourceClump))
                    {

                        targetLocation.resourceClumps.Remove(resourceClump);

                    }

                    break;

            }

            resourceClump = null;

            castFire = true;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

    }
}
