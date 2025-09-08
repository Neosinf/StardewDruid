
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Machines;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Minigames;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;

namespace StardewDruid.Handle
{

    public class ApothecaryItem
    {

        public ApothecaryHandle.items item = ApothecaryHandle.items.none;

        public IconData.relics relic = IconData.relics.none;

        public string title;

        public string description;

        public List<string> details = new();

        public AlchemyRecipe.recipes recipe = AlchemyRecipe.recipes.none;

        public int health;

        public int stamina;

        public BuffHandle.buffTypes buff = BuffHandle.buffTypes.none;

        public int level = 1;

        public int duration;

        public int convert = 0;

        public ExportResource.resources resource = ExportResource.resources.malt;

        public ExportGood.goods export = ExportGood.goods.potions;

        public int units;

        public enum herbalType
        {
            resource,
            potion,
            powder,
            ball,
            omen,
            trophy
        }

        public herbalType type = herbalType.potion;

    }

    public class ApothecaryRecord
    {

        public ApothecaryHandle.items item;

        public int amount;

        public enum automation
        {
            none,
            brew,
            apply,
            both,
        }

        public automation behaviour;

        public int total;

    }

    public class AlchemyRecipe
    {

        public enum recipes
        {

            none,

            viscosa,
            convert_viscosa,

            ligna,
            satius_ligna,
            magnus_ligna,
            optimus_ligna,

            faeth,

            vigores,
            satius_vigores,
            magnus_vigores,
            optimus_vigores,

            aether,

            celeri,
            satius_celeri,
            magnus_celeri,
            optimus_celeri,

            coruscant,

            imbus,
            amori,
            macerari,
            rapidus,

            voil,

            concutere,
            jumere,
            felis,
            sanctus,

            capesso,

            captis,
            ferrum_captis,
            aurum_captis,
            diamas_captis,

            best_ligna,
            best_vigores,
            best_celeri,

            convert_treeseed,
            convert_gems,

            random_powder,

            convert_liquid,
            convert_rareseed,
            convert_mixedseed,
            convert_flowerseed,
            convert_wildseed,

            update_cooking,
            update_stack,
            update_produce,
            update_artisanal,

            convert_bombs,
            convert_caffeine,
            convert_geodes,
            convert_gold,

            next_ligna,
            next_satius_ligna,
            next_magnus_ligna,

            next_vigores,
            next_satius_vigores,
            next_magnus_vigores,

            next_celeri,
            next_satius_celeri,
            next_magnus_celeri,

            next_coruscant,
            next_imbus,
            next_amori,
            next_macerari,
            next_rapidus,


        }

        public recipes type = recipes.viscosa;

        public enum qualifiers
        {
            none,
            validobject,
            edible,
            deconstructable,
            saleable,
            cooking,
            produce,
            artisan,
            stackable,
            tool,
        }

        public List<qualifiers> requirements = new();

        public string name;

        public ApothecaryHandle.items product;

        public string instruction;

        public List<ApothecaryHandle.items> items = new();

        public List<string> ingredients = new();

        public AlchemyProcess.processes process = AlchemyProcess.processes.transmute;


    }

    public class AlchemyProduct
    {

        public ApothecaryHandle.items apothecaryItem;

        public StardewValley.Object inventoryItem;

        public int processed;

        public int repeatable;

        public int slot;

        public Dictionary<ApothecaryHandle.items, int> fromApothecary = new();

        public Dictionary<ApothecaryHandle.items, int> totalApothecary = new();

        public Dictionary<string, int> fromInventory = new();

        public Dictionary<string, int> totalInventory = new();

        public Dictionary<int, int> fromSlot = new();

        public Dictionary<int, int> totalSlot = new();

        public AlchemyProcess.processes process = AlchemyProcess.processes.transmute;
    
    }

    public class AlchemyProcess
    {

        public enum processes
        {
            winds,
            weald,
            mists,
            stars,
            voide,
            fates,
            ether,
            witch,

            sol,
            material,
            reagent,
            lune,

            separate, // nigredo
            transmute, // albedo
            enchant, // rubedo

        }

        public processes process;

        public string label;

        public string name;

        public string description;

    }

    public class ApothecaryHandle
    {

        public enum items
        {

            none,

            // potions
            viscosa,
            ligna,
            satius_ligna,
            magnus_ligna,
            optimus_ligna,

            faeth,
            vigores,
            satius_vigores,
            magnus_vigores,
            optimus_vigores,

            aether,
            celeri,
            satius_celeri,
            magnus_celeri,
            optimus_celeri,

            // powders
            coruscant,
            imbus,
            amori,
            macerari,
            rapidus,

            voil,
            concutere,
            jumere,
            felis,
            sanctus,

            capesso,
            captis,
            ferrum_captis,
            aurum_captis,
            diamas_captis,

            // omens
            omen_feather,
            omen_tuft,
            omen_shell,
            omen_nest,
            omen_glass,

            omen_down,
            omen_coral,
            omen_berry,
            omen_courtseed,
            omen_guardhop,

            omen_doubtseed,
            omen_wealdseed,
            omen_elderbloom,
            omen_courtbloom,
            omen_sacredlily,

            // trophies
            trophy_shroom,
            trophy_eye,
            trophy_pumpkin,
            trophy_pearl,
            trophy_tooth,

            trophy_shell,
            trophy_gloop,
            trophy_string,
            trophy_tusk,
            trophy_heart,

            trophy_skull,
            trophy_dragon,
            trophy_tendril,
            trophy_spike,
            trophy_wood,

        }

        public Dictionary<items, ApothecaryItem> apothecary = new();

        public Dictionary<AlchemyRecipe.recipes, AlchemyRecipe> recipes = new();

        public Dictionary<AlchemyProcess.processes, List<AlchemyRecipe.recipes>> processing = new();

        public Dictionary<AlchemyProcess.processes, AlchemyProcess> processes = new();

        public BuffHandle buff = new();

