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
    public class EngineShrine : TerrainField
    {

        public bool opened;

        public EngineShrine(IconData.tilesheets Tilesheet, int Index, Vector2 Position, shadows Shadow = shadows.offset, bool Flip = false)
            : base(Tilesheet, Index, Position,Shadow,Flip)
        {

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle lever = new(160, 0, 32, 48);

            if (opened)
            {

                lever = new(160, 48, 32, 48);

            } 

            DrawShadow(b, origin, source, 1f, shadow);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(192, 256), lever, color, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer + 0.0001f);

        }

    }


}
