using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Reflection.PortableExecutable;

namespace StardewDruid
{

    class ModData
    {
        public List<string> buttonList { get; set; }

        public List<string> craftList { get; set; }

        public ModData()
        {
            buttonList = new List<string>
            {
                "MouseX1",
                "MouseX2"
            };

            craftList = new List<string>
            {
                "Keg",
                "Furnace",
                "Preserves Jar",
                "Recycling Machine",
                "Deconstructor",
                "Bone Mill",
                "Cheese Press",
                "Mayonnaise Machine",
                "Loom",
                "Oil Maker",
            };

        }

    }

}