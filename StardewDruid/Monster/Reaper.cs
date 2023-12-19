using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
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
            this.DamageToFarmer = (int)((double)this.combatModifier * 0.1);
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
            Vector2 position = ((Character)this).Position;
            return new Rectangle((int)position.X - 32, (int)position.Y - ((NetFieldBase<int, NetInt>)this.netFlightHeight).Value, 128, 144);
        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            if (((NPC)this).IsInvisible || !Utility.isOnScreen(((Character)this).Position, 128))
                return;
            Vector2 localPosition = ((Character)this).getLocalPosition(Game1.viewport);
            float drawLayer = Game1.player.getDrawLayer();
            if (((Character)this).IsEmoting && !Game1.eventUp)
            {
                localPosition.Y -= (float)(32 + ((Character)this).Sprite.SpriteHeight * 4);
                b.Draw(Game1.emoteSpriteSheet, localPosition, new Rectangle?(new Rectangle(((Character)this).CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, ((Character)this).CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, (SpriteEffects)0, drawLayer);
            }
            b.Draw(Game1.shadowTexture, Vector2.op_Addition(localPosition, new Vector2(0.0f, 128f)), new Rectangle?(Game1.shadowTexture.Bounds), Color.White, 0.0f, Vector2.Zero, 4f, (SpriteEffects)0, Math.Max(0.0f, (float)((Character)this).getStandingY() / 10000f) - 1E-06f);
            if (NetFieldBase<bool, NetBool>.op_Implicit((NetFieldBase<bool, NetBool>)this.netDashActive))
            {
                Rectangle rectangle;
                // ISSUE: explicit constructor call
                ((Rectangle)ref rectangle).\u002Ector(NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netFlightFrame) * 64, 0, 64, 48);
                b.Draw(((Character)this).Sprite.Texture, new Vector2(localPosition.X - 96f, localPosition.Y - 48f - (float)NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netFlightHeight)), new Rectangle?(rectangle), Color.op_Multiply(Color.White, 0.65f), ((NPC)this).rotation, new Vector2(0.0f, 0.0f), 4f, ((Character)this).flip || NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection) == 3 ? (SpriteEffects)1 : (SpriteEffects)0, drawLayer);
            }
            else if (NetFieldBase<bool, NetBool>.op_Implicit((NetFieldBase<bool, NetBool>)this.netFireActive))
            {
                if (this.bobHeight <= 0)
                    ++this.bobHeight;
                else if (this.bobHeight >= 64)
                    --this.bobHeight;
                b.Draw(((Character)this).Sprite.Texture, new Vector2(localPosition.X - 96f, localPosition.Y - 48f - (float)this.bobHeight), new Rectangle?(new Rectangle(256, 0, 64, 48)), Color.op_Multiply(Color.White, 0.65f), ((NPC)this).rotation, new Vector2(0.0f, 0.0f), 4f, ((Character)this).flip || NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection) == 3 ? (SpriteEffects)1 : (SpriteEffects)0, drawLayer);
            }
            else
                b.Draw(((Character)this).Sprite.Texture, new Vector2(localPosition.X - 96f, localPosition.Y - 48f), new Rectangle?(new Rectangle(0, 0, 64, 48)), Color.op_Multiply(Color.White, 0.65f), 0.0f, new Vector2(0.0f, 0.0f), 4f, ((Character)this).flip ? (SpriteEffects)1 : (SpriteEffects)0, drawLayer);
        }

        public virtual void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {
            if (((NPC)this).textAboveHeadTimer <= 0 || ((NPC)this).textAboveHead == null)
                return;
            Vector2 localPosition = ((Character)this).getLocalPosition(Game1.viewport);
            SpriteText.drawStringWithScrollCenteredAt(b, ((NPC)this).textAboveHead, (int)localPosition.X, (int)localPosition.Y - 160, "", ((NPC)this).textAboveHeadAlpha, ((NPC)this).textAboveHeadColor, 1, (float)((double)(((Character)this).getTileY() * 64) / 10000.0 + 1.0 / 1000.0 + (double)((Character)this).getTileX() / 10000.0), false);
        }

        public override void SpecialAttack()
        {
            Vector2 zero = Vector2.Zero;
            int num = new Random().Next(4);
            switch (this.moveDirection)
            {
                case 0:
                    // ISSUE: explicit constructor call
                    ((Vector2)ref zero).\u002Ector((float)((int)((double)((Character)this).Position.X / 64.0) + 3), (float)((int)((double)((Character)this).Position.Y / 64.0) - (4 + num)));
                    if (this.altDirection == 3 || ((Character)this).flip)
                    {
                        zero.X -= 6f;
                        break;
                    }
                    break;
                case 1:
                    // ISSUE: explicit constructor call
                    ((Vector2)ref zero).\u002Ector((float)((int)((double)((Character)this).Position.X / 64.0) + (5 + num)), (float)(int)((double)((Character)this).Position.Y / 64.0));
                    break;
                case 2:
                    // ISSUE: explicit constructor call
                    ((Vector2)ref zero).\u002Ector((float)((int)((double)((Character)this).Position.X / 64.0) + 3), (float)((int)((double)((Character)this).Position.Y / 64.0) + (4 + num)));
                    if (this.altDirection == 3 || ((Character)this).flip)
                    {
                        zero.X -= 6f;
                        break;
                    }
                    break;
                default:
                    // ISSUE: explicit constructor call
                    ((Vector2)ref zero).\u002Ector((float)((int)((double)((Character)this).Position.X / 64.0) - (5 + num)), (float)(int)((double)((Character)this).Position.Y / 64.0));
                    break;
            }
          ((Character)this).currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(0, 125f, 4, 1, Vector2.op_Subtraction(Vector2.op_Subtraction(Vector2.op_Multiply(zero, 64f), new Vector2(32f, 32f)), new Vector2((float)(32 * this.blastRadius), (float)(32 * this.blastRadius))), false, false)
          {
              sourceRect = new Rectangle(0, 0, 64, 64),
              sourceRectStartingPos = new Vector2(0.0f, 0.0f),
              texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EnergyBomb.png")),
              scale = 2f + (float)this.blastRadius,
              timeBasedMotion = true,
              layerDepth = 999f,
              rotationChange = 0.00628f,
              alphaFade = 1f / 1000f
          });
            ((Character)this).currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(23, 500f, 6, 1, new Vector2(zero.X * 64f, zero.Y * 64f), false, Game1.random.NextDouble() < 0.5)
            {
                texture = Game1.mouseCursors,
                light = true,
                lightRadius = (float)(2 + this.blastRadius),
                lightcolor = Color.Black,
                alphaFade = 0.03f,
                Parent = ((Character)this).currentLocation
            });
            Rectangle rectangle;
            // ISSUE: explicit constructor call
            ((Rectangle)ref rectangle).\u002Ector((int)(((double)zero.X - (double)this.blastRadius) * 64.0), (int)(((double)zero.Y - (double)this.blastRadius) * 64.0), 64 + this.blastRadius * 128, 64 + this.blastRadius * 128);
            this.blastZone.Enqueue(rectangle);
            // ISSUE: method pointer
            DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object)this, __methodptr(TriggerBlast)), 375);
        }

        public void TriggerBlast()
        {
            ModUtility.DamageFarmers(((Character)this).currentLocation, this.blastZone.Dequeue(), (int)((double)this.DamageToFarmer * 0.4), (StardewValley.Monsters.Monster)this);
        }
    }
}
