
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Dialogue;
using StardewDruid.Event.Challenge;
using StardewDruid.Handle;
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


namespace StardewDruid.Data
{
    public class RelicData
    {

        public enum relicsets
        {

            none,
            companion,
            wayfinder,
            other,
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
            testaments,
            druid,
        
        }

        public Dictionary<relicsets, List<IconData.relics>> lines = new()
        {
            [relicsets.companion] = new() {
                IconData.relics.effigy_crest,
                IconData.relics.jester_dice,
                IconData.relics.shadowtin_tome,
                IconData.relics.dragon_form,
                IconData.relics.blackfeather_glove,
                IconData.relics.heiress_gift,
            },
            [relicsets.wayfinder] = new() {
                IconData.relics.wayfinder_pot,
                IconData.relics.wayfinder_censer,
                IconData.relics.wayfinder_lantern,
                IconData.relics.wayfinder_water,
                IconData.relics.wayfinder_ceremonial,
                IconData.relics.wayfinder_dwarf,
            },
            [relicsets.other] = new() {
                IconData.relics.wayfinder_stone,
                IconData.relics.crow_hammer,
                IconData.relics.wayfinder_key,
                IconData.relics.wayfinder_glove,
                IconData.relics.wayfinder_eye,
            },
            [relicsets.monsterstones] = new() {
                IconData.relics.monsterbadge,
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
            [relicsets.testaments] = new() {
                IconData.relics.book_knight,
            },
            [relicsets.druid] = new() {
                IconData.relics.stardew_druid,
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
            [relicsets.wayfinder] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.9"),
                Mod.instance.Helper.Translation.Get("RelicData.10"), },
            [relicsets.other] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.27"),
                Mod.instance.Helper.Translation.Get("RelicData.28"), },
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
            [relicsets.testaments] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.386.6"),
                Mod.instance.Helper.Translation.Get("RelicData.386.7"), },
            [relicsets.druid] = new() {
                Mod.instance.Helper.Translation.Get("RelicData.361.6"),
                Mod.instance.Helper.Translation.Get("RelicData.361.7"), },
        };


        public IconData.relics favourite = IconData.relics.none;

        public RelicData()
        {

        }

