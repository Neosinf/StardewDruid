// Decompiled with JetBrains decompiler
// Type: StardewDruid.Dialogue.Dialogue
// Assembly: StardewDruid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24DA4344-683E-4959-87A6-C0A858BCC7DA
// Assembly location: C:\Users\piers\source\repos\StardewDruid\StardewDruid\bin\Debug\net5.0\StardewDruid.dll

using StardewValley;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace StardewDruid.Dialogue
{
  public class Dialogue
  {
    public StardewDruid.Character.Character npc;
    public string returnFrom;
    public Dictionary<string, List<string>> specialDialogue = new Dictionary<string, List<string>>();

    public virtual void DialogueApproach()
    {
      if (this.specialDialogue.Count > 0)
      {
        this.DialogueSpecial();
      }
      else
      {
        string str = "Welcome";
        List<Response> responseList = new List<Response>();
        responseList.Add(new Response("none", "(say nothing)"));
        StardewDruid.Dialogue.Dialogue dialogue = this;
        // ISSUE: virtual method pointer
        GameLocation.afterQuestionBehavior questionBehavior = new GameLocation.afterQuestionBehavior((object) dialogue, __vmethodptr(dialogue, AnswerApproach));
        this.returnFrom = (string) null;
        ((StardewValley.Character) Game1.player).currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, (NPC) this.npc);
      }
    }

    public virtual void DialogueSpecial()
    {
      KeyValuePair<string, List<string>> keyValuePair = this.specialDialogue.First<KeyValuePair<string, List<string>>>();
      string str = keyValuePair.Value[0];
      List<Response> responseList = new List<Response>();
      for (int index = 1; index < keyValuePair.Value.Count; ++index)
        responseList.Add(new Response("special", keyValuePair.Value[index]));
      responseList.Add(new Response("none", "(say nothing)"));
      StardewDruid.Dialogue.Dialogue dialogue = this;
      // ISSUE: virtual method pointer
      GameLocation.afterQuestionBehavior questionBehavior = new GameLocation.afterQuestionBehavior((object) dialogue, __vmethodptr(dialogue, AnswerSpecial));
      ((StardewValley.Character) Game1.player).currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, (NPC) this.npc);
    }

    public virtual void AnswerSpecial(Farmer visitor, string answer)
    {
      KeyValuePair<string, List<string>> keyValuePair = this.specialDialogue.First<KeyValuePair<string, List<string>>>();
      if (!(answer == "special"))
        return;
      this.AnswerApproach(visitor, keyValuePair.Key);
      this.specialDialogue.Remove(keyValuePair.Key);
    }

    public virtual void AnswerApproach(Farmer visitor, string answer)
    {
    }
  }
}
