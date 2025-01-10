﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Monster
{
    public class DarkWitch : Dark
    {

        public IconData.schemes specialScheme = IconData.schemes.snazzle;

        public DarkWitch()
        {


        }

        public DarkWitch(Vector2 vector, int CombatModifier, string name = "Witch")
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

            sweepSet = true;

            channelSet = true;

            specialSet = true;

            specialCeiling = 3;

            specialFloor = 0;

            specialInterval = 15;

            specialFrames = new()
            {

                [0] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpUp,
                        CharacterRender.humanoidFrames.boxUp1,
                        CharacterRender.humanoidFrames.boxUp2,
                        CharacterRender.humanoidFrames.jumpUp,
                    }),
                [1] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpRight,
                        CharacterRender.humanoidFrames.boxRight1,
                        CharacterRender.humanoidFrames.boxRight2,
                        CharacterRender.humanoidFrames.jumpRight,
                    }),
                [2] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpDown,
                        CharacterRender.humanoidFrames.boxDown1,
                        CharacterRender.humanoidFrames.boxDown2,
                        CharacterRender.humanoidFrames.jumpDown,
                    }),
                [3] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpLeft,
                        CharacterRender.humanoidFrames.boxLeft1,
                        CharacterRender.humanoidFrames.boxLeft2,
                        CharacterRender.humanoidFrames.jumpLeft,
                    }),

            };

            woundedFrames = new()
            {
                [0] = new()
                {

                    new(160, 96, 32, 32),

                },
                [1] = new()
                {

                    new(160, 96, 32, 32),

                },
                [2] = new()
                {

                    new(160, 96, 32, 32),

                },
                [3] = new()
                {

                    new(160, 96, 32, 32),

                },

            };

            hatFrames = new()
            {
                [0] = new()
                {
                    new(128, 64, 32, 32),
                },
                [1] = new()
                {
                    new(128, 32, 32, 32),
                },
                [2] = new()
                {
                    new(128, 0, 32, 32),
                },
                [3] = new()
                {
                    new(128, 32, 32, 32),
                },
            };


            loadedOut = true;

        }

        public override void DrawHat(SpriteBatch b, Vector2 spritePosition, float spriteScale, float drawLayer)
        {

            b.Draw(
                characterTexture,
                Game1.GlobalToLocal(Position) + new Vector2(32, -14f * spriteScale),
                hatFrames[netDirection.Value][0],
                Color.White,
                0f,
                new Vector2(16),
                spriteScale,
                netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer + 0.0001f
            );

        }

        public override void DrawShield(SpriteBatch b, Vector2 spritePosition, float spriteScale, float drawLayer, IconData.schemes scheme)
        {

            base.DrawShield(b, spritePosition, spriteScale, drawLayer, specialScheme);

        }

        public override bool PerformSpecial(Vector2 target)
        {

            LookAtTarget(target);

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle special = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 3);

            special.scheme = specialScheme;

            special.type = SpellHandle.spells.lightning;

            special.factor = 2;

            special.counter = -45;

            special.indicator = IconData.cursors.mists;

            special.TargetCursor();

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override bool PerformSweep()
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

            PerformRetreat(Position + (ModUtility.DirectionAsVector(netDirection.Value)*64));

            return true;

        }

        public override bool PerformChannel(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            int offset = Mod.instance.randomIndex.Next(2);

            for (int i = 0; i < 4; i++)
            {

                List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(currentLocation, ModUtility.PositionToTile(Game1.player.Position), Mod.instance.randomIndex.Next(2,3), true, (i * 2) + offset % 8);

                if (castSelection.Count > 0)
                {

                    Vector2 tryVector = castSelection[Mod.instance.randomIndex.Next(castSelection.Count)];

                    SpellHandle fireball = new(currentLocation, tryVector * 64, GetBoundingBox().Center.ToVector2(), 192, GetThreat());

                    fireball.type = SpellHandle.spells.missile;

                    fireball.factor = 2;

                    fireball.missile = MissileHandle.missiles.fireball;

                    fireball.display = IconData.impacts.puff;

                    fireball.scheme = specialScheme;

                    fireball.boss = this;

                    Mod.instance.spellRegister.Add(fireball);

                }

            }

            return true;

        }

    }

}
