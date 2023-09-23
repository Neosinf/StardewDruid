using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Monsters;
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

        public QuestData questData;

        public Queue<Vector2> trashQueue;

        public int trashCollected;

        public double expireTime;

        public int spawnCounter;

        public bool challengeWarning;

        Vector2 challengeWithin;

        Vector2 challengeRange;

        Vector2 challengeBorder;

        public Challenge(Mod mod, Vector2 target, Farmer player, Map.QuestData quest)
            : base(mod, target, player)
        {

            questData = quest;

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

            spawnCounter = 0;

            challengeWithin = questData.challengeWithin;

            challengeRange = questData.challengeRange;

            challengeBorder = challengeWithin + challengeRange;

            challengeWarning = false;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 45;

            List<Vector2> spawnPortals = questData.challengePortals;

            Portal portalHandle;

            foreach (Vector2 portalVector in spawnPortals)
            {

                portalHandle = new(mod, portalVector, targetPlayer);

                portalHandle.CastWater();

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

                    if (trashCollected < 15)
                    {

                        Game1.addHUDMessage(new HUDMessage($"Try again to collect more trash", ""));

                        //mod.UpdateQuest(activeChallenge, false);

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

                    spawnCounter++;

                    if (
                        playerVector.X >= challengeWithin.X &&
                        playerVector.Y >= challengeWithin.Y &&
                        playerVector.X < challengeBorder.X &&
                        playerVector.Y < challengeBorder.Y
                        )
                    {

                        if (spawnCounter == 2)
                        {

                            ThrowTrash();

                            spawnCounter = 0;

                        }

                    }
                    else if(!challengeWarning)
                    {

                        Game1.addHUDMessage(new HUDMessage($"Out of range of cleanup!", "3"));

                        challengeWarning = true;

                    }

                    Vector2 randomVector = challengeWithin + new Vector2(randomIndex.Next(5), randomIndex.Next(5));

                    Rockfall rockFall = new(mod, randomVector, targetPlayer);

                    rockFall.objectStrength = 3;

                    rockFall.CastEarth();

                break;

            }

        }

        public void ThrowTrash()
        {

            Dictionary<int, int> artifactIndex = new()
            {
                [0] = 105,
                [1] = 106,
                [2] = 110,
                [3] = 111,
                [4] = 112,
                [5] = 115,
                [6] = 117,
            };

            Dictionary<int, int> objectIndex = new()
            {
                [0] = artifactIndex[randomIndex.Next(7)],
                [1] = 167,
                [2] = 168,
                [3] = 169,
                [4] = 170,
                [5] = 171,
                [6] = 172,
                [7] = 167,
                [8] = 168,
                [9] = 169,
                [10] = 170,
                [11] = 171,
                [12] = 172,
            };

            int index = randomIndex.Next(13);

            Vector2 trashVector = trashQueue.Dequeue();

            Throw throwObject = new(objectIndex[index], 0);

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

                    break;

                case "challengeWater":

                    NPCIndex = new()
                    {

                        "Alex", "Elliott", "Harvey", "Sam",
                        "Abigail", "Emily", "Penny",
                        "Caroline", "Clint", "Evelyn", "George", "Gus", "Jodi", "Kent", "Lewis", "Pam", "Pierre", "Vincent",

                    };

                    break;


                default: //challengeEarth

                    NPCIndex = new()
                    {
                        "Sebastian",
                        "Maru",
                        "Robin", "Demetrius",   "Linus", "Dwarf"

                    };

                    break;

            }

            foreach (string NPCName in NPCIndex)
            {

                NPC characterFromName = Game1.getCharacterFromName(NPCName);

                if (characterFromName == null)
                {
                    characterFromName = Game1.getCharacterFromName<Child>(NPCName, mustBeVillager: false);
                }

                if (characterFromName != null)
                {

                    targetPlayer.changeFriendship(250, characterFromName);

                }
                else
                {

                    mod.Monitor.Log($"Unable to raise Friendship for {NPCName}", LogLevel.Debug);

                }

            }

        }

    }

}
