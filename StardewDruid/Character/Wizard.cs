using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;

namespace StardewDruid.Character
{
    public class Wizard : StardewDruid.Character.Recruit
    {
        public Wizard()
        {
        }

        public Wizard(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {

            
        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.recruit_one;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Wizard");

            }

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Wizard);

            LoadIntervals();

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            specialIntervals = CharacterRender.HumanoidIntervals();

            specialCeilings = CharacterRender.HumanoidCeilings();

            specialFloors = CharacterRender.HumanoidFloors();

            WeaponLoadout();

            weaponRender.swordScheme = WeaponRender.swordSchemes.sword_lightsaber;

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

        public override void DrawHat(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            bool fliphat = SpriteFlip();

            Vector2 hatPosition = spritePosition - new Vector2(0, 16 * setScale);

            Rectangle hatFrame = hatFrames[netDirection.Value][0];

            if (netIdle.Value == (int)Character.idles.kneel)
            {

                hatPosition = spritePosition - new Vector2(0, 10f * setScale);

                hatFrame = hatFrames[1][0];

            }
            else if (netSpecial.Value == (int)Character.specials.gesture)
            {


                hatPosition = spritePosition - new Vector2(0, 16f * setScale);

                hatFrame = hatFrames[1][0];

            }

            b.Draw(
                characterTexture,
                hatPosition,
                hatFrame,
                Color.White * fade,
                0f,
                new Vector2(16),
                setScale,
                fliphat ? (SpriteEffects)1 : 0,
                drawLayer + 0.0001f
            );

        }

        public override bool MonsterFear()
        {

            return false;

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.invoke);

            specialTimer = 90;

            cooldownTimer = cooldownInterval;

            LookAtTarget(monster.Position, true);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 2);

            special.type = SpellHandle.spells.missile;

            special.missile = MissileHandle.missiles.warpball;

            special.counter = -30;

            special.scheme = IconData.schemes.fates;

            special.factor = 2;

            switch (Mod.instance.randomIndex.Next(3))
            {
                case 0:

                    special.display = IconData.impacts.puff;
                    break;

                case 1:

                    special.added = new() { SpellHandle.effects.blackhole, };

                    break;

                default:

                    special.display = IconData.impacts.flasher;

                    special.added = new() { SpellHandle.effects.doom, };

                    break;

            }

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override bool TrackNotReady()
        {

            if(villager.Name == Game1.player.spouse)
            {

                return false;

            }

            if (Game1.timeOfDay < 800)
            {

                return true;

            }

            return false;

        }

        public override bool TrackOutOfTime()
        {

            if (villager.Name == Game1.player.spouse)
            {

                return false;

            }

            if (Game1.timeOfDay > 2200)
            {

                return true;

            }

            return false;

        }

    }

}
