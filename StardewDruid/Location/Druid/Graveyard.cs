using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Layers;
using xTile.Tiles;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley.Buildings;
using System.Drawing;
using StardewValley.Objects;
using System.Runtime.Intrinsics.X86;
using StardewValley.GameData.Locations;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.TerrainFeatures;
using System.Threading;
using System.Xml.Serialization;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Location.Terrain;
using static StardewValley.Menus.InventoryMenu;


namespace StardewDruid.Location.Druid
{

    public class Graveyard : DruidLocation
    {

        public Graveyard() { }

        public Graveyard(string Name)
            : base(Name)
        {

        }

        public override void RestoreTo(int restore)
        {

            if (restoration >= restore)
            {

                return;

            }

            for (int i = 1; i < restore + 1; i++)
            {

                switch (i)
                {

                    case 1:

                        foreach (TerrainField terrain in terrainFields)
                        {

                            if (terrain is GraveCandle debris)
                            {

                                debris.disabled = true;

                                debris.ruined = false;

                            }

                        }

                        break;

                    case 2:

                        foreach (TerrainField terrain in terrainFields)
                        {

                            if (terrain is Holly holly)
                            {

                                holly.ruined = false;

                            }

                            if (terrain is DarkOak darkoak)
                            {

                                darkoak.ruined = false;

                            }
                        }

                        break;
                    
                    case 3:


                        foreach (TerrainField terrain in terrainFields)
                        {

                            if(terrain is Flower flower)
                            {

                                flower.ruined = false;

                            }

                        }

                        break;

                    case 4:


                        foreach (TerrainField terrain in terrainFields)
                        {

                            if (terrain is GraveCandle debris)
                            {

                                debris.disabled = false;

                            }

                        }

                        break;
                }

            }

            restoration = restore;

        }

        public override void OnMapLoad(xTile.Map map)
        {


            xTile.Dimensions.Size tileSize = map.GetLayer("Back").TileSize;

            xTile.Map newMap = new(map.Id);

            Layer back = new("Back", newMap, new(56, 36), tileSize);

            newMap.AddLayer(back);

            Layer buildings = new("Buildings", newMap, new(56, 36), tileSize);

            newMap.AddLayer(buildings);

            Layer front = new("Front", newMap, new(56, 36), tileSize);

            newMap.AddLayer(front);

            Layer alwaysfront = new("AlwaysFront", newMap, new(56, 36), tileSize);

            newMap.AddLayer(alwaysfront);

            TileSheet outdoor = new(
                newMap,
                "StardewDruid.Tilesheets.ground",
                new(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.groundspring].Width / 16,
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.groundspring].Height / 16
                ),
                new(16, 16)
            );

            newMap.AddTileSheet(outdoor);

            newMap.LoadTileSheets(Game1.mapDisplayDevice);

            IsOutdoors = true;

            ignoreOutdoorLighting.Set(false);

            terrainFields = new();

            Dictionary<int, List<List<int>>> codes = new();

            // -----------------------------------------------------
            // normal ground

