using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Dialogue;
using StardewDruid.Event.Challenge;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Companions;
using StardewValley.GameData.HomeRenovations;
using StardewValley.Locations;
using StardewValley.Minigames;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xTile;
using xTile.Dimensions;
using static StardewDruid.Cast.Rite;

namespace StardewDruid.Data
{
    public static class DialogueData
    {


        public static string RiteNames(Rite.rites rite = rites.weald)
        {
            return Mod.instance.Helper.Translation.Get($"Enum.rites.{rite}.name");

        }

        public enum stringkeys
        {
            stardewDruid,

            // Mod, Rite, ModUtility

            receivedData,
            challengeAborted,
            stamina,
            openJournal,
            riteTool,
            noRiteAttuned,
            noToolAttunement,
            nothingHappened,
            invalidLocation,
            energyContinue,
            energyRite,
            riteBuffDescription,
            energySkill,
            dragonBuff,
            dragonBuffDescription,
            druidFreneticism,
            speedIncrease,

            // Journal

            grimoire,
            reliquary,
            dragonomicon,
            apothecary,

            startPage,
            endPage,
            sortCompletion,
            reverseOrder,
            openQuests,
            openEffects,
            openRelics,
            openPotions,

            checkHerbalism,

            hostOnly,
            questReplay,
            outOf,
            reward,
            bounty,
            transcript,

            acEnabled,
            acDisabled,
            acPriority,
            MAX,
            HP,
            STM,
            
            relicUnknown,
            relicNotFound,

            primaryColour,
            secondaryColour,
            tertiaryColour,
            dragonScheme,
            breathScheme,
            dragonRotate,
            dragonScale,
            dragonAccent,
            dragonEye,
            dragonReset,
            dragonSave,

            skipQuest,
            replayQuest,
            replayTomorrow,
            replayReward,
            cancelReplay,
            viewEffect,
            massHerbalism,

            // Miscellaneous
            trashCollected,
            bomberInterruptions,
            slimesDestroyed,
            learnRecipes,
            theDusting,
            abortTomorrow,
            noJunimo,
            noInstructions,
            leftEvent,
            leavingEvent,
            ladderAppeared,
            returnLater,
            reachEnd,
            treasureHunt,

        }

        public static string Strings(stringkeys key)
        {

            if (Enum.IsDefined(key)) {
                return Mod.instance.Helper.Translation.Get($"Enum.stringkeys.{key}");
            }
            else {
                return Mod.instance.Helper.Translation.Get("nevermind");
            }

            // ?
            // return "(nevermind)";

        }

        public static Dictionary<int, string> DialogueNarrators(string scene)
        {
            Dictionary<int, string> sceneNarrators = new();

            switch (scene)
            {

                case QuestHandle.approachEffigy:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.approachEffigy.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.approachEffigy.1"),
                    };

                    break;

                case QuestHandle.swordWeald:
                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.swordWeald.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.swordWeald.1"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueNarrators.swordWeald.2"),
                    };
                    break;

                case QuestHandle.challengeWeald:

                    sceneNarrators = new() {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeWeald.0"),
                    };

                    break;

