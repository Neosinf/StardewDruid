﻿using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;
using xTile.Dimensions;

namespace StardewDruid.Cast.Weald
{
    internal class Pool : CastHandle
    {

        public Pool(Vector2 target, Rite rite)
            : base(target, rite)
        {

            castCost = 8;

            if (rite.caster.FishingLevel >= 6)
            {

                castCost = 4;

            }

        }

        public override void CastEffect()
        {

            if (randomIndex.Next(5) == 0 && riteData.spawnIndex["wildspawn"] && !Mod.instance.EffectDisabled("Wildspawn"))
            {

                StardewValley.Monsters.Monster spawnMonster = Mod.instance.SpawnMonster(targetLocation, targetVector, new() { 0, }, "water");

                if (!riteData.castTask.ContainsKey("masterCreature") && spawnMonster != null)
                {

                    Mod.instance.UpdateTask("lessonCreature", 1);

                }

            }

            int objectIndex = Map.SpawnData.RandomPoolFish(targetLocation);

            int objectQuality = 0;

            int experienceGain = 8;

            Throw throwObject = new(targetPlayer, targetVector * 64, objectIndex, objectQuality);

            throwObject.ThrowObject();

            targetPlayer.currentLocation.playSound("pullItemFromWater");

            targetPlayer.gainExperience(1, experienceGain); // gain fishing experience

            castFire = true;

            bool targetDirection = targetPlayer.getTileLocation().X <= targetVector.X;

            ModUtility.AnimateSplash(targetLocation, targetVector, targetDirection);

        }

    }

}
