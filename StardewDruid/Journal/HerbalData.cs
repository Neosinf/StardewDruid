
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Location;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Enchantments;
using StardewValley.GameData.BigCraftables;
using StardewValley.GameData.Characters;
using StardewValley.Menus;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using xTile.Dimensions;
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Character.Character;
using static StardewDruid.Character.CharacterHandle;
using static StardewDruid.Journal.HerbalData;
using static StardewValley.Menus.CharacterCustomization;

namespace StardewDruid.Journal
{
    public class HerbalData
    {

        public enum herbals
        {

            none,
            ligna,
            melius_ligna,
            satius_ligna,
            magnus_ligna,
            optimus_ligna,
            impes,
            melius_impes,
            satius_impes,
            magnus_impes,
            optimus_impes,
            celeri,
            melius_celeri,
            satius_celeri,
            magnus_celeri,
            optimus_celeri,
            faeth,
            aether,
            ambrosia,

        }

        public Dictionary<string, Herbal> herbalism = new();

        public Dictionary<herbals, HerbalBuff> applied = new();

        public bool applyChange;

        public static List<string> HerbalIngredients(herbals herbalId)
        {

            switch (herbalId)
            {

                case herbals.ligna:
                    return new() { 
                        "(O)92", // "Sap", 
                        "(O)766", // "Slime", 
                    };

                case herbals.melius_ligna:
                    return new() { 
                        "(O)311", // "Acorn", 
                        "(O)310", // "MapleSeed", 
                        "(O)309", // "Pinecorn", 
                        "(O)292", // "MahoganySeed", 
                        "(O)Moss", // "Moss", 
                    };

                case herbals.satius_ligna:
                    return new() { 
                        "(O)418", // "Crocus", 
                        "(O)18", // "Daffodil", 
                        "(O)22", // "Dandelion", 
                        "(O)402", // "Sweet Pea", 
                        "(O)273", // "Rice Shoot", 
                        "(O)591", // "Tulip", 
                        "(O)376", // "Poppy", 
                    };

                case herbals.magnus_ligna:
                    return new()
                    {
                        "(O)247", // "Oil",
                        "(O)431", // "Sunflower Seeds",
                        "(O)270", // "Corn",
                        "(O)271", // "Unmilled Rice",
                        "(O)421", // "Sunflower",
                        "(O)593", // "Spangle",
                        "(O)597", // "Jazz",
                    };

                case herbals.optimus_ligna:
                    break;

                case herbals.impes:
                    return new() { 
                        "(O)399", // "Spring Onion", 
                        "(O)78", // "Cave Carrot", 
                        "(O)24", // "Parsnip", 
                        "(O)831", // "Taro Tubers", 
                        "(O)16", // "Wild Horseradish", 
                        "(O)412", // "Winter Root", 
                    };

                case herbals.melius_impes:
                    return new() { 
                        "(O)420", // "Red Mushrooms", 
                        "(O)404", // "Common Mushrooms", 
                        "(O)257", // "Morel", 
                        "(O)767", // "Batwings", 
                    };


                case herbals.satius_impes:
                    return new() { 
                        "(O)93", // "Torch", 
                        "(O)82", // "Fire Quartz", 
                        "(O)382", // "Coal", 
                    };

                case herbals.magnus_impes:
                    return new() { 
                        "(O)419", // "Vinegar", 
                        "(O)260", // "Hot Pepper", 
                        "(O)829", // "Ginger", 
                        "(O)248", // "Garlic", 
                    };

                case herbals.optimus_impes:
                    break;

                case herbals.celeri:
                    return new() { 
                        "(O)152", // "Algae", 
                        "(O)153", // "Seaweed", 
                        "(O)157", // "White Algae", 
                        "(O)815", // "Tea Leaves", 
                        "(O)433", // "Coffee Bean", 
                        "(O)167", // "Joja Cola", 
                    };

                case herbals.melius_celeri:
                    return new() { 
                        "(O)80", // "Quartz", 
                        "(O)86", // "Earth Crystal", 
                        "(O)84", // "Frozen Tear",
                        "(O)881", // "Bone Fragment", 
                        "(O)168", // "Trash", 
                        "(O)169", // "Driftwood", 
                        "(O)170", // "Broken Glasses", 
                        "(O)171", // "Broken CD", 
                    };

                case herbals.satius_celeri:
                    return new() { 
                        "(O)129", // "Sardine", 
                        "(O)131", // "Anchovy", 
                        "(O)137", // "Smallmouth Bass", 
                        "(O)145", // "Sunfish", 
                        "(O)132", // "Bream", 
                        "(O)147", // "Herring", 
                        "(O)142", // "Carp", 
                        "(O)156", // "Ghostfish", 
                    };

                case herbals.magnus_celeri:
                    return new() { 
                        "(O)718", // "Cockle", 
                        "(O)719", // "Mussel", 
                        "(O)720", // "Shrimp", 
                        "(O)721", // "Snail", 
                        "(O)722", // "Periwinkle", 
                        "(O)723", // "Oyster", 
                        "(O)372", // "Clam", 
                    };

                case herbals.optimus_celeri:
                    break;

                case herbals.faeth:

                    return new() { "(O)577", "(O)595", "(O)768", "(O)769", "(O)MossySeed", };

                case herbals.aether:

                    return new() { "(O)60", "(O)64", "(O)72", "(O)62", };
            }

            return new();

        }

