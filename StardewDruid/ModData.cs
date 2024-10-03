using Microsoft.VisualBasic;
using StardewDruid.Data;
using StardewModdingAPI.Utilities;
using StardewValley.Characters;

namespace StardewDruid
{

    public class ModData
    {
        
        public string modVersion { get; set; }

        public KeybindList riteButtons { get; set; }

        public KeybindList actionButtons { get; set; }

        public KeybindList specialButtons { get; set; }

        public KeybindList journalButtons { get; set; }

        public KeybindList effectsButtons { get; set; }

        public KeybindList relicsButtons { get; set; }

        public KeybindList herbalismButtons { get; set; }

        public string modDifficulty { get; set; }

        public bool disableHands { get; set; }

        public bool autoProgress { get; set; }

        public int setMilestone { get; set; }

        public bool setOnce { get; set; }

        public bool convert219 { get; set; }

        public bool maxDamage { get; set; }

        public bool slotAttune { get; set; }

        public bool slotConsume { get; set; }

        public bool slotFreedom { get; set; }

        public bool slotReverse { get; set; }

        public string slotOne { get; set; }

        public string slotTwo { get; set; }

        public string slotThree { get; set; }

        public string slotFour { get; set; }

        public string slotFive { get; set; }

        public string slotSix { get; set; }

        public string slotSeven { get; set; }

        public string slotEight { get; set; }

        public string slotNine { get; set; }

        public string slotTen { get; set; }

        public string slotEleven { get; set; }

        public string slotTwelve { get; set; }

        public int cultivateBehaviour { get; set; }

        public bool disableShopdata { get; set; }

        public int meteorBehaviour { get; set; }

        public bool cardinalMovement { get; set; }

        //public bool castAnywhere { get; set; }

        public bool decorateGrove { get; set; }

        public bool plantGrove { get; set; }

        public bool reverseJournal { get; set; }

        public bool activeJournal { get; set; }

        public int dragonScale { get; set; }

        public int dragonScheme { get; set; }

        public int dragonBreath { get; set; }

        public int dragonPrimaryR { get; set; }

        public int dragonPrimaryG { get; set; }

        public int dragonPrimaryB { get; set; }

        public int dragonSecondaryR { get; set; }

        public int dragonSecondaryG { get; set; }

        public int dragonSecondaryB { get; set; }

        public int dragonTertiaryR { get; set; }

        public int dragonTertiaryG { get; set; }

        public int dragonTertiaryB { get; set; }

        public enum difficulties
        {
            kiwi,
            hard,
            medium,
            easy,
            cali,

        }

        public enum slotOptions
        {
            weald,
            mists,
            stars,
            fates,
            ether,
            bones,
            none,
            lunch,

        }

        public enum modOptions
        {
            Default,
            Druid,
            Magic,
        }

        public ModData()
        {
            
            modVersion = modOptions.Default.ToString();
            riteButtons = KeybindList.Parse("MouseX1,MouseX2,V,LeftShoulder");
            actionButtons = KeybindList.Parse("MouseLeft,C,ControllerX");
            specialButtons = KeybindList.Parse("MouseRight,X,ControllerY");
            journalButtons = KeybindList.Parse("K");
            effectsButtons = KeybindList.Parse("L");
            relicsButtons = KeybindList.Parse("I");
            herbalismButtons = KeybindList.Parse("O");
            reverseJournal = false;
            activeJournal = true;
            disableHands = false;
            autoProgress = false;
            setMilestone = 0;
            setOnce = false;
            convert219 = false;
            maxDamage = false;
            modDifficulty = difficulties.medium.ToString();
            slotAttune = false;
            slotConsume = true;
            slotFreedom = false;
            slotReverse = false;
            slotOne = slotOptions.weald.ToString();
            slotTwo = slotOptions.mists.ToString();
            slotThree = slotOptions.stars.ToString();
            slotFour = slotOptions.fates.ToString();
            slotFive = slotOptions.ether.ToString();
            slotSix = slotOptions.bones.ToString();
            slotSeven = slotOptions.none.ToString();
            slotEight = slotOptions.none.ToString();
            slotNine = slotOptions.lunch.ToString();
            slotTen = slotOptions.lunch.ToString();
            slotEleven = slotOptions.lunch.ToString();
            slotTwelve = slotOptions.lunch.ToString();
            cultivateBehaviour = 2;
            disableShopdata = false;
            meteorBehaviour = 3;
            cardinalMovement = false;
            //castAnywhere = false;
            decorateGrove = false;
            plantGrove = false;
            dragonScale = 3;
            dragonScheme = (int)IconData.schemes.dragon_red;
            dragonBreath = (int)IconData.schemes.stars;
            dragonPrimaryR = 190;
            dragonPrimaryG = 30;
            dragonPrimaryB = 45;
            dragonSecondaryR = 191;
            dragonSecondaryG = 142;
            dragonSecondaryB = 93;
            dragonTertiaryR = 39;
            dragonTertiaryG = 170;
            dragonTertiaryB = 225;

        }

    }

    public class QueryData
    {

        public string name;

        public string value;

        public string description;

        public double time;

        public string location;

        public enum queries
        {
            RequestProgress,
            SyncProgress,
            QuestUpdate,
            EventDisplay,
            EventDialogue,
            ThrowRelic,
            ThrowSword,
            SpellHandle,
            AccessHandle,
            AccessDoor,
            HaltCharacter,
            GimmeMoney,
        }

        public QueryData()
        {

        }

    }

}