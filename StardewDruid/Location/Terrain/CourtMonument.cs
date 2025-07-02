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

    public class CourtMonument : TerrainField
    {

        public CourtMonument(Vector2 Position, int Index)
           : base()
        {

            tilesheet = StardewDruid.Data.IconData.tilesheets.court;

            index = Index;

            position = Position;

            color = Color.White;

            shadow = shadows.deepset;

            flip = false;

            shade = 0.3f;

            fadeout = 0.75f;

            reset();

        }

        public override void reset()
        {

            Vector2 tile = ModUtility.PositionToTile(position);

            int depth = 2;

            int width = 4;

            int start = 6;

            switch (index)
            {

                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:

                    depth = 1;

                    width = 2;

                    start = 3;

                    break;

            }

            switch (index)
            {
                case 1:

                    source = new(0, 0, 64, 128);

                    depth = 2;

                    break;

                case 2:

                    source = new(64, 0, 64, 128);

                    depth = 2;

                    break;

                case 3:

                    source = new(128, 0, 64, 128);

                    depth = 2;

                    break;

                case 4:

                    source = new(192, 0, 64, 128);

                    depth = 2;

                    break;

                // hangouts
                case 5:

                    source = new(256, 0, 32, 64);

                    break;

                case 6:

                    source = new(288, 0, 16, 64);

                    break;

                case 7:

                    source = new(304, 0, 48, 64);

                    break;

                case 8:

                    source = new(352, 0, 32, 64);

                    break;

                // lanterns
                case 9:

                    source = new(256, 64, 32, 64);

                    break;

                case 10:

                    source = new(288, 64, 32, 64);

                    break;

                case 11:

                    source = new(320, 64, 32, 64);

                    break;

                case 12:

                    source = new(352, 64, 32, 64);

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

                useSource.Y += 128;

            }

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, 8),useSource, Color.Black * shade * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

    }

}
