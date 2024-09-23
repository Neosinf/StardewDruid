using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public ShadowWolf(Vector2 vector, int CombatModifier, string name = "Shadowwolf")
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

            float spriteScale = 2.5f + (0.5f * netMode.Value);

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

            idleFrames = FrameSeries(64, 64, 0, 0, 1);

            idleFrames[3] = new(idleFrames[1]);

            walkFrames = FrameSeries(64, 64, 0, 0, 7);

            walkFrames[3] = new(walkFrames[1]);

            overHead = new(0, -128);

        }

        public virtual void WolfFlight()
        {

            flightSet = true;

            flightInterval = 9;

            flightSpeed = 9;

            flightPeak = 128;

            flightFrames = new Dictionary<int, List<Rectangle>>()
            {
                // 
                [0] = new()
                {
                    new(0, 320, 64, 64),
                },
                [1] = new()
                {
                    new(0, 256, 64, 64),
                },
                [2] = new()
                {
                    new(0, 192, 64, 64),
                },
                [3] = new()
                {
                    new(0, 256, 64, 64),
                },
                //
                [4] = new()
                {
                    new(64, 320, 64, 64),
                },
                [5] = new()
                {
                    new(64, 256, 64, 64),
                },
                [6] = new()
                {
                    new(64, 192, 64, 64),
                },
                [7] = new()
                {
                    new(64, 256, 64, 64),
                },
                //
                [8] = new()
                {
                    new(128, 320, 64, 64),
                    new(192, 320, 64, 64),
                },
                [9] = new()
                {
                    new(128, 256, 64, 64),
                    new(192, 256, 64, 64),
                },
                [10] = new()
                {
                    new(128, 192, 64, 64),
                    new(192, 192, 64, 64),
                },
                [11] = new()
                {
                    new(128, 256, 64, 64),
                    new(192, 256, 64, 64),
                },

            };

        }

        public virtual void WolfSmash()
        {

            smashSet = true;

            smashFrames = new()
            {
                // 
                [0] = new()
                {
                    new(0, 320, 64, 64),
                },
                [1] = new()
                {
                    new(0, 256, 64, 64),
                },
                [2] = new()
                {
                    new(0, 192, 64, 64),
                },
                [3] = new()
                {
                    new(0, 256, 64, 64),
                },
                //
                [4] = new()
                {
                    new(64, 320, 64, 64),
                },
                [5] = new()
                {
                    new(64, 256, 64, 64),
                },
                [6] = new()
                {
                    new(64, 192, 64, 64),
                },
                [7] = new()
                {
                    new(64, 256, 64, 64),
                },
                //
                [8] = new()
                {
                    new(128, 320, 64, 64),
                    new(192, 320, 64, 64),
                },
                [9] = new()
                {
                    new(128, 256, 64, 64),
                    new(192, 256, 64, 64),
                },
                [10] = new()
                {
                    new(128, 192, 64, 64),
                    new(192, 192, 64, 64),
                },
                [11] = new()
                {
                    new(128, 256, 64, 64),
                    new(192, 256, 64, 64),
                },

            };

        }

        public virtual void WolfSweep()
        {

            sweepSet = true;

            sweepInterval = 12;

            sweepFrames = new()
            {
                [0] = new()
                {
                    new(0, 320, 64, 64),
                    new(64, 320, 64, 64),
                    new(128, 320, 64, 64),
                    new(192, 320, 64, 64),
                },
                [1] = new()
                {
                    new(0, 256, 64, 64),
                    new(64, 256, 64, 64),
                    new(128, 256, 64, 64),
                    new(192, 256, 64, 64),
                },
                [2] = new()
                {
                    new(0, 192, 64, 64),
                    new(64, 192, 64, 64),
                    new(128, 192, 64, 64),
                    new(192, 192, 64, 64),
                },
                [3] = new()
                {
                    new(0, 256, 64, 64),
                    new(64, 256, 64, 64),
                    new(128, 256, 64, 64),
                    new(192, 256, 64, 64),
                },
            };

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
                    Vector2.Zero,
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
                    Vector2.Zero,
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
