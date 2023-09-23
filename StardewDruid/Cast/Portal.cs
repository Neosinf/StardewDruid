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

        public double expireTime;

        public List<Monster> monsterSpawns;

        public int specialType;

        public int mineLevel;

        public int spawnFrequency;

        public int spawnCounter;

        public Vector2 portalWithin;

        public Vector2 portalRange;

        public Portal (Mod mod, Vector2 target, Farmer player)
            : base(mod, target, player)
        {
            
            specialType = 0;

            mineLevel = 1;

            if (player.CombatLevel >= 10)
            {
                mineLevel = 140;
            }
            else if (player.CombatLevel >= 8)
            {
                mineLevel = 100;
            }
            else if (player.CombatLevel >= 4)
            {
                mineLevel = 41;
            }

            portalWithin = target - new Vector2(4, 4);

            portalRange = new Vector2(9, 9);

        }

        public override void CastWater()
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 45;

            spawnFrequency = 2;

            spawnCounter = 0;

            targetLocation.objects.Remove(targetVector);

            float animationSort = (targetVector.X * 1000) + targetVector.Y+2;

            Rectangle sourceRectForObject = new();

            sourceRectForObject.X = 276;
            sourceRectForObject.Y = 1965;
            sourceRectForObject.Width = 8;
            sourceRectForObject.Height = 8;

            portalAnimation = new("LooseSprites\\Cursors", sourceRectForObject, 100f, 6, 9999, new((targetVector.X * 64f)+12f,(targetVector.Y * 64f)-56f), false, false, animationSort, 0f, Color.Blue, 6f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(portalAnimation);

            StardewValley.Object stoneBrazier = new(targetVector, 144, false);

            int portalKey = (int)(targetVector.X * 3000f + targetVector.Y);

            portalLight = new(4, new((targetVector.X * 64f) + 12f, (targetVector.Y * 64f) - 56f), 2f, new Color(0, 80, 160), portalKey, LightSource.LightContext.None, 0L);

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

            castCost = 24;

            castActive = true;

            monsterSpawns = new();

            return;

        }

        public override bool CastActive(int castIndex, int castLimit)
        {

            if (expireTime >= Game1.currentGameTime.TotalGameTime.TotalSeconds && targetPlayer.currentLocation == targetLocation) {

                return true;
            
            }

            return false;

        }

        public override void CastRemove() {

            Game1.stopMusicTrack(Game1.MusicContext.Default);

            Game1.playSound("fireball");

            targetLocation.temporarySprites.Remove(portalAnimation);

            targetLocation.objects.Remove(targetVector);

            targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite(6, targetVector * 64f, Color.Blue * 0.75f, 8));

            if(targetLocation.hasLightSource(portalLight.Identifier))
            {

                targetLocation.removeLightSource(portalLight.Identifier);

            }

            if(Game1.currentLightSources.Contains(portalLight))
            {

                Game1.currentLightSources.Remove(portalLight);

            }

            foreach (Monster monsterSpawn in monsterSpawns)
            {

                if (monsterSpawn.Health > 0)
                {

                    targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite(6, monsterSpawn.getTileLocation() * 64f, Color.White * 0.75f, 8));

                    targetLocation.characters.Remove(monsterSpawn);

                };

            }

        }

        public override void CastTrigger() {

            spawnCounter++;

            if (spawnFrequency != spawnCounter)
            {

                return;

            }

            spawnCounter = 0;

            Vector2 spawnVector = new();

            //Dictionary<int, Vector2> offsetIndex;

            bool spawnTarget = false;

            bool waterTarget = false;

            int spawnAttempt = 0;

            Layer buildingLayer = targetLocation.Map.GetLayer("Buildings");

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            Tile buildingTile;

            Tile backTile;

            Vector2 playerVector = targetPlayer.getTileLocation();

            bool playerCollision;

            while (!spawnTarget && spawnAttempt++ < 4)
            {

                /*int randomX = randomIndex.Next(5);

                int randomY = randomIndex.Next(5);

                offsetIndex = new()
                {
                    [0] = new(randomX, randomY),
                    [1] = new(0 - randomX, 0 - randomY),
                    [2] = new(randomX, 0 - randomY),
                    [3] = new(0 - randomX, randomY),
                };
                
                 spawnVector = targetVector + offsetIndex[randomIndex.Next(4)];
                */

                playerCollision = false;

                int offsetX = randomIndex.Next((int)portalRange.X);

                int offsetY = randomIndex.Next((int)portalRange.X);

                Vector2 offsetVector = new(offsetX, offsetY);

                spawnVector = portalWithin + offsetVector;

                if (Math.Abs(spawnVector.X - playerVector.X) <= 1 && Math.Abs(spawnVector.Y - playerVector.Y) <= 1)
                {

                    playerCollision = true;

                }

                buildingTile = buildingLayer.PickTile(new xTile.Dimensions.Location((int)spawnVector.X * 64, (int)spawnVector.Y * 64), Game1.viewport.Size);

                backTile = backLayer.PickTile(new xTile.Dimensions.Location((int)spawnVector.X * 64, (int)spawnVector.Y * 64), Game1.viewport.Size);

                if (backTile != null)
                {
                    if (backTile.TileIndexProperties.TryGetValue("Water", out _))
                    {
                        waterTarget = true;
                    }

                }
                
                if (!playerCollision && spawnVector != targetVector && !waterTarget && !targetLocation.isTileOccupied(spawnVector) && buildingTile == null)
                {
                    spawnTarget = true;

                }

            }

            if (spawnTarget)
            {

                SpawnGround(spawnVector);

            }
            else if (waterTarget)
            {

                SpawnWater(spawnVector);

            }

        }

        public void SpawnGround(Vector2 spawnVector)
        {

            int spawnMob;

            List<int> spawnIndex;

            switch (specialType)
            {

                case 1: // challengeEarth

                    spawnMob = 99; // bat

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

                default: // 0

                    spawnIndex = new()
                    {
                        0,1,2,3,99,

                    };     
                    
                    spawnMob = spawnIndex[randomIndex.Next(5)];

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

                default: // Bat

                     theMonster = new Bat(spawnVector * 64f, mineLevel)
                    {
                        //wildernessFarmMonster = true
                    };

                    break;

            }

            targetLocation.characters.Add(theMonster);

            theMonster.update(Game1.currentGameTime, targetLocation);

            monsterSpawns.Add(theMonster);

            targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite(6, spawnVector * 64f, Color.DarkBlue * 0.75f, 8, false, 200));

            return;
        
        }

        public void SpawnWater(Vector2 spawnVector)
        {

            Bat monsterBat = new(spawnVector * 64f, mineLevel)
            {
                //wildernessFarmMonster = true
            };

            targetLocation.characters.Add(monsterBat);

            monsterBat.update(Game1.currentGameTime, targetLocation);

            monsterSpawns.Add(monsterBat);

            targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite(6, spawnVector * 64f, Color.DarkBlue * 0.75f, 8, false, 200));

            return;

        }


    }

}
