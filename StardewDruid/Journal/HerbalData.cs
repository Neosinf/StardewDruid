
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.GameData.BigCraftables;
using StardewValley.GameData.Machines;
using StardewValley.GameData.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using xTile.Dimensions;
using static StardewDruid.Data.IconData;
using static StardewDruid.Journal.LoreSet;

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

                if ((herbalism[herbal.ToString()].level -1) > max)
                {

                    continue;

                }

                candidates.Add(herbal);

            }

            for(int i = 0; i< Mod.instance.randomIndex.Next(4,7); i++)
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

                        orders[candidate] = 5;

                        break;

                    case 4:
                    case 5:
                        orders[candidate] = 10;

                        break;

                    case 6:
                    case 7:

                        orders[candidate] = 20;

                        break;

                    case 8:
                    case 9:

                        orders[candidate] = 50;

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

        public Dictionary<int, Journal.ContentComponent> JournalHerbals()
        {

            Dictionary<int, Journal.ContentComponent> journal = new();

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

                            if (!Journal.RelicData.HasRelic(IconData.relics.herbalism_gauge))
                            {

                                Journal.ContentComponent blank = new(ContentComponent.contentTypes.potion, key, false);

                                journal[start++] = blank;

                                Journal.ContentComponent blankToggle = new(ContentComponent.contentTypes.toggle, key, false);

                                journal[start++] = blankToggle;

                                continue;

                            }

                            break;

                        case herbals.faeth:

                            if (!Journal.RelicData.HasRelic(IconData.relics.herbalism_crucible))
                            {

                                Journal.ContentComponent blank = new(ContentComponent.contentTypes.potion, key, false);

                                journal[start++] = blank;

                                Journal.ContentComponent blankToggle = new(ContentComponent.contentTypes.toggle, key, false);

                                journal[start++] = blankToggle;

                                continue;

                            }

                            break;


                    }

                }
                else if (herbalism[key].level > max)
                {
                    
                    Journal.ContentComponent blank = new(ContentComponent.contentTypes.potion, key, false);
                    
                    journal[start++] = blank;

                    Journal.ContentComponent blankToggle = new(ContentComponent.contentTypes.toggle, key, false);

                    journal[start++] = blankToggle;

                    continue;

                }

                // =============================================================== potion button

                Journal.ContentComponent content = new(ContentComponent.contentTypes.potion, key);

                Mod.instance.herbalData.CheckHerbal(key);

                int amount = 0;

                if (Mod.instance.save.herbalism.ContainsKey(herbal))
                {

                    amount = Mod.instance.save.herbalism[herbal];

                }

                string amountString = amount.ToString();

                content.text[0] = amount.ToString();

                content.relics[0] = herbalism[key].display;

                /*content.relicColours[0] = Color.White;

                content.relics[1] = herbalism[key].grayed;

                IconData.schemes scheme = schemes[herbalism[key].line];

                Microsoft.Xna.Framework.Color potionColour = Mod.instance.iconData.schemeColours[scheme];

                if (amount == 0)
                {
                    potionColour = Microsoft.Xna.Framework.Color.LightGray;
                }

                content.relicColours[1] = potionColour;*/

                if (amount == 0)
                {
                    content.relics[0] = herbalism[key].grayed;
                }

                journal[start++] = content;

                // =============================================================== active button

                Journal.ContentComponent toggle = new(ContentComponent.contentTypes.toggle, key);

                IconData.displays flag = IconData.displays.complete;

                DialogueData.stringkeys hovertext = DialogueData.stringkeys.acEnabled;

                if (Mod.instance.save.potions.ContainsKey(herbal))
                {

                    switch (Mod.instance.save.potions[herbal])
                    {

                        case 0:

                            flag = IconData.displays.exit;

                            hovertext = DialogueData.stringkeys.acDisabled;

                            break;

                        case 1:

                            flag = IconData.displays.complete;

                            break;

                        case 2:

                            flag = IconData.displays.flag;

                            hovertext = DialogueData.stringkeys.acPriority;

                            break;

                        case 3:

                            flag = IconData.displays.active;

                            hovertext = DialogueData.stringkeys.acIgnored;

                            break;


                    }
                }

                toggle.icons[0] = flag;

                toggle.text[0] = DialogueData.Strings(hovertext);

                journal[start++] = toggle;

            }

            return journal;

        }

        public Dictionary<int, Journal.ContentComponent> TradeHerbals()
        {

            Dictionary<int, Journal.ContentComponent> journal = new();

            int start = 0;

            foreach (KeyValuePair<herbals,int> order in orders)
            {

                string herb = order.Key.ToString();

                Journal.ContentComponent content = new(ContentComponent.contentTypes.potionlist, herb);

                Herbal herbalData = herbalism[herb];

                content.text[0] = herbalData.title + " " + DialogueData.Strings(DialogueData.stringkeys.multiplier) + order.Value.ToString();

                content.text[1] = (herbalData.price * order.Value).ToString() + DialogueData.Strings(DialogueData.stringkeys.currency);

                /*content.relics[0] = herbalism[herb].container;
                
                content.relicColours[0] = Color.White;

                content.relics[1] = herbalism[herb].content;

                IconData.schemes scheme = schemes[herbalism[herb].line];

                Microsoft.Xna.Framework.Color potionColour = Mod.instance.iconData.schemeColours[scheme];*/

                content.relics[0] = herbalism[herb].display;

                if (!Mod.instance.save.herbalism.ContainsKey(order.Key))
                {

                    //potionColour = Microsoft.Xna.Framework.Color.LightGray;

                    content.relics[0] = herbalism[herb].grayed;

                    content.text[0] += " " + Mod.instance.Helper.Translation.Get("DialogueData.347.5").Tokens(new { stock = order.Value.ToString().ToString() });

                }
                else
                {
                    if (Mod.instance.save.herbalism[order.Key] < order.Value)
                    {
                        //potionColour = Microsoft.Xna.Framework.Color.LightGray;

                        content.relics[0] = herbalism[herb].grayed;

                        content.text[0] += " " + Mod.instance.Helper.Translation.Get("DialogueData.347.5").Tokens(new { stock = (order.Value - Mod.instance.save.herbalism[order.Key]).ToString() });
                    
                    }

                }

                //content.relicColours[1] = potionColour;

                journal[start++] = content;

            }

            return journal;

        }

        /*public Dictionary<int, Journal.ContentComponent> JournalHeaders()
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

        }*/

        public static Dictionary<string, Herbal> HerbalList()
        {

            Dictionary<string, Herbal> potions = new();

            // ====================================================================
            // Ligna line

            potions[herbals.ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.ligna,

                display = IconData.relics.ligna,

                grayed = IconData.relics.lignaGray,

                level = 0,

                duration = 0,

                title = Mod.instance.Helper.Translation.Get("HerbalData.25"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.27"),

                ingredients = HerbalIngredients(herbals.ligna),

                bases = new() { },

                health = 8,

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

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.melius_ligna,

                display = IconData.relics.ligna1,

                grayed = IconData.relics.lignaGray1,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("HerbalData.61"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.63"),

                ingredients = HerbalIngredients(herbals.melius_ligna),

                bases = new() { herbals.ligna, },

                health = 15,

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

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.satius_ligna,

                display = IconData.relics.ligna2,

                grayed = IconData.relics.lignaGray2,

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

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.magnus_ligna,

                display = IconData.relics.ligna3,

                grayed = IconData.relics.lignaGray3,

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

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.optimus_ligna,

                display = IconData.relics.ligna4,

                grayed = IconData.relics.lignaGray4,

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

                line = herbals.impes,

                herbal = herbals.impes,

                display = IconData.relics.impes,

                grayed = IconData.relics.impesGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.206"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.208"),

                ingredients = HerbalIngredients(herbals.impes),

                bases = new() { },

                health = 15,

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

                line = herbals.impes,

                herbal = herbals.melius_impes,

                display = IconData.relics.impes1,

                grayed = IconData.relics.impesGray1,

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

                line = herbals.impes,

                herbal = herbals.satius_impes,

                display = IconData.relics.impes2,

                grayed = IconData.relics.impesGray2,

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

                line = HerbalData.herbals.impes,

                herbal = HerbalData.herbals.magnus_impes,

                display = IconData.relics.impes3,

                grayed = IconData.relics.impesGray3,

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

                line = HerbalData.herbals.impes,

                herbal = HerbalData.herbals.optimus_impes,

                display = IconData.relics.impes4,

                grayed = IconData.relics.impesGray4,

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

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.celeri,

                display = IconData.relics.celeri,

                grayed = IconData.relics.celeriGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.388"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.390"),
                

                ingredients = HerbalIngredients(herbals.celeri),

                bases = new() { },

                health = 15,

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

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.melius_celeri,

                display = IconData.relics.celeri1,

                grayed = IconData.relics.celeriGray1,

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

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.satius_celeri,

                display = IconData.relics.celeri2,

                grayed = IconData.relics.celeriGray2,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("HerbalData.460"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.462"),

                ingredients = HerbalIngredients(herbals.satius_celeri),

                health = 30,

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

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.magnus_celeri,

                display = IconData.relics.celeri3,

                grayed = IconData.relics.celeriGray3,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("HerbalData.497"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.499"),

                ingredients = HerbalIngredients(herbals.magnus_celeri),

                health = 60,

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

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.optimus_celeri,

                display = IconData.relics.celeri4,

                grayed = IconData.relics.celeriGray4,

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

                line = HerbalData.herbals.faeth,

                herbal = HerbalData.herbals.faeth,

                display = IconData.relics.faeth,

                grayed = IconData.relics.faethGray,

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

                line = HerbalData.herbals.aether,

                herbal = HerbalData.herbals.aether,

                display = IconData.relics.aether,

                grayed = IconData.relics.aetherGray,

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
            return potions;

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

            /*Dictionary<string, int> more = new()
            {
                [Mod.instance.Helper.Translation.Get("HerbalData.697")] = 2,

            };*/

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

                /*for (int i = 0; i < Game1.player.Items.Count; i++)
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

                }*/

                //if (Mod.instance.Helper.ModRegistry.IsLoaded("FlyingTNT.ResourceStorage"))
                //{

                //}

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

        public void MassBrew(bool bench = false)
        {

            CharacterHandle.RetrieveInventory(CharacterHandle.characters.herbalism);

            int max = MaxHerbal();

            if (Journal.RelicData.HasRelic(IconData.relics.herbalism_gauge))
            {

                BrewHerbal(herbals.aether.ToString(), 50, bench);

            }

            if (Journal.RelicData.HasRelic(IconData.relics.herbalism_crucible))
            {

                BrewHerbal(herbals.faeth.ToString(), 50, bench);

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

        public void BrewHerbal(string id, int draught, bool bench = false, bool force = false)
        {

            Herbal herbal = herbalism[id];

            int brewed = 0;

            if (Mod.instance.save.potions.ContainsKey(herbal.herbal) && !force)
            {

                if(Mod.instance.save.potions[herbal.herbal] == 0 || Mod.instance.save.potions[herbal.herbal] == 3)
                {

                    return;

                }

            }

            if (Mod.instance.save.herbalism.ContainsKey(herbal.herbal))
            {

                draught = Math.Min(999 - Mod.instance.save.herbalism[herbal.herbal], draught);

            }

            if (draught == 0)
            {

                return;

            }

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

                        Item checkSlot = Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items.ElementAt(i);

                        if (checkSlot == null)
                        {

                            continue;

                        }

                        Item checkItem = checkSlot.getOne();

                        if (herbal.ingredients.Contains(@checkItem.QualifiedItemId))
                        {

                            int stack = Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items.ElementAt(i).Stack;

                            int cost = 1;

                            int brew = Math.Min(stack, (draught - brewed));

                            if (herbal.bases.Count > 0)
                            {

                                foreach (herbals required in herbal.bases)
                                {

                                    Mod.instance.save.herbalism[required] -= brew;

                                }

                            }

                            Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items.ElementAt(i).Stack -= (brew * cost);

                            brewed += brew;

                        }

                    }

                    for (int i = Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items.Count - 1; i >= 0; i--)
                    {

                        if(Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items.ElementAt(i) != null)
                        {

                            if (Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items.ElementAt(i).Stack <= 0)
                            {

                                Mod.instance.chests[Character.CharacterHandle.characters.herbalism].Items.RemoveAt(i);

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

                        int brew = Math.Min(count, (draught - brewed));

                        if (herbal.bases.Count > 0)
                        {

                            foreach (herbals required in herbal.bases)
                            {

                                Mod.instance.save.herbalism[required] -= brew;

                            }

                        }

                        Game1.player.Items.ReduceId(ingredient, brew);

                        brewed += brew;

                    }

                }

                /*for (int i = 0; i < Game1.player.Items.Count; i++)
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

                }*/

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

            /*if (Mod.instance.save.potions.ContainsKey(herbal.herbal))
            {

                if (Mod.instance.save.potions[herbal.herbal] == 0)
                {

                    return;

                }

            }*/

            float difficulty = 1.6f - (Mod.instance.ModDifficulty() * 0.1f);

            int staminaGain = (int)(herbal.stamina * difficulty);

            int healthGain = (int)(herbal.health * difficulty);

            Game1.player.Stamina = Math.Min(Game1.player.MaxStamina, Game1.player.Stamina + staminaGain);

            Game1.player.health = Math.Min(Game1.player.maxHealth, Game1.player.health + healthGain);

            Microsoft.Xna.Framework.Rectangle healthBox = Game1.player.GetBoundingBox();

            if (Game1.currentGameTime.TotalGameTime.TotalSeconds > consumeBuffer)
            {

                consumeBuffer = Game1.currentGameTime.TotalGameTime.TotalSeconds + 5;

                DisplayPotion hudmessage = new(Mod.instance.Helper.Translation.Get("HerbalData.1116").Tokens(new { potion = herbal.title, }), herbal);

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

            bool isApplied = true;

            if (applied.Count == 0)
            {

                return;

            }
            else if (!Game1.player.buffs.IsApplied(184652.ToString()))
            {

                isApplied = false;

            }
            else if (!applyChange)
            {

                return;

            }

            if (isApplied)
            {

                Game1.player.buffs.Remove(184652.ToString());

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
            
            if (Context.IsMainPlayer)
            {

                CharacterHandle.RetrieveInventory(CharacterHandle.characters.herbalism);

            }

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

            Vector2 origin = new Vector2(21,23)*64;

            foreach(KeyValuePair<string,StardewValley.Item> extract in extracts)
            {

                if (!Context.IsMainPlayer)
                {
                    
                    ThrowHandle throwExtract = new(Game1.player, origin, extract.Value);

                    throwExtract.register();

                    continue;

                }

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

            return String.Empty;

        }

        public static int ForgeRequirement(string toolId)
        {

            Dictionary<string, int> toolRequirements = new()
            {
                // Axe
                ["(T)CopperAxe"] = 4,
                ["(T)SteelAxe"] = 6,
                ["(T)GoldAxe"] = 9,
                ["(T)IridiumAxe"]= 12,
                // Pickaxe
                ["(T)CopperPickaxe"] = 4,
                ["(T)SteelPickaxe"] = 6,
                ["(T)GoldPickaxe"] = 9,
                ["(T)IridiumPickaxe"]= 12,
                // Hoe
                ["(T)CopperWateringCan"] = 4,
                ["(T)SteelWateringCan"] = 6,
                ["(T)GoldWateringCan"] = 9,
                ["(T)IridiumWateringCan"]= 12,
                // Can
                ["(T)CopperHoe"] = 4,
                ["(T)SteelHoe"] = 6,
                ["(T)GoldHoe"] = 9,
                ["(T)IridiumHoe"]= 12,

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

                    Vector2 origin = new Vector2(1312, 1080);

                    new ThrowHandle(Game1.player, origin, tooling) { delay = 60 }.register();

                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -60, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.secret1 });
                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -45, sound = SpellHandle.sounds.silent, });
                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -30, sound = SpellHandle.sounds.silent, });
                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, counter = -15, sound = SpellHandle.sounds.silent, });
                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.spells.bolt, projectile = 4, sound = SpellHandle.sounds.thunder, });

                    Microsoft.Xna.Framework.Rectangle relicRect = IconData.RelicRectangles(IconData.relics.crow_hammer);

                    TemporaryAnimatedSprite animation = new(0, 1500, 1, 1, origin - new Vector2(16, 60), false, false)
                    {
                        sourceRect = relicRect,
                        sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                        texture = Mod.instance.iconData.relicsTexture,
                        layerDepth = 900f,
                        rotation = -0.76f,
                        scale = 4f,
                    };

                    Game1.player.currentLocation.TemporarySprites.Add(animation);

                    Mod.instance.spellRegister.Add(new(origin - new Vector2(24, 32), 320, IconData.impacts.nature, new()) { type = SpellHandle.spells.effect, counter = -45, instant = true, scheme = IconData.schemes.golden, sound = SpellHandle.sounds.yoba });

                    Mod.instance.spellRegister.Add(new(origin - new Vector2(24, 32), 320, IconData.impacts.nature, new()) { type = SpellHandle.spells.effect, instant = true, scheme = IconData.schemes.golden, });

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

            if((recipes.Count == 0 || Mod.instance.randomIndex.Next(3) == 0) && !Mod.instance.Config.disableShopdata)
            {

                Dictionary<string, ShopData> shopData = DataLoader.Shops(Game1.content);

                if (shopData != null && shopData.Count > 0)
                {

                    if (shopData.ContainsKey("Bookseller"))
                    {

                        List<string> books = new();

                        foreach (ShopItemData shopItem in shopData["Bookseller"].Items)
                        {

                            if (shopItem == null)
                            {

                                continue;

                            }

                            if (shopItem.Condition != null)
                            {

                                if (!GameStateQuery.CheckConditions(shopItem.Condition, Game1.getLocationFromName("Town"), Game1.player, null, null, Mod.instance.randomIndex))
                                {

                                    continue;

                                }

                            }

                            books.Add(shopItem.ItemId);

                        }

                        if(books.Count > 0)
                        {

                            StardewValley.Item newBook = ItemRegistry.Create(books[Mod.instance.randomIndex.Next(books.Count)], 1);

                            new ThrowHandle(Game1.player, Game1.player.Position + new Vector2(64, 128), newBook) { delay = 10, holdup = true }.register();

                            return true;

                        }

                    }

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

            Microsoft.Xna.Framework.Rectangle relicRect = IconData.RelicRectangles(IconData.relics.book_letters);

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

            string text = "Learned how to cook " + newThing.DisplayName;

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

            StardewValley.Item newMachine = ItemRegistry.Create(list[Mod.instance.randomIndex.Next(list.Count)], 1);

            new ThrowHandle(Game1.player, Game1.player.Position + new Vector2(128,0), newMachine) { delay = 10, holdup = true }.register();

            return true;

        }

    }

    public class Herbal
    {

        // -----------------------------------------------
        // journal

        public HerbalData.herbals line = HerbalData.herbals.none;

        public HerbalData.herbals herbal = HerbalData.herbals.none;

        public IconData.relics display = IconData.relics.ligna;

        public IconData.relics grayed = IconData.relics.lignaGray;

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

        public int price;

    }

    public class HerbalBuff
    {

        // ----------------------------------------------------

        public HerbalData.herbals line = HerbalData.herbals.none;

        public int counter;

        public int level;

    }


}
