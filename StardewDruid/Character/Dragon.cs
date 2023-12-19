// Decompiled with JetBrains decompiler
// Type: StardewDruid.Character.Dragon
// Assembly: StardewDruid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24DA4344-683E-4959-87A6-C0A858BCC7DA
// Assembly location: C:\Users\piers\source\repos\StardewDruid\StardewDruid\bin\Debug\net5.0\StardewDruid.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace StardewDruid.Character
{
  public class Dragon : StardewDruid.Character.Character
  {
    public NetInt netDirection = new NetInt(0);
    public NetInt netAlternative = new NetInt(0);
    public Texture2D shadowTexture;
    public bool endTransform;
    public List<Rectangle> shadowFrames;
    public SButton leftButton;
    public SButton rightButton;
    public NetInt netFlightFrame = new NetInt(0);
    public NetInt netFlightHeight = new NetInt(0);
    public NetBool netDashActive = new NetBool(false);
    public Texture2D flightTexture;
    public Dictionary<int, List<Rectangle>> flightFrames;
    public bool flightActive;
    public int flightDelay;
    public int flightTimer;
    public Vector2 flightPosition;
    public Vector2 flightTo;
    public Vector2 flightInterval;
    public bool flightFlip;
    public int flightIncrement;
    public int strikeTimer;
    public int flightExtend;
    public NetInt netFireFrame = new NetInt(0);
    public NetBool netFireActive = new NetBool(false);
    public Texture2D breathTexture;
    public bool breathActive;
    public int breathDelay;
    public int breathTimer;
    public Texture2D fireTexture;
    public Dictionary<int, List<Rectangle>> fireFrames;
    public List<Vector2> fireVectors;
    public int fireTimer;
    public bool avatar;

    public Dragon()
    {
    }

    public Dragon(Vector2 position, string map, string Name)
      : base(position, map, Name)
    {
      this.moveDirection = ((StardewValley.Character) Game1.player).FacingDirection;
      this.flightInterval = Vector2.Zero;
      ((NetFieldBase<bool, NetBool>) this.breather).Value = false;
      ((NetFieldBase<bool, NetBool>) this.hideShadow).Value = true;
      this.flightTo = Vector2.Zero;
      this.avatar = true;
      this.AnimateMovement(Game1.currentGameTime);
      this.LoadOut();
    }

    public void LoadOut()
    {
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
      this.flightTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", NetFieldBase<string, NetString>.op_Implicit((NetFieldBase<string, NetString>) ((StardewValley.Character) this).name) + "Flight.png"));
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
      this.breathTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", NetFieldBase<string, NetString>.op_Implicit((NetFieldBase<string, NetString>) ((StardewValley.Character) this).name) + "Breath.png"));
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
      this.fireVectors = new List<Vector2>()
      {
        new Vector2(44f, -300f),
        new Vector2(-300f, -300f),
        new Vector2(128f, -96f),
        new Vector2(60f, -96f),
        new Vector2(-320f, -96f),
        new Vector2(-320f, -96f),
        new Vector2(136f, -280f),
        new Vector2(-260f, -280f),
        new Vector2(264f, -72f),
        new Vector2(180f, -24f),
        new Vector2(-300f, -24f),
        new Vector2(-328f, -72f)
      };
    }

    protected virtual void initNetFields()
    {
      base.initNetFields();
      ((StardewValley.Character) this).NetFields.AddFields(new INetSerializable[7]
      {
        (INetSerializable) this.netDirection,
        (INetSerializable) this.netAlternative,
        (INetSerializable) this.netFlightFrame,
        (INetSerializable) this.netFlightHeight,
        (INetSerializable) this.netDashActive,
        (INetSerializable) this.netFireFrame,
        (INetSerializable) this.netFireActive
      });
    }

    public virtual void draw(SpriteBatch b, float alpha = 1f)
    {
      if (this.IsInvisible || !Utility.isOnScreen(((StardewValley.Character) this).Position, 128) || this.avatar && Game1.displayFarmer)
        return;
      Vector2 localPosition = ((StardewValley.Character) this).getLocalPosition(Game1.viewport);
      float drawLayer = Game1.player.getDrawLayer();
      if (((StardewValley.Character) this).IsEmoting && !Game1.eventUp)
      {
        localPosition.Y -= (float) (32 + ((StardewValley.Character) this).Sprite.SpriteHeight * 4);
        b.Draw(Game1.emoteSpriteSheet, localPosition, new Rectangle?(new Rectangle(((StardewValley.Character) this).CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, ((StardewValley.Character) this).CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, (SpriteEffects) 0, drawLayer);
      }
      if (NetFieldBase<bool, NetBool>.op_Implicit((NetFieldBase<bool, NetBool>) this.netDashActive))
      {
        b.Draw(this.flightTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 160f - (float) NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netFlightHeight)), new Rectangle?(this.flightFrames[NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection)][NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netFlightFrame)]), Color.White, this.rotation, new Vector2(0.0f, 0.0f), 3f, ((StardewValley.Character) this).flip || NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection) == 3 ? (SpriteEffects) 1 : (SpriteEffects) 0, drawLayer);
        b.Draw(this.shadowTexture, new Vector2(localPosition.X - 48f, localPosition.Y - 56f), new Rectangle?(this.shadowFrames[NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection) + 4]), Color.op_Multiply(Color.White, 0.35f), 0.0f, new Vector2(0.0f, 0.0f), 4f, ((StardewValley.Character) this).flip || NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection) == 3 ? (SpriteEffects) 1 : (SpriteEffects) 0, drawLayer - 1E-05f);
        if (!NetFieldBase<bool, NetBool>.op_Implicit((NetFieldBase<bool, NetBool>) this.netFireActive))
          return;
        this.DrawFire(b, Vector2.op_Subtraction(localPosition, new Vector2(0.0f, (float) NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netFlightHeight))), NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection), drawLayer, 6);
      }
      else
      {
        if (NetFieldBase<bool, NetBool>.op_Implicit((NetFieldBase<bool, NetBool>) this.netFireActive))
        {
          b.Draw(this.breathTexture, new Vector2(localPosition.X - 64f, localPosition.Y - 160f), new Rectangle?(((StardewValley.Character) this).Sprite.SourceRect), Color.White, 0.0f, new Vector2(0.0f, 0.0f), 3f, ((StardewValley.Character) this).flip ? (SpriteEffects) 1 : (SpriteEffects) 0, drawLayer);
          this.DrawFire(b, localPosition, NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection), drawLayer);
        }
        else
          b.Draw(((StardewValley.Character) this).Sprite.Texture, new Vector2(localPosition.X - 64f, localPosition.Y - 160f), new Rectangle?(((StardewValley.Character) this).Sprite.SourceRect), Color.White, 0.0f, new Vector2(0.0f, 0.0f), 3f, ((StardewValley.Character) this).flip ? (SpriteEffects) 1 : (SpriteEffects) 0, drawLayer);
        b.Draw(this.shadowTexture, new Vector2(localPosition.X - 64f, localPosition.Y - 40f), new Rectangle?(this.shadowFrames[NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection)]), Color.op_Multiply(Color.White, 0.35f), 0.0f, new Vector2(0.0f, 0.0f), 3f, ((StardewValley.Character) this).flip || NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection) == 3 ? (SpriteEffects) 1 : (SpriteEffects) 0, drawLayer - 1E-05f);
      }
    }

    public void DrawFire(SpriteBatch b, Vector2 position, int direction, float depth, int adjust = 0)
    {
      float num1 = 4f;
      depth += 1f / 1000f;
      int num2;
      switch (direction)
      {
        case 0:
          num2 = 0;
          if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netAlternative) == 3)
            num2 = 1;
          num1 = 5f;
          depth -= 1f / 500f;
          break;
        case 1:
          num2 = 2;
          break;
        case 2:
          num2 = 3;
          if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netAlternative) == 3)
            num2 = 4;
          num1 = 5f;
          break;
        default:
          num2 = 5;
          break;
      }
      int index = num2 + adjust;
      b.Draw(this.fireTexture, Vector2.op_Addition(position, this.fireVectors[index]), new Rectangle?(this.fireFrames[direction][NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netFireFrame)]), Color.White, this.rotation, new Vector2(0.0f, 0.0f), num1, ((StardewValley.Character) this).flip || direction == 3 ? (SpriteEffects) 1 : (SpriteEffects) 0, depth);
    }

    public virtual void drawAboveAlwaysFrontLayer(SpriteBatch b)
    {
      if (!Context.IsMainPlayer)
      {
        base.drawAboveAlwaysFrontLayer(b);
      }
      else
      {
        if (this.textAboveHeadTimer <= 0 || this.textAboveHead == null)
          return;
        Vector2 local = Game1.GlobalToLocal(new Vector2((float) ((StardewValley.Character) this).getStandingX(), (float) (((StardewValley.Character) this).getStandingY() - ((StardewValley.Character) this).Sprite.SpriteHeight * 4 - 64 + ((StardewValley.Character) this).yJumpOffset)));
        SpriteText.drawStringWithScrollCenteredAt(b, this.textAboveHead, (int) local.X, (int) local.Y, "", this.textAboveHeadAlpha, this.textAboveHeadColor, 1, (float) ((double) (((StardewValley.Character) this).getTileY() * 64) / 10000.0 + 1.0 / 1000.0 + (double) ((StardewValley.Character) this).getTileX() / 10000.0), false);
      }
    }

    public override bool checkAction(Farmer who, GameLocation l) => false;

    public virtual void collisionWithFarmerBehavior()
    {
    }

    public override void behaviorOnFarmerPushing()
    {
    }

    public virtual Rectangle GetBoundingBox() => new Rectangle(-1, -1, 0, 0);

    public override void update(GameTime time, GameLocation location)
    {
      if (!Context.IsMainPlayer)
      {
        if (((StardewValley.Character) this).Sprite.loadedTexture == null || ((StardewValley.Character) this).Sprite.loadedTexture.Length == 0)
        {
          ((StardewValley.Character) this).Sprite.spriteTexture = CharacterData.CharacterTexture(NetFieldBase<string, NetString>.op_Implicit((NetFieldBase<string, NetString>) ((StardewValley.Character) this).name));
          ((StardewValley.Character) this).Sprite.loadedTexture = ((NetFieldBase<string, NetString>) ((StardewValley.Character) this).Sprite.textureName).Value;
          this.LoadOut();
        }
        if (((NetFieldBase<int, NetInt>) this.netDirection).Value != this.moveDirection)
          this.AnimateMovement(time);
        ((StardewValley.Character) this).Sprite.animateOnce(time);
      }
      else
      {
        if (Mod.instance.CasterBusy())
          return;
        if (this.shakeTimer > 0)
          this.shakeTimer = 0;
        if (this.textAboveHeadTimer > 0)
        {
          if (this.textAboveHeadPreTimer > 0)
          {
            this.textAboveHeadPreTimer -= time.ElapsedGameTime.Milliseconds;
          }
          else
          {
            this.textAboveHeadTimer -= time.ElapsedGameTime.Milliseconds;
            if (this.textAboveHeadTimer > 500)
              this.textAboveHeadAlpha = Math.Min(1f, this.textAboveHeadAlpha + 0.1f);
            else
              this.textAboveHeadAlpha = Math.Max(0.0f, this.textAboveHeadAlpha - 0.04f);
          }
        }
        ((StardewValley.Character) this).updateEmote(time);
        if (((StardewValley.Character) this).currentLocation != ((StardewValley.Character) Game1.player).currentLocation)
        {
          ((StardewValley.Character) this).currentLocation.characters.Remove((NPC) this);
          ((StardewValley.Character) Game1.player).currentLocation.characters.Add((NPC) this);
          ((StardewValley.Character) this).currentLocation = ((StardewValley.Character) Game1.player).currentLocation;
        }
        this.DefenseBuff();
        this.moveDirection = ((StardewValley.Character) Game1.player).FacingDirection;
        if (this.flightActive)
          this.UpdateFlight();
        if (!this.flightActive)
          this.UpdateFollow(time);
        if (this.breathActive)
          this.UpdateBreath();
        ((StardewValley.Character) this).Sprite.animateOnce(time);
      }
    }

    public void UpdateFollow(GameTime time)
    {
      if (Vector2.op_Inequality(((StardewValley.Character) this).Position, ((StardewValley.Character) Game1.player).Position))
      {
        if (this.moveDirection % 2 == 1)
          ((NetFieldBase<int, NetInt>) this.netAlternative).Set((double) ((StardewValley.Character) Game1.player).Position.X > (double) ((StardewValley.Character) this).Position.X ? 1 : 3);
        ((StardewValley.Character) this).Position = ((StardewValley.Character) Game1.player).Position;
        this.AnimateMovement(time);
      }
      else
      {
        if (((NetFieldBase<int, NetInt>) this.netDirection).Value == this.moveDirection)
          return;
        ((NetFieldBase<int, NetInt>) this.netAlternative).Set(((NetFieldBase<int, NetInt>) this.netDirection).Value);
        this.AnimateMovement(time);
      }
    }

    public override void AnimateMovement(GameTime time)
    {
      ((StardewValley.Character) this).flip = false;
      if (!Context.IsMainPlayer)
        this.moveDirection = NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection);
      else
        ((NetFieldBase<int, NetInt>) this.netDirection).Set(this.moveDirection);
      ((StardewValley.Character) this).FacingDirection = this.moveDirection;
      switch (this.moveDirection)
      {
        case 0:
          ((StardewValley.Character) this).Sprite.AnimateUp(time, 0, "");
          if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netAlternative) != 3)
            break;
          ((StardewValley.Character) this).flip = true;
          break;
        case 1:
          ((StardewValley.Character) this).Sprite.AnimateRight(time, 0, "");
          break;
        case 2:
          ((StardewValley.Character) this).Sprite.AnimateDown(time, 0, "");
          if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netAlternative) != 3)
            break;
          ((StardewValley.Character) this).flip = true;
          break;
        case 3:
          ((StardewValley.Character) this).Sprite.AnimateLeft(time, 0, "");
          break;
      }
    }

    public override void PlayerBusy()
    {
      if (this.breathActive && this.breathDelay > 0)
        this.breathActive = false;
      if (!this.flightActive || this.flightDelay <= 0)
        return;
      this.flightActive = false;
    }

    public override void LeftClickAction(SButton Button)
    {
      this.leftButton = Button;
      if (this.flightActive)
        return;
      this.PerformFlight();
    }

    public override void RightClickAction(SButton Button)
    {
      this.rightButton = Button;
      if (this.breathActive)
        return;
      this.PerformBreath();
    }

    public void DefenseBuff()
    {
      if (Game1.buffsDisplay.hasBuff(184655))
        return;
      Buff buff = new Buff("Dragon Scales", 3000, "Rite of the Ether", 10);
      buff.buffAttributes[10] = 5;
      buff.which = 184655;
      Game1.buffsDisplay.addOtherBuff(buff);
    }

    public void PerformFlight()
    {
      int num = this.FlightDestination();
      if (num == 0)
        return;
      this.flightActive = true;
      this.flightDelay = 3;
      ((NetFieldBase<int, NetInt>) this.netFlightFrame).Set(0);
      this.flightTimer = this.flightIncrement * num;
      this.flightInterval = Vector2.op_Division(Vector2.op_Subtraction(this.flightTo, ((StardewValley.Character) Game1.player).Position), (float) this.flightTimer);
      this.flightPosition = ((StardewValley.Character) Game1.player).Position;
      this.strikeTimer = 12;
      this.flightExtend = 0;
    }

    public bool UpdateFlight()
    {
      if (this.flightDelay > 0)
      {
        --this.flightDelay;
        return true;
      }
      ((NetFieldBase<bool, NetBool>) this.netDashActive).Set(true);
      --this.flightTimer;
      --this.strikeTimer;
      if (this.flightTimer == 0)
      {
        this.flightActive = false;
        ((NetFieldBase<bool, NetBool>) this.netDashActive).Set(false);
        if (Mod.instance.TaskList().ContainsKey("masterFlight"))
        {
          Rectangle hitBox;
          // ISSUE: explicit constructor call
          ((Rectangle) ref hitBox).\u002Ector((int) ((StardewValley.Character) this).Position.X - 120, (int) ((StardewValley.Character) this).Position.Y - 64, 240, 160);
          this.PerformStrike(hitBox, Mod.instance.DamageLevel() * 2);
        }
        return false;
      }
      Game1.player.temporarilyInvincible = true;
      Game1.player.temporaryInvincibilityTimer = 0;
      Game1.player.currentTemporaryInvincibilityDuration = 200;
      if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netFlightHeight) < 128 && this.flightTimer > 16)
        ((NetFieldBase<int, NetInt>) this.netFlightHeight).Set(((NetFieldBase<int, NetInt>) this.netFlightHeight).Value + 8);
      else if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netFlightHeight) > 0 && this.flightTimer <= 16)
        ((NetFieldBase<int, NetInt>) this.netFlightHeight).Set(((NetFieldBase<int, NetInt>) this.netFlightHeight).Value - 8);
      this.flightPosition = Vector2.op_Addition(this.flightPosition, this.flightInterval);
      ((StardewValley.Character) Game1.player).Position = this.flightPosition;
      ((StardewValley.Character) this).Position = this.flightPosition;
      if (this.flightTimer % this.flightIncrement == 0)
      {
        if (this.flightTimer == 8)
          ((NetFieldBase<int, NetInt>) this.netFlightFrame).Set(0);
        else if (((NetFieldBase<int, NetInt>) this.netFlightFrame).Value == 4)
          ((NetFieldBase<int, NetInt>) this.netFlightFrame).Set(1);
        else
          ((NetFieldBase<int, NetInt>) this.netFlightFrame).Set(((NetFieldBase<int, NetInt>) this.netFlightFrame).Value + 1);
        if (Mod.instance.Helper.Input.IsDown(this.leftButton))
        {
          int num = this.FlightDestination();
          if (num != 0)
          {
            this.flightTimer = this.flightIncrement * num;
            this.flightInterval = Vector2.op_Division(Vector2.op_Subtraction(this.flightTo, ((StardewValley.Character) Game1.player).Position), (float) this.flightTimer);
            ++this.flightExtend;
            if (this.flightExtend > 16 && !Mod.instance.TaskList().ContainsKey("masterFlight"))
              Mod.instance.UpdateTask("lessonFlight", 1);
          }
        }
      }
      if (this.strikeTimer == 0)
      {
        if (Mod.instance.TaskList().ContainsKey("masterFlight"))
        {
          Rectangle hitBox;
          // ISSUE: explicit constructor call
          ((Rectangle) ref hitBox).\u002Ector((int) ((StardewValley.Character) this).Position.X - 120, (int) ((StardewValley.Character) this).Position.Y - 64, 240, 160);
          this.PerformStrike(hitBox, Mod.instance.DamageLevel() * 2);
        }
        this.strikeTimer = 36;
      }
      return true;
    }

    public void PerformBreath()
    {
      this.breathActive = true;
      this.breathDelay = 3;
      this.breathTimer = 48;
      ((NetFieldBase<int, NetInt>) this.netFireFrame).Set(-1);
      this.fireTimer = 12;
      if (Mod.instance.TaskList().ContainsKey("masterBreath"))
        return;
      Mod.instance.UpdateTask("lessonBreath", 1);
    }

    public bool UpdateBreath()
    {
      if (this.breathDelay > 0)
      {
        --this.breathDelay;
        return true;
      }
      --this.breathTimer;
      --this.fireTimer;
      if (this.breathTimer == 0 || Game1.player.IsBusyDoingSomething())
      {
        this.breathActive = false;
        ((NetFieldBase<bool, NetBool>) this.netFireActive).Set(false);
        return false;
      }
      if (this.breathTimer % 12 == 0)
      {
        ((NetFieldBase<bool, NetBool>) this.netFireActive).Set(true);
        ((NetFieldBase<int, NetInt>) this.netFireFrame).Set(((NetFieldBase<int, NetInt>) this.netFireFrame).Value + 1);
        if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netFireFrame) == 4)
          ((NetFieldBase<int, NetInt>) this.netFireFrame).Set(2);
        if (Mod.instance.Helper.Input.IsDown(this.rightButton) && this.breathTimer == 12)
          this.breathTimer = 24;
      }
      if (this.fireTimer == 0)
      {
        Vector2 zero = Vector2.Zero;
        switch (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection))
        {
          case 0:
            // ISSUE: explicit constructor call
            ((Vector2) ref zero).\u002Ector((float) ((int) ((double) ((StardewValley.Character) this).Position.X / 64.0) + 2), (float) ((int) ((double) ((StardewValley.Character) this).Position.Y / 64.0) - 4));
            if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netAlternative) == 3 || ((StardewValley.Character) this).flip)
            {
              zero.X -= 4f;
              break;
            }
            break;
          case 1:
            // ISSUE: explicit constructor call
            ((Vector2) ref zero).\u002Ector((float) ((int) ((double) ((StardewValley.Character) this).Position.X / 64.0) + 5), (float) (int) ((double) ((StardewValley.Character) this).Position.Y / 64.0));
            break;
          case 2:
            // ISSUE: explicit constructor call
            ((Vector2) ref zero).\u002Ector((float) ((int) ((double) ((StardewValley.Character) this).Position.X / 64.0) + 3), (float) ((int) ((double) ((StardewValley.Character) this).Position.Y / 64.0) + 1));
            if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netAlternative) == 3 || ((StardewValley.Character) this).flip)
            {
              zero.X -= 4f;
              break;
            }
            break;
          default:
            // ISSUE: explicit constructor call
            ((Vector2) ref zero).\u002Ector((float) ((int) ((double) ((StardewValley.Character) this).Position.X / 64.0) - 4), (float) (int) ((double) ((StardewValley.Character) this).Position.Y / 64.0));
            break;
        }
        ModUtility.Explode(((StardewValley.Character) this).currentLocation, zero, Game1.player, 2, Mod.instance.DamageLevel() / 2, 3, (Tool) Mod.instance.virtualPick, (Tool) Mod.instance.virtualAxe);
        this.fireTimer = 36;
      }
      return true;
    }

    public void PerformStrike(Rectangle hitBox, int damage)
    {
      for (int index = ((StardewValley.Character) this).currentLocation.characters.Count - 1; index >= 0; --index)
      {
        if (((IEnumerable<NPC>) ((StardewValley.Character) this).currentLocation.characters).ElementAtOrDefault<NPC>(index) != null)
        {
          NPC character = ((StardewValley.Character) this).currentLocation.characters[index];
          Rectangle boundingBox = ((StardewValley.Character) character).GetBoundingBox();
          if (character is Monster monsterCharacter && monsterCharacter.Health > 0 && !((NPC) monsterCharacter).IsInvisible && ((Rectangle) ref boundingBox).Intersects(hitBox))
            this.DealDamageToMonster(monsterCharacter, true, damage);
        }
      }
    }

    public int FlightDestination(int flightRange = 8)
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
      int num1 = 8;
      int num2 = NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netAlternative);
      if (NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection) != this.moveDirection)
        num2 = NetFieldBase<int, NetInt>.op_Implicit((NetFieldBase<int, NetInt>) this.netDirection);
      switch (this.moveDirection)
      {
        case 0:
          if (num2 == 3)
            key = 1;
          num1 = 12;
          break;
        case 1:
          key = 2;
          break;
        case 2:
          key = 3;
          if (num2 == 3)
            key = 4;
          num1 = 12;
          break;
        case 3:
          key = 5;
          break;
      }
      Vector2 vector2 = dictionary[key];
      flightRange = 16;
      for (int index = flightRange; index > 0; --index)
      {
        int num3 = index <= 12 ? 17 - index : index - 12;
        Vector2 neighbour = Vector2.op_Addition(((StardewValley.Character) Game1.player).getTileLocation(), Vector2.op_Multiply(vector2, (float) num3));
        if (ModUtility.GroundCheck(((StardewValley.Character) this).currentLocation, neighbour))
        {
          Rectangle boundingBox = ((StardewValley.Character) Game1.player).GetBoundingBox();
          int num4 = (int) ((double) boundingBox.X - (double) ((StardewValley.Character) Game1.player).Position.X);
          int num5 = (int) ((double) boundingBox.Y - (double) ((StardewValley.Character) Game1.player).Position.Y);
          boundingBox.X = (int) ((double) neighbour.X * 64.0) + num4;
          boundingBox.Y = (int) ((double) neighbour.Y * 64.0) + num5;
          if (!((StardewValley.Character) this).currentLocation.isCollidingPosition(boundingBox, Game1.viewport, false, 0, false, (StardewValley.Character) Game1.player, false, false, false))
          {
            this.flightTo = Vector2.op_Multiply(neighbour, 64f);
            this.flightIncrement = num1;
            ((NetFieldBase<int, NetInt>) this.netAlternative).Set(num2);
            this.AnimateMovement(Game1.currentGameTime);
            return num3;
          }
        }
      }
      return 0;
    }

    public override void ShutDown()
    {
      if (!this.flightActive || ((StardewValley.Character) this).currentLocation != ((StardewValley.Character) Game1.player).currentLocation)
        return;
      ((StardewValley.Character) Game1.player).Position = this.flightTo;
    }
  }
}
