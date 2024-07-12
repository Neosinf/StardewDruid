using StardewDruid.Character;
using StardewValley.Locations;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StardewDruid.Character.CharacterHandle;
using StardewValley.Minigames;

namespace StardewDruid.Journal
{
    public class LoreData
    {

        public static string RequestLore(CharacterHandle.characters character)
        {

            switch (character)
            {
                default:
                case characters.Effigy:

                    return Mod.instance.Helper.Translation.Get("Lore.Request.Effigy");

                case characters.Jester:

                    return Mod.instance.Helper.Translation.Get("Lore.Request.Jester");

                case characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("Lore.Request.Revenant");

                case characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("Lore.Request.Buffin");

                case characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("Lore.Request.Shadowtin");

            }

        }

        public static string CharacterLore(CharacterHandle.characters character)
        {

            switch (character)
            {

                default:
                case characters.Effigy:

                    return Mod.instance.Helper.Translation.Get("Lore.Character.Effigy");

                case characters.Jester:

                    return Mod.instance.Helper.Translation.Get("Lore.Character.Jester");

                case characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("Lore.Character.Revenant");

                case characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("Lore.Character.Buffin");

                case characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("Lore.Character.Shadowtin");

            }

        }

        public enum stories
        {
            //------------------           
            
            Effigy_Weald,
            Effigy_self_1,

            //------------------

            Effigy_Mists,

            //------------------
            
            Effigy_self_2,

            //------------------

            Effigy_Stars,
            Revenant_Stars,
            Revenant_self_1,

            //------------------

            Effigy_Jester,
            Jester_Effigy,
            Jester_self_1,
            Jester_Fates,
            Revenant_Fates,

            //------------------

            Effigy_Buffin,
            Jester_Buffin,
            Revenant_court,
            Revenant_Marlon,

            //------------------

            Effigy_Shadowtin,
            Jester_Shadowtin,
            Shadowtin_Effigy,
            Shadowtin_Jester,
            Shadowtin_self_1,
            Buffin_court,

            //------------------

            Effigy_Ether,
            Jester_Tomb,
            Jester_Ether,
            Shadowtin_Ether,
            Revenant_Ether,
            Buffin_Ether,

            //------------------

            Effigy_Circle,
            Effigy_Circle_2,
            Jester_Circle,
            Revenant_Circle,
            Buffin_Circle,
            Shadowtin_Circle,

        }

        public static Dictionary<string,List<stories>> StorySets()
        {
            
            Dictionary<string, List<stories>> storySets = new()
            {

                [QuestHandle.swordWeald] = new(){
                    LoreData.stories.Effigy_Weald,
                    LoreData.stories.Effigy_self_1,
                },

                [QuestHandle.swordMists] = new(){
                    LoreData.stories.Effigy_Mists,
                },

                [QuestHandle.questEffigy] = new(){
                    LoreData.stories.Effigy_self_2,
                },

                [QuestHandle.swordStars] = new(){

                    LoreData.stories.Effigy_Stars,

                    LoreData.stories.Revenant_Stars,
                    LoreData.stories.Revenant_self_1,
                },

                [QuestHandle.approachJester] = new(){

                    LoreData.stories.Effigy_Jester,

                    LoreData.stories.Jester_Effigy,
                    LoreData.stories.Jester_self_1,
                    LoreData.stories.Jester_Fates,

                    LoreData.stories.Revenant_Fates,
                },

                [QuestHandle.questJester] = new(){

                    LoreData.stories.Effigy_Buffin,

                    LoreData.stories.Jester_Buffin,

                    LoreData.stories.Revenant_court,

                    LoreData.stories.Revenant_Marlon,


                },

                [QuestHandle.challengeEther] = new(){
                    
                    LoreData.stories.Effigy_Shadowtin,

                    LoreData.stories.Jester_Shadowtin,

                    LoreData.stories.Shadowtin_Effigy,
                    LoreData.stories.Shadowtin_Jester,
                    LoreData.stories.Shadowtin_self_1,

                    LoreData.stories.Buffin_court,

                },

                [QuestHandle.swordEther] = new(){


                    LoreData.stories.Effigy_Ether,

                    LoreData.stories.Jester_Tomb,
                    LoreData.stories.Jester_Ether,

                    LoreData.stories.Shadowtin_Ether,

                    LoreData.stories.Revenant_Ether,

                    LoreData.stories.Buffin_Ether,
                
                },

                [QuestHandle.questShadowtin] = new(){

                    LoreData.stories.Effigy_Circle,
                    LoreData.stories.Effigy_Circle_2,

                    LoreData.stories.Jester_Circle,
                    
                    LoreData.stories.Revenant_Circle,

                    LoreData.stories.Buffin_Circle,

                    LoreData.stories.Shadowtin_Circle,
                
                },

            };

            return storySets;

        }