                case QuestHandle.swordMists:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.swordMists.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.swordMists.1"),
                    };

                    break;

                case QuestHandle.questEffigy:
                    
                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questEffigy.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questEffigy.1"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questEffigy.2"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questEffigy.3"),
                    };

                    break;

                case QuestHandle.challengeMists:

                    sceneNarrators = new() {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.approachEffigy.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeMists.1"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeMists.2"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeMists.3"),
                    };

                    break;

                case QuestHandle.swordStars:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.swordStars.0"),
                    };

                    break;

                case QuestHandle.challengeStars:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeAtoll.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeAtoll.1"),
                    };

                    break;

                case QuestHandle.challengeAtoll:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeAtoll.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeAtoll.1"),
                    };

                    break;

                case QuestHandle.challengeDragon:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeDragon.0"),
                    };

                    break;

                case QuestHandle.swordFates:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.swordFates.0"),
                    };

                    break;

                case QuestHandle.questJester:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questJester.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questJester.1"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questJester.2"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questJester.3"),
                        [4] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questJester.4"),
                    };

                    break;

                case QuestHandle.challengeFates:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeFates.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeFates.1"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeFates.2"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeFates.3"),
                        [4] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeFates.4"),
                        [5] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeFates.5"),
                        [6] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeFates.6"),
                        [7] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeFates.7"),
                    };

                    break;

                case QuestHandle.swordEther:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.swordEther.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.swordEther.1"),
                    };

                    break;

                case QuestHandle.questShadowtin:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questShadowtin.0"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questShadowtin.1"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questShadowtin.2"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questShadowtin.3"),
                        [4] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questShadowtin.4"),
                        [5] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questShadowtin.5"),
                        [6] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questShadowtin.6"),
                        [7] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questShadowtin.7"),
                        [8] = Mod.instance.Helper.Translation.Get("DialogueNarrators.questShadowtin.8"),

                    };

                    break;

                case QuestHandle.challengeEther:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.challengeEther.0"),
                    };

                    break;

                case "treasureChase":

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueNarrators.treasureChase.0"),
                    };

                    break;

            };

            return sceneNarrators;

        }

        public static Dictionary<int, Dictionary<int, string>> DialogueScene(string scene)
        {

            Dictionary<int, Dictionary<int, string>> sceneDialogue = new();

            switch (scene)
            {
                case QuestHandle.approachEffigy:
                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.approachEffigy.1.0"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.approachEffigy.2.0"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.approachEffigy.3.0"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.approachEffigy.4.0"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.approachEffigy.5.0"), },
                        [6] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.approachEffigy.6.1"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueScene.approachEffigy.900.999").Tokens(new { button = Mod.instance.Config.riteButtons.ToString() }), },
                    };


                    break;

                case QuestHandle.swordWeald:

                    sceneDialogue = new()
                    {

                        [1] = new() {
                            [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordWeald.1.0"),
                            [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordWeald.1.1"),
                            [2] = Mod.instance.Helper.Translation.Get("DialogueScene.swordWeald.1.2"),
                        },
                        [2] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordWeald.2.1"), },
                        [3] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.swordWeald.3.2"), },
                        [4] = new() {
                            [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordWeald.4.0"),
                            [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordWeald.4.1"),
                            [2] = Mod.instance.Helper.Translation.Get("DialogueScene.swordWeald.4.2"),
                        },

                    };

                    break;


                case QuestHandle.challengeWeald:

                    sceneDialogue = new()
                    {

                        [22] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.22.0"), },
                        [25] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.25.0"), },
                        [28] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.28.0"), },
                        [31] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.31.0"), },
                        [34] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.34.0"), },
                        [37] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.37.0"), },
                        [40] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.40.0"), },
                        [43] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.43.0"), },

                     //  ?
                     // [54] = new() { [0] = "CHEEEP", },
                     // [54] = new() { [0] = "The time of vengeance draws near", },

                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.54.0"), },
                        [57] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.57.0"), },
                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.60.0"), },
                        [63] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.63.0"), },
                        [69] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.69.0"), },
                        [72] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.72.0"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeWeald.900.999"), },
                    };

                    break;

                case QuestHandle.swordMists:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordMists.1.0"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordMists.2.0"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordMists.3.0"), },
                        [4] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordMists.4.1"), },

                    };

                    break;

                case QuestHandle.questEffigy:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.1.0"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.2.0"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.3.0"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.4.0"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.5.0"), },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.6.0"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.7.0"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.8.0"), },
                        [9] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.9.0"), },
                        [10] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.10.0"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.11.0"), },
                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.12.0"), },
                        [13] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.13.0"), },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.14.0"), },
                        [15] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.15.0"), },

                        [16] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.16.1"), },
                        [17] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.17.0"), },
                        [18] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.18.1"), },
                        [19] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.19.1"), },
                        [20] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.20.0"), },
                        [21] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.21.1"), },
                        [22] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.22.1"), },
                        [23] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.23.0"), },
                        [24] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.24.1"), },

                        [25] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.25.1"), },
                        [26] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.26.0"), },
                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.27.0"), },
                        [28] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.28.1"), },
                        [29] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.29.0"), },
                        [30] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.30.1"), },
                        
                        [31] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.31.0"), },
                        [32] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.32.0"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.33.0"), },
                        [34] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.34.0"), },
                        [35] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.35.0"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.36.0"), },
                        [37] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.37.0"), },

                        [502] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.502.0"), },
                        [506] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.506.0"), },
                        [508] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.508.3"), },
                        [511] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.511.2"), },
                        [514] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.514.3"), },
                        [517] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.517.3"), },
                        [520] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.520.3"), },
                        [523] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.523.2"), },
                        [526] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.526.3"), },
                        [529] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.529.2"), },
                        [532] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.532.3"), },
                        [535] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.535.2"), },
                        [538] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.538.3"), },
                        [541] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.541.3"), },
                        [544] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.544.3"), },
                        [547] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.547.2"), },
                        [550] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.550.3"), },
                        [553] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.553.2"), },
                        [556] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.556.3"), },
                        [559] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.559.3"), },
                        [561] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.561.2"), },
                        [564] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.564.0"), },
                        [567] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.567.0"), },

                        [777] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questEffigy.777.0"), },
                    };

                    break;

                case QuestHandle.challengeMists:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.2.0"), },
                        [4] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.4.1"), },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.6.0"), },
                        [9] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.9.0"), },
                        [11] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.11.3"), },
                        [13] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.13.0"), },
                        [18] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.18.0"), },
                        [20] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.20.3"), },
                        [22] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.22.0"), },
                        [25] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.25.0"), },
                        [28] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.28.1"), },
                        [31] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.31.0"), },
                        [35] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.35.0"), },
                        [38] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.38.1"), },
                        [40] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.40.0"), },
                        [42] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.42.1"), },
                        [44] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.44.0"), },
                        [48] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.48.0"), },
                        [51] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.51.2"), },
                        [53] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.53.3"), },
                        [55] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.55.0"), },
                        [57] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.57.2"), },
                        [59] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.59.0"), },
                        [61] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.61.2"), },
                        [63] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.63.0"), },
                        [65] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.65.2"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.900.999"), },
                        [901] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.901.0"), },
                        [902] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeMists.902.0"), },

                    };
                    
                    break;

                case QuestHandle.swordStars:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordStars.1.0"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordStars.2.0"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordStars.3.0"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordStars.4.0"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordStars.5.0"), },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordStars.6.0"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordStars.7.0"), },

                    };

                    break;
                
                case QuestHandle.challengeStars:

                    sceneDialogue = new()
                    {

                        [3] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.3.1"), },

                        [12] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.12.1"), },

                        [15] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.15.0"), },
                        [18] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.18.0"), },
                        [21] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.21.0"), },
                        [24] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.24.0"), },
                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.27.0"), },

                        [30] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.30.1"), },
                        [33] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.33.1"), },

                        [38] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.38.0"), },
                        [41] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.41.0"), },

                        [48] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.48.0"), },
                        [51] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.51.0"), },
                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.54.0"), },

                        [57] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.57.1"), },

                        [65] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.65.0"), },
                        [68] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.68.0"), },
                        [71] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.71.0"), },

                        [74] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeStars.74.1"), },

                    };

                    break;

                case QuestHandle.challengeAtoll:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.1.0"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.4.0"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.7.0"), },
                        // cannons
                        [16] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.16.0"), },
                        [19] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.19.0"), },
                        [22] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.22.0"), },
                        // cannons
                        [25] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.25.0"), },
                        [34] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.34.0"), },
                        [37] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.37.0"), },
                        // cannons
                        [49] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.49.0"), },
                        [52] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.52.1"), },
                        [55] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.55.1"), },
                        // cannons
                        [64] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.64.0"), },
                        [67] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.67.0"), },
                        [70] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.70.0"), },
                        [73] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.73.0"), },
                        [76] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.76.1"), },
                        [79] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.79.1"), },

                        [990] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.990.0"), },
                        [991] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.991.0"), },
                        [992] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.992.0"), },
                        [993] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.993.0"), },
                        [994] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.994.0"), },
                        [995] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeAtoll.995.0"), },


                    };

                    break;

                case QuestHandle.challengeDragon:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.1.0"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.4.0"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.7.0").Tokens(new { castType = Mod.instance.rite.castType.ToString() }), },

                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.12.0"), },
                        [15] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.15.0"), },
                        [18] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.18.0"), },

                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.27.0"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.30.0"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.33.0"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.36.0"), },

                        [42] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.42.0"), },
                        [45] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.45.0"), },
                        [48] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.48.0"), },

                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.54.0"), },
                        [57] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.57.0"), },
                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.60.0"), },

                        [81] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.81.0"), },
                        [84] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.84.0"), },
                        [87] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.87.0"), },
                        [90] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.90.0"), },
                        [93] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.93.0"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeDragon.900.999"), },

                    };

                    break;

                case QuestHandle.swordFates:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.2.0"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.5.0"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.8.0"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.11.0"), },

                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.27.0"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.30.0"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.33.0"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.36.0"), },

                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.54.0"), },
                        [57] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.57.0"), },
                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.60.0"), },

                        [81] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.81.0"), },
                        [84] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.84.0"), },

                        [91] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.91.0"), },
                        [94] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.94.0"), },
                        [97] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.97.0"), },
                        [121] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.121.0"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueScene.swordFates.900.999"), },

                    };

                    break;

                case QuestHandle.questJester:

                    sceneDialogue = new()
                    {


                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.1.0"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.2.0"), },
                        [3] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.3.2"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.4.0"), },
                        [5] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.5.2"), },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.6.0"), },
                        [7] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.7.2"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.8.0"), },
                        [9] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.9.2"), },
                        [10] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.10.0"), },
                        [11] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.11.2"), },
                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.12.0"), },
                        [13] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.13.2"), },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.14.0"), },
                        [15] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.15.2"), },
                        [16] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.16.0"), },
                        [17] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.17.0"), },
                        [18] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.18.2"), },
                        [19] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.19.2"), },
                        [20] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.20.0"), },
                        [21] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.21.2"), },
                        [22] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.22.2"), },
                        [23] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.23.2"), },
                        [24] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.24.0"), },

                        [25] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.25.0"), },
                        [26] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.26.0"), },
                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.27.0"), 
                            [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.27.1")},
                        [28] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.28.0"), },
                        [29] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.29.0"), },
                        [30] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.30.1"), },
                        [31] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.31.0"), },
                        [32] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.32.0"), },
                        [33] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.33.1"), },
                        [34] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.34.0"), },
                        [35] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.35.1"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.36.0"), },
                        [37] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.37.1"), },
                        [38] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.38.0"), },
                        [39] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.39.1"), },
                        [40] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.40.0"), },
                        [41] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.41.999"), },
                        [42] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.42.1"), },
                        [43] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.43.0"), },
                        [44] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.44.999"), },

                        [45] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.45.0"), },
                        [46] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.46.1"), },
                        [47] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.47.0"), },
                        [48] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.48.1"), },
                        [49] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.49.0"), },
                        [50] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.50.1"), },
                        [51] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.51.0"), },
                        [52] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.52.1"), },
                        [53] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.53.1"), },
                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.54.0"), },
                        [55] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.55.1"), },
                        [56] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.56.0"), },
                        [57] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.57.1"), },
                        [58] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.58.0"), },
                        [59] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.59.1"), },
                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.60.0"), },

                        [61] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.61.1"), },
                        [62] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.62.1"), },
                        [63] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.63.0"), },
                        [64] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.64.1"), },
                        [65] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.65.0"), },
                        [66] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.66.1"), },
                        [67] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.67.0"), },
                        [68] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.68.1"), },
                        [69] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.69.0"), },
                        [70] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.70.1"), },
                        [71] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.71.0"), },
                        [72] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.72.0"), },
                        [73] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.73.1"), },
                        [74] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.74.0"), },
                        [75] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.75.1"), },
                        [76] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.76.0"), },
                        [77] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.77.1"), },
                        [78] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.78.0"), },
                        [79] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.79.0"), },
                        [80] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.80.1"), },
                        [81] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.81.1"), },
                        [82] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.82.0"), },
                        [83] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.83.0"), },


                        [901] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.901.3"), },
                        [903] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.903.0"), },
                        [904] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.904.3"), },
                        [906] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.906.1"), },
                        [907] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.907.3"), },
                        [910] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.910.3"), },
                        [912] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.912.0"), },
                        [913] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.913.3"), },
                        [915] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.915.1"), },
                        [916] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.916.3"), },
                        [919] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.919.0"), },
                        [922] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.922.4"), },
                        [923] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.923.3"), },
                        [925] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.925.4"), },
                        [928] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.928.3"), },
                        [931] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.931.4"), },
                        [934] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.934.3"), },
                        [937] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.937.4"), },
                        [940] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.940.4"), },
                        [943] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.943.0"), },
                        [946] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.946.4"), },
                        [949] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.949.3"), },
                        [952] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.952.4"), },
                        [955] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.955.4"), },
                        [958] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.958.3"), },
                        [961] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.961.3"), },
                        [963] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.963.1"), },
                        [965] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.965.3"), },
                        [968] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questJester.968.0"), },
                        
                    };

                    break;


                case QuestHandle.challengeFates:
                    /*
                    sceneNarrators = new()
                    {
                        [0] = "The Effigy",
                        [1] = "Jester",
                        [2] = "Buffin",
                        [3] = "Shadow Leader",
                        [4] = "Shadow Sergeant",
                        [5] = "Shadow Goblin",
                        [6] = "Shadow Rogue",
                    };*/
                    sceneDialogue = new()
                    {

                        [2] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.2.3"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.5.0"), },
                        [8] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.8.3"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.11.0"), },
                        [14] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.14.1"), },
                        [17] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.17.3"), },
                        [20] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.20.3"), },
                        [23] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.23.2"), },
                        [26] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.26.1"), },

                        [30] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.30.4"), },
                        [33] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.33.3"), },
                        [36] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.36.7"), },

                        [40] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.40.1"), },
                        [43] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.43.2"), },
                        [46] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.46.7"), },

                        [50] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.50.6"), },
                        [53] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.53.0"), },
                        [56] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.56.0"), },

                        [60] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.60.3"), },
                        [63] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.63.3"), },
                        [66] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.66.7"), },

                        [70] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.70.2"), },
                        [73] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.73.1"), },
                        [76] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.76.5"), },

                        [80] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.80.4"), },
                        [83] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.83.3"), },
                        [86] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.86.3"), },

                        [90] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.90.3"), },
                        [93] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.93.4"), },
                        [96] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.96.3"), },
                        [99] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.99.5"), },
                        [102] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.102.6"), },
                        [105] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.105.5"), },
                        [108] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.108.4"), },
                        [111] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.111.4"), },
                        [114] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.114.5"), },
                        [117] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.117.3"), },
                        [120] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.120.6"), },
                        [123] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.123.5"), },

                        [126] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.126.1"), },
                        [129] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.129.2"), },
                        [132] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeFates.132.0"), },

                    };

                    break;

                case QuestHandle.swordEther:

                    sceneDialogue = new()
                    {
                        [3] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.3.1"), },
                        [6] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.6.1"), },
                        [9] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.9.0"), },
                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.12.0"), },

                        [15] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.15.1"), },
                        [18] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.18.1"), },

                        [21] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.21.0"), },
                        [24] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.24.0"), },
                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.27.0"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.30.0"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.33.0"), },

                        [42] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.42.1"), },
                        [45] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.45.1"), },
                        [48] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.48.1"), },
                        [51] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.51.1"), },

                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.60.0"), },
                        [63] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.63.0"), },

                        [75] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.75.1"), },
                        [78] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.78.1"), },
                        [81] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.81.1"), },
                        [84] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.84.1"), },

                        [93] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.93.0"), },
                        [96] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.96.0"), },

                        [991] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.991.0"), },
                        [992] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.swordEther.992.1"), },
                    };

                    break;

                case QuestHandle.questShadowtin:

                    sceneDialogue = new()
                    {
                        // Dwarf interaction
                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.1.0"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.2.0"), },
                        [3] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.3.1"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.4.0"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.5.0"), },
                        [6] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.6.1"), },
                        [7] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.7.1"), },
                        [8] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.8.1"), },
                        [9] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.9.0"), },

                        [100] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.100.0"), },
                        [101] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.101.0"), },
                        [102] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.102.0"), },
                        [103] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.103.0"), },

                        [200] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.200.0"), },
                        [201] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.201.2"), },
                        [202] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.202.3"), },
                        [203] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.203.0"), },
                        [204] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.204.0"), },
                        [205] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.205.2"), },
                        [206] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.206.3"), },
                        [207] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.207.0"), },
                        [208] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.208.0"), },
                        [209] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.209.0"), },
                        [210] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.210.4"), },
                        [211] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.211.4"), },
                        [212] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.212.5"), },
                        [213] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.213.5").Tokens(new { playerName = Game1.player.Name }), },
                        [214] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.214.5"), },
                        [215] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.215.4"), },
                         
                        [300] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.300.6"), },
                        [301] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.301.6"), },
                        [302] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.302.6"), },
                        [303] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.303.6"), },
                        [304] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.304.6"), },
                        [305] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.305.6"), },
                        [306] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.306.6"), },
                        [307] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.307.6"), },
                        [308] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.308.6"), },
                        [309] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.309.4"), },
                        [310] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.310.6"), },

                        [400] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.400.5"), },
                        [401] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.401.5"), },
                        [402] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.402.0"), },
                        [403] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.403.0"), },
                        [404] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.404.0"), },
                        [405] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.405.5"), },
                        [406] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.406.5"), },
                        [407] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.407.5"), },
                        [408] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.408.2"), },
                        [409] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.409.2"), },
                        [410] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.410.2"), },
                        [411] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.411.3"), },

                        [500] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.500.0"), },
                        [501] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.501.0"), },
                        [502] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.502.0"), },
                        [503] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.503.0"), },
                        [504] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.504.0"), },
                        [505] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.505.0"), },

                        [600] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.600.7"), },
                        [601] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.601.8"), },
                        [602] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.602.0"), },
                        [603] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.603.7"), },
                        [604] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.604.7"), },
                        [605] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.605.0"), },
                        [606] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.606.0"), },
                        [607] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.607.7"), },
                        [608] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.608.1"), },
                        [609] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.609.0"), },
                        [610] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.610.8"), },
                        [611] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.611.1"), },
                        [612] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.612.0"), },
                        [613] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.613.0"), },
                        [614] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.614.0"), },
                        [615] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.615.7"), },
                        [616] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.616.8"), },
                        [617] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.617.1"), },
                        [618] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueScene.questShadowtin.618.1"), },

                    };

                    break;


                case QuestHandle.challengeEther:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.2.0"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.4.0"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.7.0"), },
                        [10] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.10.0"), },
                        [13] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.13.0"), },

                        [16] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.16.0"), },
                        [19] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.19.0"), },
                        [22] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.22.0"), },

                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.27.0"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.30.0"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.33.0"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.36.0"), },

                        [52] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.52.0"), },
                        [55] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.55.0"), },
                        [58] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.58.0"), },
                        [61] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.61.0"), },
                        [64] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.64.0"), },
                        [67] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.67.0"), },

                        [76] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.76.0"), },
                        [79] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.79.0"), },
                        [82] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.82.0"), },
                        [85] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.challengeEther.85.0"), },

                    };

                    break;

                case "treasureChase":

                    sceneDialogue = new()
                    {

                        [990] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueScene.treasureChase.990.999"), },
                        [991] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueScene.treasureChase.991.999"), },

                    };

                    break;

                    /*
                    case "challengeGemShrine":
                    case "challengeGemShrineTwo":

                        sceneDialogue = new()
                        {
                            [1] = new() { [0] = "So the Sisters raised another to their priesthood", },
                            [4] = new() { [0] = "It matters not", },
                            [6] = new() { [0] = "They will not reclaim her", },

                            [10] = new() { [0] = "It was I who made it possible", },
                            [15] = new() { [0] = "For the first star to fall from heaven", },
                            [20] = new() { [0] = "Why did I profane my sacred duty", },
                            [25] = new() { [0] = "Why did I desecrate the boundaries of the planes", },
                            [30] = new() { [0] = "I witnessed their love", },
                            [35] = new() { [0] = "Shine through the smoke of war and ignorance", },
                            [40] = new() { [0] = "Beauty that warmed my frozen heart", },
                            [45] = new() { [0] = "Yoba forgive me", },

                            [991] = new() { [0] = "Abandon your folly. It cannot be undone.", },
                            [992] = new() { [0] = "the Sisters chose poorly", },

                        };

                        break;

                    case "swordEther":
                    case "swordEtherTwo":

                        sceneDialogue = new()
                        {

                            [1] = new() { [0] = "a taste of the stars", },
                            [3] = new() { [0] = "from the time when the shamans sang to us", },
                            [5] = new() { [0] = "and my kin held dominion", },
                            [7] = new() { [0] = "...my bones stir...", },

                            [991] = new() { [0] = "the power of the shamans lingers", },
                            [992] = new() { [0] = "You're no match for me", },
                            [201] = new() { [1] = "...yesss...", },
                            [203] = new() { [1] = "you have done well, shaman", },
                            [205] = new() { [1] = "...I return...", },
                            [215] = new() { [1] = "For centuries I lingered in bone", },
                            [220] = new() { [1] = "As the reaper leeched my life force", },
                            [225] = new() { [1] = "But an ancient is never truly gone", },
                            [230] = new() { [1] = "As long as my ether remains", },
                            [235] = new() { [1] = "I will gather the essence of your soul", },
                            [240] = new() { [1] = "And fashion a new form from your pieces", },
                            [245] = new() { [1] = "The Mistress of Fortune will face my wrath", },
                            [250] = new() { [1] = "I will make her my servant", },

                            [991] = new() { [0] = "Thanatoshi... why...", },
                            [992] = new() { [1] = "...rwwwghhhh...", },
                        };

                        break;

                    */
            };

            return sceneDialogue;

        }

        public static Dictionary<int,DialogueSpecial> SceneConversations(string scene)
        {

            Dictionary<int, DialogueSpecial> conversations = new();

            switch (scene)
            {

                case QuestHandle.approachEffigy:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = "The Forgotten Effigy: So a successor appears. I am the Effigy, crafted by the First Farmer, sustained by the old powers, and bored.",

                            responses = new()
                            {
                                "Who stuck you in the ceiling?",
                                "I inherited this plot from my grandfather. His notes didn't say anything about a magic scarecrow.",
                                "(Say nothing)",
                            },

                            answers = new()
                            {

                                "One of the leylines of the valley seams through the bedrock of this cavern. This is where I have spent many of your centuries, in stasis, listening to the energies of the Weald, leaving periodically to witness the change of seasons. " +
                                "The last time I ventured out, your predecessor had already departed this plane. I found the farm abandoned, the mining town, diminished. Now strange shadows stalk the sacred spaces, and it has not been safe for me to leave this refuge."

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = "The Forgotten Effigy: The valley didn't always seem so inhospitable. The farmers were once aligned with the otherworld. They formed a circle of Druids.",

                            responses = new()
                            {
                                "I would love to know more about the traditions of my forebearers.",
                                "I want to be like the farmers of old and form a circle",
                                "(Say nothing)",
                            },

                            answers = new()
                            {
                                "Very well. Meet me in the grove outside, and we will test your aptitude for the otherworld.",
                            },

                            questContext = 200,

                        },

                    };

                    break;

                case QuestHandle.swordWeald:


                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = "Sighs of the Earth: What say you, farmer?",

                            responses = new()
                            {
                                "I seek the blessing of the Two Kings to reform the circle of Druids.",
                                "Ok. Whoever's behind the rock, come on out.",

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = "Whispers on the wind: The monarchs remain dormant, their realm untended. Who are you to claim the inheritance of the broken circle?",

                            responses = new()
                            {
                                "The valley is my home now. I want to care for and protect it.",
                                "I'm being friendly and playing along with your little game. Just dont pull down my pants or anything.",

                            },

                            questContext = 200,

                        },                        
                        
                        [3] = new()
                        {

                            intro = "Rustling in the Woodland: It is not an easy path, the one tread by a squire of the Two Kings. Are you ready to serve?",

                            responses = new()
                            {

                                "I will serve the sleeping monarchs like the druids of yore.",
                                "Serve... tea? Tennis ball?",

                            },

                            questContext = 300,

                        },

                    };

                    break;               
                
                case QuestHandle.swordMists:


                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = "Murmurs of the waves: We thank you for restoring our sacred waters. Though you are young. And dry. This is unexpected.",

                            responses = new()
                            {
                                "I harken to the Voice Beyond the Shore, as I was called.",
                                "Creepy voices. Creepy voices everywhere. And I never have something to record them with either.",

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = "Murmurs of the waves: Speak, friend. She listens.",

                            responses = new()
                            {
                                "Dear Lady, you once blessed the first farmer. I am their successor.",
                                "So, is this like a prayer? Do I close my eyes...",

                            },

                            questContext = 200,

                        },                        
                        
                        [3] = new()
                        {

                            intro = "(Distant voice): I hear you, successor.",

                            responses = new()
                            {
                                "My Lady, I will be your champion in the realm before the shore.",
                                "How? I'm not even on a long distance frequency.",

                            },

                            questContext = 300,

                        },

                    };


                    break;

                case QuestHandle.questEffigy:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = "The Effigy: Here again. At the hem of the valley.",

                            responses = new()
                            {
                                "Is the beach how you remember it?"
                            },

                            answers = new()
                            {
                                "The sands and waves glimmer as they once did, but I think I remember them differently. " +
                            "Perhaps they will appear more familiar when I've spent some time here."

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = "The Effigy: I've only ever possessed a small amount of talent in invoking the energies of the Weald.",

                            responses = new()
                            {
                                "I thought what you just did was great. I'm surprised you consider yourself untalented in the Weald.",

                            },

                            answers = new()
                            {
                                "You conduct the energies with a grace I do not possess. " +
                                "They panic and scatter at my touch, as if compelled by violence instead of by the gentleness I hope to express. " +
                                "Yes. The farm and grove of my former masters want for a better caretaker."
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = "The Effigy: That peculiar cooking technique was taught to me by the Lady herself. It was a favourite dish of the first farmer's. He was her champion.",

                            responses = new()
                            {
                                "You once told me the first farmer knew her.",
                                "So it's true then, you were created with the Lady's power?",
                            },

                            answers = new()
                            {
                                "When I came into this world, a great war between dragons and elderfolk had just rescinded, with the dominions of both parties in ruins. " +
                                "It was decreed by the fates that humans and other survivors would work together to revitalise the valley. " +
                                "The Lady Beyond and my master built a little garden of hope in the valley, the first farm. " +
                                "From the moment I awoke in this form, the circle of druids has been my mission, and my home. " +
                                "But the Fates lost interest in our world, and the elderborn departed. " +
                                "I have lost count of the days since that time."
                            },

                            questContext = 250,

                        },

                        [4] = new()
                        {

                            intro = "The Effigy: That menace. I swear that when I find the means, I will turn him to juice and rind. Something about that insidious, gleeful countenance inspires me to rage.",

                            responses = new()
                            {
                                "Sociopathic slime monsters tend to talk smack.",
                                "That blob was the most disgusting thing I've ever seen... today.",
                            },

                            answers = new()
                            {
                                "Though it pains me to admit, the Jellyking spoke the truth about my present position. I am not an adequate guardian of the change. " +
                                "Such duties have fallen to the wayside, and others have claimed custodianship of the sacred spaces, such as the Bat Church, or the Wizard, and his ally. " +
                                "Even the slime can see it. I have failed the Circle."
                            },

                            questContext = 400,

                        },

                        [5] = new()
                        {

                            intro = "The Effigy: The rolling energies of the mists gather here.",

                            responses = new()
                            {
                                "Are those... wisps?",
                                "Along with dust, shells, driftwood, and the salty tears of failed anglers",
                            },

                            answers = new()
                            {
                                "The will of the sleeping monarchs, their dreams and promises, become the wisps. " +
                                "In a gentle moment, they revealed themselves to me, and have continued to keep me company, in the long periods of loneliness between times of stasis. " +
                                "Now they reveal themselves to you too. It is a special privilege.",
                            },

                            questContext = 500,

                        },

                        [6] = new()
                        {

                            intro = "The Effigy: I witnessed this, many ages ago, when I was still new to this world. " +
                                "I was unable to assist my friend with his troubled heart.",

                            responses = new()
                            {

                                "You kept the circle active in the valley all this time.",
                                "Your friend was heartbroken. Such is life. His burdens are no longer yours to bear.",

                            },

                            answers = new()
                            {

                                "I can not be sure if I've honoured his friendship, as the final instruction, as you heard, was to learn the beauty of the valley. " +
                                "Even now, her distant voice asks me to abide here, and wait. I think I'll make my own decision. " +
                                "It's time for me to venture out of the hovel I've made in the secluded grove and follow your lead. You are my friend and mentor now, successor.",

                            },

                            questContext = 600,

                        },

                    };

                    break;


                case QuestHandle.swordStars:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = "The Revenant: Well now, a fellow human. Welcome to the Chapel of the Stars.",

                            responses = new()
                            {
                                "Master holy warrior, I have come, bearing the lantern of your order, to learn the ways of the Stars.",
                                "Human? You don't even have a face.",
                            },

                            answers = new()
                            {

                                "Ah... it was I that gifted that lantern you carry to the funny scarecrow man, back when he used to visit. So you're the successor he has been waiting for. ",

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = "The Revenant: Did he tell you the risks? Did he tell you what you could lose by this path?",

                            responses = new()
                            {
                                "He told me this is a necessary step towards obtaining the power the circle needs to defend our home.",
                                "I think I've exhausted all his dialogue.",
                            },

                            answers = new()
                            {
                                "Well, I'll be the first to admit that being a Holy Warrior of the Star Guardians has been pretty fun. Until the Fates cursed me with unlife. If you follow this path, you'll face your own reckoning one day.",
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = "The Revenant: If the scarecrow believes in you, I suppose that counts for something. Seems to me he never does things on a whim. ",

                            responses = new()
                            {
                                "I will do what needs to be done to lead the circle of Druids. I am not afraid.",
                                "You and the Effigy seem pretty desperate, so I'll help you out just this once.",
                            },

                            answers = new()
                            {
                                "I accept you as guardian in training. Take this holy sword as an emblem of your oath to the Stars. It belonged to the last trainee. They never used it.",

                            },

                            questContext = 300,

                        },

                    };

                    break;


                case QuestHandle.approachJester:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = "(The strange cat looks at you expectantly)",

                            responses = new()
                            {
                                "Greetings, you must be the representative from the Fae Court",
                                "Hello Kitty, are you far from home?",
                            },

                            answers = new()
                            {

                                "Greetings, person of interest. I am the Jester of Fate, and, uh... I guess I'm not really who you thought the Fae Court would send. " +
                                "I had a job as a court fool, and the best part was trying to make the High Priestess smile, but then the chance came to be a hero and go on a grand quest, and I took it. Now I'm here, trying to find a missing Celestial.",

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = "The Jester of Fate: Well farmer, you have the scent of destiny about you, and some otherworldly ability too. You must be the fabled acolyte of the celestials I was warned about.",

                            responses = new()
                            {
                                "I've received a few titles on my journey, but I prefer to be known as the Stardew Druid.",
                                "Meteorites, lightning bolts? Nah, can't say we see much of those out here.",
                                "(Say nothing and pretend the cat can't talk)",
                            },

                            answers = new()
                            {
                                "I propose a partnership. I teach you some of my special tricks, and you help me in my sacred quest. All I need to do is find the great Reaper of Fate and the world-threatening Star entity that he's hunting.",
                            },

                            questContext = 200,

                        },
                        
                        [3] = new()
                        {

                            intro = "The Jester of Fate: I'm sure you agree this would be great for both of us.",

                            responses = new()
                            {
                                "A practitioner of mysteries? This is truly fortuitous. I accept your proposal.",
                                "Well I could use a big cat on the farm.",
                            },

                            answers = new()
                            {
                                "Great! Now the adventure people said there's a dark dungeon to the east of here, full of peril and evil skull-heads. " +
                                "That's exactly the place I want to go! It's just the thought of going alone... well... uh... who doesn't like to have friends with them on an epic journey like this? " +
                                "Come see me when you're ready to venture forth, my brave and loyal farmer. (The mountain bridge must be repaired in order to proceed)",

                            },

                            questContext = 300,

                        },

                    };

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.relicWeald))
                    {

                        conversations[3].responses.Add("I'm not making any deals with a strange cat on a bridge built by forest spirits!");

                        string buffer = conversations[3].answers.First();

                        conversations[3].answers.Add(buffer);

                        conversations[3].answers.Add("Hehehe... I like you already! But you cannot escape this Fate, literally, and, well literally. " +
                                "Come see me when you're ready to explore the dungeon on the other side of this gap, I promise it will be worth your time! (The mountain bridge must be repaired in order to proceed)");

                    }

                    break;

                case QuestHandle.swordFates:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = "The Jester of Fate: Are you as creeped out as I am farmer?",

                            responses = new()
                            {
                                "After a hundred descents into the mines, I have become a master dungeon-explorer.",
                                "I saw a lot of body-less heads but not a lot of headless bodies. I suspect someone's stealing bodies.",
                            },

                            answers = new()
                            {

                                "You know what, after all that, I'm actually relieved we didnt run into Thanatoshi, because, to be honest, it's all a bit overwhelming. And what would I say if we even find him?",

                            },

                            questContext = 130,

                        },

                        [2] = new()
                        {
  
                            intro = "The Jester of Fate: These monuments are arranged like the court of the Fates. The Artisans, the Priesthood, the Morticians, and Chaos. ",

                            responses = new()
                            {
                                "Odd to think the locals produced this. I assume most of them are unaware of the mysteries of the Fates.",
                                "I don't want to throw shade at the Fates and all, but this has a real big cult vibe",
                            },

                            answers = new()
                            {
                                "I think I know this place. The first envoys to the valley came here to fix all the problems caused by the dragons and celestials and elderfolk and humans.",
                            },

                            questContext = 140,

                        },

                        [3] = new()
                        {

                            intro = "The Jester of Fate: Thanatoshi represented the Morticians. This is the last place he would have been seen by our kin. ",

                            responses = new()
                            {
                                "He's cursed a great number of souls, innocent or not, and I intend to hold him accountable.",
                                "Judging by his likeness in stone, I imagine he'll be warm and approachable.",
                            },

                            answers = new()
                            {
                                "I feel weird about all this. I think the switch I pressed opened a door to the outside in the south part of this cave. " +
                                "How about we go back to your place and practice tricks!",

                            },

                            questContext = 150,

                        },

                    };

                    break;

                case QuestHandle.questJester:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = "Jester of Fate: So this is the town of pelicans. I guess the humans killed all the birds when they took it over.",

                            responses = new()
                            {
                                "Not quite... it's probable that the town founders were the Pelican family.",
                                 "Indeed. It was quite an effort for us humans to overthrow our big billed overlords.",
                            },

                            answers = new()
                            {
                                "I wonder if the pelicans were still around, would they be able to tell us where the fallen star is."

                            },

                            questContext = 99,

                        },

                        [2] = new()
                        {

                            intro = "Jester of Fate: He's one of the good ones, farmer. Makes me sad.",
                            responses = new()
                            {
                                "Why would you be sad about Marlon?",
                                "It is a shame that there aren't more men with capes and eyepatches in the valley.",

                            },

                            answers = new()
                            {
                                "When I first came to the valley, I started at the last known position of Thanatoshi before he vanished in his pursuit of the fallen one. " +
                                "Even after a week I couldn't find a clue to his whereabouts. So I cried a little. I even yelled a bit. Well sort of cat-screamed. " +
                                "I guess kind of loudly, because the one-eyed cheese-eating adventure man told me to shut up. " +
                                "Then he offered me a spot to sleep by a warm fire, and I learned from him about the star crater, and that long tunnel of death we explored. " +
                                "Yet, just now, when he started talking about the shadow raiders, I received a vision of the oracles. " +
                                "The one eyed warrior will continue his crusade against the shadows of the valley, but he will meet his match against a cloaked warrior, and be claimed by the Fates of Death. " +
                                "Pretty grim. But hey, the priesthood doesn't always get things right. "
                            },

                            questContext = 199,

                        },

                        [3] = new()
                        {

                            intro = "Jester of Fate: Any clue where that cosmic fox went?",

                            responses = new()
                            {
                                "Probably eating cookies and sipping whiskey with the Muellers.",
                                "Within the insatiable maw of the town dog from which there is no escape.",
                                "Just check the trash. Everyone else does... right?",
                            },

                            answers = new()
                            {
                                "Sounds like something the Buffoonette of Chaos would do. " +
                                "Most fates get confused or angry by her attempts at fun. I'm one of her only friends."
                            },

                            questContext = 299,

                        },

                        [4] = new()
                        {

                            intro = "Jester of Fate: I have no idea who actually won.",

                            responses = new()
                            {
                                "I saw both of you arrive after me.",
                                "Buffin is quicker on the ground.",
                                "You Jester, you're a powerhouse of motion when you want to be.",
                            },

                            answers = new()
                            {
                                "Heh, it didn't seem like Buffin put in much effort. She's usually very foxy. " +
                                "I guess there's something else going on with her. There always is."
                            },

                            questContext = 799,

                        },

                        [5] = new()
                        {

                            intro = "Jester of Fate: I think we went too far.",
                            responses = new()
                            {
                                "Don't worry. The Junimos have a knack for fixing this kind of thing.",
                                "I don't see Gunther's precious wooden panelling being consumed by the flame. Is that even a real fire?",
                            },

                            answers = new()
                            {
                                "Buffin serves the Stream of Chaos, who occupies one of the four seats of the Fates. " +
                                "The stream has an influence on some of the more, uh, fun aspects of the mysteries of the Fates, so things like this tend to happen when we get together. " +
                                "(Jester sighs) This isn't what the high priestess expects of me."
                            },

                            questContext = 499,

                        },

                        [6] = new()
                        {

                            intro = "Jester of Fate: When Fortumei asked for a volunteer to take up the Reaper's trail, the only answer was silence. " +
                            "So I jested. I proclaimed, with big bravado, that I would get the job done.",

                            responses = new()
                            {

                                "You've done pretty well considering you never intended to get this far. Do you have any regrets?",
                                "That must have been quite the scene, with all those important Fates looking at you. Maybe some saw a fool, but some saw a hero. I see a hero.",

                            },

                            answers = new()
                            {

                                "The faithful certainly took it as a joke. One heckler said the earth cats would chase me away. " +
                                "An oracle foretold that my sparkly star cape would be torn to shreds. " +
                                "Buffin showed the court an image of me in tears, stuck in a ditch. All of which ended up happening for real but besides that, Fortumei took me at my word. " +
                                "I'm more than a joke.",

                            },

                            questContext = 599,

                        },

                    };

                    break;


                case QuestHandle.challengeFates:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            companion = 3,

                            intro = "Shadow Leader: My name is Shadowtin Bear. I am foremost a scholar of antiquity, but I was responsible for my company's operations on the surfaceland, and for that, I accept the consequence of my defeat.",

                            responses = new()
                            {
                                "Shadowtin, why did you infiltrate the valley?",
                                "Antiquity indeed. Your tactics were certainly outdated.",
                                "(Say nothing)",
                            },

                            answers = new()
                            {
                                "If you're familiar with the old legends, this is where the stars fell in the war that claimed the Dragons. " +
                                "We're, well, I guess 'they're', seeing as I've been demoted, searching for remnants of that war. Specifically, the power of the ancient ones over the Ether. " +
                                "Their efforts have been fruitless, save for a cache of writings and other ornaments found in the nearby tunnels. " +
                                "It appears to have been a repository for human followers of the Reaper of Fate. " +
                                "One of the texts mentions the tomb of Tyrannus Jin, once dragon king of this land, but there are no descriptions of its location.",

                            },

                            questContext = 144,

                        },

                        [2] = new()
                        {
                            companion = 3,

                            intro = "The Jester of Fate: He's tracking my kinsman! This is good for us, farmer, we might have finally found someone who can figure out where Thanatoshi went.",

                            responses = new()
                            {
                                "I feel inclined to deny your masters any power that might threaten peace in the valley. You will aid us instead.",
                                "I fought a dragon once. How long ago was that? Seems like I'm always fighting when I'd rather make friends. Welcome new friend.",
                                "(Say nothing)",
                            },

                            answers = new()
                            {
                                "I'm surprised that you would consider me for a partnership, but I will accept, gratefully. " +
                                "I have my own reasons for studying the forgotten war between dragons and elderborn, and if I get the opportunity to pursue my research, then I will. " +
                                "I also expect I will have to face my former masters at some point. Until then, my allegiance is yours. " +
                                "As a token of my goodwill, I entrust to you the old text I spoke of, and the compendium of my own research. " +
                                "The text also made mention of the circle of druids, and are part of the reason I wanted to capture one of your number for questioning.",
                            },

                            questContext = 148,

                        },

                    };

                    break;

                case QuestHandle.questShadowtin:

                    conversations = new()
                    {
                        
                        [1] = new()
                        {
                            
                            intro = "Shadowtin Bear: It spoke through a translation device, but I could still hear the unmistakenable tone of disgust. " +
                                "I suppose it has it's own reasons for distrusting folk like me. We're notorious for being marauders.",

                            responses = new()
                            {
                                "I've read details about a tenuous peace between stellarfolk and shadowfolk after a brutal conflict.",
                                "Where did you get that milk? It didn't have the "+ Game1.player.farmName.Value +" farm label on it."
                            },

                            answers = new()
                            {
                                "I procured the liquid from a grazing creature, coralled by that local animal handler. " +
                                "The teats did not welcome the unnatural chill of my shadowy hands. " +
                                "A necessary expense for the success of our endeavour. This device should afford us access to the ether vault."

                            },

                            questContext = 99,

                        },

                        [2] = new()
                        {

                            intro = "Shadowtin Bear: It's heartening when a clever plan comes to fruition. I know that the circumstances and methods that have led us here are not particularly honourable. But the results speak for themselves.",

                            responses = new()
                            {
                                "I'm concerned that a large magical device of unknown origin is operating under our peaceful valley.",
                                "I think that chunky glow box is a bit too big for you to steal, Shadowtin."
                            },

                            answers = new()
                            {
                                "That's an engine for ethereal transmutation. " +
                                "Considering the conduits and the transmission panel, I suspect this is providing magical current to a network of some kind. " +
                                "The nature of the technology could be attributed to shadowfolk innovation, but the composition and engineering is more akin to stellarfolk. Could it have been a collaborative effort?",
                            },

                            questContext = 199,

                        },

                        [3] = new()
                        {

                            companion = 3,

                            intro = "The Wizard: Are you aware you've trespassed onto the property of the college of wizardry?",

                            responses = new()
                            {
                                "Pardon my intrusion, sir. We are on a research venture to study ether currents and stumbled on this place.",
                                "Nonsense, my scholar friend has correctly identified the architecture and fixtures of this chamber far predate the college, which makes the circle the true custodians. YOU are the trespasser here.",
                            },

                            answers = new()
                            {
                                "I anticipated this encounter, when my own interests in the magical heritage of the valley would begin to coincide or conflict with yours. " +
                                "I welcome the re-establishment of the Circle of Druids, but your advancement has been quick by magical standards, " +
                                "and it has yet to be proven to me that you have the capacity to handle the dangers and mysteries that lie before you. " +
                                "Before I retransfigure your friend here, you'll need to face me in a contest of powers, and we'll know for sure if there are any merits to your 'rites'.",
                            },

                            questContext = 299,

                        },

                        [4] = new()
                        {

                            companion = 3,

                            intro = "The Wizard: The arcane. It seems to be merely a dim reflection of the mythic powers you have learnt to wield, despite your youth and lack of tutelage.",

                            responses = new()
                            {
                                "Perhaps you and your peers were wrong to dismiss the patrons of the Druids as bygone myths.",
                                "I do not take pleasure in administering harsh lessons, my pupil, but your education was lacking.",
                            },

                            answers = new()
                            {
                                "I recognise that your Circle of Druids are a boon to the valley, not just for your sacred spaces, but for the elemental spirits that I seek to guide and protect." +
                                "As your co-custodian, I do have some information about this antiquated machine. It is the Shrine Engine. It powers the warp statues and other such artifacts in the valley. " +
                                "Coincidentally, I recently came into possession of what I believe are the machine's specifications, in the tome your friend attempted to pilfer. " +
                                "It was in the stump of a tree, the recent victim of celestial power, I believe, within the clearings south-east of the forest. " +
                                "The tome is cryptic. Without a practical cipher, I have only managed to discern the identity of the author from a scribble that defaces the first page. 'Mother of Crows'. "
                            },
                            //"It's purpose is obvious, but of it's mechanisms and origin, I know very little, despite
                            //I believe other such artifacts may be sealed in the vicinity, but unfortunately, " +
                            //"I do not have the means to locate or excavate them, my attempts to source a codex to decipher the specifications have proven futile
                            //I can only marvel at the intricate illustrations like a child with a picture-book. 
                            questContext = 399,

                        },

                        [5] = new()
                        {

                            intro = "Shadowtin Bear: I imagine I will have to endure many more hardships on my journey to expose Lord Deep. The petty ego of that recluse is of no consequence to me.",

                            responses = new()
                            {
                                "The Wizard's words are worth considering, Shadowtin. Should you forsake your own shadowmanity to redeem that of your people?",
                                "It took me a while to realise that you didn't transform into a cat on purpose. You were a very convincing kitty.",
                            },

                            answers = new()
                            {
                                "Tell me this, human. What is the worth of another's comfort in comparison to the depravity of my existence, of the languishing of my people. " +
                                "So what if a cow got frosty teats, or we trespassed into a fabulous magician's sanctum. " +
                                "I know it's wrong, but I can't afford to care, especially when no one shows a damn bit of care to us. " +
                                "The Fates don't answer our prayers or pleas. The Lady doesn't smite my enemies. " +
                                "(Shadowtin calms as he looks to the exit) There may be answers hidden out in the cliffs south east of here, if my enhanced feline senses heard the wizard correctly. " +
                                "As a show of good faith, I'll give you the access key, and you can be certain I won't return for a bit of 'shadowthievery'.",
                            },

                            questContext = 499,

                        },

                        [6] = new()
                        {

                            intro = "Shadowtin Bear: It seems to be a cautionary warning, written in one of the older human tongues, from a Druid, no less.",

                            responses = new()
                            {
                                
                                "That's great news, the more I learn of the Circle's past, the better I can understand our current situation.",
                                "Good to know the old circle of Druids was real and Effigy didn't make up the whole thing.",
                            
                            },

                            answers = new()
                            {
                                
                                "(Shadowtin narrates) 'Here lies the profanity of Gelatin, brother of Cannoli, founding member of the Circle. " +
                                "He succumbed to the temptation afforded by the ravenous Hound, and feasted on the aspects of forest spirits, stewed and baked within succulent pastry. " +
                                "Of the artifice of Dragons was this visage of horror forged, and yet it resists the dragon's flame, and only Chaos knows the methods to unmake it. " +
                                "Therefore, discoverer, return this profanity to rest, lay heaps over it, and bind it in ethereal ties, to be forgotten again, forever. " +
                                "Beware the Rite of Bones. Wyrven, Knight Druid.' (Narration ends) " +
                                "I guess we should put it back then. There's nothing here that furthers either of our causes. (Shadowtin pauses) " +
                                "I'm almost inclined to bring that lucky cat with us if it would increase our chances of fortune.",


                            },

                            questContext = 599,

                        },

                        [7] = new()
                        {

                            intro = "Shadowtin Bear: (Sighs) Well my reputation as a fearsome mercenary captain is officially ruined.",

                            responses = new()
                            {

                                "We reap what we sow, as my grandfather might have said. You were the captain of knaves, but now you are the scholar of the circle, and reap the rewards of dignity and new purpose.",
                                "I thought the only reason you were their captain was because you were the only one of them who could read maps. So...",

                            },

                            answers = new()
                            {

                                "I am by nature a shadow thief, but when I beheld an enshrined relic of the stellarfolk in the halls of a besieged stronghold, I lost all passion for plunder. " +
                                "The similarities in the dwarven craftsmanship to our our own Great Shadow Vessel were unmistakeable. It was a Stellar Vessel. " +
                                "The dwarves recieved the same blessings as we did, not from Lord Deep, but a greater power, who refuses to reveal themselves to us. " +
                                "If it was Yoba, then why did he leave us to tyranny? How is that an example that would inspire my people to honour and glory? " +
                                "I can only look to you to know what is right, farmer, and maybe I'll know the truth of it all one day. Now... I should probably go pay for that milk I stole."
                                
                            },

                            questContext = 699,

                        },

                    };

                    break;

            }

            return conversations;

        }

    }

}
