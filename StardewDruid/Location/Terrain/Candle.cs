using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location.Terrain
{
    public class Candle 
    { 

        public enum candleFrames
        {
            riteCandle,
            riteCandle2,
            riteCandle3,
            graveCandle,
            graveCandle2,
            graveCandle3,
            graveCandle4,
            grime,

        }

        public candleFrames index;

        public Rectangle source;

        public Rectangle grime;

        public Vector2 position;

        public bool disabled;

        public bool ruined;

        public int status;

        public bool flip;

        public string lightId;

        public int where;

        public int flicker;

        public Candle(candleFrames Vessel, Vector2 Position, bool Flip = false)
        {

            index = Vessel;

            position = Position;

            reset();

        }

        public void reset()
        {

            switch (index)
            {

                case candleFrames.riteCandle:
                case candleFrames.riteCandle2:
                case candleFrames.riteCandle3:
                case candleFrames.graveCandle:
                case candleFrames.graveCandle2:
                case candleFrames.graveCandle3:
                case candleFrames.graveCandle4:

                    source = new(0, (int)index * 16, 16, 16);  
                    
                    break;

            }

            grime = new(((int)index % 4) * 16, (int)candleFrames.grime * 16, 16, 16);

            lightId = "18465_candle_" + (position.X * 10000 + position.Y).ToString();

            where = (int)ModUtility.PositionToTile(position).X;

        }

        public void update(GameLocation location)
        {

            if (disabled || ruined)
            {

                return;

            }

            switch (status)
            {
                
                case 0: // dead

                    RemoveLight();

                    return;

                case 2: // on timer

                    if (Game1.timeOfDay < 1700)
                    {

                        RemoveLight();

                        return;

                    }

                    break;

            }

            if (!Game1.currentLightSources.ContainsKey(lightId))
            {

                LightSource light = new LightSource(lightId, 4, position, 0.5f, Color.Black * 0.75f);

                Game1.currentLightSources.Add(lightId, light);

            }

            float wind = Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.candleWind);

            flicker = (int)wind * 16;

        }

        public void RemoveLight()
        {

            if (Game1.currentLightSources.ContainsKey(lightId))
            {

                Game1.currentLightSources.Remove(lightId);

                flicker = 0;

            }

        }

        public void draw(SpriteBatch b, GameLocation location, float layer, float fade = 1f)
        {

            if (disabled)
            {

                return;

            }

            if (!Utility.isOnScreen(position, 128))
            {
                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource;

            if (ruined)
            {

                useSource = new(grime.X, grime.Y, grime.Width, grime.Height);

            }
            else
            {
                useSource = new(source.X + flicker, source.Y, source.Width, source.Height);

            }

            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.candles],
                origin + new Vector2(0, 8),
                useSource,
                Color.Black * 0.3f * fade,
                0f,
                new Vector2(8),
                4,
                flip ? (SpriteEffects)1 : 0,
                layer + 0.001f
                );

            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.candles], 
                origin, 
                useSource, 
                Color.White * fade, 
                0f,
                new Vector2(8), 
                4, 
                flip ? (SpriteEffects)1 : 0,
                layer + 0.002f
                );

        }

    }


}
