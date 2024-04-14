using Microsoft.Xna.Framework;
using StardewDruid.Event;
using StardewValley.BellsAndWhistles;
using StardewValley.Locations;
using StardewValley;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast.Weald
{
    internal class Rockfall : CastHandle
    {

        public float damage;

        public string terrain;

        public Rockfall(Vector2 target, float Damage, string Terrain)
            : base(target)
        {
            castCost = 1;
            damage = Damage;
            terrain = Terrain;
        }

        public override void CastEffect()
        {

            SpellHandle rockfall = new(Game1.player, targetVector * 64, 92, damage / 3);

            rockfall.type = SpellHandle.spells.rockfall;

            rockfall.display = SpellHandle.displays.Impact;

            rockfall.power = 1;

            rockfall.terrain = 2;

            if(terrain == "ground")
            {

                rockfall.debris = 1;

            }

            Mod.instance.spellRegister.Add(rockfall);

            castFire = true;

        }

    }

}