        public static List<items> potionLayout = new()
        {

            items.viscosa,
            items.ligna,
            items.satius_ligna,
            items.magnus_ligna,
            items.optimus_ligna,

            items.faeth,
            items.vigores,
            items.satius_vigores,
            items.magnus_vigores,
            items.optimus_vigores,

            items.aether,
            items.celeri,
            items.satius_celeri,
            items.magnus_celeri,
            items.optimus_celeri,

        };

        public static List<items> bombLayout = new()
        {

            items.coruscant,
            items.imbus,
            items.amori,
            items.macerari,
            items.rapidus,

            items.voil,
            items.concutere,
            items.jumere,
            items.felis,
            items.sanctus,

            items.capesso,
            items.captis,
            items.ferrum_captis,
            items.aurum_captis,
            items.diamas_captis,

        };

        public static List<items> omenLines = new()
        {

            // omens
            items.omen_feather,
            items.omen_tuft,
            items.omen_shell,
            items.omen_nest,
            items.omen_glass,

            items.omen_down,
            items.omen_coral,
            items.omen_berry,
            items.omen_courtseed,
            items.omen_guardhop,

            items.omen_doubtseed,
            items.omen_wealdseed,
            items.omen_elderbloom,
            items.omen_courtbloom,
            items.omen_sacredlily,


        };

        public static List<items> trophyLines = new()
        {

            // trophies
            items.trophy_shroom,
            items.trophy_eye,
            items.trophy_pumpkin,
            items.trophy_pearl,
            items.trophy_tooth,

            items.trophy_shell,
            items.trophy_spike,
            items.trophy_string,
            items.trophy_dragon,
            items.trophy_tusk,

            items.trophy_heart,
            items.trophy_skull,
            items.trophy_tendril,
            items.trophy_gloop,
            items.trophy_wood,

        };

        public double consumeBuffer;

        public ApothecaryHandle()
        {

        }

        public static Microsoft.Xna.Framework.Rectangle ItemRectangles(items potion, bool gray = false)
        {

            if (potion == items.none)
            {

                return new();

            }

            int slot = (int)potion - 1;

            Microsoft.Xna.Framework.Rectangle source = new(slot % 5 * 20, slot == 0 ? 0 : slot / 5 * 20, 20, 20);

            if (gray)
            {

                source.Y += 240;

            }

            return source;

        }

        public void LoadItems()
        {

            apothecary = ApothecaryData.ItemList();

            recipes = AlchemyData.RecipeList();

            processing.Clear();

            foreach (KeyValuePair<AlchemyRecipe.recipes, AlchemyRecipe> recipe in recipes)
            {
                
                if (!processing.ContainsKey(recipe.Value.process))
                {
                    
                    processing[recipe.Value.process] = new();
                
                }
                
                processing[recipe.Value.process].Add(recipe.Key);
            
            }

            processes = AlchemyData.ProcessList();

        }

        public static int UpdateAmounts(items herbal, int amount = 0)
        {

            int existing = GetAmount(herbal);

            if (amount == 0)
            {

                return existing;

            }
            else if (amount > 0)
            {

                int addition = Math.Min(amount, 9999 - existing);

                Mod.instance.save.apothecary[herbal].amount += addition;

                Mod.instance.save.apothecary[herbal].total += addition;

                Mod.instance.SyncPreferences();

            }
            else
            {

                int subtraction = existing - Math.Abs(amount);

                if (subtraction < 0)
                {

                    Mod.instance.save.apothecary[herbal].amount = 0;

                }
                else
                {

                    Mod.instance.save.apothecary[herbal].amount -= Math.Abs(amount);

                }

                Mod.instance.SyncPreferences();

            }

            return Mod.instance.save.apothecary[herbal].amount;

        }

        public static int GetAmount(items herbal)
        {

            if(herbal == items.none)
            {

                return 0;

            }

            if (!Mod.instance.save.apothecary.ContainsKey(herbal))
            {

                Mod.instance.save.apothecary[herbal] = new() { amount = 5, total = 0, behaviour = GetDefaults(), };

            }

            return Mod.instance.save.apothecary[herbal].amount;

        }


        // =============================================================== Journal
        public Dictionary<int, ContentComponent> JournalByType(DruidJournal.journalTypes type)
        {

            switch (type)
            {
                default:
                case DruidJournal.journalTypes.potions:

                    return JournalLayout(potionLayout);

                case DruidJournal.journalTypes.powders:

                    return JournalLayout(bombLayout);

                case DruidJournal.journalTypes.omens:

                    return JournalLayout(omenLines);

                case DruidJournal.journalTypes.trophies:

                    return JournalLayout(trophyLines);

            }

        }

        public Dictionary<int, ContentComponent> JournalLayout(List<items> layout)
        {

            Dictionary<int, ContentComponent> journal = new();

            int journalGrid = 0;

            int journalKey = 0;

            foreach (items herbal in layout)
            {

                string key = herbal.ToString();

                if (!apothecary.ContainsKey(herbal))
                {

                    continue;

                }

                ApothecaryItem item = apothecary[herbal];

                /*if(item.relic != IconData.relics.none)
                {

                    if (!RelicHandle.HasRelic(item.relic))
                    {

                        continue;

                    }

                }*/

                // -----------------------------------------------------------item display

                ContentComponent content = new(ContentComponent.contentTypes.custom, key);

                int amount = GetAmount(herbal);

                string amountString = amount.ToString();

                content.text[0] = amount.ToString();

                content.textureSources[0] = ItemRectangles(item.item, amount == 0);

                content.grid = journalGrid;

                content.serial = 0;

                journal[journalKey++] = content;

                // ----------------------------------------------------------- alchemy button

                ContentComponent alchemyButton = new(ContentComponent.contentTypes.toggle, key);

                alchemyButton.grid = journalGrid;

                alchemyButton.serial = 1;

                if (item.recipe != AlchemyRecipe.recipes.none)
                {

                    alchemyButton.textureSources[0] = IconData.DisplayRectangle(IconData.displays.craft);

                }
                else
                {

                    alchemyButton.active = false;

                }

                journal[journalKey++] = alchemyButton;

                // ----------------------------------------------------------- ship button

                ContentComponent goodsButton = new(ContentComponent.contentTypes.toggle, key);

                goodsButton.grid = journalGrid;

                goodsButton.serial = 2;

                goodsButton.textureSources[0] = IconData.DisplayRectangle(IconData.displays.goods);

                if (item.units == 0 || amount == 0)
                {

                    goodsButton.active = false;

                }

                journal[journalKey++] = goodsButton;

                // ----------------------------------------------------------- settings button

                ContentComponent settingsButton = new(ContentComponent.contentTypes.toggle, key);

                settingsButton.grid = journalGrid;

                settingsButton.serial = 3;

                switch (item.type)
                {

                    case ApothecaryItem.herbalType.omen:
                    case ApothecaryItem.herbalType.trophy:

                        settingsButton.active = false;

                        break;

                    default:

                        IconData.displays flag = IconData.displays.exit;

                        StringData.str hovertext = StringData.str.autoOff;

                        if (Mod.instance.save.apothecary.ContainsKey(herbal))
                        {

                            switch (Mod.instance.save.apothecary[herbal].behaviour)
                            {

                                case ApothecaryRecord.automation.brew:

                                    flag = IconData.displays.herbalism;

                                    hovertext = StringData.str.autoCraft;

                                    break;

                                case ApothecaryRecord.automation.apply:

                                    flag = IconData.displays.complete;

                                    hovertext = StringData.str.autoApply;

                                    break;

                                case ApothecaryRecord.automation.both:

                                    flag = IconData.displays.flag;

                                    hovertext = StringData.str.autoBoth;

                                    break;


                            }

                        }

                        settingsButton.textureSources[0] = IconData.DisplayRectangle(flag);

                        settingsButton.text[0] = StringData.Get(hovertext);

                        break;

                }

                journal[journalKey++] = goodsButton;

                journalGrid++;

            }

            return journal;

        }

