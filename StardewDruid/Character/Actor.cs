// Decompiled with JetBrains decompiler
// Type: StardewDruid.Character.Actor
// Assembly: StardewDruid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24DA4344-683E-4959-87A6-C0A858BCC7DA
// Assembly location: C:\Users\piers\source\repos\StardewDruid\StardewDruid\bin\Debug\net5.0\StardewDruid.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;

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

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            if (Context.IsMainPlayer && drawSlave)
            {
                foreach (NPC character in currentLocation.characters)
                {
                    //f (!(character is StardewDruid.Character.Actor))
                        character.drawAboveAlwaysFrontLayer(b);
                }
                //this.drawAboveAlwaysFrontLayer(b);
            }
            base.draw(b, alpha);
        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {
            if (textAboveHeadTimer > 0 && textAboveHead != null)
            {
                
                Vector2 vector = Game1.GlobalToLocal(new Vector2(getStandingX(), getStandingY() -128f));

                SpriteText.drawStringWithScrollCenteredAt(b, textAboveHead, (int)vector.X, (int)vector.Y, "", textAboveHeadAlpha, textAboveHeadColor, 1, 999f);
            
            }

        }

    }
}
