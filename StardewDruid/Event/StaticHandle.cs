﻿using StardewDruid.Cast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StardewDruid.Dialogue;
using StardewValley;

namespace StardewDruid.Event
{
    public class StaticHandle : EventHandle
    {
        public StaticHandle()
            : base(Vector2.Zero, new StardewDruid.Cast.Rite())
        {

        }

        public override void EventTrigger()
        {

            Mod.instance.eventRegister.Add("static", this);

        }

        public void AddBrazier(GameLocation location, Vector2 tile)
        {

            braziers.Add(new(location, tile));

        }

        public void ResetBrazier()
        {

            for (int i = braziers.Count - 1; i >= 0; i--)
            {

                Brazier brazier = braziers.ElementAt(i);

                if (!brazier.reset())
                {

                    braziers.RemoveAt(i);

                }

            }

        }

        public override bool EventActive()
        {
            
            return true;
        
        }

        public override void EventInterval()
        {
            activeCounter++;

            if(activeCounter == 10)
            {

                ResetBrazier();

                activeCounter = 0;

            }

        }

    }

}
