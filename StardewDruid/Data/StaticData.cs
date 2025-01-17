﻿using Force.DeepCloner;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Event;
using StardewDruid.Location;
using StardewValley;
using StardewValley.BellsAndWhistles;
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

        public Dictionary<CharacterHandle.characters, RecruitData> recruits;

        public Dictionary<CharacterHandle.characters, List<ItemData>> chests;

        public Dictionary<int,Rite.rites> attunement;

        public Dictionary<HerbalData.herbals, int> herbalism;

        public Dictionary<HerbalData.herbals, int> potions;

        public Dictionary<string, int> restoration;

        public Dictionary<string, int> reliquary;

        public QuestHandle.milestones milestone;

        public Dictionary<long, MultiplayerData> multiplayer;

        public int set;

        public string serialise;

        public StaticData()
        {

            // Player specific

            version = Mod.instance.version;

            id = Game1.player.UniqueMultiplayerID;

            rite = Rite.rites.none;

            attunement = new();

            herbalism = new();

            potions = new();

            // Overwrittenn

            progress = new();

            reliquary = new();

            milestone = QuestHandle.milestones.none;

            // Host only

            characters = new();

            recruits = new();

            chests = new();

            restoration = new();

            set = 0;

            serialise = string.Empty;

            multiplayer = new();

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

        public Dictionary<int, Rite.rites> attunement;

        public Dictionary<HerbalData.herbals, int> herbalism;

        public Dictionary<HerbalData.herbals, int> potions;

        public MultiplayerData()
        {

        }

    }

    public class RecruitData
    {

        public string name;

        public string display;

        public int level;

        public Rite.rites rite;

        public RecruitData()
        {

        }

    }

}
