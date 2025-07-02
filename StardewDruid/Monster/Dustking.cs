using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Render;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Extensions;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.IO;

namespace StardewDruid.Monster
{
    public class Dustking : Boss
    {
        
        public DustkingRender dustRender;


        public Dustking()
        {
        }

        public Dustking(Vector2 vector, int CombatModifier, string useSprite = "Dustking")
          : base(vector, CombatModifier, useSprite)
        {

            SpawnData.MonsterDrops(this, SpawnData.Drops.dust);

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

            dustRender = new(realName.Value);

            walkInterval = 12;

            gait = GetGait();

            idleFrames = dustRender.idleFrames;

            walkFrames = dustRender.walkFrames;

            flightFrames = dustRender.dashFrames;

            flightSet = true;

            flightInterval = 9;

            flightSpeed = 9;

            flightPeak = 128;

            flightDefault = flightTypes.target;

            smashFrames = dustRender.dashFrames;

            smashSet = true;

            specialFrames = dustRender.specialFrames;

            specialInterval = 15;

            specialCeiling = 5;

            specialFloor = 0;

            specialSet = true;

            channelFrames = dustRender.specialFrames;

            channelCeiling = 5;

            channelFloor = 0;

            channelSet = true;

            sweepFrames = dustRender.sweepFrames;

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

            return 48;

        }

        public override int GetWidth()
        {

            return 48;

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

            DustkingRenderAdditional additional = new()
            {

                layer = (Position.Y + netLayer.Value) / 10000f + 0.0032f,

                scale = GetScale()
            
            };

            additional.position = GetPosition(localPosition, additional.scale);

            DrawEmote(b, localPosition, additional.layer);

            additional.flip = netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

            if (netSmashActive.Value || netFlightActive.Value)
            {

                int setFlightSeries = netDirection.Value + (netFlightProgress.Value * 4);

                int setFlightFrame = Math.Min(flightFrame, (flightFrames[setFlightSeries].Count - 1));

                additional.series = DustkingRenderAdditional.dustseries.dash;

                additional.direction = setFlightSeries;

                additional.frame = setFlightFrame;

                dustRender.DrawNormal(b, additional);

                return;

            }
            else if (netSweepActive.Value)
            {

                additional.series = DustkingRenderAdditional.dustseries.sweep;

                additional.direction = netDirection.Value;

                additional.frame = sweepFrame;

                dustRender.DrawNormal(b, additional);

                return;

            }
            else if (netSpecialActive.Value)
            {

                additional.series = DustkingRenderAdditional.dustseries.special;

                additional.direction = netDirection.Value;

                additional.frame = specialFrame;

                dustRender.DrawNormal(b, additional);

                return;

            }

            additional.direction = netDirection.Value;

            additional.frame = walkFrame;

            dustRender.DrawNormal(b, additional);

        }

        public override bool PerformChannel(Vector2 target)
        {

            if (base.PerformChannel(target))
            {

                Mod.instance.sounds.ActiveCue(Handle.SoundHandle.SoundCue.BearRoar);

                return true;

            }

            return false;

        }

        public override bool PerformSpecial(Vector2 target)
        {

            if(base.PerformSpecial(target))
            {

                Mod.instance.sounds.ActiveCue(Handle.SoundHandle.SoundCue.BearRoar);

                return true;
            
            }

            return false;

        }

        public override void ConnectSweep()
        {

            float useThreat = GetThreat();

            Vector2 selfTarget = GetBoundingBox().Center.ToVector2();

            SpellHandle dustbomb = new(currentLocation, selfTarget, selfTarget, 96 + (int)(24 * GetScale()), useThreat)
            {

                type = SpellHandle.Spells.explode,

                instant = true,

                display = IconData.impacts.puff,

                scheme = IconData.schemes.gray,

                sound = SpellHandle.Sounds.fireball,

                boss = this

            };

            Mod.instance.spellRegister.Add(dustbomb);

        }

        public override void deathIsNoEscape()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            SpellHandle death = new(new(box.Center.X, box.Top), 64 + (int)(GetScale() * 32f), IconData.impacts.deathbomb, new())
            {
                displayRadius = 3,
            };

            Mod.instance.spellRegister.Add(death);

        }

        public override void shedChunks(int number, float scale)
        {

            Mod.instance.iconData.ImpactIndicator(currentLocation, Position, IconData.impacts.puff, 1f + (0.25f * netMode.Value), new() { frame = 4, interval = 50, scheme = IconData.schemes.darkgray });

        }

    }

}

