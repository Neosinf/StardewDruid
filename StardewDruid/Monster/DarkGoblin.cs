using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;

namespace StardewDruid.Monster
{
    public class DarkGoblin : DarkRogue
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

            overHead = new(16, -144);

            loadedOut = true;

        }
        public override bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 128, GetThreat() * 2 / 3);

            fireball.type = SpellHandle.spells.missile;

            fireball.projectile = 2;

            fireball.missile = IconData.missiles.axe;

            fireball.display = IconData.impacts.none;

            fireball.boss = this;

            fireball.scheme = IconData.schemes.ether;

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

    }

}

