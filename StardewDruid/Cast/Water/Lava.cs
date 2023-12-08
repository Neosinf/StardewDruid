﻿using Force.DeepCloner;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Minigames;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast.Water
{
    internal class Lava : CastHandle
    {

        public Lava(Vector2 target, Rite rite)
            : base(target, rite)
        {

        }

        public override void CastEffect()
        {

            VolcanoDungeon volcanoLocation = targetLocation as VolcanoDungeon;

            int waterRadius = Math.Max(2, Mod.instance.virtualCan.UpgradeLevel / 2);

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

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

    }
}