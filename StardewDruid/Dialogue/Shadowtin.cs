using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Locations;
using System.Collections.Generic;
using System.IO;

namespace StardewDruid.Dialogue
{
    public class Shadowtin : StardewDruid.Dialogue.Dialogue
    {
        public bool lessonGiven;

        public override void DialogueApproach()
        {
            if (specialDialogue.Count > 0)
            {

                DialogueSpecial();

            }
            else if (Mod.instance.QuestOpen("approachShadowtin"))
            {

                DialogueIntro();
            
            }
            else
            {
                
                string str = "Shadowtin's ethereal eyes shine through a cold metal mask";

                List<Response> responseList = new List<Response>();

                List<string> stringList = QuestData.StageProgress();

                string questText = "(quests) What are the latest treasure prospects?";

                responseList.Add(new Response("quests", questText));

                if (Context.IsMainPlayer)
                {
                    
                    responseList.Add(new Response("relocate", "(move) Lets talk about our partnership."));

                }

                if (npc.priorities.Contains("standby"))
                {

                    responseList.Add(new Response("continue", "(continue) I've finished my business here."));

                }
                else if (npc.priorities.Contains("track"))
                {
                    
                    responseList.Add(new Response("standby", "(standby) I have some business to attend to."));

                }

                responseList.Add(new Response("rites", "(talk) I want to talk about some things"));

                responseList.Add(new Response("none", "(say nothing)"));

                GameLocation.afterQuestionBehavior questionBehavior = new(AnswerApproach);
                
                returnFrom = null;
                
                Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);
            
            }
       
        }

        public override void AnswerApproach(Farmer visitor, string answer)
        {

            switch (answer)
            {
                case "rites":
                    new Rites(npc).Approach();
                    break;
                case "quests":
                case "journey":
                    new Quests(npc).Approach();
                    break;
                case "relocate":
                    DelayedAction.functionAfterDelay(DialogueRelocate, 100);
                    break;
                case "standby":
                    DelayedAction.functionAfterDelay(ReplyStandby, 100);
                    break;
                case "continue":
                    DelayedAction.functionAfterDelay(ReplyContinue, 100);
                    break;

            }

        }

        public void AnswerIntro(Farmer visitor, string answer)
        {

            switch (answer)
            {
                case "introtwo":
                    DelayedAction.functionAfterDelay(DialogueIntroTwo, 100);
                    return;
                case "introthree":
                    DelayedAction.functionAfterDelay(DialogueIntroThree, 100);
                    return;
                case "accept":
                    ReplyAccept();
                    return;
                case "refuse":
                    ReplyRefuse();
                    return;
            }

            Game1.drawDialogue(npc, "The shadow warrior continues to watch you.");

        }

