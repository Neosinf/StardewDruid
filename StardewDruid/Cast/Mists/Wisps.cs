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

namespace StardewDruid.Cast.Mists
{
    public class Wisps : EventHandle
    {

        public Dictionary<Vector2, WispHandle> wisps = new();

        public int wispCounter;

        public int tickCounter;

        public Wisps()
            : base()
        {

            activeLimit = -1;

        }

        public override void EventDraw(SpriteBatch b)
        {

            tickCounter++;

            if(tickCounter % 6 == 0)
            {

                if (tickCounter % 24 == 0)
                {

                    foreach (KeyValuePair<Vector2, WispHandle> wisp in wisps)
                    {

                        wisp.Value.Behaviour();

                    }

                }

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

        public Vector2 WispVector(Vector2 target)
        {

            return new((int)(target.X / 12), (int)(target.Y / 12));

        }

        public Vector2 AddWisps(int index, int distance = 0)
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

                    wisps[wispVector] = new(location, tryVector);

                    return tryVector;

                }

            }

            return Vector2.Zero;

        }

        public override bool EventActive()
        {

            if (!eventLocked)
            {

                if (!Mod.instance.RiteButtonHeld())
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

                Game1.flashAlpha = 1f;

                location.playSound("thunder");

                WispArray();

                activeLimit = 120;

                eventCounter = 0;

                eventLocked = true;

                Mod.instance.rite.castLevel = 0;

            }

        }

        public virtual void WispArray()
        {

            wispCounter = Mod.instance.randomIndex.Next(8);

            List<Vector2> wispVectors = new()
            {

                AddWisps(wispCounter),

                AddWisps((wispCounter + 2) % 8),

                AddWisps((wispCounter + 4) % 8),

                AddWisps((wispCounter + 6) % 8),

                AddWisps((wispCounter + 1) % 8, 384),

                AddWisps((wispCounter + 3) % 8, 384),

                AddWisps((wispCounter + 5) % 8, 384),

                AddWisps((wispCounter + 7) % 8, 384),

            };

        }

    }

}
