using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;

namespace StardewDruid.Cast.Fates
{
    public class WhiskEvent : EventHandle
    {

        public Vector2 origin;

        public Vector2 destination;

        public int whiskCounter;

        public WhiskEvent(Vector2 target,  Vector2 Destination)
            : base(target)
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 2;

            origin = targetVector * 64f;

            destination = Destination * 64;

        }

        public override void EventTrigger()
        {

            animations.Add(ModUtility.AnimateCursor(targetLocation, origin, destination - new Vector2(0, 32), "Fates"));

            Mod.instance.RegisterEvent(this, "whisk");

            Mod.instance.clickRegister[1] = "whisk";

        }

        public override bool EventPerformAction(SButton Button, string Type)
        {

            if (Type != "Action")
            {

                return false;

            }

            if (!EventActive())
            {

                return false;

            }

            if (!Mod.instance.rite.castTask.ContainsKey("masterWhisk"))
            {

                Mod.instance.UpdateTask("lessonWhisk", 1);

            }

            PerformWarp();

            return true;

        }

        public override bool EventActive()
        {

            if (whiskCounter > 12)
            {

                return false;

            }

            return base.EventActive();

        }

        public override void EventDecimal()
        {
            whiskCounter++;
        }

        public override void EventInterval()
        {

            if (Mod.instance.rite.caster.isRidingHorse())
            {

                PerformWarp();

            }

        }

        public void PerformWarp()
        {

            Game1.flashAlpha = 1;

            //ModUtility.AnimateQuickWarp(targetLocation, Mod.instance.rite.caster.Position - new Vector2(0, 32), true);

            Mod.instance.rite.caster.Position = destination;

            ModUtility.AnimateQuickWarp(targetLocation, destination);

            RemoveAnimations();

            expireEarly = true;

        }


    }

}
