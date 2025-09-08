using StardewDruid.Handle;
using System.Collections.Generic;

namespace StardewDruid.Data
{
    internal static class AlchemyData
    {

        public static string sap = "(O)92";
        public static string slime = "(O)766";

        public static string wood = "(O)388";
        public static string hardwood = "(O)709";
        public static string stone = "(O)390";
        public static string omnigeode = "(O)749";

        public static string emerald = "(O)60";
        public static string aquamarine = "(O)62";
        public static string ruby = "(O)64";
        public static string amethyst = "(O)66";
        public static string topaz = "(O)68";
        public static string jade = "(O)70";
        public static string diamond = "(O)72";

        public static string prismatic = "(O)74";
        public static string cherrybomb = "(O)286";
        public static string bomb = "(O)287";
        public static string megabomb = "(O)288";

        public static string copperbar = "(O)334";
        public static string ironbar = "(O)335";
        public static string goldbar = "(O)336";
        public static string iridiumbar = "(O)337";
        public static string battery = "(O)787";

        public static string coffee = "(O)395";
        public static string jojacola = "(O)167";
        public static string greentea = "(O)614";
        public static string tripleshot = "(O)253";

        public static string mixedSeed = "(O)770";
        public static string flowerSeed = "MixedFlowerSeeds";
        public static string springSeed = "(O)495";
        public static string summerSeed = "(O)496";
        public static string fallSeed = "(O)497";
        public static string winterSeed = "(O)498";

        public static string ancientseed = "(O)499";
        public static string rareseed = "(O)347";
        public static string starfruitseed = "(O)486";
        public static string pineappleseed = "(O)833";
        public static string powdermelonseed = "PowdermelonSeeds";

        public static Dictionary<AlchemyProcess.processes, AlchemyProcess> ProcessList()
        {

            Dictionary<AlchemyProcess.processes, AlchemyProcess> runeboard = new();

            runeboard[AlchemyProcess.processes.winds] = new()
            {
                label = "Winds",
                name = "Aer: Winds",
                description = "Invoke Aer to attune a selected tool to the Winds",
            };

            runeboard[AlchemyProcess.processes.weald] = new()
            {
                label = "Weald",
                name = "Terra: The Weald",
                description = "Invoke Terra to attune a selected tool to the Weald",
            };

            runeboard[AlchemyProcess.processes.mists] = new()
            {
                label = "Mists",
                name = "Aqua: Mists",
                description = "Invoke Aqua to attune a selected tool to Mists",
            };

            runeboard[AlchemyProcess.processes.stars] = new()
            {
                label = "Stars",
                name = "Ignis: The Stars",
                description = "Invoke Ignis to attune a selected tool to the Stars",
            };

            runeboard[AlchemyProcess.processes.voide] = new()
            {
                label = "Voide",
                name = "The Voide",
                description = "Invoke to attune a selected tool to the Voide",
            };

            runeboard[AlchemyProcess.processes.fates] = new()
            {
                label = "Fates",
                name = "Metals: The Fates",
                description = "Invoke the metals Copper, Iron, Tin and Lead to attune a selected tool to the Fates",
            };

            runeboard[AlchemyProcess.processes.ether] = new()
            {
                label = "Ether",
                name = "Aether",
                description = "Invoke to attune a selected tool to Ether",
            };

            runeboard[AlchemyProcess.processes.witch] = new()
            {
                label = "Witch",
                name = "Sulfur: The Witch",
                description = "Invoke Sulfur to attune a selected tool to the Witch",
            };

            runeboard[AlchemyProcess.processes.material] = new()
            {
                label = "Matter",
                name = "Salt: Matter",
                description = "Select an inventory item to use as the base material for an alchemical process",
            };

            runeboard[AlchemyProcess.processes.reagent] = new()
            {
                label = "Reagent",
                name = "Quicksilver: Reagent",
                description = "Select an apothecary item to use as the reagent for an alchemical process",
            };

            runeboard[AlchemyProcess.processes.separate] = new()
            {
                label = "Separate",
                name = "Nigredo: Separate",
                description = "Complete the process of Nigredo to deconstruct the material to components that are prepared for synthesis",
            };

            runeboard[AlchemyProcess.processes.transmute] = new()
            {
                label = "Transmute",
                name = "Albedo: Transmute",
                description = "Complete the process of Albedo to synthesize a higher form of the material using the refining qualities of the reagent",
            };

            runeboard[AlchemyProcess.processes.enchant] = new()
            {
                label = "Enchant",
                name = "Rubedo: Enchant",
                description = "Complete the process of Rubedo to produce an enchanted form by the conjunction of material and learned truths and power",
            };

            runeboard[AlchemyProcess.processes.sol] = new()
            {
                label = "Sol",
                name = "Sol: Search",
                description = "Invoke Sol to search for a suitable process for the selected material and reagent",
            };

            runeboard[AlchemyProcess.processes.lune] = new()
            {
                label = "Lune",
                name = "Lune: Clear",
                description = "Invoke Lune to clear the selected material and reagent and reset the runeboard",
            };

            return runeboard;

        }

