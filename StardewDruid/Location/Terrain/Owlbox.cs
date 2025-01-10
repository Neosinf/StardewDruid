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
    public class Owlbox : TerrainField
    {

        public Owlbox(IconData.tilesheets Tilesheet, int Index, Vector2 Position, shadows Shadow = shadows.offset, bool Flip = false)
            : base(Tilesheet, Index,Position,Shadow,Flip)
        {

        }
        public override void drawFront(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {
                return;
            }

            if (ruined)
            {

                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X + 144, source.Y, source.Width, source.Height - 48);

            float opacity = Fadeout(location, useSource);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color * opacity, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {
                return;
            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            float shadowScale = 1.5f;

            if (!ruined)
            {

                useSource = new(source.X + 144, source.Y + source.Height - 48, source.Width, 48);

                origin.Y += ((source.Height - 48) * 4);

                shadowScale = 3f;

            }

            float opacity = Fadeout(location, useSource);

            b.Draw(Game1.mouseCursors, origin + new Vector2(useSource.Width * 2 - 2, useSource.Height * 4 - 12), new Rectangle(663, 1011, 41, 30), Color.White, 0f, new Vector2(20, 15), shadowScale, flip ? (SpriteEffects)1 : 0, 1E-06f);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color * opacity, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

    }


}
