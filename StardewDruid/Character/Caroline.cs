using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Render;
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

namespace StardewDruid.Character
{
    public class Caroline : StardewDruid.Character.Recruit
    {

        public Caroline()
        {
        }

        public Caroline(CharacterHandle.characters type, NPC villager)
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

                villager = CharacterHandle.FindVillager("Caroline");

            }

            LoadIntervals();

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Caroline);

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.WitchSpecial();

            specialIntervals = CharacterRender.WitchIntervals();

            specialCeilings = CharacterRender.WitchCeilings();

            specialFloors = CharacterRender.WitchFloors();

            loadedOut = true;

            setScale = 3.5f;

            idleFrames[idles.standby] = new()
            {
                [0] = new List<Rectangle> { new Rectangle(160, 32, 32, 32), },
            };

        }

        public override bool MonsterFear()
        {

            return false;

        }

        public override bool SmashAttack(StardewValley.Monsters.Monster monster)
        {

            return SpecialAttack(monster);

        }

        public override bool SweepAttack(StardewValley.Monsters.Monster monster)
        {

            return SpecialAttack(monster);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 48;

            cooldownTimer = cooldownInterval;

            cooldownTimer = cooldownInterval;

            LookAtTarget(monster.Position, true);

            SpellHandle glare = new(GetBoundingBox().Center.ToVector2(), 256, IconData.impacts.glare, new()) { scheme = IconData.schemes.mists };

            Mod.instance.spellRegister.Add(glare);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 2);

            special.type = SpellHandle.spells.missile;

            special.missile = MissileHandle.missiles.frostball;

            special.counter = -24;

            special.scheme = IconData.schemes.ether;

            special.factor = 2;

            special.added = new() { SpellHandle.effects.freeze, };

            special.display = IconData.impacts.puff;

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
