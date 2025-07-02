using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

    public class Holly : TerrainField
    {

        public Dictionary<Vector2, CommonLeaf> leaves = new();

        public Dictionary<Vector2, CommonLeaf> litter = new();

        public int size;

        public bool littered;

        public int season;

        public Holly(Vector2 Position, int Size = 1)
           : base()
        {

            tilesheet = IconData.tilesheets.holly;

            index = 1;

            position = Position;

            flip = Mod.instance.randomIndex.Next(2) != 0;

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

                    for (int x = 2; x < 4; x++)
                    {

                        baseTiles.Add(new Vector2(tile.X + x, tile.Y + (11)));

                    }

                    layer = (position.Y / 10000) + (0.0064f * 11.5f);

                    bounds = new((int)position.X + 64, (int)position.Y, 4 * 64, 9 * 64);

                    Leaves();

                    break;

                case 2:

                    baseTiles.Add(new Vector2(tile.X + 2, tile.Y + 6));

                    layer = (position.Y / 10000) + (0.0064f * 6.5f);

                    bounds = new((int)position.X + 64, (int)position.Y, 3 * 64, 5 * 64);

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
                [new Vector2(168,0)] = new(CommonLeaf.commonleaves.top, 0, 8, CommonLeaf.commonrenders.holly),
                [new Vector2(216,0)] = new(CommonLeaf.commonleaves.top, 1, 8, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(128, 68)] = new(CommonLeaf.commonleaves.top, 0, 0, CommonLeaf.commonrenders.holly),
                [new Vector2(256, 68)] = new(CommonLeaf.commonleaves.top, 1, 0, CommonLeaf.commonrenders.holly),

                [new Vector2(192, 64)] = new(CommonLeaf.commonleaves.top, 2, 0,CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(96, 132)] = new(CommonLeaf.commonleaves.top, 0, 0, CommonLeaf.commonrenders.holly),
                [new Vector2(288, 132)] = new(CommonLeaf.commonleaves.top, 1, 0, CommonLeaf.commonrenders.holly),

                [new Vector2(160, 128)] = new(CommonLeaf.commonleaves.top, 2, 0, CommonLeaf.commonrenders.holly),
                [new Vector2(224, 128)] = new(CommonLeaf.commonleaves.top, 3, 0, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(64, 204)] = new(CommonLeaf.commonleaves.topleft, 0, 4, CommonLeaf.commonrenders.holly),
                [new Vector2(320, 204)] = new(CommonLeaf.commonleaves.topright, 1, 4, CommonLeaf.commonrenders.holly),

                [new Vector2(128, 196)] = new(CommonLeaf.commonleaves.top, 2, 0, CommonLeaf.commonrenders.holly),
                [new Vector2(256, 196)] = new(CommonLeaf.commonleaves.top, 3, 0, CommonLeaf.commonrenders.holly),

                [new Vector2(192, 192)] = new(CommonLeaf.commonleaves.top, 4, 0, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(32, 264)] = new(CommonLeaf.commonleaves.topleft, 0, 4, CommonLeaf.commonrenders.holly),
                [new Vector2(352, 264)] = new(CommonLeaf.commonleaves.topright, 1, 4, CommonLeaf.commonrenders.holly),
                
                [new Vector2(96, 260)] = new(CommonLeaf.commonleaves.mid, 2, 0, CommonLeaf.commonrenders.holly),
                [new Vector2(288, 260)] = new(CommonLeaf.commonleaves.mid, 3, 0,CommonLeaf.commonrenders.holly),
                
                [new Vector2(160, 256)] = new(CommonLeaf.commonleaves.mid, 4, 0, CommonLeaf.commonrenders.holly),
                [new Vector2(224, 256)] = new(CommonLeaf.commonleaves.mid, 5, 0, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(0, 300)] = new(CommonLeaf.commonleaves.topleft, 0, 16, CommonLeaf.commonrenders.holly),
                [new Vector2(384, 300)] = new(CommonLeaf.commonleaves.topright, 1, 16, CommonLeaf.commonrenders.holly),
                
                [new Vector2(64, 308)] = new(CommonLeaf.commonleaves.mid, 2, 8, CommonLeaf.commonrenders.holly),
                [new Vector2(320, 308)] = new(CommonLeaf.commonleaves.mid, 3, 8, CommonLeaf.commonrenders.holly),
                
                [new Vector2(128, 316)] = new(CommonLeaf.commonleaves.mid, 4, 0,CommonLeaf.commonrenders.holly),
                [new Vector2(256, 316)] = new(CommonLeaf.commonleaves.mid, 5, 0, CommonLeaf.commonrenders.holly),
                
                [new Vector2(192, 320)] = new(CommonLeaf.commonleaves.mid, 6, 0, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(32, 360)] = new(CommonLeaf.commonleaves.bottomleft, 0, 16, CommonLeaf.commonrenders.holly),
                [new Vector2(352, 360)] = new(CommonLeaf.commonleaves.bottomright, 1, 16, CommonLeaf.commonrenders.holly),
                
                [new Vector2(96, 376)] = new(CommonLeaf.commonleaves.mid, 2, 8, CommonLeaf.commonrenders.holly),
                [new Vector2(288, 376)] = new(CommonLeaf.commonleaves.mid, 3, 8,CommonLeaf.commonrenders.holly),
                
                [new Vector2(160, 384)] = new(CommonLeaf.commonleaves.mid, 4, 8,CommonLeaf.commonrenders.holly),
                [new Vector2(224, 384)] = new(CommonLeaf.commonleaves.mid, 5, 8, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(0, 412)] = new(CommonLeaf.commonleaves.bottomleft, 0, 24, CommonLeaf.commonrenders.holly),
                [new Vector2(384, 412)] = new(CommonLeaf.commonleaves.bottomright, 1, 24, CommonLeaf.commonrenders.holly),
               
                [new Vector2(64, 428)] = new(CommonLeaf.commonleaves.mid, 2, 24, CommonLeaf.commonrenders.holly),
                [new Vector2(320, 428)] = new(CommonLeaf.commonleaves.mid, 3, 24, CommonLeaf.commonrenders.holly),
                
                [new Vector2(128, 440)] = new(CommonLeaf.commonleaves.mid, 4, 16, CommonLeaf.commonrenders.holly),
                [new Vector2(256, 440)] = new(CommonLeaf.commonleaves.mid, 5, 16, CommonLeaf.commonrenders.holly),
                
                [new Vector2(192, 448)] = new(CommonLeaf.commonleaves.mid, 6, 16, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(32, 492)] = new(CommonLeaf.commonleaves.bottomleft, 0, 32, CommonLeaf.commonrenders.holly),
                [new Vector2(352, 492)] = new(CommonLeaf.commonleaves.bottomright, 1, 32, CommonLeaf.commonrenders.holly),
                
                [new Vector2(96, 504)] = new(CommonLeaf.commonleaves.bottom, 2, 24, CommonLeaf.commonrenders.holly),
                [new Vector2(288, 504)] = new(CommonLeaf.commonleaves.bottom, 3, 24, CommonLeaf.commonrenders.holly),
                
                [new Vector2(160, 512)] = new(CommonLeaf.commonleaves.bottom, 4, 24,CommonLeaf.commonrenders.holly),
                [new Vector2(224, 512)] = new(CommonLeaf.commonleaves.bottom, 5, 24, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(64, 556)] = new(CommonLeaf.commonleaves.bottomleft, 0, 32, CommonLeaf.commonrenders.holly),
                [new Vector2(320, 556)] = new(CommonLeaf.commonleaves.bottomright, 1, 32, CommonLeaf.commonrenders.holly),
               
                [new Vector2(128, 568)] = new(CommonLeaf.commonleaves.bottom, 2, 24, CommonLeaf.commonrenders.holly),
                [new Vector2(256, 568)] = new(CommonLeaf.commonleaves.bottom, 3, 24, CommonLeaf.commonrenders.holly),
                
                [new Vector2(192, 576)] = new(CommonLeaf.commonleaves.bottom, 4, 24, CommonLeaf.commonrenders.holly),

            };

            foreach (KeyValuePair<Vector2, CommonLeaf> leave in leaves)
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
                //[new Vector2(192, -32)] = new(CommonLeaf.commonleaves.top, 0, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(136, 32)] = new(CommonLeaf.commonleaves.top, 0, 0, CommonLeaf.commonrenders.holly),
                [new Vector2(184, 32)] = new(CommonLeaf.commonleaves.top, 1, 0, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(88, 96)] = new(CommonLeaf.commonleaves.top, 0, 8, CommonLeaf.commonrenders.holly),
                [new Vector2(232, 96)] = new(CommonLeaf.commonleaves.top, 2, 8, CommonLeaf.commonrenders.holly),

                [new Vector2(160, 92)] = new(CommonLeaf.commonleaves.top, 3, 0, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(64, 164)] = new(CommonLeaf.commonleaves.topleft, 0, 16, CommonLeaf.commonrenders.holly),
                [new Vector2(256, 164)] = new(CommonLeaf.commonleaves.topright, 1, 16, CommonLeaf.commonrenders.holly),

                [new Vector2(128, 160)] = new(CommonLeaf.commonleaves.top, 2, 8, CommonLeaf.commonrenders.holly),
                [new Vector2(192, 160)] = new(CommonLeaf.commonleaves.top, 3, 8, CommonLeaf.commonrenders.holly),

                // ----------------------
                [new Vector2(32, 236)] = new(CommonLeaf.commonleaves.topleft, 0, 24, CommonLeaf.commonrenders.holly),
                [new Vector2(288, 236)] = new(CommonLeaf.commonleaves.topright, 1, 24, CommonLeaf.commonrenders.holly),

                [new Vector2(96, 228)] = new(CommonLeaf.commonleaves.top, 2, 16, CommonLeaf.commonrenders.holly),
                [new Vector2(224, 228)] = new(CommonLeaf.commonleaves.top, 3, 16, CommonLeaf.commonrenders.holly),

                [new Vector2(160, 224)] = new(CommonLeaf.commonleaves.top, 4, 16, CommonLeaf.commonrenders.holly),

                // ----------------------

            };

            foreach (KeyValuePair<Vector2, CommonLeaf> leave in leaves)
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

                    double radians = Math.PI / 4;

                    for (int x = 1; x < 5; x++)
                    {

                        double angle = x * radians;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(8, 12) * 16;

                        litter.Add(position, new(CommonLeaf.commonleaves.litter, 0));


                    }

                    for (int x = 0; x < 4; x++)
                    {

                        double angle = x * -1 * radians;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(8, 12) * 16;

                        litter.Add(position, new(CommonLeaf.commonleaves.litter, 0));

                    }

                    break;

                case 2:
                    /*
                    double radian2 = Math.PI / 4;

                    for (int x = 1; x < 5; x++)
                    {

                        double angle = x * radian2;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(4, 12) * 16;

                        litter.Add(position, new(CommonLeaf.commonleaves.litter, 0));


                    }

                    for (int x = 0; x < 4; x++)
                    {

                        double angle = x * -1 * radian2;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(4, 12) * 16;

                        litter.Add(position, new(CommonLeaf.commonleaves.litter, 0));

                    }
                    */
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

                foreach (KeyValuePair<Vector2, CommonLeaf> leaf in leaves)
                {

                    leaf.Value.wind = 0f;

                    leaf.Value.windout = 0f;

                    leaf.Value.wind = Mod.instance.environment.retrieve(where + leaf.Value.where, EnvironmentHandle.environmentEffect.leafShake);

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

                foreach (KeyValuePair<Vector2, CommonLeaf> leaf in leaves)
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

            Season useSeason = Game1.season;

            if (season != -1)
            {

                useSeason = (Season)season;

            }

            switch (useSeason)
            {

                case Season.Summer: offset = 160; break;

                case Season.Fall: offset = 320; break;

                case Season.Winter: offset = 480; break;

            }

            Vector2 span = new Vector2(16);

            foreach (KeyValuePair<Vector2, CommonLeaf> leaf in leaves)
            {

                Vector2 place = new(leaf.Value.windout, 0);

                b.Draw(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.hollyleaf],
                    origin + leaf.Key + place,
                    new Rectangle(leaf.Value.source.X + offset, leaf.Value.source.Y, 32, 32),
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

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 512))
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

            Microsoft.Xna.Framework.Color trunkcolour = new(224, 192, 192);

            if (!ruined)
            {

                Vector2 span = new Vector2(16);

                Vector2 lay;

                switch (size)
                {
                    default:
                    case 1:

                        lay = new Vector2(origin.X + 192, origin.Y + 704);

                        break;

                    case 2:

                        lay = new Vector2(origin.X + 160, origin.Y + 416);

                        break;

                }

                if (useSeason == Season.Fall)
                {

                    foreach (KeyValuePair<Vector2, CommonLeaf> leaf in litter)
                    {

                        int leafoffset = 0;

                        switch (useSeason)
                        {

                            case Season.Summer: leafoffset = 160; break;

                            case Season.Fall: leafoffset = 320; break;

                            case Season.Winter: leafoffset = 480; break;

                        }

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hollyleaf],
                            lay + leaf.Key,
                            new Rectangle(leaf.Value.source.X + leafoffset, leaf.Value.source.Y, 32, 32),
                            Color.White,
                            wind * 2,
                            span,
                            4,
                            leaf.Value.flip ? (SpriteEffects)1 : 0,
                            0.0005f
                        );

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hollyleaf],
                            lay + leaf.Key + new Vector2(2, 8),
                            new Rectangle(leaf.Value.source.X + leafoffset, leaf.Value.source.Y, 32, 32),
                            Color.Black * 0.3f,
                            wind * 2,
                            span,
                            4,
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

                        case Season.Summer: offset = 96; break;

                        case Season.Fall: offset = 192; break;

                        case Season.Winter: offset = 288; break;

                    }

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.holly],
                        origin + new Vector2(192, 384) + place,
                        new Rectangle(offset, 0, 96, 192),
                        trunkcolour * fade,
                        wind,
                        new Vector2(48, 96),
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
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornshade],
                            origin + new Vector2(192, 736) + place,
                            new Rectangle(192, 0, 192, 160),
                            gladeShade,
                            wind,
                            new Vector2(96, 80),
                            2.4f,
                            flip ? (SpriteEffects)1 : 0,
                            layer + 0.0448f
                            );

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(192, 736) + place,
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
                            origin + new Vector2(192, 736) + place,
                            new Rectangle(384, 0, 192, 160),
                            trunkShadow,
                            wind,
                            new Vector2(96, 80),
                            2.8f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );

                    }

                    break;

                case 2:

                    switch (useSeason)
                    {

                        case Season.Summer: offset = 80; break;

                        case Season.Fall: offset = 160; break;

                        case Season.Winter: offset = 240; break;

                    }

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.holly],
                        origin + new Vector2(160, 224) + place,
                        new Rectangle(offset, 192, 80, 112),
                        trunkcolour * fade,
                        wind,
                        new Vector2(40, 56),
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
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornshade],
                            origin + new Vector2(160, 424) + place,
                            new Rectangle(192, 0, 192, 160),
                            gladeShade,
                            wind,
                            new Vector2(96, 80),
                            1.8f,
                            flip ? (SpriteEffects)1 : 0,
                            layer + 0.0384f
                            );

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(160, 424) + place,
                            new Rectangle(0, 0, 192, 160),
                            leafShade,
                            0,
                            new Vector2(96, 80),
                            1.8f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );

                    }
                    else
                    {

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(160, 428) + place,
                            new Rectangle(384, 0, 192, 160),
                            trunkShadow,
                            wind,
                            new Vector2(96, 80),
                            2f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );

                    }

                    break;


            }

        }

    }

}
