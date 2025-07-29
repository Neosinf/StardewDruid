using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Location.Druid;
using StardewDruid.Render;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Character
{
    public class Lance : StardewDruid.Character.Recruit
    {

        public Lance()
        {
        }

        public Lance(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {

        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Lance;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Lance");

                Portrait = villager.Portrait;

            }

            setScale = 3.25f;

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Lance);

            LoadIntervals();

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            specialIntervals = CharacterRender.HumanoidIntervals();

            specialCeilings = CharacterRender.HumanoidCeilings();

            specialFloors = CharacterRender.HumanoidFloors();

            hatVectors = CharacterRender.HumanoidHats();

            WeaponLoadout(WeaponRender.weapons.gungnir);

            setScale = 3.5f;

            loadedOut = true;

        }

        public override bool MonsterFear()
        {

            return false;

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.special);

            specialTimer = 90;

            SetCooldown(specialTimer, 1f);

            LookAtTarget(monster.Position, true);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 2)
            {

                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.fireball,

                counter = -30,

                scheme = IconData.schemes.stars,

                displayFactor = 2,

                display = IconData.impacts.bomb

            };

            special.added = new() { SpellHandle.Effects.explode, };

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override bool TrackNotReady()
        {

            if (villager.Name == Game1.player.spouse)
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