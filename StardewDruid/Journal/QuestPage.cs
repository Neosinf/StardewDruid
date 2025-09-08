using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Journal
{
    public class QuestPage : DruidJournal
    {

        public string questId;

        public QuestPage(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {

            parent = journalTypes.quests;

        }

        public override void populateContent()
        {

            questId = parameters[0];

            // ---------------------------- quest

            bool isActive = Mod.instance.save.progress[questId].status == 1 || Mod.instance.save.progress[questId].status == 4;

            bool isComplete = Mod.instance.questHandle.IsComplete(questId);

            bool isReplayed = Mod.instance.save.progress[questId].status == 4;

            Data.Quest questRecord = Mod.instance.questHandle.quests[questId];

            int questReward = questRecord.reward;


            // ----------------------------- title

            int textHeight = 48;

            //contentComponents[101] = new(ContentComponent.contentTypes.title, questRecord.title);

            //contentComponents[101].setBounds(0, xPositionOnScreen, yPositionOnScreen + textHeight, width, 64);

            //textHeight += contentComponents[101].bounds.Height;

            title = questRecord.title;

            int start = 0;

            // ----------------------------- description

            contentComponents[start] = new(ContentComponent.contentTypes.text, "description");

            contentComponents[start].text[0] = questRecord.description;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width-128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            // ------------------------------ conditional description

            if (isActive && !Context.IsMainPlayer && questRecord.type != Data.Quest.questTypes.lesson)
            {

                contentComponents[start] = new(ContentComponent.contentTypes.text, "hostonly");

                contentComponents[start].text[0] = StringData.Get(StardewDruid.Data.StringData.str.hostOnly);

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkRed;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }
            
            if (isReplayed)
            {
                
                contentComponents[start] = new(ContentComponent.contentTypes.text, "questReplay");

                contentComponents[start].text[0] = StringData.Get(StardewDruid.Data.StringData.str.questReplay);

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkRed;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            // ------------------------------ instructions

            if(isComplete && !isReplayed && questRecord.explanation != null)
            {

                contentComponents[start] = new(ContentComponent.contentTypes.text, "explanation");

                contentComponents[start].text[0] = questRecord.explanation;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }
            else if (questRecord.instruction != null)
            {

                contentComponents[start] = new(ContentComponent.contentTypes.text, "instruction");

                contentComponents[start].text[0] = questRecord.instruction;

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkBlue;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            if (questRecord.notes.Count > 0)
            {

                foreach(string note in questRecord.notes)
                {

                    contentComponents[start] = new(ContentComponent.contentTypes.text, "note");

                    contentComponents[start].text[0] = note;

                    contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

                    contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                    textHeight += contentComponents[start++].bounds.Height;

                }

            }

            // ------------------------------ conditional instructions

            if (questRecord.type == Data.Quest.questTypes.lesson)
            {

                string completeString = "";

                string lessonProgress =
                            completeString +
                            Mod.instance.save.progress[questId].progress.ToString() + " " +
                            StringData.Get(StringData.str.outOf) + " " +
                            questRecord.requirement.ToString() + " " +
                            questRecord.progression;

                if (isComplete)
                {
                    
                    completeString = StringData.Get(StringData.str.mastered);

                    if(Mod.instance.save.progress[questId].progress < questRecord.requirement)
                    {

                        lessonProgress = StringData.Get(StringData.str.lessonSkipped);

                    }

                }

                contentComponents[start] = new(ContentComponent.contentTypes.text, "progress");

                contentComponents[start].text[0] = lessonProgress;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            if (questReward > 0)
            {

                if(isReplayed) { questReward *= 3; questReward /= 2; }

                float adjustReward = 1.2f - ((float)Mod.instance.ModDifficulty() * 0.1f);

                questReward = (int)((float)questReward * adjustReward);

                contentComponents[start] = new(ContentComponent.contentTypes.text, "lesson");

                contentComponents[start].text[0] = StringData.Get(StardewDruid.Data.StringData.str.reward) + ": " + questReward.ToString() + "g";

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            if (isActive && isReplayed && questRecord.replay != null)
            {

                contentComponents[start] = new(ContentComponent.contentTypes.text, "replay");

                contentComponents[start].text[0] = StringData.Get(StardewDruid.Data.StringData.str.replayReward) + ": " + questRecord.replay;

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkGreen;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            textHeight += 16;

            contentBox = new(xPositionOnScreen, yPositionOnScreen + 64, width, textHeight);

        }

        public override void populateInterface()
        {

            interfaceComponents = new()
            {


                [201] = addButton(journalButtons.previous),

                [202] = addButton(journalButtons.viewEffect),

                [203] = addButton(journalButtons.replayQuest),


                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };


            if (Mod.instance.questHandle.quests[questId].effect == EffectsData.EffectPage.none)
            {

                interfaceComponents.Remove(202);

            }

            if (!Context.IsMainPlayer)
            {

                interfaceComponents.Remove(203);

            }
            else
            if (Mod.instance.save.progress[questId].status <= 1)
            {

                interfaceComponents[203] = addButton(journalButtons.skipQuest);

            }
            else if (Mod.instance.questHandle.IsReplayable(questId))
            {

                switch (Mod.instance.save.progress[questId].status)
                {

                    case 2:

                        interfaceComponents[203] = addButton(journalButtons.replayQuest);

                        break;

                    case 3:

                        interfaceComponents[203] = addButton(journalButtons.replayTomorrow);

                        break;

                    case 4:

                        interfaceComponents[203] = addButton(journalButtons.cancelReplay);

                        break;

                }

            }
            else
            {

                interfaceComponents.Remove(203);

            }

        }


        public override void pressButton(journalButtons button)
        {
            
            switch (button)
            {

                case journalButtons.viewEffect:

                    string findEffect = Mod.instance.questHandle.questEffects(questId);

                    openJournal(journalTypes.effectPage,findEffect);

                    break;

                case journalButtons.skipQuest:

                    Game1.playSound("ghost");

                    Mod.instance.questHandle.CompleteQuest(questId);

                    Mod.instance.questHandle.OnCancel(questId);

                    openJournal(journalTypes.questPage, questId);

                    break;

                case journalButtons.replayQuest:

                    if (Mod.instance.questHandle.IsReplayable(questId))
                    {

                        Game1.playSound("yoba");

                        Mod.instance.questHandle.RevisitQuest(questId);

                        openJournal(journalTypes.questPage, questId);

                    }

                    break;

                case journalButtons.replayTomorrow:

                    break;

                case journalButtons.cancelReplay:

                    Game1.playSound("ghost");

                    Mod.instance.questHandle.OnCancel(questId);

                    Mod.instance.SyncProgress();

                    openJournal(journalTypes.questPage, questId);

                    break;

                default:

                    base.pressButton(button);

                    break;

            }

        }

    }

}
