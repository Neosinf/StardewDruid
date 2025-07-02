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
using StardewValley.Objects;
using System.Runtime.Intrinsics.X86;
using StardewValley.GameData.Locations;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley.TerrainFeatures;
using System.Threading;
using xTile;
using StardewDruid.Character;
using StardewDruid.Location.Terrain;

namespace StardewDruid.Location.Druid
{

    public class Cavern : DruidLocation
    {

        public Cavern() { }

        public Cavern(string Name)
            : base(Name)
        {

        }

        public override void OnMapLoad(Map map)
        {

            xTile.Dimensions.Size tileSize = map.GetLayer("Back").TileSize;

            Map newMap = new(map.Id);

            Layer back = new("Back", newMap, new(20, 16), tileSize);

            newMap.AddLayer(back);

            Layer buildings = new("Buildings", newMap, new(20, 16), tileSize);

            newMap.AddLayer(buildings);

            Layer front = new("Front", newMap, new(20, 16), tileSize);

            newMap.AddLayer(front);

            Layer alwaysfront = new("AlwaysFront", newMap, new(20, 16), tileSize);

            newMap.AddLayer(alwaysfront);

            TileSheet groundsheet = new(
                newMap,
                "StardewDruid.Tilesheets.cavernground",
                new(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.cavernground].Width / 16,
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.cavernground].Height / 16
                ),
                new(16, 16)
            );

            newMap.AddTileSheet(groundsheet);

            newMap.LoadTileSheets(Game1.mapDisplayDevice);

            IsOutdoors = false;

            ignoreOutdoorLighting.Set(false);

            mapReset();

            Dictionary<int, List<List<int>>> codes = new()
            {
                [5] = new() { new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, },
                [6] = new() { new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, },
                [7] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [8] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 22 }, new() { 11, 23 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [9] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 42 }, new() { 11, 43 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [10] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [11] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 20 }, new() { 6, 21 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 26 }, new() { 13, 27 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [12] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 40 }, new() { 6, 41 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 46 }, new() { 13, 47 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [13] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 24 }, new() { 11, 25 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [14] = new() { new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 28 }, new() { 8, 29 }, new() { 9, 1 }, new() { 10, 44 }, new() { 11, 45 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, },
                [15] = new() { new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 48 }, new() { 8, 49 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, },

            };

            int indexUsed = 0;

            int indexSelected = 0;

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    if (array[1] == 1)
                    {

                        while(indexSelected == indexUsed)
                        {

                            indexSelected = Mod.instance.randomIndex.Next(20);

                        }

                        back.Tiles[array[0], code.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, indexSelected);

                        indexUsed = indexSelected; 

                    }
                    else
                    {

                        back.Tiles[array[0], code.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, array[1]);

                    }

