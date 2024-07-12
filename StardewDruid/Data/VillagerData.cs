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

        public static void CommunityFriendship(villagerLocales locale = villagerLocales.mountain, int friendship = 250, int questRating = 1)
        {
            List<string> NPCIndex;
            switch (locale)
            {
                default:
                case villagerLocales.mountain:

                    Mod.instance.CastDisplay("Collected " + questRating + " pieces of trash, gained " + friendship + " friendship with mountain residents", 2);

                    NPCIndex = new(){
                        "Sebastian", "Sam",
                        "Maru", "Abigail",
                        "Robin", "Demetrius", "Linus",
                    };
                    
                    ModUtility.UpdateFriendship(Game1.player, NPCIndex, friendship);
                    
                    break;

                case villagerLocales.town:

                    Mod.instance.CastDisplay("Prevented " + questRating + " acts of destruction, gained " + friendship + " friendship with town residents", 2);

                    NPCIndex = new(){
                        "Alex", "Elliott", "Harvey",
                        "Emily", "Penny",
                        "Caroline", "Clint", "Evelyn", "George", "Gus", "Jodi", "Lewis", "Pam", "Pierre", "Vincent",
                    };
                    
                    ModUtility.UpdateFriendship(Game1.player, NPCIndex, friendship);
                    
                    break;

                case villagerLocales.forest:

                    Mod.instance.CastDisplay("Destroyed " + questRating + " slimes, gained " + friendship + " friendship with forest residents", 2);

                    NPCIndex = new(){
                        "Shane",
                        "Leah", "Haley",
                        "Marnie", "Jas", "Wizard", "Willy",
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

                    switch (name)
                    {

                        case "Harvey":

                            reaction.Add("Be careful, " + Game1.player.Name + ". The dust and vapours from those impacts could be hazardous! $s");

                            break;

                        case "Maru":

                            reaction.Add("Ooh! A meteor shower! Did you know that the difference between a meteor and an asteroid is whether they've entered the atmosphere of a planet? $h");

                            break;

                        case "Kent":

                            reaction.Add("Aerial bombardment! Take Cover! $a");

                            reaction.Add("It's over... you saw that right? I didn't imagine it. I didn't... bring it home.");

                            break;

                        case "Krobus":

                            reaction.Add("My people hide underground for many reasons. You've just demonstrated one of them. $s");

                            break;

                        case "Jodi":

                            reaction.Add("Explosions. I wonder if...");

                            reaction.Add("Sorry, my thoughts wandered to somewhere else.");

                            break;

                        default:

                            break;

                    }

                    break;

                case ReactionData.reactions.mists:

                    switch (name)
                    {

                        case "Sebastian":

                            reaction.Add("I think I'd be good at that if you taught me.");

                            reaction.Add("On second thought, I don't think I want to risk bolting myself. $h");

                            break;

                        case "Sam":

                            reaction.Add("That just gave me the best album-cover idea.");

                            break;

                        case "Demetrius":

                            reaction.Add("This is an anomalous weather pattern for this region. There haven't been recordings of such phenomena since...");

                            reaction.Add("(Demetrius is deep in thought)");

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
