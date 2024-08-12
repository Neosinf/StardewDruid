
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
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using xTile.Dimensions;
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
            other,
            herbalism,
            tactical,
            runestones,
            avalant,
            books,
            boxes,

        }

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

        };

        public Dictionary<relicsets, string> quests = new()
        {
            [relicsets.tactical] = QuestHandle.relicTactical,
            [relicsets.runestones] = QuestHandle.relicWeald,
            [relicsets.avalant] = QuestHandle.relicMists,
            [relicsets.books] = QuestHandle.relicEther,
            [relicsets.boxes] = QuestHandle.relicFates,
        };

        public Dictionary<string, Relic> reliquary = new();

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

        public void Ready()
        {
            
            if (Mod.instance.magic)
            {

                return;

            }

            for (int i = Mod.instance.save.reliquary.Count - 1; i >= 0; i--)
            {

                string relic = Mod.instance.save.reliquary.ElementAt(i).Key;

                if (!reliquary.ContainsKey(relic))
                {

                    Mod.instance.save.reliquary.Remove(relic);

                }

            }

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


                } else
                if (!HasRelic(lines[set][0]))
                {

                    continue;

                }

                for (int i = 0; i < lines[set].Count; i++)
                {

                    IconData.relics relicName = lines[set][i];

                    Journal.ContentComponent content = new(ContentComponent.contentTypes.relic, relicName.ToString());

                    content.relics[0] = relicName;

                    content.relicColours[0] = Color.White;

                    if (!HasRelic(relicName))
                    {

                        switch (set)
                        {
                            case relicsets.tactical:
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

                for (int i = 0; i < 6 - lines[set].Count; i++)
                {

                    Journal.ContentComponent content = new(ContentComponent.contentTypes.relic, set.ToString() + i.ToString());

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

            foreach (KeyValuePair<relicsets, List<string>> section in titles)
            {

                if (section.Key == relicsets.avalant)
                {

                    if (ProgressRelicQuest(relicsets.avalant) == 0)
                    {

                        continue;

                    }

                }
                else
                if (!HasRelic(lines[section.Key][0]))
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
            else if (Mod.instance.save.reliquary[id] < update)
            {

                Mod.instance.save.reliquary[id] = update;

            }

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

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Effigy) +
                                DialogueData.Strings(DialogueData.stringkeys.joinedPlayer), 0, true);

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

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Jester) +
                                DialogueData.Strings(DialogueData.stringkeys.joinedPlayer), 0, true);

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

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Shadowtin) +
                                DialogueData.Strings(DialogueData.stringkeys.joinedPlayer), 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.wayfinder_stone:

                    if (Game1.player.currentLocation is Farm || Game1.player.currentLocation is FarmHouse)
                    {

                        Game1.warpFarmer(LocationData.druid_grove_name, 21, 13, 0);

                        Game1.xLocationAfterWarp = 21;

                        Game1.yLocationAfterWarp = 13;

                        return 1;

                    }                    
                    
                    if (Game1.player.currentLocation is Grove)
                    {

                        Wand wand = new();

                        wand.lastUser = Game1.player;

                        wand.DoFunction(Game1.player.currentLocation, 0, 0, 0, Game1.player);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_pot:

                    if (Game1.player.currentLocation.Name == "UndergroundMine20")
                    {

                        Event.Access.AccessHandle lanternAccess = new();

                        lanternAccess.AccessSetup("UndergroundMine20", Location.LocationData.druid_spring_name, new(24, 13), new(27, 31));

                        lanternAccess.AccessCheck(Game1.player.currentLocation);

                        Mod.instance.CastDisplay(Mod.instance.Helper.Translation.Get("RelicData.309.8"));

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_censer:

                    if (Game1.player.currentLocation is Beach)
                    {

                        (Mod.instance.locations[LocationData.druid_atoll_name] as Atoll).AddBoatAccess(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_key:

                    if (Game1.player.currentLocation is Town)
                    {

                        if (Vector2.Distance(Game1.player.Position, new Vector2(47, 88) * 64) <= 480)
                        {

                            SpellHandle warp = new(Mod.instance.locations[LocationData.druid_graveyard_name], new Vector2(28, 29), new Vector2(0, 0)) { type = SpellHandle.spells.warp, };

                            Mod.instance.spellRegister.Add(warp);

                            return 1;

                        }
                    }

                    break;

                case IconData.relics.wayfinder_lantern:

                    if (Game1.player.currentLocation.Name == "UndergroundMine60")
                    {

                        Event.Access.AccessHandle lanternAccess = new();

                        lanternAccess.AccessSetup("UndergroundMine60", Location.LocationData.druid_chapel_name, new(24, 13), new(27, 30));

                        lanternAccess.AccessCheck(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_glove:

                    if (Game1.player.currentLocation is Forest)
                    {

                        if (Vector2.Distance(Game1.player.Position, new Vector2(79, 78) * 64) <= 1560)
                        {

                            SpellHandle warp = new(Mod.instance.locations[LocationData.druid_clearing_name], new Vector2(28, 8), new Vector2(0, 2)) { type = SpellHandle.spells.warp, };

                            Mod.instance.spellRegister.Add(warp);

                            return 1;

                        }
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
                    else if (Game1.player.currentLocation is Town)
                    {

                        if (Vector2.Distance(Game1.player.Position, new Vector2(98, 8) * 64) <= 640)
                        {

                            SpellHandle warp = new(Mod.instance.locations[LocationData.druid_court_name], new Vector2((int)28, (int)29), new Vector2(0, 0)) { type = SpellHandle.spells.warp, };

                            Mod.instance.spellRegister.Add(warp);

                            return 1;

                        }

                        destination = WarpData.WarpEntrance(Game1.player.currentLocation, Game1.player.Position);

                    }
                    else if (Game1.player.currentLocation is Court)
                    {

                        GameLocation backToTown = Game1.getLocationFromName("Town");

                        destination = WarpData.WarpEntrance(Game1.getLocationFromName("Town"), new Vector2(98, 8) * 64);

                        SpellHandle warp = new(backToTown, new Vector2((int)destination.X, (int)destination.Y), new Vector2(0, 0)) { type = SpellHandle.spells.warp, };

                        Mod.instance.spellRegister.Add(warp);

                        return 1;

                    }
                    else
                    {
                        destination = WarpData.WarpEntrance(Game1.player.currentLocation, Game1.player.Position);

                    }

                    if (destination != Vector2.Zero)
                    {

                        SpellHandle teleport = new(Game1.player.currentLocation, destination, Game1.player.Position);

                        teleport.type = SpellHandle.spells.teleport;

                        Mod.instance.spellRegister.Add(teleport);

                        Mod.instance.AbortAllEvents();

                    }
                    else
                    {

                        Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.noWarpPoint));

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

                    
                    if (Game1.player.currentLocation is Clearing clearing)
                    {
                        
                        if (!clearing.accessOpen)
                        {

                            clearing.OpenAccessDoor();

                            clearing.playSound(SpellHandle.sounds.Ship.ToString());

                        }

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

                            Mod.instance.CastMessage(
                                CharacterHandle.CharacterTitle(CharacterHandle.characters.Effigy) +
                                DialogueData.Strings(DialogueData.stringkeys.returnedHome), 0, true);

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

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Jester) +
                                DialogueData.Strings(DialogueData.stringkeys.returnedHome), 0, true);

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

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Shadowtin) +
                                DialogueData.Strings(DialogueData.stringkeys.returnedHome), 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.wayfinder_pot:

                    if (Game1.player.currentLocation.Name == "UndergroundMine20")
                    {

                        access = new();

                        access.AccessSetup("UndergroundMine20", Location.LocationData.druid_spring_name, new(24, 13), new(27, 31));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

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


            }

            return 0;


        }

        public IconData.relics RelicTacticalLocations()
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeWeald) || !Context.IsMainPlayer)
            {

                return IconData.relics.none;

            }

            if (Game1.player.currentLocation is Forest)
            {

                return IconData.relics.tactical_cell;

            }
            else if (Game1.player.currentLocation is Woods)
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
            if (!Context.IsMainPlayer)
            {

                return -1;

            }

            if (HasRelic(IconData.relics.box_mortician))
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
            if (!Context.IsMainPlayer)
            {

                return -1;

            }

            if (HasRelic(IconData.relics.box_chaos))
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

                if (HasRelic(relic))
                {

                    count++;

                }

            }

            return count;

        }

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

        };

        public static Dictionary<string, Relic> RelicsList()
        {

            Dictionary<string, Relic> relics = new();

            // ====================================================================
            // Companion Tokens

            relics[IconData.relics.effigy_crest.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.41"),
                relic = IconData.relics.effigy_crest,
                line = RelicData.relicsets.companion,
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
                line = RelicData.relicsets.companion,
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
                line = RelicData.relicsets.companion,
                function = true,
                cancel = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.76"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.79"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.81"),
            };

            if (Context.IsMainPlayer)
            {
                relics[IconData.relics.effigy_crest.ToString()].details.Add(Mod.instance.Helper.Translation.Get("RelicData.665"));
                relics[IconData.relics.jester_dice.ToString()].details.Add(Mod.instance.Helper.Translation.Get("RelicData.676"));
                relics[IconData.relics.shadowtin_tome.ToString()].details.Add(Mod.instance.Helper.Translation.Get("RelicData.687"));
            }

            relics[IconData.relics.dragon_form.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.86"),
                relic = IconData.relics.dragon_form,
                line = RelicData.relicsets.companion,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.90"),
                details = new(){
                    Mod.instance.Helper.Translation.Get("RelicData.715"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.91"),
            };

            relics[IconData.relics.stardew_druid.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.96"),
                relic = IconData.relics.stardew_druid,
                line = RelicData.relicsets.companion,
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

            // ====================================================================
            // Wayfinder relics

            relics[IconData.relics.wayfinder_pot.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.309.3"),
                relic = IconData.relics.wayfinder_pot,
                line = RelicData.relicsets.wayfinder,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.309.4"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.309.5"),
                    Mod.instance.Helper.Translation.Get("RelicData.309.7"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.309.6"),
            };

            relics[IconData.relics.wayfinder_censer.ToString()] = new ()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.115"),
                relic = IconData.relics.wayfinder_censer,
                line = RelicData.relicsets.wayfinder,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.119"),
                details = new (){Mod.instance.Helper.Translation.Get("RelicData.691"),},
                heldup = Mod.instance.Helper.Translation.Get("RelicData.120"),
            };

            relics[IconData.relics.wayfinder_lantern.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.125"),
                relic = IconData.relics.wayfinder_lantern,
                line = RelicData.relicsets.wayfinder,
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
                line = RelicData.relicsets.wayfinder,
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
                line = RelicData.relicsets.wayfinder,
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
                line = RelicData.relicsets.wayfinder,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.177"),
                details = new() { Mod.instance.Helper.Translation.Get("RelicData.711"), },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.178"),
            };

            // ======================================================================
            // Other relics

            relics[IconData.relics.wayfinder_stone.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.313.1"),
                relic = IconData.relics.wayfinder_stone,
                line = RelicData.relicsets.other,
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
                line = RelicData.relicsets.other,
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
                line = RelicData.relicsets.other,
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
                line = RelicData.relicsets.other,
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
                line = RelicData.relicsets.other,
                function = true,
                description = Mod.instance.Helper.Translation.Get("RelicData.151"),
                details = new()
                {Mod.instance.Helper.Translation.Get("RelicData.703"),
                    Mod.instance.Helper.Translation.Get("RelicData.154"),
                    Mod.instance.Helper.Translation.Get("RelicData.155"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.156"),
            };

            // ====================================================================
            // Tactical Relics

            relics[IconData.relics.tactical_discombobulator.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.310.1"),
                relic = IconData.relics.tactical_discombobulator,
                line = RelicData.relicsets.tactical,
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
                line = RelicData.relicsets.tactical,
                description = Mod.instance.Helper.Translation.Get("RelicData.310.6"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.310.7"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.310.8"),
            };

            relics[IconData.relics.tactical_cell.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.310.9"),
                relic = IconData.relics.tactical_cell,
                line = RelicData.relicsets.tactical,
                description = Mod.instance.Helper.Translation.Get("RelicData.310.10"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.310.11"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.310.12"),
            };

            relics[IconData.relics.tactical_lunchbox.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.310.13"),
                relic = IconData.relics.tactical_lunchbox,
                line = RelicData.relicsets.tactical,
                description = Mod.instance.Helper.Translation.Get("RelicData.310.14"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.310.15"),
                heldup = Mod.instance.Helper.Translation.Get("RelicData.310.16"),
            };

            relics[IconData.relics.tactical_peppermint.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.310.17"),
                relic = IconData.relics.tactical_peppermint,
                line = RelicData.relicsets.tactical,
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
                line = RelicData.relicsets.herbalism,
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
                line = RelicData.relicsets.herbalism,
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
                line = RelicData.relicsets.herbalism,
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
                line = RelicData.relicsets.herbalism,
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
                line = RelicData.relicsets.herbalism,
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
                line = RelicData.relicsets.runestones,
                description = Mod.instance.Helper.Translation.Get("RelicData.260"),
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
                line = RelicData.relicsets.runestones,
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
                line = RelicData.relicsets.runestones,
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
                line = RelicData.relicsets.runestones,
                description = Mod.instance.Helper.Translation.Get("RelicData.301"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.302"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("RelicData.305"),
                },
                heldup = Mod.instance.Helper.Translation.Get("RelicData.307"),
            };

            // ====================================================================
            // Avalant Relics

            relics[IconData.relics.avalant_disc.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.315"),
                relic = IconData.relics.avalant_disc,
                line = RelicData.relicsets.avalant,
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
                line = RelicData.relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("RelicData.333"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.334"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.336"),
            };

            relics[IconData.relics.avalant_gears.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.341"),
                relic = IconData.relics.avalant_gears,
                line = RelicData.relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("RelicData.344"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.345"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.347"),
            };

            relics[IconData.relics.avalant_casing.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.352"),
                relic = IconData.relics.avalant_casing,
                line = RelicData.relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("RelicData.355"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.356"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.358"),
            };

            relics[IconData.relics.avalant_needle.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.363"),
                relic = IconData.relics.avalant_needle,
                line = RelicData.relicsets.avalant,
                description = Mod.instance.Helper.Translation.Get("RelicData.366"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.367"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.369"),
            };

            relics[IconData.relics.avalant_measure.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.374"),
                relic = IconData.relics.avalant_measure,
                line = RelicData.relicsets.avalant,
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
                line = RelicData.relicsets.books,
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

            relics[IconData.relics.book_letters.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.447"),
                relic = IconData.relics.book_letters,
                line = RelicData.relicsets.books,
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
                line = RelicData.relicsets.books,
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
                line = RelicData.relicsets.books,
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

            // ====================================================================
            // Fates Relics

            relics[IconData.relics.box_measurer.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.577"),
                relic = IconData.relics.box_measurer,
                line = RelicData.relicsets.boxes,
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
                line = RelicData.relicsets.boxes,
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
                line = RelicData.relicsets.boxes,
                description = Mod.instance.Helper.Translation.Get("RelicData.608"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.609"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.611"),
            };

            relics[IconData.relics.box_chaos.ToString()] = new()
            {
                title = Mod.instance.Helper.Translation.Get("RelicData.616"),
                relic = IconData.relics.box_chaos,
                line = RelicData.relicsets.boxes,
                description = Mod.instance.Helper.Translation.Get("RelicData.619"),
                hint = Mod.instance.Helper.Translation.Get("RelicData.620"),

                heldup = Mod.instance.Helper.Translation.Get("RelicData.622"),
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
