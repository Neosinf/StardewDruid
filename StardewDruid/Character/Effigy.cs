using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Weald;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Network;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Character
{
  public class Effigy : StardewDruid.Character.Character
  {
    public List<Vector2> ritesDone;

    public Effigy()
    {
    }

    public Effigy(Vector2 position, string map)
      : base(position, map, nameof (Effigy))
    {
      ritesDone = new List<Vector2>();
      HideShadow = true;
    }

    public virtual void draw(SpriteBatch b, float alpha = 1f)
    {
      if (!Context.IsMainPlayer)
      {
        base.draw(b, alpha);
      }
      else
      {
        if (IsInvisible || !Utility.isOnScreen(Position, 128))
          return;
        if (IsEmoting && !Game1.eventUp)
        {
          Vector2 localPosition = getLocalPosition(Game1.viewport);
          localPosition.Y -= (float) (32 + Sprite.SpriteHeight * 4);
          b.Draw(Game1.emoteSpriteSheet, localPosition, new Rectangle?(new Rectangle(CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, (SpriteEffects) 0, (float) getStandingY() / 10000f);
        }
        Vector2 localPosition1 = getLocalPosition(Game1.viewport);
        if (timers.ContainsKey("idle"))
        {
          int num = timers["idle"] / 200 % 2 == 0 ? 0 : 32;
                    Vector2 drawPosition = new(localPosition1.X + 64f, localPosition1.Y + 16f);
                    b.Draw(Sprite.Texture, drawPosition, new Rectangle?(new Rectangle(num, 352, 32, 32)), Color.White, 0.0f, new Vector2((float) (Sprite.SpriteWidth / 2), (float) ((double) Sprite.SpriteHeight * 3.0 / 4.0)), 4f, flip || Sprite.CurrentAnimation != null && Sprite.CurrentAnimation[Sprite.currentAnimationIndex].flip ? (SpriteEffects) 1 : (SpriteEffects) 0, Math.Max(0.0f, drawOnTop ? 0.991f : (float) getStandingY() / 10000f));
        }
        else
        {

                    Vector2 drawPosition = new(localPosition1.X + 32f, localPosition1.Y + 16f);
                    b.Draw(Sprite.Texture, drawPosition, new Rectangle?(Sprite.SourceRect), Color.White, 0.0f, new Vector2((float) (Sprite.SpriteWidth / 2), (float) ((double) Sprite.SpriteHeight * 3.0 / 4.0)), 4f, flip || Sprite.CurrentAnimation != null && Sprite.CurrentAnimation[Sprite.currentAnimationIndex].flip ? (SpriteEffects) 1 : (SpriteEffects) 0, Math.Max(0.0f, drawOnTop ? 0.991f : (float) getStandingY() / 10000f));
          SpriteBatch spriteBatch = b;
          Texture2D shadowTexture = Game1.shadowTexture;
                    Vector2 vector2_1 = new(localPosition1.X + 32f, localPosition1.Y + 40f);//Vector2.op_Addition(localPosition1, new Vector2(32f, 40f));
          Rectangle? nullable = new Rectangle?(Game1.shadowTexture.Bounds);
          Color color = new(1,1,1,0.65f);
          Rectangle bounds = Game1.shadowTexture.Bounds;
          //double x = (double) ((Rectangle) ref bounds).Center.X;
          bounds = Game1.shadowTexture.Bounds;
                    //double y = (double) ((Rectangle) ref bounds).Center.Y;
                    //Vector2 vector2_2 = new Vector2((float) x, (float) y);
                    Vector2 drawOrigin = new(bounds.X, bounds.Y);
          double num = (double) Math.Max(0.0f, (float) getStandingY() / 10000f) - 9.9999999747524271E-07;
          spriteBatch.Draw(shadowTexture, vector2_1, nullable, color, 0.0f, drawOrigin, 4f, (SpriteEffects) 0, (float) num);
        }
      }
    }

    public override bool checkAction(Farmer who, GameLocation l)
    {
      base.checkAction(who, l);
      if (Context.IsMainPlayer)
        AdjustJacket();
      if (!Mod.instance.dialogue.ContainsKey(nameof (Effigy)))
      {
        Dictionary<string, StardewDruid.Dialogue.Dialogue> dialogue = Mod.instance.dialogue;
        StardewDruid.Dialogue.Effigy effigy = new StardewDruid.Dialogue.Effigy();
        effigy.npc = (StardewDruid.Character.Character) this;
        dialogue[nameof (Effigy)] = (StardewDruid.Dialogue.Dialogue) effigy;
      }
      Mod.instance.dialogue[nameof (Effigy)].DialogueApproach();
      return true;
    }

    public override List<Vector2> RoamAnalysis()
    {
      List<Vector2> vector2List = new List<Vector2>();
      foreach (Dictionary<Vector2, StardewValley.Object> dictionary in currentLocation.Objects)
      {
        foreach (KeyValuePair<Vector2, StardewValley.Object> keyValuePair in dictionary)
        {
                    if (keyValuePair.Value.IsScarecrow())
                        vector2List.Add(new(keyValuePair.Key.X + 64f, keyValuePair.Key.Y + 64f)); // Vector2.op_Multiply(keyValuePair.Key, 64f));
          if (vector2List.Count % 4 == 0)
            vector2List.Add(new Vector2(-1f));
        }
        if (vector2List.Count >= 24)
          break;
      }
      List<Vector2> collection = base.RoamAnalysis();
      vector2List.AddRange((IEnumerable<Vector2>) collection);
      return vector2List;
    }

    public void AnimateCast()
    {
      switch (moveDirection)
      {
        case 0:
          switch (Mod.instance.CurrentBlessing())
          {
            case "mists":
              Sprite.currentFrame = 26;
              break;
            case "stars":
              Sprite.currentFrame = 34;
              break;
            case "fates":
              Sprite.currentFrame = 42;
              break;
            default:
              Sprite.currentFrame = 18;
              break;
          }
          break;
        case 1:
          Sprite.currentFrame = 17;
          break;
        case 2:
          Sprite.currentFrame = 16;
          break;
        case 3:
          Sprite.currentFrame = 19;
          break;
      }
      Sprite.UpdateSourceRect();
    }

    public override void AnimateMovement(GameTime time)
    {
      if (timers.ContainsKey("cast"))
      {
        AnimateCast();
      }
      else
      {
        moveDown = false;
        moveLeft = false;
        moveRight = false;
        moveUp = false;
        FacingDirection = moveDirection;
        switch (moveDirection)
        {
          case 0:
            Sprite.AnimateUp(time, 0, "");
            moveUp = true;
            AdjustJacket();
            break;
          case 1:
            Sprite.AnimateRight(time, 0, "");
            moveRight = true;
            break;
          case 2:
            Sprite.AnimateDown(time, 0, "");
            moveDown = true;
            break;
          default:
            Sprite.AnimateLeft(time, 0, "");
            moveLeft = true;
            break;
        }
      }
    }

    public void AdjustJacket()
    {
      if (moveDirection != 0 || Sprite.currentFrame < 8 || Sprite.currentFrame >= 12)
        return;
      switch (Mod.instance.CurrentBlessing())
      {
        case "weald":
          return;
        case "mists":
          Sprite.currentFrame += 12;
          break;
        case "stars":
          Sprite.currentFrame += 20;
          break;
        case "fates":
          Sprite.currentFrame += 28;
          break;
      }
      Sprite.UpdateSourceRect();
    }

    public override void ReachedRoamPosition()
    {
            Vector2 vector2 = new(roamVectors[roamIndex].X / 64f, roamVectors[roamIndex].Y / 64f);//Vector2.op_Division(roamVectors[roamIndex], 64f);
      if (ritesDone.Contains(vector2) || !currentLocation.Objects.ContainsKey(vector2) || !currentLocation.Objects[vector2].IsScarecrow())
        return;
      Halt();
      AnimateCast();
      timers["cast"] = 30;
      Rite rite = Mod.instance.NewRite(false);
      bool Reseed = !Mod.instance.EffectDisabled("Seeds");
      for (int level = 1; level < 5; ++level)
      {
        foreach (Vector2 tilesWithinRadius in ModUtility.GetTilesWithinRadius(currentLocation, vector2, level))
        {
          if (currentLocation.terrainFeatures.ContainsKey(tilesWithinRadius) && currentLocation.terrainFeatures[tilesWithinRadius].GetType().Name.ToString() == "HoeDirt")
            rite.effectCasts[tilesWithinRadius] = (CastHandle) new StardewDruid.Cast.Weald.Crop(tilesWithinRadius, rite, Reseed, true);
        }
      }
      if (currentLocation.Name == Game1.player.currentLocation.Name && Utility.isOnScreen(Position, 128))
      {
        ModUtility.AnimateRadiusDecoration(currentLocation, vector2, "Weald", 1f, 1f, 1500f);
        Game1.player.currentLocation.playSoundPitched("discoverMineral", 1000, (NetAudio.SoundContext) 0);
      }
      rite.CastEffect(false);
      ritesDone.Add(vector2);
    }
  }
}
