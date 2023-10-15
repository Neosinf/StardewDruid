﻿using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast
{
    internal class GreetVillager : CastHandle
    {

        public GreetVillager (Mod mod, Vector2 target, Rite rite, NPC witness)
            : base(mod, target, rite)
        {

            bool friendShip = false;
            
            if (riteData.castTask.ContainsKey("masterVillager"))
            {
            
                friendShip = true;
            
            }

            bool greetVillager = ModUtility.GreetVillager(rite.castLocation, rite.caster, witness, friendShip);

            if (!riteData.castTask.ContainsKey("masterVillager") && greetVillager)
            {

                mod.UpdateTask("lessonVillager", 1);

            }

        }

    }

}