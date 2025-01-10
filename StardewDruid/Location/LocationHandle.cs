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
using StardewDruid.Event;
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

        public const string druid_limbo = "18465_Limbo";

        public const string druid_spring_name = "18465_Spring";

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

        public const string druid_gate_name = "18465_Gate";

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

                case druid_spring_name:

                    location = new Location.Druid.Spring(map);

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

                case druid_gate_name:

                    location = new Location.Druid.Gate(map);

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

                case IconData.tilesheets.outdoors:

                    switch (key)
                    {
                        case 1:

                            return new(0, 0, 48, 64);

                        case 2:

                            return new(16, 64, 16, 32);

                        case 3:

                            return new(48, 0, 48, 64);

                        case 4:

                            return new(64, 64, 16, 32);

                        case 5:

                            return new(96, 0, 64, 64);

                        case 6:

                            return new(112, 64, 32, 48);

                        case 7:

                            return new(160, 0, 48, 64);

                        case 8:

                            return new(176, 64, 16, 32);

                        case 9:

                            return new(208, 0, 48, 48);

                        case 10:

                            return new(208, 48, 32, 32);

                        case 11:

                            return new(256, 0, 96, 64);

                        case 12:

                            return new(288, 64, 32, 48);

                        case 13: //left

                            return new(128, 224, 16, 32);

                        case 14: //middle

                            return new(144, 224, 16, 32);

                        case 15: //right end

                            return new(160, 224, 16, 32);

                        case 16: //down

                            return new(176, 224, 16, 32);

                        case 17: //down end

                            return new(176, 256, 16, 32);

                    }

                    break;

                case IconData.tilesheets.outdoorsTwo:

                    switch (key)
                    {
                        case 1:

                            return new(0, 112, 112, 80);

                        case 2:

                            return new(16, 192, 64, 32);

                    }

                    break;

                case IconData.tilesheets.grove:

                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 96, 48);

                        case 2:

                            return new(96, 0, 32, 64);

                        case 3:

                            return new(128, 0, 32, 64);

                        case 4:

                            return new(160, 0, 32, 48);

                        case 5:

                            return new(192, 0, 32, 64);

                        case 6:

                            return new(224, 0, 32, 64);

                        case 7:

                            return new(96, 64, 64, 32);

                        case 8:

                            return new(160, 48, 32, 32);

                        case 9:

                            return new(0, 48, 64, 32);
                    }

                    break;


                case IconData.tilesheets.spring:


                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 192, 64);

                        case 2:

                            return new(192, 0, 96, 96);

                        case 3:

                            return new(0, 64, 32, 64);

                        case 4:

                            return new(32, 64, 16, 64);

                        case 5:

                            return new(144, 64, 16, 64);

                        case 6:

                            return new(48, 64, 48, 64);

                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:

                            return new((key - 7) * 32, 128, 32, 32);

                        case 15:
                        case 16:

                            return new((key - 15) * 32, 160, 32, 32);


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

                        // small epitaphs

                        case 8:

                            return new(0, 48, 16, 32);

                        case 9:

                            return new(16, 48, 16, 32);

                        case 10:

                            return new(32, 48, 16, 32);

                        // larger epitaphs

                        case 11:

                            return new(48, 48, 48, 32);

                        // larger urn

                        case 12:

                            return new(96, 48, 32, 32);

                        // larger sarcophagus

                        case 13:

                            return new(0, 80, 32, 48);

                        case 14:

                            return new(32, 80, 16, 48);

                        case 15:

                            return new(48, 80, 48, 32);

                        case 16:

                            return new(96, 80, 48, 32);

                        // gargoyle

                        case 17:

                            return new(144, 48, 32, 64);

                        // angel

                        case 18:

                            return new(176, 48, 32, 64);

                        // tomb

                        case 20:

                            return new(0, 176, 48, 80);

                        // larger sarcophagus 2

                        case 21:

                            return new(0, 128, 48, 32);

                        case 22:

                            return new(48, 112, 48, 32);

                        case 23:

                            return new(96, 112, 48, 32);

                        case 24:

                            return new(144, 112, 32, 48);

                        // memory stone

                        case 25:

                            return new(0, 128, 32, 48);

                        // king statues

                        case 26:

                            return new(48, 144, 48, 112);

                        case 27:

                            return new(96, 144, 48, 112);

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
                    }

                    break;

                case IconData.tilesheets.court:

                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 64, 128);

                        case 2:

                            return new(64, 0, 64, 128);

                        case 3:

                            return new(128, 0, 64, 128);

                        case 4:

                            return new(192, 0, 64, 128);

                        // hangouts
                        case 5:

                            return new(256, 0, 32, 64);

                        case 6:

                            return new(288, 0, 16, 64);

                        case 7:

                            return new(304, 0, 48, 64);

                        case 8:

                            return new(352, 0, 32, 64);

                        // lanterns
                        case 9:

                            return new(256, 64, 32, 64);

                        case 10:

                            return new(288, 64, 32, 64);

                        case 11:

                            return new(320, 64, 32, 64);

                        case 12:

                            return new(352, 64, 32, 64);

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

                        case 4:

                            return new(160, 0, 32, 48);

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

                case IconData.tilesheets.gate:

                    switch (key)
                    {
                        case 1:

                            return new(0, 0, 160, 128);

                        case 2:

                            return new(0, 144, 48, 192);

                        case 3:

                            return new(176, 0, 128, 96);

                        case 4:

                            return new(304, 0, 128, 96);

                        case 5:

                            return new(160, 96, 144, 112);

                        case 6:

                            return new(160, 208, 144, 96);

                        case 7:

                            return new(176, 304, 128, 80);

                        case 8:

                            return new(304, 304, 80, 80);

                        case 9:

                            return new(48, 144, 48, 144);

                        case 10:

                            return new(48, 288, 48, 96);

                        case 11:

                            return new(0, 384, 80, 96);

                        case 12:

                            return new(80, 384, 80, 96);

                        case 13:

                            return new(160, 384, 80, 96);

                        case 14:

                            return new(240, 384, 80, 96);

                        case 15:

                            return new(96, 224, 64, 160);

                        case 16:

                            return new(96, 144, 64, 64);

                    }

                    break;

                case IconData.tilesheets.ritual:

                    switch (key)
                    {
                        case 1:

                            return new(0,0,112,96);

                    }

                    break;

                case IconData.tilesheets.mounds:

                    switch (key)
                    {
                        case 1:

                            return new(64, 0, 64, 64);

                        case 2:

                            return new(128, 0, 64, 64);

                        case 3:

                            return new(192, 0, 32, 64);

                        case 4:

                            return new(224, 0, 32, 64);

                        case 5:

                            return new(256, 0, 32, 64);

                        case 6:

                            return new(0, 0, 64, 128);

                        case 7:

                            return new(64, 64, 96, 96);

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

                case IconData.tilesheets.court:

                    if(key < 5)
                    {

                        footPrint = 2;

                    }

                    break;

                case IconData.tilesheets.grove:

                    if(key == 1)
                    {

                        footPrint = 2;

                    }

                    break;

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
                        case 15:
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


                case IconData.tilesheets.outdoors:

                    switch (key)
                    {
                        case 1:

                        case 3:

                        case 5:

                        case 7:

                        case 11:

                            return new();

                    }

                    break;

                case IconData.tilesheets.outdoorsTwo:

                    switch (key)
                    {
                        case 1:

                            return new();

                        case 2:

                            for (int y = 0; y < 2; y++)
                            {

                                for (int x = 1; x < 4; x++)
                                {

                                    baseTiles.Add(new Vector2(tile.X + x, tile.Y +  y));

                                }

                            }

                            return baseTiles;

                    }

                    break;

                case IconData.tilesheets.spring:

                    switch (key)
                    {

                        case 6:

                            break;

                        default:

                            return new();

                    }

                    break;

                case IconData.tilesheets.graveyard:

                    switch (key)
                    {
                        
                        case 13:
                        case 14:
                        case 20:
                        case 24:
                        case 26:
                        case 27:

                            footPrint = 2;

                            break;
                    }

                    break;


                case IconData.tilesheets.tomb:

                    break;

                case IconData.tilesheets.gate:

                    switch (key)
                    {

                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 15:
                        case 16:

                            return new();

                        case 2:
                        case 9:
                        case 10:

                            footPrint = 2;

                            break;

                        case 1:

                            footPrint = 3;

                            break;

                        case 11:

                            return new()
                            {
                                new Vector2(tile.X+1,tile.Y+2),
                                new Vector2(tile.X+2,tile.Y+2),
                                new Vector2(tile.X+3,tile.Y+2),
                                new Vector2(tile.X+4,tile.Y+2),

                                new Vector2(tile.X+1,tile.Y+3),
                                new Vector2(tile.X+2,tile.Y+3),
                                new Vector2(tile.X+3,tile.Y+3),
                                new Vector2(tile.X+4,tile.Y+3),

                                new Vector2(tile.X+1,tile.Y+4),
                                new Vector2(tile.X+2,tile.Y+4),
                                new Vector2(tile.X+3,tile.Y+4),
                                new Vector2(tile.X+4,tile.Y+4),

                                new Vector2(tile.X+1,tile.Y+5),
                                new Vector2(tile.X+2,tile.Y+5),
                                new Vector2(tile.X+3,tile.Y+5),
                                new Vector2(tile.X+4,tile.Y+5),

                            };

                    }

                    break;

                case IconData.tilesheets.engineum:

                    switch (key)
                    {

                        default:

                            footPrint = 2;

                            break;

                        case 4:

                            return new();

                        case 7:

                            footPrint = 3;

                            break;

                        case 8:

                            footPrint = 4;

                            break;
                    }

                    break;

                case IconData.tilesheets.mounds:

                    switch (key)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4: 
                        case 5:

                            footPrint = 2; break;

                        case 6:

                            footPrint = 3; break;

                        case 7:

                            footPrint = 5;break;

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
                case IconData.tilesheets.outdoors:

                    switch (key)
                    {
                        case 1:
                        case 3:
                        case 7:

                            return (position.Y + 384) / 10000;

                        case 5:
                        case 11:
                            
                            return (position.Y + 448) / 10000;

                    }

                    break;

                case IconData.tilesheets.outdoorsTwo:

                    switch (key)
                    {
                        case 1:


                            return (position.Y + 448) / 10000;

                    }

                    break;

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

                case IconData.tilesheets.engineum:

                    switch (key)
                    {

                        case 4:

                            return (position.Y + (source.Height * 4) + 128) / 10000;
                    }

                    break;

                case IconData.tilesheets.gate:

                    switch (key)
                    {

                        case 16:

                            return (position.Y + (source.Height * 4) + 8) / 10000;
                    }

                    break;

            }

            return (position.Y - 32 + (source.Height * 4)) / 10000;

        }

        public static bool TerrainRules(IconData.tilesheets sheet, int key)
        {

            switch (sheet)
            {

                case IconData.tilesheets.grove:

                    return true;

                case IconData.tilesheets.court:

                    return true;

                case IconData.tilesheets.engineum:

                    switch (key)
                    {

                        case 4:

                            return true;
                    }

                    break;

                case IconData.tilesheets.gate:

                    switch (key)
                    {

                        case 15:

                            return true;

                        case 16:

                            return true;

                    }

                    break;

            }
 
            return false;

        }

        public static Rectangle TerrainSpecials(IconData.tilesheets sheet, int key, Microsoft.Xna.Framework.Rectangle source)
        {

            switch (sheet)
            {

                case IconData.tilesheets.grove:

                    if (Game1.timeOfDay > 1700)
                    {

                        source.Y += 96;

                    }

                    break;

                case IconData.tilesheets.court:

                    if (Game1.timeOfDay > 1700)
                    {

                        source.Y += 128;

                    }

                    break;

                case IconData.tilesheets.engineum:

                    switch (key)
                    {
                        
                        case 4:

                            if (Mod.instance.rite.specialCasts.ContainsKey(druid_engineum_name))
                            {

                                if (Mod.instance.rite.specialCasts[druid_engineum_name].Contains("AetherBlessing"))
                                {

                                    return new(160, 48, 32, 48);

                                }

                            }

                        break;
                    }

                    break;

                case IconData.tilesheets.gate:

                    switch (key)
                    {

                        case 15:

                            if (!Mod.instance.activeEvent.ContainsKey(QuestHandle.challengeBones))
                            {

                                return Rectangle.Empty;

                            }

                            if (Mod.instance.eventRegister[QuestHandle.challengeBones].activeCounter < 300)
                            {
                                
                                return Rectangle.Empty;

                            }

                            break;

                        case 16:

                            if (Game1.player.currentLocation is Gate gateLocation)
                            {

                                if (gateLocation.gateOpen)
                                {

                                    return Rectangle.Empty;

                                }

                            }

                            break;

                    }

                    break;

            }

            return source;

        }

        public static TerrainField.shadows TerrainShadows(IconData.tilesheets sheet, int key, TerrainField.shadows shadow)
        {

            switch (sheet)
            {

                case IconData.tilesheets.atoll:

                    switch (key)
                    {

                        case 5:

                            return TerrainField.shadows.none;

                    }

                    return TerrainField.shadows.reflection;

                case IconData.tilesheets.outdoors:

                    switch (key)
                    {

                        case 2:
                        case 4:
                        case 6:
                        case 8:
                        case 9:

                            return TerrainField.shadows.leafy;

                        case 10:

                            return TerrainField.shadows.smallleafy;

                        case 12:

                            return TerrainField.shadows.moreleafy;

                    }

                    return TerrainField.shadows.none;

                case IconData.tilesheets.outdoorsTwo:

                    switch (key)
                    {

                        case 2:

                            return TerrainField.shadows.bigleafy;

                    }

                    return TerrainField.shadows.none;

                case IconData.tilesheets.spring:

                    switch (key)
                    {

                        case 1:

                            return TerrainField.shadows.none;

                        case 2:
                        case 3:
                        case 4:
                        case 5:

                            return TerrainField.shadows.reflection;

                    }
                    break;

                case IconData.tilesheets.gate:

                    switch (key)
                    {

                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:

                            return TerrainField.shadows.none;

                    }

                    break;

            }

            return shadow;

        }

        public static float TerrainFadeout(IconData.tilesheets sheet, int key)
        {

            switch (sheet)
            {
                case IconData.tilesheets.magnolia:

                    switch (key)
                    {
                        case 1:

                            return 0.55f;

                    }

                    return 0.45f;

                case IconData.tilesheets.grove:

                    switch (key)
                    {
                        case 1:

                            return 1f;

                    }

                    break;

                case IconData.tilesheets.chapel:

                    switch (key)
                    {

                        case 10:

                            return 1f;

                        case 11:

                            return 1f;

                    }

                    break;


            }

            return 0.75f;

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
