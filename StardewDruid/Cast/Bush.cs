using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast
{
    internal class Bush : Cast
    {

        private readonly LargeTerrainFeature bushFeature;

        public Bush(Mod mod, Vector2 target, Farmer player, LargeTerrainFeature LargeTerrainFeature)
            : base(mod, target, player)
        {

            bushFeature = LargeTerrainFeature;

        }

        public override void CastEarth()
        {

            int probability = randomIndex.Next(10);

            if (probability <= 4) // nothing
            {


            }
            else if (probability <= 7) // effects
            {   
                
                if (Game1.currentSeason == "summer")
                {

                    Game1.currentLocation.critters.Add(new Firefly(targetVector + new Vector2(randomIndex.Next(-2, 3), randomIndex.Next(-2, 3))));

                    Game1.currentLocation.critters.Add(new Firefly(targetVector + new Vector2(randomIndex.Next(-2, 3), randomIndex.Next(-2, 3))));

                }
                else
                {

                    Game1.currentLocation.critters.Add(new Butterfly(targetVector + new Vector2(randomIndex.Next(-2, 3), randomIndex.Next(-2, 3)), false));

                    Game1.currentLocation.critters.Add(new Butterfly(targetVector + new Vector2(randomIndex.Next(-2, 3), randomIndex.Next(-2, 3)), false));

                }

                castFire = true;

            }
            else
            {

                int objectIndex;

                if (probability == 8)
                {

                    switch (Game1.currentSeason)
                    {

                        case "spring":

                            objectIndex = 296; // salmonberry

                            break;

                        case "summer":

                            objectIndex = 398; // grape

                            break;

                        case "fall":

                            objectIndex = 410; // blackberry

                            break;

                        default:

                            objectIndex = 414; // crystal fruit

                            break;

                    }

                }
                else
                {

                    Dictionary<int, int> objectIndexes = new()
                    {
                        [0] = 257, // 257 morel
                        [1] = 281, // 281 chanterelle
                        [2] = 404, // 404 mushroom
                        //[3] = 420,  // 420 red mushroom
                        //[4] = 422  // 421 purple mushroom

                    };

                    //objectIndex = objectIndexes[randomIndex.Next(5)];
                    objectIndex = objectIndexes[randomIndex.Next(3)];

                }


                int randomQuality = randomIndex.Next(11 - targetPlayer.foragingLevel.Value);

                int objectQuality = 0;

                if (randomQuality == 0)
                {
                    
                    objectQuality = 2;

                }

                Throw throwObject = new(objectIndex, objectQuality);

                throwObject.ThrowObject(targetPlayer, targetVector);

                castFire = true;

                ModUtility.AnimateGrowth(targetLocation, targetVector);

                targetPlayer.gainExperience(2, 2); // gain foraging experience

            }

            bushFeature.performToolAction(null, 1, targetVector, null);

        }

    }
}
