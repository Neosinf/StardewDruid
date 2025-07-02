using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Tiles;

namespace StardewDruid.Location.Terrain
{
    public class GraveLamp : TerrainField
    {

        public GraveLamp(Vector2 Position)
            : base()
        {

            tilesheet = IconData.tilesheets.graveyard;

            position = Position;

            reset();

        }

        public override void reset()
        {

            fadeout = 0.5f;

            source = new(208, 48, 16, 64);

            shadow = shadows.offset;

            layer = (position.Y + 224) / 10000;

            Vector2 tile = ModUtility.PositionToTile(position);

            baseTiles = new()
            {
                new Vector2(tile.X,tile.Y+3),
            };

            Vector2 corner00 = baseTiles.First() * 64;

            Vector2 corner11 = baseTiles.Last() * 64;

            center = corner00 + (corner11 - corner00) / 2;

            girth = Vector2.Distance(corner00, center);

            clearance = (int)Math.Ceiling(girth / 64);

        }

        public override void update(GameLocation location)
        {

            base.update(location);

            if (Game1.timeOfDay >= 1900)
            {

                source = new(224, 48, 16, 64);

                string id = "18465_lamp_" + (position.X * 10000 + position.Y).ToString();

                if (!Game1.currentLightSources.ContainsKey(id))
                {

                    LightSource light = new LightSource(id, 4, position, 3f, Color.Black * 0.75f);

                    Game1.currentLightSources.Add(id, light);

                }

            }
            else
            {

                source = new(208, 48, 16, 64);

            }

        }


        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!Utility.isOnScreen(position, 512))
            {
                return;

            }
            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.graveyard], 
                origin, 
                useSource, 
                Color.White * fade, 
                0f, 
                Vector2.Zero, 
                4, 
                flip ? (SpriteEffects)1 : 0, 
                layer
                );

            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.graveyard],
                origin + new Vector2(1, 6),
                useSource,
                Color.Black * shade * fade,
                0f,
                Vector2.Zero,
                4,
                flip ? (SpriteEffects)1 : 0,
                layer - 0.001f);

        }

    }


}
