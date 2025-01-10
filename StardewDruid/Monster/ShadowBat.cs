﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Events;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace StardewDruid.Monster
{
    public class ShadowBat : Monster.Boss
    {

        public HoverRender hoverRender;

        public ShadowBat()
        {
        }

        public ShadowBat(Vector2 vector, int CombatModifier, string name = "Bat")
          : base(vector, CombatModifier, name)
        {

            overHead = new(16, -128);

            SpawnData.MonsterDrops(this,SpawnData.drops.bat);

        }

        public override void LoadOut()
        {

            characterTexture = MonsterHandle.MonsterTexture(realName.Value);

            hoverRender = new(realName.Value);

            cooldownInterval = 120;

            BatWalk();

            BatFlight();

            BatSpecial();

            loadedOut = true;

        }

        public override void SetMode(int mode)
        {
            base.SetMode(mode);

            if(mode >= 1)
            {

                specialSet = true;

            }

        }

        public void BatWalk()
        {

            baseMode = 0;

            baseJuice = 2;

            basePulp = 25;

            hoverInterval = 18;

            hoverIncrements = 2;

            hoverElevate = 1f;

            walkInterval = 9;

            gait = 2;

            idleFrames = hoverRender.walkFrames;

            walkFrames = hoverRender.walkFrames;

        }

        public void BatFlight()
        {

            flightSet = true;

            flightSpeed = 12;

            flightHeight = 2;

            flightDefault = flightTypes.past;

            flightInterval = 9;

            flightFrames = hoverRender.dashFrames[Character.Character.dashes.dash];

            smashSet = true;

            smashFrames = hoverRender.dashFrames[Character.Character.dashes.dash];

        }

        public virtual void BatSpecial()
        {

            specialCeiling = 3;

            specialFloor = 0;

            specialInterval = 9;

            cooldownTimer = cooldownInterval;

            specialFrames = hoverRender.specialFrames[Character.Character.specials.special];

            sweepSet = true;

            sweepInterval = 12;

            sweepFrames = walkFrames;

        }

        public override float GetScale()
        {
            
            return 3f + netMode.Value * 0.5f;
        
        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {


        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            //DrawBoundingBox(b);

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            HoverRenderAdditional hoverAdditional = new();

            hoverAdditional.scale = GetScale();

            hoverAdditional.position = GetPosition(localPosition, hoverAdditional.scale);

            hoverAdditional.layer = (float)StandingPixel.Y / 10000f + 0.001f;

            hoverAdditional.flip = netDirection.Value == 3 || netDirection.Value % 2 == 0 && netAlternative.Value == 3;

            hoverAdditional.direction = netDirection.Value;

            hoverAdditional.frame = hoverFrame;

            hoverAdditional.series = HoverRenderAdditional.hoverseries.hover;

            DrawTextAboveHead(b, hoverAdditional.position);

            DrawEmote(b, hoverAdditional.position, hoverAdditional.layer);

            if (netFlightActive.Value || netSmashActive.Value)
            {

                hoverAdditional.direction = netDirection.Value + (netFlightProgress.Value * 4);

                hoverAdditional.frame = Math.Min(flightFrame, (flightFrames[hoverAdditional.direction].Count - 1));

                hoverAdditional.series = HoverRenderAdditional.hoverseries.dash;

            }
            else if (netSpecialActive.Value)
            {

                hoverAdditional.frame = specialFrame;

                hoverAdditional.series = HoverRenderAdditional.hoverseries.special;

            }

            if(netScheme.Value == 1)
            {

                hoverAdditional.hat = true;

            }

            hoverRender.DrawNormal(b, hoverAdditional);

        }

        public override bool PerformSpecial(Vector2 farmerPosition)
        {

            specialTimer = 180;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle beam = new(currentLocation, farmerPosition, GetBoundingBox().Center.ToVector2(), 192+(netScheme.Value*64), GetThreat());

            beam.type = SpellHandle.spells.echo;

            beam.factor = 2 + netScheme.Value;

            beam.boss = this;

            Mod.instance.spellRegister.Add(beam);

            return true;

        }

        public override bool PerformSweep()
        {
            if (!sweepSet) { return false; }

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, new() { Position, }, 96f + (GetWidth() * 3));

            if (targets.Count > 0)
            {

                Vector2 targetFarmer = targets.First().Position;

                return PerformFlight(targetFarmer);

            }

            return false;

        }

        public override void ConnectSweep()
        {

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, new() { Position, }, 128f);

            if (targets.Count > 0)
            {

                SpellHandle bang = new(currentLocation, targets.First().Position, GetBoundingBox().Center.ToVector2(), 128, GetThreat() / 2);

                bang.type = SpellHandle.spells.explode;

                bang.display = IconData.impacts.flashbang;

                bang.instant = true;

                bang.boss = this;

                Mod.instance.spellRegister.Add(bang);

            }

        }

    }

}