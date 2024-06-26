﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Data.IconData;
using static StardewValley.Minigames.TargetGame;


namespace StardewDruid.Monster
{
    public class Blobfiend : Boss
    {

        public bool leapAttack;

        public Blobfiend()
        {
        }

        public Blobfiend(Vector2 vector, int CombatModifier)
          : base(vector, CombatModifier, "Blobfiend")
        {

            SpawnData.MonsterDrops(this, SpawnData.drops.slime);

        }

        public override void RandomTemperment()
        {

            Random random = new();

            int newScheme = random.Next(0, 6);

            netScheme.Set(newScheme);

            base.RandomTemperment();

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

            characterTexture = MonsterHandle.MonsterTexture(realName.Value);

            baseJuice = 3;

            basePulp = 25;

            BlobWalk();

            BlobFlight();

            BlobSpecial();

            loadedOut = true;

        }

        public void BlobWalk()
        {

            walkInterval = 12;

            gait = 1.5f;

            hoverInterval = 12;

            hoverIncrements = 2;

            hoverElevate = 1;

            idleFrames = new()
            {
                [0] = new()
                {
                    new(0,240,48,48),
                    new(48,240,48,48),
                    new(96,240,48,48),
                    new(48,240,48,48),
                },
                [1] = new()
                {
                    new(0,240,48,48),
                    new(48,240,48,48),
                    new(96,240,48,48),
                    new(48,240,48,48),
                },
                [2] = new()
                {
                    new(0,240,48,48),
                    new(48,240,48,48),
                    new(96,240,48,48),
                    new(48,240,48,48),
                },
                [3] = new()
                {
                    new(0,240,48,48),
                    new(48,240,48,48),
                    new(96,240,48,48),
                    new(48,240,48,48),
                },
            };

            walkFrames = idleFrames;

            overHead = new(0, -192);

        }

        public void BlobFlight()
        {

            flightSet = true;

            flightInterval = 6;

            flightSpeed = 9;

            flightPeak = 192;
                
            flightDefault = 1;

            flightFrames = new()
            {
                [0] = new()
                {
                    new(0,240,48,48),
                    new(48,240,48,48),
                    new(96,240,48,48),
                    new(144,240,48,48),
                    new(192,240,48,48),

                },
                [1] = new()
                {
                    new(0,240,48,48),
                    new(48,240,48,48),
                    new(96,240,48,48),
                    new(144,240,48,48),
                    new(192,240,48,48),

                },
                [2] = new()
                {
                    new(0,240,48,48),
                    new(48,240,48,48),
                    new(96,240,48,48),
                    new(144,240,48,48),
                    new(192,240,48,48),

                },
                [3] = new()
                {
                    new(0,240,48,48),
                    new(48,240,48,48),
                    new(96,240,48,48),
                    new(144,240,48,48),
                    new(192,240,48,48),

                },
                [4] = new()
                {
                    new(240,240,48,48),
                },
                [5] = new()
                {
                    new(240,240,48,48),
                },
                [6] = new()
                {
                    new(240,240,48,48),
                },
                [7] = new()
                {
                    new(240,240,48,48),
                },
                [8] = new()
                {
                    new(96,240,48,48),
                    new(48,240,48,48),
                },
                [9] = new()
                {
                    new(96,240,48,48),
                    new(48,240,48,48),
                },
                [10] = new()
                {
                    new(96,240,48,48),
                    new(48,240,48,48),

                },
                [11] = new()
                {
                    new(96,240,48,48),
                    new(48,240,48,48),

                },
            };

            smashSet = true;

            smashFrames = flightFrames;

        }

        public void BlobSpecial()
        {

            BaseSpecial();

            specialSet = true;

            sweepSet = true;

            sweepInterval = 8;

            sweepFrames = specialFrames;

        }

        public override Rectangle GetBoundingBox()
        {
            
            Rectangle box = base.GetBoundingBox();

            if (leapAttack)
            {

                box.Width = 1;

                box.Height = 1;

            }

            return box;

        }

        public override void deathIsNoEscape()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            SpellHandle death = new(new(box.Center.X, box.Top), 128, IconData.impacts.deathwhirl, new());

            death.scheme = GetScheme();

            Mod.instance.spellRegister.Add(death);

        }

        public override void draw(SpriteBatch b)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = getLocalPosition(Game1.viewport);

            float drawLayer = ((float)Position.Y + 32f) / 10000f;

            DrawEmote(b, localPosition, drawLayer);

            Color schemeColor = Mod.instance.iconData.SchemeColour(GetScheme());

            int netScale = netMode.Value > 5 ? netMode.Value - 4 : netMode.Value;

            Vector2 spritePosition;

            float spriteSize = 2f + (float)netScale;

            Rectangle shadowRect;

            int faceOffset = 0;

