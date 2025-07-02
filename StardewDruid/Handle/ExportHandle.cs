using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast.Fates;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Delegates;
using StardewValley.GameData.Machines;
using StardewValley.GameData.Shops;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xTile.Dimensions;
using static StardewDruid.Handle.ExportHandle;
using static StardewValley.Minigames.TargetGame;

namespace StardewDruid.Handle
{

    public class ExportHandle
    {

        public enum exports
        {

            potions,
            powders,
            whiskey,
            brandy,

            weapons,
            supplies,
            trophies,
            omens,

            crushers,
            press,
            kiln,
            mashtun,

            fermentation,
            distillery,
            barrel,
            packer,

            church,
            dwarf,
            associate,
            smuggler,

        }

        public Dictionary<exports, List<int>> stock = new();

        public Dictionary<exports, ExportGood> goods = new();

        public Dictionary<exports, ExportMachine> machines = new();

        public Dictionary<exports, ExportGuild> guilds = new();

        public int day = 0;

        public List<string> recents = new();

        public List<string> calculations = new();

        public Dictionary<exports, int> products = new();

        public Dictionary<exports, int> estimates = new();

        public ExportHandle()
        {

        }

        public void LoadExports()
        {

            goods = ExportData.LoadGoods();

            machines = ExportData.LoadMachines();

            guilds = ExportData.LoadGuilds();

        }

        public Dictionary<int, ContentComponent> JournalOrders()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            foreach (KeyValuePair<int, ExportOrder> orderEntry in Mod.instance.save.orders)
            {

                ExportOrder order = orderEntry.Value;

                ContentComponent content = new(ContentComponent.contentTypes.order, orderEntry.Key.ToString());

                content.textureSources[0] = IconData.WorkshopRectangles(goods[order.good].display);

                content.textureSources[1] = IconData.RelicRectangles(guilds[order.guild].license);

                ExportGuild guild = guilds[order.guild];

                content.text[0] = guild.orderTitles[order.level];


                if (order.complete)
                {

                    content.text[1] = guild.orderFulfilled;

                    content.text[2] = order.requirement.ToString();

                    content.text[3] = order.sale.ToString() + StringData.currency;

                    content.textColours[0] = Color.DarkSlateGray;

                    content.textColours[1] = Color.DarkSlateGray;

                    content.textColours[2] = Color.DarkSlateGray;

                    content.textColours[3] = Color.DarkSlateGray;

                    content.textColours[4] = Color.DarkSlateGray;

                }
                else if (!Mod.instance.save.exports.ContainsKey(order.good))
                {

                    content.text[1] = guild.orderDescriptions[order.level];

                    content.text[2] = 0 + StringData.slash + order.requirement;

                    content.text[3] = StringData.Strings(StringData.stringkeys.shortfall);

                    content.textColours[3] = Color.DarkRed;


                }
                else if (Mod.instance.save.exports[order.good] < order.floor)
                {

                    content.text[1] = guild.orderDescriptions[order.level];

                    content.text[2] = Mod.instance.save.exports[order.good] + StringData.slash + order.requirement;

                    content.text[3] = StringData.Strings(StringData.stringkeys.shortfall);

                    content.textColours[3] = Color.DarkRed;

                }
                else 
                { 

                    content.text[1] = guild.orderDescriptions[order.level];

                    int provided = Math.Min(Mod.instance.save.exports[order.good], order.requirement);

                    content.text[2] = provided + StringData.slash + order.requirement;

                    int price = OrderPrice(order.good, order.requirement, provided, order.timer);

                    content.text[3] = price.ToString() + StringData.currency;

                    content.textColours[3] = Color.DarkGreen;

                }

                content.text[4] = goods[order.good].name;

                journal[start++] = content;

            }

            return journal;

        }

        public void StockTake()
        {
            
            if (!Context.IsMainPlayer)
            {

                QueryData queryData = new();

                Mod.instance.EventQuery(queryData, QueryData.queries.RequestGoods);

                return;

            }

            stock.Clear();

            int season = (int)Game1.season;

            day = season * 28 + Game1.dayOfMonth;

            foreach (KeyValuePair<exports, ExportGood> good in goods)
            {

                int amount = 0;

                if (Mod.instance.save.exports.ContainsKey(good.Value.good))
                {

                    amount = Mod.instance.save.exports[good.Value.good];

                }

                int currentPrice = GoodsPrice(good.Key, day);

                int sellPrice = 0;

                if(GuildLevel(good.Value.guild) >= 4)
                {

                    sellPrice = (int)(currentPrice * 0.6f);

                }

                stock[good.Key] = new()
                {
                    amount,
                    currentPrice,
                    sellPrice,
                };

            }

        }

        public void PostStock()
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            StockTake();

            QueryData query = new()
            {

                value = System.Text.Json.JsonSerializer.Serialize(stock),

            };

