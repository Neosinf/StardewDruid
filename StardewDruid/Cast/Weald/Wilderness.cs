using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using System.Collections.Generic;
using xTile.Layers;
using xTile.Tiles;
using static System.Net.Mime.MediaTypeNames;


namespace StardewDruid.Cast.Weald
{
    public class Wilderness : EventHandle
    {

        public int radialCounter = 0;

        public int castCost = 0;

        public int costing = 0;

        public List<string> ignore = new();

        public int offset = 0;

        public enum spawns
        {
            flower,
            forage,
            tree,
            grass,
            shell,
            weed,
            twig,
            gem,
            rock,
            mussel,
            trash,
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

                    Mod.instance.rite.channel(IconData.skies.mountain, 75);

                    channel = IconData.skies.mountain;

                }

                if (decimalCounter == 15)
                {

                    eventLocked = true;

                    SpellHandle spellHandle = new(origin, 384, IconData.impacts.nature, new());

                    Mod.instance.spellRegister.Add(spellHandle);

                    ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdThree))
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.wealdThree, 1);

                    }

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

        public override void EventRemove()
        {
            base.EventRemove();

            Mod.instance.rite.castCost = castCost;

            Mod.instance.rite.ApplyCost();

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

            for(int p = 0; p < affected.Count; p++)
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

                        spawnProspects[prospect].Add(spawns.forage);

                        spawnProspects[prospect].Add(spawns.twig);

                        spawnProspects[prospect].Add(spawns.rock);

                        if (Mod.instance.questHandle.IsComplete(QuestHandle.wealdThree))
                        {

                            if (Mod.instance.randomIndex.Next(2) == 0)
                            {

                                spawnProspects[prospect].Add(spawns.flower);

                            }

                        }

                        if (location is Beach || location is StardewDruid.Location.Atoll)
                        {

                            spawnProspects[prospect].Add(spawns.trash);

                        }
                        else
                        {
                            
                            Dictionary<string, List<Vector2>> neighbour = ModUtility.NeighbourCheck(location, prospect, 0, 1);

                            if (neighbour.Count == 0)
                            {

                                spawnProspects[prospect].Add(spawns.tree);

                            }

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

                            SpawnForage(prospect.Key,true);

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

                    }

                }

            }

            if (spawnCount > 0)
            {

                castCost = spawnCount * costing;

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

        public void SpawnTrash(Vector2 prospect)
        {

            if (location.objects.ContainsKey(prospect))
            {

                return;

            }

            StardewValley.Object newForage = new StardewValley.Object(SpawnData.RandomTrash(location).ToString(), 1);

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

            string randomWeed = GameLocation.getWeedForSeason(Mod.instance.randomIndex, location.GetSeason());

            Object @object = ItemRegistry.Create<Object>(randomWeed);

            @object.MinutesUntilReady = 1;

            location.objects.TryAdd(prospect, @object);

        }

        public void SpawnTwigs(Vector2 prospect)
        {

            Object @object = ItemRegistry.Create<Object>(SpawnData.RandomTwig(location));

            @object.MinutesUntilReady = 1;

            location.objects.TryAdd(prospect, @object);

        }

        public void SpawnRock(Vector2 prospect)
        {

            Object @object = ItemRegistry.Create<Object>(SpawnData.RandomRock(location));

            @object.MinutesUntilReady = 1;

            location.objects.TryAdd(prospect, @object);

        }

        public void SpawnTrees(Vector2 prospect)
        {

            if (location.terrainFeatures.ContainsKey(prospect))
            {
                
                return;
            
            }

            ModUtility.RandomTree(location, prospect);

            Mod.instance.iconData.CursorIndicator(location, prospect * 64 + new Vector2(0, 8), IconData.cursors.weald, new());

            spawnCount++;

        }

        public void SpawnForage(Vector2 prospect, bool flower = false)
        {

            if (location.objects.ContainsKey(prospect))
            {
                
                return;
            
            }

            int randomCrop;

            if (flower)
            {
                
                randomCrop = SpawnData.RandomFlower(location);

            }
            else
            {

                randomCrop = SpawnData.RandomForage(location);

            }

            StardewValley.Object newForage = new StardewValley.Object(randomCrop.ToString(), 1);

            newForage.IsSpawnedObject = true;

            newForage.Location = location;

            newForage.TileLocation = prospect;

            if (location.objects.TryAdd(prospect, newForage))
            {

                spawnCount++;

            }

            Mod.instance.iconData.CursorIndicator(location, prospect * 64 + new Vector2(0, 8), IconData.cursors.weald, new());

        }

    }

}
