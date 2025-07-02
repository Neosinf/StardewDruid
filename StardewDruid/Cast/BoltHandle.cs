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

            switch (Missile)
            {

                default:
                case missiles.bolt:

                    ConstructBolt();

                    break;

                case missiles.quickbolt:

                    ConstructQuickBolt();

                    break;

                case missiles.greatbolt:

                    ConstructLightning();

                    break;

                case missiles.judgement:

                    ConstructJudgement();

                    break;
            }

        }

        public override void Update()
        {

            counter++;

            switch (missile)
            {

                case missiles.bolt:

                    if (counter == 60)
                    {

                        Shutdown();

                    }
                    break;

                case missiles.quickbolt:

                    if (counter == 30)
                    {

                        Shutdown();

                    }
                    break;

                case missiles.greatbolt:
                case missiles.judgement:

                    if (counter == 70)
                    {

                        Shutdown();

                    }

                    break;

            }

        }

        public void ConstructBolt()
        {

            List<TemporaryAnimatedSprite> animations = new();

            float boltScale = 2.5f + 0.5f * factor;

            int viewY = Game1.viewport.Y;

            Vector2 originOffset = new((int)destination.X + 32 - (int)(32 * boltScale), (int)destination.Y + 48 - (int)(192 * boltScale));

            Vector2 cloudOffset = originOffset - new Vector2(0,(int)(32 * boltScale));

            bool flippit = Mod.instance.randomIndex.NextBool();

            TemporaryAnimatedSprite cloud = new(0, 60, 10, 1, cloudOffset, false, flippit)
            {

                sourceRect = new(0, 0, 64, 64),

                sourceRectStartingPos = new Vector2(0),

                texture = Mod.instance.iconData.boltTexture,

                layerDepth = 802f,

                scale = boltScale,

            };

            location.temporarySprites.Add(cloud);

            animations.Add(cloud);

            TemporaryAnimatedSprite bolt = new(0, 60, 10, 1, originOffset, false, flippit)
            {

                sourceRect = new(0, 64, 64, 192),

                sourceRectStartingPos = new Vector2(0,64),

                texture = Mod.instance.iconData.boltTexture,

                layerDepth = 801f,

                scale = boltScale,

            };

            location.temporarySprites.Add(bolt);

            animations.Add(bolt);

            construct = animations;

            origin = new((int)destination.X + 32, (int)destination.Y + 48 - (int)(192 * boltScale));

        }

        public void ConstructQuickBolt()
        {

            List<TemporaryAnimatedSprite> animations = new();

            float boltScale = 1.5f + 0.5f * factor;

            int viewY = Game1.viewport.Y;

            Vector2 originOffset = new((int)destination.X + 32 - (int)(32 * boltScale), (int)destination.Y + 64 - (int)(192 * boltScale));

            bool flippit = Mod.instance.randomIndex.NextBool();

            TemporaryAnimatedSprite bolt1 = new(0, 40, 8, 1, originOffset, false, flippit)
            {

                sourceRect = new(128, 64, 64, 192),

                sourceRectStartingPos = new Vector2(128,64),

                texture = Mod.instance.iconData.boltTexture,

                layerDepth = 801f,

                scale = boltScale,

            };

            location.temporarySprites.Add(bolt1);

            animations.Add(bolt1);

            construct = animations;

            origin = new((int)destination.X + 32, (int)destination.Y + 64 - (int)(192 * boltScale));

        }

        public void ConstructLightning()
        {

            List<TemporaryAnimatedSprite> animations = new();

            float boltScale = 2.5f + 0.5f * factor;

            int viewY = Game1.viewport.Y;

            Vector2 originAdjust = new(destination.X + 32, destination.Y + 64 - (112 * boltScale));

            Vector2 originOffset = originAdjust + new Vector2(-128 * boltScale, -32 * boltScale);

            bool flippit = false;// Mod.instance.randomIndex.NextBool();

            TemporaryAnimatedSprite bolt1 = new(0, 75, 12, 1, originOffset, false, flippit)
            {

                sourceRect = new(0, 0, 256, 64),

                sourceRectStartingPos = new Vector2(0),

                texture = Mod.instance.iconData.lightningTexture,

                layerDepth = 801f,

                rotation = (float)(Math.PI/2),

                scale = boltScale,

            };

            location.temporarySprites.Add(bolt1);

            animations.Add(bolt1);

            construct = animations;

            origin = new((int)destination.X + 32, (int)destination.Y + 64 - (int)(192 * boltScale));
        }

        public void ConstructJudgement()
        {

            List<TemporaryAnimatedSprite> animations = new();

            float boltScale = 3f;

            int viewY = Game1.viewport.Y;

            Vector2 originAdjust = new(destination.X + 32, destination.Y + 64 - (112 * boltScale));

            Vector2 originOffset = originAdjust + new Vector2(-128 * boltScale, -32 * boltScale);

            bool flippit = false;// Mod.instance.randomIndex.NextBool();

            TemporaryAnimatedSprite bolt1 = new(0, 75, 12, 1, originOffset, false, flippit)
            {

                sourceRect = new(0, 0, 256, 64),

                sourceRectStartingPos = new Vector2(0),

                texture = Mod.instance.iconData.judgementTexture,

                layerDepth = 801f,

                rotation = (float)(Math.PI / 2),

                scale = boltScale,

            };

            location.temporarySprites.Add(bolt1);

            animations.Add(bolt1);

            construct = animations;

            origin = new((int)destination.X + 32, (int)destination.Y + 64 - (int)(192 * boltScale));
        }

    }

}
