using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Event
{
    internal class Figure : StardewDruid.Cast.CastHandle
    {

        public Portal monsterPortal;

        public NPC disembodiedVoice;

        public int activeCounter;

        public Quest questData;

        public Vector2 voiceOffset;

        public List<Vector2> cannonTargets;

        bool endEarly;

        bool completeChallenge;

        int finalCount;

        public Figure(Mod mod, Vector2 target, Rite rite, Quest quest)
            : base(mod, target, rite)
        {
            questData = quest;

            voiceOffset = new(0);

            cannonTargets = new();

            endEarly = false;

        }

        public override void CastQuest()
        {

            switch (questData.name)
            {

                case "figureCanoli":

                    CastCanoli();

                    break;

                case "figureMariner":

                    CastMariner();

                    break;

                default: // figureSandDragon

                    CastSandDragon();

                    break;

            }

        }

        public void CastCanoli()
        {

            targetVector = questData.vectorList["tileVector"];

            voiceOffset = new Vector2(-8, -56);

            activeCounter = 0;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 60;

            monsterPortal = new(mod, targetVector, riteData);

            monsterPortal.spawnFrequency = 1;

            monsterPortal.spawnIndex = new() { 9, };

            Vector2 portalWithin = new(Math.Max(targetVector.X - 3, 0), targetVector.Y + 1);

            monsterPortal.portalWithin = portalWithin;

            monsterPortal.portalRange = new Vector2(8, 8);

            monsterPortal.baseVector = targetVector + new Vector2(0, 2);

            StardewValley.Locations.Woods woodsLocation = riteData.castLocation as StardewValley.Locations.Woods;

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

            mod.ActiveCast(this);

            ModUtility.AnimateBolt(targetLocation, targetVector + new Vector2(0,-1));

        }

        public void CastMariner()
        {

            targetVector = questData.vectorList["specialVector"];

            voiceOffset = new Vector2(-10, -56);

            activeCounter = 0;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 60;

            monsterPortal = new(mod, targetVector, riteData);

            monsterPortal.spawnFrequency = 1; // control portal per cast

            monsterPortal.spawnIndex = new() { 8, 11 };

            //Vector2 portalWithin = new(Math.Max(((int)targetVector.X - 6), 0), (int)targetVector.Y + 1);

            //monsterPortal.portalWithin = portalWithin;

            monsterPortal.portalWithin = targetVector + new Vector2(-5, 1);

            monsterPortal.portalRange = new Vector2(11, 11);

            monsterPortal.baseVector = targetVector + new Vector2(0, 3);

            //monsterPortal.spawnAnimation = "bolt";

            mod.ActiveCast(this);

            ModUtility.AnimateBolt(targetLocation, targetVector);

        }

        public void CastSandDragon()
        {

            targetVector = questData.vectorList["actionVector"];

            voiceOffset = new Vector2(0, -32);

            activeCounter = 0;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 100;

            monsterPortal = new(mod, targetVector, riteData);

            monsterPortal.spawnFrequency = 1; // control portal per cast

            monsterPortal.spawnIndex = new() { 10, };

            monsterPortal.portalWithin = targetVector + new Vector2(-6, -1);

            monsterPortal.portalRange = new Vector2(3, 3);

            monsterPortal.baseVector = targetVector;

            mod.ActiveCast(this);

            ModUtility.AnimateMeteorZone(targetLocation, targetVector, Color.Red, 2);

            ModUtility.AnimateMeteor(targetLocation, targetVector, true);

        }

        public override bool CastActive(int castIndex, int castLimit)
        {

            if (endEarly)
            {
                completeChallenge = true;

                return false;

            }

            if (expireTime >= Game1.currentGameTime.TotalGameTime.TotalSeconds && targetPlayer.currentLocation == targetLocation)
            {

                completeChallenge = true;

                return true;

            }

            return false;

        }

        public override void CastRemove()
        {

            Game1.stopMusicTrack(Game1.MusicContext.Default);

            finalCount = monsterPortal.monsterSpawns.Count;

            monsterPortal.CastRemove();

            switch (questData.name)
            {
                case "figureCanoli":

                    RewardCanoli();

                    break;

                case "figureMariner":

                    RewardMariner();

                    break;

                default:

                    ResetSandDragon();

                    DelayedAction.functionAfterDelay(RewardSandDragon, 2000);

                    break;

            }

        }

        public override void CastTrigger()
        {
            activeCounter++;

            switch (questData.name)
            {
                case "figureCanoli":

                    TriggerCanoli();

                    break;

                case "figureMariner":

                    TriggerMariner();

                    break;

                default:

                    TriggerSandDragon();

                    break;

            }
            
        }

        public void TriggerCanoli()
        {

            if (activeCounter < 8)
            {
                switch (activeCounter)
                {
                    case 1:

                        CastVoice("can you feel it");

                        break;

                    case 3:

                        CastVoice("all around us");

                        break;

                    case 5:

                        CastVoice("THE DUST RISES");

                        break;

                    case 7:

                        Game1.changeMusicTrack("cowboy_outlawsong", false, Game1.MusicContext.Default);

                        break;

                    default:

                        targetLocation.playSound("dustMeep");

                        break;

                }
            }
            else if (activeCounter == 35)
            {

                CastVoice("ha ha ha");

            }
            else if (activeCounter == 38)
            {

                CastVoice("dust them");

                for (int i = 0; i < 3; i++)
                {
                    Throw throwObject = new(288, 0);

                    throwObject.ThrowObject(targetPlayer, targetVector);

                }

            }
            else if(activeCounter <= 55)
            {

                monsterPortal.CastTrigger();

                monsterPortal.CastTrigger();

            }
            else
            { 
                
                monsterPortal.CheckSpawns(); 
            
            }
            
        }

        public void RewardCanoli()
        {

            if(Game1.player.currentLocation != targetLocation)
            {
                return;
            
            }

            if (finalCount <= 10 && completeChallenge)
            {

                CastVoice("the dust settles");

                Throw throwObject = new(347, 0);

                throwObject.ThrowObject(targetPlayer, targetVector);
            }
            else
            {

                CastVoice("dust overwhelming");

            }

        }

        public void TriggerMariner()
        {

            if (activeCounter < 7)
            {
                switch (activeCounter)
                {
                    case 1:

                        CastVoice("oi matey!");

                        break;

                    case 3:

                        CastVoice("ya dare wield the Lady's power here?");

                        break;

                    case 5:

                        CastVoice("the deep one take you!");

                        break;

                    case 6:

                        Game1.changeMusicTrack("PIRATE_THEME", false, Game1.MusicContext.Default);

                        break;

                    default:

                        targetLocation.playSound("thunder_small");

                        break;

                }

                return;
            }

            if (activeCounter <= 56) { monsterPortal.CastTrigger(); } else { monsterPortal.CheckSpawns(); }

            /*"There's nay a way to modify me greatness",
            "I've been here chatting every minute,",
            "Of every day for months!",
            "My friend gave me the old Mariner role!",
            "Now I can perpetuate the same dumb interjections,",
            "to every newb that walks over here!",
            "!WherePendant",
            "You're not ready for it",
            "!GetLost",
            "What do you mean my feet aren't natural?",
            "No one asked for your critique, feet hater!",
            "Making a fuss for attention is expected here",
            "It's the only thing I know I'm good at",
            "I don't care if I asked you to comment! Bigot!",
            "I'm not much of a fighter myself.",
            "Pr'fer to just ban folks I don't agree wid.",*/

            switch (activeCounter)
            {

                case 12: CastVoice("ya can't touch me! I be a reflection!", 3000); break;

                case 15: CastVoice("this ere beach is for private members!", 3000); break;

                case 20: CannonsAtTheReady(); break;

                case 27: CastVoice("the Lady is not a friend to the drowned"); break;

                case 29: CastVoice("she buried us with our boats on this shore"); break;

                case 31: CastVoice("but soon Lord Deep will avenge us!"); break;

                case 33: CastVoice("he'll swallow the ol' sea 'ag whole"); break;

                case 35: CastVoice("then the waves will no longer wash our tattered bones"); break;

                case 37: CastVoice("an we'll sink into the warm embrace of the earth"); break;

                case 40: CannonsAtTheReady(); break;

                default: break;

            }

        }

        public void RewardMariner()
        {

            if (Game1.player.currentLocation != targetLocation)
            {
                return;

            }

            if (finalCount <= 9 && completeChallenge)
            {
                CastVoice("eh. take this an sod off.");

                List<int> objectIndexes = new()
                        {
                            265,
                            275,
                            797,
                            166,
                        };

                Throw throwObject = new(objectIndexes[randomIndex.Next(objectIndexes.Count)], 0);

                throwObject.ThrowObject(targetPlayer, targetVector);

            }
            else
            {

                CastVoice("haha! not good enough lad");

            }

        }

        public void CannonsAtTheReady() 
        {

            CastVoice("CANNONS AT THE READY!",3000);

            cannonTargets = new()
            {
                monsterPortal.portalWithin + new Vector2(1,2),

                monsterPortal.portalWithin + new Vector2(7,2),

                monsterPortal.portalWithin + new Vector2(13,2),

                monsterPortal.portalWithin + new Vector2(4,7),

                monsterPortal.portalWithin + new Vector2(4,7),

                monsterPortal.portalWithin + new Vector2(1,12),

                monsterPortal.portalWithin + new Vector2(7,12),

                monsterPortal.portalWithin + new Vector2(13,12),

            };

            foreach(Monster monsterSpawn in monsterPortal.monsterSpawns)
            {
                if(monsterSpawn is PirateSkeleton pirateSkeleton)
                {
                    pirateSkeleton.triggerPanic();

                } else if(monsterSpawn is PirateGolem pirateGolem)
                {

                    pirateGolem.triggerPanic();

                }
                
            }

            foreach (Vector2 cannonTarget in cannonTargets)
            {

                ModUtility.AnimateMeteorZone(targetLocation, cannonTarget, Color.Red*0.9f, 3, 6, 1.25f);
                ModUtility.AnimateMeteorZone(targetLocation, cannonTarget, Color.Red*0.8f, 2, 6, 1f);
                ModUtility.AnimateMeteorZone(targetLocation, cannonTarget, Color.Red*0.7f, 1, 6, 0.75f);
            }

            DelayedAction.functionAfterDelay(CannonsToFire, 3600);

        }

        public void CannonsToFire() 
        {
            
            CastVoice("FIRE!");

            targetLocation.localSound("explosion");

            foreach (Vector2 cannonTarget  in cannonTargets)
            {

                targetLocation.explode(cannonTarget,3,targetPlayer,true,targetPlayer.CombatLevel*targetPlayer.CombatLevel);

            }

            cannonTargets.Clear();

        }

        public void TriggerSandDragon()
        {

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

                        CastVoice("...now my bones stir...");

                        break;

                    default:

                        Vector2 randomVector = targetVector + new Vector2(0,1) - new Vector2(randomIndex.Next(7), randomIndex.Next(3));

                        ModUtility.AnimateMeteorZone(targetLocation, randomVector, Color.Red, 2);

                        ModUtility.AnimateMeteor(targetLocation, randomVector, randomIndex.Next(2) == 0);

                        break;

                }

                return;

            }

            if (activeCounter == 9)
            {
                ModifySandDragon();

                monsterPortal.CastTrigger();

                Game1.changeMusicTrack("cowboy_boss", false, Game1.MusicContext.Default);

                return;

            }

            if (!monsterPortal.CheckSpawns())
            {

                endEarly = true;

            }

        }

        public void ModifySandDragon()
        {

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


                    if (randomIndex.Next(4) != 0)
                    {
                        backLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = new StaticTile(backLayer, desertSheet, BlendMode.Alpha, 65);
                    }
                    else if (randomIndex.Next(5) == 0)
                    {
                        backLayer.Tiles[(int)tileVector.X, (int)tileVector.Y] = new StaticTile(backLayer, desertSheet, BlendMode.Alpha, 96);
                    }
                    else if (randomIndex.Next(5) == 0)
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

        public void ResetSandDragon()
        {

            targetLocation.loadMap(targetLocation.mapPath.Value,true);

            if (Game1.eventUp || Game1.fadeToBlack || Game1.currentMinigame != null || Game1.isWarping || Game1.killScreen || Game1.player.currentLocation is not Desert)
            {
                return;

            }

            Game1.fadeScreenToBlack();

            targetPlayer.Position = targetVector * 64 - new Vector2(0,64);

        }

        public void RewardSandDragon()
        {

            if(Game1.player.currentLocation is not Desert)
            {
                return;

            }

            if (finalCount == 0 && completeChallenge)
            {
                
                CastVoice("the power of the shamans lingers still");

                Vector2 debrisVector = Game1.player.getTileLocation() + new Vector2(0, 1);

                Dictionary<string, int> blessingList = mod.BlessingList();

                if (!blessingList.ContainsKey("shardSandDragon"))
                {

                    Game1.createObjectDebris(74, (int)debrisVector.X, (int)debrisVector.Y);

                    mod.UpdateBlessing("shardSandDragon");

                }

                Game1.createObjectDebris(681, (int)debrisVector.X, (int)debrisVector.Y);

            }
            else
            {

                CastVoice("return when you have strength to reveal");

            }

        }

        public void CastVoice(string message, int duration = 2000)
        {   

            if (disembodiedVoice == null)
            {

                disembodiedVoice = mod.RetrieveVoice(targetLocation, targetVector * 64 + voiceOffset);

            }

            disembodiedVoice.showTextAboveHead(message, duration: duration);

        }

    }

}
