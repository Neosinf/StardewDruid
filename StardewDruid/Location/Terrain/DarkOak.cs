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

    public class DarkOak : TerrainField
    {

        public Dictionary<Vector2, CommonLeaf> leaves = new();

        public Dictionary<Vector2, CommonLeaf> litter = new();

        public float fade;

        public int size;

        public bool littered;

        public int season;

        public int where;

        public DarkOak(Vector2 Position, int Size = 1)
           : base()
        {

            tilesheet = IconData.tilesheets.darkoak;

            index = 1;

            position = Position;

            fade = 1f;

            flip = Mod.instance.randomIndex.Next(2) != 0;

            size = Size;

            season = 2;

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

                        for (int x = 4; x < 6; x++)
                        {

                            baseTiles.Add(new Vector2(tile.X + x, tile.Y + (10 - y)));

                        }

                    }

                    layer = (position.Y / 10000) + (0.0064f * 9.5f);

                    bounds = new((int)position.X + 64, (int)position.Y, 10 * 64, 8 * 64);

                    Leaves();

                    break;

                case 2:

                    baseTiles.Add(new Vector2(tile.X + 2, tile.Y + 5));

                    layer = (position.Y / 10000) + (0.0064f * 5.5f);

                    bounds = new((int)position.X + 64, (int)position.Y, 3 * 64, 4 * 64);

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
                [new Vector2(128, 16)] = new(CommonLeaf.commonleaves.topleft, 0, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(512, 16)] = new(CommonLeaf.commonleaves.topright, 1, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(200, -4)] = new(CommonLeaf.commonleaves.top, 2, 8, CommonLeaf.commonrenders.darkoak),
                [new Vector2(440, -4)] = new(CommonLeaf.commonleaves.top, 3, 8, CommonLeaf.commonrenders.darkoak),
                [new Vector2(280, -12)] = new(CommonLeaf.commonleaves.top, 4, 8,CommonLeaf.commonrenders.darkoak),
                [new Vector2(360, -12)] = new(CommonLeaf.commonleaves.top, 5, 8, CommonLeaf.commonrenders.darkoak),

                // ----------------------
                [new Vector2(88, 68)] = new(CommonLeaf.commonleaves.topleft, 0, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(552, 68)] = new(CommonLeaf.commonleaves.topright, 1, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(160, 60)] = new(CommonLeaf.commonleaves.topleft, 2, 8, CommonLeaf.commonrenders.darkoak),
                [new Vector2(480, 60)] = new(CommonLeaf.commonleaves.topright, 3, 8,CommonLeaf.commonrenders.darkoak),
                [new Vector2(232, 52)] = new(CommonLeaf.commonleaves.top, 4, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(408, 52)] = new(CommonLeaf.commonleaves.top, 5, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(320, 40)] = new(CommonLeaf.commonleaves.top, 6, 0, CommonLeaf.commonrenders.darkoak),

                // ----------------------
                [new Vector2(40, 120)] = new(CommonLeaf.commonleaves.topleft, 0, 12, CommonLeaf.commonrenders.darkoak),
                [new Vector2(600, 120)] = new(CommonLeaf.commonleaves.topright, 1, 12, CommonLeaf.commonrenders.darkoak),
                [new Vector2(120, 112)] = new(CommonLeaf.commonleaves.topleft, 2, 4, CommonLeaf.commonrenders.darkoak),
                [new Vector2(520, 112)] = new(CommonLeaf.commonleaves.topright, 3, 4,CommonLeaf.commonrenders.darkoak),
                [new Vector2(200, 104)] = new(CommonLeaf.commonleaves.top, 4, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(440, 104)] = new(CommonLeaf.commonleaves.top, 5, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(280, 92)] = new(CommonLeaf.commonleaves.top, 6, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(360, 92)] = new(CommonLeaf.commonleaves.top, 7, 0, CommonLeaf.commonrenders.darkoak),

                // ----------------------
                [new Vector2(80, 160)] = new(CommonLeaf.commonleaves.topleft, 0, 12,CommonLeaf.commonrenders.darkoak),
                [new Vector2(560, 160)] = new(CommonLeaf.commonleaves.topright, 1, 12, CommonLeaf.commonrenders.darkoak),
                [new Vector2(160, 156)] = new(CommonLeaf.commonleaves.mid, 2, 4, CommonLeaf.commonrenders.darkoak),
                [new Vector2(480, 156)] = new(CommonLeaf.commonleaves.mid, 3, 4,CommonLeaf.commonrenders.darkoak),
                [new Vector2(240, 152)] = new(CommonLeaf.commonleaves.top, 4, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(400, 152)] = new(CommonLeaf.commonleaves.top, 5, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(320, 148)] = new(CommonLeaf.commonleaves.top, 6, 0, CommonLeaf.commonrenders.darkoak),

                // ----------------------
                [new Vector2(0, 196)] = new(CommonLeaf.commonleaves.topleft, 0, 12, CommonLeaf.commonrenders.darkoak),
                [new Vector2(640, 196)] = new(CommonLeaf.commonleaves.topright, 1, 12, CommonLeaf.commonrenders.darkoak),
                [new Vector2(88, 208)] = new(CommonLeaf.commonleaves.topleft, 2, 4, CommonLeaf.commonrenders.darkoak),
                [new Vector2(552, 208)] = new(CommonLeaf.commonleaves.topright, 3, 4,CommonLeaf.commonrenders.darkoak),
                [new Vector2(184, 212)] = new(CommonLeaf.commonleaves.mid, 4, 0,CommonLeaf.commonrenders.darkoak),
                [new Vector2(456, 212)] = new(CommonLeaf.commonleaves.mid, 5, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(280, 216)] = new(CommonLeaf.commonleaves.mid, 6, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(360, 216)] = new(CommonLeaf.commonleaves.mid, 7, 0, CommonLeaf.commonrenders.darkoak),

                // ----------------------
                [new Vector2(40, 272)] = new(CommonLeaf.commonleaves.topleft, 0, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(600, 272)] = new(CommonLeaf.commonleaves.topright, 1, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(136, 276)] = new(CommonLeaf.commonleaves.mid, 2, 8, CommonLeaf.commonrenders.darkoak),
                [new Vector2(504, 276)] = new(CommonLeaf.commonleaves.mid, 3, 8,CommonLeaf.commonrenders.darkoak),
                [new Vector2(232, 282)] = new(CommonLeaf.commonleaves.mid, 4, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(408, 282)] = new(CommonLeaf.commonleaves.mid, 5, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(320, 282)] = new(CommonLeaf.commonleaves.mid, 6, 0, CommonLeaf.commonrenders.darkoak),

                // ----------------------
                [new Vector2(-16, 308)] = new(CommonLeaf.commonleaves.bottomleft, 0, 24, CommonLeaf.commonrenders.darkoak),
                [new Vector2(656, 308)] = new(CommonLeaf.commonleaves.bottomright, 1, 24, CommonLeaf.commonrenders.darkoak),
                [new Vector2(72, 324)] = new(CommonLeaf.commonleaves.bottomleft, 2, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(568, 324)] = new(CommonLeaf.commonleaves.bottomright, 3, 16,CommonLeaf.commonrenders.darkoak),
                [new Vector2(168, 332)] = new(CommonLeaf.commonleaves.bottom, 4, 8,CommonLeaf.commonrenders.darkoak),
                [new Vector2(472, 332)] = new(CommonLeaf.commonleaves.bottom, 5, 8, CommonLeaf.commonrenders.darkoak),
                [new Vector2(272, 340)] = new(CommonLeaf.commonleaves.mid, 6, 8, CommonLeaf.commonrenders.darkoak),
                [new Vector2(368, 340)] = new(CommonLeaf.commonleaves.mid, 7, 8, CommonLeaf.commonrenders.darkoak),

                // ----------------------
                [new Vector2(24, 388)] = new(CommonLeaf.commonleaves.bottomleft, 0, 24, CommonLeaf.commonrenders.darkoak),
                [new Vector2(616, 388)] = new(CommonLeaf.commonleaves.bottomright, 1, 24, CommonLeaf.commonrenders.darkoak),
                [new Vector2(120, 396)] = new(CommonLeaf.commonleaves.bottom, 2, 24, CommonLeaf.commonrenders.darkoak),
                [new Vector2(520, 396)] = new(CommonLeaf.commonleaves.bottom, 3, 24,CommonLeaf.commonrenders.darkoak),
                [new Vector2(224, 404)] = new(CommonLeaf.commonleaves.bottom, 4, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(416, 404)] = new(CommonLeaf.commonleaves.bottom, 5, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(320, 404)] = new(CommonLeaf.commonleaves.bottom, 6, 16, CommonLeaf.commonrenders.darkoak),

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

                [new Vector2(64, 4)] = new(CommonLeaf.commonleaves.topleft, 0, 8, CommonLeaf.commonrenders.darkoak),
                [new Vector2(256, 4)] = new(CommonLeaf.commonleaves.topright, 1, 8, CommonLeaf.commonrenders.darkoak),

                [new Vector2(128, -12)] = new(CommonLeaf.commonleaves.top, 2, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(192, -12)] = new(CommonLeaf.commonleaves.top, 3, 0, CommonLeaf.commonrenders.darkoak),

                // ----------------------

                [new Vector2(24, 60)] = new(CommonLeaf.commonleaves.topleft, 0, 8, CommonLeaf.commonrenders.darkoak),
                [new Vector2(292, 60)] = new(CommonLeaf.commonleaves.topright, 1, 8, CommonLeaf.commonrenders.darkoak),

                [new Vector2(96, 52)] = new(CommonLeaf.commonleaves.mid, 2, 0, CommonLeaf.commonrenders.darkoak),
                [new Vector2(224, 52)] = new(CommonLeaf.commonleaves.mid, 3, 0, CommonLeaf.commonrenders.darkoak),

                [new Vector2(160, 40)] = new(CommonLeaf.commonleaves.mid, 4, 0, CommonLeaf.commonrenders.darkoak),

                // ----------------------

                [new Vector2(0, 112)] = new(CommonLeaf.commonleaves.bottomleft, 0, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(320, 112)] = new(CommonLeaf.commonleaves.bottomright, 1, 16, CommonLeaf.commonrenders.darkoak),

                [new Vector2(64, 104)] = new(CommonLeaf.commonleaves.mid, 2, 8, CommonLeaf.commonrenders.darkoak),
                [new Vector2(256, 104)] = new(CommonLeaf.commonleaves.mid, 3, 8, CommonLeaf.commonrenders.darkoak),

                [new Vector2(128, 92)] = new(CommonLeaf.commonleaves.mid, 4, 8, CommonLeaf.commonrenders.darkoak),
                [new Vector2(192, 92)] = new(CommonLeaf.commonleaves.mid, 5, 8, CommonLeaf.commonrenders.darkoak),

                // ----------------------

                [new Vector2(32, 156)] = new(CommonLeaf.commonleaves.bottomleft, 0, 24, CommonLeaf.commonrenders.darkoak),
                [new Vector2(288, 156)] = new(CommonLeaf.commonleaves.bottomright, 1, 24, CommonLeaf.commonrenders.darkoak),

                [new Vector2(96, 152)] = new(CommonLeaf.commonleaves.bottom, 2, 16, CommonLeaf.commonrenders.darkoak),
                [new Vector2(224, 152)] = new(CommonLeaf.commonleaves.bottom, 3, 16, CommonLeaf.commonrenders.darkoak),

                [new Vector2(160, 148)] = new(CommonLeaf.commonleaves.bottom, 4, 16, CommonLeaf.commonrenders.darkoak),


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

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(10, 14) * 16;

                        litter.Add(position, new(CommonLeaf.commonleaves.litter, 0));


                    }

                    for (int x = 0; x < 4; x++)
                    {

                        double angle = x * -1 * radians;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(10, 14) * 16;

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

            foreach (NPC character in location.characters)
            {

                if (bounds.Contains(character.Position.X, character.Position.Y))
                {

                    fade = 0.35f;

                    return;

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

            Fade(location);

            Vector2 span = new Vector2(16);

            foreach (KeyValuePair<Vector2, CommonLeaf> leaf in leaves)
            {

                float rotate = Mod.instance.environment.retrieve(where + leaf.Value.where, EnvironmentHandle.environmentEffect.leafRotate);

                Vector2 place = new(Mod.instance.environment.retrieve(where + leaf.Value.where, EnvironmentHandle.environmentEffect.leafOffset), 0);

                b.Draw(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.darkoakleaf],
                    origin + leaf.Key + place,
                    new Rectangle(leaf.Value.source.X + offset, leaf.Value.source.Y, 32, 32),
                    leaf.Value.colour * fade,
                    rotate,
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

            Fade(location);

            float rotate = 0f;

            Vector2 place = new Vector2(0);

            Microsoft.Xna.Framework.Color trunkcolour = new(224, 192, 224);

            if (!ruined)
            {

                rotate = Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.trunkRotate);

                Vector2 span = new Vector2(16);

                Vector2 lay;

                switch (size)
                {
                    default:
                    case 1:
                        lay = new Vector2(origin.X + 320, origin.Y + 576);

                        place = new(Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.trunkOffset), 0);

                        break;

                    case 2:
                        lay = new Vector2(origin.X + 160, origin.Y + 352);

                        place = new(Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.smallTrunkOffset), 0);

                        break;

                }

                if(useSeason == Season.Fall)
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
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.darkoakleaf],
                            lay + leaf.Key,
                            new Rectangle(leaf.Value.source.X + leafoffset, leaf.Value.source.Y, 32, 32),
                            Color.White,
                            rotate * 2,
                            span,
                            4,
                            leaf.Value.flip ? (SpriteEffects)1 : 0,
                            0.0005f
                        );

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.darkoakleaf],
                            lay + leaf.Key + new Vector2(2, 8),
                            new Rectangle(leaf.Value.source.X + leafoffset, leaf.Value.source.Y, 32, 32),
                            Color.Black * 0.3f,
                            rotate * 2,
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

                        case Season.Summer: offset = 160; break;

                        case Season.Fall: offset = 320; break;

                        case Season.Winter: offset = 480; break;

                    }

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.darkoak],
                        origin + new Vector2(320) + place,
                        new Rectangle(offset, 0, 160, 160),
                        trunkcolour * fade,
                        rotate,
                        new Vector2(80),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer - 0.001f
                        );

                    if (!ruined)
                    {

                        if (Game1.timeOfDay < 2100)
                        {

                            b.Draw(
                                Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornshade],
                                origin + new Vector2(320, 576) + place,
                                new Rectangle(192, 0, 192, 160),
                                gladeShade,
                                rotate,
                                new Vector2(96, 80),
                                4f,
                                flip ? (SpriteEffects)1 : 0,
                                layer + 0.0448f
                                );

                            b.Draw(
                                Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                                origin + new Vector2(320, 576) + place,
                                new Rectangle(0, 0, 192, 160),
                                leafShade,
                                0,
                                new Vector2(96, 80),
                                3.6f,
                                flip ? (SpriteEffects)1 : 0,
                                0.001f
                                );

                        }

                    }
                    else
                    {

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(320, 576) + place,
                            new Rectangle(384, 0, 192, 160),
                            trunkShadow,
                            rotate,
                            new Vector2(96, 80),
                            3.2f,
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
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.darkoak],
                        origin + new Vector2(160,192) + place,
                        new Rectangle(offset, 160, 80, 96),
                        trunkcolour * fade,
                        rotate,
                        new Vector2(40,48),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer - 0.001f
                        );

                    if (!ruined)
                    {

                        if (Game1.timeOfDay < 2100)
                        {

                            b.Draw(
                                Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornshade],
                                origin + new Vector2(160, 368) + place,
                                new Rectangle(192, 0, 192, 160),
                                gladeShade,
                                rotate,
                                new Vector2(96, 80),
                                2f,
                                flip ? (SpriteEffects)1 : 0,
                                layer + 0.0384f
                                );

                            b.Draw(
                                Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                                origin + new Vector2(160, 368) + place,
                                new Rectangle(0, 0, 192, 160),
                                leafShade,
                                0,
                                new Vector2(96, 80),
                                1.8f,
                                flip ? (SpriteEffects)1 : 0,
                                0.001f
                                );

                        }

                    }
                    else
                    {

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.magnoliashade],
                            origin + new Vector2(160, 360) + place,
                            new Rectangle(384, 0, 192, 160),
                            trunkShadow,
                            rotate,
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