        public static Dictionary<LoreData.stories,Lorestory> LoreList()
        {

            Dictionary<LoreData.stories, Lorestory> storylist = new();

            // ===========================================
            // Weald

            storylist[stories.Effigy_Weald] = new()
            {
                story = stories.Effigy_Weald,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_Weald.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_Weald.answer"),

            };

            storylist[stories.Effigy_self_1] = new()
            {
                story = stories.Effigy_self_1,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_self_1.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_self_1.answer"),

            };

            // ===========================================
            // Mists

            storylist[stories.Effigy_Mists] = new()
            {
                story = stories.Effigy_Mists,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_Mists.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_Mists.answer"),

            };

            // ===========================================
            // Effigy Quest

            storylist[stories.Effigy_self_2] = new()
            {
                story = stories.Effigy_self_2,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_self_2.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_self_2.answer"),

            };

            // ===========================================
            // Stars

            storylist[stories.Effigy_Stars] = new()
            {
                story = stories.Effigy_Stars,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_Stars.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_Stars.answer"),

            };

            storylist[stories.Revenant_Stars] = new()
            {
                story = stories.Revenant_Stars,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("Story.Revenant_Stars.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Revenant_Stars.answer"),

            };

            storylist[stories.Revenant_self_1] = new()
            {
                story = stories.Revenant_self_1,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("Story.Revenant_self_1.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Revenant_self_1.answer"),

            };

            // ===========================================
            // Fates

            storylist[stories.Effigy_Jester] = new()
            {
                story = stories.Effigy_Jester,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_Jester.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_Jester.answer"),

            };

            storylist[stories.Jester_Effigy] = new()
            {
                story = stories.Jester_Effigy,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("Story.Jester_Effigy.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Jester_Effigy.answer"),

            };

            storylist[stories.Jester_self_1] = new()
            {
                story = stories.Jester_self_1,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("Story.Jester_self_1.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Jester_self_1.answer"),

            };

            storylist[stories.Jester_Fates] = new()
            {
                story = stories.Jester_Fates,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("Story.Jester_Fates.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Jester_Fates.answer"),

            };

            storylist[stories.Revenant_Fates] = new()
            {
                story = stories.Revenant_Fates,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("Story.Revenant_Fates.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Revenant_Fates.answer"),

            };

            // ===========================================
            // Jester Quest

            storylist[stories.Effigy_Buffin] = new()
            {
                story = stories.Effigy_Buffin,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_Buffin.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_Buffin.answer"),
            };

            storylist[stories.Jester_Buffin] = new()
            {
                story = stories.Jester_Buffin,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("Story.Jester_Buffin.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Jester_Buffin.answer"),
 

            };

            storylist[stories.Revenant_court] = new()
            {
                story = stories.Revenant_court,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("Story.Revenant_court.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Revenant_court.answer"),

            };

            storylist[stories.Revenant_Marlon] = new()
            {
                story = stories.Revenant_Marlon,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("Story.Revenant_Marlon.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Revenant_Marlon.answer"),

            };

            // ===========================================
            // Shadowtin

            storylist[stories.Effigy_Shadowtin] = new()
            {
                story = stories.Effigy_Shadowtin,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_Shadowtin.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_Shadowtin.answer"),

            };

            storylist[stories.Jester_Shadowtin] = new()
            {
                story = stories.Jester_Shadowtin,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("Story.Jester_Shadowtin.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Jester_Shadowtin.answer"),

            };

            storylist[stories.Shadowtin_Effigy] = new()
            {
                story = stories.Shadowtin_Effigy,
                character = characters.Shadowtin,
                question = Mod.instance.Helper.Translation.Get("Story.Shadowtin_Effigy.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Shadowtin_Effigy.answer"),

            };

            storylist[stories.Shadowtin_Jester] = new()
            {
                story = stories.Shadowtin_Jester,
                character = characters.Shadowtin,
                question = Mod.instance.Helper.Translation.Get("Story.Shadowtin_Jester.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Shadowtin_Jester.answer"),

            };
/*          ?
            storylist[stories.Shadowtin_self_1] = new()
            {
                story = stories.Shadowtin_self_1,
                character = characters.Shadowtin,
                question = "How did your company come into the service of Lord Deep?",
                answer = "We're a band of professional looters. Which has been a wonderful career for me, as I'm fascinated by the elder age, and the cultural heritage of exotic cultures. " +
                "How my motivations diverged from my former associates is a consequence, I suppose, of my over-analytical personality. " +
                "The folklore of shadows is enscribed on the surface of the Great Shadow Vessel of our city. The narrative begins with the coalescence of shadows into sentient life by the cosmic power of Lord Deep. " +
                "But I have uncovered something to the contrary, a bizarre account that suggests those first enscriptions have been tampered with, and that Lord Deep is a fraud. " +
                "It's a dangerous opinion to have in my homeland, as we are a warring people, and he is a warlord of great measure, " +
                "but I have come to admire the cultures that we have battled and plundered, and I can't shake these dissident thoughts, that he has burdened and slowly corrupted my people. " +
                "I hope my travels and the treasures we uncover yield answers.",

            };


            storylist[stories.Shadowtin_self_1] = new()
            {
                story = stories.Shadowtin_self_1,
                character = characters.Shadowtin,
                question = "Some of your former band members mentioned a human agent",
                answer = "Emissaries from your plane visited the Lord Deep and established a partnership to re-acquire the dormant powers of the Ancient Ones. " +
                "There are many shadow mercenaries at work in the nations of your world, and this valley was my assignment. " +
                "The human contact I conferred with does not live here, and I am purposefully ignorant of their daylight identity. I do know they're wealthy. And they are disappointed in my progress. " +
                "I used more resources than they were prepared to allocate, but I believe that it was necessary to conduct a thorough investigation of the prospects here. " +
                "I suppose demotion could be considered my contract evaluation.",

            };
*/

            storylist[stories.Shadowtin_self_1] = new()
            {
                story = stories.Shadowtin_self_1,
                character = characters.Shadowtin,
                question = Mod.instance.Helper.Translation.Get("Story.Shadowtin_self_1.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Shadowtin_self_1.answer"),

            };


            storylist[stories.Buffin_court] = new()
            {
                story = stories.Buffin_court,
                character = characters.Buffin,
                question = Mod.instance.Helper.Translation.Get("Story.Buffin_court.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Buffin_court.answer"),

            };


            // ===========================================
            // Ether

            storylist[stories.Effigy_Ether] = new()
            {
                story = stories.Effigy_Ether,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_Ether.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_Ether.answer"),

            };

            storylist[stories.Jester_Tomb] = new()
            {
                story = stories.Jester_Tomb,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("Story.Jester_Tomb.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Jester_Tomb.answer"),

            };

            storylist[stories.Jester_Ether] = new()
            {
                story = stories.Jester_Ether,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("Story.Jester_Ether.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Jester_Ether.answer"),

            };

            storylist[stories.Shadowtin_Ether] = new()
            {
                story = stories.Shadowtin_Ether,
                character = characters.Shadowtin,
                question = Mod.instance.Helper.Translation.Get("Story.Shadowtin_Ether.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Shadowtin_Ether.answer"),

            };

            storylist[stories.Revenant_Ether] = new()
            {
                story = stories.Revenant_Ether,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("Story.Revenant_Ether.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Revenant_Ether.answer"),

            };

            storylist[stories.Buffin_Ether] = new()
            {
                story = stories.Buffin_Ether,
                character = characters.Buffin,
                question = Mod.instance.Helper.Translation.Get("Story.Buffin_Ether.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Buffin_Ether.answer"),

            };


            // ===========================================
            // Ether

            storylist[stories.Effigy_Circle] = new()
            {
                story = stories.Effigy_Circle,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_Circle.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_Circle.answer"),

            };


            storylist[stories.Effigy_Circle_2] = new()
            {
                story = stories.Effigy_Circle_2,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("Story.Effigy_Circle_2.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Effigy_Circle_2.answer"),

            };

            storylist[stories.Jester_Circle] = new()
            {
                story = stories.Jester_Ether,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("Story.Jester_Circle.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Jester_Circle.answer"),

            };

            storylist[stories.Revenant_Circle] = new()
            {
                story = stories.Revenant_Ether,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("Story.Revenant_Circle.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Revenant_Circle.answer"),

            };

            storylist[stories.Buffin_Circle] = new()
            {
                story = stories.Buffin_Ether,
                character = characters.Buffin,
                question = Mod.instance.Helper.Translation.Get("Story.Buffin_Circle.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Buffin_Circle.answer"),

            };

            storylist[stories.Shadowtin_Circle] = new()
            {
                story = stories.Buffin_Ether,
                character = characters.Buffin,
                question = Mod.instance.Helper.Translation.Get("Story.Shadowtin_Circle.question"),
                answer = Mod.instance.Helper.Translation.Get("Story.Shadowtin_Circle.answer"),
            };

            return storylist;

        }

    }

    public class Lorestory
    {

        public LoreData.stories story;

        public CharacterHandle.characters character;

        public string question;

        public string answer;

    }

}
