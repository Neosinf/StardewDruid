using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using xTile.Tiles;
using static StardewDruid.Data.IconData;
using static StardewValley.Menus.CharacterCustomization;


namespace StardewDruid.Location
{

    public class WarpTile
    {

        public string location;

        public int enterX;

        public int enterY;

        public int exitX;

        public int exitY;

        public WarpTile(int x, int y, string Location, int a, int b)
        {

            enterX = x;

            enterY = y;

            location = Location;

            exitX = a;

            exitY = b;

        }

    }

    public class TerrainTile
    {

        public Vector2 position = Vector2.Zero;

        public List<Vector2> baseTiles = new();

        public Microsoft.Xna.Framework.Rectangle source = Rectangle.Empty;

        public int index;

        public float layer;

        public bool flip;

        public bool special;

        public IconData.tilesheets tilesheet;

        public int shake;

        public enum shadows
        {
            none,
            offset,
            smallleafy,
            leafy,
            moreleafy,
            bigleafy,
            bush,
            massive,
        }

        public shadows shadow;

        public TerrainTile(IconData.tilesheets Tilesheet, int Index, Vector2 Position, shadows Shadow = shadows.offset, bool Flip = false)
        {

            tilesheet = Tilesheet;

            index = Index;

            position = Position;

            source = LocationData.TerrainRectangles(tilesheet, Index);

            baseTiles = LocationData.TerrainBases(tilesheet, Index, position, source);

            layer = LocationData.TerrainLayers(tilesheet, Index, position, source);

            special = LocationData.TerrainRules(tilesheet, Index);

            shadow = LocationData.TerrainShadows(tilesheet, Index, Shadow);

            flip = Flip;

        }

