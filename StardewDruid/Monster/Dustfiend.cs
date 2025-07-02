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
    public class Dustfiend : Boss
    {
        
        public FiendRender fiendRender;


        public Dustfiend()
        {
        }

        public Dustfiend(Vector2 vector, int CombatModifier, string useSprite = "Dustfiend")
          : base(vector, CombatModifier, useSprite)
        {

            SpawnData.MonsterDrops(this, SpawnData.Drops.dust);

        }

        public override void SetBase()
        {

            tempermentActive = temperment.cautious;

            baseMode = 2;

            baseJuice = 3;

            basePulp = 15;

            cooldownInterval = 180;

            warp = IconData.warps.smoke;

        }

        public override void LoadOut()
        {

            fiendRender = new(CharacterHandle.CharacterType(realName.Value));

            characterTexture = fiendRender.fiendTexture;

            BlobWalk();

            BlobFlight();

            BlobSpecial();

            loadedOut = true;

        }

        public virtual void BlobWalk()
        {

            walkInterval = 14;

            gait = 1.5f;

            idleFrames = fiendRender.idleFrames;

            walkFrames = idleFrames;

        }

        public virtual void BlobFlight()
        {

            flightSet = true;

            flightInterval = 6;

            flightSpeed = 9;

            flightPeak = 192;

            flightDefault = flightTypes.close;

            flightFrames = fiendRender.dashFrames;

            smashSet = true;

            smashFrames = flightFrames;

        }

        public virtual void BlobSpecial()
        {
            
            specialCeiling = 8;

            specialFloor = 0;

            channelCeiling = 8;

            channelFloor = 0;

            specialSet = true;

            channelSet = true;

            sweepSet = true;

            specialInterval = 12;

            sweepInterval = 12;

            specialFrames = fiendRender.specialFrames[Character.Character.specials.special];

            channelFrames = specialFrames;

            sweepFrames = fiendRender.specialFrames[Character.Character.specials.sweep];

        }


        public override float GetScale()
        {

            return 2f + netMode.Value * 1f;

        }

        public override int GetHeight()
        {

            if(fiendRender == null)
            {

                return 32;

            }

            return fiendRender.frameHeight;

        }

        public override int GetWidth()
        {
            if (fiendRender == null)
            {

                return 32;

            }

            return fiendRender.frameWidth;

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

            FiendRenderAdditional renderAdditional = new();

            renderAdditional.layer = (Position.Y + (float)LayerOffset()) / 10000f;

            DrawEmote(b, localPosition, renderAdditional.layer);

            renderAdditional.scale = GetScale();

            renderAdditional.position = GetPosition(localPosition, renderAdditional.scale);

            renderAdditional.direction = netDirection.Value;

            renderAdditional.shadow = renderAdditional.position + new Vector2(0, 8 * renderAdditional.scale);

            renderAdditional.flip = ((netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3);

            if (netPosturing.Value)
            {

                renderAdditional.series = FiendRenderAdditional.fiendseries.fluff;

                if (netSpecialActive.Value)
                {

                    renderAdditional.frame = (specialFrame % 2) + 1;

                }
                else
                {

                    renderAdditional.frame = 0;

                }

            }
            else
            if (netFlightActive.Value || netSmashActive.Value)
            {

                renderAdditional.series = FiendRenderAdditional.fiendseries.dash;

                renderAdditional.direction = netDirection.Value + (netFlightProgress.Value * 4);

                renderAdditional.frame = flightFrame;

            }
            else if (netSweepActive.Value)
            {

                renderAdditional.series = FiendRenderAdditional.fiendseries.sweep;

                renderAdditional.frame = sweepFrame;

            }
            else if (netSpecialActive.Value)
            {

                renderAdditional.series = FiendRenderAdditional.fiendseries.special;

                renderAdditional.frame = specialFrame;
            }

            fiendRender.DrawNormal(b, renderAdditional);

        }

        public override void update(GameTime time, GameLocation location)
        {

            base.update(time, location);

            //fiendRender.Update(inMotion == false);
            fiendRender.Update(true);

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

            if(Health > (MaxHealth / 3))
            {

                return false;

            }

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            float spriteScale = GetScale();

            float useThreat = GetThreat();

            Vector2 selfTarget = GetBoundingBox().Center.ToVector2();

            SpellHandle fireball = new(currentLocation, selfTarget, selfTarget, 96 + (int)(32 * spriteScale), useThreat, useThreat)
            {

                display = IconData.impacts.none,

                displayRadius = (int)spriteScale,

                type = SpellHandle.Spells.explode,

                sound = SpellHandle.Sounds.fireball,

                instant = true,

                counter = 4 - (specialCeiling * specialInterval),

                boss = this,

                added = new() { SpellHandle.Effects.detonate }
            
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

            Mod.instance.iconData.ImpactIndicator(currentLocation, Position, IconData.impacts.puff, 1f + (0.25f * netMode.Value), new() { frame = 4, interval = 50, scheme = IconData.schemes.darkgray});

        }

    }

}

