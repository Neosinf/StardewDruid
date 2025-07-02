using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Ether;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Location.Druid;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Location
{

    public static class LocationHandle
    {

        public const string druid_grove_name = "18465_Grove";

        public const string druid_sanctuary_name = "18465_Sanctuary";

        public const string druid_temple_name = "18465_Temple";

        public const string druid_cavern_name = "18465_Cavern";

        public const string druid_limbo = "18465_Limbo";

        public const string druid_spring_name = "18465_Spring";

        public const string druid_distillery_name = "18465_Distillery";

        public const string druid_atoll_name = "18465_Atoll";

        public const string druid_graveyard_name = "18465_Graveyard";

        public const string druid_clearing_name = "18465_Clearing";

        public const string druid_chapel_name = "18465_Chapel";

        public const string druid_lair_name = "18465_Lair";

        public const string druid_tunnel_name = "18465_Tunnel";

        public const string druid_court_name = "18465_Court";

        public const string druid_archaeum_name = "18465_Archaeum";

        public const string druid_tomb_name = "18465_Tomb";

        public const string druid_engineum_name = "18465_Engineum";

        //public const string druid_gate_name = "18465_Gate";

        public const string druid_moors_name = "18465_Moors";

        public static void DruidLocations(string map)
        {

            if (Mod.instance.locations.ContainsKey(map))
            {
                
                return;

            }

            GameLocation locale = Game1.getLocationFromName(map);

            if (locale != null)
            {

                Mod.instance.locations[map] = locale;

                return;
            
            }

            GameLocation location;

            switch (map)
            {
                default:
                case druid_grove_name:

                    location = new Location.Druid.Grove(map);

                    break;

                case druid_cavern_name:

                    location = new Location.Druid.Cavern(map);

                    break;

                case druid_sanctuary_name:

                    location = new Location.Druid.Sanctuary(map);

                    break;

                case druid_temple_name:

                    location = new Location.Druid.Temple(map);

                    break;

                case druid_spring_name:

                    location = new Location.Druid.Spring(map);

                    break;

                case druid_distillery_name:

                    location = new Location.Druid.Distillery(map);

                    break;

                case druid_atoll_name:

                    location = new Location.Druid.Atoll(map);

                    break;

                case druid_graveyard_name:

                    location = new Location.Druid.Graveyard(map);

                    break;

                case druid_clearing_name:

                    location = new Location.Druid.Clearing(map);

                    break;

                case druid_chapel_name:

                    location = new Location.Druid.Chapel(map);

                    break;

                case druid_lair_name:

                    location = new Location.Druid.Lair(map);

                    break;

                case druid_tunnel_name:

                    location = new Location.Druid.Tunnel(map);

                    break;

                case druid_court_name:

                    location = new Location.Druid.Court(map);

                    break;

                case druid_archaeum_name:

                    location = new Location.Druid.Archaeum(map);

                    break;

                case druid_tomb_name:

                    location = new Location.Druid.Tomb(map);

                    break;

                case druid_engineum_name:

                    location = new Location.Druid.Engineum(map);

                    break;

                case druid_moors_name:

                    location = new Location.Druid.Moors(map);

                    break;

            }

            Game1.locations.Add(location);

            Mod.instance.locations.Add(map, location);

        }

        public static int MaxRestoration(string map)
        {

            switch (map)
            {

                case druid_spring_name:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeWeald))
                    {
                        
                        return 3;

                    }

                    break;

                case druid_graveyard_name:

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.challengeMists))
                    {
                        
                        return 4;
                    
                    }

                    break;

                case druid_clearing_name:

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.challengeStars))
                    {
                        
                        return 3;
                    
                    }

                    break;

            }

            return -1;

        }

        public static int GetRestoration(string map)
        {

            switch (map)
            {

                case druid_spring_name:
                case druid_graveyard_name:
                case druid_clearing_name:

                    if (!Mod.instance.save.restoration.ContainsKey(map))
                    {

                        Mod.instance.save.restoration[map] = 0;

                    }

                    (Mod.instance.locations[map] as DruidLocation).RestoreTo(Mod.instance.save.restoration[map]);

                    return Mod.instance.save.restoration[map];

            }

            return 0;

        }

        public static Microsoft.Xna.Framework.Rectangle TerrainRectangles(IconData.tilesheets sheet, int key)
        {

            switch (sheet)
            {

                case IconData.tilesheets.access:

                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 48, 32);

                        case 2:

                            return new(48, 0, 48, 64);

                        case 3:

                            return new(96, 0, 48, 64);

                        case 4:

                            return new(144, 0, 48, 64);

                    }

                    break;

                case IconData.tilesheets.atoll:


                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 32, 128);

                        case 2:

                            return new(32, 0, 32, 96);

                        case 3:

                            return new(64, 0, 48, 96);

                        case 4:

                            return new(112, 0, 32, 128);

                        case 5:

                            return new(160, 0, 96, 192);

                        case 6:

                            return new(160, 192, 96, 64);

                        // stones

                        case 7:

                            return new(32, 96, 16, 48);

                        case 8:

                            return new(48, 96, 32, 48);

                        case 9:

                            return new(80, 112, 32, 32);

                        case 10:

                            return new(112, 128, 48, 48);

                        case 11:

                            return new(112, 176, 48, 64);

                        // debris

                        case 12:

                            return new(0, 144, 112, 112);

                        case 13:

                            return new(0, 256, 112, 96);

                        case 14:

                            return new(112, 256, 128, 80);

                        case 15:

                            return new(112, 336, 48, 32);

                        case 16:

                            return new(160, 336, 32, 32);


                    }

                    break;

                case IconData.tilesheets.graveyard:

                    switch (key)
                    {
                        case 1:

                            return new(0, 0, 16, 48);

                        case 2:

                            return new(16, 0, 16, 48);

                        case 3:

                            return new(32, 0, 16, 48);

                        case 4:

                            return new(48, 0, 16, 48);

                        case 5:

                            return new(64, 0, 64, 48);

                        case 6:

                            return new(128, 0, 16, 64);

                        case 7:

                            return new(144, 0, 96, 48);

                    }

                    break;

                case IconData.tilesheets.chapel:

                    switch (key)
                    {

                        case 1:
                        case 2:
                            return new(80, 0, 48, 240);

                        case 3:

                            return new(128, 64, 96, 48);

                        case 4:

                            return new(128, 0, 32, 48);

                        case 5:

                            return new(144, 0, 64, 48);

                        case 6:

                            return new(192, 0, 32, 48);

                        case 7:

                            return new(128, 128, 64, 64);

                        case 8:

                            return new(0, 128, 64, 96);

                        case 9:

                            return new(0, 0, 32, 64);

                        case 10:

                            return new(0, 240, 64, 48);

                        case 11:

                            return new(64, 240, 64, 48);

                    }

                    break;

                case IconData.tilesheets.clearing:

                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 48, 128);

                        case 2:

                            return new(48, 0, 48, 128);

                        case 3:

                            return new(96, 0, 48, 128);

                        case 4:

                            return new(288, 0, 64, 128);
                    }

                    break;


                case IconData.tilesheets.tomb:

                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 32, 80);

                        case 2:
                            return new(96, 192, 32, 64);

                        case 3:
                            return new(128, 192, 32, 64);

                        // section 1
                        case 4:
                            return new(0, 192, 48, 64);

                        case 5:
                            return new(48, 192, 48, 48);

                        case 6:

                            return new(160, 32, 32, 64);


                    }

                    break;

                case IconData.tilesheets.lair:

                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 32, 80);

                        case 2:

                            return new(32, 32, 80, 48);

                    }

                    break;

                case IconData.tilesheets.engineum:

                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 160, 128);

                        case 2:

                            return new(0, 128, 48, 64);

                        case 3:

                            return new(48, 128, 48, 64);

                        case 5:

                            return new(192, 0, 48, 64);

                        case 6:

                            return new(192, 64, 48, 64);

                        case 7:

                            return new(240, 0, 32, 64);

                        case 8:

                            return new(96, 128, 48, 80);

                    }

                    break;

                case IconData.tilesheets.mushrooms:

                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 32, 32);

                    }

                    break;

            }

            return new(0, 0, 0, 0);

        }

        public static List<Microsoft.Xna.Framework.Vector2> TerrainBases(IconData.tilesheets sheet, int key, Vector2 position, Rectangle source)
        {

            List<Microsoft.Xna.Framework.Vector2> baseTiles = new();

            Vector2 tile = ModUtility.PositionToTile(position);

            int footPrint = 1;

            switch (sheet)
            {

                case IconData.tilesheets.atoll:


                    switch (key)
                    {

                        default:

                            return new();

                        case 6:

                            footPrint = 2;

                            break;

                        // stones
                        case 7:

                            footPrint = 1;

                            break;

                        case 8:
                        case 9:
                        case 10:
                        case 11:

                            footPrint = 2;

                            break;

                        // debris

                        case 12:
                        case 13:
                        case 14:
                        case 16:

                            footPrint = 1;

                            break;
                    }

                    break;

                case IconData.tilesheets.chapel:

                    switch (key)
                    {

                        case 1:

                            return new()
                            {

                                new Vector2(tile.X,tile.Y+12),
                                new Vector2(tile.X+1,tile.Y+12),

                                new Vector2(tile.X,tile.Y+13),
                                new Vector2(tile.X+1,tile.Y+13),

                                new Vector2(tile.X,tile.Y+14),
                                new Vector2(tile.X+1,tile.Y+14),


                            };

                        case 2:

                            return new()
                            {

                                new Vector2(tile.X+1,tile.Y+12),
                                new Vector2(tile.X+2,tile.Y+12),

                                new Vector2(tile.X+1,tile.Y+13),
                                new Vector2(tile.X+2,tile.Y+13),

                                new Vector2(tile.X+1,tile.Y+14),
                                new Vector2(tile.X+2,tile.Y+14),


                            };

                        case 3:
                        case 10:
                        case 11:

                            footPrint = 2;

                            break;

                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:

                            return new();

                    }

                    break;

                case IconData.tilesheets.tomb:

                    break;


                case IconData.tilesheets.engineum:

                    switch (key)
                    {

                        default:

                            footPrint = 2;

                            break;

                        case 7:

                            footPrint = 3;

                            break;

                        case 8:

                            footPrint = 4;

                            break;
                    }

                    break;


                default:

                    break;

            }

            Vector2 range = new Vector2(source.Width / 16, source.Height / 16);

            for(int y = 1; y <= footPrint; y++)
            {
                
                for (int x = 0; x < (int)range.X; x++)
                {

                    baseTiles.Add(new Vector2(tile.X + x, tile.Y + ((int)range.Y - y)));

                }

            }

            return baseTiles;

        }

        public static float TerrainLayers(IconData.tilesheets sheet, int key, Vector2 position, Rectangle source)
        {

            List<Microsoft.Xna.Framework.Vector2> baseTiles = new();

            switch (sheet)
            {


                case IconData.tilesheets.atoll:

                    switch (key)
                    {
                        case 5:

                            return (position.Y + (source.Height * 4) + 256) / 10000;


                    }

                    break;

                case IconData.tilesheets.spring:

                    switch (key)
                    {

                        case 1:

                            return (position.Y + 512) / 10000;

                        case 15:
                        case 16:

                            return (position.Y + 224) / 10000;

                    }

                    break;

                case IconData.tilesheets.tomb:

                    switch (key)
                    {

                        case 2:
                        case 3:

                            return ((position.Y + (source.Height * 4)) / 10000) + 0.0001f;
                    
                    }

                    break;

            }

            return (position.Y - 32 + (source.Height * 4)) / 10000;

        }


        public static Vector2 TerrainRedirect(GameLocation location, Vector2 target, Vector2 origin)
        {

            if(location is DruidLocation druidLocation)
            {

                // check all terrain
                foreach(TerrainField tField in druidLocation.terrainFields)
                {

                    // check if too close
                    if (Vector2.Distance(tField.center,target) < (tField.girth + 32f))
                    {

                        Vector2 center = ModUtility.PositionToTile(tField.center);

                        int segment = (ModUtility.DirectionToTarget(origin, tField.center)[2] + 4) % 8;

                        int firstsegment = (segment + 7) % 8;

                        int othersegment = (segment + 1) % 8;

                        if(Vector2.Distance(origin,tField.center) < Vector2.Distance(origin, target))
                        {

                            if (Mod.instance.randomIndex.Next(2) == 0)
                            {

                                firstsegment = (segment + 2) % 8;

                                othersegment = (segment + 6) % 8;

                            }
                            else
                            {

                                firstsegment = (segment + 2) % 8;

                                othersegment = (segment + 6) % 8;

                            }

                        }
                        else if (Mod.instance.randomIndex.Next(2) == 0)
                        {

                            firstsegment = (segment + 1) % 8;

                            othersegment = (segment + 7) % 8;

                        }

                        // check eighth segments on first side
                        List<Vector2> occupiables = ModUtility.GetOccupiableTilesNearby(location, center, firstsegment, tField.clearance, 1);

                        if (occupiables.Count > 0)
                        {

                            return occupiables.First() * 64;

                        }

                        // check eighth segments on other side
                        occupiables = ModUtility.GetOccupiableTilesNearby(location, center, othersegment, tField.clearance, 1);

                        if (occupiables.Count > 0)
                        {

                            return occupiables.First() * 64;

                        }

                        return origin;

                    }

                }

            }

            return target;

        }

    }

}
