﻿using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Weald;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Enchantments;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using xTile.Dimensions;
using static StardewDruid.Journal.HerbalData;

namespace StardewDruid.Data
{
    public static class SpawnData
    {

        public enum drops
        {
            none,
            bat,
            shadow,
            slime,
            phantom,
            seafarer,
            dragon,
            dust,
            rex,
        }

        public enum swords
        {
            forest,
            neptune,
            holy,
            lava,
            scythe,
            cutlass,
            knife,
        }

        public static MeleeWeapon SpawnSword(swords sword)
        {

            switch (sword)
            {
                default:
                case swords.forest:

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("DaLion.Overhaul"))
                    {
                        return new MeleeWeapon("44");
                    }

                    return new MeleeWeapon("15");

                case swords.neptune:

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("DaLion.Overhaul"))
                    {
                        return new MeleeWeapon("7");
                    }

                    return new MeleeWeapon("14");

                case swords.holy:

                    StardewValley.Tools.MeleeWeapon holyBlade = new("3");

                    holyBlade.enchantments.Add(new BugKillerEnchantment());

                    holyBlade.enchantments.Add(new CrusaderEnchantment());

                    RubyEnchantment powerEnchantment = new();

                    powerEnchantment.SetLevel(holyBlade, 3);

                    holyBlade.enchantments.Add(powerEnchantment);

                    return holyBlade;

                case swords.lava:

                    return new MeleeWeapon("9");

                case swords.scythe:

                    return new MeleeWeapon("53");

                case swords.cutlass:

                    return new MeleeWeapon("57");

                case swords.knife:

