﻿using StardewDruid.Character;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Constants;
using StardewValley.Menus;
using StardewValley.Minigames;
using StardewValley.Monsters;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using xTile.Dimensions;
using static StardewValley.Menus.SocialPage;
using static System.Net.Mime.MediaTypeNames;

namespace StardewDruid.Dialogue
{
    public class Dialogue
    {

        public CharacterHandle.characters characterType = CharacterHandle.characters.none;

        public StardewDruid.Character.Character npc = null;

        public Dictionary<string, Journal.Quest.questTypes> promptDialogue = new();

        public List<string> introDialogue = new();

        public Dictionary<string, DialogueSpecial> specialDialogue = new();

        public string currentSpecial;

        public CharacterHandle.subjects currentSubject;

        public int currentIndex;

        public Dialogue(CharacterHandle.characters CharacterType)
        {

            characterType = CharacterType;

            if (Mod.instance.characters.ContainsKey(CharacterType))
            {

                npc = Mod.instance.characters[CharacterType];

            }
               
        }

        public virtual void DialogueApproach()
        {

            if(promptDialogue.Count > 0)
            {

                KeyValuePair<string,Journal.Quest.questTypes> prompt = promptDialogue.Last();

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

            List<Response> responseList = new List<Response>();

            List<CharacterHandle.subjects> subjects = new()
            {
                CharacterHandle.subjects.quests,
                CharacterHandle.subjects.lore,
                CharacterHandle.subjects.relics,
                CharacterHandle.subjects.inventory,
                CharacterHandle.subjects.adventure,
                CharacterHandle.subjects.attune,
                CharacterHandle.subjects.offering,
            };

            foreach(CharacterHandle.subjects subject in subjects)
            {
                string option = CharacterHandle.DialogueOption(characterType, subject);

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

        public virtual void AnswerApproach(Farmer visitor, string answer)
        {

            List<CharacterHandle.subjects> subjects = new()
            {
                CharacterHandle.subjects.quests,
                CharacterHandle.subjects.lore,
                CharacterHandle.subjects.relics,
                CharacterHandle.subjects.inventory,
                CharacterHandle.subjects.adventure,
                CharacterHandle.subjects.attune,
                CharacterHandle.subjects.offering,
            };

            foreach (CharacterHandle.subjects subject in subjects)
            {
                        
                if (answer == subject.ToString())
                {

                    currentSubject = subject;

                    currentIndex = 0;

                    currentSpecial = subject.ToString();

                    DialogueSpecial generateSpecial = CharacterHandle.DialogueGenerator(characterType, subject);

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

            StardewValley.Dialogue dialogue = new(npc, "0", answer);

            Game1.activeClickableMenu = new DialogueBox(dialogue);

            Game1.player.Halt();

            Game1.player.CanMove = false;

            if (Context.IsMultiplayer && Context.IsMainPlayer)
            {

                string queryName = npc == null ? "none" : npc.Name;

                QueryData queryData = new()
                {

                    name = queryName,

                    value = answer,

                    location = Game1.player.currentLocation.Name,

                };

                Mod.instance.EventQuery(queryData, QueryData.queries.EventDialogue);

            }

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

                DialogueSpecial nextEntry = CharacterHandle.DialogueGenerator(characterType, currentSubject, currentIndex, answer);

                if (nextEntry.intro == null)
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

            Journal.Quest.questTypes dial = Journal.Quest.questTypes.approach;

            specialDialogue[eventId] = special;

            if(special.questId != null)
            {
                
                if(special.questContext < 2)
                {

                    switch (Mod.instance.questHandle.quests[eventId].type)
                    {
                        case Journal.Quest.questTypes.challenge:

                            dial = Journal.Quest.questTypes.challenge;

                            break;

                        case Journal.Quest.questTypes.lesson:

                            dial = Journal.Quest.questTypes.lesson;

                            break;

                        default:

                            dial = Journal.Quest.questTypes.miscellaneous;

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
