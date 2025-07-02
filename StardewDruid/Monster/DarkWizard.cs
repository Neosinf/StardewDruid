using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using static StardewDruid.Data.IconData;

namespace StardewDruid.Monster
{
    public class DarkWizard : Dark
    {

        public DarkWizard()
        {


        }

        public DarkWizard(Vector2 vector, int CombatModifier, string name = "Wizard")
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

            hatSelect = 5;

            shieldScheme = IconData.schemes.snazzle;

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
            else
            {

                PerformChannel(target);

            }

            return false;

        }

        public override bool PerformChannel(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            int offset = Mod.instance.randomIndex.Next(2);

            for (int i = 0; i < 4; i++)
            {

                List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(currentLocation, ModUtility.PositionToTile(Game1.player.Position), Mod.instance.randomIndex.Next(2, 3), true, (i * 2) + offset % 8);

                if (castSelection.Count > 0)
                {

                    Vector2 tryVector = castSelection[Mod.instance.randomIndex.Next(castSelection.Count)];

                    SpellHandle fireball = new(currentLocation, tryVector * 64, GetBoundingBox().Center.ToVector2(), 128, GetThreat())
                    {
                        type = SpellHandle.Spells.missile,

                        factor = 3,

                        missile = MissileHandle.missiles.warpball,

                        display = IconData.impacts.puff,

                        scheme = IconData.schemes.snazzle,

                        boss = this,

                        added = new() { SpellHandle.Effects.binds, }
                    };

                    Mod.instance.spellRegister.Add(fireball);

                }

            }

            return true;

        }

    }

}

