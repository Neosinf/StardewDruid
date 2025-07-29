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

namespace StardewDruid.Character
{
    public class Wizard : StardewDruid.Character.Character
    {
        public Wizard()
        {
        }

        public Wizard(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Wizard;

            }

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Wizard);

            if (Portrait == null)
            {

                Portrait = CharacterHandle.CharacterPortrait(CharacterHandle.characters.Wizard);

            }

            LoadIntervals();

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            specialIntervals = CharacterRender.HumanoidIntervals();

            specialCeilings = CharacterRender.HumanoidCeilings();

            specialFloors = CharacterRender.HumanoidFloors();

            hatVectors = CharacterRender.HumanoidHats();

            WeaponLoadout(WeaponRender.weapons.starsword);

            hatSelect = 5;

            loadedOut = true;

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

                displayFactor = 2
            };

            switch (Mod.instance.randomIndex.Next(3))
            {
                case 0:

                    special.display = IconData.impacts.puff;
                    break;

                case 1:

                    special.added = new() { SpellHandle.Effects.blackhole, };

                    break;

                default:

                    special.display = IconData.impacts.flasher;

                    special.added = new() { SpellHandle.Effects.doom, };

                    break;

            }

            Mod.instance.spellRegister.Add(special);

            return true;

        }


    }

}
