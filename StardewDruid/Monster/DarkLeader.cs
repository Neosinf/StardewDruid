using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;
using System;

namespace StardewDruid.Monster
{
    public class DarkLeader : Dark
    {

        public DarkLeader()
        {


        }

        public DarkLeader(Vector2 vector, int CombatModifier, string name = "Shadowtin")
          : base(vector, CombatModifier, name)
        {

        }


        public override void LoadOut()
        {

            baseMode = 3;

            baseJuice = 4;

            basePulp = 35;

            cooldownInterval = 180;

            DarkWalk();

            DarkFlight();

            DarkCast();

            DarkSmash();

            DarkSword();

            weaponRender = new();

            weaponRender.LoadWeapon(WeaponRender.weapons.carnyx);

            overHead = new(16, -144);

            loadedOut = true;

        }

    }

}

