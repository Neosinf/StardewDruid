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

        private readonly HoeDirt hoeDirt;

        public Hoed(Mod mod, Vector2 target, Farmer player, HoeDirt HoeDirtFeature)
            : base(mod, target, player)
        {

            hoeDirt = HoeDirtFeature;

        }

        public override void CastEarth()
        {

            int probability = randomIndex.Next(10);

            if (probability <= 2) // 3/10 fertiliser / new random seed
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

                    if (ModUtility.CheckSeed(targetLocation, targetVector))
                    {

                        int generateItem;

                        if (probability <= 1) // 2/3 low grade random seed
                        {

                            generateItem = 770;

                        }
                        else
                        {

                            Dictionary<int, int> objectIndexes;

                            switch (Game1.currentSeason)
                            {

                                case "spring":

                                    objectIndexes = new()
                                    {
                                        [0] = 478, // rhubarb
                                        [1] = 476, // garlic
                                        [2] = 433, // coffee
                                        [3] = 745, // strawberry
                                        [4] = 473, // bean
                                    };

                                    break;

                                case "summer":

                                    objectIndexes = new()
                                    {
                                        [0] = 479, // melon
                                        [1] = 485, // red cabbage
                                        [2] = 433, // coffee
                                        [3] = 481, // blueberry
                                        [4] = 301 // hops
                                     };


                                    break;

                                default: // "fall":

                                    objectIndexes = new()
                                    {
                                        [0] = 490, // pumpkin
                                        [1] = 492, // yam
                                        [2] = 299, // amaranth
                                        [3] = 493, // cranberry
                                        [4] = 302 // grape
                                    };

                                    break;

                            }

                            generateItem = objectIndexes[randomIndex.Next(5)];

                        }

                        hoeDirt.state.Value = 1;

                        hoeDirt.plant(generateItem, (int)targetVector.X, (int)targetVector.Y, targetPlayer, false, targetLocation); // high grade seed

                        hoeDirt.crop.currentPhase.Value++;

                        hoeDirt.crop.currentPhase.Value++;

                        //hoeDirt.crop.currentPhase.Value++; // growth stage 3

                        hoeDirt.crop.dayOfCurrentPhase.Value = 0;

                        hoeDirt.crop.updateDrawMath(targetVector);

                        hoeDirt.plant(920, (int)targetVector.X, (int)targetVector.Y, targetPlayer, true, targetLocation); // always watered

                        castCost = 4;

                        castFire = true;

                    }

                }

            }

        }

    
    }

}
