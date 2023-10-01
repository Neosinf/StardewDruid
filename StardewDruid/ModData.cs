using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Reflection.PortableExecutable;

namespace StardewDruid
{

    class ModData
    {
        public List<string> buttonList { get; set; }

        public Dictionary<string, int> blessingList { get; set; }

        public Dictionary<string, bool> questList { get; set; }

        public bool startWithBlessing { get; set; }

        public ModData()
        {
            buttonList = new List<string>
            {
                "MouseX1",
                "MouseX2"
            };

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

            startWithBlessing = false;

        }

    }

}