            Dictionary<int, List<List<int>>> groundCodes = new()
            {
                [0] = new() { },
                [1] = new() { },
                [2] = new() { },
                [3] = new() { new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, },
                [4] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [5] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [6] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [7] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [8] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [9] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [10] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [11] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [12] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 3 }, new() { 15, 4 }, new() { 16, 4 }, new() { 17, 3 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 3 }, new() { 39, 4 }, new() { 40, 4 }, new() { 41, 3 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [13] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 2 }, new() { 15, 3 }, new() { 16, 3 }, new() { 17, 2 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 2 }, new() { 39, 3 }, new() { 40, 3 }, new() { 41, 2 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [14] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [15] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 4 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [16] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 2 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 4 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 4 }, new() { 30, 4 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [17] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 2 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 4 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 4 }, new() { 30, 4 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [18] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 2 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 4 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 4 }, new() { 30, 4 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [19] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 2 }, new() { 23, 3 }, new() { 24, 3 }, new() { 25, 4 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 4 }, new() { 30, 4 }, new() { 31, 3 }, new() { 32, 3 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [20] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 3 }, new() { 25, 3 }, new() { 26, 4 }, new() { 27, 4 }, new() { 28, 4 }, new() { 29, 4 }, new() { 30, 3 }, new() { 31, 3 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [21] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 3 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 3 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [22] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 3 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 3 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [23] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [24] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [25] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [26] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [27] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, },
                [28] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [29] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [30] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [31] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [32] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [33] = new() { new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, },
                [34] = new() { },
                [35] = new() { },


            };

            foreach (KeyValuePair<int, List<List<int>>> groundCode in groundCodes)
            {

                foreach (List<int> array in groundCode.Value)
                {

                    switch (array[1])
                    {
                        case 1:

                            back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, Mod.instance.randomIndex.Next(80));

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Grass");

                            break;

                        case 2:

                            switch (Mod.instance.randomIndex.Next(4))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 160 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 180 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 2:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 212 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 3:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 232 + Mod.instance.randomIndex.Next(8));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;

                        case 3:

                            switch (Mod.instance.randomIndex.Next(4))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 168 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 188 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 2:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 204 + Mod.instance.randomIndex.Next(8));

                                    break;
                                case 3:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 224 + Mod.instance.randomIndex.Next(8));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;

                        case 4:

                            switch (Mod.instance.randomIndex.Next(4))
                            {
                                case 0:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 176 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 1:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 196 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 2:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 200 + Mod.instance.randomIndex.Next(4));

                                    break;
                                case 3:

                                    back.Tiles[array[0], groundCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 220 + Mod.instance.randomIndex.Next(4));

                                    break;

                            }

                            back.Tiles[array[0], groundCode.Key].TileIndexProperties.Add("Type", "Stone");

                            break;
                    }
                }

            }


            // ------------------------------------------------------
            // building

            Dictionary<int, List<List<int>>> borderCodes = new()
            {
                [0] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [1] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [2] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [3] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [4] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [5] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [6] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [7] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [8] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [16] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [17] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [18] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [19] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [32] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [33] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [34] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [35] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 2 }, new() { 27, 3 }, new() { 28, 3 }, new() { 29, 2 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> borderCode in borderCodes)
            {

                foreach (List<int> array in borderCode.Value)
                {
                    switch (array[1])
                    {
                        case 1:
                            buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, Mod.instance.randomIndex.Next(80));

                            break;

                        case 2:

                            switch (Mod.instance.randomIndex.Next(4))
                            {

                                case 0:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 160 + Mod.instance.randomIndex.Next(8));

                                    break;

                                case 1:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 180 + Mod.instance.randomIndex.Next(8));

                                    break;

                                case 2:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 212 + Mod.instance.randomIndex.Next(8));

                                    break;

                                case 3:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 232 + Mod.instance.randomIndex.Next(8));

                                    break;

                            }

                            break;

                        case 3:

                            switch (Mod.instance.randomIndex.Next(4))
                            {

                                case 0:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 168 + Mod.instance.randomIndex.Next(8));

                                    break;

                                case 1:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 188 + Mod.instance.randomIndex.Next(8));

                                    break;

                                case 2:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 204 + Mod.instance.randomIndex.Next(8));

                                    break;

                                case 3:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 224 + Mod.instance.randomIndex.Next(8));

                                    break;

                            }

                            break;

                        case 4:

                            switch (Mod.instance.randomIndex.Next(4))
                            {

                                case 0:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 176 + Mod.instance.randomIndex.Next(4));

                                    break;

                                case 1:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 196 + Mod.instance.randomIndex.Next(4));

                                    break;

                                case 2:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 200 + Mod.instance.randomIndex.Next(4));

                                    break;

                                case 3:

                                    buildings.Tiles[array[0], borderCode.Key] = new StaticTile(back, outdoor, BlendMode.Alpha, 220 + Mod.instance.randomIndex.Next(4));

                                    break;

                            }

                            break;
                    }

                }

            }


            // fences, lamps, trees
            codes = new()
            {
                [-5] = new() { new() { 49, 13 }, },
                [-4] = new() { new() { 1, 12 }, },
                [-3] = new() { },
                [-2] = new() { },
                [-1] = new() { },
                [0] = new() { },
                [1] = new() { new() { 10, 1 }, new() { 11, 5 }, new() { 15, 2 }, new() { 16, 5 }, new() { 20, 2 }, new() { 21, 5 }, new() { 25, 7 }, new() { 31, 5 }, new() { 35, 2 }, new() { 36, 5 }, new() { 40, 2 }, new() { 41, 5 }, new() { 45, 3 }, },
                [2] = new() { new() { 10, 6 }, new() { 23, 11 }, new() { 32, 11 }, new() { 45, 6 }, },
                [3] = new() { new() { 32, 9 }, },
                [4] = new() { new() { 19, 8 }, },
                [5] = new() { },
                [6] = new() { new() { 5, 1 }, new() { 6, 5 }, new() { 10, 3 }, new() { 13, 10 }, new() { 37, 10 }, new() { 45, 1 }, new() { 46, 5 }, new() { 50, 3 }, },
                [7] = new() { new() { 5, 6 }, new() { 50, 6 }, },
                [8] = new() { new() { 0, 8 }, },
                [9] = new() { new() { 51, 9 }, },
                [10] = new() { new() { 22, 11 }, new() { 33, 11 }, },
                [11] = new() { new() { 5, 4 }, new() { 50, 4 }, },
                [12] = new() { new() { 5, 6 }, new() { 37, 9 }, new() { 50, 6 }, },
                [13] = new() { new() { 13, 8 }, },
                [14] = new() { },
                [15] = new() { },
                [16] = new() { new() { 5, 4 }, new() { 50, 4 }, },
                [17] = new() { new() { 0, 8 }, new() { 5, 6 }, new() { 50, 6 }, new() { 51, 9 }, },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { new() { 22, 11 }, new() { 33, 11 }, },
                [21] = new() { new() { 5, 4 }, new() { 50, 4 }, },
                [22] = new() { new() { 5, 6 }, new() { 33, 9 }, new() { 50, 6 }, },
                [23] = new() { new() { 18, 8 }, new() { 48, 13 }, },
                [24] = new() { },
                [25] = new() { new() { 0, 12 }, },
                [26] = new() { new() { 5, 1 }, new() { 6, 5 }, new() { 10, 3 }, new() { 45, 1 }, new() { 46, 5 }, new() { 50, 3 }, },
                [27] = new() { new() { 10, 6 }, new() { 45, 6 }, },
                [28] = new() { new() { 23, 11 }, new() { 32, 11 }, },
                [29] = new() { },
                [30] = new() { },
                [31] = new() { new() { 10, 1 }, new() { 11, 5 }, new() { 15, 2 }, new() { 16, 5 }, new() { 20, 2 }, new() { 21, 5 }, new() { 25, 7 }, new() { 31, 5 }, new() { 35, 2 }, new() { 36, 5 }, new() { 40, 2 }, new() { 41, 5 }, new() { 45, 2 }, },
                [32] = new() { },
                [33] = new() { },
                [34] = new() { },
                [35] = new() { },


            };

            List<Vector2> reserved = new();

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    List<Vector2> baseTiles = new();

                    switch (array[1])
                    {

                        case 8:
                        case 12:

                            DarkOak darkoak = new(new Vector2(array[0], code.Key) * 64, array[1] > 10 ? 1 : 2);

                            darkoak.ruined = true;

                            baseTiles = darkoak.baseTiles;

                            foreach (Vector2 bottom in baseTiles)
                            {

                                reserved.Add(bottom);

                            }

                            terrainFields.Add(darkoak);

                            frontFields.Add(darkoak);

                            break;

                        case 9:
                        case 13:

                            Holly holly = new(new Vector2(array[0], code.Key) * 64, array[1] > 10 ? 1 : 2);

                            holly.ruined = true;

                            baseTiles = holly.baseTiles;

                            foreach (Vector2 bottom in baseTiles)
                            {

                                reserved.Add(bottom);

                            }

                            terrainFields.Add(holly);

                            frontFields.Add(holly);

                            break;

                        case 10:

                            GraveChapel chapel = new(new Vector2(array[0], code.Key) * 64);

                            baseTiles = chapel.baseTiles;

                            terrainFields.Add(chapel);

                            frontFields.Add(chapel);

                            break;

                        case 11:

                            GraveLamp lamp = new(new Vector2(array[0], code.Key) * 64);

                            baseTiles = lamp.baseTiles;

                            terrainFields.Add(lamp);

                            break;

                        default:

                            TerrainField tField = new(IconData.tilesheets.graveyard, array[1], new Vector2(array[0], code.Key) * 64, TerrainField.shadows.offset);

                            terrainFields.Add(tField);

                            break;

                    }

                    foreach (Vector2 bottom in baseTiles)
                    {

                        if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                        {

                            buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, back.Tiles[(int)bottom.X, (int)bottom.Y].TileSheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                        }

                    }

                }

            }

            // graves and tombs
            codes = new()
            {
                [0] = new() { },
                [1] = new() { },
                [2] = new() { },
                [3] = new() { },
                [4] = new() { },
                [5] = new() { },
                [6] = new() { },
                [7] = new() { new() { 15, 13 }, new() { 39, 25 }, },
                [8] = new() { },
                [9] = new() { },
                [10] = new() { new() { 8, 11 }, },
                [11] = new() { },
                [12] = new() { new() { 19, 26 }, new() { 34, 27 }, },
                [13] = new() { },
                [14] = new() { new() { 7, 15 }, new() { 11, 22 }, },
                [15] = new() { },
                [16] = new() { },
                [17] = new() { new() { 7, 16 }, new() { 11, 23 }, },
                [18] = new() { },
                [19] = new() { new() { 12, 20 }, },
                [20] = new() { new() { 9, 18 }, new() { 38, 17 }, },
                [21] = new() { },
                [22] = new() { },
                [23] = new() { },
                [24] = new() { },
                [25] = new() { new() { 13, 9 }, new() { 17, 9 }, new() { 21, 8 }, },
                [26] = new() { new() { 15, 10 }, new() { 19, 10 }, },
                [27] = new() { new() { 12, 8 }, new() { 18, 8 }, new() { 22, 10 }, },
                [28] = new() { new() { 14, 10 }, new() { 16, 9 }, },
                [29] = new() { },
                [30] = new() { },
                [31] = new() { },
                [32] = new() { },
                [33] = new() { },
                [34] = new() { },
                [35] = new() { },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    TerrainField tField= new(IconData.tilesheets.graveyard, array[1], new Vector2(array[0], code.Key) * 64, TerrainField.shadows.offset);

                    foreach (Vector2 bottom in tField.baseTiles)
                    {
                        if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                        {

                            buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, back.Tiles[(int)bottom.X, (int)bottom.Y].TileSheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                        }

                    }

                    terrainFields.Add(tField);

                }

            }

            // graves and tombs (flipped)
            codes = new()
            {
                [0] = new() { },
                [1] = new() { },
                [2] = new() { },
                [3] = new() { },
                [4] = new() { },
                [5] = new() { },
                [6] = new() { },
                [7] = new() { },
                [8] = new() { },
                [9] = new() { },
                [10] = new() { new() { 45, 11 }, },
                [11] = new() { },
                [12] = new() { },
                [13] = new() { },
                [14] = new() { new() { 42, 15 }, new() { 46, 22 }, },
                [15] = new() { },
                [16] = new() { },
                [17] = new() { new() { 42, 16 }, new() { 46, 23 }, },
                [18] = new() { },
                [19] = new() { new() { 41, 20 }, },
                [20] = new() { new() { 16, 18 }, new() { 45, 17 }, },
                [21] = new() { },
                [22] = new() { },
                [23] = new() { },
                [24] = new() { },
                [25] = new() { new() { 34, 10 }, new() { 38, 10 }, new() { 42, 8 }, },
                [26] = new() { new() { 33, 9 }, new() { 37, 8 }, new() { 40, 9 }, },
                [27] = new() { new() { 39, 10 }, new() { 43, 9 }, },
                [28] = new() { new() { 41, 8 }, },
                [29] = new() { },
                [30] = new() { },
                [31] = new() { },
                [32] = new() { },
                [33] = new() { },
                [34] = new() { },
                [35] = new() { },


            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    TerrainField tField = new(IconData.tilesheets.graveyard, array[1], new Vector2(array[0], code.Key) * 64, TerrainField.shadows.offset, true);

                    foreach (Vector2 bottom in tField.baseTiles)
                    {
                        if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                        {

                            buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, back.Tiles[(int)bottom.X, (int)bottom.Y].TileSheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                        }

                    }

                    terrainFields.Add(tField);

                }

            }

            // candles flowers
            codes = new()
            {
                [6] = new() { },
                [7] = new() { },
                [8] = new() { },
                [9] = new() { new() { 39, 35 }, new() { 40, 38 }, },
                [10] = new() { new() { 9, 238 }, new() { 46, 237 }, },
                [11] = new() { new() { 14, 37 }, new() { 38, 36 }, },
                [12] = new() { },
                [13] = new() { new() { 22, 38 }, },
                [14] = new() { },
                [15] = new() { new() { 7, 35 }, new() { 43, 35 }, new() { 47, 38 }, },
                [16] = new() { },
                [17] = new() { new() { 20, 238 }, new() { 35, 237 }, },
                [18] = new() { new() { 9, 37 }, new() { 11, 38 }, new() { 15, 35 }, new() { 19, 36 }, new() { 21, 35 }, new() { 34, 36 }, new() { 42, 36 }, new() { 48, 37 }, },
                [19] = new() { },
                [20] = new() { },
                [21] = new() { },
                [22] = new() { },
                [23] = new() { new() { 9, 35 }, new() { 17, 38 }, new() { 33, 37 }, new() { 38, 38 }, new() { 43, 36 }, new() { 46, 35 }, },
                [24] = new() { },
                [25] = new() { },
                [26] = new() { new() { 13, 36 }, new() { 17, 35 }, new() { 38, 36 }, },
                [27] = new() { },
                [28] = new() { new() { 43, 35 }, },
                [29] = new() { new() { 16, 36 }, },
                [30] = new() { },
                [31] = new() { },
                [32] = new() { },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    GraveCandle tField;

                    int tileKey = array[1];

                    tField = new GraveCandle(IconData.tilesheets.graveyard, tileKey, new Vector2(array[0], code.Key) * 64, TerrainField.shadows.offset, array[0] > 27);

                    tField.ruined = true;

                    terrainFields.Add(tField);

                }

            }

            // ------------------------------------------------------
            // flowers

            codes = new()
            {
                [0] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [1] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [2] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [3] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [4] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [5] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 11, 2 }, new() { 44, 2 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [6] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 11, 2 }, new() { 44, 2 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [7] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 11, 2 }, new() { 44, 2 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [8] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 11, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 44, 2 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [9] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 20, 2 }, new() { 22, 2 }, new() { 33, 2 }, new() { 35, 2 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [10] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 7, 2 }, new() { 8, 2 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 44, 2 }, new() { 45, 2 }, new() { 46, 2 }, new() { 47, 2 }, new() { 48, 2 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [11] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 7, 2 }, new() { 11, 2 }, new() { 44, 2 }, new() { 48, 2 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [12] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [13] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [14] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [15] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [16] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [17] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [18] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [19] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [20] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [21] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [22] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [23] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [24] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [25] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [26] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [27] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [28] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [29] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [30] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [31] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [32] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [33] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [34] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },
                [35] = new() { new() { 0, 1 }, new() { 1, 1 }, new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, new() { 49, 1 }, new() { 50, 1 }, new() { 51, 1 }, new() { 52, 1 }, new() { 53, 1 }, new() { 54, 1 }, new() { 55, 1 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    if (!reserved.Contains(new Vector2(array[0],code.Key)))
                    {

                        switch (array[1])
                        {
                            case 1:

                                if(Mod.instance.randomIndex.Next(8) == 0)
                                {
                                    
                                    Flower flower = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.flower);

                                    terrainFields.Add(flower);

                                }

                                break;

                            case 2:

                                Flower rose = new(new Vector2(array[0], code.Key) * 64, Flower.grasstypes.rose);

                                terrainFields.Add(rose);

                                break;


                        }

                    }


                }

            }

            addDialogue();

            this.map = newMap;

        }
        public override void addDialogue()
        {

            if (dialogueTiles.Count > 0) { return; }

            dialogueTiles.Add(new(39, 7), CharacterHandle.characters.keeper);

            dialogueTiles.Add(new(39, 8), CharacterHandle.characters.keeper);

            dialogueTiles.Add(new(39, 9), CharacterHandle.characters.keeper);

            dialogueTiles.Add(new(40, 7), CharacterHandle.characters.keeper);

            dialogueTiles.Add(new(40, 8), CharacterHandle.characters.keeper);

            dialogueTiles.Add(new(40, 9), CharacterHandle.characters.keeper);

            // graves

            dialogueTiles.Add(new(15, 9), CharacterHandle.characters.epitaph_prince);

            dialogueTiles.Add(new(16, 9), CharacterHandle.characters.epitaph_prince);

            dialogueTiles.Add(new(9, 11), CharacterHandle.characters.epitaph_isles);

            dialogueTiles.Add(new(46, 11), CharacterHandle.characters.epitaph_knoll);

            dialogueTiles.Add(new(11, 18), CharacterHandle.characters.epitaph_servants_oak);

            dialogueTiles.Add(new(13, 18), CharacterHandle.characters.epitaph_servants_oak);

            dialogueTiles.Add(new(46, 18), CharacterHandle.characters.epitaph_servants_holly);

            dialogueTiles.Add(new(48, 18), CharacterHandle.characters.epitaph_servants_holly);

            dialogueTiles.Add(new(20, 17), CharacterHandle.characters.epitaph_kings_oak);

            dialogueTiles.Add(new(20, 18), CharacterHandle.characters.epitaph_kings_oak);

            dialogueTiles.Add(new(35, 17), CharacterHandle.characters.epitaph_kings_holly);

            dialogueTiles.Add(new(35, 18), CharacterHandle.characters.epitaph_kings_holly);

            dialogueTiles.Add(new(13, 22), CharacterHandle.characters.epitaph_guardian);

            dialogueTiles.Add(new(13, 23), CharacterHandle.characters.epitaph_guardian);

            dialogueTiles.Add(new(42, 22), CharacterHandle.characters.epitaph_dragon);

            dialogueTiles.Add(new(42, 23), CharacterHandle.characters.epitaph_dragon);

        }

        public override void updateWarps()
        {

            warps.Clear();

            // top exit

            warps.Add(new Warp(26, 4, "Town", 47, 88, flipFarmer: false));

            warps.Add(new Warp(27, 4, "Town", 47, 88, flipFarmer: false));

            warps.Add(new Warp(28, 4, "Town", 47, 88, flipFarmer: false));

            warps.Add(new Warp(29, 4, "Town", 47, 88, flipFarmer: false));

            // bottom exit

            warps.Add(new Warp(26, 32, "Town", 47, 88, flipFarmer: false));

            warps.Add(new Warp(27, 32, "Town", 47, 88, flipFarmer: false));

            warps.Add(new Warp(28, 32, "Town", 47, 88, flipFarmer: false));

            warps.Add(new Warp(29, 32, "Town", 47, 88, flipFarmer: false));

        }

    }

}
