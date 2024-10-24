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
using StardewDruid.Location;
using System.ComponentModel.Design;
using StardewDruid.Character;
using StardewValley.BellsAndWhistles;
using StardewDruid.Journal;
using static StardewDruid.Data.IconData;

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
            orchard,
            tunnel,
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

            if (location is Clearing && Game1.currentSeason != "winter")
            {

                if (!Mod.instance.rite.specialCasts[location.Name].Contains("OrchardCrows"))
                {

                    creatureProspects.Clear();

                    creatureProspects[new Vector2(1, 1)] = bounties.orchard;

                    Mod.instance.rite.specialCasts[location.Name].Add("OrchardCrows");

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
                    && Game1.player.mailReceived.Contains("Morghoula.AlchemistryMFM_SevinaeIntroMail")
                    && !RelicData.HasRelic(IconData.relics.runestones_alchemistry) 
                    && !Mod.instance.eventRegister.ContainsKey("RelicAlchemistry")
                )
                {

                    creatureProspects.Clear();

                    creatureProspects[new Vector2(1, 1)] = bounties.alchemistry;

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

            IconData.relics dropRelic;

            ThrowHandle throwRelic;

            switch (prospect.Value)
            {

                case bounties.bush:

                    target = Vector2.Zero;

                    foreach (KeyValuePair<Vector2, bounties> possible in creatureProspects)
                    {

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

                        if(location is Town || location is Mountain || location is Beach || location is Farm)
                        {

                            List<Character.CharacterHandle.characters> creatureTypes = new()
                            {
                                Character.CharacterHandle.characters.RedFox,
                                Character.CharacterHandle.characters.YellowFox,
                                Character.CharacterHandle.characters.BlackCat,
                                Character.CharacterHandle.characters.GingerCat,
                                Character.CharacterHandle.characters.TabbyCat,
                            };

                            Character.CharacterHandle.characters creatureSelect = creatureTypes[Mod.instance.randomIndex.Next(creatureTypes.Count)];

                            switch (creatureSelect)
                            {
                                case Character.CharacterHandle.characters.RedFox:
                                case Character.CharacterHandle.characters.YellowFox:

                                    creature.AddCreature(location, creatureSelect, prospect.Key * 64 - new Vector2(0, 64), target * 64 - new Vector2(0, 64), 3.5f);

                                    break;
                                case Character.CharacterHandle.characters.BlackCat:
                                case Character.CharacterHandle.characters.GingerCat:
                                case Character.CharacterHandle.characters.TabbyCat:

                                    creature.AddCreature(location, creatureSelect, prospect.Key * 64 - new Vector2(0, 64), target * 64 - new Vector2(0, 64), 3f);

                                    break;

                            }

                        }
                        else
                        {

                            List<Character.CharacterHandle.characters> creatureTypes = new()
                            {
                                Character.CharacterHandle.characters.GreyWolf,
                                Character.CharacterHandle.characters.BlackWolf,
                                Character.CharacterHandle.characters.BrownBear,
                                Character.CharacterHandle.characters.BlackBear,
                            };


                            Character.CharacterHandle.characters creatureSelect = creatureTypes[Mod.instance.randomIndex.Next(creatureTypes.Count)];

                            switch (creatureSelect)
                            {
                                case Character.CharacterHandle.characters.GreyWolf:
                                case Character.CharacterHandle.characters.BlackWolf:

                                    creature.AddCreature(location, creatureSelect, prospect.Key * 64 - new Vector2(0, 64), target * 64 - new Vector2(0, 64), 2.5f + Mod.instance.randomIndex.Next(2)*0.5f);


                                    break;
                                case Character.CharacterHandle.characters.BrownBear:
                                case Character.CharacterHandle.characters.BlackBear:
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

                    dropRelic = Mod.instance.relicsData.RelicTacticalLocations();

                    if (dropRelic != IconData.relics.none)
                    {

                        if (!Journal.RelicData.HasRelic(dropRelic))
                        {

                            batRelic = true;

                        }

                    }

                    List<Character.CharacterHandle.characters> flyers = new()
                    {
                        Character.CharacterHandle.characters.ShadowBat,

                    };

                    if (batRelic)
                    {

                        flyers = new()
                        {
                            Character.CharacterHandle.characters.ShadowBat,
                            Character.CharacterHandle.characters.ShadowBat,
                            Character.CharacterHandle.characters.ShadowBat,
                            Character.CharacterHandle.characters.ShadowBat,

                        };

                    }

                    if(Game1.timeOfDay > 1900 || Game1.timeOfDay < 800)
                    {

                        flyers.AddRange(new List<Character.CharacterHandle.characters>()
                        {

                            Character.CharacterHandle.characters.BrownOwl,
                            Character.CharacterHandle.characters.GreyOwl,

                        });

                    }
                    else
                    {

                        flyers.AddRange(new List<Character.CharacterHandle.characters>()
                        {
                            Character.CharacterHandle.characters.ShadowRaven,
                            Character.CharacterHandle.characters.ShadowRook,
                            Character.CharacterHandle.characters.ShadowCrow,
                            Character.CharacterHandle.characters.ShadowMagpie,

                        });


                    }


                    CharacterHandle.characters treeCharacter = flyers[Mod.instance.randomIndex.Next(flyers.Count)];

                    if(treeCharacter is CharacterHandle.characters.ShadowBat && batRelic)
                    {

                        throwRelic = new(Game1.player, treeTop, dropRelic);

                        throwRelic.register();

                    }

                    creature.AddCreature(location, treeCharacter, treeTop, target, 3f);

                    break;

                case bounties.fruit:

                    Vector2 fruitTop = prospect.Key * 64 - new Vector2(0, 160);

                    target = fruitTop + (ModUtility.DirectionAsVector(direction) * 6400);

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat, fruitTop, target, 3f);

                    dropRelic = Mod.instance.relicsData.RelicTacticalLocations();

                    if (dropRelic != IconData.relics.none)
                    {

                        if (!Journal.RelicData.HasRelic(dropRelic))
                        {

                            throwRelic = new(Game1.player, fruitTop, dropRelic);

                            throwRelic.register();

                        }

                    }

                    break;

                case bounties.flyby:

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat, new Vector2(3, 0) *64, new Vector2(4, 31) * 64, 3f);
                            
                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat, new Vector2(4, 1) * 64, new Vector2(5, 31) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat, new Vector2(6, 0) * 64, new Vector2(7, 30) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat, new Vector2(7, 1) * 64, new Vector2(9, 31) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat, new Vector2(8, 1) * 64, new Vector2(10, 30) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat, new Vector2(10, 0) * 64, new Vector2(12, 30) * 64, 3f);

                    dropRelic = Mod.instance.relicsData.RelicTacticalLocations();

                    if (dropRelic != IconData.relics.none)
                    {

                        if (!Journal.RelicData.HasRelic(dropRelic))
                        {

                            throwRelic = new(Game1.player, new Vector2(6, 0) * 64, dropRelic);

                            throwRelic.register();

                        }

                    }

                    break;

                case bounties.orchard:

                    if(location is Clearing clearing)
                    {

                        Vector2 exit = new Vector2(27, -8) * 64;

                        if (Mod.instance.save.restoration[LocationData.druid_clearing_name] >= 3)
                        {

                            List<Character.CharacterHandle.characters> owls = new()
                            {

                                Character.CharacterHandle.characters.BrownOwl,
                                Character.CharacterHandle.characters.GreyOwl,

                            };

                            int owlCount = 0;

                            foreach (TerrainTile terrainTile in clearing.terrainTiles)
                            {

                                if (terrainTile.tilesheet == IconData.tilesheets.clearing)
                                {

                                    Vector2 roost = terrainTile.position + new Vector2(96,96);

                                    owlCount++;

                                    creature.AddCreature(location, owls[Mod.instance.randomIndex.Next(owls.Count)], exit+new Vector2(16*owlCount,0), roost, 2.5f + (0.25f * Mod.instance.randomIndex.Next(3)), true);

                                    if (!Journal.RelicData.HasRelic(IconData.relics.restore_cloth))
                                    {

                                        throwRelic = new(Game1.player, exit, IconData.relics.restore_cloth);

                                        throwRelic.register();

                                    }

                                }

                            }

                        }
                        else
                        {

                            List<Character.CharacterHandle.characters> corvids = new()
                            {

                                Character.CharacterHandle.characters.ShadowRaven,
                                Character.CharacterHandle.characters.ShadowRook,
                                Character.CharacterHandle.characters.ShadowCrow,
                                Character.CharacterHandle.characters.ShadowMagpie,

                            };

                            foreach (TerrainTile terrainTile in clearing.terrainTiles)
                            {

                                Vector2 orchardTree = terrainTile.position;

                                if (terrainTile.tilesheet == IconData.tilesheets.magnolia)
                                {

                                    if (terrainTile.index != 2)
                                    {

                                        continue;

                                    }

                                    if (Context.IsMainPlayer && Mod.instance.save.restoration[LocationData.druid_clearing_name] < 3)
                                    {

                                        continue;

                                    }

                                    orchardTree.X += 256;

                                    orchardTree.Y -= 256;

                                    if (!Journal.RelicData.HasRelic(IconData.relics.restore_cloth))
                                    {

                                        throwRelic = new(Game1.player, orchardTree, IconData.relics.restore_cloth);

                                        throwRelic.register();

                                    }

                                }
                                else if (terrainTile.tilesheet == IconData.tilesheets.outdoors)
                                {

                                    if (terrainTile.index != 11)
                                    {

                                        continue;

                                    }

                                }
                                else if (terrainTile.tilesheet == IconData.tilesheets.outdoorsTwo)
                                {

                                    if (terrainTile.index != 1)
                                    {

                                        continue;

                                    }

                                }
                                else
                                {

                                    continue;

                                }

                                for (int i = 0; i < 2 + Mod.instance.randomIndex.Next(1); i++)
                                {

                                    Vector2 startAt = orchardTree + new Vector2(Mod.instance.randomIndex.Next(5) * 96, Mod.instance.randomIndex.Next(5) * 48);

                                    creature.AddCreature(location, corvids[Mod.instance.randomIndex.Next(corvids.Count)], startAt, exit, 1.5f + (0.25f * Mod.instance.randomIndex.Next(6)));

                                }

                                location.playSound(SpellHandle.sounds.leafrustle.ToString());

                                terrainTile.shake = 16;

                            }

                        }

                    }

                    break;

                case bounties.tunnel:

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat,  new Vector2(40, 6) * 64, new Vector2(0, 6) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat, new Vector2(41, 7) * 64, new Vector2(1, 7) * 64,  2.75f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat,  new Vector2(39, 8) * 64, new Vector2(2, 8) * 64, 3f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat, new Vector2(40, 9) * 64, new Vector2(1, 9) * 64, 3.25f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat,  new Vector2(41, 10) * 64, new Vector2(0, 10) * 64, 2.5f);

                    creature.AddCreature(location, Character.CharacterHandle.characters.ShadowBat,  new Vector2(39, 11) * 64, new Vector2(1, 11) * 64, 3f);

                    dropRelic = Mod.instance.relicsData.RelicTacticalLocations();

                    if (dropRelic != IconData.relics.none)
                    {

                        if (!Journal.RelicData.HasRelic(dropRelic))
                        {

                            throwRelic = new(Game1.player, new Vector2(39, 8) * 64, dropRelic);

                            throwRelic.register();

                        }

                    }

                    break;

                case bounties.alchemistry:


                    if (!Mod.instance.eventRegister.ContainsKey("RelicAlchemistry"))
                    {

                        Event.Relics.RelicAlchemistry alch = new();

                        alch.eventId = "RelicAlchemistry";

                        alch.EventActivate();

                    }

                    break;

            }

        }

    }

}
