﻿using Microsoft.Xna.Framework;
using StardewDruid.Dialogue;
using StardewValley;

namespace StardewDruid.Cast.Weald
{
    internal class Villager : CastHandle
    {

        NPC riteWitness;

        public Villager(Vector2 target, Rite rite, NPC witness)
            : base(target, rite)
        {

            riteWitness = witness;

        }

        public override void CastEffect()
        {

            int friendship = 0;

            if (riteData.castTask.ContainsKey("masterVillager"))
            {

                friendship = 25;

            }

            bool greetVillager = ModUtility.GreetVillager(riteData.caster, riteWitness, friendship);

            if (!riteData.castTask.ContainsKey("masterVillager") && greetVillager)
            {

                Mod.instance.UpdateTask("lessonVillager", 1);

            }

            Reaction.ReactTo(riteWitness, "Weald", friendship);

        }

    }

}
