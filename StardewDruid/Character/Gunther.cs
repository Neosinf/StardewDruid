﻿using Microsoft.Xna.Framework;
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
    public class Gunther : StardewDruid.Character.Character
    {

        public Dictionary<int, List<Rectangle>> hatFrames = new();

        public Gunther()
        {
        }

        public Gunther(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            base.LoadOut();

            idleFrames = new()
            {
                [0] = new List<Rectangle> { new Rectangle(160, 32, 32, 32), },
            };

            hatFrames = new()
            {
                [0] = new()
                {
                    new(32, 64, 32, 32),
                },
                [1] = new()
                {
                    new(32, 32, 32, 32),
                },
                [2] = new()
                {
                    new(32, 0, 32, 32),
                },
                [3] = new()
                {
                    new(32, 32, 32, 32),
                },
            };

        }
        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            base.draw(b, alpha);

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = (float)StandingPixel.Y / 10000f + 0.002f;

            b.Draw(
                characterTexture,
                localPosition - new Vector2(netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? 30 : 32,72),
                hatFrames[netDirection.Value][0],
                Color.White,
                0f,
                Vector2.Zero,
                4f,
                netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer + 0.0001f
            );

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {
            return false;
        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecialActive.Set(true);

            specialTimer = 90;

            cooldownTimer = cooldownInterval;

            LookAtTarget(monster.Position, true);

            List<int> intList = new List<int>()
            {
                96,
                97,
                98,
                99,
                100,
                101,
                103,
                104,
                105,
                106,
                107,
                108,
                109,
                110,
                111,
                112,
                113,
                114,
                115,
                116,
                117,
                118,
                119,
                120,
                121,
                122,
                123,
                124,
                125,
                126,
                127,
                579,
                580,
                581,
                582,
                583,
                584,
                585,
                586,
                587,
                588,
                589
            };

            ThrowHandle throwJunk = new(Position, monster.Position, intList[Mod.instance.randomIndex.Next(intList.Count)]);

            throwJunk.pocket = true;

            throwJunk.register();

            return true;

        }

    }

}
