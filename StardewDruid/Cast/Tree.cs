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

            if(rite.caster.ForagingLevel >= 8)
            {

                castCost = 1;

            }

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

            int debrisMax = 3;

            if(targetPlayer.professions.Contains(12))
            {

                debrisMax++;

            }

            if (treeFeature.treeType.Value == 8) //mahogany
            {

                debrisType = 709; debrisMax = 1;

                if (targetPlayer.professions.Contains(14))
                {

                    debrisMax++;

                }

            }

            if (treeFeature.treeType.Value == 7) // mushroom
            {

                debrisType = 420; debrisMax = 1;

            }

            for (int i = 0; i < randomIndex.Next(Math.Min(debrisMax,riteData.castAxe.UpgradeLevel+1)); i++)
            {

                Game1.createObjectDebris(debrisType, (int)targetVector.X, (int)targetVector.Y + 1);

            }

            if (!treeFeature.stump.Value)
            {

                treeFeature.performUseAction(targetVector,targetLocation);

            }

            castFire = true;

            targetPlayer.gainExperience(2,2); // gain foraging experience

            if(randomIndex.Next(5) == 0 && riteData.spawnIndex["critter"] && !mod.ForgotEffect("forgetCritters"))
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
