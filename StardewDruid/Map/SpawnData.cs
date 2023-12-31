using GenericModConfigMenu;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using xTile.Dimensions;

namespace StardewDruid.Map
{
    static class SpawnData
    {
        public static Dictionary<int, string> WeaponAttunement()
        {
            return new Dictionary<int, string>()
            {
                [15] = "weald",
                [44] = "weald",
                [14] = "mists",
                [9] = "stars",
                [53] = "fates",
                [57] = "ether"
            };
        }

        public static List<int> StoneIndex()
        {

            List<int> stoneIndex = new()
            {
                2,  // ruby
                4,  // diamond
                6,  // jade
                8,  // amethyst
                10, // topaz
                12, // emerald
                14, // aquamarine
                44, // special ore
                46, // mystic ore
            };

            return stoneIndex;

        }

        public static List<int> RoughageList()
        {
            List<int> roughageIndex = new()
            {
                92, // Sap
                766, // Slime
                311, // PineCone
                310, // MapleSeed
                309, // Acorn
                292, // Mahogany
                767, // BatWings
                420, // RedMushroom
                831, // Taro Tuber
            };

            return roughageIndex;
        }

        public static List<int> LunchList()
        {

            List<int> lunchIndex = new()
            {
                399, // SpringOnion
                403, // Snackbar
                404, // FieldMushroom
                257, // Morel
                281, // Chanterelle
                152, // Seaweed
                153, // Algae
                157, // white Algae
                78, // Carrot
                227, // Sashimi
                296, // Salmonberry
                410, // Blackberry
                424, // Cheese
                24, // Parsnip
                851, // Magma Cap
            };

            return lunchIndex;

        }

        public static Dictionary<int, int> CoffeeList()
        {

            Dictionary<int, int> coffeeList = new()
            {
                [167] = 60000, // Joja Cola
                [433] = 20000, // Coffee Beans
                [829] = 60000, // Ginger
                [815] = 60000, // Tea Leaves
                [614] = 120000, // Tea
                [395] = 90000, // Coffee
                [253] = 300000, // Triple Espresso
                [349] = 300000, //349: "Energy Tonic/500/200/Crafting/Energy Tonic/Restores a lot of energy./drink/0 0 0 0 0 0 0 0 0 0 0/0",

            };

            return coffeeList;

        }

        public static int RandomTree(GameLocation location)
        {
            List<int> treeIndex;

            if (location is Desert || location is IslandLocation)
            {

                treeIndex = new()
                    {
                        6,8,9,
                    };

            }
            else
            {

                treeIndex = new()
                    {
                        1,2,3,1,2,3,1,2,3,8,
                    };

                if (location is Farm)
                {

                    treeIndex.Add(7);

                }

            };

            return treeIndex[Game1.random.Next(treeIndex.Count)];

        }

