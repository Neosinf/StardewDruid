using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static StardewValley.Minigames.TargetGame;


namespace StardewDruid.Character
{
    public class TrackHandle
    {
        public CharacterHandle.characters trackFor;
        
        public string trackLocation;

        public Farmer followPlayer;

        public Dictionary<Vector2, int> nodes = new();

        public bool standby;

        public int warpDelay;

        public Vector2 warpSpot;

        public int trackOffset;

        public TrackHandle(CharacterHandle.characters For, Farmer follow = null)
        {
            
            if(follow == null)
            {
                
                followPlayer = Game1.player;

            }
            else
            {

                followPlayer = follow;

            }

            trackFor = For;

        }

        public void TrackPlayer(int Offset)
        {

            trackOffset = Offset;

            if (trackLocation != followPlayer.currentLocation.Name)
            {

                trackLocation = followPlayer.currentLocation.Name;

                nodes.Clear();

            }

            if (followPlayer.currentLocation is FarmHouse || followPlayer.currentLocation is IslandFarmHouse)
            {

                return;

            }

            if (warpDelay > 0)
            {

                warpDelay--;

            }

            if (Mod.instance.characters[trackFor].currentLocation.Name != followPlayer.currentLocation.Name)
            {

                if (Mod.instance.characters[trackFor].netSceneActive.Value)
                {

                    return;

                }

                if(warpDelay > 0)
                {

                    return;

                }

                // Player is not on the same map now

                if (WarpToLocation())
                {

                    nodes.Clear();

                    return; 
                
                }

                // attempt warp every second

                warpDelay = 8 + (4 * trackOffset);

                return;

            }

            warpSpot = Vector2.Zero;

            Vector2 center = new Vector2((int)(followPlayer.Position.X / 64), (int)(followPlayer.Position.Y / 64));

            int access = ModUtility.TileAccessibility(followPlayer.currentLocation, center);

            if (access == 2)
            {

                // Path is broken, possibly due to player warping, so will need to warp to player

                nodes.Clear();

                return;
            
            }

            if (nodes.Count == 0)
            {

                if(access == 1)
                {
                    
                    // can't start path on a jump point

                    return;

                }

                nodes.Add(center,access);

                return;

            }

            if (center != nodes.Keys.Last())
            {

                if (nodes.ContainsKey(center))
                {

                    // path doubles back

                    nodes.Clear();

                    nodes.Add(center, access);

                    return;

                }

                // building a legitimate route to player

                nodes.Add(center, access);

            }

        }

        public bool WarpToLocation()
        {

            int direction = -1;

            if(Game1.xLocationAfterWarp > 0 || warpSpot != Vector2.Zero)
            {
                
                if (Game1.xLocationAfterWarp > 0)
                {
                
                    warpSpot = new(Game1.xLocationAfterWarp * 64, Game1.yLocationAfterWarp * 64);
                
                }

                float afterSpace = Vector2.Distance(followPlayer.Position, warpSpot);

                // player is still on warp
                if(afterSpace <= 64)
                {

                    return false;

                }
                else
                //if (afterSpace >= 256)
                {

                    //warp between player and warp point
                    direction = ModUtility.DirectionToTarget(followPlayer.Position, warpSpot)[2];

                }
                //else
                //{
                    
                //    direction = ModUtility.DirectionToTarget(followPlayer.Position, warpSpot)[2];
                    
                    // warp to other side of warp point
                 //   direction = (direction + 4) % 8;

                //}

            }

            if (WarpToPlayer(direction))
            {

                return true;

            }

            return false;

        }

        public bool WarpToPlayer(int direction = -1)
        {

            if(warpDelay > 0)
            {

                return false;

            }

            if (direction == -1 && nodes.Count > 0)
            {

                direction = ModUtility.DirectionToTarget(followPlayer.Position, nodes.Keys.Last())[2];

            }

            if (direction == -1)
            {

                // try for center

                direction = (ModUtility.DirectionToCenter(followPlayer.currentLocation, followPlayer.Position)[2] + 4) % 8;

            }

            // get player tile

            Vector2 center = ModUtility.PositionToTile(followPlayer.Position);// new Vector2((int)(followPlayer.Position.X / 64), (int)(followPlayer.Position.Y / 64));

            // get occupiable tiles

            List<Vector2> options = ModUtility.GetOccupiableTilesNearby(followPlayer.currentLocation, center, direction, 1, 2);

            // check who else might warp there

            List<Vector2> occupied = new();

            foreach (KeyValuePair<CharacterHandle.characters, StardewDruid.Character.Character> friends in Mod.instance.characters)
            {

                if (friends.Key == trackFor) { continue; }

                if (friends.Value is Actor) { continue; }

                if (friends.Value.currentLocation.Name != followPlayer.currentLocation.Name) { continue; }

                occupied.Add(friends.Value.occupied);

            }

            // if options available

            if (options.Count > 0)
            {

                foreach (Vector2 warppoint in options)
                {

                    // avoid if another character got there first

                    if (occupied.Contains(warppoint)) { continue; }

                    //Mod.instance.characters[trackFor].Position = warppoint*64;

                    //Mod.instance.characters[trackFor].occupied = warppoint;

                    CharacterMover mover = new(trackFor);

                    mover.WarpSet(followPlayer.currentLocation.Name, warppoint * 64, true);

                    nodes.Clear();

                    Mod.instance.movers[trackFor] = mover;

                    Mod.instance.characters[trackFor].attentionTimer = 360;

                    warpDelay = 30;

                    return true;

                }

            }

            return false;

        }


        public Dictionary<Vector2,int> NodesToTraversal()
        {

            // first get the closest vector on path from termination

            Dictionary<Vector2,int> valids = new();

            for(int i = 0; i < nodes.Count - trackOffset; i++)
            {

                KeyValuePair<Vector2, int> node = nodes.ElementAt(i);

                valids[node.Key] = node.Value;

            }

            if (valids.Count == 0)
            {

                return new();

            }

            List<Vector2> paths = new() { valids.Keys.Last(), };

            Vector2 origin = Mod.instance.characters[trackFor].occupied;

            int direct = ModUtility.DirectionToTarget(origin, valids.Keys.Last())[2];

            List<int> accept = new()
            {
                direct,
                (direct + 1) % 8,
                (direct + 7) % 8,
            };

            float threshold = Vector2.Distance(origin, valids.Keys.Last());

            if (valids.Count > 1)
            {
                
                for (int n = valids.Count - 2; n >= 0; n--)
                {

                    KeyValuePair<Vector2, int> node = valids.ElementAt(n);

                    float closer = Vector2.Distance(origin, node.Key);

                    int way = ModUtility.DirectionToTarget(origin, node.Key)[2];

                    if (!accept.Contains(way))
                    {

                        break;

                    }

                    if (closer > threshold)
                    {

                        break;

                    }

                    paths.Prepend(node.Key);

                }

            }

            return ModUtility.PathsToTraversal(Mod.instance.characters[trackFor].currentLocation, paths, valids, 2);

        }

    }

}