        public static Dictionary<AlchemyRecipe.recipes, AlchemyRecipe> RecipeList()
        {

            Dictionary<AlchemyRecipe.recipes, AlchemyRecipe> alchemy = new();

            alchemy[AlchemyRecipe.recipes.viscosa] = new()
            {

                type = AlchemyRecipe.recipes.viscosa,

                product = ApothecaryHandle.items.viscosa,

                name = "Brew Viscosa Base",

                instruction = "Convert currently held edible item to amounts of Viscosa or use 1x liquified source",

                ingredients = new() { sap,
                        slime,
                    },

            };

            alchemy[AlchemyRecipe.recipes.convert_viscosa] = new()
            {

                type = AlchemyRecipe.recipes.convert_viscosa,

                product = ApothecaryHandle.items.viscosa,

                name = "Convert To Viscosa",

                instruction = "Convert currently held edible item to amounts of Viscosa or use 1x liquified source",

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.edible,
                }

            };

            // -----------------------------------------------

            alchemy[AlchemyRecipe.recipes.ligna] = new()
            {
                type = AlchemyRecipe.recipes.ligna,

                name = "Brew Ligna Potion",

                product = ApothecaryHandle.items.ligna,

                instruction = "Requires 1x unit of Viscosa and 1x Tree Seed",

                items = new()
                {
                    ApothecaryHandle.items.viscosa,
                },

                ingredients = new() {
                        "(O)311", // "Acorn", 
                        "(O)310", // "MapleSeed", 
                        "(O)309", // "Pinecorn", 
                        "(O)292", // "MahoganySeed", 
                        "(O)Moss", // "Moss", 
                    },
            };


            alchemy[AlchemyRecipe.recipes.satius_ligna] = new()
            {
                type = AlchemyRecipe.recipes.satius_ligna,

                name = "Brew Satius Ligna Potion",

                product = ApothecaryHandle.items.satius_ligna,

                instruction = "Requires 1x Ligna Potion and 1x Nutrient Source",

                items = new()
                {
                    ApothecaryHandle.items.ligna,
                },

                ingredients = new() {
                       "(O)431", // "Sunflower Seeds",
                        "(O)270", // "Corn",
                        "(O)271", // "Unmilled Rice",
                        "(O)273", // "Rice Shoot",
                        "(O)831", // "Taro Tubers", 
                        "(O)399", // "Spring Onion", 
                    },
            };


            alchemy[AlchemyRecipe.recipes.magnus_ligna] = new()
            {
                type = AlchemyRecipe.recipes.magnus_ligna,

                name = "Brew Magnus Ligna Potion",

                product = ApothecaryHandle.items.magnus_ligna,

                instruction = "Requires 1x Satius Ligna Potion and 1x Meadow Flower",

                items = new()
                {
                    ApothecaryHandle.items.satius_ligna,
                },

                ingredients = new() {
                        "(O)418", // "Crocus", 
                        "(O)18", // "Daffodil", 
                        "(O)22", // "Dandelion", 
                        "(O)402", // "Sweet Pea", 
                        "(O)591", // "Tulip", 
                        "(O)376", // "Poppy", 
                        "(O)421", // "Sunflower",
                        "(O)593", // "Spangle",
                        "(O)597", // "Jazz",
                    },
            };


