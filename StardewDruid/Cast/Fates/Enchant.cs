using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Events;
using StardewValley.Extensions;
using StardewValley.GameData.GiantCrops;
using StardewValley.GameData.Machines;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StardewDruid.Cast.Fates
{
    internal class Enchant : Event.EventHandle
    {


        public int radialCounter = 0;

        public int faeth = 0;

        public List<Vector2> giantTiles = new();

        public Enchant()
        {


        }

        public override bool EventActive()
        {

            if (!inabsentia && !eventLocked)
            {

                if (!Mod.instance.RiteButtonHeld())
                {

                    return false;

                }

                if (Vector2.Distance(origin, Game1.player.Position) > 32 && !Mod.instance.ShiftButtonHeld())
                {

                    return false;

                }

            }

            return base.EventActive();

        }

        public override void EventDecimal()
        {

            if (!EventActive())
            {

                RemoveAnimations();

                return;

            }

            if (!inabsentia && !eventLocked)
            {

                decimalCounter++;

                if (decimalCounter == 5)
                {

                    Mod.instance.rite.Channel(IconData.skies.temple, 75);

                    channel = IconData.skies.temple;

                }

                if (decimalCounter == 15)
                {

                    eventLocked = true;

                    Mod.instance.spellRegister.Add(new(origin, 160, IconData.impacts.supree, new()) { sound = SpellHandle.Sounds.getNewSpecialItem, });

                    SpellHandle spellHandle = new(origin, 128, IconData.impacts.summoning, new())
                    {
                        scheme = IconData.schemes.fates
                    };

                    Mod.instance.spellRegister.Add(spellHandle);

                }

                return;

            }

            if (radialCounter % 2 == 0)
            {
                
                if (!inabsentia)
                {
                    
                    Enchantment();

                }
                else
                {

                    KissedByTheStars();

                }

            }

            radialCounter++;

            if (radialCounter == 18)
            {

                eventComplete = true;

            }

        }

        public override void EventRemove()
        {

            base.EventRemove();

            if (faeth > 0)
            {

                Herbal resource = Mod.instance.herbalData.herbalism[HerbalHandle.herbals.faeth.ToString()];

                string message = "-" + faeth + " " + resource.title;

                DisplayPotion hudmessage = new(message, resource);

                Game1.addHUDMessage(hudmessage);

                costCounter = faeth * 12;

                Rite.ApplyCost(costCounter);

                if (!Mod.instance.questHandle.IsComplete(QuestHandle.fatesFour))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.fatesFour, faeth);

                }

            }

        }

        public void Enchantment()
        {

            if (!Mod.instance.rite.targetCasts.ContainsKey(location.Name + "_enchant"))
            {

                Mod.instance.rite.targetCasts[location.Name + "_enchant"] = new();

            }

            List<Vector2> affected = ModUtility.GetTilesWithinRadius(location, ModUtility.PositionToTile(origin), radialCounter / 2);

            foreach (Vector2 tile in affected)
            {
                
                if (HerbalHandle.GetHerbalism(HerbalHandle.herbals.faeth) == 0)
                {

                    break;

                }

                if (Mod.instance.rite.targetCasts[location.Name + "_enchant"].ContainsKey(tile))
                {

                    continue;

                }

                if (location.terrainFeatures.ContainsKey(tile))
                {

                    if (location.terrainFeatures[tile] is HoeDirt hoeDirt)
                    {

                        if (FairyDustCrop(hoeDirt))
                        {

                            HerbalHandle.UpdateHerbalism(HerbalHandle.herbals.faeth,-1);

                            faeth += 1;

                            Mod.instance.rite.targetCasts[location.Name + "_enchant"][tile] = "crop";


                        };

                        if (giantTiles.Contains(tile))
                        {

                            GiantDustCrop(hoeDirt);

                            HerbalHandle.UpdateHerbalism(HerbalHandle.herbals.faeth, -1);

                            faeth += 1;

                            Mod.instance.rite.targetCasts[location.Name + "_enchant"][tile] = "giant";

                        }

                        continue;

                    }

                }

                if (!location.objects.ContainsKey(tile))
                {

                    continue;

                }

                StardewValley.Object target = location.objects[tile];

                if (!target.HasTypeBigCraftable())
                {

                    continue;

                }

                /*switch (target.name)
                {

                    case "Deconstructor":
                    case "Dehydrator":
                    case "Bone Mill":
                    case "Keg":
                    case "Preserves Jar":
                    case "Cheese Press":
                    case "Mayonnaise Machine":
                    case "Loom":
                    case "Oil Maker":
                    case "Furnace":
                    case "Heavy Furnace":
                    case "Geode Crusher":
                    case "Fish Smoker":
                    case "Bee House":

                        // registered for use

                        break;

                    default:

                        continue;

                }*/


                if (target.MinutesUntilReady > 2)
                {

                    if (target.TryApplyFairyDust())
                    {

                        Mod.instance.rite.targetCasts[location.Name + "_enchant"][tile] = target.name;

                        HerbalHandle.UpdateHerbalism(HerbalHandle.herbals.faeth, -1);

                        Vector2 fairyVector = tile * 64 + new Vector2(32, 32);

                        Microsoft.Xna.Framework.Color fairyColour = Mod.instance.iconData.gradientColours[IconData.schemes.fates][Mod.instance.randomIndex.Next(3)];

                        Mod.instance.iconData.ImpactIndicator(
                            Game1.player.currentLocation,
                            fairyVector,
                            IconData.impacts.glare,
                            4f,
                            new() { alpha = 0.65f, color = fairyColour, rotation = Mod.instance.randomIndex.Next(4) * 0.5f, });

                        faeth++;

                    }

                    continue;

                }

                if (target.heldObject.Value != null)
                {

                    ThrowHandle throwIt = new(Game1.player, target.TileLocation * 64, target.heldObject.Value);

                    throwIt.register();

                    target.heldObject.Set(null);

                    target.MinutesUntilReady = 0;

                    target.readyForHarvest.Set(false);

                    target.performDropDownAction(Game1.player);

                }

                int cost = 1;

                switch (target.name)
                {

                    case "Deconstructor": 
                        
                        FillDeconstructor(target); 
                        
                        break;

                    case "Dehydrator": 
                        
                        FillDehydrator(target); 
                        
                        break;

                    case "Bone Mill": 
                        
                        FillBoneMill(target); 
                        
                        break;

                    case "Keg": 
                        
                        FillKeg(target); 
                        
                        break;

                    case "Preserves Jar": 
                        
                        FillPreservesJar(target); 
                        
                        break;

                    case "Cheese Press": 
                        
                        FillCheesePress(target); 
                        
                        break;

                    case "Mayonnaise Machine": 
                        
                        FillMayonnaiseMachine(target); 
                        
                        break;

                    case "Loom": 
                        
                        FillLoom(target); 
                        
                        break;

                    case "Oil Maker": 
                        
                        FillOilMaker(target); 
                        
                        break;

                    case "Furnace":

                        FillFurnace(target);

                        break;

                    case "Heavy Furnace":

                        if (HerbalHandle.GetHerbalism(HerbalHandle.herbals.faeth) < 3)
                        {

                            continue;

                        }

                        cost = 3;

                        FillFurnace(target, 25, cost);

                        break;

                    case "Fish Smoker":

                        FillFishSmoker(target);

                        break;

                    case "Geode Crusher": 
                        
                        FillGeodeCrusher(target); 
                        
                        break;

                    default:

                        FillUniversal(target);

                        break;

                }

                Mod.instance.rite.targetCasts[location.Name + "_enchant"][tile] = target.name;

                if (target.heldObject.Value == null)
                {

                    continue;

                }

                HerbalHandle.UpdateHerbalism(HerbalHandle.herbals.faeth, 0 - cost);

                Vector2 cursorVector = tile * 64 + new Vector2(32, 32);

                Microsoft.Xna.Framework.Color colour = Mod.instance.iconData.gradientColours[IconData.schemes.fates][Mod.instance.randomIndex.Next(3)];

                Mod.instance.iconData.ImpactIndicator(
                    Game1.player.currentLocation,
                    cursorVector,
                    IconData.impacts.glare,
                    4f,
                    new() { alpha = 0.65f, color = colour, rotation = Mod.instance.randomIndex.Next(4) * 0.5f, });

                faeth += cost;

            }

        }

        public void FillUniversal(StardewValley.Object targetObject)
        {

            MachineData machineData = targetObject.GetMachineData();

            if(machineData == null || !machineData.HasInput)
            {

                return;

            }

            Dictionary<string, StardewValley.Item> possibleInputs = new();

            foreach (MachineOutputRule outputRule in machineData.OutputRules)
            {

                foreach(MachineOutputTriggerRule triggerRule in outputRule.Triggers)
                {

                    StardewValley.Item requiredInput = ItemRegistry.Create(triggerRule.RequiredItemId, triggerRule.RequiredCount);

                    possibleInputs[requiredInput.ItemId] = requiredInput;

                }

            }

            foreach(MachineItemAdditionalConsumedItems additionalInput in machineData.AdditionalConsumedItems)
            {

                StardewValley.Item extra = ItemRegistry.Create(additionalInput.ItemId, additionalInput.RequiredCount);

                Game1.player.addItemToInventory(extra);

            }

            StardewValley.Item input = possibleInputs.ElementAt(Mod.instance.randomIndex.Next(possibleInputs.Count)).Value;

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillDeconstructor(StardewValley.Object targetObject)
        {

            MachineData machineData = targetObject.GetMachineData();

            KeyValuePair<string, string> craftingRecipe = CraftingRecipe.craftingRecipes.ElementAt(Mod.instance.randomIndex.Next(CraftingRecipe.craftingRecipes.Count));

            CraftingRecipe newThing = new(craftingRecipe.Key);

            targetObject.PlaceInMachine(machineData, ItemRegistry.Create(newThing.itemToProduce.FirstOrDefault(), 1), false, Game1.player);

        }

        public void FillDehydrator(StardewValley.Object targetObject)
        {

            MachineData machineData = targetObject.GetMachineData();

            Dictionary<int, int> cropList = new()
            {

                [0] = 24,
                [1] = 188,
                [2] = 190,
                [3] = 248,
                [4] = 250,
                [5] = 256,
                [6] = 264,
                [7] = 266,
                [8] = 272,
                [9] = 274,
                [10] = 276,
                [11] = 278,
                [12] = 280,
                [13] = 284,
                [14] = 300,
                [15] = 304,
                [16] = 830,
                [17] = 259,
                [18] = 270,
                [19] = 486,
                [20] = 262,
                [21] = 304,
                [22] = 91,
                [23] = 832,
                [24] = 834,
                [25] = 634,
                [26] = 635,
                [27] = 636,
                [28] = 637,
                [29] = 638,
                [30] = 613,
                [31] = 400,
                [32] = 398,
                [33] = 282,
                [34] = 260,
                [35] = 258,
                [36] = 254,
                [37] = 252,
                [38] = 88,
                [39] = 90

            };

            int cropIndex = cropList[Mod.instance.randomIndex.Next(cropList.Count)];

            StardewValley.Object input = new(cropIndex.ToString(), 5);

            if (input == null) { location.playSound("ghost"); return; }

            switch (@input.Category)
            {

                default:

                    input = new("404", 5);

                    break;

                case -79:

                    break;

            }

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillBoneMill(StardewValley.Object targetObject)
        {

            StardewValley.Item input = ItemRegistry.Create("(O)580", 1);

            if (input == null) { location.playSound("ghost"); return; }

            MachineData machineData = targetObject.GetMachineData();

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillKeg(StardewValley.Object targetObject)
        {

            Dictionary<int, int> cropList = new()
            {

                [0] = 24,
                [1] = 188,
                [2] = 190,
                [3] = 248,
                [4] = 250,
                [5] = 256,
                [6] = 264,
                [7] = 266,
                [8] = 272,
                [9] = 274,
                [10] = 276,
                [11] = 278,
                [12] = 280,
                [13] = 284,
                [14] = 300,
                [15] = 304,
                [16] = 830,
                [17] = 259,
                [18] = 270,
                [19] = 486,
                [20] = 262,
                [21] = 304,
                [22] = 815,
                [23] = 340,
                [24] = 91,
                [25] = 832,
                [26] = 834,
                [27] = 634,
                [28] = 635,
                [29] = 636,
                [30] = 637,
                [31] = 638,
                [32] = 613,
                [33] = 400,
                [34] = 398,
                [35] = 282,
                [36] = 260,
                [37] = 258,
                [38] = 254,
                [39] = 252,
                [40] = 88,
                [41] = 90

            };

            int cropIndex = cropList[Mod.instance.randomIndex.Next(cropList.Count)];

            MachineData machineData = targetObject.GetMachineData();

            StardewValley.Item input = ItemRegistry.Create("(O)" + cropIndex.ToString(), 1);

            if (input == null) { location.playSound("ghost"); return; }

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillPreservesJar(StardewValley.Object targetObject)
        {

            Dictionary<int, int> cropList = new()
            {

                [0] = 24,
                [1] = 188,
                [2] = 190,
                [3] = 248,
                [4] = 250,
                [5] = 256,
                [6] = 264,
                [7] = 266,
                [8] = 272,
                [9] = 274,
                [10] = 276,
                [11] = 278,
                [12] = 280,
                [13] = 284,
                [14] = 300,
                [15] = 304,
                [16] = 830,
                [17] = 259,
                [18] = 270,
                [19] = 486,
                [20] = 262,
                [21] = 304,
                [22] = 91,
                [23] = 832,
                [24] = 834,
                [25] = 634,
                [26] = 635,
                [27] = 636,
                [28] = 637,
                [29] = 638,
                [30] = 613,
                [31] = 400,
                [32] = 398,
                [33] = 282,
                [34] = 260,
                [35] = 258,
                [36] = 254,
                [37] = 252,
                [38] = 88,
                [39] = 90

            };

            int cropIndex = cropList[Mod.instance.randomIndex.Next(cropList.Count)];

            MachineData machineData = targetObject.GetMachineData();

            StardewValley.Item input = ItemRegistry.Create("(O)" + cropIndex.ToString(), 1);

            if (input == null) { location.playSound("ghost"); return; }

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillCheesePress(StardewValley.Object targetObject)
        {

            string milk = Mod.instance.randomIndex.Next(2) == 0 ? "(0)186" : "(O)438";

            StardewValley.Item input = ItemRegistry.Create(milk, 1);

            if (input == null) { location.playSound("ghost"); return; }

            MachineData machineData = targetObject.GetMachineData();

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillMayonnaiseMachine(StardewValley.Object targetObject)
        {

            List<int> eggList = new()
            {

                289,
                174,
                182,
                176,
                180,
                442,
                305,
                107,
                928,

            };

            int eggIndex = eggList[Mod.instance.randomIndex.Next(eggList.Count)];

            MachineData machineData = targetObject.GetMachineData();

            StardewValley.Item input = ItemRegistry.Create("(O)" + eggIndex.ToString(), 1);

            if (input == null) { location.playSound("ghost"); return; }

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillLoom(StardewValley.Object targetObject)
        {

            StardewValley.Item input = ItemRegistry.Create("(O)440", 1);

            if (input == null) { location.playSound("ghost"); return; }

            MachineData machineData = targetObject.GetMachineData();

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillOilMaker(StardewValley.Object targetObject)
        {

            List<int> oilList = new()
            {

                270,
                421,
                430,
                431,

            };

            int oilIndex = oilList[Mod.instance.randomIndex.Next(oilList.Count)];

            StardewValley.Item input = ItemRegistry.Create("(O)" + oilIndex.ToString(), 1);

            if (input == null) { location.playSound("ghost"); return; }

            MachineData machineData = targetObject.GetMachineData();

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillFurnace(StardewValley.Object targetObject, int ore = 5, int coal = 1)
        {

            List<int> furnaceList = new()
            {

                378,
                378,
                380,
                380,
                384,
                386,
                80,

            };

            /*if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeEther))
            {
                furnaceList.Add(909);
            }*/

            int furnaceIndex = furnaceList[Mod.instance.randomIndex.Next(furnaceList.Count)];

            StardewValley.Item extra = ItemRegistry.Create("(O)382", coal);

            Game1.player.addItemToInventory(extra);

            StardewValley.Item input = ItemRegistry.Create("(O)" + furnaceIndex.ToString(), ore);

            if (input == null) { location.playSound("ghost"); return; }

            MachineData machineData = targetObject.GetMachineData();

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillGeodeCrusher(StardewValley.Object targetObject)
        {

            MachineData machineData = targetObject.GetMachineData();

            StardewValley.Item input = ItemRegistry.Create("(O)749", 1);

            if (input == null) { location.playSound("ghost"); return; }

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public void FillFishSmoker(StardewValley.Object targetObject)
        {

            MachineData machineData = targetObject.GetMachineData();

            StardewValley.Item input = ItemRegistry.Create("(O)" + SpawnData.RandomHighFish(location, Game1.player.Tile, true, Mod.instance.randomIndex.Next(2)));

            if (input == null) { location.playSound("ghost"); return; }

            StardewValley.Item extra = ItemRegistry.Create("(O)382", 1);

            Game1.player.addItemToInventory(extra);

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

        public bool FairyDustCrop(StardewValley.TerrainFeatures.HoeDirt hoeDirt)
        {

            if (hoeDirt.crop == null)
            {

                return false;

            }

            bool regrowable = false;

            int maxGrowth = hoeDirt.crop.phaseDays.Count - 1;

            if (hoeDirt.crop.RegrowsAfterHarvest())
            {

                regrowable = true;

            }

            if (hoeDirt.crop.currentPhase.Value < maxGrowth)
            {

                hoeDirt.crop.currentPhase.Value += 1;

                hoeDirt.crop.dayOfCurrentPhase.Value = 0;

                hoeDirt.crop.updateDrawMath(hoeDirt.crop.tilePosition);

                if (hoeDirt.crop.currentPhase.Value == maxGrowth && regrowable)
                {

                    hoeDirt.crop.fullyGrown.Set(true);

                }

                location.playSound("dirtyHit");

                return true;

            }
            else
            if (regrowable)
            {

                if (!hoeDirt.crop.fullyGrown.Value)
                {

                    hoeDirt.crop.fullyGrown.Set(true);

                    location.playSound("dirtyHit");

                }

                return true;

            }

            return false;

        }

        public bool GiantDustCrop(StardewValley.TerrainFeatures.HoeDirt hoeDirt)
        {
            
            if (hoeDirt.crop == null)
            {

                return false;

            }

            if (hoeDirt.crop.currentPhase.Value < hoeDirt.crop.phaseDays.Count - 1)
            {

                return false;

            }

            if (!(location is Farm || location.HasMapPropertyWithValue("AllowGiantCrops")))
            {

                return false;

            }

            if (!hoeDirt.crop.TryGetGiantCrops(out var giantCrops))
            {

                return false;

            }

            foreach (KeyValuePair<string, GiantCropData> item in giantCrops)
            {

                string key = item.Key;

                GiantCropData value = item.Value;

                if (!GameStateQuery.CheckConditions(value.Condition, location))
                {

                    continue;

                }

                bool flag = true;

                List<Vector2> neighbours = ModUtility.GetTilesWithinRadius(location, hoeDirt.Tile, 1);

                foreach (Vector2 neighbour in neighbours)
                {

                    if (!location.terrainFeatures.ContainsKey(neighbour))
                    {

                        flag = false;

                        break;

                    }

                    if (location.terrainFeatures[neighbour] is not HoeDirt hoeDirtNeighbour)
                    {

                        flag = false;

                        break;

                    }

                    if (hoeDirtNeighbour.crop == null)
                    {

                        flag = false;

                        break;

                    }

                    if (hoeDirtNeighbour.crop.indexOfHarvest != hoeDirt.crop.indexOfHarvest)
                    {

                        flag = false;

                        break;

                    }

                }

                if (!flag)
                {

                    continue;

                }

                foreach (Vector2 neighbour in neighbours)
                {

                    if (location.terrainFeatures[neighbour] is HoeDirt hoeDirtNeighbour)
                    {

                        hoeDirtNeighbour.crop = null;

                    }

                }

                hoeDirt.crop = null;

                location.resourceClumps.Add(new GiantCrop(key, hoeDirt.Tile - new Vector2(1)));

                location.playSound(SpellHandle.Sounds.yoba.ToString());

                return true;

            }

            return false;

        }

        public void KissedByTheStars()
        {

            if (!Mod.instance.rite.targetCasts.ContainsKey(location.Name + "_enchant"))
            {

                Mod.instance.rite.targetCasts[location.Name + "_enchant"] = new();

            }

            List<Vector2> affected = ModUtility.GetTilesWithinRadius(location, ModUtility.PositionToTile(origin), radialCounter / 2);

            foreach (Vector2 tile in affected)
            {

                if (Mod.instance.rite.targetCasts[location.Name + "_enchant"].ContainsKey(tile))
                {

                    continue;

                }

                if (location.terrainFeatures.ContainsKey(tile))
                {

                    if (location.terrainFeatures[tile] is HoeDirt hoeDirt)
                    {

                        FairyDustCrop(hoeDirt);

                        if (giantTiles.Contains(tile))
                        {

                            GiantDustCrop(hoeDirt);

                        }

                    }

                    continue;

                }

            }

        }

    }

}