        public void draw(SpriteBatch b, GameLocation location)
        {

            if (Utility.isOnScreen(position + source.Center.ToVector2(), source.Height*8))
            {
                
                float opacity = 1f;

                if (special)
                {

                    source = LocationData.TerrainSpecials(tilesheet, index);

                }

                Microsoft.Xna.Framework.Vector2 origin = new(position.X - (float)Game1.viewport.X, position.Y - (float)Game1.viewport.Y);
                
                Microsoft.Xna.Framework.Rectangle bounds = new((int)position.X + 8, (int)position.Y, (source.Width*4) -16, (source.Height*4) - 72);

                foreach (Farmer character in location.farmers)
                {

                    if (bounds.Contains(character.Position.X, character.Position.Y))
                    {

                        opacity = 0.75f;

                    }

                }

                if (opacity == 1f)
                {

                    foreach (NPC character in location.characters)
                    {

                        if (bounds.Contains(character.Position.X, character.Position.Y))
                        {

                            opacity = 0.75f;
                        }

                    }

                }

                /*if (shake > 0)
                {

                    switch(shake % 8)
                    {

                        case 0:

                            origin.X += 24;

                            break;

                        case 1:

                            origin.X += 48;

                            break;

                        case 2:

                            origin.X += 24;

                            break;

                        case 3:

                            break;

                        case 4:

                            origin.X -= 24;

                            break;

                        case 5:

                            origin.X -= 48;

                            break;

                        case 6:

                            origin.X -= 24;

                            break;

                        case 7:

                            break;

                    }

                }*/

                b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, Microsoft.Xna.Framework.Color.White * opacity, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                switch (shadow)
                {

                    case shadows.offset:

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(2, 8), source, Microsoft.Xna.Framework.Color.Black * 0.4f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                        break;

                    case shadows.smallleafy:

                        b.Draw(Game1.mouseCursors, origin + new Vector2((source.Width * 2), (source.Height * 4) - 18), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White, 0f, new Vector2(20, 15), 3f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;

                    case shadows.leafy:

                        b.Draw(Game1.mouseCursors, origin + new Vector2((source.Width * 2), (source.Height * 4) - 24), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White, 0f, new Vector2(20,15), 4f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;

                    case shadows.moreleafy:

                        b.Draw(Game1.mouseCursors, origin + new Vector2((source.Width * 2), (source.Height * 4) - 36), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 7f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;

                    case shadows.bigleafy:

                        origin.X += 32;

                        b.Draw(Game1.mouseCursors, origin + new Vector2((source.Width * 2), (source.Height * 4) - 64), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White*0.7f, 0f, new Vector2(20, 15), 9f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;

                    case shadows.massive:

                        origin.X += 32;

                        b.Draw(Game1.mouseCursors, origin + new Vector2((source.Width * 2)-80, (source.Height * 4) - 80), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 12f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;
                    case shadows.bush:

                        b.Draw(Game1.mouseCursors_1_6, origin + new Vector2((source.Width * 2), (source.Height * 4) - 18), new Microsoft.Xna.Framework.Rectangle(469, 298, 42, 31), Color.White, 0f, new Vector2(20, 15), 3f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;
                     
                }
            }
        }
    }
    public class LightField
    {
        public Vector2 origin;
        public int luminosity = 4;
        public Microsoft.Xna.Framework.Color colour = Microsoft.Xna.Framework.Color.LightGoldenrodYellow;

        public enum lightTypes
        {
            sconceLight,
            tableLight,
            lantern,
            brazier

        }

        public lightTypes lightType;

        public int lightFrame;

        public float lightAmbience = 0.75f;

        public int lightLayer;

        public int lightTimer = -1;

        public LightField(Vector2 Origin)
        { 
            
            origin = Origin;

        }

        public LightField(Vector2 Origin, int Luminosity, Microsoft.Xna.Framework.Color Colour)
        {

            origin = Origin;

            luminosity = Luminosity;

            colour = Colour;

        }

        public void draw(SpriteBatch b)
        {

            if(lightTimer != -1)
            {

                if(Game1.timeOfDay < lightTimer)
                {

                    return;

                }

            }

            if (Utility.isOnScreen(origin, 64 * luminosity))
            {


                //Texture2D texture2D;

                int type = 4;

                switch (lightType)
                {

                    case lightTypes.brazier:

                        int brazierTime = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 1000) / 250;

                        int frame = (brazierTime + lightFrame) % 4;

                        Microsoft.Xna.Framework.Vector2 position = new(origin.X - (float)Game1.viewport.X, origin.Y - (float)Game1.viewport.Y);

                        b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.tomb],
                            position,
                            new Microsoft.Xna.Framework.Rectangle(32 + (frame * 32), 0, 32, 32),
                            Microsoft.Xna.Framework.Color.White,
                            0f,
                            Vector2.Zero,
                            4,
                            0,
                            (origin.Y + 384) / 10000
                            );

                        //texture2D = Game1.sconceLight;

                        position += new Vector2(64);

                        break;

                    case lightTypes.lantern:

                        //texture2D = Game1.lantern;

                        type = 1;

                        break;

                    default:

                        //texture2D = Game1.sconceLight;


                        break;

                }

                /*b.Draw(
                    texture2D,
                    position,
                    texture2D.Bounds,
                    Microsoft.Xna.Framework.Color.Purple * 0.9f,
                    0f,
                    new Vector2(texture2D.Bounds.Width / 2, texture2D.Bounds.Height / 2),
                    0.25f * 6,
                    SpriteEffects.None,
                    origin.Y / 10000 + 0.9f + lightLayer
                );*/

                int id = (int)(origin.X*10000 + origin.Y);

                for(int l = Game1.currentLightSources.Count - 1; l >= 0; l--)
                {
                    LightSource lightSource = Game1.currentLightSources.ElementAt(l);

                    if (lightSource.Identifier == id)
                    {

                        //Game1.currentLightSources.Remove(lightSource);

                        return;

                    }

                }

                LightSource light = new LightSource(type, origin, 0.35f * luminosity, Microsoft.Xna.Framework.Color.Black*lightAmbience, id, LightSource.LightContext.None, 0L);

                Game1.currentLightSources.Add(light);

            }

        }

    }

    public class CrateTile
    {

        public Vector2 origin;

        public SpellHandle spell;

        public bool empty;

        public int series;

        public int crate;


        public CrateTile(Vector2 Origin, int Series, int Crate)
        {

            origin = Origin;

            series = Series;

            crate = Crate;

        }

        public void draw(SpriteBatch b)
        {

            if (!Utility.isOnScreen(origin, 64))
            {

                return;

            }

            if (empty)
            {

                if (spell != null)
                {

                    if (spell.counter < 300)
                    {

                        return;

                    }

                }

            }

            Microsoft.Xna.Framework.Vector2 position = new(origin.X - (float)Game1.viewport.X, origin.Y - (float)Game1.viewport.Y - 64);

            b.Draw(
                Mod.instance.iconData.crateTexture,
                position,
                new(empty ? 64 : 0, 0, 32, 64),
                Color.White,
                0f,
                Vector2.Zero,
                2f,
                SpriteEffects.None,
                origin.Y / 10000
            );

        }

        public void open(GameLocation location)
        {

            SpellHandle spell = new(location, new(series, crate), origin);

            spell.type = SpellHandle.spells.crate;

            spell.added.Add(SpellHandle.effects.crate);

            spell.counter = 60;

            spell.Update();

            Mod.instance.spellRegister.Add(spell);

            empty = true;

        }

    }

    
    public static class LocationData
    {

        public const string druid_grove_name = "18465_Grove";

        public const string druid_spring_name = "18465_Spring";

        public const string druid_atoll_name = "18465_Atoll";

        public const string druid_graveyard_name = "18465_Graveyard";

        public const string druid_clearing_name = "18465_Clearing";

        public const string druid_chapel_name = "18465_Chapel";

        public const string druid_vault_name = "18465_Treasure";

        public const string druid_tunnel_name = "18465_Tunnel";

        public const string druid_court_name = "18465_Court";

        public const string druid_archaeum_name = "18465_Archaeum";

        public const string druid_tomb_name = "18465_Tomb";

        public const string druid_engineum_name = "18465_Engineum";

        public const string druid_gate_name = "18465_Gate";

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

                    location = new Location.Grove(map);

                    break;

                case druid_spring_name:

                    location = new Location.Spring(map);

                    break;

                case druid_atoll_name:

                    location = new Location.Atoll(map);

                    break;

                case druid_graveyard_name:

                    location = new Location.Graveyard(map);

                    break;

                case druid_clearing_name:

                    location = new Location.Clearing(map);

                    break;

                case druid_chapel_name:

                    location = new Location.Chapel(map);

                    break;

                case druid_vault_name:

                    location = new Location.Vault(map);

                    break;

                case druid_tunnel_name:

                    location = new Location.Tunnel(map);

                    break;

                case druid_court_name:

                    location = new Location.Court(map);

                    break;

                case druid_archaeum_name:

                    location = new Location.Archaeum(map);

                    break;

                case druid_tomb_name:

                    location = new Location.Tomb(map);

                    break;

                case druid_engineum_name:

                    location = new Location.Engineum(map);

                    break;

                case druid_gate_name:

                    location = new Location.Gate(map);

                    break;
            }

            Game1.locations.Add(location);

            Mod.instance.locations.Add(map, location);

        }

        public static Microsoft.Xna.Framework.Rectangle TerrainRectangles(IconData.tilesheets sheet, int key)
        {

            switch (sheet)
            {

                case tilesheets.outdoors:

                    switch (key)
                    {
                        case 1:

                            return new(0,0,48,64);

                        case 2:

                            return new(16,64,16,32);

                        case 3:

                            return new(48,0,48,64);

                        case 4:

                            return new(64,64,16,32);

                        case 5:

                            return new(96,0,64,64);

                        case 6:

                            return new(112,64,32,48);

                        case 7:

                            return new(160,0,48,64);

                        case 8:

                            return new(176,64,16,32);

                        case 9:

                            return new(208,0,48,48);

                        case 10:

                            return new(208, 48,32,32);

                        case 11:

                            return new(256,0,96,64);

                        case 12:

                            return new(288,64,32,48);

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

                case tilesheets.outdoorsTwo:

                    switch (key)
                    {
                        case 1:

                            return new(0,112,112,80);

                        case 2:

                            return new(16,192,64,32);

                    }

                    break;

                case tilesheets.grove:


                    switch (key)
                    {

                        case 1:

                            return new(0, 32,32, 32);

                        case 2:

                            return new(32, 0, 32, 48);

                        case 3:

                            return new(80, 16, 16, 48);

                        case 4:

                            return new(96, 0, 16, 32);

                        case 5:

                            return new(112, 16, 16, 16);

                        case 6:

                            return new(96, 32, 16, 32);

                        case 7:

                            return new(112, 32, 16, 32);

                        case 8:

                            return new(128, 0, 48, 32);

                    }

                    break;

                case tilesheets.magnolia:


                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 176, 112);

                        case 2:

                            return new(64, 112, 64, 64);

                    }

                    break;

                case tilesheets.spring:


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

                    }

                    break;

                case tilesheets.atoll:


                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 48, 96);