            alchemy[AlchemyRecipe.recipes.optimus_ligna] = new()
            {
                type = AlchemyRecipe.recipes.optimus_ligna,

                name = "Brew Optimus Ligna Potion",

                product = ApothecaryHandle.items.optimus_ligna,

                instruction = "Requires 1x Magnus Ligna Potion and 1x Aether",

                items = new()
                {
                    ApothecaryHandle.items.magnus_ligna,
                    ApothecaryHandle.items.aether,
                },

            };

            // -----------------------------------------------

            alchemy[AlchemyRecipe.recipes.vigores] = new()
            {
                type = AlchemyRecipe.recipes.vigores,

                name = "Brew Vigores Potion",

                product = ApothecaryHandle.items.vigores,

                instruction = "Requires 1x Viscosa and 1x Cave Forage",

                items = new()
                {
                    ApothecaryHandle.items.viscosa,
                },

                ingredients = new() {
                        "(O)78", // "Cave Carrot", 
                        "(O)412", // "Winter Root", 
                        "(O)16", // "Wild Horseradish", 
                        "(O)404", // "Common Mushrooms", 
                        "(O)257", // "Morel", 
                        "(O)767", // "Batwings", 
                        "(O)420", // "Red Mushrooms", 
                    },
            };


            alchemy[AlchemyRecipe.recipes.satius_vigores] = new()
            {
                type = AlchemyRecipe.recipes.satius_vigores,

                name = "Brew Satius Vigores Potion",

                product = ApothecaryHandle.items.satius_vigores,

                instruction = "Requires 1x Vigores Potion and 1x Combustible Material",

                items = new()
                {
                    ApothecaryHandle.items.vigores,
                },

                ingredients = new() {
                        "(O)93", // "Torch", 
                        "(O)82", // "Fire Quartz", 
                        "(O)382", // "Coal", 
                    },
            };


            alchemy[AlchemyRecipe.recipes.magnus_vigores] = new()
            {
                type = AlchemyRecipe.recipes.magnus_vigores,

                name = "Brew Magnus Vigores Potion",

                product = ApothecaryHandle.items.magnus_vigores,

                instruction = "Requires 1x Satius Vigores Potion and 1x Spicy Ingredient",

                items = new()
                {
                    ApothecaryHandle.items.satius_vigores,
                },

                ingredients = new() {
                        "(O)419", // "Vinegar", 
                        "(O)260", // "Hot Pepper", 
                        "(O)829", // "Ginger", 
                        "(O)248", // "Garlic", 
                    },
            };


            alchemy[AlchemyRecipe.recipes.optimus_vigores] = new()
            {

                type = AlchemyRecipe.recipes.optimus_vigores,

                name = "Brew Optimus Vigores Potion",

                product = ApothecaryHandle.items.optimus_vigores,

                instruction = "Requires 1x Magnus Vigores Potion and 1x Aether",

                items = new()
                {
                    ApothecaryHandle.items.magnus_vigores,
                    ApothecaryHandle.items.aether,
                },

            };

            // -----------------------------------------------

            alchemy[AlchemyRecipe.recipes.celeri] = new()
            {
                type = AlchemyRecipe.recipes.celeri,

                name = "Brew Celeri Potion",

                product = ApothecaryHandle.items.celeri,

                instruction = "Requires 1x Viscosa and 1x Mineral Source",

                items = new()
                {
                    ApothecaryHandle.items.viscosa,
                },

                ingredients = new() {
                        "(O)168", // "Trash", 
                        "(O)169", // "Driftwood", 
                        "(O)170", // "Broken Glasses", 
                        "(O)171", // "Broken CD",
                        "(O)330", // "Clay",
                        "(O)881", // "Bone Fragment", 
                        "(O)80", // "Quartz", 
                        "(O)86", // "Earth Crystal", 
                    },
            };


