using Microsoft.Xna.Framework;

namespace StardewDruid.Cast.Fates
{
    internal class Whisk : CastHandle
    {

        public Vector2 destination;

        public Whisk(Vector2 target,  Vector2 Destination)
            : base(target)
        {

            destination = Destination;

        }

        public override void CastEffect()
        {
            
            WhiskEvent whiskEvent = new(targetVector, destination);

            whiskEvent.EventTrigger();

            castFire = true;

        }

    }

}
