// Decompiled with JetBrains decompiler
// Type: StardewDruid.Dialogue.Reaction
// Assembly: StardewDruid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24DA4344-683E-4959-87A6-C0A858BCC7DA
// Assembly location: C:\Users\piers\source\repos\StardewDruid\StardewDruid\bin\Debug\net5.0\StardewDruid.dll

using Netcode;
using StardewValley;
using StardewValley.Network;
using System.Collections.Generic;

#nullable disable
namespace StardewDruid.Dialogue
{
  public static class Reaction
  {
    public static void ReactTo(NPC NPC, string cast, int friendship = 0, List<string> context = null)
    {
      List<string> stringList = new List<string>();
      switch (cast)
      {
        case "Fates":
          if (friendship >= 75)
          {
            ((Character) NPC).doEmote(20, true);
            stringList.Add("I just had an out of body experience.");
            stringList.Add("My perception of reality is all messed up.");
            stringList.Add("It's fantastic! $l");
            break;
          }
          if (friendship >= 50)
          {
            ((Character) NPC).doEmote(32, true);
            stringList.Add("That was great! You've brightened my day. $h");
            break;
          }
          if (friendship >= 25)
          {
            ((Character) NPC).doEmote(24, true);
            stringList.Add("Well that was an... experience.");
            break;
          }
          ((Character) NPC).doEmote(28, true);
          stringList.Add("(grumble) $a");
          break;
        case "Stars":
          ((Character) NPC).doEmote(8, true);
          stringList.Add("By Yoba. The sky opened up, and, fire came down, and, and, I blanked out.");
          break;
        case "Mists":
          ((Character) NPC).doEmote(16, true);
          stringList.Add("It's like the clouds follow you around. $s");
          break;
        case "Weald":
          if (((NetDictionary<string, Friendship, NetRef<Friendship>, SerializableDictionary<string, Friendship>, NetStringDictionary<Friendship, NetRef<Friendship>>>) Game1.player.friendshipData).ContainsKey(((Character) NPC).Name))
          {
            if (((NetDictionary<string, Friendship, NetRef<Friendship>, SerializableDictionary<string, Friendship>, NetStringDictionary<Friendship, NetRef<Friendship>>>) Game1.player.friendshipData)[((Character) NPC).Name].Points >= 1000)
            {
              ((Character) NPC).doEmote(20, true);
              stringList.Add("It's as if nature sings when you walk into town. $l");
              break;
            }
            if (((NetDictionary<string, Friendship, NetRef<Friendship>, SerializableDictionary<string, Friendship>, NetStringDictionary<Friendship, NetRef<Friendship>>>) Game1.player.friendshipData)[((Character) NPC).Name].Points >= 500)
            {
              ((Character) NPC).doEmote(32, true);
              stringList.Add("The valley has never felt more alive than since you moved here. $h");
              break;
            }
          }
          ((Character) NPC).doEmote(8, true);
          stringList.Add("Did you see that? I could swear the valley shimmered for a moment.");
          break;
        case "Ether":
          if (((NetDictionary<string, Friendship, NetRef<Friendship>, SerializableDictionary<string, Friendship>, NetStringDictionary<Friendship, NetRef<Friendship>>>) Game1.player.friendshipData).ContainsKey(((Character) NPC).Name))
          {
            if (((NetDictionary<string, Friendship, NetRef<Friendship>, SerializableDictionary<string, Friendship>, NetStringDictionary<Friendship, NetRef<Friendship>>>) Game1.player.friendshipData)[((Character) NPC).Name].Points >= 1000)
            {
              ((Character) NPC).doEmote(20, true);
              stringList.Add("The way your magical scales glitter in the light of the valley... $l");
              break;
            }
            if (((NetDictionary<string, Friendship, NetRef<Friendship>, SerializableDictionary<string, Friendship>, NetStringDictionary<Friendship, NetRef<Friendship>>>) Game1.player.friendshipData)[((Character) NPC).Name].Points >= 500)
            {
              ((Character) NPC).doEmote(32, true);
              stringList.Add("Thank you for protecting our town from evil $h");
              break;
            }
          }
          ((Character) NPC).doEmote(16, true);
          stringList.Add("Trogdor? Is... how did this happen?");
          stringList.Add("Are you still... human?.");
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
