using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{
    public static class HerbalData
    {
        public static string wood = "(O)388";
        public static string hardwood = "(O)709";
        public static string stone = "(O)390";
        public static string omnigeode = "(O)749";
        public static string sap = "(O)92";

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

        public static List<string> HerbalIngredients(HerbalHandle.herbals herbalId)
        {

            switch (herbalId)
            {

                case HerbalHandle.herbals.ligna:
                    return new() {
                        "(O)92", // "Sap", 
                        "(O)766", // "Slime", 
                    };

                case HerbalHandle.herbals.melius_ligna:
                    return new() {
                        "(O)311", // "Acorn", 
                        "(O)310", // "MapleSeed", 
                        "(O)309", // "Pinecorn", 
                        "(O)292", // "MahoganySeed", 
                        "(O)Moss", // "Moss", 
                    };

                case HerbalHandle.herbals.satius_ligna:
                    return new() {
                        "(O)418", // "Crocus", 
                        "(O)18", // "Daffodil", 
                        "(O)22", // "Dandelion", 
                        "(O)402", // "Sweet Pea", 
                        "(O)273", // "Rice Shoot", 
                        "(O)591", // "Tulip", 
                        "(O)376", // "Poppy", 
                    };

                case HerbalHandle.herbals.magnus_ligna:
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

                case HerbalHandle.herbals.optimus_ligna:
                    break;

                case HerbalHandle.herbals.impes:
                    return new() {

                        "(O)831", // "Taro Tubers", 
                        "(O)399", // "Spring Onion", 
                        "(O)78", // "Cave Carrot", 
                        "(O)412", // "Winter Root", 
                        "(O)24", // "Parsnip", 
                        "(O)16", // "Wild Horseradish", 

                    };

                case HerbalHandle.herbals.melius_impes:
                    return new() {
                        "(O)420", // "Red Mushrooms", 
                        "(O)404", // "Common Mushrooms", 
                        "(O)257", // "Morel", 
                        "(O)767", // "Batwings", 
                    };

                case HerbalHandle.herbals.satius_impes:
                    return new() {
                        "(O)93", // "Torch", 
                        "(O)82", // "Fire Quartz", 
                        "(O)382", // "Coal", 
                    };

                case HerbalHandle.herbals.magnus_impes:
                    return new() {
                        "(O)419", // "Vinegar", 
                        "(O)260", // "Hot Pepper", 
                        "(O)829", // "Ginger", 
                        "(O)248", // "Garlic", 
                    };

                case HerbalHandle.herbals.optimus_impes:
                    break;

                case HerbalHandle.herbals.celeri:
                    return new() {

                        "(O)168", // "Trash", 
                        "(O)169", // "Driftwood", 
                        "(O)170", // "Broken Glasses", 
                        "(O)171", // "Broken CD",
                        "(O)330", // "Clay",
                        "(O)881", // "Bone Fragment", 
                        "(O)80", // "Quartz", 
                        "(O)86", // "Earth Crystal", 
                    };

                case HerbalHandle.herbals.melius_celeri:
                    return new() {

                        "(O)167", // "Joja Cola", 
                        "(O)153", // "Seaweed", 
                        "(O)433", // "Coffee Bean", 
                        "(O)157", // "White Algae",
                        "(O)152", // "Algae", 
                        "(O)815", // "Tea Leaves", 

                    };

                case HerbalHandle.herbals.satius_celeri:
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

                case HerbalHandle.herbals.magnus_celeri:
                    return new() {
                        "(O)718", // "Cockle", 
                        "(O)719", // "Mussel", 
                        "(O)720", // "Shrimp", 
                        "(O)721", // "Snail", 
                        "(O)722", // "Periwinkle", 
                        "(O)723", // "Oyster", 
                        "(O)372", // "Clam", 
                    };

                case HerbalHandle.herbals.optimus_celeri:
                    break;

                case HerbalHandle.herbals.faeth:

                    return new() { "(O)577", "(O)595", "(O)768", "(O)769", "(O)MossySeed", };

                case HerbalHandle.herbals.aether:

                    return new() { emerald, aquamarine, ruby, diamond, };

                // bomb line 1
                case HerbalHandle.herbals.imbus:

                    return new() { "(O)709", };

                case HerbalHandle.herbals.amori:

                    return new() { "(O)393", }; // coral

                case HerbalHandle.herbals.donis:

                    return new() { "(O)338", };

                case HerbalHandle.herbals.rapidus:

                    return new() {             // coffee  
                        "(O)395",
                    };

                case HerbalHandle.herbals.coruscant:

                    return new() {

                        "(O)66",
                        "(O)68",
                        "(O)70",
                        "(O)245",
                    };

                // bomb line 2
                case HerbalHandle.herbals.concutere:

                    return new() { "(BC)71", };

                case HerbalHandle.herbals.jumere:

                    return new() { "(O)306", "(O)307", };

                case HerbalHandle.herbals.felis:

                    return new() { "(O)184", "(O)186", };

                case HerbalHandle.herbals.sanctus:

                    return new() { "(O)346", "(O)348", };

                case HerbalHandle.herbals.voil:

                    return new() {

                        omnigeode,
                        "(O)579",
                        "(O)580",
                        "(O)581",
                        "(O)582",
                        "(O)583",
                        "(O)584",
                        "(O)585",

                    };

                // balls
                case HerbalHandle.herbals.captis:

                    return new() { "(O)334", };

                case HerbalHandle.herbals.ferrum_captis:

                    return new() { "(O)335", };

                case HerbalHandle.herbals.aurum_captis:

                    return new() { "(O)336", };

                case HerbalHandle.herbals.diamas_captis:

                    return new() { "(O)337", "(O)787", };

            }

            return new();

        }

        public static Dictionary<string, Herbal> HerbalList()
        {

            Dictionary<string, Herbal> potions = new();

            // ====================================================================
            // Ligna line

            potions[HerbalHandle.herbals.ligna.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.none,

                herbal = HerbalHandle.herbals.ligna,

                display = IconData.potions.ligna,

                grayed = IconData.potions.lignaGray,

                level = 0,

                duration = 0,

                title = Mod.instance.Helper.Translation.Get("HerbalData.25"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.27"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.ligna),

                bases = new() { },

                health = 4,

                stamina = 12,

                price = 50,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.39"),
                    
                },

                itemrequirements = new()
                {
                    
                    Mod.instance.Helper.Translation.Get("HerbalData.40"), 
                
                },


            };


            potions[HerbalHandle.herbals.melius_ligna.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.alignment,

                herbal = HerbalHandle.herbals.melius_ligna,

                display = IconData.potions.ligna1,

                grayed = IconData.potions.lignaGray1,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("HerbalData.61"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.63"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.melius_ligna),

                bases = new() { HerbalHandle.herbals.ligna, },

                health = 8,

                stamina = 30,

                price = 150,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.75"),
                    Mod.instance.Helper.Translation.Get("HerbalData.76"),
                    
                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.391.1"),

                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.77"),

                },

            };


            potions[HerbalHandle.herbals.satius_ligna.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.alignment,

                herbal = HerbalHandle.herbals.satius_ligna,

                display = IconData.potions.ligna2,

                grayed = IconData.potions.lignaGray2,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("HerbalData.98"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.100"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.satius_ligna),

                bases = new() { HerbalHandle.herbals.melius_ligna, },

                health = 20,

                stamina = 80,

                price = 300,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.112"),
                    Mod.instance.Helper.Translation.Get("HerbalData.113"),
                    
                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.391.2"),

                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.114"),

                },

                units = 1,

            };


            potions[HerbalHandle.herbals.magnus_ligna.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.alignment,

                herbal = HerbalHandle.herbals.magnus_ligna,

                display = IconData.potions.ligna3,

                grayed = IconData.potions.lignaGray3,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("HerbalData.135"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.137"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.magnus_ligna),

                bases = new() { HerbalHandle.herbals.satius_ligna, },

                health = 50,

                stamina = 200,

                price = 600,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.149"),
                    Mod.instance.Helper.Translation.Get("HerbalData.150"),
                    
                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.391.3"),

                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.151"),

                },

                units = 2,

            };

            potions[HerbalHandle.herbals.optimus_ligna.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.alignment,

                herbal = HerbalHandle.herbals.optimus_ligna,

                display = IconData.potions.ligna4,

                grayed = IconData.potions.lignaGray4,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("HerbalData.171"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.173"),

                ingredients = new() { },

                bases = new() { HerbalHandle.herbals.magnus_ligna, HerbalHandle.herbals.aether },

                health = 100,

                stamina = 400,

                price = 1200,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.185"),
                    Mod.instance.Helper.Translation.Get("HerbalData.186"),
                    
                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.187"),

                },

                units = 4,

            };

            // ====================================================================
            // Impes series

            potions[HerbalHandle.herbals.impes.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.none,

                herbal = HerbalHandle.herbals.impes,

                display = IconData.potions.impes,

                grayed = IconData.potions.impesGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.206"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.208"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.impes),

                bases = new() { },

                health = 12,

                stamina = 40,

                price = 50,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.220"),
                    
                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.221"),

                },

            };


            potions[HerbalHandle.herbals.melius_impes.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.vigor,

                herbal = HerbalHandle.herbals.melius_impes,

                display = IconData.potions.impes1,

                grayed = IconData.potions.impesGray1,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("HerbalData.242"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.244"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.melius_impes),

                bases = new() { HerbalHandle.herbals.impes, },

                health = 30,

                stamina = 80,

                price = 150,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.256"),
                    Mod.instance.Helper.Translation.Get("HerbalData.257"),
                    
                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.391.5"),

                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.258"),

                },

            };


            potions[HerbalHandle.herbals.satius_impes.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.vigor,

                herbal = HerbalHandle.herbals.satius_impes,

                display = IconData.potions.impes2,

                grayed = IconData.potions.impesGray2,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("HerbalData.279"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.281"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.satius_impes),

                bases = new() { HerbalHandle.herbals.melius_impes, },

                health = 45,

                stamina = 160,

                price = 300,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.293"),
                    Mod.instance.Helper.Translation.Get("HerbalData.294"),

                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.391.6"),

                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.296"),

                },

                units = 1,

            };


            potions[HerbalHandle.herbals.magnus_impes.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.vigor,

                herbal = HerbalHandle.herbals.magnus_impes,

                display = IconData.potions.impes3,

                grayed = IconData.potions.impesGray3,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("HerbalData.317"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.319"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.magnus_impes),

                bases = new() { HerbalHandle.herbals.satius_impes, },

                health = 70,

                stamina = 320,

                price = 600,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.331"),
                    Mod.instance.Helper.Translation.Get("HerbalData.332"),
                    
                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.391.7"),

                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.333"),

                },

                units = 2,

            };

            potions[HerbalHandle.herbals.optimus_impes.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.vigor,

                herbal = HerbalHandle.herbals.optimus_impes,

                display = IconData.potions.impes4,

                grayed = IconData.potions.impesGray4,

                level = 4,

                duration = 480,

                title = Mod.instance.Helper.Translation.Get("HerbalData.353"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.355"),

                ingredients = new() { },

                bases = new() { HerbalHandle.herbals.magnus_impes, HerbalHandle.herbals.aether, },

                health = 180,

                stamina = 560,

                price = 1200,

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.367"),
                    Mod.instance.Helper.Translation.Get("HerbalData.368"),
                    
                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.369"),

                },

                units = 4,

            };

            // ====================================================================
            // Celeri series

            potions[HerbalHandle.herbals.celeri.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.none,

                herbal = HerbalHandle.herbals.celeri,

                display = IconData.potions.celeri,

                grayed = IconData.potions.celeriGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.388"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.390"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.celeri),

                bases = new() { },

                health = 8,

                stamina = 25,

                price = 50,

                details = new()
                {
                   //Mod.instance.Helper.Translation.Get("HerbalData.402"),
                   
                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.403"),

                },

            };

            potions[HerbalHandle.herbals.melius_celeri.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.celerity,

                herbal = HerbalHandle.herbals.melius_celeri,

                display = IconData.potions.celeri1,

                grayed = IconData.potions.celeriGray1,

                level = 1,

                duration = 180,

                title = Mod.instance.Helper.Translation.Get("HerbalData.423"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.425"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.melius_celeri),

                health = 20,

                stamina = 60,

                price = 150,

                bases = new() { HerbalHandle.herbals.celeri, },

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.437"),
                    Mod.instance.Helper.Translation.Get("HerbalData.438"),

                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.391.9"),

                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.439"),

                },

            };


            potions[HerbalHandle.herbals.satius_celeri.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.celerity,

                herbal = HerbalHandle.herbals.satius_celeri,

                display = IconData.potions.celeri2,

                grayed = IconData.potions.celeriGray2,

                level = 2,

                duration = 240,

                title = Mod.instance.Helper.Translation.Get("HerbalData.460"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.462"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.satius_celeri),

                health = 40,

                stamina = 120,

                price = 300,

                bases = new() { HerbalHandle.herbals.melius_celeri, },

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.474"),
                    Mod.instance.Helper.Translation.Get("HerbalData.475"),

                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.391.10"),

                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.476"),

                },

                units = 1,

            };


            potions[HerbalHandle.herbals.magnus_celeri.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.celerity,

                herbal = HerbalHandle.herbals.magnus_celeri,

                display = IconData.potions.celeri3,

                grayed = IconData.potions.celeriGray3,

                level = 3,

                duration = 360,

                title = Mod.instance.Helper.Translation.Get("HerbalData.497"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.499"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.magnus_celeri),

                health = 80,

                stamina = 240,

                price = 600,

                bases = new() { HerbalHandle.herbals.satius_celeri, },

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.511"),
                    Mod.instance.Helper.Translation.Get("HerbalData.512"),
                    
                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.391.11"),

                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.513"),

                },

                units = 2,

            };

            potions[HerbalHandle.herbals.optimus_celeri.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.celerity,

                herbal = HerbalHandle.herbals.optimus_celeri,

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

                bases = new() { HerbalHandle.herbals.magnus_celeri, HerbalHandle.herbals.aether, },

                details = new()
                {
                    //Mod.instance.Helper.Translation.Get("HerbalData.547"),
                    Mod.instance.Helper.Translation.Get("HerbalData.548"),
                    
                },

                potionrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.549"),

                },

                units = 4,

            };

            // ====================================================================
            // Faeth

            potions[HerbalHandle.herbals.faeth.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.none,

                herbal = HerbalHandle.herbals.faeth,

                display = IconData.potions.faeth,

                grayed = IconData.potions.faethGray,

                level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.570"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.572"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.faeth),

                bases = new() { },

                price = 250,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.581"),

                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.582"),

                },

                type = Herbal.herbalType.resource,

                units = 1,

            };

            // ====================================================================
            // Ether

            potions[HerbalHandle.herbals.aether.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.none,

                herbal = HerbalHandle.herbals.aether,

                display = IconData.potions.aether,

                grayed = IconData.potions.aetherGray,

                level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.606"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.608"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.aether),

                bases = new() { },

                price = 500,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.616"),
                    
                },

                itemrequirements = new()
                {

                   Mod.instance.Helper.Translation.Get("HerbalData.617"),

                },

                type = Herbal.herbalType.resource,

                units = 2,

            };


            // ====================================================================
            // Powders

            potions[HerbalHandle.herbals.coruscant.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.none,

                herbal = HerbalHandle.herbals.coruscant,

                display = IconData.potions.coruscant,

                grayed = IconData.potions.coruscantGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.386.1"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.386.2"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.coruscant),

                bases = new(),

                price = 200,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.386.3"),
                    Mod.instance.Helper.Translation.Get("HerbalData.386.4"),
                },

                type = Herbal.herbalType.resource,

            };

            potions[HerbalHandle.herbals.imbus.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.imbuement,

                herbal = HerbalHandle.herbals.imbus,

                display = IconData.potions.imbus,

                grayed = IconData.potions.imbusGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.5"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.6"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.imbus),

                bases = new() { HerbalHandle.herbals.coruscant, },

                duration = 600,

                price = 300,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.500.buff.1"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.8"),
                },

                type = Herbal.herbalType.powder,

                export = ExportHandle.exports.powders,

                units = 2,
            };

            potions[HerbalHandle.herbals.amori.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.amorous,

                herbal = HerbalHandle.herbals.amori,

                display = IconData.potions.amori,

                grayed = IconData.potions.amoriGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.9"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.10"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.amori),

                bases = new() { HerbalHandle.herbals.coruscant, },

                duration = 600,

                price = 300,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.11"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.12"),
                },

                type = Herbal.herbalType.powder,

                export = ExportHandle.exports.powders,

                units = 2,

            };

            potions[HerbalHandle.herbals.donis.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.macerari,

                herbal = HerbalHandle.herbals.donis,

                display = IconData.potions.donis,

                grayed = IconData.potions.donisGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.500.buff.2"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.500.buff.3"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.donis),

                bases = new() { HerbalHandle.herbals.coruscant, },

                duration = 600,

                price = 300,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.500.buff.4"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.16"),
                },

                type = Herbal.herbalType.powder,

                export = ExportHandle.exports.powders,

                units = 2,

            };

            potions[HerbalHandle.herbals.rapidus.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.rapidfire,

                herbal = HerbalHandle.herbals.rapidus,

                display = IconData.potions.rapidus,

                grayed = IconData.potions.rapidusGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.386.5"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.386.6"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.rapidus),

                bases = new() { HerbalHandle.herbals.coruscant, },

                duration = 600,

                price = 300,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.386.7"),
                    Mod.instance.Helper.Translation.Get("HerbalData.386.8"),
                },

                type = Herbal.herbalType.powder,

                export = ExportHandle.exports.powders,

                units = 2,

            };


            // ====================================================================
            // Powders (Stronger)

            potions[HerbalHandle.herbals.voil.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.none,

                herbal = HerbalHandle.herbals.voil,

                display = IconData.potions.voil,

                grayed = IconData.potions.voilGray,

                level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.1"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.2"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.voil),

                bases = new() { },

                price = 250,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.3"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.4"),
                },

                type = Herbal.herbalType.resource,

            };

            potions[HerbalHandle.herbals.concutere.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.concussion,

                herbal = HerbalHandle.herbals.concutere,

                display = IconData.potions.concutere,

                grayed = IconData.potions.concutereGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.17"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.18"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.concutere),

                bases = new() { HerbalHandle.herbals.voil, },

                duration = 600,

                price = 500,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.19"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.20"),
                },

                type = Herbal.herbalType.powder,

                export = ExportHandle.exports.powders,

                units = 3,

            };

            potions[HerbalHandle.herbals.jumere.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.jumper,

                herbal = HerbalHandle.herbals.jumere,

                display = IconData.potions.jumere,

                grayed = IconData.potions.jumereGray,

                // level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.21"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.22"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.jumere),

                bases = new() { HerbalHandle.herbals.voil, },

                duration = 600,

                price = 500,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.361.23"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.24"),
                },

                type = Herbal.herbalType.powder,

                export = ExportHandle.exports.powders,

                units = 3,
            };

            potions[HerbalHandle.herbals.felis.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.feline,

                herbal = HerbalHandle.herbals.felis,

                display = IconData.potions.felis,

                grayed = IconData.potions.felisGray,

                //level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.361.25"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.361.26"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.felis),

                bases = new() { HerbalHandle.herbals.voil, },

                duration = 600,

                price = 500,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.500.buff.5"),
                    Mod.instance.Helper.Translation.Get("HerbalData.361.28"),
                },

                type = Herbal.herbalType.powder,

                export = ExportHandle.exports.powders,

                units = 3,
            };

            potions[HerbalHandle.herbals.sanctus.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.sanctified,

                herbal = HerbalHandle.herbals.sanctus,

                display = IconData.potions.sanctus,

                grayed = IconData.potions.sanctusGray,

                //level = 99,

                title = Mod.instance.Helper.Translation.Get("HerbalData.386.9"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.386.10"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.sanctus),

                bases = new() { HerbalHandle.herbals.voil, },

                duration = 600,

                price = 500,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.386.11"),
                    Mod.instance.Helper.Translation.Get("HerbalData.386.12"),
                },

                type = Herbal.herbalType.powder,

                export = ExportHandle.exports.powders,

                units = 3,
            };

            // ====================================================================
            // Capture Balls

            potions[HerbalHandle.herbals.captis.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.capture,

                herbal = HerbalHandle.herbals.captis,

                display = IconData.potions.captis,

                grayed = IconData.potions.captisGray,

                level = 1,

                title = Mod.instance.Helper.Translation.Get("HerbalData.386.13"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.386.14"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.captis),

                bases = new() { },

                duration = 600,

                price = 100,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.386.15"),
                    Mod.instance.Helper.Translation.Get("HerbalData.386.16"),
                },

                type = Herbal.herbalType.ball,

            };

            potions[HerbalHandle.herbals.ferrum_captis.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.capture,

                herbal = HerbalHandle.herbals.ferrum_captis,

                display = IconData.potions.ferrum_captis,

                grayed = IconData.potions.ferrum_captisGray,

                level = 2,

                title = Mod.instance.Helper.Translation.Get("HerbalData.386.17"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.386.18"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.ferrum_captis),

                bases = new() { HerbalHandle.herbals.captis, },

                duration = 600,

                price = 300,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.386.19"),
                    Mod.instance.Helper.Translation.Get("HerbalData.386.20"),
                },

                type = Herbal.herbalType.ball,

                export = ExportHandle.exports.powders,

                units = 2,

            };

            potions[HerbalHandle.herbals.aurum_captis.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.capture,

                herbal = HerbalHandle.herbals.aurum_captis,

                display = IconData.potions.aurum_captis,

                grayed = IconData.potions.aurum_captisGray,

                level = 3,

                title = Mod.instance.Helper.Translation.Get("HerbalData.386.21"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.386.22"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.aurum_captis),

                bases = new() { HerbalHandle.herbals.ferrum_captis, },

                duration = 600,

                price = 600,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.386.23"),
                    Mod.instance.Helper.Translation.Get("HerbalData.386.24"),
                },

                type = Herbal.herbalType.ball,

                export = ExportHandle.exports.powders,

                units = 3,

            };

            potions[HerbalHandle.herbals.diamas_captis.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.capture,

                herbal = HerbalHandle.herbals.diamas_captis,

                display = IconData.potions.diamas_captis,

                grayed = IconData.potions.diamas_captisGray,

                level = 4,

                title = Mod.instance.Helper.Translation.Get("HerbalData.386.25"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.386.26"),

                ingredients = HerbalIngredients(HerbalHandle.herbals.diamas_captis),

                bases = new() { HerbalHandle.herbals.aurum_captis, },

                duration = 600,

                price = 2000,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.386.27"),
                    Mod.instance.Helper.Translation.Get("HerbalData.386.28"),
                },

                type = Herbal.herbalType.ball,

                export = ExportHandle.exports.powders,

                units = 5,

            };

            potions[HerbalHandle.herbals.capesso.ToString()] = new()
            {

                buff = HerbalBuff.herbalbuffs.spellcatch,

                herbal = HerbalHandle.herbals.capesso,

                display = IconData.potions.capesso,

                grayed = IconData.potions.capessoGray,

                level = 1,

                title = Mod.instance.Helper.Translation.Get("HerbalData.399.1"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.399.2"),

                ingredients = new(),

                bases = new() { HerbalHandle.herbals.coruscant, HerbalHandle.herbals.voil, },

                duration = 600,

                price = 1000,

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.399.3"),
                    Mod.instance.Helper.Translation.Get("HerbalData.399.4"),
                },

                type = Herbal.herbalType.ball,

                export = ExportHandle.exports.powders,

                units = 3,

            };

            // ====================================================================
            // Omens

            potions[HerbalHandle.herbals.omen_feather.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.omen_feather,

                display = IconData.potions.omenFeather,

                grayed = IconData.potions.omenFeatherGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.56"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.57"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.60"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.61"),
                },

                type = Herbal.herbalType.omen,

                export = ExportHandle.exports.omens,

                units = 1,

            };

            potions[HerbalHandle.herbals.omen_tuft.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.omen_tuft,

                display = IconData.potions.omenTuft,

                grayed = IconData.potions.omenTuftGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.72"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.73"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.76"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.77"),
                },

                type = Herbal.herbalType.omen,

                export = ExportHandle.exports.omens,

                units = 1,

            };

            potions[HerbalHandle.herbals.omen_shell.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.omen_shell,

                display = IconData.potions.omenShell,

                grayed = IconData.potions.omenShellGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.88"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.89"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.92"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.93"),
                },

                type = Herbal.herbalType.omen,

                export = ExportHandle.exports.omens,

                units = 1,

            };

            potions[HerbalHandle.herbals.omen_tusk.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.omen_tusk,

                display = IconData.potions.omenTusk,

                grayed = IconData.potions.omenTuskGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.104"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.105"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.108"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.109"),
                },

                type = Herbal.herbalType.omen,

                export = ExportHandle.exports.omens,

                units = 1,

            };

            potions[HerbalHandle.herbals.omen_nest.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.omen_nest,

                display = IconData.potions.omenNest,

                grayed = IconData.potions.omenNestGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.120"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.121"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.124"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.125"),
                },

                type = Herbal.herbalType.omen,

                export = ExportHandle.exports.omens,

                units = 1,

            };

            potions[HerbalHandle.herbals.omen_glass.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.omen_glass,

                display = IconData.potions.omenGlass,

                grayed = IconData.potions.omenGlassGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.136"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.137"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.140"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.141"),
                },

                type = Herbal.herbalType.omen,

                export = ExportHandle.exports.omens,

                units = 1,

            };

            potions[HerbalHandle.herbals.omen_down.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.omen_down,

                display = IconData.potions.omenDown,

                grayed = IconData.potions.omenDownGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.393.1.104"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.393.1.105"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.108"),
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.109"),
                },

                type = Herbal.herbalType.omen,

                export = ExportHandle.exports.omens,

                units = 1,

            };

            potions[HerbalHandle.herbals.omen_coral.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.omen_coral,

                display = IconData.potions.omenCoral,

                grayed = IconData.potions.omenCoralGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.393.1.120"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.393.1.121"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.124"),
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.125"),
                },

                type = Herbal.herbalType.omen,

                export = ExportHandle.exports.omens,

                units = 1,

            };

            potions[HerbalHandle.herbals.omen_bloom.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.omen_bloom,

                display = IconData.potions.omenBloom,

                grayed = IconData.potions.omenBloomGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.393.1.136"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.393.1.137"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.140"),
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.141"),
                },

                type = Herbal.herbalType.omen,

                export = ExportHandle.exports.omens,

                units = 3,

            };


            // ====================================================================
            // Trophies

            potions[HerbalHandle.herbals.trophy_shroom.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.trophy_shroom,

                display = IconData.potions.trophyShroom,

                grayed = IconData.potions.trophyShroomGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.154"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.155"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.158"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.159"),
                },

                type = Herbal.herbalType.trophy,

                export = ExportHandle.exports.trophies,

                units = 1,

            };

            potions[HerbalHandle.herbals.trophy_eye.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.trophy_eye,

                display = IconData.potions.trophyEye,

                grayed = IconData.potions.trophyEyeGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.170"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.171"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.174"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.175"),
                },

                type = Herbal.herbalType.trophy,

                export = ExportHandle.exports.trophies,

                units = 1,

            };

            potions[HerbalHandle.herbals.trophy_pumpkin.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.trophy_pumpkin,

                display = IconData.potions.trophyPumpkin,

                grayed = IconData.potions.trophyPumpkinGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.186"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.187"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.190"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.191"),
                },

                type = Herbal.herbalType.trophy,

                export = ExportHandle.exports.trophies,

                units = 1,

            };

            potions[HerbalHandle.herbals.trophy_pearl.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.trophy_pearl,

                display = IconData.potions.trophyPearl,

                grayed = IconData.potions.trophyPearlGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.202"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.203"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.206"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.207"),
                },

                type = Herbal.herbalType.trophy,

                export = ExportHandle.exports.trophies,

                units = 1,

            };

            potions[HerbalHandle.herbals.trophy_tooth.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.trophy_tooth,

                display = IconData.potions.trophyTooth,

                grayed = IconData.potions.trophyToothGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.218"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.219"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.222"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.223"),
                },

                type = Herbal.herbalType.trophy,

                export = ExportHandle.exports.trophies,

                units = 1,

            };

            potions[HerbalHandle.herbals.trophy_spiral.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.trophy_spiral,

                display = IconData.potions.trophyShell,

                grayed = IconData.potions.trophyShellGray,

                title = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.234"),

                description = Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.235"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.238"),
                    Mod.instance.Helper.Translation.Get("HerbalHandle.390.1.239"),
                },

                type = Herbal.herbalType.trophy,

                export = ExportHandle.exports.trophies,

                units = 1,

            };


            potions[HerbalHandle.herbals.trophy_spike.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.trophy_spike,

                display = IconData.potions.trophySpike,

                grayed = IconData.potions.trophySpikeGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.393.1.56"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.393.1.57"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.60"),
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.61"),
                },

                type = Herbal.herbalType.trophy,

                export = ExportHandle.exports.trophies,

                units = 1,

            };

            potions[HerbalHandle.herbals.trophy_seed.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.trophy_seed,

                display = IconData.potions.trophySeed,

                grayed = IconData.potions.trophySeedGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.393.1.72"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.393.1.73"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.76"),
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.77"),
                },

                type = Herbal.herbalType.trophy,

                export = ExportHandle.exports.trophies,

                units = 1,

            };

            potions[HerbalHandle.herbals.trophy_dragon.ToString()] = new()
            {

                herbal = HerbalHandle.herbals.trophy_dragon,

                display = IconData.potions.trophyDragon,

                grayed = IconData.potions.trophyDragonGray,

                title = Mod.instance.Helper.Translation.Get("HerbalData.393.1.88"),

                description = Mod.instance.Helper.Translation.Get("HerbalData.393.1.89"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.92"),
                    Mod.instance.Helper.Translation.Get("HerbalData.393.1.93"),
                },

                type = Herbal.herbalType.trophy,

                export = ExportHandle.exports.trophies,

                units = 3,

            };

            return potions;

        }

    }

}
