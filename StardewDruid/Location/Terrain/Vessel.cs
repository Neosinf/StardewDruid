using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location.Terrain
{
    public class Vessel
    {

        public enum vesselFrames
        {
            soldier,
            witches,
            dragon
        }

        public vesselFrames index;

        public Rectangle source;

        public Vector2 position;

        public bool disabled;

        public bool flip;

        public float shadowScale;

        public float shadowOffset;

        public float shadowStrength;

        public Vessel(vesselFrames Vessel, Vector2 Position, bool Flip = false)
        {

            index = Vessel;

            position = Position;

            flip = Flip;

            reset();

        }

        public void reset()
        {

            shadowOffset = 32f;

            shadowScale = 6f;

            shadowStrength = 0.15f;

            switch (index)
            {

                case vesselFrames.soldier:


                    source = new(0,0, 48, 80);

                    break;

                case vesselFrames.witches:

                    source = new(48,0, 64, 64);

                    shadowScale = 8f;

                    break;

                case vesselFrames.dragon:

                    source = new(0, 80, 64, 80);

                    shadowScale = 7f;

                    shadowOffset = 96f;

                    shadowStrength = 0.2f;

                    break;
            }

        }

        public void draw(SpriteBatch b, GameLocation location, float layer, float fade)
        {

            if (disabled)
            {

                return;

            }

            if (!Utility.isOnScreen(position, 256))
            {
                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            b.Draw(
                Mod.instance.iconData.shadowTexture,
                origin + new Vector2(2, (source.Height*2) - shadowOffset),
                Mod.instance.iconData.shadowRectangle,
                Color.Black * shadowStrength * fade,
                0f,
                new Vector2(24),
                shadowScale,
                flip ? (SpriteEffects)1 : 0,
                layer + 0.0001f
                );

            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.vessels], 
                origin, 
                source, 
                Color.White * fade, 
                0f, 
                new Vector2(source.Width/2,source.Height/2), 
                4, 
                flip ? (SpriteEffects)1 : 0, 
                layer + 0.0002f
                );

        }

    }


}
