﻿using Microsoft.Xna.Framework;
using StardewValley;

namespace StardewDruid.Cast.Mists
{
    internal class Totem : CastHandle
    {

        public int targetIndex { get; set; }


        public Totem(Vector2 target,  int TargetIndex)
            : base(target)
        {

            targetIndex = TargetIndex;

            castCost = 0;
        }

        public override void CastEffect()
        {

            int extractionChance = 1;

            if (!Mod.instance.rite.castTask.ContainsKey("masterTotem"))
            {

                Mod.instance.UpdateTask("lessonTotem", 1);

            }
            else
            {
                extractionChance = randomIndex.Next(1, 3);

            }

            for (int i = 0; i < extractionChance; i++)
            {
                //Game1.createObjectDebris(targetIndex, (int)targetVector.X, (int)targetVector.Y - 1);
                Throw throwObject = new(targetPlayer, targetVector * 64, targetIndex);

                throwObject.ThrowObject();
            
            }

            castFire = true;

            Vector2 boltVector = new(targetVector.X, targetVector.Y - 2);

            ModUtility.AnimateBolt(targetLocation, boltVector);

            return;

        }

    }

}
