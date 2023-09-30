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
    static class QuestData
    {

        public static string FirstQuest()
        {

            return "approachEffigy";

        }

        public static Dictionary<string, Quest> QuestList()
        {

            Dictionary<string, Quest> questList = new()
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
                    triggerVector = new(23, 12),
                    triggerLimit = new(4, 3),

                    questId = 280719872,
                    questValue = 6,
                    questTitle = "The Polluted Aquifier",
                    questDescription = "The Effigy believes the mountain spring has been polluted by rubbish dumped in the abandoned mineshafts.",
                    questObjective = "Perform a rite over the aquifier in level 20 of the mines.",
                    questReward = 1500,

                    challengeWithin = new(20, 10),
                    challengeRange = new(9, 9),
                    challengePortals = new() {

                        new(21, 13),

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
                    triggerVector = new(45, 87),
                    triggerLimit = new(5, 5),

                    questId = 280719873,
                    questValue = 6,
                    questTitle = "The Shadow Invasion",
                    questDescription = "The Effigy has heard the whispers of shadowy figures that loiter in the dark spaces of the village.",
                    questObjective = "Perform a rite in Pelican Town's graveyard between 7:00 pm and Midnight.",
                    questReward = 3000,

                    startTime = 1900,
                    //endTime = 2400,
                    challengeWithin = new(42, 84),
                    challengeRange = new(10, 8),
                    challengePortals = new()
                    {

                        new(44, 89),
                        new(50, 89),

                    },
                    challengeFrequency = 6,
                    challengeSeconds = 60,
                    updateEffigy = true,

                },
                ["challengeStars"] = new()
                {

                    name = "challengeStars",
                    triggerCast = "CastStars",
                    triggerType = "challenge",
                    triggerLocation = "Forest",
                    triggerVector = new(77, 68),
                    triggerLimit = new(5, 5),

                    questId = 280719874,
                    questValue = 6,
                    questTitle = "The Slime Infestation",
                    questDescription = "Many of the trees in the local forest have been marred with a slimy substance. Has the old enemy of the farm returned?",
                    questObjective = "Perform a rite in the clearing east of arrowhead island in Cindersap Forest.",
                    questReward = 4500,

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
                    challengeSeconds = 60,
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

                    questId = 280719875,
                    questValue = 6,
                    questTitle = "The Two Kings",
                    questDescription = "The Effigy has directed you to a source of otherworldly power.",
                    questObjective = "Malus trees bloom pink in the spring. Find the biggest specimen in the southern forest and perform a rite there.",
                    questReward = 100,

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

                    questId = 280719876,
                    questValue = 6,
                    questTitle = "The Voice Beyond the Shore",
                    questDescription = "The Effigy wants you to introduce yourself to another source of otherworldly power.",
                    questObjective = "Find the far eastern pier at the beach and perform a rite there.",
                    questReward = 100,

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

                    questId = 280719877,
                    questValue = 6,
                    questTitle = "The Stars Themselves",
                    questDescription = "The Effigy wants you to introduce yourself to another source of otherworldly power.",
                    questObjective = "Find the lake of flame deep in the mountain and perform a rite there.",
                    questReward = 100,

                    updateEffigy = true,

                },

            };

            return questList;

        }        
        
        public static Quest RetrieveQuest(string quest)
        {

            Dictionary<string, Quest> questList = QuestList();

            return questList[quest];

        }



    }

}
