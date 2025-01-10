using StardewDruid.Character;
using StardewValley.Locations;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley.Minigames;
using System.Security.Cryptography.X509Certificates;
using StardewDruid.Event;

namespace StardewDruid.Data
{

    public class LoreSet
    {

        public enum settypes
        {
            character,
            location,
            transcript,
        }

        public enum loresets
        {

            Grove,

            Effigy,

            Spring,

            challengeWeald,

            Atoll,

            Graves,

            Keeper,

            challengeMists,

            Chapel,

            Revenant,

            Clearing,

            challengeStars,

            challengeAtoll,

            Lair,

            challengeDragon,

            Jester,

            Court,

            Buffin,

            challengeFates,

            Shadowtin,

            Tomb,

            swordEther,

            Engine,

            Gate,

            challengeEther,

            Blackfeather,

            Aldebaran,

        }

        public settypes settype;

        public loresets loreset;

        public string title;

        public string quest;

        public IconData.displays display;

    }

    public class LoreStory
    {

        public enum loretypes
        {
            information,
            story,
            transcript,
        }

        public enum stories
        {

            // ------------- profiles

            Effigy_Profile,
            Effigy_Adventure,
            Effigy_Conclusion,

            Keeper_Profile,

            Revenant_Profile,
            Revenant_Conclusion,

            Jester_Profile,
            Jester_Adventure,

            Buffin_Profile,

            Shadowtin_Profile,
            Shadowtin_Adventure,

            Blackfeather_Profile,
            Blackfeather_Adventure,

            Aldebaran_Profile,

            // ------------- location profiles

            Grove_Profile,

            Spring_Profile,

            Atoll_Profile,

            Graves_Profile,

            Chapel_Profile,

            Clearing_Profile,

            Lair_Profile,

            Court_Profile,

            Tomb_Profile,

            Engine_Profile,

            Gate_Profile,

            // ------------- character lore

            Effigy_Weald,
            Effigy_self_1,

            Effigy_Mists,

            Effigy_self_2,

            Memory_Prince,
            Memory_Kings,
            Memory_Dragons,

            Effigy_Stars,
            Revenant_Stars,
            Revenant_self_1,

            Effigy_Jester,
            Jester_Effigy,
            Jester_self_1,
            Jester_Fates,
            Revenant_Fates,

            Effigy_Buffin,
            Jester_Buffin,
            Revenant_court,
            Revenant_Marlon,
            Buffin_self_1,

            Effigy_Shadowtin,
            Jester_Shadowtin,
            Shadowtin_Effigy,
            Shadowtin_Jester,
            Shadowtin_self_1,
            Shadowtin_self_2,
            Buffin_court,

            Effigy_Ether,
            Jester_Tomb,
            Jester_Ether,
            Shadowtin_Ether,
            Revenant_Ether,
            Buffin_Ether,

            Effigy_Circle,
            Effigy_Circle_2,
            Jester_Circle,
            Revenant_Circle,
            Buffin_Circle,
            Shadowtin_Circle,

            // After Blackfeather quest

            Effigy_Bones,
            Jester_Bones,
            Revenant_Bones,
            Buffin_Bones,
            Shadowtin_Bones,
            Blackfeather_self_1,
            Blackfeather_self_2,

            // After Buffin quest

            Effigy_Linus,
            Buffin_Carnivellion,
            Shadowtin_Doja,
            Revenant_Carnivellion,

            // After Revenant quest

            Jester_Visions,
            Shadowtin_Crew,
            Effigy_Successor,
            Blackfeather_Successor,

            // After Rite of Bones

            Aldebaran_Self,
            Aldebaran_Self_2,
            Jester_FallenStar,
            Buffin_RiteOfBones,
            Blackfeather_Gate,


        }

        public loretypes loretype;

        public LoreSet.loresets loreset;

        public string quest;

        public string title;

        public string description;

        public List<string> details;

        // story properties

        public stories story;

        public CharacterHandle.characters character;

        public string question;

        public string answer;


    }

    public class LoreData
    {

