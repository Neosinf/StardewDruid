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
    public class Scavenger : Boss
    {
        public Queue<Rectangle> blastZone;
        public int blastRadius;

        public Scavenger()
        {
        }

        public Scavenger(Vector2 vector, int CombatModifier)
          : base(vector, CombatModifier, "Scavenger")
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
                    new Rectangle(0, 192, 32, 32),
                    new Rectangle(32, 192, 32, 32),
                    new Rectangle(64, 192, 32, 32),
                    new Rectangle(96, 192, 32, 32),
                    new Rectangle(128, 192, 32, 32),
                    new Rectangle(160, 192, 32, 32),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 32, 32),
                    new Rectangle(32, 160, 32, 32),
                    new Rectangle(64, 160, 32, 32),
                    new Rectangle(96, 160, 32, 32),
                    new Rectangle(128, 160, 32, 32),
                    new Rectangle(160, 160, 32, 32),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),
                    new Rectangle(96, 128, 32, 32),
                    new Rectangle(128, 128, 32, 32),
                    new Rectangle(160, 128, 32, 32),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 224, 32, 32),
                    new Rectangle(32, 224, 32, 32),
                    new Rectangle(64, 224, 32, 32),
                    new Rectangle(96, 224, 32, 32),
                    new Rectangle(128, 224, 32, 32),
                    new Rectangle(160, 224, 32, 32),
                }
            };

            specialFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(128, 192, 32, 32),
                },
                [1] = new List<Rectangle>()
                {
                    new Rectangle(128, 160, 32, 32),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(128, 128, 32, 32),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(128, 224, 32, 32),
                }
            };

            loadedOut = true;

        }

        public override Dictionary<int, List<Rectangle>> WalkFrames()
        {

            Dictionary<int, List<Rectangle>> walkFrames = new();

            foreach (KeyValuePair<int, int> keyValuePair in new Dictionary<int, int>()
            {
                [0] = 2,
                [1] = 1,
                [2] = 0,
                [3] = 3
            })
            {
                walkFrames[keyValuePair.Key] = new List<Rectangle>();
                for (int index = 0; index < 6; ++index)
                {
                    Rectangle rectangle = new(0, 0, Sprite.SpriteWidth, Sprite.SpriteHeight);
                    rectangle.X += Sprite.SpriteWidth * index;
                    rectangle.Y += Sprite.SpriteHeight * keyValuePair.Value;
                    walkFrames[keyValuePair.Key].Add(rectangle);
                }

            }

            return walkFrames;

        }

        public override void BaseMode()
        {
            
            Health = combatModifier * 16;

            MaxHealth = Health;

            DamageToFarmer = (int)(combatModifier * 0.1);

            ouchList = new List<string>()
              {
                "meow meow",
              };

            dialogueList = new List<string>()
              {
                "meow",
              };

            flightHeight = 2;

            flightFloor = 1;

            flightCeiling = 4;

            flightLast = 5;

            flightIncrement = 9;

            specialInterval = 48;

            specialCeiling = 4;

            specialFloor = 4;

            cooldownTimer = 60;

            cooldownInterval = 60;

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
                "meow meow",
              };

            dialogueList = new List<string>()
              {
                "meow",
              };

            blastRadius = 2;

            cooldownInterval = 60;

            tempermentActive = temperment.aggressive;

        }

        public override void ChaseMode()
        {
            base.ChaseMode();

            ouchList = new List<string>()
              {
                "meow meow",
              };

            dialogueList = new List<string>()
              {
                "meow",
                "mine mine!",
                "rwwwwrr",
                "where bear"
              };
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
                Vector2 emotePosition = localPosition;
                emotePosition.Y -= 32 + Sprite.SpriteHeight * 4;
                b.Draw(Game1.emoteSpriteSheet, localPosition, new Rectangle?(new Rectangle(CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, 0, drawLayer);
            }

            if (netDashActive)
            {

                b.Draw(characterTexture, new Vector2(localPosition.X - 32f, localPosition.Y - 64f - netFlightHeight), new Rectangle?(flightFrames[netDirection][netFlightFrame]), Color.White, 0, new Vector2(0.0f, 0.0f), 4f, flip ? (SpriteEffects)1 : 0, drawLayer);

            }
            else if (netSpecialActive)
            {

                b.Draw(characterTexture, new Vector2(localPosition.X - 32f, localPosition.Y - 64f), new Rectangle?(specialFrames[netDirection][0]), Color.White, 0.0f, new Vector2(0.0f, 0.0f), 4f, flip ? (SpriteEffects)1 : 0, drawLayer);

                DrawBeamEffect(b);

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

            SpriteText.drawStringWithScrollCenteredAt(b, textAboveHead, (int)localPosition.X + 16, (int)localPosition.Y - 128, "", textAboveHeadAlpha, textAboveHeadColor, 1, (float)(getTileY() * 64 / 10000.0 + 1.0 / 1000.0 + getTileX() / 10000.0), false);
        
        }

        public void DrawBeamEffect(SpriteBatch b)
        {

            Rectangle box = GetBoundingBox();
            float depth = 999f;
            float rotation = 0f;
            float fade = 1f;

            Vector2 position = Vector2.Zero;

            Rectangle rectangle = new(0, netSpecialFrame * 32, 160, 32);

            if (behaviourTimer <= 24)
            {

                fade = 1 - (0.04f * behaviourTimer);

            }

            switch (moveDirection)
            {
                case 0:
                    //position = new(box.Center.X - 48f - (float)Game1.viewport.X, box.Top - 496f - (float)Game1.viewport.Y);
                    position = new(box.Center.X - (float)Game1.viewport.X, box.Top - (float)Game1.viewport.Y);
                    depth = 0.0001f;
                    rotation = ((float)Math.PI / 2) + (float)Math.PI;
                    break;
                case 1:
                    position = new(box.Right - (float)Game1.viewport.X - 32f, box.Center.Y - (float)Game1.viewport.Y - 64f);
                    break;
                case 2:
                    position = new(box.Center.X - (float)Game1.viewport.X + 32f, box.Center.Y - (float)Game1.viewport.Y);
                    rotation = (float)Math.PI / 2;
                    break;
                default:
                    //position = new(box.Left - 496f - (float)Game1.viewport.X, box.Center.Y - 80f - (float)Game1.viewport.Y);
                    position = new(box.Left - (float)Game1.viewport.X - 32f, box.Center.Y - (float)Game1.viewport.Y);
                    rotation = (float)Math.PI;
                    break;

            }

            b.Draw(
                Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EnergyBeam.png")),
                position,
                rectangle,
                Color.White * fade,
                rotation,
                Vector2.Zero,
                3f,
                SpriteEffects.None,
                depth
            );

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

            Rectangle rectangle = new((int)((zero.X - (double)blastRadius) * 64.0) -32, (int)((zero.Y - (double)blastRadius) * 64.0) - 32, 128 + (blastRadius * 128), 128 + (blastRadius * 128));
            
            blastZone.Enqueue(rectangle);

            DelayedAction.functionAfterDelay(TriggerBlast, 375);
        
        }

        public void TriggerBlast()
        {

            currentLocation.playSoundPitched("flameSpellHit", 1200, 0);

            ModUtility.DamageFarmers(currentLocation, blastZone.Dequeue(), DamageToFarmer, this);

        }

        public override void SoundSpecial()
        {

        }

        public override void SoundFlight()
        {

        }

    }

}
