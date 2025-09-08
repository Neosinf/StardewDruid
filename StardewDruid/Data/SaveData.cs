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

        public const string version = "SD500";

        public long id = 0;

        public Rite.Rites rite = Rite.Rites.none;

        public Dictionary<string, QuestProgress> progress = new();

        public Dictionary<string, CompanionData> companions = new();

        public Dictionary<ChestHandle.chests, string> chests = new();

        public Dictionary<string, string> locations = new();

        public Dictionary<string,Rite.Rites> attunement = new();

        public Dictionary<ApothecaryHandle.items, ApothecaryRecord> apothecary = new();

        public Dictionary<ExportResource.resources, int> resources = new();

        public Dictionary<ExportGood.goods, int> goods = new();

        public Dictionary<ExportMachine.machines, MachineRecord> machines = new();

        public Dictionary<ExportGuild.guilds, GuildRecord> guilds = new();

        public Dictionary<int, ExportOrder> orders = new();

        public Dictionary<string, int> dialogues = new();

        public Dictionary<string, int> restoration = new();

        public Dictionary<string, int> reliquary = new();

        public int experience = 0;

        public Dictionary<MasteryNode.nodes, int> masteries = new();

        // Redundant

        public QuestHandle.milestones milestone = QuestHandle.milestones.none;

        public Dictionary<CharacterHandle.characters, Character.Character.mode> characters = new();

        public Dictionary<CharacterHandle.characters, RecruitHandle> recruits = new();

        public Dictionary<CharacterHandle.characters, PalData> pals = new();


        public SaveData()
        {

            // Player specific

            id = Game1.player.UniqueMultiplayerID;

            attunement = SpawnData.WeaponAttunement();

        }

    }


}
