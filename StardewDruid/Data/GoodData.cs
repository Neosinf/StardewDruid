using StardewDruid.Handle;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{
    public static class GoodData
    {

        public static Dictionary<ExportGood.goods, ExportGood> LoadGoods()
        {

            Dictionary<ExportGood.goods, ExportGood> goods = new();

            // --------------------------------------------

            ExportGood potions = new()
            {

                good = ExportGood.goods.potions,
                name = "Potion Box",
                description = "A box of organic homebrewed potions",
                guild = ExportGuild.guilds.church,
                price = 250,
                peak = 42,
                details = new()
                {
                    "Produced by packing potions (using the 'send to goods' option in the apothecary).",
                    "Tier 3 potions will fill 1 box each, Tier 4 potions will fill 2 boxes each, and Tier 5 will fill 4-5 boxes.",
                    "Demand increases in Summer",
                },

            };

            goods.Add(ExportGood.goods.potions, potions);

            // --------------------------------------------

            ExportGood powders = new()
            {
                good = ExportGood.goods.powders,
                name = "Powder Crate",
                description = "A crate of volative bomb powders. Carry with caution!",
                guild = ExportGuild.guilds.church,
                price = 400,
                peak = 92,
                details = new()
                {
                    "Produced by dispatching Powders using the Send-To-Goods option in the powderbox menu.",
                    "Tier 1 powders will fill 2 crate each, Tier 2 powders will fill 3 crates each, and Tier 3 will fill 4-5 crates.",
                    "Demand increases in Winter",
                },
            };

            goods.Add(ExportGood.goods.powders, powders);

            // --------------------------------------------

            ExportGood trophies = new()
            {
                good = ExportGood.goods.trophies,
                name = "Trophies",
                description = "Special items looted from vanquished foes, many with peculiar alchemical qualities",
                guild = ExportGuild.guilds.associate,
                price = 500,
                peak = 22,
                details = new()
                {
                    "Produced by dispatching Trophies using the Send-To-Goods option in the satchel menu.",
                    "Quantity per trophy varies by trophy type.",
                    "Demand increases in Spring",
                },
            };

            goods.Add(ExportGood.goods.trophies, trophies);

            // --------------------------------------------

            ExportGood omens = new()
            {
                good = ExportGood.goods.omens,
                name = "Valley Omens",
                description = "Little gifts and offerings from the critters of the wild, useful for alchemy and rites of foretelling",
                guild = ExportGuild.guilds.associate,
                price = 250,
                peak = 52,
                details = new()
                {
                    "Produced by dispatching Omens using the Send-To-Goods option in the satchel menu. ",
                    "Quantity per omen varies by omen type.",
                    "Demand increases in late Summer",
                },
            };

            goods.Add(ExportGood.goods.omens, omens);

            // --------------------------------------------

            ExportGood whiskey = new()
            {
                good = ExportGood.goods.whiskey,
                name = "Whiskey Batch",
                description = "The farm provides, and the mountain perfects",
                guild = ExportGuild.guilds.dwarf,
                price = 150,
                peak = 102,
                details = new()
                {
                    "Produced from Malt during the daily distillery production cycle.",
                    "Demand increases in late Winter",
                },
            };

            goods.Add(ExportGood.goods.whiskey, whiskey);

            // --------------------------------------------

            ExportGood brandy = new()
            {
                good = ExportGood.goods.brandy,
                name = "Brandy Batch",
                description = "You trust the Bats knew what they were doing when they distilled this stuff.",
                guild = ExportGuild.guilds.dwarf,
                price = 250,
                peak = 72,
                details = new()
                {
                    "Produced from Must during the daily Distillery production cycle.",
                    "Construct crushers and pressers to increase production.",
                    "Demand increases in Fall",
                },
            };

            goods.Add(ExportGood.goods.brandy, brandy);

            // --------------------------------------------

            ExportGood weapons = new()
            {
                good = ExportGood.goods.aquavitae,
                name = "Aqua Vitae",
                description = "Specially distilled spirits that boost physical and spiritual health.",
                guild = ExportGuild.guilds.smuggler,
                price = 1000,
                peak = 62,
                details = new()
                {
                    "Produced from Tribute during the daily Distillery production cycle.",
                    "Demand increases in early Fall",
                },
            };

            goods.Add(ExportGood.goods.aquavitae, weapons);

            // --------------------------------------------

            ExportGood supplies = new()
            {
                good = ExportGood.goods.ambrosia,
                name = "Ambrosia",
                description = "A prestige spirit that is regarded as worthy of divinity",
                guild = ExportGuild.guilds.smuggler,
                price = 500,
                peak = 32,
                details = new()
                {
                    "Produced from Nectar during the daily Distillery production cycle.",
                    "Demand increases in early Summer",
                },
            };

            goods.Add(ExportGood.goods.ambrosia, supplies);

            return goods;
            
        }

    }

}
