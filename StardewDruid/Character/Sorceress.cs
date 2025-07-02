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
using System.Linq;

namespace StardewDruid.Character
{
    public class Sorceress : StardewDruid.Character.Recruit
    {

        public Sorceress()
        {
        }

        public Sorceress(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Sorceress;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Wizard");

                Portrait = villager.Portrait;

            }

            LoadIntervals();

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Witch);

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.WitchSpecial();

            specialIntervals = CharacterRender.WitchIntervals();

            specialCeilings = CharacterRender.WitchCeilings();

            specialFloors = CharacterRender.WitchFloors();

            hatVectors = CharacterRender.HumanoidHats();

            hatVectors[hats.launch] = hatVectors[hats.stand];

            hatSelect = 4;

            idleFrames[idles.standby] = new()
            {
                [0] = new List<Rectangle> { new Rectangle(160, 32, 32, 32), },
            };

            setScale = 3.5f;

            loadedOut = true;


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

            SetCooldown(specialTimer, 1f);

            LookAtTarget(monster.Position, true);

            SpellHandle glare = new(GetBoundingBox().Center.ToVector2(), 256, IconData.impacts.glare, new()) { scheme = IconData.schemes.fates, displayRadius = 3, };

            Mod.instance.spellRegister.Add(glare);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, CombatDamage() / 2)
            {
                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.frostball,

                counter = -24,

                scheme = IconData.schemes.ether,

                factor = 3,

                added = new() { SpellHandle.Effects.freeze, },

                display = IconData.impacts.puff
            };

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


        public override bool TrackConflict(Farmer player)
        {

            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Witch))
            {

                if (Mod.instance.characters[CharacterHandle.characters.Witch].currentLocation.Name == player.currentLocation.Name)
                {

                    return true;

                }

            }

            if (Mod.instance.activeEvent.Count > 0)
            {

                foreach (KeyValuePair<int, StardewDruid.Character.Character> character in Mod.instance.eventRegister[Mod.instance.activeEvent.First().Key].companions)
                {

                    if (character.Value is Witch)
                    {

                        return true;

                    }

                }

            }

            return base.TrackConflict(player);

        }

    }

}
