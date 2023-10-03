using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace StardewDruid.Cast
{
    internal class WoodsStatue : Cast
    {

        public StardewDruid.Cast.Portal monsterPortal;

        public NPC fakeNPC;

        public int activeCounter;

        public WoodsStatue(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

        }

        public override void CastWater()
        {

            activeCounter = 0;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 60;

            monsterPortal = new(mod, targetVector, riteData);

            monsterPortal.spawnFrequency = 1;

            monsterPortal.specialType = 6;

            Vector2 portalWithin = new(Math.Max(targetVector.X -3, 0), targetVector.Y +1);

            monsterPortal.portalWithin = portalWithin;

            monsterPortal.portalRange = new Vector2(8,8);

            monsterPortal.baseVector = targetVector + new Vector2(0, 2);

            Woods woodsLocation = riteData.castLocation as Woods;

            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(10, new Vector2(8f, 7f) * 64f, Color.White, 9, flipped: false, 50f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(10, new Vector2(9f, 7f) * 64f, Color.Orange, 9, flipped: false, 70f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(10, new Vector2(8f, 6f) * 64f, Color.White, 9, flipped: false, 60f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(10, new Vector2(9f, 6f) * 64f, Color.OrangeRed, 9, flipped: false, 120f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(10, new Vector2(8f, 5f) * 64f, Color.Red, 9));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(10, new Vector2(9f, 5f) * 64f, Color.White, 9, flipped: false, 170f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(11, new Vector2(544f, 464f), Color.Orange, 9, flipped: false, 40f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(11, new Vector2(608f, 464f), Color.White, 9, flipped: false, 90f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(11, new Vector2(544f, 400f), Color.OrangeRed, 9, flipped: false, 190f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(11, new Vector2(608f, 400f), Color.White, 9, flipped: false, 80f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(11, new Vector2(544f, 336f), Color.Red, 9, flipped: false, 69f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(11, new Vector2(608f, 336f), Color.OrangeRed, 9, flipped: false, 130f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(10, new Vector2(480f, 464f), Color.Orange, 9, flipped: false, 40f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(11, new Vector2(672f, 368f), Color.White, 9, flipped: false, 90f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(10, new Vector2(480f, 464f), Color.Red, 9, flipped: false, 30f));
            woodsLocation.temporarySprites.Add(new TemporaryAnimatedSprite(11, new Vector2(672f, 368f), Color.White, 9, flipped: false, 180f));
            woodsLocation.localSound("secret1");
            woodsLocation.map.GetLayer("Front").Tiles[8, 6].TileIndex = 1117;
            woodsLocation.map.GetLayer("Front").Tiles[9, 6].TileIndex = 1118;

            castFire = true;

            castCost = 24;

            castActive = true;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            return;

        }

        public override bool CastActive(int castIndex, int castLimit)
        {

            if (expireTime >= Game1.currentGameTime.TotalGameTime.TotalSeconds && targetPlayer.currentLocation == targetLocation)
            {

                return true;

            }

            return false;

        }

        public override void CastRemove()
        {

            Game1.stopMusicTrack(Game1.MusicContext.Default);

            monsterPortal.CastRemove();

            if(fakeNPC != null)
            {

                targetLocation.characters.Remove(fakeNPC);

                fakeNPC = null;

            }

        }

        public override void CastTrigger()
        {

            activeCounter++;

            if (activeCounter < 10)
            {
                switch (activeCounter)
                {
                    case 1:

                        Monster fakeMonster = new DustSpirit(targetVector * 64f);

                        fakeMonster.setInvincibleCountdown(99999);

                        fakeMonster.IsInvisible = true;

                        targetLocation.characters.Add(fakeMonster);

                        fakeMonster.update(Game1.currentGameTime, targetLocation);

                        mod.MonsterTrack(targetLocation, fakeMonster);

                        fakeNPC = fakeMonster;

                        fakeNPC.showTextAboveHead("do you sense it?", duration: 2000);

                        break;

                    case 4:

                        fakeNPC.showTextAboveHead("the dust", duration: 2000);

                        break;

                    case 7:

                        fakeNPC.showTextAboveHead("they're coming!", duration: 2000);

                        break;

                    case 9:

                        targetLocation.characters.Remove(fakeNPC);

                        fakeNPC = null;

                        Game1.changeMusicTrack("cowboy_outlawsong", false, Game1.MusicContext.Default);

                        break;

                    default:

                        targetLocation.playSound("dustMeep");

                        break;

                }
            }
            else if (activeCounter == 35)
            {

                Monster fakeMonster = new DustSpirit(targetVector * 64f);

                targetLocation.characters.Add(fakeMonster);

                fakeMonster.setInvincibleCountdown(99999);

                fakeMonster.IsInvisible = true;

                fakeMonster.update(Game1.currentGameTime, targetLocation);

                mod.MonsterTrack(targetLocation, fakeMonster);

                fakeNPC = fakeMonster;

                fakeNPC.showTextAboveHead("ha ha ha", duration: 2000);

            }
            else if (activeCounter == 38)
            {

                fakeNPC.showTextAboveHead("try these", duration: 2000);

                for(int i = 0; i < 2; i++)
                {
                    StardewDruid.Cast.Throw throwObject = new(288, 0);

                    throwObject.ThrowObject(targetPlayer, targetVector);

                }

            }
            else if (activeCounter == 40)
            {

                targetLocation.characters.Remove(fakeNPC);

                fakeNPC = null;

            }
            else if (activeCounter == 59)
            {

                Monster fakeMonster = new DustSpirit(targetVector * 64f);

                targetLocation.characters.Add(fakeMonster);

                fakeMonster.setInvincibleCountdown(99999);

                fakeMonster.IsInvisible = true;

                fakeMonster.update(Game1.currentGameTime, targetLocation);

                mod.MonsterTrack(targetLocation, fakeMonster);

                fakeNPC = fakeMonster;

                fakeNPC.showTextAboveHead("the dust settles", duration: 2000);

                StardewDruid.Cast.Throw throwObject = new(347, 0);

                throwObject.ThrowObject(targetPlayer, targetVector);

            }
            else
            {

                monsterPortal.CastTrigger();

                monsterPortal.CastTrigger();

            }

        }

    }

}
