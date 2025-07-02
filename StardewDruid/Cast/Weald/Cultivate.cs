using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Objects;
using StardewValley.Tools;
using System.Collections.Generic;
using System.Linq;
using xTile.Tiles;

namespace StardewDruid.Cast.Weald
{
    public class Cultivate : EventHandle
    {

        public int radialCounter = 0;

        public StardewValley.Inventories.Inventory inventory = new();

        public List<string> ignore = new();

        public Farmer puppet = null;

        public List<string> conversions = new();

        public List<string> imports = new();

        public Dictionary<Vector2, string> plots = new();

        public Cultivate()
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

            if (!eventLocked)
            {

                decimalCounter++;

                if (inabsentia)
                {

                    if (decimalCounter == 1)
                    {

                        CultivateConversions();

                    }

                    if (decimalCounter == 2)
                    {

                        CultivateImports();

                    }

                    if(decimalCounter == 3)
                    {

                        Plot();

                        eventLocked = true;

                    }

                    return;

                }

                if (decimalCounter == 5)
                {

                    Mod.instance.rite.Channel(IconData.skies.valley, 75);

                    channel = IconData.skies.valley;

                }

                if(decimalCounter == 13)
                {
                    CultivateConversions();

                }

                if(decimalCounter == 14)
                {

                    CultivateImports();

                }

                if (decimalCounter == 15)
                {

                    Mod.instance.spellRegister.Add(new(origin, 128, IconData.impacts.supree, new()) { scheme = IconData.schemes.weald, sound = SpellHandle.Sounds.getNewSpecialItem, displayRadius = 4, });

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdThree))
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.wealdThree, 1);

                    }

                    Plot();

                    eventLocked = true;

                }

                return;

            }

            radialCounter++;

            Cultivation();

            if (radialCounter == 9)
            {

                eventComplete = true;
            
            }

        }

        public override void EventRemove()
        {
            
            base.EventRemove();

            Rite.ApplyCost(costCounter);

        }

        public void CultivateConversions()
        {

            int quality = Mod.instance.questHandle.IsComplete(QuestHandle.wealdFour) ? Mod.instance.Config.cultivateBehaviour : 0;

            conversions = SpawnData.CropList(location, quality);

        }

        public void CultivateImports()
        {

            int quality = Mod.instance.questHandle.IsComplete(QuestHandle.wealdFour) ? Mod.instance.Config.cultivateBehaviour : 0;

            List<string> imports = SpawnData.ShopList(location, quality);

            if (imports.Count > 0)
            {
                foreach (string import in imports)
                {

                    if (import == null)
                    {

                        continue;

                    }

                    if (!conversions.Contains(import))
                    {

                        conversions.Add(import);

                    }

                }

            }

        }

        public void Cultivation()
        {
            
            int cultivationCost = 2;

            if (Game1.player.FarmingLevel > 5)
            {

                cultivationCost = 1;

            }

            if (!Mod.instance.rite.targetCasts.ContainsKey(location.Name + "_cultivate"))
            {

                Mod.instance.rite.targetCasts[location.Name + "_cultivate"] = new();

            }

            Season cropSeason = location.GetSeason();

            Vector2 center = ModUtility.PositionToTile(origin);

            List<Vector2> affected = ModUtility.GetTilesWithinRadius(location, center, radialCounter);

            if(radialCounter == 1)
            {

                affected.Add(center);

            }

            foreach(Vector2 tile in affected)
            {

                if (!location.terrainFeatures.ContainsKey(tile))
                {

                    if (!inabsentia && Game1.player.CurrentTool is Hoe && radialCounter < 5)
                    {

                        ModUtility.Reave(location, tile, 0, false);

                    }

                    continue;

                }

                // =========================================================================
                // crops

                if (location.terrainFeatures[tile] is StardewValley.TerrainFeatures.HoeDirt hoeDirt)
                {

                    // ----------------------------------------------------------------------
                    // check recasts

                    if (Mod.instance.rite.targetCasts[location.Name + "_cultivate"].ContainsKey(tile))
                    {

                        if (Mod.instance.rite.targetCasts[location.Name + "_cultivate"][tile].Contains("Crop"))
                        {

                            if (hoeDirt.crop != null)
                            {

                                string cropName = "Crop" + hoeDirt.crop.indexOfHarvest.Value.ToString();

                                if (cropName == Mod.instance.rite.targetCasts[location.Name + "_cultivate"][tile])
                                {
   
                                    continue;

                                }

                            }

                        }
                        else if (Mod.instance.rite.targetCasts[location.Name + "_cultivate"][tile] == "Hoed")
                        {

                            if (hoeDirt.crop == null)
                            {
     
                                continue;

                            }

                        }

                    }

                    // ----------------------------------------------------------------------
                    // cultivate crop

                    if (!hoeDirt.HasFertilizer())
                    {

                        int powerLevel = Mod.instance.PowerLevel;

                        if (Mod.instance.Config.cultivateBehaviour == 1)
                        {

                            if (powerLevel >= 4)
                            {

                                hoeDirt.plant("918", Game1.player, true);

                            }
                            else
                            {

                                hoeDirt.plant("466", Game1.player, true);

                            }

                        }
                        else if (Mod.instance.Config.cultivateBehaviour == 2)
                        {

                            if (powerLevel >= 4)
                            {

                                hoeDirt.plant("466", Game1.player, true);

                            }
                            else
                            {

                                hoeDirt.plant("465", Game1.player, true);

                            }

                        }
                        else
                        {

                            if (powerLevel >= 4 || Mod.instance.Config.cultivateBehaviour == 4)
                            {

                                hoeDirt.plant("919", Game1.player, true);

                            }
                            else
                            {

                                hoeDirt.plant("369", Game1.player, true);

                            }

                        }

                    }

                    if (hoeDirt.crop == null)
                    {

                        if (inventory.Count > 0)
                        {

                            int remove = -1;

                            for (int i = 0; i < inventory.Count; i++)
                            {

                                Item item = inventory[i];

                                if (ignore.Contains(item.QualifiedItemId))
                                {
     
                                    continue;

                                }

                                string itemId = Crop.ResolveSeedId(item.QualifiedItemId.Replace("(O)", ""), location);

                                if (!Crop.TryGetData(itemId, out var cropData) || !cropData.Seasons.Contains(cropSeason))
                                {

                                    ignore.Add(item.QualifiedItemId);
   
                                    continue;

                                }

                                if (hoeDirt.plant(itemId, Game1.player, false))
                                {

                                    remove = i;

                                    break;

                                }

                            }

                            if (remove != -1)
                            {

                                inventory[remove].Stack--;

                                if (inventory[remove].Stack <= 0)
                                {

                                    inventory.RemoveAt(remove);

                                }

                            }
                            else
                            {

                                Mod.instance.rite.targetCasts[location.Name + "_cultivate"][tile] = "Hoed";

                                continue;

                            }

                        }
                        else
                        {

                            Mod.instance.rite.targetCasts[location.Name + "_cultivate"][tile] = "Hoed";

                            continue;

                        }

                    }

                    if ((hoeDirt.crop.isWildSeedCrop() && hoeDirt.crop.currentPhase.Value <= 1) || hoeDirt.crop.dead.Value)// && (Game1.currentSeason != "winter" || location.isGreenhouse.Value)
                    {

                        string attempt = UpgradeCrop(hoeDirt, location);

                        if (hoeDirt.crop == null)
                        {

                            Mod.instance.rite.targetCasts[location.Name + "_cultivate"][tile] = "Hoed";
 
                            continue;

                        }

                    }

                    if (Mod.instance.Config.cultivateBehaviour == 3)
                    {
                        
                        if (hoeDirt.crop.currentPhase.Value == 0)
                        {

                            hoeDirt.crop.currentPhase.Value = 1;

                            hoeDirt.crop.dayOfCurrentPhase.Value = 0;

                            hoeDirt.crop.updateDrawMath(hoeDirt.Tile);

                        }

                    }
                    else
                    if (hoeDirt.crop.currentPhase.Value <= 1)
                    {

                        hoeDirt.crop.currentPhase.Value = 2;

                        hoeDirt.crop.dayOfCurrentPhase.Value = 0;

                        hoeDirt.crop.updateDrawMath(hoeDirt.Tile);

                    }

                    if (hoeDirt.crop.indexOfHarvest.Value != null)
                    {

                        Mod.instance.rite.targetCasts[location.Name + "_cultivate"][tile] = "Crop" + hoeDirt.crop.indexOfHarvest.Value.ToString();

                    }
                    else
                    {

                        Mod.instance.rite.targetCasts[location.Name + "_cultivate"][tile] = "CropUnknown";

                    }

                    if (!inabsentia)
                    {

                        costCounter += cultivationCost;

                    }
                    else
                    {

                        continue;

                    }

                    Vector2 cursorVector = tile * 64 + new Vector2(32, 40);

                    Microsoft.Xna.Framework.Color randomColour;

                    switch (Mod.instance.randomIndex.Next(4))
                    {

                        case 0:

                            randomColour = Color.LightGreen;
                            break;

                        case 1:

                            randomColour = Color.White;
                            break;

                        case 3:

                            continue;

                        default:

                            randomColour = Color.YellowGreen;
                            break;

                    }

                    Mod.instance.iconData.ImpactIndicator(
                        Game1.player.currentLocation,
                        cursorVector,
                        IconData.impacts.glare,
                        4f,
                        new() { alpha = 0.65f, color = randomColour, rotation = Mod.instance.randomIndex.Next(4) * 0.5f, });

                }

            }

        }

        public string UpgradeCrop(StardewValley.TerrainFeatures.HoeDirt hoeDirt, GameLocation targetLocation)
        {

            int size = Mod.instance.Config.cultivatePlot;

            int tileX = (int)hoeDirt.Tile.X;

            int tileY = (int)hoeDirt.Tile.Y;

            Vector2 useVector = new Vector2((int)(tileX / size), (int)(tileY / size));

            string useSeed;

            bool animate = location.Name == Game1.player.currentLocation.Name;

            if (plots.ContainsKey(useVector))
            {

                if (plots[useVector] == string.Empty)
                {

                    plots[useVector] = conversions[Mod.instance.randomIndex.Next(conversions.Count)];

                }

                useSeed = plots[useVector];

            }
            else
            {

                useSeed = conversions[Mod.instance.randomIndex.Next(conversions.Count)];

            }

            if (useSeed == "829")
            {

                hoeDirt.destroyCrop(animate);

                Crop newGinger = new(true, "2", tileX, tileY, targetLocation);

                hoeDirt.crop = newGinger;

                if (animate)
                {

                    targetLocation.playSound("dirtyHit");

                }

                Game1.stats.SeedsSown++;

                return useSeed;

            }
            
            if (useSeed == string.Empty || useSeed == "802")
            {

                useSeed = "770";
            
            }

            hoeDirt.destroyCrop(animate);

            if (!animate)
            {

                Plant(hoeDirt,useSeed);

                return useSeed;

            }

            hoeDirt.plant(useSeed, Game1.player, false);

            return useSeed;

        }

        public bool Plant(StardewValley.TerrainFeatures.HoeDirt hoeDirt, string itemId)
        {

            Season season = location.GetSeason();
            
            Point point = Utility.Vector2ToPoint(hoeDirt.Tile);
            
            itemId = Crop.ResolveSeedId(itemId, location);
            
            if (!Crop.TryGetData(itemId, out var data) || data.Seasons.Count == 0)
            {
                
                return false;
            
            }

            Object value;

            bool flag = location.objects.TryGetValue(hoeDirt.Tile, out value) && value is IndoorPot;

            bool flag2 = flag && !location.IsOutdoors;

            if (!location.CheckItemPlantRules(itemId, flag, flag2 || (location.GetData()?.CanPlantHere ?? location.IsFarm), out var deniedMessage))
            {

                return false;

            }

            if (!flag2 && !location.CanPlantSeedsHere(itemId, point.X, point.Y, flag, out deniedMessage))
            {

                return false;

            }

            if (flag2 || location.SeedsIgnoreSeasonsHere() || !((!(data.Seasons?.Contains(season))) ?? true))
            {

                hoeDirt.crop = new Crop(itemId, point.X, point.Y, location);

                Game1.stats.SeedsSown++;

                hoeDirt.applySpeedIncreases(Game1.player);

                hoeDirt.nearWaterForPaddy.Value = -1;
                
                if (hoeDirt.hasPaddyCrop() && hoeDirt.paddyWaterCheck())
                {

                    hoeDirt.state.Value = 1;

                    hoeDirt.updateNeighbors();
                
                }

                return true;

            }

            return false;

        }


        public void Plot()
        {

            int size = Mod.instance.Config.cultivatePlot;

            if (size == 1)
            {

                return;

            }

            Vector2 tile = ModUtility.PositionToTile(origin);

            int tileX = (int)tile.X - 8;

            int tileY = (int)tile.Y - 8;

            for(int x = 0; x < 17; x++)
            {

                for(int y = 0; y < 17; y++)
                {

                    string useCrop = string.Empty;

                    Vector2 tryVector = new Vector2(tileX + x, tileY + y);

                    if (location.terrainFeatures.ContainsKey(tryVector))
                    {

                        if (location.terrainFeatures[tryVector] is StardewValley.TerrainFeatures.HoeDirt hoeDirt)
                        {

                            if(hoeDirt.crop != null)
                            {

                                string trySeed = hoeDirt.crop.netSeedIndex.Value;

                                if(trySeed != null && trySeed != string.Empty)
                                {
                                    
                                    if (conversions.Contains(trySeed))
                                    {

                                        useCrop = trySeed;

                                    }

                                }

                            }

                        }

                    }

                    Vector2 useVector = new Vector2((int)((tileX + x) / size), (int)((tileY + y) / size));

                    if (plots.ContainsKey(useVector))
                    {

                        if (plots[useVector] == string.Empty)
                        {

                            plots[useVector] = useCrop;

                        }

                        continue;

                    }


                    plots[useVector] = useCrop;

                }

            }

            for(int p = plots.Count-1; p >= 0; p--)
            {

                KeyValuePair<Vector2,string> plot = plots.ElementAt(p);

                if(plot.Value == string.Empty)
                {

                    plots[plot.Key] = conversions[Mod.instance.randomIndex.Next(conversions.Count)];

                }

            }

        }

    }

}
