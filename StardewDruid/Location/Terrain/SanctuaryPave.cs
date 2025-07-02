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
    public class SanctuaryPave : TerrainField
    {

        public SanctuaryPave(Vector2 Position, int Index)
            : base()
        {

            tilesheet = IconData.tilesheets.sanctuary;

            index = Index;

            position = Position;

            color = Color.White;

            shadow = shadows.none;

            flip = false;

            reset();

        }

        public override void reset()
        {

            switch (index)
            {
                case 2:

                    source = new(0, 384, 224, 160);

                    break;

                case 1:

                    source = new(0, 544, 192, 96);

                    break;

                case 3:

                    source = new(192, 544, 96, 64);

                    break;

                case 4:

                    source = new(288, 544, 64, 96);

                    break;

            }

            baseTiles.Clear();

            bounds = new((int)position.X + 8, (int)position.Y, 640 - 16, 512 - backing);

            layer = (position.Y + 480) / 10000;

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

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, 1f);

        }


    }


}
