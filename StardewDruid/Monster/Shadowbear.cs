using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Location;
using StardewDruid.Render;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StardewDruid.Data.IconData;

namespace StardewDruid.Monster
{
    public class ShadowBear : Boss
    {

        public CueWrapper growlCue;
        public CueWrapper growlCueTwo;
        public CueWrapper growlCueThree;

        public int growlTimer;

        public BearRender bearRender;

        public ShadowBear()
        {
        }

        public ShadowBear(Vector2 vector, int CombatModifier, string name = "BrownBear")
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

        public override void LoadOut()
        {

            bearRender = new(realName.Value);

            growlCue = Game1.soundBank.GetCue("BearGrowl") as CueWrapper;

            growlCue.Volume *= 2;

            growlCue.Pitch /= 2;

            growlCueTwo = Game1.soundBank.GetCue("BearGrowlTwo") as CueWrapper;

            growlCueTwo.Volume *= 2;

            growlCueTwo.Pitch /= 2;

            growlCueThree = Game1.soundBank.GetCue("BearGrowlThree") as CueWrapper;

            growlCueThree.Volume *= 2;

            growlCueThree.Pitch /= 2;

            walkInterval = 12;

            gait = GetGait();

            overHead = new(0, -128);

            sweepInterval = 9;

            sweepSet = true;

            idleFrames = bearRender.idleFrames;

            walkFrames = bearRender.walkFrames;

            flightFrames = bearRender.dashFrames;

            smashFrames = bearRender.dashFrames;

            specialFrames = walkFrames;

            channelFrames = walkFrames;

            sweepFrames = bearRender.sweepFrames;

            loadedOut = true;

        }

        public override float GetScale()
        {

            float spriteScale = 3.5f + (0.5f * netMode.Value);

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
        public override float GetGait(int mode = 0)
        {

             return 2.4f + 0.2f * mode;

        }

        public override Rectangle GetBoundingBox()
        {

            Vector2 spritePosition = GetPosition(Position);

            float spriteScale = GetScale();

            int width = GetWidth();

            int height = GetHeight();

            Rectangle box = new(
                (int)((spritePosition.X - (width * spriteScale / 2)) + (spriteScale * 2)),
                (int)((spritePosition.Y - (height * spriteScale / 2)) + (spriteScale * 16)),
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

            BearRenderAdditional additional = new();

            additional.layer = Position.Y / 10000f + 0.0032f;

            additional.scale = GetScale();

            additional.position = GetPosition(localPosition, additional.scale);

            DrawEmote(b, localPosition, additional.layer);

            additional.flip = netDirection.Value == 3 || netDirection.Value % 2 == 0 && netAlternative.Value == 3;

            if (growlCue.IsPlaying)
            {

                additional.mode = BearRenderAdditional.bearmode.growl;

            }

            if (netSweepActive.Value)
            {

                additional.series = BearRenderAdditional.bearseries.sweep;

                additional.direction = netDirection.Value;

                additional.frame = sweepFrame;

                bearRender.DrawNormal(b, additional);

                return;
            }

            additional.direction = netDirection.Value;

            additional.frame = walkFrame;

            bearRender.DrawNormal(b, additional);

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

        public override Vector2 PerformRedirect(Vector2 target)
        {

            Vector2 actual = Position + (ModUtility.PathFactor(Position, target) * 96);

            return LocationHandle.TerrainRedirect(currentLocation, actual, Position);

        }

    }

}
