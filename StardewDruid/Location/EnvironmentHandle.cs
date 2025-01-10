using System;
using System.Collections.Generic;
using System.Linq;
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
            trunkOffset,
            smallTrunkOffset,
            leafRotate,
            leafOffset,
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

            int state = (50 + counter - where) % 50;

            switch (effect)
            {

                case environmentEffect.trunkRotate:

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

                case environmentEffect.trunkOffset:

                    switch (state)
                    {

                        case 40:
                        case 49:

                            return 1f;

                        case 41:
                        case 48:

                            return 2f;

                        case 42:
                        case 47:

                            return 3f;

                        case 43:
                        case 46:

                            return 4f;

                        case 44:
                        case 45:

                            return 5f;

                        default:

                            return 0f;

                    }

                case environmentEffect.smallTrunkOffset:

                    switch (state)
                    {

                        case 40:
                        case 49:

                            return 0.5f;

                        case 41:
                        case 48:

                            return 1f;

                        case 42:
                        case 47:

                            return 1.5f;

                        case 43:
                        case 46:

                            return 2f;


                        case 44:
                        case 45:

                            return 2.5f;

                        default:

                            return 0f;


                    }

                case environmentEffect.leafRotate:

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

                case environmentEffect.leafOffset:

                    switch (state)
                    {

                        case 40:
                        case 49:

                            return 2f;

                        case 41:
                        case 48:

                            return 4f;

                        case 42:
                        case 47:

                            return 6f;

                        case 43:
                        case 46:

                            return 8f;

                        case 44:
                        case 45:

                            return 10f;

                        default:

                            return 0f;


                    }

            }

            return 0f;

        }

    }

}
