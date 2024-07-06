using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Extensions;
using StardewValley.GameData.FruitTrees;
using StardewValley.Internal;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using static StardewDruid.Cast.SpellHandle;

namespace StardewDruid.Character
{
    public class Shadowtin : StardewDruid.Character.Character
    {

        public WeaponRender weaponRender;

        public Dictionary<string,Dictionary<Vector2,int>> workVectors = new();

        public Shadowtin()
        {
        }

        public Shadowtin(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {
            
            characterType = CharacterHandle.characters.Shadowtin;

            weaponRender = new();

            weaponRender.LoadWeapon(WeaponRender.weapons.carnyx);

            base.LoadOut();

            idleFrames = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 288, 32, 32),
                    new Rectangle(224, 288, 32, 32),
                    new Rectangle(128, 288, 32, 32),
                    new Rectangle(160, 288, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(224, 288, 32, 32),
                    new Rectangle(128, 288, 32, 32),
                    new Rectangle(160, 288, 32, 32),
                    new Rectangle(192, 288, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(128, 288, 32, 32),
                    new Rectangle(160, 288, 32, 32),
                    new Rectangle(192, 288, 32, 32),
                    new Rectangle(224, 288, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(160, 288, 32, 32),
                    new Rectangle(192, 288, 32, 32),
                    new Rectangle(224, 288, 32, 32),
                    new Rectangle(128, 288, 32, 32),
                },
            };

            workFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new()
                {

                    new(128, 64, 32, 32),
                    new(160, 64, 32, 32),

                },
                [1] = new()
                {

                    new(128, 32, 32, 32),
                    new(160, 32, 32, 32),

                },
                [2] = new()
                {

                    new(128, 0, 32, 32),
                    new(160, 0, 32, 32),

                },
                [3] = new()
                {

                    new(128, 32, 32, 32),
                    new(160, 32, 32, 32),

                },

            };

            alertFrames = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 288, 32, 32),
                    new Rectangle(224, 288, 32, 32),
                    new Rectangle(128, 288, 32, 32),
                    new Rectangle(160, 288, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(224, 288, 32, 32),
                    new Rectangle(128, 288, 32, 32),
                    new Rectangle(160, 288, 32, 32),
                    new Rectangle(192, 288, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(128, 288, 32, 32),
                    new Rectangle(160, 288, 32, 32),
                    new Rectangle(192, 288, 32, 32),
                    new Rectangle(224, 288, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(160, 288, 32, 32),
                    new Rectangle(192, 288, 32, 32),
                    new Rectangle(224, 288, 32, 32),
                    new Rectangle(128, 288, 32, 32),
                },
            };

        }