            Mod.instance.EventQuery(query, QueryData.queries.SyncGoods);

        }

        public void SynchroniseStock(string stockData)
        {

            if (Context.IsMainPlayer)
            {

                return;

            }

            stock = System.Text.Json.JsonSerializer.Deserialize<Dictionary<exports, List<int>>>(stockData);

            if(Game1.activeClickableMenu is GoodsJournal goodsJournal)
            {

                goodsJournal.populateContent();

            }

        }

        public Dictionary<int, ContentComponent> JournalGoods()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            foreach (KeyValuePair<exports, ExportGood> good in goods)
            {

                ContentComponent content = new(ContentComponent.contentTypes.goods, good.Key.ToString());

                List<int> goodStock = stock[good.Key];

                content.text[0] = goodStock[0].ToString();

                content.textureSources[0] = IconData.WorkshopRectangles(good.Value.display);

                // -------------------------------------


                content.text[1] = goodStock[1].ToString() + StringData.currency;

                // -------------------------------------

                int priceDifference = goodStock[1] - good.Value.price;

                if (priceDifference > 0)
                {

                    content.textColours[2] = Color.DarkGreen;

                    content.text[2] = StringData.plus + priceDifference.ToString() + StringData.currency;

                }
                else
                {

                    content.textColours[2] = Color.DarkRed;

                    content.text[2] = priceDifference.ToString() + StringData.currency;

                }

                journal[start++] = content;

                

            }

            return journal;

        }

        public Dictionary<int, ContentComponent> JournalDistillery()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            foreach (KeyValuePair<exports, ExportMachine> machine in machines)
            {

                ContentComponent content = new(ContentComponent.contentTypes.machine, machine.Key.ToString());

                int amount = 0;

                if (Mod.instance.save.exports.ContainsKey(machine.Value.machine))
                {

                    amount = Mod.instance.save.exports[machine.Value.machine];

                }

                content.text[0] = amount.ToString();

                content.textureSources[0] = IconData.WorkshopRectangles(machine.Value.display);

                journal[start++] = content;

            }

            List<exports> exportContent = new()
            {
                exports.whiskey,
                exports.brandy,
                exports.weapons,
                exports.supplies,
            };

            for (int e = 0; e < exportContent.Count; e++)
            {

                exports good = exportContent.ElementAt(e);

                ContentComponent content = new(ContentComponent.contentTypes.estimate, good.ToString());

                content.textures[0] = Mod.instance.iconData.workshopTexture;

                content.textureSources[0] = IconData.WorkshopRectangles(goods[good].display);

                int estimate = Mod.instance.exportHandle.estimates[good];

                if (Mod.instance.exportHandle.estimates.ContainsKey(good))
                {

                    estimate = Mod.instance.exportHandle.estimates[good];

                }

                content.text[0] = estimate.ToString();

                int product = 0;

                if (Mod.instance.exportHandle.products.ContainsKey(good))
                {

                    product = Mod.instance.exportHandle.products[good];

                }

                // -------------------------------------

                int difference = estimate - product;

                if (difference > 0)
                {

                    content.textColours[1] = Color.DarkGreen;

                    content.text[1] = StringData.plus +difference.ToString();

                }
                else
                {

                    content.textColours[1] = Color.DarkRed;

                    content.text[1] = difference.ToString();

                }

                journal[start++] = content;

            }

            return journal;

        }

        public int GoodsPrice(exports export, int target)
        {

            ExportGood good = goods[export];

            float diff;

            if (target > good.peak)
            {

                diff = Math.Min(target - good.peak, good.peak + 112 - target);

            }
            else
            {

                diff = Math.Min(good.peak - target, target + 112 - good.peak);


            }

            float diff2 = diff / 112f;

            float diff3 = 1.25f - diff2;

            int adjustPrice = (int)(good.price * diff3);

            switch (export)
            {
                case exports.potions:

                    if(GuildLevel(exports.church) > 1)
                    {

                        adjustPrice = (int)(adjustPrice * 1.2f);

                    }

                    break;

                case exports.powders:

                    if (GuildLevel(exports.associate) > 1)
                    {

                        adjustPrice = (int)(adjustPrice * 1.2f);

                    }

                    break;

                case exports.whiskey:

                    if (GuildLevel(exports.dwarf) > 1)
                    {

                        adjustPrice = (int)(adjustPrice * 1.2f);

                    }

                    break;
                case exports.brandy:

                    if (GuildLevel(exports.dwarf) > 1)
                    {

                        adjustPrice = (int)(adjustPrice * 1.2f);

                    }

                    break;

                case exports.trophies:

                    if (GuildLevel(exports.smuggler) > 1)
                    {

                        adjustPrice = (int)(adjustPrice * 1.2f);

                    }

                    break;
                case exports.omens:

                    if (GuildLevel(exports.church) > 1)
                    {

                        adjustPrice = (int)(adjustPrice * 1.2f);

                    }

                    break;

                case exports.weapons:

                    if (GuildLevel(exports.smuggler) > 1)
                    {

                        adjustPrice = (int)(adjustPrice * 1.2f);

                    }

                    break;
                case exports.supplies:

                    if (GuildLevel(exports.associate) > 1)
                    {

                        adjustPrice = (int)(adjustPrice * 1.2f);

                    }

                    break;
            }

            return adjustPrice;

        }

        public void AddExport(exports export, int amount = 1)
        {

            if(amount == 0)
            {

                return;

            }

            if (!Context.IsMainPlayer)
            {

                QueryData queryData = new()
                {

                    name = export.ToString(),
                    value = amount.ToString(),

                };

                Mod.instance.EventQuery(queryData, QueryData.queries.AddExport);

                return;

            }

            if (!Mod.instance.save.exports.ContainsKey(export))
            {

                Mod.instance.save.exports[export] = amount;

                return;

            }

            Mod.instance.save.exports[export] += amount;

            if (Mod.instance.save.exports[export] > 9999)
            {

                Mod.instance.save.exports[export] = 0;

            }

        }

        public int OrderPrice(exports good, int requirement, int provided, int timer)
        {

            int season = (int)Game1.season;

            day = season * 28 + Game1.dayOfMonth;

            int basePrice = GoodsPrice(good,day);

            int price = requirement * basePrice;

            // penalty ----------------------------

            int pricePenalty = 0;

            int shortFall = requirement - provided;

            if (shortFall > 0)
            {

                float penalty = shortFall / (requirement / 2);

                pricePenalty = (int)(penalty * price);

            }

            price -= pricePenalty;

            // bonus ------------------------------

            int priceBonus = 0;

            if(timer > 1)
            {

                float bonus = timer * 0.1f;

                priceBonus = (int)(bonus * price);

            }

            price += priceBonus;

            return price;

        }

        public bool CraftMachine(exports export, int amount)
        {

            ExportMachine machine = machines[export];

            if (Mod.instance.save.pals.ContainsKey(machine.pal))
            {

                int labour = (Mod.instance.save.pals[machine.pal].caught - Mod.instance.save.pals[machine.pal].hired) / machine.labour;

                amount = Math.Min(labour,amount);

            }
            else
            {

                return false;

            }

            foreach (KeyValuePair<string, int> item in machine.resources)
            {

                int required = Game1.player.Items.CountId(item.Key) / item.Value;

                amount = Math.Min(required, amount);

            }

            if(amount < 1)
            {

                return false;

            }

            Mod.instance.save.pals[machine.pal].hired += amount * machine.labour;

            foreach (KeyValuePair<string, int> item in machine.resources)
            {

                int consumed = item.Value * amount;

                Game1.player.Items.ReduceId(item.Key,consumed);

            }

            Mod.instance.exportHandle.AddExport(export, amount);

            return true;

        }

        public void Produce()
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.distillery))
            {

                return;

            }

            CalculateOutput(true);

        }

        public void Orders()
        {

            Dictionary<exports, int> currentGoods = new();

            Dictionary<exports, int> currentGuilds = new();

            for (int o = Mod.instance.save.orders.Count - 1; o >= 0; o--)
            {

                KeyValuePair<int, ExportOrder> order = Mod.instance.save.orders.ElementAt(o);

                order.Value.timer--;

                if (order.Value.timer <= 0 || order.Value.complete)
                {

                    Mod.instance.save.orders.Remove(order.Key);

                    continue;

                }

            }

            foreach(KeyValuePair<int, ExportOrder> order in Mod.instance.save.orders)
            {

                currentGoods[order.Value.good] = 1;

                if (!currentGuilds.ContainsKey(order.Value.guild))
                {

                    currentGuilds[order.Value.guild] = 1;

                    continue;

                }

                currentGuilds[order.Value.guild] += 1;

            }

            Dictionary<exports,int> viableGoods = new();

            foreach (KeyValuePair<exports, ExportGood> viable in goods)
            {

                if (!RelicData.HasRelic(viable.Value.license))
                {

                    continue;

                }

                if (currentGoods.ContainsKey(viable.Key))
                {

                    continue;

                }

                viableGoods[viable.Key] = 1;

            }

            foreach (KeyValuePair<exports, ExportGuild> contracts in guilds)
            {

                ExportGuild guild = contracts.Value;

                if (!RelicData.HasRelic(guild.license))
                {

                    continue;

                }

                int level = GuildLevel(contracts.Key);

                int limit = 2;

                if (currentGuilds.ContainsKey(contracts.Key))
                {

                    if(currentGuilds[contracts.Key] >= limit)
                    {

                        continue;

                    }

                }

                int randomGood = Mod.instance.randomIndex.Next(viableGoods.Count);

                exports good = viableGoods.ElementAt(randomGood).Key;

                int factor = 3;

                switch (level)
                {

                    case 3:

                        factor = 4;

                        break;
                    case 5:

                        factor = 5;

                        break;

                }

                int orderLevel = Mod.instance.randomIndex.Next(factor);

                int orderRequirement = 10;

                int orderFloor = 8;

                int orderTimer = 2;

                switch (good)
                {

                    case exports.potions:
                    case exports.whiskey:

                        switch (orderLevel)
                        {

                            case 1:

                                orderRequirement = 25;
                                
                                orderFloor = 20;

                                break;

                            case 2:

                                orderTimer = 3;

                                orderRequirement = 100;

                                orderFloor = 80;

                                break;

                            case 3:

                                orderTimer = 3;

                                orderRequirement = 250;

                                orderFloor = 200;

                                break;

                            case 4:

                                orderTimer = 4;

                                orderRequirement = 500;

                                orderFloor = 400;

                                break;

                        }
                        break;

                    case exports.powders:
                    case exports.brandy:

                        switch (orderLevel)
                        {

                            case 1:

                                orderRequirement = 15;

                                orderFloor = 10;

                                break;

                            case 2:

                                orderTimer = 3;

                                orderRequirement = 60;

                                orderFloor = 45;

                                break;

                            case 3:

                                orderTimer = 3;

                                orderRequirement = 120;

                                orderFloor = 100;

                                break;

                            case 4:

                                orderTimer = 4;

                                orderRequirement = 300;

                                orderFloor = 200;

                                break;


                        }

                        break;

                    case exports.weapons:

                        orderRequirement = 5;

                        orderFloor = 4;

                        switch (orderLevel)
                        {

                            case 1:

                                orderRequirement = 5;

                                orderFloor = 3;

                                break;

                            case 2:

                                orderTimer = 3;

                                orderRequirement = 10;

                                orderFloor = 6;

                                break;

                            case 3:

                                orderTimer = 3;

                                orderRequirement = 25;

                                orderFloor = 15;

                                break;

                            case 4:

                                orderTimer = 4;

                                orderRequirement = 50;

                                orderFloor = 30;

                                break;

                        }

                        break;

                    default:

                        orderRequirement = 5;

                        orderFloor = 4;

                        switch (orderLevel)
                        {

                            case 1:

                                orderRequirement = 10;

                                orderFloor = 8;

                                break;

                            case 2:

                                orderTimer = 3;

                                orderRequirement = 25;

                                orderFloor = 20;

                                break;

                            case 3:

                                orderTimer = 3;

                                orderRequirement = 50;

                                orderFloor = 40;

                                break;

                            case 4:

                                orderTimer = 4;

                                orderRequirement = 100;

                                orderFloor = 80;

                                break;

                        }

                        break;


                }

                ExportOrder order = new();

                order.good = good;

                order.timer = orderTimer;

                order.level = orderLevel;

                order.requirement = orderRequirement;

                order.floor = orderFloor;

                order.guild = guild.guild;

                //order.display = Mod.instance.randomIndex.Next(2);

                int k = 0;

                while (Mod.instance.save.orders.ContainsKey(k))
                {

                    k++;

                }

                Mod.instance.save.orders[k] = order;

            }

        }

        public bool CompleteOrder(int orderKey)
        {

            ExportOrder order = Mod.instance.save.orders[orderKey];

            if (order.complete)
            {

                return false;

            }

            if (!Mod.instance.save.exports.ContainsKey(order.good))
            {

                return false;

            }

            if (Mod.instance.save.exports[order.good] < order.floor)
            {

                return false;

            }

            int provide = Math.Min(Mod.instance.save.exports[order.good], order.requirement);

            int sale = OrderPrice(order.good, order.requirement, provide, order.timer);

            Game1.player.Money += sale;

            Mod.instance.save.exports[order.good] -= provide;

            if (Mod.instance.save.exports[order.good] <= 0)
            {

                Mod.instance.save.exports.Remove(order.good);

            }

            Mod.instance.exportHandle.AddExport(order.guild, order.level * 5);

            GuildLevel(order.guild);

            Mod.instance.save.orders[orderKey].complete = true;

            Mod.instance.save.orders[orderKey].sale = sale;

            return true;

        }

        public int QuickSell(exports export)
        {

            if (!Context.IsMainPlayer)
            {

                return 0;

            }

            if (stock.ContainsKey(export))
            {

                return stock[export][2];

            }

            if (!Mod.instance.save.exports.ContainsKey(export))
            {

                return 0;

            }

            ExportGood good = Mod.instance.exportHandle.goods[export];

            int currentPrice = GoodsPrice(export, day);

            int sellPrice = 0;

            if (GuildLevel(good.guild) >= 4)
            {

                sellPrice = (int)(currentPrice * 0.6f);

            }

            return sellPrice;

        }

        public bool SellNow(exports export, int amount)
        {

            ExportGood good = Mod.instance.exportHandle.goods[export];

            int price = QuickSell(export);

            if(price <= 0)
            {

                return false;

            }

            int provide = Math.Min(Mod.instance.save.exports[export], amount);

            Game1.player.Money += provide * price;

            Mod.instance.save.exports[export] -= provide;

            if (Mod.instance.save.exports[export] <= 0)
            {

                Mod.instance.save.exports.Remove(export);

            }

            return true;

        }

        public static int GuildLevel(exports guild)
        {

            if (!Mod.instance.save.exports.ContainsKey(guild))
            {

                return 0;

            }

            int experience = Mod.instance.save.exports[guild];

            int level = 1;

            if (experience >= 100)
            {

                level = 5;

            }
            else if (experience >= 60)
            {

                level = 4;

            }
            else if (experience >= 25)
            {

                level = 3;

            }
            else if (experience >= 10)
            {

                level = 2;

            }

            if(level >= 3)
            {

                if (Mod.instance.questHandle.IsGiven(QuestHandle.heirsTwo))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.heirsTwo, 1);

                }

            }

            return level;

        }

        public static int GuildNext(exports guild)
        {

            if (!Mod.instance.save.exports.ContainsKey(guild))
            {

                return 10;

            }

            int experience = Mod.instance.save.exports[guild];

            if (experience >= 100)
            {

                return -1;

            }
            else if (experience >= 60)
            {

                return 100;

            }
            else if (experience >= 25)
            {

                return 60;

            }
            else if (experience >= 10)
            {

                return 25;

            }

            return 10;

        }

        public static int GuildRelationship(exports guild)
        {

            if (!Mod.instance.save.exports.ContainsKey(guild))
            {

                return 0;

            }

            return Mod.instance.save.exports[guild];

        }

        public void CalculateOutput(bool confirm = false)
        {

            estimates.Clear();

            calculations.Clear();

            string stringToAdd = string.Empty;

            // WHISKEY ===============================================================

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.1.1");

            calculations.Add(stringToAdd);

            CharacterHandle.characters sb = CharacterHandle.characters.spring_bench;

            int whiskeyOutput = 0;

            int kilns = 0;

            if (Mod.instance.save.exports.ContainsKey(exports.kiln))
            {

                kilns = Mod.instance.save.exports[exports.kiln];

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.1").Tokens(new { value = kilns, });

            calculations.Add(stringToAdd);

            float mash = 0f;

            if (Mod.instance.save.exports.ContainsKey(exports.mashtun))
            {

                mash = Mod.instance.save.exports[exports.mashtun];

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.2").Tokens(new { value = mash, });

            calculations.Add(stringToAdd);

            int whiskeyLimit = (int)(kilns * 10f * (mash * 0.2f));

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.3").Tokens(new { value = whiskeyLimit, });

            calculations.Add(stringToAdd);

            CharacterHandle.RetrieveInventory(sb);

            List<string> grains = new()
            {
                "(O)262", // wheat
                "(O)270", // corn
                "(O)271", // rice
                "(O)304", // hops
                "(O)188", // bean
                "(O)192", // potato
                "(O)433" // coffee
            };

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.1.2");

            calculations.Add(stringToAdd);

            for (int i = Mod.instance.chests[sb].Items.Count - 1; i >= 0; i--)
            {

                Item getItem = Mod.instance.chests[sb].Items.ElementAt(i);

                if (!grains.Contains(getItem.QualifiedItemId))
                {

                    stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.4").Tokens(new { value = getItem.DisplayName, });

                    calculations.Add(stringToAdd);

                    continue;

                }

                string qual = StringData.normal;

                switch (getItem.Quality)
                {
                    case 1:
                        qual = StringData.silver; break;
                    case 2:
                        qual = StringData.gold; break;
                    case 4:
                        qual = StringData.iridium; break;
                }

                int consumed = Math.Min(whiskeyLimit, getItem.Stack);

                int grainYield = consumed;

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.5").Tokens(new { value = consumed, valueTwo = qual, valueThree = getItem.DisplayName});

                calculations.Add(stringToAdd);

                switch (getItem.QualifiedItemId)
                {
  
                    case "(O)433":

                        stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.6").Tokens(new { value = getItem.DisplayName });

                        calculations.Add(stringToAdd);

                        grainYield = grainYield / 2;

                        break;


                }

                whiskeyLimit -= consumed;

                int grainQuality = 25 * getItem.Quality;

                int grainBonus = grainYield * grainQuality / 100;

                int mashOutput = grainYield + grainBonus;

                whiskeyOutput += mashOutput;

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.7").Tokens(new { value = grainQuality });

                calculations.Add(stringToAdd);

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.8").Tokens(new { value = consumed, valueTwo = getItem.DisplayName });

                calculations.Add(stringToAdd);

                if (confirm)
                {

                    getItem.Stack -= consumed;

                    if(getItem.Stack <= 0)
                    {

                        Mod.instance.chests[sb].Items.RemoveAt(i);

                    }

                }

                if(whiskeyLimit <= 0)
                {

                    break;

                }

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.9").Tokens(new { value = whiskeyOutput });

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.10");

            calculations.Add(stringToAdd);

            int tanks = 0;

            if (Mod.instance.save.exports.ContainsKey(exports.fermentation))
            {

                tanks = Mod.instance.save.exports[exports.fermentation];

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.11").Tokens(new { value = tanks });
            
            calculations.Add(stringToAdd);

            int stills = 0;

            if (Mod.instance.save.exports.ContainsKey(exports.distillery))
            {

                stills = Mod.instance.save.exports[exports.distillery];

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.12").Tokens(new { value = stills });

            calculations.Add(stringToAdd);

            int tankBonus = tanks * 5;

            int stillsBonus = stills * 10;

            int whiskeyProduct = whiskeyOutput;

            int extraWhiskeyProduct = 0;

            if (confirm)
            {

                int bonusFactor = Mod.instance.randomIndex.Next(tankBonus, tankBonus + stillsBonus);

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.13").Tokens(new { value = bonusFactor });

                calculations.Add(stringToAdd);

                if (bonusFactor > 0)
                {

                    extraWhiskeyProduct = (int)(whiskeyOutput * (bonusFactor / 100f));

                }

            }
            else
            {

                int bonusAverage = tankBonus + stillsBonus / 2;

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.14").Tokens(new { value = bonusAverage });

                calculations.Add(stringToAdd);

                if (bonusAverage > 0)
                {

                    extraWhiskeyProduct = (int)(whiskeyOutput * (bonusAverage / 100f));

                }

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.15").Tokens(new { value = extraWhiskeyProduct });

            calculations.Add(stringToAdd);

            whiskeyProduct += extraWhiskeyProduct;

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.16").Tokens(new { value = whiskeyProduct });

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.17");

            calculations.Add(stringToAdd);

            int barrels = 0;

            if (Mod.instance.save.exports.ContainsKey(exports.barrel))
            {

                barrels = Mod.instance.save.exports[exports.barrel];

            }

            int qualityFactor = 0;

            if (barrels > 0)
            {

                qualityFactor = barrels * 2;

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.18").Tokens(new { value = barrels });

            calculations.Add(stringToAdd);

            int whiskeyTotal = whiskeyProduct;

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.19").Tokens(new { value = qualityFactor });

            calculations.Add(stringToAdd);

            int whiskeyQuality = (int)(qualityFactor / 100f * whiskeyProduct);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.20").Tokens(new { value = whiskeyQuality });

            calculations.Add(stringToAdd);

            whiskeyTotal += whiskeyQuality;

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.21");

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.22").Tokens(new { value = whiskeyTotal });

            calculations.Add(stringToAdd);

            if (confirm)
            {

                Mod.instance.exportHandle.AddExport(exports.whiskey, whiskeyTotal);

            }

            estimates[exports.whiskey] = whiskeyTotal;

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.23");

            calculations.Add(stringToAdd);


            // BRANDY ===============================================================

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.24");

            calculations.Add(stringToAdd);

            CharacterHandle.characters sv = CharacterHandle.characters.spring_vintner;

            int brandyOutput = 0;

            int crushers = 0;

            if (Mod.instance.save.exports.ContainsKey(exports.crushers))
            {

                crushers = Mod.instance.save.exports[exports.crushers];

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.25").Tokens(new { value = crushers });

            calculations.Add(stringToAdd);

            float press = 0f;

            if (Mod.instance.save.exports.ContainsKey(exports.press))
            {

                press = Mod.instance.save.exports[exports.press];

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.26").Tokens(new { value = press });

            calculations.Add(stringToAdd);

            int brandyLimit = (int)(crushers * 5f * (press * 0.2f));

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.27").Tokens(new { value = brandyLimit });

            calculations.Add(stringToAdd);

            CharacterHandle.RetrieveInventory(sv);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.28");

            calculations.Add(stringToAdd);

            for (int i = Mod.instance.chests[sv].Items.Count - 1; i >= 0; i--)
            {

                Item getItem = Mod.instance.chests[sv].Items.ElementAt(i);


                if (getItem.Category != -79)
                {
                
                    stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.29").Tokens(new { value = getItem.DisplayName, });

                    calculations.Add(stringToAdd);

                    continue;

                }

                string qual = StringData.normal;

                switch (getItem.Quality)
                {
                    case 1:
                        qual = StringData.silver; break;
                    case 2:
                        qual = StringData.gold; break;
                    case 4:
                        qual = StringData.iridium; break;
                }

                int consumed = Math.Min(brandyLimit, getItem.Stack);

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.5").Tokens(new { value = consumed, valueTwo = qual, valueThree = getItem.DisplayName });

                calculations.Add(stringToAdd);

                brandyLimit -= consumed;

                int fruitYield = consumed;

                switch (getItem.QualifiedItemId)
                {
                    case "(O)400": // strawberry
                    case "PowderMelon": // powdermelon
                    case "(O)832": // pineapple

                        stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.30").Tokens(new { value = getItem.DisplayName });

                        calculations.Add(stringToAdd);

                        fruitYield = consumed * 2;

                        break;

                    case "(O)454": // ancientFruit
                    case "(O)268": // starfruit

                        stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.31").Tokens(new { value = getItem.DisplayName });

                        calculations.Add(stringToAdd);

                        fruitYield = consumed * 3;

                        break;

                }

                int fruitQuality = 25 * getItem.Quality;

                int fruitBonus = fruitYield * fruitQuality / 100;

                int juiceOutput = fruitYield + fruitBonus;

                brandyOutput += juiceOutput;

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.32").Tokens(new { value = fruitQuality });

                calculations.Add(stringToAdd);

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.33").Tokens(new { value = juiceOutput, valueTwo = getItem.DisplayName });

                calculations.Add(stringToAdd);

                if (confirm)
                {

                    getItem.Stack -= consumed;

                    if (getItem.Stack <= 0)
                    {

                        Mod.instance.chests[sv].Items.RemoveAt(i);

                    }

                }

                if (brandyLimit <= 0)
                {

                    break;

                }

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.34").Tokens(new { value = brandyOutput });

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.10");

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.11").Tokens(new { value = tanks });

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.12").Tokens(new { value = stills });

            calculations.Add(stringToAdd);


            int brandyProduct = brandyOutput;

            int extraProduct = 0;

            if (confirm)
            {

                int bonusFactor = Mod.instance.randomIndex.Next(tankBonus, tankBonus + stillsBonus);

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.13").Tokens(new { value = bonusFactor });

                calculations.Add(stringToAdd);

                if (bonusFactor > 0)
                {

                    extraProduct = (int)(brandyOutput * (bonusFactor / 100f));

                }

            }
            else
            {

                int bonusAverage = tankBonus + stillsBonus / 2;

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.14").Tokens(new { value = bonusAverage });

                calculations.Add(stringToAdd);

                if (bonusAverage > 0)
                {

                    extraProduct = (int)(brandyOutput * (bonusAverage / 100f));

                }

            }


            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.15").Tokens(new { value = extraProduct });

            calculations.Add(stringToAdd);

            whiskeyProduct += extraWhiskeyProduct;

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.16").Tokens(new { value = brandyProduct });

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.17");

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.18").Tokens(new { value = barrels });

            calculations.Add(stringToAdd);

            int brandyTotal = brandyProduct;

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.19").Tokens(new { value = qualityFactor });

            calculations.Add(stringToAdd);

            int brandyQuality = (int)(qualityFactor / 100f * brandyProduct);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.20").Tokens(new { value = brandyQuality });

            calculations.Add(stringToAdd);

            brandyTotal += brandyQuality;

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.21");

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.35").Tokens(new { value = brandyTotal });

            calculations.Add(stringToAdd);

            if (confirm)
            {

                Mod.instance.exportHandle.AddExport(exports.brandy, brandyTotal);

            }

            estimates[exports.brandy] = brandyTotal;

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.23");

            calculations.Add(stringToAdd);


            // WEAPONS / SUPPLIES ===============================================================

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.36");

            calculations.Add(stringToAdd);

            CharacterHandle.characters sp = CharacterHandle.characters.spring_packer;

            int weaponOutput = 0;

            int supplyOutput = 0;

            int packers = 0;

            if (Mod.instance.save.exports.ContainsKey(exports.packer))
            {

                packers = Mod.instance.save.exports[exports.packer];

            }

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.37").Tokens(new { value = packers });

            calculations.Add(stringToAdd);

            int packingLimit = packers * 3;

            int packingUse = packingLimit;

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.40");

            calculations.Add(stringToAdd);

            CharacterHandle.RetrieveInventory(sp);

            Dictionary<string,int> supplies = new()
            {
                ["(O)93"] = 10, // torch
                ["(O)286"] = 30, // cherry bomb
                ["(O)287"] = 40, // bomb
                ["(O)288"] = 60, // mega bomb
                ["(O)772"] = 40, // garlic
                ["(O)773"] = 40, // elixir
                ["(O)349"] = 60, // energy tonic
                ["(O)243"] = 60, // miner treat
                ["(O)261"] = 30, // desert
                ["(O)688"] = 30, // farm
                ["(O)689"] = 30, // mountain
                ["(O)690"] = 30, // beach
                ["(O)886"] = 60, // island
                ["(O)184"] = 50, // milk
                ["(O)186"] = 100, // large milk
                ["(O)436"] = 100, // goat milk
                ["(O)438"] = 180, // large goat milk
                ["(O)180"] = 20, // egg
                ["(O)182"] = 40, // large egg
                ["(O)176"] = 20, // brown egg
                ["(O)174"] = 40, // large brown egg
                ["(O)442"] = 70, // duck egg
                ["(O)424"] = 120, // cheese
                ["(O)426"] = 240, // goat cheese
                ["(O)306"] = 80, // mayo
                ["(O)307"] = 140, // duck mayo
                ["(O)440"] = 120, // wool
                ["(O)428"] = 180, // cloth
            };

            Dictionary<string, int> supplyQuality = new()
            {
                ["(O)184"] = 1, // milk
                ["(O)186"] = 1, // large milk
                ["(O)436"] = 1, // goat milk
                ["(O)438"] = 1, // large goat milk
                ["(O)180"] = 1, // egg
                ["(O)182"] = 1, // large egg
                ["(O)176"] = 1, // brown egg
                ["(O)174"] = 1, // large brown egg
                ["(O)442"] = 1, // duck egg
                ["(O)424"] = 1, // cheese
                ["(O)426"] = 1, // goat cheese
                ["(O)440"] = 1, // wool

            };

            int supplyAvailable = 0;

            int weaponAvailable = 0;

            Dictionary<string, int> packingTotals = new();

            Dictionary<string, int> packingYields = new();

            for (int i = Mod.instance.chests[sp].Items.Count - 1; i >= 0; i--)
            {

                Item getItem = Mod.instance.chests[sp].Items.ElementAt(i);

                if (getItem is MeleeWeapon weapon)
                {

                    if (weapon.isScythe())
                    {

                        stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.41").Tokens(new { value = getItem.DisplayName });

                        calculations.Add(stringToAdd);

                        continue;


                    }

                    int weaponYield = weapon.getItemLevel() * 10;

                    weaponAvailable += weaponYield;

                    packingUse--;

                    if (!packingTotals.ContainsKey(Mod.instance.chests[sp].Items.ElementAt(i).DisplayName))
                    {

                        packingTotals[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] = 1;

                        packingYields[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] = weaponYield;

                    }
                    else
                    {

                        packingTotals[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] += 1;

                    }

                    if (confirm)
                    {

                        Mod.instance.chests[sp].Items.RemoveAt(i);

                    }

                }
                else if(getItem is Boots boot)
                {

                    int weaponYield = (boot.defenseBonus.Value + boot.immunityBonus.Value) * 20; 

                    weaponAvailable += weaponYield;

                    packingUse--;

                    if (!packingTotals.ContainsKey(Mod.instance.chests[sp].Items.ElementAt(i).DisplayName))
                    {

                        packingTotals[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] = 1;

                        packingYields[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] = weaponYield;

                    }
                    else
                    {

                        packingTotals[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] += 1;

                    }

                    if (confirm)
                    {

                        Mod.instance.chests[sp].Items.RemoveAt(i);

                    }

                }
                else if (getItem.Category == StardewValley.Object.ringCategory || getItem is Ring)
                {

                    int weaponYield = 50;

                    weaponAvailable += weaponYield;

                    packingUse--;

                    if (!packingTotals.ContainsKey(Mod.instance.chests[sp].Items.ElementAt(i).DisplayName))
                    {

                        packingTotals[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] = 1;

                        packingYields[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] = weaponYield;

                    }
                    else
                    {

                        packingTotals[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] += 1;

                    }

                    if (confirm)
                    {

                        Mod.instance.chests[sp].Items.RemoveAt(i);

                    }

                }
                else
                {

                    if (!supplies.ContainsKey(getItem.QualifiedItemId))
                    {

                        stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.42").Tokens(new { value = getItem.DisplayName });

                        calculations.Add(stringToAdd);

                        continue;

                    }

                    int supplyUse = Math.Min(packingUse, getItem.Stack);

                    int supplyYield = supplies[getItem.QualifiedItemId];

                    if (supplyQuality.ContainsKey(getItem.QualifiedItemId))
                    {

                        int supplyBonus = (int)(supplyYield * 0.25f * getItem.Quality);

                        supplyYield += supplyBonus;

                    }

                    packingUse -= supplyUse;

                    supplyAvailable += supplyUse * supplyYield;

                    if (!packingTotals.ContainsKey(Mod.instance.chests[sp].Items.ElementAt(i).DisplayName))
                    {

                        packingTotals[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] = supplyUse;

                        packingYields[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] = supplyYield;

                    }
                    else
                    {

                        packingTotals[Mod.instance.chests[sp].Items.ElementAt(i).DisplayName] += supplyUse;

                    }

                    if (confirm)
                    {

                        Mod.instance.chests[sp].Items.RemoveAt(i);

                    }

                }

                if (packingUse == 0)
                {

                    break;

                }

            }

            if(weaponAvailable > 0)
            {

                weaponOutput = (int)Math.Ceiling((decimal)weaponAvailable / 100);

            }

            if (supplyAvailable > 0)
            {

                supplyOutput = (int)Math.Ceiling((decimal)supplyAvailable / 100);

            }

            foreach (KeyValuePair<string, int> packedTotal in packingTotals)
            {

                stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.43").Tokens(new { number = packedTotal.Value.ToString(), itemname = packedTotal.Key, yield = packingYields[packedTotal.Key] });

                calculations.Add(stringToAdd);

            }

            calculations.Add("");

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.44");

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.45").Tokens(new { value = weaponOutput });

            calculations.Add(stringToAdd);

            stringToAdd = Mod.instance.Helper.Translation.Get("ExportData.386.46").Tokens(new { value = supplyOutput });

            calculations.Add(stringToAdd);

            estimates[exports.weapons] = weaponOutput;

            estimates[exports.supplies] = supplyOutput;

            if (confirm)
            {

                Mod.instance.exportHandle.AddExport(exports.weapons, weaponOutput);

                Mod.instance.exportHandle.AddExport(exports.supplies, supplyOutput);

                recents = new(calculations);

                products = new(estimates);

            }

        }

    }

    public class ExportGood
    {

        public string name;

        public string description;

        public string technical;

        public ExportHandle.exports good;

        public IconData.workshops display;

        public IconData.relics license;

        public ExportHandle.exports guild;

        public int price;

        public int peak;

        public List<string> details = new();

        public int sell;

        public ExportGood()
        {

        }

    }

    public class ExportMachine
    {

        public string name;

        public string description;

        public string technical;

        public ExportHandle.exports machine;

        public IconData.workshops display;

        public CharacterHandle.characters pal;

        public int labour;

        public Dictionary<string, int> resources = new();

        public ExportMachine()
        {

        }

    }

    public class ExportGuild
    {

        public ExportHandle.exports guild;

        public string name;

        public string intro;

        public string description;

        public Dictionary<int,string> benefits = new();

        public IconData.relics license;

        public Dictionary<int,string> orderTitles = new();

        public Dictionary<int,string> orderDescriptions = new();

        public string orderFulfilled;

        public ExportGuild()
        {

        }

    }

    public class ExportOrder
    {

        public ExportHandle.exports good;

        public int requirement;

        public int floor;

        public int timer;

        public int level;

        public bool complete;

        public int sale;

        //public int display;

        public ExportHandle.exports guild = ExportHandle.exports.church;

        public ExportOrder()
        {

        }

    }

}
