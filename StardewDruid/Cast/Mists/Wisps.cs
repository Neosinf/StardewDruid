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
using StardewValley.Network;
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

            activeLimit = -1;

        }

        public override void EventDraw(SpriteBatch b)
        {

            for (int w = wisps.Count - 1; w >= 0; w--)
            {

                KeyValuePair<Vector2, WispHandle> wisp = wisps.ElementAt(w);

                wisp.Value.draw(b);

            }

        }

        public Vector2 WispVector(Vector2 target)
        {

            return new((int)(target.X / 12), (int)(target.Y / 12));

        }

        public Vector2 AddWisps(int index, int charge = 120, int distance = 0)
        {

            Vector2 usePosition = origin;

            if(distance > 0)
            {

                usePosition = origin + (ModUtility.DirectionAsVector(index) * distance);

            }

            for (int i = 0; i < 3; i++)
            {

                List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(location, ModUtility.PositionToTile(usePosition), Mod.instance.randomIndex.Next(5, 9), true, index);

                if (castSelection.Count > 0)
                {

                    Vector2 tryVector = castSelection[Mod.instance.randomIndex.Next(castSelection.Count)];

                    Vector2 wispVector = WispVector(tryVector);

                    if (wisps.ContainsKey(wispVector))
                    {

                        continue;

                    }

                    Microsoft.Xna.Framework.Color colour = Mod.instance.iconData.gradientColours[IconData.schemes.wisps][Mod.instance.randomIndex.Next(4)];

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

                Mod.instance.rite.channel(IconData.skies.moon, 75);

                channel = IconData.skies.moon;

            }

            if (decimalCounter < 15)
            {

                return;

            }

            if(decimalCounter == 15)
            {

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

            wispVector = AddWisps((wispCounter + 1) % 8, 120, 384);

            Mod.instance.spellRegister.Add(new(wispVector * 64, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

            wispVector = AddWisps((wispCounter + 3) % 8, 120, 384);

            Mod.instance.spellRegister.Add(new(wispVector * 64, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

            wispVector = AddWisps((wispCounter + 5) % 8, 120, 384);

            Mod.instance.spellRegister.Add(new(wispVector * 64, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

            wispVector = AddWisps((wispCounter + 7) % 8, 120, 384);

            Mod.instance.spellRegister.Add(new(wispVector * 64, 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

        }


        public override void EventInterval()
        {
            
            if(!eventLocked)
            { 
                
                return; 
            
            }

            activeCounter++;

            if (activeCounter % 5 != 0)
            {

                return;

            }

            List<StardewValley.Monsters.Monster> victims = new();

            for (int i = wisps.Count - 1; i >= 0; i--)
            {

                KeyValuePair<Vector2, WispHandle> moment = wisps.ElementAt(i);

                if (moment.Value.activation > 0)
                {

                    moment.Value.activation--;

                }

                if (!moment.Value.update())
                {

                    wisps.Remove(moment.Key);

                    continue;

                }

                List<StardewValley.Monsters.Monster> closeby = ModUtility.MonsterProximity(Game1.player.currentLocation, new() { moment.Value.position, }, 256, true);

                if (closeby.Count > 0)
                {
                    
                    victims.AddRange(closeby);

                    moment.Value.flash = 60;
                
                }

            }

            if(victims.Count > 0)
            {
                
                SpellHandle bolt = new(Game1.player, new() { victims[Mod.instance.randomIndex.Next(victims.Count)], }, Mod.instance.CombatDamage()*2);

                bolt.type = SpellHandle.spells.bolt;

                bolt.projectile = 4;

                bolt.sound = sounds.thunder;

                bolt.added = new() { effects.push, effects.drain, effects.shock, };

                Mod.instance.spellRegister.Add(bolt);

            }

            if(wisps.Count == 0)
            {

                eventComplete = true;

            }

        }

    }

    public class WispHandle
    {

        public GameLocation location;

        public Vector2 tile;

        public Vector2 position;
        
        public Vector2 origin;

        public LightSource light;

        public int flash;

        public int timer;

        public int activation;

        public Microsoft.Xna.Framework.Color colour;

        public bool flip;

        public WispHandle(GameLocation Location, Vector2 Tile, Microsoft.Xna.Framework.Color Colour, int Timer = -1)
        {

            location = Location;

            tile = Tile;

            position = (tile * 64);

            timer = Timer;

            colour = Colour;

            flip = Mod.instance.randomIndex.Next(2) == 0;

            initiate();

        }

        public void draw(SpriteBatch b)
        {
            if (!Utility.isOnScreen(position, 128) && Game1.player.currentLocation.Name == location.Name)
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(position);

            float drawLayer = (float)position.Y / 10000f + 0.001f;

            int wispFrame = (int)((Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 2000) / 250);

            int wispOffset = Math.Abs(-4 + wispFrame);

            Microsoft.Xna.Framework.Color useColour = colour;

            if(flash > 0)
            {

                useColour = Microsoft.Xna.Framework.Color.White;

                flash--;

            }

            b.Draw(
                Mod.instance.iconData.wispTexture,
                localPosition - new Vector2(32, 80f),
                new Microsoft.Xna.Framework.Rectangle(0 + (wispFrame * 32), 0, 32, 32),
                colour * (0.90f - (0.05f * wispOffset)),
                0f,
                Vector2.Zero,
                4f,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer
            );

            b.Draw(
                Mod.instance.iconData.wispTexture,
                localPosition - new Vector2(32, 80f),
                new Microsoft.Xna.Framework.Rectangle(0 + (wispFrame*32),32,32,32),
                Microsoft.Xna.Framework.Color.White * 0.90f,
                0f,
                Vector2.Zero,
                4f,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer
            );

            b.Draw(Mod.instance.iconData.cursorTexture, localPosition + new Vector2(32, 56), Mod.instance.iconData.shadowRectangle, Microsoft.Xna.Framework.Color.White * 0.15f, 0.0f, new Vector2(24), 1.2f + (wispOffset * 0.1f), 0, drawLayer - 0.0001f);

        }

        public void initiate()
        {


            int id = (int)(position.X * 10000 + position.Y);

            for (int l = Game1.currentLightSources.Count - 1; l >= 0; l--)
            {
                LightSource lightSource = Game1.currentLightSources.ElementAt(l);

                if (lightSource.Identifier == id)
                {

                    //Game1.currentLightSources.Remove(lightSource);

                    return;

                }

            }

            light = new LightSource(1, position + new Vector2(32), 4f, Microsoft.Xna.Framework.Color.Black * 0.75f, id, LightSource.LightContext.None, 0L);

            Game1.currentLightSources.Add(light);

        }

        public bool update()
        {

            if (timer != -1)
            {

                timer--;

                if (timer <= 0)
                {

                    shutdown();

                    return false;

                }

            }

            if (Game1.player.currentLocation.Name != location.Name)
            {
                
                shutdown();

                return false;

            }

            return true;

        }

        public void shutdown()
        {

            Game1.currentLightSources.Remove(light);

        }

    }


}
