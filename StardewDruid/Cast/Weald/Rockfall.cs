using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast.Weald
{
    internal class Rockfall : CastHandle
    {

        public int debrisIndex;

        public int powerLevel;

        public bool generateRock;

        //public bool generateHoed;

        public int castDelay;

        public float damage;

        public Rockfall(Vector2 target,  float Damage)
            : base(target)
        {

            castCost = 1;

            powerLevel = Mod.instance.virtualPick.UpgradeLevel;

            castDelay = 0;
            
            damage = Damage;

        }

        public override void CastEffect()
        {

            int tileX = (int)targetVector.X;

            int tileY = (int)targetVector.Y;

            Layer backLayer = Mod.instance.rite.castLocation.Map.GetLayer("Back");

            Tile backTile = backLayer.PickTile(new xTile.Dimensions.Location(tileX * 64, tileY * 64), Game1.viewport.Size);

            if (backTile == null)
            {

                return;

            }

            if (backTile.TileIndexProperties.TryGetValue("Type", out var typeValue))
            {

                generateRock = true;

            }

            List<int> indexes = Map.SpawnData.RockFall(targetLocation, targetPlayer, Mod.instance.rockCasts[Mod.instance.rite.castLocation.Name]);

            int objectIndex = indexes[0];

            int scatterIndex = indexes[1];

            debrisIndex = indexes[2];

            ModUtility.AnimateRockfall(this.targetLocation, this.targetVector, this.castDelay, objectIndex, scatterIndex);

            // ------------------------------ impacts

            DelayedAction.functionAfterDelay(RockImpact, 600 + castDelay);


            castFire = true;

        }

        public void RockImpact()
        {

            //ModUtility.AnimateDestruction(targetLocation, targetVector);
            ModUtility.AnimateImpact(targetLocation, targetVector,0,3);

            ModUtility.DamageMonsters(targetLocation, ModUtility.MonsterProximity(targetLocation, new() { targetVector * 64, }, 2, true), targetPlayer, (int)damage / 3, true);

            ModUtility.Explode(targetLocation, targetVector, targetPlayer, 2, powerLevel:1, dirt:1);

            if (!generateRock) { return; }

            int rockCut = randomIndex.Next(2);

            int generateAmt = 1 + randomIndex.Next(powerLevel) / 2;

            for (int i = 0; i < generateAmt; i++)
            {
                if (i == 0)
                {

                    if (targetPlayer.professions.Contains(21) && rockCut == 0)
                    {

                        Game1.createObjectDebris("382", (int)targetVector.X, (int)targetVector.Y);
 
                    }
                    else if (targetPlayer.professions.Contains(19) && rockCut == 0)
                    {

                        Game1.createObjectDebris(debrisIndex.ToString(), (int)targetVector.X, (int)targetVector.Y);

                    }

                    Game1.createObjectDebris( debrisIndex.ToString(), (int)targetVector.X, (int)targetVector.Y);

                }
                else
                {

                    Game1.createObjectDebris("390", (int)targetVector.X, (int)targetVector.Y);
  
                }

            }

            if (!Mod.instance.rite.castTask.ContainsKey("masterRockfall"))
            {

                Mod.instance.UpdateTask("lessonRockfall", generateAmt);

            }

        }


    }

}
