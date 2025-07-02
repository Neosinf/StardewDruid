using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Location.Druid;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Objects;
using StardewValley.Quests;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Tiles;

namespace StardewDruid.Location.Terrain
{

    public class Magnolia : TerrainField
    {

        public Dictionary<Vector2, MagnoliaLeaf> leaves = new();

        public Dictionary<Vector2, MagnoliaLeaf> litter = new();

        public int size;

        public bool littered;

        public int season;

        public Magnolia(Vector2 Position, int Size = 1)
           : base()
        {

            tilesheet = IconData.tilesheets.magnolia;

            index = 1;

            position = Position;

            size = Size;

            season = -1;

            fadeout = 0.5f;

            reset();

        }

        public override void reset()
        {

            Vector2 tile = ModUtility.PositionToTile(position);

            where = (int)tile.X % 50;

            baseTiles.Clear();

            switch (size)
            {

                default:
                case 1:

                    for (int y = 1; y <= 2; y++)
                    {

                        for (int x = 4; x < 8; x++)
                        {

                            baseTiles.Add(new Vector2(tile.X + x, tile.Y + (12 - y)));

                        }

                    }

                    layer = (position.Y / 10000) + (0.0064f * 11.5f);

                    bounds = new((int)position.X, (int)position.Y, 768, 512);

                    Leaves();

                    break;

                case 2:

                    baseTiles.Add(new Vector2(tile.X + 2, tile.Y + 5));

                    baseTiles.Add(new Vector2(tile.X + 3, tile.Y + 5));

                    layer = (position.Y / 10000) + (0.0064f * 5.5f);

                    bounds = new((int)position.X, (int)position.Y, 5 * 64, 4 * 64);

                    LeavesMedium();

                    break;


            }

            if (littered)
            {

                AddLitter();

            }

            Vector2 corner00 = baseTiles.First() * 64;

            Vector2 corner11 = baseTiles.Last() * 64;

            center = corner00 + (corner11 - corner00) / 2;

            girth = Vector2.Distance(corner00, center);

            clearance = (int)Math.Ceiling(girth / 64);

        }

