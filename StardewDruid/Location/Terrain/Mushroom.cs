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
    public class Mushroom : TerrainField
    {

        public enum MushroomIndex
        {

            none,
            cluster,

        }

        public Mushroom(Vector2 Position, MushroomIndex mushroom)
            : base()
        {

            tilesheet = IconData.tilesheets.mushrooms;

            index = (int)mushroom;

            position = Position;

            color = Color.White;

            shadow = TerrainField.shadows.offset;

            reset();

        }


        public override void reset()
        {

            switch (index)
            {
                case 1:

                    source = new Rectangle(0, 0, 32, 48);

                    bases();

                    break;

                case 2:

                    source = new Rectangle(32, 0, 32, 48);

                    bases();

                    break;

                case 3:

                    source = new Rectangle(64, 0, 32, 48);

                    bases();

                    break;

                case 4:

                    source = new Rectangle(96, 0, 32, 48);

                    bases();

                    break;
            }

            bounds = new((int)position.X + 8, (int)position.Y, source.Width * 4 - 16, source.Height * 4 - backing);

            layer = (position.Y + 1 + (source.Height * 4)) / 10000;

        }

        public virtual void bases()
        {

            baseTiles.Clear();

            Vector2 tile = ModUtility.PositionToTile(position);

            Vector2 range = new Vector2(source.Width / 16, source.Height / 16);

            for (int y = 1; y <= 2; y++)
            {

                for (int x = 0; x < (int)range.X; x++)
                {
                    
                    baseTiles.Add(new Vector2(tile.X + x, tile.Y + ((int)range.Y - y)));
                
                }
            
            }
        
        }

        public override void update(GameLocation location)
        {

            if (disabled)
            {

                return;

            }

            string id = "18465_mushroom_" + (position.X * 10000 + position.Y).ToString();

            if (!Game1.currentLightSources.ContainsKey(id))
            {

                LightSource light = new LightSource(id, 4, position + new Vector2(64, 64), 0.4f, Color.Black * 0.5f);

                Game1.currentLightSources.Add(id, light);

            }

        }
        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            switch (index)
            {
                case 1:
                case 2:
                case 3:
                case 4:

                    Vector2 shadowOrigin = new(origin.X + (flip ? 8 : -8), origin.Y + 4);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], shadowOrigin, source, Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    break;
            }

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

    }


}
