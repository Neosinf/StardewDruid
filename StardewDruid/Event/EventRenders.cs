using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;


namespace StardewDruid.Event
{

    public class EventRender
    {

        public string location;

        public Vector2 origin;

        public IconData.displays display;

        public Rectangle rectangle;

        public string eventId;

        public float scale;

        public Microsoft.Xna.Framework.Color colour = Microsoft.Xna.Framework.Color.White;

        public float layer;

        public enum renders
        {

            target,
            decoration,
            sky,
            circle,
            relic,

        }

        public renders render;

        public EventRender(string Id, string Location, Vector2 Origin, IconData.displays Display)
        {

            eventId = Id;

            location = Location;

            origin = Origin;

            display = Display;

            render = renders.target;

            layer = 0.9f;

            rectangle = IconData.DisplayRectangle(display);

        }

        public EventRender(string Id, string Location, Vector2 Origin, IconData.decorations Decoration)
        {

            eventId = Id;

            location = Location;

            origin = Origin;

            display = StardewDruid.Data.IconData.displays.none;

            render = renders.decoration;

            layer = 0.0001f;

            rectangle = IconData.DecorativeRectangle(Decoration);

        }

        public EventRender(string Id, string Location, Vector2 Origin, IconData.skies sky, float Scale = 4f)
        {

            eventId = Id;

            location = Location;

            origin = Origin;

            display = StardewDruid.Data.IconData.displays.none;

            render = renders.sky;

            layer = 0.0001f;

            rectangle = IconData.SkyRectangle(sky);

            scale = Scale;

        }

        public EventRender(string Id, string Location, Vector2 Origin, IconData.circles circle, Microsoft.Xna.Framework.Color Colour)
        {

            eventId = Id;

            location = Location;

            origin = Origin;

            display = StardewDruid.Data.IconData.displays.none;

            render = renders.circle;

            layer = 0.0001f;

            rectangle = IconData.CircleRectangle(circle);

            colour = Colour; 

        }        
        
        public EventRender(string Id, string Location, Vector2 Origin, IconData.relics relic)
        {

            eventId = Id;

            location = Location;

            origin = Origin;

            display = StardewDruid.Data.IconData.displays.none;

            render = renders.relic;

            scale = 4f;

            layer = Origin.Y / 10000 - 0.0001f;

            rectangle = IconData.RelicRectangles(relic);

        }

        public void draw(SpriteBatch b)
        {

            if (!Utility.isOnScreen(origin, 64) || location != Game1.player.currentLocation.Name)
            {

                return;

            }

            switch (render)
            {

                case renders.relic:

                    drawRelic(b);

                    break;

                case renders.decoration:

                    drawDecoration(b);

                    break;

                case renders.sky:

                    drawSky(b);

                    break;
                case renders.circle:

                    drawCircle(b);

                    break;
                default:
                case renders.target:

                    drawTarget(b);

                    break;


            }

        }

        public void drawRelic(SpriteBatch b)
        {

            Microsoft.Xna.Framework.Vector2 drawPosition = new(origin.X - (float)Game1.viewport.X, origin.Y - (float)Game1.viewport.Y);

            b.Draw(
                Mod.instance.iconData.relicsTexture,
                drawPosition + new Vector2(32),
                rectangle,
                Color.White,
                0,
                new Vector2(10),
                scale,
                SpriteEffects.None,
                layer
            );

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
                layer
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
                layer
            );

        }        
        
        public void drawCircle(SpriteBatch b)
        {

            Microsoft.Xna.Framework.Vector2 drawPosition = new(origin.X - (float)Game1.viewport.X, origin.Y - (float)Game1.viewport.Y);

            float circleSeconds = (float)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 5000);

            Microsoft.Xna.Framework.Color drawColour = colour * (0.25f + Math.Abs((float)(circleSeconds == 0 ? 0 : circleSeconds / 5000f) - 0.5f));

            b.Draw(
                Mod.instance.iconData.circleTexture,
                drawPosition + new Vector2(32),
                rectangle,
                drawColour,
                0,
                new Vector2(72),
                4f,
                SpriteEffects.None,
                layer
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
                layer
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
