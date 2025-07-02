using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location.Terrain
{
    public class CaveWater : TerrainField
    {

        public CaveWater(Vector2 Position, int Index)
            : base()
        {

            tilesheet = IconData.tilesheets.cavernwater;

            index = Index;

            position = Position;

            color = Color.White;

            reset();

        }

        public override void reset()
        {

            int addHeight = 256;

            switch (index)
            {

                case 1:

                    source = new(0, 0, 96, 80);

                    break;

                case 2:

                    source = new(96, 0, 96, 80);

                    break;

                // ------------------------

                case 11:

                    source = new(0, 80, 80, 112);

                    break;

                case 12:

                    source = new(80, 80, 80, 112);

                    break;

                case 13:

                    source = new(160, 80, 80, 112);

                    break;

                case 14:

                    source = new(240, 80, 80, 112);

                    break;

                // ------------------------

                case 21:

                    source = new(0, 192, 48, 128);

                    break;

                case 22:

                    source = new(48, 192, 48, 128);

                    break;

                case 23:

                    source = new(96, 192, 48, 128);

                    break;

                case 24:

                    source = new(144, 192, 48, 128);

                    break;

                // ------------------------

                case 31:

                    source = new(0, 320, 80, 64);

                    break;

                case 32:

                    source = new(80, 320, 80, 64);

                    break;

                case 33:

                    source = new(160, 320, 80, 64);

                    break;

                case 34:

                    source = new(240, 320, 80, 64);

                    break;

                // ------------------------

                case 41:

                    source = new(0, 384, 96, 48);

                    break;

                case 42:

                    source = new(96, 384, 96, 48);

                    break;

            }

            layer = ((position.Y + addHeight) / 10000) + position.X/100000;

            bounds = new((int)position.X + 8, (int)position.Y, source.Width * 4 - 16, source.Height * 4 - backing);

        }

        public override void update(GameLocation location)
        {

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            layer = ((position.Y -64) / 10000) + ((position.X / 64) / 100000);

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4, 0, layer);

        }

    }

}
