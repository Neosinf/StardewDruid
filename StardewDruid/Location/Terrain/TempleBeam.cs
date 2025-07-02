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
    public class TempleBeam : TerrainField
    {

        public TempleBeam(Vector2 Position, int Index)
            : base()
        {

            tilesheet = IconData.tilesheets.temple;

            index = Index;

            position = Position;

            color = Color.White;

            shadow = shadows.offset;

            flip = false;

            reset();

        }

        public override void reset()
        {

            switch (index)
            {

                case 1:

                    source = new(0, 96, 80, 48);

                    break;

                case 2:

                    source = new(80, 96, 64, 48);

                    break;

                case 3:

                    source = new(144, 96, 64, 48);

                    break;

                case 4:

                    source = new(208, 96, 64, 48);

                    break;

                case 5:

                    source = new(0, 144, 80, 48);

                    break;

                case 6:

                    source = new(80, 144, 64, 48);

                    break;

                case 7:

                    source = new(144, 144, 64, 48);

                    break;

                case 9:

                    source = new(272, 112, 32, 80);

                    break;

                case 10:

                    source = new(304, 112, 32, 80);

                    break;

            }

            Vector2 tile = ModUtility.PositionToTile(position);

            baseTiles.Clear();

            switch (index)
            {
                default:

                    baseTiles.Add(new Vector2(tile.X + 0, tile.Y + 7));

                    baseTiles.Add(new Vector2(tile.X + 1, tile.Y + 7));

                    Vector2 range = new Vector2(source.Width / 16, source.Height / 16);

                    center = position + (range * 32);

                    girth = Vector2.Distance(position, center);

                    clearance = (int)Math.Ceiling(girth / 64);

                    backing = 16;

                    layer = (position.Y - 32 + (source.Height * 4)) / 10000;

                    break;

            }

            bounds = new((int)position.X + 8, (int)position.Y, source.Width * 4 - 16, source.Height * 4 - backing);

        }

        public override void drawFront(SpriteBatch b, GameLocation location)
        {
            
            if (!DrawCheck())
            {

                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {
            
        }

    }

}
