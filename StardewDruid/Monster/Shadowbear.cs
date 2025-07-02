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

namespace StardewDruid.Monster
{
    public class ShadowBear : Boss
    {

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

            walkInterval = 12;

            gait = GetGait();

            idleFrames = bearRender.idleFrames;

            walkFrames = bearRender.walkFrames;

            flightFrames = bearRender.dashFrames;

            smashFrames = bearRender.dashFrames;

            smashSet = true;

            specialFrames = bearRender.specialFrames;

            specialInterval = 12;

            specialCeiling = 8;

            specialFloor = 0;

            specialSet = true;

            channelFrames = bearRender.specialFrames;

            channelCeiling = 8;

            channelFloor = 0;

            channelSet = true;

            sweepFrames = bearRender.sweepFrames;

            sweepInterval = 9;

            sweepSet = true;

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

        public override Vector2 GetPosition(Vector2 localPosition, float spriteScale = -1f)
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

            BearRenderAdditional additional = new()
            {
                layer = Position.Y / 10000f + 0.0032f,

                scale = GetScale()
            };

            additional.position = GetPosition(localPosition, additional.scale);

            DrawEmote(b, localPosition, additional.layer);

            additional.flip = netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

            if (netSweepActive.Value)
            {

                additional.series = BearRenderAdditional.bearseries.sweep;

                additional.direction = netDirection.Value;

                additional.frame = sweepFrame;

                bearRender.DrawNormal(b, additional);

                return;

            }
            else if (netSpecialActive.Value)
            {

                additional.series = BearRenderAdditional.bearseries.special;

                additional.direction = netDirection.Value;

                additional.frame = specialFrame;

                bearRender.DrawNormal(b, additional);

                return;

            }

            additional.direction = netDirection.Value;

            additional.frame = walkFrame;

            bearRender.DrawNormal(b, additional);

        }

        public override void UpdateSpecial()
        {

            base.UpdateSpecial();

            switch (specialTimer)
            {

                case 96:

                    if (netSpecialActive.Value || netChannelActive.Value)
                    {

                        Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BearRoar);

                    }

                    break;

                case 36:

                    if (netSweepActive.Value)
                    {

                        if (Mod.instance.randomIndex.Next(2) == 0)
                        {

                            return;

                        }

                        if (Mod.instance.sounds.ActiveCue(Handle.SoundHandle.SoundCue.BearGrowl))
                        {

                            return;

                        }

                        if (Mod.instance.sounds.ActiveCue(Handle.SoundHandle.SoundCue.BearRoar))
                        {

                            return;

                        }

                        Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BearGrowl);

                    }

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
