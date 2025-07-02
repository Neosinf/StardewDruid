using Microsoft.Xna.Framework;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewValley;
using xTile.Dimensions;

namespace StardewDruid.Character
{
    public class Lady : StardewDruid.Character.Character
    {

        public Lady()
        {
        }

        public Lady(CharacterHandle.characters type)
          : base(type)
        {

            
        }
        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.LadyBeyond;

            }

            LoadOutLady();

            cooldownInterval = 90;

            restSet = true;

        }

        public override void behaviorOnFarmerPushing()
        {

            return;

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {

            return false;

        }

    }

}
