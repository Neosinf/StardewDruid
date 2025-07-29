using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static StardewDruid.Data.IconData;


namespace StardewDruid.Cast.Effect
{
    public class Ember : EventHandle
    {

        public Dictionary<Vector2, EmberTarget> embers = new();

        public bool immolate;

        public Ember()
        {

        }

        public override void EventInterval()
        {

            // ===================================================
            // check embers

            for (int e = embers.Count - 1; e >= 0; e--)
            {

                KeyValuePair<Vector2, EmberTarget> ember = embers.ElementAt(e);

                if(ember.Value.expire <= Game1.currentGameTime.TotalGameTime.TotalSeconds)
                {

                    //ember.Value.Fadeout();

                    ember.Value.Shutdown();

                    embers.Remove(ember.Key);

                    continue;

                }

                if(ember.Value.location.Name != Game1.player.currentLocation.Name)
                {

                    ember.Value.Shutdown();

                    embers.Remove(ember.Key);

                    continue;

                }

                if (ember.Value.grade == 0)
                {

                    SpellHandle burning = new(ember.Value.location, ember.Value.tile * 64, ember.Value.tile * 64, 192, ember.Value.damageFarmer, ember.Value.damageMonster)
                    {
                        instant = true,

                        added = new() { SpellHandle.Effects.immolate, }
                    };

                    Mod.instance.spellRegister.Add(burning);

                }

            }

            if(embers.Count == 0)
            {

                eventComplete = true;

            }

        }

        public void RadialTarget(GameLocation location, Vector2 origin, int damageFarmers, int damageMonsters, IconData.schemes scheme = IconData.schemes.stars, int coverage = 4)
        {

            location = Game1.player.currentLocation;

            coverage = Math.Min(9, coverage);

            if(scheme == IconData.schemes.none)
            {

                scheme = IconData.schemes.stars;

            }

            for (int i = 1; i < coverage; i++)
            {

                List<Vector2> burnVectors = ModUtility.GetTilesWithinRadius(location, origin, i);

                for(int b = 0; b < burnVectors.Count; b++)
                {
                    
                    if((Mod.instance.randomIndex.Next(8) != 0))
                    {

                        continue;

                    }

                    Vector2 burnVector = burnVectors[b];

                    if(ModUtility.GroundCheck(location,burnVector) != "ground")
                    {

                        continue;

                    }

                    if (!embers.ContainsKey(burnVector))
                    {

                        EmberTarget ember = new(location, burnVector, i, damageFarmers, damageMonsters, scheme);

                        embers.Add(burnVector,ember);

                    }

                }

            }

        }

    }

    public class EmberTarget
    {

        public Vector2 tile;

        public Vector2 offset;

        public int grade;

        public float scale;

        public IconData.schemes scheme;

        public List<TemporaryAnimatedSprite> animations = new();

        public int damageFarmer;

        public int damageMonster;

        public GameLocation location;

        public double expire;

        public EmberTarget()
        {

        }

        public EmberTarget(GameLocation Location, Vector2 Tile, int Grade = 0, int vsFarmer = 0, int vsMonster = 0, IconData.schemes Scheme = IconData.schemes.stars)
        {

            location = Location;

            tile = Tile;

            grade = Grade;

            damageFarmer = vsFarmer;

            damageMonster = vsMonster;

            scheme = Scheme;

            Animations();

        }

        public void Animations()
        {

            offset = new Vector2(-16 + Mod.instance.randomIndex.Next(5) * 8, -16 + Mod.instance.randomIndex.Next(5) * 8);

            scale = 3.5f - 0.25f * grade;

            List<TemporaryAnimatedSprite> newAnimations = EmberConstruct(location, scheme, tile * 64 + offset, scale);

            animations.AddRange(newAnimations);

            expire = Game1.currentGameTime.TotalGameTime.TotalSeconds + 3;

        }

        public void Reset()
        {

            Animations();

        }

        public void Upgrade()
        {

            if(grade == 0) { return; }

            grade--;

            Shutdown();

            Animations();

        }

        public void Fadeout()
        {

            foreach (TemporaryAnimatedSprite animation in animations)
            {

                if (animation.lightId != null)
                {

                    Utility.removeLightSource(animation.lightId);

                }

                animation.alphaFade = 0.001f;

                animation.timeBasedMotion = true;

            }

        }

        public void Shutdown()
        {
            
            foreach (TemporaryAnimatedSprite animation in animations)
            {

                if (animation.lightId != null)
                {

                    Utility.removeLightSource(animation.lightId);

                }

                location.TemporarySprites.Remove(animation);

            }

        }

