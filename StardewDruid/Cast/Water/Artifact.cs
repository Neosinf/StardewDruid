using Force.DeepCloner;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Minigames;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast.Water
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

            //StardewValley.Tools.Hoe newHoe = new();

            //newHoe.DoFunction(Game1.player.currentLocation, 0, 0, 1, Game1.player);

            //if (newHoe == null)
            //{
            //    return;

            //}

            //artifactSpot.performToolAction(newHoe, targetLocation);

            Mod.instance.virtualHoe.DoFunction(targetLocation, 0, 0, 1, targetPlayer);

            targetPlayer.Stamina += Math.Min(2, targetPlayer.MaxStamina - targetPlayer.Stamina);

            artifactSpot.performToolAction(Mod.instance.virtualHoe, targetLocation);

            castFire = true;

            castCost = 8;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

    }
}
