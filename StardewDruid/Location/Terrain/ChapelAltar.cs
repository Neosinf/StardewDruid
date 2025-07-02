using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewDruid.Event;
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

    public class ChapelAltar : TerrainField
    {

        public int altarStatus = 0;

        public ChapelAltar(IconData.tilesheets Tilesheet, int Index, Vector2 Position, shadows Shadow = shadows.offset, bool Flip = false)
            : base(Tilesheet, Index, Position, Shadow, Flip)
        {

        }

        public override void update(GameLocation location)
        {

            switch (altarStatus)
            {

                default:
                case 0:

                    disabled = false; 
                    
                    base.update(location);

                    return;

                case 1:

                    disabled = true;

                    if (Mod.instance.activeEvent.Count == 0)
                    {

                        altarStatus = 0;

                    }

                    return;

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

            DrawShadow(b,origin,useSource,fade, shadow);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, 0.0064f);

        }

    }

}
