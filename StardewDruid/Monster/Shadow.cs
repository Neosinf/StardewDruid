using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Monsters;
using System.Collections.Generic;
using System;

namespace StardewDruid.Monster
{
    public class Shadow : ShadowBrute
    {

        public List<string> ouchList;

        public Shadow(Vector2 position, int combatModifier)
            : base(position * 64)
        {

            focusedOnFarmers = true;

            base.Health = combatModifier;

            base.DamageToFarmer = (int)Math.Max(2, combatModifier * 0.1);

            ouchList = new()
            {
                "oooft",
                "deep",
            };

        }
        public override int takeDamage(int damage, int xTrajectory, int yTrajectory, bool isBomb, double addedPrecision, Farmer who)
        {
            int ouchIndex = Game1.random.Next(10);

            if (ouchIndex < ouchList.Count)
            {
                showTextAboveHead(ouchList[ouchIndex], duration: 2000);
            }

            return base.takeDamage(damage, xTrajectory, yTrajectory, isBomb, addedPrecision, who);

        }
    }
}
