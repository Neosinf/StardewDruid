using Microsoft.Xna.Framework;
using StardewValley;

namespace StardewDruid.Cast.Fates
{
    internal class Trick : CastHandle
    {

        public NPC riteWitness;

        public Trick(Vector2 target, NPC witness)
            : base(target)
        {

            riteWitness = witness;

        }

        public override void CastEffect()
        {
            
            TrickEvent trickEvent = new(targetVector, riteWitness);

            trickEvent.EventTrigger();

            castFire = true;

        }

    }

}
