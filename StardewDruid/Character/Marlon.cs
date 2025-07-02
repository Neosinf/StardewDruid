using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Location.Druid;
using StardewDruid.Render;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;

namespace StardewDruid.Character
{
    public class Marlon : StardewDruid.Character.Character
    {

        public Marlon()
        {
        }

        public Marlon(CharacterHandle.characters type)
          : base(type)
        {


        }

        public override void LoadOut()
        {

            base.LoadOut();

            WeaponLoadout();

            idleFrames[idles.standby] = new()
            {
                [0] = new List<Rectangle> { new Rectangle(192, 0, 32, 32), },
            };

            restSet = true;

            idleFrames[idles.rest] = new()
            {
                [0] = new() { new(0, 64, 32, 32), },

            };

        }

    }

}