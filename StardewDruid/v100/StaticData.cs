﻿using StardewValley;
using System.Collections.Generic;

namespace StardewDruid.v100
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

        public int potentialProgress;

        public StaticData()
        {

            staticVersion = 100;

            staticId = Game1.player.UniqueMultiplayerID;

            activeBlessing = "none";

            questList = new();

            weaponAttunement = new();

            taskList = new();

            characterList = new();

            setProgress = -1;

            activeProgress = 0;

            potentialProgress = 0;

        }

    }

    class MultiplayerData
    {

        public Dictionary<long, StaticData> farmhandData;

        public MultiplayerData()
        {

            farmhandData = new Dictionary<long, StaticData>();

        }

    }

}
