
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
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Events;
using StardewValley.GameData.Characters;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using static StardewDruid.Journal.HerbalData;


namespace StardewDruid.Journal
{
    public class RelicData
    {

        public enum relicsets
        {
            
            none,
            companion,
            wayfinder,
            herbalism,
            runestones,
            avalant,
            books,
            boxes,
            other,
        }

        public Dictionary<relicsets, List<string>> titles = new()
        {

            [relicsets.companion] = new() { Mod.instance.Helper.Translation.Get("RelicSet.companion.title.0"), Mod.instance.Helper.Translation.Get("RelicSet.companion.title.1"), },
            [relicsets.wayfinder] = new() { Mod.instance.Helper.Translation.Get("RelicSet.wayfinder.title.0"), Mod.instance.Helper.Translation.Get("RelicSet.wayfinder.title.1"), },
            [relicsets.herbalism] = new() { Mod.instance.Helper.Translation.Get("RelicSet.herbalism.title.0"), Mod.instance.Helper.Translation.Get("RelicSet.herbalism.title.1"), },
            [relicsets.runestones] = new() { Mod.instance.Helper.Translation.Get("RelicSet.runestones.title.0"), Mod.instance.Helper.Translation.Get("RelicSet.runestones.title.1"), },
            [relicsets.avalant] = new() { Mod.instance.Helper.Translation.Get("RelicSet.avalant.title.0"), Mod.instance.Helper.Translation.Get("RelicSet.avalant.title.1"), },
            [relicsets.books] = new() { Mod.instance.Helper.Translation.Get("RelicSet.books.title.0"), Mod.instance.Helper.Translation.Get("RelicSet.books.title.1"), },
            [relicsets.boxes] = new() { Mod.instance.Helper.Translation.Get("RelicSet.boxes.title.0"), Mod.instance.Helper.Translation.Get("RelicSet.boxes.title.1"), },
            [relicsets.other] = new() { Mod.instance.Helper.Translation.Get("RelicSet.other.title.0"), Mod.instance.Helper.Translation.Get("RelicSet.other.title.1"), },
        };

        public Dictionary<relicsets, List<IconData.relics>> lines = new()
        {
            [relicsets.companion] = new() {
                IconData.relics.effigy_crest,
                IconData.relics.jester_dice,
                IconData.relics.shadowtin_tome,
                IconData.relics.dragon_form,
                IconData.relics.stardew_druid,
            },
            [relicsets.wayfinder] = new() {
                IconData.relics.wayfinder_censer,
                IconData.relics.wayfinder_lantern,
                IconData.relics.wayfinder_water,
                IconData.relics.wayfinder_eye,
                IconData.relics.wayfinder_ceremonial,
                IconData.relics.wayfinder_dwarf,
            },
            [relicsets.herbalism] = new() {
                IconData.relics.herbalism_mortar,
                IconData.relics.herbalism_pan,
                IconData.relics.herbalism_still,
                IconData.relics.herbalism_crucible,
                IconData.relics.herbalism_gauge,
            },
            [relicsets.runestones] = new() {
                IconData.relics.runestones_spring,
                IconData.relics.runestones_farm,
                IconData.relics.runestones_moon,
                IconData.relics.runestones_cat,
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
            },
            [relicsets.boxes] = new() {
                IconData.relics.box_measurer,
                IconData.relics.box_mortician,
                IconData.relics.box_artisan,
                IconData.relics.box_chaos,
            },
            [relicsets.other] = new() {
                IconData.relics.crow_hammer,
            },
        };


        public Dictionary<relicsets, string> quests = new()
        {
            [relicsets.runestones] = QuestHandle.relicWeald,
            [relicsets.avalant] = QuestHandle.relicMists,
            [relicsets.books] = QuestHandle.relicEther,
            [relicsets.boxes] = QuestHandle.relicFates,
        };

        public Dictionary<string, Relic> reliquary = new();

        public RelicData()
        {

            reliquary = RelicsList();

        }

        public void Ready()
        {

            for(int i = Mod.instance.save.reliquary.Count - 1; i >= 0; i--)
            {

                string relic = Mod.instance.save.reliquary.ElementAt(i).Key;

                if (!reliquary.ContainsKey(relic))
                {

                    Mod.instance.save.reliquary.Remove(relic);

                }

            }

        }

