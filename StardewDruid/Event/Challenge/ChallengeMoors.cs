using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.GameData.WorldMaps;
using StardewValley.Objects;
using System.Collections.Generic;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeMoors : EventHandle
    {

        public ChallengeMoors()
        {

            activeLimit = -1;

            mainEvent = true;

            locales = new() { LocationData.druid_moors_name, };

        }

        public override void EventSetup(string id)
        {
            
            base.EventSetup(id);

        }

        public override bool TriggerActive()
        {

            if (TriggerLocation())
            {

                EventActivate();

            }

            return false;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            MonsterSetup();

        }

        public void MonsterSetup()
        {

            monsterHandle = new(origin,location);

            monsterHandle.spawnSchedule = new();

            for(int i = 0; i < 50; i++)
            {

                switch (Mod.instance.randomIndex.Next(3))
                {
                    default:
                        monsterHandle.spawnSchedule.Add(i, new()
                        {

                            new(MonsterHandle.bosses.shadowwolf, Boss.temperment.random, Boss.difficulty.medium),

                        });
                        break;

                    case 2:
                        monsterHandle.spawnSchedule.Add(i, new()
                        {

                            new(MonsterHandle.bosses.shadowbear, Boss.temperment.random, Boss.difficulty.medium),

                        });
                        break;

                }

            }

            monsterHandle.spawnLimit = 15;

            monsterHandle.spawnWithin = ModUtility.PositionToTile(Game1.player.Position);

            monsterHandle.spawnRange = new(50,32);

            monsterHandle.spawnGroup = true;

        }

        public override bool AttemptReset()
        {

            if (!eventActive)
            {

                return true;

            }

            if (location.Name == Location.LocationData.druid_moors_name)
            {

                Game1.player.warpFarmer(location.warps[0], 2);

                DialogueCue(900);

            }

            EventRemove();

            eventActive = false;

            triggerEvent = true;

            return true;

        }

        public override bool EventExpire()
        {

            return AttemptReset();

        }


        public override void EventInterval()
        {

            monsterHandle.SpawnCheck();

            monsterHandle.spawnWithin = ModUtility.PositionToTile(Game1.player.Position) - new Vector2(25,16);

            monsterHandle.SpawnInterval();

            if(monsterHandle.spawnTotal == 50 && monsterHandle.monsterSpawns.Count <= 5)
            {

                eventComplete = true;

            }

        }

    }

}
