
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event.Challenge;
using StardewDruid.Journal;
using StardewDruid.Location.Druid;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Events;
using StardewValley.GameData.Characters;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.Quests;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;


namespace StardewDruid.Handle
{
    public class RelicHandle
    {

        public enum relicsets
        {

            none,
            companion,
            lantern,
            wayfinder,
            druid,
            monsterstones,
            crests,
            herbalism,
            tactical,
            runestones,
            avalant,
            books,
            boxes,
            restore,
            skulls,
            shadowtin,
        
        }

        public Dictionary<relicsets, List<IconData.relics>> lines = new()
        {
            [relicsets.companion] = new() {
                IconData.relics.companion_crest,
                IconData.relics.companion_badge,
                IconData.relics.companion_dice,
                IconData.relics.companion_tome,
                IconData.relics.companion_glove,
                IconData.relics.stardew_druid,
            },
            [relicsets.wayfinder] = new() {
                IconData.relics.wayfinder_stone,
                IconData.relics.wayfinder_key,
                IconData.relics.wayfinder_glove,
                IconData.relics.wayfinder_eye,
            },
            [relicsets.lantern] = new() {
                IconData.relics.lantern_pot,
                IconData.relics.lantern_censer,
                IconData.relics.lantern_guardian,
                IconData.relics.lantern_water,
                IconData.relics.lantern_ceremony,
            },
            [relicsets.druid] = new() {
                IconData.relics.druid_grimoire,
                IconData.relics.druid_runeboard,
                IconData.relics.druid_hammer,
                IconData.relics.druid_hieress,
                IconData.relics.druid_dragonomicon,
            },
            [relicsets.monsterstones] = new() {
                IconData.relics.monster_bat,
                IconData.relics.monster_slime,
                IconData.relics.monster_spirit,
                IconData.relics.monster_ghost,
                IconData.relics.monster_serpent,
            },
            [relicsets.crests] = new() {
                IconData.relics.crest_church,
                IconData.relics.crest_dwarf,
                IconData.relics.crest_associate,
                IconData.relics.crest_smuggler,
            },
            [relicsets.herbalism] = new() {
                IconData.relics.herbalism_mortar,
                IconData.relics.herbalism_pan,
                IconData.relics.herbalism_still,
                IconData.relics.herbalism_crucible,
                IconData.relics.herbalism_gauge,
            },
            [relicsets.tactical] = new() {
                IconData.relics.tactical_discombobulator,
                IconData.relics.tactical_mask,
                IconData.relics.tactical_cell,
                IconData.relics.tactical_lunchbox,
                IconData.relics.tactical_peppermint,
            },
            [relicsets.runestones] = new() {
                IconData.relics.runestones_spring,
                IconData.relics.runestones_farm,
                IconData.relics.runestones_moon,
                IconData.relics.runestones_cat,
                IconData.relics.runestones_alchemistry,
            },
            [relicsets.avalant] = new() {
                IconData.relics.avalant_disc,
                IconData.relics.avalant_chassis,
                IconData.relics.avalant_gears,
                IconData.relics.avalant_casing,
                IconData.relics.avalant_needle,
                IconData.relics.avalant_measure,
            },
            [relicsets.books] = new() {
                IconData.relics.book_wyrven,
                IconData.relics.book_letters,
                IconData.relics.book_manual,
                IconData.relics.book_druid,
                IconData.relics.book_annal,
                IconData.relics.book_knight,
            },
            [relicsets.boxes] = new() {
                IconData.relics.box_measurer,
                IconData.relics.box_mortician,
                IconData.relics.box_artisan,
                IconData.relics.box_chaos,
            },
            [relicsets.restore] = new() {
                IconData.relics.restore_goshuin,
                IconData.relics.restore_offering,
                IconData.relics.restore_cloth,
            },
            [relicsets.skulls] = new() {
                IconData.relics.skull_saurus,
                IconData.relics.skull_gelatin,
                IconData.relics.skull_cannoli,
                IconData.relics.skull_fox,
                IconData.relics.golden_core,
            },
            [relicsets.shadowtin] = new() {
                IconData.relics.shadowtin_cell,
                IconData.relics.shadowtin_bazooka,
            },
        };

        public Dictionary<relicsets, string> quests = new()
        {
            [relicsets.tactical] = QuestHandle.relicTactical,
            [relicsets.runestones] = QuestHandle.relicWeald,
            [relicsets.avalant] = QuestHandle.relicMists,
            [relicsets.books] = QuestHandle.relicEther,
            [relicsets.boxes] = QuestHandle.relicFates,
            [relicsets.restore] = QuestHandle.relicRestore,
        };

        public Dictionary<string, Relic> reliquary = new();

