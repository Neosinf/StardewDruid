using Microsoft.Xna.Framework;

namespace StardewDruid.Cast.Mists
{
    internal class Summon : CastHandle
    {

        public Summon(Vector2 target)
            : base(target)
        {

            castCost = 0;

        }

        public override void CastEffect()
        {

            Event.Challenge.Summon portalEvent = new(targetVector);

            portalEvent.EventTrigger();

            castLimit = true;

            castFire = true;

        }

    }

}
