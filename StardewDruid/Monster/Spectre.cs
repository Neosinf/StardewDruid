﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
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
    public class Spectre : Monster.Boss
    {

        public HoverRender hoverRender;

        public float fadeOut;

        public Spectre()
        {
        }

        public Spectre(Vector2 vector, int CombatModifier, string name = "Spectre")
          : base(vector, CombatModifier, name)
        {

            SpawnData.MonsterDrops(this, SpawnData.Drops.shadow);

        }

        public override void LoadOut()
        {
            characterTexture = MonsterHandle.MonsterTexture(realName.Value);

            hoverRender = new(CharacterHandle.CharacterType(realName.Value));

            cooldownInterval = 240;

            GhostWalk();

            GhostFlight();

            GhostSpecial();

            loadedOut = true;

        }

        public void GhostWalk()
        {

            baseMode = 0;

            baseJuice = 2;

            basePulp = 12;

            walkInterval = 9;

            fadeOut = 0.005f;

            gait = 2;

            idleFrames = hoverRender.walkFrames;

            walkFrames = hoverRender.walkFrames;

        }

        public void GhostFlight()
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

        public virtual void GhostSpecial()
        {
            
            specialSet = true;

            specialCeiling = 3;

            specialFloor = 0;

            specialInterval = 30;

            cooldownTimer = cooldownInterval;

            specialFrames = hoverRender.specialFrames[Character.Character.specials.special];

            sweepSet = true;

            sweepInterval = 12;

            sweepFrames = walkFrames;

        }

        public override float GetScale()
        {

            return 3.5f + netMode.Value * 0.5f;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {


        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            base.drawAboveAlwaysFrontLayer(b);

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            HoverRenderAdditional hoverAdditional = new()
            {
                scale = GetScale()
            };

            hoverAdditional.position = GetPosition(localPosition, hoverAdditional.scale);

            hoverAdditional.layer = (float)StandingPixel.Y / 10000f + 0.001f;

            hoverAdditional.flip = netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

            hoverAdditional.fade = fadeOut;

            hoverAdditional.direction = netDirection.Value;

            if (netFlightActive.Value || netSmashActive.Value)
            {

                hoverAdditional.direction = netDirection.Value + (netFlightProgress.Value * 4);

                hoverAdditional.frame = Math.Min(flightFrame, (flightFrames[hoverAdditional.direction].Count - 1));

                hoverAdditional.series = HoverRenderAdditional.hoverseries.dash;

            }
            else if (netSweepActive.Value)
            {

                hoverAdditional.series = HoverRenderAdditional.hoverseries.sweep;

                hoverAdditional.frame = specialFrame;

            }
            else if (netSpecialActive.Value)
            {

                hoverAdditional.series = HoverRenderAdditional.hoverseries.special;

            }

            hoverRender.DrawNormal(b, hoverAdditional);

            DrawEmote(b, localPosition, hoverAdditional.layer);

        }

        public override void update(GameTime time, GameLocation location)
        {

            base.update(time, location);

            if (fadeOut < 0.75f)
            {

                fadeOut += 0.0025f;

            }

            hoverRender.Update(inMotion == false);

        }

        public override bool PerformSweep(Vector2 target)
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

        public override bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle beam = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 192 + (netScheme.Value * 64), GetThreat())
            {
                type = SpellHandle.Spells.echo,

                missile = MissileHandle.missiles.deathecho,

                factor = 2 + netScheme.Value,

                boss = this
            };

            Mod.instance.spellRegister.Add(beam);

            return true;

        }

        public override void ConnectSweep()
        {

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, new() { Position, }, 128f);

            if (targets.Count > 0)
            {

                SpellHandle bang = new(currentLocation, targets.First().Position, GetBoundingBox().Center.ToVector2(), 160, GetThreat())
                {
                    type = SpellHandle.Spells.explode,

                    display = IconData.impacts.flashbang,

                    instant = true,

                    boss = this
                };

                Mod.instance.spellRegister.Add(bang);

            }

        }

    }

}
