using Microsoft.Xna.Framework;
using StardewDruid.Map;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast
{
    internal class Portal : CastHandle
    {

        public Portal (Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            castCost = 0;

        }

        public override void CastWater()
        {

            Event.Portal portalEvent = new(mod, targetVector, riteData, new Quest());

            portalEvent.EventTrigger();

            castLimit = true;

            castFire = true;

        }

    }

}
