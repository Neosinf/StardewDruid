using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StardewDruid.Data.IconData;
using StardewValley.Monsters;
using StardewValley.Projectiles;
using static StardewDruid.Cast.SpellHandle;
using System.Diagnostics.Metrics;
using StardewValley.Mods;

namespace StardewDruid.Event
{

    public class EventRender
    {

        public string location;

        public Vector2 origin;

        public IconData.displays display;

        public Rectangle rectangle;

        public string eventId;

        public NPC npc;

        public enum renders
        {

            target,
            decoration,
            sky,
        }

        public renders render;

        public EventRender(string Id, string Location, Vector2 Origin, IconData.displays Display)
        {

            eventId = Id;

            location = Location;

            origin = Origin;

            display = Display;

            render = renders.target;

            rectangle = Mod.instance.iconData.DisplayRect(display);

        }

        public EventRender(string Id, string Location, Vector2 Origin, IconData.decorations Decoration)
        {

            eventId = Id;

            location = Location;

            origin = Origin;

            display = displays.none;

            render = renders.decoration;

            rectangle = Mod.instance.iconData.DecorativeRect(Decoration);

        }

        public EventRender(string Id, string Location, Vector2 Origin, IconData.skies sky)
        {

            eventId = Id;

            location = Location;

            origin = Origin;

            display = displays.none;

            render = renders.sky;

            rectangle = IconData.SkyRectangle(sky);

        }

        public void draw(SpriteBatch b)
        {

            if (!Utility.isOnScreen(origin, 64))
            {

                return;

            }

            switch (render)
            {

                case renders.decoration:

                    drawDecoration(b);

                    break;

                case renders.sky:

                    drawSky(b);

                    break;

                default:
                case renders.target:

                    drawTarget(b);

                    break;


            }

        }

        public void drawDecoration(SpriteBatch b)
        {

            int offset = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds) % 2400 / 20;

            Microsoft.Xna.Framework.Vector2 drawPosition = new(origin.X - (float)Game1.viewport.X, origin.Y - (float)Game1.viewport.Y);

            float rotate = (float)Math.PI / 60 * offset;

            b.Draw(
                Mod.instance.iconData.decorationTexture,
                drawPosition + new Vector2(32),
                rectangle,
                Color.White * 0.75f,
                rotate,
                new Vector2(32),
                3f,
                SpriteEffects.None,
                0.0001f
            );

        }
        public void drawSky(SpriteBatch b)
        {

            Microsoft.Xna.Framework.Vector2 drawPosition = new(origin.X - (float)Game1.viewport.X, origin.Y - (float)Game1.viewport.Y);

            b.Draw(
                Mod.instance.iconData.skyTexture,
                drawPosition + new Vector2(32),
                rectangle,
                Color.White * 0.75f,
                0,
                new Vector2(32),
                4f,
                SpriteEffects.None,
                0.0001f
            );

        }

        public void drawTarget(SpriteBatch b)
        {

            int offset = Math.Abs((int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 6400) / 100 - 32);

            Microsoft.Xna.Framework.Vector2 drawPosition = new(origin.X - (float)Game1.viewport.X, origin.Y - (float)Game1.viewport.Y);

            Vector2 position = drawPosition - new Vector2(16, 48 + offset);

            b.Draw(
                Mod.instance.iconData.displayTexture,
                position,
                rectangle,
                Color.White,
                0f,
                Vector2.Zero,
                4f,
                SpriteEffects.None,
                0.9f
            );

            //}
            /*
            b.Draw(
                Mod.instance.iconData.cursorTexture,
                position,
                targetRect,
                Color.White,
                0f,
                Vector2.Zero,
                2f,
                SpriteEffects.None,
                0.9f
            );
            */

        }


    }

}
