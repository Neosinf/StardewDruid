using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Fates;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Characters;
using StardewValley.Monsters;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Buffin : Critter
    {

        public Buffin()
        {
        }

        public Buffin(CharacterHandle.characters type = CharacterHandle.characters.Buffin)
          : base(type)
        {

        }

        public override void LoadOut()
        { 
        
            base.LoadOut();

            setScale = 3.5f;

            restSet = true;

        }

        public override List<StardewValley.Monsters.Monster> FindMonsters()
        {

            return ModUtility.MonsterProximity(currentLocation, Position, 640f, true);

        }

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 192, Mod.instance.CombatDamage() / 2)
            {
                instant = true,

                sound = SpellHandle.Sounds.swordswipe,

                display = IconData.impacts.flashbang
            };

            swipeEffect.added.Add(SpellHandle.Effects.push);

            Mod.instance.spellRegister.Add(swipeEffect);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.questJester))
            {

                return false;

            }

            ResetActives();

            netSpecial.Set((int)specials.special);

            specialTimer = 90;

            SetCooldown(specialTimer, 1f);

            LookAtTarget(monster.Position, true);

            SpellHandle beam = new(Game1.player, monster.Position, 320, Mod.instance.CombatDamage() / 2)
            {
                origin = Position,

                type = SpellHandle.Spells.echo,

                missile = MissileHandle.missiles.curseecho
            };

            switch (Mod.instance.randomIndex.Next(6))
            {
                case 0:

                    beam.added.Add(SpellHandle.Effects.daze);
                    break;

                case 1:

                    beam.added.Add(SpellHandle.Effects.glare);
                    break;

                case 2:

                    beam.added.Add(SpellHandle.Effects.morph);
                    break;

                case 3:

                    beam.added.Add(SpellHandle.Effects.doom);
                    break;
            }

            Mod.instance.spellRegister.Add(beam);

            return true;

        }


    }

}
