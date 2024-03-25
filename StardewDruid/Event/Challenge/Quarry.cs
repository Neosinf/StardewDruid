using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewValley;
using System.Collections.Generic;
using System.IO;

namespace StardewDruid.Event.Challenge
{
    public class Quarry : ChallengeHandle
    {

        public List<TemporaryAnimatedSprite> challengeAnimations;

        public bool keepJester;

        public Quarry(Vector2 target,  Quest quest)
            : base(target, quest)
        {

            challengeAnimations = new();

        }

        public override void EventTrigger()
        {
            cues = DialogueData.DialogueScene(questData.name);

            monsterHandle = new(targetVector, Mod.instance.rite.castLocation);

            monsterHandle.spawnIndex = new() { 51, 52, 53, 54, };

            monsterHandle.spawnFrequency = 2;

            if (questData.name.Contains("Two"))
            {

                monsterHandle.spawnFrequency = 1;

                //monsterHandle.spawnAmplitude = 2;

            }

            monsterHandle.spawnWithin = targetVector + new Vector2(-5, -5);

            monsterHandle.spawnRange = new Vector2(11, 11);

            Mod.instance.RegisterEvent(this, "active");

            if (Mod.instance.characters.ContainsKey("Jester"))
            {

                StardewDruid.Character.Character jester = Mod.instance.characters["Jester"];

                jester.SwitchFollowMode();

            }

        }

        public override void EventRemove()
        {

            base.EventRemove();

            if (Mod.instance.characters.ContainsKey("Jester"))
            {
                if (!keepJester)
                {

                    Mod.instance.characters["Jester"].WarpToDefault();

                    Mod.instance.characters["Jester"].SwitchRoamMode();


                }
            }

            if (challengeAnimations.Count > 0)
            {

                foreach (var animation in challengeAnimations)
                {

                    targetLocation.temporarySprites.Remove(animation);

                }

            }

        }

        public override bool EventExpire()
        {
            
            EventComplete();

            if (Mod.instance.characters.ContainsKey("Jester"))
            {

                keepJester = true;

                Mod.instance.dialogue["Jester"].AddSpecial("Jester", "ThanatoshiTwo");

            }

            return false;

        }

        public override void EventInterval()
        {

            activeCounter++;

            monsterHandle.SpawnCheck();

            if (eventLinger != -1)
            {

                return;

            }

            if (Mod.instance.characters.ContainsKey("Jester"))
            {

                DialogueCue(DialogueData.DialogueNarrator(questData.name), new() { [0] = Mod.instance.characters["Jester"], }, activeCounter);

            }

            if (activeCounter < 7)
            {
                
                Vector2 meteorVector = new(-5 + randomIndex.Next(10), -5 + randomIndex.Next(10));

                new Cast.Stars.Meteor(meteorVector, Mod.instance.DamageLevel()).CastEffect();

                if (activeCounter == 4)
                {

                    TemporaryAnimatedSprite challengeAnimation = new(0, 99999f, 1, 1, targetVector * 64 - new Vector2(64, 64), false, false)
                    {

                        sourceRect = new(192, 0, 64, 64),

                        sourceRectStartingPos = new Vector2(192,0),

                        texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Decorations.png")),

                        scale = 3f,

                        layerDepth = 0.00002f,

                        rotationChange = -0.06f,

                    };

                    targetLocation.temporarySprites.Add(challengeAnimation);

                    challengeAnimations.Add(challengeAnimation);

                    TemporaryAnimatedSprite nightAnimation = new(0, 99999f, 1, 1, targetVector * 64 - new Vector2(64, 64), false, false)
                    {

                        sourceRect = new(0, 0, 64, 64),

                        sourceRectStartingPos = new Vector2(0, 0),

                        texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Nightsky.png")),

                        scale = 3f,

                        layerDepth = 0.00001f,

                    };

                    targetLocation.temporarySprites.Add(nightAnimation);

                    challengeAnimations.Add(nightAnimation);

                }

                if (activeCounter == 5)
                {

                    SetTrack("tribal");

                }

                return;
            }

            if (activeCounter == 20)
            {

                Vector2 spawnVector = monsterHandle.SpawnVector();

                if (spawnVector != new Vector2(-1))
                {
                    monsterHandle.specialIndex = new() { 55, };
                    monsterHandle.SpawnGround(spawnVector, true);

                }

            }

            if (activeCounter == 40)
            {

                Vector2 spawnVector = monsterHandle.SpawnVector();

                if (spawnVector != new Vector2(-1))
                {
                    monsterHandle.specialIndex = new() { 56, };
                    monsterHandle.SpawnGround(spawnVector, true);

                }

            }

            if (activeCounter <= 56)
            {

                monsterHandle.SpawnInterval();

            }

        }

    
    }

}
