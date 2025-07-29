using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{

    public class QueryData
    {

        public string name;

        public string value;

        public string description;

        public double time;

        public string location;

        public enum queries
        {
            RequestProgress,
            SyncProgress,
            QuestUpdate,
            EventDisplay,
            //EventDialogue,
            EventQuestion,
            ReceiveRelic,
            ThrowSword,
            SpellHandle,
            AccessHandle,
            AccessDoor,
            HaltCharacter,
            GimmeMoney,
            WarpFarmhands,
            RequestPreferences,
            PostPreferences,
            SyncPreferences,
            AddExport,
            AddPal,
            RequestGoods,
            SyncGoods,

        }

        public QueryData()
        {

        }

    }

}
