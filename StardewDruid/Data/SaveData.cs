using Force.DeepCloner;
using StardewDruid.Cast;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewDruid.Location;
using StardewValley;
using StardewValley.BellsAndWhistles;
using System;
using System.Collections.Generic;

namespace StardewDruid.Data
{
    class SaveData
    {

        public int version = 0;

        public long id = 0;

        public Rite.Rites rite = Rite.Rites.none;

        public Dictionary<string, QuestProgress> progress = new();

        public Dictionary<CharacterHandle.characters, Character.Character.mode> characters = new();

        public Dictionary<CharacterHandle.characters, RecruitHandle> recruits = new();

        public Dictionary<CharacterHandle.characters, PalData> pals = new();

        public Dictionary<CharacterHandle.characters, List<ItemData>> chests = new();

        public Dictionary<CharacterHandle.characters, string> serialchests = new();

        public Dictionary<int,Rite.Rites> attunement = new();

        public Dictionary<HerbalHandle.herbals, int> herbalism = new();

        public Dictionary<HerbalHandle.herbals, int> potions = new();

        public Dictionary<ExportHandle.exports, int> exports = new();

        public Dictionary<int, ExportOrder> orders = new();

        public Dictionary<string, int> dialogues = new();

        public Dictionary<string, int> restoration = new();

        public Dictionary<string, int> reliquary = new();

        public QuestHandle.milestones milestone = QuestHandle.milestones.none;

        public Dictionary<long, MultiplayerData> multiplayer = new();

        public int set = 0;

        public string serialise = string.Empty;

        public SaveData()
        {

            // Player specific

            version = Mod.instance.version;

            id = Game1.player.UniqueMultiplayerID;

        }

    }

    class ItemData
    {

        public string id;

        public int quality;

        public int stack;

        public ItemData()
        {

        }

    }

    class MultiplayerData
    {

        public Dictionary<int, Rite.Rites> attunement = new();

        public Dictionary<HerbalHandle.herbals, int> herbalism = new();

        public Dictionary<HerbalHandle.herbals, int> potions = new();

        public Dictionary<CharacterHandle.characters, PalData> pals = new();

        public Dictionary<int, ExportOrder> orders = new();

        public MultiplayerData()
        {

        }

    }

}
