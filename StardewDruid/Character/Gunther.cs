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
    public class Gunther : StardewDruid.Character.Character
    {

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

            idleFrames[idles.standby] = new()
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

        public override void DrawHat(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {
            bool fliphat = SpriteFlip();

            Vector2 hatPosition = localPosition - new Vector2(0, 2 * setScale);

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

            b.Draw(
                characterTexture,
                hatPosition,
                hatFrames[netDirection.Value][0],
                Color.White * fade,
                0f,
                new Vector2(16),
                setScale,
                fliphat ? (SpriteEffects)1 : 0,
                drawLayer + 0.0001f
            );

            /*b.Draw(
                characterTexture,
                //localPosition - new Vector2(netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? 30 : 32, 72),
                hatFrames[netDirection.Value][0],
                Color.White,
                0f,
                Vector2.Zero,
                4f,
                netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer + 0.002f
            );*/

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {
            return false;
        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.invoke);

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