                        case 2:

                            return new(48, 16, 16, 48);

                        case 3:

                            return new(64, 0, 32, 64);

                        case 4:

                            return new(96, 16, 16, 48);

                        case 5:

                            return new(112, 16, 32, 48);

                        case 6:

                            return new(144, 0, 16, 32);

                        case 7:

                            return new(144, 32, 16, 32);

                        case 8:

                            return new(160, 32, 16, 32);

                    }

                    break;

                case tilesheets.graveyard:

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

                        // larger statues

                        case 17:

                            return new(144, 48, 32, 64);

                        case 18:

                            return new(176, 48, 32, 64);

                        // lamppost

                        case 19:

                            return new(208, 48, 16, 64);

                        // tomb

                        case 20:

                            return new(48, 128, 48, 80);


                    }

                    break;

                case tilesheets.chapel:

                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 32, 80);

                        case 2:

                            return new(0, 112, 64, 32);

                    }

                    break;


                case tilesheets.court:

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

                    }

                    break;


                case tilesheets.tomb:

                    switch (key)
                    {
                        case 1:

                            return new(0, 0, 32, 64);

                    }

                    break;


                case tilesheets.engineum:

                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 128, 128);

                        case 2:

                            return new(128, 0, 48, 64);

                        case 3:

                            return new(128, 64, 48, 64);

                    }

                    break;

                case tilesheets.gate:

                    switch (key)
                    {
                        case 1:

                            return new(0, 0, 128, 80);

                        case 2:

                            return new(0, 112, 48, 192);

                        case 3:

                            return new(128, 0, 128, 96);

                        case 4:

                            return new(256, 0, 128, 96);

                        case 5:

                            return new(128, 96, 128, 96);

                        case 6:

                            return new(128, 192, 128, 96);

                        case 7:

                            return new(128, 288, 128, 96);

                        case 8:
                            
                            return new(256, 288, 96, 96);

                        case 9:

                            return new(48, 112, 48, 144);

                        case 10:

                            return new(48, 256, 48, 96);

                        case 11:

                            return new(352, 96, 96, 96);

                        case 12:

                            return new(352, 192, 96, 96);

                        case 13:

                            return new(352, 288, 96, 96);

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

                case tilesheets.magnolia:

                    switch (key)
                    {
                        case 1:

                            return new();

                        case 2:

                            footPrint = 2;

                            break;

                    }

                    break;

                case tilesheets.outdoors:

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

                case tilesheets.outdoorsTwo:

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

                case tilesheets.spring:

                    switch (key)
                    {

                        case 6:

                            break;

                        default:

                            return new();

                    }

                    break;

                case tilesheets.graveyard:

                    switch (key)
                    {
                        
                        case 13:
                        case 14:
                        case 20:

                            footPrint = 2;

                            break;

                        case 1:
                        case 3:
                        case 4:

                            footPrint = 3;

                            break;

                        case 6:

                            footPrint = 4;

                            break;

                    }

                    break;

                case tilesheets.gate:

                    switch (key)
                    {

                        case 2:
                        case 9:
                        case 10:

                            footPrint = 2;

                            break;

                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 11:
                        case 12:
                        case 13:

                            return new();

                    }

                    break;

                case tilesheets.engineum:

                    footPrint = 2;

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
                case tilesheets.outdoors:

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

                case tilesheets.outdoorsTwo:

                    switch (key)
                    {
                        case 1:


                            return (position.Y + 448) / 10000;

                    }

                    break;

                case tilesheets.spring:

                    switch (key)
                    {

                        case 1:

                            return (position.Y + 512) / 10000;

                    }

                    break;

            }

            return (position.Y + (source.Height * 4)) / 10000;

        }

        public static bool TerrainRules(IconData.tilesheets sheet, int key)
        {

            switch (sheet)
            {

                case tilesheets.magnolia:

                    return true;

                case tilesheets.grove:

                    switch (key)
                    {
                        case 8:

                            return true;
                    }

                    break;

                case tilesheets.graveyard:

                    switch (key)
                    {
                        case 19:

                            return true;
                    }

                    break;

            }
 
            return false;

        }

        public static Rectangle TerrainSpecials(IconData.tilesheets sheet, int key)
        {

            switch (sheet)
            {


                case tilesheets.magnolia:


                    int offset = 0;

                    switch (Game1.currentSeason)
                    {

                        case "summer": offset = 1;break;

                        case "fall": offset = 2; break;

                        case "winter": offset = 3; break;

                    }

                    switch (key)
                    {

                        case 1:

                            return new(0 +(offset*176), 0, 176, 112);

                        case 2:

                            return new(64 + (offset * 176), 112, 64, 64);

                    }

                    break;

                case tilesheets.grove:

                    switch (key)
                    {
                        case 8:

                            if (Mod.instance.save.progress.ContainsKey(QuestHandle.herbalism))
                            {

                                if (Mod.instance.save.progress[QuestHandle.herbalism].progress == 1)
                                {

                                    return new(128, 32, 48, 32);
                                }

                            }

                            break;

                    }

                    break;

                case tilesheets.graveyard:

                    switch (key)
                    {
                        case 19:

                            if(Game1.timeOfDay >= 1900)
                            {
                                return new(224, 48, 16, 64);

                            }

                            break;

                    }

                    break;

            }

            return TerrainRectangles(sheet, key);

        }

        public static TerrainTile.shadows TerrainShadows(IconData.tilesheets sheet, int key, TerrainTile.shadows shadow)
        {

            switch (sheet)
            {

                case tilesheets.magnolia:

                    switch (key)
                    {

                        case 1:

                            return TerrainTile.shadows.none;


                        case 2:
                            
                            return TerrainTile.shadows.massive;

                    }

                    break;

                case tilesheets.outdoors:

                    switch (key)
                    {

                        case 2:
                        case 4:
                        case 6:
                        case 8:
                        case 9:

                            return TerrainTile.shadows.leafy;

                        case 10:

                            return TerrainTile.shadows.smallleafy;

                        case 12:

                            return TerrainTile.shadows.moreleafy;

                    }

                    return TerrainTile.shadows.none;

                case tilesheets.outdoorsTwo:

                    switch (key)
                    {

                        case 2:

                            return TerrainTile.shadows.bigleafy;

                    }

                    return TerrainTile.shadows.none;

                case tilesheets.gate:

                    switch (key)
                    {

                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:

                        case 11:
                        case 12:
                        case 13:

                            return TerrainTile.shadows.none;

                    }

                    break;
            }

            return shadow;

        }

    }

}
