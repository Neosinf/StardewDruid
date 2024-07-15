using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;

using static StardewDruid.Data.IconData;


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

        public Vector2 position;

        public List<Vector2> baseTiles = new();

        public Microsoft.Xna.Framework.Rectangle source;

        public int index;

        public float layer;

        public bool shadow;

        public bool flip;

        public IconData.tilesheets tilesheet;

        public TerrainTile(IconData.tilesheets Tilesheet, int Index, Vector2 Position, bool Shadow = true, bool Flip = false)
        {

            tilesheet = Tilesheet;

            index = Index;

            position = Position;

            source = LocationData.TerrainRectangles(tilesheet, Index);

            Vector2 tile = ModUtility.PositionToTile(Position);

            Vector2 range = new Vector2(source.Width / 16, source.Height / 16);

            for (int x = 0; x < (int)range.X; x++)
            {

                baseTiles.Add(new Vector2(tile.X + x, tile.Y + ((int)range.Y - 1)));

            }

            layer = (Position.Y + (range.Y * 64f)) / 10000;

            shadow = Shadow;

            flip = Flip;
        }

        public void draw(SpriteBatch b, GameLocation location)
        {

            if (Utility.isOnScreen(position + source.Center.ToVector2(), source.Height*4))
            {
                
                float opacity = 1f;

                Microsoft.Xna.Framework.Vector2 origin = new(position.X - (float)Game1.viewport.X, position.Y - (float)Game1.viewport.Y);
                
                Microsoft.Xna.Framework.Rectangle bounds = new((int)position.X, (int)position.Y, (source.Width*4), (source.Height*4) - 72);

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
                
                b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, Microsoft.Xna.Framework.Color.White * opacity, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);
                
                if (shadow)
                {
                    
                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(2, 8), source, Microsoft.Xna.Framework.Color.Black * 0.4f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);
                
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

            if (Utility.isOnScreen(origin, 64 * luminosity))
            {

                Microsoft.Xna.Framework.Vector2 position = new(origin.X - (float)Game1.viewport.X, origin.Y - (float)Game1.viewport.Y);

                Texture2D texture2D;

                switch (lightType)
                {

                    case lightTypes.brazier:

                        int brazierTime = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 1000) / 250;

                        int frame = (brazierTime + lightFrame) % 4;

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

                        texture2D = Game1.sconceLight;

                        position += new Vector2(64);

                        break;

                    case lightTypes.lantern:

                        texture2D = Game1.lantern;

                        break;

                    default:

                        texture2D = Game1.sconceLight;

                        break;

                }

                b.Draw(
                    texture2D,
                    position,
                    texture2D.Bounds,
                    colour * lightAmbience,
                    0f,
                    new Vector2(texture2D.Bounds.Width / 2, texture2D.Bounds.Height / 2),
                    0.25f * luminosity,
                    SpriteEffects.None,
                    origin.Y / 10000 + 0.9f + lightLayer
                );

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

        public const string druid_atoll_name = "18465_Atoll";

        public const string druid_chapel_name = "18465_Chapel";

        public const string druid_vault_name = "18465_Treasure";

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

                case druid_atoll_name:

                    location = new Location.Atoll(map);

                    break;

                case druid_chapel_name:

                    location = new Location.Chapel(map);

                    break;

                case druid_vault_name:

                    location = new Location.Vault(map);

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

                        case 9:

                            return new(128, 32, 48, 32);

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


                case tilesheets.engineum:

                    switch (key)
                    {

                        case 1:

                        return new(0, 0, 96, 80);

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

                case tilesheets.gate:

                    switch (key)
                    {
                        case 1:

                            return new(0, 0, 128, 80);


                    }

                    break;

                case tilesheets.tomb:

                    switch (key)
                    {
                        case 1:

                            return new(0, 0, 32, 64);

                    }

                    break;

            }

            return new(0, 0, 0, 0);

        }


    }


}