        public static ApothecaryRecord.automation GetDefaults()
        {

            return Enum.Parse<ApothecaryRecord.automation>(Mod.instance.Config.potionDefault);

        }

        public void ShiftBehaviour(items id)
        {

            GetAmount(id);

            switch (Mod.instance.save.apothecary[id].behaviour)
            {

                case ApothecaryRecord.automation.none:
                    Mod.instance.save.apothecary[id].behaviour = ApothecaryRecord.automation.brew;
                    break;
                case ApothecaryRecord.automation.brew:
                    Mod.instance.save.apothecary[id].behaviour = ApothecaryRecord.automation.apply;
                    break;
                case ApothecaryRecord.automation.apply:
                    Mod.instance.save.apothecary[id].behaviour = ApothecaryRecord.automation.both;
                    break;
                default:
                case ApothecaryRecord.automation.both:
                    Mod.instance.save.apothecary[id].behaviour = ApothecaryRecord.automation.none;
                    break;

            }

        }

        public bool ConvertToGoods(items id, int amount)
        {

            ApothecaryItem itemData = apothecary[id];

            if (itemData.units <= 0)
            {

                return false;

            }

            int exist = GetAmount(id);

            if (exist == 0)
            {

                return false;

            }

            int convert = Math.Min(amount, exist);

            UpdateAmounts(id, 0 - convert);

            switch (itemData.convert)
            {
                case 1: // resource

                    int resources = convert * itemData.units;

                    Mod.instance.exportHandle.AddResource(itemData.resource, resources);

                    break;

                case 2: // good

                    int goods = convert * itemData.units;

                    Mod.instance.exportHandle.AddGood(itemData.export, goods);

                    break;
            }


            return true;

        }

        public bool ConsumeItem(items id, int amount)
        {

            int available = GetAmount(id);

            if (available <= 0)
            {

                return false;

            }

            ApothecaryItem itemData = apothecary[id];

            int updateAmount = Math.Min(available,amount);

            switch (id)
            {
                case items.faeth:
                case items.aether:
                case items.voil:
                case items.coruscant:

                    return false;

                case items.viscosa:

                    if (!ReplenishStamina(itemData, updateAmount))
                    {

                        return false;

                    }

                    Rectangle healthBox = Game1.player.GetBoundingBox();

                    if (Game1.currentGameTime.TotalGameTime.TotalSeconds > consumeBuffer)
                    {

                        consumeBuffer = Game1.currentGameTime.TotalGameTime.TotalSeconds + 5;

                        DisplayMessage hudmessage = new(Mod.instance.Helper.Translation.Get("HerbalData.1116").Tokens(new { potion = itemData.title, }), itemData);

                        Game1.addHUDMessage(hudmessage);

                    }

                    break;

                case items.ligna:
                case items.satius_ligna:
                case items.magnus_ligna:
                case items.optimus_ligna:
                case items.vigores:
                case items.satius_vigores:
                case items.magnus_vigores:
                case items.optimus_vigores:
                case items.celeri:
                case items.satius_celeri:
                case items.magnus_celeri:
                case items.optimus_celeri:

                    if(!ReplenishStamina(itemData, updateAmount) && !buff.ApplyBuff(itemData))
                    {
                        
                        return false;

                    }

                    break;

                case items.imbus:
                case items.amori:
                case items.macerari:
                case items.rapidus:
                case items.concutere:
                case items.jumere:
                case items.felis:
                case items.sanctus:
                case items.capesso:
                case items.captis:
                case items.ferrum_captis:
                case items.aurum_captis:
                case items.diamas_captis:

                    if (!buff.ApplyBuff(itemData))
                    {

                        return false;

                    }

                    break;


            }

            UpdateAmounts(itemData.item, 0 - updateAmount);

            Mod.instance.SyncPreferences();

            return true;

        }

        public bool ReplenishStamina(ApothecaryItem itemData, int updateAmount)
        {

            if (Game1.player.Stamina == Game1.player.MaxStamina && Game1.player.health == Game1.player.maxHealth)
            {

                return false;

            }

            float difficulty = Mod.instance.masteryHandle.ReplenishmentFactor();

            int staminaGain = (int)(itemData.stamina * difficulty);

            int healthGain = (int)(itemData.health * difficulty);

            Game1.player.Stamina = Math.Min(Game1.player.MaxStamina, Game1.player.Stamina + (staminaGain * updateAmount));

            Game1.player.health = Math.Min(Game1.player.maxHealth, Game1.player.health + (staminaGain * updateAmount));

            return true;

        }

