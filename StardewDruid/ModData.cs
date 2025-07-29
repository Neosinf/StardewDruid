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

        public KeybindList skillsButtons { get; set; }

        public KeybindList relicsButtons { get; set; }

        public KeybindList herbalismButtons { get; set; }

        public KeybindList warpButtons { get; set; }

        public KeybindList channelButtons { get; set; }

        public KeybindList favouriteButtons { get; set; }

        public string modDifficulty { get; set; }
        
        public string dialogueOption { get; set; }

        public string captionOption { get; set; }

        public bool autoProgress { get; set; }
        
        public int paceProgress { get; set; }

        public int setMilestone { get; set; }

        public bool setOnce { get; set; }

        public bool maxDamage { get; set; }

        public bool slotAttune { get; set; }

        public bool slotFreedom { get; set; }

        public bool slotReverse { get; set; }

        public string potionDefault { get; set; }

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

        public int cultivatePlot { get; set; }
        
        public bool cultivateTallCrops { get; set; }

        public bool disableShopdata { get; set; }

        public int meteorBehaviour { get; set; }

        public bool cardinalMovement { get; set; }

        public bool decorateGrove { get; set; }

        //public bool plantGrove { get; set; }

        public bool reverseJournal { get; set; }

        public bool activeJournal { get; set; }
        
        public bool enableGothic { get; set; }

        public bool enableCrossover { get; set; }

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
            witch,
            none,

        }

        public enum dialogueOptions
        {
            auto,
            fulltext,
            emotes,
            none,
        }

        public enum captionOptions
        {
            auto,
            textonly,
            none,
        }

        public enum modOptions
        {
            Default,
            Druid,
            Magic,
        }

        public enum potionDefaults
        {
            automatic,
            noconsume,
            nobrew,
            disabled

        }

        public ModData()
        {
            
            modVersion = modOptions.Default.ToString();
            riteButtons = KeybindList.Parse("MouseX1,MouseX2,V,LeftShoulder");
            actionButtons = KeybindList.Parse("MouseLeft,C,ControllerX");
            specialButtons = KeybindList.Parse("MouseRight,X,ControllerY");
            journalButtons = KeybindList.Parse("K");
            skillsButtons = KeybindList.Parse("L");
            relicsButtons = KeybindList.Parse("I");
            herbalismButtons = KeybindList.Parse("O");
            warpButtons = KeybindList.Parse("P");
            channelButtons = KeybindList.Parse("LeftShift,RightShift");
            favouriteButtons = KeybindList.Parse("Home");
            reverseJournal = false;
            activeJournal = true;
            dialogueOption = dialogueOptions.auto.ToString();
            captionOption = captionOptions.auto.ToString();
            autoProgress = false;
            paceProgress = 1;
            setMilestone = 0;
            setOnce = false;
            maxDamage = false;
            modDifficulty = difficulties.medium.ToString();
            slotAttune = false;
            slotFreedom = false;
            slotReverse = false;
            potionDefault = potionDefaults.automatic.ToString();
            slotOne = slotOptions.weald.ToString();
            slotTwo = slotOptions.mists.ToString();
            slotThree = slotOptions.stars.ToString();
            slotFour = slotOptions.fates.ToString();
            slotFive = slotOptions.ether.ToString();
            slotSix = slotOptions.witch.ToString();
            slotSeven = slotOptions.none.ToString();
            slotEight = slotOptions.none.ToString();
            slotNine = slotOptions.none.ToString();
            slotTen = slotOptions.none.ToString();
            slotEleven = slotOptions.none.ToString();
            slotTwelve = slotOptions.none.ToString();
            cultivateBehaviour = 2;
            cultivatePlot = 3;
            cultivateTallCrops = false;
            disableShopdata = false;
            meteorBehaviour = 3;
            cardinalMovement = false;
            decorateGrove = false;
            //plantGrove = false;
            enableGothic = false;
            enableCrossover = true;
            dragonScale = 3;
            dragonScheme = 1;
            dragonBreath = 0;
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


}