﻿using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Locations;

namespace StardewDruid.Map
{
    static class FireData
    {

        //public static Vector2 FirePoints(GameLocation location)
        public static Vector2 FireVectors(GameLocation location)
        {

            /*Dictionary<string, Vector2> firePoints = new()
             {
                 ["Mountain"] = new Vector2(29,9),
                 ["Beach"] = new Vector2(48,20),
                 ["Forest"] = new Vector2(47, 97),
             };

             return firePoints;*/

            if (location is Mountain)
            {

                return new Vector2(29, 9);

            }
            else if (location is Beach)
            {

                return new Vector2(48, 20);

            }
            else if (location is Forest)
            {

                return new Vector2(47, 97);

            }

            return Vector2.One;

        }

    }
}
