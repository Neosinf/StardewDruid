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
    internal class Hoed : Cast
    {

        public Hoed(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

        }

        public override void CastEarth()
        {

            mod.UpdateEarthCasts(targetLocation, targetVector, false);

            /*if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.HoeDirt)
            {

                return;

            }

            StardewValley.TerrainFeatures.HoeDirt hoeDirt = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.HoeDirt;

            Dictionary<string, List<Vector2>> neighbourList = ModUtility.NeighbourCheck(targetLocation, targetVector);

            if (neighbourList.ContainsKey("Tree") || neighbourList.ContainsKey("Sapling") && hoeDirt.crop == null)
            {

                targetLocation.terrainFeatures.Remove(targetVector);

            }

            mod.UpdateEarthCasts(targetLocation, targetVector, false);

            if (hoeDirt.crop != null)
            {

                if (hoeDirt.fertilizer.Value == 0)
                {

                    hoeDirt.plant(466, (int)targetVector.X, (int)targetVector.Y, Game1.player, true, Game1.player.currentLocation);

                    castFire = true;

                }

                return;

            }

            if (!neighbourList.ContainsKey("Crop"))
            {
                
                return;

            }

            int probability = randomIndex.Next(mod.SpecialLimit());

            if (probability == 0)
            {
                int gradeSeed = 5;

                if(randomIndex.Next(3) == 2)
                { 
                            
                    gradeSeed = randomIndex.Next(5); 
                        
                }

                ModUtility.PlantSeed(targetLocation, targetPlayer, targetVector, gradeSeed);

                castCost = 4;

                castFire = true;

                mod.SpecialIncrement();
                
            }*/

        }

    }

}
