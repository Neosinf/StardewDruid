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
    public class Doja: StardewDruid.Character.Character
    {

        public Doja()
        {
        }

        public Doja(CharacterHandle.characters type)
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

        public override void DrawHat(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            bool fliphat = SpriteFlip();

            Vector2 hatPosition = spritePosition - new Vector2(0, 16 * setScale);

            Rectangle hatFrame = hatFrames[netDirection.Value][0];

            if (netIdle.Value == (int)Character.idles.kneel)
            {

                hatPosition = spritePosition - new Vector2(0, 10f * setScale);

                hatFrame = hatFrames[1][0];

            }
            else if (netSpecial.Value == (int)Character.specials.gesture)
            {


                hatPosition = spritePosition - new Vector2(0, 16f * setScale);

                hatFrame = hatFrames[1][0];

            }
            else
            {

                if (netDirection.Value == 2)
                {

                    if (fliphat)
                    {

                        hatPosition.X += 2;

                    }
                    else
                    {

                        hatPosition.X -= 2;

                    }

                }

            }

            b.Draw(
                characterTexture,
                hatPosition,
                hatFrame,
                Color.White * fade,
                0f,
                new Vector2(16),
                setScale,
                fliphat ? (SpriteEffects)1 : 0,
                drawLayer + 0.0001f
            );
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
