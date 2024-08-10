
using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewValley;


namespace StardewDruid.Event.Bones
{
    internal class BonesClearing : EventHandle
    {

        public BonesClearing()
        {

        }

        public override void EventInterval()
        {

            activeCounter++;

            switch (activeCounter)
            {

                case 1:

                    eventComplete = true;

                    break;

            }

        }

    }

}