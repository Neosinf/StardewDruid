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
using System.Reflection.Metadata;
using xTile.Tiles;

namespace StardewDruid.Location.Terrain
{

    public class MoorsMonument : TerrainField
    {

        public List<Vessel> vessels = new();

        public MoorsMonument(Vector2 Position, int Index)
           : base()
        {

            tilesheet = StardewDruid.Data.IconData.tilesheets.moors;

            index = Index;

            position = Position;

            color = Color.White;

            shadow = shadows.deepset;

            flip = false;

            shade = 0.3f;

            fadeout = 0.75f;

            reset();

        }

        public virtual Rectangle Source()
        {

            switch (index)
            {
                default:
                case 1:

                    vessels.Clear();

                    vessels.Add(new(Vessel.vesselFrames.witches, position + new Vector2(192, 0)));

                    return new(0, 240, 96, 96);

                case 2:

                    vessels.Clear();

                    vessels.Add(new(Vessel.vesselFrames.dragon, position + new Vector2(192, 32)));

                    return new(0, 0, 96, 240);

                case 3:

                    vessels.Clear();

                    vessels.Add(new(Vessel.vesselFrames.soldier, position + new Vector2(256, -32)));

                    return new(96, 128, 128, 112);

                case 4:

                    return new(96, 0, 64, 64);

                case 5:

                    return new(160, 0, 64, 64);

                case 6:

                    return new(96, 64, 32, 64);

                case 7:

                    return new(128, 64, 32, 64);

                case 8:

                    return new(160, 64, 32, 64);

                case 9:

                    return new(192, 64, 32, 64);

            }


        }

        public virtual int Footprint()
        {

            switch (index)
            {

                case 1:

                    Vector2 tile = ModUtility.PositionToTile(position);

                    for (int x = 1; x < 5; x++)
                    {

                        baseTiles.Add(new Vector2(tile.X + x, tile.Y + 2));

                    }

                    for (int x = 0; x < 6; x++)
                    {

                        baseTiles.Add(new Vector2(tile.X + x, tile.Y + 3));

                    }

                    for (int x = 0; x < 6; x++)
                    {

                        baseTiles.Add(new Vector2(tile.X + x, tile.Y + 4));

                    }

                    for (int x = 1; x < 5; x++)
                    {

                        baseTiles.Add(new Vector2(tile.X + x, tile.Y + 5));

                    }

                    return 0;

                case 2:
                case 3:

                    return 4;

                case 4:
                case 5:

                    return 2;

            }

            return 1;

        }

        public override void reset()
        {

            Vector2 tile = ModUtility.PositionToTile(position);

            source = Source();

            layer = (position.Y - 48 + (source.Height * 4)) / 10000;

            bounds = new((int)position.X, (int)position.Y, source.Width * 4, source.Height * 4);

            baseTiles.Clear();

            int footPrint = Footprint();

            Vector2 range = new Vector2(source.Width / 16, source.Height / 16);

            for (int y = 1; y <= footPrint; y++)
            {

                for (int x = 0; x < (int)range.X; x++)
                {

                    baseTiles.Add(new Vector2(tile.X + x, tile.Y + ((int)range.Y - y)));

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

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, 8),useSource, Color.Black * shade * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            foreach (Vessel vessel in vessels)
            {

                vessel.draw(b, location, layer, fade);

            }

        }

    }

}
