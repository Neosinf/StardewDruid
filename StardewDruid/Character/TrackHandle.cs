using Microsoft.Xna.Framework;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Xml.Linq;

namespace StardewDruid.Character
{
    public class TrackHandle
    {
        public string trackFor;
        
        public string trackLocation;

        public Farmer followPlayer;
        
        public Vector2 trackPlayer;
        
        public List<Vector2> trackVectors;
        
        public int trackLimit;

        public bool standby;

        public Vector2 trackOffset;

        public Vector2 trackDelay;

        public TrackHandle(string For, Farmer follow = null)
        {
            
            if(follow == null)
            {
                
                followPlayer = Game1.player;

            }
            else
            {

                followPlayer = follow;

            }

            trackVectors = new List<Vector2>();
            
            trackPlayer = new Vector2(-1);
            
            trackLimit = 24;
           
            trackFor = For;

        }

        public void TrackPlayer()
        {

            if (trackLocation != followPlayer.currentLocation.Name)
            {

                trackLocation = followPlayer.currentLocation.Name;
                
                trackPlayer = new Vector2(-1);
                
                trackVectors.Clear();
            
            }

            if (followPlayer.currentLocation is FarmHouse || followPlayer.currentLocation is IslandFarmHouse)
            {

                return;

            }

            Vector2 position = followPlayer.Position;

            if(ModUtility.GroundCheck(followPlayer.currentLocation, followPlayer.Tile) == "water")
            {
                
                return;
            
            }

            int offset = 0;

            foreach (KeyValuePair<string, TrackHandle> tracker in Mod.instance.trackRegister)
            {

                if (tracker.Key == trackFor)
                {

                    break;

                }

                offset++;

            }

            if ((double)Vector2.Distance(position, trackPlayer) >= 128.0)
            {

                if(trackPlayer.X >= 0)
                {

                    trackVectors.Add(trackPlayer);

                    if (trackVectors.Count >= trackLimit)
                    {

                        trackVectors.RemoveAt(0);

                    }

                }

                trackPlayer = position + (new Vector2(32,32) * offset);

            }

            if (trackVectors.Count < (1 + offset))
            {

                return;

            }

            if(trackVectors.Count > 6)
            {

                if(Vector2.Distance(trackVectors.First(),trackVectors.Last()) < 192)
                {

                    TruncateTo(3);

                }

            }

            if (Mod.instance.characters[trackFor].netSceneActive.Value)
            {

                return;

            }

            if(trackVectors.Count == 0)
            {

                return;

            }

            if (Mod.instance.characters[trackFor].currentLocation.Name != followPlayer.currentLocation.Name)
            {

                if (WarpToTarget()) { return; }

            }

            /*if (!Utility.isOnScreen(Mod.instance.characters[trackFor].Position, 128))
            {

                if (WarpToTarget()) { return; }

            }

            if(Vector2.Distance(Mod.instance.characters[trackFor].Position, followPlayer.Position) > 800f)
            {

                if (WarpToTarget()) { return; }

            }*/

        }

        public bool WarpToTarget(bool next = true)
        {

            if(trackVectors.Count == 0)
            {
                return false;
            }

            if (Mod.instance.characters[trackFor].currentLocation.Name != followPlayer.currentLocation.Name)
            {

                Mod.instance.characters[trackFor].currentLocation.characters.Remove(Mod.instance.characters[trackFor]);

                Mod.instance.characters[trackFor].currentLocation = followPlayer.currentLocation;

                Mod.instance.characters[trackFor].currentLocation.characters.Add(Mod.instance.characters[trackFor]);

            }

            Vector2 warppoint = WarpData.WarpStart(Mod.instance.characters[trackFor].currentLocation.Name);

            if (warppoint.X < 0)
            {
                
                warppoint = next ? NextVector() : LastVector();

            }

            Mod.instance.characters[trackFor].Position = warppoint;

            ModUtility.AnimateQuickWarp(Mod.instance.characters[trackFor].currentLocation, Mod.instance.characters[trackFor].Position - new Vector2(0, 32));

            Mod.instance.characters[trackFor].DeactivateStandby();

            Mod.instance.characters[trackFor].ResetActives();

            return true;

        }


        public void TruncateTo(int requirement)
        {
            
            int num = Math.Min(requirement, trackVectors.Count);
            
            List<Vector2> vector2List = new List<Vector2>();
            
            for (int index = trackVectors.Count - num; index < trackVectors.Count; ++index)
            {
                
                vector2List.Add(trackVectors[index]);
            
            }
            
            trackVectors = vector2List;

        }

        public Vector2 ClosestVector(Vector2 origin)
        {

            if (trackVectors.Count == 0)
            {
            
                return Vector2.Zero;
            
            }

            if (trackVectors.Count == 1)
            {

                return NextVector();

            }

            int closest = 0;

            float best = Vector2.Distance(origin,trackVectors.First());

            for(int i = 1; i < trackVectors.Count; i++)
            {

                float distance = Vector2.Distance(trackVectors.ElementAt(i), origin);

                if (distance < best)
                {

                    closest = i;

                }

            }

            int truncate = trackVectors.Count - closest;

            TruncateTo(truncate);

            return NextVector();

        }

        public Vector2 NextVector()
        {
            
            if (trackVectors.Count == 0)
            {
                return Vector2.Zero;
            }

            Vector2 trackVector = trackVectors.First();
            
            trackVectors.RemoveAt(0);

            return trackVector;
        
        }

        public Vector2 LastVector()
        {

            if (trackVectors.Count == 0)
            {
                return Vector2.Zero;
            }

            Vector2 trackVector = trackVectors.Last();

            trackVectors.Clear();

            return trackVector;

        }
    
    }

}
