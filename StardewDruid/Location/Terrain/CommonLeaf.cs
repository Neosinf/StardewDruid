using StardewValley.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location.Terrain
{

    public class CommonLeaf
    {

        public enum commonleaves
        {
            top,
            mid,
            bottom,
            topright,
            topleft,
            bottomright,
            bottomleft,
            litter
        }

        public commonleaves tier;

        public enum commonrenders
        {
            darkoak,
            hawthorn,
            holly,
        }

        public commonrenders render;

        public Microsoft.Xna.Framework.Rectangle source;

        public Microsoft.Xna.Framework.Color colour;

        public int index;

        public int off;

        public bool flip;

        public float shade;

        public float rotate;

        public int where;

        public float wind;

        public float windout;

        public CommonLeaf(commonleaves Tier, int Where, int Off = 0, commonrenders Render = commonrenders.darkoak)
        {

            where = Where;

            render = Render;

            tier = Tier;

            switch (tier)
            {
                case commonleaves.top:

                case commonleaves.mid:

                case commonleaves.bottom:

                    index = Mod.instance.randomIndex.Next(5);

                    flip = Mod.instance.randomIndex.NextBool();

                    break;

                case commonleaves.topright:

                case commonleaves.bottomright:

                    index = Mod.instance.randomIndex.Next(5, 10);

                    break;

                case commonleaves.topleft:

                case commonleaves.bottomleft:

                    index = Mod.instance.randomIndex.Next(5, 10);

                    flip = true;

                    break;

                case commonleaves.litter:

                    index = Mod.instance.randomIndex.Next(10, 15);

                    flip = Mod.instance.randomIndex.NextBool();

                    rotate = Mod.instance.randomIndex.Next(4) * 0.5f * (float)Math.PI;

                    break;


            }

            off = Off;

            set();

        }

        public void set()
        {

            source = new(0 + (32 * (index % 5)), 32 * (int)(index / 5), 32, 32);

            switch (render)
            {

                case commonrenders.darkoak:

                    colour = new(256 - (off * 1), 256 - (off * 3), 256 - (off * 1));

                    break;

                case commonrenders.holly:

                    colour = new(256 - (off * 1), 256 - (off * 2), 256 - (off * 2));

                    break;

                default:

                    colour = new(256 - (off * 2), 256 - (off * 2), 256);

                    break;

            }

        }

    }

}
