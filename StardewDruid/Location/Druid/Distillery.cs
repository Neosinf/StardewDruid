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
using StardewDruid.Handle;

namespace StardewDruid.Location.Druid
{

    public class Distillery : DruidLocation
    {

        public Distillery() { }

        public Distillery(string Name)
            : base(Name)
        {

        }

        public override void OnMapLoad(Map map)
        {

            xTile.Dimensions.Size tileSize = map.GetLayer("Back").TileSize;

            Map newMap = new(map.Id);

            Layer back = new("Back", newMap, new(22, 14), tileSize);

            newMap.AddLayer(back);

            Layer buildings = new("Buildings", newMap, new(22, 14), tileSize);

            newMap.AddLayer(buildings);

            Layer front = new("Front", newMap, new(22, 14), tileSize);

            newMap.AddLayer(front);

            Layer alwaysfront = new("AlwaysFront", newMap, new(22, 14), tileSize);

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
                [6] = new() { new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, },
                [7] = new() { new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [8] = new() { new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [9] = new() { new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [10] = new() { new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [11] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, },

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
                [4] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, },
                [5] = new() { new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, },
                [6] = new() { new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, },
                [7] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 18, 1 }, new() { 19, 1 }, },
                [8] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 18, 1 }, new() { 19, 1 }, },
                [9] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 18, 1 }, new() { 19, 1 }, },
                [10] = new() { new() { 2, 1 }, new() { 3, 1 }, new() { 18, 1 }, new() { 19, 1 }, },
                [11] = new() { new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [12] = new() { new() { 3, 1 }, new() { 4, 1 }, new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, },
                [13] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    if (array[1] == 1)
                    {

                        buildings.Tiles[array[0], code.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, Mod.instance.randomIndex.Next(20));

                    }
                    else
                    {

                        buildings.Tiles[array[0], code.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, array[1]);

                    }

                }

            }

            codes = new()
            {
                [0] = new() { new() { 3, 12 }, new() { 7, 2 }, new() { 14, 13 }, },
                [1] = new() { new() { 2, 3 }, new() { 17, 4 }, },
                [2] = new() { },
                [3] = new() { new() { 0, 14 }, new() { 19, 14 }, },
                [4] = new() { },
                [5] = new() { },



            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    CaveWall caveWall = new(new Vector2(array[0], code.Key) * 64, array[1]);

                    if (array[0] > 7)
                    {

                        caveWall.flip = true;

                    }

                    terrainFields.Add(caveWall);

                }

            }

            codes = new()
            {
                [4] = new() { new() { 5, 1 }, },
                [5] = new() { new() { 9, 2 }, new() { 12, 3 }, new() { 15, 4 }, },
                [6] = new() { },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    switch (array[1])
                    {
                        default:
                        case 1:

                            SpringStructure attendantTwo = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringStructure.SpringSource.bench);

                            foreach (Vector2 bottom in attendantTwo.baseTiles)
                            {
                                dialogueTiles.Add(bottom, CharacterHandle.characters.spring_bench);

                                if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                                {

                                    buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                                }

                            }

                            terrainFields.Add(attendantTwo);

                            break;

                        case 2:

                            SpringStructure steephouse = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringStructure.SpringSource.steephouse);

                            foreach (Vector2 bottom in steephouse.baseTiles)
                            {
                                dialogueTiles.Add(bottom, CharacterHandle.characters.spring_steephouse);

                                if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                                {

                                    buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                                }

                            }

                            terrainFields.Add(steephouse);

                            break;

                        case 3:

                            SpringStructure batchhouse = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringStructure.SpringSource.batchhouse);

                            foreach (Vector2 bottom in batchhouse.baseTiles)
                            {
                                dialogueTiles.Add(bottom, CharacterHandle.characters.spring_batchhouse);

                                if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                                {

                                    buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                                }

                            }

                            terrainFields.Add(batchhouse);

                            break;

                        case 4:

                            SpringStructure packhouse = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringStructure.SpringSource.packhouse);

                            foreach (Vector2 bottom in packhouse.baseTiles)
                            {
                                dialogueTiles.Add(bottom, CharacterHandle.characters.spring_packhouse);

                                if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                                {

                                    buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                                }

                            }

                            terrainFields.Add(packhouse);

                            break;


                    }

                }

            }

            codes = new()
            {

                [8] = new() { new() { 2, 2 }, },


            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    LightField light = new(new Vector2(array[0], code.Key) * 64 + new Vector2(64, 32))
                    {
                        luminosity = 6,

                        lightAmbience = 0.7f
                    };

                    lightFields.Add(light);

                }

            }

            this.map = newMap;

        }

        /*public override void performTenMinuteUpdate(int timeOfDay)
        {

            base.performTenMinuteUpdate(timeOfDay);

            GameLocation farmLocation = Game1.getFarm();

            if (farmLocation.warps[0].TargetName != Name)
            {

                updateWarps();

            }

        }*/


        public override void updateWarps()
        {

            warps.Clear();

            warps.Add(new Warp(3, 9, LocationHandle.druid_spring_name, 32, 20, flipFarmer: false));

            warps.Add(new Warp(3, 10, LocationHandle.druid_spring_name, 32, 20, flipFarmer: false));

            warps.Add(new Warp(3, 11, LocationHandle.druid_spring_name, 32, 20, flipFarmer: false));

        }

    }

}
