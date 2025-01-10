using Microsoft.Xna.Framework;
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
            
            base.LoadOut();

            setScale = 3.75f;

            specialFrames = CharacterRender.WitchSpecial();

            specialIntervals = CharacterRender.WitchIntervals();

            specialCeilings = CharacterRender.WitchCeilings();

            specialFloors = CharacterRender.WitchFloors();

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
