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

        public static string RequestLore(CharacterHandle.characters character)
        {

            switch (character)
            {
                default:
                case characters.Effigy:

                    return Mod.instance.Helper.Translation.Get("LoreData.10");

                case characters.Jester:

                    return Mod.instance.Helper.Translation.Get("LoreData.14");

                case characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("LoreData.18");

                case characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("LoreData.22");

                case characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("LoreData.26");

            }

        }

        public static string CharacterLore(CharacterHandle.characters character)
        {

            switch (character)
            {

                default:
                case characters.Effigy:

                    return Mod.instance.Helper.Translation.Get("LoreData.41");

                case characters.Jester:

                    return Mod.instance.Helper.Translation.Get("LoreData.45");

                case characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("LoreData.49");

                case characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("LoreData.53");

                case characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("LoreData.57");

            }

        }

        public static Dictionary<LoreData.stories, Lorestory> LoreList()
        {

            Dictionary<LoreData.stories, Lorestory> storylist = new();

            // ===========================================
            // Weald

            storylist[stories.Effigy_Weald] = new()
            {
                story = stories.Effigy_Weald,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.75"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.76") +
                Mod.instance.Helper.Translation.Get("LoreData.77") +
                Mod.instance.Helper.Translation.Get("LoreData.78"),

            };

            storylist[stories.Effigy_self_1] = new()
            {
                story = stories.Effigy_self_1,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.86"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.87") +
                    Mod.instance.Helper.Translation.Get("LoreData.88") +
                    Mod.instance.Helper.Translation.Get("LoreData.89"),

            };

            // ===========================================
            // Mists

            storylist[stories.Effigy_Mists] = new()
            {
                story = stories.Effigy_Mists,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.100"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.101") +
                Mod.instance.Helper.Translation.Get("LoreData.102") +
                Mod.instance.Helper.Translation.Get("LoreData.103"),

            };

            // ===========================================
            // Effigy Quest

            storylist[stories.Effigy_self_2] = new()
            {
                story = stories.Effigy_self_2,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.114"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.115") +
                    Mod.instance.Helper.Translation.Get("LoreData.116") +
                    Mod.instance.Helper.Translation.Get("LoreData.117") +
                    Mod.instance.Helper.Translation.Get("LoreData.118") +
                    Mod.instance.Helper.Translation.Get("LoreData.119") +
                    Mod.instance.Helper.Translation.Get("LoreData.120") +
                    Mod.instance.Helper.Translation.Get("LoreData.121") +
                    Mod.instance.Helper.Translation.Get("LoreData.122"),

            };

            // ===========================================
            // Stars

            storylist[stories.Effigy_Stars] = new()
            {
                story = stories.Effigy_Stars,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.133"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.134") +
                    Mod.instance.Helper.Translation.Get("LoreData.135") +
                    Mod.instance.Helper.Translation.Get("LoreData.136") +
                    Mod.instance.Helper.Translation.Get("LoreData.137"),

            };

            storylist[stories.Revenant_Stars] = new()
            {
                story = stories.Revenant_Stars,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("LoreData.145"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.146") +
                        Mod.instance.Helper.Translation.Get("LoreData.147") +
                        Mod.instance.Helper.Translation.Get("LoreData.148") +
                        Mod.instance.Helper.Translation.Get("LoreData.149") +
                        Mod.instance.Helper.Translation.Get("LoreData.150") +
                        Mod.instance.Helper.Translation.Get("LoreData.151") +
                        Mod.instance.Helper.Translation.Get("LoreData.152") +
                        Mod.instance.Helper.Translation.Get("LoreData.153")

            };

            storylist[stories.Revenant_self_1] = new()
            {
                story = stories.Revenant_self_1,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("LoreData.161"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.162") +
                        Mod.instance.Helper.Translation.Get("LoreData.163") +
                        Mod.instance.Helper.Translation.Get("LoreData.164") +
                        Mod.instance.Helper.Translation.Get("LoreData.165")

            };

            // ===========================================
            // Fates

            storylist[stories.Effigy_Jester] = new()
            {
                story = stories.Effigy_Jester,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.176"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.177") +
                            Mod.instance.Helper.Translation.Get("LoreData.178") +
                            Mod.instance.Helper.Translation.Get("LoreData.179") +
                            Mod.instance.Helper.Translation.Get("LoreData.180"),

            };

            storylist[stories.Jester_Effigy] = new()
            {
                story = stories.Jester_Effigy,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("LoreData.188"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.189") +
                            Mod.instance.Helper.Translation.Get("LoreData.190"),

            };

            storylist[stories.Jester_self_1] = new()
            {
                story = stories.Jester_self_1,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("LoreData.198"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.199"),

            };

            storylist[stories.Jester_Fates] = new()
            {
                story = stories.Jester_Fates,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("LoreData.207"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.208") +
                Mod.instance.Helper.Translation.Get("LoreData.209") +
                Mod.instance.Helper.Translation.Get("LoreData.210"),

            };

            storylist[stories.Revenant_Fates] = new()
            {
                story = stories.Revenant_Fates,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("LoreData.218"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.219") +
                Mod.instance.Helper.Translation.Get("LoreData.220") +
                Mod.instance.Helper.Translation.Get("LoreData.221") +
                Mod.instance.Helper.Translation.Get("LoreData.222") +
                Mod.instance.Helper.Translation.Get("LoreData.223"),

            };

            // ===========================================
            // Jester Quest

            storylist[stories.Effigy_Buffin] = new()
            {
                story = stories.Effigy_Buffin,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.234"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.235") +
                Mod.instance.Helper.Translation.Get("LoreData.236") +
                Mod.instance.Helper.Translation.Get("LoreData.237") +
                Mod.instance.Helper.Translation.Get("LoreData.238"),
            };

            storylist[stories.Jester_Buffin] = new()
            {
                story = stories.Jester_Buffin,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("LoreData.245"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.246") +
                Mod.instance.Helper.Translation.Get("LoreData.247") +
                Mod.instance.Helper.Translation.Get("LoreData.248") +
                Mod.instance.Helper.Translation.Get("LoreData.249") +
                Mod.instance.Helper.Translation.Get("LoreData.250"),


            };

            storylist[stories.Revenant_court] = new()
            {
                story = stories.Revenant_court,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("LoreData.259"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.260") +
                Mod.instance.Helper.Translation.Get("LoreData.261") +
                Mod.instance.Helper.Translation.Get("LoreData.262") +
                Mod.instance.Helper.Translation.Get("LoreData.263") +
                Mod.instance.Helper.Translation.Get("LoreData.264"),

            };

            storylist[stories.Revenant_Marlon] = new()
            {
                story = stories.Revenant_Marlon,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("LoreData.272"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.273") +
                Mod.instance.Helper.Translation.Get("LoreData.274") +
                Mod.instance.Helper.Translation.Get("LoreData.275") +
                Mod.instance.Helper.Translation.Get("LoreData.276"),

            };

            // ===========================================
            // Shadowtin

            storylist[stories.Effigy_Shadowtin] = new()
            {
                story = stories.Effigy_Shadowtin,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.287"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.288") +
                Mod.instance.Helper.Translation.Get("LoreData.289")

            };

            storylist[stories.Jester_Shadowtin] = new()
            {
                story = stories.Jester_Shadowtin,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("LoreData.297"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.298") +
                Mod.instance.Helper.Translation.Get("LoreData.299"),

            };

            storylist[stories.Shadowtin_Effigy] = new()
            {
                story = stories.Shadowtin_Effigy,
                character = characters.Shadowtin,
                question = Mod.instance.Helper.Translation.Get("LoreData.307"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.308") +
                Mod.instance.Helper.Translation.Get("LoreData.309") +
                Mod.instance.Helper.Translation.Get("LoreData.310") +
                Mod.instance.Helper.Translation.Get("LoreData.311") +
                Mod.instance.Helper.Translation.Get("LoreData.312"),

            };

            storylist[stories.Shadowtin_Jester] = new()
            {
                story = stories.Shadowtin_Jester,
                character = characters.Shadowtin,
                question = Mod.instance.Helper.Translation.Get("LoreData.320"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.321") +
                Mod.instance.Helper.Translation.Get("LoreData.322")

            };

            storylist[stories.Shadowtin_self_1] = new()
            {
                story = stories.Shadowtin_self_1,
                character = characters.Shadowtin,
                question = Mod.instance.Helper.Translation.Get("LoreData.330"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.331") +
                Mod.instance.Helper.Translation.Get("LoreData.332") +
                Mod.instance.Helper.Translation.Get("LoreData.333") +
                Mod.instance.Helper.Translation.Get("LoreData.334") +
                Mod.instance.Helper.Translation.Get("LoreData.335") +
                Mod.instance.Helper.Translation.Get("LoreData.336") +
                Mod.instance.Helper.Translation.Get("LoreData.337"),

            };


            storylist[stories.Shadowtin_self_1] = new()
            {
                story = stories.Shadowtin_self_1,
                character = characters.Shadowtin,
                question = Mod.instance.Helper.Translation.Get("LoreData.346"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.347") +
                Mod.instance.Helper.Translation.Get("LoreData.348") +
                Mod.instance.Helper.Translation.Get("LoreData.349") +
                Mod.instance.Helper.Translation.Get("LoreData.350") +
                Mod.instance.Helper.Translation.Get("LoreData.351"),

            };


            storylist[stories.Buffin_court] = new()
            {
                story = stories.Buffin_court,
                character = characters.Buffin,
                question = Mod.instance.Helper.Translation.Get("LoreData.360"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.361") +
                Mod.instance.Helper.Translation.Get("LoreData.362") +
                Mod.instance.Helper.Translation.Get("LoreData.363") +
                Mod.instance.Helper.Translation.Get("LoreData.364"),

            };


            // ===========================================
            // Ether

            storylist[stories.Effigy_Ether] = new()
            {
                story = stories.Effigy_Ether,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.376"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.377") +
                Mod.instance.Helper.Translation.Get("LoreData.378") +
                Mod.instance.Helper.Translation.Get("LoreData.379")

            };

            storylist[stories.Jester_Tomb] = new()
            {
                story = stories.Jester_Tomb,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("LoreData.387"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.388") +
                Mod.instance.Helper.Translation.Get("LoreData.389") +
                Mod.instance.Helper.Translation.Get("LoreData.390"),

            };

            storylist[stories.Jester_Ether] = new()
            {
                story = stories.Jester_Ether,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("LoreData.398"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.399") +
                Mod.instance.Helper.Translation.Get("LoreData.400"),

            };

            storylist[stories.Shadowtin_Ether] = new()
            {
                story = stories.Shadowtin_Ether,
                character = characters.Shadowtin,
                question = Mod.instance.Helper.Translation.Get("LoreData.408"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.409") +
                Mod.instance.Helper.Translation.Get("LoreData.410"),

            };

            storylist[stories.Revenant_Ether] = new()
            {
                story = stories.Revenant_Ether,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("LoreData.418"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.419") +
                Mod.instance.Helper.Translation.Get("LoreData.420") +
                Mod.instance.Helper.Translation.Get("LoreData.421") +
                Mod.instance.Helper.Translation.Get("LoreData.422") +
                Mod.instance.Helper.Translation.Get("LoreData.423"),

            };

            storylist[stories.Buffin_Ether] = new()
            {
                story = stories.Buffin_Ether,
                character = characters.Buffin,
                question = Mod.instance.Helper.Translation.Get("LoreData.431"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.432") +
                Mod.instance.Helper.Translation.Get("LoreData.433") +
                Mod.instance.Helper.Translation.Get("LoreData.434"),

            };


            // ===========================================
            // Ether

            storylist[stories.Effigy_Circle] = new()
            {
                story = stories.Effigy_Circle,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.446"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.447") +
                Mod.instance.Helper.Translation.Get("LoreData.448") +
                Mod.instance.Helper.Translation.Get("LoreData.449"),

            };


            storylist[stories.Effigy_Circle_2] = new()
            {
                story = stories.Effigy_Circle_2,
                character = characters.Effigy,
                question = Mod.instance.Helper.Translation.Get("LoreData.458"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.459") +
                Mod.instance.Helper.Translation.Get("LoreData.460") +
                Mod.instance.Helper.Translation.Get("LoreData.461") +
                Mod.instance.Helper.Translation.Get("LoreData.462") +
                Mod.instance.Helper.Translation.Get("LoreData.463"),

            };

            storylist[stories.Jester_Circle] = new()
            {
                story = stories.Jester_Ether,
                character = characters.Jester,
                question = Mod.instance.Helper.Translation.Get("LoreData.471"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.472") +
                Mod.instance.Helper.Translation.Get("LoreData.473") +
                Mod.instance.Helper.Translation.Get("LoreData.474") +
                Mod.instance.Helper.Translation.Get("LoreData.475") +
                Mod.instance.Helper.Translation.Get("LoreData.476"),

            };

            storylist[stories.Revenant_Circle] = new()
            {
                story = stories.Revenant_Ether,
                character = characters.Revenant,
                question = Mod.instance.Helper.Translation.Get("LoreData.484"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.485") +
                    Mod.instance.Helper.Translation.Get("LoreData.486") +
                    Mod.instance.Helper.Translation.Get("LoreData.487") +
                    Mod.instance.Helper.Translation.Get("LoreData.488") +
                    Mod.instance.Helper.Translation.Get("LoreData.489"),

            };

            storylist[stories.Buffin_Circle] = new()
            {
                story = stories.Buffin_Ether,
                character = characters.Buffin,
                question = Mod.instance.Helper.Translation.Get("LoreData.497"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.498") +
                Mod.instance.Helper.Translation.Get("LoreData.499") +
                Mod.instance.Helper.Translation.Get("LoreData.500") +
                Mod.instance.Helper.Translation.Get("LoreData.501") +
                Mod.instance.Helper.Translation.Get("LoreData.502") +
                Mod.instance.Helper.Translation.Get("LoreData.503") +
                Mod.instance.Helper.Translation.Get("LoreData.504") +
                Mod.instance.Helper.Translation.Get("LoreData.505") +
                Mod.instance.Helper.Translation.Get("LoreData.506"),

            };

            storylist[stories.Shadowtin_Circle] = new()
            {
                story = stories.Shadowtin_Circle,
                character = characters.Shadowtin,
                question = Mod.instance.Helper.Translation.Get("LoreData.514"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.515") +
                Mod.instance.Helper.Translation.Get("LoreData.516") +
                Mod.instance.Helper.Translation.Get("LoreData.517") +
                Mod.instance.Helper.Translation.Get("LoreData.518"),
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
