
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace StardewDruid.Monster
{
    public class Boss : StardewValley.Monsters.Monster
    {
        public List<string> ouchList;
        public List<string> dialogueList;
        public bool defeated;
        public int combatModifier;
        public NetInt netDirection = new NetInt(0);
        public NetInt netWalkFrame = new NetInt(0);
        public NetInt netAlternative = new NetInt(0);
        public Dictionary<int, List<Rectangle>> walkFrames;
        public int moveDirection;
        public int altDirection;
        public bool haltActive;
        public NetBool netHaltActive = new NetBool(false);
        public int haltTimer;
        public double angleDirection;
        public Vector2 trackPosition;
        public bool cooldownActive;
        public int cooldownTimer;
        public int cooldownInterval;
        public Texture2D shadowTexture;
        public List<Rectangle> shadowFrames;
        public NetInt netFlightFrame = new NetInt(0);
        public NetInt netFlightHeight = new NetInt(0);
        public NetBool netDashActive = new NetBool(false);
        public Texture2D flightTexture;
        public Dictionary<int, List<Rectangle>> flightFrames;
        public bool flightActive;
        public int flightTimer;
        public Vector2 flightPosition;
        public Vector2 flightTo;
        public Vector2 flightInterval;
        public bool flightFlip;
        public int flightIncrement;
        public int flightExtend;
        public int flightHeight;
        public int flightCeiling;
        public int flightFloor;
        public int flightLast;
        public NetInt netFireFrame = new NetInt(0);
        public NetBool netFireActive = new NetBool(false);
        public Texture2D specialTexture;
        public bool specialActive;
        public int specialTimer;
        public Texture2D fireTexture;
        public Dictionary<int, List<Rectangle>> fireFrames;
        public List<Vector2> fireVectors;
        public int fireTimer;
        public int fireCeiling;
        public int fireFloor;
        public int fireInterval;
        public Vector2 followTarget;
        public bool followActive;
        public int followTimer;
        public Vector2 followInterval;
        public int specialThreshold;
        public int reachThreshold;

        public Boss()
        {
        }

        public Boss(Vector2 vector, int CombatModifier, string name = "PurpleDragon")
          : base("Pepper Rex", Vector2.op_Multiply(vector, 64f))
        {
            ((Character)this).Name = name;
            this.combatModifier = CombatModifier;
            ((NetList<int, NetInt>)this.objectsToDrop).Clear();
            ((NetFieldBase<bool, NetBool>)((NPC)this).breather).Value = false;
            ((NetFieldBase<bool, NetBool>)((NPC)this).hideShadow).Value = true;
            ((Character)this).Sprite = CharacterData.CharacterSprite(((Character)this).Name);
            this.BaseMode();
            this.LoadOut();
        }

        public virtual void BaseMode()
        {
            this.Health = this.combatModifier * 12;
            this.MaxHealth = this.Health;
            this.DamageToFarmer = (int)((double)this.combatModifier * 0.1);
            this.ouchList = new List<string>()
      {
        "Ah ha ha ha ha",
        "I'll Answer That... With FIRE!",
        "insolence!",
        "creep"
      };
            this.dialogueList = new List<string>()
      {
        "behold",
        "I am your new master",
        "Kneel Before Tyrannus!",
        "Why do you resist",
        "Where are my servants?"
      };
            this.flightIncrement = 12;
            this.fireInterval = 12;
            this.flightHeight = 8;
            this.specialThreshold = 480;
            this.reachThreshold = 64;
            this.haltActive = true;
            this.haltTimer = 20;
            this.cooldownInterval = 60;
            this.cooldownTimer = this.cooldownInterval;
        }

        public virtual void LoadOut()
        {
            this.walkFrames = new Dictionary<int, List<Rectangle>>();
            foreach (KeyValuePair<int, int> keyValuePair in new Dictionary<int, int>()
            {
                [0] = 2,
                [1] = 1,
                [2] = 0,
                [3] = 3
            })
            {
                this.walkFrames[keyValuePair.Key] = new List<Rectangle>();
                for (int index = 0; index < 4; ++index)
                {
                    Rectangle rectangle;
                    // ISSUE: explicit constructor call
                    ((Rectangle)ref rectangle).\u002Ector(0, 0, 64, 64);
                    rectangle.X += 64 * index;
                    rectangle.Y += 64 * keyValuePair.Value;
                    this.walkFrames[keyValuePair.Key].Add(rectangle);
                }
            }
            this.shadowTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DragonShadow.png"));
            this.shadowFrames = new List<Rectangle>()
      {
        new Rectangle(0, 0, 64, 32),
        new Rectangle(0, 32, 64, 32),
        new Rectangle(0, 64, 64, 32),
        new Rectangle(0, 32, 64, 32),
        new Rectangle(64, 0, 64, 32),
        new Rectangle(64, 32, 64, 32),
        new Rectangle(64, 64, 64, 32),
        new Rectangle(64, 32, 64, 32)
      };
            this.flightTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", NetFieldBase<string, NetString>.op_Implicit((NetFieldBase<string, NetString>)((Character)this).name) + "Flight.png"));
            this.flightFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new List<Rectangle>()
        {
          new Rectangle(0, 64, 128, 64),
          new Rectangle(128, 64, 128, 64),
          new Rectangle(256, 64, 128, 64),
          new Rectangle(384, 64, 128, 64),
          new Rectangle(256, 64, 128, 64),
          new Rectangle(0, 64, 128, 64)
        },
                [1] = new List<Rectangle>()
        {
          new Rectangle(0, 0, 128, 64),
          new Rectangle(128, 0, 128, 64),
          new Rectangle(256, 0, 128, 64),
          new Rectangle(384, 0, 128, 64),
          new Rectangle(256, 0, 128, 64),
          new Rectangle(0, 0, 128, 64)
        },
                [2] = new List<Rectangle>()
        {
          new Rectangle(0, 128, 128, 64),
          new Rectangle(128, 128, 128, 64),
          new Rectangle(256, 128, 128, 64),
          new Rectangle(384, 128, 128, 64),
          new Rectangle(256, 128, 128, 64),
          new Rectangle(0, 128, 128, 64)
        },
                [3] = new List<Rectangle>()
        {
          new Rectangle(0, 0, 128, 64),
          new Rectangle(128, 0, 128, 64),
          new Rectangle(256, 0, 128, 64),
          new Rectangle(384, 0, 128, 64),
          new Rectangle(256, 0, 128, 64),
          new Rectangle(0, 0, 128, 64)
        }
            };
            this.flightCeiling = 4;
            this.flightFloor = 1;
            this.flightLast = 5;
            this.specialTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", NetFieldBase<string, NetString>.op_Implicit((NetFieldBase<string, NetString>)((Character)this).name) + "Breath.png"));
            this.fireTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DragonFire.png"));
            this.fireFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new List<Rectangle>()
        {
          new Rectangle(0, 0, 64, 32),
          new Rectangle(64, 0, 64, 32),
          new Rectangle(128, 0, 64, 32),
          new Rectangle(192, 0, 64, 32)
        },
                [1] = new List<Rectangle>()
        {
          new Rectangle(0, 32, 64, 32),
          new Rectangle(64, 32, 64, 32),
          new Rectangle(128, 32, 64, 32),
          new Rectangle(192, 32, 64, 32)
        },
                [2] = new List<Rectangle>()
        {
          new Rectangle(0, 64, 64, 32),
          new Rectangle(64, 64, 64, 32),
          new Rectangle(128, 64, 64, 32),
          new Rectangle(192, 64, 64, 32)
        },
                [3] = new List<Rectangle>()
        {
          new Rectangle(0, 32, 64, 32),
          new Rectangle(64, 32, 64, 32),
          new Rectangle(128, 32, 64, 32),
          new Rectangle(192, 32, 64, 32)
        }
            };
            this.fireCeiling = 3;
            this.fireFloor = 2;
        }

        public virtual void HardMode()
        {
            this.Health *= 3;
            this.Health /= 2;
            this.MaxHealth = this.Health;
            this.DamageToFarmer *= 3;
            this.DamageToFarmer /= 2;
            this.fireInterval = 9;
            this.ouchList = new List<string>()
      {
        "Ah ha ha ha ha",
        "Such pitiful strikes",
        "insolence!",
        "The land has died... and so WILL YOU",
        "CREEP"
      };
            this.dialogueList = new List<string>()
      {
        "Where are my servants",
        "The shamans have failed me",
        "The only recourse for humanity, is subjugation",
        "Beg for my mercy",
        "Kneel Before Tyrannus Steve!",
        "I WILL BURNINATE... EVERYTHING"
      };
            this.cooldownInterval = 48;
        }

        public virtual Rectangle GetBoundingBox()
        {
            Vector2 position = ((Character)this).Position;
            return new Rectangle((int)position.X - 72, (int)position.Y - 32 - ((NetFieldBase<int, NetInt>)this.netFlightHeight).Value, 196, 128);
        }

        public virtual void draw(SpriteBatch b, float alpha = 1f)
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
            if (NetFieldBase<bool, NetBool>.op_Implicit((NetFieldBase<bool, NetBool>)this.netDashActive))
            {
                b.Draw(this.flightTexture, new Vector2(localPosition.X - 128f, localPosition.Y - 192f - (float)NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netFlightHeight)), new Rectangle?(this.flightFrames[NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection)][NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netFlightFrame)]), Color.op_Multiply(Color.White, 0.65f), ((NPC)this).rotation, new Vector2(0.0f, 0.0f), 4f, ((Character)this).flip || NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection) == 3 ? (SpriteEffects)1 : (SpriteEffects)0, drawLayer);
                b.Draw(this.shadowTexture, new Vector2(localPosition.X - 80f, localPosition.Y - 88f), new Rectangle?(this.shadowFrames[NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection) + 4]), Color.op_Multiply(Color.White, 0.25f), 0.0f, new Vector2(0.0f, 0.0f), 5f, ((Character)this).flip || NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection) == 3 ? (SpriteEffects)1 : (SpriteEffects)0, drawLayer - 1E-05f);
            }
            else
            {
                if (NetFieldBase<bool, NetBool>.op_Implicit((NetFieldBase<bool, NetBool>)this.netFireActive))
                {
                    b.Draw(this.specialTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 192f), new Rectangle?(this.walkFrames[NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection)][NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netWalkFrame)]), Color.op_Multiply(Color.White, 0.65f), 0.0f, new Vector2(0.0f, 0.0f), 4f, ((Character)this).flip ? (SpriteEffects)1 : (SpriteEffects)0, drawLayer);
                    this.DrawFire(b, localPosition, NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection), drawLayer);
                }
                else
                    b.Draw(((Character)this).Sprite.Texture, new Vector2(localPosition.X - 96f, localPosition.Y - 192f), new Rectangle?(this.walkFrames[NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection)][NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netWalkFrame)]), Color.op_Multiply(Color.White, 0.65f), 0.0f, new Vector2(0.0f, 0.0f), 4f, ((Character)this).flip ? (SpriteEffects)1 : (SpriteEffects)0, drawLayer);
                b.Draw(this.shadowTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 72f), new Rectangle?(this.shadowFrames[NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection)]), Color.op_Multiply(Color.White, 0.25f), 0.0f, new Vector2(0.0f, 0.0f), 4f, ((Character)this).flip || NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection) == 3 ? (SpriteEffects)1 : (SpriteEffects)0, drawLayer - 1E-05f);
            }
        }

        public virtual void SpecialAttack()
        {
            Vector2 zero = Vector2.Zero;
            Rectangle zone;
            switch (this.moveDirection)
            {
                case 0:
                    // ISSUE: explicit constructor call
                    ((Vector2)ref zero).\u002Ector((float)((int)((double)((Character)this).Position.X / 64.0) + 2), (float)((int)((double)((Character)this).Position.Y / 64.0) - 4));
                    if (this.altDirection == 3 || ((Character)this).flip)
                        zero.X -= 4f;
                    // ISSUE: explicit constructor call
                    ((Rectangle)ref zone).\u002Ector((int)(((double)zero.X - 3.0) * 64.0), (int)(((double)zero.Y - 1.0) * 64.0), 448, 256);
                    break;
                case 1:
                    // ISSUE: explicit constructor call
                    ((Vector2)ref zero).\u002Ector((float)((int)((double)((Character)this).Position.X / 64.0) + 5), (float)(int)((double)((Character)this).Position.Y / 64.0));
                    // ISSUE: explicit constructor call
                    ((Rectangle)ref zone).\u002Ector((int)(((double)zero.X - 1.0) * 64.0), (int)(((double)zero.Y - 3.0) * 64.0), 256, 448);
                    break;
                case 2:
                    // ISSUE: explicit constructor call
                    ((Vector2)ref zero).\u002Ector((float)((int)((double)((Character)this).Position.X / 64.0) + 3), (float)((int)((double)((Character)this).Position.Y / 64.0) + 1));
                    if (this.altDirection == 3 || ((Character)this).flip)
                        zero.X -= 4f;
                    // ISSUE: explicit constructor call
                    ((Rectangle)ref zone).\u002Ector((int)(((double)zero.X - 3.0) * 64.0), (int)(((double)zero.Y - 1.0) * 64.0), 448, 256);
                    break;
                default:
                    // ISSUE: explicit constructor call
                    ((Vector2)ref zero).\u002Ector((float)((int)((double)((Character)this).Position.X / 64.0) - 4), (float)(int)((double)((Character)this).Position.Y / 64.0));
                    // ISSUE: explicit constructor call
                    ((Rectangle)ref zone).\u002Ector((int)(((double)zero.X - 1.0) * 64.0), (int)(((double)zero.Y - 3.0) * 64.0), 256, 448);
                    break;
            }
          ((Character)this).currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(23, 200f, 6, 1, new Vector2(zero.X * 64f, zero.Y * 64f), false, Game1.random.NextDouble() < 0.5)
          {
              texture = Game1.mouseCursors,
              light = true,
              lightRadius = 3f,
              lightcolor = Color.Black,
              alphaFade = 0.03f,
              Parent = ((Character)this).currentLocation
          });
            ModUtility.DamageFarmers(((Character)this).currentLocation, zone, (int)((double)this.DamageToFarmer * 0.4), (StardewValley.Monsters.Monster)this);
        }

        public virtual void DrawFire(
          SpriteBatch b,
          Vector2 position,
          int direction,
          float depth,
          int adjust = 0)
        {
            this.fireVectors = new List<Vector2>()
      {
        new Vector2(44f, -336f),
        new Vector2(-348f, -336f),
        new Vector2(148f, -96f),
        new Vector2(60f, -106f),
        new Vector2(-380f, -106f),
        new Vector2(-400f, -96f)
      };
            float num1 = 5f;
            depth += 1f / 1000f;
            int num2;
            switch (direction)
            {
                case 0:
                    num2 = 0;
                    if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netAlternative) == 3)
                        num2 = 1;
                    num1 = 6f;
                    depth -= 1f / 500f;
                    break;
                case 1:
                    num2 = 2;
                    break;
                case 2:
                    num2 = 3;
                    if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netAlternative) == 3)
                        num2 = 4;
                    num1 = 6f;
                    break;
                default:
                    num2 = 5;
                    break;
            }
            int index = num2 + adjust;
            b.Draw(this.fireTexture, Vector2.op_Addition(position, this.fireVectors[index]), new Rectangle?(this.fireFrames[direction][NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netFireFrame)]), Color.op_Multiply(Color.White, 0.65f), ((NPC)this).rotation, new Vector2(0.0f, 0.0f), num1, ((Character)this).flip || direction == 3 ? (SpriteEffects)1 : (SpriteEffects)0, depth);
        }

        protected virtual void initNetFields()
        {
            base.initNetFields();
            ((Character)this).NetFields.AddFields(new INetSerializable[8]
            {
        (INetSerializable) this.netDirection,
        (INetSerializable) this.netWalkFrame,
        (INetSerializable) this.netAlternative,
        (INetSerializable) this.netFlightFrame,
        (INetSerializable) this.netFlightHeight,
        (INetSerializable) this.netDashActive,
        (INetSerializable) this.netFireFrame,
        (INetSerializable) this.netFireActive
            });
        }

        public virtual void reloadSprite()
        {
            ((Character)this).Sprite = MonsterData.MonsterSprite(((Character)this).Name);
            ((NPC)this).HideShadow = true;
        }

        public virtual void shedChunks(int number)
        {
        }

        protected virtual void sharedDeathAnimation()
        {
            ((Character)this).currentLocation.playSound("skeletonDie", (NetAudio.SoundContext)0);
            ((Character)this).currentLocation.playSound("grunt", (NetAudio.SoundContext)0);
        }

        protected virtual void localDeathAnimation()
        {
            Utility.makeTemporarySpriteJuicier(new TemporaryAnimatedSprite(44, ((Character)this).Position, Color.HotPink, 10, false, 100f, 0, -1, -1f, -1, 0)
            {
                holdLastFrame = true,
                alphaFade = 0.01f,
                interval = 70f
            }, ((Character)this).currentLocation, 8, 96, 64);
        }

        public virtual List<Item> getExtraDropItems() => new List<Item>();

        public virtual int takeDamage(
          int damage,
          int xTrajectory,
          int yTrajectory,
          bool isBomb,
          double addedPrecision,
          Farmer who)
        {
            int damage1 = Math.Max(1, damage - ((NetFieldBase<int, NetInt>)this.resilience).Value);
            this.Health -= damage1;
            if (Game1.random.Next(5) == 0)
                ((Character)this).setTrajectory(xTrajectory, yTrajectory);
            if (this.Health <= 0)
            {
                this.deathAnimation();
                this.defeated = true;
            }
            int index = Game1.random.Next(15);
            if (index < this.ouchList.Count)
                ((NPC)this).showTextAboveHead(this.ouchList[index], -1, 2, 3000, 0);
            return damage1;
        }

        public virtual void behaviorAtGameTick(GameTime time)
        {
        }

        public virtual void updateMovement(GameLocation location, GameTime time)
        {
        }

        protected virtual void updateAnimation(GameTime time)
        {
        }

        protected virtual void updateMonsterSlaveAnimation(GameTime time)
        {
        }

        public virtual void Halt()
        {
            if (this.haltTimer <= 0)
            {
                this.haltActive = true;
                this.haltTimer = 60;
                if (Context.IsMainPlayer)
                    ((NetFieldBase<bool, NetBool>)this.netHaltActive).Set(true);
            }
            this.flightActive = false;
            this.specialActive = false;
            this.followActive = false;
            if (!Context.IsMainPlayer)
                return;
            ((NetFieldBase<int, NetInt>)this.netWalkFrame).Set(0);
            ((NetFieldBase<bool, NetBool>)this.netDashActive).Set(false);
            ((NetFieldBase<bool, NetBool>)this.netFireActive).Set(false);
        }

        public virtual void update(GameTime time, GameLocation location)
        {
            if (Mod.instance.CasterBusy())
                return;
            if (!Context.IsMainPlayer)
            {
                if (((Character)this).Sprite.loadedTexture == null || ((Character)this).Sprite.loadedTexture.Length == 0)
                {
                    ((Character)this).Sprite.spriteTexture = CharacterData.CharacterTexture(NetFieldBase<string, NetString>.op_Implicit((NetFieldBase<string, NetString>)((Character)this).name));
                    ((Character)this).Sprite.loadedTexture = ((NetFieldBase<string, NetString>)((Character)this).Sprite.textureName).Value;
                    this.LoadOut();
                }
                base.update(time, location);
                this.FixDirection();
            }
            else
            {
                base.update(time, location);
                this.ChooseBehaviour(time);
                this.FixDirection();
                if (this.haltActive)
                    this.UpdateHalt(time);
                if (this.flightActive)
                    this.UpdateFlight(time);
                if (this.followActive)
                    this.UpdateFollow(time);
                if (this.specialActive)
                    this.UpdateSpecial();
                if (!this.cooldownActive)
                    return;
                this.UpdateCooldown();
            }
        }

        public void ChooseBehaviour(GameTime time)
        {
            if ((double)((Character)this).xVelocity != 0.0 || (double)((Character)this).yVelocity != 0.0)
            {
                ((Character)this).xVelocity = 0.0f;
                ((Character)this).yVelocity = 0.0f;
                ((Character)this).Halt();
            }
            if (this.haltActive || this.flightActive || this.specialActive || this.followActive)
                return;
            Random random = new Random();
            List<Farmer> source = this.TargetFarmers();
            if (source.Count > 0)
            {
                Farmer farmer = source.First<Farmer>();
                float num = Vector2.Distance(((Character)this).Position, ((Character)farmer).Position);
                this.TargetPlayer(farmer);
                if (!this.cooldownActive)
                {
                    switch (random.Next(2))
                    {
                        case 0:
                            if ((double)num < (double)this.specialThreshold)
                            {
                                ((Character)this).Halt();
                                this.PerformSpecial();
                                return;
                            }
                            this.PerformSpecial();
                            break;
                        case 1:
                            this.PerformFlight();
                            if (this.flightActive)
                                return;
                            break;
                    }
                }
                this.PerformFollow(((Character)farmer).Position);
            }
            else
                this.PerformRandom();
        }

        public List<Farmer> TargetFarmers()
        {
            List<Farmer> farmerList = new List<Farmer>();
            float num1 = 1200f;
            foreach (Farmer allFarmer in Game1.getAllFarmers())
            {
                if (((Character)allFarmer).currentLocation.Name == ((Character)this).currentLocation.Name)
                {
                    float num2 = Vector2.Distance(((Character)this).Position, ((Character)allFarmer).Position);
                    if ((double)num2 <= (double)num1)
                    {
                        farmerList.Clear();
                        farmerList.Add(allFarmer);
                        num1 = num2;
                    }
                }
            }
            return farmerList;
        }

        public void TargetPlayer(Farmer farmer)
        {
            Vector2 vector2 = Vector2.op_Subtraction(((Character)farmer).Position, ((Character)this).Position);
            float num1 = Math.Abs(vector2.X);
            float num2 = Math.Abs(vector2.Y);
            int num3 = (double)vector2.X < 1.0 / 1000.0 ? -1 : 1;
            int num4 = (double)vector2.Y < 1.0 / 1000.0 ? -1 : 1;
            this.altDirection = 0;
            if ((double)num1 > (double)num2)
            {
                this.moveDirection = num3 < 0 ? 3 : 1;
            }
            else
            {
                this.moveDirection = num4 < 0 ? 0 : 2;
                this.altDirection = num3 < 0 ? 3 : 1;
            }
        }

        public void FixDirection()
        {
            ((Character)this).flip = false;
            if (!Context.IsMainPlayer)
            {
                this.moveDirection = NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netDirection);
                this.altDirection = NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netAlternative);
                this.trackPosition = ((Character)this).Position;
            }
            else
            {
                ((NetFieldBase<int, NetInt>)this.netDirection).Set(this.moveDirection);
                ((NetFieldBase<int, NetInt>)this.netAlternative).Set(this.altDirection);
            }
          ((Character)this).FacingDirection = this.moveDirection;
            switch (this.moveDirection)
            {
                case 0:
                    if (this.altDirection != 3)
                        break;
                    ((Character)this).flip = true;
                    break;
                case 2:
                    if (this.altDirection != 3)
                        break;
                    ((Character)this).flip = true;
                    break;
            }
        }

        public void UpdateHalt(GameTime time)
        {
            if (this.haltTimer % 20 == 0)
            {
                List<Farmer> source = this.TargetFarmers();
                if (source.Count > 0)
                {
                    this.TargetPlayer(source.First<Farmer>());
                    this.FixDirection();
                }
            }
            if (this.haltTimer <= 0)
            {
                this.haltActive = false;
                ((NetFieldBase<bool, NetBool>)this.netHaltActive).Set(false);
            }
            --this.haltTimer;
        }

        public void PerformFlight()
        {
            int num = this.FlightDestination();
            if (num == 0)
                return;
            this.flightActive = true;
            ((NetFieldBase<int, NetInt>)this.netFlightFrame).Set(0);
            this.flightTimer = this.flightIncrement * num;
            this.flightInterval = Vector2.op_Division(Vector2.op_Subtraction(this.flightTo, ((Character)this).Position), (float)this.flightTimer);
            this.flightExtend = 0;
        }

        public void UpdateFlight(GameTime time)
        {
            ((NetFieldBase<bool, NetBool>)this.netDashActive).Set(true);
            --this.flightTimer;
            if (this.flightTimer == 0)
            {
                this.flightActive = false;
                ((NetFieldBase<bool, NetBool>)this.netDashActive).Set(false);
                this.cooldownActive = true;
                this.cooldownTimer = this.cooldownInterval * 2;
            }
            else
            {
                if (this.flightHeight != -1)
                {
                    if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netFlightHeight) < 16 * this.flightHeight && this.flightTimer > 16)
                        ((NetFieldBase<int, NetInt>)this.netFlightHeight).Set(((NetFieldBase<int, NetInt>)this.netFlightHeight).Value + this.flightHeight);
                    else if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netFlightHeight) > 0 && this.flightTimer <= 16)
                        ((NetFieldBase<int, NetInt>)this.netFlightHeight).Set(((NetFieldBase<int, NetInt>)this.netFlightHeight).Value - this.flightHeight);
                }
              ((Character)this).Position = Vector2.op_Addition(((Character)this).Position, this.flightInterval);
                if (this.flightTimer % this.flightIncrement != 0)
                    return;
                if (this.flightTimer == this.flightIncrement)
                {
                    ((NetFieldBase<int, NetInt>)this.netFlightFrame).Set(this.flightLast);
                }
                else
                {
                    ((NetFieldBase<int, NetInt>)this.netFlightFrame).Set(((NetFieldBase<int, NetInt>)this.netFlightFrame).Value + 1);
                    if (((NetFieldBase<int, NetInt>)this.netFlightFrame).Value > this.flightCeiling)
                        ((NetFieldBase<int, NetInt>)this.netFlightFrame).Set(this.flightFloor);
                }
            }
        }

        public int FlightDestination()
        {
            Dictionary<int, Vector2> dictionary = new Dictionary<int, Vector2>()
            {
                [0] = new Vector2(1f, -2f),
                [1] = new Vector2(-1f, -2f),
                [2] = new Vector2(2f, 0.0f),
                [3] = new Vector2(1f, 2f),
                [4] = new Vector2(-1f, 2f),
                [5] = new Vector2(-2f, 0.0f)
            };
            int key = 0;
            switch (this.moveDirection)
            {
                case 0:
                    if (this.altDirection == 3)
                    {
                        key = 1;
                        break;
                    }
                    break;
                case 1:
                    key = 2;
                    break;
                case 2:
                    key = 3;
                    if (this.altDirection == 3)
                    {
                        key = 4;
                        break;
                    }
                    break;
                case 3:
                    key = 5;
                    break;
            }
            Vector2 vector2 = dictionary[key];
            int num1 = 16;
            for (int index = num1; index > 0; --index)
            {
                int num2 = index <= 12 ? 17 - index : index - 12;
                Vector2 neighbour = Vector2.op_Addition(((Character)this).getTileLocation(), Vector2.op_Multiply(vector2, (float)num2));
                if (ModUtility.GroundCheck(((Character)this).currentLocation, neighbour))
                {
                    Rectangle boundingBox = ((Character)Game1.player).GetBoundingBox();
                    int num3 = (int)((double)boundingBox.X - (double)((Character)Game1.player).Position.X);
                    int num4 = (int)((double)boundingBox.Y - (double)((Character)Game1.player).Position.Y);
                    boundingBox.X = (int)((double)neighbour.X * 64.0) + num3;
                    boundingBox.Y = (int)((double)neighbour.Y * 64.0) + num4;
                    if (!((Character)this).currentLocation.isCollidingPosition(boundingBox, Game1.viewport, false, 0, false, (Character)Game1.player, false, false, false))
                    {
                        this.flightTo = Vector2.op_Multiply(neighbour, 64f);
                        return num2;
                    }
                }
            }
            return 0;
        }

        public void PerformSpecial()
        {
            this.specialActive = true;
            this.specialTimer = 72;
            ((NetFieldBase<int, NetInt>)this.netFireFrame).Set(-1);
            this.fireTimer = this.fireInterval;
        }

        public void UpdateSpecial()
        {
            --this.specialTimer;
            --this.fireTimer;
            if (this.specialTimer == 0 || Game1.player.IsBusyDoingSomething())
            {
                this.specialActive = false;
                this.cooldownActive = true;
                this.cooldownTimer = this.cooldownInterval;
                ((NetFieldBase<bool, NetBool>)this.netFireActive).Set(false);
            }
            else
            {
                if (this.specialTimer % 12 == 0)
                {
                    ((NetFieldBase<bool, NetBool>)this.netFireActive).Set(true);
                    ((NetFieldBase<int, NetInt>)this.netFireFrame).Set(((NetFieldBase<int, NetInt>)this.netFireFrame).Value + 1);
                    if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>)this.netFireFrame) > this.fireCeiling)
                        ((NetFieldBase<int, NetInt>)this.netFireFrame).Set(this.fireFloor);
                }
                if (this.fireTimer != 0)
                    return;
                this.SpecialAttack();
                this.fireTimer = this.fireInterval;
            }
        }

        public void PerformFollow(Vector2 target)
        {
            float num = Vector2.Distance(((Character)this).Position, target);
            if ((double)num > (double)this.reachThreshold)
            {
                this.followTarget = target;
                this.followActive = true;
                this.followTimer = 40;
                this.followInterval = Vector2.op_Multiply(Vector2.op_Division(Vector2.op_Subtraction(target, ((Character)this).Position), num), 2f);
            }
            else
                ((Character)this).Halt();
        }

        public void UpdateFollow(GameTime time)
        {
            --this.followTimer;
            if (this.followTimer == 0)
                this.followActive = false;
            ((Character)this).Position = Vector2.op_Addition(((Character)this).Position, this.followInterval);
            if (this.followTimer % 12 != 0)
                return;
            if (((NetFieldBase<int, NetInt>)this.netWalkFrame).Value == 3)
                ((NetFieldBase<int, NetInt>)this.netWalkFrame).Set(0);
            else
                ((NetFieldBase<int, NetInt>)this.netWalkFrame).Set(((NetFieldBase<int, NetInt>)this.netWalkFrame).Value + 1);
        }

        public void PerformRandom()
        {
            Dictionary<int, Vector2> dictionary = new Dictionary<int, Vector2>()
            {
                [0] = new Vector2(1f, -2f),
                [1] = new Vector2(-1f, -2f),
                [2] = new Vector2(2f, 0.0f),
                [3] = new Vector2(1f, 2f),
                [4] = new Vector2(-1f, 2f),
                [5] = new Vector2(-2f, 0.0f)
            };
            int key = new Random().Next(dictionary.Count);
            Vector2 target = Vector2.op_Multiply(dictionary[key], 128f);
            switch (key)
            {
                case 0:
                    this.moveDirection = 0;
                    this.altDirection = 1;
                    break;
                case 1:
                    this.moveDirection = 0;
                    this.altDirection = 3;
                    break;
                case 2:
                    this.moveDirection = 1;
                    this.altDirection = 1;
                    break;
                case 3:
                    this.moveDirection = 2;
                    this.altDirection = 1;
                    break;
                case 4:
                    this.moveDirection = 2;
                    this.altDirection = 3;
                    break;
                case 5:
                    this.moveDirection = 3;
                    this.altDirection = 3;
                    break;
            }
            this.PerformFollow(target);
        }

        public void UpdateCooldown()
        {
            --this.cooldownTimer;
            if ((double)this.cooldownTimer == (double)this.cooldownInterval * 0.5 && new Random().Next(3) == 0)
                ((NPC)this).showTextAboveHead(this.dialogueList[Game1.random.Next(this.dialogueList.Count)], -1, 2, 3000, 0);
            if (this.cooldownTimer > 0)
                return;
            this.cooldownActive = false;
        }
    }
}