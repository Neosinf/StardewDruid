using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace StardewDruid
{
    internal class ActiveData
    {

        public string activeBlessing = "none";

        public int toolIndex = -1;

        public bool activeCharge = false;

        public string activeKey = null;

        public int chargeAmount = 0;

        public int castLevel = 0;

        public int cycleLevel = 1;

        public List<Type> castLimits = new();

        public bool castInterrupt = false;

        public bool castDoppler = false;

        public int activeDirection = -1;

        public Vector2 originVector = new(0,0);

        public Vector2 activeVector = new(0,0);

        public Dictionary<string, bool> spawnIndex = new();

    }

}
