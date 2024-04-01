using Microsoft.Xna.Framework;
using StardewValley;

namespace StardewDruid.Cast.Weald
{
    internal class Animal : CastHandle
    {

        FarmAnimal riteWitness;

        public Animal(Vector2 target,  FarmAnimal animal)
            : base(target)
        {

            riteWitness = animal;

        }
        public override void CastEffect()
        {

            ModUtility.PetAnimal(Mod.instance.rite.caster, riteWitness);

        }

    }

}
