using StardewValley;
using System.Collections.Generic;

namespace StardewDruid
{
    class StaticData
    {

        public int staticVersion;

        public long staticId;

        public string activeBlessing;

        //public List<string> triggerList;

        public Dictionary<string, bool> questList;

        public Dictionary<string, int> blessingList;

        public Dictionary<int, string> weaponAttunement;

        public Dictionary<string, int> taskList;

        public Dictionary<string, int> toggleList;

        public StaticData()
        {

            staticId = Game1.player.UniqueMultiplayerID;

            activeBlessing = "none";

            //triggerList = new();

            questList = new();

            blessingList = new();

            weaponAttunement = new();

            taskList = new();

            toggleList = new();

        }

    }

}
