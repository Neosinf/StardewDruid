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
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.10"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.11"),
                getValue: () => Config.riteButtons,
            setValue: value => Config.riteButtons = value
            );
            configMenu.AddKeybindList(
            mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.17"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.18"),
                  getValue: () => Config.actionButtons,
                setValue: value => Config.actionButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.25"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.26"),
                getValue: () => Config.specialButtons,
                setValue: value => Config.specialButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.33"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.34"),
                getValue: () => Config.journalButtons,
                setValue: value => Config.journalButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.41"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.42"),
                getValue: () => Config.effectsButtons,
                setValue: value => Config.effectsButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.49"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.50"),
                getValue: () => Config.relicsButtons,
                setValue: value => Config.relicsButtons = value
            );

            configMenu.AddKeybindList(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.57"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.58"),
                getValue: () => Config.herbalismButtons,
                setValue: value => Config.herbalismButtons = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.65"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.66"),
                getValue: () => Config.reverseJournal,
                setValue: value => Config.reverseJournal = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.73"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.74"),
                getValue: () => Config.activeJournal,
                setValue: value => Config.activeJournal = value
            );

            string[] textOption = Enum.GetNames<ModData.difficulties>();

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.83"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.84"),
                allowedValues: textOption,
                getValue: () => Config.modDifficulty,
                setValue: value => Config.modDifficulty = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.92"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.93"),
                getValue: () => Config.disableHands,
                setValue: value => Config.disableHands = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.100"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.101"),
                getValue: () => Config.autoProgress,
                setValue: value => Config.autoProgress = value
            );

            configMenu.AddNumberOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.108"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.109"),
                min: 0,
                max: Enum.GetNames(typeof(QuestHandle.milestones)).Count() - 1,
                interval: 1,
                getValue: () => Config.setMilestone,
                setValue: value => Config.setMilestone = value,
                fieldId: Mod.instance.Helper.Translation.Get("ModConfig.115")
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.120"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.121"),
                getValue: () => Config.setOnce,
                setValue: value => Config.setOnce = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.128"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.129"),
                getValue: () => Config.convert219,
                setValue: value => Config.convert219 = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.136"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.137"),
                getValue: () => Config.slotAttune,
                setValue: value => Config.slotAttune = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.144"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.145"),
                getValue: () => Config.slotConsume,
                setValue: value => Config.slotConsume = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.152"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.153"),
                getValue: () => Config.slotFreedom,
                setValue: value => Config.slotFreedom = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.160"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.161"),
                getValue: () => Config.slotReverse,
                setValue: value => Config.slotReverse = value
            );

            string[] slotOption = Enum.GetNames<ModData.slotOptions>();

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.170"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.171"),
                allowedValues: slotOption,
                getValue: () => Config.slotOne,
                setValue: value => Config.slotOne = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.179"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.180"),
                allowedValues: slotOption,
                getValue: () => Config.slotTwo,
                setValue: value => Config.slotTwo = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.188"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.189"),
                allowedValues: slotOption,
                getValue: () => Config.slotThree,
                setValue: value => Config.slotThree = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.197"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.198"),
                allowedValues: slotOption,
                getValue: () => Config.slotFour,
                setValue: value => Config.slotFour = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.206"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.207"),
                allowedValues: slotOption,
                getValue: () => Config.slotFive,
                setValue: value => Config.slotFive = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.215"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.216"),
                allowedValues: slotOption,
                getValue: () => Config.slotSix,
                setValue: value => Config.slotSix = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.224"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.225"),
                allowedValues: slotOption,
                getValue: () => Config.slotSeven,
                setValue: value => Config.slotSeven = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.233"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.234"),
                allowedValues: slotOption,
                getValue: () => Config.slotEight,
                setValue: value => Config.slotEight = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.242"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.243"),
                allowedValues: slotOption,
                getValue: () => Config.slotNine,
                setValue: value => Config.slotNine = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.251"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.252"),
                allowedValues: slotOption,
                getValue: () => Config.slotTen,
                setValue: value => Config.slotTen = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.260"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.261"),
                allowedValues: slotOption,
                getValue: () => Config.slotEleven,
                setValue: value => Config.slotEleven = value
            );

            configMenu.AddTextOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.269"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.270"),
                allowedValues: slotOption,
                getValue: () => Config.slotTwelve,
                setValue: value => Config.slotTwelve = value
            );

            configMenu.AddNumberOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.278"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.279"),
                min: 1,
                max: 3,
                interval: 1,
                getValue: () => Config.cultivateBehaviour,
                setValue: value => Config.cultivateBehaviour = value
            );

            configMenu.AddNumberOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.289"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.290"),
                min: 1,
                max: 5,
                interval: 1,
                getValue: () => Config.meteorBehaviour,
                setValue: value => Config.meteorBehaviour = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.300"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.301"),
                getValue: () => Config.maxDamage,
                setValue: value => Config.maxDamage = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.308"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.309"),
                getValue: () => Config.cardinalMovement,
                setValue: value => Config.cardinalMovement = value
            );

            configMenu.AddBoolOption(
                mod: mod.ModManifest,
                name: () => Mod.instance.Helper.Translation.Get("ModConfig.316"),
                tooltip: () => Mod.instance.Helper.Translation.Get("ModConfig.317"),
                getValue: () => Config.castAnywhere,
                setValue: value => Config.castAnywhere = value
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
