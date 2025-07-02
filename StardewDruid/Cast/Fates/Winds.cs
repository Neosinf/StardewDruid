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
using xTile.Layers;

namespace StardewDruid.Cast.Fates
{
    public class Winds : EventHandle
    {

        public Dictionary<Vector2, WispHandle> wisps = new();

        public int wispCounter;

        public int tickCounter;

        public float damageMonsters;

        public float damageFarmers;

        public Winds()
            : base()
        {

            activeLimit = -1;

        }

        public override void EventDraw(SpriteBatch b)
        {

            if (wisps.Count == 0)
            {

                return;
            }

            tickCounter++;

            if(tickCounter % 6 == 0)
            {

                foreach (KeyValuePair<Vector2,WispHandle> wisp in wisps)
                {

                    wisp.Value.Movement();

                }

            }

            for (int w = wisps.Count - 1; w >= 0; w--)
            {

                KeyValuePair<Vector2, WispHandle> wisp = wisps.ElementAt(w);

                wisp.Value.draw(b);

            }

        }

        public override bool EventActive()
        {

            if (!eventLocked)
            {

                if (!Mod.instance.RiteButtonHeld())
                {

                    return false;

                }

                if (Vector2.Distance(origin, Game1.player.Position) > 32 && !Mod.instance.ShiftButtonHeld())
                {

                    return false;

                }

            }

            if (Vector2.Distance(origin, Game1.player.Position) > 1280)
            {

                return false;

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

            if (!EventActive())
            {

                RemoveAnimations();

                return;

            }

            decimalCounter++;

            if (eventLocked)
            {

                if (decimalCounter % 3 == 0)
                {

                    foreach (KeyValuePair<Vector2, WispHandle> wisp in wisps)
                    {

                        wisp.Value.Behaviour();

                    }

                }

                return;

            }

            if (decimalCounter == 5)
            {

                Mod.instance.rite.Channel(IconData.skies.hellscape, 75);

                channel = IconData.skies.hellscape;

            }

            if (decimalCounter < 15)
            {

                return;

            }

            if(decimalCounter == 15)
            {

                Game1.flashAlpha = 1f;

                location.playSound("thunder");

                WindArray(new(),WispHandle.wisptypes.winds, 120);

                eventLocked = true;

                Mod.instance.rite.castLevel = 0;

                Mod.instance.rite.ChargeSet(IconData.cursors.fatesCharge);

            }

        }

        public virtual void WindArray(List<StardewDruid.Monster.Boss> bossMonsters, WispHandle.wisptypes wispType = WispHandle.wisptypes.winds, int charge = 120)
        {

            Vector2 Tile = ModUtility.PositionToTile(origin);


            for (int i = 0; i < 10; i++)
            {

                double angle = 5 + 36 * i;

                Vector2 wind = origin + (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 640);

                wisps[wind] = new(location, Tile, wispType, wind, 0.1f);

            }

            for (int i = 0; i < 9; i++)
            {

                double angle = 10 + 40 * i;

                Vector2 wind = origin + (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 560);

                wisps[wind] = new(location, Tile, wispType, wind, 0.4f)
                {
                    damageFarmers = damageFarmers,

                    damageMonsters = damageMonsters
                };

                if (bossMonsters.Count > 0)
                {

                    wisps[wind].bossMonster = bossMonsters.First();

                }

            }

            for (int i = 0; i < 8; i++)
            {
                double angle = 15 + 45 * i;

                Vector2 wind = origin + (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 480);

                wisps[wind] = new(location, Tile, wispType, wind, 0.1f);

            }

            for (int i = 0; i < 7; i++)
            {
                double angle = 20 + 51 * i;

                Vector2 wind = origin + (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 400);

                wisps[wind] = new(location, Tile, wispType, wind, 0.3f)
                {
                    damageFarmers = damageFarmers,

                    damageMonsters = damageMonsters
                };

                if (bossMonsters.Count > 0)
                {

                    wisps[wind].bossMonster = bossMonsters.First();

                }

            }

            for (int i = 0; i < 6; i++)
            {
                double angle = 25 + 60 * i;

                Vector2 wind = origin + (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 320);

                wisps[wind] = new(location, Tile, wispType, wind, 0.2f);

            }

            eventCounter = 0;

            activeLimit = charge;

        }

        public override void EventInterval()
        {

            if(activeLimit - eventCounter < 5)
            {

                for (int w = wisps.Count - 1; w >= 0; w--)
                {

                    KeyValuePair<Vector2, WispHandle> wisp = wisps.ElementAt(w);

                    wisp.Value.fadeTo = 0f;

                }

            }

        }

    }

}
