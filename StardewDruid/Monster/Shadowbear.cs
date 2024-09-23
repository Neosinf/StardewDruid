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
    public class ShadowBear : Boss
    {

        public CueWrapper growlCue;
        public CueWrapper growlCueTwo;
        public CueWrapper growlCueThree;

        public int growlTimer;

        public ShadowBear()
        {
        }

        public ShadowBear(Vector2 vector, int CombatModifier, string name = "Shadowbear")
            : base(vector, CombatModifier, name)
        {

        }

        public override void SetBase()
        {

            tempermentActive = temperment.aggressive;

            baseMode = 3;
            baseJuice = 5;
            basePulp = 40;
            cooldownInterval = 180;
            

        }

        public override float GetScale()
        {

            float spriteScale = 3f + (0.75f * netMode.Value);

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

            BearWalk();

            BearSpecial();

            growlCue = Game1.soundBank.GetCue("BearGrowl") as CueWrapper;

            growlCue.Volume *= 2;

            growlCue.Pitch /= 2;

            growlCueTwo = Game1.soundBank.GetCue("BearGrowlTwo") as CueWrapper;

            growlCueTwo.Volume *= 2;

            growlCueTwo.Pitch /= 2;

            growlCueThree = Game1.soundBank.GetCue("BearGrowlThree") as CueWrapper;

            growlCueThree.Volume *= 2;

            growlCueThree.Pitch /= 2;

            loadedOut = true;

        }

        public virtual void BearWalk()
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

        public virtual void BearSpecial()
        {

            sweepInterval = 9;

            sweepFrames = new()
            {
                [0] = new()
                {
                    new(64,128,64,64),
                    new(448,128,64,64),
                    new(448,128,64,64),

                    new(256,128,64,64),
                    new(512,128,64,64),
                    new(512,128,64,64),

                },
                [1] = new()
                {
                    new(64,64,64,64),
                    new(448,64,64,64),
                    new(448,64,64,64),

                    new(256,64,64,64),
                    new(512,64,64,64),
                    new(512,64,64,64),

                },
                [2] = new()
                {
                    new(64,0,64,64),
                    new(448,0,64,64),
                    new(448,0,64,64),

                    new(256,0,64,64),
                    new(512,0,64,64),
                    new(512,0,64,64),

                },
                [3] = new()
                {
                    new(64,64,64,64),
                    new(448,64,64,64),
                    new(448,64,64,64),

                    new(256,64,64,64),
                    new(512,64,64,64),
                    new(512,64,64,64),

                },

            };

            sweepSet = true;

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

            if (growlCue.IsPlaying || growlCueTwo.IsPlaying || growlCueThree.IsPlaying)
            {

                growl = true;

            }

            if (netSweepActive.Value)
            {

                Rectangle sweepSource = sweepFrames[netDirection.Value][sweepFrame];

                if (growl)
                {

                    sweepSource.Y += 192;
                }

                b.Draw(
                    characterTexture,
                    spritePosition,
                    sweepSource,
                    Color.White,
                    0,
                    Vector2.Zero,
                    spriteScale,
                    flippity ? (SpriteEffects)1 : 0,
                    drawLayer);

            }
            else
            {
                Rectangle walkSource = walkFrames[netDirection.Value][walkFrame];

                if (growl)
                {

                    walkSource.Y += 192;
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

            b.Draw(Mod.instance.iconData.cursorTexture, shadowPosition, Mod.instance.iconData.shadowRectangle, Color.White * 0.35f, 0.0f, new Vector2(24), 6f * (GetWidth() / 32) / offset, 0, drawLayer - 0.0001f);

        }

        public override bool ChangeBehaviour()
        {

            if (growlTimer > 0)
            {

                growlTimer--;

            }

            if (base.ChangeBehaviour())
            {

                BearGrowl();

                return true;

            }

            return false;

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

                BearGrowl();

            }

        }

        public void BearGrowl()
        {
            if (growlTimer > 0)
            {

                return;

            }

            if (growlCue.IsPlaying || growlCueTwo.IsPlaying || growlCueThree.IsPlaying)
            {

                return;

            }

            growlTimer = 300;

            switch (Mod.instance.randomIndex.Next(6))
            {

                case 0:

                    growlCue.Play();

                    break;

                case 1:

                    growlCueTwo.Play();

                    break;

                case 2:

                    growlCueThree.Play();

                    break;

            }

        }

    }

}
