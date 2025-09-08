using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Enchantments;
using StardewValley.GameData.Machines;
using StardewValley.GameData.Shops;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Handle
{

    public class ExportResource
    {

        public enum resources
        {
            malt,
            must,
            tribute,
            nectar,

            labour,
            heavy,
            special,
            materials,

        }

        public resources resource;

        public string name;

        public string description;

        public List<string> details;

        public ExportResource()
        {

        }

        public static IconData.displays ResourceDisplay(resources resource)
        {

            switch (resource)
            {
                default:
                case resources.malt:
                    return IconData.displays.malt;

                case resources.must:
                    return IconData.displays.must;

                case resources.tribute:
                    return IconData.displays.tribute;

                case resources.nectar:
                    return IconData.displays.nectar;

                case resources.labour:
                    return IconData.displays.labour;

                case resources.heavy:
                    return IconData.displays.heavy;

                case resources.special:
                    return IconData.displays.special;

                case resources.materials:
                    return IconData.displays.materials;

            }

        }

    }

    public class ExportGood
    {

        public enum goods
        {

            potions,
            powders,
            trophies,
            omens,

            whiskey,
            brandy,
            aquavitae,
            ambrosia,

        }

        public goods good;

        public string name;

        public string description;

        public int price;

        public int peak;

        public List<string> details = new();

        public int sell;

        public ExportGuild.guilds guild;

        public ExportGood()
        {

        }

        public static Microsoft.Xna.Framework.Rectangle GoodRectangles(goods goodId)
        {

            int slot = (int)goodId;

            return new(slot % 4 * 32, slot == 0 ? 0 : slot / 4 * 32, 32, 32);

        }

    }

    public class ExportMachine
    {
        public enum machines
        {

            crushers,
            press,
            kiln,
            mashtun,

            fermentation,
            distillery,
            barrel,
            packer,

        }

        public machines machine;

        public string name;

        public string description;

        public string technical;

        public List<int> graduation = new();

        public ExportResource.resources labour = ExportResource.resources.labour;

        public List<int> hireage = new();

        public List<int> materials = new();

        public ExportMachine()
        {

        }

        public static Microsoft.Xna.Framework.Rectangle MachineRectangles(machines workshop)
        {

            int slot = (int)workshop;

            return new(slot % 4 * 32, 64 + (slot == 0 ? 0 : slot / 4 * 32), 32, 32);

        }

    }

    public class MachineRecord
    {

        public ExportMachine.machines machine;

        public int level;

        public int status;

    }

    public class ExportGuild
    {

        public enum guilds
        {
            church,
            dwarf,
            associate,
            smuggler,
        }

        public guilds guild;

        public string name;

        public string intro;

        public string description;

        public Dictionary<int, string> benefits = new();

        public IconData.relics license;

        public Dictionary<int, string> orderTitles = new();

        public Dictionary<int, string> orderDescriptions = new();

        public string orderFulfilled;

        public ExportGuild()
        {

        }

    }

    public class GuildRecord
    {

        public ExportGuild.guilds guild;

        public int experience;

        public int status;

    }

    public class ExportOrder
    {

        public ExportGood.goods good;

        public ExportGuild.guilds guild = ExportGuild.guilds.church;

        public int requirement;

        public int floor;

        public int timer;

        public int level;

        public bool complete;

        public int sale;

        public ExportOrder()
        {

        }

    }

    public class ExportHandle
    {

        public Dictionary<ExportResource.resources, ExportResource> resources = new();

        public Dictionary<ExportGood.goods, ExportGood> goods = new();

        public Dictionary<ExportMachine.machines, ExportMachine> machines = new();

        public Dictionary<ExportGuild.guilds, ExportGuild> guilds = new();

        public int day = 0;

        public List<string> recentProduction = new();

        public List<string> estimatedProduction = new();

        public ExportHandle()
        {

        }

        public void LoadExports()
        {

            resources = ResourceData.LoadResources();

            goods = GoodData.LoadGoods();

            machines = Data.WorkshopData.LoadMachines();

            guilds = GuildData.LoadGuilds();

        }

        public void Produce()
        {

            recentProduction.Clear();

            if (!RelicHandle.HasRelic(IconData.relics.crest_dwarf))
            {

                return;

            }

            List<string> processedResources = ResourceProcessing(true);

            List<string> producedGoods = GoodsProduction(true);

            recentProduction.AddRange(processedResources);

            recentProduction.AddRange(producedGoods);

        }

        public void Estimate()
        {

            estimatedProduction.Clear();

            if (!RelicHandle.HasRelic(IconData.relics.crest_dwarf))
            {

                return;

            }

            List<string> processedResources = ResourceProcessing();

            List<string> producedGoods = GoodsProduction();

            estimatedProduction.AddRange(processedResources);

            estimatedProduction.AddRange(producedGoods);

        }

        public Dictionary<int, ContentComponent> JournalOrders()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            foreach (KeyValuePair<int, ExportOrder> orderEntry in Mod.instance.save.orders)
            {

                ExportOrder order = orderEntry.Value;

                ContentComponent content = new(ContentComponent.contentTypes.order, orderEntry.Key.ToString());

                content.textureSources[0] = ExportGood.GoodRectangles(goods[order.good].good);

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
                else if (!Mod.instance.save.goods.ContainsKey(order.good))
                {

                    content.text[1] = guild.orderDescriptions[order.level];

                    content.text[2] = 0 + StringData.slash + order.requirement;

                    content.text[3] = StringData.Get(StringData.str.shortfall);

                    content.textColours[3] = Color.DarkRed;


                }
                else if (Mod.instance.save.goods[order.good] < order.floor)
                {

                    content.text[1] = guild.orderDescriptions[order.level];

                    content.text[2] = Mod.instance.save.goods[order.good] + StringData.slash + order.requirement;

                    content.text[3] = StringData.Get(StringData.str.shortfall);

                    content.textColours[3] = Color.DarkRed;

                }
                else 
                { 

                    content.text[1] = guild.orderDescriptions[order.level];

                    int provided = Math.Min(Mod.instance.save.goods[order.good], order.requirement);

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

        public Dictionary<int, ContentComponent> JournalByType(DruidJournal.journalTypes type)
        {

            switch (type)
            {
                default:
                case DruidJournal.journalTypes.goods:

                    return JournalGoods();

                case DruidJournal.journalTypes.distillery:

                    return JournalDistillery();

            }

        }

        public Dictionary<int, ContentComponent> JournalGoods()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            int season = (int)Game1.season;

            int grid = 0;

            day = season * 28 + Game1.dayOfMonth;

            foreach (KeyValuePair<ExportGood.goods, ExportGood> good in goods)
            {

                int amount = 0;

                if (Mod.instance.save.goods.ContainsKey(good.Value.good))
                {

                    amount = Mod.instance.save.goods[good.Key];

                }

                // ------------------------------------ good

                ContentComponent content = new(ContentComponent.contentTypes.custom, good.Key.ToString());

                content.grid = grid;

                content.serial = 1;

                content.text[0] = amount.ToString();

                content.textureSources[0] = ExportGood.GoodRectangles(good.Value.good);

                journal[start++] = content;

                // ------------------------------------ quicksell

                ContentComponent button = new(ContentComponent.contentTypes.toggle, good.Key.ToString());

                button.grid = grid;

                button.serial = 2;

                button.textureSources[0] = IconData.DisplayRectangle(IconData.displays.sell);

                int sellPrice = QuickSell(good.Key);

                button.text[0] = sellPrice.ToString();

                if (sellPrice == 0) 
                {

                    button.text[1] = StringData.Get(StringData.str.nosell);

                }

                journal[start++] = button;

                grid++;

            }

            List<ExportResource.resources> useResources = new()
            {
                ExportResource.resources.malt,
                ExportResource.resources.must,
                ExportResource.resources.tribute,
                ExportResource.resources.nectar,

            };

            foreach (ExportResource.resources useResource in useResources)
            {

                int amount = 0;

                if (Mod.instance.save.resources.ContainsKey(useResource))
                {

                    amount = Mod.instance.save.resources[useResource];

                }

                // ------------------------------------ resource

                ContentComponent content = new(ContentComponent.contentTypes.custom, useResource.ToString());

                content.grid = grid;

                content.serial = 3;

                content.text[0] = amount.ToString();

                content.textureSources[0] = IconData.DisplayRectangle(ExportResource.ResourceDisplay(useResource));

                journal[start++] = content;

                // ------------------------------------ quicksell for row formatting only

                ContentComponent button = new(ContentComponent.contentTypes.toggle, useResource.ToString());

                button.grid = grid;

                button.serial = 2;

                button.active = false;

                journal[start++] = button;

                grid++;

            }

            return journal;

        }

        public Dictionary<int, ContentComponent> JournalDistillery()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            int grid = 0;

            foreach (KeyValuePair<ExportMachine.machines, ExportMachine> machine in machines)
            {

                int level = 1;

                if (Mod.instance.save.machines.ContainsKey(machine.Key))
                {

                    level = Mod.instance.save.machines[machine.Key].level;

                }

                // ------------------------------------ machine

                ContentComponent content = new(ContentComponent.contentTypes.custom, machine.Key.ToString());

                content.grid = grid;

                content.serial = 1;

                content.text[0] = level.ToString();

                content.textureSources[0] = ExportMachine.MachineRectangles(machine.Key);

                journal[start++] = content;

                // ------------------------------------ craft

                ContentComponent button = new(ContentComponent.contentTypes.toggle, machine.Key.ToString());

                button.grid = grid;

                button.serial = 2;

                button.textureSources[0] = IconData.DisplayRectangle(IconData.displays.upgrade);

                journal[start++] = button;

                grid++;

            }

            List<ExportResource.resources> useResources = new()
            {
                ExportResource.resources.labour,
                ExportResource.resources.heavy,
                ExportResource.resources.special,
                ExportResource.resources.materials,

            };

            foreach (ExportResource.resources useResource in useResources)
            {

                int amount = 0;

                if (Mod.instance.save.resources.ContainsKey(useResource))
                {

                    amount = Mod.instance.save.resources[useResource];

                }

                // ------------------------------------ resource

                ContentComponent content = new(ContentComponent.contentTypes.custom, useResource.ToString());

                content.grid = grid;

                content.serial = 3;

                content.text[0] = amount.ToString();

                content.textureSources[0] = IconData.DisplayRectangle(ExportResource.ResourceDisplay(useResource));

                journal[start++] = content;

                // ------------------------------------ craft for row formatting only

                ContentComponent button = new(ContentComponent.contentTypes.toggle, useResource.ToString());

                button.grid = grid;

                button.serial = 2;

                button.active = false;

                journal[start++] = button;

                grid++;

            }

            return journal;

        }

        public Dictionary<int, ContentComponent> JournalGuilds()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            foreach(KeyValuePair<ExportGuild.guilds, ExportGuild> guildData in guilds)
            {

                if (!RelicHandle.HasRelic(guildData.Value.license))
                {

                    continue;

                }

                Journal.ContentComponent content = new(ContentComponent.contentTypes.list, guildData.Key.ToString());

                content.text[0] = guildData.Value.name;

                content.text[1] = StringData.Get(StringData.str.level, new { level = GuildLevel(guildData.Key) });

                content.textures[0] = Mod.instance.iconData.relicsTexture;

                content.textureSources[0] = IconData.RelicRectangles(guildData.Value.license);

                journal[start++] = content;

            }

            return journal;

        }

        public int GoodsPrice(ExportGood.goods export)
        {

            ExportGood good = goods[export];

            float diff;

            if(day == -1)
            {

                int season = (int)Game1.season;

                day = season * 28 + Game1.dayOfMonth;

            }

            if (day > good.peak)
            {

                diff = Math.Min(day- good.peak, good.peak + 112 -day);

            }
            else
            {

                diff = Math.Min(good.peak - day, day+ 112 - good.peak);


            }

            float diff2 = diff / 112f;

            if (MasteryHandle.HasMastery(MasteryNode.nodes.potion_bolster))
            {

                diff2 *= 0.75f;

            }

            float diff3 = 1.25f - diff2;

            int adjustPrice = (int)(good.price * diff3);

            return adjustPrice;

        }
        
        public void AddResource(ExportResource.resources export, int amount = 1)
        {

            if (amount == 0)
            {

                return;

            }

            if (!Mod.instance.save.resources.ContainsKey(export))
            {

                Mod.instance.save.resources[export] = amount;

                return;

            }

            Mod.instance.save.resources[export] += amount;

            if (Mod.instance.save.resources[export] > 99999)
            {

                Mod.instance.save.resources[export] = 99999;

            }

        }

        public void AddGood(ExportGood.goods export, int amount = 1)
        {

            if(amount == 0)
            {

                return;

            }

            if (!Mod.instance.save.goods.ContainsKey(export))
            {

                Mod.instance.save.goods[export] = amount;

                return;

            }

            Mod.instance.save.goods[export] += amount;

            if (Mod.instance.save.goods[export] > 9999)
            {

                Mod.instance.save.goods[export] = 9999;

            }

        }

        public int OrderPrice(ExportGood.goods good, int requirement, int provided, int timer)
        {

            int basePrice = GoodsPrice(good);

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

        public bool UpdateMachine(ExportMachine.machines machineId)
        {

            ExportMachine machine = machines[machineId];

            List<int> required = new();

            if (!Mod.instance.save.machines.ContainsKey(machineId))
            {

                Mod.instance.save.machines[machineId] = new() { machine = machineId, level = 1 };

            }

            int level = Mod.instance.save.machines[machineId].level;

            if (Mod.instance.save.resources.ContainsKey(machine.labour))
            {

                if (Mod.instance.save.resources[machine.labour] < machine.hireage[level])
                {

                    return false;

                }

            }
            else
            {

                return false;

            }

            if (Mod.instance.save.resources.ContainsKey(ExportResource.resources.materials))
            {

                if (Mod.instance.save.resources[ExportResource.resources.materials] < machine.materials[level])
                {

                    return false;

                }

            }
            else
            {

                return false;

            }

            Mod.instance.save.resources[machine.labour] -= machine.hireage[level];

            Mod.instance.save.resources[ExportResource.resources.materials] -= machine.materials[level];

            Mod.instance.save.machines[machineId].level++;

            return true;

        }

        public void Orders()
        {

            Dictionary<ExportGood.goods, int> currentGoods = new();

            Dictionary<ExportGuild.guilds, int> currentGuilds = new();

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

            Dictionary<ExportGood.goods,int> viableGoods = new();

            foreach (KeyValuePair<ExportGood.goods, ExportGood> viable in goods)
            {

                if (currentGoods.ContainsKey(viable.Key))
                {

                    continue;

                }

                viableGoods[viable.Key] = 1;

            }

            foreach (KeyValuePair<ExportGuild.guilds, ExportGuild> contracts in guilds)
            {

                ExportGuild guild = contracts.Value;

                if (!RelicHandle.HasRelic(guild.license))
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

                ExportGood.goods good = viableGoods.ElementAt(randomGood).Key;

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

                float goodRequirement = 2f;

                switch (good)
                {

                    case ExportGood.goods.potions:
                    case ExportGood.goods.whiskey:

                        goodRequirement = 5f;

                        break;

                    case ExportGood.goods.powders:
                    case ExportGood.goods.brandy:

                        goodRequirement = 3f;

                        break;

                    case ExportGood.goods.aquavitae:
                    case ExportGood.goods.ambrosia:

                        goodRequirement = 1f;

                        break;


                }

                float orderRequirement = goodRequirement * 5;

                float orderFloor = goodRequirement * 4;

                int orderTimer = 2;

                switch (orderLevel)
                {

                    case 1:

                        orderRequirement = goodRequirement * 10;

                        orderFloor = goodRequirement * 8;

                        break;

                    case 2:

                        orderTimer = 3;

                        orderRequirement = goodRequirement * 20;

                        orderFloor = goodRequirement * 16;

                        break;

                    case 3:

                        orderTimer = 3;

                        orderRequirement = goodRequirement * 50;

                        orderFloor = goodRequirement * 40;

                        break;

                    case 4:

                        orderTimer = 4;

                        orderRequirement = goodRequirement * 100;

                        orderFloor = goodRequirement * 80;

                        break;

                }


                ExportOrder order = new();

                order.good = good;

                order.timer = orderTimer;

                order.level = orderLevel;

                order.requirement = (int)orderRequirement;

                order.floor = (int)orderFloor;

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

            if (!Mod.instance.save.goods.ContainsKey(order.good))
            {

                return false;

            }

            if (Mod.instance.save.goods[order.good] < order.floor)
            {

                return false;

            }

            int provide = Math.Min(Mod.instance.save.goods[order.good], order.requirement);

            int sale = OrderPrice(order.good, order.requirement, provide, order.timer);

            Game1.player.Money += sale;

            Mod.instance.save.goods[order.good] -= provide;

            if (Mod.instance.save.goods[order.good] <= 0)
            {

                Mod.instance.save.goods[order.good] = 0;

            }

            if (!Mod.instance.save.guilds.ContainsKey(order.guild))
            { 
            
                

            }

            Mod.instance.save.guilds[order.guild].experience = order.level * 5;

            GuildLevel(order.guild);

            Mod.instance.save.orders[orderKey].complete = true;

            Mod.instance.save.orders[orderKey].sale = sale;

            return true;

        }

        public int QuickSell(ExportGood.goods export)
        {

            if (!Context.IsMainPlayer)
            {

                return 0;

            }

            int currentPrice = GoodsPrice(export);

            int sellPrice = 0;

            ExportGood good = Mod.instance.exportHandle.goods[export];

            if (GuildLevel(good.guild) >= 4)
            {

                sellPrice = (int)(currentPrice * 0.6f);

            }

            return sellPrice;

        }

        public bool SellNow(ExportGood.goods export, int amount)
        {

            ExportGood good = Mod.instance.exportHandle.goods[export];

            int price = QuickSell(export);

            if(price <= 0)
            {

                return false;

            }

            int provide = Math.Min(Mod.instance.save.goods[export], amount);

            Game1.player.Money += provide * price;

            Mod.instance.save.goods[export] -= provide;

            if (Mod.instance.save.goods[export] <= 0)
            {

                Mod.instance.save.goods.Remove(export);

            }

            return true;

        }

        public static int GuildLevel(ExportGuild.guilds guild)
        {

            if (!Mod.instance.save.guilds.ContainsKey(guild))
            {

                Mod.instance.save.guilds[guild] = new();

            }

            int experience = Mod.instance.save.guilds[guild].experience;

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

        public static int GuildNext(ExportGuild.guilds guild)
        {

            if (!Mod.instance.save.guilds.ContainsKey(guild))
            {

                return 10;

            }

            int experience = Mod.instance.save.guilds[guild].experience;

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


        public int MachineStat(ExportMachine.machines machine)
        {

            ExportMachine machineData = machines[machine];

            int stat = machineData.graduation[0];

            if (Mod.instance.save.machines.ContainsKey(machine))
            {

                MachineRecord machineRecord = Mod.instance.save.machines[machine];

                stat = machineData.graduation[machineRecord.level-1];

            }

            return stat;

        }


        public List<string> ResourceProcessing(bool consume = false)
        {

            ChestHandle.RetrieveInventory(ChestHandle.chests.Distillery);

            List<string> conversions = new();

            string sta = string.Empty;

            sta = "==== ESTIMATED PROCESSING ====";

            if (consume)
            {

                sta = "==== DISTILLERY PROCESSING ====";

            }

            conversions.Add(sta);

            int crusherLimit = MachineStat(ExportMachine.machines.crushers);

            sta = "Input crushing capacity: {{value}}";//.Tokens(new { value = crusherLimit, });

            conversions.Add(sta);

            int kilnBonus = MachineStat(ExportMachine.machines.kiln);

            kilnBonus += 5;

            if (MasteryHandle.HasMastery(MasteryNode.nodes.alchemy_byproduct))
            {

                kilnBonus += 10;

            }

            sta = "{{value}}% Potential byproduct Tribute produced by Kiln";//.Tokens(new { value = kilnBonus, });

            conversions.Add(sta);

            int fermentStat = MachineStat(ExportMachine.machines.fermentation);

            int fermentBonus = (100 + fermentStat) / 50;

            sta = "{{value}}% Potential byproduct Nectar produced by Fermentation Tank";//.Tokens(new { value = fermentBonus, });

            conversions.Add(sta);

            int mashtunBonus = MachineStat(ExportMachine.machines.mashtun);

            sta = "{{value}}% Bonus Malt produced by Mash Tun";//.Tokens(new { value = mashtunBonus, });

            conversions.Add(sta);

            int pressBonus = MachineStat(ExportMachine.machines.press);

            sta = "{{value}}% Bonus Must produced by Wine Press";//.Tokens(new { value = pressBonus, });

            conversions.Add(sta);

            sta = "--------- Item Conversion ---------";

            conversions.Add(sta);

            int processed = crusherLimit;

            for (int i = Mod.instance.chests[ChestHandle.chests.Distillery].Items.Count - 1; i >= 0; i--)
            {


                Item getItem = Mod.instance.chests[ChestHandle.chests.Distillery].Items.ElementAt(i);

                if (getItem is not StardewValley.Object)
                {

                    sta = "Skipped non-object {{display}}";//.Tokens(new { display = getItem.DisplayName, });

                    conversions.Add(sta);

                    continue;

                }

                ParsedItemData itemData = ItemRegistry.GetDataOrErrorItem(getItem.QualifiedItemId);

                if (itemData.IsErrorItem)
                {

                    sta = "Skipped error item {{display}}";//.Tokens(new { display = getItem.DisplayName, });

                    conversions.Add(sta);

                    continue;

                }

                StardewValley.Object getObject = getItem as StardewValley.Object;

                int available = Math.Min(processed, getItem.Stack);

                int byproductChance = 0;

                float mustFactor = 0f;

                float maltFactor = 0f;

                switch (getItem.QualifiedItemId)
                {

                    case "(O)262": // wheat
                    case "(O)270": // corn
                    case "(O)271": // rice
                    case "(O)304": // hops
                    case "(O)188": // bean
                    case "(O)192": // potato

                        maltFactor = 1f;

                        break;

                    case "(O)433": // coffee

                        maltFactor = 0.5f;

                        break;

                    case "(O)400": // strawberry
                    case "PowderMelon": // powdermelon
                    case "(O)832": // pineapple

                        mustFactor = 1f;

                        byproductChance += 15;

                        break;

                    case "(O)454": // ancientFruit
                    case "(O)268": // starfruit

                        mustFactor = 1f;

                        byproductChance += 25;

                        break;

                    case "(O)772":
                    case "(O)773": // elixir
                    case "(O)349": // energy tonic
                    case "(O)184": // milk
                    case "(O)186": // large milk
                    case "(O)436": // goat milk
                    case "(O)438": // large goat milk
                    case "(O)180": // egg
                    case "(O)182": // large egg
                    case "(O)176": // brown egg
                    case "(O)174": // large brown egg
                    case "(O)442": // duck egg
                    case "(O)424": // cheese
                    case "(O)426": // goat cheese
                    case "(O)306": // mayo
                    case "(O)307": // duck mayo

                        maltFactor = 0.2f;

                        byproductChance = 100;

                        break;

                    default:

                        if (getObject.Edibility < 0)
                        {

                            sta = "Skipped inedible {{display}}";//.Tokens(new { display = getItem.DisplayName, });

                            conversions.Add(sta);

                        }

                        switch (getObject.Category)
                        {

                            case StardewValley.Object.artisanGoodsCategory:

                                sta = "Skipped artisanal good {{display}}";//.Tokens(new { display = getItem.DisplayName, });

                                break;

                            case StardewValley.Object.CookingCategory:

                                maltFactor = 0.2f;

                                byproductChance = 100;

                                break;

                            case StardewValley.Object.FruitsCategory:

                                mustFactor = 1f;

                                break;

                            case StardewValley.Object.VegetableCategory:
                            case StardewValley.Object.GreensCategory:

                                maltFactor = 0.4f;

                                byproductChance -= 10;

                                if (byproductChance < 0)
                                {

                                    byproductChance = 0;

                                }

                                break;

                            case StardewValley.Object.flowersCategory:

                                mustFactor = 0.5f;

                                byproductChance += 50;

                                break;

                        }

                        break;

                }

                string qual = StringData.normal;

                switch (getItem.Quality)
                {
                    case 1:

                        qual = StringData.silver;

                        byproductChance += 5;

                        break;

                    case 2:

                        qual = StringData.gold;

                        byproductChance += 10;

                        break;

                    case 4:

                        qual = StringData.iridium;

                        byproductChance += 15;

                        break;
                }

                int byproduct = Mod.instance.randomIndex.Next(byproductChance);

                int convert = (int)(getObject.sellToStorePrice() / 50);

                int malt = (int)(convert * maltFactor);

                int producedMalt = 0;

                int producedTribute = 0;

                int producedMust = 0;

                int producedNectar = 0;

                if (malt > 0)
                {

                    int tributeChance = kilnBonus + byproductChance;

                    malt += (int)(mashtunBonus * malt / 100);

                    int tribute = Math.Min(malt, (int)(malt * byproductChance / 100));

                    malt -= tribute;

                    producedMalt = malt * available;

                    producedTribute = tribute * available;

                    sta = "Produced {{malt}} Malt and {{tribute}} Tribute from {{amount}} {{qual}} {{display}}";//.Tokens(new {  malt = producedMalt, tribute = producedTribute, amount = available, qual = qual, display = getItem.DisplayName, });

                    conversions.Add(sta);

                }

                int must = (int)(convert * mustFactor);

                if (must > 0)
                {

                    int tributeChance = fermentBonus + byproductChance;

                    must += (int)(pressBonus * must / 100);

                    int nectar = Math.Min(must, (int)(must * byproductChance / 100));

                    must -= nectar;

                    producedMust = nectar * available;

                    producedNectar = nectar * available;

                    sta = "Produced {{must}} Malt and {{nectar}} Tribute from {{amount}} {{qual}} {{display}}";//.Tokens(new {  must = producedMust, nectar = producedNectar, amount = available, qual = qual, display = getItem.DisplayName, });

                    conversions.Add(sta);

                }

                if (consume)
                {

                    int totalProduct = producedMalt + producedTribute + producedMust + producedNectar;

                    if (totalProduct > 0)
                    {

                        Mod.instance.save.resources[ExportResource.resources.must] += producedMust;

                        Mod.instance.save.resources[ExportResource.resources.malt] += producedMalt;

                        Mod.instance.save.resources[ExportResource.resources.tribute] += producedTribute;

                        Mod.instance.save.resources[ExportResource.resources.nectar] += producedNectar;

                        Mod.instance.chests[ChestHandle.chests.Distillery].Stack = Mod.instance.chests[ChestHandle.chests.Distillery].Stack - available;

                    }

                }

                processed -= available;

                if (processed <= 0)
                {

                    break;

                }

            }

            if (consume)
            {

                ChestHandle.CleanInventory(ChestHandle.chests.Distillery);

            }

            return conversions;

        }

        public List<string> GoodsProduction(bool consume = false)
        {

            List<string> conversions = new();

            string sta = string.Empty;

            sta = "==== ESTIMATED PRODUCTION ====";

            if (consume)
            {

                sta = "===== DISTILLERY PRODUCTION ====";

            }

            conversions.Add(sta);

            int fermentationLimit = MachineStat(ExportMachine.machines.fermentation);

            sta = "Base production capacity of Fermentation Tank: {{value}}";//.Tokens(new { value = fermentationLimit, });

            conversions.Add(sta);

            int distilleryBonus = MachineStat(ExportMachine.machines.distillery);

            sta = "{{value}}% increased capacity of goods production from Distillery";//.Tokens(new { value = distilleryBonus, });

            conversions.Add(sta);

            int barrelBonus = MachineStat(ExportMachine.machines.barrel);

            sta = "{{value}}% Potential bonus product from Aging Barrels";//.Tokens(new { value = barrelBonus, });

            conversions.Add(sta);

            int packerBonus = MachineStat(ExportMachine.machines.packer);

            sta = "{{value}}% increase to overall production via Bottling, Blending and Packing";//.Tokens(new { value = packerBonus, });

            conversions.Add(sta);

            sta = "--------- Goods Production ---------";

            conversions.Add(sta);

            int production = fermentationLimit + (fermentationLimit * distilleryBonus / 100);

            List<ExportResource.resources> resourcing = new()
            {

                ExportResource.resources.malt,
                ExportResource.resources.must,
                ExportResource.resources.tribute,
                ExportResource.resources.nectar,

            };

            List<ExportGood.goods> goodsAvailable = new()
            {

                ExportGood.goods.whiskey,
                ExportGood.goods.brandy,
                ExportGood.goods.aquavitae,
                ExportGood.goods.ambrosia,

            };

            List<int> goodsFactors = new()
            {
                5,
                10,
                20,
                10,
            };

            for(int i = 0; i < 4; i++)
            {

                ExportResource.resources resource = resourcing[i];

                ExportGood.goods good = goodsAvailable[i];

                int available = 0;

                if (Mod.instance.save.resources.ContainsKey(resource))
                {

                    available = Math.Min(production, Mod.instance.save.resources[resource]);

                }

                int excess = available % goodsFactors[i];

                available -= excess;

                if (available <= 0)
                {

                    sta = "Skipped production of {{good}} due to lack of {{resource}}";//.Tokens(new { good = goods[good],resource = resources[resource].name, });

                    conversions.Add(sta);

                    continue;

                }

                int goodsProduced = available / goodsFactors[i];

                int bonusProduct = Mod.instance.randomIndex.Next(barrelBonus) + packerBonus;

                goodsProduced += (goodsProduced * bonusProduct / 100);

                sta = "Produced {{amount}} of {{good}} from {{used}} {{resource}}";//.Tokens(new { amount = goodsProduced, good = goods[good], used = available, resource = resources[resource].name, });

                conversions.Add(sta);

                sta = "{{bonus}}% bonus {{good}} produced through Barrelling and Bottling process";//.Tokens(new { bonus = bonusProduct, good = goods[good],});

                conversions.Add(sta);

                if (consume)
                {

                    Mod.instance.save.resources[resource] -= available;

                    Mod.instance.save.goods[good] += goodsProduced;

                }

            }

            return conversions;


        }


    }

}
