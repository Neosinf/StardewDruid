using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Fates;
using StardewDruid.Cast.Weald;
using StardewDruid.Dialogue;
using StardewDruid.Event;

using StardewModdingAPI;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Monsters;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Buffin : Critter
    {

        public Buffin()
        {
        }

        public Buffin(CharacterHandle.characters type = CharacterHandle.characters.Buffin)
          : base(type)
        {

        }

        public override void LoadOut()
        { 
        
            base.LoadOut();

            idleFrames[idles.standby] = new()
            {
                [0] = new() { new(0, 256, 32, 32), new(32, 256, 32, 32), },
            };

        }

    }

}
