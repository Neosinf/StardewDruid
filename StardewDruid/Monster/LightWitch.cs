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
using static StardewValley.Minigames.BoatJourney;
using static StardewValley.Minigames.TargetGame;

namespace StardewDruid.Monster
{
    public class LightWitch : Dark
    {

        public LightWitch()
        {


        }

        public LightWitch(Vector2 vector, int CombatModifier, string name = "LadyBeyond")
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

            loadedOut = true;

        }

        public override void DrawShield(SpriteBatch b, Vector2 spritePosition, float spriteScale, float drawLayer, IconData.schemes scheme)
        {

            base.DrawShield(b, spritePosition, spriteScale, drawLayer, IconData.schemes.mists);

        }

        public override bool PerformSpecial(Vector2 target)
        {

            LookAtTarget(target);

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle special = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 3);

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

            if (Mod.instance.randomIndex.Next(2) == 0)
            {

                specialTimer = (specialCeiling + 1) * specialInterval;

                netSpecialActive.Set(true);

                SetCooldown(1);

                SpellHandle swipeEffect = new(currentLocation, GetBoundingBox().Center.ToVector2(), GetBoundingBox().Center.ToVector2(), 256, GetThreat());

                swipeEffect.type = SpellHandle.spells.explode;

                swipeEffect.display = IconData.impacts.boltnode;

                swipeEffect.boss = this;

                swipeEffect.instant = true;

                Mod.instance.spellRegister.Add(swipeEffect);

                return true;

            }

            PerformRetreat(Position + (ModUtility.DirectionAsVector(netDirection.Value) * 64));

            return true;

        }

        public override bool PerformChannel(Vector2 target)
        {

            SpellHandle bolt = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 256, GetThreat());

            bolt.type = SpellHandle.spells.bolt;

            bolt.display = IconData.impacts.bomb;

            bolt.sound = SpellHandle.sounds.thunder;

            bolt.boss = this;

            bolt.counter = -45;

            bolt.indicator = IconData.cursors.mists;

            bolt.TargetCursor();

            bolt.factor = 6;

            Mod.instance.spellRegister.Add(bolt);

            return true;

        }

    }

}

