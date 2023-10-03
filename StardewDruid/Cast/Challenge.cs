using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast
{
    internal class Challenge : Cast
    {

        public string activeChallenge;

        public Quest questData;

        public Queue<Vector2> trashQueue;

        public int trashCollected;

        public int spawnCounter;

        public bool challengeWarning;

        Vector2 challengeWithin;

        Vector2 challengeRange;

        Vector2 challengeBorder;

        public Challenge(Mod mod, Vector2 target, Rite rite, Map.Quest quest)
            : base(mod, target, rite)
        {

            questData = quest;

        }

        public override void CastQuest()
        {

            switch (questData.triggerCast)
            {

                case "CastStars":

                    CastStars();

                    break;

                case "CastWater":

                    CastWater();

                    break;

                default: // CastEarth

                    CastEarth();

                    break;

            }

        }

        public override void CastEarth()
        {

            activeChallenge = "challengeEarth";

            SetupPortal(1);

            Layer backLayer = targetLocation.Map.GetLayer("Back");

            trashQueue = new();

            trashCollected = 0;

            for (int i = 0; i < 3; i++)
            {

                List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(targetLocation, targetVector, 3 + i);

                foreach (Vector2 castVector in castSelection)
                {

                    Tile backTile = backLayer.PickTile(new Location((int)castVector.X * 64, (int)castVector.Y * 64), Game1.viewport.Size);

                    if (backTile != null)
                    {

                        if (backTile.TileIndexProperties.TryGetValue("Water", out _))
                        {

                            if (randomIndex.Next(2) == 0) { continue; };

                            trashQueue.Enqueue(castVector);

                        }

                    }

                }

            }

            Game1.addHUDMessage(new HUDMessage($"Stand your ground!", "2"));

        }

        public override void CastWater()
        {

            activeChallenge = "challengeWater";

            SetupPortal(2);

            Game1.addHUDMessage(new HUDMessage($"Defeat the shadows!", "2"));

        }

        public override void CastStars()
        {

            activeChallenge = "challengeStars";

            SetupPortal(3);

            Game1.addHUDMessage(new HUDMessage($"Defeat the slimes!", "2"));

        }

        public void SetupPortal(int portalType)
        {

            challengeWithin = questData.challengeWithin;

            challengeRange = questData.challengeRange;

            challengeBorder = challengeWithin + challengeRange;

            challengeWarning = false;

            int portalTime = (questData.challengeSeconds == 0) ? 45 : questData.challengeSeconds;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + portalTime;

            List<Vector2> spawnPortals = questData.challengePortals;

            Portal portalHandle;

            foreach (Vector2 portalVector in spawnPortals)
            {

                portalHandle = new(mod, portalVector, new Rite() { caster = targetPlayer });

                portalHandle.CastWater();

                portalHandle.expireTime = expireTime;

                portalHandle.portalWithin = challengeWithin;

                portalHandle.portalRange = challengeRange;

                portalHandle.specialType = portalType;

                portalHandle.spawnFrequency = questData.challengeFrequency;

                mod.ActiveCast(portalHandle);

            }

            mod.ActiveCast(this);

        }

        public override bool CastActive(int castIndex, int castLimit)
        {

            if (targetPlayer.currentLocation == targetLocation)
            {

                double nowTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;

                if (expireTime >= nowTime)
                {

                    int diffTime = (int) Math.Round(expireTime - nowTime);

                    if (diffTime % 10 == 0 && diffTime != 0)
                    {

                        switch (activeChallenge)
                        {
                            case "challengeWater":
                            case "challengeStars":

                                Game1.addHUDMessage(new HUDMessage($"Survive for {(int)diffTime} more minutes!", "2"));

                                break;


                            default: // earth

                                Game1.addHUDMessage(new HUDMessage($"{(int)diffTime} minutes left until cleanup complete", "2"));

                            break;

                        }

                    }

                    return true;

                }
            
            }

            switch (activeChallenge)
            {

                case "challengeWater":
                case "challengeStars":

                    UpdateFriendship();

                    mod.UpdateQuest(activeChallenge,true);

                    break;

                default: // "challengeEarth"

                    if (trashCollected < 12)
                    {

                        Game1.addHUDMessage(new HUDMessage($"Try again to collect more trash", ""));

                        mod.UpdateQuest(activeChallenge, false);

                    }
                    else
                    {

                        Game1.addHUDMessage(new HUDMessage($"Collected {trashCollected} pieces of trash!", ""));

                        UpdateFriendship();

                        mod.UpdateQuest(activeChallenge, true);
                        
                    }

                    break;

            }

            return false;

        }

        public override void CastTrigger()
        {

            Vector2 playerVector = targetPlayer.getTileLocation();

            switch (activeChallenge)
            {

                case "challengeWater":
                case "challengeStars":

                    if (
                        playerVector.X >= challengeWithin.X &&
                        playerVector.Y >= challengeWithin.Y &&
                        playerVector.X < challengeBorder.X &&
                        playerVector.Y < challengeBorder.Y
                        )
                    {

                        spawnCounter++;

                    }
                    else if (!challengeWarning)
                    {

                        Game1.addHUDMessage(new HUDMessage($"Get back into the fight!", "3"));

                        challengeWarning = true;

                    }

                    break;

                default: //challengeEarth

                    if (
                        playerVector.X >= challengeWithin.X &&
                        playerVector.Y >= challengeWithin.Y &&
                        playerVector.X < challengeBorder.X &&
                        playerVector.Y < challengeBorder.Y
                        )
                    {

                        if (randomIndex.Next(2) == 0)
                        {

                            ThrowTrash();

                            spawnCounter++;

                        }

                    }
                    else if(!challengeWarning)
                    {

                        Game1.addHUDMessage(new HUDMessage($"Out of range of cleanup!", "3"));

                        challengeWarning = true;

                    }

                    Vector2 randomVector = challengeWithin + new Vector2(randomIndex.Next(5), randomIndex.Next(5));

                    Rockfall rockFall = new(mod, randomVector, new Rite() { caster = targetPlayer })
                    {
                        objectStrength = 3
                    };

                    rockFall.CastEarth();

                    //targetLocation.lightGlows.Clear();

                    break;

            }

        }

        public void ThrowTrash()
        {

            if (trashQueue.Count == 0)
            {
                return;
            }

            Vector2 trashVector = trashQueue.Dequeue();

            Dictionary<int, int> artifactIndexes = new()
            {
                [0] = 105,
                [1] = 106,
                [2] = 110,
                [3] = 111,
                [4] = 112,
                [5] = 115,
                [6] = 117,
            };

            Dictionary<int, int> objectIndexes = new()
            {
                [0] = artifactIndexes[randomIndex.Next(7)],
                [1] = 167,
                [2] = 168,
                [3] = 169,
                [4] = 170,
                [5] = 171,
                [6] = 172,
            };

            int objectIndex = objectIndexes[randomIndex.Next(7)];

            Throw throwObject;

            if (trashCollected == 8)
            {

                objectIndex = 517;

                throwObject = new(objectIndex, 0);

                throwObject.objectInstance = new Ring(objectIndex);

            } else if(trashCollected == 16)
            {

                objectIndex = 519;

                throwObject = new(objectIndex, 0);

                throwObject.objectInstance = new Ring(objectIndex);

            }
            else
            {

                throwObject = new(objectIndex, 0);

            }

            throwObject.ThrowObject(targetPlayer, trashVector);

            targetPlayer.currentLocation.playSound("pullItemFromWater");

            bool targetDirection = (targetPlayer.getTileLocation().X <= trashVector.X);

            ModUtility.AnimateSplash(targetLocation, trashVector, targetDirection);

            trashCollected++;

        }

        public void UpdateFriendship()
        {

            List<string> NPCIndex;

            switch (activeChallenge)
            {

                case "challengeStars":

                    NPCIndex = new()
                    {

                        "Shane",
                        "Leah", "Haley",
                        "Marnie", "Jas", "Krobus", "Wizard", "Willy"

                    };

                    Game1.addHUDMessage(new HUDMessage($"You have gained favour with those who love the forest", ""));

                    break;

                case "challengeWater":

                    NPCIndex = new()
                    {

                        "Alex", "Elliott", "Harvey", 
                        "Emily", "Penny",
                        "Caroline", "Clint", "Evelyn", "George", "Gus", "Jodi", "Kent", "Lewis", "Pam", "Pierre", "Vincent",

                    };

                    Game1.addHUDMessage(new HUDMessage($"You have gained favour with many of the town residents", ""));

                    break;


                default: //challengeEarth

                    NPCIndex = new()
                    {
                        "Sebastian", "Sam",
                        "Maru", "Abigail",
                        "Robin", "Demetrius", "Linus", "Dwarf"

                    };

                    Game1.addHUDMessage(new HUDMessage($"You have gained favour with the mountain residents and their friends", ""));

                    break;

            }

            foreach (string NPCName in NPCIndex)
            {

                NPC characterFromName = Game1.getCharacterFromName(NPCName);

                characterFromName ??= Game1.getCharacterFromName<Child>(NPCName, mustBeVillager: false);

                if (characterFromName != null)
                {

                    targetPlayer.changeFriendship(375, characterFromName);

                }
                else
                {

                    mod.Monitor.Log($"Unable to raise Friendship for {NPCName}", LogLevel.Debug);

                }

            }

        }

    }

}
