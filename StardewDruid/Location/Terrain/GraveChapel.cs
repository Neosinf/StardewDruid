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
    public class GraveChapel : TerrainField
    {

        public float fadetop;

        public float fadeback;

        public Microsoft.Xna.Framework.Rectangle boundstop = Microsoft.Xna.Framework.Rectangle.Empty;

        public Microsoft.Xna.Framework.Rectangle boundsback = Microsoft.Xna.Framework.Rectangle.Empty;

        public GraveChapel(Vector2 Position)
            : base()
        {

            position = Position;

            reset();

        }

        public override void reset()
        {

            Vector2 tile = ModUtility.PositionToTile(position);

            layer = (position.Y / 10000);

            bounds = new((int)position.X, (int)position.Y, 384, 384);

            boundstop = new((int)position.X, (int)position.Y - 320, 384, 640);

            boundsback = new((int)position.X, (int)position.Y - 320, 384, 384);

            baseTiles =  new()
            {
                new Vector2(tile.X,tile.Y),
                new Vector2(tile.X+1,tile.Y),
                new Vector2(tile.X+4,tile.Y),
                new Vector2(tile.X+5,tile.Y),

                new Vector2(tile.X,tile.Y+1),
                new Vector2(tile.X+5,tile.Y+1),

                new Vector2(tile.X,tile.Y+2),
                new Vector2(tile.X+5,tile.Y+2),

                new Vector2(tile.X,tile.Y+3),
                new Vector2(tile.X+5,tile.Y+3),

                new Vector2(tile.X,tile.Y+4),
                new Vector2(tile.X+5,tile.Y+4),

                new Vector2(tile.X,tile.Y+5),
                new Vector2(tile.X+1,tile.Y+5),
                new Vector2(tile.X+4,tile.Y+5),
                new Vector2(tile.X+5,tile.Y+5),

            };

            Vector2 corner00 = baseTiles.First() * 64;

            Vector2 corner11 = baseTiles.Last() * 64;

            center = corner00 + (corner11 - corner00) / 2;

            girth = Vector2.Distance(corner00, center);

            clearance = (int)Math.Ceiling(girth / 64);
        }

        public override void Fadeout(GameLocation location)
        {

            fade = 1f;

            fadetop = 1f;

            fadeback = 1f;

            foreach (Farmer character in location.farmers)
            {

                if (bounds.Contains(character.Position.X, character.Position.Y))
                {

                    fade = 0.35f;

                }

                if (boundstop.Contains(character.Position.X, character.Position.Y))
                {

                    fadetop = 0.35f;

                }

                if (boundsback.Contains(character.Position.X, character.Position.Y))
                {

                    fadeback = 0.35f;

                }

            }

        }

        public override void drawFront(SpriteBatch b, GameLocation location)
        {

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 512))
            {
                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.graveyard], 
                origin + new Vector2(192,-32), 
                new Rectangle(144,112,96,144), 
                Color.White * fadetop, 
                0f, 
                new(48,72), 
                4, 
                flip ? (SpriteEffects)1 : 0, 
                layer);

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 512))
            {
                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.graveyard],
                origin + new Vector2(192, 192),
                new Rectangle(0, 256, 96, 96),
                Color.White,
                0f,
                new(48, 48),
                4,
                flip ? (SpriteEffects)1 : 0,
                0.0001f);


            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.graveyard],
                origin + new Vector2(192, 192),
                new Rectangle(96, 256, 96, 96),
                Color.White * fade,
                0f,
                new(48, 48),
                4,
                flip ? (SpriteEffects)1 : 0,
                layer + 0.0352f);


            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.graveyard],
                origin + new Vector2(192, -128),
                new Rectangle(96, 256, 96, 96),
                Color.White * fadeback,
                0f,
                new(48, 48),
                4,
                flip ? (SpriteEffects)1 : 0,
                layer + 0.0032f);

        }

    }


}
