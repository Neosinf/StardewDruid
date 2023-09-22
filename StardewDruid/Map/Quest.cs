using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;
using StardewValley.Quests;

namespace StardewDruid.Map
{
    static class Quest
    {

        public static bool CurrentQuest(Farmer targetPlayer, StaticData staticData, string questRequest)
        {

            switch (questRequest)
            {

                case "challengeEarth":

                    if (!staticData.challengesReceived.Contains("challengeEarth"))
                    {
                        return false;

                    }

                    break;

                case "swordEarth":

                    if (!staticData.blessingsReceived.Contains("earth"))
                    {
                        return false;

                    }

                    break;

                case "challengeWater":

                    if (!staticData.challengesReceived.Contains("challengeWater"))
                    {
                        return false;

                    }

                    break;

                case "swordWater":

                    if (!staticData.blessingsReceived.Contains("water"))
                    {
                        return false;

                    }

                    break;

                case "challengeStars":

                    if (!staticData.challengesReceived.Contains("challengeStars"))
                    {
                        return false;

                    }

                    break;

                case "swordStars":

                    if (!staticData.blessingsReceived.Contains("stars"))
                    {
                        return false;

                    }

                    break;

                default:

                    break;

            }

            return true;

        }

        public static Dictionary<string, int> QuestList()
        {
            Dictionary<string, int> index = new()
            {
                ["approachStatue"] = 280729871,
                ["challengeEarth"] = 280719872,
                ["challengeWater"] = 280719873,
                ["challengeStars"] = 280719874,
            };

            return index;

        }

        public static int QuestId(string questRequest)
        {
            Dictionary<string, int> index = QuestList();

            return index[questRequest];

        }

        public static StardewValley.Quests.Quest GenerateQuest(string questRequest)
        {

            StardewValley.Quests.Quest newQuest;

            switch (questRequest)
            {

                case "challengeEarth":

                    newQuest = new();

                    newQuest.questType.Value = 6;
                    newQuest.id.Value = QuestId(questRequest);
                    newQuest.questTitle = "The Polluted Aquifier";
                    newQuest.questDescription = "The Effigy believes the mountain spring has been polluted by rubbish dumped in the abandoned mineshafts.";
                    newQuest.currentObjective = "Perform a rite of the earth over the aquifier in level 20 of the mines.";
                    newQuest.showNew.Value = true;
                    newQuest.moneyReward.Value = 1500;
                    newQuest.rewardDescription.Value = "The mountain residents are grateful I cleaned up the trash.";

                    break;

                case "challengeWater":

                    newQuest = new();

                    newQuest.questType.Value = 6;
                    newQuest.id.Value = QuestId(questRequest);
                    newQuest.questTitle = "The Shadow Invasion";
                    newQuest.questDescription = "The Effigy has heard the whispers of shadowy figures that loiter in the dark spaces of the village.";
                    newQuest.currentObjective = "Perform a rite of the water in Pelican Town's graveyard after dark.";
                    newQuest.showNew.Value = true;
                    newQuest.moneyReward.Value = 3000;
                    newQuest.rewardDescription.Value = "The town residents are grateful I settled the graveyard.";

                    break;

                case "challengeStars":

                    newQuest = new();

                    newQuest.questType.Value = 6;
                    newQuest.id.Value = QuestId(questRequest);
                    newQuest.questTitle = "The Slime Infestation";
                    newQuest.questDescription = "The Effigy notes that many of the trees in the local forest have been marred with a slimy substance.";
                    newQuest.currentObjective = "Perform a rite of the stars in the clearing east of arrowhead island in Cindersap Forest.";
                    newQuest.showNew.Value = true;
                    newQuest.moneyReward.Value = 5000;
                    newQuest.rewardDescription.Value = "The forest residents are grateful I cleared out the slimes.";

                    break;

                default: // approachStatue

                    newQuest = new();

                    newQuest.questType.Value = 6;
                    newQuest.id.Value = QuestId(questRequest);
                    newQuest.questTitle = "The Druid's Effigy";
                    newQuest.questDescription = "Grandpa told me that the first farmers of the valley practiced an ancient druidic tradition.";
                    newQuest.currentObjective = "Investigate the statue in the old farm cave.";
                    newQuest.showNew.Value = true;
                    newQuest.rewardDescription.Value = null; //"I received a blessing from the Effigy, but I need a weapon to wield it.";

                    break;

            }

            return newQuest;

        }

