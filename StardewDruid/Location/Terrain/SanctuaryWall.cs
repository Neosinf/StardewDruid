using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location.Terrain
{
    public class SanctuaryWall : TerrainField
    {

        public SanctuaryWall(Vector2 Position, int Index)
            : base()
        {

            tilesheet = IconData.tilesheets.sanctuary;

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

                    source = new(0, 224, 16, 32);

                    break;

                case 2:

                    source = new(16, 224, 16, 32);

                    break;

                case 3:

                    source = new(32, 224, 16, 32);

                    break;

                case 4:
                case 5:
                    source = new(48, 224, 16, 32);

                    break;

                case 11:

                    source = new(0, 256, 64, 32);

                    break;

                case 12:

                    source = new(64, 256, 32, 32);

                    break;
                case 13:

                    source = new(96, 256, 16, 48);

                    break;

                case 14:

                    source = new(112, 256, 16, 32);

                    break;
            }

            baseTiles.Clear();

            Vector2 tile = ModUtility.PositionToTile(position);

            Vector2 range = new Vector2(source.Width / 16, source.Height / 16);

            center = position + (range * 32);

            girth = Vector2.Distance(position, center);

            clearance = (int)Math.Ceiling(girth / 64);

            backing = 72;

            bounds = new((int)position.X + 8, (int)position.Y, source.Width * 4 - 16, source.Height * 4 - backing);

            layer = (position.Y - 32 + (source.Height * 4)) / 10000;

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            DrawShadow(b, origin, useSource, 1f, shadow);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }


    }


}
