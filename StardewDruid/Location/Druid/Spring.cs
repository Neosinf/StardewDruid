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
using System.ComponentModel;
using System.Xml.Linq;
using StardewDruid.Cast;
using System.Diagnostics.Metrics;
using StardewValley.Tools;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Handle;
using StardewDruid.Location.Terrain;

namespace StardewDruid.Location.Druid
{
    public class Spring : DruidLocation
    {

        public bool unlockDistillery;

        public Spring() { }

        public Spring(string Name)
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

                            if (terrain is SpringStructure structure)
                            {

                                if (terrain.ruined)
                                {

                                    terrain.ruined = false;

                                }

                            }

                        }

                        DistilleryWarp();

                        break;

                    case 2:

                        foreach (TerrainField terrain in terrainFields)
                        {

                            if(terrain is SpringLily lily)
                            {

                                if(lily.lilyIndex == SpringLily.LilySource.pad)
                                {

                                    terrain.disabled = false;

                                }

                            }

                        }

                        break;

                    case 3:

                        foreach (TerrainField terrain in terrainFields)
                        {

                            if (terrain is SpringLily lily)
                            {

                                //if (lily.lilyIndex == SpringLily.LilySource.pad)
                                if (lily.lilyIndex == SpringLily.LilySource.flower)
                                {

                                    //lily.lilyFlower = true;
                                    terrain.disabled = false;
                                }

                            }

                        }
                        break;


                }

            }

            restoration = restore;

        }      

        public override void checkForMusic(GameTime time)
        {

            int ambientSound = Mod.instance.randomIndex.Next(2000);

            if (ambientSound < 1)
            {
                localSound(SpellHandle.Sounds.batFlap.ToString());
            }
            else
            if (ambientSound < 2)
            {
                Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BatScreech);
            }

            base.checkForMusic(time);

            if (!Game1.IsMusicContextActive())
            {

                Game1.changeMusicTrack("Upper_Ambient", true);

            }

        }

        protected override void _updateAmbientLighting()
        {

            Game1.ambientLight = new(96, 96, 64);

        }

        public override void OnMapLoad(Map map)
        {
            xTile.Dimensions.Size tileSize = map.GetLayer("Back").TileSize;
            
            Map newMap = new(map.Id);
            
            Layer back = new("Back", newMap, new(56, 36), tileSize);
            
            newMap.AddLayer(back);
            
            Layer buildings = new("Buildings", newMap, new(56, 36), tileSize);
            
            newMap.AddLayer(buildings);
            
            Layer front = new("Front", newMap, new(56, 36), tileSize);
            
            newMap.AddLayer(front);
            
            Layer alwaysfront = new("AlwaysFront", newMap, new(56, 36), tileSize);
            
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
                [3] = new() { new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, },
                [4] = new() { new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, },
                [5] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, },
                [6] = new() { new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, },
                [7] = new() { new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [8] = new() { new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, },
                [9] = new() { new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 80 }, new() { 21, 81 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 86 }, new() { 31, 87 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, },
                [10] = new() { new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 100 }, new() { 21, 101 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 106 }, new() { 31, 107 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 80 }, new() { 39, 81 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, },
                [11] = new() { new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 100 }, new() { 39, 101 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, },
                [12] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [13] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [14] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 88 }, new() { 13, 89 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [15] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 108 }, new() { 13, 109 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 22 }, new() { 29, 23 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 86 }, new() { 38, 87 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [16] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 42 }, new() { 29, 43 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 106 }, new() { 38, 107 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [17] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 84 }, new() { 16, 85 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 26 }, new() { 32, 27 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [18] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 104 }, new() { 16, 105 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 28 }, new() { 26, 29 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 46 }, new() { 32, 47 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [19] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 48 }, new() { 26, 49 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [20] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [21] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 20 }, new() { 22, 21 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [22] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 40 }, new() { 22, 41 }, new() { 23, 1 }, new() { 24, 24 }, new() { 25, 25 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 88 }, new() { 40, 89 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [23] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 44 }, new() { 25, 45 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 20 }, new() { 31, 21 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 108 }, new() { 40, 109 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [24] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 40 }, new() { 31, 41 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [25] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 84 }, new() { 41, 85 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [26] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 82 }, new() { 11, 83 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 104 }, new() { 41, 105 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [27] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 102 }, new() { 11, 103 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [28] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 80 }, new() { 37, 81 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [29] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 86 }, new() { 17, 87 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 100 }, new() { 37, 101 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [30] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 106 }, new() { 17, 107 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [31] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 80 }, new() { 19, 81 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 84 }, new() { 28, 85 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [32] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 100 }, new() { 19, 101 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 104 }, new() { 28, 105 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [33] = new() { new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, },
                [34] = new() { new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [35] = new() { new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, },

            };

            int indexUsed = 0;

            int indexSelected = 0;

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    if (array[1] == 1)
                    {

                        while (indexSelected == indexUsed)
                        {

                            indexSelected = Mod.instance.randomIndex.Next(20);

                        }

                        back.Tiles[array[0], code.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, indexSelected);

                        indexUsed = indexSelected;

                        back.Tiles[array[0], code.Key].TileIndexProperties.Add("Type", "Stone");

                    }
                    else if (array[1] == 2)
                    {

                        while (indexSelected == indexUsed)
                        {

                            indexSelected = Mod.instance.randomIndex.Next(60, 80);

                        }

                        back.Tiles[array[0], code.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, indexSelected);

                        indexUsed = indexSelected;

                    }
                    else
                    {

                        back.Tiles[array[0], code.Key] = new StaticTile(back, groundsheet, BlendMode.Alpha, array[1]);

                    }

                }

            }

            codes = new()
            {
                [3] = new() { new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, },
                [4] = new() { new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, },
                [5] = new() { new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 1 }, new() { 19, 1 }, new() { 20, 1 }, new() { 21, 1 }, new() { 22, 1 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 1 }, new() { 34, 1 }, new() { 35, 1 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, },
                [6] = new() { new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 1 }, new() { 16, 1 }, new() { 17, 1 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 1 }, new() { 37, 1 }, new() { 38, 1 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, },
                [7] = new() { new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [8] = new() { new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, },
                [9] = new() { new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 80 }, new() { 21, 81 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 86 }, new() { 31, 87 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, },
                [10] = new() { new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 100 }, new() { 21, 101 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 106 }, new() { 31, 107 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 80 }, new() { 39, 81 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, },
                [11] = new() { new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 100 }, new() { 39, 101 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, },
                [12] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [13] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [14] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 88 }, new() { 13, 89 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 1 }, new() { 22, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [15] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 108 }, new() { 13, 109 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 1 }, new() { 33, 1 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 86 }, new() { 38, 87 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [16] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 106 }, new() { 38, 107 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [17] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 84 }, new() { 16, 85 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [18] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 104 }, new() { 16, 105 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [19] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [20] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [21] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [22] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 88 }, new() { 40, 89 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [23] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 108 }, new() { 40, 109 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [24] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [25] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 1 }, new() { 34, 1 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 84 }, new() { 41, 85 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [26] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 82 }, new() { 11, 83 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 1 }, new() { 33, 1 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 104 }, new() { 41, 105 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [27] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 102 }, new() { 11, 103 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 1 }, new() { 22, 1 }, new() { 31, 1 }, new() { 32, 1 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [28] = new() { new() { 5, 1 }, new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 1 }, new() { 24, 1 }, new() { 25, 1 }, new() { 26, 1 }, new() { 27, 1 }, new() { 28, 1 }, new() { 29, 1 }, new() { 30, 1 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 80 }, new() { 37, 81 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, new() { 48, 1 }, },
                [29] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 86 }, new() { 17, 87 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 100 }, new() { 37, 101 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [30] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 106 }, new() { 17, 107 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [31] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 2 }, new() { 10, 2 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 80 }, new() { 19, 81 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 84 }, new() { 28, 85 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 2 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [32] = new() { new() { 6, 1 }, new() { 7, 1 }, new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 2 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 100 }, new() { 19, 101 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 104 }, new() { 28, 105 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 2 }, new() { 43, 2 }, new() { 44, 1 }, new() { 45, 1 }, new() { 46, 1 }, new() { 47, 1 }, },
                [33] = new() { new() { 8, 1 }, new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 2 }, new() { 13, 2 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 2 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, new() { 45, 1 }, },
                [34] = new() { new() { 9, 1 }, new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 2 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 2 }, new() { 40, 2 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, new() { 44, 1 }, },
                [35] = new() { new() { 10, 1 }, new() { 11, 1 }, new() { 12, 1 }, new() { 13, 1 }, new() { 14, 1 }, new() { 15, 2 }, new() { 16, 2 }, new() { 17, 2 }, new() { 18, 2 }, new() { 19, 2 }, new() { 20, 2 }, new() { 21, 2 }, new() { 22, 2 }, new() { 23, 2 }, new() { 24, 2 }, new() { 25, 2 }, new() { 26, 2 }, new() { 27, 2 }, new() { 28, 2 }, new() { 29, 2 }, new() { 30, 2 }, new() { 31, 2 }, new() { 32, 2 }, new() { 33, 2 }, new() { 34, 2 }, new() { 35, 2 }, new() { 36, 2 }, new() { 37, 2 }, new() { 38, 2 }, new() { 39, 1 }, new() { 40, 1 }, new() { 41, 1 }, new() { 42, 1 }, new() { 43, 1 }, },

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
                [0] = new() { new() { 11, 1 }, new() { 19, 2 }, new() { 27, 1 }, new() { 35, 2 }, },
                [1] = new() { new() { 8, 11 }, new() { 42, 13 }, },
                [2] = new() { },
                [3] = new() { new() { 5, 12 }, new() { 45, 14 }, },
                [4] = new() { },
                [5] = new() { new() { 4, 22 }, new() { 47, 23 }, },
                [6] = new() { },
                [7] = new() { },
                [8] = new() { new() { 4, 21 }, new() { 47, 24 }, },
                [9] = new() { },
                [10] = new() { },
                [11] = new() { new() { 4, 22 }, new() { 47, 23 }, },
                [12] = new() { },
                [13] = new() { },
                [14] = new() { new() { 3, 15 }, new() { 47, 16 }, },
                [15] = new() { },
                [16] = new() { new() { 2, 22 }, new() { 49, 23 }, },
                [17] = new() { },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { new() { 2, 32 }, new() { 48, 34 }, },
                [21] = new() { },
                [22] = new() { new() { 4, 21 }, new() { 47, 24 }, },
                [23] = new() { },
                [24] = new() { },
                [25] = new() { },
                [26] = new() { new() { 4, 22 }, new() { 47, 23 }, },
                [27] = new() { },
                [28] = new() { },
                [29] = new() { },
                [30] = new() { new() { 4, 32 }, new() { 46, 34 }, },
                [31] = new() { },
                [32] = new() { new() { 7, 31 }, new() { 43, 33 }, },
                [33] = new() { },
                [34] = new() { new() { 11, 41 }, new() { 19, 42 }, new() { 27, 41 }, new() { 35, 42 }, },
                [35] = new() { },


            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    CaveWall caveWall = new(new Vector2(array[0], code.Key) * 64, array[1]);

                    terrainFields.Add(caveWall);

                }

            }

            codes = new()
            {
                [6] = new() { new() { 11, 12 }, new() { 14, 1 }, new() { 18, 2 }, new() { 22, 1 }, new() { 26, 2 }, new() { 30, 1 }, new() { 34, 2 }, new() { 38, 13 }, },
                [7] = new() { },
                [8] = new() { new() { 8, 11 }, new() { 41, 14 }, },
                [9] = new() { },
                [10] = new() { new() { 7, 21 }, new() { 44, 24 }, },
                [11] = new() { },
                [12] = new() { },
                [13] = new() { new() { 7, 22 }, new() { 22, 41 }, new() { 26, 42 }, new() { 44, 23 }, },
                [14] = new() { new() { 19, 33 }, new() { 30, 31 }, },
                [15] = new() { },
                [16] = new() { new() { 7, 21 }, new() { 18, 23 }, new() { 33, 21 }, new() { 44, 24 }, },
                [17] = new() { },
                [18] = new() { },
                [19] = new() { new() { 7, 22 }, new() { 18, 24 }, new() { 33, 22 }, new() { 44, 23 }, },
                [20] = new() { },
                [21] = new() { },
                [22] = new() { new() { 7, 21 }, new() { 18, 23 }, new() { 33, 21 }, new() { 44, 24 }, },
                [23] = new() { },
                [24] = new() { },
                [25] = new() { new() { 7, 22 }, new() { 19, 13 }, new() { 30, 11 }, new() { 44, 23 }, },
                [26] = new() { },
                [27] = new() { new() { 22, 1 }, new() { 26, 2 }, },
                [28] = new() { new() { 7, 21 }, new() { 44, 24 }, },
                [29] = new() { },
                [30] = new() { },
                [31] = new() { new() { 8, 31 }, new() { 41, 33 }, },
                [32] = new() { },
                [33] = new() { new() { 11, 32 }, new() { 38, 34 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    CaveWater caveWater = new(new Vector2(array[0], code.Key) * 64, array[1]);

                    terrainFields.Add(caveWater);

                }

            }

            codes = new()
            {

                [6] = new() { new() { 24, 2 }, },
                [7] = new() { },
                [8] = new() { },
                [9] = new() { new() { 14, 4 }, new() { 38, 4 }, },
                [10] = new() { },
                [11] = new() { },
                [12] = new() { },
                [13] = new() { new() { 20, 5 }, },
                [14] = new() { },
                [15] = new() { },
                [16] = new() { },
                [17] = new() { new() { 8, 3 }, new() { 34, 6 }, },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { },
                [21] = new() { },
                [22] = new() { },
                [23] = new() { },
                [24] = new() { new() { 14, 4 }, new() { 38, 4 }, },
                [25] = new() { new() { 21, 1 }, },
                [26] = new() { },




            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    switch (array[1])
                    {
                        default:
                        case 1:

                            SpringStructure springgate = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringStructure.SpringSource.gate);

                            springgate.ruined = true;

                            terrainFields.Add(springgate);

                            break;

                        case 2:

                            SpringStructure springshrine = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringStructure.SpringSource.shrine);

                            terrainFields.Add(springshrine);

                            break;

                        case 3:

                            SpringStructure springbridge = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringStructure.SpringSource.bridge);

                            terrainFields.Add(springbridge);

                            break;

                        case 4:

                            SpringStructure springlantern = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringStructure.SpringSource.lantern);

                            terrainFields.Add(springlantern);

                            break;

                        case 5:

                            SpringStructure attendant = new (IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringStructure.SpringSource.attendant);

                            foreach (Vector2 bottom in attendant.baseTiles)
                            {
                                dialogueTiles.Add(bottom, CharacterHandle.characters.attendant);

                                if (back.Tiles[(int)bottom.X, (int)bottom.Y] != null)
                                {

                                    buildings.Tiles[(int)bottom.X, (int)bottom.Y] = new StaticTile(buildings, groundsheet, BlendMode.Alpha, back.Tiles[(int)bottom.X, (int)bottom.Y].TileIndex);

                                }

                            }

                            terrainFields.Add(attendant);

                            break;

                        case 6:

                            SpringStructure springbridgetwo = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringStructure.SpringSource.bridge);

                            springbridgetwo.ruined = true;

                            terrainFields.Add(springbridgetwo);

                            break;

                    }

                }

            }

            codes = new()
            {
                [9] = new() { new() { 19, 1 }, },
                [10] = new() { new() { 15, 1 }, new() { 21, 1 }, new() { 32, 1 }, new() { 34, 2 }, new() { 36, 1 }, },
                [11] = new() { new() { 17, 1 }, new() { 19, 2 }, },
                [12] = new() { new() { 12, 1 }, new() { 34, 1 }, new() { 36, 2 }, new() { 40, 1 }, },
                [13] = new() { new() { 17, 2 }, },
                [14] = new() { new() { 11, 1 }, new() { 36, 1 }, new() { 40, 1 }, },
                [15] = new() { new() { 13, 2 }, new() { 16, 1 }, new() { 38, 2 }, },
                [16] = new() { new() { 11, 1 }, },
                [17] = new() { },
                [18] = new() { },
                [19] = new() { },
                [20] = new() { },
                [21] = new() { },
                [22] = new() { },
                [23] = new() { },
                [24] = new() { new() { 40, 1 }, },
                [25] = new() { new() { 16, 1 }, },
                [26] = new() { new() { 10, 2 }, new() { 12, 1 }, new() { 36, 1 }, new() { 41, 1 }, },
                [27] = new() { new() { 16, 2 }, },
                [28] = new() { new() { 11, 2 }, new() { 18, 1 }, new() { 35, 2 }, new() { 40, 2 }, },
                [29] = new() { },
                [30] = new() { new() { 14, 1 }, new() { 17, 1 }, new() { 19, 2 }, new() { 26, 1 }, new() { 31, 2 }, new() { 34, 1 }, new() { 36, 1 }, },
                [31] = new() { new() { 21, 1 }, new() { 28, 1 }, new() { 40, 1 }, },
                [32] = new() { new() { 13, 1 }, new() { 15, 2 }, new() { 24, 1 }, new() { 26, 2 }, new() { 32, 1 }, new() { 38, 1 }, },
                [33] = new() { new() { 36, 2 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    switch (array[1])
                    {

                        default:

                            SpringLily pad = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringLily.LilySource.pad);

                            pad.disabled = true;

                            terrainFields.Add(pad);

                            break;

                        case 2:

                            SpringLily lily = new(IconData.tilesheets.spring, new Vector2(array[0], code.Key) * 64, SpringLily.LilySource.flower);

                            lily.disabled = true;

                            terrainFields.Add(lily);

                            break;
                    }

                }

            }

            codes = new()
            {
                [0] = new() { new() { 0, 5 }, new() { 14, 5 }, new() { 28, 5 }, },

            };

            foreach (KeyValuePair<int, List<List<int>>> code in codes)
            {

                foreach (List<int> array in code.Value)
                {

                    CaveRay light = new(new Vector2(array[0], code.Key) * 64, array[1]);

                    terrainFields.Add(light);

                }

            }

            this.map = newMap;

        }


        public override bool readyDialogue()
        {

            return Mod.instance.questHandle.IsComplete(QuestHandle.challengeWeald);

        }

        public override void updateWarps()
        {

            warps.Clear();

            warps.Add(new Warp(15, 19, "Mine", 17, 6, flipFarmer: false));

            warps.Add(new Warp(15, 20, "Mine", 17, 6, flipFarmer: false));

            if (unlockDistillery)
            {

                warps.Add(new Warp(38, 19, LocationHandle.druid_distillery_name, 5, 9, flipFarmer: false));

                warps.Add(new Warp(38, 20, LocationHandle.druid_distillery_name, 5, 9, flipFarmer: false));

            }

        }

        public virtual void DistilleryWarp()
        {

            if (unlockDistillery)
            {

                return;

            }

            unlockDistillery = true;

            updateWarps();

            Layer back = map.GetLayer("Back");

            Layer buildings = map.GetLayer("Buildings");

            for (int i = 34; i < 39; i++)
            {

                buildings.Tiles[i, 20] = null;

            }

        }

        /*public override Item getFish(float millisecondsAfterNibble, string bait, int waterDepth, Farmer who, double baitPotency, Vector2 bobberTile, string locationName = null)
        {
            return base.getFish(millisecondsAfterNibble, bait, waterDepth + 2, who, baitPotency, bobberTile, "Mountain");
        }*/


    }

}
