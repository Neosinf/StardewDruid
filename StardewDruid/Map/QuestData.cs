using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;
using StardewValley.Quests;
using StardewValley.Locations;
using System.Reflection;

namespace StardewDruid.Map
{
    static class QuestData
    {

        public static int StableVersion()
        {

            return 116;

        }

        public static string FirstQuest()
        {

            return "approachEffigy";

        }

        public static List<string> ResetTriggers()
        {

            List<string> triggerList = new()
            {

                "figureCanoli",

                "figureMariner",

                "figureSandDragon",

            };

            return triggerList;
            
        }

        public static StaticData ConfigureProgress(StaticData staticData, int blessingLevel)
        {

            staticData.questList = new();

            staticData.blessingList["setProgress"] = blessingLevel;

            if (blessingLevel == 0)
            {
                return staticData;

            }

            Dictionary<string, Map.Quest> questIndex = QuestList();

            foreach (KeyValuePair<string, Quest> questData in questIndex)
            {

                if(questData.Value.questLevel < blessingLevel)
                {   

                    if(questData.Value.questId != 0)
                    {
                        staticData.questList[questData.Key] = true;
                        
                    }

                    if (questData.Value.taskFinish != null)
                    {

                        staticData.taskList[questData.Value.taskFinish] = 1;

                    }

                }

            }

            staticData.blessingList["earth"] = Math.Min(6, blessingLevel);
            staticData.activeBlessing = "earth";

            if (blessingLevel > 6)
            {
                staticData.blessingList["water"] = Math.Min(6,blessingLevel - 6);
                staticData.activeBlessing = "water";
            }
            if (blessingLevel > 12)
            {
                staticData.blessingList["stars"] = Math.Min(2, blessingLevel - 12);
                staticData.activeBlessing = "stars";
            }

            

            return staticData;

        }

        public static StaticData QuestCheck(StaticData staticData)
        {

            int blessingLevel = 0;

            if (staticData.blessingList.ContainsKey("earth"))
            {
                blessingLevel = staticData.blessingList["earth"];

            }

            if (staticData.blessingList.ContainsKey("water"))
            {
                blessingLevel = 6 + staticData.blessingList["water"];

            }

            if (staticData.blessingList.ContainsKey("stars"))
            {
                blessingLevel = 12 + staticData.blessingList["stars"];

            }

            staticData = ConfigureProgress(staticData, blessingLevel);

            return staticData;

        }

