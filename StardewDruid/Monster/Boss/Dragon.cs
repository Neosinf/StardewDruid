
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Event;
using StardewDruid.Map;
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

namespace StardewDruid.Monster.Boss
{
    public class Dragon : StardewDruid.Monster.Boss.Boss
    {
        // ============================= Dragon Specific

        public Texture2D shadowTexture;

        public List<Rectangle> shadowFrames;

        public Dragon()
        {
        }

        public Dragon(Vector2 vector, int CombatModifier, string name = "PurpleDragon", string template = "Pepper Rex")
          : base(vector, CombatModifier, name, template)
        {

        }

        public override void LoadOut()
        {

            DragonWalk();

            DragonFlight();

            DragonSpecial();

            loadedOut = true;

        }
        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = getLocalPosition(Game1.viewport);

            float drawLayer = (float)StandingPixel.Y / 10000f;

            int adjustDirection = netDirection.Value == 3 ? 1 : netDirection.Value;

            //DrawEmote(b, localPosition, drawLayer);

            if (netFlightActive.Value)
            {

                b.Draw(flightTexture, new Vector2(localPosition.X - 128f, localPosition.Y - 192f - flightHeight), new Rectangle?(flightFrames[adjustDirection][flightFrame]), Color.White * 0.65f, 0, new Vector2(0.0f, 0.0f), 4f, (netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

                b.Draw(shadowTexture, new Vector2(localPosition.X - 80f, localPosition.Y - 48f), new Rectangle?(shadowFrames[netDirection.Value + 4]), Color.White * 0.25f, 0.0f, new Vector2(0.0f, 0.0f), 5f, (netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 1E-05f);

                return;

            }

            if (netSpecialActive.Value)
            {

                b.Draw(specialTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 192f), new Rectangle?(walkFrames[adjustDirection][walkFrame]), Color.White * 0.65f, 0.0f, new Vector2(0.0f, 0.0f), 4f, (netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

            }
            else if (netHaltActive.Value)
            {

                b.Draw(characterTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 192f), new Rectangle?(idleFrames[adjustDirection][0]), Color.White * 0.65f, 0.0f, new Vector2(0.0f, 0.0f), 4f, (netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

            }
            else
            {

                b.Draw(characterTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 192f), new Rectangle?(walkFrames[adjustDirection][walkFrame]), Color.White * 0.65f, 0.0f, new Vector2(0.0f, 0.0f), 4f, (netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3 ? (SpriteEffects)1 : 0, drawLayer);

            }

            b.Draw(shadowTexture, new Vector2(localPosition.X - 96f, localPosition.Y - 40f), new Rectangle?(shadowFrames[adjustDirection]), Color.White * 0.25f, 0.0f, new Vector2(0.0f, 0.0f), 4f, (netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3 ? (SpriteEffects)1 : 0, drawLayer - 1E-05f);

        }

        public virtual void DragonWalk()
        {

            characterTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", realName.Value + ".png"));

            walkCeiling = 6;

            walkFloor = 1;

            walkInterval = 9;

            followIncrement = 2;

            shadowTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DragonShadow.png"));

            shadowFrames = new List<Rectangle>()
              {
                new Rectangle(0, 0, 64, 32),
                new Rectangle(0, 32, 64, 32),
                new Rectangle(0, 64, 64, 32),
                new Rectangle(0, 32, 64, 32),
                new Rectangle(64, 0, 64, 32),
                new Rectangle(64, 32, 64, 32),
                new Rectangle(64, 64, 64, 32),
                new Rectangle(64, 32, 64, 32)
              };

            idleFrames = FrameSeries(64, 64, 0, 0, 1);

            walkFrames = FrameSeries(64, 64, 64);

            overHead = new(0, -224);

        }

        public virtual void DragonFlight()
        {

            flightInterval = 9;

            flightSpeed = 12;

            flightAscend = 8;

            flightCeiling = 4;

            flightFloor = 1;

            flightLast = 5;

            flightTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", realName.Value + "Flight.png"));

            flightFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 64, 128, 64),
                    new Rectangle(128, 64, 128, 64),
                    new Rectangle(256, 64, 128, 64),
                    new Rectangle(384, 64, 128, 64),
                    new Rectangle(256, 64, 128, 64),
                    new Rectangle(0, 64, 128, 64)
                },
                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 0, 128, 64),
                    new Rectangle(128, 0, 128, 64),
                    new Rectangle(256, 0, 128, 64),
                    new Rectangle(384, 0, 128, 64),
                    new Rectangle(256, 0, 128, 64),
                    new Rectangle(0, 0, 128, 64)
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 128, 64),
                    new Rectangle(128, 128, 128, 64),
                    new Rectangle(256, 128, 128, 64),
                    new Rectangle(384, 128, 128, 64),
                    new Rectangle(256, 128, 128, 64),
                    new Rectangle(0, 128, 128, 64)
                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 0, 128, 64),
                    new Rectangle(128, 0, 128, 64),
                    new Rectangle(256, 0, 128, 64),
                    new Rectangle(384, 0, 128, 64),
                    new Rectangle(256, 0, 128, 64),
                    new Rectangle(0, 0, 128, 64)
                }
            };

        }

        public virtual void DragonSpecial()
        {

            abilities = 3;

            cooldownInterval = 60;

            specialCeiling = 1;

            specialFloor = 0;

            reachThreshold = 64;

            safeThreshold = 544;

            specialThreshold = 320;

            specialInterval = 12;

            barrageThreshold = 544;

            barrages = new();

            specialFrames = walkFrames;

            specialTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", realName.Value + "Special.png"));

            sweepSet = false;

            sweepInterval = 12;

            sweepTexture = characterTexture;

            sweepFrames = walkFrames;
        }

    }

}