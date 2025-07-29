
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Mists;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event.Challenge;
using StardewDruid.Journal;
using StardewDruid.Location;
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
using System.Threading;

namespace StardewDruid.Handle
{
    public class RelicFunction
    {

        public static void WarpFunction()
        {

            List<IconData.relics> access = new()
            {
                IconData.relics.wayfinder_stone,
                IconData.relics.lantern_pot,
                IconData.relics.lantern_censer,
                IconData.relics.wayfinder_key,
                IconData.relics.lantern_guardian,
                IconData.relics.wayfinder_glove,
                IconData.relics.lantern_water,
                IconData.relics.lantern_ceremony,
                IconData.relics.runestones_alchemistry,
                IconData.relics.wayfinder_eye,

            };

            foreach (IconData.relics relic in access)
            {

                if (!RelicHandle.HasRelic(relic))
                {

                    continue;

                }

                if (RelicFunction(relic.ToString()) == 1)
                {

                    return;

                }

            }

            Game1.playSound(SpellHandle.Sounds.ghost.ToString());

        }

        public static void QuickFunction()
        {

            if(Mod.instance.relicHandle.favourite != IconData.relics.none)
            {

                if (RelicFunction(Mod.instance.relicHandle.favourite.ToString()) == 1)
                {

                    return;

                }

            }

            Game1.playSound(SpellHandle.Sounds.ghost.ToString());

        }

        public static int RelicFunction(string id, int record = 0)
        {

            if (Mod.instance.activeEvent.Count > 0) { return 0; }

            IconData.relics relic = Enum.Parse<IconData.relics>(id);

            switch (relic)
            {

                case IconData.relics.companion_crest:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    CharacterHandle.characters crestSummon = CharacterHandle.characters.Effigy;

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeBones))
                    {

                        crestSummon = CharacterHandle.characters.Aldebaran;

                    }

                    if (Mod.instance.characters.ContainsKey(crestSummon))
                    {

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, Character.Character.mode.track, };

