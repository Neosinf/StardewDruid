using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;

namespace StardewDruid.Cast
{
    internal class PetAnimal : Cast
    {

        private readonly StardewValley.FarmAnimal targetAnimal;

        public PetAnimal(Mod mod, Vector2 target, Rite rite, FarmAnimal animal)
            : base(mod, target, rite)
        {

            targetAnimal = animal;

        }

        public override void CastEarth()
        {

            targetAnimal.wasPet.Value = true;

            int num = 7;

            if (targetAnimal.wasAutoPet.Value)
            {
                
                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, (int)targetAnimal.friendshipTowardFarmer.Value + num);
            
            }
            else
            {
                
                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, (int)targetAnimal.friendshipTowardFarmer.Value + 15);
            
            }

            if ((targetPlayer.professions.Contains(3) && !targetAnimal.isCoopDweller()) || (targetPlayer.professions.Contains(2) && targetAnimal.isCoopDweller()))
            {
               
                targetAnimal.friendshipTowardFarmer.Value = Math.Min(1000, (int)targetAnimal.friendshipTowardFarmer.Value + 15);

                
                targetAnimal.happiness.Value = (byte)Math.Min(255, (byte)targetAnimal.happiness.Value + Math.Max(5, 40 - (byte)targetAnimal.happinessDrain.Value));
            
            }

            int num2 = 20;

            if (targetAnimal.wasAutoPet.Value)
            {
                
                num2 = 32;
            
            }

            targetAnimal.doEmote(((int)targetAnimal.moodMessage.Value == 4) ? 12 : num2);

            targetAnimal.happiness.Value = (byte)Math.Min(255, (byte)targetAnimal.happiness.Value + Math.Max(5, 40 - (byte)targetAnimal.happinessDrain.Value));

            targetAnimal.makeSound();

            targetPlayer.gainExperience(0, 5);

            castFire = true;

        }

    }

}
