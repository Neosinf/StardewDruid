using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
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

        public readonly Mod mod;

        public Random randomIndex;

        public CastHandle(Mod Mod, Vector2 Vector, Rite rite)
        {

            targetVector = Vector;

            mod = Mod;

            randomIndex = rite.randomIndex;

            riteData = rite;

            targetPlayer = riteData.caster;

            targetLocation = riteData.castLocation;

            castFire = false;

            castCost = 2;

            castLimit = false;

        }

        public virtual void CastQuest()
        {

        }

        public virtual void CastEarth()
        {

        }

        public virtual void CastWater()
        {

        }

        public virtual void CastStars()
        {

        }


    }
}
