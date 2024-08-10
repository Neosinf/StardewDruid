using Microsoft.Xna.Framework.Media;
using StardewDruid.Cast.Bones;
using StardewDruid.Cast.Weald;
using StardewDruid.Character;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.GameData.Characters;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading.Channels;
using static StardewDruid.Data.ReactionData;
using static StardewValley.Minigames.BoatJourney;

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
            Leo,
            Dwarf,
            Marlon,
        }

        public static void CommunityFriendship(villagerLocales locale = villagerLocales.mountain, int friendship = 250, int questRating = 1)
        {
            List<string> NPCIndex;

            friendship = Math.Min(friendship, 375);

            switch (locale)
            {
                default:
                case villagerLocales.mountain:

                    Mod.instance.CastDisplay(
                        Mod.instance.Helper.Translation.Get("VillagerData.11").Tokens(new { rating = questRating, friendship = friendship,}),2);

                    NPCIndex = new(){
                        villagers.Sebastian.ToString(), villagers.Sam.ToString(),
                        villagers.Maru.ToString(), villagers.Abigail.ToString(),
                        villagers.Robin.ToString(), villagers.Demetrius.ToString(), villagers.Linus.ToString(),
                    };

                    ModUtility.UpdateFriendship(Game1.player, NPCIndex, friendship);

                    break;

                case villagerLocales.town:

                    Mod.instance.CastDisplay(Mod.instance.Helper.Translation.Get("VillagerData.27").Tokens(new { rating = questRating, friendship = friendship, }), 2);

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

                    Mod.instance.CastDisplay(Mod.instance.Helper.Translation.Get("VillagerData.46").Tokens(new { rating = questRating, friendship = friendship, }), 2);

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

        public static List<string> CustomReaction(ReactionData.reactions react, NPC entity, int rating = 0)
        {
            List<string> reaction = new();

            CharacterData customData = entity.GetData();

            if (customData != null && customData.CustomFields != null)
            {

                string reactionValue;

                switch (react)
                {

                    //StardewDruid.Reactions.Weald(character witnesses or feels a gentle touch of magic)
                    //StardewDruid.Reactions.Weald.neutral(reaction when low friendship)
                    //StardewDruid.Reactions.Weald.like(reaction when medium friendship with player)
                    //StardewDruid.Reactions.Weald.love(reaction when high friendship with player)

                    case ReactionData.reactions.weald:

                        if(customData.CustomFields.TryGetValue("StardewDruid.Reactions.Weald", out reactionValue))
                        {

                            return new() { reactionValue, };

                        };

                        if (Game1.player.friendshipData[entity.Name].Points >= 1500)
                        {

                            if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Weald.love", out reactionValue))
                            {
                                entity.doEmote(20, true);
                                return new() { reactionValue, };

                            };

                        }
                        else if (Game1.player.friendshipData[entity.Name].Points >= 750)
                        {

                            if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Weald.like", out reactionValue))
                            {
                                entity.doEmote(32, true);
                                return new() { reactionValue, };

                            };

                        }

                        if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Weald.neutral", out reactionValue))
                        {
                            entity.doEmote(24, true);
                            return new() { reactionValue, };

                        };

                        break;

                    //StardewDruid.Reactions.Mists(character hit by lightning)
                    case ReactionData.reactions.mists:

                        if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Mists", out reactionValue))
                        {
                            entity.doEmote(28, true);
                            return new() { reactionValue, };

                        };

                        break;

                    //StardewDruid.Reactions.Stars(character hit by a meteorite)
                    case ReactionData.reactions.stars:

                        if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Stars", out reactionValue))
                        {
                            entity.doEmote(28, true);
                            return new() { reactionValue, };

                        };

                        break;

                    //StardewDruid.Reactions.Fates
                    //StardewDruid.Reactions.Fates.dislike(character doesn't like the trick produced by the Rite of the fates)
                    //StardewDruid.Reactions.Fates.neutral
                    //StardewDruid.Reactions.Fates.like
                    //StardewDruid.Reactions.Fates.love(character loves the trick produced)
                    case ReactionData.reactions.fates:

                        if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Fates", out reactionValue))
                        {

                            return new() { reactionValue, };

                        };

                        if (rating >= 75)
                        {

                            if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Fates.love", out reactionValue))
                            {
                                entity.doEmote(20, true);
                                return new() { reactionValue, };

                            };

                        }
                        else
                        if (rating >= 50)
                        {

                            if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Fates.like", out reactionValue))
                            {
                                entity.doEmote(32, true);
                                return new() { reactionValue, };

                            };

                        }
                        else
                        if (rating >= 25)
                        {

                            if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Fates.neutral", out reactionValue))
                            {
                                entity.doEmote(24, true);
                                return new() { reactionValue, };

                            };

                        }

                        if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Fates.dislike", out reactionValue))
                        {
                            entity.doEmote(28, true);
                            return new() { reactionValue, };

                        };

                        break;

                    //StardewDruid.Reactions.Dragon(character sees player in dragon form)
                    //StardewDruid.Reactions.Dragon.neutral(reaction when low friendship)
                    //StardewDruid.Reactions.Dragon.like(reaction when medium friendship with player)
                    //StardewDruid.Reactions.Dragon.love(reaction when high friendship with player)
                    case ReactionData.reactions.dragon:

                        if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Dragon", out reactionValue))
                        {

                            return new() { reactionValue, };

                        };

                        if (Game1.player.friendshipData[entity.Name].Points >= 1500)
                        {

                            if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Dragon.love", out reactionValue))
                            {
                                entity.doEmote(20, true);
                                return new() { reactionValue, };

                            };

                        }
                        else if (Game1.player.friendshipData[entity.Name].Points >= 750)
                        {

                            if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Dragon.like", out reactionValue))
                            {
                                entity.doEmote(32, true);
                                return new() { reactionValue, };

                            };

                        }

                        if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Dragon.neutral", out reactionValue))
                        {
                            entity.doEmote(24, true);
                            return new() { reactionValue, };

                        };

                        break;

                    //StardewDruid.Reactions.Jester(Jester rubs up against the character)
                    case ReactionData.reactions.jester:

                        if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Jester", out reactionValue))
                        {

                            return new() { reactionValue, };

                        };

                        break;

                    //StardewDruid.Reactions.Corvid(character is mobbed by a flock of corvids.Small chance they are pickpocketed and are aware or oblivious)
                    //StardewDruid.Reactions.Corvid.dislike(when pickpocketed by the magpie)
                    case ReactionData.reactions.corvid:

                        if(rating > 0)
                        {
                            
                            if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Corvid", out reactionValue))
                            {
                                entity.doEmote(28, true);
                                return new() { reactionValue, };

                            };

                        }

                        if (customData.CustomFields.TryGetValue("StardewDruid.Reactions.Corvid.dislike", out reactionValue))
                        {

                            return new() { reactionValue, };

                        };

                        break;

                }

            }

            if (Enum.TryParse<villagers>(entity.Name, out villagers result))
            {

                switch (react)
                {

                    case ReactionData.reactions.stars:

                        switch (result)
                        {

                            case villagers.Harvey:

                                reaction.Add(Mod.instance.Helper.Translation.Get("VillagerData.80").Tokens(new { name = Game1.player.Name }));

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

                        switch (result)
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
                        [ReactionData.portraits.neutral] = "$0",
                        [ReactionData.portraits.happy] = "$0",
                        [ReactionData.portraits.sad] = "$h",
                        [ReactionData.portraits.unique] = "$h",
                        [ReactionData.portraits.love] = "$0",
                        [ReactionData.portraits.angry] = "$h",
                    };

                    break;

            }

            return shots;

        }


        public static int Affinity(NPC entity)
        {

            int affinity = 3;

            CharacterData customData = entity.GetData();

            if (customData != null && customData.CustomFields != null)
            {
                if(customData.CustomFields.TryGetValue("StardewDruid.Reactions.Affinity", out string affinityValue))
                {
                    
                    int affinityParse = Convert.ToInt32(affinityValue);

                    if (affinityParse < -1 || affinityParse > 4)
                    {

                        return 3;

                    }

                    return affinityParse;

                }

            }

            if (Enum.TryParse<villagers>(entity.Name, out villagers result))
            {

                Dictionary<villagers, int> villagerAffinities = new()
                {

                    // Skeptical / Worldly
                    [villagers.Demetrius] = 0,
                    [villagers.Pam] = 0,
                    [villagers.Penny] = 0,
                    [villagers.Kent] = 0,
                    [villagers.Harvey] = 0,
                    [villagers.Maru] = 0,
                    [villagers.Robin] = 0,

                    // Ignorant / Don't know
                    [villagers.Jas] = 1,
                    [villagers.Alex] = 1,
                    [villagers.Vincent] = 1,
                    [villagers.Sam] = 1,
                    [villagers.Haley] = 1,
                    [villagers.Marnie] = 1,
                    [villagers.Leo] = 1,

                    // Indifferent / Unconcerned
                    [villagers.Shane] = 2,
                    [villagers.Jodi] = 2,
                    [villagers.Gus] = 2,
                    [villagers.Lewis] = 2,
                    [villagers.Pierre] = 2,
                    [villagers.George] = 2,

                    // Enthusiastic / Inspired
                    [villagers.Leah] = 3,
                    [villagers.Emily] = 3,
                    [villagers.Elliott] = 3,
                    [villagers.Abigail] = 3,
                    [villagers.Sebastian] = 3,
                    [villagers.Caroline] = 3,

                    // Matter of Fact / Concerned
                    [villagers.Evelyn] = 4,
                    [villagers.Clint] = 4,
                    [villagers.Dwarf] = 4,
                    [villagers.Marlon] = 4,
                    [villagers.Willy] = 4,

                    // Esoteric Knowledge
                    [villagers.Krobus] = 5,
                    [villagers.Linus] = 5,
                    [villagers.Wizard] = 5,

                };

                if (villagerAffinities.ContainsKey(result))
                {

                    return villagerAffinities[result];

                }

            }

            Dictionary<string, int> affinities = new()
            {

                // Can't talk
                ["FC.Roswell"] = -1,
                ["FC.Boxy"] = -1,

                ["Mateo"] = 4,

            };

            if (affinities.ContainsKey(entity.Name))
            {

                return affinities[entity.Name];

            }

            return affinity;

        }

    }

}
