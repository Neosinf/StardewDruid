using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.IO;

namespace StardewDruid.Cast.Ether
{
    public class ChaosEvent : EventHandle
    {

        public int decimalCounter;

        public StardewValley.Monsters.Monster victim;

        public BarrageHandle barrage;

        public ChaosEvent(Vector2 target, StardewValley.Monsters.Monster Monster)
          : base(target)
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 2.0;

            victim = Monster;

        }

        public override void EventTrigger()
        {
            
            Mod.instance.RegisterEvent(this, "chaos");

        }

        public override bool EventActive()
        {
            if (!ModUtility.MonsterVitals(victim, targetLocation))
            {

                return false;

            }

            return base.EventActive();
        }

        public override void EventDecimal()
        {
            
            if (!EventActive()) { return; }

            decimalCounter++;

            if(decimalCounter == 1)
            {

                Vector2 victimTile = victim.Tile;

                barrage = new(targetLocation, victimTile, Vector2.Zero, 2, 1, -1, Mod.instance.DamageLevel(), 4);

                barrage.type = BarrageHandle.barrageType.chaos;

                barrage.originPosition = Game1.player.Position;

                barrage.LaunchChaos();

            }

            if(decimalCounter == 3)
            {

                barrage.LightRadius();

            }

            if(decimalCounter == 4)
            {

                barrage.RadialDamage();

                barrage.RadialImpact(0,3);

            }

            if(decimalCounter == 8)
            {

                barrage.Shutdown();

            }


        }

    }

}
