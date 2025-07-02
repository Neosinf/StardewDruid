using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Location.Druid;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Location.Terrain
{

    public class Flower : TerrainField
    {

        public enum grasstypes
        {
            flower,
            flowertwo,
            rose,
            debris,
            grass,
            grasstwo,
            heath,
            weed,
            weedtwo,
            shadow,
        }

        public grasstypes type;

        public int season;

        public Microsoft.Xna.Framework.Rectangle shadowSource;

        public bool stabilised;

        public Flower(Vector2 Position, grasstypes Type = grasstypes.flower)
        {

            tilesheet = IconData.tilesheets.flowers;

            index = 1;

            int quirk = Mod.instance.randomIndex.Next(3);

            position = Position + new Vector2(26 + 6 * quirk, quirk);

            season = -1;

            type = Type;

            reset();

        }

        public override void reset()
        {

            Vector2 tile = ModUtility.PositionToTile(position);

            where = (int)tile.X % 50;

            baseTiles.Clear();

            bounds = new((int)position.X -32, (int)position.Y-48, 64, 96);

            center = bounds.Center.ToVector2();

            girth = 64;

            clearance = 1;

            flip = Mod.instance.randomIndex.Next(2) != 0;

            switch (type)
            {
                default:

                    source = new(Mod.instance.randomIndex.Next(3) * 16, (int)type * 32, 16, 32);

                    shadowSource = new(Mod.instance.randomIndex.Next(3) * 16, (int)grasstypes.shadow * 32, 16, 32);

                    break;

                case grasstypes.grass:
                case grasstypes.grasstwo:

                    source = new(Mod.instance.randomIndex.Next(3) * 16, (int)type * 32, 16, 32);

                    shadowSource = new(48 + Mod.instance.randomIndex.Next(3) * 16, (int)grasstypes.shadow * 32, 16, 32);

                    break;

                case grasstypes.debris:

                    source = new(Mod.instance.randomIndex.Next(3) * 16, (int)type * 32, 16, 32);

                    stabilised = true;

                    break;

                case grasstypes.heath:

                    source = new(Mod.instance.randomIndex.Next(6) * 16, (int)type * 32, 16, 32);

                    shadowSource = new(Mod.instance.randomIndex.Next(3) * 16, (int)grasstypes.shadow * 32, 16, 32);

                    break;

            }

            layer = (position.Y+ 32) / 10000;

        }

        public override void update(GameLocation location)
        {

            if (disabled || stabilised)
            {

                return;

            }

            wind = 0f;

            windout = 0f;

            if (shake > 0)
            {

                bool hold = false;

                if(shake <= 35)
                {

                    bool farmerPresent = false;

                    foreach (Farmer character in location.farmers)
                    {

                        if (bounds.Contains(character.Position.X + 32, character.Position.Y + 32))
                        {

                            farmerPresent = true; 
                            
                            break;

                        }

                    }

                    if (farmerPresent)
                    {

                        if (shake % 10 == 0)
                        {

                            shake = 40;

                        }
                        else if (shake == 35)
                        {

                            hold = true;

                        }

                    }

                }

                if (!hold)
                {

                    shake--;

                }

                switch (type)
                {
                    case grasstypes.flower:
                    case grasstypes.flowertwo:
                    case grasstypes.rose:
                    case grasstypes.heath:

                        wind = Mod.instance.environment.retrieve(shake, EnvironmentHandle.environmentEffect.leafShake);

                        windout = wind * 80;

                        break;

                    case grasstypes.grass:
                    case grasstypes.grasstwo:
                    case grasstypes.weed:
                    case grasstypes.weedtwo:

                        wind = Mod.instance.environment.retrieve(shake, EnvironmentHandle.environmentEffect.grassShake);

                        windout = wind * 80;

                        break;


                }

            }
            else
            if (!ruined)
            {

                foreach (Farmer character in location.farmers)
                {

                    if (bounds.Contains(character.Position.X + 32, character.Position.Y + 32))
                    {

                        shake = 40;

                    }

                }

                switch (type)
                {

                    case grasstypes.flower:
                    case grasstypes.flowertwo:
                    case grasstypes.rose:
                    case grasstypes.heath:

                        wind = Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.leafRotate);

                        windout = wind * 80;

                        break;

                    case grasstypes.grass:
                    case grasstypes.grasstwo:
                    case grasstypes.weed:
                    case grasstypes.weedtwo:

                        wind = Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.grassRotate);

                        windout = wind * 80;

                        break;


                }

            }

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (disabled)
            {

                return;

            }

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 192))
            {

                return;

            }

            Rectangle useSource = new Rectangle((int)source.X, (int)source.Y, 16, 32);

            Season useSeason = Game1.season;

            if (season != -1)
            {

                useSeason = (Season)season;

            }

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            switch (type)
            {
                case grasstypes.flower:
                case grasstypes.flowertwo:
                case grasstypes.rose:

                    switch (useSeason)
                    {

                        case Season.Summer: useSource.X += 48; break;

                        case Season.Fall: useSource.X += 96; break;

                        case Season.Winter: useSource.X += 144; break;

                    }

                    Vector2 place = new(windout, 0);

                    Vector2 usePosition = origin + place;

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                        usePosition,
                        useSource,
                        Color.White,
                        wind,
                        new Vector2(8, 16),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer
                    );

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                        usePosition,
                        shadowSource,
                        Color.White * 0.25f,
                        wind,
                        new Vector2(8,16),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        0.001f
                    );
                    break;

                case grasstypes.heath:

                    Vector2 heathplace = new(windout, 0);

                    Vector2 heathPosition = origin + heathplace;

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                        heathPosition,
                        useSource,
                        Color.White,
                        wind,
                        new Vector2(8, 16),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer
                    );

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                        heathPosition,
                        shadowSource,
                        Color.White * 0.2f,
                        wind,
                        new Vector2(8, 16),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        0.001f
                    );
                    break;

                case grasstypes.grass:
                case grasstypes.grasstwo:

                    switch (useSeason)
                    {

                        case Season.Summer: useSource.X += 48; break;

                        case Season.Fall: useSource.X += 96; break;

                        case Season.Winter: useSource.X += 144; break;

                    }

                    Vector2 grassplace = new(windout, 0);

                    Vector2 grassPosition = origin + grassplace;

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                        grassPosition,
                        useSource,
                        Color.White,
                        wind,
                        new Vector2(8, 16),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer
                    );

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                        grassPosition,
                        shadowSource,
                        Color.White * 0.25f,
                        wind,
                        new Vector2(8, 16),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        0.001f
                    );
                    break;

                case grasstypes.debris:

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                        origin,
                        useSource,
                        Color.White,
                        0f,
                        new Vector2(8, 16),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer
                    );

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                        origin + new Vector2(4, 8),
                        useSource,
                        Color.Black * 0.25f,
                        0f,
                        new Vector2(8, 16),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        0.001f
                    );
                    break;

                case grasstypes.weed:
                case grasstypes.weedtwo:

                    switch (useSeason)
                    {

                        case Season.Summer: useSource.X += 48; break;

                        case Season.Fall: useSource.X += 96; break;

                        case Season.Winter: useSource.X += 144; break;

                    }

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                        origin,
                        useSource,
                        Color.White,
                        wind,
                        new Vector2(8, 16),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer - 0.0024f
                    );

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                        origin + new Vector2(1, 4),
                        useSource,
                        Color.Black * 0.25f,
                        wind,
                        new Vector2(8, 16),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        0.001f
                    );

                    break;

            }

        }

    }

}
