using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Ether;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewDruid.Event.Access;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Companions;
using StardewValley.GameData.Locations;
using StardewValley.GameData.Pets;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.Projectiles;
using StardewValley.Quests;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StardewDruid
{

    public class Mod : StardewModdingAPI.Mod
    {

        public bool magic;

        public ModData Config;

        public bool modPause = true;

        public bool modReady = false;

        public bool synchroniseProgress;

        public bool synchronisePreferences;

        public bool receivedSave;

        internal static Mod instance;

        public Rite rite;

        public IconData iconData;

        public QuestHandle questHandle;

        public ApothecaryHandle apothecaryHandle;

        public RelicHandle relicHandle;

        public ExportHandle exportHandle;

        public MasteryHandle masteryHandle;

        internal StardewDruid.Data.SaveData save;

        internal Dictionary<long, SaveData> multiplayer = new();

        public Dictionary<string, Event.EventHandle> eventRegister = new();

        public Dictionary<string, bool> triggerRegister = new();

        public List<Event.EventDisplay> displayRegister = new();

        public List<Event.EventBar> barRegister = new();

        public int displayOffset = 0;

        public int barOffset = 0;

        public Dictionary<string, string> activeEvent = new();

        public Dictionary<EventHandle.actionButtons,List<string>> clickRegister = new();

        public SButton buttonPress;

        public List<SpellHandle> spellRegister = new();

        public List<ThrowHandle> throwRegister = new();

        public List<BattleHandle> battleRegister = new();

        public Dictionary<string, List<ReactionData.reactions>> reactions = new();

        public Dictionary<ReactionData.reactions, int> reactionLimits = new();

        public double messageBuffer;

        public double consumeBuffer;

        public StardewValley.Tools.Pickaxe virtualPick;

        public StardewValley.Tools.Axe virtualAxe;

        public StardewValley.Tools.Hoe virtualHoe;

        public StardewValley.Tools.WateringCan virtualCan;

        public StardewValley.Tools.MilkPail virtualPail;

        public StardewValley.Tools.Shears virtualShears;

        public int currentTool;

        public Dictionary<CharacterHandle.characters, StardewDruid.Character.Character> characters = new();

        public Dictionary<CharacterHandle.characters, StardewDruid.Character.Character> dopplegangers = new();

        public Dictionary<CharacterHandle.characters, TrackHandle> trackers = new();

        public Dictionary<CharacterHandle.characters, CharacterMover> movers = new();

        public Dictionary<CharacterHandle.characters, StardewDruid.Dialogue.Dialogue> dialogue = new();

        public Dictionary<ChestHandle.chests, StardewValley.Objects.Chest> chests = new();

        public Dictionary<string, GameLocation> locations = new();

        public string mapped;

        public Dictionary<Vector2,string> features = new();

        public Random randomIndex = new();

        public List<SButton> suppress = new();

        public SoundHandle sounds;

        public int environmentCount;

        public EnvironmentHandle environment;

        public int DamageLevel;

        public double DamagePing;

        public int PowerLevel
        {
            get
            {

                if (magic)
                {

                    if (Game1.stats.DaysPlayed > 28)
                    {

                        return 5;
                    
                    }
                    else if (Game1.stats.DaysPlayed > 21)
                    {
                        
                        return 4;

                    }
                    else if (Game1.stats.DaysPlayed > 14)
                    {

                        return 3;

                    }
                    else if (Game1.stats.DaysPlayed > 7)
                    {

                        return 2;
                    
                    }

                    return 1;

                }

                if(save.milestone > QuestHandle.milestones.fates_challenge)
                {
                    return 5;
                }
                if (save.milestone > QuestHandle.milestones.stars_threats)
                {
                    return 4;
                }
                if (save.milestone > QuestHandle.milestones.mists_challenge)
                {
                    return 3;
                }
                if (save.milestone > QuestHandle.milestones.weald_challenge)
                {
                    return 2;
                }

                return 1;

            }

        }

        // ==================================================== SAVE / LOAD

        override public void Entry(IModHelper helper)
        {

            instance = this;

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;

            helper.Events.GameLoop.SaveLoaded += SaveLoaded;

            helper.Events.Multiplayer.PeerConnected += PeerConnected;

            helper.Events.Input.ButtonPressed += OnButtonPressed;

            helper.Events.GameLoop.UpdateTicked += EveryTicked;

            helper.Events.GameLoop.Saving += SaveShutdown;

            helper.Events.GameLoop.Saving += SaveState;

            helper.Events.GameLoop.Saved += SaveUpdated;

            helper.Events.Multiplayer.ModMessageReceived += OnModMessageReceived;

            helper.Events.Content.AssetRequested += OnAssetRequested;

            helper.Events.Display.RenderingStep += OnRenderingStep;

            helper.Events.Display.RenderedStep += OnRenderedStep;

        }

        private void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {

            if (e.Name.StartsWith("StardewDruid.Tilesheets."))
            {

                string tilesheetId = e.Name.ToString().Split(".")[2];//e.Name.ToString().Replace("StardewDruid.Tilesheets.", "");

                IconData.tilesheets tilesheet = Enum.Parse<IconData.tilesheets>(tilesheetId);

                e.LoadFrom(
                    () => {
                        return IconData.GetTilesheet(tilesheet);
                    },
                    AssetLoadPriority.Medium
                );
            }

            if (e.Name.StartsWith("StardewDruid.Characters."))
            {

                string characterId = e.Name.ToString().Split(".")[2];//e.Name.ToString().Replace("StardewDruid.Characters.", "");

                CharacterHandle.characters character = Enum.Parse<CharacterHandle.characters>(characterId);

                e.LoadFrom(
                    () => {
                        return CharacterHandle.CharacterTexture(character);
                    },
                    AssetLoadPriority.Medium
                );

            }

        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {

            Config = Helper.ReadConfig<ModData>();

            ModConfig.MenuConfig(this);

            QuicksaveConfig.SaveConfig(this);

            iconData = new IconData();

            sounds = new();

            environment = new();

        }

        private void SaveLoaded(object sender, SaveLoadedEventArgs e)
        {

            LoadState();

        }

        private void PeerConnected(object sender, PeerConnectedEventArgs e)
        {

            if (Context.IsMainPlayer)
            {

                return;

            }

            LoadState();

        }

        public void LoadState() {

            switch (Config.modVersion)
            {
                
                default:
                case "Default":

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Neosinf.Magic"))
                    {

                        magic = true;

                    }

                    break;

                case "Druid":

                    break;

                case "Magic":

                    magic = true;

                    break;

            }

            rite = new();

            eventRegister = new();

            activeEvent = new();

            clickRegister = new();

            spellRegister = new();

            throwRegister = new();

            characters = new();

            dialogue = new();

            chests = new();

            trackers = new();

            reactions = new();

            reactionLimits = new();

            displayRegister = new();

            barRegister = new();

            locations = new();

            questHandle = new QuestHandle();

            questHandle.LoadQuests();

            apothecaryHandle = new ApothecaryHandle();

            apothecaryHandle.LoadItems();

            relicHandle = new RelicHandle();

            relicHandle.LoadRelics();

            exportHandle = new ExportHandle();

            exportHandle.LoadExports();

            masteryHandle = new MasteryHandle();

            masteryHandle.LoadMasteries();

            virtualPick = new Pickaxe();

            virtualPick.lastUser = Game1.player;

            virtualPick.UpgradeLevel = 5;

            virtualAxe = new Axe();

            virtualAxe.lastUser = Game1.player;

            virtualAxe.UpgradeLevel = 5;

            virtualHoe = new Hoe();

            virtualHoe.lastUser = Game1.player;

            virtualHoe.UpgradeLevel = 5;

            virtualCan = new WateringCan();

            virtualCan.lastUser = Game1.player;

            virtualCan.UpgradeLevel = 5;

            virtualPail = new MilkPail();

            virtualPail.lastUser = Game1.player;

            virtualShears = new Shears();

            virtualShears.lastUser = Game1.player;

            if (!Context.IsMainPlayer)
            {

                receivedSave = false;

                synchronisePreferences = true;

                save = new();

                multiplayer = new();

                Helper.Multiplayer.SendMessage(new QueryData(), QueryData.queries.FarmhandRequestsData.ToString(), modIDs: new[] { this.ModManifest.UniqueID });

                return;

            }

            save = Helper.Data.ReadSaveData<StardewDruid.Data.SaveData>(SaveData.version);

            multiplayer = new();

            save ??= new();

            if (magic)
            {

                // only need save data for potions
                rite.Reset();

                questHandle.Ready();

                return;

            }

            if (Config.setMilestone != 0)
            {

                save = new()
                {
                    
                    milestone = (QuestHandle.milestones)Config.setMilestone
                
                };

                if (save.milestone != QuestHandle.milestones.none)
                {

                    questHandle.Promote(save.milestone);

                }

                save.rite = rite.RequirementCheck(rite.requirement.Last().Key,true);

            }

            if (save.milestone == QuestHandle.milestones.none)
            {

                questHandle.Promote((QuestHandle.milestones)1);

            }

            Helper.Data.WriteJsonFile("debug.json", save);

            rite.Reset();

            questHandle.Ready();

            DeserialiseLocations();

            exportHandle.Produce();

        }

        public void SaveConfig()
        {

            Helper.Data.WriteJsonFile("config.json", Config);

        }

        private void SaveShutdown(object sender, SavingEventArgs e)
        {

            trackers.Clear();

            rite.Shutdown();

            dialogue.Clear();

            reactions.Clear();

            reactionLimits.Clear();

            Game1.currentSpeaker = null;

            Game1.objectDialoguePortraitPerson = null;

            RemoveEvents();

            exportHandle.Orders();

        }

        private void SaveState(object sender, SavingEventArgs e)
        {
            if (!Context.IsMainPlayer)
            {

                return;

            }

            if (!magic)
            {

                SerialiseLocations();

                SaveCharacters();

            }

            RemoveCharacters();

            if (!magic)
            {

                RemoveLocations();

            }

            if (Config.setMilestone != 0)
            {

                Config.setMilestone = 0;

                SaveConfig();

            }

            if (multiplayer.Count > 0)
            {

                foreach (KeyValuePair<long, SaveData> farmhandData in multiplayer)
                {

                    Helper.Data.WriteSaveData(SaveData.version+"_"+farmhandData.Key, farmhandData.Value);

                }

            }

            Helper.Data.WriteSaveData(SaveData.version, save);

        }

        public void RemoveEvents()
        {

            foreach (KeyValuePair<string, Event.EventHandle> eventEntry in eventRegister)
            {

                eventEntry.Value.EventRemove();

            }

            eventRegister.Clear();

        }

        public void SaveCharacters()
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            for (int c = characters.Count - 1; c >= 0; c-- )
            {

                KeyValuePair<CharacterHandle.characters, StardewDruid.Character.Character> character = characters.ElementAt(c);

                if (character.Value == null)
                {

                    continue;

                }

                if (!CharacterHandle.CharacterSave(character.Key))
                {

                    characters.Remove(character.Key);

                    continue;

                }

                switch (character.Value.modeActive)
                {

                    case Character.Character.mode.scene:
                    case Character.Character.mode.random:
                    case Character.Character.mode.limbo:

                        save.companions[character.Key.ToString()].mode = Character.Character.mode.home;

                        break;

                    case Character.Character.mode.track:
                    case Character.Character.mode.recruit:

                        characters[character.Key].currentLocation.characters.Remove(characters[character.Key]);

                        CharacterHandle.locations home = CharacterHandle.CharacterHome(character.Key);

                        characters[character.Key].currentLocation = Game1.getLocationFromName(CharacterHandle.CharacterLocation(home));

                        characters[character.Key].Position = CharacterHandle.CharacterStart(home, character.Key);

                        save.companions[character.Key.ToString()].mode = character.Value.modeActive;

                        break;

                    default:

                        save.companions[character.Key.ToString()].mode = character.Value.modeActive;

                        break;

                }

            }

            foreach (KeyValuePair<ChestHandle.chests, StardewValley.Objects.Chest> chest in chests)
            {
                    
                if (chest.Value == null)
                {

                    continue;

                }

                for (int i = chest.Value.Items.Count - 1; i >= 0; i--)
                {

                    Item item = chest.Value.Items.ElementAt(i);

                    if (item == null)
                    {

                        continue;

                    }

                    switch (item)
                    {
                        case MeleeWeapon:

                            if((item as MeleeWeapon).isScythe())
                            {

                                if (!Game1.player.addItemToInventoryBool(item))
                                {

                                    Game1.player.dropItem(item);

                                }

                                chest.Value.Items.RemoveAt(i);
                            }

                            break;

                        case Pickaxe:
                        case Axe:
                        case Hoe:
                        case WateringCan:
                        case FishingRod:
                        case Pan:

                            if (!Game1.player.addItemToInventoryBool(item))
                            {

                                Game1.player.dropItem(item);

                            }

                            chest.Value.Items.RemoveAt(i);

                            break;
                    }


                }

                XmlSerializer serializer = new(typeof(Chest));

                MemoryStream memStream;
                
                memStream = new MemoryStream();
                
                XmlTextWriter xmlWriter;
                
                xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8)
                {
                
                    Namespaces = true
                
                };
                
                serializer.Serialize(xmlWriter, chest.Value);
                
                xmlWriter.Close();
                
                memStream.Close();
                
                string xml;
                
                xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                
                xml = xml[xml.IndexOf(Convert.ToChar(60))..];
                
                xml = xml[..(xml.LastIndexOf(Convert.ToChar(62)) + 1)];

                save.chests[chest.Key] = xml;

            }

            chests.Clear();

        }

        public static void RemoveCharacters()
        {

            foreach (GameLocation location in (IEnumerable<GameLocation>)Game1.locations)
            {

                if (location.characters.Count > 0)
                {

                    for (int index = location.characters.Count - 1; index >= 0; index--)
                    {

                        NPC npc = location.characters.ElementAt(index);

                        if (npc == null)
                        {

                            continue;

                        }

                        if (npc is StardewDruid.Character.Character)
                        {

                            location.characters.RemoveAt(index);

                        }

                        if (npc is Cast.Ether.Dragon || npc is StardewDruid.Monster.Boss)
                        {

                            location.characters.RemoveAt(index);

                        }
                    }

                }

            }

        }

        public void RemoveLocations()
        {

            foreach (KeyValuePair<string, GameLocation> location in locations)
            {

                Game1.locations.Remove(location.Value);

                Game1.removeLocationFromLocationLookup(location.Value);

            }

            List<GameLocation> removals = new();

            foreach (GameLocation location in (IEnumerable<GameLocation>)Game1.locations)
            {

                if(location is StardewDruid.Location.DruidLocation)
                {

                    removals.Add(location);

                }

            }

            foreach(GameLocation location in removals)
            {

                Game1.locations.Remove(location);

                Game1.removeLocationFromLocationLookup(location);

            }

        }

        public void ReinstateLocations()
        {

            foreach (KeyValuePair<string, GameLocation> location in locations)
            {

                Game1.locations.Add(location.Value);

                location.Value.updateWarps();

            }

        }

        public void ReinstateCharacters()
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            foreach (KeyValuePair<CharacterHandle.characters, StardewDruid.Character.Character> character in characters)
            {

                if (character.Value.modeActive == Character.Character.mode.limbo || character.Value.currentLocation == null)
                {

                    continue;

                }

                character.Value.NewDay();

                character.Value.currentLocation.characters.Add(character.Value);

                Character.Character.mode mode =
                save.companions.ContainsKey(character.Key.ToString()) ?
                save.companions[character.Key.ToString()].mode :
                Character.Character.mode.home;

                character.Value.SwitchToMode(mode, Game1.player);

            }

        }

        public void SerialiseLocations()
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            if (!Config.decorateLocations)
            {

                return;

            }

            GameLocation proxy = new Shed();

            int overnightMinutesElapsed = Utility.CalculateMinutesUntilMorning(Game1.timeOfDay);

            foreach(KeyValuePair<string,GameLocation> location in locations)
            {

                locations[location.Key].passTimeForObjects(overnightMinutesElapsed);

                proxy.furniture.Set(locations[location.Key].furniture);

                proxy.netObjects.Set(locations[location.Key].netObjects.Pairs);

                XmlSerializer serializer = new(typeof(GameLocation));

                MemoryStream memStream;

                memStream = new MemoryStream();

                XmlTextWriter xmlWriter;

                xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8)
                {
                    Namespaces = true
                };

                serializer.Serialize(xmlWriter, proxy);

                xmlWriter.Close();

                memStream.Close();

                string xml;

                xml = Encoding.UTF8.GetString(memStream.GetBuffer());

                xml = xml[xml.IndexOf(Convert.ToChar(60))..];

                xml = xml[..(xml.LastIndexOf(Convert.ToChar(62)) + 1)];

                save.locations[location.Key] = xml;

            }

        }

        public void DeserialiseLocations()
        {
            
            if (!Context.IsMainPlayer)
            {

                return;

            }

            if (!Config.decorateLocations)
            {

                return;

            }

            foreach(KeyValuePair<string,string> locationData in save.locations)
            {

                if (!locations.ContainsKey(locationData.Key))
                {

                    continue;

                }

                XmlSerializer serializer = new(typeof(GameLocation));

                StringReader stringReader;

                stringReader = new StringReader(locationData.Value);

                XmlTextReader xmlReader;

                xmlReader = new XmlTextReader(stringReader);

                GameLocation proxyGrove = (GameLocation)serializer.Deserialize(xmlReader);

                xmlReader.Close();

                stringReader.Close();

                locations[locationData.Key].furniture.Set(proxyGrove.furniture);

                locations[locationData.Key].netObjects.Set(proxyGrove.netObjects.Pairs);

                foreach (Furniture item2 in locations[locationData.Key].furniture)
                {

                    item2.updateDrawPosition();

                }

                foreach (KeyValuePair<Vector2, StardewValley.Object> pair in locations[locationData.Key].objects.Pairs)
                {

                    pair.Value.initializeLightSource(pair.Key);

                    pair.Value.reloadSprite();

                }

            }


        }

        private void SaveUpdated(object sender, SavedEventArgs e)
        {

            if (Context.IsMainPlayer)
            {

                if (!magic)
                {

                    ReinstateLocations();

                    DeserialiseLocations();

                    exportHandle.Produce();

                }

                ReinstateCharacters();

            }

            rite = new();

            questHandle.Ready();

        }

        // ==================================================== MULTIPLAYER

        public void OnModMessageReceived(object sender, ModMessageReceivedEventArgs e)
        {
            
            if (e.FromModID != ModManifest.UniqueID)
            {

                return;

            }

            if (e.FromPlayerID == Game1.player.UniqueMultiplayerID)
            {

                return;

            }

            QueryData.queries queryType;

            try
            {

                queryType = Enum.Parse<QueryData.queries>(e.Type);

            }
            catch
            {

                return;

            }

            // Synchronise progress

            switch (queryType)
            {

                // From farmhand

                case QueryData.queries.FarmhandRequestsProgress:

                    if (!Context.IsMainPlayer) { return; }

                    MessageSyncProgress();

                    return;

                case QueryData.queries.FarmhandRequestsData:

                    if (!Context.IsMainPlayer) { return; }

                    if (!multiplayer.ContainsKey(e.FromPlayerID))
                    {

                        SaveData farmhandData = Helper.Data.ReadSaveData<StardewDruid.Data.SaveData>(SaveData.version + "_" + e.FromPlayerID);

                        if(farmhandData != null)
                        {

                            multiplayer[e.FromPlayerID] = farmhandData;

                        }

                    }
                    
                    if (!multiplayer.ContainsKey(e.FromPlayerID))
                    {

                        multiplayer[e.FromPlayerID] = new SaveData() { id = e.FromPlayerID };

                    }

                    multiplayer[e.FromPlayerID].progress = save.progress;

                    multiplayer[e.FromPlayerID].reliquary = save.reliquary;

                    Helper.Multiplayer.SendMessage(
                        multiplayer[e.FromPlayerID],
                        QueryData.queries.HostProvidesData.ToString(),
                        modIDs: new[] { this.ModManifest.UniqueID }
                    );


                    return;

                case QueryData.queries.FarmhandProvidesSave:

                    if (!Context.IsMainPlayer) { return; }

                    SaveData postPreferences = e.ReadAs<SaveData>();

                    multiplayer[postPreferences.id] = postPreferences;

                    return;

                // From host

                case QueryData.queries.HostProvidesProgress:

                    if (Context.IsMainPlayer) { return; }

                    SaveData syncProgress = e.ReadAs<SaveData>();

                    save.progress = syncProgress.progress;

                    save.reliquary = syncProgress.reliquary;

                    rite = new();

                    questHandle.Ready();

                    Console.WriteLine(StringData.Get(StringData.str.receivedData) + e.FromPlayerID);

                    return;

                case QueryData.queries.HostProvidesData:

                    if (Context.IsMainPlayer) { return; }

                    SaveData syncPreferences = e.ReadAs<SaveData>();

                    if(syncPreferences.id == Game1.player.UniqueMultiplayerID)
                    {

                        save = syncPreferences;

                        receivedSave = true;

                        modReady = true;

                        rite = new();

                        questHandle.Ready();

                    }

                    return;

            }

            if (modPause)
            {

                return;

            }

            // Synchronise all players

            QueryData queryData = e.ReadAs<QueryData>();

            switch (queryType)
            {

                case QueryData.queries.QuestUpdate:

                    if (!Context.IsMainPlayer)
                    {

                        return;

                    }

                    questHandle.UpdateTask(queryData.name, Convert.ToInt32(queryData.value));

                    return;

                case QueryData.queries.SpellHandle:

                    if (Game1.player.currentLocation.Name != queryData.location){ return; }

                    List<int> spellData = System.Text.Json.JsonSerializer.Deserialize<List<int>>(queryData.value);

                    SpellHandle spellEffect = new(Game1.player.currentLocation, spellData);

                    spellRegister.Add(spellEffect);

                    return;

                case QueryData.queries.HaltCharacter:

                    if (!Context.IsMainPlayer)
                    {

                        return;

                    }

                    CharacterHandle.characters characterType = Enum.Parse<CharacterHandle.characters>(queryData.name);

                    Farmer farmerTarget = Game1.GetPlayer(e.FromPlayerID);

                    if (characters.ContainsKey(characterType))
                    {

                        characters[characterType].Halt();

                        characters[characterType].idleTimer = 300;

                        characters[characterType].LookAtTarget(farmerTarget.Position);

                    }

                    return;


            }

            // Send prompts to farmhands

            if (Context.IsMainPlayer)
            { 
                
                return; 
            
            }

            switch (queryType)
            {

                case QueryData.queries.EventDisplay:

                    if (queryData.location != Game1.player.currentLocation.Name)
                    {
                        
                        return;

                    }

                    if (Config.captionOption == ModData.captionOptions.none.ToString())
                    {
                        return;

                    }

                    if(queryData.name == "999")
                    {

                        RegisterDisplay(queryData.description, queryData.value, EventDisplay.displayTypes.strong);

                        return;

                    }

                    EventDisplay cueDisplay = RegisterDisplay(queryData.description, queryData.value);

                    if (Config.captionOption == ModData.captionOptions.auto.ToString() && queryData.name != null)
                    {

                        foreach(NPC presentSpeakers in Game1.player.currentLocation.characters)
                        {

                            if(presentSpeakers.Name == queryData.name)
                            {

                                if (presentSpeakers is StardewDruid.Monster.Boss bossMonster)
                                {

                                    cueDisplay.imageTexture = bossMonster.OverheadTexture();

                                    cueDisplay.imageFrame = bossMonster.OverheadFrame();

                                    cueDisplay.imageSource = bossMonster.OverheadPortrait();

                                }
                                else if (presentSpeakers is StardewDruid.Character.Character charSpeaker)
                                {

                                    cueDisplay.imageTexture = charSpeaker.OverheadTexture();

                                    cueDisplay.imageFrame = charSpeaker.OverheadFrame();

                                    cueDisplay.imageSource = charSpeaker.OverheadPortrait();

                                }

                                break;

                            }

                        }

                    }

                    return;

                case QueryData.queries.EventQuestion:

                    if (queryData.location != Game1.player.currentLocation.Name)
                    {

                        return;

                    }

                    Dictionary<int, DialogueSpecial> conversations = ConversationData.SceneConversations(queryData.value);

                    int conversationId = Convert.ToInt32(queryData.description);

                    if (!conversations.ContainsKey(conversationId))
                    {

                        return;

                    }

                    NPC npc = Game1.player.currentLocation.getCharacterFromName(queryData.name);

                    dialogue[CharacterHandle.characters.disembodied] = new(CharacterHandle.characters.disembodied);

                    dialogue[CharacterHandle.characters.disembodied].npc = npc;

                    dialogue[CharacterHandle.characters.disembodied].QuickplaySpecialDialogue(conversations[conversationId].dialogueId, conversations[conversationId]);

                    return;

                case QueryData.queries.ReceiveRelic:

                    IconData.relics relic = (IconData.relics)(Convert.ToInt32(queryData.value));

                    if(relic == IconData.relics.none)
                    {

                        return;

                    }

                    string relicTitle = relicHandle.reliquary[relic.ToString()].title;

                    DisplayMessage relicMessage = new(relicTitle + StringData.Get(StringData.str.relicReceived), relic);

                    Game1.addHUDMessage(relicMessage);

                    return;

                case QueryData.queries.ThrowSword:

                    SpawnData.Swords sword = (SpawnData.Swords)(Convert.ToInt32(queryData.value));

                    if (sword == SpawnData.Swords.none)
                    {

                        return;

                    }
                    new ThrowHandle(Game1.player, Game1.player.Position - new Vector2(0, 640), sword).register();

                    return;

                case QueryData.queries.AccessHandle:

                    AccessHandle access = new();

                    List<string> accessData = System.Text.Json.JsonSerializer.Deserialize<List<string>>(queryData.value);

                    access.AccessSetup(accessData);

                    access.AccessCheck(Game1.getLocationFromName(queryData.name));

                    return;

                case QueryData.queries.GimmeMoney:

                    int gimmeMoney = Convert.ToInt32(queryData.value);

                    string gimmeQuest = queryData.name;

                    RegisterMessage(gimmeQuest + " " + StringData.Get(StringData.str.questComplete), 1, true);

                    Game1.player.Money += gimmeMoney;

                    return;


                case QueryData.queries.WarpFarmhands:

                    if(Game1.player.currentLocation.Name != queryData.name)
                    {

                        return;

                    }

                    List<int> warpData = System.Text.Json.JsonSerializer.Deserialize<List<int>>(queryData.value);

                    Game1.warpFarmer(queryData.location, warpData[0], warpData[1], warpData[2]);

                    Game1.xLocationAfterWarp = warpData[0];

                    Game1.yLocationAfterWarp = warpData[1];

                    return;



            }

        }

        public void MessageSyncProgress()
        {

            if (!Context.IsMultiplayer) { return; }

            if (!Context.IsMainPlayer) { return; }

            Helper.Multiplayer.SendMessage(
                save,
                QueryData.queries.HostProvidesProgress.ToString(),
                modIDs: new[] { this.ModManifest.UniqueID }
            );

        }

        public void SyncProgress()
        {

            if (!Context.IsMultiplayer)
            {

                return;

            }

            if (!Context.IsMainPlayer)
            {

                return;

            }

            synchroniseProgress = true;

        }

        public void SyncPreferences()
        {

            if (!Context.IsMultiplayer)
            {

                return;

            }

            if (Context.IsMainPlayer)
            {

                return;

            }

            synchronisePreferences = true;

        }

        public void EventQuery(QueryData querydata, QueryData.queries query)
        {

            Helper.Multiplayer.SendMessage<QueryData>(
                querydata,
                query.ToString(),
                new string[1] { ModManifest.UniqueID },
                null
            );

        }

        // ==================================================== OPERATION

        public bool ModPaused()
        {
            modReady = true;
            if (!Context.IsWorldReady)
            {

                modPause = true;

                return true;

            }

            if (!modReady)
            {

                modPause = true;

                return true;

            }

            if (CasterGone())
            {

                modPause = true;

                return true;

            }

            modPause = false;

            return false;

        }

        public static bool CasterGone()
        {

            if (Game1.eventUp)
            {
                return true;
            }

            if (Game1.currentMinigame != null)
            {
                return true;
            }

            if (Game1.killScreen)
            {
                return true;
            }

            return false;

        }

        public bool CasterBusy()
        {

            if (Game1.isWarping)
            {

                if (eventRegister.ContainsKey(Rite.eventTransform))
                {

                    (eventRegister[Rite.eventTransform] as Cast.Ether.Transform).EventWarp();

                }

                return true;

            }

            if (Game1.fadeToBlack)
            {
                return true;
            }

            if (!Game1.game1.IsActive)
            {
                return true;
            }

            if (Game1.paused)
            {
                return true;
            }

            if (Game1.player.freezePause > 0)
            {

                return true;

            }

            return false;

        }

        public bool CasterPaused()
        {

            if (Game1.isTimePaused)
            {
                return true;
            }

            if (Context.IsMultiplayer)
            {

                if (Game1.netWorldState.Value.IsTimePaused)
                {

                    return true;

                }

            }

            return false;

        }

        public static bool CasterMenu()
        {

            if (Game1.overlayMenu != null)
            {

                return true;

            }

            if (Game1.activeClickableMenu != null)
            {

                return true;

            }

            return false;

        }
        
        // ----------------------------------------------------- Button Pressed

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {

            // Game is not ready
            if (modPause)
            {

                return;

            }

            if (CasterBusy())
            {

                return;

            }

            EventHandle.actionButtons actionPressed = ActionButtonPressed();

            if (actionPressed != EventHandle.actionButtons.none)
            {

                buttonPress = e.Button;

                ActionPressed(actionPressed);

                return;

            }

            EventHandle.actionButtons warpPressed = WarpButtonPressed();

            if (warpPressed != EventHandle.actionButtons.none)
            {

                WarpPressed(warpPressed);

                return;

            }

            DruidJournal.journalTypes journalPressed = Journal.DruidJournal.JournalButtonPressed();

            if (journalPressed != DruidJournal.journalTypes.none)
            {

                JournalPressed(journalPressed);

            }

        }

        public void ActionPressed(EventHandle.actionButtons actionPressed)
        {

            if (CasterMenu())
            {

                switch (actionPressed)
                {

                    case EventHandle.actionButtons.rite:

                        if (Game1.activeClickableMenu is DruidJournal druidJournal)
                        {

                            druidJournal.exitThisMenu(true);

                            return;

                        }

                        if (Game1.activeClickableMenu is QuestLog questLog)
                        {

                            questLog.exitThisMenu(true);

                            DruidJournal.openJournal(DruidJournal.journalTypes.none);


                        }

                        break;

                }

                return;

            }

            if (ClickTrigger( actionPressed))
            {

                return;

            }

            switch (actionPressed)
            {

                case EventHandle.actionButtons.action:


                    int mouseX = Game1.getMouseX(true);

                    int mouseY = Game1.getMouseY(true);

                    bool buffHover = Game1.buffsDisplay.isWithinBounds(mouseX,mouseY);

                    if (buffHover)
                    {

                        if (Game1.buffsDisplay.hoverText.Contains(RiteData.Strings(RiteData.riteStrings.clickJournal)))
                        {

                            DruidJournal.openJournal(DruidJournal.journalTypes.quests);

                        }

                    }

                    foreach (EventDisplay display in displayRegister)
                    {

                        display.click(mouseX, mouseY);

                    }

                    foreach (EventBar bar in barRegister)
                    {

                        bar.click(mouseX, mouseY);

                    }

                    return;

                case EventHandle.actionButtons.rite:

                    if (CheckTrigger())
                    {

                        return;

                    }

                    rite.Click();

                    return;

                case EventHandle.actionButtons.warp:

                    rite.Shutdown();

                    RelicFunction.WarpFunction();

                    return;

                case EventHandle.actionButtons.favourite:

                    rite.Shutdown();

                    RelicFunction.QuickFunction();

                    return;

            }

        }

        public void WarpPressed(EventHandle.actionButtons warpPressed)
        {

            if (CasterMenu())
            {

                return;

            }

            switch (warpPressed)
            {

                case EventHandle.actionButtons.warp:

                    rite.Shutdown();

                    RelicFunction.WarpFunction();

                    return;

                case EventHandle.actionButtons.favourite:

                    rite.Shutdown();

                    RelicFunction.QuickFunction();

                    return;

            }

        }

        public void JournalPressed(DruidJournal.journalTypes journalPressed)
        {


            if (CasterMenu())
            {

                if (Game1.activeClickableMenu is DruidJournal druidJournal)
                {

                    if(druidJournal.type == journalPressed)
                    {

                        druidJournal.exitThisMenu(true);

                    }

                    return;

                }

                if (Game1.activeClickableMenu is QuestLog questLog)
                {

                    questLog.exitThisMenu(true);

                    DruidJournal.openJournal(journalPressed);

                }

                return;

            }

            DruidJournal.openJournal(journalPressed);

        }

        public EventHandle.actionButtons ActionButtonPressed()
        {

            if (Config.actionButtons.GetState() == SButtonState.Pressed)
            {

                return EventHandle.actionButtons.action;

            }

            if(Config.specialButtons.GetState() == SButtonState.Pressed)
            {

                return EventHandle.actionButtons.special;

            }

            SButtonState riteButtonState = Config.riteButtons.GetState();

            if (riteButtonState == SButtonState.Pressed)
            {

                return EventHandle.actionButtons.rite;

            }

            return EventHandle.actionButtons.none;

        }

        public EventHandle.actionButtons WarpButtonPressed()
        {

            if (Config.warpButtons.GetState() == SButtonState.Pressed)
            {

                if (!magic && activeEvent.Count == 0)
                {

                    return EventHandle.actionButtons.warp;

                }

            }

            if (Config.favouriteButtons.GetState() == SButtonState.Pressed)
            {

                if (!magic && activeEvent.Count == 0)
                {

                    return EventHandle.actionButtons.favourite;

                }

            }

            return EventHandle.actionButtons.none;

        }

        public void RiteButtonSuppress()
        {

            if (Game1.options.gamepadControls)
            {

                foreach (Keybind keybind in Mod.instance.Config.riteButtons.Keybinds)
                {

                    foreach (SButton bind in keybind.Buttons)
                    {

                        Helper.Input.Suppress(bind);

                        suppress.Add(bind);

                    }

                }

            }

        }

        public bool RiteButtonHeld()
        {

            SButtonState riteButtonState = Config.riteButtons.GetState();

            if (riteButtonState == SButtonState.Held)
            {

                return true;

            }

            if (Game1.options.gamepadControls) 
            { 

                foreach (Keybind keybind in Config.riteButtons.Keybinds)
                {

                    foreach (SButton bind in keybind.Buttons)
                    {

                        if (suppress.Contains(bind))
                        {

                            if (Helper.Input.IsSuppressed(bind))
                            {

                                continue;

                            }

                            suppress.Remove(bind);

                        }
                                
                    }

                }

                if (suppress.Count > 0)
                {

                    return true;

                }

            }

            return false;

        }

        public bool ShiftButtonHeld()
        {

            SButtonState channelButtonState = Config.channelButtons.GetState();

            if (channelButtonState == SButtonState.Held)
            {

                return true;

            }

            return false;

        }

        public bool ClickTrigger(EventHandle.actionButtons actionPressed)
        {

            // action press
            if (!clickRegister.ContainsKey(actionPressed))
            {

                return false;

            }

            if (clickRegister[actionPressed].Count == 0)
            {

                clickRegister.Remove(actionPressed);

                return false;

            }

            for (int i = clickRegister[actionPressed].Count - 1; i >= 0; i--)
            {

                string eventClick = clickRegister[actionPressed].ElementAt(i);

                if (!eventRegister.ContainsKey(eventClick))
                {

                    clickRegister[actionPressed].Remove(eventClick);

                    continue;

                }

                if (eventRegister[eventClick].EventPerformAction(actionPressed))
                {

                    if (eventRegister[eventClick].eventComplete)
                    {

                        eventRegister[eventClick].EventRemove();

                        eventRegister.Remove(eventClick);

                        clickRegister[actionPressed].Remove(eventClick);

                    }

                    return true;

                }

            }

            return false;

        }

        public bool CheckTrigger()
        {

            if (triggerRegister.Count == 0)
            {

                return false;

            }

            if (activeEvent.Count > 0)
            {

                return false;

            }

            foreach (KeyValuePair<string, bool> trigger in triggerRegister)
            {

                if (eventRegister[trigger.Key].TriggerCheck())
                {

                    return true;

                }

            }

            return false;

        }
        
        // ----------------------------------------------------- Every Tick

        private void EveryTicked(object sender, UpdateTickedEventArgs e)
        {

            int tickIndex = (int)(e.Ticks % 60);

            // Game is not ready
            if (ModPaused())
            {

                return;

            }

            for (int c = movers.Count - 1; c >= 0; c--)
            {

                KeyValuePair<CharacterHandle.characters, CharacterMover> mover = movers.ElementAt(c);

                mover.Value.Update();

                movers.Remove(mover.Key);

            }

            // caster is busy
            bool business = (CasterBusy() || CasterMenu());

            for (int j = spellRegister.Count - 1; j >= 0; j--)
            {

                SpellHandle spell = spellRegister[j];

                if(spell.counter == 0 && business)
                {

                    continue;
                
                }

                if (!spell.Update())
                {

                    spellRegister.RemoveAt(j);

                }

            }

            for (int j = throwRegister.Count - 1; j >= 0; j--)
            {

                ThrowHandle throwing = throwRegister[j];

                if (throwing.counter == 0 && business)
                {
                    
                    continue;
                
                }

                if (!throwing.update())
                {

                    throwRegister.RemoveAt(j);

                }

            }


            for (int j = battleRegister.Count - 1; j >= 0; j--)
            {

                BattleHandle battling = battleRegister[j];

                if (!battling.Update())
                {

                    battleRegister.RemoveAt(j);

                }

            }

            // Synchronise multiplayer

            if (tickIndex == 13)
            {

                if (Context.IsMultiplayer)
                {

                    if (synchroniseProgress)
                    {

                        MessageSyncProgress();

                        synchroniseProgress = false;

                    }

                    if (synchronisePreferences)
                    {

                        if (!receivedSave)
                        {

                            Helper.Multiplayer.SendMessage(
                                save,
                                QueryData.queries.FarmhandRequestsData.ToString(),
                                modIDs: new[] { this.ModManifest.UniqueID }
                            );

                        }
                        else
                        {

                            Helper.Multiplayer.SendMessage(
                                save,
                                QueryData.queries.FarmhandProvidesSave.ToString(),
                                modIDs: new[] { this.ModManifest.UniqueID }
                            );

                            synchronisePreferences = false;

                        }

                    }

                }

            }

            if (business)
            {

                return;

            }

            // Event Updates

            rite.Update();

            if (tickIndex == 27)
            {

                rite.RiteBuff();

            }

            if(tickIndex == 46)
            {

                apothecaryHandle.buff.CheckBuff();

            }

            // Event Updates

            if (tickIndex % 6 == 1)
            {

                EveryDecimal();

            }

            if (tickIndex == 39)
            {

                UpdateEvents();

            }

        }

        public void EveryDecimal()
        {

            if (displayRegister.Count > 0)
            {

                for (int d = displayRegister.Count - 1; d >= 0; d--)
                {

                    EventDisplay display = displayRegister[d];

                    if (!display.update())
                    {

                        displayRegister.RemoveAt(d);

                    }

                }

            }

            if (barRegister.Count > 0)
            {

                for (int d = barRegister.Count - 1; d >= 0; d--)
                {

                    EventBar bar = barRegister[d];

                    if (!bar.update())
                    {

                        barRegister.RemoveAt(d);

                    }

                }

            }

            if (eventRegister.Count > 0)
            {

                for (int ev = eventRegister.Count - 1; ev >= 0; ev--)
                {

                    KeyValuePair<string, Event.EventHandle> eventEntry = eventRegister.ElementAt(ev);

                    if (eventEntry.Value.eventActive)
                    {

                        if (eventEntry.Value.stalled)
                        {

                            continue;

                        }

                        eventEntry.Value.EventDecimal();

                        if (eventEntry.Value.eventComplete)
                        {

                            eventRegister[eventEntry.Key].EventRemove();

                            eventRegister.Remove(eventEntry.Key);

                            continue;

                        }

                    }

                }

            }

            if (trackers.Count > 0)
            {

                for (int tr = trackers.Count - 1; tr >= 0; tr--)
                {
                    KeyValuePair<CharacterHandle.characters, TrackHandle> trackEntry = trackers.ElementAt(tr);

                    if (trackEntry.Value.followPlayer == null)
                    {

                        trackers.Remove(trackEntry.Key);

                        continue;

                    }

                    trackEntry.Value.TrackPlayer(tr);

                }

            }

            if (Game1.player.currentLocation is DruidLocation location)
            {

                environment.update();

                foreach (TerrainField terrainField in location.terrainFields)
                {

                    terrainField.update(location);

                }

                foreach (TerrainField grassField in location.grassFields)
                {

                    grassField.update(location);

                }

                foreach (LightField light in location.lightFields)
                {

                    light.update(location);

                }

            }


        }

        private void UpdateEvents()
        {

            if (eventRegister.Count == 0)
            {

                return;

            }

            List<string> removal = new();

            for (int er = eventRegister.Count - 1; er >= 0; er--)
            {

                KeyValuePair<string, Event.EventHandle> eventHandle = eventRegister.ElementAt(er);

                Event.EventHandle eventEntry = eventHandle.Value;

                if (eventEntry.eventActive)
                {

                    if (!eventEntry.EventActive())
                    {

                        eventEntry.EventRemove();

                        removal.Add(eventHandle.Key);

                        continue;

                    }

                    if (eventEntry.stalled)
                    {

                        continue;

                    }

                    if (eventEntry.eventActive)
                    {

                        eventEntry.EventInterval();

                    }

                }
                else if (eventEntry.triggerEvent)
                {

                    if (Game1.player.currentLocation is null)
                    {

                        eventEntry.TriggerAbort();

                        continue;

                    }

                    eventEntry.TriggerUpdate();

                }
                else if (eventEntry.localEvent)
                {

                    if (Game1.player.currentLocation is null)
                    {

                        continue;

                    }

                    eventEntry.LocalUpdate();

                }

            }

            foreach (string remove in removal)
            {

                eventRegister.Remove(remove);

                if (activeEvent.ContainsKey(remove))
                {

                    activeEvent.Remove(remove);

                }

            }

        }

        public void RegisterEvent(Event.EventHandle eventHandle, string placeHolder, bool active = false)
        {

            if (placeHolder == String.Empty || placeHolder == null)
            {

                placeHolder = eventHandle.GetType().ToString() + "_" + Game1.currentGameTime.TotalGameTime.Milliseconds.ToString();

            }

            if (active)
            {

                activeEvent[placeHolder] = "active";

            }

            if (eventRegister.ContainsKey(placeHolder))
            {

                return;

            }

            eventRegister[placeHolder] = eventHandle;

        }

        // ----------------------------------------------------- Rendered Step

        public void OnRenderingStep(object sender, RenderingStepEventArgs e)
        {
            switch (e.Step)
            {

                case StardewValley.Mods.RenderSteps.HUD:

                    if (modPause)
                    {

                        return;

                    }

                    if (!Game1.displayHUD)
                    {

                        return;

                    }

                    int mouseX = Game1.getMouseX(true);

                    int mouseY = Game1.getMouseY(true);

                    DrawEventDisplay(e.SpriteBatch, mouseX, mouseY);

                    DrawEventBar(e.SpriteBatch, mouseX, mouseY);

                    return;

            }

        }

        public void DrawEventDisplay(SpriteBatch b, int mouseX, int mouseY)
        {

            if (Game1.activeClickableMenu != null)
            {

                return;

            }

            displayOffset = 124;

            for (int d = displayRegister.Count - 1; d >= 0; d--)
            {

                EventDisplay display = displayRegister.ElementAt(d);

                display.draw(b, mouseX, mouseY);

            }

        }

        public void DrawEventBar(SpriteBatch b, int mouseX, int mouseY)
        {

            bool barFade = false;

            if (Game1.activeClickableMenu != null)
            {
                
                if (Game1.activeClickableMenu is DialogueBox)
                {

                    barFade = true;

                }
                else
                {
                    return;

                }

            }

            barOffset = 300;

            for (int d = barRegister.Count - 1; d >= 0; d--)
            {

                EventBar bar = barRegister.ElementAt(d);

                bar.draw(b, mouseX, mouseY, barFade);

            }

        }

        public void OnRenderedStep(object sender, RenderedStepEventArgs e)
        {

            switch (e.Step)
            {

                case StardewValley.Mods.RenderSteps.HUD:

                    if (modPause)
                    {

                        return;

                    }

                    apothecaryHandle.buff.HoverBuff(e.SpriteBatch);

                    break;

                case StardewValley.Mods.RenderSteps.World_Sorted:

                    if (modPause)
                    {

                        return;

                    }

                    for (int er = eventRegister.Count - 1; er >= 0; er--)
                    {

                        KeyValuePair<string, Event.EventHandle> eventHandle = eventRegister.ElementAt(er);

                        eventHandle.Value.EventDraw(e.SpriteBatch);

                    }

                    if (CasterBusy())
                    {

                        return;

                    }

                    rite.Draw(e.SpriteBatch);

                    break;

            }

        }

        public void RegisterMessage(string message, int type = 0, bool ignore = false)
        {

            if (messageBuffer < Game1.currentGameTime.TotalGameTime.TotalSeconds || ignore)
            {

                HUDMessage hudmessage = new(message, type);

                if(type == 0 || type == -1)
                {
                    hudmessage.noIcon = true;
                }

                Game1.addHUDMessage(hudmessage);

                messageBuffer = Game1.currentGameTime.TotalGameTime.TotalSeconds + 6;

            }

        }

        public EventBar RegisterBar(string Text, EventBar.barTypes BarType = EventBar.barTypes.scene, int UniqueId = 0)
        {

            EventBar bar = new(Text, BarType, UniqueId);

            barRegister.Add(bar);

            return bar;

        }

        public void RemoveBars(string eventId)
        {

            for (int i = barRegister.Count - 1; i >= 0; i--)
            {

                EventBar registered = barRegister[i];

                if (registered.eventId == eventId)
                {

                    barRegister.RemoveAt(i);

                }

            }

        }

        public EventDisplay RegisterDisplay(string Text, string Title = null, EventDisplay.displayTypes DisplayType = EventDisplay.displayTypes.plain, int UniqueId = 0)
        {

            EventDisplay display = new(Text, Title, 70, DisplayType, UniqueId);

            displayRegister.Add(display);

            ResetDisplay();

            return display;

        }

        public void ResetDisplay()
        {


            List<EventDisplay> middleDisplays = new();

            foreach (EventDisplay registered in displayRegister)
            {

                middleDisplays.Add(registered);

            }

            if(middleDisplays.Count <= 3)
            {

                return;

            }

            for(int i = 0; i < middleDisplays.Count - 3; i++)
            {

                middleDisplays[i].progress = middleDisplays[i].time - 4;

            }

        }

        public void RemoveDisplays(string eventId)
        {

            for(int i = displayRegister.Count - 1; i >= 0; i--)
            {

                EventDisplay registered = displayRegister[i];

                if(registered.eventId == eventId)
                {
                    
                    registered.progress = registered.time - 4;
                
                }

            }

        }

        // ========================================================= REGISTER

        public int CombatDamage()
        {

            if(DamagePing > (Game1.currentGameTime.TotalGameTime.TotalSeconds - 30.00))
            {

                return DamageLevel;

            }

            int damageLevel = 200;

            //if (!Config.maxDamage)
            //{

                damageLevel = 3 * Game1.player.CombatLevel; // 30

                damageLevel += 2 * Game1.player.MiningLevel; // 10

                damageLevel += 1 * Game1.player.ForagingLevel; // 10

                damageLevel += 1 * Game1.player.FarmingLevel; // 10

                damageLevel += 1 * Game1.player.FishingLevel; // 10

                damageLevel = Math.Min(80, damageLevel); // 80

                if (magic)
                {

                    damageLevel *= 2;

                }
                else
                {

                    damageLevel += save.progress.Count * 3; // 90

                }

                damageLevel = Math.Min(150, damageLevel); // 150

                if (Game1.player.CurrentTool is Tool currentTool) // 25
                {

                    if (currentTool.enchantments.Count > 0)
                    {

                        damageLevel += 20;

                    }

                }

                if (Game1.player.professions.Contains(24)) // 25
                {
                    damageLevel += 15;
                }

                if (Game1.player.professions.Contains(26)) // 25
                {
                    damageLevel += 15;
                }

                damageLevel = Math.Min(200, damageLevel); // 150

            //}

            switch (PowerLevel)
            {

                case 1:

                    damageLevel = Math.Min(damageLevel, 60);

                    break;

                case 2:

                    damageLevel = Math.Min(damageLevel, 90);

                    break;

                case 3:

                    damageLevel = Math.Min(damageLevel, 120);

                    break;

                case 4:

                    damageLevel = Math.Min(damageLevel, 150);

                    break;

            }

            if (apothecaryHandle.buff.applied.ContainsKey(BuffHandle.buffTypes.alignment))
            {

                damageLevel += (int)(damageLevel * 0.1f * apothecaryHandle.buff.applied[BuffHandle.buffTypes.alignment].level);

            }

            DamageLevel = damageLevel;

            DamagePing = Game1.currentGameTime.TotalGameTime.TotalSeconds;

            return damageLevel;

        }

        public List<float> CombatCritical()
        {

            float critChance = 0.05f;

            if (Game1.player.professions.Contains(25))
            {

                critChance += 0.1f;

            }

            if (Game1.player.hasBuff("statue_of_blessings_5"))
            {
                
                critChance += 0.05f;
            
            }

            float critModifier = 0.5f;

            if (Game1.player.professions.Contains(29))
            {

                critModifier += 0.5f;

            }

            if (apothecaryHandle.buff.applied.ContainsKey(BuffHandle.buffTypes.vigor))
            {

                critChance += (0.03f * apothecaryHandle.buff.applied[BuffHandle.buffTypes.vigor].level);

                critModifier += (0.1f * apothecaryHandle.buff.applied[BuffHandle.buffTypes.vigor].level);

            }

            if(Game1.player.CurrentTool is MeleeWeapon weapon)
            {

                critChance += (weapon.critChance.Value * 2);

                critModifier += (weapon.critMultiplier.Value / 10);

            }

            return new() { critChance, critModifier };

        }

        public int ModDifficulty()
        {

            int difficulty = 2;

            switch (Config.modDifficulty)
            {

                case "kiwi":

                    difficulty = 12;

                    break;

                case "hard":

                    difficulty = 9;
                    
                    break;

                case "medium":

                    difficulty = 6;

                    break;

                case "easy": 

                    difficulty = 4;

                    break;

            }
            
            return difficulty;

        }

        public int CombatDifficulty()
        {

            int difficulty = ModDifficulty();

            if (Context.IsMultiplayer)
            {

                difficulty += 2;

            }

            return 3 + (PowerLevel * difficulty / 2);

        }

        public void GiveExperience(int skill, int amount)
        {

            int adjust = amount;

            switch (Config.modDifficulty)
            {

                case "kiwi":

                    adjust = (int)(amount * 0.4f);

                    break;

                case "hard":

                    adjust = (int)(amount * 0.7f);

                    break;

            }

            Game1.player.gainExperience(skill, adjust);

        }

        public static void DisableTrinkets()
        {


        }

        public void WarpAllFarmers(string location, int x, int y, int d)
        {

            List<int> array = new()
            {
                x,
                y,
                d,
            };

            QueryData query = new()
            {

                name = Game1.player.currentLocation.Name,

                value = System.Text.Json.JsonSerializer.Serialize(array),

                location = location,

            };

            EventQuery(query, QueryData.queries.WarpFarmhands);

            Game1.warpFarmer(location, x, y, d);

            Game1.xLocationAfterWarp = x;

            Game1.yLocationAfterWarp = y;

        }

        public bool Witnessed(ReactionData.reactions type, NPC witness)
        {
            
            return Witnessed(type, witness.Name);

        }

        public bool Witnessed(ReactionData.reactions type, string name)
        {

            if (!reactions.ContainsKey(name))
            {

                reactions[name] = new();

                return false;

            }

            if (reactions[name].Contains(type))
            {

                return true;

            }

            return false;

        }

        public void AddWitness(ReactionData.reactions type, string name)
        {

            Witnessed(type, name);

            reactions[name].Add(type);

        }


    }

}

