using Microsoft.Xna.Framework.Audio;
using StardewDruid.Cast;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location
{
    public class EnvironmentHandle
    {

        public int counter;

        public enum environmentEffect
        {
            trunkRotate,
            trunkShake,
            leafRotate,
            leafShake,
            grassRotate,
            grassShake,
            candleWind,
            lilyFloat,
        }

        public void update()
        {

            counter++;

            if (counter >= 50)
            {

                counter = 0;

            }

        }

        public float retrieve(int where, environmentEffect effect)
        {

            int state;

            switch (effect)
            {

                case environmentEffect.trunkRotate:

                    state = (50 + counter - where) % 50;

                    switch (state)
                    {

                        case 40:
                        case 49:

                            return 0.0025f;

                        case 41:
                        case 48:

                            return 0.005f;

                        case 42:
                        case 47:

                            return 0.0075f;

                        case 43:
                        case 46:

                            return 0.01f;

                        case 44:
                        case 45:

                            return 0.0125f;

                        default:

                            return 0f;

                    }

                case environmentEffect.leafRotate:

                    state = (50 + counter - where) % 50;

                    switch (state)
                    {

                        case 40:
                        case 49:

                            return 0.025f;

                        case 41:
                        case 48:

                            return 0.05f;

                        case 42:
                        case 47:

                            return 0.075f;

                        case 43:
                        case 46:

                            return 0.1f;

                        case 44:
                        case 45:

                            return 0.125f;

                        default:

                            return 0f;

                    }

                case environmentEffect.grassRotate:

                    state = (50 + counter - where) % 50;

                    switch (state)
                    {

                        case 40:
                        case 49:

                            return 0.0125f;

                        case 41:
                        case 48:

                            return 0.025f;

                        case 42:
                        case 47:

                            return 0.0375f;

                        case 43:
                        case 46:

                            return 0.05f;

                        case 44:
                        case 45:

                            return 0.0675f;

                        default:

                            return 0f;

                    }

                case environmentEffect.trunkShake:

                    switch (where)
                    {

                        case 40:
                        case 31:

                            return 0.0025f;

                        case 39:
                        case 32:

                            return 0.005f;

                        case 38:
                        case 33:

                            return 0.0075f;

                        case 37:
                        case 34:

                            return 0.01f;

                        case 36:
                        case 35:

                            return 0.0125f;

                        case 30:
                        case 21:

                            return 0f;

                        case 29:
                        case 22:

                            return -0.002f;

                        case 28:
                        case 23:

                            return -0.004f;

                        case 27:
                        case 24:

                            return -0.006f;

                        case 26:
                        case 25:

                            return -0.008f;

                        case 20:
                        case 11:

                            return 0.00125f;

                        case 19:
                        case 12:

                            return 0.0025f;

                        case 18:
                        case 13:

                            return 0.00375f;

                        case 17:
                        case 14:

                            return 0.005f;

                        case 16:
                        case 15:

                            return 0.00625f;

                        case 10:
                        case 1:

                            return 0f;

                        case 9:
                        case 2:

                            return -0.0005f;

                        case 8:
                        case 3:

                            return -0.001f;

                        case 7:
                        case 4:

                            return -0.0015f;

                        case 6:
                        case 5:

                            return -0.002f;

                    }

                    break;

                case environmentEffect.leafShake:

                    switch (where)
                    {

                        case 40:
                        case 31:

                            return 0.025f;

                        case 39:
                        case 32:

                            return 0.05f;

                        case 38:
                        case 33:

                            return 0.075f;

                        case 37:
                        case 34:

                            return 0.1f;

                        case 36:
                        case 35:

                            return 0.125f;

                        case 30:
                        case 21:

                            return 0f;

                        case 29:
                        case 22:

                            return -0.02f;

                        case 28:
                        case 23:

                            return -0.04f;

                        case 27:
                        case 24:

                            return -0.06f;

                        case 26:
                        case 25:

                            return -0.08f;

                        case 20:
                        case 11:

                            return 0.0125f;

                        case 19:
                        case 12:

                            return 0.025f;

                        case 18:
                        case 13:

                            return 0.0375f;

                        case 17:
                        case 14:

                            return 0.05f;

                        case 16:
                        case 15:

                            return 0.0625f;

                        case 10:
                        case 1:

                            return 0f;

                        case 9:
                        case 2:

                            return -0.005f;

                        case 8:
                        case 3:

                            return -0.01f;

                        case 7:
                        case 4:

                            return -0.015f;

                        case 6:
                        case 5:

                            return -0.02f;

                    }

                    break;

                case environmentEffect.grassShake:

                    switch (where)
                    {

                        case 40:
                        case 31:

                            return 0.0125f;

                        case 39:
                        case 32:

                            return 0.025f;

                        case 38:
                        case 33:

                            return 0.0375f;

                        case 37:
                        case 34:

                            return 0.05f;

                        case 36:
                        case 35:

                            return 0.0625f;

                        case 30:
                        case 21:

                            return 0f;

                        case 29:
                        case 22:

                            return -0.01f;

                        case 28:
                        case 23:

                            return -0.02f;

                        case 27:
                        case 24:

                            return -0.03f;

                        case 26:
                        case 25:

                            return -0.04f;

                        case 20:
                        case 11:

                            return 0.005f;

                        case 19:
                        case 12:

                            return 0.01f;

                        case 18:
                        case 13:

                            return 0.015f;

                        case 17:
                        case 14:

                            return 0.02f;

                        case 16:
                        case 15:

                            return 0.025f;

                        case 10:
                        case 1:

                            return 0f;

                        case 9:
                        case 2:

                            return -0.0025f;

                        case 8:
                        case 3:

                            return -0.005f;

                        case 7:
                        case 4:

                            return -0.0075f;

                        case 6:
                        case 5:

                            return -0.01f;

                    }

                    break;

                case environmentEffect.candleWind:

                    switch(counter)
                    {

                        case 19:
                        case 20:
                        case 23:
                        case 24:

                            return 2f;

                        case 21:
                        case 22:

                            return 3f;

                        case 44:
                        case 45:
                        case 48:
                        case 49:

                            return 2f;

                        case 46:
                        case 47:

                            return 3f;

                        default:

                            return 1f;

                    }

                case environmentEffect.lilyFloat:

                    state = (50 + counter - where) % 50;

                    switch (state)
                    {
                        case 40:
                        case 41:
                        case 48:
                        case 49:

                            return 1f;

                        case 42:
                        case 43:
                        case 46:
                        case 47:

                            return 2f;

                        case 44:
                        case 45:

                            return 3f;

                        default:

                            return 0f;

                    }

            }

            return 0f;

        }

    }

}
