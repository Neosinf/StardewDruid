using Microsoft.VisualBasic;
using Netcode;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Characters;
using StardewValley.Locations;
using StardewValley.Network;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using static StardewDruid.Data.IconData;
using xTile.Dimensions;
using Microsoft.Xna.Framework.Media;
using StardewValley.Minigames;

namespace StardewDruid.Data
{
    public static class ReactionData
    {

        /*
        $neutral	Switch the speaking character to their neutral portrait.
        $h	Switch the speaking character to their happy portrait.
        $s	Switch the speaking character to their sad portrait.
        $u	Switch the speaking character to their unique portrait.
        $l	Switch the speaking character to their love portrait.
        $a	Switch the speaking character to their angry portrait.
        
        row	index	purpose
        1	0–3	animation used to start an emote (I think)
        2	4–7	empty can
        3	8–11	question mark
        4	12–15	angry
        5	16–19	exclamation
        6	20–23	heart
        7	24–27	sleep
        8	28–31	sad
        9	32–35	happy
        10	36–39	x
        11	40–43	pause
        12	44–47	not used
        13	48–51	not used
        14	52–55	videogame
        15	56–59	music note
        16	60–63	blush 

         */

        public enum reactions
        {
            weald,
            stars,
            mists,
            fates,
            dragon,
            jester,
            corvid,

        }

        public enum portraits
        {
            neutral,
            happy,
            sad,
            unique,
            love,
            angry

        }

        public const string fullStop = ".";

        public const string questionMark = "?";

        public const string quotationMark = "\"";

        public const string leftParenthesis = "(";

        public static void ReactTo(NPC entity, reactions reaction, int friendship = 0, List<int> context = null)
        {

            Mod.instance.AddWitness(reaction, entity.Name);


            // Custom Reactions

            List<string> stringList = VillagerData.CustomReaction(reaction, entity);

            if (stringList.Count > 0)
            {

                for (int index = stringList.Count - 1; index >= 0; --index)
                {

                    string str = stringList[index];

                    entity.CurrentDialogue.Push(new StardewValley.Dialogue(entity, "0", str));

                }

                return;

            }

            int affinity = VillagerData.Affinity(entity);

            if(affinity == -1)
            {

                return;

            }

            string place = Mod.instance.Helper.Translation.Get("ReactionData.1");

            if (entity.currentLocation.Name.Contains("Island"))
            {

                place = Mod.instance.Helper.Translation.Get("ReactionData.2");

            }

            Dictionary<portraits, string> shots = VillagerData.ReactionPortraits(entity.Name);

            switch (reaction)
            {

                case reactions.dragon:

                    if (Game1.player.friendshipData.ContainsKey(entity.Name))
                    {

                        if (Game1.player.friendshipData[entity.Name].Points >= 1500)
                        {

                            entity.doEmote(20, true);

                            switch (affinity)
                            {
                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.21"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.23") + shots[portraits.happy]);

                                    break;

                                case 1:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.29") + shots[portraits.love]);

                                    break;

                                case 2:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.35") + shots[portraits.happy]);

                                    break;

                                case 3:


                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.42").Tokens(new { place = place }));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.44") + shots[portraits.love]);

                                    break;

                                case 4:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.50"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.52")
                                        + place +
                                        Mod.instance.Helper.Translation.Get("ReactionData.54") + shots[portraits.happy]);

                                    break;

                                case 6:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.1") + shots[portraits.happy]);

                                    break;

                                default:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.60"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.62"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.64"));