                    back.Tiles[array[0], code.Key].TileIndexProperties.Add("Type", "Stone");

                }

            }

            codes = new()
            {
                [5] = new() { new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, },
                [6] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 12, 1 }, new() { 13, 1 }, },
                [7] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 13, 1 }, new() { 14, 1 }, },
                [8] = new() { new() { 4, 1 }, new() { 5, 1 }, new() { 14, 1 }, new() { 15, 1 }, },
                [9] = new() { new() { 3, 1 }, new() { 4, 1 }, new() { 15, 1 }, new() { 16, 1 }, },
                [10] = new() { new() { 3, 1 }, new() { 4, 1 }, new() { 15, 1 }, new() { 16, 1 }, },
                [11] = new() { new() { 3, 1 }, new() { 16, 1 }, },
                [12] = new() { new() { 3, 1 }, new() { 16, 1 }, },
                [13] = new() { new() { 3, 1 }, new() { 4, 1 }, new() { 15, 1 }, new() { 16, 1 }, },
                [14] = new() { new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, },
                [15] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    buildings.Tiles[array[0], code.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, DruidLocation.cavernIndex);

                }

            }

            codes = new()
            {
                [0] = new() { new() { 6, 1 }, },
                [1] = new() { new() { 3, 15 }, new() { 13, 16 }, },
                [2] = new() { },
                [3] = new() { new() { 1, 11 }, new() { 15, 14 }, },
                [4] = new() { new() { 7, 51 }, new() { 11, 52 }, },
                [5] = new() { },
                [6] = new() { new() { 0, 21 }, new() { 17, 24 }, },
                [7] = new() { },
                [8] = new() { },
                [9] = new() { },
                [10] = new() { },
                [11] = new() { new() { 0, 31 }, new() { 16, 34 }, },
                [12] = new() { },
                [13] = new() { new() { 3, 32 }, new() { 13, 33 }, },
                [14] = new() { },
                [15] = new() { new() { 6, 41 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    CaveWall caveWall = new(new Vector2(array[0], code.Key) * 64, array[1]);

                    terrainFields.Add(caveWall);

                }

            }

            // ground mushrooms
            codes = new()
            {
                [9] = new() { new() { 3, 1 }, new() { 15, 2 }, },
                [10] = new() { },
                [11] = new() { new() { 2, 3 }, new() { 4, 4 }, new() { 14, 3 }, new() { 16, 1 }, },


            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    bool mushroomFlip = (array[0] > 7);

                    Mushroom mushroomField = new(new Vector2(array[0], code.Key) * 64, (Mushroom.MushroomIndex)array[1]);

                    if (mushroomFlip)
                    {

                        mushroomField.flip = true;

                    }

                    terrainFields.Add(mushroomField);

                    foreach (Vector2 bottom in mushroomField.baseTiles)
                    {

                        if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                        {

                            buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                        }

                    }

                }

            }

            
            codes = new()
            {
                [0] = new() { new() { 1, 3 },},


            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    CaveRay light = new(new Vector2(array[0], code.Key) * 64, array[1], array[0] > 7);

                    terrainFields.Add(light);

                }

            }

            this.map = newMap;

        }

        public override void performTenMinuteUpdate(int timeOfDay)
        {

            base.performTenMinuteUpdate(timeOfDay);

            GameLocation farmLocation = Game1.getFarm();

            if (farmLocation.warps[0].TargetName != Name)
            {

                updateWarps();

            }

        }


        public override void updateWarps()
        {

            warps.Clear();

            // Farm warps

            GameLocation farmLocation = Game1.getFarm();

            GameLocation caveLocation = Game1.getLocationFromName("FarmCave");

            int farmX = 34;

            int farmY = 5;

            int caveX = 8;

            int caveY = 12;

            for (int w = farmLocation.warps.Count - 1; w >= 0; w--)
            {

                Warp tryWarp = farmLocation.warps[w];

                if (tryWarp.TargetName == LocationHandle.druid_cavern_name)
                {

                    farmX = tryWarp.X;

                    farmY = tryWarp.Y;

                    farmLocation.warps.RemoveAt(w);

                    continue;

                }

                GameLocation tryLocation = Game1.getLocationFromName(tryWarp.TargetName);

                if (tryLocation == null)
                {

                    continue;

                }

                if (tryLocation is FarmCave)
                {

                    caveLocation = tryLocation;

                    farmX = tryWarp.X;

                    farmY = tryWarp.Y;

                }

            }

            if (caveLocation == null)
            {

                return;

            }

            for (int w = caveLocation.warps.Count - 1; w >= 0; w--)
            {

                Warp tryWarp = caveLocation.warps[w];

                if (tryWarp.TargetName == LocationHandle.druid_cavern_name)
                {

                    caveX = tryWarp.X;

                    caveY = tryWarp.Y;

                    caveLocation.warps.RemoveAt(w);

                    continue;

                }

                GameLocation tryLocation = Game1.getLocationFromName(tryWarp.TargetName);

                if (tryLocation == null)
                {

                    continue;

                }

                if (tryLocation is Farm)
                {

                    caveX = tryWarp.X;

                    caveY = tryWarp.Y;

                }

            }

            farmLocation.warps.Insert(0, new(farmX, farmY, LocationHandle.druid_cavern_name, 9, 13, flipFarmer: false));

            caveLocation.warps.Insert(0, new(caveX, caveY, LocationHandle.druid_cavern_name, 11, 9, flipFarmer: false));

            warps.Add(new Warp(8, 15, farmLocation.Name, farmX, farmY + 2, flipFarmer: false));

            warps.Add(new Warp(9, 15, farmLocation.Name, farmX, farmY + 2, flipFarmer: false));

            warps.Add(new Warp(10, 15, farmLocation.Name, farmX, farmY + 2, flipFarmer: false));

            warps.Add(new Warp(11, 15, farmLocation.Name, farmX, farmY + 2, flipFarmer: false));

            warps.Add(new Warp(5, 7, LocationHandle.druid_grove_name, 29, 26, flipFarmer: false));

            warps.Add(new Warp(6, 7, LocationHandle.druid_grove_name, 29, 26, flipFarmer: false));

            warps.Add(new Warp(5, 8, LocationHandle.druid_grove_name, 29, 26, flipFarmer: false));

            warps.Add(new Warp(13, 7, caveLocation.Name, caveX, caveY - 2, flipFarmer: false));

            warps.Add(new Warp(14, 7, caveLocation.Name, caveX, caveY - 2, flipFarmer: false));

            warps.Add(new Warp(13, 8, caveLocation.Name, caveX, caveY - 2, flipFarmer: false));

        }

    }

}
