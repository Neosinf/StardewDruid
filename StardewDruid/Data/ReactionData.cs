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

        public static void ReactTo(NPC NPC, reactions reaction, int friendship = 0, List<int> context = null)
        {

            Mod.instance.AddWitness(reaction, NPC.Name);

            List<string> stringList = VillagerData.CustomReaction(reaction, NPC.Name);

            if (stringList.Count > 0)
            {

                for (int index = stringList.Count - 1; index >= 0; --index)
                {

                    string str = stringList[index];

                    NPC.CurrentDialogue.Push(new StardewValley.Dialogue(NPC, "0", str));

                }

                return;

            }

            Dictionary<string, int> affinities = VillagerData.Affinity();

            int affinity = 3;

            if (affinities.ContainsKey(NPC.Name))
            {

                affinity = affinities[NPC.Name];

            }

            string place = Mod.instance.Helper.Translation.Get("Reaction.place.0");

            if (NPC.currentLocation.Name.Contains("Island"))
            {

                place = Mod.instance.Helper.Translation.Get("Reaction.place.1");

            }

            Dictionary<portraits,string> shots = VillagerData.ReactionPortraits(NPC.Name);

            switch (reaction)
            {

                case reactions.dragon:

                    if (Game1.player.friendshipData.ContainsKey(NPC.Name))
                    {

                        if (Game1.player.friendshipData[NPC.Name].Points >= 1500)
                        {

                            NPC.doEmote(20, true);

                            switch (affinity)
                            {
                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.0.0"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.0.1").Tokens(new { shots = shots[portraits.happy] }));

                                    break;

                                case 1:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.1.0").Tokens(new {shots = shots[portraits.love] }));

                                    break;

                                case 2:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.2.0").Tokens(new { shots = shots[portraits.happy] }));

                                    break;

                                case 3:


                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.3.0").Tokens(new { place = place}));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.3.1").Tokens(new { shots = shots[portraits.love] }));

                                    break;

                                case 4:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.4.0"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.4.1").Tokens(new { place = place,shots = shots[portraits.happy] }));

                                    break;
                                // default -> 5 ?
                                default:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.5.0"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.5.1"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1500.affinity.5.2"));

                                    break;
                            }

                            break;

                        }

                        if (Game1.player.friendshipData[NPC.Name].Points >= 1000)
                        {

                            NPC.doEmote(32, true);

                            switch (affinity)
                            {

                                case 0:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.0.0"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.0.1").Tokens(new { shots = shots[portraits.happy] }));

                                    break;

                                case 1:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.1.0"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.1.1").Tokens(new {shots = shots[portraits.happy]}));

                                    break;

                                case 2:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.2.0"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.2.1"));

                                    break;

                                case 3:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.3.0"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.3.1").Tokens(new{shots=shots[portraits.happy]}));

                                    break;

                                case 4:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.4.0"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.4.1"));

                                    break;
                                // default -> 5 ?
                                default:

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.5.0"));

                                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.1000.affinity.5.1"));

                                    break;

                            }

                            break;

                        }

                    }
                    // friendship -> 0 ?
                    switch (affinity)
                    {
                        case 0:

                            List<string> alertListOne = new()
                            {
                                Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.0.abovehead.0"),
                                Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.0.abovehead.1")
                            };

                            NPC.showTextAboveHead(alertListOne[new Random().Next(2)]);

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.0.0").Tokens(new{playerName=Game1.player.Name}));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.0.1").Tokens(new{shots=shots[portraits.sad]}));

                            break;

                        case 1:

                            List<string> alertListTwo = new()
                            {
                                Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.1.abovehead.0"),
                                Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.1.abovehead.1"),
                                Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.1.abovehead.2"),
                            };

                            NPC.showTextAboveHead(alertListTwo[new Random().Next(3)]);

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.1.0").Tokens(new{npcName=NPC.Name,shots=shots[portraits.sad]}));

                            break;

                        case 2:

                            NPC.doEmote(16, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.2.0"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.2.1").Tokens(new{shots=shots[portraits.sad]}));

                            break;

                        case 3:

                            NPC.doEmote(16, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.3.0").Tokens(new{playerName=Game1.player.Name}));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.3.1"));

                            break;

                        case 4:


                            List<string> alertList = new()
                            {
                                Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.4.abovehead.0"),
                                Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.4.abovehead.1"),
                                Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.4.abovehead.2"),
                            };

                            NPC.showTextAboveHead(alertList[new Random().Next(3)]);

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.4.0"));

                            break;
                        // default -> 5 ?
                        default:

                            NPC.doEmote(16, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.5.0").Tokens(new{place=place}));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.dragon.friendship.0.affinity.5.1").Tokens(new{shots=shots[portraits.sad]}));

                            break;
                    }

                    break;
                case reactions.fates:

                    string trick = Mod.instance.Helper.Translation.Get("Reaction.fates.trick.-1");

                    switch (context.First())
                    {

                        case 0:
                            trick = Mod.instance.Helper.Translation.Get("Reaction.fates.trick.0");
                            break;
                        case 1:
                            trick = Mod.instance.Helper.Translation.Get("Reaction.fates.trick.1");
                            break;
                        case 2:
                            trick = Mod.instance.Helper.Translation.Get("Reaction.fates.trick.2");
                            break;

                    }

                    if (friendship >= 75)
                    {

                        switch (affinity)
                        {
                            case 0:

                                NPC.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.75.affinity.0.0").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                            case 1:
                                NPC.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.75.affinity.1.0").Tokens(new{trick=trick,shots=shots[portraits.love]}));


                                break;

                            case 2:


                                NPC.showTextAboveHead(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.75.affinity.2.abovehead.0"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.75.affinity.2.0").Tokens(new{shots=shots[portraits.love]}));

                                break;

                            case 3:

                                NPC.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.75.affinity.3.0").Tokens(new{shots=shots[portraits.love]}));

                                break;

                            case 4:

                                NPC.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.75.affinity.4.0").Tokens(new{shots=shots[portraits.love]}));

                                break;

                            default:

                                NPC.doEmote(20, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.75.affinity.5.0"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.75.affinity.5.1").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                        }

                        break;

                    }

                    if (friendship >= 50)
                    {

                        switch (affinity)
                        {
                            case 0:

                                NPC.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.50.affinity.0.0").Tokens(new{playerName=Game1.player.Name}));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.50.affinity.0.1").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                            case 1:

                                NPC.showTextAboveHead(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.50.affinity.1.abovehead.0"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.50.affinity.1.0").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                            case 2:

                                NPC.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.50.affinity.2.0").Tokens(new{trick=trick.ToUpper(),shots=shots[portraits.happy]}));

                                break;

                            case 3:

                                NPC.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.50.affinity.3.0").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                            case 4:

                                NPC.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.50.affinity.4.0").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                            default:

                                NPC.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.50.affinity.5.0"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.50.affinity.5.1"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.50.affinity.5.2").Tokens(new{shots=shots[portraits.sad]}));

                                break;
                        }

                        break;

                    }

                    if (friendship >= 25)
                    {
                        switch (affinity)
                        {
                            case 0:

                                NPC.doEmote(24, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.25.affinity.0.0"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.25.affinity.0.1"));
                                break;

                            case 1:

                                NPC.showTextAboveHead(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.25.affinity.1.abovehead.0"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.25.affinity.1.0"));

                                break;

                            case 2:

                                NPC.doEmote(24, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.25.affinity.2.0"));

                                break;

                            case 3:

                                NPC.doEmote(24, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.25.affinity.3.0").Tokens(new{trick=trick}));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.25.affinity.3.1"));

                                break;

                            case 4:

                                NPC.doEmote(24, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.25.affinity.4.0"));

                                break;

                            default:

                                NPC.doEmote(32, true);

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.25.affinity.5.0"));

                                break;
                        }


                        break;

                    }
                    // friendship -> 0 ?
                    switch (affinity)
                    {

                        case 0:


                            NPC.doEmote(28, true);

                            if (NPC is Child)
                            {

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.0.affinity.0.0").Tokens(new{shots=shots[portraits.sad]}));

                            }
                            else
                            {

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.0.affinity.0.1").Tokens(new{trick=trick,shots=shots[portraits.angry]}));

                            }

                            break;

                        case 1:

                            NPC.doEmote(28, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.0.affinity.1.0").Tokens(new{shots=shots[portraits.sad]}));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.0.affinity.1.1"));

                            break;

                        case 2:

                            NPC.showTextAboveHead(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.0.affinity.2.abovehead.0"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.0.affinity.2.0").Tokens(new{shots=shots[portraits.sad]}));

                            break;

                        case 3:

                            NPC.doEmote(28, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.0.affinity.3.0").Tokens(new{shots=shots[portraits.sad]}));

                            break;

                        case 4:

                            NPC.doEmote(28, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.0.affinity.4.0").Tokens(new{shots=shots[portraits.sad]}));

                            break;
                        // defalut -> 5 ?
                        default:

                            NPC.doEmote(28, true);

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.fates.friendship.0.affinity.5.0").Tokens(new{npcName=NPC.Name,shots=shots[portraits.angry]}));

                            break;

                    }

                    break;

                case reactions.stars:

                    NPC.doEmote(8, true);

                    switch (affinity)
                    {

                        case 0:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.0.0").Tokens(new{playerName=Game1.player.Name}));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.0.1"));

                            break;

                        case 1:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.1.0"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.1.1").Tokens(new{shots=shots[portraits.happy]}));

                            break;

                        case 2:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.2.0"));

                            break;

                        case 3:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.3.0").Tokens(new{playerName=Game1.player.Name}));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.3.1").Tokens(new{place=place}));

                            break;

                        case 4:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.4.0"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.4.1").Tokens(new{shots=shots[portraits.sad]}));

                            break;
                        // default -> 5 ?
                        default:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.5.0"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.5.1"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.stars.affinity.5.2").Tokens(new{place=place}));

                            break;

                    }

                    break;

                case reactions.mists:

                    NPC.doEmote(16, true);

                    switch (affinity)
                    {
                        case 0:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.mists.affinity.0.0"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.mists.affinity.0.1"));

                            break;

                        case 1:

                            if (NPC is Child)
                            {

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.mists.affinity.1.0").Tokens(new{shots=shots[portraits.sad]}));

                            }
                            else
                            {

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.mists.affinity.1.1")+shots[portraits.angry]);

                            }

                            break;

                        case 2:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.mists.affinity.2.0").Tokens(new{shots=shots[portraits.sad]}));

                            break;

                        case 3:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.mists.affinity.3.0").Tokens(new{playerName=Game1.player.Name}));

                            break;

                        case 4:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.mists.affinity.4.0").Tokens(new{shots=shots[portraits.sad]}));

                            break;
                        // default -> 5 ?
                        default:

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.mists.affinity.5.0"));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.mists.affinity.5.1").Tokens(new{playerName=Game1.player.Name,shots=shots[portraits.sad]}));

                            stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.mists.affinity.5.2"));

                            break;

                    }

                    break;

                case reactions.weald:

                    if (Game1.player.friendshipData[NPC.Name].Points >= 1500)
                    {
                        NPC.doEmote(20, true);

                        switch (affinity)
                        {
                            case 0:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.1500.affinity.0.0"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.1500.affinity.0.1").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                            case 1:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.1500.affinity.1.0").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                            case 2:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.1500.affinity.2.0").Tokens(new{shots=shots[portraits.love]}));

                                break;

                            case 3:


                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.1500.affinity.3.0").Tokens(new{place=place,shots=shots[portraits.love]}));

                                break;

                            case 4:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.1500.affinity.4.0").Tokens(new{place=place,shots=shots[portraits.love]}));

                                break;

                            default:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.1500.affinity.5.0").Tokens(new{place=place}));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.1500.affinity.5.1").Tokens(new{shots=shots[portraits.love]}));

                                break;

                        }

                    }
                    else if (Game1.player.friendshipData[NPC.Name].Points >= 750)
                    {

                        NPC.doEmote(32, true);

                        switch (affinity)
                        {
                            case 0:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.750.affinity.0.0"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.750.affinity.0.1"));

                                break;
                            case 1:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.750.affinity.1.0"));

                                break;

                            case 2:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.750.affinity.2.0").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                            case 3:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.750.affinity.3.0").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                            case 4:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.750.affinity.4.0"));

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.750.affinity.4.1"));

                                break;
                            // default -> 5?
                            default:

                                stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.750.affinity.5.0").Tokens(new{shots=shots[portraits.happy]}));

                                break;

                        }

                    }
                    // friendship -> 0 ?
                    else
                    {

                        switch (affinity)
                        {
                            case 0:

                                NPC.showTextAboveHead(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.0.abovehead.0"));

                                switch (new Random().Next(3))
                                {

                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.0.0.0"));

                                        break;

                                    case 1:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.0.1.0"));

                                        break;
                                    // default -> 2 ?
                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.0.2.0"));

                                        break;
                                }

                                break;

                            case 1:

                                NPC.doEmote(8, true);

                                switch(new Random().Next(3))
                                {

                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.1.0.0").Tokens(new{place=place,shots=shots[portraits.happy]}));

                                        break;

                                    case 1:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.1.1.0").Tokens(new{shots=shots[portraits.happy]}));

                                        break;
                                    // default -> 2 ?
                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.1.2.0").Tokens(new{shots=shots[portraits.happy]}));

                                        break;
                                }

                                break;

                            case 2:

                                NPC.doEmote(8, true);

                                switch (new Random().Next(3))
                                {

                                    case 0:

                                        
                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.2.0.0").Tokens(new{shots=shots[portraits.happy]}));

                                        break;

                                    case 1:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.2.1.0"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.2.1.1"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.2.1.2").Tokens(new{shots=shots[portraits.happy]}));

                                        break;
                                    // default -> 2 ?
                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.2.2.0"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.2.2.1"));

                                        break;
                                }

                                break;

                            case 3:

                                NPC.doEmote(8, true);

                                switch (new Random().Next(3))
                                {

                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.3.0.0").Tokens(new{shots=shots[portraits.sad]}));
                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.3.0.1").Tokens(new{shots=shots[portraits.happy]}));

                                        break;

                                    case 1:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.3.1.0"));
                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.3.1.1").Tokens(new{shots=shots[portraits.happy]}));
                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.3.1.2").Tokens(new{playerName=Game1.player.Name}));

                                        break;
                                    // default -> 2 ?
                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.3.2.0").Tokens(new{place=place}));

                                        break;
                                }

                                break;

                            case 4:

                                NPC.doEmote(8, true);

                                switch (new Random().Next(3))
                                {

                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.4.0.0").Tokens(new{playerName=Game1.player.Name}));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.4.0.1"));

                                        break;

                                    case 1:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.4.1.0"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.4.1.1").Tokens(new{shots=shots[portraits.happy]}));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.4.1.2"));

                                        break;
                                    // default -> 2 ?
                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.4.2.0"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.4.2.1"));

                                        break;
                                }

                                break;
                            // default -> 5 ?
                            default:

                                NPC.doEmote(8, true);

                                switch (new Random().Next(2))
                                {
                                    case 0:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.5.0.0").Tokens(new{shots=shots[portraits.happy]}));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.5.0.1").Tokens(new{shots=shots[portraits.unique]}));

                                        break;
                                    // default -> 1 ?
                                    default:

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.5.1.0"));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.5.1.1").Tokens(new{shots=shots[portraits.sad]}));

                                        stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.weald.friendship.0.affinity.5.1.2").Tokens(new{shots=shots[portraits.unique]}));

                                        break;
                                
                                }

                                break;

                        }


                    }

                    break;

                case reactions.jester:

                    NPC.doEmote(20, true);

                    stringList.Add(Mod.instance.Helper.Translation.Get("Reaction.jester.0"));

                    break;

            }

            for (int index = stringList.Count - 1; index >= 0; --index)
            {

                string str = stringList[index];

                NPC.CurrentDialogue.Push(new StardewValley.Dialogue(NPC, "0", str));

            }

        }

    }

}