        public static Dictionary<LoreSet.loresets, LoreSet> LoreSets()
        {

            Dictionary<LoreSet.loresets, LoreSet> sets = new()
            {


                [LoreSet.loresets.Grove] = new()
                {

                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Grove,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.6"),

                    quest = QuestHandle.startPoint,

                    display = IconData.displays.weald,

                },

                [LoreSet.loresets.Effigy] = new()
                {

                    settype = LoreSet.settypes.character,

                    loreset = LoreSet.loresets.Effigy,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.14"),

                    quest = QuestHandle.approachEffigy,

                    display = IconData.displays.effigy,

                },

                [LoreSet.loresets.Spring] = new()
                {

                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Spring,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.22"),

                    quest = QuestHandle.challengeWeald,

                    display = IconData.displays.weald,

                },

                [LoreSet.loresets.challengeWeald] = new()
                {

                    settype = LoreSet.settypes.transcript,

                    loreset = LoreSet.loresets.challengeWeald,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.30"),

                    quest = QuestHandle.challengeWeald,

                    display = IconData.displays.weald,

                },

                [LoreSet.loresets.Atoll] = new()
                {

                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Atoll,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.38"),

                    quest = QuestHandle.swordMists,

                    display = IconData.displays.mists,

                },

                [LoreSet.loresets.Graves] = new()
                {

                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Graves,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.46"),

                    quest = QuestHandle.challengeMists,

                    display = IconData.displays.mists,

                },

                [LoreSet.loresets.challengeMists] = new()
                {

                    settype = LoreSet.settypes.transcript,

                    loreset = LoreSet.loresets.challengeMists,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.54"),

                    quest = QuestHandle.challengeMists,

                    display = IconData.displays.mists,

                },

                [LoreSet.loresets.Keeper] = new()
                {

                    settype = LoreSet.settypes.character,

                    loreset = LoreSet.loresets.Keeper,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.62"),

                    quest = QuestHandle.challengeMists,

                    display = IconData.displays.chaos,

                },

                [LoreSet.loresets.Chapel] = new()
                {

                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Chapel,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.70"),

                    quest = QuestHandle.swordStars,

                    display = IconData.displays.stars,

                },

                [LoreSet.loresets.Revenant] = new()
                {

                    settype = LoreSet.settypes.character,

                    loreset = LoreSet.loresets.Revenant,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.78"),

                    quest = QuestHandle.swordStars,

                    display = IconData.displays.revenant,

                },

                [LoreSet.loresets.Clearing] = new()
                {

                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Clearing,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.86"),

                    quest = QuestHandle.challengeStars,

                    display = IconData.displays.stars,

                },

                [LoreSet.loresets.challengeStars] = new()
                {

                    settype = LoreSet.settypes.transcript,

                    loreset = LoreSet.loresets.challengeStars,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.94"),

                    quest = QuestHandle.challengeStars,

                    display = IconData.displays.stars,

                },

                [LoreSet.loresets.challengeAtoll] = new()
                {

                    settype = LoreSet.settypes.transcript,

                    loreset = LoreSet.loresets.challengeAtoll,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.102"),

                    quest = QuestHandle.challengeAtoll,

                    display = IconData.displays.mists,

                },

                [LoreSet.loresets.Lair] = new()
                {

                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Lair,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.110"),

                    quest = QuestHandle.challengeDragon,

                    display = IconData.displays.stars,

                },

                [LoreSet.loresets.challengeDragon] = new()
                {

                    settype = LoreSet.settypes.transcript,

                    loreset = LoreSet.loresets.challengeDragon,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.118"),

                    quest = QuestHandle.challengeDragon,

                    display = IconData.displays.stars,

                },

                [LoreSet.loresets.Jester] = new()
                {
                    settype = LoreSet.settypes.character,

                    loreset = LoreSet.loresets.Jester,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.126"),

                    quest = QuestHandle.approachJester,

                    display = IconData.displays.jester,
                },

                [LoreSet.loresets.Court] = new()
                {
                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Court,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.134"),

                    quest = QuestHandle.fatesOne,

                    display = IconData.displays.fates,
                },

                [LoreSet.loresets.Buffin] = new()
                {
                    settype = LoreSet.settypes.character,

                    loreset = LoreSet.loresets.Buffin,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.142"),

                    quest = QuestHandle.questJester,

                    display = IconData.displays.buffin,
                },

                [LoreSet.loresets.challengeFates] = new()
                {
                    settype = LoreSet.settypes.transcript,

                    loreset = LoreSet.loresets.challengeFates,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.150"),

                    quest = QuestHandle.challengeFates,

                    display = IconData.displays.fates,
                },

                [LoreSet.loresets.Shadowtin] = new()
                {
                    settype = LoreSet.settypes.character,

                    loreset = LoreSet.loresets.Shadowtin,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.158"),

                    quest = QuestHandle.challengeFates,

                    display = IconData.displays.shadowtin,
                },

                [LoreSet.loresets.Tomb] = new()
                {
                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Tomb,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.166"),

                    quest = QuestHandle.swordEther,

                    display = IconData.displays.ether,
                },

                [LoreSet.loresets.swordEther] = new()
                {
                    settype = LoreSet.settypes.transcript,

                    loreset = LoreSet.loresets.swordEther,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.174"),

                    quest = QuestHandle.swordEther,

                    display = IconData.displays.ether,
                },

                [LoreSet.loresets.Engine] = new()
                {
                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Engine,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.182"),

                    quest = QuestHandle.questShadowtin,

                    display = IconData.displays.ether,
                },

                [LoreSet.loresets.Gate] = new()
                {
                    settype = LoreSet.settypes.location,

                    loreset = LoreSet.loresets.Gate,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.190"),

                    quest = QuestHandle.challengeEther,

                    display = IconData.displays.bones,
                },

                [LoreSet.loresets.challengeEther] = new()
                {
                    settype = LoreSet.settypes.transcript,

                    loreset = LoreSet.loresets.challengeEther,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.198"),

                    quest = QuestHandle.challengeEther,

                    display = IconData.displays.ether,
                },

                [LoreSet.loresets.Blackfeather] = new()
                {
                    settype = LoreSet.settypes.character,

                    loreset = LoreSet.loresets.Blackfeather,

                    title = Mod.instance.Helper.Translation.Get("LoreData.340.1.206"),

                    quest = QuestHandle.challengeEther,

                    display = IconData.displays.blackfeather,
                },

                [LoreSet.loresets.Aldebaran] = new()
                {
                    settype = LoreSet.settypes.character,

                    loreset = LoreSet.loresets.Aldebaran,

                    title = Mod.instance.Helper.Translation.Get("LoreData.343.1"),

                    quest = QuestHandle.challengeBones,

                    display = IconData.displays.aldebaran,
                },
            };

            return sets;

        }

