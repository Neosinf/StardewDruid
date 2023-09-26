using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;
using System.ComponentModel.Design;

namespace StardewDruid.Cast
{
    internal class Grass : Cast
    {

        public bool activateSeed;

        public Grass(Mod mod, Vector2 target, Farmer player)
            : base(mod, target, player)
        {

            activateSeed = false;

        }

        public override void CastEarth()
        {

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.Grass)
            {

                return;

            }

            StardewValley.TerrainFeatures.Grass grassFeature = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.Grass;

            int probability = randomIndex.Next(20);

            /*if(probability <= 1 && activateSeed)
            {

                if (ModUtility.CheckSeed(targetLocation, targetVector))
                {

                    targetLocation.terrainFeatures.Remove(targetVector);

                    targetLocation.makeHoeDirt(targetVector);

                    int gradeSeed = 5;

                    if (probability == 0)
                    {

                        gradeSeed = randomIndex.Next(5);

                    }

                    ModUtility.PlantSeed(targetLocation, targetPlayer, targetVector, gradeSeed);

                    castCost = 4;

                    castFire = true;

                }

            }
            else if (probability <= 4) // 3 / 20 fibre*/
            if (probability <= 4)
            {

                if (randomIndex.Next(100) == 0) // 1:500 chance
                {

                    Game1.createObjectDebris(114, (int)targetVector.X, (int)targetVector.Y);

                    castCost = 0;

                    castFire = true;

                }

                Game1.createObjectDebris(771, (int)targetVector.X, (int)targetVector.Y);

                Game1.createObjectDebris(771, (int)targetVector.X, (int)targetVector.Y);

                if(probability == 4)
                {
                    
                    Game1.createObjectDebris(771, (int)targetVector.X, (int)targetVector.Y);

                }

                castCost = 0;

                castFire = true;

                targetPlayer.gainExperience(2, 2); // gain foraging experience

            }


            Rectangle tileRectangle = new((int)targetVector.X * 64 + 1, (int)targetVector.Y * 64 + 1, 62, 62);

            grassFeature.doCollisionAction(tileRectangle,2,targetVector,null,Game1.currentLocation);

        }

    }
}
