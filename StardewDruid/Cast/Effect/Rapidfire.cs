using StardewDruid.Data;
using StardewDruid.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Cast.Effect
{
    public class Rapidfire : EventHandle
    {

        public int chainCounter;

        public bool chainHeat;

        public Rapidfire()
        {

            activeLimit = -1;

            chainCounter = 50;

        }

        public override void EventDecimal()
        {

            chainCounter--;

            if (chainCounter == 0)
            {

                eventComplete = true;

                return;

            }

            if(chainCounter < 50 && chainHeat)
            {

                chainHeat = false;

            }

        }

        public bool Discharge(int factor)
        {

            if (chainHeat)
            {

                if(chainCounter > 52)
                {

                    return false;

                }

                chainHeat = false;

            }

            if(chainCounter <= 50)
            {

                chainCounter = 50;

                EventBar(StringData.Strings(StringData.stringkeys.rapidfire), 1);

            }

            chainCounter += (3 * factor);

            if(chainCounter > 75)
            {

                chainHeat = true;

                EventDisplay heatbar = EventBar(StringData.Strings(StringData.stringkeys.overheated), 2);

                heatbar.colour = Microsoft.Xna.Framework.Color.DarkMagenta;

            }

            return true;

        }

        public override float DisplayProgress(int displayId)
        {

            switch (displayId)
            {

                case 1: // rapidfire

                    if (chainHeat)
                    {

                        return -1f;

                    }

                    if(chainCounter > 50)
                    {

                        return (float)(chainCounter - 50) / 25f;

                    }

                    return -1f;

                case 2: // overheated

                    if (!chainHeat)
                    {

                        return -1f;

                    }

                    if (chainCounter > 50)
                    {

                        return (float)(chainCounter - 50) / 25f;

                    }

                    return -1f;


            }

            return -1f;

        }


    }

}
