﻿using GenericModConfigMenu;
using StardewDruid.Cast.Fates;
using StardewDruid.Map;
using StardewValley;
using System.Runtime.Intrinsics.X86;
using System;

namespace StardewDruid
{
    public static class ConfigMenu
    {

        public static IGenericModConfigMenuApi MenuConfig(Mod mod)
        {

            ModData Config = mod.Config;

            // get Generic Mod Config Menu's API (if it's installed)
            var configMenu = mod.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");

            if (configMenu is null)
            {
                return null;
            }

            // register mod
            configMenu.Register(
                mod: mod.ModManifest,
                reset: () => Config = new ModData(),
                save: () => mod.Helper.WriteConfig(Config)
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => "Rite",
                tooltip: () => "Configure the list or combination of keybinds to use for casting Rites",
                getValue: () => Config.riteButtons,
            setValue: value => Config.riteButtons = value
            );
            configMenu.AddKeybindList(
            mod: mod.ModManifest, 
                name: () => "Action (Optional)",
                tooltip: () => "Assigns an alternative keybind for the Action / Left Click / Use tool function, for the purposes of the mod only. This keybind does not override or re-map any keybinds in the base game. Useful for controllers with non-standard button maps.",
                  getValue: () => Config.actionButtons,
                setValue: value => Config.actionButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => "Special (Optional)",
                tooltip: () => "Assigns an alternative keybind for the Check / Special / Right Click / Placedown function, for the purposes of the mod only. This keybind does not override or re-map any keybinds in the base game. Useful for controllers with non-standard button maps.",
                getValue: () => Config.specialButtons,
                setValue: value => Config.specialButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => "Journal (Optional)",
                tooltip: () => "Keybind assignment to open the Stardew Druid journal while in world. The rite keybind can be used to open the journal from the game questlog.",
                getValue: () => Config.journalButtons,
                setValue: value => Config.journalButtons = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Disable Cast Hands",
                tooltip: () => "Disables farmer sprite 'cast hands' animation when triggering events. Recommended disable if other game modifications make changes to the farmer rendering or draw cycle.",
                getValue: () => Config.disableHands,
                setValue: value => Config.disableHands = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Slot Attune",
                tooltip: () => "Rite casts will be based on selected slot in the toolbar as opposed to weapon or tool attunement. Slot 1 Weald, Slot 2 Mists, Slot 3 Stars, Slot 4 Fates, Slot 5 Ether.",
                getValue: () => Config.slotAttune,
                setValue: value => Config.slotAttune = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Slot Freedom",
                tooltip: () => "Invalid tool selections will be ignored when slot-attune is active. Proceed with caution!",
                getValue: () => Config.slotFreedom,
                setValue: value => Config.slotFreedom = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Auto Progress",
                tooltip: () => "Automatically progress to the next stage of the questline after loading or starting a new day.",
                getValue: () => Config.autoProgress,
                setValue: value => Config.autoProgress = value
            );

            configMenu.AddNumberOption(
                mod: mod.ModManifest,
                name: () => "Set Progress",
                tooltip: () => "Use to adjust progress level on game load. -1 is no change. Note that adjustments may clear or miss levels of progress.",
                min: -1,
                max: QuestData.MaxProgress(),
                interval: 1,
                getValue: () => Config.newProgress,
                setValue: value => Config.newProgress = value
            );

            string[] textOption = { "easy", "medium", "hard", };

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Mod Difficulty",
                tooltip: () => "Select difficulty level for mod-spawned monsters and other effects.",
                allowedValues: textOption,
                getValue: () => Config.combatDifficulty,
                setValue: value => Config.combatDifficulty = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Maximum Damage",
                tooltip: () => "Some spell effects have damage modifiers that consider player combat level, highest upgrade on Pickaxe, Axe, and applied enchantments. Enable to cast at max damage and effect everytime.",
                getValue: () => Config.maxDamage,
                setValue: value => Config.maxDamage = value
            );

