
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

        public Dictionary<herbals, List<string>> titles = new()
        {

            [herbals.ligna] = new() { Mod.instance.Helper.Translation.Get("Herbal.ligna.title.0"), Mod.instance.Helper.Translation.Get("Herbal.ligna.title.1"), },
            [herbals.impes] = new() { Mod.instance.Helper.Translation.Get("Herbal.impes.title.0"), Mod.instance.Helper.Translation.Get("Herbal.impes.title.1"), },
            [herbals.celeri] = new() { Mod.instance.Helper.Translation.Get("Herbal.celeri.title.0"), Mod.instance.Helper.Translation.Get("Herbal.celeri.title.1"), },
            [herbals.faeth] = new() { Mod.instance.Helper.Translation.Get("Herbal.faeth.title.0"), Mod.instance.Helper.Translation.Get("Herbal.faeth.title.1"), },
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

        public List<List<string>> OrganiseHerbals()
        {

            List<List<string>> source = new()
            {
                
                new List<string>()

            };

            int max = MaxHerbal();

            foreach (KeyValuePair<herbals, List<herbals>> line in lines)
            {

                foreach (herbals herbal in line.Value)
                {

                    string key = herbal.ToString();

                    if (herbalism[key].level > max)
                    {

                        source.Last().Add("blank");

                    }
                    else
                    {

                        Mod.instance.herbalData.CheckHerbal(key);

                        source.Last().Add(key);

                    }

                    if (herbal == lines[line.Key].Last())
                    {

                        source.Last().Add("configure");

                    }

                }

            }

            if(Mod.instance.save.reliquary.ContainsKey(IconData.relics.herbalism_crucible.ToString()))
            {

                source.Add(new());

                source.Last().Add(herbals.faeth.ToString());

            }

            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.herbalism_gauge.ToString()))
            {

                source.Last().Add(herbals.aether.ToString());

            }


            return source;

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

                Microsoft.Xna.Framework.Color potionColour = Mod.instance.iconData.schemeColours[herbalism[key].scheme];

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

        public static Dictionary<string,Herbal> HerbalList()
        {

            Dictionary<string, Herbal> potions = new();

            // ====================================================================
            // Ligna line

            potions[herbals.ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.ligna,

                scheme = IconData.schemes.Emerald,

                container = IconData.relics.flask,

                content = IconData.relics.flask1,

                level = 0,

                duration = 0,

                title = Mod.instance.Helper.Translation.Get("Potion.ligna.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.ligna.description"),
                
                ingredients = new(){ ["(O)92"] = "Sap", ["(O)766"] = "Slime", },

                bases = new() { },

                health = 10,

                stamina = 15,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.ligna.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.ligna.details.1")
                }

            };


            potions[herbals.melius_ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.melius_ligna,

                scheme = IconData.schemes.Emerald,

                container = IconData.relics.flask,

                content = IconData.relics.flask2,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("Potion.melius_ligna.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.melius_ligna.description"),

                ingredients = new() { ["(O)311"] = "Acorn", ["(O)310"] = "MapleSeed", ["(O)309"] = "Pinecorn", ["(O)292"] = "MahoganySeed", ["(O)Moss"] = "Moss", },

                bases = new() { herbals.ligna, },

                health = 15,

                stamina = 30,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.melius_ligna.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.melius_ligna.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.melius_ligna.details.2")
                }

            };


            potions[herbals.satius_ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.satius_ligna,

                scheme = IconData.schemes.Emerald,

                container = IconData.relics.flask,

                content = IconData.relics.flask3,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("Potion.satius_ligna.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.satius_ligna.description"),

                ingredients = new() { ["(O)418"] = "Crocus", ["(O)18"] = "Daffodil", ["(O)22"] = "Dandelion", ["(O)402"] = "Sweet Pea", ["(O)273"] = "Rice Shoot", ["(O)591"] = "Tulip", ["(O)376"] = "Poppy", },

                bases = new() { herbals.melius_ligna, },

                health = 20,

                stamina = 80,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.satius_ligna.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.satius_ligna.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.satius_ligna.details.2")
                }

            };


            potions[herbals.magnus_ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.magnus_ligna,

                scheme = IconData.schemes.Emerald,

                container = IconData.relics.flask,

                content = IconData.relics.flask4,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("Potion.magnus_ligna.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.magnus_ligna.description"),

                ingredients = new() { ["(O)247"] = "Oil", ["(O)431"] = "Sunflower Seeds", ["(O)270"] = "Corn", ["(O)271"] = "Unmilled Rice", ["(O)421"] = "Sunflower", ["(O)593"] = "Spangle", ["(O)597"] = "Jazz", }, // },

                bases = new() { herbals.satius_ligna, },

                health = 50,

                stamina = 200,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.magnus_ligna.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.magnus_ligna.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.magnus_ligna.details.2"),
                }

            };

            potions[herbals.optimus_ligna.ToString()] = new()
            {

                line = HerbalData.herbals.ligna,

                herbal = HerbalData.herbals.optimus_ligna,

                scheme = IconData.schemes.Emerald,

                container = IconData.relics.flask,

                content = IconData.relics.flask5,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("Potion.optimus_ligna.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.optimus_ligna.description"),

                ingredients = new() { },

                bases = new() { herbals.magnus_ligna, herbals.aether },

                health = 100,

                stamina = 400,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.optimus_ligna.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.optimus_ligna.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.optimus_ligna.details.2")
                }

            };

            // ====================================================================
            // Impes series

            potions[herbals.impes.ToString()] = new()
            {

                line = herbals.impes,

                herbal = herbals.impes,

                scheme = IconData.schemes.Ruby,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle1,

                title = Mod.instance.Helper.Translation.Get("Potion.impes.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.impes.description"),

                ingredients = new() { ["(O)399"] = "Spring Onion", ["(O)78"] = "Cave Carrot", ["(O)24"] = "Parsnip", ["(O)831"] = "Taro Tubers", ["(O)16"] = "Wild Horseradish", ["(O)412"] = "Winter Root",},

                bases = new() { },

                health = 15,

                stamina = 40,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.impes.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.impes.details.1")
                }

            };


            potions[herbals.melius_impes.ToString()] = new()
            {

                line = herbals.impes,

                herbal = herbals.melius_impes,

                scheme = IconData.schemes.Ruby,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle2,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("Potion.melius_impes.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.melius_impes.description"),

                ingredients = new() {  ["(O)420"] = "Red Mushrooms", ["(O)404"] = "Common Mushrooms", ["(O)257"] = "Morel", ["(O)767"] = "Batwings", },

                bases = new() { herbals.impes, },

                health = 30,

                stamina = 80,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.melius_impes.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.melius_impes.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.melius_impes.details.2")
                }

            };


            potions[herbals.satius_impes.ToString()] = new()
            {

                line = herbals.impes,

                herbal = herbals.satius_impes,

                scheme = IconData.schemes.Ruby,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle3,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("Potion.satius_impes.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.satius_impes.description"),

                ingredients = new() { ["(O)93"] = "Torch", ["(O)82"] = "Fire Quartz", ["(O)382"] = "Coal", },

                bases = new() { herbals.melius_impes, },

                health = 45,

                stamina = 160,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.satius_impes.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.satius_impes.details.1"),

                    Mod.instance.Helper.Translation.Get("Potion.satius_impes.details.2")
                }

            };


            potions[herbals.magnus_impes.ToString()] = new()
            {

                line = HerbalData.herbals.impes,

                herbal = HerbalData.herbals.magnus_impes,

                scheme = IconData.schemes.Ruby,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle4,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("Potion.magnus_impes.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.magnus_impes.description"),

                ingredients = new() { ["(O)419"] = "Vinegar", ["(O)260"] = "Hot Pepper", ["(O)829"] = "Ginger", ["(O)248"] = "Garlic", },

                bases = new() { herbals.satius_impes, },

                health = 70,

                stamina = 320,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.magnus_impes.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.magnus_impes.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.magnus_impes.details.2")
                }

            };

            potions[herbals.optimus_impes.ToString()] = new()
            {

                line = HerbalData.herbals.impes,

                herbal = HerbalData.herbals.optimus_impes,

                scheme = IconData.schemes.Ruby,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle5,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("Potion.optimus_impes.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.optimus_impes.description"),

                ingredients = new() { },

                bases = new() { herbals.magnus_impes, herbals.aether, },

                health = 180,

                stamina = 560,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.optimus_impes.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.optimus_impes.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.optimus_impes.details.2")
                }

            };

            // ====================================================================
            // Celeri series

            potions[herbals.celeri.ToString()] = new()
            {

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.celeri,

                scheme = IconData.schemes.blueberry,

                container = IconData.relics.vial,

                content = IconData.relics.vial1,

                title = Mod.instance.Helper.Translation.Get("Potion.celeri.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.celeri.description"),

                ingredients = new() { ["(O)152"] = "Algae", ["(O)153"] = "Seaweed", ["(O)157"] = "White Algae", ["(O)815"] = "Tea Leaves", ["(O)433"] = "Coffee Bean", ["(O)167"] = "Joja Cola", },

                bases = new() { },

                health = 15,

                stamina = 25,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.celeri.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.celeri.details.1")
                }

            };

            potions[herbals.melius_celeri.ToString()] = new()
            {

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.melius_celeri,

                scheme = IconData.schemes.blueberry,

                container = IconData.relics.vial,

                content = IconData.relics.vial2,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("Potion.melius_celeri.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.melius_celeri.description"),

                ingredients = new() { ["(O)80"] = "Quartz", ["(O)86"] = "Earth Crystal", ["(O)881"] = "Bone Fragment", ["(O)168"] = "Trash", ["(O)169"] = "Driftwood", ["(O)170"] = "Broken Glasses", ["(O)171"] = "Broken CD", },

                health = 20,

                stamina = 60,

                bases = new() { herbals.celeri, },

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.melius_celeri.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.melius_celeri.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.melius_celeri.details.2")
                }

            };


            potions[herbals.satius_celeri.ToString()] = new()
            {

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.satius_celeri,

                scheme = IconData.schemes.blueberry,

                container = IconData.relics.vial,

                content = IconData.relics.vial3,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("Potion.satius_celeri.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.satius_celeri.description"),

                ingredients = new() { ["(O)129"] = "Sardine", ["(O)131"] = "Anchovy", ["(O)137"] = "Smallmouth Bass", ["(O)145"] = "Sunfish", ["(O)132"] = "Bream", ["(O)147"] = "Herring", ["(O)142"] = "Carp", ["(O)156"] = "Ghostfish", },

                health = 30,

                stamina = 120,

                bases = new() { herbals.melius_celeri, },

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.satius_celeri.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.satius_celeri.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.satius_celeri.details.2")
                }

            };


            potions[herbals.magnus_celeri.ToString()] = new()
            {

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.magnus_celeri,

                scheme = IconData.schemes.blueberry,

                container = IconData.relics.vial,

                content = IconData.relics.vial4,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("Potion.magnus_celeri.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.magnus_celeri.description"),

                ingredients = new() { ["(O)718"] = "Cockle", ["(O)719"] = "Mussel", ["(O)720"] = "Shrimp", ["(O)721"] = "Snail", ["(O)722"] = "Periwinkle", ["(O)723"] = "Oyster", ["(O)372"] = "Clam", },

                health = 60,

                stamina = 240,

                bases = new() { herbals.satius_celeri, },

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.magnus_celeri.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.magnus_celeri.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.magnus_celeri.details.2")
                }

            };

            potions[herbals.optimus_celeri.ToString()] = new()
            {

                line = HerbalData.herbals.celeri,

                herbal = HerbalData.herbals.optimus_celeri,

                scheme = IconData.schemes.blueberry,

                container = IconData.relics.vial,

                content = IconData.relics.vial5,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("Potion.optimus_celeri.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.optimus_celeri.description"),

                ingredients = new() {},

                health = 120,

                stamina = 480,

                bases = new() { herbals.magnus_celeri, herbals.aether, },

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.optimus_celeri.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.optimus_celeri.details.1"),
                    Mod.instance.Helper.Translation.Get("Potion.optimus_celeri.details.2")
                }

            };

            // ====================================================================
            // Faeth

            potions[herbals.faeth.ToString()] = new()
            {

                line = HerbalData.herbals.faeth,

                herbal = HerbalData.herbals.faeth,

                scheme = IconData.schemes.Amethyst,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle5,

                level = 3,

                title = Mod.instance.Helper.Translation.Get("Potion.faeth.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.faeth.description"),

                ingredients = new() { ["(O)577"] = "Fairy Stone", ["(O)595"] = "Fairy Rose", ["(O)768"] = "Solar Essence", ["(O)769"] = "Void Essence", ["(O)MossySeed"] = "Mossy Seed", },

                bases = new() { },

                details = new()
                {
                    
                    Mod.instance.Helper.Translation.Get("Potion.faeth.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.faeth.details.1"),

                },

                resource = true,

            };

            // ====================================================================
            // Ether

            potions[herbals.aether.ToString()] = new()
            {

                line = HerbalData.herbals.faeth,

                herbal = HerbalData.herbals.aether,

                scheme = IconData.schemes.ether,

                container = IconData.relics.bottle,

                content = IconData.relics.bottle5,

                level = 4,

                title = Mod.instance.Helper.Translation.Get("Potion.aether.title"),

                description = Mod.instance.Helper.Translation.Get("Potion.aether.description"),

                ingredients = new() { ["(O)60"] = "Emerald", ["(O)64"] = "Ruby", ["(O)72"] = "Diamond", ["(O)62"] = "Aquamarine", },

                bases = new() {},

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Potion.aether.details.0"),
                    Mod.instance.Helper.Translation.Get("Potion.aether.details.1"),
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
                ["(O)684"] = 2,

            };*/

            if(herbal.ingredients.Count > 0)
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

                    if (herbal.ingredients.ContainsKey(@checkItem.QualifiedItemId))
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
                ["(O)684"] = 2,

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

                        if (herbal.ingredients.ContainsKey(@checkItem.QualifiedItemId))
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

                    if (herbal.ingredients.ContainsKey(@checkItem.QualifiedItemId))
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

            Game1.player.currentLocation.playSound("bubbles");

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

            if(Game1.currentGameTime.TotalGameTime.TotalSeconds > consumeBuffer)
            {

                consumeBuffer = Game1.currentGameTime.TotalGameTime.TotalSeconds + 5;

                DisplayPotion hudmessage = new("Consumed " + herbal.title, herbal);

                Game1.addHUDMessage(hudmessage);

            }

            if(herbal.level > 0)
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

            string description = "";

            float speed = 0f;

            int magnetism = 0;

            for(int i = applied.Count - 1; i >= 0; i--)
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

                while(addTime > 0)
                {

                    int updateTime = Math.Min(30, addTime);

                    timeOfDay += updateTime;

                    if(timeOfDay % 100 > 60)
                    {

                        timeOfDay += 40;

                    }

                    addTime -= updateTime;

                    if(timeOfDay > 2599)
                    {

                        timeOfDay = 2600;

                        break;

                    }

                }

                string meridian = "am";

                if(timeOfDay >= 2400)
                {

                    timeOfDay -= 2400;

                }
                else if(timeOfDay >= 1200)
                {

                    timeOfDay -= 1200;

                    meridian = "pm";

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

                        description += Mod.instance.Helper.Translation.Get("herbBuff.ligna.description").Tokens(new { level = herbBuff.Value.level.ToString(), expire = expire });

                        magnetism = herbBuff.Value.level * 32;

                        break;

                    case herbals.impes:

                        description += Mod.instance.Helper.Translation.Get("herbBuff.impes.description").Tokens(new { level = herbBuff.Value.level.ToString(), expire = expire });

                        break;

                    case herbals.celeri:

                        speed = 0.25f * herbBuff.Value.level;

                        description += Mod.instance.Helper.Translation.Get("herbBuff.celeri.description").Tokens(new { level = herbBuff.Value.level.ToString(), expire = expire });

                        break;

                }

            }

            if (!applyChange)
            {

                return;

            }

            if (Game1.player.buffs.IsApplied("184652"))
            {

                Game1.player.buffs.Remove("184652");

            }

            if (applied.Count == 0)
            {

                return;

            }

            Buff herbalBuff = new(
                "184652",
                source: "Stardew Druid",
                displaySource: Mod.instance.Helper.Translation.Get("herbalBuff.displaySource"),
                duration: Buff.ENDLESS,
                iconTexture: Mod.instance.iconData.displayTexture,
                iconSheetIndex: 5,
                displayName: Mod.instance.Helper.Translation.Get("herbalBuff.displayName"),
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

        public IconData.schemes scheme = IconData.schemes.none;

        public IconData.relics container = IconData.relics.flask;

        public IconData.relics content = IconData.relics.flask1;

        public string title;

        public string description;

        public Dictionary<string, string> ingredients = new();

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
