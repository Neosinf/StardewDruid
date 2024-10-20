﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Render;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Monster
{
    public class ShadowWolf : Boss
    {

        public CueWrapper growlCue;

        public int growlTimer;

        public ShadowWolf()
        {
        }

        public ShadowWolf(Vector2 vector, int CombatModifier, string name = "GreyWolf")
            : base(vector, CombatModifier, name)
        {

        }

        public override void SetBase()
        {

            tempermentActive = temperment.aggressive;

            baseMode = 3;
            baseJuice = 5;
            basePulp = 30;
            cooldownInterval = 180;

        }

        public override float GetScale()
        {

            float spriteScale = 2f + (0.5f * netMode.Value);

            return spriteScale;

        }

        public override int GetHeight()
        {

            return 64;

        }

        public override int GetWidth()
        {

            return 64;

        }

        public override void LoadOut()
        {

            WolfWalk();

            WolfFlight();

            WolfSmash();

            WolfSweep();

            growlCue = Game1.soundBank.GetCue("WolfGrowl") as CueWrapper;

            growlCue.Volume *= 2;

            growlCue.Pitch /= 2;

            loadedOut = true;

        }

        public virtual void WolfWalk()
        {

            characterTexture = MonsterHandle.MonsterTexture(realName.Value);

            walkInterval = 12;

            gait = 2.4f;

            idleFrames = WolfRender.WalkFrames();

            walkFrames = WolfRender.WalkFrames();

            overHead = new(0, -128);

        }

        public virtual void WolfFlight()
        {

            flightSet = true;

            flightInterval = 9;

            flightSpeed = 9;

            flightPeak = 128;

            flightFrames = WolfRender.DashFrames();

        }

        public virtual void WolfSmash()
        {

            smashSet = true;

            smashFrames = WolfRender.DashFrames();

        }

        public virtual void WolfSweep()
        {

            sweepSet = true;

            sweepInterval = 12;

            sweepFrames = WolfRender.SweepFrames();

        }

        public override Rectangle GetBoundingBox()
        {

            Vector2 spritePosition = GetPosition(Position);

            float spriteScale = GetScale();

            int width = GetWidth();

            int height = GetHeight();

            Rectangle box = new(
                (int)(spritePosition.X - (width * spriteScale / 2) + (spriteScale * 2)),
                (int)(spritePosition.Y - (height * spriteScale / 2) + (spriteScale * 16)),
                (int)((spriteScale * width) - (spriteScale * 4)),
                (int)((spriteScale * height) - (spriteScale * 16))
            );

            return box;

        }

        public override Vector2 GetPosition(Vector2 localPosition, float spriteScale = -1f, bool shadow = false)
        {

            if (spriteScale == -1f)
            {

                spriteScale = GetScale();

            }

            int height = GetHeight();

            Vector2 spritePosition = localPosition + new Vector2(32, 64) - new Vector2(0, height * spriteScale / 2);

            if (netFlightActive.Value || netSmashActive.Value)
            {

                spritePosition.Y -= flightHeight;

            }

            return spritePosition;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = StandingPixel.Y / 10000f;

            DrawEmote(b, localPosition, drawLayer);

            float spriteScale = GetScale();

            Vector2 spritePosition = GetPosition(localPosition, spriteScale);

            bool flippity = netDirection.Value == 3 || netDirection.Value % 2 == 0 && netAlternative.Value == 3;

            bool growl = false;

            if (growlCue.IsPlaying)
            {

                growl = true;

            }

            if (netSmashActive.Value || netFlightActive.Value)
            {

                int setFlightSeries = netDirection.Value + (netFlightProgress.Value * 4);

                int setFlightFrame = Math.Min(flightFrame, (flightFrames[setFlightSeries].Count - 1));

                Rectangle flightSource = flightFrames[setFlightSeries][setFlightFrame];

                if (growl)
                {

                    flightSource.Y += 384;
                }

                b.Draw(
                    characterTexture,
                    spritePosition,
                    flightSource,
                    Color.White,
                    0f,
                    new Vector2(flightSource.Width / 2, flightSource.Height / 2),
                    spriteScale,
                    flippity ? (SpriteEffects)1 : 0,
                    drawLayer
                );


            }
            else if (netSweepActive.Value)
            {

                Rectangle sweepSource = sweepFrames[netDirection.Value][sweepFrame];

                if (growl)
                {

                    sweepSource.Y += 384;
                }

                b.Draw(
                    characterTexture,
                    spritePosition,
                    sweepSource,
                    Color.White,
                    0f,
                    new Vector2(sweepSource.Width / 2, sweepSource.Height / 2),
                    spriteScale,
                    flippity ? (SpriteEffects)1 : 0,
                    drawLayer
                );


            }
            else
            {
                Rectangle walkSource = walkFrames[netDirection.Value][walkFrame];

                if (growl)
                {

                    walkSource.Y += 384;
                }

                b.Draw(
                    characterTexture,
                    spritePosition,
                    walkSource,
                    Color.White,
                    0,
                    new Vector2(walkSource.Width / 2, walkSource.Height / 2),
                    spriteScale,
                    flippity ? (SpriteEffects)1 : 0,
                    drawLayer);

            }


            Vector2 shadowPosition = localPosition + new Vector2(32, 36);

            float offset = 2f + (Math.Abs(0 - (walkFrames[0].Count() / 2) + walkFrame) * 0.1f);

            if (netDirection.Value % 2 == 1)
            {
                shadowPosition.Y += 4;
            }

            b.Draw(Mod.instance.iconData.cursorTexture, shadowPosition, Mod.instance.iconData.shadowRectangle, Color.White * 0.35f, 0.0f, new Vector2(24), 5f * (GetWidth() / 32) / offset, 0, drawLayer - 0.0001f);

        }

        public override bool ChangeBehaviour()
        {

            if (growlTimer > 0)
            {

                growlTimer--;

            }

            if (base.ChangeBehaviour())
            {

                WolfGrowl();

                return true;

            }

            return false;

        }

        public override bool PerformFlight(Vector2 target, int flightType = -1)
        {

            if(Vector2.Distance(target,Position) > 384)
            {

                return false;

            }

            return base.PerformFlight(target, flightType);
        
        }

        public override void UpdateMultiplayer()
        {

            base.UpdateMultiplayer();

            if (growlTimer > 0)
            {

                growlTimer--;

                return;

            }

            if (netSweepActive.Value)
            {

                WolfGrowl();

            }

        }

        public void WolfGrowl()
        {
            if (growlTimer > 0)
            {

                return;

            }

            if (growlCue.IsPlaying)
            {

                return;

            }

            growlTimer = 300;

            switch (Mod.instance.randomIndex.Next(2))
            {

                case 0:

                    growlCue.Play();

                    break;

            }

        }

    }

}