        public static Dictionary<string, string> ChallengeList()
        {
            Dictionary<string, string> index = new()
            {

                ["swordEarth"] = "CastEarth",

                ["swordWater"] = "CastWater",

                ["swordStars"] = "CastStars",

                ["challengeEarth"] = "CastEarth",

                ["challengeWater"] = "CastWater",

                ["challengeStars"] = "CastStars",

            };

            return index;

        }

        public static Vector2 ChallengeVector(string effect)
        {

            Dictionary<string, Vector2> vectorIndex = new()
            {
                ["challengeEarth"] = new(24, 13),
                ["challengeWater"] = new(45, 87),
                ["challengeStars"] = new(77, 68),
                ["swordEarth"] = new(38, 9),
                ["swordWater"] = new(82, 38),
                ["swordStars"] = new(24, 13),
            };

            return vectorIndex[effect];

        }

        public static Vector2 ChallengeLimit(string effect)
        {

            Dictionary<string, Vector2> vectorIndex = new()
            {
                ["challengeEarth"] = new(3, 3),
                ["challengeWater"] = new(5, 5),
                ["challengeStars"] = new(5, 5),
                ["swordEarth"] = new(3, 4),
                ["swordWater"] = new(9, 3),
                ["swordStars"] = new(10, 10),
            };

            return vectorIndex[effect];

        }

        public static Vector2 ChallengeWithin(string effect)
        {

            Dictionary<string, Vector2> vectorIndex = new()
            {
                ["challengeEarth"] = new(21, 10),
                ["challengeWater"] = new(42, 84),
                ["challengeStars"] = new(74, 65),
            };

            return vectorIndex[effect];

        }

        public static Vector2 ChallengeRange(string effect)
        {

            Dictionary<string, Vector2> vectorIndex = new()
            {
                ["challengeEarth"] = new(9, 9),
                ["challengeWater"] = new(10, 8),
                ["challengeStars"] = new(14, 14),
            };

            return vectorIndex[effect];

        }

        public static List<Vector2> ChallengePortals(string effect)
        {

            Dictionary<string, List<Vector2>> vectorIndex = new()
            {
                ["challengeEarth"] = new() {

                    new(23, 14),

                },
                ["challengeWater"] = new()
                {

                    new(44, 89),
                    new(50, 89),

                },
                ["challengeStars"] = new()
                {

                    new(76, 67),
                    new(82, 67),
                    new(76, 74),
                    new(82, 74),

                },
            };

            return vectorIndex[effect];

        }

        public static int SpawnFrequency(string effect)
        {

            Dictionary<string, int> vectorIndex = new()
            {
                ["challengeEarth"] = 2,
                ["challengeWater"] = 6,
                ["challengeStars"] = 5,
            };

            return vectorIndex[effect];

        }

        public static string ChallengeLocation(string effect)
        {

            Dictionary<string, string> locationIndex = ChallengeLocationIndex();

            return locationIndex[effect];

        }

        public static Dictionary<string, string> ChallengeLocationIndex()
        {

            Dictionary<string, string> locationIndex = new()
            {
                ["challengeEarth"] = "UndergroundMine20",
                ["challengeWater"] = "Town",
                ["challengeStars"] = "Forest",
                ["swordEarth"] = "Forest",
                ["swordWater"] = "Beach",
                ["swordStars"] = "UndergroundMine100",
            };

            return locationIndex;

        }


    }

}
