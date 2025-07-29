using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Location.Druid;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Dimensions;
using xTile.Tiles;

namespace StardewDruid.Location.Terrain
{

    public class RitualCircle : TerrainField
    {

        public int ritualStatus = 0;

        public Color ritualColour = Color.White;

        public Color ritualBase = Color.White;

        public Dictionary<int, Candle> candles = new();

        public Dictionary<int, Vector2> candleSpots = new()
        {

            [0] = new Vector2(32, 12) * 4,
            [1] = new Vector2(80, 12) * 4,
            [2] = new Vector2(16, 44) * 4,
            [3] = new Vector2(96, 44) * 4,
            [4] = new Vector2(32, 76) * 4,
            [5] = new Vector2(80, 76) * 4,
        };

        public RitualCircle(Vector2 Position)
            : base()
        {

            tilesheet = IconData.tilesheets.ritual;

            index = 1;

            position = Position - new Vector2(192,128);

            color = Color.White;

            shadow = shadows.none;

            flip = false;

            reset();

        }

        public override void reset()
        {

            source = new(0, 0, 112, 96);

            bounds = new((int)position.X + 8, (int)position.Y, source.Width * 4 - 16, source.Height * 4 - backing);

            layer = 0.0064f;

        }

        public override void update(GameLocation location)
        {

            switch (ritualStatus)
            {

                default:
                case 0:

                    disabled = true;

                    return;

                case 1:

                    if (Mod.instance.activeEvent.Count == 0)
                    {

                        ritualStatus = 0;

                        disabled = true;

                        SetCandles(0);

                        return;

                    }

                    disabled = false;

                    ritualColour = color * fadeout;

                    ritualBase = Color.White * fadeout;

                    break;

                case 2:

                    disabled = false;

                    ritualColour = color * fadeout;

                    ritualBase = Color.White * fadeout;

                    break;

            }

            foreach (KeyValuePair<int, Candle> candle in candles)
            {

                candle.Value.update(location);

            }

        }

        public void LoadCandles(int setto = 2)
        {

            candles = new()
            {
                [0] = new((Candle.candleFrames)Mod.instance.randomIndex.Next(7), position + candleSpots[0],Mod.instance.randomIndex.NextBool()),
                [1] = new((Candle.candleFrames)Mod.instance.randomIndex.Next(7), position + candleSpots[1], Mod.instance.randomIndex.NextBool()),
                [2] = new((Candle.candleFrames)Mod.instance.randomIndex.Next(7), position + candleSpots[2], Mod.instance.randomIndex.NextBool()),
                [3] = new((Candle.candleFrames)Mod.instance.randomIndex.Next(7), position + candleSpots[3], Mod.instance.randomIndex.NextBool()),
                [4] = new((Candle.candleFrames)Mod.instance.randomIndex.Next(7), position + candleSpots[4], Mod.instance.randomIndex.NextBool()),
                [5] = new((Candle.candleFrames)Mod.instance.randomIndex.Next(7), position + candleSpots[5], Mod.instance.randomIndex.NextBool()),
            };

            SetCandles(setto);

        }
        public void ClearCandles()
        {

            foreach (KeyValuePair<int, Candle> candle in candles)
            {

                candle.Value.RemoveLight();

            }

            candles.Clear();

        }

        public void SetCandles(int setto = 2)
        {

            foreach (KeyValuePair<int, Candle> candle in candles)
            {

                candle.Value.status = setto;

                switch (setto)
                {

                    case 0:

                        candle.Value.RemoveLight();

                        break;

                    case 1: // always lit

                        break;

                    default: // timer

                        break;

                }

            }

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {


            if (!DrawCheck())
            {

                return;

            }

            Microsoft.Xna.Framework.Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, ritualBase, 0f, Vector2.Zero, 4, 0, layer);

            useSource.X += 112;

            b.Draw(Mod.instance.iconData.sheetTextures[tilesheet], origin, useSource, ritualColour, 0f, Vector2.Zero, 4, 0, layer + 0.0001f);

            foreach (KeyValuePair<int, Candle> candle in candles)
            {

                candle.Value.draw(b, location, layer + 0.0002f);

            }

        }

    }

}
