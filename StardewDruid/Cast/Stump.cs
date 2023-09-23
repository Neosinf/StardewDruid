using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast
{
    internal class Stump : Cast
    {

        private ResourceClump resourceClump;

        public Stump(Mod mod, Vector2 target, Farmer player, ResourceClump ResourceClump)
            : base(mod, target, player)
        {

            resourceClump = ResourceClump;

        }

        public override void CastEarth()
        {

            int probability = randomIndex.Next(10);

            if (probability <= 8)
            {

                Game1.createObjectDebris(388, (int)this.targetVector.X, (int)this.targetVector.Y);

                Game1.createObjectDebris(388, (int)this.targetVector.X, (int)this.targetVector.Y + 1);

                Game1.createObjectDebris(388, (int)this.targetVector.X + 1, (int)this.targetVector.Y);

                if (probability <= 3) // wood
                {

                    Game1.createObjectDebris(388, (int)this.targetVector.X + 1, (int)this.targetVector.Y);

                    Game1.createObjectDebris(388, (int)this.targetVector.X + 1, (int)this.targetVector.Y + 1);

                }

                //if (probability <= 7) // hardwood
                //{

                //   Game1.createObjectDebris(709, (int)this.targetVector.X, (int)this.targetVector.Y + 1);

                //   Game1.createObjectDebris(709, (int)this.targetVector.X + 1, (int)this.targetVector.Y + 1);

                //}

                if (probability == 8) // seed
                {

                    Game1.createObjectDebris(382, (int)this.targetVector.X + 1, (int)this.targetVector.Y);

                    //Game1.createObjectDebris(382, (int)this.targetVector.X + 1, (int)this.targetVector.Y + 1);

                }

                castFire = true;

                targetPlayer.gainExperience(2, 6); // gain foraging experience

                ModUtility.AnimateGrowth(targetLocation,targetVector);

            }
            /*else // mushroom
            {

                Dictionary<int, int> objectIndexes = new()
                {
                    [0] = 257, // 257 morel
                    [1] = 281, // 281 chanterelle
                    [2] = 404, // 404 mushroom
                    [3] = 420  // 420 red mushroom

                };

                int objectIndex = randomIndex.Next(4);

                int randomQuality = randomIndex.Next(11 - targetPlayer.foragingLevel.Value);

                int objectQuality = 0;

                if (randomQuality == 0)
                {
                    objectQuality = 2;
                }

                Throw throwObject = new(objectIndexes[objectIndex], objectQuality);

                throwObject.ThrowObject(targetPlayer, targetVector);

                castFire = true;

                ModUtility.AnimateGrowth(targetLocation, targetVector);

            }*/

        }

        public override void CastWater()
        {

            StardewValley.Tools.Axe targetAxe = new();

            targetAxe.UpgradeLevel = 3;

            targetAxe.DoFunction(targetLocation, 0, 0, 1, targetPlayer);

            resourceClump.health.Set(1f);

            resourceClump.performToolAction(targetAxe, 1, targetVector, targetLocation);

            resourceClump.NeedsUpdate = false;

            targetLocation._activeTerrainFeatures.Remove(resourceClump);

            targetLocation.resourceClumps.Remove(resourceClump);

            resourceClump.currentLocation = null;

            Game1.createObjectDebris(709, (int)this.targetVector.X, (int)this.targetVector.Y);

            Game1.createObjectDebris(709, (int)this.targetVector.X + 1, (int)this.targetVector.Y);

            Game1.createObjectDebris(382, (int)this.targetVector.X + 1, (int)this.targetVector.Y);

            Game1.createObjectDebris(382, (int)this.targetVector.X + 1, (int)this.targetVector.Y);

            castFire = true;

            castCost = 24;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

    }
}
