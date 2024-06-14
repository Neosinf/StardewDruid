using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Constants;
using StardewValley.Locations;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using static StardewDruid.Cast.SpellHandle;
using static StardewValley.Minigames.TargetGame;
using static System.Formats.Asn1.AsnWriter;

namespace StardewDruid.Cast.Mists
{
    public class Wisps : EventHandle
    {

        public Dictionary<Vector2, WispHandle> wisps = new();

        public int wispCounter;

        public Wisps()
            : base()
        {

        }

        public Vector2 WispVector(Vector2 target)
        {

            return new((int)(target.X / 12), (int)(target.Y / 12));

        }

        public Vector2 AddWisps(int index, int charge = 120)
        {

            for (int i = 0; i < 3; i++)
            {

                List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(location, ModUtility.PositionToTile(origin), Mod.instance.randomIndex.Next(5, 9), true, index);

                if (castSelection.Count > 0)
                {

                    Vector2 tryVector = castSelection[Mod.instance.randomIndex.Next(castSelection.Count)];

                    Vector2 wispVector = WispVector(tryVector);

                    if (wisps.ContainsKey(wispVector))
                    {

                        continue;

                    }

                    string ground = ModUtility.GroundCheck(location, tryVector);

                    Microsoft.Xna.Framework.Color colour = Mod.instance.iconData.SchemeColour(IconData.schemes.Aquamarine);

                    if(ground == "water")
                    {

                        //colour = Mod.instance.iconData.SchemeColour(IconData.schemes.Topaz);
                        colour = Microsoft.Xna.Framework.Color.White;

                    }
                    else if (ground != "ground")
                    {

                        continue;

                    }

                    switch (Mod.instance.randomIndex.Next(3))
                    {
                        case 0:
                            colour = new(colour.R - 24, colour.G, colour.B - 16);
                            break;
                        case 1:
                            colour = new(colour.R, colour.G - 24, colour.B - 16);
                            break;
                    }

                    wisps[wispVector] = new(location, tryVector, colour, charge);

                    activeLimit = eventCounter + charge;


                }

            }

            return Vector2.Zero;

        }

        public override bool EventActive()
        {

            if (!eventLocked)
            {

                if (Mod.instance.Config.riteButtons.GetState() != SButtonState.Held)
                {

                    return false;

                }

                if (Vector2.Distance(origin, Game1.player.Position) > 32)
                {

                    return false;

                }

            }

            return base.EventActive();

        }

        public override void EventRemove()
        {

            for(int w = wisps.Count - 1; w >= 0; w--)
            {

                KeyValuePair<Vector2,WispHandle> wisp = wisps.ElementAt(w);

                wisp.Value.shutdown();

            }

            base.EventRemove();

        }

        public override void EventDecimal()
        {

            if (eventLocked)
            {

                return;

            }

            if (!EventActive())
            {

                RemoveAnimations();

                return;

            }

            decimalCounter++;

            if (decimalCounter == 5)
            {

                TemporaryAnimatedSprite skyAnimation = Mod.instance.iconData.SkyIndicator(location, origin, IconData.skies.moon, 1f, new() { interval = 1000, });

                skyAnimation.scaleChange = 0.002f;

                skyAnimation.motion = new(-0.064f, -0.064f);

                skyAnimation.timeBasedMotion = true;

                animations.Add(skyAnimation);

            }

            if (decimalCounter < 15)
            {

                return;

            }

            if(decimalCounter == 15)
            {

                TemporaryAnimatedSprite skyAnimation = Mod.instance.iconData.SkyIndicator(location, origin, IconData.skies.moon, 3f, new() { interval = 1000, });

                animations.Add(skyAnimation);

                location.playSound("thunder");

                WispArray();

                eventLocked = true;

            }

        }

