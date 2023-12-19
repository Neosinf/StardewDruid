using Microsoft.Xna.Framework;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast.Mists
{
    internal class Water : CastHandle
    {

        public Water(Vector2 target, Rite rite)
            : base(target, rite)
        {
            
            castCost = 8;

            if (rite.caster.FishingLevel >= 6)
            {

                castCost = 4;

            }

        }

        public override void CastEffect() {

            castCost = Math.Max(8, 48 - (targetPlayer.FishingLevel * 3));

            Event.World.Fishspot fishspotEvent = new(targetVector, riteData);

            fishspotEvent.EventTrigger();

            castFire = true;

            return;

        }

    }

}
