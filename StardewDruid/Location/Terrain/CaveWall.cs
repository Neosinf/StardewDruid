using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location.Terrain
{
    public class CaveWall : TerrainField
    {

        public CaveWall(Vector2 Position, int Index)
            : base()
        {

            tilesheet = IconData.tilesheets.cavern;

            index = Index;

            position = Position;

            color = Color.White;

            reset();

        }

        public override void reset()
        {

            int addHeight = 384;

            switch (index)
            {

                case 1:

                    source = new(0, 0, 128, 96);

                    break;

                case 2:

                    source = new(128, 0, 128, 96);

                    break;
                // ------------------------

                case 11:

                    source = new(0, 96, 64, 128);

                    break;
                case 12:

                    source = new(64, 96, 64, 128);

                    break;
                case 13:

                    source = new(128, 96, 64, 128);

                    break;

                case 14:

                    source = new(192, 96, 64, 128);

                    break;

                case 15:

                    source = new(256, 96, 64, 128);

                    break;

                case 16:

                    source = new(320, 96, 64, 128);

                    break;

                // ------------------------

                case 21:

                    source = new(0, 224, 48, 144);

                    break;

                case 22:

                    source = new(48, 224, 48, 144);

                    break;

                case 23:

                    source = new(96, 224, 48, 144);

                    break;

                case 24:

                    source = new(144, 224, 48, 144);

                    break;
                // ------------------------

                case 31:

                    source = new(0, 368, 64, 128);

                    break;

                case 32:

                    source = new(64, 368, 64, 128);

                    break;

                case 33:

                    source = new(128, 368, 64, 128);

                    break;

                case 34:

                    source = new(192, 368, 64, 128);

                    break;
                // ------------------------

                case 41:

                    source = new(0, 496, 128, 96);

                    break;

                case 42:

                    source = new(128, 496, 128, 96);

                    break;

                // ------------------------

                case 51:

                    source = new(0, 592, 32, 48);

                    addHeight = 192;

                    break;

                case 52:

                    source = new(32, 592, 32, 48);

                    addHeight = 192;

                    break;

            }

            layer = (position.Y + addHeight) / 10000;

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

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4,  0, layer);

        }

    }


}
