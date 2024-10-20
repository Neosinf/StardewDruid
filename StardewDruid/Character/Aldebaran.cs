using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.FruitTrees;
using StardewValley.Internal;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Aldebaran : StardewDruid.Character.Character
    {

        public List<Vector2> ritesDone = new();

        public Aldebaran()
        {
        }

        public Aldebaran(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            base.LoadOut();

            WeaponLoadout();

            weaponRender.swordScheme = IconData.schemes.sword_stars;

            specialFrames[specials.launch] = new()
            {
                [0] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
                [1] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
                [2] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
                [3] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
            };

            specialIntervals[specials.launch] = 15;
            specialCeilings[specials.launch] = 3;
            specialFloors[specials.launch] = 3;

            idleFrames[idles.standby] = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
            };

            gait = 1.4f;

            restSet = true;

            idleFrames[idles.rest] = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                },
            };

        }

        public override void DrawLaunch(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {
            Rectangle useFrame = specialFrames[specials.launch][netDirection.Value][specialFrame];

            b.Draw(
                characterTexture,
                spritePosition,
                useFrame,
                Color.White * fade,
                0.0f,
                new Vector2(useFrame.Width / 2, useFrame.Height / 2),
                setScale,
                SpriteFlip() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, spritePosition, drawLayer);

        }

        public override void DrawDash(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            if (netDash.Value != (int)dashes.smash)
            {

                base.DrawDash(b, spritePosition, drawLayer, fade);

                return;

            }

            int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

            int dashSetto = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][dashSeries].Count - 1));

            Vector2 dashVector = spritePosition - new Vector2(0, dashHeight);

            Rectangle dashTangle = dashFrames[(dashes)netDash.Value][dashSeries][dashSetto];

            b.Draw(
                characterTexture,
                dashVector,
                dashTangle,
                Color.White * fade,
                0f,
                new Vector2(16),
                setScale,
                SpriteFlip() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, spritePosition, drawLayer);

            weaponRender.DrawWeapon(b, dashVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = dashTangle, flipped = SpriteFlip() });

            if (netDashProgress.Value >= 2)
            {

                weaponRender.DrawSwipe(b, dashVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = dashTangle, flipped = SpriteFlip() });

            }

        }

        public override void DrawSweep(SpriteBatch b, Vector2 sweepVector, float drawLayer, float fade)
        {
            
            Rectangle sweepFrame = specialFrames[(specials)netSpecial.Value][netDirection.Value][specialFrame];

            b.Draw(
                characterTexture,
                sweepVector,
                sweepFrame,
                Color.White * fade,
                0.0f,
                new Vector2(16),
                setScale,
                0,
                drawLayer
            );

            DrawShadow(b, sweepVector, drawLayer);

            weaponRender.DrawWeapon(b, sweepVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = sweepFrame, });

            weaponRender.DrawSwipe(b, sweepVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = sweepFrame, });

        }

        public override void DrawAlert(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            Rectangle alertFrame = idleFrames[idles.alert][netDirection.Value][0];

            b.Draw(
                 characterTexture,
                 spritePosition,
                 alertFrame,
                 Color.White * fade,
                 0f,
                 new Vector2(16),
                 setScale,
                 SpriteAngle() ? (SpriteEffects)1 : 0,
                 drawLayer
             );

            DrawShadow(b, spritePosition, drawLayer);

            weaponRender.DrawWeapon(b, spritePosition - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = alertFrame, flipped = SpriteAngle() });

        }

        public override void DrawStandby(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            int chooseFrame = IdleFrame(idles.standby);

            bool standbyFlip = SpriteAngle();

            b.Draw(
                characterTexture,
                spritePosition,
                idleFrames[idles.standby][0][chooseFrame],
                Color.White,
                0f,
                new Vector2(16),
                4f,
                standbyFlip ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawUnder(b, spritePosition, drawLayer, idleFrames[idles.standby][0][chooseFrame], standbyFlip);

            return;

        }

        public override void DrawRest(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            int chooseFrame = IdleFrame(idles.rest);

            bool standbyFlip = SpriteAngle();

            b.Draw(
                characterTexture,
                spritePosition,
                idleFrames[idles.rest][0][chooseFrame],
                Color.White,
                0f,
                new Vector2(16),
                4f,
                standbyFlip ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, spritePosition, drawLayer);

            return;

        }


        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            if (currentLocation.IsFarm)
            {

                return false;

            }

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 90;

            cooldownTimer = cooldownInterval;

            LookAtTarget(monster.Position, true);

            //Mod.instance.iconData.DecorativeIndicator(currentLocation, Position, IconData.decorations.mists, 5f, new());

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 320, -1, Mod.instance.CombatDamage());

            special.type = SpellHandle.spells.missile;

            special.missile = IconData.missiles.fireball;

            special.scheme = IconData.schemes.stars;

            special.projectile = 3;

            special.power = 4;

            special.explosion = 5;

            special.terrain = 5;

            special.display = IconData.impacts.bomb;

            Mod.instance.spellRegister.Add(special);

            return true;

        }
    }

}
