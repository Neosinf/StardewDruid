// Decompiled with JetBrains decompiler
// Type: StardewDruid.Character.TrackHandle
// Assembly: StardewDruid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24DA4344-683E-4959-87A6-C0A858BCC7DA
// Assembly location: C:\Users\piers\source\repos\StardewDruid\StardewDruid\bin\Debug\net5.0\StardewDruid.dll

using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Collections.Generic;

#nullable disable
namespace StardewDruid.Character
{
    public class TrackHandle
    {
        public string trackFor;
        
        public string trackLocation;
        
        public Vector2 trackPlayer;
        
        public List<Vector2> trackVectors;
        
        public int trackLimit;

        public TrackHandle(string For)
        {
            
            trackVectors = new List<Vector2>();
            
            trackPlayer = new Vector2(-99f);
            
            trackLimit = 24;
           
            trackFor = For;

        }

        public void TrackPlayer()
        {
            
            if (trackLocation != Game1.player.currentLocation.Name)
            {
                trackLocation = Game1.player.currentLocation.Name;
                
                trackPlayer = new Vector2(-99f);
                
                trackVectors.Clear();
            
            }
            
            Vector2 position = Game1.player.Position;

            if(ModUtility.GroundCheck(Game1.player.currentLocation, Game1.player.getTileLocation()) == "water")
            {
                return;
            }
            
            if ((double)Vector2.Distance(position, trackPlayer) >= 64.0)
            {
                
                trackPlayer = position;
                
                trackVectors.Add(position);
                
                if (trackVectors.Count >= trackLimit)
                {
                    
                    trackVectors.RemoveAt(0);
                
                }
            
            }
            
            if (Mod.instance.characters[trackFor].currentLocation.Name == Game1.player.currentLocation.Name || trackVectors.Count < 3)
            {
                
                return;

            }
            
            Mod.instance.characters[trackFor].WarpToTarget();
        
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

        public Vector2 NextVector()
        {
            
            Vector2 trackVector = trackVectors[0];
            
            trackVectors.RemoveAt(0);
            
            return trackVector;
        
        }
    
    }

}
