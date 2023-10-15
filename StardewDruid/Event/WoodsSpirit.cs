using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Monsters;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Event
{
    public class WoodsSpirit : DustSpirit
    {

        public List<string> ouchList;

        public List<string> dialogueList;

        public int tickCount;

        public WoodsSpirit(Vector2 position, int difficultyMod)
            : base(position, true)
        {

            focusedOnFarmers = true;

            Slipperiness = 3;

            HideShadow = false;

            jitteriness.Value = 0.0;

            DamageToFarmer += difficultyMod;

            Health += difficultyMod * difficultyMod;

            ExperienceGained += difficultyMod / 2;

            objectsToDrop.Clear();

            if (Game1.random.Next(3) == 0)
            {
                objectsToDrop.Add(382); // coal
            }
            else if (Game1.random.Next(5) == 0 && difficultyMod >= 5)
            {
                objectsToDrop.Add(395); // coffee (edible)
            }
            else if (Game1.random.Next(7) == 0 && difficultyMod >= 8)
            {
                objectsToDrop.Add(251); // tea sapling
            }

            ouchList = new()
            {
                "ow ow",
                "ouchies",
            };

            dialogueList = new()
            {
                "meep meep?",
                "meep",
                "MEEEP",
            };

        }

        public override int takeDamage(int damage, int xTrajectory, int yTrajectory, bool isBomb, double addedPrecision, Farmer who)
        {
            int ouchIndex = Game1.random.Next(10);
            if (ouchList.Count - 1 >= ouchIndex)
            {
                showTextAboveHead(ouchList[ouchIndex], duration: 2000);
            }

            return base.takeDamage(damage, xTrajectory, yTrajectory, isBomb, addedPrecision, who);

        }

        public override void behaviorAtGameTick(GameTime time)
        {

            tickCount++;

            if (tickCount >= 200)
            {
                int dialogueIndex = Game1.random.Next(12);
                if (dialogueList.Count - 1 >= dialogueIndex)
                {
                    showTextAboveHead(dialogueList[dialogueIndex], duration: 2000);
                }
                tickCount = 0;
            }

            base.behaviorAtGameTick(time);

        }

    }

}
