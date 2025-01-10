using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.GameData.WorldMaps;
using StardewValley.Objects;
using System.Collections.Generic;

namespace StardewDruid.Event.Threat
{
    public class ThreatMoors : EventHandle
    {

        public ThreatMoors()
        {

            activeLimit = -1;

            locales = new() { LocationHandle.druid_moors_name, };

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

            monsterHandle = new(origin, location);

            monsterHandle.spawnCombat *= 5 / 4;

            monsterHandle.spawnRange = new(6, 6);

            monsterHandle.spawnGroup = true;

            for (int i = 0; i < 5; i++)
            {

                if(location is Moors moors)
                {

                    TerrainField spawn = moors.terrainFields[Mod.instance.randomIndex.Next(moors.terrainFields.Count)];

                    monsterHandle.spawnWithin = ModUtility.PositionToTile(spawn.position);

                    switch (Mod.instance.randomIndex.Next(3))
                    {
                        default:

                            monsterHandle.SpawnGround(ModUtility.PositionToTile(spawn.position), new(MonsterHandle.bosses.shadowwolf, Boss.temperment.random, Boss.difficulty.medium));

                            break;

                        case 2:

                            monsterHandle.SpawnGround(ModUtility.PositionToTile(spawn.position), new(MonsterHandle.bosses.shadowbear, Boss.temperment.random, Boss.difficulty.medium));

                            break;

                    }

                }

            }

        }

        public override bool AttemptReset()
        {

            if (!eventActive)
            {

                return true;

            }

            EventRemove();

            if(Game1.player.currentLocation.Name != location.Name)
            {

                eventActive = false;

                triggerEvent = true;

            }

            return true;

        }

        public override bool EventExpire()
        {

            return AttemptReset();

        }

        public override void EventInterval()
        {

            activeCounter++;

            monsterHandle.SpawnCheck();

            if (monsterHandle.monsterSpawns.Count <= 0)
            {

                eventComplete = true;

            }

        }

    }

}
