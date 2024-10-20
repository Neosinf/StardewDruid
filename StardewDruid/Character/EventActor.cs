using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
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
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace StardewDruid.Character
{
    public class EventActor : StardewDruid.Character.Character
    {

        public EventActor()
        {
        }

        public EventActor(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            base.LoadOut();

            gait = 1f;

            if (characterType == CharacterHandle.characters.Dwarf)
            {

                walkFrames = FrameSeries(16, 24, 0, 0, 4);

            }
            else
            {

                walkFrames = FrameSeries(16, 32, 0, 0, 4);

            }

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Rectangle useFrame = walkFrames[netDirection.Value][moveFrame];

            Vector2 spritePosition = localPosition + new Vector2(32, 64f - (2f * useFrame.Height));

            float drawLayer = (float)StandingPixel.Y / 10000f;

            b.Draw(
                characterTexture,
                spritePosition,
                useFrame,
                Color.White,
                0f,
                new Vector2(useFrame.Width/2,useFrame.Height/2),
                4f,
                SpriteEffects.None,
                drawLayer + 0.0001f
            );

            DrawShadow(b, spritePosition, drawLayer);

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {
            return false;
        }

    }

}
