using Microsoft.Xna.Framework;

namespace StardewDruid.Cast.Weald
{
    internal class FruitTree : CastHandle
    {

        public FruitTree(Vector2 target)
            : base(target)
        {

            castCost = 0;

        }

        public override void CastEffect()
        {

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.FruitTree)
            {

                return;

            }

            StardewValley.TerrainFeatures.FruitTree treeFeature = targetLocation.terrainFeatures[targetVector] as StardewValley.TerrainFeatures.FruitTree;

            treeFeature.performUseAction(targetVector);

            castFire = true;

            Vector2 cursorVector = targetVector * 64 + new Vector2(0, 8);
            ModUtility.AnimateCursor(targetLocation, cursorVector);
        }

    }

}
