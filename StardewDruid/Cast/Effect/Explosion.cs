using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Extensions;
using StardewValley.GameData.Crops;
using StardewValley.GameData.FruitTrees;
using StardewValley.GameData.WildTrees;
using StardewValley.Internal;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast.Effect
{
    public class Explosion : EventHandle
    {

        public Dictionary<int, List<Vector2>> blastVectors = new();

        public int blastSpeed = 1;

        public int blastProgression = 0;

        public int blastRadius = 1;

        public StardewValley.Tools.Axe blastAxe;

        public StardewValley.Tools.Pickaxe blastPickaxe;

        public bool explosion = false;

        public Dictionary<SpellHandle.Effects,bool> blastEffects = new()
        {
            [SpellHandle.Effects.swipe] = false,
            [SpellHandle.Effects.hack] = false,
            [SpellHandle.Effects.toolhack] = false,
            [SpellHandle.Effects.sunder] = false,
            [SpellHandle.Effects.breaker] = false,
            [SpellHandle.Effects.toolbreak] = false,
            [SpellHandle.Effects.reave] = false,
            [SpellHandle.Effects.douse] = false,
        };

        public Explosion()
        {

        }

        public void RadialSetup(GameLocation targetLocation, Vector2 targetVector, int tileRadius)
        {

            blastAxe = new();

            blastAxe.UpgradeLevel = Mod.instance.PowerLevel - 1;

            blastAxe.lastUser = Game1.player;

            blastPickaxe = new();

            blastPickaxe.UpgradeLevel = Mod.instance.PowerLevel - 1;

            blastPickaxe.lastUser = Game1.player;

            location = targetLocation;

            origin = targetVector;

            blastRadius = tileRadius;

            Vector2 targetTile = ModUtility.PositionToTile(targetVector);

            int impactRadius = Math.Min(10, tileRadius);

            for (int i = 0; i < impactRadius; i++)
            {

                blastVectors[i] = ModUtility.GetTilesWithinRadius(targetLocation, targetTile, i);

            }

        }

        public void ConicSetup(GameLocation targetLocation, Vector2 startVector, Vector2 endVector, int tileRadius)
        {

            blastAxe = new();

            blastAxe.UpgradeLevel = Mod.instance.PowerLevel - 1;

            blastAxe.lastUser = Game1.player;

            blastPickaxe = new();

            blastPickaxe.UpgradeLevel = Mod.instance.PowerLevel - 1;

            blastPickaxe.lastUser = Game1.player;

            location = targetLocation;

            origin = startVector;

            float travel = Vector2.Distance(endVector, startVector);

            float increments = blastSpeed * 64 * (travel / 384);

            List<Vector2> tileVectors;

            int impactRadius = Math.Min(10, tileRadius);

            if (impactRadius == 0)
            {

                tileVectors = ModUtility.GetTilesBetweenPositions(targetLocation, endVector, startVector, impactRadius);
            }
            else
            {

                tileVectors = ModUtility.GetTilesWithinCone(targetLocation, endVector, startVector, impactRadius);

            }

            foreach (Vector2 tileVector in tileVectors)
            {

                int distance = (int)(Vector2.Distance(startVector, tileVector*64f)/increments);

                distance = Math.Max(1, distance);

                if (!blastVectors.ContainsKey(distance))
                {

                    blastVectors[distance] = new List<Vector2>();

                }

                blastVectors[distance].Add(tileVector);

                if(distance > blastRadius)
                {

                    blastRadius = distance;

                }

            }

        }

        public override void EventDecimal()
        {

            for(int i = 0; i <= blastSpeed; i++)
            {

                BlastProgress();

            }


        }

        public void BlastProgress()
        {

            blastProgression++;

            if (blastProgression > blastRadius)
            {

                eventComplete = true;

                return;

            }

            if (!blastVectors.ContainsKey(blastProgression))
            {

                return;

            }

            foreach(KeyValuePair<SpellHandle.Effects, bool> explosive in blastEffects)
            {

                if (!explosive.Value) { continue; }

                switch (explosive.Key)
                {

                    case SpellHandle.Effects.swipe:

                        SwipeTargets(blastVectors[blastProgression]);

                        break;

                    case SpellHandle.Effects.hack:

                        HackTargets(blastVectors[blastProgression]);

                        break;

                    case SpellHandle.Effects.toolhack:

                        HackTargets(blastVectors[blastProgression], true);

                        break;

                    case SpellHandle.Effects.sunder:

                        SunderTargets(blastVectors[blastProgression]);

                        break;

                    case SpellHandle.Effects.breaker:

                        BreakTargets(blastVectors[blastProgression]);

                        break;

                    case SpellHandle.Effects.toolbreak:

                        BreakTargets(blastVectors[blastProgression],true);

                        break;

                    case SpellHandle.Effects.reave:

                        ReaveTargets(blastVectors[blastProgression]);

                        break;

                    case SpellHandle.Effects.douse:

                        DouseTargets(blastVectors[blastProgression]);

                        break;

                }

            }

        }

        public Microsoft.Xna.Framework.Rectangle BlastRectangle(List<Vector2> targetTiles)
        {

            Microsoft.Xna.Framework.Rectangle blastRectangle = new((int)targetTiles.First().X, (int)targetTiles.First().Y, 1, 1);

            if (targetTiles.Count > 0)
            {

                int nearestX = blastRectangle.X;

                int furthestX = blastRectangle.X;

                int nearestY = blastRectangle.Y;

                int furthestY = blastRectangle.Y;

                foreach (Vector2 tile in targetTiles)
                {

                    int tileX = (int)tile.X;

                    int tileY = (int)tile.Y;

                    if (tileX <= nearestX)
                    {

                        nearestX = tileX;

                    }
                    else if (tileX >= furthestX)
                    {

                        furthestX = tileX;

                    }

                    if (tileY <= nearestY)
                    {

                        nearestY = tileY;

                    }
                    else if (tileY >= furthestY)
                    {

                        furthestY = tileY;

                    }

                }

                blastRectangle = new(nearestX, nearestY, furthestX - nearestX + 1, furthestY - nearestY + 1);

            }

            blastRectangle.X *= 64;

            blastRectangle.Y *= 64;

            blastRectangle.Width *= 64;

            blastRectangle.Height *= 64;

            return blastRectangle;

        }

        public void SunderTargets(List<Vector2> targetTiles)
        {

            Microsoft.Xna.Framework.Rectangle blastRectangle = BlastRectangle(targetTiles);

            for (int index = location.resourceClumps.Count - 1; index >= 0; index--)
            {

                ResourceClump resourceClump = location.resourceClumps[index];

                if (resourceClump.getBoundingBox().Intersects(blastRectangle))
                {

                    switch (resourceClump.parentSheetIndex.Value)
                    {
                        case ResourceClump.stumpIndex:
                        case ResourceClump.hollowLogIndex:

                            DestroyStump(location, resourceClump, resourceClump.Tile);

                            break;

                        default:

                            DestroyBoulder(location, resourceClump, resourceClump.Tile);

                            break;

                    }

                }

            }

        }

        public void SwipeTargets(List<Vector2> targetTiles)
        {

            foreach (Vector2 tileVector in targetTiles)
            {

                if (location.objects.ContainsKey(tileVector))
                {

                    SwipeObject(location, tileVector);

                }

                if (location.terrainFeatures.ContainsKey(tileVector))
                {

                    SwipeFeature(location, tileVector);

                }

            }

        }

        public void BreakTargets(List<Vector2> targetTiles, bool held = false)
        {

            foreach (Vector2 tileVector in targetTiles)
            {

                if (location.objects.ContainsKey(tileVector))
                {

                    BreakObject(location, tileVector, held ? blastPickaxe : null);

                }

            }

        }

        public void HackTargets(List<Vector2> targetTiles, bool held = false)
        {

            foreach (Vector2 tileVector in targetTiles)
            {

                if (location.terrainFeatures.ContainsKey(tileVector))
                {

                    HackFeature(location, tileVector, held ? blastAxe : null);

                }

            }

        }
        public void ReaveTargets(List<Vector2> targetTiles)
        {

            Layer backLayer = location.Map.GetLayer("Back");

            int wet = Game1.IsRainingHere(location) && location.IsOutdoors && !location.Name.Equals("Desert") ? 1 : 0;

            foreach (Vector2 tileVector in targetTiles)
            {

                if (!location.terrainFeatures.ContainsKey(tileVector))
                {

                    ReaveFeature(location, tileVector, backLayer, wet);

                }

            }

        }

        public void DouseTargets(List<Vector2> targetTiles)
        {

            if(Game1.IsRainingHere(location) || !location.IsOutdoors || location.Name.Equals("Desert"))
            {

                return;

            }

            foreach (Vector2 tileVector in targetTiles)
            {

                if (location.terrainFeatures.ContainsKey(tileVector))
                {

                    DouseTile(location, tileVector);

                }

            }

        }

        // ======================== ENVIRONMENT INTERACTIONS

        public static void BreakObject(GameLocation targetLocation, Vector2 tileVector, Pickaxe blastPickaxe = null)
        {

            StardewValley.Object targetObject = targetLocation.objects[tileVector];

            if (targetObject is Fence || targetObject is Workbench || targetObject is Furniture || targetObject is Chest)
            {

                // do nothing

            }
            else
            if (targetObject.IsBreakableStone())
            {

                float stamina = Game1.player.Stamina;

                if (blastPickaxe == null)
                {

                    targetObject.MinutesUntilReady = 1;

                    Mod.instance.virtualPick.DoFunction(targetLocation, (int)tileVector.X * 64, (int)tileVector.Y * 64, 5, Game1.player);

                }
                else
                {

                    if (TileGrid(tileVector))
                    {

                        Mod.instance.iconData.ImpactIndicator(targetLocation, tileVector * 64, IconData.impacts.critical, 4, new());

                    }

                    blastPickaxe.DoFunction(targetLocation, (int)tileVector.X * 64, (int)tileVector.Y * 64, 5, Game1.player);

                }

                Game1.player.Stamina = stamina;

            }
            else if (targetObject.GetContextTags().Contains("category_litter"))
            {

                float stamina = Game1.player.Stamina;

                if (blastPickaxe == null)
                {

                    targetObject.MinutesUntilReady = 1;

                    Mod.instance.virtualPick.DoFunction(targetLocation, (int)tileVector.X * 64, (int)tileVector.Y * 64, 5, Game1.player);

                }
                else
                {


                    if (TileGrid(tileVector))
                    {

                        Mod.instance.iconData.ImpactIndicator(targetLocation, tileVector * 64, IconData.impacts.critical, 4, new());

                    }

                    blastPickaxe.DoFunction(targetLocation, (int)tileVector.X * 64, (int)tileVector.Y * 64, 5, Game1.player);

                }

                Game1.player.Stamina = stamina;

            }
            else
            {

                // ----------------- dislodge craftable

                for (int j = 0; j < 2; j++)
                {

                    Tool toolUse = j == 0 ? Mod.instance.virtualPick : Mod.instance.virtualAxe;

                    if (targetLocation.objects.ContainsKey(tileVector) && targetObject.performToolAction(toolUse))
                    {
                        
                        targetObject.performRemoveAction();

                        targetObject.dropItem(targetLocation, tileVector * 64, tileVector * 64 + new Vector2(0, 32));

                        targetLocation.objects.Remove(tileVector);

                    }

                }

            }

        }

        public static void ReaveFeature(GameLocation targetLocation, Vector2 tileVector, Layer backLayer = null, int wet = -1)
        {

            if (backLayer == null)
            {

                backLayer = targetLocation.Map.GetLayer("Back");

            }

            if (wet == -1)
            {

                wet = Game1.IsRainingHere(targetLocation) && targetLocation.IsOutdoors && !targetLocation.Name.Equals("Desert") ? 1 : 0;

            }

            Dictionary<string, List<Vector2>> neighbours = ModUtility.NeighbourCheck(targetLocation, tileVector, 0, 0);

            string ground = ModUtility.GroundCheck(targetLocation, tileVector);

            if (ground == "ground" && neighbours.Count == 0)
            {

                int tilex = (int)tileVector.X;
                int tiley = (int)tileVector.Y;

                Tile backTile = backLayer.Tiles[tilex, tiley];

                if (backTile.TileIndexProperties.TryGetValue("Diggable", out _))
                {

                    if (TileGrid(tileVector))
                    {

                        Mod.instance.iconData.ImpactIndicator(targetLocation, tileVector * 64, IconData.impacts.dust, 4, new() { alpha = 0.5f, });

                    }

                    targetLocation.checkForBuriedItem(tilex, tiley, explosion: false, detectOnly: false, Game1.player);

                    targetLocation.terrainFeatures.Add(tileVector, new HoeDirt(wet, targetLocation));

                }

            }

        }

        public static void HackFeature(GameLocation targetLocation, Vector2 tileVector, Axe blastAxe = null)
        {

            if (!targetLocation.terrainFeatures.ContainsKey(tileVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[tileVector] is not Tree targetTree)
            {
                
                return;

            }

            if (targetTree.falling.Value)
            {

                return;

            }
                
            if (targetTree.growthStage.Value == 0)
            {

                targetTree.performToolAction(Mod.instance.virtualHoe, 0, tileVector);

                targetLocation.terrainFeatures.Remove(tileVector);

                return;

            }

            if (targetTree.growthStage.Value >= 5)
            {

                if (targetTree.Location is Town)
                {

                    targetTree.Location = Game1.getFarm();

                }

                if (targetLocation.objects.ContainsKey(tileVector))
                {

                    StardewValley.Object targetObject = targetLocation.objects[tileVector];

                    if (targetObject.IsTapper() && targetObject.performToolAction(Mod.instance.virtualAxe))
                    {

                        targetObject.performRemoveAction();

                        targetObject.dropItem(targetLocation, tileVector * 64, tileVector * 64 + new Vector2(0, 32));

                        targetLocation.objects.Remove(tileVector);

                    }

                }

                if (blastAxe == null)
                {

                    targetTree.performToolAction(Mod.instance.virtualAxe, (int)targetTree.health.Value, tileVector);

                }
                else
                {

                    Mod.instance.iconData.ImpactIndicator(targetLocation, tileVector * 64, IconData.impacts.slashes, 4, new());

                    targetTree.performToolAction(blastAxe, 0, tileVector);

                    targetTree.performToolAction(blastAxe, 0, tileVector);

                }

                targetTree.Location = targetLocation;

                return;

            }

            WildTreeData data = targetTree.GetData();

            if (data != null && data.SeedItemId != null)
            {

                targetLocation.debris.Add(new Debris(ItemQueryResolver.TryResolveRandomItem(data.SeedItemId, new ItemQueryContext(targetLocation, Game1.player, null, null)), tileVector * 64f));

            }

            if (targetTree.Location is Town)
            {

                targetTree.Location = Game1.getFarm();


            }

            targetTree.performToolAction(Mod.instance.virtualAxe, 0, tileVector);

            targetLocation.terrainFeatures.Remove(tileVector);

            targetTree.Location = targetLocation;

        }

        public static void DestroyBoulder(GameLocation targetLocation, ResourceClump resourceClump, Vector2 targetVector, bool extraDebris = false)
        {
            Random random = new Random();

            resourceClump.health.Set(1f);

            resourceClump.performToolAction(Mod.instance.virtualPick, 1, targetVector);

            resourceClump.NeedsUpdate = false;

            HerbalHandle.RandomOmen(targetVector * 64, 6);

            if (targetLocation._activeTerrainFeatures.Contains(resourceClump))
            {

                targetLocation._activeTerrainFeatures.Remove(resourceClump);

            }

            if (targetLocation.resourceClumps.Contains(resourceClump))
            {

                targetLocation.resourceClumps.Remove(resourceClump);

            }

            if (!extraDebris)
            {

                return;

            }

            int debris = 2;

            if (Game1.player.professions.Contains(22))
            {
                debris = 4;
            }

            for (int index = 0; index < random.Next(1, debris); ++index)
            {
                switch (resourceClump.parentSheetIndex.Value)
                {
                    case 756:
                    case 758:

                        targetLocation.debris.Add(new Debris(ItemRegistry.Create("(O)536"), targetVector * 64f));

                        break;

                    default:

                        if (targetLocation is MineShaft)
                        {
                            MineShaft mineShaft = (MineShaft)targetLocation;

                            if (mineShaft.mineLevel >= 80)
                            {

                                targetLocation.debris.Add(new Debris(ItemRegistry.Create("(O)537"), targetVector * 64f));

                                break;
                            }
                            if (mineShaft.mineLevel >= 121)
                            {

                                targetLocation.debris.Add(new Debris(ItemRegistry.Create("(O)749"), targetVector * 64f));

                                break;
                            }
                        }

                        targetLocation.debris.Add(new Debris(ItemRegistry.Create("(O)535"), targetVector * 64f));

                        break;
                }
            }


        }

        public static void DestroyStump(GameLocation targetLocation, ResourceClump resourceClump, Vector2 targetVector, bool extraDebris = false)
        {
            resourceClump.health.Set(1f);

            resourceClump.performToolAction(Mod.instance.virtualAxe, 1, targetVector);

            Game1.createMultipleObjectDebris("(O)388", (int)resourceClump.Tile.X, (int)resourceClump.Tile.Y, 20);

            Game1.createMultipleObjectDebris("(O)709", (int)resourceClump.Tile.X, (int)resourceClump.Tile.Y, Mod.instance.PowerLevel);

            resourceClump.NeedsUpdate = false;

            HerbalHandle.RandomOmen(targetVector * 64, 4);

            if (targetLocation._activeTerrainFeatures.Contains(resourceClump))
            {

                targetLocation._activeTerrainFeatures.Remove(resourceClump);

            }

            if (targetLocation.resourceClumps.Contains(resourceClump))
            {

                targetLocation.resourceClumps.Remove(resourceClump);

            }

        }

        public static void SwipeObject(GameLocation targetLocation, Vector2 tileVector)
        {

            StardewValley.Object targetObject = targetLocation.objects[tileVector];

            if (targetObject is Fence || targetObject is Workbench || targetObject is Furniture || targetObject is Chest)
            {

                // do nothing

            }
            else if (targetObject.IsTwig() || targetObject.QualifiedItemId == "(O)169")
            {

                targetObject.onExplosion(Game1.player);

                targetLocation.debris.Add(new Debris(ItemRegistry.Create("(O)388", Mod.instance.randomIndex.Next(1, 3)), tileVector * 64f));

                targetLocation.objects.Remove(tileVector);

            }
            else if (targetObject.IsWeeds())
            {

                string spawnSeed = SpawnData.SeasonalSeed();

                if (spawnSeed != string.Empty)
                {

                    targetLocation.debris.Add(new Debris(ItemRegistry.Create(spawnSeed), tileVector * 64f));

                }

                targetObject.onExplosion(Game1.player);

                targetLocation.objects.Remove(tileVector);

            }
            else if (targetObject.Name.Contains("SupplyCrate"))
            {

                targetObject.MinutesUntilReady = 1;

                targetObject.performToolAction(Mod.instance.virtualPick);

                targetLocation.objects.Remove(tileVector);

            }
            else if (targetObject is BreakableContainer breakableContainer)
            {

                breakableContainer.releaseContents(Game1.player);

                targetLocation.objects.Remove(tileVector);

                targetLocation.playSound("barrelBreak");

            }
            else if (targetObject.QualifiedItemId == "(O)590")
            {

                targetLocation.digUpArtifactSpot((int)tileVector.X, (int)tileVector.Y, Game1.player);

                targetLocation.objects.Remove(tileVector);

            }
            else if (targetObject.QualifiedItemId == "(O)SeedSpot")
            {

                Item raccoonSeedForCurrentTimeOfYear = Utility.getRaccoonSeedForCurrentTimeOfYear(Game1.player, Mod.instance.randomIndex);

                Game1.createMultipleItemDebris(raccoonSeedForCurrentTimeOfYear, tileVector * 64f, 2, targetLocation);

                targetLocation.objects.Remove(tileVector);

            }

        }

        public static void SwipeFeature(GameLocation targetLocation, Vector2 tileVector)
        {

            if (targetLocation.terrainFeatures[tileVector] is Grass grassFeature)
            {

                grassFeature.performToolAction(null, 4, tileVector);

                targetLocation.terrainFeatures.Remove(tileVector);

                if (Mod.instance.randomIndex.Next(2) == 0)
                {

                    targetLocation.debris.Add(new Debris(ItemRegistry.Create("(O)771"), tileVector * 64f));

                }

                return;

            }

        }

        public static void DouseTile(GameLocation location, Vector2 tile)
        {

            if (location.terrainFeatures[tile] is HoeDirt hoeDirt)
            {

                if (TileGrid(tile))
                {

                    Mod.instance.iconData.ImpactIndicator(location, tile * 64, IconData.impacts.drops, 4, new() { alpha = 0.5f, });

                }

                if (hoeDirt.state.Value == 0)
                {

                    hoeDirt.state.Value = 1;

                }

            }

        }

        public static bool TileGrid(Vector2 vector)
        {

            bool xEven = vector.X % 2 == 0;

            bool yEven = vector.Y % 2 == 0;

            if(xEven && yEven)
            {

                return true;

            }

            if(!xEven && !yEven)
            {

                return true;

            }

            return false;

        }

    }

}
