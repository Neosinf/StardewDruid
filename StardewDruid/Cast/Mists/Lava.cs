using Microsoft.Xna.Framework;
using StardewValley.Locations;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast.Mists
{
    internal class Lava : CastHandle
    {

        public Lava(Vector2 target)
            : base(target)
        {

        }

        public override void CastEffect()
        {

            VolcanoDungeon volcanoLocation = targetLocation as VolcanoDungeon;

            int waterRadius = Math.Min(5, Mod.instance.PowerLevel);

            for (int i = 0; i < waterRadius + 1; i++)
            {

                List<Vector2> radialVectors = ModUtility.GetTilesWithinRadius(targetLocation, targetVector, i);

                foreach (Vector2 radialVector in radialVectors)
                {
                    int tileX = (int)radialVector.X;
                    int tileY = (int)radialVector.Y;

                    if (volcanoLocation.waterTiles[tileX, tileY] && !volcanoLocation.cooledLavaTiles.ContainsKey(radialVector))
                    {

                        volcanoLocation.CoolLava(tileX, tileY);

                        volcanoLocation.UpdateLavaNeighbor(tileX, tileY);

                    }

                }

            }

            List<Vector2> fourthVectors = ModUtility.GetTilesWithinRadius(targetLocation, targetVector, waterRadius + 1);

            foreach (Vector2 fourthVector in fourthVectors)
            {
                int tileX = (int)fourthVector.X;
                int tileY = (int)fourthVector.Y;

                volcanoLocation.UpdateLavaNeighbor(tileX, tileY);

            }

            castFire = true;

            castCost = 0;

            ModUtility.AnimateBolt(targetLocation, targetVector * 64 + new Vector2(32));

            return;

        }

    }
}
