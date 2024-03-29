﻿using StardewModdingAPI.Utilities;
using StardewValley.Characters;

namespace StardewDruid
{

    public class ModData
    {
        public KeybindList riteButtons { get; set; }

        public KeybindList actionButtons { get; set; }

        public KeybindList specialButtons { get; set; }

        public KeybindList journalButtons { get; set; }

        public bool slotAttune { get; set; }

        public bool slotFreedom { get; set; }

        public bool autoProgress { get; set; }

        public string colourPreference { get; set; }

        public bool cardinalMovement { get; set; }

        public bool slotConsume { get; set; }

        public bool consumeRoughage { get; set; }

        public bool consumeQuicksnack { get; set; }

        public bool consumeCaffeine { get; set; }

        public int newProgress { get; set; }

        public bool castAnywhere { get; set; }

        public bool maxDamage { get; set; }

        public string combatDifficulty { get; set; }

        public bool reverseJournal { get; set; }

        public bool partyHats { get; set; }

        public bool disableHands { get; set; }

        public bool disableSeeds { get; set; }

        public bool disableFish { get; set; }

        public bool disableTrees { get; set; }

        public bool disableGrass { get; set; }

        public ModData()
        {
            riteButtons = KeybindList.Parse("MouseX1,MouseX2,V,LeftShoulder");
            actionButtons = KeybindList.Parse("MouseLeft,C,ControllerX");
            specialButtons = KeybindList.Parse("MouseRight,X,ControllerY");
            journalButtons = KeybindList.Parse("K");
            disableHands = false;
            slotAttune = false;
            slotFreedom = false;
            autoProgress = false;
            colourPreference = "Red";
            cardinalMovement = false;
            newProgress = -1;
            slotConsume = false;
            consumeRoughage = true;
            consumeQuicksnack = true;
            consumeCaffeine = true;
            maxDamage = false;
            combatDifficulty = "medium";
            castAnywhere = false;
            reverseJournal = false;
            partyHats = false;
            disableSeeds = false;
            disableFish = false;
            disableTrees = false;
            disableGrass = false;
        }

    }

}