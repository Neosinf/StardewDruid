using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Locations;
using System;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Event.Boss
{
    public class SandDragon : BossHandle
    {

        public bool modifiedSandDragon;

        //public BossDragon bossMonster;
        public Monster.Boss bossMonster;

        public Vector2 returnPosition;

        public SandDragon(Vector2 target, Rite rite, Quest quest)
            : base(target, rite, quest)
        {

            targetVector = target;

            voicePosition = targetVector * 64 + new Vector2(0, -32);

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 120;

            returnPosition = rite.caster.Position;

        }

        public override void EventTrigger()
        {

            ModUtility.AnimateRadiusDecoration(targetLocation, targetVector, "Stars", 1f, 1f);

            ModUtility.AnimateMeteor(targetLocation, targetVector, true);

            base.EventTrigger();

        }

        public override void RemoveMonsters()
        {

            if (bossMonster != null)
            {

                riteData.castLocation.characters.Remove(bossMonster);

                bossMonster = null;

            }

            base.RemoveMonsters();

        }

        public override void EventRemove()
        {

            ResetSandDragon();

            base.EventRemove();

        }

        public override bool EventExpire()
        {

            if (eventLinger == -1)
            {

                RemoveMonsters();

                ResetSandDragon();

                eventLinger = 3;

                return true;

            }

            if (eventLinger == 2)
            {

                if (expireEarly)
                {

                    CastVoice("the power of the shamans lingers");

                    Vector2 debrisVector = Game1.player.getTileLocation() + new Vector2(0, 1);

                    if (!questData.name.Contains("Two"))
                    {

                        Game1.createObjectDebris(74, (int)debrisVector.X, (int)debrisVector.Y);

                    }

                    Game1.createObjectDebris(681, (int)debrisVector.X, (int)debrisVector.Y);

                    Mod.instance.CompleteQuest(questData.name);

                }
                else
                {

                    CastVoice("You're no match for me");

                    Mod.instance.CastMessage("Try again tomorrow");

                }

            }

            return base.EventExpire();

        }

        public override void EventInterval()
        {

            activeCounter++;

            if (eventLinger != -1)
            {

                return;

            }

            if (activeCounter < 9)
            {

                switch (activeCounter)
                {
                    case 1:

                        CastVoice("a taste of the stars");

                        break;

                    case 3:

                        CastVoice("from the time when the shamans sang to us");

                        break;

                    case 5:

                        CastVoice("and my kin held dominion");

                        break;

                    case 7:

                        CastVoice("...my bones stir...");

                        break;

                    default:

                        Vector2 randomVector = targetVector + new Vector2(0, 1) - new Vector2(randomIndex.Next(7), randomIndex.Next(3));

                        ModUtility.AnimateRadiusDecoration(targetLocation, randomVector, "Stars", 1f, 1f);

                        ModUtility.AnimateMeteor(targetLocation, randomVector, randomIndex.Next(2) == 0);

                        break;

                }

                return;

            }

            if (activeCounter == 9)
            {

                ModifySandDragon();

                StardewValley.Monsters.Monster theMonster = MonsterData.CreateMonster(16, targetVector + new Vector2(-5, 0), riteData.combatModifier);

                bossMonster = theMonster as Monster.Boss;

                if (questData.name.Contains("Two"))
                {

                    bossMonster.HardMode();

                }

                riteData.castLocation.characters.Add(bossMonster);

                bossMonster.currentLocation = targetLocation;

                bossMonster.update(Game1.currentGameTime, targetLocation);

                SetTrack("cowboy_boss");

                return;

            }

            if (activeCounter > 11 && !ModUtility.MonsterVitals(bossMonster, targetLocation))
            {

                expireEarly = true;

            }

        }

        public void ResetSandDragon()
        {

            if (!modifiedSandDragon)
            {

                return;

            }

            targetLocation.loadMap(targetLocation.mapPath.Value, true);

            modifiedSandDragon = false;

            if (Game1.eventUp || Game1.fadeToBlack || Game1.currentMinigame != null || Game1.isWarping || Game1.killScreen || Game1.player.currentLocation is not Desert)
            {
                return;

            }

            Game1.fadeScreenToBlack();

            targetPlayer.Position = returnPosition;

            if (soundTrack)
            {

                Game1.stopMusicTrack(Game1.MusicContext.Default);

                soundTrack = false;

            }

        }

        public void ModifySandDragon()
        {

            modifiedSandDragon = true;

            // ----------------------------- clear sheet

            Layer backLayer = targetLocation.map.GetLayer("Back");

            Layer buildingsLayer = targetLocation.map.GetLayer("Buildings");

            Layer frontLayer = targetLocation.map.GetLayer("Front");

            Layer alwaysfrontLayer = targetLocation.map.GetLayer("AlwaysFront");

            TileSheet desertSheet = targetLocation.map.GetTileSheet("desert-new");

            Vector2 offsetVector = targetVector - new Vector2(8, 5);

            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 10; j++)
                {

                    Vector2 tileVector = offsetVector + new Vector2(j, i);

                    if (buildingsLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] != null)
                    {

                        int tileIndex = buildingsLayer.Tiles[(int)tileVector.X, (int)tileVector.Y].TileIndex;

                        if (tileIndex < 192 || tileIndex == 219)
                        {
                            buildingsLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = null;

                        }

                    }

                    if (frontLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] != null)
                    {

                        int tileIndex = frontLayer.Tiles[(int)tileVector.X, (int)tileVector.Y].TileIndex;

                        if (tileIndex < 192 || tileIndex == 203)
                        {
                            frontLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = null;

                        }

                    }

                    if (alwaysfrontLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] != null)
                    {

                        int tileIndex = alwaysfrontLayer.Tiles[(int)tileVector.X, (int)tileVector.Y].TileIndex;

                        if (tileIndex < 192)
                        {
                            alwaysfrontLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = null;

                        }

                    }


                    if (randomIndex.Next(2) != 0)
                    {
                        backLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = new StaticTile(backLayer, desertSheet, BlendMode.Alpha, 65);
                    }
                    else if (randomIndex.Next(3) == 0)
                    {
                        backLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = new StaticTile(backLayer, desertSheet, BlendMode.Alpha, 96);
                    }
                    else if (randomIndex.Next(3) == 0)
                    {
                        backLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = new StaticTile(backLayer, desertSheet, BlendMode.Alpha, 97);
                    }
                    else
                    {
                        backLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = new StaticTile(backLayer, desertSheet, BlendMode.Alpha, 98);
                    }

                }

            }

        }


    }
}
