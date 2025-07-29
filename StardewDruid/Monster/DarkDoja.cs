using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;
using System;
using System.Collections.Generic;

namespace StardewDruid.Monster
{
    public class DarkDoja : Dark
    {

        public DarkDoja()
        {


        }

        public DarkDoja(Vector2 vector, int CombatModifier, string name = "Doja")
          : base(vector, CombatModifier, name)
        {

        }


        public override void LoadOut()
        {

            baseMode = 3;

            baseJuice = 4;

            basePulp = 50;

            cooldownInterval = 180;

            DarkWalk();

            DarkFlight();

            DarkCast();

            channelSet = true;

            DarkSmash();

            DarkSword();

            weaponRender = new();

            weaponRender.LoadWeapon(WeaponRender.weapons.lightsaber);

            meleeSet = true;

            hatVectors = CharacterRender.HumanoidHats();

            hatSelect = 1;

            shieldScheme = IconData.schemes.fates;

            loadedOut = true;

        }

        public override bool PerformSpecial(Vector2 target)
        {

            if (Mod.instance.randomIndex.Next(2) == 0 && !netShieldActive.Value && shieldTimer <= 0)
            {

                specialTimer = (specialCeiling + 1) * specialInterval;

                netSpecialActive.Set(true);

                SetCooldown(1);

                netShieldActive.Set(true);

                shieldTimer = 600;

                return true;

            }

            return false;

        }

        public override bool PerformChannel(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle fireball = new(currentLocation, Game1.player.Position, GetBoundingBox().Center.ToVector2(), 256, GetThreat())
            {
                type = SpellHandle.Spells.missile,

                displayFactor = 3,

                missile = MissileHandle.missiles.warpball,

                scheme = IconData.schemes.fates,

                display = IconData.impacts.puff,

                boss = this,

                added = new() { SpellHandle.Effects.binds, }
            };

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

    }

}

