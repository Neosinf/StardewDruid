using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast
{

    public class CastHandle
    {
        public Vector2 targetVector { get; set; }

        public readonly Farmer targetPlayer;

        public readonly GameLocation targetLocation;

        public readonly Rite riteData;

        public bool castFire { get; set; }

        public int castCost { get; set; }

        public bool castLimit { get; set; }

        public Random randomIndex;

        public CastHandle(Vector2 Vector, Rite rite)
        {

            targetVector = Vector;

            randomIndex = rite.randomIndex;

            riteData = rite;

            targetPlayer = riteData.caster;

            targetLocation = riteData.castLocation;

            castFire = false;

            castCost = 2;

            castLimit = false;

        }

        public virtual void CastEffect()
        {

        }


    }
}
