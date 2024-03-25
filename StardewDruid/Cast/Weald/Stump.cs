using Microsoft.Xna.Framework;
using StardewValley.TerrainFeatures;
using System.Collections.Generic;

namespace StardewDruid.Cast.Weald
{
    internal class Stump : CastHandle
    {

        private ResourceClump resourceClump;

        private string resourceType;

        public Stump(Vector2 target,  ResourceClump ResourceClump, string ResourceType)
            : base(target)
        {

            resourceClump = ResourceClump;

            resourceType = ResourceType;

        }

        public override void CastEffect()
        {

            int debrisType = 388;

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
            ModUtility.AnimateCursor(targetLocation, cursorVector, cursorVector);

        }


    }
}
