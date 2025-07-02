
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
    public class ShadowSerpent : Monster.Boss
    {

        public SerpentRender serpentRender;

        public ShadowSerpent()
        {

        }

        public ShadowSerpent(Vector2 vector, int CombatModifier, string name = "Serpent")
          : base(vector, CombatModifier, name)
        {

            SpawnData.MonsterDrops(this,SpawnData.Drops.serpent);

        }

        public override void LoadOut()
        {

            characterTexture = MonsterHandle.MonsterTexture(realName.Value);

            serpentRender = new(CharacterHandle.CharacterType(realName.Value));

            cooldownInterval = 120;

            baseMode = 2;

            baseJuice = 4;

            basePulp = 35;

            // Hover

            walkInterval = 9;

            gait = 2;

            idleFrames = serpentRender.walkFrames;

            walkFrames = serpentRender.walkFrames;

            // Flight

            flightSet = true;

            flightSpeed = 12;

            flightHeight = 2;

            flightDefault = flightTypes.past;

            flightInterval = 9;

            flightFrames = serpentRender.dashFrames[Character.Character.dashes.dash];

            smashSet = true;

            smashFrames = serpentRender.dashFrames[Character.Character.dashes.dash];

            // Special

            specialCeiling = 3;

            specialFloor = 0;

            specialInterval = 18;

            specialSet = true;

            cooldownTimer = cooldownInterval;

            specialFrames = serpentRender.specialFrames[Character.Character.specials.special];

            sweepSet = true;

            sweepInterval = 12;

            sweepFrames = walkFrames;

            loadedOut = true;

        }

        public override float GetScale()
        {
            
            return 2.5f + netMode.Value * 0.5f;
        
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

            SerpentRenderAdditional serpentAdditional = new()
            {
                scale = GetScale()
            };

            serpentAdditional.position = GetPosition(localPosition, serpentAdditional.scale);

            serpentAdditional.layer = (float)StandingPixel.Y / 10000f + 0.001f;

            serpentAdditional.flip = netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

            serpentAdditional.direction = netDirection.Value;

            //serpentAdditional.series = SerpentRenderAdditional.serpentseries.none;

            //if (followIncrement == Vector2.Zero)
            //{

                serpentAdditional.series = SerpentRenderAdditional.serpentseries.hover;

            //}

            DrawTextAboveHead(b);

            DrawEmote(b, serpentAdditional.position, serpentAdditional.layer);

            if (netFlightActive.Value || netSmashActive.Value)
            {

                serpentAdditional.direction = netDirection.Value + (netFlightProgress.Value * 4);

                serpentAdditional.frame = Math.Min(flightFrame, (flightFrames[serpentAdditional.direction].Count - 1));

                serpentAdditional.series = SerpentRenderAdditional.serpentseries.dash;

            }
            else if (netSpecialActive.Value)
            {

                serpentAdditional.series = SerpentRenderAdditional.serpentseries.special;

            }

            serpentRender.DrawNormal(b, serpentAdditional);

        }

        public override void update(GameTime time, GameLocation location)
        {

            base.update(time, location);

            serpentRender.Update(inMotion == false);

        }

        public override bool PerformSpecial(Vector2 farmerPosition)
        {

            //currentLocation.playSound("serpentHit");

            specialTimer = 180;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle beam = new(currentLocation, farmerPosition, GetBoundingBox().Center.ToVector2(), 192 + (netMode.Value * 64), GetThreat())
            {
                type = SpellHandle.Spells.echo,

                missile = MissileHandle.missiles.bubbleecho,

                soundTrigger = SoundHandle.SoundCue.SerpentCall,

            };

            if (netScheme.Value == 2)
            {

                beam.missile = MissileHandle.missiles.fireecho;

            }

            beam.factor = 2 + netMode.Value;

            beam.boss = this;

            Mod.instance.spellRegister.Add(beam);

            return true;

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

        public override bool PerformFlight(Vector2 target, flightTypes flightType = flightTypes.none)
        {

            currentLocation.playSound("serpentDie");

            return base.PerformFlight(target, flightType);

        }

        public override void ConnectSweep()
        {

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, new() { Position, }, 128f);

            if (targets.Count > 0)
            {

                SpellHandle bang = new(currentLocation, targets.First().Position, GetBoundingBox().Center.ToVector2(), 192, GetThreat())
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