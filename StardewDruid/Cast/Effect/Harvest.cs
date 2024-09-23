using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Characters;
using StardewValley.GameData.Crops;
using StardewValley.GameData.FruitTrees;
using StardewValley.GameData.WildTrees;
using StardewValley.Internal;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using static StardewDruid.Cast.Weald.Bounty;
using static StardewValley.Minigames.MineCart;
using static StardewValley.Minigames.TargetGame;

namespace StardewDruid.Cast.Effect
{
    public class Harvest : EventHandle
    {

        public Dictionary<Vector2, HarvestTarget> harvesters = new();

        public bool iridium;

        public Harvest()
        {

        }


        public virtual void AddTarget(GameLocation location, Vector2 tile)
        {

            if (harvesters.ContainsKey(tile))
            {
                return;
            }

            harvesters.Add(tile, new(location, tile));

            activeLimit = eventCounter + 8;

            iridium = (Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areAllAreasComplete();

        }

        public override void EventDecimal()
        {

            // -------------------------------------------------
            // Crops
            // -------------------------------------------------

            for (int h = harvesters.Count - 1; h >= 0; h--)
            {

                KeyValuePair<Vector2, HarvestTarget> toHarvest = harvesters.ElementAt(h);

                if ((toHarvest.Value.counter <= 0))
                {

                    harvesters.Remove(toHarvest.Key);

                }

                toHarvest.Value.counter--;

                int targetRadius = toHarvest.Value.limit - toHarvest.Value.counter;

                List<Vector2> tileVectors = ModUtility.GetTilesWithinRadius(location, toHarvest.Value.tile, targetRadius);

                if(targetRadius == 1)
                {

                    tileVectors.Add(toHarvest.Value.tile);

                }

                List<HoeDirt> hoeDirts = new();

                if (!Mod.instance.rite.targetCasts.ContainsKey(location.Name + "_harvest"))
                {

                    Mod.instance.rite.targetCasts[location.Name + "_harvest"] = new();

                }

                foreach (Vector2 tileVector in tileVectors)
                {

                    if (location.objects.ContainsKey(tileVector))
                    {

                        StardewValley.Object targetObject = location.objects[tileVector];

                        if (SpawnData.ForageCheck(targetObject))
                        {

                            StardewValley.Item objectInstance = ModUtility.ExtractForage(location, tileVector, false);

                            new ThrowHandle(tileVector * 64, toHarvest.Value.tile * 64, objectInstance) { pocket = true }.register();

                            location.objects.Remove(tileVector);

                            continue;

                        }

                        if (
                            targetObject.QualifiedItemId == "(BC)9" ||
                            targetObject.QualifiedItemId == "(BC)10" ||
                            targetObject.QualifiedItemId == "(BC)MushroomLog" ||
                            targetObject.IsTapper()
                            )
                        {

                            if (targetObject.heldObject.Value != null && targetObject.MinutesUntilReady == 0)
                            {

                                StardewValley.Item objectInstance = targetObject.heldObject.Value.getOne();

                                if (targetObject.QualifiedItemId == "(BC)MushroomLog")
                                {

                                    objectInstance.Quality = 4;

                                }


                                new ThrowHandle(tileVector * 64, toHarvest.Value.tile * 64, objectInstance) { pocket = true }.register();


                                targetObject.heldObject.Value = null;

                                targetObject.MinutesUntilReady = 0;

                                targetObject.performDropDownAction(Game1.player);

                            }

                            Mod.instance.rite.targetCasts[location.Name + "_harvest"][tileVector] = "Tree";

                            continue;

                        }

                    }

                    if (toHarvest.Value.location.terrainFeatures.ContainsKey(tileVector))
                    {

                        if (toHarvest.Value.location.terrainFeatures[tileVector] is HoeDirt hoeDirt)
                        {

                            hoeDirts.Add(hoeDirt);

                            continue;

                        }

                        if (toHarvest.Value.location.terrainFeatures[tileVector] is StardewValley.TerrainFeatures.FruitTree fruitTree)
                        {

                            if (fruitTree.stump.Value)
                            {

                                continue;

                            }

                            if (fruitTree.growthStage.Value < 4)
                            {

                                continue;

                            }

                            fruitTree.performUseAction(tileVector);

                            if (Mod.instance.rite.targetCasts[location.Name + "_harvest"].ContainsKey(tileVector))
                            {

                                continue;

                            }

                            FruitTreeData data = fruitTree.GetData();

                            if (data == null)
                            {

                                continue;

                            }

                            if (data.Seasons == null)
                            {

                                continue;

                            }

                            if (!data.Seasons.Contains(Game1.season))
                            {

                                continue;

                            }

                            if (!Game1.IsWinter)
                            {


                                if ((int)fruitTree.struckByLightningCountdown.Value > 0)
                                {

                                    StardewValley.Object coal = new("382", Mod.instance.randomIndex.Next(1, 2));

                                    new ThrowHandle(tileVector * 64, toHarvest.Value.tile * 64, coal) { pocket = true }.register();

                                }
                                else
                                {

                                    if (data.Fruit != null)
                                    {

                                        foreach (FruitTreeFruitData item2 in data.Fruit)
                                        {

                                            StardewValley.Item fruitDrop = ItemQueryResolver.TryResolveRandomItem(item2, new ItemQueryContext(location, null, null), avoidRepeat: false, null, null, null, delegate (string query, string error) { });
                                            
                                            if (fruitDrop == null)
                                            {

                                                continue;

                                            }

                                            int itemQuality = 0;

                                            if ((int)fruitTree.daysUntilMature.Value <= -112)
                                            {
                                                itemQuality = 1;
                                            }

                                            if ((int)fruitTree.daysUntilMature.Value <= -224)
                                            {
                                                itemQuality = 2;
                                            }

                                            if ((int)fruitTree.daysUntilMature.Value <= -336)
                                            {
                                                itemQuality = 4;
                                            }

                                            StardewValley.Object fruit = new(fruitDrop.QualifiedItemId, Mod.instance.randomIndex.Next(1, 2), quality:itemQuality);

                                            new ThrowHandle(tileVector * 64, toHarvest.Value.tile * 64, fruit) { pocket = true }.register();

                                            break;

                                        }

                                    }

                                }

                            }

                            Mod.instance.rite.targetCasts[location.Name + "_harvest"][tileVector] = "Tree";

                        }
                        else if (toHarvest.Value.location.terrainFeatures[tileVector] is StardewValley.TerrainFeatures.Tree treeFeature)
                        {
                         
                            if(treeFeature.growthStage.Value < 5)
                            {

                                continue;

                            }

                            treeFeature.performUseAction(tileVector);

                            if (Mod.instance.rite.targetCasts[location.Name + "_harvest"].ContainsKey(tileVector))
                            {

                                continue;

                            }

                            switch (Mod.instance.randomIndex.Next(6))
                            {

                                case 0:
                                case 1:


                                    int debrisType = 388;

                                    int debrisMax = 3;

                                    if (Game1.player.professions.Contains(12))
                                    {

                                        debrisMax++;

                                    }

                                    if (treeFeature.treeType.Value == "8") //mahogany
                                    {

                                        debrisType = 709; debrisMax = 1;

                                        if (Game1.player.professions.Contains(14))
                                        {

                                            debrisMax++;

                                        }

                                    }

                                    if (treeFeature.treeType.Value == "7") // mushroom
                                    {

                                        debrisType = 420; debrisMax = 1;

                                    }

                                    for (int i = 0; i < debrisMax; i++)
                                    {

                                        new ThrowHandle(tileVector * 64, toHarvest.Value.tile * 64, debrisType, 0) { pocket = true }.register();

                                    }

                                    break;

                                case 2:

                                    WildTreeData treeData = treeFeature.GetData();

                                    if(treeData == null)
                                    {

                                        break;

                                    }

                                    Item treeSeed = ItemRegistry.Create(treeData.SeedItemId);

                                    if (treeSeed == null)
                                    {

                                        break;

                                    }

                                    new ThrowHandle(tileVector * 64, toHarvest.Value.tile * 64, treeSeed) { pocket = true }.register();

                                    break;

                                case 3:

                                    StardewValley.Object sap = new("92", Mod.instance.randomIndex.Next(1, 3));

                                    new ThrowHandle(tileVector * 64, toHarvest.Value.tile * 64, sap) { pocket = true }.register();

                                    break;

                            }

                            if (treeFeature.hasMoss.Value)
                            {

                                new ThrowHandle(tileVector * 64, toHarvest.Value.tile * 64, ItemQueryResolver.TryResolveRandomItem("Moss", new ItemQueryContext(location, Game1.player, null))){ pocket = true }.register();

                            }

                            Mod.instance.rite.targetCasts[location.Name + "_harvest"][tileVector] = "Tree";

                        }
                        else if (toHarvest.Value.location.terrainFeatures[tileVector] is StardewValley.TerrainFeatures.Grass grassFeature)
                        {
                            
                            Microsoft.Xna.Framework.Rectangle tileRectangle = new((int)(tileVector.X * 64) + 1, (int)(tileVector.Y * 64) + 1, 62, 62);

                            grassFeature.doCollisionAction(tileRectangle, 2, tileVector, Game1.player);

                            if (Mod.instance.rite.targetCasts[location.Name + "_harvest"].ContainsKey(tileVector))
                            {

                                continue;

                            }

                            switch (Mod.instance.randomIndex.Next(20))
                            {

                                case 0:

                                    string item = "498";

                                    switch (Game1.currentSeason)
                                    {

                                        case "spring":

                                            item = "495";

                                            break;

                                        case "summer":

                                            item = "496";

                                            break;

                                        case "fall":

                                            item = "497";

                                            break;

                                    }

                                    StardewValley.Object seedPack = new(item, Mod.instance.randomIndex.Next(1, 2));

                                    new ThrowHandle(tileVector * 64, toHarvest.Value.tile * 64,seedPack) { pocket = true }.register();

                                    break;

                                case 1:
                                case 2:

                                    StardewValley.Object fibre = new("771", Mod.instance.randomIndex.Next(1, 2));

                                    new ThrowHandle(tileVector * 64, toHarvest.Value.tile * 64, fibre) { pocket = true }.register();

                                    break;


                            }

                            Mod.instance.rite.targetCasts[location.Name + "_harvest"][tileVector] = "Grass";

                        }

                    }

                    if (toHarvest.Value.location.objects.ContainsKey(tileVector))
                    {

                        if (Mod.instance.rite.targetCasts[location.Name + "_harvest"].ContainsKey(tileVector))
                        {

                            continue;

                        }

                        if (toHarvest.Value.location.objects[tileVector] is IndoorPot indoorPot)
                        {

                            if(indoorPot.hoeDirt.Value != null)
                            {
                                hoeDirts.Add(indoorPot.hoeDirt.Value);

                            }

                            if(indoorPot.bush.Value != null)
                            {

                                if(indoorPot.bush.Value.size.Value == 3)
                                {
                                    
                                    if (indoorPot.bush.Value.shakeTimer == 0)
                                    {
                                        
                                        indoorPot.bush.Value.shake(tileVector, doEvenIfStillShaking: false);

                                    }

                                }

                            }

                        }

                        if (SpawnData.ForageCheck(toHarvest.Value.location.objects[tileVector]))
                        {

                            StardewValley.Item extract = ModUtility.ExtractForage(toHarvest.Value.location,tileVector);

                            ThrowHandle throwObject = new(tileVector * 64, toHarvest.Value.tile * 64, extract);

                            throwObject.pocket = true;

                            throwObject.register();

                        }

                        Mod.instance.rite.targetCasts[location.Name + "_harvest"][tileVector] = "Pot";


                    }

                }

                foreach(HoeDirt hoeDirt in hoeDirts)
                {

                    if (hoeDirt.crop != null)
                    {
                        if (
                            hoeDirt.crop.currentPhase.Value >= hoeDirt.crop.phaseDays.Count - 1 &&
                            (!hoeDirt.crop.fullyGrown.Value || hoeDirt.crop.dayOfCurrentPhase.Value <= 0)
                            && !hoeDirt.crop.dead.Value
                            && hoeDirt.crop.indexOfHarvest.Value != null)
                        {

                            List<StardewValley.Object> extracts = ModUtility.ExtractCrop(hoeDirt, hoeDirt.crop, hoeDirt.Tile, iridium);

                            foreach (StardewValley.Object extract in extracts)
                            {

                                ThrowHandle throwObject = new(hoeDirt.Tile * 64, toHarvest.Value.tile * 64, extract);

                                throwObject.pocket = true;

                                throwObject.register();

                            }

                        }

                    }

                }

            }

        }

    }

    public class HarvestTarget
    {

        public Vector2 tile;

        public GameLocation location;

        public int counter;

        public int limit;

        public HarvestTarget(GameLocation Location, Vector2 Tile)
        {

            tile = Tile;

            counter = 8;

            limit = 8;

            location = Location;

        }

    }

}
