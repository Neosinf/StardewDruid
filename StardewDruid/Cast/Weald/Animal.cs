using Microsoft.Xna.Framework;
using StardewValley;

namespace StardewDruid.Cast.Weald
{
    internal class Animal : CastHandle
    {

        FarmAnimal riteWitness;

        public Animal(Vector2 target, Rite rite, FarmAnimal animal)
            : base(target, rite)
        {

            riteWitness = animal;

        }
        public override void CastEffect()
        {

            ModUtility.PetAnimal(riteData.caster, riteWitness);

        }
    }

}