            alchemy[AlchemyRecipe.recipes.satius_celeri] = new()
            {
                type = AlchemyRecipe.recipes.satius_celeri,

                name = "Brew Satius Celeri Potion",

                product = ApothecaryHandle.items.satius_celeri,

                instruction = "Requires 1x Celeri Potion and 1x Energy Source",

                items = new()
                {
                    ApothecaryHandle.items.celeri,
                },

                ingredients = new() {
                      "(O)167", // "Joja Cola", 
                        "(O)153", // "Seaweed", 
                        "(O)433", // "Coffee Bean", 
                        "(O)157", // "White Algae",
                        "(O)152", // "Algae", 
                        "(O)815", // "Tea Leaves", 
                    },
            };


            alchemy[AlchemyRecipe.recipes.magnus_celeri] = new()
            {
                type = AlchemyRecipe.recipes.magnus_celeri,

                name = "Brew Magnus Celeri Potion",

                product = ApothecaryHandle.items.magnus_celeri,

                instruction = "Requires 1x Satius Celeri Potion and 1x Shellfish",

                items = new()
                {
                    ApothecaryHandle.items.satius_celeri,
                },

                ingredients = new() {
                        "(O)718", // "Cockle", 
                        "(O)719", // "Mussel", 
                        "(O)720", // "Shrimp", 
                        "(O)721", // "Snail", 
                        "(O)722", // "Periwinkle", 
                        "(O)723", // "Oyster", 
                        "(O)372", // "Clam", 
                    },
            };


            alchemy[AlchemyRecipe.recipes.optimus_celeri] = new()
            {
                type = AlchemyRecipe.recipes.optimus_celeri,

                name = "Brew Optimus Celeri Potion",

                product = ApothecaryHandle.items.optimus_celeri,

                instruction = "Requires 1x Magnus Celeri Potion and 1x Aether",

                items = new()
                {
                    ApothecaryHandle.items.magnus_celeri,
                    ApothecaryHandle.items.aether,
                },

            };

            // -----------------------------------------------

            alchemy[AlchemyRecipe.recipes.faeth] = new()
            {
                type = AlchemyRecipe.recipes.faeth,

                name = "Brew Faeth Source",

                product = ApothecaryHandle.items.faeth,

                instruction = "Requires 1x Infused Source",

                ingredients = new() { "(O)577", "(O)595", "(O)768", "(O)769", "(O)MossySeed", },
            };

            alchemy[AlchemyRecipe.recipes.aether] = new()
            {
                type = AlchemyRecipe.recipes.aether,

                name = "Brew Aether Source",

                product = ApothecaryHandle.items.aether,

                instruction = "Requires 1x Quality Gem",

                ingredients = new() { emerald, aquamarine, ruby, diamond, },
            };

            // -----------------------------------------------

            alchemy[AlchemyRecipe.recipes.coruscant] = new()
            {
                type = AlchemyRecipe.recipes.coruscant,

                name = "Craft Coruscant Base",

                product = ApothecaryHandle.items.coruscant,

                instruction = "Requires 1x Sparkling item",

                ingredients = new() {

                        "(O)66",
                        "(O)68",
                        "(O)70",
                        "(O)245",
                    },
            };

            alchemy[AlchemyRecipe.recipes.imbus] = new()
            {
                type = AlchemyRecipe.recipes.imbus,

                name = "Craft Imbus Powder",

                product = ApothecaryHandle.items.imbus,

                instruction = "Requires 1x Coruscant, 1x Hardwood",

                items = new() { ApothecaryHandle.items.coruscant, },

                ingredients = new() {
                    "(O)709",
                    },
            };

            alchemy[AlchemyRecipe.recipes.amori] = new()
            {
                type = AlchemyRecipe.recipes.amori,

                name = "Craft Amori Powder",

                product = ApothecaryHandle.items.amori,

                instruction = "Requires 1x Coruscant, 1x Coral",

                items = new() { ApothecaryHandle.items.coruscant, },

                ingredients = new() {
                    "(O)393",
                    },
            };

            alchemy[AlchemyRecipe.recipes.macerari] = new()
            {
                type = AlchemyRecipe.recipes.macerari,

                name = "Craft Macerari Powder",

                product = ApothecaryHandle.items.macerari,

                instruction = "Requires 1x Coruscant, 1x Refined Quartz",

                items = new() { ApothecaryHandle.items.coruscant, },

                ingredients = new() {
                     "(O)338",
                    },
            };

