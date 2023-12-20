using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public Reaper()
        {
        }

        public Reaper(Vector2 vector, int CombatModifier)
          : base(vector, CombatModifier, nameof(Reaper))
        {
            this.blastZone = new Queue<Rectangle>();
        }

        public override void LoadOut()
        {
        }

        public override void BaseMode()
        {
            this.Health = this.combatModifier * 12;
            this.MaxHealth = this.Health;
            this.DamageToFarmer = (int)(combatModifier * 0.1);
            this.ouchList = new List<string>()
      {
        "reap",
        "...you cannot defy fate...",
        "...pain..."
      };
            this.dialogueList = new List<string>()
      {
        "Do not touch the Prime!",
        "How long has it been since I saw...",
        "The dragon's power is mine to use!",
        "I will not stray from my purpose",
        "Are you a spy of the fallen one?",
        "The undervalley... I must...",
        "I will reap, and reap, and reap"
      };
            this.flightIncrement = 12;
            this.flightHeight = 4;
            this.flightCeiling = 2;
            this.flightFloor = 2;
            this.flightLast = 3;
            this.flightIncrement = 9;
            this.fireCeiling = 0;
            this.fireFloor = 0;
            this.specialThreshold = 480;
            this.reachThreshold = 64;
            this.haltActive = true;
            this.haltTimer = 20;
            this.cooldownTimer = 48;
            this.blastRadius = 1;
            this.fireInterval = 24;
            this.cooldownInterval = 48;
        }

        public override void HardMode()
        {
            this.Health *= 3;
            this.Health /= 2;
            this.MaxHealth = this.Health;
            this.DamageToFarmer *= 3;
            this.DamageToFarmer /= 2;
            this.ouchList = new List<string>()
      {
        "reap",
        "...you cannot defy fate...",
        "I'VE HAD ENOUGH OF THIS"
      };
            this.dialogueList = new List<string>()
      {
        "The dragon's power is mine to use!",
        "I will not stray from my purpose",
        "Are you a spy of the fallen one?",
        "The undervalley... I must...",
        "I will reap, and reap, and reap",
        "FORTUMEI... PLEASE... I BEG YOU",
        "ALL WILL BE REAPED"
      };
            this.blastRadius = 2;
            this.fireInterval = 18;
            this.cooldownInterval = 40;
        }

        public override Rectangle GetBoundingBox()
        {
            Vector2 position = Position;
            return new Rectangle((int)position.X - 32, (int)position.Y - netFlightHeight.Value, 128, 144);
        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            if (IsInvisible || !Utility.isOnScreen(Position, 128))
                return;
            Vector2 localPosition = getLocalPosition(Game1.viewport);
            float drawLayer = Game1.player.getDrawLayer();
            if (IsEmoting && !Game1.eventUp)
            {
                localPosition.Y -= 32 + Sprite.SpriteHeight * 4;
                b.Draw(Game1.emoteSpriteSheet, localPosition, new Rectangle?(new Rectangle(CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, 0, drawLayer);
            }
            b.Draw(Game1.shadowTexture, new(localPosition.X, localPosition.Y + 128f), new Rectangle?(Game1.shadowTexture.Bounds), Color.White, 0.0f, Vector2.Zero, 4f, 0, Math.Max(0.0f, getStandingY() / 10000f) - 1E-06f);
            if (netDashActive)
            {
                Rectangle rectangle = new(netFlightFrame * 64, 0, 64, 48);
                b.Draw(Sprite.Texture, new Vector2(localPosition.X - 96f, localPosition.Y - 48f - netFlightHeight), new Rectangle?(rectangle), Color.White * 0.65f, rotation, new Vector2(0.0f, 0.0f), 4f, flip || netDirection == 3 ? (SpriteEffects)1 : 0, drawLayer);
            }
            else if (netFireActive)
            {
                if (this.bobHeight <= 0)
                    ++this.bobHeight;
                else if (this.bobHeight >= 64)
                    --this.bobHeight;
                b.Draw(Sprite.Texture, new Vector2(localPosition.X - 96f, localPosition.Y - 48f - bobHeight), new Rectangle?(new Rectangle(256, 0, 64, 48)), Color.White * 0.65f, rotation, new Vector2(0.0f, 0.0f), 4f, flip || netDirection == 3 ? (SpriteEffects)1 : 0, drawLayer);
            }
            else
                b.Draw(Sprite.Texture, new Vector2(localPosition.X - 96f, localPosition.Y - 48f), new Rectangle?(new Rectangle(0, 0, 64, 48)), Color.White * 0.65f, 0.0f, new Vector2(0.0f, 0.0f), 4f, flip ? (SpriteEffects)1 : 0, drawLayer);
        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {
            if (textAboveHeadTimer <= 0 || textAboveHead == null)
                return;
            Vector2 localPosition = getLocalPosition(Game1.viewport);
            SpriteText.drawStringWithScrollCenteredAt(b, textAboveHead, (int)localPosition.X, (int)localPosition.Y - 160, "", textAboveHeadAlpha, textAboveHeadColor, 1, (float)(getTileY() * 64 / 10000.0 + 1.0 / 1000.0 + getTileX() / 10000.0), false);
        }

        public override void SpecialAttack()
        {
            Vector2 zero = Vector2.Zero;
            int num = new Random().Next(4);
            switch (this.moveDirection)
            {
                case 0:
                    // ISSUE: explicit constructor call
                    zero = new((int)(Position.X / 64.0) + 3, (int)(Position.Y / 64.0) - (4 + num));
                    if (this.altDirection == 3 || flip)
                    {
                        zero.X -= 6f;
                        break;
                    }
                    break;
                case 1:
                    // ISSUE: explicit constructor call
                    zero = new((int)(Position.X / 64.0) + (5 + num), (int)(Position.Y / 64.0));
                    break;
                case 2:
                    // ISSUE: explicit constructor call
                    zero = new((int)(Position.X / 64.0) + 3, (int)(Position.Y / 64.0) + (4 + num));
                    if (this.altDirection == 3 || flip)
                    {
                        zero.X -= 6f;
                        break;
                    }
                    break;
                default:
                    // ISSUE: explicit constructor call
                    zero = new((int)(Position.X / 64.0) - (5 + num), (int)(Position.Y / 64.0));
                    break;
            }

            Vector2 zero64 = new(zero.X * 64, zero.Y * 64);
            currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(0, 125f, 4, 1, new(zero64.X - 32 - (32 * blastRadius), zero64.Y - 32 - (32 * blastRadius)), false, false)
            {
                sourceRect = new Rectangle(0, 0, 64, 64),
                sourceRectStartingPos = new Vector2(0.0f, 0.0f),
                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EnergyBomb.png")),
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
                lightRadius = 2 + this.blastRadius,
                lightcolor = Color.Black,
                alphaFade = 0.03f,
                Parent = currentLocation
            });
            Rectangle rectangle = new((int)((zero.X - (double)this.blastRadius) * 64.0), (int)((zero.Y - (double)this.blastRadius) * 64.0), 64 + this.blastRadius * 128, 64 + this.blastRadius * 128);
            this.blastZone.Enqueue(rectangle);

            DelayedAction.functionAfterDelay(TriggerBlast, 375);
        }

        public void TriggerBlast()
        {
            ModUtility.DamageFarmers(currentLocation, this.blastZone.Dequeue(), (int)(DamageToFarmer * 0.4), this);
        }
    }
}