            StardewDruid.CustomData Customisation = mod.Helper.Data.ReadJsonFile<CustomData>("customData.json");

            if (Customisation.colourPreferences == null || Customisation.colourPreferences.Count == 0) { 
                
                Customisation = new(); 
            
            }

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Colour Preference",
                tooltip: () => "Select colour preference for transformation effects.",
                allowedValues: Customisation.colourPreferences.ToArray(),
                getValue: () => Config.colourPreference,
                setValue: value => Config.colourPreference = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Cardinal Targetting",
                tooltip: () => "Disables isometric (6 way) targetting for transformation effects. Might look a little misaligned with the transformation animations.",
                getValue: () => Config.cardinalMovement,
                setValue: value => Config.cardinalMovement = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Slot Consume",
                tooltip: () => "Enables auto consumption of any edible item in the top 12 slots of the inventory, prioritising left to right",
                getValue: () => Config.slotConsume,
                setValue: value => Config.slotConsume = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Consume Roughage",
                tooltip: () => "Enables automatic consumption of usually inedible but often inventory-crowding items: Sap, Tree seeds, Slime, Batwings, Red mushrooms; Triggers when casting with critically low stamina. These items are far more abundant in Stardew Druid due to Rite of Weald behaviour.",
                getValue: () => Config.consumeRoughage,
                setValue: value => Config.consumeRoughage = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Consume Lunch",
                tooltip: () => "Enables automatic consumption of common sustenance items: SpringOnion, Snackbar, Mushrooms, Algae, Seaweed, Carrots, Sashimi, Salmonberry, Cheese, Tonic, Salad; Triggers when casting with critically low stamina.",
                getValue: () => Config.consumeQuicksnack,
                setValue: value => Config.consumeQuicksnack = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Consume Caffeine",
                tooltip: () => "Enables automatic consumption of caffeinated items: Cola, Tea Leaves, Tea, Coffee Bean, Coffee, Triple Espresso, Energy Tonic; Triggers when casting with low stamina without an active drink buff.",
                getValue: () => Config.consumeCaffeine,
                setValue: value => Config.consumeCaffeine = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Cast Anywhere",
                tooltip: () => "Disables the Map-based cast restrictions so that any rite effect can be cast anywhere. Proceed with caution!",
                getValue: () => Config.castAnywhere,
                setValue: value => Config.castAnywhere = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Reverse Journal",
                tooltip: () => "Reverse the order in which Stardew Druid journal entries are displayed. Default: oldest to newest. Enabled: newest to oldest.",
                getValue: () => Config.reverseJournal,
                setValue: value => Config.reverseJournal = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Ostentatious Hats",
                tooltip: () => "Adds hats to some monsters. Cosmetic effect only.",
                getValue: () => Config.partyHats,
                setValue: value => Config.partyHats = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Disable Seed Spawn",
                tooltip: () => "Disables wild seasonal seed spawn effect for Rite of the Weald.",
                getValue: () => Config.disableSeeds,
                setValue: value => Config.disableSeeds = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Disable Fish Spawn",
                tooltip: () => "Disables low grade fish spawn effect for Rite of the Weald.",
                getValue: () => Config.disableFish,
                setValue: value => Config.disableFish = value
            );

            /*configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Disable Wild Spawn",
                tooltip: () => "Disables wild monster spawn effect for Rite of the Weald, despite in game effect management. Disabling, saving in game, then reenabling will require additional re-enablement in game.",
                getValue: () => Config.disableWildspawn,
                setValue: value => Config.disableWildspawn = value
            );*/

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Disable Tree Spawn",
                tooltip: () => "Disables tree spawn effect for Rite of the Weald.",
                getValue: () => Config.disableTrees,
                setValue: value => Config.disableTrees = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Disable Grass Spawn",
                tooltip: () => "Disables grass spawn effect for Rite of the Weald.",
                getValue: () => Config.disableGrass,
                setValue: value => Config.disableGrass = value
            );

            return configMenu;

        }


    }
}
