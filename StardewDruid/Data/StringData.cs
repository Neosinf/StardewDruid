using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace StardewDruid.Data
{
    public static class StringData
    {

        // frequent signs

        public static string colon
        {
            get 
            { 
                return Mod.instance.Helper.Translation.Get("Punctuation.1"); 
            }
        }

        public static string comma
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("Punctuation.2");
            }
        }

        public static string slash
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("Punctuation.3");
            }
        }

        public static string currency
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("Punctuation.4");
            }
        }

        public static string plus
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("Punctuation.5");
            }
        }

        public static string ecks
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("DialogueData.347.3");
            }
        }

        public static string dash
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("DialogueData.500.dash");
            }
        }

        public static string normal
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("DialogueData.386.31");
            }
        }

        public static string silver
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("DialogueData.386.32");
            }
        }

        public static string gold
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("DialogueData.386.33");
            }
        }

        public static string iridium
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("DialogueData.386.34");
            }
        }

        public static string longbreak
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("DialogueData.388.5");
            }
        }

        public static string pluralism
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("DialogueData.388.6");
            }
        }

        public static string stamina
        {
            get
            {
                return Mod.instance.Helper.Translation.Get("StringData.500.stamina");
            }
        }

        public enum str
        {
            stardewDruid,
            magicByNeosinf,

            // Mod, Rite, ModUtility

            receivedData,
            challengeAborted,

            dragonBuff,
            dragonBuffDescription,

            questComplete,
            percentComplete,
            druidShield,
            defenseIncrease,

            jesterBuff,
            jesterBuffTitle,
            jesterBuffDescription,

            herbalBuffDescription,

            hostOnly,
            questReplay,
            outOf,
            lessonSkipped,
            mastered,
            reward,
            replayReward,
            bounty,
            transcript,

            autoOff,
            autoCraft,
            autoApply,
            autoBoth,
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
            nosell,

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

            shortfall,
            currentPrice,

            battle,
            damage,
            health,
            your,
            criticalHit,

            relicReceived,
            lessonReceived,
            questReceived,
            challengeReceived,

            level,
            nextLevel,
            upgradeLevel,

            moreAlchemy,

        }

        public static string Get(str key, System.Object tokens = null)
        {

            switch (key)
            {

                // Mod, Rite, ModUtility, QuestHandle

                case str.receivedData:

                    return Mod.instance.Helper.Translation.Get("DialogueData.265");

                case str.challengeAborted:

                    return Mod.instance.Helper.Translation.Get("DialogueData.269");

                case str.dragonBuff:

                    return Mod.instance.Helper.Translation.Get("DialogueData.277");

                case str.dragonBuffDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.281");

                case str.questComplete:

                    return Mod.instance.Helper.Translation.Get("DialogueData.333");

                case str.percentComplete:

                    return Mod.instance.Helper.Translation.Get("DialogueData.337");

                case str.druidShield:

                    return Mod.instance.Helper.Translation.Get("DialogueData.335.1");

                case str.defenseIncrease:

                    return Mod.instance.Helper.Translation.Get("DialogueData.335.2");

                case str.jesterBuff:

                    return Mod.instance.Helper.Translation.Get("DialogueData.338.1");

                case str.jesterBuffDescription:

                    return Mod.instance.Helper.Translation.Get("DialogueData.338.2");
                    
                case str.jesterBuffTitle:

                    return Mod.instance.Helper.Translation.Get("DialogueData.338.3");

                case str.herbalBuffDescription:

                    return Mod.instance.Helper.Translation.Get("HerbalData.1318");

                // ============================================ JOURNAL

                case str.stardewDruid:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343");

                case str.magicByNeosinf:

                    return Mod.instance.Helper.Translation.Get("DialogueData.352.1");

                // ============================================ JOURNAL MOMENTS

                case str.hostOnly:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399");

                case str.questReplay:

                    return Mod.instance.Helper.Translation.Get("DialogueData.403");

                case str.outOf:

                    return Mod.instance.Helper.Translation.Get("DialogueData.407");

                case str.reward:

                    return Mod.instance.Helper.Translation.Get("DialogueData.411");

                case str.replayReward:

                    return Mod.instance.Helper.Translation.Get("DialogueData.519");

                case str.bounty:

                    return Mod.instance.Helper.Translation.Get("DialogueData.415");

                case str.transcript:

                    return Mod.instance.Helper.Translation.Get("DialogueData.419");

                // -------------------------------------------- Apothecary 

                case str.autoOff:

                    return Mod.instance.Helper.Translation.Get("StringData.500.autoOff");

                case str.autoApply:

                    return Mod.instance.Helper.Translation.Get("StringData.500.autoApply");

                case str.autoCraft:

                    return Mod.instance.Helper.Translation.Get("StringData.500.autoCraft");

                case str.autoBoth:

                    return Mod.instance.Helper.Translation.Get("StringData.500.autoBoth");

                case str.MAX:

                    return Mod.instance.Helper.Translation.Get("DialogueData.435");

                case str.HP:

                    return Mod.instance.Helper.Translation.Get("DialogueData.439");

                case str.STM:

                    return Mod.instance.Helper.Translation.Get("DialogueData.443");

                case str.relicNotFound:

                    return Mod.instance.Helper.Translation.Get("DialogueData.447");

                case str.relicUnknown:

                    return Mod.instance.Helper.Translation.Get("DialogueData.451");

                case str.lessonSkipped:

                    return Mod.instance.Helper.Translation.Get("DialogueData.319.1");

                case str.mastered:

                    return Mod.instance.Helper.Translation.Get("DialogueData.319.2");

                // Dragon menu

                case str.primaryColour:

                    return Mod.instance.Helper.Translation.Get("DialogueData.455");

                case str.secondaryColour:

                    return Mod.instance.Helper.Translation.Get("DialogueData.459");

                case str.tertiaryColour:

                    return Mod.instance.Helper.Translation.Get("DialogueData.463");

                case str.dragonScheme:

                    return Mod.instance.Helper.Translation.Get("DialogueData.467");

                case str.breathScheme:

                    return Mod.instance.Helper.Translation.Get("DialogueData.471");

                case str.dragonRotate:

                    return Mod.instance.Helper.Translation.Get("DialogueData.475");

                case str.dragonScale:

                    return Mod.instance.Helper.Translation.Get("DialogueData.479");

                case str.dragonAccent:

                    return Mod.instance.Helper.Translation.Get("DialogueData.483");

                case str.dragonEye:

                    return Mod.instance.Helper.Translation.Get("DialogueData.487");

                case str.dragonSize:

                    return Mod.instance.Helper.Translation.Get("DialogueData.316.3");

                case str.questLore:

                    return Mod.instance.Helper.Translation.Get("DialogueData.318.4");

                case str.questTranscript:

                    return Mod.instance.Helper.Translation.Get("DialogueData.318.5");


                case str.questionCurrent:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.1");

                case str.questionTalkto:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.2");

                case str.questionTomorrow:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.3");

                case str.questionComplete:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.4");

                case str.questionCongratulations:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.5");

                case str.questionHint:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.6");

                case str.questionPreviously:

                    return Mod.instance.Helper.Translation.Get("DialogueData.343.7");

                case str.druidPower:

                    return Mod.instance.Helper.Translation.Get("DialogueData.356.1");

                case str.damageLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.356.2");

                case str.experience:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.10");

                case str.maxLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.11");

                case str.friendship:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.13");

                case str.healthLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.14");

                case str.attackLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.15");

                case str.speedLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.16");

                case str.resistLevel:

                    return Mod.instance.Helper.Translation.Get("DialogueData.390.4");

                case str.dragonStats:

                    return Mod.instance.Helper.Translation.Get("DialogueData.390.5");

                case str.numberHired:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.17");

                case str.winsAmount:

                    return Mod.instance.Helper.Translation.Get("DialogueData.390.6");

                case str.sendToGoods:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.27");

                case str.sellnow:

                    return Mod.instance.Helper.Translation.Get("DialogueData.390.3");

                case str.nosell:

                    return Mod.instance.Helper.Translation.Get("StringData.500.nosell");

                case str.relationship:

                    return Mod.instance.Helper.Translation.Get("DialogueData.399.1");

                case str.applyBuff:

                    return Mod.instance.Helper.Translation.Get("DialogueData.401.1");

                case str.activeBuff:

                    return Mod.instance.Helper.Translation.Get("DialogueData.401.2");

                // ============================ Miscellaneous

                case str.trashCollected:

                    return Mod.instance.Helper.Translation.Get("DialogueData.529");

                case str.bomberInterruptions:

                    return Mod.instance.Helper.Translation.Get("DialogueData.533");

                case str.slimesDestroyed:

                    return Mod.instance.Helper.Translation.Get("DialogueData.537");

                case str.learnRecipes:

                    return Mod.instance.Helper.Translation.Get("DialogueData.541");

                case str.learnCooking:

                    return Mod.instance.Helper.Translation.Get("DialogueData.347.1");

                case str.restoreStart:

                    return Mod.instance.Helper.Translation.Get("DialogueData.329.1");

                case str.restorePartial:

                    return Mod.instance.Helper.Translation.Get("DialogueData.329.2");

                case str.restoreFully:

                    return Mod.instance.Helper.Translation.Get("DialogueData.329.3");

                case str.restoreTomorrow:

                    return Mod.instance.Helper.Translation.Get("DialogueData.330.1");

                // Title for Ether/Gate challenge
                case str.theDusting:

                    return Mod.instance.Helper.Translation.Get("DialogueData.546");

                case str.abortTomorrow:

                    return Mod.instance.Helper.Translation.Get("DialogueData.550");

                case str.noJunimo:

                    return Mod.instance.Helper.Translation.Get("DialogueData.554");

                case str.noInstructions:

                    return Mod.instance.Helper.Translation.Get("DialogueData.558");

                case str.leftEvent:

                    return Mod.instance.Helper.Translation.Get("DialogueData.562");

                case str.leavingEvent:

                    return Mod.instance.Helper.Translation.Get("DialogueData.566");

                case str.ladderAppeared:

                    return Mod.instance.Helper.Translation.Get("DialogueData.570");

                case str.returnLater:

                    return Mod.instance.Helper.Translation.Get("DialogueData.574");

                case str.reachEnd:

                    return Mod.instance.Helper.Translation.Get("DialogueData.578");

                case str.treasureHunt:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582");

                case str.treasureGuardian:

                    return Mod.instance.Helper.Translation.Get("DialogueData.366.3");

                case str.returnedHome:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582.1");

                case str.joinedPlayer:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582.2");

                case str.noWarpPoint:

                    return Mod.instance.Helper.Translation.Get("DialogueData.582.3");

                case str.wandering:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.29");

                case str.chained:

                    return Mod.instance.Helper.Translation.Get("DialogueData.380.1");

                case str.rapidfire:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.1");

                case str.overheated:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.2");

                case str.shortfall:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.30");

                case str.currentPrice:

                    return Mod.instance.Helper.Translation.Get("DialogueData.386.36");

                case str.battle:

                    return Mod.instance.Helper.Translation.Get("DialogueData.388.1");
                
                case str.damage:

                    return Mod.instance.Helper.Translation.Get("DialogueData.388.2");
                
                case str.health:

                    return Mod.instance.Helper.Translation.Get("DialogueData.388.3");   
                    
                case str.your:

                    return Mod.instance.Helper.Translation.Get("DialogueData.388.4");

                // 3.9.3 buffs

                case str.criticalHit:

                    return Mod.instance.Helper.Translation.Get("DialogueData.393.9");

                case str.relicReceived:

                    return Mod.instance.Helper.Translation.Get("StringData.500.relicReceived");

                case str.lessonReceived:

                    return Mod.instance.Helper.Translation.Get("StringData.500.lessonReceived");

                case str.questReceived:

                    return Mod.instance.Helper.Translation.Get("StringData.500.questReceived");

                case str.challengeReceived:

                    return Mod.instance.Helper.Translation.Get("StringData.500.challengeReceived");

                case str.level:

                    return Mod.instance.Helper.Translation.Get("StringData.500.level").Tokens(tokens);

                case str.nextLevel:

                    return Mod.instance.Helper.Translation.Get("StringData.500.nextLevel");

                case str.upgradeLevel:

                    return Mod.instance.Helper.Translation.Get("StringData.500.upgradeLevel").Tokens(tokens);

                case str.moreAlchemy:

                    return Mod.instance.Helper.Translation.Get("StringData.500.moreAlchemy");

            }

            return Mod.instance.Helper.Translation.Get("DialogueData.586");

        }

    }

}


