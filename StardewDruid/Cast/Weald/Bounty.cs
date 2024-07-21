using StardewValley;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley.TerrainFeatures;
using StardewDruid.Data;

using StardewDruid.Cast.Effect;
using StardewModdingAPI;
using static StardewValley.Minigames.TargetGame;
using StardewValley.Locations;

namespace StardewDruid.Cast.Weald
{
    public class Bounty
    {

        public enum bounties
        {
            bush,
            tree,
            fruit,
            grass,
            flyby,
        }

        public GameLocation location;

        public Dictionary<Vector2, TerrainFeature> terrainFeatures = new();

        public Dictionary<Vector2, bounties> creatureProspects = new();

        public Bounty() { }

        public void CastActivate(GameLocation Location, Vector2 Target)
        {

            location = Location;

            if (!Mod.instance.rite.specialCasts.ContainsKey(location.Name))
            {

                Mod.instance.rite.specialCasts[location.Name] = new();

            }

            Dictionary<Vector2, int> targets = new();

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {

                    targets.Add(Target + new Vector2(i, j), 0);

                }

            }

            // ---------------------------------------------
            // Large Feature iteration
            // ---------------------------------------------

            if (location.largeTerrainFeatures.Count > 0)
            {

                foreach (LargeTerrainFeature largeTerrainFeature in location.largeTerrainFeatures)
                {

                    if (largeTerrainFeature is not StardewValley.TerrainFeatures.Bush bushFeature)
                    {

                        continue;

                    }

                    Vector2 bushTile = bushFeature.Tile;

                    if (targets.ContainsKey(bushTile) && !terrainFeatures.ContainsKey(bushTile))
                    {

                        creatureProspects[bushTile] = bounties.bush;

                        terrainFeatures[bushTile] = bushFeature;

                        bushFeature.performToolAction(null, 1, bushTile);

                    }

                }

            }

            foreach (KeyValuePair<Vector2, int> check in targets)
            {

                if (location.terrainFeatures.ContainsKey(check.Key))
                {

                    TerrainFeature terrainFeature = location.terrainFeatures[check.Key];

                    if (terrainFeature is StardewValley.TerrainFeatures.FruitTree fruitFeature)
                    {

                        if (fruitFeature.growthStage.Value >= 4)
                        {

                            creatureProspects[check.Key] = bounties.fruit;

                            fruitFeature.performUseAction(check.Key);

                        }

                        fruitFeature.dayUpdate();

                        Mod.instance.iconData.CursorIndicator(location, (check.Key * 64), IconData.cursors.weald, new());

                    }
                    else if (terrainFeature is StardewValley.TerrainFeatures.Tree treeFeature)
                    {

                        if (treeFeature.growthStage.Value >= 5)
                        {
                            
                            creatureProspects[check.Key] = bounties.tree;

                            terrainFeatures[check.Key] = treeFeature;

                            if (!treeFeature.stump.Value)
                            {

                                treeFeature.performUseAction(check.Key);

                            }

                        }
                        else if (treeFeature.growthStage.Value < 4 && ModUtility.NeighbourCheck(Game1.player.currentLocation, check.Key).Count == 0)
                        {

                            treeFeature.growthStage.Value++;

                            Mod.instance.iconData.CursorIndicator(location, (check.Key * 64), IconData.cursors.weald, new());

                        }

                    }
                    else if (terrainFeature is StardewValley.TerrainFeatures.Grass grassFeature)
                    {

                        if(Mod.instance.randomIndex.Next(4) == 0)
                        {

                            Microsoft.Xna.Framework.Rectangle tileRectangle = new((int)(check.Key.X * 64) -5 + Mod.instance.randomIndex.Next(10), (int)(check.Key.Y * 64) - 5 + Mod.instance.randomIndex.Next(10), 62, 62);

                            grassFeature.doCollisionAction(tileRectangle, Mod.instance.randomIndex.Next(1, 4), check.Key, Game1.player);

                        }

                        grassFeature.dayUpdate();

                    }

                    continue;

                }

            }

