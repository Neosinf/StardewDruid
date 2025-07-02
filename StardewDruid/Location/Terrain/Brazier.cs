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
    public class Brazier : TerrainField
    {

        public bool alight = false;

        public int brazierStatus = 0;

        public int lightFrame;

        public Vector2 lightPosition;

        public Brazier(IconData.tilesheets Tilesheet, int Index, Vector2 Position, shadows Shadow = shadows.offset, bool Flip = false)
            : base(Tilesheet, Index, Position,Shadow,Flip)
        {

            lightFrame = Mod.instance.randomIndex.Next(5);

            lightPosition = new Vector2(64, -16);

        }

        public override void update(GameLocation location)
        {

            base.update(location);

            alight = true;

            if (brazierStatus >= 1)
            {

                alight = false;

                return;

            }

            string id = "18465_" + (position.X * 10000 + position.Y).ToString();

            if (Game1.currentLightSources.ContainsKey(id))
            {

                return;

            }

            LightSource light = new LightSource(id, 4, position + lightPosition, 1.5f, Color.Black * 0.75f);

            Game1.currentLightSources.Add(id, light);

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            DrawShadow(b, origin, useSource, 1f, shadow);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            if (alight)
            {

                DrawFlame(b, origin);

            }

        }

        public virtual void DrawFlame(SpriteBatch b, Vector2 origin)
        {

            int brazierTime = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 1000) / 250;

            int frame = (brazierTime + lightFrame) % 5;

            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.tomb],
                origin + lightPosition,
                new Rectangle(32 + frame * 32, 0, 32, 32),
                Color.White * 0.75f * fade,
                0f,
                new(16),
                5,
                0,
                layer + 0.0001f
                );


        }

    }


}
