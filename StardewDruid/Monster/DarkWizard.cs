using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;
using System;
using System.Collections.Generic;
using static StardewDruid.Data.IconData;

namespace StardewDruid.Monster
{
    public class DarkWizard : DarkRogue
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

            weaponRender.LoadWeapon(WeaponRender.weapons.sword);

            weaponRender.swordScheme = IconData.schemes.snazzle;

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

        public override void DrawShield(SpriteBatch b, Vector2 spritePosition, float spriteScale, float drawLayer, IconData.schemes scheme = schemes.Void)
        {

            base.DrawShield(b, spritePosition, spriteScale, drawLayer, IconData.schemes.snazzle);

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

            int offset = Mod.instance.randomIndex.Next(2);

            for (int i = 0; i < 4; i++)
            {

                List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(currentLocation, ModUtility.PositionToTile(Game1.player.Position), Mod.instance.randomIndex.Next(2, 3), true, (i * 2) + offset % 8);

                if (castSelection.Count > 0)
                {

                    Vector2 tryVector = castSelection[Mod.instance.randomIndex.Next(castSelection.Count)];

                    SpellHandle fireball = new(currentLocation, tryVector * 64, GetBoundingBox().Center.ToVector2(), 192, GetThreat());

                    fireball.type = SpellHandle.spells.missile;

                    fireball.projectile = 2;

                    fireball.missile = IconData.missiles.warpball;

                    fireball.display = IconData.impacts.puff;

                    fireball.scheme = IconData.schemes.snazzle;

                    fireball.boss = this;

                    Mod.instance.spellRegister.Add(fireball);

                }

            }

            return true;

        }

    }

}

