using Microsoft.Xna.Framework;
using StardewDruid.Character;
using System;

namespace StardewDruid.Cast.Mists
{
    internal class Artifact : CastHandle
    {

        public Artifact(Vector2 target, Rite rite)
            : base(target, rite)
        {

        }

        public override void CastEffect()
        {
            if (!targetLocation.objects.ContainsKey(targetVector))
            {
                return;

            }

            StardewValley.Object artifactSpot = targetLocation.objects[targetVector];

            if (artifactSpot == null)
            {
                return;

            }

            targetLocation.digUpArtifactSpot((int)targetVector.X, (int)targetVector.Y, targetPlayer);

            targetLocation.objects.Remove(targetVector);

            castFire = true;

            castCost = 8;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

    }
}
