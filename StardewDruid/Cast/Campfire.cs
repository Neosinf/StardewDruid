using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast
{
    internal class Campfire : Cast
    {

        public Campfire (Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

        }

        public override void CastWater()
        {
            List<string> recipeList = new() {
                "Salad",
                "Baked Fish",
                "Fried Mushroom",
                "Carp Surprise",
                "Hashbrowns",
                "Fried Eel",
                "Sashimi",
                "Maki Roll",
                "Algae Soup",
                "Fish Stew",
                "Escargot",
                "Pale Broth",
            };

            int learnedRecipes = 0;

            foreach(string recipe in recipeList)
            {

                if(!targetPlayer.cookingRecipes.ContainsKey(recipe))
                {

                    targetPlayer.cookingRecipes.Add(recipe, 0);

                    learnedRecipes++;

                }

            }

            if(learnedRecipes >= 1)
            {

                Game1.addHUDMessage(new HUDMessage($"Learned {learnedRecipes} recipes", 2));

            }

            Vector2 newVector = new(targetVector.X, targetVector.Y);

            targetLocation.objects.Remove(targetVector);

            targetLocation.objects.Add(newVector, new Torch(newVector, 278, bigCraftable: true)
            {
                Fragility = 1,
                destroyOvernight = true
            });

            Vector2 tilePosition = new Vector2(newVector.X * 64, newVector.Y * 64);

            Game1.playSound("fireball");

            ModUtility.AnimateBolt(targetLocation, targetVector);

            castFire = true;

            castCost = 24;

            castLimit = true;

            return;

        }

    }

}
