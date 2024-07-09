using Microsoft.Xna.Framework;
using StardewValley.BellsAndWhistles;
using StardewValley.Locations;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewDruid.Data;
using StardewDruid.Journal;
using System.Linq;
using StardewValley.TerrainFeatures;
using Netcode;
using StardewDruid.Event;
using StardewDruid.Monster;
using StardewValley.Minigames;
using StardewDruid.Location;
using xTile.Tiles;
using xTile.Layers;
using StardewDruid.Character;
using System.Threading;
using StardewValley.Objects;
using StardewValley.Monsters;
using System.Xml.Linq;

namespace StardewDruid.Cast.Mists
{
    public class Artifice
    {

        public Artifice()
        {


        }

        public void CastActivate(Vector2 castVector)
        {

            GameLocation location = Game1.player.currentLocation;

            string locationName = location.Name;

            if (!Mod.instance.rite.specialCasts.ContainsKey(locationName))
            {

                Mod.instance.rite.specialCasts[locationName] = new();

            }

            List<Vector2> tileVectors;

            Dictionary<Vector2, bool> seenVectors = new();

            int casts = 0;

            bool zaprod = false;

            bool zappot = false;

            Vector2 warpVector = WarpData.WarpTiles(location);

            Vector2 fireVector = WarpData.FireVectors(location);

            for (int i = -1; i < 4; i++)
            {

                if(i == -1)
                {

                    tileVectors = ModUtility.GetTilesBetweenPositions(location, Game1.player.Position, castVector * 64);

                }
                else
                {
                    tileVectors = ModUtility.GetTilesWithinRadius(location, castVector, i);


                }

                foreach (Vector2 tileVector in tileVectors)
                {

                    if (seenVectors.ContainsKey(tileVector))
                    {

                        continue;

                    }

                    seenVectors[tileVector] = true;

                    if (warpVector != Vector2.Zero && !Mod.instance.rite.specialCasts[locationName].Contains("warp"))
                    {

                        if (tileVector == warpVector)
                        {

                            string targetIndex = WarpData.WarpTotems(location);

                            StardewValley.Object warpTotem = new(targetIndex, Mod.instance.randomIndex.Next(1, 3));

                            ThrowHandle throwObject = new(Game1.player, warpVector * 64, warpTotem);

                            throwObject.register();

                            Vector2 boltVector = new(warpVector.X, warpVector.Y - 2);

                            Mod.instance.spellRegister.Add(new(boltVector * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

                            Mod.instance.rite.specialCasts[locationName].Add("warp");

                            casts++;

                        }

                    }

                    if (fireVector != Vector2.Zero && !Mod.instance.rite.specialCasts[locationName].Contains("fire"))
                    {

                        if (tileVector == fireVector)
                        {

                            Torch campFire = new("278", true)
                            {
                                Fragility = 1,
                                destroyOvernight = true
                            };

                            if (location.objects.ContainsKey(fireVector))
                            {

                                location.objects.Remove(fireVector);

                            }

                            location.objects.Add(fireVector, campFire);

                            Game1.playSound("fireball");

                            Mod.instance.spellRegister.Add(new(fireVector * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

                            Mod.instance.rite.specialCasts[locationName].Add("fire");

                            casts++;


                        }

                    }

                    if (location.objects.Count() > 0)
                    {

                        if (location.objects.ContainsKey(tileVector))
                        {

                            StardewValley.Object targetObject = location.objects[tileVector];

                            if (location.IsFarm && targetObject.QualifiedItemId == "(BC)9")
                            {

                                if (targetObject.heldObject.Value != null)
                                {

                                    continue;

                                }

                                for (int j = 0; j <= Mod.instance.PowerLevel; j++)
                                {

                                    if (Mod.instance.rite.specialCasts[locationName].Contains("rod" + j.ToString()))
                                    {
                                        
                                        continue;

                                    }

                                    targetObject.heldObject.Value = new StardewValley.Object("787", 1);

                                    targetObject.MinutesUntilReady = Utility.CalculateMinutesUntilMorning(Game1.timeOfDay);

                                    targetObject.shakeTimer = 1000;

                                    if (!zaprod)
                                    {
                                        
                                        Mod.instance.spellRegister.Add(new(tileVector * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });
                                        
                                        zaprod = true;

                                    }

                                    Mod.instance.rite.specialCasts[locationName].Add("rod" + j.ToString());

                                    casts++;

                                    break;

                                }

                            }
                            else if (targetObject.Name.Contains("Campfire"))
                            {

                                string fireLocation = location.Name;

                                if (!Mod.instance.rite.specialCasts[locationName].Contains("campfire"))
                                {

                                    location.objects.Remove(tileVector);

                                    Torch campFire = new("278", true)
                                    {
                                        Fragility = 1,
                                        destroyOvernight = true
                                    };

                                    location.objects.Add(tileVector, campFire);

                                    Game1.playSound("fireball");

                                    Mod.instance.spellRegister.Add(new(tileVector * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

                                    Mod.instance.rite.specialCasts[locationName].Add("campfire");

                                    casts++;

                                }
                            }
                            else if (targetObject.IsScarecrow())
                            {

                                string scid = "scarecrow_" + tileVector.X.ToString() + "_" + tileVector.Y.ToString();

                                if (!Game1.isRaining && !Mod.instance.rite.specialCasts[locationName].Contains(scid))
                                {

                                    ArtificeScarecrow(location,tileVector);

                                    int tryCost = 32 - Game1.player.FarmingLevel * 3;

                                    Mod.instance.rite.castCost += tryCost < 8 ? 8 : tryCost;

                                    Mod.instance.rite.specialCasts[locationName].Add(scid);

                                    casts++;

                                }

                            }
                            else if(targetObject.QualifiedItemId == "(BC)MushroomLog")
                            {

                                for (int j = 0; j <= Mod.instance.PowerLevel; j++)
                                {

                                    if (Mod.instance.rite.specialCasts[locationName].Contains("mushroomlog" + j.ToString()))
                                    {

                                        continue;

                                    }

                                    if (targetObject.MinutesUntilReady == 0)
                                    {
                                        
                                        targetObject.DayUpdate();
    
                                    }

                                    targetObject.shakeTimer = 1000;

                                    targetObject.MinutesUntilReady = 1;

                                    Mod.instance.spellRegister.Add(new(tileVector * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

                                    Mod.instance.rite.specialCasts[locationName].Add("mushroomlog" + j.ToString());

                                    casts++;

                                    break;

                                }

                            }
                            else if(targetObject is CrabPot Crabpot)
                            {

                                if (Crabpot.heldObject.Value != null)
                                {

                                    int numberCaught = 1;

                                    if (Game1.player.stats.Get("Book_Crabbing") != 0)
                                    {

                                        numberCaught = 2;
                                    
                                    }

                                    StardewValley.Object harvest = new StardewValley.Object(Crabpot.heldObject.Value.QualifiedItemId.Replace("(O)",""), numberCaught);

                                    ThrowHandle throwHarvest = new(Game1.player, tileVector * 64, harvest);

                                    throwHarvest.register();

                                    Crabpot.heldObject.Value = null;
                                    Crabpot.bait.Value = null;
                                    Crabpot.readyForHarvest.Set(false);
                                    Crabpot.tileIndexToShow = 710;
                                    Crabpot.lidFlapping = true;
                                    Crabpot.lidFlapTimer = 60f;
                                    location.playSound("fishingRodBend");
                                    DelayedAction.playSoundAfterDelay("coin", 500);
                                    Crabpot.shake = Vector2.Zero;
                                    Crabpot.shakeTimer = 0f;

                                    if (DataLoader.Fish(Game1.content).TryGetValue(harvest.ItemId, out var value2))
                                    {
                                        string[] array = value2.Split('/');

                                        int minValue = ((array.Length <= 5) ? 1 : Convert.ToInt32(array[5]));

                                        int num = ((array.Length > 5) ? Convert.ToInt32(array[6]) : 10);

                                        Game1.player.caughtFish(harvest.QualifiedItemId, Game1.random.Next(minValue, num + 1), from_fish_pond: false, numberCaught);
                                    }

                                    Game1.player.gainExperience(1, 5);

                                }

                                if(Crabpot.bait.Value == null)
                                {

                                    if (!zappot)
                                    {
                                        Mod.instance.spellRegister.Add(new(tileVector * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });
                                        zappot = true;

                                    }

                                    Crabpot.bait.Value = new StardewValley.Object("774", i);

                                    location.playSound("Ship");

                                    Crabpot.lidFlapping = true;

                                    Crabpot.lidFlapTimer = 60f;

                                    casts++;

                                }

                            }

                            continue;

                        }

                    }

                }

            }

            if(casts > 0)
            {

                if (!Mod.instance.questHandle.IsComplete(QuestHandle.mistsTwo))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.mistsTwo, casts);

                }

            }

        }

        public void ArtificeScarecrow(GameLocation location, Vector2 targetVector)
        {

            bool animate = true;

            if (Game1.player.currentLocation.Name != location.Name && !Utility.isOnScreen(targetVector * 64, 0))
            {

                animate = false;

            }

            int radius = ((int)(Mod.instance.PowerLevel) + 3);

            radius = Math.Min(8, radius);

            for (int i = 1; i < radius; i++)
            {

                List<Vector2> hoeVectors = ModUtility.GetTilesWithinRadius(Game1.player.currentLocation, targetVector, i);

                foreach (Vector2 hoeVector in hoeVectors)
                {

                    if (Game1.player.currentLocation.terrainFeatures.ContainsKey(hoeVector))
                    {

                        var terrainFeature = Game1.player.currentLocation.terrainFeatures[hoeVector];

                        if (terrainFeature is HoeDirt)
                        {

                            HoeDirt hoeDirt = terrainFeature as HoeDirt;

                            if (hoeDirt.state.Value == 0)
                            {

                                hoeDirt.state.Value = 1;

                                if (animate)
                                {

                                    TemporaryAnimatedSprite newAnimation = new(
                                        "TileSheets\\animations",
                                        new(0, 51 * 64, 64, 64), 
                                        75f, 
                                        8, 
                                        1,
                                        new(hoeVector.X * 64 + 10, hoeVector.Y * 64 + 10), 
                                        false, 
                                        false,
                                        hoeVector.X * 1000 + hoeVector.Y, 
                                        0f,
                                        new(0.8f, 0.8f, 1f, 1f), 
                                        0.7f, 
                                        0f, 
                                        0f, 
                                        0f)
                                    {

                                        delayBeforeAnimationStart = (i * 200) + 200,

                                    };

                                    Game1.currentLocation.temporarySprites.Add(newAnimation);


                                }

                            }

                        }

                    }

                    continue;

                }

            }

            if (animate)
            {

                Mod.instance.spellRegister.Add(new(targetVector * 64 - new Vector2(0, 32), 128, IconData.impacts.boltswirl, new()) { type = SpellHandle.spells.bolt });

            }

            return;

        }

    }

}
