// Decompiled with JetBrains decompiler
// Type: StardewDruid.Character.Actor
// Assembly: StardewDruid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24DA4344-683E-4959-87A6-C0A858BCC7DA
// Assembly location: C:\Users\piers\source\repos\StardewDruid\StardewDruid\bin\Debug\net5.0\StardewDruid.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;

#nullable disable
namespace StardewDruid.Character
{
  public class Actor : StardewDruid.Character.Character
  {
    public bool drawSlave;

    public Actor(Vector2 position, string map, string Name)
      : base(position, map, Name)
    {
    }

    public virtual void draw(SpriteBatch b, float alpha = 1f)
    {
      if (Context.IsMainPlayer && this.drawSlave)
      {
        foreach (NPC character in ((StardewValley.Character) this).currentLocation.characters)
        {
          if (!(character is StardewDruid.Character.Character))
            ((StardewValley.Character) character).drawAboveAlwaysFrontLayer(b);
        }
        ((StardewValley.Character) this).drawAboveAlwaysFrontLayer(b);
      }
      base.draw(b, alpha);
    }
  }
}
