using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast
{

    public class CastHandle
    {
        public Vector2 targetVector { get; set; }

        public readonly Farmer targetPlayer;

        public GameLocation targetLocation;

        public bool castFire { get; set; }

        public int castCost { get; set; }

        public bool castLimit { get; set; }

        public Random randomIndex;

        public CastHandle(Vector2 Vector)
        {

            targetVector = Vector;

            randomIndex = Mod.instance.randomIndex;

            targetPlayer = Mod.instance.rite.caster;

            targetLocation = Mod.instance.rite.castLocation;

            castFire = false;

            castCost = 0;

            castLimit = false;

        }

        public virtual void CastEffect()
        {

        }

    }

}