        public static string LoreOption(CharacterHandle.characters character)
        {

            switch (character)
            {

                case CharacterHandle.characters.Effigy:

                    return Mod.instance.Helper.Translation.Get("LoreData.10");

                case CharacterHandle.characters.Jester:

                    return Mod.instance.Helper.Translation.Get("LoreData.14");

                case CharacterHandle.characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("LoreData.18");

                case CharacterHandle.characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("LoreData.22");

                case CharacterHandle.characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("LoreData.26");

                case CharacterHandle.characters.Blackfeather:

                    return Mod.instance.Helper.Translation.Get("LoreData.320.1");

                case CharacterHandle.characters.keeper:

                    return Mod.instance.Helper.Translation.Get("LoreData.329.1");

                case CharacterHandle.characters.Aldebaran:

                    return Mod.instance.Helper.Translation.Get("LoreData.343.2");

                case CharacterHandle.characters.recruit_one:
                case CharacterHandle.characters.recruit_two:
                case CharacterHandle.characters.recruit_three:
                case CharacterHandle.characters.recruit_four:

                    return Mod.instance.Helper.Translation.Get("DialogueLore.361.1");

            }

            return null;

        }

        public static string LoreIntro(CharacterHandle.characters character)
        {

            switch (character)
            {

                default:
                case CharacterHandle.characters.Effigy:

                    return Mod.instance.Helper.Translation.Get("LoreData.41");

                case CharacterHandle.characters.Jester:

                    return Mod.instance.Helper.Translation.Get("LoreData.45");

                case CharacterHandle.characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("LoreData.49");

                case CharacterHandle.characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("LoreData.53");

                case CharacterHandle.characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("LoreData.57");

                case CharacterHandle.characters.Blackfeather:

                    return Mod.instance.Helper.Translation.Get("LoreData.320.2");

                case CharacterHandle.characters.keeper:

                    return Mod.instance.Helper.Translation.Get("LoreData.329.2");

                case CharacterHandle.characters.Aldebaran:

                    return Mod.instance.Helper.Translation.Get("LoreData.343.3");

            }

        }

        public static List<LoreStory> RetrieveLore(CharacterHandle.characters character)
        {

            List<LoreStory> list = new();

            int limit = 0;

            for (int i = Mod.instance.questHandle.lores.Count - 1; i >= 0; i--)
            {

                LoreStory story = Mod.instance.questHandle.lores.ElementAt(i).Value;

                if (story.loretype != LoreStory.loretypes.story)
                {

                    continue;

                }

                if (story.character != character)
                {

                    continue;

                }

                if (!Mod.instance.questHandle.IsComplete(story.quest))
                {

                    continue;

                }

                list.Add(story);

                limit++;

                if (limit == 3)
                {

                    break;

                }

            }

            return list;

        }

