using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using static StardewValley.Minigames.TargetGame;

namespace StardewDruid.Cast
{
    internal class Meteor: Cast
    {

        int targetDirection;

        public Meteor(Mod mod, Vector2 target, Farmer player, int direction)
            : base(mod, target, player)
        {

            castCost = 4;

            targetDirection = direction;

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

            targetLocation.playSound("flameSpellHit");

            Dictionary<Vector2, StardewValley.Object> saveObjects = new();

            List<Vector2> tileVectors;

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

                foreach (Vector2 tileVector in tileVectors)
                {

                    // save any objects at risk of destruction
                    if (targetLocation.objects.ContainsKey(tileVector))
                    {

                        StardewValley.Object saveObject = targetLocation.objects[tileVector];

                        if(!saveObject.Name.Contains("Stone") && !saveObject.Name.Contains("Twig") && !saveObject.Name.Contains("Weed"))
                        {
                            
                            saveObjects[tileVector] = targetLocation.objects[tileVector];

                            targetLocation.objects.Remove(tileVector);

                        }
 
                    }

                    if (targetLocation.terrainFeatures.ContainsKey(tileVector))
                    {

                        if (targetLocation.terrainFeatures[tileVector] is StardewValley.TerrainFeatures.Tree)
                        {

                            StardewValley.TerrainFeatures.Tree targetTree = targetLocation.terrainFeatures[tileVector] as StardewValley.TerrainFeatures.Tree;

                            targetTree.performToolAction(null, (int)targetTree.health.Value - 1, tileVector, targetLocation);

                        }

                     }

                }

            }

            targetLocation.explode(targetVector, 3, targetPlayer, false, 128);

            foreach(KeyValuePair<Vector2,StardewValley.Object> kvpObject in saveObjects)
            {

                targetLocation.objects.Add(kvpObject.Key, kvpObject.Value);

            }

        }


    }
}