        public static Vector2 SpecialVector(GameLocation playerLocation, string triggerString)
        {
            Dictionary<string, Vector2> specialList = new();

            switch (triggerString)
            {

                case "figureMariner":

                    if (playerLocation is Beach beachLocation && Game1.isRaining && !Game1.isFestival())
                    {

                        Type reflectType = typeof(Beach);

                        FieldInfo reflectField = reflectType.GetField("oldMariner", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                        var oldMariner = reflectField.GetValue(beachLocation);

                        if (oldMariner != null)
                        {

                            return (oldMariner as NPC).getTileLocation();

                        }

                    }

                    break;

                default:
                    break;


            }

            return new(-1);

        }

        public static Dictionary<string, Quest> QuestList()
        {

            Dictionary<string, Quest> questList = new()
            {
                ["approachEffigy"] = new()
                {

                    name = "approachEffigy",
                    questId = 18465001,
                    questValue = 6,
                    questTitle = "The Druid's Effigy",
                    questDescription = "Grandpa told me that the first farmers of the valley practiced an ancient druidic tradition.",
                    questObjective = "Investigate the effigy in the old farm cave.",
                    updateEffigy = false,
                    questLevel = 0,

                },

                ["challengeEarth"] = new()
                {

                    name = "challengeEarth",
                    triggerType = "challenge",
                    triggerLocation = new() { "UndergroundMine20", },
                    vectorList = new()
                    {
                        ["triggerVector"] = new(23, 12),
                        ["triggerLimit"] = new(4, 3),
                        ["challengeWithin"] = new(17, 10),
                        ["challengeRange"] = new(9, 9),
                    },
                    questId = 18465002,
                    questValue = 6,
                    questTitle = "The Polluted Aquifier",
                    questDescription = "The Effigy believes the mountain spring has been polluted by rubbish dumped in the abandoned mineshafts.",
                    questObjective = "Perform a rite over the aquifier in level 20 of the mines.",
                    questReward = 1500,
                    challengePortals = new() {

                        new(20, 13),

                    },
                    challengeSpawn = new()
                    {
                        99,

                    },
                    challengeFrequency = 2,
                    updateEffigy = true,
                    questLevel = 5,

                },
                ["challengeWater"] = new()
                {

                    name = "challengeWater",
                    triggerType = "challenge",
                    triggerLocation = new() { "Town", },
                    vectorList = new()
                    {
                        ["triggerVector"] = new(45, 87),
                        ["triggerLimit"] = new(5, 5),
                        ["challengeWithin"] = new(42, 84),
                        ["challengeRange"] = new(10, 8),
                    },
                    questId = 18465003,
                    questValue = 6,
                    questTitle = "The Shadow Invasion",
                    questDescription = "The Effigy has heard the whispers of shadowy figures that loiter in the dark spaces of the village.",
                    questObjective = "Perform a rite in Pelican Town's graveyard between 7:00 pm and Midnight.",
                    questReward = 3000,

                    startTime = 1900,
                    challengePortals = new()
                    {
                        new(44, 89),
                        new(50, 89),
                    },
                    challengeSpawn = new()
                    {
                        2,2,6,

                    },
                    challengeFrequency = 7,
                    challengeSeconds = 60,
                    updateEffigy = true,
                    questLevel = 11,

                },
                ["challengeStars"] = new()
                {

                    name = "challengeStars",
                    triggerType = "challenge",
                    triggerLocation = new() { "Forest", },
                    vectorList = new()
                    {
                        ["triggerVector"] = new(72, 71),
                        ["triggerLimit"] = new(14, 13),
                        ["challengeWithin"] = new(71, 70),
                        ["challengeRange"] = new(16, 15),
                    },
                    questId = 18465004,
                    questValue = 6,
                    questTitle = "The Slime Infestation",
                    questDescription = "Many of the trees in the local forest have been marred with a slimy substance. Has the old enemy of the farm returned?",
                    questObjective = "Perform a rite in the clearing east of arrowhead island in Cindersap Forest.",
                    questReward = 4500,

                    challengePortals = new()
                    {

                        new(75, 74),
                        new(83, 74),
                        new(75, 81),
                        new(83, 81),

                    },
                    challengeSpawn = new()
                    {
                        0,0,0,4,

                    },
                    challengeFrequency = 2,
                    challengeSeconds = 80,
                    updateEffigy = true,
                    
                    taskFinish = "finishLesson",
                    questLevel = 13,

                },
                ["swordEarth"] = new()
                {

                    name = "swordEarth",
                    triggerType = "sword",
                    triggerLocation = new() { "Forest", },
                    vectorList = new()
                    {
                        ["triggerVector"] = new(38, 9),
                        ["triggerLimit"] = new(3, 4),

                    },
                    questId = 18465005,
                    questValue = 6,
                    questTitle = "The Two Kings",
                    questDescription = "The Effigy has directed you to a source of otherworldly power.",
                    questObjective = "Malus trees bloom pink in the spring. Find the biggest specimen in the southern forest and perform a rite there.",
                    questReward = 100,

                    updateEffigy = true,
                    questLevel = 0,
                },
                ["swordWater"] = new()
                {

                    name = "swordWater",
                    triggerType = "sword",
                    triggerLocation = new() { "Beach", },
                    vectorList = new()
                    {
                        ["triggerVector"] = new(82, 38),
                        ["triggerLimit"] = new(9, 3),

                    },
                    questId = 18465006,
                    questValue = 6,
                    questTitle = "The Voice Beyond the Shore",
                    questDescription = "The Effigy wants you to introduce yourself to another source of otherworldly power.",
                    questObjective = "Find the far eastern pier at the beach and perform a rite there.",
                    questReward = 100,

                    updateEffigy = true,
                    questLevel = 6,
                },
                ["swordStars"] = new()
                {

                    name = "swordStars",
                    triggerType = "sword",
                    triggerLocation = new() { "UndergroundMine100", },
                    vectorList = new()
                    {
                        ["triggerVector"] = new(24, 13),
                        ["triggerLimit"] = new(10, 10),

                    },
                    questId = 18465007,
                    questValue = 6,
                    questTitle = "The Stars Themselves",
                    questDescription = "The Effigy wants you to introduce yourself to another source of otherworldly power.",
                    questObjective = "Find the lake of flame deep in the mountain and perform a rite there.",
                    questReward = 100,

                    updateEffigy = true,
                    questLevel = 12,
                },

                ["figureCanoli"] = new()
                {

                    name = "figureCanoli",
                    triggerType = "figure",
                    triggerLocale = new() { typeof(Woods), },
                    triggerBlessing = "water",
                    triggerTile = 1140,
                    triggerRadius = 2,
                    useTarget = true,

                },

                ["figureMariner"] = new()
                {

                    name = "figureMariner",
                    triggerType = "figure",
                    triggerLocale = new() { typeof(Beach), },
                    triggerBlessing = "water",
                    triggerSpecial = true,
                    useTarget = true,
                    vectorList = new()
                    {
                        ["specialOffset"] = new(-2,-2),
                        ["triggerLimit"] = new(5, 5),
                    },

                },

                ["figureSandDragon"] = new()
                {

                    name = "figureSandDragon",
                    triggerType = "figure",
                    triggerLocale = new() { typeof(Desert), },
                    triggerBlessing = "stars",
                    triggerAction = "SandDragon",
                    triggerRadius = 3,

                },

                ["lessonVillager"] = new() {

                    questId = 18465011,
                    questValue = 6,
                    questTitle = "Druid Lesson One: Villagers",
                    questDescription = "Demonstrate your command over the Earth to the locals.",
                    questObjective = "Perform a rite in range of four different villagers. Unlocks friendship gain.",
                    questReward = 500,

                    taskCounter = 4,
                    taskFinish = "masterVillager",
                    questLevel = 1,
                },

                ["lessonCreature"] = new() {

                    questId = 18465012,
                    questValue = 6,
                    questTitle = "Druid Lesson Two: Creatures",
                    questDescription = "The Earth holds many riches, and some are guarded jealously.",
                    questObjective = "Draw out ten local creatures. Unlocks wild seed gathering from grass.",
                    questReward = 500,

                    taskCounter = 10,
                    taskFinish = "masterCreature",
                    questLevel = 2,
                },

                ["lessonForage"] = new() {

                    questId = 18465013,
                    questValue = 6,
                    questTitle = "Druid Lesson Three: Flowers",
                    questDescription = "The Druid fills the barren spaces with life.",
                    questObjective = "Sprout six forageables total in the Forest or elsewhere. Unlocks flowers.",
                    questReward = 500,

                    taskCounter = 6,
                    taskFinish = "masterForage",
                    questLevel = 3,
                },

                ["lessonCrop"] = new()
                {
                    questId = 18465014,
                    questValue = 6,
                    questTitle = "Druid Lesson Four: Crops",
                    questDescription = "The Farmer and the Druid share the same vision.",
                    questObjective = "Convert twenty planted wild seeds into domestic crops. Unlocks quality conversions.",
                    questReward = 500,

                    taskCounter = 20,
                    taskFinish = "masterCrop",
                    questLevel = 4,
                },

                ["lessonRock"] = new()
                {
                    questId = 18465015,
                    questValue = 6,
                    questTitle = "Druid Lesson Five: Rocks",
                    questDescription = "The Druid draws power from the deep rock.",
                    questObjective = "Draw one hundred stone from rockfalls in the mines. Unlocks rockfall damage to monsters.",
                    questReward = 500,

                    taskCounter = 100,
                    taskFinish = "masterRock",
                    questLevel = 5,
                },

                ["lessonTotem"] = new()
                {
                    questId = 18465016,
                    questValue = 6,
                    questTitle = "Druid Lesson Six: Hidden Power",
                    questDescription = "The power of the valley gathers where ley lines intersect.",
                    questObjective = "Draw the power out of two different warp shrines. Unlocks chance for double extraction.",
                    questReward = 600,

                    taskCounter = 2,
                    taskFinish = "masterTotem",
                    questLevel = 7,
                },

                ["lessonCookout"] = new()
                {
                    questId = 18465017,
                    questValue = 6,
                    questTitle = "Druid Lesson Seven: Cookouts",
                    questDescription = "Every Druid should know how to cook",
                    questObjective = "Create two cookouts from campfires, either in town or from crafts. Unlocks extra recipes.",
                    questReward = 700,

                    taskCounter = 3,
                    taskFinish = "masterCookout",
                    questLevel = 8,
                },

                ["lessonFishspot"] = new()
                {
                    questId = 18465018,
                    questValue = 6,
                    questTitle = "Druid Lesson Eight: Fishing",
                    questDescription = "Nature is always ready to test your skill",
                    questObjective = "Catch ten fish from a spawning ground created by Rite of the Water. Unlocks rarer fish.",
                    questReward = 800,

                    taskCounter = 10,
                    taskFinish = "masterFishspot",
                    questLevel = 9,
                },

                ["lessonSmite"] = new()
                {
                    questId = 18465019,
                    questValue = 6,
                    questTitle = "Druid Lesson Nine: Smite",
                    questDescription = "Call lightning down upon your enemies",
                    questObjective = "Smite enemies twenty times. Unlocks critical hits.",
                    questReward = 900,

                    taskCounter = 20,
                    taskFinish = "masterSmite",
                    questLevel = 10,
                },

                ["lessonPortal"] = new()
                {
                    questId = 18465020,
                    questValue = 6,
                    questTitle = "Druid Lesson Ten: Portals",
                    questDescription = "Who knows what lies beyond the veil",
                    questObjective = "Create a portal in the backwoods or elsewhere and hold your ground against invaders.",
                    questReward = 1000,

                    taskCounter = 1,
                    taskFinish = "masterPortal",
                    questLevel = 11,
                },

                ["lessonMeteor"] = new()
                {
                    questId = 18465021,
                    questValue = 6,
                    questTitle = "Druid Lesson Eleven: Firerain",
                    questDescription = "Call down a meteor shower to clear areas for new growth",
                    questObjective = "Summon fifty fireballs. Unlocks priority targetting of stone nodes.",
                    questReward = 1000,

                    taskCounter = 50,
                    taskFinish = "masterMeteor",
                    questLevel = 13,
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
