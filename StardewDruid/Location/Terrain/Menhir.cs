using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewDruid.Location.Druid;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Tiles;

namespace StardewDruid.Location.Terrain
{

    public class Menhir : TerrainField
    {

        public Menhir(Vector2 Position, int Index)
           : base()
        {

            tilesheet = StardewDruid.Data.IconData.tilesheets.grove;

            index = Index;

            position = Position;

            color = Color.White;

            shadow = shadows.deepset;

            flip = false;

            shade = 0.3f;

            reset();

        }

        public override void reset()
        {

            Vector2 tile = ModUtility.PositionToTile(position);

            int depth = 2;

            int width = 3;

            int start = 4;

            switch (index)
            {

                case 11:

                    source = new(0, 0, 48, 96);

                    break;

                case 12:

                    source = new(48, 0, 48, 96);

                    break;

                case 13:

                    source = new(96, 0, 48, 96);

                    break;

                case 14:

                    source = new(144, 0, 48, 96);

                    break;

                case 15:

                    source = new(192, 0, 48, 96);

                    break;

                case 16:

                    source = new(0, 96, 48, 96);

                    break;

                case 17:

                    source = new(48, 96, 96, 48);

                    width = 6;

                    start = 1;

                    fadeout = 1f;

                    break;

                case 18:

                    source = new(48, 144, 48, 48);

                    start = 1;

                    fadeout = 1f;

                    break;

                case 19:

                    source = new(144, 96, 64, 32);

                    depth = 1;

                    start = 1;

                    width = 4;

                    fadeout = 1f;

                    break;

            }

            layer = (position.Y - 32 + (source.Height * 4)) / 10000;

            bounds = new((int)position.X, (int)position.Y, source.Width*4, source.Height*4);

            baseTiles.Clear();

            for (int y = 0; y < depth; y++)
            {

                for (int x = 0; x < width; x++)
                {

                    baseTiles.Add(new Vector2(tile.X + x, tile.Y + start + y));
                
                }
            
            }

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 512))
            {

                return;

            }

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            if(Game1.timeOfDay > 1700)
            {

                useSource.Y += 192;

            }

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.grove], origin + new Vector2(0, 8),useSource, Color.Black * shade * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

            b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.grove], origin, useSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

    }

}
