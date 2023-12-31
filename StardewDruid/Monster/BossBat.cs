﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;
using System.Collections.Generic;

namespace StardewDruid.Monster
{
    public class BossBat : StardewValley.Monsters.Bat
    {
        public List<string> ouchList;

        public bool posturing;

        public Texture2D hatsTexture;

        public Rectangle hatSourceRect;

        public Vector2 hatOffset;

        public int hatIndex;

        public Dictionary<int, Vector2> hatOffsets;

        public BossBat(Vector2 vector, int combatModifier)
            : base(vector * 64, 200)
        {

            moveTowardPlayerThreshold.Value = 99;

            Health = combatModifier * 6;

            MaxHealth = Health;

            DamageToFarmer = (int)Math.Max(2, combatModifier * 0.1);

            objectsToDrop.Clear();

            hatsTexture = Game1.content.Load<Texture2D>("Characters\\Farmer\\hats");

            hatIndex = 8;

            hatSourceRect = Game1.getSourceRectForStandardTileSheet(hatsTexture, hatIndex, 20, 20);

            hatOffsets = new()
            {
                [0] = new Vector2(24f, 32f),
                [1] = new Vector2(24f, 28f),
                [2] = new Vector2(24f, 24f),
                [3] = new Vector2(24f, 28f),
            };

            ouchList = new()
            {
                "flap flap",
                "flippity",
                "cheeep"
            };

        }
        public override List<Item> getExtraDropItems()
        {
            List<Item> list = new List<Item>();

            return list;

        }

        public override void behaviorAtGameTick(GameTime time)
        {

            if (posturing) { return; }

            base.behaviorAtGameTick(time);

        }

        public override void defaultMovementBehavior(GameTime time)
        {

            if (posturing) { return; }

            base.defaultMovementBehavior(time);

        }

        protected override void localDeathAnimation()
        {
            ModUtility.AnimateDeathSpray(base.currentLocation, base.Position, Color.MediumPurple);
        }

        public override int takeDamage(int damage, int xTrajectory, int yTrajectory, bool isBomb, double addedPrecision, Farmer who)
        {

            if (posturing) { return 0; }

            int ouchIndex = Game1.random.Next(15);

            if (ouchIndex < ouchList.Count)
            {
                showTextAboveHead(ouchList[ouchIndex], duration: 3000);

            }

            return base.takeDamage(damage, xTrajectory, yTrajectory, isBomb, addedPrecision, who);

        }

        public override void drawAboveAllLayers(SpriteBatch b)
        {
            if (!Utility.isOnScreen(base.Position, 128))
            {
                return;
            }

            b.Draw(
                Sprite.Texture,
                getLocalPosition(Game1.viewport) + new Vector2(32f, 32f),
                Sprite.SourceRect,
                (shakeTimer > 0) ? Color.Red : Color.White,
                0f,
                new Vector2(8f, 16f),
                Math.Max(0.2f, scale) * 6f,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                Math.Max(0f, drawOnTop ? 0.991f : getStandingY() / 10000f)
            );

            b.Draw(
                Game1.shadowTexture,
                getLocalPosition(Game1.viewport) + new Vector2(32f, 64f),
                Game1.shadowTexture.Bounds,
                Color.White,
                0f,
                new Vector2(Game1.shadowTexture.Bounds.Center.X, Game1.shadowTexture.Bounds.Center.Y),
                6f,
                SpriteEffects.None,
                base.wildernessFarmMonster ? 0.0001f : ((getStandingY() - 1) / 10000f)
             );

            int frameOffset = Sprite.currentFrame % 4;

            hatOffset = hatOffsets[frameOffset];

            b.Draw(
                hatsTexture,
                getLocalPosition(Game1.viewport) + hatOffset,
                hatSourceRect,
                Color.White,
                0f,
                new Vector2(8f, 16f),
                4f,
                SpriteEffects.None,
                Math.Max(0f, drawOnTop ? 0.992f : getStandingY() / 10000f + 0.00005f)
             );

        }

    }

}
