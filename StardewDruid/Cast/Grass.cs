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

        public Grass(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            castCost = 0;

        }

        public override void CastEarth()
        {

            int probability = randomIndex.Next(50);

            if (probability >= 5)
            {
                return;

            }

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.Grass)
            {

                return;

            }

            StardewValley.TerrainFeatures.Grass grassFeature = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.Grass;

            int tileX = (int)targetVector.X;

            int tileY = (int)targetVector.Y;

            if (randomIndex.Next(100) == 0) // 1:1000 chance
            {

                Game1.createObjectDebris(114, tileX, tileY);

            }

            if(probability <= 1)
            {

                switch (Game1.currentSeason)
                {

                    case "spring":

                        Game1.createObjectDebris(495, tileX, tileY);

                        break;

                    case "summer":

                        Game1.createObjectDebris(496, tileX, tileY);

                        break;

                    case "fall":

                        Game1.createObjectDebris(497, tileX, tileY);

                        break;

                    default:

                        break;

                }

            }  
            else if (probability == 2)
            {

                switch (Game1.currentSeason)
                {

                    case "spring":

                        Game1.createObjectDebris(477, tileX, tileY);

                        break;

                    case "summer":

                        Game1.createObjectDebris(483, tileX, tileY);

                        break;

                    case "fall":

                        Game1.createObjectDebris(299, tileX, tileY);

                        break;

                    default:

                        break;

                }

            }
            else
            {

                for (int i = 2; i < probability; i++)
                {
                    Game1.createObjectDebris(771, tileX, tileY);

                }

            }

            Rectangle tileRectangle = new(tileX * 64 + 1, tileY * 64 + 1, 62, 62);

            grassFeature.doCollisionAction(tileRectangle,2,targetVector,null,Game1.currentLocation);

        }

    }
}
