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

        public static string FirstQuest()
        {

            return "approachEffigy";

        }

        public static Dictionary<string, QuestData> QuestList()
        {

            Dictionary<string, QuestData> questList = new()
            {
                ["approachEffigy"] = new()
                {


                    name = "approachEffigy",
                    questId = 280729871,
                    questValue = 6,
                    questTitle = "The Druid's Effigy",
                    questDescription = "Grandpa told me that the first farmers of the valley practiced an ancient druidic tradition.",
                    questObjective = "Investigate the effigy in the old farm cave.",
                    updateEffigy = false,

                },

                ["challengeEarth"] = new()
                {

                    name = "challengeEarth",
                    triggerCast = "CastEarth",
                    triggerType = "challenge",
                    triggerLocation = "UndergroundMine20",

                    questId = 280719872,
                    questValue = 6,
                    questTitle = "The Polluted Aquifier",
                    questDescription = "The Effigy believes the mountain spring has been polluted by rubbish dumped in the abandoned mineshafts.",
                    questObjective = "Perform a rite of the earth over the aquifier in level 20 of the mines.",
                    questReward = 1500,

                    triggerVector = new(24, 13),
                    triggerLimit = new(3, 3),
                    challengeWithin = new(21, 10),
                    challengeRange = new(9, 9),
                    challengePortals = new() {

                        new(23, 14),

                    },
                    challengeFrequency = 2,
                    updateEffigy = true,

                },
                ["challengeWater"] = new()
                {

                    name = "challengeWater",
                    triggerCast = "CastWater",
                    triggerType = "challenge",
                    triggerLocation = "Town",

                    questId = 280719873,
                    questValue = 6,
                    questTitle = "The Shadow Invasion",
                    questDescription = "The Effigy has heard the whispers of shadowy figures that loiter in the dark spaces of the village.",
                    questObjective = "Perform a rite of the water in Pelican Town's graveyard after dark.",
                    questReward = 3000,

                    triggerVector = new(45, 87),
                    triggerLimit = new(5, 5),
                    challengeWithin = new(42, 84),
                    challengeRange = new(10, 8),
                    challengePortals = new()
                    {

                        new(44, 89),
                        new(50, 89),

                    },
                    challengeFrequency = 6,
                    updateEffigy = true,

                },
                ["challengeStars"] = new()
                {

                    name = "challengeStars",
                    triggerCast = "CastEarth",
                    triggerType = "challenge",
                    triggerLocation = "Forest",

                    questId = 280719874,
                    questValue = 6,
                    questTitle = "The Slime Infestation",
                    questDescription = "The Effigy notes that many of the trees in the local forest have been marred with a slimy substance.",
                    questObjective = "Perform a rite of the stars in the clearing east of arrowhead island in Cindersap Forest.",
                    questReward = 5000,

                    triggerVector = new(77, 68),
                    triggerLimit = new(5, 5),
                    challengeWithin = new(74, 65),
                    challengeRange = new(14, 14),
                    challengePortals = new()
                    {

                        new(76, 67),
                        new(82, 67),
                        new(76, 74),
                        new(82, 74),

                    },
                    challengeFrequency = 5,
                    updateEffigy = true,
                },
                ["swordEarth"] = new()
                {

                    name = "swordEarth",
                    triggerCast = "CastEarth",
                    triggerType = "sword",
                    triggerLocation = "Forest",

                    triggerVector = new(38, 9),
                    triggerLimit = new(3, 4),
                    updateEffigy = true,
                },
                ["swordWater"] = new()
                {

                    name = "swordWater",
                    triggerCast = "CastWater",
                    triggerType = "sword",
                    triggerLocation = "Beach",

                    triggerVector = new(82, 38),
                    triggerLimit = new(9, 3),
                    updateEffigy = true,

                },
                ["swordStars"] = new()
                {

                    name = "swordStars",
                    triggerCast = "CastStars",
                    triggerType = "sword",
                    triggerLocation = "UndergroundMine100",

                    triggerVector = new(24, 13),
                    triggerLimit = new(10, 10),

                    updateEffigy = true,

                },

            };

            return questList;

        }        
        
        public static QuestData RetrieveQuest(string quest)
        {

            Dictionary<string, QuestData> questList = QuestList();

            return questList[quest];

        }



    }

}
