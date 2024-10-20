using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Journal;
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
    public class Firefiend : Blobfiend
    {

        public Firefiend()
        {
        }

        public Firefiend(Vector2 vector, int CombatModifier)
          : base(vector, CombatModifier, "Firefiend")
        {

            SpawnData.MonsterDrops(this, SpawnData.drops.dust);

        }
        public override void SetBase()
        {

            tempermentActive = temperment.cautious;

            baseMode = 2;

            baseJuice = 2;

            basePulp = 20;

            cooldownInterval = 180;

        }

        public override void LoadOut()
        {

            base.LoadOut();

            specialSet = false;

        }

        public override float GetScale()
        {

            return 1.5f + (0.75f * netMode.Value);

        }

        public override void RandomScheme()
        {

            int newScheme = Mod.instance.randomIndex.Next(0, 3);

            netScheme.Set(newScheme);

        }
        public override int LayerOffset()
        {

            return 32 + netLayer.Value;

        }


        public override void draw(SpriteBatch b)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = (Position.Y + (float)LayerOffset()) / 10000f;

            DrawEmote(b, localPosition, drawLayer);

            Color schemeColor = Mod.instance.iconData.SchemeColour(GetScheme());

            float spriteSize = GetScale();

            Vector2 spritePosition = GetPosition(localPosition, spriteSize);

            Vector2 shadowPosition = GetPosition(localPosition, spriteSize, true);

            // Outer fire

            Rectangle bubbleRect;

            int shadowOffset = 48;

            int faceOffset = 0;

            int timelapse = ((int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 3000) * 6 / 3000);

            // fire colour

            List<Microsoft.Xna.Framework.Color> colors = new()
            {
                Color.IndianRed,
                Color.OrangeRed,
                Color.Crimson,
                Color.IndianRed,
                Color.OrangeRed,
                Color.Crimson,
            };

            /*List<Microsoft.Xna.Framework.Color> colors = new()
            {
                Color.Gold,
                Color.LightGoldenrodYellow,
                Color.Gold,
                Color.LightGoldenrodYellow,
                Color.Gold,
                Color.LightGoldenrodYellow,
            };*/

            if (netFlightActive.Value || netSmashActive.Value)
            {

                int setFlightSeries = netDirection.Value + (netFlightProgress.Value * 4);

                int setFlightFrame = Math.Min(flightFrame, (flightFrames[setFlightSeries].Count - 1));

                bubbleRect = flightFrames[setFlightSeries][setFlightFrame].Clone();

                b.Draw(characterTexture, spritePosition, bubbleRect, colors[netMode.Value] * 0.6f, 0.0f, Vector2.Zero, spriteSize, 0, drawLayer - 0.004f);

                if (netSmashActive.Value)
                {

                    faceOffset = 48;

                }

            }
            else
            {

                bubbleRect = idleFrames[netDirection.Value][hoverFrame].Clone();

                b.Draw(characterTexture, spritePosition, bubbleRect, colors[netMode.Value] * 0.6f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer - 0.004f);

                if (netSpecialActive.Value)
                {

                    faceOffset = 48;

                }

            }

            // Shadow

            bubbleRect.Y += shadowOffset;

            b.Draw(characterTexture, shadowPosition, bubbleRect, colors[netMode.Value] * 0.15f, 0.0f, Vector2.Zero, spriteSize, 0, drawLayer - 0.005f);

            // Inner fire

            Rectangle dust = new(timelapse * 48, 336, 48, 48);

            b.Draw(characterTexture, spritePosition, dust, Color.Yellow * 0.25f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer - 0.003f);

            // Face

            if (netMode.Value >= 3)
            {

                faceOffset += 96;

            }

            switch (netDirection.Value)
            {

                case 0:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48 + 144, 96, 48, 48), colors[netMode.Value] * (0.5f + 0.1f * timelapse), 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 96, 48, 48), schemeColor * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.002f);

                    break;

                case 1:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48 + 144, 48, 48, 48), colors[netMode.Value] * (0.5f + 0.1f*timelapse), 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 48, 48, 48), schemeColor * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer - 0.002f);

                    b.Draw(characterTexture, spritePosition, new(0 + faceOffset, 192, 48, 48), Color.LightGoldenrodYellow, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer);

                    break;

                case 2:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48 + 144, 0, 48, 48), colors[netMode.Value] * (0.5f + 0.1f * timelapse), 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 0, 48, 48), schemeColor * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.002f);

                    b.Draw(characterTexture, spritePosition, new(0 + faceOffset, 144, 48, 48), Color.LightGoldenrodYellow, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

                    break;

                case 3:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48 + 144, 48, 48, 48), colors[netMode.Value] * (0.5f + 0.1f * timelapse), 0.0f, new Vector2(0.0f, 0.0f), spriteSize, (SpriteEffects)1, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 0, 48, 48), schemeColor * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.002f);

                    b.Draw(characterTexture, spritePosition, new(0 + faceOffset, 192, 48, 48), Color.LightGoldenrodYellow, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, (SpriteEffects)1, drawLayer);

                    break;

            }

        }

        public override IconData.schemes GetScheme()
        {

            IconData.schemes scheme = IconData.schemes.rock;

            switch (netScheme.Value)
            {

                case 1:

                    scheme = IconData.schemes.rockTwo;

                    break;

                case 2:

                    scheme = IconData.schemes.rockThree;

                    break;

            }

            return scheme;

        }

        public override void ConnectSweep()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            SpellHandle dustbomb = new(currentLocation, box.Center.ToVector2(), box.Center.ToVector2(), 96 + (int)(24 * GetScale()), GetThreat());

            dustbomb.type = SpellHandle.spells.explode;

            dustbomb.scheme = GetScheme();

            dustbomb.instant = true;

            dustbomb.display = IconData.impacts.puff;

            dustbomb.sound = SpellHandle.sounds.fireball;

            dustbomb.boss = this;

            Mod.instance.spellRegister.Add(dustbomb);

        }

        public override void deathIsNoEscape()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            SpellHandle death = new(new(box.Center.X, box.Top), 64 + (int)(GetScale() * 32f), IconData.impacts.deathbomb, new());

            death.scheme = GetScheme();

            Mod.instance.spellRegister.Add(death);

        }

        public override void shedChunks(int number, float scale)
        {

            Mod.instance.iconData.ImpactIndicator(currentLocation, Position, IconData.impacts.puff, 1f + (0.25f * netMode.Value), new() { frame = 4, interval = 50, color = Mod.instance.iconData.SchemeColour(GetScheme()) });

        }

    }

}

