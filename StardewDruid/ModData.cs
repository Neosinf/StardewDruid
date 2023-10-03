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

        }

    }

}