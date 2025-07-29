using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;


namespace StardewDruid.Cast.Ether
{
    public class Stomping : EventHandle
    {

        public int radialCounter = 0;

        public Stomping()
        {

        }

        public override void EventDecimal()
        {

            Stomp();

            radialCounter++;

            if (radialCounter == 4)
            {

                eventComplete = true;

            }

        }

        public void Stomp()
        {

            List<Vector2> affected = ModUtility.GetTilesWithinRadius(location, ModUtility.PositionToTile(origin), radialCounter);

            foreach (Vector2 tile in affected)
            {

                if (!location.terrainFeatures.ContainsKey(tile))
                {

                    continue;

                }

                if (location.terrainFeatures[tile] is StardewValley.TerrainFeatures.HoeDirt hoeDirt)
                {
                        
                    if (hoeDirt.crop != null)
                    {

                        continue;

                    }

                    if (hoeDirt.HasFertilizer())
                    {

                        continue;

                    }

                    location.terrainFeatures.Remove(tile);

                }

            }

        }

    }

}
