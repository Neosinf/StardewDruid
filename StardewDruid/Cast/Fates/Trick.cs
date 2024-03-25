using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;
using System;
using xTile.Dimensions;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace StardewDruid.Cast.Fates
{
    internal class Trick : CastHandle
    {

        NPC riteWitness;

        int friendship;

        private string trick;

        public Trick(Vector2 target,  NPC witness)
            : base(target)
        {

            riteWitness = witness;

        }

        public override void CastEffect()
        {

            riteWitness.faceTowardFarmerForPeriod(3000, 4, false, targetPlayer);

            riteWitness.doEmote(8);

            DelayedAction.functionAfterDelay(CastAfterDelay, 1000);

            ModUtility.AnimateCursor(targetLocation, targetPlayer.Position, riteWitness.Position,"Fates");

            castFire = true;

        }

        public void CastAfterDelay()
        {

            if (riteWitness == null || targetLocation == null) { return; }

            friendship = 100;

            int friendshipRatio = 3;

            if (!Mod.instance.rite.castTask.ContainsKey("masterTrick"))
            {

                Mod.instance.UpdateTask("lessonTrick", 1);

            }
            else
            {

                friendshipRatio = 5;

            }

            int reaction = 500;

            trick = "butterflies";

            int interval = 2000;

            int trickInt = randomIndex.Next(6);

            switch (trickInt)
            {

                case 0:

                    ModUtility.AnimateMeteor(targetLocation, riteWitness.Tile - new Vector2(0, 1), true);

                    DelayedAction.functionAfterDelay(DeathSpray, 200);

                    friendship = -10;

                    trick = "meteors";

                    break;

                case 1:

                    ModUtility.AnimateBolt(targetLocation, riteWitness.Tile - new Vector2(0, 1));

                    DelayedAction.functionAfterDelay(DeathSpray, 200);

                    trick = "bolts of lightning";

                    friendship = -10;

                    break;

                case 2:

                    ModUtility.AnimateRandomCritter(targetLocation, riteWitness.Tile);

                    friendship = randomIndex.Next(1, friendshipRatio) * 25;

                    trick = "critters";

                    break;

                case 3:

                    Levitate levitation = new(targetVector, riteWitness);

                    levitation.EventTrigger();

                    friendship = randomIndex.Next(1, friendshipRatio) * 25;

                    trick = "levitations";

                    break;

                case 4:

                    ModUtility.AnimateRandomFish(targetLocation, riteWitness.Tile);

                    friendship = randomIndex.Next(1, friendshipRatio) * 25;

                    trick = "fishies";

                    break;

                default:

                    ModUtility.AnimateButterflySpray(targetLocation, riteWitness.Tile);

                    friendship = randomIndex.Next(1, friendshipRatio) * 25;
                    break;
            }


            TemporaryAnimatedSprite radiusAnimation = new(0, interval, 1, 1, riteWitness.Position - new Microsoft.Xna.Framework.Vector2(64,64), false, false)
            {

                sourceRect = new(192, 0, 64, 64),

                sourceRectStartingPos = new Vector2(192, 0),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Decorations.png")),

                scale = 3f, //* size,

                timeBasedMotion = true,

                layerDepth = 0.0001f,

                rotationChange = 0.06f,

            };

            targetLocation.temporarySprites.Add(radiusAnimation);

            //break;

            //}

            DelayedAction.functionAfterDelay(VillagerReaction, reaction);

        }

        public void VillagerReaction()
        {

            ModUtility.GreetVillager(targetPlayer, riteWitness, friendship);

            StardewDruid.Dialogue.Reaction.ReactTo(riteWitness, "Fates", friendship, new List<string>()
              {
                trick
              });

        }

        public void DeathSpray()
        {

            ModUtility.AnimateDeathSpray(targetLocation, riteWitness.Position, Color.Gray * 0.5f);

        }

    }

}