        public static List<int> RockFall(GameLocation location, Farmer player, int specialChance = 10)
        {

            List<int> rockList = new();

            int objectIndex;

            int scatterIndex;

            int debrisIndex;

            Dictionary<int, int> objectIndexes;

            Dictionary<int, int> specialIndexes;

            if (location is MineShaft shaftLocation)
            {

                if (shaftLocation.mineLevel <= 40)
                {

                    objectIndexes = new()
                    {
                        [0] = 32, // grade 1 stone
                        [1] = 40, // grade 1 stone
                        [2] = 42, // grade 1 stone
                    };

                    specialIndexes = new()
                    {

                        [0] = 382, // coal stone
                        [1] = 378, // copper stone
                        [2] = 378, // copper stone
                        [3] = 378, // copper stone
                        //[4] = 66, // amethyst
                        //[5] = 68, // topaz

                    };


                }
                else if (shaftLocation.mineLevel <= 80)
                {

                    objectIndexes = new()
                    {
                        [0] = 48, // grade 2a stone
                        [1] = 50, // grade 2b stone
                        [2] = 52, // grade 2c stone
                        [3] = 54, // grade 2d stone
                    };

                    specialIndexes = new()
                    {

                        [0] = 382, // coal stone
                        [1] = 380, // iron ore
                        [2] = 380, // iron ore
                        [3] = 380, // iron ore
                        //[4] = 60, // emerald
                        //[5] = 62, // aquamarine

                    };



                }
                else if (shaftLocation.mineLevel <= 120)
                {

                    objectIndexes = new()
                    {
                        [0] = 760, // grade 3 stone
                        [1] = 762, // grade 3 stone
                        [2] = 56, // grade 3 stone
                    };

                    specialIndexes = new()
                    {

                        [0] = 382, // coal stone
                        [1] = 384, // gold ore
                        [2] = 384, // gold ore
                        [3] = 384, // gold ore
                        //[4] = 72, // ruby
                        //[5] = 64, // diamond*

                    };

                }
                else // Skull Cavern
                {
                    objectIndexes = new()
                    {
                        [0] = 760, // grade 3 stone
                        [1] = 762, // grade 3 stone
                        [2] = 56, // grade 3 stone
                    };

                    specialIndexes = new()
                    {

                        [0] = 382, // coal stone
                        [1] = 386, // iridium ore
                        [2] = 386, // iridium ore
                        [3] = 386, // iridium ore
                        [4] = 386, // iridium ore

                    };

                }

            }
            else // assume Volcano dungeon
            {

                objectIndexes = new()
                {
                    [0] = 845, // volcanic stone
                    [1] = 846, // volcanic stone
                    [2] = 847, // volcanic stone
                };

                specialIndexes = new()
                {

                    [0] = 848, // cinder shards
                    [1] = 848, // cinder shards
                    [2] = 848, // cinder shards
                    [3] = 386, // iridium ore
                    [4] = 384, // gold ore

                };

            }

            Dictionary<int, int> scatterIndexes = new()
            {
                [32] = 33,
                [40] = 41,
                [42] = 43,
                [44] = 45,
                [48] = 49,
                [50] = 51,
                [52] = 53,
                [54] = 55,
                [56] = 57,
                [58] = 59,
                [760] = 761,
                [762] = 763,
                [845] = 761,
                [846] = 763,
                [847] = 761,
            };


            objectIndex = objectIndexes[Game1.random.Next(objectIndexes.Count)];

            scatterIndex = scatterIndexes[objectIndex];

            debrisIndex = 390;

            if (Game1.random.Next(specialChance) == 0)
            {
                debrisIndex = specialIndexes[Game1.random.Next(specialIndexes.Count)];

                player.gainExperience(3, 4); // gain mining experience for special drops

            }

            rockList.Add(objectIndex);

            rockList.Add(scatterIndex);

            rockList.Add(debrisIndex);

            return rockList;

        }

        public static Dictionary<int, int> CropList(GameLocation location)
        {

            Dictionary<int, int> objectIndexes;

            string season = Game1.currentSeason;

            if (location.isGreenhouse.Value)
            {

                season = "greenhouse";

            }

            if (location is IslandLocation)
            {

                season = "island";

            }

            switch (season)
            {

                case "spring":

                    objectIndexes = new()
                    {
                        [0] = 478, // rhubarb
                        [1] = 476, // garlic
                        [2] = 433, // coffee
                        [3] = 745, // strawberry
                        [4] = 473, // bean
                        [5] = 477, // kale
                    };

                    break;

                case "summer":
                case "greenhouse":

                    objectIndexes = new()
                    {
                        [0] = 479, // melon
                        [1] = 485, // red cabbage
                        [2] = 433, // coffee
                        [3] = 481, // blueberry
                        [4] = 302, // hops
                        [5] = 483, // wheat
                    };


                    break;

                case "fall":

                    objectIndexes = new()
                    {
                        [0] = 490, // pumpkin
                        [1] = 492, // yam
                        [2] = 299, // amaranth
                        [3] = 493, // cranberry
                        [4] = 301, // grape
                    };

                    break;

                case "island":

                    objectIndexes = new()
                    {
                        [0] = 833, // pineapple
                        [1] = 831, // taro
                        [2] = 486, // starfruit
                        [3] = 829, // ginger

                    };

                    break;

                default:

                    objectIndexes = new();
                    break;

            }

            return objectIndexes;

        }

