using StardewDruid.Cast.Weald;
using StardewValley;
using StardewValley.Locations;
using System;
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

        public enum villagers
        {
            Sebastian, Sam,
            Maru, Abigail,
            Robin, Demetrius, Linus,
            Alex, Elliott, Harvey,
            Emily, Penny,
            Caroline, Clint, Evelyn, George, 
            Gus, Jodi, Lewis, Pam, Pierre, Vincent,
            Shane,
            Leah, Haley,
            Marnie, Jas, Wizard, Willy,
            Krobus,
            Kent,
        }

        public static void CommunityFriendship(villagerLocales locale = villagerLocales.mountain, int friendship = 250, int questRating = 1)
        {
            List<string> NPCIndex;
            switch (locale)
            {
                default:
                case villagerLocales.mountain:

                    Mod.instance.CastDisplay(
                        Mod.instance.Helper.Translation.Get("VillagerData.11") + questRating +
                        Mod.instance.Helper.Translation.Get("VillagerData.12") + friendship +
                        Mod.instance.Helper.Translation.Get("VillagerData.13"), 2);

                    NPCIndex = new(){
                        villagers.Sebastian.ToString(), villagers.Sam.ToString(),
                        villagers.Maru.ToString(), villagers.Abigail.ToString(),
                        villagers.Robin.ToString(), villagers.Demetrius.ToString(), villagers.Linus.ToString(),
                    };

                    ModUtility.UpdateFriendship(Game1.player, NPCIndex, friendship);

                    break;

                case villagerLocales.town:

                    Mod.instance.CastDisplay(Mod.instance.Helper.Translation.Get("VillagerData.27")
                        + questRating + Mod.instance.Helper.Translation.Get("VillagerData.28")
                        + friendship + Mod.instance.Helper.Translation.Get("VillagerData.29"), 2);

                    NPCIndex = new(){
                        villagers.Alex.ToString(), villagers.Elliott.ToString(), villagers.Harvey.ToString(),
                        villagers.Emily.ToString(), villagers.Penny.ToString(),
                        villagers.Caroline.ToString(), villagers.Clint.ToString(), villagers.Evelyn.ToString(),
                        villagers.George.ToString(), villagers.Gus.ToString(), villagers.Jodi.ToString(),
                        villagers.Lewis.ToString(), villagers.Pam.ToString(),
                        villagers.Pierre.ToString(), villagers.Vincent.ToString(),
                    };

                    ModUtility.UpdateFriendship(Game1.player, NPCIndex, friendship);

                    break;

                case villagerLocales.forest:

                    Mod.instance.CastDisplay(Mod.instance.Helper.Translation.Get("VillagerData.46")
                        + questRating + Mod.instance.Helper.Translation.Get("VillagerData.47")
                        + friendship + Mod.instance.Helper.Translation.Get("VillagerData.48"), 2);

                    NPCIndex = new(){
                        villagers.Shane.ToString(),
                        villagers.Leah.ToString(), villagers.Haley.ToString(),
                        villagers.Marnie.ToString(), villagers.Jas.ToString(),
                        villagers.Wizard.ToString(), villagers.Willy.ToString(),
                    };

                    ModUtility.UpdateFriendship(Game1.player, NPCIndex, friendship);

                    break;

            }

        }

        public static List<string> CustomReaction(ReactionData.reactions react, string name)
        {

            List<string> reaction = new();

            switch (react)
            {

                case ReactionData.reactions.stars:

                    switch (Enum.Parse<villagers>(name))
                    {

                        case villagers.Harvey:

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.80")
                                + Game1.player.Name +
                                Mod.instance.Helper.Translation.Get("VillagerData.82"));

                            break;

                        case villagers.Maru:

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.88"));

                            break;

                        case villagers.Kent:

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.94"));

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.96"));

                            break;

                        case villagers.Krobus:

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.102"));

                            break;

                        case villagers.Jodi:

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.108"));

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.110"));

                            break;

                        default:

                            break;

                    }

                    break;

                case ReactionData.reactions.mists:

                    switch (Enum.Parse<villagers>(name))
                    {

                        case villagers.Sebastian:

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.129"));

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.131"));

                            break;

                        case villagers.Sam:

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.137"));

                            break;

                        case villagers.Demetrius:

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.143"));

                            reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.145"));

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