                        if (!reservedModes.Contains(Mod.instance.characters[crestSummon].modeActive))
                        {

                            Mod.instance.characters[crestSummon].SwitchToMode(Character.Character.mode.track, Game1.player);

                            Mod.instance.RegisterMessage(CharacterHandle.CharacterTitle(crestSummon) + StringData.Strings(StringData.stringkeys.joinedPlayer), 0, true);

                            Mod.instance.relicHandle.favourite = relic;

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.companion_dice:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Jester))
                    {

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, Character.Character.mode.track, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Jester].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Jester].SwitchToMode(Character.Character.mode.track, Game1.player);

                            Mod.instance.RegisterMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Jester) +
                                StringData.Strings(StringData.stringkeys.joinedPlayer), 0, true);

                            Mod.instance.relicHandle.favourite = relic;

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.companion_tome:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Shadowtin))
                    {

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, Character.Character.mode.track, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Shadowtin].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Shadowtin].SwitchToMode(Character.Character.mode.track, Game1.player);

                            Mod.instance.RegisterMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Shadowtin) +
                                StringData.Strings(StringData.stringkeys.joinedPlayer), 0, true);

                            Mod.instance.relicHandle.favourite = relic;

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.companion_glove:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Blackfeather))
                    {

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, Character.Character.mode.track, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Blackfeather].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Blackfeather].SwitchToMode(Character.Character.mode.track, Game1.player);

                            Mod.instance.RegisterMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Blackfeather) +
                                StringData.Strings(StringData.stringkeys.joinedPlayer), 0, true);

                            Mod.instance.relicHandle.favourite = relic;

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.druid_grimoire:

                    DruidJournal.openJournal(DruidJournal.journalTypes.masteries, id, record);

                    return 5;

                case IconData.relics.druid_runeboard:

                    DruidJournal.openJournal(DruidJournal.journalTypes.alchemy, id, record);

                    return 5;

                case IconData.relics.herbalism_apothecary:

                    DruidJournal.openJournal(DruidJournal.journalTypes.herbalism, id, record);

                    return 5;

                case IconData.relics.druid_hieress:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    DruidJournal.openJournal(DruidJournal.journalTypes.ledger, id, record);

                    return 5;

                case IconData.relics.wayfinder_stone:

                    if (Game1.player.currentLocation is Farm || Game1.player.currentLocation is FarmHouse)
                    {

                        SpellHandle gravesWarp = new(Mod.instance.locations[LocationHandle.druid_grove_name], new Vector2(19f, 15f), Game1.player.Position) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, sound = SpellHandle.Sounds.wand, displayFactor = 0 };

                        Mod.instance.spellRegister.Add(gravesWarp);

                        return 1;

                    }

                    if (Game1.player.currentLocation is Grove)
                    {


                        FarmHouse homeOfFarmer = Utility.getHomeOfFarmer(Game1.player);

                        if (homeOfFarmer != null)
                        {
                            Point frontDoorSpot = homeOfFarmer.getFrontDoorSpot();

                            SpellHandle gravesWarp = new(Game1.getFarm(), new Vector2(frontDoorSpot.X, frontDoorSpot.Y), Game1.player.Position) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, sound = SpellHandle.Sounds.wand, displayFactor = 2 };

                            Mod.instance.spellRegister.Add(gravesWarp);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.lantern_pot:

                    if (Game1.player.currentLocation.Name == "UndergroundMine20")
                    {

                        Event.Access.AccessHandle potAccess = new();

                        potAccess.AccessSetup("UndergroundMine20", LocationHandle.druid_spring_name, new(24, 13), new(21, 20));

                        potAccess.AccessCheck(Game1.player.currentLocation);

                        Mod.instance.RegisterDisplay(Mod.instance.Helper.Translation.Get("RelicData.309.8"));

                        return 1;

                    }

                    break;

                case IconData.relics.lantern_censer:

                    if (Game1.player.currentLocation is Beach)
                    {

                        SpellHandle warp = new(Mod.instance.locations[LocationHandle.druid_atoll_name], new Vector2(14, 10), Game1.player.Position) { type = SpellHandle.Spells.warp, displayFactor = 0, };

                        Mod.instance.spellRegister.Add(warp);

                        //(Mod.instance.locations[LocationHandle.druid_atoll_name] as Atoll).AddBoatAccess();

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_key:

                    /*SpellHandle keyWarp = new(Mod.instance.locations[LocationData.druid_graveyard_name], new Vector2(28, 29), new Vector2(0, 0)) { type = SpellHandle.spells.warp, }; ;

                    Mod.instance.spellRegister.Add(keyWarp);

                    return 1; */

                    if (Game1.player.currentLocation is Town)
                    {

                        if (Vector2.Distance(Game1.player.Position, new Vector2(47, 88) * 64) <= 640)
                        {

                            SpellHandle warp = new(Mod.instance.locations[LocationHandle.druid_graveyard_name], new Vector2(28, 29), Game1.player.Position) { type = SpellHandle.Spells.warp, displayFactor = 0, };

                            Mod.instance.spellRegister.Add(warp);

                            return 1;

                        }
                    }

                    break;

                case IconData.relics.lantern_guardian:

                    /*SpellHandle lanternWarp = new(Mod.instance.locations[LocationData.druid_chapel_name], new(27, 30), new Vector2(0, 0)) { type = SpellHandle.spells.warp, };

                    Mod.instance.spellRegister.Add(lanternWarp);

                    return 1;*/

                    if (Game1.player.currentLocation.Name == "UndergroundMine60")
                    {

                        Event.Access.AccessHandle lanternAccess = new();

                        lanternAccess.AccessSetup("UndergroundMine60", LocationHandle.druid_chapel_name, new(17, 10), new(27, 30), Event.Access.AccessHandle.types.door);

                        lanternAccess.AccessCheck(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_glove:

                    if (Game1.player.currentLocation is Forest)
                    {

                        SpellHandle warpGlove = new(Mod.instance.locations[LocationHandle.druid_clearing_name], new Vector2(28, 8), Game1.player.Position) { type = SpellHandle.Spells.warp, displayFactor = 2, };

                        Mod.instance.spellRegister.Add(warpGlove);

                        return 1;

                    }

                    break;

                case IconData.relics.lantern_water:

                    if (Game1.player.currentLocation.Name == "UndergroundMine100")
                    {

                        Event.Access.AccessHandle waterAccess = new();

                        waterAccess.AccessSetup("UndergroundMine100", LocationHandle.druid_lair_name, new(24, 13), new(27, 30));

                        waterAccess.AccessCheck(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_eye:

                    Vector2 destination;

                    if (Game1.player.currentLocation is MineShaft mineShaft)
                    {

                        if (mineShaft.mineLevel == MineShaft.quarryMineShaft)
                        {

                            SpellHandle warpEye = new(Mod.instance.locations[LocationHandle.druid_court_name], new Vector2(28, 29), Game1.player.Position) { type = SpellHandle.Spells.warp, displayFactor = 0, };

                            Mod.instance.spellRegister.Add(warpEye);

                            return 1;

                        }

                        destination = WarpData.WarpXZone(mineShaft);


                    }
                    else if (Game1.player.currentLocation is Town)
                    {

                        if (Vector2.Distance(Game1.player.Position, new Vector2(98, 8) * 64) <= 800)
                        {

                            SpellHandle warpEye = new(Mod.instance.locations[LocationHandle.druid_court_name], new Vector2(28, 29), Game1.player.Position) { type = SpellHandle.Spells.warp, displayFactor = 0, };

                            Mod.instance.spellRegister.Add(warpEye);

                            return 1;

                        }

                        destination = WarpData.WarpEntrance(Game1.player.currentLocation, Game1.player.Position);

                    }
                    else if (Game1.player.currentLocation is Court)
                    {

                        GameLocation backToTown = Game1.getLocationFromName("Town");

                        destination = WarpData.WarpEntrance(Game1.getLocationFromName("Town"), new Vector2(98, 8) * 64);

                        SpellHandle warp = new(backToTown, ModUtility.PositionToTile(destination), Game1.player.Position) { type = SpellHandle.Spells.warp, displayFactor = 0, };

                        Mod.instance.spellRegister.Add(warp);

                        return 1;

                    }
                    else
                    {
                        destination = WarpData.WarpEntrance(Game1.player.currentLocation, Game1.player.Position);

                    }

                    if (destination != Vector2.Zero)
                    {

                        SpellHandle teleport = new(Game1.player.currentLocation, destination, Game1.player.Position)
                        {
                            type = SpellHandle.Spells.teleport
                        };

                        Mod.instance.spellRegister.Add(teleport);

                    }
                    else
                    {

                        Mod.instance.RegisterMessage(StringData.Strings(StringData.stringkeys.noWarpPoint));

                    }

                    return 1;

                case IconData.relics.lantern_ceremony:

                    if (Game1.player.currentLocation.Name == "SkullCave")
                    {

                        Event.Access.AccessHandle ceremonialAccess = new();

                        ceremonialAccess.AccessSetup("SkullCave", LocationHandle.druid_tomb_name, new(10, 5), new(27, 30));

                        ceremonialAccess.AccessCheck(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.shadowtin_cell:


                    if (Game1.player.currentLocation is Clearing clearing)
                    {

                        if (!clearing.accessOpen)
                        {

                            clearing.OpenAccess();

                            clearing.playSound(SpellHandle.Sounds.Ship.ToString());

                        }

                        return 1;

                    }

                    break;

                case IconData.relics.runestones_alchemistry:

                    if (Game1.player.currentLocation is Forest || Game1.player.currentLocation is Farm || Game1.player.currentLocation is WizardHouse)
                    {

                        GameLocation MagesRest = Game1.getLocationFromName("Custom_MagesRest");

                        if (MagesRest != null)
                        {

                            SpellHandle warpGlove = new(MagesRest, new Vector2(48, 29), Game1.player.Position) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, displayFactor = 3, };

                            Mod.instance.spellRegister.Add(warpGlove);

                            Mod.instance.relicHandle.favourite = relic;

                            return 1;

                        }

                    }
                    else if (Game1.player.currentLocation.Name == "Custom_MagesRest")
                    {

                        FarmHouse homeOfFarmer = Utility.getHomeOfFarmer(Game1.player);

                        if (homeOfFarmer != null)
                        {
                            Point frontDoorSpot = homeOfFarmer.getFrontDoorSpot();

                            SpellHandle gravesWarp = new(Game1.getFarm(), new Vector2(frontDoorSpot.X, frontDoorSpot.Y), Game1.player.Position) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, sound = SpellHandle.Sounds.wand, displayFactor = 2 };

                            Mod.instance.spellRegister.Add(gravesWarp);

                            Mod.instance.relicHandle.favourite = relic;

                            return 1;

                        }
                    }
                    break;

                case IconData.relics.companion_badge:

                    BattleHandle battleHandle = new();

                    battleHandle.SearchChallengers();

                    Mod.instance.relicHandle.favourite = relic;

                    return 1;

                case IconData.relics.monster_bat:
                case IconData.relics.monster_slime:
                case IconData.relics.monster_spirit:
                case IconData.relics.monster_ghost:
                case IconData.relics.monster_serpent:

                    CharacterHandle.characters entity = CharacterHandle.characters.PalBat;

                    switch (relic)
                    {

                        case IconData.relics.monster_slime:
                            entity = CharacterHandle.characters.PalSlime;
                            break;
                        case IconData.relics.monster_spirit:
                            entity = CharacterHandle.characters.PalSpirit;
                            break;
                        case IconData.relics.monster_ghost:
                            entity = CharacterHandle.characters.PalGhost;
                            break;
                        case IconData.relics.monster_serpent:
                            entity = CharacterHandle.characters.PalSerpent;
                            break;

                    }

                    if (!Mod.instance.save.pals.ContainsKey(entity))
                    {

                        return 0;

                    }

                    DruidJournal.openJournal(DruidJournal.journalTypes.palPage,entity.ToString(),record);

                    Mod.instance.relicHandle.favourite = relic;

                    return 5;


                case IconData.relics.crest_church:
                case IconData.relics.crest_dwarf:
                case IconData.relics.crest_associate:
                case IconData.relics.crest_smuggler:

                    ExportHandle.exports guild = ExportHandle.exports.church;

                    switch (relic)
                    {

                        case IconData.relics.crest_dwarf:
                            guild = ExportHandle.exports.dwarf;
                            break;
                        case IconData.relics.crest_associate:
                            guild = ExportHandle.exports.associate;
                            break;
                        case IconData.relics.crest_smuggler:
                            guild = ExportHandle.exports.smuggler;
                            break;

                    }

                    DruidJournal.openJournal(DruidJournal.journalTypes.guildPage, guild.ToString(), record);
                    
                    return 5;

                case IconData.relics.book_wyrven:
                case IconData.relics.book_letters:
                case IconData.relics.book_manual:
                case IconData.relics.book_druid:
                case IconData.relics.book_annal:
                case IconData.relics.book_knight:

                    DruidJournal.openJournal(DruidJournal.journalTypes.relicPage, id, record);

                    return 5;

                case IconData.relics.druid_dragonomicon:

                    DruidJournal.openJournal(DruidJournal.journalTypes.dragon, id, record);

                    return 5;

            }

            return 0;


        }

        public static int RelicCancel(string id, int record)
        {

            if (Mod.instance.activeEvent.Count > 0) { return 0; }

            Event.Access.AccessHandle access;

            IconData.relics relic = Enum.Parse<IconData.relics>(id);

            switch (relic)
            {

                case IconData.relics.companion_crest:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    CharacterHandle.characters crestSummon = CharacterHandle.characters.Effigy;

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeBones))
                    {

                        crestSummon = CharacterHandle.characters.Aldebaran;

                    }

                    if (Mod.instance.characters.ContainsKey(crestSummon))
                    {

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, };

                        if (!reservedModes.Contains(Mod.instance.characters[crestSummon].modeActive))
                        {

                            Mod.instance.characters[crestSummon].SwitchToMode(Character.Character.mode.home, Game1.player);

                            Mod.instance.RegisterMessage(
                                CharacterHandle.CharacterTitle(crestSummon) +
                                StringData.Strings(StringData.stringkeys.returnedHome), 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.companion_dice:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Jester))
                    {

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Jester].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Jester].SwitchToMode(Character.Character.mode.home, Game1.player);

                            Mod.instance.RegisterMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Jester) +
                                StringData.Strings(StringData.stringkeys.returnedHome), 0, true);

                            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Buffin))
                            {

                                Mod.instance.characters[CharacterHandle.characters.Buffin].SwitchToMode(Character.Character.mode.home, Game1.player);

                            }

                            return 1;

                        }

                    }

                    break;


                case IconData.relics.companion_tome:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Shadowtin))
                    {

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Shadowtin].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Shadowtin].SwitchToMode(Character.Character.mode.home, Game1.player);

                            Mod.instance.RegisterMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Shadowtin) +
                                StringData.Strings(StringData.stringkeys.returnedHome), 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.companion_glove:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Blackfeather))
                    {

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Blackfeather].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Blackfeather].SwitchToMode(Character.Character.mode.home, Game1.player);

                            Mod.instance.RegisterMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Blackfeather) +
                                StringData.Strings(StringData.stringkeys.returnedHome), 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.lantern_pot:

                    if (Game1.player.currentLocation.Name == "UndergroundMine20")
                    {

                        access = new();

                        access.AccessSetup("UndergroundMine20", LocationHandle.druid_spring_name, new(24, 13), new(21, 20));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

                case IconData.relics.lantern_guardian:

                    if (Game1.player.currentLocation.Name == "UndergroundMine60")
                    {

                        access = new();

                        access.AccessSetup("UndergroundMine60", LocationHandle.druid_chapel_name, new(17, 10), new(27, 30), Event.Access.AccessHandle.types.door);

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

                case IconData.relics.lantern_water:

                    if (Game1.player.currentLocation.Name == "UndergroundMine100")
                    {

                        access = new();

                        access.AccessSetup("UndergroundMine100", LocationHandle.druid_lair_name, new(24, 13), new(27, 30));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

                case IconData.relics.lantern_ceremony:

                    if (Game1.player.currentLocation.Name == "SkullCave")
                    {

                        access = new();

                        access.AccessSetup("SkullCave", LocationHandle.druid_tomb_name, new(10, 5), new(27, 30));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

                case IconData.relics.monster_bat:
                case IconData.relics.monster_slime:
                case IconData.relics.monster_spirit:
                case IconData.relics.monster_ghost:
                case IconData.relics.monster_serpent:

                    CharacterHandle.characters entity = CharacterHandle.characters.PalBat;

                    switch (relic)
                    {

                        case IconData.relics.monster_slime:
                            entity = CharacterHandle.characters.PalSlime;
                            break;
                        case IconData.relics.monster_spirit:
                            entity = CharacterHandle.characters.PalSpirit;
                            break;
                        case IconData.relics.monster_ghost:
                            entity = CharacterHandle.characters.PalGhost;
                            break;
                        case IconData.relics.monster_serpent:
                            entity = CharacterHandle.characters.PalSerpent;
                            break;

                    }

                    if (!Mod.instance.save.pals.ContainsKey(entity))
                    {

                        return 0;

                    }

                    DruidJournal.openJournal(DruidJournal.journalTypes.palPage, entity.ToString(), record);

                    return 5;

            }

            return 0;


        }

    }

}
