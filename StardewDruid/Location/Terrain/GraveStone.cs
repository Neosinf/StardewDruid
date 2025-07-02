using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewValley;
using StardewValley.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Location.Terrain
{

    public class GraveStone : TerrainField
    {

        public Dictionary<int, Candle> candles = new();

        public int status = 0;

        public GraveStone(Vector2 Position, int Index)
            : base()
        {

            tilesheet = StardewDruid.Data.IconData.tilesheets.graveyard;

            index = Index;

            position = Position;

            color = Color.White;

            shadow = shadows.deepset;

            flip = false;

            shade = 0.3f;

            fadeout = 0.75f;

            reset();

        }

        public virtual Rectangle Source()
        {

            switch (index)
            {

                // small epitaphs

                default:
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

                //case 12:

                //    return new(96, 48, 32, 32);

                // larger sarcophagus

                case 13:

                    return new(0, 80, 32, 48);

                //case 14:

                //    return new(32, 80, 16, 48);

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


        }

        public virtual int Footprint()
        {

            switch (index)
            {

                case 13:
                case 14:
                case 20:
                case 24:
                case 26:
                case 27:

                    return 2;

            }

            return 1;

        }

        public override void reset()
        {

            Vector2 tile = ModUtility.PositionToTile(position);

            source = Source();

            layer = (position.Y - 32 + (source.Height * 4)) / 10000;

            bounds = new((int)position.X, (int)position.Y, source.Width * 4, source.Height * 4);

            baseTiles.Clear();

            int footPrint = Footprint();

            Vector2 range = new Vector2(source.Width / 16, source.Height / 16);

            for (int y = 1; y <= footPrint; y++)
            {

                for (int x = 0; x < (int)range.X; x++)
                {

                    baseTiles.Add(new Vector2(tile.X + x, tile.Y + ((int)range.Y - y)));

                }

            }

        }

        public override void update(GameLocation location)
        {

            if (disabled)
            {

                return;

            }

            Fadeout(location);

            foreach (KeyValuePair<int, Candle> candle in candles)
            {
                candle.Value.update(location);

            }

        }

        public void LoadCandles()
        {

            Vector2 spot = CandleSpot();

            if(spot == Vector2.Zero)
            {

                return;

            }

            Candle candle = new((Candle.candleFrames)Mod.instance.randomIndex.Next(7), spot, Mod.instance.randomIndex.NextBool());

            candle.status = 2;

            candles = new()
            {
                
                [0] = candle,

            };

        }

        public Vector2 CandleSpot()
        {

            switch (index)
            {

                // small epitaphs

                default:
                case 8:
                case 9:
                case 10:

                    if (Mod.instance.randomIndex.Next(3) != 0)
                    {

                        return Vector2.Zero;

                    }

                    return position + new Vector2(32,120);

                // wide sarcophagus

                case 11:

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        return position + new Vector2(40, 24);

                    }

                    return position + new Vector2(96, 24);

                // long sarcophagus

                case 13:
                case 21:
                case 25:

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        return position + new Vector2(40, 84);

                    }

                    return position + new Vector2(24, 180);

                // larger epitaphs

                case 15:
                case 16:
                case 22:
                case 23:

                    switch (Mod.instance.randomIndex.Next(3))
                    {

                        case 0:

                            return position + new Vector2(96, 96);
                        case 1:

                            return position + new Vector2(24, 112);

                        default:

                            return Vector2.Zero;

                    }

                // gargoyle

                case 17:
                case 18:

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        return position + new Vector2(48, 216);

                    }

                    return position + new Vector2(16, 240);

                // tomb

                case 20:

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        return position + new Vector2(128, 292);

                    }

                    return position + new Vector2(64, 292);

                // king statues

                case 26:

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        return position + new Vector2(136, 324);

                    }

                    return position + new Vector2(48, 400);


                case 27:

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        return position + new Vector2(56, 324);

                    }

                    return position + new Vector2(144, 400);

            }


        }

        public void RuinCandles()
        {

            foreach (KeyValuePair<int, Candle> candle in candles)
            {

                candle.Value.disabled = false;

                candle.Value.ruined = true;

                candle.Value.RemoveLight();

            }

        }

        public void HideCandles()
        {

            foreach (KeyValuePair<int, Candle> candle in candles)
            {

                candle.Value.disabled = true;

                candle.Value.ruined = false;

                candle.Value.RemoveLight();

            }
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

                candle.Value.disabled = false;

                candle.Value.ruined = false;

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

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 512))
            {

                return;

            }

            Microsoft.Xna.Framework.Rectangle useSource = new(source.X, source.Y, source.Width, source.Height);

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.graveyard], origin + new Vector2(0, 8), useSource, Color.Black * shade * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer - 0.001f);

            b.Draw(Mod.instance.iconData.sheetTextures[IconData.tilesheets.graveyard], origin, useSource, color * fade, 0f, Vector2.Zero, 4, flip ? (SpriteEffects)1 : 0, layer);

            foreach (KeyValuePair<int, Candle> candle in candles)
            {

                candle.Value.draw(b, location, layer);

            }

        }

    }

}
