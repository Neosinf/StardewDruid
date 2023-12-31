﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace StardewDruid.Monster
{
    public class Reaper : Boss
    {
        public int bobHeight;
        public Queue<Rectangle> blastZone;
        public int blastRadius;
        public int ruffleTimer;
        public int ruffleFrame;

        public Reaper()
        {
        }

        public Reaper(Vector2 vector, int CombatModifier)
          : base(vector, CombatModifier, "Reaper")
        {
            blastZone = new Queue<Rectangle>();
        }

        public override void LoadOut()
        {

            if (!Context.IsMainPlayer)
            {

                if (Sprite.loadedTexture == null || Sprite.loadedTexture.Length == 0)
                {

                    Sprite.spriteTexture = MonsterData.MonsterTexture(Name);

                    Sprite.loadedTexture = Sprite.textureName.Value;

                }

            }

            characterTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", Name + ".png"));

            flightFrames = new()
            {
                [0] = new() { new(0, 0, 64, 64), },
                [1] = new() { new(64, 0, 64, 64), },
                [2] = new() { new(256, 0, 64, 64), },
                [3] = new() { new(320, 0, 64, 64), },

            };

            loadedOut = true;

        }

        public override void BaseMode()
        {
            Health = combatModifier * 16;
            MaxHealth = Health;
            DamageToFarmer = (int)(combatModifier * 0.1);
            ouchList = new List<string>()
              {
                "reap",
                "...you cannot defy fate...",
                "...pain..."
              };
            dialogueList = new List<string>()
              {
                "Do not touch the Prime!",
                "How long has it been since I saw...",
                "The dragon's power is mine to use!",
                "I will not stray from my purpose",
                "Are you a spy of the fallen one?",
                "The undervalley... I must...",
                "I will reap, and reap, and reap"
              };

            flightHeight = 4;
            flightFloor = 2;
            flightCeiling = 2; 
            flightLast = 3;
            flightIncrement = 9;
            specialThreshold = 480;
            //reachThreshold = 64;
            behaviourActive = behaviour.halt;
            behaviourTimer = 20;
            //haltActive = true;
            //haltTimer = 20;
            cooldownTimer = 48;
            blastRadius = 1;
            cooldownInterval = 48;
            specialInterval = 24;
        }

        public override void HardMode()
        {
            Health *= 3;
            Health /= 2;
            MaxHealth = Health;
            DamageToFarmer *= 3;
            DamageToFarmer /= 2;
            ouchList = new List<string>()
              {
                "reap",
                "...you cannot defy fate...",
                "I'VE HAD ENOUGH OF THIS"
              };
            dialogueList = new List<string>()
              {
                "The dragon's power is mine to use!",
                "I will not stray from my purpose",
                "Are you a spy of the fallen one?",
                "The undervalley... I must...",
                "I will reap, and reap, and reap",
                "FORTUMEI... PLEASE... I BEG YOU",
                "ALL WILL BE REAPED"
              };
            blastRadius = 2;
            cooldownInterval = 40;
            specialInterval = 18;

            tempermentActive = temperment.aggressive;
        }

        public override Rectangle GetBoundingBox()
        {
            Vector2 position = Position;
            return new Rectangle((int)position.X - 32, (int)position.Y - netFlightHeight.Value - 32, 128, 128);
        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            if (!loadedOut)
            {

                LoadOut();

            }

            Vector2 localPosition = getLocalPosition(Game1.viewport);
            
            float drawLayer = Game1.player.getDrawLayer();
            
            if (IsEmoting && !Game1.eventUp)
            {
                localPosition.Y -= 32 + Sprite.SpriteHeight * 4;
                b.Draw(Game1.emoteSpriteSheet, localPosition, new Rectangle?(new Rectangle(CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, 0, drawLayer);
            }
            
            b.Draw(Game1.shadowTexture, new(localPosition.X, localPosition.Y + 96f), new Rectangle?(Game1.shadowTexture.Bounds), Color.White, 0.0f, Vector2.Zero, 4f, 0, Math.Max(0.0f, getStandingY() / 10000f) - 1E-06f);

            if (netDashActive)
            {
                
                b.Draw(characterTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 160f - netFlightHeight - bobHeight), flightFrames[netFlightFrame][0], Color.White * 0.65f, 0.0f, new Vector2(0.0f, 0.0f), 4f, flip || netDirection == 3 ? (SpriteEffects)1 : 0, drawLayer);

            }
            else if (netSpecialActive)
            {       
                
                b.Draw(characterTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 160f - bobHeight), new Rectangle(128+(ruffleFrame*64), 0, 64, 64), Color.White * 0.65f, 0.0f, new Vector2(0.0f, 0.0f), 4f, flip || netDirection == 3 ? (SpriteEffects)1 : 0, drawLayer);

            }
            else
            {
                
                b.Draw(characterTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 160f - bobHeight), new Rectangle(ruffleFrame*64, 0, 64, 64), Color.White * 0.65f, 0.0f, new Vector2(0.0f, 0.0f), 4f, flip ? (SpriteEffects)1 : 0, drawLayer);

            }

        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {
            
            if (textAboveHeadTimer <= 0 || textAboveHead == null)
            {
                return;
            }
            Vector2 localPosition = getLocalPosition(Game1.viewport);

            SpriteText.drawStringWithScrollCenteredAt(b, textAboveHead, (int)localPosition.X, (int)localPosition.Y - 224, "", textAboveHeadAlpha, textAboveHeadColor, 1, (float)(getTileY() * 64 / 10000.0 + 1.0 / 1000.0 + getTileX() / 10000.0), false);
        
        }

        public override void update(GameTime time, GameLocation location)
        {
            base.update(time, location);

            if (bobHeight <= 0)
            {
                bobHeight++;
            }
            else if (bobHeight >= 64)
            {
                bobHeight--;
            }

            ruffleTimer++;

            if (ruffleTimer == 16)
            {

                ruffleFrame++;

                if(ruffleFrame == 2)
                {

                    ruffleFrame = 0;
                }

                ruffleTimer = 0;

            }

        }

        public override void SpecialAttack()
        {
            Vector2 zero = Vector2.Zero;
            int num = new Random().Next(4);
            switch (moveDirection)
            {
                case 0:

                    zero = new((int)(Position.X / 64.0) + 3, (int)(Position.Y / 64.0) - (4 + num));
                    if (altDirection == 3 || flip)
                    {
                        zero.X -= 6f;
                        break;
                    }
                    break;
                case 1:

                    zero = new((int)(Position.X / 64.0) + (5 + num), (int)(Position.Y / 64.0));
                    break;
                case 2:

                    zero = new((int)(Position.X / 64.0) + 3, (int)(Position.Y / 64.0) + (4 + num));
                    if (altDirection == 3 || flip)
                    {
                        zero.X -= 6f;
                        break;
                    }
                    break;
                default:

                    zero = new((int)(Position.X / 64.0) - (5 + num), (int)(Position.Y / 64.0));
                    break;
            }

            Vector2 zero64 = new(zero.X * 64, zero.Y * 64);
            currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(0, 125f, 4, 1, new(zero64.X - 32 - (32 * blastRadius), zero64.Y - 32 - (32 * blastRadius)), false, false)
            {
                sourceRect = new Rectangle(0, 0, 64, 64),
                sourceRectStartingPos = new Vector2(0.0f, 0.0f),
                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "PurpleBomb.png")),
                scale = 2f + blastRadius,
                timeBasedMotion = true,
                layerDepth = 999f,
                rotationChange = 0.00628f,
                alphaFade = 1f / 1000f
            });
            currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(23, 500f, 6, 1, new Vector2(zero.X * 64f, zero.Y * 64f), false, Game1.random.NextDouble() < 0.5)
            {
                texture = Game1.mouseCursors,
                light = true,
                lightRadius = 2 + blastRadius,
                lightcolor = Color.Black,
                alphaFade = 0.03f,
                Parent = currentLocation
            });
            Rectangle rectangle = new((int)((zero.X - (double)blastRadius) * 64.0) - 32, (int)((zero.Y - (double)blastRadius) * 64.0) - 32, 128 + (blastRadius * 128), 128 + (blastRadius * 128));
            blastZone.Enqueue(rectangle);

            DelayedAction.functionAfterDelay(TriggerBlast, 375);
        }

        public void TriggerBlast()
        {
            ModUtility.DamageFarmers(currentLocation, blastZone.Dequeue(), (int)(DamageToFarmer * 0.6), this);
        }

        public override void SoundSpecial()
        {
            
        }

        public override void SoundFlight()
        {
            
        }

    }

}
