using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast.Mists
{
    internal class Water : CastHandle
    {

        public Water(Vector2 target)
            : base(target)
        {


        }

        public override void CastEffect()
        {

            castCost = Math.Max(8, 32 - (targetPlayer.FishingLevel * 3));

            Fishspot fishspotEvent = new(targetVector);

            fishspotEvent.EventTrigger();

            castFire = true;

            return;

        }

    }

}
