using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewDruid.Monster.Boss;
using StardewValley;
using System.Collections.Generic;
using static StardewValley.IslandGemBird;

namespace StardewDruid.Event.Boss
{
    public class GemShrine : BossHandle
    {

        public List<Firebird> fireBirds;

        public GemShrine(Vector2 target,  Quest quest)
            : base(target, quest)
        {

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 90;

            fireBirds = new List<Firebird>();

        }

        public override void EventTrigger()
        {
            
            cues = DialogueData.DialogueScene(questData.name);

            AddActor(new Vector2(24, 22) * 64);

            ModUtility.AnimateCursor(targetLocation, targetVector*64, targetVector * 64, "Stars");

            ModUtility.AnimateMeteor(targetLocation, targetVector, true);

            Mod.instance.RegisterEvent(this, "active");

            Mod.instance.CastMessage("Gem Shrine challenge initiated");

        }


        public override void RemoveMonsters()
        {

            if (fireBirds.Count > 0)
            {

                foreach (Firebird fireBird in fireBirds)
                {

                    Mod.instance.rite.castLocation.characters.Remove(fireBird);

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
                    
                    DialogueCue(DialogueData.DialogueNarrator(questData.name), new() { [0] = actors[0], }, 991);

                    if (!questData.name.Contains("Two"))
                    {

                        Game1.createObjectDebris("74", 24, 21);

                    }

                    Game1.createObjectDebris("69", 24, 21);

                    Game1.createObjectDebris("835", 24, 21);

                    EventComplete();

                }
                else
                {

                    DialogueCue(DialogueData.DialogueNarrator(questData.name), new() { [0] = actors[0], }, 992);

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

            DialogueCue(DialogueData.DialogueNarrator(questData.name), new() { [0] = actors[0], }, activeCounter);

            if (activeCounter < 7)
            {

                Game1.playSound("batFlap");

                return;

            }

            if (activeCounter == 7)
            {


                Dictionary<string, int> birdTypes = new()
                {
                    ["Emerald"] = 41,
                    ["Aquamarine"] = 42,
                    ["Ruby"] = 43,
                    ["Amethyst"] = 44,
                    ["Topaz"] = 45,
                };

                // ---------------- West Bird

                Vector2 westVector = new Vector2(21 - 2, 27 + 1);

                TemporaryAnimatedSprite smallAnimation = new(5, westVector * 64, Color.Purple * 0.75f, 8, flipped: false, 75f)
                {
                    scale = 1.25f,
                };

                Mod.instance.rite.castLocation.temporarySprites.Add(smallAnimation);

                GemBirdType gemBirdType = GetBirdTypeForLocation("IslandWest");

                Firebird westBird = MonsterData.CreateMonster(birdTypes[gemBirdType.ToString()], westVector) as Firebird;

                Mod.instance.rite.castLocation.characters.Add(westBird);

                westBird.update(Game1.currentGameTime, Mod.instance.rite.castLocation);

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

                Mod.instance.rite.castLocation.temporarySprites.Add(smallAnimation);

                gemBirdType = GetBirdTypeForLocation("IslandEast");

                Firebird eastBird = MonsterData.CreateMonster(birdTypes[gemBirdType.ToString()], eastVector) as Firebird; ;

                Mod.instance.rite.castLocation.characters.Add(eastBird);

                eastBird.update(Game1.currentGameTime, Mod.instance.rite.castLocation);

                fireBirds.Add(eastBird);


                // ---------------- South Bird

                Vector2 southVector = new Vector2(24 + 1, 28 + 2);

                smallAnimation = new(5, southVector * 64, Color.Purple * 0.75f, 8, flipped: false, 75f)
                {
                    scale = 1.25f,
                };

                Mod.instance.rite.castLocation.temporarySprites.Add(smallAnimation);

                gemBirdType = GetBirdTypeForLocation("IslandSouth");

                Firebird southBird = MonsterData.CreateMonster(birdTypes[gemBirdType.ToString()], southVector) as Firebird;

                Mod.instance.rite.castLocation.characters.Add(southBird);

                southBird.update(Game1.currentGameTime, Mod.instance.rite.castLocation);

                fireBirds.Add(southBird);


                // ---------------- North Bird

                Vector2 northVector = new Vector2(24 - 1, 25 - 2);

                smallAnimation = new(5, northVector * 64, Color.Purple * 0.75f, 8, flipped: false, 75f)
                {
                    scale = 1.25f,
                };

                Mod.instance.rite.castLocation.temporarySprites.Add(smallAnimation);

                gemBirdType = GetBirdTypeForLocation("IslandNorth");

                Firebird northBird = MonsterData.CreateMonster(birdTypes[gemBirdType.ToString()], northVector) as Firebird;

                Mod.instance.rite.castLocation.characters.Add(northBird);

                northBird.update(Game1.currentGameTime, Mod.instance.rite.castLocation);

                fireBirds.Add(northBird);

                SetTrack("cowboy_boss");

                return;

            }

            if (activeCounter == 10)
            {
                Mod.instance.CastMessage("Hit the birds to stop their fire attacks");
            }

            int defeated = 0;

            foreach (Firebird bird in fireBirds)
            {

                if (!ModUtility.MonsterVitals(bird,targetLocation))
                {

                    Mod.instance.rite.castLocation.characters.Remove(bird);

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
