using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace StardewDruid.Event.World
{
    public class Treasure : EventHandle
    {

        public List<TemporaryAnimatedSprite> animations;

        public Vector2 treasureVector;

        public Vector2 treasurePosition;

        public string treasureTerrain;

        public string treasureGlyph;

        public Treasure(Rite rite)
            : base(rite.castVector, rite)
        {

            animations = new();

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 10;

        }

        public override void EventTrigger()
        {

            int layerWidth = targetLocation.map.Layers[0].LayerWidth;

            int layerHeight = targetLocation.map.Layers[0].LayerHeight;

            int X = riteData.randomIndex.Next(3, layerWidth - 3);

            int Y = riteData.randomIndex.Next(3, layerHeight - 3);

            treasureVector = new Vector2(X, Y);

            treasurePosition = treasureVector * 64f;

            treasureGlyph = "GlyphEarth";

            treasureTerrain = ModUtility.GroundCheck(targetLocation, treasureVector);

            switch (treasureTerrain)
            {

                case "ground":

                    treasureGlyph = "GlyphFire";

                    break;

                case "water":

                    treasureGlyph = "GlyphWater";

                    break;

            }

            TemporaryAnimatedSprite radiusAnimation = new(0, 4000, 1, 1, (treasureVector * 64) - new Vector2(16,16), false, false)
            {

                sourceRect = new(0, 0, 64, 64),

                sourceRectStartingPos = new Vector2(0, 0),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", treasureGlyph + ".png")),

                scale = 1.5f, //* size,

                layerDepth = 900f,

                rotationChange = (float)Math.PI / 1000,

            };

            targetLocation.temporarySprites.Add(radiusAnimation);

            animations.Add(radiusAnimation);

            Mod.instance.Monitor.Log(treasureVector.ToString(),StardewModdingAPI.LogLevel.Debug);

            Mod.instance.RegisterEvent(this,"treasure");

        }

        public void TreasureClaim()
        {

            if (!Mod.instance.TaskList().ContainsKey("masterTreasure"))
            {

                Mod.instance.UpdateTask("lessonTreasure", 1);

                Mod.instance.CastMessage("Druid Journal has been updated");

                Throw book = new Throw(Game1.player,Game1.player.Position + new Vector2(32, 0),102);

                book.dontInventorise = true;

                book.throwHeight = 2;

                book.ThrowObject();

            }

            TemporaryAnimatedSprite radiusAnimation = new(0, 1000, 1, 1, (treasureVector * 64) - new Vector2(16, 16), false, false)
            {

                sourceRect = new(0, 0, 64, 64),

                sourceRectStartingPos = new Vector2(0, 0),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", treasureGlyph + ".png")),

                scale = 1.5f, //* size,

                layerDepth = 900f,

                scaleChange = 0.002f,

                motion = new Vector2(-0.064f, -0.064f),

                timeBasedMotion = true,

                rotationChange = (float)Math.PI / 25,

                alphaFade = 0.0002f,

            };

            targetLocation.temporarySprites.Add(radiusAnimation);

            expireEarly = true;

            Mod.instance.specialCasts[targetLocation.Name].Add("treasure");

        }

        public void RemoveAnimations()
        {

            foreach(TemporaryAnimatedSprite animation in animations)
            {

                targetLocation.temporarySprites.Remove(animation);
            
            }

            animations.Clear();

        }

        public override void EventRemove()
        {

            RemoveAnimations();

        }

        public override void EventInterval()
        {

            activeCounter++;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 10;

            if(activeCounter % 4 == 0)
            {

                animations[0].reset();

            }

        }

    }

}
