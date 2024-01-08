using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Event.World;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Shadowtin : StardewDruid.Character.Character
    {
        public int xOffset;

        public Shadowtin()
        {
        }

        public Shadowtin(Vector2 position, string map)
          : base(position, map, nameof(Shadowtin))
        {
            
            HideShadow = true;
        
        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            if (!Context.IsMainPlayer)
            {
                base.draw(b, alpha);

                return;
            }

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            if (base.IsEmoting && !Game1.eventUp)
            {
                Vector2 localPosition2 = getLocalPosition(Game1.viewport);
                localPosition2.Y -= 32 + Sprite.SpriteHeight * 4;
                b.Draw(Game1.emoteSpriteSheet, localPosition2, new Microsoft.Xna.Framework.Rectangle(base.CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, base.CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, getStandingY() / 10000f);
            }

            Vector2 localPosition = getLocalPosition(Game1.viewport);

            if (timers.ContainsKey("idle"))
            {

                int idleFrame = timers["idle"] / 80 % 4;

                List<Rectangle> sweepRect = new()
                {
                    new Rectangle(0, 128, 64, 32),
                    new Rectangle(64, 160, 64, 32),
                    new Rectangle(0, 160, 64, 32),
                    new Rectangle(64, 128, 64, 32),

                };

                b.Draw(
                    Sprite.Texture,
                    localPosition - new Vector2(96, 64f),
                    sweepRect[idleFrame],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    4f,
                    flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    Math.Max(0f, drawOnTop ? 0.991f : (getStandingY() / 10000f))
                );

                b.Draw(
                    Game1.shadowTexture,
                    localPosition + new Vector2(0, 56f),
                    Game1.shadowTexture.Bounds,
                    Color.White * alpha, 0f,
                    Vector2.Zero,
                    4f,
                    SpriteEffects.None,
                    Math.Max(0.0f, getStandingY() / 10000f) - 0.0001f
                    );


                return;

            }

            if (timers.ContainsKey("sweep"))
            {
                List<Rectangle> sweepRect = new()
                {
                    new Rectangle(0, 128, 64, 32),
                    new Rectangle(64, 160, 64, 32),
                    new Rectangle(0, 160, 64, 32),
                    new Rectangle(64, 128, 64, 32),

                };

                Dictionary<int, List<int>> sweepPath = new()
                {

                    [0] = new() { 2, 3, 0, 1, 2 },
                    [1] = new() { 1, 2, 3, 0, 1 },
                    [2] = new() { 0, 1, 2, 3, 0 },
                    [3] = new() { 3, 0, 1, 2, 3 },

                };

                int sweepIndex = (30 - timers["sweep"]) / 6;

                b.Draw(
                    Sprite.Texture,
                    localPosition - new Vector2(96, 64f),
                    sweepRect[sweepPath[moveDirection][sweepIndex]],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    4f,
                    flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    Math.Max(0f, drawOnTop ? 0.991f : (getStandingY() / 10000f))
                );

                b.Draw(
                    Game1.shadowTexture,
                    localPosition + new Vector2(0, 56f),
                    Game1.shadowTexture.Bounds,
                    Color.White * alpha, 0f,
                    Vector2.Zero,
                    4f,
                    SpriteEffects.None,
                    Math.Max(0.0f, getStandingY() / 10000f) - 0.0001f
                    );

                return;

            }

            b.Draw(
                Sprite.Texture,
                localPosition - new Vector2(32, 64),
                Sprite.SourceRect,
                Color.White,
                0f,
                Vector2.Zero,
                4f,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                Math.Max(0f, drawOnTop ? 0.991f : (getStandingY() / 10000f))
            );

            b.Draw(
                Game1.shadowTexture,
                localPosition + new Vector2(8, 56f),
                Game1.shadowTexture.Bounds,
                Color.White * alpha, 0f,
                Vector2.Zero,
                4f,
                SpriteEffects.None,
                Math.Max(0.0f, getStandingY() / 10000f) - 0.0001f
                );

        }

        public override Rectangle GetBoundingBox()
        {

            return new Rectangle ((int)Position.X, (int)Position.Y + 8, 64, 56);

        }

        public override Rectangle GetHitBox()
        {
            return new Rectangle((int)Position.X-32, (int)Position.Y -32, 128, 128);
        }

        public override void AnimateMovement(GameTime time)
        {

            flip = false;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
            moveUp = false;
            FacingDirection = moveDirection;
            Sprite.interval = 175f;
            xOffset = 0;
            switch (moveDirection)
            {
                case 0:
                    Sprite.AnimateUp(time, 0, "");
                    moveUp = true;
                    if (altDirection == 3)
                    {
                        flip = true;
                        break;
                    }
                    break;
                case 1:
                    Sprite.AnimateRight(time, 0, "");
                    moveRight = true;
                    break;
                case 2:
                    Sprite.AnimateDown(time, 0, "");
                    moveDown = true;
                    if (altDirection == 3)
                    {
                        flip = true;
                        break;
                    }
                    break;
                default:
                    moveLeft = true;
                    Sprite.AnimateLeft(time, 0, "");
                    break;
            }

        }

        public override bool TargetOpponent()
        {
            if (!base.TargetOpponent())
            {

                return false;
            
            }
                
            float distance = Vector2.Distance(Position, targetOpponents.First().Position);

            if (distance >= 64f && distance <= 480f)
            {

                StardewValley.Monsters.Monster targetMonster = targetOpponents.First();

                Vector2 vector2 = new(targetMonster.Position.X - Position.X - 32f, targetMonster.Position.Y - Position.Y);


            }
            
            return true;

        }

        public override void update(GameTime time, GameLocation location)
        {

            base.update(time, location);
        
        }

        public override bool checkAction(Farmer who, GameLocation l)
        {
            if (!base.checkAction(who, l))
            {
                
                return false;

            }
                
            if (!Mod.instance.dialogue.ContainsKey(nameof(Shadowtin)))
            {
                
                Dictionary<string, StardewDruid.Dialogue.Dialogue> dialogue = Mod.instance.dialogue;
                
                StardewDruid.Dialogue.Shadowtin shadowtin = new StardewDruid.Dialogue.Shadowtin();

                shadowtin.npc = this;
                
                dialogue[nameof(Shadowtin)] = shadowtin;
            
            }
            
            Mod.instance.dialogue[nameof(Shadowtin)].DialogueApproach();
            
            return true;
        
        }

        public override void HitMonster(StardewValley.Monsters.Monster monsterCharacter)
        {

            DealDamageToMonster(monsterCharacter,true);

            timers["sweep"] = 29;

        }

    }
}
