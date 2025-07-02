using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;

namespace StardewDruid.Monster
{
    public class DarkBrute : Dark
    {

        public DarkBrute()
        {


        }

        public DarkBrute(Vector2 vector, int CombatModifier, string name = "DarkBrute")
          : base(vector, CombatModifier, name)
        {

        }


        public override void LoadOut()
        {

            baseMode = 2;

            baseJuice = 3;
            
            basePulp = 20;

            cooldownInterval = 180;

            groupMode = true;

            DarkWalk();

            DarkFlight();

            DarkSmash();

            DarkBrawl();

            weaponRender = new()
            {
                melee = false
            };

            loadedOut = true;

        }

    }

}

