using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;
using StardewValley.Locations;
using System.Linq;
using System.Collections;
using StardewValley.Menus;
using System.Drawing;
using xTile.Dimensions;
using xTile.Tiles;

namespace StardewDruid.Map
{
    static class SpawnData
    {

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

        public static List<int> GrizzleList()
        {
            List<int> grizzleIndex = new()
            {
                92, // Sap
                766, // Slime
                311, // PineCone
                310, // MapleSeed
                309, // Acorn
                292, // Mahogany
                767, // BatWings
                420, // RedMushroom
            };

            return grizzleIndex;
        }

        public static List<int> SashimiList()
        {

            List<int> sashimiIndex = new()
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
                424, // Cheese
                24, // Parsnip
            };

            return sashimiIndex;

        }

        public static Dictionary<int, int> CoffeeList()
        {

            Dictionary<int, int> coffeeList = new()
            {
                [167] = 60000,
                [433] = 20000,
                [395] = 90000,
                [253] = 300000,

            };

            return coffeeList;

        }


        public static Dictionary<int, int> CropList(GameLocation location)
        {

            Dictionary<int, int> objectIndexes;

            string season = Game1.currentSeason;

            if (location.isGreenhouse.Value)
            {

                season = "greenhouse";

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
                        [5] = 299, // amaranth
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
                    };

                    break;

            }

            int randomFlower = objectIndexes[Game1.random.Next(objectIndexes.Count)];

            return randomFlower;

        }
        public static int RandomForage()
        {
            Dictionary<int, int> randomCrops;

            int randomCrop;

            switch (Game1.currentSeason)
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

                    //[0] = 152, // seaweed
                    //[1] = 152, // seaweed
                    //[2] = 152, // seaweed
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
            else
            {

                objectIndexes = new Dictionary<int, int>()
                {

                    //[0] = 153, // algae
                    //[1] = 153, // algae
                    //[2] = 153, // algae
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

        public static int RandomHighFish(GameLocation location,bool enableRare)
        {
            
            Dictionary<int, int> objectIndexes;

            int seasonStar;

            switch (location.GetType().ToString())
            {

                case "Beach":

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

                    break;

                case "Woods":
                case "Desert":

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

                    break;

                default: // default

                    switch (Game1.currentSeason)
                    {
                        case "spring":
                        case "fall":
                            seasonStar = 143;
                            break;
                        default:
                            seasonStar = 698;
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

                    break;

            }

            int randomFish = objectIndexes[Game1.random.Next(objectIndexes.Count)];

            return randomFish;

        }

        public static int RandomJumpFish(GameLocation location)
        {

            int fishIndex;

            switch (location.GetType().ToString())
            {

                case "Beach":
                    fishIndex = 836;  // stingray
                    break;

                case "Woods":
                case "Desert":

                    fishIndex = 161; // ice pip
                    break;

                default: // default

                    fishIndex = 699; // tiger trout
                    break;

            }

            return fishIndex;

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

        public static Dictionary<string, bool> SpawnTemplate()
        {
            Dictionary<string, bool> spawnTemplate = new()
            {

                ["forage"] = false,
                ["flower"] = false,
                ["grass"] = false,
                ["trees"] = false,
                ["fishup"] = false,
                ["portal"] = false,
                ["critter"] = false,
                ["fishspot"] = false,
                ["cropseed"] = false,
                ["mushroom"] = false,
                ["rockfall"] = false,

            };

            return spawnTemplate;

        }

        public static Dictionary<string, bool> SpawnIndex(GameLocation playerLocation)
        {

            Dictionary<string, bool> spawnIndex;

            spawnIndex = SpawnTemplate();

            if (playerLocation is Farm || playerLocation.Name == "Custom_Garden")
            {

                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["trees"] = true;
                spawnIndex["fishup"] = true;
                spawnIndex["critter"] = true;
                spawnIndex["cropseed"] = true;
                spawnIndex["mushroom"] = true;

            }
            else if (playerLocation.isGreenhouse.Value)
            {

                spawnIndex["cropseed"] = true;

            }
            else if (playerLocation is Forest ||  playerLocation is Mountain || playerLocation is Desert || playerLocation.Name.Contains("Bus"))
            {
                
                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["trees"] = true;
                spawnIndex["fishup"] = true;
                spawnIndex["critter"] = true;
                spawnIndex["fishspot"] = true;

            }
            else if (playerLocation.Name.Contains("Backwoods") || playerLocation.Name.Contains("RailRoad"))
            {

                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["trees"] = true;
                spawnIndex["critter"] = true;
                spawnIndex["portal"] = true;

            }
            else if (playerLocation is Woods || playerLocation.Name.Contains("Grampleton"))
            {
                
                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["critter"] = true;
                spawnIndex["portal"] = true;
                spawnIndex["fishspot"] = true;

            }
            else if (playerLocation is MineShaft) //|| playerLocation.Name.Contains("Mine"))
            {
                
                spawnIndex["rockfall"] = true;

            }
            else if (playerLocation is Beach)
            {

                spawnIndex["critter"] = true;
                spawnIndex["fishup"] = true;
                spawnIndex["fishspot"] = true;

            }
            else if (playerLocation is Town)
            {
                
                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["fishup"] = true;

            }
            else if (playerLocation.Name.Contains("DeepWoods"))
            {
                
                spawnIndex["critter"] = true;
                spawnIndex["fishspot"] = true;

            }
            else if (playerLocation.Name is "Coop" || playerLocation.Name is "Barn")
            {

                spawnIndex["hay"] = true;

            }
            else
            {
                return new();

            }

            return spawnIndex;

        }

    }


}
