using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using static StardewValley.Minigames.CraneGame;

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

        public float fadeout;

        public float shade;

        public Vector2 shadeOffset;

        public Microsoft.Xna.Framework.Color color;

        public enum shadows
        {
            none,
            offset,
            deepset,
            dropset,
            smallleafy,
            leafy,
            moreleafy,
            bigleafy,
            bush,
            massive,
            reflection,
        }

        public shadows shadow;

        public TerrainTile(IconData.tilesheets Tilesheet, int Index, Vector2 Position, shadows Shadow = shadows.offset, bool Flip = false)
        {

            tilesheet = Tilesheet;

            index = Index;

            position = Position;

            color = Color.White;

            source = LocationData.TerrainRectangles(tilesheet, Index);

            baseTiles = LocationData.TerrainBases(tilesheet, Index, position, source);

            layer = LocationData.TerrainLayers(tilesheet, Index, position, source);

            special = LocationData.TerrainRules(tilesheet, Index);

            shadow = LocationData.TerrainShadows(tilesheet, Index, Shadow);

            fadeout = LocationData.TerrainFadeout(tilesheet, Index);

            flip = Flip;

            shade = 0.3f;

            shadeOffset = new Vector2(2, 8);

        }

        public void draw(SpriteBatch b, GameLocation location, float forceFade = -1f)
        {

            if (Utility.isOnScreen(position + source.Center.ToVector2(), source.Height*8+128))
            {

                if (Mod.instance.iconData.sheetTextures.ContainsKey(tilesheet))
                {
                    
                    if (Mod.instance.iconData.sheetTextures[tilesheet].IsDisposed)
                    {

                        return;

                    }

                }
                else
                {

                    return;

                }

                float opacity = 1f;

                Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

                if (special)
                {

                    useSource = LocationData.TerrainSpecials(tilesheet, index, useSource);

                }

                Microsoft.Xna.Framework.Vector2 origin = new(position.X - (float)Game1.viewport.X, position.Y - (float)Game1.viewport.Y);

                if(forceFade > 0)
                {

                    opacity = forceFade;

                }
                else
                if (fadeout < 1f)
                {

                    int backing = 0;

                    if(baseTiles.Count > 0)
                    {

                        backing = 72;

                    }

                    Microsoft.Xna.Framework.Rectangle bounds = new((int)position.X + 8, (int)position.Y, (useSource.Width * 4) - 16, (useSource.Height * 4) - backing);

                    foreach (Farmer character in location.farmers)
                    {

                        if (bounds.Contains(character.Position.X, character.Position.Y))
                        {

                            opacity = fadeout;

                        }

                    }

                    if (opacity == 1f)
                    {

                        foreach (NPC character in location.characters)
                        {

                            if (bounds.Contains(character.Position.X, character.Position.Y))
                            {

                                opacity = fadeout;
                            }

                        }

                    }


                }

                switch (shadow)
                {

                    case shadows.offset:
                        
                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(1, 6), useSource, Microsoft.Xna.Framework.Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                        break;

                    case shadows.deepset:

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(2, 10), useSource, Microsoft.Xna.Framework.Color.Black * shade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                        break;

                    case shadows.dropset:

                        if (opacity >= 1f)
                        {

                            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, 12), useSource, Microsoft.Xna.Framework.Color.DarkBlue * 0.4f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);
                        
                        }
                        
                        if (Game1.timeOfDay < 2100) 
                        { 

                            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, 428), useSource, Microsoft.Xna.Framework.Color.Black * 0.1f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                        }

                        break;

                    case shadows.smallleafy:

                        b.Draw(Game1.mouseCursors, origin + new Vector2((useSource.Width * 2), (useSource.Height * 4) - 18), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White, 0f, new Vector2(20, 15), 3f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;

                    case shadows.leafy:

                        b.Draw(Game1.mouseCursors, origin + new Vector2((useSource.Width * 2), (useSource.Height * 4) - 24), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White, 0f, new Vector2(20,15), 4f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;

                    case shadows.moreleafy:

                        b.Draw(Game1.mouseCursors, origin + new Vector2((useSource.Width * 2), (useSource.Height * 4) - 36), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 7f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;

                    case shadows.bigleafy:

                        if (flip)
                        {

                            b.Draw(Game1.mouseCursors, origin + new Vector2((useSource.Width * 2) - 32, (useSource.Height * 4) - 64), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 9f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        }
                        else
                        {

                            b.Draw(Game1.mouseCursors, origin + new Vector2((useSource.Width * 2) + 32, (useSource.Height * 4) - 64), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 9f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        }

                        break;

                    case shadows.massive:

                        //b.Draw(Game1.mouseCursors, origin + new Vector2((useSource.Width * 2) - 256, (useSource.Height * 4) - 80), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 8f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        //b.Draw(Game1.mouseCursors, origin + new Vector2((useSource.Width * 2), (useSource.Height * 4) + 48), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 8f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        //b.Draw(Game1.mouseCursors, origin + new Vector2((useSource.Width * 2) + 256, (useSource.Height * 4) - 80), new Microsoft.Xna.Framework.Rectangle(663, 1011, 41, 30), Color.White * 0.7f, 0f, new Vector2(20, 15), 8f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;

                    case shadows.bush:

                        b.Draw(Game1.mouseCursors_1_6, origin + new Vector2((useSource.Width * 2), (useSource.Height * 4) - 18), new Microsoft.Xna.Framework.Rectangle(469, 298, 42, 31), Color.White, 0f, new Vector2(20, 15), 3f, flip ? (SpriteEffects)1 : 0, 1E-06f);

                        break;

                    case shadows.reflection:

                        Rectangle shadowSource = new(useSource.X, useSource.Y + (useSource.Height - 24), useSource.Width, 24);

                        //shadow
                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, 8 + (useSource.Height * 4 - 96)), shadowSource, Microsoft.Xna.Framework.Color.Black * 0.3f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                        Microsoft.Xna.Framework.Rectangle reflectuseSource = new(useSource.X,useSource.Y+useSource.Height,useSource.Width,2);

                        Vector2 reflectOrigin = new(origin.X + (useSource.Width * 2), origin.Y + (useSource.Height * 4)-28);

                        Vector2 reflectOut = new(useSource.Width/2,1);

                        float reflectFade = 0f;

                        for (int i = 0; i < 32; i++)
                        {
                            reflectuseSource.Y -= 2;

                            reflectOrigin.Y += 8;

                            if(i >= 16)
                            {


                                reflectFade -= 0.03f;


                            }
                            else
                            {

                                reflectFade += 0.03f;


                            }

                            if (flip)
                            {

                                b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], reflectOrigin, reflectuseSource, Microsoft.Xna.Framework.Color.White * reflectFade, (float)Math.PI, reflectOut, 4, 0, 1E-06f);

                            }
                            else
                            {
                                
                                b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], reflectOrigin, reflectuseSource, Microsoft.Xna.Framework.Color.White * reflectFade, 0, reflectOut, 4,(SpriteEffects)2, 1E-06f);

                            }

                        }

                        break;
                     
                }

                b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, color * opacity, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            }

        }
    
        public void reset()
        {

            source = LocationData.TerrainRectangles(tilesheet, index);

            baseTiles = LocationData.TerrainBases(tilesheet, index, position, source);

            layer = LocationData.TerrainLayers(tilesheet, index, position, source);

            special = LocationData.TerrainRules(tilesheet, index);

            shadow = LocationData.TerrainShadows(tilesheet, index, shadow);

            fadeout = LocationData.TerrainFadeout(tilesheet, index);

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
            brazier,
            brazierDark,

        }

        public lightTypes lightType;

        public int lightFrame;

        public float lightAmbience = 0.75f;

        public int lightLayer;

        public int lightTimer = -1;

        public float lightScale = 4f;

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

                int type = 4;

                switch (lightType)
                {
                    case lightTypes.brazierDark:
                    case lightTypes.brazier:

                        int brazierTime = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 1000) / 250;

                        int frame = (brazierTime + lightFrame) % 5;

                        Microsoft.Xna.Framework.Vector2 position = new(origin.X - (float)Game1.viewport.X, origin.Y - (float)Game1.viewport.Y);

                        IconData.tilesheets useSheet = lightType == lightTypes.brazier ? IconData.tilesheets.tomb : IconData.tilesheets.lair;

                        b.Draw(
                        Mod.instance.iconData.sheetTextures[useSheet],
                            position,
                            new Microsoft.Xna.Framework.Rectangle(32 + (frame * 32), 0, 32, 32),
                            Microsoft.Xna.Framework.Color.White * 0.75f,
                            0f,
                            new(16),
                            lightScale,
                            0,
                            (origin.Y + 384 + lightLayer) / 10000
                            );

                        position += new Vector2(64);

                        break;

                    case lightTypes.lantern:

                        type = 1;

                        break;

                }

                int id = (int)(origin.X*10000 + origin.Y);

                for(int l = Game1.currentLightSources.Count - 1; l >= 0; l--)
                {
                    LightSource lightSource = Game1.currentLightSources.ElementAt(l);

                    if (lightSource.Identifier == id)
                    {

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
                position + new Vector2(32,0),
                new(empty ? 64 : 0, 0, 32, 64),
                Color.White,
                0f,
                new(16,32),
                3f,
                SpriteEffects.None,
                origin.Y / 10000
            );

            b.Draw(
                Mod.instance.iconData.crateTexture,
                position + new Vector2(34, 8),
                new(empty ? 64 : 0, 0, 32, 64),
                Color.Black * 0.3f,
                0f,
                new(16, 32),
                3f,
                SpriteEffects.None,
                origin.Y / 10000 - 0.0001f
            );

        }

        public void open(GameLocation location)
        {

            spell = new(location, new(series, crate), origin - new Vector2(0,64));

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

        public const string druid_lair_name = "18465_Lair";

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

                case druid_lair_name:

                    location = new Location.Lair(map);

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

            RestoreLocation(map);

            Game1.locations.Add(location);

            Mod.instance.locations.Add(map, location);

        }

        public static int RestoreLocation(string map)
        {

            if (!Context.IsMainPlayer)
            {

                return -1;

            }

            switch (map)
            {

                case druid_spring_name:
                    
                    if (!Mod.instance.save.restoration.ContainsKey(map))
                    {

                        Mod.instance.save.restoration[map] = 0;

                    }

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeWeald))
                    {
                        
                            return 3;
                    
                    }

                    break;

                case druid_graveyard_name:

                    if (!Mod.instance.save.restoration.ContainsKey(map))
                    {

                        Mod.instance.save.restoration[map] = 0;

                    }

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.challengeMists))
                    {
                        
                        return 4;
                    
                    }

                    break;

                case druid_clearing_name:

                    if (!Mod.instance.save.restoration.ContainsKey(map))
                    {

                        Mod.instance.save.restoration[map] = 0;

                    }

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.challengeStars))
                    {
                        
                        return 3;
                    
                    }

                    break;

            }

            return -1;

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

                case IconData.tilesheets.magnolia:


                    switch (key)
                    {

                        case 1:

                            return new(0, 0, 192, 128);

                        case 2:

                            return new(64, 128, 64, 64);

                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:

                            return new(0 + (32 * (key - 3)), 192, 32, 32);

                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:

                            return new(0 + (32 * (key - 9)), 224, 32, 32);

                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:

                            return new(0 + (32 * (key - 15)), 256, 32, 32);

                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:

                            return new(0 + (32 * (key - 21)), 288, 32, 32);

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

                        // lamppost

                        case 19:

                            return new(208, 48, 16, 64);

                        // tomb

                        case 20:

                            return new(0, 160, 48, 80);

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

                            return new(96, 144, 32, 48);

                        // king statues

                        case 26:

                            return new(48, 144, 48, 112);

                        case 27:

                            return new(96, 144, 48, 112);

                        // building structure

                        case 28:

                            return new(144, 112, 96, 112);

                        case 29:

                            return new(144, 224, 96, 128);

                        // building floor:

                        case 30:

                            return new(0, 256, 128, 96);

                        // flowers

                        case 31:

                            return new(0, 352, 16, 32);

                        case 32:

                            return new(16, 352, 16, 32);

                        case 33:

                            return new(32, 352, 16, 32);

                        case 34:

                            return new(48, 352, 16, 32);

                        // candles

                        case 35:

                            return new(64, 352, 16, 32);

                        case 36:

                            return new(80, 352, 16, 32);

                        case 37:

                            return new(96, 352, 16, 32);

                        case 38:

                            return new(112, 352, 16, 32);

                        // flowers

                        case 41:

                            return new(128, 352, 16, 32);

                        case 42:

                            return new(144, 352, 16, 32);

                        case 43:

                            return new(160, 352, 16, 32);

                        case 44:

                            return new(176, 352, 16, 32);

                        // candles

                        case 45:

                            return new(192, 352, 16, 32);

                        case 46:

                            return new(208, 352, 16, 32);

                        case 47:

                            return new(224, 352, 16, 32);

                        case 48:

                            return new(128, 320, 16, 32);
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

                case IconData.tilesheets.magnolia:

                    switch (key)
                    {
                        case 1:

                            return new();

                        case 2:

                            footPrint = 2;

                            break;

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

                        case 1:
                        case 3:
                        case 4:

                            footPrint = 3;

                            break;

                        case 6:

                            footPrint = 4;

                            break;

                        case 28:
                        case 29:

                            return new()
                            {


                            };

                        case 30:

                            return new()
                            {
                                new Vector2(tile.X+1,tile.Y),
                                new Vector2(tile.X+2,tile.Y),
                                new Vector2(tile.X+5,tile.Y),
                                new Vector2(tile.X+6,tile.Y),

                                new Vector2(tile.X+1,tile.Y+1),
                                new Vector2(tile.X+6,tile.Y+1),

                                new Vector2(tile.X+1,tile.Y+2),
                                new Vector2(tile.X+6,tile.Y+2),

                                new Vector2(tile.X+1,tile.Y+3),
                                new Vector2(tile.X+6,tile.Y+3),

                                new Vector2(tile.X+1,tile.Y+4),
                                new Vector2(tile.X+6,tile.Y+4),

                                new Vector2(tile.X+1,tile.Y+5),
                                new Vector2(tile.X+2,tile.Y+5),
                                new Vector2(tile.X+5,tile.Y+5),
                                new Vector2(tile.X+6,tile.Y+5),

                            };

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

                case IconData.tilesheets.graveyard:

                    switch (key)
                    {

                        case 30:

                            return 0.00001f;

                        // flowers

                        case 31:
                        case 33:

                            return (position.Y - 63 + (source.Height * 4)) / 10000;

                        case 32:
                        case 34:

                            return (position.Y - 63 + (source.Height * 4)) / 10000;

                        // candles

                        case 35:
                        case 36:

                            return (position.Y - 63 + (source.Height * 4)) / 10000;

                        case 37:
                        case 38:

                            return (position.Y - 63 + (source.Height * 4)) / 10000;

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

            return (position.Y + (source.Height * 4)) / 10000;

        }

        public static bool TerrainRules(IconData.tilesheets sheet, int key)
        {

            switch (sheet)
            {
                case IconData.tilesheets.spring:

                    switch (key)
                    {
                        case 7:
                        case 8:

                        case 9:
                        case 10:

                        case 11:
                        case 12:

                        case 13:
                        case 14:

                        case 15:
                        case 16:

                            return true;

                    }

                    break;

                case IconData.tilesheets.grove:

                    return true;

                case IconData.tilesheets.magnolia:

                    return true;

                case IconData.tilesheets.clearing:

                    return true;

                case IconData.tilesheets.court:

                    return true;

                case IconData.tilesheets.graveyard:

                    switch (key)
                    {
                        // lamps
                        case 19:

                            return true;
                        // flowers

                        case 31:

                        case 32:

                        case 33:

                        case 34:

                        // candles

                        case 35:

                        case 36:

                        case 37:

                        case 38:

                            return true;
                    }

                    break;

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


                case IconData.tilesheets.magnolia:


                    int offset = 0;

                    switch (Game1.currentSeason)
                    {

                        case "summer": offset = 1;break;

                        case "fall": offset = 2; break;

                        case "winter": offset = 3; break;

                    }

                    if (Game1.player.currentLocation is Clearing && key > 2)
                    {
                        
                        switch (Mod.instance.save.restoration[LocationData.druid_clearing_name])
                        {

                            case 0:

                                return Rectangle.Empty;

                            case 1:

                                if(key < 15)
                                {

                                    key += 12;

                                }

                                break;

                        }

                    }

                    Rectangle magnoliaSource = TerrainRectangles(sheet, key);

                    magnoliaSource.X += 192 * offset;

                    return magnoliaSource;

                case IconData.tilesheets.clearing:

                    Rectangle clearingSource = TerrainRectangles(sheet, key);

                    if(Mod.instance.save.restoration[LocationData.druid_clearing_name] > 2)
                    {

                        clearingSource.X += 144;

                    }

                    return clearingSource;

                case IconData.tilesheets.spring:

                    switch (key)
                    {
                        case 7:
                        case 8:
                        case 9:
                        case 10:

                            if (Mod.instance.save.restoration[LocationData.druid_spring_name] < 1)
                            {

                                return Rectangle.Empty;

                            }

                            break;

                        case 11:
                        case 12:
                        case 13:
                        case 14:

                            if (Mod.instance.save.restoration[LocationData.druid_spring_name] < 2)
                            {

                                return Rectangle.Empty;

                            }

                            break;

                        case 15:
                        case 16:

                            if (Mod.instance.save.restoration[LocationData.druid_spring_name] < 3)
                            {

                                return Rectangle.Empty;

                            }

                            break;

                    }

                    break;

                case IconData.tilesheets.grove:

                    if (Game1.timeOfDay > 1700)
                    {

                        source.Y += 96;

                    }

                    break;

                case IconData.tilesheets.graveyard:

                    switch (key)
                    {
                        case 19:

                            if(Game1.timeOfDay >= 1900)
                            {
                                return new(224, 48, 16, 64);

                            }

                            break;

                        // flowers

                        case 31:
                        case 32:
                        case 33:
                        case 34:

                            if (Mod.instance.save.restoration[LocationData.druid_graveyard_name] < 2)
                            {

                                return TerrainRectangles(sheet, key+10);

                            }
                            else
                            if (Mod.instance.save.restoration[LocationData.druid_graveyard_name] < 3)
                            {

                                return Rectangle.Empty;

                            }

                            break;

                        // candles

                        case 35:
                        case 36:
                        case 37:
                        case 38:


                            if (Mod.instance.save.restoration[LocationData.druid_graveyard_name] < 1)
                            {

                                return TerrainRectangles(sheet, key + 10);

                            }
                            else
                            if (Mod.instance.save.restoration[LocationData.druid_graveyard_name] < 4)
                            {

                                return Rectangle.Empty;

                            }

                            break;
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

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.questAldebaran))
                            {

                                return Rectangle.Empty;

                            }

                            break;

                    }

                    break;

            }

            return source;

        }

        public static TerrainTile.shadows TerrainShadows(IconData.tilesheets sheet, int key, TerrainTile.shadows shadow)
        {

            switch (sheet)
            {

                case IconData.tilesheets.atoll:

                    switch (key)
                    {

                        case 5:

                            return TerrainTile.shadows.none;

                    }

                    return TerrainTile.shadows.reflection;

                case IconData.tilesheets.magnolia:

                    switch (key)
                    {

                        case 1:

                            return TerrainTile.shadows.none;


                        case 2:
                            
                            return TerrainTile.shadows.massive;

                    }

                    return TerrainTile.shadows.dropset;

                case IconData.tilesheets.outdoors:

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

                case IconData.tilesheets.outdoorsTwo:

                    switch (key)
                    {

                        case 2:

                            return TerrainTile.shadows.bigleafy;

                    }

                    return TerrainTile.shadows.none;

                case IconData.tilesheets.spring:

                    switch (key)
                    {

                        case 1:

                            return TerrainTile.shadows.none;

                        case 2:
                        case 3:
                        case 4:
                        case 5:

                            return TerrainTile.shadows.reflection;

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

                            return TerrainTile.shadows.none;

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
                        case 2:

                            return 0.45f;

                    }

                    return 0.35f;

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

                case IconData.tilesheets.graveyard:

                    switch (key)
                    {
                        case 28:

                            return 0.25f;

                    }

                    break;


            }

            return 0.75f;

        }

    }

}
