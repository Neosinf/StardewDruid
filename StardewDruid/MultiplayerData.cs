using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid
{
    class MultiplayerData
    {

        public Dictionary<long, StaticData> farmhandData;

        public MultiplayerData()
        {

            farmhandData = new Dictionary<long, StaticData>();

        }

    }

}
