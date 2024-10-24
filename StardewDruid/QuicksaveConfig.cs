using StardewDruid.Cast;
using StardewDruid.Character;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StardewValley.Minigames.BoatJourney;

namespace StardewDruid
{

    public static class QuicksaveConfig
    {

        public static void SaveConfig(Mod mod)
        {

            if (!mod.Helper.ModRegistry.IsLoaded("DLX.QuickSave"))
            {

                return;

            }

            var api = mod.Helper.ModRegistry.GetApi<IQuickSaveAPI>("DLX.QuickSave");

            api.SavingEvent += QuickSavingEvent;

            api.SavedEvent += QuickSavedEvent;

            api.LoadedEvent += QuickLoadedEvent;

        }

        private static void QuickSavingEvent(object sender, ISavingEventArgs e)
        {

            Mod.instance.SerialiseGrove();

            Mod.instance.SaveCharacters();

            Mod.instance.Helper.Data.WriteSaveData("saveData_" + Mod.instance.version.ToString(), Mod.instance.save);

            Mod.instance.RemoveCharacters();

            Mod.instance.RemoveLocations();

        }

        private static void QuickSavedEvent(object sender, ISavedEventArgs e)
        {

            Mod.instance.ReinstateCharacters();

            Mod.instance.ReinstateLocations();

        }

        private static void QuickLoadedEvent(object sender, ILoadedEventArgs e)
        {

            Mod.instance.RemoveEvents();

            Mod.instance.RemoveCharacters();

            Mod.instance.RemoveLocations();

            Mod.instance.LoadState();

        }

    }

    public interface IQuickSaveAPI
    {
        /* Save Event Order:
         * 1. QS-Saving (IsSaving = true) 
         * 2. QS-Saved (IsSaving = false)
         */

        /// <summary>Fires before a Quicksave is being created</summary>
        public event SavingDelegate SavingEvent;
        /// <summary>Fires after a Quicksave has been created</summary>
        public event SavedDelegate SavedEvent;
        public bool IsSaving { get; }

        /* Load Event Order:
         * 1. QS-Loading (IsLoading = true)
         * 2. SMAPI-LoadStageChanged
         * 3. SMAPI-SaveLoaded & SMAPI-DayStarted
         * 4. QS-Loaded (IsLoading = false)
         */

        /// <summary>Fires before a Quicksave is being loaded</summary>
        public event LoadingDelegate LoadingEvent;
        /// <summary>Fires after a Quicksave was loaded</summary>
        public event LoadedDelegate LoadedEvent;
        public bool IsLoading { get; }

        public delegate void SavingDelegate(object sender, ISavingEventArgs e);
        public delegate void SavedDelegate(object sender, ISavedEventArgs e);
        public delegate void LoadingDelegate(object sender, ILoadingEventArgs e);
        public delegate void LoadedDelegate(object sender, ILoadedEventArgs e);
    }
    public interface ISavingEventArgs { }
    public interface ISavedEventArgs { }
    public interface ILoadingEventArgs { }
    public interface ILoadedEventArgs { }

}
