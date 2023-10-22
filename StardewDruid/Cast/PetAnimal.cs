using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast
{
    internal class PetAnimal : CastHandle
    {

        FarmAnimal riteWitness;

        public PetAnimal (Mod mod, Vector2 target, Rite rite, FarmAnimal animal)
            : base(mod, target, rite)
        {

            riteWitness = animal;

        }
        public void GentlyCaress()
        {

            ModUtility.PetAnimal(riteData.caster, riteWitness);

        }
    }

}
