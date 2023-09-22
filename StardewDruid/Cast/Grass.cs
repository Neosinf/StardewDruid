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

        private readonly StardewValley.TerrainFeatures.Grass grassFeature;

        public Grass(Mod mod, Vector2 target, Farmer player, StardewValley.TerrainFeatures.Grass GrassFeature)
            : base(mod, target, player)
        {

            grassFeature = GrassFeature;

        }

        public override void CastEarth()
        {

            int probability = randomIndex.Next(20);
            /*
            if(probability == 0)
            {

                switch (Game1.currentSeason)
                {

                    case "spring":

                        Game1.createObjectDebris(495, (int)targetVector.X, (int)targetVector.Y);

                        break;

                    case "summer":

                        Game1.createObjectDebris(496, (int)targetVector.X, (int)targetVector.Y);

                        break;

                    case "fall":

                        Game1.createObjectDebris(497, (int)targetVector.X, (int)targetVector.Y);

                        break;

                    default: // "winter":

                        Game1.createObjectDebris(498, (int)targetVector.X, (int)targetVector.Y);

                        break;

                }

                castFire = true;

            }
            else*/ 
            if (probability == 1)
            {
                
                if (randomIndex.Next(10) == 0) // 1:500 chance
                {

                    Game1.createObjectDebris(114, (int)targetVector.X, (int)targetVector.Y);

                    castCost = 0;

                    castFire = true;

                }

            }
            else if (probability <= 5) // 4 / 20 fibre
            {
                
                Game1.createObjectDebris(771, (int)targetVector.X, (int)targetVector.Y);

                Game1.createObjectDebris(771, (int)targetVector.X, (int)targetVector.Y);

                Game1.createObjectDebris(771, (int)targetVector.X, (int)targetVector.Y);

                castCost = 0;

                castFire = true;

            }


            Rectangle tileRectangle = new((int)targetVector.X * 64 + 1, (int)targetVector.Y * 64 + 1, 62, 62);

            grassFeature.doCollisionAction(tileRectangle,2,targetVector,null,Game1.currentLocation);

        }

    }
}
