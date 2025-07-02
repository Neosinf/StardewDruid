using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Event;
using StardewDruid.Handle;
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

            hatSelect = 3;

            hatVectors = new()
            {

                [hats.stand] = new()
                {
                    [0] = new(0, 14),
                    [1] = new(0, 14),
                    [2] = new(0, 14),
                    [3] = new(0, 14),
                    [4] = new(0, 14),
                    [6] = new(0, 14),
                },
                [hats.jump] = new()
                {
                    [0] = new(0, 14),
                    [1] = new(0, 14),
                    [2] = new(0, 14),
                    [3] = new(0, 14),
                    [4] = new(0, 14),
                    [6] = new(0, 14),
                },
                [hats.kneel] = new()
                {
                    [0] = new(0, 8),
                    [1] = new(0, 8),
                    [2] = new(0, 8),
                    [3] = new(0, 8),
                    [4] = new(0, 8),
                    [6] = new(0, 8),
                },
                [hats.launch] = new()
                {
                    [0] = new(0, 8),
                    [1] = new(0, 8),
                    [2] = new(0, 11),
                    [3] = new(0, 8),
                    [4] = new(0, 8),
                    [6] = new(0, 11),
                },
            };

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
