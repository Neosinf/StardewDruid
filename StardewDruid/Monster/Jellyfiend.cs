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
using System.Linq;

namespace StardewDruid.Monster
{
    public class Jellyfiend : Boss
    {

        public SlimeRender slimeRender;

        public Jellyfiend()
        {
        }

        public Jellyfiend(Vector2 vector, int CombatModifier, string name = "Jellyfiend")
          : base(vector, CombatModifier, name)
        {

            SpawnData.MonsterDrops(this, SpawnData.Drops.slime);

        }

        public override void SetBase()
        {

            tempermentActive = temperment.cautious;

            baseMode = 2;

            baseJuice = 3;

            basePulp = 25;

            cooldownInterval = 180;


        }

        public override void SetMode(int mode)
        {

            base.SetMode(mode);

            if (mode >= 2)
            {

                channelSet = true;

            }

        }

        public override void LoadOut()
        {

            slimeRender = new(CharacterHandle.CharacterType(realName.Value));

            characterTexture = slimeRender.slimeTexture;

            BlobWalk();

            BlobFlight();

            BlobSpecial();

            loadedOut = true;

        }

        public virtual void BlobWalk()
        {

            walkInterval = 14;

            gait = 1.5f;

            idleFrames = slimeRender.idleFrames[Character.Character.idles.idle];

            walkFrames = slimeRender.idleFrames[Character.Character.idles.none];

        }

        public virtual void BlobFlight()
        {

            flightSet = true;

            flightInterval = 6;

            flightSpeed = 9;

            flightPeak = 192;
                
            flightDefault = flightTypes.close;

            flightFrames = slimeRender.dashFrames;

            smashSet = true;

            smashFrames = flightFrames;

        }

        public virtual void BlobSpecial()
        {

            specialCeiling = 4;

            specialFloor = 0;

            channelCeiling = 4;

            channelFloor = 0;

            specialSet = true;

            sweepSet = true;

            specialInterval = 12;

            sweepInterval = 12;

            specialFrames = slimeRender.specialFrames[Character.Character.specials.special];

            channelFrames = specialFrames;

            sweepFrames = slimeRender.specialFrames[Character.Character.specials.sweep];

        }

        public override void deathIsNoEscape()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            SpellHandle death = new(new(box.Center.X, box.Top), 64, IconData.impacts.skull, new()) { displayRadius = 3, };

            Mod.instance.spellRegister.Add(death);

        }

        public override float GetScale()
        {

            return 3.5f + netMode.Value * 0.25f;

        }

        public override int GetHeight()
        {

            if (slimeRender == null)
            {

                return 32;

            }

            return slimeRender.frameHeight;

        }

        public override int GetWidth()
        {
            if (slimeRender == null)
            {

                return 32;

            }

            return slimeRender.frameWidth;

        }

        public override Rectangle GetBoundingBox()
        {

            Rectangle box = base.GetBoundingBox();

            if (netFlightActive.Value || netSmashActive.Value)
            {

                box.Width = 1;

                box.Height = 1;

            }

            return box;

        }

        public override void draw(SpriteBatch b)
        {

            if (!loadedOut || IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                
                return;

            }

            //DrawBoundingBox(b);

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            SlimeRenderAdditional renderAdditional = new();

            renderAdditional.layer = (Position.Y + 32f + (float)LayerOffset()) / 10000f;

            DrawEmote(b, localPosition, renderAdditional.layer);

            renderAdditional.scale = GetScale();

            renderAdditional.position = GetPosition(localPosition, renderAdditional.scale);

            renderAdditional.direction = netDirection.Value;

            renderAdditional.shadow = renderAdditional.position + new Vector2(0, 2 * renderAdditional.scale);

            renderAdditional.flip = ((netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3);

            if (netFlightActive.Value || netSmashActive.Value){

                renderAdditional.series = SlimeRenderAdditional.slimeseries.dash;

                renderAdditional.direction = netDirection.Value + (netFlightProgress.Value * 4);

                renderAdditional.frame = flightFrame;

            }
            else if(netSweepActive.Value)
            {

                renderAdditional.series = SlimeRenderAdditional.slimeseries.sweep;

                renderAdditional.frame = sweepFrame;

            }
            else if (netSpecialActive.Value)
            {

                renderAdditional.series = SlimeRenderAdditional.slimeseries.special;

                renderAdditional.frame = specialFrame;

            }

            slimeRender.DrawNormal(b, renderAdditional);

        }

        public override void update(GameTime time, GameLocation location)
        {

            base.update(time, location);

            //slimeRender.Update(inMotion == false);
            slimeRender.Update(true);

        }

        public override void ConnectSweep()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            float spriteScale = GetScale();

            SpellHandle slimebomb = new(currentLocation, Position, Position, 48 + (int)(24 * spriteScale), GetThreat())
            {
                type = SpellHandle.Spells.explode,

                instant = true,

                boss = this,

                display = IconData.impacts.splat,

                displayRadius = (int)spriteScale,

                sound = SpellHandle.Sounds.slime
            };

            Mod.instance.spellRegister.Add(slimebomb);

        }


        public override bool PerformChannel(Vector2 target)
        {

            PerformFlight(target, 0);

            flightPeak = 512;

            flightSpeed = 6;

            return true;

        }

        public override bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            float spriteScale = GetScale();

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 48 + (int)(24 * spriteScale), GetThreat())
            {
                display = IconData.impacts.splatter,

                displayRadius = (int)spriteScale,

                displayFactor = (int)spriteScale - 1,

                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.slimeball,

                sound = SpellHandle.Sounds.slime,

                counter = -20,

                boss = this
            };

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

        public override void ClearMove()
        {
            
            base.ClearMove();

            flightPeak = 192;

            flightSpeed = 9;

        }

    }

}

