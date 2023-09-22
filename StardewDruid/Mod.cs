using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid
{
    public class Mod : StardewModdingAPI.Mod
    {

        private ModData Config;

        private ActiveData activeData;

        private StaticData staticData;

        private List<string> buttonList;

        private List<string> craftList;

        Map.Statue druidStatue;

        public Dictionary<int, string> TreeTypes;

        public Dictionary<string, List<Vector2>> earthCasts;

        public Dictionary<string, List<Vector2>> servedWater;

        private Dictionary<Type, List<Cast.Cast>> activeCasts;

        private int specialCasts;

        public Dictionary<string, Vector2> warpPoints;

        public Dictionary<string, int> warpTotems;

        override public void Entry(IModHelper helper)
        {

            helper.Events.GameLoop.SaveLoaded += SaveLoaded;

            helper.Events.Input.ButtonsChanged += OnButtonsChanged;

            helper.Events.GameLoop.OneSecondUpdateTicked += EverySecond;

            helper.Events.GameLoop.Saving += SaveUpdated;

        }

        private void SaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            
            //staticData = Helper.Data.ReadSaveData<StaticData>("staticData");

            //if(staticData == null)
            //{

                staticData = new StaticData();

            //}

            ReviewQuests(true);

            Config = Helper.ReadConfig<ModData>();

            buttonList = Config.buttonList;

            craftList = Config.craftList;

            earthCasts = new();

            activeCasts = new();

            specialCasts = 3;

            druidStatue = new(this);

            druidStatue.ModifyCave();

            warpPoints = Map.Warp.WarpPoints();

            warpTotems = Map.Warp.WarpTotems();

            //ChallengeAnimations = new();

            //ChallengeLocations = Map.Spawn.ChallengeLocations();

            return;

        }

        private void SaveUpdated(object sender, SavingEventArgs e)
        {

            //Helper.Data.WriteSaveData("staticData", staticData);

            foreach (KeyValuePair<Type, List<Cast.Cast>> castEntry in activeCasts)
            {
                if (castEntry.Value.Count > 0)
                {
                    foreach (Cast.Cast castInstance in castEntry.Value)
                    {
                        castInstance.CastRemove();
                    }
                }

            }

            earthCasts = new();

            activeCasts = new();

            //ChallengeAnimations = new();

            specialCasts = 3;

        }

        private void EverySecond(object sender, OneSecondUpdateTickedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
            {

                return;

            }

            /*if (ChallengeLocations.ContainsKey(Game1.player.currentLocation.Name))
            {

                foreach (string Challenge in ChallengeLocations[Game1.player.currentLocation.Name])
                {

                    if (!staticData.ChallengeList[Challenge])
                    {

                        if (!ChallengeAnimations.ContainsKey(Challenge))
                        {

                            ChallengeAnimations[Challenge] = Map.Spawn.ChallengeAnimation(Challenge);

                        }

                    }

                }

            }*/

            if (activeCasts.Count == 0)
            {

                return;

            }

            List<Cast.Cast> activeCast = new();

            List<Cast.Cast> removeCast = new();

            foreach (KeyValuePair<Type, List<Cast.Cast>> castEntry in activeCasts)
            {

                int entryCount = castEntry.Value.Count;

                if (castEntry.Value.Count > 0)
                {

                    int castIndex = 0;

                    foreach (Cast.Cast castInstance in castEntry.Value)
                    {

                        castIndex++;

                        if (castInstance.CastActive(castIndex, entryCount))
                        {

                            activeCast.Add(castInstance);

                        }
                        else
                        {

                            removeCast.Add(castInstance);

                        }


                    }


                }

            }

            foreach (Cast.Cast castInstance in removeCast)
            {

                castInstance.CastRemove();

                activeCasts[castInstance.GetType()].Remove(castInstance);

            }

            foreach (Cast.Cast castInstance in activeCast)
            {

                castInstance.CastTrigger();

            }

            return;

        }

        private void OnButtonsChanged(object sender, ButtonsChangedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
            {
                
                return;
            
            }

            // ignore if player is busy with something else
            if (Game1.player.IsBusyDoingSomething())
            {

                return;

            }

            // simulates interactions with the farm cave statue
            if (Game1.currentLocation.Name == "FarmCave")
            {

                Vector2 playerLocation = Game1.player.getTileLocation();

                Vector2 cursorLocation = Game1.currentCursorTile;

                if (playerLocation.X >= 5 && playerLocation.X <= 7 && playerLocation.Y == 4 && cursorLocation.X == 6 && (cursorLocation.Y == 2 || cursorLocation.Y == 3))
                {

                    foreach (SButton buttonPressed in e.Pressed)
                    {
                        if (buttonPressed.IsUseToolButton() || buttonPressed.IsActionButton())
                        {

                            if(!staticData.questList["approachStatue"])
                            {

                                Game1.player.completeQuest(Map.Quest.QuestId("approachStatue"));

                                staticData.questList["approachStatue"] = true;

                            }

                            druidStatue.Approach(Game1.player);

                            Helper.Input.Suppress(buttonPressed);

                            return;

                        }

                    }

                }

            }

            // ignore if current tool isn't set
            if (Game1.player.CurrentTool is null)
            {

                return;

            }

            // ignore if current tool isn't weapon
            if (Game1.player.CurrentTool.GetType().Name != "MeleeWeapon")
            {

                return;

            }

            if(activeData == null)
            {

                activeData = new ActiveData();

            }

            string activeBlessing;

            switch (Game1.player.CurrentTool.InitialParentTileIndex)
            {

                case 15: // Forest Sword

                    activeBlessing = "earth";

                    break;

                case 14: // Neptune's Glaive

                    activeBlessing = "water";

                    break;

                case 9: // Lava Katana

                    activeBlessing = "stars";

                    break;

                default:
                    
                    activeBlessing = staticData.statueChoice;

                    break;

            }

            bool chargeEffect = false;

            foreach (SButton buttonPressed in e.Pressed)
            {
                if (buttonPressed.IsUseToolButton() || buttonPressed.IsActionButton())
                {

                    // Active State is Overridden by tool usage
                    if (activeData.activeCharge)
                    {

                        activeData.castComplete = true;

                    };

                }
                else if (buttonList.Contains(buttonPressed.ToString())) 
                {

                    Dictionary<string, bool> spawnIndex = Map.Spawn.SpawnIndex(Game1.player.currentLocation);

                    if(spawnIndex.Count == 0)
                    {

                        Game1.addHUDMessage(new HUDMessage("Unable to reach the otherworldly plane", 2));

                        return;

                    }

                    if (activeBlessing == "none")
                    {

                        Game1.addHUDMessage(new HUDMessage("Nothing happens", 2));

                        return;

                    }

                    int staminaRequired;

                    switch (activeBlessing)
                    {

                        case "water":

                            staminaRequired = 48;

                            break;

                        case "stars":

                            staminaRequired = 72;

                            break;

                        default: //"earth"

                            staminaRequired = 24;

                            break;

                    };

                    if (Game1.player.Stamina <= staminaRequired)
                    {

                        //Game1.addHUDMessage(new HUDMessage($"Not enough energy to perform rite of the {activeBlessing}", 3);
                        Game1.addHUDMessage(new HUDMessage("Not enough energy", 3));

                        return;

                    }

                    activeData = new ActiveData();

                    activeData.spawnIndex = spawnIndex;

                }

            }

            // Ignore if cast complete and not retriggered
            if (activeData.castComplete)
            {
                
                return;

            }

            foreach (SButton buttonHeld in e.Held)
            {

                // Already in Active State
                if (buttonList.Contains(buttonHeld.ToString()))
                {

                    activeData.activeKey = buttonHeld.ToString();

                    chargeEffect = true;

                    int chargeLevel;

                    int chargeFactor;

                    //-------------------------- hand animation

                    if (activeData.activeDirection == -1)
                    {

                        activeData.activeDirection = Game1.player.FacingDirection;

                    }

                    if (activeData.chargeAmount == 0 || activeData.chargeAmount % 12 == 0)
                    {
                        
                        ModUtility.AnimateHands(Game1.player, activeData.activeDirection, 225);
                    
                    }

                    //-------------------------- cast animation

                    activeData.chargeAmount++;

                    switch (activeBlessing)
                    {

                        case "water":

                            chargeFactor = 2;

                            break;

                        case "stars":

                            chargeFactor = 6;

                            break;

                        default: // earth

                            chargeFactor = 4;

                            break;

                    }

                    if (activeData.chargeAmount <= (chargeFactor * 10))
                    {

                        chargeLevel = 1;

                    }
                    else if (activeData.chargeAmount <= (chargeFactor * 20))
                    {

                        chargeLevel = 2;

                    }
                    else if (activeData.chargeAmount <= (chargeFactor * 30))
                    {

                        chargeLevel = 3;

                    }
                    else if (activeData.chargeAmount <= (chargeFactor * 40))
                    {

                        chargeLevel = 4;

                    }
                    else if (activeData.chargeAmount <= (chargeFactor * 50))
                    {

                        chargeLevel = 5;

                    }
                    else
                    {

                        chargeLevel = 6;

                        activeData.castComplete = true;

                    }

                    activeData.chargeLevel = chargeLevel;

                }

            }

            // Active charge requires engaged input
            if (activeData.activeCharge && !chargeEffect)
            {

                activeData.castComplete = true;

            }

            if (chargeEffect)
            {

                foreach (SButton buttonPressed in e.Pressed)
                {

                    if (buttonPressed.ToString() != activeData.activeKey && buttonPressed.IsUseToolButton() == false && buttonPressed.IsActionButton() == false)
                    {

                        Helper.Input.Suppress(buttonPressed);

                    }

                }

                foreach (SButton buttonHeld in e.Held)
                {

                    if (buttonHeld.ToString() != activeData.activeKey && buttonHeld.IsUseToolButton() == false && buttonHeld.IsActionButton() == false)
                    {

                        Helper.Input.Suppress(buttonHeld);

                    }

                }

                if (activeData.animateLevel != activeData.chargeLevel)
                {

                    switch (activeBlessing)
                    {

                        case "stars":

                            AnimateStars();

                            break;

                        case "water":

                            AnimateWater();

                            break;

                        default: //"earth"

                            AnimateEarth();

                            break;

                    }

                }

                activeData.activeCharge = true;

            }

            return;

        }

        private void ReviewQuests(bool loadQuests)
        {

            Dictionary<string, int> questList = Map.Quest.QuestList();

            StardewValley.Quests.Quest newQuest;

            foreach (KeyValuePair<string, int> quest in questList)
            {

                if (Map.Quest.CurrentQuest(Game1.player, staticData, quest.Key))
                {

                    if (!staticData.questList.ContainsKey(quest.Key))
                    {

                        newQuest = Map.Quest.GenerateQuest(quest.Key);

                        staticData.questList[quest.Key] = false;

                        Game1.player.questLog.Add(newQuest);

                    }
                    else if (!staticData.questList[quest.Key] && loadQuests)
                    {

                        newQuest = Map.Quest.GenerateQuest(quest.Key);

                        newQuest.showNew.Value = false;

                        Game1.player.questLog.Add(newQuest);

                    }

                }

            }

            Dictionary<string, string> challengeList = Map.Quest.ChallengeList();

            foreach (KeyValuePair<string, string> challenge in challengeList)
            {

                if (Map.Quest.CurrentQuest(Game1.player, staticData, challenge.Key))
                {

                    if (!staticData.challengeList.ContainsKey(challenge.Key))
                    {

                        staticData.challengeList[challenge.Key] = false;

                        if (!staticData.triggerList.ContainsKey(challenge.Value))
                        {

                            staticData.triggerList[challenge.Value] = new();


                        }

                        staticData.triggerList[challenge.Value].Add(challenge.Key);

                    }

                }

            }

        }

        private void AnimateEarth()
        {
                
            if(activeData.activeDirection == -1)
            {

                activeData.activeDirection = Game1.player.FacingDirection;

            }

            //-------------------------- cast variables

            int animationRow;

            Microsoft.Xna.Framework.Rectangle animationRectangle;

            float animationInterval;

            int animationLength;

            int animationLoops;

            bool animationFlip;

            float animationScale;

            Microsoft.Xna.Framework.Color animationColor;

            Vector2 animationPosition;

            TemporaryAnimatedSprite newAnimation;

            animationFlip = false;

            switch (activeData.chargeLevel)
            {

                case 1:
                    animationColor = new(uint.MaxValue); // deepens from white to green
                    animationScale = 0.6f;
                    animationPosition = Game1.player.Position + new Vector2(12f, -128f); // 2 tiles above player
                    break;
                case 2:
                    animationColor = new(0.8f, 1, 0.8f, 1);
                    animationScale = 0.8f;
                    animationPosition = Game1.player.Position + new Vector2(6f, -134f); // 2 tiles above player
                    break;
                case 3:
                    animationColor = new(0.6f, 1, 0.6f, 1);
                    animationScale = 1f;
                    animationPosition = Game1.player.Position + new Vector2(0f, -140f); // 2 tiles above player
                    break;
                case 4:
                    animationColor = new(0.4f, 1, 0.4f, 1);
                    animationScale = 1.2f;
                    animationPosition = Game1.player.Position + new Vector2(-6f, -146f); // 2 tiles above player
                    break;
                default:
                    animationColor = new(0.2f, 1, 0.2f, 1);
                    animationScale = 1.4f;
                    animationPosition = Game1.player.Position + new Vector2(-12f, -152f); // 2 tiles above player
                    break;

            }

            //-------------------------- cast animation

            animationRow = 10;

            animationRectangle = new(0, animationRow * 64, 64, 64);

            animationInterval = 75f;

            animationLength = 8;

            animationLoops = 1;

            newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlip, -1, 0f, animationColor, animationScale, 0f, 0f, 0f)
            {
                motion = new Vector2(0f, -0.1f)

            };

            Game1.currentLocation.temporarySprites.Add(newAnimation);

            //-------------------------- hand animation

            ModUtility.AnimateHands(Game1.player, activeData.activeDirection, 700);

            activeData.animateLevel = activeData.chargeLevel;

            DelayedAction.functionAfterDelay(CastEarth,616);

            if(activeData.chargeLevel % 2 != 0)
            {

                Game1.currentLocation.playSoundPitched("discoverMineral", 600 + (activeData.chargeLevel * 200));

            }

        }

        private void CastEarth()
        {

            activeData.castLevel++;

            //-------------------------- tile effects

            float staminaCost = 0f;

            int castCost = 0;

            int experienceGain = 0;

            Farmer targetPlayer = Game1.player;

            GameLocation playerLocation = targetPlayer.currentLocation;

            Vector2 playerVector = targetPlayer.getTileLocation();

            if (staticData.triggerList.ContainsKey("CastEarth")) 
            {

                List<string> castStrings = staticData.triggerList["CastEarth"];

                for (int i = 0; i < castStrings.Count; i++)
                {

                    string castString = castStrings[i];

                    if (!staticData.challengeList[castString])
                    {

                        if (Map.Quest.ChallengeLocation(castString) == playerLocation.Name)
                        {

                            Vector2 ChallengeVector = Map.Quest.ChallengeVector(castString);

                            Vector2 ChallengeLimit = ChallengeVector + Map.Quest.ChallengeLimit(castString);

                            if (
                                playerVector.X >= ChallengeVector.X &&
                                playerVector.Y >= ChallengeVector.Y &&
                                playerVector.X < ChallengeLimit.X &&
                                playerVector.Y < ChallengeLimit.Y
                                )
                            {

                                Cast.Cast castHandle;

                                if (i == 0)
                                {
                                    castHandle = new Cast.Sword(this, playerVector, targetPlayer);
                                }
                                else
                                {
                                    castHandle = new Cast.Challenge(this, playerVector, targetPlayer);
                                }

                                staticData.challengeList[castString] = true;

                                castHandle.CastEarth();

                                activeData.castComplete = true;

                                return;

                            }

                        }

                    }

                }

            }

            List<Vector2> tileVectors = ModUtility.GetTilesWithinRadius(playerLocation, playerVector, activeData.castLevel);

            Dictionary<Vector2, Cast.Cast> effectCasts = new();

            Layer backLayer = playerLocation.Map.GetLayer("Back");

            Layer buildingLayer = playerLocation.Map.GetLayer("Buildings");

            //Layer frontLayer = playerLocation.Map.GetLayer("Front");

            //Layer pathLayer = playerLocation.Map.GetLayer("Paths");

            List<Vector2> clumpIndex;

            //Dictionary<string, Vector2> swordIndex = Map.Spawn.SwordIndex("earth");

            if (!earthCasts.ContainsKey(playerLocation.Name))
            {
                earthCasts[playerLocation.Name] = new();

            };

            foreach (Vector2 tileVector in tileVectors)
            {

                if (playerLocation is MineShaft && activeData.castLevel >= 2)
                {

                    if (playerLocation.objects.Count() > 0)
                    {

                        if (playerLocation.objects.ContainsKey(tileVector))
                        {

                            continue;

                        }

                    }

                    int probability = Game1.random.Next(10);

                    if (probability <= 2)
                    {

                        effectCasts[tileVector] = new Cast.Rockfall(this, tileVector, targetPlayer);

                    }

                    continue;

                }

                if (earthCasts[playerLocation.Name].Contains(tileVector)) // already served
                {

                    continue;

                }
                else
                {

                    earthCasts[playerLocation.Name].Add(tileVector);

                }

                int tileX = (int)tileVector.X;

                int tileY = (int)tileVector.Y;

                /*string message = "";

                Tile frontTile = frontLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                if (frontTile != null)
                {

                    message += $"{tileVector} fronttile; ";

                    if (frontTile.TileIndexProperties.TryGetValue("Type", out var typeFront))
                    {

                        message += $"{typeFront}; ";

                    }

                }

                Tile pathTile = pathLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                if (pathTile != null)
                {

                    message += $"{tileVector} pathtile; ";

                    if (pathTile.TileIndexProperties.TryGetValue("Type", out var typePath))
                    {

                        message += $"{typePath}; ";

                    }
                
                }

                if(message != "")
                {
                    Monitor.Log($"{playerVector}: " + message, LogLevel.Debug);

                }*/

                Tile buildingTile = buildingLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                if (buildingTile != null)
                {

                    if (buildingTile.TileIndexProperties.TryGetValue("Passable", out _) == false)
                    {

                        //Monitor.Log($"{playerVector}: {tileVector} nonpassable", LogLevel.Debug);
                        continue;

                    }

                }

                if (playerLocation.terrainFeatures.ContainsKey(tileVector))
                {

                    TerrainFeature terrainFeature = playerLocation.terrainFeatures[tileVector];

                    switch (terrainFeature.GetType().Name.ToString())
                    {

                        case "Tree":

                            effectCasts[tileVector] = new Cast.Tree(this, tileVector, targetPlayer, terrainFeature as Tree);

                            break;

                        case "Grass":

                            effectCasts[tileVector] = new Cast.Grass(this, tileVector, targetPlayer, terrainFeature as Grass);

                            break;

                        case "HoeDirt":

                            if (!Game1.currentSeason.Equals("winter") && (playerLocation.IsFarm || playerLocation.IsGreenhouse))
                            {
                                HoeDirt hoeDirt = terrainFeature as HoeDirt;

                                effectCasts[tileVector] = new Cast.Hoed(this, tileVector, targetPlayer, hoeDirt);

                            }

                            break;

                        default:

                            break;

                    }

                    continue;

                }

                if (playerLocation.resourceClumps.Count > 0)
                {

                    bool targetClump = false;

                    foreach (ResourceClump resourceClump in playerLocation.resourceClumps)
                    {

                        clumpIndex = new()
                        {
                            tileVector,
                            tileVector + new Vector2(0,-1),
                            tileVector + new Vector2(-1,0),
                            tileVector + new Vector2(-1,1)

                        };

                        foreach(Vector2 originVector in clumpIndex)
                        {

                            if (resourceClump.tile.Value == originVector)
                            {

                                switch (resourceClump.parentSheetIndex.Value)
                                {

                                    case 600:
                                    case 602:

                                        effectCasts[tileVector] = new Cast.Stump(this, tileVector, targetPlayer, resourceClump);

                                        break;

                                    default:

                                        effectCasts[tileVector] = new Cast.Boulder(this, tileVector, targetPlayer, resourceClump);

                                        break;

                                }

                                Vector2 clumpVector = resourceClump.tile.Value;

                                activeData.removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y));

                                activeData.removeVectors.Add(new Vector2(clumpVector.X, clumpVector.Y + 1));

                                activeData.removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y + 1));

                                targetClump = true;

                                continue;

                            }

                        }

                    }

                    if (targetClump)
                    {

                        continue;

                    }
                
                }

                if (playerLocation.largeTerrainFeatures.Count > 0)
                {
                    
                    bool targetTerrain = false;

                    Microsoft.Xna.Framework.Rectangle tileRectangle = new(tileX * 64 + 1, tileY * 64 + 1, 62, 62);

                    foreach (LargeTerrainFeature largeTerrainFeature in playerLocation.largeTerrainFeatures)
                    {
                        if (largeTerrainFeature.getBoundingBox().Intersects(tileRectangle))
                        {

                            effectCasts[tileVector] = new Cast.Bush(this, tileVector, targetPlayer, largeTerrainFeature);

                            targetTerrain = true;

                            break;

                        }
                    }

                    if (targetTerrain)
                    {
                        continue;
                    }

                }

                if (playerLocation.objects.Count() > 0)
                {

                    if (playerLocation.objects.ContainsKey(tileVector))
                    {

                        continue;

                    }

                }

                foreach (Furniture item in playerLocation.furniture)
                {

                    if (item.boundingBox.Value.Contains(tileX * 64, tileY * 64))
                    {

                        continue;

                    }

                }

                Tile backTile = backLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                if (backTile != null)
                {

                    if (backTile.TileIndexProperties.TryGetValue("Water", out _))
                    {

                        if (activeData.spawnIndex["fishup"])
                        {

                            effectCasts[tileVector] = new Cast.Water(this, tileVector, targetPlayer);

                            continue;

                        }

                    }

                    if (backTile.TileIndexProperties.TryGetValue("Type", out var typeValue))
                    {

                        if (typeValue == "Dirt" || backTile.TileIndexProperties.TryGetValue("Diggable", out _))
                        {

                            effectCasts[tileVector] = new Cast.Dirt(this, tileVector, targetPlayer, activeData.spawnIndex);

                            continue;

                        }

                        if (typeValue == "Grass" && backTile.TileIndexProperties.TryGetValue("NoSpawn", out _) == false)
                        {

                            effectCasts[tileVector] = new Cast.Lawn(this, tileVector, targetPlayer, activeData.spawnIndex);

                            continue;

                        }


                    }

                }

            }

            //-------------------------- fire effects

            if (effectCasts.Count != 0)
            {
                foreach (KeyValuePair<Vector2, Cast.Cast> effectEntry in effectCasts)
                {

                    if (activeData.removeVectors.Contains(effectEntry.Key)) // ignore tiles covered by clumps
                    {

                        continue;

                    }

                    Cast.Cast effectHandle = effectEntry.Value;

                    effectHandle.CastEarth();

                    if (effectHandle.castFire)
                    {

                        castCost += effectHandle.castCost;

                        experienceGain++;

                    }

                    if (effectHandle.castActive)
                    {

                        ActiveCast(effectHandle);

                    }

                    ModUtility.AnimateEarth(targetPlayer.currentLocation,effectHandle.targetVector);

                }

            }

            //-------------------------- effect on player

            if (castCost > 0)
            {

                float oldStamina = Game1.player.Stamina;

                staminaCost = Math.Min(castCost, oldStamina - 1);

                Game1.player.Stamina -= staminaCost;

                Game1.player.checkForExhaustion(oldStamina);

                Game1.player.gainExperience(2, experienceGain); // gain foraging experience

            }

            //Monitor.Log($"Cast Earth, Level {activeData.castLevel}, Location {playerLocation.Name}, Position {playerVector}, Energy {staminaCost}, Experience {experienceGain}.", LogLevel.Debug);

            return;

        }

        private void AnimateWater()
        {

            if (activeData.activeDirection == -1)
            {

                activeData.activeDirection = Game1.player.FacingDirection;

            }

            if(activeData.chargeLevel > 1)
            {

                //-------------------------- cast variables

                int animationRow;

                Microsoft.Xna.Framework.Rectangle animationRectangle;

                float animationInterval;

                int animationLength;

                int animationLoops;

                bool animationFlip;

                float animationScale;

                Microsoft.Xna.Framework.Color animationColor;

                Vector2 animationPosition;

                TemporaryAnimatedSprite newAnimation;

                //-------------------------- cast shadow

                float activePosition = (activeData.chargeLevel) * 64;

                Dictionary<int, float> xPoints = new()
                {

                    [0] = 0,// up
                    [1] = activePosition, // right
                    [2] = 0,// down
                    [3] = 0 - activePosition, // left

                };

                Dictionary<int, float> yPoints = new()
                {

                    [0] = 0 - activePosition,// up
                    [1] = 0, // right
                    [2] = activePosition,// down
                    [3] = 0, // left

                };

                animationRow = 5;

                animationRectangle = new(0, animationRow * 64, 64, 64);

                animationInterval = 100f;

                animationLength = 4;

                animationLoops = 1;

                animationFlip = false;

                animationColor = new(0, 0, 0, 0.5f);

                switch (activeData.chargeLevel)
                {

                    case 2:
                        animationScale = 0.3f;
                        animationPosition = new Vector2(Game1.player.Position.X + xPoints[activeData.activeDirection] + 21, Game1.player.Position.Y + yPoints[activeData.activeDirection]);
                        break;
                    case 3:
                        animationScale = 0.4f;
                        animationPosition = new Vector2(Game1.player.Position.X + xPoints[activeData.activeDirection] + 18, Game1.player.Position.Y + yPoints[activeData.activeDirection]);
                        break;
                    case 4:
                        animationScale = 0.5f;
                        animationPosition = new Vector2(Game1.player.Position.X + xPoints[activeData.activeDirection] + 15, Game1.player.Position.Y + yPoints[activeData.activeDirection]);
                        break;
                    case 5:
                        animationScale = 0.6f;
                        animationPosition = new Vector2(Game1.player.Position.X + xPoints[activeData.activeDirection] + 12, Game1.player.Position.Y + yPoints[activeData.activeDirection]);
                        break;
                    default:
                        animationScale = 0.7f;
                        animationPosition = new Vector2(Game1.player.Position.X + xPoints[activeData.activeDirection] + 9, Game1.player.Position.Y + yPoints[activeData.activeDirection]);
                        break;

                }

                newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlip, -1, 0f, animationColor, animationScale, 0f, 0f, 0f);

                Game1.currentLocation.temporarySprites.Add(newAnimation);

                //-------------------------- cast animation

                animationRow = 5;

                animationRectangle = new(0, animationRow * 64, 64, 64);

                animationInterval = 100f;

                animationLength = 4;

                animationLoops = 1;

                animationFlip = false;

                switch (activeData.chargeLevel)
                {

                    case 2:
                        animationColor = new(uint.MaxValue); // deepens from white to blue
                        animationScale = 0.6f;
                        animationPosition = new Vector2(Game1.player.Position.X + xPoints[activeData.activeDirection] + 12, Game1.player.Position.Y + yPoints[activeData.activeDirection] - 144f);
                        break;
                    case 3:
                        animationColor = new(0.9f, 0.9f, 1, 1);
                        animationFlip = true;
                        animationScale = 0.8f;
                        animationPosition = new Vector2(Game1.player.Position.X + xPoints[activeData.activeDirection] + 6, Game1.player.Position.Y + yPoints[activeData.activeDirection] - 160f);
                        break;
                    case 4:
                        animationColor = new(0.8f, 0.8f, 1, 1);
                        animationScale = 1.0f;
                        animationPosition = new Vector2(Game1.player.Position.X + xPoints[activeData.activeDirection], Game1.player.Position.Y + yPoints[activeData.activeDirection] - 176f);
                        break;
                    case 5:
                        animationColor = new(0.7f, 0.7f, 1, 1);
                        animationFlip = true;
                        animationScale = 1.2f;
                        animationPosition = new Vector2(Game1.player.Position.X + xPoints[activeData.activeDirection] - 6, Game1.player.Position.Y + yPoints[activeData.activeDirection] - 192f);
                        break;
                    default:
                        animationColor = new(0.6f, 0.6f, 1, 1);
                        animationScale = 1.4f;
                        animationPosition = new Vector2(Game1.player.Position.X + xPoints[activeData.activeDirection] - 12, Game1.player.Position.Y + yPoints[activeData.activeDirection] - 208f);
                        break;

                }

                newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlip, -1, 0f, animationColor, animationScale, 0f, 0f, 0f);

                Game1.currentLocation.temporarySprites.Add(newAnimation);

            }

            //-------------------------- hand animation

            ModUtility.AnimateHands(Game1.player, activeData.activeDirection, 350);

            activeData.animateLevel = activeData.chargeLevel;

            switch (activeData.chargeLevel) // increasing pitch
            {

                case 2:
                case 4:
                case 6:

                    DelayedAction.functionAfterDelay(CastWater, 283);
                    break;
                default:

                    Game1.currentLocation.playSoundPitched("thunder_small", 700 + activeData.chargeLevel * 100);

                    break;

            }

        }

        private void CastWater() {

            activeData.castLevel++;

            activeData.castLevel++;

            //-------------------------- tile effects

            float staminaCost = 0f;

            int castCost = 0;

            //int experienceGain = 0;

            Farmer targetPlayer = Game1.player;

            GameLocation playerLocation = Game1.player.currentLocation;
            
            Vector2 playerVector = Game1.player.getTileLocation();

            if (staticData.triggerList.ContainsKey("CastWater"))
            {

                List<string> castStrings = staticData.triggerList["CastWater"];

                for (int i = 0; i < castStrings.Count; i++)
                {

                    string castString = castStrings[i];

                    if (!staticData.challengeList[castString])
                    {

                        if (Map.Quest.ChallengeLocation(castString) == playerLocation.Name)
                        {

                            Vector2 ChallengeVector = Map.Quest.ChallengeVector(castString);

                            Vector2 ChallengeLimit = ChallengeVector + Map.Quest.ChallengeLimit(castString);

                            if (
                                playerVector.X >= ChallengeVector.X &&
                                playerVector.Y >= ChallengeVector.Y &&
                                playerVector.X < ChallengeLimit.X &&
                                playerVector.Y < ChallengeLimit.Y
                                )
                            {

                                Cast.Cast castHandle;

                                if (i == 0)
                                {
                                    castHandle = new Cast.Sword(this, playerVector, targetPlayer);
                                }
                                else
                                {
                                    castHandle = new Cast.Challenge(this, playerVector, targetPlayer);
                                }

                                castHandle.CastWater();

                                activeData.castComplete = true;

                                staticData.challengeList[castString] = true;

                                return;

                            }

                        }

                    }

                }

            }

            float activePosition = activeData.castLevel;

            Dictionary<int, float> xPoints = new()
            {

                [0] = 0,// up
                [1] = activePosition, // right
                [2] = 0,// down
                [3] = 0 - activePosition, // left

            };

            Dictionary<int, float> yPoints = new()
            {

                [0] = 0 - activePosition,// up
                [1] = 0, // right
                [2] = activePosition,// down
                [3] = 0, // left

            };

            Vector2 castVector = new(playerVector.X + xPoints[activeData.activeDirection], playerVector.Y + yPoints[activeData.activeDirection]);

            List<Vector2> tileVectors = new();

            Dictionary<Vector2, Cast.Cast> effectCasts = new();

            Layer backLayer = playerLocation.Map.GetLayer("Back");

            Layer buildingLayer = playerLocation.Map.GetLayer("Buildings");

            //Dictionary<string, bool> spawnIndex = Map.Spawn.SpawnIndex(playerLocation);

            //Dictionary<string, Vector2> swordIndex = Map.Spawn.SwordIndex("water");

            string locationName = playerLocation.Name.ToString();

            int k = (activeData.castLevel + 1);

            for (int i = 0; i < k; i++) {

                if(i == 0)
                {

                    tileVectors = new List<Vector2>
                    {
                        
                        castVector
                    
                    };

                } 
                else
                {

                    tileVectors = ModUtility.GetTilesWithinRadius(playerLocation, castVector, i);

                }

                foreach (Vector2 tileVector in tileVectors)
                {

                    if (activeData.servedVectors.Contains(tileVector)) // already served
                    {

                        continue;

                    }
                    else
                    {

                        activeData.servedVectors.Add(tileVector);

                    }

                    int tileX = (int)tileVector.X;

                    int tileY = (int)tileVector.Y;


                    if (warpPoints.ContainsKey(locationName))
                    {

                        if (warpPoints[locationName] == tileVector)
                        {

                            int targetIndex = warpTotems[locationName];

                            effectCasts[tileVector] = new Cast.Totem(this, tileVector, targetPlayer, targetIndex);

                            continue;

                        }

                    }

                    Tile buildingTile = buildingLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                    if (buildingTile != null)
                    {
                        continue;
                    }

                    if (playerLocation.terrainFeatures.ContainsKey(tileVector))
                    {

                        continue;

                    }

                    if (playerLocation.resourceClumps.Count > 0)
                    {

                        bool targetClump = false;

                        foreach (ResourceClump resourceClump in playerLocation.resourceClumps)
                        {

                            if (resourceClump.tile.Value == tileVector)
                            {

                                switch (resourceClump.parentSheetIndex.Value)
                                {

                                    case 600:
                                    case 602:

                                        effectCasts[tileVector] = new Cast.Stump(this, tileVector, targetPlayer, resourceClump);

                                        break;

                                    default:

                                        effectCasts[tileVector] = new Cast.Boulder(this, tileVector, targetPlayer, resourceClump);

                                        break;

                                }

                                Vector2 clumpVector = resourceClump.tile.Value;

                                activeData.removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y));

                                activeData.removeVectors.Add(new Vector2(clumpVector.X, clumpVector.Y + 1));

                                activeData.removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y + 1));

                                targetClump = true;

                                continue;

                            }

                        }

                        if (targetClump)
                        {

                            continue;

                        }

                    }

                    if (playerLocation.objects.Count() > 0)
                    {

                        if (playerLocation.objects.ContainsKey(tileVector))
                        {

                            StardewValley.Object targetObject = playerLocation.objects[tileVector];

                            if (craftList.Contains(targetObject.Name.ToString()))
                            {

                                if (targetObject.MinutesUntilReady > 0)
                                {

                                    effectCasts[tileVector] = new Cast.Craft(this, tileVector, targetPlayer, targetObject);

                                }

                            }
                            else if (targetObject.Name.Contains("Campfire"))
                            {

                                effectCasts[tileVector] = new Cast.Campfire(this, tileVector, targetPlayer);

                            }
                            else if (targetObject is Torch && targetObject.ParentSheetIndex == 93) // crafted candle torch
                            {

                                if (activeData.spawnIndex["wilderness"])
                                {

                                    effectCasts[tileVector] = new Cast.Portal(this, tileVector, targetPlayer);

                                }

                            }
                            else if (targetObject.IsScarecrow())
                            {

                                effectCasts[tileVector] = new Cast.Scarecrow(this, tileVector, targetPlayer);

                            }

                            continue;

                        }

                    }

                    foreach (Furniture item in playerLocation.furniture)
                    {

                        if (item.boundingBox.Value.Contains(tileX * 64, tileY * 64))
                        {

                            continue;

                        }

                    }

                    if (activeData.castLevel == 4 && activeData.spawnIndex["fishspot"])
                    { 
                        
                        Tile backTile = backLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                        if (backTile != null)
                        {

                            if (backTile.TileIndexProperties.TryGetValue("Water", out _))
                            {

                                effectCasts[tileVector] = new Cast.Water(this, tileVector, targetPlayer);

                                continue;

                            }

                        }

                    }

                }

            }

            Microsoft.Xna.Framework.Rectangle areaOfEffect = new(
                ((int)castVector.X - activeData.castLevel + 1) * 64, 
                ((int)castVector.Y - activeData.castLevel + 1) * 64, 
                (activeData.castLevel-1) * 128, 
                (activeData.castLevel-1) * 128
            );

            foreach (NPC nonPlayableCharacter in playerLocation.characters) {

                if (nonPlayableCharacter is Monster) {

                    Monster monsterCharacter = nonPlayableCharacter as Monster;

                    if(monsterCharacter.Health > 0 && !monsterCharacter.IsInvisible && !monsterCharacter.isInvincible() && monsterCharacter.GetBoundingBox().Intersects(areaOfEffect))
                    {
                        Vector2 monsterVector = monsterCharacter.getTileLocation();

                        effectCasts[monsterVector] = new Cast.Smite(this, monsterVector, targetPlayer, monsterCharacter);

                    }

                }

            }

            //-------------------------- fire effects

            List<Type> castLimits = new();

            if (effectCasts.Count != 0)
            {
                foreach (KeyValuePair<Vector2, Cast.Cast> effectEntry in effectCasts)
                {

                    if (activeData.removeVectors.Contains(effectEntry.Key)) // ignore tiles covered by clumps
                    {

                        continue;

                    }

                    Cast.Cast effectHandle = effectEntry.Value;

                    Type effectType = effectHandle.GetType();

                    if (castLimits.Contains(effectType))
                    {

                        continue;

                    }

                    effectHandle.CastWater();

                    if (effectHandle.castFire)
                    {

                        castCost += effectEntry.Value.castCost;

                    }

                    if (effectHandle.castLimit)
                    {

                        castLimits.Add(effectEntry.Value.GetType());

                    }

                    if(effectHandle.castActive)
                    {

                        ActiveCast(effectHandle);

                    }

                }

            }

            //-------------------------- effect on player

            if (castCost > 0)
            {

                float oldStamina = Game1.player.Stamina;

                staminaCost = Math.Min(castCost, oldStamina - 1);

                Game1.player.Stamina -= staminaCost;

                Game1.player.checkForExhaustion(oldStamina);

            }

            //Monitor.Log($"Cast Water, Level {activeData.castLevel}, Location {playerLocation.Name}, Position {playerVector}, Energy {staminaCost}, Experience {experienceGain}.", LogLevel.Debug);

        }
        
        private void AnimateStars()
        {

            activeData.animateLevel = activeData.chargeLevel;

            DelayedAction.functionAfterDelay(CastStars, 950);

            if(activeData.chargeLevel == 2)
            {

                Game1.currentLocation.playSound("Meteorite");

            }

        }

        private void CastStars()
        {

            activeData.castLevel++;

            //-------------------------- cast sound

            Game1.currentLocation.playSound("fireball");

            //-------------------------- tile effects

            float staminaCost = 0f;

            int castCost = 0;

            //int experienceGain = 0;

            Farmer targetPlayer = Game1.player;

            GameLocation playerLocation = Game1.player.currentLocation;

            Vector2 playerVector = Game1.player.getTileLocation();

            Random randomIndex = new();

            Dictionary<Vector2, Cast.Meteor> effectCasts = new();

            //Dictionary<string, Vector2> swordIndex = Map.Spawn.SwordIndex("stars");

            if (staticData.triggerList.ContainsKey("CastStars"))
            {

                List<string> castStrings = staticData.triggerList["CastStars"];

                for (int i = 0; i < castStrings.Count; i++)
                {

                    string castString = castStrings[i];

                    if (!staticData.challengeList[castString])
                    {

                        if (Map.Quest.ChallengeLocation(castString) == playerLocation.Name)
                        {

                            Vector2 ChallengeVector = Map.Quest.ChallengeVector(castString);

                            Vector2 ChallengeLimit = ChallengeVector + Map.Quest.ChallengeLimit(castString);

                            if (
                                playerVector.X >= ChallengeVector.X &&
                                playerVector.Y >= ChallengeVector.Y &&
                                playerVector.X < ChallengeLimit.X &&
                                playerVector.Y < ChallengeLimit.Y
                                )
                            {

                                Cast.Cast castHandle;

                                if (i == 0)
                                {
                                    castHandle = new Cast.Sword(this, playerVector, targetPlayer);
                                }
                                else
                                {
                                    castHandle = new Cast.Challenge(this, playerVector, targetPlayer);
                                }

                                castHandle.CastStars();

                                activeData.castComplete = true;

                                staticData.challengeList[castString] = true;

                                return;

                            }

                        }

                    }

                }

            }

            int castAttempt = (int) Math.Ceiling((decimal)activeData.castLevel / 2) + 1;

            List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(playerLocation, playerVector, 2 + castAttempt);

            int castSelect = castSelection.Count;

            if(castSelect != 0)
            {

                int castIndex;

                Vector2 castVector;

                int castSegment = castSelect / castAttempt;

                for (int i = 0; i < castAttempt; i++)
                {

                    castIndex = randomIndex.Next(castSegment) + (i * castSegment);

                    castVector = castSelection[castIndex];

                    effectCasts[castVector] = new Cast.Meteor(this, castVector, targetPlayer, activeData.activeDirection);

                }

            }
            //-------------------------- fire effects

            if (effectCasts.Count != 0)
            {
                foreach (KeyValuePair<Vector2, Cast.Meteor> effectEntry in effectCasts)
                {

                    Cast.Meteor effectHandle = effectEntry.Value;

                    effectHandle.CastStars();

                    if (effectHandle.castFire)
                    {

                        castCost += effectEntry.Value.castCost;

                    }

                }

            }

            //-------------------------- effect on player

            if (castCost > 0)
            {

                float oldStamina = Game1.player.Stamina;

                staminaCost = Math.Min(castCost, oldStamina - 1);

                Game1.player.Stamina -= staminaCost;

                Game1.player.checkForExhaustion(oldStamina);

            }

           // Monitor.Log($"Cast Stars, Level {activeData.castLevel}, Location {playerLocation.Name}, Position {playerVector}, Energy {staminaCost}, Experience {experienceGain}.", LogLevel.Debug);

        }

        public int SpecialLimit()
        {

            decimal limit = specialCasts / 3;

            return (int) Math.Floor(limit);

        }

        public void SpecialIncrement()
        {

            specialCasts++;

        }

        public string StatueChoice()
        {

            return staticData.statueChoice;

        }

        public void UpdateChoice(string choice)
        {

            staticData.statueChoice = choice;

            if(!staticData.blessingsReceived.Contains(choice))
            {

                staticData.blessingsReceived.Add(choice);

                ReviewQuests(false);

            }

        }

        public void ActiveCast(Cast.Cast castHandle)
        {

            Type handleType = castHandle.GetType();

            if (!activeCasts.ContainsKey(handleType))
            {

                activeCasts[handleType] = new();

            }

            activeCasts[handleType].Add(castHandle);

        }

        public void UpdateChallenge(string challenge, bool update)
        {

            staticData.challengeList[challenge] = update;

        }

        public void ReceiveChallenge(string challenge)
        {
            
            if (!staticData.challengesReceived.Contains(challenge))
            {

                staticData.challengesReceived.Add(challenge);

                ReviewQuests(false);

            }

        }

        public string CastControl()
        {

            string control = Config.buttonList[0];

            return control;

        }

        public List<string> ChallengesCompleted()
        {

            List<string> completed = new();

            foreach (KeyValuePair<string, bool> challenge in staticData.challengeList)
            { 
                
                if (challenge.Value)
                { 
                
                    completed.Add(challenge.Key);
                
                }

            }

            return completed;

        }

        public List<string> ChallengesReceived()
        {

            return staticData.challengesReceived;

        }

        public List<string> BlessingsReceived()
        {

            return staticData.blessingsReceived;

        }
        


    }

}