        public void DialogueIntro()
        {
            List<Response> responseList = new List<Response>();
            string str = "Masked Shadow: Hail to you, Dragon master.";
            responseList.Add(new Response("introtwo", "Greetings, shadow warrior. I am " + Game1.player.Name));
            responseList.Add(new Response("cancel", "(ignore him)"));
            GameLocation.afterQuestionBehavior questionBehavior = new(AnswerIntro);
            Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);
        }

        public void DialogueIntroTwo()
        {
            List<Response> responseList = new List<Response>();
            string str = "Masked Shadow: Shadowtin Bear, professional treasure hunter, ready for hire. If you're amenable to a new partnership, I'd be honoured to join your crew.";
            responseList.Add(new Response("introthree", "I might be open to the idea, once you return all the dragon treasures your shady friends took."));
            responseList.Add(new Response("introthree", "I have no reason to trust a thief who answers to the whims of Lord Deep"));
            GameLocation.afterQuestionBehavior questionBehavior = new(AnswerIntro);
            Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);
        }

        public void DialogueIntroThree()
        {
            List<Response> responseList = new List<Response>();
            string str = "Shadowtin Bear: I have a different ambition than my simpler, former colleagues. I believe Lord Deep has misled the shadowfolk to their ruin, and the truth of the matter lies on the path before you.";
            //They've already given everything we gathered to the Lord, except for the few things I've retained for study, as my desire is to discover and reveal the lost history of my folk, a history shrouded by the sweetened words of the Deep one.I believe the truth will be revealed on the path set before you.
            responseList.Add(new Response("accept", "I've heard enough! Welcome to the circle of druids."));
            responseList.Add(new Response("refuse", "I have a better idea. Shadowshift back to your master and tell him I'm coming for him. Soon."));
            GameLocation.afterQuestionBehavior questionBehavior = new(AnswerIntro);
            Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);
        }

        public void ReplyAccept()
        {
            Game1.drawDialogue(npc, "You have shown wisdom, Dragon master. I make my services and research available to you.");
            CompleteIntro();
        }

        public void ReplyRefuse()
        {
            Game1.drawDialogue(npc, "The surface expedition has failed, and my shadowhome will offer no protection from the wrath of Deep. I'll consult your companions. I'm sure they will see the opportunities you do not.");
            CompleteIntro();
        }

        public void CompleteIntro()
        {   
            if(npc.currentLocation is not FarmCave)
            {
                Mod.instance.CastMessage("Shadowtin has moved to the farm cave", -1);
            }
            
            (npc as StardewDruid.Character.Shadowtin).SwitchDefaultMode();
            Mod.instance.CompleteQuest("approachShadowtin");
            QuestData.NextProgress();
            Mod.instance.CharacterRegister(nameof(Shadowtin), "FarmCave");
            npc.WarpToDefault();
        }

        public void DialogueRelocate()
        {
            
            List<Response> responseList = new List<Response>();
            
            string str = "Shadowtin Bear: When's the next raid?";
            
            bool flag = npc.priorities.Contains("track");
            
            if (npc.DefaultMap == "FarmCave" || flag)
            {
                
                responseList.Add(new Response("Farm", "How about you explore the farm. (relocate to farm)"));
            
            }
            
            if (npc.DefaultMap == "Farm" || flag)
            {
                
                responseList.Add(new Response("FarmCave", "I'm sure the farmcave is full of secrets to uncover. (relocate to farmcave)"));
            
            }
            
            if (!flag && QuestData.StageProgress().Contains("ether"))
            {
                
                responseList.Add(new Response("Follow", "Let's go on a treasure hunt. (Shadowtin Bear will join you on your adventures)"));
            
            }  
            
            responseList.Add(new Response("return", "(nevermind)"));

            GameLocation.afterQuestionBehavior questionBehavior = new(AnswerRelocate);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);

        }

        public void AnswerRelocate(Farmer visitor, string answer)
        {
            string str = "I'll be happy to get somewhere dark and shady again.";
            switch (answer)
            {
                case "FarmCave":
                    
                    Mod.instance.CharacterRegister(nameof(Shadowtin), "FarmCave");
                    npc.WarpToDefault();
                    (npc as StardewDruid.Character.Shadowtin).SwitchDefaultMode();
                    Mod.instance.CastMessage("Shadowtin has moved to the farm cave", -1);
                    return;
                case "Farm":
                    str = "Lets see how profitable this agricultural venture is.";
                    Mod.instance.CharacterRegister(nameof(Shadowtin), "Farm");
                    npc.WarpToDefault();
                    (npc as StardewDruid.Character.Shadowtin).SwitchRoamMode();
                    Mod.instance.CastMessage("Shadowtin now roams the farm", -1);
                    break;
                case "Follow":
                    str = "Indeed. How about we split the spoils fifty fifty.";
                    (npc as StardewDruid.Character.Shadowtin).SwitchFollowMode();
                    Mod.instance.CastMessage("Shadowtin joins you on your adventures", -1);
                    break;
            }
            Game1.drawDialogue(npc, str);
        }

        public void ReplyStandby()
        {
            string str = "Time to practice sounding my Carnyx.";
            npc.ActivateStandby();
            Game1.drawDialogue(npc, str);
        }

        public void ReplyContinue()
        {
            string str = "Did you find anything? Perhaps the entrance to the lair of a treasure hoarding monster?";
            npc.DeactivateStandby();
            Game1.drawDialogue(npc, str);
        }

    }

}
