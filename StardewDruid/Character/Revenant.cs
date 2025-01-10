using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
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
    public class Revenant : Character
    {

        public Revenant()
        {


        }

        public Revenant(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            base.LoadOut();

            WeaponLoadout();

            //weaponRender.swordScheme = IconData.schemes.stars;

            idleFrames[idles.standby] = new()
            {
                [0] = new(){ new(192, 0, 32, 32), },
                [1] = new() { new(192, 0, 32, 32), },
                [2] = new() { new(192, 0, 32, 32), },
                [3] = new() { new(192, 0, 32, 32), },
            };

            restSet = true;

            idleFrames[idles.rest] = new()
            {
                [0] = new() { new(0, 64, 32, 32), },

            };


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

            special.missile = MissileHandle.missiles.firefall;

            special.counter = -30;

            special.scheme = IconData.schemes.stars;

            special.factor = 2;

            special.power = 4;

            special.explosion = 4;

            special.terrain = 4;

            special.display = IconData.impacts.bomb;

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override mode SpecialMode(mode modechoice)
        {

            switch (modechoice)
            {

                case mode.home:

                case mode.random:

                case mode.roam:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.questRevenant))
                    {

                        return mode.limbo;

                    }

                    break;

            }

            return modechoice;

        }

    }

}
