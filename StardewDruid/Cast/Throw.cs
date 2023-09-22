using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Cast
{
    internal class Throw
    {

        private Farmer targetPlayer;

        private StardewValley.Object objectInstance;

        public int objectIndex;

        public int objectQuality;

        public Throw(int ObjectIndex, int ObjectQuality)
        {
            
            objectIndex = ObjectIndex;

            objectQuality = ObjectQuality;

        }

        public void ThrowObject(Farmer TargetPlayer, Vector2 targetVector)
        {

            /*
             * compensate       compensate for downward arc // 555 seems a nice substitute for 0.001 compounded 1000 times
             * 
             * motion           the movement of the animation every millisecond
             * 
             * acceleration     positive Y movement every millisecond creates a downward arc
             * 
             */

            targetPlayer = TargetPlayer;

            Rectangle targetRectangle = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, objectIndex, 16, 16);

            Vector2 targetPosition = new(targetVector.X * 64, targetVector.Y * 64);

            Vector2 playerPosition = targetPlayer.Position;

            float animationInterval = 1000f;

            float motionX = (playerPosition.X - targetPosition.X) / 1000;

            float compensate = 0.555f;

            float motionY = ((playerPosition.Y - targetPosition.Y) / 1000) - compensate;

            targetPlayer.currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite("Maps\\springobjects", targetRectangle, animationInterval, 1, 0, targetPosition, flicker: false, flipped: false, targetPosition.Y / 10000f, 0f, Color.White, 4f, 0f, 0f, 0f)
            {
                
                motion = new Vector2(motionX, motionY),

                acceleration = new Vector2(0f, 0.001f),

                timeBasedMotion = true,

                endFunction = InventoriseObject,

            });

            objectInstance = new StardewValley.Object(objectIndex, 1, false, -1, objectQuality);

        }

        public void InventoriseObject(int endBehaviour)
        {

            if (!targetPlayer.addItemToInventoryBool(objectInstance)) // if unable to add to inventory spawn as debris
            {

                Vector2 spawnVector = targetPlayer.getTileLocation();

                Game1.createObjectDebris(objectInstance.ParentSheetIndex, (int)spawnVector.X, (int)spawnVector.Y);

                objectInstance = null;

            }

        }


    }

}
