
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
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

            [relicsets.companion] = new() { "Companion Tokens", "Things to remember my new friends by", },
            [relicsets.wayfinder] = new() { "Mystic Wayfinders", "Used to reveal hidden passageways", },
            [relicsets.herbalism] = new() { "Apothecary's Tools", "Used to create potent herbal tonics and potions", },
            [relicsets.runestones] = new() { "Old Runestones", "Recovered from foes of the circle", },
            [relicsets.avalant] = new() { "Bronze Artifacts", "Retrieved from fishing spots around the valley", },
            [relicsets.books] = new() { "Preserved Texts", "Writing from the time of dragons", },
            [relicsets.boxes] = new() { "Ornament Boxes", "Left behind by envoys of the Fae Court", },
            [relicsets.other] = new() { "Powerwrought Tools", "Implements from the otherworld", },
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
                title = "Enameled Star Crest",
                relic = IconData.relics.effigy_crest,
                line = RelicData.relicsets.companion,
                function = true,
                cancel = true,
                description = "Given to me by the Forgotten Effigy, the only surviving member of the old Circle of Druids.",
                details = new()
                {
                    "It is untarnished by time, and connected to my companion by some mystery.",
                },
                heldup = "You received The Effigy's Enameled Star Crest! It can be used to summon the Effigy to accompany you on your adventures. Check the relics journal to view further details.",
            };

            relics[IconData.relics.jester_dice.ToString()] = new()
            {
                title = "Embossed Die of the Fates",
                relic = IconData.relics.jester_dice,
                line = RelicData.relicsets.companion,
                function = true,
                cancel = true,
                description = "Given to me by the Jester of Fate, envoy of the Fae Court.",
                details = new()
                {
                    "There's some kind of mechanism inside that jingles when the die is rolled.",
                },
                heldup = "You received The Jester of Fate's Embossed Die of the Fates! It can be used to summon Jester to accompany you on your adventures. Check the relics journal to view further details.",
            };

            relics[IconData.relics.shadowtin_tome.ToString()] = new()
            {
                title = "Compendium on the Ancient War",
                relic = IconData.relics.shadowtin_tome,
                line = RelicData.relicsets.companion,
                function = true,
                cancel = true,
                description = "Given to me by Shadowtin Bear, treasure hunter and aspirant scholar.",
                details = new()
                {
                    "It's a comprehensive collection of Shadowtin's knowledge on the War for the Fallen Star. It is written in shadowtongue.",
                },
                heldup = "You received Shadowtin Bear's Compendium on the Ancient War! It can be used to summon Shadowtin to accompany you on your adventures. Check the relics journal to view further details.",
            };

            relics[IconData.relics.dragon_form.ToString()] = new()
            {
                title = "Dragonomicon",
                relic = IconData.relics.dragon_form,
                line = RelicData.relicsets.companion,
                function = true,
                description = "Retrieved by Shadowtin Bear from the tomb of a Dragon King.",
                heldup = "You received the Dragonomicon! It can be used to configure dragon form. Check the relics journal to view further details.",
            };

            relics[IconData.relics.stardew_druid.ToString()] = new()
            {
                title = "Crumbly Druid Cookie",
                relic = IconData.relics.stardew_druid,
                line = RelicData.relicsets.companion,
                description = "Congratulations on reaching the end of Stardew Druid!",
                details = new()
                {
                    "I hope you had as much fun playing as I have had modding and sharing with the community. " +
                    "I have a lot more planned so please stay subscribed for future updates! " +
                    "Much love, Neosinf.",
                    "If you have the time, please consider endorsing the mod on Nexus and joining us on the Stardew Druid discord server!",
                },
                heldup = "You received the Crumbly Stardew Druid Cookie! It means you've finished all the mod content (as of this version). Check the relics journal to view further details.",
            };

            // ====================================================================
            // Wayfinder relics

            relics[IconData.relics.wayfinder_censer.ToString()] = new()
            {
                title = "Mist Servant's Censer",
                relic = IconData.relics.wayfinder_censer,
                line = RelicData.relicsets.wayfinder,
                function = true,
                description = "The valley is touched by the Voice Beyond the Shore, the Lady of the Isle of Mists.",
                heldup = "You received the Mist Servant's Censer! It can be used to summon the small vessel that provides access to the remote Atoll, from the far eastern side of the beach. Check the relics journal to view further details.",
            };

            relics[IconData.relics.wayfinder_lantern.ToString()] = new()
            {
                title = "Star Guardian Lantern",
                relic = IconData.relics.wayfinder_lantern,
                line = RelicData.relicsets.wayfinder,
                function = true,
                cancel = true,
                description = "The order of Star Guardians preceded the Church of Bats, and placed hundreds of lanterns throughout their cavernholds in the mountain",
                heldup = "You received the Star Guardian Lantern! It can be used to access the Chapel of the Stars on level 60 of the mines. Check the relics journal to view further details.",
            };

            relics[IconData.relics.wayfinder_water.ToString()] = new()
            {
                title = "Luminous Water of the Sacred Spring",
                relic = IconData.relics.wayfinder_water,
                line = RelicData.relicsets.wayfinder,
                function = true,
                cancel = true,
                description = "The waters of the sacred spring resist the brazen heat of the lavaflows deep under the mountain, where the terror beneath resides",
                heldup = "You received the Luminous Water of the Sacred Spring! It can be used to access the Molten Lair on level 100 of the mines. Check the relics journal to view further details.",
            };

            relics[IconData.relics.wayfinder_eye.ToString()] = new()
            {
                title = "Eye of the Warble",
                relic = IconData.relics.wayfinder_eye,
                line = RelicData.relicsets.wayfinder,
                function = true,
                description = "A Warble can peer beyond the bounds of space and time.",
                details = new()
                {
                    "Squeeze the eye and all the frustrations of the current moment will seem to fade away.",
                },
                heldup = "You received the Eye of the Warble! It can be used to warp to map entrances. Check the relics journal to view further details.",
            };

            relics[IconData.relics.wayfinder_ceremonial.ToString()] = new()
            {
                title = "Calico Ceremonial Lamp",
                relic = IconData.relics.wayfinder_ceremonial,
                line = RelicData.relicsets.wayfinder,
                function = true,
                cancel = true,
                description = "The civilisation of the calico shamans centered on reverence for a dragon. When the ancient being perished, so did the shaman's customs.",
                heldup = "You received the Calico Ceremonial Lamp! It can be used to access the Tomb of Tyrannus in the skull caverns. Check the relics journal to view further details.",
            };

            relics[IconData.relics.wayfinder_dwarf.ToString()] = new()
            {
                title = "Dwarven Essence Cell",
                relic = IconData.relics.wayfinder_dwarf,
                line = RelicData.relicsets.wayfinder,
                function = true,
                cancel = true,
                description = "The dwarves were the first elemental race to harness the technological capabilities of free essence.",
                heldup = "You received the Dwarven Essence Cell! It can be used to access the Shrine Engine Room. Check the relics journal to view further details.",
            };

            // ====================================================================
            // Herbalism Relics

            relics[IconData.relics.herbalism_mortar.ToString()] = new()
            {
                title = "Circle Mortar and Pestle",
                relic = IconData.relics.herbalism_mortar,
                line = RelicData.relicsets.herbalism,
                description = "One of the tools left behind by the old circle of Druids.",
                details = new()
                {
                    "Provides the means to brew basic potions.",
                    "The bowl is pleased."
                },
                heldup = "You received the Circle Mortar and Pestle! You are now able to brew basic potions. Check the potions journal to view further details.",
            };

            relics[IconData.relics.herbalism_pan.ToString()] = new()
            {
                title = "The First Pan",
                relic = IconData.relics.herbalism_pan,
                line = RelicData.relicsets.herbalism,
                description = "It belonged to the first farmer, but is useless to the Effigy, who does not consume organic matter",
                details = new()
                {
                    "Provides the means to brew better potions.",
                },
                heldup = "You received The First Pan! You are now able to brew good potions. Check the potions journal to view further details.",
            };

            relics[IconData.relics.herbalism_still.ToString()] = new()
            {
                title = "Sacred Water Still",
                relic = IconData.relics.herbalism_still,
                line = RelicData.relicsets.herbalism,
                description = "Before they were reformed into the Order of the Guardians of the Star, some amongst the human custodians of the sacred spring were hobby brewers.",
                details = new()
                {
                    "Provides the means to brew good potions.",
                },
                heldup = "You received the Sacred Water Still! You are now able to brew great potions. Check the potions journal to view further details.",
            };

            relics[IconData.relics.herbalism_crucible.ToString()] = new()
            {
                title = "Dark Crucible of Yoba",
                relic = IconData.relics.herbalism_crucible,
                line = RelicData.relicsets.herbalism,
                description = "From the smelter-halls of the Artisans of the Fates, who delight in drawing things down to their very essence.",
                details = new()
                {
                    "Provides the means to brew great potions.",
                },
                heldup = "You received the Dark Crucible Of Yoba! You are now able to brew a special resource; Faeth! Check page 2 of the potions journal for more details.",
            };


            relics[IconData.relics.herbalism_gauge.ToString()] = new()
            {
                title = "Splendid Ether Gauge",
                relic = IconData.relics.herbalism_gauge,
                line = RelicData.relicsets.herbalism,
                description = "Required by most shadowfolk machinery to measure ether pressure.",
                details = new()
                {
                    "Provides the means to brew the best potions.",
                },
                heldup = "You received the Splendid Ether Gauge! You are now able to brew the best potions and a special resource; Aether! Check page 2 of the potions journal for more details.",
            };


            // ====================================================================
            // Circle Runestones

            relics[IconData.relics.runestones_spring.ToString()] = new()
            {
                title = "Runestone of the Sacred Spring",
                relic = IconData.relics.runestones_spring,
                line = RelicData.relicsets.runestones,
                description = "Carried by the high cleric of bats.",
                details = new(){
                    "The bearer of this runestone adopted the customs and zealotry of those that once guarded the sacred mountain spring.",
                    "Ask the energies of the Weald in the Druid grove to learn more.",
                },
                heldup = "You received the Runestone of the Sacred Spring! The energies of the weald might know more about this artifact. Check the relics journal to view further details.",
            };

            relics[IconData.relics.runestones_moon.ToString()] = new()
            {
                title = "Runestone of the Crooked Moon",
                relic = IconData.relics.runestones_moon,
                line = RelicData.relicsets.runestones,
                description = "Carried by the Jellyking.",
                hint = "One of the foes I'll encounter on my journey should have this runestone.",
                details = new()
                {
                    "This runestone was once covered in pumpkin muck and slime. It had been swallowed by a foul spirit of the forest, the Jellyking, who took great delight in spiting the old circle of druids",
                },
                heldup = "You received the Runestone of the Crooked Moon! The energies of the weald might know more about this artifact. Check the relics journal to view further details.",
            };

            relics[IconData.relics.runestones_farm.ToString()] = new()
            {
                title = "Runestone of the New Farm",
                relic = IconData.relics.runestones_farm,
                line = RelicData.relicsets.runestones,
                description = "Carried by a Seafaring Phantom.",
                hint = "One of the foes I'll encounter on my journey should have this runestone.",
                details = new()
                {
                    "The Effigy believes this runestone symbolises the hopes of those that restored the farmholds that were torched during the old war. One of those farmers must have lost it to the sea.",
                },
                heldup = "You received the Runestone of the New Farm! The energies of the weald might know more about this artifact. Check the relics journal to view further details.",
            };

            relics[IconData.relics.runestones_cat.ToString()] = new()
            {
                title = "Runestone of the Old Cat",
                relic = IconData.relics.runestones_cat,
                line = RelicData.relicsets.runestones,
                description = "Carried by a Lesser Dragon.",
                hint = "One of the foes I'll encounter on my journey should have this runestone.",
                details = new()
                {
                    "This runestone was carried by a champion of the Star Guardians who was also a member of the Circle of Druids, until they ran foul of a powerful dragon in the depths of the lava caverns.",
                },
                heldup = "You received the Runestone of the Old Cat! The energies of the weald might know more about this artifact. Check the relics journal to view further details.",
            };

            // ====================================================================
            // Avalant Relics

            relics[IconData.relics.avalant_disc.ToString()] = new()
            {
                title = "Ancient Bronze Disc",
                relic = IconData.relics.avalant_disc,
                line = RelicData.relicsets.avalant,
                description = "The base of an ancient device,",
                hint = "Located in the waters of the local forest.",
                details = new()
                {
                    "Ask the servants of the Lady Beyond at the Atoll monument to learn more.",

                },
                heldup = "You received a Ancient Bronze Disc! The servants of the Lady Beyond the Shore might know more about this artifact.",
            };

            relics[IconData.relics.avalant_chassis.ToString()] = new()
            {
                title = "Ancient Bronze Chassis",
                relic = IconData.relics.avalant_chassis,
                line = RelicData.relicsets.avalant,
                description = "The structural core of an ancient device,",
                hint = "Located in the waters of the mountain lake.",

                heldup = "You received a Ancient Bronze Chassis! The servants of the Lady Beyond the Shore might know more about this artifact.",
            };

            relics[IconData.relics.avalant_gears.ToString()] = new()
            {
                title = "Ancient Bronze Gears",
                relic = IconData.relics.avalant_gears,
                line = RelicData.relicsets.avalant,
                description = "A mechanical component from the core of an ancient device.",
                hint = "Located in the waters of the local beach.",

                heldup = "You received a set of Ancient Bronze Gears! The servants of the Lady Beyond the Shore might know more about this artifact.",
            };

            relics[IconData.relics.avalant_casing.ToString()] = new()
            {
                title = "Ancient Bronze Casement",
                relic = IconData.relics.avalant_casing,
                line = RelicData.relicsets.avalant,
                description = "The top casement of an ancient device.",
                hint = "Located in the waters of local caverns.",

                heldup = "You received a Ancient Bronze Casement! The servants of the Lady Beyond the Shore might know more about this artifact.",
            };

            relics[IconData.relics.avalant_needle.ToString()] = new()
            {
                title = "Ancient Bronze Needle",
                relic = IconData.relics.avalant_needle,
                line = RelicData.relicsets.avalant,
                description = "A measuring needle that forms part of a larger device.",
                hint = "Located in the waters of the town river.",

                heldup = "You received a Strange Bronze Needle! The servants of the Lady Beyond the Shore might know more about this artifact.",
            };

            relics[IconData.relics.avalant_measure.ToString()] = new()
            {
                title = "Ancient Bronze Measure",
                relic = IconData.relics.avalant_measure,
                line = RelicData.relicsets.avalant,
                description = "A measuring wheel that forms part of a larger device.",
                hint = "Located in the waters of a secluded atoll.",

                heldup = "You received an Ancient Bronze Measure! The servants of the Lady Beyond the Shore might know more about this artifact.",
            };

            // ====================================================================
            // Preserved Texts

            relics[IconData.relics.book_wyrven.ToString()] = new()
            {
                title = "Knight Wyrven's Journal",
                relic = IconData.relics.book_wyrven,
                line = RelicData.relicsets.books,
                function = true,
                description = "A journal that serves as an annal in the lost chronicles of the Guardians of the Star.",
                details = new()
                {
                    "Ask the Revenant about the book to learn more about its use.",
                    "Recovered by Shadowtin's mercenaries from a repository in the deep quarry tunnels.",

                },
                heldup = "You received Knight Wyrven's Journal! It appears to have belonged to a Holy Warrior of the Order of the Guardians of the Star. The Revenant might know more about this artifact.",

                narrative = new()
                {
                    
                    "Summer, Second Year",
                    "A low court of the Fates has been established to administer the valley in the aftermath of the war. " +
                    "It sits on the banks of the mountain stream that flows from the sacred spring, and the Justiciar of the Fates resides over the proceedings. " +
                    "The Justiciar's prosecutor, the Reaper, continues to grow in reverence with the assembly. " +
                    "His obsession with the recovery of the hidden Star-born is on full display, and the refugees listen fervently to his rhetoric. " +
                    "They applaud the viciousness with which the Mortician demonstrates against the surviving elderborn. " +
                    "They desire justice for the destruction of lives and families, but what comfort can be found in the humiliation of an endangered race. " +
                    "So few civilisations remain after the devastation of war.",
                    
                    "Winter, Second Year",
                    "The calico shamans have paid the final tributes to their draconic master, Tyrannus Jin, and discarded the yoke of their servitude with the ancient one. " +
                    "I was invited to play a symbolic role in the ceremony. So I travelled to the skull caverns, to a mausoleum big enough to house the wasted bones of a dragon. " +
                    "We exchanged gifts of sacred water at the dais, to represent the reconciliation of the Guardians of the Star with our once great adversaries. " +
                    "Their water was pure, but I knew it was doused in their tears, and I drank to share their pain. " +
                    "For I played my part in the destruction wrought by the Bull of the Heavens in his vendetta against the dragons. " +
                    "As I watched them bury their old traditions, I felt a breeze sweeping through the cavern, an impossible breeze, as we were still far under the desolation that claimed the lush nation of Calico. " +
                    "I know now it was a stream of ether. " +
                    "The shamans, either knowingly or unknowingly, revealed it to me, the ephemeral, transformational substance that flows between realms. " +
                    "The legacy of Dragons. " +
                    "I intend to explore the possibilities of this purchase, and perhaps transform the Guardians into a force to rival the unchallenged authority of the Fates.",
                    
                    "Spring, Third year",
                    "The Justiciar intends to close the low court at the end of summer. " +
                    "The crowmother has already lost interest in hearing appeals and accusations, and has convinced her peers to vest authority in the Circle of Druids, " +
                    "to take responsibility for the renewal of the valley and the protection of the sacred space. Were it so simple. " +
                    "The Stream of Chaos feasts on the residue of the war, and produces something unlike cosmic essence, or ancient ether, but of the world, elemental, a new magic to threaten the old ways. " +
                    "The Crowmother has threatened to forsake the mortal plane for lack of order and continuity. ",
                    
                    "Summer, Third Year",
                    "I have incorporated the remainder of my order into the circle. It was the prudent choice considering our fragmented morale and numbers. " +
                    "The court has been disestablished, but the unresolved issue of the missing Star-born does not sit well with the Justiciar and Reaper. " +
                    "They are desperate to bring the Bull of the Heavens before the high Fae Court to answer for his actions. " +
                    "As far as I'm aware, our great general vanished with the Elderborn prince, and that is the story we have stuck too. " +
                    "Still, the Reaper's acolytes continue to harrass us in the public forums. " +
                    "The Hound of Chaos, long since banished from the low court for stirring trouble, has been seen amongst their number. " +
                    "I fear an escalation.",

                }

            };

            relics[IconData.relics.book_letters.ToString()] = new()
            {
                title = "Letters to the Lady",
                relic = IconData.relics.book_letters,
                line = RelicData.relicsets.books,
                function = true,
                description = "A bundle of letters received by the Lady Beyond the Shore from her former betrothed, when she still served at the court of the Two Kings. ",
                hint = "Sealed within ether in Cindersap Forest",
                details = new()
                {
                    "Preserved as a record of the events that led to the War for the Fallen Star.",

                },
                heldup = "You received the Letters to the Lady! They appear to have been preserved as a record of the War for the Fallen Star. The Revenant might know more about this artifact.",

                narrative = new()
                {

                    "Ether soaked letter #1",
                    "I strolled alone, in the valley untouched by the war of our great kin, and I came upon a spring that mirrors the celestial plane. " +
                    "I saw her reflection, and from the mirrored side, she saw mine. " +
                    "I made a garden for her here, where the fruits grow sweetest, and dyed the blossoms with all the burgeoning colours of my heart. " +
                    "Her light dappled over the grove, bringing it to life. " +
                    "She was pleased, and I was captivated. My engagement to you cannot hope to satisfy the yearning I have. I am deeply sorry.",

                    "Ether soaked letter #2",
                    "I climbed the heights of the sacred mountain, and there, I constructed a marriage altar. I looked upward for many nights. " +
                    "Her light came again, and confirmed her own yearning. " +
                    "I convinced the Benevolus Prime to conduct a rite of union. " + 
                    "By the purity and power of our vows, the barriers between our realms fell momentarily, and we were united. " +
                    "This joyous event marks the resolution of my betrothal to you. Farewell.", 

                    "Ether soaked letter #2, annotation",
                    "The celestial cannot be accommodated. " +
                    "The court is enraptured by her brilliance, and all have come to covet her unworldly beauty. " +
                    "I am beset by accusations of envy and indignation, and my counsel is ignored. " +
                    "I fear the lust of dragons most of all. " +
                    "My ears remain alert for the sound of an ethereal roar, or the beating of ancient wings above the palace. " +
                    "The most threatening omen is the red star of the broken constellation. It is closer. It is angry. " +
                    "For the sake of peace, the Star must return to her family.", 

                    "Ether soaked letter #3",
                    "Dear Lady, I have bargained with the crone, and acquired the means to remain whole with my beloved. " +
                    "We will flee the vanity of the masters, and make a sanctuary in the secretest place, in the darkened plane, away from the prying eyes of cats and owls. " +
                    "I intend to honour my side of the bargain, and despite how it will eventuate, " +
                    "I intend for you to inherit my title and duties, as I recognise now you were always the best hope for the future of our kin.",

                    "Ether soaked letter #3, annotation",
                    "Enjoy the false comfort of the grave you've dug for yourself, traitor. " +
                    "The follower endures. He will become my avenger, I swear, I will remain until I see his power manifest in your ruin.",

                }

            };

            relics[IconData.relics.book_manual.ToString()] = new()
            {
                title = "Crowmother's Manual",
                relic = IconData.relics.book_manual,
                line = RelicData.relicsets.books,
                function = true,
                description = "A manual of instructions for the creation and maintenance of magical artifacts, in unnaturally perfect print.",
                hint = "Sealed within ether in the mountain passes",
                details = new()
                {
                    "Only two passages are intelligible to human readers.",

                },
                heldup = "You received Crowmother's Manual! It appears to have been gifted to the Circle of Druids by an Artisan of the Fates. The Revenant might know more about this artifact.",

                narrative = new()
                {
                    "Third Passage, Step 2",
                    "The shell is to be constructed of the roots of once living things, that every fibre may be amenable to the flow of life essence. " +
                    "The craft must appear dignified and reasonable to the human eye. " +
                    "Be careful to weave the roots tightly and securely together to prevent the degradation of the shell from tears and defects. " +
                    "The completed construction is to be submitted to an ancient one for appraisal as a suitable house of the soulcore. " +
                    "(The First of the Benevolent might be amenable to providing this service for you in light of their penance). " +
                    
                    "Third Passage, Step 5",
                    "If the rite is successful, the shell will begin to recomposite and reshape itself from the influence of the soulcore, forming natural defences against blights and decay. " +
                    "A suitable dwelling and appropriate fashions will need to be provided for whatever form results from the recomposition process.",

                    "Fourth Passage, Step 1",
                    "From the human candidates, two must be selected for their dichotomy of spirit, one to the hope for prosperity, " +
                    "and one to the hope for renewal, for they must both have hope for the change of seasons and a commitment to guard it. " +
                    "As long as two remain to speak for the monarchy, the energies of the Weald will not forsake this realm, " +
                    "and the special qualities of this land will perpetuate far into the zenith of humanity.",

                }

            };

            relics[IconData.relics.book_druid.ToString()] = new()
            {
                title = "Book of Succession",
                relic = IconData.relics.book_druid,
                line = RelicData.relicsets.books,
                function = true,
                description = "The preface implies the text is for the benefit of the successor of the Archdruid of the Circle. ",
                hint = "Sealed within ether in the secluded atoll",
                details = new()
                {

                    "The text has not survived the time spent in ethereal stasis. " +
                    "All that remains is the preface, some cooking and gardening tips, and a poem dribbled into the underside of the binding."

                },
                heldup = "You received the Book of Succession! It appears to have been written by a former member of the Circle of Druids. The Revenant might know more about this artifact.",

                narrative = new()
                {
                    
                    "We healed and we sealed and it all went to plan.",
                    "Our hands intertwined as they restored the land.",
                    "But mine became peeled, old and blistered",
                    "and to you it seemed like nothing was right",

                    "You reveal how you feel but your face is a mask,",
                    "then you leave and believe that I'm set to the task.",
                    "My mind remains steeled, but my heart is afflicted,",
                    "without you it seems like nothing is right.",

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
                title = "Measurer's Box",
                relic = IconData.relics.box_measurer,
                line = RelicData.relicsets.boxes,
                description = "A box that once carried measuring pins used by the Priestly order of the Fates.",
                details = new()
                {
                    "Ask Buffin about the box to learn more about its use.",
                    "Found in the possession of Thanatoshi, the Twilight Reaper",
                },
                heldup = "You received the Measurer's Pin! This might have belonged to a member of the Fae Court. Buffin should know more about it.",
            };

            relics[IconData.relics.box_mortician.ToString()] = new()
            {
                title = "Mortician's Box",
                relic = IconData.relics.box_mortician,
                line = RelicData.relicsets.boxes,
                description = "An empty box, left amongst the monuments of the disused low court of the Fates, a sign the Morticians were in attendance.",
                hint = "Buffin suggests holding a prehistoric fossil or bone in the presence of the owl monument.",
                details = new()
                {
                    "Masayoshi, Justiciar of the Fates, exchanged boxes with his brother Thanatoshi before the latter left on the trial of the fugitive Starborn.",
                },
                heldup = "You received the Mortician's Shears! This might have belonged to a member of the Fae Court. Buffin should know more about it.",
            };

            relics[IconData.relics.box_artisan.ToString()] = new()
            {
                title = "Artisan's Box",
                relic = IconData.relics.box_artisan,
                line = RelicData.relicsets.boxes,
                description = "The box has the same dimensions and materials as the others, yet is entirely free of quirk or misalignment. This must have belonged to an Artisan of the Fates.",
                hint = "Buffin suggests bringing an implement forged in iridium or better to the crow monument to please the artisans.",

                heldup = "You received the Artisan's Box! This might have belonged to a member of the Fae Court. Buffin should know more about it.",
            };

            relics[IconData.relics.box_chaos.ToString()] = new()
            {
                title = "Box of Chaos",
                relic = IconData.relics.box_chaos,
                line = RelicData.relicsets.boxes,
                description = "An hierloom of the Hound of Chaos, one of the first Agents of Chaos to visit the valley.",
                hint = "Buffin suggests building a deluxe coop or barn to raise animals that please the foxes of Chaos.",

                heldup = "You received the Box of Chaos! This might have belonged to a member of the Fae Court. Buffin should know more about it.",
            };

            // ======================================================================
            // Other relics

            relics[IconData.relics.crow_hammer.ToString()] = new()
            {
                title = "Crow Hammer",
                relic = IconData.relics.crow_hammer,
                line = RelicData.relicsets.other,
                description = "The Effigy believes this implement was used by his former mentors to craft his material form.",
                details = new(){
                    "A symbol of a crow is engraved into the head.",
                    "The herbalist bench in the druid grove provides an option to break all geodes.",
                },
                heldup = "You received the Crow Hammer! This provides a new craft option at the herbalist bench in the Druid Grove.",
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

                    return "Click to summon The Effigy. Rightclick to dismiss.";

                case IconData.relics.jester_dice:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    return "Click to summon The Jester of Fate. Rightclick to dismiss.";

                case IconData.relics.shadowtin_tome:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    return "Click to summon Shadowtin Bear. Rightclick to dismiss.";

                case IconData.relics.wayfinder_censer:

                    return "Click when at the beach to summon a vessel to the Remote Atoll.";

                case IconData.relics.wayfinder_lantern:

                    return "Click when on level 60 of the mines to open the passage way to the Chapel of the Stars. Rightclick to close.";

                case IconData.relics.wayfinder_water:

                    return "Click when on level 100 of the mines to open the passage way to the Molten Lair. Rightclick to close.";

                case IconData.relics.wayfinder_eye:

                    return "Click to warp to the map entrance, prioritising the direction you're facing.";

                case IconData.relics.wayfinder_ceremonial:

                    return "Click when in the entrance to the skull caverns to open the passage way to the Tomb of Tyrannus. Rightclick to close.";

                case IconData.relics.wayfinder_dwarf:

                    return "Click while on the arrowhead island in the forest to open the entrance to the Shrine Engine Room. Rightclick to close.";

                case IconData.relics.dragon_form:

                    return "Click to study the guise of the ancient ones.";

                case IconData.relics.book_wyrven:
                case IconData.relics.book_letters:
                case IconData.relics.book_manual:
                case IconData.relics.book_druid:

                    return "Click to read";

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

                case IconData.relics.dragon_form:
                case IconData.relics.book_wyrven:
                case IconData.relics.book_letters:
                case IconData.relics.book_manual:
                case IconData.relics.book_druid:

                    return 2;

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
