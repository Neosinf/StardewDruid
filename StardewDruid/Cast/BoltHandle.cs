using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Cast
{
    public class BoltHandle : MissileHandle
    {

        public Texture2D boltTexture;

        public BoltHandle() 
        { 
        
        
        
        }

        public override void Setup(
            GameLocation Location,
            Vector2 Origin,
            Vector2 Destination,
            missiles Missile,
            IconData.schemes Scheme = IconData.schemes.none,
            int Factor = 2)
        {

            location = Location;

            origin = Origin;

            destination = Destination;

            missile = Missile;

            scheme = Scheme;

            factor = Factor;

            boltTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Bolt.png"));

            switch (Missile)
            {

                case missiles.bolt:

                    ConstructBolt();

                    break;

                /*case missiles.zap:

                    ConstructZap();

                    break;*/

            }

        }

        public override void Update()
        {

            counter++;

            if(counter == 60)
            {

                Shutdown();

                return;

            }
            
            for (int i = 0; i < Math.Min(4, construct.Count); i++)
            {

                int factor = (10 - counter);

                construct[i].alpha = 1 - Math.Abs(factor) * 0.025f;

            }


        }

        public void ConstructBolt()
        {

            if (scheme == IconData.schemes.none)
            {

                scheme = IconData.schemes.mists;

            }

            List<TemporaryAnimatedSprite> animations = new();

            float boltScale = 2.5f + 0.5f * factor;

            int viewY = Game1.viewport.Y;

            int offset = 0;

            Vector2 originOffset = new((int)destination.X + 32 - (int)(24 * boltScale), (int)destination.Y + 48 - (int)(400 * boltScale));

            if ((int)originOffset.Y < viewY && (int)destination.Y - viewY >= 200)
            {

                offset = (int)((viewY - (int)originOffset.Y) / boltScale);

                originOffset.Y = viewY;

            }

            int randSet1 = 48 * Mod.instance.randomIndex.Next(2);

            bool flippit = Mod.instance.randomIndex.NextBool();

            TemporaryAnimatedSprite bolt1 = new(0, 500, 1, 1, originOffset, false, flippit)
            {

                sourceRect = new(randSet1, offset, 48, 400 - offset),

                sourceRectStartingPos = new Vector2(randSet1, offset),

                texture = boltTexture,

                layerDepth = 801f,

                scale = boltScale,

            };

            location.temporarySprites.Add(bolt1);

            animations.Add(bolt1);

            // ------------------------- flash

            TemporaryAnimatedSprite bolt2 = new(0, 50, 1, 1, originOffset, false, flippit)
            {

                sourceRect = new(randSet1+96, offset, 48, 400 - offset),

                sourceRectStartingPos = new Vector2(randSet1 + 96, offset),

                texture = boltTexture,

                layerDepth = 802f,

                scale = boltScale,

            };

            location.temporarySprites.Add(bolt2);

            //animations.Add(bolt2);

            // ---------------------- clouds

            if (factor <= 1)
            {

                construct = animations;

                return;

            }

            int frame = Mod.instance.randomIndex.Next(4) * 2;

            Mod.instance.iconData.CreateImpact(
            location,
            originOffset - new Vector2(0, 64),
            IconData.impacts.clouds,
            4f,
            new()
            {
                girth = 2,
                layer = 802f,
                interval = 500,
                frame = frame,
                flip = Mod.instance.randomIndex.NextBool(),
                frames = 1,
                alpha = 1f,
            });

            for (int i = 0; i < ((400 - offset) * boltScale); i += 192)
            {

                Vector2 lightSource = new(destination.X, originOffset.Y + 128 + i);

                string lightid = "18465_" + (lightSource.X * 10000 + lightSource.Y).ToString();

                TemporaryAnimatedSprite lightCircle = new(23, 200f, 6, 1, lightSource, false, Game1.random.NextDouble() < 0.5)
                {
                    texture = Game1.mouseCursors,
                    lightId = lightid,
                    lightRadius = 2f,
                    lightcolor = Microsoft.Xna.Framework.Color.Black,
                    alphaFade = 0.03f,
                    Parent = location,
                };

                location.temporarySprites.Add(lightCircle);

                animations.Add(lightCircle);

            }

            construct = animations;

        }

        /*public void ConstructZap()
        {

            if (scheme == IconData.schemes.none)
            {

                scheme = IconData.schemes.mists;

            }

            List<TemporaryAnimatedSprite> animations = new();

            float distance = Math.Min(800f,Vector2.Distance(origin, destination));

            Vector2 diff = destination - origin;

            Vector2 point = diff / distance;

            Vector2 middle = origin + (diff / 2);

            //Vector2 end = origin + (point * 144 * boltScale);

            float rotate = (float)Math.Atan2(diff.Y, diff.X) - (float)(Math.PI / 2);

            Vector2 originOffset = new(middle.X - (48f), middle.Y - (distance / 2));

            Microsoft.Xna.Framework.Rectangle sourceRect2 = new(0 + Mod.instance.randomIndex.Next(2)*48, 400 - ((int)distance / 2), 48, (int)distance / 2);

            bool flippit = Mod.instance.randomIndex.NextBool();

            TemporaryAnimatedSprite bolt1 = new(0, 500, 1, 1, originOffset, false, flippit)
            {

                sourceRect = sourceRect2,

                sourceRectStartingPos = new Vector2(sourceRect2.X, sourceRect2.Y),

                texture = boltTexture,

                layerDepth = 801f,

                scale = 2f,

                rotation = rotate,

            };

            location.temporarySprites.Add(bolt1);

            animations.Add(bolt1);

            sourceRect2.X += 96;

            TemporaryAnimatedSprite bolt2 = new(0, 50, 1, 1, originOffset, false, flippit)
            {

                sourceRect = sourceRect2,

                sourceRectStartingPos = new Vector2(sourceRect2.X, sourceRect2.Y),

                texture = boltTexture,

                layerDepth = 802f,

                scale = 2f,

                rotation = rotate,

            };

            location.temporarySprites.Add(bolt2);

            animations.Add(bolt2);

            // Light
            string lightid = "18465_" + (destination.X * 10000 + destination.Y).ToString();

            TemporaryAnimatedSprite lightCircle = new(23, 200f, 6, 1, destination, false, Game1.random.NextDouble() < 0.5)
            {
                texture = Game1.mouseCursors,
                lightId = lightid,
                lightRadius = 3f,
                lightcolor = Microsoft.Xna.Framework.Color.Black,
                alphaFade = 0.03f,
                Parent = location,
            };

            location.temporarySprites.Add(lightCircle);

            animations.Add(lightCircle);

            construct = animations;

        }*/


    }


}