        public override void draw(SpriteBatch b, float alpha = 1)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            if (characterTexture == null)
            {

                return;

            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = (float)StandingPixel.Y / 10000f;

            bool flippant = (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

            bool flippity = flippant || netDirection.Value == 3;

            DrawEmote(b);

            if (netStandbyActive.Value)
            {

                DrawStandby(b, localPosition, drawLayer);

            }
            else if (netHaltActive.Value)
            {

                if (onAlert && idleTimer > 0)
                {

                    DrawAlert(b, localPosition, drawLayer);

                }
                else
                {
                    b.Draw(
                        characterTexture,
                        localPosition - new Vector2(32, 64f),
                        haltFrames[netDirection.Value][0],
                        Color.White,
                        0f,
                        Vector2.Zero,
                        4f,
                        flippant ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                        drawLayer
                    );
                }

            }
            else if (netSweepActive.Value)
            {

                Vector2 sweepVector = localPosition - new Vector2(32, 64f);

                b.Draw(
                     characterTexture,
                     localPosition - new Vector2(32, 64f),
                     sweepFrames[netDirection.Value][sweepFrame],
                     Color.White,
                     0f,
                     Vector2.Zero,
                     4f,
                     flippity ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                     drawLayer
                 );

                weaponRender.DrawWeapon(b, sweepVector, drawLayer, new() { source = sweepFrames[netDirection.Value][sweepFrame], flipped = flippity });

                weaponRender.DrawSwipe(b, sweepVector, drawLayer, new() { source = sweepFrames[netDirection.Value][sweepFrame], flipped = flippity });

            }
            else if (netSpecialActive.Value)
            {

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(32, 64f),
                    specialFrames[netDirection.Value][specialFrame],
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    4f,
                    flippant ? (SpriteEffects)1 : 0,
                    drawLayer
                );

            }
            else if (netDashActive.Value)
            {

                int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

                int dashSetto = Math.Min(dashFrame, (dashFrames[dashSeries].Count - 1));

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(32, 64f + dashHeight),
                    dashFrames[dashSeries][dashSetto],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    4f,
                    flippant ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }
            else if (netSmashActive.Value)
            {
                int smashSeries = netDirection.Value + (netDashProgress.Value * 4);

                int smashSetto = Math.Min(dashFrame, (smashFrames[smashSeries].Count - 1));

                Vector2 smashVector = localPosition - new Vector2(32, 64f + dashHeight);

                b.Draw(
                    characterTexture,
                    smashVector,
                    smashFrames[smashSeries][smashSetto],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    4f,
                    flippity ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

                weaponRender.DrawWeapon(b, smashVector, drawLayer, new() { source = smashFrames[smashSeries][smashSetto], flipped = flippity });
                
                if(netDashProgress.Value >= 2)
                {

                    weaponRender.DrawSwipe(b, smashVector, drawLayer, new() { source = smashFrames[smashSeries][smashSetto], flipped = flippity });

                }

            }
            else
            {

                if (onAlert && idleTimer > 0)
                {

                    DrawAlert(b, localPosition, drawLayer);

                }
                else
                {

                    b.Draw(
                        characterTexture,
                        localPosition - new Vector2(32, 64f),
                        walkFrames[netDirection.Value][moveFrame],
                        Color.White,
                        0f,
                        Vector2.Zero,
                        4f,
                        flippant ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                        drawLayer
                    );

                }

            }

            DrawShadow(b, localPosition, drawLayer);

        }
        
        public override void DrawAlert(SpriteBatch b, Vector2 localPosition, float drawLayer)
        {

            Vector2 alertVector = localPosition - new Vector2(32, 64f);

            int chooseFrame = IdleFrame();

            Rectangle alertFrame = alertFrames[netDirection.Value][chooseFrame];

            bool flippant = (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

            b.Draw(
                 characterTexture,
                 alertVector,
                 alertFrame,
                 Color.White,
                 0f,
                 Vector2.Zero,
                 4f,
                 flippant ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                 drawLayer
             );

            weaponRender.DrawWeapon(b, alertVector, drawLayer, new() { source = alertFrame, flipped = flippant });

        }

        public override void DrawStandby(SpriteBatch b, Vector2 localPosition, float drawLayer)
        {

            DrawAlert(b, localPosition, drawLayer);

        }

        public override void NewDay()
        {
            
            base.NewDay();
            
            workVectors = new();

        }

        public override bool TargetWork()
        {

            if (!Mod.instance.chests.ContainsKey(CharacterHandle.characters.Shadowtin))
            {

                Mod.instance.chests[CharacterHandle.characters.Shadowtin] = new();

            }

            Chest chest = Mod.instance.chests[CharacterHandle.characters.Shadowtin];

            if (!workVectors.ContainsKey(currentLocation.Name))
            {

                workVectors[currentLocation.Name] = new();

            }

            Vector2 currentVector = new((int)(Position.X / 64), (int)(Position.Y / 64));

            List<Vector2> objectVectors = new List<Vector2>();

            for (int i = 0; i < 6; i++)
            {

                if (currentLocation.objects.Count() == 0)
                {
                    break;

                }

                objectVectors = ModUtility.GetTilesWithinRadius(currentLocation, currentVector, i); ;

                foreach (Vector2 objectVector in objectVectors)
                {

                    if (currentLocation.objects.ContainsKey(objectVector))
                    {

                        if (workVectors[currentLocation.Name].ContainsKey(objectVector))
                        {

                            continue;

                        }

                        workVectors[currentLocation.Name][objectVector] = 1;

                        StardewValley.Object targetObject = currentLocation.objects[objectVector];

                        if (ValidWorkTarget(targetObject))
                        {

                            workVector = objectVector;

                            Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position, true);

                            Position = (workVector * 64);

                            SettleOccupied();

                            Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position);

                            netWorkActive.Set(true);

                            netSpecialActive.Set(true);

                            specialTimer = 60;

                            return true;

                        }

                    }

                }

            }

            return false;

        }

