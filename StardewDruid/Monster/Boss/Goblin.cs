using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;

namespace StardewDruid.Monster.Boss
{
    public class Goblin : Shadowtin
    {

        public Goblin(Vector2 vector, int CombatModifier)
          : base(vector, CombatModifier /= 2, "Goblin")
        {

        }

    }
}
