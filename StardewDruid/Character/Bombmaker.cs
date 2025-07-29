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
    public class Bombmaker : StardewDruid.Character.Recruit
    {

        public Bombmaker()
        {
        }

        public Bombmaker(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Bombmaker;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Dwarf");

                Portrait = villager.Portrait;

            }

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Dwarf);

            LoadIntervals();

            setScale = 3.25f;

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            specialIntervals = CharacterRender.HumanoidIntervals();

            specialCeilings = CharacterRender.HumanoidCeilings();

            specialFloors = CharacterRender.HumanoidFloors();

            loadedOut = true;

        }

        public override bool MonsterFear()
        {

            return false;

        }

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 128, CombatDamage() / 2)
            {
                type = SpellHandle.Spells.swipe,

                added = new() { SpellHandle.Effects.push, },

                sound = SpellHandle.Sounds.swordswipe
            };

            Mod.instance.spellRegister.Add(swipeEffect);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.invoke);

            specialTimer = 90;

            SetCooldown(90, 1f);

            LookAtTarget(monster.Position, true);

            Vector2 missileFrom = GetBoundingBox().Center.ToVector2();

            SpellHandle special = new(currentLocation, monster.Position, missileFrom, 256, -1, CombatDamage() / 2)
            {
                instant = true,

                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.bomb,

                display = IconData.impacts.bomb,

            };

            special.added = new() { SpellHandle.Effects.explode };

            switch (Mod.instance.randomIndex.Next(4))
            {

                case 1:

                    special.added.Add(SpellHandle.Effects.embers);

                    if (Mod.instance.randomIndex.Next(6) == 0)
                    {

                        special.added.Add(SpellHandle.Effects.bore);

                    }

                    break;

                case 2:

                    special.added.Add(SpellHandle.Effects.jump);

                    break;

            }

            switch (Mod.instance.randomIndex.Next(5))
            {

                default:
                case 1: // cherry bomb

                    special.scheme = IconData.schemes.bomb_one;

                    special.sound = SpellHandle.Sounds.flameSpellHit;

                    special.added = new() { SpellHandle.Effects.explode };

                    special.effectRadius = 2;

                    Mod.instance.spellRegister.Add(special);

                    break;

                case 2:
                case 3: // bomb

                    special.scheme = IconData.schemes.bomb_two;

                    special.sound = SpellHandle.Sounds.explosion;

                    special.displayFactor = 3;

                    special.damageRadius = 320;

                    special.effectRadius = 4;

                    Mod.instance.spellRegister.Add(special);

                    break;

                case 4: // mega bomb

                    special.scheme = IconData.schemes.bomb_three;

                    special.sound = SpellHandle.Sounds.explosion;

                    special.displayFactor = 4;

                    special.damageRadius = 512;

                    special.added = new() { SpellHandle.Effects.explode };

                    special.effectRadius = 6;

                    special.damageMonsters = Mod.instance.CombatDamage();

                    Mod.instance.spellRegister.Add(special);

                    break;

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

        public override bool TrackConflict(Farmer player)
        {

            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Dwarf))
            {

                if (Mod.instance.characters[CharacterHandle.characters.Dwarf].currentLocation.Name == player.currentLocation.Name)
                {

                    return true;

                }

            }

            if (Mod.instance.activeEvent.Count > 0)
            {

                foreach (KeyValuePair<int, StardewDruid.Character.Character> character in Mod.instance.eventRegister[Mod.instance.activeEvent.First().Key].companions)
                {

                    if (character.Value is Dwarf)
                    {

                        return true;

                    }

                }

            }

            return base.TrackConflict(player);

        }

    }

}