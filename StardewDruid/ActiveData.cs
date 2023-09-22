using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid
{
    internal class ActiveData
    {

        public bool activeCharge = false;

        public string activeKey = null;

        public int chargeAmount = 0;

        public int castLevel = 0;

        public int animateLevel = 0;

        public int chargeLevel = 0;

        public bool castComplete = false;

        public int activeDirection = -1;

        public List<Microsoft.Xna.Framework.Vector2> removeVectors = new(); // keep track of tiles under clumps

        public List<Microsoft.Xna.Framework.Vector2> servedVectors = new();

        public Dictionary<string, bool> spawnIndex = new();

    }

}
