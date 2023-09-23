using System.Collections.Generic;

namespace StardewDruid
{
    class StaticData
    {

        public string activeBlessing;

        public Dictionary<string, List<string>> triggerList;

        public Dictionary<string, bool> questList;

        public Dictionary<string, int> blessingList;

        public int defaultMagnetism;

        public StaticData()
        {

            activeBlessing = "none";

            triggerList = new();

            questList = new();

            blessingList = new();

            defaultMagnetism = 1;

        }

    }

}
