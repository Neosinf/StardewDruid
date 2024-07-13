using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
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
            questComplete,
            percentComplete,

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

            quests,
            effects,
            relics,
            herbalism,

            active,
            reverse,
            refresh,

            innerOne,
            innerTwo,

            close,

            back,
            start,

            scrollUp,
            scrollBar,
            scrollDown,
            forward,
            end,

        }

        public static string ButtonStrings(DruidJournal.journalButtons button)
        {

            switch (button)
            {
                case DruidJournal.journalButtons.back:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.back");

                case DruidJournal.journalButtons.start:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.start");

                case DruidJournal.journalButtons.forward:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.forward");

                case DruidJournal.journalButtons.end:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.end");

                case DruidJournal.journalButtons.exit:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.exit");

                case DruidJournal.journalButtons.quests:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.quests");

                case DruidJournal.journalButtons.effects:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.effects");

                case DruidJournal.journalButtons.relics:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.relics");

                case DruidJournal.journalButtons.herbalism:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.herbalism");

                case DruidJournal.journalButtons.active:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.active");

                case DruidJournal.journalButtons.reverse:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.reverse");

                case DruidJournal.journalButtons.refresh:

                    return Mod.instance.Helper.Translation.Get("Enum.journalButtons.refresh");

                default:

                    return null;
            }

        }

        // ok , i switch back to full version
        public static string Strings(stringkeys key)
        {

            switch (key)
            {

                // Mod, Rite, ModUtility, QuestHandle

                case stringkeys.receivedData:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.receivedData");

                case stringkeys.challengeAborted:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.challengeAborted");

                case stringkeys.riteBuffDescription:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.riteBuffDescription");

                case stringkeys.dragonBuff:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.dragonBuff");

                case stringkeys.dragonBuffDescription:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.dragonBuffDescription");

                case stringkeys.energySkill:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.energySkill");

                case stringkeys.openJournal:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.openJournal");

                case stringkeys.noRiteAttuned:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.noRiteAttuned");

                case stringkeys.riteTool:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.riteTool");

                case stringkeys.noToolAttunement:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.noToolAttunement");

                case stringkeys.nothingHappened:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.nothingHappened");

                case stringkeys.invalidLocation:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.invalidLocation");

                case stringkeys.energyContinue:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.energyContinue");

                case stringkeys.energyRite:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.energyRite");

                case stringkeys.stamina:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.stamina");

                case stringkeys.druidFreneticism:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.druidFreneticism");

                case stringkeys.speedIncrease:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.speedIncrease");

                case stringkeys.questComplete:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.questComplete");

                case stringkeys.percentComplete:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.percentComplete");

                // ============================================ JOURNAL

                case stringkeys.stardewDruid: 

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.stardewDruid");

                case stringkeys.grimoire:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.grimoire");

                case stringkeys.reliquary:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.reliquary");

                case stringkeys.dragonomicon:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.dragonomicon");

                case stringkeys.apothecary:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.apothecary");

                case stringkeys.startPage: 
                    
                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.startPage");

                case stringkeys.endPage: 
                    
                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.endPage");

                case stringkeys.sortCompletion: 
                    
                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.sortCompletion");

                case stringkeys.reverseOrder: 
                    
                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.reverseOrder");

                case stringkeys.openQuests: 
                    
                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.openQuests");

                case stringkeys.openEffects: 
                    
                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.openEffects");

                case stringkeys.openRelics: 
                    
                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.openRelics");

                case stringkeys.openPotions: 
                    
                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.openPotions");

                case stringkeys.checkHerbalism:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.checkHerbalism");

                case stringkeys.hostOnly:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.hostOnly");

                case stringkeys.questReplay:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.questReplay");

                case stringkeys.outOf:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.outOf");

                case stringkeys.reward:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.reward");

                case stringkeys.bounty:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.bounty");

                case stringkeys.transcript:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.transcript");

                case stringkeys.acEnabled:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.acEnabled");

                case stringkeys.acDisabled:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.acDisabled");

                case stringkeys.acPriority:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.acPriority");

                case stringkeys.MAX:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.MAX");

                case stringkeys.HP:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.HP");

                case stringkeys.STM:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.STM");

                case stringkeys.relicNotFound:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.relicNotFound");

                case stringkeys.relicUnknown:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.relicUnknown"); 

                case stringkeys.primaryColour:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.primaryColour");

                case stringkeys.secondaryColour:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.secondaryColour");

                case stringkeys.tertiaryColour:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.tertiaryColour");

                case stringkeys.dragonScheme:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.dragonScheme");

                case stringkeys.breathScheme:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.breathScheme");

                case stringkeys.dragonRotate:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.dragonRotate");

                case stringkeys.dragonScale:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.dragonScale");

                case stringkeys.dragonAccent:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.dragonAccent");

                case stringkeys.dragonEye:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.dragonEye");

                case stringkeys.dragonReset:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.dragonReset");

                case stringkeys.dragonSave:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.dragonSave");

                case stringkeys.skipQuest:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.skipQuest");

                case stringkeys.replayQuest:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.replayQuest");

                case stringkeys.replayTomorrow:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.replayTomorrow");

                case stringkeys.viewEffect:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.viewEffect");

                case stringkeys.cancelReplay:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.cancelReplay");

                case stringkeys.replayReward:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.replayReward");

                case stringkeys.massHerbalism:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.massHerbalism");

                // Miscellaneous / Events

                case stringkeys.trashCollected:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.trashCollected");

                case stringkeys.bomberInterruptions:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.bomberInterruptions");

                case stringkeys.slimesDestroyed:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.slimesDestroyed");

                case stringkeys.learnRecipes:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.learnRecipes");

                case stringkeys.theDusting:

                    // Title for Ether/Gate challenge
                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.theDusting");

                case stringkeys.abortTomorrow:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.abortTomorrow");

                case stringkeys.noJunimo:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.noJunimo");

                case stringkeys.noInstructions:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.noInstructions");

                case stringkeys.leftEvent:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.leftEvent");

                case stringkeys.leavingEvent:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.leavingEvent");

                case stringkeys.ladderAppeared:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.ladderAppeared");

                case stringkeys.returnLater:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.returnLater");

                case stringkeys.reachEnd:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.reachEnd");

                case stringkeys.treasureHunt:

                    return Mod.instance.Helper.Translation.Get("Enum.stringkeys.treasureHunt");

            }

            // ?
            return Mod.instance.Helper.Translation.Get("nevermind");

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
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueScene.swordStars.8.0"), },

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

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.approachEffigy.1.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachEffigy.1.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachEffigy.1.responses.1"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachEffigy.1.responses.2"),
                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.approachEffigy.1.answers.0")

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.approachEffigy.2.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachEffigy.2.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachEffigy.2.responses.1"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachEffigy.2.responses.2"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachEffigy.2.answers.0"),
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

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordWeald.1.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordWeald.1.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordWeald.1.responses.1"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordWeald.2.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordWeald.2.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordWeald.2.responses.1"),

                            },

                            questContext = 200,

                        },                        
                        
                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordWeald.3.intro"),

                            responses = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.swordWeald.3.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordWeald.3.responses.1"),

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

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordMists.1.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordMists.1.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordMists.1.responses.1"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordMists.2.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordMists.2.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordMists.2.responses.1"),

                            },

                            questContext = 200,

                        },                        
                        
                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordMists.3.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordMists.3.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordMists.3.responses.1"),

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

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.1.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.1.responses.0")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.1.answers.0")

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.2.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.2.responses.0"),

                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.2.answers.0")
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.3.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.3.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.3.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.3.answers.0")
                            },

                            questContext = 250,

                        },

                        [4] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.4.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.4.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.4.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.4.answers.0")
                            },

                            questContext = 400,

                        },

                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.5.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.5.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.5.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.5.answers.0"),
                            },

                            questContext = 500,

                        },

                        [6] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.6.intro"),

                            responses = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.6.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.6.responses.1"),

                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.questEffigy.6.answers.0"),

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

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.1.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.1.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.1.responses.1"),
                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.1.answers.0"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.2.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.2.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.2.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.2.answers.0"),
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.3.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.3.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.3.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordStars.3.answers.0"),

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

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.1.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.1.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.1.responses.1"),
                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.1.answers.0"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.2.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.2.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.2.responses.1"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.2.responses.2"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.2.answers.0"),
                            },

                            questContext = 200,

                        },
                        
                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.3.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.3.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.3.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.3.answers.0"),

                            },

                            questContext = 300,

                        },

                    };

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.relicWeald))
                    {

                        conversations[3].responses.Add(Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.3.responses.2"));

                        string buffer = conversations[3].answers.First();

                        conversations[3].answers.Add(buffer);

                        conversations[3].answers.Add(Mod.instance.Helper.Translation.Get("SceneConversations.approachJester.3.answers.1"));

                    }

                    break;

                case QuestHandle.swordFates:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.1.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.1.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.1.responses.1"),
                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.1.answers.0"),

                            },

                            questContext = 130,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.2.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.2.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.2.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.2.answers.0"),
                            },

                            questContext = 140,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.3.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.3.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.3.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.swordFates.3.answers.0"),

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

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questJester.1.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.1.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.1.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.1.answers.0")

                            },

                            questContext = 99,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questJester.2.intro"),
                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.2.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.2.responses.1"),

                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.2.responses.0")
                            },

                            questContext = 199,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questJester.3.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.3.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.3.responses.1"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.3.responses.2"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.3.answers.0")
                            },

                            questContext = 299,

                        },

                        [4] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questJester.4.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.4.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.4.responses.1"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.4.responses.2"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.4.answers.0")
                            },

                            questContext = 799,

                        },

                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questJester.5.intro"),
                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.5.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.5.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.5.answers.0")
                            },

                            questContext = 499,

                        },

                        [6] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questJester.6.intro"),

                            responses = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.6.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.6.responses.1"),

                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.questJester.6.answers.0"),

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

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.challengeFates.1.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.challengeFates.1.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.challengeFates.1.responses.1"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.challengeFates.1.responses.2"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.challengeFates.1.answers.0"),

                            },

                            questContext = 144,

                        },

                        [2] = new()
                        {
                            companion = 3,

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.challengeFates.2.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.challengeFates.2.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.challengeFates.2.responses.1"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.challengeFates.2.responses.2"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.challengeFates.2.answers.0"),
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

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.1.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.1.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.1.responses.1").Tokens(new {farmName=Game1.player.farmName.Value })
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.1.answers.0")

                            },

                            questContext = 99,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.2.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.2.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.2.responses.1")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.2.answers.0"),
                            },

                            questContext = 199,

                        },

                        [3] = new()
                        {

                            companion = 3,

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.3.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.3.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.3.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.3.answers.0"),
                            },

                            questContext = 299,

                        },

                        [4] = new()
                        {

                            companion = 3,

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.4.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.4.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.4.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.4.answers.0")
                            },
                            //"It's purpose is obvious, but of it's mechanisms and origin, I know very little, despite
                            //I believe other such artifacts may be sealed in the vicinity, but unfortunately, " +
                            //"I do not have the means to locate or excavate them, my attempts to source a codex to decipher the specifications have proven futile
                            //I can only marvel at the intricate illustrations like a child with a picture-book. 
                            questContext = 399,

                        },

                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.5.intro"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.5.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.5.responses.1"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.5.answers.0"),
                            },

                            questContext = 499,

                        },

                        [6] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.6.intro"),

                            responses = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.6.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.6.responses.1"),

                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.6.answers.0"),


                            },

                            questContext = 599,

                        },

                        [7] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.7.intro"),

                            responses = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.7.responses.0"),
                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.7.responses.1"),

                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("SceneConversations.questShadowtin.7.answers.0")

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
