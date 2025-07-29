using Microsoft.Xna.Framework;
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

            hatVectors = CharacterRender.HumanoidHats();

            hatVectors[StardewDruid.Character.Character.hats.launch] = hatVectors[StardewDruid.Character.Character.hats.stand];

            hatSelect = 4;

            shieldScheme = IconData.schemes.snazzle;

            loadedOut = true;

        }

        public override float GetScale()
        {

            return 3.75f;

        }

        public override bool PerformSpecial(Vector2 target)
        {

            LookAtTarget(target);

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle special = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 3)
            {
                scheme = specialScheme,

                type = SpellHandle.Spells.lightning,

                displayFactor = 2,

                counter = -45,

                indicator = IconData.cursors.mists
            };

            special.TargetCursor();

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override bool PerformSweep(Vector2 target)
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

            PerformRetreat(Position + (ModUtility.DirectionAsVector(netDirection.Value*2)*64));

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

                    SpellHandle fireball = new(currentLocation, tryVector * 64, GetBoundingBox().Center.ToVector2(), 192, GetThreat())
                    {
                        type = SpellHandle.Spells.missile,

                        displayFactor = 2,

                        missile = MissileHandle.missiles.fireball,

                        display = IconData.impacts.puff,

                        scheme = specialScheme,

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

