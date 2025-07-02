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
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Archaeologist : StardewDruid.Character.Recruit
    {

        public Archaeologist()
        {
        }

        public Archaeologist(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Archaeologist;

            }

            if (villager == null)
            {

                string whichGunther = "Gunther";

                if (Mod.instance.Helper.ModRegistry.IsLoaded("FlashShifter.SVECode"))
                {

                    whichGunther = "GuntherSilvian";

                }

                villager = CharacterHandle.FindVillager(whichGunther);

            }

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Gunther);

            if (Portrait == null)
            {

                Portrait = villager.Portrait;

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

            WeaponLoadout(WeaponRender.weapons.estoc);

            idleFrames[idles.standby] = new()
            {
                [0] = new List<Rectangle> { new Rectangle(192, 0, 32, 32), },
            };

            hatSelect = 2;

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

            SetCooldown(90, 1f);

            LookAtTarget(monster.Position, true);

            List<int> intList = new List<int>()
            {
                96,
                97,
                98,
                99,
                100,
                101,
                103,
                104,
                105,
                106,
                107,
                108,
                109,
                110,
                111,
                112,
                113,
                114,
                115,
                116,
                117,
                118,
                119,
                120,
                121,
                122,
                123,
                124,
                125,
                126,
                127,
                579,
                580,
                581,
                582,
                583,
                584,
                585,
                586,
                587,
                588,
                589
            };

            ThrowHandle throwJunk = new(Position, monster.Position, intList[Mod.instance.randomIndex.Next(intList.Count)])
            {
                pocket = true
            };

            throwJunk.register();

            SpellHandle hit = new(Game1.player, Position, 192, CombatDamage() / 2)
            {
                instant = true,

                counter = -60
            };

            Mod.instance.spellRegister.Add(hit);

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

            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Gunther))
            {

                if (Mod.instance.characters[CharacterHandle.characters.Gunther].currentLocation.Name == player.currentLocation.Name)
                {

                    return true;

                }

            }

            if (Mod.instance.activeEvent.Count > 0)
            {

                foreach (KeyValuePair<int, StardewDruid.Character.Character> character in Mod.instance.eventRegister[Mod.instance.activeEvent.First().Key].companions)
                {

                    if (character.Value is Gunther)
                    {

                        return true;

                    }

                }

            }

            return base.TrackConflict(player);

        }
    }

}