        public Dictionary<herbals, List<string>> titles = new()
        {
            [herbals.ligna] = new() { 
                "Ligna", 
                "Boosts rite damage and success-rate", },
            [herbals.impes] = new() { 
                "Vigores", 
                "Boosts charge-ups and rite critical hit chance", },
            [herbals.celeri] = new() { 
                "Celeri", 
                "Boosts movement speed, lowers rite cooldowns", },
            [herbals.faeth] = new() { 
                "Essentia", 
                "Magical resources used for advanced alchemy", },
        };

        public Dictionary<herbals, IconData.schemes> schemes = new()
        {
            [herbals.ligna] = IconData.schemes.herbal_ligna,
            [herbals.impes] = IconData.schemes.herbal_impes,
            [herbals.celeri] = IconData.schemes.herbal_celeri,
            [herbals.faeth] = IconData.schemes.herbal_faeth,
            [herbals.aether] = IconData.schemes.herbal_aether,

        };

        public Dictionary<herbals,List<herbals>> lines = new()
        {
            [herbals.ligna] = new() {
                herbals.ligna,
                herbals.melius_ligna,
                herbals.satius_ligna,
                herbals.magnus_ligna,
                herbals.optimus_ligna, 
            },
            [herbals.impes] = new() {
                herbals.impes,
                herbals.melius_impes,
                herbals.satius_impes,
                herbals.magnus_impes,
                herbals.optimus_impes,
            },
            [herbals.celeri] = new() {
                herbals.celeri,
                herbals.melius_celeri,
                herbals.satius_celeri,
                herbals.magnus_celeri,
                herbals.optimus_celeri,
            },
        };

        public List<herbals> herbalLayout = new()
        {
            
            herbals.ligna,
            herbals.melius_ligna,
            herbals.satius_ligna,
            herbals.magnus_ligna,
            herbals.optimus_ligna,

            herbals.faeth,

            herbals.impes,
            herbals.melius_impes,
            herbals.satius_impes,
            herbals.magnus_impes,
            herbals.optimus_impes,

            herbals.aether,

            herbals.celeri,
            herbals.melius_celeri,
            herbals.satius_celeri,
            herbals.magnus_celeri,
            herbals.optimus_celeri,

        };

        public double consumeBuffer;

        public HerbalData()
        {

            herbalism = HerbalList();

        }

