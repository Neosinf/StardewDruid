using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;
using StardewValley.Locations;
using System.Linq;
using System.Collections;
using StardewValley.Menus;

namespace StardewDruid.Map
{
    static class Spawn
    {

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
                    ["meteor"] = true,
                    ["fishspot"] = false,

                };

            }
            else if (playerLocation.Name.Contains("Forest") || playerLocation.Name.Contains("Mountain"))
            {

                spawnIndex = new()
                {

                    ["forage"] = true,
                    ["flower"] = false,
                    ["grass"] = true,
                    ["trees"] = true,
                    ["fishup"] = true,
                    ["wilderness"] = false,
                    ["meteor"] = true,
                    ["fishspot"] = true,

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
                    ["meteor"] = true,
                    ["fishspot"] = false,

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
                    ["meteor"] = true,
                    ["fishspot"] = true,

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
                    ["meteor"] = true,
                    ["fishspot"] = true,

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
                    ["meteor"] = true,
                    ["fishspot"] = false,

                };

            }
            else if (playerLocation.Name.Contains("Beach") || playerLocation.Name.Contains("Town") || playerLocation.Name.Contains("Bus"))
            {

                spawnIndex = new()
                {

                    ["forage"] = true,
                    ["flower"] = false,
                    ["grass"] = false,
                    ["trees"] = false,
                    ["fishup"] = true,
                    ["wilderness"] = false,
                    ["meteor"] = false,
                    ["fishspot"] = true,

                };

            }
            else
            {
                spawnIndex = new();

            }

            return spawnIndex;

        }

        /*public static TemporaryAnimatedSprite ChallengeAnimation(string effect)
        {

            GameLocation location = Game1.getLocationFromName(ChallengeLocation(effect));

            Vector2 vector = ChallengeVector(effect);

            Color color = ChallengeColor(effect);

            TemporaryAnimatedSprite animation = ModUtility.AnimateChallenge(location,vector,color);

            return animation;

        }

        public static Color ChallengeColor(string effect)
        {

            Dictionary<string, Color> colorIndex = new()
            {
                ["challengeEarth"] = Color.Green,
                ["challengeWater"] = Color.Blue,
                ["challengeStars"] = Color.Red,
                ["swordEarth"] = Color.Green,
                ["swordWater"] = Color.Blue,
                ["swordStars"] = Color.Red,
            };

            return colorIndex[effect];

        }

        public static Dictionary<string, List<string>> ChallengeLocations()
        {

            Dictionary<string, string> locationIndex = ChallengeLocationIndex();

            Dictionary<string, List<string>> locationList = new();

            foreach (KeyValuePair<string, string> location in locationIndex)
            {

                if (!locationList.ContainsKey(location.Value))
                {

                    locationList[location.Value] = new();

                }

                locationList[location.Value].Add(location.Key);

            }

            return locationList;

        }*/

    }

}