        public static int RandomFlower()
        {

            Dictionary<int, int> objectIndexes;

            switch (Game1.currentSeason)
            {

                case "spring":

                    objectIndexes = new()
                    {
                        [0] = 591, // tulip
                        [1] = 597, // jazz
                    };

                    break;

                case "summer":

                    objectIndexes = new()
                    {
                        [0] = 593, // spangle
                        [1] = 376, // poppy
                    };

                    break;

                default: //"fall":

                    objectIndexes = new()
                    {
                        [0] = 595, // fairy
                        [1] = 421, // sunflower
                        [2] = 418, // crocus
                    };

                    break;

            }

            int randomFlower = objectIndexes[Game1.random.Next(objectIndexes.Count)];

            return randomFlower;

        }

        public static int RandomForage(GameLocation location)
        {
            Dictionary<int, int> randomCrops;

            int randomCrop;

            string season;

            if (location is IslandEast || location is Woods)
            {

                season = "woods";

            }
            else
            {
                season = Game1.currentSeason;

            }

            switch (season)
            {

                case "spring":

                    randomCrop = 16 + Game1.random.Next(4) * 2;

                    break;

                case "summer":

                    randomCrops = new()
                    {
                        [0] = 396,
                        [1] = 396,
                        [2] = 402,
                        [3] = 398,
                    };

                    randomCrop = randomCrops[Game1.random.Next(4)];

                    break;

                case "woods":

                    randomCrops = new()
                    {

                        [0] = 257,
                        [1] = 259,
                        [2] = 815,

                    };

                    randomCrop = randomCrops[Game1.random.Next(randomCrops.Count)];

                    break;

                default: //"fall":

                    randomCrop = 404 + Game1.random.Next(4) * 2;

                    break;

            }

            return randomCrop;

        }

        public static int RandomLowFish(GameLocation location)
        {
            Dictionary<int, int> objectIndexes;

            if (location is Beach)
            {

                objectIndexes = new Dictionary<int, int>()
                {


                    [0] = 131, // sardine
                    [1] = 147, // herring
                    [2] = 129, // anchovy
                    [3] = 701, // tilapia
                    [4] = 131, // sardine
                    [5] = 147, // herring
                    [6] = 129, // anchovy
                    [7] = 150, // red snapper

                };

            }
            else if (location is IslandLocation)
            {

                objectIndexes = new Dictionary<int, int>()
                {


                    [0] = 838, // blue discuss
                    [1] = 837, // lionfish
                    [2] = 267, // flounder
                    [3] = 701, // tilapia
                    [4] = 838, // blue discuss
                    [5] = 837, // lionfish
                    [6] = 267, // flounder
                    [7] = 150, // red snapper

                };

            }
            else
            {

                objectIndexes = new Dictionary<int, int>()
                {

                    [0] = 145, // carp
                    [1] = 137, // smallmouth bass
                    [2] = 142,  // sunfish
                    [3] = 141, // perch
                    [4] = 145, // carp
                    [5] = 137, // smallmouth bass
                    [6] = 142,  // sunfish
                    [7] = 132  // bream

                };

            }

            int randomFish = objectIndexes[Game1.random.Next(objectIndexes.Count)];

            return randomFish;

        }

