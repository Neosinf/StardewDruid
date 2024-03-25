using Microsoft.Xna.Framework;

namespace StardewDruid.Cast.Weald
{
    internal class Trough : CastHandle
    {

        public Trough(Vector2 target)
            : base(target)
        {

        }

        public override void CastEffect()
        {
            if (targetLocation.objects.ContainsKey(targetVector))
            {
                return;
            }

            targetLocation.objects.Add(targetVector, new StardewValley.Object("178", 1));

            return;

        }

    }

}
