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
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StardewDruid
{

    public class Mod : StardewModdingAPI.Mod
    {

        public bool magic;

        public ModData Config;

        public bool modReady;

        public bool synchroniseProgress;

        public bool synchronisePreferences;

        public bool receivedPreferences;

        internal static Mod instance;

        public Rite rite;

        public IconData iconData;

        public QuestHandle questHandle;

        public HerbalHandle herbalData;

        public RelicData relicsData;

        public ExportHandle exportHandle;

        internal StardewDruid.Data.StaticData save;

        public Dictionary<string, Event.EventHandle> eventRegister = new();

        public List<Event.EventDisplay> displayRegister = new();

        public int displayOffset = 0;

        public int barOffset = 0;

        public Dictionary<string, string> activeEvent = new();

        public Dictionary<string, string> drawEvent = new();

        public Dictionary<EventHandle.actionButtons,List<string>> clickRegister = new();

        public List<SpellHandle> spellRegister = new();

        public List<ThrowHandle> throwRegister = new();

        public List<BattleHandle> battleRegister = new();

        public Dictionary<string, List<ReactionData.reactions>> reactions = new();

        public Dictionary<ReactionData.reactions, int> reactionLimits = new();

        public Dictionary<CharacterHandle.characters,CharacterMover> movers = new();

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

        public Dictionary<CharacterHandle.characters, StardewDruid.Dialogue.Dialogue> dialogue = new();

        public Dictionary<CharacterHandle.characters, TrackHandle> trackers = new();

        public Dictionary<CharacterHandle.characters, StardewValley.Objects.Chest> chests = new();

        public Dictionary<string, GameLocation> locations = new();

        public string mapped;

        public Dictionary<Vector2,string> features = new();

        public Random randomIndex = new();

        public List<SButton> suppress = new();

        public SoundHandle sounds;

        public int environmentCount;

        public EnvironmentHandle environment;

        public int version = 220;

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

            helper.Events.GameLoop.OneSecondUpdateTicked += EverySecond;

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

            SoundData.AddSounds();

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

            drawEvent = new();

            clickRegister = new();

            spellRegister = new();

            throwRegister = new();

            characters = new();

            dopplegangers = new();

            dialogue = new();

            chests = new();

            trackers = new();

            reactions = new();

            reactionLimits = new();

            displayRegister = new();

            locations = new();

            questHandle = new QuestHandle();

            questHandle.LoadQuests();

            herbalData = new HerbalHandle();

            herbalData.LoadHerbals();

            relicsData = new RelicData();

            relicsData.LoadRelics();

            exportHandle = new ExportHandle();

            exportHandle.LoadExports();

            virtualPick = new Pickaxe();

            virtualPick.DoFunction(Game1.player.currentLocation, 0, 0, 1, Game1.player);

            virtualPick.UpgradeLevel = 5;

            virtualAxe = new Axe();

            virtualAxe.DoFunction(Game1.player.currentLocation, 0, 0, 1, Game1.player);

            virtualAxe.UpgradeLevel = 5;

            virtualHoe = new Hoe();

            virtualHoe.DoFunction(Game1.player.currentLocation, 0, 0, 1, Game1.player);

            virtualHoe.UpgradeLevel = 5;

            virtualCan = new WateringCan();

            virtualCan.DoFunction(Game1.player.currentLocation, 0, 0, 1, Game1.player);

            virtualCan.UpgradeLevel = 5;

            virtualPail = new MilkPail();

            virtualPail.DoFunction(Game1.player.currentLocation, 0, 0, 1, Game1.player);

            virtualShears = new Shears();

            virtualShears.DoFunction(Game1.player.currentLocation, 0, 0, 1, Game1.player);

            Game1.player.Stamina += Math.Min(12, Game1.player.MaxStamina - Game1.player.Stamina);

            if (!Context.IsMainPlayer)
            {

                save = new();

                if (!magic)
                {

                    Helper.Multiplayer.SendMessage(new QueryData(), QueryData.queries.RequestProgress.ToString(), modIDs: new[] { this.ModManifest.UniqueID });
                
                }

                Helper.Multiplayer.SendMessage(new QueryData(), QueryData.queries.RequestPreferences.ToString(), modIDs: new[] { this.ModManifest.UniqueID });

                return;

            }

            save = Helper.Data.ReadSaveData<StardewDruid.Data.StaticData>("saveData_" + version.ToString());

            save ??= new();

            if (magic)
            {
                
                // only need save data for potions
                ReadyState();

                return;

            }

            if (Config.setMilestone != 0)
            {

                if (save.set != Config.setMilestone)
                {

                    save.progress = new();

                    save.reliquary = new();

                    save.characters = new();

                    save.milestone = (QuestHandle.milestones)Config.setMilestone;

                    save.set = Config.setMilestone;

                    if (save.milestone != QuestHandle.milestones.none)
                    {

                        questHandle.Promote(save.milestone);

                    }

                    save.rite = rite.RequirementCheck(rite.requirement.Last().Key,true);

                    if (Config.setOnce)
                    {

                        Config.setOnce = false;

                        Config.setMilestone = 0;

                        SaveConfig();

                    }

                }

            }

            if (save.milestone == QuestHandle.milestones.none)
            {

                questHandle.Promote((QuestHandle.milestones)1);

            }

            Helper.Data.WriteJsonFile("saveData.json", save);

            // for pre 3.8.6 inventory serialisation
            CharacterHandle.ConvertInventory();

            ReadyState();

            DeserialiseGrove();

            exportHandle.Produce();

        }

        public void ReadyState()
        {

            modReady = true;

            rite.Reset();

            questHandle.Ready();

            /*Mod.instance.save.herbalism[herbals.omen_feather] = 2;
            Mod.instance.save.herbalism[herbals.omen_tuft] = 3;
            Mod.instance.save.herbalism[herbals.omen_shell] = 4;
            Mod.instance.save.herbalism[herbals.omen_tusk] = 1;
            Mod.instance.save.herbalism[herbals.omen_nest] = 1;
            Mod.instance.save.herbalism[herbals.omen_glass] = 2;

            Mod.instance.save.herbalism[herbals.trophy_shroom] = 5;
            Mod.instance.save.herbalism[herbals.trophy_eye] = 1;
            Mod.instance.save.herbalism[herbals.trophy_pumpkin] = 1;
            Mod.instance.save.herbalism[herbals.trophy_pearl] = 2;
            Mod.instance.save.herbalism[herbals.trophy_tooth] = 4;
            Mod.instance.save.herbalism[herbals.trophy_spiral] = 3;

            Mod.instance.save.herbalism[herbals.omen_down] = 1;
            Mod.instance.save.herbalism[herbals.omen_coral] = 2;
            Mod.instance.save.herbalism[herbals.trophy_spike] = 4;
            Mod.instance.save.herbalism[herbals.trophy_seed] = 3;

            Mod.instance.save.pals[CharacterHandle.characters.PalSerpent] = new(CharacterHandle.characters.PalSerpent);
            Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.monster_serpent.ToString());

            Mod.instance.save.pals[CharacterHandle.characters.PalSpirit] = new(CharacterHandle.characters.PalSpirit);
            Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.monster_spirit.ToString());

            Mod.instance.save.pals[CharacterHandle.characters.PalGhost] = new(CharacterHandle.characters.PalGhost);
            Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.monster_ghost.ToString());*/

            return;

        }

        public void SaveConfig()
        {

            Helper.Data.WriteJsonFile("config.json", Config);

        }

        private void SaveShutdown(object sender, SavingEventArgs e)
        {

            trackers.Clear();

            dopplegangers.Clear();

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

                SerialiseGrove();

                SaveCharacters();

            }

            RemoveCharacters();

            if (!magic)
            {

                RemoveLocations();

            }

            Helper.Data.WriteSaveData("saveData_" + version.ToString(), save);

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

            save.characters = new();

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

                        save.characters[character.Key] = Character.Character.mode.home;

                        break;

                    case Character.Character.mode.track:
                    case Character.Character.mode.recruit:

                        characters[character.Key].currentLocation.characters.Remove(characters[character.Key]);

                        CharacterHandle.locations home = CharacterHandle.CharacterHome(character.Key);

                        characters[character.Key].currentLocation = Game1.getLocationFromName(CharacterHandle.CharacterLocation(home));

                        characters[character.Key].Position = CharacterHandle.CharacterStart(home, character.Key);

                        save.characters[character.Key] = character.Value.modeActive;

                        break;

                    default:

                        save.characters[character.Key] = character.Value.modeActive;

                        break;

                }

            }

            //save.serialchests.Clear();

            foreach (KeyValuePair<CharacterHandle.characters, StardewValley.Objects.Chest> chest in chests)
            {
                    
                if (chest.Value == null)
                {

                    continue;

                }

                /*if (save.chests.ContainsKey(chest.Key))
                {

                    save.chests[chest.Key].Clear();

                }
                else
                {

                    save.chests[chest.Key] = new();

                }*/
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

                save.serialchests[chest.Key] = xml;

                bool redundant = false;

                if (redundant)
                {

                    List<string> removal = new();

                    for (int i = chest.Value.Items.Count - 1; i >= 0; i--)
                    {

                        Item item = chest.Value.Items.ElementAt(i);

                        if (item == null)
                        {

                            continue;

                        }

                        ParsedItemData parsedItemData = ItemRegistry.GetData(item.QualifiedItemId);

                        if (System.Object.ReferenceEquals(parsedItemData, null))
                        {

                            continue;

                        }

                        if (parsedItemData.ItemType is not ObjectDataDefinition)
                        {

                            if (!Game1.player.addItemToInventoryBool(item))
                            {

                                Game1.player.dropItem(item);

                            }

                            continue;

                        }

                        save.chests[chest.Key].Add(new() { id = item.QualifiedItemId, quality = item.quality.Value, stack = item.stack.Value, });

                    }

                }

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
                save.characters.ContainsKey(character.Key) ?
                save.characters[character.Key] :
                Character.Character.mode.home;

                character.Value.SwitchToMode(mode, Game1.player);

            }

        }

        public void SerialiseGrove()
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            if (!Config.decorateGrove)
            {

                return;

            }

            GameLocation proxyGrove = new Shed();

            int overnightMinutesElapsed = Utility.CalculateMinutesUntilMorning(Game1.timeOfDay);

            locations[LocationHandle.druid_grove_name].passTimeForObjects(overnightMinutesElapsed);

            proxyGrove.furniture.Set(locations[LocationHandle.druid_grove_name].furniture);

            proxyGrove.netObjects.Set(locations[LocationHandle.druid_grove_name].netObjects.Pairs);

            if (Config.plantGrove)
            {

                proxyGrove.largeTerrainFeatures.Set(locations[LocationHandle.druid_grove_name].largeTerrainFeatures);

                proxyGrove.terrainFeatures.Set(locations[LocationHandle.druid_grove_name].terrainFeatures.Pairs);

            }

            XmlSerializer serializer = new(typeof(GameLocation));

            MemoryStream memStream;
            memStream = new MemoryStream();
            XmlTextWriter xmlWriter;
            xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8)
            {
                Namespaces = true
            };
            serializer.Serialize(xmlWriter, proxyGrove);
            xmlWriter.Close();
            memStream.Close();
            string xml;
            xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            xml = xml[xml.IndexOf(Convert.ToChar(60))..];
            xml = xml[..(xml.LastIndexOf(Convert.ToChar(62)) + 1)];

            save.serialise = xml;

        }

        public void DeserialiseGrove()
        {
            
            if (!Context.IsMainPlayer)
            {

                return;

            }

            if (!Config.decorateGrove)
            {

                return;

            }

            if (save.serialise == string.Empty)
            {

                return;

            }

            if (!locations.ContainsKey(LocationHandle.druid_grove_name))
            {
                return;

            }

            XmlSerializer serializer = new(typeof(GameLocation));

            StringReader stringReader;
            stringReader = new StringReader(save.serialise);
            XmlTextReader xmlReader;
            xmlReader = new XmlTextReader(stringReader);
            GameLocation proxyGrove = (GameLocation)serializer.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();

            locations[LocationHandle.druid_grove_name].furniture.Set(proxyGrove.furniture);

            locations[LocationHandle.druid_grove_name].netObjects.Set(proxyGrove.netObjects.Pairs);

            foreach (Furniture item2 in locations[LocationHandle.druid_grove_name].furniture)
            {
                item2.updateDrawPosition();
            }

            foreach (KeyValuePair<Vector2, StardewValley.Object> pair in locations[LocationHandle.druid_grove_name].objects.Pairs)
            {
                pair.Value.initializeLightSource(pair.Key);
                pair.Value.reloadSprite();
            }

            if (Config.plantGrove)
            {

                locations[LocationHandle.druid_grove_name].largeTerrainFeatures.Set(proxyGrove.largeTerrainFeatures);

                locations[LocationHandle.druid_grove_name].terrainFeatures.Set(proxyGrove.terrainFeatures.Pairs);

                foreach (LargeTerrainFeature largeTerrainFeature in locations[LocationHandle.druid_grove_name].largeTerrainFeatures)
                {
                    largeTerrainFeature.Location = locations[LocationHandle.druid_grove_name];
                    largeTerrainFeature.loadSprite();
                }

                foreach (TerrainFeature value4 in locations[LocationHandle.druid_grove_name].terrainFeatures.Values)
                {
                    value4.Location = locations[LocationHandle.druid_grove_name];
                    value4.loadSprite();
                    if (value4 is HoeDirt hoeDirt)
                    {
                        hoeDirt.updateNeighbors();
                    }
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

                    DeserialiseGrove();

                    exportHandle.Produce();

                }

                ReinstateCharacters();

            }

            rite = new();

            ReadyState();

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

            if (Context.IsMainPlayer)
            {

                save.multiplayer ??= new();


            }
            else
            {

                save ??= new();

            }

            // Synchronise progress

            switch (queryType)
            {

                case QueryData.queries.RequestProgress:

                    if (!Context.IsMainPlayer) { return; }

                    MessageSyncProgress();

                    return;

                case QueryData.queries.SyncProgress:

                    if (Context.IsMainPlayer) { return; }

                    StaticData syncProgress = e.ReadAs<StaticData>();

                    save.reliquary = syncProgress.reliquary;

                    save.progress = syncProgress.progress;

                    save.milestone = syncProgress.milestone;

                    save.restoration = syncProgress.restoration;

                    ReadyState();

                    Console.WriteLine(StringData.Strings(StringData.stringkeys.receivedData) + e.FromPlayerID);

                    return;

                case QueryData.queries.RequestPreferences:

                    if (!Context.IsMainPlayer) { return; }

                    MessageSendPreferences();

                    return;

                case QueryData.queries.PostPreferences:

                    if (!Context.IsMainPlayer) { return; }

                    StaticData postPreferences = e.ReadAs<StaticData>();

                    if (!save.multiplayer.ContainsKey(e.FromPlayerID))
                    {

                        save.multiplayer[e.FromPlayerID] = new();

                    }

                    save.multiplayer[e.FromPlayerID].attunement = postPreferences.attunement;

                    save.multiplayer[e.FromPlayerID].herbalism = postPreferences.herbalism;

                    save.multiplayer[e.FromPlayerID].potions = postPreferences.potions;

                    save.multiplayer[e.FromPlayerID].pals = postPreferences.pals;

                    return;

                case QueryData.queries.SyncPreferences:

                    if (Context.IsMainPlayer) { return; }

                    StaticData syncPreferences = e.ReadAs<StaticData>();

                    save ??= new();

                    if (syncPreferences.multiplayer.ContainsKey(Game1.player.UniqueMultiplayerID))
                    {

                        save.attunement = syncPreferences.multiplayer[Game1.player.UniqueMultiplayerID].attunement;

                        save.herbalism = syncPreferences.multiplayer[Game1.player.UniqueMultiplayerID].herbalism;

                        save.potions = syncPreferences.multiplayer[Game1.player.UniqueMultiplayerID].potions;

                        save.pals = syncPreferences.multiplayer[Game1.player.UniqueMultiplayerID].pals;

                    }
                    else if(!receivedPreferences)
                    {

                        save.attunement = syncPreferences.attunement;

                        save.herbalism = syncPreferences.herbalism;

                        save.potions = syncPreferences.potions;

                        save.pals = syncPreferences.pals;

                    }

                    receivedPreferences = true;

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

                case QueryData.queries.AddExport:

                    if (!Context.IsMainPlayer)
                    {

                        return;

                    }

                    ExportHandle.exports addexport = Enum.Parse<ExportHandle.exports>(queryData.name);

                    Mod.instance.exportHandle.AddExport(addexport, Convert.ToInt32(queryData.value));

                    return;

                case QueryData.queries.AddPal:

                    if (!Context.IsMainPlayer)
                    {

                        return;

                    }

                    CharacterHandle.characters addpal = Enum.Parse<CharacterHandle.characters>(queryData.name);

                    PalHandle.ReceiveHelp(addpal, Convert.ToInt32(queryData.value));

                    return;

                case QueryData.queries.RequestGoods:

                    exportHandle.PostStock();

                    return;

                case QueryData.queries.SyncGoods:

                    exportHandle.SynchroniseStock(queryData.value);

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

                    EventDisplay cueDisplay = CastDisplay(queryData.description, queryData.value);

                    if (Config.captionOption == ModData.captionOptions.auto.ToString() && queryData.name != null)
                    {

                        foreach(NPC presentSpeakers in Game1.player.currentLocation.characters)
                        {

                            if(presentSpeakers.Name == queryData.name)
                            {

                                if (presentSpeakers is StardewDruid.Monster.Boss bossMonster)
                                {

                                    cueDisplay.portrait = bossMonster.OverheadTexture();

                                    cueDisplay.portraitSource = bossMonster.OverheadPortrait();

                                }
                                else if (presentSpeakers is StardewDruid.Character.Character charSpeaker)
                                {

                                    cueDisplay.portrait = charSpeaker.OverheadTexture();

                                    cueDisplay.portraitSource = charSpeaker.OverheadPortrait();

                                }

                                break;

                            }

                        }

                    }

                    return;

                case QueryData.queries.EventDialogue:

                    if (queryData.location != Game1.player.currentLocation.Name)
                    {
                        
                        return;

                    }

                    if(queryData.name == null)
                    {

                        Game1.currentSpeaker = null;

                        List<string> answers = ModUtility.SplitStringByLength(queryData.value, 185);

                        Game1.activeClickableMenu = new DialogueBox(answers);

                    }
                    else
                    {

                        NPC npc = Game1.player.currentLocation.getCharacterFromName(queryData.name);

                        Game1.currentSpeaker = npc;

                        if (npc == null)
                        {

                            List<string> answers = ModUtility.SplitStringByLength(queryData.value, 185);

                            Game1.activeClickableMenu = new DialogueBox(answers);

                        }
                        else
                        {

                            StardewValley.Dialogue dialogue = new(npc, "0", queryData.value);

                            Game1.activeClickableMenu = new DialogueBox(dialogue);

                        }

                    }

                    Game1.player.Halt();

                    Game1.player.CanMove = false;

                    return;


                case QueryData.queries.EventQuestion:

                    if (queryData.location != Game1.player.currentLocation.Name)
                    {

                        return;

                    }

                    List<Response> responseList = new();

                    List<string> responseArray = queryData.description.Split("|").ToList();

                    for (int r = 0; r < responseArray.Count; r++)
                    {

                        responseList.Add(new(r.ToString(), responseArray[r]));

                    }

                    responseList.Add(new Response("999", Helper.Translation.Get("DialogueData.Nothing")).SetHotKey(Microsoft.Xna.Framework.Input.Keys.Escape));

                    Game1.player.currentLocation.createQuestionDialogue(queryData.value, responseList.ToArray(), queryData.name);

                    Game1.player.Halt();

                    Game1.player.CanMove = false;

                    return;

                case QueryData.queries.ThrowRelic:

                    IconData.relics relic = (IconData.relics)(Convert.ToInt32(queryData.value));

                    if(relic == IconData.relics.none)
                    {

                        return;

                    }

                    new ThrowHandle(Game1.player,Game1.player.Position - new Vector2(0,640), relic).register();

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

                    CastMessage(gimmeQuest + " " + StringData.Strings(StringData.stringkeys.questComplete), 1, true);

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

            StaticData requestProgress = new()
            {
                reliquary = save.reliquary,

                progress = save.progress,

                milestone = save.milestone,

                attunement = save.attunement,

                herbalism = save.herbalism,

                potions = save.potions,

                pals = save.pals,

                multiplayer = save.multiplayer,

            };

            Helper.Multiplayer.SendMessage(
                requestProgress,
                QueryData.queries.SyncProgress.ToString(),
                modIDs: new[] { this.ModManifest.UniqueID }
            );

        }

        public void MessageSendPreferences()
        {

            StaticData requestProgress = new()
            {

                attunement = save.attunement,

                herbalism = save.herbalism,

                potions = save.potions,

                pals = save.pals,

                multiplayer = save.multiplayer,

            };

            Helper.Multiplayer.SendMessage(
                requestProgress,
                QueryData.queries.SyncPreferences.ToString(),
                modIDs: new[] { this.ModManifest.UniqueID }
            );

        }

        public void MessagePostPreferences()
        {

            StaticData requestProgress = new()
            {
                
                attunement = save.attunement,

                herbalism = save.herbalism,

                potions = save.potions,

                pals = save.pals,

            };

            Helper.Multiplayer.SendMessage(
                requestProgress,
                QueryData.queries.PostPreferences.ToString(),
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

            if (!receivedPreferences)
            {

                return;

            }

            synchronisePreferences = true;

        }

        // ==================================================== OPERATION

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

            if (Game1.paused)
            {
                return true;
            }

            if (Game1.fadeToBlack)
            {
                return true;
            }

            if (Game1.isTimePaused)
            {
                return true;
            }

            if (!Game1.game1.IsActive)
            {
                return true;
            }

            if (Game1.player.freezePause > 0)
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

        private void EverySecond(object sender, OneSecondUpdateTickedEventArgs e)
        {

            if (!Context.IsWorldReady || !modReady)
            {

                return;

            }

            if (Context.IsMultiplayer)
            {

                if (synchroniseProgress)
                {

                    MessageSyncProgress();

                    synchroniseProgress = false;

                }

                if (synchronisePreferences)
                {

                    MessagePostPreferences();

                    synchronisePreferences = false;

                }

            }

            if (eventRegister.Count == 0)
            {

                return;
            
            }

            if (CasterBusy() || CasterMenu())
            {

                return;

            }

            bool exitAll = false;

            if (CasterGone())
            {

                exitAll = true;

            }
    
            List<string> removal = new();

            for(int er = eventRegister.Count - 1; er >= 0; er--)
            {

                KeyValuePair<string, Event.EventHandle> eventHandle = eventRegister.ElementAt(er);
                
                Event.EventHandle eventEntry = eventHandle.Value;

                if (eventEntry.eventActive)
                {

                    if (exitAll)
                    {

                        eventEntry.eventAbort = true;

                    }

                    if (!eventEntry.EventActive())
                    {

                        eventEntry.EventRemove();

                        removal.Add(eventHandle.Key);

                        continue;

                    }

                    if (eventEntry.eventActive)
                    {

                        eventEntry.EventTimer();

                        eventEntry.EventInterval();

                        if (activeEvent.ContainsKey(eventHandle.Key))
                        {

                            DisableTrinkets();

                        }

                    }

                }
                else if (eventEntry.triggerEvent)
                {

                    if (eventEntry.TriggerAbort())
                    {

                        removal.Add(eventHandle.Key);

                    }
                    else
                    if (exitAll || Game1.player.currentLocation is null)
                    {

                        continue;

                    }
                    else
                    if (eventEntry.TriggerActive())
                    {

                        eventEntry.TriggerInterval();

                    }

                }

            }

            foreach (string remove in removal)
            {
                
                eventRegister.Remove(remove);

                if (activeEvent.ContainsKey(remove))
                {

                    activeEvent.Remove(remove);

                }

                if (drawEvent.ContainsKey(remove))
                {

                    drawEvent.Remove(remove);

                }

            }

        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {

            // Game is not ready
            if (!Context.IsWorldReady || !modReady)
            {
                return;

            }

            if (CasterGone())
            {

                rite.Shutdown();

                return;

            }

            EventHandle.actionButtons actionPressed = ActionButtonPressed();

            DruidJournal.journalTypes journalPressed = Journal.DruidJournal.JournalButtonPressed();

            if (Game1.activeClickableMenu is DruidJournal druidJournal)
            {

                if (journalPressed != DruidJournal.journalTypes.none)
                {

                    if (druidJournal.type == journalPressed)
                    {

                        druidJournal.exitThisMenu(true);

                    }
                    else
                    {

                        DruidJournal.openJournal(journalPressed);

                    }

                }
                else if (actionPressed == EventHandle.actionButtons.rite)
                {

                    druidJournal.exitThisMenu(true);

                }

                return;

            }

            if (!CasterBusy() && !CasterMenu()) 
            {

                // action press
                if (clickRegister.ContainsKey(actionPressed))
                {

                    bool fire = false;

                    for(int i = clickRegister[actionPressed].Count - 1; i >= 0; i--)
                    {

                        string eventClick = clickRegister[actionPressed].ElementAt(i);

                        if (!eventRegister.ContainsKey(eventClick))
                        {

                            clickRegister[actionPressed].Remove(eventClick);

                            continue;

                        }

                        if (eventRegister[eventClick].EventPerformAction(e.Button, actionPressed))
                        {

                            if (eventRegister[eventClick].eventComplete)
                            {

                                eventRegister[eventClick].EventRemove();

                                eventRegister.Remove(eventClick);

                                clickRegister[actionPressed].Remove(eventClick);

                            }

                            fire = true;

                        }

                    }

                    if (clickRegister[actionPressed].Count == 0)
                    {

                        clickRegister.Remove(actionPressed);

                    }

                    if (fire)
                    {

                        return;

                    }

                }

                if (!clickRegister.ContainsKey(actionPressed))
                {

                    rite.Click(actionPressed);

                }

                if(actionPressed == EventHandle.actionButtons.action)
                {

                    bool buffHover = Game1.buffsDisplay.isWithinBounds(Game1.getMouseX(true), Game1.getMouseY(true));

                    if (buffHover)
                    {

                        if (Game1.buffsDisplay.hoverText.Contains(StringData.Strings(StringData.stringkeys.riteBuffDescription)))
                        {
                            
                            DruidJournal.openJournal(journalPressed);

                        }

                    }

                }

            }
            else
            {
                
                rite.Shutdown();

            }

            if (Game1.activeClickableMenu != null)
            {

                if(Game1.activeClickableMenu is QuestLog questLog)
                {
                       
                   if(actionPressed == EventHandle.actionButtons.rite || journalPressed != DruidJournal.journalTypes.none)
                    {

                        questLog.exitThisMenu(true);

                        DruidJournal.openJournal(journalPressed);

                    }

                }

                rite.Shutdown();

                return;

            }
            else if (actionPressed == EventHandle.actionButtons.warp)
            {

                rite.Shutdown();

                RelicHandle.WarpFunction();

            }            
            else if (actionPressed == EventHandle.actionButtons.favourite)
            {

                rite.Shutdown();

                RelicHandle.QuickFunction();

            }

            if (journalPressed != DruidJournal.journalTypes.none)
            {

                DruidJournal.openJournal(journalPressed);
                
                rite.Shutdown();

            }

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

            SButtonState channelButtonState = Config.channelButtons.GetState();

            if (channelButtonState == SButtonState.Pressed)
            {

                return EventHandle.actionButtons.shift;

            }

            if (Config.warpButtons.GetState() == SButtonState.Pressed)
            {

                if(!magic && activeEvent.Count == 0)
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
            /*if (Helper.Input.GetState(SButton.LeftShift) == SButtonState.Held)
            {

                return true;

            }
            else if (Helper.Input.GetState(SButton.RightShift) == SButtonState.Held)
            {

                return true;

            }*/

            return false;

        }

        private void EveryTicked(object sender, UpdateTickedEventArgs e)
        {

            // Game is not ready
            if (!Context.IsWorldReady || !modReady)
            {

                return;

            }

            if (CasterGone())
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

            if (business)
            {

                return;

            }

            rite.Update();

            if (e.Ticks % 60 == 27)
            {

                rite.ToolBuff();

                rite.ChargeBuff();

                rite.RiteBuff();

                herbalData.CheckBuff();

            }

            //===========================================
            // Every tenth of a second

            if (e.IsMultipleOf(6))
            {

                EveryDecimal();

            }

        }

        public void EveryDecimal()
        {

            /*if (displayRegister.Count > 0)
            {

                int bh = 1;

                int dh = 1;

                for (int d = displayRegister.Count - 1; d >= 0; d--)
                {

                    EventDisplay display = displayRegister[d];

                    if (!display.update())
                    {

                        displayRegister[d].timeLeft = 1;

                        displayRegister.RemoveAt(d);

                    }
                    else
                    {

                        switch (display.type)
                        {

                            case EventDisplay.displayTypes.bar:

                                displayRegister[d].level = bh;

                                bh++;

                                break;

                            default:

                                if (dh == 4)
                                {

                                    displayRegister[d].timeLeft = 1;

                                    break;
                                }

                                displayRegister[d].level = dh;

                                dh++;

                                break;

                        }

                    }

                }

            }*/

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

            if (eventRegister.Count > 0)
            {

                for (int ev = eventRegister.Count - 1; ev >= 0; ev--)
                {

                    KeyValuePair<string, Event.EventHandle> eventEntry = eventRegister.ElementAt(ev);

                    if (eventEntry.Value.eventActive)
                    {

                        eventEntry.Value.EventDecimal();

                        if (eventEntry.Value.eventComplete)
                        {

                            eventRegister[eventEntry.Key].EventRemove();

                            eventRegister.Remove(eventEntry.Key);

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

        public bool CheckTrigger()
        {
            
            if (Game1.eventUp || Game1.currentMinigame != null || Game1.isWarping || Game1.killScreen)
            {

                return false;

            }

            if (activeEvent.Count > 0)
            {

                return false;
            
            }

            if (eventRegister.Count == 0)
            {

                return false;
            
            }

            foreach(KeyValuePair<string, EventHandle> trigger in eventRegister)
            {

                if (trigger.Value.triggerActive)
                {

                    if (trigger.Value.TriggerCheck())
                    {

                        return true;

                    }

                }

            }

            return false;

        }

        public void OnRenderingStep(object sender, RenderingStepEventArgs e)
        {

            if (e.Step != StardewValley.Mods.RenderSteps.HUD)
            {

                return;

            }

            if (!Context.IsWorldReady || !modReady)
            {

                return;

            }

            displayOffset = 124;

            barOffset = 300;

            for (int d = displayRegister.Count - 1; d >= 0; d--)
            {

                EventDisplay display = displayRegister.ElementAt(d);

                display.draw(e.SpriteBatch);

            }

        }

        public void OnRenderedStep(object sender, RenderedStepEventArgs e)
        {

            if (!Context.IsWorldReady || !modReady)
            {

                return;

            }

            switch (e.Step)
            {

                case StardewValley.Mods.RenderSteps.HUD:

                    herbalData.HoverBuff(e.SpriteBatch);

                    break;

                case StardewValley.Mods.RenderSteps.World_Sorted:


                    if (CasterGone())
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

        public void CastMessage(string message, int type = 0, bool ignore = false)
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

        public EventDisplay CastDisplay(string text, int index = 0)
        {

            EventDisplay display = new(index.ToString(), text, 7, EventDisplay.displayTypes.plain);

            if(displayRegister.Count >= 4)
            {

                EventDisplay displayFirst = displayRegister.First();

                displayFirst.count = display.time - 4;

            }

            displayRegister.Add(display);

           // Game1.addHUDMessage(display);

            return display;

        }
        
        public EventDisplay CastDisplay(string title, string text)
        {

            EventDisplay display = new(title, text, 7, EventDisplay.displayTypes.title);

            displayRegister.Add(display);

            //Game1.addHUDMessage(display);

            return display;

        }

        public int AttuneableWeapon()
        {

            if (HoldingBomb())
            {

                currentTool = 997;

                return 997;

            }
            else
            if (Game1.player.CurrentTool is null)
            {

                if ((Config.slotAttune || magic) && Config.slotFreedom)
                {

                    // valid tool not required
                    return 999;

                }

                if (Game1.player.CurrentToolIndex == Transform.toolPlaceholder && eventRegister.ContainsKey(Rite.eventTransform))
                {

                    if (eventRegister[Rite.eventTransform] is Cast.Ether.Transform transform)
                    {

                        return transform.attuneableIndex;

                    }

                }

                return -1;

            }

            switch (Game1.player.CurrentTool)
            {
                default:
                    return -1;
                case MeleeWeapon:
                    currentTool = Game1.player.CurrentTool.InitialParentTileIndex;
                    break;
                case Pickaxe:
                    currentTool = 991;
                    break;
                case Axe:
                    currentTool = 992;
                    break;
                case Hoe:
                    currentTool = 993;
                    break;
                case WateringCan:
                    currentTool = 994;
                    break;
                case FishingRod:
                    currentTool = 995;
                    break;
                case Pan:
                    currentTool = 996;
                    break;
            }

            return currentTool;

        }

        public static bool HoldingBomb()
        {

            if(Game1.player.CurrentItem == null)
            {

                return false;

            }

            switch (Game1.player.CurrentItem.ItemId)
            {

                case "286":
                case "287": 
                case "288":
                case "390":

                    return true;

            }

            return false;

        }

        public int CombatDamage()
        {

            if(DamagePing > (Game1.currentGameTime.TotalGameTime.TotalSeconds - 30.00))
            {

                return DamageLevel;

            }

            int damageLevel = 200;

            if (!Config.maxDamage)
            {

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

            }

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

            if (herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.alignment))
            {

                damageLevel += (int)(damageLevel * 0.1f * herbalData.buff.applied[HerbalBuff.herbalbuffs.alignment].level);

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

            if (herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.vigor))
            {

                critChance += (0.03f * herbalData.buff.applied[HerbalBuff.herbalbuffs.vigor].level);

                critModifier += (0.1f * herbalData.buff.applied[HerbalBuff.herbalbuffs.vigor].level);

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

            foreach(Companion companion in Game1.player.companions)
            {

                if (companion is HungryFrogCompanion frog)
                {

                    frog.fullnessTime = 12000f;

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

                DisableTrinkets();

            }

            if (eventRegister.ContainsKey(placeHolder))
            {

                if (eventRegister[placeHolder] == eventHandle) 
                { 
                    
                    return; 
                
                }

                if (eventRegister[placeHolder].AttemptReset())
                {

                    return;

                }

                eventRegister[placeHolder].EventRemove();

            }

            eventRegister[placeHolder] = eventHandle;

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

        public void EventQuery(QueryData querydata,QueryData.queries query)
        {

            Helper.Multiplayer.SendMessage<QueryData>(
                querydata,
                query.ToString(),
                new string[1] { ModManifest.UniqueID },
                null
            );

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

        public void CriticalCondition()
        {

            if (Context.IsMainPlayer)
            {

                if(activeEvent.Count == 0) { return; }

                AbortAllEvents();
                    
                CastMessage(StringData.Strings(StringData.stringkeys.challengeAborted), 3, true);

            }

        }

        public void AbortAllEvents()
        {

            foreach (KeyValuePair<string, Event.EventHandle> eventEntry in eventRegister)
            {

                eventRegister[eventEntry.Key].eventAbort = true;

            }

        }

        public bool AttuneWeapon(Rite.Rites blessing)
        {

            int toolIndex = AttuneableWeapon();

            if (toolIndex == -1) { return false; };

            save.attunement[toolIndex] = blessing;

            rite.Shutdown();

            SyncPreferences();

            return true;

        }

        public bool DetuneWeapon()
        {

            int toolIndex = AttuneableWeapon();

            if (toolIndex == -1) { return false; };

            if (save.attunement.ContainsKey(toolIndex))
            {

                save.attunement.Remove(toolIndex);

            }

            rite.Shutdown();

            SyncPreferences();

            return true;

        }

        public void AutoConsume()
        {

            ConsumeLunch();

            List<HerbalHandle.herbals> lines = new()
            {
                HerbalHandle.herbals.ligna,
                HerbalHandle.herbals.impes,
                HerbalHandle.herbals.celeri,
            };

            List<HerbalHandle.herbals> potions = new();

            List<HerbalHandle.herbals> prioritised = new();

            int max = herbalData.MaxHerbal();

            foreach (HerbalHandle.herbals line in lines)
            {

                for (int i = herbalData.lines[line].Count - 1; i >= 0; i--)
                {

                    bool priority = false;

                    HerbalHandle.herbals herbal = herbalData.lines[line][i];

                    if (herbalData.herbalism[herbal.ToString()].level > max)
                    {

                        continue;

                    }

                    if (save.potions.ContainsKey(herbal))
                    {

                        if (save.potions[herbal] == 0 || save.potions[herbal] == 4)
                        {

                            continue;

                        }

                        if (save.potions[herbal] == 2)
                        {

                            priority = true;

                        }

                    }

                    if (save.herbalism.ContainsKey(herbal))
                    {

                        if (save.herbalism[herbal] > 0)
                        {
                            if (priority)
                            {

                                prioritised.Add(herbal);

                            }
                            else
                            {
                                potions.Add(herbal);

                            }

                            break;

                        }

                    }

                }

            }

            prioritised.AddRange(potions);

            foreach (HerbalHandle.herbals potion in prioritised)
            {

                if (Game1.player.Stamina >= (Game1.player.MaxStamina * 0.5) && Game1.player.health >= (Game1.player.maxHealth * 0.5))
                {
                    break;
                }

                herbalData.ConsumeHerbal(potion.ToString());

            }

        }

        public void ConsumeLunch()
        {

            if (!Config.slotConsume)
            {
                return;
            }

            Dictionary<int, string> slots = new()
            {
                [0] = Config.slotOne,
                [1] = Config.slotTwo,
                [2] = Config.slotThree,
                [3] = Config.slotFour,
                [4] = Config.slotFive,
                [5] = Config.slotSix,
                [6] = Config.slotSeven,
                [7] = Config.slotEight,
                [8] = Config.slotNine,
                [9] = Config.slotTen,
                [10] = Config.slotEleven,
                [11] = Config.slotTwelve,

            };

            List<int> indexes = new()
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11,
            };

            if (Config.slotReverse)
            {

                indexes = new()
                {
                    11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0,
                };

            }

            foreach (int i in indexes)
            {

                if (slots[i] != "lunch")
                {

                    continue;

                }

                if (Game1.player.Stamina >= Game1.player.MaxStamina / 2 && Game1.player.health >= (Game1.player.maxHealth / 2))
                {

                    break;

                }

                Item checkSlot = Game1.player.Items[i];

                // ignore empty slots
                if (checkSlot == null || checkSlot is not StardewValley.Object checkItem)
                {

                    continue;

                }

                if (@checkItem.Edibility <= 0)
                {

                    continue;

                }

                int itemStamina = @checkItem.staminaRecoveredOnConsumption();

                if (itemStamina <= 0)
                {
                    return;
                }

                int itemHealth = @checkItem.healthRecoveredOnConsumption();

                if (itemHealth <= 0)
                {
                    return;
                }

                if (@checkItem.HasContextTag("ginger_item"))
                {
                    Game1.player.buffs.Remove("25");
                }

                foreach (Buff foodOrDrinkBuff in @checkItem.GetFoodOrDrinkBuffs())
                {
                    Game1.player.applyBuff(foodOrDrinkBuff);
                }

                if (@checkItem.QualifiedItemId == "(O)773")
                {
                    Game1.player.health = Game1.player.maxHealth;
                }
                else if (@checkItem.QualifiedItemId == "(O)351")
                {
                    Game1.player.exhausted.Value = false;
                }

                if (Game1.player.MaxStamina > Game1.player.Stamina)
                {

                    Game1.player.Stamina = Math.Min(Game1.player.MaxStamina, Game1.player.Stamina + itemStamina);

                }

                if (Game1.player.maxHealth > Game1.player.health)
                {

                    Game1.player.health = Math.Min(Game1.player.maxHealth, Game1.player.health + itemHealth);

                }

                Game1.player.Items[i].Stack -= 1;

                if (Game1.player.Items[i].Stack <= 0)
                {
                    Game1.player.Items[i] = null;

                }

                if (consumeBuffer < Game1.currentGameTime.TotalGameTime.TotalSeconds)
                {

                    ConsumeEdible hudmessage = new(checkItem.DisplayName + " " + @checkItem.staminaRecoveredOnConsumption().ToString() + " " + StringData.Strings(StringData.stringkeys.stamina), checkItem);

                    Game1.addHUDMessage(hudmessage);

                    consumeBuffer = Game1.currentGameTime.TotalGameTime.TotalSeconds + 5;

                }

                break;

            }

        }

    }

}

