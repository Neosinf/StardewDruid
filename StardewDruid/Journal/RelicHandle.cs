
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Dialogue;
using StardewDruid.Event;
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


namespace StardewDruid.Data
{
    public class RelicHandle
    {

        public static void WarpFunction()
        {

            List<IconData.relics> access = new()
            {
                IconData.relics.wayfinder_stone,
                IconData.relics.wayfinder_pot,
                IconData.relics.wayfinder_censer,
                IconData.relics.wayfinder_key,
                IconData.relics.wayfinder_lantern,
                IconData.relics.wayfinder_glove,
                IconData.relics.wayfinder_water,
                IconData.relics.wayfinder_ceremonial,
                IconData.relics.runestones_alchemistry,
                IconData.relics.wayfinder_eye,

            };

            foreach (IconData.relics relic in access)
            {

                if (!RelicData.HasRelic(relic))
                {

                    continue;

                }

                if (RelicFunction(relic.ToString()) == 1)
                {

                    return;

                }

            }

            Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

        }

        public static int RelicFunction(string id)
        {

            if (Mod.instance.activeEvent.Count > 0) { return 0; }

            IconData.relics relic = Enum.Parse<IconData.relics>(id);

            switch (relic)
            {

                case IconData.relics.effigy_crest:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    //Mod.instance.characters[CharacterHandle.characters.BrownBear] = new Character.Bear(CharacterHandle.characters.BrownBear);

                    //Mod.instance.characters[CharacterHandle.characters.BrownBear].SwitchToMode(Character.Character.mode.track, Game1.player);

                    //Mod.instance.characters[CharacterHandle.characters.BlackWolf] = new Character.Wolf(CharacterHandle.characters.BlackWolf);

                    //Mod.instance.characters[CharacterHandle.characters.BlackWolf].SwitchToMode(Character.Character.mode.track, Game1.player);
              
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

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(crestSummon) + StringData.Strings(StringData.stringkeys.joinedPlayer), 0, true);

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

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, Character.Character.mode.track, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Jester].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Jester].SwitchToMode(Character.Character.mode.track, Game1.player);

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Jester) +
                                StringData.Strings(StringData.stringkeys.joinedPlayer), 0, true);

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

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, Character.Character.mode.track, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Shadowtin].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Shadowtin].SwitchToMode(Character.Character.mode.track, Game1.player);

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Shadowtin) +
                                StringData.Strings(StringData.stringkeys.joinedPlayer), 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.blackfeather_glove:

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

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Blackfeather) +
                                StringData.Strings(StringData.stringkeys.joinedPlayer), 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.heiress_gift:

                    if (!Context.IsMainPlayer)
                    {

                        break;

                    }

                    return 4;

                case IconData.relics.wayfinder_stone:

                    if (Game1.player.currentLocation is Farm || Game1.player.currentLocation is FarmHouse)
                    {

                        //Game1.warpFarmer(LocationData.druid_grove_name, 21, 13, 0);

                        //Game1.xLocationAfterWarp = 21;

                        //Game1.yLocationAfterWarp = 13;

                        SpellHandle gravesWarp = new(Mod.instance.locations[LocationHandle.druid_grove_name], new Vector2(20, 15), Game1.player.Position) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, sound = SpellHandle.sounds.wand, factor = 0 };

                        Mod.instance.spellRegister.Add(gravesWarp);

                        return 1;

                    }

                    if (Game1.player.currentLocation is Grove)
                    {

                        //Wand wand = new();

                        //wand.lastUser = Game1.player;

                        //wand.DoFunction(Game1.player.currentLocation, 0, 0, 0, Game1.player);

                        FarmHouse homeOfFarmer = Utility.getHomeOfFarmer(Game1.player);

                        if (homeOfFarmer != null)
                        {
                            Point frontDoorSpot = homeOfFarmer.getFrontDoorSpot();

                            SpellHandle gravesWarp = new(Game1.getFarm(), new Vector2(frontDoorSpot.X, frontDoorSpot.Y), Game1.player.Position) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, sound = SpellHandle.sounds.wand, factor = 2 };

                            Mod.instance.spellRegister.Add(gravesWarp);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.wayfinder_pot:

                    if (Game1.player.currentLocation.Name == "UndergroundMine20")
                    {

                        Event.Access.AccessHandle potAccess = new();

                        potAccess.AccessSetup("UndergroundMine20", LocationHandle.druid_spring_name, new(24, 13), new(27, 31));

                        potAccess.AccessCheck(Game1.player.currentLocation);

                        Mod.instance.CastDisplay(Mod.instance.Helper.Translation.Get("RelicData.309.8"));

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_censer:

                    if (Game1.player.currentLocation is Beach)
                    {

                        (Mod.instance.locations[LocationHandle.druid_atoll_name] as Atoll).AddBoatAccess();

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

                            SpellHandle warp = new(Mod.instance.locations[LocationHandle.druid_graveyard_name], new Vector2(28, 29), Game1.player.Position) { type = SpellHandle.spells.warp, factor = 0, };

                            Mod.instance.spellRegister.Add(warp);

                            return 1;

                        }
                    }

                    break;

                case IconData.relics.wayfinder_lantern:

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

                        SpellHandle warpGlove = new(Mod.instance.locations[LocationHandle.druid_clearing_name], new Vector2(28, 8), Game1.player.Position) { type = SpellHandle.spells.warp, factor = 2, };

                        Mod.instance.spellRegister.Add(warpGlove);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_water:

                    /*SpellHandle waterWarp = new(Mod.instance.locations[LocationData.druid_lair_name], new(24, 13), new Vector2(0, 0)) { type = SpellHandle.spells.warp, };

                    Mod.instance.spellRegister.Add(waterWarp);

                    return 1;*/

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

                            SpellHandle warpEye = new(Mod.instance.locations[LocationHandle.druid_court_name], new Vector2(28, 29), Game1.player.Position) { type = SpellHandle.spells.warp, factor = 0, };

                            Mod.instance.spellRegister.Add(warpEye);

                            return 1;

                        }

                        destination = WarpData.WarpXZone(mineShaft);


                    }
                    else if (Game1.player.currentLocation is Town)
                    {

                        if (Vector2.Distance(Game1.player.Position, new Vector2(98, 8) * 64) <= 800)
                        {

                            SpellHandle warpEye = new(Mod.instance.locations[LocationHandle.druid_court_name], new Vector2(28, 29), Game1.player.Position) { type = SpellHandle.spells.warp, factor = 0, };

                            Mod.instance.spellRegister.Add(warpEye);

                            return 1;

                        }

                        destination = WarpData.WarpEntrance(Game1.player.currentLocation, Game1.player.Position);

                    }
                    else if (Game1.player.currentLocation is Court)
                    {

                        GameLocation backToTown = Game1.getLocationFromName("Town");

                        destination = WarpData.WarpEntrance(Game1.getLocationFromName("Town"), new Vector2(98, 8) * 64);

                        SpellHandle warp = new(backToTown, ModUtility.PositionToTile(destination), Game1.player.Position) { type = SpellHandle.spells.warp, factor = 0, };

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

                        Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.noWarpPoint));

                    }

                    return 1;

                case IconData.relics.wayfinder_ceremonial:

                    if (Game1.player.currentLocation.Name == "SkullCave")
                    {

                        Event.Access.AccessHandle ceremonialAccess = new();

                        ceremonialAccess.AccessSetup("SkullCave", LocationHandle.druid_tomb_name, new(10, 5), new(27, 30));

                        ceremonialAccess.AccessCheck(Game1.player.currentLocation);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_dwarf:


                    if (Game1.player.currentLocation is Clearing clearing)
                    {

                        if (!clearing.accessOpen)
                        {

                            clearing.OpenAccess();

                            clearing.playSound(SpellHandle.sounds.Ship.ToString());

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

                            SpellHandle warpGlove = new(MagesRest, new Vector2(48, 29), Game1.player.Position) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, factor = 3, };

                            Mod.instance.spellRegister.Add(warpGlove);

                            return 1;

                        }

                    }
                    else if (Game1.player.currentLocation.Name == "Custom_MagesRest")
                    {

                        FarmHouse homeOfFarmer = Utility.getHomeOfFarmer(Game1.player);

                        if (homeOfFarmer != null)
                        {
                            Point frontDoorSpot = homeOfFarmer.getFrontDoorSpot();

                            SpellHandle gravesWarp = new(Game1.getFarm(), new Vector2(frontDoorSpot.X, frontDoorSpot.Y), Game1.player.Position) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, sound = SpellHandle.sounds.wand, factor = 2 };

                            Mod.instance.spellRegister.Add(gravesWarp);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.book_wyrven:
                case IconData.relics.book_letters:
                case IconData.relics.book_manual:
                case IconData.relics.book_druid:
                case IconData.relics.book_knight:

                    return 2;

                case IconData.relics.dragon_form:

                    return 3;

            }

            return 0;


        }

        public static int RelicCancel(string id)
        {

            if (Mod.instance.activeEvent.Count > 0) { return 0; }

            Event.Access.AccessHandle access;

            IconData.relics relic = Enum.Parse<IconData.relics>(id);

            switch (relic)
            {

                case IconData.relics.effigy_crest:

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

                            Mod.instance.CastMessage(
                                CharacterHandle.CharacterTitle(crestSummon) +
                                StringData.Strings(StringData.stringkeys.returnedHome), 0, true);

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

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Jester].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Jester].SwitchToMode(Character.Character.mode.home, Game1.player);

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Jester) +
                                StringData.Strings(StringData.stringkeys.returnedHome), 0, true);

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

                        List<Character.Character.mode> reservedModes = new() { Character.Character.mode.scene, };

                        if (!reservedModes.Contains(Mod.instance.characters[CharacterHandle.characters.Shadowtin].modeActive))
                        {

                            Mod.instance.characters[CharacterHandle.characters.Shadowtin].SwitchToMode(Character.Character.mode.home, Game1.player);

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Shadowtin) +
                                StringData.Strings(StringData.stringkeys.returnedHome), 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.blackfeather_glove:

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

                            Mod.instance.CastMessage(CharacterHandle.CharacterTitle(CharacterHandle.characters.Blackfeather) +
                                StringData.Strings(StringData.stringkeys.returnedHome), 0, true);

                            return 1;

                        }

                    }

                    break;

                case IconData.relics.wayfinder_pot:

                    if (Game1.player.currentLocation.Name == "UndergroundMine20")
                    {

                        access = new();

                        access.AccessSetup("UndergroundMine20", LocationHandle.druid_spring_name, new(24, 13), new(27, 31));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_lantern:

                    if (Game1.player.currentLocation.Name == "UndergroundMine60")
                    {

                        access = new();

                        access.AccessSetup("UndergroundMine60", LocationHandle.druid_chapel_name, new(17, 10), new(27, 30), Event.Access.AccessHandle.types.door);

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_water:

                    if (Game1.player.currentLocation.Name == "UndergroundMine100")
                    {

                        access = new();

                        access.AccessSetup("UndergroundMine100", LocationHandle.druid_lair_name, new(24, 13), new(27, 30));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;

                case IconData.relics.wayfinder_ceremonial:

                    if (Game1.player.currentLocation.Name == "SkullCave")
                    {

                        access = new();

                        access.AccessSetup("SkullCave", LocationHandle.druid_tomb_name, new(10, 5), new(27, 30));

                        access.AccessCheck(Game1.player.currentLocation, true);

                        return 1;

                    }

                    break;


            }

            return 0;


        }

    }

}
