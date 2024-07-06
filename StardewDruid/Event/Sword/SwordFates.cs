
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Threading;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Event.Sword
{
    internal class SwordFates : EventHandle
    {

        public bool entranceFound;

        public Vector2 statueVector;

        public Dictionary<Vector2, Tile> heldTiles = new();

        public Event.Access.AccessHandle CourtAccess;

        public Event.Access.AccessHandle TunnelAccess;

        public SwordFates()
        {

        }

        public override void TriggerInterval()
        {
            
            if (!entranceFound)
            {
                
                Vector2 entrance = (location as MineShaft).tileBeneathLadder;

                origin = (entrance + new Vector2(0, 2)) * 64;

                statueVector = new Vector2(30,6) * 64;

                for (int x = 28; x < 32; x++)
                {

                    for (int y = 4; y < 8; y++)
                    {

                        if (location.isActionableTile(x, y, Game1.player))
                        {

                            foreach (Layer layer in location.map.Layers)
                            {

                                if (layer.Tiles[x, y] != null)
                                {

                                    Vector2 tileVector = new(x, y);

                                    heldTiles[tileVector] = layer.Tiles[x, y];

                                    layer.Tiles[x, y] = new StaticTile(layer, heldTiles[tileVector].TileSheet, BlendMode.Alpha, heldTiles[tileVector].TileIndex);

                                }

                            }

                        };

                    }

                }

                entranceFound = true;

            }

            base.TriggerInterval();

        }

        public override void EventRemove()
        {

            companions.Clear();
            
            base.EventRemove();

            foreach(KeyValuePair<Vector2,Tile> tile in heldTiles)
            {

                Layer layer = tile.Value.Layer;

                layer.Tiles[(int)tile.Key.X, (int)tile.Key.Y] = tile.Value;

            }

            heldTiles.Clear();

        }

        public override void EventActivate()
        {

            base.EventActivate();

            locales = new() { "UndergroundMine77377", LocationData.druid_court_name, };

            monsterHandle = new(origin, location);

            monsterHandle.spawnSchedule = new();

            for (int i = 1; i <= 90; i += 2)
            {

                monsterHandle.spawnSchedule.Add(i, new() { new(MonsterHandle.bosses.spectre, 4, Mod.instance.randomIndex.Next(2)) });

            }

            monsterHandle.spawnWithin = ModUtility.PositionToTile(Game1.player.Position);

            monsterHandle.spawnRange = new(15, 15);

            monsterHandle.spawnWater = true;

            monsterHandle.spawnVoid = true;

            eventProximity = -1;

            activeLimit = 90;

            EventBar(Mod.instance.questHandle.quests[eventId].title, 0);

        }

        public override void EventInterval()
        {

            activeCounter++;

            DialogueCue(activeCounter);

            if (activeCounter <= 10)
            {

                EventPartOne();

            }
            else
            if(activeCounter <= 90)
            {

                EventPartTwo();

            }
            else
            if(activeCounter <= 120)
            {

                EventPartThree();

            }
            else
            {

                EventPartFour();

            }

        }

        public void EventPartOne()
        {

            switch (activeCounter)
            {
                
                case 1:

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Jester, Character.Character.mode.track);

                    companions[0] = Mod.instance.characters[CharacterHandle.characters.Jester];

                    voices[0] = companions[0];
                    
                    break;

                case 6:
                case 8:
                    location.playSound("ghost");

                    break;
                
                case 10:

                    DialogueCue(900);

                    location.playSound("ghost");
                        
                    SetTrack("cowboy_outlawsong");

                    break;

            }

        }


        public void EventPartTwo()
        {
            
            if (activeCounter % 10 == 0)
            {

                Mod.instance.trackers[CharacterHandle.characters.Jester].WarpToPlayer();

                if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Effigy))
                {

                    Mod.instance.trackers[CharacterHandle.characters.Effigy].WarpToPlayer();

                }

                DialogueCue(900);

            }

            monsterHandle.spawnWithin = ModUtility.PositionToTile(Game1.player.Position);

            monsterHandle.SpawnCheck();

            monsterHandle.SpawnInterval();

            if(Vector2.Distance(Game1.player.Position,statueVector) <= 384)
            {

                activeCounter = 90;

                activeLimit = 160;

            }

        }

        public void EventPartThree()
        {

            if (Game1.player.currentLocation.Name == LocationData.druid_court_name)
            {

                activeCounter = 120;

                return;

            }

            switch (activeCounter)
            {

                case 91:

                    companions[0].Position = new Vector2(27, 6) * 64;

                    companions[0].Halt();

                    companions[0].idleTimer = 600;

                    companions[0].netDirection.Set(1);

                    break;

                case 93:

                    if (!Game1.player.mailReceived.Contains("gotGoldenScythe"))
                    {
                        
                        ThrowHandle swordThrow = new(Game1.player, companions[0].Position, SpawnData.swords.scythe);

                        swordThrow.register();

                        Game1.playSound("parry");

                        Game1.player.mailReceived.Add("gotGoldenScythe");

                        location.setMapTileIndex(29, 4, 245, "Front");

                        location.setMapTileIndex(30, 4, 246, "Front");

                        location.setMapTileIndex(29, 5, 261, "Front");

                        location.setMapTileIndex(30, 5, 262, "Front");

                        location.setMapTileIndex(29, 6, 277, "Buildings");

                        location.setMapTileIndex(30, 56, 278, "Buildings");

                    }

                    break;

                case 98:

                    CourtAccess = new();

                    CourtAccess.AccessSetup("UndergroundMine77377", LocationData.druid_court_name, new(29, 8), new(45, 20));

                    CourtAccess.location = location;

                    CourtAccess.AccessStair();

                    TunnelAccess = new();

                    TunnelAccess.AccessSetup(LocationData.druid_court_name, "UndergroundMine77377", new(45, 20), new(29, 10));

                    TunnelAccess.location = Mod.instance.locations[LocationData.druid_court_name];

                    TunnelAccess.AccessStair();

                    break;

                case 99:

                    location.localSound("secret1");

                    break;

                case 102:

                    Game1.warpFarmer(LocationData.druid_court_name, 41, 20, 1);

                    Game1.xLocationAfterWarp = 41;

                    Game1.yLocationAfterWarp = 20;

                    location = Mod.instance.locations[LocationData.druid_court_name];

                    CharacterMover.Warp(Mod.instance.locations[LocationData.druid_court_name], companions[0], new Vector2(40, 18) * 64);

                    break;

                case 106:

                    activeCounter = 120;

                    break;


            }

        }


        public void EventPartFour()
        {

            switch (activeCounter)
            {

                case 121:

                    DialogueCue(121);

                    break;

                case 124:

                    companions[0].LookAtTarget(Game1.player.Position);

                    DialogueLoad(0, 1);

                    break;

                case 128:

                    DialogueNext(companions[0]);

                    break;

                case 131:

                    companions[0].LookAtTarget(Game1.player.Position);

                    DialogueLoad(0,2);

                    break;

                case 135:

                    DialogueNext(companions[0]);

                    break;

                case 141:

                    companions[0].LookAtTarget(Game1.player.Position);

                    DialogueLoad(0, 3);

                    break;

                case 145:

                    DialogueNext(companions[0]);

                    break;

                case 151:

                    CourtAccess.AccessWarps();

                    TunnelAccess.AccessWarps();

                    eventComplete = true;

                    break;


            }


        }

    }

}