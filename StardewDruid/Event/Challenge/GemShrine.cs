﻿using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewDruid.Monster;
using StardewValley;
using System.Collections.Generic;
using static StardewValley.IslandGemBird;

namespace StardewDruid.Event.Challenge
{
    public class GemShrine : ChallengeHandle
    {

        public List<Firebird> fireBirds;

        public GemShrine(Vector2 target, Rite rite, Quest quest)
            : base(target, rite, quest)
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 90;

            fireBirds = new List<Firebird>();

            voicePosition = new Vector2(24, 22) * 64;

        }

        public override void EventTrigger()
        {

            //ModUtility.AnimateMeteorZone(targetLocation, targetVector, Color.Red);

            ModUtility.AnimateRadiusDecoration(targetLocation, targetVector, "Stars", 1f, 1f);

            ModUtility.AnimateMeteor(targetLocation, targetVector, true);

            Mod.instance.RegisterEvent(this, "active");

            Game1.addHUDMessage(new HUDMessage($"Gem Shrine challenge initiated", ""));

        }


        public override void RemoveMonsters()
        {

            if (fireBirds.Count > 0)
            {

                foreach (Firebird fireBird in fireBirds)
                {

                    riteData.castLocation.characters.Remove(fireBird);

                }

                fireBirds.Clear();

            }

        }

        public override bool EventExpire()
        {

            if (eventLinger == -1)
            {
                if (expireEarly)
                {

                    CastVoice("you have the Island... for now", 3000);

                    if (!questData.name.Contains("Two"))
                    {

                        Game1.createObjectDebris(74, 24, 21);

                    }

                    Game1.createObjectDebris(69, 24, 21);

                    Game1.createObjectDebris(835, 24, 21);

                    Mod.instance.CompleteQuest(questData.name);

                }
                else
                {

                    CastVoice("the Sisters chose poorly", 3000);

                }

                eventLinger = 3;

                RemoveMonsters();

                return true;

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

            if (activeCounter < 7)
            {

                switch (activeCounter)
                {
                    case 1:

                        CastVoice("the Sisters raised another to the priesthood", 3000);

                        break;

                    case 4:

                        CastVoice("it matters not", 2000);

                        break;

                    case 6:

                        CastVoice("the Island boasts a stronger Power", 2000);

                        break;

                    default:

                        break;

                }

                Game1.playSound("batFlap");

                return;

            }

            if (activeCounter == 7)
            {

                // ---------------- West Bird

                Vector2 westVector = new Vector2(21 - 2, 27 + 1);

                TemporaryAnimatedSprite smallAnimation = new(5, westVector * 64, Color.Purple * 0.75f, 8, flipped: false, 75f)
                {
                    scale = 1.25f,
                };

                riteData.castLocation.temporarySprites.Add(smallAnimation);

                GemBirdType gemBirdType = GetBirdTypeForLocation("IslandWest");

                Firebird westBird = MonsterData.CreateMonster(15, westVector, riteData.combatModifier) as Firebird;

                westBird.setBirdType(gemBirdType.ToString());

                riteData.castLocation.characters.Add(westBird);

                westBird.update(Game1.currentGameTime, riteData.castLocation);

                if (!questData.name.Contains("Two"))
                {

                    westBird.HardMode();

                }

                fireBirds.Add(westBird);

                // ---------------- East Bird

                Vector2 eastVector = new Vector2(27 + 2, 27 - 1);

                smallAnimation = new(5, eastVector * 64, Color.Purple * 0.75f, 8, flipped: false, 75f)
                {
                    scale = 1.25f,
                };

                riteData.castLocation.temporarySprites.Add(smallAnimation);

                gemBirdType = GetBirdTypeForLocation("IslandEast");

                Firebird eastBird = MonsterData.CreateMonster(15, eastVector, riteData.combatModifier) as Firebird; ;

                eastBird.setBirdType(gemBirdType.ToString());

                riteData.castLocation.characters.Add(eastBird);

                eastBird.update(Game1.currentGameTime, riteData.castLocation);

                fireBirds.Add(eastBird);


                // ---------------- South Bird

                Vector2 southVector = new Vector2(24 + 1, 28 + 2);

                smallAnimation = new(5, southVector * 64, Color.Purple * 0.75f, 8, flipped: false, 75f)
                {
                    scale = 1.25f,
                };

                riteData.castLocation.temporarySprites.Add(smallAnimation);

                gemBirdType = GetBirdTypeForLocation("IslandSouth");

                Firebird southBird = MonsterData.CreateMonster(15, southVector, riteData.combatModifier) as Firebird;

                southBird.setBirdType(gemBirdType.ToString());

                riteData.castLocation.characters.Add(southBird);

                southBird.update(Game1.currentGameTime, riteData.castLocation);

                fireBirds.Add(southBird);


                // ---------------- North Bird

                Vector2 northVector = new Vector2(24 - 1, 25 - 2);

                smallAnimation = new(5, northVector * 64, Color.Purple * 0.75f, 8, flipped: false, 75f)
                {
                    scale = 1.25f,
                };

                riteData.castLocation.temporarySprites.Add(smallAnimation);

                gemBirdType = GetBirdTypeForLocation("IslandNorth");

                Firebird northBird = MonsterData.CreateMonster(15, northVector, riteData.combatModifier) as Firebird;

                northBird.setBirdType(gemBirdType.ToString());

                riteData.castLocation.characters.Add(northBird);

                northBird.update(Game1.currentGameTime, riteData.castLocation);

                fireBirds.Add(northBird);

                SetTrack("cowboy_boss");

                return;

            }

            if (activeCounter == 10)
            {

                Game1.addHUDMessage(new HUDMessage($"Hit the birds to stop their fire attacks", ""));

            }

            int defeated = 0;

            foreach (Firebird bird in fireBirds)
            {

                if (bird.defeated)
                {

                    riteData.castLocation.characters.Remove(bird);

                    defeated++;

                }

            }

            if (defeated == fireBirds.Count)
            {

                expireEarly = true;

            }

        }


    }
}
