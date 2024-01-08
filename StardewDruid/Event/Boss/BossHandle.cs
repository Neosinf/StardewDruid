using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Characters;
using System;
using System.Collections.Generic;

namespace StardewDruid.Event.Boss
{
    public class BossHandle : EventHandle
    {

        public readonly Quest questData;

        public BossHandle(Vector2 target, Rite rite, Quest quest)
            : base(target, rite)
        {
            questData = quest;
        }

        public override void EventTrigger()
        {

            Mod.instance.RegisterEvent(this, "active");

            Game1.addHUDMessage(new HUDMessage($"Boss fight initiated", ""));

        }

        public override bool EventActive()
        {

            int diffTime = (int)Math.Round(expireTime - Game1.currentGameTime.TotalGameTime.TotalSeconds);

            if (activeCounter != 0 && diffTime % 10 == 0 && diffTime != 0)
            {

                MinutesLeft(diffTime);

            }

            return base.EventActive();

        }

        public override void EventAbort()
        {

            Game1.addHUDMessage(new HUDMessage($"Boss fight aborted", ""));

            base.EventAbort();

        }


    }

}
