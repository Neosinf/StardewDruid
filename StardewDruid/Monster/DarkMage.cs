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
    public class DarkMage : DarkRogue
    {

        public DarkMage()
        {


        }

        public DarkMage(Vector2 vector, int CombatModifier, string name = "Doja")
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

            weaponRender.LoadWeapon(WeaponRender.weapons.sword);

            weaponRender.swordScheme = IconData.schemes.sword_warp;

            overHead = new(16, -144);

            hatFrames = new()
            {
                [0] = new()
                {
                    new(192, 64, 32, 32),
                },
                [1] = new()
                {
                    new(192, 32, 32, 32),
                },
                [2] = new()
                {
                    new(192, 0, 32, 32),
                },
                [3] = new()
                {
                    new(192, 32, 32, 32),
                },
            };

            loadedOut = true;

        }

        public override void DrawHat(SpriteBatch b, Vector2 spritePosition, float spriteScale, float drawLayer)
        {
            if (netWoundedActive.Value)
            {

                spritePosition.Y += 20;


            }

            b.Draw(
                characterTexture,
                spritePosition - new Vector2(0,64),
                hatFrames[netDirection.Value][0],
                Color.White,
                0f,
                Vector2.Zero,
                4f,
                netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer + 0.0001f
            );
        }

        public override void DrawShield(SpriteBatch b, Vector2 spritePosition, float spriteScale, float drawLayer, IconData.schemes scheme = IconData.schemes.Void)
        {

            base.DrawShield(b, spritePosition, spriteScale, drawLayer, IconData.schemes.none);

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

            SpellHandle fireball = new(currentLocation, Game1.player.Position, GetBoundingBox().Center.ToVector2(), 256, GetThreat());

            fireball.type = SpellHandle.spells.missile;

            fireball.projectile = 3;

            fireball.missile = IconData.missiles.warpball;

            fireball.scheme = IconData.schemes.fates;

            fireball.display = IconData.impacts.puff;

            fireball.boss = this;

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

    }

}

