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
    public class Astarion : StardewDruid.Character.Recruit
    {

        public Vector2 reapparate;

        public Astarion()
        {
        }

        public Astarion(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {

            reapparate = Vector2.Zero;

        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Astarion;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Astarion");

                Portrait = villager.Portrait;

            }

            setScale = 3.25f;

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Astarion);

            LoadIntervals();

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            specialIntervals = CharacterRender.HumanoidIntervals();

            specialCeilings = CharacterRender.HumanoidCeilings();

            specialFloors = CharacterRender.HumanoidFloors();

            hatVectors = CharacterRender.HumanoidHats();

            WeaponLoadout(WeaponRender.weapons.estoc);

            specialFrames[specials.liftup] = new()
            {
                [0] = CharacterRender.RectangleHumanoidList(new(){

                    CharacterRender.humanoidFrames.jumpUp,
                    CharacterRender.humanoidFrames.jumpUp,
                }),
                [1] = CharacterRender.RectangleHumanoidList(new(){

                    CharacterRender.humanoidFrames.jumpRight,
                    CharacterRender.humanoidFrames.jumpRight,
                }),
                [2] = CharacterRender.RectangleHumanoidList(new(){

                    CharacterRender.humanoidFrames.jumpDown,
                    CharacterRender.humanoidFrames.jumpDown,
                }),
                [3] = CharacterRender.RectangleHumanoidList(new(){

                    CharacterRender.humanoidFrames.jumpLeft,
                    CharacterRender.humanoidFrames.jumpLeft,
                }),
            };

            specialIntervals[specials.liftup] = 120;

            specialCeilings[specials.liftup] = 1;

            specialFloors[specials.liftup] = 0;

            setScale = 3.5f;

            loadedOut = true;

        }

        public override bool MonsterFear()
        {

            return false;

        }

        public override void ClearSpecial()
        {

            base.ClearSpecial();

            fadeSet = 1f;

            if (reapparate != Vector2.Zero)
            {

                Position = reapparate;

                reapparate = Vector2.Zero;

            }

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            LookAtTarget(monster.Position);

            netSpecial.Set((int)specials.liftup);

            specialTimer = 240;

            fadeOut = 0.6f;

            fadeSet = 0.01f;

            reapparate = monster.Position;

            SetCooldown(60, 1f);

            int delay = 120;

            List<int> directions = new() { 0, 1, 2, 3, 4, 5, 6, 7, };

            int s = Mod.instance.randomIndex.Next(3);

            for (int i = 3; i >= 0; i--)
            {

                float strikeDamage = -1f;

                if (i == 0)
                {

                    strikeDamage = 3 * CombatDamage();

                }

                SpellHandle sweep = new(Game1.player, new() { monster }, strikeDamage)
                {
                    type = SpellHandle.Spells.warpstrike,

                    counter = 0 - delay
                };

                int d = directions[Mod.instance.randomIndex.Next(directions.Count)];

                directions.Remove(d);

                sweep.displayFactor = d;

                sweep.damageRadius = 128;

                sweep.display = IconData.impacts.flashbang;

                sweep.sound = SpellHandle.Sounds.swordswipe;

                sweep.scheme = IconData.schemes.darkgray;

                Mod.instance.spellRegister.Add(sweep);

                delay += 18;

            }

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