            alchemy[AlchemyRecipe.recipes.rapidus] = new()
            {
                type = AlchemyRecipe.recipes.rapidus,

                name = "Craft Rapidus Powder",

                product = ApothecaryHandle.items.rapidus,

                instruction = "Requires 1x Coruscant, 1x Coffee",

                items = new() { ApothecaryHandle.items.coruscant, },

                ingredients = new() {
                    "(O)395",
                    },
            };

            // -----------------------------------------------

            alchemy[AlchemyRecipe.recipes.voil] = new()
            {
                type = AlchemyRecipe.recipes.voil,

                name = "Craft Voil Base",

                product = ApothecaryHandle.items.voil,

                instruction = "Requires 1x Prehistoric item",

                ingredients = new() { omnigeode,
                        "(O)579",
                        "(O)580",
                        "(O)581",
                        "(O)582",
                        "(O)583",
                        "(O)584",
                        "(O)585",
                    },
            };

            alchemy[AlchemyRecipe.recipes.concutere] = new()
            {
                type = AlchemyRecipe.recipes.concutere,

                name = "Craft Concutere Powder",

                product = ApothecaryHandle.items.concutere,

                instruction = "Requires 1x Voil, 1x Staircase",

                items = new() { ApothecaryHandle.items.voil, },

                ingredients = new() {
                    "(BC)71",
                    },
            };

            alchemy[AlchemyRecipe.recipes.jumere] = new()
            {
                type = AlchemyRecipe.recipes.jumere,

                name = "Craft Jumere Powder",

                product = ApothecaryHandle.items.jumere,

                instruction = "Requires 1x Voil, 1x Mayonnaise",

                items = new() { ApothecaryHandle.items.voil, },

                ingredients = new() {
                    "(O)306", "(O)307",
                    },
            };

            alchemy[AlchemyRecipe.recipes.felis] = new()
            {
                type = AlchemyRecipe.recipes.felis,

                name = "Craft Felis Powder",

                product = ApothecaryHandle.items.felis,

                instruction = "Requires 1x Voil, 1x Milk",

                items = new() { ApothecaryHandle.items.voil, },

                ingredients = new() {
                     "(O)184", "(O)186",
                    },
            };

            alchemy[AlchemyRecipe.recipes.sanctus] = new()
            {
                type = AlchemyRecipe.recipes.sanctus,

                name = "Craft Sanctus Powder",

                product = ApothecaryHandle.items.sanctus,

                instruction = "Requires 1x Voil, 1x Alcohol",

                items = new() { ApothecaryHandle.items.voil, },

                ingredients = new() {
                    "(O)346", "(O)348",
                    },
            };

            // -----------------------------------------------

            alchemy[AlchemyRecipe.recipes.capesso] = new()
            {
                type = AlchemyRecipe.recipes.capesso,

                name = "Craft Capesso Powder",

                product = ApothecaryHandle.items.capesso,

                instruction = "Requires 1x Coruscant and 1x Voil",

                items = new() { ApothecaryHandle.items.voil, ApothecaryHandle.items.coruscant },


            };

            alchemy[AlchemyRecipe.recipes.captis] = new()
            {
                type = AlchemyRecipe.recipes.captis,

                name = "Craft Captis Powder",

                product = ApothecaryHandle.items.captis,

                instruction = "Requires 1x Copper Bar",

                ingredients = new() { copperbar,
                    },
            };

            alchemy[AlchemyRecipe.recipes.ferrum_captis] = new()
            {
                type = AlchemyRecipe.recipes.ferrum_captis,

                name = "Craft Ferrum Captis Powder",

                product = ApothecaryHandle.items.ferrum_captis,

                instruction = "Requires 1x Captis (base), 1x Iron Bar",

                items = new() { ApothecaryHandle.items.captis, },

                ingredients = new() { ironbar,
                    },
            };

            alchemy[AlchemyRecipe.recipes.aurum_captis] = new()
            {
                type = AlchemyRecipe.recipes.aurum_captis,

                name = "Craft Aurum Captis Powder",

                product = ApothecaryHandle.items.aurum_captis,

                instruction = "Requires 1x Ferrum Captis (base), 1x Gold Bar",

                items = new() { ApothecaryHandle.items.ferrum_captis, },

                ingredients = new() { goldbar,
                    },
            };