        public void MassCraft(List<items> layout, int amount = 5)
        {

            foreach (items id in layout)
            {

                if (!apothecary.ContainsKey(id))
                {

                    continue;

                }

                ApothecaryItem item = apothecary[id];

                if(item.recipe == AlchemyRecipe.recipes.none)
                {

                    continue;

                }

                if (item.relic != IconData.relics.none)
                {

                    if (!RelicHandle.HasRelic(item.relic))
                    {

                        continue;

                    }

                }

                UseRecipe(item.recipe, Game1.player.CurrentToolIndex, amount);

            }

        }

        public static bool CompareLists<T>(List<T> list1,List<T> list2)
        {

            foreach (T requirement in list1)
            {

                if (!list2.Contains(requirement))
                {

                    return false;

                }

            }

            return true;

        }

        public static Dictionary<AlchemyProcess.processes, AlchemyRecipe.recipes> FindRecipe(int slot, ApothecaryHandle.items reagent)
        {

            Dictionary<AlchemyProcess.processes, AlchemyRecipe.recipes> recipes = new();

            Dictionary<AlchemyRecipe.qualifiers, int> qualifiers = new();

            if (slot != -1 && Game1.player.Items[slot] != null)
            {

                qualifiers = ApothecaryHandle.AnalyseItem(Game1.player.Items[slot]);

            }

            foreach (KeyValuePair<AlchemyProcess.processes, List<AlchemyRecipe.recipes>> processList in Mod.instance.apothecaryHandle.processing)
            {

                switch (processList.Key)
                {

                    case AlchemyProcess.processes.separate:

                        if (!MasteryHandle.HasMastery(Data.MasteryNode.nodes.alchemy_separate))
                        {

                            continue;

                        }

                        break;

                    case AlchemyProcess.processes.enchant:

                        if (!MasteryHandle.HasMastery(Data.MasteryNode.nodes.alchemy_enchant))
                        {

                            continue;

                        }

                        break;

                }

                foreach (AlchemyRecipe.recipes recipe in processList.Value)
                {

                    AlchemyRecipe recipeData = Mod.instance.apothecaryHandle.recipes[recipe];

                    if (recipeData.requirements.Count > 0)
                    {

                        if (slot == -1 || Game1.player.Items[slot] == null)
                        {

                            continue;

                        }

                        if (!CompareLists(recipeData.requirements,new List<AlchemyRecipe.qualifiers>(qualifiers.Keys)))
                        {

                            continue;

                        }

                    }

                    if (recipeData.ingredients.Count > 0)
                    {
                        
                        if (slot == -1 || Game1.player.Items[slot] == null)
                        {

                            continue;

                        }

                        if (!recipeData.ingredients.Contains(Game1.player.Items[slot].QualifiedItemId))
                        {

                            continue;

                        }

                    }

                    if (recipeData.items.Count > 0)
                    {

                        if (reagent == items.none)
                        {

                            continue;

                        }

                        if (recipeData.items.First() != reagent)
                        {

                            continue;

                        }

                    }

                    recipes[processList.Key] = recipe;

                    break;

                }


            }

            return recipes;

        }

        // =============================================================== Item Analysis

        public static Dictionary<AlchemyRecipe.qualifiers,int> AnalyseItem(StardewValley.Item item)
        {

            Dictionary<AlchemyRecipe.qualifiers, int> qualifiers = new();

            if (item == null)
            {

                return qualifiers;

            }

            if(item.Stack <= 0)
            {

                return qualifiers;

            }

            if (item is StardewValley.Object obj)
            {

                qualifiers.Add(AlchemyRecipe.qualifiers.validobject,1);

                if (obj.Edibility > 0)
                {

                    qualifiers.Add(AlchemyRecipe.qualifiers.edible, 1);

                }


                Item proxy = StardewValley.Object.OutputDeconstructor(null, obj, true, null, Game1.player, out _);

                if (proxy != null)
                {

                    qualifiers.Add(AlchemyRecipe.qualifiers.deconstructable, 1);

                }

                int sell = obj.sellToStorePrice();

                if (sell <= 20)
                {

                    if (proxy != null)
                    {

                        sell = proxy.sellToStorePrice() * 2;

                    }

                }

                if (sell > 20)
                {

                    qualifiers.Add(AlchemyRecipe.qualifiers.saleable, sell);

                }

                switch (obj.Category)
                {

                    case StardewValley.Object.CookingCategory:

                        qualifiers.Add(AlchemyRecipe.qualifiers.cooking, 1);

                        break;

                    case StardewValley.Object.artisanGoodsCategory:

                        qualifiers.Add(AlchemyRecipe.qualifiers.artisan, 1);

                        break;

                    case StardewValley.Object.VegetableCategory:
                    case StardewValley.Object.FruitsCategory:
                    case StardewValley.Object.flowersCategory:

                        qualifiers.Add(AlchemyRecipe.qualifiers.produce, 1);

                        break;

                }

                int stacksize = obj.maximumStackSize();

                if (stacksize > 1)
                {

                    qualifiers.Add(AlchemyRecipe.qualifiers.produce, stacksize);

                }

            }

            if(item is Tool tool)
            {
                
                qualifiers.Add(AlchemyRecipe.qualifiers.tool, 1);

            }

            return qualifiers;

        }

        public static bool CheckProduce(StardewValley.Object obj)
        {

            switch (obj.Category)
            {

                case StardewValley.Object.VegetableCategory:
                case StardewValley.Object.FruitsCategory:
                case StardewValley.Object.flowersCategory:

                    return true;

            }

            return false;

        }

        // =============================================================== Recipe Management