        public void Leaves()
        {

            leaves.Clear();

            leaves = new()
            {

                // ----------------------
                [new Vector2(44+ 32, 0)] = new(MagnoliaLeaf.magnoliatiers.top, 16, 0),
                [new Vector2(596 + 32, 0)] = new(MagnoliaLeaf.magnoliatiers.top, 16, 1),

                [new Vector2(156 + 32, -28)] = new(MagnoliaLeaf.magnoliatiers.top, 16, 2),
                [new Vector2(484 + 32, -28)] = new(MagnoliaLeaf.magnoliatiers.top, 16, 3),

                [new Vector2(268 + 32, -40)] = new(MagnoliaLeaf.magnoliatiers.top, 16, 4),
                [new Vector2(372 + 32, -40)] = new(MagnoliaLeaf.magnoliatiers.top, 16, 5),

                // ----------------------
                [new Vector2(-16 + 32, 60)] = new(MagnoliaLeaf.magnoliatiers.topleft, 16, 0),
                [new Vector2(656 + 32, 60)] = new(MagnoliaLeaf.magnoliatiers.topright, 16, 1),

                [new Vector2(92 + 32, 44)] = new(MagnoliaLeaf.magnoliatiers.top, 8, 2),
                [new Vector2(548 + 32, 44)] = new(MagnoliaLeaf.magnoliatiers.top, 8, 3),

                [new Vector2(204 + 32, 24)] = new(MagnoliaLeaf.magnoliatiers.top, 8, 4),
                [new Vector2(436 + 32, 24)] = new(MagnoliaLeaf.magnoliatiers.top, 8, 5),

                [new Vector2(320 + 32, 12)] = new(MagnoliaLeaf.magnoliatiers.top, 0, 6),

                // ----------------------
                [new Vector2(-64 + 32, 104)] = new(MagnoliaLeaf.magnoliatiers.topleft, 16, 0),
                [new Vector2(704 + 32, 104)] = new(MagnoliaLeaf.magnoliatiers.topright, 16, 1),

                [new Vector2(36 + 32, 96)] = new(MagnoliaLeaf.magnoliatiers.top, 8, 2),
                [new Vector2(604 + 32, 96)] = new(MagnoliaLeaf.magnoliatiers.top, 8, 3),

                [new Vector2(156 + 32, 84)] = new(MagnoliaLeaf.magnoliatiers.top, 0, 4),
                [new Vector2(484 + 32, 84)] = new(MagnoliaLeaf.magnoliatiers.top, 0, 5),

                [new Vector2(268 + 32, 72)] = new(MagnoliaLeaf.magnoliatiers.top, 0, 6),
                [new Vector2(372 + 32, 72)] = new(MagnoliaLeaf.magnoliatiers.top, 0, 7),

                // ----------------------
                [new Vector2(-96 + 32, 168)] = new(MagnoliaLeaf.magnoliatiers.topleft, 16, 0),
                [new Vector2(740 + 32, 168)] = new(MagnoliaLeaf.magnoliatiers.topright, 16,1),

                [new Vector2(-8 + 32, 160)] = new(MagnoliaLeaf.magnoliatiers.top, 8,2),
                [new Vector2(648 + 32, 160)] = new(MagnoliaLeaf.magnoliatiers.top, 8,3),

                [new Vector2(104 + 32, 148)] = new(MagnoliaLeaf.magnoliatiers.top, 0,4),
                [new Vector2(536 + 32, 148)] = new(MagnoliaLeaf.magnoliatiers.top, 0,5),

                [new Vector2(212 + 32, 140)] = new(MagnoliaLeaf.magnoliatiers.top, 0,6),
                [new Vector2(428 + 32, 140)] = new(MagnoliaLeaf.magnoliatiers.top, 0,7),

                [new Vector2(320 + 32, 132)] = new(MagnoliaLeaf.magnoliatiers.top, 0,8),

                // ----------------------
                [new Vector2(-144 + 32, 216)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 24, 0),
                [new Vector2(780 + 32, 216)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 24, 1),

                [new Vector2(-52 + 32, 216)] = new(MagnoliaLeaf.magnoliatiers.topleft, 16, 2),
                [new Vector2(692 + 32, 216)] = new(MagnoliaLeaf.magnoliatiers.topright, 16, 3),

                [new Vector2(56 + 32, 212)] = new(MagnoliaLeaf.magnoliatiers.top, 8, 4),
                [new Vector2(584 + 32, 212)] = new(MagnoliaLeaf.magnoliatiers.top, 8, 5),

                [new Vector2(160 + 32, 208)] = new(MagnoliaLeaf.magnoliatiers.top, 8, 6),
                [new Vector2(480 + 32, 208)] = new(MagnoliaLeaf.magnoliatiers.top, 8, 7),

                [new Vector2(268 + 32, 204)] = new(MagnoliaLeaf.magnoliatiers.top, 0, 8),
                [new Vector2(372 + 32, 204)] = new(MagnoliaLeaf.magnoliatiers.top, 0, 9),

                // ----------------------
                [new Vector2(-96 + 32, 256)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 24, 0),
                [new Vector2(736 + 32, 256)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 24, 1),

                [new Vector2(-4 + 32, 260)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 2),
                [new Vector2(644 + 32, 260)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 3),

                [new Vector2(100 + 32, 264)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 4),
                [new Vector2(540 + 32, 264)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 5),

                [new Vector2(204 + 32, 268)] = new(MagnoliaLeaf.magnoliatiers.mid, 8, 6),
                [new Vector2(436 + 32, 268)] = new(MagnoliaLeaf.magnoliatiers.mid, 8, 7),

                [new Vector2(320 + 32, 272)] = new(MagnoliaLeaf.magnoliatiers.mid, 8, 8),

                // ----------------------
                [new Vector2(-132 + 32, 312)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 32, 0),
                [new Vector2(760 + 32, 312)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 32, 1),

                [new Vector2(-56 + 32, 316)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 24, 2),
                [new Vector2(696 + 32, 316)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 24, 3),

                [new Vector2(52 + 32, 324)] = new(MagnoliaLeaf.magnoliatiers.mid, 24, 4),
                [new Vector2(588 + 32, 324)] = new(MagnoliaLeaf.magnoliatiers.mid, 24, 5),

                [new Vector2(156 + 32, 332)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 6),
                [new Vector2(484 + 32, 332)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 7),

                [new Vector2(272 + 32, 340)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 8),
                [new Vector2(368 + 32, 340)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 9),

                // ----------------------
                [new Vector2(-96 + 32, 376)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 40, 0),
                [new Vector2(716 + 32, 376)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 40, 1),

                [new Vector2(-12 + 32, 384)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 32, 2),
                [new Vector2(636 + 32, 384)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 32, 3),

                [new Vector2(96 + 32, 392)] = new(MagnoliaLeaf.magnoliatiers.bottom, 24, 4),
                [new Vector2(540 + 32, 392)] = new(MagnoliaLeaf.magnoliatiers.bottom, 24, 5),

                [new Vector2(200 + 32, 404)] = new(MagnoliaLeaf.magnoliatiers.bottom, 24, 6),
                [new Vector2(424 + 32, 404)] = new(MagnoliaLeaf.magnoliatiers.bottom, 24, 7),

                [new Vector2(320 + 32, 416)] = new(MagnoliaLeaf.magnoliatiers.bottom, 16, 8),

                // ----------------------
                [new Vector2(-40 + 32, 444)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 48, 0),
                [new Vector2(680 + 32, 444)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 48, 1),

                [new Vector2(52 + 32, 456)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 40, 2),
                [new Vector2(588 + 32, 456)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 40, 3),

                [new Vector2(156 + 32, 468)] = new(MagnoliaLeaf.magnoliatiers.bottom, 32, 4),
                [new Vector2(484 + 32, 468)] = new(MagnoliaLeaf.magnoliatiers.bottom, 32, 5),

                [new Vector2(272 + 32, 484)] = new(MagnoliaLeaf.magnoliatiers.bottom, 24, 6),
                [new Vector2(368 + 32, 484)] = new(MagnoliaLeaf.magnoliatiers.bottom, 24, 7),

            };

            foreach(KeyValuePair<Vector2,MagnoliaLeaf> leave in leaves)
            {

                leave.Value.where = Math.Abs((int)(leave.Key.X / 64)) % 50;

            }

        }

