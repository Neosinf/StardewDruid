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

namespace StardewDruid.Cast.Fates
{
    public class Escape : EventHandle
    {

        public Dictionary<int, TemporaryAnimatedSprite> escapeAnimations;

        public Vector2 escapeCorner;

        public Vector2 escapeAnchor;

        public Escape(Vector2 target)
            : base(target)
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 6;

            escapeAnimations = new();

            escapeAnchor = targetVector;

            escapeCorner = escapeAnchor - new Vector2(64, 64);

        }

        public override void EventTrigger()
        {

            Mod.instance.RegisterEvent(this, "escape");

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
            if (escapeAnimations.Count > 0)
            {

                foreach (KeyValuePair<int, TemporaryAnimatedSprite> animation in escapeAnimations)
                {

                    targetLocation.temporarySprites.Remove(animation.Value);

                }

            }

            escapeAnimations.Clear();

        }

        public override void EventInterval()
        {

            if (Vector2.Distance(targetVector, Mod.instance.rite.caster.Position) <= 32 && Mod.instance.rite.castLevel > activeCounter)
            {

                activeCounter++;

            }
            else
            {

                EventRemove();

                expireEarly = true;

                return;

            }

            if (activeCounter == 3)
            {

                TemporaryAnimatedSprite startNightAnimation = new(0, 2000f, 1, 1, escapeAnchor, false, false)
                {

                    sourceRect = new(0, 0, 64, 64),

                    sourceRectStartingPos = new Vector2(0, 0),

                    texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Nightsky.png")),

                    scale = 1f,

                    scaleChange = 0.002f,

                    motion = new(-0.064f, -0.064f),

                    layerDepth = 0.0001f,

                    alpha = 0.75f,

                    timeBasedMotion = true,

                };

                targetLocation.temporarySprites.Add(startNightAnimation);

                escapeAnimations[1] = startNightAnimation;

            }

            if(activeCounter == 4)
            {

                escapeAnimations[1].scaleChange = 0;
                escapeAnimations[1].motion = Vector2.Zero;

            }

            if (activeCounter == 5)
            {
                if (targetLocation is MineShaft)
                {

                    if (PerformXzone())
                    {

                        Mod.instance.AbortAllEvents();

                    }

                }
                else
                {
                    if (PerformEscape())
                    {

                        Mod.instance.AbortAllEvents();

                    }

                }

            }

        }

        public bool PerformEscape()
        {

            List<Vector2> destinations = new();

            float newDistance;

            float furthestDistance = 0f;

            List<string> surveyed = new();

            foreach (Warp warp in targetLocation.warps)
            {

                if (surveyed.Contains(warp.TargetName))
                {

                    continue;

                }

                surveyed.Add(warp.TargetName);

                Vector2 destination;

                if (WarpData.WarpExclusions(targetLocation, warp))
                {

                    destination = WarpData.WarpVectors(targetLocation);

                    if (destination == Vector2.Zero)
                    {

                        continue;

                    }

                }
                else
                {

                    destination = WarpData.WarpReverse(targetLocation, warp);

                    if (destination == Vector2.Zero)
                    {

                        continue;

                    }

                }

                Vector2 possibility = destination * 64;

                if (destinations.Count == 0)
                {

                    destinations.Add(possibility);

                    furthestDistance = Vector2.Distance(targetVector, possibility);

                }
                else
                {

                    newDistance = Vector2.Distance(targetVector, possibility);

                    if (Mod.instance.rite.caster.getGeneralDirectionTowards(possibility, 0, false, false) == Mod.instance.rite.caster.facingDirection.Value && newDistance > furthestDistance)
                    {

                        destinations.Clear();

                        destinations.Add(possibility);

                    }

                }

            }

            if (destinations.Count > 0)
            {
                Game1.flashAlpha = 1;

                Mod.instance.rite.caster.Position = destinations[0];

                //ModUtility.AnimateQuickWarp(targetLocation, destinations[0]);

                return true;

            }

            return false;

        }

        public bool PerformXzone()
        {

            Type reflectType = typeof(MineShaft);

            FieldInfo reflectField = reflectType.GetField("netTileBeneathLadder", BindingFlags.NonPublic | BindingFlags.Instance);

            var tile = reflectField.GetValue(targetLocation as MineShaft);

            if (tile == null)
            {

                return false;
            }

            string tileString = tile.ToString();

            Match m = Regex.Match(tileString, @"\{*X\:(\d+)\sY\:(\d+)\}", RegexOptions.IgnoreCase);

            if (!m.Success)
            {

                return false;

            }

            int tileX = Convert.ToInt32(m.Groups[1].Value);

            int tileY = Convert.ToInt32(m.Groups[2].Value);

            Vector2 destination = new Vector2(tileX, tileY) * 64;

            Game1.flashAlpha = 1;

            Mod.instance.rite.caster.Position = destination;

            //ModUtility.AnimateQuickWarp(targetLocation, destination);

            return true;

        }

    }

}