        public AlchemyProduct GetProduct(AlchemyRecipe.recipes recipe, int slot = -1, int intent = 1)
        {

            switch (recipe)
            {

                case AlchemyRecipe.recipes.viscosa:

                    if(slot == -1)
                    {

                        return CraftProduct(recipe, slot, intent);

                    }

                    return ConvertViscosa(AlchemyRecipe.recipes.convert_viscosa, slot, intent);

                case AlchemyRecipe.recipes.ligna:
                case AlchemyRecipe.recipes.satius_ligna:
                case AlchemyRecipe.recipes.magnus_ligna:
                case AlchemyRecipe.recipes.optimus_ligna:
                case AlchemyRecipe.recipes.faeth:
                case AlchemyRecipe.recipes.vigores:
                case AlchemyRecipe.recipes.satius_vigores:
                case AlchemyRecipe.recipes.magnus_vigores:
                case AlchemyRecipe.recipes.optimus_vigores:
                case AlchemyRecipe.recipes.aether:
                case AlchemyRecipe.recipes.celeri:
                case AlchemyRecipe.recipes.satius_celeri:
                case AlchemyRecipe.recipes.magnus_celeri:
                case AlchemyRecipe.recipes.optimus_celeri:
                case AlchemyRecipe.recipes.coruscant:
                case AlchemyRecipe.recipes.imbus:
                case AlchemyRecipe.recipes.amori:
                case AlchemyRecipe.recipes.macerari:
                case AlchemyRecipe.recipes.rapidus:
                case AlchemyRecipe.recipes.voil:
                case AlchemyRecipe.recipes.concutere:
                case AlchemyRecipe.recipes.jumere:
                case AlchemyRecipe.recipes.felis:
                case AlchemyRecipe.recipes.sanctus:
                case AlchemyRecipe.recipes.capesso:
                case AlchemyRecipe.recipes.captis:
                case AlchemyRecipe.recipes.ferrum_captis:
                case AlchemyRecipe.recipes.aurum_captis:
                case AlchemyRecipe.recipes.diamas_captis:

                    return CraftProduct(recipe,slot,intent);

                case AlchemyRecipe.recipes.best_celeri:
                case AlchemyRecipe.recipes.best_vigores:
                case AlchemyRecipe.recipes.best_ligna:
                case AlchemyRecipe.recipes.random_powder:

                    return BestProduct(recipe, slot, intent);

                case AlchemyRecipe.recipes.convert_viscosa:

                    return ConvertViscosa(recipe, slot, intent);

                case AlchemyRecipe.recipes.convert_treeseed:
                case AlchemyRecipe.recipes.convert_gems:
                case AlchemyRecipe.recipes.convert_liquid:
                case AlchemyRecipe.recipes.convert_rareseed:
                case AlchemyRecipe.recipes.convert_mixedseed:
                case AlchemyRecipe.recipes.convert_flowerseed:
                case AlchemyRecipe.recipes.convert_wildseed:
                case AlchemyRecipe.recipes.convert_bombs:
                case AlchemyRecipe.recipes.convert_caffeine:
                case AlchemyRecipe.recipes.convert_geodes:
                case AlchemyRecipe.recipes.convert_gold:

                    return ConvertInventoryItem(recipe, slot, intent);

                case AlchemyRecipe.recipes.update_cooking:
                case AlchemyRecipe.recipes.update_stack:
                case AlchemyRecipe.recipes.update_produce:
                case AlchemyRecipe.recipes.update_artisanal:

                    return UpdateInventoryItem(recipe, slot, intent);

                default:

                    return new();

            }

        }

        public AlchemyProduct CraftProduct(AlchemyRecipe.recipes recipe, int slot = -1, int intent = 1)
        {

            AlchemyRecipe recipeData = recipes[recipe];

            AlchemyProduct product = new();

            product.slot = slot;

            product.apothecaryItem = recipeData.product;

            List<int> draft= new()
            {
                9999 - GetAmount(recipeData.product),
            };

            if (recipeData.items.Count > 0)
            {

                foreach (ApothecaryHandle.items item in recipeData.items)
                {

                    int itemAmount = GetAmount(item);

                    product.totalApothecary[item] = itemAmount;

                    draft.Add(itemAmount);

                }

            }

            if (recipeData.ingredients.Count > 0)
            {

                int ingredientTotal = 0;

                if (slot == -1)
                {

                    foreach (string ingredient in recipeData.ingredients)
                    {

                        int ingredientAmount = Game1.player.Items.CountId(ingredient);

                        product.totalInventory[ingredient] = ingredientAmount;

                        ingredientTotal += ingredientAmount;

                    }

                }
                else
                {

                    Item slotIngredient = Game1.player.Items[slot];

                    if (recipeData.ingredients.Contains(slotIngredient.ItemId))
                    {

                        int ingredientAmount = slotIngredient.Stack;

                        product.totalInventory[slotIngredient.ItemId] = ingredientAmount;

                        ingredientTotal += ingredientAmount;

                    }

                }

                draft.Add(ingredientTotal);

            }

            List<int> minimum = draft;

            minimum.Add(intent);

            product.processed = minimum.Min();

            product.repeatable = draft.Min();

            foreach(KeyValuePair<ApothecaryHandle.items,int> item in product.totalApothecary)
            {

                product.fromApothecary[item.Key] = product.processed;

            }

            int useIngredient = product.processed;

            foreach (KeyValuePair<string, int> ingredient in product.totalInventory)
            {

                product.fromInventory[ingredient.Key] = Math.Min(ingredient.Value,useIngredient);

                useIngredient -= ingredient.Value;

                if(useIngredient <= 0)
                {

                    break;

                }

            }

            return product;

        }

        public AlchemyProduct NextTier(AlchemyRecipe.recipes recipe, int slot, int intent)
        {

            AlchemyRecipe recipeData = recipes[recipe];

            AlchemyProduct product = new();

            product.slot = slot;

            items consume = recipes[recipe].items.First();

            int available = Mod.instance.save.apothecary[consume].amount / 2;

            int consumable = available / 2;

            if (consumable == 0)
            {

                return product;

            }

            product.totalApothecary[consume] = available;

            if (!RelicHandle.HasRelic(apothecary[recipeData.product].relic))
            {

                return product;

            }

            product.apothecaryItem = recipeData.product;

            product.processed = Math.Min(intent, consumable);

            product.repeatable = consumable;

            product.fromApothecary[consume] = product.processed * 2;

            return product;

        }

