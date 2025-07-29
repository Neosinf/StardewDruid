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
            
            tempermentActive = temperment.cautious;

            baseMode = 3;

            baseJuice = 4;

            basePulp = 45;

            cooldownInterval = 60;

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

            shieldScheme = IconData.schemes.mists;

            loadedOut = true;

        }

        public override bool PerformSpecial(Vector2 target)
        {

            LookAtTarget(target);

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle special = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 3)
            {
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

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, Position, 96f + (GetWidth() * 3));

            if (targets.Count > 0)
            {
                specialTimer = (specialCeiling + 1) * specialInterval;

                netSpecialActive.Set(true);

                SetCooldown(1);

                SpellHandle swipeEffect = new(currentLocation, targets.First().GetBoundingBox().Center.ToVector2(), GetBoundingBox().Center.ToVector2(), 256, GetThreat())
                {
                    type = SpellHandle.Spells.explode,

                    display = IconData.impacts.boltnode,

                    boss = this,

                    instant = true
                };

                Mod.instance.spellRegister.Add(swipeEffect);

            }

            return true;

        }

        public override bool PerformChannel(Vector2 target)
        {

            SpellHandle bolt = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 256, GetThreat())
            {
                type = SpellHandle.Spells.bolt,

                display = IconData.impacts.boltnode,

                sound = SpellHandle.Sounds.thunder,

                boss = this,

                counter = -45,

                indicator = IconData.cursors.mists
            };

            bolt.TargetCursor();

            bolt.displayFactor = 4;

            Mod.instance.spellRegister.Add(bolt);

            return true;

        }

    }

}

