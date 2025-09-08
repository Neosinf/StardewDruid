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
            FarmhandRequestsProgress,
            HostProvidesProgress,
            QuestUpdate,
            EventDisplay,
            EventQuestion,
            ReceiveRelic,
            ThrowSword,
            SpellHandle,
            AccessHandle,
            AccessDoor,
            HaltCharacter,
            GimmeMoney,
            WarpFarmhands,
            FarmhandRequestsData,
            FarmhandProvidesSave,
            HostProvidesData,

        }

        public QueryData()
        {

        }

    }

}
