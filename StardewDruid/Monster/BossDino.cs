using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace StardewDruid.Monster
{
    public class BossDino : DinoMonster
    {
        public List<string> ouchList;
        public List<string> dialogueList;
        public Queue<Vector2> burningQueue;
        public int burnDamage;
        public Texture2D hatsTexture;
        public Rectangle hatSourceRect;
        public Vector2 hatOffset;
        public int hatIndex;
        public Dictionary<int, Rectangle> hatSourceRects;
        public Dictionary<int, Vector2> hatOffsets;
        public Dictionary<int, float> hatRotates;
        public Dictionary<int, Vector2> hatRotateOffsets;
        public bool firingAction;
        public bool defeated;
        public bool hardMode;
        public int dialogueTimer;

        public BossDino(Vector2 vector, int combatModifier)
          : base(Vector2.op_Multiply(vector, 64f))
        {
            ((StardewValley.Monsters.Monster)this).Health = combatModifier * 12;
            ((StardewValley.Monsters.Monster)this).MaxHealth = ((StardewValley.Monsters.Monster)this).Health;
            ((StardewValley.Monsters.Monster)this).focusedOnFarmers = true;
            ((StardewValley.Monsters.Monster)this).DamageToFarmer = (int)((double)combatModifier * 0.15);
            ((NetList<int, NetInt>)((StardewValley.Monsters.Monster)this).objectsToDrop).Clear();
            this.burnDamage = (int)((double)combatModifier * 0.1);
            this.burningQueue = new Queue<Vector2>();
            ((NetFieldBase<bool, NetBool>)((NPC)this).hideShadow).Value = true;
            this.ouchList = new List<string>()
      {
        "ouch",
        "croak",
        "can't you aim for the helmet?"
      };
            this.dialogueList = new List<string>()
      {
        "Why am I here",
        "The power of the Stars has seeped into the land",
        "I should be at rest, I should be...",
        "Surrender, and I'll give you a pony ride",
        "STOP MOVING. JUST BURN.",
        "My helmet provides +3 Intelligence!"
      };
            this.hatsTexture = Game1.content.Load<Texture2D>("Characters\\Farmer\\hats");
            this.hatIndex = 345;
            this.hatSourceRects = new Dictionary<int, Rectangle>()
            {
                [2] = Game1.getSourceRectForStandardTileSheet(this.hatsTexture, this.hatIndex, 20, 20),
                [1] = Game1.getSourceRectForStandardTileSheet(this.hatsTexture, this.hatIndex + 12, 20, 20),
                [3] = Game1.getSourceRectForStandardTileSheet(this.hatsTexture, this.hatIndex + 24, 20, 20),
                [0] = Game1.getSourceRectForStandardTileSheet(this.hatsTexture, this.hatIndex + 36, 20, 20)
            };
            this.hatOffsets = new Dictionary<int, Vector2>()
            {
                [2] = new Vector2(-16f, 0.0f),
                [1] = new Vector2(36f, 2f),
                [3] = new Vector2(-68f, 4f),
                [0] = new Vector2(-16f, -32f)
            };
            this.hatRotates = new Dictionary<int, float>()
            {
                [1] = 6f,
                [3] = 0.4f
            };
            this.hatRotateOffsets = new Dictionary<int, Vector2>()
            {
                [1] = new Vector2(-4f, -2f),
                [3] = new Vector2(8f, -12f)
            };
        }

        public void HardMode()
        {
            ((StardewValley.Monsters.Monster)this).Health = ((StardewValley.Monsters.Monster)this).Health * 3;
            ((StardewValley.Monsters.Monster)this).Health = ((StardewValley.Monsters.Monster)this).Health / 2;
            ((StardewValley.Monsters.Monster)this).MaxHealth = ((StardewValley.Monsters.Monster)this).Health;
            ((StardewValley.Monsters.Monster)this).DamageToFarmer = ((StardewValley.Monsters.Monster)this).DamageToFarmer * 3;
            ((StardewValley.Monsters.Monster)this).DamageToFarmer = ((StardewValley.Monsters.Monster)this).DamageToFarmer / 2;
            this.hardMode = true;
        }

        public virtual Rectangle GetBoundingBox()
        {
            Vector2 position = ((Character)this).Position;
            return new Rectangle((int)position.X - 48, (int)position.Y - 32, 160, 128);
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
            int damage1 = Math.Max(1, damage - ((NetFieldBase<int, NetInt>)((StardewValley.Monsters.Monster)this).resilience).Value);
            ((StardewValley.Monsters.Monster)this).Health = ((StardewValley.Monsters.Monster)this).Health - damage1;
            if (Game1.random.Next(5) == 0)
                ((Character)this).setTrajectory(xTrajectory, yTrajectory);
            if (((StardewValley.Monsters.Monster)this).Health <= 0)
            {
                ((StardewValley.Monsters.Monster)this).deathAnimation();
                this.defeated = true;
            }
            if (this.dialogueTimer <= 0 && Game1.random.Next(4) == 0)
            {
                ((NPC)this).showTextAboveHead(this.ouchList[Game1.random.Next(this.ouchList.Count)], -1, 2, 3000, 0);
                this.dialogueTimer = 300;
            }
            return damage1;
        }

        public virtual void draw(SpriteBatch b)
        {
            if (((NPC)this).IsInvisible || !Utility.isOnScreen(((Character)this).Position, 128))
                return;
            b.Draw(((Character)this).Sprite.Texture, Vector2.op_Addition(((Character)this).getLocalPosition(Game1.viewport), new Vector2(56f, (float)(16 + ((Character)this).yJumpOffset))), new Rectangle?(((Character)this).Sprite.SourceRect), Color.op_Multiply(Color.White, 0.7f), ((NPC)this).rotation, new Vector2(16f, 16f), 7f, ((Character)this).flip ? (SpriteEffects)1 : (SpriteEffects)0, 0.99f);
            this.hatSourceRect = this.hatSourceRects[((NetFieldBase<int, NetInt>)((Character)this).facingDirection).Value];
            this.hatOffset = this.hatOffsets[((NetFieldBase<int, NetInt>)((Character)this).facingDirection).Value];
            float num1 = 0.0f;
            if (this.firingAction)
            {
                if (this.hatRotates.ContainsKey(((NetFieldBase<int, NetInt>)((Character)this).facingDirection).Value))
                {
                    num1 = this.hatRotates[((NetFieldBase<int, NetInt>)((Character)this).facingDirection).Value];
                    this.hatOffset = Vector2.op_Addition(this.hatOffset, this.hatRotateOffsets[((NetFieldBase<int, NetInt>)((Character)this).facingDirection).Value]);
                }
                else
                    this.hatOffset = Vector2.op_Subtraction(this.hatOffset, new Vector2(0.0f, 4f));
            }
            if (((NetFieldBase<int, NetInt>)((Character)this).facingDirection).Value % 2 == 0)
            {
                switch (((Character)this).Sprite.currentFrame % 4)
                {
                    case 1:
                        this.hatOffset = Vector2.op_Addition(this.hatOffset, new Vector2(4f, 0.0f));
                        break;
                    case 3:
                        this.hatOffset = Vector2.op_Subtraction(this.hatOffset, new Vector2(4f, 0.0f));
                        break;
                }
            }
            else
            {
                switch (((Character)this).Sprite.currentFrame % 4)
                {
                    case 1:
                        this.hatOffset = Vector2.op_Addition(this.hatOffset, new Vector2(0.0f, 4f));
                        break;
                    case 3:
                        this.hatOffset = Vector2.op_Addition(this.hatOffset, new Vector2(0.0f, 4f));
                        break;
                }
            }
            float num2 = 0.991f;
            if (((NetFieldBase<int, NetInt>)((Character)this).facingDirection).Value == 0)
                num2 = 0.989f;
            Vector2 vector2 = Vector2.op_Addition(Vector2.op_Addition(((Character)this).getLocalPosition(Game1.viewport), new Vector2(56f, (float)(16 + ((Character)this).yJumpOffset))), this.hatOffset);
            b.Draw(this.hatsTexture, vector2, new Rectangle?(this.hatSourceRect), Color.op_Multiply(Color.White, 0.6f), num1, new Vector2(8f, 12f), 8f, ((Character)this).flip ? (SpriteEffects)1 : (SpriteEffects)0, num2);
        }

        public virtual void behaviorAtGameTick(GameTime time)
        {
            --this.dialogueTimer;
            TimeSpan elapsedGameTime;
            if (((NetFieldBase<int, NetInt>)this.attackState).Value == 1)
            {
                ((NPC)this).IsWalkingTowardPlayer = false;
                ((Character)this).Halt();
            }
            else if (((NPC)this).withinPlayerThreshold())
            {
                ((NPC)this).IsWalkingTowardPlayer = true;
                this.firingAction = false;
            }
            else
            {
                ((NPC)this).IsWalkingTowardPlayer = false;
                this.nextChangeDirectionTime -= time.ElapsedGameTime.Milliseconds;
                int nextWanderTime = this.nextWanderTime;
                elapsedGameTime = time.ElapsedGameTime;
                int milliseconds = elapsedGameTime.Milliseconds;
                this.nextWanderTime = nextWanderTime - milliseconds;
                if (this.nextChangeDirectionTime < 0)
                {
                    this.nextChangeDirectionTime = Game1.random.Next(500, 1000);
                    int facingDirection = ((Character)this).FacingDirection;
                    ((NetFieldBase<int, NetInt>)((Character)this).facingDirection).Value = (((NetFieldBase<int, NetInt>)((Character)this).facingDirection).Value + (Game1.random.Next(0, 3) - 1) + 4) % 4;
                }
                if (this.nextWanderTime < 0)
                {
                    if (this.wanderState)
                        this.nextWanderTime = Game1.random.Next(1000, 2000);
                    else
                        this.nextWanderTime = Game1.random.Next(1000, 3000);
                    this.wanderState = !this.wanderState;
                }
                if (this.wanderState)
                {
                    ((Character)this).moveLeft = ((Character)this).moveUp = ((Character)this).moveRight = ((Character)this).moveDown = false;
                    ((Character)this).tryToMoveInDirection(((NetFieldBase<int, NetInt>)((Character)this).facingDirection).Value, false, ((StardewValley.Monsters.Monster)this).DamageToFarmer, NetFieldBase<bool, NetBool>.op_Implicit((NetFieldBase<bool, NetBool>)((StardewValley.Monsters.Monster)this).isGlider));
                }
                this.firingAction = false;
            }
            int timeUntilNextAttack = this.timeUntilNextAttack;
            elapsedGameTime = time.ElapsedGameTime;
            int milliseconds1 = elapsedGameTime.Milliseconds;
            this.timeUntilNextAttack = timeUntilNextAttack - milliseconds1;
            if (((NetFieldBase<int, NetInt>)this.attackState).Value == 0 && (double)Vector2.Distance(((Character)this).Position, ((Character)Game1.player).Position) <= 560.0)
            {
                ((NetFieldBase<bool, NetBool>)this.firing).Set(false);
                if (this.timeUntilNextAttack >= 0)
                    return;
                this.timeUntilNextAttack = 0;
                ((NetFieldBase<int, NetInt>)this.attackState).Set(1);
                this.totalFireTime = 1500;
                ((Character)this).currentLocation.playSound("croak", (NetAudio.SoundContext)0);
                if (this.dialogueTimer <= 0 && Game1.random.Next(3) == 0)
                {
                    ((NPC)this).showTextAboveHead(this.dialogueList[Game1.random.Next(this.dialogueList.Count)], -1, 2, 3000, 0);
                    this.dialogueTimer = 300;
                }
            }
            else
            {
                if (this.totalFireTime <= 0)
                    return;
                if (!NetFieldBase<bool, NetBool>.op_Implicit((NetFieldBase<bool, NetBool>)this.firing))
                {
                    Farmer player = ((StardewValley.Monsters.Monster)this).Player;
                    if (player != null)
                        ((Character)this).faceGeneralDirection(((Character)player).Position, 0, false);
                }
                int totalFireTime = this.totalFireTime;
                elapsedGameTime = time.ElapsedGameTime;
                int milliseconds2 = elapsedGameTime.Milliseconds;
                this.totalFireTime = totalFireTime - milliseconds2;
                if (!this.firingAction)
                {
                    if (!((NetFieldBase<bool, NetBool>)this.firing).Value)
                    {
                        ((NetFieldBase<bool, NetBool>)this.firing).Set(true);
                        ((Character)this).currentLocation.playSound("furnace", (NetAudio.SoundContext)0);
                    }
                    Vector2 position = ((Character)this).Position;
                    Vector2 zero1 = Vector2.Zero;
                    Vector2 zero2 = Vector2.Zero;
                    float num1 = 0.0f;
                    float num2 = 998f;
                    bool flag = false;
                    Vector2 vector2_1;
                    Vector2 vector2_2;
                    Vector2 vector2_3;
                    switch (((Character)this).FacingDirection)
                    {
                        case 0:
                            ((Character)this).Sprite.AnimateUp(Game1.currentGameTime, 0, "");
                            num1 = -1.57079637f;
                            num2 = 0.0001f;
                            vector2_1 = Vector2.op_Addition(((Character)this).Position, new Vector2(0.0f, -300f));
                            vector2_2 = Vector2.op_Addition(((Character)this).Position, new Vector2(0.0f, -500f));
                            vector2_3 = Vector2.op_Addition(((Character)this).Position, new Vector2(-200f, -300f));
                            break;
                        case 1:
                            vector2_1 = Vector2.op_Addition(((Character)this).Position, new Vector2(300f, 0.0f));
                            vector2_2 = Vector2.op_Addition(((Character)this).Position, new Vector2(500f, 0.0f));
                            vector2_3 = Vector2.op_Addition(((Character)this).Position, new Vector2(100f, 0.0f));
                            break;
                        case 2:
                            num1 = 1.57079637f;
                            vector2_1 = Vector2.op_Addition(((Character)this).Position, new Vector2(0.0f, 300f));
                            vector2_2 = Vector2.op_Addition(((Character)this).Position, new Vector2(0.0f, 500f));
                            vector2_3 = Vector2.op_Addition(((Character)this).Position, new Vector2(-200f, 300f));
                            break;
                        default:
                            vector2_1 = Vector2.op_Addition(((Character)this).Position, new Vector2(-300f, 0.0f));
                            vector2_2 = Vector2.op_Addition(((Character)this).Position, new Vector2(-500f, 0.0f));
                            vector2_3 = Vector2.op_Addition(((Character)this).Position, new Vector2(-500f, 0.0f));
                            flag = true;
                            break;
                    }
                    this.burningQueue.Clear();
                    this.burningQueue.Enqueue(vector2_1);
                    this.burningQueue.Enqueue(vector2_2);
                    // ISSUE: method pointer
                    DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object)this, __methodptr(burningDesert)), 800);
                    // ISSUE: method pointer
                    DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object)this, __methodptr(burningDesert)), 1000);
                    ((Character)this).currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(0, 120f, 4, 1, vector2_3, false, flag)
                    {
                        sourceRect = new Rectangle(0, 0, 160, 32),
                        sourceRectStartingPos = new Vector2(0.0f, 0.0f),
                        texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EnergyBeam.png")),
                        scale = 3f,
                        timeBasedMotion = true,
                        layerDepth = num2,
                        rotation = num1
                    });
                    ((Character)this).currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(0, 400f, 1, 1, vector2_3, false, flag)
                    {
                        sourceRect = new Rectangle(0, 128, 160, 32),
                        sourceRectStartingPos = new Vector2(0.0f, 128f),
                        texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EnergyBeam.png")),
                        scale = 3f,
                        timeBasedMotion = true,
                        layerDepth = num2,
                        alphaFade = 1f / 1000f,
                        rotation = num1,
                        delayBeforeAnimationStart = 500
                    });
                    ((Character)this).currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(0, 125f, 4, 1, Vector2.op_Subtraction(vector2_2, new Vector2(96f, 96f)), false, false)
                    {
                        sourceRect = new Rectangle(0, 0, 64, 64),
                        sourceRectStartingPos = new Vector2(0.0f, 0.0f),
                        texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EnergyBomb.png")),
                        scale = 4f,
                        timeBasedMotion = true,
                        layerDepth = num2 + 1f,
                        rotationChange = 0.00628f,
                        delayBeforeAnimationStart = 500
                    });
                    this.nextFireTime = this.hardMode ? 30 : 50;
                    this.firingAction = true;
                }
                if (this.totalFireTime <= 0)
                {
                    this.totalFireTime = 0;
                    ((NetFieldBase<int, NetInt>)this.attackState).Set(0);
                    this.timeUntilNextAttack = Game1.random.Next(1000, 2000);
                    this.burningQueue.Clear();
                    this.firingAction = false;
                }
            }
        }

        public void burningDesert()
        {
            if (this.burningQueue.Count <= 0)
                return;
            ((Character)this).currentLocation.explode(Vector2.op_Division(this.burningQueue.Dequeue(), 64f), 2, Game1.player, true, 20 + Game1.player.CombatLevel * 2);
        }
    }
}
