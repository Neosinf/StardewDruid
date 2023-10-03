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

        public Bush(Mod mod, Vector2 target, Rite rite, LargeTerrainFeature LargeTerrainFeature)
            : base(mod, target, rite)
        {

            bushFeature = LargeTerrainFeature;

        }

        public override void CastEarth()
        {

            int probability = randomIndex.Next(20);

            if (probability >= 3) // nothing
            {

                bushFeature.performToolAction(null, 1, targetVector, null);

                return;

            }

            if (probability == 2)
            {
                if (riteData.spawnIndex["critter"])
                {

                    Portal critterPortal = new(mod, targetPlayer.getTileLocation(), riteData);

                    critterPortal.spawnFrequency = 1;

                    critterPortal.specialType = 5;

                    critterPortal.baseType = "terrain";

                    critterPortal.baseVector = targetVector;

                    critterPortal.baseTarget = true;

                    critterPortal.CastTrigger();

                }

                bushFeature.performToolAction(null, 1, targetVector, null);

                return;

            }

            int objectIndex;

            if (probability == 1)
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
                    [1] = 257, // 257 morel
                    [2] = 281, // 281 chanterelle
                    [3] = 404, // 404 mushroom
                    [4] = 404, // 404 mushroom

                };

                objectIndex = objectIndexes[randomIndex.Next(5)];

            }

            int randomQuality = randomIndex.Next(11 - targetPlayer.foragingLevel.Value);

            int objectQuality = 0;

            if (randomQuality == 0)
            {

                objectQuality = 2;

            }

            if (targetPlayer.professions.Contains(16))
            {

                objectQuality = 3;

            }

            int throwAmount = 1;

            if (targetPlayer.professions.Contains(13))
            {

                throwAmount = randomIndex.Next(1, 3);

            }

            for(int i = 0;i<throwAmount;i++) { 

                Throw throwObject = new(objectIndex, objectQuality);

                throwObject.ThrowObject(targetPlayer, targetVector);

            };

            castFire = true;

            ModUtility.AnimateGrowth(targetLocation, targetVector);

            targetPlayer.gainExperience(2, 2); // gain foraging experience

            bushFeature.performToolAction(null, 1, targetVector, null);

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

        }

    }
}
