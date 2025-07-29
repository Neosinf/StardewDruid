using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Character
{
    public class Clint : StardewDruid.Character.Recruit
    {
        public Clint()
        {
        }

        public Clint(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {

            
        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Clint;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Clint");

                Portrait = villager.Portrait;

            }

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Clint);

            LoadIntervals();

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            specialIntervals = CharacterRender.HumanoidIntervals();

            specialCeilings = CharacterRender.HumanoidCeilings();

            specialFloors = CharacterRender.HumanoidFloors();

            WeaponLoadout(WeaponRender.weapons.hammer);

            specialFrames[specials.launch] = new()
            {
                [0] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
                [1] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
                [2] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
                [3] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
            };

            specialIntervals[specials.launch] = 15;
            specialCeilings[specials.launch] = 3;
            specialFloors[specials.launch] = 3;

            loadedOut = true;

        }
        public override bool MonsterFear()
        {

            return false;

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 90;

            SetCooldown(specialTimer, 1f);

            LookAtTarget(monster.Position, true);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, CombatDamage() / 2)
            {
                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.hammer,

                scheme = IconData.schemes.mists,

                counter = -30,

                displayFactor = 3,

                display = IconData.impacts.flashbang
            };

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override bool TargetWork()
        {

            List<Vector2> objectVectors = new List<Vector2>();

            for (int i = 0; i < 4; i++)
            {

                if (currentLocation.objects.Count() == 0)
                {
                    break;

                }

                objectVectors = ModUtility.GetTilesWithinRadius(currentLocation, occupied, i); ;

                foreach (Vector2 objectVector in objectVectors)
                {

                    if (currentLocation.objects.ContainsKey(objectVector))
                    {

                        if (ValidWorkTarget(currentLocation.objects[objectVector]))
                        {

                            if (PathTarget(workVector * 64, 2, 1))
                            {

                                pathActive = pathing.monster;

                                SetDash(workVector * 64, true);

                                SetCooldown(40, 0.5f);

                                workVector = objectVector;

                                specialTimer = 90;

                                return true;

                            }

                            return false;

                        }

                    }

                }

            }

            return false;

        }

        public bool ValidWorkTarget(StardewValley.Object targetObject)
        {

            if (targetObject.isForage())
            {

                return true;

            }

            if (
                targetObject.IsBreakableStone() ||
                targetObject.IsTwig() ||
                targetObject.QualifiedItemId == "(O)169" ||
                targetObject.IsWeeds() ||
                targetObject.Name.Contains("SupplyCrate") ||
                targetObject is BreakableContainer breakableContainer ||
                targetObject.QualifiedItemId == "(O)590" ||
                targetObject.QualifiedItemId == "(O)SeedSpot"
            )
            {

                return true;

            }

            return false;

        }

        public override void PerformWork()
        {

            if (specialTimer != 30)
            {

                return;

            }

            /*SpellHandle explode = new(Game1.player, workVector * 64, 128, -1)
            {
                type = SpellHandle.Spells.explode,

                indicator = IconData.cursors.none
            };

            if (currentLocation.objects[workVector].IsBreakableStone())
            {

                explode.sound = SpellHandle.Sounds.hammer;

            }

            explode.power = 2;

            explode.terrain = 2;

            Mod.instance.spellRegister.Add(explode);*/

        }

        public override bool TrackNotReady()
        {

            if(villager.Name == Game1.player.spouse)
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
