using Microsoft.Xna.Framework;
using StardewDruid.Cast.Earth;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using static StardewValley.Menus.CharacterCustomization;

namespace StardewDruid.Cast.Fates
{
    internal class Whisk : CastHandle
    {

        Vector2 destination;

        public Whisk(Vector2 target, Rite rite, Vector2 Destination)
            : base(target, rite)
        {

            destination = Destination;

        }

        public override void CastEffect()
        {
            Event.World.Whisk whiskEvent = new(targetVector, riteData, destination);

            whiskEvent.EventTrigger();

            castFire = true;

        }

    }

}
