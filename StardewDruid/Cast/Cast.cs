using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;

namespace StardewDruid.Cast
{
    /*
        // Reference data for casts
        forester = 12; Gain 25% more wood when chopping
        gatherer = 13; Chance for double harvest of foraged items.
        lumberjack = 14; All trees have a chance to drop hardwood.

        geologist = 19; Chance for gems to appear in pairs.
        burrower = 21; Chance to find coal doubled.
        excavator = 22; Chance to find geodes doubled.

    */

    public class Cast
    {
        public Vector2 targetVector { get; set; }

        public Random randomIndex { get; set; }

        public readonly Rite riteData;

        public bool castFire { get; set; }

        public int castCost { get; set; }

        public bool castLimit { get; set; }

        public bool castActive { get; set; }

        public double expireTime { get; set; }

        public readonly Mod mod;

        public readonly Farmer targetPlayer;

        public readonly GameLocation targetLocation;

        public Cast(Mod Mod, Vector2 Vector, Rite RiteData)
        {

            targetVector = Vector;

            mod = Mod;

            randomIndex = new Random();

            riteData = RiteData;

            targetPlayer = riteData.caster;

            targetLocation = riteData.castLocation;

            castFire = false;

            castCost = 2;

            castLimit = false;

            castActive = false;

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

        public virtual bool CastActive(int castIndex, int castLimit)
        {
            return false;
        }

        public virtual void CastExtend()
        {

            expireTime++;

        }

        public virtual void CastRemove()
        {

        }

        public virtual void CastTrigger()
        {

        }

    }
}
