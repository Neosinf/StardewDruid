using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;

namespace StardewDruid.Monster
{
    public class BossSlime : StardewValley.Monsters.BigSlime
    {
        public List<string> ouchList;
        
        public bool posturing = true;

        public Texture2D hatsTexture;

        public Rectangle hatSourceRect;

        public Vector2 hatOffset;

        public Dictionary<int, Vector2> hatOffsets;

        public BossSlime(Vector2 position, int combatModifier)
            : base(position * 64, combatModifier /2)
        {
            
            base.Health = (int)(combatModifier * 8);

            base.MaxHealth = (int)(combatModifier * 8);

            base.DamageToFarmer = (int)Math.Max(2, combatModifier * 0.1);

            ouchList = new()
            {
                "bloop",
                "bloop bloop",
                "jelly superiority!"
            };

            hatsTexture = Game1.content.Load<Texture2D>("Characters\\Farmer\\hats");

            //hatSourceRect = Game1.getSourceRectForStandardTileSheet(hatsTexture, 151, 20, 20);
            hatSourceRect = Game1.getSourceRectForStandardTileSheet(hatsTexture, 192, 20, 20);



        }

        public override void defaultMovementBehavior(GameTime time)
        {

            if (posturing)
            { 
                return; 
            }

            base.defaultMovementBehavior(time);

        }

        public override void behaviorAtGameTick(GameTime time)
        {
            if (posturing)
            { 
                return; 
            }

            base.behaviorAtGameTick(time);

        }

        public override void updateMovement(GameLocation location, GameTime time)
        {

            if (posturing)
            { 
                return; 
            }

            base.updateMovement(location, time);

        }

        public override int takeDamage(int damage, int xTrajectory, int yTrajectory, bool isBomb, double addedPrecision, Farmer who)
        {

            if (posturing)
            { 
                return 0; 
            }

            int ouchIndex = Game1.random.Next(15);

            if (ouchIndex < ouchList.Count)
            {
                showTextAboveHead(ouchList[ouchIndex], duration: 3000);

            }

            base.Slipperiness = 3;

            base.Health -= damage;

            setTrajectory(xTrajectory, yTrajectory);

            base.currentLocation.playSound("hitEnemy");

            base.IsWalkingTowardPlayer = true;

            if (base.Health <= 0)
            {
                deathAnimation();

                Item happyHat = new Hat(48);

                who.dropItem(happyHat);

            }

            return damage;

        }

        public override void draw(SpriteBatch b)
        {
            //base.draw(b);

            // ----------------- hats

            if (!IsInvisible && Utility.isOnScreen(Position, 128))
            {

                b.Draw(
                    Sprite.Texture, 
                    getLocalPosition(Game1.viewport) + new Vector2(56f, 16 + yJumpOffset), 
                    Sprite.SourceRect,
                    Color.Orange*0.7f,
                    rotation,
                    new Vector2(16f, 16f), 
                    6f, 
                    flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 
                    Math.Max(0f, drawOnTop ? 0.991f : ((float)getStandingY() / 10000f))
                );

                hatOffsets = new()
                {
                    [0] = new Vector2(0, 6),
                    [1] = new Vector2(0, 0),
                    [2] = new Vector2(0, -6),
                    [3] = new Vector2(0, -12),
                    [4] = new Vector2(0, -18),
                    [5] = new Vector2(0, -12),
                    [6] = new Vector2(0, -6),
                    [7] = new Vector2(0, 0),
                };

                hatOffset = hatOffsets[Sprite.currentFrame];

                Vector2 localPosition = getLocalPosition(Game1.viewport) + new Vector2(56f, 16 + yJumpOffset);

                b.Draw(
                    hatsTexture,
                    localPosition + hatOffset,
                    hatSourceRect,
                    Color.White * 0.90f,
                    0f,
                    //new Vector2(10f, 16f),
                    new Vector2(10f, 9f),
                    6f,
                    flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    Math.Max(0f, drawOnTop ? 0.990f : ((float)getStandingY() / 10000f)-0.00005f)
                );

            }

        }

    }

}
