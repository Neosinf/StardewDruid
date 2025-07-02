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
    public class SanctuaryPergola : TerrainField
    {

        public SanctuaryPergola(Vector2 Position)
            : base()
        {

            tilesheet = IconData.tilesheets.sanctuary;

            index = 0;

            position = Position;

            color = Color.White;

            shadow = shadows.offset;

            flip = false;

            reset();

        }

        public override void reset()
        {

            source = new(0, 128, 192, 32);

            baseTiles.Clear();

            Vector2 tile = ModUtility.PositionToTile(position);

            baseTiles.Add(new Vector2(tile.X + 1, tile.Y + 4));

            baseTiles.Add(new Vector2(tile.X + 4, tile.Y + 4));

            baseTiles.Add(new Vector2(tile.X + 7, tile.Y + 4));

            baseTiles.Add(new Vector2(tile.X + 10, tile.Y + 4));

            baseTiles.Add(new Vector2(tile.X + 1, tile.Y + 7));

            baseTiles.Add(new Vector2(tile.X + 4, tile.Y + 7));

            baseTiles.Add(new Vector2(tile.X + 7, tile.Y + 7));

            baseTiles.Add(new Vector2(tile.X + 10, tile.Y + 7));

            Vector2 corner00 = position;

            Vector2 corner11 = new Vector2(tile.X + 11, tile.Y + 7) * 64;

            center = corner00 + (corner11 - corner00) / 2;

            girth = Vector2.Distance(corner00, center);

            clearance = (int)Math.Ceiling(girth / 64);

            backing = 72;

            bounds = new((int)position.X + 8, (int)position.Y, 768 - 16, 512 - backing);

            layer = (position.Y + 480) / 10000;

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            Microsoft.Xna.Framework.Rectangle columnSource = new(0, 160, 16, 64);

            // column shadows

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(68, 264), columnSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0193f);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(260, 264), columnSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.002f);
            
            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(452, 264), columnSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0193f);
            
            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(644, 264), columnSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.002f);

            // pergola shadow

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(8, 200), useSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0065f);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(8, 392), useSource, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0065f);

            // columns

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(64, 64), columnSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0192f);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(256, 64), columnSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0192f);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(448, 64), columnSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0192f);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(640, 64), columnSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0192f);

            // back columns

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(64, 256), columnSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(256, 256), columnSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(448, 256), columnSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(640, 256), columnSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

        public override void drawFront(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            // pergola

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, 192), useSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

    }


}
