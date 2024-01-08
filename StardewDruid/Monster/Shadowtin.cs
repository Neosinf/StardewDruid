using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Map;
using StardewValley;
using StardewValley.BellsAndWhistles;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace StardewDruid.Monster
{
    public class Shadowtin : Boss
    {
        public Queue<Rectangle> blastZone;
        public int blastRadius;

        public Shadowtin()
        {
        }

        public Shadowtin(Vector2 vector, int CombatModifier)
          : base(vector, CombatModifier, "Shadowtin")
        {
            blastZone = new Queue<Rectangle>();
        }

        public override void LoadOut()
        {

            characterTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", Name + ".png"));

            walkFrames = WalkFrames();

            flightFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 64, 32),
                    new Rectangle(64, 128, 64, 32),
                    new Rectangle(0, 128, 64, 32),
                    new Rectangle(64, 160, 64, 32),
                    new Rectangle(0, 160, 64, 32),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(64, 128, 64, 32),
                    new Rectangle(0, 128, 64, 32),
                    new Rectangle(64, 160, 64, 32),
                    new Rectangle(0, 160, 64, 32),
                    new Rectangle(64, 128, 64, 32),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 64, 32),
                    new Rectangle(64, 160, 64, 32),
                    new Rectangle(0, 160, 64, 32),
                    new Rectangle(64, 128, 64, 32),
                    new Rectangle(0, 128, 64, 32),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(64, 160, 64, 32),
                    new Rectangle(0, 160, 64, 32),
                    new Rectangle(64, 128, 64, 32),
                    new Rectangle(0, 128, 64, 32),
                    new Rectangle(64, 160, 64, 32),
                }
            };

            specialFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 64, 32),
                },
                [1] = new List<Rectangle>()
                {
                    new Rectangle(64, 128, 64, 32),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 64, 32),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(64, 160, 64, 32),
                }
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
                "unbearable",
                "the power",
                "Deep will not be pleased",
              };

            dialogueList = new List<string>()
              {
                "bear",
                "I can't believe it",
                "A Dragon? Here? Now?",
                "this changes everything",
                "show me your strength",
                "so you defeated the scouts"
              };

            flightHeight = 2;

            flightFloor = 0;

            flightCeiling = 3;

            flightLast = 4;

            flightIncrement = 9;

            specialInterval = 24;

            cooldownTimer = 48;

            cooldownInterval = 48;

            blastRadius = 1;

            behaviourActive = behaviour.idle;

            tempermentActive = temperment.cautious;
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
                "unbearable",
                "the power",
                "Deep will not be pleased",
              };

            dialogueList = new List<string>()
              {
                "bear",
                "I can't believe it",
                "A Dragon? Here? Now?",
                "this changes everything",
                "show me your strength",
                "so you defeated the scouts"
              };

            blastRadius = 2;
            
            specialInterval = 18;
            
            cooldownInterval = 40;

            tempermentActive = temperment.aggressive;

        }

        public override Rectangle GetBoundingBox()
        {
            Vector2 position = Position;
            return new Rectangle((int)position.X - 16, (int)position.Y - netFlightHeight.Value - 32, 96, 96);
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
                Vector2 emotePosition = localPosition;
                emotePosition.Y -= 32 + Sprite.SpriteHeight * 4;
                b.Draw(Game1.emoteSpriteSheet, localPosition, new Rectangle?(new Rectangle(CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, 0, drawLayer);
            }

            if (netDashActive)
            {

                b.Draw(characterTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 64f - netFlightHeight), new Rectangle?(flightFrames[netDirection][netFlightFrame]), Color.White, 0, new Vector2(0.0f, 0.0f), 4f, flip || netDirection == 3 ? (SpriteEffects)1 : 0, drawLayer);

            }
            else if (netSpecialActive)
            {

                b.Draw(characterTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 64f), new Rectangle?(specialFrames[netDirection][0]), Color.White, 0.0f, new Vector2(0.0f, 0.0f), 4f, flip ? (SpriteEffects)1 : 0, drawLayer);

            }
            else
            {

                b.Draw(characterTexture, new Vector2(localPosition.X - 32f, localPosition.Y - 64f), new Rectangle?(walkFrames[netDirection][netWalkFrame]), Color.White, 0.0f, new Vector2(0.0f, 0.0f), 4f, flip ? (SpriteEffects)1 : 0, drawLayer);

            }

            b.Draw(Game1.shadowTexture, new(localPosition.X, localPosition.Y + 32f), new Rectangle?(Game1.shadowTexture.Bounds), Color.White, 0.0f, Vector2.Zero, 4f, 0, drawLayer - 1E-06f);

        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {
            
            if (textAboveHeadTimer <= 0 || textAboveHead == null)
            {
                return;
            }
            Vector2 localPosition = getLocalPosition(Game1.viewport);

            SpriteText.drawStringWithScrollCenteredAt(b, textAboveHead, (int)localPosition.X + 16, (int)localPosition.Y - 144, "", textAboveHeadAlpha, textAboveHeadColor, 1, (float)(getTileY() * 64 / 10000.0 + 1.0 / 1000.0 + getTileX() / 10000.0), false);
        
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
                lightRadius = 2 + blastRadius,
                lightcolor = Color.Black,
                alphaFade = 0.03f,
                Parent = currentLocation
            });
            Rectangle rectangle = new((int)((zero.X - (double)blastRadius) * 64.0) -32, (int)((zero.Y - (double)blastRadius) * 64.0) - 32, 128 + (blastRadius * 128), 128 + (blastRadius * 128));
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
