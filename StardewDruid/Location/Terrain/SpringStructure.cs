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
    public class SpringStructure : TerrainField
    {

        public enum SpringSource
        {
            gate,
            lantern,
            shrine,
            attendant,
            bench,
            bridge,
            steephouse,
            batchhouse,
            packhouse,
        }

        public SpringSource springIndex = SpringSource.gate;

        public Rectangle shadowSource;

        public Vector2 shadowOffset;

        public Rectangle reflectionSource;

        public Vector2 reflectionOffset;

        public SpringStructure(IconData.tilesheets Tilesheet, Vector2 Position, SpringSource SpringIndex)
            : base()
        {

            tilesheet = IconData.tilesheets.spring;

            index = 1;

            position = Position;

            color = Color.White;

            shadow = TerrainField.shadows.none;

            springIndex = SpringIndex;

            reset();

        }

        public override void reset()
        {

            source = new(0, 0, 192, 128);

            switch (springIndex)
            {

                case SpringSource.gate:

                    source = new(0, 0, 192, 64);

                    shadowSource = new(0, 64, 192, 32);

                    shadowOffset = new(0, 8 * 64 - 32);

                    break;

                case SpringSource.shrine:

                    source = new(48, 96, 96, 112);

                    shadowSource = new(48, 208, 96, 32);

                    shadowOffset = new(0, 5 * 64);

                    reflectionSource = new(48, 144, 96, 40);

                    reflectionOffset = new Vector2(0, 352);

                    break;

                case SpringSource.bridge:

                    source = new(0, 288, 192, 80);

                    shadowSource = new(0, 368, 192, 64);

                    shadowOffset = new(0, 3 * 64);

                    reflectionSource = new(16, 288, 160, 80);

                    reflectionOffset = new Vector2(64, 336);

                    break;

                case SpringSource.lantern:

                    source = new(16, 96, 32, 80);

                    shadowSource = new(16, 172, 32, 32);

                    shadowOffset = new(0, 4 * 64);

                    reflectionSource = new(16, 96, 32, 64);

                    reflectionOffset = new Vector2(0, 256);

                    break;

                case SpringSource.attendant:

                    source = new(192, 0, 48, 64);

                    shadowSource = new(192, 64, 48, 16);

                    shadowOffset = new(0, 200);

                    baseTiles = LocationHandle.TerrainBases(tilesheet, index, position, source);

                    break;

                case SpringSource.bench:

                    source = new(240, 0, 48, 64);

                    shadowSource = new(192, 64, 48, 16);

                    shadowOffset = new(0, 200);

                    baseTiles = LocationHandle.TerrainBases(tilesheet, index, position, source);

                    break;

                case SpringSource.steephouse:

                    source = new(192, 80, 32, 48);

                    shadowSource = new(16, 172, 32, 32);

                    shadowOffset = new(0, 120);

                    baseTiles = LocationHandle.TerrainBases(tilesheet, index, position, source);

                    break;

                case SpringSource.batchhouse:

                    source = new(224, 80, 32, 48);

                    shadowSource = new(16, 172, 32, 32);

                    shadowOffset = new(0, 120);

                    baseTiles = LocationHandle.TerrainBases(tilesheet, index, position, source);

                    break;


                case SpringSource.packhouse:

                    source = new(256, 80, 32, 48);

                    shadowSource = new(16, 172, 32, 32);

                    shadowOffset = new(0, 120);

                    baseTiles = LocationHandle.TerrainBases(tilesheet, index, position, source);

                    break;

            }

            bounds = new((int)position.X + 8, (int)position.Y, source.Width * 4 - 16, source.Height * 4 - backing);

            layer = (position.Y + 1 + (source.Height * 4)) / 10000;

        }


        public override void update(GameLocation location)
        {

            base.update(location);

            switch (springIndex)
            {

                case SpringSource.shrine:


                    string shrineid = "18465_springshrine_" + (position.X * 10000 + position.Y).ToString();

                    if (!Game1.currentLightSources.ContainsKey(shrineid))
                    {

                        LightSource light = new LightSource(shrineid, 4, bounds.Center.ToVector2(), 1f, Color.Black * 0.75f);

                        Game1.currentLightSources.Add(shrineid, light);

                    }

                    break;

                case SpringSource.lantern:

                    string id = "18465_springlantern_" + (position.X * 10000 + position.Y).ToString();

                    if (!Game1.currentLightSources.ContainsKey(id))
                    {

                        LightSource light = new LightSource(id, 4, position + new Vector2(64, 32), 1f, Color.Black * 0.75f);

                        Game1.currentLightSources.Add(id, light);

                    }

                    break;

            }

        }

        public override void drawFront(SpriteBatch b, GameLocation location)
        {

            if (!DrawCheck())
            {

                return;

            }

            switch (springIndex)
            {

                case SpringSource.gate:

                    if (ruined)
                    {

                        return;

                    }

                    Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

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

            switch (springIndex)
            {

                case SpringSource.gate:

                    if (ruined)
                    {

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(128, 256), new(144,96,16,80), color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(576, 256), new(160, 96, 16, 80), color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                        return;

                    }

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + shadowOffset, shadowSource, Color.White * 0.1f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    Rectangle postSource = new(0, 96, 16, 80);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(128, 256), postSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(576, 256), postSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    break;

                case SpringSource.attendant:

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + shadowOffset, shadowSource, Color.White * 0.15f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    if ((int)(Game1.currentGameTime.TotalGameTime.TotalSeconds % 6) > 3)
                    {

                        List<Vector2> eyes = new()
                        {
                            new Vector2(84,160),
                            new Vector2(104,160),
                        };

                        foreach (Vector2 place in eyes)
                        {

                            Vector2 eye = origin + place;

                            b.Draw(Game1.staminaRect, new Vector2(eye.X, eye.Y), new Rectangle((int)eye.X, (int)eye.Y, 4, 4), new Color(0, 174, 239), 0f, Vector2.Zero, 1f, 0, eye.Y / 10000f + 990f);

                        }

                    }

                    break;

                case SpringSource.bench:
                case SpringSource.packhouse:
                case SpringSource.batchhouse:
                case SpringSource.steephouse:

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + shadowOffset, shadowSource, Color.White * 0.15f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    break;

                case SpringSource.bridge:

                    if (ruined)
                    {

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(128, 128), new(176, 128, 16, 48), color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(576, 128), new(176, 128, 16, 48), color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(128, 256), new(176, 128, 16, 48), color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer + 0.0001f);

                        b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(576, 256), new(176, 128, 16, 48), color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer + 0.0001f);


                        return;

                    }

                    DrawReflection(b, origin + reflectionOffset, reflectionSource, fade);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + shadowOffset, shadowSource, Color.White * 0.15f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0127f);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0194f);

                    Rectangle bridgeSource = new(source.X, source.Y - 48, source.Width, 48);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, 128), bridgeSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.0126f);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + new Vector2(0, 128), source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    break;

                case SpringSource.shrine:

                    DrawReflection(b, origin + reflectionOffset, reflectionSource, fade);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + shadowOffset, shadowSource, Color.White * 0.15f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    break;
                    
                default:

                    DrawReflection(b, origin + reflectionOffset, reflectionSource, fade);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin + shadowOffset, shadowSource, Color.White * 0.15f, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

                    b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, source, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

                    break;

            }

        }

    }

}
