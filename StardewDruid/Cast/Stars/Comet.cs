using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using xTile.Dimensions;

namespace StardewDruid.Cast.Stars
{
    public class Comet : EventHandle
    {

        public Dictionary<int, TemporaryAnimatedSprite> cometAnimations;

        public Vector2 cometVector;

        public float damage;

        public Comet(Vector2 target,  float Damage)
            : base(target)
        {

            cometVector = target * 64;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 6;

            cometAnimations = new();

            damage = Damage * 5;

        }

        public override void EventTrigger()
        {

            Mod.instance.RegisterEvent(this, "comet" + targetVector.ToString());

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

                TemporaryAnimatedSprite startAnimation = new(0, 2000f, 1, 1, cometVector - new Vector2(128, 128), false, false)
                {

                    sourceRect = new(128, 0, 64, 64),

                    sourceRectStartingPos = new Vector2(128,0),

                    texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Decorations.png")),

                    scale = 5f,

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

                ModUtility.AnimateMeteor(targetLocation, targetVector, false,2);

            }

            if (activeCounter == 3)
            {

                targetLocation.playSound("explosion");

                Mod.instance.CastMessage("Meteor Impact");

                ModUtility.DamageMonsters(targetLocation, ModUtility.MonsterProximity(targetLocation, new() { targetVector * 64 }, 8, true), targetPlayer, (int)damage, true);

                ModUtility.Explode(targetLocation, targetVector, targetPlayer, 8, 4, 5);

                ModUtility.AnimateImpact(targetLocation, targetVector, 3, 2);

                expireEarly = true;

            }

        }

    }

}
