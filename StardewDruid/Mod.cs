using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Quests;
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

        private Map.Effigy druidEffigy;

        public Dictionary<int, string> TreeTypes;

        public Dictionary<string, List<Vector2>> earthCasts;

        private Dictionary<Type, List<Cast.Cast>> activeCasts;

        private int specialCasts;

        private int specialLimit;

        public Dictionary<string, Vector2> warpPoints;

        public Dictionary<string, int> warpTotems;

        private Dictionary<string, Map.Quest> questIndex;

        private Queue<Rite> riteQueue;

        override public void Entry(IModHelper helper)
        {

            helper.Events.GameLoop.SaveLoaded += SaveLoaded;

            helper.Events.Input.ButtonsChanged += OnButtonsChanged;

            helper.Events.GameLoop.OneSecondUpdateTicked += EverySecond;

            helper.Events.GameLoop.Saving += SaveUpdated;

        }

        private void SaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            
            staticData = Helper.Data.ReadSaveData<StaticData>("staticData");

            staticData ??= new StaticData() {

                    /*questList = new()
                    {
                        ["approachEffigy"] = true,
                        ["swordEarth"] = true,
                        ["challengeEarth"] = true,
                    },

                    blessingList = new()
                    {
                        ["earth"] = 5,
                    },*/

                };

            questIndex = Map.QuestData.QuestList();

            //ReviewQuests();

            if (staticData.questList.Count == 0)
            {

                string firstQuest = Map.QuestData.FirstQuest();

                staticData.questList[firstQuest] = false;

                ReassignQuest(firstQuest);

            }

            Config = Helper.ReadConfig<ModData>();

            buttonList = Config.buttonList;

            craftList = Config.craftList;

            earthCasts = new();

            activeCasts = new();

            riteQueue = new();

            if (staticData.blessingList != null)
            {

                if (!staticData.blessingList.ContainsKey("special"))
                {

                    staticData.blessingList["special"] = 1;

                }

                specialLimit = 2 + (staticData.blessingList["special"] * 2);

                specialCasts = specialLimit;

            }

            druidEffigy = new(this);

            druidEffigy.ModifyCave();

            warpPoints = Map.WarpData.WarpPoints();

            warpTotems = Map.WarpData.WarpTotems();

            return;

        }

        private void SaveUpdated(object sender, SavingEventArgs e)
        {

            Helper.Data.WriteSaveData("staticData", staticData);

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

            activeData.castComplete = true;

            earthCasts = new();

            activeCasts = new();

            riteQueue = new();

            specialCasts = specialLimit;

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

            removeCast.Clear();

            foreach (Cast.Cast castInstance in activeCast)
            {

                castInstance.CastTrigger();

            }

            activeCast.Clear();

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

            activeData ??= new ActiveData();

            string activeBlessing = staticData.activeBlessing;

            switch (Game1.player.CurrentTool.InitialParentTileIndex)
            {

                case 15: // Forest Sword

                    if(staticData.blessingList.ContainsKey("earth"))
                    {

                        activeBlessing = "earth";

                    }

                    break;

                case 14: // Neptune's Glaive

                    if (staticData.blessingList.ContainsKey("water"))
                    {

                        activeBlessing = "water";

                    }

                    break;

                case 9: // Lava Katana

                    if (staticData.blessingList.ContainsKey("stars"))
                    {

                        activeBlessing = "stars";

                    }

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

                    Dictionary<string, bool> spawnIndex = Map.SpawnData.SpawnIndex(Game1.player.currentLocation);

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

                    activeData = new ActiveData
                    {
                        activeCast = activeBlessing,

                        spawnIndex = spawnIndex

                    };

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

                    int staminaRequired;

                    switch (activeBlessing)
                    {

                        case "water":

                            staminaRequired = 48;

                            break;

                        case "stars":

                            staminaRequired = 24;

                            break;

                        default: //"earth"

                            staminaRequired = 12;

                            break;

                    };

                    if (Game1.player.Stamina <= staminaRequired)
                    {

                        //Game1.addHUDMessage(new HUDMessage($"Not enough energy to perform rite of the {activeBlessing}", 3);
                        Game1.addHUDMessage(new HUDMessage("Not enough energy to perform rite", 3));

                        activeData.castComplete = true;

                        return;

                    }

                    activeData.activeKey = buttonHeld.ToString();

                    chargeEffect = true;

                    int chargeLevel;

                    //-------------------------- hand animation

                    if (activeData.activeDirection == -1)
                    {

                        activeData.activeDirection = Game1.player.FacingDirection;

                        activeData.activeVector = Game1.player.getTileLocation();

                        if(activeBlessing == "water")
                        {

                            Dictionary<int, Vector2> vectorIndex = new()
                            {

                                [0] = activeData.activeVector + new Vector2(0,-5),// up
                                [1] = activeData.activeVector + new Vector2(5, 0), // right
                                [2] = activeData.activeVector + new Vector2(0, 5),// down
                                [3] = activeData.activeVector + new Vector2(-5, 0), // left

                            };

                            activeData.activeVector = vectorIndex[activeData.activeDirection];

                        }

                    }

                    if (activeData.chargeAmount == 0 || activeData.chargeAmount % 12 == 0)
                    {
                        
                        ModUtility.AnimateHands(Game1.player, activeData.activeDirection, 225);
                    
                    }

                    List<Map.Quest> triggeredQuests = new();

                    Vector2 playerVector = activeData.activeVector;

                    foreach (string castString in staticData.triggerList)
                    {

                        Map.Quest questData = questIndex[castString];

                        if (questData.triggerLocation == Game1.player.currentLocation.Name)
                        {

                            bool triggerValid = true;

                            if (questData.startTime != 0)
                            {

                                if (Game1.timeOfDay < questData.startTime)
                                {

                                    triggerValid = false;

                                }

                            }

                            /*if (questData.endTime != 0)
                            {

                                if (Game1.timeOfDay > questData.endTime)
                                {

                                    triggerValid = false;

                                }

                            }*/

                            if(triggerValid)
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

                                    triggeredQuests.Add(questData);

                                }

                            }

                        }

                    }

                    foreach(Map.Quest questData in triggeredQuests)
                    {

                        Cast.Cast castHandle;

                        if (questData.triggerType == "sword")
                        {
                            castHandle = new Cast.Sword(this, playerVector, new Rite(), questData);
                        }
                        else
                        {
                            castHandle = new Cast.Challenge(this, playerVector, new Rite(), questData);
                        }

                        castHandle.CastQuest();

                        staticData.triggerList.Remove(questData.name);

                        activeData.castComplete = true;

                        return;

                    }


                    //-------------------------- cast animation

                    activeData.chargeAmount++;

                    chargeLevel = 3;

                    if (activeData.chargeAmount <= 40)
                    {

                        chargeLevel = 1;

                    }
                    else if (activeData.chargeAmount <= 80)
                    {

                        chargeLevel = 2;

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

                //if (activeData.animateLevel != activeData.chargeLevel)
                if (activeData.castLevel != activeData.chargeLevel)
                {

                    activeData.castLevel = activeData.chargeLevel;

                    Rite castRite = new()
                    {

                        //castLevel = activeData.castLevel++,
                        castLevel = activeData.castLevel,

                        direction = activeData.activeDirection,

                    };

                    switch (activeBlessing)
                    {

                        case "stars":

                            castRite.castType = "CastStars";

                            AnimateStars();

                            break;

                        case "water":

                            castRite.castType = "CastWater";

                            castRite.castVector = activeData.activeVector;

                            AnimateWater();

                            break;

                        default: //"earth"

                            AnimateEarth();

                            break;

                    }

                    riteQueue.Enqueue(castRite);

                    DelayedAction.functionAfterDelay(CastRite, 666);

                }

                activeData.activeCharge = true;

            }

            return;

        }

        /*private void ReviewQuests()
        {

            if(staticData.questList.Count == 0)
            {

                string firstQuest = Map.Quest.FirstQuest();

                staticData.questList[firstQuest] = false;

                ReassignQuest(firstQuest,true);

            }
            else
            {

                foreach (KeyValuePair<string, bool> quest in staticData.questList)
                {

                    if (!quest.Value)
                    {

                        ReassignQuest(quest.Key);

                    }

                }

            }

        }*/

        private void CastRite()
        { 
            
            if(riteQueue.Count != 0)
            {

                Rite rite = riteQueue.Dequeue();

                switch(rite.castType)
                {

                    case "CastStars":

                        CastStars(rite); break;

                    case "CastWater":

                        CastWater(rite); break;

                    default: //CastEarth

                        CastEarth(rite); break;
                }

            }
        
        }

        private void AnimateEarth()
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

            animationFlip = false;

            switch (activeData.chargeLevel)
            {

                case 1:
                    animationColor = new(uint.MaxValue); // deepens from white to green
                    animationScale = 0.8f;
                    animationPosition = Game1.player.Position + new Vector2(6f, -128f); // 2 tiles above player
                    break;
                case 2:
                    animationColor = new(0.8f, 1, 0.8f, 1);
                    animationScale = 1f;
                    animationPosition = Game1.player.Position + new Vector2(0f, -134f); // 2 tiles above player
                    break;
                default:
                    animationColor = new(0.6f, 1, 0.6f, 1);
                    animationScale = 1.2f;
                    animationPosition = Game1.player.Position + new Vector2(-6f, -140f); // 2 tiles above player
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
                //motion = new Vector2(0f, -0.1f)

            };

            Game1.currentLocation.temporarySprites.Add(newAnimation);

            //-------------------------- hand animation

            //activeData.animateLevel = activeData.chargeLevel;

            if(activeData.chargeLevel == 1)
            {

                Game1.currentLocation.playSoundPitched("discoverMineral", 800);

            }

            if (activeData.chargeLevel == 3)
            {

                Game1.currentLocation.playSoundPitched("discoverMineral", 1000);

            }

        }

        private void CastEarth(Rite riteData = null)
        {

            int castCost = 0;

            riteData ??= new();

            List<Vector2> removeVectors = new();

            Dictionary<Vector2, Cast.Cast> effectCasts = new();

            Layer backLayer = riteData.castLocation.Map.GetLayer("Back");

            Layer buildingLayer = riteData.castLocation.Map.GetLayer("Buildings");

            int blessingLevel = staticData.blessingList["earth"];

            List<Vector2> clumpIndex;

            if (!earthCasts.ContainsKey(riteData.castLocation.Name))
            {
                earthCasts[riteData.castLocation.Name] = new();

            };

            int castRange;

            for (int i = 0; i < 2; i++)
            {

                castRange = (riteData.castLevel * 2) - 1 + i;

                List<Vector2> tileVectors = ModUtility.GetTilesWithinRadius(riteData.castLocation, riteData.castVector, castRange);

                foreach (Vector2 tileVector in tileVectors)
                {

                    if (earthCasts[riteData.castLocation.Name].Contains(tileVector) || removeVectors.Contains(tileVector)) // already served
                    {

                        continue;

                    }
                    else
                    {

                        earthCasts[riteData.castLocation.Name].Add(tileVector);

                    }

                    int tileX = (int)tileVector.X;

                    int tileY = (int)tileVector.Y;

                    Tile buildingTile = buildingLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                    if (buildingTile != null)
                    {
                        
                        if (riteData.castLocation.Name.Contains("Beach"))
                        {

                            List<int> tidalList = new() { 60, 61, 62, 63, 77, 78, 79, 80, 94, 95, 96, 97, 104, 287, 288, 304, 305, 321, 362, 363 };

                            if (tidalList.Contains(buildingTile.TileIndex))
                            {

                                effectCasts[tileVector] = new Cast.Pool(this, tileVector, riteData);

                            }

                        }
                        
                        if (buildingTile.TileIndexProperties.TryGetValue("Passable", out _) == false)
                        {

                            continue;

                        }

                    }

                    if (riteData.castLocation.objects.Count() > 0)
                    {

                        if (riteData.castLocation.objects.ContainsKey(tileVector))
                        {

                            if (blessingLevel >= 1)
                            {

                                StardewValley.Object tileObject = riteData.castLocation.objects[tileVector];

                                if (tileObject.name.Contains("Stone"))
                                {

                                    if (Map.SpawnData.StoneIndex().Contains(tileObject.ParentSheetIndex))
                                    {

                                        effectCasts[tileVector] = new Cast.Weed(this, tileVector, riteData);

                                    }


                                }
                                else if (tileObject.name.Contains("Weeds") || tileObject.name.Contains("Twig"))
                                {

                                    effectCasts[tileVector] = new Cast.Weed(this, tileVector, riteData);

                                }

                            }

                            continue;

                        }

                    }

                    if (riteData.castLocation.terrainFeatures.ContainsKey(tileVector))
                    {

                        if (blessingLevel >= 2) {

                            TerrainFeature terrainFeature = riteData.castLocation.terrainFeatures[tileVector];

                            switch (terrainFeature.GetType().Name.ToString())
                            {

                                case "Tree":

                                    StardewValley.TerrainFeatures.Tree treeFeature = terrainFeature as StardewValley.TerrainFeatures.Tree;

                                    if (treeFeature.growthStage.Value >= 5)
                                    {

                                        effectCasts[tileVector] = new Cast.Tree(this, tileVector, riteData);

                                    }
                                    else if(staticData.blessingList["earth"] >= 4 && treeFeature.fertilized.Value == false)
                                    {

                                        effectCasts[tileVector] = new Cast.Sapling(this, tileVector, riteData);

                                    }

                                    break;

                                case "Grass":

                                    Cast.Grass effectGrass = new(this, tileVector, riteData);

                                    if (!Game1.currentSeason.Equals("winter") && riteData.spawnIndex["cropseed"] && staticData.blessingList["earth"] >= 4)
                                    {

                                        effectGrass.activateSeed = true;

                                    }

                                    effectCasts[tileVector] = effectGrass;

                                    break;

                                case "HoeDirt":

                                    if (!Game1.currentSeason.Equals("winter") && riteData.spawnIndex["cropseed"] && staticData.blessingList["earth"] >= 4)
                                    {

                                        effectCasts[tileVector] = new Cast.Hoed(this, tileVector, riteData);

                                    }

                                    break;

                                default:

                                    break;

                            }

                        }

                        continue;

                    }

                    if (riteData.castLocation.resourceClumps.Count > 0)
                    {

                        bool targetClump = false;

                        clumpIndex = new()
                        {
                            tileVector,
                            tileVector + new Vector2(0,-1),
                            tileVector + new Vector2(-1,0),
                            tileVector + new Vector2(-1,-1)

                        };

                        foreach (ResourceClump resourceClump in riteData.castLocation.resourceClumps)
                        {

                            foreach (Vector2 originVector in clumpIndex)
                            {

                                if (resourceClump.tile.Value == originVector)
                                {

                                    if (blessingLevel >= 2)
                                    {

                                        switch (resourceClump.parentSheetIndex.Value)
                                        {

                                            case 600:
                                            case 602:

                                                effectCasts[tileVector] = new Cast.Stump(this, tileVector, riteData, resourceClump);

                                                break;

                                            default:

                                                effectCasts[tileVector] = new Cast.Boulder(this, tileVector, riteData, resourceClump);

                                                break;

                                        }

                                    }

                                    Vector2 clumpVector = resourceClump.tile.Value;

                                    removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y));

                                    removeVectors.Add(new Vector2(clumpVector.X, clumpVector.Y + 1));

                                    removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y + 1));

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

                    if (riteData.castLocation.largeTerrainFeatures.Count > 0)
                    {

                        bool targetTerrain = false;

                        Microsoft.Xna.Framework.Rectangle tileRectangle = new(tileX * 64 + 1, tileY * 64 + 1, 62, 62);

                        foreach (LargeTerrainFeature largeTerrainFeature in riteData.castLocation.largeTerrainFeatures)
                        {
                            if (largeTerrainFeature.getBoundingBox().Intersects(tileRectangle))
                            {

                                if (blessingLevel >= 2)
                                {

                                    effectCasts[tileVector] = new Cast.Bush(this, tileVector, riteData, largeTerrainFeature);

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

                    foreach (Furniture item in riteData.castLocation.furniture)
                    {

                        if (item.boundingBox.Value.Contains(tileX * 64, tileY * 64))
                        {

                            continue;

                        }

                    }

                    if (riteData.castLocation is MineShaft)
                    {

                        MineShaft mineShaft = riteData.castLocation as MineShaft;

                        if (blessingLevel >= 5 && mineShaft.isTileOnClearAndSolidGround(tileX,tileY))
                        {
                            int probability = Game1.random.Next(10);

                            if (probability == 0)
                            {

                                effectCasts[tileVector] = new Cast.Rockfall(this, tileVector, riteData);

                            }

                        }

                        continue;

                    }

                    Tile backTile = backLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                    if (backTile != null)
                    {

                        

                        if (backTile.TileIndexProperties.TryGetValue("Water", out _))
                        {

                            if (blessingLevel >= 2)
                            {

                                if (riteData.spawnIndex["fishup"])
                                {

                                    if(riteData.castLocation.Name.Contains("Farm"))
                                    {

                                        effectCasts[tileVector] = new Cast.Pool(this, tileVector, riteData);

                                    }
                                    else
                                    {

                                        effectCasts[tileVector] = new Cast.Water(this, tileVector, riteData);

                                    }

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

                                    effectCasts[tileVector] = new Cast.Dirt(this, tileVector, riteData);

                                    continue;

                                }

                                if (typeValue == "Grass" && backTile.TileIndexProperties.TryGetValue("NoSpawn", out _) == false)
                                {

                                    effectCasts[tileVector] = new Cast.Lawn(this, tileVector, riteData);

                                    continue;

                                }

                            }

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

                    if (removeVectors.Contains(effectEntry.Key)) // ignore tiles covered by clumps
                    {

                        continue;

                    }

                    
                    Cast.Cast effectHandle = effectEntry.Value;

                    Type effectType = effectHandle.GetType();

                    if (castLimits.Contains(effectType))
                    {

                        continue;

                    }

                    effectHandle.CastEarth();

                    if (effectHandle.castFire)
                    {

                        castCost += effectHandle.castCost;

                        //experienceGain++;

                    }

                    if (effectHandle.castLimit)
                    {

                        castLimits.Add(effectEntry.Value.GetType());

                    }

                    if (effectHandle.castActive)
                    {

                        ActiveCast(effectHandle);

                    }

                    ModUtility.AnimateEarth(riteData.castLocation,effectHandle.targetVector);

                }

            }

            //-------------------------- effect on player

            if (castCost > 0)
            {

                float oldStamina = Game1.player.Stamina;

                float staminaCost = Math.Min(castCost, oldStamina - 1);

                //float staminaCost = 2;

                Game1.player.Stamina -= staminaCost;

                Game1.player.checkForExhaustion(oldStamina);

            }

            return;

        }

        private void AnimateWater()
        {


            //-------------------------- cast variables

            int animationRow = 5;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 84f;

            int animationLength = 4;

            int animationLoops = 1;

            bool animationFlip = false;

            float animationScale;

            float animationDepth = activeData.activeVector.X * 1000 + activeData.activeVector.Y;

            Microsoft.Xna.Framework.Color animationColor;

            Vector2 animationPosition;

            //-------------------------- cast shadow

            animationColor = new(0, 0, 0, 0.5f);

            switch (activeData.chargeLevel)
            {

                case 1:
                    animationScale = 0.2f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(24,24);
                    break;
                case 2:
                    animationScale = 0.4f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(18, 18);
                    break;
                default: // 3
                    animationScale = 0.6f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(12, 12);
                    break;

            }

            TemporaryAnimatedSprite shadowAnimationOne = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlip, animationDepth+1, 0f, animationColor, animationScale, 0f, 0f, 0f);

            Game1.currentLocation.temporarySprites.Add(shadowAnimationOne);

            switch (activeData.chargeLevel)
            {

                case 1:
                    animationScale = 0.3f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(21, 21);
                    break;
                case 2:
                    animationScale = 0.5f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(15, 15);
                    break;
                default: // 3
                    animationScale = 0.7f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(9, 9);
                    break;

            }

            TemporaryAnimatedSprite shadowAnimationTwo = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlip, animationDepth+2, 0f, animationColor, animationScale, 0f, 0f, 0f)
            {

                delayBeforeAnimationStart = 336,

            };

            Game1.currentLocation.temporarySprites.Add(shadowAnimationTwo);

            //-------------------------- cast animation

            switch (activeData.chargeLevel)
            {

                case 1:
                    animationColor = new(uint.MaxValue); // deepens from white to blue
                    animationScale = 0.6f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(18,-128f);
                    break;
                case 2:
                    animationColor = new(0.8f, 0.8f, 1, 1); // deepens from white to blue
                    animationScale = 1.0f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(6,-160f);
                    break;
                default: // 3
                    animationColor = new(0.6f, 0.6f, 1, 1);
                    animationFlip = true;
                    animationScale = 1.4f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(-6,-192f);
                    break;

            }

            TemporaryAnimatedSprite animationOne = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlip, animationDepth+3, 0f, animationColor, animationScale, 0f, 0f, 0f);

            Game1.currentLocation.temporarySprites.Add(animationOne);

            switch (activeData.chargeLevel)
            {
                case 1:
                    animationColor = new(0.9f, 0.9f, 1, 1);
                    animationScale = 0.8f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(12, -144f);
                    break;
                case 2:
                    animationColor = new(0.7f, 0.7f, 1, 1);
                    animationFlip = true;
                    animationScale = 1.2f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(0, -176f);
                    break;
                default:
                    animationColor = new(0.5f, 0.5f, 1, 1);
                    animationScale = 1.6f;
                    animationPosition = (activeData.activeVector * 64) + new Vector2(-12, -208f);
                    break;

            }

            TemporaryAnimatedSprite animationTwo = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlip, animationDepth+4, 0f, animationColor, animationScale, 0f, 0f, 0f)
            {

                delayBeforeAnimationStart = 336,

            };

            Game1.currentLocation.temporarySprites.Add(animationTwo);

            //-------------------------- hand animation

            //ModUtility.AnimateHands(Game1.player, activeData.activeDirection, 333);

            //activeData.animateLevel = activeData.chargeLevel;

            if (activeData.chargeLevel == 1)
            {

                Game1.currentLocation.playSoundPitched("thunder_small", 800);

            }

            if (activeData.chargeLevel == 3)
            {

                Game1.currentLocation.playSoundPitched("thunder_small", 1000);

            }

        }

        private void CastWater(Rite riteData = null)
        {

            //-------------------------- tile effects

            int castCost = 0;

            riteData ??= new();

            Vector2 castVector = riteData.castVector;

            List <Vector2> tileVectors;

            List<Vector2> removeVectors = new();

            Dictionary<Vector2, Cast.Cast> effectCasts = new();

            Layer backLayer = riteData.castLocation.Map.GetLayer("Back");

            Layer buildingLayer = riteData.castLocation.Map.GetLayer("Buildings");

            int blessingLevel = staticData.blessingList["water"];

            string locationName = riteData.castLocation.Name.ToString();

            int castRange; 

            for (int i = 0; i < 2; i++) {

                castRange = (riteData.castLevel * 2) - 2 + i;

                tileVectors = ModUtility.GetTilesWithinRadius(riteData.castLocation, castVector, castRange);

                foreach (Vector2 tileVector in tileVectors)
                {

                    int tileX = (int)tileVector.X;

                    int tileY = (int)tileVector.Y;

                    if (warpPoints.ContainsKey(locationName))
                    {

                        if (warpPoints[locationName] == tileVector)
                        {

                            if (blessingLevel >= 1)
                            { 
                                int targetIndex = warpTotems[locationName];

                                effectCasts[tileVector] = new Cast.Totem(this, tileVector, riteData, targetIndex);

                                continue;

                            }

                        }

                    }

                    Tile buildingTile = buildingLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                    if (buildingTile != null)
                    {
                        continue;
                    }

                    if (riteData.castLocation.terrainFeatures.ContainsKey(tileVector))
                    {

                        continue;

                    }

                    if (riteData.castLocation.resourceClumps.Count > 0)
                    {

                        bool targetClump = false;

                        foreach (ResourceClump resourceClump in riteData.castLocation.resourceClumps)
                        {

                            if (resourceClump.tile.Value == tileVector)
                            {

                                if (blessingLevel >= 4)
                                {

                                    switch (resourceClump.parentSheetIndex.Value)
                                    {

                                        case 600:
                                        case 602:

                                            effectCasts[tileVector] = new Cast.Stump(this, tileVector, riteData, resourceClump);

                                            break;

                                        default:

                                            effectCasts[tileVector] = new Cast.Boulder(this, tileVector, riteData, resourceClump);

                                            break;

                                    }

                                    Vector2 clumpVector = resourceClump.tile.Value;

                                    removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y));

                                    removeVectors.Add(new Vector2(clumpVector.X, clumpVector.Y + 1));

                                    removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y + 1));
                                
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

                    if (riteData.castLocation.objects.Count() > 0)
                    {

                        if (riteData.castLocation.objects.ContainsKey(tileVector))
                        {

                            StardewValley.Object targetObject = riteData.castLocation.objects[tileVector];

                            if (craftList.Contains(targetObject.Name.ToString()))
                            {
                                if (blessingLevel >= 2)
                                {
                                    if (targetObject.MinutesUntilReady > 0)
                                    {

                                        effectCasts[tileVector] = new Cast.Craft(this, tileVector, riteData);

                                    }

                                }

                            }
                            else if (riteData.castLocation.IsFarm && targetObject.bigCraftable.Value && targetObject.ParentSheetIndex == 9)
                            {

                                if (blessingLevel >= 2)
                                {

                                    effectCasts[tileVector] = new Cast.Rod(this, tileVector, riteData);
                                
                                }

                            }
                            else if (targetObject.Name.Contains("Campfire"))
                            {
                                if (blessingLevel >= 2)
                                {
                                    
                                    effectCasts[tileVector] = new Cast.Campfire(this, tileVector, riteData);

                                }
                            }
                            else if (targetObject is Torch && targetObject.ParentSheetIndex == 93) // crafted candle torch
                            {
                                if (blessingLevel >= 5)
                                {
                                    if (riteData.spawnIndex["wilderness"])
                                    {

                                        effectCasts[tileVector] = new Cast.Portal(this, tileVector, riteData);

                                    }
                                }

                            }
                            else if (targetObject.IsScarecrow())
                            {
                                if (blessingLevel >= 2 && !Game1.isRaining)
                                {
                                    effectCasts[tileVector] = new Cast.Scarecrow(this, tileVector, riteData);
                                }
                            }

                            continue;

                        }

                    }

                    foreach (Furniture item in riteData.castLocation.furniture)
                    {

                        if (item.boundingBox.Value.Contains(tileX * 64, tileY * 64))
                        {

                            continue;

                        }

                    }

                    if (riteData.castLevel == 1 && riteData.spawnIndex["fishspot"])
                    { 
                        
                        Tile backTile = backLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                        if (backTile != null)
                        {

                            if (backTile.TileIndexProperties.TryGetValue("Water", out _))
                            {
                                if (blessingLevel >= 3)
                                {

                                    Dictionary<int, Vector2> portalOffsets = new()
                                    {

                                        [0] = activeData.activeVector + new Vector2(0, -64),// up
                                        [1] = activeData.activeVector + new Vector2(64, 0), // right
                                        [2] = activeData.activeVector + new Vector2(0, 64),// down
                                        [3] = activeData.activeVector + new Vector2(-64, 0), // left

                                    };

                                    Cast.Water waterPortal = new(this, tileVector, riteData)
                                    {
                                        portalPosition = new Vector2(tileVector.X * 64, tileVector.Y * 64) + portalOffsets[riteData.direction]
                                    };

                                    effectCasts[tileVector] = waterPortal;

                                }

                                continue;

                            }

                        }

                    }

                }

            }

            if (blessingLevel >= 4)
            {
                Vector2 smiteVector = riteData.caster.getTileLocation();

                Microsoft.Xna.Framework.Rectangle castArea = new(
                    ((int)castVector.X - riteData.castLevel - 1) * 64,
                    ((int)castVector.Y - riteData.castLevel - 1) * 64,
                    (riteData.castLevel * 128) + 192,
                    (riteData.castLevel * 128) + 192
                );


                Microsoft.Xna.Framework.Rectangle smiteArea = new(
                    ((int)smiteVector.X - riteData.castLevel - 1) * 64,
                    ((int)smiteVector.Y - riteData.castLevel - 1) * 64,
                    (riteData.castLevel * 128) + 192,
                    (riteData.castLevel * 128) + 192
                );

                int smiteCount = 0;

                foreach (NPC nonPlayableCharacter in riteData.castLocation.characters)
                {

                    if (nonPlayableCharacter is Monster)
                    {

                        Monster monsterCharacter = nonPlayableCharacter as Monster;

                        if (monsterCharacter.Health > 0 && !monsterCharacter.IsInvisible && !monsterCharacter.isInvincible() && (monsterCharacter.GetBoundingBox().Intersects(castArea) || monsterCharacter.GetBoundingBox().Intersects(smiteArea)))
                        {
                            Vector2 monsterVector = monsterCharacter.getTileLocation();

                            effectCasts[monsterVector] = new Cast.Smite(this, monsterVector, riteData, monsterCharacter);

                            smiteCount++;

                        } 

                    }

                    if (smiteCount == 2)
                    {
                        break;
                    }

                }

            }

            //-------------------------- fire effects

            List<Type> castLimits = new();

            if (effectCasts.Count != 0)
            {
                foreach (KeyValuePair<Vector2, Cast.Cast> effectEntry in effectCasts)
                {

                    if (removeVectors.Contains(effectEntry.Key)) // ignore tiles covered by clumps
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

                float staminaCost = Math.Min(castCost, oldStamina - 1);

                //staminaCost = 18;

                Game1.player.Stamina -= staminaCost;

                Game1.player.checkForExhaustion(oldStamina);

            }

        }
        
        private void AnimateStars()
        {

            //activeData.animateLevel = activeData.chargeLevel;

            if(activeData.chargeLevel == 2)
            {

                Game1.currentLocation.playSound("Meteorite");

            }

        }

        private void CastStars(Rite riteData = null)
        {

            if (staticData.blessingList["stars"] < 1)
            {

                return;

            }

            //-------------------------- tile effects

            int castCost = 0;

            riteData ??= new();

            //-------------------------- cast sound

            Game1.currentLocation.playSound("fireball");

            Random randomIndex = new();

            Dictionary<Vector2, Cast.Meteor> effectCasts = new();

            int castAttempt = riteData.castLevel + 1;

            List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(riteData.castLocation, riteData.castVector, 2 + castAttempt);

            int castSelect = castSelection.Count;

            if(castSelect != 0)
            {

                int castIndex;

                Vector2 newVector;

                int castSegment = castSelect / castAttempt;

                for (int i = 0; i < castAttempt; i++)
                {

                    castIndex = randomIndex.Next(castSegment) + (i * castSegment);

                    newVector = castSelection[castIndex];

                    effectCasts[newVector] = new Cast.Meteor(this, newVector, riteData);

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

                float staminaCost = Math.Min(castCost, oldStamina - 1);

                //staminaCost = 8;

                Game1.player.Stamina -= staminaCost;

                Game1.player.checkForExhaustion(oldStamina);

            }

        }

        public int SpecialLimit()
        {

            if (specialCasts == (specialLimit * 6))
            {

                Game1.addHUDMessage(new HUDMessage("Special power has almost completely dissipated", 2));

            }

            decimal limit = specialCasts / specialLimit;

            return (int) Math.Floor(limit);

        }

        public void SpecialIncrement()
        {

            specialCasts++;

            if (specialCasts == (specialLimit * 3))
            {

                Game1.addHUDMessage(new HUDMessage("Special power starts to wane", 2));

            }

        }

        public string ActiveBlessing()
        {

            return staticData.activeBlessing;

        }

        public Dictionary<string, int> BlessingList()
        {

            return staticData.blessingList;

        }

        public void UpdateBlessing(string blessing)
        {

            staticData.activeBlessing = blessing;

            if(!staticData.blessingList.ContainsKey(blessing))
            {

                staticData.blessingList[blessing] = 0;

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

            ReassignQuest(quest);

        }

        public bool QuestGiven(string quest)
        {

            return staticData.questList.ContainsKey(quest);

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

        public void ReassignQuest(string quest)
        {

            Map.Quest questData = questIndex[quest];

            if (questData.questId != 0)
            {

                if(!Game1.player.hasQuest(questData.questId))
                {

                    StardewValley.Quests.Quest newQuest = new();

                    newQuest.questType.Value = questData.questValue;
                    newQuest.id.Value = questData.questId;
                    newQuest.questTitle = questData.questTitle;
                    newQuest.questDescription = questData.questDescription;
                    newQuest.currentObjective = questData.questObjective;
                    newQuest.showNew.Value = true;
                    newQuest.moneyReward.Value = questData.questReward;

                    Game1.player.questLog.Add(newQuest);

                }

            }

            if (questData.triggerCast != null)
            {

                if(!staticData.triggerList.Contains(quest))
                {
                    
                    staticData.triggerList.Add(quest);

                }

            }

        }

        public void RemoveQuest(string quest)
        {

            Map.Quest questData = questIndex[quest];

            if (questData.questId != 0)
            {

                Game1.player.completeQuest(questData.questId);

            }

            if (questData.triggerCast != null)
            {

                staticData.triggerList.Remove(quest);


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

            if (activeData.castComplete || activeData.activeCast != "earth")
            {
                
                Game1.player.MagneticRadius -= 10;

            }

        }

    }

}
