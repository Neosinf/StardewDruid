using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;
using Force.DeepCloner;
using StardewValley.Quests;
using static StardewValley.Minigames.MineCart.Whale;
using StardewValley.Menus;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace StardewDruid
{
    public class Mod : StardewModdingAPI.Mod
    {
        
        public ModData Config;

        //private string activeBlessing;

        private ActiveData activeData;

        private StaticData staticData;

        public Dictionary<int, string> weaponAttunement;

        private MultiplayerData multiplayerData;

        private Map.Effigy druidEffigy;

        public Dictionary<int, string> TreeTypes;

        public Dictionary<string, List<Vector2>> earthCasts;

        private Dictionary<Type, List<Cast.CastHandle>> activeCasts;

        public string activeLockout;

        public Dictionary<string, Vector2> warpPoints;

        public Dictionary<string, Vector2> firePoints;

        public Dictionary<string, int> warpTotems;

        public List<string> warpCasts;

        public List<string> fireCasts;

        private Dictionary<string, Map.Quest> questIndex;

        private Dictionary<string, List<Monster>> monsterSpawns;

        private Queue<Rite> riteQueue;

        private Queue<int> castBuffer;

        public StardewValley.Tools.Pickaxe targetPick;

        public StardewValley.Tools.Axe targetAxe;

        public Dictionary<string, string> locationPoll;

        public StardewValley.NPC disembodiedVoice;

        private bool trainedToday;

        public List<string> triggerList;

        public bool receivedData;

        public string mineShaftName;

        override public void Entry(IModHelper helper)
        {

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;

            helper.Events.GameLoop.SaveLoaded += SaveLoaded;

            helper.Events.Input.ButtonsChanged += OnButtonsChanged;

            helper.Events.GameLoop.OneSecondUpdateTicked += EverySecond;

            helper.Events.GameLoop.Saving += SaveUpdated;

            helper.Events.Multiplayer.ModMessageReceived += OnModMessageReceived;

        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {

            Config = Helper.ReadConfig<ModData>();

            ConfigMenu.MenuConfig(this);
            
        }

        private void SaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            
            if (Context.IsMainPlayer) {

                staticData = Helper.Data.ReadSaveData<StaticData>("staticData");

            }
            else if(!receivedData)
            {

                StaticData loadData = new();

                Helper.Multiplayer.SendMessage(loadData, "FarmhandRequest", modIDs: new[] { this.ModManifest.UniqueID });

            }

            staticData ??= new StaticData() {  staticVersion = Map.QuestData.StableVersion() };

            StaticChecks();

            questIndex = Map.QuestData.QuestList();

            warpPoints = Map.WarpData.WarpPoints();

            warpTotems = Map.WarpData.WarpTotems();

            firePoints = Map.FireData.FirePoints();

            Map.TileData.LoadSheets();

            druidEffigy = new(
                this,
                Config.farmCaveStatueX,
                Config.farmCaveStatueY,
                Config.farmCaveHideStatue,
                Config.farmCaveMakeSpace
            );

            druidEffigy.ModifyCave();

            ReadyState();

        }

        private void StaticChecks()
        {

            int stableVersion = Map.QuestData.StableVersion();

            if (Config.masterStart && !staticData.blessingList.ContainsKey("masterStart"))
            {

                staticData = new StaticData() { staticVersion = stableVersion };

                staticData = Map.QuestData.ConfigureProgress(staticData,14);

                staticData.blessingList["masterStart"] = 1;

                return;

            }
            else if (staticData.blessingList.ContainsKey("masterStart"))
            {

                staticData = new StaticData() { staticVersion = stableVersion };

            }

            if (Config.setProgress != -1)
            {
                
                if (!staticData.blessingList.ContainsKey("setProgress"))
                {

                    staticData = new StaticData() { staticVersion = stableVersion };

                    staticData = Map.QuestData.ConfigureProgress(staticData, Config.setProgress);

                    return;

                } 
                
                if(staticData.blessingList["setProgress"] != Config.setProgress)
                {

                    staticData = new StaticData() { staticVersion = stableVersion };

                    staticData = Map.QuestData.ConfigureProgress(staticData, Config.setProgress);

                    return;
                
                }

            }

            if (staticData.staticVersion != stableVersion)
            {

                staticData = Map.QuestData.QuestCheck(staticData);

                staticData.staticVersion = stableVersion;

            }

        }

        private void SaveUpdated(object sender, SavingEventArgs e)
        {
            if(Context.IsMainPlayer)
            {
                
                Helper.Data.WriteSaveData("staticData", staticData);

            }
            else
            {
                
                Helper.Multiplayer.SendMessage(staticData, "FarmhandSave", modIDs: new[] { this.ModManifest.UniqueID });

            }

            druidEffigy.lessonGiven = false;

            foreach (KeyValuePair<Type, List<Cast.CastHandle>> castEntry in activeCasts)
            {
                
                if (castEntry.Value.Count > 0)
                {
                    
                    foreach (Cast.CastHandle castInstance in castEntry.Value)
                    {
                        
                        castInstance.CastRemove();
                    
                    }
                
                }

            }

            foreach (KeyValuePair<string, List<Monster>> monsterEntry in monsterSpawns)
            {
                
                if (monsterEntry.Value.Count > 0)
                {
                    
                    foreach (Monster monsterInstance in monsterEntry.Value)
                    {
                        
                        Game1.getLocationFromName(monsterEntry.Key).characters.Remove(monsterInstance);
                    
                    }
                
                }

            }

            //activeData.castComplete = true;
            activeData.castInterrupt = true;

            if (disembodiedVoice != null)
            {

                GameLocation previous = disembodiedVoice.currentLocation;

                previous.characters.Remove(disembodiedVoice);

                disembodiedVoice = null;

            }

            ReadyState();

        }

        public void OnModMessageReceived(object sender, ModMessageReceivedEventArgs e)
        {

            if (e.FromModID == ModManifest.UniqueID)
            {

                if (Context.IsMainPlayer)
                {

                    if (e.Type == "FarmhandSave")
                    {
                        StaticData farmhandData = e.ReadAs<StaticData>();

                        multiplayerData ??= Helper.Data.ReadSaveData<MultiplayerData>("multiplayerData");

                        multiplayerData ??= new MultiplayerData();

                        multiplayerData.farmhandData[e.FromPlayerID] = farmhandData;

                        Helper.Data.WriteSaveData("multiplayerData", multiplayerData);

                        //Game1.addHUDMessage(new HUDMessage($"Saved Stardew Druid data for Farmer ID {e.FromPlayerID}", ""));

                        Console.WriteLine($"Saved Stardew Druid data for Farmer ID {e.FromPlayerID}");

                    }

                    if (e.Type == "FarmhandRequest")
                    {
                        multiplayerData ??= Helper.Data.ReadSaveData<MultiplayerData>("multiplayerData");

                        multiplayerData ??= new MultiplayerData();

                        StaticData farmhandData = multiplayerData.farmhandData[e.FromPlayerID];

                        farmhandData.staticId = e.FromPlayerID;
                        
                        this.Helper.Multiplayer.SendMessage(farmhandData, "FarmhandLoad", modIDs: new[] { this.ModManifest.UniqueID });

                        //Game1.addHUDMessage(new HUDMessage($"Sent Stardew Druid data to Farmer ID {e.FromPlayerID}", ""));
                        Console.WriteLine($"Sent Stardew Druid data to Farmer ID {e.FromPlayerID}");
                    }

                }
                else if (e.Type == "FarmhandLoad" || e.Type == "FarmhandTrain")
                {

                    StaticData farmhandData = e.ReadAs<StaticData>();
                    
                    if(farmhandData.staticId == Game1.player.UniqueMultiplayerID)
                    {

                        if (e.Type == "FarmhandTrain")
                        {

                            if (trainedToday) { return; }

                            trainedToday = true;

                        }

                        staticData = farmhandData;

                        StaticChecks();

                        //Game1.addHUDMessage(new HUDMessage($"Received Stardew Druid data for Farmer ID {e.FromPlayerID}", ""));
                        Console.WriteLine($"Received Stardew Druid data for Farmer ID {e.FromPlayerID}");

                        ReadyState();

                        receivedData = true;

                    }

                }

            }
        
        }

        public void ReadyState()
        {

            weaponAttunement = Config.weaponAttunement.DeepClone();

            foreach(KeyValuePair<int,string> kvp in staticData.weaponAttunement)
            {

                weaponAttunement[kvp.Key] = kvp.Value;

            }

            triggerList = new();

            activeData = new ActiveData() { activeBlessing = staticData.activeBlessing };

            earthCasts = new();

            activeCasts = new();

            activeLockout = "none";

            riteQueue = new();

            castBuffer = new();

            druidEffigy.DecorateCave();

            warpCasts = new();

            fireCasts = new();

            monsterSpawns = new();

            targetPick = new();

            targetPick.DoFunction(Game1.player.currentLocation, 0, 0, 1, Game1.player);

            targetAxe = new();

            targetAxe.DoFunction(Game1.player.currentLocation, 0, 0, 1, Game1.player);

            locationPoll = new();

            // ---------------------- trigger assignment

            if (staticData.questList.Count == 0)
            {

                string firstQuest = Map.QuestData.FirstQuest();

                staticData.questList[firstQuest] = false;

            }

            foreach (KeyValuePair<string,bool> questRecord in staticData.questList)
            {

                if (!questRecord.Value)
                {

                    ReassignQuest(questRecord.Key);

                }

            }

            List<string> resetTriggers = QuestData.ResetTriggers();

            foreach(string trigger in resetTriggers)
            {

                if (!triggerList.Contains(trigger))
                {

                    triggerList.Add(trigger);

                }

            }

            return;

        }

        private void EverySecond(object sender, OneSecondUpdateTickedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
            {

                return;

            }

            if (activeCasts.Count == 0)
            {

                activeLockout = "none";

                return;

            }
            
            List<Cast.CastHandle> activeCast = new();

            List<Cast.CastHandle> removeCast = new();

            foreach (KeyValuePair<Type, List<Cast.CastHandle>> castEntry in activeCasts)
            {

                int entryCount = castEntry.Value.Count;

                if (castEntry.Value.Count > 0)
                {

                    int castIndex = 0;

                    foreach (Cast.CastHandle castInstance in castEntry.Value)
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

            foreach (Cast.CastHandle castInstance in removeCast)
            {
                
                castInstance.CastRemove();

                Type castType = castInstance.GetType();

                activeCasts[castType].Remove(castInstance);

                if (activeCasts[castType].Count == 0)
                {

                    activeCasts.Remove(castType);

                }

            }

            removeCast.Clear();

            bool exitAll = false;

            if (Game1.eventUp || Game1.fadeToBlack || Game1.currentMinigame != null || Game1.isWarping || Game1.killScreen)
            {
                exitAll = true;
            
            }

            foreach (Cast.CastHandle castInstance in activeCast)
            {
                if (exitAll)
                {

                    castInstance.CastRemove();

                    Type castType = castInstance.GetType();

                    activeCasts[castType].Remove(castInstance);

                    if (activeCasts[castType].Count == 0)
                    {

                        activeCasts.Remove(castType);

                    }

                }
                if (!Game1.shouldTimePass() || !Game1.game1.IsActive)
                {

                    castInstance.CastExtend();
                    
                }
                else
                {

                    castInstance.CastTrigger();

                }

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
            if (CasterBusy())
            {

                activeData.castInterrupt = true;

                riteQueue.Clear();

                castBuffer.Clear();

                return;

            }

            // simulates interactions with the farm cave effigy
            if (Game1.currentLocation.Name == "FarmCave")
            {

                Vector2 playerLocation = Game1.player.getTileLocation();

                Vector2 cursorLocation = Game1.currentCursorTile;

                if (playerLocation.X >= Config.farmCaveStatueX - 1 && playerLocation.X <= Config.farmCaveActionX + 1 && playerLocation.Y == Config.farmCaveActionY)// && cursorLocation.X == 6 && (cursorLocation.Y == 2 || cursorLocation.Y == 3))
                {

                    foreach (SButton buttonPressed in e.Pressed)
                    {

                        if (buttonPressed.IsUseToolButton() || buttonPressed.IsActionButton())
                        {

                            Helper.Input.Suppress(buttonPressed);

                            druidEffigy.DialogueApproach();

                            return;

                        }

                    }

                }

                if (Config.riteButtons.GetState() == SButtonState.Pressed)
                {
                    druidEffigy.DialogueApproach();

                    return;
                }

            }

            // ignore if player is busy with something else
            if (CasterTool())
            {

                activeData.castInterrupt = true;

                riteQueue.Clear();

                castBuffer.Clear();

                return;

            }

            if (activeData.toolIndex != Game1.player.CurrentTool.InitialParentTileIndex)
            {

                activeData.castInterrupt = true;

            }

            if (Config.riteButtons.GetState() == SButtonState.Pressed)
            {
                //Monitor.Log($"{Game1.player.currentLocation.Name},{Game1.player.currentLocation.GetType().Name}",LogLevel.Debug);
                // new cast configuration
                if ((riteQueue.Count != 0 || castBuffer.Count != 0) && !(Config.unrestrictedStars && activeData.activeBlessing == "stars"))
                {

                    return;

                }

                if(!ResetCast())
                {
                    
                    return;

                }

            }
            else if (Config.riteButtons.GetState() != SButtonState.Held)
            {

                return;

            }

            activeData.chargeAmount--;

            if (activeData.chargeAmount > 0) {

                return;

            }

            if( activeData.castInterrupt)
            {

                if (!ResetCast())
                {

                    return;

                }

            }

            activeData.castLevel++;

            if (activeData.castLevel == 5)
            {

                if (activeData.activeBlessing == "water")
                {

                    activeData.activeDirection = -1;

                }

                activeData.castLevel = 1;

                activeData.cycleLevel++;

            }

            activeData.chargeAmount = 40;

            if (activeData.activeBlessing == "stars")
            {

                activeData.chargeAmount = 80;

            }

            if (activeData.activeBlessing != "water")
            {

                activeData.activeDirection = -1;

            }

            if (activeData.activeDirection == -1)
            {

                activeData.activeDirection = Game1.player.FacingDirection;

                activeData.activeVector = Game1.player.getTileLocation();

                if (activeData.activeBlessing == "water")
                {

                    Dictionary<int, Vector2> vectorIndex = new()
                    {

                        [0] = activeData.activeVector + new Vector2(0, -5),// up
                        [1] = activeData.activeVector + new Vector2(5, 0), // right
                        [2] = activeData.activeVector + new Vector2(0, 5),// down
                        [3] = activeData.activeVector + new Vector2(-5, 0), // left

                    };

                    activeData.activeVector = vectorIndex[activeData.activeDirection];

                }

            }

            if (activeData.castLevel == 1)
            {

                if (EventHandler(activeData.activeVector))
                {
                    activeData.castInterrupt = true;
                    //activeData.castComplete = true;

                    return;
                }

            }

            //int staminaRequired = 48;
            int staminaRequired = 32;

            // check player has enough energy for eventual costs
            if (Game1.player.Stamina <= staminaRequired)
            {

                if (Config.consumeRoughage || Config.consumeQuicksnack)
                {

                    int grizzleConsume;

                    int grizzlePower;

                    float staminaUp;

                    bool sashimiPower = false;

                    List<int> grizzleList = Map.SpawnData.GrizzleList();

                    List<int> sashimiList = Map.SpawnData.SashimiList();

                    Dictionary<int,int> coffeeList = Map.SpawnData.CoffeeList();

                    int checkIndex;

                    for (int i = 0; i < Game1.player.Items.Count; i++)
                    {

                        if (Game1.player.Stamina == Game1.player.MaxStamina)
                        {

                            break;

                        }

                        Item checkItem = Game1.player.Items[i];

                        // ignore empty slots
                        if (checkItem == null)
                        {

                            continue;

                        }

                        int itemIndex = checkItem.ParentSheetIndex;

                        if (Config.consumeRoughage)
                        {
                            checkIndex = grizzleList.IndexOf(itemIndex);

                            if (checkIndex != -1)
                            {

                                grizzlePower = Math.Max(5, checkIndex);

                                grizzleConsume = Math.Min(checkItem.Stack, (int)(30/grizzlePower));

                                staminaUp = grizzleConsume * grizzlePower;

                                Game1.player.Stamina = Math.Min(Game1.player.MaxStamina, Game1.player.Stamina + (float)staminaUp);

                                Game1.addHUDMessage(new HUDMessage(checkItem.DisplayName + " x" + grizzleConsume, 4));

                                Game1.player.Items[i].Stack -= grizzleConsume;

                                if (Game1.player.Items[i].Stack <= 0)
                                {
                                    Game1.player.Items[i] = null;

                                }

                                continue;

                            }
                        }

                        if (Config.consumeQuicksnack)
                        {
                            checkIndex = sashimiList.IndexOf(itemIndex);

                            if (checkIndex != -1 && !sashimiPower)
                            {

                                Game1.player.Stamina = Math.Min(Game1.player.MaxStamina, Game1.player.Stamina + @checkItem.staminaRecoveredOnConsumption());

                                Game1.player.health = Math.Min(Game1.player.maxHealth, Game1.player.health + @checkItem.healthRecoveredOnConsumption());

                                Game1.addHUDMessage(new HUDMessage(checkItem.DisplayName, 4));

                                Game1.player.Items[i].Stack -= 1;

                                if (Game1.player.Items[i].Stack <= 0)
                                {
                                    Game1.player.Items[i] = null;

                                }

                                sashimiPower = true;

                            }

                            if (coffeeList.ContainsKey(itemIndex))
                            {

                                if (Game1.buffsDisplay.drink == null)
                                {

                                    int coffeeConsume = 1;

                                    int getSpeed = coffeeList[itemIndex];

                                    if(getSpeed < 90000) { 
                                        
                                        coffeeConsume = Math.Min(5, Game1.player.Items[i].Stack);
                                        
                                        getSpeed *= coffeeConsume; 
                                    
                                    }

                                    Buff speedBuff = new("Druidic Roastmaster", getSpeed, checkItem.DisplayName, 9);

                                    speedBuff.buffAttributes[9] = 1;

                                    speedBuff.total = 1;

                                    speedBuff.which = 184653;

                                    Game1.buffsDisplay.tryToAddDrinkBuff(speedBuff);

                                    Game1.player.Stamina = Math.Min(Game1.player.MaxStamina, Game1.player.Stamina + 25);
                                    
                                    Game1.addHUDMessage(new HUDMessage(checkItem.DisplayName, 4));

                                    Game1.player.Items[i].Stack -= coffeeConsume;

                                }

                            }

                        }

                    }

                }

                if (Game1.player.Stamina <= staminaRequired)
                {

                    if (activeData.castLevel >= 1)
                    {
                        Game1.addHUDMessage(new HUDMessage("Not enough energy to continue rite", 3));

                    }
                    else
                    {
                        Game1.addHUDMessage(new HUDMessage("Not enough energy to perform rite", 3));

                    }
                    activeData.castInterrupt = true;
                    //activeData.castComplete = true;

                    return;

                }

            }

            Rite castRite = NewRite();

            castRite.castLevel = activeData.castLevel.DeepClone();

            castRite.direction = activeData.activeDirection.DeepClone();

            castRite.castType = activeData.activeBlessing.DeepClone();

            castRite.castCycle = activeData.cycleLevel.DeepClone();

            switch (activeData.activeBlessing)
            {

                case "stars":

                    ModUtility.AnimateStarsCast(activeData.activeVector, activeData.castLevel, activeData.cycleLevel);

                    riteQueue.Enqueue(castRite);

                    CastRite();

                    castBuffer.Enqueue(1);

                    DelayedAction.functionAfterDelay(ClearBuffer, 1333);

                    break;

                case "water":

                    castRite.castVector = activeData.activeVector.DeepClone();

                    ModUtility.AnimateWaterCast(activeData.activeVector, activeData.castLevel, activeData.cycleLevel);

                    riteQueue.Enqueue(castRite);

                    DelayedAction.functionAfterDelay(CastRite, 666);

                    break;

                default: //"earth"
                        
                    ModUtility.AnimateEarthCast(activeData.activeVector,activeData.castLevel,activeData.cycleLevel);

                    riteQueue.Enqueue(castRite);

                    DelayedAction.functionAfterDelay(CastRite, 666);

                    break;

            }


        }

        private bool ResetCast()
        {

            if (activeData.activeBlessing == "none")
            {

                if (castBuffer.Count == 0)
                {

                    if (!EventHandler(Game1.player.getTileLocation()))
                    {

                        Game1.player.currentLocation.playSound("ghost");

                        castBuffer.Enqueue(1);

                        DelayedAction.functionAfterDelay(ClearBuffer, 1333);

                        Game1.addHUDMessage(new HUDMessage("Nothing happened... I should consult the Effigy in the Farmcave.", 2));
                    }
                    
                }

                activeData.castInterrupt = true;

                return false;

            }

            /*if(Game1.player.CurrentTool is not MeleeWeapon)
            {

                castBuffer.Enqueue(1);

                DelayedAction.functionAfterDelay(ClearBuffer, 1333);

                activeData.castInterrupt = true;

                Game1.addHUDMessage(new HUDMessage("Rite requires a Melee Weapon or Scythe to activate", 2));

                return false;


            }*/

            string activeBlessing = staticData.activeBlessing;

            int toolIndex = Game1.player.CurrentTool.InitialParentTileIndex;

            if (weaponAttunement.ContainsKey(toolIndex))
            {
                activeBlessing = weaponAttunement[toolIndex];

                if (!staticData.blessingList.ContainsKey(activeBlessing))
                {
                    
                    Game1.addHUDMessage(new HUDMessage("I'm not attuned to this weapon... perhaps the Effigy can help", 2));

                    return false;

                }

            }

            Dictionary<string, bool> spawnIndex = Map.SpawnData.SpawnIndex(Game1.player.currentLocation);

            // ignore if game location has no spawn profile
            if (spawnIndex.Count == 0)
            {

                if (castBuffer.Count == 0)
                {
                    castBuffer.Enqueue(1);

                    DelayedAction.functionAfterDelay(ClearBuffer, 1333);

                    Game1.addHUDMessage(new HUDMessage("Unable to reach the otherworldly plane from this location", 2));

                }

                activeData.castInterrupt = true;

                return false;

            }

            if (activeLockout == "trigger" && activeBlessing == "earth")
            {
                if (castBuffer.Count == 0)
                {
                    castBuffer.Enqueue(1);

                    DelayedAction.functionAfterDelay(ClearBuffer, 1333);

                    Game1.addHUDMessage(new HUDMessage("Something is interfering with the rite!", 2));

                }

                return false;

            }

            activeData = new ActiveData
            {
                
                activeBlessing = activeBlessing,

                toolIndex = toolIndex,

                spawnIndex = spawnIndex,
    
            };

            return true;

        }

        private bool CasterBusy()
        {
            if (Game1.eventUp)
            {
                return true;
            }

            if (Game1.fadeToBlack)
            {
                return true;
            }

            if (Game1.currentMinigame != null)
            {
                return true;
            }

            if (Game1.activeClickableMenu != null)
            {
                return true;
            }

            if (Game1.isWarping)
            {
                return true;
            }

            if (Game1.killScreen)
            {
                return true;
            }

            if (Game1.player.freezePause > 0)
            {
                return true;
            }

            return false;

        }

        public bool CasterTool() {

            // ignore if current tool isn't set
            if (Game1.player.CurrentTool is null)
            {

                if (Config.riteButtons.GetState() == SButtonState.Pressed)
                { 
                    Game1.addHUDMessage(new HUDMessage("Requires a melee weapon or scythe to activate rite", 2));

                }

                return true;

            }

            // ignore if current tool isn't weapon
            if (Game1.player.CurrentTool.GetType().Name != "MeleeWeapon")
            {

                if (Config.riteButtons.GetState() == SButtonState.Pressed)
                {
                    Game1.addHUDMessage(new HUDMessage("Requires a melee weapon or scythe to activate rite", 2));

                }

                return true;

            }

            return false;

        }

        private bool EventHandler(Vector2 targetVector)
        {

            if (activeLockout != "none"){ 
                
                return false; 
            
            }

            // check if player has activated a triggered event
            Vector2 playerVector = Game1.player.getTileLocation();

            List<Map.Quest> triggeredQuests = new();

            GameLocation playerLocation = Game1.player.currentLocation;

            string locationName = Game1.player.currentLocation.Name;

            Type locationType = playerLocation.GetType();

            foreach (string castString in triggerList)
            {

                Map.Quest questData = questIndex[castString];
                //Monitor.Log($"{castString}", LogLevel.Debug);
                if (questData.useTarget)
                {

                    playerVector = targetVector;

                }

                if (questData.triggerLocation != null)
                {
                    if (!questData.triggerLocation.Contains(locationName))
                    {
                        continue;
                    }
                }

                if (questData.triggerLocale != null)
                {
                    if (!questData.triggerLocale.Contains(locationType))
                    {
                        continue;
                    }
                }

                if (questData.startTime != 0)
                {
                    if (Game1.timeOfDay < questData.startTime)
                    {   
                        continue;
                    }
                }

                if (questData.triggerBlessing != null)
                {
                    if(activeData.activeBlessing != questData.triggerBlessing)
                    {
                        continue;
                    }
                }

                bool runTrigger = false;

                if (questData.triggerSpecial && !questData.vectorList.ContainsKey("triggerVector"))
                {

                    Vector2 specialTrigger = Map.QuestData.SpecialVector(playerLocation, questData.name);
                    
                    if (specialTrigger != new Vector2(-1))
                    {

                        questData.vectorList["specialVector"] = specialTrigger;

                        questData.vectorList["triggerVector"] = specialTrigger;

                        if (questData.vectorList.ContainsKey("specialOffset"))
                        {
                            questData.vectorList["triggerVector"] += questData.vectorList["specialOffset"];
                        }

                        questIndex[castString].triggerSpecial = false;

                        questIndex[castString].vectorList["triggerVector"] = questData.vectorList["triggerVector"];
                    
                    }
                
                }

                if (questData.vectorList.ContainsKey("triggerVector"))
                {
                    //Monitor.Log($"{questData.vectorList["triggerVector"]}", LogLevel.Debug);
                    Vector2 TriggerVector = questData.vectorList["triggerVector"];

                    Vector2 TriggerLimit = TriggerVector + questData.vectorList["triggerLimit"];

                    if (
                        playerVector.X >= TriggerVector.X &&
                        playerVector.Y >= TriggerVector.Y &&
                        playerVector.X < TriggerLimit.X &&
                        playerVector.Y < TriggerLimit.Y
                        )
                    {
                        runTrigger = true;
                    }

                }

                if(questData.triggerTile != 0 || questData.triggerAction != null)
                {

                    Layer buildingLayer = playerLocation.Map.GetLayer("Buildings");

                    for (int i = 0; i < questData.triggerRadius; i++)
                    {

                        List<Vector2> tileVectors = ModUtility.GetTilesWithinRadius(playerLocation, playerVector, i);

                        foreach (Vector2 tileVector in tileVectors)
                        {

                            Tile buildingTile = buildingLayer.PickTile(new Location((int)tileVector.X * 64, (int)tileVector.Y * 64), Game1.viewport.Size);

                            if (buildingTile != null)
                            {

                                if (questData.triggerTile != 0)
                                {

                                    int tileIndex = buildingTile.TileIndex;

                                    if ((uint)(tileIndex - questData.triggerTile) <= 1u)
                                    {

                                        runTrigger = true; //1140

                                        questData.vectorList["tileVector"] = tileVector;

                                    }

                                }
                                else
                                {

                                    buildingTile.Properties.TryGetValue("Action", out var value3);
                                    
                                    if (value3 != null)
                                    {
                                        if(value3.ToString() == questData.triggerAction)
                                        {

                                            questData.vectorList["actionVector"] = tileVector;

                                            runTrigger = true;
                                        
                                        }

                                    }

                                }

                            }

                            if (runTrigger)
                            {
                                break;
                                //questData.vectorList["triggerVector"] = tileVector;

                                //questData.vectorList["triggerLimit"] = new(1, 1);

                            }

                        }

                        if (runTrigger)
                        {
                            break;

                        }

                    }

                }

                if(runTrigger)
                {

                    triggeredQuests.Add(questData);

                }

            }

            Event.EventHandle eventHandle = new(this);

            Cast.Rite rite = NewRite();

            rite.direction = Game1.player.getFacingDirection();

            foreach (Map.Quest questData in triggeredQuests)
            {
                //Monitor.Log($"{questData.vectorList["triggerVector"]}",LogLevel.Debug);
                triggerList.Remove(questData.name);

                eventHandle.RunEvent(playerVector, questData, rite);

                activeLockout = "trigger";

                return true;

            }

            return false;

        }

        private Rite NewRite()
        {

            Axe castAxe = RetrieveAxe();

            Pickaxe castPick = RetrievePick();

            int damageLevel = 150;

            if (!Config.maxDamage)
            {

                damageLevel = 5 * Game1.player.CombatLevel;

                damageLevel += 1 * Game1.player.MiningLevel;

                damageLevel += 1 * Game1.player.ForagingLevel;

                damageLevel += 5 * castAxe.UpgradeLevel;

                damageLevel += 5 * castPick.UpgradeLevel;

                if (Game1.player.CurrentTool is Tool currentTool)
                {
                    if (currentTool.enchantments.Count > 0)
                    {
                        damageLevel += 25;
                    }
                }

            }

            Rite newRite = new()
            {

                //castBlessing = staticData.blessingList.DeepClone(),

                castTask = staticData.taskList.DeepClone(),

                castToggle = staticData.toggleList.DeepClone(),

                castAxe = castAxe,

                castPick = castPick,

                castDamage = damageLevel,

            };

            return newRite;

        }

        private void CastRite()
        { 
            
            if(riteQueue.Count != 0)
            {

                Rite rite = riteQueue.Dequeue();

                Vector2 castPosition = rite.castVector * 64;

                float castLimit = (rite.castLevel * 128) + 32f;

                if (rite.castLocation.characters.Count > 0)
                {

                    if (rite.castType == "stars")
                    {
                        castLimit = 1000;

                    }

                    foreach (NPC riteWitness in rite.castLocation.characters)
                    {
                        if (riteWitness is Monster)
                        {
                            continue;
                        }

                        if (Vector2.Distance(riteWitness.Position, castPosition) < castLimit)
                        {
                            if (riteWitness is Pet petPet)
                            {

                                petPet.checkAction(rite.caster, rite.castLocation);

                            }

                            if (riteWitness.isVillager() && riteWitness.Name != "Disembodied Voice")
                            {

                                if (rite.castType == "stars")
                                {

                                    riteWitness.doEmote(8);

                                    Game1.addHUDMessage(new HUDMessage($"The stars are afraid {riteWitness.Name} could get hurt", 3));

                                    return;

                                }
                                else if (rite.castType == "water")
                                {

                                    riteWitness.doEmote(16);

                                    Game1.addHUDMessage(new HUDMessage($"{riteWitness.Name} is disturbed by the storm cloud", 2));

                                }
                                else
                                {

                                    new Cast.GreetVillager(this, rite.castVector, rite, riteWitness);

                                }

                            }
                        }

                    }

                }

                if (rite.castLocation is Farm farmLocation)
                {

                    foreach (KeyValuePair<long, FarmAnimal> pair in farmLocation.animals.Pairs)
                    {

                        if (Vector2.Distance(pair.Value.Position, castPosition) >= castLimit)
                        {

                            continue;

                        }
                        new Cast.PetAnimal(this, rite.castVector, rite, pair.Value);

                    }

                }

                if (rite.castLocation is AnimalHouse animalLocation)
                {

                    foreach (KeyValuePair<long, FarmAnimal> pair in animalLocation.animals.Pairs)
                    {

                        if (Vector2.Distance(pair.Value.Position, castPosition) >= castLimit)
                        {

                            continue;

                        }

                        new Cast.PetAnimal(this, rite.castVector, rite, pair.Value);

                    }

                }

                // Add cast buff if enabled
                if (Config.castBuffs)
                {

                    Buff magnetBuff = new("Druidic Magnetism", 6000, "Rite of the " + rite.castDisplay, 8);

                    magnetBuff.buffAttributes[8] = 192;

                    magnetBuff.which = 184651;

                    if (!Game1.buffsDisplay.hasBuff(184651))
                    {

                        Game1.buffsDisplay.addOtherBuff(magnetBuff);

                    }

                    Vector2 casterVector = rite.caster.getTileLocation();

                    if (rite.castLocation.terrainFeatures.ContainsKey(casterVector))
                    {
                       
                        if (rite.castLocation.terrainFeatures[casterVector] is StardewValley.TerrainFeatures.Grass)
                        {
                            Buff speedBuff = new("Druidic Freneticism", 6000, "Rite of the " + rite.castType, 9);

                            speedBuff.buffAttributes[9] = 2;

                            speedBuff.which = 184652;

                            if (!Game1.buffsDisplay.hasBuff(184652))
                            {

                                Game1.buffsDisplay.addOtherBuff(speedBuff);

                            }

                        }

                    }

                }

                switch (rite.castType)
                {

                    case "stars":

                        CastStars(rite); break;

                    case "water":

                        CastWater(rite); break;

                    default: //CastEarth

                        CastEarth(rite);

                        break;
                }

            }
        
        }

        private void ClearBuffer()
        {

            castBuffer.Clear();

        }

        private void CastEarth(Rite riteData = null)
        {

            int castCost = 0;

            riteData ??= new();

            List<Vector2> removeVectors = new();

            List<Vector2> terrainVectors = new();

            Dictionary<Vector2, Cast.CastHandle> effectCasts = new();

            Layer backLayer = riteData.castLocation.Map.GetLayer("Back");

            Layer buildingLayer = riteData.castLocation.Map.GetLayer("Buildings");

            int blessingLevel = staticData.blessingList["earth"];

            List<Vector2> clumpIndex;

            int rockfallChance = 10 - Math.Max(5, riteData.castPick.UpgradeLevel);

            string locationName = riteData.castLocation.Name;

            if (riteData.castLocation is MineShaft)
            {

                if (locationName != mineShaftName && earthCasts.ContainsKey(locationName))
                {

                    earthCasts.Remove(locationName);

                    mineShaftName = locationName;

                }

            }

            if (!earthCasts.ContainsKey(locationName))
            {
                earthCasts[locationName] = new();
                
            };

            int castRange;

            if (riteData.castLocation.largeTerrainFeatures.Count > 0)
            {

                float castLimit = (riteData.castLevel * 2) + 0.5f;

                foreach (LargeTerrainFeature largeTerrainFeature in riteData.castLocation.largeTerrainFeatures)
                {

                    if(largeTerrainFeature is not StardewValley.TerrainFeatures.Bush bushFeature)
                    {

                        continue;

                    }

                    Vector2 terrainVector = bushFeature.tilePosition.Value;
                    
                    if (earthCasts[locationName].Contains(terrainVector)) // already served
                    {

                        continue;

                    }
                    
                    if (Vector2.Distance(terrainVector, riteData.castVector) < castLimit)
                    {
                        
                        if (blessingLevel >= 2)
                        {
                            effectCasts[terrainVector] = new Cast.Bush(this, terrainVector, riteData, bushFeature);

                            earthCasts[locationName].Add(terrainVector);
                        }
                        
                    }

                    if (!terrainVectors.Contains(terrainVector)) // already served
                    {

                        terrainVectors.Add(terrainVector);

                    }

                }

            }

            for (int i = 0; i < 2; i++)
            {

                castRange = (riteData.castLevel * 2) - 1 + i;

                List<Vector2> tileVectors = ModUtility.GetTilesWithinRadius(riteData.castLocation, riteData.castVector, castRange);

                foreach (Vector2 tileVector in tileVectors)
                {

                    if (earthCasts[locationName].Contains(tileVector) || removeVectors.Contains(tileVector) || terrainVectors.Contains(tileVector)) // already served
                    {

                        continue;

                    }
                    else
                    {

                        earthCasts[locationName].Add(tileVector);

                    }

                    int tileX = (int)tileVector.X;

                    int tileY = (int)tileVector.Y;

                    Tile buildingTile = buildingLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                    if (buildingTile != null)
                    {

                        //if (riteData.castLocation.Name.Contains("Beach"))
                        if (riteData.castLocation is Beach)
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
                                else if (riteData.castLocation is MineShaft && tileObject is BreakableContainer)
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

                                case "FruitTree":

                                    StardewValley.TerrainFeatures.FruitTree fruitFeature = terrainFeature as StardewValley.TerrainFeatures.FruitTree;

                                    if (fruitFeature.growthStage.Value >= 4)
                                    {

                                        effectCasts[tileVector] = new Cast.FruitTree(this, tileVector, riteData);

                                    }
                                    else if (staticData.blessingList["earth"] >= 4)
                                    {

                                        effectCasts[tileVector] = new Cast.FruitSapling(this, tileVector, riteData);

                                    }
                                    
                                    break;

                                case "Tree":

                                    StardewValley.TerrainFeatures.Tree treeFeature = terrainFeature as StardewValley.TerrainFeatures.Tree;

                                    if (treeFeature.growthStage.Value >= 5)
                                    {
                                        //Monitor.Log($"{riteData.castAxe.UpgradeLevel}", LogLevel.Debug);
                                        effectCasts[tileVector] = new Cast.Tree(this, tileVector, riteData);

                                    }
                                    else if(staticData.blessingList["earth"] >= 4 && treeFeature.fertilized.Value == false)
                                    {

                                        effectCasts[tileVector] = new Cast.Sapling(this, tileVector, riteData);

                                    }

                                    break;

                                case "Grass":

                                    effectCasts[tileVector] = new Cast.Grass(this, tileVector, riteData);

                                    break;

                                case "HoeDirt":

                                    if (staticData.blessingList["earth"] >= 4)
                                    {

                                        if (riteData.spawnIndex["cropseed"])
                                        {

                                            effectCasts[tileVector] = new Cast.Crop(this, tileVector, riteData);

                                        }

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

                                                effectCasts[tileVector] = new Cast.Stump(this, tileVector, riteData, resourceClump, "Farm");

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

                    if (riteData.castLocation is Woods)
                    {
                        if (blessingLevel >= 2)
                        {
                            Woods woodsLocation = riteData.castLocation as Woods;

                            foreach (ResourceClump resourceClump in woodsLocation.stumps)
                            {

                                if (resourceClump.tile.Value == tileVector)
                                {

                                    effectCasts[tileVector] = new Cast.Stump(this, tileVector, riteData, resourceClump, "Woods");

                                    Vector2 clumpVector = tileVector;

                                    removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y));

                                    removeVectors.Add(new Vector2(clumpVector.X, clumpVector.Y + 1));

                                    removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y + 1));

                                }

                            }

                        }

                    }

                    foreach (Furniture item in riteData.castLocation.furniture)
                    {

                        if (item.boundingBox.Value.Contains(tileX * 64, tileY * 64))
                        {

                            continue;

                        }

                    }

                    if (riteData.spawnIndex["rockfall"])
                    {

                        if (blessingLevel >= 5)
                        {
                            int probability = Game1.random.Next(rockfallChance);

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

                        if(riteData.castLocation is AnimalHouse)
                        {

                            if (backTile.TileIndexProperties.TryGetValue("Trough", out _))
                            {

                                effectCasts[tileVector] = new Cast.Trough(this, tileVector, riteData);

                                continue;

                            }

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

                foreach (Vector2 tileVector in tileVectors)
                {

                    ModUtility.AnimateCastRadius(riteData.castLocation, tileVector, new Color(0.8f, 1f, 0.8f, 1), i);

                }

            }

            //-------------------------- fire effects

            List<Type> castLimits = new();

            if (effectCasts.Count != 0)
            {
                foreach (KeyValuePair<Vector2, Cast.CastHandle> effectEntry in effectCasts)
                {

                    if (removeVectors.Contains(effectEntry.Key)) // ignore tiles covered by clumps
                    {

                        continue;

                    }

                    
                    Cast.CastHandle effectHandle = effectEntry.Value;

                    Type effectType = effectHandle.GetType();

                    if (castLimits.Contains(effectType))
                    {

                        continue;

                    }
                    
                    effectHandle.CastEarth();

                    if (effectHandle.castFire)
                    {

                        castCost += effectHandle.castCost;

                    }

                    if (effectHandle.castLimit)
                    {

                        castLimits.Add(effectEntry.Value.GetType());

                    }

                    if (effectHandle.castActive)
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

                Game1.player.Stamina -= staminaCost;

                Game1.player.checkForExhaustion(oldStamina);

            }

            return;

        }

        private void CastWater(Rite riteData = null)
        {

            //-------------------------- tile effects

            int castCost = 0;

            riteData ??= new();

            Vector2 castVector = riteData.castVector;

            List <Vector2> tileVectors;

            List<Vector2> removeVectors = new();

            Dictionary<Vector2, Cast.CastHandle> effectCasts = new();

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

                    if (blessingLevel >= 1)
                    {
                        if (warpPoints.ContainsKey(locationName))
                        {

                            if (warpPoints[locationName] == tileVector)
                            {

                                if (warpCasts.Contains(locationName))
                                {

                                    Game1.addHUDMessage(new HUDMessage($"Already extracted {locationName} warp power today", 3));

                                    continue;

                                }

                                int targetIndex = warpTotems[locationName];

                                effectCasts[tileVector] = new Cast.Totem(this, tileVector, riteData, targetIndex);

                                warpCasts.Add(locationName);

                                continue;

                            }

                        }

                    }

                    if (blessingLevel >= 1)
                    {
                        if (firePoints.ContainsKey(locationName))
                        {

                            if (firePoints[locationName] == tileVector)
                            {

                                if (fireCasts.Contains(locationName))
                                {

                                    Game1.addHUDMessage(new HUDMessage($"Already ignited {locationName} camp fire today", 3));

                                    continue;

                                }

                                effectCasts[tileVector] = new Cast.Campfire(this, tileVector, riteData);

                                fireCasts.Add(locationName);
                                
                                continue;

                            }

                        }

                    }

                    if (riteData.castLocation.objects.Count() > 0)
                    {

                        if (riteData.castLocation.objects.ContainsKey(tileVector))
                        {
                            
                            StardewValley.Object targetObject = riteData.castLocation.objects[tileVector];

                            if (riteData.castLocation.IsFarm && targetObject.bigCraftable.Value && targetObject.ParentSheetIndex == 9)
                            {

                                if (warpCasts.Contains("rod"))
                                {

                                    Game1.addHUDMessage(new HUDMessage("Already powered a lightning rod today", 3));

                                }
                                else if (blessingLevel >= 2)
                                {

                                    effectCasts[tileVector] = new Cast.Rod(this, tileVector, riteData);

                                    warpCasts.Add("rod");

                                }

                            }
                            else if (targetObject.Name.Contains("Campfire"))
                            {

                                string fireLocation = riteData.castLocation.Name;

                                if (fireCasts.Contains(fireLocation))
                                {

                                    //if (fireLocation != "Beach")
                                    //{
                                        Game1.addHUDMessage(new HUDMessage($"Already ignited {fireLocation} camp fire today", 3));

                                    //}

                                }
                                else if (blessingLevel >= 2)
                                {

                                    effectCasts[tileVector] = new Cast.Campfire(this, tileVector, riteData);

                                    fireCasts.Add(fireLocation);

                                }
                            }
                            else if (targetObject is Torch && targetObject.ParentSheetIndex == 93) // crafted candle torch
                            {
                                if (blessingLevel >= 5 && activeLockout != "trigger")
                                {
                                    if (riteData.spawnIndex["portal"])
                                    {

                                        effectCasts[tileVector] = new Cast.Portal(this, tileVector, riteData);

                                    }

                                }

                            }
                            else if (targetObject.IsScarecrow())
                            {
                                
                                string scid = "scarecrow_" + tileVector.X.ToString() + "_" + tileVector.Y.ToString();

                                if (blessingLevel >= 2 && !Game1.isRaining && !warpCasts.Contains(scid))
                                {
                                    
                                    effectCasts[tileVector] = new Cast.Scarecrow(this, tileVector, riteData);

                                    warpCasts.Add(scid);

                                }
                            
                            }

                            continue;

                        }

                    }

                    if (riteData.castLocation.terrainFeatures.ContainsKey(tileVector))
                    {

                        if (blessingLevel >= 1)
                        {

                            if(riteData.castLocation.terrainFeatures[tileVector] is StardewValley.TerrainFeatures.Tree treeFeature)
                            {
                                
                                if (treeFeature.stump.Value)
                                {

                                    effectCasts[tileVector] = new Cast.Tree(this, tileVector, riteData);

                                }

                            }

                        }

                        continue;

                    }

                    Tile buildingTile = buildingLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

                    if (buildingTile != null)
                    {

                        if (riteData.castLocation is Farm farmLocation)
                        {
                            int tileIndex = buildingTile.TileIndex;

                            if (tileIndex == 1938)
                            {
                                effectCasts[tileVector] = new Cast.PetBowl(this, tileVector, riteData);
                            }

                        }

                        //Monitor.Log($"{buildingTile.TileIndex}", LogLevel.Debug);
                        continue;

                    }
                   
                    if (riteData.castLocation.terrainFeatures.ContainsKey(tileVector))
                    {
                        
                        continue;

                    }

                    if(riteData.castLocation is Forest forestLocation) 
                    {

                        if (blessingLevel >= 1)
                        {

                            if (forestLocation.log != null && forestLocation.log.tile.Value == tileVector)
                            {

                                effectCasts[tileVector] = new Cast.Stump(this, tileVector, riteData, forestLocation.log, "Log");

                                Vector2 clumpVector = tileVector;

                                removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y));

                                removeVectors.Add(new Vector2(clumpVector.X, clumpVector.Y + 1));

                                removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y + 1));

                            }

                        }

                    }
                    
                    if(riteData.castLocation is Woods)
                    {
                        if (blessingLevel >= 1)
                        {
                            Woods woodsLocation = riteData.castLocation as Woods;

                            foreach (ResourceClump resourceClump in woodsLocation.stumps)
                            {

                                if (resourceClump.tile.Value == tileVector)
                                {

                                    effectCasts[tileVector] = new Cast.Stump(this, tileVector, riteData, resourceClump, "Woods");

                                    Vector2 clumpVector = tileVector;

                                    removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y));

                                    removeVectors.Add(new Vector2(clumpVector.X, clumpVector.Y + 1));

                                    removeVectors.Add(new Vector2(clumpVector.X + 1, clumpVector.Y + 1));

                                }

                            }

                        }

                    }

                    if (riteData.castLocation.resourceClumps.Count > 0)
                    {

                        bool targetClump = false;

                        foreach (ResourceClump resourceClump in riteData.castLocation.resourceClumps)
                        {

                            if (resourceClump.tile.Value == tileVector)
                            {

                                if (blessingLevel >= 1)
                                {

                                    switch (resourceClump.parentSheetIndex.Value)
                                    {

                                        case 600:
                                        case 602:

                                            effectCasts[tileVector] = new Cast.Stump(this, tileVector, riteData, resourceClump, "Farm");

                                            break;

                                        default:

                                            effectCasts[tileVector] = new Cast.Boulder(this, tileVector, riteData, resourceClump);

                                            break;

                                    }

                                    Vector2 clumpVector = tileVector;

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
                    /*
                    foreach (Furniture item in riteData.castLocation.furniture)
                    {

                        if (item.boundingBox.Value.Contains(tileX * 64, tileY * 64))
                        {

                            continue;

                        }

                    }
                    */
                }

                foreach (Vector2 tileVector in tileVectors) {

                    ModUtility.AnimateCastRadius(riteData.castLocation, tileVector, new Color(0.8f,0.8f,1f,1),i);

                }

            }

            if (blessingLevel >= 3 && riteData.castLevel == 1 && riteData.spawnIndex["fishspot"])
            {

                Dictionary<int, Vector2> portalOffsets = new()
                {

                    [0] = new Vector2(0, -1),// up
                    [1] = new Vector2(1, 0), // right
                    [2] = new Vector2(0, 1),// down
                    [3] = new Vector2(-1, 0), // left

                };

                Vector2 fishVector = castVector + portalOffsets[riteData.direction];

                if (ModUtility.WaterCheck(riteData.castLocation,fishVector))
                {

                    effectCasts[fishVector] = new Water(this, fishVector, riteData);

                }

            }

            if (blessingLevel >= 4)
            {

                /*Vector2 smiteVector = riteData.caster.getTileLocation();

                Microsoft.Xna.Framework.Rectangle castArea = new(
                    ((int)castVector.X - riteData.castLevel) * 64,
                    ((int)castVector.Y - riteData.castLevel) * 64,
                    (riteData.castLevel * 128) + 64,
                    (riteData.castLevel * 128) + 64
                );

                int riteAoe = (riteData.castLevel - 1);

                Microsoft.Xna.Framework.Rectangle smiteArea = new(
                    ((int)smiteVector.X - riteAoe) * 64,
                    ((int)smiteVector.Y - riteAoe) * 64,
                    ((riteData.castLevel-1) * 128) + 64,
                    (riteAoe * 128) + 64
                );*/

                int smiteCount = 0;

                //int smiteLimit = (int)(riteData.castLevel / 2);

                Vector2 castPosition = riteData.castVector * 64;

                float castLimit = (riteData.castLevel * 128) + 32f;

                float castThreshold = Math.Max(0, castLimit - 320f);

                foreach (NPC nonPlayableCharacter in riteData.castLocation.characters)
                {

                    if (nonPlayableCharacter is Monster monsterCharacter)
                    {

                        float monsterDifference = Vector2.Distance(monsterCharacter.Position, castPosition);

                        if (monsterDifference < castLimit && monsterDifference > castThreshold)
                        {

                            //if (monsterCharacter.Health > 0 && !monsterCharacter.IsInvisible && !monsterCharacter.isInvincible() && (monsterCharacter.GetBoundingBox().Intersects(castArea) || monsterCharacter.GetBoundingBox().Intersects(smiteArea)))
                            //if (monsterCharacter.Health > 0 && !monsterCharacter.IsInvisible && !monsterCharacter.isInvincible() && monsterCharacter.GetBoundingBox().Intersects(smiteArea))
                            //{
                            
                            Vector2 monsterVector = monsterCharacter.getTileLocation();

                            effectCasts[monsterVector] = new Cast.Smite(this, monsterVector, riteData, monsterCharacter);

                            smiteCount++;

                            break;

                        } 

                    }

                    //if (smiteCount == smiteLimit)
                    if (smiteCount == riteData.castLevel)
                    {
                       break;
                    }

                }

            }
            
            //-------------------------- fire effects

            List<Type> castLimits = new();

            if (effectCasts.Count != 0)
            {
                foreach (KeyValuePair<Vector2, Cast.CastHandle> effectEntry in effectCasts)
                {

                    if (removeVectors.Contains(effectEntry.Key)) // ignore tiles covered by clumps
                    {

                        continue;

                    }

                    Cast.CastHandle effectHandle = effectEntry.Value;

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

            Random randomIndex = new();

            Dictionary<Vector2, Cast.Meteor> effectCasts = new();

            int castAttempt = riteData.castLevel + 1;

            List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(riteData.castLocation, riteData.castVector, 2 + castAttempt);

            int castSelect = castSelection.Count;

            if(castSelect != 0)
            {
                
                int castIndex = 0;

                Vector2 newVector;

                int castSegment = castSelect / castAttempt;

                if(castSelect % castAttempt >= 2)
                {

                    castAttempt++;

                }

                int lastUpper = castSelect;

                /*if(castAttempt == 5)
                {

                    castAttempt = 4;

                }

                int castFrom = 0;
                */
                for (int k = 0; k < castAttempt; k++)
                {

                    int castLower = castSegment * k;

                    int castHigher = castLower + castSegment;

                    bool priorityCast = false;

                    if (riteData.castLocation.objects.Count() > 0 && riteData.castTask.ContainsKey("masterMeteor"))
                    {

                        //int lowerLimit = castSegment * k;

                        //int upperLimit = lowerLimit + castSegment;

                        //for (int j = lowerLimit; j < upperLimit; j++)
                        for (int j = castLower; j < Math.Min(castHigher, castSelection.Count); j++)
                        {

                            newVector = castSelection[j];

                            if (riteData.castLocation.objects.ContainsKey(newVector))
                            {

                                StardewValley.Object tileObject = riteData.castLocation.objects[newVector];

                                //Monitor.Log($"{tileObject.name}",LogLevel.Debug);
                                if (tileObject.name.Contains("Stone"))
                                {

                                    effectCasts[newVector] = new Cast.Meteor(this, newVector, riteData);

                                    priorityCast = true;

                                    break;

                                }

                            }

                        }

                        if (priorityCast)
                        {

                            continue;

                        }

                    }

                    if (!riteData.castTask.ContainsKey("masterMeteor"))
                    {

                        UpdateTask("lessonMeteor", 1);

                    }

                    //if (!priorityCast)
                    //{

                    //castIndex = randomIndex.Next(castSegment) + castFrom;
                    castIndex = randomIndex.Next(castLower, Math.Min(castHigher,castSelection.Count));

                        /*if(castSelection.Count <= castIndex)
                        {

                            castIndex = castSelection.Count - 1;

                        }*/

                        newVector = castSelection[castIndex];

                        effectCasts[newVector] = new Cast.Meteor(this, newVector, riteData);

                    //}

                    /*if(castFrom == castSelect)
                    {

                        break;

                    }*/
                    if(k == 0)
                    {

                        lastUpper = (castIndex + castSelect - 3);

                    }


                    if(castIndex >= lastUpper)
                    {

                        break;

                    }


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

                Game1.player.Stamina -= staminaCost;

                Game1.player.checkForExhaustion(oldStamina);

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

            activeData = new() { activeBlessing = blessing, castInterrupt = true, };

            if (!staticData.blessingList.ContainsKey(blessing))
            {

                staticData.blessingList[blessing] = 0;

            }

        }

        public void LevelBlessing(string blessing)
        {

            staticData.blessingList[blessing] += 1;

        }

        public void ActiveCast(Cast.CastHandle castHandle)
        {

            Type handleType = castHandle.GetType();

            if (!activeCasts.ContainsKey(handleType))
            {

                activeCasts[handleType] = new();

            }

            activeCasts[handleType].Add(castHandle);

        }

        /*public void QuestCheck()
        {

            List<string> saveQuests = new();

            List<string> alsoQuests = new();

            foreach (KeyValuePair<string,bool> questPair in staticData.questList)
            {

                Map.Quest questData = questIndex[questPair.Key];

                if (Game1.player.hasQuest(questData.questId))
                {
                    if (questPair.Value)
                    {

                        saveQuests.Add(questPair.Key);

                    } else
                    {

                        alsoQuests.Add(questPair.Key);

                    }

                }

            }

            foreach (string quest in saveQuests)
            {

                staticData.questList[quest] = true;

            }

            foreach (string quest in alsoQuests)
            {

                staticData.questList[quest] = false;

            }

        }*/

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

        public bool QuestGiven(string quest)
        {

            return staticData.questList.ContainsKey(quest);

        }

        /*public void UpdateQuest(string quest, bool update)
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

        }*/

        public void ReassignQuest(string quest)
        {

            if (!staticData.questList.ContainsKey(quest))
            {

                staticData.questList[quest] = false;

            }

            Map.Quest questData = questIndex[quest];

            if (questData.questId != 0)
            {
                bool addQuest = true;

                for (int num = Game1.player.questLog.Count - 1; num >= 0; num--)
                {
                    if (Game1.player.questLog[num].id.Value == questData.questId)
                    {

                        addQuest = false;

                        if (Game1.player.questLog[num].completed.Value)
                        { 
                            Game1.player.questLog.RemoveAt(num);

                            addQuest = true;
                        }
                    
                    }

                }

                if(addQuest)
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

            if (questData.triggerType != null)
            {

                if(!triggerList.Contains(quest))
                {
                    
                    triggerList.Add(quest);

                }

            }

            if (questData.taskCounter != 0)
            {

                if (!staticData.taskList.ContainsKey(quest))
                {

                    staticData.taskList[quest] = 0;

                }

            }

        }

        public void CompleteQuest(string quest)
        {

            Map.Quest questData = questIndex[quest];

            staticData.questList[quest] = true;

            if (questData.questId != 0)
            {

                Game1.player.completeQuest(questData.questId);

            }

            if (questData.triggerType != null)
            {

                if (triggerList.Contains(quest))
                {

                    triggerList.Remove(quest);

                }

            }

            if (questData.updateEffigy == true)
            {

                druidEffigy.questCompleted = quest;

            }

            if(questData.taskFinish != null)
            {

                staticData.taskList[questData.taskFinish] = 1;

            }

        }

        public int UpdateTask(string quest, int update)
        {

            Map.Quest questData = questIndex[quest];

            if (questData.taskCounter == 0)
            {
                return -1;
            }

            if (!staticData.questList.ContainsKey(quest))
            {
                ReassignQuest(quest);    
            }

            if(!staticData.taskList.ContainsKey(quest))
            {
                ReassignQuest(quest);
            }

            if (staticData.questList[quest])
            {
                return -1;
            }

            staticData.taskList[quest] += update;

            if(staticData.taskList[quest] >= questData.taskCounter)
            {
                CompleteQuest(quest);
            }

            return staticData.taskList[quest];

        }

        public void TaskSet(string task, int set)
        {

            staticData.taskList[task] = set;

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

            return Config.riteButtons.ToString();

        }

        public void MonsterTrack(GameLocation location, Monster monster)
        {
            if (!monsterSpawns.ContainsKey(location.Name))
            {

                monsterSpawns[location.Name] = new();

            }

            monsterSpawns[location.Name].Add(monster);
        
        }

        public void UnlockAll()
        {

            staticData.blessingList = new()
            {
                ["earth"] = 5,
                ["water"] = 5,
                ["stars"] = 1,
            };

            staticData.activeBlessing = "earth";

        }
        
        public bool LocationPoll(string entryKey)
        {

            string location = Game1.player.currentLocation.Name;

            if (locationPoll.ContainsKey(entryKey))
            {

               if(locationPoll[entryKey] == location)
               {

                    return true;

                }

            }

            locationPoll[entryKey] = location;

            return false;

        }

        public Axe RetrieveAxe()
        {
            
            if(!LocationPoll("Axe"))
            {

                UpgradeTool("Axe");

            }

            return targetAxe;
        
        
        }

        public Pickaxe RetrievePick()
        {

            if (!LocationPoll("Pickaxe"))
            {

                UpgradeTool("Pickaxe");

            }

            return targetPick;

        }

        public void UpgradeTool(string toolType) {

            int upgradeLevel = 0;

            string toolBlessing = "level" + toolType;

            Tool targetTool = (toolType == "Axe") ? targetAxe : targetPick;

            if (Config.maxDamage)
            {

                targetTool.UpgradeLevel = 5;

                return;

            }

            if (Config.masterStart)
            {

                targetTool.UpgradeLevel = Config.blessingList.ContainsKey(toolBlessing) ? Config.blessingList[toolBlessing].DeepClone() : 5;

                return;

            }

            if (staticData.blessingList.ContainsKey(toolBlessing))
            {

                upgradeLevel = staticData.blessingList[toolBlessing];

            }

            foreach (Item item in Game1.player.Items)
            {
                
                if (item != null && item is Tool && item.Name.Contains(toolType))
                {
                    Tool playerTool = (Tool)item;

                    if(playerTool.UpgradeLevel > upgradeLevel)
                    {
                        upgradeLevel = playerTool.UpgradeLevel;

                    }
                
                }
            
            }

            int levelCheck = (toolType == "Axe") ? Game1.player.ForagingLevel : Game1.player.MiningLevel;

            if (levelCheck >= 7 && upgradeLevel <= 1)
            {
                
                upgradeLevel = 2;

            }
            else if (levelCheck >= 4 && upgradeLevel == 0)
            {
               
                upgradeLevel = 1;

            }

            staticData.blessingList[toolBlessing] = upgradeLevel;

            targetTool.UpgradeLevel = upgradeLevel;

        }
        
        public void ToggleEffect(string effect)
        {

            if (staticData.toggleList.ContainsKey(effect))
            {
                
                staticData.toggleList.Remove(effect);

            }
            else
            {

                staticData.toggleList[effect] = 1;

            }

        }

        public NPC RetrieveVoice(GameLocation location, Vector2 position)
        {

            if (disembodiedVoice != null)
            {

                GameLocation previous = disembodiedVoice.currentLocation;

                if (previous != location && disembodiedVoice.position != position)
                {
                    previous.characters.Remove(disembodiedVoice);

                    disembodiedVoice = null;

                }

            }

            if (disembodiedVoice == null)
            {

                disembodiedVoice = new StardewValley.NPC(new AnimatedSprite("Characters\\Junimo", 0, 16, 16), position, 2,"Disembodied Voice");

                disembodiedVoice.IsInvisible = true;

                disembodiedVoice.eventActor = true;

                disembodiedVoice.forceUpdateTimer = 9999;

                disembodiedVoice.collidesWithOtherCharacters.Value = true;

                disembodiedVoice.farmerPassesThrough = true;

                location.characters.Add(disembodiedVoice);

                disembodiedVoice.update(Game1.currentGameTime, location);

            }

            return disembodiedVoice;

        }

        public void TrainFarmhands()
        {

            foreach (Farmer farmer in Game1.getOnlineFarmers())
            {

                if(Helper.Multiplayer.GetConnectedPlayer(farmer.UniqueMultiplayerID) != null)
                {

                    StaticData farmhandData = staticData.DeepClone();

                    farmhandData.staticId = farmer.UniqueMultiplayerID;

                    Helper.Multiplayer.SendMessage(farmhandData, "FarmhandTrain", modIDs: new[] { this.ModManifest.UniqueID });

                }

            }

        }

        public bool AttuneableWeapon()
        {
            if(staticData.activeBlessing == "none")
            {
                //Monitor.Log($"{staticData.activeBlessing} blessing", LogLevel.Debug);
                return false; 
            }

            if (Game1.player.CurrentTool is not MeleeWeapon)
            {
                //Monitor.Log($"{Game1.player.CurrentTool.Name} not weapon", LogLevel.Debug);
                return false;
            }

            int toolIndex = Game1.player.CurrentTool.InitialParentTileIndex;

            if (Config.weaponAttunement.ContainsKey(toolIndex))
            {
                //Monitor.Log($"{Game1.player.CurrentTool.InitialParentTileIndex} reserved", LogLevel.Debug);
                return false;
            }

            if(weaponAttunement.ContainsKey(toolIndex))
            {

                if(weaponAttunement[toolIndex] == staticData.activeBlessing)
                {

                    return false;

                }

            }

            return true;

        }

        public bool AttuneWeapon()
        {

            if (!AttuneableWeapon()) { return false; };

            string activeBlessing = staticData.activeBlessing;

            int toolIndex = Game1.player.CurrentTool.InitialParentTileIndex;

            staticData.weaponAttunement[toolIndex] = activeBlessing;

            weaponAttunement[toolIndex] = activeBlessing;

            activeData.castInterrupt = true;

            return true;

        }

    }

}