            if(location is FarmCave)
            {

                if (!Mod.instance.rite.specialCasts[location.Name].Contains("FarmCaveBats"))
                {
                    creatureProspects.Clear();
                    
                    creatureProspects[new Vector2(1,1)] = bounties.flyby;

                    Mod.instance.rite.specialCasts[location.Name].Add("FarmCaveBats");
                
                }

            }

            if (location is Woods)
            {

                if (!Mod.instance.rite.specialCasts[location.Name].Contains("WoodsBats"))
                {

                    if (Vector2.Distance(Game1.player.Position, new Vector2(640, 720)) <= 320)
                    {

                        creatureProspects.Clear();

                        creatureProspects[new Vector2(1, 1)] = bounties.flyby;

                        Mod.instance.rite.specialCasts[location.Name].Add("WoodsBats");

                    }

                }

            }

            SpawnCreature();

        }

        public void SpawnCreature()
        {
            
            if (creatureProspects.Count == 0)
            {
                return;
            }

            int index = Mod.instance.randomIndex.Next(creatureProspects.Count);

            KeyValuePair<Vector2, bounties> prospect = creatureProspects.ElementAt(index);

            Creature creature;

            string id = "creature" + location.Name;

            if (!Mod.instance.eventRegister.ContainsKey(id))
            {

                creature = new();

                creature.eventId = id;

                creature.EventActivate();

            }
            else
            {

                creature = Mod.instance.eventRegister[id] as Creature;

            }

            int direction = Mod.instance.randomIndex.Next(8);

            Vector2 target;

            switch (prospect.Value)
            {

                case bounties.bush:

                    target = Vector2.Zero;

                    foreach(KeyValuePair<Vector2, bounties> possible in creatureProspects)
                    {

                        if(possible.Key != prospect.Key && Vector2.Distance(prospect.Key,possible.Key) >= 6)
                        {

                            if (ModUtility.PathsToTraversal(location, ModUtility.GetTilesBetweenPositions(location,possible.Key*64,prospect.Key*64), new(), 0).Count > 0)
                            {

                                target = possible.Key;

                                break;

                            }

                        }

                    }

                    if(target != Vector2.Zero)
                    {

                        if (Mod.instance.randomIndex.Next(2) == 0)
                        {

                            creature.AddCreature(location, Character.CharacterHandle.characters.Shadowfox, prospect.Key * 64 - new Vector2(0, 64), target * 64 - new Vector2(0, 64), 3f);

                        }
                        else
                        {

                            creature.AddCreature(location, Character.CharacterHandle.characters.Shadowcat, prospect.Key * 64 - new Vector2(0, 64), target * 64 - new Vector2(0,64), 3f);

                        }

                    }

                    break;

                case bounties.tree:

                    Vector2 treeTop = prospect.Key * 64 - new Vector2(0, 160);

                    target = treeTop + (ModUtility.DirectionAsVector(direction) * 6400);

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        creature.AddCreature(location, Character.CharacterHandle.characters.Shadowbat, treeTop, target, 3f);

                        break;

                    }

                    creature.AddCreature(location, Character.CharacterHandle.characters.Crow, treeTop, target, 3f);

                    break;

                case bounties.flyby:

                    creature.AddCreature(location, Character.CharacterHandle.characters.Shadowbat, new Vector2(3, 0) *64, new Vector2(4, 31) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.Shadowbat, new Vector2(4, 1) * 64, new Vector2(5, 31) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.Shadowbat, new Vector2(6, 0) * 64, new Vector2(7, 30) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.Shadowbat, new Vector2(7, 1) * 64, new Vector2(9, 31) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.Shadowbat, new Vector2(8, 1) * 64, new Vector2(10, 30) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.Shadowbat, new Vector2(10, 0) * 64, new Vector2(12, 30) * 64, 3f);

                    break;

                case bounties.fruit:

                    Vector2 fruitTop = prospect.Key * 64 - new Vector2(0, 160);

                    target = fruitTop + (ModUtility.DirectionAsVector(direction) * 6400);

                    creature.AddCreature(location, Character.CharacterHandle.characters.Shadowbat, fruitTop, target, 3f);

                    break;

            }

        }

    }

}
