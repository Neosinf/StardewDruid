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

    public class GroveBowl : TerrainField
    {

        public GroveBowl(Vector2 Position, int Index)
           : base()
        {

            tilesheet = StardewDruid.Data.IconData.tilesheets.grove;

            index = Index;

            position = Position;

            color = Color.White;

            shadow = shadows.offset;

            flip = false;

            shade = 0.3f;

            fadeout = 0.25f;

            reset();

        }

        public override void reset()
        {

            layer = (position.Y + 192) / 10000;

            source = new(208, 96, 32, 32);

            bounds = new((int)position.X, (int)position.Y, source.Width*4, source.Height*4);

            baseTiles.Clear();

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (disabled)
            {

                return;

            }

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

            b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.grove], origin + new Vector2(2, 10), useSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

            b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.grove], origin, useSource, color, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

    }

}
