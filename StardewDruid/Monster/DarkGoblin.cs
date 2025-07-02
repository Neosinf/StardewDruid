using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;

namespace StardewDruid.Monster
{
    public class DarkGoblin : Dark
    {

        public DarkGoblin()
        {


        }

        public DarkGoblin(Vector2 vector, int CombatModifier, string name = "DarkGoblin")
          : base(vector, CombatModifier, name)
        {

        }


        public override void LoadOut()
        {

            baseMode = 3;

            baseJuice = 3;
            
            basePulp = 30;

            cooldownInterval = 180;

            DarkWalk();

            DarkFlight();

            DarkCast();

            DarkSmash();

            DarkSword();

            weaponRender = new();

            weaponRender.LoadWeapon(WeaponRender.weapons.axe);

            loadedOut = true;

        }
        public override bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 128, GetThreat() * 2 / 3)
            {
                type = SpellHandle.Spells.missile,

                factor = 2,

                missile = MissileHandle.missiles.axe,

                display = IconData.impacts.none,

                boss = this,

                scheme = IconData.schemes.ether
            };

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

    }

}