        public static int RandomHighFish(GameLocation location, bool enableRare)
        {

            Dictionary<int, int> objectIndexes;

            int seasonStar;

            if (location is Woods || location is Desert || location is Caldera || location is MineShaft)
            {

                switch (Game1.currentSeason)
                {
                    case "spring":
                        seasonStar = 734;
                        break;
                    case "fall":
                    case "winter":
                        seasonStar = 161;
                        break;
                    default:
                        seasonStar = 162;
                        break;

                }

                objectIndexes = new()
                {
                    [0] = 161, // ice pip
                    [1] = 734, // wood skip
                    [2] = 164, // sand fish
                    [3] = 165, // scorpion carp
                    [4] = 156, // ghost fish
                    [5] = seasonStar,
                    [6] = seasonStar,
                };

                if (enableRare)
                {
                    objectIndexes[7] = 162;  // lava eel

                }
            }
            else if (location is Beach || location is IslandLocation)
            {

                switch (Game1.currentSeason)
                {
                    case "spring":
                    case "fall":
                        seasonStar = 148;
                        break;
                    case "winter":
                        seasonStar = 151;
                        break;
                    default:
                        seasonStar = 155;
                        break;

                }

                objectIndexes = new()
                {
                    [0] = 148, // eel
                    [1] = 149, // squid
                    [2] = 151, // octopus
                    [3] = 155, // super cucumber
                    [4] = 128, // puff ball
                    [5] = seasonStar,
                    [6] = seasonStar,
                };

                if (enableRare)
                {
                    objectIndexes[7] = 836;  // stingray

                }

            }
            else
            {

                switch (Game1.currentSeason)
                {
                    case "spring":
                    case "fall":
                        seasonStar = 143; // catfish
                        break;
                    default:
                        seasonStar = 698; // sturgeon
                        break;

                }

                objectIndexes = new()
                {
                    [0] = 143, // cat fish
                    [1] = 698, // sturgeon
                    [2] = 140, // walleye
                    [3] = 699, // tiger trout
                    [4] = 158, // stone fish
                    [5] = seasonStar,
                    [6] = seasonStar,

                };

                if (enableRare)
                {
                    objectIndexes[7] = 269;  // midnight carp

                }

            }

            int randomFish = objectIndexes[Game1.random.Next(objectIndexes.Count)];

            return randomFish;

        }

        public static int RandomJumpFish(GameLocation location)
        {

            int fishIndex;

            if (location is Caldera || location is MineShaft)
            {
                if (location.Name.Contains("60"))
                {
                    fishIndex = 161; // ice pip
                }
                else
                {
                    fishIndex = 165;  // scorpion carp
                }
            }
            else if (location is Woods)
            {
                fishIndex = 734; // wood skip
            }
            else if (location is Desert)
            {
                fishIndex = 164; // sand fish
            }
            else if (location is Beach)
            {
                fishIndex = 128; // puff ball 
            }
            else if (location is IslandLocation)
            {
                fishIndex = 836;  // stingray  
            }
            else
            {
                fishIndex = 699; // tiger trout
            }

            return fishIndex;

        }

        public static int RandomPoolFish(GameLocation location)
        {

            Dictionary<int, int> objectIndexes;

            if (location.Name.Contains("Beach"))
            {

                objectIndexes = new Dictionary<int, int>()
                {

                    [0] = 392, // nautilus shell
                    [1] = 152, // seaweed
                    [2] = 152, // seaweed
                    [3] = 397, // urchin
                    [4] = 718, // cockle
                    [5] = 715, // lobster
                    [6] = 720, // shrimp
                    [7] = 719, // mussel
                };

            }
            else
            {

                objectIndexes = new Dictionary<int, int>()
                {

                    [0] = 153, // algae
                    [1] = 153, // algae
                    [2] = 153, // algae
                    [3] = 153, // algae
                    [4] = 721, // snail 721
                    [5] = 716, // crayfish 716
                    [6] = 722, // periwinkle 722
                    [7] = 717, // crab 717

                };

            }

            int probability = new Random().Next(objectIndexes.Count);

            int objectIndex = objectIndexes[probability];

            return objectIndex;

        }