        public AlchemyProduct BestProduct(AlchemyRecipe.recipes recipe, int slot, int intent)
        {

            AlchemyRecipe recipeData = recipes[recipe];

            AlchemyProduct product = new();

            product.slot = slot;

            items consume = recipes[recipe].items.First();

            int available = Mod.instance.save.apothecary[consume].amount;

            product.totalApothecary[consume] = available;

            if (available == 0)
            {

                return product;

            }

            product.processed = Math.Min(intent, available);

            product.repeatable = available;

            product.fromApothecary[consume] = product.processed;

            List<items> list = new()
            {
                items.optimus_ligna,
                items.magnus_ligna,
                items.satius_ligna,
                items.ligna,
            };

            switch (recipe)
            {
                case AlchemyRecipe.recipes.best_vigores:
                    list = new()
                    {
                        items.optimus_vigores,
                        items.magnus_vigores,
                        items.satius_vigores,
                        items.vigores,
                    };
                    break;

                case AlchemyRecipe.recipes.best_celeri:
                    list = new()
                    {
                        items.optimus_celeri,
                        items.magnus_celeri,
                        items.satius_celeri,
                        items.celeri,
                    };
                    break;

                case AlchemyRecipe.recipes.random_powder:

                    List<items> potential = new()
                    {
                        items.imbus,
                        items.amori,
                        items.macerari,
                        items.rapidus,
                        items.concutere,
                        items.jumere,
                        items.felis,
                        items.sanctus,
                        items.capesso,
                        items.captis,
                        items.ferrum_captis,
                        items.aurum_captis,
                        items.diamas_captis,
                    };

                    list.Clear();

                    int count = potential.Count;

                    while (count > 0)
                    {

                        items item = potential[Mod.instance.randomIndex.Next(count)];

                        if (RelicHandle.HasRelic(apothecary[item].relic))
                        {

                            list.Add(item);

                        }

                        potential.Remove(item);

                        count--;

                    }
  
                    break;

            }

            foreach (items item in list)
            {

                if (RelicHandle.HasRelic(apothecary[item].relic))
                {

                    product.apothecaryItem = item;

                    break;

                }

            }

            return product;

        }

        public AlchemyProduct ConvertViscosa(AlchemyRecipe.recipes recipe, int slot = -1, int intent = 1)
        {

            AlchemyRecipe recipeData = recipes[recipe];

            AlchemyProduct product = new();

            product.slot = slot;

            if (slot == -1)
            {

                return product;

            }

            Dictionary<AlchemyRecipe.qualifiers, int> qualifiers = AnalyseItem(Game1.player.Items[slot]);

            if (!qualifiers.ContainsKey(AlchemyRecipe.qualifiers.edible))
            {

                return product;

            }

            StardewValley.Object obj = Game1.player.Items[slot] as StardewValley.Object;

            product.totalSlot[slot] = obj.Stack;

            int convert = obj.staminaRecoveredOnConsumption();

            product.repeatable = obj.Stack;

            List<int> available = new()
            {

                intent,
                product.repeatable,

            };

            product.processed = available.Min();

            product.fromSlot[slot] = product.processed;

            // -------------------------------------------------

            int adjust = RandomGameIndex(items.viscosa);

            int adjusttwo = RandomGameIndex(items.viscosa, 1);

            string itemId = AlchemyData.goldbar;

            float exchange = 0.05f;

            int amount = (int)((float)convert * exchange * (1 + 0.05 * adjust));

            amount = Math.Max(amount, 1);

            amount *= product.processed;

            product.apothecaryItem = items.viscosa;

            return product;

        }

