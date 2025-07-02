using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Ether;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Extensions;
using StardewValley.GameData.Crops;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Locations;
using StardewValley.Minigames;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Timers;

namespace StardewDruid.Cast.Effect
{
    public class Whirlpool : EventHandle
    {

        public Dictionary<Vector2, TornadoTarget> tornadoes = new();

        public bool iridium;

        public Whirlpool()
        {

            iridium = (Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areAllAreasComplete();

        }

        public virtual void AddTarget(GameLocation location, Vector2 tile)
        {

            if (tornadoes.ContainsKey(tile))
            {
                return;
            }

            tornadoes.Add(tile, new(location, tile));

            activeLimit = eventCounter + 16;

        }

        public override void EventDecimal()
        {

            // -------------------------------------------------
            // Fish
            // -------------------------------------------------

            for(int h = tornadoes.Count - 1; h >= 0; h--)
            {

                KeyValuePair<Vector2, TornadoTarget> tornado = tornadoes.ElementAt(h);

                if ((tornado.Value.counter <= 0))
                {

                    tornado.Value.ReleaseCatch();

                    tornadoes.Remove(tornado.Key);

                }

                tornado.Value.counter--;

                if(tornado.Value.counter % 3 != 0)
                {

                    continue;

                }

                int targetRadius = (tornado.Value.limit - tornado.Value.counter) / 3;

                List<Vector2> tileVectors = ModUtility.GetTilesWithinRadius(location, tornado.Value.tile, targetRadius);

                if(targetRadius == 1)
                {

                    tileVectors.Add(tornado.Value.tile);

                }

                int difficulty = Mod.instance.ModDifficulty();

                foreach (Vector2 tileVector in tileVectors)
                {

                    if(Mod.instance.randomIndex.Next(16 + difficulty) == 0)
                    {

                        if (Mod.instance.randomIndex.Next(10-Mod.instance.PowerLevel) == 0)
                        {

                            string highFish = SpawnData.RandomHighFish(location, tileVector);

                            tornado.Value.AddCatch(highFish, tileVector);

                            continue;

                        }

                        string randomFish = SpawnData.RandomLowFish(location, tileVector);

                        tornado.Value.AddCatch(randomFish, tileVector);

                    }

                }

            }

        }

    }

    public class TornadoTarget
    {

        public Vector2 tile;

        public GameLocation location;

        public int counter;

        public int limit;

        public Dictionary<string,int> fishes = new();

        public TemporaryAnimatedSprite animation;

        public TornadoTarget(GameLocation Location, Vector2 Tile)
        {

            tile = Tile;

            counter = 21;

            limit = 21;

            location = Location;

        }

        public void AddCatch(string fish, Vector2 fishTile)
        {

            StardewValley.Object item = new(fish, 1, false, -1, 0);

            ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(item.QualifiedItemId);

            if (dataOrErrorItem.IsErrorItem)
            {

                return;

            }

            if (fishes.ContainsKey(fish))
            {

                if (fishes[fish] >= 3)
                {

                    return;

                }

                fishes[fish]++;

            }
            else
            {

                fishes[fish] = 1;

            }

            Microsoft.Xna.Framework.Rectangle itemRect = dataOrErrorItem.GetSourceRect(0, item.ParentSheetIndex);

            float interval = counter * 100f;

            Vector2 tilePosition = tile * 64;

            Vector2 fishPosition = fishTile * 64f;

            int direction = ModUtility.DirectionToTarget(tilePosition, fishPosition)[2];

            direction = (direction + 6) % 8;

            Vector2 newPosition = tilePosition + ModUtility.DirectionAsVector(direction) * 96;

            Vector2 difference = (newPosition - fishPosition);

            Vector2 movement = difference / (interval * 2f);

            animation = new(0, interval, 1, 1, fishTile * 64 + new Vector2(12), false, false)
            {
                sourceRect = itemRect,
                sourceRectStartingPos = new(itemRect.X, itemRect.Y),
                texture = dataOrErrorItem.GetTexture(),
                layerDepth = 0.005f,
                alphaFade = 0.0005f,
                motion = movement,
                timeBasedMotion = true,
                rotationChange = (float)Math.PI / 1000f,
                scale = 3f,

            };

            Game1.player.currentLocation.TemporarySprites.Add(animation);

        }

        public void ReleaseCatch()
        {

            Dictionary<int, List<StardewValley.Object>> treasureObjects = new();

            int bounty = 0;

            treasureObjects[0] = new();

            foreach (KeyValuePair<string,int> fish in fishes)
            {
                int quality = 0;

                StardewValley.Object candidate = new(fish.Key, 1);

                if (candidate.Category == StardewValley.Object.FishCategory)
                {

                    if (candidate.BaseName.Contains("Jelly"))
                    {

                        quality = 0;

                    }
                    else if (Game1.player.FishingLevel >= 9)
                    {

                        quality = 4;

                    }
                    else if (Mod.instance.randomIndex.Next(11 - Game1.player.FishingLevel) <= 0)
                    {

                        quality = 2;

                    }

                }

                candidate.Quality = quality;

                if(location is not Town && Mod.instance.ModDifficulty() > 5)
                {

                    if (candidate.sellToStorePrice() > 200)
                    {

                        for (int i = 0; i < fish.Value; i++)
                        {

                            StardewValley.Object toss = new(fish.Key, 1)
                            {
                                Quality = quality
                            };

                            treasureObjects[bounty].Add(toss);

                            if (treasureObjects[bounty].Count >= 4)
                            {

                                bounty++;

                                treasureObjects[bounty] = new();

                            }

                        }

                        continue;

                    }

                }

                for (int i = 0; i < fish.Value; i++)
                {

                    StardewValley.Object toss = new(fish.Key, 1)
                    {
                        Quality = quality
                    };

                    Vector2 offset = tile * 64 + new Vector2(-64 + Mod.instance.randomIndex.Next(10) * 16, -64 + Mod.instance.randomIndex.Next(10) * 16);

                    ThrowHandle throwObject = new(Game1.player, offset, toss)
                    {
                        delay = Mod.instance.randomIndex.Next(5) * 10
                    };

                    throwObject.register();

                    Mod.instance.GiveExperience(1, quality * 12); // gain fishing experience

                    Game1.player.caughtFish(candidate.ItemId, 1, false, 1);

                }

                SpellHandle splash = new(tile * 64, 160, IconData.impacts.fish, new())
                {
                    sound = SpellHandle.Sounds.pullItemFromWater
                };

                Mod.instance.spellRegister.Add(splash);

            }

            foreach (KeyValuePair<int, List<StardewValley.Object>> treasureHorde in treasureObjects)
            {

                if(treasureHorde.Value.Count == 0)
                {

                    continue;

                }

                string treasureId = "treasure_chase_" + treasureHorde.Key + "_" + Game1.player.currentLocation.Name;

                if (Mod.instance.eventRegister.ContainsKey(treasureId))
                {

                    continue;

                }

                Crate treasure = new();

                treasure.EventSetup(tile * 64, treasureId, false);

                treasure.crateThief = true;

                if(treasureObjects.Count > 1)
                {

                    treasure.skipBattle = true;

                }

                treasure.heldTreasure = true;

                treasure.crateTerrain = 2;

                treasure.treasures = treasureHorde.Value;

                treasure.location = Game1.player.currentLocation;

                treasure.EventActivate();

            }

        }


    }

}