        public override List<Vector2> RoamAnalysis()
        {

            List<Vector2> collection = base.RoamAnalysis();

            if (Game1.currentSeason == "winter")
            {

                return collection;

            }

            List<Vector2> scarelist = new List<Vector2>();

            if(currentLocation is Farm farm)
            {

                for(int i = 0; i < 2; i++)
                {

                    foreach (Building building in farm.buildings)
                    {

                        if (building.buildingType.Contains("Coop") || building.buildingType.Contains("Barn"))
                        {

                            Vector2 scareVector = new(building.tileX.Value * 64f, building.tileY.Value * 64f);

                            scarelist.Add(scareVector);

                            scarelist.Add(new Vector2(-1f));

                        }

                    }

                }

            }

            scarelist.AddRange(collection);

            return scarelist;

        }

        public bool ValidWorkTarget(StardewValley.Object targetObject)
        {

            if (targetObject.Name.Contains("Artifact Spot"))
            {

                return true;
            
            }

            if (targetObject.isForage())
            {

                return true;

            }

            if (
                targetObject.QualifiedItemId == "(BC)9" ||
                targetObject.QualifiedItemId == "(BC)10" ||
                targetObject.QualifiedItemId == "(BC)MushroomLog" ||
                targetObject.IsTapper()
                )
            {

                if (targetObject.heldObject.Value != null && targetObject.MinutesUntilReady == 0)
                {

                    return true;
                
                }

            }

            return false;

        }

        public override void PerformWork()
        {

            if (specialTimer == 30)
            {

                if (currentLocation.objects.ContainsKey(workVector))
                {

                    Chest chest = Mod.instance.chests[CharacterHandle.characters.Shadowtin];

                    StardewValley.Object targetObject = currentLocation.objects[workVector];

                    if (targetObject.Name.Contains("Artifact Spot"))
                    {

                        currentLocation.digUpArtifactSpot((int)workVector.X, (int)workVector.Y, Game1.player);

                        currentLocation.objects.Remove(workVector);

                        return;

                    }
                    
                    if (targetObject.isForage())
                    {

                        StardewValley.Item objectInstance = targetObject.getOne();

                        objectInstance.Quality = 4;

                        if (chest.addItem(objectInstance) != null)
                        {

                            if (Mod.instance.trackers.ContainsKey(characterType) && currentLocation.Name == Game1.player.currentLocation.Name)
                            {

                                ThrowHandle throwItem = new(Game1.player, Position, objectInstance);

                                Mod.instance.throwRegister.Add(throwItem);

                            }
                            else
                            {
                                return;

                            }

                        }

                        currentLocation.objects.Remove(workVector);

                        return;

                    }

                    if (
                        targetObject.QualifiedItemId == "(BC)9" ||
                        targetObject.QualifiedItemId == "(BC)10" ||
                        targetObject.QualifiedItemId == "(BC)MushroomLog" ||
                        targetObject.IsTapper()
                        )
                    {

                        if (targetObject.heldObject.Value != null && targetObject.MinutesUntilReady == 0)
                        {

                            StardewValley.Item objectInstance = targetObject.heldObject.Value.getOne();

                            if(targetObject.QualifiedItemId == "(BC)MushroomLog")
                            {

                                objectInstance.Quality = 4;

                            }

                            if (chest.addItem(objectInstance) != null)
                            {

                                if (Mod.instance.trackers.ContainsKey(characterType) && currentLocation.Name == Game1.player.currentLocation.Name)
                                {

                                    ThrowHandle throwItem = new(Game1.player, Position, objectInstance);

                                    Mod.instance.throwRegister.Add(throwItem);

                                }
                                else
                                {
                                    return;

                                }
                            
                            }

                            targetObject.heldObject.Value = null;


                        }

                        return;

                    }

                }

            }

        }

    }

}
