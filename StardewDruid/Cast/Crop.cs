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
    internal class Crop : CastHandle
    {

        public Crop(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            if (rite.caster.FarmingLevel >= 5)
            {

                castCost = 1;

            }

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

            //Dictionary<string, List<Vector2>> neighbourList = ModUtility.NeighbourCheck(targetLocation, targetVector);

            if (hoeDirt.fertilizer.Value == 0)
            {

                hoeDirt.plant(466, (int)targetVector.X, (int)targetVector.Y, targetPlayer, true, targetLocation);

                castFire = true;

            }

            StardewValley.Crop targetCrop = hoeDirt.crop;

            if(targetCrop.isWildSeedCrop() && targetCrop.currentPhase.Value <= 1 && (Game1.currentSeason != "winter" || targetLocation.isGreenhouse.Value))
            {

                bool enableQuality = (riteData.castTask.ContainsKey("masterCrop")) ? true : false;

                ModUtility.UpgradeCrop(hoeDirt, (int)targetVector.X, (int)targetVector.Y, targetPlayer, targetLocation, enableQuality);

                if(hoeDirt.crop == null)
                {
                    
                    return;

                }

                targetCrop = hoeDirt.crop;

                castFire = true;

                if (!riteData.castTask.ContainsKey("masterCrop"))
                {

                    mod.UpdateTask("lessonCrop", 1);

                }

            }

            if(targetCrop.currentPhase.Value <= 1)
            {

                targetCrop.currentPhase.Value = 2;

                hoeDirt.crop.dayOfCurrentPhase.Value = 0;

                hoeDirt.crop.updateDrawMath(targetVector);

                castFire = true;

            }

        }

    }

}