        public AlchemyProduct ConvertInventoryItem(AlchemyRecipe.recipes recipe, int slot = -1, int intent = 1)
        {

            AlchemyRecipe recipeData = recipes[recipe];

            AlchemyProduct product = new();

            items consume = recipes[recipe].items.First();

            int consumable = Mod.instance.save.apothecary[consume].amount;

            List<int> available = new()
            {

                consumable,

            };

            product.totalApothecary[consume] = consumable;

            product.slot = slot;

            if (slot == -1)
            {

                return product;

            }

            Dictionary<AlchemyRecipe.qualifiers,int> qualifiers = AnalyseItem(Game1.player.Items[slot]);

            if (!qualifiers.ContainsKey(AlchemyRecipe.qualifiers.saleable))
            {

                return product;

            }

            StardewValley.Object obj = Game1.player.Items[slot] as StardewValley.Object;

            product.totalSlot[slot] = obj.Stack;

            if (consumable <= 0)
            {

                return product;

            }

            int convert = qualifiers[AlchemyRecipe.qualifiers.saleable];

            product.repeatable = available.Min();

            available.Add(intent);

            product.processed = available.Min();

            product.fromApothecary[consume] = product.processed;

            product.fromSlot[slot] = product.processed;

            // -------------------------------------------------

            int adjust = RandomGameIndex(consume);

            int adjusttwo = RandomGameIndex(consume,1);

            string itemId = AlchemyData.goldbar;

            float exchange = 0.0075f;

            switch (recipe)
            {

                case AlchemyRecipe.recipes.convert_treeseed:

                    List<string> trees = new()
                    {
                        "(O)311", // Pine
                        "(O)310", // Maple
                        "(O)309", // Oak
                        "(O)311", // Pine
                        "(O)310", // Maple
                        "(O)309", // Oak
                        "(O)292", // Mahogany
                        "(O)292", // Mahogany
                        "MossySeed", // Mossy
                        "(O)251", // Tea Sapling
                    };

                    itemId = trees[adjusttwo];

                    exchange = 0.02f;

                    break;

                case AlchemyRecipe.recipes.convert_gems:

                    List<string> gems = new()
                    {
                        AlchemyData.amethyst,
                        AlchemyData.topaz,
                        AlchemyData.jade,
                        AlchemyData.aquamarine,
                        AlchemyData.emerald,
                        AlchemyData.emerald,
                        AlchemyData.ruby,
                        AlchemyData.ruby,
                        AlchemyData.diamond,
                        AlchemyData.diamond,
                    };

                    itemId = gems[adjusttwo];

                    exchange = 0.005f;

                    break;

                case AlchemyRecipe.recipes.convert_liquid:

                    List<string> syrups = new()
                    {
                        "(O)772", // Garlic
                        "(O)773", // Life Elixer
                        "(O)879", // Musk
                        "(O)724", // Maple
                        "(O)725", // Oak
                        "(O)726", // Pine
                        "(O)724", // Maple
                        "(O)725", // Oak
                        "MysticSyrup", // Pine
                        "(O)186", // Milk
                    };

                    itemId = syrups[adjusttwo];

                    exchange = 0.005f;

                    break;

                case AlchemyRecipe.recipes.convert_rareseed:

                    List<string> rares = new()
                    {
                        AlchemyData.ancientseed,
                        AlchemyData.ancientseed,
                        AlchemyData.rareseed,
                        AlchemyData.rareseed,
                        AlchemyData.starfruitseed,
                        AlchemyData.starfruitseed,
                        AlchemyData.pineappleseed,
                        AlchemyData.pineappleseed,
                        AlchemyData.powdermelonseed,
                        AlchemyData.powdermelonseed,
                    };

                    itemId = rares[adjusttwo];

                    exchange = 0.002f;

                    break;

                case AlchemyRecipe.recipes.convert_mixedseed:

                    itemId = AlchemyData.mixedSeed;

                    exchange = 0.02f;

                    break;

                case AlchemyRecipe.recipes.convert_flowerseed:

                    itemId = AlchemyData.flowerSeed;

                    exchange = 0.04f;

                    break;

                case AlchemyRecipe.recipes.convert_wildseed:

                    itemId = AlchemyData.winterSeed;

                    switch (Game1.season)
                    {

                        case Season.Spring:

                            itemId = AlchemyData.springSeed;

                            break;

                        case Season.Summer:

                            itemId = AlchemyData.summerSeed;

                            break;

                        case Season.Fall:

                            itemId = AlchemyData.fallSeed;

                            break;

                    }

                    exchange = 0.03f;

                    break;

                case AlchemyRecipe.recipes.convert_bombs:

                    List<string> bombs = new()
                    {
                        AlchemyData.cherrybomb,
                        AlchemyData.cherrybomb,
                        AlchemyData.cherrybomb,
                        AlchemyData.bomb,
                        AlchemyData.bomb,
                        AlchemyData.bomb,
                        AlchemyData.bomb,
                        AlchemyData.megabomb,
                        AlchemyData.megabomb,
                        AlchemyData.megabomb,
                    };

                    itemId = bombs[adjusttwo];

                    exchange = 0.01f;

                    break;

                case AlchemyRecipe.recipes.convert_caffeine:

                    List<string> caffeine = new()
                    {
                        AlchemyData.coffee,
                        AlchemyData.coffee,
                        AlchemyData.coffee,
                        AlchemyData.coffee,
                        AlchemyData.jojacola,
                        AlchemyData.jojacola,
                        AlchemyData.greentea,
                        AlchemyData.greentea,
                        AlchemyData.tripleshot,
                        AlchemyData.tripleshot
                    };

                    itemId = caffeine[adjusttwo];

                    exchange = 0.0250f;

                    break;

                case AlchemyRecipe.recipes.convert_geodes:

                    exchange = 0.0125f;

                    itemId = AlchemyData.omnigeode;

                    break;

                default:
                case AlchemyRecipe.recipes.convert_gold:

                    break;

            }

            int amount = (int)((float)convert * exchange * (1 + 0.05 * adjust));

            amount = Math.Max(amount, 1);

            amount *= product.processed;

            StardewValley.Object gold = new StardewValley.Object(itemId, amount);

            product.inventoryItem = gold;

            return product;

        }

        public int RandomGameIndex(items item, int offset = 0)
        {

            int used = 1;

            if (Mod.instance.save.apothecary.ContainsKey(item))
            {

                used = Mod.instance.save.apothecary[item].total - Mod.instance.save.apothecary[item].amount + 1;

            }

            Random random = Utility.CreateDaySaveRandom(used);

            if (offset > 0)
            {

                for (int i = 0; i < offset; i++) 
                { 
                    
                    random.Next(10); 
                
                }
            
            }

            return random.Next(10);

        }

        public AlchemyProduct UpdateInventoryItem(AlchemyRecipe.recipes recipe, int slot = -1, int intent = 1)
        {

            AlchemyRecipe recipeData = recipes[recipe];

            AlchemyProduct product = new();

            items consume = recipes[recipe].items.First();

            int consumable = Mod.instance.save.apothecary[consume].amount;

            List<int> available = new()
            {

                consumable,
            
            };

            product.totalApothecary[consume] = consumable;

            product.slot = slot;

            if (slot == -1)
            {

                return product;

            }

            product.totalSlot[slot] = 0;

            Dictionary<AlchemyRecipe.qualifiers, int> qualifiers = AnalyseItem(Game1.player.Items[slot]);

            if (!qualifiers.ContainsKey(AlchemyRecipe.qualifiers.validobject))
            {

                return product;

            }

            StardewValley.Object obj = Game1.player.Items[slot] as StardewValley.Object;

            product.totalSlot[slot] = obj.Stack;

            if (consumable <= 0)
            {

                return product;

            }

            switch (recipe)
            {

                case AlchemyRecipe.recipes.update_cooking:

                    if (!qualifiers.ContainsKey(AlchemyRecipe.qualifiers.cooking))
                    {

                        return product;

                    }

                    break;

                case AlchemyRecipe.recipes.update_artisanal:

                    if (!qualifiers.ContainsKey(AlchemyRecipe.qualifiers.artisan))
                    {

                        return product;

                    }

                    break;

                case AlchemyRecipe.recipes.update_produce:

                    if (!qualifiers.ContainsKey(AlchemyRecipe.qualifiers.produce))
                    {

                        return product;

                    }

                    break;

            }
            
            switch (recipe)
            {

                case AlchemyRecipe.recipes.update_cooking:
                case AlchemyRecipe.recipes.update_artisanal:
                case AlchemyRecipe.recipes.update_produce:

                    if(obj.Quality >= 4)
                    {

                        return product;

                    }

                    available.Add(obj.Stack);

                    product.repeatable = available.Min();

                    available.Add(intent);

                    product.processed = available.Min();

                    StardewValley.Object updated = obj.getOne() as StardewValley.Object;

                    updated.Stack = product.processed;

                    updated.Quality = 4;

                    product.inventoryItem = updated;

                    product.fromSlot[slot] = product.processed;

                    break;

                case AlchemyRecipe.recipes.update_stack:

                    if (!qualifiers.ContainsKey(AlchemyRecipe.qualifiers.stackable))
                    {

                        return product;

                    }

                    available.Add(999 - obj.Stack);

                    available.Add(obj.Stack);

                    product.repeatable = available.Min();

                    available.Add(intent);

                    product.processed = available.Min();

                    StardewValley.Object stacked = obj.getOne() as StardewValley.Object;

                    stacked.Stack = product.processed;

                    product.inventoryItem = stacked;

                    break;

            }

            

            return product;

        }

