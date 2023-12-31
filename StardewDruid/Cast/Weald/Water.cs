﻿using Microsoft.Xna.Framework;
using StardewDruid.Event.World;
using StardewDruid.Map;
using StardewValley;

namespace StardewDruid.Cast.Weald
{
    internal class Water : CastHandle
    {

        public Water(Vector2 target, Rite rite)
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

            int randomFish = SpawnData.RandomLowFish(targetLocation);

            int objectQuality = 0;

            int experienceGain;

            experienceGain = 8;

            if(11 - targetPlayer.fishingLevel.Value <= 0)
            {

                objectQuality = 2;

                experienceGain = 16;

            }
            else if (randomIndex.Next(11 - targetPlayer.fishingLevel.Value) == 0)
            {

                objectQuality = 2;

                experienceGain = 16;

            }

            StardewDruid.Cast.Throw throwObject = new(targetPlayer, targetVector * 64, randomFish, objectQuality);

            Game1.player.checkForQuestComplete(Game1.getCharacterFromName("Willy"), randomFish, 1, throwObject.objectInstance, "fish", 7);

            throwObject.ThrowObject();

            targetPlayer.currentLocation.playSound("pullItemFromWater");

            targetPlayer.gainExperience(1, experienceGain); // gain fishing experience

            castFire = true;

            bool targetDirection = (targetPlayer.getTileLocation().X > targetVector.X); // false animation goes left to right, true animation right to left, check if target is right of left

            ModUtility.AnimateSplash(targetLocation, targetVector, targetDirection);

        }


    }

}
