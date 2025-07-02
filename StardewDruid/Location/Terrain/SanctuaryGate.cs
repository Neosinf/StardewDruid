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
    public class SanctuaryGate : TerrainField
    {

        public bool opened;

        public bool doorOpen;

        public int doorStatus;

        public SanctuaryGate(Vector2 Position)
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

            source = new(0, 0, 160, 128);

            baseTiles.Clear();

            Vector2 tile = ModUtility.PositionToTile(position);

            Vector2 range = new Vector2(source.Width / 16, source.Height / 16);

            for (int y = 1; y <= 4; y++)
            {

                for (int x = 0; x < (int)range.X; x++)
                {

                    baseTiles.Add(new Vector2(tile.X + x, tile.Y + ((int)range.Y - y)));

                }

            }

            if (baseTiles.Count > 0)
            {

                Vector2 corner00 = baseTiles.First() * 64;

                Vector2 corner11 = baseTiles.Last() * 64;

                center = corner00 + (corner11 - corner00) / 2;

                girth = Vector2.Distance(corner00, center);

                clearance = (int)Math.Ceiling(girth / 64);

                backing = 72;

            }

            bounds = new((int)position.X + 8, (int)position.Y, source.Width * 4 - 16, source.Height * 4 - backing);

            layer = (position.Y - 32 + (source.Height * 4)) / 10000;

        }

        public override void update(GameLocation location)
        {

            doorOpen = opened;

            switch (doorStatus)
            {

                case 1:

                    if (Mod.instance.activeEvent.Count == 0)
                    {
                        
                        doorStatus = 0; break;
                    
                    }

                    doorOpen = true;

                    break;

                case 2:

                    if (Mod.instance.activeEvent.Count == 0)
                    {

                        doorStatus = 0; break;

                    }

                    doorOpen = false;

                    break;


            }

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            if (!doorOpen)
            {

                b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(192,256), new(160, 0, 64, 64), color, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer + 0.0001f);

            }

            DrawShadow(b, origin, useSource, 1f, shadow);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

        }

    }


}
