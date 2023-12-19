// Decompiled with JetBrains decompiler
// Type: StardewDruid.Dialogue.Quests
// Assembly: StardewDruid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24DA4344-683E-4959-87A6-C0A858BCC7DA
// Assembly location: C:\Users\piers\source\repos\StardewDruid\StardewDruid\bin\Debug\net5.0\StardewDruid.dll

using StardewDruid.Map;
using StardewValley;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace StardewDruid.Dialogue
{
  public class Quests
  {
    public NPC npc;

    public Quests(NPC NPC) => this.npc = NPC;

    public void Approach()
    {
      string str1 = QuestData.StageProgress().Last<string>();
      if (this.npc is StardewDruid.Character.Effigy)
      {
        switch (str1)
        {
          case "none":
          case "weald":
          case "mists":
          case "stars":
            // ISSUE: method pointer
            DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(ProgressQuests)), 100);
            break;
          case "hidden":
            // ISSUE: method pointer
            DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(HiddenQuests)), 100);
            break;
          case "Jester":
            // ISSUE: method pointer
            DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(JesterQuest)), 100);
            break;
          default:
            // ISSUE: method pointer
            DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(CycleQuests)), 100);
            break;
        }
      }
      if (!(this.npc is StardewDruid.Character.Jester))
        return;
      string str2 = str1;
      if (str2 == "fates" || str2 == "ether")
      {
        // ISSUE: method pointer
        DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(ProgressQuests)), 100);
      }
      else
      {
        // ISSUE: method pointer
        DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(CycleQuests)), 100);
      }
    }

    public void ReturnTomorrow()
    {
      if (this.npc is StardewDruid.Character.Effigy)
        Game1.drawDialogue(this.npc, "Hmm... return tomorrow after I have consulted the Others.");
      else
        Game1.drawDialogue(this.npc, "Prrr... (Jester is deep in thought about tomorrow's possibilities)");
    }

    public void ProgressQuests()
    {
      string quest = QuestData.NextProgress();
      if (Mod.instance.QuestComplete(quest))
      {
        this.ReturnTomorrow();
      }
      else
      {
        string str = Mod.instance.QuestDiscuss(quest);
        Mod.instance.CastMessage("Druid journal has been updated");
        Game1.drawDialogue(this.npc, str);
      }
    }

    public void HiddenQuests()
    {
      QuestData.NextProgress();
      Game1.drawDialogue(this.npc, "Those with a twisted connection to the otherworld may remain tethered to the Valley long after their mortal vessel wastes away. Strike them with bolt and flame to draw out and disperse their corrupted energies. (Check your quest log for new challenges)");
    }

    public void JesterQuest()
    {
      if (QuestData.NextProgress() == "approachJester")
      {
        string str = "Fortune gazes upon you... but it can't be her. One of her kin perhaps.";
        List<Response> responseList = new List<Response>()
        {
          new Response("Jester", "What do you mean?")
        };
        // ISSUE: method pointer
        GameLocation.afterQuestionBehavior questionBehavior = new GameLocation.afterQuestionBehavior((object) this, __methodptr(AnswerJester));
        ((StardewValley.Character) Game1.player).currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, this.npc);
      }
      else
        this.CycleQuests();
    }

    public void CycleQuests()
    {
      string str = "An old threat has re-emerged. Be careful, they may have increased in power since your last confrontation.";
      List<Response> responseList = new List<Response>();
      if (this.npc is StardewDruid.Character.Jester)
        str = "Come on, time for something fun! I like popping slimes.";
      responseList.Add(new Response("accept", "(accept) I am ready to face the renewed threat"));
      if (this.npc is StardewDruid.Character.Effigy && Mod.instance.QuestComplete("challengeSandDragon") && !Mod.instance.QuestGiven("challengeSandDragonTwo"))
        responseList.Add(new Response("dragon", "(dragon fight) I want a rematch with that ghost dragon!"));
      if (this.npc is StardewDruid.Character.Jester && !Mod.instance.QuestGiven("challengeStarsTwo"))
        responseList.Add(new Response("slimes", "(slimes fight) Lets put ol' pumpkin head in his place."));
      responseList.Add(new Response("abort", "I have been unable to proceed against any of these threats. Can we start over? (abort quests)"));
      responseList.Add(new Response("cancel", "(not now) I'll need some time to prepare"));
      // ISSUE: method pointer
      GameLocation.afterQuestionBehavior questionBehavior = new GameLocation.afterQuestionBehavior((object) this, __methodptr(AnswerThreats));
      ((StardewValley.Character) Game1.player).currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, this.npc);
    }

    public void AnswerThreats(Farmer effigyVisitor, string effigyAnswer)
    {
      string str1 = "The valley will withstand the threat as it can, as it always has.";
      string str2 = "daily";
      if (this.npc is StardewDruid.Character.Jester)
      {
        str1 = "We will leave the Valley to it's fate. Actually I'm not sure if there's a specific Fate who looks after this place.";
        str2 = "dailytwo";
      }
      switch (effigyAnswer)
      {
        case "accept":
          Dictionary<string, string> dictionary = QuestData.SecondQuests();
          List<string> stringList1 = new List<string>();
          List<string> stringList2 = new List<string>();
          str1 = "May you be successful against the shadows of the otherworld.";
          Mod.instance.CastMessage("Druid journal has been updated");
          foreach (KeyValuePair<string, string> keyValuePair in dictionary)
          {
            if (!Mod.instance.QuestGiven(keyValuePair.Key))
            {
              Mod.instance.NewQuest(keyValuePair.Key);
              Mod.instance.lessons.Contains(str2);
              break;
            }
            string quest = keyValuePair.Key + "Two";
            if (!Mod.instance.QuestGiven(quest))
              stringList1.Add(quest);
            stringList2.Add(quest);
          }
          if (stringList1.Count == 0)
            stringList1 = stringList2;
          string quest1 = stringList1[Game1.random.Next(stringList1.Count)];
          Mod.instance.NewQuest(quest1);
          Mod.instance.lessons.Contains(str2);
          break;
        case "dragon":
          str1 = "May the Tyrant be buried under the weight of sand and might.";
          Mod.instance.NewQuest("challengeSandDragonTwo");
          Mod.instance.lessons.Contains(str2);
          Mod.instance.CastMessage("Druid journal has been updated");
          break;
        case "slimes":
          str1 = "Yeet. Time for me to ready my beam face!";
          Mod.instance.NewQuest("challengeStarsTwo");
          Mod.instance.lessons.Contains(str2);
          Mod.instance.CastMessage("Druid journal has been updated");
          break;
        case "abort":
          using (List<string>.Enumerator enumerator = QuestData.ActiveSeconds().GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              string current = enumerator.Current;
              Mod.instance.RemoveQuest(current);
            }
            break;
          }
      }
      Game1.drawDialogue(this.npc, str1);
    }

    public void AnswerJester(Farmer visitor, string answer)
    {
      Game1.drawDialogue(this.npc, "I felt the industry of the forest spirits the night they toiled on the span across the mountain ravine. They restored not only a bridge over land but between two destinies. Should you decide to cross, a fateful encounter awaits you. (Should be worth checking out the bridge to the Quarry)");
    }
  }
}
