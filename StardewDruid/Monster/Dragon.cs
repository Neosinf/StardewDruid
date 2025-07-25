﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Buildings;
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
    public class Dragon : StardewDruid.Monster.Boss
    {
        
        // ============================= Dragon Specific

        public NetBool netBreathActive = new NetBool(false);

        public DragonRender dragonRender;

        public Dragon()
        {
        }

        public Dragon(Vector2 vector, int CombatModifier, string name = "Dragon")
          : base(vector, CombatModifier, name)
        {

            SpawnData.MonsterDrops(this, SpawnData.Drops.dragon);

        }

        public override void LoadOut()
        {

            baseMode = 2;

            baseJuice = 5;

            basePulp = 40;

            gait = 2;

            walkInterval = 9;

            flightInterval = 12;

            flightSpeed = 12;

            flightDefault = flightTypes.past;

            sweepInterval = 6;

            cooldownInterval = 150;

            specialCeiling = 2;

            specialFloor = 0;

            specialInterval = 12;

            flightSet = true;

            smashSet = true;

            sweepSet = true;

            specialSet = true;

            channelSet = true;

            dragonRender = new();

            loadedOut = true;

        }

        protected override void initNetFields()
        {
            base.initNetFields();
            NetFields.AddField(netBreathActive,"netBreathActive");

        }

        public override Rectangle GetBoundingBox()
        {
            
            Vector2 position = Position;

            float netScale = GetScale();

            Vector2 baseVector = new(Position.X + 32f - (28f * netScale), Position.Y + 64f - (56f * netScale));

            Rectangle baseRectangle = new((int)baseVector.X,(int)baseVector.Y, (int)(56f * netScale), (int)(56f * netScale));

            return baseRectangle;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            //DrawBoundingBox(b);

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = (float)StandingPixel.Y / 10000f;

            DrawEmote(b, localPosition, drawLayer);

            if (netFlightActive.Value || netSmashActive.Value)
            {

                return;

            }

            float netScale = GetScale();

            Vector2 spritePosition = new Vector2(localPosition.X + 32 - (32 * netScale), localPosition.Y + 64 - (64 * netScale));

            bool flippant = ((netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3);

            if (netSweepActive.Value)
            {

                if (netSpecialActive.Value)
                {

                    dragonRender.drawSweep(b, spritePosition, new() { direction = netDirection.Value, scale = netScale, version = 1, frame = sweepFrame, flip = flippant, layer = drawLayer, });

                }
                else
                {

                    dragonRender.drawSweep(b, spritePosition, new() { direction = netDirection.Value, scale = netScale, frame = sweepFrame, flip = flippant, layer = drawLayer, });

                }

                return;

            }

            if (netSpecialActive.Value || netChannelActive.Value)
            {

                dragonRender.drawWalk(b, spritePosition, new() { direction = netDirection.Value, scale = netScale, version = 1, breath = netBreathActive.Value, frame = walkFrame, flip = flippant, layer = drawLayer, });

            }
            else
            {

                dragonRender.drawWalk(b, spritePosition, new() { direction = netDirection.Value, scale = netScale, frame = netHaltActive.Value ? 0 : walkFrame, flip = flippant, layer = drawLayer, });

            }

        }


        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {
            
            Vector2 localPosition = Position - new Vector2((float)Game1.viewport.X, (float)Game1.viewport.Y);

            if (netFlightActive.Value || netSmashActive.Value)
            {

                float netScale = GetScale();

                Vector2 spritePosition = new Vector2(localPosition.X + 32 - (32 * netScale), localPosition.Y + 64 - (64 * netScale));

                bool flippant = ((netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3);

                float drawLayer = (float)StandingPixel.Y / 10000f;

                int useFrame = flightFrame;

                if(netFlightProgress.Value == 1)
                {

                    useFrame = flightFrame % 4;

                }

                if (netSpecialActive.Value)
                {

                    dragonRender.drawFlight(b, spritePosition, new() { direction = netDirection.Value, flight = flightHeight, scale = netScale, frame = useFrame, flip = flippant, layer = drawLayer, version = 1, breath = netBreathActive.Value, });

                }
                else
                {

                    dragonRender.drawFlight(b, spritePosition, new() { direction = netDirection.Value, flight = flightHeight, scale = netScale, frame = useFrame, flip = flippant, layer = drawLayer, });

                }

            }

            DrawTextAboveHead(b);

        }

        public override float GetScale()
        {

            return 2f + (netMode.Value * 0.75f);

        }

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return IconData.DisplayRectangle(Data.IconData.displays.ether);

        }

        public override SpellHandle.Effects IsCursable(SpellHandle.Effects effect = SpellHandle.Effects.knock)
        {

            if (effect == SpellHandle.Effects.immolate)
            {

                return SpellHandle.Effects.none;

            }

            return base.IsCursable(effect);

        }

        public override bool PerformSpecial(Vector2 farmerPosition)
        {

            specialTimer = 60;

            SetCooldown(1);

            netSpecialActive.Set(true);

            netBreathActive.Set(true);

            //currentLocation.playSound("DragonFire");

            List<Vector2> zero = BlastZero();

            SpellHandle burn = new(currentLocation, zero[0] * 64, Position, (int)GetScale() * 32 + 96, GetThreat())
            {
                type = SpellHandle.Spells.explode,

                scheme = IconData.schemes.stars,

                display = IconData.impacts.impact,

                soundTrigger = SoundHandle.SoundCue.DragonFire,

                instant = true,

                added = new() { SpellHandle.Effects.embers, }
            };

            Mod.instance.spellRegister.Add(burn);

            return true;

        }

        public override bool PerformChannel(Vector2 target)
        {

            return PerformFlight(target);

            /*specialTimer = (specialCeiling + 1) * specialInterval * 2;

            netChannelActive.Set(true);

            netBreathActive.Set(false);

            SetCooldown(2f);

            int offset = Mod.instance.randomIndex.Next(2);

            float radius = GetScale();

            for (int k = 0; k < 5; k++)
            {

                Vector2 impact = target;
                
                if(k < 4)
                {

                    List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(currentLocation, ModUtility.PositionToTile(target), 5, true, (k * 2) + offset);

                    if (castSelection.Count == 0)
                    {

                        continue;

                    }

                    impact = castSelection[Mod.instance.randomIndex.Next(castSelection.Count)] * 64;

                }

                SpellHandle missile = new(currentLocation, impact, Position, (int)radius * 32 + 96, GetThreat() * 0.75f);

                missile.type = SpellHandle.spells.ballistic;

                missile.display = IconData.impacts.impact;

                missile.missile = MissileHandle.missiles.fireball;

                missile.projectileSpeed = 1f;

                missile.factor =(int)radius;

                missile.added = new() { SpellHandle.effects.burn, };

                missile.boss = this;

                Mod.instance.spellRegister.Add(missile);

            }

            return true;*/

        }

        public override void ClearSpecial()
        {

            base.ClearSpecial();

            netBreathActive.Set(false);

        }

        public override bool ValidPush()
        {

            return false;

        }

        public override int WalkCount()
        {

            return 6;

        }

        public override int IdleCount()
        {

            return 1;

        }

        public override int SweepCount()
        {

            return 6;

        }

        public override int FlightCount(int segment = 0)
        {

            switch (segment)
            {
                
                default:

                case 0: 
                    
                    return 1;

                case 1: 
                    
                    return 1;

                case 2: 
                    
                    return 1;

                case 3: 
                    
                    return 3;

            }

        }

        public override int SmashCount(int segment = 0)
        {

            return FlightCount(segment);

        }

    }

}