        public List<TemporaryAnimatedSprite> EmberConstruct(GameLocation location, schemes scheme, Vector2 origin, float scale, int Time = 1, float layer = -1f)
        {

            if (!Mod.instance.iconData.gradientColours.ContainsKey(scheme))
            {

                scheme = schemes.stars;

            }

            List<TemporaryAnimatedSprite> emberAnimations = new();

            if (layer <= 0)
            {

                layer = origin.Y / 10000;

                layer -= (0.0001f * scale);

            }

            float fade = 1f - (0.05f * Mod.instance.randomIndex.Next(8));

            bool smokeFlip = Game1.player.FacingDirection < 2;

            switch (Mod.instance.randomIndex.Next(4))
            {
                default:

                    break;

                case 0:

                    emberAnimations.Add(Mod.instance.iconData.CreateImpact(location, origin + new Vector2(32) - (new Vector2(16, 28) * scale), impacts.plume, scale, new() { interval = 200, loops = Math.Min(3, Time), layer = layer - 0.0001f, alpha = 0.75f, flip = Mod.instance.randomIndex.NextBool() }));

                    break;

                case 1:

                    emberAnimations.Add(Mod.instance.iconData.CreateImpact(location, origin + new Vector2(32) - (new Vector2(16, 28) * scale), impacts.plume, scale, new() { interval = 200, loops = Math.Min(3, Time), color = Microsoft.Xna.Framework.Color.LightGray, layer = layer - 0.0001f, alpha = 0.75f, flip = Mod.instance.randomIndex.NextBool() }));

                    break;

                case 2:

                    emberAnimations.Add(Mod.instance.iconData.CreateImpact(location, origin + new Vector2(32) - (new Vector2(16, 28) * scale), impacts.smoke, scale, new() { interval = 200, loops = Math.Min(3, Time), layer = layer - 0.0001f, alpha = 0.75f, flip = Mod.instance.randomIndex.NextBool() }));

                    break;

                case 3:

                    emberAnimations.Add(Mod.instance.iconData.CreateImpact(location, origin + new Vector2(32) - (new Vector2(16, 28) * scale), impacts.smoke, scale, new() { interval = 200, loops = Math.Min(3, Time), color = Microsoft.Xna.Framework.Color.LightGray, layer = layer - 0.0001f, alpha = 0.75f, flip = Mod.instance.randomIndex.NextBool() }));

                    break;
            }

            emberAnimations.Add(emberAnimation(location, origin, scale, 0, Mod.instance.iconData.gradientColours[scheme][2], layer, fade, Time));

            emberAnimations.Add(emberAnimation(location, origin, scale, 1, Mod.instance.iconData.gradientColours[scheme][1], layer, fade, Time));

            emberAnimations.Add(emberAnimation(location, origin, scale, 2, Mod.instance.iconData.gradientColours[scheme][0], layer, fade, Time));

            emberAnimations.Add(emberAnimation(location, origin, scale, 3, Microsoft.Xna.Framework.Color.White, layer, fade, Time));

            emberAnimations.Add(emberAnimation(location, origin + new Vector2(0, 8), scale, 3, Microsoft.Xna.Framework.Color.Black * 0.25f, layer - 0.0002f, 1f, Time));

            return emberAnimations;

        }

        public TemporaryAnimatedSprite emberAnimation(GameLocation location, Vector2 origin, float scale, int part, Microsoft.Xna.Framework.Color color, float layer, float alpha, int Time)
        {

            TemporaryAnimatedSprite burnAnimation;

            if (part == 2)
            {

                string lightid = "18465_" + (origin.X * 10000 + origin.Y).ToString();

                burnAnimation = new(0, 200, 15, Time, origin + new Vector2(32) - (new Vector2(16) * scale), false, false)
                {

                    sourceRect = new(0, (part * 32), 32, 32),

                    sourceRectStartingPos = new(0, (part * 32)),

                    texture = Mod.instance.iconData.emberTexture,

                    scale = scale,

                    layerDepth = layer,

                    alpha = alpha,

                    color = color,

                    lightId = lightid,

                    lightRadius = 2,

                };

            }
            else
            {

                burnAnimation = new(0, 200, 15, Time, origin + new Vector2(32) - (new Vector2(16) * scale), false, false)
                {

                    sourceRect = new(0, (part * 32), 32, 32),

                    sourceRectStartingPos = new(0, (part * 32)),

                    texture = Mod.instance.iconData.emberTexture,

                    scale = scale,

                    layerDepth = layer,

                    alpha = alpha,

                    color = color,

                };


            }

            location.TemporarySprites.Add(burnAnimation);

            return burnAnimation;

        }


    }

}
