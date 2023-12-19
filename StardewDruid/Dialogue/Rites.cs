// Decompiled with JetBrains decompiler
// Type: StardewDruid.Dialogue.Rites
// Assembly: StardewDruid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24DA4344-683E-4959-87A6-C0A858BCC7DA
// Assembly location: C:\Users\piers\source\repos\StardewDruid\StardewDruid\bin\Debug\net5.0\StardewDruid.dll

using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;

#nullable disable
namespace StardewDruid.Dialogue
{
  public class Rites
  {
    public NPC npc;

    public Rites(NPC NPC) => this.npc = NPC;

    public void Approach()
    {
      if (this.npc is StardewDruid.Character.Effigy)
      {
        // ISSUE: method pointer
        DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(RitesEffigy)), 100);
      }
      // ISSUE: method pointer
      DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(RitesJester)), 100);
    }

    public void RitesEffigy()
    {
      string str = "Forgotten Effigy: ^The traditions live on.";
      List<Response> responseList = new List<Response>();
      QuestData.RitesProgress();
      Mod.instance.CurrentBlessing();
      responseList.Add(new Response("blessing", "I want to practice a different rite (change rite)"));
      if (Context.IsMultiplayer && Context.IsMainPlayer)
        responseList.Add(new Response("farmhands", "I want to share what I've learned with others (train farmhands)"));
      if (Mod.instance.AttuneableWeapon() != -1)
        responseList.Add(new Response("attune", "I want to dedicate this " + ((Item) Game1.player.CurrentTool).Name + " (manage attunement)"));
      responseList.Add(new Response("none", "(nevermind)"));
      // ISSUE: method pointer
      GameLocation.afterQuestionBehavior questionBehavior = new GameLocation.afterQuestionBehavior((object) this, __methodptr(RitesAnswer));
      ((StardewValley.Character) Game1.player).currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, this.npc);
    }

    public void RitesJester()
    {
      string str = "The Jester of Fate: ^You assume I know anything.";
      List<Response> responseList = new List<Response>();
      QuestData.RitesProgress();
      Mod.instance.CurrentBlessing();
      responseList.Add(new Response("blessing", "I want to practice a different rite (change rite)"));
      int num = Mod.instance.AttuneableWeapon();
      if (num != -1 && num != 999)
        responseList.Add(new Response("attune", "I want to dedicate this " + ((Item) Game1.player.CurrentTool).Name + " (manage attunement)"));
      responseList.Add(new Response("none", "(nevermind)"));
      // ISSUE: method pointer
      GameLocation.afterQuestionBehavior questionBehavior = new GameLocation.afterQuestionBehavior((object) this, __methodptr(RitesAnswer));
      ((StardewValley.Character) Game1.player).currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, this.npc);
    }

    public void RitesAnswer(Farmer visitor, string answer)
    {
      switch (answer)
      {
        case "blessing":
          // ISSUE: method pointer
          DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(DialogueBlessing)), 100);
          break;
        case "farmhands":
          // ISSUE: method pointer
          DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(DialogueFarmhands)), 100);
          break;
        case "attune":
          // ISSUE: method pointer
          DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(DialogueAttune)), 100);
          break;
      }
    }

    public void DialogueBlessing()
    {
      string str1 = "Forgotten Effigy: ^The Kings, the Lady, the Stars, I may entreat them all.";
      List<string> stringList = QuestData.RitesProgress();
      string str2 = Mod.instance.CurrentBlessing();
      List<Response> responseList = new List<Response>();
      if (this.npc is StardewDruid.Character.Effigy)
      {
        if (str2 != "weald")
          responseList.Add(new Response("weald", "Let us pay homage to the Two Kings"));
        if (stringList.Contains("mists") && str2 != "mists")
          responseList.Add(new Response("mists", "Call out to the Lady Beyond The Shore"));
        if (stringList.Contains("stars") && str2 != "stars")
          responseList.Add(new Response("stars", "Look to the Stars for me"));
      }
      if (this.npc is StardewDruid.Character.Jester)
      {
        str1 = "Jester: ^I hear many voices, and some aren't even mine";
        if (str2 != "fates")
          responseList.Add(new Response("fates", "Ask your kin to favour me"));
        if (stringList.Contains("ether") && str2 != "ether")
          responseList.Add(new Response("ether", "I want to reach the Masters of the Ether, the Dragons"));
      }
      responseList.Add(new Response("none", "I don't want anyone's favour (disable)"));
      responseList.Add(new Response("cancel", "(say nothing)"));
      // ISSUE: method pointer
      GameLocation.afterQuestionBehavior questionBehavior = new GameLocation.afterQuestionBehavior((object) this, __methodptr(AnswerBlessing));
      ((StardewValley.Character) Game1.player).currentLocation.createQuestionDialogue(str1, responseList.ToArray(), questionBehavior, this.npc);
    }

    public void AnswerBlessing(Farmer effigyVisitor, string effigyAnswer)
    {
      string str;
      switch (effigyAnswer)
      {
        case "weald":
          Game1.addHUDMessage(new HUDMessage(Mod.instance.CastControl() + " to perform Rite of the Weald", ""));
          str = "The Kings of Oak and Holly come again.";
          break;
        case "mists":
          Game1.addHUDMessage(new HUDMessage(Mod.instance.CastControl() + " to perform Rite of the Mists", ""));
          str = "The Voice Beyond the Shore echoes around us.";
          break;
        case "stars":
          Game1.addHUDMessage(new HUDMessage(Mod.instance.CastControl() + " to perform Rite of the Stars", ""));
          str = "Life to ashes. Ashes to dust.";
          break;
        case "fates":
          Game1.addHUDMessage(new HUDMessage(Mod.instance.CastControl() + " to perform Rite of the Fates", ""));
          str = "The Fates peer through the veil. (Jester tries to be as expressionless as the Effigy)";
          break;
        case "ether":
          Game1.addHUDMessage(new HUDMessage(Mod.instance.CastControl() + " to perform Rite of the Ether", ""));
          str = "You're the master now, Farmer. The ancient ones have retreated from this world.";
          break;
        case "none":
          Game1.addHUDMessage(new HUDMessage(Mod.instance.CastControl() + " will do nothing", ""));
          str = !(this.npc is StardewDruid.Character.Jester) ? "The light fades away." : "Well I don't blame you. Much.";
          break;
        default:
          str = "(says nothing back).";
          break;
      }
      if (effigyAnswer != "cancel")
        Mod.instance.ChangeBlessing(effigyAnswer);
      Game1.drawDialogue(this.npc, str);
    }

    public void DialogueEffects()
    {
      List<string> stringList = QuestData.RitesProgress();
      Mod.instance.CurrentBlessing();
      List<Response> responseList = new List<Response>();
      string str;
      if (this.npc is StardewDruid.Character.Effigy)
      {
        str = "Forgotten Effigy: ^Our traditions are etched into the bedrock of the valley.";
        responseList.Add(new Response("weald", "What role do the Two Kings play?"));
        if (stringList.Contains("mists"))
          responseList.Add(new Response("mists", "Who is the Voice Beyond the Shore?"));
        if (stringList.Contains("stars"))
          responseList.Add(new Response("stars", "Do the Stars have names?"));
        if (stringList.Contains("fates"))
          responseList.Add(new Response("fates", "What do you know of the Fates?"));
        if (stringList.Contains("ether"))
          responseList.Add(new Response("ether", "Who were the Masters of the Ether?"));
      }
      else
      {
        str = "The Jester of Fate: ^I enjoy answering questions. One of my dearest sisters was a sphinx.";
        responseList.Add(new Response("Jester", "Have you learned anything more about your purpose?"));
        responseList.Add(new Response("fates", "What do you know of the Fates?"));
        if (stringList.Contains("ether"))
          responseList.Add(new Response("ether", "Where are the Dragons?"));
      }
      responseList.Add(new Response("return", "(nevermind)"));
      // ISSUE: method pointer
      GameLocation.afterQuestionBehavior questionBehavior = new GameLocation.afterQuestionBehavior((object) this, __methodptr(AnswerEffects));
      ((StardewValley.Character) Game1.player).currentLocation.createQuestionDialogue(str, responseList.ToArray(), questionBehavior, this.npc);
    }

    public void AnswerEffects(Farmer effigyVisitor, string effigyAnswer)
    {
      string str = effigyAnswer;
      if (str == null)
        return;
      switch (str.Length)
      {
        case 5:
          switch (str[0])
          {
            case 'e':
              if (!(str == "ether"))
                return;
              // ISSUE: method pointer
              DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(EffectsEther)), 100);
              return;
            case 'f':
              if (!(str == "fates"))
                return;
              // ISSUE: method pointer
              DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(EffectsFates)), 100);
              return;
            case 'm':
              if (!(str == "mists"))
                return;
              // ISSUE: method pointer
              DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(EffectsMists)), 100);
              return;
            case 's':
              if (!(str == "stars"))
                return;
              // ISSUE: method pointer
              DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(EffectsStars)), 100);
              return;
            case 'w':
              if (!(str == "weald"))
                return;
              // ISSUE: method pointer
              DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(EffectsWeald)), 100);
              return;
            default:
              return;
          }
        case 6:
          switch (str[0])
          {
            case 'J':
              if (!(str == "Jester"))
                return;
              // ISSUE: method pointer
              DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(EffectsJester)), 100);
              return;
            case 'r':
              if (!(str == "return"))
                return;
              if (this.npc is StardewDruid.Character.Effigy)
              {
                // ISSUE: method pointer
                DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(RitesEffigy)), 100);
                return;
              }
              // ISSUE: method pointer
              DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(RitesJester)), 100);
              return;
            default:
              return;
          }
      }
    }

    public void EffectsWeald()
    {
      Game1.drawDialogue(this.npc, "In times past, the King of Oaks and the King of Holly would war upon the Equinox. Their warring would conclude for the sake of new life in Spring. When need arose, they lent their strength to a conflict from which neither could fully recover. They rest now, dormant. May those awake guard the change of seasons.");
    }

    public void EffectsMists()
    {
      Game1.drawDialogue(this.npc, "The Voice is that of the Lady of the Isle of Mists. She is as beautiful and distant as the sunset on the Gem Sea. The first farmer knew her. (The Effigy's eyes flicker a brilliant turqoise). He has imprinted a feeling within my memories, a feeling I cannot describe.");
    }

    public void EffectsStars()
    {
      Game1.drawDialogue(this.npc, "The Stars have no names that can be uttered by earthly dwellers. They exist high above, and beyond, and care not for the life of our world, though their light sustains much of it.The Stars that have shown on you belong to constellation of Sisters. There is one star... a fallen star. This one the Druids did give a name too. (Effigy's flaming eyes flicker). I have been forbidden to share it.");
    }

    public void EffectsFates()
    {
      if (this.npc is StardewDruid.Character.Effigy)
        Game1.drawDialogue(this.npc, "The Fates weave the cords of destiny into the great tapestry that is the story of the world. It is said that they each serve a special purpose known only to Yoba, and so they often appear to work by mystery and happenchance, by whim even. (Effigy motions ever so slightly in the direction of Jester) Many have been known to stray from their duty.");
      else
        Game1.drawDialogue(this.npc, "Flameface is right. Every Fate has a special role we're given by Fortumei, the greatest of us, priestess of Yoba. Some of us are fairies, and care for the fates of plants and little things. For my contribution, well, I've had some pretty cool moments... (Jester is pensive as it's voice trails off)");
    }

    public void EffectsEther()
    {
      if (this.npc is StardewDruid.Character.Effigy)
        Game1.drawDialogue(this.npc, "I know very little of Dragonkind and their ilk. They were the first servants of Yoba, and perhaps they disappointed their creator. Their bones have become the foundation of the other world, their potent life essence has become the streams of ether that flow through the planes.");
      else
        Game1.drawDialogue(this.npc, "We're talking about creatures that could reforge the world itself, Farmer. They don't like our kind, otherfolk or humanfolk. I'm... (Jester's hairs raise across it's backside) scared of them. I'm glad you're here to face them with me.");
    }

    public void EffectsJester()
    {
      Game1.drawDialogue(this.npc, "I'm as lost as when I started. But, I have found out something about myself, something disturbing, embarrassing, and yet, I must accept it. I like to hide in boxes.");
    }

    public void DialogueFarmhands()
    {
      string str = "Teach them to embrace the source, or seize it.";
      Mod.instance.TrainFarmhands();
      Game1.drawDialogue(this.npc, str);
    }

    public void DialogueAttune()
    {
      List<string> stringList = QuestData.RitesProgress();
      Mod.instance.CurrentBlessing();
      List<Response> responseList = new List<Response>();
      int toolIndex = Mod.instance.AttuneableWeapon();
      string str1 = Mod.instance.AttunedWeapon(toolIndex);
      string str2;
      if (str1 != "reserved")
      {
        if (this.npc is StardewDruid.Character.Effigy)
        {
          str2 = "Forgotten Effigy: ^To whom should this " + ((Item) Game1.player.CurrentTool).Name + " be dedicated to?^";
          if (str1 != "weald" && stringList.Contains("weald"))
            responseList.Add(new Response("weald", "To the Two Kings (Rite of the Weald)"));
          if (str1 != "mists" && stringList.Contains("mists"))
            responseList.Add(new Response("mists", "To the Lady Beyond the Shore (Rite of the Mists)"));
          if (str1 != "stars" && stringList.Contains("stars"))
            responseList.Add(new Response("stars", "To the Stars Themselves (Rite of the Stars)"));
        }
        else
        {
          str2 = "The Jester of Fate: ^Who do you want to use this " + ((Item) Game1.player.CurrentTool).Name + " against - I mean in honor of?^";
          if (str1 != "fates" && stringList.Contains("fates"))
            responseList.Add(new Response("fates", "To the Folk of Mystery (Rite of the Fates)"));
          if (str1 != "ether" && stringList.Contains("ether"))
            responseList.Add(new Response("ether", "To the Masters of the Ether (Rite of the Ether)"));
        }
        responseList.Add(new Response("none", "I want to reclaim it for myself (removes attunement)"));
      }
      else
        str2 = !(this.npc is StardewDruid.Character.Effigy) ? "The Jester of Fate: ^I don't think " + ((Item) Game1.player.CurrentTool).Name + " wants to change^" : "Forgotten Effigy: ^This " + ((Item) Game1.player.CurrentTool).Name + " will serve no other master^";
      responseList.Add(new Response("return", "(nevermind)"));
      // ISSUE: method pointer
      GameLocation.afterQuestionBehavior questionBehavior = new GameLocation.afterQuestionBehavior((object) this, __methodptr(AnswerAttune));
      ((StardewValley.Character) Game1.player).currentLocation.createQuestionDialogue(str2, responseList.ToArray(), questionBehavior, this.npc);
    }

    public void AnswerAttune(Farmer effigyVisitor, string effigyAnswer)
    {
      string str1 = "This " + ((Item) Game1.player.CurrentTool).Name + " will serve ";
      string str2;
      switch (effigyAnswer)
      {
        case "return":
          if (this.npc is StardewDruid.Character.Effigy)
          {
            // ISSUE: method pointer
            DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(RitesEffigy)), 100);
            return;
          }
          // ISSUE: method pointer
          DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(RitesJester)), 100);
          return;
        case "none":
          string str3 = "This " + ((Item) Game1.player.CurrentTool).Name + " will no longer serve.";
          Mod.instance.DetuneWeapon();
          Game1.drawDialogue(this.npc, str3);
          return;
        case "stars":
          str2 = str1 + "the very Stars Themselves";
          break;
        case "mists":
          str2 = str1 + "the Lady Beyond the Shore";
          break;
        case "fates":
          str2 = str1 + "the Folk of Mystery";
          break;
        case "ether":
          str2 = str1 + "the Masters of the Ether";
          break;
        default:
          str2 = str1 + "the Two Kings";
          break;
      }
      Mod.instance.AttuneWeapon(effigyAnswer);
      Game1.drawDialogue(this.npc, str2);
    }
  }
}