        public List<List<string>> OrganiseRelics()
        {

            List<List<string>> source = new();

            foreach(relicsets set in lines.Keys)
            {

                /*if(set == relicsets.runestones)
                {

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.fatesFive))
                    {

                        RunestoneFunction();

                    }

                }*/

                List<string> add = new();

                switch (set)
                {
                    case relicsets.runestones:
                    case relicsets.avalant:
                    case relicsets.books:
                    case relicsets.boxes:

                        int check = 0;

                        for (int i = 0; i < lines[set].Count; i++)
                        {

                            if (Mod.instance.save.reliquary.ContainsKey(lines[set][i].ToString()))
                            {

                                check++;

                            }

                        }

                        if(check > 0)
                        {

                            for (int i = 0; i < lines[set].Count; i++)
                            {
                                
                                add.Add(lines[set][i].ToString());

                            }

                        }

                        break;

                    default:

                        for (int i = 0; i < lines[set].Count; i++)
                        {

                            if (Mod.instance.save.reliquary.ContainsKey(lines[set][i].ToString()))
                            {

                                add.Add(lines[set][i].ToString());

                            }

                        }

                        break;


                }



                if(add.Count == 0)
                {
                    
                    continue;

                }

                if (source.Count == 0 || source.Last().Count() == 18)
                {
                    
                    source.Add(new List<string>());
                
                }

                for (int i = 0; i < 6; i++)
                {

                    if(i < add.Count)
                    {

                        source.Last().Add(add[i]);


                    }
                    else
                    {

                        source.Last().Add("blank");

                    }

                }

            }

            return source;

        }

        public Dictionary<int, Journal.ContentComponent> JournalRelics()
        {

            Dictionary<int, Journal.ContentComponent> journal = new();

            int start = 0;

            foreach (relicsets set in lines.Keys)
            {
                
                if (set == relicsets.avalant)
                {

                    if (ProgressRelicQuest(relicsets.avalant) == 0)
                    {

                        continue;

                    }


                }else
                if (!Mod.instance.save.reliquary.ContainsKey(lines[set][0].ToString()))
                {

                    continue;

                }

                for (int i = 0; i < lines[set].Count; i++)
                {

                    IconData.relics relicName = lines[set][i];

                    Journal.ContentComponent content = new(ContentComponent.contentTypes.relic, relicName.ToString());

                    content.relics[0] = relicName;

                    content.relicColours[0] = Color.White;

                    if (!Mod.instance.save.reliquary.ContainsKey(relicName.ToString()))
                    {

                        switch (set)
                        {
                            case relicsets.runestones:
                            case relicsets.avalant:
                            case relicsets.books:
                            case relicsets.boxes:

                                content.relicColours[0] = Color.Black * 0.01f;

                                break;

                            default:

                                content.active = false;

                                break;

                        }

                    }

                    journal[start++] = content;

                }

                for(int i = 0; i < 6 - lines[set].Count; i++)
                {

                    Journal.ContentComponent content = new(ContentComponent.contentTypes.relic, set.ToString()+i.ToString());

                    content.active = false;

                    journal[start++] = content;

                }

            }

            return journal;

        }

        public Dictionary<int, Journal.ContentComponent> JournalHeaders()
        {

            Dictionary<int, Journal.ContentComponent> journal = new();

            int start = 0;

            foreach(KeyValuePair<relicsets, List<string>> section in titles)
            {

                if (section.Key == relicsets.avalant)
                {

                    if (ProgressRelicQuest(relicsets.avalant) == 0)
                    {

                        continue;

                    }

                }
                else
                if (!Mod.instance.save.reliquary.ContainsKey(lines[section.Key][0].ToString()))
                {

                    continue;

                }

                Journal.ContentComponent content = new(ContentComponent.contentTypes.header, section.Key.ToString());

                content.text[0] = section.Value[0];

                content.text[1] = section.Value[1];

                journal[start++] = content;

            }

            return journal;

        }

        public void ReliquaryUpdate(string id, int update = 0)
        {

            if (!Mod.instance.save.reliquary.ContainsKey(id))
            {

                Mod.instance.save.reliquary[id] = update;

            }
            else if(Mod.instance.save.reliquary[id] < update)
            {

                Mod.instance.save.reliquary[id] = update;

            }

        }

