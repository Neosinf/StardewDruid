using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Weald;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Buffs;
using StardewValley.Buildings;
using StardewValley.Characters;
using StardewValley.Enchantments;
using StardewValley.GameData;
using StardewValley.GameData.Crops;
using StardewValley.GameData.FarmAnimals;
using StardewValley.GameData.WildTrees;
using StardewValley.Internal;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.SpecialOrders;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using xTile;
using xTile.Dimensions;
using xTile.Layers;
using xTile.ObjectModel;
using xTile.Tiles;


namespace StardewDruid
{
    static class ModUtility
    {

        // ======================== Animations

        public static void AnimateRandomFish(GameLocation location, Vector2 vector)
        {
            if (vector == Vector2.Zero)
            {
                return;
            }

            Vector2 position = vector * 64;

            if (!Utility.isOnScreen(position, 128))
            {

                return;

            }

            if (location == null)
            {
                return;
            }

            List<int> fishIndexes = new() { 138, 146, 717, };

            int fishIndex = fishIndexes[Game1.random.Next(fishIndexes.Count)];

            AnimateFishJump(location, position - new Vector2(32, 64), fishIndex, true);

            AnimateFishJump(location, position + new Vector2(32, 64), fishIndex, false);

            AnimateFishJump(location, position - new Vector2(32, 0), fishIndex, true);

            AnimateFishJump(location, position + new Vector2(32, 0), fishIndex, false);

        }

        public static void AnimateFishJump(GameLocation targetLocation, Vector2 targetPosition, int fishIndex, bool fishDirection)
        {

            if (!Utility.isOnScreen(targetPosition, 128))
            {

                return;

            }

            Vector2 targetVector = targetPosition;

            Vector2 fishPosition;

            Vector2 sloshPosition;

            Vector2 sloshMotion;

            Vector2 sloshAcceleration;

            bool fishFlip;

            float fishRotate;

            switch (fishDirection)
            {

                case true:

                    fishPosition = new Vector2(targetVector.X - 64, targetVector.Y - 8) + new Vector2(Mod.instance.randomIndex.Next(40));

                    sloshPosition = new Vector2(targetVector.X + 100, targetVector.Y) + new Vector2(Mod.instance.randomIndex.Next(40));

                    sloshMotion = new(0.160f, -0.5f);

                    sloshAcceleration = new(0f, 0.001f);

                    fishFlip = false;

                    fishRotate = 0.025f;

                    targetLocation.playSound(Mod.instance.randomIndex.Next(2) == 0 ? "pullItemFromWater" : "slosh");

                    break;

                default:

                    fishPosition = new Vector2(targetVector.X + 64, targetVector.Y - 8) + new Vector2(Mod.instance.randomIndex.Next(40));

                    sloshPosition = new Vector2(targetVector.X - 128, targetVector.Y) + new Vector2(Mod.instance.randomIndex.Next(40));

                    sloshMotion = new(-0.160f, -0.5f);

                    sloshAcceleration = new(0f, 0.001f);

                    fishFlip = true;

                    fishRotate = -0.025f;

                    break;


            }


            Microsoft.Xna.Framework.Rectangle targetRectangle = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, fishIndex, 16, 16);

            float animationInterval = 1050f;

            float animationSort = targetVector.Y / 10000 + 0.00003f;

            int fishDelay = Mod.instance.randomIndex.Next(300);

            TemporaryAnimatedSprite fishAnimation = new("Maps\\springobjects", targetRectangle, animationInterval, 1, 0, fishPosition, flicker: false, flipped: fishFlip, animationSort, 0f, Color.White, 3f, 0f, 0f, fishRotate)
            {

                motion = sloshMotion,

                acceleration = sloshAcceleration,

                timeBasedMotion = true,

                delayBeforeAnimationStart = fishDelay,

            };

            targetLocation.temporarySprites.Add(fishAnimation);

            // ------------------------------------

            Mod.instance.iconData.ImpactIndicator(targetLocation, fishPosition, IconData.impacts.fish, 2.5f, new() { alpha = 0.7f, delay = fishDelay, });

            // ------------------------------------

            Mod.instance.iconData.ImpactIndicator(targetLocation, sloshPosition, IconData.impacts.splash, 2.5f, new() { alpha = 0.7f, delay = 1000 + fishDelay, });

        }

        public static void AnimateButterflySpray(GameLocation location, Vector2 vector)
        {

            if (vector == Vector2.Zero)
            {
                
                return;
            
            }

            if (!Utility.isOnScreen(vector * 64, 128))
            {

                return;

            }

            if (location == null)
            {
                
                return;
            
            }

            if (location.critters == null)
            {
                
                return;
            
            }

            location.critters.Add(new Butterfly(location, vector, false));

            location.critters.Add(new Butterfly(location, vector - new Vector2(1, 0), false));

            location.critters.Add(new Butterfly(location, vector + new Vector2(1, 0), false));

            location.critters.Add(new Butterfly(location, vector - new Vector2(2, 0), false));

            location.critters.Add(new Butterfly(location, vector + new Vector2(2, 0), false));

            location.critters.Add(new Butterfly(location, vector - new Vector2(1, -1), false));

            location.critters.Add(new Butterfly(location, vector + new Vector2(1, -1), false));

            location.critters.Add(new Butterfly(location, vector - new Vector2(2, -1), false));

            location.critters.Add(new Butterfly(location, vector + new Vector2(2, -1), false));

        }


        // ======================== Gameworld Interactions

        public static void ChangeFriendship(Farmer player, NPC friend, int friendship)
        {

            if (Mod.instance.Helper.ModRegistry.IsLoaded("drbirbdev.SocializingSkill"))
            {

                player.changeFriendship(friendship/5, friend);

                return;

            }

            player.changeFriendship(friendship, friend);

        }

        public static void UpdateFriendship(Farmer player, List<string> NPCIndex, int friendship = 375)
        {

            foreach (string NPCName in NPCIndex)
            {

                if (!player.friendshipData.ContainsKey(NPCName))
                {

                    continue;

                }

                NPC characterFromName = Game1.getCharacterFromName(NPCName);

                characterFromName ??= Game1.getCharacterFromName<Child>(NPCName, mustBeVillager: false);

                if (characterFromName != null)
                {

                    ChangeFriendship(player,characterFromName, friendship);

                }

            }

        }

