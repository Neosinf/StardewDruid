using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
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
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using xTile.Dimensions;


namespace StardewDruid.Character
{
    public class Recruit : StardewDruid.Character.Character
    {

        public NPC villager;

        public Recruit()
        {
        }

        public Recruit(CharacterHandle.characters type, NPC Villager)
          : base(type, Villager)
        {

            villager = Villager;

        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.recruit_one;

            }

            if(villager == null)
            {

                villager = CharacterHandle.FindVillager(Name.Replace("18465_",""));

                Portrait = villager.Portrait;

            }

            LoadIntervals();

            gait = 1.6f;

            moveInterval = 12;

            characterTexture = Sprite.Texture;

            walkFrames = new()
            {
                [0] = new() { new(0, 64, 16, 32), new(16, 64, 16, 32), new(32, 64, 16, 32), new(48, 64, 16, 32), new(32, 64, 16, 32), },
                [1] = new() { new(0, 32, 16, 32), new(16, 32, 16, 32), new(32, 32, 16, 32), new(48, 32, 16, 32), new(32, 32, 16, 32), },
                [2] = new() { new(0, 0, 16, 32), new(16, 0, 16, 32), new(32, 0, 16, 32), new(48, 0, 16, 32), new(32, 0, 16, 32), },
                [3] = new() { new(0, 96, 16, 32), new(16, 96, 16, 32), new(32, 96, 16, 32), new(48, 96, 16, 32), new(32, 96, 16, 32), },
            };

            dashFrames[dashes.dash] = new()
            {
                [0] = new() { new(0, 64, 16, 32), new(16, 64, 16, 32), new(32, 64, 16, 32), },
                [1] = new() { new(0, 32, 16, 32), new(16, 32, 16, 32), new(32, 32, 16, 32), },
                [2] = new() { new(0, 0, 16, 32), new(16, 0, 16, 32), new(32, 0, 16, 32), },
                [3] = new() { new(0, 96, 16, 32), new(16, 96, 16, 32), new(32, 96, 16, 32), },
                [4] = new() { new(48, 64, 16, 32), },
                [5] = new() { new(48, 32, 16, 32), },
                [6] = new() { new(48, 0, 16, 32), },
                [7] = new() { new(48, 96, 16, 32), },
                [8] = new() { new(32, 64, 16, 32), },
                [9] = new() { new(32, 32, 16, 32), },
                [10] = new() { new(32, 0, 16, 32), },
                [11] = new() { new(32, 96, 16, 32), },
            };

            loadedOut = true;

        }

        public override void SetCooldown(int start = 0, float factor = 1)
        {

            float refactor = 1.6f;

            int experience = Mod.instance.save.recruits[characterType].level;

            if (refactor > 100)
            {

                refactor = 0.8f;


            } else if(refactor > 75)
            {

                refactor = 1f;

            }
            else if (refactor > 50)
            {


                refactor = 1.2f;


            }
            else if (refactor > 25)
            {

                refactor = 1.4f;

            }

            factor *= refactor;

            base.SetCooldown(start, factor);

        }

        public override int CombatDamage()
        {

            float damage = (float)Mod.instance.CombatDamage();

            float refactor = 0.7f;

            int experience = Mod.instance.save.recruits[characterType].level;

            if (refactor > 100)
            {

                refactor = 1.2f;


            }
            else if (refactor > 75)
            {

                refactor = 1f;

            }
            else if (refactor > 50)
            {


                refactor = 0.9f;


            }
            else if (refactor > 25)
            {

                refactor = 0.8f;
            }

            damage *= refactor;

            return (int)damage;

        }

        public override bool TargetMonster()
        {

            if (!MonsterFear())
            {

                return base.TargetMonster();

            }

            List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(currentLocation, Position, 640f, true);

            if (monsters.Count > 0)
            {

                if (cooldownTimer == 0)
                {

                    doEmote(8);

                    cooldownTimer = 720;

                }

            }

            return false;

        }

        public virtual bool MonsterFear()
        {

            return true;

        }
        public override bool tryToReceiveActiveObject(Farmer who, bool probe = false)
        {

            return villager.tryToReceiveActiveObject(who, probe);

        }

        public override void receiveGift(Object o, Farmer giver, bool updateGiftLimitInfo = true, float friendshipChangeMultiplier = 1, bool showResponse = true)
        {

            villager.receiveGift(o, giver, updateGiftLimitInfo, friendshipChangeMultiplier, showResponse);

        }

        public override void normalUpdate(GameTime time, GameLocation location)
        {

            base.normalUpdate(time, location);

            if(modeActive == mode.recruit)
            {
                fellowship++;

                if (fellowship >= 6000)
                {

                    if (RecruitHandle.RecruitFriendship(villager))
                    {

                        ModUtility.ChangeFriendship(villager, 10);

                    }

                    RecruitHandle.LevelUpdate(characterType);

                    fellowship = 0;

                }

            }

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            RecruitHandle.LevelUpdate(characterType);

            return base.SpecialAttack(monster);

        }

        public virtual bool TrackNotReady()
        {

            if (villager.Name == Game1.player.spouse)
            {

                return false;

            }

            if (Game1.timeOfDay < 900)
            {

                return true;

            }

            return false;

        }

        public virtual bool TrackOutOfTime()
        {

            if (villager.Name == Game1.player.spouse)
            {

                return false;

            }

            if (Game1.timeOfDay > 1900)
            {

                return true;

            }

            return false;

        }

        public virtual bool TrackConflict(Farmer player)
        {

            if(villager.currentLocation.Name == player.currentLocation.Name)
            {

                return true;

            }

            return false;

        }

        public override void SwitchToMode(mode modechoice, Farmer player)
        {

            ResetActives();

            RemoveCompanionBuff(player);

            netSceneActive.Set(false);

            Mod.instance.trackers.Remove(characterType);

            switch (modechoice)
            {

                case mode.recruit:

                    if (!Context.IsMainPlayer)
                    {

                        if (!Mod.instance.dopplegangers.ContainsKey(characterType))
                        {


                            tether = CharacterHandle.RoamTether(currentLocation);

                            break;

                        }

                    }
                    else if (!Mod.instance.characters.ContainsKey(characterType))
                    {


                        tether = CharacterHandle.RoamTether(currentLocation);

                        break;

                    }

                    modeActive = modechoice;

                    Mod.instance.trackers[characterType] = new TrackHandle(characterType, player, trackQuadrant);

                    specialDisable = false;

                    CompanionBuff(player);

                    break;

                default:

                    RecruitHandle.RecruitRemove(characterType);

                    break;

            }

        }

    }

}
