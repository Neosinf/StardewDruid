using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewValley;
using System.Collections.Generic;

namespace StardewDruid.Character
{
    public class Wizard : StardewDruid.Character.Character
    {
        public Wizard()
        {
        }

        public Wizard(CharacterHandle.characters type)
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
