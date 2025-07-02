using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
using StardewValley;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace StardewDruid.Data
{
    public static class StringData
    {

        public static string colon = Mod.instance.Helper.Translation.Get("Punctuation.1");

        public static string comma = Mod.instance.Helper.Translation.Get("Punctuation.2");

        public static string slash = Mod.instance.Helper.Translation.Get("Punctuation.3");

        public static string currency = Mod.instance.Helper.Translation.Get("Punctuation.4");

        public static string plus = Mod.instance.Helper.Translation.Get("Punctuation.5");

        public static string normal = Mod.instance.Helper.Translation.Get("DialogueData.386.31");

        public static string silver = Mod.instance.Helper.Translation.Get("DialogueData.386.32");

        public static string gold = Mod.instance.Helper.Translation.Get("DialogueData.386.33");

        public static string iridium = Mod.instance.Helper.Translation.Get("DialogueData.386.34");

        public static string longbreak = Mod.instance.Helper.Translation.Get("DialogueData.388.5");

        public static string pluralism = Mod.instance.Helper.Translation.Get("DialogueData.388.6");

        public static string RiteNames(Rite.Rites rite = Rite.Rites.weald)
        {
            switch (rite)
            {
                default:
                case Rite.Rites.none: return Mod.instance.Helper.Translation.Get("DialogueData.361.1");
                case Rite.Rites.weald: return Mod.instance.Helper.Translation.Get("DialogueData.38");
                case Rite.Rites.mists: return Mod.instance.Helper.Translation.Get("DialogueData.39");
                case Rite.Rites.stars: return Mod.instance.Helper.Translation.Get("DialogueData.40");
                case Rite.Rites.fates: return Mod.instance.Helper.Translation.Get("DialogueData.41");
                case Rite.Rites.ether: return Mod.instance.Helper.Translation.Get("DialogueData.42");
                case Rite.Rites.bones: return Mod.instance.Helper.Translation.Get("DialogueData.312.2");
                case Rite.Rites.bombs: return Mod.instance.Helper.Translation.Get("DialogueData.361.2");
            }

        }

        public enum stringkeys
        {
            stardewDruid,
            magicByNeosinf,

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

            herbalBuffDescription,

            // Journal

            grimoire,
            reliquary,
            dragonomicon,
            apothecary,
            chronicle,
            herbalTrade,
            powderbox,
            ledger,
            menagerie,
            orders,
            satchel,
            goods,
            distillery,
            distilleryEstimated,
            distilleryRecent,

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
            acRestricted,
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

            questionCurrent,
            questionTalkto,
            questionTomorrow,
            questionComplete,
            questionCongratulations,
            questionHint,
            questionPreviously,

            multiplier,
            currency,

            druidPower,
            damageLevel,

            experience,
            maxLevel,
            friendship,
            healthLevel,
            attackLevel,
            speedLevel,
            resistLevel,
            dragonStats,
            numberHired,
            winsAmount,
            relationship,

            sendToGoods,
            sellnow,

            applyBuff,
            activeBuff,

            // Miscellaneous
            trashCollected,
            bomberInterruptions,
            slimesDestroyed,
            learnRecipes,
            learnCooking,
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
            treasureGuardian,

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
            noWarpPoint,
            wandering,

            chained,

            rapidfire,
            overheated,

            level1,
            level2,
            level3,
            level4,
            level5,

            shortfall,
            currentPrice,

            battle,
            damage,
            health,
            your,

            axeBuffName,
            axeBuffDescription,
            pickBuffName,
            pickBuffDescription,
            hoeBuffName,
            hoeBuffDescription,
            canBuffName,
            canBuffDescription,
            criticalHit,

            chargeWealdName,
            chargeWealdDescription,
            chargeMistsName,
            chargeMistsDescription, 
            chargeStarsName,
            chargeStarsDescription, 
            chargeFatesName,
            chargeFatesDescription, 
            chargeBonesName,
            chargeBonesDescription,

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

                case DruidJournal.journalButtons.question:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.1");

                case DruidJournal.journalButtons.bombs:

                    return Mod.instance.Helper.Translation.Get("DialogueData.361.4");

                case DruidJournal.journalButtons.recruits:

                    return Mod.instance.Helper.Translation.Get("DialogueData.361.6");


                case DruidJournal.journalButtons.clearBuffs:

                    return Mod.instance.Helper.Translation.Get("DialogueData.361.5");

                case DruidJournal.journalButtons.clearOne:

                case DruidJournal.journalButtons.clearTwo:

                case DruidJournal.journalButtons.clearThree:

                case DruidJournal.journalButtons.clearFour:

                    return Mod.instance.Helper.Translation.Get("DialogueData.361.8");


                case DruidJournal.journalButtons.summonRecruit:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.5");

                case DruidJournal.journalButtons.dismissRecruit:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.6");

                case DruidJournal.journalButtons.clearRecruit:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.12");


                case DruidJournal.journalButtons.summonPal:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.7");

                case DruidJournal.journalButtons.dismissPal:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.8");

                case DruidJournal.journalButtons.schemePal:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.9");

                case DruidJournal.journalButtons.renamePal:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.18");

                case DruidJournal.journalButtons.removePal:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.29");

                case DruidJournal.journalButtons.goods:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.22");

                case DruidJournal.journalButtons.distilleryEstimated:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.25");

                case DruidJournal.journalButtons.distilleryRecent:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.26");

                case DruidJournal.journalButtons.omens:

                    return Mod.instance.Helper.Translation.Get("DialogueData.390.1");

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

                case stringkeys.herbalBuffDescription:

                    return Mod.instance.Helper.Translation.Get("HerbalData.1318");

                // ============================================ JOURNAL

                case stringkeys.stardewDruid:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343");

                case stringkeys.magicByNeosinf:

                    return Mod.instance.Helper.Translation.Get("DialogueData.352.1");

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

                case stringkeys.herbalTrade:

                    return Mod.instance.Helper.Translation.Get("DialogueData.347.2");

                case stringkeys.powderbox:

                    return Mod.instance.Helper.Translation.Get("DialogueData.361.3");

                case stringkeys.ledger:

                    return Mod.instance.Helper.Translation.Get("DialogueData.361.7");

                case stringkeys.menagerie:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.4");

                case stringkeys.orders:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.19");

                case stringkeys.goods:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.20");

                case stringkeys.distillery:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.21");

                case stringkeys.distilleryEstimated:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.23");

                case stringkeys.distilleryRecent:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.24");

                // ============================================ JOURNAL MOMENTS

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

                case stringkeys.acRestricted:

                    return Mod.instance.Helper.Translation.Get("DialogueData.350.1");

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


                case stringkeys.questionCurrent:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.1");

                case stringkeys.questionTalkto:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.2");

                case stringkeys.questionTomorrow:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.3");

                case stringkeys.questionComplete:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.4");

                case stringkeys.questionCongratulations:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.5");

                case stringkeys.questionHint:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.6");

                case stringkeys.questionPreviously:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.7");

                case stringkeys.multiplier:

                    return Mod.instance.Helper.Translation.Get("DialogueData.347.3");

                case stringkeys.currency:

                    return Mod.instance.Helper.Translation.Get("DialogueData.347.4");


                case stringkeys.druidPower:

                    return Mod.instance.Helper.Translation.Get("DialogueData.356.1");

                case stringkeys.damageLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.356.2");

                case stringkeys.experience:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.10");

                case stringkeys.maxLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.11");

                case stringkeys.friendship:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.13");

                case stringkeys.healthLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.14");

                case stringkeys.attackLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.15");

                case stringkeys.speedLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.16");

                case stringkeys.resistLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.390.4");

                case stringkeys.dragonStats:

                    return Mod.instance.Helper.Translation.Get("DialogueData.390.5");

                case stringkeys.numberHired:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.17");

                case stringkeys.winsAmount:

                    return Mod.instance.Helper.Translation.Get("DialogueData.390.6");

                case stringkeys.sendToGoods:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.27");

                case stringkeys.sellnow:

                    return Mod.instance.Helper.Translation.Get("DialogueData.390.3");

                case stringkeys.relationship:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.1");

                case stringkeys.applyBuff:

                    return Mod.instance.Helper.Translation.Get("DialogueData.401.1");

                case stringkeys.activeBuff:

                    return Mod.instance.Helper.Translation.Get("DialogueData.401.2");

                // ============================ Miscellaneous

                case stringkeys.trashCollected:

                    return Mod.instance.Helper.Translation.Get("DialogueData.529");

                case stringkeys.bomberInterruptions:

                    return Mod.instance.Helper.Translation.Get("DialogueData.533");

                case stringkeys.slimesDestroyed:

                    return Mod.instance.Helper.Translation.Get("DialogueData.537");

                case stringkeys.learnRecipes:

                    return Mod.instance.Helper.Translation.Get("DialogueData.541");

                case stringkeys.learnCooking:

                    return Mod.instance.Helper.Translation.Get("DialogueData.347.1");

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

                case stringkeys.treasureGuardian:

                    return Mod.instance.Helper.Translation.Get("DialogueData.366.3");

                case stringkeys.returnedHome:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582.1");

                case stringkeys.joinedPlayer:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582.2");

                case stringkeys.noWarpPoint:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582.3");

                case stringkeys.wandering:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.29");

                case stringkeys.chained:

                    return Mod.instance.Helper.Translation.Get("DialogueData.380.1");

                case stringkeys.rapidfire:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.1");

                case stringkeys.overheated:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.2");


                case stringkeys.level1:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.377.6");

                case stringkeys.level2:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.377.7");

                case stringkeys.level3:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.377.8");

                case stringkeys.level4:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.377.9");

                case stringkeys.level5:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.377.10");

                case stringkeys.shortfall:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.30");

                case stringkeys.currentPrice:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.36");

                case stringkeys.battle:

                    return Mod.instance.Helper.Translation.Get("DialogueData.388.1");
                
                case stringkeys.damage:

                    return Mod.instance.Helper.Translation.Get("DialogueData.388.2");
                
                case stringkeys.health:

                    return Mod.instance.Helper.Translation.Get("DialogueData.388.3");   
                    
                case stringkeys.your:

                    return Mod.instance.Helper.Translation.Get("DialogueData.388.4");

                case stringkeys.satchel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.390.2");

                // 3.9.3 buffs

                case stringkeys.axeBuffName:

                    return Mod.instance.Helper.Translation.Get("DialogueData.393.1");

                case stringkeys.axeBuffDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.393.2");

                case stringkeys.pickBuffName:

                    return Mod.instance.Helper.Translation.Get("DialogueData.393.3");

                case stringkeys.pickBuffDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.393.4");

                case stringkeys.hoeBuffName:

                    return Mod.instance.Helper.Translation.Get("DialogueData.393.5");

                case stringkeys.hoeBuffDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.393.6");

                case stringkeys.canBuffName:

                    return Mod.instance.Helper.Translation.Get("DialogueData.393.7");

                case stringkeys.canBuffDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.393.8");

                case stringkeys.criticalHit:

                    return Mod.instance.Helper.Translation.Get("DialogueData.393.9");


                // 3.9.9 buffs

                case stringkeys.chargeWealdName:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.11");

                case stringkeys.chargeWealdDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.2");

                case stringkeys.chargeMistsName:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.3");

                case stringkeys.chargeMistsDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.4");

                case stringkeys.chargeStarsName:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.5");

                case stringkeys.chargeStarsDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.6");

                case stringkeys.chargeFatesName:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.7");

                case stringkeys.chargeFatesDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.8");

                case stringkeys.chargeBonesName:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.9");

                case stringkeys.chargeBonesDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.10");


            }

            return Mod.instance.Helper.Translation.Get("DialogueData.586");

        }

        public static string LevelStrings(int level)
        {

            switch (level)
            {
                default:
                case 1:
                    return StringData.Strings(StringData.stringkeys.level1);
                case 2:
                    return StringData.Strings(StringData.stringkeys.level2);
                case 3:
                    return StringData.Strings(StringData.stringkeys.level3);
                case 4:
                    return StringData.Strings(StringData.stringkeys.level4);
                case 5:
                    return StringData.Strings(StringData.stringkeys.level5);

            }
            

        }

    }

}


