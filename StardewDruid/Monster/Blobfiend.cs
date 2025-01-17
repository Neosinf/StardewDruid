﻿using Microsoft.Xna.Framework;
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
using System.Linq;
using static StardewDruid.Data.IconData;


namespace StardewDruid.Monster
{
    public class Blobfiend : Boss
    {

        public bool leapAttack;

        public Blobfiend()
        {
        }

        public Blobfiend(Vector2 vector, int CombatModifier, string name = "Blobfiend")
          : base(vector, CombatModifier, name)
        {

            SpawnData.MonsterDrops(this, SpawnData.drops.slime);

        }

        public override void SetBase()
        {

            tempermentActive = temperment.cautious;

            baseMode = 2;

            baseJuice = 3;

            basePulp = 25;

            cooldownInterval = 180;

        }

        public override void RandomTemperment()
        {
            
            RandomScheme();

            base.RandomTemperment();

        }

        public virtual void RandomScheme()
        {

            int newScheme = Mod.instance.randomIndex.Next(0, 6);

            netScheme.Set(newScheme);

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

            BlobWalk();

            BlobFlight();

            BlobSpecial();

            loadedOut = true;

        }

        public void BlobWalk()
        {

            walkInterval = 14;

            gait = 1.5f;

            hoverInterval = 14;

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
                
            flightDefault = flightTypes.close;

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

        public override void deathIsNoEscape()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            SpellHandle death = new(new(box.Center.X, box.Top), 64 + (int)(GetScale() * 32f), IconData.impacts.skull, new());

            death.scheme =GetSpellScheme();

            Mod.instance.spellRegister.Add(death);

        }


        public override float GetScale()
        {

            return 3f + netMode.Value*0.5f;

        }

        public override int GetHeight()
        {

            return 48;

        }

        public override int GetWidth()
        {

            return 48;

        }

        public override Rectangle GetBoundingBox()
        {

            /*Vector2 spritePosition = GetPosition(Position);

            float spriteScale = GetScale();

            int width = GetWidth();

            int height = GetHeight();

            Rectangle box = new(
                (int)(spritePosition.X + (spriteScale * 2)),
                (int)(spritePosition.Y + (spriteScale * 4)),
                (int)(spriteScale * (width - 4)),
                (int)(spriteScale * (height - 4))
            );

            if (leapAttack)
            {

                box.Width = 1;

                box.Height = 1;

            }*/

            Rectangle box = base.GetBoundingBox();

            if (leapAttack)
            {

                box.Width = 1;

                box.Height = 1;

            }

            return box;

        }

        public override void draw(SpriteBatch b)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            //DrawBoundingBox(b);

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = (Position.Y + 32f + (float)LayerOffset()) / 10000f ;

            DrawEmote(b, localPosition, drawLayer);

            Color schemeColor = GetSchemeColour();

            float spriteSize = GetScale();

            Vector2 spritePosition = GetPosition(localPosition, spriteSize);

            Vector2 shadowPosition = GetPosition(localPosition, spriteSize, true);

            Rectangle shadowRect;

            int faceOffset = 0;

            if (netFlightActive.Value || netSmashActive.Value){

                int setFlightSeries = netDirection.Value + (netFlightProgress.Value * 4);

                int setFlightFrame = Math.Min(flightFrame, (flightFrames[setFlightSeries].Count - 1));

                Rectangle flightRect = flightFrames[setFlightSeries][setFlightFrame];

                b.Draw(characterTexture, spritePosition, flightRect, schemeColor * 0.5f, 0.0f, new Vector2(24, 24), spriteSize, 0, drawLayer - 0.002f);

                shadowRect = flightFrames[setFlightSeries][setFlightFrame].Clone();

                if (netSmashActive.Value)
                {

                    faceOffset = 48;

                }

            }
            else {

                Rectangle idleRect = idleFrames[netDirection.Value][hoverFrame];

                b.Draw(characterTexture, spritePosition, idleRect, schemeColor * 0.5f, 0.0f, new Vector2(24, 24), spriteSize, 0, drawLayer - 0.002f);

                shadowRect = idleFrames[netDirection.Value][hoverFrame].Clone();

                if (netSpecialActive.Value)
                {

                    faceOffset = 48;

                }

            }

            shadowRect.Y += 48;

            b.Draw(characterTexture, shadowPosition, shadowRect, schemeColor * 0.35f, 0.0f, new Vector2(24, 24), spriteSize, 0, drawLayer - 0.003f);

            Rectangle gleamRect = shadowRect.Clone();

            gleamRect.Y += 48;

            b.Draw(characterTexture, spritePosition, gleamRect, Color.White * 0.35f, 0.0f, new Vector2(24, 24), spriteSize, 0, drawLayer + 0.001f);

            if (netMode.Value >= 2)
            {

                faceOffset += 96;

            }

            switch (netDirection.Value)
            {

                case 0:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value*48, 96, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(24,24), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.001f);

                    break;

                case 1:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 48, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(24,24), spriteSize, 0, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(0+ faceOffset, 192, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(24, 24), spriteSize, 0, drawLayer);

                    break;

                case 2:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 0, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(24, 24), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(0 + faceOffset, 144, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(24, 24), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

                    break;

                case 3:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 48, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(24, 24), spriteSize, (SpriteEffects)1, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(0 + faceOffset, 192, 48, 48), Color.White * 0.75f, 0.0f, new Vector2(24, 24), spriteSize, (SpriteEffects)1, drawLayer);

                    break;

            }

        }

        public virtual IconData.schemes GetSpellScheme()
        {

            switch (netScheme.Value)
            {

                default: // apple

                    return IconData.schemes.slime_three;

                case 1: // grannysmith

                    return IconData.schemes.slime_two;
                case 2: // pumpkin

                    return IconData.schemes.slime_three;
                case 3: // plum

                    return IconData.schemes.slime_one;
                case 4: // blueberry

                    return IconData.schemes.slime_one;
                case 5: // melon

                    return IconData.schemes.slime_two;

            }


        }

        public virtual Microsoft.Xna.Framework.Color GetSchemeColour()
        {

            switch (netScheme.Value)
            {

                default: // apple

                    return new(237, 32, 36);

                case 1: // grannysmith

                    return new(141, 198, 63);
                case 2: // pumpkin

                    return new(238, 102, 37);
                case 3: // plum

                    return new(196, 35, 86);
                case 4: // blueberry

                    return new(84, 104, 177);
                case 5: // melon

                    return new(107, 192, 111);

            }

        }

        public override void ConnectSweep()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            SpellHandle slimebomb = new(currentLocation, box.Center.ToVector2(), box.Center.ToVector2(), 96 +(int)(24 * GetScale()), GetThreat());

            slimebomb.type = SpellHandle.spells.explode;

            slimebomb.instant = true;

            slimebomb.scheme = GetSpellScheme();

            slimebomb.display = IconData.impacts.splatter;

            slimebomb.boss = this;

            Mod.instance.spellRegister.Add(slimebomb);

        }

        public override void shedChunks(int number, float scale)
        {

            Mod.instance.iconData.ImpactIndicator(currentLocation, Position, IconData.impacts.splatter, 1f + (0.25f * netMode.Value), new() { frame = 4, interval = 50, color = GetSchemeColour() }); 
            
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

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 128 + (int)(32 * GetScale()), GetThreat());

            fireball.type = SpellHandle.spells.missile;

            fireball.scheme = GetSpellScheme();

            fireball.missile = MissileHandle.missiles.slimeball;

            fireball.display = IconData.impacts.splatter;

            fireball.factor += 1;

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

