using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;

namespace StardewDruid.Event.World
{
    public class Wildspawn : EventHandle
    {

        public Wildspawn(Vector2 target, Rite rite)
            : base(target, rite)
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 300;

        }

        public override void EventTrigger()
        {

            monsterHandle = new(targetVector, riteData.castLocation);

            Mod.instance.RegisterEvent(this, "wildspawn");

        }

        public void SpawnMonster(GameLocation location, Vector2 vector, List<int> spawnIndex, string terrain = "ground", bool precision = false)
        {

            monsterHandle.spawnIndex = spawnIndex;

            if (terrain == "ground")
            {

                monsterHandle.SpawnGround(vector);

            }
            else
            {

                monsterHandle.TargetToPlayer(vector, precision);

                Vector2 spawnVector = monsterHandle.SpawnVector();

                if (spawnVector != new Vector2(-1))
                {

                    monsterHandle.SpawnTerrain(spawnVector, vector, (terrain == "water"));

                }

            }

        }

        public override void EventInterval()
        {
            
            monsterHandle.SpawnCheck();

        }

    }

}
