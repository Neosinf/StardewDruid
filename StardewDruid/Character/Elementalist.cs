using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Character
{
    public class Elementalist : StardewDruid.Character.Recruit
    {
        public Elementalist()
        {
        }

        public Elementalist(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {

            
        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Elementalist;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Wizard");

                Portrait = villager.Portrait;

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

            hatVectors = CharacterRender.HumanoidHats();

            WeaponLoadout(WeaponRender.weapons.lightsaber);

            hatSelect = 5;

            loadedOut = true;

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

            SetCooldown(specialTimer, 1f);

            LookAtTarget(monster.Position, true);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, CombatDamage() / 2)
            {
                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.warpball,

                counter = -30,

                scheme = IconData.schemes.fates,

                factor = 2,

                display = IconData.impacts.puff,

                added = new() { SpellHandle.Effects.binds, }
            };

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override bool TrackNotReady()
        {

            if(villager.Name == Game1.player.spouse)
            {

                return false;

            }

            if (Game1.timeOfDay < 600)
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

            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Wizard))
            {

                if (Mod.instance.characters[CharacterHandle.characters.Wizard].currentLocation.Name == player.currentLocation.Name)
                {

                    return true;

                }

            }

            if (Mod.instance.activeEvent.Count > 0)
            {

                foreach (KeyValuePair<int, StardewDruid.Character.Character> character in Mod.instance.eventRegister[Mod.instance.activeEvent.First().Key].companions)
                {

                    if (character.Value is Wizard)
                    {

                        return true;

                    }

                }

            }

            return base.TrackConflict(player);

        }
    }

}
