
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Dialogue;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.GameData.BigCraftables;
using StardewValley.GameData.Machines;
using StardewValley.GameData.Shops;
using StardewValley.Monsters;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using StardewDruid.Journal;

namespace StardewDruid.Data
{
    public class HerbalData
    {

        public enum herbals
        {

            none,

            // potions

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
            voil,

            // powders

            imbus,
            amori,
            donis,
            concutere,
            jumere,
            felis,

        }

        public Dictionary<string, Herbal> herbalism = new();

        public enum herbalbuffs
        {
            none,
            alignment,
            vigor,
            celerity,
            imbuement,
            amorous,
            donor,
            concussion,
            jumper,
            feline,
        }

        public Dictionary<herbalbuffs, HerbalBuff> applied = new();

        public Dictionary<herbals, int> orders = new();

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

                        "(O)80", // "Quartz", 
                        "(O)86", // "Earth Crystal", 
                        "(O)330", // "Clay",
                        "(O)881", // "Bone Fragment", 
                        "(O)168", // "Trash", 
                        "(O)169", // "Driftwood", 
                        "(O)170", // "Broken Glasses", 
                        "(O)171", // "Broken CD",
                    };

                case herbals.melius_celeri:
                    return new() {

                        "(O)815", // "Tea Leaves", 
                        "(O)433", // "Coffee Bean", 
                        "(O)167", // "Joja Cola", 
                        "(O)157", // "White Algae",
                        "(O)152", // "Algae", 
                        "(O)153", // "Seaweed", 
 
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

                case herbals.voil:

                    return new() {

                        "(O)749",
                        "(O)579",
                        "(O)580",
                        "(O)581",
                        "(O)582",
                        "(O)583",
                        "(O)584",
                        "(O)585",

                    };

                case herbals.imbus:

                    return new() { "(O)709", };

                case herbals.amori:

                    return new() { "(O)395", };

                case herbals.donis:

                    return new() { "(O)338", };

                case herbals.concutere:

                    return new() { "(BC)71", };

                case herbals.jumere:

                    return new() { "(O)306", "(O)307", };

                case herbals.felis:

                    return new() { "(O)184", "(O)186", };

            }

