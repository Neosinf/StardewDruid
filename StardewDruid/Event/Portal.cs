using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewDruid.Monster;
using StardewValley;
using System.Collections.Generic;

namespace StardewDruid.Event
{
    public class Portal : ChallengeHandle
    {

        public Portal(Mod Mod, Vector2 target, Rite rite, Quest quest)
            : base(Mod, target, rite, quest)
        {
            targetVector = target;

        }

        public override void EventTrigger()
        {

            monsterHandle = new(mod, targetVector, riteData);

            monsterHandle.spawnFrequency = 2;

            monsterHandle.spawnIndex = new()
            {
                0,1,2,3,4,5,99,

            };

            targetLocation.objects.Remove(targetVector);

            Torch torch = ModUtility.StoneBrazier(targetLocation, targetVector);

            torchList.Add(torch);

            Vector2 boltVector = new(targetVector.X, targetVector.Y - 1);

            ModUtility.AnimateBolt(targetLocation, boltVector);

            Game1.changeMusicTrack("tribal", false, Game1.MusicContext.Default);

            mod.RegisterChallenge(this, "active");

        }

        public override bool EventActive()
        {

            if (expireTime < Game1.currentGameTime.TotalGameTime.TotalSeconds)
            {

                if (!riteData.castTask.ContainsKey("masterPortal"))
                {

                    mod.UpdateTask("lessonPortal", 1);

                }

                return false;

            }


            if (targetPlayer.currentLocation == targetLocation)
            {

                return true;

            }

            return false;

        }

        public override void EventInterval()
        {
            monsterHandle.SpawnInterval();
        }

    }

}
