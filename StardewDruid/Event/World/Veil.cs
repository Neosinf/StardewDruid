using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;

namespace StardewDruid.Event.World
{
  public class Veil : EventHandle
  {
    public Dictionary<int, TemporaryAnimatedSprite> hideAnimations;
    public Vector2 hideCorner;
    public Vector2 hideAnchor;

    public Veil(Vector2 target, Rite rite)
      : base(target, rite)
    {
      this.expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 6.0;
      this.hideAnimations = new Dictionary<int, TemporaryAnimatedSprite>();
      this.hideAnchor = Vector2.op_Addition(this.targetVector, new Vector2(0.0f, -32f));
      this.hideCorner = Vector2.op_Addition(this.hideAnchor, new Vector2(-96f, -96f));
    }

    public override void EventTrigger() => Mod.instance.RegisterEvent((EventHandle) this, "veil");

    public override bool EventActive()
    {
      return !this.expireEarly && !(((Character) this.targetPlayer).currentLocation.Name != this.targetLocation.Name) && this.expireTime >= Game1.currentGameTime.TotalGameTime.TotalSeconds;
    }

    public override void EventRemove() => this.RemoveAnimations();

    public void RemoveAnimations()
    {
      if (this.hideAnimations.Count > 0)
      {
        foreach (KeyValuePair<int, TemporaryAnimatedSprite> hideAnimation in this.hideAnimations)
          this.targetLocation.temporarySprites.Remove(hideAnimation.Value);
      }
      this.hideAnimations.Clear();
    }

    public override void EventInterval()
    {
      ++this.activeCounter;
      if (this.activeCounter == 1)
      {
        TemporaryAnimatedSprite temporaryAnimatedSprite = new TemporaryAnimatedSprite(0, 1000f, 1, 1, this.hideAnchor, false, false)
        {
          sourceRect = new Rectangle(0, 0, 64, 64),
          sourceRectStartingPos = new Vector2(0.0f, 0.0f),
          texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Veil.png")),
          scaleChange = 3f / 1000f,
          motion = new Vector2(-0.096f, -0.096f),
          layerDepth = 999f,
          timeBasedMotion = true,
          rotationChange = -0.06f,
          alpha = 0.25f,
          color = new Color(0.75f, 0.75f, 1f, 1f)
        };
        this.targetLocation.temporarySprites.Add(temporaryAnimatedSprite);
        this.hideAnimations[0] = temporaryAnimatedSprite;
      }
      if (this.activeCounter == 2)
      {
        this.RemoveAnimations();
        TemporaryAnimatedSprite temporaryAnimatedSprite = new TemporaryAnimatedSprite(0, 6000f, 1, 1, this.hideCorner, false, false)
        {
          sourceRect = new Rectangle(0, 0, 64, 64),
          sourceRectStartingPos = new Vector2(0.0f, 0.0f),
          texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Veil.png")),
          scale = 4f,
          layerDepth = 999f,
          timeBasedMotion = true,
          rotationChange = -0.06f,
          alpha = 0.25f,
          color = new Color(0.75f, 0.75f, 1f, 1f)
        };
        this.targetLocation.temporarySprites.Add(temporaryAnimatedSprite);
        this.hideAnimations[1] = temporaryAnimatedSprite;
        this.expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 7.0;
      }
      if ((double) Vector2.Distance(this.targetVector, ((Character) this.riteData.caster).Position) > 128.0)
        return;
      int num = Math.Min(this.riteData.caster.maxHealth / 8, this.riteData.caster.maxHealth - this.riteData.caster.health);
      this.riteData.caster.health += num;
      Mod.instance.CastMessage("Mists grant +" + num.ToString() + " health", 5);
    }
  }
}