            alchemy[AlchemyRecipe.recipes.diamas_captis] = new()
            {
                type = AlchemyRecipe.recipes.diamas_captis,

                name = "Craft Diamas Captis Powder",

                product = ApothecaryHandle.items.diamas_captis,

                instruction = "Requires 1x Aurum Captis (base), 1x Quality Metal",

                items = new() { ApothecaryHandle.items.aurum_captis, },

                ingredients = new() { iridiumbar, battery,
                    },
            };

            // -----------------------------------------------

            alchemy[AlchemyRecipe.recipes.best_ligna] = new()
            {
                type = AlchemyRecipe.recipes.best_ligna,

                name = "Best Ligna",

                instruction = "Consume to brew the best version of Ligna potion available",

                items = new() { ApothecaryHandle.items.omen_feather, },

            };

            alchemy[AlchemyRecipe.recipes.best_vigores] = new()
            {
                type = AlchemyRecipe.recipes.best_vigores,

                name = "Best Vigores",

                instruction = "Consume to brew the best version of Vigores potion available",

                items = new() { ApothecaryHandle.items.omen_tuft, },

            };

            alchemy[AlchemyRecipe.recipes.best_celeri] = new()
            {
                type = AlchemyRecipe.recipes.best_celeri,

                name = "Best Celeri",

                instruction = "Consume to brew the best version of Celeri potion available",

                items = new() { ApothecaryHandle.items.omen_shell, },

            };

            alchemy[AlchemyRecipe.recipes.random_powder] = new()
            {
                type = AlchemyRecipe.recipes.random_powder,

                name = "Random Powder",

                instruction = "Consume to craft a random Bomb Powder",

                items = new() { ApothecaryHandle.items.omen_down, },

            };

            // -----------------------------------------------

