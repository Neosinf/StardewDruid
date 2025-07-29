using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewValley;
using StardewValley.Companions;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Data
{
    public static class ConversationData
    {

        public static Dictionary<int, DialogueSpecial> SceneConversations(string scene)
        {

            Dictionary<int, DialogueSpecial> conversations = new();

            // Effigy intro quest

            switch (scene)
            {

                case QuestHandle.approachEffigy:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            dialogueId = scene + 0.ToString(),

                            intro = "Stranger: So a successor appears. I am the Effigy, crafted by the First Farmer, and sustained by the old powers.",

                            responses = new()
                            {
                                [0] = "Greetings Effigy. I inherited this plot of land from my grandfather. Amongst his things was an old waystone that guided me here.",
                                [1] = "Grandpa's notes didn't say anything about a magic scarecrow.",
                                [2] = "(Say nothing)"
                            },

                            answers = new()
                            {
                                [0] = "The last time I roamed the furrowed hills of this farm, your grandfather was a young farmer. " +
                                "I'm sorry for his passing. He could not see the hidden paths as you do. Only someone with an affinity for the Weald would have found the waystone.",
                                [1] = "Scarecrow? If only I possessed the means to frighten the feathered creatures away, for they are attracted to the nest-like qualities of my headdress. " +
                                "In regards to magic, if you are a seeker of the arcane, I have a power even more ancient to reveal to you.",
                                [2] = "You do not need to introduce yourself, I know exactly why you have come."
                            },

                            choices = new()
                            {
                                [0] = DialogueSpecial.dialoguemanner.serious,
                                [1] = DialogueSpecial.dialoguemanner.lighthearted,
                                [2] = DialogueSpecial.dialoguemanner.taciturn,
                            },

                            updateScene = -1

                        },

                        [2] = new()
                        {

                            dialogueId = scene + 2.ToString(),

                            intro = "The Effigy: The first farmer to cultivate this land befriended those who are aligned with the otherworld. They formed a circle of Druids.",

                            responses = new()
                            {
                                [0] = "I've always felt a special connection to this place. I would like to follow in the footsteps of the first farmer and learn the traditions of the Circle.",
                                [1] = "Yes, great things happen when people stand in circles.",
                                [2] = "(nod thoughtfully)"
                            },

                            answers = new()
                            {
                                [0] = "A wise decision, as the ways of the Druid are harmonious with the nature of the valley. Meet me in the grove outside, and we will test your aptitude for the otherworld.",
                                [1] = "I am glad that you are already familiar with the ancient ways, as this familiarity will benefit you in the trials ahead. Meet me in the grove outside, and we can begin.",
                                [2] = "Follow me, we shall see if we can reform the Circle ourselves.",
                            },

                            choices = new()
                            {
                                [0] = DialogueSpecial.dialoguemanner.serious,
                                [1] = DialogueSpecial.dialoguemanner.lighthearted,
                                [2] = DialogueSpecial.dialoguemanner.taciturn,
                            },

                            updateScene = -1

                        },

                    };

                    break;

                case QuestHandle.squireWinds:

                    //"DialogueData.1671": "Sighs of the Earth: What say you, farmer?",
                    //"DialogueData.1675": "I seek the blessing of the Two Kings to reform the circle of Druids.",
                    // "DialogueData.1676": "Uh... is there someone hiding behind that rock?",
                    //"DialogueData.1687": "Whispers on the wind: The monarchs remain dormant, their realm untended. Who are you to claim the inheritance of the broken circle?",
                    //"DialogueData.1691": "The valley is my home now. I want to care for and protect it.",
                    //"DialogueData.1692": "A magic scarecrow told me that I'm special.",
                    //"DialogueData.1703": "Rustling in the Woodland: It is not an easy path, the one tread by a squire of the Two Kings. Are you ready to serve?",
                    //"DialogueData.1708": "I will serve the sleeping monarchs like the Druids of yore.",
                    //"DialogueData.1709": "Will I get a title or a fancy moniker?",
                    conversations = new()
                    {

                        [0] = new()
                        {

                            dialogueId = scene + 0.ToString(),

                            intro = "Sighs of the Earth: Why have you come here, traveller?",

                            responses = new()
                            {
                                [0] = "I seek the blessing of the Two Kings to reform the circle of Druids.",
                                [1] = "My dear rock ghosts, I am but a seeker of the magic of circles.",
                                [2] = "(allow the winds to pass over you)"
                            },

                            answers = new()
                            {
                                [0] = "Whispers on the wind: The monarchs remain dormant, their realm untended. The wilderness has claimed much of the Weald.",
                                [1] = "Sighs of the Earth: This pleases me. Though I'm not sure why.",
                                [2] = "(the energies of the weald bristle past, softly chuckling)",
                            },

                            choices = new()
                            {
                                [0] = DialogueSpecial.dialoguemanner.serious,
                                [1] = DialogueSpecial.dialoguemanner.lighthearted,
                                [2] = DialogueSpecial.dialoguemanner.taciturn,
                            },

                            updateScene = -1

                        },

                        [1] = new()
                        {

                            dialogueId = scene + 1.ToString(),

                            intro = "Whispers on the wind: Who are you to claim the inheritance of the broken circle ?",

                            responses = new()
                            {
                                [0] = "The valley is my home now. I want to care for and protect it.",
                                [1] = "A magic scarecrow told me that I'm special.",
                                [2] = "(hold the waystone up above you)"

                            },

                            answers = new()
                            {
                                [0] = "Whispers on the wind: The Weald yearns for a gentle hand to care for it and a strong hand to protect it. Hands like those of the Two Kings.",
                                [1] = "Sighs of the Earth: As was foretold, a successor blessed in the art of circle-making would walk these paths again.",
                                [2] = "(you hear gasps in the winds, and the excited chittering of the energies amongst the standing stones)",
                            },

                            choices = new()
                            {
                                [0] = DialogueSpecial.dialoguemanner.serious,
                                [1] = DialogueSpecial.dialoguemanner.lighthearted,
                                [2] = DialogueSpecial.dialoguemanner.taciturn,
                            },

                            updateScene = -1

                        },

                        [2] = new()
                        {

                            dialogueId = scene + 2.ToString(),

                            intro = "Rustling in the Woodland: It is not an easy path, the one tread by a squire of the Two Kings. Are you ready to serve?",

                            responses = new()
                            {

                                [0] = "I will faithfully serve the sleeping monarchs like the Druids of yore.",
                                [1] = "Will I get a title or a fancy moniker?",
                                [2] = "(nod)"

                            },

                            answers = new()
                            {
                                [0] = "Whispers on the wind: Arise, squire of the Two Kings. The winds of the Weald beckon to you.",
                                [1] = "Sighs of the Earth: If you prove yourself adept in the powers of the Weald, you might yet ascend to the esteemed rank of the Archdruid.",
                                [2] = "(the breeze intensifies through the standing stones, a sign of agreement and new possibilities)",
                            },

                            choices = new()
                            {
                                [0] = DialogueSpecial.dialoguemanner.serious,
                                [1] = DialogueSpecial.dialoguemanner.lighthearted,
                                [2] = DialogueSpecial.dialoguemanner.taciturn,
                            },

                            updateScene = -1

                        },

                        [3] = new()
                        {

                            companion = 0,

                            dialogueId = scene + 3.ToString(),

                            intro = "The Effigy: So you have entreated with the energies of the Weald, the old winds.",
             
                            responses = new()
                            {

                                [0] = "I have pledged myself to the sleeping monarchs, the Kings of Oak and Holly. The Circle is reformed.",
                                [1] = "I thought I heard something speak but maybe it was just the wind.",
                                [2] = "(shrug)"

                            },

                            answers = new()
                            {
                                [0] = "The energies of the Weald are unsettling, but their loyalty to the sleeping monarchs endures. You have gained powerful allies for the trials ahead of you.",
                                [1] = "Indeed it was, and you heard them clearly. I am already amazed at your talent, especially the ease with which you engage with the agents of the otherworld.",
                                [2] = "Until tomorrow then.",
                            },

                            choices = new()
                            {
                                [0] = DialogueSpecial.dialoguemanner.serious,
                                [1] = DialogueSpecial.dialoguemanner.lighthearted,
                                [2] = DialogueSpecial.dialoguemanner.taciturn,
                            },

                            updateScene = -1

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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1734"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.1735"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1746"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1750"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.1751"),

                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1762"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1766"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.1767"),

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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1792"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.377.1"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1797") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1798")

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1809"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1813"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.377.2"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1819") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1835"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.1836"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1841") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1860"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.377.3"),
                                //Mod.instance.Helper.Translation.Get("DialogueData.1861"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1866") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1867") +
                                Mod.instance.Helper.Translation.Get("DialogueData.1868")
                            },

                            questContext = 400,

                        },

                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1878"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1882"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.1883"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1888") +
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

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1906"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.1907"),

                            },

                            answers = new()
                            {

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1914") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1941"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.1942"),
                            },

                            answers = new()
                            {

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1948"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1959"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1963"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.1964"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1969"),
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.1979"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1983"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.1984"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.1989"),

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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2014"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2015"),
                            },

                            answers = new()
                            {

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2021") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2022"),

                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2033"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2037"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2038"),
                                [2] = Mod.instance.Helper.Translation.Get("DialogueData.2039"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2044"),
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2054"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2058"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2059"),
                                [2] = Mod.instance.Helper.Translation.Get("DialogueData.2079"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2064") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2065") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2066"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2064") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2065") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2066"),
                                [2] = Mod.instance.Helper.Translation.Get("DialogueData.2085") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2086") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2087"),
                            },

                            questContext = 300,

                        },

                    };

                    break;

                case QuestHandle.swordFates:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2101"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2105"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2106"),
                            },

                            answers = new()
                            {

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2112"),

                            },

                            questContext = 130,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2123"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2127"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2128"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2133"),
                            },

                            questContext = 140,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2143"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2147"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2148"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2153") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2178"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2179"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2184")

                            },

                            questContext = 99,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2195"),
                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2198"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2199"),

                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2205") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2225"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2226"),
                                [2] = Mod.instance.Helper.Translation.Get("DialogueData.2227"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2232") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2233")
                            },

                            questContext = 299,

                        },

                        [4] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2243"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2247"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2248"),
                                [2] = Mod.instance.Helper.Translation.Get("DialogueData.2249"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2254") +
                                Mod.instance.Helper.Translation.Get("DialogueData.2255")
                            },

                            questContext = 799,

                        },

                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.2265"),
                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2268"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2269"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2274") +
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

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2292"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2293"),

                            },

                            answers = new()
                            {

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2300") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2330"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2331"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2337") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2357"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2358"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2364") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2392"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2393").Tokens(new { farm = Game1.player.farmName.Value, }),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2400") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2417"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2418")
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2423") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2441"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2442"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2447") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2466"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2467"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2472") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2493"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2494"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2499") +
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

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2519"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2520"),

                            },

                            answers = new()
                            {

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2527") +
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

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2550"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.2551"),

                            },

                            answers = new()
                            {

                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.2558") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.315.2"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.315.3"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.315.4"),
                            },

                            questContext = 199,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.315.5"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.315.6"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.315.7")
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.315.8") +
                                Mod.instance.Helper.Translation.Get("DialogueData.315.9"),
                            },

                            questContext = 299,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.315.10"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.315.11"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.315.12"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.315.13") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.91"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.92"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.93"),
                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.324.94"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.95"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.96"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.97") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.102"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.103"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.104") +
                                Mod.instance.Helper.Translation.Get("DialogueData.324.105"),
                            },

                            questContext = 300,

                        },

                        [4] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.324.106"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.107"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.108"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.109") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.114"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.115"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.116"),
                            },

                            questContext = 500,

                        },

                        [6] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.324.117"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.118"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.119"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.120") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.87"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.88")
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.92") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.93"),
                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("DialogueData.339.1.99"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.102"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.103")
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.107") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.119"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.120")
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.124") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.135"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.136")
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.140") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.154"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.155")
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.159") +
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
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.170"),
                                [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.171")
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.175") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.176") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.177") +
                                Mod.instance.Helper.Translation.Get("DialogueData.339.1.178"),

                            },

                            questContext = 600,

                        },
                    };



                    break;


                case QuestHandle.questRevenant:
                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.342.1.164"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.167"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.168"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.172") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.173"),
                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            companion = 3,

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.342.1.180"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.183"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.184"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.188") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.189") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.190"),
                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.342.1.196"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.199"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.200"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.204") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.205") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.206"),
                            },

                            questContext = 300,

                        },

                        [4] = new()
                        {

                            companion = 3,

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.342.1.213"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.216"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.217"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.221") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.222") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.223") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.224") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.225") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.226") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.227") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.228") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.229") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.230") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.231"),
                            },

                            questContext = 400,

                        },


                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.342.1.237"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.240"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.241"),
                            },

                            companion = 1,

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.342.1.246") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.247") +
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.248"),
                            },

                            questContext = 500,

                        },

                    };


                    break;                
                
                case QuestHandle.challengeBones:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            companion = 1,

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.343.1.85"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.88"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.89"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.93") +
                                Mod.instance.Helper.Translation.Get("ConversationData.343.1.94") +
                                Mod.instance.Helper.Translation.Get("ConversationData.343.1.95") +
                                Mod.instance.Helper.Translation.Get("ConversationData.343.1.96") +
                                Mod.instance.Helper.Translation.Get("ConversationData.343.1.97"),
                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.343.1.103"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.106"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.107"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.111") +
                                Mod.instance.Helper.Translation.Get("ConversationData.343.1.112") +
                                Mod.instance.Helper.Translation.Get("ConversationData.343.1.113"),

                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            companion = 0,

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.343.1.120"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.123"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.124"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.128") +
                                Mod.instance.Helper.Translation.Get("ConversationData.343.1.129"),

                            },

                            questContext = 300,

                        },

                        [4] = new()
                        {

                            companion = 1,

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.343.1.136"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.139"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.140"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.144") +
                                Mod.instance.Helper.Translation.Get("ConversationData.343.1.145"),
                            },

                            questContext = 400,

                        },


                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.343.1.151"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.154"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.155"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.343.1.159") +
                                Mod.instance.Helper.Translation.Get("ConversationData.343.1.160"),
                            },

                            questContext = 500,

                        },


                    };

                    break;

                case QuestHandle.swordHeirs:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.361.6"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.361.9"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.361.10"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.361.14"),
                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.361.20"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.361.23"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.361.24"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.361.28") +
                                Mod.instance.Helper.Translation.Get("ConversationData.361.29") +
                                Mod.instance.Helper.Translation.Get("ConversationData.361.30"),

                            },

                            questContext = 200,

                        },


                    };

                    break;


                case QuestHandle.challengeMoors:

                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.361.41"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.361.44"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.361.45"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.361.49") +
                                Mod.instance.Helper.Translation.Get("ConversationData.385.1") +
                                Mod.instance.Helper.Translation.Get("ConversationData.385.2"),
                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.361.55"),

                            responses = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.361.58"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.361.59"),
                            },

                            answers = new()
                            {
                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.361.63") +
                                Mod.instance.Helper.Translation.Get("ConversationData.361.64") +
                                Mod.instance.Helper.Translation.Get("ConversationData.361.65") +
                                Mod.instance.Helper.Translation.Get("ConversationData.361.66") +
                                Mod.instance.Helper.Translation.Get("ConversationData.361.67"),

                            },

                            questContext = 200,

                        },

                        [3] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.361.73"),

                            responses = new()
                            {

                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.361.76"),
                                [1] = Mod.instance.Helper.Translation.Get("ConversationData.385.3"),
                            },

                            answers = new()
                            {

                                [0] = Mod.instance.Helper.Translation.Get("ConversationData.361.80") +
                                Mod.instance.Helper.Translation.Get("ConversationData.385.4"),

                            },

                            questContext = 300,

                        },

                    };

                    break;


            }

            return conversations;

        }

    }

}
