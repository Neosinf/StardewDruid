using StardewDruid.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{
    public static class ExportData
    {

        public static Dictionary<ExportHandle.exports, ExportGood> LoadGoods()
        {

            Dictionary<ExportHandle.exports, ExportGood> goods = new();

            // --------------------------------------------

            ExportGood potions = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.58"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.59"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.388.1"),
                display = IconData.workshops.potions,
                good = ExportHandle.exports.potions,
                license = IconData.relics.crest_church,
                guild = ExportHandle.exports.church,
                price = 250,
                peak = 42,
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("ExportData.386.67"),
                },

            };

            goods.Add(ExportHandle.exports.potions, potions);

            // --------------------------------------------

            ExportGood powders = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.74"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.75"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.388.2"),
                display = IconData.workshops.powders,
                good = ExportHandle.exports.powders,
                license = IconData.relics.crest_church,
                guild = ExportHandle.exports.church,
                price = 400,
                peak = 92,
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("ExportData.386.83"),
                },
            };

            goods.Add(ExportHandle.exports.powders, powders);

            // --------------------------------------------

            ExportGood trophies = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.90"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.91"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.388.3"),
                display = IconData.workshops.trophies,
                good = ExportHandle.exports.trophies,
                license = IconData.relics.crest_church,
                guild = ExportHandle.exports.associate,
                price = 500,
                peak = 22,
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("ExportData.386.99"),
                },
            };

            goods.Add(ExportHandle.exports.trophies, trophies);

            // --------------------------------------------

            ExportGood omens = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.106"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.107"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.388.4"),
                display = IconData.workshops.omens,
                good = ExportHandle.exports.omens,
                license = IconData.relics.crest_church,
                guild = ExportHandle.exports.associate,
                price = 250,
                peak = 52,
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("ExportData.386.115"),
                },
            };

            goods.Add(ExportHandle.exports.omens, omens);

            // --------------------------------------------

            ExportGood whiskey = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.122"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.123"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.124"),
                display = IconData.workshops.whiskey,
                good = ExportHandle.exports.whiskey,
                license = IconData.relics.crest_dwarf,
                guild = ExportHandle.exports.dwarf,
                price = 150,
                peak = 102,
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("ExportData.386.132"),
                    Mod.instance.Helper.Translation.Get("ExportData.388.5"),
                },
            };

            goods.Add(ExportHandle.exports.whiskey, whiskey);

            // --------------------------------------------

            ExportGood brandy = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.139"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.140"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.141"),
                display = IconData.workshops.brandy,
                good = ExportHandle.exports.brandy,
                license = IconData.relics.crest_dwarf,
                guild = ExportHandle.exports.dwarf,
                price = 250,
                peak = 72,
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("ExportData.386.149"),
                    Mod.instance.Helper.Translation.Get("ExportData.388.5"),
                },
            };

            goods.Add(ExportHandle.exports.brandy, brandy);

            // --------------------------------------------

            ExportGood weapons = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.156"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.157"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.158"),
                display = IconData.workshops.weapons,
                good = ExportHandle.exports.weapons,
                license = IconData.relics.crest_associate,
                guild = ExportHandle.exports.smuggler,
                price = 1000,
                peak = 62,
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("ExportData.386.166"),
                    Mod.instance.Helper.Translation.Get("ExportData.388.5"),
                },
            };

            goods.Add(ExportHandle.exports.weapons, weapons);

            // --------------------------------------------

            ExportGood supplies = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.173"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.174"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.175"),
                display = IconData.workshops.supplies,
                good = ExportHandle.exports.supplies,
                license = IconData.relics.crest_associate,
                guild = ExportHandle.exports.smuggler,
                price = 350,
                peak = 32,
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("ExportData.386.183"),
                    Mod.instance.Helper.Translation.Get("ExportData.388.5"),
                },
            };

            goods.Add(ExportHandle.exports.supplies, supplies);

            return goods;

        }

        public static Dictionary<ExportHandle.exports, ExportMachine> LoadMachines()
        {

            Dictionary<ExportHandle.exports, ExportMachine> machines = new();

            // --------------------------------------------

            ExportMachine crushers = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.194"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.195"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.196"),
                display = IconData.workshops.crushers,
                machine = ExportHandle.exports.crushers,
                pal = CharacterHandle.characters.PalSlime,
                labour = 3,
                resources = new()
                {
                    ["(O)335"] = 2,
                    ["(O)390"] = 100,
                },

            };

            machines.Add(ExportHandle.exports.crushers, crushers);

            // --------------------------------------------
            ExportMachine press = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.207"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.208"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.209"),
                display = IconData.workshops.press,
                machine = ExportHandle.exports.press,
                pal = CharacterHandle.characters.PalSlime,
                labour = 2,
                resources = new()
                {
                    ["(O)335"] = 2,
                    ["(O)388"] = 100,
                },
            };

            machines.Add(ExportHandle.exports.press, press);

            // --------------------------------------------
            ExportMachine kiln = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.220"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.221"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.222"),
                display = IconData.workshops.kiln,
                machine = ExportHandle.exports.kiln,
                pal = CharacterHandle.characters.PalSpirit,
                labour = 3,
                resources = new()
                {
                    ["(O)334"] = 2,
                    ["(O)390"] = 100,
                },
            };

            machines.Add(ExportHandle.exports.kiln, kiln);

            // --------------------------------------------
            ExportMachine mashtun = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.233"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.234"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.235"),
                display = IconData.workshops.mashtun,
                machine = ExportHandle.exports.mashtun,
                pal = CharacterHandle.characters.PalSpirit,
                labour = 2,
                resources = new()
                {
                    ["(O)334"] = 2,
                    ["(O)388"] = 100,
                },
            };

            machines.Add(ExportHandle.exports.mashtun, mashtun);

            // --------------------------------------------
            ExportMachine fermentation = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.246"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.247"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.248"),
                display = IconData.workshops.fermentation,
                machine = ExportHandle.exports.fermentation,
                pal = CharacterHandle.characters.PalBat,
                labour = 3,
                resources = new()
                {
                    ["(O)335"] = 2,
                    ["(O)388"] = 100,
                },
            };

            machines.Add(ExportHandle.exports.fermentation, fermentation);

            // --------------------------------------------
            ExportMachine distillery = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.259"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.260"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.261"),
                display = IconData.workshops.distillery,
                machine = ExportHandle.exports.distillery,
                pal = CharacterHandle.characters.PalBat,
                labour = 2,
                resources = new()
                {
                    ["(O)334"] = 2,
                    ["(O)390"] = 100,
                },
            };

            machines.Add(ExportHandle.exports.distillery, distillery);


            // --------------------------------------------
            ExportMachine barrel = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.272"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.273"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.274"),
                display = IconData.workshops.barrel,
                machine = ExportHandle.exports.barrel,
                pal = CharacterHandle.characters.PalGhost,
                labour = 2,
                resources = new()
                {
                    ["(O)335"] = 2,
                    ["(O)388"] = 100,
                },
            };

            machines.Add(ExportHandle.exports.barrel, barrel);


            // --------------------------------------------
            ExportMachine packer = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.285"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.286"),
                technical = Mod.instance.Helper.Translation.Get("ExportData.386.287"),
                display = IconData.workshops.packer,
                machine = ExportHandle.exports.packer,
                pal = CharacterHandle.characters.PalSerpent,
                labour = 1,
                resources = new()
                {
                    ["(O)335"] = 2,
                    ["(O)390"] = 100,
                },
            };

            machines.Add(ExportHandle.exports.packer, packer);

            return machines;

        }

        public static Dictionary<ExportHandle.exports, ExportGuild> LoadGuilds()
        {

            Dictionary<ExportHandle.exports, ExportGuild> guilds = new();

            ExportGuild church = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.302"),
                intro = Mod.instance.Helper.Translation.Get("ExportData.386.303"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.304") +
                Mod.instance.Helper.Translation.Get("ExportData.386.305"),
                guild = ExportHandle.exports.church,
                benefits = new()
                {
                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.309"),
                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.310"),
                    [4] = Mod.instance.Helper.Translation.Get("ExportData.390.1"),
                },
                license = IconData.relics.crest_church,

                orderTitles = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.371"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.373"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.375"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.377"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.379"),
                },

                orderDescriptions = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.438"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.440"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.442"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.444"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.446"),
                },

                orderFulfilled = Mod.instance.Helper.Translation.Get("ExportData.386.501"),

            };

            guilds.Add(ExportHandle.exports.church, church);

            // --------------------------------------------

            ExportGuild dwarf = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.318"),
                intro = Mod.instance.Helper.Translation.Get("ExportData.386.319"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.320"),
                guild = ExportHandle.exports.dwarf,
                benefits = new()
                {
                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.324"),
                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.325"),
                    [4] = Mod.instance.Helper.Translation.Get("ExportData.390.2"),
                },
                license = IconData.relics.crest_dwarf,

                orderTitles = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.386"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.388"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.390"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.392"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.394"),
                },

                orderDescriptions = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.453"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.455"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.457"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.459"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.461"),
                },

                orderFulfilled = Mod.instance.Helper.Translation.Get("ExportData.386.503"),

            };

            guilds.Add(ExportHandle.exports.dwarf, dwarf);

            // --------------------------------------------

            ExportGuild associate = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.333"),
                intro = Mod.instance.Helper.Translation.Get("ExportData.386.334"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.335"),
                guild = ExportHandle.exports.associate,
                benefits = new()
                {
                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.339"),
                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.340"),
                    [4] = Mod.instance.Helper.Translation.Get("ExportData.390.3"),
                },
                license = IconData.relics.crest_associate,

                orderTitles = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.401"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.403"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.405"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.407"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.409"),
                },

                orderDescriptions = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.468"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.470"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.472"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.474"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.476"),
                },

                orderFulfilled = Mod.instance.Helper.Translation.Get("ExportData.386.505"),

            };

            guilds.Add(ExportHandle.exports.associate, associate);

            // --------------------------------------------

            ExportGuild smuggler = new()
            {

                name = Mod.instance.Helper.Translation.Get("ExportData.386.348"),
                intro = Mod.instance.Helper.Translation.Get("ExportData.386.349"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.350"),
                guild = ExportHandle.exports.smuggler,
                benefits = new()
                {
                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.354"),
                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.355"),
                    [4] = Mod.instance.Helper.Translation.Get("ExportData.390.4"),
                },
                license = IconData.relics.crest_smuggler,

                orderTitles = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.416"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.418"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.420"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.422"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.424"),
                },

                orderDescriptions = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.483"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.485"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.487"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.489"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.491"),
                },

                orderFulfilled = Mod.instance.Helper.Translation.Get("ExportData.386.507"),

            };

            guilds.Add(ExportHandle.exports.smuggler, smuggler);

            return guilds;

        }

    }

}
