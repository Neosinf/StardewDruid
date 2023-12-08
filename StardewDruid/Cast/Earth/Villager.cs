﻿using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast.Earth
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

        }

    }

}