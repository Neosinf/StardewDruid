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
            switch (rite)
            {
                default:
                case rites.weald: return "Rite of the Weald";
                case rites.mists: return "Rite of Mists";
                case rites.stars: return "Rite of the Stars";
                case rites.fates: return "Rite of the Fates";
                case rites.ether: return "Rite of Ether";

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

            switch (key)
            {

                // Mod, Rite, ModUtility

                case stringkeys.receivedData:

                    return "Received Stardew Druid data for Farmer ID ";

                case stringkeys.challengeAborted:

                    return "Challenge aborted due to critical condition";

                case stringkeys.riteBuffDescription:

                    return "Actively selected rite";

                case stringkeys.dragonBuff:

                    return "Dragon Scales";

                case stringkeys.dragonBuffDescription:

                    return "Defense increased by transformation";

                case stringkeys.energySkill:

                    return "Not enough energy to perform skill";

                case stringkeys.openJournal:

                    return "to open Druid Journal and get started";

                case stringkeys.noRiteAttuned:

                    return "No rite attuned to slot ";

                case stringkeys.riteTool:

                    return "Rite requires a melee weapon or tool";

                case stringkeys.noToolAttunement:

                    return "This tool has not been attuned to a rite";

                case stringkeys.nothingHappened:

                    return "Nothing happened... ";

                case stringkeys.invalidLocation:

                    return "Unable to reach the otherworldly plane from this location";

                case stringkeys.energyContinue:

                    return "Not enough energy to continue rite";

                case stringkeys.energyRite:

                    return "Not enough energy to perform rite";

                case stringkeys.stamina:

                    return "stamina";

                case stringkeys.druidFreneticism:

                    return "Druidic Freneticism";

                case stringkeys.speedIncrease:

                    return "Speed increased when casting amongst Grass";

                // journal

                case stringkeys.stardewDruid: 

                    return "Stardew Druid";

                case stringkeys.grimoire:

                    return "Grimoire";

                case stringkeys.reliquary:

                    return "Reliquary";

                case stringkeys.dragonomicon:

                    return "Dragonomicon";

                case stringkeys.apothecary:

                    return "Apothecary";

                case stringkeys.startPage: 
                    
                    return "Start page";

                case stringkeys.endPage: 
                    
                    return "End page";

                case stringkeys.sortCompletion: 
                    
                    return "Sort quests by completion";

                case stringkeys.reverseOrder: 
                    
                    return "Reverse order of entries";

                case stringkeys.openQuests: 
                    
                    return "Quests";

                case stringkeys.openEffects: 
                    
                    return "Effects";

                case stringkeys.openRelics: 
                    
                    return "Relics";

                case stringkeys.openPotions: 
                    
                    return "Potions";

                case stringkeys.checkHerbalism:

                    return "Check the herbalism bench in the farm grove";

                case stringkeys.hostOnly:

                    return "(Please note only the farm host can activate quests.)";

                case stringkeys.questReplay:

                    return "(Quest currently being replayed)";

                case stringkeys.outOf:

                    return "out of";

                case stringkeys.reward:

                    return "Reward";

                case stringkeys.bounty:

                    return "Bounty";

                case stringkeys.transcript:

                    return "(transcript)";

                case stringkeys.acEnabled:

                    return "Autoconsumption enabled";

                case stringkeys.acDisabled:

                    return "Autoconsumption disabled";

                case stringkeys.acPriority:

                    return "Autoconsumption enabled with priority given to this line of herbal potions";

                case stringkeys.MAX:

                    return "(MAX)";

                case stringkeys.HP:

                    return "HP";

                case stringkeys.STM:

                    return "STM";

                case stringkeys.relicNotFound:

                    return "You haven't found this relic yet";

                case stringkeys.relicUnknown:

                    return "Unknown Relic"; 

                case stringkeys.primaryColour:

                    return "Primary Colour";

                case stringkeys.secondaryColour:

                    return "Secondary Colour";

                case stringkeys.tertiaryColour:

                    return "Tertiary Colour";

                case stringkeys.dragonScheme:

                    return "Dragon Scheme";

                case stringkeys.breathScheme:

                    return "Breath Scheme";

                case stringkeys.dragonRotate:

                    return "Click to rotate";

                case stringkeys.dragonScale:

                    return "Scale colour";

                case stringkeys.dragonAccent:

                    return "Accent colour";

                case stringkeys.dragonEye:

                    return "Eye colour";

                case stringkeys.dragonReset:

                    return "Reset custom colours";

                case stringkeys.dragonSave:

                    return "Save and Exit";

                case stringkeys.skipQuest:

                    return "Skip Quest";

                case stringkeys.replayQuest:

                    return "Replay Quest";

                case stringkeys.replayTomorrow:

                    return "Replay Available Tomorrow";

                case stringkeys.viewEffect:

                    return "View Related Effect";

                case stringkeys.cancelReplay:

                    return "Cancel Replay";

                case stringkeys.replayReward:

                    return "Special Reward";

                case stringkeys.massHerbalism:

                    return "Brew All Available";

                // Miscellaneous / Events

                case stringkeys.trashCollected:

                    return "Trash Collected";

                case stringkeys.bomberInterruptions:

                    return "Bomber Interrupted";

                case stringkeys.slimesDestroyed:

                    return "Slimes Destroyed";

                case stringkeys.learnRecipes:

                    return "Recipes Learnt";

                case stringkeys.theDusting:

                    // Title for Ether/Gate challenge
                    return "The Dusting";

                case stringkeys.abortTomorrow:

                    return "Event aborted, try again tomorrow";

                case stringkeys.noJunimo:

                    return "The forest spirits left instructions. You can't read them yet.";

                case stringkeys.noInstructions:

                    return "The forest spirits have not left instructions for how to fix this yet";

                case stringkeys.leftEvent:

                    return "Left event zone";

                case stringkeys.leavingEvent:

                    return "Leaving event zone";

                case stringkeys.ladderAppeared:

                    return "A way down has appeared";

                case stringkeys.returnLater:

                    return "Return later today";

                case stringkeys.reachEnd:

                    return "Reach the end of the Tunnel!";

                case stringkeys.treasureHunt:

                    return "Treasure Chase";

            }

            return "(nevermind)";

        }

        public static Dictionary<int, string> DialogueNarrators(string scene)
        {
            Dictionary<int, string> sceneNarrators = new();

            switch (scene)
            {

                case QuestHandle.approachEffigy:

                    sceneNarrators = new()
                    {
                        [0] = "Unknown Voice",
                        [1] = "The Forgotten Effigy",
                    };

                    break;

                case QuestHandle.swordWeald:
                    sceneNarrators = new()
                    {
                        [0] = "Rustling in the woodland",
                        [1] = "Whispers on the wind",
                        [2] = "Sighs of the earth",
                    };
                    break;

                case QuestHandle.challengeWeald:

                    sceneNarrators = new() { 
                        [0] = "Clericbat",
                    };

                    break;

                case QuestHandle.swordMists:

                    sceneNarrators = new()
                    {
                        [0] = "Murmurs on the waves",
                        [1] = "Voice Beyond the Shore",
                    };

                    break;

                case QuestHandle.questEffigy:
                    
                    sceneNarrators = new()
                    {
                        [0] = "The Effigy",
                        [1] = "The Jellyking",
                        [2] = "First Farmer",
                        [3] = "Lady Beyond",
                    };

                    break;

                case QuestHandle.challengeMists:

                    sceneNarrators = new() { 
                        [0] = "Shadow Sergeant", 
                        [1] = "Shadow Thug",
                        [2] = "Shadow Leader",
                        [3] = "The Effigy",
                    };

                    break;

                case QuestHandle.swordStars:

                    sceneNarrators = new()
                    {
                        [0] = "The Last Guardian",
                    };

                    break;

                case QuestHandle.challengeStars:

                    sceneNarrators = new()
                    {
                        [0] = "The Jellyking",
                        [1] = "The Effigy",
                    };

                    break;

                case QuestHandle.challengeAtoll:

                    sceneNarrators = new()
                    {
                        [0] = "Captain of the Drowned",
                        [1] = "The Effigy",
                    };

                    break;

                case QuestHandle.challengeDragon:

                    sceneNarrators = new()
                    {
                        [0] = "Lesser Dragon",
                    };

                    break;

                case QuestHandle.swordFates:

                    sceneNarrators = new()
                    {
                        [0] = "The Jester of Fate",
                    };

                    break;

                case QuestHandle.questJester:

                    sceneNarrators = new()
                    {
                        [0] = "Jester",
                        [1] = "Buffin",
                        [2] = "Marlon",
                        [3] = "Gunther",
                        [4] = "Summoned Saurus",
                    };

                    break;

                case QuestHandle.challengeFates:

                    sceneNarrators = new()
                    {
                        [0] = "The Effigy",
                        [1] = "Jester",
                        [2] = "Buffin",
                        [3] = "Shadow Leader",
                        [4] = "Shadow Sergeant",
                        [5] = "Shadow Goblin",
                        [6] = "Shadow Rogue",
                        [7] = "Confused Bear",
                    };

                    break;

                case QuestHandle.swordEther:

                    sceneNarrators = new()
                    {
                        [0] = "The Jester of Fate",
                        [1] = "Thanatoshi, Twilight Reaper",
                    };

                    break;

                case QuestHandle.questShadowtin:

                    sceneNarrators = new()
                    {
                        [0] = "Shadowtin Bear",
                        [1] = "Dwarf",
                        [2] = "Intriguing Voice",
                        [3] = "Enamoured Voice",
                        [4] = "Shadowtin Cat",
                        [5] = "The Wizard",
                        [6] = "Wizard Duellist",
                        [7] = "Shadow Rogue",
                        [8] = "Shadow Goblin",

                    };

                    break;

                case QuestHandle.challengeEther:

                    sceneNarrators = new()
                    {
                        [0] = "Dust Chef",
                    };

                    break;

                case "treasureChase":

                    sceneNarrators = new()
                    {
                        [0] = "Tresure Thief",
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

                        [1] = new() { [0] = "Farmer", },
                        [2] = new() { [0] = "You come at last", },
                        [3] = new() { [0] = "I'm in the ceiling", },
                        [4] = new() { [0] = "Stand here and perform the rite", },
                        [5] = new() { [0] = "As the first farmer did long ago", },
                        [6] = new() { [1] = "Well done", },

                        [900] = new() { [999] = "Press one of " + Mod.instance.Config.riteButtons.ToString() + " with a tool selected in your inventory", },
                    };


                    break;

                case QuestHandle.swordWeald:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = "Something treads the old paths", [1] = "!", [2] = "!", },
                        [2] = new() { [1] = "Aye, a mortal", },
                        [3] = new() { [2] = "Sent by the gardener", },
                        [4] = new() { [0] = "arise", [1] = "arise", [2] = "sunrise", },

                    };

                    break;


                case QuestHandle.challengeWeald:

                    sceneDialogue = new()
                    {

                        [22] = new() { [0] = "Trespasser", },
                        [25] = new() { [0] = "Filthy two legger", },
                        [28] = new() { [0] = "Cheeep cheep", },
                        [31] = new() { [0] = "You and your kind", },
                        [34] = new() { [0] = "Have defiled the sacred waters", },
                        [37] = new() { [0] = "Cheeep cheep", },
                        [40] = new() { [0] = "Our Lady of Mists", },
                        [43] = new() { [0] = "Demands retribution!", },
                        [54] = new() { [0] = "CHEEEP", },
                        [54] = new() { [0] = "The time of vengeance draws near", },
                        [57] = new() { [0] = "We will be her vanguard", },
                        [60] = new() { [0] = "to cleanse the undervalley", },
                        [63] = new() { [0] = "and bring ruin to the betrayer", },
                        [69] = new() { [0] = "Interminable rocks of damnation!", },
                        [72] = new() { [0] = "CHEEE--- aack", },

                        [900] = new() { [999] = "Remain on the rite circle to increase trash collection", },
                    };

                    break;

                case QuestHandle.swordMists:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = "See who comes before the Lady", },
                        [2] = new() { [0] = "The one who cleansed the spring", },
                        [3] = new() { [0] = "The one who made the river sing again", },
                        [4] = new() { [1] = "My blessing is yours", },

                    };

                    break;

                case QuestHandle.questEffigy:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = "A great day for the beach" },
                        [2] = new() { [0] = "As my old friend would say" },
                        [3] = new() { [0] = "Time to reminisce" },
                        [4] = new() { [0] = "With an old angler technique" },
                        [5] = new() { [0] = "DENIZENS OF THE SHALLOWS, HEED MY VOICE" },
                        [6] = new() { [0] = "!" },
                        [7] = new() { [0] = "Hasten away!" },
                        [8] = new() { [0] = "Now, I will demonstrate how to make fish stew" },
                        [9] = new() { [0] = "I would often prepare this for friends" },
                        [10] = new() { [0] = "BY THE POWER BEYOND THE SHORE" },
                        [11] = new() { [0] = "Perfection" },
                        [12] = new() { [0] = "This stream is a new feature" },
                        [13] = new() { [0] = "We would often gaze at the sky" },
                        [14] = new() { [0] = "That big cloud might have once been a dragon" },
                        [15] = new() { [0] = "Things are simpler now" },
                        
                        [16] = new() { [1] = "Ha ha ha. The wooden puppet returns." },
                        [17] = new() { [0] = "Jellyking. The wretched fiend" },
                        [18] = new() { [1] = "I heard your creaky, broken voice" },
                        [19] = new() { [1] = "And came to laugh at you" },
                        [20] = new() { [0] = "Have you no fear?" },
                        [21] = new() { [1] = "Your friends are gone, your power spent" },
                        [22] = new() { [1] = "You can no longer guard the change" },
                        [23] = new() { [0] = "Enough of you!" },
                        [24] = new() { [1] = "!" },

                        [25] = new() { [1] = "Creak creak creak goes the little wooden man" },
                        [26] = new() { [0] = "You are nothing but a slimey abberation" },
                        [27] = new() { [0] = "How have you survived so many long, barren winters" },
                        [28] = new() { [1] = "I have been resurrected by a new power, a hungry power" },
                        [29] = new() { [0] = "Face my judgement, Jelly-fiend" },
                        [30] = new() { [1] = "Ha ha ha. Too slow, scarecrow." },
                        
                        [31] = new() { [0] = "The energies of the waves call to me" },
                        [32] = new() { [0] = "This is the shore I remember" },
                        [33] = new() { [0] = "We are not alone it seems" },
                        [34] = new() { [0] = "Hmmm... why there are so many?" },
                        [35] = new() { [0] = "This is for you." },
                        [36] = new() { [0] = "I'll linger here for a while" },
                        [37] = new() { [0] = "(sigh)" },

                        [502] = new() { [0] = "It seems the wisps would remind me of something" },
                        [506] = new() { [0] = "A fragment of the past" },
                        [508] = new() { [3] = "You seem upset", },
                        [511] = new() { [2] = "It's just a bit... sudden.", },
                        [514] = new() { [3] = "The health of my kin deteriorates.", },
                        [517] = new() { [3] = "I would have realised sooner...", },
                        [520] = new() { [3] = "...had I not been... distracted", },
                        [523] = new() { [2] = "So you'll go across the sea then", },
                        [526] = new() { [3] = "I must care for them in their slumber", },
                        [529] = new() { [2] = "What about the valley, our circle?", },
                        [532] = new() { [3] = "You have talents, space... and our friend", },
                        [535] = new() { [2] = "He needs you more than he does me", },
                        [538] = new() { [3] = "You must continue to mentor him", },
                        [541] = new() { [3] = "Keep him safe from the Fates", },
                        [544] = new() { [3] = "Show him the beauty of the valley", },
                        [547] = new() { [2] = "It's not enough for me", },
                        [550] = new() { [3] = "??", },
                        [553] = new() { [2] = "Please. Stay.", },
                        [556] = new() { [3] = "I have given you all the time I can", },
                        [559] = new() { [3] = "Goodbye, farmer", },
                        [561] = new() { [2] = "Lady...", },
                        [564] = new() { [0] = "He was never the same after this", },
                        [567] = new() { [0] = "Will I ever understand why?", },

                        [777] = new() { [0] = "?", }
                    };

                    break;

                case QuestHandle.challengeMists:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = "Ah whats that then", },
                        [4] = new() { [1] = "One of them twinkle fingers", },
                        [6] = new() { [0] = "Alright lads get em", },
                        [9] = new() { [0] = "Loading charge", },
                        [11] = new() { [3] = "Beware those explosive rounds",},
                        [13] = new() { [0] = "Blasted thing jammed!", },
                        [18] = new() { [0] = "Loading again", },
                        [20] = new() { [3] = "We must prevent such callous destruction", },
                        [22] = new() { [0] = "Ah whiffed it", },
                        [25] = new() { [0] = "Stop waffling and pin them down", },
                        [28] = new() { [1] = "Its no good. Twinkly's too tricky", },
                        [31] = new() { [0] = "Ughhh... alright loading again", },
                        [35] = new() { [0] = "I'M UNDER PRESSURE HERE", },
                        [38] = new() { [1] = "Dont feel so good bout this", },
                        [40] = new() { [0] = "You'd rather anger the Deep one?", },
                        [42] = new() { [1] = "Sod that, I'd rather fight", },
                        [44] = new() { [0] = "Preparing incendiary", },
                        [48] = new() { [0] = "This isn't going well", },
                        [51] = new() { [2] = "Attempting a capture are we", },
                        [53] = new() { [3] = "That is the mercenary that hunted me", },
                        [55] = new() { [0] = "Trying, boss", },
                        [57] = new() { [2] = "Aim higher", },
                        [59] = new() { [0] = "Uh, sorry boss", },
                        [61] = new() { [2] = "We're too exposed here, call them all back", },
                        [63] = new() { [0] = "You heard the boss!", },
                        [65] = new() { [2] = "Watch yourself, farmer", },

                        [900] = new() { [999] = "Hit the cannoneer to prevent them from firing on the town!", },
                        [901] = new() { [0] = "Boom!", },
                        [902] = new() { [0] = "Fire!", },

                    };
                    
                    break;

                case QuestHandle.swordStars:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = "My Lady of Fortune", },
                        [2] = new() { [0] = "High Priestess of Yoba", },
                        [3] = new() { [0] = "Deliverer of Destiny", },
                        [4] = new() { [0] = "Relieve me of my tireless vigil", },
                        [5] = new() { [0] = "Lead me to the afterlife", },
                        [6] = new() { [0] = "At least, shut the bats up", },
                        [7] = new() { [0] = "Your unwilling servant", },

                    };

                    break;
                
                case QuestHandle.challengeStars:

                    sceneDialogue = new()
                    {

                        [3] = new() { [1] = "The infestation must be contained here", },

                        [12] = new() { [1] = "A larger threat approaches. An old enemy", },

                        [15] = new() { [0] = "HOW BORING", },
                        [18] = new() { [0] = "The monarchs must be asleep.", },
                        [21] = new() { [0] = "If they send only a farmer", },
                        [24] = new() { [0] = "To face the onslaught...", },
                        [27] = new() { [0] = "OF THE MIGHTY SLIME", },

                        [30] = new() { [1] = "Arrogance! You are far diminished since the last age", },
                        [33] = new() { [1] = "A sad reflection in a murky puddle", },

                        [38] = new() { [0] = "Spread out, find the apple spirits!", },
                        [41] = new() { [0] = "Gorge yourselves on elemental power", },

                        [48] = new() { [0] = "You're too late", },
                        [51] = new() { [0] = "The slumber of the kings has led to stagnation", },
                        [54] = new() { [0] = "The land must be destroyed to be renewed", },

                        [57] = new() { [1] = "No, the circle will be renewed", },

                        [65] = new() { [0] = "You will be consumed", },
                        [68] = new() { [0] = "Along with the whole valley", },
                        [71] = new() { [0] = "ALL WILL BE JELLY", },

                        [74] = new() { [1] = "Your jelly is overrated", },

                    };

                    break;

                case QuestHandle.challengeAtoll:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = "Oi matey!", },
                        [4] = new() { [0] = "Ya dare wield the Lady's power here?", },
                        [7] = new() { [0] = "The Fates take you!", },
                        // cannons
                        [16] = new() { [0] = "The Lady is not a friend to the drowned", },
                        [19] = new() { [0] = "She buried us with our boats on this shore", },
                        [22] = new() { [0] = "And the fae won't let us cross over", },
                        // cannons
                        [25] = new() { [0] = "Until we hear the caws of crows", },
                        [34] = new() { [0] = "The waves will wash over our tattered bones", },
                        [37] = new() { [0] = "And we'll stay in the cold embrace of the earth", },
                        // cannons
                        [49] = new() { [0] = "Yeaarggh", },
                        [52] = new() { [1] = "Beg for forgiveness, fiend", },
                        [55] = new() { [1] = "That you may cease to disturb the living", },
                        // cannons
                        [64] = new() { [0] = "You think me too far gone?", },
                        [67] = new() { [0] = "I regret each day the choices we made", },
                        [70] = new() { [0] = "The horizon is the colour of reckoning", },
                        [73] = new() { [0] = "She'll take what's owed her, and you'll be one of us", },
                        [76] = new() { [1] = "Bizarre... the Morticians", },
                        [79] = new() { [1] = "Why would they refuse passage to the afterlife", },

                        [990] = new() { [0] = "cover!", },
                        [991] = new() { [0] = "RUN", },
                        [992] = new() { [0] = "crikey!", },
                        [993] = new() { [0] = "CANNONBALL", },
                        [994] = new() { [0] = "CANNONS AT THE READY!", },
                        [995] = new() { [0] = "FIRE!", },


                    };

                    break;

                case QuestHandle.challengeDragon:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = "(menacing chuckles)", },
                        [4] = new() { [0] = "Something new stumbles into my lair", },
                        [7] = new() { [0] = "Ah... I smell... the " + Mod.instance.rite.castType.ToString(), },

                        [12] = new() { [0] = "the circle is weak", },
                        [15] = new() { [0] = "you'll never compare to the druids of old", },
                        [18] = new() { [0] = "the valley is cursed", },

                        [27] = new() { [0] = "I dared to harness a power", },
                        [30] = new() { [0] = "That would make me the envy of all guardians", },
                        [33] = new() { [0] = "My ambition angered the Fates", },
                        [36] = new() { [0] = "And they trapped me in a prison of my own hubris", },

                        [42] = new() { [0] = "Ah ha ha ha ha", },
                        [45] = new() { [0] = "Such pitiful strikes", },
                        [48] = new() { [0] = "I'll Answer That... With FIRE!", },

                        [54] = new() { [0] = "You should be grateful", },
                        [57] = new() { [0] = "You'll soon be naught but dust and ash", },
                        [60] = new() { [0] = "Better by my hands than by the Reapers", },

                        [81] = new() { [0] = "Your death might please the high priestess", },
                        [84] = new() { [0] = "Perhaps I will be favoured", },
                        [87] = new() { [0] = "Maybe even freed", },
                        [90] = new() { [0] = "Oh to be free of fate", },
                        [93] = new() { [0] = "Yes. Now, DIE", },

                        [900] = new() { [999] = "You managed to escape! Enter the lair to try again.", },

                    };

                    break;

                case QuestHandle.swordFates:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = "DUNGEON TIME!", },
                        [5] = new() { [0] = "Uh... did you just see a ghost? I just saw a ghost.", },
                        [8] = new() { [0] = "Ok time to run", },
                        [11] = new() { [0] = "Well this is a great start", },

                        [27] = new() { [0] = "Well I know we are definitely on the right path", },
                        [30] = new() { [0] = "These spectres bear traces of judgement", },
                        [33] = new() { [0] = "Fragments of souls convicted by the Reaper", },
                        [36] = new() { [0] = "They will linger in undeath until their sentence is up", },

                        [54] = new() { [0] = "Ah is there an end to this place?", },
                        [57] = new() { [0] = "Why is everything so grey", },
                        [60] = new() { [0] = "Then again I am colourblind to half the rainbow", },

                        [81] = new() { [0] = "I think we're getting closer!", },
                        [84] = new() { [0] = "Dont think there will be anything spooky at the end of this... right?", },

                        [91] = new() { [0] = "There... Thanatoshi... the Reaper", },
                        [94] = new() { [0] = "Hey farmer, there's something back here", },
                        [97] = new() { [0] = "Fate wins again!", },
                        [121] = new() { [0] = "Oh... wow", },

                        [900] = new() { [999] = "Get to the end of the tunnel before time runs out!", },

                    };

                    break;

                case QuestHandle.questJester:

                    sceneDialogue = new()
                    {


                        [1] = new() { [0] = "So much human stuff happens here" },
                        [2] = new() { [0] = "I smell something familiar" },
                        [3] = new() { [2] = "Heh. Folks just like the sound of Pelican Town, is all" },
                        [4] = new() { [0] = "Hello adventure man" },
                        [5] = new() { [2] = "So, cat'o'fates, how was the mountain?" },
                        [6] = new() { [0] = "The fallen one eludes me yet" },
                        [7] = new() { [2] = "Ah. I'm sorry I was of no use to your search" },
                        [8] = new() { [0] = "Without your help I wouldn't have even found the mountain" },
                        [9] = new() { [2] = "Only keeping the adventurer's oaths. Hiccup." },
                        [10] = new() { [0] = "Are you well? Looks like cheese poisoning" },
                        [11] = new() { [2] = "I was found unconscious on the path" },
                        [12] = new() { [0] = "!" },
                        [13] = new() { [2] = "Doc had to perform emergency surgery" },
                        [14] = new() { [0] = "That's happened to farmer a few times" },
                        [15] = new() { [2] = "Maru fed me two chunks of iridium cheddar to aid recovery..." },
                        [16] = new() { [0] = "x" },
                        [17] = new() { [0] = "Gross, cheese is for crackers, not humans" },
                        [18] = new() { [2] = "So, as I'm finding it hard to move, I have an errand for you" },
                        [19] = new() { [2] = "I took this off a shadow raider" },
                        [20] = new() { [0] = "! A VISION !" },
                        [21] = new() { [2] = "Lots of shadowfolk topside these days, all graverobbers and thieves" },
                        [22] = new() { [2] = "Gunther's waiting for it at the museum" },
                        [23] = new() { [2] = "Goodbye cat'o'fates, goodbye farmer" },
                        [24] = new() { [0] = "Farewell friend" },

                        [25] = new() { [0] = "Time to get this lumbering thing to the blue man" },
                        [26] = new() { [0] = "Guess tricks will have to wait" },
                        [27] = new() { [0] = "!", [1] = "!" },
                        [28] = new() { [0] = "Buffin!" },
                        [29] = new() { [0] = "?" },
                        [30] = new() { [1] = "I've come to challenge you, Jester" },
                        [31] = new() { [0] = "Of course you have" },
                        [32] = new() { [0] = "What's the game then, Buffin?" },
                        [33] = new() { [1] = "I'll let you decide" },
                        [34] = new() { [0] = "And the stakes?" },
                        [35] = new() { [1] = "A boon, by the laws of the fates" },
                        [36] = new() { [0] = "Hmmm. A paw race. No warp tricks" },
                        [37] = new() { [1] = "Hehe. A terrestrial contest! Exciting" },
                        [38] = new() { [0] = "Past the manor, over the bridge, to the museum" },
                        [39] = new() { [1] = "I'll leave you with a mouth full of tail fluff" },
                        [40] = new() { [0] = "Try to keep up Farmer. GO!" },
                        [41] = new() { [999] = "Wait for Jester by the museum" },
                        [42] = new() { [1] = "Authorities! Apprehend this thief! He stole ALL your biscuits!" },
                        [43] = new() { [0] = "Someone, please, this fox wants to eat me! Grab her!" },
                        [44] = new() { [999] = "Jester has reached the town museum" },

                        [45] = new() { [0] = "This place smells like bread and jam" },
                        [46] = new() { [1] = "Time to head back, Jester" },
                        [47] = new() { [0] = "What?" },
                        [48] = new() { [1] = "It's time to return to court" },
                        [49] = new() { [0] = "...why would I go back. I have my mission" },
                        [50] = new() { [1] = "It's where you belong. It's where we both belong" },
                        [51] = new() { [0] = "I think I belong here." },
                        [52] = new() { [1] = "Where you keep making a fool of yourself?" },
                        [53] = new() { [1] = "Admit it, Fortumei stuffed up again" },
                        [54] = new() { [0] = "Do not besmirch the High Priestess!" },
                        [55] = new() { [1] = "Not everyone shares your adulation, Jester" },
                        [56] = new() { [0] = "Fortumei is our Priestess. Only she knows Yoba's will" },
                        [57] = new() { [1] = "Nonsense. For my boon, I command you to return" },
                        [58] = new() { [0] = "I don't think so" },
                        [59] = new() { [1] = "You're nothing next to Chaos" },
                        [60] = new() { [0] = "BEHOLD! My new trick..." },
                        
                        [61] = new() { [1] = "Pretty" },
                        [62] = new() { [1] = "Jester..." },
                        [63] = new() { [0] = "Hey Buffin, wanna sit on a bridge for a while?" },
                        [64] = new() { [1] = "I'm sorry for what happened at court" },
                        [65] = new() { [0] = "Yea. I was sad for a long time" },
                        [66] = new() { [1] = "I panicked when you volunteered yourself" },
                        [67] = new() { [0] = "To be honest. I was joking when I offered to go" },
                        [68] = new() { [1] = "What? You weren't serious?" },
                        [69] = new() { [0] = "I didn't think it would go this far" },
                        [70] = new() { [1] = "BUT WHAT ABOUT THE REAPER" },
                        [71] = new() { [0] = "Oh. Yea. I thought he would be easier to find" },
                        [72] = new() { [0] = "Imagine if I brought him home. The celebrations..." },
                        [73] = new() { [1] = "Have you found anything?" },
                        [74] = new() { [0] = "We found a hundred of his victims" },
                        [75] = new() { [1] = "!" },
                        [76] = new() { [0] = "They chased us the entire length of a dungeon" },
                        [77] = new() { [1] = "Oh my Chaos" },
                        [78] = new() { [0] = "Yea. At the end of it was statue made in his honour" },
                        [79] = new() { [0] = "Was a bit weird" },
                        [80] = new() { [1] = "Jester, I cant promise to sing your praises at court" },
                        [81] = new() { [1] = "But I'll be watching out for you" },
                        [82] = new() { [0] = "It is good to see you Buffin" },
                        [83] = new() { [0] = "Good night Farmer. Thank you for coming" },


                        [901] = new() { [3] = "Well, by the look of it, the palentological hypothesis is...", },
                        [903] = new() { [0] = "Meow", },
                        [904] = new() { [3] = "That it's very old. Pre-catastrophe, perhaps.", },
                        [906] = new() { [1] = "Jest (cough) bark", },
                        [907] = new() { [3] = "I'm more of a mythologist myself", },
                        [910] = new() { [3] = "Could be a legendary saurus, once the dominant species", },
                        [912] = new() { [0] = "Meowwwww?", },
                        [913] = new() { [3] = "Before the advent of dragons", },
                        [915] = new() { [1] = "Woof! Woof!", },
                        [916] = new() { [3] = "Huh? What is it girl?", },
                        [919] = new() { [0] = "I sense... sadness... and rage", },
                        [922] = new() { [4] = "(grizzled roaring)", },
                        [923] = new() { [3] = "Ahhh! Protect the library!", },
                        [925] = new() { [4] = "Why am I here", },
                        [928] = new() { [3] = "What have I got to throw here...", },
                        [931] = new() { [4] = "I should be at rest, I should be...", },
                        [934] = new() { [3] = "It's defacing my inlaid hardwood panelling!", },
                        [937] = new() { [4] = "The power of the Stars has seeped into the land", },
                        [940] = new() { [4] = "The Fates continue to shun us", },
                        [943] = new() { [0] = "Dear ancient lizard, I am an envoy of Fate, the Jester", },
                        [946] = new() { [4] = "What, furred one? You are naught but a morsel", },
                        [949] = new() { [3] = "Crikey! If only I didn't loan our weapon collection to Zuzu mid!", },
                        [952] = new() { [4] = "Have the dragons abandoned this world?", },
                        [955] = new() { [4] = "The furries have taken dominion", },
                        [958] = new() { [3] = "Tell the guildmaster I wont accept any more cursed artifacts!", },
                        [961] = new() { [3] = "Farmer??! Can't you perform a rite of banishment or something?", },
                        [963] = new() { [1] = "The blue guy has a good idea!", },
                        [965] = new() { [3] = "This is going to cost the historic trust society", },
                        [968] = new() { [0] = "Well that was fun. But it's a bit smokey in here.", },
                        
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

                        [2] = new() { [3] = "Hmm. The druids got a step ahead of us", },
                        [5] = new() { [0] = "Caution, interloper. We will not tolerate any more trespasses", },
                        [8] = new() { [3] = "You have developed some courage since our first encounter", },
                        [11] = new() { [0] = "That was before the successor ascended to archdruid of the circle", },
                        [14] = new() { [1] = "Yea, tell them woodface! Who's hiding in caves now?", },
                        [17] = new() { [3] = "Strange thing to say, but no matter", },
                        [20] = new() { [3] = "Surrender, and we will deal with you fairly, with your circle intact", },
                        [23] = new() { [2] = "YOUR TERMS ARE UNACCEPTABLE! EN GARDE!", },
                        [26] = new() { [1] = "Buffin, wait! Why is she always so feisty", },
                        
                        [30] = new() { [4] = "Sir they're fielding... animals", },
                        [33] = new() { [3] = "Unexpected, but we're still better prepared", },
                        [36] = new() { [7] = "My pride has not recovered from my humiliation at your mutiny", },

                        [40] = new() { [1] = "Fear not Buffy, I will protect thee", },
                        [43] = new() { [2] = "I don't think they are in the mood for tricks, Jester", },
                        [46] = new() { [7] = "I do not resent my situation though", },

                        [50] = new() { [6] = "Shadows take thee, twinkle fingers", },
                        [53] = new() { [0] = "By decree of the Kings and the Lady Beyond", },
                        [56] = new() { [0] = "The sacred spaces shall not bear those of ill intent", },

                        [60] = new() { [3] = "Sergeant, command your brutes to suppress the golem", },
                        [63] = new() { [3] = "I will engage the Druid", },
                        [66] = new() { [7] = "I have found a better way, a more just cause", },

                        [70] = new() { [2] = "Grrrr....Bark bark!", },
                        [73] = new() { [1] = "Purrrrrr... (hack) I mean, WOOF!", },
                        [76] = new() { [5] = "Stay back beasts!", },

                        [80] = new() { [4] = "I'm almost spent on ammunition", },
                        [83] = new() { [3] = "Those creatures are clearly not of the earthly variety", },
                        [86] = new() { [3] = "I suspect the Fates work against us. Retreat!", },

                        [90] = new() { [3] = "We're beaten. I should treaty with the druids", },
                        [93] = new() { [4] = "Sir, the other humans won't pay if we expose ourselves", },
                        [96] = new() { [3] = "You're still concerned with compensation?", },
                        [99] = new() { [5] = "We put our trust in coin... and dragon magic.", },
                        [102] = new() { [6] = "This is it for me. Too many set backs. Too many wounded.", },
                        [105] = new() { [5] = "Yea I'm done with Bear. ", },
                        [108] = new() { [4] = "You're a great scholar, sir, but...", },
                        [111] = new() { [4] = "We need a better point man.", },
                        [114] = new() { [5] = "The Deep One will know what to do", },
                        [117] = new() { [3] = "You're making a mistake! He is the Lord of ruin, our ruin.", },
                        [120] = new() { [6] = "Pfft. He's got the power. What can you do.", },
                        [123] = new() { [5] = "See ya Bear.", },

                        [126] = new() { [1] = "I feel bad for Burgundy Bear. He's professional", },
                        [129] = new() { [2] = "His humiliation is almost complete. Now for the coup de grace", },
                        [132] = new() { [0] = "Well Successor... you may determine the vanquished's fate", },

                    };

                    break;

                case QuestHandle.swordEther:

                    sceneDialogue = new()
                    {
                        [3] = new() { [1] = "You", },
                        [6] = new() { [1] = "You bear the scent... OF HERESY", },
                        [9] = new() { [0] = "What a moment...", },
                        [12] = new() { [0] = "Thanatoshi?", },

                        [15] = new() { [1] = "The dragon's power is mine to use!", },
                        [18] = new() { [1] = "I will reap, and reap, and reap", },

                        [21] = new() { [0] = "Farmer, it's him, The Reaper", },
                        [24] = new() { [0] = "Thanatoshi!", },
                        [27] = new() { [0] = "It is I, your kin, the Jester", },
                        [30] = new() { [0] = "Stop this madness!", },
                        [33] = new() { [0] = "It's no use, he won't listen", },

                        [42] = new() { [1] = "The seal to the undervalley will not withstand me", },
                        [45] = new() { [1] = "I will remain true to my purpose", },
                        [48] = new() { [1] = "Yoba will forgive me", },
                        [51] = new() { [1] = "Justice will favour me", },

                        [60] = new() { [0] = "That's... a cutlass... on the shaft", },
                        [63] = new() { [0] = "What has he done to himself?", },

                        [75] = new() { [1] = "Are you a spy of the star general", },
                        [78] = new() { [1] = "He cannot hope to match me now", },
                        [81] = new() { [1] = "Now...", },
                        [84] = new() { [1] = "How long has it been since I saw...", },

                        [93] = new() { [0] = "I guess we have no choice...", },
                        [96] = new() { [0] = "For Fate and Fortune!", },

                        [991] = new() { [0] = "Thanatoshi... why...", },
                        [992] = new() { [1] = "Masayoshi... I failed...", },
                    };

                    break;

                case QuestHandle.questShadowtin:

                    sceneDialogue = new()
                    {
                        // Dwarf interaction
                        [1] = new() { [0] = "I'm impressed with your timeliness, Archdruid", },
                        [2] = new() { [0] = "My associate agreed to wait here", },
                        [3] = new() { [1] = "(PsShT) Quickly now, the product", },
                        [4] = new() { [0] = "As agreed. Thank you for honouring our agreement", },
                        [5] = new() { [0] = "I know my past and nature might have", },
                        [6] = new() { [1] = "(PsShT) Whatever, shadowthief.", },
                        [7] = new() { [1] = "(PsShT) We all pillage from the surface.", },
                        [8] = new() { [1] = "(PsShT) You can drop the pretense of honour.", },
                        [9] = new() { [0] = "I... suppose you're right.", },
                        
                        [100] = new() { [0] = "The bunker is south of here.", },
                        [101] = new() { [0] = "The dwarf insisted this universal access key would suffice.", },
                        [102] = new() { [0] = "Brilliant.", },
                        [103] = new() { [0] = "An ethereal nexus, just as I suspected.", },

                        [200] = new() { [0] = "Wait... do you hear those voices?", },
                        [201] = new() { [2] = "Amongst desert elementals, I'm known as... Snazzymodius", },
                        [202] = new() { [3] = "Oooo... the name fits the presentation", },
                        [203] = new() { [0] = "The speakers must be in proximity to a network terminal", },
                        [204] = new() { [0] = "Their voices are being carried on a conduit", },
                        [205] = new() { [2] = "They have a particular eye for that which glitters", },
                        [206] = new() { [3] = "The eye is very pleased", },
                        [207] = new() { [0] = "We'll be fine if we're silent near the machine", },
                        [208] = new() { [0] = "It was prudent to leave one overly large and noisy cat at home.", },
                        [209] = new() { [0] = "Ah... a manuscript.", },
                        [210] = new() { [4] = "Me.... ow? Meow", },
                        [211] = new() { [4] = "MEOW", },
                        [212] = new() { [5] = "Trespassers", },
                        [213] = new() { [5] = "So. Farmer "+Game1.player.Name, },
                        [214] = new() { [5] = "How will you explain this intrusion", },
                        [215] = new() { [4] = "Nyan", },
                         
                        [300] = new() { [6] = "Those feeble rites you practice", },
                        [301] = new() { [6] = "Are an antiquated form of spellcasting", },
                        [302] = new() { [6] = "As diminished as the myths associated with them", },
                        [303] = new() { [6] = "The Celestials, The Fates, all contortions of legends", },
                        [304] = new() { [6] = "The magic of our world is an elemental construct", },
                        [305] = new() { [6] = "A direct generation from the power of dragons", },
                        [306] = new() { [6] = "How... you have matched me so far", },
                        [307] = new() { [6] = "You should be exhausted... edified by magic", },
                        [308] = new() { [6] = "I cannot sustain this exertion", },
                        [309] = new() { [4] = "meow... (furball hack)", },
                        [310] = new() { [6] = "Enough. I've been bested.", },

                        [400] = new() { [5] = "A shame, that you keep company with this knave", },
                        [401] = new() { [5] = "Relinquish the tome. Its secrets are beyond you", },
                        [402] = new() { [0] = "I only seek to liberate my people", },
                        [403] = new() { [0] = "from the lies and corruption spread by those", },
                        [404] = new() { [0] = "who lay traps and pitfalls for seekers of truth", },
                        [405] = new() { [5] = "As long as your behaviour lacks decency and care for others", },
                        [406] = new() { [5] = "You will never be worthy of what you seek", },
                        [407] = new() { [5] = "Now if you'll excuse me, I have prior business to attend to" },
                        [408] = new() { [2] = "Forgive the interruption, my duties" },
                        [409] = new() { [2] = "They require an attentive mind", },
                        [410] = new() { [2] = "Lately my research... has made it difficult to relax", },
                        [411] = new() { [3] = "Oh, I think I can help with that", },

                        [500] = new() { [0] = "Curious, I had this area marked on my chart", },
                        [501] = new() { [0] = "As a node of concentrated, stagnant ether", },
                        [502] = new() { [0] = "I theorise that more artifacts are sealed here.", },
                        [503] = new() { [0] = "If you would be so kind, Archdruid", },
                        [504] = new() { [0] = "Great. The digusting remains of a pumpkin fiend", },
                        [505] = new() { [0] = "There's an epitaph underneath it.", },

                        [600] = new() { [7] = "Shadowtin! I believe you owe us something", },
                        [601] = new() { [8] = "Our friend here says you have OUR access key", },
                        [602] = new() { [0] = "What ploy is this, Rogue? Goblin?", },
                        [603] = new() { [7] = "The dwarf was told not to deal with deserters", },
                        [604] = new() { [7] = "Either you pay the penalty, or we hurt him", },
                        [605] = new() { [0] = "You would debase the name of MY mercenary company", },
                        [606] = new() { [0] = "With a shakedown this pathetic", },
                        [607] = new() { [7] = "We're following the standard you set, with improvements", },
                        [608] = new() { [1] = "(PsSht) Please, just give them what they want", },
                        [609] = new() { [0] = "It's the farmer's key now. It's not mine to give", },
                        [610] = new() { [8] = "If you care for the small man's life, you'll hand it over", },
                        [611] = new() { [1] = "(PsSht) Whelp. That's me done for then. Dragonbreath incoming.", },
                        [612] = new() { [0] = "No. I'll settle this.", },
                        [613] = new() { [0] = "This ether-wind chart led to all these discoveries", },
                        [614] = new() { [0] = "That concludes my association with you lot. We're finished.", },
                        [615] = new() { [7] = "Haha. Deep was right about you Shadowtin", },
                        [616] = new() { [8] = "You're just a tool, and you've lost your edge", },
                        [617] = new() { [1] = "(PsSht) I didn't expect you to help... thank you", },
                        [618] = new() { [1] = "(PsSht) Farewell friend", },

                    };

                    break;


                case QuestHandle.challengeEther:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = "unexpected", },
                        [4] = new() { [0] = "the leader of the circle should know", },
                        [7] = new() { [0] = "only the rite of bones can open the gate", },
                        [10] = new() { [0] = "but the crows are gone", },
                        [13] = new() { [0] = "and the golden bones with them", },

                        [16] = new() { [0] = "can you feel it", },
                        [19] = new() { [0] = "all around us", },
                        [22] = new() { [0] = "THE DUST RISES", },

                        [27] = new() { [0] = "the flowers the kings grew here", },
                        [30] = new() { [0] = "the sweet berries I tendered", },
                        [33] = new() { [0] = "made to ashes by war", },
                        [36] = new() { [0] = "turned to dust by neglect"},

                        [52] = new() { [0] = "the monarchs sleep", },
                        [55] = new() { [0] = "and meeps creep into the world", },
                        [58] = new() { [0] = "we dared to taste the spirits of the forest", },
                        [61] = new() { [0] = "the pastry was exquisite", },
                        [64] = new() { [0] = "I dusted it perfectly", },
                        [67] = new() { [0] = "and now I am dust", },

                        [76] = new() { [0] = "the grit of our lives", },
                        [79] = new() { [0] = "the chaff of our misdeeds", },
                        [82] = new() { [0] = "take it", },
                        [85] = new() { [0] = "blow it all away", },

                    };

                    break;

                case "treasureChase":

                    sceneDialogue = new()
                    {

                        [990] = new() { [999] = "Chase the thief to recover the treasure!", },
                        [991] = new() { [999] = "A way down has appeared", },

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
