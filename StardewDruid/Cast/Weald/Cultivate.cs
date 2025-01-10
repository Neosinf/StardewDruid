using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;

namespace StardewDruid.Cast.Weald
{
    public class Cultivate : EventHandle
    {

        public int radialCounter = 0;

        public int castCost = 0;

        public StardewValley.Inventories.Inventory inventory = new();

        public List<string> ignore = new();

        public Farmer puppet = null;

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

                    Mod.instance.rite.channel(IconData.skies.valley, 75);

                    channel = IconData.skies.valley;

                }

                if (decimalCounter == 15)
                {

                    eventLocked = true;

                    Mod.instance.spellRegister.Add(new(origin, 384, IconData.impacts.supree, new()) { scheme = IconData.schemes.weald, sound = SpellHandle.sounds.getNewSpecialItem, });

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdThree))
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.wealdThree, 1);

                    }

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

            Mod.instance.rite.castCost = castCost;

            Mod.instance.rite.ApplyCost();

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

                    if (hoeDirt.crop != null)
                    {

                        if (hoeDirt.crop.dead.Value)
                        {

                            hoeDirt.destroyCrop(true);

                            /*if (Game1.currentSeason == "winter" && !location.IsGreenhouse)
                            {

                                Mod.instance.rite.targetCasts[location.Name][tile] = "Hoed";

                                continue;

                            }*/

                            string wildSeed = "498";

                            switch (Game1.currentSeason)
                            {

                                case "spring":

                                    wildSeed = "495";
                                    break;

                                case "summer":

                                    wildSeed = "496";
                                    break;

                                case "fall":

                                    wildSeed = "497";
                                    break;

                            }

                            if (inabsentia)
                            {

                                if (puppet == null)
                                {

                                    puppet = new();

                                    puppet.currentLocation = location;

                                    puppet.professions.Add(5);

                                }

                                hoeDirt.plant(wildSeed, puppet, false);


                            }
                            else
                            {


                                hoeDirt.plant(wildSeed, Game1.player, false);

                            }

                        }

                    }

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

                    if (hoeDirt.crop.isWildSeedCrop() && hoeDirt.crop.currentPhase.Value <= 1)// && (Game1.currentSeason != "winter" || location.isGreenhouse.Value)
                    {

                        int quality = Mod.instance.questHandle.IsComplete(QuestHandle.wealdFour) ? Mod.instance.Config.cultivateBehaviour : 0;

                        UpgradeCrop(hoeDirt, location, quality);

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

                        castCost += cultivationCost;

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

        public static void UpgradeCrop(StardewValley.TerrainFeatures.HoeDirt hoeDirt, GameLocation targetLocation, int qualityFactor)
        {

            string generateItem = "770";

            if (Game1.season == Season.Winter)
            {

                generateItem = "PowdermelonSeeds";

            }

            if (qualityFactor > 0)
            {

                List<string> objectIndexes = SpawnData.CropList(targetLocation);

                for (int q = 3 - qualityFactor; q >= 0; q--)
                {

                    objectIndexes.Add(generateItem);
                    objectIndexes.Add(generateItem);

                }

                generateItem = objectIndexes[Mod.instance.randomIndex.Next(objectIndexes.Count)];

            }

            hoeDirt.destroyCrop(true);

            if (generateItem == "829")
            {

                Crop newGinger = new(true, "2", (int)hoeDirt.Tile.X, (int)hoeDirt.Tile.Y, targetLocation);

                hoeDirt.crop = newGinger;

                targetLocation.playSound("dirtyHit");

                Game1.stats.SeedsSown++;

                return;

            }

            hoeDirt.plant(generateItem, Game1.player, false);

        }

    }

}
