using System.Collections.Generic;

namespace StardewDruid
{
    class StaticData
    {

        public string statueChoice;

        public Dictionary<string, bool> challengeList;

        public Dictionary<string, List<string>> triggerList;

        public Dictionary<string, bool> questList;

        public List<string> blessingsReceived;

        public List<string> challengesReceived;

        public StaticData()
        {

            statueChoice = "none";

            challengeList = new();

            triggerList = new();

            questList = new();

            blessingsReceived = new();

            challengesReceived = new();

        }

    }

}
