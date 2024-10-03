using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace StardewDruid.Character
{
    public class Marlon : StardewDruid.Character.Character
    {

        public Marlon()
        {
        }

        public Marlon(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            base.LoadOut();

            idleFrames[idles.standby] = new()
            {
                [0] = new List<Rectangle> { new Rectangle(160, 32, 32, 32), },
            };

        }

    }

}