                    return new MeleeWeapon("16");
            }

        }

        public static Dictionary<int, Rite.rites> WeaponAttunement(bool reserved = false)
        {

            Dictionary<int, Rite.rites> weapons = new();

            if (!reserved)
            {
                weapons = Mod.instance.save.attunement;

            }

            weapons[15] = Rite.rites.weald;
            weapons[14] = Rite.rites.mists;
            weapons[3] = Rite.rites.stars;
            weapons[53] = Rite.rites.fates;
            weapons[57] = Rite.rites.ether;
            weapons[16] = Rite.rites.bones;

            if (Mod.instance.Helper.ModRegistry.IsLoaded("DaLion.Overhaul"))
            {
                weapons[44] = Rite.rites.weald;
                weapons[7] = Rite.rites.mists;

            }

            return weapons;

        }

        public static void MonsterDrops(StardewValley.Monsters.Monster monster, drops drop)
        {

            switch (drop)
            {

                case drops.bat:

                    monster.objectsToDrop.Clear();

                    monster.objectsToDrop.Add("767");

                    if (Game1.random.Next(3) == 0)
                    {
                        monster.objectsToDrop.Add("767");
                    }
                    else if (Game1.random.Next(4) == 0 && Mod.instance.PowerLevel >= 3)
                    {
                        monster.objectsToDrop.Add("767");
                    }
                    else if (Game1.random.Next(5) == 0 && Mod.instance.PowerLevel >= 5)
                    {
                        List<string> batElixers = new()
                        {
                            "772","773","879",
                        };

                        monster.objectsToDrop.Add(batElixers[Game1.random.Next(batElixers.Count)]);

                    }

                    break;

                case drops.shadow:

                    monster.objectsToDrop.Clear();

                    monster.objectsToDrop.Add("769");

                    if (Game1.random.Next(3) == 0)
                    {
                        monster.objectsToDrop.Add("768");
                    }
                    else if (Game1.random.Next(4) == 0 && Mod.instance.PowerLevel >= 3)
                    {
                        List<string> shadowGems = new()
                        {
                            "62","66","68","70",
                        };

                        monster.objectsToDrop.Add(shadowGems[Game1.random.Next(shadowGems.Count)]);

                    }
                    else if (Game1.random.Next(5) == 0 && Mod.instance.PowerLevel >= 5)
                    {
                        List<string> shadowGems = new()
                        {
                            "60","64","72",
                        };

                        monster.objectsToDrop.Add(shadowGems[Game1.random.Next(shadowGems.Count)]);
                    }

                    break;

                case drops.slime:


                    monster.objectsToDrop.Add("766");

                    if (Game1.random.Next(3) == 0)
                    {
                        monster.objectsToDrop.Add("766");
                    }
                    else if (Game1.random.Next(4) == 0 && Mod.instance.PowerLevel >= 3)
                    {
                        monster.objectsToDrop.Add("766");

                    }
                    else if (Game1.random.Next(5) == 0 && Mod.instance.PowerLevel >= 5)
                    {
                        List<string> slimeSyrups = new()
                        {
                            "724","725","726","247","184","419",
                        };

                        monster.objectsToDrop.Add(slimeSyrups[Game1.random.Next(slimeSyrups.Count)]);
                    }

                    break;

                case drops.phantom:

                    monster.objectsToDrop.Clear();

                    if (Game1.random.Next(3) == 0)
                    {
                        monster.objectsToDrop.Add("378");
                    }
                    else if (Game1.random.Next(4) == 0 && Mod.instance.PowerLevel >= 3)
                    {
                        monster.objectsToDrop.Add("380");
                    }
                    else if (Game1.random.Next(5) == 0 && Mod.instance.PowerLevel >= 4)
                    {
                        monster.objectsToDrop.Add("384");
                    }
                    else if (Game1.random.Next(6) == 0 && Mod.instance.PowerLevel >= 5)
                    {
                        monster.objectsToDrop.Add("386"); // iridium
                    }

                    break;

                case drops.seafarer:

                    monster.objectsToDrop.Clear();

                    switch (Game1.random.Next(4))
                    {
                        default:
                        case 0: monster.objectsToDrop.Add("797"); break;

                        case 1: monster.objectsToDrop.Add("166"); break;

                        case 2: monster.objectsToDrop.Add("265"); break;

                        case 3: monster.objectsToDrop.Add("275"); break;

                    }

                    break;

                case drops.dust:

                    monster.objectsToDrop.Clear();

                    if (Game1.random.Next(3) == 0)
                    {
                        monster.objectsToDrop.Add("382"); // coal

                    }
                    else if (Game1.random.Next(4) == 0 && Mod.instance.PowerLevel >= 3)
                    {
                        monster.objectsToDrop.Add("395"); // coffee (edible)

                    }
                    else if (Game1.random.Next(5) == 0 && Mod.instance.PowerLevel >= 4)
                    {
                        monster.objectsToDrop.Add("251"); // tea sapling
                    }

                    break;

                case drops.rex:

                    monster.objectsToDrop.Clear();

                    List<string> dropList = new()
                    {
                        "580", //: "Prehistoric Tibia/100/-300/Arch/Prehistoric Tibia/A thick and sturdy leg bone./Forest .01//",
                        "583", //: "Prehistoric Rib/100/-300/Arch/Prehistoric Rib/Little gouge marks on the side suggest that this rib was someone's dinner./Farm .01//",
                        "584", //: "Prehistoric Vertebra/100/-300/Arch/Prehistoric Vertebra/A segment of some prehistoric creature's spine./BusStop .01//",

                    };

                    monster.objectsToDrop.Add(dropList[Mod.instance.randomIndex.Next(dropList.Count)]);

                    if (Game1.random.Next(3) == 0 && Mod.instance.PowerLevel >= 3)
                    {
                        monster.objectsToDrop.Add("107"); // dino egg

                    }
                    else if (Game1.random.Next(4) == 0 && Mod.instance.PowerLevel >= 4)
                    {
                        monster.objectsToDrop.Add("72"); // diamond
                    }

                    break;

            }

            return;

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
                343, // debris stone
                450, // debris stone
                169,
                818,
                25,

            };

            return stoneIndex;

        }

        public static Tree RandomTree(GameLocation location)
        {
            List<int> treeIndex;

            if (location is Desert || location is IslandLocation)
            {

                treeIndex = new()
                    {
                        6,8,
                    };

            }
            else if (location is IslandLocation)
            {

                treeIndex = new()
                    {
                        8,9,
                    };

            }
            else
            {

                treeIndex = new()
                    {
                        1,2,3,1,2,3,8,7
                    };

                /*if (location is Farm)
                {

                    treeIndex.Add(7);

                }*/

                if(Game1.player.ForagingLevel > 5)
                {

                    treeIndex.Add(10);
                    treeIndex.Add(11);
                    treeIndex.Add(12);

                }

                if (Game1.player.ForagingLevel > 10)
                {

                    treeIndex.Add(13);

                }

            };

            return new(treeIndex[Game1.random.Next(treeIndex.Count)].ToString(), 1);

        }

        public static List<int> RockFall(GameLocation location, Farmer player, int specialChance = 10)
        {

            List<int> rockList = new();

            int objectIndex;

            int scatterIndex;

            int debrisIndex;

            Dictionary<int, int> objectIndexes;

            Dictionary<int, int> specialIndexes;

            int oreLevel = 5;

            if (location is MineShaft shaftLocation)
            {

                if (shaftLocation.mineLevel <= 40)
                {

                    oreLevel = 1;

                }
                else if (shaftLocation.mineLevel <= 80)
                {

                    oreLevel = 2;

                }
                else if (shaftLocation.mineLevel <= 120)
                {

                    oreLevel = 3;
                }
                else // Skull Cavern
                {
                    oreLevel = 4;

                }

            }
            else if(location is Spring)
            {

                oreLevel = 1;

            }
            else if(location is Vault)
            {

                oreLevel = 3;

            }
            else if (location is Tomb)
            {

                oreLevel = 4;

            }

            switch (oreLevel)
            {

                default:
                case 1:

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

                    break;


                case 2:

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

                    break;



                case 3:

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

                    break;

                case 4:

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

                    break;

                case 5:

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

                    break;

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

        public static List<string> CropList(GameLocation location)
        {

            List<string> objectIndexes;

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
                        "478", // rhubarb
                        "476", // garlic
                        "433", // coffee
                        "745", // strawberry
                        "473", // bean
                        //"477", // kale
                        "CarrotSeeds",
                    };

                    break;

                case "summer":
                case "greenhouse":

                    objectIndexes = new()
                    {
                        "479", // melon
                        "485", // red cabbage
                        "433", // coffee
                        "481", // blueberry
                        "302", // hops
                        "483", // wheat
                        "SummerSquashSeeds",
                    };

                    break;

                case "fall":

                    objectIndexes = new()
                    {
                        "490", // pumpkin
                        "492", // yam
                        "299", // amaranth
                        "493", // cranberry
                        "301", // grape
                        "BroccoliSeeds",
                    };

                    break;

                case "island":

                    objectIndexes = new()
                    {
                        "833", // pineapple
                        "831", // taro
                        "486", // starfruit
                        "829", // ginger

                    };

                    break;

                default:

                    objectIndexes = new()
                    {
                        "PowdermelonSeeds",

                    };

                    break;

            }

            return objectIndexes;

        }

        public static string SeasonalSeed(GameLocation location)
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdOne) || Mod.instance.ModDifficulty() > 10)
            {

                return null;

            }

            if (Mod.instance.randomIndex.Next(500) == 0)
            {

                return "114";

            }

            if(Mod.instance.randomIndex.Next(5) == 0)
            {

                string item = "(O)498";

                switch (Game1.currentSeason)
                {

                    case "spring":

                        item = "(O)495";

                        break;

                    case "summer":

                        item = "(O)496";

                        break;

                    case "fall":

                        item = "(O)497";

                        break;

                }

                return item;

            }

            return null;

        }

        public static int RandomFlower(GameLocation location)
        {

            Dictionary<int, int> objectIndexes;

            string season = Game1.currentSeason;

            if (location is Beach || location is StardewDruid.Location.Atoll || location is IslandWest || location is IslandSouth || location is IslandSouthEast)
            {

                season = "beach";

            }

            switch (season)
            {

                case "beach":

                    objectIndexes = new()
                    {
                        [0] = 392, // shell
                        [1] = 394, // shell
                    };

                    break;

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

                case "winter":

                    objectIndexes = new()
                    {
                        [0] = 421, // crocus
                        [1] = 421, // crocus
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

        public static bool ForageCheck(StardewValley.Object obj)
        {

            if(
                obj.isForage() || 
                obj.isAnimalProduct() || 
                obj.QualifiedItemId == "(O)398"
            )
            {

                return true;

            }

            return false;

        }

        public static int RandomForage(GameLocation location)
        {
            Dictionary<int, int> randomCrops;

            int randomCrop;

            string season = Game1.currentSeason;

            if (location is IslandEast || location is Woods)
            {

                season = "woods";

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
                    if (Game1.currentSeason == "winter" && location is Woods)
                    {
                        randomCrops[3] = 416;
                    }

                    randomCrop = randomCrops[Game1.random.Next(randomCrops.Count)];

                    break;

                default: //"fall":

                    randomCrop = 404 + Game1.random.Next(4) * 2;

                    break;

            }

            return randomCrop;

        }

        public static string RandomRock(GameLocation location)
        {

            if (Mod.instance.randomIndex.Next(100) == 0)
            {

                return "(O)46";

            }

            if (Mod.instance.randomIndex.Next(20) == 0)
            {


                return "(O)44";

            }

            if (Mod.instance.randomIndex.Next(2) == 0)
            {
                
                return "(O)343";

            }

            return "(O)450";

        }

        public static string RandomBeach(GameLocation location)
        {

            List<string> spawn = new() {
                "(O)80", // "Quartz", 
                "(O)86", // "Earth Crystal", 
                "(O)84", // "Frozen Tear",
                "(O)881", // "Bone Fragment", 
                "(O)168", // "Trash", 
                "(O)169", // "Driftwood", 
                "(O)170", // "Broken Glasses", 
                "(O)171", // "Broken CD", 
                "(O)152", // "Algae", 
                "(O)153", // "Seaweed", 
                "(O)157", // "White Algae", 
                "(O)167", // "Joja Cola", 
                "(O)718", // "Cockle", 
                "(O)719", // "Mussel", 
                "(O)720", // "Shrimp", 
                "(O)721", // "Snail", 
                "(O)722", // "Periwinkle", 
                "(O)723", // "Oyster", 
                "(O)372", // "Clam", 
                "(O)393",
            };
            return spawn[Game1.random.Next(spawn.Count)];

        }

        public static string RandomTwig(GameLocation location)
        {

            return "(O)" + (294 + Mod.instance.randomIndex.Next(2)).ToString();

        }

        public static int ForestWaterCheck(GameLocation location)
        {

            if(location is not Forest)
            {
                
                return 0;

            }

            int Y = (int)Game1.player.Tile.Y;

            if (Y > 100)
            {

                return 3;

            } else if (Y > 50)
            {

                return 2;

            }

            return 1;

        }

        public static string RandomLowFish(GameLocation location)
        {
            List<string> indexes = new();


            int forest = ForestWaterCheck(location);

            if (location is Beach || location is Atoll || forest == 3)
            {

                if (Game1.isRaining)
                {

                    indexes.Add("150"); // red snapper
                    if (Mod.instance.randomIndex.Next(3) == 0) 
                    { 
                        
                        indexes.Add("SeaJelly"); 
                    
                    } 

                }

                switch (Game1.currentSeason)
                {

                    case "spring":
                        indexes.Add("147"); // herring
                        indexes.Add("129"); // anchovy
                        break;
                    case "summer":
                        indexes.Add("701"); // tilapia
                        indexes.Add("131"); // sardine
                        break;
                    case "fall":
                        indexes.Add("129"); // anchovy
                        indexes.Add("131"); // sardine
                        break;
                    case "winter":
                        indexes.Add("131"); // sardine
                        indexes.Add("147"); // herring
                        break;

                }

                //indexes.Add("152"); // Seaweed

            }
            else if (location is IslandLocation)
            {

                if (Game1.isRaining)
                {

                    indexes.Add("150"); // red snapper
                    if (Mod.instance.randomIndex.Next(3) == 0)
                    {
                        indexes.Add("SeaJelly");
                    }

                }

                indexes.Add("838"); // blue discuss
                indexes.Add("837"); // lionfish

            }
            else if (location is Town || location is Forest)
            {
                
                if(Game1.timeOfDay >= 1700)
                {

                    indexes.Add("132"); // bream
                    if (Mod.instance.randomIndex.Next(3) == 0)
                    {
                        indexes.Add("RiverJelly");
                    }
                }

                switch (Game1.currentSeason)
                {

                    case "spring":

                        if(forest != 1)
                        {
                            indexes.Add("137"); // smallmouth bass

                        }

                        indexes.Add("145"); // sunfish
                        break;
                    case "summer":
                        indexes.Add("145"); // sunfish
                        indexes.Add("138"); // rainbow trout
                        break;

                    case "fall":

                        if (forest != 1)
                        {
                            indexes.Add("137"); // smallmouth bass

                        }
                        indexes.Add("141"); // perch
                        break;

                    case "winter":
                        indexes.Add("141"); // perch
                        break;

                }

                //indexes.Add("153"); // Green Algae

            }
            else
            {

                if (Game1.timeOfDay >= 1700)
                {

                    indexes.Add("132"); // bream
                    if (Mod.instance.randomIndex.Next(3) == 0)
                    {
                        indexes.Add("CaveJelly");
                    }

                }

                indexes.Add("142"); // carp

                //indexes.Add("157"); // White Algae

            }

            string randomFish = indexes[Game1.random.Next(indexes.Count)];

            return randomFish;

        }

        public static string RandomHighFish(GameLocation location, bool enableRare)
        {

            Dictionary<int, int> objectIndexes;

            int seasonStar;

            int forest = ForestWaterCheck(location);

            if (location is Woods || location is Desert || location is Caldera || location is MineShaft)
            {

                switch (Game1.currentSeason)
                {
                    case "spring":
                        seasonStar = 734; // woodskip
                        break;
                    case "fall":
                    case "winter":
                        seasonStar = 161; // icepip
                        break;
                    default:
                        seasonStar = 162; // lava eel
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
            else if (location is Beach || location.Name.Contains("Beach") || location is IslandLocation || location is Atoll || forest == 3)
            {

                switch (Game1.currentSeason)
                {
                    case "spring":
                    case "fall":
                        seasonStar = 148; // eel
                        break;
                    case "winter":
                        seasonStar = 151; // octopus
                        break;
                    default:
                        seasonStar = 155; // super cucumber
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
                    [4] = seasonStar,
                    //158, // stone fish
                    [5] = seasonStar,
                    [6] = seasonStar,

                };

                if (enableRare)
                {
                    objectIndexes[7] = 269;  // midnight carp

                }

            }

            int randomFish = objectIndexes[Game1.random.Next(objectIndexes.Count)];

            return randomFish.ToString();

        }

        public static string RandomJunkItem(GameLocation location)
        {

            Dictionary<int, int> objectIndexes = new ()
            {

                [0] = 275, //"Artifact Trove/0/-300/Basic/Artifact Trove/A blacksmith can open this for you. These troves often contain ancient relics and curiosities./100 101 103 104 105 106 108 109 110 111 112 113 114 115 116 117 118 119 120 121 122 123 124 125 166 373 797//",
                [1] = 167, //"Joja Cola/25/5/Fish -20/Joja Cola/The flagship product of Joja corporation./drink/0 0 0 0 0 0 0 0 0 0 0/0",
                [2] = 167, //"Joja Cola/25/5/Fish -20/Joja Cola/The flagship product of Joja corporation./drink/0 0 0 0 0 0 0 0 0 0 0/0",
            };
        
            if(location is Beach || location is IslandLocation || location is Atoll)
            {

                objectIndexes = new()
                {
                    [0] = 397, // urchin
                    [1] = 394, //"Rainbow Shell/300/-300/Basic -23/Rainbow Shell/It's a very beautiful shell.///",
                    [2] = 393, //"Coral/80/-300/Basic -23/Coral/A colony of tiny creatures that clump together to form beautiful structures.///",
                };

            }

            if (location is Caldera)
            {

                objectIndexes = new Dictionary<int, int>()
                {
                    [0] = 848, // cinder shard,
                    [1] = 848, // cinder shard,
                    [2] = 167, //"Joja Cola/25/5/Fish -20/Joja Cola/The flagship product of Joja corporation./drink/0 0 0 0 0 0 0 0 0 0 0/0",
                };

            }

            int randomFish = objectIndexes[Game1.random.Next(objectIndexes.Count)];

            return randomFish.ToString();

        }

        public static int RandomTrash(GameLocation location)
        {

            Dictionary<int, int> artifactIndexes = new()
            {
                [0] = 105,
                [1] = 106,
                [2] = 110,
                [3] = 111,
                [4] = 112,
                [5] = 115,
                [6] = 117,
            };

            Dictionary<int, int> objectIndexes = new()
            {
                [0] = artifactIndexes[Mod.instance.randomIndex.Next(7)],
                [1] = 167,
                [2] = 168,
                [3] = 169,
                [4] = 170,
                [5] = 171,
                [6] = 172,
            };

            int objectIndex = objectIndexes[Mod.instance.randomIndex.Next(7)];

            return objectIndex;

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
            else if (location is Beach || location is Atoll)
            {
                fishIndex = 705; // puff ball 
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

        public static string RandomPoolFish(GameLocation location)
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

            return objectIndex.ToString();

        }

        public static string RandomBushForage()
        {

            int seasonal = 414; // crystal fruit

            switch (Game1.currentSeason)
            {

                case "spring":

                    seasonal = 296; // salmonberry

                    break;

                case "summer":

                    seasonal = 398; // grape

                    break;

                case "fall":

                    seasonal = 410; // blackberry

                    break;

            }
            Dictionary<int, int> objectIndexes = new()
            {

                [0] = 404, // 404 mushroom
                [1] = seasonal,
                [2] = seasonal,
                [3] = seasonal,

            };

            if (Mod.instance.randomIndex.Next(3) == 0)
            {
                objectIndexes[4] = 257;
            }

            int objectIndex = objectIndexes[new Random().Next(objectIndexes.Count)];

            return "(O)"+objectIndex.ToString();
        }
        
        public static string RandomSeasonalFruit()
        {

            List<string> indexes = new();

            switch (Game1.currentSeason)
            {

                case "spring":

                    indexes.Add("(O)634"); // apricot
                    indexes.Add("(O)638"); // cherry
                    indexes.Add("(O)296"); // salmonberry
                    indexes.Add("(O)296"); // salmonberry
                    break;

                case "summer":

                    indexes.Add("(O)636"); // peach
                    indexes.Add("(O)635"); // orange
                    indexes.Add("(O)398"); // grape
                    indexes.Add("(O)398"); // grape
                    break;

                case "fall":

                    indexes.Add("(O)613"); // apple
                    indexes.Add("(O)637"); // pomegranite
                    indexes.Add("(O)410"); // blackberry
                    indexes.Add("(O)410"); // blackberry
                    break;

                case "winter":

                    return null;

            }

            string objectIndex = indexes[new Random().Next(indexes.Count)];

            return objectIndex;

        }

        public static StardewValley.Object RandomTreasure(GameLocation location, bool rareTreasure = false)
        {

            int options = 3;

            if (rareTreasure) { options++; }

            switch (Mod.instance.randomIndex.Next(options))
            {
                default:
                case 0:

                    string low =  RandomLowFish(location);

                    return new StardewValley.Object(low, 1, quality: 4);
                
                case 1:

                    string junk = RandomJunkItem(location);

                    return new StardewValley.Object(junk, Mod.instance.randomIndex.Next(1,4));

                case 2:

                    string pool = RandomPoolFish(location);

                    return new StardewValley.Object(pool, Mod.instance.randomIndex.Next(1,3));

                case 3:

                    string fish = RandomHighFish(location,true);

                    return new StardewValley.Object(fish, 1, quality: 4);

            }

        }

        public static void CrateTreasure(GameLocation location, Microsoft.Xna.Framework.Vector2 origin, Microsoft.Xna.Framework.Vector2 vector)
        {

            if(location is Vault)
            {

                switch (vector.Y)
                {
                    default:
                    case 1:
                        new ThrowHandle(Game1.player, origin, swords.lava).register();
                        return;

                    case 2:
                        new ThrowHandle(Game1.player, origin, new StardewValley.Object("336", 5)).register();
                        return;

                    case 3:
                        new ThrowHandle(Game1.player, origin, new StardewValley.Object("74", 1)).register();
                        return;

                }

            }

            switch (Mod.instance.randomIndex.Next(6))
            {
                
                default:
                case 0:
                    new ThrowHandle(Game1.player, origin, new StardewValley.Object("166", 1)).register();
                    return; //"Treasure Chest/5000/-300/Basic/Treasure Chest/Wow, it's loaded with treasure! This is sure to fetch a good price./Day Night^Spring Summer Fall Winter//",

                case 1:
                    new ThrowHandle(Game1.player, origin, new StardewValley.Object("336", 5)).register();
                    return; // Gold bars

                case 2:
                    new ThrowHandle(Game1.player, origin, new StardewValley.Object("74", 1)).register();
                    return; // Prismatic Shard

                case 3:
                    new ThrowHandle(Game1.player, origin, new StardewValley.Object("797", 1)).register();
                    return; //"Pearl/2500/-300/Basic/Pearl/A rare treasure from the sea.///",

                case 4:
                    new ThrowHandle(Game1.player, origin, new StardewValley.Object("852", 2)).register();
                    return; //"Dragon Tooth/500/-300/Basic/Dragon Tooth/These are rumored to be the teeth of ancient serpents. The enamel is made of pure iridium!///",

                case 5:
                    new ThrowHandle(Game1.player, origin, new StardewValley.Object("275", 3)).register();
                    return; //"Artifact Trove/0/-300/Basic/Artifact Trove/A blacksmith can open this for you. These troves often contain ancient relics and curiosities./100 101 103 104 105 106 108 109 110 111 112 113 114 115 116 117 118 119 120 121 122 123 124 125 166 373 797//",

            }

        }

        public static void LearnRecipe()
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

            int learnedRecipes = 0;

            foreach (string recipe in recipeList)
            {

                if (!Game1.player.cookingRecipes.ContainsKey(recipe))
                {

                    Game1.player.cookingRecipes.Add(recipe, 0);

                    learnedRecipes++;

                }

            }

            if (learnedRecipes >= 1)
            {

                Game1.addHUDMessage(new HUDMessage(learnedRecipes + " " + DialogueData.Strings(DialogueData.stringkeys.learnRecipes), 2));

            }

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

        public static int RandomBarbeque()
        {
            List<int> cookingList = new() {
                194, //"Fried Egg/35/20/Cooking -7/Fried Egg/Sunny-side up./food/0 0 0 0 0 0 0 0 0 0 0/0",
                195, //"Omelet/125/40/Cooking -7/Omelet/It's super fluffy./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //196, //"Salad/110/45/Cooking -7/Salad/A healthy garden salad./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //197, //"Cheese Cauliflower/300/55/Cooking -7/Cheese Cauliflower/It smells great!/food/0 0 0 0 0 0 0 0 0 0 0/0",
                198, //"Baked Fish/100/30/Cooking -7/Baked Fish/Baked fish on a bed of herbs./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //199, //"Parsnip Soup/120/34/Cooking -7/Parsnip Soup/It's fresh and hearty./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //200, //"Vegetable Medley/120/66/Cooking -7/Vegetable Medley/This is very nutritious./food/0 0 0 0 0 0 0 0 0 0 0/0",
                201, //"Complete Breakfast/350/80/Cooking -7/Complete Breakfast/You'll feel ready to take on the world!/food/2 0 0 0 0 0 0 50 0 0 0/600",
                202, //"Fried Calamari/150/32/Cooking -7/Fried Calamari/It's so chewy./food/0 0 0 0 0 0 0 0 0 0 0/0",
                203, //"Strange Bun/225/40/Cooking -7/Strange Bun/What's inside?/food/0 0 0 0 0 0 0 0 0 0 0/0",
                204, //"Lucky Lunch/250/40/Cooking -7/Lucky Lunch/A special little meal./food/0 0 0 0 3 0 0 0 0 0 0/960",
                //205, //"Fried Mushroom/200/54/Cooking -7/Fried Mushroom/Earthy and aromatic./food/0 0 0 0 0 0 0 0 0 0 0 2/600",
                //206, //"Pizza/300/60/Cooking -7/Pizza/It's popular for all the right reasons./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //207, //"Bean Hotpot/100/50/Cooking -7/Bean Hotpot/It sure is healthy./food/0 0 0 0 0 0 0 30 32 0 0/600",
                //208, //"Glazed Yams/200/80/Cooking -7/Glazed Yams/Sweet and satisfying... The sugar gives it a hint of caramel./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //209, //"Carp Surprise/150/36/Cooking -7/Carp Surprise/It's bland and oily./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //210, //"Hashbrowns/120/36/Cooking -7/Hashbrowns/Crispy and golden-brown!/food/1 0 0 0 0 0 0 0 0 0 0/480",
                //211, //"Pancakes/80/36/Cooking -7/Pancakes/A double stack of fluffy, soft pancakes./food/0 0 0 0 0 2 0 0 0 0 0/960",
                212, //"Salmon Dinner/300/50/Cooking -7/Salmon Dinner/The lemon spritz makes it special./food/0 0 0 0 0 0 0 0 0 0 0/0",
                213, //"Fish Taco/500/66/Cooking -7/Fish Taco/It smells delicious./food/0 2 0 0 0 0 0 0 0 0 0/600",
                214, //"Crispy Bass/150/36/Cooking -7/Crispy Bass/Wow, the breading is perfect./food/0 0 0 0 0 0 0 0 64 0 0/600",
                //215, //"Pepper Poppers/200/52/Cooking -7/Pepper Poppers/Spicy breaded peppers filled with cheese./food/2 0 0 0 0 0 0 0 0 1 0/600",
                //216, //"Bread/60/20/Cooking -7/Bread/A crusty baguette./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //218, //"Tom Kha Soup/250/70/Cooking -7/Tom Kha Soup/These flavors are incredible!/food/2 0 0 0 0 0 0 30 0 0 0/600",
                //219, //"Trout Soup/100/40/Cooking -7/Trout Soup/Pretty salty./food/0 1 0 0 0 0 0 0 0 0 0/400",
                //220, //"Chocolate Cake/200/60/Cooking -7/Chocolate Cake/Rich and moist with a thick fudge icing./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //221, //"Pink Cake/480/100/Cooking -7/Pink Cake/There's little heart candies on top./food/0 0 0 0 0 0 0 0 0 0 0/0",
                222, //"Rhubarb Pie/400/86/Cooking -7/Rhubarb Pie/Mmm, tangy and sweet!/food/0 0 0 0 0 0 0 0 0 0 0/0",
                //223, //"Cookie/140/36/Cooking -7/Cookie/Very chewy./food/0 0 0 0 0 0 0 0 0 0 0/0",
                224, //"Spaghetti/120/30/Cooking -7/Spaghetti/An old favorite./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //225, //"Fried Eel/120/30/Cooking -7/Fried Eel/Greasy but flavorful./food/0 0 0 0 1 0 0 0 0 0 0/600",
                226, //"Spicy Eel/175/46/Cooking -7/Spicy Eel/It's really spicy! Be careful./food/0 0 0 0 1 0 0 0 0 1 0/600",
                227, //"Sashimi/75/30/Cooking -7/Sashimi/Raw fish sliced into thin pieces./food/0 0 0 0 0 0 0 0 0 0 0/0",
                228, //"Maki Roll/220/40/Cooking -7/Maki Roll/Fish and rice wrapped in seaweed./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //229, //"Tortilla/50/20/Cooking -7/Tortilla/Can be used as a vessel for food or eaten by itself./food/0 0 0 0 0 0 0 0 0 0 0/0",
                230, //"Red Plate/400/96/Cooking -7/Red Plate/Full of antioxidants./food/0 0 0 0 0 0 0 50 0 0 0/300",
                //231, //"Eggplant Parmesan/200/70/Cooking -7/Eggplant Parmesan/Tangy, cheesy, and wonderful./food/0 0 1 0 0 0 0 0 0 0 3/400",
                //232, //"Rice Pudding/260/46/Cooking -7/Rice Pudding/It's creamy, sweet, and fun to eat./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //233, //"Ice Cream/120/40/Cooking -7/Ice Cream/It's hard to find someone who doesn't like this./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //234, //"Blueberry Tart/150/50/Cooking -7/Blueberry Tart/It's subtle and refreshing./food/0 0 0 0 0 0 0 0 0 0 0/0",
                235, //"Autumn's Bounty/350/88/Cooking -7/Autumn's Bounty/A taste of the season./food/0 0 0 0 0 2 0 0 0 0 2/660",
                //236, //"Pumpkin Soup/300/80/Cooking -7/Pumpkin Soup/A seasonal favorite./food/0 0 0 0 2 0 0 0 0 0 2/660",
                237, //"Super Meal/220/64/Cooking -7/Super Meal/It's a really energizing meal./food/0 0 0 0 0 0 0 40 0 1 0/300",
                //238, //"Cranberry Sauce/120/50/Cooking -7/Cranberry Sauce/A festive treat./food/0 0 2 0 0 0 0 0 0 0 0/300",
                //239, //"Stuffing/165/68/Cooking -7/Stuffing/Ahh... the smell of warm bread and sage./food/0 0 0 0 0 0 0 0 0 0 2/480",
                240, //"Farmer's Lunch/150/80/Cooking -7/Farmer's Lunch/This'll keep you going./food/3 0 0 0 0 0 0 0 0 0 0/480",
                241, //"Survival Burger/180/50/Cooking -7/Survival Burger/A convenient snack for the explorer./food/0 0 0 0 0 3 0 0 0 0 0/480",
                242, //"Dish O' The Sea/220/60/Cooking -7/Dish O' The Sea/This'll keep you warm in the cold sea air./food/0 3 0 0 0 0 0 0 0 0 0/480",
                243, //"Miner's Treat/200/50/Cooking -7/Miner's Treat/This should keep your energy up./food/0 0 3 0 0 0 0 0 32 0 0/480",
                244, //"Roots Platter/100/50/Cooking -7/Roots Platter/This'll get you digging for more./food/0 0 0 0 0 0 0 0 0 0 0 3/480",
                //604, //"Plum Pudding/260/70/Cooking -7/Plum Pudding/A traditional holiday treat./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //605, //"Artichoke Dip/210/40/Cooking -7/Artichoke Dip/It's cool and refreshing./food/0 0 0 0 0 0 0 0 0 0 0/0",
                606, //"Stir Fry/335/80/Cooking -7/Stir Fry/Julienned vegetables on a bed of rice./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //607, //"Roasted Hazelnuts/270/70/Cooking -7/Roasted Hazelnuts/The roasting process creates a rich forest flavor./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //608, //"Pumpkin Pie/385/90/Cooking -7/Pumpkin Pie/Silky pumpkin cream in a flaky crust./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //609, //"Radish Salad/300/80/Cooking -7/Radish Salad/The radishes are so crisp!/food/0 0 0 0 0 0 0 0 0 0 0/0",
                //610, //"Fruit Salad/450/105/Cooking -7/Fruit Salad/A delicious combination of summer fruits./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //611, //"Blackberry Cobbler/260/70/Cooking -7/Blackberry Cobbler/There's nothing quite like it./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //612, //"Cranberry Candy/175/50/Cooking -7/Cranberry Candy/It's sweet enough to mask the bitter fruit./drink/0 0 0 0 0 0 0 0 0 0 0/0",
                //618, //"Bruschetta/210/45/Cooking -7/Bruschetta/Roasted tomatoes on a crisp white bread./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //648, //"Coleslaw/345/85/Cooking -7/Coleslaw/It's light, fresh and very healthy./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //649, //"Fiddlehead Risotto/350/90/Cooking -7/Fiddlehead Risotto/A creamy rice dish served with sauteed fern heads. It's a little bland./food/0 0 0 0 0 0 0 0 0 0 0/0",
                //651, //"Poppyseed Muffin/250/60/Cooking -7/Poppyseed Muffin/It has a soothing effect./food/0 0 0 0 0 0 0 0 0 0 0/0",
                727, //"Chowder/135/90/Cooking -7/Chowder/A perfect way to warm yourself after a cold night at sea./food/0 1 0 0 0 0 0 0 0 0 0/1440",																									
                730, //"Lobster Bisque/205/90/Cooking -7/Lobster Bisque/This delicate soup is a secret family recipe of Willy's./food/0 3 0 0 0 0 0 50 0 0 0/1440",																									
                728, //"Fish Stew/175/90/Cooking -7/Fish Stew/It smells a lot like the sea. Tastes better, though./food/0 3 0 0 0 0 0 0 0 0 0/1440",																									
                729, //"Escargot/125/90/Cooking -7/Escargot/Butter-soaked snails cooked to perfection./food/0 2 0 0 0 0 0 0 0 0 0/1440",																									
                //731, //"Maple Bar/300/90/Cooking -7/Maple Bar/It's a sweet doughnut topped with a rich maple glaze./food/1 1 1 0 0 0 0 0 0 0 0/1440",																									
                732, //"Crab Cakes/275/90/Cooking -7/Crab Cakes/Crab, bread crumbs, and egg formed into patties then fried to a golden brown./food/0 0 0 0 0 0 0 0 0 1 1/1440",																									
                733, //"Shrimp Cocktail/160/90/Cooking -7/Shrimp Cocktail/A sumptuous appetizer made with freshly-caught shrimp./food/0 1 0 0 1 0 0 0 0 0 0/860",
            };

            int probability = new Random().Next(cookingList.Count);

            int objectIndex = cookingList[probability];

            return objectIndex;

        }

    }

    public class SpawnIndex
    {
        public bool anywhere;
        public string locale;

        public bool cast;
        public bool weeds;
        public bool cultivate;
        public bool wilderness;
        public bool fishspot;
        public bool crate;

        public SpawnIndex()
        {

            if (Mod.instance.Config.castAnywhere)
            {

                SpawnAnywhere();

            }

        }

        public void SpawnAnywhere()
        {

            anywhere = true;

            cast = true;

            weeds = true;

            fishspot = true;

            if (Game1.player.currentLocation.IsOutdoors)
            {
                
                wilderness = true;

                if (Game1.player.currentLocation.IsFarm)
                {

                    cultivate = true;
                    wilderness = false;

                }

                if (Game1.player.currentLocation is IslandWest islandWest)
                {

                    if (Vector2.Distance(Game1.player.Position, new Vector2(90, 55) * 64) <= 1280)
                    {

                        cultivate = true;
                        wilderness = false;

                    }

                }


            }
            else if (Game1.player.currentLocation.isGreenhouse.Value || Game1.player.currentLocation is Shed || Game1.player.currentLocation is AnimalHouse)
            {

                cultivate = true;

            }

            if (Game1.player.currentLocation.Map.Layers[0].LayerWidth * Game1.player.currentLocation.Map.Layers[0].LayerHeight > 1600)
            {

                crate = true;

            }

        }

        public SpawnIndex(GameLocation location)
        {

            if (Mod.instance.Config.castAnywhere)
            {
                
                SpawnAnywhere();

                return;

            }

            cast = true;

            locale = location.Name;

            if (location.IsOutdoors && location.IsFarm)
            {

                weeds = true;
                cultivate = true;

            }
            else if (location.isGreenhouse.Value || location is Shed || location is AnimalHouse)
            {

                cultivate = true;

            }
            else if (location is IslandWest || location is IslandNorth)
            {

                fishspot = true;
                weeds = true;
                wilderness = true;

                if (location is IslandWest islandWest)
                {

                    if (Vector2.Distance(Game1.player.Position,new Vector2(90,55)*64) <= 1280)
                    {

                        cultivate = true;
                        wilderness = false;

                    }

                    crate = true;

                }

            }
            else if (location is Forest || location is Mountain || location is Desert || location is BugLand || location is Clearing)
            {

                weeds = true;
                wilderness = true;
                fishspot = true;

                crate = true;

            }
            else if (location.Name.Contains("Backwoods") || location is Railroad || location is BusStop)
            {

                wilderness = true;
                weeds = true;

            }
            else if (location is Woods || location is Grove || location is IslandEast || location is IslandShrine)
            {

                wilderness = true;
                fishspot = true;
                weeds = true;

            }
            else if (location is Beach || location is Atoll)
            {

                wilderness = true;
                fishspot = true;
                weeds = true;
                crate = true;

            }
            else if (location is IslandSouth || location is IslandSouthEast || location is IslandSouthEastCave)
            {

                wilderness = true;
                fishspot = true;
                weeds = true;

            }
            else if (location is Town)
            {
                weeds = true;
                wilderness = true;
                fishspot = true;

            }
            else if (location.Name.Contains("DeepWoods"))
            {

                fishspot = true;

                if (location.Map.Layers[0].LayerWidth * location.Map.Layers[0].LayerHeight > 2000)
                {

                    crate = true;

                }

            }
            else if (
                location is MineShaft ||
                location is VolcanoDungeon ||
                location is Vault ||
                location is Court ||
                location is Engineum
                )
            {

                if (location.Name.Contains("20") || location.Name.Contains("60") || location.Name.Contains("100"))
                {
                    fishspot = true;

                }

                if (location is MineShaft mineShaft)
                {

                    List<int> mineLevels = new() { 3, 7 };

                    if (mineShaft.mineLevel == MineShaft.bottomOfMineLevel || mineShaft.mineLevel == MineShaft.quarryMineShaft)
                    {



                    }
                    else if (mineLevels.Contains(mineShaft.mineLevel % 10) && Mod.instance.questHandle.IsComplete(QuestHandle.etherFour))
                    {

                        crate = true;

                    }

                }

                weeds = true;

            }
            else if (location is Caldera || location is Sewer)
            {

                fishspot = true;

            }
            else if (
                location.Name.Contains("Saloon") 
                || location is DruidLocation 
                || location is FarmCave 
                || location.Name.Equals("Tunnel")
            )
            {

                cast = true;

            }
            else if (Mod.instance.Helper.ModRegistry.IsLoaded("dreamy.kickitspot"))
            {
                if (location.Name.Contains("Custom_SeaCavern")
                || location.Name.Contains("Custom_AreaSecret")
                || location.Name.Contains("Custom_DarkCave")
                || location.Name.Contains("Custom_SecretBoss")
                || location.Name.Contains("Custom_CapeCaving"))
                {

                    cast = true;


                }
                else
                {

                    cast = false;

                }

            }
            else
            {

                cast = false;

            }

        }

    }

}
