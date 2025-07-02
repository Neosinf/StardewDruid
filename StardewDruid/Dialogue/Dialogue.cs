using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Constants;
using StardewValley.Menus;
using StardewValley.Minigames;
using StardewValley.Monsters;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace StardewDruid.Dialogue
{
    public class Dialogue
    {

        public CharacterHandle.characters characterType = CharacterHandle.characters.none;

        public StardewValley.NPC npc = null;

        public Dictionary<string, Data.Quest.questTypes> promptDialogue = new();

        public List<string> introDialogue = new();

        public Dictionary<string, DialogueSpecial> specialDialogue = new();

        public string currentSpecial;

        public subjects currentSubject;

        public enum subjects
        {
            quests,
            talk,
            lore,
            relics,
            inventory,
            adventure,
            warp,
            attune,
            offering,
        }

        public int currentIndex;

        public Dialogue(CharacterHandle.characters CharacterType)
        {

            characterType = CharacterType;

            if (Mod.instance.characters.ContainsKey(CharacterType))
            {

                npc = Mod.instance.characters[CharacterType];

            }
               
        }

        public Dialogue(CharacterHandle.characters CharacterType, NPC NPC)
        {

            characterType = CharacterType;

            npc = NPC;

        }

        public virtual void DialogueApproach()
        {

            if(promptDialogue.Count > 0)
            {

                KeyValuePair<string,Data.Quest.questTypes> prompt = promptDialogue.Last();

                promptDialogue.Remove(prompt.Key);

                if (specialDialogue.ContainsKey(prompt.Key))
                {

                    RunSpecialDialogue(prompt.Key);

                }

                return;

            }

            string str = DialogueIntroduction.DialogueApproach(characterType);

            if(introDialogue.Count > 0)
            {

                str = introDialogue.First(); 
                
                introDialogue.Clear();

            }

            if(str == null)
            {

                return;

            }

            List<Response> responseList = new List<Response>();

            List<subjects> trySubjects = new()
            {
                subjects.quests,
                subjects.talk,
                subjects.lore,
                subjects.relics,
                subjects.inventory,
                subjects.adventure,
                subjects.attune,
                subjects.offering,
            };

            foreach(subjects subject in trySubjects)
            {
                string option = DialogueOption(characterType, subject);

                if (option != null)
                {

                    responseList.Add(new(subject.ToString(), option));


                }

            }

            Response nevermind = new ("none", DialogueIntroduction.DialogueNevermind(characterType));

            nevermind.SetHotKey(Microsoft.Xna.Framework.Input.Keys.Escape);

            responseList.Add(nevermind);

            GameLocation.afterQuestionBehavior questionBehavior = new(AnswerApproach);

            Game1.player.currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, npc);

        }

        public static string DialogueOption(CharacterHandle.characters character, subjects subject)
        {


            switch (subject)
            {

                case subjects.quests:

                    if (!Context.IsMainPlayer)
                    {
                        return null;

                    }

                    if (Mod.instance.questHandle.IsQuestGiver(character))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.159");

                    }

                    return null;

                case subjects.talk:


                    switch (character)
                    {

                        case CharacterHandle.characters.recruit_one:
                        case CharacterHandle.characters.recruit_two:
                        case CharacterHandle.characters.recruit_three:
                        case CharacterHandle.characters.recruit_four:

                            return DialogueTalk.DialogueOption(character);

                    }

                    return null;

                case subjects.lore:

                    return DialogueLore.DialogueOption(character);

                case subjects.inventory:

                    return DialogueInventory.DialogueOption(character);

                case subjects.relics:

                    return DialogueRelics.DialogueOption(character);

                case subjects.adventure:

                    return DialogueAdventure.DialogueOption(character);

                case subjects.attune:

                    return DialogueAttune.DialogueOption(character);

                case subjects.offering:

                    return DialogueOffering.DialogueOption(character);

            }

            return null;

        }

        public static DialogueSpecial DialogueGenerator(CharacterHandle.characters character, subjects subject, int index = 0, int answer = 0)
        {

            DialogueSpecial generate = new();

            switch (subject)
            {

                case subjects.quests:

                    Mod.instance.questHandle.DialogueReload(character);

                    return null;

                case subjects.talk:

                    DialogueTalk.DialogueGenerate(character);

                    return null;

                case subjects.lore:

                    return DialogueLore.DialogueGenerate(character, index, answer);

                case subjects.inventory:

                    return DialogueInventory.DialogueGenerate(character, index, answer);

                case subjects.relics:

                    return DialogueRelics.DialogueGenerate(character, index, answer);

                case subjects.adventure:

                    return DialogueAdventure.DialogueGenerate(character, index, answer);

                case subjects.attune:

                    return DialogueAttune.DialogueGenerate(character, index, answer);

                case subjects.offering:

                    return DialogueOffering.DialogueGenerate(character, index, answer);

            }

            return generate;

        }

        public virtual void AnswerApproach(Farmer visitor, string answer)
        {

            List<subjects> trySubjects = new()
            {
                subjects.quests,
                subjects.lore,
                subjects.relics,
                subjects.inventory,
                subjects.adventure,
                subjects.attune,
                subjects.offering,
            };

            foreach (subjects subject in trySubjects)
            {
                        
                if (answer == subject.ToString())
                {

                    currentSubject = subject;

                    currentIndex = 0;

                    currentSpecial = subject.ToString();

                    DialogueSpecial generateSpecial = DialogueGenerator(characterType, subject);

                    if (generateSpecial == null)
                    {

                        if(promptDialogue.Count > 0)
                        {

                            DelayedAction.functionAfterDelay(DialogueApproach, 100);

                        }

                        return;

                    }

                    AddSpecialDialogue(subject.ToString(), generateSpecial);

                    RunSpecialDialogue(subject.ToString());

                }

            }

        }

        public virtual void RunSpecialDialogue(string prompt)
        {

            DialogueSpecial specialEntry = specialDialogue[prompt];

            if(specialEntry.responses.Count > 0)
            {

                currentSpecial = prompt;

                DelayedAction.functionAfterDelay(RunSpecialQuestion, 100);

                return;

            }

            if (specialEntry.questId != null)
            {

                Mod.instance.questHandle.DialogueCheck(specialEntry.questId, specialEntry.questContext, characterType);

            }

            RunSpecialAnswer(specialEntry.intro);

        }

        public virtual void RunSpecialQuestion()
        {

            DialogueSpecial specialEntry = specialDialogue[currentSpecial];

            GameLocation.afterQuestionBehavior questionBehavior = new(RespondSpecialDialogue);

            List<Response> responseList = new();

            for (int r = 0; r < specialEntry.responses.Count; r++)
            {

                responseList.Add(new(r.ToString(), specialEntry.responses[r]));

            }

            responseList.Add(new Response("999", DialogueIntroduction.DialogueNevermind(characterType)).SetHotKey(Microsoft.Xna.Framework.Input.Keys.Escape));

            Game1.player.currentLocation.createQuestionDialogue(specialEntry.intro, responseList.ToArray(), questionBehavior, npc);

        }

        public virtual void RunSpecialAnswer(string answer)
        {

            Game1.currentSpeaker = npc;

            if (npc == null)
            {

                List<string> answers = ModUtility.SplitStringByLength(answer, 185);

                Game1.activeClickableMenu = new DialogueBox(answers);

            }
            else
            {

                StardewValley.Dialogue dialogue = new(npc, "0", answer);

                Game1.activeClickableMenu = new DialogueBox(dialogue);

            }

            Game1.player.Halt();

            Game1.player.CanMove = false;

        }

        public virtual void RespondSpecialDialogue(Farmer visitor, string dialogueId)
        {

            string specialId = currentSpecial;

            DialogueSpecial special = specialDialogue[specialId];

            int id = Convert.ToInt32(dialogueId);

            if (id == 999)
            {

                return;

            }

            if (special.lead)
            {

                currentIndex++;

                int answer = Convert.ToInt32(special.answers[id]);

                DialogueSpecial nextEntry = DialogueGenerator(characterType, currentSubject, currentIndex, answer);

                if (nextEntry == null)
                {

                    return;

                }
                else if(nextEntry.intro == null)
                {

                    return;

                }

                AddSpecialDialogue(currentSpecial, nextEntry);

                if (nextEntry.responses.Count > 0)
                {

                    DelayedAction.functionAfterDelay(RunSpecialQuestion, 100);

                    return;

                }

                RunSpecialAnswer(nextEntry.intro);

                return;

            }

            if (special.questId != null)
            {

                Mod.instance.questHandle.DialogueCheck(special.questId, special.questContext, characterType, id);

            }

            if (special.answers.Count > 0)
            {

                string answer;

                if (special.answers.Count <= id)
                {

                    answer = special.answers.First();

                }
                else
                {

                    answer = special.answers[id];

                }

                RunSpecialAnswer(answer);

                return;

            }

            DelayedAction.functionAfterDelay(DialogueApproach,100);

        }


        public virtual void AddSpecialDialogue(string eventId, DialogueSpecial special)
        {

            if(special == null)
            {

                return;

            }

            Data.Quest.questTypes dial = Data.Quest.questTypes.approach;

            specialDialogue[eventId] = special;

            if(special.questId != null)
            {
                
                if(special.questContext < 2)
                {

                    switch (Mod.instance.questHandle.quests[eventId].type)
                    {

                        case Data.Quest.questTypes.challenge:

                            dial = Data.Quest.questTypes.challenge;

                            break;

                        case Data.Quest.questTypes.lesson:

                            dial = Data.Quest.questTypes.lesson;

                            break;

                        default:

                            dial = Data.Quest.questTypes.miscellaneous;

                            break;
                    
                    }

                }

            }

            if (special.prompt)
            {

                promptDialogue[eventId] = dial;

            }

        }

        public virtual void RemoveSpecialDialogue(string eventId)
        {

            if (promptDialogue.ContainsKey(eventId))
            {

                promptDialogue.Remove(eventId);

            }

            if (specialDialogue.ContainsKey(eventId))
            {

                specialDialogue.Remove(eventId);

            }

        }

    }

    public class DialogueSpecial
    {

        public string intro;

        public int companion;

        public List<string> responses = new();

        public List<string> answers = new();

        public bool prompt;

        public string questId;

        public int questContext;

        public bool lead;

    }

}
