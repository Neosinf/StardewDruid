using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;


namespace StardewDruid.Cast.Effect
{
    public class Ember : EventHandle
    {

        public Dictionary<Vector2, EmberTarget> embers = new();

        public Dictionary<string,Texture2D> burnTextures = new();

        public bool immolate;

        public Ember()
        {

            inabsentia = true;

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

                    SpellHandle burning = new(ember.Value.location,ember.Value.tile*64,ember.Value.tile*64,192,ember.Value.damageFarmer,ember.Value.damageMonster);

                    burning.instant = true;

                    burning.added = new() { SpellHandle.effects.immolate, };

                    Mod.instance.spellRegister.Add(burning);

                }

            }

        }

        public void RadialTarget(GameLocation location, Vector2 origin, int damageFarmers, int damageMonsters, IconData.schemes scheme = IconData.schemes.stars)
        {

            location = Game1.player.currentLocation;

            if(scheme == IconData.schemes.none)
            {

                scheme = IconData.schemes.stars;

            }

            for (int i = 0; i < 4; i++)
            {

                List<Vector2> burnVectors = ModUtility.GetTilesWithinRadius(location, origin, i);

                for(int b = 0; b < burnVectors.Count; b++)
                {
                    
                    Vector2 burnVector = burnVectors[b];

                    if(ModUtility.GroundCheck(location,burnVector) != "ground" || (i != 0 && Mod.instance.randomIndex.Next(10) != 0))
                    {

                        continue;

                    }

                    if (embers.ContainsKey(burnVector))
                    {

                        //EmberTarget existing = embers[burnVector];

                        //if (existing.grade > i)
                        //{

                        //    existing.Upgrade();

                        //}

                    }
                    else
                    {

                        embers.Add(burnVector,new(location,burnVector, i, damageFarmers, damageMonsters, scheme));

                    }

                }

            }

            activeLimit = eventCounter + 5;

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

            List<TemporaryAnimatedSprite> newAnimations = Mod.instance.iconData.EmberConstruct(location, scheme, tile * 64 + offset, scale);

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

                if (animation.lightID != 0)
                {

                    Utility.removeLightSource(animation.lightID);

                }

                animation.alphaFade = 0.001f;

                animation.timeBasedMotion = true;

            }

        }

        public void Shutdown()
        {
            
            foreach (TemporaryAnimatedSprite animation in animations)
            {

                if(animation.lightID != 0)
                {

                    Utility.removeLightSource(animation.lightID);

                }

                location.TemporarySprites.Remove(animation);

            }

        }

    }

}