        public Dictionary<relicsets, List<string>> titles = new()
        {

            [relicsets.companion] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.6"),
                Mod.instance.Helper.Translation.Get("RelicData.7"), },
            [relicsets.lantern] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.9"),
                Mod.instance.Helper.Translation.Get("RelicData.10"), },
            [relicsets.wayfinder] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.27"),
                Mod.instance.Helper.Translation.Get("RelicData.28"), },
            [relicsets.druid] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.500.druidrelics.1"),
                Mod.instance.Helper.Translation.Get("RelicData.500.druidrelics.2"), },
            [relicsets.monsterstones] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.386.8"),
                Mod.instance.Helper.Translation.Get("RelicData.386.9"), },
            [relicsets.crests] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.386.30"),
                Mod.instance.Helper.Translation.Get("RelicData.386.31"), },
            [relicsets.herbalism] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.12"),
                Mod.instance.Helper.Translation.Get("RelicData.13"), },
            [relicsets.tactical] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.309.1"),
                Mod.instance.Helper.Translation.Get("RelicData.309.2"), },
            [relicsets.runestones] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.15"),
                Mod.instance.Helper.Translation.Get("RelicData.16"), },
            [relicsets.avalant] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.18"),
                Mod.instance.Helper.Translation.Get("RelicData.19"), },
            [relicsets.books] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.21"),
                Mod.instance.Helper.Translation.Get("RelicData.22"), },
            [relicsets.boxes] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.24"),
                Mod.instance.Helper.Translation.Get("RelicData.25"), },
            [relicsets.restore] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.329.5"),
                Mod.instance.Helper.Translation.Get("RelicData.329.6"), },
            [relicsets.skulls] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.339.1"),
                Mod.instance.Helper.Translation.Get("RelicData.339.2"), },
            [relicsets.shadowtin] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.500.shadowtinrelics.1"),
                Mod.instance.Helper.Translation.Get("RelicData.500.shadowtinrelics.2"), },

        };


        public IconData.relics favourite = IconData.relics.none;

        public RelicHandle()
        {

        }

        public void LoadRelics()
        {

            if (Mod.instance.magic)
            {

                return;

            }

            reliquary = RelicData.RelicsList();

        }

        public static bool HasRelic(IconData.relics relic)
        {

            if (Mod.instance.magic)
            {

                return true;

            }

            if (Mod.instance.save.reliquary.ContainsKey(relic.ToString()))
            {

                return true;

            }

            return false;

        }

        public Dictionary<int, ContentComponent> JournalRelics()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            foreach (KeyValuePair<relicsets,List<IconData.relics>> set in lines)
            {

                Dictionary<IconData.relics, bool> setDisplay = new();

                bool setActive = false;

                foreach (IconData.relics relicName in set.Value)
                {

                    if (!HasRelic(relicName))
                    {

                        if (reliquary[relicName.ToString()].hint == null)
                        {

                            continue;

                        }

                        setDisplay.Add(relicName, false);

                    }
                    else
                    {

                        setDisplay.Add(relicName, true);

                        setActive = true;

                    }
                    
                }

                if (!setActive)
                {

                    continue;

                }

                foreach (IconData.relics relicName in set.Value)
                {

                    ContentComponent content = new(ContentComponent.contentTypes.relic, relicName.ToString());

                    if (!setDisplay.ContainsKey(relicName))
                    {

                        content.active = false;

                        continue;

                    }
                    else
                    {

                        content.textureSources[0] = IconData.RelicRectangles(relicName);

                        if (setDisplay[relicName])
                        {

                            content.textureColours[0] = Color.White;

                        }
                        else
                        {

                            content.textureColours[0] = Color.Black * 0.01f;

                        }

                    }

                    journal[start++] = content;

                }

                for (int i = 0; i < 6 - set.Value.Count; i++)
                {

                    ContentComponent content = new(ContentComponent.contentTypes.relic, set.ToString() + i.ToString())
                    {
                        active = false
                    };

                    journal[start++] = content;

                }

            }

            return journal;

        }

        public Dictionary<int, ContentComponent> JournalHeaders()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            foreach (relicsets set in lines.Keys)
            {

                switch (set)
                {

                    case relicsets.runestones:

                        if (!HasRelic(lines[set][0]) && !HasRelic(lines[set][4]))
                        {

                            continue;

                        }

                        break;

                    case relicsets.avalant:
                    case relicsets.restore:


                        if (ProgressRelicQuest(set) == 0)
                        {

                            continue;

                        }
                        break;

                    default:

                        if (!HasRelic(lines[set][0]))
                        {

                            continue;

                        }

                        break;

                }

                ContentComponent content = new(ContentComponent.contentTypes.header, set.ToString());

                content.text[0] = titles[set][0];

                content.text[1] = titles[set][1];

                journal[start++] = content;

            }

            return journal;

        }

        public void ReliquaryUpdate(string id)
        {

            Mod.instance.save.reliquary[id] = 1;

        }

        public IconData.relics RelicTacticalLocations()
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeWeald) || !Context.IsMainPlayer)
            {

                return IconData.relics.none;

            }

            if (Game1.player.currentLocation is Forest || Game1.player.currentLocation.Name.Contains("Custom_Forest"))
            {

                return IconData.relics.tactical_cell;

            }
            else if (Game1.player.currentLocation is Woods || Game1.player.currentLocation.Name.Contains("Custom_Woods"))
            {

                return IconData.relics.tactical_mask;

            }
            else if (Game1.player.currentLocation is FarmCave)
            {

                return IconData.relics.tactical_peppermint;

            }
            else if (Game1.player.currentLocation is BusStop || Game1.player.currentLocation.Name.Equals("Backwoods") || Game1.player.currentLocation.Name.Equals("Tunnel"))
            {

                return IconData.relics.tactical_lunchbox;

            }

            return IconData.relics.none;

        }

        public IconData.relics RelicMistsLocations()
        {

            if (!Context.IsMainPlayer)
            {

                return IconData.relics.none;

            }

            if (Game1.player.currentLocation is Forest)
            {

                return IconData.relics.avalant_disc;

            }
            else if (Game1.player.currentLocation is Mountain)
            {

                return IconData.relics.avalant_chassis;

            }
            else if (Game1.player.currentLocation is Beach)
            {

                return IconData.relics.avalant_gears;

            }
            else if (Game1.player.currentLocation is MineShaft || Game1.player.currentLocation is Spring)
            {

                return IconData.relics.avalant_casing;

            }
            else if (Game1.player.currentLocation is Town)
            {

                return IconData.relics.avalant_needle;

            }
            else if (Game1.player.currentLocation is Atoll)
            {

                return IconData.relics.avalant_measure;

            }

            return IconData.relics.none;

        }

        public IconData.relics RelicBooksLocations()
        {

            if (!Context.IsMainPlayer)
            {

                return IconData.relics.none;

            }

            if (Game1.player.currentLocation is Forest)
            {

                return IconData.relics.book_letters;

            }
            else if (Game1.player.currentLocation is Mountain)
            {

                return IconData.relics.book_manual;

            }
            else if (Game1.player.currentLocation is Atoll)
            {

                return IconData.relics.book_druid;

            }
            else if (Game1.player.currentLocation is Desert)
            {

                return IconData.relics.book_annal;

            }
            return IconData.relics.none;

        }

        public int ArtisanRelicQuest()
        {

            if (!Context.IsMainPlayer)
            {

                return -1;

            }

            if (HasRelic(IconData.relics.box_artisan))
            {

                return -1;

            }

            for (int i = 0; i < Game1.player.Items.Count; i++)
            {

                Item checkSlot = Game1.player.Items[i];

                // ignore empty slots
                if (checkSlot == null || checkSlot is not Tool toolCheck)
                {

                    continue;

                }

                if (toolCheck.UpgradeLevel >= 4)
                {

                    return 1;

                }

            }

            return 0;

        }

        public int MorticianRelicQuest()
        {

            if (!Context.IsMainPlayer)
            {

                return -1;

            }

            if (HasRelic(IconData.relics.box_mortician))
            {

                return -1;

            }

            for(int i = 0; i < 18; i++)
            {

                HerbalHandle.herbals herbal = (HerbalHandle.herbals)((int)HerbalHandle.herbals.omen_feather + i);

                if (HerbalHandle.GetHerbalism(herbal) <= 0)
                {

                    return 0;

                }


            }

            return 1;

        }

        public int ChaosRelicQuest()
        {
            if (!Context.IsMainPlayer)
            {

                return -1;

            }

            if (HasRelic(IconData.relics.box_chaos))
            {

                return -1;

            }

            foreach(GameLocation location in Game1.locations)
            {

                if(location.buildings.Count > 0)
                {

                    foreach (Building building in location.buildings)
                    {

                        if (building.buildingType.Contains("Deluxe Coop") || building.buildingType.Contains("Deluxe Barn"))
                        {

                            return 1;

                        }

                    }

                }

            }

            return 0;


        }

        public int ProgressRelicQuest(relicsets relicset)
        {

            if (!Context.IsMainPlayer)
            {

                return -1;

            }

            if (Mod.instance.questHandle.IsComplete(quests[relicset]))
            {

                return -1;

            }

            int count = 0;

            foreach (IconData.relics relic in lines[relicset])
            {

                //if (relic == IconData.relics.book_knight)
                //{

                //    continue;

                //}

                if (relic == IconData.relics.runestones_alchemistry)
                {

                    continue;

                }

                if (HasRelic(relic))
                {

                    count++;

                }

            }

            return count;

        }


    }

}
