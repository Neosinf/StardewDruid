using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
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
    public class Sophia : StardewDruid.Character.Recruit
    {

        public Sophia()
        {
        }

        public Sophia(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Sophia;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Sophia");

                Portrait = villager.Portrait;

            }

            LoadIntervals();

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Sophia);

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.WitchSpecial();

            specialIntervals = CharacterRender.WitchIntervals();

            specialCeilings = CharacterRender.WitchCeilings();

            specialFloors = CharacterRender.WitchFloors();

            LoadOutKick();

            loadedOut = true;

            setScale = 3.75f;

        }

        public override bool MonsterFear()
        {

            return false;

        }

        public override bool EngageMonster(StardewValley.Monsters.Monster monster, float distance)
        {

            return PathTarget(Game1.player.Position, 2, 1);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 60;

            SetCooldown(40, 0.5f);

            Vector2 thisCenter = GetBoundingBox().Center.ToVector2();

            Vector2 monsterCenter = monster.GetBoundingBox().Center.ToVector2();

            Vector2 targetOne = monsterCenter + new Vector2(-16, -48);

            Vector2 targetTwo = monsterCenter + new Vector2(32, 64);

            Vector2 targetThree = monsterCenter + new Vector2(-64, 32);

            LookAtTarget(monsterCenter, true);

            SpellHandle special = new(currentLocation, targetOne, thisCenter, 192, -1, Mod.instance.CombatDamage()/4)
            {
                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.shuriken,

                scheme = IconData.schemes.fates

            };

            Mod.instance.spellRegister.Add(special);

            SpellHandle specialTwo = new(currentLocation, targetTwo, thisCenter, 192, -1, Mod.instance.CombatDamage() / 4)
            {
                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.shuriken,

                counter = -12,

                scheme = IconData.schemes.mists,

            };

            Mod.instance.spellRegister.Add(specialTwo);

            SpellHandle specialThree = new(currentLocation, targetThree, thisCenter, 192, -1, Mod.instance.CombatDamage() / 4)
            {
                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.shuriken,

                counter = -24,

                scheme = IconData.schemes.snazzle,

            };

            Mod.instance.spellRegister.Add(specialThree);

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
