﻿using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Objects;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Dialogue
{
    public class Effigy : StardewDruid.Dialogue.Dialogue
    {
        public bool lessonGiven;
        public string questFeedback;

        public override void DialogueApproach()
        {
            if (specialDialogue.Count > 0)
                DialogueSpecial();
            else if (Mod.instance.QuestOpen("approachEffigy"))
            {
                Mod.instance.CompleteQuest("approachEffigy");
                Mod.instance.CharacterRegister(nameof(Effigy), "FarmCave");
                DialogueIntro();
            }
            else
            {
                string str = "Forgotten Effigy: ^Successor.";
                List<Response> responseList = new List<Response>();
                List<string> stringList = QuestData.StageProgress();
                if (stringList.Contains("Jester"))
                {

                    if((Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[1] && QuestData.StageProgress().Last<string>() == "Jester")
                    {
                        str = "Forgotten Effigy: ^The Fates watch over you.";
                        responseList.Add(new Response("quests", "What do you mean?"));
                    }
                    else
                    {
                        responseList.Add(new Response("quests", "(quests) What threatens the valley?"));
                    }

                    if (Context.IsMainPlayer)
                    {
                        responseList.Add(new Response("relocate", "(move) It's time for a change of scene"));
                    }
                        
                }
                else if (stringList.Contains("hidden"))
                    responseList.Add(new Response("quests", "(quests) Is the valley safe?"));
                else if (stringList.Contains("stars"))
                    responseList.Add(new Response("quests", "(lessons) I want to master the power of the stars."));
                else if (stringList.Contains("mists"))
                    responseList.Add(new Response("quests", "(lessons) What can you teach me about the mists?"));
                else if (stringList.Contains("weald"))
                    responseList.Add(new Response("quests", "(lessons) I want to learn more about the weald."));
                if (Mod.instance.CurrentProgress() > 2)
                    responseList.Add(new Response("rites", "(talk) I have some requests."));

                if (npc.priorities.Contains("standby"))
                {

                    responseList.Add(new Response("continue", "(continue) Thank you for keeping watch."));

                }
                else if (npc.priorities.Contains("track"))
                {

                    responseList.Add(new Response("standby", "(standby) Can you stand guard for a moment?"));

                }

                responseList.Add(new Response("none", "(say nothing)"));
                Effigy effigy = this;
                GameLocation.afterQuestionBehavior questionBehavior = new(AnswerApproach);
                returnFrom = null;
                Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);
            }
        }

        public override void AnswerApproach(Farmer effigyVisitor, string effigyAnswer)
        {
            switch (effigyAnswer)
            {
                case "intro":
                    DelayedAction.functionAfterDelay(DialogueQuery, 100);
                    break;
                case "quests":
                case "journey":
                    new Quests(npc).Approach();
                    break;
                case "relocate":
                    DelayedAction.functionAfterDelay(DialogueRelocate, 100);
                    break;
                case "Demetrius":
                    DelayedAction.functionAfterDelay(DialogueDemetrius, 100);
                    break;
                case "rites":
                    new Rites(npc).Approach();
                    break;
                case "standby":
                    DelayedAction.functionAfterDelay(ReplyStandby, 100);
                    break;
                case "continue":
                    DelayedAction.functionAfterDelay(ReplyContinue, 100);
                    break;
            }
        }

        public void DialogueIntro()
        {
            string str = "So the successor appears, and has demonstrated remarkable potential. I am the Effigy of the First Farmer, and the sole remnant of my circle of Druids.";
            List<Response> responseList = new List<Response>()
              {
                new Response("intro", "Who stuck you in the ceiling?"),
                new Response("none", "(say nothing)")
              };
            Effigy effigy = this;
            GameLocation.afterQuestionBehavior questionBehavior = new(AnswerApproach);
            Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);
        }

        public void DialogueQuery()
        {
            List<Response> responseList = new List<Response>();
            string str = "Forgotten Effigy: ^I was crafted by the first farmer of the valley, a powerful friend of the otherworld. If you intend to succeed him, you will need to learn many lessons.";
            responseList.Add(new Response("quests", "(start journey) Ok. What is the first lesson?"));
            responseList.Add(new Response("none", "(say nothing)"));
            Effigy effigy = this;
            GameLocation.afterQuestionBehavior questionBehavior = new(AnswerApproach);
            returnFrom = null;
            Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);
        }

        public void DialogueDemetrius()
        {
            string str = "Forgotten Effigy: ^I concealed myself for a time, then I spoke to him in the old tongue of the Calico shamans.";
            List<Response> responseList = new List<Response>()
              {
                new Response("descended", "Do you think Demetrius is descended from the shaman tradition?!"),
                new Response("offended", "Wow, he must have been offended. Demetrius is a man of modern science and sensibilities."),
                new Response("return", "Nope, not going to engage with ")
              };
            GameLocation.afterQuestionBehavior questionBehavior = new(AnswerDemetrius);
            Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);
        }

        public void AnswerDemetrius(Farmer effigyVisitor, string effigyAnswer)
        {
            if (effigyAnswer == "return")
            {
                returnFrom = "demetrius";
                Effigy effigy = this;
                DelayedAction.functionAfterDelay(DialogueApproach, 100);
            }
            else
                Game1.drawDialogue(npc, Game1.player.caveChoice.Value != 1 ? "I can smell the crisp, sandy scent of the Calico variety of mushroom. The shamans would eat them to... enter a trance-like state." : "... ... He came in with a feathered mask on, invoked a rite of summoning, threw Bat feed everywhere, and then left just as quickly as he entered. His shamanic heritage is very... particular.");
        }

        public void DialogueRelocate()
        {
            
            List<Response> responseList = new List<Response>();
            
            string str = "Forgotten Effigy: ^Now that you have vanquished the twisted spectres of the past, it is safe for me to roam the wilds of the Valley once more. Where shall I await your command?";

            bool flag = npc.priorities.Contains("track");

            if (npc.DefaultMap == "FarmCave" || flag)
            {
                responseList.Add(new Response("Farm", "My farm would benefit from your gentle stewardship. (The Effigy will garden around scarecrows on the farm)"));

            }
            
            if (npc.DefaultMap == "Farm" || flag)
            {
                responseList.Add(new Response("FarmCave", "Shelter within the farm cave for the while."));

            }

            if (!flag && QuestData.StageProgress().Contains("fates"))
            {

                responseList.Add(new Response("Follow", "Come see the valley of the new farmer. (Effigy will follow you around)"));

            }

            responseList.Add(new Response("return", "(nevermind)"));

            GameLocation.afterQuestionBehavior questionBehavior = new(AnswerRelocate);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);

        }

        public void AnswerRelocate(Farmer effigyVisitor, string effigyAnswer)
        {
            string str = "(nevermind isn't a place, successor)";
            switch (effigyAnswer)
            {
                case "FarmCave":
                    str = "I will return to where I may feel the rumbling energies of the Valley's leylines.";
                    Mod.instance.CharacterRegister(npc.Name, "FarmCave");
                    npc.WarpToDefault();
                    (npc as StardewDruid.Character.Effigy).SwitchDefaultMode();
                    Mod.instance.CastMessage("The Effigy has moved to the farm cave", -1);
                    break;
                case "Farm":
                    str = "I will take my place amongst the posts and furrows of my old master's home.";
                    Mod.instance.CharacterRegister(npc.Name, "Farm");
                    npc.WarpToDefault();
                    (npc as StardewDruid.Character.Effigy).SwitchRoamMode();
                    Mod.instance.CastMessage("The Effigy now roams the farm", -1);
                    break;
                case "Follow":
                    str = "I will see how you put the lessons of the First Farmer to use.";
                    (npc as StardewDruid.Character.Effigy).SwitchFollowMode();
                    Mod.instance.CastMessage("The Effigy joins you on your adventures", -1);
                    break;
            }
            Game1.drawDialogue(npc, str);
        }

        public void ReplyStandby()
        {
            string str = "Vigilance is a speciality of mine. (The Effigy does its best to stay as dumb and still as a scarecrow)";
            npc.ActivateStandby();
            Game1.drawDialogue(npc, str);
        }

        public void ReplyContinue()
        {
            string str = "Thank you for not stuffing me in a ceiling cavity.";
            npc.DeactivateStandby();
            Game1.drawDialogue(npc, str);
        }

    }
}
