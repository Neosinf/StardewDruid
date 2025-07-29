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

using StardewValley.Locations;
using StardewDruid.Location;
using System.ComponentModel.Design;
using StardewValley.BellsAndWhistles;
using StardewDruid.Location.Druid;
using StardewDruid.Location.Terrain;
using StardewValley.Objects;
using StardewDruid.Handle;

namespace StardewDruid.Cast.Weald
{
    public class Bounty
    {

        public enum bounties
        {
            bush,
            tree,
            fruit,
            water,
            flyby,
            owlbox,
            orchardtree,
            tunnel,
            beach,
            alchemistry
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

            Dictionary<Vector2, int> waterTargets = new();

            int waterCheck = 33 + Mod.instance.randomIndex.Next(15);

            int water = waterCheck;

            for (int i = 0; i < 16; i++)
            {

                for (int j = 0; j < 16; j++)
                {

                    Vector2 newTarget = Target + new Vector2(i, j);

                    targets.Add(newTarget, 0);

                    water--;

                    if(water < 0)
                    {

                        water = waterCheck;

                        waterTargets.Add(newTarget, 0);

                    }

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

            // ---------------------------------------------
            // Small Feature iteration
            // ---------------------------------------------

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
                        else
                        {

                            fruitFeature.dayUpdate();

                        }

                        Mod.instance.iconData.CursorIndicator(location, (check.Key * 64) + new Vector2(32), IconData.cursors.weald, new());

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

                            Mod.instance.iconData.CursorIndicator(location, (check.Key * 64) + new Vector2(32), IconData.cursors.weald, new());

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

            if (location is DruidLocation druidlocation)
            {

                foreach (TerrainField terrainField in druidlocation.terrainFields)
                {

                    if (terrainField.ruined || terrainField.disabled || terrainField.shaked)
                    {

                        continue;

                    }

                    if (!targets.ContainsKey(ModUtility.PositionToTile(terrainField.position)))
                    {

                        continue;

                    }

                    if (terrainField is Magnolia magnolia || terrainField is DarkOak darkoak || terrainField is Holly holly || terrainField is Hawthorn hawthorn)
                    {

                        terrainField.shake = 40;

                        terrainField.shaked = true;

                        if(druidlocation is Clearing && Game1.currentSeason != "winter")
                        {

                            creatureProspects[terrainField.bounds.Center.ToVector2() - new Vector2(0, 128)] = bounties.orchardtree;

                        }
                        else
                        {

                            creatureProspects[ModUtility.PositionToTile(terrainField.bounds.Center.ToVector2())] = bounties.tree;

                        }

                    }

                    if (terrainField is Owlbox owlbox)
                    {

                        terrainField.shaked = true;

                        creatureProspects[terrainField.position + new Vector2(96, 96)] = bounties.owlbox;

                    }

                }

            }

            // ---------------------------------------------
            // Water iteration
            // ---------------------------------------------

            int waterLimit = 0;

            foreach (KeyValuePair<Vector2, int> check in waterTargets)
            {
                
                if(ModUtility.GroundCheck(Location, check.Key) != "water")
                {

                    continue;

                }

                if(ModUtility.WaterCheck(Location, check.Key, 2))
                {

                    creatureProspects[check.Key] = bounties.water;

                    waterLimit++;

                    if(waterLimit >= 2)
                    {

                        break;

                    }

                }
            
            }

            if (location is FarmCave)
            {

                if (!Mod.instance.rite.specialCasts[location.Name].Contains("FarmCaveBats"))
                {
                    creatureProspects.Clear();
                    
                    creatureProspects[new Vector2(1,1)] = bounties.flyby;

                    Mod.instance.rite.specialCasts[location.Name].Add("FarmCaveBats");
                
                }

            }

            if (location is Woods || location.Name.Contains("Custom_Woods"))
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

            if (location.Name.Equals("Tunnel"))
            {

                if (!Mod.instance.rite.specialCasts[location.Name].Contains("TunnelBats"))
                {

                    creatureProspects.Clear();

                    creatureProspects[new Vector2(1, 1)] = bounties.tunnel;

                    Mod.instance.rite.specialCasts[location.Name].Add("TunnelBats");

                }

            }

            if (location is Forest)
            {

                if (
                    Mod.instance.Helper.ModRegistry.IsLoaded("Morghoula.AlchemistryCP") 
                    && !RelicHandle.HasRelic(IconData.relics.runestones_alchemistry) 
                    && !Mod.instance.eventRegister.ContainsKey("RelicAlchemistry")
                )
                {

                    creatureProspects.Clear();

                    creatureProspects[new Vector2(1, 1)] = bounties.alchemistry;

                }

            }

            if (location is Beach)
            {

                if (!Mod.instance.rite.specialCasts[location.Name].Contains("BeachGulls"))
                {

                    creatureProspects.Clear();

                    creatureProspects[new Vector2(1, 1)] = bounties.beach;

                    Mod.instance.rite.specialCasts[location.Name].Add("BeachGulls");

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

            string id ="creature" + location.Name;

            if (!Mod.instance.eventRegister.ContainsKey(id))
            {

                creature = new()
                {
                    eventId = id
                };

                creature.EventActivate();

            }
            else
            {

                creature = Mod.instance.eventRegister[id] as Creature;

            }

            int direction = Mod.instance.randomIndex.Next(8);

            Vector2 target;

            IconData.relics dropRelic;

            ThrowHandle throwRelic;

            switch (prospect.Value)
            {

                case bounties.bush:

                    target = Vector2.Zero;

                    foreach (KeyValuePair<Vector2, bounties> possible in creatureProspects)
                    {

                        if (possible.Value != bounties.bush && possible.Value != bounties.tree)
                        {

                            continue;

                        }

                        if (possible.Key != prospect.Key && Vector2.Distance(prospect.Key, possible.Key) >= 6)
                        {

                            if (ModUtility.PathsToTraversal(location, ModUtility.GetTilesBetweenPositions(location, possible.Key * 64, prospect.Key * 64), new(), 0).Count > 0)
                            {

                                target = possible.Key;

                                break;

                            }

                        }

                    }

                    if (target != Vector2.Zero)
                    {

                        if (location is Town || location is Mountain || location is Beach || location is Farm)
                        {

                            List<CharacterHandle.characters> creatureTypes = new()
                            {
                                CharacterHandle.characters.RedFox,
                                CharacterHandle.characters.YellowFox,
                                CharacterHandle.characters.BlackCat,
                                CharacterHandle.characters.GingerCat,
                                CharacterHandle.characters.TabbyCat,
                            };

                            CharacterHandle.characters creatureSelect = creatureTypes[Mod.instance.randomIndex.Next(creatureTypes.Count)];

                            switch (creatureSelect)
                            {
                                case CharacterHandle.characters.RedFox:
                                case CharacterHandle.characters.YellowFox:

                                    creature.AddCreature(location, creatureSelect, prospect.Key * 64 - new Vector2(0, 64), target * 64 - new Vector2(0, 64), 3.5f);

                                    break;
                                case CharacterHandle.characters.BlackCat:
                                case CharacterHandle.characters.GingerCat:
                                case CharacterHandle.characters.TabbyCat:

                                    creature.AddCreature(location, creatureSelect, prospect.Key * 64 - new Vector2(0, 64), target * 64 - new Vector2(0, 64), 3.5f);

                                    break;

                            }

                        }
                        else
                        {

                            List<CharacterHandle.characters> creatureTypes = new()
                            {
                                CharacterHandle.characters.BrownWolf,
                                CharacterHandle.characters.BlackWolf,
                                CharacterHandle.characters.BrownBear,
                                CharacterHandle.characters.BlackBear,
                            };


                            CharacterHandle.characters creatureSelect = creatureTypes[Mod.instance.randomIndex.Next(creatureTypes.Count)];

                            switch (creatureSelect)
                            {
                                case CharacterHandle.characters.BrownWolf:
                                case CharacterHandle.characters.BlackWolf:

                                    creature.AddCreature(location, creatureSelect, prospect.Key * 64 - new Vector2(0, 64), target * 64 - new Vector2(0, 64), 3f + Mod.instance.randomIndex.Next(2) * 0.5f);


                                    break;
                                case CharacterHandle.characters.BrownBear:
                                case CharacterHandle.characters.BlackBear:
                                    creature.AddCreature(location, creatureSelect, prospect.Key * 64 - new Vector2(0, 64), target * 64 - new Vector2(0, 64), 3f + Mod.instance.randomIndex.Next(3) * 0.5f);


                                    break;

                            }

                        }


                    }

                    break;

                case bounties.tree:

                    Vector2 treeTop = prospect.Key * 64 - new Vector2(0, 160);

                    target = treeTop + (ModUtility.DirectionAsVector(direction) * 6400);

                    bool batRelic = false;

                    dropRelic = Mod.instance.relicHandle.RelicTacticalLocations();

                    if (dropRelic != IconData.relics.none)
                    {

                        if (!RelicHandle.HasRelic(dropRelic))
                        {

                            batRelic = true;

                        }

                    }

                    List<CharacterHandle.characters> flyers = new()
                    {
                        CharacterHandle.characters.Bat,
                        CharacterHandle.characters.BrownBat,
                    };

                    if (batRelic)
                    {

                        flyers = new()
                        {
                            CharacterHandle.characters.Bat,
                            CharacterHandle.characters.BrownBat,
                            CharacterHandle.characters.Bat,
                            CharacterHandle.characters.BrownBat,

                        };

                    }

                    if (Game1.timeOfDay > 1900 || Game1.timeOfDay < 800)
                    {

                        flyers.AddRange(new List<CharacterHandle.characters>()
                        {

                            CharacterHandle.characters.BrownOwl,
                            CharacterHandle.characters.GreyOwl,

                        });

                    }
                    else
                    {

                        flyers.AddRange(new List<CharacterHandle.characters>()
                        {
                            CharacterHandle.characters.CorvidRaven,
                            CharacterHandle.characters.CorvidRook,
                            CharacterHandle.characters.CorvidCrow,
                            CharacterHandle.characters.CorvidMagpie,

                        });


                    }

                    CharacterHandle.characters treeCharacter = flyers[Mod.instance.randomIndex.Next(flyers.Count)];

                    if ((treeCharacter is CharacterHandle.characters.Bat || treeCharacter is CharacterHandle.characters.BrownBat) && batRelic)
                    {

                        throwRelic = new(Game1.player, treeTop, dropRelic);

                        throwRelic.register();

                    }

                    creature.AddCreature(location, treeCharacter, treeTop, target, 3f);

                    break;

                case bounties.fruit:

                    Vector2 fruitTop = prospect.Key * 64 - new Vector2(0, 160);

                    target = fruitTop + (ModUtility.DirectionAsVector(direction) * 6400);

                    switch (Mod.instance.randomIndex.Next(2))
                    {
                        default:
                            creature.AddCreature(location, CharacterHandle.characters.Bat, fruitTop, target, 4f);
                            break;

                        case 1:
                            creature.AddCreature(location, CharacterHandle.characters.BrownBat, fruitTop, target, 4f);
                            break;

                    }

                    dropRelic = Mod.instance.relicHandle.RelicTacticalLocations();

                    if (dropRelic != IconData.relics.none)
                    {

                        if (!RelicHandle.HasRelic(dropRelic))
                        {

                            throwRelic = new(Game1.player, fruitTop, dropRelic);

                            throwRelic.register();

                        }

                    }

                    break;

                case bounties.water:

                    Vector2 serpentStart = prospect.Key * 64;

                    target = serpentStart + (ModUtility.DirectionAsVector(direction) * 6400);

                    int forest = SpawnData.ForestWaterCheck(location);

                    if (location is Caldera || location.Name == "UndergroundMine100")
                    {

                        creature.AddCreature(location, CharacterHandle.characters.LavaSerpent, serpentStart, target, 2.5f + Mod.instance.randomIndex.Next(2) * 0.5f);

                        break;

                    }
                    else if (location is Beach || location.Name.Contains("Beach") || location is IslandLocation || location is Atoll || forest == 3)
                    {    
                        if (Mod.instance.randomIndex.Next(3) == 0)
                        {

                            creature.AddCreature(location, CharacterHandle.characters.NightSerpent, serpentStart, target, 2.5f + Mod.instance.randomIndex.Next(2) * 0.5f);

                            break;

                        }

                        creature.AddCreature(location, CharacterHandle.characters.Serpent, serpentStart, target, 2.5f + Mod.instance.randomIndex.Next(2) * 0.5f);

                        break;
                    }

                    if (Mod.instance.randomIndex.Next(3) == 0)
                    {

                        creature.AddCreature(location, CharacterHandle.characters.NightSerpent, serpentStart, target, 2.5f + Mod.instance.randomIndex.Next(2) * 0.5f);

                        break;

                    }

                    creature.AddCreature(location, CharacterHandle.characters.RiverSerpent, serpentStart, target, 2.5f + Mod.instance.randomIndex.Next(2) * 0.5f);

                    break;

                case bounties.flyby:

                    creature.AddCreature(location, CharacterHandle.characters.Bat, new Vector2(3, 0) *64, new Vector2(4, 31) * 64, 4f);
                            
                    creature.AddCreature(location, CharacterHandle.characters.BrownBat, new Vector2(4, 1) * 64, new Vector2(5, 31) * 64, 3.5f, 2);

                    creature.AddCreature(location, CharacterHandle.characters.Bat, new Vector2(6, 0) * 64, new Vector2(7, 30) * 64, 4f);

                    creature.AddCreature(location, CharacterHandle.characters.Bat, new Vector2(7, 1) * 64, new Vector2(9, 31) * 64, 3.75f);

                    creature.AddCreature(location, CharacterHandle.characters.Bat, new Vector2(8, 1) * 64, new Vector2(10, 30) * 64, 3.5f, 2);

                    creature.AddCreature(location, CharacterHandle.characters.BrownBat, new Vector2(10, 0) * 64, new Vector2(12, 30) * 64, 4f, 2);

                    dropRelic = Mod.instance.relicHandle.RelicTacticalLocations();

                    if (dropRelic != IconData.relics.none)
                    {

                        if (!RelicHandle.HasRelic(dropRelic))
                        {

                            throwRelic = new(Game1.player, new Vector2(6, 0) * 64, dropRelic);

                            throwRelic.register();

                        }

                    }

                    break;

                case bounties.owlbox:

                    Vector2 owlexit = new Vector2(27, -8) * 64;

                    List<CharacterHandle.characters> owls = new()
                    {

                        CharacterHandle.characters.BrownOwl,
                        CharacterHandle.characters.GreyOwl,

                    };

                    Vector2 roost = prospect.Key + new Vector2(96,96);

                    CharacterHandle.characters owl = owls[Mod.instance.randomIndex.Next(owls.Count)];

                    for (int i = 0; i < Mod.instance.randomIndex.Next(1,3); i++)
                    {

                        Vector2 exitAt = owlexit + new Vector2(Mod.instance.randomIndex.Next(5) * 96, Mod.instance.randomIndex.Next(5) * 48);

                        creature.AddCreature(location, owl, roost + new Vector2(0,i), exitAt, 2.5f + (0.25f * Mod.instance.randomIndex.Next(3)));

                    }

                    /*if (!RelicData.HasRelic(IconData.relics.restore_cloth))
                    {

                        throwRelic = new(Game1.player, roost, IconData.relics.restore_cloth);

                        throwRelic.register();

                    }*/

                    break;

                case bounties.orchardtree:

                    Vector2 orchardTree = prospect.Key;

                    Vector2 treeexit = new Vector2(27, -8) * 64;

                    List<CharacterHandle.characters> corvids = new()
                    {

                        CharacterHandle.characters.CorvidRaven,
                        CharacterHandle.characters.CorvidRook,
                        CharacterHandle.characters.CorvidCrow,
                        CharacterHandle.characters.CorvidMagpie,

                    };

                    for (int i = 0; i < 2 + Mod.instance.randomIndex.Next(2); i++)
                    {

                        Vector2 startAt = orchardTree + new Vector2(Mod.instance.randomIndex.Next(5) * 96, Mod.instance.randomIndex.Next(5) * 48);

                        creature.AddCreature(location, corvids[Mod.instance.randomIndex.Next(corvids.Count)], startAt, treeexit, 2.5f + (0.25f * Mod.instance.randomIndex.Next(6)));

                    }

                    location.playSound(SpellHandle.Sounds.leafrustle.ToString());

                    break;

                case bounties.tunnel:

                    creature.AddCreature(location, CharacterHandle.characters.Bat,  new Vector2(40, 6) * 64, new Vector2(0, 6) * 64, 4f, 2);

                    creature.AddCreature(location, CharacterHandle.characters.BrownBat, new Vector2(41, 7) * 64, new Vector2(1, 7) * 64,  3.5f);

                    creature.AddCreature(location, CharacterHandle.characters.Bat,  new Vector2(39, 8) * 64, new Vector2(2, 8) * 64, 4f, 2);

                    creature.AddCreature(location, CharacterHandle.characters.BrownBat, new Vector2(40, 9) * 64, new Vector2(1, 9) * 64, 3.75f);

                    creature.AddCreature(location, CharacterHandle.characters.Bat,  new Vector2(41, 10) * 64, new Vector2(0, 10) * 64, 3.5f, 2);

                    creature.AddCreature(location, CharacterHandle.characters.Bat,  new Vector2(39, 11) * 64, new Vector2(1, 11) * 64, 4f);

                    dropRelic = Mod.instance.relicHandle.RelicTacticalLocations();

                    if (dropRelic != IconData.relics.none)
                    {

                        if (!RelicHandle.HasRelic(dropRelic))
                        {

                            throwRelic = new(Game1.player, new Vector2(39, 8) * 64, dropRelic);

                            throwRelic.register();

                        }

                    }

                    break;

                case bounties.beach:

                    creature.AddCreature(location, CharacterHandle.characters.SeaGull, new Vector2(location.map.Layers[0].LayerWidth + 12, 18) * 64, new Vector2(0, 2) * 64, 3f,1);

                    creature.AddCreature(location, CharacterHandle.characters.SeaGull, new Vector2(location.map.Layers[0].LayerWidth + 9, 18) * 64, new Vector2(0, 3) * 64, 3.75f, 1);

                    creature.AddCreature(location, CharacterHandle.characters.SeaGull, new Vector2(location.map.Layers[0].LayerWidth + 6, 18) * 64, new Vector2(0, 4) * 64, 3f, 1);

                    creature.AddCreature(location, CharacterHandle.characters.SeaGull, new Vector2(location.map.Layers[0].LayerWidth + 3, 18) * 64, new Vector2(0, 5) * 64, 3.25f,1);

                    creature.AddCreature(location, CharacterHandle.characters.SeaGull, new Vector2(location.map.Layers[0].LayerWidth, 18) * 64, new Vector2(0, 6) * 64, 3.5f, 1);

                    creature.AddCreature(location, CharacterHandle.characters.SeaGull, new Vector2(location.map.Layers[0].LayerWidth + 1, 20) * 64, new Vector2(0, 7) * 64, 3.75f, 1);

                    creature.AddCreature(location, CharacterHandle.characters.SeaGull, new Vector2(location.map.Layers[0].LayerWidth + 2, 22) * 64, new Vector2(0, 8) * 64, 3.25f, 1);

                    creature.AddCreature(location, CharacterHandle.characters.SeaGull, new Vector2(location.map.Layers[0].LayerWidth + 3, 24) * 64, new Vector2(0, 9) * 64, 3.5f, 1);

                    creature.AddCreature(location, CharacterHandle.characters.SeaGull, new Vector2(location.map.Layers[0].LayerWidth + 4, 26) * 64, new Vector2(0, 10) * 64, 3.25f, 1);

                    break;

                case bounties.alchemistry:


                    if (!Mod.instance.eventRegister.ContainsKey("RelicAlchemistry"))
                    {

                        Event.Relics.RelicAlchemistry alch = new()
                        {
                            eventId = "RelicAlchemistry"
                        };

                        alch.EventActivate();

                    }

                    break;

            }

        }

    }

}
