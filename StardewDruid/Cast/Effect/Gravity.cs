using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewValley;
using StardewValley.Characters;
using StardewValley.GameData.Crops;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StardewDruid.Cast.Effect
{
    public class Gravity : EventHandle
    {


        public Dictionary<Vector2, GravityTarget> gravityWells = new();

        public Gravity()
        {

        }

        public virtual void AddTarget(GameLocation location, Vector2 tile, int timer, float radius = 256)
        {

            if (gravityWells.ContainsKey(tile))
            {
                return;
            }

            gravityWells.Add(tile, new(location, tile, timer, radius));

        }

        public override void EventDecimal()
        {

            if (gravityWells.Count == 0)
            {

                eventComplete = true;

                return;

            }

            List<StardewValley.Monsters.Monster> procced = new();

            for (int g = gravityWells.Count - 1; g >= 0; g--)
            {

                KeyValuePair<Vector2, GravityTarget> gravityWell = gravityWells.ElementAt(g);

                if ((gravityWell.Value.counter <= 0))
                {

                    gravityWells.Remove(gravityWell.Key);

                }

                gravityWell.Value.counter--;

                Vector2 gravityCenter = gravityWell.Value.tile * 64;

                List<StardewValley.Monsters.Monster> victims = ModUtility.MonsterProximity(gravityWell.Value.location, gravityCenter, gravityWell.Value.radius, true);

                foreach(StardewValley.Monsters.Monster victim in victims)
                {

                    if(procced.Contains(victim)) 
                    { 
                        
                        continue; 
                    
                    }

                    procced.Add(victim);

                    if (victim.xVelocity > 0 || victim.yVelocity > 0)
                    {
                        
                        continue;

                    }

                    if (victim is StardewDruid.Monster.Boss)
                    {

                        if(gravityWell.Value.counter == 30)
                        {

                            victim.Halt();

                        }

                    } 
                    else if(victim.stunTime.Value <= 0)
                    {
                        
                        victim.stunTime.Set(1000);
                    
                    }

                    Vector2 attemptPosition = ModUtility.PathMovement(victim.Position, gravityCenter, 7);

                    if(ModUtility.TileAccessibility(location,ModUtility.PositionToTile(attemptPosition)) == 0 || victim.isGlider.Value)
                    {

                        victim.Position = attemptPosition;

                    }

                }

            }

        }

    }

    public class GravityTarget
    {

        public Vector2 tile;

        public GameLocation location;

        public int counter;

        public int limit;

        public float radius;

        public GravityTarget(GameLocation Location, Vector2 Tile, int timer, float Radius = 256)
        {

            tile = Tile;

            counter = timer * 10;

            location = Location;

            radius = Radius;

        }

    }

}
