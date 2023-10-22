using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Monsters;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Netcode;

namespace StardewDruid.Monster
{

    public class Bat : StardewValley.Monsters.Bat
    {

        public List<string> ouchList;

        public Bat(Vector2 vector, int combatModifier)
            : base(vector * 64, combatModifier / 2)
        {

            base.focusedOnFarmers = true;

            base.Health = (int)(combatModifier * 0.375);

            base.DamageToFarmer = (int)Math.Max(2, combatModifier * 0.05);

            ouchList = new()
            {
                "flap flap",
                "flippity",
                "cheeep"
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
