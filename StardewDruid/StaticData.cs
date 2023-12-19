using StardewValley;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace StardewDruid
{
    class StaticData
    {

        public int staticVersion;

        public long staticId;

        public string activeBlessing;

        public Dictionary<string, bool> questList;

        public Dictionary<int, string> weaponAttunement;

        public Dictionary<string, int> taskList;

        public Dictionary<string, string> characterList;

        public int setProgress;

        public int activeProgress;

        public StaticData()
        {

            staticId = Game1.player.UniqueMultiplayerID;

            activeBlessing = "none";

            questList = new();

            weaponAttunement = new();

            taskList = new();

            characterList = new();

            setProgress = -1;

            activeProgress = 0;

        }

    }

}
