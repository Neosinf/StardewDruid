﻿using Force.DeepCloner;
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewValley;
using System;
using System.Collections.Generic;
using static StardewValley.Minigames.TargetGame;

namespace StardewDruid.Event.Challenge
{
    public class Summon : EventHandle
    {

        List<int> portalConfig;

        public Summon(Vector2 target)
            : base(target)
        {
            targetVector = target;
            
        }

        public override void EventTrigger()
        {

            AddActor((targetVector - new Vector2(0, 1)) * 64);

            targetLocation.objects.Remove(targetVector);

            portalConfig = PortalConfig();

            monsterHandle = new(targetVector, Mod.instance.rite.castLocation);

            monsterHandle.spawnFrequency = portalConfig[1];

            monsterHandle.spawnAmplitude = portalConfig[2];

            monsterHandle.spawnSpecial = portalConfig[3];

            monsterHandle.spawnCombat *= 1 + (portalConfig[5] / 4);

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + portalConfig[4];

            monsterHandle.spawnIndex = new()
            {
                0, 1, 2, 3, 4, 99,

            };

            monsterHandle.specialIndex = new()
            {
                11, 12, 13,

            };

            LightHandle brazier = new(targetLocation, targetVector);

            braziers.Add(brazier);

            Vector2 boltVector = new(targetVector.X, targetVector.Y - 1);

            ModUtility.AnimateBolt(targetLocation, boltVector);

            SetTrack("tribal");

            Mod.instance.RegisterEvent(this, "active");

            Mod.instance.CastMessage("Portal Strength " + portalConfig[0].ToString());

        }

        public List<int> PortalConfig()
        {

            int strength = 0;

            List<Vector2> removeList = new();

            for (int i = 1; i < 4; i++)
            {

                List<Vector2> tileVectors = ModUtility.GetTilesWithinRadius(targetLocation, targetVector, i);

                foreach (Vector2 tileVector in tileVectors)
                {

                    if (targetLocation.objects.ContainsKey(tileVector))
                    {

                        StardewValley.Object targetObject = targetLocation.objects[tileVector];

                        if (targetObject is Torch && targetObject.itemId.Contains("93")) // crafted candle torch
                        {

                            removeList.Add(tileVector);

                        }

                    }

                }

            }

            foreach (Vector2 tileVector in removeList)
            {

                targetLocation.objects.Remove(tileVector);

                strength++;

            }

            Dictionary<int, List<int>> configs = new()
            {

                [0] = new() { 1, 2, 1, 0, 60, 0, }, // 30 monsters,
                [1] = new() { 2, 3, 2, 0, 72, 0, }, // 48 monsters, 
                [2] = new() { 3, 1, 1, 0, 72, 1, }, // 72 monsters, 1.25x difficulty
                [3] = new() { 4, 1, 1, 1, 72, 1, }, // 72 monsters, 1 boss, 1.25x difficulty
                [4] = new() { 5, 2, 3, 1, 72, 1, }, // 108 monsters, 1 boss, 1.25x difficulty
                [5] = new() { 6, 1, 1, 2, 96, 2, }, // 96 monsters, 2 bosses, 1.5x difficulty
                [6] = new() { 7, 1, 1, 3, 120, 2, }, // 120 monsters, 3 bosses, 1.5x difficulty
                [7] = new() { 8, 3, 2, 3, 144, 3, }, // 96 monsters, 3 bosses, 1.75x difficulty
                [8] = new() { 9, 3, 2, 4, 180, 4, }, // 120 monsters, 4 bosses, 2x difficulty

            };

            int setting = Math.Min(strength, configs.Count - 1);

            return configs[setting];

        }

        public override bool EventExpire()
        {

            if (eventLinger == -1)
            {

                if (!Mod.instance.rite.castTask.ContainsKey("masterPortal"))
                {

                    Mod.instance.UpdateTask("lessonPortal", 1);

                }

                int tileX = (int)targetVector.X;

                int tileY = (int)targetVector.Y;

                switch (portalConfig[0])
                {
                    
                    case 2:
                    case 3:
                    
                        for (int i = 0; i < 2; i++)
                        {
                            Game1.createObjectDebris("335", tileX, tileY);
                        }
                        
                        CastVoice("good", 2000);
                        
                        break;
                    
                    case 4:
                    case 5:
                    
                        for (int i = 0; i < 2; i++)
                        {
                            Game1.createObjectDebris("336", tileX, tileY);
                        }
                        
                        CastVoice("great", 2000);
                        
                        break;
                    
                    case 6:
                        
                        for (int i = 0; i < 2; i++)
                        {
                            Game1.createObjectDebris("337", tileX, tileY);
                        }
                        
                        CastVoice("superb", 2000);
                        
                        break;
                    
                    case 7:
                    case 8:

                        Game1.createObjectDebris("446", tileX, tileY, itemQuality: 4);

                        CastVoice("brilliant", 2000);
                        
                        break;

                    case 9:

                        Game1.createObjectDebris("74", tileX, tileY);

                        CastVoice("legendary", 2000);

                        break;

                    default:
                        
                        for (int i = 0; i < 2; i++)
                        {
                            Game1.createObjectDebris("334", tileX, tileY);
                        }
                        
                        CastVoice("sufficient", 2000);
                        
                        break;

                }

                RemoveMonsters();

                eventLinger = 2;

                return true;

            }

            return base.EventExpire();

        }

        public override void EventAbort()
        {
            Mod.instance.CastMessage("The portal through the veil has collapsed");
        }

        public override void EventInterval()
        {

            activeCounter++;

            monsterHandle.SpawnCheck();

            if (eventLinger != -1)
            {

                return;

            }

            monsterHandle.SpawnInterval();

            if (activeCounter % 30 == 0)
            {

                ResetBraziers();

            }

        }

    }

}
