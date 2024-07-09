
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event.Relics;
using StardewDruid.Location;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.GameData.Buildings;
using StardewValley.GameData.Characters;
using StardewValley.GameData.Movies;
using StardewValley.Locations;
using StardewValley.Tools;
using System.Collections.Generic;

namespace StardewDruid.Journal
{
    public static class QuestData
    {


        public static Dictionary<string,Quest> QuestList()
        {

            Dictionary<string,Quest> quests = new();

            // =====================================================
            // APPROACH EFFIGY

            Quest approachEffigy = new()
            {
                
                name = QuestHandle.approachEffigy,
                
                type = Quest.questTypes.approach,

                icon = IconData.displays.effigy,

                // -----------------------------------------------

                give = Quest.questGivers.none,

                trigger = true,

                triggerLocation = "FarmCave",

                triggerRite = Rite.rites.none,

                origin = new Vector2(7,7)*64,

                // -----------------------------------------------

                title = "Grandfather's note",

                description = "There's a note stuck to grandpa's old scythe: " +
                    "\"For the new farmer. Before my forefathers came to the valley, the secluded grove behind the farm was a frequent meeting ground for a circle of Druids. " +
                    "An eeriness hangs over the place, so I have kept my father's tradition of leaving it well enough alone. Perhaps you should too.\"",

                instruction = "Investigate the secluded grove and old farm cave. Press one of " + Mod.instance.Config.riteButtons.ToString() + " to cast a rite at the quest icon.",

                explanation = "I heard a voice in the cave, and it bid me to speak words in a forgotten dialect. " +
                    "I raised my hands, recited a chant, and a wooden effigy crashed to the floor, which then started to talk! " +
                    "It spoke to me and told me how I could learn the ways of the valley Druids. And so my apprenticeship begins...",

                progression = null,

                // -----------------------------------------------

            };

            quests.Add(approachEffigy.name, approachEffigy);

            // =====================================================
            // SWORD WEALD

            Quest swordWeald = new()
            {

                name = QuestHandle.swordWeald,

                type = Quest.questTypes.sword,

                icon = IconData.displays.weald,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = Location.LocationData.druid_grove_name,

                triggerTime = 0,

                triggerRite = Rite.rites.none,

                origin = new Vector2(21, 10) * 64,

                // -----------------------------------------------

                title = "The Two Kings",

                description = "The Effigy says I need the favour of the Kings of Oak and Holly to begin my apprencticeship to the circle of Druids. " +
                "He instructed me to pay homage to the forgotten monarchs at a sacred place in the secluded grove.",

                instruction = "Investigate the standing stones in the grove west of the farmcave. Press one of " + Mod.instance.Config.riteButtons.ToString() + " to cast a rite at the quest icon.",

                explanation = "The voices that spoke from beyond the standing stones made me a squire, and the sword I received seems to brim with woodland magic.",

                progression = null,

                requirement = 0,

                reward = 250,

                details = new(),

                // -----------------------------------------------

                before = new() {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "The standing stones to the west of here were important to the old circle of Druids. Stand before them, and pay homage to the two kings by performing the rite as you did before. " +
                            "If you truly possess a connection to the otherworld, then the latent energies there will be drawn to you. " +
                            "(New quest received)",

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "So you have returned as a squire of the Two Kings.",
                        responses = new()
                        {
                            "I heard some really weird voices. Something touched me. I'm ok.",
                            "So it is real. The power of the Druids.",
                            "I have a sword shaped like a branch. It's a stick of power."
                        },
                        answers = new(){
                            "The energies of the Weald are unsettling. And dumb as rocks. Seek them out again if you would like to dedicate a different implement to the work of the Kings.",
                        }
                    }
                },

                // -----------------------------------------------

            };

            quests.Add(swordWeald.name, swordWeald);

            // =====================================================
            // HERBALISM

