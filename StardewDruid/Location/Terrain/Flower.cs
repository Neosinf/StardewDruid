using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Location.Druid;
using StardewValley;
using StardewValley.Extensions;
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
            rose,
            debris,
            heath,
        }

        public grasstypes type;

        public int season;

        public float fade;

        public int where;

        public Flower(Vector2 Position, grasstypes Type = grasstypes.flower)
        {

            tilesheet = IconData.tilesheets.flowers;

            index = 1;

            position = Position + new Vector2(24 + 8 * Mod.instance.randomIndex.Next(3), 0);

            season = 2;

            type = Type;

            reset();

        }

        public override void reset()
        {

            Vector2 tile = ModUtility.PositionToTile(position);

            where = (int)tile.X % 50;

            baseTiles.Clear();

            bounds = new((int)position.X, (int)position.Y, 64, 64);

            center = bounds.Center.ToVector2();

            girth = 64;

            clearance = 1;

            flip = Mod.instance.randomIndex.Next(2) != 0;

            source = new(Mod.instance.randomIndex.Next(3) * 16, (int)type * 32,16,32);

            layer = (position.Y+ 32) / 10000;

        }

        public void Fade(GameLocation location)
        {

            fade = 1f;

            foreach (Farmer character in location.farmers)
            {

                if (bounds.Contains(character.Position.X, character.Position.Y))
                {

                    fade = 0.35f;

                    return;

                }

            }

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 192))
            {

                return;

            }

            Rectangle useSource = new Rectangle((int)source.X, (int)source.Y, 16, 32);

            if (ruined)
            {

                switch (type)
                {

                    case grasstypes.flower:

                        return;

                    case grasstypes.rose:

                        useSource.Y += 64;

                        return;

                }

            }

            Season useSeason = Game1.season;

            if (season != -1)
            {

                useSeason = (Season)season;

            }

            switch (useSeason)
            {

                case Season.Summer: useSource.X += 48; break;

                case Season.Fall: useSource.X += 96; break;

                case Season.Winter: useSource.X += 144; break;

            }
            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Fade(location);

            float rotate = Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.leafRotate);

            Vector2 place = new(Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.leafOffset), 0);

            Vector2 usePosition = origin + place;

            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                usePosition,
                useSource,
                Color.White * fade,
                rotate,
                new Vector2(8,16),
                4,
                flip ? (SpriteEffects)1 : 0,
                layer
            );

            b.Draw(
                Mod.instance.iconData.sheetTextures[IconData.tilesheets.flowers],
                usePosition + new Vector2(4,8),
                useSource,
                Color.Black * 0.25f,
                rotate,
                new Vector2(8, 16),
                4,
                flip ? (SpriteEffects)1 : 0,
                0.001f
            );

        }

    }

}