        public void LeavesMedium()
        {

            leaves.Clear();

            leaves = new()
            {
                // ----------------------

                [new Vector2(64, -40)] = new(MagnoliaLeaf.magnoliatiers.topleft, 8,0),
                [new Vector2(320, -40)] = new(MagnoliaLeaf.magnoliatiers.topright, 8,1),

                [new Vector2(144, -56)] = new(MagnoliaLeaf.magnoliatiers.top, 0,2),
                [new Vector2(240, -56)] = new(MagnoliaLeaf.magnoliatiers.top, 0,3),

                // ----------------------

                [new Vector2(16, 16)] = new(MagnoliaLeaf.magnoliatiers.topleft, 8,0),
                [new Vector2(368, 16)] = new(MagnoliaLeaf.magnoliatiers.topright, 8,1),

                [new Vector2(114, 8)] = new(MagnoliaLeaf.magnoliatiers.mid, 0, 2),
                [new Vector2(280, 8)] = new(MagnoliaLeaf.magnoliatiers.mid, 0, 3),

                [new Vector2(192, 0)] = new(MagnoliaLeaf.magnoliatiers.mid, 0, 4),

                // ----------------------

                [new Vector2(-20, 68)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 16, 0),
                [new Vector2(408, 68)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 16, 1),

                [new Vector2(56, 72)] = new(MagnoliaLeaf.magnoliatiers.mid, 8, 2),
                [new Vector2(328, 72)] = new(MagnoliaLeaf.magnoliatiers.mid, 8, 3),

                [new Vector2(144, 76)] = new(MagnoliaLeaf.magnoliatiers.mid, 8, 4),
                [new Vector2(240, 76)] = new(MagnoliaLeaf.magnoliatiers.mid, 8, 5),

                // ----------------------
                [new Vector2(-64, 132)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 24, 0),
                [new Vector2(448, 132)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 24, 1),

                [new Vector2(16, 144)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 24, 0),
                [new Vector2(368, 144)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 24, 1),

                [new Vector2(104, 152)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 2),
                [new Vector2(280, 152)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 3),

                [new Vector2(192, 160)] = new(MagnoliaLeaf.magnoliatiers.mid, 16, 4),

                // ----------------------
                [new Vector2(-20, 204)] = new(MagnoliaLeaf.magnoliatiers.bottomleft, 16, 0),
                [new Vector2(408, 204)] = new(MagnoliaLeaf.magnoliatiers.bottomright, 16, 1),

                [new Vector2(56, 216)] = new(MagnoliaLeaf.magnoliatiers.bottom, 8, 2),
                [new Vector2(328, 216)] = new(MagnoliaLeaf.magnoliatiers.bottom, 8, 3),

                [new Vector2(144, 224)] = new(MagnoliaLeaf.magnoliatiers.bottom, 8, 4),
                [new Vector2(240, 224)] = new(MagnoliaLeaf.magnoliatiers.bottom, 8, 5),

            };

            foreach (KeyValuePair<Vector2, MagnoliaLeaf> leave in leaves)
            {

                leave.Value.where = Math.Abs((int)(leave.Key.X / 64)) % 50;

            }

        }

