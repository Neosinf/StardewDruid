using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;
using StardewValley.Monsters;

namespace StardewDruid.Cast
{
    internal class Tree : Cast
    {

        public Tree(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

        }

        public override void CastEarth()
        {

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.Tree)
            {

                return;

            }

            StardewValley.TerrainFeatures.Tree treeFeature = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.Tree;

            int debrisType = 388;

            int debrisAmount = randomIndex.Next(1,4);

            if (treeFeature.treeType.Value == 8) //mahogany
            {

                debrisType = 709; debrisAmount = 1;

            }

            if (treeFeature.treeType.Value == 7) // mushroom
            {

                debrisType = 420; debrisAmount = 1;

            }

            for (int i = 0; i < debrisAmount; i++)
            {

                Game1.createObjectDebris(debrisType, (int)targetVector.X, (int)targetVector.Y + 1);

            }

            if (!treeFeature.stump.Value)
            {

                treeFeature.performUseAction(targetVector,targetLocation);

                //treeFeature.performToolAction(null, 1, targetVector, null);

                //treeFeature.health.Value += 1;

            }

            castFire = true;

            targetPlayer.gainExperience(2,4); // gain foraging experience

            if(debrisAmount == 4 && riteData.spawnIndex["critter"])
            {

                Portal critterPortal = new(mod, targetPlayer.getTileLocation(), riteData);

                critterPortal.spawnFrequency = 1;

                critterPortal.specialType = 5;

                critterPortal.baseType = "terrain";

                critterPortal.baseVector = targetVector + new Vector2(0,-3);

                critterPortal.baseTarget = true;

                critterPortal.CastTrigger();

            }

        }

    }

}
