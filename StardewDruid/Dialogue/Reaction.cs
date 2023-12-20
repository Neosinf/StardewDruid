using Netcode;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Network;
using System;
using System.Collections.Generic;

namespace StardewDruid.Dialogue
{
    public static class Reaction
    {
        public static void ReactTo(NPC NPC, string cast, int friendship = 0, List<string> context = null)
        {

            List<string> stringList = new List<string>();

            int random = new Random().Next(3);

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

                                    stringList.Add("One day I might have the courage to soar through the sky with you. $l");

                                    break;

                                case 1:

                                    stringList.Add("It's so great to have a resident Dragon warrior looking out for us. $h");

                                    stringList.Add("It's like something out of the old legends. $h");

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

                                    stringList.Add("Can you breath fire?");

                                    stringList.Add("Can you fly?");

                                    stringList.Add("Tell me everything! $h");

                                    break;

                                case 1:

                                    stringList.Add("I thought I heard something massive stomping along the wayside!");

                                    stringList.Add("Guess it would be our friendly neighbourhood Dragon. $h");

                                    break;

                                default:

                                    stringList.Add("Thank you for protecting our town from evil $h");

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

                            stringList.Add("(" + NPC.Name + " trembles with fear and uncertainty). $s");

                            break;

                        default:

                            NPC.doEmote(16, true);

                            stringList.Add("Farmer "+ Game1.player.Name +"?");

                            stringList.Add("I can't believe this happened to you.");

                            stringList.Add("Were you cursed by something?");

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

                            default:

                                NPC.doEmote(20, true);

                                stringList.Add("I just had an out of body experience.");

                                stringList.Add("My perception of reality is all messed up.");

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

                                stringList.Add("Nice one Farmer "+ Game1.player.Name +".");

                                stringList.Add("I can picture you in a big show!");

                                stringList.Add("I would go to see it. $h");

                                break;

                            case 1:

                                NPC.showTextAboveHead("Hehe");

                                stringList.Add("That was hilarious.");

                                stringList.Add("Do it again for me sometime! $h");

                                break;


                            default:

                                NPC.doEmote(32, true);

                                stringList.Add("That was great! You've brightened my day. $h");

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

                                stringList.Add("But maybe trying too hard to be funny?");

                                stringList.Add("Just be yourself.");

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

                            stringList.Add("Let's not talk about this.");

                            break;

                        case 1:

                            NPC.showTextAboveHead("Eeek!");

                            stringList.Add("No thanks. I don't need any more mischief in my life. $s");

                            break;

                        default:

                            NPC.doEmote(28, true);

                            stringList.Add("(grumble) $a");

                            break;

                    }

                    break;
                
                case "Stars":

                    NPC.doEmote(8, true);

                    switch (random)
                    {
                        case 0:

                            stringList.Add("Wow farmer "+Game1.player.Name+", you're very strong.");

                            stringList.Add("Please keep the valley safe.");

                            break;

                        case 1:

                            stringList.Add("The grief of the Sisters showers the valley in a hail of fire.");

                            break;

                        default:

                            stringList.Add("By Yoba. The sky opened up, and, fire came down, and, and, I blanked out.");

                            stringList.Add("Is the time foretold in the valley chronicles upon us?");

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

                            stringList.Add("Was that a bolt of lightning?");

                            stringList.Add("You might have the favour or the ire of the Lady upon you");

                            break;

                        default:

                            stringList.Add("It's like the clouds follow you around. $s");

                            break;

                    }

                    break;
                
                case "Weald":
                    
                    if (Game1.player.friendshipData.ContainsKey(NPC.Name))
                    {
                        
                        if (Game1.player.friendshipData[NPC.Name].Points >= 1000)
                        {
                            NPC.doEmote(20, true);

                            switch (random)
                            {
                                case 0:

                                    stringList.Add("I want to visit sometime, and see all the fantastic things you've brought to life. $l");

                                    break;

                                case 1:

                                    stringList.Add("You're a fantastic farmer, but also a dedicated Druid! $l");

                                    stringList.Add("The valley is in good hands. $h");

                                    break;


                                default:

                                    stringList.Add("It's as if nature sings when you walk into town. $l");

                                    break;
                            }

                        
                        }
                        
                        if (Game1.player.friendshipData[NPC.Name].Points >= 500)
                        {
                            
                            NPC.doEmote(32, true);

                            switch (random)
                            {
                                case 0:

                                    stringList.Add("Now I'm sure there's some kind of forest spirit at work.");

                                    stringList.Add("Random trees appear every morning, and old things get fixed miraculously overnight!");

                                    stringList.Add("I hope they don't steal my slippers.");

                                    break;

                                case 1:

                                    stringList.Add("I saw the way you caught fish from the stream yesterday.");

                                    stringList.Add("The fish must really love your voice or something. $h");

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

                            stringList.Add("The flowers and birds have come back.");

                            stringList.Add("I think there are good things to come. $h");

                            break;

                        case 1:

                            NPC.showTextAboveHead("Huh?");

                            stringList.Add("Why are you speaking strange words and waving your hands like that.");

                            stringList.Add("Is it some kind of farmer ritual?");

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
