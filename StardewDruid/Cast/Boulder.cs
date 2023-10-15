using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using System;

namespace StardewDruid.Cast
{
    internal class Boulder : CastHandle
    {

        private readonly ResourceClump resourceClump;

        public Boulder(Mod mod, Vector2 target, Rite rite, ResourceClump ResourceClump)
            : base(mod, target, rite)
        {

            resourceClump = ResourceClump;

        }

        public override void CastEarth()
        {

            int debrisType = 390;

            int debrisAmount = randomIndex.Next(1, 5);

            for (int i = 0; i < debrisAmount; i++)
            {

                Game1.createObjectDebris(debrisType, (int)targetVector.X, (int)targetVector.Y + 1);

            }

            if (debrisAmount == 1)
            {
                Game1.createObjectDebris(382, (int)targetVector.X + 1, (int)targetVector.Y);

            }

            castFire = true;

            targetPlayer.gainExperience(2, 2); // gain foraging experience

            ModUtility.AnimateGrowth(targetLocation, targetVector);

        }

        public override void CastWater()
        {

            StardewValley.Tools.Pickaxe targetPick = mod.RetrievePick();

            castCost = Math.Max(2, 36 - (targetPlayer.MiningLevel * targetPick.UpgradeLevel));

            resourceClump.health.Set(1f);

            resourceClump.performToolAction(targetPick, 1, targetVector, targetLocation);

            resourceClump.NeedsUpdate = false;

            targetLocation._activeTerrainFeatures.Remove(resourceClump);

            targetLocation.resourceClumps.Remove(resourceClump);

            resourceClump.currentLocation = null;

            if (targetPick.UpgradeLevel >= 3)
            {
                Game1.createObjectDebris(709, (int)this.targetVector.X, (int)this.targetVector.Y);

                Game1.createObjectDebris(709, (int)this.targetVector.X + 1, (int)this.targetVector.Y);

            }

            int debrisMax = 1;

            if (targetPlayer.professions.Contains(22))
            {
                debrisMax = 2;

            }

            for (int i = 0; i < randomIndex.Next(1,debrisMax); i++)
            {

                switch ((int)resourceClump.parentSheetIndex.Value)
                {

                    case 756:
                    case 758:

                        Game1.createObjectDebris(536, (int)targetVector.X, (int)targetVector.Y);

                        break;

                    default:

                        if (targetLocation is MineShaft)
                        {
                            MineShaft mineLocation = (MineShaft)targetLocation;

                            if (mineLocation.mineLevel >= 80)
                            {
                                Game1.createObjectDebris(537, (int)targetVector.X, (int)targetVector.Y);

                                break;

                            }
                            else if (mineLocation.mineLevel >= 121)
                            {
                                Game1.createObjectDebris(749, (int)targetVector.X, (int)targetVector.Y);

                                break;

                            }

                        }

                        Game1.createObjectDebris(535, (int)targetVector.X, (int)targetVector.Y);

                        break;

                }

            }

            castFire = true;

            castCost = 24;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

    }
}
