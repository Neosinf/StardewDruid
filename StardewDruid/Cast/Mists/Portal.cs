using Microsoft.Xna.Framework;

namespace StardewDruid.Cast.Mists
{
    internal class Portal : CastHandle
    {

        public Portal(Vector2 target, Rite rite)
            : base(target, rite)
        {

            castCost = 0;

        }

        public override void CastEffect()
        {

            Event.World.Portal portalEvent = new(targetVector, riteData);

            portalEvent.EventTrigger();

            castLimit = true;

            castFire = true;

        }

    }

}
