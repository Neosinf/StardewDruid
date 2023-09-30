using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
//using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast
{
    internal class Crop : Cast
    {

        public Crop(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {
            //castCost = 3;
        }

        public override void CastEarth()
        {

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.HoeDirt)
            {

                return;

            }

            StardewValley.TerrainFeatures.HoeDirt hoeDirt = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.HoeDirt;

            if (hoeDirt.crop == null)
            {

                return;

            }

            Dictionary<string, List<Vector2>> neighbourList = ModUtility.NeighbourCheck(targetLocation, targetVector);

            if (hoeDirt.fertilizer.Value == 0)
            {

                hoeDirt.plant(466, (int)targetVector.X, (int)targetVector.Y, targetPlayer, true, targetLocation);

                castFire = true;

            }

            StardewValley.Crop targetCrop = hoeDirt.crop;

            if(targetCrop.isWildSeedCrop() && targetCrop.currentPhase.Value <= 1)
            {

                ModUtility.UpgradeCrop(hoeDirt, (int)targetVector.X, (int)targetVector.Y, targetPlayer, targetLocation);

                targetCrop = hoeDirt.crop;

                castFire = true;

            }

            if(targetCrop.currentPhase.Value <= 1)
            {

                targetCrop.currentPhase.Value = 2;

                hoeDirt.crop.dayOfCurrentPhase.Value = 0;

                hoeDirt.crop.updateDrawMath(targetVector);

                castFire = true;

            }

            /*if(!targetCrop.fullyGrown.Value)
            {

                targetCrop.currentPhase.Value++;

                castCost = targetCrop.currentPhase.Value;

                hoeDirt.crop.dayOfCurrentPhase.Value = 0;

                hoeDirt.crop.updateDrawMath(targetVector);

                castFire = true;

            }*/

        }

    }

}
