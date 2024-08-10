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
using System.Linq;
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Data.IconData;


namespace StardewDruid.Monster
{
    public class Dustfiend : Blobfiend
    {

        public Dustfiend()
        {
        }

        public Dustfiend(Vector2 vector, int CombatModifier)
          : base(vector, CombatModifier, "Dustfiend")
        {

            SpawnData.MonsterDrops(this, SpawnData.drops.dust);

        }

        public override void LoadOut()
        {

            base.LoadOut();

            baseJuice = 2;

            basePulp += 5;

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

        public override void draw(SpriteBatch b)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = ((float)Position.Y + 32f) / 10000f;

            DrawEmote(b, localPosition, drawLayer);

            Color schemeColor = Mod.instance.iconData.SchemeColour(GetScheme());

            float spriteSize = GetScale();

            Vector2 spritePosition = GetPosition(localPosition, spriteSize);

            Vector2 shadowPosition = GetPosition(localPosition, spriteSize, true);

            Rectangle bubbleRect;

            int shadowOffset = 48;

            int faceOffset = 0;

            if (netFlightActive.Value || netSmashActive.Value){

                int setFlightSeries = netDirection.Value + (netFlightProgress.Value * 4);

                int setFlightFrame = Math.Min(flightFrame, (flightFrames[setFlightSeries].Count - 1));

                bubbleRect = flightFrames[setFlightSeries][setFlightFrame].Clone();

                b.Draw(characterTexture, spritePosition, bubbleRect, schemeColor * 0.4f, 0.0f, Vector2.Zero, spriteSize, 0, drawLayer - 0.002f);

                if (netSmashActive.Value)
                {

                    faceOffset = 48;

                }

            }
            else {


                bubbleRect = idleFrames[netDirection.Value][hoverFrame].Clone();

                b.Draw(characterTexture, spritePosition, bubbleRect, schemeColor * 0.4f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer - 0.002f);

                if (netSpecialActive.Value)
                {

                    faceOffset = 48;

                }

            }

            bubbleRect.Y += shadowOffset;

            b.Draw(characterTexture, shadowPosition, bubbleRect, schemeColor * 0.2f, 0.0f, Vector2.Zero, spriteSize, 0, drawLayer - 0.003f);

            List<Microsoft.Xna.Framework.Color> colors = new()
            {
                new(34,160,156),
                new(253,194,129),
                Color.LightPink,
                new(34,160,156),
                new(253,194,129),
                Color.LightPink,
            };

            switch (netDirection.Value)
            {

                case 0:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value*48, 96, 48, 48), schemeColor * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.001f);

                    break;

                case 1:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 48, 48, 48), schemeColor * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(0+ faceOffset, 192, 48, 48), colors[netMode.Value], 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer);

                    break;

                case 2:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 0, 48, 48), schemeColor * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(0 + faceOffset, 144, 48, 48), colors[netMode.Value], 0.0f, new Vector2(0.0f, 0.0f), spriteSize, netAlternative.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

                    break;

                case 3:

                    b.Draw(characterTexture, spritePosition, new(netScheme.Value * 48, 48, 48, 48), schemeColor * 0.75f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, (SpriteEffects)1, drawLayer - 0.001f);

                    b.Draw(characterTexture, spritePosition, new(0 + faceOffset, 192, 48, 48), colors[netMode.Value], 0.0f, new Vector2(0.0f, 0.0f), spriteSize, (SpriteEffects)1, drawLayer);

                    break;

            }

            int timelapse = ((int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 3000) * 6 / 3000);

            Rectangle dust = new(144 + (timelapse % 3) * 48, 0 + (int)(timelapse / 3) * 48, 48, 48);

            b.Draw(characterTexture, spritePosition, dust, schemeColor*0.1f, 0.0f, new Vector2(0.0f, 0.0f), spriteSize, 0, drawLayer + 0.001f);


        }

        public override IconData.schemes GetScheme()
        {

            IconData.schemes scheme = schemes.rock;

            switch (netScheme.Value)
            {

                case 1:

                    scheme = schemes.rockTwo;

                    break;

                case 2:

                    scheme = schemes.rockThree;

                    break;

            }

            return scheme;

        }

        public override void ConnectSweep()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            SpellHandle dustbomb = new(currentLocation, box.Center.ToVector2(), box.Center.ToVector2(), 96 + (int)(24 * GetScale()), GetThreat());

            dustbomb.type = spells.explode;

            dustbomb.scheme = GetScheme();

            dustbomb.instant = true;

            dustbomb.display = IconData.impacts.puff;

            dustbomb.sound = sounds.dustMeep;

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

