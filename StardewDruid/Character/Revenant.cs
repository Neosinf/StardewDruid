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

namespace StardewDruid.Character
{
    public class Revenant : StardewDruid.Character.Character
    {

        public Revenant()
        {


        }

        public Revenant(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void DrawStandby(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {

            b.Draw(
                 characterTexture,
                 SpritePosition(localPosition),
                 new(160,32,32,32),
                 Color.White * fade,
                 0f,
                 new Vector2(16,16),
                 setScale,
                 0,
                 drawLayer
             );

            DrawShadow(b, localPosition, drawLayer);
        }

    }

}
