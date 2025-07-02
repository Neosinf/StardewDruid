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
    public class DarkPhantom : Dark
    {

        public DarkPhantom()
        {


        }

        public DarkPhantom(Vector2 vector, int CombatModifier, string name = "Phantom")
          : base(vector, CombatModifier, name)
        {
            SpawnData.MonsterDrops(this, SpawnData.Drops.phantom);

        }

        public override void LoadOut()
        {

            baseMode = 3;

            baseJuice = 4;

            basePulp = 50;

            cooldownInterval = 300;

            DarkWalk();

            gait = 1.5f;

            specialSet = true;

            specialCeiling = 1;

            specialFloor = 0;

            specialInterval = 30;

            DarkFlight();

            DarkSmash();

            DarkSword();

            weaponRender = new();

            weaponRender.LoadWeapon(WeaponRender.weapons.cutlass);

            hatVectors = CharacterRender.HumanoidHats();

            hatSelect = 1;

            switch (realName.Value)
            {

                default:
                case "Phantom":

                    hatSelect = 9;

                    break;

                case "PhantomCaptain":

                    hatSelect = 8;

                    break;

                case "PhantomMate":

                    smashSet = false;

                    meleeSet = false;

                    channelFrames = CharacterRender.WeaponLaunch();

                    firearmSet = true;

                    hatSelect = 7;

                    weaponRender.LoadWeapon(WeaponRender.weapons.cannon);

                    break;

            }

            loadedOut = true;

        }

        public override void SetMode(int mode)
        {

            base.SetMode(mode);

            switch (realName.Value)
            {

                default:
                case "Phantom":

                    RandomTemperment();

                    break;

                case "PhantomCaptain":

                    tempermentActive = temperment.aggressive;

                    break;

                case "PhantomMate":

                    tempermentActive = temperment.ranged;

                    break;

            }

        }

        public override float GetScale()
        {

            float spriteScale = 3.5f + (0.25f * netMode.Value);

            return spriteScale;

        }

        public override void update(GameTime time, GameLocation location)
        {

            if (fadeOut < 0.75f)
            {

                fadeOut += 0.0025f;

            }

            base.update(time, location);
        
        }

        public override bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle missile = new(currentLocation, target, Position - new Vector2(64), 192, GetThreat())
            {
                type = SpellHandle.Spells.missile,

                factor = 2,

                missile = MissileHandle.missiles.knife,

                display = IconData.impacts.none,

                scheme = IconData.schemes.mists,

                sound = SpellHandle.Sounds.swordswipe
            };

            Mod.instance.spellRegister.Add(missile);

            return true;

        }

    }

}

