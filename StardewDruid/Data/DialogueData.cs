﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
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
            switch (rite)
            {
                default:
                case rites.weald: return Mod.instance.Helper.Translation.Get("DialogueData.38");
                case rites.mists: return Mod.instance.Helper.Translation.Get("DialogueData.39");
                case rites.stars: return Mod.instance.Helper.Translation.Get("DialogueData.40");
                case rites.fates: return Mod.instance.Helper.Translation.Get("DialogueData.41");
                case rites.ether: return Mod.instance.Helper.Translation.Get("DialogueData.42");
                case rites.bones: return Mod.instance.Helper.Translation.Get("DialogueData.312.2");
            }

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
            defaultToolAttunement,
            nothingHappened,
            invalidLocation,
            energyContinue,
            energyRite,
            riteBuffDescription,
            normalAttunementActive,
            slotAttunementActive,
            energySkill,
            dragonBuff,
            dragonBuffDescription,
            druidFreneticism,
            speedIncrease,
            questComplete,
            percentComplete,
            druidShield,
            defenseIncrease,

            jesterBuff,
            jesterBuffTitle,
            jesterBuffDescription,

            // Journal

            grimoire,
            reliquary,
            dragonomicon,
            apothecary,
            chronicle,

            hostOnly,
            questReplay,
            outOf,
            lessonSkipped,
            mastered,
            reward,
            replayReward,
            bounty,
            transcript,

            acEnabled,
            acDisabled,
            acPriority,
            acIgnored,
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
            dragonSize,

            questLore,
            questTranscript,

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

            restoreStart,
            restorePartial,
            restoreFully,
            restoreTomorrow,

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

            returnedHome,
            joinedPlayer,
            noWarpPoint
        }

        public static string ButtonStrings(DruidJournal.journalButtons button)
        {

            switch (button)
            {
                case DruidJournal.journalButtons.back:

                    return Mod.instance.Helper.Translation.Get("DialogueData.177");

                case DruidJournal.journalButtons.start:

                    return Mod.instance.Helper.Translation.Get("DialogueData.181");

                case DruidJournal.journalButtons.forward:

                    return Mod.instance.Helper.Translation.Get("DialogueData.185");

                case DruidJournal.journalButtons.end:

                    return Mod.instance.Helper.Translation.Get("DialogueData.189");

                case DruidJournal.journalButtons.exit:

                    return Mod.instance.Helper.Translation.Get("DialogueData.193");

                case DruidJournal.journalButtons.quests:

                    return Mod.instance.Helper.Translation.Get("DialogueData.197");

                case DruidJournal.journalButtons.effects:

                    return Mod.instance.Helper.Translation.Get("DialogueData.201");

                case DruidJournal.journalButtons.relics:

                    return Mod.instance.Helper.Translation.Get("DialogueData.205");

                case DruidJournal.journalButtons.herbalism:

                    return Mod.instance.Helper.Translation.Get("DialogueData.209");

                case DruidJournal.journalButtons.lore:

                    return Mod.instance.Helper.Translation.Get("DialogueData.318.1");

                case DruidJournal.journalButtons.transform:

                    return Mod.instance.Helper.Translation.Get("DialogueData.318.2");

                case DruidJournal.journalButtons.active:

                    return Mod.instance.Helper.Translation.Get("DialogueData.213");

                case DruidJournal.journalButtons.reverse:

                    return Mod.instance.Helper.Translation.Get("DialogueData.217");

                case DruidJournal.journalButtons.refresh:

                    return Mod.instance.Helper.Translation.Get("DialogueData.221");

                case DruidJournal.journalButtons.skipQuest:

                    return Mod.instance.Helper.Translation.Get("DialogueData.225");

                case DruidJournal.journalButtons.replayQuest:

                    return Mod.instance.Helper.Translation.Get("DialogueData.229");

                case DruidJournal.journalButtons.replayTomorrow:

                    return Mod.instance.Helper.Translation.Get("DialogueData.233");

                case DruidJournal.journalButtons.cancelReplay:

                    return Mod.instance.Helper.Translation.Get("DialogueData.237");

                case DruidJournal.journalButtons.viewEffect:

                    return Mod.instance.Helper.Translation.Get("DialogueData.241");

                case DruidJournal.journalButtons.viewQuest:

                    return Mod.instance.Helper.Translation.Get("DialogueData.245");

                case DruidJournal.journalButtons.dragonReset:

                    return Mod.instance.Helper.Translation.Get("DialogueData.316.1");

                case DruidJournal.journalButtons.dragonSave:

                    return Mod.instance.Helper.Translation.Get("DialogueData.316.2");

                default:

                    return null;
            }

        }


        public static string Strings(stringkeys key)
        {

            switch (key)
            {

                // Mod, Rite, ModUtility, QuestHandle

                case stringkeys.receivedData:

                    return Mod.instance.Helper.Translation.Get("DialogueData.265");

                case stringkeys.challengeAborted:

                    return Mod.instance.Helper.Translation.Get("DialogueData.269");

                case stringkeys.riteBuffDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.273");

                case stringkeys.normalAttunementActive:

                    return Mod.instance.Helper.Translation.Get("DialogueData.336.1");

                case stringkeys.slotAttunementActive:

                    return Mod.instance.Helper.Translation.Get("DialogueData.336.2");

                case stringkeys.dragonBuff:

                    return Mod.instance.Helper.Translation.Get("DialogueData.277");

                case stringkeys.dragonBuffDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.281");

                case stringkeys.energySkill:

                    return Mod.instance.Helper.Translation.Get("DialogueData.285");

                case stringkeys.openJournal:

                    return Mod.instance.Helper.Translation.Get("DialogueData.289");

                case stringkeys.noRiteAttuned:

                    return Mod.instance.Helper.Translation.Get("DialogueData.293");

                case stringkeys.riteTool:

                    return Mod.instance.Helper.Translation.Get("DialogueData.297");

                case stringkeys.noToolAttunement:

                    return Mod.instance.Helper.Translation.Get("DialogueData.301");

                case stringkeys.defaultToolAttunement:

                    return Mod.instance.Helper.Translation.Get("DialogueData.330.2").Tokens(new { rite = RiteNames(Mod.instance.save.rite), });

                case stringkeys.nothingHappened:

                    return Mod.instance.Helper.Translation.Get("DialogueData.305");

                case stringkeys.invalidLocation:

                    return Mod.instance.Helper.Translation.Get("DialogueData.309");

                case stringkeys.energyContinue:

                    return Mod.instance.Helper.Translation.Get("DialogueData.313");

                case stringkeys.energyRite:

                    return Mod.instance.Helper.Translation.Get("DialogueData.317");

                case stringkeys.stamina:

                    return Mod.instance.Helper.Translation.Get("DialogueData.321");

                case stringkeys.druidFreneticism:

                    return Mod.instance.Helper.Translation.Get("DialogueData.325");

                case stringkeys.speedIncrease:

                    return Mod.instance.Helper.Translation.Get("DialogueData.329");

                case stringkeys.questComplete:

                    return Mod.instance.Helper.Translation.Get("DialogueData.333");

                case stringkeys.percentComplete:

                    return Mod.instance.Helper.Translation.Get("DialogueData.337");

                case stringkeys.druidShield:

                    return Mod.instance.Helper.Translation.Get("DialogueData.335.1");

                case stringkeys.defenseIncrease:

                    return Mod.instance.Helper.Translation.Get("DialogueData.335.2");

                case stringkeys.jesterBuff:

                    return Mod.instance.Helper.Translation.Get("DialogueData.338.1");

                case stringkeys.jesterBuffDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.338.2");

                case stringkeys.jesterBuffTitle:

                    return Mod.instance.Helper.Translation.Get("DialogueData.338.3");


                // ============================================ JOURNAL

                case stringkeys.stardewDruid:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343");

                case stringkeys.grimoire:

                    return Mod.instance.Helper.Translation.Get("DialogueData.347");

                case stringkeys.reliquary:

                    return Mod.instance.Helper.Translation.Get("DialogueData.351");

                case stringkeys.dragonomicon:

                    return Mod.instance.Helper.Translation.Get("DialogueData.355");

                case stringkeys.apothecary:

                    return Mod.instance.Helper.Translation.Get("DialogueData.359");

                case stringkeys.chronicle:

                    return Mod.instance.Helper.Translation.Get("DialogueData.318.3");

                case stringkeys.hostOnly:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399");

                case stringkeys.questReplay:

                    return Mod.instance.Helper.Translation.Get("DialogueData.403");

                case stringkeys.outOf:

                    return Mod.instance.Helper.Translation.Get("DialogueData.407");

                case stringkeys.reward:

                    return Mod.instance.Helper.Translation.Get("DialogueData.411");

                case stringkeys.replayReward:

                    return Mod.instance.Helper.Translation.Get("DialogueData.519");

                case stringkeys.bounty:

                    return Mod.instance.Helper.Translation.Get("DialogueData.415");

                case stringkeys.transcript:

                    return Mod.instance.Helper.Translation.Get("DialogueData.419");

                case stringkeys.acEnabled:

                    return Mod.instance.Helper.Translation.Get("DialogueData.423");

                case stringkeys.acDisabled:

                    return Mod.instance.Helper.Translation.Get("DialogueData.427");

                case stringkeys.acPriority:

                    return Mod.instance.Helper.Translation.Get("DialogueData.431");

                case stringkeys.acIgnored:

                    return Mod.instance.Helper.Translation.Get("DialogueData.340.1");

                case stringkeys.MAX:

                    return Mod.instance.Helper.Translation.Get("DialogueData.435");

                case stringkeys.HP:

                    return Mod.instance.Helper.Translation.Get("DialogueData.439");

                case stringkeys.STM:

                    return Mod.instance.Helper.Translation.Get("DialogueData.443");

                case stringkeys.relicNotFound:

                    return Mod.instance.Helper.Translation.Get("DialogueData.447");

                case stringkeys.relicUnknown:

                    return Mod.instance.Helper.Translation.Get("DialogueData.451");

                case stringkeys.lessonSkipped:

                    return Mod.instance.Helper.Translation.Get("DialogueData.319.1");

                case stringkeys.mastered:

                    return Mod.instance.Helper.Translation.Get("DialogueData.319.2");

                // Dragon menu

                case stringkeys.primaryColour:

                    return Mod.instance.Helper.Translation.Get("DialogueData.455");

                case stringkeys.secondaryColour:

                    return Mod.instance.Helper.Translation.Get("DialogueData.459");

                case stringkeys.tertiaryColour:

                    return Mod.instance.Helper.Translation.Get("DialogueData.463");

                case stringkeys.dragonScheme:

                    return Mod.instance.Helper.Translation.Get("DialogueData.467");

                case stringkeys.breathScheme:

                    return Mod.instance.Helper.Translation.Get("DialogueData.471");

                case stringkeys.dragonRotate:

                    return Mod.instance.Helper.Translation.Get("DialogueData.475");

                case stringkeys.dragonScale:

                    return Mod.instance.Helper.Translation.Get("DialogueData.479");

                case stringkeys.dragonAccent:

                    return Mod.instance.Helper.Translation.Get("DialogueData.483");

                case stringkeys.dragonEye:

                    return Mod.instance.Helper.Translation.Get("DialogueData.487");

                case stringkeys.dragonSize:

                    return Mod.instance.Helper.Translation.Get("DialogueData.316.3");

                case stringkeys.questLore:

                    return Mod.instance.Helper.Translation.Get("DialogueData.318.4");

                case stringkeys.questTranscript:

                    return Mod.instance.Helper.Translation.Get("DialogueData.318.5");

                // Miscellaneous / Events

                case stringkeys.trashCollected:

                    return Mod.instance.Helper.Translation.Get("DialogueData.529");

                case stringkeys.bomberInterruptions:

                    return Mod.instance.Helper.Translation.Get("DialogueData.533");

                case stringkeys.slimesDestroyed:

                    return Mod.instance.Helper.Translation.Get("DialogueData.537");

                case stringkeys.learnRecipes:

                    return Mod.instance.Helper.Translation.Get("DialogueData.541");

                case stringkeys.restoreStart:

                    return Mod.instance.Helper.Translation.Get("DialogueData.329.1");

                case stringkeys.restorePartial:

                    return Mod.instance.Helper.Translation.Get("DialogueData.329.2");

                case stringkeys.restoreFully:

                    return Mod.instance.Helper.Translation.Get("DialogueData.329.3");

                case stringkeys.restoreTomorrow:

                    return Mod.instance.Helper.Translation.Get("DialogueData.330.1");

                // Title for Ether/Gate challenge
                case stringkeys.theDusting:

                    return Mod.instance.Helper.Translation.Get("DialogueData.546");

                case stringkeys.abortTomorrow:

                    return Mod.instance.Helper.Translation.Get("DialogueData.550");

                case stringkeys.noJunimo:

                    return Mod.instance.Helper.Translation.Get("DialogueData.554");

                case stringkeys.noInstructions:

                    return Mod.instance.Helper.Translation.Get("DialogueData.558");

                case stringkeys.leftEvent:

                    return Mod.instance.Helper.Translation.Get("DialogueData.562");

                case stringkeys.leavingEvent:

                    return Mod.instance.Helper.Translation.Get("DialogueData.566");

                case stringkeys.ladderAppeared:

                    return Mod.instance.Helper.Translation.Get("DialogueData.570");

                case stringkeys.returnLater:

                    return Mod.instance.Helper.Translation.Get("DialogueData.574");

                case stringkeys.reachEnd:

                    return Mod.instance.Helper.Translation.Get("DialogueData.578");

                case stringkeys.treasureHunt:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582");

                case stringkeys.returnedHome:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582.1");

                case stringkeys.joinedPlayer:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582.2");

                case stringkeys.noWarpPoint:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582.2");
            }

            return Mod.instance.Helper.Translation.Get("DialogueData.586");

        }

        public static Dictionary<int, string> DialogueNarrators(string scene)
        {
            Dictionary<int, string> sceneNarrators = new();

            switch (scene)
            {

                case QuestHandle.approachEffigy:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.601"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.602"),
                    };

                    break;

                case QuestHandle.swordWeald:
                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.610"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.611"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueData.612"),
                    };
                    break;

                case QuestHandle.challengeWeald:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.619"),
                    };

                    break;

                case QuestHandle.swordMists:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.628"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.629"),
                    };

                    break;

                case QuestHandle.questEffigy:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.638"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.639"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueData.640"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueData.641"),
                    };

                    break;

                case QuestHandle.challengeMists:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.649"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.650"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueData.651"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueData.652"),
                        [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.13"),
                    };

                    break;

                case QuestHandle.swordStars:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.661"),
                    };

                    break;

                case QuestHandle.challengeStars:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.670"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.671"),
                    };

                    break;

                case QuestHandle.challengeAtoll:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.680"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.681"),
                    };

                    break;

                case QuestHandle.challengeDragon:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.690"),
                    };

                    break;

                case QuestHandle.swordFates:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.699"),
                    };

                    break;

                case QuestHandle.questJester:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.708"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.709"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueData.710"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueData.711"),
                        [4] = Mod.instance.Helper.Translation.Get("DialogueData.712"),
                    };

                    break;

                case QuestHandle.challengeFates:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.721"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.722"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueData.723"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueData.724"),
                        [4] = Mod.instance.Helper.Translation.Get("DialogueData.725"),
                        [5] = Mod.instance.Helper.Translation.Get("DialogueData.726"),
                        [6] = Mod.instance.Helper.Translation.Get("DialogueData.727"),
                        [7] = Mod.instance.Helper.Translation.Get("DialogueData.728"),
                    };

                    break;

                case QuestHandle.swordEther:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.737"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.738"),
                    };

                    break;

                case QuestHandle.questShadowtin:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.747"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.748"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueData.749"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueData.750"),
                        [4] = Mod.instance.Helper.Translation.Get("DialogueData.751"),
                        [5] = Mod.instance.Helper.Translation.Get("DialogueData.752"),
                        [6] = Mod.instance.Helper.Translation.Get("DialogueData.753"),
                        [7] = Mod.instance.Helper.Translation.Get("DialogueData.754"),
                        [8] = Mod.instance.Helper.Translation.Get("DialogueData.755"),

                    };

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Nom0ri.RomRas"))
                    {

                        sceneNarrators[5] = Mod.instance.Helper.Translation.Get("DialogueData.339.5");
                        sceneNarrators[6] = Mod.instance.Helper.Translation.Get("DialogueData.339.6");
                    }

                    break;

                case QuestHandle.challengeEther:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.765"),
                    };

                    break;

                case QuestHandle.treasureChase:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.774"),
                    };

                    break;

                case QuestHandle.questBlackfeather:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.1"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.2"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.3"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueData.324.4"),
                        [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.5"),
                        [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.6"),
                        [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.7"),
                        [7] = Mod.instance.Helper.Translation.Get("DialogueData.324.8"),
                    };

                    break;

                case QuestHandle.questBuffin:

                    sceneNarrators = new()
                    {
                        [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.185"),
                        [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.186"),
                        [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.187"),
                        [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.188"),
                        [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.189"),
                        [5] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.190"),
                        [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.191"),
                        [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.192"),
                        [8] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.193"),
                        [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.194"),

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

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.796"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.797"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.798"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.799"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.800"), },
                        [6] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.801"), },

                        [900] = new()
                        {
                            [999] = Mod.instance.Helper.Translation.Get("DialogueData.802").Tokens(new { button = Mod.instance.Config.riteButtons.ToString() }),
                        },
                    };

                    break;

                case QuestHandle.swordWeald:

                    sceneDialogue = new()
                    {

                        [1] = new()
                        {
                            [0] = Mod.instance.Helper.Translation.Get("DialogueData.816"),
                            [1] = Mod.instance.Helper.Translation.Get("DialogueData.817"),
                            [2] = Mod.instance.Helper.Translation.Get("DialogueData.818"),
                        },
                        [2] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.819"), },
                        [3] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.820"), },
                        [4] = new()
                        {
                            [0] = Mod.instance.Helper.Translation.Get("DialogueData.821"),
                            [1] = Mod.instance.Helper.Translation.Get("DialogueData.822"),
                            [2] = Mod.instance.Helper.Translation.Get("DialogueData.823"),
                        },

                    };

                    break;


                case QuestHandle.challengeWeald:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.835"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.836"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.837"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.838"), },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.839"), },
                        [17] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.840"), },
                        [20] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.841"), },
                        [23] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.842"), },

                        [41] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.843"), },
                        [44] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.844"), },
                        [47] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.845"), },

                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.846"), },
                        [63] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.847"), },
                        [69] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.848"), },
                        [72] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.849"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.851"), },
                    };

                    break;

                case QuestHandle.swordMists:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.861"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.862"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.863"), },
                        [4] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.864"), },

                    };

                    break;

                case QuestHandle.questEffigy:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.875") },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.876") },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.877") },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.878") },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.879") },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.880") },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.881") },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.882") },
                        [9] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.883") },
                        [10] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.884") },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.885") },
                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.886") },
                        [13] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.887") },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.888") },
                        [15] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.889") },

                        [16] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.891") },
                        [17] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.892") },
                        [18] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.893") },
                        [19] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.894") },
                        [20] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.895") },
                        [21] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.896") },
                        [22] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.897") },
                        [23] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.898") },
                        [24] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.899") },

                        [25] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.901") },
                        [26] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.902") },
                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.903") },
                        [28] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.904") },
                        [29] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.905") },
                        [30] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.906") },

                        [31] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.908") },
                        [32] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.909") },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.910") },
                        [34] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.911") },
                        [35] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.912") },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.913") },
                        [37] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.914") },

                        [502] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.916") },
                        [506] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.917") },
                        [508] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.918"), },
                        [511] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.919"), },
                        [514] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.920"), },
                        [517] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.921"), },
                        [520] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.922"), },
                        [523] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.923"), },
                        [526] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.924"), },
                        [529] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.925"), },
                        [532] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.926"), },
                        [535] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.927"), },
                        [538] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.928"), },
                        [541] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.929"), },
                        [544] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.930"), },
                        [547] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.931"), },
                        [550] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.932"), },
                        [553] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.933"), },
                        [556] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.934"), },
                        [559] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.935"), },
                        [561] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.936"), },
                        [564] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.937"), },
                        [567] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.938"), },

                        [777] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.940"), }
                    };

                    break;

                case QuestHandle.challengeMists:

                    sceneDialogue = new()
                    {
                        [2] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.1"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.2"), },
                        [8] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.3"), },
                        [11] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.4"), },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.5"), },
                        [17] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.6"), },
                        [20] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.7"), },
                        [23] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.8"), },

                        [101] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.950"), },
                        [104] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.951"), },
                        [107] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.9"), },
                        [110] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.10"), },

                        [221] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.954"), },
                        [224] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.957"), },
                        [229] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.959"), },
                        [232] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.960"), },
                        [246] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.963"), },
                        [249] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.964"), },
                        [252] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.965"), },
                        [255] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.967"), },

                        [301] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.968"), },
                        [302] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.969"), },
                        [304] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.970"), },
                        [307] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.971"), },
                        [310] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.972"), },
                        [313] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.973"), },
                        [316] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.974"), },
                        [319] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.975"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.977"), },
                        [910] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.311.14"), },
                        // loading
                        [901] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.966"), },
                        [902] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.953"), },
                        [903] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.11"), },
                        // cancelling
                        [904] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.962"), },
                        [905] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.958"), },
                        [906] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.955"), },
                        // firing
                        [907] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.978"), },
                        [908] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.979"), },
                        [909] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.12"), },

                    };

                    break;

                case QuestHandle.swordStars:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.990"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.991"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.992"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.993"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.994"), },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.995"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.996"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.997"), },

                    };

                    break;

                case QuestHandle.challengeStars:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1012"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1013"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1014"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1015"), },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1016"), },

                        [17] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1018"), },
                        [20] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1019"), },

                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1021"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1022"), },

                        [36] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1008"), },
                        [39] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1010"), },

                        [48] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1024"), },
                        [51] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1025"), },
                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1026"), },

                        [57] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1028"), },

                        [65] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1030"), },
                        [68] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1031"), },
                        [71] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1032"), },

                        [74] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1034"), },

                    };

                    break;

                case QuestHandle.challengeAtoll:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1045"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1046"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1047"), },
                        // cannons
                        [16] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1049"), },
                        [19] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1050"), },
                        [22] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1051"), },
                        // cannons
                        [25] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1053"), },
                        [34] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1054"), },
                        [37] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1055"), },
                        // cannons
                        [49] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1057"), },
                        [52] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1058"), },
                        [55] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1059"), },
                        // cannons
                        [64] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1061"), },
                        [67] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1062"), },
                        [70] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1063"), },
                        [73] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1064"), },
                        [76] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1065"), },
                        [79] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1066"), },

                        [990] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1068"), },
                        [991] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1069"), },
                        [992] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1070"), },
                        [993] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1071"), },
                        [994] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1072"), },
                        [995] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1073"), },


                    };

                    break;

                case QuestHandle.challengeDragon:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1085"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1086"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1087").Tokens(new { rite = Mod.instance.save.rite.ToString() }) },

                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1089"), },
                        [15] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1090"), },
                        [18] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1091"), },

                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1093"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1094"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1095"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1096"), },

                        [42] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1098"), },
                        [45] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1099"), },
                        [48] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1100"), },

                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1102"), },
                        [57] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1103"), },
                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1104"), },

                        [81] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1106"), },
                        [84] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1107"), },
                        [87] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1108"), },
                        [90] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1109"), },
                        [93] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1110"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1112"), },

                    };

                    break;

                case QuestHandle.swordFates:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1123"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1124"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1125"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1126"), },

                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1128"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1129"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1130"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1131"), },

                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1133"), },
                        [57] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1134"), },
                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1135"), },

                        [81] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1137"), },
                        [84] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1138"), },

                        [91] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1140"), },
                        [94] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1141"), },
                        [97] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1142"), },
                        [121] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1143"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1145"), },
                        [901] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.312.1"), },
                    };

                    break;

                case QuestHandle.questJester:

                    sceneDialogue = new()
                    {


                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1157") },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1158") },
                        [3] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1159") },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1160") },
                        [5] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1161") },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1162") },
                        [7] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1163") },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1164") },
                        [9] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1165") },
                        [10] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1166") },
                        [11] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1167") },
                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1168") },
                        [13] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1169") },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1170") },
                        [15] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1171") },
                        [16] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1172") },
                        [17] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1173") },
                        [18] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1174") },
                        [19] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1175") },
                        [20] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1176") },
                        [21] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1177") },
                        [22] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1178") },
                        [23] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1179") },
                        [24] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1180") },

                        [25] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1182") },
                        [26] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1183") },
                        [27] = new()
                        {
                            [0] = Mod.instance.Helper.Translation.Get("DialogueData.1184"),
                            [1] = Mod.instance.Helper.Translation.Get("DialogueData.1185")
                        },
                        [28] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1186") },
                        [29] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1187") },
                        [30] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1188") },
                        [31] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1189") },
                        [32] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1190") },
                        [33] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1191") },
                        [34] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1192") },
                        [35] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1193") },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1194") },
                        [37] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1195") },
                        [38] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1196") },
                        [39] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1197") },
                        [40] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1198") },
                        [41] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1199") },
                        [42] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1200") },
                        [43] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1201") },
                        [44] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1202") },

                        [45] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1204") },
                        [46] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1205") },
                        [47] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1206") },
                        [48] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1207") },
                        [49] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1208") },
                        [50] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1209") },
                        [51] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1210") },
                        [52] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1211") },
                        [53] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1212") },
                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1213") },
                        [55] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1214") },
                        [56] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1215") },
                        [57] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1216") },
                        [58] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1217") },
                        [59] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1218") },
                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1219") },

                        [61] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1221") },
                        [62] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1222") },
                        [63] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1223") },
                        [64] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1224") },
                        [65] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1225") },
                        [66] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1226") },
                        [67] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1227") },
                        [68] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1228") },
                        [69] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1229") },
                        [70] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1230") },
                        [71] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1231") },
                        [72] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1232") },
                        [73] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1233") },
                        [74] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1234") },
                        [75] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1235") },
                        [76] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1236") },
                        [77] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1237") },
                        [78] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1238") },
                        [79] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1239") },
                        [80] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1240") },
                        [81] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1241") },
                        [82] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1242") },
                        [83] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1243") },


                        [901] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1246"), },
                        [903] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1247"), },
                        [904] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1248"), },
                        [906] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1249"), },
                        [907] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1250"), },
                        [910] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1251"), },
                        [912] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1252"), },
                        [913] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1253"), },
                        [915] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1254"), },
                        [916] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1255"), },
                        [919] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1256"), },
                        [922] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1257"), },
                        [923] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1258"), },
                        [925] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1259"), },
                        [928] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1260"), },
                        [931] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1261"), },
                        [934] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1262"), },
                        [937] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1263"), },
                        [940] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1264"), },
                        [943] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1265"), },
                        [946] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1266"), },
                        [949] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1267"), },
                        [952] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1268"), },
                        [955] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1269"), },
                        [958] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1270"), },
                        [961] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1271"), },
                        [963] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1272"), },
                        [965] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1273"), },
                        [968] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1274"), },

                    };

                    break;


                case QuestHandle.challengeFates:

                    sceneDialogue = new()
                    {

                        [2] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1296"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1297"), },
                        [8] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1298"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1299"), },
                        [14] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1300"), },
                        [17] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1301"), },
                        [20] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1302"), },
                        [23] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1303"), },
                        [26] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1304"), },

                        [30] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1306"), },
                        [33] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1307"), },
                        [36] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1308"), },

                        [40] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1310"), },
                        [43] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1311"), },
                        [46] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1312"), },

                        [50] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1314"), },
                        [53] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1315"), },
                        [56] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1316"), },

                        [60] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1318"), },
                        [63] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1319"), },
                        [66] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1320"), },

                        [70] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1322"), },
                        [73] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1323"), },
                        [76] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1324"), },

                        [80] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1326"), },
                        [83] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1327"), },
                        [86] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1328"), },

                        [90] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1330"), },
                        [93] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1331"), },
                        [96] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1332"), },
                        [99] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1333"), },
                        [102] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1334"), },
                        [105] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1335"), },
                        [108] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1336"), },
                        [111] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1337"), },
                        [114] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1338"), },
                        [117] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1339"), },
                        [120] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1340"), },
                        [123] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1341"), },

                        [126] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1343"), },
                        [129] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1344"), },
                        [132] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1345"), },

                    };

                    break;

                case QuestHandle.swordEther:

                    sceneDialogue = new()
                    {
                        [3] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1355"), },
                        [6] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1356"), },
                        [9] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1357"), },
                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1358"), },

                        [15] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1360"), },
                        [18] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1361"), },

                        [21] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1363"), },
                        [24] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1364"), },
                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1365"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1366"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1367"), },

                        [42] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1369"), },
                        [45] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1370"), },
                        [48] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1371"), },
                        [51] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1372"), },

                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1374"), },
                        [63] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1375"), },

                        [75] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1377"), },
                        [78] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1378"), },
                        [81] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1379"), },
                        [84] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1380"), },

                        [93] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1382"), },
                        [96] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1383"), },

                        [991] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1385"), },
                        [992] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1386"), },
                    };

                    break;

                case QuestHandle.questShadowtin:

                    sceneDialogue = new()
                    {
                        // Dwarf interaction
                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1396"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1397"), },
                        [3] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1398"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1399"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1400"), },
                        [6] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1401"), },
                        [7] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1402"), },
                        [8] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1403"), },
                        [9] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1404"), },

                        [100] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1406"), },
                        [101] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1407"), },
                        [102] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1408"), },
                        [103] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1409"), },

                        [200] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1411"), },
                        [201] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1412"), },
                        [202] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1413"), },
                        [203] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1414"), },
                        [204] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1415"), },
                        [205] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1416"), },
                        [206] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1417"), },
                        [207] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1418"), },
                        [208] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1419"), },
                        [209] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1420"), },
                        [210] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1421"), },
                        [211] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1422"), },
                        [212] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1423"), },
                        [213] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1424").Tokens(new { name = Game1.player.Name }) },
                        [214] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1425"), },
                        [215] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1426"), },

                        [300] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1428"), },
                        [301] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1429"), },
                        [302] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1430"), },
                        [303] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1431"), },
                        [304] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1432"), },
                        [305] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1433"), },
                        [306] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1434"), },
                        [307] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1435"), },
                        [308] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1436"), },
                        [309] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1437"), },
                        [310] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1438"), },

                        [400] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1440"), },
                        [401] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1441"), },
                        [402] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1442"), },
                        [403] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1443"), },
                        [404] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1444"), },
                        [405] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1445"), },
                        [406] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1446"), },
                        [407] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1447") },
                        [408] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1448") },
                        [409] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1449"), },
                        [410] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1450"), },
                        [411] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1451"), },

                        [500] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1453"), },
                        [501] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1454"), },
                        [502] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1455"), },
                        [503] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1456"), },
                        [504] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1457"), },
                        [505] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1458"), },

                        [600] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1460"), },
                        [601] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.1461"), },
                        [602] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1462"), },
                        [603] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1463"), },
                        [604] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1464"), },
                        [605] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1465"), },
                        [606] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1466"), },
                        [607] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1467"), },
                        [608] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1468"), },
                        [609] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1469"), },
                        [610] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.1470"), },
                        [611] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1471"), },
                        [612] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1472"), },
                        [613] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1473"), },
                        [614] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1474"), },
                        [615] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1475"), },
                        [616] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.1476"), },
                        [617] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1477"), },
                        [618] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1478"), },

                    };

                    break;


                case QuestHandle.challengeEther:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1490"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1491"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1492"), },
                        [10] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1493"), },
                        [13] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1494"), },

                        [16] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1496"), },
                        [19] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1497"), },
                        [22] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1498"), },

                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1500"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1501"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1502"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1503") },

                        [52] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1505"), },
                        [55] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1506"), },
                        [58] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1507"), },
                        [61] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1508"), },
                        [64] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1509"), },
                        [67] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1510"), },

                        [76] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1512"), },
                        [79] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1513"), },
                        [82] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1514"), },
                        [85] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1515"), },

                    };

                    break;

                case QuestHandle.treasureChase:

                    sceneDialogue = new()
                    {

                        [990] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1526"), },
                        [991] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1527"), },

                    };

                    break;

                case QuestHandle.questBlackfeather:

                    sceneDialogue = new()
                    {
                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.9"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.10"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.11"), },

                        [101] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.12"), },
                        [102] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.13"), },
                        [103] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.14"), },
                        [104] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.15"), },
                        [105] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.16"), },
                        [106] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.17"), },
                        [107] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.18"), },
                        [108] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.19"), },
                        [109] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.324.20"), },
                        [110] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.21"), },
                        [111] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.324.22"), },
                        [112] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.324.23"), },
                        [113] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.324.24"), },
                        [114] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.25"), },

                        [201] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.26"), },
                        [202] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.27"), },
                        [203] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.28"), },
                        [204] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.29"), },
                        [205] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.30"), },
                        [206] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.31"), },
                        [207] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.32"), },
                        [208] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.33"), },
                        [209] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.34"), },
                        [210] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.35"), },
                        [211] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.36"), },
                        [212] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.37"), },
                        [213] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.38"), },
                        [214] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.39"), },
                        [215] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.40"), },
                        [216] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.41"), },
                        [217] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.42"), },
                        [218] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.43"), },
                        [219] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.44"), },
                        [220] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.45"), },

                        [301] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.46"), },
                        [302] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.47"), },
                        [303] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.48"), },
                        [304] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.49"), },
                        [305] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.50"), },
                        [306] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.51"), },
                        [307] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.52") },
                        [308] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.53"), },
                        [309] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.54"), },
                        [310] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.55"), },
                        [311] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.56"), },
                        [312] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.57"), },
                        [313] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.58"), },
                        [314] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.59"), },
                        [315] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.60"), },
                        [316] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.61"), },
                        [317] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.62"), },
                        [318] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.63"), },
                        [319] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.64"), },
                        [320] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.65"), },
                        [321] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.324.66"), },
                        [322] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.67"), },
                        [323] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.324.68"), },
                        [323] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.69"), },
                        [324] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.70"), },
                        [325] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.71"), },

                        [401] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.72"), },
                        [402] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.73"), },

                        [501] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.74"), },
                        [502] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.75"), },
                        [503] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.76"), },
                        [504] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.77"), },
                        [505] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.78"), },
                        [506] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.79"), },
                        [507] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.80"), },
                        [508] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.81"), },
                        [509] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.82"), },
                        [510] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.83"), },
                        [511] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.84"), },

                        [601] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.85"), },
                        [602] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.86"), },
                        [603] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.87"), },
                        [604] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.88"), },
                        [605] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.89"), },


                    };

                    break;

                case QuestHandle.questBuffin:

                    sceneDialogue = new()
                    {
                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.9"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.10"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.11"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.12"), },

                        [103] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.13"), },
                        [107] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.14"), },
                        [111] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.15"), },
                        [115] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.16"), },
                        [119] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.17"), },
                        [122] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.18"), },
                        [125] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.19"), },

                        [204] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.20"), },
                        [206] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.21"), },
                        [208] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.22"), },
                        [210] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.23"), },
                        [212] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.24"), },
                        [214] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.25"), },
                        [216] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.26"), },
                        [218] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.27"), },
                        [220] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.28"), },
                        [222] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.29"), },
                        [224] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.30"), },
                        [226] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.31"), },

                        [302] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.32"), },
                        [305] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.33"), },
                        [308] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.34"), },
                        [311] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.35"), },
                        [317] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.36"), },
                        [320] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.37"), },
                        [323] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.38"), },
                        [326] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.39"), },
                        [330] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.40"), },
                        [333] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.41") },
                        [337] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.42"), },
                        [341] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.43"), },
                        [344] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.44"), },
                        [348] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.45"), },
                        [352] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.46"), },
                        [355] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.47"), },
                        [358] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.48"), },
                        [361] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.49"), },
                        [364] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.50"), },

                        [402] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.51"), },
                        [405] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.52"), },
                        [412] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.53"), },
                        [450] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.54"), },
                        [455] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.55"), },
                        [458] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.56"), },
                        [461] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.57"), },
                        [464] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.58"), },


                        [502] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.59"), },
                        [506] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.60"), },
                        [510] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.61"), },
                        [514] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.62"), },
                        [518] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.63"), },
                        [522] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.64"), },
                        [526] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.65"), },
                        [529] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.66"), },
                        [532] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.67"), },
                        [536] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.68"), },
                        [539] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.69"), },
                        [542] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.70"), },
                        [546] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.71"), },
                        [550] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.72"), },
                        [554] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.73"), },
                        [557] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.74"), },
                        [560] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.75"), },
                        [564] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.76"), },
                        [567] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.77"), },
                        [570] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.78"), },

                        [601] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.79"), },


                    };

                    break;
            };

            return sceneDialogue;

        }

        public static Dictionary<int, DialogueSpecial> SceneConversations(string scene)
        {

            Dictionary<int, DialogueSpecial> conversations = new();

            switch (scene)
            {

                case QuestHandle.approachEffigy:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1613"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1617"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1618"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1619"),
                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.1625") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1626") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1627") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1628")

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1639") +
                            Mod.instance.Helper.Translation.Get("DialogueData.1640"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1644"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1645"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1646"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1651"),
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

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1671"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1675"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1676"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1687"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1691"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1692"),

                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1703"),

                            responses = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.1708"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1709"),

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

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1730"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1734"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1735"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1746"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1750"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1751"),

                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1762"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1766"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1767"),

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

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1788"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1792")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1797") +
                            Mod.instance.Helper.Translation.Get("DialogueData.1798")

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1809"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1813"),

                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1819") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1820") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1821")
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1831"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1835"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1836"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1841") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1842") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1843") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1844") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1845") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1846")
                            },

                            questContext = 250,

                        },

                        [4] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1856"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1860"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1861"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1866") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1867") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1868")
                            },

                            //questContext = 400,
                            questContext = 450,
                        },

                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1878"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1882"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1883"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1888") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1889") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1890"),
                            },

                            questContext = 500,

                        },

                        [6] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1900") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1901"),

                            responses = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.1906"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1907"),

                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.1914") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1915") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1916"),

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

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1937"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1941"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1942"),
                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.1948"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1959"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1963"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1964"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1969"),
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1979"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1983"),
                                Mod.instance.Helper.Translation.Get("DialogueData.1984"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.1989"),

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

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2010"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2014"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2015"),
                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.2021") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2022"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2033"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2037"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2038"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2039"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2044"),
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2054"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2058"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2059"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2064") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2065") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2066"),

                            },

                            questContext = 300,

                        },

                    };

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.relicWeald))
                    {

                        conversations[3].responses.Add(Mod.instance.Helper.Translation.Get("DialogueData.2079"));

                        string buffer = conversations[3].answers.First();

                        conversations[3].answers.Add(buffer);

                        conversations[3].answers.Add(Mod.instance.Helper.Translation.Get("DialogueData.2085") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2086") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2087"));

                    }

                    break;

                case QuestHandle.swordFates:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2101"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2105"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2106"),
                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.2112"),

                            },

                            questContext = 130,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2123"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2127"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2128"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2133"),
                            },

                            questContext = 140,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2143"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2147"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2148"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2153") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2154"),

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

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2174"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2178"),
                                 Mod.instance.Helper.Translation.Get("DialogueData.2179"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2184")

                            },

                            questContext = 99,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2195"),
                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2198"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2199"),

                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2205") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2206") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2207") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2208") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2209") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2210") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2211")
                            },

                            questContext = 199,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2221"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2225"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2226"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2227"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2232") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2233")
                            },

                            questContext = 299,

                        },

                        [4] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2243"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2247"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2248"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2249"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2254") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2255")
                            },

                            questContext = 799,

                        },

                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2265"),
                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2268"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2269"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2274") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2275") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2276")
                            },

                            questContext = 499,

                        },

                        [6] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2286") +
                            Mod.instance.Helper.Translation.Get("DialogueData.2287"),

                            responses = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.2292"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2293"),

                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.2300") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2301") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2302") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2303"),

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

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2326"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2330"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2331"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2332"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2337") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2338") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2339") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2340") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2341"),

                            },

                            questContext = 144,

                        },

                        [2] = new()
                        {
                            companion = 3,

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2353"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2357"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2358"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2359"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2364") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2365") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2366") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2367") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2368"),
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

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2387") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2388"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2392"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2393").Tokens(new { farm = Game1.player.farmName.Value, }),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2400") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2401") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2402")

                            },

                            questContext = 99,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2413"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2417"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2418")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2423") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2424") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2425"),
                            },

                            questContext = 199,

                        },

                        [3] = new()
                        {

                            companion = 3,

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2437"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2441"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2442"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2447") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2448") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2449") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2450"),
                            },

                            questContext = 299,

                        },

                        [4] = new()
                        {

                            companion = 3,

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2462"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2466"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2467"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2472") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2473") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2474") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2475") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2476")
                            },

                            questContext = 399,

                        },

                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2489"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2493"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2494"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.2499") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2500") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2501") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2502") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2503") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2504"),
                            },

                            questContext = 499,

                        },

                        [6] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2514"),

                            responses = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.2519"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2520"),

                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.2527") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2528") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2529") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2530") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2531") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2532") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2533"),


                            },

                            questContext = 599,

                        },

                        [7] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2545"),

                            responses = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.2550"),
                                Mod.instance.Helper.Translation.Get("DialogueData.2551"),

                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("DialogueData.2558") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2559") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2560") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2561") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2562")

                            },

                            questContext = 699,

                        },

                    };

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Nom0ri.RomRas"))
                    {

                        conversations[3].intro = Mod.instance.Helper.Translation.Get("DialogueData.339.1");
                        conversations[3].responses[0] = Mod.instance.Helper.Translation.Get("DialogueData.339.2");
                        conversations[4].intro = Mod.instance.Helper.Translation.Get("DialogueData.339.3");
                        conversations[5].responses[0] = Mod.instance.Helper.Translation.Get("DialogueData.339.4");

                    }

                    break;

                case QuestHandle.challengeEther:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.315.1"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.315.2"),
                                Mod.instance.Helper.Translation.Get("DialogueData.315.3"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.315.4"),
                            },

                            questContext = 199,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.315.5"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.315.6"),
                                Mod.instance.Helper.Translation.Get("DialogueData.315.7")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.315.8") +
                                Mod.instance.Helper.Translation.Get("DialogueData.315.9"),
                            },

                            questContext = 299,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.315.10"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.315.11"),
                                Mod.instance.Helper.Translation.Get("DialogueData.315.12"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.315.13") +
                                Mod.instance.Helper.Translation.Get("DialogueData.315.14") +
                                Mod.instance.Helper.Translation.Get("DialogueData.315.15") +
                                Mod.instance.Helper.Translation.Get("DialogueData.315.16"),
                            },
                            
                            questContext = 399,

                        },

                    };

                    break;

                case QuestHandle.questBlackfeather:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.324.90"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.91"),
                                Mod.instance.Helper.Translation.Get("DialogueData.324.92"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.93"),
                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.324.94"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.95"),
                                Mod.instance.Helper.Translation.Get("DialogueData.324.96"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.97") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.98") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.99") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.100"),
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.324.101"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.102"),
                                Mod.instance.Helper.Translation.Get("DialogueData.324.103"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.104") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.105"),
                            },

                            questContext = 300,

                        },

                        [4] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.324.106"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.107"),
                                Mod.instance.Helper.Translation.Get("DialogueData.324.108"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.109") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.110") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.111") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.112"),
                            },

                            questContext = 400,

                        },

                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.324.113"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.114"),
                                Mod.instance.Helper.Translation.Get("DialogueData.324.115"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.116"),
                            },

                            questContext = 500,

                        },

                        [6] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.324.117"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.118"),
                                Mod.instance.Helper.Translation.Get("DialogueData.324.119"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.324.120") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.121") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.122") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.123") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.124") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.125") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.126"),
                            },

                            questContext = 600,

                        },

                    };


                    break;

                case QuestHandle.questBuffin:
                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.339.1.84"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.87"),
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.88")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.92") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.93"),
                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.339.1.99"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.102"),
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.103")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.107") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.108") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.109") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.110"),
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.339.1.116"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.119"),
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.120")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.124") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.125") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.126"),
                            },

                            questContext = 300,

                        },

                        [4] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.339.1.132"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.135"),
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.136")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.140") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.141") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.142") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.143") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.144"),
                            },

                            questContext = 400,

                        },

                        [5] = new()
                        {
                            companion = 9,

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.339.1.151"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.154"),
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.155")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.159") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.160") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.161"),

                            },

                            questContext = 500,

                        },

                        [6] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.339.1.167"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.170"),
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.171")
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.175") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.176") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.177") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.178"),

                            },

                            questContext = 600,

                        },
                    };



                    break;
            }

            return conversations;

        }

    }

}