                                    break;

                               
                            }

                            break;

                        }

                        if (Game1.player.friendshipData[entity.Name].Points >= 1000)
                        {

                            entity.doEmote(32, true);

                            switch (affinity)
                            {

                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.83"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.85") + shots[portraits.happy]);

                                    break;

                                case 1:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.91"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.93") + shots[portraits.happy]);

                                    break;

                                case 2:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.99"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.101"));

                                    break;

                                case 3:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.107"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.109") + shots[portraits.happy]);

                                    break;

                                case 4:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.115"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.117"));

                                    break;

                                case 6:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.2"));

                                    break;
                                default:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.123"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.125"));

                                    break;

                            }

                            break;

                        }

                    }

                    switch (affinity)
                    {
                        case 0:

                            List<string> alertListOne = new()
                            {
                                Mod.instance.Helper.Translation.Get("ReactionData.142"),
                                Mod.instance.Helper.Translation.Get("ReactionData.143")
                            };

                            entity.showTextAboveHead(alertListOne[new Random().Next(2)]);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.148").Tokens(new { name = Game1.player.Name }));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.150") + shots[portraits.sad]);

                            break;

                        case 1:

                            List<string> alertListTwo = new()
                            {
                                Mod.instance.Helper.Translation.Get("ReactionData.158"),
                                Mod.instance.Helper.Translation.Get("ReactionData.159"),
                                Mod.instance.Helper.Translation.Get("ReactionData.160"),
                            };

                            entity.showTextAboveHead(alertListTwo[new Random().Next(3)]);

                            stringList.Add(leftParenthesis + entity.Name + Mod.instance.Helper.Translation.Get("ReactionData.165") + shots[portraits.sad]);

                            break;

                        case 2:

                            entity.doEmote(16, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.173"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.175") + shots[portraits.sad]);

                            break;

                        case 3:

                            entity.doEmote(16, true);

                            stringList.Add(Game1.player.Name + questionMark);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.185"));

                            break;

                        case 4:


                            List<string> alertList = new()
                            {
                                Mod.instance.Helper.Translation.Get("ReactionData.194"),
                                Mod.instance.Helper.Translation.Get("ReactionData.195"),
                                Mod.instance.Helper.Translation.Get("ReactionData.196"),
                            };

                            entity.showTextAboveHead(alertList[new Random().Next(3)]);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.201"));

                            break;

                        case 6:

                            entity.doEmote(16, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.3") + shots[portraits.sad]);

                            break;

                        default:

                            entity.doEmote(16, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.209") +
                                place + Mod.instance.Helper.Translation.Get("ReactionData.210"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.212") + shots[portraits.sad]);

                            break;
                    }

                    break;

                case reactions.fates:

                    string trick = Mod.instance.Helper.Translation.Get("ReactionData.220");

                    switch (context.First())
                    {

                        case 0:
                            trick = Mod.instance.Helper.Translation.Get("ReactionData.226");
                            break;
                        case 1:
                            trick = Mod.instance.Helper.Translation.Get("ReactionData.229");
                            break;
                        case 2:
                            trick = Mod.instance.Helper.Translation.Get("ReactionData.232");
                            break;

                    }

                    if (friendship >= 75)
                    {

                        switch (affinity)
                        {
                            case 0:

                                entity.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.246") + shots[portraits.happy]);

                                break;

                            case 1:
                                entity.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.253") +
                                    trick +
                                    Mod.instance.Helper.Translation.Get("ReactionData.255") + shots[portraits.love]);


                                break;

                            case 2:


                                entity.showTextAboveHead(Mod.instance.Helper.Translation.Get("ReactionData.263"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.265") + shots[portraits.love]);

                                break;

                            case 3:

                                entity.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.273") + shots[portraits.love]);

                                break;

                            case 4:

                                entity.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.281") + shots[portraits.love]);

                                break;

                            case 6:

                                entity.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.4") + shots[portraits.happy]);

                                break;

                            default:

                                entity.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.289"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.291") + shots[portraits.happy]);

                                break;

                        }

                        break;

                    }

                    if (friendship >= 50)
                    {

                        switch (affinity)
                        {
                            case 0:

                                entity.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.310").Tokens(new { name = Game1.player.Name }));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313") + shots[portraits.happy]);

                                break;

                            case 1:

                                entity.showTextAboveHead(Mod.instance.Helper.Translation.Get("ReactionData.319"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.321") + shots[portraits.happy]);

                                break;

                            case 2:

                                entity.doEmote(32, true);

                                stringList.Add(trick.ToUpper() + Mod.instance.Helper.Translation.Get("ReactionData.329") + shots[portraits.happy]);

                                break;

                            case 3:

                                entity.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.337") + shots[portraits.happy]);

                                break;

                            case 4:

                                entity.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.345") + shots[portraits.happy]);

                                break;

                            case 6:

                                entity.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.5") + shots[portraits.happy]);

                                break;

                            default:

                                entity.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.353"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.355"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.357") + shots[portraits.sad]);

                                break;
                        }

                        break;

                    }

                    if (friendship >= 25)
                    {
                        switch (affinity)
                        {
                            case 0:

                                entity.doEmote(24, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.374"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.376"));
                                break;

                            case 1:

                                entity.showTextAboveHead(Mod.instance.Helper.Translation.Get("ReactionData.381"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.383"));

                                break;

                            case 2:

                                entity.doEmote(24, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.391"));

                                break;

                            case 3:

                                entity.doEmote(24, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.399").Tokens(new { trick = trick, }));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.402"));

                                break;

                            case 4:

                                entity.doEmote(24, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.410"));

                                break;

                            case 6:

                                entity.doEmote(24, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.6"));

                                break;

                            default:

                                entity.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.418"));

                                break;
                        }


                        break;

                    }

                    switch (affinity)
                    {

                        case 0:


                            entity.doEmote(28, true);

                            if (entity is Child)
                            {

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.439") + shots[portraits.sad]);

                            }
                            else
                            {

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.445").Tokens(new { trick = trick, }) + shots[portraits.angry]);

                            }

                            break;

                        case 1:

                            entity.doEmote(28, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.456") + shots[portraits.sad]);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.458"));

                            break;

                        case 2:

                            entity.showTextAboveHead(Mod.instance.Helper.Translation.Get("ReactionData.464"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.466") + shots[portraits.sad]);

                            break;

                        case 3:

                            entity.doEmote(28, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.474") + shots[portraits.sad]);

                            break;

                        case 4:

                            entity.doEmote(28, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.482") + shots[portraits.sad]);

                            break;

                        case 6:

                            entity.doEmote(28, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.7") + shots[portraits.sad]);

                            break;

                        default:

                            entity.doEmote(28, true);

                            stringList.Add(leftParenthesis + entity.Name + Mod.instance.Helper.Translation.Get("ReactionData.490") + shots[portraits.angry]);

                            break;

                    }

                    break;

                case reactions.stars:

                    entity.doEmote(8, true);

                    switch (affinity)
                    {

                        case 0:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.507").Tokens(new { name = Game1.player.Name }));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.510"));

                            break;

                        case 1:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.516"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.518") + shots[portraits.happy]);

                            break;

                        case 2:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.524"));

                            break;

                        case 3:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.530").Tokens(new { name = Game1.player.Name }));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.533").Tokens(new { place = place, }));

                            break;

                        case 4:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.540"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.542") + shots[portraits.sad]);

                            break;

                        default:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.548"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.550"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.552").Tokens(new { place = place, }));

                            break;

                    }

                    break;

                case reactions.mists:

                    entity.doEmote(16, true);

                    switch (affinity)
                    {
                        case 0:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.569"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.571"));

                            break;

                        case 1:

                             stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.586") + shots[portraits.angry]);

                            break;

                        case 2:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.594") + shots[portraits.sad]);

                            break;

                        case 3:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.600").Tokens(new { name = Game1.player.Name, }));

                            break;

                        case 4:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.606") + shots[portraits.sad]);

                            break;

                        case 6:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.580") + shots[portraits.sad]);

                            break;

                        default:

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.612"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.614").Tokens(new { name = Game1.player.Name, }) + shots[portraits.sad]);

                            stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.617"));

                            break;

                    }

                    break;

                case reactions.weald:

                    if (Game1.player.friendshipData[entity.Name].Points >= 1500)
                    {
                        entity.doEmote(20, true);

                        switch (affinity)
                        {
                            case 0:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.635"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.637") + shots[portraits.happy]);

                                break;

                            case 1:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.643") + shots[portraits.happy]);

                                break;

                            case 2:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.649") + shots[portraits.love]);

                                break;

                            case 3:


                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.656").Tokens(new { place = place, }) + shots[portraits.love]);

                                break;

                            case 4:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.663").Tokens(new { place = place, }) + shots[portraits.love]);

                                break;

                            case 6:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.8") + shots[portraits.love]);

                                break;

                            default:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.670").Tokens(new { place = place, }));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.674") + shots[portraits.love]);

                                break;

                        }

                    }
                    else if (Game1.player.friendshipData[entity.Name].Points >= 750)
                    {

                        entity.doEmote(32, true);

                        switch (affinity)
                        {
                            case 0:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.690"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.692"));

                                break;
                            case 1:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.697"));

                                break;

                            case 2:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.703") + shots[portraits.happy]);

                                break;

                            case 3:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.709") + shots[portraits.happy]);

                                break;

                            case 4:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.715"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.717"));

                                break;

                            case 6:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.9") + shots[portraits.love]);

                                break;

                            default:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.723") + shots[portraits.happy]);

                                break;

                        }

                    }
                    else
                    {

                        switch (affinity)
                        {
                            case 0:

                                entity.showTextAboveHead(Mod.instance.Helper.Translation.Get("ReactionData.737"));

                                switch (new Random().Next(3))
                                {

                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.744"));

                                        break;

                                    case 1:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.750"));

                                        break;

                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.756"));

                                        break;
                                }

                                break;

                            case 1:

                                entity.doEmote(8, true);

                                switch (new Random().Next(3))
                                {

                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.772").Tokens(new { place = place, }) + shots[portraits.happy]);

                                        break;

                                    case 1:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.780") + shots[portraits.happy]);

                                        break;

                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.786") + shots[portraits.happy]);

                                        break;
                                }

                                break;

                            case 2:

                                entity.doEmote(8, true);

                                switch (new Random().Next(3))
                                {

                                    case 0:


                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.803") + shots[portraits.happy]);

                                        break;

                                    case 1:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.809"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.811"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.813") + shots[portraits.happy]);

                                        break;

                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.819"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.821"));

                                        break;
                                }

                                break;

                            case 3:

                                entity.doEmote(8, true);

                                switch (new Random().Next(3))
                                {

                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.837") + shots[portraits.sad]);
                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.838") + shots[portraits.happy]);

                                        break;

                                    case 1:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.844"));
                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.845") + shots[portraits.happy]);
                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.846").Tokens(new { name = Game1.player.Name, }));

                                        break;

                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.852").Tokens(new { place = place, }));

                                        break;
                                }

                                break;

                            case 4:

                                entity.doEmote(8, true);

                                switch (new Random().Next(3))
                                {

                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.869").Tokens(new { name = Game1.player.Name, }));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.871"));

                                        break;

                                    case 1:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.877"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.879") + shots[portraits.happy]);

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.881"));

                                        break;

                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.887"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.889"));

                                        break;
                                }

                                break;

                            case 6:

                                entity.doEmote(8, true);

                                switch (new Random().Next(3))
                                {

                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.10") + shots[portraits.happy]);

                                        break;

                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.11"));

                                        break;

                                  
                                }

                                break;

                            default:

                                entity.doEmote(8, true);

                                switch (new Random().Next(2))
                                {
                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.904") + shots[portraits.happy]);

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.906") + shots[portraits.unique]);

                                        break;

                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.912"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.914") + shots[portraits.sad]);

                                        stringList.Add(quotationMark + Mod.instance.Helper.Translation.Get("ReactionData.916") + quotationMark + shots[portraits.unique]);

                                        break;

                                }

                                break;

                        }


                    }

                    break;

                case reactions.jester:

                    entity.doEmote(20, true);

                    switch (affinity)
                    {
                        case 0:

                            switch (Mod.instance.randomIndex.Next(3))
                            {
                                default:
                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.1"));

                                    break;

                                case 1:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.1"));

                                    break;

                                case 2:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.7"));

                                    break;

                            }

                            break;

                        case 1:

                            switch (Mod.instance.randomIndex.Next(3))
                            {
                                default:
                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.2") + shots[portraits.happy]);

                                    break;
                                case 1:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.2") + shots[portraits.happy]);

                                    break;

                                case 2:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.8") + shots[portraits.happy]);

                                    break;

                            }

                            break;

                        case 2:
                            switch (Mod.instance.randomIndex.Next(3))
                            {
                                default:
                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.935"));

                                    break;

                                case 1:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.3"));

                                    break;

                                case 2:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.9"));

                                    break;

                            }

                            break;

                        case 3:

                            switch (Mod.instance.randomIndex.Next(3))
                            {
                                default:
                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.3"));

                                    break;

                                case 1:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.4"));

                                    break;

                                case 2:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.10"));

                                    break;

                            }

                            break;

                        case 4:

                            switch (Mod.instance.randomIndex.Next(3))
                            {
                                default:
                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.4"));

                                    break;

                                case 1:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.5"));

                                    break;

                                case 2:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.11") + Mod.instance.Helper.Translation.Get("ReactionData.332.111"));

                                    break;

                            }

                            break;

                        case 6:

                            switch (new Random().Next(3))
                            {

                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.12") + shots[portraits.happy]);

                                    break;

                                default:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.13") + shots[portraits.happy]);

                                    break;


                            }

                            break;

                        default:

                            switch (Mod.instance.randomIndex.Next(3))
                            {
                                default:
                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.5"));

                                    break;

                                case 1:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.6") + Mod.instance.Helper.Translation.Get("ReactionData.332.61"));

                                    break;

                                case 2:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.332.12"));

                                    break;


                            }

                            break;

                    };

                    break;


                case reactions.corvid:

                    
                    if(friendship > 15)
                    {

                        entity.doEmote(20, true);

                        switch (affinity)
                        {
                            case 0:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.6") + shots[portraits.happy]);

                                break;

                            case 1:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.7") + shots[portraits.happy]);

                                break;

                            case 2:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.8") + shots[portraits.happy]);

                                break;

                            case 3:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.9") + shots[portraits.happy]);

                                break;

                            case 4:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.10") + shots[portraits.happy]);

                                break;

                            case 6:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.14") + shots[portraits.love]);

                                break;

                            default:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.11") + shots[portraits.happy]);

                                break;

                        };

                    }
                    else if (friendship > 0)
                    {

                        entity.doEmote(32, true);

                        switch (affinity)
                        {
                            case 0:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.18"));

                                break;

                            case 1:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.19").Tokens(new { name = Game1.player.Name, }));

                                break;

                            case 2:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.20"));

                                break;

                            case 3:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.21").Tokens(new { name = Game1.player.Name, }));

                                break;

                            case 4:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.22"));

                                break;

                            case 6:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.15"));

                                break;

                            default:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.23").Tokens(new { name = Game1.player.Name, }));

                                break;

                        };

                    }
                    else
                    {
                        
                        entity.doEmote(8, true);

                        switch (affinity)
                        {
                            case 0:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.12"));

                                break;

                            case 1:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.13") + shots[portraits.sad]);

                                break;

                            case 2:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.14"));

                                break;

                            case 3:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.15") + shots[portraits.angry]);

                                break;

                            case 4:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.16"));

                                break;

                            case 6:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.340.16"));

                                break;

                            default:

                                stringList.Add(Mod.instance.Helper.Translation.Get("ReactionData.313.17"));

                                break;

                        };

                    }

                    break;

            }

            // Limit Reactions

            if (Mod.instance.reactionLimits.ContainsKey(reaction))
            {

                if (Mod.instance.reactionLimits[reaction] > 4)
                {

                    return;

                }

            }
            else
            {

                Mod.instance.reactionLimits[reaction] = 0;

            }

            Mod.instance.reactionLimits[reaction] += 1;

            for (int index = stringList.Count - 1; index >= 0; --index)
            {

                string str = stringList[index];

                entity.CurrentDialogue.Push(new StardewValley.Dialogue(entity, 0.ToString(), str));

            }

        }

    }

}
