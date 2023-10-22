﻿using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast
{
    internal class Trough : CastHandle
    {

        public Trough (Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

        }

        public override void CastEarth()
        {
            if (targetLocation.objects.ContainsKey(targetVector))
            {
                return;
            }

            targetLocation.objects.Add(targetVector, new StardewValley.Object(178, 1));

            return;

        }

    }

}
