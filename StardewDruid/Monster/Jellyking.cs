using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Render;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Extensions;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;

namespace StardewDruid.Monster
{
    public class Jellyking : Jellyfiend
    {

        public Jellyking()
        {
        }

        public Jellyking(Vector2 vector, int CombatModifier, string name = "Jellyking")
          : base(vector, CombatModifier, name)
        {

        }
        public override float GetScale()
        {

            return 3f + netMode.Value * 0.5f;

        }

        public override void ConnectSweep()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            float spriteScale = GetScale();

            SpellHandle slimebomb = new(currentLocation, Position, Position, 48 + (int)(24 * spriteScale), GetThreat())
            {
                type = SpellHandle.Spells.explode,

                instant = true,

                boss = this,

                display = IconData.impacts.splattwo,

                displayRadius = (int)spriteScale,

                sound = SpellHandle.Sounds.slime
            };

            Mod.instance.spellRegister.Add(slimebomb);

        }

        public override bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            float spriteScale = GetScale();

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 48 + (int)(24 * spriteScale), GetThreat())
            {
                displayRadius = (int)spriteScale,

                factor = (int)spriteScale - 1,

                type = SpellHandle.Spells.missile,

                display = IconData.impacts.splattertwo,

                missile = MissileHandle.missiles.slimeballtwo,

                counter = -20,

                sound = SpellHandle.Sounds.slime,

                boss = this
            };

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

    }

}

