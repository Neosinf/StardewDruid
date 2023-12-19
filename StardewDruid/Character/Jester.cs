using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Event.World;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
  public class Jester : StardewDruid.Character.Character
  {
    public int xOffset;
    public Vector2 facePosition;
    public Vector2 beamPosition;
    public Vector2 beamTargetOne;
    public Vector2 beamTargetTwo;
    public float beamRotation;
    public bool flipBeam;
    public bool flipFace;

    public Jester()
    {
    }

    public Jester(Vector2 position, string map)
      : base(position, map, nameof (Jester))
    {
      this.HideShadow = true;
    }

    public virtual void draw(SpriteBatch b, float alpha = 1f)
    {
      if (!Context.IsMainPlayer)
      {
        base.draw(b, alpha);
      }
      else
      {
        if (this.IsInvisible || !Utility.isOnScreen(((StardewValley.Character) this).Position, 128))
          return;
        if (((StardewValley.Character) this).IsEmoting && !Game1.eventUp)
        {
          Vector2 localPosition = ((StardewValley.Character) this).getLocalPosition(Game1.viewport);
          localPosition.X += (float) this.xOffset;
          localPosition.Y -= (float) (32 + ((StardewValley.Character) this).Sprite.SpriteHeight * 4);
          b.Draw(Game1.emoteSpriteSheet, localPosition, new Rectangle?(new Rectangle(((StardewValley.Character) this).CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, ((StardewValley.Character) this).CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, (SpriteEffects) 0, (float) ((StardewValley.Character) this).getStandingY() / 10000f);
        }
        Vector2 localPosition1 = ((StardewValley.Character) this).getLocalPosition(Game1.viewport);
        SpriteBatch spriteBatch = b;
        Texture2D shadowTexture = Game1.shadowTexture;
        Vector2 vector2_1 = Vector2.op_Addition(localPosition1, new Vector2(32f, 40f));
        Rectangle? nullable = new Rectangle?(Game1.shadowTexture.Bounds);
        Color white = Color.White;
        Rectangle bounds1 = Game1.shadowTexture.Bounds;
        double x = (double) ((Rectangle) ref bounds1).Center.X;
        Rectangle bounds2 = Game1.shadowTexture.Bounds;
        double y = (double) ((Rectangle) ref bounds2).Center.Y;
        Vector2 vector2_2 = new Vector2((float) x, (float) y);
        double num1 = (double) Math.Max(0.0f, (float) (4.0 + (double) ((StardewValley.Character) this).yJumpOffset / 40.0) * NetFieldBase<float, NetFloat>.op_Implicit((NetFieldBase<float, NetFloat>) ((StardewValley.Character) this).scale));
        double num2 = (double) Math.Max(0.0f, (float) ((StardewValley.Character) this).getStandingY() / 10000f) - 9.9999999747524271E-07;
        spriteBatch.Draw(shadowTexture, vector2_1, nullable, white, 0.0f, vector2_2, (float) num1, (SpriteEffects) 0, (float) num2);
        if (this.timers.ContainsKey("idle"))
        {
          int num3 = this.timers["idle"] / 80 % 4 * 32;
          b.Draw(((StardewValley.Character) this).Sprite.Texture, Vector2.op_Addition(localPosition1, new Vector2(64f, 16f)), new Rectangle?(new Rectangle(num3, 256, 32, 32)), Color.White, 0.0f, new Vector2((float) (((StardewValley.Character) this).Sprite.SpriteWidth / 2), (float) ((double) ((StardewValley.Character) this).Sprite.SpriteHeight * 3.0 / 4.0)), 4f, ((StardewValley.Character) this).flip || ((StardewValley.Character) this).Sprite.CurrentAnimation != null && ((StardewValley.Character) this).Sprite.CurrentAnimation[((StardewValley.Character) this).Sprite.currentAnimationIndex].flip ? (SpriteEffects) 1 : (SpriteEffects) 0, Math.Max(0.0f, ((StardewValley.Character) this).drawOnTop ? 0.991f : (float) ((StardewValley.Character) this).getStandingY() / 10000f));
        }
        else
          b.Draw(((StardewValley.Character) this).Sprite.Texture, Vector2.op_Addition(localPosition1, new Vector2((float) (64 + this.xOffset), 16f)), new Rectangle?(((StardewValley.Character) this).Sprite.SourceRect), Color.White, 0.0f, new Vector2((float) (((StardewValley.Character) this).Sprite.SpriteWidth / 2), (float) ((double) ((StardewValley.Character) this).Sprite.SpriteHeight * 3.0 / 4.0)), 4f, ((StardewValley.Character) this).flip || ((StardewValley.Character) this).Sprite.CurrentAnimation != null && ((StardewValley.Character) this).Sprite.CurrentAnimation[((StardewValley.Character) this).Sprite.currentAnimationIndex].flip ? (SpriteEffects) 1 : (SpriteEffects) 0, Math.Max(0.0f, ((StardewValley.Character) this).drawOnTop ? 0.991f : (float) ((StardewValley.Character) this).getStandingY() / 10000f));
      }
    }

    public virtual Rectangle GetBoundingBox()
    {
      return new Rectangle((int) ((StardewValley.Character) this).Position.X + 8, (int) ((StardewValley.Character) this).Position.Y + 16, 48, 32);
    }

    public override Rectangle GetHitBox()
    {
      return this.moveDirection % 2 == 0 ? new Rectangle((int) ((StardewValley.Character) this).Position.X + 8, (int) ((StardewValley.Character) this).Position.Y - 16, 48, 64) : new Rectangle((int) ((StardewValley.Character) this).Position.X - 16, (int) ((StardewValley.Character) this).Position.Y + 16, 96, 32);
    }

    public override void AnimateMovement(GameTime time)
    {
      if (this.timers.ContainsKey("attack") && this.targetOpponents.Count > 0 && (double) Vector2.Distance(((StardewValley.Character) this).Position, ((StardewValley.Character) this.targetOpponents.First<Monster>()).Position) <= 96.0 && ((StardewValley.Character) this).Sprite.CurrentFrame % 6 == 2 && ((StardewValley.Character) this).Sprite.currentFrame >= 24)
        return;
      ((StardewValley.Character) this).flip = false;
      ((StardewValley.Character) this).moveDown = false;
      ((StardewValley.Character) this).moveLeft = false;
      ((StardewValley.Character) this).moveRight = false;
      ((StardewValley.Character) this).moveUp = false;
      ((StardewValley.Character) this).FacingDirection = this.moveDirection;
      ((StardewValley.Character) this).Sprite.interval = 175f;
      this.xOffset = 0;
      switch (this.moveDirection)
      {
        case 0:
          ((StardewValley.Character) this).Sprite.AnimateUp(time, 0, "");
          ((StardewValley.Character) this).moveUp = true;
          if (this.altDirection == 3)
          {
            ((StardewValley.Character) this).flip = true;
            this.xOffset = -56;
            break;
          }
          break;
        case 1:
          ((StardewValley.Character) this).Sprite.AnimateRight(time, 0, "");
          ((StardewValley.Character) this).moveRight = true;
          this.xOffset = -32;
          break;
        case 2:
          ((StardewValley.Character) this).Sprite.AnimateDown(time, 0, "");
          ((StardewValley.Character) this).moveDown = true;
          if (this.altDirection == 3)
          {
            ((StardewValley.Character) this).flip = true;
            this.xOffset = -56;
            break;
          }
          break;
        default:
          ((StardewValley.Character) this).moveLeft = true;
          ((StardewValley.Character) this).Sprite.AnimateLeft(time, 0, "");
          this.xOffset = -32;
          break;
      }
      if (!this.timers.ContainsKey("sprint") && !this.timers.ContainsKey("attack"))
        return;
      if (((StardewValley.Character) this).Sprite.CurrentFrame < 24)
      {
        ((StardewValley.Character) this).Sprite.CurrentFrame += 24;
        ((StardewValley.Character) this).Sprite.UpdateSourceRect();
      }
      ((StardewValley.Character) this).Sprite.interval = 125f;
    }

    public override bool TargetOpponent()
    {
      if (!base.TargetOpponent())
        return false;
      float num = Vector2.Distance(((StardewValley.Character) this).Position, ((StardewValley.Character) this.targetOpponents.First<Monster>()).Position);
      if ((double) num >= 64.0 && (double) num <= 480.0)
      {
        Vector2 vector2 = Vector2.op_Subtraction(((StardewValley.Character) this.targetOpponents.First<Monster>()).Position, Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2(32f, 0.0f)));
        if ((double) Math.Abs(vector2.Y) <= 32.0 || (double) Math.Abs(vector2.X) <= 32.0)
          this.TargetBeam();
      }
      return true;
    }

    public void TargetBeam()
    {
      ((StardewValley.Character) this).Halt();
      ((StardewValley.Character) this).flip = false;
      this.timers["stop"] = 120;
      this.timers["cooldown"] = 180;
      this.timers["busy"] = 600;
      this.flipFace = false;
      this.flipBeam = false;
      this.facePosition = Vector2.Zero;
      string str = "RalphFace";
      this.beamRotation = 0.0f;
      float num = 999f;
      switch (this.moveDirection)
      {
        case 0:
          ((StardewValley.Character) this).Sprite.AnimateUp(Game1.currentGameTime, 0, "");
          this.beamTargetOne = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2(32f, -208f));
          this.beamTargetTwo = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2(32f, -448f));
          this.beamPosition = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2((float) (this.xOffset - 200), -320f));
          this.beamRotation = -1.57079637f;
          num = 0.0001f;
          if (this.altDirection == 3)
          {
            this.beamPosition = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2((float) (this.xOffset - 136), -320f));
            ((StardewValley.Character) this).flip = true;
            break;
          }
          break;
        case 1:
          ((StardewValley.Character) this).Sprite.AnimateRight(Game1.currentGameTime, 0, "");
          this.beamTargetOne = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2(272f, 32f));
          this.beamTargetTwo = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2(512f, 32f));
          this.facePosition = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2((float) (68 + this.xOffset), -52f));
          this.beamPosition = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2((float) (96 + this.xOffset), -48f));
          break;
        case 2:
          ((StardewValley.Character) this).Sprite.AnimateDown(Game1.currentGameTime, 0, "");
          this.beamTargetOne = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2(32f, 272f));
          this.beamTargetTwo = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2(32f, 512f));
          this.facePosition = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2((float) (12 + this.xOffset), -52f));
          this.beamPosition = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2((float) (this.xOffset - 200), 186f));
          this.beamRotation = 1.57079637f;
          str = "RalphZero";
          if (this.altDirection == 3)
          {
            this.facePosition = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2((float) (60 + this.xOffset), -52f));
            this.beamPosition = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2((float) (this.xOffset - 136), 186f));
            ((StardewValley.Character) this).flip = true;
            break;
          }
          break;
        default:
          ((StardewValley.Character) this).Sprite.AnimateLeft(Game1.currentGameTime, 0, "");
          this.beamTargetOne = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2(-208f, 32f));
          this.beamTargetTwo = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2(-448f, 32f));
          this.facePosition = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2((float) this.xOffset, -52f));
          this.beamPosition = Vector2.op_Addition(((StardewValley.Character) this).Position, new Vector2((float) (this.xOffset - 464), -56f));
          this.flipBeam = true;
          this.flipFace = true;
          break;
      }
      if (Vector2.op_Inequality(this.facePosition, Vector2.Zero))
        ((StardewValley.Character) this).currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(0, 600f, 1, 1, this.facePosition, true, this.flipFace)
        {
          sourceRect = new Rectangle(0, 0, 32, 32),
          sourceRectStartingPos = new Vector2(0.0f, 0.0f),
          texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", str + ".png")),
          scale = 2f,
          timeBasedMotion = true,
          layerDepth = 998f
        });
      ((StardewValley.Character) this).currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(0, 75f, 4, 1, this.beamPosition, false, this.flipBeam)
      {
        sourceRect = new Rectangle(0, 0, 160, 32),
        sourceRectStartingPos = new Vector2(0.0f, 0.0f),
        texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EnergyBeam.png")),
        scale = 3f,
        timeBasedMotion = true,
        layerDepth = num,
        rotation = this.beamRotation
      });
      ((StardewValley.Character) this).currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(0, 75f, 2, 2, this.beamPosition, false, this.flipBeam)
      {
        sourceRect = new Rectangle(0, 96, 160, 32),
        sourceRectStartingPos = new Vector2(0.0f, 96f),
        texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EnergyBeam.png")),
        scale = 3f,
        timeBasedMotion = true,
        layerDepth = num,
        alphaFade = 1f / 500f,
        rotation = this.beamRotation,
        delayBeforeAnimationStart = 300
      });
      this.ApplyDazeEffect(this.targetOpponents.First<Monster>());
      // ISSUE: method pointer
      DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object) this, __methodptr(ApplyBeamEffect)), 300);
    }

    public void ApplyBeamEffect()
    {
      if (!ModUtility.MonsterVitals(this.targetOpponents.First<Monster>(), ((StardewValley.Character) this).currentLocation))
        return;
      for (int index = ((StardewValley.Character) this).currentLocation.characters.Count - 1; index >= 0; --index)
      {
        if (((StardewValley.Character) this).currentLocation.characters[index] is Monster character && character != this.targetOpponents.First<Monster>())
        {
          if ((double) Vector2.Distance(((StardewValley.Character) character).Position, this.beamTargetOne) < 128.0)
            base.DealDamageToMonster(character, true, Mod.instance.DamageLevel(), false);
          else if ((double) Vector2.Distance(((StardewValley.Character) character).Position, this.beamTargetTwo) < 128.0)
            base.DealDamageToMonster(character, true, Mod.instance.DamageLevel(), false);
        }
      }
      base.DealDamageToMonster(this.targetOpponents.First<Monster>(), true, Mod.instance.DamageLevel() * 2, false);
      ((StardewValley.Character) this).currentLocation.playSoundPitched("flameSpellHit", 1200, (NetAudio.SoundContext) 0);
    }

    public override bool checkAction(Farmer who, GameLocation l)
    {
      if (!base.checkAction(who, l))
        return false;
      if (!Mod.instance.dialogue.ContainsKey(nameof (Jester)))
      {
        Dictionary<string, StardewDruid.Dialogue.Dialogue> dialogue = Mod.instance.dialogue;
        StardewDruid.Dialogue.Jester jester = new StardewDruid.Dialogue.Jester();
        jester.npc = (StardewDruid.Character.Character) this;
        dialogue[nameof (Jester)] = (StardewDruid.Dialogue.Dialogue) jester;
      }
      Mod.instance.dialogue[nameof (Jester)].DialogueApproach();
      return true;
    }

    public override void DealDamageToMonster(Monster monster, bool kill = false, int damage = -1, bool push = true)
    {
      base.DealDamageToMonster(monster, kill, damage, push);
      if (Mod.instance.CurrentProgress() < 25)
        return;
      this.ApplyDazeEffect(monster);
    }

    public void ApplyDazeEffect(Monster monster)
    {
      if (Mod.instance.eventRegister.ContainsKey("Gravity"))
        return;
      List<int> source = new List<int>();
      for (int index = 0; index < 5; ++index)
      {
        string key = "daze" + index.ToString();
        if (!Mod.instance.eventRegister.ContainsKey(key))
          source.Add(index);
        else if ((Mod.instance.eventRegister[key] as Daze).victim == monster)
          return;
      }
      if (source.Count <= 0)
        return;
      Rite rite = Mod.instance.NewRite(false);
      Daze daze = new Daze(((StardewValley.Character) this).getTileLocation(), rite, monster, source.First<int>(), 1);
      if (!MonsterData.CustomMonsters().Contains(monster.GetType()))
      {
        ((StardewValley.Character) monster).Halt();
        monster.stunTime = 4000;
      }
      daze.EventTrigger();
    }

    public override void SwitchFollowMode()
    {
      base.SwitchFollowMode();
      Buff buff = new Buff("Fortune's Favour", 999999, nameof (Jester), 4);
      buff.buffAttributes[4] = 2;
      buff.which = 184654;
      if (Game1.buffsDisplay.hasBuff(184654))
        return;
      Game1.buffsDisplay.addOtherBuff(buff);
    }

    public override void SwitchDefaultMode()
    {
      base.SwitchDefaultMode();
      foreach (Buff otherBuff in Game1.buffsDisplay.otherBuffs)
      {
        if (otherBuff.which == 184654)
          otherBuff.removeBuff();
      }
    }
  }
}
