using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Journal;
using StardewModdingAPI.Utilities;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid
{
    public static class ModConfig
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
                name: () => "Action (Druid Only)",
                tooltip: () => "Assigns an alternative keybind for the Action / Left Click / Use tool function, for the purposes of the mod only. This keybind does not override or re-map any keybinds in the base game. Useful for controllers with non-standard button maps.",
                  getValue: () => Config.actionButtons,
                setValue: value => Config.actionButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => "Special (Druid Only)",
                tooltip: () => "Assigns an alternative keybind for the Check / Special / Right Click / Placedown function, for the purposes of the mod only. This keybind does not override or re-map any keybinds in the base game. Useful for controllers with non-standard button maps.",
                getValue: () => Config.specialButtons,
                setValue: value => Config.specialButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => "Quests Journal",
                tooltip: () => "Keybind assignment to open the Stardew Druid effects journal while in world. The rite keybind can be used to open the journal from the game questlog.",
                getValue: () => Config.journalButtons,
                setValue: value => Config.journalButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => "Effects Journal",
                tooltip: () => "Keybind assignment to open the Stardew Druid effects journal while in world.",
                getValue: () => Config.effectsButtons,
                setValue: value => Config.effectsButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => "Relics Journal",
                tooltip: () => "Keybind assignment to open the Stardew Druid relics journal while in world.",
                getValue: () => Config.relicsButtons,
                setValue: value => Config.relicsButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => "Herbalism Journal",
                tooltip: () => "Keybind assignment to open the Stardew Druid herbalism journal while in world.",
                getValue: () => Config.herbalismButtons,
                setValue: value => Config.herbalismButtons = value
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
                name: () => "Active Journal",
                tooltip: () => "Show active quests on the front pages of the Stardew Druid journal. Default: active entries on front page. Disabled: no change in order.",
                getValue: () => Config.activeJournal,
                setValue: value => Config.activeJournal = value
            );

            string[] textOption = { "cali", "easy", "medium", "hard", "kiwi", };

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Mod Difficulty",
                tooltip: () => "Select Mod difficulty level. Affects monetary rewards, stamina costs, monster difficulty.",
                allowedValues: textOption,
                getValue: () => Config.modDifficulty,
                setValue: value => Config.modDifficulty = value
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
                name: () => "Auto Progress",
                tooltip: () => "Automatically progress to the next stage of the questline after loading or starting a new day.",
                getValue: () => Config.autoProgress,
                setValue: value => Config.autoProgress = value
            );

            configMenu.AddNumberOption(
                mod: mod.ModManifest,
                name: () => "Set Progress",
                tooltip: () => "Use to adjust progress level on game load. 0 is no change. Note that adjustments may clear or miss levels of progress.",
                min: 0,
                max: Enum.GetNames(typeof(QuestHandle.milestones)).Count() - 1,
                interval: 1,
                getValue: () => Config.setMilestone,
                setValue: value => Config.setMilestone = value,
                fieldId: "setProgress"
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Set Once",
                tooltip: () => "Automatically returns set progress to 0 after reconfiguring one save file.",
                getValue: () => Config.setOnce,
                setValue: value => Config.setOnce = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Convert 2.1.9 Data",
                tooltip: () => "(Not recommended) Reconfigures 2.1.9 and earlier version data if present in the loaded save file and sets new progress to the relative 2.1.9 level.",
                getValue: () => Config.convert219,
                setValue: value => Config.convert219 = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Slot Attune",
                tooltip: () => "Rite casts will be based on selected slot in the toolbar as opposed to weapon or tool attunement, as per the below slot assignments. [lunch] will consume any edible item in that slot when health or stamina is below 33%.",
                getValue: () => Config.slotAttune,
                setValue: value => Config.slotAttune = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Slot Consume",
                tooltip: () => "For slots set to [lunch], the mod will consume any edible item in that inventory slot when health or stamina is below 33%.",
                getValue: () => Config.slotConsume,
                setValue: value => Config.slotConsume = value
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
                name: () => "Slot Reverse",
                tooltip: () => "Tool and lunch detection moves from right to left (slot 12 to 1)",
                getValue: () => Config.slotReverse,
                setValue: value => Config.slotReverse = value
            );

            string[] slotOption = { "none", "lunch", "weald", "mists", "stars", "fates", "ether", };

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 1",
                tooltip: () => "Select slot behaviour for inventory slot one",
                allowedValues: slotOption,
                getValue: () => Config.slotOne,
                setValue: value => Config.slotOne = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 2",
                tooltip: () => "Select slot behaviour for inventory slot two",
                allowedValues: slotOption,
                getValue: () => Config.slotTwo,
                setValue: value => Config.slotTwo = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 3",
                tooltip: () => "Select slot behaviour for inventory slot three",
                allowedValues: slotOption,
                getValue: () => Config.slotThree,
                setValue: value => Config.slotThree = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 4",
                tooltip: () => "Select slot behaviour for inventory slot four",
                allowedValues: slotOption,
                getValue: () => Config.slotFour,
                setValue: value => Config.slotFour = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 5",
                tooltip: () => "Select slot behaviour for inventory slot five",
                allowedValues: slotOption,
                getValue: () => Config.slotFive,
                setValue: value => Config.slotFive = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 6",
                tooltip: () => "Select slot behaviour for inventory slot six",
                allowedValues: slotOption,
                getValue: () => Config.slotSix,
                setValue: value => Config.slotSix = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 7",
                tooltip: () => "Select slot behaviour for inventory slot seven",
                allowedValues: slotOption,
                getValue: () => Config.slotSeven,
                setValue: value => Config.slotSeven = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 8",
                tooltip: () => "Select slot behaviour for inventory slot eight",
                allowedValues: slotOption,
                getValue: () => Config.slotEight,
                setValue: value => Config.slotEight = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 9",
                tooltip: () => "Select slot behaviour for inventory slot nine",
                allowedValues: slotOption,
                getValue: () => Config.slotNine,
                setValue: value => Config.slotNine = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 10",
                tooltip: () => "Select slot behaviour for inventory slot ten",
                allowedValues: slotOption,
                getValue: () => Config.slotTen,
                setValue: value => Config.slotTen = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 11",
                tooltip: () => "Select slot behaviour for inventory slot eleven",
                allowedValues: slotOption,
                getValue: () => Config.slotEleven,
                setValue: value => Config.slotEleven = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => "Slot 12",
                tooltip: () => "Select slot behaviour for inventory slot twelve",
                allowedValues: slotOption,
                getValue: () => Config.slotTwelve,
                setValue: value => Config.slotTwelve = value
            );

            configMenu.AddNumberOption(
                mod: mod.ModManifest,
                name: () => "Cultivate Behaviour",
                tooltip: () => "Adjust settings for Weald: Cultivate in regards to crop handling. See readme for specifics. 1 Highest growth rate. 2 Average growth, average quality. 3 Highest quality.",
                min: 1,
                max: 3,
                interval: 1,
                getValue: () => Config.cultivateBehaviour,
                setValue: value => Config.cultivateBehaviour = value
            );

            configMenu.AddNumberOption(
                mod: mod.ModManifest,
                name: () => "Meteor Behaviour",
                tooltip: () => "Adjust risk/reward setting for Stars: Meteor. See readme for specifics. 1 Intelligent targetting, lowest damage. 5 Completely random targets, highest damage.",
                min: 1,
                max: 5,
                interval: 1,
                getValue: () => Config.meteorBehaviour,
                setValue: value => Config.meteorBehaviour = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => "Maximum Damage",
                tooltip: () => "Some spell effects have damage modifiers that consider player combat level, highest upgrade on Pickaxe, Axe, and applied enchantments. Enable to cast at max damage and effect everytime.",
                getValue: () => Config.maxDamage,
                setValue: value => Config.maxDamage = value
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
                name: () => "Cast Anywhere",
                tooltip: () => "Disables the Map-based cast restrictions so that any rite effect can be cast anywhere. Proceed with caution!",
                getValue: () => Config.castAnywhere,
                setValue: value => Config.castAnywhere = value
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

    public interface IGenericModConfigMenuApi
    {
        /*********
        ** Methods
        *********/
        /****
        ** Must be called first
        ****/
        /// <summary>Register a mod whose config can be edited through the UI.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="reset">Reset the mod's config to its default values.</param>
        /// <param name="save">Save the mod's current config to the <c>config.json</c> file.</param>
        /// <param name="titleScreenOnly">Whether the options can only be edited from the title screen.</param>
        /// <remarks>Each mod can only be registered once, unless it's deleted via <see cref="Unregister"/> before calling this again.</remarks>
        void Register(IManifest mod, Action reset, Action save, bool titleScreenOnly = false);


        /****
        ** Basic options
        ****/
        /// <summary>Add a section title at the current position in the form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="text">The title text shown in the form.</param>
        /// <param name="tooltip">The tooltip text shown when the cursor hovers on the title, or <c>null</c> to disable the tooltip.</param>
        void AddSectionTitle(IManifest mod, Func<string> text, Func<string> tooltip = null);

        /// <summary>Add a paragraph of text at the current position in the form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="text">The paragraph text to display.</param>
        void AddParagraph(IManifest mod, Func<string> text);

        /// <summary>Add an image at the current position in the form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="texture">The image texture to display.</param>
        /// <param name="texturePixelArea">The pixel area within the texture to display, or <c>null</c> to show the entire image.</param>
        /// <param name="scale">The zoom factor to apply to the image.</param>
        void AddImage(IManifest mod, Func<Texture2D> texture, Microsoft.Xna.Framework.Rectangle? texturePixelArea = null, int scale = Game1.pixelZoom);

        /// <summary>Add a boolean option at the current position in the form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="getValue">Get the current value from the mod config.</param>
        /// <param name="setValue">Set a new value in the mod config.</param>
        /// <param name="name">The label text to show in the form.</param>
        /// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
        /// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
        void AddBoolOption(IManifest mod, Func<bool> getValue, Action<bool> setValue, Func<string> name, Func<string> tooltip = null, string fieldId = null);

        /// <summary>Add an integer option at the current position in the form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="getValue">Get the current value from the mod config.</param>
        /// <param name="setValue">Set a new value in the mod config.</param>
        /// <param name="name">The label text to show in the form.</param>
        /// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
        /// <param name="min">The minimum allowed value, or <c>null</c> to allow any.</param>
        /// <param name="max">The maximum allowed value, or <c>null</c> to allow any.</param>
        /// <param name="interval">The interval of values that can be selected.</param>
        /// <param name="formatValue">Get the display text to show for a value, or <c>null</c> to show the number as-is.</param>
        /// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
        void AddNumberOption(IManifest mod, Func<int> getValue, Action<int> setValue, Func<string> name, Func<string> tooltip = null, int? min = null, int? max = null, int? interval = null, Func<int, string> formatValue = null, string fieldId = null);

        /// <summary>Add a float option at the current position in the form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="getValue">Get the current value from the mod config.</param>
        /// <param name="setValue">Set a new value in the mod config.</param>
        /// <param name="name">The label text to show in the form.</param>
        /// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
        /// <param name="min">The minimum allowed value, or <c>null</c> to allow any.</param>
        /// <param name="max">The maximum allowed value, or <c>null</c> to allow any.</param>
        /// <param name="interval">The interval of values that can be selected.</param>
        /// <param name="formatValue">Get the display text to show for a value, or <c>null</c> to show the number as-is.</param>
        /// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
        void AddNumberOption(IManifest mod, Func<float> getValue, Action<float> setValue, Func<string> name, Func<string> tooltip = null, float? min = null, float? max = null, float? interval = null, Func<float, string> formatValue = null, string fieldId = null);

        /// <summary>Add a string option at the current position in the form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="getValue">Get the current value from the mod config.</param>
        /// <param name="setValue">Set a new value in the mod config.</param>
        /// <param name="name">The label text to show in the form.</param>
        /// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
        /// <param name="allowedValues">The values that can be selected, or <c>null</c> to allow any.</param>
        /// <param name="formatAllowedValue">Get the display text to show for a value from <paramref name="allowedValues"/>, or <c>null</c> to show the values as-is.</param>
        /// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
        void AddTextOption(IManifest mod, Func<string> getValue, Action<string> setValue, Func<string> name, Func<string> tooltip = null, string[] allowedValues = null, Func<string, string> formatAllowedValue = null, string fieldId = null);

        /// <summary>Add a key binding at the current position in the form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="getValue">Get the current value from the mod config.</param>
        /// <param name="setValue">Set a new value in the mod config.</param>
        /// <param name="name">The label text to show in the form.</param>
        /// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
        /// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
        void AddKeybind(IManifest mod, Func<SButton> getValue, Action<SButton> setValue, Func<string> name, Func<string> tooltip = null, string fieldId = null);

        /// <summary>Add a key binding list at the current position in the form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="getValue">Get the current value from the mod config.</param>
        /// <param name="setValue">Set a new value in the mod config.</param>
        /// <param name="name">The label text to show in the form.</param>
        /// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
        /// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
        void AddKeybindList(IManifest mod, Func<KeybindList> getValue, Action<KeybindList> setValue, Func<string> name, Func<string> tooltip = null, string fieldId = null);


        /****
        ** Multi-page management
        ****/
        /// <summary>Start a new page in the mod's config UI, or switch to that page if it already exists. All options registered after this will be part of that page.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="pageId">The unique page ID.</param>
        /// <param name="pageTitle">The page title shown in its UI, or <c>null</c> to show the <paramref name="pageId"/> value.</param>
        /// <remarks>You must also call <see cref="AddPageLink"/> to make the page accessible. This is only needed to set up a multi-page config UI. If you don't call this method, all options will be part of the mod's main config UI instead.</remarks>
        void AddPage(IManifest mod, string pageId, Func<string> pageTitle = null);

        /// <summary>Add a link to a page added via <see cref="AddPage"/> at the current position in the form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="pageId">The unique ID of the page to open when the link is clicked.</param>
        /// <param name="text">The link text shown in the form.</param>
        /// <param name="tooltip">The tooltip text shown when the cursor hovers on the link, or <c>null</c> to disable the tooltip.</param>
        void AddPageLink(IManifest mod, string pageId, Func<string> text, Func<string> tooltip = null);


        /****
        ** Advanced
        ****/
        /// <summary>Add an option at the current position in the form using custom rendering logic.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="name">The label text to show in the form.</param>
        /// <param name="draw">Draw the option in the config UI. This is called with the sprite batch being rendered and the pixel position at which to start drawing.</param>
        /// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
        /// <param name="beforeMenuOpened">A callback raised just before the menu containing this option is opened.</param>
        /// <param name="beforeSave">A callback raised before the form's current values are saved to the config (i.e. before the <c>save</c> callback passed to <see cref="Register"/>).</param>
        /// <param name="afterSave">A callback raised after the form's current values are saved to the config (i.e. after the <c>save</c> callback passed to <see cref="Register"/>).</param>
        /// <param name="beforeReset">A callback raised before the form is reset to its default values (i.e. before the <c>reset</c> callback passed to <see cref="Register"/>).</param>
        /// <param name="afterReset">A callback raised after the form is reset to its default values (i.e. after the <c>reset</c> callback passed to <see cref="Register"/>).</param>
        /// <param name="beforeMenuClosed">A callback raised just before the menu containing this option is closed.</param>
        /// <param name="height">The pixel height to allocate for the option in the form, or <c>null</c> for a standard input-sized option. This is called and cached each time the form is opened.</param>
        /// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
        /// <remarks>The custom logic represented by the callback parameters is responsible for managing its own state if needed. For example, you can store state in a static field or use closures to use a state variable.</remarks>
        void AddComplexOption(IManifest mod, Func<string> name, Action<SpriteBatch, Microsoft.Xna.Framework.Vector2> draw, Func<string> tooltip = null, Action beforeMenuOpened = null, Action beforeSave = null, Action afterSave = null, Action beforeReset = null, Action afterReset = null, Action beforeMenuClosed = null, Func<int> height = null, string fieldId = null);

        /// <summary>Set whether the options registered after this point can only be edited from the title screen.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="titleScreenOnly">Whether the options can only be edited from the title screen.</param>
        /// <remarks>This lets you have different values per-field. Most mods should just set it once in <see cref="Register"/>.</remarks>
        void SetTitleScreenOnlyForNextOptions(IManifest mod, bool titleScreenOnly);

        /// <summary>Register a method to notify when any option registered by this mod is edited through the config UI.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="onChange">The method to call with the option's unique field ID and new value.</param>
        /// <remarks>Options use a randomized ID by default; you'll likely want to specify the <c>fieldId</c> argument when adding options if you use this.</remarks>
        void OnFieldChanged(IManifest mod, Action<string, object> onChange);

        /// <summary>Open the config UI for a specific mod.</summary>
        /// <param name="mod">The mod's manifest.</param>
        void OpenModMenu(IManifest mod);

        /// <summary>Get the currently-displayed mod config menu, if any.</summary>
        /// <param name="mod">The manifest of the mod whose config menu is being shown, or <c>null</c> if not applicable.</param>
        /// <param name="page">The page ID being shown for the current config menu, or <c>null</c> if not applicable. This may be <c>null</c> even if a mod config menu is shown (e.g. because the mod doesn't have pages).</param>
        /// <returns>Returns whether a mod config menu is being shown.</returns>
        bool TryGetCurrentMenu(out IManifest mod, out string page);

        /// <summary>Remove a mod from the config UI and delete all its options and pages.</summary>
        /// <param name="mod">The mod's manifest.</param>
        void Unregister(IManifest mod);
    }


}