        public static Dictionary<string, Relic> RelicsList()
        {

            Dictionary<string, Relic> relics = new();

            // ====================================================================
            // Companion Tokens

            relics[IconData.relics.effigy_crest.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.effigy_crest.title"),
                relic = IconData.relics.effigy_crest,
                line = RelicData.relicsets.companion,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("Relic.effigy_crest.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.effigy_crest.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.effigy_crest.heldup"),
            };

            relics[IconData.relics.jester_dice.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.jester_dice.title"),
                relic = IconData.relics.jester_dice,
                line = RelicData.relicsets.companion,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("Relic.jester_dice.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.jester_dice.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.jester_dice.heldup"),
            };

            relics[IconData.relics.shadowtin_tome.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.shadowtin_tome.title"),
                relic = IconData.relics.shadowtin_tome,
                line = RelicData.relicsets.companion,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("Relic.shadowtin_tome.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.shadowtin_tome.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.shadowtin_tome.heldup"),
            };

            relics[IconData.relics.dragon_form.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.dragon_form.title"),
                relic = IconData.relics.dragon_form,
                line = RelicData.relicsets.companion,
                function = true,
                description = Mod.instance.Helper.Translation.Get("Relic.dragon_form.description"),
                heldup = Mod.instance.Helper.Translation.Get("Relic.dragon_form.heldup"),
            };

            relics[IconData.relics.stardew_druid.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.stardew_druid.title"),
                relic = IconData.relics.stardew_druid,
                line = RelicData.relicsets.companion,
                description = Mod.instance.Helper.Translation.Get("Relic.stardew_druid.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.stardew_druid.details.0"),
                    Mod.instance.Helper.Translation.Get("Relic.stardew_druid.details.1"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.stardew_druid.heldup"),
            };

            // ====================================================================
            // Wayfinder relics

            relics[IconData.relics.wayfinder_censer.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.wayfinder_censer.title"),
                relic = IconData.relics.wayfinder_censer,
                line = RelicData.relicsets.wayfinder,
                function = true,
                description = Mod.instance.Helper.Translation.Get("Relic.wayfinder_censer.description"),
                heldup = Mod.instance.Helper.Translation.Get("Relic.wayfinder_censer.heldup"),
            };

            relics[IconData.relics.wayfinder_lantern.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.wayfinder_lantern.title"),
                relic = IconData.relics.wayfinder_lantern,
                line = RelicData.relicsets.wayfinder,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("Relic.wayfinder_lantern.description"),
                heldup = Mod.instance.Helper.Translation.Get("Relic.wayfinder_lantern.heldup"),
            };

            relics[IconData.relics.wayfinder_water.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.wayfinder_water.title"),
                relic = IconData.relics.wayfinder_water,
                line = RelicData.relicsets.wayfinder,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("Relic.wayfinder_water.description"),
                heldup = Mod.instance.Helper.Translation.Get("Relic.wayfinder_water.heldup"),
            };

            relics[IconData.relics.wayfinder_eye.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.wayfinder_eye.title"),
                relic = IconData.relics.wayfinder_eye,
                line = RelicData.relicsets.wayfinder,
                function = true,
                description = Mod.instance.Helper.Translation.Get("Relic.wayfinder_eye.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.wayfinder_eye.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.wayfinder_eye.heldup"),
            };

            relics[IconData.relics.wayfinder_ceremonial.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.wayfinder_ceremonial.title"),
                relic = IconData.relics.wayfinder_ceremonial,
                line = RelicData.relicsets.wayfinder,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("Relic.wayfinder_ceremonial.description"),
                heldup = Mod.instance.Helper.Translation.Get("Relic.wayfinder_ceremonial.heldup"),
            };

            relics[IconData.relics.wayfinder_dwarf.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.wayfinder_dwarf.title"),
                relic = IconData.relics.wayfinder_dwarf,
                line = RelicData.relicsets.wayfinder,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("Relic.wayfinder_dwarf.description"),
                heldup = Mod.instance.Helper.Translation.Get("Relic.wayfinder_dwarf.heldup"),
            };

            // ====================================================================
            // Herbalism Relics

            relics[IconData.relics.herbalism_mortar.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.herbalism_mortar.title"),
                relic = IconData.relics.herbalism_mortar,
                line = RelicData.relicsets.herbalism,
                description = Mod.instance.Helper.Translation.Get("Relic.herbalism_mortar.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.herbalism_mortar.details.0"),
                    Mod.instance.Helper.Translation.Get("Relic.herbalism_mortar.details.1"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.herbalism_mortar.heldup"),
            };

            relics[IconData.relics.herbalism_pan.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.herbalism_pan.title"),
                relic = IconData.relics.herbalism_pan,
                line = RelicData.relicsets.herbalism,
                description = Mod.instance.Helper.Translation.Get("Relic.herbalism_pan.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.herbalism_pan.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.herbalism_pan.heldup"),
            };

            relics[IconData.relics.herbalism_still.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.herbalism_still.title"),
                relic = IconData.relics.herbalism_still,
                line = RelicData.relicsets.herbalism,
                description = Mod.instance.Helper.Translation.Get("Relic.herbalism_still.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.herbalism_still.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.herbalism_still.heldup"),
            };

            relics[IconData.relics.herbalism_crucible.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.herbalism_crucible.title"),
                relic = IconData.relics.herbalism_crucible,
                line = RelicData.relicsets.herbalism,
                description = Mod.instance.Helper.Translation.Get("Relic.herbalism_crucible.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.herbalism_crucible.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.herbalism_crucible.heldup"),
            };


            relics[IconData.relics.herbalism_gauge.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.herbalism_gauge.title"),
                relic = IconData.relics.herbalism_gauge,
                line = RelicData.relicsets.herbalism,
                description = Mod.instance.Helper.Translation.Get("Relic.herbalism_gauge.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.herbalism_gauge.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.herbalism_gauge.heldup"),
            };


            // ====================================================================
            // Circle Runestones

            relics[IconData.relics.runestones_spring.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.runestones_spring.title"),
                relic = IconData.relics.runestones_spring,
                line = RelicData.relicsets.runestones,
                description = Mod.instance.Helper.Translation.Get("Relic.runestones_spring.description"),
                details = new(){
                    Mod.instance.Helper.Translation.Get("Relic.runestones_spring.details.0"),
                    Mod.instance.Helper.Translation.Get("Relic.runestones_spring.details.1"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.runestones_spring.heldup"),
            };

            relics[IconData.relics.runestones_moon.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.runestones_moon.title"),
                relic = IconData.relics.runestones_moon,
                line = RelicData.relicsets.runestones,
                description = Mod.instance.Helper.Translation.Get("Relic.runestones_moon.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.runestones_moon.hint"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.runestones_moon.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.runestones_moon.heldup"),
            };

            relics[IconData.relics.runestones_farm.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.runestones_farm.title"),
                relic = IconData.relics.runestones_farm,
                line = RelicData.relicsets.runestones,
                description = Mod.instance.Helper.Translation.Get("Relic.runestones_farm.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.runestones_farm.hint"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.runestones_farm.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.runestones_farm.heldup"),
            };

            relics[IconData.relics.runestones_cat.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.runestones_cat.title"),
                relic = IconData.relics.runestones_cat,
                line = RelicData.relicsets.runestones,
                description = Mod.instance.Helper.Translation.Get("Relic.runestones_cat.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.runestones_cat.hint"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.runestones_cat.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.runestones_cat.heldup"),
            };

            // ====================================================================
            // Avalant Relics

            relics[IconData.relics.avalant_disc.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.avalant_disc.title"),
                relic = IconData.relics.avalant_disc,
                line = RelicData.relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("Relic.avalant_disc.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.avalant_disc.hint"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.avalant_disc.details.0"),

                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.avalant_disc.heldup"),
            };

            relics[IconData.relics.avalant_chassis.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.avalant_chassis.title"),
                relic = IconData.relics.avalant_chassis,
                line = RelicData.relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("Relic.avalant_chassis.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.avalant_chassis.hint"),

                heldup = Mod.instance.Helper.Translation.Get("Relic.avalant_chassis.heldup"),
            };

            relics[IconData.relics.avalant_gears.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.avalant_gears.title"),
                relic = IconData.relics.avalant_gears,
                line = RelicData.relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("Relic.avalant_gears.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.avalant_gears.hint"),

                heldup = Mod.instance.Helper.Translation.Get("Relic.avalant_gears.heldup"),
            };

            relics[IconData.relics.avalant_casing.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.avalant_casing.title"),
                relic = IconData.relics.avalant_casing,
                line = RelicData.relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("Relic.avalant_casing.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.avalant_casing.hint"),

                heldup = Mod.instance.Helper.Translation.Get("Relic.avalant_casing.heldup"),
            };

            relics[IconData.relics.avalant_needle.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.avalant_needle.title"),
                relic = IconData.relics.avalant_needle,
                line = RelicData.relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("Relic.avalant_needle.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.avalant_needle.hint"),

                heldup = Mod.instance.Helper.Translation.Get("Relic.avalant_needle.heldup"),
            };

            relics[IconData.relics.avalant_measure.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.avalant_measure.title"),
                relic = IconData.relics.avalant_measure,
                line = RelicData.relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("Relic.avalant_measure.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.avalant_measure.hint"),

                heldup = Mod.instance.Helper.Translation.Get("Relic.avalant_measure.heldup"),
            };

            // ====================================================================
            // Preserved Texts

            relics[IconData.relics.book_wyrven.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.book_wyrven.title"),
                relic = IconData.relics.book_wyrven,
                line = RelicData.relicsets.books,
                function = true,
                description = Mod.instance.Helper.Translation.Get("Relic.book_wyrven.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.book_wyrven.details.0"),
                    Mod.instance.Helper.Translation.Get("Relic.book_wyrven.details.1"),

                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.book_wyrven.heldup"),

                narrative = new()
                {
                    
                    Mod.instance.Helper.Translation.Get("Relic.book_wyrven.narrative.0"),
                    Mod.instance.Helper.Translation.Get("Relic.book_wyrven.narrative.1"),
                    
                    Mod.instance.Helper.Translation.Get("Relic.book_wyrven.narrative.2"),
                    Mod.instance.Helper.Translation.Get("Relic.book_wyrven.narrative.3"),
                    
                    Mod.instance.Helper.Translation.Get("Relic.book_wyrven.narrative.4"),
                    Mod.instance.Helper.Translation.Get("Relic.book_wyrven.narrative.5"),
                    
                    Mod.instance.Helper.Translation.Get("Relic.book_wyrven.narrative.6"),
                    Mod.instance.Helper.Translation.Get("Relic.book_wyrven.narrative.7"),

                }

            };

            relics[IconData.relics.book_letters.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.book_letters.title"),
                relic = IconData.relics.book_letters,
                line = RelicData.relicsets.books,
                function = true,
                description = Mod.instance.Helper.Translation.Get("Relic.book_letters.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.book_letters.hint"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.book_letters.details.0"),

                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.book_letters.heldup"),

                narrative = new()
                {

                    Mod.instance.Helper.Translation.Get("Relic.book_letters.narrative.0"),
                    Mod.instance.Helper.Translation.Get("Relic.book_letters.narrative.1"),

                    Mod.instance.Helper.Translation.Get("Relic.book_letters.narrative.2"),
                    Mod.instance.Helper.Translation.Get("Relic.book_letters.narrative.3"),

                    Mod.instance.Helper.Translation.Get("Relic.book_letters.narrative.4"),
                    Mod.instance.Helper.Translation.Get("Relic.book_letters.narrative.5"),

                    Mod.instance.Helper.Translation.Get("Relic.book_letters.narrative.6"),
                    Mod.instance.Helper.Translation.Get("Relic.book_letters.narrative.7"),

                    Mod.instance.Helper.Translation.Get("Relic.book_letters.narrative.8"),
                    Mod.instance.Helper.Translation.Get("Relic.book_letters.narrative.9"),

                }

            };

            relics[IconData.relics.book_manual.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.book_manual.title"),
                relic = IconData.relics.book_manual,
                line = RelicData.relicsets.books,
                function = true,
                description = Mod.instance.Helper.Translation.Get("Relic.book_manual.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.book_manual.hint"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.book_manual.details.0"),

                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.book_manual.heldup"),

                narrative = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.book_manual.narrative.0"),
                    Mod.instance.Helper.Translation.Get("Relic.book_manual.narrative.1"),
                //  i think this is a bug, so i remove the plus sign
                //  "(The First of the Benevolent might be amenable to providing this service for you in light of their penance). " +

                    Mod.instance.Helper.Translation.Get("Relic.book_manual.narrative.2"),
                    Mod.instance.Helper.Translation.Get("Relic.book_manual.narrative.3"),
                    
                    Mod.instance.Helper.Translation.Get("Relic.book_manual.narrative.4"),
                    Mod.instance.Helper.Translation.Get("Relic.book_manual.narrative.5"),

                }

            };

            relics[IconData.relics.book_druid.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.book_druid.title"),
                relic = IconData.relics.book_druid,
                line = RelicData.relicsets.books,
                function = true,
                description = Mod.instance.Helper.Translation.Get("Relic.book_druid.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.book_druid.hint"),
                details = new()
                {

                    Mod.instance.Helper.Translation.Get("Relic.book_druid.details.0"),

                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.book_druid.heldup"),

                narrative = new()
                {
                    
                    Mod.instance.Helper.Translation.Get("Relic.book_druid.narrative.0"),
                    Mod.instance.Helper.Translation.Get("Relic.book_druid.narrative.1"),
                    Mod.instance.Helper.Translation.Get("Relic.book_druid.narrative.2"),
                    Mod.instance.Helper.Translation.Get("Relic.book_druid.narrative.3"),

                    Mod.instance.Helper.Translation.Get("Relic.book_druid.narrative.4"),
                    Mod.instance.Helper.Translation.Get("Relic.book_druid.narrative.5"),
                    Mod.instance.Helper.Translation.Get("Relic.book_druid.narrative.6"),
                    Mod.instance.Helper.Translation.Get("Relic.book_druid.narrative.7"),

                    /*"The war",
                    "For many lives my family lived in the Weald, at the woodland estate of the elder race. We could talk to the trees, the stones, the wind, anything blessed by the rule of the Two Kings. " +
                    "If only it had lasted. The elder race started a war with dragons, and when the Kings fled here, to the Weald, the dragons followed, and everything burned. " +
                    "It seemed the Kings would die, and their power lost forever, but an unworldly power came to the aid of the elder prince. The dragons were bested. " +
                    "Still, the wounds of the Two Kings caused them to fall into endless slumber.", 
                    
                    "The circle",
                    "So it seemed the power of the Weald would be lost forever, until the old crone and the princess came to us, and put us to task, building, sewing seeds, healing the land. " +
                    "But we would no longer answer to the Kings, for the power would be ours to wield, as long as we kept to the crone's plan. New friends came to us, some that loved the Stars, those that served dragons, some survivors, those faithful to Yoba. " +
                    "We formed a circle of druids to keep the old ways alive and look after the new folk. We had our enemies though. Many men blamed the elder race for their troubles.",

                    "The end",
                    "The princess shared many things with me, and favoured me most, for she trusted me with her biggest secret, the wooden man, who we crafted to house the soul of a fallen hero, the prince I think, though only the crone and princess know. " +
                    "But the blind anger of our enemies made it unsafe for the princess and her kin. She took her liegelords across the sea to an isle beyond the reach of the cultists. Then, after too short a time, she left the Weald for good. " +
                    "Now I go to be with her. The wooden one knows everything there is about the druids, and can train the next leader, and the next, and keep the crone's plan in my stead. For I cannot enjoy the Weald without the sight of my princess."
                    */
                }

            };

            // ====================================================================
            // Fates Relics

            relics[IconData.relics.box_measurer.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.box_measurer.title"),
                relic = IconData.relics.box_measurer,
                line = RelicData.relicsets.boxes,
                description = Mod.instance.Helper.Translation.Get("Relic.box_measurer.description"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.box_measurer.details.0"),
                    Mod.instance.Helper.Translation.Get("Relic.box_measurer.details.1"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.box_measurer.heldup"),
            };

            relics[IconData.relics.box_mortician.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.box_mortician.title"),
                relic = IconData.relics.box_mortician,
                line = RelicData.relicsets.boxes,
                description = Mod.instance.Helper.Translation.Get("Relic.box_mortician.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.box_mortician.hint"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Relic.box_mortician.details.0"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.box_mortician.heldup"),
            };

            relics[IconData.relics.box_artisan.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.box_artisan.title"),
                relic = IconData.relics.box_artisan,
                line = RelicData.relicsets.boxes,
                description = Mod.instance.Helper.Translation.Get("Relic.box_artisan.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.box_artisan.hint"),

                heldup = Mod.instance.Helper.Translation.Get("Relic.box_artisan.heldup"),
            };

            relics[IconData.relics.box_chaos.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.box_chaos.title"),
                relic = IconData.relics.box_chaos,
                line = RelicData.relicsets.boxes,
                description = Mod.instance.Helper.Translation.Get("Relic.box_chaos.description"),
                hint = Mod.instance.Helper.Translation.Get("Relic.box_chaos.hint"),

                heldup = Mod.instance.Helper.Translation.Get("Relic.box_chaos.heldup"),
            };

            // ======================================================================
            // Other relics

            relics[IconData.relics.crow_hammer.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("Relic.crow_hammer.title"),
                relic = IconData.relics.crow_hammer,
                line = RelicData.relicsets.other,
                description = Mod.instance.Helper.Translation.Get("Relic.crow_hammer.description"),
                details = new(){
                    Mod.instance.Helper.Translation.Get("Relic.crow_hammer.details.0"),
                    Mod.instance.Helper.Translation.Get("Relic.crow_hammer.details.1"),
                },
                heldup = Mod.instance.Helper.Translation.Get("Relic.crow_hammer.heldup"),
            };


            return relics;

        }

        public string RelicInstruction(string id)
        {

            Relic relic = reliquary[id];

            string str = null;

            switch (relic.relic)
            {

                case IconData.relics.effigy_crest:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.effigy_crest");

                case IconData.relics.jester_dice:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.jester_dice");

                case IconData.relics.shadowtin_tome:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.shadowtin_tome");

                case IconData.relics.wayfinder_censer:

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.wayfinder_censer");

                case IconData.relics.wayfinder_lantern:

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.wayfinder_lantern");

                case IconData.relics.wayfinder_water:

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.wayfinder_water");

                case IconData.relics.wayfinder_eye:

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.wayfinder_eye");

                case IconData.relics.wayfinder_ceremonial:

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.wayfinder_ceremonial");

                case IconData.relics.wayfinder_dwarf:

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.wayfinder_dwarf");

                case IconData.relics.dragon_form:

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.dragon_form");

                case IconData.relics.book_wyrven:
                case IconData.relics.book_letters:
                case IconData.relics.book_manual:
                case IconData.relics.book_druid:

                    return Mod.instance.Helper.Translation.Get("Relic.Instruction.-1");

            }

            return str;

        }

        public int RelicFunction(string id)
        {

            if (Mod.instance.activeEvent.Count > 0) { return 0; }

            Relic relic = reliquary[id];

            switch (relic.relic)
            {

                case IconData.relics.effigy_crest:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Effigy))
                    {

                        List<StardewDruid.Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, Character.Character.mode.track, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Effigy].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Effigy].SwitchToMode(Character.Character.mode.track, Game1.player);

                            Mod.instance.CastMessage("The Effigy has joined you", 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.jester_dice:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Jester))
                    {

                        List<StardewDruid.Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, Character.Character.mode.track, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Jester].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Jester].SwitchToMode(Character.Character.mode.track, Game1.player);

                            Mod.instance.CastMessage("The Jester of Fates follows you", 0, true);

                            return 1;

                        }

                    }

                    break;


                case IconData.relics.shadowtin_tome:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Shadowtin))
                    {

                        List<StardewDruid.Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, Character.Character.mode.track, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Shadowtin].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Shadowtin].SwitchToMode(Character.Character.mode.track, Game1.player);

                            Mod.instance.CastMessage("Shadowtin Bear has joined you", 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.wayfinder_censer:

                    if (Game1.player.currentLocation is Beach)
                    {

                        (Mod.instance.locations[LocationData.druid_atoll_name] as Atoll).AddBoatAccess(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_lantern:

                    if(Game1.player.currentLocation.Name == "UndergroundMine60")
                    {

                        Event.Access.AccessHandle lanternAccess = new();

                        lanternAccess.AccessSetup("UndergroundMine60", Location.LocationData.druid_chapel_name, new(24, 13), new(27, 30));

                        lanternAccess.AccessCheck(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_water:

                    if (Game1.player.currentLocation.Name == "UndergroundMine100")
                    {

                        Event.Access.AccessHandle lanternAccess = new();

                        lanternAccess.AccessSetup("UndergroundMine100", Location.LocationData.druid_vault_name, new(24, 13), new(27, 30));

                        lanternAccess.AccessCheck(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_eye:


                    Vector2 destination;

                    if (Game1.player.currentLocation is MineShaft)
                    {

                        destination = WarpData.WarpXZone(Game1.player.currentLocation);


                    }
                    else
                    {
                        destination = WarpData.WarpEntrance(Game1.player.currentLocation, Game1.player.Position);

                    }

                    if(destination != Vector2.Zero)
                    {

                        SpellHandle teleport = new(Game1.player.currentLocation, destination, Game1.player.Position);

                        teleport.type = SpellHandle.spells.teleport;

                        Mod.instance.spellRegister.Add(teleport);

                        Mod.instance.AbortAllEvents();

                    }
                    else
                    {

                        Mod.instance.CastMessage("Unable to find a valid warp point");

                    }

                    return 1;

                case IconData.relics.wayfinder_ceremonial:

                    if (Game1.player.currentLocation.Name == "SkullCave")
                    {

                        Event.Access.AccessHandle lanternAccess = new();

                        lanternAccess.AccessSetup("SkullCave", Location.LocationData.druid_tomb_name, new(10, 5), new(27, 30));

                        lanternAccess.AccessCheck(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_dwarf:

                    if (Game1.player.currentLocation.Name == "Forest")
                    {

                        Event.Access.AccessHandle access = new();

                        access.AccessSetup("Forest", Location.LocationData.druid_engineum_name, new(50, 73), new(27, 30));

                        access.AccessCheck(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.book_wyrven:
                case IconData.relics.book_letters:
                case IconData.relics.book_manual:
                case IconData.relics.book_druid:

                    return 2;

                case IconData.relics.dragon_form:

                    return 3;

            }

            return 0;


        }
        
        public int RelicCancel(string id)
        {

            if (Mod.instance.activeEvent.Count > 0) { return 0; }

            Event.Access.AccessHandle access;

            Relic relic = reliquary[id];

            switch (relic.relic)
            {

                case IconData.relics.effigy_crest:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Effigy))
                    {

                        List<StardewDruid.Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Effigy].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Effigy].SwitchToMode(Character.Character.mode.home, Game1.player);

                            Mod.instance.CastMessage("The Effigy has returned home", 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.jester_dice:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Jester))
                    {

                        List<StardewDruid.Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Jester].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Jester].SwitchToMode(Character.Character.mode.home, Game1.player);

                            Mod.instance.CastMessage("The Jester of Fates has returned home", 0, true);

                            return 1;

                        }

                    }

                    break;


                case IconData.relics.shadowtin_tome:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Shadowtin))
                    {

                        List<StardewDruid.Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Shadowtin].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Shadowtin].SwitchToMode(Character.Character.mode.home, Game1.player);

                            Mod.instance.CastMessage("Shadowtin Bear has returned home", 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.wayfinder_lantern:

                    if (Game1.player.currentLocation.Name == "UndergroundMine60")
                    {

                        access = new();

                        access.AccessSetup("UndergroundMine60", Location.LocationData.druid_chapel_name, new(24, 13), new(27, 30));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_water:

                    if (Game1.player.currentLocation.Name == "UndergroundMine100")
                    {

                        access = new();

                        access.AccessSetup("UndergroundMine100", Location.LocationData.druid_vault_name, new(24, 13), new(27, 30));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_ceremonial:

                    if (Game1.player.currentLocation.Name == "SkullCave")
                    {

                        access = new();

                        access.AccessSetup("SkullCave", Location.LocationData.druid_tomb_name, new(10, 5), new(27, 30));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_dwarf:

                    if (Game1.player.currentLocation.Name == "Forest")
                    {

                        access = new();

                        access.AccessSetup("Forest", Location.LocationData.druid_engineum_name, new(50, 73), new(27, 30));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

            }

            return 0;


        }

        public IconData.relics RelicMistsLocations()
        {

            if(Game1.player.currentLocation is Forest)
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
            else if (Game1.player.currentLocation is MineShaft)
            {

                return IconData.relics.avalant_casing;

            }
            else if (Game1.player.currentLocation is Town)
            {

                return IconData.relics.avalant_needle;

            }
            else if (Game1.player.currentLocation is StardewDruid.Location.Atoll)
            {
                
                return IconData.relics.avalant_measure;

            }

            return IconData.relics.none;

        }

        public IconData.relics RelicBooksLocations()
        {

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

            return IconData.relics.none;

        }

        public int ArtisanRelicQuest()
        {

            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.box_artisan.ToString()))
            {

                return -1;

            }

            for (int i = 0; i < Game1.player.Items.Count; i++)
            {

                Item checkSlot = Game1.player.Items[i];

                // ignore empty slots
                if (checkSlot == null || checkSlot is not StardewValley.Tool toolCheck)
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

            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.box_mortician.ToString()))
            {

                return -1;

            }

            List<string> boneItems = new()
            {
                "(O)580", //"Prehistoric Tibia/100/-300/Arch/Prehistoric Tibia/A thick and sturdy leg bone./Forest .01//",
                "(O)581", //"Prehistoric Skull/100/-300/Arch/Prehistoric Skull/This is definitely a mammalian skull./Mountain .01//",
                "(O)582", //"Skeletal Hand/100/-300/Arch/Skeletal Hand/It's a wonder all these ancient little pieces lasted so long./Beach .01//",
                "(O)583", //"Prehistoric Rib/100/-300/Arch/Prehistoric Rib/Little gouge marks on the side suggest that this rib was someone's dinner./Farm .01//",
                "(O)584", //"Prehistoric Vertebra/100/-300/Arch/Prehistoric Vertebra/A segment of some prehistoric creature's spine./BusStop .01//",
                "(O)585", //"Skeletal Tail/100/-300/Arch/Skeletal Tail/It's pretty short for a tail./UndergroundMine .01//",
                "(O)820", //"Fossilized Skull/100/-300/Basic/Fossilized Skull/It's a perfect specimen!/Island_North .01//",
                "(O)821", //"Fossilized Spine/100/-300/Basic/Fossilized Spine/A column of interlocking vertebrae./Island_North .01//",
                "(O)822", //"Fossilized Tail/100/-300/Basic/Fossilized Tail/This tail has a club-like feature at the tip./Island_North .01//",
                "(O)823", //"Fossilized Leg/100/-300/Basic/Fossilized Leg/A thick and sturdy leg bone./Island_North .01//",
                "(O)824", //"Fossilized Ribs/100/-300/Basic/Fossilized Ribs/Long ago, these ribs protected the body of a large animal./Island_North .01//",
                "(O)825", //"Snake Skull/100/-300/Basic/Snake Skull/A preserved skull that once belonged to a snake./Island_North .01//",
                "(O)826", //"Snake Vertebrae/100/-300/Basic/Snake Vertebrae/It appears this serpent may have been extremely flexible./Island_North .01//",
                "(O)119", //119: "Bone Flute/100/-300/Arch/Bone Flute/It's a prehistoric wind instrument carved from an animal's bone. It produces an eerie tone./Mountain .01 Forest .01 UndergroundMine .02 Town .005/Recipe 2 Flute_Block 150/",
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

            return 0;
        }

        public int ChaosRelicQuest()
        {

            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.box_chaos.ToString()))
            {

                return -1;

            }

            Farm farm = Game1.getFarm();

            foreach (Building building in farm.buildings)
            {
                
                if (building.buildingType.Contains("Deluxe Coop") || building.buildingType.Contains("Deluxe Barn"))
                {

                    return 1;

                }

            }

            return 0;


        }

        public int ProgressRelicQuest(relicsets relicset)
        {

            if (Mod.instance.questHandle.IsComplete(quests[relicset]))
            {

                return -1;

            }

            int count = 0;

            foreach (IconData.relics relic in lines[relicset])
            {

                if (Mod.instance.save.reliquary.ContainsKey(relic.ToString()))
                {

                    count++;

                }

            }

            return count;

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
