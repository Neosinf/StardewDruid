using StardewDruid.Cast.Weald;
using StardewValley;
using StardewValley.Locations;
using System.Collections.Generic;
using static StardewDruid.Data.ReactionData;

namespace StardewDruid.Data
{
    static class VillagerData
    {

        public enum villagerLocales
        {
            mountain,
            town,
            forest,
        }

        public static List<string> VillagerIndex(villagerLocales locale)
        {

            switch (locale)
            {
                default:
                case villagerLocales.mountain:
                    return new(){
                        "Sebastian", "Sam",
                        "Maru", "Abigail",
                        "Robin", "Demetrius", "Linus",
                    };

                case villagerLocales.town:
                    return new(){
                        "Alex", "Elliott", "Harvey",
                        "Emily", "Penny",
                        "Caroline", "Clint", "Evelyn", "George", "Gus", "Jodi", "Lewis", "Pam", "Pierre", "Vincent",
                    };

                case villagerLocales.forest:
                    return new(){
                        "Shane",
                        "Leah", "Haley",
                        "Marnie", "Jas", "Wizard", "Willy",
                    };


            }

        }

        public static void CommunityFriendship(villagerLocales id = villagerLocales.mountain, int friendship = 250)
        {

            List<string> NPCIndex = VillagerData.VillagerIndex(id);

            ModUtility.UpdateFriendship(Game1.player, NPCIndex, friendship);

        }

        public static List<string> CustomReaction(ReactionData.reactions react, string name)
        {

            List<string> reaction = new();

            switch (react)
            {

                case ReactionData.reactions.stars:

                    switch (name)
                    {

                        case "Harvey":

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.stars.Harvey.0").Tokens(new { playerName= Game1.player.Name }));

                            break;

                        case "Maru":

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.stars.Maru.0"));

                            break;

                        case "Kent":

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.stars.Kent.0"));

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.stars.Kent.1"));

                            break;

                        case "Krobus":

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.stars.Krobus.0"));

                            break;

                        case "Jodi":

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.stars.Jodi.0"));

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.stars.Jodi.1"));

                            break;

                        default:

                            break;

                    }

                    break;

                case ReactionData.reactions.mists:

                    switch (name)
                    {

                        case "Sebastian":

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.mists.Sebastian.0"));

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.mists.Sebastian.1"));

                            break;

                        case "Sam":

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.mists.Sam.0"));

                            break;

                        case "Demetrius":

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.mists.Demetrius.0"));

                            reaction.Add(Mod.instance.Helper.Translation.Get("Reaction.Custom.mists.Demetrius.1"));

                            break;

                    }

                    break;

            }

            return reaction;

        }

        public static Dictionary<ReactionData.portraits, string> ReactionPortraits(string name)
        {

            Dictionary<ReactionData.portraits, string> shots = new()
            {
                [ReactionData.portraits.neutral] = "$0",
                [ReactionData.portraits.happy] = "$h",
                [ReactionData.portraits.sad] = "$s",
                [ReactionData.portraits.unique] = "$u",
                [ReactionData.portraits.love] = "$l",
                [ReactionData.portraits.angry] = "$a",
            };


            switch (name)
            {
                case "Lewis":

                    shots[ReactionData.portraits.love] = "$h";

                    break;

                case "Caroline":

                    shots[ReactionData.portraits.love] = "$h";
                    shots[ReactionData.portraits.angry] = "$s";
                    break;

                case "Pam":
                case "Jodi":
                case "Kent":
                case "Linus":

                    shots[ReactionData.portraits.love] = "$h";
                    shots[ReactionData.portraits.angry] = "$u";

                    break;

                case "Clint":

                    shots[ReactionData.portraits.love] = "$a";
                    shots[ReactionData.portraits.angry] = "$u";

                    break;

                case "Wizard":

                    shots = new()
                    {
                        [ReactionData.portraits.neutral] = "$neutral",
                        [ReactionData.portraits.happy] = "$neutral",
                        [ReactionData.portraits.sad] = "$h",
                        [ReactionData.portraits.unique] = "$h",
                        [ReactionData.portraits.love] = "$neutral",
                        [ReactionData.portraits.angry] = "$h",
                    };

                    break;

            }

            return shots;

        }


        public static Dictionary<string, int> Affinity()
        {
            Dictionary<string, int> affinities = new()
            {

                // Skeptical / Worldly
                ["Demetrius"] = 0,
                ["Pam"] = 0,
                ["Penny"] = 0,
                ["Kent"] = 0,
                ["Harvey"] = 0,
                ["Maru"] = 0,
                ["Robin"] = 0,

                // Ignorant / Don't know
                ["Jas"] = 1,
                ["Alex"] = 1,
                ["Vincent"] = 1,
                ["Sam"] = 1,
                ["Haley"] = 1,
                ["Marnie"] = 1,

                // Indifferent / Unconcerned
                ["Shane"] = 2,
                ["Jodi"] = 2,
                ["Gus"] = 2,
                ["Lewis"] = 2,
                ["Pierre"] = 2,
                ["George"] = 2,

                // Enthusiastic / Inspired
                ["Leah"] = 3,
                ["Emily"] = 3,
                ["Elliott"] = 3,
                ["Abigail"] = 3,
                ["Sebastian"] = 3,
                ["Caroline"] = 3,

                // Matter of Fact / Concerned
                ["Evelyn"] = 4,
                ["Clint"] = 4,
                ["Dwarf"] = 4,
                ["Marlon"] = 4,
                ["Willy"] = 4,
                ["Mateo"] = 4,

                // Esoteric Knowledge
                ["Krobus"] = 5,
                ["Linus"] = 5,
                ["Wizard"] = 5,

            };

            return affinities;

        }

    }

}
