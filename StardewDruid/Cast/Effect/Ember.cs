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
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net;
using xTile.Dimensions;
using xTile.Tiles;
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Data.IconData;
using static System.Net.WebRequestMethods;


namespace StardewDruid.Cast.Effect
{
    public class Ember : EventHandle
    {

        public int skipCounter;

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

                if(ember.Value.expire <= Game1.currentGameTime.TotalGameTime.TotalSeconds || ember.Value.location.Name != Game1.player.currentLocation.Name)
                {

                    ember.Value.Shutdown();

                    embers.Remove(ember.Key);

                    continue;

                }

                if(ember.Value.grade == 0)
                {

                    SpellHandle burning = new(ember.Value.location,ember.Value.tile*64,ember.Value.tile*64,192,ember.Value.damageFarmer,ember.Value.damageMonster);

                    burning.instant = true;

                    burning.added = new() { effects.immolate, };

                    Mod.instance.spellRegister.Add(burning);

                }

            }

        }

        public void RadialTarget(GameLocation location, Vector2 origin, int damageFarmers, int damageMonsters, IconData.schemes scheme = schemes.stars, int Time = 3)
        {

            location = Game1.player.currentLocation;

            if(scheme == schemes.none)
            {

                scheme = schemes.stars;

            }

            for (int i = 0; i < 4; i++)
            {

                List<Vector2> burnVectors = ModUtility.GetTilesWithinRadius(location, origin, i);

                foreach (Vector2 burnVector in burnVectors)
                {

                    if (embers.ContainsKey(burnVector))
                    {

                        EmberTarget existing = embers[burnVector];

                        if (existing.grade > i)
                        {

                            existing.Upgrade(Time);

                        }
                        else
                        {

                            existing.Reset(Time);

                        }

                    }
                    else
                    {


                        embers.Add(burnVector,new(location,burnVector, i, damageFarmers, damageMonsters, scheme, Time));

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

        public List<TemporaryAnimatedSprite> animations;

        public int damageFarmer;

        public int damageMonster;

        public GameLocation location;

        public double expire;

        public EmberTarget(GameLocation Location, Vector2 Tile, int Grade = 0, int vsFarmer = 0, int vsMonster = 0, IconData.schemes Scheme = IconData.schemes.stars, int Time = 3)
        {

            location = Location;

            tile = Tile;

            grade = Grade;

            damageFarmer = vsFarmer;

            damageMonster = vsMonster;

            scheme = Scheme;

            Animations(Time);

            expire = Game1.currentGameTime.TotalGameTime.TotalSeconds + Time;

        }

        public void Animations(int Time = 3)
        {

            offset = new Vector2(-16 + Mod.instance.randomIndex.Next(5) * 8, -16 + Mod.instance.randomIndex.Next(5) * 8);

            scale = 1.75f + Mod.instance.randomIndex.Next(4) * 0.25f;

            if(grade == 3)
            {
                scale -= 0.5f;
                offset += new Vector2(8);

            }

            animations = Mod.instance.iconData.EmberConstruct(location, scheme, tile * 64 + offset, 1.75f, grade, Time);

        }

        public void Reset(int Time = 3)
        {
            foreach(TemporaryAnimatedSprite animation in animations)
            {
                
                animation.reset();

            }

            expire = Game1.currentGameTime.TotalGameTime.TotalSeconds + Time;

        }

        public void Upgrade(int Time = 3)
        {

            if(grade == 0) { return; }

            grade--;

            Shutdown();

            Animations(Time);

            expire = Game1.currentGameTime.TotalGameTime.TotalSeconds + Time;

        }

        public void Shutdown()
        {
            
            foreach (TemporaryAnimatedSprite animation in animations)
            {

                location.TemporarySprites.Remove(animation);

            }

        }

    }

}
