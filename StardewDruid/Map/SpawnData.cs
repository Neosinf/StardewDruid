using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;
using StardewValley.Locations;
using System.Linq;
using System.Collections;
using StardewValley.Menus;
using System.Drawing;

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
                167, // JojaCola
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
            };

            return sashimiIndex;

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

            if (playerLocation.Name == "Farm" || playerLocation.Name == "Custom_Garden")
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
            else if (playerLocation.Name.Contains("Forest") || playerLocation.Name.Contains("Mountain") || playerLocation.Name.Contains("Desert"))
            {
                
                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["trees"] = true;
                spawnIndex["fishup"] = true;
                spawnIndex["critter"] = true;
                spawnIndex["fishspot"] = true;

            }
            else if (playerLocation.Name.Contains("Bus"))
            {

                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["trees"] = true;
                spawnIndex["critter"] = true;

            }
            else if (playerLocation.Name.Contains("RailRoad"))
            {

                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["trees"] = true;
                spawnIndex["critter"] = true;
                spawnIndex["portal"] = true;

            }
            else if (playerLocation.Name.Contains("Backwoods") || playerLocation is Woods || playerLocation.Name.Contains("Grampleton"))
            {
                
                spawnIndex["forage"] = true;
                spawnIndex["flower"] = true;
                spawnIndex["grass"] = true;
                spawnIndex["critter"] = true;
                spawnIndex["portal"] = true;
                spawnIndex["fishspot"] = true;

            }
            else if (playerLocation is MineShaft || playerLocation.Name.Contains("Mine"))
            {
                
                spawnIndex["rockfall"] = true;

            }
            else if (playerLocation.Name.Contains("Beach"))
            {

                spawnIndex["critter"] = true;
                spawnIndex["fishup"] = true;
                spawnIndex["fishspot"] = true;

            }
            else if (playerLocation.Name.Contains("Town"))
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
            else
            {
                return null;

            }

            return spawnIndex;

        }

    }


}
