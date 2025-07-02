using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location.Terrain
{
    public class SpringLily : TerrainField
    {

        public enum LilySource
        {
            pad,
            flower,
        }

        public LilySource lilyIndex = LilySource.pad;

        public float floatation;

        public SpringLily(IconData.tilesheets Tilesheet, Vector2 Position, LilySource LilyIndex)
            : base()
        {

            tilesheet = IconData.tilesheets.waterflowers;

            index = 1;

            position = Position;

            color = Color.White;

            shadow = TerrainField.shadows.none;

            lilyIndex = LilyIndex;

            reset();

        }

        public override void reset()
        {

            Vector2 tile = ModUtility.PositionToTile(position);

            where = (int)tile.X % 50;

            switch (lilyIndex)
            {

                case LilySource.pad:

                    int randomIndex = Mod.instance.randomIndex.Next(8);

                    source = new(32 * randomIndex, 0, 32, 32);

                    layer = (position.Y + 1 + (source.Height * 4)) / 10000;

                    break;

                case LilySource.flower:

                    source = new(32 * Mod.instance.randomIndex.Next(4), 32, 32, 32);

                    layer = (position.Y + 1 + (source.Height * 4)) / 10000;

                    break;

            }

            flip = Mod.instance.randomIndex.Next(2) == 0;

            bounds = new((int)position.X + 8, (int)position.Y, source.Width * 4 - 16, source.Height * 4 - backing);

        }

        public override void update(GameLocation location)
        {

            if (disabled)
            {

                return;

            }

            floatation = Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.lilyFloat);

            switch (lilyIndex)
            {

                case LilySource.flower:

                    string id = "18465_lily_" + (position.X * 10000 + position.Y).ToString();

                    if (!Game1.currentLightSources.ContainsKey(id))
                    {

                        LightSource light = new LightSource(id, 4, position + new Vector2(64, 32), 0.2f, Color.Black * 0.75f);

                        Game1.currentLightSources.Add(id, light);

                    }

                    break;

            }

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            switch (lilyIndex)
            {

                case LilySource.pad:

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0,80), source, Color.Black * 0.15f, 0f, Vector2.Zero, 3.2f, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, floatation), source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    break;

                case LilySource.flower:

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, 80), source, Color.Black * 0.15f, 0f, Vector2.Zero, 3.2f, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0,floatation), source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    DrawReflection(b, origin + new Vector2(0, 72 + floatation), source, fade);

                    break;

            }

        }

    }

}
