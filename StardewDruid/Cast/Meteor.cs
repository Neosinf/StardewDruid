using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics;
using System.Xml.Linq;
using xTile;

namespace StardewDruid.Cast
{
    internal class Meteor: Cast
    {

        int targetDirection;

        public Meteor(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            castCost = 8 - (int)Math.Ceiling((double)(rite.caster.CombatLevel/2));

            targetDirection = rite.direction;

        }

        public override void CastStars()
        {

            // --------------------------- splash animation

            int animationRow = 0;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            int animationLoops = 1;

            Vector2 animationPosition = new((targetVector.X * 64), (targetVector.Y * 64));

            float animationSort = (targetVector.X * 1000) + targetVector.Y;

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, Color.Red, 1f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(newAnimation);


            // ---------------------------- fireball animation

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

            StardewValley.Tools.Axe targetAxe = mod.RetrieveAxe();

            targetLocation.playSound("flameSpellHit");

            Dictionary<Vector2, StardewValley.Object> saveObjects = new();

            List<Vector2> tileVectors;

            Microsoft.Xna.Framework.Rectangle areaOfEffect = new Microsoft.Xna.Framework.Rectangle((int)(targetVector.X - 3f) * 64, (int)(targetVector.Y - 3f) * 64, (3 * 2 + 1) * 64, (3 * 2 + 1) * 64);

            targetLocation.damageMonster(areaOfEffect, 128, 256, true, targetPlayer);

            TemporaryAnimatedSprite bigAnimation = new(23, 9999f, 6, 1, new Vector2(targetVector.X * 64f, targetVector.Y * 64f), flicker: false, (Game1.random.NextDouble() < 0.5) ? true : false)
            {
                light = true,
                lightRadius = 3,
                lightcolor = Color.Black,
                alphaFade = 0.03f - 3f * 0.003f,
                Parent = targetLocation
            };

            targetLocation.temporarySprites.Add(bigAnimation);

            // reduce tree health for visceral impact
            for (int i = 0; i < 5; i++)
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

                        if (targetLocation.objects.ContainsKey(tileVector))
                        {

                            if (targetObject.Name.Contains("Twig"))
                            {

                                for (int fibreDebris = 2; fibreDebris < i; fibreDebris++)
                                {
                                    Game1.createObjectDebris(388, (int)tileVector.X, (int)tileVector.Y);

                                }

                                targetLocation.objects.Remove(tileVector);

                                destroyVector = true;

                            }

                        }

                        if (targetLocation.objects.ContainsKey(tileVector))
                        {

                            if (targetObject.Name.Contains("Weed"))
                            {

                                //for (int fibreDebris = 2; fibreDebris < i; fibreDebris++)
                                //{
                                    Game1.createObjectDebris(771, (int)tileVector.X, (int)tileVector.Y);

                                //}

                                targetLocation.objects.Remove(tileVector);

                                destroyVector = true;

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

                                targetTree.performToolAction(targetAxe, 0, tileVector, targetLocation);

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

                                //for (int fibreDebris = 2; fibreDebris < i; fibreDebris++)
                                //{
                                    Game1.createObjectDebris(771, (int)tileVector.X, (int)tileVector.Y);

                                //}

                                destroyVector = true;

                            }

                        }

                    }

                    if(i == 3 || destroyVector) { 

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