            Quest herbalism = new()
            {

                name = QuestHandle.herbalism,

                icon = IconData.displays.chaos,

                type = Quest.questTypes.miscellaneous,

                give = Quest.questGivers.none,

                title = "The Druid Tradition",

                description = "I have the opportunity to augment my natural abilities with tonics and potions brewed in the Druidic tradition.",

                instruction = "Check the herbalism bench in the sacred grove to learn how to brew basic potions.",

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "I managed to retrieve an old mortar and pestle, a favoured tool of your forebearers in the circle. It is in remarkable condition. I left it on the bench on the southern extent of the grove. " +
                        "(New quest received)",

                    }
                },

            };

            quests.Add(herbalism.name, herbalism);

            // =====================================================
            // WEALD LESSONS

            Quest wealdOne = new()
            {

                name = QuestHandle.wealdOne,

                icon = IconData.displays.weald,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.none,

                title = "Lesson One: Clearance",

                description = "I have been blessed by the energies of the Weald. I can practice my new found ability by clearing weeds and twigs from overgrown spaces.",

                instruction = "Perform Rite of the Weald: Clearance one hundred times. Check the effects page "+ Mod.instance.Config.effectsButtons.ToString() + " for details",

                progression = "weeds and twigs destroyed",

                requirement = 100,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "Good. You are now a subject of the two kingdoms, and bear authority over the weed and the twig. Use this new power to drive out decay and detritus. Return tomorrow for another lesson. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(wealdOne.name, wealdOne);

            // -----------------------------------------------------

            Quest wealdTwo = new()
            {

                name = QuestHandle.wealdTwo,

                icon = IconData.displays.weald,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Two: Wild Bounty",

                description = "As I perform the rite, the valley springs into new life around me, offering a sample of its hidden bounty.",

                instruction = "Perform Rite of the Weald: Bounty to rustle twenty large bushes for forageables. Unlocks wild seed gathering from grass.",

                progression = "bushes rustled",

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "Your journey continues with this lesson. The wild spaces hold many riches. Call out to the trees, the bushes and the grass, that they may offer you a portion of their bounty. " +
                        "Those creatures that are caught in the midst of your work should be delighted by the gentle caress of power. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(wealdTwo.name, wealdTwo);

            // -----------------------------------------------------

            Quest wealdThree = new()
            {

                name = QuestHandle.wealdThree,

                icon = IconData.displays.weald,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Three: Wild Growth",

                description = "Years of stagnation have starved the valley of it's wilderness. I now have the means and power to recolour the barren spaces with new plant-life.",

                instruction = "Perform Rite of the Weald: Wild Growth to sprout twenty forageables total in the Forest or anywhere with lawn (grass tiles). Unlocks flowers.",

                progression = "forageables spawned",

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "This is your task today. Perform the rite as you have been taught, only this time, you may convince the wild to sprout new shoots and buds. Now go, fill the barren spaces with life. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(wealdThree.name, wealdThree);

            // -----------------------------------------------------

            Quest wealdFour = new()
            {

                name = QuestHandle.wealdFour,

                icon = IconData.displays.weald,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Four: Cultivation",

                description = "My connection to the otherworld deepens. I may channel the power of the Two Kings to enhance the quality and growth of my crops.",

                instruction = "While on the farm, hold the rite button for 2-3 seconds while standing still to channel this effect. Complete 5 times. Converts planted seasonal wild seeds into domestic crops. Updates growth cycle of all planted seeds. Unlocks quality conversions.",

                progression = "cultivation rites completed",

                requirement = 5,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "For those who plan for a life of toil in the fields and meadows, they should know that farmer and druid share many of the same ideals. " +
                        "You'll need to remain still to channel the energies required to bring the seeds to sprout. May the blessings of the two kings assist you in your farmwork. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(wealdFour.name, wealdFour);

            // -----------------------------------------------------

            Quest wealdFive = new()
            {

                name = QuestHandle.wealdFive,

                icon = IconData.displays.weald,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Five: Rockfall",

                description = "The power of the two Kings resonates through the deep earth, and in the mines, the earth responds in kind.",

                instruction = "Gather one hundred stone from creating rockfalls in the mines with Rite of the Weald. Unlocks rockfall damage to monsters.",

                progression = "rockfall events",

                requirement = 100,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "Be careful in the mines. The Druid draws power from the deep rock, and it will answer your call, both above and below you. " +
                        "For those rocks that prove recalcitrant, gather them to the crafters' bench in the grove, and employ the use of this hammer. " +
                        "It is of the otherworld, and was used for the artifice of otherworldly things, such as myself, though I was not there to witness it. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(wealdFive.name, wealdFive);

            // =====================================================
            // WEALD CHALLENGE

            Quest challengeWeald = new()
            {

                name = QuestHandle.challengeWeald,

                type = Quest.questTypes.challenge,

                icon = IconData.displays.weald,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = "UndergroundMine20",

                triggerTime = 0,

                triggerRite = Rite.rites.weald,

                origin = new Vector2(25f, 13f) * 64,

                // -----------------------------------------------

                title = "The Polluted Aquifier",

                description = "The mountain spring, from an aquifer of special significance to the otherworld, has been polluted by rubbish dumped in the abandoned mineshafts. " +
                "The Effigy believes I am strong enough to cleanse the waters with the power of the Two Kings.",

                instruction = "Perform a Rite of the Weald at the aquifier in level 20 of the mines.",

                explanation = "I reached a large cavern with a once pristine lake, and used the Rite of the Weald to purify it. " +
                "There was so much trash, and so many bats. A big one in a cleric's mitre claimed to serve a higher power, one with a vendetta against the polluters.",

                progression = null,

                requirement = 0,

                reward = 2000,

                replay = "Friendship with mountain residents",

                details = new()
                {
                    "The residents of the mountain and their friends are pleased with the cleanup: Sebastian, Sam, Maru, Abigail, Robin, Demetrius, Linus.",

                },

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "A trial presents itself. For some time now, foulness has seeped from a spring once cherished by the monarchs. " +
                        "You must travel through caverns of the mountain to the spring's source and cleanse it with the blessing of the Kings. " +
                        "(New quest received)",

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "I sense a change. The foulness has dissipated.",
                        responses = new()
                        {
                            "The rite disturbed some bats. BIG BATS.",
                            "I removed the pollutants from the aquifer. The water quality of the mountain springs has already started to recover."
                        },
                        answers = new(){
                            "You have my gratitude. And there are still many more sacred places to restore.",
                        }
                    }
                },

            };

            quests.Add(challengeWeald.name, challengeWeald);

            // =====================================================
            // RELICS QUESTS

            Quest relicWeald = new()
            {

                name = QuestHandle.relicWeald,

                type = Quest.questTypes.relics,

                icon = IconData.displays.weald,

                // -----------------------------------------------

                give = Quest.questGivers.none,

                trigger = true,

                triggerLocation = "CommunityCenter",

                triggerTime = 0,

                triggerRite = Rite.rites.weald,

                origin = new Vector2(14, 22) * 64,

                // -----------------------------------------------

                title = "The Runestones",

                description = "I have gathered all the runestones crafted by the founding members of the circle of druids. " +
                "Ironically, during the course of the recovery of the runestones, I gained enough experience to master the powers my forebearers sought to acquire, and so the runestones themselves offer little use to me now. " +
                "Perhaps they might attain some status, and inspire some new art, if I put them on display in the craftsroom of the local community center.",

                instruction = "Perform a Rite of the Weald in the craftroom in the community center.",

                explanation = "I placed the runestones in organised positions around the craftsroom, and incidentally created a happy zone of harmonised energies. Then the forest spirits took the runestones off the wall and decorated the craftroom with pictures of happy apples.",

            };

            quests.Add(relicWeald.name, relicWeald);

            // =====================================================
            // SWORD MISTS

            Quest swordMists = new()
            {

                name = QuestHandle.swordMists,

                type = Quest.questTypes.sword,

                icon = IconData.displays.mists,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = Location.LocationData.druid_atoll_name,

                triggerTime = 0,

                triggerRite = Rite.rites.none,

                origin = new Vector2(30,21) * 64,

                // -----------------------------------------------

                title = "The Voice Beyond the Shore",

                description = "The Effigy believes the religious coven of large bats serve none other than the Lady Beyond the Shore. " +
                "She has granted me an audience on a small atoll accessed from the furthest side of the beach. I'll have to repair the bridge to the tidal pools to reach it.",

                instruction = "Travel to the beach south of Pelican town, past the small wooden bridge, the tidal pools, and onto a dinghy you can use to access the atoll. " +
                "Cast the Rite of the Weald at the statue there.",

                explanation = "I called out across the waves, imagining my voice travelling over the gem sea to an isle of mystery and magic. " +
                "Then a voice answered, and it was like lightning, my body shook with the words, and when I opened my eyes my hands held a weapon.",

                progression = null,

                requirement = 0,

                reward = 250,

                details = new(),

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "The purification of the sacred waters has pleased a distant patron of the mists, the Lady Beyond the Shore. " +
                        "Perhaps you cannot hear her voice as I can, but it harkens to you. " +
                        "There is a special place east of the local shoreline where you can answer her call. " +
                        "The servants of the Mists supply a vessel to cross the waters to the atoll, and this relic can be used to summon them if necessary. " +
                        "(New quest received)",

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "So you have been blessed by the Lady Beyond.",
                        responses = new()
                        {
                            "The Lady has a beautiful voice. Bit loud though.",
                            "There was a bolt of lightning. There was a voice, like thunder. There was a gannet, it went 'squawk'. I wasn't scared though.",
                        },
                        answers = new()
                        {
                            "It takes a lot of power to speak over the distance between the valley and the isle of mists.",
                        },
                    }
                },


            };

            quests.Add(swordMists.name, swordMists);

            // =====================================================
            // MISTS LESSONS

            Quest mistsOne = new()
            {

                name = QuestHandle.mistsOne,

                icon = IconData.displays.mists,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.none,

                title = "Lesson Six: Sunder",

                description = "The Lady Beyond the Shore has granted me the power to remove common obstacles. Now I can be her representative to the further, wilder spaces of the valley.",

                instruction = "Perform Rite of Mists: Sunder to destroy ten obstacles. This includes boulders, stumps (including little stumps) or logs.",

                progression = "obstacles destroyed",

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "The relationship between our Circle and Lady Beyond the Shore has languished over many generations. It is good to know she still favours us, and our mission here. " +
                        "Enjoy the use of the power of the mists against the larger obstacles in your path. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(mistsOne.name, mistsOne);

            // -----------------------------------------------------

            Quest mistsTwo = new()
            {

                name = QuestHandle.mistsTwo,

                icon = IconData.displays.mists,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Seven: Artifice",

                description = "The raw energy provided by the mists is precise enough to charge artifacts with special power.",

                instruction = "Rite of Mists has special interactions with various map-specific and crafted items, including warp statues, lightning rods and scarecrows. " +
                "Perform ten of these interactions. See the Druid Effects journal for details.",

                progression = "special interactions performed",

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "The Lady is fascinated by the industriousness of humanity. You can combine your artifice with her blessing for great effect. " +
                        "I also want you to have this, my old friend's pan, which he called 'the source of goodness'. Many a fish stew was mastered in the source pan, and may it serve you well. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(mistsTwo.name, mistsTwo);

            // -----------------------------------------------------

            Quest mistsThree = new()
            {

                name = QuestHandle.mistsThree,

                icon = IconData.displays.mists,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Eight: Fishing",

                description = "I now have an advantage in the popular sport of fishing in the valley. The biggest and rarest specimens of the water answer to the authority of the Voice Beyond the Shore.",

                instruction = "Strike open water to create fishing spots. " +
                "Use a fishing rod to cast a line over the spot and wait for the fishing minigame to auto-trigger. " +
                "Attempt ten catches. Quest completion unlocks rarer fish.",

                progression = "catches attempted",

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "The valley waters glisten with the mystical properties of the sacred spring, and the creatures that swim within them answer to the Voice Beyond the Shore. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(mistsThree.name, mistsThree);

            // -----------------------------------------------------

            Quest relicMists = new()
            {

                name = QuestHandle.relicMists,

                type = Quest.questTypes.relics,

                icon = IconData.displays.mists,

                // -----------------------------------------------

                give = Quest.questGivers.none,

                trigger = true,

                triggerLocation = "CommunityCenter",

                triggerTime = 0,

                triggerRite = Rite.rites.mists,

                origin = new Vector2(40, 9) * 64,

                // -----------------------------------------------

                title = "The Avalant",

                description = "I have gathered all the major components of the Avalant, an ancient navigation device used for passage to a city drowned within the abyssal trench. " +
                "I do not have the means to explore the trench, but I could use the components to spruce up the disused fishtank in the community center.",

                instruction = "Perform a Rite of Mists at the fish tank in the community center.",

                explanation = "Through artifice bolstered by the power of the Lady Beyond the Shore, the repairs have been completed to the derelict fish tank. Then the forest spirits replaced it with a better model.",

            };

            quests.Add(relicMists.name, relicMists);

            // -----------------------------------------------------

            Quest mistsFour = new()
            {

                name = QuestHandle.mistsFour,

                icon = IconData.displays.mists,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Nine: Smite",

                description = "I now have an answer for some of the more terrifying threats I've encountered in my adventures. Bolts of lightning strike at my foes.",

                instruction = "Smite enemies with Rite of Mists twenty times. Unlocks critical hits.",

                progression = "enemies hit",

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "Your connection to the plane beyond broadens. Call upon the Lady's Voice to destroy your foes. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(mistsFour.name, mistsFour);


            // =====================================================
            // QUEST EFFIGY

            Quest questEffigy = new()
            {

                name = QuestHandle.questEffigy,

                type = Quest.questTypes.heart,

                icon = IconData.displays.effigy,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = "Beach",

                triggerTime = 0,

                triggerRite = Rite.rites.none,

                origin = new Vector2(12f, 13f) * 64,

                // -----------------------------------------------

                title = "At The Beach",

                description = "The Effigy feels a strange yearning for the shoreline. Whether for nostalgia, or for simple leisure, he is determined to visit the local beach, and I have agreed to accompany him.",

                instruction = "Go to the beach with 5-6 hours to spare and perform any rite at the marker to trigger the quest. " +
                    "Observe and talk to the Effigy as they go about various activities to learn more about their past.",

                explanation = "I watched the Effigy undertake many of the recreational activities once enjoyed by the first farmer. " +
                        "After several instances involving fish, slime and displays of raw, unbridled power, the Effigy used a veil of mists to conjure a vision of the past. " +
                        "The construct itself is a testament to the ingenuity of the Lady Beyond the Shore and her human acolyte, the first farmer. " +
                        "After they had completed their project, and taught the Effigy many things, the Lady deemed it an appropriate time to depart the valley. ",

                progression = null,

                requirement = 0,

                reward = 1000,

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "I feel drawn to the shore, to the enduring sands and tidal pools once walked by the first farmer. " +
                        "Though it is not related to your studies in Druidry, I think it would be of benefit for you to accompany me. " +
                        "(New quest received)",

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "I think I am starting to see the past with a clearer, wiser perspective. Thank you for sharing this moment with me.",
                    }
                },

            };

            quests.Add(questEffigy.name, questEffigy);


            // =====================================================
            // MISTS CHALLENGE

            Quest challengeMists = new()
            {

                name = QuestHandle.challengeMists,

                type = Quest.questTypes.challenge,

                icon = IconData.displays.mists,

                // -----------------------------------------------

                //give = Quest.questGivers.none,
                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = "Town",

                //triggerTime = 0,
                triggerTime = 1900,

                triggerRite = Rite.rites.mists,

                origin = new Vector2(47f, 88f) * 64,

                // -----------------------------------------------

                title = "The Shadow Invasion",

                description = "When I first discovered The Effigy, he had hidden himself in the farmcave after a chance encounter with a shadowfolk raider. " +
                "He feels more confident about confronting the invaders, and has requested that we reconnoitre the graveyard for signs of their operation.",

                instruction = "Perform a Rite of Mists in Pelican Town's graveyard between 7:00 pm and Midnight.",

                explanation = "My use of the power of the Mists gained the attention of a small group of the shadow invaders. " +
                "One of the marauders had a cannon, and in the interest of town peace and property, I decided it would be prudent to confiscate such a destructive weapon. " +
                "Fortunately for the cannon-bearer the shadowfolk's captain arrived in time to coordinate a hasty retreat. " +
                "The town residents noticed only enough of the commotion to believe that I had chased away some vandals. They commended my efforts.",

                details = new()
                {
                    "My victory in the graveyard will ensure the safety of most of the townsfolk: Alex, Elliott, Harvey, Emily, Penny, Caroline, Clint, Evelyn, George, Gus, Jodi, Lewis, Pam, Pierre, Vincent.",
                },

                progression = null,

                requirement = 0,

                reward = 3000,

                replay = "Friendship with town residents",

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = //"This might be an appropriate time to discuss the circumstances that led to my encasement in the roof of the farm cave. " +
                        //"As you're aware, I came out of stasis some time ago, shortly after your grandparent passed away. " +
                        //"The energies of the Weald were disturbed by a group of shadowfolk, and I was roused in order to investigate. " +
                        //"By happenstance I encountered their captain on the banks of the river south of the village cemetery. " +
                        //"This rogue expressed a disconcerting interest in my 'make', but I was able to waylay him on the chase home through the brambled paths of the farm. " +
                        //"I secreted myself away, partly out of self-preservation, partly so to wait until the new custodian of the farm arrived. And now you are here. " +
                        //"I ask that we reconnoitre site of their operation to gauge the mercenary numbers and location. " +
                        //"For this, I would be indebted to your kindness. " +
                        "Successor, someone must confront the shadows that stalk the sacred spaces, and where I have stumbled in this matter, you may yet succeed. " +
                        "Shortly after your grandparent's passing, the energies of the Weald roused me from a long stasis. " +
                        "They bid me to investigate signs of intrusion in the secret woods, and that is where I encountered the captain of a band of shadowfolk marauders. " +
                        "This rogue expressed a disconcerting interest in my 'make', but I was able to waylay him on the chase home through the brambled paths of the farm. " +
                        "I secreted myself away in the farmcave, as I was not confident in my abilities to confront the invaders. " +
                        "Now you are here, and the energies entreat us to investigate the town cemetary. " + 
                        "(New quest received)",

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = //"So they were acting on behalf of the Deep one. Hmmm. " +
                        //"Lord Deep was an elderborn noble who fell into obscurity in the aftermath of the war. " +
                        //"Or so my mentors told me. It appears he has gathered a small measure of power and resources. " +
                        "Whomever the Deep one is, his interest in the valley has caused no small measure of trouble. " +
                        "I wonder if his presence here relates to the re-emergence of the Jelly fiend, and the desecration of the aquifer. " +
                        "He might send more forces to the surface to disturb the sacred places. We must be vigilant.",
                    }
                },

            };

            quests.Add(challengeMists.name, challengeMists);


            // =====================================================
            // SWORD STARS

            Quest swordStars = new()
            {

                name = QuestHandle.swordStars,

                type = Quest.questTypes.sword,

                icon = IconData.displays.revenant,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = Location.LocationData.druid_chapel_name,

                triggerTime = 0,

                triggerRite = Rite.rites.stars,

                origin = new Vector2(27, 16) * 64,

                // -----------------------------------------------

                title = "The Star Chapel",

                description = "The Effigy recommends that I apprentice myself to a master of starcraft, that I might gain power enough to protect the sacred Spaces against the Deep one.",

                instruction = "The Star Chapel can be accessed by using the Guardian Lantern in the relics journal while on floor sixty (level 60) of the local mines.",

                explanation = "Like all the powers granted to the Druids of the Valley, the light of the celestials comes with the burden of service. " +
                        "The search for the Stars' missing kin aligns with my greater purpose to preserve the tenuous balance between the realms.",

                progression = null,

                requirement = 0,

                reward = 250,

                details = new(),

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "By my assessment, your present abilities will not be enough to overcome the comparable forces of the Deep One. I know of only one more blessing available to us, the power of the celestials. " +
                        "It is dangerous, difficult to wield, and incongrous with the harmonies of the Weald and Mists, but I believe in your potential. " +
                        "You must undertake a pilgrimage to a chapel carved into the high side of the mountain, and there you will find the last Holy Warrior of the Guardians of the Star. " +
                        "The way will be revealed to you by the light of this lantern. Show it to the warrior, and he will know your purpose. " +
                        "I have not visited him for a long, long time. Not since the church of bats was founded, as they are irritated by my presence, and I do not wish to disturb them, or anyone. " +
                        "(New quest received)",

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "You have an aura of divine favour.",
                        responses = new()
                        {
                            "The revenant of the last Guardian of the Star has agreed to teach me starcraft.",
                            "How long will it take for me to become as ancient and awkward as you and your friends?",
                        },
                        answers = new()
                        {
                            "The revenant has waited a long time for a resolution to his plight, something I have been unable to give. Now our hopes are vested with you. ",
                        }

                    }
                },

            };

            quests.Add(swordStars.name, swordStars);

            // =====================================================
            // STARS LESSONS

            Quest starsOne = new()
            {

                name = QuestHandle.starsOne,

                icon = IconData.displays.stars,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Ten: Meteor Rain",

                description = "The appearance of comets in the night sky would inpire the divinations of druid astronomers.",

                instruction = "Hit monsters twenty times with Rite of the Stars: Meteor Rain. Mastery will provide a small chance for an additional meteor fall per cast.",

                progression = "monsters hit",

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = "So you want to call down the wrath of the heavens. Well farmer, its pretty simple. You pray and hope that the starfire will fall where you want it to. " +
                        "It's the celestials that will answer your call, for they have committed themselves to the work of our warrior order, and our work is far from finished. " +
                        "The rite can be quite taxing, so I say make yourself a couple of tonics using this old still here. It's in pretty good knick, only had to wipe the guano off every few seasons. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(starsOne.name, starsOne);

            // -----------------------------------------------------

            Quest starsTwo = new()
            {

                name = QuestHandle.starsTwo,

                icon = IconData.displays.stars,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Eleven: Gravity Well",

                description = "The lights of the heavens share their home with the vast and ravenous dark, the progenitor of the void.",

                instruction = "Channel Rite of the Stars to create ten gravity wells at the position of your cursor (set distance in facing direction on controllers). Harvests crops and forageables. Mastery will spawn a massive comet when Stars:Meteor Rain is cast within proximity to a well.",

                progression = "gravity wells created",

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = "Why do the stars shine so bright? Where does all that light go once it's touched us? It goes into the dark, farmer. The vast dark that grasps at the stars. " +
                        "The hungry dark that would feast on all the bliss and colour. That is the true nature of the void, the backdrop to the celestial realm. " +
                        "(New lesson received)",

                    },

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = "It would serve you well to ascend to the mountain chapel, to receive the last instructions from our guardian friend. " +
                        "I abandoned my own journey to master the blessings of the celestial realm. The powers of the voided space do not lend themselves to me. " +
                        "It is up to you to gain and safeguard all the traditions of our circle and allies.",

                    },

                },

            };

            quests.Add(starsTwo.name, starsTwo);


            // =====================================================
            // STARS CHALLENGE

            Quest challengeStars = new()
            {

                name = QuestHandle.challengeStars,

                type = Quest.questTypes.challenge,

                icon = IconData.displays.stars,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = "Forest",

                triggerTime = 0,

                triggerRite = Rite.rites.stars,

                origin = new Vector2(79f, 78f) * 64,

                // -----------------------------------------------

                title = "The Slime Infestation",

                description = "Throughout my adventures I've engaged many a slime in combat. " +
                    "Unlike the bats and shadowfolk, I am uncertain of their origin or master, but while I spent time obtaining the blessing of the stars, a grand splattering of slime infested the forest. " +
                    "The Effigy suspects the Jellyking, his old nemesis, has finally made a play for the valley's natural splendour.",

                instruction = "Perform a Rite of the Stars in the clearing east of arrowhead island in Cindersap Forest.",

                explanation = "The pumpkin visaged king of the slimes mocked my lieges for leaving the valley wasted and unguarded. " +
                    "But the circle of druids has been reborn in the valley, and the wrath of heavens destroyed all hope the Jellyking had of establishing his slimedom in the forest.",

                details = new() { "My victory in the forest has helped some of the villagers return to it's glens: Shane, Leah, Haley, Marnie, Jas, The Wizard, Willy.", },

                progression = null,

                requirement = 0,

                reward = 5000,

                replay = "Friendship with forest and beach residents",

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "Another trial presents itself. The southern forest reeks of our mortal enemy, the ravenous jelly, and I am certain their king leads the wobbling host. " +
                            "Rain judgement upon them with the blessing of the Stars. Prove your worth as a leader of the Guardians of the Stars and the Circle of Druids. " +
                            "(New quest received)",

                    }
                },

                after = new()
                {
                    
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "A splendid end to a longstanding feud. For a long time, the valley farmers would wake to find their prize pumpkins mutilated, burnt, trodden or smashed to many pieces, with cursewords written with pulverised innards. " +
                        "Well, you might think the perpetrator was the Jellyking or some other fiend. No, all along, it was me. My petty rage is something I had to learn to restrain, to bury for the good of the circle. " +
                        "Now one of the sources of my anger has been removed for good. I thank you.",
                    },

                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = "The bishop of bats mentioned to me, in passing, that you managed a great victory in the forest.",
                    },

                },

            };

            quests.Add(challengeStars.name, challengeStars);

            // =====================================================
            // ATOLL CHALLENGE

            Quest challengeAtoll = new()
            {

                name = QuestHandle.challengeAtoll,

                type = Quest.questTypes.challenge,

                icon = IconData.displays.mists,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = LocationData.druid_atoll_name,

                triggerTime = 0,

                triggerRite = Rite.rites.mists,

                origin = new Vector2(28, 10f) * 64,

                // -----------------------------------------------

                title = "The Lost Seafarers",

                description = "Local folklore suggests that spectres of drowned seafarers haunt the eastern beaches on days where the Lady's storms and squalls rock the shoreline. " +
                "The wisps of the mists are sensitive to the restless spirits of the drowned, and their whispers entreat the Effigy to investigate the Atoll for such disturbances.",

                instruction = "Perform a Rite of the Mists on the Atoll (accessible from the dinghy on the far eastern side of the Beach).",

                explanation = "The seafarers claim to have been drowned by the Lady herself, and that their passage to the otherworld is barred by the judgement of the Fates.",

                details = new(),

                progression = null,

                requirement = 0,

                reward = 5000,

                replay = "Rain Totem",

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "Successor, consider the wisps that gathered to us at the atoll. They were unsettled, and concerned with issues of the past. " +
                        "After listening to their many whispers, I believe I have surmised the source of their discontent. " +
                        "The drowned haunt the shores of the valley, successor, and the wisps cannot abide it. " +
                        "(New Quest Received)",

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "The whispers of the wisps dissipate, but the vendetta of the phantom captain troubles me, especially the implication that the Fates have refused the phantoms egress to the afterlife. " +
                        "We must be wary of the Fae Court's involvement in the affairs of the valley.",
                    },
                },

            };

            quests.Add(challengeAtoll.name, challengeAtoll);

            // =====================================================
            // DRAGON CHALLENGE

            Quest challengeDragon = new()
            {

                name = QuestHandle.challengeDragon,

                type = Quest.questTypes.challenge,

                icon = IconData.displays.stars,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = LocationData.druid_vault_name,

                triggerTime = 0,

                origin = new Vector2(27, 15) * 64,

                // -----------------------------------------------

                title = "The Terror Beneath",

                description = "The Revenant says that an ancient evil stalks the lava caverns deep beneath the rock. A long time ago, a guardian-druid like myself went to confront the creature and never returned.",

                instruction = "Access the lair of the terror by using the Luminous Water of the Sacred Spring relic in the journal on level 100 of the mines.",

                explanation = "The terror beneath the mountain was a lesser Dragon in service to the Reaper of Fate. I brought the beast low and found the lava-tinged relics of the missing Star Guardian.",

                details = new(),

                progression = null,

                requirement = 0,

                reward = 5000,

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = "So you are collecting the runestones of the old Circle. I know where one might be, if you're ready for a bit of... danger. " +
                        "One of our order actually participated in the formation of the circle of druids. He had the talent for the powers, so to speak, kind of like you do, and carried a runestone around with him. " +
                        "He undertook a mission to confront a terror deep within the mountains and never returned. I think the Fates had a hand in his disappearance, " +
                        "another punishment inflicted upon our guardian order for our role in the old war. If you can find his remains, you might find the old runestone. " +
                        "(New Quest Received)",

                    },

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = "By releasing several souls from the tethers that bound them to this material plane, you have surely defied the judgments of the Fates. " +
                            "Our undying friend once told me a tragic story of one of the founding members of circle, who was also a stalwart guardian of the stars. " +
                            "The Revanant believes the Fae court played a role in the tragedy. I entreat you to attend the chapel and hear the tale yourself.",


                    },

                },

                replay = "Prismatic Shard",

                after = new()
                {
                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = "Hail to you, dragon slayer. Judging by the fact you have his relics, I'd say the old champion of our order has passed on. " +
                        "I was his squire, so it is a small comfort to know he has not been bound by the Fates. " +
                        "You get the title of Knight Guardian, though there's not much point in a ceremony for it, unless you want to host a congregation of winged rodents.",
                    },
                },

            };

            quests.Add(challengeDragon.name, challengeDragon);


            // =====================================================
            // APPROACH JESTER

            Quest approachJester = new()
            {

                name = QuestHandle.approachJester,

                type = Quest.questTypes.approach,

                icon = IconData.displays.jester,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = false,

                triggerLocation = "Mountain",

                triggerTime = 0,

                origin = new Vector2(90, 27) * 64,

                // -----------------------------------------------

                title = "Fate Jests",

                description = "My work with the power of the Stars has caught the attention of the Fates, the primeval beings that serve under the creator Yoba. " +
                "Long ago, the Fae Court issued a warrant to arrest and trial the Fallen Star, the very one that was a catalyst for the ancient war between elderborn and dragons. " +
                "The Reaper of Fate tried in vain to administer the arrest, but did not succeed, and even after an age has passed, the charges still stand. " +
                "As the bearer of the sacred duty of the Star Guardians, though I have never met the Fallen Star, and have no clue as to their whereabouts, I am obliged to meet an envoy of the Fates to discuss the matter.",

                instruction = "Meet the envoy of the Fates near the bridge by the adventurer's guild.",

                explanation = "I was surprised by the nature of the envoy of fate sent to treaty with me. The Jester of Fate appears to be interested in the unknown plight of his predecessor, " +
                "the fabled Reaper, who has not reported to the Fae Court since he was tasked with finding the Fallen Star." +
                "Jester has only one lead, an old valley legend about a den of death on the eastward side of the mountain, beyond the ravine and abandoned quarry. " +
                "Jester needs the services of someone with 'earth sense', and so a deal was struck, that I might learn some mysteries of the Fates in exchange for my services.",

                details = new(),

                progression = null,

                requirement = 0,

                reward = 1000,

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = "Well, I knew this fateful day would come. The wing-mother of the bats came to see me this morning, and this time I managed to make some sense of what she was screeching about. " +
                        "It seems the Fates once again cast the gaze of judgment on our lowly order, as an envoy from the Fae Court has been out on the slopes, scaring all the bats, asking to see you, 'the star bringer'. " +
                        "They wait for you at the ravine, near the site where the Star-born made ground-fall all those years ago. " +
                        "Apparently that place has been mined down to the bare bones of the mountain, when the valleyfolk had a keen eye for star-touched ores. " +
                        "Anyhow, remember to speak well of me when you do find the representative of our Lady of Fortune. " +
                        "(New Quest Received)",

                    },
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = "I witnessed the predatory motions of a Molossus Gigantus in the farmcave earlier today. Then I realised it was a messenger, for you. The Revenant wishes to discuss a matter of importance.",
                    },

                },

                after = new()
                {
                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = "Now that I've heard from you what this Jester chap is like, the description given to me by the wing mother makes a lot more sense. " +
                        "The way she said it you would think a ferocious tiger was teleporting around, sniffing at every nook a bat might seek shelter in, whining like a kitten whenever such a bat soiled itself and flew away.",
                    },
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "The Fae Court sent a mere low-ranking courtier to entreat with you, and not even of their nobility, but one of their anthropomorphic entertainers. It seems almost farcical. I'm unsure what to make of this occurrence.",
                    },
                },

            };

            quests.Add(approachJester.name, approachJester);

            // =====================================================
            // SWORD (SCYTHE) FATES

            Quest swordFates = new()
            {

                name = QuestHandle.swordFates,

                type = Quest.questTypes.sword,

                icon = IconData.displays.fates,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = "UndergroundMine77377",

                triggerTime = 0,

                triggerRite = Rite.rites.none,

                origin = new Vector2(27, 99) * 64,

                // -----------------------------------------------

                title = "The Reaper's Trail",

                description = "The Jester of Fate has been tasked by the Fae Court to investigate the disappearance of one of their agents, the Reaper, who was last seen on his mission to detain the Fallen Star. " +
                    "Jester believes that his clues to his kin's whereabouts lie within a death-infested dungeon to the east. As per our agreement, I am obliged to escort him.",

                instruction = "Travel to the quarry tunnel entrance and perform a rite at the quest marker to summon Jester to explore with you.",

                explanation = "We entered the dungeon, and immediately faced a host of disturbed spectres, all bearing traces of the Reaper's power. Our pursuers chased us all the way to a statue of the reaper himself at a dead-end. " +
                    "Jester recognised some of the signs marked onto the surrounding walls and managed to trigger the mechanism for a secret exit.We found ourselves in a cavern furnished with monuments to strange entities. " +
                    "It appears we have discovered the forgotten court of Fates and Chaos, established to administer the fallen dominion of the Two Kings in the aftermath of the War for the Star.",

                progression = null,

                requirement = 0,

                reward = 2000,

                details = new(),

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = "Summon me when you get inside the quarry tunnel, and we can combine the awesome power of our noses. With one cosmic cat snout and one big human honker at our disposal, we'll sniff out my kinsman in no time! " +
                        "(New quest received)",

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = "Those monuments were not entirely like the real thing but I guess if you're a human artist who's never glimpsed the divine, you do what you can."

                    }
                },

            };

            quests.Add(swordFates.name, swordFates);


            // =====================================================
            // FATES LESSONS

            Quest fatesOne = new()
            {

                name = QuestHandle.fatesOne,

                icon = IconData.displays.fates,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Twelve: Whisk",

                description = "The Fates do not constrain themselves to the physical laws of this world.",

                instruction = "Cast Rite of the Fates to fire a warp projectile, then warp along its path by pressing the rite button (again) or action button before the projectile expires. Complete twenty times.",

                progression = "times whisked away",

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = "Here farmer, take this stress-relieving device. Now you can see like a Warble, now you can move like a warble! " +
                        "It's fun, but also a bit, well, (Jester grins). I'm sure you'll get used to it. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(fatesOne.name, fatesOne);

            Quest fatesTwo = new()
            {

                name = QuestHandle.fatesTwo,

                icon = IconData.displays.fates,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Thirteen: Curses",

                description = "The fane creatures of this world cannot understand their own mundane purpose.",

                instruction = "Fates: Whisk, Charged attacks and Jester attacks apply curse effects to monsters. " +
                "Activating Fates: Whisk in the vicinity of cursed monsters triggers a warp strike in addition to the normal movement. " +
                "Curses including Polymorph, Pickpocket, Dazzle, Doom and Instant-Death. Curse twenty monsters to unlock warpstrike mastery.",

                progression = "monsters cursed",

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = "(Jester sighs) I'm having a hard time getting on with the monsters of this plane. " +
                        "I reach out to them, to try to open their mind to the mysteries of Fate, but they are never the same afterwards. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(fatesTwo.name, fatesTwo);

            Quest fatesThree = new()
            {

                name = QuestHandle.fatesThree,

                icon = IconData.displays.fates,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Fourteen: Tricks!",

                description = "Jester has a nuanced sense of humour. I hope that humour translates well in the benign magical mysteries I will peform for the townsfolk.",

                instruction = "Amuse or annoy five villagers with tricks produced by Rite of the Fates. Uses cursor and directional targetting. Quest completion enables more tricks.",

                progression = "villagers tricked",

                requirement = 5,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = "(Jester's eyes sparkle) Magic tricks! Fates are known for being the best at making others happy. Or soaked. Try it out yourself, and then we'll go on the town! " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(fatesThree.name, fatesThree);

            // =====================================================
            // QUEST JESTER

            Quest questJester = new()
            {

                name = QuestHandle.questJester,

                type = Quest.questTypes.heart,

                icon = IconData.displays.jester,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = "Town",

                triggerTime = 1200,

                triggerRite = Rite.rites.none,

                origin = new Vector2(29f, 56f) * 64,

                // -----------------------------------------------

                title = "Jesters In The Night",

                description = "Jester thinks a distraction from our regular routine will be good for morale, and has invited me to spend the evening in the bustling Pelican Town.",

                instruction = "Go to Pelican Town after 12 noon, with 6 - 7 hours to spare, and perform any rite at the marker outside the clinic to trigger the quest. " +
                "Observe and talk to the Jester of Fate as they go about various activities to learn more about their mission.",

                explanation = 
                        "Jester joined me in the town square and immediately took to sniffery and prepositions. Marlon showed up at one point and reminded Jester of the tough days out on the mountain, looking for the fallen one's crash site. " +
                        "Marlon asked Jester to deliver a pre-catastrophe fossil specimen to Gunther. Then Jester recognised a familiar scent, and followed it to find one of his old associates, the Buffoonette of Chaos, who raced him to the museum. " +
                        "While Gunther gave a cursory assessment of the fossil, it suddenly sprang to life! " +
                        "Once the monster was dealt with, Buffin and Jester argued over whether he should return to his prior position at the court of the fates, and the drama escalated to the point of arson. " +
                        "Jester has the conviction to prove himself, and after finding strength in the bonds formed between us members of the circle of druids, and that's enough for him to continue.",

                reward = 5000,

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = "You know what Farmer, I think it's best to practice your tricks on live subjects, and the town is a lively place! Lets go get up to some mischief and forget about our troubles for a while.",

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = "Did we even get to trick anyone? I guess I wasn't really in the mood for pranks.",
                    },
                    [CharacterHandle.characters.Buffin] = new()
                    {
                        prompt = true,
                        intro = "I'll always remember the joy of seeing an ancient reptile smash through that funnily dressed man's palace of books.",
                    },

                },

            };

            quests.Add(questJester.name, questJester);

            // =====================================================
            // FATES FINAL LESSONS
            
            Quest fatesFour = new()
            {

                name = QuestHandle.fatesFour,

                icon = IconData.displays.buffin,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson Fifteen: Enchant",

                description = "Even a machine can answer to a higher purpose.",

                instruction = "Successfully channelling Rite of the Fates (hold rite button for 2-3 seconds) will apply a randomised input to nearby farm machines. " +
                "Each application consumes one Faeth, a new brewable resource available in the herbalism journal. Enchant ten farm machines with Faeth.",

                progression = "machines enchanted",

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = "So I checked out your 'machines'. Well built but... kind of predictable, I think. What happens if you put something from the otherworld in them? " +
                        "At court we trade and serve in vintages of Faeth, which is easy to make, if you can get the right stuff from the celestial and material realms.",
                        responses = new()
                        {
                            "What ingredients and equipment do I require to produce this alchemical mystery?",
                            "Jester, leave my things alone. I don't want any more explosions or pink spotted rabbits."
                        },
                        answers = new()
                        {
                            "(Jester smiles) It just so happens that we have an agent of Chaos to parlay with. I've been meeting Buffin at odd moments in the hidden court cave. " +
                            "You can get there from the eastern side of the waterfall in town.",
                        },
                        questContext = 1
                    },
                    [CharacterHandle.characters.Buffin] = new()
                    {
                        prompt = true,
                        intro = "Jester thinks you're competent enough to handle Faeth. I like to imagine how the Artisans will react to the sight of a mortal in possession of their sacred liquor. " +
                        "Of course you'll need this trinket to contain the melted essence. Remember to visit me again tomorrow! I would very much like to know the results. " +
                        "(New lesson received)",
                    },

                },

            };

            quests.Add(fatesFour.name, fatesFour);

            // =====================================================
            // FATES CHALLENGE

            Quest challengeFates = new()
            {

                name = QuestHandle.challengeFates,

                icon = IconData.displays.fates,

                type = Quest.questTypes.challenge,

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = LocationData.druid_court_name,

                triggerTime = 1700,

                triggerRite = Rite.rites.none,

                origin = new Vector2(30, 20) * 64,

                title = "The Court of Shadows",

                description = "One late afternoon in the Court of Fates and Chaos, whilst Buffin and Jester chatted about the latest intrigues of the Fae court, " +
                "and the light began to dwindle over the great monuments, they spied a solitary pair of pale eyes in the burgeoning shadows.",

                instruction = "Cast Rite of the Fates after 5pm at the trigger marker within the Court of Fates and Chaos",

                explanation =
                        "The Circle of Druids gathered within the Court of Fates and Chaos and waited for the light to dim. " +
                        "The Shadowfolk mercenary company I drove from the town a couple of weeks ago emerged en-masse from the tunnel system that adjoins the cavern. " +
                        "It was a brutal melee, as the shadowfolk's voided nature made them resilient to the blessed weapons and powers of my allied forces. " +
                        "Yet the mercenaries did not have the conviction to see the fight through, and abandoned their leader to face the reckoning of the circle. " +
                        "The mercenary captain introduced himself as Shadowtin Bear, treasure hunter and aspirant historian. " +
                        "The shadowfolk goals in the valley appear to be parallel to Jester's, and my companion suggested that Shadowtin should redeem himself as our ally.",

                reward = 10000,

                replay = "Iridium Sprinkler",

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = "There's something nice about that old cave with the big statues, but lately it's felt a bit, uh, unhomely. " +
                        "Like it's not a special place for an important envoy of the Fates to receive visitors from court. " +
                        "(Jester scrunches his face in thought) Buffin would be able to explain better than me."
                    },
                    [CharacterHandle.characters.Buffin] = new()
                    {
                        prompt = true,
                        intro = "Farmer! Beware of... the spawn of the void! Creatures of the darkened plane, lurking somewhere nearby. " +
                        "Rally your allies, for you must challenge the shadowy foe for dominion of the cave and my honour. " +
                        "(Buffin grins) I hear they have a techno-punk aesthetic, and that will be cool to see square off against your, uh, country style. " +
                        "(New challenge quest received)"
                    },

                },

                after = new()
                {

                    [CharacterHandle.characters.Buffin] = new()
                    {
                        prompt = true,
                        intro = "I am very pleased with what transpired here. " +
                        "During the performance, I sensed the great Stream of Chaos linger over the battlefield, and I perceived an imminent shift, a change of fortune, allegiance, or treachery. " +
                        "There's a reason the unexpected happens, farmer.",
                    },

                },

            };

            quests.Add(challengeFates.name, challengeFates);


            // =====================================================
            // SWORD ETHER (CUTLASS)

            Quest swordEther= new()
            {

                name = QuestHandle.swordEther,

                type = Quest.questTypes.sword,

                icon = IconData.displays.ether,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = LocationData.druid_tomb_name,

                triggerTime = 0,

                triggerRite = Rite.rites.none,

                origin = new Vector2(27, 15) * 64,

                // -----------------------------------------------

                title = "The Tomb of Tyrannus",

                description = "Shadowtin Bear has divulged the intentions of Lord Deep and his shadowy allies. " +
                "They hunt for the power of the ancient race of Dragons, who were diminished in their vain war against the Elderborn for the Fallen Star. " +
                "The best lead is the Tomb of Tyrannus Jin, once master of the shamans of Calico, and the Revenant has provided the means to reach it.",

                instruction = "Travel to the entrance of the Skull Cavern in Calico Desert, and use the Ceremonial Lantern in the relics journal to access the Tomb.",

                explanation = "We were set upon by the wraith of Thanatoshi, who had been trapped within the sealed tomb at the culmination of his mad quest to find the missing Starborn. " +
                "Though the reaper's mind had deteriorated with the burden of unfulfilled purpose, he continued to tether himself to this world by the power of a Tyrant dragon tooth, which has been fashioned into a weapon of ethereal might.",

                progression = null,

                requirement = 0,

                reward = 10000,

                replay = "Faeth x20",

                details = new(),

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = "I confess, this partnership with the mercenary captain infuriates me. " +
                        "If it was my decision, he would be stripped of his ill-gotten trinkets and sent him back to Lord Deep in shame, as a warning to those that would disturb the sanctity of the valley. " +
                        "Perhaps that is merely vindictiveness on my part. I have no recourse for this... anger I feel. " +
                        "I have little comprehension of the events described in the text the captain provided, but perhaps our revenant friend would be able to assist. " +
                        "He is more clear-headed, if only because his skull is empty.",


                    },
                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = "So farmer, how goes your adventures with the new envoy of Fates?",
                        responses = new()
                        {
                            "I've got a journal from a former Guardian of the Star, and has a few details about what happened when the old war ended.",
                            "I'm often asked if I have a Large Cat permit, and Jester often interjects 'Large Cat? Where?'",
                        },
                        answers = new(){
                            "Ah. so all this time, that annal was hiding in the haunted halls of the followers of the Reaper. Those guys were a bunch of crazies. " +
                            "They styled the Reaper of Fate as the great avenger of humanity, and made sure the veterans of the war couldn't live in peace. " +
                            "That's one of the reasons why your Lady Beyond left for the Isle. Once there were no more Elderborn around, the crazies started to point fingers at the circle, at those that revere the Old Monarchs and the Stars. " +
                            "So good Knight Wyrven left to confront them. Shortly afterwards, the Reaper attacked us openly. I was cursed, and all our secrets were laid bare, including the location of the Tyrant of Calico's tomb. " +
                            "Now, this lamp here is one of the few things that remains of ancient Calico culture. You'll find the tomb in the cavern of skulls in the desert waste. " +
                            "This guiding lamp is yours, but it comes with a warning, too. The place was given the skull moniker for good reason. "+
                            "(New quest received)",
                        },

                    },

                },

                after = new()
                {
                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = "You have vanquished the Reaper, but my curse remains. (Revenant turns to the chapel altar) That can only mean, the duty I have yet to fulfil. But when, when will I be free to pass on. Only the oracles know."
                    },
                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = "(Jester's eyes have turned a sullen green) I want to go home... Buffin was right."
                    },
                },

            };

            quests.Add(swordEther.name, swordEther);

            // =====================================================
            // ETHER LESSONS


            Quest relicFates = new()
            {

                name = QuestHandle.relicFates,

                type = Quest.questTypes.relics,

                icon = IconData.displays.fates,

                // -----------------------------------------------

                give = Quest.questGivers.none,

                trigger = true,

                triggerLocation = "CommunityCenter",

                triggerTime = 0,

                triggerRite = Rite.rites.fates,

                origin = new Vector2(45, 12) * 64,

                // -----------------------------------------------

                title = "Keepsake Boxes",

                description = "There are many memories plastered on the walls and bulletin board of the community center, and all of them will have a home in the keepsake boxes left behind by the Fates that once served this very community.",

                instruction = "Perform a Rite of the Fates at the bulletin board in the community center.",

                explanation = "As I placed the last wall-dried photograph away, I sensed a spell of goodwill had just spouted and spread over the community. Then I raced after a forest spirit who tried to put the keepsake boxes in storage.",

            };

            quests.Add(relicFates.name, relicFates);


            Quest etherOne = new()
            {

                name = QuestHandle.etherOne,

                icon = IconData.displays.ether,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson 16: Dragon Form",

                description = "I can feel the streams of ether rushing softly under my finger tips, and then my wing tips as I allow them to lift me into the sky.",

                instruction = "The Rite of Ether transforms you into a Dragon! Press and hold the action button while moving to do a sweeping flight, release to land. Practice flight ten times to increase flight range.",

                progression = "flights attempted",

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        intro = "The shamans of the Calico civilisation practiced the ancient art of transfiguration. " +
                        "They liked to become large cats, mostly, in deference to their dragon overlord. " +
                        "It seems you have an affinity for the technique, as you have acquired it by proxy, even without studying this Dragonomicon. " +
                        "(New lesson received)",

                    },

                },

            };

            quests.Add(etherOne.name, etherOne);

            Quest etherTwo = new()
            {

                name = QuestHandle.etherTwo,

                icon = IconData.displays.ether,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson 17: Dragon Breath",

                description = "I draw the ether in, then expel it as a torrent of violent energy.",

                instruction = "Perform twenty firebreath attacks with the special button / right click. Uses directional targetting. Quest completion enables monster immolation.",

                progression = "firebreath attacks",

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        intro = "Ether is extremely volatile when compelled into a material state. In the darkened realm, we employ it's use in all manner of combustion powered machinery. " +
                        "The light produced is gentle to our eyes. Be careful with your channelling, and don't practice near any incendiaries! " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(etherTwo.name, etherTwo);

            Quest etherThree = new()
            {

                name = QuestHandle.etherThree,

                icon = IconData.displays.ether,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson 18: Dragon Dive",

                description = "The ether flows everywhere, through the thickened forest, the rugged mountain, and the frigid depths.",

                instruction = "Fly onto the water and perform ten dives with the special button/right click.",

                progression = "dive attempts",

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        intro = "You might find this very unsettling, as I did when I learned of it's existence. Are you aware, that there is a strange technique that can be learned by some landborne creatures to stay alive in water. It's called swimming. It's unnatural. " +
                        "(New lesson received)",

                    }
                },

            };

            quests.Add(etherThree.name, etherThree);


            // =====================================================
            // QUEST SHADOWTIN

            Quest questShadowtin = new()
            {

                name = QuestHandle.questShadowtin,

                type = Quest.questTypes.heart,

                icon = IconData.displays.shadowtin,

                // -----------------------------------------------

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = "Forest",

                triggerTime = 0,

                triggerRite = Rite.rites.none,

                origin = new Vector2(60f, 20f) * 64,

                // -----------------------------------------------

                title = "The Shadow Scholar",

                description = "Shadowtin has busied himself with the measurement of streams of ether manipulated by my transformative powers. " +
                "This research has led him to a nexus of streams within Cindersap Forest, a remote bunker secured by a lock-system of dwarven design. " +
                "Shadowtin has managed to source an access key, and would like me to be present when he investigates the secrets of the bunker.",

                instruction = "Meet Shadowtin Bear outside Marnie's Ranch in Cindersap Forest with 5 - 6 hours to spare. Perform any rite at the marker to trigger the quest.",

                explanation = "Shadowtin managed to open the door to the nexus bunker with an accesskey from the local dwarf merchant. Inside was a large ether-fuelled machine. " +
                "Incidentally, the place was booby-trapped with a contraption that turned Shadowtin into a panicked feline. " +
                "The local wizard discovered the ruckus and demanded a duel to test our worthiness of the secrets of the machine, a shrine engine that powers the Valley warp network. " +
                "I defeated the wizard, and he revealed that he had discovered a specification for the machine in a recently unearthed cache nearby. " +
                "We investigated the site, only to find the hollowed skull of a pumpkin demon, with a note from a past Druid of the Circle, but nothing else to further Shadowtin's cause. " +
                "Shadowtin's former gang approached, with the dwarf as hostage. " +
                "Shadowtin traded a map of ether-currents for the dwarf's safety, a valuable exchange, as the map indicates the locations of other ether-sealed caches littered throughout the valley. " +
                "The race to uncover the buried secrets of the Circle of Druids begins.",

                reward = 5000,

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        intro = "Archdruid. I noticed how your transformation has an odd effect on the currents of ether that flow through the valley. " +
                        "I never thought to study them before, but with your inspiration, I have managed to make a crude chart. " +
                        "(Shadowtin presents an articulate map of ether flows with intricate signs and labels) " +
                        "It appears the currents converge at a nexus, at this point in the nearby Forest. " +
                        "There's a concealed bunker there, secured with a lock-system of dwarven origin, but I have managed to source an access key from one of my mercenary contacts." +
                        "I doubt the truths I seek lay inside, but regardless, I think it would be beneficial to the circle to be present for the discovery. " +
                        "Though I would recommend leaving our friends at home, as they might scare my contact. (New quest recieved)",

                    },
                    [CharacterHandle.characters.Jester] = new()
                    { 
                        prompt = true,
                        questContext = 1,
                        intro = "Metalface is planning for an adventure, and I was excited about it, but when I asked him where we were going he said somewhere that doesn't allow pets. " +
                        "I said that Fates aren't allowed to keep pets either, then he seemed to get annoyed. Maybe you can talk to him and clear up this whole misunderstanding.",
                    
                    },

                },

                after = new()
                {


                },

            };

            quests.Add(questShadowtin.name, questShadowtin);

            Quest etherFour = new()
            {

                name = QuestHandle.etherFour,

                icon = IconData.displays.ether,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = "Lesson 19: Dragon Treasure",

                description = "The ancient ones hoarded their immeasurable treasures in vaults of the ethereal realm, where only they could enjoy them.",

                instruction = "Claim 5 dragon treasures. Search for the ether symbol on these large map locations: Cindersap Forest, Beach, Mountain, Desert, Island-West, Atoll and Bug-Land.  " +
                "Move over the spot and either dig or dive (special/right click button) to claim the dragon treasure. Be careful, you might have to fight to keep the treasure contents. Mastery spawns dig spots in mineshafts.",

                progression = "treasures claimed",

                requirement = 5,

                reward = 1000,

                before = new()
                {
                    
                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = "Woodface showed me how to use my nose to track the scent of prey. Then he told me to hunt vermin like a good farmcat. " +
                        "Am I a just a joke to him? Because I like jokes. Anyway, I tracked a different kind of pest, Shadowtin's old gang! " +
                        "They have been out searching for dragon troves, using what looks to me like an ancient treasure map. We should ask our friend how to beat his pals to the prizes. " +
                        "(Shadowtin has a new lesson)",

                    },

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        intro = 

                        "About the map. I hope to make amends for its loss by offering you this ether-pressure gauge, which should help you locate more nodes of concentrated ether. " +
                        "I suspect that a lot of the hidden troves contain treasures sealed by Dragons and their servants, " +
                        "who would have taught the technique to your forebearers, so your bound to find more than just books and weird skulls." +
                        "The gauge might also help with some of your more audacious alchemical experiments. " +
                        "(New lesson received)",

                    },


                },

            };

            quests.Add(etherFour.name, etherFour);

            Quest relicEther = new()
            {

                name = QuestHandle.relicEther,

                type = Quest.questTypes.relics,

                icon = IconData.displays.ether,

                // -----------------------------------------------

                give = Quest.questGivers.none,

                trigger = true,

                triggerLocation = "CommunityCenter",

                triggerTime = 0,

                triggerRite = Rite.rites.ether,

                origin = new Vector2(16, 7) * 64,

                // -----------------------------------------------

                title = "Antiquated Recipes",

                description = "Fearing zealots, the Lady Beyond stashed away several important texts, including testimonies of the war for the fallen star, and the efforts of the first circle to restore the Weald. " +
                "Though I have recovered these records, there is not much that would make sense to the community of today, save for some cooking and gardening instructions. I'll add them to the cooking digests in the community pantry. ",

                instruction = "Perform a Rite of the Ether in the pantry in the community center.",

                explanation = "I added the ether-smudged book of recipes, tips and poetry to the other digests in the disused pantry. " +
                "It felt right, like a piece missing from the community's collection had finally been restored. Then all the shelves collapsed and I had to call the forest spirit repair service.",

            };

            quests.Add(relicEther.name, relicEther);

            // =====================================================
            // Challenge Ether

            Quest challengeEther = new()
            {

                name = QuestHandle.challengeEther,

                icon = IconData.displays.ether,

                type = Quest.questTypes.challenge,

                give = Quest.questGivers.dialogue,

                trigger = true,

                triggerLocation = LocationData.druid_gate_name,

                triggerRite = Rite.rites.none,

                origin = new Vector2(28, 9) * 64,

                title = "The Sealed Gate",

                description = "The Effigy suggests that I explore the forlorn woodland estate of the Two Kings, a place frequently mentioned in the texts cached by the Lady Beyond. " +
                "The estate interior is secured by an ancient gate, brandished in iconography associated with the Mother of Crows, " +
                "the venerable Artisan of the Fates, the first patron of the Circle.",

                instruction = "Cast any rite at the Sealed Gate, accessible from the path in the northern part of the Druid's Grove.",

                explanation =
                        "I prayed before the ancient gate, and entreated the Crowmother for her blessing, but it was not a Fate that answered, but a fiend.",


                reward = 10000,

                replay = "Rare Seed x2",

                before = new()
                {

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = "Archdruid. You've done well to gather so many texts and relics from the Circle's past. " +
                        "It seems we have assembled all the pieces of a shattered picture, but I struggle to see how it all fits back together. " +
                        "The cat and fox seem more interested in playing an adolescent game called shadows and searchlights. I always end up being the shadow. " +
                        "The Effigy is familiar with one detail from our investigation. The 'Mother of Crows'. Something about that name has stirred the scarecrow. " +
                        "(The Effigy has a new quest)",

                    },
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = "Successor, though I think the personal endeavours of the Envoy and Scholar should be of no concern to the circle, it, well, it pleases me to provide counsel where I can. ",
                        responses = new()
                        {

                            "I'm glad to hear you want to help our friends. The fellowship of the members of our circle is it's own gift, as powerful as any blessing from our patrons.",
                            "You're going to talk about the first farmer again, aren't you.",

                        },
                        answers = new()
                        {
                            "Within the ruined estate of the sleeping monarchs, deep in the woodlands, stands a gate designed by the Mother of Crows, with a mural that venerates her careful work. " +
                            "The first time I visited the estate, it was already a field of scorched rubble, and the crowmother had long since returned to the Fae Court. " +
                            "The first farmer and other Druids held the opinion that the place should be left destitute and never mentioned again. " +
                            "From the testimonies in texts you recovered, I have gained a better understanding of their prejudice towards the place, as many desired to forget the terrors of the war and the fall of the elder-kingdom. " +
                            "Now its time for the circle to reclaim the seat of the Weald. " +
                            "The path lies under the brambled walls of the woodlands north of the sacred grove."

                        }

                    },
                },


            };

            quests.Add(challengeEther.name, challengeEther);

            return quests;

        }

    }
    public class Quest
    {

        public enum questTypes
        {
            none,
            approach,
            sword,
            challenge,
            lesson,
            heart,
            relics,
            miscellaneous
        }

        public enum questGivers
        {
            none,
            dialogue,
            chain,
        }

        public string name;

        public questTypes type = questTypes.none;

        public questGivers give = questGivers.none;

        public Vector2 origin = Vector2.Zero;

        // -----------------------------------------------
        // trigger

        public bool trigger;

        public string triggerLocation;

        public int triggerTime;

        public Rite.rites triggerRite = Rite.rites.none;

        // -----------------------------------------------
        // journal

        public string title;

        public IconData.displays icon = IconData.displays.none;

        public string description;

        public string instruction;

        public string explanation;

        public List<string> details = new();

        public string progression;

        public int requirement;

        public int reward;

        public string replay;

        // -----------------------------------------------
        // dialogues

        public Dictionary<CharacterHandle.characters, Dialogue.DialogueSpecial> before = new();

        public Dictionary<CharacterHandle.characters, Dialogue.DialogueSpecial> after = new();

    }

    public class QuestProgress
    {

        public int status;

        public int progress;

        public int replay;

        public int delay;

        public QuestProgress()
        {

        }

        public QuestProgress(int Status, int Delay = 0, int Progress = 0, int Replay = 0)
        {

            status = Status;

            progress = Progress;

            replay = Replay;

            delay = Delay;

        }

    }
}
