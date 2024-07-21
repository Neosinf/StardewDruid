
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
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
    public class Batwing : Monster.Boss
    {

        public Batwing()
        {
        }

        public Batwing(Vector2 vector, int CombatModifier, string name = "Batwing")
          : base(vector, CombatModifier, name)
        {

            overHead = new(16, -128);

            SpawnData.MonsterDrops(this,SpawnData.drops.bat);

        }

        public override void LoadOut()
        {

            characterTexture = MonsterHandle.MonsterTexture(realName.Value);

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

            hoverInterval = 12;

            hoverIncrements = 2;

            hoverElevate = 1f;

            walkInterval = 9;

            gait = 2;

            idleFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),

                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),

                }
            };

            walkFrames = new(idleFrames);

            schemeFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(96, 64, 32, 32),
                },
                [1] = new List<Rectangle>()
                {
                    new Rectangle(96, 32, 32, 32),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(96, 0, 32, 32),
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(96, 32, 32, 32),
                }

            };

        }

        public void BatFlight()
        {

            flightSet = true;

            flightSpeed = 12;

            flightHeight = 2;

            flightDefault = 2;

            flightInterval = 9;

            flightFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 32, 32),
                    new Rectangle(32, 160, 32, 32),
                    new Rectangle(64, 160, 32, 32),

                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),

                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32),

                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),

                },
                [4] = new List<Rectangle>()
                {
                    new Rectangle(32, 160, 32, 32),
                },
                [5] = new List<Rectangle>()
                {
                    new Rectangle(32, 128, 32, 32),
                },
                [6] = new List<Rectangle>()
                {
                    new Rectangle(32, 96, 32, 32),
                },
                [7] = new List<Rectangle>()
                {
                    new Rectangle(32, 128, 32, 32),
                },
                [8] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 32, 32),
                    new Rectangle(32, 160, 32, 32),
                    new Rectangle(64, 160, 32, 32),
                    new Rectangle(32, 160, 32, 32),
                },
                [9] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),

                },
                [10] = new List<Rectangle>()
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),

                },
                [11] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),

                }
            };

            smashSet = true;

            smashFrames = flightFrames;

        }

        public virtual void BatSpecial()
        {

            specialCeiling = 3;

            specialFloor = 0;

            specialInterval = 9;

            cooldownTimer = cooldownInterval;

            specialFrames = idleFrames;

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

            base.drawAboveAlwaysFrontLayer(b);

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = StandingPixel.Y / 10000f;

            DrawEmote(b, localPosition, drawLayer);

            float spriteScale = GetScale();

            Vector2 spritePosition = GetPosition(localPosition, spriteScale);

            bool flippity = netDirection.Value == 3 || netDirection.Value % 2 == 0 && netAlternative.Value == 3;

            if (netFlightActive.Value)
            {

                int setFlightSeries = netDirection.Value + (netFlightProgress.Value * 4);

                int setFlightFrame = Math.Min(flightFrame, (flightFrames[setFlightSeries].Count - 1));

                b.Draw(
                    characterTexture, 
                    spritePosition, 
                    flightFrames[setFlightSeries][setFlightFrame], 
                    Color.White, 
                    0, 
                    Vector2.Zero, 
                    spriteScale, 
                    flippity ? (SpriteEffects)1 : 0, 
                    drawLayer);

            }
            else if (netSpecialActive.Value)
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    flightFrames[netDirection.Value + 8][specialFrame],
                    Color.White,
                    0,
                    Vector2.Zero,
                    spriteScale,
                    flippity ? (SpriteEffects)1 : 0,
                    drawLayer);

            }
            else
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    idleFrames[netDirection.Value][hoverFrame],
                    Color.White,
                    0,
                    Vector2.Zero,
                    spriteScale,
                    flippity ? (SpriteEffects)1 : 0,
                    drawLayer);

            }

            if(netScheme.Value == 1)
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    schemeFrames[netDirection.Value][0],
                    Color.White,
                    0,
                    Vector2.Zero,
                    spriteScale,
                    flippity ? (SpriteEffects)1 : 0,
                    drawLayer);

            }

            b.Draw(Game1.shadowTexture, new(localPosition.X, localPosition.Y + 64f), Game1.shadowTexture.Bounds, Color.White, 0.0f, Vector2.Zero, 4f, 0, drawLayer - 1E-06f);

        }

        public override bool PerformSpecial(Vector2 farmerPosition)
        {

            specialTimer = 180;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle beam = new(currentLocation, farmerPosition, GetBoundingBox().Center.ToVector2(), 192, GetThreat());

            beam.type = SpellHandle.spells.echo;

            beam.display = IconData.impacts.flashbang;

            beam.scheme = IconData.schemes.blueberry;

            beam.indicator = IconData.cursors.scope;

            beam.added = new() { SpellHandle.effects.aiming, };

            beam.boss = this;

            Mod.instance.spellRegister.Add(beam);

            return true;

        }

        public override void ConnectSweep()
        {

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, new() { Position, }, 128f);

            if (targets.Count > 0)
            {

                SpellHandle bang = new(currentLocation, targets.First().Position, GetBoundingBox().Center.ToVector2(), 128, GetThreat() / 2);

                bang.type = SpellHandle.spells.explode;

                bang.display = IconData.impacts.flashbang;

                bang.scheme = IconData.schemes.blueberry;

                bang.instant = true;

                bang.boss = this;

                Mod.instance.spellRegister.Add(bang);

            }

        }

    }

}