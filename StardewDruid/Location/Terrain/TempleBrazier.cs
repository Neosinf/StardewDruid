using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location.Terrain
{
    public class TempleBrazier : TerrainField
    {

        public bool alight = false;

        public int brazierFrame = 0;

        public int brazierStatus = 0;

        public int lightFrame;

        public TempleBrazier(Vector2 Position)
        : base()
        {

            tilesheet = IconData.tilesheets.temple;

            index = 0;

            position = Position;

            color = Color.White;

            shadow = shadows.offset;

            flip = false;

            lightFrame = Mod.instance.randomIndex.Next(5);

            reset();

        }

        public override void reset()
        {

            source = new(0, 0, 96, 96);

            Vector2 tile = ModUtility.PositionToTile(position);

            baseTiles.Clear();

            baseTiles = new()
            {
                new Vector2(tile.X + 1, tile.Y + 2),
                new Vector2(tile.X + 2, tile.Y + 2),
                new Vector2(tile.X + 3, tile.Y + 2),
                new Vector2(tile.X + 4, tile.Y + 2),

                new Vector2(tile.X + 1, tile.Y + 3),
                new Vector2(tile.X + 2, tile.Y + 3),
                new Vector2(tile.X + 3, tile.Y + 3),
                new Vector2(tile.X + 4, tile.Y + 3),

                new Vector2(tile.X + 1, tile.Y + 4),
                new Vector2(tile.X + 2, tile.Y + 4),
                new Vector2(tile.X + 3, tile.Y + 4),
                new Vector2(tile.X + 4, tile.Y + 4),

                new Vector2(tile.X + 1, tile.Y + 5),
                new Vector2(tile.X + 2, tile.Y + 5),
                new Vector2(tile.X + 3, tile.Y + 5),
                new Vector2(tile.X + 4, tile.Y + 5),

            };

            Vector2 range = new Vector2(source.Width / 16, source.Height / 16);

            center = position + new Vector2(128,192);

            girth = Vector2.Distance(position, center);

            clearance = (int)Math.Ceiling(girth / 64);

            backing = 72;

            layer = (position.Y - 32 + (source.Height * 4)) / 10000;

            bounds = new((int)position.X + 8, (int)position.Y, 256 - 16, 384 - backing);

        }

        public override void update(GameLocation location)
        {

            brazierFrame = brazierStatus % 4;

            alight = false;

            if (Mod.instance.activeEvent.Count == 0)
            {

                base.update(location);

                brazierStatus = 0;

                brazierFrame = 0;

                if (Game1.timeOfDay >= 1900 && Mod.instance.questHandle.IsComplete(QuestHandle.challengeEther))
                {

                    brazierFrame = 1;

                    alight = true;

                    CheckLight();


                }

            }
            else
            if (brazierStatus >= 4)
            {

                alight = true;

                CheckLight();

            }

        }

        public void CheckLight()
        {

            string id = "18465_" + (position.X * 10000 + position.Y).ToString();

            if (Game1.currentLightSources.ContainsKey(id))
            {

                return;

            }

            LightSource light = new LightSource(id, 1, position + new Vector2(160 + 64), 3f, Color.Black * 0.75f);

            Game1.currentLightSources.Add(id, light);

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            source = new(0, 0, 96, 96);

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            DrawShadow(b, origin, useSource, 1f, shadow);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            if(brazierFrame > 0)
            {

                Microsoft.Xna.Framework.Vector2 coalVector = origin + new Vector2(96, 32);

                Microsoft.Xna.Framework.Rectangle coalSource = new(48 + (brazierFrame * 48),0,48,48);

                b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], coalVector, coalSource, color, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            }

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
                origin + new Vector2(160, 72),
                new Rectangle(32 + frame * 32, 0, 32, 32),
                Color.White * 0.75f,
                0f,
                new(16),
                5,
                0,
                layer + 0.0001f
                );


        }

    }


}
