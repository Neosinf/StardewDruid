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
    public class Witch : StardewDruid.Character.Character
    {

        public Witch()
        {
        }

        public Witch(CharacterHandle.characters type)
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
                    new(128, 64, 32, 32),
                },
                [1] = new()
                {
                    new(128, 32, 32, 32),
                },
                [2] = new()
                {
                    new(128, 0, 32, 32),
                },
                [3] = new()
                {
                    new(128, 32, 32, 32),
                },
            };

        }

        public override void DrawHat(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
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

        public override void behaviorOnFarmerPushing()
        {

            return;

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {

            if (netSceneActive.Value)
            {

                return EngageDialogue(who);

            }

            return false;

        }

    }

}
