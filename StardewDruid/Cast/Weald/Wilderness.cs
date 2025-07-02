using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Location.Druid;
using StardewModdingAPI;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using xTile.Layers;
using xTile.Tiles;


namespace StardewDruid.Cast.Weald
{
    public class Wilderness : EventHandle
    {

        public int radialCounter = 0;

        public int costing = 0;

        public List<string> ignore = new();

        public int offset = 0;

        public bool beachTerrain = false;

        public enum spawns
        {
            flower,
            forage,
            tree,
            grass,
            weed,
            twig,
            rock,
            beach,
            gems,
        }

        int spawnCount;

        public Wilderness()
        {

            offset = Mod.instance.randomIndex.Next(2);

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

                    Mod.instance.rite.Channel(IconData.skies.mountain, 75);

                    channel = IconData.skies.mountain;

                }

                if (decimalCounter == 15)
                {

                    eventLocked = true;

                    SpellHandle circleHandle = new(origin, 256, IconData.impacts.summoning, new())
                    {
                        scheme = IconData.schemes.herbal_ligna,

                        sound = SpellHandle.Sounds.discoverMineral
                    };

                    Mod.instance.spellRegister.Add(circleHandle);

                    Mod.instance.spellRegister.Add(new(origin, 384, IconData.impacts.roots, new() { SpellHandle.Effects.snare}) { sound = SpellHandle.Sounds.treethud, displayRadius = 4,});

                    Mod.instance.rite.specialCasts[location.Name].Add(Rite.eventWilderness + costing.ToString());

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdFour))
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.wealdFour, 1);
                    }

                    Mod.instance.rite.ChargeSet(IconData.cursors.wealdCharge);

                }

                return;

            }

            Wilding();

            radialCounter++;

            if (radialCounter == 5)
            {

                eventComplete = true;
            
            }

        }

        public void Wilding()
        {
            
            if(radialCounter == 0)
            {

                return;

            }

            Layer back = location.map.GetLayer("Back");

            List<Vector2> affected = ModUtility.GetTilesWithinRadius(location, ModUtility.PositionToTile(origin), radialCounter * 2);

            Dictionary<Vector2, List<spawns>> spawnProspects = new();

            if(location is Beach || location is Atoll || location is IslandWest || location is IslandSouth || location is IslandSouthEast)
            {

                beachTerrain = true;

            }

            for (int p = 0; p < affected.Count; p++)
            {

                switch (radialCounter)
                {
                    
                    case 1:

                        if (p % 2 == offset)
                        {

                            continue;

                        }

                        break;

                    case 2:

                        if (p % 4 != offset)
                        {

                            continue;

                        }

                        break;

                    case 3:

                        if (p % 6 != offset)
                        {

                            continue;

                        }

                        break;

                    case 4:

                        if (p % 8 != offset)
                        {

                            continue;

                        }

                        break;
                }

                Vector2 prospect = affected[p];

                spawnProspects[prospect] = new();

                string tileCheck = ModUtility.GroundCheck(location, prospect);

                if (tileCheck != "ground")
                {

                    continue;

                }

                Dictionary<string, List<Vector2>> occupied = ModUtility.NeighbourCheck(location, prospect, 0, 0);

                if (occupied.Count != 0)
                {

                    continue;

                }

                if (location is MineShaft)
                {

                    spawnProspects[prospect].Add(spawns.weed);

                    spawnProspects[prospect].Add(spawns.weed);

                    spawnProspects[prospect].Add(spawns.weed);

                    spawnProspects[prospect].Add(spawns.gems);

                    continue;

                }

                int tileX = (int)prospect.X;

                int tileY = (int)prospect.Y;

                Tile backTile = back.Tiles[tileX, tileY];

                if (backTile.TileIndexProperties.TryGetValue("Type", out var typeValue))
                {

                    if (
                        typeValue == "Dirt" || 
                        backTile.TileIndexProperties.TryGetValue("Diggable", out _) || 
                        (typeValue == "Grass" && backTile.TileIndexProperties.TryGetValue("NoSpawn", out _) == false)
                    ) 
                    {

                        spawnProspects[prospect].Add(spawns.weed);

                        spawnProspects[prospect].Add(spawns.rock);

                        spawnProspects[prospect].Add(spawns.twig);

                        if (beachTerrain)
                        {

                            spawnProspects[prospect].Add(spawns.beach);

                            continue;

                        }

                        if (Mod.instance.randomIndex.Next(3) == 0)
                        {

                            spawnProspects[prospect].Add(spawns.forage);

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.wealdFour))
                            {

                                spawnProspects[prospect].Add(spawns.flower);

                            }

                        }
                        
                        Dictionary<string, List<Vector2>> neighbour = ModUtility.NeighbourCheck(location, prospect, 0, 1);

                        if (neighbour.Count == 0)
                        {

                            spawnProspects[prospect].Add(spawns.tree);


                        }
                        else
                        {

                            spawnProspects[prospect].Add(spawns.grass);

                        }

                    }

                }

            }

            if (spawnProspects.Count > 0)
            {

                foreach (KeyValuePair<Vector2,List<spawns>> prospect in spawnProspects)
                {

                    if(prospect.Value.Count == 0)
                    {
                        continue;
                    }

                    spawns spawn = prospect.Value[Mod.instance.randomIndex.Next(prospect.Value.Count)];

                    switch (spawn)
                    {

                        case spawns.tree:

                            SpawnTrees(prospect.Key);

                            break;

                        case spawns.weed:

                            SpawnWeeds(prospect.Key);

                            break;

                        case spawns.twig:

                            SpawnTwigs(prospect.Key);

                            break;

                        case spawns.flower:

                            SpawnForage(prospect.Key, true);

                            break;

                        case spawns.forage:

                            SpawnForage(prospect.Key);

                            break;

                        case spawns.grass:

                            SpawnGrass(prospect.Key);

                            break;

                        case spawns.rock:

                            SpawnRock(prospect.Key);

                            break;

                        case spawns.beach:

                            SpawnBeach(prospect.Key);

                            break;

                        case spawns.gems:

                            SpawnGems(prospect.Key);

                            break;

                    }

                }

            }

            if (spawnCount > 0)
            {

                costCounter = spawnCount * costing;

                Rite.ApplyCost(costCounter);

            }

            Mod.instance.rite.castTimer = 120;

        }

        public void SpawnGrass(Vector2 prospect)
        {

            if (location.terrainFeatures.ContainsKey(prospect))
            {
                
                return;
            
            }

            StardewValley.TerrainFeatures.Grass grassFeature = new(1, 4);

            location.terrainFeatures.Add(prospect, grassFeature);

            Microsoft.Xna.Framework.Rectangle tileRectangle = new((int)prospect.X * 64 + 1, (int)prospect.Y * 64 + 1, 62, 62);

            grassFeature.doCollisionAction(tileRectangle, 2, prospect, Game1.player);

        }

        public void SpawnBeach(Vector2 prospect)
        {

            if (location.objects.ContainsKey(prospect))
            {

                return;

            }

            StardewValley.Object newForage = new StardewValley.Object(SpawnData.RandomBeach(location).ToString(), 1);

            ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(newForage.QualifiedItemId);

            if (dataOrErrorItem.IsErrorItem)
            {

                return;

            }

            newForage.IsSpawnedObject = true;

            newForage.Location = location;

            newForage.TileLocation = prospect;

            if (location.objects.TryAdd(prospect, newForage))
            {

                spawnCount++;

            }

            Mod.instance.iconData.CursorIndicator(location, prospect * 64 + new Vector2(0, 8), IconData.cursors.weald, new());

        }

        public void SpawnWeeds(Vector2 prospect)
        {
            string randomWeed;

            if (location is MineShaft mineShaft)
            {
                
                if (mineShaft.getMineArea() == 40)
                {
                    randomWeed = "(O)"+Mod.instance.randomIndex.Next(319, 322).ToString();

                    if (mineShaft.GetAdditionalDifficulty() > 0)
                    {

                        randomWeed = "(O)" + Mod.instance.randomIndex.Next(313,316).ToString();

                    }

                }
                else if (mineShaft.getMineArea() == 80)
                {
                    
                    randomWeed = "(O)" + Mod.instance.randomIndex.Next(316, 319).ToString();

                }
                else
                {

                    randomWeed = "(O)" + Mod.instance.randomIndex.Next(313, 316).ToString();

                }

                spawnCount++;

            }
            else
            {

                randomWeed = GameLocation.getWeedForSeason(Mod.instance.randomIndex, location.GetSeason());

            }

            StardewValley.Object @object = ItemRegistry.Create<StardewValley.Object>(randomWeed);

            ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(@object.QualifiedItemId);

            if (dataOrErrorItem.IsErrorItem)
            {

                return;

            }

            @object.MinutesUntilReady = 1;

            location.objects.TryAdd(prospect, @object);

        }

        public void SpawnTwigs(Vector2 prospect)
        {

            StardewValley.Object @object = ItemRegistry.Create<StardewValley.Object>(SpawnData.RandomTwig());

            @object.MinutesUntilReady = 1;

            if (location.objects.TryAdd(prospect, @object))
            {

                Mod.instance.iconData.CursorIndicator(location, prospect * 64 + new Vector2(0, 8), IconData.cursors.weald, new());

            }

        }

        public void SpawnRock(Vector2 prospect)
        {

            StardewValley.Object @object = ItemRegistry.Create<StardewValley.Object>(SpawnData.RandomRock());

            @object.MinutesUntilReady = 1;

            if (location.objects.TryAdd(prospect, @object))
            {

                Mod.instance.iconData.CursorIndicator(location, prospect * 64 + new Vector2(0, 8), IconData.cursors.weald, new());

            }

        }

        public void SpawnTrees(Vector2 prospect)
        {

            if (location.terrainFeatures.ContainsKey(prospect))
            {
                
                return;
            
            }

            Tree newTree = SpawnData.RandomTree(location);

            if (newTree == null)
            {
                return;

            }

            location.terrainFeatures.Add(prospect, newTree);

            Mod.instance.iconData.CursorIndicator(location, prospect * 64 + new Vector2(0, 8), IconData.cursors.weald, new());

            spawnCount++;

        }

        public void SpawnForage(Vector2 prospect, bool flower = false)
        {

            if (location.objects.ContainsKey(prospect))
            {
                
                return;
            
            }

            string randomCrop;

            if (flower)
            {
                
                randomCrop = SpawnData.RandomFlower(location);

            }
            else
            {

                randomCrop = SpawnData.RandomForage(location);

            }

            StardewValley.Object newForage = new StardewValley.Object(randomCrop, 1);

            ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(newForage.QualifiedItemId);

            if (dataOrErrorItem.IsErrorItem)
            {

                return;

            }

            newForage.IsSpawnedObject = true;

            newForage.Location = location;

            newForage.TileLocation = prospect;

            if (location.objects.TryAdd(prospect, newForage))
            {

                spawnCount++;

            }

            Mod.instance.iconData.CursorIndicator(location, prospect * 64 + new Vector2(0, 8), IconData.cursors.weald, new());

        }

        public void SpawnGems(Vector2 prospect)
        {
            ParsedItemData dataOrErrorItem;

            if (Mod.instance.randomIndex.Next(5) == 0)
            {

                StardewValley.Object @object = ItemRegistry.Create<StardewValley.Object>("(O)44");

                dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(@object.QualifiedItemId);

                if (dataOrErrorItem.IsErrorItem)
                {

                    return;

                }

                @object.MinutesUntilReady = 1;

                location.objects.TryAdd(prospect, @object);

                Mod.instance.iconData.CursorIndicator(location, prospect * 64 + new Vector2(0, 8), IconData.cursors.weald, new());

                return;

            }

            string randomGem = SpawnData.RandomForage(location);

            StardewValley.Object newForage = new StardewValley.Object(randomGem, 1);

            dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(newForage.QualifiedItemId);

            if (dataOrErrorItem.IsErrorItem)
            {

                return;

            }

            newForage.IsSpawnedObject = true;

            newForage.Location = location;

            newForage.TileLocation = prospect;

            if (location.objects.TryAdd(prospect, newForage))
            {

                spawnCount++;

                Mod.instance.iconData.CursorIndicator(location, prospect * 64 + new Vector2(0, 8), IconData.cursors.weald, new());

            }

        }

    }

}
