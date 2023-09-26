using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Map
{
    internal class QuestData
    {

        public string name;

        public string triggerCast;

        public string triggerType;

        public string triggerLocation;

        public int questId;

        public int questValue;

        public string questTitle;

        public string questDescription;

        public string questObjective;

        public int questReward;

        public bool updateEffigy;

        public Vector2 triggerVector;

        public Vector2 triggerLimit;

        public int startTime;

        //public int endTime;

        public Vector2 challengeWithin;

        public Vector2 challengeRange;

        public List<Vector2> challengePortals;

        public int challengeSeconds;

        public int challengeFrequency;

    }
}
