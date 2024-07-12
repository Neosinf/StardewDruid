
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

                title = Mod.instance.Helper.Translation.Get("Quest.approachEffigy.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.approachEffigy.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.approachEffigy.instruction").Tokens(new{button=Mod.instance.Config.riteButtons.ToString()}),

                explanation = Mod.instance.Helper.Translation.Get("Quest.approachEffigy.explanation"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.swordWeald.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.swordWeald.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.swordWeald.instruction").Tokens(new{button=Mod.instance.Config.riteButtons.ToString()}),

                explanation = Mod.instance.Helper.Translation.Get("Quest.swordWeald.explanation"),

                progression = null,

                requirement = 0,

                reward = 250,

                details = new(),

                // -----------------------------------------------

                before = new() {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordWeald.before.Effigy.intro"),

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordWeald.after.Effigy.intro"),
                        responses = new()
                        {
                            Mod.instance.Helper.Translation.Get("Quest.swordWeald.after.Effigy.responses.0"),
                            Mod.instance.Helper.Translation.Get("Quest.swordWeald.after.Effigy.responses.1"),
                            Mod.instance.Helper.Translation.Get("Quest.swordWeald.after.Effigy.responses.2"),
                        },
                        answers = new(){
                            Mod.instance.Helper.Translation.Get("Quest.swordWeald.after.Effigy.answers.0"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.herbalism.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.herbalism.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.herbalism.instruction"),

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.herbalism.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.wealdOne.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.wealdOne.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.wealdOne.instruction").Tokens(new{button=Mod.instance.Config.effectsButtons.ToString()}),

                progression = Mod.instance.Helper.Translation.Get("Quest.wealdOne.progression"),

                requirement = 100,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.wealdOne.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.wealdTwo.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.wealdTwo.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.wealdTwo.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.wealdTwo.progression"),

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.wealdTwo.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.wealdThree.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.wealdThree.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.wealdThree.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.wealdThree.progression"),

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.wealdThree.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.wealdFour.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.wealdFour.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.wealdFour.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.wealdFour.progression"),

                requirement = 5,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.wealdFour.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.wealdFive.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.wealdFive.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.wealdFive.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.wealdFive.progression"),

                requirement = 100,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.wealdFive.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.challengeWeald.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.challengeWeald.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.challengeWeald.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.challengeWeald.explanation"),

                progression = null,

                requirement = 0,

                reward = 2000,

                replay = Mod.instance.Helper.Translation.Get("Quest.challengeWeald.replay"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Quest.challengeWeald.details.0"),
                },

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeWeald.before.Effigy.intro"),

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeWeald.after.Effigy.intro"),
                        responses = new()
                        {
                            Mod.instance.Helper.Translation.Get("Quest.challengeWeald.after.Effigy.responses.0"),
                            Mod.instance.Helper.Translation.Get("Quest.challengeWeald.after.Effigy.responses.1"),
                        },
                        answers = new(){
                            Mod.instance.Helper.Translation.Get("Quest.challengeWeald.after.Effigy.answers.0"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.relicWeald.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.relicWeald.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.relicWeald.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.relicWeald.explanation"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.swordMists.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.swordMists.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.swordMists.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.swordMists.explanation"),

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
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordMists.before.Effigy.intro"),

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordMists.after.Effigy.intro"),
                        responses = new()
                        {
                            Mod.instance.Helper.Translation.Get("Quest.swordMists.after.Effigy.responses.0"),
                            Mod.instance.Helper.Translation.Get("Quest.swordMists.after.Effigy.responses.1"),
                        },
                        answers = new()
                        {
                            Mod.instance.Helper.Translation.Get("Quest.swordMists.after.Effigy.answers.0"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.mistsOne.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.mistsOne.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.mistsOne.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.mistsOne.progression"),

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.mistsOne.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.mistsTwo.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.mistsTwo.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.mistsTwo.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.mistsTwo.progression"),

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.mistsTwo.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.mistsThree.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.mistsThree.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.mistsThree.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.mistsThree.progression"),

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.mistsThree.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.relicMists.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.relicMists.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.relicMists.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.relicMists.explanation"),

            };

            quests.Add(relicMists.name, relicMists);

            // -----------------------------------------------------

            Quest mistsFour = new()
            {

                name = QuestHandle.mistsFour,

                icon = IconData.displays.mists,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = Mod.instance.Helper.Translation.Get("Quest.mistsFour.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.mistsFour.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.mistsFour.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.mistsFour.progression"),

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.mistsFour.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.questEffigy.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.questEffigy.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.questEffigy.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.questEffigy.explanation"),

                progression = null,

                requirement = 0,

                reward = 1000,

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.questEffigy.before.Effigy.intro"),

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.questEffigy.after.Effigy.intro"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.challengeMists.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.challengeMists.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.challengeMists.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.challengeMists.explanation"),

                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Quest.challengeMists.details.0"),
                },

                progression = null,

                requirement = 0,

                reward = 3000,

                replay = Mod.instance.Helper.Translation.Get("Quest.challengeMists.replay"),

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
                        Mod.instance.Helper.Translation.Get("Quest.challengeMists.before.Effigy.intro"),

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
                        Mod.instance.Helper.Translation.Get("Quest.challengeMists.after.Effigy.intro"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.swordStars.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.swordStars.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.swordStars.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.swordStars.explanation"),

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
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordStars.before.Effigy.intro"),

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordStars.after.Effigy.intro"),
                        responses = new()
                        {
                            Mod.instance.Helper.Translation.Get("Quest.swordStars.after.Effigy.responses.0"),
                            Mod.instance.Helper.Translation.Get("Quest.swordStars.after.Effigy.responses.0"),
                        },
                        answers = new()
                        {
                            Mod.instance.Helper.Translation.Get("Quest.swordStars.after.Effigy.answers.0"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.starsOne.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.starsOne.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.starsOne.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.starsOne.progression"),

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.starsOne.before.Revenant.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.starsTwo.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.starsTwo.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.starsTwo.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.starsTwo.progression"),

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.starsTwo.before.Revenant.intro"),

                    },

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = Mod.instance.Helper.Translation.Get("Quest.starsTwo.before.Effigy.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.challengeStars.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.challengeStars.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.challengeStars.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.challengeStars.explanation"),

                details = new() { Mod.instance.Helper.Translation.Get("Quest.challengeStars.details.0"), },

                progression = null,

                requirement = 0,

                reward = 5000,

                replay = Mod.instance.Helper.Translation.Get("Quest.challengeStars.replay"),

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeStars.before.Effigy.intro"),

                    }
                },

                after = new()
                {
                    
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeStars.after.Effigy.intro"),
                    },

                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeStars.after.Revenant.intro"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.challengeAtoll.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.challengeAtoll.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.challengeAtoll.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.challengeAtoll.explanation"),

                details = new(),

                progression = null,

                requirement = 0,

                reward = 5000,

                replay = Mod.instance.Helper.Translation.Get("Quest.challengeAtoll.replay"),

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeAtoll.before.Effigy.intro"),

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeAtoll.after.Effigy.intro"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.challengeDragon.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.challengeDragon.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.challengeDragon.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.challengeDragon.explanation"),

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
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeDragon.before.Revenant.intro"),

                    },

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeDragon.before.Effigy.intro"),


                    },

                },

                replay = Mod.instance.Helper.Translation.Get("Quest.challengeDragon.replay"),

                after = new()
                {
                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeDragon.after.Revenant.intro"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.approachJester.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.approachJester.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.approachJester.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.approachJester.explanation"),

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
                        intro = Mod.instance.Helper.Translation.Get("Quest.approachJester.before.Revenant.intro"),

                    },
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = Mod.instance.Helper.Translation.Get("Quest.approachJester.before.Effigy.intro"),
                    },

                },

                after = new()
                {
                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.approachJester.after.Revenant.intro"),
                    },
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.approachJester.after.Effigy.intro"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.swordFates.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.swordFates.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.swordFates.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.swordFates.explanation"),

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
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordFates.before.Jester.intro"),

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordFates.after.Jester.intro")

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

                title = Mod.instance.Helper.Translation.Get("Quest.fatesOne.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.fatesOne.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.fatesOne.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.fatesOne.progression"),

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.fatesOne.before.Jester.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.fatesTwo.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.fatesTwo.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.fatesTwo.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.fatesTwo.progression"),

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.fatesTwo.before.Jester.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.fatesThree.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.fatesThree.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.fatesThree.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.fatesThree.progression"),

                requirement = 5,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.fatesThree.before.Jester.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.questJester.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.questJester.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.questJester.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.questJester.explanation"),

                reward = 5000,

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.questJester.before.Jester.intro"),

                    }
                },

                after = new()
                {
                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.questJester.after.Jester.intro"),
                    },
                    [CharacterHandle.characters.Buffin] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.questJester.after.Buffin.intro"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.fatesFour.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.fatesFour.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.fatesFour.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.fatesFour.progression"),

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.fatesFour.before.Jester.intro"),
                        responses = new()
                        {
                            Mod.instance.Helper.Translation.Get("Quest.fatesFour.before.Jester.responses.0"),
                            Mod.instance.Helper.Translation.Get("Quest.fatesFour.before.Jester.responses.1"),
                        },
                        answers = new()
                        {
                            Mod.instance.Helper.Translation.Get("Quest.fatesFour.before.Jester.answers.0"),
                        },
                        questContext = 1
                    },
                    [CharacterHandle.characters.Buffin] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.fatesFour.before.Buffin.intro"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.challengeFates.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.challengeFates.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.challengeFates.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.challengeFates.explanation"),

                reward = 10000,

                replay = Mod.instance.Helper.Translation.Get("Quest.challengeFates.replay"),

                before = new()
                {

                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeFates.before.Jester.intro")
                    },
                    [CharacterHandle.characters.Buffin] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeFates.before.Buffin.intro")
                    },

                },

                after = new()
                {

                    [CharacterHandle.characters.Buffin] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeFates.after.Buffin.intro"),
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

                title = Mod.instance.Helper.Translation.Get("Quest.swordEther.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.swordEther.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.swordEther.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.swordEther.explanation"),

                progression = null,

                requirement = 0,

                reward = 10000,

                replay = Mod.instance.Helper.Translation.Get("Quest.swordEther.replay"),

                details = new(),

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordEther.before.Effigy.intro"),


                    },
                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordEther.before.Revenant.intro"),
                        responses = new()
                        {
                            Mod.instance.Helper.Translation.Get("Quest.swordEther.before.Revenant.responses.0"),
                            Mod.instance.Helper.Translation.Get("Quest.swordEther.before.Revenant.responses.0"),
                        },
                        answers = new(){
                            Mod.instance.Helper.Translation.Get("Quest.swordEther.before.Revenant.answers.0"),
                        },

                    },

                },

                after = new()
                {
                    [CharacterHandle.characters.Revenant] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordEther.after.Revenant.intro")
                    },
                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.swordEther.after.Jester.intro")
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

                title = Mod.instance.Helper.Translation.Get("Quest.relicFates.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.relicFates.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.relicFates.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.relicFates.explanation"),

            };

            quests.Add(relicFates.name, relicFates);


            Quest etherOne = new()
            {

                name = QuestHandle.etherOne,

                icon = IconData.displays.ether,

                type = Quest.questTypes.lesson,

                give = Quest.questGivers.dialogue,

                title = Mod.instance.Helper.Translation.Get("Quest.etherOne.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.etherOne.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.etherOne.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.etherOne.progression"),

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.etherOne.before.Shadowtin.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.etherTwo.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.etherTwo.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.etherTwo.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.etherTwo.progression"),

                requirement = 20,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.etherTwo.before.Shadowtin.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.etherThree.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.etherThree.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.etherThree.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.etherThree.progression"),

                requirement = 10,

                reward = 1000,

                before = new()
                {

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.etherThree.before.Shadowtin.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.questShadowtin.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.questShadowtin.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.questShadowtin.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.questShadowtin.explanation"),

                reward = 5000,

                // -----------------------------------------------

                before = new()
                {

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.questShadowtin.before.Shadowtin.intro"),

                    },
                    [CharacterHandle.characters.Jester] = new()
                    { 
                        prompt = true,
                        questContext = 1,
                        intro = Mod.instance.Helper.Translation.Get("Quest.questShadowtin.before.Jester.intro"),
                    
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

                title = Mod.instance.Helper.Translation.Get("Quest.etherFour.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.etherFour.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.etherFour.instruction"),

                progression = Mod.instance.Helper.Translation.Get("Quest.etherFour.progression"),

                requirement = 5,

                reward = 1000,

                before = new()
                {
                    
                    [CharacterHandle.characters.Jester] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = Mod.instance.Helper.Translation.Get("Quest.etherFour.before.Jester.intro"),

                    },

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.etherFour.before.Shadowtin.intro"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.relicEther.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.relicEther.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.relicEther.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.relicEther.explanation"),

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

                title = Mod.instance.Helper.Translation.Get("Quest.challengeEther.title"),

                description = Mod.instance.Helper.Translation.Get("Quest.challengeEther.description"),

                instruction = Mod.instance.Helper.Translation.Get("Quest.challengeEther.instruction"),

                explanation = Mod.instance.Helper.Translation.Get("Quest.challengeEther.explanation"),


                reward = 10000,

                replay = Mod.instance.Helper.Translation.Get("Quest.challengeEther.replay"),

                before = new()
                {

                    [CharacterHandle.characters.Shadowtin] = new()
                    {
                        prompt = true,
                        questContext = 1,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeEther.before.Shadowtin.intro"),

                    },
                    [CharacterHandle.characters.Effigy] = new()
                    {
                        prompt = true,
                        intro = Mod.instance.Helper.Translation.Get("Quest.challengeEther.before.Effigy.intro"),
                        responses = new()
                        {

                            Mod.instance.Helper.Translation.Get("Quest.challengeEther.before.Effigy.responses.0"),
                            Mod.instance.Helper.Translation.Get("Quest.challengeEther.before.Effigy.responses.1"),

                        },
                        answers = new()
                        {
                            Mod.instance.Helper.Translation.Get("Quest.challengeEther.before.Effigy.answers.0"),

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
