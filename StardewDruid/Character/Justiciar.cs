using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.FruitTrees;
using StardewValley.Internal;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace StardewDruid.Character
{
    public class Justiciar : StardewDruid.Character.Character
    {

        public Dictionary<int, List<Rectangle>> hatFrames = new();

        public Justiciar()
        {
        }

        public Justiciar(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {
            base.LoadOut();

            setScale = 3.75f;

            idleFrames[idles.standby] = new()
            {
                [0] = new List<Rectangle> { new Rectangle(160, 32, 32, 32), },
            };

            hatFrames = new()
            {
                [0] = new()
                {
                    new(192, 64, 32, 32),
                },
                [1] = new()
                {
                    new(192, 32, 32, 32),
                },
                [2] = new()
                {
                    new(192, 0, 32, 32),
                },
                [3] = new()
                {
                    new(192, 32, 32, 32),
                },
            };

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            base.draw(b, alpha);

            if (IsInvisible || !Utility.isOnScreen(Position, 128) || characterTexture == null)
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = (float)StandingPixel.Y / 10000f + 0.001f;

            float fade = fadeOut == 0 ? 1f : fadeOut;

            if (netIdle.Value == (int)Character.idles.kneel)
            {

                b.Draw(
                    characterTexture,
                    SpritePosition(localPosition) - new Vector2(0, 8f * setScale),
                    hatFrames[netDirection.Value][0],
                    Color.White * fade,
                    0.0f,
                    new Vector2(16),
                    setScale,
                    SpriteFlip() ? (SpriteEffects)1 : 0,
                    drawLayer + 0.0001f
                );
            }
            else if (netSpecial.Value == (int)Character.specials.gesture)
            {

                b.Draw(
                    characterTexture,
                    SpritePosition(localPosition) - new Vector2(0, 8f * setScale),
                    hatFrames[1][0],
                    Color.White * fade,
                    0.0f,
                    new Vector2(16),
                    setScale,
                    SpriteFlip() ? (SpriteEffects)1 : 0,
                    drawLayer + 0.0001f
                );
            }
            else
            {

                b.Draw(
                    characterTexture,
                    SpritePosition(localPosition) - new Vector2(0, 14f * setScale),
                    hatFrames[netDirection.Value][0],
                    Color.White * fade,
                    0.0f,
                    new Vector2(16),
                    setScale,
                    SpriteFlip() ? (SpriteEffects)1 : 0,
                    drawLayer + 0.0001f
                );

            }


        }

        public override void behaviorOnFarmerPushing()
        {

            return;

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {

            return false;

        }

    }

}