        public void LoadRelics()
        {

            if (Mod.instance.magic)
            {

                return;

            }

            reliquary = RelicsList();

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

                for (int i = 0; i < lines[set].Count; i++)
                {

                    IconData.relics relicName = lines[set][i];

                    ContentComponent content = new(ContentComponent.contentTypes.relic, relicName.ToString());

                    content.textureSources[0] = IconData.RelicRectangles(relicName);

                    content.textureColours[0] = Color.White;

                    if (!HasRelic(relicName))
                    {

                        switch (set)
                        {

                            case relicsets.runestones:

                                if (relicName == IconData.relics.runestones_alchemistry)
                                {

                                    content.active = false;

                                }

                                content.textureColours[0] = Color.Black * 0.01f;

                                break;

                            case relicsets.tactical:
                            case relicsets.avalant:
                            case relicsets.boxes:
                            case relicsets.restore:

                                content.textureColours[0] = Color.Black * 0.01f;

                                break;

                            case relicsets.books:

                                if (relicName == IconData.relics.book_knight)
                                {

                                    content.active = false;

                                    break;
                                }

                                content.textureColours[0] = Color.Black * 0.01f;

                                break;

                            default:

                                content.active = false;

                                break;

                        }

                    }

                    journal[start++] = content;

                }

                for (int i = 0; i < 6 - lines[set].Count; i++)
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

            /*List<string> boneItems = new()
            {
                "(O)579", //"Prehistoric Scapula
                "(O)580", //"Prehistoric Tibia/100/-300/Arch/Prehistoric Tibia/A thick and sturdy leg bone./Forest .01//",
                "(O)581", //"Prehistoric Skull/100/-300/Arch/Prehistoric Skull/This is definitely a mammalian skull./Mountain .01//",
                //"(O)582", //"Skeletal Hand/100/-300/Arch/Skeletal Hand/It's a wonder all these ancient little pieces lasted so long./Beach .01//",
                "(O)583", //"Prehistoric Rib/100/-300/Arch/Prehistoric Rib/Little gouge marks on the side suggest that this rib was someone's dinner./Farm .01//",
                "(O)584", //"Prehistoric Vertebra/100/-300/Arch/Prehistoric Vertebra/A segment of some prehistoric creature's spine./BusStop .01//",
                //"(O)585", //"Skeletal Tail/100/-300/Arch/Skeletal Tail/It's pretty short for a tail./UndergroundMine .01//",
                "(O)820", //"Fossilized Skull/100/-300/Basic/Fossilized Skull/It's a perfect specimen!/Island_North .01//",
                "(O)821", //"Fossilized Spine/100/-300/Basic/Fossilized Spine/A column of interlocking vertebrae./Island_North .01//",
                "(O)822", //"Fossilized Tail/100/-300/Basic/Fossilized Tail/This tail has a club-like feature at the tip./Island_North .01//",
                "(O)823", //"Fossilized Leg/100/-300/Basic/Fossilized Leg/A thick and sturdy leg bone./Island_North .01//",
                "(O)824", //"Fossilized Ribs/100/-300/Basic/Fossilized Ribs/Long ago, these ribs protected the body of a large animal./Island_North .01//",
                //"(O)825", //"Snake Skull/100/-300/Basic/Snake Skull/A preserved skull that once belonged to a snake./Island_North .01//",
                //"(O)826", //"Snake Vertebrae/100/-300/Basic/Snake Vertebrae/It appears this serpent may have been extremely flexible./Island_North .01//",
                //"(O)119", //119: "Bone Flute/100/-300/Arch/Bone Flute/It's a prehistoric wind instrument carved from an animal's bone. It produces an eerie tone./Mountain .01 Forest .01 UndergroundMine .02 Town .005/Recipe 2 Flute_Block 150/",
            };

            for (int i = 0; i < Game1.player.Items.Count; i++)
            {

                Item checkSlot = Game1.player.Items[i];

                // ignore empty slots
                if (checkSlot == null || checkSlot is not StardewValley.Object itemCheck)
                {

                    continue;

                }

                if (boneItems.Contains(itemCheck.QualifiedItemId))
                {

                    return 1;

                }

            }

            return 0;*/
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

        public static Dictionary<string, Relic> RelicsList()
        {

            Dictionary<string, Relic> relics = new();

            // ====================================================================
            // Companion Tokens

            relics[IconData.relics.effigy_crest.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.41"),
                relic = IconData.relics.effigy_crest,
                line = relicsets.companion,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.46"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.49"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.51"),
            };

            relics[IconData.relics.jester_dice.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.56"),
                relic = IconData.relics.jester_dice,
                line = relicsets.companion,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.61"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.64"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.66"),
            };

            relics[IconData.relics.shadowtin_tome.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.71"),
                relic = IconData.relics.shadowtin_tome,
                line = relicsets.companion,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.76"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.79"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.81"),
            };

            relics[IconData.relics.dragon_form.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.86"),
                relic = IconData.relics.dragon_form,
                line = relicsets.companion,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.90"),
                details = new(){
                    Mod.instance.Helper.Translation.Get("RelicData.715"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.91"),
            };

            relics[IconData.relics.blackfeather_glove.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.323.1"),
                relic = IconData.relics.blackfeather_glove,
                line = relicsets.companion,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.323.2"),
                details = new(){
                    Mod.instance.Helper.Translation.Get("RelicData.323.3"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.323.4"),
            };

            relics[IconData.relics.heiress_gift.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.361.1"),
                relic = IconData.relics.heiress_gift,
                line = relicsets.companion,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.361.2"),
                details = new(){
                    Mod.instance.Helper.Translation.Get("RelicData.361.3"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.361.4"),
            };

            if (Context.IsMainPlayer)
            {

                relics[IconData.relics.effigy_crest.ToString()].details.Add(Mod.instance.Helper.Translation.Get("RelicData.665"));
                relics[IconData.relics.jester_dice.ToString()].details.Add(Mod.instance.Helper.Translation.Get("RelicData.676"));
                relics[IconData.relics.shadowtin_tome.ToString()].details.Add(Mod.instance.Helper.Translation.Get("RelicData.687"));
                relics[IconData.relics.blackfeather_glove.ToString()].details.Add(Mod.instance.Helper.Translation.Get("RelicData.323.11"));
                relics[IconData.relics.heiress_gift.ToString()].details.Add(Mod.instance.Helper.Translation.Get("RelicData.361.5"));

            }

            // ====================================================================
            // Wayfinder relics

            relics[IconData.relics.wayfinder_pot.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.309.3"),
                relic = IconData.relics.wayfinder_pot,
                line = relicsets.wayfinder,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.309.4"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.309.5"),
                    Mod.instance.Helper.Translation.Get("RelicData.309.7"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.309.6"),
            };

            relics[IconData.relics.wayfinder_censer.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.115"),
                relic = IconData.relics.wayfinder_censer,
                line = relicsets.wayfinder,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.119"),
                details = new() { Mod.instance.Helper.Translation.Get("RelicData.691"), },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.120"),
            };

            relics[IconData.relics.wayfinder_lantern.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.125"),
                relic = IconData.relics.wayfinder_lantern,
                line = relicsets.wayfinder,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.130"),
                details = new() { Mod.instance.Helper.Translation.Get("RelicData.695"), },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.131"),
            };

            relics[IconData.relics.wayfinder_water.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.136"),
                relic = IconData.relics.wayfinder_water,
                line = relicsets.wayfinder,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.141"),
                details = new() { Mod.instance.Helper.Translation.Get("RelicData.699"), },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.142"),
            };

            relics[IconData.relics.wayfinder_ceremonial.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.161"),
                relic = IconData.relics.wayfinder_ceremonial,
                line = relicsets.wayfinder,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.166"),
                details = new() { Mod.instance.Helper.Translation.Get("RelicData.707"), },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.167"),
            };

            relics[IconData.relics.wayfinder_dwarf.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.172"),
                relic = IconData.relics.wayfinder_dwarf,
                line = relicsets.wayfinder,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.177"),
                details = new() { Mod.instance.Helper.Translation.Get("RelicData.711"), },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.178"),
            };

            // ======================================================================
            // Other wayfinder relics

            relics[IconData.relics.wayfinder_stone.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.313.1"),
                relic = IconData.relics.wayfinder_stone,
                line = relicsets.other,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.313.2"),
                details = new(){
                    Mod.instance.Helper.Translation.Get("RelicData.313.3"),
                    Mod.instance.Helper.Translation.Get("RelicData.313.4"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.313.5"),
            };

            relics[IconData.relics.crow_hammer.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.630"),
                relic = IconData.relics.crow_hammer,
                line = relicsets.other,
                description = Mod.instance.Helper.Translation.Get("RelicData.633"),
                details = new(){
                    Mod.instance.Helper.Translation.Get("RelicData.635"),
                    Mod.instance.Helper.Translation.Get("RelicData.636"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.638"),
            };

            relics[IconData.relics.wayfinder_key.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.311.1"),
                relic = IconData.relics.wayfinder_key,
                line = relicsets.other,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.311.2"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.311.3"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.311.4"),

            };

            relics[IconData.relics.wayfinder_glove.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.312.1"),
                relic = IconData.relics.wayfinder_glove,
                line = relicsets.other,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.312.2"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.312.3"),
                    Mod.instance.Helper.Translation.Get("RelicData.312.4"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.312.5"),

            };

            relics[IconData.relics.wayfinder_eye.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.147"),
                relic = IconData.relics.wayfinder_eye,
                line = relicsets.other,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.151"),
                details = new()
                {Mod.instance.Helper.Translation.Get("RelicData.703"),
                    Mod.instance.Helper.Translation.Get("RelicData.154"),
                    Mod.instance.Helper.Translation.Get("RelicData.155"),
                    Mod.instance.Helper.Translation.Get("RelicData.346.1"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.156"),
            };

            // ====================================================================
            // Monsterball Relics

            relics[IconData.relics.monsterbadge.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.386.26"),
                relic = IconData.relics.monsterbadge,
                line = relicsets.monsterstones,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.386.27"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.386.28"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.386.29"),
            };

            relics[IconData.relics.monster_bat.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.386.10"),
                relic = IconData.relics.monster_bat,
                line = relicsets.monsterstones,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.386.11"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.386.12"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.386.13"),
            };

            relics[IconData.relics.monster_slime.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.386.14"),
                relic = IconData.relics.monster_slime,
                line = relicsets.monsterstones,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.386.15"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.386.16"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.386.17"),
            };

            relics[IconData.relics.monster_spirit.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.386.18"),
                relic = IconData.relics.monster_spirit,
                line = relicsets.monsterstones,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.386.19"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.386.20"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.386.21"),
            };

            relics[IconData.relics.monster_ghost.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.390.1"),
                relic = IconData.relics.monster_ghost,
                line = relicsets.monsterstones,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.390.2"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.390.3"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.390.4"),
            };

            relics[IconData.relics.monster_serpent.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.386.22"),
                relic = IconData.relics.monster_serpent,
                line = relicsets.monsterstones,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.386.23"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.386.24"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.386.25"),
            };


            // ====================================================================
            // Crest Relics

            relics[IconData.relics.crest_church.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.386.32"),
                relic = IconData.relics.crest_church,
                line = relicsets.crests,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.386.33"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.386.34"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.386.35"),
            };

            relics[IconData.relics.crest_dwarf.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.386.36"),
                relic = IconData.relics.crest_dwarf,
                line = relicsets.crests,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.386.37"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.386.34"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.386.38"),
            };

            relics[IconData.relics.crest_associate.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.386.39"),
                relic = IconData.relics.crest_associate,
                line = relicsets.crests,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.386.40"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.386.34"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.386.41"),
            };

            relics[IconData.relics.crest_smuggler.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.386.42"),
                relic = IconData.relics.crest_smuggler,
                line = relicsets.crests,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.386.43"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.386.34"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.386.44"),
            };

            // ====================================================================
            // Tactical Relics

            relics[IconData.relics.tactical_discombobulator.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.310.1"),
                relic = IconData.relics.tactical_discombobulator,
                line = relicsets.tactical,
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.310.2"),
                },
                description = Mod.instance.Helper.Translation.Get("RelicData.310.3"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.310.4"),
            };

            relics[IconData.relics.tactical_mask.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.310.5"),
                relic = IconData.relics.tactical_mask,
                line = relicsets.tactical,
                description = Mod.instance.Helper.Translation.Get("RelicData.310.6"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.310.7"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.310.8"),
            };

            relics[IconData.relics.tactical_cell.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.310.9"),
                relic = IconData.relics.tactical_cell,
                line = relicsets.tactical,
                description = Mod.instance.Helper.Translation.Get("RelicData.310.10"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.310.11"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.310.12"),
            };

            relics[IconData.relics.tactical_lunchbox.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.310.13"),
                relic = IconData.relics.tactical_lunchbox,
                line = relicsets.tactical,
                description = Mod.instance.Helper.Translation.Get("RelicData.310.14"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.310.15"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.310.16"),
            };

            relics[IconData.relics.tactical_peppermint.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.310.17"),
                relic = IconData.relics.tactical_peppermint,
                line = relicsets.tactical,
                description = Mod.instance.Helper.Translation.Get("RelicData.310.18"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.310.19"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.310.20"),
            };

            // ====================================================================
            // Herbalism Relics

            relics[IconData.relics.herbalism_mortar.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.186"),
                relic = IconData.relics.herbalism_mortar,
                line = relicsets.herbalism,
                description = Mod.instance.Helper.Translation.Get("RelicData.189"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.192"),
                    Mod.instance.Helper.Translation.Get("RelicData.193")
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.195"),
            };

            relics[IconData.relics.herbalism_pan.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.200"),
                relic = IconData.relics.herbalism_pan,
                line = relicsets.herbalism,
                description = Mod.instance.Helper.Translation.Get("RelicData.203"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.206"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.208"),
            };

            relics[IconData.relics.herbalism_still.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.213"),
                relic = IconData.relics.herbalism_still,
                line = relicsets.herbalism,
                description = Mod.instance.Helper.Translation.Get("RelicData.216"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.219"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.221"),
            };

            relics[IconData.relics.herbalism_crucible.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.226"),
                relic = IconData.relics.herbalism_crucible,
                line = relicsets.herbalism,
                description = Mod.instance.Helper.Translation.Get("RelicData.229"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.232"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.234"),
            };


            relics[IconData.relics.herbalism_gauge.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.240"),
                relic = IconData.relics.herbalism_gauge,
                line = relicsets.herbalism,
                description = Mod.instance.Helper.Translation.Get("RelicData.243"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.246"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.248"),
            };


            // ====================================================================
            // Circle Runestones

            relics[IconData.relics.runestones_spring.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.257"),
                relic = IconData.relics.runestones_spring,
                line = relicsets.runestones,
                description = Mod.instance.Helper.Translation.Get("RelicData.260"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.351.5"),
                details = new(){
                    Mod.instance.Helper.Translation.Get("RelicData.262"),
                    Mod.instance.Helper.Translation.Get("RelicData.263"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.265"),
            };

            relics[IconData.relics.runestones_moon.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.270"),
                relic = IconData.relics.runestones_moon,
                line = relicsets.runestones,
                description = Mod.instance.Helper.Translation.Get("RelicData.273"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.274"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.277"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.279"),
            };

            relics[IconData.relics.runestones_farm.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.284"),
                relic = IconData.relics.runestones_farm,
                line = relicsets.runestones,
                description = Mod.instance.Helper.Translation.Get("RelicData.287"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.288"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.291"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.293"),
            };

            relics[IconData.relics.runestones_cat.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.298"),
                relic = IconData.relics.runestones_cat,
                line = relicsets.runestones,
                description = Mod.instance.Helper.Translation.Get("RelicData.301"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.302"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.305"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.307"),
            };

            relics[IconData.relics.runestones_alchemistry.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.351.1"),
                relic = IconData.relics.runestones_alchemistry,
                line = relicsets.other,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.351.2"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.351.3"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.351.4"),
            };

            // ====================================================================
            // Avalant Relics

            relics[IconData.relics.avalant_disc.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.315"),
                relic = IconData.relics.avalant_disc,
                line = relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("RelicData.318"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.319"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.322"),

                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.325"),
            };

            relics[IconData.relics.avalant_chassis.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.330"),
                relic = IconData.relics.avalant_chassis,
                line = relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("RelicData.333"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.334"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.336"),
            };

            relics[IconData.relics.avalant_gears.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.341"),
                relic = IconData.relics.avalant_gears,
                line = relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("RelicData.344"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.345"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.347"),
            };

            relics[IconData.relics.avalant_casing.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.352"),
                relic = IconData.relics.avalant_casing,
                line = relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("RelicData.355"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.356"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.358"),
            };

            relics[IconData.relics.avalant_needle.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.363"),
                relic = IconData.relics.avalant_needle,
                line = relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("RelicData.366"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.367"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.369"),
            };

            relics[IconData.relics.avalant_measure.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.374"),
                relic = IconData.relics.avalant_measure,
                line = relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("RelicData.377"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.378"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.380"),
            };

            // ====================================================================
            // Preserved Texts

            relics[IconData.relics.book_wyrven.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.388"),
                relic = IconData.relics.book_wyrven,
                line = relicsets.books,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.392"),
                details = new()
                {Mod.instance.Helper.Translation.Get("RelicData.722"),
                    Mod.instance.Helper.Translation.Get("RelicData.395"),
                    Mod.instance.Helper.Translation.Get("RelicData.396"),

                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.399"),

                narrative = new()
                {

                    Mod.instance.Helper.Translation.Get("RelicData.404"),
                    Mod.instance.Helper.Translation.Get("RelicData.405") +
                    Mod.instance.Helper.Translation.Get("RelicData.406") +
                    Mod.instance.Helper.Translation.Get("RelicData.407") +
                    Mod.instance.Helper.Translation.Get("RelicData.408") +
                    Mod.instance.Helper.Translation.Get("RelicData.409") +
                    Mod.instance.Helper.Translation.Get("RelicData.410") +
                    Mod.instance.Helper.Translation.Get("RelicData.411"),

                    Mod.instance.Helper.Translation.Get("RelicData.413"),
                    Mod.instance.Helper.Translation.Get("RelicData.414") +
                    Mod.instance.Helper.Translation.Get("RelicData.415") +
                    Mod.instance.Helper.Translation.Get("RelicData.416") +
                    Mod.instance.Helper.Translation.Get("RelicData.417") +
                    Mod.instance.Helper.Translation.Get("RelicData.418") +
                    Mod.instance.Helper.Translation.Get("RelicData.419") +
                    Mod.instance.Helper.Translation.Get("RelicData.420") +
                    Mod.instance.Helper.Translation.Get("RelicData.421") +
                    Mod.instance.Helper.Translation.Get("RelicData.422") +
                    Mod.instance.Helper.Translation.Get("RelicData.423"),

                }

            };

            relics[IconData.relics.book_letters.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.447"),
                relic = IconData.relics.book_letters,
                line = relicsets.books,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.451"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.452"),
                details = new()
                {Mod.instance.Helper.Translation.Get("RelicData.722"),
                    Mod.instance.Helper.Translation.Get("RelicData.455"),

                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.458"),

                narrative = new()
                {

                    Mod.instance.Helper.Translation.Get("RelicData.463"),
                    Mod.instance.Helper.Translation.Get("RelicData.464") +
                    Mod.instance.Helper.Translation.Get("RelicData.465") +
                    Mod.instance.Helper.Translation.Get("RelicData.466") +
                    Mod.instance.Helper.Translation.Get("RelicData.467") +
                    Mod.instance.Helper.Translation.Get("RelicData.468"),

                    Mod.instance.Helper.Translation.Get("RelicData.470"),
                    Mod.instance.Helper.Translation.Get("RelicData.471") +
                    Mod.instance.Helper.Translation.Get("RelicData.472") +
                    Mod.instance.Helper.Translation.Get("RelicData.473") +
                    Mod.instance.Helper.Translation.Get("RelicData.474") +
                    Mod.instance.Helper.Translation.Get("RelicData.475"),

                    Mod.instance.Helper.Translation.Get("RelicData.477"),
                    Mod.instance.Helper.Translation.Get("RelicData.478") +
                    Mod.instance.Helper.Translation.Get("RelicData.479") +
                    Mod.instance.Helper.Translation.Get("RelicData.480") +
                    Mod.instance.Helper.Translation.Get("RelicData.481") +
                    Mod.instance.Helper.Translation.Get("RelicData.482") +
                    Mod.instance.Helper.Translation.Get("RelicData.483") +
                    Mod.instance.Helper.Translation.Get("RelicData.484"),

                    Mod.instance.Helper.Translation.Get("RelicData.486"),
                    Mod.instance.Helper.Translation.Get("RelicData.487") +
                    Mod.instance.Helper.Translation.Get("RelicData.488") +
                    Mod.instance.Helper.Translation.Get("RelicData.489") +
                    Mod.instance.Helper.Translation.Get("RelicData.490"),

                    Mod.instance.Helper.Translation.Get("RelicData.492"),
                    Mod.instance.Helper.Translation.Get("RelicData.493") +
                    Mod.instance.Helper.Translation.Get("RelicData.494"),

                }

            };

            relics[IconData.relics.book_manual.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.502"),
                relic = IconData.relics.book_manual,
                line = relicsets.books,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.506"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.507"),
                details = new()
                {Mod.instance.Helper.Translation.Get("RelicData.722"),
                    Mod.instance.Helper.Translation.Get("RelicData.510"),

                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.513"),

                narrative = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.517"),
                    Mod.instance.Helper.Translation.Get("RelicData.518") +
                    Mod.instance.Helper.Translation.Get("RelicData.519") +
                    Mod.instance.Helper.Translation.Get("RelicData.520") +
                    Mod.instance.Helper.Translation.Get("RelicData.521") +
                    Mod.instance.Helper.Translation.Get("RelicData.522"),

                    Mod.instance.Helper.Translation.Get("RelicData.524"),
                    Mod.instance.Helper.Translation.Get("RelicData.525") +
                    Mod.instance.Helper.Translation.Get("RelicData.526"),

                    Mod.instance.Helper.Translation.Get("RelicData.528"),
                    Mod.instance.Helper.Translation.Get("RelicData.529") +
                    Mod.instance.Helper.Translation.Get("RelicData.530") +
                    Mod.instance.Helper.Translation.Get("RelicData.531") +
                    Mod.instance.Helper.Translation.Get("RelicData.532"),

                }

            };

            relics[IconData.relics.book_druid.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.540"),
                relic = IconData.relics.book_druid,
                line = relicsets.books,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.544"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.545"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.722"),
                    Mod.instance.Helper.Translation.Get("RelicData.549") +
                    Mod.instance.Helper.Translation.Get("RelicData.550")

                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.553"),

                narrative = new()
                {

                    Mod.instance.Helper.Translation.Get("RelicData.558"),
                    Mod.instance.Helper.Translation.Get("RelicData.559"),
                    Mod.instance.Helper.Translation.Get("RelicData.560"),
                    Mod.instance.Helper.Translation.Get("RelicData.561"),

                    Mod.instance.Helper.Translation.Get("RelicData.563"),
                    Mod.instance.Helper.Translation.Get("RelicData.564"),
                    Mod.instance.Helper.Translation.Get("RelicData.565"),
                    Mod.instance.Helper.Translation.Get("RelicData.566"),

                }

            };

            relics[IconData.relics.book_annal.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.386.1"),
                relic = IconData.relics.book_annal,
                line = relicsets.books,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.386.2"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.386.4"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.722"),
                    Mod.instance.Helper.Translation.Get("RelicData.386.3"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.386.5"),

                narrative = new()
                {

                    Mod.instance.Helper.Translation.Get("RelicData.425"),
                    Mod.instance.Helper.Translation.Get("RelicData.426") +
                    Mod.instance.Helper.Translation.Get("RelicData.427") +
                    Mod.instance.Helper.Translation.Get("RelicData.428") +
                    Mod.instance.Helper.Translation.Get("RelicData.429") +
                    Mod.instance.Helper.Translation.Get("RelicData.430"),

                    Mod.instance.Helper.Translation.Get("RelicData.432"),
                    Mod.instance.Helper.Translation.Get("RelicData.433") +
                    Mod.instance.Helper.Translation.Get("RelicData.434") +
                    Mod.instance.Helper.Translation.Get("RelicData.435") +
                    Mod.instance.Helper.Translation.Get("RelicData.436") +
                    Mod.instance.Helper.Translation.Get("RelicData.437") +
                    Mod.instance.Helper.Translation.Get("RelicData.438") +
                    Mod.instance.Helper.Translation.Get("RelicData.439"),

                }

            };

            // ====================================================================
            // Fates Relics

            relics[IconData.relics.box_measurer.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.577"),
                relic = IconData.relics.box_measurer,
                line = relicsets.boxes,
                description = Mod.instance.Helper.Translation.Get("RelicData.580"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.583"),
                    Mod.instance.Helper.Translation.Get("RelicData.584"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.586"),
            };

            relics[IconData.relics.box_mortician.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.591"),
                relic = IconData.relics.box_mortician,
                line = relicsets.boxes,
                description = Mod.instance.Helper.Translation.Get("RelicData.594"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.595"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.598"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.600"),
            };

            relics[IconData.relics.box_artisan.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.605"),
                relic = IconData.relics.box_artisan,
                line = relicsets.boxes,
                description = Mod.instance.Helper.Translation.Get("RelicData.608"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.609"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.611"),
            };

            relics[IconData.relics.box_chaos.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.616"),
                relic = IconData.relics.box_chaos,
                line = relicsets.boxes,
                description = Mod.instance.Helper.Translation.Get("RelicData.619"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.620"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.622"),
            };

            // ====================================================================
            // Restore Relics

            relics[IconData.relics.restore_goshuin.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.329.1"),
                relic = IconData.relics.restore_goshuin,
                line = relicsets.restore,
                description = Mod.instance.Helper.Translation.Get("RelicData.329.2"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.329.3"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.329.4"),
            };

            relics[IconData.relics.restore_offering.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.329.7"),
                relic = IconData.relics.restore_offering,
                line = relicsets.restore,
                description = Mod.instance.Helper.Translation.Get("RelicData.329.8"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.329.9"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.329.10"),
            };

            relics[IconData.relics.restore_cloth.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.330.1"),
                relic = IconData.relics.restore_cloth,
                line = relicsets.restore,
                description = Mod.instance.Helper.Translation.Get("RelicData.330.2"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.330.3"),
                },
                hint = Mod.instance.Helper.Translation.Get("RelicData.330.4"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.330.5"),
            };


            // ====================================================================
            // Rite of Bones Skull relics

            relics[IconData.relics.skull_saurus.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.339.3"),
                relic = IconData.relics.skull_saurus,
                line = relicsets.skulls,
                description = Mod.instance.Helper.Translation.Get("RelicData.339.4"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.339.5"),
            };

            relics[IconData.relics.skull_gelatin.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.339.6"),
                relic = IconData.relics.skull_gelatin,
                line = relicsets.skulls,
                description = Mod.instance.Helper.Translation.Get("RelicData.339.7"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.339.8"),
            };

            relics[IconData.relics.skull_cannoli.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.339.9"),
                relic = IconData.relics.skull_cannoli,
                line = relicsets.skulls,
                description = Mod.instance.Helper.Translation.Get("RelicData.339.10"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.339.11"),
            };

            relics[IconData.relics.skull_fox.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.339.12"),
                relic = IconData.relics.skull_fox,
                line = relicsets.skulls,
                description = Mod.instance.Helper.Translation.Get("RelicData.339.13"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.339.14"),
            };

            relics[IconData.relics.golden_core.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.343.1"),
                relic = IconData.relics.golden_core,
                line = relicsets.skulls,
                description = Mod.instance.Helper.Translation.Get("RelicData.343.2"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.343.3"),
            };

            // ====================================================================
            // Testaments

            relics[IconData.relics.book_knight.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.343.4"),
                relic = IconData.relics.book_knight,
                line = relicsets.testaments,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.343.5"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.343.6"),

                narrative = new()
                {

                    Mod.instance.Helper.Translation.Get("RelicData.343.7"),
                    Mod.instance.Helper.Translation.Get("RelicData.343.8"),
                    Mod.instance.Helper.Translation.Get("RelicData.343.9"),
                    Mod.instance.Helper.Translation.Get("RelicData.343.10"),
                    Mod.instance.Helper.Translation.Get("RelicData.343.11"),
                    Mod.instance.Helper.Translation.Get("RelicData.343.12"),
                    Mod.instance.Helper.Translation.Get("RelicData.343.13"),
                    Mod.instance.Helper.Translation.Get("RelicData.343.14"),
                    Mod.instance.Helper.Translation.Get("RelicData.343.15"),

                }

            };

            // ====================================================================
            // Stardew Druid

            relics[IconData.relics.stardew_druid.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.96"),
                relic = IconData.relics.stardew_druid,
                line = relicsets.druid,
                description = Mod.instance.Helper.Translation.Get("RelicData.99"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.102") +
                    Mod.instance.Helper.Translation.Get("RelicData.103") +
                    Mod.instance.Helper.Translation.Get("RelicData.104"),
                    Mod.instance.Helper.Translation.Get("RelicData.105"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.107"),
            };

            return relics;

        }


    }

    public class Relic
    {

        // -----------------------------------------------
        // journal

        public string title;

        public IconData.relics relic = IconData.relics.none;

        public RelicData.relicsets line = RelicData.relicsets.none;

        public bool function;

        public bool cancel;

        public bool activatable;

        public string description;

        public string hint;

        public string heldup;

        public List<string> details = new();

        public List<string> narrative;

    }

}