        public void AddLitter()
        {

            littered = true;

            litter.Clear();

            switch (size)
            {

                default:
                case 1:

                    double radians = Math.PI / 8;

                    for (int x = 1; x < 9; x++)
                    {

                        double angle = x * radians;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(10,20) * 16;

                        litter.Add(position, new(MagnoliaLeaf.magnoliatiers.litter, 0, 0));


                    }

                    for (int x = 0; x < 8; x++)
                    {

                        double angle = x * -1 * radians;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(10, 20) * 16;

                        litter.Add(position, new(MagnoliaLeaf.magnoliatiers.litter, 0, 0));

                    }

                    break;

                case 2:

                    double radian2 = Math.PI / 5;

                    for (int x = 1; x < 6; x++)
                    {

                        double angle = x * radian2;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(4, 12) * 16;

                        litter.Add(position, new(MagnoliaLeaf.magnoliatiers.litter, 0, 0));


                    }

                    for (int x = 0; x < 5; x++)
                    {

                        double angle = x * -1 * radian2;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(4, 12) * 16;

                        litter.Add(position, new(MagnoliaLeaf.magnoliatiers.litter, 0, 0));

                    }

                    break;


            }


        }

        public override void update(GameLocation location)
        {

            base.update(location);

            wind = 0f;

            windout = 0f;

            if (shake > 0)
            {

                shake--;

                wind = Mod.instance.environment.retrieve(shake, EnvironmentHandle.environmentEffect.trunkShake);

                switch (size)
                {

                    default:
                    case 1:

                        windout = wind * 400;

                        break;

                    case 2:

                        windout = wind * 200;

                        break;

                }

                foreach (KeyValuePair<Vector2, MagnoliaLeaf> leaf in leaves)
                {

                    leaf.Value.wind = 0f;

                    leaf.Value.windout = 0f;

                    leaf.Value.wind = Mod.instance.environment.retrieve(shake, EnvironmentHandle.environmentEffect.leafShake);

                    leaf.Value.windout = windout + leaf.Value.wind * 80f;

                }

            }
            else
            if (!ruined)
            {

                wind = Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.trunkRotate);
                
                switch (size)
                {

                    default:
                    case 1:

                        windout = wind * 400;

                        break;

                    case 2:

                        windout = wind * 200;

                        break;

                }

                foreach (KeyValuePair<Vector2, MagnoliaLeaf> leaf in leaves)
                {

                    leaf.Value.wind = 0f;

                    leaf.Value.windout = 0f;

                    leaf.Value.wind = Mod.instance.environment.retrieve(where + leaf.Value.where, EnvironmentHandle.environmentEffect.leafRotate);

                    leaf.Value.windout = leaf.Value.wind * 80f;

                }

            }

        }

