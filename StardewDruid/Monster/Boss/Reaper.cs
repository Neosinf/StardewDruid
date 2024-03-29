﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;

namespace StardewDruid.Monster.Boss
{
    public class Reaper : Boss
    {
        public int bobHeight;
        public int ruffleTimer;
        public int ruffleFrame;

        public Reaper()
        {
        }

        public Reaper(Vector2 vector, int CombatModifier)
          : base(vector, CombatModifier, "Reaper")
        {

        }

        public override void LoadOut()
        {

            BaseWalk();

            ReaperFlight();

            BaseSpecial();

            specialThreshold = 480;

            cooldownTimer = 48;

            overHead = new(0, -244);

            loadedOut = true;

        }

        public void ReaperFlight()
        {

            flightTexture = characterTexture;

            flightFrames = new()
            {
                [0] = new() { new(0, 0, 64, 64), },
                [1] = new() { new(64, 0, 64, 64), },
                [2] = new() { new(256, 0, 64, 64), },
                [3] = new() { new(320, 0, 64, 64), },

            };

            flightHeight = 4;

            flightFloor = 2;

            flightCeiling = 2;

            flightLast = 3;

            flightInterval= 9;

            flightSpeed = 12;

        }

        public override void HardMode()
        {
            Health *= 3;
            Health /= 2;
            MaxHealth = Health;

            cooldownInterval = 40;

            tempermentActive = temperment.aggressive;
        }

        public override Rectangle GetBoundingBox()
        {
            Vector2 position = Position;
            return new Rectangle((int)position.X - 32, (int)position.Y - flightHeight - 32, 128, 128);
        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = getLocalPosition(Game1.viewport);

            float drawLayer = (float)StandingPixel.Y / 10000f;

            if (IsEmoting && !Game1.eventUp)
            {
                localPosition.Y -= 32 + Sprite.SpriteHeight * 4;
                b.Draw(Game1.emoteSpriteSheet, localPosition, new Rectangle?(new Rectangle(CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, 0, drawLayer);
            }

            b.Draw(Game1.shadowTexture, new(localPosition.X, localPosition.Y + 96f), new Rectangle?(Game1.shadowTexture.Bounds), Color.White, 0.0f, Vector2.Zero, 4f, 0, Math.Max(0.0f, Tile.Y / 10000f) - 1E-06f);

            if (netFlightActive.Value)
            {

                b.Draw(characterTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 160f - flightHeight - bobHeight), flightFrames[flightFrame][0], Color.White * 0.65f, 0.0f, new Vector2(0.0f, 0.0f), 4f, netAlternative.Value == 3 || netDirection.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

            }
            else if (netSpecialActive.Value)
            {

                b.Draw(characterTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 160f - bobHeight), new Rectangle(128 + ruffleFrame * 64, 0, 64, 64), Color.White * 0.65f, 0.0f, new Vector2(0.0f, 0.0f), 4f, netAlternative.Value == 3 || netDirection.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

            }
            else
            {

                b.Draw(characterTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 160f - bobHeight), new Rectangle(ruffleFrame * 64, 0, 64, 64), Color.White * 0.65f, 0.0f, new Vector2(0.0f, 0.0f), 4f, netAlternative.Value == 3 || netDirection.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

            }

        }

        public override void update(GameTime time, GameLocation location)
        {
            
            base.update(time, location);

            if (bobHeight <= 0)
            {
                bobHeight++;
            }
            else if (bobHeight >= 64)
            {
                bobHeight--;
            }

            ruffleTimer++;

            if (ruffleTimer == 16)
            {

                ruffleFrame++;

                if (ruffleFrame == 2)
                {

                    ruffleFrame = 0;
                }

                ruffleTimer = 0;

            }

        }

        public override void PerformSpecial()
        {
            behaviourActive = behaviour.special;

            behaviourTimer = 72;

            netSpecialActive.Set(true);

            Vector2 omega = Tile;

            for (int i = 0; i < 3; i++)
            {

                Vector2 zero = BlastTarget(2)[0];

                BarrageHandle fireball = new(currentLocation, zero, omega, 2, 1, DamageToFarmer);

                fireball.type = BarrageHandle.barrageType.fireball;

                fireball.monster = this;

                barrages.Add(fireball);

            }

        }

    }

}
