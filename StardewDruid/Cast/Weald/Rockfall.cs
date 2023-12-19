using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast.Weald
{
    internal class Rockfall : CastHandle
    {

        public int debrisIndex;

        public int powerLevel;

        public bool challengeCast;

        public bool generateRock;

        public bool generateHoed;

        public int castDelay;

        public string castSound;

        public Rockfall(Vector2 target, Rite rite)
            : base(target, rite)
        {

            castCost = 1;

            powerLevel = Mod.instance.virtualPick.UpgradeLevel;

            castDelay = 0;

        }

        public override void CastEffect()
        {

            int tileX = (int)targetVector.X;

            int tileY = (int)targetVector.Y;

            Layer backLayer = riteData.castLocation.Map.GetLayer("Back");

            Tile backTile = backLayer.PickTile(new Location(tileX * 64, tileY * 64), Game1.viewport.Size);

            if (backTile == null)
            {

                return;

            }

            if (backTile.TileIndexProperties.TryGetValue("Type", out var typeValue))
            {

                if (typeValue == "Stone")
                {

                    generateRock = true;

                }
                else if (typeValue == "Dirt")
                {

                    generateHoed = true;

                }

            }

            List<int> indexes = Map.SpawnData.RockFall(targetLocation, targetPlayer, Mod.instance.rockCasts[riteData.castLocation.Name]);

            int objectIndex = indexes[0];

            int scatterIndex = indexes[1];

            debrisIndex = indexes[2];

            ModUtility.AnimateRockfall(this.targetLocation, this.targetVector, this.castDelay, objectIndex, scatterIndex);

            // ------------------------------ impacts

            DelayedAction.functionAfterDelay(DebrisImpact, 575 + castDelay);

            if (generateRock)
            {

                DelayedAction.functionAfterDelay(RockImpact, 600 + castDelay);

            }

            if (generateHoed)
            {

                DelayedAction.functionAfterDelay(DirtImpact, 600 + castDelay);

            }

            castFire = true;

        }

        public void DebrisImpact()
        {
            ModUtility.ImpactVector(targetLocation, targetVector);

            if (riteData.castTask.ContainsKey("masterRockfall") || challengeCast)
            {

                Microsoft.Xna.Framework.Rectangle areaOfEffect = new(
                    (int)targetVector.X * 64 - 32,
                    (int)targetVector.Y * 64 - 32,
                    128,
                    128
                );

                int castDamage = riteData.castDamage / 2;

                targetLocation.damageMonster(areaOfEffect, castDamage, riteData.castDamage, true, targetPlayer);

            }

            if(castSound != null)
            {

                targetLocation.playSoundPitched(castSound, 800);

            }

        }

        public void RockImpact()
        {
            int rockCut = randomIndex.Next(2);

            int generateRock = 1 + randomIndex.Next(powerLevel) / 2;

            for (int i = 0; i < generateRock; i++)
            {

                if (i == 0)
                {

                    if (targetPlayer.professions.Contains(21) && rockCut == 0)
                    {

                        Game1.createObjectDebris(382, (int)targetVector.X, (int)targetVector.Y);

                    }
                    else if (targetPlayer.professions.Contains(19) && rockCut == 0)
                    {

                        Game1.createObjectDebris(debrisIndex, (int)targetVector.X, (int)targetVector.Y);

                    }

                    Game1.createObjectDebris(debrisIndex, (int)targetVector.X, (int)targetVector.Y);

                }
                else
                {

                    Game1.createObjectDebris(390, (int)targetVector.X, (int)targetVector.Y);

                }

            }

            if (!riteData.castTask.ContainsKey("masterRockfall"))
            {

                Mod.instance.UpdateTask("lessonRockfall", generateRock);

            }

            if(castSound != null)
            {

                targetLocation.playSoundPitched(castSound, 800);

            }

        }

        public void DirtImpact()
        {

            List<Vector2> tileVectors = new()
            {
                targetVector,
                targetVector + new Vector2(-1,0),
                targetVector + new Vector2(0,-1),
                targetVector + new Vector2(1,0),
                targetVector + new Vector2(0,1),

            };

            foreach (Vector2 tileVector in tileVectors)
            {
                if (!targetLocation.objects.ContainsKey(tileVector) && targetLocation.doesTileHaveProperty((int)tileVector.X, (int)tileVector.Y, "Diggable", "Back") != null && !targetLocation.isTileHoeDirt(tileVector))
                {

                    targetLocation.checkForBuriedItem((int)tileVector.X, (int)tileVector.Y, explosion: true, detectOnly: false, targetPlayer);

                    targetLocation.makeHoeDirt(tileVector);

                }

            }

        }

    }

}
