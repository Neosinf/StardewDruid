using Microsoft.Xna.Framework;
using StardewDruid.Data;
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

            int debrisAmount = 2 + randomIndex.Next(Mod.instance.PowerLevel);

            if (resourceClump.parentSheetIndex.Value == ResourceClump.meteoriteIndex)
            {

                debrisType = 386;

                debrisAmount *= 2;

            }

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

            Mod.instance.iconData.CursorIndicator(targetLocation, cursorVector, IconData.cursors.weald);

        }

    }
}
