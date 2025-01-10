using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Xml.Linq;

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

        public int trackQuadrant;

        public int linger;

        public Vector2 lingerSpot;

        public bool eventLock;

        public bool suspended;

        public TrackHandle(CharacterHandle.characters For, Farmer follow = null, int quadrant = 0)
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

            trackQuadrant = quadrant;

        }

        public StardewDruid.Character.Character TrackSubject()
        {

            if (!Context.IsMainPlayer)
            {

                return Mod.instance.dopplegangers[trackFor];

            }

            return Mod.instance.characters[trackFor];

        }

        public Vector2 TrackPosition()
        {

            if(linger > 0)
            {

                return lingerSpot;

            }

            return followPlayer.Position;

        }

        public void TrackPlayer(int Offset)
        {

            StardewDruid.Character.Character subject = TrackSubject();

            if (subject == null)
            {

                return;

            }

            if (subject is Recruit villager)
            {


                if (villager.currentLocation == null)
                {

                    villager.currentLocation = Game1.player.currentLocation;

                    suspended = true;

                }

                if (villager.TrackNotReady())
                {

                    if (!suspended)
                    {

                        subject.currentLocation.characters.Remove(subject);

                        suspended = true;

                        string notReadyMessage = Mod.instance.Helper.Translation.Get("CharacterHandle.363.1").Tokens(new { name = subject.displayName });

                        Mod.instance.CastMessage(notReadyMessage);

                    }

                    return;

                }

                if (villager.TrackOutOfTime())
                {

                    if (!suspended)
                    {

                        subject.currentLocation.characters.Remove(subject);

                        suspended = true;

                        string outOfTimeMessage = Mod.instance.Helper.Translation.Get("CharacterHandle.363.2").Tokens(new { name = subject.displayName });

                        Mod.instance.CastMessage(outOfTimeMessage);

                    }

                    return;

                }

                if (villager.villager.currentLocation.Name == followPlayer.currentLocation.Name)
                {

                    if (!suspended)
                    {

                        subject.currentLocation.characters.Remove(subject);

                        suspended = true;

                        Mod.instance.CastMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.361.4").Tokens(new { name = subject.displayName, }), 0, true);
                    }

                    return;

                }

                if (suspended)
                {

                    suspended = false;

                    Mod.instance.CastMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.361.3").Tokens(new { name = subject.displayName, }), 0, true);

                    if (subject.currentLocation != null)
                    {

                        subject.currentLocation.characters.Add(subject);

                    }

                }

            }

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

            if (subject.currentLocation == null)
            {
                
                WarpToPlayer();

                nodes.Clear();

                warpDelay = 8 + (4 * trackOffset);

                return;

            }

            if (subject.currentLocation.Name != followPlayer.currentLocation.Name)
            {

                if (subject.netSceneActive.Value)
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

            if(linger > 0)
            {

                linger--;

                return;

            }

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
                {

                    direction = ModUtility.DirectionToTarget(followPlayer.Position, warpSpot)[2];

                }

            }

            if (WarpToPlayer(direction))
            {

                linger = 0;

                lingerSpot = Vector2.Zero;

                return true;

            }

            return false;

        }

        public bool WarpToPlayer(int direction = -1)
        {

            StardewDruid.Character.Character subject = TrackSubject();

            if (subject == null)
            {

                return false;

            }

            if (warpDelay > 0)
            {

                return false;

            }

            if (direction == -1 && nodes.Count > 0)
            {

                direction = ModUtility.DirectionToTarget(followPlayer.Position, nodes.Keys.Last())[2];

            }

            if (direction == -1 && trackQuadrant == 0)
            {

                // try for center

                direction = (ModUtility.DirectionToCenter(followPlayer.currentLocation, followPlayer.Position)[2] + 4) % 8;

            }
            else
            {

                direction = trackQuadrant;

            }

            // get player tile

            Vector2 center = ModUtility.PositionToTile(followPlayer.Position);

            // get occupiable tiles

            List<Vector2> options = ModUtility.GetOccupiableTilesNearby(followPlayer.currentLocation, center, direction, 1, 2);

            // check who else might warp there

            List<Vector2> occupied = new();

            foreach (KeyValuePair<CharacterHandle.characters, StardewDruid.Character.Character> friends in Mod.instance.characters)
            {

                if (friends.Key == trackFor) { continue; }

                if (friends.Value is Actor) { continue; }

                if (friends.Value.currentLocation == null) { continue; }

                if (friends.Value.currentLocation.Name != followPlayer.currentLocation.Name) { continue; }

                occupied.Add(friends.Value.occupied);

            }

            foreach (KeyValuePair<CharacterHandle.characters, StardewDruid.Character.Character> friends in Mod.instance.dopplegangers)
            {

                if (friends.Key == trackFor) { continue; }

                if (friends.Value is Actor) { continue; }

                if (friends.Value.currentLocation == null) { continue; }

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

                    CharacterMover mover = new(subject, followPlayer.currentLocation, warppoint * 64, true);

                    mover.warp = subject.warpDisplay;

                    Mod.instance.movers[trackFor] = mover;

                    warpDelay = 30;

                    nodes.Clear();

                    return true;

                }

            }

            return false;

        }

        public Dictionary<Vector2,int> NodesToTraversal()
        {

            StardewDruid.Character.Character subject = TrackSubject();

            if (subject == null)
            {

                return new();

            }
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

            Vector2 origin = subject.occupied;
            
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

            return ModUtility.PathsToTraversal(subject.currentLocation, paths, valids, 2);

        }

    }

}
