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
    public class Dwarf : StardewDruid.Character.Character
    {

        public Dwarf()
        {
        }

        public Dwarf(CharacterHandle.characters type)
          : base(type)
        {


        }

        public override void LoadOut()
        {

            base.LoadOut();

            setScale = 3.25f;

        }

    }

}