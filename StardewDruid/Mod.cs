using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
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

        Map.Effigy druidEffigy;

        public Dictionary<int, string> TreeTypes;

        public Dictionary<string, List<Vector2>> earthCasts;

        public Dictionary<string, List<Vector2>> servedWater;

        private Dictionary<Type, List<Cast.Cast>> activeCasts;

        private int specialCasts;

        public Dictionary<string, Vector2> warpPoints;

        public Dictionary<string, int> warpTotems;

        Dictionary<string, QuestData> questIndex;

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

            questIndex = Map.Quest.QuestList();

            ReviewQuests();

            Config = Helper.ReadConfig<ModData>();

            buttonList = Config.buttonList;

            craftList = Config.craftList;

            earthCasts = new();

            activeCasts = new();

            specialCasts = 3;

            druidEffigy = new(this);

            druidEffigy.ModifyCave();

            warpPoints = Map.Warp.WarpPoints();

            warpTotems = Map.Warp.WarpTotems();

            //ChallengeAnimations = new();

            //ChallengeLocations = Map.Spawn.ChallengeLocations();

            return;

        }

        private void SaveUpdated(object sender, SavingEventArgs e)
        {

            //Helper.Data.WriteSaveData("staticData", staticData);

            druidEffigy.lessonGiven = false;

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

                activeData = new ActiveData();

                return;

            }

            // simulates interactions with the farm cave effigy
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

                            /*if(!staticData.questList["approachEffigy"])
                            {

                                Game1.player.completeQuest(Map.Quest.QuestId("approachEffigy"));

                                staticData.questList["approachEffigy"] = true;

                            }*/

                            druidEffigy.Approach(Game1.player);

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
                    
                    activeBlessing = staticData.activeBlessing;

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

                /*foreach (SButton buttonPressed in e.Pressed)
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

                }*/

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

        private void ReviewQuests()
        {

            if(staticData.questList.Count == 0)
            {

                string firstQuest = Map.Quest.FirstQuest();

                staticData.questList[firstQuest] = false;

            }

            foreach (KeyValuePair<string,bool> quest in staticData.questList)
            {

                if (!quest.Value)
                {

                    ReassignQuest(quest.Key);

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

            int castCost = 0;

            Farmer targetPlayer = Game1.player;

            staticData.defaultMagnetism = targetPlayer.MagneticRadius;

            targetPlayer.MagneticRadius = 10;

            DelayedAction.functionAfterDelay(ResetMagnetism, 500);

            GameLocation playerLocation = targetPlayer.currentLocation;

            Vector2 playerVector = targetPlayer.getTileLocation();

            if (staticData.triggerList.ContainsKey("CastEarth")) 
            {

                List<string> castStrings = staticData.triggerList["CastEarth"];

                for (int i = 0; i < castStrings.Count; i++)
                {

                    string castString = castStrings[i];

                    QuestData questData = questIndex[castString];

                    if (questData.triggerLocation == playerLocation.Name)
                    {

                        Vector2 ChallengeVector = questData.triggerVector;

                        Vector2 ChallengeLimit = ChallengeVector + questData.triggerLimit;

                        if (
                            playerVector.X >= ChallengeVector.X &&
                            playerVector.Y >= ChallengeVector.Y &&
                            playerVector.X < ChallengeLimit.X &&
                            playerVector.Y < ChallengeLimit.Y
                            )
                        {

                            Cast.Cast castHandle;

                            if (questData.triggerType == "sword")
                            {
                                castHandle = new Cast.Sword(this, playerVector, targetPlayer, questData);
                            }
                            else
                            {
                                castHandle = new Cast.Challenge(this, playerVector, targetPlayer, questData);
                            }

                            castHandle.CastEarth();

                            activeData.castComplete = true;

                            return;

                        }

                    }

                }

            }

            if (!staticData.blessingList.ContainsKey("earth"))
            {

                return;
            
            }

            List<Vector2> tileVectors = ModUtility.GetTilesWithinRadius(playerLocation, playerVector, activeData.castLevel);

            Dictionary<Vector2, Cast.Cast> effectCasts = new();

            Layer backLayer = playerLocation.Map.GetLayer("Back");

            Layer buildingLayer = playerLocation.Map.GetLayer("Buildings");

            int blessingLevel = staticData.blessingList["earth"];

            List<Vector2> clumpIndex;

            if (!earthCasts.ContainsKey(playerLocation.Name))
            {
                earthCasts[playerLocation.Name] = new();

            };

            foreach (Vector2 tileVector in tileVectors)
            {

                if (playerLocation is MineShaft) //&& activeData.castLevel >= 2)
                {

                    if (playerLocation.objects.Count() > 0)
                    {

                        if (playerLocation.objects.ContainsKey(tileVector))
                        {

                            if (blessingLevel >= 1)
                            {

                                StardewValley.Object tileObject = playerLocation.objects[tileVector];

                                if (tileObject.name.Contains("Weeds") || tileObject.name.Contains("Twig"))
                                {

                                    effectCasts[tileVector] = new Cast.Weed(this, tileVector, targetPlayer);

                                }

                            }

                            continue;

                        }

                    }

                    if (blessingLevel >= 5)
                    {
                        int probability = Game1.random.Next(10);

                        if (probability <= 2)
                        {

                            effectCasts[tileVector] = new Cast.Rockfall(this, tileVector, targetPlayer);

                        }

                    };

                    continue;

                }

                if (earthCasts[playerLocation.Name].Contains(tileVector) || activeData.removeVectors.Contains(tileVector)) // already served
                {

                    continue;

                }
                else
                {

                    earthCasts[playerLocation.Name].Add(tileVector);

                }

                int tileX = (int)tileVector.X;

                int tileY = (int)tileVector.Y;

                Tile buildingTile = buildingLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                if (buildingTile != null)
                {

                    if (buildingTile.TileIndexProperties.TryGetValue("Passable", out _) == false)
                    {

                        continue;

                    }

                }

                if (playerLocation.terrainFeatures.ContainsKey(tileVector))
                {

                    if (blessingLevel >= 2) {

                        TerrainFeature terrainFeature = playerLocation.terrainFeatures[tileVector];

                        switch (terrainFeature.GetType().Name.ToString())
                        {

                            case "Tree":

                                effectCasts[tileVector] = new Cast.Tree(this, tileVector, targetPlayer);

                                break;

                            case "Grass":

                                effectCasts[tileVector] = new Cast.Grass(this, tileVector, targetPlayer);

                                break;

                            case "HoeDirt":

                                if (!Game1.currentSeason.Equals("winter") && activeData.spawnIndex["cropseed"] && staticData.blessingList["earth"] >= 4)
                                {
                                    HoeDirt hoeDirt = terrainFeature as HoeDirt;

                                    effectCasts[tileVector] = new Cast.Hoed(this, tileVector, targetPlayer, hoeDirt);

                                }

                                break;

                            default:

                                break;

                        }

                    }

                    continue;

                }

                if (playerLocation.resourceClumps.Count > 0)
                {

                    bool targetClump = false;

                    clumpIndex = new()
                    {
                        tileVector,
                        tileVector + new Vector2(0,-1),
                        tileVector + new Vector2(-1,0),
                        tileVector + new Vector2(-1,-1)

                    };

                    foreach (ResourceClump resourceClump in playerLocation.resourceClumps)
                    {

                        foreach(Vector2 originVector in clumpIndex)
                        {

                            if (resourceClump.tile.Value == originVector)
                            {

                                if (blessingLevel >= 2)
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

                            if (blessingLevel >= 2)
                            {

                                effectCasts[tileVector] = new Cast.Bush(this, tileVector, targetPlayer, largeTerrainFeature);

                            }

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

                        if (blessingLevel >= 1)
                        {

                            StardewValley.Object tileObject = playerLocation.objects[tileVector];

                            if (tileObject.name.Contains("Weeds") || tileObject.name.Contains("Twig"))
                            {

                                effectCasts[tileVector] = new Cast.Weed(this, tileVector, targetPlayer);

                            }

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



                Tile backTile = backLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                if (backTile != null)
                {

                    if (backTile.TileIndexProperties.TryGetValue("Water", out _))
                    {

                        if (blessingLevel >= 2)
                        {

                            if (activeData.spawnIndex["fishup"])
                            {

                                effectCasts[tileVector] = new Cast.Water(this, tileVector, targetPlayer);

                                

                            }

                        }

                        continue;

                    }

                    if (blessingLevel >= 3)
                    {

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

                        //experienceGain++;

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

                float staminaCost = Math.Min(castCost, oldStamina - 1);

                Game1.player.Stamina -= staminaCost;

                Game1.player.checkForExhaustion(oldStamina);

            }

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

            Farmer targetPlayer = Game1.player;

            GameLocation playerLocation = Game1.player.currentLocation;
            
            Vector2 playerVector = Game1.player.getTileLocation();

            if (staticData.triggerList.ContainsKey("CastWater"))
            {

                List<string> castStrings = staticData.triggerList["CastWater"];

                for (int i = 0; i < castStrings.Count; i++)
                {

                    string castString = castStrings[i];

                    QuestData questData = questIndex[castString];

                    if (questData.triggerLocation == playerLocation.Name)
                    {

                        Vector2 ChallengeVector = questData.triggerVector;

                        Vector2 ChallengeLimit = ChallengeVector + questData.triggerLimit;

                        if (
                            playerVector.X >= ChallengeVector.X &&
                            playerVector.Y >= ChallengeVector.Y &&
                            playerVector.X < ChallengeLimit.X &&
                            playerVector.Y < ChallengeLimit.Y
                            )
                        {

                            Cast.Cast castHandle;

                            if (questData.triggerType == "sword")
                            {
                                castHandle = new Cast.Sword(this, playerVector, targetPlayer, questData);
                            }
                            else
                            {
                                castHandle = new Cast.Challenge(this, playerVector, targetPlayer, questData);
                            }

                            castHandle.CastWater();

                            activeData.castComplete = true;

                            return;

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

            int blessingLevel = staticData.blessingList["water"];

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

                            if (blessingLevel >= 1)
                            { 
                                int targetIndex = warpTotems[locationName];

                                effectCasts[tileVector] = new Cast.Totem(this, tileVector, targetPlayer, targetIndex);

                                continue;

                            }

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

                                if (blessingLevel >= 4)
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
                                
                                }

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
                                if (blessingLevel >= 2)
                                {
                                    if (targetObject.MinutesUntilReady > 0)
                                    {

                                        effectCasts[tileVector] = new Cast.Craft(this, tileVector, targetPlayer, targetObject);

                                    }

                                }

                            }
                            else if (playerLocation.IsFarm && targetObject.bigCraftable.Value && targetObject.ParentSheetIndex == 9)
                            {

                                if (blessingLevel >= 2)
                                {

                                    effectCasts[tileVector] = new Cast.Rod(this, tileVector, targetPlayer);
                                }

                            }
                            else if (targetObject.Name.Contains("Campfire"))
                            {
                                if (blessingLevel >= 5)
                                {
                                    effectCasts[tileVector] = new Cast.Campfire(this, tileVector, targetPlayer);
                                }
                            }
                            else if (targetObject is Torch && targetObject.ParentSheetIndex == 93) // crafted candle torch
                            {
                                if (blessingLevel >= 5)
                                {
                                    if (activeData.spawnIndex["wilderness"])
                                    {

                                        effectCasts[tileVector] = new Cast.Portal(this, tileVector, targetPlayer);

                                    }
                                }

                            }
                            else if (targetObject.IsScarecrow())
                            {
                                if (blessingLevel >= 2)
                                {
                                    effectCasts[tileVector] = new Cast.Scarecrow(this, tileVector, targetPlayer);
                                }
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
                                if (blessingLevel >= 3)
                                {
                                    effectCasts[tileVector] = new Cast.Water(this, tileVector, targetPlayer);

                                }
                                continue;

                            }

                        }

                    }

                }

            }

            if (blessingLevel >= 4)
            {

                Microsoft.Xna.Framework.Rectangle areaOfEffect = new(
                    ((int)castVector.X - activeData.castLevel + 1) * 64,
                    ((int)castVector.Y - activeData.castLevel + 1) * 64,
                    (activeData.castLevel - 1) * 128,
                    (activeData.castLevel - 1) * 128
                );

                foreach (NPC nonPlayableCharacter in playerLocation.characters)
                {

                    if (nonPlayableCharacter is Monster)
                    {

                        Monster monsterCharacter = nonPlayableCharacter as Monster;

                        if (monsterCharacter.Health > 0 && !monsterCharacter.IsInvisible && !monsterCharacter.isInvincible() && monsterCharacter.GetBoundingBox().Intersects(areaOfEffect))
                        {
                            Vector2 monsterVector = monsterCharacter.getTileLocation();

                            effectCasts[monsterVector] = new Cast.Smite(this, monsterVector, targetPlayer, monsterCharacter);

                        }

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

            Farmer targetPlayer = Game1.player;

            GameLocation playerLocation = Game1.player.currentLocation;

            Vector2 playerVector = Game1.player.getTileLocation();

            Random randomIndex = new();

            Dictionary<Vector2, Cast.Meteor> effectCasts = new();

            if (staticData.triggerList.ContainsKey("CastStars"))
            {

                List<string> castStrings = staticData.triggerList["CastStars"];

                for (int i = 0; i < castStrings.Count; i++)
                {

                    string castString = castStrings[i];

                    QuestData questData = questIndex[castString];

                    if (questData.triggerLocation == playerLocation.Name)
                    {

                        Vector2 ChallengeVector = questData.triggerVector;

                        Vector2 ChallengeLimit = ChallengeVector + questData.triggerLimit;

                        if (
                            playerVector.X >= ChallengeVector.X &&
                            playerVector.Y >= ChallengeVector.Y &&
                            playerVector.X < ChallengeLimit.X &&
                            playerVector.Y < ChallengeLimit.Y
                            )
                        {

                            Cast.Cast castHandle;

                            if (questData.triggerType == "sword")
                            {
                                castHandle = new Cast.Sword(this, playerVector, targetPlayer, questData);
                            }
                            else
                            {
                                castHandle = new Cast.Challenge(this, playerVector, targetPlayer, questData);
                            }

                            castHandle.CastStars();

                            activeData.castComplete = true;

                            return;

                        }

                    }

                }

            }

            if (staticData.blessingList["stars"] < 1)
            {

                return;

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

        public string ActiveBlessing()
        {

            return staticData.activeBlessing;

        }

        public Dictionary<string, int> BlessingList()
        {

            return staticData.blessingList;

        }

        public void UpdateBlessing(string choice)
        {

            staticData.activeBlessing = choice;

            if(!staticData.blessingList.ContainsKey(choice))
            {

                staticData.blessingList[choice] = 0;

            }

        }

        public void LevelBlessing(string blessing)
        {

            staticData.blessingList[blessing] += 1;

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

        public bool QuestComplete(string quest)
        {

            if(staticData.questList.ContainsKey(quest))
            {

                return staticData.questList[quest];

            }

            return false;

        }

        public List<string> QuestList()
        {

            List<string> received = new();

            foreach (KeyValuePair<string, bool> challenge in staticData.questList)
            {

                received.Add(challenge.Key);

            }

            return received;

        }

        public void NewQuest(string quest)
        {
            
            staticData.questList[quest] = false;

            ReassignQuest(quest, true);

        }

        public void UpdateQuest(string quest, bool update)
        {

            if(!update)
            {

                staticData.questList[quest] = false;

                ReassignQuest(quest);

            }
            else
            {

                staticData.questList[quest] = true;

                RemoveQuest(quest);

            }

        }

        public void ReassignQuest(string quest, bool showNew = false)
        {

            QuestData questData = questIndex[quest];

            if (questData.questId != 0)
            {

                StardewValley.Quests.Quest newQuest = new();

                newQuest.questType.Value = questData.questValue;
                newQuest.id.Value = questData.questId;
                newQuest.questTitle = questData.questTitle;
                newQuest.questDescription = questData.questDescription;
                newQuest.currentObjective = questData.questObjective;
                newQuest.showNew.Value = showNew;
                newQuest.moneyReward.Value = questData.questReward;

                Game1.player.questLog.Add(newQuest);

            }

            if (questData.triggerCast != null)
            {

                if(!staticData.triggerList.ContainsKey(questData.triggerCast))
                {

                    staticData.triggerList[questData.triggerCast] = new();

                }

                staticData.triggerList[questData.triggerCast].Add(quest);

            }

        }

        public void RemoveQuest(string quest)
        {

            QuestData questData = questIndex[quest];

            if (questData.questId != 0)
            {

                Game1.player.completeQuest(questData.questId);

            }

            if (questData.triggerCast != null)
            {

                staticData.triggerList[questData.triggerCast].Remove(quest);

                if (staticData.triggerList[questData.triggerCast].Count == 0)
                {

                    staticData.triggerList.Remove(questData.triggerCast);

                }

            }

            if (questData.updateEffigy == true)
            {

                druidEffigy.questCompleted = quest;

            }

        }

        public void UpdateEarthCasts(GameLocation location, Vector2 vector, bool update)
        {
            
            if (!earthCasts.ContainsKey(location.Name))
            {

                earthCasts.Add(location.Name, new());
            
            }

            if (!earthCasts[location.Name].Contains(vector) && update)
            {

                earthCasts[location.Name].Add(vector);

            }

            if (earthCasts[location.Name].Contains(vector) && !update)
            {

                earthCasts[location.Name].Remove(vector);

            }

        }

        public string CastControl()
        {

            string control = Config.buttonList[0];

            return control;

        }

        public void ResetMagnetism()
        {

            Game1.player.MagneticRadius = staticData.defaultMagnetism;

        }

    }

}
