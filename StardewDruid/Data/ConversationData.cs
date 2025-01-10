using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Dialogue;
using StardewDruid.Event;
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

                            questContext = 400,

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


                case QuestHandle.questRevenant:
                    conversations = new()
                    {

                        [1] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.342.1.164"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.167"),
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.168"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.172") +
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
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.183"),
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.184"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.188") +
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
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.199"),
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.200"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.204") +
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
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.216"),
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.217"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.221") +
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
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.240"),
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.241"),
                            },

                            companion = 1,

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.342.1.246") +
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
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.88"),
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.89"),
                          },

                            answers = new()
                          {
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.93") +
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
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.106"),
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.107"),
                          },

                            answers = new()
                          {
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.111") +
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
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.123"),
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.124"),
                          },

                            answers = new()
                          {
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.128") +
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
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.139"),
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.140"),
                          },

                            answers = new()
                          {
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.144") +
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.145"),
                          },

                            questContext = 400,

                        },


                        [5] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.343.1.151"),

                            responses = new()
                          {
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.154"),
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.155"),
                          },

                            answers = new()
                          {
                              Mod.instance.Helper.Translation.Get("ConversationData.343.1.159") +
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
                                Mod.instance.Helper.Translation.Get("ConversationData.361.9"),
                                Mod.instance.Helper.Translation.Get("ConversationData.361.10"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.361.14"),
                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.361.20"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.361.23"),
                                Mod.instance.Helper.Translation.Get("ConversationData.361.24"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.361.28") +
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
                                Mod.instance.Helper.Translation.Get("ConversationData.361.44"),
                                Mod.instance.Helper.Translation.Get("ConversationData.361.45"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.361.49"),
                            },

                            questContext = 100,

                        },

                        [2] = new()
                        {

                            intro = Mod.instance.Helper.Translation.Get("ConversationData.361.55"),

                            responses = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.361.58"),
                                Mod.instance.Helper.Translation.Get("ConversationData.361.59"),
                            },

                            answers = new()
                            {
                                Mod.instance.Helper.Translation.Get("ConversationData.361.63") +
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

                                Mod.instance.Helper.Translation.Get("ConversationData.361.76")

                            },

                            answers = new()
                            {

                                Mod.instance.Helper.Translation.Get("ConversationData.361.80"),

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