        public static int RandomTreasure(GameLocation location, bool rareTreasure = false)
        {

            Dictionary<int, int> objectIndexes;

            if (location is Beach)
            {

                objectIndexes = new Dictionary<int, int>()
                {
                    [0] = 797, //"Pearl/2500/-300/Basic/Pearl/A rare treasure from the sea.///",
                    [1] = 275, //"Artifact Trove/0/-300/Basic/Artifact Trove/A blacksmith can open this for you. These troves often contain ancient relics and curiosities./100 101 103 104 105 106 108 109 110 111 112 113 114 115 116 117 118 119 120 121 122 123 124 125 166 373 797//",
                    [2] = 166, //"Treasure Chest/5000/-300/Basic/Treasure Chest/Wow, it's loaded with treasure! This is sure to fetch a good price./Day Night^Spring Summer Fall Winter//",
                    [3] = 852, //"Dragon Tooth/500/-300/Basic/Dragon Tooth/These are rumored to be the teeth of ancient serpents. The enamel is made of pure iridium!///",
                    [4] = 167, //"Joja Cola/25/5/Fish -20/Joja Cola/The flagship product of Joja corporation./drink/0 0 0 0 0 0 0 0 0 0 0/0",
                    [5] = 392, // nautilus shell
                    [6] = 152, // seaweed
                    [7] = 152, // seaweed
                    [8] = 397, // urchin
                    [9] = 718, // cockle
                    [10] = 715, // lobster
                    [11] = 720, // shrimp
                    [12] = 719, // mussel
                    [13] = 393, //"Coral/80/-300/Basic -23/Coral/A colony of tiny creatures that clump together to form beautiful structures.///",
                    [14] = 394, //"Rainbow Shell/300/-300/Basic -23/Rainbow Shell/It's a very beautiful shell.///",
                    [15] = 131, // sardine
                    [16] = 147, // herring
                    [17] = 129, // anchovy
                    [18] = 701, // tilapia
                    [19] = 150, // red snapper
                    [20] = 148, // eel
                    [21] = 149, // squid
                    [22] = 151, // octopus
                    [23] = 155, // super cucumber
                    [24] = 128, // puff ball

                };

            }
            else if (location is IslandLocation)
            {

                objectIndexes = new Dictionary<int, int>()
                {

                    [0] = 797, //"Pearl/2500/-300/Basic/Pearl/A rare treasure from the sea.///",
                    [1] = 275, //"Artifact Trove/0/-300/Basic/Artifact Trove/A blacksmith can open this for you. These troves often contain ancient relics and curiosities./100 101 103 104 105 106 108 109 110 111 112 113 114 115 116 117 118 119 120 121 122 123 124 125 166 373 797//",
                    [2] = 166, //"Treasure Chest/5000/-300/Basic/Treasure Chest/Wow, it's loaded with treasure! This is sure to fetch a good price./Day Night^Spring Summer Fall Winter//",
                    [3] = 852, //"Dragon Tooth/500/-300/Basic/Dragon Tooth/These are rumored to be the teeth of ancient serpents. The enamel is made of pure iridium!///",
                    [4] = 167, //"Joja Cola/25/5/Fish -20/Joja Cola/The flagship product of Joja corporation./drink/0 0 0 0 0 0 0 0 0 0 0/0",
                    [5] = 392, // nautilus shell
                    [6] = 152, // seaweed
                    [7] = 152, // seaweed
                    [8] = 397, // urchin
                    [9] = 718, // cockle
                    [10] = 715, // lobster
                    [11] = 720, // shrimp
                    [12] = 719, // mussel
                    [13] = 393, //"Coral/80/-300/Basic -23/Coral/A colony of tiny creatures that clump together to form beautiful structures.///",
                    [14] = 394, //"Rainbow Shell/300/-300/Basic -23/Rainbow Shell/It's a very beautiful shell.///",
                    [15] = 838, // blue discuss
                    [16] = 837, // lionfish
                    [17] = 267, // flounder
                    [18] = 701, // tilapia
                    [19] = 838, // blue discuss
                    [20] = 837, // lionfish
                    [21] = 267, // flounder
                    [22] = 150, // red snapper

                };

            }
            else
            {

                objectIndexes = new Dictionary<int, int>()
                {

                    [0] = 797, //"Pearl/2500/-300/Basic/Pearl/A rare treasure from the sea.///",
                    [1] = 275, //"Artifact Trove/0/-300/Basic/Artifact Trove/A blacksmith can open this for you. These troves often contain ancient relics and curiosities./100 101 103 104 105 106 108 109 110 111 112 113 114 115 116 117 118 119 120 121 122 123 124 125 166 373 797//",
                    [2] = 166, //"Treasure Chest/5000/-300/Basic/Treasure Chest/Wow, it's loaded with treasure! This is sure to fetch a good price./Day Night^Spring Summer Fall Winter//",
                    [3] = 852, //"Dragon Tooth/500/-300/Basic/Dragon Tooth/These are rumored to be the teeth of ancient serpents. The enamel is made of pure iridium!///",
                    [4] = 167, //"Joja Cola/25/5/Fish -20/Joja Cola/The flagship product of Joja corporation./drink/0 0 0 0 0 0 0 0 0 0 0/0",
                    [5] = 145, // carp
                    [6] = 137, // smallmouth bass
                    [7] = 142,  // sunfish
                    [8] = 141, // perch
                    [9] = 132,  // bream
                    [10] = 153, // algae
                    [11] = 153, // algae
                    [12] = 721, // snail 721
                    [13] = 716, // crayfish 716
                    [14] = 722, // periwinkle 722
                    [15] = 717, // crab 717
                    [16] = 517, //"Glow Ring/200/-300/Ring/Glow Ring/Emits a constant light.///",
                    [17] = 519, //"Magnet Ring/200/-300/Ring/Magnet Ring/Increases your radius for collecting items.///",
                    [18] = 143, // cat fish
                    [19] = 698, // sturgeon
                    [20] = 140, // walleye
                    [21] = 699, // tiger trout
                    [22] = 158, // stone fish}

                };

            }

            int bottom = rareTreasure ? 0 : 4;

            int probability = new Random().Next(bottom,objectIndexes.Count);

            int objectIndex = objectIndexes[probability];

            return objectIndex;

        }

