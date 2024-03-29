﻿using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Event.Challenge;
using StardewDruid.Map;
using StardewDruid.Monster.Boss;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Location
{
    public static class LocationData
    {

        public static void LocationEdit(QueryData query)
        {

            switch (query.name)
            {

                case "challengeSandDragon":
                case "challengeSandDragonTwo":
                    SandDragonEdit(); break;
                case "challengeMuseum":
                case "challengeMuseumTwo":
                    MuseumEdit(); break;
                case "swordEther":
                case "swordEtherTwo":
                    SkullCavernEdit();
                    SkullCavernWarp();
                    break;
                case "challengeEther":
                case "challengeEtherTwo":
                    CryptEdit(); break;
            }

        }

        public static void LocationPortal(QueryData query)
        {

            switch (query.name)
            {

                case "swordEther":
                case "swordEtherTwo":
                    SkullCavernPortal(); break;
                case "challengeEther":
                case "challengeEtherTwo":
                    CryptPortal(); break;

            }

        }

        public static void LocationReset(QueryData query)
        {

            switch (query.name)
            {

                case "challengeSandDragon":
                case "challengeSandDragonTwo":
                    SandDragonReset(); break;
                case "challengeMuseum":
                case "challengeMuseumTwo":
                    MuseumReset();  break;
                case "swordEther":
                case "swordEtherTwo":
                    SkullCavernExit(); break;

            }

        }


        public static void LocationReturn(QueryData query)
        {

            switch (query.name)
            {

                case "challengeEther":
                case "challengeEtherTwo":
                    CryptReturn(); break;

            }

        }

        public static void QuestComplete(QueryData query)
        {

            switch (query.name)
            {

                /*case "challengeSandDragon":
                case "challengeMuseum":
                case "challengeGemShrine":

                    new Throw(Game1.player, Game1.player.Position, 74).ThrowObject(); break;*/

                case "swordEther":
                    new Throw().ThrowSword(Game1.player, 57, Game1.player.Position, 500); break;
            }

        }

        public static void SandDragonReset()
        {
            
            GameLocation targetLocation = Game1.getLocationFromName("Desert");
            
            targetLocation.loadMap(targetLocation.mapPath.Value, true);

        }

        public static void SandDragonEdit()
        {
            
            GameLocation targetLocation = Game1.getLocationFromName("Desert");

            Vector2 targetVector = QuestData.SpecialVector(targetLocation, "challengeSandDragon");

            Random randomIndex = new();

            // ----------------------------- clear sheet

            Layer backLayer = targetLocation.map.GetLayer("Back");

            Layer buildingsLayer = targetLocation.map.GetLayer("Buildings");

            Layer frontLayer = targetLocation.map.GetLayer("Front");

            Layer alwaysfrontLayer = targetLocation.map.GetLayer("AlwaysFront");

            TileSheet desertSheet = targetLocation.map.GetTileSheet("desert-new");

            Vector2 offsetVector = targetVector - new Vector2(8, 5);

            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 10; j++)
                {

                    Vector2 tileVector = offsetVector + new Vector2(j, i);

                    if (buildingsLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] != null)
                    {

                        buildingsLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = null;

                    }

                    if (frontLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] != null)
                    {

                        frontLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = null;

                    }

                    if (alwaysfrontLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] != null)
                    {

                        alwaysfrontLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = null;

                    }


                    if (randomIndex.Next(2) != 0)
                    {
                        backLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = new StaticTile(backLayer, desertSheet, BlendMode.Alpha, 65);
                    }
                    else if (randomIndex.Next(3) == 0)
                    {
                        backLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = new StaticTile(backLayer, desertSheet, BlendMode.Alpha, 96);
                    }
                    else if (randomIndex.Next(3) == 0)
                    {
                        backLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = new StaticTile(backLayer, desertSheet, BlendMode.Alpha, 97);
                    }
                    else
                    {
                        backLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = new StaticTile(backLayer, desertSheet, BlendMode.Alpha, 98);
                    }

                }

            }

        }

        public static void MuseumReset()
        {

            GameLocation targetLocation = Game1.getLocationFromName("ArchaeologyHouse");

            targetLocation.loadMap(targetLocation.mapPath.Value, true);

        }

        public static void MuseumEdit()
        {

            Vector2 targetVector = new Vector2(17f, 10f);

            Random randomIndex = new();

            GameLocation targetLocation = Game1.getLocationFromName("ArchaeologyHouse");

            Layer backLayer = targetLocation.map.GetLayer("Back");

            Layer layer2 = targetLocation.map.GetLayer("Buildings");

            Layer layer3 = targetLocation.map.GetLayer("Front");

            Layer layer4 = targetLocation.map.GetLayer("AlwaysFront");

            TileSheet tileSheet = targetLocation.map.TileSheets[1];

            Vector2 vector2_1 = new(targetVector.X - 8, targetVector.Y - 5);//Vector2.op_Subtraction(targetVector, new Vector2(8f, 5f));

            for (int index1 = 0; index1 < 14; ++index1)
            {
                for (int index2 = 0; index2 < 13; ++index2)
                {
                    Vector2 vector2_2 = new(vector2_1.X + index2, vector2_1.Y + index1);//Vector2.op_Addition(vector2_1, new Vector2(index2, index1));

                    if (layer2.Tiles[(int)vector2_2.X, (int)vector2_2.Y] != null)
                        layer2.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = null;

                    if (layer3.Tiles[(int)vector2_2.X, (int)vector2_2.Y] != null)
                        layer3.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = null;

                    if (layer4.Tiles[(int)vector2_2.X, (int)vector2_2.Y] != null)
                        layer4.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = null;

                    if (randomIndex.Next(4) == 0)
                    {

                        backLayer.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = new StaticTile(backLayer, tileSheet, 0, 607);
                    }
                    else if (randomIndex.Next(5) != 0)
                    {
                        backLayer.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = new StaticTile(backLayer, tileSheet, 0, 606);
                    }
                    else if (randomIndex.Next(5) != 0)
                    {
                        backLayer.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = new StaticTile(backLayer, tileSheet, 0, 639);
                    }
                    else
                    {
                        backLayer.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = new StaticTile(backLayer, tileSheet, 0, 638);
                    }

                    backLayer.Tiles[(int)vector2_2.X, (int)vector2_2.Y].TileIndexProperties.Add("Type", new("stone"));

                }

            }



        }


        public static void SkullCavernWarp()
        {
            Game1.warpFarmer("UndergroundMine145", 13, 20, 2);
            Game1.xLocationAfterWarp = 13;
            Game1.yLocationAfterWarp = 20;
        }

        public static void SkullCavernPortal()
        {

            Game1.player.Position = new(13 * 64, 20 * 64);

        }

        public static void SkullCavernEdit()
        {
            
            MineShaft mineShaft;

            if (Context.IsMainPlayer)
            {
                mineShaft = new MineShaft(145);
                MineShaft.activeMines.Clear();
                MineShaft.activeMines.Add(mineShaft);
            }
            else
            {
                mineShaft = MineShaft.GetMine("UndergroundMine145");

            }

            mineShaft.mapPath.Value = "Maps\\Mines\\33";
            mineShaft.loadedMapNumber = 33;
            mineShaft.updateMap();
            mineShaft.mapImageSource.Value = "Maps\\Mines\\mine_desert_dark_dangerous";
            mineShaft.Map.TileSheets[0].ImageSource = "Maps\\Mines\\mine_desert_dark_dangerous";
            mineShaft.Map.LoadTileSheets(Game1.mapDisplayDevice);
            mineShaft.mineLevel = 100;
            mineShaft.chooseLevelType();
            mineShaft.mineLevel = 145;
            mineShaft.findLadder();

            GameLocation location = Game1.getLocationFromName("UndergroundMine145");
            Layer layer1 = location.map.GetLayer("Back");
            Layer layer2 = location.map.GetLayer("Buildings");
            Layer layer3 = location.map.GetLayer("Front");
            TileSheet tileSheet1 = new TileSheet("zestfordragontiles99999999", location.map, Path.Combine("Maps", "DesertTiles"), new Size(16, 23), new Size(1, 1));
            location.map.AddTileSheet(tileSheet1);
            TileSheet tileSheet2 = location.map.TileSheets[0];
            layer1.Tiles[15, 11] = new StaticTile(layer1, tileSheet2, 0, 166);
            layer1.Tiles[16, 11] = new StaticTile(layer1, tileSheet2, 0, 167);
            layer1.Tiles[17, 11] = new StaticTile(layer1, tileSheet2, 0, 167);
            layer1.Tiles[18, 11] = new StaticTile(layer1, tileSheet2, 0, 167);
            layer1.Tiles[19, 11] = new StaticTile(layer1, tileSheet2, 0, 167);
            layer1.Tiles[20, 11] = new StaticTile(layer1, tileSheet2, 0, 168);
            layer1.Tiles[11, 12] = new StaticTile(layer1, tileSheet2, 0, 166);
            layer1.Tiles[12, 12] = new StaticTile(layer1, tileSheet2, 0, 167);
            layer1.Tiles[13, 12] = new StaticTile(layer1, tileSheet2, 0, 167);
            layer1.Tiles[14, 12] = new StaticTile(layer1, tileSheet2, 0, 167);
            layer1.Tiles[15, 12] = new StaticTile(layer1, tileSheet2, 0, 152);
            layer1.Tiles[16, 12] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[17, 12] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[18, 12] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[19, 12] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[20, 12] = new StaticTile(layer1, tileSheet2, 0, 184);
            layer1.Tiles[8, 13] = new StaticTile(layer1, tileSheet2, 0, 166);
            layer1.Tiles[9, 13] = new StaticTile(layer1, tileSheet2, 0, 167);
            layer1.Tiles[10, 13] = new StaticTile(layer1, tileSheet2, 0, 167);
            layer1.Tiles[11, 13] = new StaticTile(layer1, tileSheet2, 0, 152);
            layer1.Tiles[12, 13] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[13, 13] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[14, 13] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[15, 13] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[16, 13] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[17, 13] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[18, 13] = new StaticTile(layer1, tileSheet2, 0, 181);
            layer1.Tiles[19, 13] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[20, 13] = new StaticTile(layer1, tileSheet2, 0, 184);
            layer1.Tiles[8, 14] = new StaticTile(layer1, tileSheet2, 0, 182);
            layer1.Tiles[9, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[10, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[11, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[12, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[13, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[14, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[15, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[16, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[17, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[18, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[19, 14] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[20, 14] = new StaticTile(layer1, tileSheet2, 0, 184);
            layer1.Tiles[8, 15] = new StaticTile(layer1, tileSheet2, 0, 182);
            layer1.Tiles[9, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[10, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[11, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[12, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[13, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[14, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[15, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[16, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[17, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[18, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[19, 15] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[20, 15] = new StaticTile(layer1, tileSheet2, 0, 184);
            layer1.Tiles[8, 16] = new StaticTile(layer1, tileSheet2, 0, 182);
            layer1.Tiles[9, 16] = new StaticTile(layer1, tileSheet2, 0, 181);
            layer1.Tiles[10, 16] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[11, 16] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[12, 16] = new StaticTile(layer1, tileSheet2, 0, 181);
            layer1.Tiles[13, 16] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[14, 16] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[15, 16] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[16, 16] = new StaticTile(layer1, tileSheet2, 0, 150);
            layer1.Tiles[17, 16] = new StaticTile(layer1, tileSheet2, 0, 199);
            layer1.Tiles[18, 16] = new StaticTile(layer1, tileSheet2, 0, 199);
            layer1.Tiles[19, 16] = new StaticTile(layer1, tileSheet2, 0, 199);
            layer1.Tiles[20, 16] = new StaticTile(layer1, tileSheet2, 0, 200);
            layer1.Tiles[8, 17] = new StaticTile(layer1, tileSheet2, 0, 182);
            layer1.Tiles[9, 17] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[10, 17] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[11, 17] = new StaticTile(layer1, tileSheet2, 0, 165);
            layer1.Tiles[12, 17] = new StaticTile(layer1, tileSheet2, 0, 150);
            layer1.Tiles[13, 17] = new StaticTile(layer1, tileSheet2, 0, 199);
            layer1.Tiles[14, 17] = new StaticTile(layer1, tileSheet2, 0, 199);
            layer1.Tiles[15, 17] = new StaticTile(layer1, tileSheet2, 0, 199);
            layer1.Tiles[16, 17] = new StaticTile(layer1, tileSheet2, 0, 200);
            layer1.Tiles[8, 18] = new StaticTile(layer1, tileSheet2, 0, 198);
            layer1.Tiles[9, 18] = new StaticTile(layer1, tileSheet2, 0, 199);
            layer1.Tiles[10, 18] = new StaticTile(layer1, tileSheet2, 0, 199);
            layer1.Tiles[11, 18] = new StaticTile(layer1, tileSheet2, 0, 199);
            layer1.Tiles[12, 18] = new StaticTile(layer1, tileSheet2, 0, 200);
            layer3.Tiles[17, 13] = new StaticTile(layer3, tileSheet1, 0, 3);
            layer3.Tiles[18, 13] = new StaticTile(layer3, tileSheet1, 0, 4);
            layer2.Tiles[17, 14] = new StaticTile(layer2, tileSheet1, 0, 19);
            layer2.Tiles[18, 14] = new StaticTile(layer2, tileSheet1, 0, 20);
            layer3.Tiles[15, 13] = new StaticTile(layer3, tileSheet1, 0, 5);
            layer3.Tiles[15, 14] = new StaticTile(layer3, tileSheet1, 0, 21);
            layer2.Tiles[15, 15] = new StaticTile(layer2, tileSheet1, 0, 37);
            layer3.Tiles[14, 13] = new StaticTile(layer3, tileSheet1, 0, 5);
            layer3.Tiles[14, 14] = new StaticTile(layer3, tileSheet1, 0, 21);
            layer2.Tiles[14, 15] = new StaticTile(layer2, tileSheet1, 0, 37);
            layer3.Tiles[13, 13] = new StaticTile(layer3, tileSheet1, 0, 5);
            layer3.Tiles[13, 14] = new StaticTile(layer3, tileSheet1, 0, 21);
            layer2.Tiles[13, 15] = new StaticTile(layer2, tileSheet1, 0, 37);

        }

        public static void SkullCavernExit()
        {

            Vector2 ladderTile = Game1.player.Tile;

            GameLocation location = Game1.getLocationFromName("UndergroundMine145");

            for (int index1 = 0; index1 < location.map.GetLayer("Buildings").LayerHeight; ++index1)
            {

                for (int index2 = 0; index2 < location.map.GetLayer("Buildings").LayerWidth; ++index2)
                {

                    if (location.map.GetLayer("Buildings").Tiles[index2, index1] != null && location.map.GetLayer("Buildings").Tiles[index2, index1].TileIndex == 115)
                    {
                        ladderTile = new(index2 + 1, index1 + 1);

                        break;

                    }

                }

            }

            Layer layer = location.map.GetLayer("Buildings");

            layer.Tiles[(int)ladderTile.X, (int)ladderTile.Y] = new StaticTile(layer, location.map.TileSheets[0], 0, 174);

            Game1.player.TemporaryPassableTiles.Add(new Microsoft.Xna.Framework.Rectangle((int)ladderTile.X * 64, (int)ladderTile.Y * 64, 64, 64));

            Mod.instance.CastMessage("A way down has appeared");

        }

        public static void CryptPortal()
        {

            Game1.warpFarmer("18465_Crypt", 20, 9, 2);

            Game1.xLocationAfterWarp = 20;

            Game1.yLocationAfterWarp = 9;

        }

        public static void CryptEdit()
        {

            GameLocation crypt = Game1.getLocationFromName("18465_Crypt");

            if (crypt != null)
            {

                return;

            }

            crypt = new Location.Crypt("18465_Crypt");

            Game1.locations.Add(crypt);

            Mod.instance.locationList.Add("18465_Crypt");

        }

        public static void CryptReturn()
        {

            Quest questData = QuestData.RetrieveQuest("challengeCrypt");

            int warpX = (int)questData.triggerVector.X;

            int warpY = (int)questData.triggerVector.Y + 2;

            Game1.warpFarmer("town", warpX, warpY, 2);

            Game1.xLocationAfterWarp = warpX;

            Game1.yLocationAfterWarp = warpY;

        }


    }

}
