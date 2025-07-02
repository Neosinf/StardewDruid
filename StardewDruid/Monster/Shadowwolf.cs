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
    public class ShadowWolf : Boss
    {

        //public CueWrapper growlCue;

        public int growlTimer;

        public WolfRender wolfRender;

        public ShadowWolf()
        {
        }

        public ShadowWolf(Vector2 vector, int CombatModifier, string name = "BrownWolf")
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

            float spriteScale = 3f + (0.5f * netMode.Value);

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

            wolfRender = new(realName.Value);

            /*growlCue = Game1.soundBank.GetCue("WolfGrowl") as CueWrapper;

            growlCue.Volume *= 2;

            growlCue.Pitch /= 2;*/

            walkInterval = 12;

            gait = 2.4f;

            flightSet = true;

            flightInterval = 9;

            flightSpeed = 9;

            flightPeak = 128;

            flightDefault = flightTypes.target;

            sweepInterval = 12;

            idleFrames = wolfRender.idleFrames;

            walkFrames = wolfRender.walkFrames;

            flightFrames = wolfRender.dashFrames;

            smashFrames = wolfRender.dashFrames;

            smashSet = true;

            specialFrames = wolfRender.specialFrames;

            specialInterval = 45;

            specialCeiling = 3;

            specialFloor = 0;

            specialSet = true;

            channelFrames = wolfRender.specialFrames;

            channelCeiling = 3;

            channelFloor = 0;

            channelSet = true;

            sweepFrames = wolfRender.sweepFrames;

            sweepSet = true;

            loadedOut = true;

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

            WolfRenderAdditional additional = new()
            {
                layer = Position.Y / 10000f + 0.0032f,

                scale = GetScale()
            };

            additional.position = GetPosition(localPosition, additional.scale);

            DrawEmote(b, localPosition, additional.layer);

            additional.flip = netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

            if (Mod.instance.sounds.ActiveCue(Handle.SoundHandle.SoundCue.WolfGrowl))
            {

                additional.mode = WolfRenderAdditional.wolfmode.growl;

            }

            if (netSmashActive.Value || netFlightActive.Value)
            {

                int setFlightSeries = netDirection.Value + (netFlightProgress.Value * 4);

                int setFlightFrame = Math.Min(flightFrame, (flightFrames[setFlightSeries].Count - 1));

                additional.series = WolfRenderAdditional.wolfseries.dash;

                additional.direction = setFlightSeries;

                additional.frame = setFlightFrame;

                wolfRender.DrawNormal(b, additional);

                return;

            }
            else if (netSweepActive.Value)
            {

                additional.series = WolfRenderAdditional.wolfseries.sweep;

                additional.direction = netDirection.Value;

                additional.frame = sweepFrame;

                wolfRender.DrawNormal(b, additional);

                return;
            }

            additional.direction = netDirection.Value;

            additional.frame = walkFrame;

            wolfRender.DrawNormal(b, additional);

        }

        public override void UpdateSpecial()
        {

            base.UpdateSpecial();

            switch (specialTimer)
            {

                case 96:

                    if (netSpecialActive.Value || netChannelActive.Value)
                    {

                        //Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.WolfHowl);

                        Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.WolfGrowl);

                    }

                    break;

                case 36:

                    if (netSweepActive.Value)
                    {

                        if (Mod.instance.randomIndex.Next(2) == 0)
                        {

                            return;

                        }

                        if (Mod.instance.sounds.ActiveCue(Handle.SoundHandle.SoundCue.WolfGrowl))
                        {

                            return;

                        }

                        /*if (Mod.instance.sounds.ActiveCue(Handle.SoundHandle.SoundCue.WolfHowl))
                        {

                            return;

                        }*/

                        Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.WolfGrowl);

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
