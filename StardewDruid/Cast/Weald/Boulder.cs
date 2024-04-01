using Microsoft.Xna.Framework;
using StardewValley.TerrainFeatures;
using System.Collections.Generic;

namespace StardewDruid.Cast.Weald
{
    internal class Boulder : CastHandle
    {

        private readonly ResourceClump resourceClump;

        public Boulder(Vector2 target,  ResourceClump ResourceClump)
            : base(target)
        {
            castCost = 1;

            resourceClump = ResourceClump;

        }

        public override void CastEffect()
        {

            int debrisType = 390;

            int debrisAmount = randomIndex.Next(1, 5);

            Dictionary<int, Throw> throwList = new();

            for (int i = 0; i < debrisAmount; i++)
            {

                throwList[i] = new(targetPlayer, targetVector * 64, debrisType, 0);

                throwList[i].ThrowObject();

            }

            if (debrisAmount == 1)
            {

                throwList[1] = new(targetPlayer, targetVector * 64, 382, 0);

                throwList[1].ThrowObject();

            }

            castFire = true;

            targetPlayer.gainExperience(2, 2); // gain foraging experience

            Vector2 cursorVector = targetVector * 64 + new Vector2(32, 40);
            ModUtility.AnimateCursor(targetLocation, cursorVector);
        }

    }
}
