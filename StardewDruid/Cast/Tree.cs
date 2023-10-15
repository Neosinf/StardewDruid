using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;
using StardewValley.Monsters;

namespace StardewDruid.Cast
{
    internal class Tree : CastHandle
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

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.Tree treeFeature)
            {

                return;

            }

            //StardewValley.TerrainFeatures.Tree treeFeature = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.Tree;

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

            if(randomIndex.Next(5) == 0 && riteData.spawnIndex["critter"] && !riteData.castToggle.ContainsKey("forgetCritters"))
            {

                Portal critterPortal = new(mod, targetPlayer.getTileLocation(), riteData);

                critterPortal.spawnFrequency = 1;

                critterPortal.spawnIndex = new()
                    {       
                        0,3,99,

                    };

                critterPortal.baseType = "terrain";

                critterPortal.baseVector = targetVector + new Vector2(0,-3);

                critterPortal.baseTarget = true;

                critterPortal.CastTrigger();

                if(critterPortal.spawnQueue.Count > 0)
                {

                    if (!riteData.castTask.ContainsKey("masterCreature"))
                    {

                        mod.UpdateTask("lessonCreature", 1);

                    }

                }

            }

        }

        public override void CastWater()
        {
            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {
                return;
            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.Tree treeFeature)
            {
                return;
            }

            Dictionary<int, int> resinIndex = new()
            {
                [1] = 725, // Oak
                [2] = 724, // Maple
                [3] = 726, // Pine
                [6] = 247, // Palm
                [7] = 422, // Purple Mushroom // Mushroom
                [8] = 419, // Vinegar // Mahogany
                [9] = 247, // Palm
            };

            treeFeature.health.Value = 1;

            treeFeature.performToolAction(riteData.castAxe,0,targetVector, targetLocation);

            if (randomIndex.Next(4) == 0 && resinIndex.ContainsKey(treeFeature.treeType.Value))
            {
                StardewDruid.Cast.Throw throwObject = new(resinIndex[treeFeature.treeType.Value],0);

                throwObject.ThrowObject(targetPlayer, targetVector);

            }

            targetLocation.terrainFeatures.Remove(targetVector);

        }

    }

}
