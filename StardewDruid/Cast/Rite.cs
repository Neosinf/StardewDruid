using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Cast
{
    internal class Rite
    {

        public Dictionary<string, bool> spawnIndex;

        public string castType;

        public int castLevel;

        public Vector2 castVector;

        public StardewValley.Farmer caster;

        public StardewValley.GameLocation castLocation;

        public int direction;

        public Rite()
        {

            castLevel = 1;

            castType = "CastEarth";

            caster = Game1.player;

            castLocation = caster.currentLocation;

            castVector = caster.getTileLocation();

            spawnIndex = Map.Spawn.SpawnIndex(castLocation);

            direction = 0;

        }

    }

}