        public static int HighTreasure(string terrain)
        {

            Dictionary<int, int> objectIndexes;

            switch (terrain)
            {

                case "ground":

                    objectIndexes = new Dictionary<int, int>()
                    {
                        
                        [0] = 166, //"Treasure Chest/5000/-300/Basic/Treasure Chest/Wow, it's loaded with treasure! This is sure to fetch a good price./Day Night^Spring Summer Fall Winter//",

                    };

                    break;

                case "water":

                    objectIndexes = new Dictionary<int, int>()
                    {

                        [0] = 797, //"Pearl/2500/-300/Basic/Pearl/A rare treasure from the sea.///",

                    };

                    break;

                default:


                    objectIndexes = new Dictionary<int, int>()
                    {

                        [0] = 852, //"Dragon Tooth/500/-300/Basic/Dragon Tooth/These are rumored to be the teeth of ancient serpents. The enamel is made of pure iridium!///",

                    };

                    break;
            }

            int probability = new Random().Next(objectIndexes.Count);

            int objectIndex = objectIndexes[probability];

            return objectIndex;

        }

        public static List<string> RecipeList()
        {
            List<string> recipeList = new(){
                "Salad",
                "Baked Fish",
                "Fried Mushroom",
                "Carp Surprise",
                "Hashbrowns",
                "Fried Eel",
                "Sashimi",
                "Maki Roll",
                "Algae Soup",
                "Fish Stew",
                "Escargot",
                "Pale Broth",
            };

            return recipeList;

        }

        public static List<string> MachineList()
        {

            List<string> machineList = new(){
                "Deconstructor",
                "Bone Mill",
                "Keg",
                "Preserves Jar",
                "Cheese Press",
                "Mayonnaise Machine",
                "Loom",
                "Oil Maker",
                "Furnace",
                "Geode Crusher",
            };

            return machineList;

        }

        public static Dictionary<string, bool> SpawnTemplate()
        {
            Dictionary<string, bool> spawnTemplate = new()
            {

                ["weeds"] = false,
                ["forage"] = false,
                ["flower"] = false,
                ["grass"] = false,
                ["trees"] = false,
                ["fishup"] = false,
                ["portal"] = false,
                ["wildspawn"] = false,
                ["fishspot"] = false,
                ["cropseed"] = false,
                ["artifact"] = false,
                ["whisk"] = false,
                ["gravity"] = false,
                ["teahouse"] = false,
                ["treasure"] = false,

            };

            return spawnTemplate;

        }

