using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast
{
    internal class Portal : Cast
    {

        public TemporaryAnimatedSprite portalAnimation;

        public LightSource portalLight;

        public StardewValley.Object stoneBrazier;

        public List<Monster> monsterSpawns;

        public int specialType;

        public int mineLevel;

        public int spawnFrequency;

        public int spawnCounter;

        public Queue<Monster> spawnQueue;

        public bool portalExpired;

        public Vector2 portalWithin;

        public Vector2 portalRange;

        public bool baseTarget;

        public bool firstSpawn;

        public Vector2 baseVector;

        public string baseType;

        public Portal (Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {
            
            specialType = 0;

            mineLevel = 1;

            if (targetPlayer.CombatLevel >= 10)
            {
                mineLevel = 140;
            }
            else if (targetPlayer.CombatLevel >= 8)
            {
                mineLevel = 100;
            }
            else if (targetPlayer.CombatLevel >= 4)
            {
                mineLevel = 41;
            }

            portalWithin = target - new Vector2(4, 4);

            portalRange = new Vector2(9, 9);

            baseTarget = false;

            baseVector = target;

            baseType = "ground";

            spawnQueue = new();

            monsterSpawns = new();

            castCost = 0;

            portalExpired = false;

            firstSpawn = false;

        }

        public override void CastWater()
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 60;

            spawnFrequency = 2;

            if (targetPlayer.hasBuff(24))
            {
                spawnFrequency = 1;

            }

            spawnCounter = 0;

            targetLocation.objects.Remove(targetVector);

            Rectangle sourceRectForObject = new();

            sourceRectForObject.X = 276;
            sourceRectForObject.Y = 1965;
            sourceRectForObject.Width = 8;
            sourceRectForObject.Height = 8;

            float animationSort = targetVector.X * 1000 + targetVector.Y + 2;

            portalAnimation = new("LooseSprites\\Cursors", sourceRectForObject, 100f, 6, 9999, new((targetVector.X * 64f)+12f,(targetVector.Y * 64f)-56f), false, false, animationSort, 0f, Color.Blue, 6f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(portalAnimation);

            stoneBrazier = new(targetVector, 144, false);

            int portalKey = (int) float.Parse(baseVector.X.ToString() + baseVector.Y.ToString());
            
            portalLight = new(4, new((targetVector.X * 64f) + 12f, (targetVector.Y * 64f) - 56f), 2f, new Color(0, 80, 160), portalKey, LightSource.LightContext.None, 0L);

            stoneBrazier.name = "PortalFlame";
            stoneBrazier.CanBeSetDown = false;
            stoneBrazier.Fragility = 2;
            stoneBrazier.setHealth(9999);
            stoneBrazier.isLamp.Value = true;
            stoneBrazier.IsOn = true;
            stoneBrazier.lightSource = portalLight;

            targetLocation.objects.Add(targetVector, stoneBrazier);

            Vector2 boltVector = new(targetVector.X, targetVector.Y - 2);

            ModUtility.AnimateBolt(targetLocation, boltVector);

            Game1.changeMusicTrack("tribal", false, Game1.MusicContext.Default);

            castFire = true;

            castActive = true;

            return;

        }

        public override bool CastActive(int castIndex, int castLimit)
        {

            if (expireTime >= Game1.currentGameTime.TotalGameTime.TotalSeconds && targetPlayer.currentLocation == targetLocation) {

                portalExpired = true;

                return true;
            
            }

            return false;

        }

        public override void CastRemove() {

            if(stoneBrazier != null)
            {

                Game1.stopMusicTrack(Game1.MusicContext.Default);

                Game1.playSound("fireball");

                targetLocation.temporarySprites.Remove(portalAnimation);

                targetLocation.objects.Remove(targetVector);

                stoneBrazier = null;

                targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite(6, targetVector * 64f, Color.Blue * 0.75f, 8));

                if (targetLocation.hasLightSource(portalLight.Identifier))
                {

                    targetLocation.removeLightSource(portalLight.Identifier);

                }

                if (Game1.currentLightSources.Contains(portalLight))
                {

                    Game1.currentLightSources.Remove(portalLight);

                }

                portalLight = null;

            }

            int removedCounter = 0;

            foreach (Monster monsterSpawn in monsterSpawns)
            {

                if (monsterSpawn.Health > 0)
                {

                    targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite(6, monsterSpawn.getTileLocation() * 64f, Color.White * 0.75f, 8));

                    targetLocation.characters.Remove(monsterSpawn);

                    removedCounter++;

                };

            }

            if(portalExpired && specialType == 0 && removedCounter <= 4)
            {

                int objectIndex;
                
                if (mineLevel <= 40)
                {

                    objectIndex = 535;

                }
                else if (mineLevel <= 80)
                {

                    objectIndex = 536;

                }
                else //(targetLocation.mineLevel <= 120)
                {
                    
                    objectIndex = 537;
                
                }

                for(int i = 0; i < randomIndex.Next(1,4);  i++)
                {

                    Game1.createObjectDebris(objectIndex, (int)targetVector.X, (int)targetVector.Y);

                }

            }

            spawnQueue = new();

        }

        public override void CastTrigger() {

            spawnCounter++;

            if(spawnFrequency >= 3 && spawnCounter == 2 && !firstSpawn)
            {
                firstSpawn = true;
            
            }
            else if (spawnFrequency != spawnCounter)
            {

                return;

            }

            spawnCounter = 0;

            Vector2 spawnVector = new();

            //Dictionary<int, Vector2> offsetIndex;

            bool spawnTarget = false;

            int spawnAttempt = 0;

            Layer buildingLayer = targetLocation.Map.GetLayer("Buildings");

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            Tile buildingTile;

            Tile backTile;

            Vector2 playerVector = targetPlayer.getTileLocation();

            while (!spawnTarget && spawnAttempt++ < 4)
            {

                int offsetX = randomIndex.Next((int)portalRange.X);

                int offsetY = randomIndex.Next((int)portalRange.Y);

                Vector2 offsetVector = new(offsetX, offsetY);

                spawnVector = portalWithin + offsetVector;

                if(spawnVector == targetVector)
                {

                    continue;

                }

                if (Math.Abs(spawnVector.X - playerVector.X) <= 1 && Math.Abs(spawnVector.Y - playerVector.Y) <= 1)
                {
                    continue;

                }

                buildingTile = buildingLayer.PickTile(new xTile.Dimensions.Location((int)spawnVector.X * 64, (int)spawnVector.Y * 64), Game1.viewport.Size);

                backTile = backLayer.PickTile(new xTile.Dimensions.Location((int)spawnVector.X * 64, (int)spawnVector.Y * 64), Game1.viewport.Size);

                if (backTile == null)
                {
                    continue;
                }

                if (backTile.TileIndexProperties.TryGetValue("Water", out _))
                {
                    if (!baseTarget)
                    {
                        baseType = "water";

                        baseVector = spawnVector;

                        baseTarget = true;

                    }

                }     
                else if (targetLocation.isTileOccupied(spawnVector))
                {

                    if (!baseTarget)
                    {
                        baseType = "terrain";

                        baseVector = spawnVector;

                        baseTarget = true;

                    }

                }
                else if (buildingTile == null)
                {

                    spawnTarget = true;

                    switch (baseType)
                    {

                        case "water":
                            SpawnTerrain(spawnVector,true);
                            break;
                        case "terrain":
                            SpawnTerrain(spawnVector,false);
                            break;
                        default: // ground
                            SpawnGround(spawnVector);
                            break;

                    }

                }

            }

            baseTarget = false;

            baseType = "ground";

        }

        public KeyValuePair<int, Monster> SpawnMonster(Vector2 spawnVector)
        {

            int spawnMob;

            List<int> spawnIndex;

            switch (specialType)
            {

                case 1: // challengeEarth

                    spawnIndex = new()
                    {
                        99,

                    };

                    spawnMob = spawnIndex[randomIndex.Next(2)];

                    break;

                case 2: // challengeWater

                    spawnIndex = new()
                    {
                        2,2,6,

                    };

                    spawnMob = spawnIndex[randomIndex.Next(3)];

                    break;

                case 3: // challengeStars

                    spawnIndex = new()
                    {
                        0,0,0,4,

                    };

                    spawnMob = spawnIndex[randomIndex.Next(4)];

                    break;

                case 5: // forageable challenge

                    spawnIndex = new()
                    {
                        0,3,99,

                    };

                    spawnMob = spawnIndex[randomIndex.Next(3)];

                    break;

                case 6: // dustSprites in SecretWoods

                    spawnMob = 1;

                    break;

                default: // 0

                    spawnIndex = new()
                    {
                        0,1,2,3,4,99,

                    };

                    spawnMob = spawnIndex[randomIndex.Next(6)];

                    break;

            }

            Monster theMonster;

            switch (spawnMob)
            {

                case 0: // Slime

                    theMonster = new GreenSlime(spawnVector * 64f, mineLevel);

                    break;

                case 1: // Duster

                    theMonster = new DustSpirit(spawnVector * 64f);

                    break;

                case 2: // Brute

                    theMonster = new ShadowBrute(spawnVector * 64f)
                    {
                        focusedOnFarmers = true,
                    };

                    break;

                case 3: // Golem

                    RockGolem golemMonster = new(spawnVector * 64f, targetPlayer.CombatLevel);

                    theMonster = golemMonster;

                    break;

                case 4: // Big Slime

                    BigSlime bigMonster = new(spawnVector * 64f, targetPlayer.CombatLevel);

                    theMonster = bigMonster;

                    break;

                case 5: // Shadow Archer

                    theMonster = new Shooter(spawnVector * 64f, "Shadow Sniper");

                    break;

                case 6: // Shadow Shaman

                    theMonster = new ShadowShaman(spawnVector * 64f);

                    break;

                default: // Bat 99

                    theMonster = new Bat(spawnVector * 64f, mineLevel)
                    {
                        //wildernessFarmMonster = true
                    };

                    break;

            }

            return new KeyValuePair<int, Monster>(spawnMob,theMonster);

        }

        public void SpawnGround(Vector2 spawnVector)
        {

            KeyValuePair<int, Monster> monsterData = SpawnMonster(spawnVector);

            Monster theMonster = monsterData.Value;

            spawnQueue.Enqueue(theMonster);

            targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite(6, spawnVector * 64f, Color.DarkBlue * 0.75f, 4, false, 125));

            DelayedAction.functionAfterDelay(ManifestMonster, 500);

        }

        public void SpawnTerrain(Vector2 spawnVector,bool splash)
        {

            KeyValuePair<int, Monster> monsterData = SpawnMonster(spawnVector);

            int spawnMob = monsterData.Key;

            Monster theMonster = monsterData.Value;

            if(theMonster is RockGolem)
            {
                spawnQueue.Enqueue(theMonster);

                DelayedAction.functionAfterDelay(ManifestMonster, 100);

                return;

            }

            Rectangle targetRectangle = new(0,0,16,32);

            Vector2 fromPosition = new(baseVector.X * 64, baseVector.Y * 64);

            Vector2 toPosition = new(spawnVector.X * 64, spawnVector.Y * 64);

            float animationInterval = 125f;

            float motionX = (toPosition.X - fromPosition.X) / 1000;

            float compensate = 0.555f;

            float motionY = ((toPosition.Y - fromPosition.Y) / 1000) - compensate;

            float animationSort = float.Parse("0.0" + baseVector.X.ToString() + baseVector.Y.ToString());

            Color monsterColor = Color.White;

            if(theMonster is GreenSlime)
            {
                GreenSlime slimeMonster = (GreenSlime)theMonster;

                monsterColor = slimeMonster.color.Value;
            }

            string textureName = theMonster.Sprite.textureName.Value;

            //"Characters\\Monsters\\" + theMonster.Name

            TemporaryAnimatedSprite monsterSprite = new(textureName, targetRectangle, animationInterval, 4, 2, fromPosition, flicker: false, flipped: false, animationSort, 0f, monsterColor, 4f, 0f, 0f, 0f)
            {

                motion = new Vector2(motionX, motionY),

                acceleration = new Vector2(0f, 0.001f),

                timeBasedMotion = true,

            };

            targetLocation.temporarySprites.Add(monsterSprite);

            spawnQueue.Enqueue(theMonster);

            if (splash)
            {

                ModUtility.AnimateSplash(targetLocation, baseVector, true);

            }

            DelayedAction.functionAfterDelay(ManifestMonster, 1000);

        }

        public void ManifestMonster()
        {
            if(spawnQueue.Count > 0)
            {

                Monster theMonster = spawnQueue.Dequeue();

                targetLocation.characters.Add(theMonster);

                theMonster.update(Game1.currentGameTime, targetLocation);

                monsterSpawns.Add(theMonster);

                mod.MonsterTrack(targetLocation,theMonster);

            }

        }

    }

}