            return new();

        }

        public Dictionary<herbals, List<string>> titles = new()
        {
            [herbals.ligna] = new() {
                Mod.instance.Helper.Translation.Get("HerbalData.1"),
                Mod.instance.Helper.Translation.Get("HerbalData.2"),
            },
            [herbals.impes] = new() {
                Mod.instance.Helper.Translation.Get("HerbalData.3"),
                Mod.instance.Helper.Translation.Get("HerbalData.4"),
            },
            [herbals.celeri] = new() {
                Mod.instance.Helper.Translation.Get("HerbalData.5"),
                Mod.instance.Helper.Translation.Get("HerbalData.6"),
            },
            [herbals.faeth] = new() {
                Mod.instance.Helper.Translation.Get("HerbalData.7"),
                Mod.instance.Helper.Translation.Get("HerbalData.8"),
            },
        };

        public Dictionary<herbals, IconData.schemes> schemes = new()
        {
            [herbals.ligna] = IconData.schemes.herbal_ligna,
            [herbals.impes] = IconData.schemes.herbal_impes,
            [herbals.celeri] = IconData.schemes.herbal_celeri,
            [herbals.faeth] = IconData.schemes.herbal_faeth,
            [herbals.aether] = IconData.schemes.herbal_aether,
            [herbals.voil] = IconData.schemes.herbal_voil,

        };

        public Dictionary<herbals, List<herbals>> lines = new()
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

            herbals.voil,

        };

        public List<herbals> bombLayout = new()
        {

            herbals.imbus,
            herbals.amori,
            herbals.donis,
            herbals.concutere,
            herbals.jumere,
            herbals.felis,

        };

        public double consumeBuffer;

        public HerbalData()
        {

        }

        public void LoadHerbals()
        {

            herbalism = HerbalList();

        }

        public void Ready()
        {

            orders.Clear();

            int max = MaxHerbal();

            List<herbals> candidates = new();

            foreach (herbals herbal in herbalLayout)
            {

                if (!herbalism.ContainsKey(herbal.ToString()))
                {

                    continue;

                }

                if (herbalism[herbal.ToString()].level - 1 > max)
                {

                    continue;

                }

                candidates.Add(herbal);

            }

            for (int i = 0; i < Mod.instance.randomIndex.Next(4, 7); i++)
            {

                herbals candidate = candidates[Mod.instance.randomIndex.Next(candidates.Count)];

                switch (Mod.instance.randomIndex.Next(10))
                {

                    case 0:

                        orders[candidate] = 1;

                        break;

                    case 1:
                    case 2:
                    case 3:
                    case 4:

                        orders[candidate] = 5;

                        break;

                    case 5:
                    case 6:
                    case 7:

                        orders[candidate] = 10;

                        break;

                    case 8:
                    case 9:

                        orders[candidate] = 20;

                        break;


                }

            }

        }

        public int MaxHerbal()
        {

            if (RelicData.HasRelic(IconData.relics.herbalism_gauge))
            {

                return 4;

            }
            if (RelicData.HasRelic(IconData.relics.herbalism_still))
            {

                return 3;

            }
            if (RelicData.HasRelic(IconData.relics.herbalism_pan))
            {

                return 2;

            }
            if (RelicData.HasRelic(IconData.relics.herbalism_mortar))
            {

                return 1;

            }

            return -1;

        }

        public Dictionary<int, ContentComponent> JournalHerbals()
        {

            Dictionary<int, ContentComponent> journal = new();

            int max = MaxHerbal();

            int start = 0;

            foreach (herbals herbal in herbalLayout)
            {

                string key = herbal.ToString();

                if (herbalism[key].level == 99)
                {

                    switch (herbal)
                    {

                        case herbals.aether:

                            if (!RelicData.HasRelic(IconData.relics.herbalism_gauge))
                            {

                                ContentComponent blank = new(ContentComponent.contentTypes.potion, key, false);

                                journal[start++] = blank;

                                ContentComponent blankToggle = new(ContentComponent.contentTypes.toggle, key, false);

                                journal[start++] = blankToggle;

                                continue;

                            }

                            break;

                        case herbals.faeth:

                            if (!RelicData.HasRelic(IconData.relics.herbalism_crucible))
                            {

                                ContentComponent blank = new(ContentComponent.contentTypes.potion, key, false);

                                journal[start++] = blank;

                                ContentComponent blankToggle = new(ContentComponent.contentTypes.toggle, key, false);

                                journal[start++] = blankToggle;

                                continue;

                            }

                            break;

                        case herbals.voil:

                            if (!RelicData.HasRelic(IconData.relics.herbalism_crucible))
                            {

                                ContentComponent blank = new(ContentComponent.contentTypes.potion, key, false);

                                journal[start++] = blank;

                                ContentComponent blankToggle = new(ContentComponent.contentTypes.toggle, key, false);

                                journal[start++] = blankToggle;

                                continue;

                            }

                            break;


                    }

                }
                else if (herbalism[key].level > max)
                {

                    ContentComponent blank = new(ContentComponent.contentTypes.potion, key, false);

                    journal[start++] = blank;

                    ContentComponent blankToggle = new(ContentComponent.contentTypes.toggle, key, false);

                    journal[start++] = blankToggle;

                    continue;

                }

                // =============================================================== potion button

                ContentComponent content = new(ContentComponent.contentTypes.potion, key);

                Mod.instance.herbalData.CheckHerbal(key);

                int amount = UpdateHerbalism(herbal);

                string amountString = amount.ToString();

                content.text[0] = amount.ToString();

                content.potions[0] = herbalism[key].display;

                if (amount == 0)
                {
                    content.potions[0] = herbalism[key].grayed;
                }

                journal[start++] = content;

                // =============================================================== active button

                ContentComponent toggle = new(ContentComponent.contentTypes.toggle, key);

                IconData.displays flag = IconData.displays.complete;

                StringData.stringkeys hovertext = StringData.stringkeys.acEnabled;

                if (Mod.instance.save.potions.ContainsKey(herbal))
                {

                    switch (Mod.instance.save.potions[herbal])
                    {

                        case 0:

                            flag = IconData.displays.exit;

                            hovertext = StringData.stringkeys.acDisabled;

                            break;

                        case 1:

                            flag = IconData.displays.complete;

                            break;

                        case 2:

                            flag = IconData.displays.flag;

                            hovertext = StringData.stringkeys.acPriority;

                            break;

                        case 3:

                            flag = IconData.displays.active;

                            hovertext = StringData.stringkeys.acIgnored;

                            break;

                        case 4:

                            flag = IconData.displays.skull;

                            hovertext = StringData.stringkeys.acRestricted;

                            break;

                    }
                }

                toggle.icons[0] = flag;

                toggle.text[0] = StringData.Strings(hovertext);

                journal[start++] = toggle;

            }

            return journal;

        }

        public Dictionary<int, ContentComponent> JournalBombs()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            foreach (herbals herbal in bombLayout)
            {

                string key = herbal.ToString();

                // =============================================================== potion button

                ContentComponent content = new(ContentComponent.contentTypes.potion, key);

                Mod.instance.herbalData.CheckHerbal(key);

                int amount = UpdateHerbalism(herbal);

                string amountString = amount.ToString();

                content.text[0] = amount.ToString();

                content.potions[0] = herbalism[key].display;

                if (amount == 0)
                {
                    content.potions[0] = herbalism[key].grayed;
                }

                journal[start++] = content;

                // =============================================================== active button

                ContentComponent toggle = new(ContentComponent.contentTypes.toggle, key);

                IconData.displays flag = IconData.displays.complete;

                StringData.stringkeys hovertext = StringData.stringkeys.acEnabled;

                if (Mod.instance.save.potions.ContainsKey(herbal))
                {

                    switch (Mod.instance.save.potions[herbal])
                    {

                        case 0:

                            flag = IconData.displays.exit;

                            hovertext = StringData.stringkeys.acDisabled;

                            break;

                        case 1:

                            flag = IconData.displays.complete;

                            break;

                        case 2:

                            flag = IconData.displays.flag;

                            hovertext = StringData.stringkeys.acPriority;

                            break;

                        case 3:

                            flag = IconData.displays.active;

                            hovertext = StringData.stringkeys.acIgnored;

                            break;

                        case 4:

                            flag = IconData.displays.skull;

                            hovertext = StringData.stringkeys.acRestricted;

                            break;

                    }
                }

                toggle.icons[0] = flag;

                toggle.text[0] = StringData.Strings(hovertext);

                journal[start++] = toggle;

            }

            return journal;

        }

        public Dictionary<int, ContentComponent> TradeHerbals()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            foreach (KeyValuePair<herbals, int> order in orders)
            {

                string herb = order.Key.ToString();

                ContentComponent content = new(ContentComponent.contentTypes.potionlist, herb);

                Herbal herbalData = herbalism[herb];

                content.text[0] = herbalData.title + " " + StringData.Strings(StringData.stringkeys.multiplier) + order.Value.ToString();

                content.text[1] = (herbalData.price * order.Value).ToString() + StringData.Strings(StringData.stringkeys.currency);

                content.potions[0] = herbalism[herb].display;

                int amount = UpdateHerbalism(order.Key);

                if (amount < order.Value)
                {

                    content.potions[0] = herbalism[herb].grayed;

                    content.text[0] += " " + Mod.instance.Helper.Translation.Get("DialogueData.347.5").Tokens(new { stock = (order.Value - amount).ToString() });

                }

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

                buff = herbalbuffs.none,

                herbal = herbals.ligna,

                display = IconData.potions.ligna,

                grayed = IconData.potions.lignaGray,

                level = 0,

                duration = 0,

                title = Mod.instance.Helper.Translation.Get("HerbalData.25"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.27"),

                ingredients = HerbalIngredients(herbals.ligna),

                bases = new() { },

                health = 4,

                stamina = 12,

                price = 50,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.39"),
                    Mod.instance.Helper.Translation.Get("HerbalData.40")
                },

            };


            potions[herbals.melius_ligna.ToString()] = new()
            {

                buff = herbalbuffs.alignment,

                herbal = herbals.melius_ligna,

                display = IconData.potions.ligna1,

                grayed = IconData.potions.lignaGray1,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("HerbalData.61"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.63"),

                ingredients = HerbalIngredients(herbals.melius_ligna),

                bases = new() { herbals.ligna, },

                health = 8,

                stamina = 30,

                price = 150,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.75"),
                    Mod.instance.Helper.Translation.Get("HerbalData.76"),
                    Mod.instance.Helper.Translation.Get("HerbalData.77")
                }

            };


            potions[herbals.satius_ligna.ToString()] = new()
            {

                buff = herbalbuffs.alignment,

                herbal = herbals.satius_ligna,

                display = IconData.potions.ligna2,

                grayed = IconData.potions.lignaGray2,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("HerbalData.98"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.100"),

                ingredients = HerbalIngredients(herbals.satius_ligna),

                bases = new() { herbals.melius_ligna, },

                health = 20,

                stamina = 80,

                price = 300,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.112"),
                    Mod.instance.Helper.Translation.Get("HerbalData.113"),
                    Mod.instance.Helper.Translation.Get("HerbalData.114")
                }

            };


            potions[herbals.magnus_ligna.ToString()] = new()
            {

                buff = herbalbuffs.alignment,

                herbal = herbals.magnus_ligna,

                display = IconData.potions.ligna3,

                grayed = IconData.potions.lignaGray3,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("HerbalData.135"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.137"),

                ingredients = HerbalIngredients(herbals.magnus_ligna),

                bases = new() { herbals.satius_ligna, },

                health = 50,

                stamina = 200,

                price = 600,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.149"),
                    Mod.instance.Helper.Translation.Get("HerbalData.150"),
                    Mod.instance.Helper.Translation.Get("HerbalData.151"),
                }

            };

            potions[herbals.optimus_ligna.ToString()] = new()
            {

                buff = herbalbuffs.alignment,

                herbal = herbals.optimus_ligna,

                display = IconData.potions.ligna4,

                grayed = IconData.potions.lignaGray4,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("HerbalData.171"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.173"),

                ingredients = new() { },

                bases = new() { herbals.magnus_ligna, herbals.aether },

                health = 100,

                stamina = 400,

                price = 1200,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.185"),
                    Mod.instance.Helper.Translation.Get("HerbalData.186"),
                    Mod.instance.Helper.Translation.Get("HerbalData.187")
                }

            };

            // ====================================================================
            // Impes series

            potions[herbals.impes.ToString()] = new()
            {

                buff = herbalbuffs.none,

                herbal = herbals.impes,

                display = IconData.potions.impes,

                grayed = IconData.potions.impesGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.206"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.208"),

                ingredients = HerbalIngredients(herbals.impes),

                bases = new() { },

                health = 12,

                stamina = 40,

                price = 50,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.220"),
                    Mod.instance.Helper.Translation.Get("HerbalData.221")
                }

            };


            potions[herbals.melius_impes.ToString()] = new()
            {

                buff = herbalbuffs.vigor,

                herbal = herbals.melius_impes,

                display = IconData.potions.impes1,

                grayed = IconData.potions.impesGray1,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("HerbalData.242"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.244"),

                ingredients = HerbalIngredients(herbals.melius_impes),

                bases = new() { herbals.impes, },

                health = 30,

                stamina = 80,

                price = 150,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.256"),
                    Mod.instance.Helper.Translation.Get("HerbalData.257"),
                    Mod.instance.Helper.Translation.Get("HerbalData.258")
                }

            };


            potions[herbals.satius_impes.ToString()] = new()
            {

                buff = herbalbuffs.vigor,

                herbal = herbals.satius_impes,

                display = IconData.potions.impes2,

                grayed = IconData.potions.impesGray2,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("HerbalData.279"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.281"),

                ingredients = HerbalIngredients(herbals.satius_impes),

                bases = new() { herbals.melius_impes, },

                health = 45,

                stamina = 160,

                price = 300,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.293"),
                    Mod.instance.Helper.Translation.Get("HerbalData.294"),

                    Mod.instance.Helper.Translation.Get("HerbalData.296")
                }

            };


            potions[herbals.magnus_impes.ToString()] = new()
            {

                buff = herbalbuffs.vigor,

                herbal = herbals.magnus_impes,

                display = IconData.potions.impes3,

                grayed = IconData.potions.impesGray3,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("HerbalData.317"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.319"),

                ingredients = HerbalIngredients(herbals.magnus_impes),

                bases = new() { herbals.satius_impes, },

                health = 70,

                stamina = 320,

                price = 600,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.331"),
                    Mod.instance.Helper.Translation.Get("HerbalData.332"),
                    Mod.instance.Helper.Translation.Get("HerbalData.333")
                }

            };

            potions[herbals.optimus_impes.ToString()] = new()
            {

                buff = herbalbuffs.vigor,

                herbal = herbals.optimus_impes,

                display = IconData.potions.impes4,

                grayed = IconData.potions.impesGray4,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("HerbalData.353"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.355"),

                ingredients = new() { },

                bases = new() { herbals.magnus_impes, herbals.aether, },

                health = 180,

                stamina = 560,

                price = 1200,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.367"),
                    Mod.instance.Helper.Translation.Get("HerbalData.368"),
                    Mod.instance.Helper.Translation.Get("HerbalData.369")
                }

            };

            // ====================================================================
            // Celeri series

            potions[herbals.celeri.ToString()] = new()
            {

                buff = herbalbuffs.none,

                herbal = herbals.celeri,

                display = IconData.potions.celeri,

                grayed = IconData.potions.celeriGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.388"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.390"),


                ingredients = HerbalIngredients(herbals.celeri),

                bases = new() { },

                health = 8,

                stamina = 25,

                price = 50,

                details = new()
                {
                   //Mod.instance.Helper.Translation.Get("HerbalData.402"),
                   Mod.instance.Helper.Translation.Get("HerbalData.403")
                }

            };

            potions[herbals.melius_celeri.ToString()] = new()
            {

                buff = herbalbuffs.celerity,

                herbal = herbals.melius_celeri,

                display = IconData.potions.celeri1,

                grayed = IconData.potions.celeriGray1,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("HerbalData.423"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.425"),

                ingredients = HerbalIngredients(herbals.melius_celeri),

                health = 20,

                stamina = 60,

                price = 150,

                bases = new() { herbals.celeri, },

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.437"),
                    Mod.instance.Helper.Translation.Get("HerbalData.438"),
                    Mod.instance.Helper.Translation.Get("HerbalData.439")
                }

            };


            potions[herbals.satius_celeri.ToString()] = new()
            {

                buff = herbalbuffs.celerity,

                herbal = herbals.satius_celeri,

                display = IconData.potions.celeri2,

                grayed = IconData.potions.celeriGray2,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("HerbalData.460"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.462"),

                ingredients = HerbalIngredients(herbals.satius_celeri),

                health = 40,

                stamina = 120,

                price = 300,

                bases = new() { herbals.melius_celeri, },

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.474"),
                    Mod.instance.Helper.Translation.Get("HerbalData.475"),
                    Mod.instance.Helper.Translation.Get("HerbalData.476")
                }

            };


            potions[herbals.magnus_celeri.ToString()] = new()
            {

                buff = herbalbuffs.celerity,

                herbal = herbals.magnus_celeri,

                display = IconData.potions.celeri3,

                grayed = IconData.potions.celeriGray3,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("HerbalData.497"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.499"),

                ingredients = HerbalIngredients(herbals.magnus_celeri),

                health = 80,

                stamina = 240,

                price = 600,

                bases = new() { herbals.satius_celeri, },

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.511"),
                    Mod.instance.Helper.Translation.Get("HerbalData.512"),
                    Mod.instance.Helper.Translation.Get("HerbalData.513")
                }

            };

            potions[herbals.optimus_celeri.ToString()] = new()
            {

                buff = herbalbuffs.celerity,

                herbal = herbals.optimus_celeri,

                display = IconData.potions.celeri4,

                grayed = IconData.potions.celeriGray4,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("HerbalData.533"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.535"),

                ingredients = new() { },

                health = 120,

                stamina = 480,

                price = 1200,

                bases = new() { herbals.magnus_celeri, herbals.aether, },

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.547"),
                    Mod.instance.Helper.Translation.Get("HerbalData.548"),
                    Mod.instance.Helper.Translation.Get("HerbalData.549")
                }

            };

            // ====================================================================
            // Faeth

            potions[herbals.faeth.ToString()] = new()
            {

                buff = herbalbuffs.none,

                herbal = herbals.faeth,

                display = IconData.potions.faeth,

                grayed = IconData.potions.faethGray,

                level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.570"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.572"),

                ingredients = HerbalIngredients(herbals.faeth),

                bases = new() { },

                price = 250,

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

                buff = herbalbuffs.none,

                herbal = herbals.aether,

                display = IconData.potions.aether,

                grayed = IconData.potions.aetherGray,

                level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.606"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.608"),

                ingredients = HerbalIngredients(herbals.aether),

                bases = new() { },

                price = 500,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.616"),
                    Mod.instance.Helper.Translation.Get("HerbalData.617"),
                },

                resource = true,


            };

            // ====================================================================
            // Voil

            potions[herbals.voil.ToString()] = new()
            {

                buff = herbalbuffs.none,

                herbal = herbals.voil,

                display = IconData.potions.voil,

                grayed = IconData.potions.voilGray,

                level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.1"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.2"),

                ingredients = HerbalIngredients(herbals.voil),

                bases = new() { },

                price = 250,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.3"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.4"),
                },

                resource = true,

            };


            // ====================================================================
            // Powders

            potions[herbals.imbus.ToString()] = new()
            {

                buff = herbalbuffs.imbuement,

                herbal = herbals.imbus,

                display = IconData.potions.imbus,

                grayed = IconData.potions.imbusGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.5"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.6"),

                ingredients = HerbalIngredients(herbals.imbus),

                bases = new() { herbals.melius_ligna, },

                duration = 600,

                price = 50,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.7"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.8"),
                },

            };

            potions[herbals.amori.ToString()] = new()
            {

                buff = herbalbuffs.amorous,

                herbal = herbals.amori,

                display = IconData.potions.amori,

                grayed = IconData.potions.amoriGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.9"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.10"),

                ingredients = HerbalIngredients(herbals.amori),

                bases = new() { herbals.melius_impes, },

                duration = 600,

                price = 50,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.11"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.12"),
                },

            };

            potions[herbals.donis.ToString()] = new()
            {

                buff = herbalbuffs.donor,

                herbal = herbals.donis,

                display = IconData.potions.donis,

                grayed = IconData.potions.donisGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.13"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.14"),

                ingredients = HerbalIngredients(herbals.donis),

                bases = new() { herbals.melius_celeri, },

                duration = 600,

                price = 50,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.15"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.16"),
                },

            };

            // ====================================================================
            // Powders (Stronger)

            potions[herbals.concutere.ToString()] = new()
            {

                buff = herbalbuffs.concussion,

                herbal = herbals.concutere,

                display = IconData.potions.concutere,

                grayed = IconData.potions.concutereGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.17"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.18"),

                ingredients = HerbalIngredients(herbals.concutere),

                bases = new() { herbals.voil, },

                duration = 600,

                price = 100,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.19"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.20"),
                },

            };

            potions[herbals.jumere.ToString()] = new()
            {

                buff = herbalbuffs.jumper,

                herbal = herbals.jumere,

                display = IconData.potions.jumere,

                grayed = IconData.potions.jumereGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.21"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.22"),

                ingredients = HerbalIngredients(herbals.jumere),

                bases = new() { herbals.voil, },

                duration = 600,

                price = 100,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.23"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.24"),
                },

            };

            potions[herbals.felis.ToString()] = new()
            {

                buff = herbalbuffs.feline,

                herbal = herbals.felis,

                display = IconData.potions.felis,

                grayed = IconData.potions.felisGray,

                //level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.25"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.26"),

                ingredients = HerbalIngredients(herbals.felis),

                bases = new() { herbals.voil, },

                duration = 600,

                price = 100,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.27"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.28"),
                },

            };

            return potions;

        }

        public static int UpdateHerbalism(herbals herbal, int amount = 0)
        {

            if (amount >= 0)
            {

                if (Mod.instance.save.herbalism.ContainsKey(herbal))
                {

                    if (amount != 0)
                    {
                        int addition = Math.Min(amount, 999 - Mod.instance.save.herbalism[herbal]);

                        Mod.instance.save.herbalism[herbal] += amount;

                    }

                }
                else
                {

                    Mod.instance.save.herbalism[herbal] = amount;

                }

            }
            else
            {

                if (Mod.instance.save.herbalism.ContainsKey(herbal))
                {

                    int subtraction = Mod.instance.save.herbalism[herbal] - Math.Abs(amount);

                    if (subtraction < 0)
                    {

                        Mod.instance.save.herbalism[herbal] = 0;

                    }
                    else
                    {

                        Mod.instance.save.herbalism[herbal] -= Math.Abs(amount);

                    }

                }
                else
                {

                    Mod.instance.save.herbalism[herbal] = 0;

                }

            }

            if (amount != 0)
            {

                Mod.instance.SyncPreferences();

            }

            return Mod.instance.save.herbalism[herbal];

        }

        public void PotionBehaviour(string id)
        {

            Herbal herbal = herbalism[id];

            if (!Mod.instance.save.potions.ContainsKey(herbal.herbal))
            {

                Mod.instance.save.potions.Add(herbal.herbal, 1);

            }

            switch (Mod.instance.save.potions[herbal.herbal])
            {

                case 0:
                    Mod.instance.save.potions[herbal.herbal] = 1;
                    break;
                case 1:
                    Mod.instance.save.potions[herbal.herbal] = 2;
                    break;
                case 2:
                    Mod.instance.save.potions[herbal.herbal] = 3;
                    break;
                case 3:
                    Mod.instance.save.potions[herbal.herbal] = 4;
                    break;
                default:
                case 4:
                    Mod.instance.save.potions[herbal.herbal] = 0;
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

            if (herbal.ingredients.Count > 0)
            {

                craftable = false;

                foreach (string ingredient in herbal.ingredients)
                {

                    int count = Game1.player.Items.CountId(ingredient);

                    if (count > 0)
                    {

                        herbalism[id].amounts[ingredient] = count;

                        craftable = true;

                    }

                }

            }

            int amount = UpdateHerbalism(herbal.herbal);

            if (amount == 999)
            {

                return 3;

            }

            if (herbal.bases.Count > 0)
            {

                foreach (herbals required in herbal.bases)
                {

                    if (UpdateHerbalism(required) == 0)
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

        public void MassBrew(bool bench = false)
        {

            CharacterHandle.RetrieveInventory(CharacterHandle.characters.herbalism);

            int max = MaxHerbal();

            if (RelicData.HasRelic(IconData.relics.herbalism_gauge))
            {

                BrewHerbal(herbals.aether.ToString(), 50, bench);

            }

            if (RelicData.HasRelic(IconData.relics.herbalism_crucible))
            {

                BrewHerbal(herbals.faeth.ToString(), 50, bench);

                BrewHerbal(herbals.voil.ToString(), 50, bench);

            }

            foreach (KeyValuePair<herbals, List<herbals>> line in lines)
            {

                foreach (herbals herbal in line.Value)
                {

                    string key = herbal.ToString();

                    if (herbalism[key].level > max)
                    {

                        continue;

                    }

                    BrewHerbal(key, 50, bench);

                }

            }

        }

        public void MassGrind(bool bench = false)
        {

            CharacterHandle.RetrieveInventory(CharacterHandle.characters.herbalism);

            foreach (KeyValuePair<herbals, List<herbals>> line in lines)
            {

                foreach (herbals herbal in line.Value)
                {

                    string key = herbal.ToString();

                    BrewHerbal(key, 10);

                }

            }

        }

        public void BrewHerbal(string id, int draught, bool bench = false, bool force = false)
        {

            Herbal herbal = herbalism[id];

            int brewed = 0;

            if (Mod.instance.save.potions.ContainsKey(herbal.herbal) && !force)
            {

                if (Mod.instance.save.potions[herbal.herbal] == 3 || Mod.instance.save.potions[herbal.herbal] == 4)
                {

                    return;

                }

            }

            draught = Math.Min(999 - UpdateHerbalism(herbal.herbal), draught);

            if (draught == 0)
            {

                return;

            }

            if (herbal.bases.Count > 0)
            {

                foreach (herbals required in herbal.bases)
                {

                    if (UpdateHerbalism(required) == 0)
                    {

                        return;

                    }

                }

            }

            if (herbal.ingredients.Count > 0)
            {

                if (bench)
                {

                    for (int i = 0; i < Mod.instance.chests[CharacterHandle.characters.herbalism].Items.Count; i++)
                    {

                        if (brewed >= draught)
                        {

                            break;

                        }

                        Item checkSlot = Mod.instance.chests[CharacterHandle.characters.herbalism].Items.ElementAt(i);

                        if (checkSlot == null)
                        {

                            continue;

                        }

                        Item checkItem = checkSlot.getOne();

                        if (herbal.ingredients.Contains(@checkItem.QualifiedItemId))
                        {

                            int stack = Mod.instance.chests[CharacterHandle.characters.herbalism].Items.ElementAt(i).Stack;

                            int brew = Math.Min(stack, draught - brewed);

                            if (herbal.bases.Count > 0)
                            {

                                foreach (herbals required in herbal.bases)
                                {

                                    UpdateHerbalism(required, 0 - brew);

                                }

                            }

                            Mod.instance.chests[CharacterHandle.characters.herbalism].Items.ElementAt(i).Stack -= brew;

                            brewed += brew;

                        }

                    }

                    for (int i = Mod.instance.chests[CharacterHandle.characters.herbalism].Items.Count - 1; i >= 0; i--)
                    {

                        if (Mod.instance.chests[CharacterHandle.characters.herbalism].Items.ElementAt(i) != null)
                        {

                            if (Mod.instance.chests[CharacterHandle.characters.herbalism].Items.ElementAt(i).Stack <= 0)
                            {

                                Mod.instance.chests[CharacterHandle.characters.herbalism].Items.RemoveAt(i);

                            }

                        }

                    }

                }


                foreach (string ingredient in herbal.ingredients)
                {

                    if (brewed >= draught)
                    {

                        break;

                    }

                    int count = Game1.player.Items.CountId(ingredient);

                    if (count > 0)
                    {

                        int brew = Math.Min(count, draught - brewed);

                        if (herbal.bases.Count > 0)
                        {

                            foreach (herbals required in herbal.bases)
                            {

                                UpdateHerbalism(required, 0 - brew);

                            }

                        }

                        Game1.player.Items.ReduceId(ingredient, brew);

                        brewed += brew;

                    }

                }

            }
            else if (herbal.bases.Count > 0)
            {

                foreach (herbals required in herbal.bases)
                {

                    UpdateHerbalism(required, 0 - draught);

                }

                brewed = draught;

            }

            CheckHerbal(id);

            Game1.player.currentLocation.playSound(SpellHandle.sounds.bubbles.ToString());

            UpdateHerbalism(herbal.herbal, brewed);

        }

        public void ConsumeHerbal(string id)
        {

            Herbal herbal = herbalism[id];

            float difficulty = 1.6f - Mod.instance.ModDifficulty() * 0.1f;

            int staminaGain = (int)(herbal.stamina * difficulty);

            int healthGain = (int)(herbal.health * difficulty);

            Game1.player.Stamina = Math.Min(Game1.player.MaxStamina, Game1.player.Stamina + staminaGain);

            Game1.player.health = Math.Min(Game1.player.maxHealth, Game1.player.health + healthGain);

            Rectangle healthBox = Game1.player.GetBoundingBox();

            if (Game1.currentGameTime.TotalGameTime.TotalSeconds > consumeBuffer)
            {

                consumeBuffer = Game1.currentGameTime.TotalGameTime.TotalSeconds + 5;

                DisplayPotion hudmessage = new(Mod.instance.Helper.Translation.Get("HerbalData.1116").Tokens(new { potion = herbal.title, }), herbal);

                Game1.addHUDMessage(hudmessage);

            }

            if (herbal.buff != herbalbuffs.none)
            {

                if (applied.ContainsKey(herbal.buff))
                {

                    if (applied[herbal.buff].level < herbal.level)
                    {

                        applied[herbal.buff].level = herbal.level;

                        applyChange = true;

                    }

                }
                else
                {

                    applied[herbal.buff] = new();

                    applied[herbal.buff].level = herbal.level;

                    applied[herbal.buff].counter = 0;

                    applyChange = true;

                }

                applied[herbal.buff].counter += herbal.duration;

                if (applied[herbal.buff].counter >= 999)
                {

                    applied[herbal.buff].counter = 999;

                }

            }

            UpdateHerbalism(herbal.herbal, 0 - 1);

            Mod.instance.SyncPreferences();

        }

        public void HerbalBuff()
        {

            string description = string.Empty;

            float speed = 0f;

            int magnetism = 0;

            for (int i = applied.Count - 1; i >= 0; i--)
            {

                KeyValuePair<herbalbuffs, HerbalBuff> herbBuff = applied.ElementAt(i);

                applied[herbBuff.Key].counter -= 1;

                if (applied[herbBuff.Key].counter <= 0)
                {

                    applied.Remove(herbBuff.Key);

                    applyChange = true;

                }

            }

            for (int i = 0; i < applied.Count; i++)
            {

                KeyValuePair<herbalbuffs, HerbalBuff> herbBuff = applied.ElementAt(i);

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

                for (int l = 0; l < 4 - tODs.Length; l++)
                {

                    tODs = "0" + tODs;

                }

                string expire = tODs.Substring(0, 1) + tODs.Substring(1, 1) + ":" + tODs.Substring(2, 1) + "0" + meridian;

                string level = string.Empty;

                if (herbBuff.Value.level > 1)
                {

                    level = herbBuff.Value.level.ToString();

                }

                switch (herbBuff.Key)
                {

                    case herbalbuffs.alignment:

                        description += Mod.instance.Helper.Translation.Get("HerbalData.1266") + level + " " + expire + ". ";

                        magnetism = herbBuff.Value.level * 32;

                        break;

                    case herbalbuffs.vigor:

                        description += Mod.instance.Helper.Translation.Get("HerbalData.1274") + level + " " + expire + ". ";

                        break;

                    case herbalbuffs.celerity:

                        speed = 0.25f * herbBuff.Value.level;

                        description += Mod.instance.Helper.Translation.Get("HerbalData.1282") + level + " " + expire + ". ";

                        break;

                    case herbalbuffs.imbuement:

                        description += Mod.instance.Helper.Translation.Get("HerbalData.361.29") + " " + expire + ". ";

                        break;

                    case herbalbuffs.amorous:

                        description += Mod.instance.Helper.Translation.Get("HerbalData.361.30") + " " + expire + ". ";

                        break;

                    case herbalbuffs.donor:

                        description += Mod.instance.Helper.Translation.Get("HerbalData.361.31") + " " + expire + ". ";

                        break;

                    case herbalbuffs.concussion:

                        description += Mod.instance.Helper.Translation.Get("HerbalData.361.32") + " " + expire + ". ";

                        break;

                    case herbalbuffs.jumper:

                        description += Mod.instance.Helper.Translation.Get("HerbalData.361.33") + " " + expire + ". ";

                        break;

                    case herbalbuffs.feline:

                        description += Mod.instance.Helper.Translation.Get("HerbalData.361.34") + " " + expire + ". ";

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

        public void ClearBuffs()
        {

            applied.Clear();

            applyChange = true;

        }

        public void ConvertGeodes()
        {

            if (Context.IsMainPlayer)
            {

                CharacterHandle.RetrieveInventory(CharacterHandle.characters.herbalism);

            }

            Dictionary<string, Item> extracts = new();

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

                    for (int e = Game1.player.Items[i].Stack - 1; e >= 0; e--)
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

            Vector2 origin = new Vector2(20.5f, 22f) * 64;

            foreach (KeyValuePair<string, Item> extract in extracts)
            {

                if (!Context.IsMainPlayer)
                {

                    ThrowHandle throwExtract = new(Game1.player, origin, extract.Value);

                    throwExtract.register();

                    continue;

                }

                if (Mod.instance.chests[CharacterHandle.characters.herbalism].addItem(extract.Value) != null)
                {
                    ThrowHandle throwExtract = new(Game1.player, origin, extract.Value);

                    throwExtract.register();

                }

            }

            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, factor = 3, counter = -60, sound = SpellHandle.sounds.secret1 });
            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, factor = 4, counter = -40, sound = SpellHandle.sounds.silent, });
            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, factor = 4, counter = -20, sound = SpellHandle.sounds.silent, });
            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, factor = 3, sound = SpellHandle.sounds.thunder, });


            Rectangle relicRect = IconData.RelicRectangles(IconData.relics.crow_hammer);

            TemporaryAnimatedSprite animation = new(0, 1500, 1, 1, origin, false, false)
            {
                sourceRect = relicRect,
                sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                texture = Mod.instance.iconData.relicsTexture,
                layerDepth = 900f,
                rotation = -0.76f,
                scale = 4f,
            };

            Game1.player.currentLocation.TemporarySprites.Add(animation);

            Mod.instance.spellRegister.Add(new(origin, 320, IconData.impacts.supree, new()) { type = SpellHandle.spells.effect, counter = -45, instant = true, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.yoba });

            Mod.instance.spellRegister.Add(new(origin, 256, IconData.impacts.supree, new()) { type = SpellHandle.spells.effect, instant = true, });

        }

        public static string ForgeCheck()
        {

            if (Game1.player.CurrentTool is Tool currentTool)
            {

                Dictionary<string, string> toolUpgrades = new()
                {
                    // Axe
                    ["(T)Axe"] = "(T)CopperAxe",
                    ["(T)CopperAxe"] = "(T)SteelAxe",
                    ["(T)SteelAxe"] = "(T)GoldAxe",
                    ["(T)GoldAxe"] = "(T)IridiumAxe",
                    // Pickaxe
                    ["(T)Pickaxe"] = "(T)CopperPickaxe",
                    ["(T)CopperPickaxe"] = "(T)SteelPickaxe",
                    ["(T)SteelPickaxe"] = "(T)GoldPickaxe",
                    ["(T)GoldPickaxe"] = "(T)IridiumPickaxe",
                    // Hoe
                    ["(T)WateringCan"] = "(T)CopperWateringCan",
                    ["(T)CopperWateringCan"] = "(T)SteelWateringCan",
                    ["(T)SteelWateringCan"] = "(T)GoldWateringCan",
                    ["(T)GoldWateringCan"] = "(T)IridiumWateringCan",
                    // Can
                    ["(T)Hoe"] = "(T)CopperHoe",
                    ["(T)CopperHoe"] = "(T)SteelHoe",
                    ["(T)SteelHoe"] = "(T)GoldHoe",
                    ["(T)GoldHoe"] = "(T)IridiumHoe",

                };

                if (toolUpgrades.ContainsKey(currentTool.QualifiedItemId))
                {

                    return toolUpgrades[currentTool.QualifiedItemId];

                }

            }

            return string.Empty;

        }

        public static int ForgeRequirement(string toolId)
        {

            Dictionary<string, int> toolRequirements = new()
            {
                // Axe
                ["(T)CopperAxe"] = 4,
                ["(T)SteelAxe"] = 6,
                ["(T)GoldAxe"] = 9,
                ["(T)IridiumAxe"] = 12,
                // Pickaxe
                ["(T)CopperPickaxe"] = 4,
                ["(T)SteelPickaxe"] = 6,
                ["(T)GoldPickaxe"] = 9,
                ["(T)IridiumPickaxe"] = 12,
                // Hoe
                ["(T)CopperWateringCan"] = 4,
                ["(T)SteelWateringCan"] = 6,
                ["(T)GoldWateringCan"] = 9,
                ["(T)IridiumWateringCan"] = 12,
                // Can
                ["(T)CopperHoe"] = 4,
                ["(T)SteelHoe"] = 6,
                ["(T)GoldHoe"] = 9,
                ["(T)IridiumHoe"] = 12,

            };

            return toolRequirements[toolId];


        }

        public static void ForgeUpgrade()
        {

            if (Game1.player.CurrentTool is Tool currentTool)
            {

                Dictionary<string, string> toolUpgrades = new()
                {
                    // Axe
                    ["(T)Axe"] = "(T)CopperAxe",
                    ["(T)CopperAxe"] = "(T)SteelAxe",
                    ["(T)SteelAxe"] = "(T)GoldAxe",
                    ["(T)GoldAxe"] = "(T)IridiumAxe",
                    // Pickaxe
                    ["(T)Pickaxe"] = "(T)CopperPickaxe",
                    ["(T)CopperPickaxe"] = "(T)SteelPickaxe",
                    ["(T)SteelPickaxe"] = "(T)GoldPickaxe",
                    ["(T)GoldPickaxe"] = "(T)IridiumPickaxe",
                    // Hoe
                    ["(T)WateringCan"] = "(T)CopperWateringCan",
                    ["(T)CopperWateringCan"] = "(T)SteelWateringCan",
                    ["(T)SteelWateringCan"] = "(T)GoldWateringCan",
                    ["(T)GoldWateringCan"] = "(T)IridiumWateringCan",
                    // Can
                    ["(T)Hoe"] = "(T)CopperHoe",
                    ["(T)CopperHoe"] = "(T)SteelHoe",
                    ["(T)SteelHoe"] = "(T)GoldHoe",
                    ["(T)GoldHoe"] = "(T)IridiumHoe",

                };

                if (toolUpgrades.ContainsKey(currentTool.QualifiedItemId))
                {

                    Tool tooling = (Tool)ItemRegistry.Create(toolUpgrades[currentTool.QualifiedItemId]);

                    tooling.UpgradeFrom(currentTool);

                    Game1.player.removeItemFromInventory(currentTool);

                    Vector2 origin = new Vector2(1280, 1048);

                    new ThrowHandle(Game1.player, origin, tooling) { delay = 60 }.register();

                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, factor = 3, counter = -60, sound = SpellHandle.sounds.secret1 });
                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, factor = 4, counter = -40, sound = SpellHandle.sounds.silent, });
                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, factor = 4, counter = -20, sound = SpellHandle.sounds.silent, });
                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, factor = 3, sound = SpellHandle.sounds.thunder, });

                    Rectangle relicRect = IconData.RelicRectangles(IconData.relics.crow_hammer);

                    TemporaryAnimatedSprite animation = new(0, 1500, 1, 1, origin, false, false)
                    {
                        sourceRect = relicRect,
                        sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                        texture = Mod.instance.iconData.relicsTexture,
                        layerDepth = 900f,
                        rotation = -0.76f,
                        scale = 4f,
                    };

                    Game1.player.currentLocation.TemporarySprites.Add(animation);

                    Mod.instance.spellRegister.Add(new(origin, 320, IconData.impacts.supree, new()) { type = SpellHandle.spells.effect, counter = -45, instant = true, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.yoba });

                    Mod.instance.spellRegister.Add(new(origin, 256, IconData.impacts.supree, new()) { type = SpellHandle.spells.effect, instant = true, });

                }

            }

        }

        public static bool RandomStudy()
        {

            Dictionary<string, string> recipes = new();

            foreach (KeyValuePair<string, string> allRecipes in CraftingRecipe.cookingRecipes)
            {

                if (!Game1.player.cookingRecipes.ContainsKey(allRecipes.Key))
                {

                    recipes[allRecipes.Key] = allRecipes.Value;

                }

            }

            if ((recipes.Count == 0 || Mod.instance.randomIndex.Next(3) == 0) && !Mod.instance.Config.disableShopdata)
            {
                List<string> books = new()
                {
                    "Book_Trash",
                    "Book_Crabbing",
                    "Book_Bombs",
                    "Book_Roe",
                    "Book_WildSeeds",
                    "Book_Woodcutting",
                    "Book_Defense",
                    "Book_Friendship",
                    "Book_Void",
                    "Book_Speed",
                    "Book_Marlon",
                    "Book_QueenOfSauce",
                    "Book_Diamonds",
                    "Book_Mystery",
                    "Book_Speed2",
                    "Book_Artifact",
                    "Book_Horse",
                    "Book_Grass",
                };

                List<string> bookCandidates = new();

                foreach (string book in books)
                {

                    if (Game1.player.stats.Get(book) != 0)
                    {

                        continue;

                    }

                    bookCandidates.Add(book);

                }

                if (bookCandidates.Count > 0)
                {

                    Item newBook = ItemRegistry.Create(books[Mod.instance.randomIndex.Next(bookCandidates.Count)], 1);

                    new ThrowHandle(Game1.player, Game1.player.Position + new Vector2(64, 128), newBook) { delay = 10, holdup = true }.register();

                    return true;

                }

            }

            if (recipes.Count == 0)
            {

                return false;

            }

            KeyValuePair<string, string> craftingRecipe = recipes.ElementAt(Mod.instance.randomIndex.Next(recipes.Count));

            Game1.player.cookingRecipes.Add(craftingRecipe.Key, 0);

            CraftingRecipe newThing = new(craftingRecipe.Key);

            ThrowHandle.AnimateHoldup();

            Rectangle relicRect = IconData.RelicRectangles(IconData.relics.book_letters);

            TemporaryAnimatedSprite animation = new(0, 2000, 1, 1, Game1.player.Position + new Vector2(2, -124f), false, false)
            {
                sourceRect = relicRect,
                sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                texture = Mod.instance.iconData.relicsTexture,
                layerDepth = 900f,
                delayBeforeAnimationStart = 175,
                scale = 3f,

            };

            Game1.player.currentLocation.TemporarySprites.Add(animation);

            string text = Mod.instance.Helper.Translation.Get("HerbalData.361.35") + newThing.DisplayName;

            Game1.drawObjectDialogue(text);

            return true;

        }

        public static bool RandomTinker()
        {

            List<string> list = new()
            {
                "(BC)10",
                "(BC)12",
                "(BC)13",
                "(BC)16",
                "(BC)19",
                "(BC)21",
                "(BC)25",
                "(BC)156",
                "(BC)158",
                "(BC)165",
                "(BC)182",
                "(BC)208",
                "(BC)211",
                "(BC)216",
                "(BC)239",
                "(BC)246",
                "(BC)265",
                "(BC)272",
                "(BC)275",
                //"(BC)MushroomLog",
                "(BC)BaitMaker",
                "(BC)Dehydrator",
                "(BC)FishSmoker",

            };

            Item newMachine = ItemRegistry.Create(list[Mod.instance.randomIndex.Next(list.Count)], 1);

            new ThrowHandle(Game1.player, Game1.player.Position + new Vector2(128, 0), newMachine) { delay = 10, holdup = true }.register();

            return true;

        }

    }

    public class Herbal
    {

        // -----------------------------------------------
        // journal

        public HerbalData.herbals herbal = HerbalData.herbals.none;

        public IconData.potions display = IconData.potions.ligna;

        public IconData.potions grayed = IconData.potions.lignaGray;

        public string title;

        public string description;

        public List<string> ingredients = new();

        public List<HerbalData.herbals> bases = new();

        public List<string> details = new();

        public int status;

        public Dictionary<string, int> amounts = new();

        public int level;

        public int duration;

        public HerbalData.herbalbuffs buff = HerbalData.herbalbuffs.none;

        public int health;

        public int stamina;

        public bool resource;

        public int price;

    }

    public class HerbalBuff
    {

        // ----------------------------------------------------

        public HerbalData.herbalbuffs buff = HerbalData.herbalbuffs.none;

        public int counter;

        public int level;

    }


}
