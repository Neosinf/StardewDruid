using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast
{
    internal class Pool : Cast
    {

        public Pool(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

        }

        public override void CastEarth()
        {
           
            if (mod.ForgotEffect("forgetFish"))
            {

                return;

            }

            int probability = randomIndex.Next(40);

            if (probability >= 10)
            {
                return;
            }

            if (probability >= 8)
            {

                if (riteData.spawnIndex["critter"] && !mod.ForgotEffect("forgetCritters"))
                {

                    Portal critterPortal = new(mod, targetPlayer.getTileLocation(), riteData);

                    critterPortal.spawnFrequency = 1;

                    critterPortal.specialType = 5;

                    critterPortal.baseType = "terrain";

                    critterPortal.baseVector = targetVector;

                    critterPortal.baseTarget = true;

                    critterPortal.CastTrigger();

                }

                return;

            }

            Dictionary<int, int> objectIndexes;

            if (targetLocation.Name.Contains("Beach"))
            {

                objectIndexes = new Dictionary<int, int>()
                {

                    [0] = 392, // nautilus shell
                    [1] = 393, // coral
                    [2] = 394, // rainbow shell
                    [3] = 397, // urchin
                    [4] = 718, // cockle
                    [5] = 715, // lobster
                    [6] = 720, // shrimp
                    [7] = 719, // mussel

                };

            }
            else
            {

                objectIndexes = new Dictionary<int, int>()
                {

                    [0] = 153, // algae
                    [1] = 153, // algae
                    [2] = 153, // algae
                    [3] = 153, // algae
                    [4] = 721, // snail 721
                    [5] = 716, // crayfish 716
                    [6] = 722, // periwinkle 722
                    [7] = 717, // crab 717

                };

            }

            int objectQuality = 0;

            int experienceGain;

            if (probability <= 3)
            {

                experienceGain = 6;

            }
            else
            {

                experienceGain = 12;

            }

            StardewDruid.Cast.Throw throwObject = new(objectIndexes[probability], objectQuality);

            throwObject.ThrowObject(targetPlayer, targetVector);

            targetPlayer.currentLocation.playSound("pullItemFromWater");

            castCost = 8;

            targetPlayer.gainExperience(1, experienceGain); // gain fishing experience

            castFire = true;

            castLimit = true;

            bool targetDirection = (targetPlayer.getTileLocation().X <= targetVector.X);

            ModUtility.AnimateSplash(targetLocation, targetVector, targetDirection);

        }

    }

}
