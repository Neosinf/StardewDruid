using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;
using StardewValley.Locations;
using System.Linq;
using System.Collections;
using StardewValley.Menus;

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

        public static Dictionary<string, bool> SpawnIndex(GameLocation playerLocation)
        {

            Dictionary<string, bool> spawnIndex;

            if (playerLocation.Name == "Farm")
            {
                spawnIndex = new()
                {

                    ["forage"] = true,
                    ["flower"] = true,
                    ["grass"] = true,
                    ["trees"] = true,
                    ["fishup"] = true,
                    ["wilderness"] = false,
                    ["critter"] = true,
                    ["meteor"] = true,
                    ["fishspot"] = false,
                    ["stonecut"] = true,
                    ["cropseed"] = true,

                };

            }
            else if (playerLocation.Name.Contains("Forest") || playerLocation.Name.Contains("Mountain"))
            {

                spawnIndex = new()
                {

                    ["forage"] = true,
                    ["flower"] = true,
                    ["grass"] = true,
                    ["trees"] = true,
                    ["fishup"] = true,
                    ["wilderness"] = false,
                    ["critter"] = true,
                    ["meteor"] = true,
                    ["fishspot"] = true,
                    ["stonecut"] = true,
                    ["cropseed"] = false,

                };

            }
            else if (playerLocation.Name.Contains("Bus"))
            {

                spawnIndex = new()
                {

                    ["forage"] = true,
                    ["flower"] = true,
                    ["grass"] = true,
                    ["trees"] = true,
                    ["fishup"] = true,
                    ["wilderness"] = false,
                    ["critter"] = true,
                    ["meteor"] = true,
                    ["fishspot"] = true,
                    ["stonecut"] = true,
                    ["cropseed"] = false,

                };

            }
            else if (playerLocation.Name.Contains("RailRoad"))
            {

                spawnIndex = new()
                {

                    ["forage"] = true,
                    ["flower"] = true,
                    ["grass"] = true,
                    ["trees"] = true,
                    ["fishup"] = false,
                    ["wilderness"] = true,
                    ["critter"] = true,
                    ["meteor"] = true,
                    ["fishspot"] = false,
                    ["stonecut"] = true,
                    ["cropseed"] = false,

                };

            }
            else if (playerLocation.Name.Contains("Backwoods") || playerLocation is Woods)
            {
                spawnIndex = new()
                {

                    ["forage"] = true,
                    ["flower"] = true,
                    ["grass"] = false,
                    ["trees"] = false,
                    ["fishup"] = false,
                    ["wilderness"] = true,
                    ["critter"] = true,
                    ["meteor"] = true,
                    ["fishspot"] = true,
                    ["stonecut"] = false,
                    ["cropseed"] = false,

                };
            }
            else if (playerLocation.Name.Contains("Desert"))
            {
                spawnIndex = new()
                {

                    ["forage"] = false, // no lawn anyway
                    ["flower"] = false,
                    ["grass"] = true, // a miracle!
                    ["trees"] = true,
                    ["fishup"] = true,
                    ["wilderness"] = true,
                    ["critter"] = true,
                    ["meteor"] = true,
                    ["fishspot"] = true,
                    ["stonecut"] = false,
                    ["cropseed"] = false,

                };
            }
            else if (playerLocation is MineShaft || playerLocation.Name.Contains("FarmCave") || playerLocation.Name.Contains("Mine"))
            {
                spawnIndex = new()
                {

                    ["forage"] = false,
                    ["flower"] = false,
                    ["grass"] = false,
                    ["trees"] = false,
                    ["fishup"] = false,
                    ["wilderness"] = false,
                    ["critter"] = false,
                    ["meteor"] = true,
                    ["fishspot"] = false,
                    ["stonecut"] = false,
                    ["cropseed"] = false,

                };

            }
            else if (playerLocation.Name.Contains("Beach"))
            {

                spawnIndex = new()
                {

                    ["forage"] = true,
                    ["flower"] = false,
                    ["grass"] = false,
                    ["trees"] = false,
                    ["fishup"] = true,
                    ["wilderness"] = false,
                    ["critter"] = true,
                    ["meteor"] = false,
                    ["fishspot"] = true,
                    ["stonecut"] = false,
                    ["cropseed"] = false,

                };

            }
            else if (playerLocation.Name.Contains("Town"))
            {

                spawnIndex = new()
                {

                    ["forage"] = true,
                    ["flower"] = false,
                    ["grass"] = false,
                    ["trees"] = false,
                    ["fishup"] = false,
                    ["wilderness"] = false,
                    ["critter"] = false,
                    ["meteor"] = false,
                    ["fishspot"] = true,
                    ["stonecut"] = false,
                    ["cropseed"] = false,

                };

            }
            else
            {
                spawnIndex = new();

            }

            return spawnIndex;

        }

    }

}