        public static Dictionary<string, bool> SpawnIndex(GameLocation playerLocation)
        {

            Dictionary<string, bool> spawnIndex;

            spawnIndex = SpawnTemplate();

            if (Mod.instance.CastAnywhere())
            {

                foreach (KeyValuePair<string, bool> keyValuePair in spawnIndex)
                {

                    spawnIndex[keyValuePair.Key] = true;

                }

                return spawnIndex;

            }

            if (playerLocation is Farm || playerLocation.Name == "Custom_Garden")
            {

                spawnIndex["weeds"] = true;
                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["trees"] = true;
                spawnIndex["fishup"] = true;
                spawnIndex["wildspawn"] = true;
                spawnIndex["cropseed"] = true;
                spawnIndex["whisk"] = true;
                spawnIndex["gravity"] = true;

            }
            else if (playerLocation.isGreenhouse.Value)
            {

                spawnIndex["cropseed"] = true;
                spawnIndex["teahouse"] = true;

            }
            else if (playerLocation is IslandWest || playerLocation is IslandNorth)
            {
                spawnIndex["fishup"] = true;
                spawnIndex["fishspot"] = true;
                spawnIndex["wildspawn"] = true;
                spawnIndex["cropseed"] = true;
                spawnIndex["trees"] = true;
                spawnIndex["weeds"] = true;
                spawnIndex["artifact"] = true;
                spawnIndex["whisk"] = true;
                spawnIndex["gravity"] = true;

            }
            else if (playerLocation is Forest || playerLocation is Mountain || playerLocation is Desert || playerLocation is BusStop)
            {
                spawnIndex["weeds"] = true;
                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["trees"] = true;
                spawnIndex["fishup"] = true;
                spawnIndex["wildspawn"] = true;
                spawnIndex["fishspot"] = true;
                spawnIndex["artifact"] = true;
                spawnIndex["whisk"] = true;

            }
            else if (playerLocation.Name.Contains("Backwoods") || playerLocation is Railroad)
            {

                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["trees"] = true;
                spawnIndex["wildspawn"] = true;
                spawnIndex["portal"] = true;
                spawnIndex["artifact"] = true;
                spawnIndex["whisk"] = true;

            }
            else if (playerLocation is Woods || playerLocation.Name.Contains("Grampleton") || playerLocation is IslandEast || playerLocation is IslandShrine)
            {

                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["wildspawn"] = true;
                spawnIndex["portal"] = true;
                spawnIndex["fishspot"] = true;
                spawnIndex["weeds"] = true;

            }
            else if (playerLocation is MineShaft || playerLocation is VolcanoDungeon) //|| playerLocation.Name.Contains("Mine"))
            {

                if (playerLocation.Name.Contains("60") || playerLocation.Name.Contains("100"))
                {
                    spawnIndex["fishspot"] = true;

                }

                spawnIndex["weeds"] = true;

            }
            else if (playerLocation is Beach || playerLocation is IslandSouth || playerLocation is IslandSouthEast || playerLocation is IslandSouthEastCave)
            {

                spawnIndex["wildspawn"] = true;
                spawnIndex["fishup"] = true;
                spawnIndex["fishspot"] = true;
                spawnIndex["artifact"] = true;
                spawnIndex["whisk"] = true;

            }
            else if (playerLocation is Town)
            {
                spawnIndex["weeds"] = true;
                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["fishup"] = true;
                spawnIndex["artifact"] = true;
                spawnIndex["whisk"] = true;

            }
            else if (playerLocation.Name.Contains("DeepWoods"))
            {

                spawnIndex["wildspawn"] = true;
                spawnIndex["fishspot"] = true;

            }
            else if (playerLocation is AnimalHouse)
            {

                spawnIndex["hay"] = true;

            }
            else if (playerLocation is Caldera)
            {

                spawnIndex["fishspot"] = true;

            }
            else if (playerLocation is Shed)
            {

                spawnIndex["machine"] = true;
                spawnIndex["teahouse"] = true;
            }
            else
            {
                return new();

            }

            if (playerLocation.Map.Layers[0].LayerWidth * playerLocation.Map.Layers[0].LayerHeight > 2000)
            {

                spawnIndex["treasure"] = true;

            }

            return spawnIndex;

        }

    }


}
