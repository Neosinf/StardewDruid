using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast.Earth
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
