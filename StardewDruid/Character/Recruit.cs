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
using System.Collections.Generic;
using System.Linq;
using xTile.Dimensions;


namespace StardewDruid.Character
{
    public class Recruit : StardewDruid.Character.Character
    {

        public NPC villager;

        public int fellowship;

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

            }

            LoadIntervals();

            gait = 1.6f;

            moveInterval = 12;

            characterTexture = Sprite.Texture;

            if (characterType == CharacterHandle.characters.Dwarf)
            {

                walkFrames = new()
                {
                    [0] = new() { new(0, 64, 16, 24), new(16, 64, 16, 24), new(32, 64, 16, 24), new(48, 64, 16, 24), new(32, 64, 16, 24), },
                    [1] = new() { new(0, 32, 16, 24), new(16, 24, 16, 24), new(32, 32, 16, 24), new(48, 32, 16, 24), new(32, 32, 16, 24), },
                    [2] = new() { new(0, 0, 16, 24), new(16, 0, 16, 24), new(32, 0, 16, 24), new(48, 0, 16, 24), new(32, 0, 16, 24), },
                    [3] = new() { new(0, 96, 16, 24), new(16, 96, 16, 24), new(32, 96, 16, 24), new(48, 96, 16, 24), new(32, 96, 16, 24), },
                };

                dashFrames[dashes.dash] = new()
                {
                    [0] = new() { new(0, 64, 16, 24), new(16, 64, 16, 24), new(32, 64, 16, 24), },
                    [1] = new() { new(0, 32, 16, 24), new(16, 24, 16, 24), new(32, 32, 16, 24), },
                    [2] = new() { new(0, 0, 16, 24), new(16, 0, 16, 24), new(32, 0, 16, 24), },
                    [3] = new() { new(0, 96, 16, 24), new(16, 96, 16, 24), new(32, 96, 16, 24), },
                    [4] = new() { new(48, 64, 16, 24), },
                    [5] = new() { new(48, 32, 16, 24), },
                    [6] = new() { new(48, 0, 16, 24), },
                    [7] = new() { new(48, 96, 16, 24), },
                    [8] = new() { new(32, 64, 16, 24), },
                    [9] = new() { new(32, 32, 16, 24), },
                    [10] = new() { new(32, 0, 16, 24), },
                    [11] = new() { new(32, 96, 16, 24), },
                };

            }
            else
            {
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

            }

            loadedOut = true;

        }

        public override bool TargetMonster()
        {

            if (!MonsterFear())
            {

                return base.TargetMonster();

            }

            List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(currentLocation, new() { Position, }, 640f, true);

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

        public override void update(GameTime time, GameLocation location)
        {
            
            base.update(time, location);

            fellowship++;

            if (fellowship >= 6000)
            {

                ModUtility.ChangeFriendship(Game1.player, villager, 10);

                fellowship = 0;

            }

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

    }

}
