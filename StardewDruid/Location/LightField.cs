using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location
{

    public class LightField
    {

        public Vector2 origin;

        public int luminosity = 4;

        public Color colour = Color.LightGoldenrodYellow;

        public enum lightTypes
        {
            sconceLight,
            tableLight,
            lantern,
            brazier,
            brazierDark,

        }

        public lightTypes lightType;

        public int lightFrame;

        public float lightAmbience = 0.75f;

        public int lightLayer;

        public int lightTimer = -1;

        public float lightScale = 4f;

        public LightField(Vector2 Origin)
        {

            origin = Origin;

        }

        public LightField(Vector2 Origin, int Luminosity, Color Colour)
        {

            origin = Origin;

            luminosity = Luminosity;

            colour = Colour;

        }

        public void draw(SpriteBatch b)
        {

            string id = "18465_" + (origin.X * 10000 + origin.Y).ToString();

            bool active = Game1.currentLightSources.ContainsKey(id);

            if (lightTimer != -1)
            {

                if (Game1.timeOfDay < lightTimer)
                {

                    if (active)
                    {

                        Game1.currentLightSources.Remove(id);

                    }

                    return;

                }

            }

            if (Utility.isOnScreen(origin, 64 * luminosity))
            {

                int type = 4;

                switch (lightType)
                {
                    case lightTypes.brazierDark:
                    case lightTypes.brazier:

                        int brazierTime = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 1000) / 250;

                        int frame = (brazierTime + lightFrame) % 5;

                        Vector2 position = new(origin.X - Game1.viewport.X, origin.Y - Game1.viewport.Y);

                        IconData.tilesheets useSheet = lightType == lightTypes.brazier ? IconData.tilesheets.tomb : IconData.tilesheets.lair;

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[useSheet],
                            position,
                            new Rectangle(32 + frame * 32, 0, 32, 32),
                            Color.White * 0.75f,
                            0f,
                            new(16),
                            lightScale,
                            0,
                            (origin.Y + 384 + lightLayer) / 10000
                            );

                        position += new Vector2(64);

                        break;

                    case lightTypes.lantern:

                        type = 1;

                        break;

                }

                if (active)
                {

                    return;

                }

                LightSource light = new LightSource(id, type, origin, 0.35f * luminosity, Color.Black * lightAmbience);

                Game1.currentLightSources.Add(id, light);

            }

        }

    }
}
