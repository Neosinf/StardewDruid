using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Characters;
using StardewValley.GameData.Crops;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            // Crops
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

                List<HoeDirt> hoeDirts = new();

                foreach (Vector2 tileVector in tileVectors)
                {

                    if(Mod.instance.randomIndex.Next(15 - Mod.instance.PowerLevel) == 0)
                    {
                        
                        string randomFish = SpawnData.RandomLowFish(location, tileVector);

                        /*switch (Mod.instance.randomIndex.Next(5))
                        {
                            case 0:
                                randomFish = SpawnData.RandomHighFish(location, false);
                                break;
                            case 1:
                                randomFish = SpawnData.RandomTrash(location).ToString();
                                break;

                        }*/

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

        public List<string> fishes = new();

        public TemporaryAnimatedSprite animation;

        public TornadoTarget(GameLocation Location, Vector2 Tile)
        {

            tile = Tile;

            counter = 24;

            limit = 24;

            location = Location;

        }

        public void AddCatch(string fish, Vector2 fishTile)
        {

            fishes.Add(fish);

            Vector2 tilePosition = tile * 64;

            Vector2 fishPosition = fishTile * 64f;

            StardewValley.Object item = new(fish.ToString(), 1, false, -1, 0);

            ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(item.QualifiedItemId);

            Microsoft.Xna.Framework.Rectangle itemRect = dataOrErrorItem.GetSourceRect(0, item.ParentSheetIndex);

            float interval = counter * 100f;

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

            foreach(string fish in fishes)
            {

                if(Mod.instance.randomIndex.Next(2) != 0)
                {

                    continue;

                }

                int experienceGain = 12;

                StardewValley.Object candidate = new(fish, 1);

                if(candidate.Category == StardewValley.Object.FishCategory)
                {

                    if (Game1.player.FishingLevel >= 9)
                    {

                        candidate.Quality = 4;

                        experienceGain = 48;

                    }
                    else if (Mod.instance.randomIndex.Next(11 - Game1.player.FishingLevel) <= 0)
                    {

                        candidate.Quality = 2;

                        experienceGain = 36;

                    }

                    experienceGain = 24;

                }

                Vector2 offset = tile * 64 + new Vector2(-64 + Mod.instance.randomIndex.Next(10) * 16, -64 + Mod.instance.randomIndex.Next(10) * 16);

                ThrowHandle throwObject = new(Game1.player, offset, candidate);

                throwObject.delay = Mod.instance.randomIndex.Next(5) * 10;

                throwObject.register();

                Game1.player.gainExperience(1, experienceGain); // gain fishing experience

                Game1.player.checkForQuestComplete(null, -1, 1, null, fish, 7);

            }

            Game1.player.currentLocation.playSound("pullItemFromWater");
            Game1.player.currentLocation.playSound("pullItemFromWater");
            Game1.player.currentLocation.playSound("pullItemFromWater");

        }

    }

}
