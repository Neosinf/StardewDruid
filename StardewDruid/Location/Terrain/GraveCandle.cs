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
    public class GraveCandle : TerrainField
    {

        public GraveCandle(IconData.tilesheets Tilesheet, int Index, Vector2 Position, shadows Shadow = shadows.offset, bool Flip = false)
            : base(Tilesheet, Index,Position,Shadow,Flip)
        {


        }

        public override void reset()
        {

            int useIndex = index;

            if(useIndex > 200)
            {

                useIndex -= 200;

            }

            switch (useIndex)
            {

                case 35:

                    source = new(0, 352, 16, 32);  break;

                case 36:

                    source = new(16, 352, 16, 32); break;

                case 37:

                    source = new(32, 352, 16, 32); break;

                case 38:

                    source = new(48, 352, 16, 32); break;


            }

            layer = (position.Y + 48f) / 10000;

            if (index > 200)
            {

                layer += 0.064f;

            }

        }

        public Microsoft.Xna.Framework.Rectangle Restored()
        {

            int useIndex = index;

            if (useIndex > 200)
            {

                useIndex -= 200;

            }

            switch (useIndex)
            {

                case 35:
                case 36:
                case 37:
                case 38:

                    string id = "18465_candle_" + (position.X * 10000 + position.Y).ToString();

                    if (!Game1.currentLightSources.ContainsKey(id))
                    {

                        LightSource light = new LightSource(id, 4, position, 0.35f, Color.Black * 0.75f);

                        Game1.currentLightSources.Add(id, light);

                    }

                    break;

            }

            switch (useIndex)
            {
                default:
                case 35:

                    return new(64, 352, 16, 32);

                case 36:

                    return new(80, 352, 16, 32);

                case 37:

                    return new(96, 352, 16, 32);

                case 38:

                    return new(112, 352, 16, 32);

            }


        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!Utility.isOnScreen(position, 128))
            {
                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            if (!ruined)
            {

                useSource = Restored();

            }

            DrawShadow(b, origin, useSource, 1f);

            b.Draw(
                Mod.instance.iconData.sheetTextures[tilesheet], 
                origin, 
                useSource, 
                Color.White, 
                0f, 
                Vector2.Zero, 
                4, 
                flip ? (SpriteEffects)1 : 0, 
                layer
                );

        }

    }


}