        public int MaxHerbal()
        {

            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.herbalism_gauge.ToString()))
            {

                return 4;

            }
            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.herbalism_still.ToString()))
            {

                return 3;

            }
            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.herbalism_pan.ToString()))
            {

                return 2;

            }
            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.herbalism_mortar.ToString()))
            {

                return 1;

            }

            return -1;

        }

        public Dictionary<int, Journal.ContentComponent> JournalHerbals()
        {

            Dictionary<int, Journal.ContentComponent> journal = new();

            int max = MaxHerbal();

            int start = 0;

            foreach (herbals herbal in herbalLayout)
            {

                string key = herbal.ToString();

                if (herbalism[key].level > max)
                {
                    
                    Journal.ContentComponent blank = new(ContentComponent.contentTypes.potion, key, false);
                    
                    journal[start++] = blank;

                    continue;

                }

                Journal.ContentComponent content = new(ContentComponent.contentTypes.potion, key);

                Mod.instance.herbalData.CheckHerbal(key);

                int amount = 0;

                if (Mod.instance.save.herbalism.ContainsKey(herbal))
                {

                    amount = Mod.instance.save.herbalism[herbal];

                }

                string amountString = amount.ToString();

                content.text[0] = amount.ToString();

                content.relics[0] = herbalism[key].container;

                content.relicColours[0] = Color.White;

                content.relics[1] = herbalism[key].content;

                IconData.schemes scheme = schemes[herbalism[key].line];

                Microsoft.Xna.Framework.Color potionColour = Mod.instance.iconData.schemeColours[scheme];

                if (amount == 0)
                {
                    potionColour = Microsoft.Xna.Framework.Color.LightGray;
                }

                content.relicColours[1] = potionColour;

                journal[start++] = content;

            }

            return journal;

        }

        public Dictionary<int, Journal.ContentComponent> JournalHeaders()
        {

            Dictionary<int, Journal.ContentComponent> journal = new();

            int start = 0;

            foreach(KeyValuePair<HerbalData.herbals, List<string>> section in titles)
            {

                Journal.ContentComponent content = new(ContentComponent.contentTypes.header, section.Key.ToString());

                content.text[0] = section.Value[0];

                content.text[1] = section.Value[1];

                journal[start++] = content;

            }

            return journal;

        }
        public static Dictionary<string, Herbal> HerbalList()
        {

            Dictionary<string, Herbal> potions = new();

            // ====================================================================
            // Ligna line

            potions[herbals.ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.ligna,

                container = IconData.relics.flask,

                content = IconData.relics.flask1,

                level = 0,

                duration = 0,

                title = Mod.instance.Helper.Translation.Get("HerbalData.25"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.27"),

                ingredients = HerbalIngredients(herbals.ligna),

                bases = new() { },

                health = 10,

                stamina = 15,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.39"),
                    Mod.instance.Helper.Translation.Get("HerbalData.40")
                }

            };


            potions[herbals.melius_ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.melius_ligna,

                container = IconData.relics.flask,

                content = IconData.relics.flask2,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("HerbalData.61"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.63"),

                ingredients = HerbalIngredients(herbals.melius_ligna),

                bases = new() { herbals.ligna, },

                health = 15,

                stamina = 30,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.75"),
                    Mod.instance.Helper.Translation.Get("HerbalData.76"),
                    Mod.instance.Helper.Translation.Get("HerbalData.77")
                }

            };


            potions[herbals.satius_ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.satius_ligna,

                container = IconData.relics.flask,

                content = IconData.relics.flask3,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("HerbalData.98"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.100"),

                ingredients = HerbalIngredients(herbals.satius_ligna),

                bases = new() { herbals.melius_ligna, },

                health = 20,

                stamina = 80,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.112"),
                    Mod.instance.Helper.Translation.Get("HerbalData.113"),
                    Mod.instance.Helper.Translation.Get("HerbalData.114")
                }

            };


            potions[herbals.magnus_ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.magnus_ligna,

                container = IconData.relics.flask,

                content = IconData.relics.flask4,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("HerbalData.135"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.137"),

                ingredients = HerbalIngredients(herbals.magnus_ligna),

                bases = new() { herbals.satius_ligna, },

                health = 50,

                stamina = 200,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.149"),
                    Mod.instance.Helper.Translation.Get("HerbalData.150"),
                    Mod.instance.Helper.Translation.Get("HerbalData.151"),
                }

            };

            potions[herbals.optimus_ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.optimus_ligna,

                container = IconData.relics.flask,

                content = IconData.relics.flask5,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("HerbalData.171"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.173"),

                ingredients = new() { },

                bases = new() { herbals.magnus_ligna, herbals.aether },

                health = 100,

                stamina = 400,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.185"),
                    Mod.instance.Helper.Translation.Get("HerbalData.186"),
                    Mod.instance.Helper.Translation.Get("HerbalData.187")
                }

            };

            // ====================================================================
            // Impes series

            potions[herbals.impes.ToString()] = new()
            {

                line = herbals.impes,

                herbal = herbals.impes,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle1,

                title = Mod.instance.Helper.Translation.Get("HerbalData.206"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.208"),

                ingredients = HerbalIngredients(herbals.impes),

                bases = new() { },

                health = 15,

                stamina = 40,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.220"),
                    Mod.instance.Helper.Translation.Get("HerbalData.221")
                }

            };


            potions[herbals.melius_impes.ToString()] = new()
            {

                line = herbals.impes,

                herbal = herbals.melius_impes,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle2,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("HerbalData.242"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.244"),

                ingredients = HerbalIngredients(herbals.melius_impes),

                bases = new() { herbals.impes, },

                health = 30,

                stamina = 80,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.256"),
                    Mod.instance.Helper.Translation.Get("HerbalData.257"),
                    Mod.instance.Helper.Translation.Get("HerbalData.258")
                }

            };


            potions[herbals.satius_impes.ToString()] = new()
            {

                line = herbals.impes,

                herbal = herbals.satius_impes,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle3,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("HerbalData.279"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.281"),

                ingredients = HerbalIngredients(herbals.satius_impes),

                bases = new() { herbals.melius_impes, },

                health = 45,

                stamina = 160,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.293"),
                    Mod.instance.Helper.Translation.Get("HerbalData.294"),

                    Mod.instance.Helper.Translation.Get("HerbalData.296")
                }

            };


            potions[herbals.magnus_impes.ToString()] = new()
            {

                line = HerbalData.herbals.impes,

                herbal = HerbalData.herbals.magnus_impes,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle4,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("HerbalData.317"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.319"),

                ingredients = HerbalIngredients(herbals.magnus_impes),

                bases = new() { herbals.satius_impes, },

                health = 70,

                stamina = 320,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.331"),
                    Mod.instance.Helper.Translation.Get("HerbalData.332"),
                    Mod.instance.Helper.Translation.Get("HerbalData.333")
                }

            };

            potions[herbals.optimus_impes.ToString()] = new()
            {

                line = HerbalData.herbals.impes,

                herbal = HerbalData.herbals.optimus_impes,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle5,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("HerbalData.353"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.355"),

                ingredients = new() { },

                bases = new() { herbals.magnus_impes, herbals.aether, },

                health = 180,

                stamina = 560,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.367"),
                    Mod.instance.Helper.Translation.Get("HerbalData.368"),
                    Mod.instance.Helper.Translation.Get("HerbalData.369")
                }

            };

            // ====================================================================
            // Celeri series

            potions[herbals.celeri.ToString()] = new()
            {

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.celeri,

                container = IconData.relics.vial,

                content = IconData.relics.vial1,

                title = Mod.instance.Helper.Translation.Get("HerbalData.388"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.390"),

                ingredients = HerbalIngredients(herbals.celeri),

                bases = new() { },

                health = 15,

                stamina = 25,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.402"),
                   Mod.instance.Helper.Translation.Get("HerbalData.403")
                }

            };

            potions[herbals.melius_celeri.ToString()] = new()
            {

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.melius_celeri,

                container = IconData.relics.vial,

                content = IconData.relics.vial2,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("HerbalData.423"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.425"),

                ingredients = HerbalIngredients(herbals.melius_celeri),

                health = 20,

                stamina = 60,

                bases = new() { herbals.celeri, },

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.437"),
                    Mod.instance.Helper.Translation.Get("HerbalData.438"),
                    Mod.instance.Helper.Translation.Get("HerbalData.439")
                }

            };


            potions[herbals.satius_celeri.ToString()] = new()
            {

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.satius_celeri,

                container = IconData.relics.vial,

                content = IconData.relics.vial3,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("HerbalData.460"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.462"),

                ingredients = HerbalIngredients(herbals.satius_celeri),

                health = 30,

                stamina = 120,

                bases = new() { herbals.melius_celeri, },

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.474"),
                    Mod.instance.Helper.Translation.Get("HerbalData.475"),
                    Mod.instance.Helper.Translation.Get("HerbalData.476")
                }

            };


            potions[herbals.magnus_celeri.ToString()] = new()
            {

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.magnus_celeri,

                container = IconData.relics.vial,

                content = IconData.relics.vial4,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("HerbalData.497"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.499"),

                ingredients = HerbalIngredients(herbals.magnus_celeri),

                health = 60,

                stamina = 240,

                bases = new() { herbals.satius_celeri, },

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.511"),
                    Mod.instance.Helper.Translation.Get("HerbalData.512"),
                    Mod.instance.Helper.Translation.Get("HerbalData.513")
                }

            };

            potions[herbals.optimus_celeri.ToString()] = new()
            {

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.optimus_celeri,

                container = IconData.relics.vial,

                content = IconData.relics.vial5,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("HerbalData.533"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.535"),

                ingredients = new() { },

                health = 120,

                stamina = 480,

                bases = new() { herbals.magnus_celeri, herbals.aether, },

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.547"),
                    Mod.instance.Helper.Translation.Get("HerbalData.548"),
                    Mod.instance.Helper.Translation.Get("HerbalData.549")
                }

            };

            // ====================================================================
            // Faeth

            potions[herbals.faeth.ToString()] = new()
            {

                line = HerbalData.herbals.faeth,

                herbal = HerbalData.herbals.faeth,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle5,

                level = 3,

                title = Mod.instance.Helper.Translation.Get("HerbalData.570"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.572"),

                ingredients = HerbalIngredients(herbals.faeth),

                bases = new() { },

                details = new()
                {

                    Mod.instance.Helper.Translation.Get("HerbalData.581"),
                    Mod.instance.Helper.Translation.Get("HerbalData.582"),

                },

                resource = true,

            };

            // ====================================================================
            // Ether

            potions[herbals.aether.ToString()] = new()
            {

                line = HerbalData.herbals.aether,

                herbal = HerbalData.herbals.aether,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle5,

                level = 4,

                title = Mod.instance.Helper.Translation.Get("HerbalData.606"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.608"),

                ingredients = HerbalIngredients(herbals.aether),

                bases = new() { },

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.616"),
                    Mod.instance.Helper.Translation.Get("HerbalData.617"),
                },

                resource = true,

            };
            return potions;

        }


        public void PotionBehaviour(int index)
        {

            HerbalData.herbals potion = herbals.ligna;

            switch (index)
            {

                case 11:

                    potion = herbals.impes;
                    break;

                case 17:

                    potion = herbals.celeri;
                    break;

            }

            PotionBehaviour(potion);

        }

        public void PotionBehaviour(HerbalData.herbals potion)
        {

            if (!Mod.instance.save.potions.ContainsKey(potion))
            {

                Mod.instance.save.potions.Add(potion, 1);

            }

            switch (Mod.instance.save.potions[potion])
            {

                case 0:
                    Mod.instance.save.potions[potion] = 1;
                    break;
                case 1:
                    Mod.instance.save.potions[potion] = 2;
                    break;
                case 2:
                    Mod.instance.save.potions[potion] = 0;
                    break;

            }

        }

        public void CheckHerbal(string id)
        {

            herbalism[id].status = CheckInventory(id);

        }

        public int CheckInventory(string id)
        {

            Herbal herbal = herbalism[id];

            herbalism[id].amounts.Clear();

            bool craftable = true;

            /*Dictionary<string, int> more = new()
            {
                [Mod.instance.Helper.Translation.Get("HerbalData.697")] = 2,

            };*/

            if (herbal.ingredients.Count > 0)
            {

                craftable = false;

                for (int i = 0; i < Game1.player.Items.Count; i++)
                {

                    Item checkSlot = Game1.player.Items[i];

                    if (checkSlot == null)
                    {

                        continue;

                    }

                    Item checkItem = checkSlot.getOne();

                    if (herbal.ingredients.Contains(@checkItem.QualifiedItemId))
                    {

                        int stack = Game1.player.Items[i].Stack;

                        /*if (more.ContainsKey(@checkItem.QualifiedItemId))
                        {

                            double revise = stack / 2;

                            stack = (int)Math.Floor(revise);

                        }*/

                        if (!herbalism[id].amounts.ContainsKey(@checkItem.QualifiedItemId))
                        {

                            herbalism[id].amounts[@checkItem.QualifiedItemId] = stack;

                        }
                        else
                        {

                            herbalism[id].amounts[@checkItem.QualifiedItemId] += stack;

                        }

                        craftable = true;

                    }

                }

            }

            if (Mod.instance.save.herbalism.ContainsKey(herbal.herbal))
            {

                if (Mod.instance.save.herbalism[herbal.herbal] == 999)
                {

                    return 3;

                }

            }

            if (herbal.bases.Count > 0)
            {

                foreach (herbals required in herbal.bases)
                {

                    if (!Mod.instance.save.herbalism.ContainsKey(required))
                    {

                        return 2;

                    }

                    if (Mod.instance.save.herbalism[required] == 0)
                    {

                        return 2;

                    }

                }

            }

            if (craftable)
            {

                return 1;

            }

            return 0;

        }

        public void MassBrew()
        {

            CharacterHandle.RetrieveInventory(CharacterHandle.characters.herbalism);

            int max = MaxHerbal();

            foreach (KeyValuePair<herbals, List<herbals>> line in lines)
            {

                foreach (herbals herbal in line.Value)
                {

                    string key = herbal.ToString();

                    if (herbalism[key].level > max)
                    {

                        continue;

                    }

                    BrewHerbal(key, 50, true);

                }

            }

            if (herbalism[herbals.aether.ToString()].level <= max)
            {

                BrewHerbal(herbals.aether.ToString(), 50, true);

            }

            if (herbalism[herbals.faeth.ToString()].level <= max)
            {

                BrewHerbal(herbals.faeth.ToString(), 50, true);

            }

        }

        public void BrewHerbal(string id, int draught, bool bench = false)
        {

            Herbal herbal = herbalism[id];

            int brewed = 0;

            if (Mod.instance.save.herbalism.ContainsKey(herbal.herbal))
            {

                draught = Math.Min(999 - Mod.instance.save.herbalism[herbal.herbal], draught);

            }

            if (draught == 0)
            {

                return;

            }

            /*Dictionary<string, int> more = new()
            {
                [Mod.instance.Helper.Translation.Get("HerbalData.869")] = 2,

            };*/

            if (herbal.bases.Count > 0)
            {

                foreach (herbals required in herbal.bases)
                {

                    if (Mod.instance.save.herbalism.ContainsKey(required))
                    {

                        draught = Math.Min(Mod.instance.save.herbalism[required], draught);

                    }
                    else
                    {

                        return;

                    }

                }

            }

            if (draught == 0)
            {

                return;

            }

            if (herbal.ingredients.Count > 0)
            {


                if (bench)
                {

                    for (int i = 0; i < Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items.Count; i++)
                    {

                        if (brewed >= draught)
                        {

                            break;

                        }

                        Item checkSlot = Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items[i];

                        if (checkSlot == null)
                        {

                            continue;

                        }

                        Item checkItem = checkSlot.getOne();

                        if (herbal.ingredients.Contains(@checkItem.QualifiedItemId))
                        {

                            int stack = Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items[i].Stack;

                            int cost = 1;

                            /* if (more.ContainsKey(@checkItem.QualifiedItemId))
                             {

                                 double revise = stack / 2;

                                 stack = (int)Math.Floor(revise);

                                 if (stack == 0)
                                 {

                                     continue;

                                 }

                                 cost = 2;

                             }*/

                            int brew = Math.Min(stack, (draught - brewed));

                            if (herbal.bases.Count > 0)
                            {

                                foreach (herbals required in herbal.bases)
                                {

                                    Mod.instance.save.herbalism[required] -= brew;

                                }

                            }

                            Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items[i].Stack -= (brew * cost);

                            brewed += brew;

                        }

                    }

                    for (int i = Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items.Count - 1; i >= 0; i--)
                    {

                        if (Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items[i].Stack <= 0)
                        {

                            Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items.RemoveAt(i);

                        }

                    }

                }

                for (int i = 0; i < Game1.player.Items.Count; i++)
                {

                    if (brewed >= draught)
                    {

                        break;

                    }

                    Item checkSlot = Game1.player.Items[i];

                    if (checkSlot == null)
                    {

                        continue;

                    }

                    Item checkItem = checkSlot.getOne();

                    if (herbal.ingredients.Contains(@checkItem.QualifiedItemId))
                    {

                        int stack = Game1.player.Items[i].Stack;

                        int cost = 1;

                        /*if (more.ContainsKey(@checkItem.QualifiedItemId))
                        {

                            double revise = stack / 2;

                            stack = (int)Math.Floor(revise);

                            if (stack == 0)
                            {

                                continue;

                            }

                            cost = 2;

                        }*/

                        int brew = Math.Min(stack, (draught - brewed));

                        if (herbal.bases.Count > 0)
                        {

                            foreach (herbals required in herbal.bases)
                            {

                                Mod.instance.save.herbalism[required] -= brew;

                            }

                        }

                        Game1.player.Items[i].Stack -= (brew * cost);

                        if (Game1.player.Items[i].Stack <= 0)
                        {

                            Game1.player.Items[i] = null;

                        }

                        brewed += brew;

                    }

                }

            }
            else if (herbal.bases.Count > 0)
            {

                foreach (herbals required in herbal.bases)
                {

                    Mod.instance.save.herbalism[required] -= draught;

                }

                brewed = draught;

            }

            CheckHerbal(id);

            Game1.player.currentLocation.playSound(SpellHandle.sounds.bubbles.ToString());

            if (!Mod.instance.save.herbalism.ContainsKey(herbal.herbal))
            {

                Mod.instance.save.herbalism[herbal.herbal] = brewed;

                return;

            }

            Mod.instance.save.herbalism[herbal.herbal] += brewed;


        }

        public void ConsumeHerbal(string id)
        {

            Herbal herbal = herbalism[id];

            Game1.player.Stamina = Math.Min(Game1.player.MaxStamina, Game1.player.Stamina + herbal.stamina);

            Game1.player.health = Math.Min(Game1.player.maxHealth, Game1.player.health + herbal.health);

            Microsoft.Xna.Framework.Rectangle healthBox = Game1.player.GetBoundingBox();

            if (Game1.currentGameTime.TotalGameTime.TotalSeconds > consumeBuffer)
            {

                consumeBuffer = Game1.currentGameTime.TotalGameTime.TotalSeconds + 5;

                DisplayPotion hudmessage = new(Mod.instance.Helper.Translation.Get("HerbalData.1116") + herbal.title, herbal);

                Game1.addHUDMessage(hudmessage);

            }

            if (herbal.level > 0)
            {

                if (applied.ContainsKey(herbal.line))
                {

                    if (applied[herbal.line].level < herbal.level)
                    {

                        applied[herbal.line].level = herbal.level;

                        applyChange = true;

                    }

                }
                else
                {

                    applied[herbal.line] = new();

                    applied[herbal.line].level = herbal.level;

                    applied[herbal.line].counter = 0;

                    applyChange = true;

                }

                applied[herbal.line].counter += herbal.duration;

                if (applied[herbal.line].counter >= 999)
                {

                    applied[herbal.line].counter = 999;

                }

            }

            Mod.instance.save.herbalism[herbal.herbal] -= 1;

            //HerbalBuff();

        }

        public void HerbalBuff()
        {

            if (applied.Count == 0)
            {

                return;

            }

            string description = string.Empty;

            float speed = 0f;

            int magnetism = 0;

            for (int i = applied.Count - 1; i >= 0; i--)
            {

                KeyValuePair<herbals, HerbalBuff> herbBuff = applied.ElementAt(i);

                applied[herbBuff.Key].counter -= 1;

                if (applied[herbBuff.Key].counter <= 0)
                {

                    applied.Remove(herbBuff.Key);

                    applyChange = true;

                    continue;

                }

                int timeOfDay = Game1.timeOfDay;

                int addTime = applied[herbBuff.Key].counter;

                while (addTime > 0)
                {

                    int updateTime = Math.Min(30, addTime);

                    timeOfDay += updateTime;

                    if (timeOfDay % 100 > 60)
                    {

                        timeOfDay += 40;

                    }

                    addTime -= updateTime;

                    if (timeOfDay > 2599)
                    {

                        timeOfDay = 2600;

                        break;

                    }

                }

                string meridian = Mod.instance.Helper.Translation.Get("HerbalData.1233");

                if (timeOfDay >= 2400)
                {

                    timeOfDay -= 2400;

                }
                else if (timeOfDay >= 1200)
                {

                    timeOfDay -= 1200;

                    meridian = Mod.instance.Helper.Translation.Get("HerbalData.1246");

                }

                string tODs = timeOfDay.ToString();

                for(int l = 0; l < 4 - tODs.Length; l++)
                {

                    tODs = "0" + tODs;

                }

                string expire = tODs.Substring(0,1) + tODs.Substring(1, 1) + ":" + tODs.Substring(2, 1) + "0" + meridian;

                switch(herbBuff.Key)
                {

                    case herbals.ligna:

                        description += Mod.instance.Helper.Translation.Get("HerbalData.1266") + herbBuff.Value.level.ToString() + " " + expire + ". ";

                        magnetism = herbBuff.Value.level * 32;

                        break;

                    case herbals.impes:

                        description += Mod.instance.Helper.Translation.Get("HerbalData.1274") + herbBuff.Value.level.ToString() + " " + expire + ". ";

                        break;

                    case herbals.celeri:

                        speed = 0.25f * herbBuff.Value.level;

                        description += Mod.instance.Helper.Translation.Get("HerbalData.1282") + herbBuff.Value.level.ToString() + " " + expire + ". ";

                        break;

                }

            }

            if (!applyChange)
            {

                return;

            }

            if (Game1.player.buffs.IsApplied(184652.ToString()))
            {

                Game1.player.buffs.Remove(184652.ToString());

            }

            if (applied.Count == 0)
            {

                return;

            }

            Buff herbalBuff = new(
                184652.ToString(),
                source: Mod.instance.Helper.Translation.Get("HerbalData.1313"),
                displaySource: Mod.instance.Helper.Translation.Get("HerbalData.1314"),
                duration: Buff.ENDLESS,
                iconTexture: Mod.instance.iconData.displayTexture,
                iconSheetIndex: 5,
                displayName: Mod.instance.Helper.Translation.Get("HerbalData.1318"),
                description: description
                );

            if (speed > 0f)
            {

                BuffEffects buffEffect = new();

                buffEffect.Speed.Set(speed);

                herbalBuff.effects.Add(buffEffect);

            }

            if (magnetism > 0)
            {

                BuffEffects buffEffect = new();

                buffEffect.MagneticRadius.Set(magnetism);

                herbalBuff.effects.Add(buffEffect);

            }

            Game1.player.buffs.Apply(herbalBuff);

            applyChange = false;

        }

        public void ConvertGeodes()
        {
            
            CharacterHandle.RetrieveInventory(CharacterHandle.characters.herbalism);

            Dictionary<string,StardewValley.Item> extracts = new();

            for (int i = 0; i < Game1.player.Items.Count; i++)
            {

                Item checkSlot = Game1.player.Items[i];

                if (checkSlot == null)
                {

                    continue;

                }

                Item checkItem = checkSlot.getOne();

                if (Utility.IsGeode(checkItem))
                {

                    for(int e = Game1.player.Items[i].Stack - 1; e >= 0;  e--)
                    {

                        Item extraction = Utility.getTreasureFromGeode(checkItem);

                        if (checkItem.QualifiedItemId.Contains("MysteryBox"))
                        {
                            
                            Game1.stats.Increment("MysteryBoxesOpened", 1);
                        
                        }
                        else
                        {
                            
                            Game1.stats.GeodesCracked++;
                        
                        }

                        if (extracts.ContainsKey(extraction.QualifiedItemId))
                        {

                            extracts[extraction.QualifiedItemId].Stack += extraction.Stack;

                        }
                        else
                        {

                            extracts[extraction.QualifiedItemId] = extraction;

                        }

                        Game1.player.Items[i] = null;

                    }

                }

            }

            Vector2 origin = (Mod.instance.locations[Location.LocationData.druid_grove_name] as Grove).herbalTiles.First().position + new Vector2(64,32);

            foreach(KeyValuePair<string,StardewValley.Item> extract in extracts)
            {
                
                if (Mod.instance.chests[Character.CharacterHandle.characters.herbalism].addItem(extract.Value) != null)
                {
                    ThrowHandle throwExtract = new(Game1.player, origin, extract.Value);

                    throwExtract.register();

                }

            }

            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -60, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.secret1 });
            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -45, sound = SpellHandle.sounds.silent, });
            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -30, sound = SpellHandle.sounds.silent, });
            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -15, sound = SpellHandle.sounds.silent, });
            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4,  sound = SpellHandle.sounds.thunder, });

            Microsoft.Xna.Framework.Rectangle relicRect = IconData.RelicRectangles(IconData.relics.crow_hammer);

            TemporaryAnimatedSprite animation = new(0,1500, 1, 1, origin - new Vector2(16,60), false, false)
            {
                sourceRect = relicRect,
                sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                texture = Mod.instance.iconData.relicsTexture,
                layerDepth = 900f,
                rotation = -0.76f,
                scale = 4f,
            };

            Game1.player.currentLocation.TemporarySprites.Add(animation);

            Mod.instance.spellRegister.Add(new(origin-new Vector2(24,32), 320, IconData.impacts.nature, new()) { type = SpellHandle.spells.effect, counter = -45, instant = true, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.yoba });

            Mod.instance.spellRegister.Add(new(origin - new Vector2(24,32), 320, IconData.impacts.nature, new()) { type = SpellHandle.spells.effect, instant = true, scheme = IconData.schemes.golden, });

        }

    }

    public class Herbal
    {

        // -----------------------------------------------
        // journal

        public HerbalData.herbals line = HerbalData.herbals.none;

        public HerbalData.herbals herbal = HerbalData.herbals.none;

        public IconData.relics container = IconData.relics.flask;

        public IconData.relics content = IconData.relics.flask1;

        public string title;

        public string description;

        public List<string> ingredients = new();

        public List<HerbalData.herbals> bases = new();

        public List<string> details = new();

        public int status;

        public Dictionary<string, int> amounts = new();

        public int level;

        public int duration;

        public int health;

        public int stamina;

        public bool resource;

    }

    public class HerbalBuff
    {

        // ----------------------------------------------------

        public HerbalData.herbals line = HerbalData.herbals.none;

        public int counter;

        public int level;

    }


}
