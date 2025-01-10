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
    public class HonourGuard : Character
    {

        public HonourGuard()
        {


        }

        public HonourGuard(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            base.LoadOut();

            fadeOut = 0.75f;

            WeaponLoadout();

        }

        public override void behaviorOnFarmerPushing()
        {

            return;

        }

        public override bool checkAction(Farmer who, GameLocation l)
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

            SpellHandle fireball = new(Game1.player, new() { monster, }, Mod.instance.CombatDamage() / 2);

            fireball.origin = GetBoundingBox().Center.ToVector2();

            fireball.type = SpellHandle.spells.missile;

            fireball.factor =3;

            fireball.missile = MissileHandle.missiles.knife;

            fireball.display = IconData.impacts.impact;

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

    }

}