        public virtual void WispArray()
        {

            wispCounter = Mod.instance.randomIndex.Next(8);

            Vector2 wispVector = AddWisps(wispCounter);

            Mod.instance.spellRegister.Add(new(wispVector * 64, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

            wispVector = AddWisps((wispCounter + 2) % 8);

            Mod.instance.spellRegister.Add(new(wispVector * 64, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

            wispVector = AddWisps((wispCounter + 4) % 8);

            Mod.instance.spellRegister.Add(new(wispVector * 64, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

            wispVector = AddWisps((wispCounter + 6) % 8);

            Mod.instance.spellRegister.Add(new(wispVector * 64, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

        }


        public override void EventInterval()
        {
            
            if(!eventLocked)
            { 
                
                return; 
            
            }

            activeCounter++;

            if (activeCounter % 3 != 0)
            {

                return;

            }

            float damage = Mod.instance.CombatDamage() / 2;

            for (int i = wisps.Count - 1; i >= 0; i--)
            {

                KeyValuePair<Vector2, WispHandle> moment = wisps.ElementAt(i);

                if (moment.Value.activation > 0)
                {

                    moment.Value.activation--;

                }

                if (activeCounter % 6 == 0)
                {
                    if (!moment.Value.reset())
                    {

                        wisps.Remove(moment.Key);

                        continue;

                    }

                }

                List<StardewValley.Monsters.Monster> victims = ModUtility.MonsterProximity(Game1.player.currentLocation, new() { moment.Value.position, }, 256, true);

                if(victims.Count == 0)
                {

                    continue;

                }

                foreach (StardewValley.Monsters.Monster victim in victims)
                {

                    SpellHandle bolt = new(Game1.player, new() { victim, }, damage);

                    bolt.type = SpellHandle.spells.bolt;

                    bolt.added = new() { effects.push };

                    Mod.instance.spellRegister.Add(bolt);

                }

            }

        }

    }

    public class WispHandle
    {

        public GameLocation location;

        public Vector2 tile;

        public Vector2 position;

        public TemporaryAnimatedSprite wisp;

        public TemporaryAnimatedSprite eyes;

        public TemporaryAnimatedSprite light;

        public int timer;

        public bool initiated;

        public bool completed;

        public Random randomIndex;

        public int activation;

        public Microsoft.Xna.Framework.Color colour;

        public WispHandle(GameLocation Location, Vector2 Tile, Microsoft.Xna.Framework.Color Colour, int Timer = -1)
        {

            location = Location;

            tile = Tile;

            position = tile * 64;

            timer = Timer;

            randomIndex = new();

            colour = Colour;

            initiate();

        }

        public void initiate()
        {

            light = new(23, 1000f, 1, timer, position, flicker: false, randomIndex.Next(2) == 0 ? true : false)
            {
                texture = Game1.animations,
                light = true,
                lightRadius = 3,
                lightcolor = Microsoft.Xna.Framework.Color.White,
                alpha = 0.5f,
                Parent = location,
            };

            location.temporarySprites.Add(light);

            bool wispFlip = randomIndex.Next(2) == 0;

            Texture2D wispTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Wisp.png"));

            wisp = new(0, 250f, 8, timer / 2, position - new Vector2(32, 64), false, false)
            {

                sourceRect = new(0, 0, 32, 32),

                sourceRectStartingPos = new Vector2(0,0),

                texture = wispTexture,

                scale = 4, //* size,

                layerDepth = 992f,

                alpha = 0.3f,

                flipped = wispFlip,

                color = colour,

            };

            location.temporarySprites.Add(wisp);

            eyes = new(0, 250f, 8, timer / 2, position - new Vector2(32, 64), false, false)
            {

                sourceRect = new(0, 32, 32, 32),

                sourceRectStartingPos = new Vector2(0, 32),

                texture = wispTexture,

                scale = 4, //* size,

                layerDepth = 992f,

                alpha = 0.4f,

                flipped = wispFlip,

            };

            location.temporarySprites.Add(eyes);

            //light = Mod.instance.iconData.ImpactIndicator(location, position, IconData.impacts.cloud, 2.5f, new() { alpha = 0.3f, interval = 125f, loops = timer, color = colour, });

            initiated = true;

        }

        public bool reset()
        {

            if (!initiated)
            {

                initiate();

            }

            if (timer != -1)
            {

                timer--;

                if (timer <= 0)
                {

                    shutdown();

                    return false;

                }

            }

            if (Game1.getLocationFromName(location.Name) == null)
            {
                shutdown();

                return false;

            }

            if (!location.temporarySprites.Contains(wisp))
            {
                if (light != null)
                {

                    location.temporarySprites.Remove(light);

                }
                if (eyes != null)
                {

                    location.temporarySprites.Remove(eyes);

                }

                initiate();

                completed = false;

            }
            else
            {

                wisp.reset();

                light.reset();

                eyes.reset();

            }

            return true;

        }

        public void shutdown()
        {

            location.temporarySprites.Remove(light);

            location.temporarySprites.Remove(wisp);

            location.temporarySprites.Remove(eyes);

            //location.playSound("fireball");

        }

    }


}
