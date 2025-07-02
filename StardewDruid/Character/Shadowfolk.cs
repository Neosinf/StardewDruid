using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Machines;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Character
{
    public class Shadowfolk : StardewDruid.Character.Character
    {

        public Shadowfolk()
        {
        }

        public Shadowfolk(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {
            
            base.LoadOut();

            idleFrames[idles.standby] = new(specialFrames[specials.sweep]);

            idleFrames[idles.alert] = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 288, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(224, 288, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(128, 288, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(160, 288, 32, 32),
                },
            };

            switch (characterType)
            {

                default:
                case CharacterHandle.characters.DarkShooter:

                    WeaponLoadout(WeaponRender.weapons.bazooka);

                    idleFrames[idles.kneel] = new()
                    {
                        [0] = new()
                        {
                            new Rectangle(128, 32, 32, 32),
                        },
                    };

                    break;

                case CharacterHandle.characters.DarkRogue:

                    WeaponLoadout(WeaponRender.weapons.estoc);

                    idleFrames[idles.kneel] = new()
                    {
                        [0] = new()
                        {
                            new Rectangle(128, 32, 32, 32),
                        },

                    };

                    break;

                case CharacterHandle.characters.DarkGoblin:

                    WeaponLoadout(WeaponRender.weapons.axe);

                    idleFrames[idles.kneel] = new()
                    {
                        [0] = new()
                        {
                            new Rectangle(128, 32, 32, 32),
                        },

                    };

                    break;

            }


        }

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 192, Mod.instance.CombatDamage() / 2)
            {
                instant = true,

                added = new() { SpellHandle.Effects.knock, },

                sound = SpellHandle.Sounds.swordswipe
            };

            Mod.instance.spellRegister.Add(swipeEffect);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 90;

            SetCooldown(specialTimer, 2f);

            LookAtTarget(monster.Position, true);

            SpellHandle fireball = new(Game1.player, monster.Position, 384, Mod.instance.CombatDamage() * 3)
            {
                origin = GetBoundingBox().Center.ToVector2(),

                counter = -30,

                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.rocket,

                display = IconData.impacts.impact,

                indicator = IconData.cursors.scope,

                factor = 3,

                scheme = IconData.schemes.stars,

                sound = SpellHandle.Sounds.explosion,

                added = new() { SpellHandle.Effects.embers, },

                power = 4,

                explosion = 4,

                terrain = 4
            };

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

    }

}
