using Force.DeepCloner;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Journal;
using StardewValley;
using System;
using System.Collections.Generic;

namespace StardewDruid.Data
{
    class StaticData
    {

        public static int version;

        public long id;

        public Rite.rites rite;

        public Dictionary<string, QuestProgress> progress;

        public Dictionary<CharacterHandle.characters, Character.Character.mode> characters;

        public Dictionary<CharacterHandle.characters, List<ItemData>> chests;

        public Dictionary<int,Rite.rites> attunement;

        public Dictionary<HerbalData.herbals, int> herbalism;

        public Dictionary<HerbalData.herbals, int> potions;

        public Dictionary<string, int> reliquary;

        public QuestHandle.milestones milestone;

        public int set;

        public StaticData()
        {

            version = Mod.instance.version;

            id = Game1.player.UniqueMultiplayerID;

            rite = Rite.rites.none;

            progress = new();

            characters = new();

            chests = new();

            attunement = new();

            herbalism = new();

            potions = new();

            reliquary = new();

            milestone = QuestHandle.milestones.none;

            set = 0;

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

}