        public static void PetAnimal(Farmer targetPlayer, FarmAnimal targetAnimal)
        {

            if (targetAnimal.wasPet.Value)
            {

                return;

            }

            targetAnimal.wasPet.Value = true;

            int num = 7;

            if (targetAnimal.wasAutoPet.Value)
            {

                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, targetAnimal.friendshipTowardFarmer.Value + num);

            }
            else
            {

                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, targetAnimal.friendshipTowardFarmer.Value + 15);

            }

            FarmAnimalData animalData = targetAnimal.GetAnimalData();

            int num2 = animalData?.HappinessDrain ?? 0;

            if (animalData != null && animalData.ProfessionForHappinessBoost >= 0 && targetPlayer.professions.Contains(animalData.ProfessionForHappinessBoost))
            {

                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, targetAnimal.friendshipTowardFarmer.Value + 15);

                targetAnimal.happiness.Value = (byte)Math.Min(255, targetAnimal.happiness.Value + Math.Max(5, 30 + num2));

            }

            targetAnimal.doEmote(20);

            targetAnimal.happiness.Value = (byte)Math.Min(255, targetAnimal.happiness.Value + Math.Max(5, 30 + num2));

            targetAnimal.makeSound();

            targetPlayer.gainExperience(0, 5);

        }

        public static List<StardewValley.Object> ExtractCrop(HoeDirt soil, Crop crop, Vector2 tileVector, bool allowIridium)
        {

            Random randomIndex = new();

            List<StardewValley.Object> extracts = new();

            int qualityMax = 0;

            int quantityMin = 1;

            int quantityMax = 0;

            int qualityMost = 4;

            int qualityLeast = 0;

            CropData data = crop.GetData();

            if (data != null)
            {

                if (data.HarvestMinStack > 1)
                {
                    quantityMin = data.HarvestMinStack;
                }

                if (data.HarvestMaxStack > 1)
                {

                    quantityMax = data.HarvestMaxStack - data.HarvestMinStack;

                }

                if (data.HarvestMaxIncreasePerFarmingLevel > 0f)
                {

                    quantityMax += (int)Math.Floor(data.HarvestMaxIncreasePerFarmingLevel * Game1.player.FarmingLevel);

                }

                if (data.ExtraHarvestChance >= 0.0)
                {

                    quantityMax++;

                }

                if (data.ExtraHarvestChance >= 0.5 && Mod.instance.ModDifficulty() <= 6)
                {

                    quantityMax++;

                }

                if (data.HarvestMinQuality != 0)
                {

                    qualityLeast = data.HarvestMinQuality;

                }

                if (data.HarvestMaxQuality.HasValue)
                {

                    qualityMost = (int)data.HarvestMaxQuality;

                }

            }

            if(Mod.instance.Config.cultivateBehaviour == 1 && Mod.instance.ModDifficulty() <= 6)
            {

                quantityMax++;

            }

            if(Game1.player.FarmingLevel > 5 && Mod.instance.ModDifficulty() <= 6)
            {

                qualityMax++;

            }

            if (soil.HasFertilizer())
            {

                int boostSoil = soil.GetFertilizerQualityBoostLevel();

                qualityMax += boostSoil;

                if(boostSoil == 3)
                {

                    allowIridium = true;

                }

            }

            Game1.player.currentLocation.playSound("harvest");

            int quantity = randomIndex.Next(quantityMin, Math.Min(5,quantityMin + quantityMax));

            for (int i = 0; i < quantity; i++)
            {

                int quality = randomIndex.Next(0, 3 + qualityMax);

                if (quality == 3)
                { 
                    
                    quality = randomIndex.Next(2) == 0 ? 2 : 4; 
                
                }

                if(quality > 4)
                {

                    quality = 4;

                }

                if(quality > 2 && !allowIridium)
                {

                    quality = 2;

                }

                if (crop.indexOfHarvest.Value.Contains("771") || crop.indexOfHarvest.Value.Contains("889"))
                {

                    quality = 0;

                }

                if (quality < qualityLeast)
                {

                    quality = qualityLeast;

                }

                if (quality > qualityMost)
                {

                    quality = qualityMost;

                }

                StardewValley.Object extract = crop.programColored.Value ? new ColoredObject(crop.indexOfHarvest.Value, 1, crop.tintColor.Value)
                {

                    Quality = quality

                } : new StardewValley.Object(crop.indexOfHarvest.Value, 1, isRecipe: false, -1, quality);

                if (extract.Category == -80) // abort if flower
                {

                    return new();

                }

                int num6 = extract.Price;

                float num7 = (float)(16.0 * Math.Log(0.018 * num6 + 1.0, Math.E));

                Game1.player.gainExperience(0, (int)Math.Round(num7));

                extracts.Add(extract);

            }

            int num8 = data?.RegrowDays ?? -1;

            if (num8 <= 0)
            {
                soil.destroyCrop(true);

                return extracts;
            }

            crop.fullyGrown.Value = true;

            if (crop.dayOfCurrentPhase.Value == num8)
            {

                crop.updateDrawMath(tileVector * 64);

            }

            crop.dayOfCurrentPhase.Value = num8;

            return extracts;

        }

        public static StardewValley.Object ExtractForage(GameLocation location, Vector2 vector, bool remove = true)
        {

            StardewValley.Object value = (location.objects[vector].getOne() as StardewValley.Object);

            int quality = value.Quality;

            Random random = Utility.CreateDaySaveRandom(vector.X, vector.Y * 777f);

            if (Game1.player.professions.Contains(16))
            {
                value.Quality = 4;
            }
            else
            {
                if (random.NextDouble() < (double)((float)Game1.player.ForagingLevel / 30f))
                {
                    value.Quality = Math.Max(2, quality);
                }
                else if (random.NextDouble() < (double)((float)Game1.player.ForagingLevel / 15f))
                {
                    value.Quality = Math.Max(1, quality);
                }
            }

            location.localSound("pickUpItem");

            if (Game1.player.professions.Contains(13) && random.NextDouble() < 0.2 && !value.questItem.Value && !location.isFarmBuildingInterior())
            {

                value.Stack = 2;

            }

            Game1.player.gainExperience(2, 7 * value.Stack);

            Game1.stats.ItemsForaged++;

            if (remove) {

                location.objects.Remove(vector); 
            
            }

            return value;

        }

        // ======================== Tile Interactions

        public static Vector2 PositionToTile(Vector2 position)
        {

            Vector2 tiled = new((int)((position.X - position.X % 64) / 64), (int)((position.Y - position.Y % 64) / 64));

            return tiled;

        }

        public static bool WaterCheck(GameLocation targetLocation, Vector2 targetVector, int radius = 4)
        {

            bool check = false;

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            Tile backTile = backLayer.Tiles[(int)targetVector.X, (int)targetVector.Y];

            if (backTile == null)
            {

                return false;

            }

            if (backTile.TileIndexProperties.TryGetValue("Water", out _))
            {

                check = true;

            }

            for (int i = 1; i < radius; i++)
            {

                List<Vector2> neighbours = GetTilesWithinRadius(targetLocation, targetVector, i);

                foreach (Vector2 neighbour in neighbours)
                {

                    backTile = backLayer.Tiles[(int)neighbour.X, (int)neighbour.Y];

                    if (backTile != null)
                    {

                        if (!backTile.TileIndexProperties.TryGetValue("Water", out _))
                        {

                            check = false;

                        }

                    }

                }

            }

            return check;

        }

        public static string GroundCheck(GameLocation targetLocation, Vector2 neighbour, bool barrierCheck = false)
        {

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            Tile backTile;

            int mapWidth = targetLocation.Map.DisplayWidth / 64;

            int mapHeight = targetLocation.Map.DisplayHeight / 64;

            int targetX = (int)neighbour.X;

            int targetY = (int)neighbour.Y;

            string assumption = "unknown";

            if (targetX <= 0 || targetX >= mapWidth - 1 || targetY <= 0 || targetY >= mapHeight - 1)
            {

                return "void";

            }

            backTile = backLayer.Tiles[targetX, targetY];

            if (backTile == null)
            {

                return "void";

            }

            if (backTile.TileIndex == 2154)
            {

                return "void";

            }

            if (targetLocation is MineShaft || !targetLocation.IsOutdoors)
            {

                Layer buildingLayer = Game1.player.currentLocation.Map.GetLayer("Buildings");

                Tile buildingTile = buildingLayer.Tiles[targetX, targetY];

                if (buildingTile != null)
                {
                    return "void";

                }

                Layer frontLayer = Game1.player.currentLocation.Map.GetLayer("Front");

                Tile frontTile = frontLayer.Tiles[targetX, targetY];

                if (frontTile != null)
                {
                    return "void";

                }

                if (backTile.TileIndex == 77)
                {

                    return "void";

                }

            }

            if (backTile.TileIndexProperties.TryGetValue("Water", out _))
            {

                return "water";

            }

            if (barrierCheck)
            {

                if (BarrierCheck(targetLocation, neighbour))
                {

                    return "barrier";

                }

                assumption = "ground";

            }

            PropertyValue backing = null;

            backTile.TileIndexProperties.TryGetValue("Type", out backing);

            if (backing != null)
            {

                return "ground";

            }

            if (targetLocation.IsOutdoors)
            {

                List<int> grounds = new() { 304, 351, 404, 356, 300, 305, };

                if (grounds.Contains(backTile.TileIndex))
                {
                    return "ground";

                }

            }

            if (targetLocation is Caldera)
            {

                if (backTile.TileIndex == 28)
                {
                    return "ground";
                }

            }

            if (targetLocation is Sewer)
            {

                if (backTile.TileIndex == 34 || backTile.TileIndex == 41 || backTile.TileIndex == 42)
                {
                    return "ground";
                }

            }

            return assumption;

        }

        public static bool BarrierCheck(GameLocation targetLocation, Vector2 tileVector)
        {

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            Layer buildingLayer = targetLocation.Map.GetLayer("Buildings");

            List<Vector2> checks = new()
            {

                tileVector,
                tileVector+ new Vector2(0,-1),
                tileVector+ new Vector2(1,0),
                tileVector+ new Vector2(0,1),
                tileVector+ new Vector2(-1,0),

            };

            Dictionary<int, bool> found = new()
            {
                [0] = false,
                [1] = false,
                [2] = false,
                [3] = false,
                [4] = false,
            };

            for (int i = 0; i <= 5; i++)
            {

                int targetX = (int)checks[0].X;

                int targetY = (int)checks[0].Y;

                if (targetLocation is FarmCave)
                {

                    if (targetX <= 3 || targetX >= backLayer.LayerWidth - 3)
                    {

                        if (i == 0) { return true; }

                        found[i] = true;

                        break;

                    }

                    if (targetY <= 3 || targetY >= backLayer.LayerHeight - 3)
                    {
                        if (i == 0) { return true; }

                        found[i] = true;

                        break;

                    }


                }


                Tile backTile = backLayer.Tiles[targetX, targetY];

                Tile buildingTile = buildingLayer.Tiles[targetX, targetY];

                PropertyValue barrier = null;

                backTile.Properties.TryGetValue("NPCBarrier", out barrier);

                if (barrier != null) { if (i == 0) { return true; } found[i] = true; break; }

                backTile.Properties.TryGetValue("TemporaryBarrier", out barrier);

                if (barrier != null) { if (i == 0) { return true; } found[i] = true; break; }

                backTile.TileIndexProperties.TryGetValue("Passable", out barrier);

                if (barrier != null) { if (i == 0) { return true; } found[i] = true; break; }

                if (buildingLayer != null)
                {

                    if (buildingTile != null)
                    {

                        buildingTile.TileIndexProperties.TryGetValue("Shadow", out barrier);

                        if (barrier != null) { if (i == 0) { return true; } found[i] = true; break; }

                        buildingTile.TileIndexProperties.TryGetValue("Passable", out barrier);

                        if (barrier != null) { break; }

                        buildingTile.TileIndexProperties.TryGetValue("NPCPassable", out barrier);

                        if (barrier != null) { break; }

                        buildingTile.Properties.TryGetValue("Passable", out barrier);

                        if (barrier != null) { break; }

                        buildingTile.Properties.TryGetValue("NPCPassable", out barrier);

                        if (barrier != null) { break; }

                        if (i == 0) { return true; }

                        found[i] = true; break;

                    }

                }

            }

            if (found[1] && found[2])
            {

                return true;

            }

            if (found[2] && found[3])
            {

                return true;

            }

            if (found[3] && found[4])
            {

                return true;

            }

            if (found[4] && found[1])
            {

                return true;

            }

            return false;

        }

        public static Dictionary<Vector2, string> LocationTargets(GameLocation targetLocation)
        {

            Dictionary<Vector2, string> targetCasts = new();

            if (targetLocation.largeTerrainFeatures.Count > 0)
            {

                foreach (LargeTerrainFeature largeTerrainFeature in targetLocation.largeTerrainFeatures)
                {

                    if (largeTerrainFeature is not Bush bushFeature)
                    {

                        continue;

                    }

                    Vector2 originVector = bushFeature.Tile;

                    targetCasts[originVector] = "Bush";

                    switch (bushFeature.size.Value)
                    {
                        case 0:
                        case 3:
                            targetCasts[new Vector2(originVector.X, originVector.Y + 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X, originVector.Y - 1)] = "Bush";
                            break;
                        case 1:
                        case 4:
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y)] = "Bush";
                            targetCasts[new Vector2(originVector.X, originVector.Y + 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y + 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X, originVector.Y - 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y - 1)] = "Bush";
                            break;
                        case 2:
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 2, originVector.Y)] = "Bush";
                            targetCasts[new Vector2(originVector.X, originVector.Y + 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y + 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 2, originVector.Y + 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X, originVector.Y - 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 1, originVector.Y - 1)] = "Bush";
                            targetCasts[new Vector2(originVector.X + 2, originVector.Y - 1)] = "Bush";
                            break;
                    }

                }

            }

            if (targetLocation.resourceClumps.Count > 0)
            {

                foreach (ResourceClump resourceClump in targetLocation.resourceClumps)
                {

                    Vector2 originVector = resourceClump.Tile;

                    targetCasts[originVector] = "Clump";

                    targetCasts[new Vector2(originVector.X + 1, originVector.Y)] = "Clump";

                    targetCasts[new Vector2(originVector.X, originVector.Y + 1)] = "Clump";

                    targetCasts[new Vector2(originVector.X + 1, originVector.Y + 1)] = "Clump";

                }

            }

            /*
            if (targetLocation is Woods woodsLocation)
            {
                foreach (ResourceClump resourceClump in woodsLocation.stumps)
                {

                    Vector2 originVector = resourceClump.Tile;

                    targetCasts[originVector] = "Clump";

                    targetCasts[new Vector2(originVector.X + 1, originVector.Y)] = "Clump";

                    targetCasts[new Vector2(originVector.X, originVector.Y + 1)] = "Clump";

                    targetCasts[new Vector2(originVector.X + 1, originVector.Y + 1)] = "Clump";

                }

            }*/

            if (targetLocation.furniture.Count > 0)
            {
                foreach (Furniture item in targetLocation.furniture)
                {

                    Vector2 originVector = item.TileLocation;

                    for (int i = 0; i < item.boundingBox.Width / 64; i++)
                    {

                        for (int j = 0; j < item.boundingBox.Height / 64; j++)
                        {

                            targetCasts[new Vector2(originVector.X + i, originVector.Y + j)] = "Furniture";

                        }

                    }

                }

            }
            foreach (Building building in targetLocation.buildings)
            {

                int radius = building.GetAdditionalTilePropertyRadius();

                int cornerX = building.tileX.Value - radius;

                int cornerY = building.tileY.Value - radius;

                int width = building.tilesWide.Value + radius * 2;

                int height = building.tilesHigh.Value + radius * 2;

                for (int i = 0; i < width; i++)
                {

                    for (int j = 0; j < height; j++)
                    {

                        targetCasts[new Vector2(cornerX + i, cornerY + j)] = "Building";

                    }

                }

            }

            return targetCasts;

        }

        public static Dictionary<string, List<Vector2>> NeighbourCheck(GameLocation targetLocation, Vector2 targetVector, int startRadius = 1, int endRadius = 1)
        {

            Dictionary<string, List<Vector2>> neighbourList = new();

            for (int i = startRadius; i <= endRadius; i++)
            {

                List<Vector2> neighbourVectors = GetTilesWithinRadius(targetLocation, targetVector, i);

                Layer buildingLayer = targetLocation.Map.GetLayer("Buildings");

                Layer pathsLayer = targetLocation.Map.GetLayer("Paths");

                if (Mod.instance.mapped != targetLocation.Name)
                {
                    
                    Mod.instance.mapped = targetLocation.Name;

                    Mod.instance.features = LocationTargets(targetLocation);

                }

                foreach (Vector2 neighbourVector in neighbourVectors)
                {

                    if (Mod.instance.features.ContainsKey(neighbourVector))
                    {

                        string targetType = Mod.instance.features[neighbourVector];

                        if (!neighbourList.ContainsKey(targetType))
                        {

                            neighbourList[targetType] = new();

                        }

                        neighbourList[targetType].Add(neighbourVector);

                        continue;

                    }

                    int targetX = (int)neighbourVector.X;

                    int targetY = (int)neighbourVector.Y;

                    Tile buildingTile = buildingLayer.Tiles[targetX, targetY];
                   // Tile buildingTile = buildingLayer.PickTile(new xTile.Dimensions.Location((int)neighbourVector.X * 64, (int)neighbourVector.Y * 64), Game1.viewport.Size);

                    if (buildingTile != null)
                    {

                        if (buildingTile.TileIndexProperties.TryGetValue("Passable", out _) == false)
                        {

                            if (!neighbourList.ContainsKey("Wall"))
                            {

                                neighbourList["Wall"] = new();

                            }

                            neighbourList["Wall"].Add(neighbourVector);

                            continue;

                        }

                        if (targetLocation is Beach)
                        {

                            List<int> tidalList = new() { 60, 61, 62, 63, 77, 78, 79, 80, 94, 95, 96, 97, 104, 287, 288, 304, 305, 321, 362, 363 };

                            if (tidalList.Contains(buildingTile.TileIndex))
                            {

                                neighbourList["Pool"].Add(neighbourVector);

                                continue;

                            }

                        }

                    }

                    if (pathsLayer != null)
                    {

                        Tile pathsTile = buildingLayer.PickTile(new xTile.Dimensions.Location((int)neighbourVector.X * 64, (int)neighbourVector.Y * 64), Game1.viewport.Size);

                        if (pathsTile != null)
                        {

                            if (!neighbourList.ContainsKey("Path"))
                            {

                                neighbourList["Path"] = new();

                            }

                            neighbourList["Path"].Add(neighbourVector);

                        }

                    }

                    if (targetLocation.terrainFeatures.ContainsKey(neighbourVector))
                    {
                        var terrainFeature = targetLocation.terrainFeatures[neighbourVector];

                        switch (terrainFeature.GetType().Name.ToString())
                        {

                            case "FruitTree":

                                if (!neighbourList.ContainsKey("Sapling"))
                                {

                                    neighbourList["Sapling"] = new();

                                }

                                neighbourList["Sapling"].Add(neighbourVector);

                                break;

                            case "Tree":

                                Tree treeCheck = terrainFeature as Tree;

                                if (treeCheck.growthStage.Value >= 5)
                                {

                                    if (!neighbourList.ContainsKey("Tree"))
                                    {

                                        neighbourList["Tree"] = new();

                                    }

                                    neighbourList["Tree"].Add(neighbourVector);

                                }
                                else
                                {

                                    if (!neighbourList.ContainsKey("Sapling"))
                                    {

                                        neighbourList["Sapling"] = new();

                                    }

                                    neighbourList["Sapling"].Add(neighbourVector);

                                }

                                break;

                            case "HoeDirt":

                                HoeDirt hoedCheck = terrainFeature as HoeDirt;

                                if (hoedCheck.crop != null)
                                {

                                    if (!neighbourList.ContainsKey("Crop"))
                                    {

                                        neighbourList["Crop"] = new();

                                    }

                                    neighbourList["Crop"].Add(neighbourVector);

                                }

                                if (!neighbourList.ContainsKey("HoeDirt"))
                                {

                                    neighbourList["HoeDirt"] = new();

                                }

                                neighbourList["HoeDirt"].Add(neighbourVector);

                                break;

                            default:

                                if (!neighbourList.ContainsKey("Feature"))
                                {

                                    neighbourList["Feature"] = new();

                                }

                                neighbourList["Feature"].Add(neighbourVector);

                                break;

                        }

                        continue;

                    }

                    if (targetLocation.objects.ContainsKey(neighbourVector))
                    {

                        if (!neighbourList.ContainsKey("Object"))
                        {

                            neighbourList["Object"] = new();

                        }

                        neighbourList["Object"].Add(neighbourVector);

                        if (targetLocation.objects[neighbourVector] is Fence || targetLocation.objects[neighbourVector] is BreakableContainer || targetLocation.objects[neighbourVector].bigCraftable.Value)
                        {

                            if (!neighbourList.ContainsKey("BigObject"))
                            {

                                neighbourList["BigObject"] = new();

                            }

                            neighbourList["BigObject"].Add(neighbourVector);

                        }

                    }

                }

            }

            return neighbourList;

        }

        static List<Vector2> TilesWithinOne(Vector2 center)
        {

            List<Vector2> result = new() {

                center + new Vector2(0, -1),    // N
                center + new Vector2(1, -1),    // NE
                center + new Vector2(1, 0),     // E
                center + new Vector2(1, 1),     // SE
                center + new Vector2(0, 1),     // S
                center + new Vector2(-1, 1),    // SW
                center + new Vector2(-1, 0),    // W
                center + new Vector2(-1, -1),   // NW

            };

            return result;

        }

        static List<Vector2> TilesWithinTwo(Vector2 center)
        {
            List<Vector2> result = new()
            {

                center + new Vector2(0,-2), // N
                center + new Vector2(1,-2), // NE

                center + new Vector2(2,-1), // NE
                center + new Vector2(2,0), // E
                center + new Vector2(2,1), // SE

                center + new Vector2(1,2), // SE
                center + new Vector2(0,2), // S
                center + new Vector2(-1,2), // SW

                center + new Vector2(-2,1), // SW
                center + new Vector2(-2,0), // W
                center + new Vector2(-2,-1), // NW

                 center + new Vector2(-1,-2), // NW

            };

            return result;

        }

        static List<Vector2> TilesWithinThree(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-3), // N
                center + new Vector2(1,-3),

                center + new Vector2(2,-2), // NE

                center + new Vector2(3,-1), // E
                center + new Vector2(3,0),
                center + new Vector2(3,1),

                center + new Vector2(2,2), // SE

                center + new Vector2(1,3), // S
                center + new Vector2(0,3),
                center + new Vector2(-1,3),

                center + new Vector2(-2,2), // SW

                center + new Vector2(-3,1), // W
                center + new Vector2(-3,0),
                center + new Vector2(-3,-1),

                center + new Vector2(-2,-2), // NW

                center + new Vector2(-1,-3), // NNW
 
            };

            return result;

        }

        static List<Vector2> TilesWithinFour(Vector2 center)
        {
            List<Vector2> result = new() {


                center + new Vector2(0,-4), // N
                center + new Vector2(1,-4),

                center + new Vector2(2,-3),
                center + new Vector2(3,-3), // NE
                center + new Vector2(3,-2),

                center + new Vector2(4,-1), // E
                center + new Vector2(4,0),
                center + new Vector2(4,1),

                center + new Vector2(3,2),
                center + new Vector2(3,3), // SE
                center + new Vector2(2,3),

                center + new Vector2(1,4), // S
                center + new Vector2(0,4),
                center + new Vector2(-1,4),

                center + new Vector2(-2,3),
                center + new Vector2(-3,3), // SW
                center + new Vector2(-3,2),

                center + new Vector2(-4,1), // W
                center + new Vector2(-4,0),
                center + new Vector2(-4,-1),

                center + new Vector2(-3,-2),
                center + new Vector2(-3,-3), // NW
                center + new Vector2(-2,-3),

                center + new Vector2(-1,-4), // NNW

            };

            return result;

        }

        static List<Vector2> TilesWithinFive(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-5), // N
                center + new Vector2(1,-5),

                center + new Vector2(2,-4), // NE
                center + new Vector2(3,-4),
                center + new Vector2(4,-3),
                center + new Vector2(4,-2),

                center + new Vector2(5,-1), // E
                center + new Vector2(5,0),
                center + new Vector2(5,1),

                center + new Vector2(4,2), // SE
                center + new Vector2(4,3),
                center + new Vector2(3,4),
                center + new Vector2(2,4),

                center + new Vector2(1,5), // S
                center + new Vector2(0,5),
                center + new Vector2(-1,5),

                center + new Vector2(-2,4), // SW
                center + new Vector2(-3,4),
                center + new Vector2(-4,3),
                center + new Vector2(-4,2),

                center + new Vector2(-5,1), // W
                center + new Vector2(-5,0),
                center + new Vector2(-5,-1),

                center + new Vector2(-4,-2), // NW
                center + new Vector2(-4,-3),
                center + new Vector2(-3,-4),
                center + new Vector2(-2,-4),

                center + new Vector2(-1,-5), // NNW

            };

            return result;

        }

        static List<Vector2> TilesWithinSix(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-6), // N
                center + new Vector2(1,-6),

                center + new Vector2(2,-5),
                center + new Vector2(3,-5),
                center + new Vector2(4,-4), // NE
                center + new Vector2(5,-3),
                center + new Vector2(5,-2),

                center + new Vector2(6,-1),
                center + new Vector2(6,0), // E
                center + new Vector2(6,1),

                center + new Vector2(5,2),
                center + new Vector2(5,3),
                center + new Vector2(4,4), // SE
                center + new Vector2(3,5),
                center + new Vector2(2,5),

                center + new Vector2(1,6),
                center + new Vector2(0,6), // S
                center + new Vector2(-1,6),

                center + new Vector2(-2,5),
                center + new Vector2(-3,5),
                center + new Vector2(-4,4), // SW
                center + new Vector2(-5,3),
                center + new Vector2(-5,2),

                center + new Vector2(-6,1),
                center + new Vector2(-6,0), // W
                center + new Vector2(-6,-1),

                center + new Vector2(-5,-2),
                center + new Vector2(-5,-3),
                center + new Vector2(-4,-4), // NW
                center + new Vector2(-3,-5),
                center + new Vector2(-2,-5),

                center + new Vector2(-1,-6), // NNW

            };

            return result;

        }

        static List<Vector2> TilesWithinSeven(Vector2 center, bool smooth = false)
        {
            List<Vector2> result;

            if (smooth)
            {

                result = new() {

                    center + new Vector2(0,-7), // N
                    center + new Vector2(1,-7),

                    center + new Vector2(2,-6),
                    center + new Vector2(3,-6),

                    center + new Vector2(4,-5), // NE
                    center + new Vector2(5,-4), // NE

                    center + new Vector2(6,-3),
                    center + new Vector2(6,-2),

                    center + new Vector2(7,-1),
                    center + new Vector2(7,0), // E
                    center + new Vector2(7,1),

                    center + new Vector2(6,2),
                    center + new Vector2(6,3),

                    center + new Vector2(5,4), // SE
                    center + new Vector2(4,5), // SE

                    center + new Vector2(3,6),
                    center + new Vector2(2,6),

                    center + new Vector2(1,7),
                    center + new Vector2(0,7), // S
                    center + new Vector2(-1,7),

                    center + new Vector2(-2,6),
                    center + new Vector2(-3,6),

                    center + new Vector2(-4,5), // SW
                    center + new Vector2(-5,4), // SW

                    center + new Vector2(-6,3),
                    center + new Vector2(-6,2),

                    center + new Vector2(-7,1),
                    center + new Vector2(-7,0), // W
                    center + new Vector2(-7,-1),

                    center + new Vector2(-6,-2),
                    center + new Vector2(-6,-3),

                    center + new Vector2(-5,-4), // NW
                    center + new Vector2(-4,-5), // NW

                    center + new Vector2(-3,-6),
                    center + new Vector2(-2,-6),

                    center + new Vector2(-1,-7), // NNW

                };

                return result;

            }

            result = new() {

                center + new Vector2(0,-7), // N
                center + new Vector2(1,-7),

                center + new Vector2(2,-6),
                //center + new Vector2(3,-6),

                center + new Vector2(4,-5), // NE
                center + new Vector2(5,-4), // NE

                //center + new Vector2(6,-3),
                center + new Vector2(6,-2),

                center + new Vector2(7,-1),
                center + new Vector2(7,0), // E
                center + new Vector2(7,1),

                center + new Vector2(6,2),
                //center + new Vector2(6,3),

                center + new Vector2(5,4), // SE
                center + new Vector2(4,5), // SE

                //center + new Vector2(3,6),
                center + new Vector2(2,6),

                center + new Vector2(1,7),
                center + new Vector2(0,7), // S
                center + new Vector2(-1,7),

                center + new Vector2(-2,6),
                //center + new Vector2(-3,6),

                center + new Vector2(-4,5), // SW
                center + new Vector2(-5,4), // SW

                //center + new Vector2(-6,3),
                center + new Vector2(-6,2),

                center + new Vector2(-7,1),
                center + new Vector2(-7,0), // W
                center + new Vector2(-7,-1),

                center + new Vector2(-6,-2),
                //center + new Vector2(-6,-3),

                center + new Vector2(-5,-4), // NW
                center + new Vector2(-4,-5), // NW

                //center + new Vector2(-3,-6),
                center + new Vector2(-2,-6),

                center + new Vector2(-1,-7), // NNW

            };

            return result;

        }

        static List<Vector2> TilesWithinEight(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-8),

                center + new Vector2(2,-7),
                center + new Vector2(3,-6),

                center + new Vector2(4,-6),
                center + new Vector2(5,-5), // NE
                center + new Vector2(6,-4),

                center + new Vector2(6,-3),
                center + new Vector2(7,-2),

                center + new Vector2(8,0),

                center + new Vector2(7,2),
                center + new Vector2(6,3),

                center + new Vector2(6,4),
                center + new Vector2(5,5), // SE
                center + new Vector2(4,6),

                center + new Vector2(3,6),
                center + new Vector2(2,7),

                center + new Vector2(0,8),

                center + new Vector2(-2,7),
                center + new Vector2(-3,6),

                center + new Vector2(-4,6),
                center + new Vector2(-5,5), // SW
                center + new Vector2(-6,4),

                center + new Vector2(-6,3),
                center + new Vector2(-7,2),

                center + new Vector2(-8,0),

                center + new Vector2(-7,-2),
                center + new Vector2(-6,-3),

                center + new Vector2(-6,-4),
                center + new Vector2(-5,-5), // NW
                center + new Vector2(-4,-6),

                center + new Vector2(-3,-6),
                center + new Vector2(-2,-7),

            };

            return result;

        }

        public static List<Vector2> GetTilesWithinRadius(GameLocation location, Vector2 center, int level, bool onmap = true, int segment = -1)
        {

            List<Vector2> templateList;

            switch (level)
            {
                case 1:
                    templateList = TilesWithinOne(center);
                    break;
                case 2:
                    templateList = TilesWithinTwo(center);
                    break;
                case 3:
                    templateList = TilesWithinThree(center);
                    break;
                case 4:
                    templateList = TilesWithinFour(center);
                    break;
                case 5:
                    templateList = TilesWithinFive(center);
                    break;
                case 6:
                    templateList = TilesWithinSix(center);
                    break;
                case 7:
                    templateList = TilesWithinSeven(center, !onmap);
                    break;
                case 8:
                    templateList = TilesWithinEight(center);
                    break;
                default: // 0
                    templateList = new() { center, };
                    break;

            }

            if (segment != -1)
            {

                List<Vector2> segmentList = new();

                int total = templateList.Count;

                float segmentLength = total / 8;

                int segmentGrab = (int)Math.Ceiling((double)(total / 3)) - 1;

                int segmentStart = (int)(segmentLength * segment);

                segmentList.Add(templateList[segmentStart]);

                int segmentIndex;

                int j = 1;

                for (int i = 0; i < segmentGrab; i++)
                {

                    if (i % 2 == 0)
                    {

                        segmentIndex = (segmentStart + j + total) % total;

                    }
                    else
                    {

                        segmentIndex = (segmentStart + j + total) % total;

                        j++;

                    }

                    segmentList.Add(templateList[segmentIndex]);

                }

                templateList = segmentList;

            }

            if (!onmap)
            {

                return templateList;


            }

            List<Vector2> vectorList = new();

            foreach (Vector2 testVector in templateList)
            {

                if (location.isTileOnMap(testVector))
                {

                    vectorList.Add(testVector);

                }

            }

            return vectorList;

        }

        public static List<Vector2> GetTilesBetweenPositions(GameLocation location, Vector2 distant, Vector2 near, float limit = -1)
        {

            List<Vector2> vectorList = new();

            float increment = limit * 2;

            if (limit == -1)
            {

                increment = Vector2.Distance(distant, near) / 32;

            }

            Vector2 factor = PathFactor(near, distant) * 32;

            Vector2 check = near + factor;

            for (int i = 1; i <= increment; i++)
            {

                check += factor;

                Vector2 tile = PositionToTile(check);

                if (!vectorList.Contains(tile) && tile != near && tile != distant)
                {
                    vectorList.Add(tile);
                }

            }

            return vectorList;

        }

        public static List<Vector2> GetOccupiableTilesNearby(GameLocation location, Vector2 target, int segment = 0, int proximity = 0, int margin = 0)
        {

            List<Vector2> occupiable = new();

            for (int i = 0; i < margin + 1; i++)
            {

                List<Vector2> leads = GetTilesWithinRadius(location, target, i + proximity, true, segment);

                foreach (Vector2 lead in leads)
                {

                    if (TileAccessibility(location, lead) == 0)
                    {

                        occupiable.Add(lead);

                    }

                }


            }

            return occupiable;

        }

        public static List<Vector2> GetOccupiedTilesWithinRadius(GameLocation location, Vector2 center, int level)
        {

            List<Vector2> occupied = new();

            for (int i = 0; i < level; i++)
            {

                List<Vector2> leads = GetTilesWithinRadius(location, center, i + 1, true);

                foreach (Vector2 lead in leads)
                {

                    if (TileAccessibility(location, lead) != 0)
                    {

                        occupied.Add(lead);

                    }

                }

            }

            return occupied;

        }

        public static List<int> DirectionToCenter(GameLocation location, Vector2 position)
        {

            int layerWidth = location.map.Layers[0].LayerWidth;

            int layerHeight = location.map.Layers[0].LayerHeight;

            int midWidth = layerWidth / 2 * 64;

            int midHeight = layerHeight / 2 * 64;

            Vector2 centerPosition = new(midWidth, midHeight);

            List<int> directions = DirectionToTarget(position, centerPosition);

            return directions;

        }

        public static List<int> DirectionToTarget(Vector2 origin, Vector2 target)
        {

            List<int> directions = new();

            int moveDirection;

            int altDirection;

            int diagonal;

            int straight = 0;

            Vector2 difference = new(target.X - origin.X, target.Y - origin.Y);

            double radian = Math.Atan2(difference.Y, difference.X);

            double pie = Math.PI / 8;

            if (difference.Y == 0f)
            {

                if (difference.X == 0f)
                {

                    moveDirection = 2;
                    altDirection = 1;
                    diagonal = 3;

                }
                else if (difference.X > 0f)
                {

                    straight = 1;

                    moveDirection = 1;
                    altDirection = 1;
                    diagonal = 2;

                }
                else
                {

                    straight = 1;

                    moveDirection = 3;
                    altDirection = 3;
                    diagonal = 6;

                }

            }
            else if (difference.X == 0f)
            {

                if (difference.Y >= 0f)
                {

                    moveDirection = 2;
                    altDirection = 1;
                    diagonal = 3;

                }
                else
                {

                    moveDirection = 0;
                    altDirection = 1;
                    diagonal = 0;

                }

            }
            else if (radian > 0.00)
            {

                straight = 1;

                if (radian < pie)
                {
 
                    moveDirection = 1;
                    altDirection = 1;
                    diagonal = 2;

                }
                else if (radian < pie * 2)
                {

                    moveDirection = 1;
                    altDirection = 1;
                    diagonal = 3;

                }
                else if (radian < pie * 3)
                {

                    moveDirection = 2;
                    altDirection = 1;
                    diagonal = 3;

                }
                else if (radian < pie * 4)
                {

                    moveDirection = 2;
                    altDirection = 1;
                    diagonal = 4;

                }
                else if (radian < pie * 5)
                {

                    moveDirection = 2;
                    altDirection = 3;
                    diagonal = 4;

                }
                else if (radian < pie * 6)
                {

                    moveDirection = 2;
                    altDirection = 3;
                    diagonal = 5;

                }
                else if (radian < pie * 7)
                {

                    moveDirection = 3;
                    altDirection = 3;
                    diagonal = 5;

                }
                else
                {

                    moveDirection = 3;
                    altDirection = 3;
                    diagonal = 6;

                }

            }
            else
            {

                straight = 1;

                radian = Math.Abs(radian);

                if (radian < pie)
                {

                    moveDirection = 1;
                    altDirection = 1;
                    diagonal = 2;

                }
                else if (radian < pie * 2)
                {

                    moveDirection = 1;
                    altDirection = 1;
                    diagonal = 1;

                }
                else if (radian < pie * 3)
                {

                    moveDirection = 0;
                    altDirection = 1;
                    diagonal = 1;

                }
                else if (radian < pie * 4)
                {
 
                    moveDirection = 0;
                    altDirection = 1;
                    diagonal = 0;

                }
                else if (radian < pie * 5)
                {

                    moveDirection = 0;
                    altDirection = 3;
                    diagonal = 0;

                }
                else if (radian < pie * 6)
                {

                    moveDirection = 0;
                    altDirection = 3;
                    diagonal = 7;

                }
                else if (radian < pie * 7)
                {

                    moveDirection = 3;
                    altDirection = 3;
                    diagonal = 7;

                }
                else
                {

                    moveDirection = 3;
                    altDirection = 3;
                    diagonal = 6;

                }

            }

            directions.Add(moveDirection);

            directions.Add(altDirection);

            directions.Add(diagonal);

            directions.Add(straight);

            return directions;

        }

        public static Vector2 DirectionAsVector(int direction)
        {

            Vector2 move = new(0, -1);

            switch (direction)
            {

                case 1: move = new(1, -1); break;

                case 2: move = new(1, 0); break;

                case 3: move = new(1, 1); break;

                case 4: move = new(0, 1); break;

                case 5: move = new(-1, 1); break;

                case 6: move = new(-1, 0); break;

                case 7: move = new(-1, -1); break;

            }

            return move;

        }
        
        public static Vector2 DirectionAsVectorOffset(int direction)
        {

            Vector2 move = new(0.5f, -1f);

            switch (direction)
            {

                case 1: move = new(1f, -0.5f); break;

                case 2: move = new(1f, 0.5f); break;

                case 3: move = new(0.5f, 1f); break;

                case 4: move = new(-0.5f, 1f); break;

                case 5: move = new(-1f, 0.5f); break;

                case 6: move = new(-1f, -0.5f); break;

                case 7: move = new(-0.5f, -1f); break;

            }

            return move;

        }
        
        public static Vector2 PathMovement(Vector2 origin, Vector2 destination, float speed)
        {

            if (Vector2.Distance(destination, origin) < speed * 1.5)
            {

                return destination;

            }

            Vector2 factor = PathFactor(origin, destination);

            return origin + factor * speed;

        }

        public static Vector2 PathFactor(Vector2 origin, Vector2 destination)
        {

            Vector2 difference = destination - origin;

            float absX = Math.Abs(difference.X); // x position

            float absY = Math.Abs(difference.Y); // y position

            float moveX = difference.X > 0f ? 1 : -1; // x sign

            float moveY = difference.Y > 0f ? 1 : -1; // y sign

            if (destination.X == origin.X)
            {

                moveX = 0;

            }
            else if (destination.Y == origin.Y)
            {

                moveY = 0;

            }
            else if (absY > absX)
            {

                moveX *= absX / absY;

            }
            else
            {
                moveY *= absY / absX;

            }

            Vector2 factor = new(moveX, moveY);

            return factor;

        }

        public static Dictionary<Vector2, int> TraversalToTarget(GameLocation location, Vector2 occupied, Vector2 target, int ability, int proximity, int direction = -1)
        {

            if (direction == -1)
            {

                // direction from target to origin, will search tiles in between

                direction = DirectionToTarget(target * 64, occupied * 64)[2];

            }

            // get possible leads for target

            List<Vector2> open = GetOccupiableTilesNearby(location, target, direction, proximity, 1);

            Dictionary<Vector2, int> access = new();

            if (open.Count == 0)
            {

                return new();

            }

            foreach (Vector2 lead in open)
            {

                // between paths exclude occupied tile and lead

                List<Vector2> paths = GetTilesBetweenPositions(location, lead * 64, occupied * 64);

                // add lead as termination point

                paths.Add(lead);

                // update registry of accessed tiles

                access = PathsToAccess(location, paths, access);

                // get valid tile based traversal path to target, with warp and jump sections

                Dictionary<Vector2, int> points = PathsToTraversal(location, paths, access, ability);

                if (points.Count > 0)
                {

                    return points;

                }

            }

            return new();

        }

        public static Dictionary<Vector2, int> PathsToAccess(GameLocation location, List<Vector2> paths, Dictionary<Vector2, int> access)
        {

            foreach (Vector2 point in paths)
            {

                if (!access.ContainsKey(point))
                {

                    access[point] = TileAccessibility(location, point);

                }

            }

            return access;

        }

        public static Dictionary<Vector2, int> PathsToTraversal(GameLocation location, List<Vector2> paths, Dictionary<Vector2, int> access, int ability)
        {

            Dictionary<Vector2, int> points = new();

            bool warp = false;

            bool jump = false;

            int span = 0;

            int accessibility;

            foreach (Vector2 point in paths)
            {

                if (!access.ContainsKey(point))
                {

                    accessibility = TileAccessibility(location, point);

                    access[point] = accessibility;

                }
                else
                {

                    accessibility = access[point];

                }

                if (accessibility == 2)
                {

                    if (ability == 2)
                    {

                        warp = true;

                        continue;

                    }

                    return new();

                }

                if (accessibility == 1)
                {

                    if (ability >= 1 && span <= 6)
                    {

                        jump = true;

                        span++;

                        continue;

                    }

                    return new();

                }

                points[point] = 0;

                if (warp)
                {

                    points[point] = 2;

                    span = 0;

                }
                else if (jump)
                {

                    if (points.Count >= 2)
                    {

                        KeyValuePair<Vector2, int> previous = points.ElementAt(points.Count - 1);

                        if (previous.Value == 0)
                        {

                            points.Remove(previous.Key);

                        }

                    }

                    points[point] = 1;

                    span = 0;

                }

                jump = false;

                warp = false;

            }

            return points;

        }

        public static int TileAccessibility(GameLocation location, Vector2 check)
        {

            Dictionary<string, List<Vector2>> ObjectCheck = NeighbourCheck(location, check, 0, 0);

            if (ObjectCheck.ContainsKey("Building") || ObjectCheck.ContainsKey("Wall"))
            {

                return 2;

            }

            if (ObjectCheck.ContainsKey("BigObject") || ObjectCheck.ContainsKey("Fence"))
            {

                return 1;

            }

            string groundCheck = GroundCheck(location, check, true);

            if (groundCheck == "ground")
            {

                return 0;

            }

            if (groundCheck == "water")
            {

                return 1;

            }

            return 2;

        }

        // ======================== FARMER INTERACTIONS

        public static float Proximation(Vector2 position, List<Vector2> positions, float threshold)
        {

            foreach (Vector2 attempt in positions)
            {

                float difference = Vector2.Distance(position, attempt);

                if (difference < threshold)
                {

                    return difference;

                }

            }

            return -1f;

        }

        public static List<Farmer> GetFarmersInLocation(GameLocation location)
        {

            List<Farmer> farmers = new();

            if (location == null)
            {

                return farmers;

            }

            foreach (Farmer farmer in location.farmers)
            {
                
                farmers.Add(farmer);

            }

            return farmers;

        }

        public static List<Farmer> FarmerProximity(GameLocation targetLocation, List<Vector2> targetPosition, float threshold, bool checkInvincible = false)
        {

            Dictionary<Farmer, float> farmerList = new();

            if (targetLocation == null)
            {

                return new();

            }

            threshold = Math.Max(64, threshold);

            List<Farmer> farmers = GetFarmersInLocation(targetLocation);

            if (farmers.Count == 0)
            {

                return farmers;

            }

            foreach (Farmer farmer in farmers)
            {

                if (checkInvincible && farmer.temporarilyInvincible)
                {

                    continue;

                }

                float distance = Proximation(farmer.Position, targetPosition, threshold);

                if (distance != -1)
                {

                    farmerList.Add(farmer, distance);

                }

            }

            List<Farmer> ordered = new();

            foreach (KeyValuePair<Farmer, float> kvp in farmerList.OrderBy(key => key.Value))
            {

                ordered.Add(kvp.Key);

            }

            return ordered;

        }

        public static List<Character.Character> CompanionProximity(GameLocation targetLocation, List<Vector2> targetPosition, float threshold, bool checkInvincible = false)
        {

            if (targetLocation == null || Mod.instance.trackers.Count == 0)
            {

                return new();

            }

            Dictionary<Character.Character, float> companionList = new();

            threshold = Math.Max(64, threshold);

            foreach (KeyValuePair<CharacterHandle.characters, TrackHandle> tracker in Mod.instance.trackers)
            {

                if(Mod.instance.characters[tracker.Key].currentLocation.Name != targetLocation.Name)
                {

                    continue;

                }

                float distance = Proximation(Mod.instance.characters[tracker.Key].Position, targetPosition, threshold);

                if (distance != -1)
                {

                    companionList.Add(Mod.instance.characters[tracker.Key], distance);

                }

            }

            List<Character.Character> ordered = new();

            foreach (KeyValuePair<Character.Character, float> kvp in companionList.OrderBy(key => key.Value))
            {

                ordered.Add(kvp.Key);

            }

            return ordered;

        }

        public static void DamageFarmers(List<Farmer> farmers, int damage, StardewValley.Monsters.Monster monster, bool parry = false)
        {

            if (farmers.Count == 0)
            {
                return;
            }

            foreach (Farmer farmer in farmers)
            {

                if (farmer.health < 5 && Mod.instance.activeEvent.Count > 0)
                {

                    Mod.instance.CriticalCondition();

                    break;

                }

                farmer.takeDamage(Math.Min(farmer.health - 5, damage), parry, monster);

            }

        }

        public static List<NPC> GetFriendsInLocation(GameLocation location, bool friend = false)
        {

            List<NPC> villagers = new();

            foreach (NPC nPC in location.characters)
            {

                if (nPC is StardewValley.Monsters.Monster)
                {

                    continue;

                }

                if (nPC is Character.Character)
                {

                    continue;

                }

                if (nPC is Cast.Ether.Dragon)
                {

                    continue;

                }

                if (nPC.IsInvisible)
                {

                    continue;

                }

                StardewValley.GameData.Characters.CharacterData data = nPC.GetData();

                if (data == null)
                {

                    continue;

                }

                if (friend)
                {
                    if (!Game1.player.friendshipData.ContainsKey(nPC.Name))
                    {

                        continue;

                    }

                }

                villagers.Add(nPC);

            }

            return villagers;

        }

        // ======================== MONSTER INTERACTIONS

        public static bool MonsterVitals(StardewValley.Monsters.Monster Monster, GameLocation location)
        {

            if (Monster == null)
            {

                return false;

            }

            if (Monster.Health <= 0)
            {

                return false;

            }

            if (Monster.currentLocation == null)
            {

                return false;

            }

            if (!Monster.currentLocation.characters.Contains(Monster))
            {

                return false;

            }

            if (Monster.currentLocation.Name != location.Name)
            {

                return false;

            }

            return true;

        }

        public static float CalculateCritical(float damage, float critChance = 0.1f, float critModifier = 1f)
        {

            Random random = new();

            if ((float)random.NextDouble() > critChance)
            {

                return -1f;

            }

            if (Game1.player.hasTrinketWithID("IridiumSpur"))
            {

                BuffEffects buffEffects = new BuffEffects();

                buffEffects.Speed.Value = 1f;

                Game1.player.applyBuff(new Buff("iridiumspur", null, Game1.content.LoadString("Strings\\1_6_Strings:IridiumSpur_Name"), Game1.player.getFirstTrinketWithID("IridiumSpur").GetEffect().general_stat_1 * 1000, Game1.objectSpriteSheet_2, 76, buffEffects, false));

            }

            damage *= 1f + critModifier;

            return damage;

        }

        public static List<int> CalculatePush(StardewValley.Monsters.Monster monster, Vector2 from, int range = 128)
        {

            if (monster.isGlider.Value)
            {

                return new() { 0, 0 };

            }

            Vector2 direction = DirectionAsVector(DirectionToTarget(from, monster.Position)[2]);

            float fraction = 128f / Vector2.Distance(Vector2.Zero, direction * 128);

            float factor = 128f * fraction;

            Vector2 distance = direction * factor;

            return new() { (int)distance.X, (int)distance.Y };

        }

        public static List<StardewValley.Monsters.Monster> GetMonstersInLocation(GameLocation location, bool includeBosses = true)
        {

            List<StardewValley.Monsters.Monster> monsters = new();

            foreach (NPC nPC in location.characters)
            {

                if (nPC is not StardewValley.Monsters.Monster monster)
                {

                    continue;

                }

                if (!MonsterVitals(monster, location))
                {

                    continue;

                }

                if (!includeBosses)
                {

                    if (monster is Boss && (monster as Boss).netMode.Value > 1)
                    {

                        continue;

                    }

                }

                monsters.Add(monster);

            }

            return monsters;

        }

        public static List<StardewValley.Monsters.Monster> MonsterProximity(GameLocation targetLocation, List<Vector2> targetPositions, float threshold, bool checkInvincible = false)
        {

            if (targetLocation is SlimeHutch)
            {

                return new();

            }

            Dictionary<StardewValley.Monsters.Monster, float> monsterList = new();

            foreach (StardewValley.Monsters.Monster monster in GetMonstersInLocation(targetLocation, true))
            {

                if (monster.IsInvisible || monster.Health <= 0)
                {

                    continue;

                }

                if (checkInvincible)
                {

                    if (monster.isInvincible())
                    {

                        continue;

                    }

                    if (monster is Boss boss)
                    {

                        if (boss.netPosturing.Value)
                        {

                            if (!boss.netChannelActive.Value)
                            {

                                continue;

                            }

                        }

                        if (boss.netWoundedActive.Value)
                        {

                            continue;

                        }

                    }

                }

                float monsterThreshold = threshold;

                if (monster.Sprite.SpriteWidth > 16)
                {
                        
                    monsterThreshold += 32f;
                    
                }

                if (monster.Sprite.SpriteWidth > 32)
                {
                        
                    monsterThreshold += 32f;
                    
                }

                float difference = Proximation(monster.Position, targetPositions, monsterThreshold);

                if (difference > 0)
                {

                    monsterList.Add(monster, difference);

                }

            }

            List<StardewValley.Monsters.Monster> ordered = new();

            foreach (KeyValuePair<StardewValley.Monsters.Monster, float> kvp in monsterList.OrderBy(key => key.Value))
            {

                ordered.Add(kvp.Key);

            }

            return ordered;

        }

        public static List<StardewValley.Monsters.Monster> MonsterIntersect(GameLocation targetLocation, Microsoft.Xna.Framework.Rectangle hitBox, bool checkInvincible = false)
        {

            List<StardewValley.Monsters.Monster> monsterList = new();

            foreach (NPC nonPlayableCharacter in targetLocation.characters)
            {

                if (nonPlayableCharacter is StardewValley.Monsters.Monster monster)
                {

                    if (monster.IsInvisible || monster.Health <= 0)
                    {

                        continue;

                    }

                    if (checkInvincible && monster.isInvincible())
                    {
                        continue;
                    }

                    Microsoft.Xna.Framework.Rectangle boundingBox = monster.GetBoundingBox();

                    if (boundingBox.Intersects(hitBox))
                    {

                        monsterList.Add(monster);

                    }

                }

            }

            return monsterList;

        }

        public static void DamageMonsters(List<StardewValley.Monsters.Monster> monsterList, Farmer targetPlayer, int damage, float critChance = 0.1f, float critModifier = 1, bool push = false)
        {

            if (monsterList.Count == 0)
            {
                return;
            }

            foreach (StardewValley.Monsters.Monster monster in monsterList)
            {

                bool critApplied = false;

                if (critChance > 0f)
                {

                    float critDamage = CalculateCritical(damage, critChance, critModifier);

                    if (critDamage > 0)
                    {

                        damage = (int)critDamage;

                        critApplied = true;

                    }

                }

                List<int> diff = new() { 0, 0 };

                if (push)
                {

                    diff = CalculatePush(monster, targetPlayer.Position, 64);

                }

                HitMonster(targetPlayer, monster, damage, critApplied, diffX: diff[0], diffY: diff[1]);

            }

        }

        public static void HitMonster(Farmer targetPlayer, StardewValley.Monsters.Monster targetMonster, int damage, bool critApplied, int diffX = 0, int diffY = 0)
        {

            GameLocation targetLocation = targetMonster.currentLocation;

            if (targetLocation == null)
            {

                return;

            }

            bool specialHit = false;

            int damageDealt = 0;

            if (targetMonster is Mummy mummy)
            {

                if (mummy.reviveTimer.Value > 0)
                {

                    damageDealt = mummy.takeDamage(1, 0, 0, true, 1.00, targetPlayer);

                    specialHit = true;

                }

            }

            if (targetMonster is Bug buggy)
            {

                damageDealt = 99;

                buggy.Health = 0;

                buggy.currentLocation.playSound("hitEnemy");

                buggy.deathAnimation();

                specialHit = true;

            }

            if (targetMonster is RockCrab crabby)
            {
                Type reflectType = typeof(RockCrab);

                FieldInfo reflectField = reflectType.GetField("shellGone", BindingFlags.NonPublic | BindingFlags.Instance);

                var shellGone = reflectField.GetValue(crabby);
                ;
                if (shellGone != null)
                {

                    if (!(shellGone as NetBool).Value)
                    {
                        FieldInfo shellField = reflectType.GetField("shellHealth", BindingFlags.NonPublic | BindingFlags.Instance);

                        var shellHealth = shellField.GetValue(crabby);

                        for (int i = 0; i < (shellHealth as NetInt).Value; i++)
                        {

                            crabby.hitWithTool(Mod.instance.virtualPick);

                        }

                    }

                }

            }

            if (!specialHit)
            {

                damageDealt = targetMonster.takeDamage(damage, diffX, diffY, false, 1.00, targetPlayer);

            }

            foreach (BaseEnchantment enchantment in targetPlayer.enchantments)
            {
                enchantment.OnCalculateDamage(targetMonster, targetLocation, targetPlayer, ref damageDealt);
            }

            //targetLocation.removeDamageDebris(targetMonster);

            Microsoft.Xna.Framework.Rectangle boundingBox = targetMonster.GetBoundingBox();

            Color color = new(255, 130, 0);

            if (critApplied)
            {

                color = Color.Yellow;

                targetLocation.playSound("crit");

            }

            targetLocation.removeDamageDebris(targetMonster);

            targetLocation.debris.Add(new Debris(damageDealt, new Vector2(boundingBox.Center.X + 16, boundingBox.Center.Y), color, critApplied ? 1f + damageDealt / 300f : 1f, targetMonster));

            foreach (BaseEnchantment enchantment2 in targetPlayer.enchantments)
            {
                enchantment2.OnDealDamage(targetMonster, targetLocation, targetPlayer, ref damageDealt);
            }

            foreach (Trinket trinketItem in targetPlayer.trinketItems)
            {
                trinketItem?.OnDamageMonster(targetPlayer, targetMonster, damageDealt);
            }

            if (targetMonster.Health <= 0)
            {
                
                Farmer who = targetPlayer;

                StardewValley.Monsters.Monster monster = targetMonster;

                Microsoft.Xna.Framework.Rectangle monsterBox = monster.GetBoundingBox();

                if (!targetLocation.IsFarm)
                {
                    who.checkForQuestComplete(null, 1, 1, null, monster.Name, 4);
                    if (new List<SpecialOrder>(Game1.player.team.specialOrders) != null)
                    {
                        foreach (SpecialOrder specialOrder in Game1.player.team.specialOrders)
                        {
                            specialOrder.onMonsterSlain?.Invoke(Game1.player, monster);
                        }
                    }
                }

                if (who != null)
                {
                    foreach (BaseEnchantment enchantment in who.enchantments)
                    {
                        enchantment.OnMonsterSlay(monster, targetLocation, who);
                    }
                }

                who?.leftRing.Value?.onMonsterSlay(monster, targetLocation, who);

                who?.rightRing.Value?.onMonsterSlay(monster, targetLocation, who);

                if (who != null && !(targetLocation is SlimeHutch) && (!(monster is GreenSlime greenSlime) || greenSlime.firstGeneration.Value))
                {
                    if (who.IsLocalPlayer)
                    {
                        //if(monster is StardewDruid.Monster.Boss bossMonster)
                        //{

                        //    Game1.stats.monsterKilled(SpawnData.MonsterSlayer(bossMonster));
                        //}
                        //else
                        //{

                            Game1.stats.monsterKilled(monster.Name);

                        //}

                    }
                    else if (Game1.IsMasterGame)
                    {

                        who.queueMessage(25, Game1.player, monster.Name);

                    }

                }

                if (monster is DinoMonster)
                {

                    SpawnData.MonsterDrops(monster, SpawnData.drops.rex);

                    if (monster.objectsToDrop.Count > 0)
                    {

                        string drop = monster.objectsToDrop[Mod.instance.randomIndex.Next(monster.objectsToDrop.Count)];

                        StardewValley.Object dropItem = new StardewValley.Object(drop, 1);

                        Game1.createItemDebris(dropItem, monster.Position + new Vector2(0, 32), 2, targetLocation, -1);

                    }

                }
                else
                {

                    targetLocation.monsterDrop(monster, monsterBox.Center.X, monsterBox.Center.Y, who);


                }

                if (who != null && !(targetLocation is SlimeHutch))
                {
                    who.gainExperience(4, targetLocation.IsFarm ? Math.Max(1, monster.ExperienceGained / 3) : monster.ExperienceGained);
                }

                if (monster.isHardModeMonster.Value)
                {
                    Game1.stats.Increment("hardModeMonstersKilled");
                }

                if (monster.ShouldMonsterBeRemoved())
                {
                    targetLocation.characters.Remove(monster);
                }

                Game1.stats.MonstersKilled++;

                targetLocation.removeTemporarySpritesWithID((int)(monster.position.X * 777f + monster.position.Y * 77777f));

                if (who != null && who.CurrentTool != null && who.CurrentTool is MeleeWeapon meleeWeapon && (meleeWeapon.QualifiedItemId == "(W)65" || meleeWeapon.appearance.Value != null && meleeWeapon.appearance.Value.Equals("(W)65")))
                {

                    Utility.addRainbowStarExplosion(targetLocation, new Vector2(monsterBox.Center.X - 32, monsterBox.Center.Y - 32), Game1.random.Next(6, 9));

                }
                /*
                targetPlayer.checkForQuestComplete(null, 1, 1, null, targetMonster.Name, 4);

                if (Game1.player.team.specialOrders is not null)
                {
                    foreach (StardewValley.SpecialOrders.SpecialOrder specialOrder in Game1.player.team.specialOrders)
                    {
                        if (specialOrder.onMonsterSlain != null)
                        {
                            specialOrder.onMonsterSlain(Game1.player, targetMonster);
                        }
                    }
                }

                foreach (StardewValley.Enchantments.BaseEnchantment enchantment3 in targetPlayer.enchantments)
                {
                    enchantment3.OnMonsterSlay(targetMonster, targetLocation, targetPlayer);
                }

                if (targetPlayer.leftRing.Value != null)
                {
                    targetPlayer.leftRing.Value.onMonsterSlay(targetMonster, targetLocation, targetPlayer);
                }

                if (targetPlayer.rightRing.Value != null)
                {
                    targetPlayer.rightRing.Value.onMonsterSlay(targetMonster, targetLocation, targetPlayer);
                }

                if (!(targetMonster is GreenSlime) || (targetMonster as GreenSlime).firstGeneration.Value)
                {
                    if (targetPlayer.IsLocalPlayer)
                    {
                        Game1.stats.monsterKilled(targetMonster.Name);
                    }
                    else if (Game1.IsMasterGame)
                    {
                        targetPlayer.queueMessage(25, Game1.player, targetMonster.Name);
                    }
                }

                targetLocation.monsterDrop(targetMonster, boundingBox.Center.X, boundingBox.Center.Y, targetPlayer);

                targetPlayer.gainExperience(4, targetMonster.ExperienceGained);

                if (targetMonster.isHardModeMonster.Value)
                {
                    Game1.stats.Increment("hardModeMonstersKilled", 1);
                }

                //targetLocation.characters.Remove(targetMonster);

                Game1.stats.MonstersKilled++;*/

            }

        }

        // ======================== ENVIRONMENT INTERACTIONS

        public static List<Vector2> Explode(GameLocation targetLocation, Vector2 targetVector, Farmer targetPlayer, int tileRadius, int powerLevel = 1)
        {

            Tool Pick = Mod.instance.virtualPick;

            Tool Axe = Mod.instance.virtualAxe;

            List<Vector2> returnVectors = new();

            // ----------------- clump destruction

            if (targetLocation.resourceClumps.Count > 0 && powerLevel >= 4)
            {

                for (int index = targetLocation.resourceClumps.Count - 1; index >= 0; --index)
                {

                    ResourceClump resourceClump = targetLocation.resourceClumps[index];

                    if ((double)Vector2.Distance(resourceClump.Tile, targetVector) <= tileRadius + 1)
                    {

                        switch (resourceClump.parentSheetIndex.Value)
                        {
                            case ResourceClump.stumpIndex:
                            case ResourceClump.hollowLogIndex:

                                DestroyStump(targetLocation, resourceClump, resourceClump.Tile);
                                break;

                            default:

                                DestroyBoulder(targetLocation, resourceClump, resourceClump.Tile);
                                break;

                        }

                    }

                }

            }

            // ----------------- object destruction

            List<Vector2> tileVectors;

            int impactRadius = tileRadius + 1;

            for (int i = 0; i < impactRadius; i++)
            {

                if (i == 0)
                {

                    tileVectors = new List<Vector2>
                    {

                        targetVector

                    };

                }
                else
                {

                    tileVectors = GetTilesWithinRadius(targetLocation, targetVector, i);

                }


                bool destroyVector;

                foreach (Vector2 tileVector in tileVectors)
                {

                    destroyVector = false;

                    if (targetLocation.objects.ContainsKey(tileVector))
                    {

                        StardewValley.Object targetObject = targetLocation.objects[tileVector];

                        if (targetObject.IsBreakableStone())
                        {

                            if (powerLevel >= 2)
                            {

                                targetLocation.OnStoneDestroyed(@targetObject.QualifiedItemId.Replace("(O)",""), (int)tileVector.X, (int)tileVector.Y, targetPlayer);

                                targetLocation.objects.Remove(tileVector);

                                destroyVector = true;

                            }

                        }
                        else if (targetObject.IsTwig() || targetObject.QualifiedItemId == "(O)169")
                        {

                            targetObject.onExplosion(targetPlayer);

                            targetLocation.debris.Add(new Debris(ItemRegistry.Create("(O)388", Mod.instance.randomIndex.Next(1, 3)), tileVector * 64f));

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;

                        }
                        else if (targetObject.IsWeeds())
                        {

                            string spawnSeed = SpawnData.SeasonalSeed(targetLocation);

                            if (spawnSeed != null)
                            {

                                targetLocation.debris.Add(new Debris(ItemRegistry.Create(spawnSeed), tileVector * 64f));

                            }

                            targetObject.onExplosion(targetPlayer);

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;

                        }
                        else if (targetObject.Name.Contains("SupplyCrate"))
                        {
                            targetObject.MinutesUntilReady = 1;

                            targetObject.performToolAction(Pick);

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;
                        }
                        else if (targetObject is BreakableContainer breakableContainer)
                        {

                            breakableContainer.releaseContents(targetPlayer);

                            targetLocation.objects.Remove(tileVector);

                            targetLocation.playSound("barrelBreak");

                            destroyVector = true;

                        }
                        else if (targetObject.QualifiedItemId == "(O)590")
                        {

                            targetLocation.digUpArtifactSpot((int)targetVector.X, (int)targetVector.Y, targetPlayer);

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;

                        }
                        else if (targetObject.QualifiedItemId == "(O)SeedSpot")
                        {

                            Item raccoonSeedForCurrentTimeOfYear = Utility.getRaccoonSeedForCurrentTimeOfYear(Game1.player, Mod.instance.randomIndex);

                            Game1.createMultipleItemDebris(raccoonSeedForCurrentTimeOfYear, targetVector * 64f, 2, targetLocation);

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;

                        }
                        else if (targetObject is Fence || targetObject is Workbench || targetObject is Furniture || targetObject is Chest)
                        {

                            // do nothing

                        }
                        else if (powerLevel >= 3)
                        {

                            // ----------------- dislodge craftable

                            for (int j = 0; j < 2; j++)
                            {

                                Tool toolUse = j == 0 ? Pick : Axe;

                                if (targetLocation.objects.ContainsKey(tileVector) && targetObject.performToolAction(toolUse))
                                {
                                    targetObject.performRemoveAction();

                                    targetObject.dropItem(targetLocation, tileVector * 64, tileVector * 64 + new Vector2(0, 32));

                                    targetLocation.objects.Remove(tileVector);

                                }

                            }

                        }

                    }

                    if (targetLocation.terrainFeatures.ContainsKey(tileVector))
                    {

                        if (targetLocation.terrainFeatures[tileVector] is Tree targetTree)
                        {

                            if (targetTree.falling.Value)
                            {


                            }
                            else if (targetTree.growthStage.Value == 0 && i <= 1)
                            {

                                targetTree.performToolAction(Mod.instance.virtualHoe, 0, tileVector);

                                targetLocation.terrainFeatures.Remove(tileVector);

                            }
                            else if (powerLevel >= 3)
                            {

                                if (targetTree.growthStage.Value >= 5)
                                {

                                    if(targetTree.Location is Town)
                                    {

                                        targetTree.Location = Game1.getFarm();

                                    }

                                    targetTree.performToolAction(Axe, (int)targetTree.health.Value, tileVector);

                                    targetTree.Location = targetLocation;

                                }
                                else
                                {
                                    
                                    WildTreeData data = targetTree.GetData();

                                    if (data != null && data.SeedItemId != null)
                                    {
                                        
                                        targetLocation.debris.Add(new Debris(ItemQueryResolver.TryResolveRandomItem(data.SeedItemId, new ItemQueryContext(targetLocation, Game1.player, null)), tileVector * 64f));
                                    
                                    }

                                    if (targetTree.Location is Town)
                                    {

                                        targetTree.Location = Game1.getFarm();


                                    }

                                    targetTree.performToolAction(Axe, 0, tileVector);

                                    targetLocation.terrainFeatures.Remove(tileVector);

                                    targetTree.Location = targetLocation;



                                }

                                targetTree = null;

                                destroyVector = true;

                            }

                        }
                        else
                        if (targetLocation.terrainFeatures[tileVector] is Grass grassFeature && powerLevel >= 3)
                        {

                            targetLocation.terrainFeatures.Remove(tileVector);

                            if (Mod.instance.randomIndex.Next(2) == 0)
                            {

                                targetLocation.debris.Add(new Debris(ItemRegistry.Create("(O)771"), tileVector * 64f));

                            }

                            destroyVector = true;

                        }

                    }

                    if (destroyVector)
                    {

                        returnVectors.Add(tileVector);

                    }

                }

            }

            return returnVectors;

        }

        public static void Reave(GameLocation targetLocation, Vector2 targetVector, Farmer targetPlayer, int radiusTiles)
        {

            List<Vector2> tileVectors;

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            int wet = Game1.IsRainingHere(targetLocation) && targetLocation.IsOutdoors && !targetLocation.Name.Equals("Desert") ? 1 : 0;

            for (int i = 0; i < radiusTiles + 1; i++)
            {

                if (i == 0)
                {

                    tileVectors = new List<Vector2>
                    {

                        targetVector

                    };

                }
                else
                {

                    tileVectors = GetTilesWithinRadius(targetLocation, targetVector, i);

                }

                int dirtCount = 0;

                foreach (Vector2 tileVector in tileVectors)
                {

                    dirtCount++;

                    if (i == radiusTiles && dirtCount % 2 == 1)
                    {

                        continue;

                    }

                    if (GroundCheck(targetLocation, tileVector) == "ground" && NeighbourCheck(targetLocation, tileVector, 0, 0).Count == 0)
                    {

                        int tilex = (int)tileVector.X;
                        int tiley = (int)tileVector.Y;

                        Tile backTile = backLayer.Tiles[tilex, tiley];

                        if (backTile.TileIndexProperties.TryGetValue("Diggable", out _))
                        {

                            targetLocation.checkForBuriedItem(tilex, tiley, explosion: false, detectOnly: false, Game1.player);

                            targetLocation.terrainFeatures.Add(tileVector, new HoeDirt(wet, targetLocation));

                        }
                        else if (backTile.TileIndexProperties.TryGetValue("Diggable", out _))
                        {

                            targetLocation.checkForBuriedItem(tilex, tiley, explosion: false, detectOnly: false, Game1.player);

                            targetLocation.terrainFeatures.Add(tileVector, new HoeDirt(wet, targetLocation));

                        }

                    }

                }

            }

        }

        public static void DestroyBoulder(GameLocation targetLocation, ResourceClump resourceClump, Vector2 targetVector, bool extraDebris = false)
        {
            Random random = new Random();

            resourceClump.health.Set(1f);

            resourceClump.performToolAction(Mod.instance.virtualPick, 1, targetVector);

            resourceClump.NeedsUpdate = false;

            targetLocation._activeTerrainFeatures.Remove(resourceClump);

            targetLocation.resourceClumps.Remove(resourceClump);

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

            resourceClump.NeedsUpdate = false;

            if (extraDebris)
            {

                targetLocation.debris.Add(new Debris(ItemRegistry.Create("(O)709"), targetVector * 64f));

                targetLocation.debris.Add(new Debris(ItemRegistry.Create("(O)709"), targetVector * 64f));

            }

            if (targetLocation._activeTerrainFeatures.Contains(resourceClump))
            {

                targetLocation._activeTerrainFeatures.Remove(resourceClump);

            }

            if (targetLocation.resourceClumps.Contains(resourceClump))
            {

                targetLocation.resourceClumps.Remove(resourceClump);

            }

        }

    }

}