            if (netFlightActive.Value || netSmashActive.Value){

                int setFlightSeries = netDirection.Value + (netFlightProgress.Value * 4);

                int setFlightFrame = Math.Min(flightFrame, (flightFrames[setFlightSeries].Count - 1));

                /*List<int> blobHeights = new()
                {
                    0 * netScale,
                    2 * netScale,
                    4 * netScale,
                    6 * netScale,
                    8 * netScale,
                    8 * netScale,
                    6 * netScale,
                    4 * netScale,
                    2 * netScale,
                    0 * netScale,
                };*/

                spritePosition = new Vector2(localPosition.X + 32 - 24 * spriteSize, localPosition.Y + 64 - flightHeight - 48 * spriteSize);

                b.Draw(characterTexture, spritePosition, flightFrames[setFlightSeries][setFlightFrame], schemeColor * 0.5f, 0.0f, Vector2.Zero, spriteSize, 0, drawLayer - 0.002f);

                shadowRect = flightFrames[setFlightSeries][setFlightFrame];

                /*if (flightFrame > 1)
                {
                    
                    int slimeInterval = (int)((Game1.currentGameTime.ElapsedGameTime.TotalMilliseconds % 1000) / 166.66);

                    Rectangle slimeRect = new(slimeInterval * 48, 288, 48, 48);

                    b.Draw(characterTexture, spritePosition, slimeRect, schemeColor * 0.5f, 0.0f, Vector2.Zero, spriteSize, 0, drawLayer - 0.002f);

                }*/

                if (netSmashActive.Value)
                {

                    faceOffset = 48;

                }

            }
            else {

                float hovering = (Math.Abs(hoverHeight) * hoverElevate);

                spritePosition = new Vector2(localPosition.X + 32 - 24 * spriteSize, localPosition.Y + 64f - hovering - (48 * spriteSize));

                b.Draw(characterTexture, spritePosition, idleFrames[netDirection.Value][hoverFrame], schemeColor * 0.5f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer - 0.002f);

                shadowRect = idleFrames[netDirection.Value][hoverFrame];

                if (netSpecialActive.Value)
                {

                    faceOffset = 48;

                }

            }

            shadowRect.Y += 48;

            b.Draw(characterTexture, new Vector2(localPosition.X + 32 - 24 * spriteSize, localPosition.Y + 64f - (48f * spriteSize)), shadowRect, schemeColor * 0.35f, 0.0f, Vector2.Zero, spriteSize, 0, drawLayer - 0.003f);

            if (netScale >= 2)
            {

                faceOffset += 96;

            }

            switch (netDirection.Value)
            {

                case 0:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value*48, 96, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.001f);

                    break;

                case 1:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 48, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(0+ faceOffset, 192, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer);

                    break;

                case 2:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 0, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(0 + faceOffset, 144, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

                    break;

                case 3:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 48, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, (SpriteEffects)1, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(0 + faceOffset, 192, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, (SpriteEffects)1, drawLayer);

                    break;

            }

        }

        public virtual IconData.schemes GetScheme()
        {

            IconData.schemes scheme = schemes.apple;

            switch (netScheme.Value)
            {

                case 1:

                    scheme = schemes.grannysmith;

                    break;

                case 2:

                    scheme = schemes.pumpkin;

                    break;

                case 3:

                    scheme = schemes.plum;

                    break;

                case 4:

                    scheme = schemes.blueberry;

                    break;

                case 5:

                    scheme = schemes.melon;

                    break;

            }

            return scheme;

        }

        public override void ConnectSweep()
        {

           // Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            float damageFactor = leapAttack ? 3f : 1.5f;

            SpellHandle slimebomb = new(currentLocation, Position, Position, 64+(64*netMode.Value), (int)(damageToFarmer.Value * damageFactor));

            slimebomb.type = spells.explode;

            slimebomb.scheme = GetScheme();

            slimebomb.display = IconData.impacts.splatter;

            slimebomb.boss = this;

            Mod.instance.spellRegister.Add(slimebomb);

        }

        public override void shedChunks(int number, float scale)
        {

            Mod.instance.iconData.ImpactIndicator(currentLocation, Position, IconData.impacts.splatter, 1f+(0.25f * netMode.Value), new() { frame = 4, interval = 50, color = Mod.instance.iconData.SchemeColour((IconData.schemes)netScheme.Value) });

        }

        public override bool PerformChannel(Vector2 target)
        {

            PerformFlight(target, 0);

            flightPeak = 512;

            flightSpeed = 6;

            leapAttack = true;

            return true;

        }

        public override bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 192, GetThreat());

            fireball.type = spells.missile;

            fireball.scheme = GetScheme();

            fireball.missile = IconData.missiles.slimeball;

            fireball.display = IconData.impacts.splatter;

            fireball.projectile += 1;

            fireball.boss = this;

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

        public override void ClearMove()
        {
            
            base.ClearMove();

            flightPeak = 192;

            flightSpeed = 9;

            leapAttack = false;

        }

    }

}

