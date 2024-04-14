using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Event;

using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace StardewDruid.Monster.Boss
{
    public class Rogue : Shadowtin
    {

        public Rogue()
        {
        }

        public Rogue(Vector2 vector, int CombatModifier)
          : base(vector, CombatModifier, "Rogue")
        {

        }

    }

}
