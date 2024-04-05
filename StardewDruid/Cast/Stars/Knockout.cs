using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Companions;
using StardewValley.Locations;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StardewDruid.Cast.Stars
{
    public class Knockout : EventHandle
    {

        Dictionary<StardewValley.Monsters.Monster,float> victims;

        public Knockout()
          : base(Vector2.Zero)
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 5.0;

            victims = new();

        }

        public override void EventTrigger()
        {

            Mod.instance.RegisterEvent(this, "knockout");


        }

        public void AddVictim(StardewValley.Monsters.Monster monster, int knock = 2000)
        {

            if (MonsterData.BossMonster(monster))
            {
                
                monster.stunTime.Set(Math.Max(monster.stunTime.Value, 500));
                
                return;
            
            }

            monster.Halt();
            
            monster.stunTime.Set(Math.Max(monster.stunTime.Value, knock));

            if (monster.isGlider.Value)
            {

                return;

            }

            if (victims.ContainsKey(monster))
            {

                return;

            }

            victims.Add(monster, monster.rotation);

            if (monster.Sprite.getWidth() > 16)
            {

                monster.rotation = (float)(Math.PI);

            }
            else
            {

                monster.rotation = (float)(Math.PI / 2);

            }

        }

        public void ReleaseVictims()
        {

            for(int m = victims.Count-1; m >= 0; m--)
            {

                KeyValuePair<StardewValley.Monsters.Monster,float> victim = victims.ElementAt(m);

                if (!ModUtility.MonsterVitals(victim.Key, victim.Key.currentLocation))
                {
                    continue;
                }

                victim.Key.rotation = victim.Value;

            }

            victims.Clear();

        }

        public override void EventRemove()
        {

            ReleaseVictims();

            base.EventRemove();
        }

        public override void EventDecimal()
        {
            
            for (int m = victims.Count - 1; m >= 0; m--)
            {

                KeyValuePair<StardewValley.Monsters.Monster, float> victim = victims.ElementAt(m);

                if (!ModUtility.MonsterVitals(victim.Key, victim.Key.currentLocation))
                {

                    victims.Remove(victim.Key);

                    continue;
                }

                if(victim.Key.stunTime.Value <= 0)
                {
                    victim.Key.rotation = victim.Value;

                    victims.Remove(victim.Key);

                    continue;

                }

                victim.Key.Halt();

            }

        }

    }

}
