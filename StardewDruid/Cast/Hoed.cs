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

        public Hoed(Mod mod, Vector2 target, Farmer player)
            : base(mod, target, player)
        {

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

            int probability = randomIndex.Next(mod.SpecialLimit());

            if (probability == 0)
            {

                if (hoeDirt.crop != null)
                {

                    if (hoeDirt.fertilizer.Value == 0)
                    {

                        hoeDirt.plant(466, (int)targetVector.X, (int)targetVector.Y, Game1.player, true, Game1.player.currentLocation);

                        castFire = true;

                    }

                }
                else
                {

                    //if (ModUtility.CheckSeed(targetLocation, targetVector))
                    //{

                    int gradeSeed = 5;

                    if(probability == 2)
                    { 
                            
                        gradeSeed = randomIndex.Next(5); 
                        
                    }

                    ModUtility.PlantSeed(targetLocation, targetPlayer, targetVector, gradeSeed);

                    castCost = 4;

                    castFire = true;

                    mod.SpecialIncrement();

                    //}

                }

            }

        }

    
    }

}
