using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics;
using System.Xml.Linq;
using xTile;

namespace StardewDruid.Cast
{
    internal class Meteor: CastHandle
    {

        int targetDirection;

        int meteorRange;

        public Meteor(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            castCost = Math.Max(4,12-Game1.player.CombatLevel);

            targetDirection = rite.direction;

            meteorRange = 2;

        }

        public override void CastStars()
        {

            ModUtility.AnimateMeteorZone(targetLocation, targetVector, Color.White, meteorRange);

            ModUtility.AnimateMeteor(targetLocation, targetVector, targetDirection < 2);

            DelayedAction.functionAfterDelay(MeteorImpact, 600);

            castFire = true;

        }

        public void MeteorImpact()
        {

            if(targetLocation != Game1.currentLocation)
            {

                return;

            }

            int addedRange = 0;

            if(riteData.castAxe.UpgradeLevel >= 3)
            {

                addedRange++;

            }

            if (riteData.castPick.UpgradeLevel >= 3)
            {

                addedRange++;

            }

            targetLocation.playSound("flameSpellHit");

            Dictionary<Vector2, StardewValley.Object> saveObjects = new();

            List<Vector2> tileVectors;

            int damageRadius = meteorRange + addedRange;

            int damageDiameter = (damageRadius * 2) + 1;

            Microsoft.Xna.Framework.Rectangle areaOfEffect = new Microsoft.Xna.Framework.Rectangle((int)(targetVector.X - damageRadius) * 64, (int)(targetVector.Y - damageRadius) * 64, damageDiameter * 64, damageDiameter * 64);

            targetLocation.damageMonster(areaOfEffect, riteData.castDamage, riteData.castDamage * damageRadius, true, targetPlayer);

            TemporaryAnimatedSprite bigAnimation = new(23, 9999f, 6, 1, new Vector2(targetVector.X * 64f, targetVector.Y * 64f), flicker: false, (Game1.random.NextDouble() < 0.5) ? true : false)
            {
                light = true,
                lightRadius = 3,
                lightcolor = Color.Black,
                alphaFade = 0.03f - 3f * 0.003f,
                Parent = targetLocation
            };

            targetLocation.temporarySprites.Add(bigAnimation);

            int meteorRadius = meteorRange + addedRange + 1;

            for (int i = 0; i < meteorRadius; i++)
            {

                if (i == 0)
                {

                    tileVectors = new List<Vector2>
                    {

                        targetVector

                    };

                }
                else
                {

                    tileVectors = ModUtility.GetTilesWithinRadius(targetLocation, targetVector, i);

                }

                bool destroyVector;

                foreach (Vector2 tileVector in tileVectors)
                {

                    destroyVector = false;

                    if (targetLocation.objects.ContainsKey(tileVector))
                    {

                        StardewValley.Object targetObject = targetLocation.objects[tileVector];

                        if (targetObject.Name.Contains("Stone"))
                        {

                            targetLocation.OnStoneDestroyed(@targetObject.ParentSheetIndex, (int)tileVector.X, (int)tileVector.Y, targetPlayer);

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;

                        }
                        else if (targetObject.Name.Contains("Twig"))
                        {

                            for (int fibreDebris = 2; fibreDebris < i; fibreDebris++)
                            {
                                Game1.createObjectDebris(388, (int)tileVector.X, (int)tileVector.Y);

                            }

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;

                        }
                        else if (targetObject.Name.Contains("Weed"))
                        {

                            Game1.createObjectDebris(771, (int)tileVector.X, (int)tileVector.Y);

                            targetLocation.objects.Remove(tileVector);

                            destroyVector = true;

                        }
                        else if (targetLocation is StardewValley.Locations.MineShaft && targetLocation.objects.ContainsKey(tileVector))
                        {

                            if (targetObject is BreakableContainer)
                            {

                                targetObject.MinutesUntilReady = 1;

                                targetObject.performToolAction(riteData.castPick, targetLocation);

                                targetLocation.objects.Remove(tileVector);

                                destroyVector = true;

                            }

                        }
                        else
                        {

                            for (int j = 0; j < 2; j++)
                            {

                                Tool toolUse = (j == 0) ? riteData.castPick : riteData.castAxe;

                                if (targetLocation.objects.ContainsKey(tileVector) && targetObject.performToolAction(toolUse, targetLocation))
                                {
                                    targetObject.performRemoveAction(tileVector, targetLocation);

                                    targetObject.dropItem(targetLocation, tileVector*64, tileVector * 64 + new Vector2(0,32));

                                    targetLocation.objects.Remove(tileVector);

                                }

                            }

                        }

                    }
                    if (targetLocation.terrainFeatures.ContainsKey(tileVector))
                    {

                        if (targetLocation.terrainFeatures[tileVector] is StardewValley.TerrainFeatures.Tree)
                        {

                            StardewValley.TerrainFeatures.Tree targetTree = targetLocation.terrainFeatures[tileVector] as StardewValley.TerrainFeatures.Tree;

                            if(targetTree.growthStage.Value >= 5)
                            {

                                targetTree.performToolAction(null, (int)targetTree.health.Value, tileVector, targetLocation);

                            }
                            else
                            {

                                targetTree.performToolAction(riteData.castAxe, 0, tileVector, targetLocation);

                                targetLocation.terrainFeatures.Remove(tileVector);

                            }

                            targetTree = null;

                            destroyVector = true;

                        }

                        if (targetLocation.terrainFeatures.ContainsKey(tileVector))
                        {

                            if (targetLocation.terrainFeatures[tileVector] is StardewValley.TerrainFeatures.Grass)
                            {

                                targetLocation.terrainFeatures.Remove(tileVector);


                                if (Game1.random.NextDouble() < 0.5)
                                {

                                    Game1.createObjectDebris(771, (int)tileVector.X, (int)tileVector.Y);

                                }

                                destroyVector = true;

                            }

                        }

                    }

                    if(i == damageRadius || destroyVector) { 

                        if (Game1.random.NextDouble() < 0.5)
                        {
                            TemporaryAnimatedSprite smallAnimation = new(362, Game1.random.Next(30, 90), 6, 1, new Vector2(tileVector.X * 64f, tileVector.Y * 64f), flicker: false, (Game1.random.NextDouble() < 0.5) ? true : false)
                            {
                                delayBeforeAnimationStart = Game1.random.Next(700)
                            };

                            targetLocation.temporarySprites.Add(smallAnimation);
                        }
                        else
                        {

                            TemporaryAnimatedSprite smallAnimation = new(5, new Vector2(tileVector.X * 64f, tileVector.Y * 64f), Color.White, 8, flipped: false, 50f)
                            {
                                delayBeforeAnimationStart = Game1.random.Next(200),
                                scale = (float)Game1.random.Next(5, 15) / 10f
                            };

                            targetLocation.temporarySprites.Add(smallAnimation);

                        }

                    }

                }

            }

        }

    }
}