            alchemy[AlchemyRecipe.recipes.convert_treeseed] = new()
            {
                type = AlchemyRecipe.recipes.convert_treeseed,

                name = "Convert to Tree Seed",

                instruction = "Consume to convert up to five of a saleable item into random tree seed",

                items = new() { ApothecaryHandle.items.omen_nest, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            alchemy[AlchemyRecipe.recipes.convert_gems] = new()
            {
                type = AlchemyRecipe.recipes.convert_gems,

                name = "Convert to Gems",

                instruction = "Consume to convert up to five of a saleable item into an amount of a random gems",

                items = new() { ApothecaryHandle.items.omen_glass, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            alchemy[AlchemyRecipe.recipes.convert_liquid] = new()
            {
                type = AlchemyRecipe.recipes.convert_liquid,

                name = "Convert to Liquid",

                instruction = "Consume to convert up to five of a saleable item into an amount of a random liquid",

                items = new() { ApothecaryHandle.items.omen_coral, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            alchemy[AlchemyRecipe.recipes.convert_rareseed] = new()
            {
                type = AlchemyRecipe.recipes.convert_rareseed,

                name = "Convert to Rare Seed",

                instruction = "Consume to convert up to five of a saleable item into a random rare seed",

                items = new() { ApothecaryHandle.items.omen_wealdseed, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            alchemy[AlchemyRecipe.recipes.convert_mixedseed] = new()
            {
                type = AlchemyRecipe.recipes.convert_mixedseed,

                name = "Convert to Mixed Seed",

                instruction = "Consume to convert up to five of a saleable item into an amount of mixed seed",

                items = new() { ApothecaryHandle.items.omen_courtbloom, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            alchemy[AlchemyRecipe.recipes.convert_flowerseed] = new()
            {
                type = AlchemyRecipe.recipes.convert_flowerseed,

                name = "Convert to Flower Seed",

                instruction = "Consume to convert up to five of a saleable item into an amount of flower seed",

                items = new() { ApothecaryHandle.items.omen_doubtseed, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            alchemy[AlchemyRecipe.recipes.convert_wildseed] = new()
            {
                type = AlchemyRecipe.recipes.convert_wildseed,

                name = "Convert to Wild Seeds",

                instruction = "Consume to convert up to five of a saleable item into an amount of a wild seed",

                items = new() { ApothecaryHandle.items.omen_elderbloom, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            alchemy[AlchemyRecipe.recipes.convert_bombs] = new()
            {
                type = AlchemyRecipe.recipes.convert_bombs,

                name = "Convert to Bombs",

                instruction = "Consume to convert up to five of a saleable item into an amount of bombs",

                items = new() { ApothecaryHandle.items.trophy_gloop, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            alchemy[AlchemyRecipe.recipes.convert_caffeine] = new()
            {
                type = AlchemyRecipe.recipes.convert_caffeine,

                name = "Convert to Gems",

                instruction = "Consume to convert up to five of a saleable item into an amount of caffeinated product",

                items = new() { ApothecaryHandle.items.trophy_tusk, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            alchemy[AlchemyRecipe.recipes.convert_geodes] = new()
            {
                type = AlchemyRecipe.recipes.convert_geodes,

                name = "Convert to Geodes",

                instruction = "Consume to convert up to five of a saleable item into an amount of geodes",

                items = new() { ApothecaryHandle.items.trophy_skull, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            alchemy[AlchemyRecipe.recipes.convert_gold] = new()
            {
                type = AlchemyRecipe.recipes.convert_gold,

                name = "Convert to Gold",

                instruction = "Consume to convert up to five of a saleable item into it's weight in gold bars",

                items = new() { ApothecaryHandle.items.trophy_heart, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.saleable,
                }

            };

            // -----------------------------------------------

            alchemy[AlchemyRecipe.recipes.update_cooking] = new()
            {
                type = AlchemyRecipe.recipes.update_cooking,

                name = "Enhance Cooking",

                instruction = "Consume to update the quality of a Cooking item to Iridium Quality",

                items = new() { ApothecaryHandle.items.trophy_pumpkin, },

                requirements = new()
                {
                    
                    AlchemyRecipe.qualifiers.cooking,

                }

            };

            alchemy[AlchemyRecipe.recipes.update_stack] = new()
            {
                type = AlchemyRecipe.recipes.update_stack,

                name = "Duplicate",

                instruction = "Consume to increase the stack size of a stackable item by one unit",

                items = new() { ApothecaryHandle.items.trophy_pearl, },

                requirements = new()
                {
                    AlchemyRecipe.qualifiers.stackable,
                }

            };

            alchemy[AlchemyRecipe.recipes.update_produce] = new()
            {
                type = AlchemyRecipe.recipes.update_produce,

                name = "Enhance Produce",

                instruction = "Consume to update the quality of a Forage, Crop or Fruit item to Iridium Quality",

                items = new() { ApothecaryHandle.items.trophy_tooth, },

                requirements = new()
                {

                    AlchemyRecipe.qualifiers.produce,
                
                }

            };

            alchemy[AlchemyRecipe.recipes.update_artisanal] = new()
            {
                type = AlchemyRecipe.recipes.update_artisanal,

                name = "Enhance Artisanal",

                instruction = "Consume to update the quality of an artisanal item to Iridium Quality",

                items = new() { ApothecaryHandle.items.trophy_shell, },

                requirements = new()
                {
                    
                    AlchemyRecipe.qualifiers.artisan,

                }

            };

            // ------------------------------------------------------
            // Enchant

            alchemy[AlchemyRecipe.recipes.next_ligna] = new()
            {

                type = AlchemyRecipe.recipes.next_ligna,

                product = ApothecaryHandle.items.satius_celeri,

                name = "Remix Ligna",

                instruction = "Remix a concentrated amount of Ligna potion to produce a better potion.",

                items = new() { ApothecaryHandle.items.ligna, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_satius_ligna] = new()
            {

                type = AlchemyRecipe.recipes.next_satius_ligna,

                product = ApothecaryHandle.items.magnus_celeri,

                name = "Remix Satius Ligna",

                instruction = "Remix a concentrated amount of Satius Ligna potion to produce a better potion.",

                items = new() { ApothecaryHandle.items.satius_ligna, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_magnus_ligna] = new()
            {

                type = AlchemyRecipe.recipes.next_magnus_ligna,

                product = ApothecaryHandle.items.optimus_celeri,

                name = "Remix Magnus Ligna",

                instruction = "Remix a concentrated amount of Magnus Ligna potion to produce a better potion.",

                items = new() { ApothecaryHandle.items.magnus_ligna, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_vigores] = new()
            {

                type = AlchemyRecipe.recipes.next_vigores,

                product = ApothecaryHandle.items.satius_vigores,

                name = "Remix Vigores",

                instruction = "Remix a concentrated amount of Vigores potion to produce a better potion.",

                items = new() { ApothecaryHandle.items.vigores, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_satius_vigores] = new()
            {

                type = AlchemyRecipe.recipes.next_satius_vigores,

                product = ApothecaryHandle.items.magnus_vigores,

                name = "Remix Satius Vigores",

                instruction = "Remix a concentrated amount of Satius Vigores potion to produce a better potion.",

                items = new() { ApothecaryHandle.items.satius_vigores, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_magnus_vigores] = new()
            {

                type = AlchemyRecipe.recipes.next_magnus_vigores,

                product = ApothecaryHandle.items.optimus_vigores,

                name = "Remix Magnus Vigores",

                instruction = "Remix a concentrated amount of Magnus Vigores potion to produce a better potion.",

                items = new() { ApothecaryHandle.items.magnus_vigores, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_celeri] = new()
            {

                type = AlchemyRecipe.recipes.next_celeri,

                product = ApothecaryHandle.items.satius_celeri,

                name = "Remix Celeri",

                instruction = "Remix a concentrated amount of Celeri potion to produce a better potion.",

                items = new() { ApothecaryHandle.items.celeri, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_satius_celeri] = new()
            {

                type = AlchemyRecipe.recipes.next_satius_celeri,

                product = ApothecaryHandle.items.magnus_celeri,

                name = "Remix Satius Celeri",

                instruction = "Remix a concentrated amount of Satius Celeri potion to produce a better potion.",

                items = new() { ApothecaryHandle.items.satius_celeri, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_magnus_celeri] = new()
            {

                type = AlchemyRecipe.recipes.next_magnus_celeri,

                product = ApothecaryHandle.items.optimus_celeri,

                name = "Remix Magnus Celeri",

                instruction = "Remix a concentrated amount of Magnus Celeri potion to produce a better potion.",

                items = new() { ApothecaryHandle.items.magnus_celeri, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_coruscant] = new()
            {

                type = AlchemyRecipe.recipes.next_coruscant,

                product = ApothecaryHandle.items.voil,

                name = "Remix Coruscant",

                instruction = "Remix a concentrated amount of Coruscant powder to produce a better powder.",

                items = new() { ApothecaryHandle.items.coruscant, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_imbus] = new()
            {

                type = AlchemyRecipe.recipes.next_imbus,

                product = ApothecaryHandle.items.concutere,

                name = "Remix Imbus",

                instruction = "Remix a concentrated amount of Imbus powder to produce a better powder.",

                items = new() { ApothecaryHandle.items.imbus, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_amori] = new()
            {

                type = AlchemyRecipe.recipes.next_amori,

                product = ApothecaryHandle.items.jumere,

                name = "Remix Amori",

                instruction = "Remix a concentrated amount of Amori powder to produce a better powder.",

                items = new() { ApothecaryHandle.items.amori, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_macerari] = new()
            {

                type = AlchemyRecipe.recipes.next_macerari,

                product = ApothecaryHandle.items.felis,

                name = "Remix Macerari",

                instruction = "Remix a concentrated amount of Macerari powder to produce a better powder.",

                items = new() { ApothecaryHandle.items.macerari, },

                process = AlchemyProcess.processes.enchant,

            };

            alchemy[AlchemyRecipe.recipes.next_rapidus] = new()
            {

                type = AlchemyRecipe.recipes.next_rapidus,

                product = ApothecaryHandle.items.sanctus,

                name = "Remix Rapidus",

                instruction = "Remix a concentrated amount of Rapidus powder to produce a better powder.",

                items = new() { ApothecaryHandle.items.rapidus, },

                process = AlchemyProcess.processes.enchant,

            };

            return alchemy;

        }
    }
}