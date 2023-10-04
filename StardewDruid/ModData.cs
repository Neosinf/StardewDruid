using System.Collections.Generic;
using StardewModdingAPI;
using Microsoft.Xna.Framework;
using System.Reflection.PortableExecutable;
using StardewModdingAPI.Utilities;

namespace StardewDruid
{

    class ModData
    {

        public StardewModdingAPI.Utilities.KeybindList riteButtons { get; set; }

        public Dictionary<string, int> blessingList { get; set; }

        public Dictionary<string, bool> questList { get; set; }

        public bool masterStart { get; set; }

        public bool castBuffs { get; set; }

        public int farmCaveStatueX { get; set; }

        public int farmCaveStatueY { get; set; }

        public int farmCaveActionX { get; set; }

        public int farmCaveActionY { get; set; }

        public bool farmCaveHideStatue { get; set; }

        public bool farmCaveMakeSpace { get; set; }

        public ModData()
        {

            riteButtons = KeybindList.Parse("MouseX1,MouseX2,LeftShoulder");

            questList = new()
            {
                ["approachEffigy"] = false,
                ["swordEarth"] = false,
                ["challengeEarth"] = false,
                ["swordWater"] = false,
                ["challengeWater"] = false,
                ["swordStars"] = false,
                ["challengeStars"] = false,

            };

            blessingList = new()
            {
                ["earth"] = 5,
                ["water"] = 5,
                ["stars"] = 1,
            };

            masterStart = false;

            castBuffs = true;

            farmCaveActionX = 6;

            farmCaveActionY = 4;

            farmCaveStatueX = 6;

            farmCaveStatueY = 3;

            farmCaveHideStatue = false;

            farmCaveMakeSpace = true;

        }

    }

}