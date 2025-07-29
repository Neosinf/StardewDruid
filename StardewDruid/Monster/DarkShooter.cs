using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;

namespace StardewDruid.Monster
{
    public class DarkShooter : Dark
    {

        public DarkShooter()
        {


        }

        public DarkShooter(Vector2 vector, int CombatModifier, string name = "DarkShooter")
          : base(vector, CombatModifier, name)
        {

        }

        public override void LoadOut()
        {

            baseMode = 2;

            baseJuice = 4;

            basePulp = 20;

            cooldownInterval = 150;

            DarkWalk();

            DarkFlight();

            DarkBlast();

            DarkBrawl();

            weaponRender = new()
            {
                melee = false
            };

            weaponRender.LoadWeapon(WeaponRender.weapons.bazooka);

            loadedOut = true;

        }

        public override void SetMode(int mode)
        {
            
            base.SetMode(mode);

            tempermentActive = temperment.ranged;

        }

        public override bool PerformSpecial(Vector2 target)
        {

            PerformChannel(target);

            /*specialTimer = (specialCeiling + 1) * specialInterval * 2;

            netChannelActive.Set(true);

            SetCooldown(1);

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 192, GetThreat());

            fireball.type = SpellHandle.spells.missile;

            fireball.factor =2;

            fireball.missile = MissileHandle.missiles.rocket;

            fireball.display = IconData.impacts.impact;

            fireball.boss = this;

            fireball.indicator = IconData.cursors.scope;

            fireball.scheme = IconData.schemes.stars;

            fireball.added = new() { SpellHandle.effects.embers, };

            Mod.instance.spellRegister.Add(fireball);*/

            return true;

        }

        public override bool PerformChannel(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval * 2;

            netChannelActive.Set(true);

            SetCooldown(2);

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 256, GetThreat())
            {
                type = SpellHandle.Spells.missile,

                displayFactor = 3,

                missile = MissileHandle.missiles.rocket,

                display = IconData.impacts.impact,

                boss = this,

                indicator = IconData.cursors.scope,

                scheme = IconData.schemes.stars,

                added = new() { SpellHandle.Effects.embers, }
            };

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

    }

}