        public static void ReduceSlot(int slot, int amount)
        {

            Game1.player.Items[slot] = Game1.player.Items[slot].ConsumeStack(amount);

            Game1.player.Items.RemoveEmptySlots();

        }

        public AlchemyProduct UseRecipe(AlchemyRecipe.recipes recipe, int slot = -1, int intent = 1)
        {

            AlchemyProduct product = GetProduct(recipe, slot, intent);

            if(product.processed == 0)
            {

                return product;

            }

            if(product.apothecaryItem != items.none)
            {

                UpdateAmounts(product.apothecaryItem, product.processed);

            }

            if (product.inventoryItem != null)
            {

                if (!Game1.player.addItemToInventoryBool(product.inventoryItem))
                {

                    Game1.player.dropItem(product.inventoryItem);

                }

            }

            foreach (KeyValuePair<items, int> item in product.fromApothecary)
            {

                UpdateAmounts(item.Key, 0 - product.processed);

            }

            foreach (KeyValuePair<string, int> ingredient in product.fromInventory)
            {

                Game1.player.Items.ReduceId(ingredient.Key, ingredient.Value);

            }

            foreach (KeyValuePair<int, int> slotIngredient in product.fromSlot)
            {

                ReduceSlot(slotIngredient.Key, slotIngredient.Value);

            }

            return product;

        }

        // =========================================================== Interactions

        public void ConvertGeodes()
        {

            if (Context.IsMainPlayer)
            {
                
                ChestHandle.RetrieveInventory(ChestHandle.chests.Anvil);

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

            Vector2 origin = new(0);

            if (Game1.currentLocation is Grove grove)
            {

                origin = grove.anvilPosition;

            }
            else
            {

                return;

            }

            foreach (KeyValuePair<string, Item> extract in extracts)
            {

                if (!Context.IsMainPlayer)
                {

                    ThrowHandle throwExtract = new(Game1.player, origin, extract.Value);

                    throwExtract.register();

                    continue;

                }

                if (Mod.instance.chests[ChestHandle.chests.Anvil].addItem(extract.Value) != null)
                {
                    ThrowHandle throwExtract = new(Game1.player, origin, extract.Value);

                    throwExtract.register();

                }

            }

            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.Spells.greatbolt, displayFactor = 3, sound = SpellHandle.Sounds.thunder, });

            Rectangle relicRect = IconData.RelicRectangles(IconData.relics.druid_hammer);

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

            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.supree, new()) { type = SpellHandle.Spells.effect, counter = -15, sound = SpellHandle.Sounds.yoba, displayRadius = 2, });

            Mod.instance.spellRegister.Add(new(origin, 256, IconData.impacts.supree, new()) { type = SpellHandle.Spells.effect, counter = -60, instant = true, displayRadius = 3, scheme = IconData.schemes.mists, sound = SpellHandle.Sounds.secret1 });

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

                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.Spells.greatbolt, displayFactor = 3, sound = SpellHandle.Sounds.thunder, });

                    Rectangle relicRect = IconData.RelicRectangles(IconData.relics.druid_hammer);

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

                    Mod.instance.spellRegister.Add(new(origin, 256, IconData.impacts.supree, new()) { type = SpellHandle.Spells.effect, displayRadius = 3, counter = -15, sound = SpellHandle.Sounds.yoba });

                    Mod.instance.spellRegister.Add(new(origin, 320, IconData.impacts.supree, new()) { type = SpellHandle.Spells.effect, displayRadius = 4, counter = -60, instant = true, scheme = IconData.schemes.mists, sound = SpellHandle.Sounds.secret1 });

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

        public items BestHerbal(items line)
        {

            List<items> list = new()
            {
                items.optimus_ligna,
                items.magnus_ligna,
                items.satius_ligna,
                items.ligna,
            };

            switch (line)
            {
                case items.vigores:
                    list = new()
                    {
                        items.optimus_vigores,
                        items.magnus_vigores,
                        items.satius_vigores,
                        items.vigores,
                    };
                    break;

                case items.celeri:
                    list = new()
                    {
                        items.optimus_celeri,
                        items.magnus_celeri,
                        items.satius_celeri,
                        items.celeri,
                    };
                    break;

            }

            foreach (items item in list)
            {

                if (GetAmount(item) > 0)
                {

                    return item;

                }

            }

            return items.none;

        }

        public static items RandomApothecaryItem(Vector2 position)
        {

            items loot = (items)((int)items.omen_feather + Mod.instance.randomIndex.Next(30));

            new ThrowHandle(Game1.player, position, loot, 1).register();

            return loot;

        }

        public static void RandomOmen(Vector2 position, int chance = 8)
        {

            if (Mod.instance.randomIndex.Next(chance) != 0)
            {

                return;

            }

            items omen = (items)((int)items.omen_feather + Mod.instance.randomIndex.Next(15));
        
            new ThrowHandle(Game1.player, position, omen, 1).register();

        }


        public static void RandomTrophy(Vector2 position, int chance = 8)
        {

            if (Mod.instance.randomIndex.Next(chance) != 0)
            {

                return;

            }

            items trophy = (items)((int)items.trophy_shroom + Mod.instance.randomIndex.Next(15));

            new ThrowHandle(Game1.player, position, trophy, 1).register();

        }        

    }

}