        public static Dictionary<LoreStory.stories, LoreStory> LoreList()
        {

            Dictionary<LoreStory.stories, LoreStory> storylist = new();

            // ===========================================
            // Characters
            storylist[LoreStory.stories.Effigy_Profile] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.approachEffigy,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.215"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.218"),
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.219"),
                }
            };


            storylist[LoreStory.stories.Effigy_Adventure] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.questEffigy,
                description = Mod.instance.Helper.Translation.Get("EffectsData.351"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("EffectsData.352"),
                    Mod.instance.Helper.Translation.Get("EffectsData.355"),
                    Mod.instance.Helper.Translation.Get("EffectsData.356"),
                    Mod.instance.Helper.Translation.Get("EffectsData.357"),
                    Mod.instance.Helper.Translation.Get("EffectsData.358"),
                }
            };

            storylist[LoreStory.stories.Effigy_Conclusion] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.questEffigy,
                description = Mod.instance.Helper.Translation.Get("LoreData.343.4"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("LoreData.343.5"),
                    Mod.instance.Helper.Translation.Get("LoreData.343.6"),
                }
            };

            storylist[LoreStory.stories.Keeper_Profile] = new()
            {
                loreset = LoreSet.loresets.Keeper,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.challengeMists,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.227"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.230"),
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.231")
                }
            };

            storylist[LoreStory.stories.Revenant_Profile] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.swordStars,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.239") +
                Mod.instance.Helper.Translation.Get("LoreData.340.1.240"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.243"),
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.244")
                }
            };

            storylist[LoreStory.stories.Revenant_Conclusion] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.questRevenant,
                description = Mod.instance.Helper.Translation.Get("LoreData.343.7"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("LoreData.343.8"),
                }
            };

            storylist[LoreStory.stories.Jester_Profile] = new()
            {
                loreset = LoreSet.loresets.Jester,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.approachJester,
                description =
                Mod.instance.Helper.Translation.Get("LoreData.340.1.253") +
                Mod.instance.Helper.Translation.Get("LoreData.340.1.254"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.257"),
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.258"),
                }
            };


            storylist[LoreStory.stories.Jester_Adventure] = new()
            {
                loreset = LoreSet.loresets.Jester,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.approachJester,
                description = Mod.instance.Helper.Translation.Get("EffectsData.432"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("EffectsData.433"),
                    Mod.instance.Helper.Translation.Get("EffectsData.436"),
                    Mod.instance.Helper.Translation.Get("EffectsData.437"),
                    Mod.instance.Helper.Translation.Get("EffectsData.438") +
                    Mod.instance.Helper.Translation.Get("EffectsData.439"),
                }
            };

            storylist[LoreStory.stories.Buffin_Profile] = new()
            {
                loreset = LoreSet.loresets.Buffin,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.questJester,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.266"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.269"),
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.270"),
                }
            };

            storylist[LoreStory.stories.Shadowtin_Profile] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.challengeFates,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.278") +
                Mod.instance.Helper.Translation.Get("LoreData.340.1.279"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.282"),
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.283")
                }

            };

            storylist[LoreStory.stories.Shadowtin_Adventure] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.challengeFates,
                description = Mod.instance.Helper.Translation.Get("EffectsData.537") +
                Mod.instance.Helper.Translation.Get("EffectsData.538"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("EffectsData.539"),
                    Mod.instance.Helper.Translation.Get("EffectsData.542"),
                    Mod.instance.Helper.Translation.Get("EffectsData.543"),
                    Mod.instance.Helper.Translation.Get("EffectsData.544"),
                    Mod.instance.Helper.Translation.Get("EffectsData.545"),
                    Mod.instance.Helper.Translation.Get("EffectsData.546")
                }
            };

            storylist[LoreStory.stories.Blackfeather_Profile] = new()
            {
                loreset = LoreSet.loresets.Blackfeather,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.questBlackfeather,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.291") +
                Mod.instance.Helper.Translation.Get("LoreData.340.1.292") +
                Mod.instance.Helper.Translation.Get("LoreData.340.1.293"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.296"),
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.297")

                },
            };

            storylist[LoreStory.stories.Blackfeather_Adventure] = new()
            {
                loreset = LoreSet.loresets.Blackfeather,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.questBlackfeather,
                description = Mod.instance.Helper.Translation.Get("EffectsData.326.2"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("EffectsData.326.3"),
                    Mod.instance.Helper.Translation.Get("EffectsData.326.4"),
                    Mod.instance.Helper.Translation.Get("EffectsData.326.5"),
                    Mod.instance.Helper.Translation.Get("EffectsData.326.6"),
                }
            };

            storylist[LoreStory.stories.Aldebaran_Profile] = new()
            {
                loreset = LoreSet.loresets.Aldebaran,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.challengeBones,
                description = Mod.instance.Helper.Translation.Get("LoreData.343.9") + Mod.instance.Helper.Translation.Get("LoreData.343.10"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("LoreData.343.11"),
                    Mod.instance.Helper.Translation.Get("LoreData.343.12"),
                    Mod.instance.Helper.Translation.Get("LoreData.356.1"),
                }
            };

            // ===========================================
            // Locations

            storylist[LoreStory.stories.Grove_Profile] = new()
            {
                loreset = LoreSet.loresets.Grove,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.startPoint,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.305"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.308"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.309"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.310"),
                   Mod.instance.Helper.Translation.Get("LoreData.347.4"),
               }
            };

            storylist[LoreStory.stories.Spring_Profile] = new()
            {
                loreset = LoreSet.loresets.Spring,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.challengeWeald,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.318"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.321"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.322"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.323"),
               }
            };

            storylist[LoreStory.stories.Atoll_Profile] = new()
            {
                loreset = LoreSet.loresets.Atoll,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.swordMists,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.332"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.335"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.336")
               }
            };

            storylist[LoreStory.stories.Graves_Profile] = new()
            {
                loreset = LoreSet.loresets.Graves,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.challengeMists,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.344"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.347"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.348"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.349"),
               }
            };

            storylist[LoreStory.stories.Chapel_Profile] = new()
            {
                loreset = LoreSet.loresets.Chapel,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.swordStars,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.357"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.360"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.361"),
                   Mod.instance.Helper.Translation.Get("LoreData.347.1"),
                   Mod.instance.Helper.Translation.Get("LoreData.347.2")
               }
            };

            storylist[LoreStory.stories.Clearing_Profile] = new()
            {
                loreset = LoreSet.loresets.Clearing,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.challengeStars,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.369"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.372"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.373"),
               }
            };

            storylist[LoreStory.stories.Lair_Profile] = new()
            {
                loreset = LoreSet.loresets.Lair,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.challengeDragon,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.381"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.384"),
               }
            };

            storylist[LoreStory.stories.Court_Profile] = new()
            {
                loreset = LoreSet.loresets.Court,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.swordFates,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.392") +
                    Mod.instance.Helper.Translation.Get("LoreData.340.1.393"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.396"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.397"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.398")
               }
            };

            storylist[LoreStory.stories.Tomb_Profile] = new()
            {
                loreset = LoreSet.loresets.Tomb,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.swordEther,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.406"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.409"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.410")
               }
            };

            storylist[LoreStory.stories.Engine_Profile] = new()
            {
                loreset = LoreSet.loresets.Engine,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.questShadowtin,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.418"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.421"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.422"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.423"),
                   Mod.instance.Helper.Translation.Get("LoreData.347.3"),
               }
            };

            storylist[LoreStory.stories.Gate_Profile] = new()
            {
                loreset = LoreSet.loresets.Gate,
                loretype = LoreStory.loretypes.information,
                quest = QuestHandle.challengeEther,
                description = Mod.instance.Helper.Translation.Get("LoreData.340.1.431"),
                details = new()
               {
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.434"),
                   Mod.instance.Helper.Translation.Get("LoreData.340.1.435")
               }
            };


            // ===========================================
            // Weald

            storylist[LoreStory.stories.Effigy_Weald] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                character = CharacterHandle.characters.Effigy,
                story = LoreStory.stories.Effigy_Weald,
                quest = QuestHandle.swordWeald,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.75"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.76") +
                Mod.instance.Helper.Translation.Get("LoreData.77") +
                Mod.instance.Helper.Translation.Get("LoreData.78"),

            };

            storylist[LoreStory.stories.Effigy_self_1] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_self_1,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.swordWeald,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.86"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.87") +
                    Mod.instance.Helper.Translation.Get("LoreData.88") +
                    Mod.instance.Helper.Translation.Get("LoreData.89"),

            };

            // ===========================================
            // Mists

            storylist[LoreStory.stories.Effigy_Mists] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Mists,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.swordMists,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.100"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.101") +
                Mod.instance.Helper.Translation.Get("LoreData.102") +
                Mod.instance.Helper.Translation.Get("LoreData.103"),

            };

            // ===========================================
            // Effigy Quest

            storylist[LoreStory.stories.Effigy_self_2] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_self_2,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.questEffigy,

                loretype = LoreStory.loretypes.story,
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
            // Keeper Memories

            storylist[LoreStory.stories.Memory_Prince] = new()
            {
                loreset = LoreSet.loresets.Keeper,
                story = LoreStory.stories.Memory_Prince,
                character = CharacterHandle.characters.keeper,
                quest = QuestHandle.challengeMists,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.329.3"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.329.4") +
                Mod.instance.Helper.Translation.Get("LoreData.329.5")

            };

            storylist[LoreStory.stories.Memory_Kings] = new()
            {

                loreset = LoreSet.loresets.Keeper,
                story = LoreStory.stories.Memory_Kings,
                character = CharacterHandle.characters.keeper,
                quest = QuestHandle.challengeMists,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.329.6"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.329.7") +
                Mod.instance.Helper.Translation.Get("LoreData.329.8")

            };

            storylist[LoreStory.stories.Memory_Dragons] = new()
            {

                loreset = LoreSet.loresets.Keeper,
                story = LoreStory.stories.Memory_Dragons,
                character = CharacterHandle.characters.keeper,
                quest = QuestHandle.challengeMists,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.329.9"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.329.10") +
                Mod.instance.Helper.Translation.Get("LoreData.329.11") +
                Mod.instance.Helper.Translation.Get("LoreData.329.12")

            };

            // ===========================================
            // Stars

            storylist[LoreStory.stories.Effigy_Stars] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Stars,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.swordStars,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.133"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.134") +
                    Mod.instance.Helper.Translation.Get("LoreData.135") +
                    Mod.instance.Helper.Translation.Get("LoreData.136") +
                    Mod.instance.Helper.Translation.Get("LoreData.137"),

            };

            storylist[LoreStory.stories.Revenant_Stars] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                story = LoreStory.stories.Revenant_Stars,
                character = CharacterHandle.characters.Revenant,
                quest = QuestHandle.swordStars,

                loretype = LoreStory.loretypes.story,
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

            storylist[LoreStory.stories.Revenant_self_1] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                story = LoreStory.stories.Revenant_self_1,
                character = CharacterHandle.characters.Revenant,
                quest = QuestHandle.swordStars,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.161"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.162") +
                        Mod.instance.Helper.Translation.Get("LoreData.163") +
                        Mod.instance.Helper.Translation.Get("LoreData.164") +
                        Mod.instance.Helper.Translation.Get("LoreData.165")

            };

            // ===========================================
            // Fates

            storylist[LoreStory.stories.Effigy_Jester] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Jester,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.approachJester,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.176"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.177") +
                            Mod.instance.Helper.Translation.Get("LoreData.178") +
                            Mod.instance.Helper.Translation.Get("LoreData.179") +
                            Mod.instance.Helper.Translation.Get("LoreData.180"),

            };

            storylist[LoreStory.stories.Jester_Effigy] = new()
            {
                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_Effigy,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.approachJester,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.188"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.189") +
                            Mod.instance.Helper.Translation.Get("LoreData.190"),

            };

            storylist[LoreStory.stories.Jester_self_1] = new()
            {
                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_self_1,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.approachJester,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.198"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.199"),

            };

            storylist[LoreStory.stories.Jester_Fates] = new()
            {

                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_Fates,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.approachJester,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.207"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.208") +
                Mod.instance.Helper.Translation.Get("LoreData.209") +
                Mod.instance.Helper.Translation.Get("LoreData.210"),

            };

            storylist[LoreStory.stories.Revenant_Fates] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                story = LoreStory.stories.Revenant_Fates,
                character = CharacterHandle.characters.Revenant,
                quest = QuestHandle.approachJester,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.218"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.219") +
                Mod.instance.Helper.Translation.Get("LoreData.220") +
                Mod.instance.Helper.Translation.Get("LoreData.221") +
                Mod.instance.Helper.Translation.Get("LoreData.222") +
                Mod.instance.Helper.Translation.Get("LoreData.223"),

            };

            // ===========================================
            // Jester Quest

            storylist[LoreStory.stories.Effigy_Buffin] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Buffin,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.questJester,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.234"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.235") +
                Mod.instance.Helper.Translation.Get("LoreData.236") +
                Mod.instance.Helper.Translation.Get("LoreData.237") +
                Mod.instance.Helper.Translation.Get("LoreData.238"),
            };

            storylist[LoreStory.stories.Jester_Buffin] = new()
            {
                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_Buffin,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.questJester,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.245"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.246") +
                Mod.instance.Helper.Translation.Get("LoreData.247") +
                Mod.instance.Helper.Translation.Get("LoreData.248") +
                Mod.instance.Helper.Translation.Get("LoreData.249") +
                Mod.instance.Helper.Translation.Get("LoreData.250"),


            };

            storylist[LoreStory.stories.Revenant_court] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                story = LoreStory.stories.Revenant_court,
                character = CharacterHandle.characters.Revenant,
                quest = QuestHandle.questJester,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.259"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.260") +
                Mod.instance.Helper.Translation.Get("LoreData.261") +
                Mod.instance.Helper.Translation.Get("LoreData.262") +
                Mod.instance.Helper.Translation.Get("LoreData.263") +
                Mod.instance.Helper.Translation.Get("LoreData.264"),

            };

            storylist[LoreStory.stories.Revenant_Marlon] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                story = LoreStory.stories.Revenant_Marlon,
                character = CharacterHandle.characters.Revenant,
                quest = QuestHandle.questJester,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.272"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.273") +
                Mod.instance.Helper.Translation.Get("LoreData.274") +
                Mod.instance.Helper.Translation.Get("LoreData.275") +
                Mod.instance.Helper.Translation.Get("LoreData.276"),

            };

            storylist[LoreStory.stories.Buffin_self_1] = new()
            {
                loreset = LoreSet.loresets.Buffin,
                story = LoreStory.stories.Buffin_self_1,
                character = CharacterHandle.characters.Buffin,
                quest = QuestHandle.questJester,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.340.1"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.340.2") +
                Mod.instance.Helper.Translation.Get("LoreData.340.3"),

            };
            // ===========================================
            // Shadowtin

            storylist[LoreStory.stories.Effigy_Shadowtin] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Shadowtin,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.challengeEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.287"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.288") +
                Mod.instance.Helper.Translation.Get("LoreData.289")

            };

            storylist[LoreStory.stories.Jester_Shadowtin] = new()
            {

                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_Shadowtin,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.challengeEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.297"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.298") +
                Mod.instance.Helper.Translation.Get("LoreData.299"),

            };

            storylist[LoreStory.stories.Shadowtin_Effigy] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                story = LoreStory.stories.Shadowtin_Effigy,
                character = CharacterHandle.characters.Shadowtin,
                quest = QuestHandle.challengeEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.307"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.308") +
                Mod.instance.Helper.Translation.Get("LoreData.309") +
                Mod.instance.Helper.Translation.Get("LoreData.310") +
                Mod.instance.Helper.Translation.Get("LoreData.311") +
                Mod.instance.Helper.Translation.Get("LoreData.312"),

            };

            storylist[LoreStory.stories.Shadowtin_Jester] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                story = LoreStory.stories.Shadowtin_Jester,
                character = CharacterHandle.characters.Shadowtin,
                quest = QuestHandle.challengeEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.320"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.321") +
                Mod.instance.Helper.Translation.Get("LoreData.322")

            };

            storylist[LoreStory.stories.Shadowtin_self_1] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                story = LoreStory.stories.Shadowtin_self_1,
                character = CharacterHandle.characters.Shadowtin,
                quest = QuestHandle.challengeEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.330"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.331") +
                Mod.instance.Helper.Translation.Get("LoreData.332") +
                Mod.instance.Helper.Translation.Get("LoreData.333") +
                Mod.instance.Helper.Translation.Get("LoreData.334") +
                Mod.instance.Helper.Translation.Get("LoreData.335") +
                Mod.instance.Helper.Translation.Get("LoreData.336") +
                Mod.instance.Helper.Translation.Get("LoreData.337"),

            };


            storylist[LoreStory.stories.Shadowtin_self_2] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                story = LoreStory.stories.Shadowtin_self_2,
                character = CharacterHandle.characters.Shadowtin,
                quest = QuestHandle.challengeEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.346"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.347") +
                Mod.instance.Helper.Translation.Get("LoreData.348") +
                Mod.instance.Helper.Translation.Get("LoreData.349") +
                Mod.instance.Helper.Translation.Get("LoreData.350") +
                Mod.instance.Helper.Translation.Get("LoreData.351"),

            };


            storylist[LoreStory.stories.Buffin_court] = new()
            {
                loreset = LoreSet.loresets.Buffin,
                story = LoreStory.stories.Buffin_court,
                character = CharacterHandle.characters.Buffin,
                quest = QuestHandle.challengeEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.360"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.361") +
                Mod.instance.Helper.Translation.Get("LoreData.362") +
                Mod.instance.Helper.Translation.Get("LoreData.363") +
                Mod.instance.Helper.Translation.Get("LoreData.364"),

            };

            // ===========================================
            // Ether

            storylist[LoreStory.stories.Effigy_Ether] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Ether,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.swordEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.376"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.377") +
                Mod.instance.Helper.Translation.Get("LoreData.378") +
                Mod.instance.Helper.Translation.Get("LoreData.379")

            };

            storylist[LoreStory.stories.Jester_Tomb] = new()
            {

                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_Tomb,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.swordEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.387"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.388") +
                Mod.instance.Helper.Translation.Get("LoreData.389") +
                Mod.instance.Helper.Translation.Get("LoreData.390"),

            };

            storylist[LoreStory.stories.Jester_Ether] = new()
            {

                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_Ether,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.swordEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.398"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.399") +
                Mod.instance.Helper.Translation.Get("LoreData.400"),

            };

            storylist[LoreStory.stories.Shadowtin_Ether] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                story = LoreStory.stories.Shadowtin_Ether,
                character = CharacterHandle.characters.Shadowtin,
                quest = QuestHandle.swordEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.408"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.409") +
                Mod.instance.Helper.Translation.Get("LoreData.410"),

            };

            storylist[LoreStory.stories.Revenant_Ether] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                story = LoreStory.stories.Revenant_Ether,
                character = CharacterHandle.characters.Revenant,
                quest = QuestHandle.swordEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.418"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.419") +
                Mod.instance.Helper.Translation.Get("LoreData.420") +
                Mod.instance.Helper.Translation.Get("LoreData.421") +
                Mod.instance.Helper.Translation.Get("LoreData.422") +
                Mod.instance.Helper.Translation.Get("LoreData.423"),

            };

            storylist[LoreStory.stories.Buffin_Ether] = new()
            {
                loreset = LoreSet.loresets.Buffin,
                story = LoreStory.stories.Buffin_Ether,
                character = CharacterHandle.characters.Buffin,
                quest = QuestHandle.swordEther,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.431"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.432") +
                Mod.instance.Helper.Translation.Get("LoreData.433") +
                Mod.instance.Helper.Translation.Get("LoreData.434"),

            };

            // ===========================================
            // Shadowtin

            storylist[LoreStory.stories.Effigy_Circle] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Circle,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.questShadowtin,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.446"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.447") +
                Mod.instance.Helper.Translation.Get("LoreData.448") +
                Mod.instance.Helper.Translation.Get("LoreData.449"),

            };

            storylist[LoreStory.stories.Effigy_Circle_2] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Circle_2,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.questShadowtin,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.458"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.459") +
                Mod.instance.Helper.Translation.Get("LoreData.460") +
                Mod.instance.Helper.Translation.Get("LoreData.461") +
                Mod.instance.Helper.Translation.Get("LoreData.462") +
                Mod.instance.Helper.Translation.Get("LoreData.463"),

            };

            storylist[LoreStory.stories.Jester_Circle] = new()
            {

                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_Ether,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.questShadowtin,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.471"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.472") +
                Mod.instance.Helper.Translation.Get("LoreData.473") +
                Mod.instance.Helper.Translation.Get("LoreData.474") +
                Mod.instance.Helper.Translation.Get("LoreData.475") +
                Mod.instance.Helper.Translation.Get("LoreData.476"),

            };

            storylist[LoreStory.stories.Revenant_Circle] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                story = LoreStory.stories.Revenant_Ether,
                character = CharacterHandle.characters.Revenant,
                quest = QuestHandle.questShadowtin,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.484"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.485") +
                    Mod.instance.Helper.Translation.Get("LoreData.486") +
                    Mod.instance.Helper.Translation.Get("LoreData.487") +
                    Mod.instance.Helper.Translation.Get("LoreData.488") +
                    Mod.instance.Helper.Translation.Get("LoreData.489"),

            };

            storylist[LoreStory.stories.Buffin_Circle] = new()
            {
                loreset = LoreSet.loresets.Buffin,
                story = LoreStory.stories.Buffin_Ether,
                character = CharacterHandle.characters.Buffin,
                quest = QuestHandle.questShadowtin,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.497"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.498") +
                Mod.instance.Helper.Translation.Get("LoreData.499") +
                Mod.instance.Helper.Translation.Get("LoreData.500") +
                Mod.instance.Helper.Translation.Get("LoreData.501"),

            };

            storylist[LoreStory.stories.Shadowtin_Circle] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                story = LoreStory.stories.Shadowtin_Circle,
                character = CharacterHandle.characters.Shadowtin,
                quest = QuestHandle.questShadowtin,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.514"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.515") +
                Mod.instance.Helper.Translation.Get("LoreData.516") +
                Mod.instance.Helper.Translation.Get("LoreData.517") +
                Mod.instance.Helper.Translation.Get("LoreData.518"),
            };

            // ===========================================
            // After Blackfeather quest

            storylist[LoreStory.stories.Effigy_Bones] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Bones,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.questBlackfeather,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.320.3"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.320.4") +
                Mod.instance.Helper.Translation.Get("LoreData.320.5"),
            };

            storylist[LoreStory.stories.Jester_Bones] = new()
            {

                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_Bones,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.questBlackfeather,


                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.320.6"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.320.7") +
                Mod.instance.Helper.Translation.Get("LoreData.320.8") +
                Mod.instance.Helper.Translation.Get("LoreData.320.9") +
                Mod.instance.Helper.Translation.Get("LoreData.320.10"),
            };

            storylist[LoreStory.stories.Revenant_Bones] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                story = LoreStory.stories.Revenant_Bones,
                character = CharacterHandle.characters.Revenant,
                quest = QuestHandle.questBlackfeather,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.323.1"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.323.2") +
                Mod.instance.Helper.Translation.Get("LoreData.323.3"),
            };

            storylist[LoreStory.stories.Buffin_Bones] = new()
            {
                loreset = LoreSet.loresets.Buffin,
                story = LoreStory.stories.Buffin_Bones,
                character = CharacterHandle.characters.Buffin,
                quest = QuestHandle.questBlackfeather,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.323.4"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.323.5") +
                Mod.instance.Helper.Translation.Get("LoreData.323.6"),
            };

            storylist[LoreStory.stories.Shadowtin_Bones] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                story = LoreStory.stories.Shadowtin_Bones,
                character = CharacterHandle.characters.Shadowtin,
                quest = QuestHandle.questBlackfeather,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.323.7"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.323.8") +
                Mod.instance.Helper.Translation.Get("LoreData.323.9"),
            };

            storylist[LoreStory.stories.Blackfeather_self_1] = new()
            {

                loreset = LoreSet.loresets.Blackfeather,
                story = LoreStory.stories.Blackfeather_self_1,
                character = CharacterHandle.characters.Blackfeather,
                quest = QuestHandle.questBlackfeather,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.323.10"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.323.11") +
                Mod.instance.Helper.Translation.Get("LoreData.323.12"),
            };

            storylist[LoreStory.stories.Blackfeather_self_2] = new()
            {

                loreset = LoreSet.loresets.Blackfeather,
                story = LoreStory.stories.Blackfeather_self_2,
                character = CharacterHandle.characters.Blackfeather,
                quest = QuestHandle.questBlackfeather,

                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.323.13"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.323.14") +
                Mod.instance.Helper.Translation.Get("LoreData.323.15"),
            };

            // ===========================================
            // After Buffin quest

            storylist[LoreStory.stories.Effigy_Linus] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Linus,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.questBuffin,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.11"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.12") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.13") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.14"),
            };

            storylist[LoreStory.stories.Buffin_Carnivellion] = new()
            {
                loreset = LoreSet.loresets.Buffin,
                story = LoreStory.stories.Buffin_Carnivellion,
                character = CharacterHandle.characters.Buffin,
                quest = QuestHandle.questBuffin,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.23"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.24") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.25") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.26"),
            };

            storylist[LoreStory.stories.Shadowtin_Doja] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                story = LoreStory.stories.Shadowtin_Doja,
                character = CharacterHandle.characters.Shadowtin,
                quest = QuestHandle.questBuffin,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.35"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.36") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.37") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.38"),
            };

            storylist[LoreStory.stories.Revenant_Carnivellion] = new()
            {
                loreset = LoreSet.loresets.Revenant,
                story = LoreStory.stories.Revenant_Carnivellion,
                character = CharacterHandle.characters.Revenant,
                quest = QuestHandle.questBuffin,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.47"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.48") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.49") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.50") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.51"),
            };

            // ===========================================
            // After Revenant quest

            storylist[LoreStory.stories.Jester_Visions] = new()
            {
                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_Visions,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.questRevenant,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.62"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.63") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.64") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.65"),
            };

            storylist[LoreStory.stories.Effigy_Successor] = new()
            {
                loreset = LoreSet.loresets.Effigy,
                story = LoreStory.stories.Effigy_Successor,
                character = CharacterHandle.characters.Effigy,
                quest = QuestHandle.questRevenant,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.74"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.75") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.76"),
            };
            storylist[LoreStory.stories.Blackfeather_Successor] = new()
            {
                loreset = LoreSet.loresets.Blackfeather,
                story = LoreStory.stories.Blackfeather_Successor,
                character = CharacterHandle.characters.Blackfeather,
                quest = QuestHandle.questRevenant,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.85"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.86") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.87") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.88"),
            };

            // ===========================================
            // After Rite of Bones

            storylist[LoreStory.stories.Aldebaran_Self] = new()
            {
                loreset = LoreSet.loresets.Aldebaran,
                story = LoreStory.stories.Aldebaran_Self,
                character = CharacterHandle.characters.Aldebaran,
                quest = QuestHandle.challengeBones,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.99"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.100") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.101") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.102") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.103"),
            };
            storylist[LoreStory.stories.Aldebaran_Self_2] = new()
            {
                loreset = LoreSet.loresets.Aldebaran,
                story = LoreStory.stories.Aldebaran_Self_2,
                character = CharacterHandle.characters.Aldebaran,
                quest = QuestHandle.challengeBones,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.112"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.113") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.114") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.115"),
            };
            storylist[LoreStory.stories.Jester_FallenStar] = new()
            {
                loreset = LoreSet.loresets.Jester,
                story = LoreStory.stories.Jester_FallenStar,
                character = CharacterHandle.characters.Jester,
                quest = QuestHandle.challengeBones,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.124"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.125") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.126"),
            };
            storylist[LoreStory.stories.Shadowtin_Crew] = new()
            {
                loreset = LoreSet.loresets.Shadowtin,
                story = LoreStory.stories.Shadowtin_Crew,
                character = CharacterHandle.characters.Shadowtin,
                quest = QuestHandle.challengeBones,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.135"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.136"),
            };
            storylist[LoreStory.stories.Buffin_RiteOfBones] = new()
            {
                loreset = LoreSet.loresets.Buffin,
                story = LoreStory.stories.Buffin_RiteOfBones,
                character = CharacterHandle.characters.Buffin,
                quest = QuestHandle.challengeBones,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.145"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.146"),
            };
            storylist[LoreStory.stories.Blackfeather_Gate] = new()
            {
                loreset = LoreSet.loresets.Blackfeather,
                story = LoreStory.stories.Blackfeather_Gate,
                character = CharacterHandle.characters.Blackfeather,
                quest = QuestHandle.challengeBones,
                loretype = LoreStory.loretypes.story,
                question = Mod.instance.Helper.Translation.Get("LoreData.343.1.155"),
                answer = Mod.instance.Helper.Translation.Get("LoreData.343.1.156") +
                Mod.instance.Helper.Translation.Get("LoreData.343.1.157"),
            };

            return storylist;

        }

    }


}