        public override void drawFront(SpriteBatch b, GameLocation location)
        {

            if (ruined)
            {

                return;

            }

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 512))
            {
                return;

            }

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            int offset = 0;

            IconData.tilesheets leafsheet = IconData.tilesheets.magnolialeaf;

            Season useSeason = Game1.season;

            if (season != -1)
            {

                useSeason = (Season)season;

            }

            switch (useSeason)
            {

                case Season.Summer: offset = 192; break;

                case Season.Fall: leafsheet = IconData.tilesheets.magnolialeaftwo; break;

                case Season.Winter: offset = 192; leafsheet = IconData.tilesheets.magnolialeaftwo; break;

            }

            Vector2 span = new Vector2(16);

            foreach (KeyValuePair<Vector2, MagnoliaLeaf> leaf in leaves)
            {

                Vector2 place = new(leaf.Value.windout, 0);

                b.Draw(
                    Mod.instance.iconData.sheetTextures[leafsheet], 
                    origin + leaf.Key + place, 
                    new Rectangle(leaf.Value.source.X + offset,leaf.Value.source.Y, 32, 32), 
                    leaf.Value.colour * fade, 
                    leaf.Value.wind, 
                    span,
                    4f, 
                    leaf.Value.flip ? (SpriteEffects)1 : 0, 
                    900f
                );

            }

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 576))
            {
                
                return;

            }

            Season useSeason = Game1.season;

            if (season != -1)
            {

                useSeason = (Season)season;

            }

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Vector2 place = new Vector2(windout,0);

            Microsoft.Xna.Framework.Color trunkcolour = new(224, 224, 224);

            if (!ruined)
            {

                Vector2 span = new Vector2(16);

                Vector2 lay;

                switch (size)
                {

                    default:
                    case 1:

                        lay = new Vector2(origin.X + 384, origin.Y + 704);

                        break;

                    case 2:

                        lay = new Vector2(origin.X + 192, origin.Y + 352);

                        break;

                }

                if (useSeason == Season.Fall)
                {

                    foreach (KeyValuePair<Vector2, MagnoliaLeaf> leaf in litter)
                    {

                        int leafoffset = 0;

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnolialeaftwo],
                            lay + leaf.Key,
                            new Rectangle(leaf.Value.source.X + leafoffset, leaf.Value.source.Y, 32, 32),
                            Color.White,
                            wind * 2,
                            span,
                            3.6f,
                            leaf.Value.flip ? (SpriteEffects)1 : 0,
                            0.0005f
                        );

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnolialeaftwo],
                            lay + leaf.Key + new Vector2(2, 8),
                            new Rectangle(leaf.Value.source.X + leafoffset, leaf.Value.source.Y, 32, 32),
                            Color.Black * 0.3f,
                            wind * 2,
                            span,
                            3.6f,
                            leaf.Value.flip ? (SpriteEffects)1 : 0,
                            0.0004f
                        );

                    }
                
                }

            }

            int offset = 0;

            Microsoft.Xna.Framework.Color trunkShadow = Color.White * 0.2f;

            Microsoft.Xna.Framework.Color gladeShade = Color.White * 0.1f;

            Microsoft.Xna.Framework.Color leafShade = Color.White * 0.15f;

            trunkcolour = Color.White;

            if (fade < 1f)
            {

                trunkShadow = Color.White * 0.1f;

                gladeShade = Color.White * 0.05f;

                leafShade = Color.White * 0.075f;

            }

            switch (size)
            {
                default:
                case 1:

                    switch (useSeason)
                    {

                        case Season.Summer: offset = 192; break;

                        case Season.Fall: offset = 384; break;

                        case Season.Winter: offset = 576; break;

                    }

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[tilesheet],
                        origin + new Vector2(384) + place,
                        new Rectangle(offset, 0, 192, 192),
                        trunkcolour * fade,
                        wind,
                        new Vector2(96),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer - 0.001f
                        );


                    if (Game1.timeOfDay > 2100)
                    {

                        break;

                    }

                    if (!ruined)
                    {

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(384, 716) + place,
                            new Rectangle(192, 0, 192, 160),
                            gladeShade,
                            wind,
                            new Vector2(96, 80),
                            4,
                            flip ? (SpriteEffects)1 : 0,
                            layer + 0.0384f
                            );

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(384, 716) + place,
                            new Rectangle(0, 0, 192, 160),
                            leafShade,
                            0,
                            new Vector2(96, 80),
                            4f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );


                    }
                    else
                    {


                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(384, 716) + place,
                            new Rectangle(384, 0, 192, 160),
                            trunkShadow,
                            wind,
                            new Vector2(96, 80),
                            4f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );

                    }

                    break;

                case 2:

                    switch (useSeason)
                    {

                        case Season.Summer: offset = 96; break;

                        case Season.Fall: offset = 192; break;

                        case Season.Winter: offset = 288; break;

                    }

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnolia],
                        origin + new Vector2(192, 192) + place,
                        new Rectangle(offset, 192, 96, 96),
                        trunkcolour * fade,
                        wind,
                        new Vector2(48, 48),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer - 0.001f
                        );


                    if (Game1.timeOfDay > 2100)
                    {

                        break;

                    }

                    if (!ruined)
                    {

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(192, 368) + place,
                            new Rectangle(192, 0, 192, 160),
                            gladeShade,
                            wind,
                            new Vector2(96, 80),
                            2.4f,
                            flip ? (SpriteEffects)1 : 0,
                            layer + 0.0320f
                            );

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(192, 368) + place,
                            new Rectangle(0, 0, 192, 160),
                            leafShade,
                            0,
                            new Vector2(96, 80),
                            2.4f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );

                    }
                    else
                    {

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(192, 360) + place,
                            new Rectangle(384, 0, 192, 160),
                            trunkShadow,
                            wind,
                            new Vector2(96, 80),
                            2.4f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );

                    }

                    break;

            }

        }

    }

    public class MagnoliaLeaf
    {

        public enum magnoliatiers
        {
            top,
            mid,
            bottom,
            topright,
            topleft,
            bottomright,
            bottomleft,
            litter
        }

        public magnoliatiers tier;

        public Microsoft.Xna.Framework.Rectangle source;

        public Microsoft.Xna.Framework.Color colour;

        public int index;

        public int off;

        public bool flip;

        public float shade;

        public float rotate;

        public int where;

        public float wind;

        public float windout;

        public MagnoliaLeaf(magnoliatiers Tier, int Off, int Where)
        {

            where = Where;

            tier = Tier;

            switch (tier)
            {
                case magnoliatiers.top:
                case magnoliatiers.mid:

                    switch (Mod.instance.randomIndex.Next(3))
                    {
                        default:
                            index = Mod.instance.randomIndex.Next(18, 30);
                            break;
                        case 2:
                            index = Mod.instance.randomIndex.Next(12);
                            break;

                    }

                    flip = Mod.instance.randomIndex.NextBool();

                    break;

                /*case magnoliatiers.mid:

                    switch (Mod.instance.randomIndex.Next(6))
                    {
                        default:
                            index = Mod.instance.randomIndex.Next(12);
                            break;
                        case 4:
                        case 5:
                        case 6:
                            index = Mod.instance.randomIndex.Next(18, 30);
                            break;

                    }

                    flip = Mod.instance.randomIndex.NextBool();

                    break; */

                case magnoliatiers.bottom:

                    index = Mod.instance.randomIndex.Next(18, 30);

                    flip = Mod.instance.randomIndex.NextBool();

                    break;

                case magnoliatiers.topright:

                    switch (Mod.instance.randomIndex.Next(3))
                    {
                        default:
                            index = Mod.instance.randomIndex.Next(30, 36);
                            break;
                        case 2:
                            index = Mod.instance.randomIndex.Next(12, 18);
                            break;

                    }
                    index = Mod.instance.randomIndex.Next(12, 18);

                    break;

                case magnoliatiers.topleft:

                    switch (Mod.instance.randomIndex.Next(3))
                    {
                        default:
                            index = Mod.instance.randomIndex.Next(30, 36);
                            break;
                        case 2:
                            index = Mod.instance.randomIndex.Next(12, 18);
                            break;

                    }
                    

                    flip = true;

                    break;

                case magnoliatiers.bottomright:

                    index = Mod.instance.randomIndex.Next(30, 36);

                    break;

                case magnoliatiers.bottomleft:

                    index = Mod.instance.randomIndex.Next(30, 36);

                    flip = true;

                    break;

                case magnoliatiers.litter:

                    index = Mod.instance.randomIndex.Next(36, 42);

                    flip = Mod.instance.randomIndex.NextBool();

                    rotate = Mod.instance.randomIndex.Next(4) * 0.5f * (float)Math.PI;

                    break;


            }

            off = Off;

            set();

        }

        public void set()
        {

            source = new(0 + (32 * (index %6)), 0 + (32 * (int)(index / 6)), 32, 32);

            colour = new(256 - off, 256 - off, 256);

        }

    }

}
