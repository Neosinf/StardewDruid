using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace StardewDruid.Event.World
{
    public class Comet : EventHandle
    {

        public Dictionary<int, TemporaryAnimatedSprite> cometAnimations;

        public Comet(Vector2 target, Rite rite)
            : base(target, rite)
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 6;

            cometAnimations = new();

        }

        public override void EventTrigger()
        {

            Mod.instance.RegisterEvent(this, "comet");

        }

        public override bool EventActive()
        {

            if (expireEarly)
            {

                return false;

            }

            if (targetPlayer.currentLocation.Name != targetLocation.Name)
            {

                return false;

            }

            if (expireTime < Game1.currentGameTime.TotalGameTime.TotalSeconds)
            {

                return false;

            }

            return true;

        }

        public override void EventRemove()
        {
            if (cometAnimations.Count > 0)
            {

                foreach (KeyValuePair<int, TemporaryAnimatedSprite> animation in cometAnimations)
                {

                    targetLocation.temporarySprites.Remove(animation.Value);

                }

            }

            cometAnimations.Clear();

        }

        public override void EventInterval()
        {

            activeCounter++;

            if (activeCounter == 1)
            {

                TemporaryAnimatedSprite startAnimation = new(0, 2000f, 1, 1, targetVector - new Vector2(128,128), false, false)
                {

                    sourceRect = new(0, 0, 64, 64),

                    sourceRectStartingPos = new Vector2(0, 0),

                    texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Stars.png")),

                    scale = 5f,

                    //scaleChange = 0.001f,

                    //motion = new Vector2(-0.032f, -0.032f),

                    layerDepth = 0.0001f,

                    rotationChange = 0.06f,

                    timeBasedMotion = true,

                    alpha = 0.75f,

                };

                targetLocation.temporarySprites.Add(startAnimation);

                cometAnimations[0] = startAnimation;

            }

            if (activeCounter == 2)
            {

                Vector2 cometPosition = new(targetVector.X + 320, targetVector.Y - 720);

                Vector2 cometMotion = new Vector2(-0.32f, 0.64f);

                TemporaryAnimatedSprite cometAnimation = new(0, 1000f, 1, 1, cometPosition, false, false)
                {

                    sourceRect = new(0, 0, 32, 32),

                    sourceRectStartingPos = new Vector2(0, 0),

                    texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Fireball.png")),

                    scale = 4f,

                    motion = cometMotion,

                    timeBasedMotion = true,

                    rotationChange = -0.08f,

                    layerDepth = float.Parse("0.0" + targetVector.X.ToString() + targetVector.Y.ToString() + "5"),

                };

                targetLocation.temporarySprites.Add(cometAnimation);

                cometAnimations[1] = cometAnimation;

            }

            if (activeCounter == 3)
            {

                targetLocation.playSound("explosion");

                Mod.instance.CastMessage("Meteor Impact");

                List<Vector2> impactVectors = ModUtility.Explode(targetLocation, new(targetVector.X/64,targetVector.Y/64), targetPlayer, 8, riteData.castDamage * 3, 3, 5);

                foreach (Vector2 vector in impactVectors)
                {

                    ModUtility.ImpactVector(targetLocation, vector);

                }
                expireEarly = true;

            }

        }

    }

}
