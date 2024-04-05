using Microsoft.Xna.Framework;
using StardewDruid.Event;
using StardewValley.BellsAndWhistles;
using StardewValley.Locations;
using StardewValley;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast.Weald
{
    internal class Weed : CastHandle
    {

        public float damage;

        public Weed(Vector2 target,  float Damage)
            : base(target)
        {

            castCost = 1;

            damage = Damage;

        }

        public override void CastEffect()
        {

            int radius = 3 + Mod.instance.PowerLevel;

            SpellHandle explode = new(targetLocation, targetVector * 64, Game1.player.Position, radius, 1, -1, (int)(damage * 0.25), 2);

            explode.type = SpellHandle.spells.explode;

            explode.sound = SpellHandle.sounds.flameSpellHit;

            explode.display = SpellHandle.displays.Impact;

            Mod.instance.spellRegister.Add(explode);

            castFire = true;

        }

    }

}
