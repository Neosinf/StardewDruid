using Netcode;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Network;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace StardewDruid.Dialogue
{
    public static class Reaction
    {

        /*
        $neutral	Switch the speaking character to their neutral portrait.
        $h	Switch the speaking character to their happy portrait.
        $s	Switch the speaking character to their sad portrait.
        $u	Switch the speaking character to their unique portrait.
        $l	Switch the speaking character to their love portrait.
        $a	Switch the speaking character to their angry portrait.
        */

        public static void ReactTo(NPC NPC, string cast, int friendship = 0, List<string> context = null)
        {

            List<string> stringList = new List<string>();

            int random = new Random().Next(4);

            switch (cast)
            {

                case "Ether":

                    if (Game1.player.friendshipData.ContainsKey(NPC.Name))
                    {

                        if (Game1.player.friendshipData[NPC.Name].Points >= 1000)
                        {

                            NPC.doEmote(20, true);

                            switch (random)
                            {
                                case 0:

                                    stringList.Add("One day I might have the courage to soar through the sky with you.$l");

                                    break;

                                case 1:

                                    stringList.Add("It's so great to have a resident Dragon warrior looking out for us.$h");

                                    stringList.Add("It's like something out of the old legends. $h");

                                    break;

                                case 2:

                                    stringList.Add("I read somewhere about the return of Dragons. I forgot what it was supposed to mean.");

                                    stringList.Add("I trust you though! $h");

                                    break;

                                default:

                                    stringList.Add("Your scales glitter in the light of the valley.");

                                    stringList.Add("Majestic! $l");

                                    break;
                            }

                            break;

                        }

                        if (Game1.player.friendshipData[NPC.Name].Points >= 500)
                        {

                            NPC.doEmote(32, true);

                            switch (random)
                            {
                                case 0:

                                    stringList.Add("Can you breath fire? Can you fly?");

                                    stringList.Add("Tell me everything! $h");

                                    break;

                                case 1:

                                    stringList.Add("I thought I heard something massive stomping along the wayside!");

                                    stringList.Add("Guess it would be our friendly neighbourhood Dragon. $h");

                                    break;

                                case 2:

                                    stringList.Add("I don't know if I'll ever get used to seeing you like this.");

                                    stringList.Add("I'm sure one day it will be wierd to see you as a human! $h");
                                    
                                    break;

                                default:

                                    stringList.Add("Thank you for protecting our town from evil. $h");

                                    break;
                            }

                            break;

                        }

                    }
                    switch (random)
                    {
                        case 0:

                            List<string> alertList = new()
                            {
                                "Alert the Guild!",
                                "To Arms!",
                                "We're under attack!",
                                "OH NO NO NO NO",
                            };

                            NPC.showTextAboveHead(alertList[new Random().Next(4)]);

                            stringList.Add("Oh... it's farmer " + Game1.player.Name);

                            stringList.Add("I feared the worst $s");

                            break;

                        case 1:

                            NPC.showTextAboveHead("AHHH! DRAGON!!");

                            stringList.Add("(" + NPC.Name + " trembles with fear and uncertainty).$s");

                            break;

                        case 2:

                            NPC.doEmote(16, true);

                            stringList.Add("Oh... hi there big friend...");

                            stringList.Add("If you're looking for a horde of treasure... maybe try the mountain... or even further away.");

                            break;

                        default:

                            NPC.doEmote(16, true);

                            stringList.Add("Farmer "+ Game1.player.Name + "?");

                            stringList.Add("I can't believe this happened to you. Were you cursed by something?");

                            break;
                    }

                    break;

                case "Fates":
                    
                    if (friendship >= 75)
                    {

                        switch (random)
                        {
                            case 0:

                                NPC.doEmote(20, true);

                                stringList.Add("You've picked up so many wonderful skills so quickly.");

                                stringList.Add("It's as if Fate watches over you. $l");

                                break;

                            case 1:

                                NPC.showTextAboveHead("Bravo!");

                                stringList.Add("You've become a master entertainer! $l");

                                break;

                            case 2:

                                NPC.doEmote(20, true);

                                stringList.Add("I think "+context.First()+" is my new favourite trick. $l");

                                break;

                            default:

                                NPC.doEmote(20, true);

                                stringList.Add("I just had an out of body experience. My perception of reality is all messed up.");

                                stringList.Add("It's fantastic! $l");

                                break;

                        }

                        break;
                    
                    }
                    
                    if (friendship >= 50)
                    {

                        switch (random)
                        {
                            case 0:

                                NPC.doEmote(32, true);

                                stringList.Add("Nice one Farmer "+ Game1.player.Name + ". I can picture you in a big show!");

                                stringList.Add("I would go to see it. $h");

                                break;

                            case 1:

                                NPC.showTextAboveHead("Hehe");

                                stringList.Add("That was hilarious.");

                                stringList.Add("Do it again for me sometime! $h");

                                break;

                            case 2:

                                NPC.doEmote(24, true);

                                stringList.Add(context.First() + "! Now that was special.$h");

                                break;

                            default:

                                NPC.doEmote(32, true);

                                stringList.Add("That was great! You've brightened my day.$h");

                                break;

                        }

                        break;
                    
                    }
                    
                    if (friendship >= 25)
                    {
                        switch (random)
                        {
                            case 0:

                                NPC.showTextAboveHead("What?");

                                stringList.Add("I'm confused.");

                                stringList.Add("I guess I missed the point of that.");

                                break;

                            case 1:

                                NPC.doEmote(24, true);

                                stringList.Add("I know you're trying.");

                                stringList.Add("But maybe you're trying too hard to be funny? Just be yourself.");

                                break;

                            case 2:

                                NPC.doEmote(24, true);

                                stringList.Add("Hmmm. Well, I think " + context.First() + " might not be your strongest trick. How about cake out of a hat?");

                                stringList.Add("I mean, who doesn't like cake? Maybe a couple of the older guys.");

                                break;

                            default:

                                NPC.doEmote(24, true);

                                stringList.Add("Well that was an... experience.");

                                break;

                        }

                        
                        break;
                    
                    }

                    switch (random)
                    {

                        case 0:

                            NPC.doEmote(28, true);

                            stringList.Add("Let's not talk about this again.$s");

                            stringList.Add("It's all in the past.");

                            break;

                        case 1:

                            NPC.showTextAboveHead("Eeek!");

                            stringList.Add("No thanks. I don't need any more mischief in my life.$s");

                            break;

                        case 2:

                            NPC.doEmote(28, true);

                            stringList.Add("Ughh. I think I'll be happy if random " + context.First() + " never happen to me again.$a");

                            break;

                        default:

                            NPC.doEmote(28, true);

                            stringList.Add("("+NPC.Name+" grumbles)$a");

                            break;

                    }

                    break;
                
                case "Stars":

                    NPC.doEmote(8, true);

                    switch (random)
                    {
                        case 0:

                            stringList.Add("Wow farmer "+Game1.player.Name+ ", you're very strong.");

                            stringList.Add("Please use your power to keep the valley safe.");

                            break;

                        case 1:

                            stringList.Add("And the grief of the Sisters will overwhelm the heavens and shower the land in a hail of fire.");

                            stringList.Add("At least that's whats enscribed on the top of one of the library bookcases.");

                            break;

                        case 2:

                            stringList.Add("The servants of the Lord of the Deep will cover the land in shadow.");

                            stringList.Add("I can't remember the second part. Something to do with massive fireballs. $h");

                            break;

                        default:

                            stringList.Add("By Yoba. The sky opened up, and, fire came down, and, and, I blanked out.");

                            break;

                    }

                    break;
               
                case "Mists":

                    NPC.doEmote(16, true);

                    switch (random)
                    {
                        case 0:

                            stringList.Add("rain and thunder every day! $a");

                            break;

                        case 1:

                            stringList.Add("Was that a bolt of lightning? ");

                            stringList.Add("I hope you're careful using that around tall poles and bodies of water!");

                            break;

                        case 2:

                            stringList.Add("In the valley we say thunder is the voice of the Lady Beyond the Shore.");

                            stringList.Add("Be careful, "+Game1.player.Name+", you don't want to catch her ire. $s");

                            break;

                        default:

                            stringList.Add("It's like the clouds follow you around. $s");

                            break;

                    }

                    break;
                
                case "Weald":
                    
                    if (Game1.player.friendshipData.ContainsKey(NPC.Name))
                    {
                        
                        if (Game1.player.friendshipData[NPC.Name].Points >= 1500)
                        {
                            NPC.doEmote(20, true);

                            switch (random)
                            {
                                case 0:

                                    stringList.Add("I want to visit sometime, and see all the fantastic things you've brought to life. $l");

                                    break;

                                case 1:

                                    stringList.Add("The valley is in good hands. $h");

                                    break;

                                case 2:

                                    stringList.Add("You have flower petals all over you. $h");

                                    break;

                                default:

                                    stringList.Add("It's as if nature sings when you walk into town. $l");

                                    break;
                            }

                        
                        }
                        
                        if (Game1.player.friendshipData[NPC.Name].Points >= 750)
                        {
                            
                            NPC.doEmote(32, true);

                            switch (random)
                            {
                                case 0:

                                    stringList.Add("Now I'm sure there's some kind of forest spirit at work. Trees are sprouting everything.");

                                    break;

                                case 1:

                                    stringList.Add("I saw the way you caught fish from the stream yesterday.");

                                    stringList.Add("The fish must really love your voice or something. $h");

                                    break;

                                case 2:

                                    stringList.Add("There are gardens sprouting up everywhere this season. $h");

                                    break;

                                default:

                                    stringList.Add("The valley has never felt more alive than since you moved here. $h");

                                    break;
                            }

                        
                        }
                    
                    }

                    switch (random)
                    {
                        case 0:

                            NPC.doEmote(8, true);

                            stringList.Add("Does this place seem greener?");

                            stringList.Add("The flowers and birds have come back. I think there are good things to come. $h");

                            break;

                        case 1:

                            NPC.showTextAboveHead("Huh?");

                            stringList.Add("Why are you speaking strange words and waving your hands like that.");

                            stringList.Add("Is it some kind of farmer ritual?");
                            
                            break;

                        case 2:

                            stringList.Add("How many seeds do you have in your pocket?");
                            
                            stringList.Add("It seems you have an endless amount to throw around.");

                            break;

                        default:

                            NPC.doEmote(8, true);

                            stringList.Add("Did you see that? I could swear the valley shimmered for a moment.");

                            break;
                    }

                    break;

           
            }
            
            for (int index = stringList.Count - 1; index >= 0; --index)
            {
               
                string str = stringList[index];
                
                NPC.CurrentDialogue.Push(new StardewValley.Dialogue(str, NPC));
            
            }
        
        }
    
    }

}
