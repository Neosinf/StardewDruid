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

            castCost = 0;

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

            /*if(targetLocation.Name == "Beach" && !targetLocation.objects.ContainsKey(targetVector))
            {

                List<Vector2> fireList = new()
                {
                    newVector + new Vector2(1, 0),
                    newVector + new Vector2(0,1),
                    newVector + new Vector2(1,1)
                };

                foreach (Vector2 fireVector in fireList)
                {
                    if (targetLocation.objects.ContainsKey(fireVector))
                    {
                        targetLocation.objects.Remove(fireVector);

                    }

                    targetLocation.objects.Add(fireVector, new Torch(fireVector, 146, bigCraftable: true)
                    {
                        Fragility = 1,
                        destroyOvernight = true
                    });
                }

            }*/

            if (targetLocation.objects.ContainsKey(targetVector))
            {
                targetLocation.objects.Remove(targetVector);

            }

            targetLocation.objects.Add(newVector, new Torch(newVector, 278, bigCraftable: true)
            {
                Fragility = 1,
                destroyOvernight = true
            });

            //Vector2 tilePosition = new Vector2(newVector.X * 64, newVector.Y * 64);

            Game1.playSound("fireball");

            ModUtility.AnimateBolt(targetLocation, targetVector);

            castFire = true;

            castCost = 24;

            castLimit = true;

            return;

        }

    }

}
