using Microsoft.Xna.Framework;
using System;

namespace StardewDruid.Cast
{
    interface ICast
    {
        Vector2 targetVector { get; set; }

        Random randomIndex { get; set; }

        bool castFire { get; set; }

        int castCost { get; set; }
        
        bool castLimit { get; set; }

        bool castActive { get; set; }

        void CastEarth();

        void CastWater();

        void CastStars();

        bool CastActive(int castIndex, int castLimit);

        void CastRemove();

        void CastTrigger();

    }
}
