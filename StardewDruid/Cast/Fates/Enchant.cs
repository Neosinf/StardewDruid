using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Extensions;
using StardewValley.GameData.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using static StardewDruid.Journal.HerbalData;


namespace StardewDruid.Cast.Fates
{
    internal class Enchant : Event.EventHandle
    {


        public int radialCounter = 0;

        public int faeth = 0;

        public Enchant()
        {

        }

        public override bool EventActive()
        {

            if (!inabsentia && !eventLocked)
            {

                if (Mod.instance.Config.riteButtons.GetState() != SButtonState.Held)
                {

                    return false;

                }

                if (Vector2.Distance(origin, Game1.player.Position) > 32)
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

                    Mod.instance.rite.channel(IconData.skies.sunset, 75);

                    channel = IconData.skies.sunset;

                }

                if (decimalCounter == 15)
                {

                    eventLocked = true;

                    SpellHandle spellHandle = new(origin, 320, IconData.impacts.nature, new());

                    spellHandle.scheme = IconData.schemes.fates;

                    Mod.instance.spellRegister.Add(spellHandle);

                }

                return;

            }

            if(radialCounter % 2 == 0)
            {

                Enchantment();

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

                Herbal resource = Mod.instance.herbalData.herbalism[herbals.faeth.ToString()];

                string message = "-" + faeth + " " + resource.title;

                DisplayPotion hudmessage = new(message, resource);

                Game1.addHUDMessage(hudmessage);

                Mod.instance.rite.castCost *= faeth * 12;

                Mod.instance.rite.ApplyCost();

                if (!Mod.instance.questHandle.IsComplete(QuestHandle.fatesFour))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.fatesFour, faeth);

                }

            }
  
        }

        public void Enchantment()
        {

            if (!Mod.instance.rite.targetCasts.ContainsKey(location.Name))
            {

                Mod.instance.rite.targetCasts[location.Name] = new();

            }

            List<Vector2> affected = ModUtility.GetTilesWithinRadius(location, ModUtility.PositionToTile(origin), radialCounter/2);

            foreach (Vector2 tile in affected)
            {

                if (!Mod.instance.save.herbalism.ContainsKey(HerbalData.herbals.faeth))
                {

                    break;

                }
                
                if (Mod.instance.save.herbalism[HerbalData.herbals.faeth] == 0)
                {

                    break;

                }

                if (Mod.instance.rite.targetCasts[location.Name].ContainsKey(tile))
                {

                    continue;

                }

                if (!location.objects.ContainsKey(tile))
                {

                    continue;

                }

                if (!location.objects[tile].HasTypeBigCraftable())
                {

                    continue;

                }

                StardewValley.Object target = location.objects[tile];


                switch (target.name)
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

                }


                if (target.MinutesUntilReady > 0)
                {

                    Utility.addSprinklesToLocation(location, (int)tile.X, (int)tile.Y, 1, 2, 400, 40, Color.White);

                    target.MinutesUntilReady = 10;

                    DelayedAction.functionAfterDelay(delegate { target.minutesElapsed(10); }, 50);

                    Mod.instance.rite.targetCasts[location.Name][tile] = target.name;

                    Mod.instance.save.herbalism[HerbalData.herbals.faeth] -= 1;

                    Vector2 cursorVector = tile * 64 + new Vector2(32, 32);

                    Microsoft.Xna.Framework.Color colour = Mod.instance.iconData.gradientColours[IconData.schemes.fates][Mod.instance.randomIndex.Next(3)];

                    Mod.instance.iconData.ImpactIndicator(location, cursorVector, IconData.impacts.glare, 0.75f + Mod.instance.randomIndex.Next(5) * 0.25f, new() { color = colour, });

                    faeth++;

                }
                else 
                {
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

                        case "Deconstructor": FillDeconstructor(target); break;

                        case "Dehydrator": FillDehydrator(target); break;

                        case "Bone Mill": FillBoneMill(target); break;

                        case "Keg": FillKeg(target); break;

                        case "Preserves Jar": FillPreservesJar(target); break;

                        case "Cheese Press": FillCheesePress(target); break;

                        case "Mayonnaise Machine": FillMayonnaiseMachine(target); break;

                        case "Loom": FillLoom(target); break;

                        case "Oil Maker": FillOilMaker(target); break;

                        case "Furnace": 
                            
                            FillFurnace(target); 
                            
                            break;

                        case "Heavy Furnace": 
                            
                            if(Mod.instance.save.herbalism[HerbalData.herbals.faeth] < 3)
                            { 
                                
                                continue; 
                            
                            } 
                            
                            cost = 3;  
                            
                            FillFurnace(target, 25, cost); 
                            
                            break;

                        case "Fish Smoker":

                            FillFishSmoker(target);

                            break;

                        case "Geode Crusher": FillGeodeCrusher(target); break;

                        default:

                            continue;

                    }

                    if(target.heldObject.Value == null)
                    {

                        continue;

                    }

                    Mod.instance.rite.targetCasts[location.Name][tile] = target.name;

                    Mod.instance.save.herbalism[HerbalData.herbals.faeth] -= cost;

                    Vector2 cursorVector = tile * 64 + new Vector2(32, 32);

                    Microsoft.Xna.Framework.Color colour = Mod.instance.iconData.gradientColours[IconData.schemes.fates][Mod.instance.randomIndex.Next(3)];

                    Mod.instance.iconData.ImpactIndicator(location, cursorVector, IconData.impacts.glare, 0.75f + Mod.instance.randomIndex.Next(5) * 0.25f, new() { color = colour, });

                    faeth += cost;

                }
 
            }

        }

        public void FillDeconstructor(StardewValley.Object targetObject)
        {

            MachineData machineData = targetObject.GetMachineData();

            KeyValuePair<string, string> craftingRecipe = CraftingRecipe.craftingRecipes.ElementAt(Mod.instance.randomIndex.Next(CraftingRecipe.craftingRecipes.Count));

            CraftingRecipe newThing = new(craftingRecipe.Key);

            targetObject.PlaceInMachine(machineData, ItemRegistry.Create(newThing.itemToProduce.FirstOrDefault(),1), false, Game1.player);

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
                380,
                384,
                386,
                80,
                82,
                909,

            };

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

            StardewValley.Item input = ItemRegistry.Create("(O)"+ SpawnData.RandomHighFish(location,true,Mod.instance.randomIndex.Next(3)), 1);

            if (input == null) { location.playSound("ghost"); return; }

            StardewValley.Item extra = ItemRegistry.Create("(O)382", 1);

            Game1.player.addItemToInventory(extra);

            targetObject.PlaceInMachine(machineData, input, false, Game1.player);

        }

    }

}
